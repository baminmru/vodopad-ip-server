using System.Diagnostics;
using System.IO;
using OsmExplorer.Exceptions;
using OsmExplorer.Routing.Internal;
using OsmExplorer.Spatial;
using Volante;

namespace OsmExplorer.Data
{
    /// <summary>
    /// The DataRepository class loads and manages the .dbf data file 
    /// for routing and spatial queries on OSM data. The LoadRepository() method 
    /// should be called upon initialization of the application's components. See
    /// remarks.
    /// </summary>
    /// <remarks> The DataRepository is based on the
    /// Volante embedded .NET database engine (see http://blog.kowalczyk.info/software/volante/database.html 
    /// for more info).</remarks>
    [DebuggerDisplay("Connected = {DatabaseOpen}")]
    public static class DataRepository
    {
        #region Private
        private const int m_CacheSizeBytes = 67108864;
        private static IDatabase m_Db;
        private static DatabaseRoot m_DbRoot;
        private static BoundingBox m_WrappingBox;
        private static bool m_DatabaseOpen;
        #endregion
        #region Internal
        internal static DatabaseRoot Root
        {
            get
            {
                return m_DbRoot;
            }
        }
        #endregion

        /// <summary>
        /// Loads the DataRepository from the current directory.
        /// </summary>
        /// <exception cref=" OsmExplorer.Exceptions.DatabaseLoadErrorException"> Thrown when the .dbf data file is
        /// not found in the current directory.</exception>
        public static void LoadRepository()
        {
            if (m_Db == null)
            {
                m_Db = DatabaseFactory.CreateDatabase();
                m_Db.CodeGeneration = true;

                string dir = Directory.GetCurrentDirectory();
                DirectoryInfo info = new DirectoryInfo(dir);
                FileInfo[] files = info.GetFiles("*.dbf");

                foreach (var file in files) 
                {
                    m_Db.Open(dir + @"\" + file);
                    m_DbRoot = m_Db.Root as DatabaseRoot;
                    if (m_DbRoot != null)
                        break;
                }
                
                if (m_DbRoot == null) 
                {
                    m_DatabaseOpen = false;
                    throw new DatabaseLoadErrorException(string.Format("Error loading .dbf file. The file could not be found in directory:\r \r {0}. \r " 
                        + "\r" + "Spatial and routing functions will be disabled.", dir));
                }
                m_DatabaseOpen = true;
                m_WrappingBox = new BoundingBox(m_DbRoot.SpatialNdx.WrappingRectangle);
                RoutingNetwork.InitializeNetwork();
            }
        }
        /// <summary>
        /// Loads the DataRepository from the specified directory.
        /// </summary>
        /// <param name="info"></param>
        /// <exception cref=" OsmExplorer.Exceptions.DatabaseLoadErrorException"> Thrown when the .dbf data file is
        /// not found in the specified directory.</exception>
        public static void LoadRepository(FileInfo info)
        {
            if (m_Db == null)
            {
                m_Db = DatabaseFactory.CreateDatabase();
                m_Db.CodeGeneration = true;
                m_Db.Open(info.FullName);
                m_DbRoot = m_Db.Root as DatabaseRoot;

                if (m_DbRoot == null) 
                {
                    m_DatabaseOpen = false;
                    throw new DatabaseLoadErrorException("Error loading .dbf file. The file could not be found in the specified directory. \r" 
                        + "Spatial and routing functions will be disabled.");
                }
                m_DatabaseOpen = true;
                m_WrappingBox = new BoundingBox(m_DbRoot.SpatialNdx.WrappingRectangle);
                RoutingNetwork.InitializeNetwork();
            }
        }

        /// <summary>
        /// Returns true if the database is currently open, false otherwise.
        /// </summary>
        public static bool DatabaseOpen 
        {
            get 
            {
                return m_DatabaseOpen;
            }
        }
        /// <summary>
        /// Returns the minimum BoundingBox that encloses the BoundingBoxes
        /// of all elements in the current data file.
        /// </summary>
        public static BoundingBox WrappingBox
        {
            get
            {
                return m_WrappingBox;
            }
        }
    }
}
