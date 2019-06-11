using System;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// The type of vehicle to route.
    /// </summary>
    [Flags]
    public enum VehicleType
    {
        /// <summary>
        /// Passenger car
        /// </summary>
        Auto = 1,
        /// <summary>
        /// Freight/delivery truck with GVWR less than 26,000 lbs.
        /// </summary>
        Truck_1 = 2,
        /// <summary>
        /// Freight vehicle (typically semi/tractor-trailer) with GVWR greater than 26,000 lbs.
        /// </summary>
        Truck_2 = 4,
        /// <summary>
        /// Any vehicle carrying hazardous material. Will avoid tunnels
        /// and (if tagged) ares where hazmat transport is specifically
        /// prohibited.
        /// </summary>
        Hazmat = 8,
        /// <summary>
        /// Scooter/moped under 50cc. Will avoid controlled-access
        /// freeways and other roads where slow traffic is prohibited.
        /// </summary>
        Scooter_50cc = 16
    }
}
