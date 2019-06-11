
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Thrown when a route is attempted on a null-equivalent (uninitialized)
    /// LatLon coordinate (coordinates will be set to 0,0).
    /// </summary>
    public sealed class InvalidRouteStopException : RoutingException
    {
        internal InvalidRouteStopException() { }
        internal InvalidRouteStopException(string msg) : base(msg) { }
    }
}
