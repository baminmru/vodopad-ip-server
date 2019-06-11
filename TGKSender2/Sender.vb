Public Class Sender

    Private SZTDB As DB
    Private Inited As Boolean

    Private Function LogPath() As String
        Return System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\tgksender_log.txt"
    End Function


    'Private Sub KillVPN()
    '    Dim pp As System.Diagnostics.Process()
    '    Dim epp As System.Diagnostics.Process = Nothing
    '    Dim pidx As Long
    '    pp = System.Diagnostics.Process.GetProcessesByName(SZTDB.VPNGUINAME)
    '    For pidx = 0 To pp.Length - 1
    '        epp = pp(pidx)
    '        If Not epp Is Nothing Then
    '            Try
    '                epp.Kill()
    '            Catch ex As Exception

    '            End Try
    '            epp = Nothing
    '        End If
    '    Next
    'End Sub

    'Private Function StartVPN() As Boolean
    '    KillVPN()
    '    Dim myProcess As Process
    '    Try
    '        myProcess = Process.Start(SZTDB.VPNGUIPATH, SZTDB.VPNGUIARGS)
    '    Catch ex As System.Exception
    '        Return False
    '    End Try
    '    System.Threading.Thread.Sleep(10000)
    '    Return True
    'End Function

    'Private Function IsVPNOK() As Boolean
    '    Dim soc As System.Net.Sockets.Socket
    '    soc = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
    '    soc.Blocking = True

    '    Try
    '        soc.Connect(SZTDB.TEST_VPN_IP, SZTDB.TEST_VPN_PORT)

    '    Catch
    '    End Try

    '    If soc.Connected Then
    '        Try
    '            soc.Close()
    '        Catch ex As Exception

    '        End Try

    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function


    Public Function SendAllToTGK(Optional ByVal NoSend As Boolean = False, Optional ByVal deleteAfterSend As Boolean = True, Optional ByVal usedates As Boolean = False, Optional ByVal d1 As Long = 0, Optional ByVal d2 As Long = 0) As String
        Dim Log As String
        Dim OK As Boolean

        Log = ""
   
        If SZTDB.Init() Then
            Inited = True

            My.Computer.FileSystem.WriteAllText(LogPath(), "Start at " & Format(Date.Now, "dd.MM.yyyy HH:mm") & vbCrLf, True, System.Text.Encoding.Default)

            ' проверяем не появилось ли данных для пропущенных дат
            ' CheckForLostData()



            ' попытка отослать архивы, которые не удалось отослать ранее
            ' или это могут быть данные за пропущенные даты
            SendArchives()


            Dim dt As DataTable

            Dim i As Long
            Dim sfile As String
            Dim fileData As String
            Dim result As Short
            Dim sdate As Date
            Dim edate As Date
            Dim pass2 As Boolean



            ' получаем список устройств для посылки отчета
            dt = SZTDB.GetTGK1Dev()

            For i = 0 To dt.Rows.Count - 1
                ' это на всякий случай, если все-таки глючит ситуация с  левой датой
                pass2 = False
lbl_pass2:
                OK = False

                If usedates Then
                    sdate = New Date(d1)
                    edate = New Date(d2)
                Else
                    sdate = DateAdd(DateInterval.Hour, -12, Date.Now)
                    edate = New Date(sdate.Year, sdate.Month, sdate.Day, 23, 59, 59)
                End If


                My.Computer.FileSystem.WriteAllText(LogPath(), "Node: " & dt.Rows(i)("CSHORT") & vbCrLf, True, System.Text.Encoding.Default)

                ' если канал есть, то начинаем процесс
                If True Then


                    ' проверяем надо ли отсылать данные

                    If usedates Then
                        OK = True
                    Else
                        ' запрос нужных дат в ТГК
                        OK = SZTDB.CheckDeviceAtTGK(dt.Rows(i)("EQType"), dt.Rows(i)("vich_num"), sdate, edate)
                    End If


                    If OK Then
                        Dim pute As New puteclient.PuteClient()

                        ' немного урезаем апетиты, чтобы не нарваться на ситуацию с отсутствующими полностью данными
                        If DateAdd(DateInterval.Day, +5, sdate) < edate Then
                            If pass2 = False Then
                                edate = DateAdd(DateInterval.Day, +5, sdate)
                                pass2 = True
                            Else
                                sdate = DateAdd(DateInterval.Hour, -12, Date.Now)
                                pass2 = False
                            End If
                        Else
                            pass2 = False
                        End If

                        ' немного урезаем апетиты, чтобы не нарваться на ситуацию с отсутствующими полностью данными
                        If edate >= DateAdd(DateInterval.Minute, -60, Date.Now) Then
                            edate = DateAdd(DateInterval.Minute, -60, Date.Now)
                        End If



                        'сформировать файл архива по узлу
                        sfile = SZTDB.ExecTGK1Proc(dt.Rows(i)("id_bd"), dt.Rows(i)("reportproc"), dt.Rows(i)("CSHORT"), sdate, edate)

                        If sfile <> "" Then

                            fileData = ""

                            Try
                                ' читаем файл
                                fileData = My.Computer.FileSystem.ReadAllText(SZTDB.FileDirectory & sfile, _
                                   System.Text.Encoding.Default)
                            Catch ex As Exception
                                My.Computer.FileSystem.WriteAllText(LogPath(), vbCrLf & ex.Message & vbCrLf, True, System.Text.Encoding.Default)
                                OK = False
                            End Try


                            My.Computer.FileSystem.WriteAllText(LogPath(), vbCrLf & fileData & vbCrLf, True, System.Text.Encoding.Default)

                            result = 0
                            ' канал все еще есть ?
                            If OK Then
                                ' пытаемся отослать

                                Try
                                    ' отсылка
                                    result = pute.COMSaveArchive(fileData)
                                Catch
                                    result = 1
                                End Try
                            Else
                                My.Computer.FileSystem.WriteAllText(LogPath(), "ошибка отправки файла  " & vbCrLf, True, System.Text.Encoding.Default)
                                Log = Log & "Проблемы..." & vbCrLf
                                GoTo storefile
                            End If


storefile:
                            If result = 0 Then


                                Log = Log & "OK-" & sfile & " " & dt.Rows(i)("vich_num") & " " & Format(sdate, "dd.MM.yyyy HH:mm") & "-" & Format(edate, "dd.MM.yyyy HH:mm") & vbCrLf

                                My.Computer.FileSystem.WriteAllText(LogPath(), "OK-" & sfile & " " & dt.Rows(i)("vich_num") & " " & Format(sdate, "dd.MM.yyyy HH:mm") & "-" & Format(edate, "dd.MM.yyyy HH:mm") & vbCrLf, True, System.Text.Encoding.Default)


                                If deleteAfterSend Then
                                    ' удаляем файл с архивом
                                    Try
                                        My.Computer.FileSystem.DeleteFile(SZTDB.FileDirectory & sfile)
                                    Catch
                                    End Try
                                End If

                            Else

                                Try
                                    ' сохранить файл, который не удалось отослать в папке FilesToSend
                                    My.Computer.FileSystem.WriteAllText(System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\FilesToSend\" & Guid.NewGuid.ToString() & ".dat", fileData, False, System.Text.Encoding.Default)
                                Catch
                                End Try

                                If deleteAfterSend Then
                                    ' удаляем файл с архивом
                                    Try
                                        My.Computer.FileSystem.DeleteFile(SZTDB.FileDirectory & sfile)
                                    Catch
                                    End Try
                                End If

                                Log = Log & "Error[" & result.ToString() & "]-" & sfile & " " & dt.Rows(i)("vich_num") & vbCrLf
                                My.Computer.FileSystem.WriteAllText(LogPath(), "Error[code=" & result.ToString() & "]-" & sfile & " " & dt.Rows(i)("vich_num") & vbCrLf, True, System.Text.Encoding.Default)

                                ' если беда по причине падения канала, то уходим из процедуры
                                'If Not IsVPNOK() Then
                                '    Return Log
                                'End If

                            End If


                        Else
                            My.Computer.FileSystem.WriteAllText(LogPath(), "Error[no file]- " & dt.Rows(i)("reportproc") & " " & dt.Rows(i)("CSHORT") & vbCrLf, True, System.Text.Encoding.Default)
                            Log = Log & "Error[No file to send]- " & dt.Rows(i)("reportproc") & " " & dt.Rows(i)("CSHORT")
                        End If
                    Else
                        My.Computer.FileSystem.WriteAllText(LogPath(), "Данные не требуются " & " " & dt.Rows(i)("CSHORT") & vbCrLf, True, System.Text.Encoding.Default)
                        Log = Log & "Данные не требуются " & " " & dt.Rows(i)("CSHORT") & vbCrLf
                        pass2 = False
                    End If

                End If
                If pass2 Then
                    GoTo lbl_pass2
                End If
                My.Computer.FileSystem.WriteAllText(LogPath(), vbCrLf & "*****************" & vbCrLf, True, System.Text.Encoding.Default)
            Next
            My.Computer.FileSystem.WriteAllText(LogPath(), "Stop at " & Format(Date.Now, "dd.MM.yyyy HH:mm") & vbCrLf, True, System.Text.Encoding.Default)
        End If
        Return Log
    End Function

   


    Private Sub SendArchives()
        Dim di As System.IO.DirectoryInfo
        Dim fi As System.IO.FileInfo()
        Dim fileData As String
        Dim i As Long
        Dim result As Short
        Dim pute As New puteclient.PuteClient


        di = New System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\FilesToSend\")
        fi = di.GetFiles("*.dat")


        My.Computer.FileSystem.WriteAllText(LogPath(), vbCrLf & "Load files from " & di.FullName & vbCrLf, True, System.Text.Encoding.Default)

        For i = LBound(fi, 1) To UBound(fi, 1)

            ' читаем файл
            fileData = My.Computer.FileSystem.ReadAllText(fi(i).FullName, _
               System.Text.Encoding.Default)

            My.Computer.FileSystem.WriteAllText(LogPath(), vbCrLf & "file (" & i.ToString() & ") = " & fi(i).FullName & vbCrLf, True, System.Text.Encoding.Default)


            My.Computer.FileSystem.WriteAllText(LogPath(), vbCrLf & fileData & vbCrLf, True, System.Text.Encoding.Default)

            If True Then
                ' пытаемся отослать
                result = 0
                Try
                    result = pute.COMSaveArchive(fileData)
                Catch
                    result = 1
                End Try
            Else
                My.Computer.FileSystem.WriteAllText(LogPath(), "нет  соединения  " & vbCrLf, True, System.Text.Encoding.Default)
                Exit Sub
            End If


            '' попытка 2

            'If result = 1 Then
            '    If IsVPNOK() Then
            '        System.Threading.Thread.Sleep(500)
            '        result = 0
            '        Try
            '            result = pute.ComSaveArchive(fileData)
            '        Catch
            '            result = 1
            '        End Try
            '    Else
            '        My.Computer.FileSystem.WriteAllText(LogPath(), "нет VPN соединения  " & vbCrLf, True)
            '        Exit Sub
            '    End If

            'End If


            ''попытка 3
            'If result = 1 Then
            '    If IsVPNOK() Then
            '        System.Threading.Thread.Sleep(500)
            '        result = 0
            '        Try
            '            result = pute.ComSaveArchive(fileData)
            '        Catch
            '            result = 1
            '        End Try
            '    Else
            '        My.Computer.FileSystem.WriteAllText(LogPath(), "нет VPN соединения  " & vbCrLf, True)
            '        Exit Sub
            '    End If

            'End If

            If result = 0 Then
                My.Computer.FileSystem.WriteAllText(LogPath(), "OK-Сохраненный архив отослан" & vbCrLf, True, System.Text.Encoding.Default)
            End If

            ' удаляем файл с архивом
            Try
                My.Computer.FileSystem.DeleteFile(fi(i).FullName)
            Catch
                My.Computer.FileSystem.WriteAllText(LogPath(), "Error-Ошибка удаления файла: " & fi(i).FullName & vbCrLf, True, System.Text.Encoding.Default)
            End Try




            '

        Next


    End Sub

    Public Sub New()
        SZTDB = New DB

    End Sub

    Protected Overrides Sub Finalize()
        If inited Then
            SZTDB.EndWork()
        End If

        MyBase.Finalize()
    End Sub
End Class
