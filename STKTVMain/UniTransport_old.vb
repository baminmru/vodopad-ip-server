Option Explicit On
Imports System.IO.Ports

Public MustInherit Class TransportSetupData
    Protected mBaudRate As Integer
    Public Overridable Property BaudRate() As Integer
        Get
            Return mBaudRate
        End Get
        Set(ByVal value As Integer)
            mBaudRate = value
        End Set
    End Property
End Class

Public MustInherit Class UniTransport
    Public MustOverride Function TransportType() As String
    Protected mIsConnected As Boolean
    Public MustOverride Function SetupData() As TransportSetupData
    Public MustOverride Function SetupTransport(ByRef SetupData As TransportSetupData) As Boolean
    Public MustOverride Function Connect() As Boolean
    Public MustOverride Function DisConnect() As Boolean
    Public Overridable ReadOnly Property IsConnected() As Boolean
        Get
            Return mIsConnected
        End Get
    End Property

    Public MustOverride Function BytesToRead() As Integer
    Public MustOverride Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
    Public MustOverride Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
    Public MustOverride Sub Close()
End Class


Public Class SerialTransportSetupData
    Inherits TransportSetupData

    'Public BaudRate As Integer
    Public DataBits As Integer
    Public DtrEnable As Boolean
    Public Handshake As System.IO.Ports.Handshake
    Public Parity As System.IO.Ports.Parity
    Public PortName As String
    Public RtsEnable As Boolean
    Public StopBits As System.IO.Ports.StopBits

End Class


Public Class SerialTransport

    Inherits UniTransport
    Private Port As System.IO.Ports.SerialPort
    Private mData As SerialTransportSetupData


    Public Sub New()
        mData = New SerialTransportSetupData
        Port = New SerialPort
    End Sub

    Protected Overrides Sub Finalize()
        'If Port.IsOpen Then
        '    Port.Close()
        'End If

        'Port = Nothing
        'mData = Nothing
    End Sub

    Public Overrides Sub Close()
        If Port.IsOpen Then
            Port.Close()
        End If

        Port = Nothing
        mData = Nothing
    End Sub

    Public Overrides Function BytesToRead() As Integer
        Return Port.BytesToRead
    End Function

    Public Overrides Function Connect() As Boolean
        Try
            SetupTransport(SetupData)
            Port.Open()
            mIsConnected = Port.IsOpen
            Return mIsConnected
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function DisConnect() As Boolean
        Try
            Port.Close()
            mIsConnected = Port.IsOpen
            Return Not mIsConnected
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer

        Return Port.Read(buffer, offset, count)

    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef vSetupData As TransportSetupData) As Boolean
        Try
            mData = vSetupData

            If Port.IsOpen Then Port.Close()
            Port.BaudRate = mData.BaudRate
            Port.DtrEnable = mData.DtrEnable
            Port.Handshake = mData.Handshake
            Port.Parity = mData.Parity
            Port.PortName = mData.PortName
            Port.RtsEnable = mData.RtsEnable
            Port.StopBits = mData.StopBits
            Return True
        Catch
            Return False
        End Try

    End Function

    Public Overrides Function TransportType() As String
        Return "DIRECT"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
        Port.Write(buffer, offset, count)
    End Sub
End Class

Public Class NportTransportSetupData
    Inherits TransportSetupData
    'connect setup data
    Public IPAddress As String
    'Public Password As String
    Public Timeout As Integer = 1000

    ' com setup data
    'Public BaudRate As Integer
    Public DataBits As Integer
    Public DtrEnable As Boolean
    Public Handshake As System.IO.Ports.Handshake
    Public Parity As System.IO.Ports.Parity
    Public RtsEnable As Boolean
    Public StopBits As System.IO.Ports.StopBits
End Class

Public Class NportTransport
    Inherits UniTransport

    Private mData As NportTransportSetupData
    Private Port_id As Integer = -1
    Private Shared OpenCount As Integer = 0
    Private inBuffer(0 To 32000) As Byte
    Private ReadCount As Integer = 0

    Public Sub New()
        mData = New NportTransportSetupData
        If OpenCount = 0 Then
            IPSerial.nsio_init()
        End If
        OpenCount += 1

    End Sub

    Protected Overrides Sub Finalize()
        
        'OpenCount -= 1
        'If OpenCount = 0 Then
        '    IPSerial.nsio_end()
        'End If

        'mData = Nothing
    End Sub

    Public Overrides Sub Close()
        OpenCount -= 1
        If OpenCount = 0 Then
            IPSerial.nsio_end()
        End If

        mData = Nothing
    End Sub


    Private Sub TryRead()
        Dim tmpBuffer(0 To 32000) As Byte
        Dim tmpReadCount As Integer
        Dim tmpSize As Integer
        Dim i As Integer
        If ReadCount = 32000 Then Exit Sub
        tmpSize = 32000 - ReadCount
        tmpReadCount = IPSerial.nsio_read(Port_id, tmpBuffer(0), tmpSize)
        If tmpReadCount > 0 Then
            For i = 0 To tmpReadCount - 1
                inBuffer(ReadCount) = tmpBuffer(i)
                ReadCount += 1
            Next
        End If
    End Sub

    Public Overrides Function BytesToRead() As Integer
        TryRead()
        Return ReadCount
    End Function

    Public Overrides Function Connect() As Boolean

        Port_id = IPSerial.nsio_open(mData.IPAddress, 1, mData.Timeout)
        If Port_id >= 0 Then

            Dim ret, Sb, Db, Pt, Fc As Short
            Dim br As Short

            Select Case mData.BaudRate
                Case 50
                    br = 0
                Case 75
                    br = 1
                Case 110
                    br = 2
                Case 134
                    br = 3
                Case 150
                    br = 4
                Case 300
                    br = 5
                Case 600
                    br = 6
                Case 1200
                    br = 7
                Case 2400
                    br = 9
                Case 4800
                    br = 10
                Case 7200
                    br = 11
                Case 9600
                    br = 12
                Case 19200
                    br = 13
                Case 38400
                    br = 14
                Case 57600
                    br = 15
                Case 115200
                    br = 16
                Case 230400
                    br = 17
                Case 460800
                    br = 18
                Case 921600
                    br = 19
            End Select


            Select Case mData.DataBits
                Case 8
                    Db = 3
                Case 7
                    Db = 2
                Case 6
                    Db = 1
                Case 5
                    Db = 0
            End Select

            Select Case mData.Parity
                Case Parity.None
                    Pt = 0
                Case Parity.Even
                    Pt = 8
                Case Parity.Odd
                    Pt = 16
                Case Parity.Mark
                    Pt = 24
                Case Parity.Space
                    Pt = 32
            End Select

            Select Case mData.Handshake
                Case Handshake.None
                    Fc = 0
                Case Handshake.RequestToSend
                    Fc = 3
                Case Handshake.XOnXOff
                    Fc = 12
                Case Handshake.RequestToSendXOnXOff
                    Fc = 15
            End Select

            Select Case mData.StopBits
                Case 1
                    Sb = 0
                Case 2
                    Sb = 4
            End Select
            Port_id = 0
            ret = nsio_ioctl(Port_id, br, Db Or Sb Or Pt)
            If ret <> NSIO_OK Then
                Return False
            End If

            'ret = nsio_baud(Port_id, mData.BaudRate)

            ret = nsio_flowctrl(Port_id, Fc)

            ret = IPSerial.nsio_baud(Port_id, br)
            If ret <> NSIO_OK Then
                Return False
            End If

            mIsConnected = True
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function DisConnect() As Boolean
        IPSerial.nsio_close(Port_id)
        mIsConnected = False
        Return mIsConnected
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
        TryRead()
        Dim i As Integer
        Dim CntRead As Integer
        If ReadCount <= count Then
            CntRead = ReadCount
        Else
            CntRead = count
        End If

        For i = 0 To CntRead - 1
            buffer(i + offset) = inBuffer(i)
        Next



        ReadCount -= CntRead
        If ReadCount > 0 Then
            For i = 0 To ReadCount - 1
                inBuffer(i) = inBuffer(CntRead + i)
            Next
        End If
        Return CntRead
    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef SetupData As TransportSetupData) As Boolean
        mData = SetupData
    End Function

    Public Overrides Function TransportType() As String
        Return "NORT"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
        IPSerial.nsio_write(Port_id, buffer(offset), count)
    End Sub
End Class



Public Class ModemTransportSetupData
    Inherits SerialTransportSetupData
    Public InitCommand As String
    Public CallCommand As String
End Class
Public Class ModemTransport
    Inherits UniTransport

    Private Port As System.IO.Ports.SerialPort
    Private mData As ModemTransportSetupData


    Public Sub New()
        mData = New ModemTransportSetupData
        Port = New SerialPort
    End Sub

    Protected Overrides Sub Finalize()
        'If Port.IsOpen Then
        '    Port.Close()
        'End If

        'Port = Nothing
        'mData = Nothing
    End Sub

    Public Overrides Sub Close()
        If Port.IsOpen Then
            Port.Close()
        End If

        Port = Nothing
        mData = Nothing
    End Sub

    Public Overrides Function BytesToRead() As Integer
        Return Port.BytesToRead
    End Function

    Private Function WaitOK(Optional ByVal WaitStr As String = "OK", Optional ByVal Timeout As Integer = 30000) As Boolean

        Dim i As Int16
        Dim j As Int16
        'For i = 1 To 600


        'Next
        Dim bufStr As String = ""
        Dim btr As Long

        Dim buf(2000) As Byte

        Dim sz As Long = 0



        sz = 0
        For i = 1 To 100
            System.Threading.Thread.Sleep(timeout / 100)



            btr = Port.BytesToRead
            If btr > 0 Then

                Port.Read(buf, sz, btr)
                sz += btr
                If sz > Len(WaitStr) Then Exit For
            End If
        Next
        bufStr = ""
        buf = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 0, sz)
        For j = 0 To sz - 1
            bufStr = bufStr + Chr(buf(j))
        Next
        bufStr = bufStr.Replace(vbCrLf, "")
        If bufStr.Substring(0, WaitStr.Length).ToLower() <> WaitStr.ToLower() Then
            Return False
        End If
        Return True




    End Function
    Private Sub ReadAll()
        Dim bufStr As String = ""
        Dim btr As Long

        Dim buf(2000) As Byte
        System.Threading.Thread.Sleep(100)
        Dim sz As Long = 0
        btr = Port.BytesToRead
        While btr > 0
            Port.Read(buf, 0, btr)
            System.Threading.Thread.Sleep(10)
            btr = Port.BytesToRead
        End While
    End Sub

    Public Overrides Function Connect() As Boolean
        Try
            Port.Open()
            If Port.IsOpen Then

                Port.Write("ATE0" & vbCrLf)
                ReadAll()
                
                If mData.InitCommand <> "" Then
                    Port.Write(mData.InitCommand)
                    If Not WaitOK("OK", 1000) Then
                        Return False
                    End If
                End If
                ReadAll()

                Port.Write("ATDP" & mData.CallCommand.Replace("-", "") & vbCrLf)
                mIsConnected = WaitOK("CONNECT", 59000)
                ReadAll()


            End If



            Return mIsConnected
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function DisConnect() As Boolean
        Try
            Port.Write("+++" & vbCrLf)
            ReadAll()
            System.Threading.Thread.Sleep(100)
            Port.Write("ATH0" & vbCrLf)
            ReadAll()
            Port.Close()
            mIsConnected = Port.IsOpen
            Return Not mIsConnected
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer

        Return Port.Read(buffer, offset, count)

    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef vSetupData As TransportSetupData) As Boolean
        Try
            mData = vSetupData

            If Port.IsOpen Then Port.Close()
            Port.BaudRate = mData.BaudRate
            Port.DtrEnable = mData.DtrEnable
            Port.Handshake = mData.Handshake
            Port.Parity = mData.Parity
            Port.PortName = mData.PortName
            Port.RtsEnable = mData.RtsEnable
            Port.StopBits = mData.StopBits
            Return True
        Catch ex As System.Exception
            Return False
        End Try

    End Function

    Public Overrides Function TransportType() As String
        Return "MODEM"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
        Port.Write(buffer, offset, count)
    End Sub
End Class


Public Class VortexTransportSetupData
    Inherits TransportSetupData
    'connect setup data
    Public Host As String
    Public Port As Integer = 2001
    Public Timeout As Integer = 1000

    Public Sub New()

    End Sub
End Class

Public Class VortexTransport
    Inherits UniTransport

    Private mData As VortexTransportSetupData
    Private inBuffer(0 To 32000) As Byte
    Private ReadCount As Integer = 0
    Private soc As System.Net.Sockets.Socket

    Public Sub New()
        mData = New VortexTransportSetupData
       

        soc = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
        soc.Blocking = False
    End Sub

    Protected Overrides Sub Finalize()

        'If soc.Connected Then
        '    Try
        '        soc.Close()
        '    Catch ex As Exception

        '    End Try
        'End If
        'soc = Nothing

        'mData = Nothing
    End Sub

    Public Overrides Sub Close()

        If soc.Connected Then
            Try
                soc.Close()
            Catch ex As Exception

            End Try
        End If
        soc = Nothing

        mData = Nothing
    End Sub

    Private Sub TryRead()
        Dim tmpBuffer(0 To 32000) As Byte
        Dim tmpReadCount As Integer
        Dim tmpSize As Integer
        Dim i As Integer
        If ReadCount = 32000 Then Exit Sub
        tmpSize = 32000 - ReadCount
        If soc.Available > 0 Then
            If tmpSize > soc.Available Then
                tmpSize = soc.Available
            End If
            tmpReadCount = soc.Receive(tmpBuffer, tmpSize, Net.Sockets.SocketFlags.None)
            If tmpReadCount > 0 Then
                For i = 0 To tmpReadCount - 1
                    inBuffer(ReadCount) = tmpBuffer(i)
                    ReadCount += 1
                Next
            End If
        End If
    End Sub

    Public Overrides Function BytesToRead() As Integer
        TryRead()
        Return ReadCount
    End Function

    Public Overrides Function Connect() As Boolean

        Try
            soc.Connect(mData.Host, mData.Port)

        Catch
        End Try

        If soc.Connected Then
            mIsConnected = True

            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function DisConnect() As Boolean
        If (soc.Connected) Then
            soc.Close()
        End If

        mIsConnected = False
        Return mIsConnected
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
        TryRead()
        Dim i As Integer
        Dim CntRead As Integer
        If ReadCount < count Then
            CntRead = ReadCount
        Else
            CntRead = count
        End If

        For i = 0 To CntRead - 1
            buffer(i + offset) = inBuffer(i)
        Next



        ReadCount -= CntRead
        If ReadCount > 0 Then
            For i = 0 To ReadCount - 1
                inBuffer(i) = inBuffer(CntRead + i)
            Next
        End If
	return CntRead

    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef SetupData As TransportSetupData) As Boolean
        mData = SetupData
    End Function

    Public Overrides Function TransportType() As String
        Return "VORTEX"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
        soc.Send(buffer, offset, count, Net.Sockets.SocketFlags.None)
    End Sub
End Class