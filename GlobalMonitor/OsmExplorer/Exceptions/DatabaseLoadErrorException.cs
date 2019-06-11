using System;

namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Error thrown when a .dbf file fails to load. Check that 
    /// the file is located in the /bin folder along with the application executable.
    /// The DatabaseRepository uses Directory.GetCurrentDirectory() to look
    /// in the executable directory for the MapData.dbs file
    /// and maintains an open database connection while an application is running.
    /// </summary>
    public sealed class DatabaseLoadErrorException : DatabaseException
    {
        internal DatabaseLoadErrorException() : base("Error loading database file.") { }
        internal DatabaseLoadErrorException(string msg) : base(msg) { }
    }
}
