
namespace OsmExplorer.Routing
{
    /// <summary>
    /// Sets the routing strategy for a route.
    /// </summary>
    public enum RoutingStrategy
    {
        /// <summary>
        /// Strategy that indicates the RoutingEngine should attempt
        /// to find the quickest route.
        /// </summary>
        Fastest = 0,
        /// <summary>
        /// Strategy that indicates the RoutingEngine should attempt
        /// to find the shortest route.
        /// </summary>
        Shortest = 1
    }
}
