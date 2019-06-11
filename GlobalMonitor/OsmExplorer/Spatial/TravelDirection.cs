using System;

namespace OsmExplorer.Spatial
{
    /// <summary>
    /// Enumeration indicating the allowed directions of travel on a
    /// RoadLink. See comments.
    /// </summary>
    public enum TravelDirection
    {
        /// <summary>
        /// Travel permitted in both directions.
        /// </summary>
        Both = 1,
        /// <summary>
        /// One-way with travel allowed only from the FirstPoint
        /// to the LastPoint.
        /// </summary>
        From = 2,
        /// <summary>
        /// One-way with travel allowed only from the LastPoint
        /// to the FirstPoint.
        /// </summary>
        To = 4,
    }
}
