using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using STKTVMain;

namespace STKService
{
    public partial class STKService : ServiceBase
    {
         public class ThreadObj
         {
             public int DevID;
             public System.Diagnostics.Process  Process ;
         }
        
        public ManualResetEvent pStopServiceEvent;
        public ManualResetEvent pStopApprovedEvent;

        private Dictionary<Int32, ThreadObj> Threads;
        private System.Threading.Thread pMainThread = null;
        
        private const int cWaitInterval = 120000;
        private Object thisLock = new Object();
        private LogInfo pLogParams = new LogInfo();
        
        Int32 id_bd;
        public STKService()
        {
            InitializeComponent();
            pStopServiceEvent = new ManualResetEvent(false);
            pStopApprovedEvent = new ManualResetEvent(false);
        }

        public void  StartMe(){
            string[] Args = new string[1];
            OnStart(Args);
        }

        public void StopMe()
        {
            OnStop();
        }
        protected override void OnStart(string[] args)
        {
            Threads = new Dictionary<Int32, ThreadObj>();
            InfoReport("Starting STKService...");
            if (pMainThread == null)
            {
                InfoReport("Creating STKService main thread...");
                pMainThread = new Thread(new ThreadStart(MainThread));
                //pMainThread.ApartmentState = ApartmentState.MTA;
                pMainThread.SetApartmentState(ApartmentState.MTA);
                pMainThread.Name = "STKService Main thread! It's need for STKService's working";
            }
            InfoReport("Starting STKService main thread...");
            pMainThread.Start();
            //base.OnStart(args); 
        }
        private void StopDeviceThreads()
        {
            foreach (ThreadObj to in Threads.Values )
            {
                try
                {
                    if (!to.Process.HasExited) to.Process.Kill();
                }
                catch { }
            }
        }
        private void MainThread()
        {
            Threads = new Dictionary<Int32, ThreadObj>();
            eventLog1.BeginInit();
            STKTVMain.TVMain TvMain;
            DataRow dr;
            int RequestInterval=60000;

            bool bLogged = false;
            do
            {
                bLogged = false;
                TvMain = new STKTVMain.TVMain();
                do
                {
                    // случилось команда на выход из сервиса
                    if (pStopServiceEvent.WaitOne(1, false))
                    {
                        try
                        {

                            InfoReport("Exiting working thread...");
                            StopDeviceThreads();
                            pStopApprovedEvent.Set();
                            return;
                        }
                        catch (Exception Ex)
                        {
                            ErrorReport("Exiting working thread error: " + Ex.Message);
                            return;
                        }
                    }



                    // пытаемся соединиться с базой данных
                    try
                    {

                        InfoReport("Try DB login.");
                        if (TvMain.Init(true) == true)
                        {
                            bLogged = true;
                            RequestInterval = TvMain.RequestInterval;
                        }
                        else
                        {
                            //pRefService.WarningReport("Unable to login, check credentials");
                            WarningReport("Unable to login, check credentials");
                            InfoReport(TvMain.GetEnvInfo());
                        }
                    }
                    catch (Exception Ex)
                    {
                        //pRefService.ErrorReport("Login failed, try again... " + Ex.Message);
                        ErrorReport("Login failed, try again... " + Ex.Message);
                        InfoReport(TvMain.GetEnvInfo());
                    }
                } while (!pStopServiceEvent.WaitOne(1000, false) && !bLogged);
                //pRefService.InfoReport("DB Initialization OK");
                InfoReport("DB Initialization OK");


                // соединение прошло успешно
                if (bLogged)
                {

                    System.Data.DataTable oRS;
                    ThreadObj throbj = null;
                    

                    oRS = null;
                    oRS = TvMain.GetDBDevicePlanList();


                    if (oRS != null)
                    {
                       
                        if (oRS.Rows.Count > 0)
                        {
                            InfoReport("Checking " + oRS.Rows.Count + " active plan(s) at " + DateTime.Now.ToString());
                            for (int i = 0; i < oRS.Rows.Count; i++)
                            {

                                try
                                {
                                    dr = oRS.Rows[i];
                                    id_bd = Convert.ToInt32(dr["id_bd"].ToString());
                                    if (!Threads.ContainsKey(id_bd))
                                    {

                                        // создаем и стартуем новый процесс для опроса устройства
                                        throbj = new ThreadObj();
                                        throbj.DevID = id_bd;
                                        Process p = new Process();
                                        String FileName;
                                        FileName = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\STKDeviceThread.exe";
                                        String DirName = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
                                        p.StartInfo.FileName = FileName;
                                        p.StartInfo.Arguments = "-D " + id_bd.ToString();
                                        p.StartInfo.WorkingDirectory = DirName;
                                        throbj.Process = p;
                                        Threads.Add(id_bd, throbj);
                                        throbj.Process.Start();


                                    }
                                    else
                                    {
                                        if (Threads[id_bd].Process.HasExited == true)
                                        {
                                            // инициализируем процесс еще раз
                                            Threads[id_bd].Process.Close();
                                            throbj = new ThreadObj();
                                            throbj.DevID = id_bd;
                                            Process p = new Process();
                                            String FileName;
                                            FileName = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\STKDeviceThread.exe";
                                            String DirName = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);

                                            p.StartInfo.FileName = FileName;
                                            p.StartInfo.Arguments = "-D " + id_bd.ToString();
                                            p.StartInfo.WorkingDirectory = DirName;
                                            throbj.Process = p;
                                            Threads[id_bd] = throbj;
                                            throbj.Process.Start();

                                        }
                                    }

                                }
                                catch (Exception Ex)
                                {
                                    ErrorReport("Thread " + id_bd.ToString() + " error:" + Ex.Message);
                                }
                            }
                        } // зершение цикла по активным устройствам 

                        oRS = null;
                        throbj = null;

                    }

                }

                TvMain.EndInit(true);
                //TvMain.CloseDBConnection();
                TvMain = null;
               
            } while (!pStopServiceEvent.WaitOne(RequestInterval, false));

            try
            {
                InfoReport("Closing STKService...");
                StopDeviceThreads();
                Threads.Clear();
                Threads = null;
                dr = null;
                //base.OnStop();
                eventLog1.Dispose();
                return;
            }
            catch (Exception Ex)
            {
                ErrorReport("Closing STKService error:" + Ex.Message);
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
