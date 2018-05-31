using OsmExplorer.Data.Internal;
using OsmExplorer.Routing.Internal;
using OsmExplorer.Spatial;
using Volante;

namespace OsmExplorer.Data
{
    internal sealed class DatabaseRoot : Persistent
    {
        public DatabaseRoot() { }
        public DatabaseRoot(IDatabase Db) 
        {
            CellNdx = Db.CreateIndex<uint, IIndex<ulong, RoadLink>>(IndexType.Unique);

            for (uint i = 0; i < 12; i++)
                CellNdx.Put(i, Db.CreateThickIndex<ulong, RoadLink>());
            
            SpatialNdx = Db.CreateSpatialIndexR2<RoadLink>();

            LatLonNdx = Db.CreateIndex<long, PersistentLatLon>(IndexType.Unique);
            RestrictionsNdx = Db.CreateIndex<int, PersistentAccessRestriction>(IndexType.Unique);
            TrafficControlNdx = Db.CreateIndex<int, PersistentTrafficControl>(IndexType.Unique);
            CategoryNdx = Db.CreateIndex<byte, PersistentRoadCategory>(IndexType.Unique);
            NameNdx = Db.CreateIndex<string, PersistentString>(IndexType.Unique);
            DirectionNdx = Db.CreateIndex<int, PersistentTravelDirection>(IndexType.Unique);
            HeadingNdx = Db.CreateIndex<long, HeadingCollection>(IndexType.Unique);
            DimensionsNdx = Db.CreateIndex<ulong, RoadDimensions>(IndexType.Unique);
            SpeedNdx = Db.CreateIndex<byte, PersistentSpeed>(IndexType.Unique);
            LengthNdx = Db.CreateIndex<uint, PersistentLength>(IndexType.Unique);
            WayIdNdx = Db.CreateIndex<long, PersistentWayId>(IndexType.Unique);
        }
        
        public IIndex<uint, IIndex<ulong, RoadLink>> CellNdx;
        public ISpatialIndexR2<RoadLink> SpatialNdx;

        public IIndex<long, PersistentLatLon> LatLonNdx;
        public IIndex<int, PersistentAccessRestriction> RestrictionsNdx;
        public IIndex<int, PersistentTrafficControl> TrafficControlNdx;
        public IIndex<byte, PersistentRoadCategory> CategoryNdx;
        public IIndex<string, PersistentString> NameNdx;
        public IIndex<int, PersistentTravelDirection> DirectionNdx;
        public IIndex<long, HeadingCollection> HeadingNdx;
        public IIndex<ulong, RoadDimensions> DimensionsNdx;
        public IIndex<byte, PersistentSpeed> SpeedNdx;
        public IIndex<uint, PersistentLength> LengthNdx;
        public IIndex<long, PersistentWayId> WayIdNdx;
    }
}
