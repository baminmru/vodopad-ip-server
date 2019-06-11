using System.Linq;
using OsmExplorer.Data;
using OsmExplorer.Spatial;
using Volante;

namespace OsmExplorer.Routing.Internal
{
    internal sealed class RouteLink
    {
        #region Private
        private RoadLink m_rlink;
        private LatLon[] m_Points;
        private LatLon m_Start;
        private LatLon m_End;
        private TravelDirection m_Direction;
        private double m_TravelTime;
        private IPArray<CellId> m_DestinationCellIds;
        #endregion

        public RouteLink(RoadLink rl, 
            LatLon start, 
            LatLon end, 
            TravelDirection direction, 
            IPArray<CellId> destinationCellIds)
        {
            m_rlink = rl;
            m_Start = start;
            m_End = end;
            m_Direction = direction;
            m_DestinationCellIds = destinationCellIds;
        }

        public LatLon Start 
        {
            get 
            {
                return m_Start;
            }
        }
        public LatLon End 
        {
            get 
            {
                return m_End;
            }
        }
        public TravelDirection Direction 
        {
            get 
            {
                return m_Direction;
            }
        }
        public IPArray<CellId> DestinationCellIds 
        {
            get 
            {
                return m_DestinationCellIds;
            }
        }

        public ulong LinkId
        {
            get
            {
                return m_rlink.LinkId;
            }
        }
        public long WayId
        {
            get
            {
                return m_rlink.WayId;
            }
        }
        public string[] Names 
        {
            get 
            {
                return m_rlink.Names;
            }
        }
        public RoadCategory RoadCategory
        {
            get
            {
                return m_rlink.Category;
            }
        }
        public double TravelTime 
        {
            get 
            {
                return m_TravelTime;
            }
            set 
            {
                m_TravelTime = value;
            }
        }
        public LatLon FirstPoint
        {
            get
            {
                return this.Points[0];
            }
        }
        public LatLon LastPoint
        {
            get
            {
                return this.Points[Points.Count() - 1];
            }
        }
        public RoadFlags Flags
        {
            get
            {
                return m_rlink.Flags;
            }
        }
        public LatLon[] Points
        {
            get
            {
                if (m_Points != null)
                    return m_Points;
                return m_rlink.Points;
            }
            internal set
            {
                m_Points = value;
            }
        }

        public override int GetHashCode()
        {
            return m_rlink.GetHashCode();
        }
        public bool Equals(RouteLink other)
        {
            if (other != null)
                return this.LinkId == other.LinkId;
            return false;
        }
        public override bool Equals(object obj)
        {
            RouteLink other = obj as RouteLink;
            return this.Equals(other);
        }
    }
}
