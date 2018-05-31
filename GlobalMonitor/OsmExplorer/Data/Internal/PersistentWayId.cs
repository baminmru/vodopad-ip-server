using Volante;

namespace OsmExplorer.Data.Internal
{
    internal sealed class PersistentWayId : Persistent
    {
        private long m_WayId;

        public PersistentWayId() { }
        public PersistentWayId(long wayId) 
        {
            m_WayId = wayId;
        }

        public long WayId 
        {
            get 
            {
                return m_WayId;
            }
        }
        public static implicit operator long(PersistentWayId wayId) 
        {
            return wayId.WayId;
        }
    }
}
