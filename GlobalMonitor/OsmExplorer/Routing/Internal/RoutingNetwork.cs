using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using OsmExplorer.Collections;
using OsmExplorer.Data;
using OsmExplorer.Spatial;
using Volante;

namespace OsmExplorer.Routing.Internal
{
    internal static class RoutingNetwork
    {
        private const uint m_MaxCategoryPreloaded = 5;
        private static Stopwatch m_Watch;
        private static int[] m_CellLimit;
        private static PriorityHeap<long, ulong>[] m_CellManager;
        private static ConcurrentDictionary<ulong, Dictionary<long, List<RouteLink>>>[] m_Network;
        private static void LoadSuperNetwork() 
        {
            for (uint i = 0; i < m_MaxCategoryPreloaded; i++) 
            {
                var Enumerator =  DataRepository.Root.CellNdx.Get(i).GetDictionaryEnumerator();

                while (Enumerator.MoveNext())
                {
                    var key = (ulong)Enumerator.Key;
                    var roadLink = Enumerator.Value as RoadLink;

                    var LinkArray = GetRouteLinks(roadLink, SearchDirection.Forward);
                    Dictionary<long, List<RouteLink>> SubNetwork;
                    if (!m_Network[0].TryGetValue(key, out SubNetwork))
                    {
                        SubNetwork = new Dictionary<long, List<RouteLink>>(1000);
                        m_Network[0].TryAdd(key, SubNetwork);
                    }
                    foreach (var link in LinkArray)
                    {
                        List<RouteLink> linkList;
                        if (!SubNetwork.TryGetValue(link.Start.Id, out linkList))
                        {
                            linkList = new List<RouteLink>();
                            SubNetwork.Add(link.Start.Id, linkList);
                        }
                        linkList.Add(link);
                    }

                    LinkArray = GetRouteLinks(roadLink, SearchDirection.Backward);
                    if (!m_Network[1].TryGetValue(key, out SubNetwork))
                    {
                        SubNetwork = new Dictionary<long, List<RouteLink>>(1000);
                        m_Network[1].TryAdd(key, SubNetwork);
                    }
                    foreach (var link in LinkArray)
                    {
                        List<RouteLink> linkList;
                        if (!SubNetwork.TryGetValue(link.Start.Id, out linkList))
                        {
                            linkList = new List<RouteLink>();
                            SubNetwork.Add(link.Start.Id, linkList);
                        }
                        linkList.Add(link);
                    }
                }
            }
        }

        public static void InitializeNetwork()
        {
            m_Watch = new Stopwatch();
            m_Watch.Start();
            m_Network = new ConcurrentDictionary<ulong, Dictionary<long, List<RouteLink>>>[2] 
            {
                new ConcurrentDictionary<ulong, Dictionary<long, List<RouteLink>>>(100, 100000),
                new ConcurrentDictionary<ulong, Dictionary<long, List<RouteLink>>>(100, 100000)
            };
            m_CellManager = new PriorityHeap<long, ulong>[12] 
            {
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>(),
                new PriorityHeap<long, ulong>()
            };
            m_CellLimit = new int[12] 
            {
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue,
                1000,
                200,
                200,
                200,
                100,
                100,
                100,
                50
            };
            LoadSuperNetwork();
        }
        public static void LoadSubNetwork(RoadLink rl)
        {
            List<CellId> Ids = new List<CellId>();
            Dictionary<long, List<RouteLink>> SubNetwork;
            IIndex<ulong, RoadLink>[] ndxArray = DataRepository.Root.CellNdx.Get(m_MaxCategoryPreloaded + 1, 12);
            
            foreach (var ndx in ndxArray) 
            {
                foreach (var cellId in rl.FirstPointCellIds) 
                {
                    SubNetwork = m_Network[0].GetOrAdd(cellId.Id, new Dictionary<long, List<RouteLink>>(1000));
                    foreach (var roadLink in ndx.Get(cellId.Id, cellId.Id))
                    {
                        var LinkArray = GetRouteLinks(roadLink, SearchDirection.Forward);

                        foreach (var routeLink in LinkArray)
                        {
                            List<RouteLink> rlinks;
                            if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                            {
                                rlinks = new List<RouteLink>();
                                SubNetwork.Add(routeLink.Start.Id, rlinks);
                            }
                            rlinks.Add(routeLink);
                        }
                    }
                    SubNetwork = m_Network[1].GetOrAdd(cellId.Id, new Dictionary<long, List<RouteLink>>(1000));
                    foreach (var roadLink in ndx.Get(cellId.Id, cellId.Id))
                    {
                        var LinkArray = GetRouteLinks(roadLink, SearchDirection.Backward);

                        foreach (var routeLink in LinkArray)
                        {
                            List<RouteLink> rlinks;
                            if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                            {
                                rlinks = new List<RouteLink>();
                                SubNetwork.Add(routeLink.Start.Id, rlinks);
                            }
                            rlinks.Add(routeLink);
                        }
                    }
                }

                foreach (var cellId in rl.LastPointCellIds)
                {
                    SubNetwork = m_Network[0].GetOrAdd(cellId.Id, new Dictionary<long, List<RouteLink>>(1000));
                    foreach (var roadLink in ndx.Get(cellId.Id, cellId.Id))
                    {
                        var LinkArray = GetRouteLinks(roadLink, SearchDirection.Forward);

                        foreach (var routeLink in LinkArray)
                        {
                            List<RouteLink> rlinks;
                            if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                            {
                                rlinks = new List<RouteLink>();
                                SubNetwork.Add(routeLink.Start.Id, rlinks);
                            }
                            rlinks.Add(routeLink);
                        }
                    }
                    SubNetwork = m_Network[1].GetOrAdd(cellId.Id, new Dictionary<long, List<RouteLink>>(1000));
                    foreach (var roadLink in ndx.Get(cellId.Id, cellId.Id))
                    {
                        var LinkArray = GetRouteLinks(roadLink, SearchDirection.Backward);

                        foreach (var routeLink in LinkArray)
                        {
                            List<RouteLink> rlinks;
                            if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                            {
                                rlinks = new List<RouteLink>();
                                SubNetwork.Add(routeLink.Start.Id, rlinks);
                            }
                            rlinks.Add(routeLink);
                        }
                    }
                }
            }
        }
        public static Dictionary<long, List<RouteLink>> GetOrUpdateSubNetwork(CellId cellId, Route route, SearchDirection direction)
        {
            Dictionary<long, List<RouteLink>> result = new Dictionary<long, List<RouteLink>>(1000);
            if (!m_Network[(int)direction].TryGetValue(cellId.Id, out result)) 
            {
                //var enumerator = DataRepository.Root.CellNdx.GetDictionaryEnumerator();
                //while (enumerator.MoveNext()) 
                //{
                //    var key = (uint)enumerator.Key;
                //    if (key < cellId.Category)
                //        continue;
                //    if (key > route.BestCategory)
                //        break;

                //    var index = (IIndex<ulong, RoadLink>)enumerator.Value;
                //    Dictionary<long, List<RouteLink>> SubNetwork = new Dictionary<long, List<RouteLink>>();
                //    foreach (var roadLink in index.Get(cellId.Id, cellId.Id))
                //    {
                //        var LinkArray = GetRouteLinks(roadLink, direction);
                //        foreach (var routeLink in LinkArray)
                //        {
                //            List<RouteLink> rlinks;
                //            if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                //            {
                //                rlinks = new List<RouteLink>();
                //                SubNetwork.Add(routeLink.Start.Id, rlinks);
                //            }
                //            rlinks.Add(routeLink);
                //        }
                //    }
                //}
                foreach (var cell in route.LastLink.DestinationCellIds)
                {
                    if (cell.Category > route.BestCategory)
                        return result;

                    Dictionary<long, List<RouteLink>> SubNetwork = m_Network[(int)direction].GetOrAdd(cell.Id, LoadNetwork(cell, direction));
                    foreach (var category in DataRepository.Root.CellNdx.Get(route.BestCategory, cell.Category))
                    {
                        foreach (var roadLink in category.Get(cell.Id, cell.Id))
                        {
                            var LinkArray = GetRouteLinks(roadLink, direction);
                            foreach (var routeLink in LinkArray)
                            {
                                List<RouteLink> rlinks;
                                if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                                {
                                    rlinks = new List<RouteLink>();
                                    SubNetwork.Add(routeLink.Start.Id, rlinks);
                                }
                                rlinks.Add(routeLink);
                            }
                        }
                    }
                    if (cell.Category == cellId.Category)
                        result = SubNetwork;
                }
            }
            return result;
        }
        public static Func<CellId, SearchDirection, Dictionary<long, List<RouteLink>>> LoadNetwork = (cell, direction) =>
        {
            Dictionary<long, List<RouteLink>> subNetwork = new Dictionary<long, List<RouteLink>>(1000);
            foreach (var roadLink in DataRepository.Root.CellNdx.Get(cell.Category).Get(cell.Id, cell.Id))
            {
                var LinkArray = GetRouteLinks(roadLink, direction);
                foreach (var routeLink in LinkArray)
                {
                    List<RouteLink> rlinks;
                    if (!subNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                    {
                        rlinks = new List<RouteLink>();
                        subNetwork.Add(routeLink.Start.Id, rlinks);
                    }
                    rlinks.Add(routeLink);
                }
            }
            return subNetwork;
        };

        public static void LoadSubNetwork(CellId cellId, SearchDirection direction, out Dictionary<long, List<RouteLink>> SubNetwork)
        {
            //m_CellManager[cellId.Category].Push(m_Watch.ElapsedTicks, cellId.Id);

            //if (m_CellManager[cellId.Category].Count > m_CellLimit[cellId.Category])
            //{
            //    var Id = m_CellManager[cellId.Category].Pop();
            //    m_Network[(int)direction].TryRemove(Id, out SubNetwork);
            //}
            SubNetwork = new Dictionary<long, List<RouteLink>>(1000);

            if (!m_Network[(int)direction].TryAdd(cellId.Id, SubNetwork))
                return;

            foreach (var category in DataRepository.Root.CellNdx.Get(cellId.Category, cellId.Category))
            {
                foreach (var roadLink in category.Get(cellId.Id, cellId.Id)) 
                {
                    var LinkArray = GetRouteLinks(roadLink, direction);
                    foreach (var routeLink in LinkArray)
                    {
                        List<RouteLink> rlinks;
                        if (!SubNetwork.TryGetValue(routeLink.Start.Id, out rlinks))
                        {
                            rlinks = new List<RouteLink>();
                            SubNetwork.Add(routeLink.Start.Id, rlinks);
                        }
                        rlinks.Add(routeLink);
                    }
                }
            }
        }
        public static RouteLink[] GetRouteLinks(RoadLink rl, SearchDirection direction)
        {
            switch ((int)rl.DirectionOfTravel ^ (int)direction)
            {
                case 1:
                    return new RouteLink[] 
                    { 
                        new RouteLink(rl, rl.FirstPoint, rl.LastPoint, TravelDirection.From, rl.LastPointCellIds), 
                        new RouteLink(rl, rl.LastPoint, rl.FirstPoint, TravelDirection.To, rl.FirstPointCellIds)
                    };
                case 2:
                    return new RouteLink[] 
                    { 
                        new RouteLink(rl, rl.FirstPoint, rl.LastPoint, TravelDirection.From, rl.LastPointCellIds) 
                    };
                case 4:
                    return new RouteLink[] 
                    { 
                        new RouteLink(rl, rl.LastPoint, rl.FirstPoint, TravelDirection.To, rl.FirstPointCellIds) 
                    };
                case 0:
                    return new RouteLink[] 
                    { 
                        new RouteLink(rl, rl.LastPoint, rl.FirstPoint, TravelDirection.From, rl.FirstPointCellIds), 
                        new RouteLink(rl, rl.FirstPoint, rl.LastPoint, TravelDirection.To, rl.LastPointCellIds) 
                    };
                case 3:
                    return new RouteLink[] 
                    { 
                        new RouteLink(rl, rl.LastPoint, rl.FirstPoint, TravelDirection.From, rl.FirstPointCellIds) 
                    };
                case 5:
                    return new RouteLink[] 
                    { 
                        new RouteLink(rl, rl.FirstPoint, rl.LastPoint, TravelDirection.To, rl.LastPointCellIds) 
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static Stack<RouteLink> Candidates(Route route, SearchDirection direction)
        {
            Stack<RouteLink> Candidates = new Stack<RouteLink>();
            List<RouteLink> Rlinks;
            Dictionary<long, List<RouteLink>> SubNetwork;

            foreach (var cell in route.LastLink.DestinationCellIds) 
            {
                if (cell.Category > route.BestCategory)
                    return Candidates;

                if (!m_Network[(int)direction].TryGetValue(cell.Id, out SubNetwork))
                    LoadSubNetwork(cell, direction, out SubNetwork);

                if (SubNetwork.TryGetValue(route.LastLink.End.Id, out Rlinks))
                    Rlinks.ForEach(y => Candidates.Push(y));
            }
            return Candidates;
        }
    }
}
