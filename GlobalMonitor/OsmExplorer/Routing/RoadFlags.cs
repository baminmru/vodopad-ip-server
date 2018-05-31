using System;
using System.Diagnostics;
using OsmExplorer.Data.Internal;
using OsmExplorer.Routing.Internal;
using OsmExplorer.Spatial;
using OsmExplorer.Units;
using Volante;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Represents a set of routing constraints for a RoadLink.
    /// </summary>
    public class RoadFlags : Persistent
    {
        #region Private Members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<IPersistent> m_Items;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<PersistentWayId> m_ProhibitedFrom;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IPArray<PersistentWayId> m_ProhibitedTo;
        #endregion
        #region Internal
        internal RoadFlags() { }
        internal RoadFlags(IPArray<IPersistent> items,
            IPArray<PersistentWayId> prohibitedFrom,
            IPArray<PersistentWayId> prohibitedTo)
        {
            m_Items = items;
            m_ProhibitedFrom = prohibitedFrom;
            m_ProhibitedTo = prohibitedTo;
        }
        internal RoadFlags(char[] chars, 
            params RoadFlags[] other) 
        {
            throw new NotImplementedException();

            //m_Tollway = other.Any(x => x.Tollway);
            //m_Tunnel = other.Any(x => x.Tunnel);
            //m_Bridge = other.Any(x => x.Bridge);
            //m_Roundabout = other.Any(x => x.Roundabout);
            //m_PrivateAccess = other.Any(x => x.PrivateAccess);
            //m_PermissiveAccess = other.Any(x => x.PermissiveAccess);
            //m_DeliveryAccess = !other.Any(x => !x.DeliveryAccess);
            //m_TruckAccess = !other.Any(x => !x.TruckAccess);
            //m_HazmatAccess = !other.Any(x => !x.HazmatAccess);
            //m_EmergencyAccess = !other.Any(x => !x.EmergencyAccess);
            //m_MotorVehicleAccess = !other.Any(x => !x.MotorVehicleAccess);
            //m_BicycleAccess = !other.Any(x => !x.BicycleAccess);

            //m_TravelSpeed = (byte)other.Average(x => x.TravelSpeed);
            //m_TravelDistance = (uint)other.Average(x => x.TravelDistanceMeters);
            //m_EstTravelTime = (uint)other.Average(x => x.EstTravelTime);

            //m_StopSign = chars[1];
            //m_TrafficSignal = chars[2];
        }
        #endregion

        /// <summary>
        /// Gets a set of maxmimum dimensions (size and weight) that a given RoadLink
        /// allows. RoadDimensions can be compared directly to a vehicle's VehicleDimensions 
        /// to determine if through travel is possible.
        /// </summary>
        public RoadDimensions MaxDimensions 
        {
            get 
            {
                return m_Items[0] as RoadDimensions;
            }
        }
        /// <summary>
        /// Gets the initial heading starting at the FirstPoint 
        /// and moving towards the LastPoint of the RoadLink.
        /// </summary>
        public short Heading_F_Inbound
        {
            get
            {
                return (m_Items[1] as HeadingCollection).Heading_F_Inbound;
            }
        }
        /// <summary>
        /// Gets the final heading at the FirstPoint when traversing the RoadLink
        /// starting at the LastPoint.
        /// </summary>
        public short Heading_F_Outbound
        {
            get
            {
                return (m_Items[1] as HeadingCollection).Heading_F_Outbound;
            }
        }
        /// <summary>
        /// Gets the initial heading when starting at the LastPoint on
        /// the RoadLink and moving towards the FirstPoint.
        /// </summary>
        public short Heading_L_Inbound
        {
            get
            {
                return (m_Items[1] as HeadingCollection).Heading_L_Inbound;
            }
        }
        /// <summary>
        /// Gets the final heading at the LastPoint when traversing 
        /// the RoadLink starting at the FirstPoint.
        /// </summary>
        public short Heading_L_Outbound
        {
            get
            {
                return (m_Items[1] as HeadingCollection).Heading_L_Outbound;
            }
        }

        /// <summary>
        /// Indicates whether the RoadLink has a barricade of some kind across it,
        /// prohibiting through travel.
        /// </summary>
        public bool Barricade
        {
            get
            {
                return m_Items[2] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates whether bicycles are permitted.
        /// </summary>
        public bool BicycleAccess
        {
            get
            {
                return m_Items[3] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates if this RoadLink is a bridge
        /// </summary>
        public bool Bridge
        {
            get
            {
                return m_Items[4] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Access permission for light commercial vehicles (LCV) 
        /// or goods vehicles of category N1 with a maximum allowed 
        /// mass of up to 3.5 tonnes. In the USA, combined weight 
        /// 26,000 lbs or less. True if delivery access allowed.
        /// </summary>
        public bool DeliveryAccess
        {
            get
            {
                return m_Items[5] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates if emergency vehicles are permitted.
        /// </summary>
        public bool EmergencyAccess
        {
            get
            {
                return m_Items[6] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Access permission for vehicles carrying 
        /// hazardous materials.
        /// </summary>
        public bool HazmatAccess
        {
            get
            {
                return m_Items[7] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates if mopeds (scooter with engine less than 50cc) are
        /// permitted on the RoadLink.
        /// </summary>
        public bool MopedAccess 
        {
            get 
            {
                return m_Items[8] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates whether motor vehicle are permitted.
        /// </summary>
        public bool MotorVehicleAccess
        {
            get
            {
                return m_Items[9] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates whether general access is permissive.
        /// </summary>
        public bool PermissiveAccess
        {
            get
            {
                return m_Items[10] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// True when general traffic (public) is not permitted
        /// to travel on this RoadLink.
        /// </summary>
        public bool PrivateAccess
        {
            get
            {
                return m_Items[11] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates if this RoadLink is a roundabout
        /// </summary>
        public bool Roundabout
        {
            get
            {
                return m_Items[12] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates whether RoadLink is a tollway
        /// </summary>
        public bool Tollway
        {
            get
            {
                return m_Items[13] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Access permission for Heavy Goods Vehicles (HGV) (UK), 
        /// e.g. for goods vehicles of category N2 and N3 (trucks, 
        /// lorries) with a maximum allowed mass over 3.5 tonnes. 
        /// In the USA, combined weight 26,001 lbs or greater
        /// </summary>
        public bool TruckAccess
        {
            get
            {
                return m_Items[14] as PersistentAccessRestriction;
            }
        }
        /// <summary>
        /// Indicates if this RoadLink is a tunnel
        /// </summary>
        public bool Tunnel
        {
            get
            {
                return m_Items[15] as PersistentAccessRestriction;
            }
        }
        
        /// <summary>
        /// Get an array of WayIds that this RoadLink is not reachable from due to 
        /// turn restrictions or other constraints.
        /// </summary>
        public long[] ProhibitedFrom 
        {
            get 
            {
                return Array.ConvertAll(m_ProhibitedFrom.ToArray(), x => x.WayId);
            }
        }
        /// <summary>
        /// Get an array of WayIds that are not reachable from this RoadLink due to 
        /// turn restrictions or other constraints.
        /// </summary>
        public long[] ProhibitedTo 
        {
            get 
            {
                return Array.ConvertAll(m_ProhibitedTo.ToArray(), x => x.WayId);
            }
        }

        /// <summary>
        /// Returns the travel distance along the RoadLink in meters.
        /// </summary>
        public Length TravelDistance
        {
            get
            {
                return m_Items[16] as PersistentLength;
            }
        }
        /// <summary>
        /// Approximate travel speed along the RoadLink
        /// </summary>
        public Speed TravelSpeed
        {
            get
            {
                return m_Items[17] as PersistentSpeed;
            }
        }
        
        /// <summary>
        /// Returns the configuration of stop signs on the RoadLink. See remarks.
        /// </summary>
        /// <remarks >
        /// F = Stop sign on the FirstPoint
        /// L = Stop sign on the LastPoint
        /// B = Stop signs on both the FirstPoint and LastPoint
        /// N = No stop signs on either end
        /// </remarks>
        public char StopSign
        {
            get
            {
                return (m_Items[18] as PersistentTrafficControl).StopSign;
            }
        }
        /// <summary>
        /// Returns the configuration of traffic signals on the RoadLink. See remarks.
        /// </summary>
        /// <remarks >
        /// F = Signal on the FirstPoint
        /// L = Signal on the LastPoint
        /// B = Signals on both the FirstPoint and LastPoint
        /// N = No signals on either end
        /// </remarks>
        public char TrafficSignal
        {
            get
            {
                return (m_Items[18] as PersistentTrafficControl).TrafficSignal;
            }
        }
    }
}
