
namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Exception thrown when the DataRepository is not open. This usually occurs when a .dbs
    /// file fails to load.
    /// </summary>
    public class DataRepositoryNotOpenException : DatabaseException
    {
        private const string m_Msg = "DataRepository could not open or could not be found.";

        internal DataRepositoryNotOpenException() : base(m_Msg) { }
        internal DataRepositoryNotOpenException(string msg) : base(msg) { }
    }
}
