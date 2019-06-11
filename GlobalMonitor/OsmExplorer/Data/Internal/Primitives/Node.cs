using System.Collections.Generic;
using Volante;
using OsmExplorer.Spatial;

namespace OsmExplorer.Data.Internal.Primitives
{
    internal class Node : Persistent
    {
        private long m_NodeId;
        private float m_Lat;
        private float m_Lon;
        private IIndex<string, PersistentString> m_Tags;
        private IIndex<string, PersistentString> m_Relations;

        public Node() { }
        public Node(long nodeId, IDatabase Db)
        {
            m_NodeId = nodeId;
            m_Tags = Db.CreateIndex<string, PersistentString>(IndexType.Unique);
            m_Relations = Db.CreateIndex<string, PersistentString>(IndexType.Unique);
        }
        public Node(long nodeId,
            float lat,
            float lon,
            IEnumerable<KeyValuePair<string, string>> tags,
            IDatabase Db)
        {
            m_NodeId = nodeId;
            m_Lat = lat;
            m_Lon = lon;
            m_Tags = Db.CreateIndex<string, PersistentString>(IndexType.Unique);
            m_Relations = Db.CreateThickIndex<string, PersistentString>();
            foreach (var kvp in tags)
            {
                m_Tags.Put(kvp.Key, new PersistentString(kvp.Value));
            }
        }

        public long NodeId
        {
            get
            {
                return m_NodeId;
            }
        }
        public float Lat
        {
            get
            {
                return m_Lat;
            }
        }
        public float Lon
        {
            get
            {
                return m_Lon;
            }
        }
        public IIndex<string, PersistentString> Tags
        {
            get
            {
                return m_Tags;
            }
            set
            {
                if (m_Tags == value)
                    return;
                m_Tags = value;
                Modify();
            }
        }
        public IIndex<string, PersistentString> Relations
        {
            get
            {
                return m_Relations;
            }
            set
            {
                if (m_Relations == value)
                    return;
                m_Relations = value;
                Modify();
            }
        }

        public static implicit operator PersistentLatLon(Node node) 
        {
            return new PersistentLatLon(node.Lat, node.Lon, node.NodeId);
        }

        public override bool Equals(object obj)
        {
            Node other = obj as Node;
            if (other == null)
                return false;
            return this.NodeId == other.NodeId;
        }
        public override int GetHashCode()
        {
            return m_NodeId.GetHashCode();
        }
    }
}
