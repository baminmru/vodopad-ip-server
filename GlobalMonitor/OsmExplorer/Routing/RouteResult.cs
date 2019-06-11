using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using OsmExplorer.Rendering;
using OsmExplorer.Routing.Internal;
using OsmExplorer.Spatial;
using OsmExplorer.Units;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Output class produced by the routing engine containing
    /// results from a calculated route, driving directions and 
    /// rendering data.
    /// </summary>
    public class RouteResult : IRenderable
    {
        #region Private
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<LatLon> m_RoutePoints;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<P2PRouterResult> m_Results;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan m_TravelTime;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TimeSpan m_Runtime;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Length m_TravelDistance;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DrivingDirections m_Directions;
        #endregion
        #region Internal
        internal RouteResult(params P2PRouterResult[] results) 
        {
            m_RoutePoints = new List<LatLon>();
            m_TravelTime = new TimeSpan(0);
            m_Results = new List<P2PRouterResult>();
            List<TimeSpan> runTimes = new List<TimeSpan>();
            foreach (var result in results) 
            {
                m_RoutePoints.AddRange(result.RoutePoints);
                m_Results.Add(result);
                runTimes.Add(result.AlgorithmRuntime);
                m_TravelTime += result.TravelTime;
                m_TravelDistance += result.TravelDistance;
            }
            m_Directions = new DrivingDirections(Array.ConvertAll(results, x=>x.Directions));
            m_Runtime = runTimes.Max();
        }
        #endregion

        /// <summary>
        /// Start location of the route.
        /// </summary>
        public LatLon StartLocation 
        {
            get
            {
                return RoutePoints[0];
            }
        }
        /// <summary>
        /// End location of the route.
        /// </summary>
        public LatLon EndLocation 
        {
            get
            {
                return RoutePoints[RoutePoints.Count() - 1];
            }
        }
        /// <summary>
        /// Points that make up this route.
        /// </summary>
        public LatLon[] RoutePoints 
        {
            get
            {
                return m_RoutePoints.ToArray();
            }
        }
        /// <summary>
        /// Estimates total travel time of the route.
        /// </summary>
        public TimeSpan TravelTime 
        {
            get
            {
                return m_TravelTime;
            }
        }
        /// <summary>
        /// Estimated total travel distance of the route.
        /// </summary>
        public Length TravelDistance 
        {
            get
            {
                return m_TravelDistance;
            }
        }
        /// <summary>
        /// Amount of the time the RoutingEngine required to calculate the route.
        /// </summary>
        public TimeSpan AlgorithmRuntime 
        {
            get
            {
                return m_Runtime;
            }
        }
        /// <summary>
        /// Gets a set of driving directions for the route.
        /// </summary>
        public DrivingDirections Directions 
        {
            get
            {
                return m_Directions;
            }
        }

        /// <summary>
        /// Gets or sets the System.Drawing.Pen that renders this RouteResult instance.
        /// </summary>
        public Pen RenderPen { get; set; }
        /// <summary>
        /// Renders the RouteResult instance using the specified System.Drawing.Graphics
        /// and RenderCollection.
        /// </summary>
        /// <param name="graphics">A System.Drawing.Graphics object.</param>
        /// <param name="collection">An associated RenderCollection.</param>
        public void Render(Graphics graphics, RenderCollection collection)
        {
            lock (m_Results) 
            {
                foreach (var dir in m_Results) 
                {
                    dir.RenderPen = this.RenderPen;
                    dir.Render(graphics, collection);
                }
            }
        }
    }
}
