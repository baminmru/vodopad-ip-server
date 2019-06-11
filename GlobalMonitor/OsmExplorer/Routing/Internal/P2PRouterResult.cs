using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using OsmExplorer.Rendering;
using OsmExplorer.Spatial;
using OsmExplorer.Units;

namespace OsmExplorer.Routing.Internal
{
    internal sealed class P2PRouterResult : IRenderable
    {
        private List<LatLon> m_RoutePoints;
        private TimeSpan m_TravelTime;
        private TimeSpan m_Runtime;
        private Length m_TravelDistance;
        private P2PRouterDirections m_Directions;

        internal P2PRouterResult(List<LatLon> routePoints, 
            TimeSpan travelTime, 
            Length travelDistance, 
            TimeSpan runtime, 
            P2PRouterDirections directions)
        {
            m_RoutePoints = routePoints;
            m_TravelTime = travelTime;
            m_TravelDistance = travelDistance;
            m_Runtime = runtime;
            m_Directions = directions;
        }

        public LatLon[] RoutePoints
        {
            get
            {
                return m_RoutePoints.ToArray();
            }
        }
        public TimeSpan TravelTime
        {
            get
            {
                return m_TravelTime;
            }
        }
        public Length TravelDistance
        {
            get
            {
                return m_TravelDistance;
            }
        }
        public TimeSpan AlgorithmRuntime
        {
            get
            {
                return m_Runtime;
            }
        }
        public P2PRouterDirections Directions
        {
            get
            {
                return m_Directions;
            }
        }
        public Pen RenderPen { get; set; }
        public void Render(Graphics graphics, RenderCollection collection)
        {
            if (this.RenderPen == null)
                this.RenderPen = new Pen(Color.FromArgb(127, Color.Blue));
                
            this.RenderPen.Width = 4f;
            this.RenderPen.StartCap = LineCap.Round;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 1; i < RoutePoints.Count(); i++)
            {
                PointF next = collection.LatLonToPoint(RoutePoints[i]);

                if (Directions.IsDecisionPt(RoutePoints[i]))
                {
                    this.RenderPen.EndCap = LineCap.ArrowAnchor;
                    this.RenderPen.Width = 4.5F;
                }
                else 
                {
                    this.RenderPen.EndCap = LineCap.Round;
                    this.RenderPen.Width = 4F;
                }

                graphics.DrawLine(this.RenderPen,
                    collection.LatLonToPoint(RoutePoints[i - 1]),
                    collection.LatLonToPoint(RoutePoints[i]));
            }
        }
    }
}
