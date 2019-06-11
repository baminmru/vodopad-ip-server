using System;
using System.Collections.Generic;
using System.Xml;

namespace OsmExplorer.Data.Internal
{
    internal class OsmXmlReader
    {
        private List<string> m_Maxweight;
        private List<string> m_Maxheight;
        private List<string> m_Maxlength;
        private List<string> m_Maxwidth;
        private string m_FilePath;
        private int m_Counter;

        public OsmXmlReader(string XmlPath, string filePath)
        {
            m_Maxweight = new List<string>();
            m_Maxheight = new List<string>();
            m_Maxlength = new List<string>();
            m_Maxwidth = new List<string>();
            m_FilePath = filePath;
            m_Counter = 0;

            XmlReader reader = XmlReader.Create(XmlPath);
            reader.MoveToContent();
            reader.ReadStartElement("osm");
            reader.Skip();
            if (reader.Name == "bound")
            {
                reader.ReadStartElement("bound");
                reader.Skip();
            }
            while (reader.IsStartElement())
            {
                switch (reader.Name)
                {
                    case "node":
                        SkipNode(reader);
                        break;
                    case "way":
                        ReadWay(reader);
                        break;
                    case "relation":
                        SkipRelation(reader);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            WriteToFile();
        }

        private void ReadBound(XmlReader reader)
        {
            reader.ReadStartElement();
            reader.Skip();
        }

        private void ReadWay(XmlReader reader)
        {
            long WayId = Convert.ToInt64(reader.GetAttribute("id"));
            reader.ReadStartElement();
            reader.Skip();

            List<long> NodeIds = new List<long>();
            HashSet<long> NodeSet = new HashSet<long>();
            Dictionary<string, string> Tags = new Dictionary<string, string>();
            while (reader.Name == "nd")
            {
                long nodeId = Convert.ToInt64(reader.GetAttribute("ref"));
                if (NodeSet.Add(nodeId))
                    NodeIds.Add(nodeId);
                reader.ReadStartElement();
                reader.Skip();
            }
            while (reader.Name == "tag")
            {
                string key = reader.GetAttribute("k");
                if (key != null)
                {
                    if (!Tags.ContainsKey(key))
                        Tags.Add(key, reader.GetAttribute("v"));
                }
                reader.ReadStartElement();
                reader.Skip();
            }

            if (!Tags.ContainsKey("highway"))
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                    reader.ReadEndElement();
                return;
            }

            m_Counter++;
            if (m_Counter % 1000 == 0)
            {
                Console.Clear();
                Console.WriteLine(m_Counter);
            }

            string tag;
            if (Tags.TryGetValue("maxweight", out tag))
                m_Maxweight.Add(tag);

            if (Tags.TryGetValue("maxheight", out tag))
                m_Maxheight.Add(tag);

            if (Tags.TryGetValue("maxlength", out tag))
                m_Maxlength.Add(tag);

            if (Tags.TryGetValue("maxwidth", out tag))
                m_Maxwidth.Add(tag);

            if (reader.NodeType == XmlNodeType.EndElement)
                reader.ReadEndElement();
        }
        private void SkipNode(XmlReader reader)
        {
            reader.ReadStartElement();
            reader.Skip();
            while (reader.Name == "tag")
            {
                reader.ReadStartElement();
                reader.Skip();
            }
            if (reader.NodeType == XmlNodeType.EndElement)
                reader.ReadEndElement();
        }
        private void SkipRelation(XmlReader reader)
        {
            reader.ReadStartElement(); // relation
            reader.Skip();

            while (reader.Name == "member")
            {
                reader.ReadStartElement();
                reader.Skip();
            }
            while (reader.Name == "tag")
            {
                reader.ReadStartElement();
                reader.Skip();
            }

            if (reader.NodeType == XmlNodeType.EndElement)
                reader.ReadEndElement(); // relation
        }

        private void WriteToFile()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(m_FilePath + @"\Maxheight.txt"))
            {
                foreach (var tag in m_Maxheight)
                {
                    file.WriteLine(tag);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(m_FilePath + @"\Maxweight.txt"))
            {
                foreach (var tag in m_Maxweight)
                {
                    file.WriteLine(tag);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(m_FilePath + @"\Maxwidth.txt"))
            {
                foreach (var tag in m_Maxwidth)
                {
                    file.WriteLine(tag);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(m_FilePath + @"\Maxlength.txt"))
            {
                foreach (var tag in m_Maxlength)
                {
                    file.WriteLine(tag);
                }
            }
        }
    }
}
