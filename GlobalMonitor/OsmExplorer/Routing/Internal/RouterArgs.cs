using OsmExplorer.Spatial;

namespace OsmExplorer.Routing.Internal
{
    internal class RouterArgs
    {
        public RouterArgs() { }

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public LatLon Start { get; set; }
        public LatLon End { get; set; }
        public RouteLink[] StartLinks { get; set; }
        public RouteLink[] EndLinks { get; set; }
        public RoutingStrategy Strategy { get; set; }
        public VehicleType vehicleType { get; set; }
        public SpeedProfile Profile { get; set; }
        public bool AvoidUTurns { get; set; }
        public bool AvoidTollways { get; set; }
        public double Range { get; set; }
    }
}
