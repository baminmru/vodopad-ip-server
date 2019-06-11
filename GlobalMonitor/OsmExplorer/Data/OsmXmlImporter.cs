using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using OsmExplorer.Data.Internal;
using OsmExplorer.Data.Internal.Primitives;
using OsmExplorer.Functions;
using OsmExplorer.Routing;
using OsmExplorer.Routing.Internal;
using OsmExplorer.Spatial;
using OsmExplorer.Units;
using Volante;

namespace OsmExplorer.Data
{
    /// <summary>
    /// Class for importing OpenStreetMap XML files (.osm) into a spatial database
    /// suitable for spatial queries and routing.
    /// </summary>
    public class OsmXmlImporter
    {
        #region Private 
        private SpeedUnits m_DefaultUnits;
        private XmlImportProgress m_Progress;
        private XmlReader m_Reader;
        private long m_FileLength;
        private long m_ReaderProgress;
        private int m_ProgressPercentage 
        {
            get 
            {
                if (m_FileLength > 0)
                    return (int)((decimal)m_ReaderProgress / (decimal)m_FileLength * 100m);
                return 0;
            }
        }
        private uint m_Nodes;
        private uint m_Ways;
        private uint m_RoadLinks;
        private double[] m_AverageSpeedsByCategory;
        private FileInfo m_XmlPath;
        private DirectoryInfo m_OutputDirectory;
        private string m_FileName;
        private IDatabase m_PrimitiveDb;
        private IDatabase m_Db;
        private PrimitiveDataRoot m_PrimitiveRoot;
        private DatabaseRoot m_DbRoot;
        private Dictionary<long, HashSet<long>> m_ProhibitedFrom;
        private Dictionary<long, HashSet<long>> m_ProhibitedTo;
        private Dictionary<long, List<long>> m_StopSignsRef;
        private BoundingBox m_bbox;
        
        private PersistentAccessRestriction GetAccessRestriction(Dictionary<string, PersistentString> Tags, List<Node> nodes, RestrictionType rType)
        {
            PersistentAccessRestriction Restriction;
            bool bRestriction;
            switch (rType)
            {
                case RestrictionType.Barricade:
                    bRestriction = nodes.Any(x => Array.ConvertAll(x.Tags.ToArray(), y => y.Get()).Any(z => z == "gate"));
                    break;
                case RestrictionType.BicycleAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "bicycle");
                    break;
                case RestrictionType.Bridge:
                    bRestriction = DataInterpreter.AssignBridge(Tags);
                    break;
                case RestrictionType.DeliveryAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "goods");
                    break;
                case RestrictionType.EmergencyAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "emergency");
                    break;
                case RestrictionType.HazmatAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "hazmat");
                    break;
                case RestrictionType.MopedAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "moped");
                    break;
                case RestrictionType.MotorVehicleAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "motor_vehicle");
                    break;
                case RestrictionType.PermissiveAccess:
                    bRestriction = DataInterpreter.AssignAccess(Tags, "permissive");
                    break;
                case RestrictionType.PrivateAccess:
                    bRestriction = DataInterpreter.AssignAccess(Tags, "private");
                    break;
                case RestrictionType.Roundabout:
                    bRestriction = DataInterpreter.AssignRoundabout(Tags);
                    break;
                case RestrictionType.Tollway:
                    bRestriction = DataInterpreter.AssignTollway(Tags);
                    break;
                case RestrictionType.TruckAccess:
                    bRestriction = DataInterpreter.AssignVehicleAccess(Tags, "hgv");
                    break;
                case RestrictionType.Tunnel:
                    bRestriction = DataInterpreter.AssignTunnel(Tags);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Restriction = new PersistentAccessRestriction(bRestriction, rType);
            if (!m_DbRoot.RestrictionsNdx.Put(Restriction.Id, Restriction))
            {
                int key = Restriction.Id;
                Restriction = m_DbRoot.RestrictionsNdx.Get(key);
            }
            return Restriction;
        }
        private PersistentLength GetLength(IEnumerable<PersistentLatLon> points) 
        {
            PersistentLength distance = new PersistentLength((uint)MathFunctions.Distance(Array.ConvertAll(points.ToArray(), pt => (LatLon)pt)).Meters);
            if (!m_DbRoot.LengthNdx.Put(distance.Distance, distance))
            {
                uint key = distance.Distance;
                distance = m_DbRoot.LengthNdx.Get(key);
            }
            return distance;
        }
        private PersistentSpeed GetSpeed(Way way) 
        {
            PersistentSpeed speed;
            var Tags = TagsToDictionary(way);
            if (way.TravelSpeed == Speed.ZeroSpeed)
                speed = new PersistentSpeed((byte)m_AverageSpeedsByCategory[DataInterpreter.CategoryNdx(Tags)]);
            else
                speed = new PersistentSpeed((byte)way.TravelSpeed.SpeedValue(m_DefaultUnits));

            if (!m_DbRoot.SpeedNdx.Put(speed.SpeedValue, speed))
            {
                byte key = speed.SpeedValue;
                speed = m_DbRoot.SpeedNdx.Get(key);
            }
            return speed;
        }
        private PersistentTrafficControl GetTrafficControls(byte[] TrafficSignalPoints, byte[] StopSignPoints) 
        {
            char stopSign = DataInterpreter.AssignTrafficControl(StopSignPoints);
            char trafficSignal = DataInterpreter.AssignTrafficControl(TrafficSignalPoints);
            PersistentTrafficControl control = new PersistentTrafficControl(new char[] { stopSign, trafficSignal });

            if (!m_DbRoot.TrafficControlNdx.Put(control.Id, control))
            {
                int controlId = control.Id;
                control = m_DbRoot.TrafficControlNdx.Get(controlId);
            }
            return control;
        }
        private HeadingCollection GetHeadings(List<PersistentLatLon> Points) 
        {
            short[] HeadingArray = new short[4];
            HeadingArray[0] = (short)MathFunctions.GetHeading(Points[0], Points[1]);
            HeadingArray[1] = (short)MathFunctions.GetHeading(Points[1], Points[0]);
            HeadingArray[2] = (short)MathFunctions.GetHeading(Points[Points.Count() - 1], Points[Points.Count() - 2]);
            HeadingArray[3] = (short)MathFunctions.GetHeading(Points[Points.Count() - 2], Points[Points.Count() - 1]);

            HeadingCollection hCollection = new HeadingCollection(HeadingArray);
            if (!m_DbRoot.HeadingNdx.Put(hCollection.Id, hCollection))
            {
                long hId = hCollection.Id;
                hCollection = m_DbRoot.HeadingNdx.Get(hId);
            }
            return hCollection;
        }
        private IPArray<PersistentLatLon> PointsToIPArray(IEnumerable<PersistentLatLon> points) 
        {
            IPArray<PersistentLatLon> IPoints = m_Db.CreateArray<PersistentLatLon>();
            foreach (var ll in points)
            {
                if (m_DbRoot.LatLonNdx.Put(ll.Id, ll))
                    IPoints.Add(ll);
                else
                    IPoints.Add(m_DbRoot.LatLonNdx.Get(ll.Id));
            }
            return IPoints;
        }
        private RoadDimensions GetRoadDimensions(Dictionary<string, PersistentString> Tags) 
        {
            ushort weight = DataInterpreter.GetMaxWeight(Tags);
            ushort length = DataInterpreter.GetMaxLength(Tags);
            ushort width = DataInterpreter.GetMaxWidth(Tags);
            ushort height = DataInterpreter.GetMaxHeight(Tags);
            RoadDimensions dimensions = new RoadDimensions(height, length, width, weight);

            if (!m_DbRoot.DimensionsNdx.Put(dimensions.Key, dimensions))
            {
                ulong dimId = dimensions.Key;
                dimensions = m_DbRoot.DimensionsNdx.Get(dimId);
            }
            return dimensions;
        }
        private IPArray<PersistentString> GetNameArray(Dictionary<string, PersistentString> Tags) 
        {
            string[] names = DataInterpreter.GetNames(Tags);
            IPArray<PersistentString> NameArray = m_Db.CreateArray<PersistentString>();
            foreach (var name in names)
            {
                PersistentString persistentName = m_DbRoot.NameNdx.Get(name);
                if (persistentName == null)
                {
                    persistentName = new PersistentString(name);
                    m_DbRoot.NameNdx.Put(name, persistentName);
                }
                NameArray.Add(persistentName);
            }
            return NameArray;
        }
        private PersistentRoadCategory GetRoadCategory(Dictionary<string, PersistentString> Tags) 
        {
            PersistentRoadCategory category = new PersistentRoadCategory(DataInterpreter.GetRoadCategory(Tags));
            if (!m_DbRoot.CategoryNdx.Put((byte)category.Value, category))
            {
                byte key = (byte)category.Value;
                category = m_DbRoot.CategoryNdx.Get(key);
            }
            return category;
        }
        private PersistentTravelDirection GetTravelDirection(Dictionary<string, PersistentString> Tags) 
        {
            PersistentTravelDirection direction = new PersistentTravelDirection(DataInterpreter.AssignTravelDirection(Tags));
            if (!m_DbRoot.DirectionNdx.Put(direction.Id, direction))
            {
                int key = direction.Id;
                direction = m_DbRoot.DirectionNdx.Get(key);
            }
            return direction;
        }
        private PersistentWayId GetWayId(Way way) 
        {
            PersistentWayId wayId = new PersistentWayId(way.WayId);
            if (!m_DbRoot.WayIdNdx.Put(wayId.WayId, wayId))
            {
                long key = wayId.WayId;
                wayId = m_DbRoot.WayIdNdx.Get(key);
            }
            return wayId;
        }
        private RoadFlags GetFlags(Way way, List<PersistentLatLon> Points, List<Node> splitway) 
        {
            var Tags = TagsToDictionary(way);
            byte[] TrafficSignalPoints = new byte[2] { 0, 0 };
            byte[] StopSignPoints = new byte[2] { 0, 0 };
            HashSet<long> ProhibitedFrom = new HashSet<long>();
            HashSet<long> ProhibitedTo = new HashSet<long>();
            List<Node> nodes = new List<Node>();

            for (int i = 0; i < splitway.Count(); i++)
            {
                HashSet<long> ProhibitedWay;
                if (m_ProhibitedFrom.TryGetValue(splitway[i].NodeId, out ProhibitedWay))
                {
                    foreach (var wayId in ProhibitedWay)
                        ProhibitedFrom.Add(wayId);
                }
                if (m_ProhibitedTo.TryGetValue(splitway[i].NodeId, out ProhibitedWay))
                {
                    foreach (var wayId in ProhibitedWay)
                        ProhibitedTo.Add(wayId);
                }
                var node = m_PrimitiveRoot.NodeNdx.Get(splitway[i].NodeId);
                if (node.Lat == 0 || node.Lon == 0)
                    continue;
                nodes.Add(node);
                if (i == 0)
                {
                    PersistentString[] tags = node.Tags.Get("highway", "highway");
                    foreach (var tag in tags)
                    {
                        if (tag != null)
                        {
                            switch (tag.Get())
                            {
                                case "stop":
                                    StopSignPoints[0] = 1;
                                    break;
                                case "traffic_signals":
                                    TrafficSignalPoints[0] = 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                if (i == splitway.Count() - 1)
                {
                    PersistentString[] tags = node.Tags.Get("highway", "highway");
                    foreach (var tag in tags)
                    {
                        if (tag != null)
                        {
                            switch (tag.Get())
                            {
                                case "stop":
                                    StopSignPoints[1] = 1;
                                    break;
                                case "traffic_signals":
                                    TrafficSignalPoints[1] = 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            IPArray<IPersistent> Items = m_Db.CreateArray<IPersistent>(17);

            Items.Add(GetRoadDimensions(Tags));
            Items.Add(GetHeadings(Points));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.Barricade));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.BicycleAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.Bridge));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.DeliveryAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.EmergencyAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.HazmatAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.MopedAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.MotorVehicleAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.PermissiveAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.PrivateAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.Roundabout));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.Tollway));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.TruckAccess));
            Items.Add(GetAccessRestriction(Tags, nodes, RestrictionType.Tunnel));
            Items.Add(GetLength(Points));
            Items.Add(GetSpeed(way));
            Items.Add(GetTrafficControls(TrafficSignalPoints, StopSignPoints));

            IPArray<PersistentWayId> ProhibitedFromArray = m_Db.CreateArray<PersistentWayId>();
            IPArray<PersistentWayId> ProhibitedToArray = m_Db.CreateArray<PersistentWayId>();
            foreach (var wayId in ProhibitedFrom)
            {
                var pWayId = new PersistentWayId(wayId);
                if (!m_DbRoot.WayIdNdx.Put(wayId, pWayId))
                    pWayId = m_DbRoot.WayIdNdx.Get(wayId);

                ProhibitedFromArray.Add(pWayId);
            }
            foreach (var wayId in ProhibitedTo)
            {
                var pWayId = new PersistentWayId(wayId);
                if (!m_DbRoot.WayIdNdx.Put(wayId, pWayId))
                    pWayId = m_DbRoot.WayIdNdx.Get(wayId);

                ProhibitedToArray.Add(pWayId);
            }

            RoadFlags flags = new RoadFlags(
                Items,
                ProhibitedFromArray,
                ProhibitedToArray);

            return flags;
        }
        private List<long>[] ParseWay(Way way) 
        {
            List<List<long>> PointLists = new List<List<long>>();
            var subList = new List<long>();
            int vertexCount = 0;

            for (int i = 0; i < way.NodeIds.Count(); i++)
            {
                subList.Add(way.NodeIds[i]);
                var node = m_PrimitiveRoot.NodeNdx.Get(way.NodeIds[i]);
                if (i == 0 || i == way.NodeIds.Count() - 1 || m_PrimitiveRoot.VertexSet.Contains(node))
                    vertexCount++;

                if (vertexCount == 2)
                {
                    PointLists.Add(subList);
                    subList = new List<long>() { way.NodeIds[i] };
                    vertexCount = 1;
                }
            }
            return PointLists.ToArray();
        }
        private Dictionary<string, PersistentString> TagsToDictionary(Way way) 
        {
            Dictionary<string, PersistentString> Tags = new Dictionary<string, PersistentString>();
            var enumerator = way.Tags.GetDictionaryEnumerator();

            while (enumerator.MoveNext())
            {
                var key = enumerator.Key as string;
                var value = enumerator.Value as PersistentString;
                Tags.Add(key, value);
            }
            return Tags;
        }
        private void PopulateSpeedTable() 
        {
            uint[] Counter = new uint[12];
            List<Speed>[] Speeds = new List<Speed>[12];
            for (int i = 0; i < 12; i++) 
            {
                Speeds[i] = new List<Speed>(100000);
            }
            m_AverageSpeedsByCategory = new double[12];

            foreach (var way in m_PrimitiveRoot.WayNdx)
            {
                if (way.TravelSpeed > Speed.ZeroSpeed)
                {
                    switch (way.HighwayTag)
                    {
                        case "motorway":
                            Speeds[0].Add(way.TravelSpeed);
                            break;
                        case "motorway_link":
                            Speeds[1].Add(way.TravelSpeed);
                            break;
                        case "trunk":
                            Speeds[2].Add(way.TravelSpeed);
                            break;
                        case "trunk_link":
                            Speeds[3].Add(way.TravelSpeed);
                            break;
                        case "primary":
                            Speeds[4].Add(way.TravelSpeed);
                            break;
                        case "primary_link":
                            Speeds[5].Add(way.TravelSpeed);
                            break;
                        case "secondary":
                            Speeds[6].Add(way.TravelSpeed);
                            break;
                        case "secondary_link":
                            Speeds[7].Add(way.TravelSpeed);
                            break;
                        case "tertiary":
                            Speeds[8].Add(way.TravelSpeed);
                            break;
                        case "tertiary_link":
                            Speeds[9].Add(way.TravelSpeed);
                            break;
                        case "residential":
                            Speeds[10].Add(way.TravelSpeed);
                            break;
                        case "service":
                        default:
                            Speeds[11].Add(way.TravelSpeed);
                            break;
                    }
                }
            }
            for (int i = 0; i < Speeds.Count(); i++) 
            {
                if (Speeds[i].Count() > 0)
                    m_AverageSpeedsByCategory[i] = Speeds[i].Average(x => x.SpeedValue(m_DefaultUnits));
                else
                    m_AverageSpeedsByCategory[i] = 0;
            }
            for (int i = 0; i < Speeds.Count(); i++)
            {
                if (m_AverageSpeedsByCategory[i] == 0)
                {
                    if (i == 0)
                        m_AverageSpeedsByCategory[i] = m_AverageSpeedsByCategory[i + 1];
                    else if (i == Speeds.Count() - 1)
                        m_AverageSpeedsByCategory[i] = m_AverageSpeedsByCategory[i - 1];
                    else
                        m_AverageSpeedsByCategory[i] = (m_AverageSpeedsByCategory[i - 1] + m_AverageSpeedsByCategory[i + 1]) / 2D;
                }
            }
        }

        private long GetFileLength() 
        {
            try
            {
                m_Reader = XmlReader.Create(m_XmlPath.FullName);
                long Counter = 0;
                m_Reader.MoveToContent();
                m_Reader.ReadStartElement("osm");
                m_Reader.Skip();
                if (m_Reader.Name == "bounds")
                {
                    m_Reader.ReadStartElement("bounds");
                    m_Reader.Skip();
                }
                if (m_Reader.Name == "bound")
                {
                    m_Reader.ReadStartElement("bound");
                    m_Reader.Skip();
                }
                while (m_Reader.IsStartElement())
                {
                    switch (m_Reader.Name)
                    {
                        case "node":
                            SkipNode();
                            break;
                        case "way":
                            SkipWay();
                            break;
                        case "relation":
                            SkipRelation();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    Counter++;
                }
                m_Reader = null;
                return Counter;
            }
            catch(XmlException) 
            {
                throw;
            }
        }
        private void ImportWays()
        {
            try
            {
                m_Reader = XmlReader.Create(m_XmlPath.FullName);
                m_Reader.MoveToContent();
                m_Reader.ReadStartElement("osm");
                m_Reader.Skip();
                if (m_Reader.Name == "bounds")
                {
                    m_Reader.ReadStartElement("bounds");
                    m_Reader.Skip();
                }
                if (m_Reader.Name == "bound")
                {
                    m_Reader.ReadStartElement("bound");
                    m_Reader.Skip();
                }
                while (m_Reader.IsStartElement())
                {
                    switch (m_Reader.Name)
                    {
                        case "node":
                            SkipNode();
                            break;
                        case "way":
                            ReadWay();
                            m_ReaderProgress++;
                            break;
                        case "relation":
                            ReadRelation();
                            m_ReaderProgress++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                m_PrimitiveDb.Commit();
                m_Reader = null;
            }
            catch (XmlException) 
            {
                throw;
            }
        }
        private void ImportNodes()
        {
            m_Reader = XmlReader.Create(m_XmlPath.FullName);
            m_Reader.MoveToContent();
            m_Reader.ReadStartElement("osm");
            m_Reader.Skip();

            if (m_Reader.Name == "bounds")
            {
                m_Reader.ReadStartElement("bounds");
                m_Reader.Skip();
            }
            if (m_Reader.Name == "bound")
            {
                m_Reader.ReadStartElement("bound");
                m_Reader.Skip();
            }
            while (m_Reader.IsStartElement())
            {
                switch (m_Reader.Name)
                {
                    case "node":
                        ReadNode();
                        m_ReaderProgress++;
                        break;
                    case "way":
                        SkipWay();
                        break;
                    case "relation":
                        SkipRelation();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            m_PrimitiveDb.Commit();
            m_Reader = null;

            var enumerator = m_PrimitiveRoot.ArcNdx.GetDictionaryEnumerator();

            while (enumerator.MoveNext()) 
            {
                var key = (long)enumerator.Key;
                var value = enumerator.Value as Volante.ISet<Way>;

                if (value.Count > 1) 
                {
                    var node = m_PrimitiveRoot.NodeNdx.Get(key);
                    m_PrimitiveRoot.VertexSet.Add(node);
                }
            }
            m_Reader = null;
        }
        private void CreateRoadLinks() 
        {
            //Assigns average speeds by category to ways not containing speed limit information.
            PopulateSpeedTable();

            ulong wayCount = 0;
            foreach (var way in m_PrimitiveRoot.WayNdx)
            {
                int linkSubNdx = 0;

                Dictionary<string, PersistentString> Tags = TagsToDictionary(way);
                List<long>[] SplitWays = ParseWay(way);

                foreach (var splitway in SplitWays)
                {
                    //Skip 1-node ways (data anomalies that sometimes exist).
                    if (splitway.Count < 2)
                        continue;

                    List<PersistentLatLon> Points = new List<PersistentLatLon>();
                    List<Node> nodes = new List<Node>();
                    for (int i = 0; i < splitway.Count(); i++)
                    {
                        var node = m_PrimitiveRoot.NodeNdx.Get(splitway[i]);
                        if (node.Lat == 0 || node.Lon == 0)
                            continue;
                        nodes.Add(node);
                        PersistentLatLon ll = node;
                        Points.Add(ll);
                    }
                    if (Points.Count < 2)
                        continue;

                    RoadFlags flags = GetFlags(way, Points, nodes);

                    IPArray<PersistentLatLon> IPoints = PointsToIPArray(Points);
                    IPArray<IPersistent> LinkItems = m_Db.CreateArray<IPersistent>(4);
                    IPArray<PersistentString> NameArray = GetNameArray(Tags);

                    LinkItems.Add(GetWayId(way));
                    LinkItems.Add(GetRoadCategory(Tags));
                    LinkItems.Add(flags);
                    LinkItems.Add(GetTravelDirection(Tags));

                    RoadLink rl = new RoadLink((ulong)(way.WayId * 100 + ++linkSubNdx), m_Db, IPoints, LinkItems, NameArray);

                    //Insert into spatial index using smallest rectange that encloses all points
                    m_DbRoot.SpatialNdx.Put(rl.Rectangle, rl);

                    m_RoadLinks++;
                    if (m_RoadLinks % 100 == 0) 
                    {
                        //Report import progress
                        int progress = (int)((decimal)wayCount / (decimal)m_Ways * 100m);
                        m_Progress.Invoke(progress, new ImportStatus(m_RoadLinks, ImportState.ImportingRoadLinks));
                    }
                    if (m_RoadLinks % 1000 == 0)
                    {
                        //Commit database changes to disk. Objects are kept in memory until written to disk.
                        m_Db.Commit();
                    }
                }
                wayCount++;
            }

            m_Db.Commit();
            m_PrimitiveDb.Close();
            m_ProhibitedFrom = null;
            m_ProhibitedTo = null;
        }

        private void ReadNode()
        {
            //Get coordinates and Id
            long id = Convert.ToInt64(m_Reader.GetAttribute("id"));
            float latitude = Convert.ToSingle(m_Reader.GetAttribute("lat"));
            float longtitude = Convert.ToSingle(m_Reader.GetAttribute("lon"));

            m_Reader.ReadStartElement();
            m_Reader.Skip();

            //Get the tags, if any.
            List<KeyValuePair<string, string>> Tags = new List<KeyValuePair<string, string>>();
            while (m_Reader.Name == "tag")
            {
                m_Reader.ReadStartElement();
                m_Reader.Skip();

                string key = m_Reader.GetAttribute("k");
                if (key != null)
                    Tags.Add(new KeyValuePair<string, string>(key, m_Reader.GetAttribute("v")));
            }
            
            Key nodeKey = new Key(id);
            if (m_PrimitiveRoot.NodeNdx.Get(nodeKey) == null)
            {
                if (m_Reader.NodeType == XmlNodeType.EndElement)
                    m_Reader.ReadEndElement();
                return;
            }

            if (m_Reader.NodeType == XmlNodeType.EndElement)
                m_Reader.ReadEndElement();

            m_Nodes++;
            var NewNode = new Node(id, latitude, longtitude, Tags, m_PrimitiveDb);
            m_PrimitiveRoot.NodeNdx.Set(NewNode.NodeId, NewNode);

            if (m_Nodes % 1000 == 0)
                m_Progress.Invoke(m_ProgressPercentage, new ImportStatus(m_Nodes, ImportState.ImportingNodes));
            if (m_Nodes % 20000 == 0)
                m_PrimitiveDb.Commit();
        }
        private void ReadWay()
        {
            //Get the way ID
            long WayId = Convert.ToInt64(m_Reader.GetAttribute("id"));
            m_Reader.ReadStartElement();
            m_Reader.Skip();

            //Get the a list of node IDs, ignoring duplicates in case any exist.
            //When the reader reads the list of nodes it will ignore those not belonging
            //to a way tagged as "highway".
            List<long> NodeIds = new List<long>();
            HashSet<long> NodeSet = new HashSet<long>();
            while (m_Reader.Name == "nd")
            {
                long nodeId = Convert.ToInt64(m_Reader.GetAttribute("ref"));
                if (NodeSet.Add(nodeId))
                    NodeIds.Add(nodeId);
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }

            //Get the tags 
            Dictionary<string, string> Tags = new Dictionary<string, string>();
            string key;
            while (m_Reader.Name == "tag")
            {
                key = m_Reader.GetAttribute("k");
                if (key != null)
                {
                    if (!Tags.ContainsKey(key))
                        Tags.Add(key, m_Reader.GetAttribute("v"));
                }
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }
            //If the way isn't tagged as a highway, then ignore it.
            if (!Tags.TryGetValue("highway", out key))
            {
                if (m_Reader.NodeType == XmlNodeType.EndElement)
                    m_Reader.ReadEndElement();
                return;
            }
            //Filter out highway-tagged ways that aren't vehicular (these could be
            //included, e.g. for pedestrian/bicycle routing but they can make the database
            //file enormous.
            switch (key) 
            {
                case "motorway":
                case "motorway_link":
                case "trunk":
                case "trunk_link":
                case "primary":
                case "primary_link":
                case "secondary":
                case "secondary_link":
                case "tertiary":
                case "tertiary_link":
                case "residential":
                case "service":
                case "living_street":
                    break;
                default:
                    if (m_Reader.NodeType == XmlNodeType.EndElement)
                    m_Reader.ReadEndElement();
                return;
            }
            Speed speed = DataInterpreter.AssignTravelSpeed(Tags, m_DefaultUnits);
            PersistentString highwayTag = new PersistentString(key);
            if (!m_PrimitiveRoot.TagNdx.Put(key, highwayTag)) 
            {
                highwayTag = m_PrimitiveRoot.TagNdx.Get(key);
            }
            var NewWay = new Way(WayId, NodeIds.ToArray(), speed, highwayTag, Tags, m_PrimitiveDb);
            m_PrimitiveRoot.WayNdx.Put(WayId, NewWay);

            for (int i = 0; i < NewWay.NodeIds.Count(); i++)
            {
                if (m_PrimitiveRoot.ArcNdx.Get(NodeIds[i]) == null)
                {
                    Volante.ISet<Way> set = m_Db.CreateSet<Way>();
                    set.Add(NewWay);
                    m_PrimitiveRoot.ArcNdx.Put(NodeIds[i], set);
                }
                else 
                {
                    m_PrimitiveRoot.ArcNdx.Get(NodeIds[i]).Add(NewWay);
                }
                var node = new Node(NewWay.NodeIds[i], m_PrimitiveDb);
                m_PrimitiveRoot.NodeNdx.Put(NewWay.NodeIds[i], node);
            }

            m_Ways++;
            if (m_Ways % 1000 == 0)
            {
                m_Progress.Invoke(m_ProgressPercentage, new ImportStatus(m_Ways, ImportState.ImportingWays));
            }

            if (m_Ways % 10000 == 0)
                m_PrimitiveDb.Commit();

            if (m_Reader.NodeType == XmlNodeType.EndElement)
                m_Reader.ReadEndElement();
        }
        private void ReadRelation()
        {
            Dictionary<string, string> Members = new Dictionary<string, string>();
            Dictionary<string, string> Roles = new Dictionary<string, string>();
            Dictionary<string, string> Tags = new Dictionary<string, string>();

            m_Reader.ReadStartElement(); // relation
            m_Reader.Skip();
            string str;
            while (m_Reader.Name == "member")
            {
                string key = m_Reader.GetAttribute("type");
                if (key != null)
                {
                    if (!Members.ContainsKey(key))
                        Members.Add(key, m_Reader.GetAttribute("ref"));

                    if (!Roles.ContainsKey(key))
                        Roles.Add(key, m_Reader.GetAttribute("role"));
                }
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }
            while (m_Reader.Name == "tag")
            {
                string key = m_Reader.GetAttribute("k");
                if (key != null)
                {
                    if (!Tags.ContainsKey(key))
                        Tags.Add(key, m_Reader.GetAttribute("v"));
                }
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }
            //Get turn/access restrictions between ways, if any exist.
            if (Tags.TryGetValue("restriction", out str))
            {
                if (str.Contains("no"))
                {
                    if (Members.TryGetValue("way", out str))
                    {
                        long WayId = Convert.ToInt64(str);
                        if (Roles.TryGetValue("way", out str))
                        {
                            HashSet<long> ProhibitedWays;
                            if (str == "to")
                            {
                                if (Members.TryGetValue("node", out str))
                                {
                                    long NodeId = Convert.ToInt64(str);
                                    if (!m_ProhibitedTo.TryGetValue(NodeId, out ProhibitedWays))
                                    {
                                        ProhibitedWays = new HashSet<long>();
                                        m_ProhibitedTo.Add(NodeId, ProhibitedWays);
                                    }
                                    ProhibitedWays.Add(WayId);
                                }
                            }
                            else if (str == "from")
                            {
                                if (Members.TryGetValue("node", out str))
                                {
                                    long NodeId = Convert.ToInt64(str);
                                    if (!m_ProhibitedFrom.TryGetValue(NodeId, out ProhibitedWays))
                                    {
                                        ProhibitedWays = new HashSet<long>();
                                        m_ProhibitedFrom.Add(NodeId, ProhibitedWays);
                                    }
                                    ProhibitedWays.Add(WayId);
                                }
                            }
                        }
                    }
                }
            }
            if (m_Reader.NodeType == XmlNodeType.EndElement)
                m_Reader.ReadEndElement(); // relation
        }

        private void SkipNode()
        {
            m_Reader.ReadStartElement();
            m_Reader.Skip();
            while (m_Reader.Name == "tag")
            {
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }
            if (m_Reader.NodeType == XmlNodeType.EndElement)
                m_Reader.ReadEndElement();
        }
        private void SkipWay()
        {
            m_Reader.ReadStartElement();
            m_Reader.Skip();

            while (m_Reader.Name == "nd")
            {
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }
            while (m_Reader.Name == "tag")
            {
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }

            if (m_Reader.NodeType == XmlNodeType.EndElement)
                m_Reader.ReadEndElement();
        }
        private void SkipRelation()
        {
            m_Reader.ReadStartElement(); // relation
            m_Reader.Skip();

            while (m_Reader.Name == "member")
            {
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }
            while (m_Reader.Name == "tag")
            {
                m_Reader.ReadStartElement();
                m_Reader.Skip();
            }

            if (m_Reader.NodeType == XmlNodeType.EndElement)
                m_Reader.ReadEndElement(); // relation
        }

        private void CreateNetwork(object arguments)
        {
            object[] args = arguments as object[];
            uint category = (uint)args[0];
            double boxDimensions = (double)args[1];

            double MaxLat = m_bbox.y_max;
            double MinLat = m_bbox.y_min;
            double MaxLon = m_bbox.x_max;
            double MinLon = m_bbox.x_min;

            double dLat;
            double dLon;
            uint cellNdx = 0;
            BoundingBox bb;

            dLat = MinLat + boxDimensions / 2D;
            do
            {
                dLon = MinLon + boxDimensions / 2D;
                do
                {
                    bb = new BoundingBox(new LatLon(dLat, dLon), boxDimensions);
                    RoadLink[] Links = new RoadLink[] { };

                    lock (m_DbRoot.SpatialNdx)
                    {
                        Links = m_DbRoot.SpatialNdx.Get(bb.Rectangle);
                        if (Links.Count() == 0 || Links.All(x => (int)x.Category != category))
                        {
                            dLon += boxDimensions;
                            continue;
                        }
                    }

                    var cellId = new CellId(cellNdx + (category + 1) * 1000000, category);
                    lock (m_DbRoot.SpatialNdx)
                    {
                        foreach (var rl in m_DbRoot.SpatialNdx.Get(bb.Rectangle))
                        {
                            if (bb.Contains(rl.FirstPoint))
                            {
                                if (!rl.FirstPointCellIds.Contains(cellId))
                                    rl.FirstPointCellIds.Add(cellId);
                            }

                            if (bb.Contains(rl.LastPoint))
                            {
                                if (!rl.LastPointCellIds.Contains(cellId))
                                    rl.LastPointCellIds.Add(cellId);
                            }
                        }
                    }
                    dLon += boxDimensions;
                    cellNdx++;

                    if (cellNdx % 100 == 0)
                    {
                        lock (m_Db)
                        {
                            m_Db.Commit();
                        }
                    }
                } while (dLon < MaxLon + 1);

                dLat += boxDimensions;

            } while (dLat < MaxLat + 1);
            lock (m_Db)
            {
                m_Db.Commit();
            }
        }
        private void CreateRoutingDatabase() 
        {
            uint Counter = 0;
            uint cellNdx = 0;
            m_bbox = new BoundingBox(m_DbRoot.SpatialNdx.WrappingRectangle);

            double MaxLat = m_bbox.y_max;
            double MinLat = m_bbox.y_min;
            double MaxLon = m_bbox.x_max;
            double MinLon = m_bbox.x_min;

            double dLat;
            double dLon;
            BoundingBox bb;

            double[] BoxDimensions = new double[12] 
            {
                1,
                1,
                0.5,
                0.5,
                0.25,
                0.25,
                0.25,
                0.25,
                0.1,
                0.1,
                0.05,
                0.05,
            };
            for (uint i = 0; i < 12; i++)
            {
                dLat = MinLat + BoxDimensions[i] / 2D;
                do
                {
                    dLon = MinLon + BoxDimensions[i] / 2D;
                    do
                    {
                        bb = new BoundingBox(new LatLon(dLat, dLon), BoxDimensions[i]);
                        RoadLink[] Links = new RoadLink[] { };

                        Links = m_DbRoot.SpatialNdx.Get(bb.Rectangle);
                        if (Links.Count() == 0 || Links.All(x => (uint)x.Category != i))
                        {
                            dLon += BoxDimensions[i];
                            continue;
                        }

                        var cellId = new CellId(cellNdx + (i + 1) * 1000000, i);
                        foreach (var rl in m_DbRoot.SpatialNdx.Get(bb.Rectangle))
                        {
                            if (bb.Contains(rl.FirstPoint))
                            {
                                if (!rl.FirstPointCellIds.Contains(cellId))
                                    rl.FirstPointCellIds.Add(cellId);
                            }

                            if (bb.Contains(rl.LastPoint))
                            {
                                if (!rl.LastPointCellIds.Contains(cellId))
                                    rl.LastPointCellIds.Add(cellId);
                            }
                        }
                        dLon += BoxDimensions[i];
                        cellNdx++;

                        if (cellNdx % 100 == 0)
                        {
                            m_Db.Commit();
                            m_Progress.Invoke(0, new ImportStatus(cellNdx, ImportState.CreatingRoutingDatabase));
                        }
                    } while (dLon < MaxLon + 1);

                    dLat += BoxDimensions[i];

                } while (dLat < MaxLat + 1);
            }
            m_Db.Commit();

            Counter = 0;

            foreach (var rl in m_DbRoot.SpatialNdx)
            {
                HashSet<ulong> ParentIds = new HashSet<ulong>();
                
                foreach (var id in rl.FirstPointCellIds)
                {
                    if (id.Category == (uint)rl.Category)
                        ParentIds.Add(id.Id);
                }
                foreach (var id in rl.LastPointCellIds)
                {
                    if (id.Category == (uint)rl.Category)
                        ParentIds.Add(id.Id);
                }
                CellId[] SortedIds = rl.FirstPointCellIds.ToArray();
                Array.Sort(SortedIds);
                rl.FirstPointCellIds = m_Db.CreateArray<CellId>();
                foreach (var id in SortedIds) 
                {
                    rl.FirstPointCellIds.Add(id);
                }

                SortedIds = rl.LastPointCellIds.ToArray();
                Array.Sort(SortedIds);
                rl.LastPointCellIds = m_Db.CreateArray<CellId>();
                foreach (var id in SortedIds)
                {
                    rl.LastPointCellIds.Add(id);
                }

                foreach (var id in ParentIds)
                    m_DbRoot.CellNdx.Get((uint)rl.Category).Put(id, rl);

                Counter++;
                if (Counter % 1000 == 0)
                {
                    int progress = (int)((decimal)Counter / (decimal)m_RoadLinks * 100m);
                    m_Progress.Invoke(progress, new ImportStatus(Counter, ImportState.UpdatingSpatialIndex));
                    m_Db.Commit();
                }
            }
            m_Db.Commit();
            m_Db.Close();
            m_Progress.Invoke(0, new ImportStatus(0, ImportState.Complete));
        }
        #endregion

        /// <summary>
        /// Creates a new OsmXmlImporter for a specified XML file (.osm) and an output
        /// directory for the database files.
        /// </summary>
        /// <param name="XmlFile">.osm XML file.</param>
        /// <param name="OutputDirectory">Output directory for database files.</param>
        public OsmXmlImporter(FileInfo XmlFile, DirectoryInfo OutputDirectory)
        {
            if (!XmlFile.Exists)
                throw new FileNotFoundException(XmlFile.Name + " not found.");

            if (!OutputDirectory.Exists)
                throw new DirectoryNotFoundException(OutputDirectory.FullName + " not found.");

            m_XmlPath = XmlFile;
            string[] split = Regex.Split(XmlFile.Name, ".osm");
            m_FileName = split[0];

            int counter = 0;
            string dir = OutputDirectory + @"\" + "Map Data";
            while (Directory.Exists(dir))
            {
                dir = OutputDirectory + @"\" + "Map Data" + "_" + counter.ToString();
                counter++;
            }
            m_OutputDirectory = Directory.CreateDirectory(dir);

            m_Db = DatabaseFactory.CreateDatabase();
            m_Db.Open(m_OutputDirectory.FullName + @"\" + m_FileName + ".dbf");
            m_DefaultUnits = SpeedUnits.KPH;
            m_DbRoot = m_Db.Root as DatabaseRoot;
            if (m_DbRoot == null) 
            {
                m_DbRoot = new DatabaseRoot(m_Db);
                m_Db.Root = m_DbRoot;
                m_Db.Commit();
            }

            m_PrimitiveDb = DatabaseFactory.CreateDatabase();
            m_PrimitiveDb.ObjectIndexInitSize *= 10;

            m_PrimitiveDb.Open(m_OutputDirectory.FullName + @"\" + m_FileName + ".pdbf");
            m_PrimitiveRoot = m_PrimitiveDb.Root as PrimitiveDataRoot;
            if (m_PrimitiveRoot == null) 
            {
                m_PrimitiveRoot = new PrimitiveDataRoot(m_PrimitiveDb);
                m_PrimitiveDb.Root = m_PrimitiveRoot;
                m_PrimitiveDb.Commit();
            }

            m_ProhibitedFrom = new Dictionary<long, HashSet<long>>(1000000);
            m_ProhibitedTo = new Dictionary<long, HashSet<long>>(1000000);
            m_Nodes = 0;
            m_Ways = 0;
        }

        /// <summary>
        /// Imports the XML file and creates primitives (.pdbf) file.
        /// </summary>
        /// <param name="TrackProgress">If true, the importer will read the length of the .osm
        /// file prior to importing in order to generate a progress percentage.</param>
        public void CreatePrimitiveDatabase(bool TrackProgress)
        {
            try
            {
                if (TrackProgress)
                {
                    m_FileLength = GetFileLength();
                }
                ImportWays();
                ImportNodes();
            }
            catch (XmlException) //Bad Xml file or (more likely) buggy import code. 
            {
                //Close the databases to hopefully prevent corruption of files.
                m_Db.Close();
                m_PrimitiveDb.Close();
                throw;
            }
            catch (Volante.DatabaseException) //Catch Volante database errors that I haven't figured out yet.
            {
                //Close the databases to hopefully prevent corruption of files.
                m_Db.Close();
                m_PrimitiveDb.Close();
                throw;
            }
        }
        /// <summary>
        /// Creates a spatial database for routing and spatial quieries via the MapExplorer control.
        /// </summary>
        public void CreateSpatialDatabase() 
        {
            try
            {
                CreateRoadLinks();
                CreateRoutingDatabase();
            }
            catch (Volante.DatabaseException) //Catch Volante database errors that I haven't figured out yet.
            {
                //Close the databases to hopefully prevent corruption of files.
                m_Db.Close();
                m_PrimitiveDb.Close();
                throw;
            }
        }
        /// <summary>
        /// Sets a delegate function to track import progress, e.g. through a
        /// BackgroundWorker or other background thread.
        /// </summary>
        public XmlImportProgress ImportProgress 
        {
            set 
            {
                m_Progress = value;
            }
        }
        /// <summary>
        /// Sets the default units the DataInterpreter should use when a way is tagged with
        /// a speed but no units. Generally MPH in the U.S. and KPH elsewhere.
        /// </summary>
        public SpeedUnits DefaultSpeedUnits 
        {
            set 
            {
                m_DefaultUnits = value;
            }
        }

        /// <summary>
        /// Structure containing information on import progress.
        /// </summary>
        public struct ImportStatus
        {
            #region Private
            private uint m_Count;
            private ImportState m_State;
            #endregion
            #region Internal
            internal ImportStatus(uint count, ImportState state) 
            {
                m_Count = count;
                m_State = state;
            }
            #endregion

            /// <summary>
            /// Returns the amount of an imported database object,
            /// which is specified in the Status message.
            /// </summary>
            public uint Count 
            {
                get
                {
                    return m_Count;
                }
            }
            /// <summary>
            /// Returns an ImportState enumeration value. See ImportState documentation.
            /// </summary>
            public ImportState State 
            {
                get 
                {
                    return m_State;
                }
            }
        }
        /// <summary>
        /// Enum indicating the state of an XML file import.
        /// </summary>
        public enum ImportState 
        {
            /// <summary>
            /// Indicates importer is importing OSM Nodes.
            /// </summary>
            ImportingNodes,
            /// <summary>
            /// Indicates importer is importing OSM Ways.
            /// </summary>
            ImportingWays,
            /// <summary>
            /// Indicates importer is creating RoadLinks and populating the .dbf file.
            /// </summary>
            ImportingRoadLinks,
            /// <summary>
            /// Indicates importer is creating and populating indices used by the routing engine.
            /// </summary>
            CreatingRoutingDatabase,
            /// <summary>
            /// Indicates importer is executing a post-processing step subsequent to populating
            /// the routing database.
            /// </summary>
            UpdatingSpatialIndex,
            /// <summary>
            /// Indicates an import is completed.
            /// </summary>
            Complete
        }
    }
}
