Imports System.Net.Sockets
Public Class ASSVSocket
    Inherits GRPSSocket


    Public Overrides ReadOnly Property SocketType() As String
        Get
            Return "ASSVSOCKET"
        End Get
    End Property

    Public Sub New(ByRef aSocket As Socket)
        MyBase.New(aSocket)
    End Sub

    Protected Overrides Sub Init()

        If IPSocket.Connected Then

            Dim buffer(20) As Byte
            buffer(0) = &H10
            buffer(1) = &H1
            buffer(2) = &H0
            buffer(3) = &H0
            buffer(4) = &H10
            buffer(5) = &H1F
            buffer(6) = &H3E
            buffer(7) = &H0
            buffer(8) = &H10
            buffer(9) = &H2
            buffer(10) = &H0
            buffer(11) = &H10
            buffer(12) = &H3
            buffer(13) = &H6
            buffer(14) = &HAF
            Dim cc As UShort
            cc = assvCRC(buffer, 2, 12)

            buffer(13) = ((cc >> 8) And 255)
            buffer(14) = (cc And 255)


            Dim i As Integer
            Dim sout As String = ""
            For i = 0 To 14
                sout += (Hex(buffer(i)) + " ")

            Next
            Dim cnt As Integer = 5
            While (mHasID = False And cnt > 0)
                cnt -= 1
                LOG("ASSV Hello:" + sout)

                IPSocket.Send(buffer, 15, SocketFlags.None)
                System.Threading.Thread.Sleep(2000)
                Dim result(1024) As Byte
                Dim rCnt As Integer
                rCnt = Read(result, 0, 1023)
                If rCnt > 8 Then
                    i = 0
                    mCallerID = ""
                    For i = 0 To 8
                        If result(i) >= &H30 Then
                            mCallerID = mCallerID + Chr(result(i))
                        End If
                    Next
                    If mCallerID.Length > 1 Then
                        mHasID = True
                        LOG("ASSVID=" + mCallerID)
                    Else
                        mLastError = "Идентификатор модуля АССВ не получен"
                        LOG(mLastError)
                    End If
                End If
            End While

        End If

    End Sub


    Private Function assvCRC(ByRef msg() As Byte, ByRef start As Integer, ByRef epos As Integer) As UShort

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


    Public Overrides Function Read(ByRef buf() As Byte, ByVal offset As Integer, ByVal dlength As Integer) As Integer
        ' LOG("ASSV Socket Read")
        Dim tmpSize As Integer
        Dim tmpoffset As Integer
        Dim tmpcount As Integer

        Dim l As Integer
        Dim i As Integer
        Dim c As Integer
        c = 0
        l = 0

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

        c = 0
        l = IPSocket.Available
        If l = 0 And c < 5 Then
            System.Threading.Thread.Sleep(50)
            l = IPSocket.Available
            c += 1
        End If

        tmpSize = 4096
        tmpoffset = 0
        IPSocket.ReceiveTimeout = 1500

        If IPSocket.Available > 0 Then
            rawCount = 0
            While IPSocket.Available > 0
                tmpSize = IPSocket.Available
                tmpcount = IPSocket.Receive(rawBuffer, tmpoffset, tmpSize, Net.Sockets.SocketFlags.None)
                tmpoffset = tmpoffset + tmpcount
                rawCount += tmpcount
                System.Threading.Thread.Sleep(100)
                dLastRcv = Now
            End While

        Else
            'mLastError = "Данные не получены"
            'LOG(mLastError)
            Return 0
        End If


        dataCount = 0


        ' проверка CRC  ????
        mLastError = ""
        Dim cc As UShort
        cc = assvCRC(rawBuffer, 2, rawCount - 3)
        If (rawBuffer(rawCount - 2) = ((cc >> 8) And 255)) And (rawBuffer(rawCount - 1) = (cc And 255)) Then

            Dim isData As Boolean = False
            i = 0
            While i < rawCount - 1
                If isData = False And rawBuffer(i) = &H10 And rawBuffer(i + 1) = 2 Then
                    i = i + 2
                    isData = True
                End If

                If isData = True And rawBuffer(i) = &H10 And rawBuffer(i + 1) = 3 Then
                    i = i + 2
                    isData = False
                End If

                If isData = True And rawBuffer(i) = &H10 And rawBuffer(i + 1) = &H10 Then
                    i = i + 1
                End If
                If isData Then
                    dataBuffer(dataCount) = rawBuffer(i)
                    dataCount = dataCount + 1
                End If
                i = i + 1

            End While


            Dim sOut As String = ""
            For i = 0 To dataCount - 1
                sOut = sOut + " " + Hex(dataBuffer(i))
            Next
            LOG(sOut)


            If dataCount = 8 And dataBuffer(0) = &H9 And dataBuffer(1) = &H8E And dataBuffer(2) = &HE8 And dataBuffer(3) = &HA8 And dataBuffer(4) = &HA1 And dataBuffer(5) = &HAA And dataBuffer(6) = &HA0 And dataBuffer(7) = &HC Then
                mLastError = "Ошибка взаимодействия АССВ"
                LOG(mLastError)
                Return 0
            End If

            ' 09 87 A0 AD EF E2 AE 0C 
            If dataCount = 8 And dataBuffer(0) = &H9 And dataBuffer(1) = &H87 And dataBuffer(2) = &HA0 And dataBuffer(3) = &HAD And dataBuffer(4) = &HEF And dataBuffer(5) = &HE2 And dataBuffer(6) = &HAE And dataBuffer(7) = &HC Then
                mLastError = "модуль АССВ занят"
                LOG(mLastError)
                Return 0
            End If


            If dataCount = 2 And dataBuffer(0) = &H9 And dataBuffer(0) = &HC Then
                mLastError = ""
                LOG("OK")
                Return 0
            End If

            For i = 0 To dataCount - 1
                If offset + i < dlength Then
                    buf(i + offset) = dataBuffer(i)
                End If
            Next
            LOG("Get " + dataCount.ToString() + " bytes")
            Return dataCount

        Else
            mLastError = "Контрольная сумма АССВ не совпала"
            LOG(mLastError)
            Return 0

        End If
    End Function


    Public Overrides Function GetRawData(ByVal rawBuffer() As Byte, ByVal size As Integer) As Byte()
        Dim ii As Integer
        Dim arr() As Byte
        For ii = 6 To size
            If CheckBuffer(rawBuffer, ii) Then
                arr = ConvertBuffer(rawBuffer, ii)
                If Not arr Is Nothing Then
                    Return arr
                End If
            End If
        Next
        Return Nothing
    End Function


    Private Function CheckBuffer(ByVal buf() As Byte, ByVal size As Integer) As Boolean
        Dim cc As UShort
        cc = assvCRC(buf, 2, size - 3)
        If (buf(size - 2) = ((cc >> 8) And 255)) And (buf(size - 1) = (cc And 255)) Then
            Return True
        End If
        Return False
    End Function



    Private Function ConvertBuffer(ByVal rawBuffer() As Byte, ByVal size As Integer) As Byte()
        Dim isData As Boolean = False
        Dim i As Integer
        Dim outBuf() As Byte
        i = 0
        dataCount = 0
        While i < size - 1
            If isData = False And rawBuffer(i) = &H10 And rawBuffer(i + 1) = 2 Then
                i = i + 2
                isData = True
            End If

            If isData = True And rawBuffer(i) = &H10 And rawBuffer(i + 1) = 3 Then
                i = i + 2
                isData = False
            End If

            If isData = True And rawBuffer(i) = &H10 And rawBuffer(i + 1) = &H10 Then
                i = i + 1
            End If
            If isData Then
                dataBuffer(dataCount) = rawBuffer(i)
                dataCount = dataCount + 1
            End If
            i = i + 1
        End While



        Dim sOut As String = ""
        For i = 0 To dataCount - 1
            sOut = sOut + " " + Hex(dataBuffer(i))
        Next
        LOG(sOut)


        If dataCount = 8 And dataBuffer(0) = &H9 And dataBuffer(1) = &H8E And dataBuffer(2) = &HE8 And dataBuffer(3) = &HA8 And dataBuffer(4) = &HA1 And dataBuffer(5) = &HAA And dataBuffer(6) = &HA0 And dataBuffer(7) = &HC Then
            mLastError = "Ошибка взаимодействия АССВ"
            LOG(mLastError)
            Return Nothing
        End If

        ' 09 87 A0 AD EF E2 AE 0C 
        If dataCount = 8 And dataBuffer(0) = &H9 And dataBuffer(1) = &H87 And dataBuffer(2) = &HA0 And dataBuffer(3) = &HAD And dataBuffer(4) = &HEF And dataBuffer(5) = &HE2 And dataBuffer(6) = &HAE And dataBuffer(7) = &HC Then
            mLastError = "модуль АССВ занят"
            LOG(mLastError)
            Return Nothing
        End If


        If dataCount = 2 And dataBuffer(0) = &H9 And dataBuffer(0) = &HC Then
            mLastError = ""
            LOG("OK")
            dLastRcv = Now
            Return Nothing
        End If

        ReDim outBuf(dataCount - 1)

        For i = 0 To dataCount - 1
            outBuf(i) = dataBuffer(i)
        Next
        dLastRcv = Now
        LOG("ASSV Get " + dataCount.ToString() + " bytes. " + sOut)
        Return outBuf

    End Function



    Public Overrides Function Send(ByRef buf() As Byte, ByVal l As Integer, Optional ByVal NoReplay As Boolean = False) As Integer
        LOG("ASSV Socket Send")
        Dim buffer(1024) As Byte
        buffer(0) = &H10
        buffer(1) = &H1
        buffer(2) = &H0
        buffer(3) = &H0
        buffer(4) = &H10
        buffer(5) = &H1F
        If NoReplay Then
            buffer(6) = &H2A
        Else
            buffer(6) = &H28
        End If

        buffer(7) = &H0
        buffer(8) = &H10
        buffer(9) = &H2
        Dim i As Integer, cnt As Integer, cc As UShort
        cnt = 9
        For i = 0 To l - 1
            cnt = cnt + 1
            buffer(cnt) = buf(i)

            If buf(i) = &H10 Then
                cnt = cnt + 1
                buffer(cnt) = buf(i)
            End If
        Next
        cnt = cnt + 1
        buffer(cnt) = &H10
        cnt = cnt + 1
        buffer(cnt) = &H3
        cc = assvCRC(buffer, 2, cnt)

        cnt = cnt + 1
        buffer(cnt) = ((cc >> 8) And 255)
        cnt = cnt + 1
        buffer(cnt) = (cc And 255)


        Dim sOut As String = ""
        For i = 0 To cnt
            sOut += (Hex(buffer(i)) + " ")
        Next
        LOG(vbCrLf & "sent :" + sOut)

        If Not IPSocket Is Nothing Then
            Return IPSocket.Send(buffer, cnt + 1, SocketFlags.None)
        Else
            Return 0
        End If
    End Function




  
End Class
