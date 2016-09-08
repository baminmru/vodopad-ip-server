using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;
using System.Xml;
using Opc.Ua.Configuration;




    public class OPCRead
    {
        private DataTable dt;
        private DataRow dr;
        private ApplicationConfiguration m_configuration;
        private Session m_session;
        private SessionReconnectHandler m_reconnectHandler;

        private EventHandler m_ReconnectComplete;
        private EventHandler m_ReconnectStarting;
        private EventHandler m_KeepAliveComplete;
        private EventHandler m_ConnectComplete;
        private String m_ServerUrl;
    private String m_XML;

        public OPCRead(String s_serverURL, String sXML)
        {
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Client;
            application.ConfigSectionName = "OPCReader";

            m_ServerUrl = s_serverURL;
            m_XML = sXML;


            try
            {
                
                application.LoadApplicationConfiguration(GetMyDir()+"\\DrvOPC.Config.xml",   false);
                m_configuration = application.ApplicationConfiguration;
            }
            catch (Exception e)
            {
                
                return;
            }

    
        }


        /// <summary>
        /// The locales to use when creating the session.
        /// </summary>
        public string[] PreferredLocales { get; set; }

        /// <summary>
        /// The user identity to use when creating the session.
        /// </summary>
        public IUserIdentity UserIdentity { get; set; }

        /// <summary>
        /// The client application configuration.
        /// </summary>
        public ApplicationConfiguration Configuration
        {
            get { return m_configuration; }

            set
            {
                if (!Object.ReferenceEquals(m_configuration, value))
                {
                    m_configuration = value;

                }
            }
        }

        /// <summary>
        /// The currently active session. 
        /// </summary>
        public Session Session
        {
            get { return m_session; }
        }


        /// <summary>
        /// The currently active session. 
        /// </summary>
        public String SetupXML
        {
            get { return m_XML ; }
        }


        private int m_reconnectPeriod = 10;
        /// <summary>
        /// The number of seconds between reconnect attempts (0 means reconnect is disabled).
        /// </summary>
        [DefaultValue(10)]
        public int ReconnectPeriod
        {
            get { return m_reconnectPeriod; }
            set { m_reconnectPeriod = value; }
        }

        /// <summary>
        /// Raised when a good keep alive from the server arrives.
        /// </summary>
        public event EventHandler KeepAliveComplete
        {
            add { m_KeepAliveComplete += value; }
            remove { m_KeepAliveComplete -= value; }
        }

        /// <summary>
        /// Raised when a reconnect operation starts.
        /// </summary>
        public event EventHandler ReconnectStarting
        {
            add { m_ReconnectStarting += value; }
            remove { m_ReconnectStarting -= value; }
        }

        /// <summary>
        /// Raised when a reconnect operation completes.
        /// </summary>
        public event EventHandler ReconnectComplete
        {
            add { m_ReconnectComplete += value; }
            remove { m_ReconnectComplete -= value; }
        }

        /// <summary>
        /// Raised after successfully connecting to or disconnecing from a server.
        /// </summary>
        public event EventHandler ConnectComplete
        {
            add { m_ConnectComplete += value; }
            remove { m_ConnectComplete -= value; }
        }


        public Session Connect()
        {
            // disconnect from existing session.
            Disconnect();

            // determine the URL that was selected.
            string serverUrl = m_ServerUrl;



            if (m_configuration == null)
            {
                throw new ArgumentNullException("m_configuration");
            }

            // select the best endpoint.
            EndpointDescription endpointDescription = ClientUtils.SelectEndpoint(serverUrl, false);

            EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(m_configuration);
            ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

            m_session = Session.Create(
                m_configuration,
                endpoint,
                false,
                false,
                "DrvOpc",
                60000,
                UserIdentity,
                PreferredLocales);

            // set up keep alive callback.
            // m_session.KeepAlive += new KeepAliveEventHandler(Session_KeepAlive);

            // raise an event.
            DoConnectComplete(null);

            // return the new session.
            return m_session;
        }

        /// <summary>
        /// Creates a new session.
        /// </summary>
        /// <param name="serverUrl">The URL of a server endpoint.</param>
        /// <param name="useSecurity">Whether to use security.</param>
        /// <returns>The new session object.</returns>
        public Session Connect(string s_serverUrl)
        {
            m_ServerUrl = s_serverUrl;

            return Connect();
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public void Disconnect()
        {
            // UpdateStatus(false, DateTime.UtcNow, "Disconnected");

            // stop any reconnect operation.
            if (m_reconnectHandler != null)
            {
                m_reconnectHandler.Dispose();
                m_reconnectHandler = null;
            }

            // disconnect any existing session.
            if (m_session != null)
            {
                m_session.Close(10000);
                m_session = null;
            }

            // raise an event.
            DoConnectComplete(null);
        }

        /// <summary>
        /// Raises the connect complete event on the main GUI thread.
        /// </summary>
        private void DoConnectComplete(object state)
        {
            if (m_ConnectComplete != null)
            {
               
                m_ConnectComplete(this, null);
                Log("Connect OK");
            }
        }

      

        private String S80(String S)
        {
            String sout;
            sout = S;
            if (sout.Length > 80) sout = sout.Substring(0, 77) + "...";
            if (sout.Length < 80)
            {
                for (int i = sout.Length; i < 80; i++)
                {
                    sout = sout + " ";
                }
            }
            return sout;
        }
        private String s40(String S)
        {
            String sout;
            sout = S;
            if (sout.Length > 40) sout = sout.Substring(0, 37) + "...";
            if (sout.Length < 40)
            {
                for (int i = sout.Length; i < 40; i++)
                {
                    sout = sout + " ";
                }
            }
            return sout;
        }

        private void ReadNode(NodeId nodeid, bool ignoreroot, bool ignorechildren, String DriverName)
        {
            INode node = m_session.NodeCache.Find(nodeid);
            if (node != null)
            {
                //TreeNode root = new TreeNode(node.ToString());
                //root.ImageIndex = ClientUtils.GetImageIndex(m_session, node.NodeClass, node.TypeDefinitionId, false);
                //root.SelectedImageIndex = ClientUtils.GetImageIndex(m_session, node.NodeClass, node.TypeDefinitionId, true);

                ReferenceDescription reference = new ReferenceDescription();

                reference.NodeId = node.NodeId;
                reference.NodeClass = node.NodeClass;
                reference.BrowseName = node.BrowseName;
                reference.DisplayName = node.DisplayName;
                reference.TypeDefinition = node.TypeDefinitionId;


                // build list of attributes to read.
                ReadValueIdCollection nodesToRead = new ReadValueIdCollection();

                foreach (uint attributeId in Attributes.GetIdentifiers())
                {
                    ReadValueId nodeToRead = new ReadValueId();
                    nodeToRead.NodeId = (NodeId)reference.NodeId;
                    nodeToRead.AttributeId = attributeId;
                    nodesToRead.Add(nodeToRead);
                }

                // read the attributes.
                DataValueCollection results = null;
                DiagnosticInfoCollection diagnosticInfos = null;

                m_session.Read(
                 null,
                 0,
                 TimestampsToReturn.Neither,
                 nodesToRead,
                 out results,
                 out diagnosticInfos);
                if (ignoreroot == false)
                {
                    //Log(results[0].ToString() + " \t| " + results[3].ToString() + " \t| "+ results[4].ToString() + " \t| " + results[12].ToString());
                    Log("UID:" + S80(results[(int)Attributes.NodeId - 1].ToString()) + " \tBrowseName: " + s40(results[(int)Attributes.BrowseName - 1].ToString()) + " \tDisplayName:  " + s40(results[(int)Attributes.DisplayName - 1].ToString()) + " \tDescription:  " + S80(results[(int)Attributes.Description - 1].ToString()) + " \tValue:  " + results[(int)Attributes.Value - 1].ToString());

                    dr = dt.NewRow();
                    dr["UID"] = results[(int)Attributes.NodeId - 1].ToString();
                    dr["BrowseName"] = results[(int)Attributes.BrowseName - 1].ToString();
                    dr["DisplayName"] = results[(int)Attributes.DisplayName - 1].ToString();
                    dr["Description"] = results[(int)Attributes.Description - 1].ToString();
                    dr["Value"] = results[(int)Attributes.Value - 1].ToString();
                    dr["Name"] = DriverName;
                    dt.Rows.Add(dr);
                    if (results[(int)Attributes.AccessLevel - 1].Value != null)
                    {
                        if (((byte)(results[(int)Attributes.AccessLevel - 1].GetValue(typeof(byte))) & AccessLevels.HistoryRead) == AccessLevels.HistoryRead)
                        {
                            //gv.DataSource = dt;
                            ReadHistory(DateTime.Now.AddDays(-1), DateTime.Now, new NodeId(results[0].ToString()), 25, "");
                        }
                    }
                }

                if (ignorechildren == false)
                {
                    BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();


                    BrowseDescription nodeToBrowse = new BrowseDescription();

                    nodeToBrowse.NodeId = nodeid;
                    nodeToBrowse.BrowseDirection = BrowseDirection.Forward;
                    nodeToBrowse.ReferenceTypeId = Opc.Ua.ReferenceTypeIds.HierarchicalReferences;
                    nodeToBrowse.IncludeSubtypes = true;
                    nodeToBrowse.NodeClassMask = 0;
                    nodeToBrowse.ResultMask = (uint)BrowseResultMask.All;

                    if (reference != null)
                    {
                        nodeToBrowse.NodeId = (NodeId)reference.NodeId;
                    }

                    nodesToBrowse.Add(nodeToBrowse);

                    ReferenceDescriptionCollection references = ClientUtils.Browse(m_session, null, nodesToBrowse, false);

                    for (int ii = 0; references != null && ii < references.Count; ii++)
                    {
                        reference = references[ii];


                        // build list of attributes to read.
                        nodesToRead = new ReadValueIdCollection();

                        foreach (uint attributeId in Attributes.GetIdentifiers())
                        {
                            ReadValueId nodeToRead = new ReadValueId();
                            nodeToRead.NodeId = (NodeId)reference.NodeId;
                            nodeToRead.AttributeId = attributeId;
                            nodesToRead.Add(nodeToRead);
                        }

                        // read the attributes.
                        results = null;
                        diagnosticInfos = null;

                        m_session.Read(
                         null,
                         0,
                         TimestampsToReturn.Neither,
                         nodesToRead,
                         out results,
                         out diagnosticInfos);
                        //    Log("UID:"+results[0].ToString() + " \tBrowseName: " + results[(int)Attributes.BrowseName].ToString() + " \tDisplayName:  " + results[(int)Attributes.DisplayName].ToString() + " \tDescription:  " + results[(int)Attributes.Description].ToString() + " \t|  " + results[(int)Attributes.Value].ToString());
                        //Log("UID:" + S80(results[(int)Attributes.NodeId - 1].ToString()) + " \tBrowseName: " + results[(int)Attributes.BrowseName - 1].ToString() + " \tDisplayName:  " + results[(int)Attributes.DisplayName - 1].ToString() + " \tDescription:  " + results[(int)Attributes.Description - 1].ToString() + " \tValue:  " + results[(int)Attributes.Value - 1].ToString());
                        Log("UID:" + S80(results[(int)Attributes.NodeId - 1].ToString()) + " \tBrowseName: " + s40(results[(int)Attributes.BrowseName - 1].ToString()) + " \tDisplayName:  " + s40(results[(int)Attributes.DisplayName - 1].ToString()) + " \tDescription:  " + S80(results[(int)Attributes.Description - 1].ToString()) + " \tValue:  " + results[(int)Attributes.Value - 1].ToString());
                        dr = dt.NewRow();
                        dr["UID"] = results[(int)Attributes.NodeId - 1].ToString();
                        dr["BrowseName"] = results[(int)Attributes.BrowseName - 1].ToString();
                        dr["DisplayName"] = results[(int)Attributes.DisplayName - 1].ToString();
                        dr["Description"] = results[(int)Attributes.Description - 1].ToString();
                        dr["Value"] = results[(int)Attributes.Value - 1].ToString();
                        dt.Rows.Add(dr);
                        try
                        {
                            if (results[(int)Attributes.AccessLevel - 1].Value != null)
                            {
                                if (((byte)(results[(int)Attributes.AccessLevel - 1].GetValue(typeof(byte))) & AccessLevels.HistoryRead) == AccessLevels.HistoryRead)
                                {
                                    //gv.DataSource = dt;
                                    ReadHistory(DateTime.Today, DateTime.Now, new NodeId(results[0].ToString()), 25, "");
                                }
                            }
                            else
                            {
                                if (results[(int)Attributes.Value - 1].Value == null)
                                {
                                   // gv.DataSource = dt;
                                    ReadNode(new NodeId(results[(int)Attributes.NodeId - 1].ToString()), true, false, "");
                                }

                            }
                        }
                        catch
                        {
                            Log("V=" + results[17].ToString());
                        }

                    }
                }

            }

        }

        private void ReadHistory(DateTime StartTime, DateTime EndTime, NodeId node, uint HCount, String DriverName)
        {
            ReadRawModifiedDetails details = new ReadRawModifiedDetails();
            details.StartTime = DateTime.MinValue;
            details.EndTime = DateTime.MinValue;
            details.IsReadModified = false;
            details.NumValuesPerNode = HCount;
            details.ReturnBounds = true;
            details.StartTime = StartTime.ToUniversalTime();
            details.EndTime = EndTime.ToUniversalTime();

            HistoryReadValueId nodeToRead = new HistoryReadValueId();
            nodeToRead.NodeId = node;

            HistoryReadValueIdCollection nodesToRead = new HistoryReadValueIdCollection();
            nodesToRead.Add(nodeToRead);

            HistoryReadResultCollection results = null;
            DiagnosticInfoCollection diagnosticInfos = null;

            m_session.HistoryRead(
                null,
                new ExtensionObject(details),
                TimestampsToReturn.Source,
                false,
                nodesToRead,
                out results,
                out diagnosticInfos);

            for (int i = 0; i < results.Count; i++)
            {

                HistoryData hd_results = ExtensionObject.ToEncodeable(results[i].HistoryData) as HistoryData;

                if (hd_results != null)
                {

                    for (int ii = 0; ii < hd_results.DataValues.Count; ii++)
                    {
                        StatusCode status = hd_results.DataValues[ii].StatusCode;


                        string timestamp = hd_results.DataValues[ii].SourceTimestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        string value = Utils.Format("{0}", hd_results.DataValues[ii].WrappedValue);
                        string quality = Utils.Format("{0}", (StatusCode)status.CodeBits);
                        string historyInfo = Utils.Format("{0:X2}", (int)status.AggregateBits);
                        Log("UID:" + S80(node.ToString()) + " \tTime: " + timestamp + "\tValue: " + value); // + ", Q:" + quality + ", histInfo:" + historyInfo);
                        dr = dt.NewRow();
                        dr["UID"] = node.ToString();
                        dr["SourceTime"] = timestamp;
                        dr["Value"] = value;
                        dr["Name"] = DriverName;
                        dt.Rows.Add(dr);
                    }
                }



            }

        }

        private void Log(String text)
        {

            System.Diagnostics.Debug.Print(text);
            //Application.DoEvents();
        }


        private string GetMyDir()
        {
            String s;
            s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            s = s.Substring(6);
            return s;
        }

    public String GetTime()
    {
        dt = new DataTable();
        dt.Columns.Add("UID");
        dt.Columns.Add("BrowseName");
        dt.Columns.Add("DisplayName");
        dt.Columns.Add("Description");
        dt.Columns.Add("SourceTime");
        dt.Columns.Add("Name");
        dt.Columns.Add("Value");


        XmlDocument xml = new XmlDocument();
        xml.LoadXml(SetupXML);
        XmlElement root;
        XmlElement node;
        XmlNodeList xlst;

        root = (XmlElement)xml.LastChild;
        xlst = root.GetElementsByTagName("device");
        if (xlst.Count == 0) return DateTime.Now.ToString(); 
        node = (XmlElement)xlst.Item(0);      // device

        String opcroot = "";
        String curtime_uid = "";
     

        opcroot = node.GetAttribute("opcroot");
        curtime_uid = node.GetAttribute("curtime");
      
        if(curtime_uid !="")
        {
            ReadNode(opcroot + curtime_uid, false, true, "Time");
            return dt.Rows[0]["Value"].ToString();
        }
        else
        {
            return DateTime.Now.ToString();
        }
        
      
        
        



    }

    public DataTable GetCurrent()
        {
            dt = new DataTable();
            dt.Columns.Add("UID");
            dt.Columns.Add("BrowseName");
            dt.Columns.Add("DisplayName");
            dt.Columns.Add("Description");
            dt.Columns.Add("SourceTime");
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");


         



            XmlDocument xml = new XmlDocument();
            xml.LoadXml(SetupXML  );
            XmlElement root;
            XmlElement node;
            XmlNodeList xlst;

            root = (XmlElement)xml.LastChild;
            xlst = root.GetElementsByTagName("device");
            if (xlst.Count == 0) return dt;
            node = (XmlElement)xlst.Item(0);      // device

            String opcroot = "";
            String curtime_uid = "";
            String serialnum_uid = "";
            String model_uid = "";

            opcroot = node.GetAttribute("opcroot");
            curtime_uid = node.GetAttribute("curtime");
            serialnum_uid = node.GetAttribute("serialnum");
            model_uid = node.GetAttribute("model");

            ReadNode(opcroot + curtime_uid, false, true, "Time");
            ReadNode(opcroot + serialnum_uid, false, true, "Serial Number");
            ReadNode(opcroot + model_uid, false, true, "Model");


            xlst = root.GetElementsByTagName("current");
            if (xlst.Count > 0)
            {
                node = (XmlElement)xlst.Item(0);      // device

                foreach (XmlAttribute xattr in node.Attributes)
                {
                    ReadNode(opcroot + xattr.Value, false, true, xattr.Name);
                }
            }
           return dt;


          
        }


        public DataTable GetTotals()
        {
            dt = new DataTable();
            dt.Columns.Add("UID");
            dt.Columns.Add("BrowseName");
            dt.Columns.Add("DisplayName");
            dt.Columns.Add("Description");
            dt.Columns.Add("SourceTime");
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            XmlDocument xml = new XmlDocument();
        xml.LoadXml(SetupXML);
        XmlElement root;
            XmlElement node;
            XmlNodeList xlst;

            root = (XmlElement)xml.LastChild;
            xlst = root.GetElementsByTagName("device");
            if (xlst.Count == 0) return dt;
            node = (XmlElement)xlst.Item(0);      // device

            String opcroot = "";
            String curtime_uid = "";
            String serialnum_uid = "";
            String model_uid = "";

            opcroot = node.GetAttribute("opcroot");
            curtime_uid = node.GetAttribute("curtime");
            serialnum_uid = node.GetAttribute("serialnum");
            model_uid = node.GetAttribute("model");

            ReadNode(opcroot + curtime_uid, false, true, "Time");
            ReadNode(opcroot + serialnum_uid, false, true, "Serial Number");
            ReadNode(opcroot + model_uid, false, true, "Model");


            xlst = root.GetElementsByTagName("totals");
            if (xlst.Count > 0)
            {
                node = (XmlElement)xlst.Item(0);      // device

                foreach (XmlAttribute xattr in node.Attributes)
                {
                    ReadNode(opcroot + xattr.Value, false, true, xattr.Name);
                }
            }
           return dt;


        }

        public DataTable getHour(DateTime StartDate)
        {
            dt = new DataTable();
            dt.Columns.Add("UID");
            dt.Columns.Add("BrowseName");
            dt.Columns.Add("DisplayName");
            dt.Columns.Add("Description");
            dt.Columns.Add("SourceTime");
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");


            XmlDocument xml = new XmlDocument();
        xml.LoadXml(SetupXML);
        XmlElement root;
            XmlElement node;
            XmlNodeList xlst;

            root = (XmlElement)xml.LastChild;
            xlst = root.GetElementsByTagName("device");
            if (xlst.Count == 0) return dt;
            node = (XmlElement)xlst.Item(0);      // device

            String opcroot = "";
            String curtime_uid = "";
            String serialnum_uid = "";
            String model_uid = "";

            opcroot = node.GetAttribute("opcroot");
            curtime_uid = node.GetAttribute("curtime");
            serialnum_uid = node.GetAttribute("serialnum");
            model_uid = node.GetAttribute("model");

            //ReadNode(opcroot + curtime_uid, false, true, "Time");
            //ReadNode(opcroot + serialnum_uid, false, true, "Serial Number");
            //ReadNode(opcroot + model_uid, false, true, "Model");


            xlst = root.GetElementsByTagName("hourly");
            if (xlst.Count > 0)
            {
                node = (XmlElement)xlst.Item(0);      // device
                DateTime sDate;
                DateTime eDate;
                sDate = StartDate.AddMinutes(-5);
                eDate = sDate.AddMinutes(10);
                foreach (XmlAttribute xattr in node.Attributes)
                {
                    ReadHistory(sDate, eDate, opcroot + xattr.Value, 1, xattr.Name);
                }
            }
            return dt;


           
        }

        public DataTable ReadDay(DateTime StartDate)
        {
            dt = new DataTable();
            dt.Columns.Add("UID");
            dt.Columns.Add("BrowseName");
            dt.Columns.Add("DisplayName");
            dt.Columns.Add("Description");
            dt.Columns.Add("SourceTime");
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            XmlDocument xml = new XmlDocument();
        xml.LoadXml(SetupXML);
        XmlElement root;
            XmlElement node;
            XmlNodeList xlst;

            root = (XmlElement)xml.LastChild;
            xlst = root.GetElementsByTagName("device");
            if (xlst.Count == 0) return dt;
            node = (XmlElement)xlst.Item(0);      // device

            String opcroot = "";
            String curtime_uid = "";
            String serialnum_uid = "";
            String model_uid = "";

            opcroot = node.GetAttribute("opcroot");
            curtime_uid = node.GetAttribute("curtime");
            serialnum_uid = node.GetAttribute("serialnum");
            model_uid = node.GetAttribute("model");

            //ReadNode(opcroot + curtime_uid, false, true, "Time");
            //ReadNode(opcroot + serialnum_uid, false, true, "Serial Number");
            //ReadNode(opcroot + model_uid, false, true, "Model");


            xlst = root.GetElementsByTagName("dayly");
            if (xlst.Count > 0)
            {
                node = (XmlElement)xlst.Item(0);      // device

                DateTime sDate;
                DateTime eDate;
                sDate = StartDate.AddMinutes(-5);
                eDate = sDate.AddMinutes(10);
                foreach (XmlAttribute xattr in node.Attributes)
                {
                    ReadHistory(sDate, eDate, opcroot + xattr.Value, 1, xattr.Name);
                }

            }
            return  dt;


           
        }

    }

