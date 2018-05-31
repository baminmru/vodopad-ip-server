
Imports System
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading


Public Class driver
    Inherits STKTVMain.TVDriver

    Private NetAddr As Byte = 1

    Public Const c_lng256 As Long = 256&

    Private mIsConnected As Boolean

    Private isTCP As Boolean
    Private SleepTime As Long
    Private Cash(0 To 2048) As CashItem


    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

    Dim tv As Short


    Dim ActiveCount As Integer




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
            Frame(1) = NetAddr
            Frame(2) = Not NetAddr
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
                            If b(6) = Asc("T") And b(7) = Asc("E") And b(8) = Asc("M") And b(9) = Asc("1") And b(10) = Asc("0") And b(11) = Asc("4") And b(12) = Asc("-") And b(13) = Asc("1") Then
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

                GetCurRowNumber()

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
        Dim pass As Integer = 0
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
            addr = &H1B8
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

            CurHourNumber = CurHourNumber / 64
            CurDayNumber = (CurDayNumber) / 64


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

        ReDim SysInt(0 To 64)


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

                If ArchType = archType_hour Then
                    idx = CurHourNumber
                End If

                If ArchType = archType_day Then
                    idx = CurDayNumber
                End If

                ' ищем нужную архивную запись
                For i_seek = 0 To maxrec - 1

                    If ArchType = archType_hour Then
                        idx = idx - 1
                        If idx < 0 Then idx = 1536
                        memAddr = idx * 64
                    End If

                    If ArchType = archType_day Then
                        idx = idx - 1
                        If idx < 1536 Then idx = 1903
                        memAddr = idx * 64
                    End If


                    If Not Cash(idx).ok Then

                        tryCnt = 3
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

                            Frame(6) = &H0

                            Frame(7) = (memAddr And &HFF0000) >> 16
                            Frame(8) = (memAddr And &HFF00) >> 8
                            Frame(9) = memAddr And &HFF
                            Frame(10) = &H4

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
                                        Cash(idx).RecDate(0) = SysInt(0) ' H
                                        Cash(idx).RecDate(1) = SysInt(1) ' M
                                        Cash(idx).RecDate(2) = SysInt(2) ' D
                                        Cash(idx).RecDate(3) = SysInt(3) ' Y

                                        If ArchType = archType_hour Then

                                            If SysInt(0) = BCD(dt2.Hour) And
                                                SysInt(1) = BCD(dt2.Day) And
                                                SysInt(2) = BCD(dt2.Month) And
                                                SysInt(3) = BCD(dt2.Year - 2000) Then
                                                found = True
                                            End If

                                        End If
                                        If ArchType = archType_day Then

                                            If SysInt(1) = BCD(dt2.Day) And
                                                 SysInt(2) = BCD(dt2.Month) And
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

                            If Cash(idx).RecDate(0) = BCD(dt2.Hour) And
                                Cash(idx).RecDate(1) = BCD(dt2.Day) And
                                Cash(idx).RecDate(2) = BCD(dt2.Month) And
                                Cash(idx).RecDate(3) = BCD(dt2.Year - 2000) Then
                                ok = True

                                GoTo getInfo
                            End If

                        End If
                        If ArchType = archType_day Then
                            If Cash(idx).RecDate(1) = BCD(dt2.Day) And
                             Cash(idx).RecDate(2) = BCD(dt2.Month) And
                             Cash(idx).RecDate(3) = BCD(dt2.Year - 2000) Then
                                ok = True
                                GoTo getInfo
                            End If

                        End If
                    End If
                Next



                ok = False

getInfo:

                ' record found in archive
                If ok Then

                    ok = False
                    Dim tryPassCnt As Integer
                    Dim blockAddr As Long


                    ' try get record data
                    tryPassCnt = 5
                    blockAddr = memAddr
                    While Not ok And tryPassCnt >= 0
                        tryPassCnt = tryPassCnt - 1
                        EraseInputQueue()

                        Frame(0) = &H55
                        Frame(1) = NetAddr
                        Frame(2) = Not NetAddr
                        Frame(3) = &HF
                        Frame(4) = &H3
                        Frame(5) = &H5


                        Frame(6) = 0


                        Frame(7) = (blockAddr And &HFF0000) >> 16
                        Frame(8) = (blockAddr And &HFF00) >> 8
                        Frame(9) = blockAddr And &HFF
                        Frame(10) = &H40

                        ch = CheckSum(Frame, 0, 11)
                        Frame(11) = ch
                        MyTransport.CleanPort()
                        MyTransport.Write(Frame, 0, 12)

                        WaitForData()



                        cnt = MyTransport.BytesToRead
                        If Not ok And cnt > 0 Then
                            Dim ptr As Integer
                            Dim sz As Integer
                            ptr = 0
                            sz = 0
                            While cnt > 0
                                MyTransport.Read(b, ptr, cnt)
                                ptr += cnt
                                sz += cnt


                                If VerifySumm(b, 0, sz) Then


                                    ' packet size check
                                    If (b(5) = &H40) Then
                                        ok = True

                                        ' archive date check ?
                                        For i As Integer = 0 To &H40 - 1
                                            SysInt(i) = b(6 + i)
                                        Next
                                    End If
                                End If

                                RaiseIdle()
                                Thread.Sleep(CalcInterval(10))
                                cnt = MyTransport.BytesToRead
                            End While


                        End If


                        If ok Then
                            ok = VerifySysInt(SysInt)
                            If ok Then
                                If ArchType = archType_hour Then
                                    If SysInt(0) = BCD(dt2.Hour) And
                                        SysInt(1) = BCD(dt2.Day) And
                                        SysInt(2) = BCD(dt2.Month) And
                                        SysInt(3) = BCD(dt2.Year - 2000) Then

                                        Return SysInt
                                    End If
                                Else
                                    If SysInt(1) = BCD(dt2.Day) And
                                        SysInt(2) = BCD(dt2.Month) And
                                        SysInt(3) = BCD(dt2.Year - 2000) Then

                                        Return SysInt
                                    End If
                                End If


                            End If
                            End If


                    End While



                End If
            End If
        Catch
            Return Nothing
        End Try

        Return Nothing
    End Function

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
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


    Public Overrides Function DeCodeHCNumber(ByVal CodeHC As Long, Optional tv As Integer = 0) As String

        DeCodeHCNumber = CodeHC.ToString()

    End Function
    Public Overrides Function DeCodeHCText(ByVal CodeHC As Long) As String

        DeCodeHCText = ""
        If (CodeHC And &HFF) = &HFF Then DeCodeHCText = DeCodeHCText & "Техническая неисправность;"
        If (CodeHC And &H1) = &H1 Then DeCodeHCText = DeCodeHCText & "Разность температур меньше минимальной;"
        If (CodeHC And &H2) = &H2 Then DeCodeHCText = DeCodeHCText & "Расход меньше минимального;"
        If (CodeHC And &H4) = &H4 Then DeCodeHCText = DeCodeHCText & "Расход больше максимального;"



    End Function
    Public Overrides Function DeCodeHC(ByVal CodeHC As Long) As String
        DeCodeHC = ""
        If (CodeHC And &HFF) = &HFF Then DeCodeHC = DeCodeHC & "Техническая неисправность;"
        If (CodeHC And &H1) = &H1 Then DeCodeHC = DeCodeHC & "Разность температур меньше минимальной;"
        If (CodeHC And &H2) = &H2 Then DeCodeHC = DeCodeHC & "Расход меньше минимального;"
        If (CodeHC And &H4) = &H4 Then DeCodeHC = DeCodeHC & "Расход больше максимального;"

    End Function






    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
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
        Try
            l = Bytes2Single(SI, pos, True)
        Catch ex As Exception
            l = Single.NaN
        End Try

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

        l = GetLng(SI, &H144, 0)
        tarc.V1 = l

        f = GetFlt(SI, &H148, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            'If f < 1 Then
            tarc.V1 += f
            ' End If
        End If





        l = GetLng(SI, &H14C, 0)
        tarc.M1 = l

        f = GetFlt(SI, &H150, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            'If f < 1 Then
            tarc.M1 += f
            ' End If

        End If


            l = GetLng(SI, &H154, 0)
        tarc.Q1 = l

        f = GetFlt(SI, &H158, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            'If f < 1 Then
            tarc.Q1 += f
            ' End If
        End If





            l = GetLng(SI, &H15C, 0)
        tarc.WORKTIME1 = l
        l = GetLng(SI, &H160, 1)
        tarc.OKTIME1 = l
        l = GetLng(SI, &H164, 1)
        tarc.ERRTIME1 = l

        i = GetInt(SI, &H176, 0, 0)
        tarc.T1 = i / 100

        i = GetInt(SI, &H178, 0, 0)
        tarc.T2 = i / 100


        i = SI(&H17A)
        tarc.P1 = i / 100

        i = SI(&H17B)
        tarc.P2 = i / 100

    End Sub

    Private Sub ProcessM(ByRef tarc As MArchive, ByVal SI() As Byte)
        Dim f As Double
        Dim l As Long
        Dim i As Integer



        l = GetLng(SI, &H144, 0)
        tarc.V1 = l

        f = GetFlt(SI, &H148, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            'If f < 1 Then
            tarc.V1 += f
            'End If
        End If





        l = GetLng(SI, &H14C, 0)
        tarc.M1 = l

        f = GetFlt(SI, &H150, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            'If f < 1 Then
            tarc.M1 += f
            ' End If

        End If


        l = GetLng(SI, &H154, 0)
        tarc.Q1 = l

        f = GetFlt(SI, &H158, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            ' If f < 1 Then
            tarc.Q1 += f
            ' End If
        End If




        l = GetLng(SI, &H15C, 0)
        tarc.WORKTIME1 = l
        l = GetLng(SI, &H160, 1)
        tarc.OKTIME1 = l
        l = GetLng(SI, &H164, 1)
        tarc.ERRTIME1 = l

        i = GetInt(SI, &H176, 0, 0)
        tarc.t1 = i / 100

        i = GetInt(SI, &H178, 0, 0)
        tarc.t2 = i / 100


        i = SI(&H17A)
        tarc.p1 = i / 100

        i = SI(&H17B)
        tarc.p2 = i / 100






    End Sub

    Private Sub ProcessA(ByRef tarc As Archive, ByVal SI() As Byte, ByVal SIP() As Byte)
        Dim f As Double
        Dim l As Long
        Dim i As Integer

        i = GetInt(SI, &H36, 0, 0)
        tarc.T1 = i / 100
        i = GetInt(SI, &H36, 0, 1)
        tarc.T2 = i / 100

        i = SI(&H3A)
        tarc.P1 = i / 100

        i = SI(&H3B)
        tarc.P2 = i / 100



        l = GetLng(SI, &H4, 0)
        tarc.V1 = l

        f = GetFlt(SI, &H8, 0)

        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            ' If f < 1 Then
            tarc.V1 += f
            ' End If

        End If

        tarc.V1H = tarc.V1



        l = GetLng(SIP, &H4, 0)
        tarc.V1 -= l

        f = GetFlt(SIP, &H8, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            ' If f < 1 Then
            tarc.V1 -= f
            ' End If

        End If



        l = GetLng(SI, &HC, 0)
        tarc.M1 = l




        f = GetFlt(SI, &H10, 0)

        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            If f < 1 Then
                tarc.M1 += f
            End If

        End If



        l = GetLng(SIP, &HC, 0)
        tarc.M1 -= l

        f = GetFlt(SIP, &H10, 0)

        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            'If f < 1 Then
            tarc.M1 -= f
            'End If
        End If

        l = GetLng(SI, &H14, 0)
        tarc.Q1 = l

        f = GetFlt(SI, &H18, 0)
        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            ' If f < 1 Then
            tarc.Q1 += f
            ' End If
        End If

        tarc.Q1H = tarc.Q1

        l = GetLng(SIP, &H14, 0)
        tarc.Q1 -= l

        f = GetFlt(SIP, &H18, 0)

        If (Not Single.IsNaN(f)) And (Not Single.IsInfinity(f)) Then
            If f < 1 Then
                tarc.Q1 -= f
            End If
        End If



        l = GetLng(SI, &H1C, 0)
        tarc.WORKTIME1 = l / 60

        l = GetLng(SIP, &H1C, 0)
        tarc.WORKTIME1 -= l / 60



        l = GetLng(SI, &H20, 0)
        tarc.OKTIME1 = l / 60

        l = GetLng(SIP, &H20, 0)
        tarc.OKTIME1 -= l / 60

        tarc.ERRTIME1 = tarc.WORKTIME1 - tarc.OKTIME1


        tarc.HC = SI(&H34)
        tarc.HCtv1 = SI(&H34)
        tarc.HCtv2 = SI(&H35)


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





    Public Overrides Function ReadTArch() As String
        Dim AErr As String = ""
        clearTArchive(tArch)
        EraseInputQueue()

        Dim ok As Boolean
        Dim Frame(10) As Byte
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt(512) As Byte
        Dim tryCnt As Integer
        Dim okPass(0 To 1) As Boolean
        Dim i As Integer
        Dim packetSize As UInteger = 226

        For i = 0 To 1
            okPass(i) = False
        Next

        For pass = 0 To 1
            ok = False
            tryCnt = 3
            While Not ok And tryCnt >= 0
                tryCnt = tryCnt - 1
                ok = False


                EraseInputQueue()
                Frame(0) = &H55
                Frame(1) = NetAddr
                Frame(2) = Not NetAddr
                Frame(3) = &HF
                Frame(4) = &H1
                Frame(5) = &H3

                addr = packetSize * (pass)
                Frame(6) = (addr And &HFF00) >> 8
                Frame(7) = addr And &HFF
                Frame(8) = packetSize + pass * 2
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
                            If b(5) = packetSize + pass * 2 Then
                                For i = 0 To packetSize - 1
                                    SysInt(pass * packetSize + i) = b(6 + i)
                                Next
                                okPass(pass) = True
                                ok = True
                            End If
                        End If

                        RaiseIdle()
                        Thread.Sleep(CalcInterval(10))
                        cnt = MyTransport.BytesToRead
                    End While


                End If
            End While
            Thread.Sleep(1000)
            EraseInputQueue()

        Next

        ok = True
        For i = 0 To 1
            If okPass(i) = False Then
                ok = False
            End If
        Next



        If ok Then

            ProcessT(tArch, SysInt)
            tArch.DateArch = GetDeviceDate()

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
        clearMArchive(mArch)
        EraseInputQueue()

        Dim ok As Boolean
        Dim Frame(10) As Byte
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt(512) As Byte
        Dim tryCnt As Integer
        Dim okPass(0 To 4) As Boolean
        Dim i As Integer
        Dim packetSize As UInteger = 226

        For i = 0 To 1
            okPass(i) = False
        Next

        For pass = 0 To 1
            ok = False
            tryCnt = 3
            While Not ok And tryCnt >= 0
                tryCnt = tryCnt - 1
                ok = False


                EraseInputQueue()
                Frame(0) = &H55
                Frame(1) = NetAddr
                Frame(2) = Not NetAddr
                Frame(3) = &HF
                Frame(4) = &H1
                Frame(5) = &H3

                addr = packetSize * (pass)
                Frame(6) = (addr And &HFF00) >> 8
                Frame(7) = addr And &HFF
                Frame(8) = packetSize + pass * 2
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
                            If b(5) = packetSize + pass * 2 Then
                                For i = 0 To packetSize - 1
                                    SysInt(pass * packetSize + i) = b(6 + i)
                                Next
                                okPass(pass) = True
                                ok = True
                            End If

                        End If

                            RaiseIdle()
                        Thread.Sleep(CalcInterval(10))
                        cnt = MyTransport.BytesToRead
                    End While


                End If
            End While
            Thread.Sleep(1000)
            EraseInputQueue()
        Next

        ok = True
        For i = 0 To 1
            If okPass(i) = False Then
                ok = False
            End If
        Next



        If ok Then
            'ok = VerifySysInt(SysInt)
            'If ok Then

            ProcessM(mArch, SysInt)
            mArch.DateArch = GetDeviceDate()

            'End If

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
        EraseInputQueue()

        Dim ok As Boolean
        Dim Frame(10) As Byte
        Dim addr As UInteger
        Dim ch As UInt16
        Dim pass As Integer
        Dim SysInt(512) As Byte
        Dim tryCnt As Integer
        Dim okPass(0 To 4) As Boolean
        Dim i As Integer
        Dim packetSize As UInteger = 226

        For i = 0 To 1
            okPass(i) = False
        Next

        For pass = 0 To 1
            ok = False
            tryCnt = 3
            While Not ok And tryCnt >= 0
                tryCnt = tryCnt - 1
                ok = False


                EraseInputQueue()
                Frame(0) = &H55
                Frame(1) = NetAddr
                Frame(2) = Not NetAddr
                Frame(3) = &HF
                Frame(4) = &H1
                Frame(5) = &H3

                addr = packetSize * (pass)
                Frame(6) = (addr And &HFF00) >> 8
                Frame(7) = addr And &HFF
                Frame(8) = packetSize
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
                            For i = 0 To packetSize - 1
                                SysInt(pass * packetSize + i) = b(6 + i)
                            Next
                            okPass(pass) = True
                            ok = True
                        End If

                        RaiseIdle()
                        Thread.Sleep(CalcInterval(10))
                        cnt = MyTransport.BytesToRead
                    End While


                End If
            End While
            Thread.Sleep(1000)
            EraseInputQueue()
        Next

        ok = True
        For i = 0 To 1
            If okPass(i) = False Then
                ok = False
            End If
        Next

        If ok Then

            dr = dt.NewRow
            dr("Название") = "заводской номер прибора"
            dr("Значение") = Chr(SysInt(0)) + Chr(SysInt(1)) + Chr(SysInt(2)) + Chr(SysInt(3)) + Chr(SysInt(4)) + Chr(SysInt(5))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Диаметр условного прохода"
            dr("Значение") = SysInt(10)
            Select Case SysInt(10)
                Case 0
                    dr("Значение") = 15
                Case 1
                    dr("Значение") = 25
                Case 2
                    dr("Значение") = 32
                Case 3
                    dr("Значение") = 40
                Case 4
                    dr("Значение") = 50
                Case 5
                    dr("Значение") = 80
                Case 6
                    dr("Значение") = 100
                Case 7
                    dr("Значение") = 150
            End Select

            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Текущая дата прибора"
            dr("Значение") = GetDeviceDate().ToString()

            dt.Rows.Add(dr)


            dr = dt.NewRow
            dr("Название") = "G: 0 – программируемое значение температуры 1 - измеряемое"
            dr("Значение") = SysInt(&H22)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Программируемое значение расхода, % от Gmax"
            dr("Значение") = SysInt(&H23)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Уставка Gmin"
            dr("Значение") = SysInt(&H24)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Уставка Gmax"
            dr("Значение") = SysInt(&H25)
            dt.Rows.Add(dr)




            dr = dt.NewRow
            dr("Название") = "Т1: 0 – программируемое значение температуры 1 - измеряемое"
            dr("Значение") = SysInt(&H31)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Программируемое значение тем-пературы Т1"
            dr("Значение") = SysInt(&H32)
            dt.Rows.Add(dr)


            dr = dt.NewRow
            dr("Название") = "Т2: 0 – программируемое значение температуры 1 - измеряемое"
            dr("Значение") = SysInt(&H34)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Программируемое значение тем-пературы Т2"
            dr("Значение") = SysInt(&H35)
            dt.Rows.Add(dr)


            dr = dt.NewRow
            dr("Название") = "P1: 0 – программируемое значение  1 - измеряемое"
            dr("Значение") = SysInt(&H37)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Программируемое значение  P1"
            dr("Значение") = SysInt(&H38)
            dt.Rows.Add(dr)


            dr = dt.NewRow
            dr("Название") = "P2: 0 – программируемое значение  1 - измеряемое"
            dr("Значение") = SysInt(&H3A)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Программируемое значение  P2"
            dr("Значение") = SysInt(&H3B)
            dt.Rows.Add(dr)



            dr = dt.NewRow
            dr("Название") = "Минимальная разность температур"
            dr("Значение") = SysInt(&H3D)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = " Тип теплосистемы"
            dr("Значение") = SysInt(&H3D)
            dt.Rows.Add(dr)


            dr = dt.NewRow
            dr("Название") = "Адрес следующей часовой записи"
            dr("Значение") = GetLng(SysInt, &H1B8, 0)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Адрес следующей суточной записи"
            dr("Значение") = GetLng(SysInt, &H1BC, 0)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Адрес следующей записи на отчетную дату"
            dr("Значение") = GetLng(SysInt, &H1C0, 0)
            dt.Rows.Add(dr)
        End If

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
        tryCnt = 3

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
            Frame(6) = &H0
            Frame(7) = 7
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
                        If b(5) = 7 Then
                            ok = True
                            Dim d As DateTime
                            Dim ss As Integer
                            ss = ((b(6) And &HF0) >> 4) * 10 + (b(6) And &HF)
                            Dim mm As Integer
                            mm = ((b(7) And &HF0) >> 4) * 10 + (b(7) And &HF)
                            Dim hh As Integer
                            hh = ((b(8) And &HF0) >> 4) * 10 + (b(8) And &HF)

                            Dim dd As Integer
                            dd = ((b(10) And &HF0) >> 4) * 10 + (b(10) And &HF)

                            Dim mn As Integer
                            mn = ((b(11) And &HF0) >> 4) * 10 + (b(11) And &HF)
                            Dim yy As Integer
                            yy = ((b(12) And &HF0) >> 4) * 10 + (b(12) And &HF)

                            Try
                                d = New DateTime(2000 + yy, mn, dd, hh, mm, ss)
                            Catch ex As Exception
                                d = DateAndTime.Now
                            End Try

                            Return d
                        End If
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
