using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;

namespace CurrentLoader
{
    public class ThreadObj
     {
         public System.Data.DataRow dr;
         public STKTVMain.TVMain TvMain;
     }

    class DeviceProcessor
    {
        private System.Diagnostics.EventLog eventLog1;

        private void InitializeComponent()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // eventLog1
            // 
            this.eventLog1.Log = "Application";
            this.eventLog1.Source = "AREALMODBUSService";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }
         public void Run()
        {
            InitializeComponent(); 
            //InfoReport("Starting DeviceThread...ID=" + DivID.ToString() );
            MainThread();
            //InfoReport("Stop DeviceThread...ID=" + DivID.ToString());
        }

        public int DivID=0;
     
        private STKTVMain.TVMain TvMain;

        private void MainThread()
        {

          
            eventLog1.BeginInit();
           
            DataRow dr=null;
            bool bLogged = false;
            TvMain = new STKTVMain.TVMain();
            
            try
            {
            
                if (TvMain.Init() == true)
                {
                    bLogged = true;
                }
                else
                {
                    WarningReport("Unable to login, check credentials");
                    InfoReport(TvMain.GetEnvInfo());
                    return;
                }
            }
            catch (Exception Ex)
            {
            
                ErrorReport("Login failed, try again... " + Ex.Message);
                InfoReport(TvMain.GetEnvInfo());
            }
      
            //InfoReport("DB Initialization OK");
            System.Data.DataTable oRS;
            if (bLogged)
            {
                oRS = null;
                String q;
                q = "select plancall.*,npip,nppassword,ipport,transport,sysdate ServerDate from bdevices join plancall on bdevices.id_bd=plancall.id_bd where  ( nplock is null or nplock <sysdate ) and npquery=1 " +
                    " and bdevices.id_bd=" + DivID.ToString() + " ";
                oRS = TvMain.QuerySelect(q) ;
                if (oRS != null)
                {

                    if (oRS.Rows.Count > 0)
                    {
                        try
                        {
                            dr = oRS.Rows[0];
                            DeviceThread(dr);

                        }
                        catch (Exception Ex)
                        {
                            ErrorReport("Прибор ID=  " + DivID.ToString() + " error:" + Ex.Message);
                            dr = null;
                        }
                    }
                    oRS = null;

                }




                try
                {
                    //InfoReport("Closing Device thread...");

                    //dr = null;

                    TvMain.ClearDuration(); 
                    // close transport
                    TvMain.DeviceClose();
                    TvMain.CloseDBConnection();
                    TvMain = null;
                    eventLog1.Dispose();
                    return;
                }
                catch (Exception Ex)
                {
                    ErrorReport("Closing DeviceThread error:" + Ex.Message);
                }

                
            }
            
            
        }
             
        private void DeviceThread( DataRow dr)
        {

          
            DateTime SrvDate;
            DateTime ddd;
            Boolean DeviceOK ;
            Int16 archType_moment = 1;
            Int16 ncall=0;
            Int16 nmaxcall=5;
            Int16 minrepeat = 5;
            
            try
            {
                #region "init"
                ncall = Convert.ToInt16(dr["ncall"]);
                nmaxcall = Convert.ToInt16(dr["nmaxcall"]);
                minrepeat = Convert.ToInt16(dr["minrepeat"]);
                Int32 id_bdc;
           
                id_bdc = Convert.ToInt32(dr["id_bd"].ToString());
             
                SrvDate = DateTime.Now;
                try
                {
                    SrvDate = Convert.ToDateTime(dr["ServerDate"].ToString());
                }
                catch { 
                }

                
              
                {
                    TvMain.ClearDuration();
                    if (TvMain.LockDevice(id_bdc, 60 * 40, false))
                    {
                        if (TvMain.DeviceInit(id_bdc)) 
                        {
                            DeviceOK = true;
                            //TvMain.SaveLog(id_bdc,0,"??",1,"Инициализация транспортного уровня:OK");
                        }
                        else
                        {
                            bool SkipErr = false;
                            if( TvMain.TVD !=null){
                                if (TvMain.TVD.Transport == 0){ 
                                    if( TvMain.TVD.ComPort == "")
                                    {
                                        SkipErr = true;
                                    }
                                    if (TvMain.PortBusy)
                                    {
                                        SkipErr = true;
                                    }
                                }

                            }
                            if (!SkipErr)
                            {
                                string tError = "";
                                try
                                {
                                    tError = TvMain.ConnectStatus() ; 
                                }
                                catch (Exception)
                                {

                                    tError = "";
                                }


                                if (tError != "")
                                {
                                    TvMain.WriteErrToDB(id_bdc, DateTime.Now,  tError);
                                    TvMain.SaveLog(id_bdc, 0, "??", 1, tError);
                                }else{
                                    if (TvMain.TVD != null)
                                    {
                                        if(TvMain.TVD.DriverTransport!=null)
                                            tError = TvMain.TVD.DriverTransport.GetError;
                                    }
                                    TvMain.WriteErrToDB(id_bdc, DateTime.Now, "Ошибка транспорта. " + tError);
                                    TvMain.SaveLog(id_bdc, 0, "??", 1, "Ошибка транспорта. " +tError);
                                 }
                             
                                
                           
                               


                              
                               
                            }
                            DeviceOK = false;
                            TvMain.UnLockDevice(id_bdc); 
                            TvMain.SaveLog(id_bdc,0,"??",1,"Снятие блокировки устройства");
                        }
                    }
                    else
                    {
                        TvMain.SaveLog(id_bdc, 0, "??", 1, "Тепловычислитель занят");
                        DeviceOK = false;
                    }
                    if (DeviceOK)
                    {
                      

                            TvMain.connect();
                            if (TvMain.isConnected() == false)
                            {
                                string tError = "";
                                try
                                {
                                    tError = TvMain.ConnectStatus();
                                }
                                catch (Exception)
                                {

                                    tError = "";
                                }
                                if(tError !=""){
                                    TvMain.WriteErrToDB(id_bdc, DateTime.Now, tError);
                                    TvMain.SaveLog(id_bdc, 0, "??", 1, tError);
                                }else{
                                    TvMain.WriteErrToDB(id_bdc, DateTime.Now, "Ошибка соединения. "+ tError);
                                    TvMain.SaveLog(id_bdc, 0, "??", 1, "Ошибка соединения. "+tError);
                                }

                                if (ncall+1 < nmaxcall)
                                {
                                    TvMain.SetNCALLToPlanCall(id_bdc.ToString(), ncall + 1);
                                    TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", DateTime.Now);
                                }
                                else
                                {
                                    TvMain.SetNCALLToPlanCall(id_bdc.ToString(), 0);

                                    ddd = SrvDate;
                                    try
                                    {
                                        ddd = Convert.ToDateTime(dr["dnextcurr"].ToString());
                                    }
                                    catch (System.Exception ex)
                                    {
                                        InfoReport("Прибор ID=  " + id_bdc.ToString() + " error converting dnextcurr :" + dr["dnextcurr"].ToString());
                                        TvMain.SaveLog(id_bdc, 3, "??", 0, "Ошибка преобразования даты (dnextcurr) :" + dr["dnextcurr"].ToString() + " " + ex.Message);
                                    }
                                    while (ddd < SrvDate)
                                    {
                                        ddd = ddd.AddHours(1);
                                    }
                                    ddd = ddd.AddMinutes(-minrepeat);  

                                    TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", ddd);
                                }

                                ErrorReport("Прибор ID " + dr["id_bd"].ToString() + " Counter initialization Error!");
                                DeviceOK = false;

                                
                            }
                            else
                            {
                                TvMain.SaveLog(id_bdc, 0, "??", 1, "Соединение с тепловычислителем:OK");
                            }
                        
                        
                    }
                #endregion "init"
                    if (DeviceOK)
                    {

                        TvMain.SetNCALLToPlanCall(id_bdc.ToString(), 0);


 #region "moment"
                        

                       ddd = SrvDate;

                        if (TvMain.TVD.IsConnected() )
                        {
                      
                            InfoReport("Прибор ID=  " + id_bdc.ToString() + " чтение текущих архивов на " + ddd.ToString());
                            try
                            {
                                if(TvMain.LockDevice(id_bdc,400,true)){
                                    TvMain.HoldLine();
                                String str;
                                TvMain.ClearDuration();
                                str = TvMain.readmarch();
                                if (str.Length == 0)
                                {

                                    TvMain.WriteErrToDB(id_bdc, SrvDate, "Ошибка чтения архива");
                                    TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "Ошибка чтения текущего архива");
                                }
                                else
                                {
                                    if (str.Substring(0, 6) != "Ошибка")
                                    {
                                        if (TvMain.TVD.isMArchToDBWrite)
                                        {
                                            TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "Текущий архив" + ":OK");
                                            TvMain.WritemArchToDB();

                                        }
                                        //);
                                       

                                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextcurr", SrvDate.AddMilliseconds(500) );
                                    }
                                    else {
                                        TvMain.WriteErrToDB(id_bdc, SrvDate, str);
                                        TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "Ошибка чтения текущего архива " +str);



                                            TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextcurr", SrvDate.AddMilliseconds(500));
                                        }
                                }
                                //TvMain.UnLockDevice(id_bdc);
                            }
                            }//try
                            catch (Exception Ex)
                            {
                                TvMain.WriteErrToDB(id_bdc, SrvDate, "Ошибка чтения архива:" + Ex.Message);  
                                ErrorReport("Прибор ID " + dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);
                            }
                          
                        }//if (ccurr)






                        #endregion "moment"



                        #region "commands"
                        if (TvMain.TVD.IsConnected())
                        {

                            InfoReport("Прибор ID=  " + id_bdc.ToString() + " Отработка очереди комманд ");
                            try
                            {
                                if (TvMain.LockDevice(id_bdc, 2000, true))
                                {
                                    TvMain.HoldLine();
                                    TvMain.ClearDuration();
                                    if (TvMain.TVD.ProcessComands() > 0)
                                    {
                                        TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "Отработка очереди комманд: OK");
                                    }
                                }
                            }//try
                            catch (Exception Ex)
                            {
                                TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "Отработка очереди комманд:" + Ex.Message);
                                ErrorReport("Прибор ID " + dr["id_bd"].ToString() + " commands processing error, " + Ex.Message);
                            }

                        }
                        #endregion

                        TvMain.UnLockDevice(id_bdc);
                         string transpname  = "";
                         if (TvMain.TVD != null)
                         {
                             if (TvMain.TVD.Transport == 0)
                             {
                                 transpname = TvMain.TVD.ComPort;
                             }

                         }
                        TvMain.SaveLog(id_bdc, 0, "??", 1, "Закрытие канала. " + transpname);
                     
        
                    }
                    else
                    {
                        ErrorReport("Прибор ID " + dr["id_bd"].ToString() + " transport initialization Error!"  );
                    }
                }
              
            }
            catch (System.Exception threadEx)
            {
                ErrorReport("Прибор ID " + dr["id_bd"].ToString() + " thread failed, " + threadEx.Message);
            }
        }



       


        #region log functions
        private Object thisLock = new Object();
      
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
            try {
           
               
                    try
                    {
                        string FileName = "";
                        if (FileName == string.Empty || FileName == "") FileName = System.IO.Path.GetDirectoryName(GetType().Assembly.Location)+"LOG"+DateTime.Today.ToString("yyyymmdd")  +".txt";
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
                
            
            }catch{
            }
            return;
        }
        #endregion log functions

      
    }

    
}
