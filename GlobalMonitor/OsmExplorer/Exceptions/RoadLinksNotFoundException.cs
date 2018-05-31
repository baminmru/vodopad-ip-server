using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmExplorer.Exceptions
{
    /// <summary>
    /// Exception thrown when a RoadLink query fails to return a RoadLink object.
    /// Most often due to insufficient/non-existent data at the quieried location.
    /// </summary>
    public sealed class RoadLinksNotFoundException : DatabaseException
    {
        internal RoadLinksNotFoundException(string msg) : base(msg) { }
    }
}
