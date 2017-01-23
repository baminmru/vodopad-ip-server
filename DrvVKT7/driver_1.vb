
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


    Public v1 As Single
    Public v2 As Single
    Public v3 As Single
    Public v4 As Single
   
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

    Public Const OPC_QUALITY_GOOD As Byte = &HC0
    Public Const OPC_QUALITY_BAD As Byte = &H0
    Public Const OPC_QUALITY_CONFIG_ERROR As Byte = &H4
    Public Const OPC_QUALITY_DEVICE_FAILURE As Byte = &HC
    Public Const OPC_QUALITY_UNCERTAIN As Byte = &H40
    Public Const OPC_QUALITY_SENSOR_CAL As Byte = &H50

    Public Const c_lng256 As Long = 256&

    Private mIsConnected As Boolean

    Private isTCP As Boolean
    Private SleepTime As Long
    Private SequenceErrorCount As Integer = 0

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
        Return "VKT7"
    End Function



    Private Function DevInit() As Boolean

        InitDigMap()

        'FF FF 00 10 3F FF 00 00 CC 63 00 00 00 64 54
        Dim Frame(15) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H10
        Frame(4) = &H3F
        Frame(5) = &HFF
        Frame(6) = &H0
        Frame(7) = &H0
        Frame(8) = &HCC
        Frame(9) = &H80 ' &h63
        Frame(10) = &H0
        Frame(11) = &H0
        Frame(12) = &H0
        ch = CheckSum(Frame, 2, 11)
        Frame(13) = ch Mod 256 '&H64
        Frame(14) = ch \ 256 '&H54

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 15)

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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    Return True
                End If


                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        Else
            DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            Return False
        End If

    End Function


    Private Function GetDotPositions() As Boolean
        RaiseIdle()
        Thread.Sleep(300)


        Dim b() As Byte = Nothing
        Dim i As Integer

        Dim sat As Integer = 5
        Dim satok As Boolean
        satok = SetArchType(VKT7ArchType.AT_Properties)
        While Not satok And sat > 0
            sat = sat - 1
            satok = SetArchType(VKT7ArchType.AT_Properties)
        End While

        If satok Then

            ' получение точности
            For i = 57 To 76
                ElemSize(i) = 1
                Dim si As Integer = 5
                Dim siok As Boolean
                siok = SelectElement(i)
                While Not siok And si > 0
                    si = si - 1
                    siok = SelectElement(i)
                End While
                If siok Then
                    Dim rdok As Boolean = False
                    Dim rdi As Integer
                    rdi = 5
                    While Not rdok And rdi > 0
                        Try
                            b = ReadData(i)
                            rdok = True
                        Catch ex As Exception
                            b = Nothing
                        End Try
                    End While


                    If Not b Is Nothing Then
                        Try
                            PropVal(i) = b(3)
                        Catch ex As Exception
                            Return False
                        End Try

                    Else
                        Return False
                    End If
                Else
                    Return False
                End If

                'If PropVal(i) > 3 Then PropVal(i) = 3

            Next
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Sub Connect()
        SleepTime = 700
        MyTransport.CleanPort()
        EraseInputQueue()
        mIsConnected = False
        Dim t As Integer
        t = 5
        While Not mIsConnected And t > 0
            Try
                If DevInit() Then
                    mIsConnected = True

                    Dim dp As Integer
                    Dim ok As Boolean
                    dp = 7
                    ok = GetDotPositions()
                    While Not ok And dp > 0
                        dp = dp - 1
                        ok = GetDotPositions()
                    End While
                    If Not ok Then
                        mIsConnected = False
                    End If

                End If
            Catch exc As Exception
                Return
            End Try
            t = t - 1
        End While


    End Sub

    Private m_readRAMByteCount As Short

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim retsum As String
        Dim AErr As String
        'AErr = "Ошибка в получении параметров: "
        AErr = ""
        'Dim ok As Boolean
        SequenceErrorCount = 0
        Try


            cleararchive(Arch)
            EraseInputQueue()


            Dim dt1 As Date
            Dim dt2 As Date
            Dim devdate As Date
            Dim checkdate As Date

            devdate = GetDeviceDate()
            If SequenceErrorCount > 5 Then GoTo ArchErr

            If ArchType = archType_hour Then
                checkdate = devdate.AddHours(-1)
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                If dt2 <= checkdate Then
                    Arch.archType = ArchType

                    dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                    dt1 = dt2
                    SetArchType(VKT7ArchType.AT_Hour)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    SetArchDate(dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    GetList()
                    If IsError Then
                        Return ErrorMessage
                    End If
                Else

                    isArchToDBWrite = False
                    Return "Часовой архив еще не сформирован"
                End If
            End If


            If ArchType = archType_day Then
                checkdate = devdate.AddDays(-1)
                If dt2 <= checkdate Then
                    Arch.archType = ArchType
                    dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                    dt2.AddHours(-1)
                    dt1 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                    dt1.AddMinutes(-1)
                    SetArchType(VKT7ArchType.AT_Day)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    SetArchDate(dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    GetList()
                    If IsError Then
                        Return ErrorMessage
                    End If
                Else
                    isArchToDBWrite = False
                    Return "Суточный архив еще не сформирован"
                End If
            End If

            Arch.DateArch = dt1
            AErr = ""


            Dim elemets() As VKT7ElemType = {VKT7ElemType.Qg_1TypeP, VKT7ElemType.Qg_2TypeP, VKT7ElemType.Qo_1TypeP, VKT7ElemType.Qo_2TypeP, _
                                             VKT7ElemType.M1_1Type, VKT7ElemType.M2_1Type, VKT7ElemType.M3_1Type, VKT7ElemType.M1_2Type, VKT7ElemType.M2_2Type, VKT7ElemType.M3_2Type, _
                                             VKT7ElemType.V1_1Type, VKT7ElemType.V2_1Type, VKT7ElemType.V3_1Type, VKT7ElemType.V1_2Type, VKT7ElemType.V2_2Type, VKT7ElemType.V3_2Type, _
                                             VKT7ElemType.t1_1Type, VKT7ElemType.t2_1Type, VKT7ElemType.t3_1Type, VKT7ElemType.t1_2Type, VKT7ElemType.t2_2Type, VKT7ElemType.t3_2Type, _
                                             VKT7ElemType.P1_1Type, VKT7ElemType.P2_1Type, VKT7ElemType.P1_2Type, VKT7ElemType.P1_2Type, _
                                             VKT7ElemType.NSPrintTypeM_1, VKT7ElemType.NSPrintTypeM_2, _
                                             VKT7ElemType.QntType_1P, VKT7ElemType.Qnt_2TypeP, VKT7ElemType.QntType_1HIP, VKT7ElemType.Qnt_2TypeHIP}
            SelectElements(elemets)
            If IsError Then

            End If

            Dim data() As Byte
            IsError = False
            IsHC = False
            CurHC = ""
            ErrorMessage = ""
            EraseInputQueue()

            Dim tryCnt = 0

            While tryCnt < 5
                data = TryGetData()
                If Not IsError Then
                    Exit While
                End If
                tryCnt += 1
                EraseInputQueue()
            End While

            '''' processing data 
            If Not data Is Nothing Then
                Dim l As Integer
                Dim i As Integer
                Dim fsz As Integer
                Dim ei As Integer
                Dim d As Single

                Dim sout As String = ""
                ei = 0
                l = data.Length
                i = 3

                For ei = 0 To ActiveElements.Length - 1
                    fsz = data(i)
                    sout = "0"
                    Dim z As ULong

                    If data(i + fsz + 3) = (OPC_QUALITY_UNCERTAIN Or OPC_QUALITY_SENSOR_CAL) Then
                        If data(i + fsz + 2) <> 0 And data(i + fsz + 2) <> 255 Then
                            IsHC = True
                            CurHC = data(i + fsz + 2).ToString
                        End If
                    End If
                    If data(i + fsz + 3) = (OPC_QUALITY_BAD Or OPC_QUALITY_DEVICE_FAILURE) Then
                        Return Single.NaN
                    End If

                    If data(i + fsz + 3) = (OPC_QUALITY_BAD Or OPC_QUALITY_CONFIG_ERROR) Then
                        Return Single.NaN
                    End If

                    If fsz = 1 Then
                        z = data(fsz + 2)
                        sout = z.ToString()
                    End If

                    Dim digs As Integer
                    digs = 0
                    If elemets(ei) >= 0 Then
                        digs = PropVal(DigMap(elemets(ei)))
                    End If
                    If fsz = 2 Then
                        z = data(i + 2 + 1) * 256 + data(i + 2)
                        sout = SetDot(z.ToString(), digs)
                    End If

                    If fsz = 4 Then
                        If elemets(ei) = VKT7ElemType.G1Type Or _
                           elemets(ei) = VKT7ElemType.G2Type Or _
                           elemets(ei) = VKT7ElemType.G3Type Or _
                           elemets(ei) = VKT7ElemType.G1_2Type Or _
                           elemets(ei) = VKT7ElemType.G2_2Type Or _
                           elemets(ei) = VKT7ElemType.G3_2Type Then

                            Dim ddd As Single
                            ddd = BToSingle(data, i + 2)
                            sout = ddd.ToString().Replace(",", ".")
                        Else


                            z = data(i + 2 + 4) * 256L * 65536L + data(i + 2 + 3) * 65536L + data(i + 2 + 2) * 256L + data(i + 2 + 1)
                            sout = SetDot(z.ToString(), digs)
                        End If
                    End If
                    d = Val(sout)


                Next

            End If



            Arch.MsgHC_1 = " "
            Arch.MsgHC_2 = " "
            Arch.MsgHC_1 = Arch.MsgHC

fin_err:
            'If AErr = "" Then
            '    retsum = "Архив прочитан"
            '    retsum = retsum & vbCrLf
            '    EraseInputQueue()
            '    isArchToDBWrite = True
            'Else
            retsum = "Ошибка: не удалось получить часть параметров " & dt2.ToString() + "->"
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = False
            'End If

            Return retsum


archErr:

            retsum = "Ошибка не удалось получить архив " & AErr
            EraseInputQueue()
            isArchToDBWrite = False
            Return retsum

        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function





    Public Function ReadArch1(ByVal ArchType As Short, ByVal ArchYear As Short, _
   ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim retsum As String
        Dim AErr As String
        'AErr = "Ошибка в получении параметров: "
        AErr = ""
        'Dim ok As Boolean
        SequenceErrorCount = 0
        Try


            cleararchive(Arch)
            EraseInputQueue()


            Dim dt1 As Date
            Dim dt2 As Date
            Dim devdate As Date
            Dim checkdate As Date

            devdate = GetDeviceDate()
            If SequenceErrorCount > 5 Then GoTo ArchErr

            If ArchType = archType_hour Then
                checkdate = devdate.AddHours(-1)
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                If dt2 <= checkdate Then
                    Arch.archType = ArchType

                    dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                    dt1 = dt2
                    SetArchType(VKT7ArchType.AT_Hour)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    SetArchDate(dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    GetList()
                    If IsError Then
                        Return ErrorMessage
                    End If
                Else

                    isArchToDBWrite = False
                    Return "Часовой архив еще не сформирован"
                End If
            End If


            If ArchType = archType_day Then
                checkdate = devdate.AddDays(-1)
                If dt2 <= checkdate Then
                    Arch.archType = ArchType
                    dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                    dt2.AddHours(-1)
                    dt1 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                    dt1.AddMinutes(-1)
                    SetArchType(VKT7ArchType.AT_Day)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    SetArchDate(dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    GetList()
                    If IsError Then
                        Return ErrorMessage
                    End If
                Else
                    isArchToDBWrite = False
                    Return "Суточный архив еще не сформирован"
                End If
            End If

            Arch.DateArch = dt1
            AErr = ""
            Arch.QG1 = GetParam(VKT7ElemType.Qg_1TypeP)
            If IsHC Then
                Arch.MsgHC += "QG1:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.Qg_1TypeP) Then AErr += "QG1;"
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.QG2 = GetParam(VKT7ElemType.Qg_2TypeP)
            If IsHC Then
                Arch.MsgHC += "QG2:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.Qg_2TypeP) Then
                    AErr += "QG2;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.M1 = GetParam(VKT7ElemType.M1_1Type)
            If IsHC Then
                Arch.MsgHC += "M1:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.M1_1Type) Then
                    AErr += "M1;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.M2 = GetParam(VKT7ElemType.M2_1Type)
            If IsHC Then
                Arch.MsgHC += "M2:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.M2_1Type) Then
                    AErr += "M2;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.M3 = GetParam(VKT7ElemType.M3_1Type)
            If IsHC Then
                Arch.MsgHC += "M3:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.M3_1Type) Then
                    AErr += "M3;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.M4 = GetParam(VKT7ElemType.M1_2Type)
            If IsHC Then
                Arch.MsgHC += "M4:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.M1_2Type) Then AErr += "M4;"
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.M5 = GetParam(VKT7ElemType.M2_2Type)
            If IsHC Then
                Arch.MsgHC += "M5:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.M2_2Type) Then
                    AErr += "M5;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.M6 = GetParam(VKT7ElemType.M3_2Type)
            If IsHC Then
                Arch.MsgHC += "M6:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.M3_2Type) Then
                    AErr += "M6;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.V1 = GetParam(VKT7ElemType.V1_1Type)
            If IsHC Then
                Arch.MsgHC += "V1:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.V1_1Type) Then
                    AErr += "V1;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.V2 = GetParam(VKT7ElemType.V2_1Type)
            If IsHC Then
                Arch.MsgHC += "V2:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.V2_1Type) Then
                    AErr += "V2;"
                    GoTo ArchErr
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.V3 = GetParam(VKT7ElemType.V3_1Type)
            If IsHC Then
                Arch.MsgHC += "V3:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.V3_1Type) Then
                    AErr += "V3;"
                    GoTo ArchErr

                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.v4 = GetParam(VKT7ElemType.V1_2Type)
            If IsHC Then
                Arch.MsgHC += "V4:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.V1_2Type) Then
                    AErr += "V4;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.v5 = GetParam(VKT7ElemType.V2_2Type)
            If IsHC Then
                Arch.MsgHC += "V5:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.V2_2Type) Then
                    AErr += "V5;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.v6 = GetParam(VKT7ElemType.V3_2Type)
            If IsHC Then
                Arch.MsgHC += "V6:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.V3_2Type) Then
                    AErr += "V6;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr


            Arch.Q1 = GetParam(VKT7ElemType.Qo_1TypeP)
            If IsHC Then
                Arch.MsgHC += "Q1:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.Qo_1TypeP) Then
                    AErr += "Q1;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.Q2 = GetParam(VKT7ElemType.Qo_2TypeP)
            If IsHC Then
                Arch.MsgHC += "Q2:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.Qo_2TypeP) Then
                    AErr += "Q2;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr


            Arch.T1 = GetParam(VKT7ElemType.t1_1Type)
            If IsHC Then
                Arch.MsgHC += "T1:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.t1_1Type) Then
                    AErr += "T1;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.T2 = GetParam(VKT7ElemType.t2_1Type)
            If IsHC Then
                Arch.MsgHC += "T2:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.t2_1Type) Then
                    AErr += "T2;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.T3 = GetParam(VKT7ElemType.t3_1Type)
            If IsHC Then
                Arch.MsgHC += "T3:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.t3_1Type) Then
                    AErr += "T3;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.T4 = GetParam(VKT7ElemType.t1_2Type)
            If IsHC Then
                Arch.MsgHC += "T4:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.t1_2Type) Then
                    AErr += "T4;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.T5 = GetParam(VKT7ElemType.t2_2Type)
            If IsHC Then
                Arch.MsgHC += "T5:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.t2_2Type) Then
                    AErr += "T5;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr


            Arch.T6 = GetParam(VKT7ElemType.t3_2Type)
            If IsHC Then
                Arch.MsgHC += "T6:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.t3_2Type) Then
                    AErr += "T6;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr


            Try
                Arch.HCtv1 = GetParam0(VKT7ElemType.NSPrintTypeM_1)
            Catch ex As Exception

            End Try

            If IsError Then
                'If VerifyElement(VKT7ElemType.NSPrintTypeM_1) Then AErr += "HCtv1;"
            End If
            Try
                Arch.HCtv2 = GetParam0(VKT7ElemType.NSPrintTypeM_2)
            Catch ex As Exception

            End Try

            If IsError Then
                If VerifyElement(VKT7ElemType.NSPrintTypeM_2) Then
                    If Not OldVersion Then
                        '       AErr += "HCtv2;"
                    Else
                        Arch.HCtv2 = 32
                    End If
                End If

            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.P1 = GetParam(VKT7ElemType.P1_1Type)
            If IsHC Then
                Arch.MsgHC += "P1:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.P1_1Type) Then
                    AErr += "P1;"
                    GoTo ArchErr
                End If
            End If

            Arch.P2 = GetParam(VKT7ElemType.P2_1Type)
            If IsHC Then
                Arch.MsgHC += "P2:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.P2_1Type) Then
                    AErr += "P2;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr


            Arch.P3 = GetParam(VKT7ElemType.P2_1Type)
            If IsHC Then
                Arch.MsgHC += "P3:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.P2_1Type) Then
                    AErr += "P3;"
                    GoTo ArchErr
                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr


            Arch.P4 = GetParam(VKT7ElemType.P2_2Type)
            If IsHC Then
                Arch.MsgHC += "P4:" + CurHC + " "
            End If
            If IsError Then
                If VerifyElement(VKT7ElemType.P2_2Type) Then
                    AErr += "P4;"
                    GoTo ArchErr

                End If
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.DateArch = dt1

            Arch.errtime1 = GetParam0(VKT7ElemType.QntType_1P)
            If IsError Then
                'AErr += "errtime1;"
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr

            Arch.errtime2 = GetParam(VKT7ElemType.Qnt_2TypeP)
            If IsError Then
                'AErr += "errtime2;"
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr
            Arch.oktime1 = GetParam0(VKT7ElemType.QntType_1HIP)
            If IsError Then
                'AErr += "oktime1;"
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr
            Arch.oktime2 = GetParam0(VKT7ElemType.Qnt_2TypeHIP)
            If IsError Then
                'AErr += "oktime2;"
            End If
            If SequenceErrorCount > 5 Then GoTo ArchErr



            'Arch.MsgHC_1 = GetParam(VKT7ElemType.QntNS_1).ToString
            'Arch.MsgHC_2 = GetParam(VKT7ElemType.QntNS_2).ToString
            Arch.MsgHC_1 = " "
            Arch.MsgHC_2 = " "
            Arch.MsgHC_1 = Arch.MsgHC


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

            retsum = "Ошибка не удалось получить архив " & AErr
            EraseInputQueue()
            isArchToDBWrite = False
            Return retsum

        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function



    Public Function DeCodeHCNumber(ByVal CodeHC As Long) As String

        Try
            If CodeHC >= 32 And CodeHC < 127 Then
                DeCodeHCNumber = Chr(CodeHC) + " "
            Else
                DeCodeHCNumber = "-"
            End If

        Catch ex As Exception
            DeCodeHCNumber = "-"
        End Try






    End Function
    Public Function DeCodeHCText(ByVal CodeHC As Long) As String
        Try
            If CodeHC >= 32 And CodeHC < 127 Then
                DeCodeHCText = Chr(CodeHC) + " "
            Else
                DeCodeHCText = "-"
            End If

        Catch ex As Exception
            DeCodeHCText = "-"
        End Try


    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String
        Try
            If CodeHC >= 32 And CodeHC < 127 Then
                DeCodeHC = Chr(CodeHC) + " "
            Else
                DeCodeHC = "-"
            End If

        Catch ex As Exception
            DeCodeHC = "-"
        End Try


    End Function

   

    Public Overrides Function WriteArchToDB() As String

        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,dm12,V1H,V2H,V5H,V4H,q1h,q2h,sp_TB1,sp_TB2,q1,q2,q4,q5,TSUM1,TSUM2,hc,hc_code,hc_1,hc_2, oktime,oktime2,errtime,errtime2,hcraw) values ("
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
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.QG1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.QG2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Tw1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Tw2, "##############0.000").Replace(",", ".") + ","

        WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCNumber(Arch.HCtv1)) + "','" + S180(Arch.MsgHC) + "',"
        WriteArchToDB = WriteArchToDB + "'" + S180(Arch.MsgHC_1) + "','" + S180(Arch.MsgHC_2) + "',"
        WriteArchToDB = WriteArchToDB + Format((Arch.oktime1), "##############0").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format((Arch.oktime2), "##############0").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.errtime1, "##############0").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Format(Arch.errtime2, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + ",'" + Arch.HCtv1.ToString().Replace(",", ".") + "'"
        WriteArchToDB = WriteArchToDB + ")"
        Debug.Print(WriteArchToDB)
    End Function

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS')"
    End Function
    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = ""
        Try
            WriteMArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,g1,g2,g3,g4,g5,g6,v1,v2,v3,v4,sp_TB1,sp_TB2,tce1,tce2,tair1,tair2,hc_code,hc,hc_1,hc_2,hcraw) values ("
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
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.v4, "##############0.000").Replace(",", ".") + ","

            'WriteMArchToDB = WriteMArchToDB + Format(mArch.t1 - mArch.t2, "##############0.000").Replace(",", ".") + "," 'mArch.dt12
            'WriteMArchToDB = WriteMArchToDB + Format(mArch.dt45, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.SPtv1.ToString + ","
            WriteMArchToDB = WriteMArchToDB + mArch.SPtv2.ToString + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tx1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tx2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair2, "##############0.000").Replace(",", ".") + ","




            WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HC) + "','" + S180(DeCodeHCNumber(mArch.HC) + " " + DeCodeHCText(mArch.HC)) + "',"

            WriteMArchToDB = WriteMArchToDB + "'" + S180(DeCodeHCNumber(mArch.HCtv1) + " " + DeCodeHCText(mArch.HCtv1)) + "',"
            WriteMArchToDB = WriteMArchToDB + "'" + S180(DeCodeHCNumber(mArch.HCtv2) + " " + DeCodeHCText(mArch.HCtv2)) + "',"

            WriteMArchToDB = WriteMArchToDB + "'" + mArch.HC.ToString().Replace(",", ".") + "'"

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
        'System.Threading.Thread.Sleep()
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









    Public Overrides Function ReadMArch() As String



        clearMarchive(mArch)
        SequenceErrorCount = 0
        SetArchType(VKT7ArchType.AT_Current)
        If IsError Then
            Return ErrorMessage
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr


        GetList()
        If IsError Then
            Return ErrorMessage
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        Dim AErr As String = ""
        Dim pVal As String
        pVal = GetParam(VKT7ElemType.G1Type)
        mArch.G1 = pVal
        If IsError Then
            AErr += "G1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.G2Type)
        mArch.G2 = pVal
        If IsError Then
            AErr += "G2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.G3Type)
        mArch.G3 = pVal
        If IsError Then
            AErr += "G3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.G1_2Type)
        mArch.G4 = pVal
        If IsError Then
            AErr += "G4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.G2_2Type)
        mArch.G5 = pVal
        If IsError Then
            AErr += "G5;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.G3_2Type)
        mArch.G6 = pVal
        If IsError Then
            AErr += "G6;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.t1_1Type)
        mArch.t1 = pVal
        If IsError Then
            AErr += "t1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.t2_1Type)
        mArch.t2 = pVal
        If IsError Then
            AErr += "t2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.t3_1Type)
        mArch.t3 = pVal
        If IsError Then
            AErr += "t3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.t1_2Type)
        mArch.t4 = pVal
        If IsError Then
            AErr += "t4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr



        pVal = GetParam(VKT7ElemType.t2_2Type)
        mArch.t5 = pVal
        If IsError Then
            AErr += "t5;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.t3_2Type)
        mArch.t6 = pVal
        If IsError Then
            AErr += "t6;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.dt_1TypeP)
        mArch.dt12 = pVal
        If IsError Then
            AErr += "dt12;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.dt_2TypeP)
        mArch.dt45 = pVal
        If IsError Then
            AErr += "dt45;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

      

        pVal = GetParam(VKT7ElemType.tswTypeP)
        mArch.tx1 = pVal
        If IsError Then
            AErr += "tx1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        Try
            pVal = GetParam0(VKT7ElemType.NSPrintTypeM_1)
            mArch.HCtv1 = Convert.ToInt32(pVal, 2)
        Catch
        End Try

        Try
            pVal = GetParam0(VKT7ElemType.NSPrintTypeM_2)
            mArch.HCtv2 = Convert.ToInt32(pVal, 2)
        Catch
        End Try


        pVal = GetParam(VKT7ElemType.P1_1Type)
        mArch.p1 = pVal
        If IsError Then
            AErr += "p1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.P2_1Type)
        mArch.p2 = pVal
        If IsError Then
            AErr += "p2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.P1_2Type)
        mArch.p3 = pVal
        If IsError Then
            AErr += "p3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.P2_2Type)
        mArch.p4 = pVal
        If IsError Then
            AErr += "p4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr


        pVal = GetParam(VKT7ElemType.P3P_Type)
        mArch.p5 = pVal
        If IsError Then
            AErr += "p5;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        ' остальные данные из текущих итогов
        SetArchType(VKT7ArchType.AT_CurItog)
        If IsError Then
            Return ErrorMessage
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        GetList()

        pVal = GetParam(VKT7ElemType.Qg_1TypeP)
        mArch.dQ1 = pVal
        If IsError Then
            AErr += "dQ1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.Qg_2TypeP)
        mArch.dQ2 = pVal
        If IsError Then
            AErr += "dQ2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr


        mArch.v1 = GetParam(VKT7ElemType.V1_1Type)
        If IsError Then
            If VerifyElement(VKT7ElemType.V1_1Type) Then AErr += "V1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        mArch.v2 = GetParam(VKT7ElemType.V2_1Type)
        If IsError Then
            If VerifyElement(VKT7ElemType.V2_1Type) Then AErr += "V2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        mArch.v3 = GetParam(VKT7ElemType.V1_2Type)
        If IsError Then
            If VerifyElement(VKT7ElemType.V1_2Type) Then AErr += "V3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        mArch.v4 = GetParam(VKT7ElemType.V2_2Type)
        If IsError Then
            If VerifyElement(VKT7ElemType.V2_2Type) Then AErr += "V4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr



        mArch.DateArch = GetDeviceDate()


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
            isMArchToDBWrite = True
            Return retsum
        End If

ArchErr:
        
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
        Dim AErr As String = ""
        clearTarchive(tArch)
        EraseInputQueue()
        SequenceErrorCount = 0

        SetArchType(VKT7ArchType.AT_CurItog)
        If IsError Then
            Return ErrorMessage
        End If

        GetList()
        If IsError Then
            Return ErrorMessage
        End If

        Dim pVal As String
        pVal = GetParam(VKT7ElemType.M1_1Type)
        tArch.M1 = pVal
        If IsError Then
            AErr += "M1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.M2_1Type)
        tArch.M2 = pVal
        If IsError Then
            AErr += "M2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.M3_1Type)
        tArch.M3 = pVal
        If IsError Then
            AErr += "M3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.M1_2Type)
        tArch.M4 = pVal
        If IsError Then
            AErr += "M4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.M2_2Type)
        tArch.M5 = pVal
        If IsError Then
            AErr += "M5;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.M3_2Type)
        tArch.M6 = pVal
        If IsError Then
            AErr += "M6;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr


        pVal = GetParam(VKT7ElemType.V1_1Type)
        tArch.V1 = pVal
        If IsError Then
            AErr += "v1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr


        pVal = GetParam(VKT7ElemType.V2_1Type)
        tArch.V2 = pVal
        If IsError Then
            AErr += "v2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.V3_1Type)
        tArch.V3 = pVal
        If IsError Then
            AErr += "v3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.V1_2Type)
        tArch.V4 = pVal
        If IsError Then
            AErr += "v4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.V2_2Type)
        tArch.V5 = pVal
        If IsError Then
            AErr += "v5;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.V3_2Type)
        tArch.V6 = pVal
        If IsError Then
            AErr += "v6;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr




        pVal = GetParam(VKT7ElemType.Qo_1TypeP)
        tArch.Q1 = pVal
        If IsError Then
            AErr += "Q1;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.Qo_2TypeP)
        tArch.Q2 = pVal
        If IsError Then
            AErr += "Q2;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.Qg_1TypeP)
        tArch.Q3 = pVal
        If IsError Then
            AErr += "Q3;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        pVal = GetParam(VKT7ElemType.Qg_2TypeP)
        tArch.Q4 = pVal
        If IsError Then
            AErr += "Q4;"
        End If
        If SequenceErrorCount > 5 Then GoTo ArchErr

        Try
            tArch.errtime1 = GetParam0(VKT7ElemType.QntType_1P)
            tArch.errtime2 = GetParam0(VKT7ElemType.Qnt_2TypeP)
            tArch.oktime1 = GetParam0(VKT7ElemType.QntType_1HIP)
            tArch.oktime2 = GetParam0(VKT7ElemType.Qnt_2TypeHIP)
        Catch ex As Exception

        End Try


        If SequenceErrorCount > 5 Then GoTo ArchErr




        tArch.DateArch = GetDeviceDate()

        If SequenceErrorCount > 5 Then GoTo ArchErr




        Dim retsum As String
        retsum = "Итоговый архив прочитан"
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
        WriteTArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1H,Q2H,Q4,Q5,M1,M2,M3,M4,M5,M6,v1h,v2h,v3,v4h,v5h,v6,TSUM1,TSUM2,oktime,oktime2,ERRTIME,errtime2) values ("
        WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q4, "##############0.000").Replace(",", ".") + ","
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


        '0x00 0x03 0x3f 0xf9 0x00 0x00 0x98 0x3e
        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HF9
        Frame(6) = &H0
        Frame(7) = &H0
        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256 '&H98
        Frame(9) = ch \ 256 '&H3E

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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt
                If VerifySumm(b, 0, sz) Then
                    MyTransport.CleanPort()
                    GoTo filldata
                End If
                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        End If
        Return dt
filldata:
        dr = dt.NewRow
        dr("Название") = "Номер версии ПО (формат Х.Х)"
        dr("Значение") = ((b(3) And &HF0) / 16).ToString() & "." & (b(3) And &HF).ToString()
        dt.Rows.Add(dr)


        dr = dt.NewRow
        dr("Название") = "Схема измерения по ТВ1×2"
        dr("Значение") = ((b(5) And &H1E) / 2).ToString()  ' & " " & ((b(5) And &H1E) / 2).ToString()
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Схема измерения по ТВ2×2"
        dr("Значение") = ((b(7) And &H1E) / 2).ToString() ' & " " & ((b(7) And &H1E) / 2).ToString()
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Идентификатор абонента"
        dr("Значение") = Chr(b(8)) + Chr(b(9)) + Chr(b(10)) + Chr(b(11)) + Chr(b(12)) + Chr(b(13)) + Chr(b(14)) + Chr(b(15))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Сетевой номер прибора"
        dr("Значение") = BCD(b(16))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Дата отчета"
        dr("Значение") = BCD(b(17))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Модель исполнения"
        dr("Значение") = BCD(b(18))
        dt.Rows.Add(dr)

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




    ''''''''''''''''''''''''''''''' VKT7 specific '''''''''''''''''
    Private Enum VKT7ElemType
        t1_1Type = 0   't1 Тв1 параметр                                                    
        t2_1Type = 1   't2 Тв1 параметр                                                    
        t3_1Type = 2   't3 Тв1 параметр                                                    
        V1_1Type = 3   'V1 Тв1 параметр                                                    
        V2_1Type = 4   'V2 Тв1 параметр                                                    
        V3_1Type = 5   'V3 Тв1 параметр                                                    
        M1_1Type = 6   'M1 Тв1 параметр                                                    
        M2_1Type = 7   'M2 Тв1 параметр                                                    
        M3_1Type = 8   'M3 Тв1 параметр                                                    
        P1_1Type = 9   'P1 Тв1 параметр                                                    
        P2_1Type = 10  'P2 Тв1 параметр                                                    
        Mg_1TypeP = 11  'Mг Тв1 параметр                                                    
        Qo_1TypeP = 12  'Qо Тв1 параметр                                                    
        Qg_1TypeP = 13  'Qг Тв1 параметр                                                    
        dt_1TypeP = 14  'dt Тв1 параметр                                                    
        tswTypeP = 15  'tх параметр                                                        
        taTypeP = 16  'ta параметр                                                        
        QntType_1HIP = 17  'BНP Тв1 параметр                                                   
        QntType_1P = 18  'BOC Тв1 параметр                                                   
        G1Type = 19  'G1 Тв1 параметр                                                    
        G2Type = 20  'G2 Тв1 параметр                                                    
        G3Type = 21  'G3 Тв1 параметр                                                    
        t1_2Type = 22  't1 Тв2 параметр                                                    
        t2_2Type = 23  't2 Тв2 параметр                                                    
        t3_2Type = 24  't3 Тв2 параметр                                                    
        V1_2Type = 25  'V1 Тв2 параметр                                                    
        V2_2Type = 26  'V2 Тв2 параметр                                                    
        V3_2Type = 27  'V3 Тв2 параметр                                                    
        M1_2Type = 28  'M1 Тв2 параметр                                                    
        M2_2Type = 29  'M2 Тв2 параметр                                                    
        M3_2Type = 30  'M3 Тв2 параметр                                                    
        P1_2Type = 31  'P1 Тв2 параметр                                                    
        P2_2Type = 32  'P2 Тв2 параметр                                                    
        Mg_2TypeP = 33  'Mг Тв2 параметр                                                    
        Qo_2TypeP = 34  'Qо Тв2 параметр                                                    
        Qg_2TypeP = 35  'Qг Тв2 параметр                                                    
        dt_2TypeP = 36  'dt Тв2 параметр                                                    
        tsw_2TypeP = 37  'резерв параметр                                                    
        ta_2TypeP = 38  'резерв параметр                                                    
        Qnt_2TypeHIP = 39  'BНP Тв2 параметр                                                   
        Qnt_2TypeP = 40  'BOC Тв2 параметр                                                   
        G1_2Type = 41  'G1 Тв2 параметр                                                    
        G2_2Type = 42  'G2 Тв2 параметр                                                    
        G3_2Type = 43  'G3 Тв2 параметр                                                    
        tTypeM = 44  'ед. измерения по t (температуре) свойство                          
        GTypeM = 45  'ед. измерения по G (расходу) свойство                              
        VTypeM = 46  'ед. измерения по V (объему) свойство                               
        MTypeM = 47  'ед. измерения по M (массе) свойство                                
        PTypeM = 48  'ед. измерения по P (давлению) свойство                             
        dtTypeM = 49  'ед. измерения по dt (температуре) свойство                         
        tswTypeM = 50  'ед. измерения по tx (температуре) свойство                         
        taTypeM = 51  'ед. измерения по ta (температуре) свойство                         
        MgTypeM = 52  'ед. измерения по M (массе) свойство                                
        QoTypeM = 53  'ед. измерения по Q (теплу) свойство                                
        QgTypeM = 54  'ед. измерения по Q (теплу) свойство                                
        QntTypeHIM = 55  'ед. измерения по BНP (времени) свойство                            
        QntTypeM = 56  'ед. измерения по ВОС (времени)* (см. прим.) свойство               
        tTypeFractDiNum = 57  'кол-во знаков после запятой для t свойство                         
        GTypeFractDigNum1 = 58  'резерв свойство                                                    
        VTypeFractDigNum1 = 59  'кол-во знаков после запятой для V по Тв1 свойство                  
        MTypeFractDigNum1 = 60  'кол-во знаков после запятой для M по Тв1 свойство                  
        PTypeFractDigNum1 = 61  'кол-во знаков после запятой для P свойство                         
        dtTypeFractDigNum1 = 62  'кол-во знаков после запятой для dt свойство                        
        tswTypeFractDigNum1 = 63  'кол-во знаков после запятой для tx свойство                        
        taTypeFractDigNum1 = 64  'кол-во знаков после запятой для ta свойство                        
        MgTypeFractDigNum1 = 65  'кол-во знаков после запятой для Mг свойство                        
        QoTypeFractDigNum1 = 66  'кол-во знаков после запятой для Q по Тв1 свойство                  
        tTypeFractDigNum2 = 67  'резерв свойство                                                    
        GTypeFractDigNum2 = 68  'резерв свойство                                                    
        VTypeFractDigNum2 = 69  'кол-во знаков после запятой для V по Тв2 свойство                  
        MTypeFractDigNum2 = 70  'кол-во знаков после запятой для M по Тв2 свойство                  
        PTypeFractDigNum2 = 71  'кол-во знаков после запятой для P свойство                         
        dtTypeFractDigNum2 = 72  'кол-во знаков после запятой для dt свойство                        
        tswTypeFractDigNum2 = 73  'кол-во знаков после запятой для tx свойство                        
        taTypeFractDigNum2 = 74  'кол-во знаков после запятой для ta свойство                        
        MgTypeFractDigNum2 = 75  'кол-во знаков после запятой для Mг свойство                        
        QoTypeFractDigNum2 = 76  'кол-во знаков после запятой для Q по Тв2 свойство                  
        NSPrintTypeM_1 = 77  'Наличие нештатной ситуации по ТВ1 параметр                         
        NSPrintTypeM_2 = 78  'Наличие нештатной ситуации по ТВ2 параметр                         
        QntNS_1 = 79  'Длительность HC по параметрам Тв1 параметр                         
        QntNS_2 = 80  'Длительность HC по параметрам Тв2 параметр                         
        DopInpImpP_Type = 81  ' DI параметр                                                   
        P3P_Type = 82  'P3 параметр    
    End Enum


    Private ActiveElements(83) As Integer
    Private ElemSize(83) As Byte
    Private PropVal(83) As Byte

    Private DigMap(83) As Byte

    Private Sub InitDigMap()
        Dim i As Integer
        For i = 0 To 83
            DigMap(i) = 0
        Next
        DigMap(0) = VKT7ElemType.tTypeFractDiNum
        DigMap(1) = VKT7ElemType.tTypeFractDiNum
        DigMap(2) = VKT7ElemType.tTypeFractDiNum
        DigMap(22) = VKT7ElemType.tTypeFractDiNum
        DigMap(23) = VKT7ElemType.tTypeFractDiNum
        DigMap(24) = VKT7ElemType.tTypeFractDiNum

        DigMap(3) = VKT7ElemType.VTypeFractDigNum1
        DigMap(4) = VKT7ElemType.VTypeFractDigNum1
        DigMap(5) = VKT7ElemType.VTypeFractDigNum1
        DigMap(25) = VKT7ElemType.VTypeFractDigNum2
        DigMap(26) = VKT7ElemType.VTypeFractDigNum2
        DigMap(27) = VKT7ElemType.VTypeFractDigNum2

        DigMap(6) = VKT7ElemType.MTypeFractDigNum1
        DigMap(7) = VKT7ElemType.MTypeFractDigNum1
        DigMap(8) = VKT7ElemType.MTypeFractDigNum1
        DigMap(28) = VKT7ElemType.MTypeFractDigNum2
        DigMap(29) = VKT7ElemType.MTypeFractDigNum2
        DigMap(30) = VKT7ElemType.MTypeFractDigNum2

        DigMap(9) = VKT7ElemType.PTypeFractDigNum1
        DigMap(10) = VKT7ElemType.PTypeFractDigNum1
        DigMap(31) = VKT7ElemType.PTypeFractDigNum2
        DigMap(32) = VKT7ElemType.PTypeFractDigNum2

        DigMap(11) = VKT7ElemType.MgTypeFractDigNum1
        DigMap(33) = VKT7ElemType.MgTypeFractDigNum2

        DigMap(12) = VKT7ElemType.QoTypeFractDigNum1
        DigMap(34) = VKT7ElemType.QoTypeFractDigNum2

        DigMap(13) = VKT7ElemType.QoTypeFractDigNum1
        DigMap(35) = VKT7ElemType.QoTypeFractDigNum2

        DigMap(14) = VKT7ElemType.dtTypeFractDigNum1
        DigMap(36) = VKT7ElemType.dtTypeFractDigNum2

        DigMap(15) = VKT7ElemType.tswTypeFractDigNum1
        DigMap(37) = VKT7ElemType.tswTypeFractDigNum2

        DigMap(16) = VKT7ElemType.taTypeFractDigNum1
        DigMap(38) = VKT7ElemType.taTypeFractDigNum2

        DigMap(19) = VKT7ElemType.GTypeFractDigNum1
        DigMap(20) = VKT7ElemType.GTypeFractDigNum1
        DigMap(21) = VKT7ElemType.GTypeFractDigNum1
        DigMap(41) = VKT7ElemType.taTypeFractDigNum2
        DigMap(42) = VKT7ElemType.taTypeFractDigNum2
        DigMap(43) = VKT7ElemType.taTypeFractDigNum2

    End Sub

    Public Function VerifySumm(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As Boolean
        Dim ch As Long
        If sz <= 4 Then Return False
        ch = CheckSum(Data, offset, sz - 2)
        If Data(offset + sz - 2) = ch Mod 256 And Data(offset + sz - 1) = ch \ 256 Then
            Return True
        End If
        Return False
    End Function



    Private Function CheckSum(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As UInt16
        Dim s As UInt16
        Dim sl As Long
        sl = &HFFFF
        On Error Resume Next
        For i As Integer = 0 To sz - 1

            sl = ((sl \ 256) * 256 + ((sl Mod 256) Xor Data(offset + i))) And &HFFFF
            For sh As Integer = 0 To 7
                If (sl And 1) = 1 Then
                    sl = ((sl \ 2) Xor &HA001) And &HFFFF
                Else
                    sl = (sl \ 2) And &HFFFF
                End If


            Next
        Next
        s = sl And &HFFFF
        Return s
    End Function

    Private Function SelectElement(ByVal elemType As VKT7ElemType) As Boolean


        Dim Frame(20) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H10
        Frame(4) = &H3F
        Frame(5) = &HFF
        Frame(6) = &H0
        Frame(7) = &H0
        Frame(8) = &H6
        Frame(9) = elemType
        Frame(10) = &H0
        Frame(11) = &H0
        Frame(12) = 64
        Frame(13) = ElemSize(elemType)
        Frame(14) = 0


        ch = CheckSum(Frame, 2, 13)
        Frame(15) = ch Mod 256
        Frame(16) = ch \ 256

        EraseInputQueue()
        MyTransport.Write(Frame, 0, 17)

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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    MyTransport.CleanPort()
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return False
                    End If
                    Return True
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return False

    End Function





    Private Function SelectElements(ByVal elemTypes() As VKT7ElemType) As Boolean


        Dim Frame(512) As Byte
        Dim elemType As VKT7ElemType
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H10
        Frame(4) = &H3F
        Frame(5) = &HFF
        Frame(6) = &H0
        Frame(7) = &H0
        Frame(8) = &H6 * elemTypes.Length


        Dim i As Integer
        Dim lastidx As Integer
        For i = 0 To elemTypes.Length - 1
            elemType = elemTypes(i)
            Frame(9 + i * 6) = elemType
            Frame(10 + i * 6) = &H0
            Frame(11 + i * 6) = &H0
            Frame(12 + i * 6) = &H40
            Frame(13 + i * 6) = ElemSize(elemType)
            Frame(14 + i * 6) = 0
            lastidx = 14 + i * 6
        Next

        

        ch = CheckSum(Frame, 2, lastidx - 1)
        Frame(lastidx + 1) = ch Mod 256
        Frame(lastidx + 2) = ch \ 256

        EraseInputQueue()
        MyTransport.Write(Frame, 0, lastidx + 3)

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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    MyTransport.CleanPort()
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return False
                    End If
                    Return True
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return False

    End Function



    Private Function TryGetParam(ByVal ElemType As VKT7ElemType) As Single
        SelectElement(ElemType)

        If IsError Then
            Return Single.NaN
        End If

        Dim d As Single = 0.0

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HFE
        Frame(6) = &H0
        Frame(7) = &H0
        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256
        Frame(9) = ch \ 256

        EraseInputQueue()
        MyTransport.Write(Frame, 0, 10)

        WaitForData()

        Dim b() As Byte
        ReDim b(4096)
        Dim sout As String = "0"

        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return Single.NaN
                    End If

                    Dim z As ULong
                    Try
                        If b(sz - 4) = (OPC_QUALITY_UNCERTAIN Or OPC_QUALITY_SENSOR_CAL) Then
                            If b(sz - 3) <> 0 And b(sz - 3) <> 255 Then
                                IsHC = True
                                CurHC = b(sz - 3).ToString
                            End If
                        End If
                        If b(sz - 4) = (OPC_QUALITY_BAD Or OPC_QUALITY_DEVICE_FAILURE) Then
                            Return Single.NaN
                        End If

                        If b(sz - 4) = (OPC_QUALITY_BAD Or OPC_QUALITY_CONFIG_ERROR) Then
                            Return Single.NaN
                        End If

                        If b(2) = 3 Then

                            z = b(3)
                            sout = z.ToString()
                        End If

                        Dim digs As Integer
                        digs = 0
                        If ElemType >= 0 Then
                            digs = PropVal(DigMap(ElemType))
                        End If
                        If b(2) = 4 Then
                            z = b(4) * 256 + b(3)
                            sout = SetDot(z.ToString(), digs)
                        End If

                        If b(2) = 6 Then
                            If ElemType = VKT7ElemType.G1Type Or _
                               ElemType = VKT7ElemType.G2Type Or _
                               ElemType = VKT7ElemType.G3Type Or _
                               ElemType = VKT7ElemType.G1_2Type Or _
                               ElemType = VKT7ElemType.G2_2Type Or _
                               ElemType = VKT7ElemType.G3_2Type Then

                                Dim ddd As Single
                                ddd = BToSingle(b, 3)
                                sout = ddd.ToString().Replace(",", ".")
                            Else


                                z = b(6) * 256L * 65536L + b(5) * 65536L + b(4) * 256L + b(3)
                                sout = SetDot(z.ToString(), digs)
                            End If
                    End If
                        d = Val(sout)
                        Return d
                    Catch ex As Exception
                        SequenceErrorCount += 1
                        Return Single.NaN
                    End Try


                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1

        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return Single.NaN
    End Function





    Private Function TryGetElements(ByVal ElemTypes() As VKT7ElemType, ByRef Arch As Archive) As String
        SelectElements(ElemTypes)
        Dim ElemType As VKT7ElemType
        Dim AErr As String = ""

        If IsError Then
            Return "Ошибка выбора списка параметров"
        End If

        Dim d As Single = 0.0

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HFE
        Frame(6) = &H0
        Frame(7) = &H0
        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256
        Frame(9) = ch \ 256

        EraseInputQueue()
        MyTransport.Write(Frame, 0, 10)

        WaitForData()

        Dim b() As Byte
        ReDim b(4096)
        Dim sout As String = "0"

        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return ErrorMessage
                    End If

                    Dim z As ULong
                    Dim i As Integer
                    Dim pSz As Integer
                    Dim pRes As Integer
                    Dim bQ As Byte
                    Dim bNC As Byte
                    Dim digs As Integer

                    pRes = 3
                    For i = 0 To ElemTypes.Length - 1
                        ElemType = ElemTypes(i)
                        sout = ""
                        z = 0
                        digs = 0
                        If ElemType >= 0 Then
                            digs = PropVal(DigMap(ElemType))
                        End If
                        IsError = False

                        pSz = b(pRes)
                        bQ = b(pRes + pSz + 2)
                        bNC = b(pRes + pSz + 3)
                        If bQ = (OPC_QUALITY_UNCERTAIN Or OPC_QUALITY_SENSOR_CAL) Then
                            If bNC <> 0 And bNC <> 255 Then
                                IsHC = True
                                CurHC = bNC.ToString
                            End If
                        End If
                        If bQ = (OPC_QUALITY_BAD Or OPC_QUALITY_DEVICE_FAILURE) Then
                            IsError = True
                        End If

                        If bQ = (OPC_QUALITY_BAD Or OPC_QUALITY_CONFIG_ERROR) Then
                            IsError = True
                        End If

                        If IsError = False Then
                            If pSz = 1 Then
                                z = b(pRes + 2)
                            sout = z.ToString()
                        End If

                            If pSz = 2 Then
                                z = b(pRes + 3) * 256 + b(pRes + 2)
                            sout = SetDot(z.ToString(), digs)
                        End If


                            If pSz = 4 Then
                            If ElemType = VKT7ElemType.G1Type Or _
                               ElemType = VKT7ElemType.G2Type Or _
                               ElemType = VKT7ElemType.G3Type Or _
                               ElemType = VKT7ElemType.G1_2Type Or _
                               ElemType = VKT7ElemType.G2_2Type Or _
                               ElemType = VKT7ElemType.G3_2Type Then

                                Dim ddd As Single
                                ddd = BToSingle(b, 3)
                                sout = ddd.ToString().Replace(",", ".")
                            Else


                                    z = b(pRes + 5) * 256L * 65536L + b(pRes + 4) * 65536L + b(pRes + 3) * 256L + b(pRes + 2)
                                sout = SetDot(z.ToString(), digs)
                            End If
                        End If

                        End If

                        ''''  распихать по параметрам в зависимости от elementtype


                        Select Case ElemType
                            Case VKT7ElemType.Qg_1TypeP
                                Arch.QG1 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "QG1:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.Qg_1TypeP) Then AErr += "QG1;"
                                End If


                            Case VKT7ElemType.Qg_2TypeP
                                Arch.QG2 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "QG2:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.Qg_2TypeP) Then
                                        AErr += "QG2;"
                                    End If
                                End If


                            Case VKT7ElemType.M1_1Type
                                Arch.M1 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "M1:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.M1_1Type) Then
                                        AErr += "M1;"
                                    End If

                                End If

                            Case VKT7ElemType.M2_1Type
                                Arch.M2 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "M2:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.M2_1Type) Then
                                        AErr += "M2;"
                                    End If
                                End If

                            Case VKT7ElemType.M3_1Type
                                Arch.M3 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "M3:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.M3_1Type) Then
                                        AErr += "M3;"
                                    End If
                                End If


                            Case VKT7ElemType.M1_2Type
                                Arch.M4 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "M4:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.M1_2Type) Then AErr += "M4;"
                                End If

                            Case VKT7ElemType.M2_2Type
                                Arch.M5 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "M5:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.M2_2Type) Then
                                        AErr += "M5;"
                                    End If

                                End If

                            Case VKT7ElemType.M3_2Type
                                Arch.M6 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "M6:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.M3_2Type) Then
                                        AErr += "M6;"
                                    End If

                                End If


                            Case VKT7ElemType.V1_1Type
                                Arch.V1 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "V1:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.V1_1Type) Then
                                        AErr += "V1;"
                                    End If
                                End If

                            Case VKT7ElemType.V2_1Type
                                Arch.V2 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "V2:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.V2_1Type) Then
                                        AErr += "V2;"
                                    End If

                                End If

                            Case VKT7ElemType.V3_1Type
                                Arch.V3 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "V3:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.V3_1Type) Then
                                        AErr += "V3;"
                                    End If
                                End If

                            Case VKT7ElemType.V1_2Type

                                Arch.v4 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "V4:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.V1_2Type) Then
                                        AErr += "V4;"
                                    End If
                                End If

                            Case VKT7ElemType.V2_2Type
                                Arch.v5 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "V5:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.V2_2Type) Then
                                        AErr += "V5;"
                                    End If
                                End If

                            Case VKT7ElemType.V3_2Type
                                Arch.v6 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "V6:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.V3_2Type) Then
                                        AErr += "V6;"

                                    End If
                                End If

                            Case VKT7ElemType.Qo_1TypeP

                                Arch.Q1 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "Q1:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.Qo_1TypeP) Then
                                        AErr += "Q1;"

                                    End If
                                End If

                            Case VKT7ElemType.Qo_2TypeP
                                Arch.Q2 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "Q2:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.Qo_2TypeP) Then
                                        AErr += "Q2;"
                                    End If
                                End If


                            Case VKT7ElemType.t1_1Type
                                Arch.T1 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "T1:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.t1_1Type) Then
                                        AErr += "T1;"
                                    End If
                                End If

                            Case VKT7ElemType.t2_1Type
                                Arch.T2 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "T2:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.t2_1Type) Then
                                        AErr += "T2;"
                                    End If
                                End If

                            Case VKT7ElemType.t3_1Type
                                Arch.T3 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "T3:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.t3_1Type) Then
                                        AErr += "T3;"

                                    End If
                                End If

                            Case VKT7ElemType.t1_2Type
                                Arch.T4 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "T4:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.t1_2Type) Then
                                        AErr += "T4;"

                                    End If
                                End If

                            Case VKT7ElemType.t2_2Type
                                Arch.T5 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "T5:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.t2_2Type) Then
                                        AErr += "T5;"

                                    End If
                                End If


                            Case VKT7ElemType.t3_2Type
                                Arch.T6 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "T6:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.t3_2Type) Then
                                        AErr += "T6;"

                                    End If
                                End If


                            Case VKT7ElemType.NSPrintTypeM_1
                                Try
                                    Arch.HCtv1 = Val(sout)
                                Catch ex As Exception

                                End Try

                            Case VKT7ElemType.NSPrintTypeM_2
                                Try
                                    Arch.HCtv2 = Val(sout)
                    Catch ex As Exception

                    End Try

                                If IsError Then
                                    If VerifyElement(VKT7ElemType.NSPrintTypeM_2) Then
                                        If Not OldVersion Then
                                            '       AErr += "HCtv2;"
                                        Else
                                            Arch.HCtv2 = 32
                                        End If
                                    End If

                                End If

                            Case VKT7ElemType.P1_1Type
                                Arch.P1 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "P1:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.P1_1Type) Then
                                        AErr += "P1;"
                                    End If
                                End If

                            Case VKT7ElemType.P2_1Type

                                Arch.P2 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "P2:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.P2_1Type) Then
                                        AErr += "P2;"

                                    End If
                                End If

                            Case VKT7ElemType.P2_1Type

                                Arch.P3 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "P3:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.P2_1Type) Then
                                        AErr += "P3;"

                                    End If
                                End If

                            Case VKT7ElemType.P2_2Type

                                Arch.P4 = Val(sout)
                                If IsHC Then
                                    Arch.MsgHC += "P4:" + CurHC + " "
                                End If
                                If IsError Then
                                    If VerifyElement(VKT7ElemType.P2_2Type) Then
                                        AErr += "P4;"


                                    End If
                                End If


                            Case VKT7ElemType.QntType_1P

                                Arch.errtime1 = Val(sout)
                                If IsError Then
                                    'AErr += "errtime1;"
                                End If

                            Case VKT7ElemType.Qnt_2TypeP
                                Arch.errtime2 = Val(sout)
                                If IsError Then
                                    'AErr += "errtime2;"
                                End If

                            Case VKT7ElemType.QntType_1HIP

                                Arch.oktime1 = Val(sout)
                                If IsError Then
                                    'AErr += "oktime1;"
                                End If
                            Case VKT7ElemType.Qnt_2TypeHIP
                                Arch.oktime2 = Val(sout)
                                If IsError Then
                                    'AErr += "oktime2;"
                                End If


                        End Select

                        pRes += (4 + pSz) ' size + pSz + Q+NC
                    Next

                    Return AErr

                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1

        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return ErrorMessage + " " + AErr
    End Function


    Private Function TryGetParamS(ByVal ElemType As VKT7ElemType) As String
        SelectElement(ElemType)

        If IsError Then
            Return 0
        End If

        Dim d As Single = 0.0

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HFE
        Frame(6) = &H0
        Frame(7) = &H0
        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256
        Frame(9) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 10)

        WaitForData()

        Dim b() As Byte
        ReDim b(4096)
        Dim sout As String = ""

        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return 0
                    End If

                    Dim z As ULong
                    Try

                        If b(2) = 3 Then
                            z = b(3)
                            sout = z.ToString()
                        End If

                        Dim digs As Integer
                        digs = 0
                        If ElemType >= 0 Then
                            digs = PropVal(DigMap(ElemType))
                        End If
                        If b(2) = 4 Then
                            z = b(4) * 256 + b(3)
                            sout = SetDot(z.ToString(), digs)
                        End If

                        If b(2) = 6 Then
                            If ElemType = VKT7ElemType.G1Type Or _
                               ElemType = VKT7ElemType.G2Type Or _
                               ElemType = VKT7ElemType.G3Type Or _
                               ElemType = VKT7ElemType.G1_2Type Or _
                               ElemType = VKT7ElemType.G2_2Type Or _
                               ElemType = VKT7ElemType.G3_2Type Then

                                Dim ddd As Single
                                ddd = BToSingle(b, 3)
                                sout = ddd.ToString().Replace(",", ".")
                            Else


                                Dim ii As Integer
                                sout = ""
                                For ii = 0 To 5
                                    sout = sout + b(3 + ii).ToString + " "
                                Next
                            End If
                        End If
                        If b(2) = 10 Then
                            Dim ii As Integer
                            sout = ""
                            For ii = 0 To 9
                                sout = b(3 + ii).ToString + " "
                            Next
                        End If

                        Return sout
                    Catch ex As Exception
                        SequenceErrorCount += 1
                        Return ""
                    End Try
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1

        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return d
    End Function


    Public Function Bytes2Float(ByVal fbytes() As Byte, ByVal index As Int16) As Single
        If UBound(fbytes) - index < 3 Then
            Return 0
        End If
        Return System.BitConverter.ToSingle(fbytes, index)
    End Function

    Private Function BToSingle(ByVal hexValue() As Byte, ByVal index As Int16) As Single

        Try

            Dim iInputIndex As Integer = 0

            Dim iOutputIndex As Integer = 0

            Dim bArray(3) As Byte



            For iInputIndex = 0 To 3

                bArray(iOutputIndex) = hexValue(index + iInputIndex)

                iOutputIndex += 1

            Next
            'Array.Reverse(bArray)
            Return BitConverter.ToSingle(bArray, 0)

        Catch ex As Exception

        End Try
    End Function


    Private Function GetParam(ByVal ElemType As VKT7ElemType) As Single
        IsError = False
        IsHC = False
        CurHC = ""
        ErrorMessage = ""
        EraseInputQueue()

        Dim tryCnt = 0
        Dim r As Single
        While tryCnt < 5
            r = TryGetParam(ElemType)
            If Not IsError Then
                Return r
            End If
            tryCnt += 1
            EraseInputQueue()
        End While
        Return Single.NaN
    End Function


    Private Function GetParam0(ByVal ElemType As VKT7ElemType) As Single
        IsError = False
        IsHC = False
        CurHC = ""
        ErrorMessage = ""
        EraseInputQueue()
        Try
        Dim tryCnt = 0
        Dim r As Single
        While tryCnt < 5
            r = TryGetParam(ElemType)
            If Not IsError Then
                If Single.IsNaN(r) Then
                    Return 0
                End If
                Return r
            End If
            tryCnt += 1
            EraseInputQueue()
        End While
        Catch ex As Exception
            Return 0
        End Try
        
        Return 0
    End Function

    Private Function GetParamS(ByVal ElemType As VKT7ElemType) As String
        IsError = False
        ErrorMessage = ""
        EraseInputQueue()

        Dim tryCnt = 0
        Dim r As String
        While tryCnt < 5
            r = TryGetParamS(ElemType)
            If Not IsError Then
                Return r
            End If
            tryCnt += 1
            EraseInputQueue()
        End While
        Return ""
    End Function


    Private Function SetDot(ByVal S As String, ByVal dig As Integer) As String
        Dim oo As String
        If dig > 0 Then
            oo = S
            While Len(oo) < dig + 1
                oo = "0" + oo
            End While

            Return oo.Substring(0, oo.Length - dig) + "." + oo.Substring(oo.Length - dig, dig)
        Else
            Return S
        End If
    End Function


    Private Function ReadData(Optional ByVal ElemType As Integer = -1) As Byte()

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HFE
        Frame(6) = &H0
        Frame(7) = &H0
        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256
        Frame(9) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 10)

        ' Dim t As Integer


        WaitForData()

        Dim b() As Byte
        ReDim b(4096)
        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    Return b
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
            Return Nothing
        Else
            SequenceErrorCount += 1
            Return Nothing
        End If

    End Function


    Private Function GetDeviceDate2() As Date
        EraseInputQueue()

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HF6
        Frame(6) = 0
        Frame(7) = 0


        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256
        Frame(9) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 10)

        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim d As DateTime
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return Date.Now
                    End If
                    If b(2) >= 12 Then
                        Try
                            d = New DateTime(2000 + b(4 + 5), b(4 + 4), b(4 + 3), b(4 + 6), 0, 0)
                        Catch ex As Exception
                            d = DateTime.Now
                        End Try

                        Return d
                    Else
                        Return DateTime.Now
                    End If

                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(10))
                cnt = MyTransport.BytesToRead
            End While

        Else
            SequenceErrorCount += 1
        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return Date.Now
    End Function
    Private OldVersion As Boolean = False
    Private Function GetDeviceDate() As Date
        EraseInputQueue()

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HFB
        ch = CheckSum(Frame, 2, 4)
        Frame(6) = ch Mod 256
        Frame(7) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 8)

        'Dim t As Integer
        'RaiseIdle()
        'Thread.Sleep(60)
        't = 0
        'While MyTransport.BytesToRead = 0 And t < 20
        '    RaiseIdle()
        '    Thread.Sleep(CalcInterval(2))
        '    t = t + 1
        'End While
        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim d As DateTime
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return Date.Now
                    End If
                    If b(2) >= 4 Then
                        Try
                            d = New DateTime(2000 + b(5), b(4), b(3), b(6), 0, 0)
                        Catch ex As Exception
                            d = Now
                        End Try

                        Return d
                    ElseIf b(2) >= 3 Then
                        Try
                            d = New DateTime(2000 + b(5), b(4), b(3), 0, 0, 0)
                        Catch ex As Exception
                            d = Today
                        End Try

                        Return d
                    Else 'If b(2) = 0 Then
                        OldVersion = True
                        Return GetDeviceDate2()
                    End If
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1
        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return Date.Now

    End Function


    Private Function SetArchType(ByVal Atype As VKT7ArchType) As Boolean
        IsError = False
        ErrorMessage = ""
        RaiseIdle()
        Thread.Sleep(60)

        Dim Frame(12) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H10
        Frame(4) = &H3F
        Frame(5) = &HFD
        Frame(6) = &H0
        Frame(7) = &H0
        Frame(8) = &H2
        Frame(9) = Atype
        Frame(10) = &H0
        ch = CheckSum(Frame, 2, 9)
        Frame(11) = ch Mod 256
        Frame(12) = ch \ 256

        EraseInputQueue()
        MyTransport.Write(Frame, 0, 13)

        'Dim t As Integer
        'RaiseIdle()
        Thread.Sleep(200)
        't = 0
        'While MyTransport.BytesToRead = 0 And t < 20
        '    RaiseIdle()
        '    Thread.Sleep(CalcInterval(2))
        '    t = t + 1
        'End While

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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return False
                    End If
                    Return True
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1

        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return False
    End Function

    Public Enum VKT7ArchType
        AT_Hour = 0 '-часовой архив;
        AT_Day = 1  '-суточный архив;
        AT_Month = 2 '-месячный архив;
        AT_Itog = 3 '-итоговый архив;
        AT_Current = 4 '-текущие значения;
        AT_CurItog = 5 '-итоговые текущие;
        AT_Properties = 6 '-свойства.
    End Enum

    Private Sub SetArchDate(ByVal Dat As Date)
        IsError = False
        ErrorMessage = ""
        EraseInputQueue()
        Dim Frame(20) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H10
        Frame(4) = &H3F
        Frame(5) = &HFB
        Frame(6) = &H0
        Frame(7) = &H0
        Frame(8) = &H4
        Frame(9) = Dat.Day
        Frame(10) = Dat.Month
        Frame(11) = (Dat.Year - 2000)
        Frame(12) = Dat.Hour
        ch = CheckSum(Frame, 2, 11)
        Frame(13) = ch Mod 256
        Frame(14) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 15)

        'Dim t As Integer
        'RaiseIdle()
        'Thread.Sleep(60)
        't = 0
        'While MyTransport.BytesToRead = 0 And t < 20
        '    RaiseIdle()
        '    Thread.Sleep(CalcInterval(2))
        '    t = t + 1
        'End While

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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    SequenceErrorCount = 0
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return
                    End If
                    Return
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        Else
            SequenceErrorCount += 1
        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
    End Sub

    Private Sub GetList()
        IsError = False
        ErrorMessage = ""
        MyTransport.CleanPort()
        EraseInputQueue()
        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &HFF
        Frame(1) = &HFF
        Frame(2) = &H0
        Frame(3) = &H3
        Frame(4) = &H3F
        Frame(5) = &HFC
        Frame(6) = &H0
        Frame(7) = &H0
        ch = CheckSum(Frame, 2, 6)
        Frame(8) = ch Mod 256
        Frame(9) = ch \ 256

        EraseInputQueue()
        MyTransport.Write(Frame, 0, 10)

        'Dim t As Integer
        'RaiseIdle()
        Thread.Sleep(200)
        't = 0
        'While MyTransport.BytesToRead = 0 And t < 20
        '    RaiseIdle()
        '    Thread.Sleep(CalcInterval(2))
        '    t = t + 1
        'End While
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
                cnt = MyTransport.Read(b, ptr, 1)
                ptr += cnt
                sz += cnt

                Dim i As Integer
                If VerifySumm(b, 0, sz) Then
                    EraseInputQueue()
                    Dim sout As String = ""
                    Dim cout As String = ""
                    If (b(1) = &H83 Or b(1) = &H90) Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & b(2)
                        Return
                    End If


                    ActiveCount = 0
                    For i = 0 To 83
                        ElemSize(i) = 0
                    Next
                    For i = 3 To cnt - 5 Step 6
                        ActiveElements(ActiveCount) = b(i)
                        ElemSize(b(i)) = b(i + 4)
                        ActiveCount += 1
                    Next
                    Return

                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While


        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"

    End Sub

    Private Function VerifyElement(ByVal EType As VKT7ElemType) As Boolean
        Dim i As Integer

        For i = 0 To ActiveCount - 1
            If ActiveElements(i) = EType Then
                Return True
            End If
        Next
        If EType >= 44 And EType <= 80 Then
            Return True
        End If
        Return False

    End Function


End Class
