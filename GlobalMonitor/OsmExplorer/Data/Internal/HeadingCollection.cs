using Volante;

namespace OsmExplorer.Data.Internal
{
    internal class HeadingCollection : Persistent
    {
        private short m_Heading_F_Inbound;
        private short m_Heading_F_Outbound;
        private short m_Heading_L_Inbound;
        private short m_Heading_L_Outbound;

        public HeadingCollection() { }
        public HeadingCollection(HeadingCollection other) 
        {
            m_Heading_F_Inbound = other.m_Heading_F_Inbound;
            m_Heading_F_Outbound = other.m_Heading_F_Outbound;
            m_Heading_L_Inbound = other.m_Heading_L_Inbound;
            m_Heading_L_Outbound = other.m_Heading_L_Outbound;
        }
        public HeadingCollection(short[] Headings)
        {
            m_Heading_F_Inbound = Headings[0];
            m_Heading_F_Outbound = Headings[1];
            m_Heading_L_Inbound = Headings[2];
            m_Heading_L_Outbound = Headings[3];
        }

        public long Id 
        {
            get 
            {
                return (m_Heading_F_Inbound + 180) 
                    + (m_Heading_F_Outbound + 180) * 1000 
                    + (m_Heading_L_Inbound + 180) * 2 * 1000 
                    + (m_Heading_L_Outbound + 180) * 3 * 1000;
            }
        }
        public short Heading_F_Inbound
        {
            get
            {
                return m_Heading_F_Inbound;
            }
        }
        public short Heading_F_Outbound
        {
            get
            {
                return m_Heading_F_Outbound;
            }
        }
        public short Heading_L_Inbound
        {
            get
            {
                return m_Heading_L_Inbound;
            }
        }
        public short Heading_L_Outbound
        {
            get
            {
                return m_Heading_L_Outbound;
            }
        }
    }
}
