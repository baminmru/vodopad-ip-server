
namespace OsmExplorer.Spatial
{
    /// <summary>
    /// Interface representing a set of spatial and weigh dimensions for either a
    /// vehicle or as a set of maximum permitted dimensions on a RoadLink.
    /// </summary>
    public interface IDimensions
    {
        /// <summary>
        /// Height in centimeters.
        /// </summary>
        ushort Height_cm { get; }
        /// <summary>
        /// Length in centimeters.
        /// </summary>
        ushort Length_cm { get; }
        /// <summary>
        /// Width in centimeters.
        /// </summary>
        ushort Width_cm { get; }
        /// <summary>
        /// Weight in kilograms.
        /// </summary>
        ushort Weight_kg { get; }
    }
}
