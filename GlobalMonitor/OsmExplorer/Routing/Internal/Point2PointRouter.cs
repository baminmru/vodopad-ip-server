using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using OsmExplorer.Collections;
using OsmExplorer.Spatial;

namespace OsmExplorer.Routing.Internal
{
    /// <summary>
    /// Arc-based bidirectional A* algorithm based on the HBA* algorithm
    /// (see http://www.icsi.berkeley.edu/pubs/techreports/TR-09-007.pdf).
    /// </summary>
    internal sealed class Point2PointRouter : RouterBase
    {
        public Point2PointRouter(RouterArgs args)
            : base(args)
        {
            Start = args.Start;
            End = args.End;
            Strategy = args.Strategy;
            VehicleType = args.vehicleType;
            Profile = args.Profile;
            InitializeOpenSet(args.StartLinks, args.EndLinks);
            ForwardClosedSet = new ConcurrentDictionary<long, Route>(2, 1000);
            BackwardClosedSet = new ConcurrentDictionary<long, Route>(2, 1000);
            stopWatch = new Stopwatch();
        }
        public P2PRouterResult Calculate()
        {
            stopWatch.Start();
            while (true)
            {
                ForwardSearch:
                if (ForwardOpenSet.IsEmpty)
                    return null;

                BestForwardCategory = Math.Min((uint)ForwardOpenSet.Peek().LastLink.RoadCategory, BestForwardCategory);

                if (BestForwardCategory < BestBackwardCategory)
                    goto BackwardSearch;

                Route ForwardPath = ForwardOpenSet.Pop();
                if (!ForwardClosedSet.TryAdd(ForwardPath.LastLink.End.Id, ForwardPath))
                    goto ForwardSearch;

                Route OtherPath;
                if (BackwardClosedSet.TryGetValue(ForwardPath.LastLink.End.Id, out OtherPath))
                    return RouteConstructor(ForwardPath, OtherPath);

                Stack<RouteLink> ForwardCandidates = RoutingNetwork.Candidates(ForwardPath, SearchDirection.Forward);
                while (ForwardCandidates.Count > 0)
                {
                    RouteLink rl = ForwardCandidates.Pop();
                    if (ForwardPath.LastLink.Equals(rl))
                        continue;
                    var cost = Cost(ForwardPath.LastLink, rl, SearchDirection.Forward);
                    if (cost >= ProhibitedCost)
                        continue;
                    var newPath = ForwardPath.AddLink(rl, cost);
                    ForwardOpenSet.Push(newPath.TotalCost + Estimate(rl.End, End, Start), newPath);
                }

                BackwardSearch:
                if (BackwardOpenSet.IsEmpty)
                    return null;

                BestBackwardCategory = Math.Min((uint)BackwardOpenSet.Peek().LastLink.RoadCategory, BestBackwardCategory);

                if (BestBackwardCategory < BestForwardCategory)
                    goto ForwardSearch;

                Route BackwardPath = BackwardOpenSet.Pop();
                if (!BackwardClosedSet.TryAdd(BackwardPath.LastLink.End.Id, BackwardPath))
                    goto BackwardSearch;

                if (ForwardClosedSet.TryGetValue(BackwardPath.LastLink.End.Id, out OtherPath))
                    return RouteConstructor(OtherPath, BackwardPath);

                Stack<RouteLink> BackwardCandidates = RoutingNetwork.Candidates(BackwardPath, SearchDirection.Backward);
                while (BackwardCandidates.Count > 0)
                {
                    RouteLink rl = BackwardCandidates.Pop();
                    if (BackwardPath.LastLink.Equals(rl))
                        continue;
                    var cost = Cost(BackwardPath.LastLink, rl, SearchDirection.Backward);
                    if (cost >= ProhibitedCost)
                        continue;
                    var newPath = BackwardPath.AddLink(rl, cost);
                    BackwardOpenSet.Push(newPath.TotalCost + Estimate(rl.End, Start, End), newPath);
                }
            }
        }

        protected override uint BestForwardCategory { get; set; }
        protected override uint BestBackwardCategory { get; set; }
        protected override ConcurrentDictionary<long, Route> ForwardClosedSet { get; set; }
        protected override ConcurrentDictionary<long, Route> BackwardClosedSet { get; set; }
        protected override PriorityHeap<double, Route> ForwardOpenSet { get; set; }
        protected override PriorityHeap<double, Route> BackwardOpenSet { get; set; }
        protected override Stopwatch stopWatch { get; set; }
        protected override LatLon End { get; set; }
        protected override LatLon Start { get; set; }
        protected override RoutingStrategy Strategy { get; set; }
        protected override VehicleType VehicleType { get; set; }
        protected override SpeedProfile Profile { get; set; }
    }
}
