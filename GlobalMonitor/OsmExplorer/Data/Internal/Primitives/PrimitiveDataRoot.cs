using Volante;
using OsmExplorer.Spatial;
using OsmExplorer.Routing.Internal;

namespace OsmExplorer.Data.Internal.Primitives
{
    internal class PrimitiveDataRoot : Persistent
    {
        public PrimitiveDataRoot() { }
        public PrimitiveDataRoot(IDatabase Db)
        {
            NodeNdx = Db.CreateIndex<long, Node>(IndexType.Unique);
            WayNdx = Db.CreateIndex<long, Way>(IndexType.Unique);
            TagNdx = Db.CreateIndex<string, PersistentString>(IndexType.Unique);
            ArcNdx = Db.CreateIndex<long, Volante.ISet<Way>>(IndexType.Unique);
            VertexSet = Db.CreateSet<Node>();
        }

        public IIndex<long, Node> NodeNdx;
        public IIndex<long, Way> WayNdx;
        public IIndex<string, PersistentString> TagNdx;
        public IIndex<long, Volante.ISet<Way>> ArcNdx;
        public Volante.ISet<Node> VertexSet;
    }
}
