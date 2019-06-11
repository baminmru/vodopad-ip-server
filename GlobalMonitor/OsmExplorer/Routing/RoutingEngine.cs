using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OsmExplorer.Data;
using OsmExplorer.Exceptions;
using OsmExplorer.Routing.Internal;
using OsmExplorer.Spatial;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Instantiates the RoutingEngine class for point-to-point routing along an
    /// array of 2 or more RouteStops.
    /// </summary>
    /// <remarks>The RoutingEngine utilizes a bidirectional, hierarchical A* search 
    /// algorithm based on the HBA* algorithm
    /// (see http://www.icsi.berkeley.edu/pubs/techreports/TR-09-007.pdf). </remarks>
    public sealed class RoutingEngine
    {
        #region Private
        private void ResolveStop(object args)
        {
            object[] arguments = args as object[];
            RouteStop stop = arguments[0] as RouteStop;
            SearchDirection[] directions = (SearchDirection[])arguments[1];
            int index = (int)arguments[2];

            LatLon ResolvedPoint;
            LatLon[] InterpolatedStartPoints;
            RoadLink StartRl = SpatialQuery.QueryLink(stop.Location, out ResolvedPoint, out InterpolatedStartPoints);
            RouteLink[][] LinkArray = new RouteLink[2][];

            foreach (var direction in directions) 
            {
                var FirstLinks = RoutingNetwork.GetRouteLinks(StartRl, direction);
                LatLon[] NewPointArray;
                int ClosestPointNdx = Array.FindIndex(InterpolatedStartPoints, x => x == ResolvedPoint);
                for (int i = 0; i < FirstLinks.Count(); i++)
                {
                    foreach (var cellId in FirstLinks[i].DestinationCellIds)
                    {
                        if (cellId.Category > (uint)StartRl.Category)
                            continue;
                    }
                    if (FirstLinks[i].LastPoint == FirstLinks[i].End)
                    {
                        NewPointArray = InterpolatedStartPoints.Skip(ClosestPointNdx).ToArray();
                        if (NewPointArray.Count() < 2)
                        {
                            NewPointArray = new LatLon[2]
                        {
                            InterpolatedStartPoints[InterpolatedStartPoints.Count() - 2],
                            InterpolatedStartPoints[InterpolatedStartPoints.Count() - 1]
                        };
                        }
                        FirstLinks[i].Points = NewPointArray;
                    }
                    else
                    {
                        NewPointArray = InterpolatedStartPoints.Take(ClosestPointNdx).ToArray();
                        if (NewPointArray.Count() < 2)
                        {
                            NewPointArray = new LatLon[2] 
                        {
                            InterpolatedStartPoints[0],
                            InterpolatedStartPoints[1]
                        };
                        }
                        FirstLinks[i].Points = NewPointArray;
                    }
                }
                LinkArray[(int)direction] = FirstLinks;
            }

            RoutingNetwork.LoadSubNetwork(StartRl);
            switch (index) 
            {
                case -1:
                    m_End = new RouteStop(ResolvedPoint);
                    m_ResolvedEndLinks = LinkArray;
                    return;
                case -2:
                    m_Start = new RouteStop(ResolvedPoint);
                    m_ResolvedStartLinks = LinkArray;
                    return;
                default:
                    m_ResolvedStops.Insert(index, new RouteStop(ResolvedPoint));
                    m_ResolvedLinks.Insert(index, LinkArray);
                    return;
            }
        }
        private void CalculateP2P(object args) 
        {
            RouterArgs Args = args as RouterArgs;
            Point2PointRouter router = new Point2PointRouter(Args);
            m_Results[Args.RowIndex] = router.Calculate();
            m_RoutingThreadsComplete[Args.RowIndex].Set();
        }
        private void CalculateM2M(object args)
        {
        }

        private ManualResetEvent[] m_RoutingThreadsComplete;
        private P2PRouterResult[] m_Results;
        private List<Thread> m_ResolvingThreads;

        private List<RouteLink[][]> m_ResolvedLinks;
        private List<RouteStop> m_ResolvedStops;
        private RouteLink[][] m_ResolvedStartLinks;
        private RouteLink[][] m_ResolvedEndLinks;
        private RouteStop m_Start;
        private RouteStop m_End;

        private RoutingStrategy m_Strategy;
        private SpeedProfile m_Profile;
        private VehicleType m_VehicleType;

        private bool m_AvoidTollRoads;
        private bool m_AvoidUTurns;
        #endregion

        /// <summary>
        /// Initializes a new instance of the RoutingEngine.
        /// </summary>
        public RoutingEngine()
        {
            CategoryHueristic.RangeParameter = 5000;
            AvoidUTurns = false;
            AvoidTollRoads = false;
            m_VehicleType = VehicleType.Auto;
            m_ResolvedStops = new List<RouteStop>(100);
            m_ResolvedLinks = new List<RouteLink[][]>(100);
            m_Profile = new SpeedProfile();
            m_ResolvingThreads = new List<Thread>();
        }

        /// <summary>
        /// Gets the array of stops that are part of the current route.
        /// </summary>
        public RouteStop[] Stops 
        {
            get 
            {
                return m_ResolvedStops.ToArray();
            }
        }
        /// <summary>
        /// Gets or sets whether the RoutingEngine will avoid making
        /// U-turns. Default is false;
        /// </summary>
        public bool AvoidUTurns
        {
            get
            {
                return m_AvoidUTurns;
            }
            set
            {
                m_AvoidUTurns = value;
            }
        }
        /// <summary>
        /// Gets or sets whether RoutingEngine will avoid toll roads. 
        /// Default is false;
        /// </summary>
        public bool AvoidTollRoads
        {
            get
            {
                return m_AvoidTollRoads;
            }
            set
            {
                m_AvoidTollRoads = value;
            }
        }
        /// <summary>
        /// Gets or sets the vehicle type. 
        /// </summary>
        public VehicleType vehicleType
        {
            get
            {
                return m_VehicleType;
            }
            set
            {
                m_VehicleType = value;
            }
        }
        /// <summary>
        /// Gets or sets the routing strategy used for this route.
        /// </summary>
        public RoutingStrategy Strategy
        {
            get
            {
                return m_Strategy;
            }
            set
            {
                m_Strategy = value;
            }
        }
        /// <summary>
        /// Gets or sets the road speed profile used for RoutingStrategy.Fastest
        /// routing option.
        /// </summary>
        public SpeedProfile Profile 
        {
            get 
            {
                return m_Profile;
            }
            set 
            {
                m_Profile = value;
            }
        }

        /// <summary>
        /// Calculates a route for the specified set of route stops.
        /// </summary>
        /// <returns>Results from the calculated route</returns>
        public RouteResult CalculateRoute()
        {
            if (!DataRepository.DatabaseOpen)
                throw new RoutingDataNotFoundException();

            m_ResolvingThreads.ForEach(thread => thread.Join());

            if (m_ResolvedLinks.Any(x => x.Any(y => y == null)))
                throw new RouteStopTooFarAwayException();

            List<RouteStop> Stops = new List<RouteStop>(m_ResolvedStops.Count + 2);
            List<RouteLink[][]> ResolvedLinks = new List<RouteLink[][]>(m_ResolvedStops.Count + 2);
            if (m_Start != null) 
            {
                Stops.Add(m_Start);
                ResolvedLinks.Add(m_ResolvedStartLinks);
            }
                
            Stops.AddRange(m_ResolvedStops);
            ResolvedLinks.AddRange(m_ResolvedLinks);

            if (m_End != null) 
            {
                Stops.Add(m_End);
                ResolvedLinks.Add(m_ResolvedEndLinks);
            }
            if (Stops.Count < 2)
                throw new NotEnoughRouteStopsException("Not enough route stops. Route must contain 2 or more stops.");

            m_Results = new P2PRouterResult[Stops.Count() - 1];
            m_RoutingThreadsComplete = new ManualResetEvent[Stops.Count() - 1];
            for (int i = 1; i < Stops.Count(); i++)
            {
                RouterArgs args = new RouterArgs();
                args.RowIndex = i - 1;
                args.StartLinks = ResolvedLinks[i - 1][0];
                args.EndLinks = ResolvedLinks[i][1];
                args.Start = Stops[i - 1].Location;
                args.End = Stops[i].Location;
                args.Strategy = Strategy;
                args.vehicleType = vehicleType;
                args.AvoidTollways = AvoidTollRoads;
                args.AvoidUTurns = AvoidUTurns;
                args.Profile = m_Profile;
                m_RoutingThreadsComplete[i - 1] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateP2P), args);
            }
            WaitHandle.WaitAll(m_RoutingThreadsComplete);
            if (m_Results.Any(result => result == null))
                throw new RouteNotFoundException();

            RouteResult Result = new RouteResult(m_Results);
            return Result;
        }
        /// <summary>
        /// Calculates an array of results for each route stop to every other
        /// stop.
        /// </summary>
        /// <returns>A RouteResultMatrix of RouteResults for each pair of route stops.</returns>
        public RouteResultMatrix CalculateManyToMany() 
        {
            throw new NotImplementedException("Not yet implemented.");
            //if (!DataRepository.DatabaseOpen)
            //    throw new RoutingDataNotFoundException();

            //m_ResolvingThreads.ForEach(thread => thread.Join());

            //if (m_ResolvedLinks.Any(x => x.Any(y => y == null)))
            //    throw new RouteStopTooFarAwayException();

            //if (m_ResolvedStops.Count < 2)
            //    throw new NotEnoughRouteStopsException("Not enough route stops. Route must contain 2 or more stops.");

            //m_ResultArray = new RouteResult[m_ResolvedStops.Count()][];
            //m_ThreadArray = new Thread[m_ResolvedStops.Count()][];

            //for (int i = 0; i < m_ResolvedStops.Count(); i++)
            //{
            //    m_ResultArray[i] = new RouteResult[m_ResolvedStops.Count()];
            //    m_ThreadArray[i] = new Thread[m_ResolvedStops.Count()];
            //    for (int j = 0; j < m_ResolvedStops.Count(); j++) 
            //    {
            //        if (i == j)
            //            continue;

            //        RouterArgs args = new RouterArgs();
            //        args.RowIndex = i;
            //        args.ColumnIndex = j;
            //        args.StartLinks = m_ResolvedLinks[i][0];
            //        args.EndLinks = m_ResolvedLinks[j][1];
            //        args.Start = m_ResolvedStops[i];
            //        args.End = m_ResolvedStops[j];
            //        args.Strategy = Strategy;
            //        args.vehicleType = vehicleType;
            //        args.AvoidTollways = AvoidTollRoads;
            //        args.AvoidUTurns = AvoidUTurns;
            //        args.Profile = m_Profile;

            //        ParameterizedThreadStart tStart = new ParameterizedThreadStart(CalculateM2M);
            //        m_ThreadArray[i][j] = new Thread(tStart);
            //        m_ThreadArray[i][j].Start(args);
            //    }
            //}

            //foreach (var threadArray in m_ThreadArray) 
            //{
            //    foreach (var thread in threadArray) 
            //    {
            //        if (thread != null)
            //            thread.Join();
            //    }
            //}
            //for (int i = 0; i < m_ThreadArray.Count(); i++) 
            //{
            //    for (int j = 0; j < m_ThreadArray.Count(); j++) 
            //    {
            //        if (i == j)
            //            continue;

            //        if (m_ThreadArray[i][j] == null)
            //            throw new RouteNotFoundException();
            //    }
            //}

            //return new RouteResultMatrix(m_ResultArray);
        }
        /// <summary>
        /// Sets a start location for the route by inserting a route stop
        /// at the beginning of the list of stops.
        /// </summary>
        /// <param name="start">Start location.</param>
        public void SetStart(ref RouteStop start) 
        {
            if (DataRepository.DatabaseOpen) 
            {
                Thread thread = new Thread(new ParameterizedThreadStart(ResolveStop));
                m_ResolvingThreads.Add(thread);
                thread.Start(new object[] { start, new SearchDirection[] { SearchDirection.Forward, SearchDirection.Backward }, -2 });
                thread.Join();
                start = m_Start;
            }
        }
        /// <summary>
        /// Sets an end location for the route by inserting a route stop
        /// at the end of the list of stops.
        /// </summary>
        /// <param name="end">End location.</param>
        public void SetEnd(ref RouteStop end) 
        {
            if (DataRepository.DatabaseOpen) 
            {
                Thread thread = new Thread(new ParameterizedThreadStart(ResolveStop));
                m_ResolvingThreads.Add(thread);
                thread.Start(new object[] { end, new SearchDirection[] { SearchDirection.Forward, SearchDirection.Backward }, -1 });
                thread.Join();

                end = m_End;
            }
        }
        /// <summary>
        /// Resolved the specified LatLon to a start location on the map and adds
        /// to the list of route stops.
        /// </summary>
        /// <param name="stop">Stop location.</param>
        public void AddStop(ref RouteStop stop)
        {
            if (DataRepository.DatabaseOpen) 
            {
                int index = m_ResolvedStops.Count();
                Thread thread = new Thread(new ParameterizedThreadStart(ResolveStop));
                m_ResolvingThreads.Add(thread);
                thread.Start(new object[] { stop, new SearchDirection[] { SearchDirection.Forward, SearchDirection.Backward }, index });
                thread.Join();
                stop = m_ResolvedStops[m_ResolvedStops.Count() - 1];
            }
        }
        /// <summary>
        /// Deletes the specified route stop from the current route.
        /// </summary>
        /// <param name="stop">Stop to delete.</param>
        /// <returns>True if the stop is successfully deleted.</returns>
        public bool DeleteStop(RouteStop stop)
        {
            int ndx = m_ResolvedStops.IndexOf(stop);
            bool removed = m_ResolvedStops.Remove(stop);
            if (removed)
            {
                m_ResolvedLinks.RemoveAt(ndx);
            }
            else if (stop.Equals(m_Start)) 
            {
                m_Start = null;
                return true;
            }
            else if (stop.Equals(m_End)) 
            {
                m_End = null;
                return true;
            }
            return removed;
        }
        /// <summary>
        /// Clears all stops from the current route.
        /// </summary>
        public void ClearStops()
        {
            m_ResolvedStops.Clear();
            m_ResolvedLinks.Clear();
            m_ResolvedStartLinks = null;
            m_ResolvedEndLinks = null;
            m_Start = null;
            m_End = null;
        }
        /// <summary>
        /// Returns the number of stops on the current route.
        /// </summary>
        public int StopCount
        {
            get
            {
                return m_ResolvedStops.Count;
            }
        }
    }
}
