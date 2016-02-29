
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading






Public Structure MArchive
    Public DateArch As DateTime
    Public HC As Int32
    Public MsgHC As String

    Public HCtv1 As Long
    Public MsgHC_1 As String

    Public HCtv2 As Long
    Public MsgHC_2 As String

    Public G1 As Single
    Public G2 As Single
    Public G3 As Single
    Public G4 As Single
    Public G5 As Single
    Public G6 As Single

    Public Q1 As Single
    Public Q2 As Single
    Public Q3 As Single
    Public Q4 As Single
    Public Q5 As Single
    Public Q6 As Single

    Public t1 As Single
    Public t2 As Single
    Public t3 As Single
    Public t4 As Single
    Public t5 As Single
    Public t6 As Single

    Public p1 As Single
    Public p2 As Single
    Public p3 As Single
    Public p4 As Single
    Public p5 As Single
    Public p6 As Single

    Public v1 As Single
    Public v2 As Single
    Public v3 As Single
    Public v4 As Single
    Public v5 As Single
    Public v6 As Single
    Public vh1 As Single
    Public vh2 As Single

    Public m1 As Single
    Public m2 As Single
    Public m3 As Single
    Public m4 As Single
    Public m5 As Single
    Public m6 As Single
   
    Public dt12 As Single
    Public dt45 As Single

    Public tx1 As Single
    Public tx2 As Single

    Public tair1 As Single
    Public tair2 As Single

    Public MyTransport As Long
    Public SPtv1 As Long
    Public SPtv2 As Long

    Public dQ1 As Single
    Public dQ2 As Single


    Public archType As Short
End Structure

Public Structure Archive
    Public DateArch As DateTime
    Public Errtime As Long
    Public HC As Long
    Public MsgHC As String
    Public MsgHC_1 As String
    Public MsgHC_2 As String

    Public HCtv1 As Long
  
    Public HCtv2 As Long


    Public Tw1 As Single
    Public Tw2 As Single

    Public P1 As Single
    Public T1 As Single
    Public M1 As Single
    Public V1 As Single

    Public P2 As Single
    Public T2 As Single
    Public M2 As Single
    Public V2 As Single

    Public P3 As Single
    Public T3 As Single
    Public M3 As Single
    Public V3 As Single

    Public P4 As Single
    Public T4 As Single
    Public M4 As Single
    Public V4 As Single


    Public P5 As Single
    Public T5 As Single
    Public M5 As Single
    Public V5 As Single

    Public P6 As Single
    Public T6 As Single
    Public M6 As Single
    Public V6 As Single



    Public Q1 As Single
    Public Q2 As Single
    Public Q3 As Single
    Public Q4 As Single
    Public Q5 As Single
    Public Q6 As Single

    Public QG1 As Single
    Public QG2 As Single

    Public MyTransport As Long
    Public SPtv1 As Long
    Public SPtv2 As Long

    Public tx1 As Long
    Public tx2 As Long
    Public tair1 As Long
    Public tair2 As Long

    Public V1h As Single
    Public V2h As Single
    Public V3h As Single
    Public V4h As Single
    Public Q1H As Single
    Public Q2H As Single

    Public errtime1 As Int64
    Public errtime2 As Int64
    Public oktime1 As Int64
    Public oktime2 As Int64


    Public archType As Short
End Structure

Public Structure TArchive
    Public DateArch As DateTime
    Public MsgHC As String
    Public MsgHC_1 As String
    Public MsgHC_2 As String

    Public V1 As Single
    Public V2 As Single
    Public V3 As Single
    Public V4 As Single
    Public V5 As Single
    Public V6 As Single

    Public M1 As Single
    Public M2 As Single
    Public M3 As Single
    Public M4 As Single
    Public M5 As Single
    Public M6 As Single
    Public Q1 As Single
    Public Q2 As Single

    Public TW1 As Single
    Public TW2 As Single
    Public Q3 As Single
    Public Q4 As Single
    Public Q5 As Single
    Public Q6 As Single

    Public HC As Int32
    Public errtime1 As Int64
    Public errtime2 As Int64
    Public oktime1 As Int64
    Public oktime2 As Int64

    Public archType As Short

End Structure





Public Class driver
    Inherits STKTVMain.TVDriver

    Public CurHC As String
    Public IsHC As Boolean = False


    Public Const c_lng256 As Long = 256&

    

    Private mIsConnected As Boolean

    Private SleepTime As Long
    Private SequenceErrorCount As Integer = 0

    Dim tArch As TArchive
    Dim IsTArchToRead As Boolean = False

    Dim tv As Short

    Dim archType_hour = 3
    Dim archType_day = 4
    Dim ActiveCount As Integer


    Dim Arch As Archive
    Dim mArch As MArchive

    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False
    Dim bufferindex As Short = 0

    Dim AppVersion As String

    Dim m_isArchToDBWrite As Boolean = False

   

    Public Overrides Property isArchToDBWrite() As Boolean

        Get
            Return m_isArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isArchToDBWrite = value
        End Set
    End Property
    Dim m_isMArchToDBWrite As Boolean = False


    Public Overrides Function CounterName() As String
        Return "DANFOSS200_300"
    End Function

    Private Function DecodeError(e As Byte) As String
       
                Return "Ошибка прибора"


    End Function






   


    Private Function DevInit() As Boolean

        AppVersion = DAN_GetApplication()
        If AppVersion <> "" Then
            Return True
        End If
        Return False
    End Function


    


    Private Version As Byte

    Public Overrides Sub Connect()
        SleepTime = 700
        MyTransport.CleanPort()
        EraseInputQueue()
        mIsConnected = False

        Dim t As Integer
        t = 7
        While Not mIsConnected And t > 0
            Try

                If DevInit() Then
                    mIsConnected = True

                   

                End If


            Catch exc As Exception
                'Return
            End Try
            t = t - 1
        End While


    End Sub

    Private m_readRAMByteCount As Short

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim retsum As String
        'Dim ok As Boolean
        SequenceErrorCount = 0
        Try


            cleararchive(Arch)
            EraseInputQueue()


            Dim dt1 As Date
            Dim dt2 As Date



            If SequenceErrorCount > 5 Then GoTo ArchErr

            If ArchType = archType_hour Then
                isArchToDBWrite = False
                Return "Устройство не содержит архивов"

            End If


            If ArchType = archType_day Then
                    isArchToDBWrite = False
                Return "Устройство не содержит архивов"
            End If

          
       
            Dim AErr As String
            AErr = ErrorMessage

            If AErr = "" Then
                retsum = "Архив прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = True
            Else


                retsum = "Ошибка: не удалось получить часть параметров " & dt2.ToString() + "->"
                retsum = retsum & AErr & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = False
            End If

            Return retsum


archErr:

            retsum = "Ошибка не удалось получить архив "
            EraseInputQueue()
            isArchToDBWrite = False
            Return retsum

        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function

    Public Function DeCodeHCNumber(ByVal CodeHC As Long) As String

        Try
            'If CodeHC >= 32 And CodeHC < 127 Then
            '    DeCodeHCNumber = Chr(CodeHC) + " "
            'Else
            DeCodeHCNumber = "-"
            'End If

        Catch ex As Exception
            DeCodeHCNumber = "-"
        End Try






    End Function
    Public Function DeCodeHCText(ByVal CodeHC As Long) As String
        Try
            'If CodeHC >= 32 And CodeHC < 127 Then
            '    DeCodeHCText = Chr(CodeHC) + " "
            'Else
            DeCodeHCText = "-"
            'End If

        Catch ex As Exception
            DeCodeHCText = "-"
        End Try


    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String
        Try
            'If CodeHC >= 32 And CodeHC < 127 Then
            '    DeCodeHC = Chr(CodeHC) + " "
            'Else
            DeCodeHC = "-"
            'End If

        Catch ex As Exception
            DeCodeHC = "-"
        End Try


    End Function

   

    Public Overrides Function WriteArchToDB() As String

        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,dm12,V1H,V2H,V5H,V4H,q1h,q2h,sp_TB1,sp_TB2,q1,q2,q3,q4,q5,q6,TSUM1,TSUM2,hc,hc_code,hc_1,hc_2, oktime,oktime2,errtime,errtime2) values ("
        WriteArchToDB = WriteArchToDB + DeviceID.ToString() + ","
        WriteArchToDB = WriteArchToDB + Arch.archType.ToString() + ","
        WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tx1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tx2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tair1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tair2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M6, "##############0.000").Replace(",", ".") + ","
        If Not Single.IsNaN(Arch.M1) And Not Single.IsNaN(Arch.M2) Then
            WriteArchToDB = WriteArchToDB + NanFormat(Arch.M1 - Arch.M2, "##############0.000").Replace(",", ".") + ","
        Else
            WriteArchToDB = WriteArchToDB + "NULL,"
        End If

        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V3h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V4h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1H, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2H, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv1.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv2.ToString + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Tw1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Tw2, "##############0.000").Replace(",", ".") + ","

        WriteArchToDB = WriteArchToDB + "'" + S180(Arch.MsgHC) + "','" + S180(Arch.HC.ToString()) + "',"
        WriteArchToDB = WriteArchToDB + "'" + S180(Arch.MsgHC_1) + "','" + S180(Arch.MsgHC_2) + "',"
        WriteArchToDB = WriteArchToDB + Format((Arch.oktime1), "##############0").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format((Arch.oktime2), "##############0").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.errtime1, "##############0").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.errtime2, "##############0").Replace(",", ".")

        WriteArchToDB = WriteArchToDB + ")"
        Debug.Print(WriteArchToDB)
    End Function

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS')"
    End Function
    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = ""
        Try
            WriteMArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,p5,p6,g1,g2,g3,g4,g5,g6,m1,m2,m3,m4,m5,m6,v1,v2,v3,v4,v5,v6,q1,q2,q3,q4,q5,q6,tair1,tair2) values ("
            WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
            WriteMArchToDB = WriteMArchToDB + "1,"
            WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair2, "##############0.000").Replace(",", ".")
            WriteMArchToDB = WriteMArchToDB + ")"
        Catch
        End Try
        Debug.Print(WriteMArchToDB)
    End Function




     Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(250)
        MyTransport.CleanPort()
    End Sub

    Private Sub cleararchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue

        arc.HC = 0
        arc.MsgHC = ""
        arc.MsgHC_1 = ""
        arc.MsgHC_2 = ""

        arc.HCtv1 = 0


        arc.HCtv2 = 0


        arc.Tw1 = 0
        arc.Tw2 = 0

        arc.P1 = 0
        arc.T1 = 0
        arc.M2 = 0
        arc.V1 = 0

        arc.P2 = 0
        arc.T2 = 0
        arc.M3 = 0
        arc.V2 = 0

        arc.V3 = 0
        arc.M1 = 0

        arc.Q1 = 0
        arc.Q2 = 0
        arc.Q3 = 0
        arc.Q4 = 0
        arc.Q5 = 0
        arc.Q6 = 0


        arc.QG1 = 0
        arc.QG2 = 0

        arc.MyTransport = 0
        arc.SPtv1 = 0
        arc.SPtv2 = 0

        arc.tx1 = 0
        arc.tx2 = 0
        arc.tair1 = 0
        arc.tair2 = 0

        arc.T3 = 0
        arc.T4 = 0
        arc.T5 = 0
        arc.T6 = 0

        arc.P3 = 0
        arc.P4 = 0
        arc.P5 = 0
        arc.P6 = 0

        arc.v4 = 0
        arc.v5 = 0
        arc.v6 = 0
        arc.M4 = 0
        arc.M5 = 0
        arc.M6 = 0

        arc.archType = 0
        isArchToDBWrite = False
    End Sub
    Private Sub clearMarchive(ByRef marc As MArchive)
        marc.DateArch = DateTime.MinValue
        marc.HC = 0
        marc.MsgHC = ""

        marc.HCtv1 = 0
        marc.MsgHC_1 = ""

        marc.HCtv2 = 0
        marc.MsgHC_2 = ""

        marc.G1 = Single.NaN
        marc.G2 = Single.NaN
        marc.G3 = Single.NaN
        marc.G4 = Single.NaN
        marc.G5 = Single.NaN
        marc.G6 = Single.NaN

        marc.t1 = Single.NaN
        marc.t2 = Single.NaN
        marc.t3 = Single.NaN
        marc.t4 = Single.NaN
        marc.t5 = Single.NaN
        marc.t6 = Single.NaN

        marc.p1 = Single.NaN
        marc.p2 = Single.NaN
        marc.p3 = Single.NaN
        marc.p4 = Single.NaN

        marc.dt12 = Single.NaN
        marc.dt45 = Single.NaN

        marc.tx1 = Single.NaN
        marc.tx2 = Single.NaN

        marc.tair1 = Single.NaN
        marc.tair2 = Single.NaN

        marc.MyTransport = 0
        marc.SPtv1 = 0
        marc.SPtv2 = 0


        marc.archType = 1
        isMArchToDBWrite = False
    End Sub



    Private Function FindValue(dt As DataTable, name As String) As Double
        Dim i As Integer
        Dim v As Double = 0.0
        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("Название") = name Then
                v = dt.Rows(i)("Значение")
                Return v
            End If
        Next
        Return Double.NaN

    End Function


    Public Overrides Function ReadMArch() As String
        clearMarchive(mArch)


        Dim AErr As String = ""

        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow

        'dr = dt.NewRow
        'dr("Название") = "Номер версии ПО"
        'dr("Значение") = AppVersion
        'dt.Rows.Add(dr)
        'DAN_GetMode(dt)
        DAN_CommonParameters(dt)
        'DAN_WeekPlan(dt)

        DAN_R8(dt)

        If AppVersion.ToUpper() = "C60" Then
            DAN_C60(dt)
        End If

        If AppVersion.ToUpper() = "C66" Then
            DAN_C66(dt)
        End If

        If AppVersion.ToUpper() = "C62" Then
            DAN_C62(dt)
        End If


        mArch.DateArch = GetDeviceDate()
        mArch.t1 = FindValue(dt, "Room temperature circuit 1")
        mArch.t2 = FindValue(dt, "Room temperature circuit 2")
        mArch.t3 = FindValue(dt, "Calculated flow temperature Circuit 1")
        mArch.t4 = FindValue(dt, "Calculated flow temperature Circuit 2")
        mArch.t5 = FindValue(dt, "Calculated return temperature Circuit 1")
        mArch.t6 = FindValue(dt, "Calculated return temperature Circuit 2")
        mArch.tair1 = FindValue(dt, "Outdoor temperature")

        mArch.G1 = FindValue(dt, "Sensor 1")
        mArch.G2 = FindValue(dt, "Sensor 2")
        mArch.G3 = FindValue(dt, "Sensor 3")
        mArch.G4 = FindValue(dt, "Sensor 4")
        mArch.G5 = FindValue(dt, "Sensor 5")
        mArch.G6 = FindValue(dt, "Sensor 6")


        mArch.v1 = FindValue(dt, "Heating curve circuit 1 (*10)")
        mArch.v2 = FindValue(dt, "Heating curve circuit 2 (*10)")
        mArch.v3 = FindValue(dt, "Parallel displacement circuit 1")
        mArch.v4 = FindValue(dt, "Parallel displacement circuit 2")
        mArch.v5 = FindValue(dt, "Flow temperature-minimum circuit 1")
        mArch.v6 = FindValue(dt, "Flow temperature-minimum circuit 2")

        mArch.Q1 = FindValue(dt, "Flow temperature-maximum circuit 1")
        mArch.Q2 = FindValue(dt, "Flow temperature-maximum circuit 2")
        mArch.Q3 = FindValue(dt, "Hotwater temperature day setpoint")
        mArch.Q4 = FindValue(dt, "Hotwater temperature night setpoint")

        mArch.p1 = FindValue(dt, "Summer cut-out circuit 1")
        mArch.p2 = FindValue(dt, "Summer cut-out circuit 2")
        mArch.p3 = FindValue(dt, "Room temperature day setpoint circuit 1")
        mArch.p4 = FindValue(dt, "Room temperature night setpoint circuit 1")
        mArch.p5 = FindValue(dt, "Room temperature day setpoint circuit 2")
        mArch.p6 = FindValue(dt, "Room temperature night setpoint circuit 2")



        If mArch.t1 = 192 Then mArch.t1 = Single.NaN
        If mArch.t2 = 192 Then mArch.t2 = Single.NaN
        If mArch.t3 = 192 Then mArch.t3 = Single.NaN
        If mArch.t4 = 192 Then mArch.t4 = Single.NaN
        If mArch.t5 = 192 Then mArch.t5 = Single.NaN
        If mArch.t6 = 192 Then mArch.t6 = Single.NaN

        If mArch.G1 = 192 Then mArch.G1 = Single.NaN
        If mArch.G2 = 192 Then mArch.G2 = Single.NaN
        If mArch.G3 = 192 Then mArch.G3 = Single.NaN
        If mArch.G4 = 192 Then mArch.G4 = Single.NaN
        If mArch.G5 = 192 Then mArch.G5 = Single.NaN
        If mArch.G6 = 192 Then mArch.G6 = Single.NaN


        If mArch.v1 = 192 Then mArch.v1 = Single.NaN
        If mArch.v2 = 192 Then mArch.v2 = Single.NaN
        If mArch.v3 = 192 Then mArch.v3 = Single.NaN
        If mArch.v4 = 192 Then mArch.v4 = Single.NaN
        If mArch.v5 = 192 Then mArch.v5 = Single.NaN
        If mArch.v6 = 192 Then mArch.v6 = Single.NaN

        If mArch.Q1 = 192 Then mArch.Q1 = Single.NaN
        If mArch.Q2 = 192 Then mArch.Q2 = Single.NaN
        If mArch.Q3 = 192 Then mArch.Q3 = Single.NaN
        If mArch.Q4 = 192 Then mArch.Q4 = Single.NaN


        If mArch.p1 = 192 Then mArch.p1 = Single.NaN
        If mArch.p2 = 192 Then mArch.p2 = Single.NaN
        If mArch.p3 = 192 Then mArch.p3 = Single.NaN
        If mArch.p4 = 192 Then mArch.p4 = Single.NaN
        If mArch.p5 = 192 Then mArch.p5 = Single.NaN
        If mArch.p6 = 192 Then mArch.p6 = Single.NaN


        Try
            mArch.v1 = mArch.v1 / 10
        Catch ex As Exception

        End Try

        Try
            mArch.v2 = mArch.v2 / 10
        Catch ex As Exception

        End Try


        Dim retsum As String
        retsum = "Мгновенный архив прочитан"
        If AErr = "" Then
            'retsum = "Мгновенный архив прочитан"
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isMArchToDBWrite = True
            Return retsum
        Else
            retsum = "Не удалось получить часть параметров "
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isMArchToDBWrite = False
            Return retsum
        End If

mArchErr:

        EraseInputQueue()
        isMArchToDBWrite = False
        Return "Ошибка чтения мгновенного архива"

    End Function
    Dim m_isTArchToDBWrite As Boolean = False
    Public Overrides Property isTArchToDBWrite() As Boolean
        Get
            Return m_isTArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isTArchToDBWrite = value
        End Set
    End Property



    Private Sub clearTarchive(ByRef marc As TArchive)
        marc.DateArch = DateTime.MinValue


        marc.V1 = Single.NaN
        marc.V2 = Single.NaN
        marc.V3 = Single.NaN
        marc.V4 = Single.NaN
        marc.V5 = Single.NaN
        marc.V6 = Single.NaN
        marc.M1 = Single.NaN
        marc.M2 = Single.NaN
        marc.M3 = Single.NaN
        marc.M4 = Single.NaN
        marc.M5 = Single.NaN
        marc.M6 = Single.NaN
        marc.Q1 = Single.NaN
        marc.Q2 = Single.NaN
        marc.TW1 = Single.NaN
        marc.TW2 = Single.NaN

        marc.archType = 2
        isTArchToDBWrite = False
    End Sub

    Public Overrides Function ReadTArch() As String

        clearTarchive(tArch)



        '00 03 00 1C 00 00 85 DD 
        Dim Frame(10) As Byte
        Dim b(4096) As Byte
        Dim AErr As String = ""

       


        tArch.DateArch = GetDeviceDate()

        If SequenceErrorCount > 5 Then GoTo ArchErr




        Dim retsum As String
        retsum = "Итоговый архив прочитан"

        AErr = "Устройство не содержит архивов"
        If AErr = "" Then

            retsum = retsum & vbCrLf
            EraseInputQueue()
            isTArchToDBWrite = True
            Return retsum
        Else
            retsum = "Не удалось получить часть параметров "
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isTArchToDBWrite = True
            Return retsum
        End If
archerr:
        retsum = "Ошибка чтения итогового архива"
        EraseInputQueue()
        isTArchToDBWrite = False
        Return retsum

    End Function

    Public Overrides Function WriteTArchToDB() As String
        WriteTArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1,Q2,Q3,Q4,Q5,Q6,M1,M2,M3,M4,M5,M6,v1h,v2h,v3,v4h,v5h,v6,TSUM1,TSUM2,oktime,oktime2,ERRTIME,errtime2) values ("
        WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q6, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M6, "##############0.000").Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V6, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.TW1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.TW2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime1, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime2, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime1, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime2, "##############0").Replace(",", ".")
        WriteTArchToDB = WriteTArchToDB + ")"
        Debug.Print(WriteTArchToDB)
    End Function

    Private Function ExtLong4(ByVal extStr As String) As Single
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function

    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function
    Private Function S180(ByVal s As String) As String

        Dim outs As String
        outs = s
        If outs.Length <= 180 Then
            Return outs
        End If
        outs = outs.Substring(0, 180)
        Return outs
    End Function




    Private mIsError As Boolean

    Public Property IsError() As Boolean
        Get
            Return mIsError
        End Get
        Private Set(ByVal value As Boolean)
            mIsError = value
        End Set
    End Property

    Private mErrorMessage As String

    Public Property ErrorMessage() As String
        Get
            Return mErrorMessage
        End Get
        Private Set(ByVal value As String)
            mErrorMessage = value
        End Set
    End Property

    Private Function TableForArch(ByVal ArchType As Short) As String
        Dim tName As String = ""
        If ArchType = 1 Then
            tName = "TPLC_M"
        End If

        If ArchType = 3 Then
            tName = "TPLC_H"
        End If
        If ArchType = 4 Then
            tName = "TPLC_D"
        End If
        If ArchType = 2 Then
            tName = "TPLC_T"
        End If
        Return tName
    End Function

    Public Overrides Function ReadSystemParameters() As DataTable
        SequenceErrorCount = 0
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow

        dr = dt.NewRow
        dr("Название") = "Номер версии ПО"
        dr("Значение") = AppVersion
        dt.Rows.Add(dr)


        DAN_GetMode(dt)
        DAN_CommonParameters(dt)
        DAN_WeekPlan(dt)

        DAN_R8(dt)

        If AppVersion.ToUpper() = "C60" Then
            DAN_C60(dt)
        End If

        If AppVersion.ToUpper() = "C62" Then
            DAN_C62(dt)
        End If

        If AppVersion.ToUpper() = "C66" Then
            DAN_C66(dt)
        End If


        
        Return dt
    End Function

    Private Function BCD(ByVal B As Byte) As UInteger
        Dim i As UInteger
        Dim o As UInteger
        i = B
        If (i Mod 16) > 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 100)
        ElseIf (i Mod 16) <= 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 10)
        End If

        Return o And &HFF
    End Function

    Public Overrides Property isMArchToDBWrite() As Boolean
        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property




   


    Private ActiveElements(100) As Integer
    Private ElemSize(100) As Byte
    Private PropVal(100) As Byte

 
    'Public Function VerifySumm(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As Boolean
    '    Dim ch As Long
    '    If sz <= 2 Then Return False
    '    ch = CheckSum(Data, offset, sz - 2)
    '    If Data(offset + sz - 2) = ch Mod 256 And Data(offset + sz - 1) = ch \ 256 Then
    '        Return True
    '    End If
    '    Return False
    'End Function



    'Private Function CheckSum(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As UInt16
    '    Dim s As UInt16
    '    Dim sl As Long
    '    sl = &HFFFF
    '    On Error Resume Next
    '    For i As Integer = 0 To sz - 1

    '        sl = ((sl \ 256) * 256 + ((sl Mod 256) Xor Data(offset + i))) And &HFFFF
    '        For sh As Integer = 0 To 7
    '            If (sl And 1) = 1 Then
    '                sl = ((sl \ 2) Xor &HA001) And &HFFFF
    '            Else
    '                sl = (sl \ 2) And &HFFFF
    '            End If


    '        Next
    '    Next
    '    s = sl And &HFFFF
    '    Return s
    'End Function

    Public Function Bytes2Float(ByVal fbytes() As Byte, ByVal index As Int16) As Single
        If UBound(fbytes) - index < 3 Then
            Return 0
        End If
        Return System.BitConverter.ToSingle(fbytes, index)
    End Function

    Public Function Bytes2Int(ByVal fbytes() As Byte, ByVal index As Int16) As Int16
        If UBound(fbytes) - index < 2 Then
            Return 0
        End If
        Dim iInputIndex As Integer, iOutputIndex As Integer = 0
        Dim bArray(0 To 1) As Byte
        For iInputIndex = 0 To 1

            bArray(iOutputIndex) = fbytes(index + iInputIndex)

            iOutputIndex += 1

        Next
        Array.Reverse(bArray)
        Return System.BitConverter.ToInt16(bArray, 0)
    End Function


    Private Function BToSingle(ByVal data() As Byte, ByVal index As Int16) As Single

        Try

            Dim iInputIndex As Integer = 0

            Dim iOutputIndex As Integer = 0

            Dim bArray(3) As Byte



            For iInputIndex = 0 To 3

                bArray(iOutputIndex) = data(index + iInputIndex)

                iOutputIndex += 1

            Next
            Array.Reverse(bArray)

            Return BitConverter.ToSingle(bArray, 0)

        Catch ex As Exception

        End Try
    End Function



    
    Private Function GetDeviceDate() As Date
        EraseInputQueue()

   
        Return Date.Now

    End Function


   

    '''''''''''''''''''''''''''''''' DANFOSS SPECIFIC ''''''''''''''''''''''''''
    Private sOut As String = ""
    Private Function CRC(b() As Byte, ByVal cnt As Integer) As Byte
        Dim c As Byte
        Dim i As Integer
        If b.GetUpperBound(0) > cnt Then
            c = b(0)
            For i = 1 To cnt - 1
                c = c Xor b(i)
            Next

            Return c
        Else
            Return 0
        End If

    End Function

    Private Function DAN_GetApplication() As String
        Dim cmdOut(5) As Byte
        Dim buffer(100) As Byte

        Dim cnt As Integer
        'Dim i As Integer
        Dim appstr As String = ""

        cmdOut(0) = &H80
        cmdOut(1) = 9
        cmdOut(2) = 0
        cmdOut(3) = 0
        cmdOut(4) = CRC(cmdOut, 4)
        MyTransport.Write(cmdOut, 0, 5)
        System.Threading.Thread.Sleep(1300)
        cnt = MyTransport.Read(buffer, 0, 5)
        System.Threading.Thread.Sleep(400)
        If cnt = 0 Then
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(500)
            cnt = MyTransport.Read(buffer, 0, 5)
        End If

        If cnt > 0 Then
            If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                If buffer(2) < 12 Then
                    appstr = Mid("AbCdEFGHLnoPU", CInt(buffer(2) + 1), 1)
                End If
                appstr = appstr + buffer(3).ToString

                sOut = sOut & vbCrLf & "App=" & appstr ' & " :"
            End If
            Return appstr
        Else
            Return ""
        End If

    End Function

    Private Function DAN_GetMode(dt As DataTable) As String

        Dim cmdOut(5) As Byte
        Dim buffer(100) As Byte
        Dim dr As DataRow

        

        Dim cnt As Integer
        Dim i As Integer
        Dim modestr As String = ""
        For i = 1 To 3
            cmdOut(0) = &H11
            cmdOut(1) = i
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(1300)
            cnt = 0
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)

            If cnt = 5 Then
                If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                    dr = dt.NewRow
                    dr("Название") = "Circuit # " & i.ToString & " mode"
                    Select Case buffer(3)
                        Case 0
                            dr("Значение") = "Manuel"
                        Case 1
                            dr("Значение") = "Clock mode"
                        Case 2
                            dr("Значение") = "Comfort mode"
                        Case 3
                            dr("Значение") = "Reduced mode"
                        Case 4
                            dr("Значение") = "Standby mode"
                        Case Else
                            dr("Значение") = "Unknown mode"
                    End Select
                    dt.Rows.Add(dr)
                End If
            End If
        Next

        Return modestr

    End Function

    Private Sub DAN_R8(dt As DataTable)
        Dim cmdOut(5) As Byte
        Dim buffer(10) As Byte
        Dim addr As Dictionary(Of String, Byte())
        Dim dr As DataRow


        addr = New Dictionary(Of String, Byte())
        addr.Add("Heating curve circuit 1 (*10)", {&HE, &HC9, &H5D, 0}) ' low 2 (= 0,2) 34 (=3,4)
        addr.Add("Summer cut-out circuit 1", {&HE, &HC2, &H5A, 1}) ' high -9 9
        addr.Add("Summer cut-out circuit 2", {&HE, &HC3, &H5A, 0}) ' low -9 9
        addr.Add("Room temperature day setpoint circuit 1", {&HE, &H9E, &H48, 1}) ' low 10 110
        addr.Add("Room temperature night setpoint circuit 1", {&HE, &HA0, &H49, 1}) ' low 10 110
        addr.Add("Room temperature day setpoint circuit 2", {&HE, &H9F, &H48, 0}) 'high 10 110
        addr.Add("Room temperature night setpoint circuit 2", {&HE, &HA1, &H49, 0}) ' high 10 110



        Dim v As Byte()
        Dim cnt As Integer
        For Each n As String In addr.Keys
            v = addr.Item(n)
            cmdOut(0) = &HC0 + v(0)
            cmdOut(1) = v(1)
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(500)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)
            If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                If buffer(0) = 2 And buffer(1) = cmdOut(0) Then
                    dr = dt.NewRow
                    dr("Название") = n
                    dr("Значение") = buffer(2).ToString() ' & " ?  " & vx.ToString
                    dt.Rows.Add(dr)
                End If
            End If


        Next
    End Sub

    Private Sub DAN_C60(ByVal dt As DataTable)
        Dim cmdOut(5) As Byte
        Dim buffer(10) As Byte
        Dim addr As Dictionary(Of String, Byte())
        Dim dr As DataRow


        addr = New Dictionary(Of String, Byte())
        addr.Add("Heating curve circuit 2 (*10)", {&HE, &HD9, &H65, 0}) ' low 2 (= 0,2) 34 (=3,4)
        addr.Add("Parallel displacement circuit 1", {&HE, &HA8, &H4D, 1}) ' high -9 9
        addr.Add("Parallel displacement circuit 2", {&HE, &HA9, &H4D, 0}) ' low -9 9
        addr.Add("Flow temperature-minimum circuit 1", {&HE, &HCB, &H5E, 0}) ' low 10 110
        addr.Add("Flow temperature-minimum circuit 2", {&HE, &HDB, &H66, 0}) ' low 10 110
        addr.Add("Flow temperature-maximum circuit 1", {&HE, &HCC, &H5F, 1}) 'high 10 110
        addr.Add("Flow temperature-maximum circuit 2", {&HE, &HDC, &H66, 1}) ' high 10 110



        Dim v As Byte()
        Dim cnt As Integer
        For Each n As String In addr.Keys
            v = addr.Item(n)
            cmdOut(0) = &HC0 + v(0)
            cmdOut(1) = v(1)
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(500)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)
            'Dim vx As Short
            'vx = BitConverter.ToInt16(buffer, 2)
            'If v(3) Then
            '    vx = (vx And &HFF00) >> 8
            'Else
            '    vx = vx And &HFF
            'End If

            If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                If buffer(0) = 2 And buffer(1) = cmdOut(0) Then
                    dr = dt.NewRow
                    dr("Название") = n
                    dr("Значение") = buffer(2).ToString() ' & " ?  " & vx.ToString
                    dt.Rows.Add(dr)
                End If

            End If

        Next
    End Sub

    Private Sub DAN_C66(ByVal dt As DataTable)
        Dim cmdOut(5) As Byte
        Dim buffer(10) As Byte
        Dim addr As Dictionary(Of String, Byte())
        Dim dr As DataRow


        addr = New Dictionary(Of String, Byte())
        'addr.Add("Heating curve circuit 2 (*10)", {&HE, &HD9, &H65, 0}) ' low 2 (= 0,2) 34 (=3,4)
        addr.Add("Parallel displacement circuit 1", {&HE, &HA8, &H4D, 1}) ' high -9 9
        'addr.Add("Parallel displacement circuit 2", {&HE, &HA9, &H4D, 0}) ' low -9 9
        addr.Add("Flow temperature-minimum circuit 1", {&HE, &HCB, &H5E, 0}) ' low 10 110
        'addr.Add("Flow temperature-minimum circuit 2", {&HE, &HDB, &H66, 0}) ' low 10 110
        addr.Add("Flow temperature-maximum circuit 1", {&HE, &HCC, &H5F, 1}) 'high 10 110
        'addr.Add("Flow temperature-maximum circuit 2", {&HE, &HDC, &H66, 1}) ' high 10 110
        addr.Add("Hotwater temperature day setpoint", {&HE, &HD0, &H61, 1})
        addr.Add("Hotwater temperature night setpoint", {&HE, &HD1, &H61, 0})

        Dim v As Byte()
        Dim cnt As Integer
        For Each n As String In addr.Keys
            v = addr.Item(n)
            cmdOut(0) = &HC0 + v(0)
            cmdOut(1) = v(1)
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(500)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)
            'Dim vx As Short
            'vx = BitConverter.ToInt16(buffer, 2)
            'If v(3) Then
            '    vx = (vx And &HFF00) >> 8
            'Else
            '    vx = vx And &HFF
            'End If

            If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                If buffer(0) = 2 And buffer(1) = cmdOut(0) Then
                    dr = dt.NewRow
                    dr("Название") = n
                    dr("Значение") = buffer(2).ToString() ' & " ?  " & vx.ToString
                    dt.Rows.Add(dr)
                End If
            End If

        Next
    End Sub



    Private Sub DAN_C62(ByVal dt As DataTable)
        Dim cmdOut(5) As Byte
        Dim buffer(10) As Byte
        Dim addr As Dictionary(Of String, Byte())
        Dim dr As DataRow


        addr = New Dictionary(Of String, Byte())
        addr.Add("Heating curve circuit 2 (*10)", {&HE, &HD9, &H65, 0}) ' low 2 (= 0,2) 34 (=3,4)
        addr.Add("Parallel displacement circuit 1", {&HE, &HA8, &H4D, 1}) ' high -9 9
        addr.Add("Parallel displacement circuit 2", {&HE, &HA9, &H4D, 0}) ' low -9 9
        addr.Add("Flow temperature-minimum circuit 1", {&HE, &HCB, &H5E, 0}) ' low 10 110
        addr.Add("Flow temperature-minimum circuit 2", {&HE, &HDB, &H66, 0}) ' low 10 110
        addr.Add("Flow temperature-maximum circuit 1", {&HE, &HCC, &H5F, 1}) 'high 10 110
        addr.Add("Flow temperature-maximum circuit 2", {&HE, &HDC, &H66, 1}) ' high 10 110
        'addr.Add("Hotwater temperature day setpoint", {&HE, &HD0, &H61, 1})
        'addr.Add("Hotwater temperature night setpoint", {&HE, &HD1, &H61, 0})

        Dim v As Byte()
        Dim cnt As Integer
        For Each n As String In addr.Keys
            v = addr.Item(n)
            cmdOut(0) = &HC0 + v(0)
            cmdOut(1) = v(1)
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(500)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)
            'Dim vx As Short
            'vx = BitConverter.ToInt16(buffer, 2)
            'If v(3) Then
            '    vx = (vx And &HFF00) >> 8
            'Else
            '    vx = vx And &HFF
            'End If
            If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                If buffer(0) = 2 And buffer(1) = cmdOut(0) Then
                    dr = dt.NewRow
                    dr("Название") = n
                    dr("Значение") = buffer(2).ToString() ' & " ?  " & vx.ToString
                    dt.Rows.Add(dr)
                End If
            End If

        Next
    End Sub

    Private Sub DAN_WeekPlan(ByVal dt As DataTable)
        Dim cmdOut(5) As Byte
        Dim buffer(100) As Byte
        Dim addr As Dictionary(Of String, Short)
        Dim dr As DataRow



        addr = New Dictionary(Of String, Short)
        addr.Add("Monday Weekplan I", &H72)
        addr.Add("Monday Weekplan II", &H75)
        addr.Add("Tuesday Weekplan I", &H78)
        addr.Add("Tuesday Weekplan II", &H7B)
        addr.Add("Wednesday Weekplan I", &H7E)
        addr.Add("Wednesday Weekplan II", &H81)


        addr.Add("Thursday Weekplan I", &H84)
        addr.Add("Thursday Weekplan II", &H87)

        addr.Add("Friday Weekplan I", &H8A)
        addr.Add("Friday Weekplan II", &H8D)

        addr.Add("Saturday Weekplan I", &H90)
        addr.Add("Saturday Weekplan II", &H93)

        addr.Add("Sunday Weekplan I", &H96)
        addr.Add("Sunday Weekplan II", &H99)

        Dim v As Short
        Dim h(48) As Byte
        Dim cnt As Integer
        Dim i As Integer
        For Each n As String In addr.Keys

            For i = 0 To 47
                h(i) = 0
            Next
            v = addr.Item(n)
            cmdOut(0) = &H80
            cmdOut(1) = v And &HFF
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(600)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)

            If cnt > 0 Then

                If buffer(3) & (1) Then h(0) = 1
                If buffer(3) & (2) Then h(1) = 1
                If buffer(3) & (4) Then h(2) = 1
                If buffer(3) & (8) Then h(3) = 1
                If buffer(3) & (16) Then h(4) = 1
                If buffer(3) & (32) Then h(5) = 1
                If buffer(3) & (64) Then h(6) = 1
                If buffer(3) & (128) Then h(7) = 1

                If buffer(4) & (1) Then h(8) = 1
                If buffer(4) & (2) Then h(9) = 1
                If buffer(4) & (4) Then h(10) = 1
                If buffer(4) & (8) Then h(11) = 1
                If buffer(4) & (16) Then h(12) = 1
                If buffer(4) & (32) Then h(13) = 1
                If buffer(4) & (64) Then h(14) = 1
                If buffer(4) & (128) Then h(15) = 1

            End If




            cmdOut(0) = &H80
            cmdOut(1) = (v + 1) And &HFF
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(600)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)
            If cnt > 0 Then
                If buffer(3) & (1) Then h(16) = 1
                If buffer(3) & (2) Then h(17) = 1
                If buffer(3) & (4) Then h(18) = 1
                If buffer(3) & (8) Then h(19) = 1
                If buffer(3) & (16) Then h(20) = 1
                If buffer(3) & (32) Then h(21) = 1
                If buffer(3) & (64) Then h(22) = 1
                If buffer(3) & (128) Then h(23) = 1

                If buffer(4) & (1) Then h(24) = 1
                If buffer(4) & (2) Then h(25) = 1
                If buffer(4) & (4) Then h(26) = 1
                If buffer(4) & (8) Then h(27) = 1
                If buffer(4) & (16) Then h(28) = 1
                If buffer(4) & (32) Then h(29) = 1
                If buffer(4) & (64) Then h(30) = 1
                If buffer(4) & (128) Then h(31) = 1
            End If


            cmdOut(0) = &H80
            cmdOut(1) = (v + 2) And &HFF
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(600)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)
            If cnt > 0 Then
                If buffer(3) & (1) Then h(32) = 1
                If buffer(3) & (2) Then h(33) = 1
                If buffer(3) & (4) Then h(34) = 1
                If buffer(3) & (8) Then h(35) = 1
                If buffer(3) & (16) Then h(36) = 1
                If buffer(3) & (32) Then h(37) = 1
                If buffer(3) & (64) Then h(38) = 1
                If buffer(3) & (128) Then h(39) = 1

                If buffer(4) & (1) Then h(40) = 1
                If buffer(4) & (2) Then h(41) = 1
                If buffer(4) & (4) Then h(42) = 1
                If buffer(4) & (8) Then h(43) = 1
                If buffer(4) & (16) Then h(44) = 1
                If buffer(4) & (32) Then h(45) = 1
                If buffer(4) & (64) Then h(46) = 1
                If buffer(4) & (128) Then h(47) = 1
            End If


            Dim shour As String
            For i = 0 To 47
                dr = dt.NewRow
                shour = (i \ 2).ToString & ":"
                If i Mod 2 = 1 Then
                    shour = shour & "30"
                Else
                    shour = shour & "00"
                End If

                dr("Название") = n & " " & shour



                If h(i).ToString Then
                    dr("Значение") = "Comfort"
                Else
                    dr("Значение") = "Reduced"
                End If
                dt.Rows.Add(dr)
            Next
        Next


    End Sub


    Private Sub DAN_CommonParameters(ByVal dt As DataTable)
        Dim cmdOut(5) As Byte
        Dim buffer(100) As Byte

        Dim dr As DataRow



        Dim addr As Dictionary(Of String, Short)
        addr = New Dictionary(Of String, Short)
        addr.Add("Sensor 6", &HE30)
        addr.Add("Sensor 5", &HE32)
        addr.Add("Sensor 4", &HE34)
        addr.Add("Sensor 3", &HE36)
        addr.Add("Sensor 2", &HE38)
        addr.Add("Sensor 1", &HE3A)
        addr.Add("Outdoor temperature", &HE3C)
        addr.Add("Room temperature circuit 1", &HE3E)
        addr.Add("Room temperature circuit 2", &HE40)
        addr.Add("Calculated flow temperature Circuit 1", &HE46)
        addr.Add("Calculated flow temperature Circuit 2", &HE48)
        addr.Add("Calculated return temperature Circuit 1", &HE4A)
        addr.Add("Calculated return temperature Circuit 2", &HE4C)
        addr.Add("Room temperature influence-max circuit 1", &HE76)
        addr.Add("Room temperature influence-min circuit 1", &HE78)
        addr.Add("Room temperature influence-max circuit 2", &HE72)
        addr.Add("Room temperature influence-min circuit 2", &HE74)



        Dim v As Short
        Dim cnt As Integer
        Dim i As Integer


        For Each n As String In addr.Keys
            v = addr.Item(n)
            cmdOut(0) = &HC0 + ((v And &HF00) >> 8)
            cmdOut(1) = v And &HFF
            cmdOut(2) = 0
            cmdOut(3) = 0
            cmdOut(4) = CRC(cmdOut, 4)
            MyTransport.Write(cmdOut, 0, 5)
            System.Threading.Thread.Sleep(500)
            cnt = MyTransport.Read(buffer, 0, 5)
            System.Threading.Thread.Sleep(400)

            If cnt = 5 And CRC(buffer, 4) = buffer(4) Then
                If buffer(0) = 2 And buffer(1) = cmdOut(0) Then
                    dr = dt.NewRow
                    dr("Название") = n
                    Dim vx As Short

                    'vx = BitConverter.ToInt16(buffer, 2)
                    vx = Bytes2Int(buffer, 2)

                    'If v >= &HE46 Then
                    vx = vx / 128
                    'End If

                    dr("Значение") = vx.ToString
                    dt.Rows.Add(dr)
                End If
            End If
        Next
    End Sub


End Class
