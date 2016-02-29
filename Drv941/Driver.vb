Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Class Driver
    Inherits STKTVMain.TVDriver
    Private mIsConnected As Boolean

    Private Structure MArchive
        Public DateArch As DateTime
        Public HC As Long
        Public MsgНС As String

        Public HCtv1 As Long
        Public MsgНСtv1 As String

        Public HCtv2 As Long
        Public MsgНСtv2 As String

        Public G1 As Single
        Public G2 As Single
        Public G3 As Single
        Public G4 As Single
        Public G5 As Single
        Public G6 As Single

        Public t1 As Single
        Public t2 As Single
        Public t4 As Single
        Public t5 As Single

        Public p1 As Single
        Public p2 As Single
        Public p3 As Single
        Public p4 As Single

        Public dt12 As Single
        Public dt45 As Single

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long


        Public archType As Short
    End Structure

    Private Structure TArchive
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

        Public TW1 As Double
        Public TW2 As Double

        Public archType As Short
    End Structure

    Private Structure Archive
        Public DateArch As DateTime

        Public HC As Long
        Public MsgНС As String

        Public HCtv1 As Long
        Public MsgНСtv1 As String

        Public HCtv2 As Long
        Public MsgНСtv2 As String

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

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long

        Public T3 As Single
        Public T4 As Single
        Public P3 As Single
        Public P4 As Single
        Public v4 As Single
        Public v5 As Single
        Public v6 As Single
        Public M4 As Single
        Public M5 As Single
        Public M6 As Single

        Public archType As Short
    End Structure

    Dim V54 As Boolean = False
    Dim archType_hour = 3
    Dim archType_day = 4


    Dim Arch As Archive
    Dim mArch As MArchive
    Dim tArch As TArchive

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

    Public Overrides Function CounterName() As String
        Return "SPT941"
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
            ret = MyRead(buf, 0, WillCountToRead, CalcInterval(WillCountToRead))
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
                        If V54 Then
                            Return writeMessage54(buffer, bufferindex)
                        End If
                        Return writeMessage(buffer, bufferindex)
                    End If
                    If (pagesToRead > 1) Then
                        pagesToRead = pagesToRead - 1
                        If V54 Then
                            Return writeMessage54(buf, ret)
                        End If
                        Return writeMessage(buf, ret)
                    End If
                    'tim.Stop()
                    IsBytesToRead = False
                    EraseInputQueue()
                    If V54 Then
                        Return writeMessage54(buf, ret)
                    End If
                    Return writeMessage(buf, ret)
                End If
                If (ret < WillCountToRead) Then
                    For i = bufferindex To bufferindex + ret - 1
                        buffer(i) = buf(i)
                    Next
                    ispackageError = True
                    WillCountToRead = WillCountToRead - ret
                    bufferindex = bufferindex + ret - 1
                    Return "Ошибка. Пакет получен не полностью."
                End If
            End If
        Catch ex As Exception
            Return "Ошибка." + ex.Message
        End Try
        Return ""
    End Function

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

        Dim startBytes(0 To 20) As Byte
        Dim i As Int16


        If (IsBytesToRead = True) Then
            Return False
        End If
        For i = 0 To 18
            startBytes(i) = &HFF
        Next
        startBytes(19) = 0
        startBytes(20) = 0

        write(startBytes, 21)
        System.Threading.Thread.Sleep(CalcInterval(40))
        RaiseIdle()

        Dim bArr(0 To 8) As Byte
        Try

            bArr(0) = &H10
            bArr(1) = &HFF
            bArr(2) = &H3F
            bArr(3) = &H0
            bArr(4) = &H0
            bArr(5) = &H0
            bArr(6) = &H0
            bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
            bArr(8) = &H16


            EraseInputQueue()
            WillCountToRead = 8
            IsBytesToRead = True
            write(bArr, 9)


            Dim sret As String
            WaitForData()
            sret = GetAndProcessData()
            If (sret.Length > 5) Then
                If (sret.Substring(0, 6) = "Ошибка") Then
                    EraseInputQueue()
                    Return False
                End If
                If sret.IndexOf(" 54 ") >= 8 Then
                    V54 = True
                    Debug.Print("old version 941")
                End If
                If sret.IndexOf(" 92 ") >= 8 Then
                    V54 = False
                    Debug.Print("new version 941")
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
        Dim buf(8000) As Byte

        Try
            If (fistpage < 0 Or fistpage > 3071) Then
                'MsgBox("Неверный номер первой считываемой страницы", MsgBoxStyle.OkOnly, "ReadFlash")
                Return ""
            End If
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
        For i = 3 To bufferindex - 3
            sout = sout & Chr(buffer(i))
        Next
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

        write(bArr, 9)
    End Sub

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
        If V54 Then Return ReadArch54(ArchType, ArchYear, ArchMonth, ArchDay, ArchHour)
        If (IsBytesToRead = True) Then
            Return ""
        End If
        cleararchive(Arch)
        EraseInputQueue()
        Dim bArr(0 To 8) As Byte
        Dim ret As String = ""
        Dim retsum As String = ""
        Dim trycnt As Int32
        Dim tv1OK As Boolean
        Arch.MsgНС = ""
        trycnt = 5
tryagain1:

        If (ArchType = archType_hour) Then
            bArr(2) = &H48
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = ArchHour Mod 24
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)
        End If

        If (ArchType = archType_day) Then

            bArr(2) = &H59
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = &H0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)
        End If

        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        tv1OK = True

        WillCountToRead = 69
        IsBytesToRead = True


        write(bArr, 9)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = retsum + ret
                If trycnt = 0 Then
                    tv1OK = False
                    trycnt = 5
                    GoTo finalRet
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
            retsum = retsum & vbCrLf & "Ошибка чтения архива"
            tv1OK = False

        End If




finalRet:
        If tv1OK Then
            retsum = "Архив прочитан" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return retsum
        Else
            retsum = "Ошибка чтения" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        End If

    End Function

    Private Function ReadArch54(ByVal ArchType As Short, ByVal ArchYear As Short, _
  ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
        If (IsBytesToRead = True) Then
            Return ""
        End If
        cleararchive(Arch)
        EraseInputQueue()
        Dim bArr(0 To 8) As Byte
        Dim ret As String = ""
        Dim retsum As String = ""
        Dim trycnt As Int32
        Dim tv1OK As Boolean
        Arch.MsgНС = ""

        trycnt = 5
tryagain1:

        If (ArchType = archType_hour) Then
            bArr(2) = &H48
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = ArchHour Mod 24
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)
            WillCountToRead = 37
        End If

        If (ArchType = archType_day) Then

            bArr(2) = &H59
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = &H0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)
            WillCountToRead = 69
        End If

        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        tv1OK = True

        IsBytesToRead = True

        write(bArr, 9)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = retsum + ret
                If trycnt = 0 Then
                    tv1OK = False
                    trycnt = 5
                    GoTo finalRet
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
            retsum = retsum & vbCrLf & "Ошибка чтения архива"
            tv1OK = False

        End If

finalRet:
        If tv1OK Then
            retsum = "Архив прочитан" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
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
                KC = 255 - ((Int(buf(1)) + Int(buf(2)) + _
                    Int(buf(3)) + Int(buf(4)) + Int(buf(5))) Mod 256)
                retstring += vbCrLf
                If (KC <> buf(6)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
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
                'If (tv = 3) Then m_readRAMByteCount = 6
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




                Return "архив прочитан"
            End If



            If (buf(2) = &H52 And IsmArchToRead = True) Then 'чтение мгновенного архива
                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                'If (tv = 1 Or tv = 2) Then
                m_readRAMByteCount = 36
                'End If

                'If (tv = 3) Then m_readRAMByteCount = 6
                KC = 0
                For i = 1 To 2 + m_readRAMByteCount
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(3 + m_readRAMByteCount)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                'If (tv = 1 Or tv = 2) Then
                For i = 3 To 39
                    str = str + Chr(buf(i))
                Next
                'End If
                mArch.archType = 1
                Dim Adr As Long
                Adr = 1


                mArch.HCtv1 = Asc(Mid(str, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr, 1))
                mArch.MsgНСtv1 = DeCodeHC(mArch.HCtv1)
                mArch.SPtv1 = Asc(Mid(str, Adr + 3, 1))
                mArch.G1 = FloatExt(Mid(str, Adr + 4, 4))
                mArch.G2 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                mArch.G3 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                mArch.t1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                mArch.t2 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                mArch.dt12 = FloatExt(Mid(str, Adr + 4 * 6, 4))

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
                'If (tv = 0) Then Return ""
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

                Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                Arch.MsgНСtv1 = DeCodeHC(Arch.HCtv1)
                Arch.SPtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                Arch.T1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                Arch.Tw1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))

                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "архив прочитан"
            End If
            If (buf(2) = &H59) Then 'суотчный архив
                'If (tv = 0) Then Return ""
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
                'If (tv = 1) Then
                'Arch.HCtv1 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                Arch.MsgНСtv1 = DeCodeHC(Arch.HCtv1)
                Arch.SPtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                Arch.T1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                Arch.Tw1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))


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
    Private ReadG1G2 As Boolean = False

    Public Function writeMessage54(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""
        Dim KC As Long = 0
        Try

            If (buf(2) = &H3F) Then 'установка связи
                Dim i As Integer
                For i = 0 To 7
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                KC = 255 - ((Int(buf(1)) + Int(buf(2)) + _
                    Int(buf(3)) + Int(buf(4)) + Int(buf(5))) Mod 256)
                retstring += vbCrLf
                If (KC <> buf(6)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
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
                'If (tv = 3) Then m_readRAMByteCount = 6
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




                Return "архив прочитан"
            End If



            If (buf(2) = &H52 And IsmArchToRead = True) Then 'чтение мгновенного архива

                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""

                m_readRAMByteCount = 8

                KC = 0
                For i = 1 To 2 + m_readRAMByteCount
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(3 + m_readRAMByteCount)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                'If (tv = 1 Or tv = 2) Then
                For i = 3 To 11
                    str = str + Chr(buf(i))
                Next
                'End If

                mArch.archType = 1
                Dim Adr As Long
                Adr = 1

                If ReadG1G2 = False Then
                    mArch.t1 = FloatExt(Mid(str, Adr + 4 * 0, 4))
                    mArch.t2 = FloatExt(Mid(str, Adr + 4 * 1, 4))
                    'ReadG1G2 = True
                Else
                    mArch.G1 = FloatExt(Mid(str, Adr + 4 * 0, 4))
                    mArch.G2 = FloatExt(Mid(str, Adr + 4 * 1, 4))

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
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32
                KC = 0
                For i = 1 To 34
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(35)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 3 To 34
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1

                'Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                'Arch.MsgНСtv1 = DeCodeHC(Arch.HCtv1)
                Arch.SPtv1 = Asc(Mid(hourstr, Adr + 2, 1))
                Arch.HCtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                Arch.MsgНСtv1 = DeCodeHC54(Arch.HCtv1)
                Arch.T1 = FloatExt(Mid(hourstr, Adr + 4 * 1, 4))
                Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                'Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                'Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                'Arch.Tw1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))

                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "архив прочитан"
            End If
            If (buf(2) = &H59) Then 'суотчный архив
                'If (tv = 0) Then Return ""
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

                For i = 3 To 66
                    hourstr = hourstr + Chr(buf(i))
                Next

                'hourstr = buf.ToString
                Arch.archType = archType_day
                Dim Adr As Long
                Adr = 1
                'If (tv = 1) Then
                'Arch.HCtv1 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                'Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                'Arch.MsgНСtv1 = DeCodeHC54(Arch.HCtv1)
                Arch.SPtv1 = Asc(Mid(hourstr, Adr + 2, 1))
                Arch.T1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                Arch.Tw1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))


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

    Public Function DeCodeHCNumber54(ByVal CodeHC As Long, ByVal tv As Int32) As String

        DeCodeHCNumber54 = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCNumber54 = "TB" + tv.ToString + ":НС00" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":НС01" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":НС02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":НС03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":НС04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":НС05" + ";"

        End If
    End Function


    Public Function DeCodeHCRaw54(ByVal CodeHC As Long) As String
        DeCodeHCRaw54 = ""
        If CodeHC = 0 Then Return "-"
        If CodeHC And 2 ^ 0 Then
            DeCodeHCRaw54 = "00" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCRaw54 = DeCodeHCRaw54 + "01" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCRaw54 = DeCodeHCRaw54 + "02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCRaw54 = DeCodeHCRaw54 + "03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCRaw54 = DeCodeHCRaw54 + "04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCRaw54 = DeCodeHCRaw54 + "05" + ";"

        End If
    End Function

    Public Function DeCodeHCText54(ByVal CodeHC As Long) As String

        DeCodeHCText54 = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCText54 = DeCodeHCText54 _
                   & "Разряд батареи" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "T1 вне 0-176гр.С" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "T2 вне 0-176гр.С" + ";"
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHCText54 = DeCodeHCText54 _
                       & "Перегрузка питания" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "M3ч < -0.04M1" + ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "Qч < 0" + ";"
        End If
        If DeCodeHCText54 = "" Then
            DeCodeHCText54 = "Нет НС"
        End If
    End Function

    Public Function DeCodeHC54(ByVal CodeHC As Long) As String

        DeCodeHC54 = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHC54 = "НС:0 - Разряд батареи" & vbCrLf
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "НС:1 - T1 вне 0-176гр.С" & vbCrLf
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "НС:2 - T2 вне 0-176гр.С" & vbCrLf
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "НС:3 - Перегрузка питания" & vbCrLf
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "НС:4 - M3ч < -0.04M1" & vbCrLf
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "НС:5 - Qч < 0" & vbCrLf
        End If
    End Function

    Public Function DeCodeHCNumber(ByVal CodeHC As Long, ByVal tv As Int32) As String
        If V54 Then Return DeCodeHCNumber54(CodeHC, tv)
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
        If V54 Then Return DecodeHCRaw54(CodeHC)
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


    Public Function DeCodeHCText(ByVal CodeHC As Long) As String
        If V54 Then Return DeCodeHCText54(CodeHC)
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

    Public Function DeCodeHC(ByVal CodeHC As Long) As String
        If V54 Then Return DeCodeHC54(CodeHC)
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

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Public Overrides Function WriteArchToDB() As String
        WriteArchToDB = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t4,t5,p1,p2,p3,p4,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,sp_TB1,sp_TB2,q1,q2,TSUM1,TSUM2,hc_code,hc,hc_1,hc_2,hcraw,hcraw1,hcraw2) values ("
        WriteArchToDB = WriteArchToDB + DeviceID.ToString() + ","
        WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + Arch.archType.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.T1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T3.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T4.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.P1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.P2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.P3.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.P4.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.V1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.V2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.V3.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.v4.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.v5.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.v6.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.M1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.M2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.M3.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.M4.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.M5.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.M6.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv1.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv2.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.Q1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.Q2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.Tw1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.Tw2.ToString.Replace(",", ".") + ","

        If DeCodeHCNumber(Arch.HCtv1, 1) = "" And DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            WriteArchToDB = WriteArchToDB + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(Arch.HCtv1, 1) = "" Then
            WriteArchToDB = WriteArchToDB + "'" + DeCodeHCNumber(Arch.HCtv2, 2) + "','" + S180("Счетчик: кан2:" + DeCodeHCText(Arch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            WriteArchToDB = WriteArchToDB + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(Arch.HCtv1)) + "',"
        Else
            WriteArchToDB = WriteArchToDB + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + DeCodeHCNumber(Arch.HCtv2, 2) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(Arch.HCtv1) + "кан2:" + DeCodeHCText(Arch.HCtv2)) + "',"
        End If

        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCText(Arch.HCtv1) + "',"
        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCText(Arch.HCtv2) + "',"

        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCRaw(Arch.HC) + "'" + ","
        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCRaw(Arch.HCtv1) + "'" + ","
        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCRaw(Arch.HCtv2) + "'"
        WriteArchToDB = WriteArchToDB + ")"

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

    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t4,t5,p1,p2,p3,p4,g1,g2,g3,g4,g5,g6,dt12,dt45,sp_TB1,sp_TB2,hc_code,hc,hc_1,hc_2,hcraw,hcraw1,hcraw2) values ("
        WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
        WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
        WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
        WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
        WriteMArchToDB = WriteMArchToDB + mArch.archType.ToString + ","
        WriteMArchToDB = WriteMArchToDB + mArch.t1.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.t2.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.t4.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.t5.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.p1.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.p2.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.p3.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.p4.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.G1.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.G2.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.G3.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.G4.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.G5.ToString.Replace(",", ".") + ","
        WriteMArchToDB = WriteMArchToDB + mArch.G6.ToString.Replace(",", ".") + ","

        ' проблема с вычислением dt 
        WriteMArchToDB = WriteMArchToDB + "null, null,"
        'WriteMArchToDB = WriteMArchToDB + mArch.dt12.ToString.Replace(",", ".") + ","
        'WriteMArchToDB = WriteMArchToDB + mArch.dt45.ToString.Replace(",", ".") + ","


        WriteMArchToDB = WriteMArchToDB + mArch.SPtv1.ToString + ","
        WriteMArchToDB = WriteMArchToDB + mArch.SPtv2.ToString + ","
        'WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + ";" + DeCodeHCNumber(mArch.HCtv2, 2) + "',"


        If DeCodeHCNumber(mArch.HCtv1, 1) = "" And DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
            WriteMArchToDB = WriteMArchToDB + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(mArch.HCtv1, 1) = "" Then
            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv2, 2) + "','" + S180("Счетчик: кан2:" + DeCodeHCText(mArch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(mArch.HCtv1)) + "',"
        Else
            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + DeCodeHCNumber(mArch.HCtv2, 2) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(mArch.HCtv1) + "кан2:" + DeCodeHCText(mArch.HCtv2)) + "',"
        End If

        WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCText(mArch.HCtv1) + "',"
        WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCText(mArch.HCtv2) + "',"
        WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCRaw(mArch.HC) + "'" + ","
        WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCRaw(mArch.HCtv1) + "'" + ","
        WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCRaw(mArch.HCtv2) + "'"
        WriteMArchToDB = WriteMArchToDB + ")"
    End Function

    Private Sub cleararchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue

        arc.HC = 0
        arc.MsgНС = ""

        arc.HCtv1 = 0
        arc.MsgНСtv1 = ""

        arc.HCtv2 = 0
        arc.MsgНСtv2 = ""

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

        arc.SP = 0
        arc.SPtv1 = 0
        arc.SPtv2 = 0

        arc.T3 = 0
        arc.T4 = 0
        arc.P3 = 0
        arc.P4 = 0
        arc.v4 = 0
        arc.v5 = 0
        arc.v6 = 0
        arc.M4 = 0
        arc.M5 = 0
        arc.M6 = 0

        arc.archType = 0
    End Sub

    Private Sub clearMarchive(ByRef marc As MArchive)
        marc.DateArch = DateTime.MinValue
        marc.HC = 0
        marc.MsgНС = ""

        marc.HCtv1 = 0
        marc.MsgНСtv1 = ""

        marc.HCtv2 = 0
        marc.MsgНСtv2 = ""

        marc.G1 = 0
        marc.G2 = 0
        marc.G3 = 0
        marc.G4 = 0
        marc.G5 = 0
        marc.G6 = 0

        marc.t1 = 0
        marc.t2 = 0
        marc.t4 = 0
        marc.t5 = 0

        marc.p1 = 0
        marc.p2 = 0
        marc.p3 = 0
        marc.p4 = 0

        marc.dt12 = 0
        marc.dt45 = 0

        marc.SP = 0
        marc.SPtv1 = 0
        marc.SPtv2 = 0


        marc.archType = 0
    End Sub

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
    End Sub

    Private Function ReadMArch54() As String
        If (IsBytesToRead = True) Then
            Return ""
        End If
        Dim ret As String
        Dim bArr(0 To 8) As Byte
        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = &HE8 Mod 256
        bArr(4) = &HE8 \ 256
        bArr(5) = 8
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        IsmArchToRead = True
        clearMarchive(mArch)
        EraseInputQueue()

        WillCountToRead = 13
        IsBytesToRead = True

        ReadG1G2 = False
        write(bArr, 9)
        WaitForData()
        ret = GetAndProcessData()

        If (IsBytesToRead = True) Then
            Return ""
        End If


        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = &H148 Mod 256
        bArr(4) = &H148 \ 256
        bArr(5) = 8
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        IsmArchToRead = True
        'clearMarchive(mArch)
        'EraseInputQueue()

        WillCountToRead = 13
        IsBytesToRead = True

        ReadG1G2 = True
        write(bArr, 9)
        WaitForData()
        ret += GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret
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



        Dim InpStrB As String
        InpStrB = ReadRAMSync(&H64, 6)
        If InpStrB <> "" Then
            Try
                mArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
            Catch ex As Exception
                mArch.DateArch = DateTime.Now
            End Try

        End If

        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты мгновенного архива "
        End If
        m_isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function

    Public Overrides Function ReadMArch() As String
        If V54 Then Return ReadMArch54()

        If (IsBytesToRead = True) Then
            Return ""
        End If
        Dim ret As String
        Dim bArr(0 To 8) As Byte
        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = &H200 Mod 256
        bArr(4) = &H200 \ 256
        bArr(5) = 36
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        IsmArchToRead = True
        clearMarchive(mArch)
        EraseInputQueue()

        WillCountToRead = 41
        IsBytesToRead = True



        write(bArr, 9)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret
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



        Dim InpStrB As String
        InpStrB = ReadRAMSync(&HF3, 6)
        If InpStrB <> "" Then
            Try
                mArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
            Catch ex As Exception
                mArch.DateArch = DateTime.Now
            End Try

        End If

        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты мгновенного архива "
        End If
        m_isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function

    Private Function ReadTArch54() As String


        Dim bArr(0 To 8) As Byte

        clearTarchive(tArch)
        EraseInputQueue()

        '========итоговых данных блок =============
        Dim InpStrB As String
        InpStrB = ReadFlashSync(&HC3 \ 64, 1)


        InpStrB = InpStrB & ReadFlashSync(&HC3 \ 64 + 1, 1)
        If InpStrB <> "" Then

            InpStrB = ReadRAMSync(&HC3, 8 * 4)
            If InpStrB <> "" Then
                tArch.V1 = tArch.V1 + FloatExt(Mid(InpStrB, 1, 4))
                tArch.V2 = tArch.V2 + FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V3 = tArch.V3 + FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M1 = tArch.M1 + FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M2 = tArch.M2 + FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M3 = tArch.M3 + FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q1 = tArch.Q1 + FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.TW1 = tArch.TW1 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
            End If

        End If


        InpStrB = ReadRAMSync(&H64, 6) 'дата
        If InpStrB <> "" Then
            Try
                tArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
            Catch ex As Exception
                tArch.DateArch = DateTime.Now
            End Try
        End If


        m_isTArchToDBWrite = True
        Return "Тотальный архив прочитан"
    End Function

    Public Overrides Function ReadTArch() As String
        If V54 Then Return ReadTArch54()

        Dim bArr(0 To 8) As Byte

        clearTarchive(tArch)
        EraseInputQueue()

        '========итоговых данных блок =============
        Dim InpStrB As String
        InpStrB = ReadFlashSync(&H424A \ 64, 1)


        InpStrB = InpStrB & ReadFlashSync(&H424A \ 64 + 1, 1)
        If InpStrB <> "" Then
            InpStrB = Mid(InpStrB, (&H424A Mod 64) + 1)
            tArch.V1 = ExtLong4(Mid(InpStrB, 1 + 8 * 0, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 0 + 4, 4))
            tArch.V2 = ExtLong4(Mid(InpStrB, 1 + 8 * 1, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 1 + 4, 4))
            tArch.V3 = ExtLong4(Mid(InpStrB, 1 + 8 * 2, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 2 + 4, 4))
            tArch.M1 = ExtLong4(Mid(InpStrB, 1 + 8 * 3, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 3 + 4, 4))
            tArch.M2 = ExtLong4(Mid(InpStrB, 1 + 8 * 4, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 4 + 4, 4))
            tArch.M3 = ExtLong4(Mid(InpStrB, 1 + 8 * 5, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 5 + 4, 4))
            tArch.Q1 = ExtLong4(Mid(InpStrB, 1 + 8 * 6, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 6 + 4, 4))
            tArch.TW1 = ExtLong4(Mid(InpStrB, 1 + 8 * 7, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 7 + 4, 4))

            InpStrB = ReadRAMSync(&H520, 8 * 4)
            If InpStrB <> "" Then
                tArch.V1 = tArch.V1 + FloatExt(Mid(InpStrB, 1, 4))
                tArch.V2 = tArch.V2 + FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V3 = tArch.V3 + FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M1 = tArch.M1 + FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M2 = tArch.M2 + FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M3 = tArch.M3 + FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q1 = tArch.Q1 + FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.TW1 = tArch.TW1 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
            End If

        End If


        InpStrB = ReadRAMSync(&HF3, 6)
        If InpStrB <> "" Then
            Try
                tArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
            Catch ex As Exception
                tArch.DateArch = DateTime.Now
            End Try
        End If


        isTArchToDBWrite = True
        Return "Тотальный архив прочитан"
    End Function

    Public Overrides Function WriteTArchToDB() As String
        WriteTArchToDB = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,Q1,Q2,M1,M2,M3,M4,M5,M6,v1,v2,v3,v4,v5,v6,TSUM1,TSUM2) values ("
        WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M3.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M4.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M5.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M6.ToString.Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + tArch.V1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V3.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V4.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V5.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V6.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.TW1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.TW2.ToString.Replace(",", ".")
        WriteTArchToDB = WriteTArchToDB + ")"
    End Function

 

    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function

    Public Function ReadSystemParameters54() As System.Data.DataTable
        Dim dt As DataTable
        Dim dr As DataRow
        Dim InpStrS As String
        InpStrS = ""
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        InpStrS = ReadFlashSync(&H0 \ 64, 1)

        InpStrS = InpStrS & ReadFlashSync(&H0 \ 64 + 1, 1)
        If InpStrS <> "" Then
            InpStrS = Mid(InpStrS, (&H0 Mod 64) + 1)




            'float	СП Схема потребления
            dr = dt.NewRow
            dr("Название") = "Схема потребления"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 0, 4))
            dt.Rows.Add(dr)

            'float	TС Тип термопреобразователя сопротивления
            dr = dt.NewRow
            dr("Название") = "Тип термопреобразователя сопротивления"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 1, 4))
            dt.Rows.Add(dr)

            'float	C1 Цена импульса ВС1
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС1"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 2, 4))
            dt.Rows.Add(dr)

            'float	C2 Цена импульса ВС2
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС2"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 3, 4))
            dt.Rows.Add(dr)

            'float	tx Константа температуры холодной воды
            dr = dt.NewRow
            dr("Название") = "Константа температуры холодной воды"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 4, 4))
            dt.Rows.Add(dr)

            'float	t3 Константа температуры в 3 трубопроводе
            dr = dt.NewRow
            dr("Название") = "Константа температуры в 3 трубопроводе"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 5, 4))
            dt.Rows.Add(dr)

            'float	P1 Константа давления в 1-м трубопроводе
            dr = dt.NewRow
            dr("Название") = "Константа давления в 1-м трубопроводе"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 6, 4))
            dt.Rows.Add(dr)

            'float	 P2 Константа давления во 2-м трубопроводе
            dr = dt.NewRow
            dr("Название") = "Константа давления во 2-м трубопроводе"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 7, 4))
            dt.Rows.Add(dr)

            'float	P3 Константа давления в 3-м трубопроводе
            dr = dt.NewRow
            dr("Название") = "Константа давления в 3-м трубопроводе"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 8, 4))
            dt.Rows.Add(dr)

            'float	КЧ Коррекция хода часов
            dr = dt.NewRow
            dr("Название") = "Коррекция хода часов"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 9, 4))
            dt.Rows.Add(dr)

            'float	r1 Поправка на отклонение R0 для ТС1
            dr = dt.NewRow
            dr("Название") = "Поправка на отклонение R0 для ТС1"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 10, 4))
            dt.Rows.Add(dr)

            'float	r2 Поправка на отклонение R0 для ТС2
            dr = dt.NewRow
            dr("Название") = "Поправка на отклонение R0 для ТС2"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 11, 4))
            dt.Rows.Add(dr)

            'float	 w1 Поправка на отклонение W для ТС1
            dr = dt.NewRow
            dr("Название") = "Поправка на отклонение W для ТС1"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 12, 4))
            dt.Rows.Add(dr)

            'float	w2 Поправка на отклонение W для ТС2
            dr = dt.NewRow
            dr("Название") = "Поправка на отклонение W для ТС2"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 13, 4))
            dt.Rows.Add(dr)

            'bin	TО Отсчетное время
            dr = dt.NewRow
            dr("Название") = "Отсчетное время"
            dr("Значение") = "Часы: " & Asc(Mid(InpStrS, 1 + 4 * 14 + 1, 1)) & "; Минуты: " & Asc(Mid(InpStrS, 1 + 4 * 14 + 0, 1))
            dt.Rows.Add(dr)

            'bin	ДО Отсчетная календарная дата
            dr = dt.NewRow
            dr("Название") = "Отсчетная календарная дата"
            dr("Значение") = "Год: " & Asc(Mid(InpStrS, 1 + 4 * 15 + 0, 1)) & "; Месяц: " & Asc(Mid(InpStrS, 1 + 4 * 15 + 1, 1)) & "; День: " & Asc(Mid(InpStrS, 1 + 4 * 15 + 2, 1))
            dt.Rows.Add(dr)

            'bin	ДЛ Дата перехода на летнее время
            dr = dt.NewRow
            dr("Название") = "Дата перехода на летнее время"
            dr("Значение") = "Месяц: " & Asc(Mid(InpStrS, 1 + 4 * 16 + 0, 1)) & "; День: " & Asc(Mid(InpStrS, 1 + 4 * 16 + 1, 1))
            dt.Rows.Add(dr)

            'bin	ДЗ Дата перехода на зимнее время
            dr = dt.NewRow
            dr("Название") = "Дата перехода на зимнее время"
            dr("Значение") = "Месяц: " & Asc(Mid(InpStrS, 1 + 4 * 17 + 0, 1)) & "; День: " & Asc(Mid(InpStrS, 1 + 4 * 17 + 1, 1))
            dt.Rows.Add(dr)

            'float	NТ Групповой номер СПТ941
            dr = dt.NewRow
            dr("Название") = "Групповой номер СПТ941"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 18, 4))
            dt.Rows.Add(dr)

            'float	ИД Расширенный идентификатор СПТ941
            dr = dt.NewRow
            dr("Название") = "Расширенный идентификатор СПТ941"
            dr("Значение") = FloatExt(Mid(InpStrS, 1 + 4 * 19, 4))
            dt.Rows.Add(dr)

        End If
        Return dt
    End Function

    Public Overrides Function ReadSystemParameters() As System.Data.DataTable
        If V54 Then Return ReadSystemParameters54()
        'TryConnect()

        Dim dt As DataTable
        Dim dr As DataRow
        Dim InpStrG As String

        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        InpStrG = ""
        InpStrG = ReadFlashSync(&H200 \ 64, 11)

        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG, (&H200 Mod 64) + 1)

            'Каждый параметр базы данных хранится во FLASH в виде 16 – байтовой области. Формат
            'области представлен в таблице 2. Начальный адрес области хранения БД – 0x200.

            'Таблица 2 – Формат хранения параметров БД
            'Байт                Описание           Примечание
            '0 Сборка флагов состояния параметра
            '1:          Зарезервирован()
            '2:          Зарезервирован()        Системная область
            '3:          Зарезервирован()

            '4 ASCII код параметра, байт 0
            '5 ASCII код параметра, байт 1
            '6 ASCII код параметра, байт 2
            '7 ASCII код параметра, байт 3       Значение параметра в ASCII представлении
            '8 ASCII код параметра, байт 4
            '9 ASCII код параметра, байт 5
            'a ASCII код параметра, байт 6
            'b ASCII код параметра, байт 7

            'c Форматированное представление, байт 0
            'd Форматированное представление, байт 1 Значение параметра во внутреннем формате
            'e Форматированное представление, байт 2 Сборка флагов состояния параметра:
            'f Форматированное представление, байт 3



            dr = dt.NewRow
            dr("Название") = "Единицы измерений"
            dr("Значение") = SysParam(Mid(InpStrG, 5 + 16 * 0, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Схема потребления"
            dr("Значение") = SysParam(Mid(InpStrG, 5 + 16 * 1, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Время"
            dr("Значение") = SysParam(Mid(InpStrG, 5 + 16 * 2, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Дата"
            dr("Значение") = SysParam(Mid(InpStrG, 5 + 16 * 3, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Расчетные сутки"
            dr("Значение") = SysParam(Mid(InpStrG, 10 + 16 * 4, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Расчетный час"
            dr("Значение") = SysParam(Mid(InpStrG, 10 + 16 * 5, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Тип подключаемых датчиков температуры"
            dr("Значение") = SysParam(Mid(InpStrG, 10 + 16 * 6, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм контроля объемного расхода"
            dr("Значение") = SysParam(Mid(InpStrG, 10 + 16 * 7, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС1"
            dr("Значение") = SysParam(Mid(InpStrG, 15 + 16 * 8, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел измерений датчика объема V1"
            dr("Значение") = SysParam(Mid(InpStrG, 15 + 16 * 9, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижний предел измерений датчика объема V1"
            dr("Значение") = SysParam(Mid(InpStrG, 15 + 16 * 10, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа расхода по трубопроводу 1"
            dr("Значение") = SysParam(Mid(InpStrG, 15 + 16 * 11, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС2"
            dr("Значение") = SysParam(Mid(InpStrG, 20 + 16 * 12, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел измерений датчика объема V2"
            dr("Значение") = SysParam(Mid(InpStrG, 20 + 16 * 13, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижний предел измерений датчика объема V2"
            dr("Значение") = SysParam(Mid(InpStrG, 20 + 16 * 14, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа расхода по трубопроводу 2"
            dr("Значение") = SysParam(Mid(InpStrG, 20 + 16 * 15, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС3"
            dr("Значение") = SysParam(Mid(InpStrG, 25 + 16 * 16, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел измерений датчика объема V3"
            dr("Значение") = SysParam(Mid(InpStrG, 25 + 16 * 17, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижний предел измерений датчика объема V3"
            dr("Значение") = SysParam(Mid(InpStrG, 25 + 16 * 18, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа расхода по трубопроводу 3"
            dr("Значение") = SysParam(Mid(InpStrG, 25 + 16 * 19, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура по трубопроводу 1"
            dr("Значение") = SysParam(Mid(InpStrG, 30 + 16 * 20, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа давления по трубопроводу 1"
            dr("Значение") = SysParam(Mid(InpStrG, 30 + 16 * 21, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа температуры по трубопроводу 2"
            dr("Значение") = SysParam(Mid(InpStrG, 30 + 16 * 22, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа давления по трубопроводу 2"
            dr("Значение") = SysParam(Mid(InpStrG, 30 + 16 * 23, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа температуры по трубопроводу 3"
            dr("Значение") = SysParam(Mid(InpStrG, 35 + 16 * 24, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа давления по трубопроводу 3"
            dr("Значение") = SysParam(Mid(InpStrG, 35 + 16 * 25, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа температуры холодной воды"
            dr("Значение") = SysParam(Mid(InpStrG, 35 + 16 * 26, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа давления холодной воды"
            dr("Значение") = SysParam(Mid(InpStrG, 35 + 16 * 27, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм использования константы часовой массы"
            dr("Значение") = SysParam(Mid(InpStrG, 40 + 16 * 28, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа часовой массы"
            dr("Значение") = SysParam(Mid(InpStrG, 40 + 16 * 29, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Уставка на небаланс масс"
            dr("Значение") = SysParam(Mid(InpStrG, 40 + 16 * 30, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм вычислений часового тепла"
            dr("Значение") = SysParam(Mid(InpStrG, 40 + 16 * 31, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа часового тепла"
            dr("Значение") = SysParam(Mid(InpStrG, 45 + 16 * 32, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Автоматический переход на летнее/зимнее время"
            dr("Значение") = SysParam(Mid(InpStrG, 45 + 16 * 33, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль дискретного входа"
            dr("Значение") = SysParam(Mid(InpStrG, 45 + 16 * 34, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Сетевой номер прибора"
            dr("Значение") = SysParam(Mid(InpStrG, 45 + 16 * 35, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Идентификатор прибора"
            dr("Значение") = SysParam(Mid(InpStrG, 50 + 16 * 36, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Конфигурация интерфейса"
            dr("Значение") = SysParam(Mid(InpStrG, 50 + 16 * 37, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Начало разрешенного интервала времени работы модема"
            dr("Значение") = SysParam(Mid(InpStrG, 50 + 16 * 38, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Конец разрешенного интервала времени работы модема"
            dr("Значение") = SysParam(Mid(InpStrG, 50 + 16 * 39, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Автоматическая печать суточных отчетов"
            dr("Значение") = SysParam(Mid(InpStrG, 55 + 16 * 40, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Автоматическая печать месячных отчетов"
            dr("Значение") = SysParam(Mid(InpStrG, 55 + 16 * 41, 8))
            dt.Rows.Add(dr)

        End If
        Return dt
    End Function

    Private Function SysParam(ByVal ParamStr As String) As String
        'Dim str As String
        'str = Mid(ParamStr, 1, 8)

        Return ParamStr
    End Function
    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
      
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
