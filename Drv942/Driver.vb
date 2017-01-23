Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Class Driver
    Inherits STKTVMain.TVDriver


    Private mIsConnected As Boolean


    'Private Structure MArchive
    '    Public DateArch As DateTime
    '    Public HC As Long
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

    '    Public t1 As Single
    '    Public t2 As Single
    '    Public t4 As Single
    '    Public t5 As Single

    '    Public p1 As Single
    '    Public p2 As Single
    '    Public p3 As Single
    '    Public p4 As Single

    '    Public dt12 As Single
    '    Public dt45 As Single

    '    Public SP As Long
    '    Public SPtv1 As Long
    '    Public SPtv2 As Long


    '    Public archType As Short
    'End Structure

    'Private Structure TArchive
    '    Public DateArch As DateTime


    '    Public V1 As Double
    '    Public V2 As Double
    '    Public V3 As Double
    '    Public V4 As Double
    '    Public V5 As Double
    '    Public V6 As Double

    '    Public M1 As Double
    '    Public M2 As Double
    '    Public M3 As Double
    '    Public M4 As Double
    '    Public M5 As Double
    '    Public M6 As Double
    '    Public Q1 As Double
    '    Public Q2 As Double

    '    Public TW1 As Double
    '    Public TW2 As Double

    '    Public archType As Short
    'End Structure

    'Private Structure Archive
    '    Public DateArch As DateTime

    '    Public HC As Long
    '    Public MsgHC As String

    '    Public HCtv1 As Long
    '    Public MsgHC_1 As String

    '    Public HCtv2 As Long
    '    Public MsgHC_2 As String

    '    Public Tw1 As Single
    '    Public Tw2 As Single

    '    Public P1 As Single
    '    Public T1 As Single
    '    Public M2 As Single
    '    Public V1 As Single

    '    Public P2 As Single
    '    Public T2 As Single
    '    Public M3 As Single
    '    Public V2 As Single

    '    Public V3 As Single
    '    Public M1 As Single

    '    Public Q1 As Single
    '    Public Q2 As Single

    '    Public SP As Long
    '    Public SPtv1 As Long
    '    Public SPtv2 As Long

    '    Public T3 As Single
    '    Public T4 As Single
    '    Public P3 As Single
    '    Public P4 As Single
    '    Public v4 As Single
    '    Public v5 As Single
    '    Public v6 As Single
    '    Public M4 As Single
    '    Public M5 As Single
    '    Public M6 As Single

    '    Public archType As Short
    'End Structure

    Dim tv As Short





    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False
    Dim IsTArchToRead As Boolean = False

    Dim buffer(32767) As Byte
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
    Public Overrides Property isMArchToDBWrite() As Boolean

        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property
    Dim m_isTArchToDBWrite As Boolean = False
    Public Overrides Property isTArchToDBWrite() As Boolean

        Get
            Return m_isTArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isTArchToDBWrite = value
        End Set
    End Property


    'Public inputbuffer(69) As Byte

    Public Overrides Function CounterName() As String
        Return "SPT942"
    End Function







    Public Sub ConnectTV1()
        EraseInputQueue()

        Dim bArr(0 To 8) As Byte
        Try

            bArr(0) = &H10
            bArr(1) = &HFF
            bArr(2) = &H3F
            bArr(3) = &H1
            bArr(4) = &H0
            bArr(5) = &H0
            bArr(6) = &H0
            'bArr(7) = &HC1
            bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
            bArr(8) = &H16

            WillCountToRead = 8
            IsBytesToRead = True

            write(bArr, 9)
            tv = 1
        Catch exc As Exception
        End Try
    End Sub
    Public Sub ConnectTV2()
        EraseInputQueue()


        Dim bArr(0 To 8) As Byte
        Try

            bArr(0) = &H10
            bArr(1) = &HFF
            bArr(2) = &H3F
            bArr(3) = &H2
            bArr(4) = &H0
            bArr(5) = &H0
            bArr(6) = &H0
            'bArr(7) = &HC1
            bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
            bArr(8) = &H16

            WillCountToRead = 8
            IsBytesToRead = True
            ' tim.Start()
            write(bArr, 9)
            tv = 2
        Catch exc As Exception
        End Try
    End Sub
    Public Overrides Sub Connect()



        Dim i As Integer

        For i = 0 To 5
            If TryConnect() Then
                Return ' True
            End If
        Next
        Return 'False
    End Sub

    Private Function TryConnect() As Boolean
        EraseInputQueue()

        Dim startBytes(0 To 28) As Byte
        Dim i As Int16


        If (IsBytesToRead = True) Then
            Return False
        End If
        For i = 0 To 18
            startBytes(i) = &HFF
        Next
        startBytes(19) = 0



        startBytes(20) = &H10
        startBytes(21) = &HFF
        startBytes(22) = &H3F
        startBytes(23) = &H0
        startBytes(24) = &H0
        startBytes(25) = &H0
        startBytes(26) = &H0
        startBytes(27) = 255 - ((Int(startBytes(21)) + Int(startBytes(22)) + Int(startBytes(23)) + Int(startBytes(24)) + Int(startBytes(25)) + Int(startBytes(26))) Mod 256)
        startBytes(28) = &H16


        WillCountToRead = 8
        IsBytesToRead = True
        write(startBytes, 29)


        System.Threading.Thread.Sleep(CalcInterval(40))
        RaiseIdle()

        ' Dim bArr(0 To 8) As Byte
        Try

            'bArr(0) = &H10
            'bArr(1) = &HFF
            'bArr(2) = &H3F
            'bArr(3) = &H0
            'bArr(4) = &H0
            'bArr(5) = &H0
            'bArr(6) = &H0
            'bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
            'bArr(8) = &H16


            'EraseInputQueue()
            'WillCountToRead = 8
            'IsBytesToRead = True
            'write(bArr, 9)
            tv = 0

            Dim sret As String

            RaiseIdle()
            WaitForData()
            sret = GetAndProcessData()
            If (sret.Length > 5) Then
                If (sret.Substring(0, 6) = "Ошибка") Then
                    EraseInputQueue()
                    Return False
                End If
                mIsConnected = True
                Return True
            End If

            If sret.Length = 0 Then
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            End If

        Catch exc As Exception
            Return False
        End Try

    End Function
    Public Function ReadFlashSync(ByVal fistpage As Integer, ByVal ReadPageCount As Integer) As String
        pagesToRead = 0
        Dim bArr(0 To 8) As Byte
        Dim buf(32000) As Byte



        Try
            'If (fistpage < 0 Or fistpage > 3071) Then
            '    'MsgBox("Неверный номер первой считываемой страницы", MsgBoxStyle.OkOnly, "ReadFlash")
            '    Return ""
            'End If
            If (ReadPageCount < 1 Or ReadPageCount > 64) Then
                'MsgBox("Неверное количество считываемых страниц", MsgBoxStyle.OkOnly, "ReadFlash")
                Return ""
            End If
        Catch ew As Exception
            'MsgBox("Неверные параметры чтения FLASH-памяти", MsgBoxStyle.OkOnly, "ReadFlash")
            Return ""
        End Try

        EraseInputQueue()

        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H45
        bArr(3) = fistpage Mod 256
        bArr(4) = fistpage \ 256
        bArr(5) = ReadPageCount
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16

        WillCountToRead = (ReadPageCount * (64 + 5))

        write(bArr, 9)

        WaitForData()


        Dim T As DateTime
        Dim ret As Integer
        Dim i As Integer
        T = DateTime.Now

        bufferindex = 0
        While (bufferindex < WillCountToRead)
            RaiseIdle()
            ret = MyRead(buf, 0, WillCountToRead - bufferindex, CalcInterval(WillCountToRead))
            For i = 0 To ret - 1
                buffer(bufferindex + i) = buf(i)
            Next
            If T.AddSeconds(90) < DateTime.Now Then
                Return ""
            End If
            bufferindex = bufferindex + ret
        End While


        Dim sout As String

        sout = ""
        Dim sss As String = ""
        For i = 3 To bufferindex - 3
            If i < 32000 Then
                sout = sout & Chr(buffer(i))
                sss = sss + Hex(buffer(i)) + " "
                'bout(i - 3) = buffer(i)
            End If

        Next
        Debug.Print("buffrer:" + sss)
        Return sout

    End Function


    Private m_readRAMByteCount As Short

    Public Function ReadRAMSync(ByVal fistbyte As Integer, ByVal byteCount As Integer) As String
        Dim buf(8000) As Byte
        Dim bArr(0 To 8) As Byte
        'm_readRAMByteCount = byteCount
        Try
            If (fistbyte < 0 Or fistbyte > 1023) Then
                'MsgBox("Неверный адрес первого считываемого элемента", MsgBoxStyle.OkOnly, "ReadRAM")
                Return ""
            End If
            If (byteCount < 1 Or byteCount > 64) Then
                'MsgBox("Неверное количество считываемых байтов", MsgBoxStyle.OkOnly, "ReadRAM")
                Return ""
            End If
        Catch ew As Exception
            'MsgBox("Неверные параметры чтения ОЗУ", MsgBoxStyle.OkOnly, "ReadRAM")
            Return ""
        End Try

        EraseInputQueue()

        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = fistbyte Mod 256
        bArr(4) = fistbyte \ 256
        bArr(5) = byteCount
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16


        write(bArr, 9)

        WaitForData()

        Dim T As DateTime
        Dim ret As Integer
        Dim i As Integer
        T = DateTime.Now

        bufferindex = 0
        While (bufferindex < byteCount + 5)
            RaiseIdle()

            ret = MyRead(buf, (0), byteCount + 5 - bufferindex, CalcInterval(byteCount + 5))
            For i = 0 To ret - 1
                buffer(bufferindex + i) = buf(i)
            Next
            If T.AddSeconds(90) < DateTime.Now Then
                Return ""
            End If
            bufferindex = bufferindex + ret
        End While
        Dim sout As String

        sout = ""
        For i = 3 To bufferindex - 3
            sout = sout & Chr(buffer(i))
        Next
        Return sout

    End Function


    Public Sub ReadRAM(ByVal fistbyte As Integer, ByVal byteCount As Integer)
        If (IsBytesToRead = True) Then
            Return
        End If
        Dim bArr(0 To 8) As Byte
        m_readRAMByteCount = byteCount
        Try
            If (fistbyte < 0 Or fistbyte > 1023) Then
                'MsgBox("Неверный адрес первого считываемого элемента", MsgBoxStyle.OkOnly, "ReadRAM")
                Return
            End If
            If (byteCount < 1 Or byteCount > 64) Then
                'MsgBox("Неверное количество считываемых байтов", MsgBoxStyle.OkOnly, "ReadRAM")
                Return
            End If
        Catch ew As Exception
            'MsgBox("Неверные параметры чтения ОЗУ", MsgBoxStyle.OkOnly, "ReadRAM")
            Return
        End Try
        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = fistbyte Mod 256
        bArr(4) = fistbyte \ 256
        bArr(5) = byteCount
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16

        WillCountToRead = m_readRAMByteCount + 5
        IsBytesToRead = True
        ' tim.Start()
        write(bArr, 9)
    End Sub
    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        cleararchive(Arch)
        EraseInputQueue()
        If (IsBytesToRead = True) Then
            Return ""
        End If
        m_isArchToDBWrite = False
        Dim bArr(0 To 8) As Byte
        Dim ret As String = ""
        Dim retsum As String = ""
        Dim trycnt As Int32
        Dim trycnttv As Int32
        Dim tv1OK As Boolean
        Dim tv2OK As Boolean
        Arch.MsgHC = ""



        trycnt = 5

tryagain1:

        trycnttv = 5

tryagain1a:
        If (ArchType = archType_hour) Then
            bArr(2) = &H48
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = ArchHour Mod 24
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            Arch.DateArch = Arch.DateArch.AddSeconds(-1)
        End If

        If (ArchType = archType_day) Then
            bArr(2) = &H59
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = &H0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            Arch.DateArch = Arch.DateArch.AddSeconds(-1)
        End If


        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16

        tv1OK = True
        IsBytesToRead = False
        ConnectTV1()
        WaitForData()

        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = ret
                tv1OK = False
                trycnttv -= 1
                If trycnttv = 0 Then
                    GoTo tryagain2
                Else
                    GoTo tryagain1a
                End If

            End If

            If (ret.Length = 0) Then
                EraseInputQueue()
                retsum = "Ошибка переключения на ТВ1"
                tv1OK = False
                trycnttv -= 1
                If trycnttv = 0 Then
                    GoTo tryagain2
                Else
                    GoTo tryagain1a
                End If
            End If

        End If


        WillCountToRead = 69
        IsBytesToRead = True
        write(bArr, 9)
        WaitForData()

        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = retsum + ret + " " + "ТВ" + tv.ToString
                If trycnt = 0 Then
                    trycnt = 5
                    tv1OK = False
                    GoTo tryagain2
                Else
                    trycnt -= 1
                    GoTo tryagain1
                End If

            Else
                tv1OK = True
            End If

        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            retsum = retsum & vbCrLf & "Ошибка чтения архива по ТВ1"
            tv1OK = False
            If trycnt = 0 Then
                trycnt = 5
                GoTo tryagain2
            Else
                trycnt -= 1
                GoTo tryagain1
            End If
        End If

        trycnt = 5

tryagain2:

        trycnttv = 5

tryagain2a:

        If (ArchType = archType_hour) Then
            bArr(2) = &H48
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = ArchHour Mod 24
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            Arch.DateArch = Arch.DateArch.AddSeconds(-1)
        End If

        If (ArchType = archType_day) Then
            bArr(2) = &H59
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = &H0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            Arch.DateArch = Arch.DateArch.AddSeconds(-1)
        End If



        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16

        IsBytesToRead = False
        ConnectTV2()
        WaitForData()

        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = retsum + ret
                tv2OK = False

                trycnttv -= 1
                If trycnttv = 0 Then
                    GoTo finalRet
                Else
                    GoTo tryagain2a
                End If


            End If


        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            tv2OK = False
            retsum = retsum + "Ошибка перключения на ТВ2"
            trycnttv -= 1
            If trycnttv = 0 Then
                GoTo finalRet
            Else
                GoTo tryagain2a
            End If
        End If



        WillCountToRead = 69
        IsBytesToRead = True
        write(bArr, 9)
        WaitForData()

        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                If (retsum <> "") Then
                    retsum = retsum & vbCrLf
                End If
                retsum = retsum + ret + " " + "ТВ" + tv.ToString
                retsum = retsum & vbCrLf
                retsum = retsum + "Архив не прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                If trycnt = 0 Then
                    tv2OK = False
                    GoTo finalRet
                Else
                    trycnt -= 1
                    GoTo tryagain2
                End If
            Else
                tv2OK = True
            End If
            retsum = retsum + ret + " " + "ТВ" + tv.ToString
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            tv2OK = False
            GoTo finalRet
        End If
finalRet:
        If tv1OK Or tv2OK Then
            retsum = "Архив прочитан" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        Else
            retsum = "Ошибка чтения" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        End If

    End Function
    Public Function writeMessage(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""
        Dim KC As Long = 0
        Try

            If (buf(2) = &H3F) Then 'установка связи
                Dim i As Integer
                For i = 0 To 7
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                KC = 255 - ((Int(buf(1)) + Int(buf(2)) +
                    Int(buf(3)) + Int(buf(4)) + Int(buf(5))) Mod 256)
                retstring += vbCrLf
                If (KC <> buf(6)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")

                End If
                Return retstring
            End If
            If (buf(2) = &H45) Then 'чтение Flash-памяти
                Dim i As Integer
                For i = 0 To 68
                    retstring += Hex(buf(i)) + " "
                Next
                retstring += vbCrLf
                KC = 0
                For i = 1 To 66
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(67)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                Return retstring
            End If

            If (buf(2) = &H52 And IsTArchToRead = True) Then 'чтение тотального архива
                IsTArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                'If (tv = 1 Or tv = 2) Then m_readRAMByteCount = 36
                If (tv = 3) Then m_readRAMByteCount = 6
                KC = 0
                For i = 1 To 2 + m_readRAMByteCount
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(3 + m_readRAMByteCount)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                tArch.archType = 2



                If (tv = 3) Then
                    tArch.DateArch = New DateTime(buf(3) + 2000, buf(4), buf(5), buf(6), buf(7), buf(8))
                    m_isTArchToDBWrite = True
                    m_readRAMByteCount = 0
                End If

                Return "архив прочитан"
            End If



            If (buf(2) = &H52 And IsmArchToRead = True) Then 'чтение мгновенного архива
                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                If (tv = 1 Or tv = 2) Then m_readRAMByteCount = 36
                If (tv = 3) Then m_readRAMByteCount = 6
                KC = 0
                For i = 1 To 2 + m_readRAMByteCount
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(3 + m_readRAMByteCount)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                If (tv = 1 Or tv = 2) Then
                    For i = 3 To 39
                        str = str + Chr(buf(i))
                    Next
                End If
                mArch.archType = 1
                Dim Adr As Long
                Adr = 1


                If (tv = 1) Then
                    'mArch.HCtv1 = Asc(Mid(str, Adr, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr + 2, 1))
                    mArch.HCtv1 = Asc(Mid(str, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr, 1))
                    mArch.MsgHC_1 = DeCodeHC(mArch.HCtv1)
                    mArch.SPtv1 = Asc(Mid(str, Adr + 3, 1))
                    mArch.G1 = FloatExt(Mid(str, Adr + 4, 4))
                    mArch.G2 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                    mArch.G3 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                    mArch.p1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                    mArch.p2 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                    mArch.t1 = FloatExt(Mid(str, Adr + 4 * 6, 4))
                    mArch.t2 = FloatExt(Mid(str, Adr + 4 * 7, 4))
                    mArch.dt12 = FloatExt(Mid(str, Adr + 4 * 8, 4))
                End If
                If (tv = 2) Then
                    'mArch.HCtv2 = Asc(Mid(str, Adr, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr + 2, 1))
                    mArch.HCtv2 = Asc(Mid(str, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr, 1))
                    mArch.MsgHC = DeCodeHC(mArch.HCtv2)
                    mArch.SPtv2 = Asc(Mid(str, Adr + 3, 1))
                    mArch.G4 = FloatExt(Mid(str, Adr + 4, 4))
                    mArch.G5 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                    mArch.G6 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                    mArch.p3 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                    mArch.p4 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                    mArch.t4 = FloatExt(Mid(str, Adr + 4 * 6, 4))
                    mArch.t5 = FloatExt(Mid(str, Adr + 4 * 7, 4))
                    mArch.dt45 = FloatExt(Mid(str, Adr + 4 * 8, 4))
                End If
                If (tv = 3) Then
                    mArch.DateArch = New DateTime(buf(3) + 2000, buf(4), buf(5), buf(6), buf(7), buf(8))
                    m_isMArchToDBWrite = True
                    m_readRAMByteCount = 0
                End If

                Return "архив прочитан"
            End If
            If (buf(2) = &H52 And IsmArchToRead = False) Then 'чтение ОЗУ
                Dim i As Integer
                For i = 0 To 4 + m_readRAMByteCount
                    retstring += Hex(buf(i)) + " "
                Next
                retstring += vbCrLf
                KC = 0
                For i = 1 To 2 + m_readRAMByteCount
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(3 + m_readRAMByteCount)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                Return retstring
            End If
            If (buf(2) = &H48) Then 'часовой архив
                If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32
                KC = 0
                For i = 1 To 66
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(67)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 3 To 64
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1

                If (tv = 1) Then
                    'Arch.HCtv1 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    Arch.MsgHC_1 = DeCodeHC(Arch.HCtv1)
                    Arch.SPtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.WORKTIME1 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                End If
                If (tv = 2) Then
                    'Arch.HCtv2 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    Arch.MsgHC_2 = DeCodeHC(Arch.HCtv2)
                    Arch.SPtv2 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P3 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P4 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T3 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T4 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.v5 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.v6 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.M5 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.M6 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.Q2 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.WORKTIME2 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                End If

                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "архив прочитан"
            End If
            If (buf(2) = &H59) Then 'суотчный архив
                If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                For i = 1 To 66
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(67)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 3 To 64
                    hourstr = hourstr + Chr(buf(i))
                Next

                'hourstr = buf.ToString
                Arch.archType = archType_day
                Dim Adr As Long
                Adr = 1
                If (tv = 1) Then
                    'Arch.HCtv1 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    Arch.MsgHC_1 = DeCodeHC(Arch.HCtv1)
                    Arch.SPtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.WORKTIME1 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                End If
                If (tv = 2) Then
                    'Arch.HCtv2 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    Arch.MsgHC_2 = DeCodeHC(Arch.HCtv2)
                    Arch.SPtv2 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P3 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P4 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T3 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T4 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.v5 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.v6 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.M5 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.M6 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.Q2 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.WORKTIME2 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                End If

                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "Архив прочитан"
            End If
            'MsgBox("Пакет распознан некорректно!", MsgBoxStyle.OkOnly, "Ошибка")
            retstring = "Ошибка"
            Return retstring
        Catch exc As Exception
        End Try
        Return "Ошибка!Пакет не распознан"
    End Function
    Public Function GetAndProcessData() As String
        Dim buf(69) As Byte
        Dim i As Int16
        For i = 0 To 69
            buf(i) = 0
        Next

        Dim ret As Long

        If (IsBytesToRead = False) Then
            Return ""
        End If

        Try
            ret = MyRead(buf, 0, WillCountToRead, CalcInterval(WillCountToRead) * 5)

            If (buf(2) = &H21) Then
                '
                EraseInputQueue()
                Return "Ошибка. Код ошибки:" + Hex(buf(3))
            End If
            If (ret > 0) Then
                If (ret = WillCountToRead) Then
                    If (ispackageError = True) Then

                        For i = bufferindex + 1 To bufferindex + ret
                            buffer(i) = buf(i - bufferindex - 1)
                        Next
                        If (pagesToRead < 2) Then IsBytesToRead = False
                        bufferindex = 0
                        For i = 0 To 69
                            buffer(i) = 0
                        Next
                        If (pagesToRead < 2) Then EraseInputQueue()
                        ispackageError = False
                        Return writeMessage(buffer, bufferindex)
                    End If
                    If (pagesToRead > 1) Then
                        pagesToRead = pagesToRead - 1
                        Return writeMessage(buf, ret)
                    End If
                    'tim.Stop()
                    IsBytesToRead = False
                    EraseInputQueue()
                    Return writeMessage(buf, ret)
                End If
                If (ret < WillCountToRead) Then
                    For i = bufferindex To bufferindex + ret - 1
                        buffer(i) = buf(i)
                    Next
                    ispackageError = True
                    WillCountToRead = WillCountToRead - ret
                    bufferindex = bufferindex + ret - 1
                    Return "Ошибка. Пакет получен не полностью"
                End If
            End If
        Catch ex As Exception
            Return "Ошибка." + ex.Message
        End Try
        Return ""
    End Function


    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        Dim arr(3) As Byte
        For i = 0 To 3
            arr(i) = Asc(Mid(extStr, 1 + i, 1))
        Next

        i = BitConverter.ToInt32(arr, 0)
        ExtLong4 = i

        'For i = 0 To 3
        '    ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        'Next i
    End Function


    Public Overrides Function DeCodeHCNumber(ByVal CodeHC As Long, Optional tv As Integer = 0) As String

        DeCodeHCNumber = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCNumber = "TB" + tv.ToString + ":НС00" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС01" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС05" + ";"
        End If
        If CodeHC And 2 ^ 6 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС06" + ";"
        End If
        If CodeHC And 2 ^ 7 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС07" + ";"
        End If



        If CodeHC And 2 ^ 8 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС08" + ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС09" + ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС10" + ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС11 " + ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС12" + ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС13" + ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС14" + ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС15" + ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС16" + ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС17 " + ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС18" + ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":НС19" + ";"
        End If
    End Function


    Public Function DeCodeHCRaw(ByVal CodeHC As Long) As String

        DeCodeHCRaw = ""


        If CodeHC = 0 Then Return "-"

        If CodeHC And 2 ^ 0 Then
            DeCodeHCRaw = " 00" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCRaw = DeCodeHCRaw + " 01" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCRaw = DeCodeHCRaw + " 02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCRaw = DeCodeHCRaw + " 03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCRaw = DeCodeHCRaw + " 04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCRaw = DeCodeHCRaw + " 05" + ";"
        End If
        If CodeHC And 2 ^ 6 Then
            DeCodeHCRaw = DeCodeHCRaw + " 06" + ";"
        End If
        If CodeHC And 2 ^ 7 Then
            DeCodeHCRaw = DeCodeHCRaw + " 07" + ";"
        End If


        If CodeHC And 2 ^ 8 Then
            DeCodeHCRaw = DeCodeHCRaw + " 08" + ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCRaw = DeCodeHCRaw + " 09" + ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCRaw = DeCodeHCRaw + " 10" + ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCRaw = DeCodeHCRaw + " 11 " + ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCRaw = DeCodeHCRaw + " 12" + ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCRaw = DeCodeHCRaw + " 13" + ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCRaw = DeCodeHCRaw + " 14" + ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCRaw = DeCodeHCRaw + " 15" + ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCRaw = DeCodeHCRaw + " 16" + ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCRaw = DeCodeHCRaw + " 17 " + ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCRaw = DeCodeHCRaw + " 18" + ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCRaw = DeCodeHCRaw + " 19" + ";"
        End If
    End Function

    Public Overrides Function DeCodeHCText(ByVal CodeHC As Long) As String

        DeCodeHCText = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCText = DeCodeHCText _
                   & "Разряд батареи" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCText = DeCodeHCText _
                   & "Перегрузка питания" + ";"
        End If


        If CodeHC And 2 ^ 3 Then
            DeCodeHCText = DeCodeHCText _
                    & "Активный уровень сигнала на дискретном входе D2" & ";"
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHCText = DeCodeHCText _
                    & "Сигнал Qp по каналу т1 меньше нижнего предела " & ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHCText = DeCodeHCText _
                    & "Сигнал Qp по каналу т2 меньше нижнего предела " & ";"
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHCText = DeCodeHCText _
                    & "Сигнал Qp по каналу т1 вревысил верхний предела " & ";"
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHCText = DeCodeHCText _
                    & "Сигнал Qp по каналу т2 вревысил верхний предела " & ";"
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHCText = DeCodeHCText _
                    & "P1 вне 0-1.1ВП1" + ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCText = DeCodeHCText _
                    & "P2 вне 0-1.1ВП1" + ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCText = DeCodeHCText _
                    & "T1 вне 0-176гр.С" + ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCText = DeCodeHCText _
                    & "T2 вне 0-176гр.С" + ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCText = DeCodeHCText _
                    & "G1>Gв1" + ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G1<Gн1" + ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCText = DeCodeHCText _
                    & "G2>Gв2" + ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G2>Gн2" + ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCText = DeCodeHCText _
                    & "G3>Gв3" + ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G3>Gн3" + ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCText = DeCodeHCText _
                    & "M3ч < -0.04M1" + ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCText = DeCodeHCText _
                    & "Qч < 0" + ";"
        End If
        If DeCodeHCText = "" Then
            DeCodeHCText = "Нет НС"
        End If
    End Function
    Public Overrides Function DeCodeHC(ByVal CodeHC As Long) As String

        DeCodeHC = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHC = "НС:0 - Разряд батареи" & vbCrLf
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHC = DeCodeHC _
                    & "НС:1 - Перегрузка питания" & vbCrLf
        End If


        If CodeHC And 2 ^ 2 Then
            DeCodeHC = DeCodeHC _
                    & "НС:2 - Перегрузка по цепям питания датчиков давления (только для модели 02)" & vbCrLf
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHC = DeCodeHC _
                    & "НС:3 - Активный уровень сигнала на дискретном входе D2" & vbCrLf
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHC = DeCodeHC _
                    & "НС:4 - Сигнал Qp по каналу т1 меньше нижнего предела " & vbCrLf
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHC = DeCodeHC _
                    & "НС:5 - Сигнал Qp по каналу т2 меньше нижнего предела " & vbCrLf
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHC = DeCodeHC _
                    & "НС:6 - Сигнал Qp по каналу т1 вревысил верхний предела " & vbCrLf
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHC = DeCodeHC _
                    & "НС:7 - Сигнал Qp по каналу т2 вревысил верхний предел " & vbCrLf
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHC = DeCodeHC _
                    & "НС:8 - P1 вне 0-1.1ВП1" & vbCrLf
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHC = DeCodeHC _
                    & "НС:9 -  - P2 вне 0-1.1ВП1" & vbCrLf
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHC = DeCodeHC _
                    & "НС:10 - T1 вне 0-176гр.С" & vbCrLf
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHC = DeCodeHC _
                    & "НС:11 - T2 вне 0-176гр.С" & vbCrLf
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHC = DeCodeHC _
                    & "НС:12 - G1>Gв1" & vbCrLf
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHC = DeCodeHC _
                    & "НС:13 - 0<G1<Gн1" & vbCrLf
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHC = DeCodeHC _
                    & "НС:14 - G2>Gв2" & vbCrLf
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHC = DeCodeHC _
                    & "НС:15 - 0<G2>Gн2" & vbCrLf
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHC = DeCodeHC _
                    & "НС:16 - G3>Gв3" & vbCrLf
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHC = DeCodeHC _
                    & "НС:17 - 0<G3>Gн3" & vbCrLf
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHC = DeCodeHC _
                    & "НС:18 - M3ч < -0.04M1" & vbCrLf
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHC = DeCodeHC _
                    & "НС:19 - Qч < 0" & vbCrLf
        End If
    End Function
    Private Function FloatExt(ByVal floatStr As String) As Single
        Dim tmpStr As String = ""
        Dim E As Long
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Single
        Dim i As Long
        'If floatStr = "" Then Exit Function
        If floatStr.Length <> 4 Then Exit Function
        ' If floatStr = String(4, 0) Then Exit Function
        If floatStr = Chr(0) + Chr(0) + Chr(0) + Chr(0) Then
            Return 0.0
        End If
        For i = 1 To 4
            tmpStr = Chr(Asc(Mid(floatStr, i, 1))) & tmpStr
        Next i


        floatStr = tmpStr
        '================ Float число========================
        'ст.байт                                 младший байт
        '====================================================
        'двоич.порядок |ст.байт                  младший байт
        '----------------------------------------------------
        ' xxxx xxxx     | sxxx xxxx | xxxx xxxx | xxxx xxxx |

        ' A = (-1)^s * f * 2^(e-127)
        ' f= сумма от 0 до 23 a(k)*2^(-k), где a(k) бит мантисы с номером k


        E = Asc(Mid(floatStr, 1, 1))
        If Asc(Mid(floatStr, 2, 1)) And (2 ^ 7) Then
            s = 1
        Else
            s = 0
        End If
        Mantissa = ((Asc(Mid(floatStr, 2, 1)) And &H7F) << 16) _
                     + (Asc(Mid(floatStr, 3, 1)) << 8) _
                     + (Asc(Mid(floatStr, 4, 1)))

        'Mantissa = (Asc(Mid(floatStr, 2, 1)) And &H7F) * (2 ^ 16) _
        '                     + Asc(Mid(floatStr, 3, 1)) * (2 ^ 8) _
        '                     + Asc(Mid(floatStr, 4, 1))

        f = 2 ^ 0
        For i = 22 To 0 Step -1
            If Mantissa And 2& ^ i Then
                f = f + 2 ^ (i - 23)
            End If
        Next i
        FloatExt = (-1) ^ s * f * 2.0! ^ (E - 127)
    End Function












    Public Overrides Function ReadMArch() As String
        If (IsBytesToRead = True) Then
            Return ""
        End If
        isMArchToDBWrite = False
        Dim ret As String
        Dim bArr(0 To 8) As Byte
        Dim temptv As Short
        temptv = tv
        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = &H220 Mod 256
        bArr(4) = &H220 \ 256
        bArr(5) = 36
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        IsmArchToRead = True
        clearMarchive(mArch)
        EraseInputQueue()

        WillCountToRead = 41
        IsBytesToRead = True
        tv = 1
        ' tim.Start()

        write(bArr, 9)

        WaitForData()

        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret + " " + "ТВ" + tv.ToString
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения мгновенного архива по ТВ1"
        End If

        bArr(3) = &H244 Mod 256
        bArr(4) = &H244 \ 256
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        IsmArchToRead = True
        WillCountToRead = 41
        IsBytesToRead = True
        tv = 2
        ' tim.Start()
        write(bArr, 9)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret + " " + "ТВ" + tv.ToString
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения мгновенного архива по ТВ2"
        End If

        bArr(3) = &HF3 Mod 256
        bArr(4) = &HF3 \ 256
        bArr(5) = 6
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        IsmArchToRead = True
        WillCountToRead = 11
        IsBytesToRead = True
        tv = 3
        ' tim.Start()
        write(bArr, 9)

        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret + "Ошибка чтения даты мгновенного архива"
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты мгновенного архива "
        End If
        tv = temptv
        isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function

    Public Overrides Function ReadTArch() As String


        Dim bArr(0 To 8) As Byte
        Dim temptv As Short
        temptv = tv
        clearTarchive(tArch)
        EraseInputQueue()
        isTArchToDBWrite = False

        '========итоговых данных блок =============
        Dim InpStrB As String
        InpStrB = ReadFlashSync(&H483E \ 64, 1)


        InpStrB = InpStrB & ReadFlashSync(&H483E \ 64 + 1, 1)
        If InpStrB <> "" Then
            InpStrB = Mid(InpStrB, (&H483E Mod 64) + 1)
            tArch.V1 = ExtLong4(Mid(InpStrB, 1 + 8 * 0, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 0 + 4, 4))
            tArch.V2 = ExtLong4(Mid(InpStrB, 1 + 8 * 1, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 1 + 4, 4))
            tArch.V3 = ExtLong4(Mid(InpStrB, 1 + 8 * 2, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 2 + 4, 4))
            tArch.M1 = ExtLong4(Mid(InpStrB, 1 + 8 * 3, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 3 + 4, 4))
            tArch.M2 = ExtLong4(Mid(InpStrB, 1 + 8 * 4, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 4 + 4, 4))
            tArch.M3 = ExtLong4(Mid(InpStrB, 1 + 8 * 5, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 5 + 4, 4))
            tArch.Q1 = ExtLong4(Mid(InpStrB, 1 + 8 * 6, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 6 + 4, 4))
            tArch.WORKTIME1 = ExtLong4(Mid(InpStrB, 1 + 8 * 7, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 7 + 4, 4))

            InpStrB = ReadRAMSync(&H29C, 8 * 4)
            If InpStrB <> "" Then
                tArch.V1 = tArch.V1 + FloatExt(Mid(InpStrB, 1, 4))
                tArch.V2 = tArch.V2 + FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V3 = tArch.V3 + FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M1 = tArch.M1 + FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M2 = tArch.M2 + FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M3 = tArch.M3 + FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q1 = tArch.Q1 + FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.WORKTIME1 = tArch.WORKTIME1 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
            End If

        End If

        InpStrB = ReadFlashSync(&H5DAC \ 64, 1)
        InpStrB = InpStrB & ReadFlashSync(&H5DAC \ 64 + 1, 1)
        If InpStrB <> "" Then
            InpStrB = Mid(InpStrB, &H5DAC Mod 64 + 1)
            tArch.V4 = ExtLong4(Mid(InpStrB, 1 + 8 * 0, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 0 + 4, 4))
            tArch.V5 = ExtLong4(Mid(InpStrB, 1 + 8 * 1, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 1 + 4, 4))
            tArch.V6 = ExtLong4(Mid(InpStrB, 1 + 8 * 2, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 2 + 4, 4))
            tArch.M4 = ExtLong4(Mid(InpStrB, 1 + 8 * 3, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 3 + 4, 4))
            tArch.M5 = ExtLong4(Mid(InpStrB, 1 + 8 * 4, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 4 + 4, 4))
            tArch.M6 = ExtLong4(Mid(InpStrB, 1 + 8 * 5, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 5 + 4, 4))
            tArch.Q2 = ExtLong4(Mid(InpStrB, 1 + 8 * 6, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 6 + 4, 4))
            tArch.WORKTIME2 = ExtLong4(Mid(InpStrB, 1 + 8 * 7, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 7 + 4, 4))

            InpStrB = ReadRAMSync(&H2D0, 8 * 4)
            If InpStrB <> "" Then
                tArch.V4 = tArch.V4 + FloatExt(Mid(InpStrB, 1, 4))
                tArch.V5 = tArch.V5 + FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V6 = tArch.V6 + FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M4 = tArch.M4 + FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M5 = tArch.M5 + FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M6 = tArch.M6 + FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q2 = tArch.Q2 + FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.WORKTIME2 = tArch.WORKTIME2 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
            End If

        End If

        InpStrB = ReadRAMSync(&HF3, 6)
        If InpStrB <> "" Then
            tArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
        End If


        isTArchToDBWrite = True
        Return "Тотальный архив прочитан"
    End Function



    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function

    Public Overrides Function ReadSystemParameters() As System.Data.DataTable
   
        TryConnect()
        EraseInputQueue()

        Dim dt As DataTable
        Dim dr As DataRow
        Dim InpStrG As String

        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        InpStrG = ""
        InpStrG = ReadFlashSync(&H200 \ 64, 3)

        If InpStrG <> "" Then


            dr = dt.NewRow
            dr("Название") = "Единицы измерений давления и тепловой энергии"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 0, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Периодичность опроса датчиков, с"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 1, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Время"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 2, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Дата"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 3, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Коррекция хода часов"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 4, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Расчетные сутки"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 5, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Расчетный час"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 6, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "День перехода на летнее время"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 7, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "День перехода на зимнее время"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 8, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Сетевой номер прибора"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 9, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = " Идентификатор прибора для внешнего устройства"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 10, 8)
            dt.Rows.Add(dr)
        End If
        InpStrG = ""
        InpStrG = ReadFlashSync(&H520 \ 64, 8)

        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG,-4+ (&H520 Mod 64) + 1)
            dr = dt.NewRow
            dr("Название") = ""
            dr("Значение") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Параметры по "
            dr("Значение") = "ТВ1"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Схема потребления"
            dr("Значение") = Mid(InpStrG, 32 + 5 + 16 * 0, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Идентификатор (код) ввода"
            dr("Значение") = Mid(InpStrG, 32 + 5 + 16 * 1, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа температуры холодной воды"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 2, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорное давление холодной воды"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 3, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Градуировка термометров"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 4, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура в трубе 1"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 5, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура в трубе 2"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 6, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура ГВС"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 7, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Использование датчиков давления"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 8, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р1"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 9, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р2"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 10, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р1"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 11, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р2"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 12, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р3"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 13, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль объемного расхода"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 14, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС1"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 15, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на G1"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 16, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на G1"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 17, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной расход в трубе 1"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 18, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС2"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 19, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на G2"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 20, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на G2"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 21, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной расход в трубе 2"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 22, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС3"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 23, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на G3"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 24, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на G3"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 25, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной расход в трубе 3"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 26, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати суточных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 27, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати месячных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 28, 8)
            dt.Rows.Add(dr)

        End If
        InpStrG = ""
        InpStrG = ReadFlashSync(&H840 \ 64, 8)

        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG,-4+ (&H840 Mod 64) + 1)
            dr = dt.NewRow
            dr("Название") = ""
            dr("Значение") = ""
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Параметры по "
            dr("Значение") = "ТВ2"
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Схема потребления"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 0, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Идентификатор (код) ввода"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 1, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа температуры холодной воды"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 2, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорное давление холодной воды"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 3, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Градуировка термометров"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 4, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура в трубе 1"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 5, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура в трубе 2"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 6, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура ГВС"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 7, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Использование датчиков давления"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 8, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р1"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 9, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р2"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 10, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р1"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 11, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р2"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 12, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р3"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 13, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль объемного расхода"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 14, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС1"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 15, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на G1"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 16, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на G1"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 17, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной расход в трубе 1"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 18, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС2"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 19, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на G2"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 20, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на G2"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 21, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной расход в трубе 2"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 22, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС3"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 23, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на G3"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 24, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на G3"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 25, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной расход в трубе 3"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 26, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати суточных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 27, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати месячных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 40 + 16 * 28, 8)
            dt.Rows.Add(dr)

        End If
        Return dt
    End Function


    

    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
    End Sub


End Class
