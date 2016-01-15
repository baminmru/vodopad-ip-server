using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace STKService
{
    public partial class STKService 
    {
        public class ThreadObj
        {
            public int DevID;
            public System.Diagnostics.Process Process;
        }

        public ManualResetEvent pStopServiceEvent;
        public ManualResetEvent pStopApprovedEvent;

        private Dictionary<Int32, ThreadObj> Threads;
        private System.Threading.Thread pMainThread = null;

        private const int cWaitInterval = 120000;
        private Object thisLock = new Object();
        //private LogInfo pLogParams = new LogInfo();

        Int32 id_bd;
        public STKService()
        {
            //InitializeComponent();
            pStopServiceEvent = new ManualResetEvent(false);
            pStopApprovedEvent = new ManualResetEvent(false);
        }

        public void StartMe()
        {
            Threads = new Dictionary<Int32, ThreadObj>();
            InfoReport("Starting STKService...");
            if (pMainThread == null)
            {
                //InfoReport("Creating STKService main thread...");
                pMainThread = new Thread(new ThreadStart(MainThread));
                //pMainThread.ApartmentState = ApartmentState.MTA;
                pMainThread.SetApartmentState(ApartmentState.MTA);
                pMainThread.Name = "STKService Main thread! It's need for STKService's working";
            }
            //InfoReport("Starting STKService main thread...");
            pMainThread.Start();
            //base.OnStart(args);
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

            for (int i = 0; i < Threads.Count; i++)
            {
                if (Threads[i].Process.HasExited == true || Threads[i].Process.StartTime.AddMinutes(30) <= DateTime.Now)
                {
                    try
                    {
                        Threads[id_bd].Process.Kill();
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
            //eventLog1.BeginInit();
            STKTVMain.TVMain TvMain;
            int RequestInterval = 60000;

            DateTime TGKSendTime;
            DateTime AnalizerTime;
            AnalizerTime = DateTime.Now;
            TGKSendTime = DateTime.Now;
            TGKSendTime = TGKSendTime.AddMinutes(2);
            AnalizerTime = AnalizerTime.AddMinutes(10);

            DataRow dr;
            int ModemCount = 0;
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
                //InfoReport("DB Initialization OK");



                // соединение прошло успешно
                if (bLogged)
                {
                    try
                    {

                       
                        System.Data.DataTable oRS;
                        ThreadObj throbj = null;

                        oRS = null;
                        oRS = TvMain.GetDBDevicePlanListIP();


                        if (oRS != null)
                        {

                            if (oRS.Rows.Count > 0)
                            {
                                InfoReport("Checking " + oRS.Rows.Count + " active plan(s) at " + DateTime.Now.ToString());
                                for (int i = 0; i < oRS.Rows.Count && i < 10 && ActiveThreadsCount() <= ModemCount + 1; i++)
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
                                            if (Threads[id_bd].Process.HasExited == true || Threads[id_bd].Process.StartTime.AddMinutes(15) <= DateTime.Now)
                                            {
                                                // инициализируем процесс еще раз
                                                // сначала убиваем процесс
                                                try
                                                {
                                                    Threads[id_bd].Process.Kill();
                                                }
                                                catch
                                                {

                                                }
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
                                    Thread.Sleep(8000);  // wait for modem pull locking
                                }
                            } // зершение цикла по активным устройствам 

                            oRS = null;
                            throbj = null;

                        }
                    }
                    catch
                    { }


                    //if (true)  //Analizer flag
                    //{
                    //    if (AnalizerTime <= DateTime.Now)
                    //    {
                    //        try{
                    //            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\NCAnalizer.exe");
                    //            AnalizerTime = DateTime.Now.AddMinutes(13);
                    //        }catch {
                    //        }
                    //    }
                    //}

                    if (TvMain.SendToTGK)
                    {

                        if (TGKSendTime <= DateTime.Now)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location) + "\\TGKSender.exe");
                                TGKSendTime = DateTime.Now.AddHours(6);
                            }
                            catch
                            {
                            }
                        }
                    }
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
                InfoReport("Closing STKService...");
                StopDeviceThreads();
                Threads.Clear();
                Threads = null;
                dr = null;
               // base.OnStop();
                //eventLog1.Dispose();
                return;
            }
            catch (Exception Ex)
            {
                ErrorReport("Closing STKService error:" + Ex.Message);
            }


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
          
                    System.Diagnostics.Trace.WriteLine(ELET.ToString() + " :" + Message);
              
                //this.eventLog1.WriteEntry(System.DateTime.Now.ToString() + Message, ELET);
                if (ELET == System.Diagnostics.EventLogEntryType.Error)
                    System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString() + " Error: " + Message);
                else if (ELET == System.Diagnostics.EventLogEntryType.Warning)
                    System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString() + " Warning: " + Message);
                else
                    System.Diagnostics.Trace.WriteLine(System.DateTime.Now.ToString() + Message);
            
            return;
        }
        #endregion log functions

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
