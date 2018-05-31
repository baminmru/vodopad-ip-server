using System;
using System.Collections.Generic;
using System.Linq;
using OsmExplorer.Collections;
using OsmExplorer.Exceptions;
using OsmExplorer.Functions;
using OsmExplorer.Spatial;
using Volante;

namespace OsmExplorer.Data
{
    /// <summary>
    /// Provides methods for querying the spatial database contained in the .dbf file.
    /// </summary>
    public static class SpatialQuery
    {
        internal static RoadLink QueryLink(LatLon Position, out LatLon ClosestLinkPoint, out LatLon[] InterpolatedArray)
        {
            var SearchBoxLimit = 0.05;
            var SearchBoxSize = 0.01;

            BoundingBox bb;
            var SortedQueryResults = new PriorityHeap<double, Tuple<RoadLink, LatLon, LatLon[]>>();

            while (SortedQueryResults.IsEmpty && SearchBoxSize < SearchBoxLimit)
            {
                bb = new BoundingBox(Position, SearchBoxSize);
                RoadLink[] rlinks = new RoadLink[] { };

                lock (DataRepository.Root)
                {
                    rlinks = DataRepository.Root.SpatialNdx.Get(bb.Rectangle);
                }

                if (rlinks.Count() == 0)
                {
                    SearchBoxSize += 0.01;
                    bb = new BoundingBox(Position, SearchBoxSize);
                    continue;
                }

                foreach (var rl in rlinks)
                {
                    List<LatLon> IPoints = new List<LatLon>();
                    IPoints.Add(rl.Points[0]);
                    var SortedLinkPoints = new PriorityHeap<double, LatLon>();
                    for (int i = 1; i < rl.Points.Count(); i++)
                    {
                        LatLon[] points = MathFunctions.Interpolate(rl.Points[i - 1], rl.Points[i], 20);
                        for (int j = 1; j < points.Count(); j++)
                            IPoints.Add(points[j]);
                    }

                    var SortedDistances = new SortedSet<double>();
                    foreach (LatLon pt in IPoints)
                    {
                        SortedDistances.Add(Position.PDistanceTo(pt).Meters);
                        SortedLinkPoints.Push(Position.DistanceTo(pt).Meters, pt);
                    }

                    var MinDistance = SortedDistances.Min();
                    SortedQueryResults.Push(MinDistance, new Tuple<RoadLink, LatLon, LatLon[]>(rl, SortedLinkPoints.Peek(), IPoints.ToArray()));
                }

                SearchBoxSize += 0.01;
            }

            if (!SortedQueryResults.IsEmpty)
            {
                ClosestLinkPoint = SortedQueryResults.Peek().Item2;
                InterpolatedArray = SortedQueryResults.Peek().Item3;
                return SortedQueryResults.Peek().Item1;
            }
            ClosestLinkPoint = new LatLon();
            InterpolatedArray = null;
            return null;
        }

        /// <summary>
        /// Queries the nearest RoadLink to the given LatLon coordinate. Useful for obtaining
        /// RoadLink information and as a hook for arc-based routing engines.
        /// </summary>
        /// <param name="Position">Query location.</param>
        /// <returns>Nearest RoadLink to the queried location.</returns>
        /// <exception cref=" OsmExplorer.Exceptions.DataRepositoryNotOpenException"> Thrown when the DataRepository is not open or failed to load.</exception>
        /// <exception cref="OsmExplorer.Exceptions.RoadLinksNotFoundException">Thrown when a RoadLink could not be located near the specified location.</exception>
        public static RoadLink QueryLink(LatLon Position)
        {
            if (!DataRepository.DatabaseOpen)
                throw new DataRepositoryNotOpenException("The spatial database is not open or failed to load. \r" 
                    + " Spatial query functions are disabled.");

            var SearchBoxLimit = 0.1;
            var SearchBoxSize = 0.02;

            BoundingBox bb;
            var SortedQueryResults = new PriorityHeap<double, RoadLink>();
            RoadLink[] rlinks = new RoadLink[] { };

            while (SortedQueryResults.IsEmpty && SearchBoxSize < SearchBoxLimit)
            {
                bb = new BoundingBox(Position, SearchBoxSize);

                lock (DataRepository.Root)
                {
                    try
                    {
                        rlinks = DataRepository.Root.SpatialNdx.Get(bb.Rectangle);
                    }
                    catch (OutOfMemoryException ex) 
                    {
                        throw ex;
                    }
                }

                if (rlinks.Count() == 0)
                {
                    SearchBoxSize += 0.1;
                    bb = new BoundingBox(Position, SearchBoxSize);
                    continue;
                }

                foreach (var rl in rlinks)
                {
                    List<LatLon> IPoints = new List<LatLon>();
                    IPoints.AddRange(MathFunctions.Interpolate(rl.Points[0], rl.Points[1], 100));

                    for (int i = 2; i < rl.Points.Count(); i++)
                    {
                        LatLon[] points = MathFunctions.Interpolate(rl.Points[i - 1], rl.Points[i], 100);
                        for (int j = 1; j < points.Count(); j++)
                            IPoints.Add(points[j]);
                    }

                    var SortedDistances = new SortedSet<double>();
                    foreach (LatLon pt in IPoints)
                    {
                        SortedDistances.Add(Position.PDistanceTo(pt).Meters);
                    }

                    var MinDistance = SortedDistances.Min();
                    SortedQueryResults.Push(MinDistance, rl);
                }

                SearchBoxSize += 0.01;
            }

            if (!SortedQueryResults.IsEmpty)
                return SortedQueryResults.Peek();

            throw new RoadLinksNotFoundException("RoadLink could not be found at or near the specified location");
        }
        /// <summary>
        /// Queries all links within the provided BoundingBox. Note that large quieries
        /// can throw OutOfMemoryExceptions and make for extremely slow map rendering,
        /// so be conservative with BoundingBox size.
        /// </summary>
        /// <param name="bbox">BoundingBox to query RoadLinks</param>
        /// <param name="qmode">Query mode (see QueryMode documentation).</param>
        /// <returns>Array of RoadLinks</returns>
        public static RoadLink[] QueryLinks(BoundingBox bbox, QueryMode qmode)
        {
            if (!DataRepository.DatabaseOpen)
                throw new DataRepositoryNotOpenException("The spatial database is not open or failed to load. \r"
                    + " Spatial query functions are disabled.");

            RectangleR2 rect = new RectangleR2(bbox.y_max, bbox.x_min, bbox.y_min, bbox.x_max);
            lock (DataRepository.Root)
            {
                switch (qmode)
                {
                    case QueryMode.Contains:
                        return DataRepository.Root.SpatialNdx.Get(bbox.Rectangle);
                    case QueryMode.Overlaps:
                        return DataRepository.Root.SpatialNdx.Overlaps(bbox.Rectangle).ToArray();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        /// <summary>
        /// Queries all links within the provided BoundingBox and are of the specified 
        /// RoadCategory(s) (see RoadCategory enum for more info). Note that large quieries
        /// can throw OutOfMemoryExceptions and make for extremely slow map rendering,
        /// so be conservative with BoundingBox size.
        /// </summary>
        /// <param name="bbox">BoundingBox to query RoadLinks</param>
        /// <param name="qmode">Query mode</param>
        /// <param name="categories">RoadCategory filter</param>
        /// <returns></returns>
        public static RoadLink[] QueryLinks(BoundingBox bbox, QueryMode qmode, params RoadCategory[] categories)
        {
            if (!DataRepository.DatabaseOpen)
                throw new DataRepositoryNotOpenException("The spatial database is not open or failed to load. \r"
                    + " Spatial query functions are disabled.");
            RoadLink[] links = new RoadLink[] { };
            lock (DataRepository.Root)
            {
                switch (qmode)
                {
                    case QueryMode.Contains:
                        links = DataRepository.Root.SpatialNdx.Get(bbox.Rectangle);
                        break;
                    case QueryMode.Overlaps:
                        links = DataRepository.Root.SpatialNdx.Overlaps(bbox.Rectangle).ToArray();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            RoadLink[] ResultArray = (from n in links where categories.Contains(n.Category) select n).ToArray();
            return ResultArray;
        }
    }
}
