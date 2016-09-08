Imports System.IO
Imports System.Threading

Public MustInherit Class TVDriver

    Public Const archType_moment As Integer = 1
    Public Const archType_total As Integer = 2
    Public Const archType_hour As Integer = 3
    Public Const archType_day As Integer = 4

#Region "Driver specific"
    Public Overridable Sub DoSpecificSetup(ByRef TvMain As Object)
        MsgBox("Дополнительная настройка драйвера " + CounterName() + " не требуется", MsgBoxStyle.OkOnly And MsgBoxStyle.Information, "Дополнительная настройки драйвера")
    End Sub

    Public Overridable Sub SetupDriverFromDB(ByRef TvMain As Object)
        'do nothing by defaul
        Debug.Print(CounterName() + ": execute SetupDriverFromDB ")
    End Sub
#End Region
#Region "properties"
    Private mDeviceID As Integer
    Public Overridable Property DeviceID As Integer
        Get
            Return mDeviceID
        End Get
        Set(value As Integer)
            mDeviceID = value
        End Set
    End Property

    Private m_ComPort As String = ""
    Public Overridable Property ComPort As String
        Get
            Return m_ComPort
        End Get
        Set(value As String)
            m_ComPort = value
        End Set
    End Property

    Private m_ConnectLimit As Integer = 60
    Public Overridable Property ConnectLimit As Integer
        Get
            Return m_ConnectLimit
        End Get
        Set(ByVal value As Integer)
            m_ConnectLimit = value
        End Set
    End Property



    Private m_Phone As String = ""
    Public Overridable Property Phone As String
        Get
            Return m_Phone
        End Get
        Set(value As String)
            m_Phone = value
        End Set
    End Property

    Private m_PhoneLineType As String = ""
    Public Overridable Property PhoneLineType As String
        Get
            Return m_PhoneLineType
        End Get
        Set(value As String)
            m_PhoneLineType = value
        End Set
    End Property

    Private m_AtCommand As String = ""
    Public Overridable Property AtCommand As String
        Get
            Return m_AtCommand
        End Get
        Set(value As String)
            m_AtCommand = value
        End Set
    End Property

    Private m_DBTableName As String = "datacurr"

    Public Overridable Property DBTableName() As String
        Get
            Return (m_DBTableName)
        End Get
        Set(ByVal value As String)
            m_DBTableName = value
        End Set
    End Property



    Private m_serverip As String
    Public Overridable Property ServerIp() As String
        Get
            Return m_serverip
        End Get
        Set(ByVal value As String)
            m_serverip = value
        End Set
    End Property

    Private m_timeout As Long
    Public Overridable Property TimeOut() As Long
        Get
            Return m_timeout
        End Get
        Set(ByVal value As Long)
            m_timeout = value
        End Set
    End Property
    Private m_BaudRate As Long
    Public Overridable Property BaudRate() As Long
        Get
            Return m_BaudRate
        End Get
        Set(ByVal value As Long)
            m_BaudRate = value
        End Set
    End Property
    Private m_DataBits As Long
    Public Overridable Property DataBits() As Long
        Get
            Return m_DataBits
        End Get
        Set(ByVal value As Long)
            m_DataBits = value
        End Set
    End Property
    Private m_StopBits As Long
    Public Overridable Property StopBits() As Long
        Get
            Return m_StopBits
        End Get
        Set(ByVal value As Long)
            m_StopBits = value
        End Set
    End Property
    Private m_Parity As String
    Public Overridable Property Parity() As String
        Get
            Return m_Parity
        End Get
        Set(ByVal value As String)
            m_Parity = value
        End Set
    End Property
    Private m_FlowControl As String
    'Public Overridable Property FlowControl() As String
    '    Get
    '        Return m_FlowControl
    '    End Get
    '    Set(ByVal value As String)
    '        m_FlowControl = value
    '    End Set
    'End Property


    Private m_NPPassword As String
    Public Overridable Property NPPassword() As String
        Get
            Return m_NPPassword
        End Get
        Set(ByVal value As String)
            m_NPPassword = value
        End Set
    End Property

    Private m_IPPort As String
    Public Overridable Property IPPort() As String
        Get
            Return m_IPPort
        End Get
        Set(ByVal value As String)
            m_IPPort = value
        End Set
    End Property



    Private mTransport As Integer
    Public Overridable Property Transport As Integer
        Get
            Return mTransport
        End Get
        Set(value As Integer)
            mTransport = value
        End Set
    End Property



    Dim m_sleepInterval As Int32
    Public Overridable Property sleepInterval() As Int32

        Get
            Return m_sleepInterval
        End Get
        Set(ByVal value As Int32)
            m_sleepInterval = value
        End Set
    End Property

#End Region
#Region "Transport support"

    Protected WithEvents MyTransport As UniTransport

    Public ReadOnly Property DriverTransport() As UniTransport
        Get
            Return MyTransport
        End Get
    End Property


    Public Overridable Function OpenPort(Optional ByRef aSocket As GRPSSocket = Nothing) As Boolean
        Dim nd As NportTransportSetupData
        Dim vd As VortexTransportSetupData
        Dim sd As SerialTransportSetupData
        Dim md As ModemTransportSetupData
        Dim ad As GRPSTransportSetupData
        Dim dd As TransportSetupData


        If Transport = 9 Then
            MyTransport = New DummyTransport()
            dd = New TransportSetupData
            dd.BaudRate = BaudRate
            MyTransport.SetupTransport(dd)
        End If
        If Transport = 5 Or Transport = 6 Then
            MyTransport = New GRPSTransport(aSocket)
            ad = New GRPSTransportSetupData
            ad.BaudRate = BaudRate
            MyTransport.SetupTransport(ad)
        End If
        If Transport = 3 Then
            MyTransport = New VortexTransport
            vd = New VortexTransportSetupData
            vd.BaudRate = BaudRate

            vd.Host = ServerIp
            Try
                vd.Port = Integer.Parse(IPPort)
            Catch ex As Exception
                vd.Port = 80
            End Try

            vd.Timeout = TimeOut
            MyTransport.SetupTransport(vd)
        End If
        If Transport = 2 Then
            MyTransport = New NportTransport
            nd = New NportTransportSetupData
            nd.BaudRate = BaudRate
            nd.Parity = Ports.Parity.None
            If Parity.ToLower() = "none" Then
                nd.Parity = Ports.Parity.None
            End If
            If Parity.ToLower() = "even" Then
                nd.Parity = Ports.Parity.Even
            End If
            If Parity.ToLower() = "odd" Then
                nd.Parity = Ports.Parity.Odd
            End If
            If Parity.ToLower() = "mark" Then
                nd.Parity = Ports.Parity.Mark
            End If
            If Parity.ToLower() = "space" Then
                nd.Parity = Ports.Parity.Space
            End If
            nd.DataBits = DataBits
            nd.StopBits = StopBits
            Dim nIPport As Integer
            Try
                nIPport = Integer.Parse(IPPort)
            Catch ex As Exception
                nIPport = 1
            End Try

            If nIPport > 0 And nIPport <= 12 Then
                nd.ComPortIdx = nIPport
            Else
                nd.ComPortIdx = 1
            End If

            nd.IPAddress = ServerIp
            nd.Timeout = TimeOut
            MyTransport.SetupTransport(nd)

        End If

        If Transport = 1 Then
            MyTransport = New SerialTransport
            sd = New SerialTransportSetupData

            sd.BaudRate = BaudRate
            sd.Parity = Ports.Parity.None
            If Parity.ToLower() = "none" Then
                sd.Parity = Ports.Parity.None
            End If
            If Parity.ToLower() = "even" Then
                sd.Parity = Ports.Parity.Even
            End If
            If Parity.ToLower() = "odd" Then
                sd.Parity = Ports.Parity.Odd
            End If
            If Parity.ToLower() = "mark" Then
                sd.Parity = Ports.Parity.Mark
            End If
            If Parity.ToLower() = "space" Then
                sd.Parity = Ports.Parity.Space
            End If
            sd.DataBits = DataBits
            sd.StopBits = StopBits
            sd.PortName = ComPort
            sd.Handshake = Ports.Handshake.RequestToSend

            MyTransport.SetupTransport(sd)

        End If
        If Transport = 0 Then
            MyTransport = New ModemTransport
            md = New ModemTransportSetupData

            md.BaudRate = BaudRate
            md.Parity = Ports.Parity.None
            If Parity.ToLower() = "none" Then
                md.Parity = Ports.Parity.None
            End If
            If Parity.ToLower() = "even" Then
                md.Parity = Ports.Parity.Even
            End If
            If Parity.ToLower() = "odd" Then
                md.Parity = Ports.Parity.Odd
            End If
            If Parity.ToLower() = "mark" Then
                md.Parity = Ports.Parity.Mark
            End If
            If Parity.ToLower() = "space" Then
                md.Parity = Ports.Parity.Space
            End If
            md.DataBits = DataBits
            md.StopBits = StopBits
            md.PortName = ComPort
            md.InitCommand = AtCommand
            md.ConnectLimit = ConnectLimit
            md.Phone = Phone
            md.PhoneLineType = PhoneLineType
            If PhoneLineType.ToUpper = "G" Then
                BaudRate = 1200
            End If
            MyTransport.SetupTransport(md)

        End If
        If MyTransport Is Nothing Then
            MyTransport = New NportTransport
        End If

        Return MyTransport.Connect()
    End Function


    Public Overridable Sub CloseTransportConnect()
        If Not MyTransport Is Nothing Then
            Try
                MyTransport.DisConnect()
            Catch ex As Exception

            End Try


            MyTransport = Nothing
        End If
    End Sub



    Public Overridable Sub EraseInputQueue()
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
    End Sub

    Protected Overridable Function MyRead(ByVal buf() As Byte, ByVal offset As Integer, ByVal len As Integer, ByVal timeToRead As Integer) As Integer
        Dim ret As Integer = 0
        Dim sz As Integer = 0
        'Dim i As Integer
        Dim btr As Integer
        Dim cnt As Integer


        btr = MyTransport.BytesToRead
        While btr > 0
            While btr
                Dim i As Integer
                i = MyTransport.Read(buf, sz + offset, 1)

                sz += i
                If sz = len Then
                    Return sz
                End If
                System.Threading.Thread.Sleep(CalcInterval(3))
                btr = MyTransport.BytesToRead
            End While

            RaiseIdle()
            cnt = 0
            While btr = 0 And cnt < 10
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
                cnt = cnt + 1
            End While

        End While
        Return sz


        'Try
        '    For i = 1 To 10
        '        ret = ret + MyTransport.Read(buf, ret + offset, len - ret)
        '        If ret = len Then
        '            Return ret
        '        End If

        '        If MyTransport.BytesToRead = 0 Then
        '            System.Threading.Thread.Sleep(1 + timeToRead / 10)
        '            RaiseIdle()
        '        End If
        '    Next
        'Catch ex As Exception
        '    'Stop
        'End Try

        'Return ret
    End Function

    Public Overridable Function write(ByVal buf() As Byte, ByVal len As Long) As String
        Try
            MyTransport.CleanPort()
            MyTransport.Write(buf, 0, len)
            Return "Отослано " & len.ToString() & " байт"
        Catch ew As Exception
            Return "Ошибка." & ew.Message

        End Try
    End Function

    Public Overridable Function CalcInterval(ByVal datasize As Long) As Double
        Dim d As Double
        d = 1000.0 * (DataBits + StopBits) * datasize / BaudRate
        If d < 5 Then
            d = 5
        End If
        Return d
    End Function
#End Region
#Region "Events"

    Public Event Idle()
    Public Overridable Sub RaiseIdle()
        RaiseEvent Idle()
    End Sub

    Public Event TransportStatus(ByVal Action As UnitransportAction, ByVal MSG As String)


#End Region
#Region "MustOvverride_zone"
    Public MustOverride Property isArchToDBWrite() As Boolean
    Public MustOverride Property isMArchToDBWrite() As Boolean
    Public MustOverride Property isTArchToDBWrite() As Boolean
    Public MustOverride Function CounterName() As String
    Public MustOverride Sub Connect()
    Public MustOverride Function IsConnected() As Boolean
    Public MustOverride Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
     ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
    'Public MustOverride Function WriteArchToDB() As String
    'Public MustOverride Function WriteMArchToDB() As String
    'Public MustOverride Function WriteTArchToDB() As String
    Public MustOverride Function ReadMArch() As String
    Public MustOverride Function ReadTArch() As String
    Public MustOverride Function ReadSystemParameters() As DataTable


#End Region

    Public Shared Function M4CRC(ByRef msg() As Byte, ByRef start As Integer, ByRef epos As Integer) As UShort

        Dim j As Integer, crc As UShort, ptr As Integer, t As UShort
        crc = 0
        ptr = start
        While (ptr <= epos)

            t = CType(msg(ptr), Short)
            crc = crc Xor t << 8
            'Debug.Print(ptr & " " & Hex(msg(ptr)) & " " & Hex(crc))
            ptr = ptr + 1
            For j = 0 To 7

                If crc And &H8000 Then
                    crc = (crc << 1) Xor &H1021
                Else
                    crc = crc << 1
                End If

            Next
        End While
        'Debug.Print(Hex(crc))
        Return crc
    End Function

    Private Sub MyTransport_Idle() Handles MyTransport.Idle
        RaiseEvent Idle()
    End Sub

    Private Sub MyTransport_TransportAction(ByVal Action As UnitransportAction, ByVal MSG As String) Handles MyTransport.TransportAction
        RaiseEvent TransportStatus(Action, MSG)
    End Sub

    Public Sub WaitForData(Optional ByVal q1 As Integer = 10, Optional ByVal Q2 As Integer = 20, Optional ByVal q3 As Integer = 10)

        Dim t As Int16
        Dim si As Double
        'If q1 <= 0 Then q1 = 10
        'If Q2 <= 0 Then Q2 = 20
        'If q3 <= 0 Then q3 = 10


        RaiseIdle()
        t = 0
        si = CalcInterval(10)
        Thread.Sleep(si)
        RaiseIdle()
        While MyTransport.BytesToRead = 0 And t < 500
            If Not MyTransport.IsConnected Then Exit Sub
            si = CalcInterval(1)
            Thread.Sleep(si)
            RaiseIdle()
            t = t + 1
        End While

        Dim cnt As Integer
        cnt = -1
        t = 0
        While MyTransport.BytesToRead <> cnt And t < 20
            If Not MyTransport.IsConnected Then Exit Sub
            cnt = MyTransport.BytesToRead
            RaiseIdle()
            si = CalcInterval(10)
            Thread.Sleep(si)
            t = t + 1
        End While


        If cnt = 0 Then
            t = 0
            While MyTransport.BytesToRead = 0 And t < 20
                If Not MyTransport.IsConnected Then Exit Sub
                System.Threading.Thread.Sleep(100)
                RaiseIdle()
                t = t + 1
            End While
            cnt = MyTransport.BytesToRead
            If cnt = 0 Then
                System.Threading.Thread.Sleep(500)
            End If

            If Me.BaudRate - (Me.BaudRate / 10) >= 1200 Then
                Me.BaudRate -= (Me.BaudRate / 10)
            Else
                Me.BaudRate = 1200
            End If

            Debug.Print("Decrement baud rate: " + BaudRate.ToString)
        End If

    End Sub

    Public Shared Function NanFormat(ByVal n As Single, ByVal fStr As String) As String
        If Single.IsNaN(n) Then
            Return "NULL"
        Else
            Dim s As String
            s = Format(n, fStr)
            If s.Length > 19 Then Return "NULL"
            If s.Contains("E") Then Return "NULL"
            Return s
        End If
    End Function

    Protected Shared Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function


    Public Structure MArchive
        Public DateArch As DateTime
        Public HC As Long
        Public MsgHC As String

        Public HCtv1 As Long
        Public MsgHC_1 As String

        Public HCtv2 As Long
        Public MsgHC_2 As String

        Public M1 As Single
        Public M2 As Single
        Public M3 As Single
        Public M4 As Single
        Public M5 As Single
        Public M6 As Single

        Public V1 As Single
        Public V2 As Single
        Public V3 As Single
        Public V4 As Single
        Public V5 As Single
        Public V6 As Single


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
        Public dQ1 As Single
        Public dQ2 As Single

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

        Public dt12 As Single
        Public dt45 As Single

        Public tx1 As Single
        Public tx2 As Single

        Public tair1 As Single
        Public tair2 As Single

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long


        Public OKTIME1 As Single
        Public OKTIME2 As Single
        Public ERRTIME1 As Single
        Public ERRTIME2 As Single
        Public WORKTIME1 As Single
        Public WORKTIME2 As Single

        Public archType As Short
    End Structure

    Public Structure Archive
        Public DateArch As DateTime

        Public HC As Long
        Public MsgHC As String

        Public HCtv1 As Long
        Public MsgHC_1 As String

        Public HCtv2 As Long
        Public MsgHC_2 As String

        Public V1 As Single
        Public V2 As Single
        Public V3 As Single
        Public v4 As Single
        Public v5 As Single
        Public v6 As Single

        Public V1H As Single
        Public V2H As Single
        Public V3H As Single
        Public v4H As Single
        Public v5H As Single
        Public v6H As Single

        Public P1 As Single
        Public P2 As Single
        Public P3 As Single
        Public P4 As Single
        Public P5 As Single
        Public P6 As Single

        Public T1 As Single
        Public T2 As Single
        Public T3 As Single
        Public T4 As Single
        Public T5 As Single
        Public T6 As Single


        Public Q1 As Single
        Public Q2 As Single
        Public Q3 As Single
        Public Q4 As Single
        Public Q5 As Single
        Public Q6 As Single

        Public Q1H As Single
        Public Q2H As Single
        Public Q3H As Single
        Public Q4H As Single
        Public Q5H As Single
        Public Q6H As Single

        'Public QG1 As Single
        'Public QG2 As Single

        Public M1 As Single
        Public M2 As Single
        Public M3 As Single
        Public M4 As Single
        Public M5 As Single
        Public M6 As Single

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long

        Public tx1 As Single
        Public tx2 As Single
        Public tair1 As Single
        Public tair2 As Single

        Public OKTIME1 As Single
        Public OKTIME2 As Single
        Public ERRTIME As Single
        Public ERRTIMEH As Single
        Public ERRTIME1 As Single
        Public ERRTIME2 As Single
        Public WORKTIME1 As Single
        Public WORKTIME2 As Single



        Public archType As Short
    End Structure


    Public Structure TArchive
        Public DateArch As DateTime

        Public HC As Long
        Public MsgHC As String

        Public HCtv1 As Long
        Public MsgHC_1 As String

        Public HCtv2 As Long
        Public MsgHC_2 As String

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

        Public P1 As Single
        Public P2 As Single
        Public P3 As Single
        Public P4 As Single
        Public P5 As Single
        Public P6 As Single


        Public T1 As Single
        Public T2 As Single
        Public T3 As Single
        Public T4 As Single
        Public T5 As Single
        Public T6 As Single

        Public Q1 As Double
        Public Q2 As Double
        Public Q3 As Double
        Public Q4 As Double
        Public Q5 As Double
        Public Q6 As Double

        Public OKTIME1 As Single
        Public OKTIME2 As Single

        Public ERRTIME1 As Single
        Public ERRTIME2 As Single

        Public WORKTIME1 As Single
        Public WORKTIME2 As Single

        Public archType As Short
    End Structure

    Protected Sub clearTArchive(ByRef arc As TArchive)
        arc.DateArch = DateTime.MinValue
        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""


        arc.ERRTIME1 = 0
        arc.ERRTIME2 = 0
        arc.WORKTIME1 = 0
        arc.WORKTIME2 = 0
        arc.OKTIME1 = 0
        arc.OKTIME2 = 0



        arc.T1 = Single.NaN
        arc.T2 = Single.NaN
        arc.T3 = Single.NaN
        arc.T4 = Single.NaN
        arc.T5 = Single.NaN
        arc.T6 = Single.NaN

        arc.P1 = Single.NaN
        arc.P2 = Single.NaN
        arc.P3 = Single.NaN
        arc.P4 = Single.NaN
        arc.P5 = Single.NaN
        arc.P6 = Single.NaN


        arc.M1 = Single.NaN
        arc.M2 = Single.NaN
        arc.M3 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.V1 = Single.NaN
        arc.V2 = Single.NaN
        arc.V3 = Single.NaN
        arc.V4 = Single.NaN
        arc.V5 = Single.NaN
        arc.V6 = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN
        arc.Q3 = Single.NaN
        arc.Q4 = Single.NaN
        arc.Q5 = Single.NaN
        arc.Q6 = Single.NaN
        arc.archType = 2
        isTArchToDBWrite = False
    End Sub
    Protected Sub clearArchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue
        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""


        arc.ERRTIME1 = 0
        arc.ERRTIME2 = 0
        arc.WORKTIME1 = 0
        arc.WORKTIME2 = 0
        arc.OKTIME1 = 0
        arc.OKTIME2 = 0



        arc.T1 = Single.NaN
        arc.T2 = Single.NaN
        arc.T3 = Single.NaN
        arc.T4 = Single.NaN
        arc.T5 = Single.NaN
        arc.T6 = Single.NaN

        arc.P1 = Single.NaN
        arc.P2 = Single.NaN
        arc.P3 = Single.NaN
        arc.P4 = Single.NaN
        arc.P5 = Single.NaN
        arc.P6 = Single.NaN


        arc.M1 = Single.NaN
        arc.M2 = Single.NaN
        arc.M3 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.V1 = Single.NaN
        arc.V2 = Single.NaN
        arc.V3 = Single.NaN
        arc.v4 = Single.NaN
        arc.v5 = Single.NaN
        arc.v6 = Single.NaN

        arc.V1H = Single.NaN
        arc.V2H = Single.NaN
        arc.V3H = Single.NaN
        arc.v4H = Single.NaN
        arc.v5H = Single.NaN
        arc.v6H = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN
        arc.Q3 = Single.NaN
        arc.Q4 = Single.NaN
        arc.Q5 = Single.NaN
        arc.Q6 = Single.NaN


        arc.Q1H = Single.NaN
        arc.Q2H = Single.NaN
        arc.Q3H = Single.NaN
        arc.Q4H = Single.NaN
        arc.Q5H = Single.NaN
        arc.Q6H = Single.NaN
        arc.archType = 0
        isArchToDBWrite = False
    End Sub

    Protected Sub clearMArchive(ByRef arc As MArchive)
        arc.DateArch = DateTime.MinValue
        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""


        arc.ERRTIME1 = 0
        arc.ERRTIME2 = 0
        arc.WORKTIME1 = 0
        arc.WORKTIME2 = 0
        arc.OKTIME1 = 0
        arc.OKTIME2 = 0



        arc.t1 = Single.NaN
        arc.t2 = Single.NaN
        arc.t3 = Single.NaN
        arc.t4 = Single.NaN
        arc.t5 = Single.NaN
        arc.t6 = Single.NaN

        arc.p1 = Single.NaN
        arc.p2 = Single.NaN
        arc.p3 = Single.NaN
        arc.p4 = Single.NaN
        arc.p5 = Single.NaN
        arc.p6 = Single.NaN


        arc.M1 = Single.NaN
        arc.M2 = Single.NaN
        arc.M3 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.V1 = Single.NaN
        arc.V2 = Single.NaN
        arc.V3 = Single.NaN
        arc.V4 = Single.NaN
        arc.V5 = Single.NaN
        arc.V6 = Single.NaN


        arc.G1 = Single.NaN
        arc.G2 = Single.NaN
        arc.G3 = Single.NaN
        arc.G4 = Single.NaN
        arc.G5 = Single.NaN
        arc.G6 = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN
        arc.Q3 = Single.NaN
        arc.Q4 = Single.NaN
        arc.Q5 = Single.NaN
        arc.Q6 = Single.NaN




        arc.archType = 1
        isMArchToDBWrite = False
    End Sub
    Protected tArch As TArchive
    Protected Arch As Archive
    Protected mArch As MArchive

    Public Overridable Function WriteTArchToDB() As String
        Dim sOUt As String = ""
        sOUt = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,TSUM1,TSUM2,errtime,errtime2,oktime,oktime2,hc_code,hc,hc_1,hc_2) values ("
        sOUt = sOUt + DeviceID.ToString() + ","
        sOUt = sOUt + "SYSDATE" + ","
        sOUt = sOUt + OracleDate(tArch.DateArch) + ","
        sOUt = sOUt + OracleDate(tArch.DateArch) + ","
        sOUt = sOUt + tArch.archType.ToString() + ","
        sOUt = sOUt + NanFormat(tArch.T1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T6, "##############0.000000").Replace(",", ".") + ","

        sOUt = sOUt + NanFormat(tArch.P1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.ERRTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.ERRTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.OKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.OKTIME2, "##############0.000000").Replace(",", ".") + ","



        If DeCodeHCNumber(tArch.HCtv1, 1) = "" And DeCodeHCNumber(tArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(tArch.HCtv1, 1) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(tArch.HCtv2, 2) + "','" + S180("К2:" + DeCodeHCText(tArch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(tArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(tArch.HCtv1, 1) + "','" + S180("К1:" + DeCodeHCText(tArch.HCtv1)) + "',"
        Else
            sOUt = sOUt + "'" + S180(DeCodeHCNumber(tArch.HCtv1, 1) + DeCodeHCNumber(tArch.HCtv2, 2)) + "','" + S180("К1:" + DeCodeHCText(tArch.HCtv1) + " К2:" + DeCodeHCText(tArch.HCtv2)) + "',"
        End If

        'sOUt = sOUt + "'" + DeCodeHCNumber(tArch.HCtv1, 1) + ";" + DeCodeHCNumber(tArch.HCtv2, 2) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(tArch.HCtv1)) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(tArch.HCtv2)) + "'"
        sOUt = sOUt + ")"
        Return sOUt
    End Function


    Public Overridable Function WriteArchToDB() As String
        Dim sOUt As String = ""
        sOUt = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,SP_TB1,SP_TB2,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,v1H,v2H,v4H,v5H,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,q1H,q2H,TSUM1,TSUM2,errtime,errtime2,oktime,oktime2,hc_code,hc,hc_1,hc_2) values ("
        sOUt = sOUt + DeviceID.ToString() + ","
        sOUt = sOUt + "SYSDATE" + ","
        sOUt = sOUt + OracleDate(Arch.DateArch) + ","
        sOUt = sOUt + OracleDate(Arch.DateArch) + ","
        sOUt = sOUt + Arch.archType.ToString() + ","
        sOUt = sOUt + Arch.SPtv1.ToString + ","
        sOUt = sOUt + Arch.SPtv2.ToString + ","
        sOUt = sOUt + NanFormat(Arch.T1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tx1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tx2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tair1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tair2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V1H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V2H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v4H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v5H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q1H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q2H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.ERRTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.ERRTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.OKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.OKTIME2, "##############0.000000").Replace(",", ".") + ","



        If DeCodeHCNumber(Arch.HCtv1, 1) = "" And DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(Arch.HCtv1, 1) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(Arch.HCtv2, 2) + "','" + S180("K2:" + DeCodeHCText(Arch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + "','" + S180("К1:" + DeCodeHCText(Arch.HCtv1)) + "',"
        Else
            sOUt = sOUt + "'" + S180(DeCodeHCNumber(Arch.HCtv1, 1) + DeCodeHCNumber(Arch.HCtv2, 2)) + "','" + S180("К1:" + DeCodeHCText(Arch.HCtv1) + " К2: " + DeCodeHCText(Arch.HCtv2)) + "',"
        End If

        'sOUt = sOUt + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + ";" + DeCodeHCNumber(Arch.HCtv2, 2) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(Arch.HCtv1)) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(Arch.HCtv2)) + "'"
        sOUt = sOUt + ")"
        Return sOUt
    End Function
    Public Overridable Function WriteMArchToDB() As String
        Dim sOUt As String = ""
        sOUt = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,p6,g1,g2,g3,g4,g5,g6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,TSUM1,TSUM2,errtime,errtime2,oktime,oktime2,hc_code,hc,hc_1,hc_2) values ("
        sOUt = sOUt + DeviceID.ToString() + ","
        sOUt = sOUt + "SYSDATE" + ","
        sOUt = sOUt + OracleDate(mArch.DateArch) + ","
        sOUt = sOUt + OracleDate(mArch.DateArch) + ","
        sOUt = sOUt + mArch.archType.ToString() + ","
        sOUt = sOUt + NanFormat(mArch.t1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tx1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tx2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tair1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tair2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.ERRTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.ERRTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.OKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.OKTIME2, "##############0.000000").Replace(",", ".") + ","



        If DeCodeHCNumber(mArch.HCtv1, 1) = "" And DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(mArch.HCtv1, 1) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(mArch.HCtv2, 2) + "','" + S180("К2:" + DeCodeHCText(mArch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + "','" + S180("К1:" + DeCodeHCText(mArch.HCtv1)) + "',"
        Else
            sOUt = sOUt + "'" + S180(DeCodeHCNumber(mArch.HCtv1, 1) + DeCodeHCNumber(mArch.HCtv2, 2)) + "','" + S180("К1:" + DeCodeHCText(mArch.HCtv1) + " К2:" + DeCodeHCText(mArch.HCtv2)) + "',"
        End If

        'sOUt = sOUt + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + ";" + DeCodeHCNumber(mArch.HCtv2, 2) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(mArch.HCtv1)) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(mArch.HCtv2)) + "'"
        sOUt = sOUt + ")"
        Return sOUt

    End Function


    Public Overridable Function DeCodeHCNumber(ByVal CodeHC As Long, Optional inputnumber As Integer = 0) As String
        If CodeHC = 0 Then Return ""
        Return CodeHC.ToString()

    End Function
    Public Overridable Function DeCodeHCText(ByVal CodeHC As Long) As String
        If CodeHC = 0 Then Return ""
        Return CodeHC.ToString()

    End Function
    Public Overridable Function DeCodeHC(ByVal CodeHC As Long) As String
        If CodeHC = 0 Then Return ""
        Return CodeHC.ToString()

    End Function

    Protected Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() +
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Protected Function S180(ByVal s As String) As String

        Dim outs As String
        outs = s
        If outs.Length <= 180 Then
            Return outs
        End If
        outs = outs.Substring(0, 180)
        Return outs
    End Function

End Class
