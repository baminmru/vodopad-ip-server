using System;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace OsmExplorer.Units
{
    /// <summary>
    /// Structure representing a unit of length.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Meters = {m_DistanceMeters}")]
    public struct Length : ISerializable, IComparable<Length>, IComparable, IEquatable<Length>
    {
        #region Private
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly double m_DistanceMeters;
        private Length(double distanceMeters)
        {
            m_DistanceMeters = distanceMeters;
        }
        #endregion

        /// <summary>
        /// Number of meters per foot. This field is constant.
        /// </summary>
        public const float MetersPerFoot = 0.3048F;
        /// <summary>
        /// Number of meters per kilometer. This field is constant.
        /// </summary>
        public const float MetersPerKilometer = 1000F;
        /// <summary>
        /// Number of meters per mile. This field is constant.
        /// </summary>
        public const float MetersPerMile = 1609.344F;
        /// <summary>
        /// Number of meters per nautical mile. This field is constant.
        /// </summary>
        public const float MetersPerNauticalMile = 1852F;
        /// <summary>
        /// Number of feet per meter. This field is constant.
        /// </summary>
        public const float FeetPerMeter = 3.2808399F;
        /// <summary>
        /// Number of feet per kilometer. This field is constant.
        /// </summary>
        public const float FeetPerKilometer = 3280.8399F;
        /// <summary>
        /// Number of feet per mile. This field is constant.
        /// </summary>
        public const float FeetPerMile = 5280F;
        /// <summary>
        /// Number of feet per nautical mile. This field is constant.
        /// </summary>
        public const float FeetPerNauticalMile = 6076.11549F;

        /// <summary>
        /// Returns a zero-equivalent Length.
        /// </summary>
        public static Length ZeroLength 
        {
            get 
            {
                return new Length(0);
            }
        }
        /// <summary>
        /// Creates a new Length for the specified distance and LengthUnits.
        /// </summary>
        /// <param name="distance">Distance interval.</param>
        /// <param name="units">Distance units.</param>
        public Length(double distance, LengthUnits units)
        {
            switch (units)
            {
                case LengthUnits.Feet:
                    m_DistanceMeters = distance * MetersPerFoot;
                    return;
                case LengthUnits.Meters:
                    m_DistanceMeters = distance;
                    return;
                case LengthUnits.Kilometers:
                    m_DistanceMeters = distance * MetersPerKilometer;
                    return;
                case LengthUnits.Miles:
                    m_DistanceMeters = distance * MetersPerMile;
                    return;
                case LengthUnits.NauticalMiles:
                    m_DistanceMeters = distance * MetersPerNauticalMile;
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported LengthUnit.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Length(SerializationInfo info, StreamingContext context) 
        {
            m_DistanceMeters = info.GetDouble("distance");
        }

        /// <summary>
        /// Returns the value of a Length instance in the specified units.
        /// </summary>
        /// <param name="units">Specified units.</param>
        /// <returns>A double-precision value representing the Length in the specified units.</returns>
        public double LengthValue(LengthUnits units) 
        {
            switch (units) 
            {
                case LengthUnits.Feet:
                    return m_DistanceMeters / MetersPerFoot;
                case LengthUnits.Meters:
                    return m_DistanceMeters;
                case LengthUnits.Kilometers:
                    return m_DistanceMeters / MetersPerKilometer;
                case LengthUnits.Miles:
                    return m_DistanceMeters / MetersPerMile;
                case LengthUnits.NauticalMiles:
                    return m_DistanceMeters / MetersPerNauticalMile;
                default:
                    throw new ArgumentException("Unsupported LengthUnits");
            }
        }
        /// <summary>
        /// Gets the Length in feet.
        /// </summary>
        public double Feet
        {
            get
            {
                return m_DistanceMeters * FeetPerMeter;
            }
        }
        /// <summary>
        /// Gets the Length in meters.
        /// </summary>
        public double Meters
        {
            get
            {
                return m_DistanceMeters;
            }
        }
        /// <summary>
        /// Gets the Length in kilometers.
        /// </summary>
        public double Kilometers
        {
            get
            {
                return m_DistanceMeters / MetersPerKilometer;
            }
        }
        /// <summary>
        /// Gets the Length in miles.
        /// </summary>
        public double Miles
        {
            get
            {
                return m_DistanceMeters / MetersPerMile;
            }
        }
        /// <summary>
        /// Gets the Length in nautical miles.
        /// </summary>
        public double NauticalMiles
        {
            get
            {
                return m_DistanceMeters / 1852;
            }
        }

        /// <summary>
        /// Adds two Lengths.
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>Sum of the Lengths.</returns>
        public static Length operator +(Length d1, Length d2)
        {
            return new Length(d1.m_DistanceMeters + d2.m_DistanceMeters);
        }
        /// <summary>
        /// Subtracts one Length from another.
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>The difference of the Lengths</returns>
        public static Length operator -(Length d1, Length d2)
        {
            return new Length(d1.m_DistanceMeters - d2.m_DistanceMeters);
        }
        /// <summary>
        /// Gets the ratio of two Lengths
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>Ratio of the Lengths.</returns>
        public static double operator /(Length d1, Length d2)
        {
            return d1.m_DistanceMeters / d2.m_DistanceMeters;
        }
        /// <summary>
        /// Determines if two Lengths are equal in value;
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>True if x and y are equal in length</returns>
        public static bool operator ==(Length d1, Length d2)
        {
            return d1.m_DistanceMeters == d2.m_DistanceMeters;
        }
        /// <summary>
        /// Determines if two Lengths have different values;
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>True if x and y are of different lengths</returns>
        public static bool operator !=(Length d1, Length d2)
        {
            return !(d1 == d2);
        }
        /// <summary>
        /// Tests whether one Length is greater than another
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>True if d1 is greater than d2</returns>
        public static bool operator >(Length d1, Length d2)
        {
            return d1.m_DistanceMeters > d2.m_DistanceMeters;
        }
        /// <summary>
        /// Tests whether one Length is shorter than another
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>True if d1 is less than d2</returns>
        public static bool operator <(Length d1, Length d2)
        {
            return d1.m_DistanceMeters < d2.m_DistanceMeters;
        }
        /// <summary>
        /// Tests whether one Length is greater than or equal to another
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>True if d1 is greater than or equal to d2</returns>
        public static bool operator >=(Length d1, Length d2)
        {
            return d1.m_DistanceMeters >= d2.m_DistanceMeters;
        }
        /// <summary>
        /// Tests whether one Length is less than or equal  to another
        /// </summary>
        /// <param name="d1">First Length.</param>
        /// <param name="d2">Second Length.</param>
        /// <returns>True if d1 is less than or equal to d2</returns>
        public static bool operator <=(Length d1, Length d2)
        {
            return d1.m_DistanceMeters <= d2.m_DistanceMeters;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this Length instance
        /// </summary>
        /// <param name="obj">System.Object</param>
        /// <returns>True if obj is a Length and equal in value, otherwise false</returns>
        public override bool Equals(object obj)
        {
            try
            {
                Length other = (Length)obj;
                return this.Equals(other);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        /// <summary>
        /// Returns a hashcode for the Length instance.
        /// </summary>
        /// <returns>A hashcode for this instance</returns>
        public override int GetHashCode()
        {
            return m_DistanceMeters.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            try
            {
                Length other = (Length)obj;
                if (this.m_DistanceMeters > other.m_DistanceMeters)
                    return 1;
                if (this.m_DistanceMeters < other.m_DistanceMeters)
                    return -1;
                return 0;
            }
            catch (InvalidCastException) 
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Length other)
        {
            if (this.m_DistanceMeters > other.m_DistanceMeters)
                return 1;
            if (this.m_DistanceMeters < other.m_DistanceMeters)
                return -1;
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Length other)
        {
            return this.m_DistanceMeters == other.m_DistanceMeters;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("distance", m_DistanceMeters);
        }
    }
}
