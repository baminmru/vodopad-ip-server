using System;
using OsmExplorer.Spatial;

namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Error thrown when a route cannot be found between a specified
    /// start and end location. Can be the result of insufficient data,
    /// erroneous data or impassable areas on the map, among other things.
    /// </summary>
    public sealed class RouteNotFoundException : RoutingException
    {
        private const string m_MsgRouteTooLong = "A route could not be found between locations {0} and {1}. \r "
            + "A feasible route exists but would be excessively long because of the existence \r"
            + "of prohibited areas for the chosen vehicle type and/or routing constraints.";
        private const string m_MsgImpassableArea = "A route could not be found between locations {0} and {1}. \r "
            + "An impassable area exists between the chosen start and end points exists. This may be due to \r"
            + "erroneous and/or missing map data.";
        private static string m_GetMessage(RouteNotFoundErrorMsg cause)
        {
            switch (cause)
            {
                case RouteNotFoundErrorMsg.ImpassableArea:
                    return m_MsgImpassableArea;
                case RouteNotFoundErrorMsg.RouteTooLong:
                    return m_MsgRouteTooLong;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal RouteNotFoundException() : base("A route could not be found between the specified start and end locations.") { }
        internal RouteNotFoundException(LatLon Start, LatLon End, RouteNotFoundErrorMsg cause) : base(string.Format(m_GetMessage(cause), Start.ToString(), End.ToString())) { }
        internal RouteNotFoundException(string msg) : base(msg) { }
    }
}
