using OsmExplorer.Spatial;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Structure representing a set of specifications for vehicle size and weight.
    /// Vehicle dimensions can be tested with VehicleDimensions.WithinSpecifications 
    /// against maximum vehicle dimensions associated with RoadLinks to determine if
    /// a vehicle can be routed on a given section of road.
    /// </summary>
    public struct VehicleDimensions : IDimensions
    {
        #region Private
        private ushort m_Height_cm;
        private ushort m_Length_cm;
        private ushort m_Width_cm;
        private ushort m_Weight_kg;
        #endregion

        /// <summary>
        /// Creates a new VehicleDimensions for the specified vehicle parameters.
        /// </summary>
        /// <param name="height_cm">Height in centimeters.</param>
        /// <param name="length_cm">Lenght in centimeters.</param>
        /// <param name="width_cm">Width in centimeters.</param>
        /// <param name="weight_kg">Weight in kilograms.</param>
        public VehicleDimensions(ushort height_cm, ushort length_cm, ushort width_cm, ushort weight_kg)
        {
            m_Height_cm = height_cm;
            m_Length_cm = length_cm;
            m_Width_cm = width_cm;
            m_Weight_kg = weight_kg;
        }

        /// <summary>
        /// Gets or sets the vehicle height in centimeters.
        /// </summary>
        public ushort Height_cm
        {
            get
            {
                return m_Height_cm;
            }
            set
            {
                m_Height_cm = value;
            }
        }
        /// <summary>
        /// Gets or sets the vehicle length in centimeters.
        /// </summary>
        public ushort Length_cm
        {
            get
            {
                return m_Length_cm;
            }
            set
            {
                m_Length_cm = value;
            }
        }
        /// <summary>
        /// Gets or sets the vehicle weight in centimeters.
        /// </summary>
        public ushort Width_cm
        {
            get
            {
                return m_Width_cm;
            }
            set
            {
                m_Width_cm = value;
            }
        }
        /// <summary>
        /// Gets or sets the vehicle weight in kilograms.
        /// </summary>
        public ushort Weight_kg
        {
            get
            {
                return m_Weight_kg;
            }
            set
            {
                m_Weight_kg = value;
            }
        }

        /// <summary>
        /// Determines if this set of vehicle dimensions is within another set of IDimensions,
        /// such as the maximum dimensions of a RoadLink
        /// </summary>
        /// <param name="MaxDimensions">Another set of IDimensions</param>
        /// <returns>True if all the specifications in this IDimensions instance are
        /// within (less than) those of the specified IDimensions.</returns>
        public bool WithinSpecifications(IDimensions MaxDimensions)
        {
            if (this.Height_cm > MaxDimensions.Height_cm)
                return false;
            if (this.Length_cm > MaxDimensions.Length_cm)
                return false;
            if (this.Width_cm > MaxDimensions.Width_cm)
                return false;
            if (this.Weight_kg > MaxDimensions.Weight_kg)
                return false;
            return true;
        }
    }
}
