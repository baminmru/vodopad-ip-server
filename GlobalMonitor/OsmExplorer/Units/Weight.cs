using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OsmExplorer.Units
{
    /// <summary>
    /// Structure representing an amount of weight.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{m_Kilograms} kilograms")]
    public struct Weight : ISerializable, IComparable<Weight>, IComparable, IEquatable<Weight>
    {
        #region Private
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly float m_Kilograms;
        private Weight(float weightKilograms) 
        {
            m_Kilograms = weightKilograms;
        }
        #endregion

        /// <summary>
        /// Amount of pounds per kilogram. This field is constant.
        /// </summary>
        public const float PoundsPerKilogram = 2.2046226F;
        /// <summary>
        /// Amount of pounds per short ton. This field is constant.
        /// </summary>
        public const float PoundsPerShortTon = 2000F;
        /// <summary>
        /// Amount of pounts per metric ton (tonne). This field is constant.
        /// </summary>
        public const float PoundsPerMetricTon = 2204.62262F;
        /// <summary>
        /// Amount of kilograms per pound. This field is constant.
        /// </summary>
        public const float KilogramsPerPound = 0.4535924F;
        /// <summary>
        /// Amount of kilograms per short ton. This field is constant.
        /// </summary>
        public const float KilogramsPerShortTon = 907.18474F;
        /// <summary>
        /// Amount of kilograms per metric ton (tonne). This field is constant.
        /// </summary>
        public const float KilogramsPerMetricTon = 1000F;

        /// <summary>
        /// Represents the maximum weight value that can be assigned.
        /// </summary>
        public static Weight MaxWeight 
        {
            get 
            {
                return new Weight(float.MaxValue, WeightUnits.MetricTons);
            }
        }
        /// <summary>
        /// Represents the minimum (zero) value of a Weight instance.
        /// </summary>
        public static Weight ZeroWeight 
        {
            get 
            {
                return new Weight(0);
            }
        }

        /// <summary>
        /// Creates a new Weight object for the specified amount and WeightUnits.
        /// </summary>
        /// <param name="weight">Weight quantity.</param>
        /// <param name="units">Units of weight measurement</param>
        /// <exception cref=" System.ArgumentException"> Weight cannot be a negative value.</exception>
        public Weight(float weight, WeightUnits units) 
        {
            if (weight >= 0)
            {
                switch (units)
                {
                    case WeightUnits.Pounds:
                        m_Kilograms = weight * KilogramsPerPound;
                        return;
                    case WeightUnits.Kilograms:
                        m_Kilograms = weight;
                        return;
                    case WeightUnits.ShortTons:
                        m_Kilograms = weight * KilogramsPerShortTon;
                        return;
                    case WeightUnits.MetricTons:
                        m_Kilograms = weight * KilogramsPerMetricTon;
                        return;
                    default:
                        throw new ArgumentException("Unsupported WeightUnits.");
                }
            }
            throw new ArgumentException("Weight cannot be negative.");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Weight(SerializationInfo info, StreamingContext context) 
        {
            m_Kilograms = info.GetSingle("weight");
        }

        /// <summary>
        /// Returns the Weight instance in units of pounds (lbs).
        /// </summary>
        public double Pounds
        {
            get
            {
                return m_Kilograms * PoundsPerKilogram;
            }
        }
        /// <summary>
        /// Returns the Weight instance in units of kilograms (kgs).
        /// </summary>
        public double Kilograms
        {
            get
            {
                return m_Kilograms;
            }
        }
        /// <summary>
        /// Returns the Weight instance in units of short tons.
        /// </summary>
        public double ShortTons
        {
            get
            {
                return m_Kilograms / KilogramsPerShortTon;
            }
        }
        /// <summary>
        /// Returns the Weight instance in units of metric tons (tonnes).
        /// </summary>
        public double MetricTons
        {
            get
            {
                return m_Kilograms / KilogramsPerMetricTon;
            }
        }

        /// <summary>
        /// Compares two Weights for equality.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>True if the Weights are equal.</returns>
        public static bool operator ==(Weight w1, Weight w2) 
        {
            return w1.m_Kilograms == w2.m_Kilograms;
        }
        /// <summary>
        /// Compares two Weights for inequality
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns></returns>
        public static bool operator !=(Weight w1, Weight w2) 
        {
            return !(w1 == w2);
        }
        /// <summary>
        /// Determines if a Weight is greater than another.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>True if w1 is greater than w2.</returns>
        public static bool operator >(Weight w1, Weight w2) 
        {
            return w1.m_Kilograms > w2.m_Kilograms;
        }
        /// <summary>
        /// Determines if a Weight is less than another.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>True if w1 is less than w2.</returns>
        public static bool operator <(Weight w1, Weight w2) 
        {
            return w1.m_Kilograms < w2.m_Kilograms;
        }
        /// <summary>
        /// Determines if a Weight is greater than or equal to another.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>True if w1 is greater than or equal to w2.</returns>
        public static bool operator >=(Weight w1, Weight w2) 
        {
            return w1.m_Kilograms >= w2.m_Kilograms;
        }
        /// <summary>
        /// Determines if a Weight is less than or equal to another.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>True if w1 is less than or equal to w2.</returns>
        public static bool operator <=(Weight w1, Weight w2) 
        {
            return w1.m_Kilograms <= w2.m_Kilograms;
        }
        /// <summary>
        /// Sums two Weight objects.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>The sum of Weights w1 and w2.</returns>
        public static Weight operator +(Weight w1, Weight w2) 
        {
            return new Weight(w1.m_Kilograms + w2.m_Kilograms);
        }
        /// <summary>
        /// Gets the difference of two Weights.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>Weight w1 subtracted by Weight w2.</returns>
        /// <exception cref=" System.ArgumentException">The difference between w1 and w2 cannot be less than 0.</exception>
        public static Weight operator -(Weight w1, Weight w2) 
        {
            float w = w1.m_Kilograms - w2.m_Kilograms;
            if (w >= 0)
                return new Weight(w);
            throw new ArgumentException("Weight cannot be a negative value");
        }
        /// <summary>
        /// Gets the ratio of two Weights.
        /// </summary>
        /// <param name="w1">First Weight.</param>
        /// <param name="w2">Second Weight.</param>
        /// <returns>The ratio of w1 and w2 (w1/w2).</returns>
        public static float operator /(Weight w1, Weight w2) 
        {
            return w1.m_Kilograms / w2.m_Kilograms;
        }
        
        /// <summary>
        /// Compares this Weight instance to the specified object.
        /// </summary>
        /// <param name="obj">A System.Object</param>
        /// <returns>1 if this instance is greater than the specified object; 
        /// -1 if this instance is less than the specified object; 0 if they are equal.</returns>
        /// <exception cref=" System.ArgumentException">Value is not a OsmExplorer.Units.Weight.</exception>
        public int CompareTo(object obj)
        {
            try
            {
                Weight w = (Weight)obj;
                return this.CompareTo(w);
            }
            catch (InvalidCastException ex) 
            {
                throw new ArgumentException("Object is not a Weight.", ex);
            }
        }
        /// <summary>
        /// Compares this Weight to another.
        /// </summary>
        /// <param name="other">Another Weight.</param>
        /// <returns>1 if this instance is greater than the specified object; 
        /// -1 if this instance is less than the specified object; 0 if they are equal.</returns>
        public int CompareTo(Weight other)
        {
            if (this.m_Kilograms > other.m_Kilograms)
                return 1;
            if (this.m_Kilograms < other.m_Kilograms)
                return -1;
            return 0;
        }
        /// <summary>
        /// Determines if this Weight instance is equal to another.
        /// </summary>
        /// <param name="other">Another Weight.</param>
        /// <returns>True if this Weight is equal in amount to the other.</returns>
        public bool Equals(Weight other)
        {
            return this == other;
        }
        /// <summary>
        /// Returns a value indicating whether this Weight instance is equal to the specified object.
        /// </summary>
        /// <param name="obj"> An object to compare with this Weight instance.</param>
        /// <returns>True if obj is a Weight and equal in amount to this instance; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                Weight w = (Weight)obj;
                return this.Equals(w);
            }
            catch (InvalidCastException) 
            {
                return false;
            }
        }
        /// <summary>
        /// Returns a hash code for this Weight instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code for this Weight instance.</returns>
        public override int GetHashCode()
        {
            return m_Kilograms.GetHashCode();
        }
        /// <summary>
        /// System.Runtime.Serialization.SerializationInfo object 
        /// with data needed to serialize a Weight instance.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) 
        /// for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("weight", m_Kilograms);
        }
    }
}
