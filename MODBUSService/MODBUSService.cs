using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace AREALModbusService
{
    public partial class AREALModbusService : ServiceBase
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
     
        
        Int32 id_bd;
        public AREALModbusService()
        {
            InitializeComponent();
            pStopServiceEvent = new ManualResetEvent(false);
            pStopApprovedEvent = new ManualResetEvent(false);
        }

        protected override void OnStart(string[] args)
        {
            Threads = new Dictionary<Int32, ThreadObj>();
            InfoReport("Starting AREALModbusService...");
            if (pMainThread == null)
            {
                //InfoReport("Creating AREALModbusService main thread...");
                pMainThread = new Thread(new ThreadStart(MainThread));
                //pMainThread.ApartmentState = ApartmentState.MTA;
                pMainThread.SetApartmentState(ApartmentState.MTA);
                pMainThread.Name = "AREALModbusService Main thread! It's need for AREALModbusService's working";
            }
            //InfoReport("Starting AREALModbusService main thread...");
            pMainThread.Start();
            base.OnStart(args); 
        }
        private void StopDeviceThreads()
        {
            // пусть заканчиваются самостоятельно
            //foreach (ThreadObj to in Threads.Values )
            //{
            //    try
            //    {
            //        if (!to.Process.HasExited) to.Process.Kill();
            //    }
            //    catch { }
            //}
        }

        private int ActiveThreadsCount()
        {
            int Count = 0;
            ThreadObj tt;

            foreach (var pair in Threads)
            {
                tt = pair.Value;
                if (tt.Process.HasExited == true || tt.Process.StartTime.AddMinutes(1) <= DateTime.Now)
                {
                    try
                    {
                        tt.Process.Kill();
                    }
                    catch
                    {

                    }

                }
                else
                {
                    Count++;
                }

            }

            return Count;


        }

        private void MainThread()
        {
            Threads = new Dictionary<Int32, ThreadObj>();
            eventLog1.BeginInit();
            STKTVMain.TVMain TvMain;
            int RequestInterval = 500;

          

            DataRow dr;
         
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

                        //InfoReport("Try DB login.");
                        if (TvMain.Init() == true)
                        {
                            bLogged = true;
                           
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
                //InfoReport("DB Initialization OK");



                // соединение прошло успешно
                if (bLogged)
                {
                    try
                    {

                     
                        System.Data.DataTable oRS;
                        ThreadObj throbj = null;

                
                        String q;

                        oRS = null;
                        q = "select  plancall.*,npip,nppassword,ipport,transport,sysdate ServerDate from bdevices join plancall on bdevices.id_bd=plancall.id_bd where id_dev=25 and  ( nplock is null or nplock <sysdate ) and npquery=1 ";
                        oRS = TvMain.QuerySelect(q);

                        if (oRS != null)
                        {

                            if (oRS.Rows.Count > 0)
                            {
                                InfoReport("Checking " + oRS.Rows.Count + " active plan(s) at " + DateTime.Now.ToString());
                                for (int i = 0; i < oRS.Rows.Count && i < 30 ;  i++)
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
                                            FileName = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\CurrentLoader.exe";
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
                                            if (Threads[id_bd].Process.HasExited == true || Threads[id_bd].Process.StartTime.AddMinutes(1) <= DateTime.Now)
                                            {
                                                // инициализируем процесс еще раз
                                                // сначала убиваем процесс
                                                try
                                                {
                                                    Threads[id_bd].Process.Kill();
                                                }catch{

                                                }
                                                throbj = new ThreadObj();
                                                throbj.DevID = id_bd;
                                                Process p = new Process();
                                                String FileName;
                                                FileName = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\CurrentLoader.exe";
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
                                    Thread.Sleep(200);  // wait for modem pull locking
                                }
                            } // зершение цикла по активным устройствам 

                            oRS = null;
                            throbj = null;

                        }
                    }
                    catch 
                    { }


                  

                  
                    try
                    {
                        TvMain.DeviceClose();
                        TvMain.CloseDBConnection();
                    }
                    catch { }
                }
                TvMain = null;

               
            } while (!pStopServiceEvent.WaitOne(RequestInterval, false));

            try
            {
                InfoReport("Closing AREALModbusService...");
                StopDeviceThreads();
                Threads.Clear();
                Threads = null;
                dr = null;
                base.OnStop();
                eventLog1.Dispose();
                return;
            }
            catch (Exception Ex)
            {
                ErrorReport("Closing AREALModbusService error:" + Ex.Message);
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
        #endregion log functions

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
