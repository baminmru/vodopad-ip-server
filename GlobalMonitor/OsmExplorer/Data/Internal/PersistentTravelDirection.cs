using OsmExplorer.Spatial;
using Volante;

namespace OsmExplorer.Data.Internal
{
    internal sealed class PersistentTravelDirection : Persistent
    {
        private TravelDirection m_Direction;

        public PersistentTravelDirection() { }
        public PersistentTravelDirection(TravelDirection direction) 
        {
            m_Direction = direction;
        }

        public int Id 
        {
            get 
            {
                return (int)m_Direction;
            }
        }
        public static implicit operator TravelDirection(PersistentTravelDirection pDirection) 
        {
            return pDirection.m_Direction;
        }
    }
}
