using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using OsmExplorer.Units;

namespace OsmExplorer.Spatial
{
    /// <summary>
    /// Structure that represents a latitude/longitude pair.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Lat = {Lat}, Lon = {Lon}")]
    public struct LatLon : ISerializable, IEquatable<LatLon>
    {
        #region Private
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly double m_Lat;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly double m_Lon;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly long m_Id;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const double m_earth_radius_meters = 6371000;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const double m_RadiansPerDegree = 0.0174532925;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const double m_RadiansPerHalfDegree = 0.00872664626;
        #endregion
        #region Internal
        internal LatLon(float lat, float lon, long Id)
        {
            m_Lat = lat;
            m_Lon = lon;
            m_Id = Id;
        }
        #endregion

        /// <summary>
        /// Constructs a LatLon from a latitude and longitude value
        /// </summary>
        /// <param name="lat">Latitude value.</param>
        /// <param name="lon">Longitude value.</param>
        public LatLon(double lat, double lon)
        {
            m_Lat = lat;
            m_Lon = lon;
            m_Id = 0;
            if (m_Lat > 90D)
                m_Lat = 90D;
            if (m_Lat < -90D)
                m_Lat = -90D;
            if (m_Lon > 180D)
                m_Lon = 180D;
            if (m_Lon < -180D)
                m_Lon = -180D;
        }
        /// <summary>
        /// Constructs a LatLon from a comma-separated string 
        /// representation of a coordinate e.g. "Lat,Lon"
        /// </summary>
        /// <param name="latlon">String representation of coordinate</param>
        /// <exception cref=" System.FormatException">LatLon string format incorrect.</exception>
        public LatLon(string latlon)
        {
            string[] LatLonString = latlon.Split(',');
            try
            {
                try
                {
                    m_Lat = Convert.ToDouble(LatLonString[0]);
                }
                catch {
                    m_Lat = Convert.ToDouble(LatLonString[0].Replace('.',','));
                }
                try{
                     m_Lon = Convert.ToDouble(LatLonString[1]);
                }catch{
                     m_Lon = Convert.ToDouble(LatLonString[1].Replace('.',','));
                }
               
                m_Id = 0;
            }
            catch (FormatException ex1)
            {
                throw new FormatException("Incorrect LatLon string format.", ex1);
            }
            catch (IndexOutOfRangeException) 
            {
                throw new FormatException("Incorrect LatLon string format.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public LatLon(SerializationInfo info, StreamingContext context)
        {
            m_Id = info.GetInt64("Id");
            m_Lat = info.GetDouble("Lat");
            m_Lon = info.GetDouble("Lon");
        }

        /// <summary>
        /// A unique identifier of a LatLon derived from the
        /// OpenStreetMap Node Id. 
        /// </summary>
        public long Id
        {
            get
            {
                return m_Id;
            }
        }
        /// <summary>
        /// The Latitude (y-coordinate) of the LatLon
        /// </summary>
        public double Lat
        {
            get
            {
                return m_Lat;
            }
        }
        /// <summary>
        /// The Longitude (x-coordinate) of the LatLon
        /// </summary>
        public double Lon
        {
            get
            {
                return m_Lon;
            }
        }
        /// <summary>
        /// Returns true if a LatLon has been initialized and its latitude
        /// and longitude fields set.
        /// </summary>
        public bool IsValid 
        {
            get 
            {
                return m_Lat != 0 || m_Lon != 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(LatLon a, LatLon b) 
        {
            if (a.Id == 0)
                return a.Lat == b.Lat && a.Lon == b.Lon;
            if (b.Id == 0)
                return a.Lat == b.Lat && a.Lon == b.Lon;
            return a.Id == b.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(LatLon a, LatLon b) 
        {
            return !(a == b);
        }

        /// <summary>
        /// Calculates the geodesic (world line) distance between 
        /// this LatLon and another.
        /// </summary>
        /// <param name="other">Another LatLon coordinate.</param>
        /// <returns>DistanceSpan representing the distance interval.</returns>
        public Length DistanceTo(LatLon other)
        {
            var dLat = (this.Lat - other.Lat) * m_RadiansPerDegree;
            var dLon = (this.Lon - other.Lon) * m_RadiansPerDegree;
            var lat1 = this.Lat * m_RadiansPerDegree;
            var lat2 = this.Lat * m_RadiansPerDegree;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = m_earth_radius_meters * c;

            return new Length(d, LengthUnits.Meters);
        }
        /// <summary>
        /// Calulates the Pythagorean (flat-earth) distance to another LatLon. See remarks.
        /// </summary>
        /// <param name="other">Another LatLon coordinate.</param>
        /// <returns>DistanceSpan representing the distance interval.</returns>
        /// <remarks>The Pythagorean distance assumes the world is flat. For short-
        /// distance calculations this can be an acceptable approximation and may be
        /// preferred for multiple repeat calculations (slightly less than half as expensive
        /// as DistanceTo method).</remarks>
        public Length PDistanceTo(LatLon other)
        {
            var x = (other.Lon - this.Lon) * m_RadiansPerDegree * Math.Cos((this.Lat + other.Lat) * m_RadiansPerHalfDegree);
            var y = (other.Lat - this.Lat) * m_RadiansPerDegree;
            var d = Math.Sqrt(x * x + y * y) * m_earth_radius_meters;

            return new Length(d, LengthUnits.Meters);
        }
        /// <summary>
        /// Converts the LatLon to a string representation.
        /// </summary>
        /// <returns>String representation of the LatLon</returns>
        public override string ToString()
        {
            decimal lat = (decimal)this.Lat;
            decimal lon = (decimal)this.Lon;
            return Math.Round(lat, 6).ToString() + "," + Math.Round(lon, 6).ToString();
        }

        /// <summary>
        /// Returns the hash code for this LatLon instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this LatLon instance.</returns>
        public override int GetHashCode()
        {
            return m_Id.GetHashCode();
        }
        /// <summary>
        /// Determines if this LatLon is equal to the specified object
        /// </summary>
        /// <param name="obj">The specified object</param>
        /// <returns>True when the object is a LatLon and equal to this instance.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                LatLon other = (LatLon)obj;
                return this.Equals(other);
            }
            catch (InvalidCastException) 
            {
                return false;
            }
        }
        /// <summary>
        /// Compares this LatLon to another.
        /// </summary>
        /// <param name="other">Another LatLon.</param>
        /// <returns>True if the latitude/longitude coordinates in this instance
        /// equals those of other.</returns>
        public bool Equals(LatLon other)
        {
            if (this.Id == 0 || other.Id == 0)
                return this.Lat == other.Lat && this.Lon == other.Lon;
            return this.Id == other.Id;
        }
        /// <summary>
        /// Populates a System.Runtime.Serialization.SerializationInfo with the data
        /// needed to serialize a LatLon object
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to 
        /// populate with data</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) 
        /// for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.m_Id);
            info.AddValue("Lat", this.m_Lat);
            info.AddValue("Lon", this.m_Lon);
        }
    }
}
