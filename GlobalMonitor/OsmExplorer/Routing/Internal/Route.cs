using System.Collections;
using System.Collections.Generic;

namespace OsmExplorer.Routing.Internal
{
    internal sealed class Route : IEnumerable<RouteLink>
    {
        private Route(
            RouteLink lastLink,
            Route previousLinks,
            double totalCost,
            double totalDistance,
            double totalTravelTime,
            int count)
        {
            this.LastLink = lastLink;
            this.PreviousLinks = previousLinks;
            this.TotalCost = totalCost;
            this.TotalDistance = totalDistance;
            this.TotalTravelTime = totalTravelTime;
            this.Count = count;
            this.BestCategory = CategoryHueristic.GroupedFunction(lastLink.RoadCategory, totalDistance);
        }

        public Route(RouteLink start, double Cost, double Distance, double TravelTime, double range)
            : this(start, null, Cost, Distance, TravelTime, 1) { }

        public Route AddLink(RouteLink rlink, double stepCost)
        {
            return new Route(
                rlink,
                this,
                this.TotalCost + stepCost,
                this.TotalDistance + rlink.Flags.TravelDistance.Meters,
                this.TotalTravelTime + rlink.TravelTime,
                ++this.Count);
        }
        public IEnumerator<RouteLink> GetEnumerator()
        {
            for (Route p = this; p != null; p = p.PreviousLinks)
                yield return p.LastLink;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public RouteLink LastLink { get; private set; }
        public Route PreviousLinks { get; private set; }
        public double TotalCost { get; private set; }
        public double TotalDistance { get; private set; }
        public double TotalTravelTime { get; private set; }
        public int Count { get; private set; }
        public uint BestCategory { get; private set; }
    }
}
