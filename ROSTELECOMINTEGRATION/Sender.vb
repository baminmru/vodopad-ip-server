Imports System.Net
Imports ROSTELECOMINTEGRATION
Imports ROSTELECOMINTEGRATION.WebReference
Imports System.IO
Imports System.Text

Public Class Sender

    Private SZTDB As DB
    Private Inited As Boolean

    Private Function LogPath() As String
        Return System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\RTintegration_log.txt"
    End Function

    Public Sub SendAllToRTI(Optional ByVal NoSend As Boolean = False, Optional ByVal usedates As Boolean = False, Optional ByVal d1 As Long = 0, Optional ByVal d2 As Long = 0)


        If SZTDB.Init() Then
            Inited = True

            My.Computer.FileSystem.WriteAllText(LogPath(), "Start at " & Format(Date.Now, "dd.MM.yyyy HH:mm") & vbCrLf, True, System.Text.Encoding.Default)
            Dim sdate As Date
            Dim edate As Date
            Dim cur As Date

            If usedates Then
                sdate = New Date(d1)
                edate = New Date(d2)

            Else

                cur = Date.Today
                cur = cur.AddDays(-1)
                sdate = New Date(cur.Year, cur.Month, cur.Day, 0, 0, 0)
                edate = New Date(cur.Year, cur.Month, cur.Day, 0, 0, 0)
            End If

            cur = sdate
            While cur <= edate

                cur = New Date(cur.Year, cur.Month, cur.Day, 0, 0, 0)
                'сформировать файлs архивов
                SZTDB.ExecRTIProc("XML_LST_FULL", cur)


                Dim service As ROSTELECOMINTEGRATION.WebReference.Integration
                service = New ROSTELECOMINTEGRATION.WebReference.Integration



                Dim myCredentials As System.Net.CredentialCache = New System.Net.CredentialCache()
                Dim netCred As NetworkCredential = New NetworkCredential("severo_zapad", "123456")
                myCredentials.Add(New Uri(service.Url), "Basic", netCred)
                service.Credentials = myCredentials

                Dim req As ROSTELECOMINTEGRATION.WebReference.RequestType
                Dim resp As ROSTELECOMINTEGRATION.WebReference.ResponseType

                Dim di As DirectoryInfo
                Dim fi As FileInfo
                Dim SINP As String
                Dim sarr() As Byte
                Dim ok As Boolean


                di = New DirectoryInfo(SZTDB.FileDirectory)
                For Each fi In di.GetFiles("*.xml")
                    ok = False
                    My.Computer.FileSystem.WriteAllText(LogPath(), "Отслаем файл :" & fi.FullName & vbCrLf, True, System.Text.Encoding.Default)

                    req = New ROSTELECOMINTEGRATION.WebReference.RequestType
                    req.Header = New ROSTELECOMINTEGRATION.WebReference.HeaderType

                    req.Header.Operation = "CONSUME_CAE_020"
                    req.Header.RequestId = Guid.NewGuid().ToString
                    req.Header.SendDate = DateTime.Now
                    req.Header.Sender = "MEE"
                    req.Header.Signature = "sign"
                    req.Header.Version = "1.0.0"
                    Console.WriteLine(fi.FullName)
                    SINP = File.ReadAllText(fi.FullName, System.Text.Encoding.GetEncoding(1251))

                    sarr = System.Text.Encoding.GetEncoding(1251).GetBytes(SINP)
                    req.Data = sarr

                    My.Computer.FileSystem.WriteAllText(LogPath(), "Размер файла :" & sarr.Length.ToString & vbCrLf, True, System.Text.Encoding.Default)

                    Try
                        resp = service.post(req)
                        If Not resp Is Nothing Then
                            Console.WriteLine("Version:" + resp.Header.Version)
                            Console.WriteLine("RequestId:" + resp.Header.RequestId)
                            Console.WriteLine("Sender:" + resp.Header.Sender)
                            Console.WriteLine("Signature:" + resp.Header.Signature)
                            Console.WriteLine("Operation:" + resp.Header.Operation)

                            Console.WriteLine(resp.Code)
                            If resp.Code.ToLower = "ok" Then
                                ok = True
                            End If
                            My.Computer.FileSystem.WriteAllText(LogPath(), resp.Code & vbCrLf, True, System.Text.Encoding.Default)

                            Console.WriteLine(resp.Description)
                            If Not resp.Data Is Nothing Then
                                Console.WriteLine("resp.data begin")
                                Console.WriteLine(System.Text.Encoding.ASCII.GetString(resp.Data))
                                Console.WriteLine("resp.data end")

                                My.Computer.FileSystem.WriteAllText(LogPath(), System.Text.Encoding.ASCII.GetString(resp.Data) & vbCrLf, True, System.Text.Encoding.Default)
                            End If
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        My.Computer.FileSystem.WriteAllText(LogPath(), ex.Message & vbCrLf, True, System.Text.Encoding.Default)

                    End Try

                    'If ok And deleteAfterSend Then

                    ' удаляем файл с архивом
                    Try
                        My.Computer.FileSystem.DeleteFile(fi.FullName)
                    Catch
                    End Try


                Next
                cur = cur.AddDays(1)
            End While

            My.Computer.FileSystem.WriteAllText(LogPath(), "Stop at " & Format(Date.Now, "dd.MM.yyyy HH:mm") & vbCrLf, True, System.Text.Encoding.Default)
        End If

    End Sub



    
    Public Sub New()
        SZTDB = New DB

    End Sub

    Protected Overrides Sub Finalize()
        If Inited Then
            SZTDB.EndWork()
        End If

        MyBase.Finalize()
    End Sub
End Class
