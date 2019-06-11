Option Explicit On
Imports System.IO.Ports
Imports System.Xml

Public Class TransportSetupData
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

Public Enum UnitransportAction
    NoAction = -1
    Connecting = 0
    Connected = 1
    SendData = 2
    ReceiveData = 3
    Disconnecting = 4
    Disconnected = 5
    Wait = 6
    WakeUp = 10
    Destroy = 11
    SettingUp = 12
    LowLevelStop = 13
End Enum

Public MustInherit Class UniTransport
    Public Event Idle()
    Protected mCancelNow As Boolean = False
    Protected mError As String = ""
    Protected mSessionID As Guid = Guid.Empty
    Protected Shared m_LogEnabled As Boolean = False
    Protected Shared m_Inited As Boolean = False

    Protected Shared Sub Init()
        If m_Inited Then Exit Sub
        Dim xml As XmlDocument
        xml = New XmlDocument
        xml.Load(GetMyDir() + "\Config.xml")
        Dim node As XmlElement
        node = xml.FirstChild()

        Try
            m_LogEnabled = (node.Attributes.GetNamedItem("LogEnabled").Value.ToLower() = "true")
        Catch
            m_LogEnabled = False
        End Try
        m_Inited = True
    End Sub


    Public Sub CancelNow()
        mCancelNow = True
    End Sub


  

    Protected Shared Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function



    Protected Sub LOG(ByVal s As String)
        Init()
        If m_LogEnabled Then
            Try
                System.IO.File.AppendAllText(GetMyDir() + "\LOGS\" + TransportType() + "_LOG_" + Date.Now.ToString("yyyyMMdd") + "_" + SessionID + ".txt", Date.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + " " + s + vbCrLf)

            Catch ex As Exception

            End Try
            Debug.Print(s)
            Console.WriteLine(s)
        End If
    End Sub

    Public MustOverride Function TransportType() As String
    Protected mIsConnected As Boolean
    Public Event TransportAction(ByVal Action As UnitransportAction, ByVal msg As String)
    Public MustOverride Function SetupData() As TransportSetupData
    Public MustOverride Function SetupTransport(ByRef SetupData As TransportSetupData) As Boolean
    Public MustOverride Function Connect() As Boolean
    Public MustOverride Function DisConnect() As Boolean
    Public MustOverride Sub CleanPort()

    Public Sub SendEvent(ByVal Action As UnitransportAction, ByVal MSG As String)
        If Action = UnitransportAction.LowLevelStop Then
            mError = MSG
        End If

        If MSG <> "" Then
        LOG(Action.ToString() + " " + MSG)
        End If

        RaiseEvent TransportAction(Action, MSG)
    End Sub

    Public Sub SendIdle()
        RaiseEvent Idle()
    End Sub
    Public Overridable ReadOnly Property IsConnected() As Boolean
        Get
            Return mIsConnected
        End Get
    End Property


    Public ReadOnly Property SessionID() As String
        Get
            If mSessionID.Equals(Guid.Empty) Then
                mSessionID = Guid.NewGuid
            End If
            Return mSessionID.ToString()
        End Get
    End Property

    Public Overridable ReadOnly Property GetError() As String
        Get
            Return mError
        End Get
    End Property

    Public BytesSent As Integer = 0
    Public BytesReceived As Integer = 0
    Public PortBusy As Boolean = False

    Public Sub New(Optional ByVal _LogEnabled As Boolean = False)
        m_LogEnabled = _LogEnabled
    End Sub

    Public MustOverride Function BytesToRead() As Integer
    Public MustOverride Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
    Public MustOverride Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)
End Class


Public Class SerialTransportSetupData
    Inherits TransportSetupData


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
        SendEvent(UnitransportAction.WakeUp, "")
        mData = New SerialTransportSetupData
        Port = New SerialPort
    End Sub

    Protected Overrides Sub Finalize()
        If Port.IsOpen Then
            SendEvent(UnitransportAction.Disconnecting, "")
            Port.Close()
            SendEvent(UnitransportAction.Disconnected, "")
        End If

        Port = Nothing
        mData = Nothing
    End Sub


    Public Overrides Function BytesToRead() As Integer
        Return Port.BytesToRead
    End Function

    Public Overrides Function Connect() As Boolean
        Try
            SendEvent(UnitransportAction.Connecting, "")
            SetupTransport(SetupData)
            Port.Open()
            mIsConnected = Port.IsOpen
            If mIsConnected Then
                SendEvent(UnitransportAction.Connected, "")
            End If


            Log("DTR =0")
            Port.DtrEnable = False
            System.Threading.Thread.Sleep(1000)
            SendIdle()

            Port.RtsEnable = False
            System.Threading.Thread.Sleep(1000)
            SendIdle()

            Log("DTR =1")
            Port.DtrEnable = True
            System.Threading.Thread.Sleep(1000)
            SendIdle()

            Port.RtsEnable = True
            System.Threading.Thread.Sleep(1000)
            SendIdle()



            Return mIsConnected
        Catch ex As Exception
            SendEvent(UnitransportAction.LowLevelStop, "Порт " + Port.PortName + "(уже) занят! " + ex.Message)
            PortBusy = True
            Return False
        End Try
    End Function

    Public Overrides Function DisConnect() As Boolean
        Try
            Log("RTS =0")
            Port.RtsEnable = False
            System.Threading.Thread.Sleep(1000)

            Log("DTR =0")
            Port.DtrEnable = False
            System.Threading.Thread.Sleep(1000)
            SendIdle()
            SendEvent(UnitransportAction.Disconnecting, "")
            Port.Close()
            SendEvent(UnitransportAction.Disconnected, "")
            mIsConnected = Port.IsOpen
            Return Not mIsConnected
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
        Dim i As Integer
        If (Port.BytesToRead > 0) Then
            Dim cnt As Integer
            If Port.BytesToRead > count Then
                cnt = count
            Else
                cnt = Port.BytesToRead
            End If
            i = Port.Read(buffer, offset, cnt)
            BytesReceived += cnt
            If i > 0 Then
                SendEvent(UnitransportAction.ReceiveData, "")
            End If


            Dim bufstr As String = ""
            Dim j As Integer
            'Try
            '    Dim buf2() As Byte
            '    buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buffer, offset, count)
            For j = 0 To i
                bufstr = bufstr + Hex(buffer(offset + j)) + " "
            Next
            If bufstr <> "" Then
                Log(">>r" + i.ToString + " (" + bufstr + ")")
            End If
            Return i
        Else
            Return 0
        End If

    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef vSetupData As TransportSetupData) As Boolean
        Try
            mData = CType(vSetupData, SerialTransportSetupData)

            If Port.IsOpen Then Port.Close()
            Port.BaudRate = mData.BaudRate
            Port.Handshake = Handshake.None
            Port.Parity = mData.Parity
            Port.PortName = mData.PortName
            Port.StopBits = mData.StopBits
            SendEvent(UnitransportAction.SettingUp, "")
            Return True
        Catch
            Return False
        End Try

    End Function

    Public Overrides Function TransportType() As String
        Return "DIRECT"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)


        Port.DtrEnable = True
        Port.RtsEnable = True
        Dim bufstr As String = ""
        Dim j As Integer
        For j = 0 To count - 1
            bufstr = bufstr + Hex(buffer(offset + j)) + " "
        Next
        Log(">>w" + count.ToString + " (" + bufstr + ")")
        System.Threading.Thread.Sleep(10)
        SendIdle()
        Port.Write(buffer, offset, count)
        BytesSent += count
        SendEvent(UnitransportAction.SendData, "")

        System.Threading.Thread.Sleep(10)
        SendIdle()
    End Sub

    Public Overrides Sub CleanPort()
        Port.DiscardInBuffer()
        'Log("iX clean input buffer")
        Port.DiscardOutBuffer()
        'Log("oX clean output buffer")
    End Sub
End Class

Public Class NportTransportSetupData
    Inherits TransportSetupData
    'connect setup data
    Public IPAddress As String
    'Public Password As String
    Public Timeout As Integer = 1000
    Public ComPortIdx As Integer = 1


    ' com setup data

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
        SendEvent(UnitransportAction.WakeUp, "")
        mData = New NportTransportSetupData
        If OpenCount = 0 Then
            Try
                IPSerial.nsio_init()
            Catch ex As Exception
                SendEvent(UnitransportAction.LowLevelStop, "NPORT INIT: " + ex.Message)
            End Try

        End If
        OpenCount += 1

    End Sub

    Protected Overrides Sub Finalize()

        OpenCount -= 1
        If OpenCount = 0 Then
            Try
            IPSerial.nsio_end()
            Catch ex As Exception

            End Try

        End If
        SendEvent(UnitransportAction.Destroy, "")
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
            BytesReceived += tmpReadCount
            SendEvent(UnitransportAction.ReceiveData, "")
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
        SendEvent(UnitransportAction.Connecting, "")
        Try
            Port_id = IPSerial.nsio_open(mData.IPAddress, mData.ComPortIdx, mData.Timeout)
        Catch ex As Exception
            Port_id = -1
            SendEvent(UnitransportAction.LowLevelStop, "NPORT connecting error")
        End Try

        If Port_id >= 0 Then
            SendEvent(UnitransportAction.Connected, "")
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
                    Fc = F_NONE
                Case Handshake.RequestToSend
                    Fc = F_CTS
                Case Handshake.XOnXOff
                    Fc = F_XON + F_XOFF
                Case Handshake.RequestToSendXOnXOff
                    Fc = F_XON + F_XOFF + F_CTS + F_RTS
            End Select

            Select Case mData.StopBits
                Case 1
                    Sb = 0
                Case 2
                    Sb = 4
            End Select

            IPSerial.nsio_DTR(Port_id, 1)

            ret = nsio_ioctl(Port_id, br, Db Or Sb Or Pt)
            If ret <> NSIO_OK Then
                Return False
            End If



            ret = nsio_flowctrl(Port_id, Fc)

            IPSerial.nsio_baud(Port_id, br)
            IPSerial.nsio_RTS(Port_id, 1)

            SendEvent(UnitransportAction.SettingUp, "")



            mIsConnected = True
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Function DisConnect() As Boolean
        SendEvent(UnitransportAction.Disconnecting, "")
        If Port_id >= 0 Then
            Try
                IPSerial.nsio_RTS(Port_id, 0)
                IPSerial.nsio_DTR(Port_id, 0)
                IPSerial.nsio_close(Port_id)
            Catch ex As Exception

            End Try
        End If

        mIsConnected = False
        SendEvent(UnitransportAction.Disconnected, "")
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


        Dim bufstr As String = ""
        Dim j As Integer
        'Try
        '    Dim buf2() As Byte
        '    buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buffer, offset, count)
        For j = 0 To CntRead - 1
            bufstr = bufstr + Hex(buffer(offset + j)) + " "
        Next
        If bufstr <> "" Then
            Log(">>r" + CntRead.ToString + " (" + bufstr + ")")
        End If
        'Catch ex As Exception
        '    'Stop
        'End Try



        BytesReceived += CntRead
        SendEvent(UnitransportAction.ReceiveData, "")
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
        mData = CType(SetupData, NportTransportSetupData)
    End Function

    Public Overrides Function TransportType() As String
        Return "NORT"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)

        IPSerial.nsio_DTR(Port_id, 1)
        IPSerial.nsio_RTS(Port_id, 1)
        IPSerial.nsio_write(Port_id, buffer(offset), count)
        BytesSent += count

        SendEvent(UnitransportAction.SendData, "")
        Dim bufstr As String = ""
        Dim j As Integer
        'Dim buf2() As Byte
        'buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buffer, offset, count)
        For j = 0 To count - 1
            bufstr = bufstr + Hex(buffer(offset + j)) + " "
        Next
        Log(">>w" + count.ToString + " (" + bufstr + ")")
    End Sub

    Public Overrides Sub CleanPort()
        IPSerial.nsio_flush(Port_id, 2)

        'Log("ioX clean buffers")
    End Sub
End Class



Public Class ModemTransportSetupData
    Inherits SerialTransportSetupData
    Public InitCommand As String
    Public Phone As String
    Public PhoneLineType As String = "T"
    Public ConnectLimit As Integer = 80
End Class
Public Class ModemTransport
    Inherits UniTransport


    Private WithEvents Port As System.IO.Ports.SerialPort
    Private mData As ModemTransportSetupData


    Public Sub New()
        SendEvent(UnitransportAction.WakeUp, "")
        mData = New ModemTransportSetupData
        Port = New SerialPort
        mCancelNow = False
    End Sub

    Protected Overrides Sub Finalize()

        If Port.IsOpen Then
            SendEvent(UnitransportAction.Disconnecting, "")
            DisConnect()
            Port.Close()
            SendEvent(UnitransportAction.Disconnected, "")
        End If

        Port = Nothing
        mData = Nothing
        SendEvent(UnitransportAction.Destroy, "")

    End Sub


    Public Overrides Function BytesToRead() As Integer
        Return Port.BytesToRead
    End Function


    Private Function WaitOK(ByVal CmdStr As String, Optional ByVal WaitStr As String = "OK", Optional ByVal Timeout As Integer = 30000, Optional ByRef ReceivedString As String = Nothing) As Boolean
        If Not IsConnected And Not Connecting Then
            Port.DiscardInBuffer()
            Port.DiscardOutBuffer()
            Return False
        End If
        Dim i As Int16
        Dim j As Int16
        Dim bufStr As String = ""
        Dim btr As Long

        Dim buf(32000) As Byte
        Dim buf1(32000) As Byte

        Dim sz As Long
        sz = 0
        SendEvent(UnitransportAction.Wait, "")
        For i = 1 To 100
            SendIdle()
            If mCancelNow Then
                SendEvent(UnitransportAction.LowLevelStop, "Cancelled by user")
                mIsConnected = False
                Log("Cancelled by user")
                Return False
            End If
            System.Threading.Thread.Sleep(Timeout / 100)
            SendIdle()
            If i Mod 10 = 0 Then Log("wait for answer " + i.ToString + "%")
            btr = Port.BytesToRead
            BytesReceived += btr
            If btr > 0 Then
                If sz + btr < 32000 Then
                    Port.Read(buf, sz, btr)
                    sz += btr
                Else
                    Port.Read(buf, sz, 32000 - 1 - sz)
                    sz = 32000
                End If

                bufStr = ""
                Try
                    buf1 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 0, sz)
                    For j = 0 To sz - 1
                        bufStr = bufStr + Chr(buf1(j))
                    Next
                Catch ex As Exception

                End Try

                bufStr = bufStr.Replace(vbCrLf, "")

                Log("<<wait for:" + WaitStr + "<< received:" + bufStr)

                If bufStr.ToLower().IndexOf(WaitStr.ToLower) >= 0 Then
                    If Not ReceivedString Is Nothing Then
                        ReceivedString = bufStr
                    End If
                    Return True
                End If

                If bufStr.Length = 0 Then
                    SendEvent(UnitransportAction.LowLevelStop, "Данные не получены (0 байт)")
                    mIsConnected = False
                    Return False
                End If

                If bufStr.IndexOf("ERROR") >= 0 Then
                    SendEvent(UnitransportAction.LowLevelStop, "COM->ERROR")
                    mIsConnected = False
                    Return False
                End If
                If bufStr.IndexOf("NO ANSWER") >= 0 Then
                    SendEvent(UnitransportAction.LowLevelStop, "COM->NO ANSWER")
                    mIsConnected = False
                    Return False
                End If


                If bufStr.IndexOf("NO DIALTONE") >= 0 Then
                    SendEvent(UnitransportAction.LowLevelStop, "COM->NO DIALTONE")
                    mIsConnected = False
                    Return False
                End If
                If bufStr.IndexOf("NO CARRIER") >= 0 Then
                    SendEvent(UnitransportAction.LowLevelStop, "COM->NO CARRIER")
                    mIsConnected = False
                    Return False
                End If
                If bufStr.IndexOf("BUSY") >= 0 Then
                    mIsConnected = False
                    SendEvent(UnitransportAction.LowLevelStop, "COM->BUSY")
                    Return False
                End If

                If bufStr.ToLower().IndexOf(WaitStr.ToLower) < 0 Then
                    Return False
                End If
            End If
        Next
        bufStr = ""
        Dim buf2() As Byte
        buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 0, sz)
        For j = 0 To sz - 1
            bufStr = bufStr + Chr(buf2(j))
        Next
        Log("<<" + WaitStr + "<<" + bufStr)
        bufStr = bufStr.Replace(vbCrLf, "")

        If bufStr.IndexOf("ERROR") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->ERROR")
            mIsConnected = False
            Return False
        End If
        If bufStr.IndexOf("NO ANSWER") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->NO ANSWER")
            mIsConnected = False
            Return False
        End If


        If bufStr.IndexOf("NO DIALTONE") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->NO DIALTONE")
            mIsConnected = False
            Return False
        End If
        If bufStr.IndexOf("NO CARRIER") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->NO CARRIER")
            mIsConnected = False
            Return False
        End If
        If bufStr.IndexOf("BUSY") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->BUSY")
            mIsConnected = False
            Return False
        End If

        If bufStr.ToLower().IndexOf(WaitStr.ToLower) < 0 Then
            Return False
        End If
        Return True

    End Function

    Private Sub ReadAll(Optional ByVal delay As Integer = 10)

        If Not IsConnected And Not Connecting Then
            Port.DiscardInBuffer()
            Port.DiscardOutBuffer()
            Return
        End If

        If mCancelNow Then
            Log("Cancelled by user")
            SendEvent(UnitransportAction.LowLevelStop, "Cancelled by user")
            Return
        End If

        Dim bufStr As String = ""
        Dim btr As Long

        Dim buf(2000) As Byte
        Dim sz As Long = 0

        System.Threading.Thread.Sleep(delay)
        SendIdle()
        btr = Port.BytesToRead
        While btr > 0

            Port.Read(buf, 0, btr)
            BytesReceived += btr
            sz += btr
            SendEvent(UnitransportAction.ReceiveData, "")

            System.Threading.Thread.Sleep(1 + CInt(1000 * 8 * 2 / Port.BaudRate))
            SendIdle()
            If mCancelNow Then
                Log("Cancelled by user")
                SendEvent(UnitransportAction.LowLevelStop, "Cancelled by user")
                Return
            End If
            btr = Port.BytesToRead
        End While
        bufStr = ""
        Dim buf2() As Byte
        buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 0, sz)
        Dim j As Integer
        For j = 0 To sz - 1
            If j < buf2.Length Then
            bufStr = bufStr + Chr(buf2(j))
            End If

        Next

        If bufStr.IndexOf("NO CARRIER") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->NO CARRIER")
            mIsConnected = False
        End If
        If bufStr.IndexOf("BUSY") >= 0 Then
            mIsConnected = False
            SendEvent(UnitransportAction.LowLevelStop, "COM->BUSY")
        End If
        Log("<<A<<" + bufStr)
    End Sub

    Private Connecting As Boolean = False
    Public Overrides Function Connect() As Boolean
        mCancelNow = False
        LOG("Connecting")
        Try
            Port.Open()
            If Port.IsOpen Then
                SendEvent(UnitransportAction.Connecting, "")


                Log("RTS =0")
                Port.RtsEnable = True

                System.Threading.Thread.Sleep(1000)
                SendIdle()
                Log("DTR =0")
                Port.DtrEnable = False

                System.Threading.Thread.Sleep(1000)
                SendIdle()
                Log("DTR =1")
                Port.DtrEnable = True

                Log("RTS =0")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1000)
                SendIdle()


                Log("RTS =1")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1000)
                SendIdle()

              

                System.Threading.Thread.Sleep(1000)
                SendIdle()
                Connecting = True

                Port.Write("atez" & vbCrLf)
                System.Threading.Thread.Sleep(1000)
                WaitOK("")


                WriteS("at&f" & vbCrLf)
                System.Threading.Thread.Sleep(1000)
                WaitOK("")



                If mData.InitCommand <> "" Then
                    WriteS(mData.InitCommand & vbCrLf)
                    ReadAll(1000)
                End If



                ' эхо
                WriteS("ate0" & vbCrLf)
                System.Threading.Thread.Sleep(1000)
                WaitOK("")

                If mData.PhoneLineType.ToUpper = "G" Then
                    WriteS("at+CFUN=1" & vbCrLf)
                    System.Threading.Thread.Sleep(10000)
                    WaitOK("")

                    'LOG("at+CREG=1" & vbCrLf)
                    WriteS("at+CREG=1" & vbCrLf)
                    System.Threading.Thread.Sleep(5000)
                    WaitOK("")

                    'LOG("AT+COPS=0" & vbCrLf)
                    WriteS("AT+COPS=0" & vbCrLf)
                    System.Threading.Thread.Sleep(5000)
                    WaitOK("")
                End If



                If mCancelNow Then
                    Return False
                End If
                Dim callnum As String
                If mData.PhoneLineType.ToUpper <> "G" Then
                    callnum = "ATD" & mData.PhoneLineType & mData.Phone.Replace("-", "") & vbCrLf
                Else
                    callnum = "ATD" & mData.Phone.Replace("-", "") & vbCrLf
                End If

                WriteS(callnum)
                If mCancelNow Then
                    Log("Cancelled by user")
                    Return False
                End If

                Dim RStr As String = ""


                mIsConnected = WaitOK("", "CONNECT", mData.ConnectLimit * 2000, RStr)


                If mIsConnected Then

                    If RStr <> "" Then
                        If Not RStr.ToLower.StartsWith(("CONNECT " & mData.BaudRate.ToString).ToLower) Then
                            SendEvent(UnitransportAction.LowLevelStop, "Соединение на неверной скорости. Ожидалось:" + mData.BaudRate.ToString + " Получено:" + RStr)

                        End If
                    End If
                End If

                'mIsConnected = WaitOK("", "CONNECT " & mData.BaudRate.ToString, mData.ConnectLimit * 1000)
                ReadAll(100)

                Log("RTS =0")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1000)
                SendIdle()


                Log("RTS =1")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1000)
                SendIdle()

                If mIsConnected Then
                    SendEvent(UnitransportAction.Connected, "")
                End If

                Connecting = False
            End If

            Return mIsConnected
        Catch ex As Exception
            SendEvent(UnitransportAction.NoAction, "Порт " + Port.PortName + "(уже) занят! " + ex.Message)
            PortBusy = True
            Log(ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function DisConnect() As Boolean
        mCancelNow = False
        Try
            Dim ok As Boolean = False
            Dim cnt As Integer = 0

            SendEvent(UnitransportAction.Disconnecting, "")

            'While cnt < 2 And Not ok

            ReadAll(100)
            CleanPort()


            If mData.PhoneLineType.ToUpper = "G" Then

             Log("RTS =0")
            Port.RtsEnable = True
            System.Threading.Thread.Sleep(1000)
            SendIdle()

            System.Threading.Thread.Sleep(1000)
            Port.DtrEnable = False
            Log("DTR =0")


                Port.DtrEnable = True
                Log("DTR =1")
                System.Threading.Thread.Sleep(1000)

                Log("RTS =1")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1100)
                SendIdle()

                System.Threading.Thread.Sleep(1100)
                WriteS("+++" & vbCrLf)

                System.Threading.Thread.Sleep(1100)
                SendIdle()
                ok = WaitOK("", "OK", 2000)
                '' ''cnt = cnt + 1
                '' ''End While

                ReadAll(100)

                'If mData.PhoneLineType.ToUpper = "G" Then
                '    WriteS("ATH" & vbCrLf)
                'Else
                WriteS("ATH0" & vbCrLf)
                LOG("ATH0")

                'End If

                ReadAll(100)
                LOG("at+CFUN=1,1")
                WriteS("at+CFUN=1,1" & vbCrLf)
                System.Threading.Thread.Sleep(5000)
            End If

            Log("RTS =0")
            Port.RtsEnable = True
            System.Threading.Thread.Sleep(1000)
            SendIdle()

            System.Threading.Thread.Sleep(1000)
            Port.DtrEnable = False
            Log("DTR =0")

            System.Threading.Thread.Sleep(5000)

            If IsConnected Then

                
                Port.DtrEnable = True
                Log("DTR =1")
                System.Threading.Thread.Sleep(1000)

                Log("RTS =1")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1100)
                SendIdle()


                SendIdle()
                WriteS("+++" & vbCrLf)
                LOG("+++")

                System.Threading.Thread.Sleep(1000)
                SendIdle()
                ok = WaitOK("", "OK", 2000)
                'cnt = cnt + 1
                'End While

                ReadAll(100)

                'If mData.PhoneLineType.ToUpper = "G" Then
                '    WriteS("ATH" & vbCrLf)
                'Else
                LOG("ATH0")
                WriteS("ATH0" & vbCrLf)
                'End If

                ReadAll(100)

                Log("RTS =0")
                Port.RtsEnable = True
                System.Threading.Thread.Sleep(1000)
                SendIdle()

                Port.DtrEnable = False
                Log("DTR =0")
                System.Threading.Thread.Sleep(1000)
                SendIdle()
            End If


            Port.Close()
            Log("Close port " + Port.PortName)

            mIsConnected = Port.IsOpen
            If Not mIsConnected Then
                SendEvent(UnitransportAction.Disconnected, "")
            End If
            Return Not mIsConnected
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer

        If Not IsConnected Then
            Port.DiscardInBuffer()
            Port.DiscardOutBuffer()
            Return 0
        End If


        Dim bufStr As String = ""
        Dim i As Integer = 0
        Dim btr As Integer
        btr = Port.BytesToRead
        Dim cnt As Integer = 0

        While i < count And cnt < 30
            If btr > 0 Then
                If btr > count Then btr = count
                i = i + Port.Read(buffer, offset + i, btr)
                BytesReceived += btr
                cnt = 0
            Else
                cnt = cnt + 1
            End If

            SendEvent(UnitransportAction.ReceiveData, "")
            btr = Port.BytesToRead
            If btr = 0 Then
                System.Threading.Thread.Sleep(10 + CInt(1000 * 8 * 16 / Port.BaudRate))
                SendIdle()
            End If
            btr = Port.BytesToRead
        End While

        Dim j As Integer
        Dim buf2() As Byte
        buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buffer, offset, count)
        For j = 0 To i - 1
            If j < buf2.Length Then
                Try
            bufStr = bufStr + Chr(buf2(j))
                Catch ex As Exception

                End Try
            End If

        Next
        If bufStr.IndexOf("NO CARRIER") >= 0 Then
            SendEvent(UnitransportAction.LowLevelStop, "COM->NO CARRIER")
            mIsConnected = False
        End If
        If bufStr.IndexOf("BUSY") >= 0 Then
            mIsConnected = False
            SendEvent(UnitransportAction.LowLevelStop, "COM->BUSY")
        End If
        If i > 0 Then
            bufStr = ""

            For j = 0 To i - 1
                If j < buffer.Length Then
                    Try
                        bufStr = bufStr + Hex(buffer(offset + j)) + " "
                    Catch ex As Exception

                    End Try
                End If

            Next
            Log("<<R" + i.ToString + "<<" + bufStr)
        End If


        Return i

    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef vSetupData As TransportSetupData) As Boolean
        Try
            mData = CType(vSetupData, ModemTransportSetupData)

            If Port.IsOpen Then Port.Close()
            Port.BaudRate = mData.BaudRate
            Port.Handshake = mData.Handshake
            Port.Parity = mData.Parity

            Port.PortName = mData.PortName
            Port.StopBits = mData.StopBits
            SendEvent(UnitransportAction.SettingUp, "")
            Return True
        Catch ex As System.Exception
            Return False
        End Try

    End Function

    Public Overrides Function TransportType() As String
        Return "MODEM"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)
        Log("RTS =0")
        Port.RtsEnable = False
        System.Threading.Thread.Sleep(10)
        SendIdle()
        Log("RTS =1")
        Port.RtsEnable = True
        System.Threading.Thread.Sleep(10)
        SendIdle()

        Port.Write(buffer, offset, count)
        BytesSent += count
        SendEvent(UnitransportAction.SendData, "")
        Dim bufStr As String = ""
        Dim j As Integer

        For j = 0 To count - 1
            bufStr = bufStr + Hex(buffer(offset + j)) + " "
        Next
        Log(">>W" + count.ToString + ">>" + bufStr)

    End Sub

    Public Sub WriteS(ByVal s As String)
        Log("RTS =0")
        Port.RtsEnable = False
        System.Threading.Thread.Sleep(10)
        SendIdle()
        Log("RTS =1")
        Port.RtsEnable = True
        System.Threading.Thread.Sleep(10)
        SendIdle()
        Port.Write(s)
        BytesSent += s.Length
        SendEvent(UnitransportAction.SendData, "")

        Log(">>W" + s.Length.ToString + ">>" + s)
    End Sub

    Private Sub Port_PinChanged(ByVal sender As Object, ByVal e As System.IO.Ports.SerialPinChangedEventArgs) Handles Port.PinChanged

        If IsConnected And e.EventType = SerialPinChange.CDChanged Then
            mIsConnected = False
            Log("MODEM LOST CARRIER")
            SendEvent(UnitransportAction.Disconnected, "NO CARRIER")
        End If

        If e.EventType = SerialPinChange.CDChanged Then
            Log("CARRIER BIT CHANGED")

        End If

        If e.EventType = SerialPinChange.Ring Then
            Log("RING BIT CHANGED")
        End If

        If e.EventType = SerialPinChange.CtsChanged Then
            Log("CTS BIT CHANGED")
        End If

        If e.EventType = SerialPinChange.DsrChanged Then
            Log("DSR BIT CHANGED")
        End If



    End Sub

    Public Overrides Sub CleanPort()
        Port.DiscardInBuffer()
       'Log("iX clean input buffer")
        Port.DiscardOutBuffer()
        'Log("oX clean output buffer")
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

Public Class DummyTransport
    Inherits UniTransport


    Private mData As TransportSetupData


    Public Sub New()
        SendEvent(UnitransportAction.WakeUp, "")
        mData = New TransportSetupData
    End Sub

    Protected Overrides Sub Finalize()

        SendEvent(UnitransportAction.Destroy, "")
        mData = Nothing
    End Sub

    Private Sub TryRead()
    End Sub

    Public Overrides Function BytesToRead() As Integer
        TryRead()
        Return 0
    End Function

    Public Overrides Function Connect() As Boolean
        mIsConnected = True
        SendEvent(UnitransportAction.Connected, "")
        Return mIsConnected 
    End Function

    Public Overrides Function DisConnect() As Boolean
        mIsConnected = False
        Return Not mIsConnected
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
        Return 0
    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef SetupData As TransportSetupData) As Boolean
        mData = SetupData
    End Function

    Public Overrides Function TransportType() As String
        Return "DUMMY"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)

        LOG(">>w" + count.ToString)
    End Sub

    Public Overrides Sub CleanPort()
    End Sub
End Class



Public Class VortexTransport
    Inherits UniTransport


    Private mData As VortexTransportSetupData
    Private inBuffer(0 To 32000) As Byte
    Private ReadCount As Integer = 0
    Private soc As System.Net.Sockets.Socket

    Public Sub New()
        SendEvent(UnitransportAction.WakeUp, "")
        mData = New VortexTransportSetupData


        soc = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
        soc.Blocking = True
        soc.ReceiveTimeout = 200
        soc.SendTimeout = 200
    End Sub

    Protected Overrides Sub Finalize()
        Try
            If soc IsNot Nothing Then

                If soc.Connected Then
                    SendEvent(UnitransportAction.Disconnecting, "")
                    Try
                        soc.Close()
                        SendEvent(UnitransportAction.Disconnected, "")
                    Catch ex As Exception
                        LOG(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception

        End Try

        SendEvent(UnitransportAction.Destroy, "")
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
        If soc.Available = 0 Then
            System.Threading.Thread.Sleep(10)
            SendIdle()
        End If
        If soc.Available > 0 Then
            If tmpSize > soc.Available Then
                tmpSize = soc.Available
            End If
            tmpReadCount = soc.Receive(tmpBuffer, tmpSize, Net.Sockets.SocketFlags.None)
            BytesReceived += tmpReadCount
            SendEvent(UnitransportAction.ReceiveData, "")
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
            SendEvent(UnitransportAction.Connecting, "")
            soc.Connect(mData.Host, mData.Port)

        Catch ex As System.Exception
            LOG("Connecting: " + ex.Message)
        End Try
        System.Threading.Thread.Sleep(1000)
        SendIdle()
        If soc.Connected Then
            mIsConnected = True
            SendEvent(UnitransportAction.Connected, "")
            Return True
        Else
            SendEvent(UnitransportAction.LowLevelStop, "VORTEX connecting error")
            Return False
        End If
    End Function

    Public Overrides Function DisConnect() As Boolean
        If (soc.Connected) Then
            SendEvent(UnitransportAction.Disconnecting, "")
            soc.Close()
            SendEvent(UnitransportAction.Disconnected, "")
        End If

        mIsConnected = False
        Return Not mIsConnected
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

        LOG(">>r" + CntRead.ToString)
        For i = 0 To CntRead - 1
            buffer(i + offset) = inBuffer(i)
            LOG(buffer(i + offset).ToString("X02") + " ")
        Next

        'Dim bufstr As String = ""
        'Dim j As Integer
        'Try
        '    Dim buf2() As Byte
        '    buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buffer, offset, count)
        '    For j = 0 To CntRead - 1
        '        bufstr = bufstr + Chr(buf2(j))
        '    Next
        '    If bufstr <> "" Then
        '        LOG(">>r" + CntRead.ToString + ">>" + bufstr)
        '    End If
        'Catch ex As Exception
        '    'Stop
        'End Try
        BytesReceived += CntRead
        SendEvent(UnitransportAction.ReceiveData, "")
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
        mData = CType(SetupData, VortexTransportSetupData)
    End Function

    Public Overrides Function TransportType() As String
        Return "VORTEX"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)
        soc.Send(buffer, offset, count, Net.Sockets.SocketFlags.None)
        SendEvent(UnitransportAction.SendData, "")
        BytesSent += count

        'Dim bufstr As String = ""
        Dim j As Integer
        'Dim buf2() As Byte
        'buf2 = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buffer, offset, count)
        'For j = 0 To count - 1
        '    bufstr = bufstr + Chr(buf2(j))
        'Next
        LOG(">>w" + count.ToString) '+ ">>" + bufstr)
        For j = 0 To count - 1
            LOG(buffer(offset + j).ToString("X02") + " ")
        Next
    End Sub

    Public Overrides Sub CleanPort()
        ReadCount = 0

    End Sub
End Class




Public Class GRPSTransportSetupData
    Inherits TransportSetupData
   
    Public Sub New()
    End Sub
End Class


Public Class GRPSTransport
    Inherits UniTransport

    Private mData As GRPSTransportSetupData
    'Private inBuffer(0 To 32000) As Byte
    Private dataBuffer(0 To 32000) As Byte
    'Private ReadCount As Integer = 0
    Private DataCount As Integer = 0

    Private Soc As GRPSSocket

    Public Overrides ReadOnly Property IsConnected As Boolean
        Get
            If Soc Is Nothing Then Return False
            If Soc.Connected And Soc.LastRcvTime.AddMinutes(4) > Now Then
                Return True
            End If
            Return False
        End Get
    End Property

    Public Sub New(ByRef OpennedSocket As GRPSSocket)
        SendEvent(UnitransportAction.WakeUp, "")
        mData = New GRPSTransportSetupData
        Soc = OpennedSocket
    End Sub

    Protected Overrides Sub Finalize()
        Try
            If Soc IsNot Nothing Then

                If Soc.Connected Then
                    SendEvent(UnitransportAction.Disconnecting, "")
                    Try
                        Soc.Close()
                        SendEvent(UnitransportAction.Disconnected, "")
                    Catch ex As Exception
                        Log(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception

        End Try

        SendEvent(UnitransportAction.Destroy, "")
        Soc = Nothing

        mData = Nothing
    End Sub

    Private Sub TryRead()
        Dim tmpBuffer(0 To 32000) As Byte
        Dim tmpReadCount As Integer
        Dim tmpSize As Integer
        Dim i As Integer
        If DataCount = 32000 Then Exit Sub
        tmpSize = 32000 - DataCount
        tmpReadCount = Soc.Read(tmpBuffer, 0, tmpSize)
        If tmpReadCount > 0 Then
            BytesReceived += tmpReadCount
            SendEvent(UnitransportAction.ReceiveData, "")
                For i = 0 To tmpReadCount - 1
                dataBuffer(DataCount) = tmpBuffer(i)
                DataCount += 1
                Next
            End If
    End Sub

    'Private Sub TryRead()
    '    Dim tmpBuffer(0 To 32000) As Byte
    '    Dim tmpReadCount As Integer
    '    Dim tmpSize As Integer
    '    Dim i As Integer
    '    If ReadCount = 32000 Then Exit Sub

    '    tmpSize = 32000 - ReadCount

    '    If Soc.IPSocket.Available = 0 Then
    '        System.Threading.Thread.Sleep(2)
    '        SendIdle()
    '    End If
    '    If Soc.IPSocket.Available > 0 Then
    '        If tmpSize > Soc.IPSocket.Available Then
    '            tmpSize = Soc.IPSocket.Available
    '        End If
    '        tmpReadCount = Soc.IPSocket.Receive(tmpBuffer, tmpSize, Net.Sockets.SocketFlags.None)
    '        BytesReceived += tmpReadCount
    '        SendEvent(UnitransportAction.ReceiveData, "")
    '        If tmpReadCount > 0 Then
    '            For i = 0 To tmpReadCount - 1
    '                inBuffer(ReadCount) = tmpBuffer(i)
    '                ReadCount += 1
    '            Next
    '        End If

    '        Dim arr() As Byte

    '        arr = Soc.GetRawData(inBuffer, ReadCount)
    '        Dim j As Integer

    '        If Not arr Is Nothing Then
    '            ' DataCount = 0
    '            For j = 0 To arr.Length - 1
    '                dataBuffer(DataCount) = arr(j)
    '                DataCount += 1
    '            Next
    '            ReadCount = 0
    '        End If
    '    End If
    'End Sub
   
    Public Overrides Function BytesToRead() As Integer
        TryRead()
        Return DataCount
    End Function

    Public Overrides Function Connect() As Boolean
        If Soc.Connected Then
            mIsConnected = True
            SendEvent(UnitransportAction.Connected, "")
            Return True
        Else
            SendEvent(UnitransportAction.LowLevelStop, "GRPS connecting error")
            Return False
        End If
    End Function

    Public Overrides Function DisConnect() As Boolean
        If (Soc.Connected) Then
            SendEvent(UnitransportAction.Disconnecting, "")
            Soc.Close()
            SendEvent(UnitransportAction.Disconnected, "")
        End If

        mIsConnected = False
        Return Not mIsConnected
    End Function

    Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
        TryRead()
        Dim i As Integer
        Dim CntRead As Integer
        If DataCount < count Then
            CntRead = DataCount
        Else
            CntRead = count
        End If

        For i = 0 To CntRead - 1
            buffer(i + offset) = dataBuffer(i)
        Next


        Log(">>r" + CntRead.ToString + " >>")

        BytesReceived += CntRead
        SendEvent(UnitransportAction.ReceiveData, "")
        DataCount -= CntRead
        If DataCount > 0 Then
            For i = 0 To DataCount - 1
                dataBuffer(i) = dataBuffer(CntRead + i)
            Next
        End If
        Return CntRead
    End Function

    Public Overrides Function SetupData() As TransportSetupData
        Return mData
    End Function

    Public Overrides Function SetupTransport(ByRef SetupData As TransportSetupData) As Boolean
        mData = CType(SetupData, GRPSTransportSetupData)
    End Function

    Public Overrides Function TransportType() As String
        Return "GRPS"
    End Function

    Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer, Optional ByVal NoReplay As Boolean = False)
        'ReadCount = 0
        DataCount = 0
        Soc.Send(buffer, count, NoReplay)
        SendEvent(UnitransportAction.SendData, "")
        BytesSent += count
        Log(">>w" + count.ToString) '+ ">>" + bufstr)
    End Sub

    

    Public Overrides Sub CleanPort()
        'ReadCount = 0
        DataCount = 0
    End Sub
End Class