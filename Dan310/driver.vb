
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading






'Public Structure MArchive
'    Public DateArch As DateTime
'    Public HC As Int32
'    Public MsgHC As String

'    Public HCtv1 As Long
'    Public MsgHC_1 As String

'    Public HCtv2 As Long
'    Public MsgHC_2 As String

'    Public G1 As Single
'    Public G2 As Single
'    Public G3 As Single
'    Public G4 As Single
'    Public G5 As Single
'    Public G6 As Single

'    Public Q1 As Single
'    Public Q2 As Single
'    Public Q3 As Single
'    Public Q4 As Single
'    Public Q5 As Single
'    Public Q6 As Single

'    Public t1 As Single
'    Public t2 As Single
'    Public t3 As Single
'    Public t4 As Single
'    Public t5 As Single
'    Public t6 As Single

'    Public p1 As Single
'    Public p2 As Single
'    Public p3 As Single
'    Public p4 As Single
'    Public p5 As Single
'    Public p6 As Single

'    Public v1 As Single
'    Public v2 As Single
'    Public v3 As Single
'    Public v4 As Single
'    Public v5 As Single
'    Public v6 As Single
'    Public vh1 As Single
'    Public vh2 As Single

'    Public m1 As Single
'    Public m2 As Single
'    Public m3 As Single
'    Public m4 As Single
'    Public m5 As Single
'    Public m6 As Single

'    Public dt12 As Single
'    Public dt45 As Single

'    Public tx1 As Single
'    Public tx2 As Single

'    Public tair1 As Single
'    Public tair2 As Single

'    Public MyTransport As Long
'    Public SPtv1 As Long
'    Public SPtv2 As Long

'    Public dQ1 As Single
'    Public dQ2 As Single


'    Public archType As Short
'End Structure

'Public Structure Archive
'    Public DateArch As DateTime
'    Public Errtime As Long
'    Public HC As Long
'    Public MsgHC As String
'    Public MsgHC_1 As String
'    Public MsgHC_2 As String

'    Public HCtv1 As Long

'    Public HCtv2 As Long


'    Public Tw1 As Single
'    Public Tw2 As Single

'    Public P1 As Single
'    Public T1 As Single
'    Public M1 As Single
'    Public V1 As Single

'    Public P2 As Single
'    Public T2 As Single
'    Public M2 As Single
'    Public V2 As Single

'    Public P3 As Single
'    Public T3 As Single
'    Public M3 As Single
'    Public V3 As Single

'    Public P4 As Single
'    Public T4 As Single
'    Public M4 As Single
'    Public V4 As Single


'    Public P5 As Single
'    Public T5 As Single
'    Public M5 As Single
'    Public V5 As Single

'    Public P6 As Single
'    Public T6 As Single
'    Public M6 As Single
'    Public V6 As Single



'    Public Q1 As Single
'    Public Q2 As Single
'    Public Q3 As Single
'    Public Q4 As Single
'    Public Q5 As Single
'    Public Q6 As Single

'    Public QG1 As Single
'    Public QG2 As Single

'    Public MyTransport As Long
'    Public SPtv1 As Long
'    Public SPtv2 As Long

'    Public tx1 As Long
'    Public tx2 As Long
'    Public tair1 As Long
'    Public tair2 As Long

'    Public V1h As Single
'    Public V2h As Single
'    Public V3h As Single
'    Public V4h As Single
'    Public Q1H As Single
'    Public Q2H As Single

'    Public errtime1 As Int64
'    Public errtime2 As Int64
'    Public oktime1 As Int64
'    Public oktime2 As Int64


'    Public archType As Short
'End Structure

'Public Structure TArchive
'    Public DateArch As DateTime
'    Public MsgHC As String
'    Public MsgHC_1 As String
'    Public MsgHC_2 As String

'    Public V1 As Single
'    Public V2 As Single
'    Public V3 As Single
'    Public V4 As Single
'    Public V5 As Single
'    Public V6 As Single

'    Public M1 As Single
'    Public M2 As Single
'    Public M3 As Single
'    Public M4 As Single
'    Public M5 As Single
'    Public M6 As Single
'    Public Q1 As Single
'    Public Q2 As Single

'    Public TW1 As Single
'    Public TW2 As Single
'    Public Q3 As Single
'    Public Q4 As Single
'    Public Q5 As Single
'    Public Q6 As Single

'    Public HC As Int32
'    Public errtime1 As Int64
'    Public errtime2 As Int64
'    Public oktime1 As Int64
'    Public oktime2 As Int64

'    Public archType As Short

'End Structure





Public Class driver
    Inherits STKTVMain.TVDriver

    Public CurHC As String
    Public IsHC As Boolean = False


    Public Const c_lng256 As Long = 256&

    

    Private mIsConnected As Boolean

    Private SleepTime As Long
    Private SequenceErrorCount As Integer = 0


    Dim IsTArchToRead As Boolean = False

    Dim tv As Short


    Dim ActiveCount As Integer



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
        Return "DANFOSS210_310"
    End Function

    Private Function DecodeError(e As Byte) As String
       
                Return "Ошибка прибора"


    End Function






   


    Private Function DevInit() As Boolean

        'AppVersion = DAN_GetApplication()
        'If AppVersion <> "" Then
        Return True
        'End If
        'Return False
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

     Public overrides Function DeCodeHCNumber(ByVal CodeHC As Long, optional tv As Integer=0) as string

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
    Public overrides Function DeCodeHCText(ByVal CodeHC As Long) As String
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
    Public overrides Function DeCodeHC(ByVal CodeHC As Long) As String
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



    'Public Overrides Function WriteArchToDB() As String

    '    'If Arch.archType <> 4 Then
    '    '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
    '    'End If

    '    WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,dm12,V1H,V2H,V5H,V4H,q1h,q2h,sp_TB1,sp_TB2,q1,q2,q3,q4,q5,q6,TSUM1,TSUM2,hc,hc_code,hc_1,hc_2, oktime,oktime2,errtime,errtime2) values ("
    '    WriteArchToDB = WriteArchToDB + DeviceID.ToString() + ","
    '    WriteArchToDB = WriteArchToDB + Arch.archType.ToString() + ","
    '    WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
    '    WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
    '    WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.T1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.T2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.T3, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.T4, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.T5, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.T6, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.tx1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.tx2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.tair1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.tair2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.P1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.P2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.P3, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.P4, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.P5, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.P6, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V3, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.v4, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.v5, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.v6, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.M1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.M2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.M3, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.M4, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.M5, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.M6, "##############0.000000").Replace(",", ".") + ","
    '    If Not Single.IsNaN(Arch.M1) And Not Single.IsNaN(Arch.M2) Then
    '        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M1 - Arch.M2, "##############0.000000").Replace(",", ".") + ","
    '    Else
    '        WriteArchToDB = WriteArchToDB + "NULL,"
    '    End If

    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1h, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2h, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V3h, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.V4h, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1H, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2H, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + Arch.SPtv1.ToString + ","
    '    WriteArchToDB = WriteArchToDB + Arch.SPtv2.ToString + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q3, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q4, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q5, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q6, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + NanFormat(Arch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","

    '    WriteArchToDB = WriteArchToDB + "'" + S180(Arch.MsgHC) + "','" + S180(Arch.HC.ToString()) + "',"
    '    WriteArchToDB = WriteArchToDB + "'" + S180(Arch.MsgHC_1) + "','" + S180(Arch.MsgHC_2) + "',"
    '    WriteArchToDB = WriteArchToDB + Format((Arch.oktime1), "##############0").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + Format((Arch.oktime2), "##############0").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + Format(Arch.errtime1, "##############0").Replace(",", ".") + ","
    '    WriteArchToDB = WriteArchToDB + Format(Arch.errtime2, "##############0").Replace(",", ".")

    '    WriteArchToDB = WriteArchToDB + ")"
    '    Debug.Print(WriteArchToDB)
    'End Function


    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = ""
        Try
            WriteMArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,p5,p6,g1,g2,g3,g4,g5,g6,m1,m2,m3,m4,m5,m6,v1,v2,v3,v4,v5,v6,q1,q2,q3,q4,q5,q6,tair1,tair2) values ("
            WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
            WriteMArchToDB = WriteMArchToDB + "1,"
            WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.m6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.Q6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair2, "##############0.000000").Replace(",", ".")
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



    Private Function FindValue(ByVal dt As DataTable, ByVal name As String) As Double
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

     


       

        Dim retsum As String
        'retsum = "Мгновенный архив прочитан"
        'If AErr = "" Then
        '    'retsum = "Мгновенный архив прочитан"
        '    retsum = retsum & vbCrLf
        '    EraseInputQueue()
        '    isMArchToDBWrite = True
        '    Return retsum
        'Else
        retsum = "Чтение мгновенного архива не реализовано"
        retsum = retsum & AErr & vbCrLf
        EraseInputQueue()
        isMArchToDBWrite = False
        Return retsum
        'End If

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

    'Public Overrides Function WriteTArchToDB() As String
    '    WriteTArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1,Q2,Q3,Q4,Q5,Q6,M1,M2,M3,M4,M5,M6,v1h,v2h,v3,v4h,v5h,v6,TSUM1,TSUM2,oktime,oktime2,ERRTIME,errtime2) values ("
    '    WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
    '    WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString() + ","
    '    WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
    '    WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
    '    WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q1, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q2, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q3, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q4, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q5, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q6, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M1, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M2, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M3, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M4, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M5, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.M6, "##############0.000000").Replace(",", ".") + ","

    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V1, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V2, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V3, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V4, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V5, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V6, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime1, "##############0").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime2, "##############0").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime1, "##############0").Replace(",", ".") + ","
    '    WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime2, "##############0").Replace(",", ".")
    '    WriteTArchToDB = WriteTArchToDB + ")"
    '    Debug.Print(WriteTArchToDB)
    'End Function

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


   

    


End Class
