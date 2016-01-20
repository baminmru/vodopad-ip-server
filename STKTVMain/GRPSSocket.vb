Imports System.Net.Sockets
Public MustInherit Class GRPSSocket


    Protected rawBuffer(4096) As Byte
    Protected rawCount As Integer = 0

    Protected dataBuffer(4096) As Byte
    Protected dataCount As Integer = 0

    Protected dLastRcv As Date = Now

    Protected mCallerID As String = ""
    Public Overridable ReadOnly Property callerID() As String
        Get
            Return mCallerID
        End Get
    End Property

    Public Overridable ReadOnly Property LastRcvTime As Date
        Get
            Return dLastRcv
        End Get
    End Property



    Public Overridable ReadOnly Property SocketType() As String
        Get
            Return "GRPSSOCKET"
        End Get
    End Property


    Protected mHasID As Boolean
    Public Overridable ReadOnly Property HasID() As Boolean
        Get
            Return mHasID
        End Get
    End Property


    Protected mLastError As String = ""
    Public Overridable ReadOnly Property LastError() As String
        Get
            Return mLastError
        End Get
    End Property

    Protected Shared Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function

    Protected Sub LOG(ByVal s As String)

        Dim ep As String = ""

        If Not IPSocket Is Nothing Then
            If IPSocket.Connected Then
                ep = IPSocket.RemoteEndPoint.ToString()
            End If

        End If

        Try
            System.IO.File.AppendAllText(GetMyDir() + "\" + SocketType() + "_LOG_" + Date.Now.ToString("yyyyMMdd") + "_" + callerID + ".txt", Date.Now.ToString("yyyy.MM.dd HH:mm:ss") + " (" + ep + ") " + s + vbCrLf)
        Catch ex As Exception

        End Try
        Console.WriteLine(s)
    End Sub

    Public IPSocket As Socket


    Protected Overridable Sub Init()
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
                        If i < 8 Then
                            If result(i) >= &H30 Then
                                mCallerID = mCallerID + Chr(result(i))
                            End If
                        End If

                    Next
                    If mCallerID.Length > 1 Then
                        mHasID = True


                        If callerID.Length > 8 Then
                            mCallerID = mCallerID.Substring(0, 8)
                        End If

                        LOG("Caller ID=" + callerID)
                    Else
                        mLastError = "Идентификатор модуля CallerID не получен"
                        LOG(mLastError)
                    End If
                End If
            End While
        End If
    End Sub


    Public Sub New(ByRef aSocket As Socket)
        IPSocket = aSocket
        Init()
    End Sub





    Public Overridable Function Read(ByRef buf() As Byte, ByVal offset As Integer, ByVal dlength As Integer) As Integer
        'LOG("GRPS Socket Read")
        Dim tmpSize As Integer
        Dim tmpoffset As Integer
        Dim tmpcount As Integer

        Dim l As Integer
        Dim c As Integer
        Dim i As Integer

        l = 0
        c = 0

        If IPSocket Is Nothing Then
            mLastError = "Сокет не установлен"
            LOG(mLastError)
            Return 0
        End If

        If Not IPSocket.Connected Then
            mLastError = "Сокет закрыт"
            LOG(mLastError)
            Return 0
        End If


        l = IPSocket.Available
        If l = 0 Then
            System.Threading.Thread.Sleep(10)
        End If

        ''Console.WriteLine(".")
        'While l = 0 And c < 10
        '    System.Threading.Thread.Sleep(10)
        '    l = IPSocket.Available
        '    ' Console.Write(".")
        '    c = c + 1
        'End While


        rawCount = 0

        tmpSize = 4096
        tmpoffset = 0
        IPSocket.ReceiveTimeout = 1500

        If IPSocket.Available > 0 Then
            While IPSocket.Available > 0
                tmpSize = IPSocket.Available
                tmpcount = IPSocket.Receive(rawBuffer, tmpoffset, tmpSize, Net.Sockets.SocketFlags.None)
                tmpoffset = tmpoffset + tmpcount
                rawCount += tmpcount
                System.Threading.Thread.Sleep(100)
                dLastRcv = Now
            End While

        Else
            mLastError = "Данные не получены"
            'LOG(mLastError)
            Return 0
        End If



        mLastError = ""

        Dim isData As Boolean = False
        Dim sOut As String = ""
        For i = 0 To rawCount - 1
            dataBuffer(dataCount) = rawBuffer(i)
            dataCount = dataCount + 1
            sOut = sOut + " " + Hex(rawBuffer(i))
        Next

        LOG(sOut)

        Dim rcnt As Integer
        rcnt = 0
        For i = 0 To dataCount - 1
            If offset + i < dlength Then
                rcnt += 1
                buf(i + offset) = dataBuffer(i)
            Else
                Exit For
            End If
        Next

        LOG("Get " + rcnt.ToString() + " bytes")

        dataCount -= rcnt
        If dataCount > 0 Then
            For i = 0 To dataCount - 1
                dataBuffer(i) = dataBuffer(rcnt + i)
            Next
        End If


        Return rcnt

    End Function


    Public Overridable Function Send(ByRef buf() As Byte, ByVal l As Integer, Optional ByVal NoReplay As Boolean = False) As Integer
        LOG("GRPS Socket Send")


        Dim sOut As String = ""
        Dim i As Integer
        For i = 0 To l - 1
            sOut = sOut + " " + Hex(buf(i))
        Next

        LOG(sOut)


        If Not IPSocket Is Nothing Then
            Return IPSocket.Send(buf, l, SocketFlags.None)
        Else
            Return 0
        End If
    End Function


    Public Overridable Function GetRawData(ByVal rawBuffer() As Byte, ByVal size As Integer) As Byte()
        Dim i As Integer
        Dim outBuf() As Byte
        i = 0
    

        Dim sOut As String = ""
        For i = 0 To size - 1
            sOut = sOut + " " + Hex(rawBuffer(i))
        Next

        LOG(sOut)


        ReDim outBuf(size - 1)

        For i = 0 To size - 1
            outBuf(i) = rawBuffer(i)
        Next

        LOG("Get " + size.ToString() + " bytes. " + sOut)
        Return outBuf
    End Function

    



  

    Public Overridable Function Connected() As Boolean
        If Not IPSocket Is Nothing Then
            Return IPSocket.Connected And HasID
        End If
        Return False
    End Function

    Public Overridable Sub Close()
        If Not IPSocket Is Nothing Then
            IPSocket.Close()
            IPSocket = Nothing
        End If
    End Sub
End Class
