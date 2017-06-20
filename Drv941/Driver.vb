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

    Dim V54 As Boolean = False





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
                Return "������. ��� ������:" + Hex(buf(3))
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
                    Return "������. ����� ������� �� ���������."
                End If
            End If
        Catch ex As Exception
            Return "������." + ex.Message
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
                If (sret.Substring(0, 6) = "������") Then
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
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "������ �� ��������")
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
                'MsgBox("�������� ����� ������ ����������� ��������", MsgBoxStyle.OkOnly, "ReadFlash")
                Return ""
            End If
            If (ReadPageCount < 1 Or ReadPageCount > 64) Then
                'MsgBox("�������� ���������� ����������� �������", MsgBoxStyle.OkOnly, "ReadFlash")
                Return ""
            End If
        Catch ew As Exception
            'MsgBox("�������� ��������� ������ FLASH-������", MsgBoxStyle.OkOnly, "ReadFlash")
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
                'MsgBox("�������� ����� ������� ������������ ��������", MsgBoxStyle.OkOnly, "ReadRAM")
                Return ""
            End If
            If (byteCount < 1 Or byteCount > 64) Then
                'MsgBox("�������� ���������� ����������� ������", MsgBoxStyle.OkOnly, "ReadRAM")
                Return ""
            End If
        Catch ew As Exception
            'MsgBox("�������� ��������� ������ ���", MsgBoxStyle.OkOnly, "ReadRAM")
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
                'MsgBox("�������� ����� ������� ������������ ��������", MsgBoxStyle.OkOnly, "ReadRAM")
                Return
            End If
            If (byteCount < 1 Or byteCount > 64) Then
                'MsgBox("�������� ���������� ����������� ������", MsgBoxStyle.OkOnly, "ReadRAM")
                Return
            End If
        Catch ew As Exception
            'MsgBox("�������� ��������� ������ ���", MsgBoxStyle.OkOnly, "ReadRAM")
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

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
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
        Arch.MsgHC = ""
        trycnt = 5
tryagain1:

        If (ArchType = archType_hour) Then
            bArr(2) = &H48
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = ArchHour Mod 24
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
        End If

        If (ArchType = archType_day) Then

            bArr(2) = &H59
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = &H0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
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
            If (ret.Substring(0, 6) = "������") Then
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
            retsum = retsum & vbCrLf & "������ ������ ������"
            tv1OK = False

        End If




finalRet:
        If tv1OK Then
            retsum = "����� ��������" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return retsum
        Else
            retsum = "������ ������" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        End If

    End Function

    Private Function ReadArch54(ByVal ArchType As Short, ByVal ArchYear As Short,
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
        Arch.MsgHC = ""

        trycnt = 5
tryagain1:

        If (ArchType = archType_hour) Then
            bArr(2) = &H48
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = ArchHour Mod 24
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
            WillCountToRead = 37
        End If

        If (ArchType = archType_day) Then

            bArr(2) = &H59
            bArr(3) = ArchYear - 1900
            bArr(4) = ArchMonth Mod 13
            bArr(5) = ArchDay Mod 32
            bArr(6) = &H0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
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
            If (ret.Substring(0, 6) = "������") Then
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
            retsum = retsum & vbCrLf & "������ ������ ������"
            tv1OK = False

        End If

finalRet:
        If tv1OK Then
            retsum = "����� ��������" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return retsum
        Else
            retsum = "������ ������" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        End If

    End Function

    Public Function writeMessage(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""
        Dim KC As Long = 0
        Try

            If (buf(2) = &H3F) Then '��������� �����
                Dim i As Integer
                For i = 0 To 7
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                KC = 255 - ((Int(buf(1)) + Int(buf(2)) +
                    Int(buf(3)) + Int(buf(4)) + Int(buf(5))) Mod 256)
                retstring += vbCrLf
                If (KC <> buf(6)) Then
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If
                Return retstring
            End If
            If (buf(2) = &H45) Then '������ Flash-������
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If

                Return retstring
            End If

            If (buf(2) = &H52 And IsTArchToRead = True) Then '������ ���������� ������
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If

                tArch.archType = 2




                Return "����� ��������"
            End If



            If (buf(2) = &H52 And IsmArchToRead = True) Then '������ ����������� ������
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
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
                mArch.MsgHC_1 = DeCodeHC(mArch.HCtv1)
                mArch.SPtv1 = Asc(Mid(str, Adr + 3, 1))
                mArch.G1 = FloatExt(Mid(str, Adr + 4, 4))
                mArch.G2 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                mArch.G3 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                mArch.t1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                mArch.t2 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                mArch.dt12 = FloatExt(Mid(str, Adr + 4 * 6, 4))

                Return "����� ��������"
            End If
            If (buf(2) = &H52 And IsmArchToRead = False) Then '������ ���
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If
                Return retstring
            End If
            If (buf(2) = &H48) Then '������� �����
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32
                KC = 0
                For i = 1 To 66
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(67)) Then
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If

                For i = 3 To 64
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1

                Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                Arch.MsgHC_1 = DeCodeHC(Arch.HCtv1)
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
                Arch.WORKTIME1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))

                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "����� ��������"
            End If
            If (buf(2) = &H59) Then '�������� �����
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                For i = 1 To 66
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(67)) Then
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
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
                Arch.MsgHC_1 = DeCodeHC(Arch.HCtv1)
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
                Arch.WORKTIME1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))


                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "����� ��������"
            End If
            'MsgBox("����� ��������� �����������!", MsgBoxStyle.OkOnly, "������")
            retstring = "������"
            Return retstring
        Catch exc As Exception
        End Try
        Return "������!����� �� ���������"
    End Function
    Private ReadG1G2 As Boolean = False

    Public Function writeMessage54(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""
        Dim KC As Long = 0
        Try

            If (buf(2) = &H3F) Then '��������� �����
                Dim i As Integer
                For i = 0 To 7
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                KC = 255 - ((Int(buf(1)) + Int(buf(2)) +
                    Int(buf(3)) + Int(buf(4)) + Int(buf(5))) Mod 256)
                retstring += vbCrLf
                If (KC <> buf(6)) Then
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If
                Return retstring
            End If
            If (buf(2) = &H45) Then '������ Flash-������
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If

                Return retstring
            End If

            If (buf(2) = &H52 And IsTArchToRead = True) Then '������ ���������� ������
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If

                tArch.archType = 2




                Return "����� ��������"
            End If



            If (buf(2) = &H52 And IsmArchToRead = True) Then '������ ����������� ������

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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
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

                Return "����� ��������"
            End If
            If (buf(2) = &H52 And IsmArchToRead = False) Then '������ ���
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
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If
                Return retstring
            End If
            If (buf(2) = &H48) Then '������� �����
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32
                KC = 0
                For i = 1 To 34
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(35)) Then
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
                    'Return ""
                End If

                For i = 3 To 34
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1

                'Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                'Arch.MsgHC_1 = DeCodeHC(Arch.HCtv1)
                Arch.SPtv1 = Asc(Mid(hourstr, Adr + 2, 1))
                Arch.HCtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                Arch.MsgHC_1 = DeCodeHC54(Arch.HCtv1)
                Arch.T1 = FloatExt(Mid(hourstr, Adr + 4 * 1, 4))
                Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                'Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                'Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                'Arch.WORKTIME1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))

                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "����� ��������"
            End If
            If (buf(2) = &H59) Then '�������� �����
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                For i = 1 To 66
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(67)) Then
                    Return "������!����������� ����� �� �������!" ', MsgBoxStyle.OkOnly, "����������� �����")
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
                'Arch.MsgHC_1 = DeCodeHC54(Arch.HCtv1)
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
                Arch.WORKTIME1 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))


                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "����� ��������"
            End If
            'MsgBox("����� ��������� �����������!", MsgBoxStyle.OkOnly, "������")
            retstring = "������"
            Return retstring
        Catch exc As Exception
        End Try
        Return "������!����� �� ���������"
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
            DeCodeHCNumber54 = "TB" + tv.ToString + ":��00" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":��01" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":��02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":��03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":��04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCNumber54 = DeCodeHCNumber54 + "TB" + tv.ToString + ":��05" + ";"

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
                   & "������ �������" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "T1 ��� 0-176��.�" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "T2 ��� 0-176��.�" + ";"
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHCText54 = DeCodeHCText54 _
                       & "���������� �������" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "M3� < -0.04M1" + ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHCText54 = DeCodeHCText54 _
                    & "Q� < 0" + ";"
        End If
        If DeCodeHCText54 = "" Then
            DeCodeHCText54 = "��� ��"
        End If
    End Function

    Public Function DeCodeHC54(ByVal CodeHC As Long) As String

        DeCodeHC54 = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHC54 = "��:0 - ������ �������" & vbCrLf
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "��:1 - T1 ��� 0-176��.�" & vbCrLf
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "��:2 - T2 ��� 0-176��.�" & vbCrLf
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "��:3 - ���������� �������" & vbCrLf
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "��:4 - M3� < -0.04M1" & vbCrLf
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHC54 = DeCodeHC54 _
                    & "��:5 - Q� < 0" & vbCrLf
        End If
    End Function

    Public Overrides Function DeCodeHCNumber(ByVal CodeHC As Long, Optional ByVal tv As Int32 = 0) As String
        If V54 Then Return DeCodeHCNumber54(CodeHC, tv)
        DeCodeHCNumber = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCNumber = "TB" + tv.ToString + ":��00" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��01" + ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��05" + ";"
        End If
        If CodeHC And 2 ^ 6 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��06" + ";"
        End If
        If CodeHC And 2 ^ 7 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":��07" + ";"
        End If



        If CodeHC And 2 ^ 8 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��08" + ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��09" + ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��10" + ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��11 " + ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��12" + ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��13" + ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��14" + ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��15" + ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��16" + ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��17 " + ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��18" + ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString + ":��19" + ";"
        End If
    End Function


    Public Function DeCodeHCRaw(ByVal CodeHC As Long) As String
        If V54 Then Return DeCodeHCRaw54(CodeHC)
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
        If V54 Then Return DeCodeHCText54(CodeHC)
        DeCodeHCText = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCText = DeCodeHCText _
                   & "������ �������" + ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCText = DeCodeHCText _
                   & "���������� �������" + ";"
        End If


        If CodeHC And 2 ^ 3 Then
            DeCodeHCText = DeCodeHCText _
                    & "�������� ������� ������� �� ���������� ����� D2" & ";"
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHCText = DeCodeHCText _
                    & "������ Qp �� ������ �1 ������ ������� ������� " & ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHCText = DeCodeHCText _
                    & "������ Qp �� ������ �2 ������ ������� ������� " & ";"
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHCText = DeCodeHCText _
                    & "������ Qp �� ������ �1 �������� ������� ������� " & ";"
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHCText = DeCodeHCText _
                    & "������ Qp �� ������ �2 �������� ������� ������� " & ";"
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHCText = DeCodeHCText _
                    & "P1 ��� 0-1.1��1" + ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCText = DeCodeHCText _
                    & "P2 ��� 0-1.1��1" + ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCText = DeCodeHCText _
                    & "T1 ��� 0-176��.�" + ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCText = DeCodeHCText _
                    & "T2 ��� 0-176��.�" + ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCText = DeCodeHCText _
                    & "G1>G�1" + ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G1<G�1" + ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCText = DeCodeHCText _
                    & "G2>G�2" + ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G2>G�2" + ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCText = DeCodeHCText _
                    & "G3>G�3" + ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G3>G�3" + ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCText = DeCodeHCText _
                    & "M3� < -0.04M1" + ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCText = DeCodeHCText _
                    & "Q� < 0" + ";"
        End If
        If DeCodeHCText = "" Then
            DeCodeHCText = "��� ��"
        End If
    End Function

    Public Overrides Function DeCodeHC(ByVal CodeHC As Long) As String
        If V54 Then Return DeCodeHC54(CodeHC)
        DeCodeHC = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHC = "��:0 - ������ �������" & vbCrLf
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHC = DeCodeHC _
                    & "��:1 - ���������� �������" & vbCrLf
        End If


        If CodeHC And 2 ^ 2 Then
            DeCodeHC = DeCodeHC _
                    & "��:2 - ���������� �� ����� ������� �������� �������� (������ ��� ������ 02)" & vbCrLf
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHC = DeCodeHC _
                    & "��:3 - �������� ������� ������� �� ���������� ����� D2" & vbCrLf
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHC = DeCodeHC _
                    & "��:4 - ������ Qp �� ������ �1 ������ ������� ������� " & vbCrLf
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHC = DeCodeHC _
                    & "��:5 - ������ Qp �� ������ �2 ������ ������� ������� " & vbCrLf
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHC = DeCodeHC _
                    & "��:6 - ������ Qp �� ������ �1 �������� ������� ������� " & vbCrLf
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHC = DeCodeHC _
                    & "��:7 - ������ Qp �� ������ �2 �������� ������� ������ " & vbCrLf
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHC = DeCodeHC _
                    & "��:8 - P1 ��� 0-1.1��1" & vbCrLf
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHC = DeCodeHC _
                    & "��:9 -  - P2 ��� 0-1.1��1" & vbCrLf
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHC = DeCodeHC _
                    & "��:10 - T1 ��� 0-176��.�" & vbCrLf
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHC = DeCodeHC _
                    & "��:11 - T2 ��� 0-176��.�" & vbCrLf
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHC = DeCodeHC _
                    & "��:12 - G1>G�1" & vbCrLf
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHC = DeCodeHC _
                    & "��:13 - 0<G1<G�1" & vbCrLf
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHC = DeCodeHC _
                    & "��:14 - G2>G�2" & vbCrLf
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHC = DeCodeHC _
                    & "��:15 - 0<G2>G�2" & vbCrLf
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHC = DeCodeHC _
                    & "��:16 - G3>G�3" & vbCrLf
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHC = DeCodeHC _
                    & "��:17 - 0<G3>G�3" & vbCrLf
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHC = DeCodeHC _
                    & "��:18 - M3� < -0.04M1" & vbCrLf
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHC = DeCodeHC _
                    & "��:19 - Q� < 0" & vbCrLf
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
        '================ Float �����========================
        '��.����                                 ������� ����
        '====================================================
        '�����.������� |��.����                  ������� ����
        '----------------------------------------------------
        ' xxxx xxxx     | sxxx xxxx | xxxx xxxx | xxxx xxxx |

        ' A = (-1)^s * f * 2^(e-127)
        ' f= ����� �� 0 �� 23 a(k)*2^(-k), ��� a(k) ��� ������� � ������� k


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
            If (ret.Substring(0, 6) = "������") Then
                ret = ret
                ret = ret & vbCrLf
                ret = ret + "����� �� ��������"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "������ ������ ����������� ������ �� ��1"
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
            Return "������ ������ ���� ����������� ������ "
        End If
        m_isMArchToDBWrite = True
        Return "���������� ����� ��������"
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
            If (ret.Substring(0, 6) = "������") Then
                ret = ret
                ret = ret & vbCrLf
                ret = ret + "����� �� ��������"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "������ ������ ����������� ������ �� ��1"
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
            Return "������ ������ ���� ����������� ������ "
        End If
        m_isMArchToDBWrite = True
        Return "���������� ����� ��������"
    End Function

    Private Function ReadTArch54() As String


        Dim bArr(0 To 8) As Byte

        clearTarchive(tArch)
        EraseInputQueue()

        '========�������� ������ ���� =============
        Dim InpStrB As String
        InpStrB = ReadFlashSync(&HC3 \ 64, 1)


        InpStrB = InpStrB & ReadFlashSync(&HC3 \ 64 + 1, 1)
        If InpStrB <> "" Then

            InpStrB = ReadRAMSync(&HC3, 8 * 4)
            If InpStrB <> "" Then
                tArch.V1 = FloatExt(Mid(InpStrB, 1, 4))
                tArch.V2 = FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V3 = FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M1 = FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M2 = FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M3 = FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q1 = FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.WORKTIME1 = tArch.WORKTIME1 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
            End If

        End If


        InpStrB = ReadRAMSync(&H64, 6) '����
        If InpStrB <> "" Then
            Try
                tArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
            Catch ex As Exception
                tArch.DateArch = DateTime.Now
            End Try
        End If


        m_isTArchToDBWrite = True
        Return "��������� ����� ��������"
    End Function

    Public Overrides Function ReadTArch() As String
        If V54 Then Return ReadTArch54()

        Dim bArr(0 To 8) As Byte

        clearTarchive(tArch)
        EraseInputQueue()

        '========�������� ������ ���� =============
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
            tArch.WORKTIME1 = ExtLong4(Mid(InpStrB, 1 + 8 * 7, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 7 + 4, 4))

            InpStrB = ReadRAMSync(&H520, 8 * 4)
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


        InpStrB = ReadRAMSync(&HF3, 6)
        If InpStrB <> "" Then
            Try
                tArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
            Catch ex As Exception
                tArch.DateArch = DateTime.Now
            End Try
        End If


        isTArchToDBWrite = True
        Return "��������� ����� ��������"
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
        dt.Columns.Add("��������")
        dt.Columns.Add("��������")
        InpStrS = ReadFlashSync(&H0 \ 64, 1)

        InpStrS = InpStrS & ReadFlashSync(&H0 \ 64 + 1, 1)
        If InpStrS <> "" Then
            InpStrS = Mid(InpStrS, (&H0 Mod 64) + 1)




            'float	�� ����� �����������
            dr = dt.NewRow
            dr("��������") = "����� �����������"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 0, 4))
            dt.Rows.Add(dr)

            'float	T� ��� �������������������� �������������
            dr = dt.NewRow
            dr("��������") = "��� �������������������� �������������"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 1, 4))
            dt.Rows.Add(dr)

            'float	C1 ���� �������� ��1
            dr = dt.NewRow
            dr("��������") = "���� �������� ��1"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 2, 4))
            dt.Rows.Add(dr)

            'float	C2 ���� �������� ��2
            dr = dt.NewRow
            dr("��������") = "���� �������� ��2"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 3, 4))
            dt.Rows.Add(dr)

            'float	tx ��������� ����������� �������� ����
            dr = dt.NewRow
            dr("��������") = "��������� ����������� �������� ����"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 4, 4))
            dt.Rows.Add(dr)

            'float	t3 ��������� ����������� � 3 ������������
            dr = dt.NewRow
            dr("��������") = "��������� ����������� � 3 ������������"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 5, 4))
            dt.Rows.Add(dr)

            'float	P1 ��������� �������� � 1-� ������������
            dr = dt.NewRow
            dr("��������") = "��������� �������� � 1-� ������������"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 6, 4))
            dt.Rows.Add(dr)

            'float	 P2 ��������� �������� �� 2-� ������������
            dr = dt.NewRow
            dr("��������") = "��������� �������� �� 2-� ������������"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 7, 4))
            dt.Rows.Add(dr)

            'float	P3 ��������� �������� � 3-� ������������
            dr = dt.NewRow
            dr("��������") = "��������� �������� � 3-� ������������"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 8, 4))
            dt.Rows.Add(dr)

            'float	�� ��������� ���� �����
            dr = dt.NewRow
            dr("��������") = "��������� ���� �����"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 9, 4))
            dt.Rows.Add(dr)

            'float	r1 �������� �� ���������� R0 ��� ��1
            dr = dt.NewRow
            dr("��������") = "�������� �� ���������� R0 ��� ��1"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 10, 4))
            dt.Rows.Add(dr)

            'float	r2 �������� �� ���������� R0 ��� ��2
            dr = dt.NewRow
            dr("��������") = "�������� �� ���������� R0 ��� ��2"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 11, 4))
            dt.Rows.Add(dr)

            'float	 w1 �������� �� ���������� W ��� ��1
            dr = dt.NewRow
            dr("��������") = "�������� �� ���������� W ��� ��1"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 12, 4))
            dt.Rows.Add(dr)

            'float	w2 �������� �� ���������� W ��� ��2
            dr = dt.NewRow
            dr("��������") = "�������� �� ���������� W ��� ��2"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 13, 4))
            dt.Rows.Add(dr)

            'bin	T� ��������� �����
            dr = dt.NewRow
            dr("��������") = "��������� �����"
            dr("��������") = "����: " & Asc(Mid(InpStrS, 1 + 4 * 14 + 1, 1)) & "; ������: " & Asc(Mid(InpStrS, 1 + 4 * 14 + 0, 1))
            dt.Rows.Add(dr)

            'bin	�� ��������� ����������� ����
            dr = dt.NewRow
            dr("��������") = "��������� ����������� ����"
            dr("��������") = "���: " & Asc(Mid(InpStrS, 1 + 4 * 15 + 0, 1)) & "; �����: " & Asc(Mid(InpStrS, 1 + 4 * 15 + 1, 1)) & "; ����: " & Asc(Mid(InpStrS, 1 + 4 * 15 + 2, 1))
            dt.Rows.Add(dr)

            'bin	�� ���� �������� �� ������ �����
            dr = dt.NewRow
            dr("��������") = "���� �������� �� ������ �����"
            dr("��������") = "�����: " & Asc(Mid(InpStrS, 1 + 4 * 16 + 0, 1)) & "; ����: " & Asc(Mid(InpStrS, 1 + 4 * 16 + 1, 1))
            dt.Rows.Add(dr)

            'bin	�� ���� �������� �� ������ �����
            dr = dt.NewRow
            dr("��������") = "���� �������� �� ������ �����"
            dr("��������") = "�����: " & Asc(Mid(InpStrS, 1 + 4 * 17 + 0, 1)) & "; ����: " & Asc(Mid(InpStrS, 1 + 4 * 17 + 1, 1))
            dt.Rows.Add(dr)

            'float	N� ��������� ����� ���941
            dr = dt.NewRow
            dr("��������") = "��������� ����� ���941"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 18, 4))
            dt.Rows.Add(dr)

            'float	�� ����������� ������������� ���941
            dr = dt.NewRow
            dr("��������") = "����������� ������������� ���941"
            dr("��������") = FloatExt(Mid(InpStrS, 1 + 4 * 19, 4))
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
        dt.Columns.Add("��������")
        dt.Columns.Add("��������")
        InpStrG = ""
        InpStrG = ReadFlashSync(&H200 \ 64, 11)

        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG, (&H200 Mod 64) + 1)

            '������ �������� ���� ������ �������� �� FLASH � ���� 16 � �������� �������. ������
            '������� ����������� � ������� 2. ��������� ����� ������� �������� �� � 0x200.

            '������� 2 � ������ �������� ���������� ��
            '����                ��������           ����������
            '0 ������ ������ ��������� ���������
            '1:          ��������������()
            '2:          ��������������()        ��������� �������
            '3:          ��������������()

            '4 ASCII ��� ���������, ���� 0
            '5 ASCII ��� ���������, ���� 1
            '6 ASCII ��� ���������, ���� 2
            '7 ASCII ��� ���������, ���� 3       �������� ��������� � ASCII �������������
            '8 ASCII ��� ���������, ���� 4
            '9 ASCII ��� ���������, ���� 5
            'a ASCII ��� ���������, ���� 6
            'b ASCII ��� ���������, ���� 7

            'c ��������������� �������������, ���� 0
            'd ��������������� �������������, ���� 1 �������� ��������� �� ���������� �������
            'e ��������������� �������������, ���� 2 ������ ������ ��������� ���������:
            'f ��������������� �������������, ���� 3



            dr = dt.NewRow
            dr("��������") = "������� ���������"
            dr("��������") = SysParam(Mid(InpStrG, 5 + 16 * 0, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "����� �����������"
            dr("��������") = SysParam(Mid(InpStrG, 5 + 16 * 1, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�����"
            dr("��������") = SysParam(Mid(InpStrG, 5 + 16 * 2, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "����"
            dr("��������") = SysParam(Mid(InpStrG, 5 + 16 * 3, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� �����"
            dr("��������") = SysParam(Mid(InpStrG, 10 + 16 * 4, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ���"
            dr("��������") = SysParam(Mid(InpStrG, 10 + 16 * 5, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��� ������������ �������� �����������"
            dr("��������") = SysParam(Mid(InpStrG, 10 + 16 * 6, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������� �������� ��������� �������"
            dr("��������") = SysParam(Mid(InpStrG, 10 + 16 * 7, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "���� �������� ��1"
            dr("��������") = SysParam(Mid(InpStrG, 15 + 16 * 8, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������� ������ ��������� ������� ������ V1"
            dr("��������") = SysParam(Mid(InpStrG, 15 + 16 * 9, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������ ������ ��������� ������� ������ V1"
            dr("��������") = SysParam(Mid(InpStrG, 15 + 16 * 10, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ������� �� ������������ 1"
            dr("��������") = SysParam(Mid(InpStrG, 15 + 16 * 11, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "���� �������� ��2"
            dr("��������") = SysParam(Mid(InpStrG, 20 + 16 * 12, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������� ������ ��������� ������� ������ V2"
            dr("��������") = SysParam(Mid(InpStrG, 20 + 16 * 13, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������ ������ ��������� ������� ������ V2"
            dr("��������") = SysParam(Mid(InpStrG, 20 + 16 * 14, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ������� �� ������������ 2"
            dr("��������") = SysParam(Mid(InpStrG, 20 + 16 * 15, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "���� �������� ��3"
            dr("��������") = SysParam(Mid(InpStrG, 25 + 16 * 16, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������� ������ ��������� ������� ������ V3"
            dr("��������") = SysParam(Mid(InpStrG, 25 + 16 * 17, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������ ������ ��������� ������� ������ V3"
            dr("��������") = SysParam(Mid(InpStrG, 25 + 16 * 18, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ������� �� ������������ 3"
            dr("��������") = SysParam(Mid(InpStrG, 25 + 16 * 19, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "���������� ����������� �� ������������ 1"
            dr("��������") = SysParam(Mid(InpStrG, 30 + 16 * 20, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� �������� �� ������������ 1"
            dr("��������") = SysParam(Mid(InpStrG, 30 + 16 * 21, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ����������� �� ������������ 2"
            dr("��������") = SysParam(Mid(InpStrG, 30 + 16 * 22, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� �������� �� ������������ 2"
            dr("��������") = SysParam(Mid(InpStrG, 30 + 16 * 23, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ����������� �� ������������ 3"
            dr("��������") = SysParam(Mid(InpStrG, 35 + 16 * 24, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� �������� �� ������������ 3"
            dr("��������") = SysParam(Mid(InpStrG, 35 + 16 * 25, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ����������� �������� ����"
            dr("��������") = SysParam(Mid(InpStrG, 35 + 16 * 26, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� �������� �������� ����"
            dr("��������") = SysParam(Mid(InpStrG, 35 + 16 * 27, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������� ������������� ��������� ������� �����"
            dr("��������") = SysParam(Mid(InpStrG, 40 + 16 * 28, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� ������� �����"
            dr("��������") = SysParam(Mid(InpStrG, 40 + 16 * 29, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������� �� �������� ����"
            dr("��������") = SysParam(Mid(InpStrG, 40 + 16 * 30, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������� ���������� �������� �����"
            dr("��������") = SysParam(Mid(InpStrG, 40 + 16 * 31, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "��������� �������� �����"
            dr("��������") = SysParam(Mid(InpStrG, 45 + 16 * 32, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������������� ������� �� ������/������ �����"
            dr("��������") = SysParam(Mid(InpStrG, 45 + 16 * 33, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������� ����������� �����"
            dr("��������") = SysParam(Mid(InpStrG, 45 + 16 * 34, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������� ����� �������"
            dr("��������") = SysParam(Mid(InpStrG, 45 + 16 * 35, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������������� �������"
            dr("��������") = SysParam(Mid(InpStrG, 50 + 16 * 36, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������������ ����������"
            dr("��������") = SysParam(Mid(InpStrG, 50 + 16 * 37, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "������ ������������ ��������� ������� ������ ������"
            dr("��������") = SysParam(Mid(InpStrG, 50 + 16 * 38, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "����� ������������ ��������� ������� ������ ������"
            dr("��������") = SysParam(Mid(InpStrG, 50 + 16 * 39, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������������� ������ �������� �������"
            dr("��������") = SysParam(Mid(InpStrG, 55 + 16 * 40, 8))
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("��������") = "�������������� ������ �������� �������"
            dr("��������") = SysParam(Mid(InpStrG, 55 + 16 * 41, 8))
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
