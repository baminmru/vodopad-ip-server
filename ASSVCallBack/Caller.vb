Imports System.Data
Imports STKTVMain
Imports System.IO.Ports
Imports Oracle.DataAccess


Public Class Caller
    Implements IDisposable

    Dim tvmain As STKTVMain.TVMain
    Dim port As System.IO.Ports.SerialPort

    Private Shared Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function

    Private Sub LOG(ByVal s As String)
        Try
            System.IO.File.AppendAllText(GetMyDir() + "\ASSV_LOG_" + Date.Now.ToString("yyyyMMdd") + "_.txt", Date.Now.ToString("yyyy.MM.dd HH:mm:ss") + " " + s + vbCrLf)
        Catch ex As Exception

        End Try
        Console.WriteLine(s)
    End Sub

    Public Sub New()
        tvmain = New STKTVMain.TVMain
    End Sub

    Public Function isOK() As Boolean
        If tvmain Is Nothing Then Return False
        If tvmain.dbconnect().State = ConnectionState.Open Then Return True
        Return False
    End Function
    Public Sub New(mytvmain As STKTVMain.TVMain)
        tvmain = mytvmain
    End Sub

    Private IsScanning As Boolean = False
    Public Sub ScanASSV()
        If IsScanning Then Return

        IsScanning = True
        Try
            Dim dt As DataTable
            Dim dt1 As DataTable
            dt = tvmain.QuerySelect("select cphone, bdevices.id_bd from bdevices join bmodems  on bdevices.id_bd=bmodems.id_bd join plancall on bdevices.id_bd=plancall.id_bd   where transport = 5 And npquery = 1 And (dlastcall is null or dlastcall < sysdate - 1 / 24 / 2 ) and (nplock is null or nplock < sysdate)")
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                dt1 = tvmain.QuerySelect("select cphone, bdevices.id_bd from bdevices join bmodems  on bdevices.id_bd=bmodems.id_bd join plancall on bdevices.id_bd=plancall.id_bd   where transport = 5 And npquery = 1 And (dlastcall is null or dlastcall < sysdate - 1 / 24 / 2 ) and (nplock is null or nplock < sysdate) and bdevices.id_bd=" & dt.Rows(i)("id_bd").ToString())
                If dt1.Rows.Count > 0 Then
                    CallASSV(dt.Rows(i)("cphone"), dt.Rows(i)("id_bd").ToString())
                End If

            Next
        Catch ex As Exception

        End Try

        System.Threading.Thread.Sleep(30000)

        IsScanning = False



    End Sub




    Private Function WaitOK(Optional ByVal WaitStr As String = "OK", Optional ByVal Timeout As Integer = 30000, Optional ByRef ReceivedString As String = Nothing) As Boolean

        Dim i As Int16
        Dim j As Int16
        Dim bufStr As String = ""
        Dim btr As Long

        Dim buf(32000) As Byte
        Dim buf1(32000) As Byte

        Dim sz As Long
        sz = 0

        For i = 1 To 100
            System.Threading.Thread.Sleep(Timeout / 100)
            If i Mod 10 = 0 Then LOG("wait for answer " + i.ToString + "%")
            btr = port.BytesToRead

            If btr > 0 Then
                If sz + btr < 32000 Then
                    port.Read(buf, sz, btr)
                    sz += btr
                Else
                    port.Read(buf, sz, 32000 - 1 - sz)
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

                bufStr = bufStr.Replace(vbCrLf, " ")

                LOG("<<wait for:" + WaitStr + "<< received:" + bufStr)

                If bufStr.ToLower().IndexOf(WaitStr.ToLower) >= 0 Then
                    If Not ReceivedString Is Nothing Then
                        ReceivedString = bufStr
                    End If
                    Return True
                End If

                If bufStr.Length = 0 Then
                    Return False
                End If

                If bufStr.IndexOf("ERROR") >= 0 Then
                    Return False
                End If
                If bufStr.IndexOf("NO ANSWER") >= 0 Then
                    Return False
                End If


                If bufStr.IndexOf("NO DIALTONE") >= 0 Then
                    Return False
                End If
                If bufStr.IndexOf("NO CARRIER") >= 0 Then
                    Return False
                End If
                If bufStr.IndexOf("BUSY") >= 0 Then
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
        LOG("<<" + WaitStr + "<<" + bufStr)
        bufStr = bufStr.Replace(vbCrLf, "")

        If bufStr.IndexOf("ERROR") >= 0 Then
            Return False
        End If
        If bufStr.IndexOf("NO ANSWER") >= 0 Then
            Return False
        End If


        If bufStr.IndexOf("NO DIALTONE") >= 0 Then
            Return False
        End If
        If bufStr.IndexOf("NO CARRIER") >= 0 Then
            Return False
        End If
        If bufStr.IndexOf("BUSY") >= 0 Then
            Return False
        End If

        If bufStr.ToLower().IndexOf(WaitStr.ToLower) < 0 Then
            Return False
        End If
        Return True

    End Function


    Private Sub CallASSV(ByVal pnum As String, ByVal id As String)
        If Trim(pnum & "") = "" Then
            Exit Sub
        End If
        Dim callOK As Boolean = False
        Dim portname As String
        Dim initstr As String
        tvmain.ClearDuration()
        portname = tvmain.GetNextModem("G")
        If portname <> "" Then
            LOG("Use " & portname & vbCrLf)
            port = New SerialPort
            port.PortName = portname
            initstr = tvmain.GetModemINIT()
            port.BaudRate = 9600
            port.DataBits = 8
            port.StopBits = 1
            port.Parity = Parity.None
            port.Handshake = Handshake.None

            port.Open()
            If port.IsOpen Then
                tvmain.SaveLog(Integer.Parse(id), 0, portname.Replace("COM", ""), 0, "Инициируем сеанс АССВ " & pnum)
                tvmain.ClearDuration()
                port.RtsEnable = False
                port.DtrEnable = False
                System.Threading.Thread.Sleep(100)
                port.RtsEnable = True
                port.DtrEnable = True
                If initstr <> "" Then

                   

                    LOG("atz" & vbCrLf)
                    port.Write("atez" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    WaitOK()

                    LOG("at&f" & vbCrLf)
                    port.Write("at&f" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    WaitOK()

                    LOG(initstr & vbCrLf)
                    port.Write(initstr & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    WaitOK()

                    LOG("ate0" & vbCrLf)
                    port.Write("ate0" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                Else
                   
                    LOG("atz" & vbCrLf)
                    port.Write("atez" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    WaitOK()

                    LOG("at&f" & vbCrLf)
                    port.Write("at&f" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    WaitOK()


                    LOG("ate0" & vbCrLf)
                    port.Write("ate0" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                End If
                WaitOK()

                LOG("at+CFUN=1" & vbCrLf)
                port.Write("at+CFUN=1" & vbCrLf)
                System.Threading.Thread.Sleep(10000)
                WaitOK()

                LOG("at+CREG=1" & vbCrLf)
                port.Write("at+CREG=1" & vbCrLf)
                System.Threading.Thread.Sleep(5000)
                WaitOK()

                LOG("AT+COPS=0" & vbCrLf)
                port.Write("AT+COPS=0" & vbCrLf)
                System.Threading.Thread.Sleep(5000)
                WaitOK()




                Dim callnum As String
                callnum = "ATD" & pnum.Replace("-", "") & ";" & vbCrLf
                port.Write(callnum)
                LOG("call " + pnum + " for deviceid=" + id)

                If WaitOK("BUSY", 60000) Then
                    port.DiscardInBuffer()
                    LOG("CALL OK!!!")

                    callOK = True
                End If

                LOG("ATH0" & vbCrLf)
                port.Write("ATH0" & vbCrLf)
                WaitOK()

                If callOK Then
                    tvmain.QueryExec("update plancall set dlastcall = sysdate where id_bd=" + id)
                    tvmain.SaveLog(Integer.Parse(id), 0, portname.Replace("COM", ""), 0, "Успешный вызов АССВ " + pnum)
                Else
                    LOG("ATH0" & vbCrLf)
                    port.Write("ATH0" & vbCrLf)
                    WaitOK()
                    tvmain.SaveLog(Integer.Parse(id), 0, portname.Replace("COM", ""), 0, "Неудачный вызов АССВ " + pnum)
                    LOG("restart at+CFUN=1,1" & vbCrLf)
                    port.Write("at+CFUN=1,1" & vbCrLf)
                    System.Threading.Thread.Sleep(10000)
                    WaitOK()
                End If



                port.RtsEnable = False
                port.DtrEnable = False
                port.Close()
            End If

            tvmain.FreeModem()
        End If


    End Sub


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not port Is Nothing Then
                    Try
                        port.Dispose()
                    Catch ex As Exception

                    End Try
                    port = Nothing
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        If Not port Is Nothing Then
            Try
                port.Dispose()
            Catch ex As Exception

            End Try
            port = Nothing
        End If
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
