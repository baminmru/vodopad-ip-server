
using System;
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Base class for database exceptions
    /// </summary>
    public abstract class DatabaseException : Exception
    {
        internal DatabaseException(string msg) : base(msg) { }
    }
}
