using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Data.OracleClient ;

namespace STKService
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
            this.eventLog1.Source = "STKService";
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
                oRS = TvMain.GetDBOneDevicePlanList(DivID);
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
                            ErrorReport("������ ID=  " + DivID.ToString() + " error:" + Ex.Message);
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

                    if (dr != null)
                    {
                        AnalizeDevice(dr);
                    }
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
            Boolean DeviceOK ;
            Int16 archType_hour = 3;
            Int16 archType_day = 4;
            Int16 archType_moment = 1;
            Int16 archType_total = 2;
            Int16 ncall=0;
            Int16 nmaxcall=5;
            Int16 minrepeat = 5;
            try
            {

                ncall = Convert.ToInt16(dr["ncall"]);
                nmaxcall = Convert.ToInt16(dr["nmaxcall"]);
                minrepeat = Convert.ToInt16(dr["minrepeat"]);
                Int32 id_bdc;
                bool chour = false, ccurr = false, c24 = false,csum = false;
                id_bdc = Convert.ToInt32(dr["id_bd"].ToString());
                if (dr["chour"].ToString() == "1") chour = true;
                if (dr["ccurr"].ToString() == "1") ccurr = true;
                if (dr["c24"].ToString() == "1") c24 = true;
                if (dr["csum"].ToString() == "1") csum = true;
                SrvDate = DateTime.Now;
                try
                {
                    SrvDate = Convert.ToDateTime(dr["ServerDate"].ToString());
                }
                catch { 
                }

                
                //if (chour || ccurr || c24 || csum)
                {
                    TvMain.ClearDuration();
                    if (TvMain.LockDevice(id_bdc, 120, false))
                    {
                        if (TvMain.DeviceInit(id_bdc)) 
                        {
                            DeviceOK = true;
                            //TvMain.SaveLog(id_bdc,0,"??",1,"������������� ������������� ������:OK");
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
                                    TvMain.WriteErrToDB(id_bdc, DateTime.Now, "������ ����������. " + tError);
                                    TvMain.SaveLog(id_bdc, 0, "??", 1, "������ ����������. " +tError);
                                 }
                             
                                
                                if (ncall +1< nmaxcall)
                                {
                                    TvMain.SetNCALLToPlanCall(id_bdc.ToString(), ncall + 1);
                                    TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", DateTime.Now);
                                }
                                else
                                {
                                    TvMain.SetNCALLToPlanCall(id_bdc.ToString(), 0);
                                    
                                    DateTime  ddd = SrvDate;
                                    try
                                    {
                                        ddd = Convert.ToDateTime(dr["dnextcurr"].ToString());
                                    }
                                    catch
                                    {
                                        InfoReport("������ ID=  " + id_bdc.ToString() + " error converting dnextcurr :" + dr["dnextcurr"].ToString());
                                    }
                                    while (ddd < SrvDate)
                                    {
                                        ddd = ddd.AddHours(1);
                                    }
                                    ddd = ddd.AddMinutes(-minrepeat);  

                                    TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", ddd);
                                }


                              
                                try
                                {


                                    VIPAnalizer.NodeAnalizer na = new VIPAnalizer.NodeAnalizer();
                                    na.CheckStatus(TvMain, id_bdc, 3, 2);
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                            DeviceOK = false;
                        }
                    }
                    else
                    {
                        TvMain.SaveLog(id_bdc, 0, "??", 1, "���������������� �����");
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
                                    TvMain.WriteErrToDB(id_bdc, DateTime.Now, "������ ����������. "+ tError);
                                    TvMain.SaveLog(id_bdc, 0, "??", 1, "������ ����������. "+tError);
                                }

                                if (ncall+1 < nmaxcall)
                                {
                                    TvMain.SetNCALLToPlanCall(id_bdc.ToString(), ncall + 1);
                                    TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", DateTime.Now);
                                }
                                else
                                {
                                    TvMain.SetNCALLToPlanCall(id_bdc.ToString(), 0);

                                    DateTime ddd = SrvDate;
                                    try
                                    {
                                        ddd = Convert.ToDateTime(dr["dnextcurr"].ToString());
                                    }
                                    catch
                                    {
                                        InfoReport("������ ID=  " + id_bdc.ToString() + " error converting dnextcurr :" + dr["dnextcurr"].ToString());
                                    }
                                    while (ddd < SrvDate)
                                    {
                                        ddd = ddd.AddHours(1);
                                    }
                                    ddd = ddd.AddMinutes(-minrepeat);  

                                    TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", ddd);
                                }

                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " Counter initialization Error!");
                                DeviceOK = false;

                                try
                                {

                                    VIPAnalizer.NodeAnalizer na = new VIPAnalizer.NodeAnalizer();
                                    na.CheckStatus  (TvMain, id_bdc, 3,2);
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                            else
                            {
                                TvMain.SaveLog(id_bdc, 0, "??", 1, "���������� � �����������������:OK");
                            }
                        
                        
                    }
                    if (DeviceOK)
                    {

                        TvMain.SetNCALLToPlanCall(id_bdc.ToString(), 0);
                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlock", DateTime.Now);

                        DateTime ddd;
                        ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(dr["dnexthour"].ToString());
                        }
                        catch
                        {
                            InfoReport("������ ID=  " + id_bdc.ToString() + " error while convert dnexthour :" + dr["dnexthour"].ToString());
                        }
                        if (TvMain.TVD.IsConnected()  && chour && ddd <= SrvDate)
                        {
                           
                            //DateTime dlasthour, nowhour,tempdate;
                            DateTime tempdate;
                            Int16 numhour;
                            numhour = Convert.ToInt16("0" + dr["numhour"].ToString());
                            Int16 icall = Convert.ToInt16("0" + dr["icall"].ToString());
                            if (TvMain.LockDevice(id_bdc, 120 * numhour,true))
                            {
                                TvMain.HoldLine();
                                
                                if (ddd.AddHours(numhour) <= SrvDate.AddHours(1))
                                {
                                    // ����������, ������ ������
                                   
                                    tempdate = ddd;
                                    tempdate = tempdate.AddHours(-1);

                                    for (int j = 0; j < numhour ; j++)
                                    {
                                        if (TvMain.TVD.IsConnected())
                                        {
                                            try
                                            {
                                                tempdate = tempdate.AddHours(1);
                                                if (TvMain.CheckForArch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc) == false)
                                                {
                                                    InfoReport("������ ID=  " + id_bdc.ToString() + " ������ ����� �� ���� " + tempdate.ToString());
                                                    String str;
                                                    TvMain.ClearDuration();
                                                    str = TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);

                                                    if (str.Length == 0)
                                                    {
                                                        WarningReport("������ ID: " + dr["id_bd"].ToString() + " �� ������� ������� ����� �� ����:" + tempdate.ToString());
                                                        TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ �������� ������ �� ����:" + tempdate.ToString());
                                                    }
                                                    else
                                                    {
                                                        if (str.Substring(0, 6) != "������")
                                                        {

                                                            if (TvMain.TVD.isArchToDBWrite)
                                                            {
                                                                TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������� ����� �� ����:" + tempdate.ToString() + ":OK");
                                                                TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc);
                                                                TvMain.WriteArchToDB();
                                                                TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlasthour", tempdate.AddSeconds(-1));
                                                            }

                                                        }
                                                        else
                                                        {
                                                            TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ �������� ������ �� ����:" + tempdate.ToString() + " " + str);
                                                            WarningReport("������ ID: " + dr["id_bd"].ToString() + " �� �������  ������� ����� �� " + tempdate.ToString() + "\r\n" + str);
                                                            TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlasthour", tempdate.AddSeconds(-1));
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    InfoReport("������ ID=  " + id_bdc.ToString() + " ����� �� ���� " + tempdate.ToString() + "  ��� ���� � ����");
                                                }

                                            }
                                            catch (Exception Ex)
                                            {
                                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " failed, " + Ex.Message);
                                            }
                                        }
                                 
                                    }// end for

                                    // ��������� ���������� �����, ������� ���� �������� �� ���
                                    if (TvMain.TVD.IsConnected())
                                    {
                                        TvMain.AddHourToPlanCall(id_bdc.ToString(), "dnexthour", numhour);
                                    }
                                }
                                else
                                {
                                    tempdate = SrvDate;
                                    //tempdate = tempdate.AddHours(1);
                                    bool ReadHOK;
                                    ReadHOK = false;

                                    // ��� �� ����������, ������ ������ �����
                                    for (int j = 0; j < numhour; j++)
                                    {
                                        if (TvMain.TVD.IsConnected())
                                        {
                                            try
                                            {
                                                tempdate = tempdate.AddHours(-1);


                                                String str;
                                                if (TvMain.CheckForArch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc) == false)
                                                {
                                                    InfoReport("������ ID=  " + id_bdc.ToString() + " ������ ����� �� ���� " + tempdate.ToString());
                                                    TvMain.ClearDuration();
                                                    str = TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                                    if (str.Length == 0)
                                                    {

                                                        WarningReport("������ ID: " + dr["id_bd"].ToString() + " �� �������  ������� ����� �� " + tempdate.ToString());
                                                        TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ �������� ������ �� ����:" + tempdate.ToString());

                                                    }
                                                    else
                                                    {
                                                        if (str.Substring(0, 6) != "������")
                                                        {

                                                            if (TvMain.TVD.isArchToDBWrite)
                                                            {
                                                                TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������� ����� �� ����:" + tempdate.ToString() + ":OK");
                                                                TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc);
                                                                TvMain.WriteArchToDB();

                                                            }
                                                            if (!ReadHOK)
                                                            {
                                                                ReadHOK = true;

                                                                // ��������� ��������� ������� �����
                                                                TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlasthour", tempdate.AddSeconds(-1));
                                                            }

                                                        }
                                                        else
                                                        {
                                                            WarningReport("������ ID: " + dr["id_bd"].ToString() + " �� �������  ������� ����� �� " + tempdate.ToString() + "\r\n" + str);
                                                            TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ �������� ������ �� ����:" + tempdate.ToString() + " " + str);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    InfoReport("������ ID=  " + id_bdc.ToString() + " ����� �� ���� " + tempdate.ToString() + "  ��� ���� � ����");
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " failed, " + Ex.Message);
                                            }
                                        }
                                    }// end for

                                    // �������� ��������� �� ������ ���������� �����
                                    if (TvMain.TVD.IsConnected())
                                    {
                                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnexthour", SrvDate); 
                                        TvMain.AddMinutesToPlanCall(id_bdc.ToString(), "dnexthour", icall);
                                    }

                                 
                               
                                }
                                //TvMain.UnLockDevice(id_bdc); 
                            } // Lock
                           
                        }//if (chour)


                        ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(dr["dnext24"].ToString());
                        }
                        catch
                        {
                            InfoReport("������ ID=  " + id_bdc.ToString() + " error while convert dnext24 :" + dr["dnext24"].ToString());
                        }

                        if (TvMain.TVD.IsConnected() && c24 && ddd <= SrvDate)
                        {

                           
                            
                            DateTime tempdate;
                            Int16 num24;
                            num24 = Convert.ToInt16(dr["num24"].ToString());
                            Int16 icall24 = Convert.ToInt16(dr["icall24"].ToString());
                            if (ddd.AddDays(num24) <= SrvDate.AddDays(1))
                            {
                                // ���� �� ������ ������� �� ���������� � ��������

                                tempdate = ddd;
                                tempdate = tempdate.AddDays(-1);
                                try
                                {
                                    if( TvMain.LockDevice(id_bdc,20 * num24,true )){
                                        TvMain.HoldLine();

                                        // ��������� �� ���������� ������������ ������
                                        for (int j = 0; j < num24 ; j++)
                                        {
                                            if (TvMain.TVD.IsConnected())
                                            {
                                                tempdate = tempdate.AddDays(1);

                                                if (TvMain.CheckForArch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc) == false)
                                                {
                                                    InfoReport("������ ID=  " + id_bdc.ToString() + " ������ �������� ����� �� ���� " + tempdate.ToString());
                                                    String str;
                                                    TvMain.ClearDuration();
                                                    str = TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                                    if (str.Length == 0)
                                                    {
                                                        WarningReport("������ ID: " + dr["id_bd"].ToString() + " �� ������� ����� �� ���� " + tempdate.ToString());
                                                        TvMain.SaveLog(id_bdc, archType_day, "??", 1, "������ ������ ��������� ������ �� ����:" + tempdate.ToString());
                                                    }
                                                    else
                                                    {
                                                        if (str.Substring(0, 6) != "������")
                                                        {


                                                            if (TvMain.TVD.isArchToDBWrite)
                                                            {
                                                                TvMain.SaveLog(id_bdc, archType_day, "??", 1, "�������� ����� �� ����:" + tempdate.ToString() + ":OK");
                                                                TvMain.ClearDBArchString(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc);
                                                                TvMain.WriteArchToDB();
                                                            }


                                                            // ���������� ��������� �������
                                                            TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastday", tempdate.AddSeconds(-1));
                                                        }
                                                        else
                                                        {
                                                            WarningReport("������ ID: " + dr["id_bd"].ToString() + " �� ������� ����� �� ���� " + tempdate.ToString() + "\r\n" + str);
                                                            TvMain.SaveLog(id_bdc, archType_day, "??", 1, "������ ������ ��������� ����� �� ����:" + tempdate.ToString() + " " + str);


                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    InfoReport("������ ID=  " + id_bdc.ToString() + " �������� ����� �� ���� " + tempdate.ToString() + " ��� ���� � ����");
                                                }
                                            }

                                        }//end for 

                                        // ��������� ����������� �����
                                        if (TvMain.TVD.IsConnected())
                                        {
                                            TvMain.AddDaysToPlanCall(id_bdc.ToString(), "dnext24", num24);
                                        }

                                        //TvMain.UnLockDevice(id_bdc); 
                                    }
                                }//try
                                catch (Exception Ex)
                                {
                                     ErrorReport("������ ID: " + dr["id_bd"].ToString() + " read day arch failed, " + Ex.Message);
                                }

                            }
                            else
                            {
                                tempdate = ddd;
                                tempdate = tempdate.AddDays(1);
                                bool ReadDOK;
                                ReadDOK = false;
                                try
                                {
                                    if( TvMain.LockDevice(id_bdc,120 * num24,true) ){ 
                                    for (int j = 0; j < num24 ; j++)
                                    {

                                        if (TvMain.TVD.IsConnected())
                                        {
                                            TvMain.HoldLine();
                                            tempdate = tempdate.AddDays(-1);

                                            String str;
                                            if (TvMain.CheckForArch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc) == false)
                                            {
                                                InfoReport("������ ID=  " + id_bdc.ToString() + " ������ �������� ����� �� ���� " + tempdate.ToString());
                                                TvMain.ClearDuration();
                                                str = TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                                if (str.Length == 0)
                                                {
                                                    WarningReport("������ ID: " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                                    TvMain.SaveLog(id_bdc, archType_day, "??", 1, "������ ������ ��������� ������ �� ����:" + tempdate.ToString());
                                                }
                                                else
                                                {
                                                    if (str.Substring(0, 6) != "������")
                                                    {

                                                        if (TvMain.TVD.isArchToDBWrite)
                                                        {
                                                            TvMain.SaveLog(id_bdc, archType_day, "??", 1, "�������� ����� �� ����:" + tempdate.ToString() + ":OK");
                                                            TvMain.ClearDBArchString(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc);
                                                            TvMain.WriteArchToDB();
                                                            if (!ReadDOK)
                                                            {
                                                                ReadDOK = true;
                                                                TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastday", tempdate.AddSeconds(-1));
                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        WarningReport("������ ID: " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                                        TvMain.SaveLog(id_bdc, archType_day, "??", 1, "������ ������ ��������� ������ �� ����:" + tempdate.ToString() + " " + str);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                InfoReport("������ ID=  " + id_bdc.ToString() + " �������� ����� �� ���� " + tempdate.ToString() + " ��� ���� � ����");
                                            }
                                        }

                                    }//for (int j = 0; j <= razn.Days; j++)
                                    if (TvMain.TVD.IsConnected())
                                    {
                                        TvMain.AddHourToPlanCall(id_bdc.ToString(), "dnext24", Convert.ToInt32(dr["icall24"].ToString()));
                                    }
                                    //TvMain.UnLockDevice(id_bdc );
                                    }
                                }//try
                                catch (Exception Ex)
                                {
                                    ErrorReport("������ ID: " + dr["id_bd"].ToString() + " read day arch failed, " + Ex.Message);
                                }
                            }
                        }//if (c24)

                        if ( ccurr)

                            ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(dr["dnextcurr"].ToString());
                        }
                        catch
                        {
                            InfoReport("������ ID=  " + id_bdc.ToString() + " error converting dnextcurr :" + dr["dnextcurr"].ToString());
                        }


                        if (TvMain.TVD.IsConnected() && ccurr && ddd <= SrvDate)
                        {
                            DateTime tempdate;
                            Double nmin;
                            InfoReport("������ ID=  " + id_bdc.ToString() + " ������ ������� ������� �� " + ddd.ToString());
                            try
                            {
                                if(TvMain.LockDevice(id_bdc,120,true)){
                                    TvMain.HoldLine();
                                String str;
                                TvMain.ClearDuration();
                                str = TvMain.readmarch();
                                if (str.Length == 0)
                                {

                                    TvMain.WriteErrToDB(id_bdc, SrvDate, "������ ������ ������");
                                    TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "������ ������ �������� ������");
                                }
                                else
                                {
                                    if (str.Substring(0, 6) != "������")
                                    {
                                        if (TvMain.TVD.isMArchToDBWrite)
                                        {
                                            TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "������� �����" + ":OK");
                                            TvMain.WritemArchToDB();

                                        }
                                        //);
                                        tempdate = ddd;
                                        nmin = Convert.ToDouble(dr["icallcurr"].ToString());

                                        while (tempdate.AddMinutes(nmin) <= SrvDate)
                                        {
                                            tempdate = tempdate.AddMinutes(nmin);
                                        }
                                        tempdate = tempdate.AddMinutes(nmin);

                                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextcurr", tempdate);
                                    }
                                    else {
                                        TvMain.WriteErrToDB(id_bdc, SrvDate, str);
                                        TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "������ ������ �������� ������ " +str);

                                        tempdate = ddd;
                                        nmin = Convert.ToDouble(dr["icallcurr"].ToString());

                                        while (tempdate.AddMinutes(nmin) <= SrvDate)
                                        {
                                            tempdate = tempdate.AddMinutes(nmin);
                                        }
                                        //tempdate = tempdate.AddMinutes(nmin);

                                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextcurr", tempdate);
                                    }
                                }
                                //TvMain.UnLockDevice(id_bdc);
                            }
                            }//try
                            catch (Exception Ex)
                            {
                                TvMain.WriteErrToDB(id_bdc, SrvDate, "������ ������ ������:" + Ex.Message);  
                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);
                            }
                          
                        }//if (ccurr)


                        if (csum)

                            ddd = SrvDate;
                        try
                        {
                            ddd = Convert.ToDateTime(dr["dnextsum"].ToString());
                        }
                        catch
                        {
                            InfoReport("������ ID=  " + id_bdc.ToString() + " error converting dnextsum " + dr["dnextsum"].ToString());
                        }


                        if (TvMain.TVD.IsConnected() && csum && ddd <= SrvDate)
                        {
                            DateTime tempdate;
                            Double nmin;
                            InfoReport("������ ID=  " + id_bdc.ToString() + " read total data at " + SrvDate.ToString());
                            try
                            {

                                String str;
                                if (TvMain.LockDevice(id_bdc, 120,true))
                                {
                                    TvMain.HoldLine();
                                    TvMain.ClearDuration();
                                    str = TvMain.readtarch();

                                    InfoReport("������ ID " + id_bdc.ToString() + " -> " + str);
                                    if (str.Length == 0)
                                    {
                                        // oops!
                                        TvMain.SaveLog(id_bdc, archType_total, "??", 1, "������ ������ ��������� ������ " );
                                    }
                                    else
                                    {
                                        if (str.Substring(0, 6) != "������")
                                        {
                                            if (TvMain.TVD.isTArchToDBWrite)
                                            {
                                                TvMain.SaveLog(id_bdc, archType_moment, "??", 1, "�������� ����� " + ":OK");
                                                TvMain.WriteTArchToDB();
                                            }
                                            tempdate = ddd;
                                            nmin = Convert.ToDouble(dr["icallsum"].ToString());

                                            while (tempdate.AddMinutes(nmin) <= SrvDate)
                                            {
                                                tempdate = tempdate.AddMinutes(nmin);
                                            }
                                            tempdate = tempdate.AddMinutes(nmin);

                                            TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dnextsum", tempdate);
                                            //TvMain.AddMinutesToPlanCall(id_bdc.ToString(), "dnextsum", Convert.ToInt32(nmin));
                                        }
                                        else
                                        {
                                            TvMain.SaveLog(id_bdc, archType_total, "??", 1, "������ ������ ��������� ������ " +str);
                                        }
                                    }
                                    //TvMain.UnLockDevice(id_bdc);
                                }
                            }//try
                            catch (Exception Ex)
                            {
                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " read arch failed, " + Ex.Message);

                            }
                        }//if (ccurr)




                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastcall", SrvDate);

                        // try to load missing hour archives
                        {
                            ddd = SrvDate;
                            try
                            {
                                ddd = Convert.ToDateTime(dr["dnexthour"].ToString());
                            }
                            catch
                            {
                            }

                            DateTime tempdate;
                            DataTable missing;
                            missing = TvMain.QuerySelect("select ARCHDATE from missingarch where id_bd=" + id_bdc.ToString() + " and ARCHDATE>SYSDATE-32 and ARCHDATE<SYSDATE-4/24 and ARCHDATE<" + TvMain.OracleDate(ddd) + "  and DEVNAME like '%���%' order by archdate desc  "); // and devname not like '%����%'");

                            try
                            {
                                for (int j = 0; j < missing.Rows.Count && j <15 ; j++)
                                {
                                    tempdate = (DateTime)(missing.Rows[j]["ARCHDATE"]);
                                    if (TvMain.TVD.IsConnected())
                                    {
                                        TvMain.HoldLine();

                                        String str;

                                        InfoReport("������ ID=  " + id_bdc.ToString() + " ������ ������������ �������� ������ �� " + tempdate.ToString());
                                        TvMain.ClearDuration();
                                        str = TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                        if (str.Length == 0)
                                        {
                                            WarningReport("������ ID: " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                            TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ ������������ �������� ������ �� ����:" + tempdate.ToString());
                                        }
                                        else
                                        {
                                            if (str.Substring(0, 6) != "������")
                                            {
                                                //if (TvMain.CheckForArch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc) == false)
                                                {
                                                    if (TvMain.TVD.isArchToDBWrite)
                                                    {
                                                        TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������� ����� �� ����:" + tempdate.ToString() + ":OK");
                                                        TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc);
                                                        TvMain.WriteArchToDB();
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                WarningReport("������ ID: " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                                TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ ������������ �������� ������ �� ����:" + tempdate.ToString());
                                            }
                                        }
                                    }
                                }

                            }//try
                            catch (Exception Ex)
                            {
                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " ������ ������ ������������ �������� ������, " + Ex.Message);

                            }
                        }// (missing hour)


                        // try to load missing day archives
                        {

                            ddd = SrvDate;
                            try
                            {
                                ddd = Convert.ToDateTime(dr["dnext24"].ToString());
                            }
                            catch
                            {

                            }
                            DateTime tempdate;
                            DataTable missing;
                            missing = TvMain.QuerySelect("select ARCHDATE from missingarch where id_bd=" + id_bdc.ToString() + " and ARCHDATE>SYSDATE-32 and ARCHDATE<SYSDATE-1 and  ARCHDATE<" + TvMain.OracleDate(ddd) + " and DEVNAME like '%�����%'  order by archdate desc "); //and devname not like '%����%'");

                            try
                            {
                                for (int j = 0; j < missing.Rows.Count && j <15; j++)
                                {
                                    tempdate = (DateTime)(missing.Rows[j]["ARCHDATE"]);
                                    if (TvMain.TVD.IsConnected())
                                    {
                                        TvMain.HoldLine();

                                        String str;

                                        InfoReport("������ ID=  " + id_bdc.ToString() + " ������ ������������ ��������� ������ ��  " + tempdate.ToString());
                                        TvMain.ClearDuration();
                                        str = TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0);
                                        if (str.Length == 0)
                                        {
                                            WarningReport("������ ID: " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                            TvMain.SaveLog(id_bdc, archType_day, "??", 1, "������ ������ ������������ ��������� ������ �� ����:" + tempdate.ToString());
                                        }
                                        else
                                        {
                                            if (str.Substring(0, 6) != "������")
                                            {
                                                //if (TvMain.CheckForArch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc) == false)
                                                {
                                                    if (TvMain.TVD.isArchToDBWrite)
                                                    {
                                                        TvMain.SaveLog(id_bdc, archType_day, "??", 1, "����������� �������� ����� �� ����:" + tempdate.ToString() + ":OK");
                                                        TvMain.ClearDBArchString(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, id_bdc);
                                                        TvMain.WriteArchToDB();
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                WarningReport("������ ID: " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                                TvMain.SaveLog(id_bdc, archType_day, "??", 1, "������ ������ ������������ ��������� ������ �� ����:" + tempdate.ToString());
                                            }
                                        }
                                    }
                                }

                            }//try
                            catch (Exception Ex)
                            {
                                ErrorReport("������ ID " + dr["id_bd"].ToString() + " ������ ������ ������������ ��������� ������, " + Ex.Message);

                            }
                        }// (missing day)




              #region "qlist hour"

                        TvMain.SetTimeToPlanCall(id_bdc.ToString(), "dlastcall", SrvDate);

                        // try to load missing hour archives
                        {

                            ddd = SrvDate;
                            try
                            {
                                ddd = Convert.ToDateTime(dr["dnexthour"].ToString());
                            }
                            catch
                            {
                            }

                            DateTime tempdate;
                            DataTable missing;
                            missing = TvMain.QuerySelect("select QLISTID, QDATE,PROCESSED from QLIST where id_bd=" + id_bdc.ToString() + " and id_PTYPE=3 ");

                            try
                            {
                                for (int j = 0; j < missing.Rows.Count && j < 6; j++)
                                {

                                   
                                    tempdate = (DateTime)(missing.Rows[j]["QDATE"]);
                                    if (TvMain.TVD.IsConnected())
                                    {
                                        TvMain.HoldLine();

                                        String str;

                                        InfoReport("������ ID=  " + id_bdc.ToString() + " ������ �������� ������ �� ������� " + tempdate.ToString());
                                        TvMain.ClearDuration();
                                        str = TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour);
                                        if (str.Length == 0)
                                        {
                                         
                                            WarningReport("������ ID= " + dr["id_bd"].ToString() + " " + str + " " + tempdate.ToString());
                                            TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������  �������� ������ �� ������� �� ����:" + tempdate.ToString() + " " + str);
                                            if (Convert.ToInt16(missing.Rows[j]["PROCESSED"]) < 9)
                                            {
                                                TvMain.QueryExec("update QLIST set processed=processed+1 where QLISTID=" + missing.Rows[j]["QLISTID"].ToString());
                                            }
                                            else
                                            {
                                                TvMain.QueryExec("delete from QLIST where QLISTID=" + missing.Rows[j]["QLISTID"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            if (str.Substring(0, 6) != "������")
                                            {
                                                //if (TvMain.CheckForArch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc) == false)
                                                {
                                                    if (TvMain.TVD.isArchToDBWrite)
                                                    {
                                                        TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������� ����� �� ������� �� ����:" + tempdate.ToString() + ":OK");
                                                        TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, id_bdc);
                                                        TvMain.WriteArchToDB();
                                                        TvMain.QueryExec("delete from QLIST where QLISTID=" + missing.Rows[j]["QLISTID"].ToString());
                                                       
                                                    }

                                                }
                                            }
                                            else
                                            {
                                              
                                                WarningReport("������ ID= " + dr["id_bd"].ToString() + " " + str + tempdate.ToString());
                                                TvMain.SaveLog(id_bdc, archType_hour, "??", 1, "������ ������ �������� ������ �� ������� �� ����:" + tempdate.ToString() + " " + str);
                                                if (Convert.ToInt16(missing.Rows[j]["PROCESSED"]) < 9)
                                                {
                                                    TvMain.QueryExec("update QLIST set processed=processed+1 where QLISTID=" + missing.Rows[j]["QLISTID"].ToString());
                                                }
                                                else
                                                {
                                                    TvMain.QueryExec("delete from QLIST where QLISTID=" + missing.Rows[j]["QLISTID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }

                            }//try
                            catch (Exception Ex)
                            {
                                
                                ErrorReport("������ ID= " + dr["id_bd"].ToString() + " ������ ������ �������� ������ �� �������, " + Ex.Message);

                            }
                        }// (QLIST hour)

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
                        TvMain.SaveLog(id_bdc, 0, "??", 1, "�������� ������. " + transpname);
                     
        
                    }
                    else
                    {
                        ErrorReport("������ ID " + dr["id_bd"].ToString() + " transport initialization Error! Check IP:" + dr["NPIP"].ToString()  );
                    }
                }
                //else
                //{
                //    InfoReport("������ ID " + dr["id_bd"].ToString() + " plan is active, but no tasks to process!");
                //}
            }
            catch (System.Exception threadEx)
            {
                ErrorReport("������ ID " + dr["id_bd"].ToString() + " thread failed, " + threadEx.Message);
            }
        }



        private void AnalizeDevice(DataRow dr)
        {
            // finalization 
            Int32 id_bdc;
          
            id_bdc = Convert.ToInt32(dr["id_bd"].ToString());

            

            DataTable aCFG = TvMain.QuerySelect("select * from ANALIZER_CFG where ID_BD=" + dr["id_bd"].ToString());
            if (aCFG.Rows.Count > 0)
            {

                if (int.Parse(aCFG.Rows[0]["ANALIZENODE"].ToString()) != 0)
                {
                    try
                    {
                        VIPAnalizer.NodeAnalizer na = new VIPAnalizer.NodeAnalizer();
                        na.AnalizeNode(TvMain, id_bdc, 14,true);
                        TvMain.SaveLog(id_bdc, 0, "??", 1, "������ ������");
                    }
                    catch (System.Exception ex)
                    {
                        TvMain.SaveLog(id_bdc, 0, "??", 1, "������ ��� ������� ������: " + ex.Message );
                    }

                }

            }
        }


        #region log functions
        private Object thisLock = new Object();
        private LogInfo pLogParams = new LogInfo();

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
            }catch{
            }
            return;
        }
        #endregion log functions

      
    }

    
}
