using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml ;
using ROBUSTELServer;

namespace ROBUSTELServiceNS
{
    public partial class ROBUSTELService : ServiceBase
    {
     


        private String GetMyDir() {
            String s;
            s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            s = s.Substring(6);
            return s;
        }


        private ROBUSTELServer.TCPServer srv=null;
        public ManualResetEvent pStopServiceEvent;
        public ManualResetEvent pStopApprovedEvent;

        private System.Threading.Thread pMainThread = null;
        
        private const int cWaitInterval = 120000;
        private Object thisLock = new Object();
        private LogInfo pLogParams = new LogInfo();
        
     
        public ROBUSTELService()
        {
            InitializeComponent();
            pStopServiceEvent = new ManualResetEvent(false);
            pStopApprovedEvent = new ManualResetEvent(false);
        }

        protected override void OnStart(string[] args)
        {
           
            InfoReport("Starting ROBUSTELService...");
            if (pMainThread == null)
            {
                //InfoReport("Creating STKServiceModem main thread...");
                pMainThread = new Thread(new ThreadStart(MainThread));
                //pMainThread.ApartmentState = ApartmentState.MTA;
                pMainThread.SetApartmentState(ApartmentState.MTA);
                pMainThread.Name = "ROBUSTELService Main thread! It's need for ROBUSTELService's working";
            }
            //InfoReport("Starting STKServiceModem main thread...");
            pMainThread.Start();
            base.OnStart(args); 
        }
        private void StopDeviceThreads()
        {
            if (srv!=null) srv.StopServer();
            srv = null;
        }

        

        private void MainThread()
        {



            InfoReport("Load config from :" +GetMyDir() + "\\ROBUSTELServiceCFG.xml");
            int RequestInterval = 60000;
            XmlDocument xml=null;
            XmlElement node = null;
            xml = new XmlDocument();
            try
            {
                xml.Load(GetMyDir() + "\\ROBUSTELServiceCFG.xml");
                
                node = (XmlElement)xml.LastChild;
            }
            catch (Exception)
            {
                
                
            }
           
            String sAddr;
            short  nPort;

            try{
                sAddr = node.Attributes.GetNamedItem("serveraddress").Value;
            }catch(System.Exception ex){
                sAddr = "192.168.9.146";
            }

            try{
                nPort = Convert.ToInt16 ( node.Attributes.GetNamedItem("serverport").Value);
            }catch(System.Exception ex){
                nPort = 16899;
            }
            
            eventLog1.BeginInit();

        

            bool bLogged = false;
            do
            {
                bLogged = false;
                do
                {
                    // случилось команда на выход из сервиса
                    if (pStopServiceEvent.WaitOne(1, false))
                    {
                        try
                        {

                            InfoReport("Exiting working thread");
                            pStopApprovedEvent.Set();
                            return;
                        }
                        catch (Exception Ex)
                        {
                            ErrorReport("Exiting working thread error: " + Ex.Message);
                            return;
                        }
                    }



                    // запускаем TCP  сервер слушать порт
                    try
                    {
                        if (srv == null)
                        {
                            InfoReport("Start ROBUSTEL Server");
                            srv = new ROBUSTELServer.TCPServer(sAddr, nPort);
                            srv.StartServer();
                        }
                        else
                        {
                            if (!srv.IsLive())
                            {
                                try
                                {
                                    InfoReport("Stop ROBUSTEL Server");
                                    srv.StopServer();
                                    srv = null;
                                }
                                catch (Exception Ex)
                                {
                                    ErrorReport("Error while stopping TCP Listenner... " + Ex.Message);

                                }
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        ErrorReport("Error while start TCP Listenner... " + Ex.Message);
                        
                    }

                    System.Threading.Thread.Sleep(1000);

                   


                } while (!pStopServiceEvent.WaitOne(1000, false) && !bLogged);
             

               

               
            } while (!pStopServiceEvent.WaitOne(RequestInterval, false));

            try
            {
                InfoReport("Closing ROBUSTELService...");
                StopDeviceThreads();
                base.OnStop();
                eventLog1.Dispose();
                return;
            }
            catch (Exception Ex)
            {
                ErrorReport("Closing ROBUSTELService error:" + Ex.Message);
            }
            
            
        }
             

        protected override void OnStop()
        {
            pStopServiceEvent.Set();
        }
        protected override void OnContinue()
        {
            base.OnContinue();
   
        }

        protected override void OnPause()
        {
            base.OnPause();
          
        }
        
        

        #region log functions
        public void ErrorReport(string Message)
        {
            lock (thisLock)
            {
                LogReport(Message, EventLogEntryType.Error);
            }
        }
        public void InfoReport(string Message)
        {
            lock (thisLock)
            {
                LogReport(Message, EventLogEntryType.Information);
            }
        }
        public void WarningReport(string Message)
        {
            lock (thisLock)
            {
                LogReport(Message, EventLogEntryType.Warning);
            }
        }
        public void LogReport(string Message, System.Diagnostics.EventLogEntryType ELET)
        {
            if (pLogParams != null)
            {
                if (pLogParams.UseEventLog)
                {
                    //this.EventLog.WriteEntry(Message, ELET);
                    this.eventLog1.WriteEntry(Message, ELET);
                    System.Diagnostics.Trace.WriteLine(ELET.ToString() + " :" + Message);
                }
                if (pLogParams.UseFileLog && pLogParams.LogFile.ToString() != "")
                {
                    try
                    {
                        string FileName = "";//string FileName = pLogParams.LogFile;
                        if (FileName == string.Empty || FileName == "") FileName = System.IO.Path.GetDirectoryName(GetType().Assembly.Location)+"STKServiceLogFile.txt";
                        System.IO.TextWriter LogFile = new System.IO.StreamWriter(FileName, true);
                        if (ELET == System.Diagnostics.EventLogEntryType.Error)
                            LogFile.WriteLine(System.DateTime.Now.ToString() + " Error: " + Message);
                        else if (ELET == System.Diagnostics.EventLogEntryType.Warning)
                            LogFile.WriteLine(System.DateTime.Now.ToString() + " Warning: " + Message);
                        else
                            LogFile.WriteLine(System.DateTime.Now.ToString() + Message);
                        LogFile.Close();
                        LogFile = null;
                    }
                    catch { }
                }
            }
            else
            {
                this.eventLog1.WriteEntry(System.DateTime.Now.ToString() + Message, ELET);
                if (ELET == System.Diagnostics.EventLogEntryType.Error)
                    System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString() + " Error: " + Message);
                else if (ELET == System.Diagnostics.EventLogEntryType.Warning)
                    System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString() + " Warning: " + Message);
                else
                    System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString() + Message);
            }
            return;
        }
        #endregion log functions

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
