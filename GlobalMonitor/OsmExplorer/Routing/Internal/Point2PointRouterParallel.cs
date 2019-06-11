using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OsmExplorer.Collections;
using OsmExplorer.Spatial;

namespace OsmExplorer.Routing.Internal
{
    /// <summary>
    /// A parallel implementation of the HBA* algorithm that runs the forward 
    /// and backward searches on their own threads. Currently very buggy and 
    /// unpredictable (use of non-parallel Point2PointRouter is recommended for now).
    /// </summary>
    internal sealed class Point2PointRouterParallel : RouterBase
    {
        #region Private
        private void ForwardSearch()
        {
            Route ForwardPath;
            Route BackwardPath;
            while (m_BackwardThread.IsAlive)
            {
                if (ForwardOpenSet.IsEmpty)
                    return;

                BestForwardCategory = Math.Min((uint)ForwardOpenSet.Peek().LastLink.RoadCategory, BestForwardCategory);

                if (BestForwardCategory < BestBackwardCategory)
                    continue;

                ForwardPath = ForwardOpenSet.Pop();
                if (!ForwardClosedSet.TryAdd(ForwardPath.LastLink.End.Id, ForwardPath))
                    continue;

                if (BackwardClosedSet.TryGetValue(ForwardPath.LastLink.End.Id, out BackwardPath))
                {
                    m_Result = RouteConstructor(ForwardPath, BackwardPath);
                    return;
                }

                Stack<RouteLink> ForwardCandidates = RoutingNetwork.Candidates(ForwardPath, SearchDirection.Forward);
                while (ForwardCandidates.Count > 0)
                {
                    RouteLink rl = ForwardCandidates.Pop();
                    if (ForwardPath.LastLink.Equals(rl))
                        continue;
                    var cost = Cost(ForwardPath.LastLink, rl, SearchDirection.Forward);
                    if (cost >= ProhibitedCost)
                        continue;
                    var newPath = ForwardPath.AddLink(rl, cost);
                    ForwardOpenSet.Push(newPath.TotalCost + Estimate(rl.End, End, Start), newPath);
                }
            }
        }
        private void BackwardSearch()
        {
            Route BackwardPath;
            Route ForwardPath;
            while (m_ForwardThread.IsAlive)
            {
                if (BackwardOpenSet.IsEmpty)
                    return;

                BestBackwardCategory = Math.Min((uint)BackwardOpenSet.Peek().LastLink.RoadCategory, BestBackwardCategory);

                if (BestBackwardCategory < BestForwardCategory)
                    continue;

                BackwardPath = BackwardOpenSet.Pop();
                if (!BackwardClosedSet.TryAdd(BackwardPath.LastLink.End.Id, BackwardPath))
                    continue;

                if (ForwardClosedSet.TryGetValue(BackwardPath.LastLink.End.Id, out ForwardPath))
                {
                    m_Result = RouteConstructor(ForwardPath, BackwardPath);
                    return;
                }
                Stack<RouteLink> BackwardCandidates = RoutingNetwork.Candidates(BackwardPath, SearchDirection.Backward);
                while (BackwardCandidates.Count > 0)
                {
                    RouteLink rl = BackwardCandidates.Pop();
                    if (BackwardPath.LastLink.Equals(rl))
                        continue;
                    var cost = Cost(BackwardPath.LastLink, rl, SearchDirection.Backward);
                    if (cost >= ProhibitedCost)
                        continue;
                    var newPath = BackwardPath.AddLink(rl, cost);
                    BackwardOpenSet.Push(newPath.TotalCost + Estimate(rl.End, Start, End), newPath);
                }
            }
        }

        private Thread m_ForwardThread;
        private Thread m_BackwardThread;
        private P2PRouterResult m_Result;
        #endregion

        public Point2PointRouterParallel(RouterArgs args)
            : base(args)
        {
            Start = args.Start;
            End = args.End;
            Strategy = args.Strategy;
            VehicleType = args.vehicleType;
            Profile = args.Profile;
            InitializeOpenSet(args.StartLinks, args.EndLinks);
            ForwardClosedSet = new ConcurrentDictionary<long, Route>(2, 1000);
            BackwardClosedSet = new ConcurrentDictionary<long, Route>(2, 1000);
            stopWatch = new Stopwatch();
        }

        public P2PRouterResult Calculate()
        {
            stopWatch.Start();
            m_Result = null;
            this.m_ForwardThread = new Thread(new ThreadStart(ForwardSearch));
            this.m_BackwardThread = new Thread(new ThreadStart(BackwardSearch));
            this.m_ForwardThread.Priority = ThreadPriority.AboveNormal;
            this.m_BackwardThread.Priority = ThreadPriority.AboveNormal;
            this.m_ForwardThread.Start();
            this.m_BackwardThread.Start();

            m_ForwardThread.Join();
            m_BackwardThread.Join();

            return m_Result;
        }

        protected override PriorityHeap<double, Route> ForwardOpenSet { get; set; }
        protected override PriorityHeap<double, Route> BackwardOpenSet { get; set; }
        protected override ConcurrentDictionary<long, Route> ForwardClosedSet { get; set; }
        protected override ConcurrentDictionary<long, Route> BackwardClosedSet { get; set; }
        protected override uint BestForwardCategory { get; set; }
        protected override uint BestBackwardCategory { get; set; }
        protected override Stopwatch stopWatch { get; set; }
        protected override LatLon End { get; set; }
        protected override LatLon Start { get; set; }
        protected override RoutingStrategy Strategy { get; set; }
        protected override VehicleType VehicleType { get; set; }
        protected override SpeedProfile Profile { get; set; }
    }
}
