using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volante;
using OsmExplorer.Routing;

namespace OsmExplorer.Spatial
{
    /// <summary>
    /// Class representing a set of maximum vehicle dimensions (weight and size)
    /// that are permitted on a given RoadLink. RoadLinks with no restrictions on a given dimension
    /// will have values set to ushort.MaxValue.
    /// </summary>
    public sealed class RoadDimensions: Persistent, IDimensions
    {
        #region Private
        private ushort m_Height_cm;
        private ushort m_Length_cm;
        private ushort m_Width_cm;
        private ushort m_Weight_kg;
        #endregion
        #region Internal
        internal ulong Key 
        {
            get 
            {
                return (ulong)(m_Height_cm + m_Length_cm * ushort.MaxValue + m_Width_cm * 2 * ushort.MaxValue + m_Weight_kg * 3 * ushort.MaxValue);
            }
        }
        internal RoadDimensions() { }
        internal RoadDimensions(ushort height, ushort length, ushort width, ushort weight) 
        {
            m_Height_cm = height;
            m_Length_cm = length;
            m_Width_cm = width;
            m_Weight_kg = weight;
        }
        #endregion

        /// <summary>
        /// Gets the maximum height in centimeters.
        /// </summary>
        public ushort Height_cm
        {
            get
            {
                return m_Height_cm;
            }
        }
        /// <summary>
        /// Gets the maximum permitted vehicle length in centimeters.
        /// </summary>
        public ushort Length_cm
        {
            get
            {
                return m_Length_cm;
            }
        }
        /// <summary>
        /// Gets the maximum vehicle width in centimeters.
        /// </summary>
        public ushort Width_cm
        {
            get
            {
                return m_Width_cm;
            }
        }
        /// <summary>
        /// Gets the maximum vehicle weight in kilograms.
        /// </summary>
        public ushort Weight_kg
        {
            get
            {
                return m_Weight_kg;
            }
        }
    }
}
