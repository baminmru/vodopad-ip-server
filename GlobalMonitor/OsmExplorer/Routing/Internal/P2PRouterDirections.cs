using System.Collections.Generic;
using OsmExplorer.Spatial;

namespace OsmExplorer.Routing.Internal
{
    internal sealed class P2PRouterDirections
    {
        private List<Direction> m_Directions;
        private HashSet<string> m_DirectionSet;
        private HashSet<LatLon> m_DecisionPoints;

        public P2PRouterDirections() 
        {
            m_Directions = new List<Direction>();
            m_DirectionSet = new HashSet<string>();
            m_DecisionPoints = new HashSet<LatLon>();
        }

        public List<Direction> Directions
        {
            get
            {
                return m_Directions;
            }
        }
        public void Add(Direction direction)
        {
            if (m_DirectionSet.Add(direction.DirectionMsg))
                m_Directions.Add(direction);
        }
        public void Add(Direction direction, LatLon decisionPt) 
        {
            if (m_DirectionSet.Add(direction.DirectionMsg)) 
            {
                m_Directions.Add(direction);
                m_DecisionPoints.Add(decisionPt);
            }
        }
        public bool IsDecisionPt(LatLon pt) 
        {
            return m_DecisionPoints.Contains(pt);
        }
    }
}
