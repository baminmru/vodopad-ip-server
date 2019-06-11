
namespace OsmExplorer.Data
{
    /// <summary>
    /// Query mode for BoundingBox queries. See remarks.
    /// </summary>
    public enum QueryMode
    {
        /// <summary>
        /// When selected, a BoundingBox query returns only items with
        /// BoundingBoxes entirely within the query box.
        /// </summary>
        Contains = 0,
        /// <summary>
        /// When selected, a BoundingBox query returns items whose BoundingBoxes
        /// are either contained within the query box or overlap it. Note that a RoadLink
        /// that otherwise appears completely outside the querying BoundingBox can be included
        /// since RoadLink BoundingBoxes are constructed as the smallest rectangle that contains
        /// all points in the Points array.
        /// </summary>
        Overlaps = 1
    }
}
