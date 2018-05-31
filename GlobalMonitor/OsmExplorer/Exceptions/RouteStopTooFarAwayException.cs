
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Exception thrown when a route stop is too far away from the closest 
    /// traversable street. The routing engine will search for several kilometers
    /// around the start and end locations for a suitable street to start routing on,
    /// so this will most often be the result of insufficient (i.e. non-existent) map
    /// data. Make sure the MapData.dbs file contains map data for the region
    /// you are trying to route on.
    /// </summary>
    public sealed class RouteStopTooFarAwayException : RoutingException
    {
        internal RouteStopTooFarAwayException() { }
        internal RouteStopTooFarAwayException(string msg) : base(msg) { }
    }
}
