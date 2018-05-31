using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmExplorer.Routing.Internal
{
    [Flags]
    internal enum RestrictionType
    {
        Tollway = 1,
        Tunnel = 2,
        Bridge = 4,
        Roundabout = 8,
        Barricade = 16,
        PrivateAccess = 32,
        PermissiveAccess = 64,
        DeliveryAccess = 128,
        TruckAccess = 256,
        HazmatAccess = 512,
        EmergencyAccess = 1024,
        MotorVehicleAccess = 2048,
        BicycleAccess = 4096,
        MopedAccess = 8192
    }
}
