Imports STKTVMain
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.ComponentModel
Imports System.Xml




Public Class Driver
    Inherits STKTVMain.TVDriver


    Private sXML As String

    Public Overrides Sub DoSpecificSetup(ByRef TvMain As Object)
        Dim f As frmSetup
        f = New frmSetup
        f.tvMain = TvMain
        f.IDBD = DeviceID
        f.ShowDialog()
    End Sub

    Private MyTV As STKTVMain.TVMain

    Public Overrides Sub SetupDriverFromDB(ByRef TvMain As Object)
        Dim sData As String = ""
        TvMain.GetStringFromField(sData, "DRIVERSETUP", "SETUPXML", "ID_BD", DeviceID.ToString())
        sXML = sData
        MyTV = TvMain
    End Sub

    Private Function GetDeviceDate() As Date
        Dim d As Date



        d = DateTime.Now
        Return d

    End Function

    Private mIsConnected As Boolean





    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

    Dim tv As Short





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
        Return "Areal"
    End Function

    Private m_serverip As String





    Private Function TryConnect() As Boolean
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        Dim dataStream As Stream
        Dim reader As StreamReader
        Dim responseFromServer As String
        Dim i As Integer



        'Dim tc As Sockets.TcpClient = New Sockets.TcpClient()
        ''Подключаемся, используя имя хоста и порт
        'tc.Connect(MyBase.ServerIp, MyBase.IPPort)
        ''Получаем экземпляр NetworkStream для отправки данных
        'Dim ns As NetworkStream = tc.GetStream()



        Try
            Dim q As String

            q = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/"
            Dim webClient As New System.Net.WebClient
            Dim result As String = webClient.DownloadString(q)


            'request = HttpWebRequest.Create(q)
            'request.Timeout = 250

            'request.Method = "GET"
            ''request.PreAuthenticate = False
            ''request.Accept="*/*"
            'request.UserAgent = "Mozilla/5.0 (Windows NT 8.1; WOW64; rv:40.0)"
            'request.AllowAutoRedirect = True
            '' If required by the server, set the credentials.
            'request.UseDefaultCredentials = True
            ''request.Credentials = CredentialCache.DefaultCredentials
            '' Get the response.
            'response = request.GetResponse()

            'dataStream = response.GetResponseStream()
            '' Open the stream using a StreamReader for easy access.
            'reader = New StreamReader(dataStream)
            '' Read the content.
            'responseFromServer = reader.ReadToEnd()

            'reader.Close()
            'reader.Dispose()
            'dataStream.Dispose()
            'response.Close()
            '' response.Dispose()
            Console.WriteLine("connected!!!")

            Return True


        Catch ex As Exception
            Return False
        End Try


    End Function


    Private Function Str2Dbl(ByVal s As String) As Double
        Dim v As Double
        Dim s1 As String
        If Double.TryParse(s, v) Then Return v
        s1 = s.Replace(",", ".")
        If Double.TryParse(s1, v) Then Return v
        s1 = s.Replace(".", ",")
        If Double.TryParse(s1, v) Then Return v
    End Function

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim ok As Boolean = False
        Dim dt As DataTable
        Dim rdate As Date

        If ArchType = archType_day Then
            rdate = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
        Else

            rdate = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
        End If


        clearArchive(Arch)

        Dim d As Date



        dt = New DataTable




        If dt.Rows.Count > 0 Then
            ok = True


            Arch.archType = ArchType
            Arch.DateArch = rdate
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                Debug.Print(dt.Rows(i)("Name").ToString().ToUpper() + "-->" + dt.Rows(i)("Value").ToString())
                Select Case dt.Rows(i)("Name").ToString().ToUpper
                    Case "M1"
                        Arch.M1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M2"
                        Arch.M2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M3"
                        Arch.M3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M4"
                        Arch.M4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M5"
                        Arch.M5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M6"
                        Arch.M6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "V1"
                        Arch.V1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V2"
                        Arch.V2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V3"
                        Arch.V3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V4"
                        Arch.v4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V5"
                        Arch.v5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V6"
                        Arch.v6 = Str2Dbl(dt.Rows(i)("Value").ToString())


                    Case "V1N"
                        Arch.V1H = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V2N"
                        Arch.V2H = Str2Dbl(dt.Rows(i)("Value").ToString())
                    'Case "V3N"
                    '    Arch.V3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V4N"
                        Arch.v4H = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V5N"
                        Arch.v5H = Str2Dbl(dt.Rows(i)("Value").ToString())
                    'Case "V6N"
                    '    Arch.v6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "P1"
                        Arch.P1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P2"
                        Arch.P2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P3"
                        Arch.P3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P4"
                        Arch.P4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P5"
                        Arch.P5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P6"
                        Arch.P6 = Str2Dbl(dt.Rows(i)("Value").ToString())


                    Case "T1"
                        Arch.T1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T2"
                        Arch.T2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T3"
                        Arch.T3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T4"
                        Arch.T4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T5"
                        Arch.T5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T6"
                        Arch.T6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "Q1"
                        Arch.Q1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q2"
                        Arch.Q2 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "Q3"
                        Arch.Q3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q4"
                        Arch.Q4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q5"
                        Arch.Q5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q6"
                        Arch.Q6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "Q1N"
                        Arch.Q1H = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q2N"
                        Arch.Q2H = Str2Dbl(dt.Rows(i)("Value").ToString())


                    Case "ERR1"
                        Arch.HCtv1 = Long.Parse(dt.Rows(i)("Value"))
                        Arch.HC = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERR2"
                        Arch.HCtv2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME1"
                        Arch.WORKTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME2"
                        Arch.WORKTIME2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERRTIME1"
                        Arch.ERRTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERRTIME2"
                        Arch.ERRTIME2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME1N"
                        Arch.WORKTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME2N"
                        Arch.WORKTIME2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME1N_"
                        Arch.OKTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME2N_"
                        Arch.OKTIME2 = Long.Parse(dt.Rows(i)("Value"))
                End Select

            Next
        End If



arch_final:

        If ok = False Then
            isArchToDBWrite = False
            Return "Ошибка чтения архива"
        Else
            isArchToDBWrite = True
            Return "Архива прочитан"
        End If

    End Function









    Private Function GetLng(ByVal SI() As Byte, ByVal Pos As Integer) As Long

        Dim h As ULong
        h = 0
        Dim b1 As Integer, b2 As Integer, b3 As Integer, b0 As Integer
        Try
            b0 = SI(Pos + 3)
            b1 = SI(Pos + 2)
            b2 = SI(Pos + 1)
            b3 = SI(Pos + 0)
            h = (b0 << 24) + (b1 << 16) + (b2 << 8) + b3
        Catch ex As Exception

            h = 0
        End Try
        Return h
    End Function
    Private Function GetInt(ByVal SI() As Byte, ByVal Pos As Integer) As Integer
        Dim h As Integer
        Dim b1 As Integer, b0 As Integer
        b0 = SI(Pos)
        b1 = SI(Pos + 1)
        h = (b0 << 8) + b1
        Return h
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




    Private Function ReadItem(dt As DataTable, name As String, xval As String) As Boolean
        Dim addr() As String
        Dim vnum As String
        Dim v1 As String
        Dim va As UShort
        Dim ba As Integer
        Dim SlaveId As Byte = 0
        If NPPassword <> "" Then
            Try
                SlaveId = Byte.Parse(NPPassword)
            Catch ex As Exception

            End Try
        End If
        'Connect()
        'If IsConnected() = False Then
        '    Return False
        'End If

        v1 = xval.Substring(0, 1)
        vnum = xval.Substring(1).ToUpper
        If vnum.IndexOf(".") >= 0 Then
            addr = vnum.Split(".")
            va = UShort.Parse(addr(0))
            ba = addr(1)
        Else
            va = UShort.Parse(vnum)
            ba = -1
        End If


        If v1 = "H" Or v1 = "I" Or v1 = "C" Or v1 = "D" Then


            Dim dr As DataRow
            dr = dt.NewRow
            dr("name") = name
            Dim vv As UShort


            'Dim request As HttpWebRequest
            'Dim response As HttpWebResponse
            'Dim dataStream As Stream
            'Dim reader As StreamReader
            Dim responseFromServer As String

            Try
                Select Case v1
                    Case "H"

                        Try
                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?hreg=" + va.ToString())
                            'request.Timeout = 250
                            '' If required by the server, set the credentials.
                            'request.Credentials = CredentialCache.DefaultCredentials
                            '' Get the response.
                            'response = request.GetResponse()

                            'dataStream = response.GetResponseStream()
                            '' Open the stream using a StreamReader for easy access.
                            'reader = New StreamReader(dataStream)
                            '' Read the content.
                            'responseFromServer = reader.ReadToEnd()

                            'reader.Close()
                            'reader.Dispose()
                            'dataStream.Dispose()
                            'response.Close()
                            '' response.Dispose()


                            Dim q As String = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?hreg=" + va.ToString()
                            Dim webClient As New System.Net.WebClient
                            responseFromServer = webClient.DownloadString(q)
                            webClient.Dispose()
                            Console.WriteLine(q + "->" + responseFromServer)



                            Try
                                vv = UShort.Parse(responseFromServer)
                            Catch ex As Exception
                                vv = 0
                            End Try

                        Catch ex As Exception

                        End Try




                        If ba = -1 Then
                            dr("value") = vv
                        Else
                            If (vv And (1 << ba)) = (1 << ba) Then
                                dr("value") = 1
                            Else
                                dr("value") = 0
                            End If

                        End If


                    Case "I"
                        Try


                            Dim q As String = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?ireg=" + va.ToString()
                            Dim webClient As New System.Net.WebClient
                            responseFromServer = webClient.DownloadString(q)
                            webClient.Dispose()
                            Console.WriteLine(q + "->" + responseFromServer)

                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?ireg=" + va.ToString())
                            'request.Timeout = 250
                            '' If required by the server, set the credentials.
                            'request.Credentials = CredentialCache.DefaultCredentials
                            '' Get the response.
                            'response = request.GetResponse()

                            'dataStream = response.GetResponseStream()
                            '' Open the stream using a StreamReader for easy access.
                            'reader = New StreamReader(dataStream)
                            '' Read the content.
                            'responseFromServer = reader.ReadToEnd()

                            'reader.Close()
                            'reader.Dispose()
                            'dataStream.Dispose()
                            'response.Close()
                            '' response.Dispose()


                            Try
                                vv = UShort.Parse(responseFromServer)
                            Catch ex As Exception
                                vv = 0
                            End Try

                        Catch ex As Exception

                        End Try




                        If ba = -1 Then
                            dr("value") = vv
                        Else
                            If (vv And (1 << ba)) = (1 << ba) Then
                                dr("value") = 1
                            Else
                                dr("value") = 0
                            End If

                        End If

                    Case "C"


                        Try

                            Dim q As String = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?coil=" + va.ToString()
                            Dim webClient As New System.Net.WebClient
                            responseFromServer = webClient.DownloadString(q)
                            webClient.Dispose()
                            Console.WriteLine(q + "->" + responseFromServer)
                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?coil=" + va.ToString())
                            'request.Timeout = 250
                            '' If required by the server, set the credentials.
                            'request.Credentials = CredentialCache.DefaultCredentials
                            '' Get the response.
                            'response = request.GetResponse()

                            'dataStream = response.GetResponseStream()
                            '' Open the stream using a StreamReader for easy access.
                            'reader = New StreamReader(dataStream)
                            '' Read the content.
                            'responseFromServer = reader.ReadToEnd()

                            'reader.Close()
                            'reader.Dispose()
                            'dataStream.Dispose()
                            'response.Close()
                            '' response.Dispose()


                            Try
                                If responseFromServer.ToLower() = "true" Then
                                    vv = 1
                                Else
                                    vv = 0
                                End If
                            Catch ex As Exception
                                vv = 0
                            End Try

                        Catch ex As Exception
                            vv = 0
                        End Try





                        If vv Then
                            dr("value") = 1
                        Else
                            dr("value") = 0
                        End If



                    Case "D"
                        Try

                            Dim q As String = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?input=" + va.ToString()
                            Dim webClient As New System.Net.WebClient
                            responseFromServer = webClient.DownloadString(q)
                            webClient.Dispose()
                            Console.WriteLine(q + "->" + responseFromServer)

                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?input=" + va.ToString())
                            'request.Timeout = 250
                            '' If required by the server, set the credentials.
                            'request.Credentials = CredentialCache.DefaultCredentials
                            '' Get the response.
                            'response = request.GetResponse()

                            'dataStream = response.GetResponseStream()
                            '' Open the stream using a StreamReader for easy access.
                            'reader = New StreamReader(dataStream)
                            '' Read the content.
                            'responseFromServer = reader.ReadToEnd()

                            'reader.Close()
                            'reader.Dispose()
                            'dataStream.Dispose()
                            'response.Close()
                            ''response.Dispose()


                            Try
                                If responseFromServer.ToLower() = "true" Then
                                    vv = 1
                                Else
                                    vv = 0
                                End If
                                'vv = Boolean.Parse(responseFromServer)
                            Catch ex As Exception
                                vv = 0
                            End Try

                        Catch ex As Exception
                            vv = 0
                        End Try


                        If vv Then
                            dr("value") = 1
                        Else
                            dr("value") = 0
                        End If

                End Select
                dt.Rows.Add(dr)
                Return True

            Catch ex As Exception
                Return False
            End Try

        Else
            Return False
        End If





    End Function




    Public Overrides Function ProcessComands() As Integer
        Dim dtcmd As DataTable
        Dim i As Integer
        Dim cnt As Integer
        Dim vRegType As String
        Dim vAddr As UShort
        Dim vBit As Integer
        Dim vValue As UShort
        Dim vMask As UShort
        Dim xval As String
        Dim QRegs As Integer
        Dim SlaveId As Byte = 0
        Dim OK As Boolean

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        Dim dataStream As Stream
        Dim reader As StreamReader
        Dim responseFromServer As String


        If NPPassword <> "" Then
            Try
                SlaveId = Byte.Parse(NPPassword)
            Catch ex As Exception
                Return 0
            End Try
        End If
        'Connect()
        'If IsConnected() = False Then
        '    Return 0
        'End If


        dtcmd = MyTV.QuerySelect("select ROWID,RCW.* from RCW where id_BD=" + DeviceID.ToString() + " and DONE=0 order by CREATEDATE")

        If dtcmd.Rows.Count > 0 Then
            cnt = 0
            For i = 0 To dtcmd.Rows.Count - 1
                OK = True

                QRegs = Integer.Parse(dtcmd.Rows(i)("QREGS").ToString())

                '''''''''''''''''''  reg1 
                If QRegs > 0 Then
                    xval = dtcmd.Rows(i)("REG1").ToString()

                    vRegType = xval.Substring(0, 1).ToUpper()
                    If vRegType = "H" Or vRegType = "I" Or vRegType = "C" Or vRegType = "D" Then
                        vAddr = xval.Substring(1).ToUpper
                    Else
                        vRegType = "C"
                        vAddr = xval.ToString()
                    End If

                    ' формируем новое значение регистра
                    vBit = Integer.Parse(dtcmd.Rows(i)("BIT1").ToString())
                    vValue = UShort.Parse(dtcmd.Rows(i)("VALUE1").ToString())


                    If vRegType = "H" Or vRegType = "I" Or vRegType = "C" Or vRegType = "D" Then

                        Try
                            Select Case vRegType
                                Case "H", "I"
                                    If vBit = -1 Then

                                        Try

                                            Dim q As String = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vValue.ToString()
                                            Dim webClient As New System.Net.WebClient
                                            responseFromServer = webClient.DownloadString(q)
                                            webClient.Dispose()
                                            Console.WriteLine(q + "->" + responseFromServer)

                                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vValue.ToString())
                                            'request.Timeout = 1000
                                            '' If required by the server, set the credentials.
                                            'request.Credentials = CredentialCache.DefaultCredentials
                                            '' Get the response.
                                            'response = request.GetResponse()

                                            'dataStream = response.GetResponseStream()
                                            '' Open the stream using a StreamReader for easy access.
                                            'reader = New StreamReader(dataStream)
                                            '' Read the content.
                                            'responseFromServer = reader.ReadToEnd()

                                            'reader.Close()
                                            'response.Close()

                                        Catch ex As Exception

                                        End Try
                                    Else
                                        If vBit >= 0 And vBit <= 15 Then
                                            If vValue > 0 Then
                                                vMask = 1 << vBit

                                            Else
                                                vMask = 1 << vBit
                                                vMask = Not vMask

                                            End If

                                            Dim q As String = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vMask.ToString()
                                            Dim webClient As New System.Net.WebClient
                                            responseFromServer = webClient.DownloadString(q)
                                            webClient.Dispose()
                                            Console.WriteLine(q + "->" + responseFromServer)

                                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vMask.ToString())
                                            'request.Timeout = 1000
                                            '' If required by the server, set the credentials.
                                            'request.Credentials = CredentialCache.DefaultCredentials
                                            '' Get the response.
                                            'response = request.GetResponse()

                                            'dataStream = response.GetResponseStream()
                                            '' Open the stream using a StreamReader for easy access.
                                            'reader = New StreamReader(dataStream)
                                            '' Read the content.
                                            'responseFromServer = reader.ReadToEnd()

                                            'reader.Close()
                                            'response.Close()


                                        End If
                                    End If




                                Case "C", "D"
                                    If vBit >= 0 And vBit <= 15 Then
                                        vAddr = (vAddr << 4) Or (vBit And &HF)
                                        Dim q As String
                                        If vValue > 0 Then
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=1")
                                        Else
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=0")
                                        End If


                                        Dim webClient As New System.Net.WebClient
                                        responseFromServer = webClient.DownloadString(q)
                                        webClient.Dispose()
                                        Console.WriteLine(q + "->" + responseFromServer)
                                        'request.Timeout = 1000
                                        '' If required by the server, set the credentials.
                                        'request.Credentials = CredentialCache.DefaultCredentials
                                        '' Get the response.
                                        'response = request.GetResponse()

                                        'dataStream = response.GetResponseStream()
                                        '' Open the stream using a StreamReader for easy access.
                                        'reader = New StreamReader(dataStream)
                                        '' Read the content.
                                        'responseFromServer = reader.ReadToEnd()

                                        'reader.Close()
                                        'response.Close()
                                    Else

                                        Dim q As String
                                        If vValue > 0 Then
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=1")
                                        Else
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=0")
                                        End If
                                        Dim webClient As New System.Net.WebClient
                                        responseFromServer = webClient.DownloadString(q)
                                        webClient.Dispose()
                                        Console.WriteLine(q + "->" + responseFromServer)
                                    End If


                            End Select


                        Catch ex As Exception
                            OK = False
                        End Try

                    Else
                        OK = False
                    End If

                End If

                System.Threading.Thread.Sleep(50)

                ''''''''''''''''''' REG2
                If QRegs > 1 Then
                    xval = dtcmd.Rows(i)("REG2").ToString()

                    vRegType = xval.Substring(0, 1).ToUpper()
                    If vRegType = "H" Or vRegType = "I" Or vRegType = "C" Or vRegType = "D" Then
                        vAddr = xval.Substring(1).ToUpper
                    Else
                        vRegType = "C"
                        vAddr = xval.ToString()
                    End If

                    ' формируем новое значение регистра
                    vBit = Integer.Parse(dtcmd.Rows(i)("BIT2").ToString())
                    vValue = UShort.Parse(dtcmd.Rows(i)("VALUE2").ToString())


                    If vRegType = "H" Or vRegType = "I" Or vRegType = "C" Or vRegType = "D" Then

                        Try
                            Select Case vRegType
                                Case "H", "I"
                                    If vBit = -1 Then

                                        Try
                                            Dim q As String
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vValue.ToString())
                                            Dim webClient As New System.Net.WebClient
                                            responseFromServer = webClient.DownloadString(q)
                                            webClient.Dispose()
                                            Console.WriteLine(q + "->" + responseFromServer)
                                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vValue.ToString())
                                            '' If required by the server, set the credentials.
                                            'request.Credentials = CredentialCache.DefaultCredentials
                                            '' Get the response.
                                            'response = request.GetResponse()

                                            'dataStream = response.GetResponseStream()
                                            '' Open the stream using a StreamReader for easy access.
                                            'reader = New StreamReader(dataStream)
                                            '' Read the content.
                                            'responseFromServer = reader.ReadToEnd()

                                            'reader.Close()
                                            'response.Close()

                                        Catch ex As Exception

                                        End Try
                                    Else
                                        If vBit >= 0 And vBit <= 15 Then
                                            If vValue > 0 Then
                                                vMask = 1 << vBit

                                            Else
                                                vMask = 1 << vBit
                                                vMask = Not vMask

                                            End If


                                            Dim q As String
                                            q = "http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vMask.ToString()
                                            Dim webClient As New System.Net.WebClient
                                            responseFromServer = webClient.DownloadString(q)
                                            webClient.Dispose()
                                            Console.WriteLine(q + "->" + responseFromServer)

                                            'request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?hreg=" + vAddr.ToString() + "&value=" + vMask.ToString())
                                            '' If required by the server, set the credentials.
                                            'request.Credentials = CredentialCache.DefaultCredentials
                                            '' Get the response.
                                            'response = request.GetResponse()

                                            'dataStream = response.GetResponseStream()
                                            '' Open the stream using a StreamReader for easy access.
                                            'reader = New StreamReader(dataStream)
                                            '' Read the content.
                                            'responseFromServer = reader.ReadToEnd()

                                            'reader.Close()
                                            'response.Close()


                                        End If
                                    End If




                                Case "C", "D"
                                    If vBit >= 0 And vBit <= 15 Then
                                        vAddr = (vAddr << 4) Or (vBit And &HF)

                                        Dim q As String
                                        If vValue > 0 Then
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=1")
                                        Else
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=0")
                                        End If

                                        Dim webClient As New System.Net.WebClient
                                        responseFromServer = webClient.DownloadString(q)
                                        webClient.Dispose()
                                        Console.WriteLine(q + "->" + responseFromServer)

                                        '' If required by the server, set the credentials.
                                        'request.Credentials = CredentialCache.DefaultCredentials
                                        '' Get the response.
                                        'response = request.GetResponse()

                                        'dataStream = response.GetResponseStream()
                                        '' Open the stream using a StreamReader for easy access.
                                        'reader = New StreamReader(dataStream)
                                        '' Read the content.
                                        'responseFromServer = reader.ReadToEnd()

                                        'reader.Close()
                                        'response.Close()
                                    Else
                                        'If vValue > 0 Then
                                        '    ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=1")
                                        'Else
                                        '    request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=0")
                                        'End If

                                        Dim q As String
                                        If vValue > 0 Then
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=1")
                                        Else
                                            q = ("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/set?coil=" + vAddr.ToString() + "&value=0")
                                        End If

                                        Dim webClient As New System.Net.WebClient
                                        responseFromServer = webClient.DownloadString(q)
                                        webClient.Dispose()
                                        Console.WriteLine(q + "->" + responseFromServer)

                                        '' If required by the server, set the credentials.
                                        'request.Credentials = CredentialCache.DefaultCredentials
                                        '' Get the response.
                                        'response = request.GetResponse()

                                        'dataStream = response.GetResponseStream()
                                        '' Open the stream using a StreamReader for easy access.
                                        'reader = New StreamReader(dataStream)
                                        '' Read the content.
                                        'responseFromServer = reader.ReadToEnd()

                                        'reader.Close()
                                        'response.Close()
                                    End If


                            End Select


                        Catch ex As Exception
                            OK = False
                        End Try

                    Else
                        OK = False
                    End If

                End If




                If OK Then cnt = cnt + 1

                MyTV.QueryExec("UPDATE RCW SET DONE=1,RCWDATE=SYSDATE WHERE RCW.ROWID='" + dtcmd.Rows(i)("ROWID") + "'")


                System.Threading.Thread.Sleep(50)



            Next
            Return cnt

        End If

        Return 0
    End Function



    Private Sub Areal2DT(ByRef dt As DataTable)
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        Dim dataStream As Stream
        Dim reader As StreamReader
        Dim responseFromServer As String
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim l As Integer
        Dim s As String
        Dim dr As DataRow
        Dim HCbits As Integer

#Region "read"
        Try
            request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?ireg=0")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            response = request.GetResponse()

            dataStream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            reader = New StreamReader(dataStream)
            ' Read the content.
            responseFromServer = reader.ReadToEnd()

            reader.Close()
            response.Close()


            Try
                i = Integer.Parse(responseFromServer)
            Catch ex As Exception
                i = 0
            End Try

        Catch ex As Exception

        End Try


        Try
            request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?ireg=1")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            response = request.GetResponse()

            dataStream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            reader = New StreamReader(dataStream)
            ' Read the content.
            responseFromServer = reader.ReadToEnd()

            reader.Close()
            response.Close()


            Try
                j = Integer.Parse(responseFromServer)
            Catch ex As Exception
                j = 0
            End Try

        Catch ex As Exception

        End Try

        Try
            request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?ireg=2")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            response = request.GetResponse()

            dataStream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            reader = New StreamReader(dataStream)
            ' Read the content.
            responseFromServer = reader.ReadToEnd()

            reader.Close()
            response.Close()


            Try
                k = Integer.Parse(responseFromServer)
            Catch ex As Exception
                k = 0
            End Try

        Catch ex As Exception

        End Try


        Try
            request = HttpWebRequest.Create("http://" & MyBase.ServerIp & ":" & MyBase.IPPort.ToString() & "/readx?ireg=3")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            response = request.GetResponse()

            dataStream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            reader = New StreamReader(dataStream)
            ' Read the content.
            responseFromServer = reader.ReadToEnd()

            reader.Close()
            response.Close()


            Try
                l = Integer.Parse(responseFromServer)
            Catch ex As Exception
                l = 0
            End Try

        Catch ex As Exception

        End Try
#End Region

        Dim t As Integer
        Dim m As Boolean = False

        HCbits = 0


        dr = dt.NewRow
        dr("Name") = "P1"
        If (k And 1) = 1 Then

            dr("Value") = 1
            HCbits += 1
        Else
            dr("Value") = 0
        End If
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Name") = "P2"
        If (k And 2) = 2 Then
            dr("Value") = 1
            HCbits += 2
        Else
            dr("Value") = 0
        End If
        dt.Rows.Add(dr)


        dr = dt.NewRow
        dr("Name") = "T1"
        s = ""
        t = 0
        If (i And 1) = 1 Then
            t = 1
            s = "сухой ход"
        End If

        If (i And 2) = 2 Then
            t = 2
            s = "1 уровень"
        End If

        If (i And 4) = 4 Then
            t = 3
            s = "2 уровень"
        End If
        If (i And 8) = 8 Then
            s = "3 уровень"
            t = 4
        End If
        If (i And 16) = 16 Then
            s = "аварийный уровень"
            t = 5
            HCbits += 4
        End If
        dr("Value") = t
        dt.Rows.Add(dr)


        dr = dt.NewRow
        dr("Name") = "M1"
        m = True
        t = 0
        s = ""
        If (i And 128) = 128 Then
            t = 1
            s = "готовность"
        End If

        If (i And 256) = 256 Then
            t = 2
            s = "работа"
        End If

        If (k And 16) = 16 Then
            t = 3
            s = "авария"
            HCbits += 256
            HCbits += 8
        End If

        If (i And 512) = 512 Then
            m = False
            s = "авт. " & s
        Else


            s = "руч. " & s
        End If

        If m Then
            dr("Value") = -t
        Else
            dr("Value") = t
        End If
        dt.Rows.Add(dr)


        dr = dt.NewRow
        dr("Name") = "M2"



        m = True
        t = 0
        s = ""
        If (i And 1024) = 1024 Then
            t = 1
            s = "готовность"
        End If

        If (i And 2048) = 2048 Then
            t = 2
            s = "работа"
        End If

        If (k And 64) = 64 Then
            t = 3
            s = "авария"
            HCbits += 16
        End If


        If (j And 1) = 1 Then
            m = False
            s = "авт. " & s
        Else


            s = "руч. " & s
        End If

        If m Then
            dr("Value") = -t
        Else
            dr("Value") = t
        End If
        dt.Rows.Add(dr)


        dr = dt.NewRow
        dr("Name") = "M3"


        m = True
        t = 0
        s = ""
        If (j And 2) = 2 Then
            t = 1
            s = "готовность"
        End If

        If (j And 4) = 4 Then
            t = 2
            s = "работа"
        End If

        If (l And 1) = 1 Then
            t = 3
            s = "авария"
            HCbits += 32
        End If

        If (j And 8) = 8 Then
            m = False
            s = "авт. " & s
        Else


            s = "руч. " & s
        End If

        If m Then
            dr("Value") = -t
        Else
            dr("Value") = t
        End If
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Name") = "ERR1"
        dr("Value") = HCbits
        dt.Rows.Add(dr)



    End Sub

    Public Overrides Function ReadMArch() As String

        Dim d As Date
        d = GetDeviceDate()



        Dim ok As Boolean = False


        clearMArchive(mArch)
        mArch.archType = 1
        mArch.DateArch = d

        Dim dt As DataTable

        dt = New DataTable
        dt.Columns.Add("Name")
        dt.Columns.Add("Value")





        Dim Xml As XmlDocument = New XmlDocument()
        Xml.LoadXml(sXML)
        Dim root As XmlElement
        Dim node As XmlElement
        Dim xlst As XmlNodeList
        Dim xattr As XmlAttribute


        root = CType(Xml.LastChild, XmlElement)
        xlst = root.GetElementsByTagName("device")
        If (xlst.Count > 0) Then
            node = CType(xlst.Item(0), XmlElement)       ' device
            For Each xattr In node.Attributes
                ok = ReadItem(dt, xattr.Name, xattr.Value)
            Next
        End If



        xlst = root.GetElementsByTagName("current")
        If (xlst.Count > 0) Then

            node = CType(xlst.Item(0), XmlElement)      ' // device
            For Each xattr In node.Attributes
                ok = ReadItem(dt, xattr.Name, xattr.Value)
            Next

        End If



        'Areal2DT(dt)





        If dt.Rows.Count > 0 Then


            ok = True


            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                Select Case dt.Rows(i)("Name").ToString().ToUpper
                    Case "M1"
                        mArch.M1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M2"
                        mArch.M2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M3"
                        mArch.M3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M4"
                        mArch.M4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M5"
                        mArch.M5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M6"
                        mArch.M6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "V1"
                        mArch.V1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V2"
                        mArch.V2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V3"
                        mArch.V3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V4"
                        mArch.V4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V5"
                        mArch.V5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V6"
                        mArch.V6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "P1"
                        mArch.p1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P2"
                        mArch.p2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P3"
                        mArch.p3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P4"
                        mArch.p4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P5"
                        mArch.p5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P6"
                        mArch.p6 = Str2Dbl(dt.Rows(i)("Value").ToString())


                    Case "T1"
                        mArch.t1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T2"
                        mArch.t2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T3"
                        mArch.t3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T4"
                        mArch.t4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T5"
                        mArch.t5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T6"
                        mArch.t6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "Q1"
                        mArch.Q1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q2"
                        mArch.Q2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q3"
                        mArch.Q3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q4"
                        mArch.Q4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q5"
                        mArch.Q5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q6"
                        mArch.Q6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "ERR1"
                        mArch.HCtv1 = Long.Parse(dt.Rows(i)("Value"))
                        mArch.HC = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERR2"
                        mArch.HCtv2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME1"
                        mArch.WORKTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME2"
                        mArch.WORKTIME2 = Long.Parse(dt.Rows(i)("Value"))
                End Select

            Next
        End If


march_final:

        EraseInputQueue()
        If ok = False Then
            EraseInputQueue()
            isMArchToDBWrite = False
            Return "Ошибка чтения мгновенного архива "
        End If

        isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function






    Public Overrides Function ReadTArch() As String
        Dim d As Date
        d = GetDeviceDate()


        Dim ok As Boolean = False

        clearTArchive(tArch)
        tArch.archType = 2
        tArch.DateArch = d
        Dim dt As DataTable

        dt = New DataTable

        If dt.Rows.Count > 0 Then


            ok = True

            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                Select Case dt.Rows(i)("Name").ToString().ToUpper
                    Case "M1"
                        tArch.M1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M2"
                        tArch.M2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M3"
                        tArch.M3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M4"
                        tArch.M4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M5"
                        tArch.M5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M6"
                        tArch.M6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "V1"
                        tArch.V1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V2"
                        tArch.V2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V3"
                        tArch.V3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V4"
                        tArch.V4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V5"
                        tArch.V5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V6"
                        tArch.V6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "P1"
                        tArch.P1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P2"
                        tArch.P2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P3"
                        tArch.P3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P4"
                        tArch.P4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P5"
                        tArch.P5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "P6"
                        tArch.P6 = Str2Dbl(dt.Rows(i)("Value").ToString())


                    Case "T1"
                        tArch.T1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T2"
                        tArch.T2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T3"
                        tArch.T3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T4"
                        tArch.T4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T5"
                        tArch.T5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "T6"
                        tArch.T6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "Q1"
                        tArch.Q1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q2"
                        tArch.Q2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q3"
                        tArch.Q3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q4"
                        tArch.Q4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q5"
                        tArch.Q5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q6"
                        tArch.Q6 = Str2Dbl(dt.Rows(i)("Value").ToString())



                    Case "M1N"
                        tArch.M1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M2N"
                        tArch.M2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M3N"
                        tArch.M3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M4N"
                        tArch.M4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M5N"
                        tArch.M5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "M6N"
                        tArch.M6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "V1N"
                        tArch.V1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V2N"
                        tArch.V2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V3N"
                        tArch.V3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V4N"
                        tArch.V4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V5N"
                        tArch.V5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "V6N"
                        tArch.V6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "Q1N"
                        tArch.Q1 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q2N"
                        tArch.Q2 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q3N"
                        tArch.Q3 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q4N"
                        tArch.Q4 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q5N"
                        tArch.Q5 = Str2Dbl(dt.Rows(i)("Value").ToString())
                    Case "Q6N"
                        tArch.Q6 = Str2Dbl(dt.Rows(i)("Value").ToString())

                    Case "ERR1"
                        tArch.HCtv1 = Long.Parse(dt.Rows(i)("Value"))
                        tArch.HC = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERR2"
                        tArch.HCtv2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME1N"
                        tArch.WORKTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME2N"
                        tArch.WORKTIME2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME1N_"
                        tArch.OKTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "WORKTIME2N_"
                        tArch.OKTIME2 = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERRTIME1"
                        tArch.ERRTIME1 = Long.Parse(dt.Rows(i)("Value"))
                    Case "ERRTIME2"
                        tArch.ERRTIME2 = Long.Parse(dt.Rows(i)("Value"))
                End Select

            Next
        End If

tarch_final:

        If ok = False Then

            isTArchToDBWrite = False
            Return "Ошибка чтения итогового архива "
        End If

        isTArchToDBWrite = True
        Return "Итоговый архив прочитан"

    End Function



    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function



    Public Overrides Function IsConnected() As Boolean

        Return mIsConnected
    End Function


    Public Overrides Function ReadSystemParameters() As System.Data.DataTable

        TryConnect()
        EraseInputQueue()
        Dim dt As DataTable
        Dim dr As DataRow
        Dim cn(34) As String
        Dim sn(26) As String





        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")

        Dim d As Date
        d = GetDeviceDate()


        Dim ok As Boolean = False



        Return dt
    End Function



    Private m_CFGPath As String = ""


    Public Overrides Sub Connect()
        Dim i As Integer


        For i = 0 To 5
            If TryConnect() Then
                mIsConnected = True
                Return ' True
            End If
        Next
        Return 'False

    End Sub



    Public Sub New()

    End Sub
    Public Overrides Sub CloseTransportConnect()
        MyBase.CloseTransportConnect()

    End Sub
End Class
