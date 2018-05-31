
namespace OsmExplorer.Data
{
    /// <summary>
    /// RoadLink categories based on OSM highway classifications. See descriptions.
    /// </summary>
    public enum RoadCategory
    {
        /// <summary>
        /// Motorways.
        /// </summary>
        motorway = 0,
        /// <summary>
        /// Motorway links
        /// </summary>
        motorway_link = 1,
        /// <summary>
        /// Trunks roads.
        /// </summary>
        trunk = 2,
        /// <summary>
        /// Trunk links.
        /// </summary>
        trunk_link = 3,
        /// <summary>
        /// Primary roads.
        /// </summary>
        primary = 4,
        /// <summary>
        /// Primary links.
        /// </summary>
        primary_link = 5,
        /// <summary>
        /// Secondary roads.
        /// </summary>
        secondary = 6,
        /// <summary>
        /// Secondary links.
        /// </summary>
        secondary_link = 7,
        /// <summary>
        /// Tertiary roads.
        /// </summary>
        tertiary = 8,
        /// <summary>
        /// Tertiary links.
        /// </summary>
        tertiary_link = 9,
        /// <summary>
        /// Residential roads.
        /// </summary>
        residential = 10,
        /// <summary>
        /// Service and/or private roads.
        /// </summary>
        service = 11
    }
}
