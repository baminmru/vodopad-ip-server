
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// The type of error message given when a RouteNotFoundException is thrown.
    /// </summary>
    public enum RouteNotFoundErrorMsg
    {
        /// <summary>
        /// Route could not be found due to an impassable area.
        /// </summary>
        ImpassableArea,
        /// <summary>
        /// A possible route was determined to be excessively long.
        /// </summary>
        RouteTooLong
    }
}
