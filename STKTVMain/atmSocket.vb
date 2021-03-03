Imports System.Net.Sockets
Public Class atmSocket
    Inherits GRPSSocket


    Public Overrides ReadOnly Property SocketType() As String
        Get
            Return "ATMSOCKET"
        End Get
    End Property


    Public Sub New(ByRef aSocket As Socket)
        MyBase.New(aSocket)
    End Sub

    Protected Overrides Sub LOG(ByVal s As String)

        CheckLog()
        If m_LogEnabled Then

            If ID_BD <> 0 Then
                NLog.GlobalDiagnosticsContext.Set("counter", "_" & ID_BD & "_ATM")
                NLog.GlobalDiagnosticsContext.Set("id", ID_BD)
            Else
                NLog.GlobalDiagnosticsContext.Set("counter", "_" & callerID & "_ATM")
                NLog.GlobalDiagnosticsContext.Set("id", callerID)
            End If


            Logger.Info(s)
        End If
    End Sub

    Protected Overrides Sub Init()
        If IPSocket.Connected Then

            Dim i As Integer

            Dim cnt As Integer = 5
            While (mHasID = False And cnt > 0)
                cnt -= 1
                Dim result(1024) As Byte
                Dim rCnt As Integer
                System.Threading.Thread.Sleep(500)
                rCnt = Read(result, 0, 1023)
                If rCnt >= 0 Then
                    i = 0
                    mCallerID = ""
                    For i = 0 To rCnt - 1
                        If i < 15 Then
                            If result(i) >= &H30 Then
                                mCallerID = mCallerID + Chr(result(i))
                            End If
                        End If

                    Next
                    If mCallerID.Length > 1 Then
                        mHasID = True


                        If callerID.Length > 15 Then
                            mCallerID = mCallerID.Substring(0, 15)
                        End If

                        LOG("Caller ID=" + callerID)
                        Return

                    End If
                End If
            End While
            mLastError = "Идентификатор модуля CallerID не получен"
            LOG(mLastError)
        End If
    End Sub

    Public Overrides Sub Close()
        If Not IPSocket Is Nothing Then
            Send(System.Text.Encoding.Default.GetBytes("END"), 3)
            IPSocket.Close()
            IPSocket = Nothing
        End If
    End Sub
End Class
