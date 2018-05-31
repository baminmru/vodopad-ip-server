using System;
using OsmExplorer.Units;

namespace OsmExplorer.Routing
{
    /// <summary>
    /// Class representing a single driving instruction.
    /// </summary>
    public class Direction
    {
        #region Private
        private const string m_ArrivalMsg = "Arrive at {0}.";
        private const string m_DepartMsg = "Depart from {0}.";
        private const string m_ThruMsg = "Continue straight on {0}.";
        private const string m_RightTurnMsg = "Turn right onto {0}.";
        private const string m_LeftTurnMsg = "Turn left onto {0}.";
        private const string m_SlightRightMsg = "Veer slight right on {0}";
        private const string m_SlightLeftMsg = "Veer slight left on {0}";
        private const string m_UTurnMsg = "Make a U-turn at the end of {0}.";

        private MovementType m_Movement;
        private string m_StreetName;
        private Length m_CumulativeDistance;
        private TimeSpan m_CumulativeTravelTime;
        #endregion
        #region Internal
        internal Direction(string streetName, 
            MovementType movement,
            Length cumulativeDistance,
            TimeSpan cumulativeTime) 
        {
            m_StreetName = streetName;
            m_Movement = movement;
            m_CumulativeDistance = cumulativeDistance;
            m_CumulativeTravelTime = cumulativeTime;
        }
        #endregion

        /// <summary>
        /// Returns the driving instruction for the particular movement.
        /// </summary>
        public string DirectionMsg 
        {
            get 
            {
                switch (m_Movement) 
                {
                    case MovementType.Arrival:
                        return string.Format(m_ArrivalMsg, m_StreetName);
                    case MovementType.Departure:
                        return string.Format(m_DepartMsg, m_StreetName);
                    case MovementType.LeftTurn:
                        return string.Format(m_LeftTurnMsg, m_StreetName);
                    case MovementType.RightTurn:
                        return string.Format(m_RightTurnMsg, m_StreetName);
                    case MovementType.SlightLeft:
                        return string.Format(m_SlightLeftMsg, m_StreetName);
                    case MovementType.SlightRight:
                        return string.Format(m_SlightRightMsg, m_StreetName);
                    case MovementType.StraightAhead:
                        return string.Format(m_ThruMsg, m_StreetName);
                    case MovementType.UTurn:
                        return string.Format(m_UTurnMsg, m_StreetName);
                default:
                        throw new ArgumentException("Unsupported movement type");
                }
            }
        }
        /// <summary>
        /// Returns the MovementType enumeration for the Direction.
        /// </summary>
        public MovementType Movement 
        {
            get 
            {
                return m_Movement;
            }
        }
        /// <summary>
        /// Returns the estimated cumulative distance travelled up to the point where
        /// this direction is given along a route.
        /// </summary>
        public Length CumulativeDistance 
        {
            get 
            {
                return m_CumulativeDistance;
            }
        }
        /// <summary>
        /// Returns the estimated cumulative travel time up to the point where
        /// this direction is given along a route.
        /// </summary>
        public TimeSpan CumulativeTravelTime 
        {
            get 
            {
                return m_CumulativeTravelTime;
            }
        }
    }
}
