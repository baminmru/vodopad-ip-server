
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Exception thrown when the RoutingEngine fails to locate a .dbs data file.
    /// </summary>
    public sealed class RoutingDataNotFoundException : RoutingException
    {
        private const string m_Msg = "RoutingEngine could not locate a .dbs file. Route calculation aborted.";

        internal RoutingDataNotFoundException() : base(m_Msg) { }
    }
}
