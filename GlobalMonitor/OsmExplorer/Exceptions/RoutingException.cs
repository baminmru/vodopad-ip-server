using System;

namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Base class for all routing exceptions.
    /// </summary>
    public abstract class RoutingException : Exception
    {
        internal RoutingException() { }
        internal RoutingException(string msg) : base(msg) { }
    }
}
