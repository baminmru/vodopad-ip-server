using System.Windows.Forms;
using OsmExplorer.Spatial;

namespace OsmExplorer.Components
{
    /// <summary>
    /// Class that contains the latitude and longitude of the mouse cursor position
    /// with respect to a MaxExplorer control in addition to standard MouseEventArg
    /// data.
    /// </summary>
    public class MouseMoveEventArgs : MouseEventArgs
    {
        #region Internal
        private LatLon m_Location;
        internal MouseMoveEventArgs(LatLon location, MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            m_Location = location;
        }
        #endregion

        /// <summary>
        /// Gets the LatLon coordinate position of the mouse cursor
        /// within a MapExplorer control.
        /// </summary>
        public LatLon CoordinateLocation 
        {
            get 
            {
                return m_Location;
            }
        }
    }
}
