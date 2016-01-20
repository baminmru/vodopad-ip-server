Imports System.IO
Imports System.Threading

Public MustInherit Class TVDriver
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
            MyTransport.DisConnect()
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
    Public MustOverride Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
     ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
    Public MustOverride Function WriteArchToDB() As String
    Public MustOverride Function WriteMArchToDB() As String
    Public MustOverride Function WriteTArchToDB() As String
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

    Public Function NanFormat(ByVal n As Single, ByVal fStr As String) As String
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

End Class
