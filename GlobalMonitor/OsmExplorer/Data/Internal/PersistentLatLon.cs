using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volante;
using OsmExplorer.Spatial;

namespace OsmExplorer.Data.Internal
{
    internal class PersistentLatLon : Persistent
    {
        private float m_Lat;
        private float m_Lon;
        private long m_Id;

        public PersistentLatLon() { }
        public PersistentLatLon(float lat, float lon, long id) 
        {
            m_Lat = lat;
            m_Lon = lon;
            m_Id = id;
        }

        public long Id
        {
            get
            {
                return m_Id;
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

        public static implicit operator LatLon(PersistentLatLon pLL) 
        {
            return new LatLon(pLL.Lat, pLL.Lon, pLL.Id);
        }
    }
}
