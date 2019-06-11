using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OsmExplorer.Units
{
    /// <summary>
    /// Structure representing a speed value.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{m_SpeedMPS} meters per second")]
    public struct Speed : ISerializable, IComparable<Speed>, IComparable, IEquatable<Speed>
    {
        #region Private
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly double m_SpeedMPS;
        private Speed(double speedMPS) 
        {
            m_SpeedMPS = speedMPS;
        }
        #endregion

        /// <summary>
        /// 1 foot per second expressed in meters per second.
        /// </summary>
        public const double MPS_per_FPS = 0.3048F;
        /// <summary>
        /// 1 kilometer per hour expressed in meters per second.
        /// </summary>
        public const double MPS_per_KPH = 0.277777778F;
        /// <summary>
        /// 1 mile per hour expressed in meters per second.
        /// </summary>
        public const double MPS_per_MPH = 0.44704F;
        /// <summary>
        /// 1 knot expressed in meters per second.
        /// </summary>
        public const double MPS_per_Knot = 0.514444444F;

        /// <summary>
        /// Gets a zero-values Speed.
        /// </summary>
        public static Speed ZeroSpeed 
        {
            get 
            {
                return new Speed(0);
            }
        }
        /// <summary>
        /// Creates a new Speed for a given value and SpeedUnits.
        /// </summary>
        /// <param name="value">Speed value.</param>
        /// <param name="units">Type of speed units.</param>
        public Speed(double value, SpeedUnits units) 
        {
            switch (units) 
            {
                case SpeedUnits.FPS:
                    m_SpeedMPS = value * MPS_per_FPS;
                    return;
                case SpeedUnits.Knots:
                    m_SpeedMPS = value * MPS_per_Knot;
                    return;
                case SpeedUnits.KPH:
                    m_SpeedMPS = value * MPS_per_KPH;
                    return;
                case SpeedUnits.MPH:
                    m_SpeedMPS = value * MPS_per_MPH;
                    return;
                case SpeedUnits.MPS:
                    m_SpeedMPS = value;
                    return;
                default:
                    throw new ArgumentException("Unsupported speed units");
            }
        }
        /// <summary>
        /// Gets a Speed instance from a System.Runtime.Serialization.SerializationInfo object.
        /// </summary>
        /// <param name="info">A System.Runtime.Serialization.SerializationInfo object.</param>
        /// <param name="context">A StreamingContext for the serialization.</param>
        public Speed(SerializationInfo info, StreamingContext context) 
        {
            m_SpeedMPS = info.GetDouble("speed");
        }

        /// <summary>
        /// Returns the Speed instance in terms of the specified units.
        /// </summary>
        /// <param name="units">Specified units.</param>
        /// <returns>Double-precision value of the Speed expressed in the specified units.</returns>
        public double SpeedValue(SpeedUnits units) 
        {
            switch (units)
            {
                case SpeedUnits.FPS:
                    return m_SpeedMPS / MPS_per_FPS;
                case SpeedUnits.Knots:
                    return m_SpeedMPS / MPS_per_Knot;
                case SpeedUnits.KPH:
                    return m_SpeedMPS / MPS_per_KPH;
                case SpeedUnits.MPH:
                    return m_SpeedMPS / MPS_per_MPH;
                case SpeedUnits.MPS:
                    return m_SpeedMPS;
                default:
                    throw new ArgumentException("Unsupported speed units");
            }
        }
        /// <summary>
        /// Returns a Speed instance in feet per second.
        /// </summary>
        public double FeetPerSecond 
        {
            get 
            {
                return m_SpeedMPS / MPS_per_FPS;
            }
        }
        /// <summary>
        /// Returns a Speed instance in meters per second.
        /// </summary>
        public double MetersPerSecond 
        {
            get 
            {
                return m_SpeedMPS;
            }
        }
        /// <summary>
        /// Returns a Speed instance in kilometers per hour.
        /// </summary>
        public double KilometersPerHour 
        {
            get 
            {
                return m_SpeedMPS / MPS_per_KPH;
            }
        }
        /// <summary>
        /// Returns a Speed instance in miles per hour.
        /// </summary>
        public double MilesPerHour 
        {
            get 
            {
                return m_SpeedMPS / MPS_per_MPH;
            }
        }
        /// <summary>
        /// Returns a Speed instance in knots (nautical miles per hour).
        /// </summary>
        public double Knots 
        {
            get 
            {
                return m_SpeedMPS / MPS_per_Knot;
            }
        }

        /// <summary>
        /// Adds to Speed values.
        /// </summary>
        /// <param name="a">First speed value.</param>
        /// <param name="b">Second speed value.</param>
        /// <returns>The sum of a and b.</returns>
        public static Speed operator +(Speed a, Speed b) 
        {
            return new Speed(a.m_SpeedMPS + b.m_SpeedMPS);
        }
        /// <summary>
        /// Returns the difference of two speed values.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>The difference of a and b.</returns>
        /// <remarks>It is possible for this operator to return a negative speed value,
        /// which may be non-sensical depending on the context. Note that a negative speed
        /// value is acceptable when speed is considered analagous to velocity.</remarks>
        public static Speed operator -(Speed a, Speed b)
        {
            return new Speed(a.m_SpeedMPS - b.m_SpeedMPS);
        }
        /// <summary>
        /// Determines if two Speeds are equal in value.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>True if a and b are equal.</returns>
        public static bool operator ==(Speed a, Speed b) 
        {
            return a.m_SpeedMPS == b.m_SpeedMPS;
        }
        /// <summary>
        /// Determines if two speeds are inequal in value.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>True if a and b are inequal.</returns>
        public static bool operator !=(Speed a, Speed b) 
        {
            return !(a == b);
        }
        /// <summary>
        /// Determines if one speed is greater than another.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>True if a is larger than b.</returns>
        public static bool operator >(Speed a, Speed b) 
        {
            return a.m_SpeedMPS > b.m_SpeedMPS;
        }
        /// <summary>
        /// Determines if one Speed is less than another.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>True if a is less than b.</returns>
        public static bool operator <(Speed a, Speed b) 
        {
            return a.m_SpeedMPS < b.m_SpeedMPS;
        }
        /// <summary>
        /// Determines if one speed is greater than or equal to another.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>True if a is larger than or equal to b.</returns>
        public static bool operator >=(Speed a, Speed b) 
        {
            return a > b || a == b;
        }
        /// <summary>
        /// Determines if one Speed is less than or equal to another.
        /// </summary>
        /// <param name="a">First Speed.</param>
        /// <param name="b">Second Speed.</param>
        /// <returns>True if a is less than or equal to b.</returns>
        public static bool operator <=(Speed a, Speed b) 
        {
            return a < b || a == b;
        }
        
        /// <summary>
        /// Populates a System.Runtime.Serialization.SerializationInfo object 
        /// with data needed to serialize a Speed instance.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) 
        /// for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("speed", this.m_SpeedMPS);
        }
        /// <summary>
        /// Compares this Speed instance to the specified object.
        /// </summary>
        /// <param name="obj">A specified object.</param>
        /// <returns>1 if this Speed instance is greater than the specified object; 
        /// -1 if this Speed instance is less than the specified object;
        ///  0 if this Speed instance equals the specified object.</returns>
        /// <exception cref=" InvalidCastException">Object is not a Speed.</exception>
        public int CompareTo(object obj)
        {
            try
            {
                Speed other = (Speed)obj;
                return this.CompareTo(other);
            }
            catch (InvalidCastException) 
            {
                throw;
            }
        }
        /// <summary>
        /// Compares this Speed instance to another.
        /// </summary>
        /// <param name="other">Another Speed instance.</param>
        /// <returns>1 if this Speed instance is greater than other; 
        /// -1 if this Speed instance is less than other;
        ///  0 if this Speed instance equals other.</returns>
        public int CompareTo(Speed other)
        {
            if (this.m_SpeedMPS > other.m_SpeedMPS)
                return 1;
            if (this.m_SpeedMPS < other.m_SpeedMPS)
                return -1;
            return 0;
        }
        /// <summary>
        /// Determines if this Speed instance equals another.
        /// </summary>
        /// <param name="other">Another Speed instance.</param>
        /// <returns>True if this instance and other are equal; false otherwise.</returns>
        public bool Equals(Speed other)
        {
            return this.m_SpeedMPS == other.m_SpeedMPS;
        }
        /// <summary>
        /// Determines if this Speed instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">A System.Object.</param>
        /// <returns>True if the specified object is a Speed and is equal to this instance; 
        /// otherwise false.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                Speed other = (Speed)obj;
                return this.Equals(other);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        /// <summary>
        /// Returns a hash code for this Speed instance.
        /// </summary>
        /// <returns>A 32-bit signed integer.</returns>
        public override int GetHashCode()
        {
            return m_SpeedMPS.GetHashCode();
        }
    }
}
