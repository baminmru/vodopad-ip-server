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
             public DataRow dr;
             public STKTVMain.TVMain TvMain;
         }
        Int16 archType_hour  = 3;
        Int16 archType_day = 4;
        //Int16 archType_moment = 1;
        public ManualResetEvent pStopServiceEvent;
        public ManualResetEvent pStopApprovedEvent;
       // private System.Threading.Thread pMainThread = null;
        private Dictionary<Int32, System.Threading.Thread> Threads;
        //private object[] threads;
        private System.Threading.Thread pMainThread = null;
        
        private const int cWaitInterval = 120000;
        private Object thisLock = new Object();
        private LogInfo pLogParams = new LogInfo();
        
        Int32 id_bd;
        public STKService()
        {
            //Int16 b = 5;
            //while (b < 6)
            //{
            //    b = 5;
            //}
            InitializeComponent();
            pStopServiceEvent = new ManualResetEvent(false);
            pStopApprovedEvent = new ManualResetEvent(false);
        }

        public void OnStart()
        {
            Threads = new Dictionary<Int32, Thread>();
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
          
        }
        public int DivID=0;
        private Int32 NextPort = 0;
        private void MainThread()
        {

            NextPort = 0;
            Threads = new Dictionary<Int32, Thread>();
            eventLog1.BeginInit();
            STKTVMain.TVMain TvMain;
            DataRow dr;
            bool bLogged = false;
            TvMain = new STKTVMain.TVMain();
            
            do
            {
                if (pStopServiceEvent.WaitOne(1, false))
                {
                    try
                    {
                        //TvMain.EndInit(); 
                        //pRefService.InfoReport("Exiting working thread...");
                        InfoReport("Exiting working thread...");
                        pStopApprovedEvent.Set();
                        return;
                    }
                    catch (Exception Ex)
                    {
                        ErrorReport("Exiting working thread error: " + Ex.Message);
                       
                        return;
                    }
                }
                try
                {
                    //pRefService.InfoReport("Try DB login.");
                    InfoReport("Try DB login.");
                    if (TvMain.Init(true) == true)
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
            InfoReport("DB Initialization OK");
            //bool isgetplantable = false;
            
            //Для проверки работы сервиса
            //Int16 a = 5;
            //while (a < 6)
            //{
            //    a = 5;
            //}

            do
            {


                System.Data.DataTable oRS;
                ThreadObj throbj = null;

                oRS = null;

                if (DivID==0)
                    oRS = TvMain.GetDBDevicePlanList();
                else
                    oRS = TvMain.GetDBOneDevicePlanList(DivID);

                if (oRS != null)
                {
                    //isgetplantable = true; 
                    NextPort = 0;
                    if (oRS.Rows.Count > 0)
                    {
                        InfoReport("Checking " + oRS.Rows.Count  + " active plan(s) at " + DateTime.Now.ToString() );
                        for (int i = 0; i < oRS.Rows.Count; i++)
                        {

                            try
                            {
                                dr = oRS.Rows[i];
                                id_bd = Convert.ToInt32(dr["id_bd"].ToString());
                                //if (Threads[id_bd] == null)
                                if (!Threads.ContainsKey(id_bd))
                                {
                                    
                                    throbj = new ThreadObj();
                                    //throbj.dr = oRS.Rows[i];
                                    throbj.dr = null;
                                    throbj.dr = dr;
                                    
                                    //throbj.TvMain = TvMain;
                                    throbj.TvMain = new STKTVMain.TVMain();
                                    throbj.TvMain.Init(false);
                                    //throbj.TvMain.connect();

                                    Threads.Add(id_bd, new Thread(new ParameterizedThreadStart(this.CallThr)));
                                    Threads[id_bd].SetApartmentState(ApartmentState.MTA);
                                    Threads[id_bd].Name = "Service thread " + id_bd.ToString();
                                    //InfoReport("Starting  thread " + id_bd.ToString());
                                    Threads[id_bd].Start(throbj);

                                }
                                else
                                {
                                    if (Threads[id_bd].IsAlive == false)
                                    {
                                        throbj = new ThreadObj();
                                        //throbj.dr = oRS.Rows[i];
                                        throbj.dr = null;
                                        throbj.dr = dr;
                                        //throbj.TvMain = TvMain;
                                        throbj.TvMain = new STKTVMain.TVMain();
                                        throbj.TvMain.Init(false);
                                        Threads[id_bd] = new Thread(new ParameterizedThreadStart(this.CallThr));
                                        Threads[id_bd].SetApartmentState(ApartmentState.MTA);
                                        Threads[id_bd].Name = "Service thread " + id_bd.ToString();
                                        //InfoReport("Restarting  thread " + id_bd.ToString());
                                        Threads[id_bd].Start(throbj);

                                    }
                                }

                            }
                            catch (Exception Ex)
                            {
                                ErrorReport("Thread " + id_bd.ToString() + " error:" + Ex.Message);
                            }
                        }
                    }
                    oRS = null;
                    throbj = null;
             
                }
               
            } while (!pStopServiceEvent.WaitOne(TvMain.RequestInterval, false));

            try
            {
                InfoReport("Closing STKService...");
                Threads.Clear();
                Threads = null;
                dr = null;

                // close nport library
                TvMain.EndInit(true);
                TvMain.CloseDBConnection();
                TvMain = null;
                //eventLog1.Close();
                eventLog1.Dispose();
                return;
            }
            catch (Exception Ex)
            {
                ErrorReport("Closing STKService error:" + Ex.Message);
            }
            
            
        }
             

        public void OnStop()
        {
            pStopServiceEvent.Set();
        }
        

       
        
        private void CallThr(object objct)
        {

            ThreadObj obj;
            obj = (ThreadObj)objct;
            DateTime SrvDate;
            Boolean DeviceOK ;
            try
            {

                Int32 id_bdc;
                bool chour = false, ccurr = false, c24 = false,csum = false;
                id_bdc = Convert.ToInt32(obj.dr["id_bd"].ToString());
                if (obj.dr["chour"].ToString() == "1") chour = true;
                if (obj.dr["ccurr"].ToString() == "1") ccurr = true;
                if (obj.dr["c24"].ToString() == "1") c24 = true;
                if (obj.dr["csum"].ToString() == "1") csum = true;
                SrvDate = DateTime.Now;
                try
                {
                    SrvDate = Convert.ToDateTime(obj.dr["ServerDate"].ToString());
                }
                catch { 
                }

                
                if (chour || ccurr || c24 || csum)
                {
                    lock (thisLock)
                    {

                        if (obj.TvMain.DeviceInit(id_bdc, NextPort + 1))  //NextPort + 1
                        {
                            DeviceOK = true;
                            NextPort = NextPort + 1;
                        }
                        else
                        {
                            DeviceOK = false;
                        }
                    }
                    
                    if (DeviceOK)
                    {
                        InfoReport("CounterID " + obj.dr["id_bd"].ToString() + " NPORT Device ready!");
                        InfoReport("CounterID " + obj.dr["id_bd"].ToString() + " MAP to port " + obj.TvMain.TVD.RetPortID );
                        InfoReport("CounterID " + obj.dr["id_bd"].ToString() + " Driver " + obj.TvMain.TVD.CounterName()); 

                        obj.TvMain.connect();
                                               

                        DateTime ddd;
                        ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(obj.dr["dnexthour"].ToString());
                        }
                        catch
                        {

                        }
                        if (chour && ddd <= SrvDate)
                        {

                            InfoReport("Thread " + id_bdc.ToString() + " read hour archive at " + SrvDate.ToString());
                            //DateTime dlasthour, nowhour,tempdate;
                            DateTime tempdate;
                            Int16 numhour;
                            numhour = Convert.ToInt16("0" + obj.dr["numhour"].ToString());

                            if (ddd.AddHours(numhour) <= SrvDate.AddHours(1))
                            {

                                //dlasthour = Convert.ToDateTime(obj.dr["dlasthour"].ToString());
                                tempdate = ddd;
                                tempdate = tempdate.AddHours(-1);

                                for (int j = 0; j <= numhour - 1; j++)
                                {
                                    try
                                    {
                                        tempdate = tempdate.AddHours(1);
                                        //transaction = TvMain.dbconnect.BeginTransaction(IsolationLevel.ReadCommitted)

                                        String str;
                                        //TvMain.ClearDBarch(DateTimePickerAfter.Value, DateTimePickerBefor.Value, archType_hour, ListBox1.SelectedItem("ID_BD").ToString)
                                        //InfoReport("readarch");
                                        str = obj.TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                        //InfoReport("readarch->" + str );

                                        if (str.Length == 0)
                                        {
                                            //transaction.Rollback()
                                            WarningReport("CounterID: " + obj.dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                            
                                        }
                                        else
                                        {

                                            if (str.Substring(1, 6) != "Ошибка")
                                            {
                                                obj.TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc);
                                                InfoReport("CounterID " + id_bdc.ToString() + " -> " + obj.TvMain.WriteArchToDB());
                                            }
                                            else
                                            {
                                                //transaction.Rollback()
                                                WarningReport("CounterID: " + obj.dr["id_bd"].ToString() + " " + str + tempdate.ToString());

                                            }
                                        }

                                    }
                                    catch (Exception Ex)
                                    {
                                        //transaction.Rollback()
                                        ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " failed, " + Ex.Message);
                                    }
                                }//for (int j = 0;j <= razn.Hours + razn.Days * 24;j++)

                                obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnexthour", ddd.AddHours(numhour));
                                tempdate = new DateTime(ddd.Year, ddd.Month, ddd.Day, ddd.Hour, 0, 0);
                                obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlasthour", tempdate.AddSeconds(-1));

                            }
                            else
                            {


                                //dlasthour = Convert.ToDateTime(obj.dr["dlasthour"].ToString());
                                tempdate = ddd;
                                tempdate = tempdate.AddHours(1);

                                for (int j = 0; j <= numhour - 1; j++)
                                {
                                    try
                                    {
                                        tempdate = tempdate.AddHours(-1);
                                        //transaction = TvMain.dbconnect.BeginTransaction(IsolationLevel.ReadCommitted)

                                        String str;
                                        //TvMain.ClearDBarch(DateTimePickerAfter.Value, DateTimePickerBefor.Value, archType_hour, ListBox1.SelectedItem("ID_BD").ToString)
                                        
                                        str = obj.TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                        if (str.Substring(1, 6) != "Ошибка")
                                        {
                                            obj.TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc);
                                            InfoReport("CounterID " + id_bdc.ToString() + " -> " + obj.TvMain.WriteArchToDB());
                                        }
                                        else
                                        {
                                            //transaction.Rollback()
                                            WarningReport("CounterID: " + obj.dr["id_bd"].ToString() + " " + str + tempdate.ToString());

                                        }

                                    }
                                    catch (Exception Ex)
                                    {
                                        //transaction.Rollback()
                                        ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " failed, " + Ex.Message);
                                    }
                                }//for (int j = 0;j <= razn.Hours + razn.Days * 24;j++)

                                obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnexthour", ddd.AddMinutes(Convert.ToDouble(obj.dr["icall"].ToString())));
                                tempdate = new DateTime(ddd.Year, ddd.Month, ddd.Day, ddd.Hour, 0, 0);
                                obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlasthour", tempdate.AddSeconds(-1));
                            }

                        }//if (chour)


                        ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(obj.dr["dnext24"].ToString());
                        }
                        catch
                        {

                        }

                        if (c24 && ddd <= SrvDate)
                        {


                            InfoReport("Thread " + id_bdc.ToString() + " read day archive at " + SrvDate.ToString());
                            DateTime tempdate;
                            Int16 num24;
                            num24 = Convert.ToInt16(obj.dr["num24"].ToString());

                            if (ddd.AddDays(num24) <= SrvDate.AddDays(1))
                            {

                                tempdate = ddd;
                                tempdate = tempdate.AddDays(-1);
                                try
                                {
                                    for (int j = 0; j <= num24 - 1; j++)
                                    {

                                        tempdate = tempdate.AddDays(1);
                                        //transaction = TvMain.dbconnect.BeginTransaction(IsolationLevel.ReadCommitted)

                                        String str;
                                        //TvMain.ClearDBarch(DateTimePickerAfter.Value, DateTimePickerBefor.Value, archType_hour, ListBox1.SelectedItem("ID_BD").ToString)
                                        
                                        str = obj.TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                        if (str.Substring(1, 6) != "Ошибка")
                                        {
                                            obj.TvMain.ClearDBArchString(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc);
                                            InfoReport("CounterID " + id_bdc.ToString() + " -> " + obj.TvMain.WriteArchToDB());
                                            //obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnext24", ddd.AddHours(Convert.ToDouble(obj.dr["icall24"].ToString())));
                                            obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnext24", ddd.AddDays (num24));
                                            tempdate = new DateTime(ddd.Year, ddd.Month, ddd.Day, 0, 0, 0);
                                            obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastday", tempdate.AddSeconds(-1));
                                        }
                                        else
                                        {
                                            WarningReport("CounterID: " + obj.dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                        }

                                    }//for (int j = 0; j <= razn.Days; j++)

                                }//try
                                catch (Exception Ex)
                                {
                                    //transaction.Rollback()
                                    ErrorReport("CounterID: " + obj.dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);
                                }

                            }
                            else
                            {
                                tempdate = ddd;
                                tempdate = tempdate.AddDays(1);
                                try
                                {
                                    for (int j = 0; j <= num24 - 1; j++)
                                    {

                                        tempdate = tempdate.AddDays(-1);
                                        //transaction = TvMain.dbconnect.BeginTransaction(IsolationLevel.ReadCommitted)

                                        String str;
                                        //TvMain.ClearDBarch(DateTimePickerAfter.Value, DateTimePickerBefor.Value, archType_hour, ListBox1.SelectedItem("ID_BD").ToString)
                                        
                                        str = obj.TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                        if (str.Substring(1, 6) != "Ошибка")
                                        {
                                            obj.TvMain.ClearDBArchString(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc);
                                            InfoReport("CounterID " + id_bdc.ToString() + " -> " + obj.TvMain.WriteArchToDB());
                                            obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnext24", ddd.AddHours(Convert.ToDouble(obj.dr["icall24"].ToString())));
                                            tempdate = new DateTime(ddd.Year, ddd.Month, ddd.Day, 0, 0, 0);
                                            obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastday", tempdate.AddSeconds(-1));
                                        }
                                        else
                                        {
                                            WarningReport("CounterID: " + obj.dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                        }

                                    }//for (int j = 0; j <= razn.Days; j++)

                                }//try
                                catch (Exception Ex)
                                {
                                    //transaction.Rollback()
                                    ErrorReport("CounterID: " + obj.dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);
                                }
                            }
                        }//if (c24)

                        if (ccurr)

                            ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(obj.dr["dnextcurr"].ToString());
                        }
                        catch
                        {

                        }


                        if (ccurr && ddd <= SrvDate)
                        {
                            DateTime tempdate;
                            Double nmin;
                            InfoReport("Thread " + id_bdc.ToString() + " read current data at " + SrvDate.ToString());
                            try
                            {

                                String str;
                                str = obj.TvMain.readmarch();
                                InfoReport("CounterID " + id_bdc.ToString() + " -> " +str);
                                if(str.Substring(1, 6) != "Ошибка")
                                    {
                                    InfoReport("CounterID " + id_bdc.ToString() + " -> " + obj.TvMain.WritemArchToDB());
                                    tempdate=ddd;
                                    nmin=Convert.ToDouble(obj.dr["icallcurr"].ToString());

                                    while (tempdate.AddMinutes(nmin) <= SrvDate)
                                    {
                                        tempdate = tempdate.AddMinutes(nmin);
                                    }
                                    tempdate = tempdate.AddMinutes(nmin);

                                    obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextcurr", tempdate);
                                }
                            }//try
                            catch (Exception Ex)
                            {
                                ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);
                            }
                        }//if (ccurr)


                        if (csum)

                            ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(obj.dr["dnextsum"].ToString());
                        }
                        catch
                        {

                        }


                        if (csum && ddd <= SrvDate)
                        {
                            DateTime tempdate;
                            Double nmin;
                            InfoReport("Thread " + id_bdc.ToString() + " read total data at " + SrvDate.ToString());
                            try
                            {

                                String str;
                                str = obj.TvMain.readtarch();
                                InfoReport("CounterID " + id_bdc.ToString() + " -> " + str);
                                if (str.Substring(1, 6) != "Ошибка")
                                {
                                    InfoReport("CounterID " + id_bdc.ToString() + " -> " + obj.TvMain.WriteTArchToDB());
                                    tempdate = ddd;
                                    nmin = Convert.ToDouble(obj.dr["icallsum"].ToString());

                                    while (tempdate.AddMinutes(nmin) <= SrvDate)
                                    {
                                        tempdate = tempdate.AddMinutes(nmin);
                                    }
                                    tempdate = tempdate.AddMinutes(nmin);

                                    obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextsum", tempdate);
                                }
                            }//try
                            catch (Exception Ex)
                            {
                                ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);
                            }
                        }//if (ccurr)

                        // finalization 

                        obj.TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastcall", SrvDate);




                        try
                        {
                            if (obj != null)
                            {
                                if (obj.TvMain != null)
                                {
                                    obj.TvMain.EndInit(false);
                                    InfoReport("Thread " + id_bdc.ToString() + " close NPort ");
                                }
                             }
                        }
                        catch (System.Exception closeEx)
                        {
                            ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " thread failed, " + closeEx.Message);
                        }
              
                    }
                    else
                    {
                        ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " NPORT Device initialization Error! Check IP:" + obj.dr["NPIP"].ToString()  );
                    }
                }
                else
                {
                    InfoReport("CounterID " + obj.dr["id_bd"].ToString() + " plan is active, but no tasks to process!");
                }
            }
            catch (System.Exception threadEx)
            {
                ErrorReport("CounterID " + obj.dr["id_bd"].ToString() + " thread failed, " + threadEx.Message);
            }

            obj = null;
       
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
