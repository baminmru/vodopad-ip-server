Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Class driver
    Inherits STKTVMain.TVDriver



    Private mIsConnected As Boolean



    Private Structure MArchive
        Public DateArch As DateTime
        Public HC As Long
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

        Public dt12 As Single
        Public dt45 As Single

        Public tx1 As Single
        Public tx2 As Single

        Public tair1 As Single
        Public tair2 As Single

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long


        Public archType As Short
    End Structure

    Private Structure Archive
        Public DateArch As DateTime

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

        Public SP As Long
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
        Public Q3 As Double
        Public Q4 As Double

        Public archType As Short
    End Structure

    Dim tArch As TArchive
    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

    Dim tv As Short

    Dim archType_hour = 3
    Dim archType_day = 4


    Dim Arch As Archive
    Dim mArch As MArchive

    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False

    Dim buffer(0 To 32000) As Byte
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
        Return "SPT943"
    End Function

    Private m_serverip As String

 

  

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
            EraseInputQueue()
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
            EraseInputQueue()
            WillCountToRead = 8
            IsBytesToRead = True

            write(bArr, 9)
            tv = 2
        Catch exc As Exception
        End Try
    End Sub

    Private Function TryConnect() As Boolean
        EraseInputQueue()

        Dim startBytes(0 To 30) As Byte
        Dim i As Int16


        If (IsBytesToRead = True) Then
            Return False
        End If
        For i = 0 To 30
            startBytes(i) = &H0
        Next

        'write(startBytes, 20)
        'System.Threading.Thread.Sleep(CalcInterval(20))

        For i = 0 To 20
            startBytes(i) = &HFF
        Next

        'System.Threading.Thread.Sleep(CalcInterval(20))
        'For i = 0 To 20
        '    startBytes(i) = &H0
        'Next

        'write(startBytes, 10)
        'System.Threading.Thread.Sleep(CalcInterval(10))

        Dim bArr(0 To 9) As Byte
        Try

            startBytes(20 + 0) = &H10
            startBytes(20 + 1) = &HFF
            startBytes(20 + 2) = &H3F
            startBytes(20 + 3) = &H0
            startBytes(20 + 4) = &H0
            startBytes(20 + 5) = &H0
            startBytes(20 + 6) = &H0
            startBytes(20 + 7) = 255 - ((Int(startBytes(20 + 1)) + Int(startBytes(20 + 2)) + Int(startBytes(20 + 3)) + Int(startBytes(20 + 4)) + Int(startBytes(20 + 5)) + Int(startBytes(20 + 6))) Mod 256)
            startBytes(20 + 8) = &H16
            startBytes(20 + 9) = &H0


            EraseInputQueue()
            WillCountToRead = 8
            IsBytesToRead = True

            write(startBytes, 30)
            'write(bArr, 10)

            tv = 0

            Dim sret As String
          
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


    Private m_readRAMByteCount As Short

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String


        Dim trycnt As Int32
        Dim trycnttv As Int32
        Dim tv1OK As Boolean = True
        Dim tv2OK As Boolean = True

        Try
            EraseInputQueue()
            If (IsBytesToRead = True) Then
                Return ""
            End If
            cleararchive(Arch)

            Dim bArr(0 To 8) As Byte
            Dim ret As String = ""
            Dim retsum As String = ""
            Arch.MsgHC = ""
            trycnt = 5
tryagain1:

            trycnttv = 5
tryagain1a:
            ConnectTV1()
            WaitForData()

            ret = GetAndProcessData()
            If (ret.Length > 5) Then
                If (ret.Substring(0, 6) = "Ошибка") Then
                    If trycnttv = 0 Then
                        tv1OK = False
                        trycnt = 5
                        GoTo tryagain2
                    Else
                        trycnttv -= 1
                        GoTo tryagain1a
                    End If
                End If
            End If
            If (ret.Length = 0) Then
                retsum = retsum + "Ошибка переключения на ТВ1"
                If trycnttv = 0 Then
                    tv1OK = False
                    trycnt = 5
                    GoTo tryagain2
                Else
                    trycnttv -= 1
                    GoTo tryagain1a
                End If
            End If


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

            WillCountToRead = 73
            IsBytesToRead = True


            write(bArr, 9)

        
            WaitForData()

            ret = GetAndProcessData()
            If (ret.Length > 5) Then
                If (ret.Substring(0, 6) = "Ошибка") Then
                    retsum = retsum + ret + " " + "ТВ" + tv.ToString
                    If trycnt = 0 Then
                        tv1OK = False
                        trycnt = 5
                        GoTo tryagain2
                    Else
                        trycnt -= 1
                        GoTo tryagain1
                    End If

                End If
            End If
            If (ret.Length = 0) Then
                retsum = retsum + "Ошибка чтения архива по ТВ1"

                If trycnt = 0 Then
                    tv1OK = False
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
            tv2OK = True
            ConnectTV2()
            WaitForData()

            ret = GetAndProcessData()
            If (ret.Length > 5) Then
                If (ret.Substring(0, 6) = "Ошибка") Then
                    retsum = retsum + vbCrLf + ret
                    If trycnttv = 0 Then
                        tv2OK = False
                        GoTo finalRet
                    Else
                        trycnttv -= 1
                        GoTo tryagain2a
                    End If

                End If
            End If
            If (ret.Length = 0) Then

                retsum = retsum + "Ошибка чтения архива по ТВ2"
                If trycnttv = 0 Then
                    tv2OK = False
                    GoTo finalRet
                Else
                    trycnttv -= 1
                    GoTo tryagain2a
                End If

            End If



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

            WillCountToRead = 73
            IsBytesToRead = True


            WillCountToRead = 73
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

                    If trycnt = 0 Then
                        tv2OK = False
                        GoTo finalRet

                    Else
                        trycnt -= 1
                        GoTo tryagain2
                    End If

                    retsum = retsum + vbCrLf + ret + " " + "ТВ" + tv.ToString
                Else
                    tv2OK = True
                End If

            End If
            If (ret.Length = 0) Then

                retsum = retsum + vbCrLf + "Ошибка чтения архива по ТВ2"
                tv2OK = False
            End If

finalRet:
            If tv1OK Then 'And tv2OK Then
                retsum = "Архив прочитан" & vbCrLf & retsum
                retsum = retsum & vbCrLf
                retsum = retsum & vbCrLf
                isArchToDBWrite = True
                Return retsum
            Else
                retsum = "Ошибка чтения архива" & vbCrLf & retsum
                retsum = retsum & vbCrLf
                retsum = retsum & vbCrLf

                Return retsum
            End If

        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try

    End Function
    Public Function ProcessRcvData(ByVal buf() As Byte, ByVal ret As Short) As String
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
                    Return "Ошибка!Контрольная сумма не совпала!" ', XXXXXStyle.OkOnly, "Контрольная сумма")
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
                    Return "Ошибка!Контрольная сумма не совпала!" ', XXXXXStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                Return retstring
            End If
            If (buf(2) = &H52 And IsmArchToRead = True) Then 'чтение мгновенного архива
                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                If (tv = 1 Or tv = 2) Then m_readRAMByteCount = 48
                If (tv = 3) Then m_readRAMByteCount = 6
                KC = 0
                For i = 1 To 2 + m_readRAMByteCount
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(3 + m_readRAMByteCount)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', XXXXXStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                If (tv = 1 Or tv = 2) Then
                    For i = 3 To 52
                        str = str + Chr(buf(i))
                    Next
                End If
                mArch.archType = 1
                Dim Adr As Long
                Adr = 1


                If (tv = 1) Then
                    'mArch.HCtv1 = Asc(Mid(str, Adr, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr + 2, 1))
                    mArch.HCtv1 = Asc(Mid(str, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(str, Adr + 1, 1)) * 256& + Asc(Mid(str, Adr, 1))
                    mArch.MsgHCtv1 = DeCodeHC(mArch.HCtv1)
                    mArch.SPtv1 = Asc(Mid(str, Adr + 3, 1))
                    mArch.G1 = FloatExt(Mid(str, Adr + 4, 4))
                    mArch.G2 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                    mArch.G3 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                    mArch.p1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                    mArch.p2 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                    mArch.t1 = FloatExt(Mid(str, Adr + 4 * 6, 4))
                    mArch.t2 = FloatExt(Mid(str, Adr + 4 * 7, 4))
                    mArch.dt12 = FloatExt(Mid(str, Adr + 4 * 8, 4))
                    mArch.t3 = FloatExt(Mid(str, Adr + 4 * 9, 4))
                    mArch.tx1 = FloatExt(Mid(str, Adr + 4 * 10, 4))
                    mArch.tair1 = FloatExt(Mid(str, Adr + 4 * 11, 4))

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
                    mArch.t6 = FloatExt(Mid(str, Adr + 4 * 9, 4))
                    mArch.tx2 = FloatExt(Mid(str, Adr + 4 * 10, 4))
                    mArch.tair2 = FloatExt(Mid(str, Adr + 4 * 11, 4))
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
                    Return "Ошибка!Контрольная сумма не совпала!" ', XXXXXStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                Return retstring
            End If

            If (buf(2) = &H48) Then 'часовой архив
                If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                KC = 0
                For i = 1 To 70
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(71)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', XXXXXStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 3 To 70
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1

                If (tv = 1) Then
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    'Arch.HCtv1 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.MsgHCtv1 = DeCodeHC(Arch.HCtv1)
                    Arch.SPtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.T3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.tx1 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.tair1 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 13, 4))
                    Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 14, 4))
                    Arch.Tw1 = FloatExt(Mid(hourstr, Adr + 4 * 15, 4))
                    Arch.QG1 = FloatExt(Mid(hourstr, Adr + 4 * 16, 4))
                End If

                If (tv = 2) Then
                    'Arch.HCtv2 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    Arch.MsgHCtv2 = DeCodeHC(Arch.HCtv1)
                    Arch.SPtv2 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P3 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P4 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T4 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T5 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.T6 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.tx2 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.tair2 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.v5 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.v6 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.M5 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                    Arch.M6 = FloatExt(Mid(hourstr, Adr + 4 * 13, 4))
                    Arch.Q2 = FloatExt(Mid(hourstr, Adr + 4 * 14, 4))
                    Arch.Tw2 = FloatExt(Mid(hourstr, Adr + 4 * 15, 4))
                    Arch.QG2 = FloatExt(Mid(hourstr, Adr + 4 * 16, 4))
                End If

                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "архив прочитан"
            End If
            If (buf(2) = &H59) Then 'суотчный архив
                If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                For i = 1 To 70
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = 255 - KC
                If (KC <> buf(71)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', XXXXXStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 3 To 70
                    hourstr = hourstr + Chr(buf(i))
                Next

                'hourstr = buf.ToString
                Arch.archType = archType_day
                Dim Adr As Long
                Adr = 1
                If (tv = 1) Then
                    'Arch.HCtv1 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))

                    Arch.MsgHCtv1 = DeCodeHC(Arch.HCtv1)

                    Arch.SPtv1 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P1 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P2 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T1 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T2 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.T3 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.tx1 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.tair1 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 13, 4))
                    Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 14, 4))
                    Arch.Tw1 = FloatExt(Mid(hourstr, Adr + 4 * 15, 4))
                    Arch.QG1 = FloatExt(Mid(hourstr, Adr + 4 * 16, 4))
                End If

                If (tv = 2) Then
                    'Arch.HCtv2 = Asc(Mid(hourstr, Adr, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr + 2, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + 2, 1)) * 256& ^ 2 + Asc(Mid(hourstr, Adr + 1, 1)) * 256& + Asc(Mid(hourstr, Adr, 1))
                    Arch.MsgHCtv2 = DeCodeHC(Arch.HCtv1)
                    Arch.SPtv2 = Asc(Mid(hourstr, Adr + 3, 1))
                    Arch.P3 = FloatExt(Mid(hourstr, Adr + 4, 4))
                    Arch.P4 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                    Arch.T4 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))
                    Arch.T5 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                    Arch.T6 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                    Arch.tx2 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                    Arch.tair2 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 8, 4))
                    Arch.v5 = FloatExt(Mid(hourstr, Adr + 4 * 9, 4))
                    Arch.v6 = FloatExt(Mid(hourstr, Adr + 4 * 10, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + 4 * 11, 4))
                    Arch.M5 = FloatExt(Mid(hourstr, Adr + 4 * 12, 4))
                    Arch.M6 = FloatExt(Mid(hourstr, Adr + 4 * 13, 4))
                    Arch.Q2 = FloatExt(Mid(hourstr, Adr + 4 * 14, 4))
                    Arch.Tw2 = FloatExt(Mid(hourstr, Adr + 4 * 15, 4))
                    Arch.QG2 = FloatExt(Mid(hourstr, Adr + 4 * 16, 4))
                End If

                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "Архив прочитан"
            End If
            'Return "Пакет распознан некорректно!", XXXXXStyle.OkOnly, "Ошибка")
            retstring = "Ошибка"
            Return retstring
        Catch exc As Exception
            'XXXXX(exc.Message)
        End Try
        Return "Ошибка!Пакет не распознан"
    End Function
    Public Function GetAndProcessData() As String
        Dim buf(0 To 73) As Byte
        Dim i As Int16
        For i = 0 To 73
            buf(i) = 0
        Next

        Dim ret As Long

        If (IsBytesToRead = False) Then
            Return ""
        End If

        Try

            ret = MyRead(buf, 0, WillCountToRead, CalcInterval(WillCountToRead) * 5)
            If (buf(2) = &H21) Then

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
                        For i = 0 To 73
                            buffer(i) = 0
                        Next
                        If (pagesToRead < 2) Then EraseInputQueue()
                        ispackageError = False
                        Return ProcessRcvData(buffer, bufferindex)
                    End If
                    If (pagesToRead > 1) Then
                        pagesToRead = pagesToRead - 1
                        Return ProcessRcvData(buf, ret)
                    End If

                    IsBytesToRead = False
                    EraseInputQueue()
                    Return ProcessRcvData(buf, ret)
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
    Public Function DeCodeHCNumber(ByVal CodeHC As Long, ByVal tv As Int32) As String

        DeCodeHCNumber = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCNumber = "TB" + tv.ToString + ":НС00" & ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС01" & ";"
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
                    + "TB" + tv.ToString() + ":НС08" & ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС09" & ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС10" & ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС11 " & ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС12" & ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС13" & ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС14" & ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС15" & ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС16" & ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС17 " & ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС18" & ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС19" & ";"
        End If
        If CodeHC And 2 ^ 20 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС20" & ";"
        End If
        If CodeHC And 2 ^ 21 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС21" & ";"
        End If
        If CodeHC And 2 ^ 22 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС22" & ";"
        End If
        If CodeHC And 2 ^ 23 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС23" & ";"
        End If
    End Function


    Public Function DeCodeHCRaw(ByVal CodeHC As Long) As String

        If CodeHC = 0 Then Return "-"
        DeCodeHCRaw = ""

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
        If CodeHC And 2 ^ 20 Then
            DeCodeHCRaw = DeCodeHCRaw + " 20" + ";"
        End If
        If CodeHC And 2 ^ 21 Then
            DeCodeHCRaw = DeCodeHCRaw + " 21" + ";"
        End If
        If CodeHC And 2 ^ 22 Then
            DeCodeHCRaw = DeCodeHCRaw + " 22" + ";"
        End If
        If CodeHC And 2 ^ 23 Then
            DeCodeHCRaw = DeCodeHCRaw + " 23" + ";"
        End If
    End Function

    Public Function DeCodeHCText(ByVal CodeHC As Long) As String

        DeCodeHCText = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHCText = DeCodeHCText _
                    & "Разряд батареи" & ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCText = DeCodeHCText _
                    & "Перегрузка по цепям питания датчика объема" & ";"
        End If


        If CodeHC And 2 ^ 2 Then
            DeCodeHCText = DeCodeHCText _
                    & "Изменение сигнала на дискретном входе" & ";"
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHCText = DeCodeHCText _
                    & "Tхв вне диапазона" & ";"
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHCText = DeCodeHCText _
                    & "Выход контролируемого параметра за границы диапазона Ун -Ув " & ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHCText = DeCodeHCText _
                    & "нет расшифровки  " & ";"
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHCText = DeCodeHCText _
                    & "нет расшифровки " & ";"
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHCText = DeCodeHCText _
                    & "нет расшифровки " & ";"
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHCText = DeCodeHCText _
                    & "P1 вне 0-1.1ВП1" & ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCText = DeCodeHCText _
                    & "P2 вне 0-1.1ВП1" & ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCText = DeCodeHCText _
                    & "T1 вне 0-176гр.С" & ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCText = DeCodeHCText _
                    & "T2 вне 0-176гр.С" & ";"
        End If
        If CodeHC And 2 ^ 12 Then
            DeCodeHCText = DeCodeHCText _
                    & "T3 вне 0-176гр.С" & ";"
        End If


        If CodeHC And 2 ^ 13 Then
            DeCodeHCText = DeCodeHCText _
                    & "G1>Gв1" & ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G1<Gн1" & ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCText = DeCodeHCText _
                    & "G2>Gв2" & ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G2<Gн2" & ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCText = DeCodeHCText _
                    & "G3>Gв3" & ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCText = DeCodeHCText _
                    & "0<G3<Gн3" & ";"
        End If


        If CodeHC And 2 ^ 19 Then
            DeCodeHCText = DeCodeHCText _
                    & "M1ч-М2ч  < (-HM)* M1ч" & ";"
        End If
        If CodeHC And 2 ^ 20 Then
            DeCodeHCText = DeCodeHCText _
                    & "Qч < 0 " & ";"
        End If
        If CodeHC And 2 ^ 21 Then
            DeCodeHCText = DeCodeHCText _
                    & "M1ч-М2ч  < 0" & ";"
        End If
        If CodeHC And 2 ^ 22 Then
            DeCodeHCText = DeCodeHCText _
                    & "НС22" & ";"
        End If
        If CodeHC And 2 ^ 23 Then
            DeCodeHCText = DeCodeHCText _
                    & "НС23" & ";"
        End If
    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String

        DeCodeHC = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            DeCodeHC = DeCodeHC _
                    & "HC:0 - Разряд батареи" & ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHC = DeCodeHC _
                    & "HC:1 - Перегрузка по цепям питания датчика объема" & ";"
        End If


        If CodeHC And 2 ^ 2 Then
            DeCodeHC = DeCodeHC _
                    & "HC:2 - Изменение сигнала на дисретном входе" & ";"
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHC = DeCodeHC _
                    & "HC:3 - Tхв вне диапазона" & ";"
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHC = DeCodeHC _
                    & "HC:4 - Выход контролируемого параметра за границы диапазона Ун -Ув " & ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHC = DeCodeHC _
                    & "HC:5 - нет расшифровки  " & ";"
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHC = DeCodeHC _
                    & "HC:6 - нет расшифровки " & ";"
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHC = DeCodeHC _
                    & "HC:7 - нет расшифровки " & ";"
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHC = DeCodeHC _
                    & "HC:8 - P1 вне 0-1.1ВП1" & ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHC = DeCodeHC _
                    & "HC:9 - P2 вне 0-1.1ВП1" & ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHC = DeCodeHC _
                    & "HC:10 - T1 вне 0-176гр.С" & ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHC = DeCodeHC _
                    & "HC:11 - T2 вне 0-176гр.С" & ";"
        End If
        If CodeHC And 2 ^ 12 Then
            DeCodeHC = DeCodeHC _
                    & "HC:12 - T3 вне 0-176гр.С" & ";"
        End If


        If CodeHC And 2 ^ 13 Then
            DeCodeHC = DeCodeHC _
                    & "HC:13 - G1>Gв1" & ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHC = DeCodeHC _
                    & "HC:14 - 0<G1<Gн1" & ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHC = DeCodeHC _
                    & "HC:15 - G2>Gв2" & ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHC = DeCodeHC _
                    & "HC:16 - 0<G2<Gн2" & ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHC = DeCodeHC _
                    & "HC:17 - G3>Gв3" & ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHC = DeCodeHC _
                    & "HC:18 - 0<G3<Gн3" & ";"
        End If


        If CodeHC And 2 ^ 19 Then
            DeCodeHC = DeCodeHC _
                    & "HC:19 - M1ч-М2ч  < (-HM)* M1ч" & ";"
        End If
        If CodeHC And 2 ^ 20 Then
            DeCodeHC = DeCodeHC _
                    & "HC:20 - Qч < 0 " & ";"
        End If
        If CodeHC And 2 ^ 21 Then
            DeCodeHC = DeCodeHC _
                    & "HC:21 -M1ч-М2ч  < 0" & ";"
        End If
        If CodeHC And 2 ^ 22 Then
            DeCodeHC = DeCodeHC _
                    & "HC:22" & ";"
        End If
        If CodeHC And 2 ^ 23 Then
            DeCodeHC = DeCodeHC _
                    & "HC:23" & ";"
        End If
    End Function
    Public Function FloatExt(ByVal floatStr As String) As Single
        Dim tmpStr As String = ""
        Dim E As Long
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Single
        Dim i As Long
        If floatStr = "" Then Exit Function
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
        FloatExt = ((-1) ^ s) * f * (2.0! ^ (E - 127))
    End Function
    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function
    Public Overrides Function WriteArchToDB() As String
        '!!!!!!!!!!!!!!! 943 -  xx.59.59-> xx+1.00.00 !!!!
        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,sp_TB1,sp_TB2,q1,q2,q4,q5,TSUM1,TSUM2,hc_code,hc,hc_1,hc_2,hcraw,hcraw1,hcraw2) values ("
        WriteArchToDB = WriteArchToDB + DeviceID.ToString() + ","
        WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + Arch.archType.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.T1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T3.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T4.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T5.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.T6.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.tx1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.tx2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.tair1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.tair2.ToString.Replace(",", ".") + ","
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
        WriteArchToDB = WriteArchToDB + Arch.QG1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.Q2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.QG2.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.Tw1.ToString.Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.Tw2.ToString.Replace(",", ".") + ","



        If DeCodeHCNumber(Arch.HCtv1, 1) = "" And DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            WriteArchToDB = WriteArchToDB + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(Arch.HCtv1, 1) = "" Then
            WriteArchToDB = WriteArchToDB + "'" + DeCodeHCNumber(Arch.HCtv2, 2) + "','" + S180("Счетчик: кан2:" + DeCodeHCText(Arch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            WriteArchToDB = WriteArchToDB + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(Arch.HCtv1)) + "',"
        Else
            WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCNumber(Arch.HCtv1, 1) + DeCodeHCNumber(Arch.HCtv2, 2)) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(Arch.HCtv1) + "кан2:" + DeCodeHCText(Arch.HCtv2)) + "',"
        End If

        'WriteArchToDB = WriteArchToDB + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + ";" + DeCodeHCNumber(Arch.HCtv2, 2) + "',"
        WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCText(Arch.HCtv1)) + "',"
        WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCText(Arch.HCtv2)) + "',"

        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCRaw(Arch.HC) + "'" + ","
        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCRaw(Arch.HCtv1) + "'" + ","
        WriteArchToDB = WriteArchToDB + "'" + DeCodeHCRaw(Arch.HCtv2) + "'"

        WriteArchToDB = WriteArchToDB + ")"
    End Function
    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = ""
        Try
            WriteMArchToDB = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,g1,g2,g3,g4,g5,g6,dt12,dt45,sp_TB1,sp_TB2,tce1,tce2,tair1,tair2,hc_code,hc,hc_1,hc_2,hcraw,hcraw1,hcraw2) values ("
            WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
            WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","

            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + mArch.archType.ToString + ","
            WriteMArchToDB = WriteMArchToDB + mArch.t1.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.t2.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.t3.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.t4.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.t5.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.t6.ToString.Replace(",", ".") + ","
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
            WriteMArchToDB = WriteMArchToDB + mArch.dt12.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.dt45.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.SPtv1.ToString + ","
            WriteMArchToDB = WriteMArchToDB + mArch.SPtv2.ToString + ","
            WriteMArchToDB = WriteMArchToDB + mArch.tx1.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.tx2.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.tair1.ToString.Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.tair2.ToString.Replace(",", ".") + ","



            If DeCodeHCNumber(mArch.HCtv1, 1) = "" And DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
                WriteMArchToDB = WriteMArchToDB + "'-','Нет НС',"
            ElseIf DeCodeHCNumber(mArch.HCtv1, 1) = "" Then
                WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv2, 2) + "','" + S180("Счетчик: кан2:" + DeCodeHCText(mArch.HCtv2)) + "',"
            ElseIf DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
                WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(mArch.HCtv1)) + "',"
            Else
                WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + DeCodeHCNumber(mArch.HCtv2, 2) + "','" + S180("Счетчик: кан1:" + DeCodeHCText(mArch.HCtv1) + "кан2:" + DeCodeHCText(mArch.HCtv2)) + "',"
            End If

            WriteMArchToDB = WriteMArchToDB + "'" + S180(DeCodeHCText(mArch.HCtv1)) + "',"
            WriteMArchToDB = WriteMArchToDB + "'" + S180(DeCodeHCText(mArch.HCtv2)) + "',"
            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCRaw(mArch.HC) + "'" + ","
            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCRaw(mArch.HCtv1) + "'" + ","
            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCRaw(mArch.HCtv2) + "'"
            WriteMArchToDB = WriteMArchToDB + ")"
        Catch
        End Try
        'Return WriteMArchToDB
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

        arc.SP = 0
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

        marc.dt12 = 0
        marc.dt45 = 0

        marc.tx1 = 0
        marc.tx2 = 0

        marc.tair1 = 0
        marc.tair2 = 0

        marc.SP = 0
        marc.SPtv1 = 0
        marc.SPtv2 = 0


        marc.archType = 0
        isMArchToDBWrite = False
    End Sub
    Public Overrides Function ReadMArch() As String
        If (IsBytesToRead = True) Then
            Return ""
        End If
        Dim ret As String
        Dim bArr(0 To 8) As Byte
        Dim temptv As Short
        temptv = tv
        bArr(0) = &H10
        bArr(1) = &HFF
        bArr(2) = &H52
        bArr(3) = &H200 Mod 256
        bArr(4) = &H200 \ 256
        bArr(5) = 48
        bArr(6) = &H0
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        bArr(8) = &H16
        IsmArchToRead = True
        clearMarchive(mArch)
        EraseInputQueue()

        WillCountToRead = 53
        IsBytesToRead = True
        tv = 1
        'tim.Start()

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

        bArr(3) = &H230 Mod 256
        bArr(4) = &H230 \ 256
        bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
        IsmArchToRead = True
        EraseInputQueue()
        WillCountToRead = 53
        IsBytesToRead = True
        tv = 2
        'tim.Start()
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
        EraseInputQueue()
        WillCountToRead = 11
        IsBytesToRead = True
        tv = 3
        ' tim.Start()
        write(bArr, 9)
        WaitForData()
        RaiseIdle()
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



    Public Function ReadFlashSync(ByVal fistpage As Integer, ByVal ReadPageCount As Integer) As Object
        pagesToRead = 0
        Dim bArr(0 To 8) As Byte
        Dim buf(32000) As Byte



        Try
            'If (fistpage < 0 Or fistpage > 3071) Then
            '    'XXXXX("Неверный номер первой считываемой страницы", XXXXXStyle.OkOnly, "ReadFlash")
            '    Return ""
            'End If
            If (ReadPageCount < 1 Or ReadPageCount > 64) Then
                'XXXXX("Неверное количество считываемых страниц", XXXXXStyle.OkOnly, "ReadFlash")
                Return ""
            End If
        Catch ew As Exception
            'XXXXX("Неверные параметры чтения FLASH-памяти", XXXXXStyle.OkOnly, "ReadFlash")
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

        WillCountToRead = ReadPageCount * 64

        write(bArr, 9)

        WaitForData()




        Dim T As DateTime
        Dim ret As Integer = 0
        Dim i As Integer
        T = DateTime.Now
        Try


            bufferindex = 0
            While (bufferindex < ReadPageCount * (64 + 5))
                ret = MyRead(buf, 0, ReadPageCount * (64 + 5), CalcInterval(ReadPageCount * (64 + 5)))
                For i = 0 To ret - 1
                    If bufferindex + i <= 32000 Then
                        Try
                            buffer(bufferindex + i) = buf(i)
                        Catch ex As Exception

                        End Try
                    End If


                Next
                If T.AddSeconds(90) < DateTime.Now Then
                    Return ""
                End If
                bufferindex = bufferindex + ret
            End While

        Catch ex As Exception
            'Stop
        End Try


        Dim sout As String
        'Dim bout() As Byte

        sout = ""
        'ReDim bout(0 To bufferindex)
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
        'End If

    End Function

    Public Function ReadRAMSync(ByVal fistbyte As Integer, ByVal byteCount As Integer) As Object
        Dim buf(8000) As Byte
        Dim bArr(0 To 8) As Byte
        'm_readRAMByteCount = byteCount
        Try
            'If (fistbyte < 0 Or fistbyte > 1023) Then
            '    'XXXXX("Неверный адрес первого считываемого элемента", XXXXXStyle.OkOnly, "ReadRAM")
            '    Return ""
            'End If
            If (byteCount < 1 Or byteCount > 64) Then
                'XXXXX("Неверное количество считываемых байтов", XXXXXStyle.OkOnly, "ReadRAM")
                Return ""
            End If
        Catch ew As Exception
            'XXXXX("Неверные параметры чтения ОЗУ", XXXXXStyle.OkOnly, "ReadRAM")
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

            ret = MyRead(buf, 0, byteCount + 5, CalcInterval(byteCount + 5))
            For i = 0 To ret - 1
                Try
                    buffer(bufferindex + i) = buf(i)
                Catch ex As Exception

                End Try

            Next
            If T.AddSeconds(90) < DateTime.Now Then
                Return ""
            End If
            bufferindex = bufferindex + ret
        End While
        Dim sout As String


        'Dim bout() As Byte

        sout = ""
        'ReDim bout(0 To bufferindex - 6)
        For i = 3 To bufferindex - 3
            sout = sout & Chr(buffer(i))
            'bout(i - 3) = buffer(i)
        Next
        Return sout

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
        Dim bArr(0 To 8) As Byte
        Dim temptv As Short
        temptv = tv
        clearTarchive(tArch)
        EraseInputQueue()

        '========итоговых данных блок =============
        Dim InpStrB As Object
        InpStrB = ReadFlashSync(&H424A \ 64, 1)


        InpStrB = InpStrB & ReadFlashSync(&H424A \ 64 + 1, 1)
        If InpStrB.Length <> 0 Then
            InpStrB = Mid(InpStrB, (&H424A Mod 64) + 1)
            tArch.V1 = ExtLong4(Mid(InpStrB, 1 + 8 * 0, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 0 + 4, 4))
            tArch.V2 = ExtLong4(Mid(InpStrB, 1 + 8 * 1, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 1 + 4, 4))
            tArch.V3 = ExtLong4(Mid(InpStrB, 1 + 8 * 2, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 2 + 4, 4))
            tArch.M1 = ExtLong4(Mid(InpStrB, 1 + 8 * 3, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 3 + 4, 4))
            tArch.M2 = ExtLong4(Mid(InpStrB, 1 + 8 * 4, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 4 + 4, 4))
            tArch.M3 = ExtLong4(Mid(InpStrB, 1 + 8 * 5, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 5 + 4, 4))
            tArch.Q1 = ExtLong4(Mid(InpStrB, 1 + 8 * 6, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 6 + 4, 4))
            tArch.TW1 = ExtLong4(Mid(InpStrB, 1 + 8 * 7, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 7 + 4, 4))
            tArch.Q2 = ExtLong4(Mid(InpStrB, 1 + 8 * 8, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 8 + 4, 4))

            InpStrB = ReadRAMSync(&H520, 8 * 4)
            If InpStrB.Length <> 0 Then
                tArch.V1 = tArch.V1 + FloatExt(Mid(InpStrB, 1, 4))
                tArch.V2 = tArch.V2 + FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V3 = tArch.V3 + FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M1 = tArch.M1 + FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M2 = tArch.M2 + FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M3 = tArch.M3 + FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q1 = tArch.Q1 + FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.TW1 = tArch.TW1 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
                tArch.Q2 = tArch.Q2 + FloatExt(Mid(InpStrB, 1 + 4 * 8, 4))
            Else
                Return "Ошибка чтения тотального архива "
            End If
        Else
            Return "Ошибка чтения тотального архива "

        End If

        InpStrB = ReadFlashSync(&H677B \ 64, 1)
        InpStrB = InpStrB & ReadFlashSync(&H677B \ 64 + 1, 1)
        If InpStrB.Length <> 0 Then
            InpStrB = Mid(InpStrB, &H677B Mod 64 + 1)
            tArch.V4 = ExtLong4(Mid(InpStrB, 1 + 8 * 0, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 0 + 4, 4))
            tArch.V5 = ExtLong4(Mid(InpStrB, 1 + 8 * 1, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 1 + 4, 4))
            tArch.V6 = ExtLong4(Mid(InpStrB, 1 + 8 * 2, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 2 + 4, 4))
            tArch.M4 = ExtLong4(Mid(InpStrB, 1 + 8 * 3, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 3 + 4, 4))
            tArch.M5 = ExtLong4(Mid(InpStrB, 1 + 8 * 4, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 4 + 4, 4))
            tArch.M6 = ExtLong4(Mid(InpStrB, 1 + 8 * 5, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 5 + 4, 4))
            tArch.Q3 = ExtLong4(Mid(InpStrB, 1 + 8 * 6, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 6 + 4, 4))
            tArch.TW2 = ExtLong4(Mid(InpStrB, 1 + 8 * 7, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 7 + 4, 4))
            tArch.Q4 = ExtLong4(Mid(InpStrB, 1 + 8 * 8, 4)) + FloatExt(Mid(InpStrB, 1 + 8 * 8 + 4, 4))

            InpStrB = ReadRAMSync(&H568, 8 * 4)
            If InpStrB.Length <> 0 Then
                tArch.V4 = tArch.V4 + FloatExt(Mid(InpStrB, 1, 4))
                tArch.V5 = tArch.V5 + FloatExt(Mid(InpStrB, 1 + 4 * 1, 4))
                tArch.V6 = tArch.V6 + FloatExt(Mid(InpStrB, 1 + 4 * 2, 4))
                tArch.M4 = tArch.M4 + FloatExt(Mid(InpStrB, 1 + 4 * 3, 4))
                tArch.M5 = tArch.M5 + FloatExt(Mid(InpStrB, 1 + 4 * 4, 4))
                tArch.M6 = tArch.M6 + FloatExt(Mid(InpStrB, 1 + 4 * 5, 4))
                tArch.Q3 = tArch.Q3 + FloatExt(Mid(InpStrB, 1 + 4 * 6, 4))
                tArch.TW2 = tArch.TW2 + FloatExt(Mid(InpStrB, 1 + 4 * 7, 4))
                tArch.Q4 = tArch.Q4 + FloatExt(Mid(InpStrB, 1 + 4 * 8, 4))
            Else
                Return "Ошибка чтения тотального архива "
            End If
        Else
            Return "Ошибка чтения тотального архива "
        End If

        InpStrB = ReadRAMSync(&HF3, 6)
        If InpStrB <> "" Then
            tArch.DateArch = New DateTime(buffer(3) + 2000, buffer(4), buffer(5), buffer(6), buffer(7), buffer(8))
        Else
            Return "Ошибка чтения даты тотального архива "
        End If
        isTArchToDBWrite = True

        Return "Итоговый архив прочитан"
    End Function

    Public Overrides Function WriteTArchToDB() As String
        WriteTArchToDB = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,Q1,Q2,Q4,Q5,M1,M2,M3,M4,M5,M6,v1,v2,v3,v4,v5,v6,TSUM1,TSUM2) values ("
        WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q3.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q4.ToString.Replace(",", ".") + ","
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
        InpStrG = ReadFlashSync(&H200 \ 64, 6)
   
        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG,-5+ (&H200 Mod 64) + 1)

            dr = dt.NewRow
            dr("Название") = "Единицы измерений"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 0, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Время"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 1, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Дата"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 2, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Расчетные сутки"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 3, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Расчетный час"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 4, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/выкл автоматического перехода на зимнее/летнее время"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 5, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Сетевой номер тепловычислителя"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 6, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Идентификатор тепловычислителя"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 7, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Конфигурация интерфейса"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 8, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Начало разрешенного интервала времени работы модема"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 9, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Конец разрешенного интервала времени работы модема"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 10, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа температуры холодной воды"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 11, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорное давление холодной воды"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 12, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Градуировка термометров"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 13, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль дискретного входа"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 14, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Правило формирования входного дискретного сигнала"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 15, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Распределение аппаратных ресурсов для измерения t3,tх и tв"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 16, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль уставок"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 17, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Номер текушего параметра контр уставкам УВ"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 18, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 19, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 20, 8)
            dt.Rows.Add(dr)


        End If

        InpStrG = ""
        InpStrG = ReadFlashSync(&H520 \ 64, 8)

        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG,-5+ (&H520 Mod 64) + 1)
            dr = dt.NewRow
            dr("Название") = " "
            dr("Значение") = " "
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
            dr("Название") = "Договорная температура в трубе 1"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 2, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура в трубе 2"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 3, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура ГВС"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 4, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Использование датчиков давления"
            dr("Значение") = Mid(InpStrG, 32 + 10 + 16 * 5, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р1"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 6, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р2"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 7, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р1"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 8, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р2"
            dr("Значение") = Mid(InpStrG, 32 + 15 + 16 * 9, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р3"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 10, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль объемного расхода"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 11, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС1"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 12, 8)
            dr = dt.NewRow

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на V1"
            dr("Значение") = Mid(InpStrG, 32 + 20 + 16 * 13, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на V1"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 14, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной часовой объём в трубе 1"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 15, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС2"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 16, 8)
            dr = dt.NewRow

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на V2"
            dr("Значение") = Mid(InpStrG, 32 + 25 + 16 * 17, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на V2"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 18, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной часовой объём в трубе 2"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 19, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС3"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 20, 8)
            dr = dt.NewRow

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на V3"
            dr("Значение") = Mid(InpStrG, 32 + 30 + 16 * 21, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на V3"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 22, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной часовой объём в трубе 3"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 23, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм подстановки константы Мк вместо (М1-М2)"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 24, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа М3"
            dr("Значение") = Mid(InpStrG, 32 + 35 + 16 * 25, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Уставка на небаланс масс"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 26, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм вычислений часового тепла"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 27, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константное значение часового тепла"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 28, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати суточных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 32 + 40 + 16 * 29, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати месячных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 32 + 45 + 16 * 30, 8)
            dt.Rows.Add(dr)

        End If

        InpStrG = ""
        InpStrG = ReadFlashSync(&H840 \ 64, 8)

        If InpStrG <> "" Then
            'InpStrG = Mid(InpStrG,-5+ (&H840 Mod 64) + 1)
            dr = dt.NewRow
            dr("Название") = " "
            dr("Значение") = " "
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
            dr("Название") = "Договорная температура в трубе 1"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 2, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура в трубе 2"
            dr("Значение") = Mid(InpStrG, 5 + 16 * 3, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорная температура ГВС"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 4, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Использование датчиков давления"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 5, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р1"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 6, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Верхний предел датчика Р2"
            dr("Значение") = Mid(InpStrG, 10 + 16 * 7, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р1"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 8, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р2"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 9, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа Р3"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 10, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Контроль объемного расхода"
            dr("Значение") = Mid(InpStrG, 15 + 16 * 11, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС1"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 12, 8)
            dr = dt.NewRow

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на V1"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 13, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на V1"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 14, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной часовой объём в трубе 1"
            dr("Значение") = Mid(InpStrG, 20 + 16 * 15, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС2"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 16, 8)
            dr = dt.NewRow

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на V2"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 17, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на V2"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 18, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной часовой объём в трубе 2"
            dr("Значение") = Mid(InpStrG, 25 + 16 * 19, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Цена импульса ВС3"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 20, 8)
            dr = dt.NewRow

            dr = dt.NewRow
            dr("Название") = "Верхняя уставка на V3"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 21, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Нижняя уставка на V3"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 22, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Договорной часовой объём в трубе 3"
            dr("Значение") = Mid(InpStrG, 30 + 16 * 23, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм подстановки константы Мк вместо (М1-М2)"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 24, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константа М3"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 25, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Уставка на небаланс масс"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 26, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Алгоритм вычислений часового тепла"
            dr("Значение") = Mid(InpStrG, 35 + 16 * 27, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Константное значение часового тепла"
            dr("Значение") = Mid(InpStrG, 40 + 16 * 28, 8)
            dt.Rows.Add(dr)
            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати суточных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 40 + 16 * 29, 8)
            dt.Rows.Add(dr)

            dr = dt.NewRow
            dr("Название") = "Вкл/Выкл автоматической печати месячных отчетов по вводу"
            dr("Значение") = Mid(InpStrG, 40 + 16 * 30, 8)
            dt.Rows.Add(dr)

        End If
        Return dt
    End Function

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




    Public Overrides Sub Connect()
        Dim i As Integer

        For i = 0 To 5
            If TryConnect() Then
                Return ' True
            End If
        Next
        Return 'False

    End Sub

 

    Public Sub New()

    End Sub
End Class
