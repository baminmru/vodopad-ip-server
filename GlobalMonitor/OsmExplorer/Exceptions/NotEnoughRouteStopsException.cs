
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Exception thrown when a route contains fewer than 2 stops.
    /// </summary>
    public sealed class NotEnoughRouteStopsException : RoutingException
    {
        internal NotEnoughRouteStopsException() { }
        internal NotEnoughRouteStopsException(string msg) : base(msg) { }
    }
}
