using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmExplorer.Units
{
    /// <summary>
    /// Enumeration representing the type of units for a speed value.
    /// </summary>
    public enum SpeedUnits
    {
        /// <summary>
        /// Speed expressed in feet per second.
        /// </summary>
        FPS,
        /// <summary>
        /// Speed expressed in meters per second.
        /// </summary>
        MPS,
        /// <summary>
        /// Speed expressed in kilometers per hour.
        /// </summary>
        KPH,
        /// <summary>
        /// Speed expressed in miles per hour.
        /// </summary>
        MPH,
        /// <summary>
        /// Speed expressed in nautical miles per hour (knots).
        /// </summary>
        Knots
    }
}
