using System.Collections.Generic;
using System.Linq;
using Volante;
using OsmExplorer.Units;

namespace OsmExplorer.Data.Internal.Primitives
{
    internal class Way : Persistent
    {
        private long m_WayId;
        private Speed m_TraveSpeed;
        private PersistentString m_HighwayTag;
        private long[] m_NodeIds;
        private IIndex<string, PersistentString> m_Tags;

        public Way() { }
        public Way(long wayId, 
            long[] nodeIds, 
            Speed travelSpeed,
            PersistentString highwayTag,
            IEnumerable<KeyValuePair<string, string>> Tags, 
            IDatabase Db)
        {
            m_WayId = wayId;
            m_NodeIds = nodeIds;
            m_TraveSpeed = travelSpeed;
            m_HighwayTag = highwayTag;
            m_Tags = Db.CreateIndex<string, PersistentString>(IndexType.Unique);
            foreach (var kvp in Tags)
            {
                m_Tags.Put(kvp.Key, new PersistentString(kvp.Value));
            }
        }

        public long WayId
        {
            get
            {
                return m_WayId;
            }
            set
            {
                if (m_WayId == value)
                    return;
                m_WayId = value;
                Modify();
            }
        }
        public string HighwayTag 
        {
            get 
            {
                return m_HighwayTag.Get();
            }
        }
        public Speed TravelSpeed 
        {
            get 
            {
                return m_TraveSpeed;
            }
        }
        public long FirstNode
        {
            get
            {
                return this.NodeIds[0];
            }
        }
        public long LastNode
        {
            get
            {
                return this.NodeIds[NodeIds.Count() - 1];
            }
        }
        public long[] NodeIds
        {
            get
            {
                return m_NodeIds;
            }
            set
            {
                if (m_NodeIds == value)
                    return;
                m_NodeIds = value;
                Modify();
            }
        }

        public IIndex<string, PersistentString> Tags
        {
            get
            {
                return m_Tags;
            }
        }

        public override bool Equals(object obj)
        {
            Way other = obj as Way;
            if (other != null)
                return this.WayId == other.WayId;
            return false;
        }
        public override int GetHashCode()
        {
            return m_WayId.GetHashCode();
        }
    }
}
