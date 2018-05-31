
namespace OsmExplorer.Routing
{
    /// <summary>
    /// Enumeration representing a type of movement or turn along a route.
    /// </summary>
    public enum MovementType
    {
        /// <summary>
        /// Represents an arrival at a destination.
        /// </summary>
        Arrival,
        /// <summary>
        /// Represents a departure from an origin.
        /// </summary>
        Departure,
        /// <summary>
        /// Indicates a left turn movement.
        /// </summary>
        LeftTurn,
        /// <summary>
        /// Indicates a right turn movement.
        /// </summary>
        RightTurn,
        /// <summary>
        /// Indicates a slight left turn, usually occurring at a split
        /// junction but not a formal intersection.
        /// </summary>
        SlightLeft,
        /// <summary>
        /// Indicates a slight right turn, usually occurring at a split
        /// junction but not a formal intersection.
        /// </summary>
        SlightRight,
        /// <summary>
        /// Indicates a straight or thru movement.
        /// </summary>
        StraightAhead,
        /// <summary>
        /// Indicates a u-turn movement.
        /// </summary>
        UTurn
    }
}
