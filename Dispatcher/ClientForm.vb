
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinTree
Imports VIPAnalizer
Imports Oracle.DataAccess.Client

Imports SpreadsheetGear

Public Class ClientForm
    Inherits System.Windows.Forms.Form
    Dim LastCaption As String
    Dim archType_hour As Short = 3
    Dim archType_day As Short = 4
    Dim archType_moment As Short = 1
    Dim archType_total As Short = 2
    Dim bActivated As Boolean = False
    Dim ftM As Boolean = True
    Dim ftH As Boolean = True
    Dim ftD As Boolean = True
    Dim ftT As Boolean = True
    Dim ftL As Boolean = True

    Private WithEvents workbook As IWorkbook
    Private WithEvents outworkbook As IWorkbook
    Private WithEvents ws As IWorksheet
    Private WithEvents outws As IWorksheet

    Private StopReading As Boolean

    Public localtree As Infragistics.Win.UltraWinTree.UltraTree

    Dim timesp As System.TimeSpan

    Private Function SentRcv() As String
        Dim ut As STKTVMain.UniTransport
        If Not TvMain.TVD Is Nothing Then
            ut = TvMain.TVD.DriverTransport
            If Not ut Is Nothing Then
                Return "(R:" & ut.BytesReceived.ToString & " S:" & ut.BytesSent.ToString & ")"
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function
    Private Sub ReadTimer_Tick(sender As Object, e As System.EventArgs) Handles ReadTimer.Tick
        timesp = New System.TimeSpan
        timesp = DateTime.Now - queryStartTime
        ReadTimer.Interval = 100
        labelTime.Text = Format(timesp.Minutes, "00") + ":" + Format(timesp.Seconds, "00")
        Select Case LastAction
            Case STKTVMain.UnitransportAction.Connected
                lblError.Text = "Соединение установлено" + vbCrLf + lblError.Text
                lblActiopn.Text = "Соединение установлено " & SentRcv()
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Green
            Case STKTVMain.UnitransportAction.Connecting
                lblActiopn.Text = "Соединяемся " & SentRcv()
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.Destroy
                lblActiopn.Text = "Транспорт удален " & SentRcv()
            Case STKTVMain.UnitransportAction.Disconnected
                lblActiopn.Text = "Отключен " & SentRcv()
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.Disconnecting
                lblActiopn.Text = "Отключение " & SentRcv()
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.ReceiveData
                lblActiopn.Text = "Получены данные " & SentRcv() & LastMSG
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Green
                lblRcv.BackColor = Color.Red
            Case STKTVMain.UnitransportAction.SendData
                lblActiopn.Text = "Отосланы данные " & SentRcv() & LastMSG
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Green
                lblSnd.BackColor = Color.Red
            Case STKTVMain.UnitransportAction.SettingUp
                lblActiopn.Text = "Настроен " & SentRcv()
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.Wait
                lblActiopn.Text = "Ожидание " & SentRcv()
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Green
            Case STKTVMain.UnitransportAction.LowLevelStop
                lblError.Text = "Ошибка связи." + LastMSG + vbCrLf + lblError.Text
                lblActiopn.Text = "Ошибка связи." + LastMSG
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
        End Select
        LastAction = STKTVMain.UnitransportAction.NoAction
        Application.DoEvents()

    End Sub

    Private Sub ButtonReadArch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReadArch.Click
        lblNodeName.Text = LastCaption
        Application.DoEvents()



        If chkSystem.Checked = False And chkByList.Checked = False And CheckBoxMoment.Checked = False And CheckBoxHour.Checked = False And CheckBoxDay.Checked = False And chkTotal.Checked = False Then
            lblNodeName.Text = ""
            Return
        End If



        Dim tag As Object

        If localtree.SelectedNodes.Count > 0 Then
            If localtree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = localtree.SelectedNodes.Item(0).Tag
            Else
                lblNodeName.Text = ""
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        queryStartTime = DateTime.Now
        ReadTimer.Enabled = True

        lblError.Text = ""

        Dim query As String
        Dim qdt As DataTable
        Dim str As String


        'query = "select npquery from bdevices  where npquery=1 and  bdevices.id_bd=" + tag.ToString()
        'qdt = TvMain.QuerySelect(query)
        'If qdt.Rows.Count = 0 Then
        '    AppendInfo("Для прибора не включен режим опроса по новому алгоритму. " & DateTime.Now.ToString)
        '    MsgBox("Для прибора не включен режим опроса по новому алгоритму.")
        '    ReadTimer.Enabled = False
        '    Exit Sub
        'End If

        query = "select npquery,transport from bdevices  where  ( nplock is null or nplock <sysdate )    and bdevices.id_bd=" + tag.ToString() + " "
        qdt = TvMain.QuerySelect(query)
        If qdt.Rows.Count = 0 Then
            AppendInfo("Тепловычислитель занят, идет опрос" & DateTime.Now.ToString)
            Application.DoEvents()
            MsgBox(lblNodeName.Text + ". Тепловычислитель занят, идет опрос")
            ReadTimer.Enabled = False
            lblNodeName.Text = ""
            Exit Sub
        End If

        If qdt.Rows.Count = 1 Then
            If qdt.Rows(0)("transport") = 5 Or qdt.Rows(0)("transport") = 6 Then
                AppendInfo("Тепловычислитель подключен по GPRS, прямой опрос невозможен")
                Application.DoEvents()
                MsgBox(lblNodeName.Text + ". Тепловычислитель подключен по GPRS, прямой опрос невозможен, запрос по списку будет поставлен в очередь на опрос.")
                ReadTimer.Enabled = False
                lblNodeName.Text = ""


                If (chkByList.Checked = True) Then
                    If lstDT.Rows.Count > 0 Then
                        Dim iii As Integer
                        Dim atype As String
                        Dim adat As DateTime
                        For iii = 0 To lstDT.Rows.Count - 1
                            adat = lstDT.Rows(iii)("atime")
                            atype = lstDT.Rows(iii)("atype")
                            If atype = "Час." Then
                                TvMain.QueryExec("delete from QLIST where id_bd=" + tag.ToString() + " and QDATE=" + TvMain.OracleDate(adat) + " and ID_PTYPE=3")
                                TvMain.QueryExec("insert into QLIST(QLISTID,ID_BD,QDATE,ID_PTYPE) values( QLIST_SEQ.nextval," + tag.ToString() + "," + TvMain.OracleDate(adat) + ",3)")
                            Else
                                TvMain.QueryExec("delete from QLIST where id_bd=" + tag.ToString() + " and QDATE=" + TvMain.OracleDate(adat) + " and ID_PTYPE=4")
                                TvMain.QueryExec("insert into QLIST(QLISTID,ID_BD,QDATE,ID_PTYPE) values( QLIST_SEQ.nextval," + tag.ToString() + "," + TvMain.OracleDate(adat) + ",4)")
                            End If

                        Next
                    End If
                End If

                ' заставить модем позвонить на ТВ
                TvMain.QueryExec("update plancall set dlastcall=sysdate-1/24 where id_bd=" + tag.ToString())

                Exit Sub
            End If
        End If


        TvMain.ClearDuration()
        If Not TvMain.LockDevice(tag, 120, False) Then
            MsgBox(lblNodeName.Text + ". Тепловычислитель занят")
            ReadTimer.Enabled = False
            lblNodeName.Text = ""
            Exit Sub
        End If
        TvMain.ClearDuration()
        AppendInfo("Инициализация " & DateTime.Now.ToString)
        Application.DoEvents()
        StopReading = False
        cmdStop.Enabled = True
        If TvMain.DeviceInit(tag, cmbUsePort.Text) = False Then
            If Not StopReading Then

                Dim tError As String = ""
                Try
                    tError = TvMain.ConnectStatus
                Catch ex As Exception

                End Try
                AppendInfo("Ошибка инициализации транспортного уровня. " & tError & DateTime.Now.ToString)
                TvMain.SaveLog(tag, 0, "??", 1, "Ошибка инициализации транспортного уровня. " + tError)
                TvMain.SetTimeToPlanCall(tag, "dlock", DateTime.Now)
                Application.DoEvents()
                MsgBox(lblNodeName.Text + ". " & tError, MsgBoxStyle.OkOnly, "Ошибка инициализации транспортного уровня")

            End If

            ReadTimer.Enabled = False
            TvMain.UnLockDevice(tag)
            cmdStop.Enabled = False
            lblNodeName.Text = ""
            Return
        End If
        TvMain.SaveLog(tag, 0, "??", 1, "Инициализация соединения")
        AppendInfo("Подключение " & DateTime.Now.ToString)
        Application.DoEvents()

        TvMain.connect()
        If Not TvMain.isConnected() Or StopReading Then
            Dim tError As String = ""
            Try
                tError = TvMain.ConnectStatus
            Catch ex As Exception

            End Try
            TvMain.DeviceClose()
            If Not StopReading Then

                AppendInfo("Ошибка соединения с тепловычислителем. " & tError & DateTime.Now.ToString)
                TvMain.SaveLog(tag, 0, "??", 1, "Ошибка соединения с тепловычислителем. " & tError)
                TvMain.SetTimeToPlanCall(tag, "dlock", DateTime.Now)
                Application.DoEvents()
                MsgBox(lblNodeName.Text + ". " & tError, MsgBoxStyle.OkOnly, "Ошибка соединения с тепловычислителем")
            End If
            cmdStop.Enabled = False
            ReadTimer.Enabled = False
            TvMain.UnLockDevice(tag)
            lblNodeName.Text = ""
            Exit Sub
        End If
        TvMain.SaveLog(tag, 0, "??", 1, "Подключение к тепловычислителю")



        AppendInfo("Начало опроса " & DateTime.Now.ToString)
        Application.DoEvents()

read_again:


        ButtonReadArch.Enabled = False

        If chkSystem.Checked = True Then
            Dim dtsys As DataTable = Nothing
            If (TvMain.LockDevice(tag, 120, True)) Then
                TvMain.ClearDuration()

                dtsys = TvMain.ReadSystemParameters
                If Not dtsys Is Nothing Then
                    DataGridSystem.DataSource = Nothing
                    Application.DoEvents()
                    DataGridSystem.DataSource = dtsys
                    TvMain.SaveLog(tag, 0, "??", 1, "Читаем системные параметры")
                    TvMain.UnLockDevice(tag)
                Else
                    DataGridSystem.DataSource = Nothing
                    TvMain.SaveLog(tag, 0, "??", 1, "Ошибка чтения системных параметров")
                    TvMain.UnLockDevice(tag)
                End If
            End If

        End If

        If (chkByList.Checked = True) Then
            If lstDT.Rows.Count > 0 Then
                Dim iii As Integer
                Dim atype As String
                Dim adat As DateTime
                For iii = 0 To lstDT.Rows.Count - 1
                    If StopReading Then GoTo Stopping
                    adat = lstDT.Rows(iii)("atime")
                    atype = lstDT.Rows(iii)("atype")
                    AppendInfo("Читаем по списку." & lstDT.Rows(iii)("name"))

                    Try

                        If (TvMain.LockDevice(tag, 120, True)) Then
                            If atype = "Час." Then
                                If adat.Minute > 30 Then
                                    adat = adat.AddHours(1)
                                End If
                                TvMain.ClearDuration()


                                str = TvMain.readarch(archType_hour, adat.Year, adat.Month, adat.Day, adat.Hour)
                                TvMain.SaveLog(tag, archType_hour, "??", 1, "Читаем по списку." & lstDT.Rows(iii)("name"))

                                If str.Substring(0, 6) = "Ошибка" Then
                                    TvMain.SaveLog(tag, archType_hour, "??", 1, str)
                                    TvMain.UnLockDevice(tag)
                                    AppendInfo(str)
                                    Application.DoEvents()
                                    Continue For
                                End If

                                If TvMain.TVD.isArchToDBWrite Then
                                    Dim rewrite As Boolean = False
                                    Dim writeit As Boolean = False
                                    Dim d1 As DateTime
                                    rewrite = TvMain.CheckForArch(archType_hour, adat.Year, adat.Month, adat.Day, adat.Hour, tag)


                                    If rewrite And chkQuestionOnRewrite.Checked Then

                                        d1 = TvMain.GetRealDateFromBase(archType_hour, adat.Year, adat.Month, adat.Day, adat.Hour, tag)
                                        If MsgBox("Часовой архив на: " + d1.ToString + " уже есть в базе." & vbCrLf & "Перезаписать ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                            writeit = True
                                        Else
                                            writeit = False
                                        End If

                                    Else
                                        writeit = True
                                    End If
                                    If writeit Then

                                        TvMain.ClearDBArchString(archType_hour, adat.Year, adat.Month, adat.Day, adat.Hour, tag)

                                        TvMain.WriteArchToDB()
                                    End If

                                End If

                            End If


                            If atype = "Сут." Then
                                TvMain.ClearDuration()

                                str = TvMain.readarch(archType_day, adat.Year, adat.Month, adat.Day, 0)
                                TvMain.SaveLog(tag, archType_day, "??", 1, "Читаем по списку." & lstDT.Rows(iii)("name"))

                                If str.Substring(0, 6) = "Ошибка" Then

                                    TvMain.UnLockDevice(tag)
                                    AppendInfo(str)
                                    TvMain.SaveLog(tag, archType_day, "??", 1, str)
                                    Application.DoEvents()
                                    Continue For
                                End If
                                If TvMain.TVD.isArchToDBWrite Then
                                    Dim rewrite As Boolean = False
                                    Dim writeit As Boolean = False
                                    Dim d1 As DateTime

                                    rewrite = TvMain.CheckForArch(archType_day, adat.Year, adat.Month, adat.Day, 0, tag)

                                    If rewrite And chkQuestionOnRewrite.Checked Then
                                        d1 = TvMain.GetRealDateFromBase(archType_day, adat.Year, adat.Month, adat.Day, 0, tag)
                                        If MsgBox("Суточный архив на: " + d1.ToString() + " уже есть в базе." & vbCrLf & "Перезаписать ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                            writeit = True
                                        Else
                                            writeit = False
                                        End If
                                    Else
                                        writeit = True
                                    End If

                                    If writeit Then

                                        TvMain.ClearDBArchString(archType_day, adat.Year, adat.Month, adat.Day, 0, tag)

                                        TvMain.WriteArchToDB()
                                    End If

                                End If
                            End If



                            If StopReading Then GoTo Stopping
                        End If
                        Application.DoEvents()
                        TvMain.UnLockDevice(tag)
                    Catch ex As Exception
                        MsgBox(ex.Message)

                    End Try



                Next
            End If
        End If


        If (CheckBoxMoment.Checked = True) Then
            Try
                If StopReading Then GoTo Stopping
                If (TvMain.LockDevice(tag, 120, True)) Then
                    AppendInfo("Чтение мгновенного архива")
                    Application.DoEvents()

                    TvMain.ClearDuration()
                    TvMain.readmarch()
                    TvMain.SaveLog(tag, archType_moment, "??", 1, "Чтение мгновенного архива")

                    If (TvMain.TVD.isMArchToDBWrite) Then
                        AppendInfo("Сохранение мгновенного архива")
                        Application.DoEvents()
                        TvMain.WritemArchToDB()

                    End If
                    AppendInfo("Чтение мгновенного архива завершено")
                    Application.DoEvents()
                    TvMain.UnLockDevice(tag)

                    If StopReading Then GoTo Stopping
                End If
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try

        End If

        If StopReading Then GoTo Stopping

        If (chkTotal.Checked = True) Then
            Try

                If (TvMain.LockDevice(tag, 120, True)) Then

                    AppendInfo("Чтение итогового архива")
                    Application.DoEvents()
                    Dim sss As String


                    TvMain.ClearDuration()
                    sss = TvMain.readtarch()
                    TvMain.SaveLog(tag, archType_total, "??", 1, "Чтение итогового архива")

                    If TvMain.TVD.isTArchToDBWrite Then
                        AppendInfo("Сохранение итогового архива")
                        Application.DoEvents()
                        TvMain.WriteTArchtoDB()

                    End If
                    AppendInfo("Чтение итогового архива завершено")
                    Application.DoEvents()
                    TvMain.UnLockDevice(tag)

                    If StopReading Then GoTo Stopping
                End If

            Catch ex As Exception

                MsgBox(ex.Message)
            End Try


        End If

        If StopReading Then GoTo Stopping

        If (CheckBoxHour.Checked = True) Then


            Dim i As Double
            Dim razn As TimeSpan
            Dim tempdate As Date
            razn = DateTimePickerBefor.Value - DateTimePickerAfter.Value
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            i = razn.Hours + razn.Days * 24 + 1
            If i > 0 Then
                ProgressBar1.Maximum = i
            Else
                ProgressBar1.Maximum = 100
            End If

            ProgressBar1.Value = 1
            ProgressBar1.Step = 1
            'Dim transaction As OracleTransaction
            AppendInfo("Чтение часовых архивов")
            Dim errCnt As Integer
            Application.DoEvents()
            errCnt = 0
            For i = 0 To razn.Hours + razn.Days * 24

                If StopReading Then GoTo Stopping
                If errCnt > 3 Then GoTo Stopping
                tempdate = DateTimePickerAfter.Value
                tempdate = tempdate.AddHours(i)

                If StopReading Then GoTo Stopping
                AppendInfo("Чтение архива за " & tempdate.ToString)
                Application.DoEvents()
                Try




                    If (TvMain.LockDevice(tag, 120, True)) Then

                        TvMain.ClearDuration()


                        str = TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour)

                        If str.Substring(0, 6) = "Ошибка" Then
                            'transaction.Rollback()
                            TvMain.UnLockDevice(tag)
                            AppendInfo(str)
                            TvMain.SaveLog(tag, archType_hour, "??", 1, "Чтение архива за " & tempdate.ToString & " " & str)
                            Application.DoEvents()
                            errCnt += 1
                            Continue For
                        Else
                            TvMain.SaveLog(tag, archType_hour, "??", 1, "Чтение архива за " & tempdate.ToString)
                        End If

                        If TvMain.TVD.isArchToDBWrite Then
                            errCnt = 0
                            Dim rewrite As Boolean = False
                            Dim writeit As Boolean = False
                            Dim d1 As DateTime
                            rewrite = TvMain.CheckForArch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, tag)


                            If rewrite And chkQuestionOnRewrite.Checked Then

                                d1 = TvMain.GetRealDateFromBase(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, tag)
                                If MsgBox("Часовой архив на: " + d1.ToString + " уже есть в базе." & vbCrLf & "Перезаписать ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    writeit = True
                                Else
                                    writeit = False
                                End If

                            Else
                                writeit = True
                            End If
                            If writeit Then

                                TvMain.ClearDBArchString(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour, tag)

                                TvMain.WriteArchToDB()
                            End If

                        End If

                        TvMain.UnLockDevice(tag)

                        If StopReading Then GoTo Stopping

                    End If


                    ProgressBar1.PerformStep()



                Catch ex As Exception

                    AppendInfo(ex.Message())
                    Application.DoEvents()
                    'MsgBox(ex.Message)

                End Try

            Next

            If StopReading Then GoTo Stopping
            ProgressBar1.Visible = False
            AppendInfo("Чтение часовых архивов завершено")
            Application.DoEvents()
        End If
        If (CheckBoxDay.Checked = True) Then
            Dim i As Double
            Dim razn As TimeSpan
            Dim tempdate As Date
            razn = DateTimePickerBefor.Value - DateTimePickerAfter.Value
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            i = razn.Days + 1
            If i > 0 Then
                ProgressBar1.Maximum = i
            Else
                ProgressBar1.Maximum = 100
            End If


            ProgressBar1.Value = 1
            ProgressBar1.Step = 1
            AppendInfo("Чтение суточных архивов")
            Application.DoEvents()
            Dim ERRcNT As Integer
            ERRcNT = 0
            For i = 0 To razn.Days

                If ERRcNT Then GoTo Stopping
                If StopReading Then GoTo Stopping
                timesp = DateTime.Now - queryStartTime



                tempdate = DateTimePickerAfter.Value
                tempdate = tempdate.AddDays(i)

                AppendInfo("Чтение архива за " & tempdate.ToString)
                Application.DoEvents()
                Try

                    If (TvMain.LockDevice(tag, 120, True)) Then

                        TvMain.ClearDuration()
                        str = TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0)
                        If str.Substring(0, 6) = "Ошибка" Then
                            ERRcNT += 1
                            TvMain.UnLockDevice(tag)
                            AppendInfo(str)
                            TvMain.SaveLog(tag, archType_day, "??", 1, "Чтение архива за " & tempdate.ToString & " " & str)
                            Application.DoEvents()
                            Continue For
                        Else
                            TvMain.SaveLog(tag, archType_day, "??", 1, "Чтение архива за " & tempdate.ToString)
                        End If

                        If TvMain.TVD.isArchToDBWrite Then
                            ERRcNT = 0
                            Dim rewrite As Boolean = False
                            Dim writeit As Boolean = False
                            Dim d1 As DateTime

                            rewrite = TvMain.CheckForArch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, tag)

                            If rewrite And chkQuestionOnRewrite.Checked Then
                                d1 = TvMain.GetRealDateFromBase(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, tag)
                                If MsgBox("Суточный архив на: " + d1.ToString() + " уже есть в базе." & vbCrLf & "Перезаписать ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    writeit = True
                                Else
                                    writeit = False
                                End If
                            Else
                                writeit = True
                            End If

                            If writeit Then

                                TvMain.ClearDBArchString(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0, tag)

                                TvMain.WriteArchToDB()
                            End If

                        End If


                        TvMain.UnLockDevice(tag)

                        If StopReading Then GoTo Stopping
                    End If
                    'transaction.Commit()
                    ProgressBar1.PerformStep()

                Catch ex As Exception

                    AppendInfo(ex.Message())
                    Application.DoEvents()
                    MsgBox(ex.Message)
                End Try
            Next
            ProgressBar1.Visible = False
            AppendInfo("Чтение суточных архивов завершено")
            Application.DoEvents()

        End If

        If chkNoHangup.Checked Then
            Dim sleeper As Integer = 60
            If Integer.TryParse(txtRepeat.Text, sleeper) Then
                sleeper = sleeper * 60

            Else
                sleeper = 60
            End If

            Dim i_sleep As Integer
            For i_sleep = sleeper To 0 Step -1
                System.Threading.Thread.Sleep(1000)
                lblActiopn.Text = "ждем:" + i_sleep.ToString
                If StopReading Then
                    GoTo stopping
                End If
                Application.DoEvents()
            Next

            GoTo read_again
        End If

Stopping:
        If StopReading Then
            ProgressBar1.Visible = False
            AppendInfo("Чтение данных прервано")
            TvMain.ClearDuration()
            TvMain.SaveLog(tag, 0, "??", 1, "Чтение данных прервано")
            lblNodeName.Text = ""
            Application.DoEvents()
        End If

        lblRcv.BackColor = Color.LightGray
        lblSnd.BackColor = Color.LightGray
        Dim transpname As String = ""
        If Not TvMain.TVD Is Nothing Then
            If TvMain.TVD.Transport = 0 Then
                transpname = TvMain.TVD.ComPort
            End If

        End If


        'TVD.ComPort =
        TvMain.ClearDuration()
        TvMain.CloseTransportConnect()
        TvMain.DeviceClose()
        TvMain.SaveLog(tag, 0, "??", 1, "Закрытие канала. " & transpname)
        cmdStop.Enabled = False
        RefreshData(localtree)
        ButtonReadArch.Enabled = True
        ReadTimer.Enabled = False
        lblNodeName.Text = ""
    End Sub

    Private Sub AppendInfo(ByVal s As String)
        lblError.Text = s & vbCrLf & lblError.Text
    End Sub


    Private Sub ButtonExportMoment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportMoment.Click
        ExportGrid(DataGridMoment, "Мгновенные, " + lblMoment.Text)

     

    End Sub


    Private Sub ButtonExportHour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportHour.Click
        ExportGrid(DataGridHour, "Часовые архивы, " + lblHour.Text)
    End Sub

    Private Sub ButtonExportDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportDay.Click

        ExportGrid(DataGridDay, "Суточные архивы, " + lblDay.Text)

    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        RefreshData(localtree)
    End Sub

    Private Sub ExportGrid(ByVal gv As DataGridView, ByVal caption As String)
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then

            wb.GetLock()
            outworkbook = wb.ActiveWorkbook


            outworkbook.SaveAs(SaveFileDialog1.FileName, FileFormat.Excel8)

            While outworkbook.Worksheets.Count > 1
                outworkbook.Worksheets.Item(0).Delete()
            End While
            outworkbook.Worksheets.Item(0).Cells().Clear()
            Dim row As Integer

            Dim col As Integer
            Dim cell As IRange
            ws = outworkbook.Worksheets.Item(0)
            Dim hstyle As IStyle = outworkbook.Styles.Add("Header")
            Dim cstyle As IStyle = outworkbook.Styles.Add("colheader")
            hstyle.Font.Size = 15
            hstyle.Font.Bold = True

            cstyle.Font.Size = 12
            cstyle.Font.Bold = True
            cstyle.Font.Underline = UnderlineStyle.Single

            
            Dim border As SpreadsheetGear.IBorder



            

            For row = 0 To gv.Rows.Count - 1
                For col = 0 To gv.Columns.Count - 1
                    cell = ws.Cells(row + 2, col)
                    cell.Value = gv.Rows.Item(row).Cells.Item(col).Value
                    border = cell.Borders(SpreadsheetGear.BordersIndex.EdgeTop)
                    border.LineStyle = LineStyle.Dash
                    border.Weight = 1
                    border.Color = Color.Black
                    border = cell.Borders(SpreadsheetGear.BordersIndex.EdgeLeft)
                    border.LineStyle = LineStyle.Dash
                    border.Weight = 1
                    border.Color = Color.Black

                Next
            Next

            cell = ws.Cells(0, 0)
            cell.Value = caption
            cell.Style = hstyle

            For col = 0 To gv.Columns.Count - 1
                cell = ws.Cells(1, col)
                cell.Value = gv.Columns.Item(col).HeaderText
                cell.ColumnWidth = gv.Columns.Item(col).Width / 8
                cell.Style = cstyle
                border = cell.Borders(SpreadsheetGear.BordersIndex.EdgeTop)
                border.LineStyle = LineStyle.Dash
                border.Weight = 1
                border.Color = Color.Black
                border = cell.Borders(SpreadsheetGear.BordersIndex.EdgeLeft)
                border.LineStyle = LineStyle.Dash
                border.Weight = 1
                border.Color = Color.Black
            Next

            outworkbook.Save()
            wb.ReleaseLock()
            MsgBox("Файл сохранен")
        End If
    End Sub

    Private Sub ButtonExportTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportTotal.Click
        ExportGrid(DataGridTotal, "Итоговые, " + lblTotal.Text)

    End Sub

    Private Sub RefreshDatabyID(ByVal tag As Integer, ByVal GCaption As String)
        Dim dTo As Date
        Dim dfrom As Date
        dTo = Date.Now
        Dim edt As DataTable
        edt = New DataTable

        LastCaption = GCaption
        Select Case (TabControl1.SelectedTab.Text)
            Case "Мгновенный"
                If optMoment.CheckedItem.DataValue = 0 Then
                    dfrom = DateTimePickerAfter.Value
                    dTo = DateTimePickerBefor.Value
                Else
                    dfrom = dTo.AddDays(-optMoment.CheckedItem.DataValue)
                End If
                DataGridMoment.Enabled = False
                'DataGridMoment.DataSource = edt
                DataGridMoment.DataSource = GetTVData(tag, dfrom, dTo, archType_moment)
                Utils.SetupGridMS(DataGridMoment, tag, archType_moment)
                lblMoment.Text = GCaption
                DataGridMoment.Enabled = True
            Case "Часовой"

                If optHour.CheckedItem.DataValue = 0 Then
                    dfrom = DateTimePickerAfter.Value
                    dTo = DateTimePickerBefor.Value
                Else
                    dfrom = dTo.AddDays(-optHour.CheckedItem.DataValue)
                End If
                DataGridHour.DataSource = edt
                DataGridHour.DataSource = GetTVData(tag, dfrom, dTo, archType_hour)
                Utils.SetupGridMS(DataGridHour, tag, archType_hour)
                lblHour.Text = GCaption

            Case "Суточный"
                If optDay.CheckedItem.DataValue = 0 Then
                    dfrom = DateTimePickerAfter.Value
                    dTo = DateTimePickerBefor.Value
                Else
                    dfrom = dTo.AddDays(-optDay.CheckedItem.DataValue)
                End If
                DataGridDay.DataSource = edt
                DataGridDay.DataSource = GetTVData(tag, dfrom, dTo, archType_day)

                Utils.SetupGridMS(DataGridDay, tag, archType_day)
                lblDay.Text = GCaption
            Case "Итоговые"


                If optTotal.CheckedItem.DataValue = 0 Then
                    dfrom = DateTimePickerAfter.Value
                    dTo = DateTimePickerBefor.Value
                Else
                    dfrom = dTo.AddDays(-optTotal.CheckedItem.DataValue)
                End If
                DataGridTotal.DataSource = edt
                DataGridTotal.DataSource = GetTVData(tag, dfrom, dTo, archType_total)
                Utils.SetupGridMS(DataGridTotal, tag, archType_total)
                lblTotal.Text = GCaption
            Case "Системные параметры"
                If DataGridSystem.Tag <> tag Then
                    DataGridSystem.DataSource = Nothing
                    DataGridSystem.Tag = tag
                End If
                lblSystem.Text = GCaption

            Case "Пропущенные архивы"
                DataGridMissing.DataSource = GetmissingData(tag)
                DataGridMissing.Tag = tag
                DataGridMissing.Columns("NodeName").HeaderText = "Узел и тип архива"
                DataGridMissing.Columns(0).MinimumWidth = 300
                DataGridMissing.Columns(0).Width = 300
                DataGridMissing.Columns(0).DefaultCellStyle.Format = "G"
                DataGridMissing.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                lblMissing.Text = GCaption
        End Select
    End Sub

   

    Public Sub RefreshData(ByVal tree As Infragistics.Win.UltraWinTree.UltraTree)
        localtree = tree
        Dim tag As Object
        If tree Is Nothing Then Exit Sub

        If Not bActivated Then Exit Sub
        If tree.SelectedNodes.Count > 0 Then
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = tree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        Dim cfg As STKTVMain.TVMain.ConfigStruct

        cfg = TvMain.GetConfigStructFromId_BD(tag)

        RefreshDatabyID(tag, tree.SelectedNodes.Item(0).Text + " - " + cfg.device + " (" + cfg.Transport + ")")
    End Sub

    Private Sub DateTimePickerAfter_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerAfter.ValueChanged
        'RefreshData()
    End Sub

    Private Sub DateTimePickerBefor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerBefor.ValueChanged
        'RefreshData()
    End Sub







    Private Sub SetupMoment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetupMoment.Click



        Dim tag As Object
        If localtree Is Nothing Then Exit Sub
        If localtree.SelectedNodes.Count > 0 Then
            If localtree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = localtree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridMoment
        f.id_bd = tag
        f.ptype = archType_moment
        f.LoadData()

        If f.ShowDialog = DialogResult.OK Then
            RefreshData(localtree)
        End If
    End Sub



    Private Sub cmdHourSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHourSetup.Click

        Dim tag As Object
        If localtree Is Nothing Then Exit Sub
        If localtree.SelectedNodes.Count > 0 Then
            If localtree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = localtree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridHour
        f.id_bd = tag
        f.ptype = archType_hour
        f.LoadData()
        If f.ShowDialog = DialogResult.OK Then
            RefreshData(localtree)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDaySetup.Click
        'DataGridDay.ShowColumnChooser()

        'Dim lpath As String
        'Try
        '    lpath = GetSetting("MTZ", "CONFIG", "LAYOUTS", My.Application.Info.DirectoryPath & "\Layouts\")

        '    DataGridDay.DisplayLayout.Save(lpath & "\" & "Day" & ".ugl")
        'Catch
        'End Try
        Dim tag As Object
        If localtree Is Nothing Then Exit Sub
        If localtree.SelectedNodes.Count > 0 Then
            If localtree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = localtree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridDay
        f.id_bd = tag
        f.ptype = archType_day
        f.LoadData()
        If f.ShowDialog = DialogResult.OK Then
            RefreshData(localtree)
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTotalSetup.Click

        Dim tag As Object
        If localtree Is Nothing Then Exit Sub
        If localtree.SelectedNodes.Count > 0 Then
            If localtree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = localtree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridTotal
        f.id_bd = tag
        f.ptype = archType_total
        f.LoadData()
        If f.ShowDialog = DialogResult.OK Then
            RefreshData(localtree)
        End If
    End Sub

    Private Sub CheckBoxHour_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxHour.CheckedChanged

    End Sub


    'Private mTVCmd As OracleCommand = Nothing

    Private Function getTVCmd(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + "  from datacurr where id_bd=:id_bd and  id_ptype=:id_ptype and dcounter >=:dfrom and dcounter<=:dto order by dcounter desc  "

        cmd = New OracleCommand
        cmd.CommandText = ctext
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Int16)
        cmd.Parameters.Add("id_ptype", OracleDbType.Int16)
        cmd.Parameters.Add("dfrom", OracleDbType.Date)
        cmd.Parameters.Add("dto", OracleDbType.Date)
        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()
        Return cmd


    End Function

    Private Function getTVCmdLinked(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + "  from v_linked_hour where id_bd=:id_bd and  id_ptype=:id_ptype and dcounter >=:dfrom and dcounter<=:dto order by dcounter desc  "

        cmd = New OracleCommand
        cmd.CommandText = ctext
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Int16)
        cmd.Parameters.Add("id_ptype", OracleDbType.Int16)
        cmd.Parameters.Add("dfrom", OracleDbType.Date)
        cmd.Parameters.Add("dto", OracleDbType.Date)
        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()
        Return cmd


    End Function


    Private Function getTVCmdLinkedD(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + "  from v_linked_day where id_bd=:id_bd and  id_ptype=:id_ptype and dcounter >=:dfrom and dcounter<=:dto order by dcounter desc  "

        cmd = New OracleCommand
        cmd.CommandText = ctext
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Decimal)
        cmd.Parameters.Add("id_ptype", OracleDbType.Decimal)
        cmd.Parameters.Add("dfrom", OracleDbType.Date)
        cmd.Parameters.Add("dto", OracleDbType.Date)
        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()
        Return cmd


    End Function



    Private Function getTVCmdM(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + "  from V_LINKED_MOMENT where id_bd=:id_bd and  id_ptype=:id_ptype and dcall >=:dfrom and (dcall-1/24/6)<=:dto order by dcall desc   "

        cmd = New OracleCommand
        cmd.CommandText = ctext
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Int16)
        cmd.Parameters.Add("id_ptype", OracleDbType.Int16)
        cmd.Parameters.Add("dfrom", OracleDbType.Date)
        cmd.Parameters.Add("dto", OracleDbType.Date)
        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()
        Return cmd


    End Function


    Private Function getElectroCmd(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + "  from electro where id_bd=:id_bd and  id_ptype=:id_ptype and dcounter >=:dfrom and dcounter<=:dto order by dcounter desc  "

        cmd = New OracleCommand
        cmd.CommandText = ctext
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Int16)
        cmd.Parameters.Add("id_ptype", OracleDbType.Int16)
        cmd.Parameters.Add("dfrom", OracleDbType.Date)
        cmd.Parameters.Add("dto", OracleDbType.Date)
        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()
        Return cmd


    End Function

    Private Function getElectroCmdM(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + "  from electro where id_bd=:id_bd and  id_ptype=:id_ptype and dcall >=:dfrom and (dcall-1/24/6)<=:dto order by dcall desc   "

        cmd = New OracleCommand
        cmd.CommandText = ctext
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Int16)
        cmd.Parameters.Add("id_ptype", OracleDbType.Int16)
        cmd.Parameters.Add("dfrom", OracleDbType.Date)
        cmd.Parameters.Add("dto", OracleDbType.Date)
        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()
        Return cmd


    End Function



    Private Function getMissingCmd() As OracleCommand
        Dim cmd As OracleCommand

        cmd = New OracleCommand
        cmd.CommandText = "select DEVNAME as NodeName, ARCHDATE as ""Дата"" from missingarch where missingarch.ARCHDATE >= sysdate-40 and id_bd=:id_bd"
        cmd.CommandType = CommandType.Text
        cmd.Parameters.Add("id_bd", OracleDbType.Int16)

        cmd.Connection = TvMain.dbconnect()
        cmd.Prepare()

        Return cmd
    End Function

    Private Function GetTVData(ByVal id_bd As Integer, ByVal dfrom As Date, ByVal dto As Date, ByVal id_ptype As Integer) As DataTable
        Dim cmd As OracleCommand
        Select id_ptype
            Case 1
            cmd = getTVCmdM(Utils.ArchFieldList(id_bd, id_ptype))
            Case 3
                cmd = getTVCmdLinked(Utils.ArchFieldList(id_bd, id_ptype))
            Case 4
                cmd = getTVCmdLinkedD(Utils.ArchFieldList(id_bd, id_ptype))
            Case Else
            cmd = getTVCmd(Utils.ArchFieldList(id_bd, id_ptype))
        End Select
        cmd.Parameters.Item("id_bd").Value = id_bd
        cmd.Parameters.Item("id_ptype").Value = id_ptype
        cmd.Parameters.Item("dfrom").Value = dfrom
        cmd.Parameters.Item("dto").Value = dto
        cmd.CommandTimeout = 1000

        Dim da As OracleDataAdapter
        Dim dt As DataTable
        da = New OracleDataAdapter

        da.SelectCommand = cmd
        dt = New DataTable
        Try
            ' SyncLock cmd.Connection
            da.Fill(dt)
            'End SyncLock
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try
        da.Dispose()
        cmd.Dispose()

        Return dt
    End Function

    Private Function GetElectroData(ByVal id_bd As Integer, ByVal dfrom As Date, ByVal dto As Date, ByVal id_ptype As Integer) As DataTable
        Dim cmd As OracleCommand
        If id_ptype = 1 Then
            cmd = getElectroCmdM(Utils.ArchFieldList(id_bd, id_ptype))
        Else
            cmd = getElectroCmd(Utils.ArchFieldList(id_bd, id_ptype))
        End If

        cmd.Parameters.Item("id_bd").Value = id_bd
        cmd.Parameters.Item("id_ptype").Value = id_ptype
        cmd.Parameters.Item("dfrom").Value = dfrom
        cmd.Parameters.Item("dto").Value = dto

        Dim da As OracleDataAdapter
        Dim dt As DataTable
        da = New OracleDataAdapter

        da.SelectCommand = cmd
        dt = New DataTable
        Try
            SyncLock cmd.Connection
                da.Fill(dt)
            End SyncLock
        Catch
        End Try


        Return dt
    End Function





    Private Function GetmissingData(ByVal id_bd As Integer) As DataTable
        Dim cmd As OracleCommand
        cmd = getMissingCmd()
        cmd.Parameters.Item("id_bd").Value = id_bd


        Dim da As OracleDataAdapter
        Dim dt As DataTable
        da = New OracleDataAdapter

        da.SelectCommand = cmd
        dt = New DataTable
        Try
            SyncLock cmd.Connection
                da.Fill(dt)
            End SyncLock
        Catch
        End Try
        da.Dispose()
        cmd.Dispose()
        Return dt
    End Function

    Private Sub ButtonExportMissing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportMissing.Click
        'If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
        '    expExcel.Export(DataGridMissing, SaveFileDialog1.FileName)
        'End If
    End Sub

    Private Sub ButtonPrintMissing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPrintMissing.Click
        DataGridMissing.Text = "Пропущенные архивы"
        'DataGridMissing.PrintPreview()
    End Sub

    Private Sub ButtonSetupMissing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSetupMissing.Click
        'DataGridMissing.ShowColumnChooser()

        Dim lpath As String
        Try
            lpath = GetSetting("MTZ", "CONFIG", "LAYOUTS", My.Application.Info.DirectoryPath & "\Layouts\")

            'DataGridMissing.DisplayLayout.Save(lpath & "\" & "Missing" & ".ugl")
        Catch
        End Try
    End Sub


    Private Sub optMoment_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMoment.ValueChanged
        RefreshData(localtree)
    End Sub

    Private Sub optHour_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optHour.ValueChanged
        RefreshData(localtree)
    End Sub

    Private Sub optDay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDay.ValueChanged
        RefreshData(localtree)
    End Sub

    Private Sub optTotal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTotal.ValueChanged
        RefreshData(localtree)
    End Sub

    Private Sub ClientForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bActivated = True
        ComTimer.Enabled = True
        RefreshData(localtree)

    End Sub

    Private Sub ClientForm_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        bActivated = False
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdRefreshLost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshLost.Click
        Dim an As NodeAnalizer = New NodeAnalizer
       
        an.AnalizeNode(TvMain, DataGridMissing.Tag, 31, True)
        an = Nothing
        RefreshDatabyID(DataGridMissing.Tag, DataGridMissing.Text)
    End Sub


  

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        If TvMain.TVD.IsConnected Then
            StopReading = True
            cmdStop.Enabled = False
            MsgBox("Опрос будет  остановлен." & vbCrLf & "Дождитесь окончания текущей операции.", MsgBoxStyle.Information, "Оcтановка опроса")
        Else
            If Not TvMain.TVD.DriverTransport Is Nothing Then
                StopReading = True
                TvMain.TVD.DriverTransport.CancelNow()
            End If

        End If

    End Sub

 




    Private Sub DataGridMissing_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridMissing.CellMouseDoubleClick
        Dim row As Integer
        row = e.RowIndex

        Dim dat As DateTime
        Dim name As String
        name = DataGridMissing.Rows(row).Cells(0).Value
        dat = DataGridMissing.Rows(row).Cells(1).Value

        If InStr(name, "Часовой") > 0 Then
            DateTimePickerAfter.Value = dat.AddHours(-1)
            DateTimePickerBefor.Value = dat.AddHours(1)
            CheckBoxHour.Checked = True
            CheckBoxMoment.Checked = False
            CheckBoxDay.Checked = False
            chkTotal.Checked = False

            If lstDT Is Nothing Then BuildLstDT()

            'Dim dt As DataTable
            Dim dr As DataRow
            dr = lstDT.NewRow
            dr("Atype") = "Час."
            dr("Atime") = dat
            dr("Name") = "Час." + dat.ToString

            lstDT.Rows.Add(dr)
            cmdClearLst.Enabled = True

        End If

        If InStr(name, "Суточный") > 0 Then
            DateTimePickerAfter.Value = dat.AddDays(-1)
            DateTimePickerBefor.Value = dat.AddDays(1)
            CheckBoxHour.Checked = False
            CheckBoxMoment.Checked = False
            CheckBoxDay.Checked = True
            chkTotal.Checked = False

            If lstDT Is Nothing Then BuildLstDT()

            'Dim dt As DataTable
            Dim dr As DataRow

            dr = lstDT.NewRow
            dr("Atype") = "Сут."
            dr("Atime") = dat
            dr("Name") = "Сут." + dat.ToString

            lstDT.Rows.Add(dr)
            cmdClearLst.Enabled = True
        End If
    End Sub

    Private Sub ClientForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MyfrmManual = Nothing
        ComTimer.Enabled = False
    End Sub

    Private Sub cmdRefreshMoment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshMoment.Click
        RefreshData(localtree)
    End Sub

    Private Sub cmdRefreshTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshTotal.Click
        RefreshData(localtree)
    End Sub

    Private Sub cmdRefreshDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshDay.Click
        RefreshData(localtree)
    End Sub

    Private Sub cmdRefreshHour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshHour.Click
        RefreshData(localtree)
    End Sub



    Private Sub cmdClearLst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearLst.Click

        BuildLstDT()
        cmdClearLst.Enabled = False
    End Sub

    Dim lstDT As DataTable
    Private Sub BuildLstDT()
        lstDT = New DataTable
        lstDT.Columns.Add("AType", GetType(String))
        lstDT.Columns.Add("ATime", GetType(DateTime))
        lstDT.Columns.Add("Name", GetType(String))
        lstReads.DisplayMember = "Name"
        lstReads.DataSource = lstDT
    End Sub
    Private Sub DataGridHour_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridHour.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        If lstDT Is Nothing Then BuildLstDT()
        Dim row As Integer
        row = e.RowIndex
        Dim dat As DateTime
        Dim dt As DataTable
        Dim dr As DataRow
        dt = DataGridHour.DataSource
        dat = dt.Rows(e.RowIndex)("DCOUNTER")
        dr = lstDT.NewRow
        dr("Atype") = "Час."
        dr("Atime") = dat
        dr("Name") = "Час." + dat.ToString

        lstDT.Rows.Add(dr)
        cmdClearLst.Enabled = True

    End Sub

    'Private Sub DataGridDay_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridDay.CellDoubleClick
    '    If lstDT Is Nothing Then BuildLstDT()
    '    Dim row As Integer
    '    row = e.RowIndex
    '    Dim dat As DateTime
    '    Dim dt As DataTable
    '    Dim dr As DataRow
    '    dt = DataGridDay.DataSource
    '    dat = dt.Rows(e.RowIndex)("DCOUNTER")
    '    dr = lstDT.NewRow
    '    dr("Atype") = "Сут."
    '    dr("Atime") = dat
    '    dr("Name") = "Сут." + dat.ToString
    '    lstDT.Rows.Add(dr)
    'End Sub

    Private Sub chkByList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkByList.CheckedChanged
        If chkByList.Checked Then
            lstReads.Enabled = True
        Else
            lstReads.Enabled = False
        End If
    End Sub

    Private Sub DataGridDay_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridDay.CellMouseDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        If lstDT Is Nothing Then BuildLstDT()
        Dim row As Integer
        row = e.RowIndex
        Dim dat As DateTime
        Dim dt As DataTable
        Dim dr As DataRow
        dt = DataGridDay.DataSource
        dat = dt.Rows(e.RowIndex)("DCOUNTER")
        dr = lstDT.NewRow
        dr("Atype") = "Сут."
        dr("Atime") = dat
        dr("Name") = "Сут." + dat.ToString
        lstDT.Rows.Add(dr)
        cmdClearLst.Enabled = True
    End Sub


    Private Sub ClientForm_Invalidated(ByVal sender As Object, ByVal e As System.Windows.Forms.InvalidateEventArgs) Handles Me.Invalidated

        If GetSetting("vodopadip", "setting", "checknulls", "1") = "1" Then
            chkCheckNull.Checked = True
        Else
            chkCheckNull.Checked = False
        End If

    End Sub

    Private Sub chkCheckNull_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckNull.CheckedChanged
        If chkCheckNull.Checked = True Then

            SaveSetting("vodopadip", "setting", "checknulls", "1")
        Else
            SaveSetting("vodopadip", "setting", "checknulls", "0")
        End If
    End Sub

    Private Sub ComTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComTimer.Tick
        lstFreeCOM.DisplayMember = "COMPORT"
        lstFreeCOM.DataSource = TvMain.GetFreeModems()
    End Sub

    Private Sub chkNoHangup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoHangup.CheckedChanged
        If chkNoHangup.Checked Then
            lblRepeat1.Enabled = True
            lblRepeat2.Enabled = True
            txtRepeat.Enabled = True
        Else
            lblRepeat1.Enabled = False
            lblRepeat2.Enabled = False
            txtRepeat.Enabled = False

        End If
    End Sub

    Private Sub cmdRefreshStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshStatus.Click
        Dim an As NodeAnalizer = New NodeAnalizer
        an.AnalizeNode(TvMain, DataGridMissing.Tag, 1, True)
        an = Nothing
    End Sub

    Private Sub DataGridMissing_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridMissing.CellContentClick

    End Sub

    Private Sub cmdAllToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAllToList.Click
        Dim row As Integer
        CheckBoxHour.Checked = False
        CheckBoxMoment.Checked = False
        CheckBoxDay.Checked = False
        chkTotal.Checked = False
        chkByList.Checked = True
        For row = 0 To DataGridMissing.Rows.Count - 1

            Dim dat As DateTime
            Dim name As String
            name = DataGridMissing.Rows(row).Cells(0).Value
            dat = DataGridMissing.Rows(row).Cells(1).Value

            If InStr(name, "Часовой") > 0 Then


                If lstDT Is Nothing Then BuildLstDT()

                Dim dr As DataRow
                dr = lstDT.NewRow
                dr("Atype") = "Час."
                dr("Atime") = dat
                dr("Name") = "Час." + dat.ToString
                lstDT.Rows.Add(dr)
                cmdClearLst.Enabled = True
            End If

            If InStr(name, "Суточный") > 0 Then

                If lstDT Is Nothing Then BuildLstDT()

                Dim dr As DataRow

                dr = lstDT.NewRow
                dr("Atype") = "Сут."
                dr("Atime") = dat
                dr("Name") = "Сут." + dat.ToString

                lstDT.Rows.Add(dr)
                cmdClearLst.Enabled = True
            End If
        Next

    End Sub

    Private Sub cmdMinpanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMinpanel.Click
        If Panel2.Height > cmdMinpanel.Top + cmdMinpanel.Height + 3 Then
            Panel2.Height = cmdMinpanel.Top + cmdMinpanel.Height + 3
            cmdMinpanel.Text = "развернуть (+)"
            SaveSetting("VIP", "View", "DialerPosition", "collapsed")
        Else
            Panel2.Height = 167
            cmdMinpanel.Text = "свернуть (-)"
            SaveSetting("VIP", "View", "DialerPosition", "expanded")
        End If
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub ClientForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If GetSetting("VIP", "View", "DialerPosition", "expanded") = "collapsed" Then
            cmdMinpanel_Click(sender, e)
        End If
        Dim query As String
        Dim MyName As String
        MyName = Environment.MachineName

        query = "select COMPORT from ipaddr join comports on ipaddr.id_ip=comports.id_ip  where  TERMINAL='" & MyName & "'" + " order by COMPORT  "
        Dim dt As DataTable
        dt = TvMain.QuerySelect(query)
        Dim dr As DataRow
        dr = dt.NewRow
        dr("COMPORT") = "(Любой)"
        dt.Rows.Add(dr)
        cmbUsePort.DisplayMember = "comport"
        cmbUsePort.ValueMember = "comport"
        cmbUsePort.DataSource = dt
        cmbUsePort.Text = "(Любой)"

    End Sub

    Private Sub DataGridHour_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridHour.CellContentClick

    End Sub

    Private Sub btnSystemExport_Click(sender As System.Object, e As System.EventArgs) Handles btnSystemExport.Click
        ExportGrid(DataGridSystem, "Системные, " + lblSystem.Text)
    End Sub

    Private Sub cmdChek24_Click(sender As System.Object, e As System.EventArgs) Handles cmdChek24.Click
        Dim tag As Object
        If localtree Is Nothing Then Exit Sub

        If Not bActivated Then Exit Sub
        If localtree.SelectedNodes.Count > 0 Then
            If localtree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = localtree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        Dim an As LostAnalizer
        an = New LostAnalizer()
        an.tvmain = TvMain
      
        an.NodeName = lblDay.Text
        an.CheckD24H(tag, 31)
        RefreshData(localtree)
    End Sub
End Class
