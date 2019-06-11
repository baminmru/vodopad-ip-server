using System;
using OsmExplorer.Spatial;

namespace OsmExplorer.Components
{
    /// <summary>
    /// Class containing event data for the ViewChangedEvent within a MapExplorer control.
    /// </summary>
    public class ViewChangedEventArgs : EventArgs
    {
        #region Internal
        private LatLon m_CenterPosition;
        private double m_ZoomScale;

        internal ViewChangedEventArgs(LatLon centerPosition, double zoomScale) 
        {
            m_CenterPosition = centerPosition;
            m_ZoomScale = zoomScale;
        }
        #endregion

        /// <summary>
        /// Gets the LatLon coordinate of the center of a MapExplorer control.
        /// </summary>
        public LatLon CenterPosition 
        {
            get 
            {
                return m_CenterPosition;
            }
        }
        /// <summary>
        /// Gets the zoom level of the current view within a MapExplorer control.
        /// </summary>
        public double ZoomScale 
        {
            get 
            {
                return m_ZoomScale;
            }
        }
    }
}
