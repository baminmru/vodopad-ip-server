using System;
using OsmExplorer.Data;

namespace OsmExplorer.Routing.Internal
{
    internal static class CategoryHueristic
    {
        public static double RangeParameter = 2000;

        public static uint GroupedFunction(RoadCategory category, double distance)
        {
            if (distance > RangeParameter) 
            {
                switch (category)
                {
                    case RoadCategory.motorway:
                    case RoadCategory.motorway_link:
                        return 2;
                    case RoadCategory.trunk:
                    case RoadCategory.trunk_link:
                        return 4;
                    case RoadCategory.primary:
                    case RoadCategory.primary_link:
                        return 6;
                    case RoadCategory.secondary:
                    case RoadCategory.secondary_link:
                        return 8;
                    case RoadCategory.tertiary:
                    case RoadCategory.tertiary_link:
                        return 10;
                    case RoadCategory.residential:
                        return 10;
                    case RoadCategory.service:
                        return 11;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return 11;
        }
        public static uint MonotoneFunction(RoadCategory category, double distance) 
        {
            if (distance > RangeParameter)
            {
                switch (category)
                {
                    case RoadCategory.motorway:
                        return 0;
                    case RoadCategory.motorway_link:
                        return 1;
                    case RoadCategory.trunk:
                        return 2;
                    case RoadCategory.trunk_link:
                        return 3;
                    case RoadCategory.primary:
                        return 4;
                    case RoadCategory.primary_link:
                        return 5;
                    case RoadCategory.secondary:
                        return 6;
                    case RoadCategory.secondary_link:
                        return 7;
                    case RoadCategory.tertiary:
                        return 8;
                    case RoadCategory.tertiary_link:
                        return 9;
                    case RoadCategory.residential:
                        return 10;
                    case RoadCategory.service:
                        return 11;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return 11;
        }
        public static uint GroupedMonotone(RoadCategory category, double distance) 
        {
            if (distance > RangeParameter)
            {
                switch (category)
                {
                    case RoadCategory.motorway:
                    case RoadCategory.motorway_link:
                        return 1;
                    case RoadCategory.trunk:
                    case RoadCategory.trunk_link:
                        return 3;
                    case RoadCategory.primary:
                    case RoadCategory.primary_link:
                        return 5;
                    case RoadCategory.secondary:
                    case RoadCategory.secondary_link:
                        return 7;
                    case RoadCategory.tertiary:
                    case RoadCategory.tertiary_link:
                        return 9;
                    case RoadCategory.residential:
                        return 10;
                    case RoadCategory.service:
                        return 11;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return 11;
        }
    }
}
