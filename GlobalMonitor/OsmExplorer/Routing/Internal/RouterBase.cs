using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OsmExplorer.Collections;
using OsmExplorer.Functions;
using OsmExplorer.Spatial;
using OsmExplorer.Units;

namespace OsmExplorer.Routing.Internal
{
    /// <summary>
    /// Base class for all P2P router implementations.
    /// </summary>
    internal abstract class RouterBase
    {
        #region Private
        private readonly Dictionary<int, Tuple<double[], bool>> TurnCostTable;
        private readonly Dictionary<int, MovementType> MovementTable;
        private const double DiscouragedCost = 1000D;
        private const double StopSignCost = 5; // In seconds
        private const double StopLightCost = 5; // In seconds
        private const double DefaultUTurnCost = 10; // In seconds
        private double UTurnCost;
        private double TollRoadCost;
        private double MaxSpeed;
        private double Range;
        #endregion

        /// <summary>
        /// Cost assigned to prohibited RoadLinks. Rather than modify the underlying graph
        /// structure and removing prohibited links, they are instead assigned 
        /// a very high cost (unattainable by standard distance or calculated travel time metrics)
        /// and skipped as the routing algorithm populates the open set.
        /// </summary>
        protected const double ProhibitedCost = uint.MaxValue;
        
        protected RouterBase(RouterArgs args)
        {
            Range = args.Range;
            UTurnCost = args.AvoidUTurns ? DiscouragedCost : DefaultUTurnCost;
            TollRoadCost = args.AvoidTollways ? DiscouragedCost : 0;
            MaxSpeed = args.Profile.MaxSpeed;

            TurnCostTable = new Dictionary<int, Tuple<double[], bool>>(361);
            MovementTable = new Dictionary<int, MovementType>(361);

            for (int i = -20; i <= 20; i++)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 0, 0 }, false));
                MovementTable.Add(i, MovementType.StraightAhead);
            }
            for (int i = -21; i >= -50; i--)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 0, 0 }, false));
                MovementTable.Add(i, MovementType.SlightLeft);
            }
            for (int i = 21; i <= 50; i++)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 0, 0 }, false));
                MovementTable.Add(i, MovementType.SlightRight);
            }
            for (int i = 51; i <= 140; i++)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 2, 0 }, false));
                MovementTable.Add(i, MovementType.RightTurn);
            }
            for (int i = -51; i >= -140; i--)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 4, 0 }, false));
                MovementTable.Add(i, MovementType.LeftTurn);
            }
            for (int i = -141; i >= -180; i--)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 20, 1000 }, args.AvoidUTurns));
                MovementTable.Add(i, MovementType.UTurn);
            }
            for (int i = 141; i <= 180; i++)
            {
                TurnCostTable.Add(i, new Tuple<double[], bool>(new double[] { 20, 1000 }, args.AvoidUTurns));
                MovementTable.Add(i, MovementType.UTurn);
            }
        }
        
        /// <summary>
        /// A* estimating heuristic. Inflating the heuristic improves runtime but can cause the
        /// algorithm to produce suboptimal paths.
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="P3"></param>
        /// <returns></returns>
        protected double Estimate(LatLon P1, LatLon P2, LatLon P3)
        {
            double distance1 = P1.DistanceTo(P2).Meters;
            double distance2 = P1.DistanceTo(P3).Meters;
            double DistFunction = Math.Max((distance1 + distance2) / 2, distance1);
            double time = DistFunction / MaxSpeed;

            switch (Strategy)
            {
                case RoutingStrategy.Fastest:
                    return time;
                case RoutingStrategy.Shortest:
                    return 1.2 * DistFunction;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        protected static uint TrafficControlEffect(char ControlLocation, char TravelDirection)
        {
            switch (ControlLocation ^ TravelDirection)
            {
                case 0:
                case 14:
                    return 1;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Determines whether a turn is prohibited while outputting an associated turn cost.
        /// </summary>
        /// <param name="Previous"></param>
        /// <param name="Next"></param>
        /// <param name="direction"></param>
        /// <param name="turnCost"></param>
        /// <returns></returns>
        protected bool TurnProhibited(RouteLink Previous, RouteLink Next, SearchDirection direction, out double turnCost)
        {
            int headingDiff;
            Tuple<double[], bool> turnItems;
            switch ((int)direction ^ (int)Previous.Direction << (int)Next.Direction)
            {
                    //Forward Search
                case 8: //0 ^ From << From = 8
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_L_Outbound, Next.Flags.Heading_F_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                case 16: //0 ^ To << From = 16
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_F_Outbound, Next.Flags.Heading_F_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                case 32: //0 ^ From << To = 32
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_L_Outbound, Next.Flags.Heading_L_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                case 64: //0 ^ To << To = 64
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_F_Outbound, Next.Flags.Heading_L_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;

                    //Backward Search
                case 9: //1 ^ From << From = 9
                    headingDiff = (int)MathFunctions.HeadingDiff(Next.Flags.Heading_L_Outbound, Previous.Flags.Heading_F_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                case 17: //1 ^ To << From = 17
                    headingDiff = (int)MathFunctions.HeadingDiff(Next.Flags.Heading_L_Outbound, Previous.Flags.Heading_L_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                case 33: //1 ^ From << To = 33
                    headingDiff = (int)MathFunctions.HeadingDiff(Next.Flags.Heading_F_Outbound, Previous.Flags.Heading_F_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                case 65: //1 ^ To << To = 65
                    headingDiff = (int)MathFunctions.HeadingDiff(Next.Flags.Heading_F_Outbound, Previous.Flags.Heading_L_Inbound);
                    turnItems = TurnCostTable[headingDiff];
                    turnCost = turnItems.Item1[(int)Strategy];
                    return turnItems.Item2;
                default:
                    throw new ArgumentOutOfRangeException("Bit-shift argument out of range.");
            }
        }
        protected bool AccessProhibited(RouteLink Previous, RouteLink Next, SearchDirection direction)
        {
            switch (direction)
            {
                case SearchDirection.Forward:
                    return Next.Flags.ProhibitedTo.Any(x => x == Previous.WayId);
                case SearchDirection.Backward:
                    return Previous.Flags.ProhibitedFrom.Any(x => x == Next.WayId);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        protected MovementType TurnInterpreter(RouteLink Previous, RouteLink Next) 
        {
            int headingDiff;
            switch ((int)Previous.Direction << (int)Next.Direction) 
            {
                case 8: //0 ^ From << From = 8
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_L_Outbound, Next.Flags.Heading_F_Inbound);
                    return MovementTable[headingDiff];
                case 16: //0 ^ To << From = 16
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_F_Outbound, Next.Flags.Heading_F_Inbound);
                    return MovementTable[headingDiff];
                case 32: //0 ^ From << To = 32
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_L_Outbound, Next.Flags.Heading_L_Inbound);
                    return MovementTable[headingDiff];
                case 64: //0 ^ To << To = 64
                    headingDiff = (int)MathFunctions.HeadingDiff(Previous.Flags.Heading_F_Outbound, Next.Flags.Heading_L_Inbound);
                    return MovementTable[headingDiff];
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        protected void SetTravelTime(ref RouteLink rlink)
        {
            if (rlink.TravelTime == 0)
                rlink.TravelTime = rlink.Flags.TravelDistance.Meters / Profile.Speeds[(uint)rlink.RoadCategory];
        }
        protected double Cost(RouteLink Previous, RouteLink Next, SearchDirection direction)
        {
            bool ProhibitedAccess;
            double TurnCost;
            SetTravelTime(ref Next);
            switch ((int)Strategy ^ (int)VehicleType)
            {
                //Fastest
                //Auto
                case 1:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.TravelTime
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Truck_1
                case 2:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.DeliveryAccess ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.TravelTime
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Truck_2
                case 4:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.TruckAccess ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.TravelTime
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Hazmat
                case 8:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.HazmatAccess ||
                        Next.Flags.Tunnel ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.TravelTime
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Scooter
                case 16:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction) ||
                        !Next.Flags.MopedAccess;
                    return Next.TravelTime
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Shortest
                //Auto
                case 0:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.Flags.TravelDistance.Meters
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Truck_1
                case 3:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.DeliveryAccess ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.Flags.TravelDistance.Meters
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Truck_2
                case 5:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.TruckAccess ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.Flags.TravelDistance.Meters
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Hazmat
                case 9:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.HazmatAccess ||
                        Next.Flags.Tunnel ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction);
                    return Next.Flags.TravelDistance.Meters
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                //Scooter
                case 17:
                    ProhibitedAccess =
                        TurnProhibited(Previous, Next, direction, out TurnCost) ||
                        !Next.Flags.MotorVehicleAccess ||
                        AccessProhibited(Previous, Next, direction) ||
                        !Next.Flags.MopedAccess;
                    return Next.Flags.TravelDistance.Meters
                        + TurnCost
                        + Convert.ToUInt16(Next.Flags.Tollway) * TollRoadCost
                        + Convert.ToUInt16(ProhibitedAccess) * ProhibitedCost
                        + Convert.ToUInt16(Next.Flags.PrivateAccess) * DiscouragedCost;
                default:
                    throw new ArgumentOutOfRangeException("Cost argument out of range");
            }
        }
        protected P2PRouterResult RouteConstructor(Route ForwardPath, Route ReversePath)
        {
            List<LatLon> ForwardPoints = new List<LatLon>(100000);
            List<LatLon> ReversePoints = new List<LatLon>(100000);
            HashSet<LatLon> PointsRef = new HashSet<LatLon>();

            List<RouteLink> ForwardLinks = new List<RouteLink>(1000);
            List<RouteLink> BackwardLinks = new List<RouteLink>(1000);
            foreach (RouteLink rl in ForwardPath)
            {
                if (rl.End == rl.Points[0])
                {
                    for (int i = 0; i < rl.Points.Count(); i++)
                    {
                        if (PointsRef.Add(rl.Points[i]))
                            ForwardPoints.Add(rl.Points[i]);
                    }
                }
                else
                {
                    for (int i = rl.Points.Count() - 1; i >= 0; i--)
                    {
                        if (PointsRef.Add(rl.Points[i]))
                            ForwardPoints.Add(rl.Points[i]);
                    }
                }
                ForwardLinks.Add(rl);
            }
            ForwardPoints.Reverse();
            ForwardLinks.Reverse();
            foreach (RouteLink rl in ReversePath)
            {
                if (rl.End == rl.Points[0])
                {
                    for (int i = 0; i < rl.Points.Count(); i++)
                        ReversePoints.Add(rl.Points[i]);
                }
                else
                {
                    for (int i = rl.Points.Count() - 1; i >= 0; i--)
                        ReversePoints.Add(rl.Points[i]);
                }
                
                BackwardLinks.Add(rl);
            }
            List<LatLon> RoutePoints = new List<LatLon>(ForwardPoints);
            RoutePoints.AddRange(ReversePoints);

            List<RouteLink> RouteLinks = new List<RouteLink>(ForwardLinks);
            RouteLinks.AddRange(BackwardLinks);

            P2PRouterDirections directions = new P2PRouterDirections();
            string previous;
            Direction dir;
            if (RouteLinks.First().Names.Count() == 0)
            {
                dir = new Direction("unknown street", MovementType.Departure, new Length(0, LengthUnits.Meters), new TimeSpan(0));
                directions.Add(dir, this.Start);
                previous = "unknown street";
            }
            else 
            {
                dir = new Direction(RouteLinks.First().Names[0], MovementType.Departure, new Length(0, LengthUnits.Meters), new TimeSpan(0));
                directions.Add(dir, this.Start);
                previous = RouteLinks.First().Names[0];
            }

            Length cumulativeDistance = new Length(0, LengthUnits.Meters);
            TimeSpan cumulativeTime = new TimeSpan(0);
            for (int i = 1; i < RouteLinks.Count(); i++)
            {
                cumulativeDistance += RouteLinks[i].Flags.TravelDistance;
                cumulativeTime += new TimeSpan((long)RouteLinks[i].TravelTime * TimeSpan.TicksPerSecond);
                MovementType movement = TurnInterpreter(RouteLinks[i - 1], RouteLinks[i]);
                if (RouteLinks[i].Names.Count() == 0)
                {
                    if (movement != MovementType.StraightAhead)
                        directions.Add(new Direction("unknown street", movement, cumulativeDistance, cumulativeTime), RouteLinks[i].End);
                    else
                        directions.Add(new Direction("unknown street", movement, cumulativeDistance, cumulativeTime));
                }
                else if (RouteLinks[i].Names[0] == previous)
                    continue;
                else 
                {
                    if (movement != MovementType.StraightAhead)
                        directions.Add(new Direction(RouteLinks[i].Names[0], movement, cumulativeDistance, cumulativeTime));
                    else
                        directions.Add(new Direction(RouteLinks[i].Names[0], movement, cumulativeDistance, cumulativeTime), RouteLinks[i].End);
                }
            }

            var Distance = MathFunctions.Distance(RoutePoints.ToArray());
            TimeSpan TravelTime = new TimeSpan((long)(ForwardPath.TotalTravelTime + ReversePath.TotalTravelTime) * TimeSpan.TicksPerSecond);

            if (RouteLinks.Last().Names.Count() == 0)
                directions.Add(new Direction("unknown street", MovementType.Arrival, Distance, TravelTime), this.End);
            else
                directions.Add(new Direction(RouteLinks.Last().Names[0], MovementType.Arrival, Distance, TravelTime), this.End);

            stopWatch.Stop();
            return new P2PRouterResult(RoutePoints, TravelTime, Distance, this.stopWatch.Elapsed, directions);
        }
        protected void InitializeOpenSet(RouteLink[] StartLinks, RouteLink[] EndLinks)
        {
            ForwardOpenSet = new PriorityHeap<double, Route>();
            BackwardOpenSet = new PriorityHeap<double, Route>();
            Route FirstPath;
            for (int i = 0; i < StartLinks.Count(); i++)
            {
                SetTravelTime(ref StartLinks[i]);
                var distance = MathFunctions.Distance(StartLinks[i].Points).Meters;
                var time = (distance / StartLinks[i].Flags.TravelDistance.Meters) * StartLinks[i].TravelTime;
                switch (Strategy)
                {
                    case RoutingStrategy.Fastest:
                        FirstPath = new Route(StartLinks[i], time, distance, time, Range);
                        break;
                    case RoutingStrategy.Shortest:
                        FirstPath = new Route(StartLinks[i], distance, distance, time, Range);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var estimate = Estimate(StartLinks[i].End, End, Start);
                ForwardOpenSet.Push(estimate + FirstPath.TotalCost, FirstPath);
            }
            for (int i = 0; i < EndLinks.Count(); i++)
            {
                SetTravelTime(ref EndLinks[i]);
                var distance = MathFunctions.Distance(EndLinks[i].Points).Meters;
                var time = (distance / EndLinks[i].Flags.TravelDistance.Meters) * EndLinks[i].TravelTime;
                switch (Strategy)
                {
                    case RoutingStrategy.Fastest:
                        FirstPath = new Route(EndLinks[i], time, distance, time, Range);
                        break;
                    case RoutingStrategy.Shortest:
                        FirstPath = new Route(EndLinks[i], distance, distance, time, Range);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var estimate = Estimate(EndLinks[i].End, Start, End);
                BackwardOpenSet.Push(estimate + FirstPath.TotalCost, FirstPath);
            }
        }

        protected abstract ConcurrentDictionary<long, Route> ForwardClosedSet { get; set; }
        protected abstract ConcurrentDictionary<long, Route> BackwardClosedSet { get; set; }
        protected abstract PriorityHeap<double, Route> ForwardOpenSet { get; set; }
        protected abstract PriorityHeap<double, Route> BackwardOpenSet { get; set; }
        protected abstract Stopwatch stopWatch { get; set; }
        protected abstract uint BestForwardCategory { get; set; }
        protected abstract uint BestBackwardCategory { get; set; }
        protected abstract LatLon End { get; set; }
        protected abstract LatLon Start { get; set; }
        protected abstract RoutingStrategy Strategy { get; set; }
        protected abstract VehicleType VehicleType { get; set; }
        protected abstract SpeedProfile Profile { get; set; }
    }
}
