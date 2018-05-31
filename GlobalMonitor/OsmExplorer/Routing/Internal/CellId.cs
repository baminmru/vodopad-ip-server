using System;
using Volante;

namespace OsmExplorer.Routing.Internal
{
    internal class CellId : Persistent, IComparable<CellId>
    {
        private ulong m_Id;
        private byte m_Category;

        public CellId() { }
        public CellId(CellId other) 
        {
            m_Id = other.Id;
            m_Category = Convert.ToByte(other.Category);
        }
        public CellId(ulong Id, uint Category)
        {
            m_Id = Id;
            m_Category = Convert.ToByte(Category);
        }

        public ulong Id
        {
            get
            {
                return m_Id;
            }
        }
        public uint Category
        {
            get
            {
                return m_Category;
            }
        }

        public override bool Equals(object obj)
        {
            CellId other = obj as CellId;
            if (other == null)
                return false;
            return this.Id == other.Id;
        }
        public override int GetHashCode()
        {
            return m_Id.GetHashCode();
        }

        public int CompareTo(CellId other)
        {
            if (this.Category > other.Category)
                return 1;
            if (this.Category < other.Category)
                return -1;
            return 0;
        }
    }
}
