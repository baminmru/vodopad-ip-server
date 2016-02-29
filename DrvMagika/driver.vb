
Imports System
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading
Imports System.Collections.Generic
#Region "structures"



Public Structure MArchive
    Public DateArch As DateTime
    Public HC As Int32
    Public MsgHC As String

    Public HCtv1 As Long
    Public MsgHCtv1 As String

    Public HCtv2 As Long
    Public MsgHCtv2 As String

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
    Public OKTime1 As Long
    Public OKTime2 As Long
    Public Errtime1 As Long
    Public Errtime2 As Long
End Structure

Public Structure Archive
    Public DateArch As DateTime
    Public Errtime As Long
    Public HC As Long
    Public MsgHC As String

    Public HCtv1 As Long
    Public MsgHCtv1 As String

    Public HCtv2 As Long
    Public MsgHCtv2 As String

    Public Tw1 As Single
    Public Tw2 As Single

    Public P1 As Single
    Public T1 As Single
    Public M2 As Single
    Public V1 As Single

    Public P2 As Single
    Public T2 As Single
    Public M3 As Single
    Public V2 As Single

    Public V3 As Single
    Public M1 As Single

    Public Q1 As Single
    Public Q2 As Single

    Public QG1 As Single
    Public QG2 As Single

    Public MyTransport As Long
    Public SPtv1 As Long
    Public SPtv2 As Long

    Public tx1 As Long
    Public tx2 As Long
    Public tair1 As Long
    Public tair2 As Long

    Public T3 As Single
    Public T4 As Single
    Public T5 As Single
    Public T6 As Single
    Public P3 As Single
    Public P4 As Single
    Public v4 As Single
    Public v5 As Single
    Public v6 As Single
    Public M4 As Single
    Public M5 As Single
    Public M6 As Single
    Public V1h As Double
    Public V2h As Double
    Public V3h As Double
    Public V4h As Double
    Public Q1H As Double
    Public Q2H As Double

    Public errtime1 As Int64
    Public errtime2 As Int64
    Public oktime1 As Int64
    Public oktime2 As Int64
    Public worktime As Int64


    Public archType As Short
End Structure

Public Structure TArchive
    Public DateArch As DateTime


    Public V1 As Double
    Public V2 As Double
    Public V3 As Double
    Public V4 As Double
    Public V5 As Double
    Public V6 As Double

    Public M1 As Double
    Public M2 As Double
    Public M3 As Double
    Public M4 As Double
    Public M5 As Double
    Public M6 As Double
    Public Q1 As Double
    Public Q2 As Double
    Public P1 As Double
    Public P2 As Double
    Public P3 As Double
    Public P4 As Double
    Public P5 As Double
    Public P6 As Double


    Public TW1 As Double
    Public TW2 As Double
    Public Q3 As Double
    Public Q4 As Double
    Public HC As Int32
    Public errtime1 As Int64
    Public errtime2 As Int64
    Public oktime1 As Int64
    Public oktime2 As Int64
    Public worktime As Int64


    Public t1 As Single
    Public t2 As Single
    Public t3 As Single
    Public t4 As Single
    Public t5 As Single
    Public t6 As Single

    Public archType As Short
End Structure


#End Region


Public Class driver
    Inherits STKTVMain.TVDriver

    Private Const MAXHOUR As Integer = 60 * 24
    Private Const MAXDAY As Integer = 60

    Private mIsConnected As Boolean

    Private DeviceSubtype As String

    Private isTCP As Boolean
    Private SleepTime As Long
    Private HCash As Dictionary(Of String, CashItem)
    Private DCash As Dictionary(Of String, CashItem)
    Private lastD As DateTime
    Private lastH As DateTime

    Dim tArch As TArchive
    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

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

    Dim buffer(0 To 73) As Byte
    Dim bufferindex As Short = 0



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
        Return "MAGIKA"
    End Function




    Private Function DevInit() As Boolean


        Dim Frame(10) As Byte
        Dim ch As UInt16
        Dim tryCnt As Integer
        Dim ok As Boolean
        tryCnt = 7
        ok = False
        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            EraseInputQueue()
            Frame(0) = &H12
            Frame(1) = &H80
            Dim i As Integer
            For i = 0 To 7
                Frame(i + 2) = Asc(Mid(NPPassword, i + 1, 1))
            Next
            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 10)
            WaitForData()
            Dim b(4096) As Byte
            Dim cnt As Integer
            cnt = MyTransport.BytesToRead
            If cnt > 0 Then
                Dim ptr As Integer
                Dim sz As Integer
                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt
                    If b(0) = &H1 Then
                        Return True
                    End If
                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            End If
        End While
        If Not ok Then
            Return False
        End If
    End Function


    Public Overrides Sub Connect()
        SleepTime = 700
        EraseInputQueue()
        mIsConnected = False
        Try

            If DevInit() Then
                mIsConnected = True



                GetCFG()
                RaiseIdle()
                Thread.Sleep(CalcInterval(10))



            End If
        Catch exc As Exception
            Return
        End Try
    End Sub


    Private Sub GetCFG()
        Dim cmd(2) As Byte
        cmd(0) = &H12
        cmd(1) = &H11
        EraseInputQueue()
        MyTransport.Write(cmd, 0, 2)
        WaitForData()

        Dim b(10) As Byte
        Dim cnt As Integer
        Dim sz As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            sz = 0
            While cnt > 0
                MyTransport.Read(b, sz, cnt)
                sz += cnt
                RaiseIdle()
                Thread.Sleep(CalcInterval(10))
                cnt = MyTransport.BytesToRead
            End While
        End If
        If sz = 4 Then
            DeviceSubtype = "A"
            If b(0) And 64 Then
                DeviceSubtype = "E"
                Exit Sub
            End If
            If b(0) And 32 Then
                DeviceSubtype = "A"
            End If
            If b(3) And 16 Then
                DeviceSubtype = "AM"
            End If

        End If

    End Sub

    Private Function BCD(ByVal B As Byte) As Byte
        Dim i As UInteger
        Dim o As UInteger
        i = B
        o = (i Mod 16) + (i \ 16) * 10
        Return o And &HFF
        'Return B
    End Function

  

   

    Private Function ReadArchRecord(ByVal ArchType As Short, ByVal ArchDate As Date) As Byte()


        Dim ArchYear As Short
        Dim ArchMonth As Short
        Dim ArchDay As Short
        Dim ArchHour As Short
        Dim cc As CashItem
        Dim key As String
        ArchYear = ArchDate.Year
        ArchMonth = ArchDate.Month
        ArchDay = ArchDate.Day
        ArchHour = ArchDate.Hour




        Try

            If ArchType = archType_hour Then
                key = ArchYear.ToString + "_" + ArchMonth.ToString() + "_" + ArchDay.ToString() + "_" + (ArchHour + 1).ToString()
                If HCash.ContainsKey(key) Then
                    cc = HCash(key)
                    Return cc.data
                End If
                Debug.Print("Key not found " + key + " lastH=" + lastH.ToString)

            Else
                key = ArchYear.ToString + "_" + ArchMonth.ToString() + "_" + ArchDay.ToString()
                If DCash.ContainsKey(key) Then
                    cc = DCash(key)
                    Return cc.data
                End If
                Debug.Print("Key not found " + key + " lastD=" + lastD.ToString)
            End If




        Catch
            Return Nothing
        End Try

        Return Nothing
    End Function

    Private Function AddHourBlock(ByVal b() As Byte, ByVal offset As Integer) As Boolean
        Dim I As Integer
        Dim key As String
        Dim cc As CashItem
        Dim d As DateTime

        key = (2000 + BCD(b(offset + 3 + 1))).ToString + "_" + BCD(b(offset + 1 + 1)).ToString() + "_" + BCD(b(offset + 0 + 1)).ToString() + "_" + (BCD(b(offset + 2 + 1))).ToString()
        If HCash.ContainsKey(key) Then Return False

        Debug.Print("Add " + key)
        DriverTransport.SendEvent(UnitransportAction.ReceiveData, key)
        cc = New CashItem
        With cc
            .Y = 2000 + BCD(b(offset + 3 + 1))
            .M = BCD(b(offset + 1 + 1))
            .D = BCD(b(offset + 0 + 1))
            .H = BCD(b(offset + 2 + 1))
            For I = 0 To b(offset)
                .data(I) = b(offset + 1 + I)
            Next
            .ok = True
            d = DateSerial(.Y, .M, .D)
            d = d.AddTicks(TimeSerial(.H - 1, 0, 0).Ticks)
        End With
        If lastH > d Then lastH = d
        HCash.Add(key, cc)
        Return True
    End Function

    Private Function AddDayBlock(ByVal b() As Byte, ByVal offset As Integer) As Boolean
        Dim I As Integer
        Dim key As String
        Dim cc As CashItem
        Dim d As DateTime

        key = (2000 + BCD(b(offset + 3 + 1))).ToString + "_" + BCD(b(offset + 1 + 1)).ToString() + "_" + BCD(b(offset + 0 + 1)).ToString()
        If DCash.ContainsKey(key) Then Return False

        DriverTransport.SendEvent(UnitransportAction.ReceiveData, key)
        Debug.Print("Add " + key)
        cc = New CashItem
        With cc


            .Y = 2000 + BCD(b(offset + 3 + 1))
            .M = BCD(b(offset + 1 + 1))
            .D = BCD(b(offset + 0 + 1))
            .H = 0 'b(offset + 2 + 1) ???
            For I = 0 To b(offset)
                .data(I) = b(offset + 1 + I)
            Next
            .ok = True
            d = DateSerial(.Y, .M, .D)
        End With
        DCash.Add(key, cc)
        If lastD > d Then lastD = d
        Return True
    End Function


    Private Sub PreloadHours(ByVal dt As Date)

        Dim ok As Boolean
        Dim Frame(10) As Byte
        If dt >= GetDeviceDate().AddHours(-1) Then Exit Sub
        If dt > lastH Then Exit Sub
        If Not ReadArchRecord(archType_hour, dt) Is Nothing Then Exit Sub

        ok = False
        EraseInputQueue()
        Frame(0) = &H12
        Frame(1) = 4
        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 2)
        Thread.Sleep(CalcInterval(70))
        WaitForData()

        Dim sz As Integer
        'Dim rcnt As Integer
        Dim b(4096) As Byte
        Dim cnt As Integer
        'rcnt = 0
        cnt = MyTransport.BytesToRead
        sz = 0
        While cnt > 0
            MyTransport.Read(b, sz, cnt)
            sz += cnt
            ok = True

            RaiseIdle()
            Thread.Sleep(CalcInterval(50))
            cnt = MyTransport.BytesToRead
        End While
        If b(0) = 1 Then
            If b(1) > 0 Then
                AddHourBlock(b, 1)
                ok = True
                While ok
                    Frame(0) = &H1
                    MyTransport.Write(Frame, 0, 1)
                    Thread.Sleep(CalcInterval(70))
                    WaitForData()
                    cnt = MyTransport.BytesToRead
                    sz = 0
                    While cnt > 0
                        MyTransport.Read(b, sz, cnt)
                        sz += cnt
                        ok = True
                        RaiseIdle()
                        Thread.Sleep(CalcInterval(50))
                        cnt = MyTransport.BytesToRead
                    End While
                    If b(0) > 0 Then
                        AddHourBlock(b, 0)


                        If HCash.Count = MAXHOUR Then
                            ok = False
                        Else
                            ok = True
                        End If

                        If Not ReadArchRecord(archType_hour, dt) Is Nothing Then ok = False

                    Else
                        ok = False
                    End If
                End While

                Frame(0) = &H8
                MyTransport.Write(Frame, 0, 1, True)
                Thread.Sleep(CalcInterval(70))

            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If



    End Sub


    Private Sub PreloadDays(ByVal dt As Date)

        Dim ok As Boolean
        Dim Frame(10) As Byte
        If dt >= GetDeviceDate().AddDays(-1) Then Exit Sub
        If dt > lastD Then Exit Sub
        If Not ReadArchRecord(archType_day, dt) Is Nothing Then Exit Sub
        
        ok = False
        EraseInputQueue()
        Frame(0) = &H12
        Frame(1) = 5
        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 2)
        Thread.Sleep(CalcInterval(70))
        WaitForData()

        Dim sz As Integer
        'Dim rcnt As Integer
        Dim b(4096) As Byte
        Dim cnt As Integer
        'rcnt = 0
        cnt = MyTransport.BytesToRead
        sz = 0
        While cnt > 0
            MyTransport.Read(b, sz, cnt)
            sz += cnt
            ok = True

            RaiseIdle()
            Thread.Sleep(CalcInterval(50))
            cnt = MyTransport.BytesToRead
        End While
        If sz > 40 Then
            If b(0) = 1 Then
                If b(1) > 0 Then
                    AddDayBlock(b, 1)
                    ok = True
                    While ok
                        Frame(0) = &H1
                        MyTransport.Write(Frame, 0, 1)
                        Thread.Sleep(CalcInterval(70))
                        WaitForData()
                        cnt = MyTransport.BytesToRead
                        sz = 0
                        While cnt > 0
                            MyTransport.Read(b, sz, cnt)
                            sz += cnt
                            ok = True
                            RaiseIdle()
                            Thread.Sleep(CalcInterval(10))
                            cnt = MyTransport.BytesToRead
                        End While
                        If b(0) > 0 Then
                            AddDayBlock(b, 0)



                            If DCash.Count >= MAXDAY Then
                                ok = False
                            Else
                                ok = True
                            End If


                            If Not ReadArchRecord(archType_day, dt) Is Nothing Then ok = False

                        Else
                            ok = False
                        End If
                    End While
                End If


                Frame(0) = &H8
                MyTransport.Write(Frame, 0, 1, True)
                Thread.Sleep(CalcInterval(70))

            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If



    End Sub


    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
        Dim retsum As String
        Dim ok As Boolean = False
        Dim SI() As Byte
        Dim dt As Date
        cleararchive(Arch)
        Arch.archType = ArchType

        Try

            If ArchType = archType_hour Then
                dt = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                Arch.DateArch = dt
                PreloadHours(dt)
                SI = ReadArchRecord(ArchType, dt)
                If Not SI Is Nothing Then
                    ProcessH(Arch, SI)
                    ok = True
                End If
            Else
                dt = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                Arch.DateArch = dt
                PreloadDays(dt)
                SI = ReadArchRecord(ArchType, dt)
                If Not SI Is Nothing Then
                    ProcessD(Arch, SI)
                    ok = True
                End If

            End If



            If ok Then
                retsum = "Архив прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = True
            Else
                retsum = "Ошибка: не удалось получить архив за " & dt.ToString()
                EraseInputQueue()
                isArchToDBWrite = False
            End If

            Return retsum
        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function


    Private Function VerifySysInt(ByVal si() As Byte) As Boolean
        Return True
        Dim s As Integer
        Dim i As Integer
        s = 0
        For i = 0 To 254 '&HFB
            s = (s And &HFF) + si(i)
        Next
        s = s And &HFF

        If s = si(255) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function DeCodeHCNumber(ByVal CodeHC As Long) As String
        DeCodeHCNumber = CodeHC.ToString()
    End Function
    Public Function DeCodeHCText(ByVal CodeHC As Long) As String
        Return CodeHC.ToString
    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String
        Dim s As String
        s = ""
        If (CodeHC And 1) = 1 Then s = s & "1"
        If (CodeHC And 2) = 2 Then s = s & "2"
        If (CodeHC And 4) = 4 Then s = s & "3"
        If (CodeHC And 8) = 8 Then s = s & "4"
        If (CodeHC And 16) = 16 Then s = s & "5"
        If (CodeHC And 32) = 32 Then s = s & "6"
        If (CodeHC And 64) = 64 Then s = s & "7"
        If (CodeHC And 128) = 128 Then s = s & "8"

        Return s
    End Function


    Public Overrides Function WriteArchToDB() As String

        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,dm12,V1H,V2H,V5H,V4H,q1h,q2h,sp_TB1,sp_TB2,q1,q2,q4,q5,TSUM1,TSUM2,hc_code,hc,hc_1,hc_2, worktime,oktime,oktime2, errtime,errtime2,hcraw1,hcraw2) values ("
        WriteArchToDB = WriteArchToDB + "'" + DeviceID.ToString() + "',"
        WriteArchToDB = WriteArchToDB + "'" + Arch.archType.ToString() + "',"
        WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.T1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.T2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.T3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.T4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.T5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.T6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.tx1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.tx2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.tair1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.tair2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.P1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.P2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.P3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.P4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.v4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.v5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.v6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.M1 - Arch.M2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V1h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V2h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V3h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.V4h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.Q1H, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.Q2H, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv1.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv2.ToString + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.QG1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.QG2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.Tw1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.Tw2, "##############0.000").Replace(",", ".") + ","




        WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHC(Arch.HC)) + "','" + S180(DeCodeHC(Arch.HC)) + "'"
        WriteArchToDB = WriteArchToDB + ",'" + DeCodeHC(Arch.HC) + "','" + DeCodeHC(Arch.HCtv2) + "'"
        WriteArchToDB = WriteArchToDB + "," + Format((Arch.worktime), "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + Format((Arch.oktime1), "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + Format((Arch.oktime2), "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + Format(Arch.errtime1, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + Format(Arch.errtime2, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + "'" + Arch.HC.ToString() + "','" + Arch.HCtv2.ToString() + "'"
        WriteArchToDB = WriteArchToDB + ")"
        Debug.Print(WriteArchToDB)
    End Function

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Public Overrides Sub EraseInputQueue()

        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()

    

    End Sub
    Private Sub cleararchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue

        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHCtv1 = ""

        arc.HCtv2 = 0
        arc.MsgHCtv2 = ""

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
        marc.MsgHCtv1 = ""

        marc.HCtv2 = 0
        marc.MsgHCtv2 = ""

        marc.G1 = 0
        marc.G2 = 0
        marc.G3 = 0
        marc.G4 = 0
        marc.G5 = 0
        marc.G6 = 0

        marc.t1 = 0
        marc.t2 = 0
        marc.t3 = 0
        marc.t4 = 0
        marc.t5 = 0
        marc.t6 = 0

        marc.p1 = 0
        marc.p2 = 0
        marc.p3 = 0
        marc.p4 = 0
        marc.p5 = 0
        marc.p6 = 0

        marc.dt12 = 0
        marc.dt45 = 0

        marc.tx1 = 0
        marc.tx2 = 0

        marc.tair1 = 0
        marc.tair2 = 0

        marc.MyTransport = 0
        marc.SPtv1 = 0
        marc.SPtv2 = 0


        marc.archType = 1
        isMArchToDBWrite = False
    End Sub

    Private Function ChanelToByte(ByVal ch As Byte) As Byte
        '1 – первый канал,
        '0 – второй канал,
        '2 – холодная вода,
        '4 – четвертый канал,
        '3 – пятый канал.

        Select Case ch
            Case 1
                Return 1
            Case 2
                Return 0
            Case 3
                Return 2
            Case 4
                Return 4
            Case 5
                Return 3
            Case Else
                Return ch
        End Select
    End Function


    Private Function GetDbl(ByVal SI() As Byte, ByVal PosL As Integer, ByVal PosH As Integer, ByVal Chanel As Integer) As Double
        Dim f As Double
        Dim l As Single
        Dim h As ULong
        Dim pos As Integer
        pos = PosL + 4 * (Chanel)

        l = Bytes2Single(SI, pos, True)

        pos = PosH + 4 * (Chanel)
        h = 0
        Dim b1 As Integer, b2 As Integer, b3 As Integer, b0 As Integer
        Try
            b0 = SI(pos)
            b1 = SI(pos + 1)
            b2 = SI(pos + 2)
            b3 = SI(pos + 3)
            h = (b0 << 24) + (b1 << 16) + (b2 << 8) + b3
        Catch ex As Exception

            h = 0

        End Try

        f = l + h
        Return f
    End Function

    Private Function GetFlt(ByVal SI() As Byte, ByVal PosL As Integer, ByVal Chanel As Integer) As Single

        Dim l As Single
        Dim lrev As Single
        Dim pos As Integer
        pos = PosL + 4 * (Chanel)
        Try

            l = MyBytes2Single(SI, pos, False)

            If l > 1 Or l < 0 Then
                Debug.Print(Hex(SI(pos)) + " " + Hex(SI(pos + 1)) + " " + Hex(SI(pos + 2)) + " " + Hex(SI(pos + 3)) + "-->" + l.ToString)
            End If

            'lrev = MyBytes2Single(SI, pos, True)

            'l = Byte2Float32(SI, pos, False)
        Catch ex As Exception
            l=0
        End Try

        Return l
    End Function

    'Private Function GetFlt24(ByVal SI() As Byte, ByVal PosL As Integer, ByVal Chanel As Integer) As Single

    '    Dim l As Single
    '    Dim pos As Integer
    '    pos = PosL + 4 * (Chanel)
    '    l = Byte2Float24(SI, pos, True)
    '    Return l
    'End Function
    Private Function GetLng(ByVal SI() As Byte, ByVal PosH As Integer, ByVal Chanel As Integer) As Long

        Dim h As ULong
        Dim pos As Integer
        pos = PosH + 4 * (Chanel)
        h = 0
        Dim b1 As Integer, b2 As Integer, b3 As Integer, b0 As Integer
        Try
            b0 = SI(pos)
            b1 = SI(pos + 1)
            b2 = SI(pos + 2)
            b3 = SI(pos + 3)
            h = (b0 << 24) + (b1 << 16) + (b2 << 8) + b3
        Catch ex As Exception

            h = 0
        End Try
        Return h
    End Function

    Private Function GetInt24(ByVal SI() As Byte, ByVal PosH As Integer, ByVal Chanel As Integer) As Long

        Dim h As ULong
        Dim pos As Integer
        pos = PosH + 4 * (Chanel)
        h = 0
        Dim b2 As Integer, b1 As Integer, b0 As Integer
        Try
            b0 = SI(pos)
            b1 = SI(pos + 1)
            b2 = SI(pos + 2)
            h = (b0 << 16) + (b1 << 8) + (b2)
        Catch ex As Exception

            h = 0
        End Try
        Return h
    End Function
    Private Function GetInt(ByVal SI() As Byte, ByVal PosH As Integer, ByVal SysNum As Integer, ByVal Chanel As Integer) As Integer
        Dim h As Integer
        Dim pos As Integer
        pos = PosH + 6 * SysNum + 2 * (Chanel)
        Dim b1 As Integer, b0 As Integer
        b0 = SI(pos)
        b1 = SI(pos + 1)
        h = (b0 << 8) + b1
        Return h
    End Function


    Private Sub ProcessT(ByRef tarc As TArchive, ByVal SI() As Byte)
        Dim offset As Integer
        offset = 2
        Debug.Print("M1:")
        tarc.M1 = GetInt24(SI, 36 + offset, 0) + GetFlt(SI, 8 + offset, 0)
        Debug.Print("M2:")
        tarc.M2 = GetInt24(SI, 39 + offset, 0) + GetFlt(SI, 12 + offset, 0)
        Debug.Print("M3:")
        tarc.M3 = GetInt24(SI, 42 + offset, 0) + GetFlt(SI, 16 + offset, 0)
        Debug.Print("M4:")
        tarc.M4 = GetInt24(SI, 45 + offset, 0) + GetFlt(SI, 20 + offset, 0)
        Debug.Print("Q1:")
        tarc.Q1 = GetInt24(SI, 48 + offset, 0) + GetFlt(SI, 24 + offset, 0)
        Debug.Print("Q2:")
        tarc.Q2 = GetInt24(SI, 51 + offset, 0) + GetFlt(SI, 28 + offset, 0)
        Debug.Print("Q3:")
        tarc.Q3 = GetInt24(SI, 54 + offset, 0) + GetFlt(SI, 32 + offset, 0)


        If DeviceSubtype = "E" Then
            tarc.oktime1 = GetInt24(SI, 57 + offset, 0)
            tarc.oktime2 = GetInt24(SI, 60 + offset, 0)
            tarc.worktime = GetInt24(SI, 63 + offset, 0)
            tarc.errtime1 = tarc.worktime - tarc.oktime1
            tarc.errtime2 = tarc.worktime - tarc.oktime2

        Else
            tarc.oktime1 = GetInt24(SI, 57 + offset, 0)
            tarc.worktime = GetInt24(SI, 60 + offset, 0)
            tarc.errtime1 = tarc.worktime - tarc.oktime1
        End If

        

    End Sub

    Private Sub ProcessM(ByRef tarc As MArchive, ByVal SI() As Byte)
        Dim t As Double
        Dim offset As Integer
        offset = 2

        t = GetFlt(SI, 20 + offset, 0)

        tarc.v1 = GetFlt(SI, 0 + offset, 0) * 3600000 / 1.024 / t
        tarc.v2 = GetFlt(SI, 4 + offset, 0) * 3600000 / 1.024 / t
        tarc.m1 = GetFlt(SI, 8 + offset, 0) * 3600000 / 1.024
        tarc.m2 = GetFlt(SI, 12 + offset, 0) * 3600000 / 1.024
        tarc.Q1 = GetFlt(SI, 16 + offset, 0) * 3600000 / 1.024
        tarc.t1 = GetFlt(SI, 24 + offset, 0)
        tarc.t2 = GetFlt(SI, 28 + offset, 0)
        tarc.t3 = GetFlt(SI, 32 + offset, 0)
        tarc.t4 = GetFlt(SI, 36 + offset, 0)
        tarc.p1 = GetFlt(SI, 40 + offset, 0)
        tarc.p2 = GetFlt(SI, 44 + offset, 0)
        tarc.v3 = GetFlt(SI, 48 + offset, 0) * 3600000 / 1.024 / t
        tarc.v4 = GetFlt(SI, 52 + offset, 0) * 3600000 / 1.024 / t
        tarc.m3 = GetFlt(SI, 56 + offset, 0) * 3600000 / 1.024
        tarc.m4 = GetFlt(SI, 60 + offset, 0) * 3600000 / 1.024
        tarc.Q2 = GetFlt(SI, 68 + offset, 0) * 3600000 / 1.024
        tarc.Q3 = GetFlt(SI, 72 + offset, 0) * 3600000 / 1.024



    End Sub

    Private Sub ProcessH(ByRef tarc As Archive, ByVal SI() As Byte)
        With tarc
            If DeviceSubtype = "A" Then
                .oktime1 = SI(4)
                .HC = SI(5)
                .Q1 = GetFlt(SI, 14, 0)
                .M1 = GetFlt(SI, 18, 0)
                .M2 = GetFlt(SI, 22, 0)
                .M3 = GetFlt(SI, 26, 0)
                .T1 = GetFlt(SI, 30, 0)
                .T2 = GetFlt(SI, 34, 0)
                .T3 = GetFlt(SI, 38, 0)
                .P1 = GetFlt(SI, 40, 0)
                .P2 = GetFlt(SI, 46, 0)
                .errtime1 = 60 - .oktime1
            End If
            If DeviceSubtype = "AM" Then
                .oktime1 = SI(4)
                .HC = SI(5)
                .Q1 = GetFlt(SI, 14, 0)
                .QG1 = GetFlt(SI, 18, 0)
                .QG2 = GetFlt(SI, 22, 0)
                .M1 = GetFlt(SI, 18 + 8, 0)
                .M2 = GetFlt(SI, 22 + 8, 0)
                .M3 = GetFlt(SI, 26 + 8, 0)
                .T1 = GetFlt(SI, 30 + 8, 0)
                .T2 = GetFlt(SI, 34 + 8, 0)
                .T3 = GetFlt(SI, 38 + 8, 0)
                .P1 = GetFlt(SI, 40 + 8, 0)
                .P2 = GetFlt(SI, 46 + 8, 0)
                .errtime1 = 60 - .oktime1
            End If

            If DeviceSubtype = "E" Then

                .HC = SI(5)
                .HCtv2 = SI(14)
                .Q1 = GetFlt(SI, 18, 0)
                .Q2 = GetFlt(SI, 22, 0)
                .M1 = GetFlt(SI, 26, 0)
                .M2 = GetFlt(SI, 30, 0)
                .M3 = GetFlt(SI, 34, 0)
                .M4 = GetFlt(SI, 38, 0)
                .T1 = GetFlt(SI, 42, 0)
                .T2 = GetFlt(SI, 46, 0)
                .T3 = GetFlt(SI, 50, 0)
                .T4 = GetFlt(SI, 54, 0)
                .P1 = GetFlt(SI, 58, 0)
                .P2 = GetFlt(SI, 62, 0)


                .oktime1 = SI(4)
                .oktime2 = SI(15)
                .errtime1 = 60 - .oktime1
                .errtime2 = 60 - .oktime2


            End If
        End With
    End Sub




    Private Sub ProcessD(ByRef tarc As Archive, ByVal SI() As Byte)
        With tarc
            If DeviceSubtype = "A" Then
                .worktime = GetInt(SI, 4, 0, 0)
                .HC = SI(20)
                .Q1 = GetFlt(SI, 22, 0)
                .M1 = GetFlt(SI, 26, 0)
                .M2 = GetFlt(SI, 30, 0)
                .M3 = GetFlt(SI, 34, 0)
                .T1 = GetFlt(SI, 38, 0)
                .T2 = GetFlt(SI, 42, 0)
                .T3 = GetFlt(SI, 46, 0)
                .P1 = GetFlt(SI, 50, 0)
                .P2 = GetFlt(SI, 54, 0)

                .Q1H = GetInt24(SI, 106, 0) + GetFlt(SI, 82, 0)
                .Q2H = GetInt24(SI, 109, 0) + GetFlt(SI, 86, 0)
                '.q3H = GetInt24(SI, 112, 0) + GetFlt(SI, 90, 0)
                .oktime1 = GetInt24(SI, 115, 0)
                .errtime1 = GetInt24(SI, 118, 0) - .oktime1
                .Tw1 = GetInt24(SI, 118, 0)
            End If
            If DeviceSubtype = "AM" Then
                .worktime = GetInt(SI, 4, 0, 0)
                .HC = SI(20)
                .Q1 = GetFlt(SI, 22, 0)
                .QG1 = GetFlt(SI, 26, 0)
                .QG2 = GetFlt(SI, 30, 0)
                .M1 = GetFlt(SI, 26 + 8, 0)
                .M2 = GetFlt(SI, 30 + 8, 0)
                .M3 = GetFlt(SI, 34 + 8, 0)
                .T1 = GetFlt(SI, 38 + 8, 0)
                .T2 = GetFlt(SI, 42 + 8, 0)
                .T3 = GetFlt(SI, 46 + 8, 0)
                .P1 = GetFlt(SI, 50 + 8, 0)
                .P2 = GetFlt(SI, 54 + 8, 0)

                .Q1H = GetInt24(SI, 106 + 8, 0) + GetFlt(SI, 82 + 8, 0)
                .Q2H = GetInt24(SI, 109 + 8, 0) + GetFlt(SI, 86 + 8, 0)
                '.q3H = GetInt24(SI, 112+8, 0) + GetFlt(SI, 90+8, 0)
                .oktime1 = GetInt24(SI, 115 + 8, 0)
                .errtime1 = GetInt24(SI, 118 + 8, 0) - .oktime1
                .Tw1 = GetInt24(SI, 118, 0)
            End If

            If DeviceSubtype = "E" Then
                .worktime = GetInt(SI, 4, 0, 0)
                .HC = SI(20)
                .HCtv2 = SI(22)
                .Q1 = GetFlt(SI, 29, 0)
                .Q2 = GetFlt(SI, 33, 0)
                .M1 = GetFlt(SI, 37, 0)
                .M2 = GetFlt(SI, 41, 0)
                .M3 = GetFlt(SI, 45, 0)
                .M4 = GetFlt(SI, 49, 0)
                .T1 = GetFlt(SI, 53, 0)
                .T2 = GetFlt(SI, 57, 0)
                .T3 = GetFlt(SI, 61, 0)
                .T4 = GetFlt(SI, 65, 0)
                .P1 = GetFlt(SI, 69, 0)
                .P2 = GetFlt(SI, 73, 0)

                .Q1H = GetInt24(SI, 133, 0) + GetFlt(SI, 109, 0)
                .Q2H = GetInt24(SI, 136, 0) + GetFlt(SI, 113, 0)
                '.q3H = GetInt24(SI, 139, 0) + GetFlt(SI, 117, 0)

                .oktime1 = GetInt24(SI, 142, 0)
                .oktime2 = GetInt24(SI, 145, 0) ' ГВС

                .Errtime = 1440 - GetInt(SI, 4, 0, 0)
                .errtime1 = GetInt24(SI, 148, 0) - .oktime1
                .errtime2 = GetInt24(SI, 148, 0) - .oktime2
                .Tw1 = GetInt24(SI, 148, 0)

            End If
        End With



    End Sub



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


        marc.V1 = 0
        marc.V2 = 0
        marc.V3 = 0
        marc.V4 = 0
        marc.V5 = 0
        marc.V6 = 0
        marc.M1 = 0
        marc.M2 = 0
        marc.M3 = 0
        marc.M4 = 0
        marc.M5 = 0
        marc.M6 = 0
        marc.Q1 = 0
        marc.Q2 = 0
        marc.TW1 = 0
        marc.TW2 = 0

        marc.archType = 2
        isTArchToDBWrite = False
    End Sub

    Public Overrides Function ReadTArch() As String
        Dim AErr As String = ""
        clearTarchive(tArch)
        EraseInputQueue()

        Dim ok As Boolean
        Dim Frame(10) As Byte
        Frame(0) = &H12
        Frame(1) = &H8

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 2)
        Thread.Sleep(CalcInterval(70))
        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then

            Dim sz As Integer

            sz = 0
            While cnt > 0
                MyTransport.Read(b, sz, cnt)

                sz += cnt
                RaiseIdle()
                Thread.Sleep(CalcInterval(50))
                cnt = MyTransport.BytesToRead
                ok = True
            End While
        End If

        Frame(0) = &H1


        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 1, True)


        ProcessT(tArch, b)
        tArch.DateArch = GetDeviceDate()

        Dim retsum As String
        retsum = "Итоговый архив прочитан"
        If ok And AErr = "" Then

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

    End Function



    Public Overrides Function ReadMArch() As String
        Dim AErr As String = ""
        clearMarchive(mArch)
        EraseInputQueue()

        Dim ok As Boolean
        Dim Frame(10) As Byte


        ok = False

        EraseInputQueue()
        Frame(0) = &H12
        Frame(1) = 6



        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 2)
        Thread.Sleep(CalcInterval(70))
        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
    
        Dim sz As Integer
        sz = 0
        While cnt > 0
            MyTransport.Read(b, sz, cnt)
            sz += cnt
            ok = True

            RaiseIdle()
            Thread.Sleep(CalcInterval(50))
            cnt = MyTransport.BytesToRead
        End While

        Frame(0) = &H1
        MyTransport.Write(Frame, 0, 1, True)

        If ok Then
            ProcessM(mArch, b)
            mArch.DateArch = GetDeviceDate()
        End If


        Dim retsum As String
        retsum = "Мгновенный архив прочитан"
        If ok And AErr = "" Then
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isMArchToDBWrite = True
            Return retsum
        Else
            retsum = "Не удалось получить часть параметров "
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isMArchToDBWrite = True
            Return retsum
        End If

    End Function

    Public Overrides Function WriteTArchToDB() As String
        WriteTArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1H,Q2H,Q4,Q5,M1,M2,M3,M4,M5,M6,v1h,v2h,v3,v4h,v5h,v6,p1,p2,p3,p4,p5,p6,t1,t2,t3,t4,t5,t6,TSUM1,TSUM2,worktime,ERRTIME,oktime,oktime2,errtime2) values ("
        WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.Q3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.Q4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.M1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.M2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.M3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.M4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.M5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.M6, "##############0.000").Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + Format(tArch.V1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.V2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.V3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.V4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.V5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.V6, "##############0.000").Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + Format(tArch.P1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.P2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.P3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.P4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.P5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.P6, "##############0.000").Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + Format(tArch.t1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.t2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.t3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.t4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.t5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.t6, "##############0.000").Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + Format(tArch.TW1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.TW2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.worktime, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime1, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime1, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime2, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime2, "##############0").Replace(",", ".")
        WriteTArchToDB = WriteTArchToDB + ")"
    End Function


    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1,Q2,Q4,Q5,M1,M2,M3,M4,M5,M6,v1,v2,v3,v4,v5,v6,p1,p2,p3,p4,p5,p6,t1,t2,t3,t4,t5,t6,worktime,ERRTIME) values ("
        WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
        WriteMArchToDB = WriteMArchToDB + mArch.archType.ToString() + ","
        WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
        WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
        WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.Q3, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.Q4, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.m1, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.m2, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.m3, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.m4, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.M5, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.M6, "##############0.000").Replace(",", ".") + ","

        WriteMArchToDB = WriteMArchToDB + Format(mArch.v1, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.v2, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.v3, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.v4, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.V5, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.V6, "##############0.000").Replace(",", ".") + ","

        WriteMArchToDB = WriteMArchToDB + Format(mArch.p1, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.p2, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.p3, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.p4, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.p5, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.p6, "##############0.000").Replace(",", ".") + ","

        WriteMArchToDB = WriteMArchToDB + Format(mArch.t1, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.t2, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.t3, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.t4, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.t5, "##############0.000").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.t6, "##############0.000").Replace(",", ".") + ","

        WriteMArchToDB = WriteMArchToDB + Format(mArch.OKTime1 / 60, "##############0").Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + Format(mArch.Errtime1 / 60, "##############0").Replace(",", ".")
        WriteMArchToDB = WriteMArchToDB + ")"
    End Function

    Private Function ExtLong4(ByVal extStr As String) As Double
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

    'Private Function TableForArch(ByVal ArchType As Short) As String
    '    Dim tName As String = ""
    '    If ArchType = 1 Then
    '        tName = "TPLC_M"
    '    End If

    '    If ArchType = 3 Then
    '        tName = "TPLC_H"
    '    End If
    '    If ArchType = 4 Then
    '        tName = "TPLC_D"
    '    End If
    '    If ArchType = 2 Then
    '        tName = "TPLC_T"
    '    End If
    '    Return tName
    'End Function

    Public Overrides Function ReadSystemParameters() As DataTable
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        'Dim dr As DataRow

        'Dim AErr As String = ""
        'clearTarchive(tArch)
        'EraseInputQueue()

        'Dim ok As Boolean
        'Dim Frame(10) As Byte
        'Dim addr As UInteger
        'Dim ch As UInt16
        'Dim pass As Integer
        'Dim SysInt(256) As Byte
        'Dim tryCnt As Integer
        'Dim okPass(0 To 3) As Boolean
        'tryCnt = 7

        'ok = False

        'While Not ok And tryCnt >= 0
        '    tryCnt = tryCnt - 1
        '    okPass(3) = False
        '    okPass(2) = False
        '    okPass(1) = False
        '    okPass(0) = False
        '    ok = False

        '    For pass = 0 To 3
        '        EraseInputQueue()
        '        Frame(0) = &H12
        '        Frame(1) = NetAddr



        '        MyTransport.CleanPort()
        '        MyTransport.Write(Frame, 0, 2)

        '        WaitForData()

        '        Dim b(4096) As Byte
        '        Dim cnt As Integer
        '        cnt = MyTransport.BytesToRead
        '        If cnt > 0 Then
        '            Dim ptr As Integer
        '            Dim sz As Integer
        '            ptr = 0
        '            sz = 0
        '            While cnt > 0
        '                MyTransport.Read(b, ptr, cnt)
        '                ptr += cnt
        '                sz += cnt


        '                If VerifySumm(b, 0, sz) Then
        '                    For i As Integer = 0 To &H40 - 1
        '                        SysInt(pass * &H40 + i) = b(6 + i)
        '                    Next
        '                    okPass(pass) = True
        '                End If

        '                RaiseIdle()
        '                Thread.Sleep(CalcInterval(10))
        '                cnt = MyTransport.BytesToRead
        '            End While


        '        End If
        '    Next
        '    If okPass(0) And okPass(1) And okPass(2) And okPass(3) Then
        '        ok = True
        '    End If
        'End While

        'dr = dt.NewRow
        'dr("Название") = "Число систем"
        'dr("Значение") = SysInt(&H0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "тип датчиков расхода"
        'dr("Значение") = SysInt(&H1)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "тип единиц энергии"
        'dr("Значение") = SysInt(&H7)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "тип температур в статистике"
        'dr("Значение") = SysInt(&HC)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "номер прибора в сети"
        'dr("Значение") = GetLng(SysInt, &H78, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "заводской номер прибора"
        'dr("Значение") = GetLng(SysInt, &H7C, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Диаметр условного прохода по каналам"
        'dr("Значение") = GetInt(SysInt, &HC4, 0, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Максимальное значение расхода по системам(Gmax1)*"
        'dr("Значение") = GetFlt(SysInt, &HCC, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Установленное значение Gуmax в процентах от (*)"
        'dr("Значение") = SysInt(&HDC)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Установленное значение Gуmin в процентах от (*)"
        'dr("Значение") = SysInt(&HE0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Максимальная частота"
        'dr("Значение") = GetFlt(SysInt, &HE4, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Вес импульса"
        'dr("Значение") = GetFlt(SysInt, &H74, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Адрес следующей часовой записи"
        'dr("Значение") = GetLng(SysInt, &HF4, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "Адрес следующей суточной записи"
        'dr("Значение") = GetLng(SysInt, &HF8, 0)
        'dt.Rows.Add(dr)

        'dr = dt.NewRow
        'dr("Название") = "ВАдрес следующей записи на отчетную дату"
        'dr("Значение") = GetLng(SysInt, &HFC, 0)
        'dt.Rows.Add(dr)

        Return dt
    End Function



    Public Function bufcheck() As String
        Return ""
    End Function


    Private Function Byte2Float32(ByVal Buf() As Byte, ByVal offset As Integer, ByVal Revers As Boolean) As Single
        Dim floatStr(3) As Byte
        Dim E As Short
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Double
        Dim i As Long

        For i = 0 To 3
            floatStr(i) = Buf(i + offset)
        Next i

        'If Revers Then
        'Array.Reverse(floatStr)
        'End If

        If floatStr(0) = 0 And floatStr(1) = 0 And floatStr(2) = 0 And floatStr(3) = 0 Then
            Return 0.0
        End If




        '================ Float число========================
        'ст.байт                                 младший байт
        '====================================================
        'двоич.порядок |ст.байт                  младший байт
        '----------------------------------------------------
        ' xxxx xxxx     | sxxx xxxx | xxxx xxxx | xxxx xxxx |

        ' A = (-1)^s * f * 2^(e-127)
        ' f= сумма от 0 до 23 a(k)*2^(-k), где a(k) бит мантисы с номером k


        E = floatStr(0)

        If E <> 0 Then
            E = E - 2
        End If
        
        If floatStr(1) And (2 ^ 7) Then
            s = 1
        Else
            s = 0
        End If

        Mantissa = (((floatStr(1) And &H7F) << 16) _
                     + (floatStr(2) << 8) _
                     + floatStr(3))


        f = 2 ^ 0
        For i = 22 To 0 Step -1
            If Mantissa And 2& ^ i Then
                f = f + 2 ^ (i - 23)
            End If
        Next i
        Return (-1) ^ s * f * 2.0! ^ (E - 127)


    End Function







    'Private Function Byte2Float24(ByVal Buf() As Byte, ByVal offset As Integer, ByVal Revers As Boolean) As Single
    '    Dim floatStr(3) As Byte
    '    Dim E As Short
    '    Dim Mantissa As Long
    '    Dim s As Long
    '    Dim f As Double
    '    Dim i As Long

    '    For i = 0 To 3
    '        floatStr(i) = Buf(i + offset)
    '    Next i

    '    'If Revers Then
    '    '    Array.Reverse(floatStr)
    '    'End If

    '    If floatStr(0) = 0 And floatStr(1) = 0 And floatStr(2) = 0 And floatStr(3) = 0 Then
    '        Return 0.0
    '    End If




    '    '================ Float число========================
    '    'ст.байт                                 младший байт
    '    '====================================================
    '    'двоич.порядок |ст.байт                  младший байт
    '    '----------------------------------------------------
    '    ' xxxx xxxx     | sxxx xxxx | xxxx xxxx | xxxx xxxx |

    '    ' A = (-1)^s * f * 2^(e-127)
    '    ' f= сумма от 0 до 23 a(k)*2^(-k), где a(k) бит мантисы с номером k


    '    E = floatStr(0)

    '    If E <> 0 Then
    '        E = E - 2
    '    End If

    '    If floatStr(1) And (2 ^ 7) Then
    '        s = 1
    '    Else
    '        s = 0
    '    End If

    '    Mantissa = (((floatStr(1) And &H7F) << 8) _
    '                 + (floatStr(2)))


    '    f = 2 ^ 0
    '    For i = 14 To 0 Step -1
    '        If Mantissa And 2& ^ i Then
    '            f = f + 2 ^ (i - 15)
    '        End If
    '    Next i
    '    Return (-1) ^ s * f * 2.0! ^ (E - 127)
    'End Function

    Public Overrides Property isMArchToDBWrite() As Boolean
        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property


    Private Function Bytes2Single(ByVal hexValue() As Byte, ByVal index As Int16, ByVal Revers As Boolean) As Single

        Try

            Dim i As Integer = 0
            Dim bArray(0 To 3) As Byte

            For i = 0 To 3
                bArray(i) = hexValue(index + i)
            Next
            If Revers Then
                Array.Reverse(bArray)
            End If

            Return BitConverter.ToSingle(bArray, 0)

        Catch ex As Exception
            Return 0.0
        End Try
    End Function



    Private Function MyBytes2Single(ByVal hexValue() As Byte, ByVal index As Int16, ByVal Revers As Boolean) As Single

        Try

            Dim i As Integer = 0
            Dim bArray(0 To 3) As Byte

            For i = 0 To 3
                bArray(i) = hexValue(index + i)
            Next
            If Revers Then
                Array.Reverse(bArray)
            End If


            Dim bArray2(0 To 3) As Byte

            bArray2(0) = bArray(3)
            bArray2(1) = bArray(2)


            If bArray(0) > 0 Then
                '   bArray(0) += 2
            End If
            bArray2(2) = (bArray(1) And &H7F) + ((bArray(0) And &H1) << 7)
            bArray2(3) = ((bArray(0) >> 1) And &H7F) + (bArray(1) And &H80)



            Return BitConverter.ToSingle(bArray2, 0)

        Catch ex As Exception
            Return 0.0
        End Try
    End Function




    Private Function GetDeviceDate() As Date
        Return Date.Now
    End Function



    Public Sub New()

        HCash = New Dictionary(Of String, CashItem)
        DCash = New Dictionary(Of String, CashItem)
        lastD = GetDeviceDate()
        lastH = GetDeviceDate()
        
    End Sub
 
End Class
