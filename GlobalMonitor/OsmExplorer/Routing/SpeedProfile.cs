using System.Collections.Generic;
using System.Linq;
using OsmExplorer.Data;
using OsmExplorer.Units;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Class representing a set of speed profiles for each RoadCategory enumeration value
    /// of a RoadLink.
    /// </summary>
    public class SpeedProfile
    {
        #region Private
        private const double MPStoKPH = 0.277777778;
        private readonly RoadCategory[] m_CategoryArray;
        private readonly double[] m_DefaultSpeeds;
        private double[] m_Speeds;
        #endregion
        #region Internal
        internal double[] Speeds 
        {
            get 
            {
                return m_Speeds;
            }
        }
        internal double MaxSpeed 
        {
            get 
            {
                return Speeds.Max();
            }
        }
        #endregion

        /// <summary>
        /// Creates a new SpeedProfile.
        /// </summary>
        public SpeedProfile() 
        {
            m_CategoryArray = new RoadCategory[12] 
            {
                RoadCategory.motorway,
                RoadCategory.motorway_link,
                RoadCategory.trunk,
                RoadCategory.trunk_link,
                RoadCategory.primary,
                RoadCategory.primary_link,
                RoadCategory.secondary,
                RoadCategory.secondary_link,
                RoadCategory.tertiary,
                RoadCategory.tertiary_link,
                RoadCategory.residential,
                RoadCategory.service
            };
            m_DefaultSpeeds = new double[12] 
            {
                120 * MPStoKPH,
                105 * MPStoKPH,
                95 * MPStoKPH,
                85 * MPStoKPH,
                75 * MPStoKPH,
                65 * MPStoKPH,
                55 * MPStoKPH,
                50 * MPStoKPH,
                45 * MPStoKPH,
                40 * MPStoKPH,
                35 * MPStoKPH,
                30 * MPStoKPH
            };
            m_Speeds = m_DefaultSpeeds;
        }
        /// <summary>
        /// Creates a new SpeedProfile from a Dictionary of RoadCategory/Speed key-value pairs.
        /// </summary>
        /// <param name="speeds"></param>
        public SpeedProfile(Dictionary<RoadCategory, Speed> speeds) 
        {
            m_CategoryArray = new RoadCategory[12] 
            {
                RoadCategory.motorway,
                RoadCategory.motorway_link,
                RoadCategory.trunk,
                RoadCategory.trunk_link,
                RoadCategory.primary,
                RoadCategory.primary_link,
                RoadCategory.secondary,
                RoadCategory.secondary_link,
                RoadCategory.tertiary,
                RoadCategory.tertiary_link,
                RoadCategory.residential,
                RoadCategory.service
            };
            m_DefaultSpeeds = new double[12] 
            {
                120 / MPStoKPH,
                105 / MPStoKPH,
                95 / MPStoKPH,
                85 / MPStoKPH,
                75 / MPStoKPH,
                65 / MPStoKPH,
                55 / MPStoKPH,
                50 / MPStoKPH,
                45 / MPStoKPH,
                40 / MPStoKPH,
                35 / MPStoKPH,
                30 / MPStoKPH
            };
            m_Speeds = m_DefaultSpeeds;
            foreach (var category in m_CategoryArray) 
            {
                Speed speed;
                if (!speeds.TryGetValue(category, out speed))
                    m_Speeds[(uint)category] = m_DefaultSpeeds[(uint)category];
            }
        }

        /// <summary>
        /// Sets a speed for a given RoadCategory
        /// </summary>
        /// <param name="category">RoadCategory enumeration to set a Speed value for.</param>
        /// <param name="speed">A Speed value.</param>
        public void SetSpeed(RoadCategory category, Speed speed) 
        {
            m_Speeds[(uint)category] = speed.SpeedValue(SpeedUnits.MPS);
        }
    }
}
