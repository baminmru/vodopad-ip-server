
Imports System
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


    Public t1 As Single
    Public t2 As Single
    Public t3 As Single
    Public t4 As Single
    Public t5 As Single
    Public t6 As Single

    Public archType As Short
End Structure





Public Class driver
    Inherits STKTVMain.TVDriver

    Private NetAddr As Byte = 1

    Public Const c_lng256 As Long = 256&

    Private mIsConnected As Boolean

    Private isTCP As Boolean
    Private SleepTime As Long
    Private Cash(0 To 2048) As CashItem

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
        Return "TEM104"
    End Function



    Private Function DevInit() As Boolean


        Dim Frame(15) As Byte
        Dim ch As UInt16
        Dim tryCnt As Integer
        Dim ok As Boolean
        tryCnt = 7
        ok = False
        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            EraseInputQueue()
            Frame(0) = &H55
            Frame(1) = &H0
            Frame(2) = &HFF
            Frame(3) = &H0
            Frame(4) = &H0
            Frame(5) = &H0
            Frame(6) = &HAB

            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 7)

     

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


                    If VerifySumm(b, 0, sz) Then
                        ok = True
                        If b(0) = &HAA Then
                            If b(6) = Asc("T") And b(7) = Asc("E") And b(8) = Asc("M") And b(9) = Asc("-") And b(10) = Asc("1") And b(11) = Asc("0") And b(12) = Asc("4") Then
                                NetAddr = b(1)
                                Return True
                            End If
                        End If

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

                RaiseIdle()
                Thread.Sleep(CalcInterval(10))



            End If


        Catch exc As Exception
            Return
        End Try



    End Sub

    Private m_readRAMByteCount As Short

    Private Function BCD(ByVal B As Byte) As Byte
        Dim i As UInteger
        Dim o As UInteger
        i = B
        o = (i Mod 10) + (i \ 10) * 16
        Return o And &HFF
    End Function

    Private CurHourNumber As Long
    Private CurDayNumber As Long

    Private Function GetCurRowNumber() As Boolean
        Dim ok As Boolean
        Dim Frame(10) As Byte
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim AddrBuf(8) As Byte
        Dim tryCnt As Integer
        tryCnt = 5
        CurHourNumber = 1535
        CurDayNumber = 1903

        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            EraseInputQueue()
            Frame(0) = &H55
            Frame(1) = NetAddr
            Frame(2) = Not NetAddr
            Frame(3) = &HF
            Frame(4) = &H1
            Frame(5) = &H3
            addr = &HF4
            addr = addr + &H40 * (pass)
            Frame(6) = (addr And &HFF00) >> 8
            Frame(7) = addr And &HFF
            Frame(8) = &H8
            ch = CheckSum(Frame, 0, 9)
            Frame(9) = ch


            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 10)

            WaitForData()


            Dim b(4096) As Byte
            Dim cnt As Integer, t As Integer

            t = 0
            cnt = MyTransport.BytesToRead
            If cnt > 0 Then
                Dim ptr As Integer
                Dim sz As Integer
                ptr = 0
                sz = 0
                While cnt > 0 And t < 20
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt
                    t += 1

                    If VerifySumm(b, 0, sz) Then
                        For i As Integer = 0 To 7
                            AddrBuf(i) = b(6 + i)
                        Next
                        ok = True
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(10))
                    cnt = MyTransport.BytesToRead
                End While


            End If
        End While
        If ok Then
            CurHourNumber = GetLng(AddrBuf, 0, 0)
            CurDayNumber = GetLng(AddrBuf, 4, 0)
            CurHourNumber = CurHourNumber - &H200000
            CurDayNumber = CurDayNumber - &H200000
            CurHourNumber = CurHourNumber / 256
            CurDayNumber = CurDayNumber / 256


        End If
        Return ok
    End Function

    Private Function ReadArchRecord(ByVal ArchType As Short, ByVal ArchDate As Date) As Byte()


        Dim ArchYear As Short
        Dim ArchMonth As Short
        Dim ArchDay As Short
        Dim ArchHour As Short
        Dim ok As Boolean = False
        Dim AErr As String = ""
        Dim Frame(16) As Byte

        ArchYear = ArchDate.Year
        ArchMonth = ArchDate.Month
        ArchDay = ArchDate.Day
        ArchHour = ArchDate.Hour

        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt() As Byte

        Dim b(4096) As Byte
        Dim cnt As Integer

        ' Dim t As Integer
        Dim maxrec As Integer
        Dim memAddr As Long
        Dim dt2 As Date
        Dim devdate As Date
        Dim checkdate As Date

        Dim tryCnt As Integer
        Dim okPass(0 To 3) As Boolean
        tryCnt = 7

        ok = False

        ReDim SysInt(0 To 255)


        Try

            EraseInputQueue()

            devdate = GetDeviceDate()
            If ArchType = archType_hour Then

                checkdate = devdate.AddHours(-1)
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                maxrec = 1536

            Else
                checkdate = devdate.AddDays(-1)
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                maxrec = 368

            End If


            If dt2 <= checkdate Then




                ' seek for record
                Dim i_seek As Integer
                Dim idx As Integer
                Dim found As Boolean

                found = False
                GetCurRowNumber()
                If ArchType = archType_hour Then
                    idx = CurHourNumber
                End If

                If ArchType = archType_day Then
                    idx = CurDayNumber
                End If

                For i_seek = 0 To maxrec - 1

                    If ArchType = archType_hour Then
                        idx = idx - 1
                        If idx < 0 Then idx = 1536
                        memAddr = idx * 256
                    End If

                    If ArchType = archType_day Then
                        idx = idx - 1
                        If idx < 1536 Then idx = 1903
                        memAddr = idx * 256
                    End If


                    If Not Cash(idx).ok Then

                        tryCnt = 7
                        ok = False
                        'memAddr = memAddr + &H40
                        While Not ok And tryCnt >= 0
                            tryCnt = tryCnt - 1
                            EraseInputQueue()

                            Frame(0) = &H55
                            Frame(1) = NetAddr
                            Frame(2) = Not NetAddr
                            Frame(3) = &HF
                            Frame(4) = &H3
                            Frame(5) = &H5


                            Frame(6) = &H8

                            Frame(7) = (memAddr And &HFF000000) >> 24
                            Frame(8) = (memAddr And &HFF0000) >> 16
                            Frame(9) = (memAddr And &HFF00) >> 8
                            Frame(10) = memAddr And &HFF

                            ch = CheckSum(Frame, 0, 11)
                            Frame(11) = ch
                            MyTransport.CleanPort()
                            MyTransport.Write(Frame, 0, 12)


                            WaitForData()



                            cnt = MyTransport.BytesToRead
                            If cnt > 0 Then
                                Dim ptr As Integer
                                Dim sz As Integer
                                ptr = 0
                                sz = 0
                                While Not ok And cnt > 0
                                    MyTransport.Read(b, ptr, cnt)
                                    ptr += cnt
                                    sz += cnt

                                    If VerifySumm(b, 0, sz) Then
                                        ok = True
                                        For i As Integer = 0 To sz - 6
                                            SysInt(i) = b(6 + i)
                                        Next
                                        Cash(idx).ok = True
                                        Cash(idx).RecDate(0) = SysInt(0)
                                        Cash(idx).RecDate(1) = SysInt(1)
                                        Cash(idx).RecDate(2) = SysInt(2)
                                        Cash(idx).RecDate(3) = SysInt(3)

                                        If ArchType = archType_hour Then

                                            If SysInt(0) = BCD(dt2.Hour) And _
                                                SysInt(1) = BCD(dt2.Day) And _
                                                SysInt(2) = BCD(dt2.Month) And _
                                                SysInt(3) = BCD(dt2.Year - 2000) Then
                                                found = True
                                            End If

                                        End If
                                        If ArchType = archType_day Then

                                            If SysInt(1) = BCD(dt2.Day) And _
                                                 SysInt(2) = BCD(dt2.Month) And _
                                                 SysInt(3) = BCD(dt2.Year - 2000) Then
                                                found = True
                                            End If

                                        End If

                                    End If

                                    RaiseIdle()
                                    Thread.Sleep(CalcInterval(10))
                                    cnt = MyTransport.BytesToRead
                                End While

                            End If
                        End While
                        If found Then GoTo getInfo

                    Else
                        If ArchType = archType_hour Then

                            If Cash(idx).RecDate(0) = BCD(dt2.Hour) And _
                                Cash(idx).RecDate(1) = BCD(dt2.Day) And _
                                Cash(idx).RecDate(2) = BCD(dt2.Month) And _
                                Cash(idx).RecDate(3) = BCD(dt2.Year - 2000) Then
                                ok = True

                                GoTo getInfo
                            End If

                        End If
                        If ArchType = archType_day Then
                            If Cash(idx).RecDate(1) = BCD(dt2.Day) And _
                             Cash(idx).RecDate(2) = BCD(dt2.Month) And _
                             Cash(idx).RecDate(3) = BCD(dt2.Year - 2000) Then
                                ok = True
                                GoTo getInfo
                            End If

                        End If
                    End If
                Next
                ok = False

getInfo:


                If ok Then

                    ok = False
                    Dim tryPassCnt As Integer
                    Dim blockAddr As Long

                    tryCnt = 7
                    While Not ok And tryCnt >= 0
                        tryCnt = tryCnt - 1


                        okPass(3) = False
                        okPass(2) = False
                        okPass(1) = False
                        okPass(0) = False

                        For pass = 0 To 3
                            okPass(pass) = False
                            tryPassCnt = 7
                            blockAddr = memAddr + pass * &H40
                            While Not okPass(pass) And tryPassCnt >= 0
                                tryPassCnt = tryPassCnt - 1
                                EraseInputQueue()

                                Frame(0) = &H55
                                Frame(1) = NetAddr
                                Frame(2) = Not NetAddr
                                Frame(3) = &HF
                                Frame(4) = &H3
                                Frame(5) = &H5


                                Frame(6) = &H40

                                Frame(7) = (blockAddr And &HFF000000) >> 24
                                Frame(8) = (blockAddr And &HFF0000) >> 16
                                Frame(9) = (blockAddr And &HFF00) >> 8
                                Frame(10) = blockAddr And &HFF

                                ch = CheckSum(Frame, 0, 11)
                                Frame(11) = ch
                                MyTransport.CleanPort()
                                MyTransport.Write(Frame, 0, 12)

                                WaitForData()



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


                                        If VerifySumm(b, 0, sz) Then
                                            okPass(pass) = True

                                            For i As Integer = 0 To &H40 - 1
                                                SysInt(pass * &H40 + i) = b(6 + i)
                                            Next
                                        End If

                                        RaiseIdle()
                                        Thread.Sleep(CalcInterval(10))
                                        cnt = MyTransport.BytesToRead
                                    End While


                                End If

                            End While
                        Next
                        If okPass(0) And okPass(1) And okPass(2) And okPass(3) Then
                            ok = True
                        End If
                        If ok Then
                            ok = VerifySysInt(SysInt)
                            Return SysInt

                        End If

                    End While
                End If
            End If
        Catch
            Return Nothing
        End Try

        Return Nothing
    End Function

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim retsum As String
        Dim ok As Boolean = False
        Dim AErr As String = ""

        Dim SI() As Byte
        Dim SIP() As Byte
        Dim dt As Date
        Dim dt2 As Date
        cleararchive(Arch)

        Arch.archType = ArchType

        If ArchType = archType_hour Then
            dt = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            dt2 = dt2.AddHours(-1)
        Else
            dt = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            dt2 = dt2.AddDays(-1)
        End If

        Arch.DateArch = dt
        Try

            SI = ReadArchRecord(ArchType, dt)
            SIP = ReadArchRecord(ArchType, dt2)
            If Not SI Is Nothing And Not SIP Is Nothing Then
                ok = True
                ProcessA(Arch, SI, SIP)
            End If

            If ok Then
                retsum = "Архив прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = True
            Else

                retsum = "Ошибка: не удалось получить часть параметров " & dt2.ToString()

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

    Public Function ProcessRcvData(ByVal buf() As Byte, ByVal ret As Short) As String


        Return "Ошибка!Пакет не распознан"
    End Function
    Public Function GetAndProcessData() As String

        Return ""
    End Function
    Public Function DeCodeHCNumber(ByVal CodeHC As Long) As String

        DeCodeHCNumber = CodeHC.ToString()




    End Function
    Public Function DeCodeHCText(ByVal CodeHC As Long) As String

        DeCodeHCText = ""
        If CodeHC And 1 Then DeCodeHCText = DeCodeHCText & "G1 < min;"
        If CodeHC And 2 Then DeCodeHCText = DeCodeHCText & "G2 < min;"
        If CodeHC And 4 Then DeCodeHCText = DeCodeHCText & "G3 < min;"
        If CodeHC And 8 Then DeCodeHCText = DeCodeHCText & "G1 > max;"
        If CodeHC And 16 Then DeCodeHCText = DeCodeHCText & "G2 > max;"
        If CodeHC And 32 Then DeCodeHCText = DeCodeHCText & "G3 > max;"
        If CodeHC And 64 Then DeCodeHCText = DeCodeHCText & "dt1 < min;"
        If CodeHC And 128 Then DeCodeHCText = DeCodeHCText & "dt2 < min;"


    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String
        DeCodeHC = ""
        If CodeHC And 1 Then DeCodeHC = DeCodeHC & "G1 < min;"
        If CodeHC And 2 Then DeCodeHC = DeCodeHC & "G2 < min;"
        If CodeHC And 4 Then DeCodeHC = DeCodeHC & "G3 < min;"
        If CodeHC And 8 Then DeCodeHC = DeCodeHC & "G1 > max;"
        If CodeHC And 16 Then DeCodeHC = DeCodeHC & "G2 > max;"
        If CodeHC And 32 Then DeCodeHC = DeCodeHC & "G3 > max;"
        If CodeHC And 64 Then DeCodeHC = DeCodeHC & "dt1 < min;"
        If CodeHC And 128 Then DeCodeHC = DeCodeHC & "dt2 < min;"

    End Function


    Public Overrides Function WriteArchToDB() As String

        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,dm12,V1H,V2H,V5H,V4H,q1h,q2h,sp_TB1,sp_TB2,q1,q2,q4,q5,TSUM1,TSUM2,hc_code,hc, oktime,errtime,hcraw,hcraw1,hcraw2) values ("
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




        WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCNumber(Arch.HCtv1)) + "','" + S180(DeCodeHCNumber(Arch.HCtv2)) + "'"

        WriteArchToDB = WriteArchToDB + "," + Format((Arch.oktime1 / 60), "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + Format(Arch.errtime1 / 60, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + "'" + Arch.HC.ToString().Replace(",", ".") + "'" + ","
        WriteArchToDB = WriteArchToDB + "'" + Arch.HCtv1.ToString().Replace(",", ".") + "'" + ","
        WriteArchToDB = WriteArchToDB + "'" + Arch.HCtv2.ToString().Replace(",", ".") + "'"
        WriteArchToDB = WriteArchToDB + ")"
        Debug.Print(WriteArchToDB)
    End Function

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function
    'Public Overrides Function WriteMArchToDB() As String
    '    WriteMArchToDB = ""
    '    Try
    '        WriteMArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,g1,g2,g3,g4,g5,g6,v1,v2,v3,v4,dt12,dt45,sp_TB1,sp_TB2,tce1,tce2,tair1,tair2,hc_code,hc,hc_1,hc_2) values ("
    '        WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
    '        WriteMArchToDB = WriteMArchToDB + "1,"
    '        WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
    '        WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
    '        WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.t1, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.t2, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.t3, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.t4, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.t5, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.t6, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.p1, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.p2, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.p3, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.p4, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.G1, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.G2, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.G3, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.G4, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.G5, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.G6, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.v1, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.v2, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.v3, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.v4, "##############0.000").Replace(",", ".") + ","

    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.dt12, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.dt45, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + mArch.SPtv1.ToString + ","
    '        WriteMArchToDB = WriteMArchToDB + mArch.SPtv2.ToString + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.tx1, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.tx2, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.tair1, "##############0.000").Replace(",", ".") + ","
    '        WriteMArchToDB = WriteMArchToDB + Format(mArch.tair2, "##############0.000").Replace(",", ".") + ","




    '        WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HC) + "','" + S180(DeCodeHCNumber(mArch.HC) + " " + DeCodeHCText(mArch.HC)) + "',"

    '        WriteMArchToDB = WriteMArchToDB + "'" + S180(DeCodeHCNumber(mArch.HCtv1) + " " + DeCodeHCText(mArch.HCtv1)) + "',"
    '        WriteMArchToDB = WriteMArchToDB + "'" + S180(DeCodeHCNumber(mArch.HCtv2) + " " + DeCodeHCText(mArch.HCtv2)) + "'"



    '        WriteMArchToDB = WriteMArchToDB + ")"
    '    Catch
    '    End Try
    '    'Return WriteMArchToDB
    'End Function

   

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









    'Public Overrides Function ReadMArch() As String

    '    clearMarchive(mArch)

    '    Dim AErr As String = ""

    '    Dim ok As Boolean
    '    Dim okPass(0 To 1) As Boolean


    '    Dim Frame(10) As Byte
    '    Dim addr As UInteger
    '    Dim ch As UInt16
    '    Dim pass As Integer
    '    Dim RAM(256) As Byte
    '    Dim tryCnt As Integer
    '    tryCnt = 7
    '    okPass(0) = False
    '    ok = False
    '    okPass(1) = False
    '    While Not ok And tryCnt >= 0
    '        okPass(0) = False
    '        okPass(1) = False
    '        tryCnt = tryCnt - 1

    '        For pass = 0 To 1
    '            EraseInputQueue()
    '            Frame(0) = &H55
    '            Frame(1) = NetAddr
    '            Frame(2) = Not NetAddr
    '            Frame(3) = &HF
    '            Frame(4) = &H1
    '            Frame(5) = &H3
    '            addr = &H2200
    '            addr = addr + &H40 * (pass)
    '            Frame(6) = (addr And &HFF00) >> 8
    '            Frame(7) = addr And &HFF
    '            Frame(8) = &H40
    '            ch = CheckSum(Frame, 0, 9)
    '            Frame(9) = ch


    '           MyTransport.CleanPort()
    '            MyTransport.Write(Frame, 0, 10)

    '            Dim t As Integer
    '            RaiseIdle()
    'Thread.Sleep(30)
    '            t = 0
    '            While MyTransport.BytesToRead = 0 And t < 20
    '                RaiseIdle()
    'Thread.Sleep(30)
    '                t = t + 1
    '            End While

    '            Dim b(4096) As Byte
    '            Dim cnt As Integer
    '            cnt = MyTransport.BytesToRead
    '            If cnt > 0 Then
    '                Dim ptr As Integer
    '                Dim sz As Integer
    '                ptr = 0
    '                sz = 0
    '                While cnt > 0
    '                    MyTransport.Read(b, ptr, cnt)
    '                    ptr += cnt
    '                    sz += cnt


    '                    If VerifySumm(b, 0, sz) Then
    '                        For i As Integer = 0 To &H40 - 1
    '                            RAM(pass * &H40 + i) = b(6 + i)
    '                        Next
    '                        okPass(pass) = True

    '                    End If

    '                    RaiseIdle()
    'Thread.Sleep(30)
    '                    cnt = MyTransport.BytesToRead
    '                End While


    '            End If
    '        Next
    '        If okPass(0) And okPass(1) Then
    '            ok = True
    '        End If
    '    End While
    '    'Dim test As Single
    '    'Dim bytes() As Byte
    '    'test = 47.105 'Q
    '    'bytes = BitConverter.GetBytes(test)
    '    'test = 546.98 'M
    '    'bytes = BitConverter.GetBytes(test)
    '    'test = 577.46 'V
    '    'bytes = BitConverter.GetBytes(test)

    '    If ok Then

    '        ProcessM(mArch, RAM)
    '        mArch.DateArch = GetDeviceDate()

    '    End If


    '    Dim retsum As String
    '    retsum = "Мгновенный архив прочитан"
    '    If ok And AErr = "" Then
    '        'retsum = "Мгновенный архив прочитан"
    '        retsum = retsum & vbCrLf
    '        EraseInputQueue()
    '        isMArchToDBWrite = True
    '        Return retsum
    '    Else
    '        retsum = "Не удалось получить часть параметров "
    '        retsum = retsum & AErr & vbCrLf
    '        EraseInputQueue()
    '        isMArchToDBWrite = True
    '        Return retsum
    '    End If


    'End Function

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
        Dim pos As Integer
        pos = PosL + 4 * (Chanel)
        l = Bytes2Single(SI, pos, True)
        Return l
    End Function
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


    'Private Sub ProcessM(ByRef marc As MArchive, ByVal SI() As Byte)
    '    Dim f As Single
    '    Dim l As Long

    '    f = GetFlt(SI, &H0, 0)
    '    mArch.t1 = f
    '    f = GetFlt(SI, &H0, 1)
    '    mArch.t2 = f
    '    f = GetFlt(SI, &H0, 2)
    '    mArch.t3 = f
    '    f = GetFlt(SI, &H0, 3)
    '    mArch.t4 = f


    '    f = GetFlt(SI, &H10, 0)
    '    mArch.p1 = f
    '    f = GetFlt(SI, &H10, 1)
    '    mArch.p2 = f
    '    f = GetFlt(SI, &H10, 2)
    '    mArch.p3 = f
    '    f = GetFlt(SI, &H10, 3)
    '    mArch.p4 = f

    '    f = GetFlt(SI, &H50, 0)
    '    mArch.m1 = f
    '    f = GetFlt(SI, &H50, 1)
    '    mArch.m2 = f
    '    f = GetFlt(SI, &H50, 2)
    '    mArch.m3 = f
    '    f = GetFlt(SI, &H50, 3)
    '    mArch.m4 = f

    '    f = GetFlt(SI, &H40, 0)
    '    mArch.v1 = f
    '    f = GetFlt(SI, &H40, 1)
    '    mArch.v2 = f
    '    f = GetFlt(SI, &H40, 2)
    '    mArch.v3 = f
    '    f = GetFlt(SI, &H40, 3)
    '    mArch.v4 = f

    '    f = GetFlt(SI, &H60, 0)
    '    mArch.G1 = f
    '    f = GetFlt(SI, &H60, 1)
    '    mArch.G2 = f
    '    f = GetFlt(SI, &H60, 2)
    '    mArch.G3 = f
    '    f = GetFlt(SI, &H60, 3)
    '    mArch.G4 = f


    '    'Dim eStr As String
    '    'eStr = Chr(SI(&H70)) + Chr(SI(&H71))
    '    'l = (SI(&H72 + 1) << 8) + SI(&H72)


    '    mArch.HC = SI(&H70)



    'End Sub

    Private Sub ProcessT(ByRef tarc As TArchive, ByVal SI() As Byte)
        Dim f As Double
        Dim l As Long
        Dim i As Integer

        i = GetInt(SI, &HC8, 0, 0)
        tarc.t1 = i / 100
        i = GetInt(SI, &HC8, 0, 1)
        tarc.t2 = i / 100
        i = GetInt(SI, &HC8, 0, 2)
        tarc.t3 = i / 100

        i = GetInt(SI, &HC8, 1, 0)
        tarc.t4 = i / 100
        i = GetInt(SI, &HC8, 1, 1)
        tarc.t5 = i / 100
        i = GetInt(SI, &HC8, 1, 2)
        tarc.t6 = i / 100

        i = GetInt(SI, &HC8, 2, 0)
        'tarc.tx1 = i/100
        i = GetInt(SI, &HC8, 2, 1)
        'tarc.tx2 = i/100


        f = GetDbl(SI, &H8, &H38, 0)
        tarc.V1 = f
        f = GetDbl(SI, &H8, &H38, 1)
        tarc.V2 = f
        f = GetDbl(SI, &H8, &H38, 2)
        tarc.V3 = f
        f = GetDbl(SI, &H8, &H38, 3)
        tarc.V4 = f

        f = GetDbl(SI, &H18, &H48, 0)
        tarc.M1 = f
        f = GetDbl(SI, &H18, &H48, 1)
        tarc.M2 = f
        f = GetDbl(SI, &H18, &H48, 2)
        tarc.M3 = f
        f = GetDbl(SI, &H18, &H48, 3)
        tarc.M4 = f

        f = GetDbl(SI, &H28, &H58, 0)
        tarc.Q1 = f
        f = GetDbl(SI, &H28, &H58, 1)
        tarc.Q2 = f
        f = GetDbl(SI, &H28, &H58, 2)
        tarc.Q3 = f
        f = GetDbl(SI, &H28, &H58, 3)
        tarc.Q4 = f
        l = GetLng(SI, &H68, 0)
        tarc.errtime1 = l

        l = GetLng(SI, &H68, 1)
        tarc.errtime2 = l

        l = GetLng(SI, &H6C, 0)
        tarc.oktime1 = l
        l = GetLng(SI, &H6C, 1)
        tarc.oktime2 = l


        i = SI(&HE0)
        tarc.P1 = i / 100
        i = SI(&HE1)
        tarc.P2 = i / 100
        i = SI(&HE2)
        tarc.P3 = i / 100
        i = SI(&HE3)
        tarc.P4 = i / 100
        i = SI(&HE4)
        tarc.P5 = i / 100
        i = SI(&HE5)
        tarc.P6 = i / 100
        tarc.errtime1 -= tarc.oktime1
        tarc.errtime2 -= tarc.oktime2

        tarc.HC = SI(&HBC)


        tarc.errtime2 = tarc.oktime2 - l


    End Sub

    Private Sub ProcessM(ByRef tarc As MArchive, ByVal SI() As Byte)
        Dim f As Double
        Dim l As Long
        Dim i As Integer

        i = GetInt(SI, &HC8, 0, 0)
        tarc.t1 = i / 100
        i = GetInt(SI, &HC8, 0, 1)
        tarc.t2 = i / 100
        i = GetInt(SI, &HC8, 0, 2)
        tarc.t3 = i / 100

        i = GetInt(SI, &HC8, 1, 0)
        tarc.t4 = i / 100
        i = GetInt(SI, &HC8, 1, 1)
        tarc.t5 = i / 100
        i = GetInt(SI, &HC8, 1, 2)
        tarc.t6 = i / 100

        i = GetInt(SI, &HC8, 2, 0)
        'tarc.tx1 = i/100
        i = GetInt(SI, &HC8, 2, 1)
        'tarc.tx2 = i/100


        f = GetDbl(SI, &H8, &H38, 0)
        tarc.V1 = f
        f = GetDbl(SI, &H8, &H38, 1)
        tarc.V2 = f
        f = GetDbl(SI, &H8, &H38, 2)
        tarc.V3 = f
        f = GetDbl(SI, &H8, &H38, 3)
        tarc.V4 = f

        f = GetDbl(SI, &H18, &H48, 0)
        tarc.M1 = f
        f = GetDbl(SI, &H18, &H48, 1)
        tarc.M2 = f
        f = GetDbl(SI, &H18, &H48, 2)
        tarc.M3 = f
        f = GetDbl(SI, &H18, &H48, 3)
        tarc.M4 = f

        f = GetDbl(SI, &H28, &H58, 0)
        tarc.Q1 = f
        f = GetDbl(SI, &H28, &H58, 1)
        tarc.Q2 = f
        f = GetDbl(SI, &H28, &H58, 2)
        tarc.Q3 = f
        f = GetDbl(SI, &H28, &H58, 3)
        tarc.Q4 = f
        l = GetLng(SI, &H68, 0)
        tarc.errtime1 = l

        l = GetLng(SI, &H68, 1)
        tarc.errtime2 = l

        l = GetLng(SI, &H6C, 0)
        tarc.oktime1 = l
        l = GetLng(SI, &H6C, 1)
        tarc.oktime2 = l


        i = SI(&HE0)
        tarc.P1 = i / 100
        i = SI(&HE1)
        tarc.P2 = i / 100
        i = SI(&HE2)
        tarc.P3 = i / 100
        i = SI(&HE3)
        tarc.P4 = i / 100
        i = SI(&HE4)
        tarc.P5 = i / 100
        i = SI(&HE5)
        tarc.P6 = i / 100
        tarc.errtime1 -= tarc.oktime1
        tarc.errtime2 -= tarc.oktime2

        tarc.HC = SI(&HBC)


        tarc.errtime2 = tarc.oktime2 - l


    End Sub

    Private Sub ProcessA(ByRef tarc As Archive, ByVal SI() As Byte, ByVal SIP() As Byte)
        Dim f As Double
        Dim l As Long
        Dim i As Integer

        i = GetInt(SI, &HC8, 0, 0)
        tarc.T1 = i / 100
        i = GetInt(SI, &HC8, 0, 1)
        tarc.T2 = i / 100
        i = GetInt(SI, &HC8, 0, 2)
        tarc.T3 = i / 100

        i = GetInt(SI, &HC8, 1, 0)
        tarc.T4 = i / 100
        i = GetInt(SI, &HC8, 1, 1)
        tarc.T5 = i / 100
        i = GetInt(SI, &HC8, 1, 2)
        tarc.T6 = i / 100

        i = GetInt(SI, &HC8, 2, 0)
        tarc.tx1 = i / 100
        i = GetInt(SI, &HC8, 2, 1)
        tarc.tx2 = i / 100


        f = GetDbl(SI, &H8, &H38, 0)
        tarc.V1 = f
        f = GetDbl(SI, &H8, &H38, 1)
        tarc.V2 = f
        f = GetDbl(SI, &H8, &H38, 2)
        tarc.V3 = f
        f = GetDbl(SI, &H8, &H38, 3)
        tarc.v4 = f


        f = GetDbl(SIP, &H8, &H38, 0)
        tarc.V1 -= f
        f = GetDbl(SIP, &H8, &H38, 1)
        tarc.V2 -= f
        f = GetDbl(SIP, &H8, &H38, 2)
        tarc.V3 -= f
        f = GetDbl(SIP, &H8, &H38, 3)
        tarc.v4 -= f

        f = GetDbl(SI, &H18, &H48, 0)
        tarc.M1 = f
        f = GetDbl(SI, &H18, &H48, 1)
        tarc.M2 = f
        f = GetDbl(SI, &H18, &H48, 2)
        tarc.M3 = f
        f = GetDbl(SI, &H18, &H48, 3)
        tarc.M4 = f

        f = GetDbl(SIP, &H18, &H48, 0)
        tarc.M1 -= f
        f = GetDbl(SIP, &H18, &H48, 1)
        tarc.M2 -= f
        f = GetDbl(SIP, &H18, &H48, 2)
        tarc.M3 -= f
        f = GetDbl(SIP, &H18, &H48, 3)
        tarc.M4 -= f




        i = SI(&HE0)
        tarc.P1 = i / 100
        i = SI(&HE1)
        tarc.P2 = i / 100
        i = SI(&HE2)
        tarc.P3 = i / 100
        i = SI(&HE3)
        tarc.P4 = i / 100

        f = GetDbl(SI, &H28, &H58, 0)
        tarc.Q1 = f
        f = GetDbl(SI, &H28, &H58, 1)
        tarc.Q2 = f
        f = GetDbl(SI, &H28, &H58, 2)
        'tarc.Q3 = f
        'f = GetDbl(SI, &H28, &H58, 3)
        'tarc.Q4 = f

        f = GetDbl(SIP, &H28, &H58, 0)
        tarc.Q1 -= f
        f = GetDbl(SIP, &H28, &H58, 1)
        tarc.Q2 -= f
        f = GetDbl(SIP, &H28, &H58, 2)
        'tarc.Q3 -= f
        'f = GetDbl(SIP, &H28, &H58, 3)
        'tarc.Q4 -= f


        l = GetLng(SI, &H68, 0)
        tarc.errtime1 = l

        l = GetLng(SI, &H68, 1)
        tarc.errtime2 = l


        l = GetLng(SIP, &H68, 0)
        tarc.errtime1 -= l

        l = GetLng(SIP, &H68, 1)
        tarc.errtime2 -= l


        l = GetLng(SI, &H6C, 0)
        tarc.oktime1 = l

        l = GetLng(SI, &H6C, 1)
        tarc.oktime2 = l

        l = GetLng(SIP, &H6C, 0)
        tarc.oktime1 -= l

        l = GetLng(SIP, &H6C, 1)
        tarc.oktime2 -= l

        tarc.errtime1 -= tarc.oktime1
        tarc.errtime2 -= tarc.oktime2

        tarc.HC = SI(&HBC)
        tarc.HCtv1 = SI(&HBC)

        tarc.HCtv2 = SI(&HBC + 1)


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

    Public Function ReadFlashSync(ByVal fistpage As Integer, ByVal ReadPageCount As Integer) As String
        Return ""
    End Function

    Public Function ReadRAMSync(ByVal fistbyte As Integer, ByVal byteCount As Integer) As String
        Return ""
    End Function

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
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt(256) As Byte
        Dim tryCnt As Integer
        Dim okPass(0 To 3) As Boolean
        tryCnt = 7

        ok = False

        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            okPass(3) = False
            okPass(2) = False
            okPass(1) = False
            okPass(0) = False
            ok = False

            For pass = 0 To 3
                EraseInputQueue()
                Frame(0) = &H55
                Frame(1) = NetAddr
                Frame(2) = Not NetAddr
                Frame(3) = &HF
                Frame(4) = &H1
                Frame(5) = &H3
                addr = &H200
                addr = addr + &H40 * (pass)
                Frame(6) = (addr And &HFF00) >> 8
                Frame(7) = addr And &HFF
                Frame(8) = &H40
                ch = CheckSum(Frame, 0, 9)
                Frame(9) = ch


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


                        If VerifySumm(b, 0, sz) Then
                            For i As Integer = 0 To &H40 - 1
                                SysInt(pass * &H40 + i) = b(6 + i)
                            Next
                            okPass(pass) = True
                        End If

                        RaiseIdle()
                        Thread.Sleep(CalcInterval(10))
                        cnt = MyTransport.BytesToRead
                    End While


                End If
            Next
            If okPass(0) And okPass(1) And okPass(2) And okPass(3) Then
                ok = True
            End If
        End While

        If ok Then
            ok = VerifySysInt(SysInt)
            If ok Then
                ProcessT(tArch, SysInt)
                tArch.DateArch = GetDeviceDate()
            End If

        End If


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
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt(256) As Byte
        Dim tryCnt As Integer
        Dim okPass(0 To 3) As Boolean
        tryCnt = 7

        ok = False

        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            okPass(3) = False
            okPass(2) = False
            okPass(1) = False
            okPass(0) = False
            ok = False

            For pass = 0 To 3
                EraseInputQueue()
                Frame(0) = &H55
                Frame(1) = NetAddr
                Frame(2) = Not NetAddr
                Frame(3) = &HF
                Frame(4) = &H1
                Frame(5) = &H3
                addr = &H200
                addr = addr + &H40 * (pass)
                Frame(6) = (addr And &HFF00) >> 8
                Frame(7) = addr And &HFF
                Frame(8) = &H40
                ch = CheckSum(Frame, 0, 9)
                Frame(9) = ch


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


                        If VerifySumm(b, 0, sz) Then
                            For i As Integer = 0 To &H40 - 1
                                SysInt(pass * &H40 + i) = b(6 + i)
                            Next
                            okPass(pass) = True
                        End If

                        RaiseIdle()
                        Thread.Sleep(CalcInterval(10))
                        cnt = MyTransport.BytesToRead
                    End While


                End If
            Next
            If okPass(0) And okPass(1) And okPass(2) And okPass(3) Then
                ok = True
            End If
        End While

        If ok Then
            ok = VerifySysInt(SysInt)
            If ok Then
                ProcessM(mArch, SysInt)
                mArch.DateArch = GetDeviceDate()
            End If

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
        WriteTArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1H,Q2H,Q4,Q5,M1,M2,M3,M4,M5,M6,v1h,v2h,v3,v4h,v5h,v6,p1,p2,p3,p4,p5,p6,t1,t2,t3,t4,t5,t6,TSUM1,TSUM2,worktime,ERRTIME) values ("
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
        WriteTArchToDB = WriteTArchToDB + Format(tArch.oktime1 / 60, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.errtime1 / 60, "##############0").Replace(",", ".")
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
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow

        Dim AErr As String = ""
        clearTarchive(tArch)
        EraseInputQueue()

        Dim ok As Boolean
        Dim Frame(10) As Byte
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt(256) As Byte
        Dim tryCnt As Integer
        Dim okPass(0 To 3) As Boolean
        tryCnt = 7

        ok = False

        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            okPass(3) = False
            okPass(2) = False
            okPass(1) = False
            okPass(0) = False
            ok = False

            For pass = 0 To 3
                EraseInputQueue()
                Frame(0) = &H55
                Frame(1) = NetAddr
                Frame(2) = Not NetAddr
                Frame(3) = &HF
                Frame(4) = &H1
                Frame(5) = &H3
                addr = &H0
                addr = addr + &H40 * (pass)
                Frame(6) = (addr And &HFF00) >> 8
                Frame(7) = addr And &HFF
                Frame(8) = &H40
                ch = CheckSum(Frame, 0, 9)
                Frame(9) = ch


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


                        If VerifySumm(b, 0, sz) Then
                            For i As Integer = 0 To &H40 - 1
                                SysInt(pass * &H40 + i) = b(6 + i)
                            Next
                            okPass(pass) = True
                        End If

                        RaiseIdle()
                        Thread.Sleep(CalcInterval(10))
                        cnt = MyTransport.BytesToRead
                    End While


                End If
            Next
            If okPass(0) And okPass(1) And okPass(2) And okPass(3) Then
                ok = True
            End If
        End While

        dr = dt.NewRow
        dr("Название") = "Число систем"
        dr("Значение") = SysInt(&H0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "тип датчиков расхода"
        dr("Значение") = SysInt(&H1)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "тип единиц энергии"
        dr("Значение") = SysInt(&H7)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "тип температур в статистике"
        dr("Значение") = SysInt(&HC)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "номер прибора в сети"
        dr("Значение") = GetLng(SysInt, &H78, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "заводской номер прибора"
        dr("Значение") = GetLng(SysInt, &H7C, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Диаметр условного прохода по каналам"
        dr("Значение") = GetInt(SysInt, &HC4, 0, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Максимальное значение расхода по системам(Gmax1)*"
        dr("Значение") = GetFlt(SysInt, &HCC, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Установленное значение Gуmax в процентах от (*)"
        dr("Значение") = SysInt(&HDC)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Установленное значение Gуmin в процентах от (*)"
        dr("Значение") = SysInt(&HE0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Максимальная частота"
        dr("Значение") = GetFlt(SysInt, &HE4, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Вес импульса"
        dr("Значение") = GetFlt(SysInt, &H74, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Адрес следующей часовой записи"
        dr("Значение") = GetLng(SysInt, &HF4, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Адрес следующей суточной записи"
        dr("Значение") = GetLng(SysInt, &HF8, 0)
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "ВАдрес следующей записи на отчетную дату"
        dr("Значение") = GetLng(SysInt, &HFC, 0)
        dt.Rows.Add(dr)

        Return dt
    End Function



    Public Function bufcheck() As String
        Return ""
    End Function


    Private Function Byte2Float(ByVal Buf() As Byte, ByVal offset As Integer, ByVal Revers As Boolean) As Single
        Dim floatStr(3) As Byte
        Dim E As Long
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Single
        Dim i As Long
        'If floatStr = "" Then Exit Function
        'If floatStr.Length <> 4 Then Exit Function
        ' If floatStr = String(4, 0) Then Exit Function
        For i = 0 To 3
            floatStr(i) = Buf(i + offset)
        Next i
        If Revers Then
            Array.Reverse(floatStr)
        End If
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
        If floatStr(1) And (2 ^ 7) Then
            s = 1
        Else
            s = 0
        End If
        Mantissa = ((floatStr(1) And &H7F) << 16) _
                     + (floatStr(2) << 8) _
                     + (floatStr(3))

        'Mantissa = (Asc(Mid(floatStr, 2, 1)) And &H7F) * (2 ^ 16) _
        '                     + Asc(Mid(floatStr, 3, 1)) * (2 ^ 8) _
        '                     + Asc(Mid(floatStr, 4, 1))

        f = 2 ^ 0
        For i = 22 To 0 Step -1
            If Mantissa And 2& ^ i Then
                f = f + 2 ^ (i - 23)
            End If
        Next i
        Return (-1) ^ s * f * 2.0! ^ (E - 127)
    End Function




    Public Overrides Property isMArchToDBWrite() As Boolean
        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property


    Public Function VerifySumm(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As Boolean
        Dim ch As Byte
        Dim i As Integer
        If sz < 2 Then Return False
        For i = offset To offset + sz - 1
            If Data(i) <> 0 Then
                GoTo cont
            End If
        Next
        Return False
cont:

        ch = CheckSum(Data, offset, sz - 1)
        If Data(sz - 1) = ch Then
            Return True
        End If
        Return False
    End Function



    Private Function CheckSum(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As Byte
        Dim s As Byte
        Dim sl As ULong
        sl = &H0
        On Error Resume Next
        For i As Integer = 0 To sz - 1

            sl = (sl + Data(offset + i)) And &HFF

        Next
        s = (Not (sl And 255)) And 255
        Return s
    End Function





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





    'Private Function SetDot(ByVal S As String, ByVal dig As Integer) As String
    '    Dim oo As String
    '    If dig > 0 Then
    '        oo = S
    '        While Len(oo) < dig + 1
    '            oo = "0" + oo
    '        End While

    '        Return oo.Substring(0, oo.Length - dig) + "." + oo.Substring(oo.Length - dig, dig)
    '    Else
    '        Return S
    '    End If
    'End Function


    'Private Function ReadData(Optional ByVal ElemType As Integer = -1) As Byte()

    '    Dim Frame(10) As Byte
    '    Dim ch As UInt16
    '    Frame(0) = &HFF
    '    Frame(1) = &HFF
    '    Frame(2) = &H0
    '    Frame(3) = &H3
    '    Frame(4) = &H3F
    '    Frame(5) = &HFE
    '    Frame(6) = &H0
    '    Frame(7) = &H0
    '    ch = CheckSum(Frame, 2, 6)
    '    Frame(8) = ch Mod 256
    '    Frame(9) = ch \ 256

    '    MyTransport.Write(Frame, 0, 10)

    '    Dim t As Integer
    '    Dim i As Integer
    '    RaiseIdle()
    'Thread.Sleep(30)
    '    t = 0
    '    While MyTransport.BytesToRead = 0 And t < 20
    '        RaiseIdle()
    'Thread.Sleep(30)
    '        t = t + 1
    '    End While

    '    Dim b() As Byte
    '    ReDim b(4096)
    '    Dim cnt As Integer
    '    cnt = MyTransport.BytesToRead
    '    If cnt > 0 Then
    '        Dim ptr As Integer
    '        Dim sz As Integer
    '        ptr = 0
    '        sz = 0
    '        While cnt > 0
    '            MyTransport.Read(b, ptr, cnt)
    '            ptr += cnt
    '            sz += cnt


    '            If VerifySumm(b, 0, sz) Then
    '                Dim sout As String = ""
    '                Dim cout As String = ""
    '                For i = 0 To cnt - 1
    '                    sout = sout + " " + b(i).ToString()


    '                Next


    '                If b(2) = 3 Then
    '                    Dim q As Integer
    '                    q = b(3)

    '                End If

    '                Dim digs As Integer
    '                digs = 0
    '                If ElemType >= 0 Then
    '                    digs = PropVal(DigMap(ElemType))
    '                End If
    '                If b(2) = 4 Then
    '                    Dim r As Integer
    '                    r = b(4) * 256 + b(3)

    '                End If

    '                If b(2) = 6 Then
    '                    Dim z As Long
    '                    z = b(6) * 256 * 65536 + b(5) * 65536 + b(4) * 256 + b(3)

    '                End If

    '            End If

    '            RaiseIdle()
    'Thread.Sleep(30)
    '            cnt = MyTransport.BytesToRead
    '        End While
    '        Return b
    '    Else

    '        Return b
    '    End If

    'End Function


    Private Function GetDeviceDate() As Date
        EraseInputQueue()

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Dim tryCnt As Integer
        Dim ok As Boolean
        tryCnt = 7

        ok = False
        While Not ok And tryCnt >= 0
            tryCnt = tryCnt - 1
            EraseInputQueue()

            Frame(0) = &H55
            Frame(1) = NetAddr
            Frame(2) = Not NetAddr
            Frame(3) = &HF
            Frame(4) = &H2
            Frame(5) = &H2
            Frame(6) = &H10
            Frame(7) = &H10
            ch = CheckSum(Frame, 0, 8)
            Frame(8) = ch




            MyTransport.Write(Frame, 0, 9)

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


                    If VerifySumm(b, 0, sz) Then
                        ok = True
                        Dim d As DateTime
                        Dim ss As Integer
                        ss = ((b(6) And &HF0) >> 4) * 10 + (b(6) And &HF)
                        Dim mm As Integer
                        mm = ((b(8) And &HF0) >> 4) * 10 + (b(8) And &HF)
                        Dim hh As Integer
                        hh = ((b(10) And &HF0) >> 4) * 10 + (b(10) And &HF)
                        Dim dd As Integer
                        dd = ((b(13) And &HF0) >> 4) * 10 + (b(13) And &HF)

                        Dim mn As Integer
                        mn = ((b(14) And &HF0) >> 4) * 10 + (b(14) And &HF)
                        Dim yy As Integer
                        yy = ((b(15) And &HF0) >> 4) * 10 + (b(15) And &HF)


                        d = New DateTime(2000 + yy, mn, dd, hh, mm, ss)
                        Return d
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(10))
                    cnt = MyTransport.BytesToRead
                End While


            End If
        End While

        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return Date.Now

    End Function



    Public Sub New()
        For i As Integer = 0 To 2048
            Cash(i) = New CashItem
            Cash(i).ok = False
        Next
    End Sub
 
End Class
