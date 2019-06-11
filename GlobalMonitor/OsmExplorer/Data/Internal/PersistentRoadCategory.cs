using Volante;

namespace OsmExplorer.Data.Internal
{
    internal sealed class PersistentRoadCategory : Persistent
    {
        private RoadCategory m_Value;

        public PersistentRoadCategory() { }
        public PersistentRoadCategory(RoadCategory value) 
        {
            m_Value = value;
        }

        public RoadCategory Value 
        {
            get 
            {
                return m_Value;
            }
        }
        public static implicit operator RoadCategory(PersistentRoadCategory pCategory) 
        {
            return pCategory.m_Value;
        }
    }
}
