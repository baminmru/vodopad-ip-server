using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using OsmExplorer.Spatial;
using Volante;
using OsmExplorer.Units;

namespace OsmExplorer.Data.Internal
{
    internal static class DataInterpreter
    {
        public static char AssignTrafficControl(byte[] array)
        {
            switch (array[0] ^ array[1])
            {
                case 0:
                    return 'N';
                case 1:
                    return 'F';
                case 2:
                    return 'L';
                case 3:
                    return 'B';
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static TravelDirection AssignTravelDirection(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("oneway", out value))
            {
                switch (value.Get())
                {
                    case "yes":
                    case "Yes":
                    case "YES":
                    case "true":
                    case "1":
                        return TravelDirection.From;
                    case "-1":
                        return TravelDirection.To;
                    case "no":
                    case "No":
                    case "NO":
                    case "false":
                    case "0":
                    default:
                        return TravelDirection.Both;
                }
            }
            else
                return TravelDirection.Both;
        }
        public static string[] GetNames(Dictionary<string, PersistentString> Tags)
        {
            List<string> Names = new List<string>();
            PersistentString value;
            if (Tags.TryGetValue("name", out value))
                Names.Add(value.Get());

            if (Tags.TryGetValue("ref", out value))
                Names.Add(value.Get());

            return Names.ToArray();
        }
        public static RoadCategory GetRoadCategory(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("highway", out value))
            {
                switch (value.Get())
                {
                    case "motorway":
                        return RoadCategory.motorway;
                    case "motorway_link":
                        return RoadCategory.motorway_link;
                    case "trunk":
                        return RoadCategory.trunk;
                    case "trunk_link":
                        return RoadCategory.trunk_link;
                    case "primary":
                        return RoadCategory.primary;
                    case "primary_link":
                        return RoadCategory.primary_link;
                    case "secondary":
                        return RoadCategory.secondary;
                    case "secondary_link":
                        return RoadCategory.secondary_link;
                    case "tertiary":
                        return RoadCategory.tertiary;
                    case "tertiary_link":
                        return RoadCategory.tertiary_link;
                    case "residential":
                        return RoadCategory.residential;
                    case "service":
                    default:
                        return RoadCategory.service;
                }
            }
            else
                return RoadCategory.service;
        }
        public static ushort GetMaxHeight(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (!Tags.TryGetValue("maxheight:physical", out value))
            {
                if (!Tags.TryGetValue("maxheight", out value))
                    return ushort.MaxValue;
            }
            string str = value.Get();
            try
            {
                double height = Convert.ToDouble(str);
                if (height <= 5F)
                    return (ushort)(height * 100);
                else
                    return (ushort)(height * 100D / 3.2808399D);
            }
            catch (FormatException)
            {
                try
                {
                    string[] split;
                    if (str.Contains("\'") || str.Contains("\""))
                    {
                        split = Regex.Split(str, "(\")|(\')");
                        if (split[2] == string.Empty)
                        {
                            return (ushort)(Convert.ToDouble(split[0]) * 100D / 3.2808399D);
                        }
                        else
                        {
                            return (ushort)((Convert.ToDouble(split[0]) + Convert.ToDouble(split[2]) / 12F) * 100D / 3.2808399D);
                        }
                    }
                    else if (str.Contains("ft") && !str.Contains("in"))
                    {
                        split = Regex.Split(str, "(ft)");
                        return (ushort)(Convert.ToDouble(split[0]) * 100D / 3.2808399D);
                    }
                    else if (str.Contains("ft") && str.Contains("in"))
                    {
                        split = Regex.Split(str, "(ft)|(in)");
                        return (ushort)((Convert.ToDouble(split[0]) + Convert.ToDouble(split[2]) / 12D) * 100D / 3.2808399D);
                    }
                    else if (str.Contains("inches") && !str.Contains("feet"))
                    {
                        split = Regex.Split(str, "inches");
                        return (ushort)((Convert.ToDouble(split[0]) / 12D) * 100 / 3.2808399D);
                    }
                    else if (str.Contains("m"))
                    {
                        split = Regex.Split(str, "m");
                        return (ushort)(Convert.ToDouble(split[0]) * 100D);
                    }
                    else if (str.Contains("meter"))
                    {
                        split = Regex.Split(str, "meter");
                        return (ushort)(Convert.ToDouble(split[0]) * 100D);
                    }
                    else if (str.Contains("meters"))
                    {
                        split = Regex.Split(str, "meters");
                        return (ushort)(Convert.ToDouble(split[0]) * 100D);
                    }
                    else
                    {
                        return ushort.MaxValue;
                    }
                }
                catch (FormatException)
                {
                    return ushort.MaxValue;
                }
                catch (IndexOutOfRangeException)
                {
                    return ushort.MaxValue;
                }
            }
        }
        public static ushort GetMaxWidth(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("maxwidth", out value))
            {
                string str = value.Get();
                try
                {
                    return (ushort)(Convert.ToDouble(str) * 100D);
                }
                catch (FormatException)
                {
                    try
                    {
                        string[] split;
                        if (str.Contains("\'") || str.Contains("\""))
                        {
                            split = Regex.Split(str, "(\")|(\')");
                            if (split[2] == string.Empty)
                            {
                                return (ushort)(Convert.ToDouble(split[0]) * 100D / 3.2808399D);
                            }
                            else
                            {
                                return (ushort)((Convert.ToDouble(split[0]) + Convert.ToDouble(split[2]) / 12D) * 100D / 3.2808399D);
                            }
                        }
                        else if (str.Contains("ft") && !str.Contains("in"))
                        {
                            split = Regex.Split(str, "(ft)");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("ft") && str.Contains("in"))
                        {
                            split = Regex.Split(str, "(ft)|(in)");
                            return (ushort)((Convert.ToDouble(split[0]) + Convert.ToDouble(split[2]) / 12D) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("inches") && !str.Contains("feet"))
                        {
                            split = Regex.Split(str, "inches");
                            return (ushort)((Convert.ToDouble(split[0]) / 12D) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("feet"))
                        {
                            split = Regex.Split(str, "(feet)");
                            return (ushort)((Convert.ToDouble(split[0])) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("m"))
                        {
                            split = Regex.Split(str, "m");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D);
                        }
                        else if (str.Contains("meter"))
                        {
                            split = Regex.Split(str, "meter");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D);
                        }
                        else if (str.Contains("meters"))
                        {
                            split = Regex.Split(str, "meters");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D);
                        }
                        else
                        {
                            return ushort.MaxValue;
                        }
                    }
                    catch (FormatException)
                    {
                        return ushort.MaxValue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return ushort.MaxValue;
                    }
                }
            }
            else
                return ushort.MaxValue;
        }
        public static ushort GetMaxLength(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("maxlength", out value))
            {
                string str = value.Get();
                try
                {
                    return (ushort)(Convert.ToDouble(str) * 100D);
                }
                catch (FormatException)
                {
                    try
                    {
                        string[] split;
                        if (str.Contains("\'") || str.Contains("\""))
                        {
                            split = Regex.Split(str, "(\")|(\')");
                            if (split[2] == string.Empty)
                            {
                                return (ushort)(Convert.ToDouble(split[0]) * 100D / 3.2808399D);
                            }
                            else
                            {
                                return (ushort)((Convert.ToDouble(split[0]) + Convert.ToDouble(split[2]) / 12D) * 100D / 3.2808399D);
                            }
                        }
                        else if (str.Contains("ft") && !str.Contains("in"))
                        {
                            split = Regex.Split(str, "(ft)");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("ft") && str.Contains("in"))
                        {
                            split = Regex.Split(str, "(ft)|(in)");
                            return (ushort)((Convert.ToDouble(split[0]) + Convert.ToDouble(split[2]) / 12D) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("inches") && !str.Contains("feet"))
                        {
                            split = Regex.Split(str, "inches");
                            return (ushort)((Convert.ToDouble(split[0]) / 12D) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("feet"))
                        {
                            split = Regex.Split(str, "(feet)");
                            return (ushort)((Convert.ToDouble(split[0])) * 100D / 3.2808399D);
                        }
                        else if (str.Contains("m"))
                        {
                            split = Regex.Split(str, "m");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D);
                        }
                        else if (str.Contains("meter"))
                        {
                            split = Regex.Split(str, "meter");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D);
                        }
                        else if (str.Contains("meters"))
                        {
                            split = Regex.Split(str, "meters");
                            return (ushort)(Convert.ToDouble(split[0]) * 100D);
                        }
                        else
                        {
                            return ushort.MaxValue;
                        }
                    }
                    catch (FormatException)
                    {
                        return ushort.MaxValue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return ushort.MaxValue;
                    }
                }
            }
            else
                return ushort.MaxValue;
        }
        public static ushort GetMaxWeight(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("maxweight", out value))
            {
                string str = value.Get();
                try
                {
                    float flt = Convert.ToSingle(str);
                    if (flt > 500)
                        return Convert.ToUInt16(new Weight(flt, WeightUnits.Pounds).Kilograms);
                    else
                        return Convert.ToUInt16(new Weight(flt, WeightUnits.ShortTons).Kilograms);
                }
                catch (FormatException)
                {
                    try
                    {
                        string[] split;
                        if (str.Contains("lbs"))
                        {
                            if (str.Contains("single:") && str.Contains("double:"))
                            {
                                split = Regex.Split(str, "(lbs)|(single:)|(double:)");
                                List<float> flts = new List<float>();
                                for (int i = 0; i < split.Count(); i++)
                                {
                                    try
                                    {
                                        float flt = Convert.ToSingle(split[i]);
                                        flts.Add(flt);
                                    }
                                    catch (FormatException)
                                    {
                                        continue;
                                    }
                                }
                                if (flts.Count == 0)
                                    return ushort.MaxValue;
                                else
                                    return Convert.ToUInt16(new Weight(flts.Max(), WeightUnits.Pounds).Kilograms);
                            }
                            else
                            {
                                split = Regex.Split(str, "lbs");
                                return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.Pounds).Kilograms);
                            }
                        }
                        else if (str.Contains("lb"))
                        {
                            split = Regex.Split(str, "lb");
                            return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.Pounds).Kilograms);
                        }
                        else if (str.Contains("tons"))
                        {
                            split = Regex.Split(str, "tons");
                            return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.ShortTons).Kilograms);
                        }
                        else if (str.Contains("ton"))
                        {
                            split = Regex.Split(str, "ton");
                            return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.ShortTons).Kilograms);
                        }
                        else if (str.Contains("US ton"))
                        {
                            split = Regex.Split(str, "US ton");
                            return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.ShortTons).Kilograms);
                        }
                        else if (str.Contains("US tons"))
                        {
                            split = Regex.Split(str, "US tons");
                            return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.ShortTons).Kilograms);
                        }
                        else if (str.Contains("tonnes"))
                        {
                            split = Regex.Split(str, "tonnes");
                            return Convert.ToUInt16(new Weight(Convert.ToSingle(split[0]), WeightUnits.MetricTons).Kilograms);
                        }
                        else
                        {
                            return ushort.MaxValue;
                        }
                    }
                    catch (FormatException)
                    {
                        return ushort.MaxValue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return ushort.MaxValue;
                    }
                }
            }
            else
                return ushort.MaxValue;
        }
        public static bool AssignTollway(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("toll", out value))
            {
                switch (value.Get())
                {
                    case "yes":
                    case "Yes":
                    case "YES":
                    case "y":
                    case "Y":
                    case "true":
                    case "1":
                        return true;
                    case "-1":
                    case "n":
                    case "N":
                    case "no":
                    case "No":
                    case "NO":
                    default:
                        return false;
                }
            }
            else
                return false;
        }
        public static bool AssignTunnel(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("tunnel", out value))
            {
                switch (value.Get())
                {
                    case "yes":
                    case "Yes":
                    case "YES":
                    case "y":
                    case "Y":
                    case "true":
                    case "1":
                        return true;
                    case "-1":
                    case "n":
                    case "N":
                    case "no":
                    case "No":
                    case "NO":
                    default:
                        return false;
                }
            }
            else
                return false;
        }
        public static bool AssignBridge(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("bridge", out value))
            {
                switch (value.Get())
                {
                    case "yes":
                    case "Yes":
                    case "YES":
                    case "y":
                    case "Y":
                    case "true":
                    case "1":
                        return true;
                    case "-1":
                    case "n":
                    case "N":
                    case "no":
                    case "No":
                    case "NO":
                    default:
                        return false;
                }
            }
            else
                return false;
        }
        public static bool AssignRoundabout(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("junction", out value))
                return value.Get() == "roundabout";
            return false;
        }
        public static bool AssignAccess(Dictionary<string, PersistentString> Tags, string AccessType)
        {
            PersistentString value;
            if (Tags.TryGetValue("access", out value))
                return value.Get() == AccessType;
            return false;
        }
        public static bool AssignVehicleAccess(Dictionary<string, PersistentString> Tags, string Vehicle)
        {
            PersistentString value;
            if (Tags.TryGetValue(Vehicle, out value))
            {
                switch (value.Get())
                {
                    case "yes":
                    case "Yes":
                    case "YES":
                    case "y":
                    case "Y":
                    case "true":
                    case "1":
                    default:
                        return true;
                    case "-1":
                    case "n":
                    case "N":
                    case "no":
                    case "No":
                    case "NO":
                        return false;
                }
            }
            else
                return true;
        }
        public static Speed AssignTravelSpeed(Dictionary<string, string> Tags, SpeedUnits DefaultUnits)
        {
            string speedTag;
            if (Tags.TryGetValue("maxspeed", out speedTag))
            {
                double speedkph;
                string[] speed = Regex.Split(speedTag, " ");
                if (speed.Count() > 1)
                {
                    switch (speed[1])
                    {
                        case "mph":
                        case "MPH":
                        case "Mph":
                            return new Speed(Convert.ToDouble(speed[0]), SpeedUnits.MPH);
                        case "kph":
                        case "KPH":
                        case "Kph":
                            return new Speed(Convert.ToDouble(speed[0]), SpeedUnits.KPH);
                        default:
                            return new Speed(Convert.ToDouble(speed[0]), DefaultUnits);
                    }
                }
                else
                {
                    string units;
                    string spd;
                    switch (speed[0].Length)
                    {
                        case 1:
                        case 2:
                        case 3:
                            units = "mph";
                            spd = speed[0];
                            break;
                        case 4:
                            units = speed[0].Substring(1, 3);
                            spd = speed[0].Substring(0, 1);
                            break;
                        case 5:
                            units = speed[0].Substring(2, 3);
                            spd = speed[0].Substring(0, 2);
                            break;
                        case 6:
                            units = speed[0].Substring(3, 3);
                            spd = speed[0].Substring(0, 3);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    switch (units)
                    {
                        case "mph":
                        case "MPH":
                        case "Mph":
                            return new Speed(Convert.ToDouble(spd), SpeedUnits.MPH);
                        case "kph":
                        case "KPH":
                        case "Kph":
                            return new Speed(Convert.ToDouble(spd), SpeedUnits.KPH);
                        default:
                            return new Speed(Convert.ToDouble(spd), DefaultUnits);
                    }
                }
            }
            else
                return Speed.ZeroSpeed;
        }
        public static int CategoryNdx(Dictionary<string, PersistentString> Tags)
        {
            PersistentString value;
            if (Tags.TryGetValue("highway", out value))
            {
                switch (value.Get())
                {
                    case "motorway":
                        return 0;
                    case "motorway_link":
                        return 1;
                    case "trunk":
                        return 2;
                    case "trunk_link":
                        return 3;
                    case "primary":
                        return 4;
                    case "primary_link":
                        return 5;
                    case "secondary":
                        return 6;
                    case "secondary_link":
                        return 7;
                    case "tertiary":
                        return 8;
                    case "tertiary_link":
                        return 9;
                    case "residential":
                        return 10;
                    case "service":
                    default:
                        return 11;
                }
            }
            return 11;
        }
    }
}
