
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinTree
Imports Oracle.DataAccess.Client

Public Class ClientForm
    Inherits System.Windows.Forms.Form
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

    Private StopReading As Boolean
    Public id As Integer
    Public NodeName As String


    Dim timesp As System.TimeSpan

    Private Sub ReadTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReadTimer.Tick
        timesp = New System.TimeSpan
        timesp = DateTime.Now - queryStartTime
        labelTime.Text = Format(timesp.Minutes, "00") + ":" + Format(timesp.Seconds, "00")
        Select Case LastAction
            Case STKTVMain.UnitransportAction.Connected
                lblActiopn.Text = "Соединение установлено"
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Green
            Case STKTVMain.UnitransportAction.Connecting
                lblActiopn.Text = "Соединяемся"
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.Destroy
                lblActiopn.Text = "Транспорт удален"
            Case STKTVMain.UnitransportAction.Disconnected
                lblActiopn.Text = "Отключен"
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.Disconnecting
                lblActiopn.Text = "Отключение"
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.ReceiveData
                lblActiopn.Text = "Получены данные"
                lblRcv.BackColor = Color.Red
                lblSnd.BackColor = Color.Green
            Case STKTVMain.UnitransportAction.SendData
                lblActiopn.Text = "Отосланы данные"
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Red
            Case STKTVMain.UnitransportAction.SettingUp
                lblActiopn.Text = "Настроен"
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
            Case STKTVMain.UnitransportAction.Wait
                lblActiopn.Text = "Ожидание"
                lblRcv.BackColor = Color.Green
                lblSnd.BackColor = Color.Green
            Case STKTVMain.UnitransportAction.LowLevelStop
                lblActiopn.Text = "Потеря связи." + LastMSG
                lblError.Text = lblError.Text & vbCrLf & "Потеря связи." + LastMSG + " " + DateTime.Now.ToString
                lblRcv.BackColor = Color.LightGray
                lblSnd.BackColor = Color.LightGray
        End Select
        Application.DoEvents()

    End Sub

    Private Sub ButtonReadArch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReadArch.Click

        Application.DoEvents()



        If chkByList.Checked = False And CheckBoxMoment.Checked = False And CheckBoxHour.Checked = False And CheckBoxDay.Checked = False And chkTotal.Checked = False Then
            Return
        End If



        Dim tag As Object

        tag = id
        queryStartTime = DateTime.Now
        ReadTimer.Enabled = True

        lblError.Text = ""

        Dim query As String
        Dim qdt As DataTable
        Dim str As String


        query = "select npquery from bdevices  where npquery=1 and  bdevices.id_bd=" + tag.ToString() + " "
        qdt = TvMain.QuerySelect(query)
        If qdt.Rows.Count = 0 Then
            lblError.Text = lblError.Text & vbCrLf & "Для прибора не включен режим опроса по новому алгоритму. " & DateTime.Now.ToString
            MsgBox("Для прибора не включен режим опроса по новому алгоритму.")
            ReadTimer.Enabled = False
            Exit Sub
        End If

        query = "select npquery from bdevices  where  ( nplock is null or nplock <sysdate ) and   transport in (0,2,3)   and bdevices.id_bd=" + tag.ToString() + " "
        qdt = TvMain.QuerySelect(query)
        If qdt.Rows.Count = 0 Then
            lblError.Text = lblError.Text & vbCrLf & "Тепловычислитель занят, идет опрос" & DateTime.Now.ToString
            Application.DoEvents()
            MsgBox("Тепловычислитель занят, идет опрос")
            ReadTimer.Enabled = False
            Exit Sub
        End If
        TvMain.ClearDuration()
        If Not TvMain.LockDevice(tag, 120, False) Then
            MsgBox("Тепловычислитель занят")
            ReadTimer.Enabled = False
            Exit Sub
        End If



        lblError.Text = lblError.Text & vbCrLf & "Инициализация " & DateTime.Now.ToString
        Application.DoEvents()
        If TvMain.DeviceInit(tag) = False Then
            'TvMain.WriteErrToDB(tag, DateTime.Now, "Ошибка инициализации транспортного уровня")
            lblError.Text = lblError.Text & vbCrLf & "Ошибка инициализации транспортного уровня" & DateTime.Now.ToString
            TvMain.SaveLog(tag, 0, "??", 1, "Ошибка инициализации транспортного уровня")
            Application.DoEvents()
            MsgBox("Ошибка инициализации транспортного уровня")
            ReadTimer.Enabled = False
            Return
        End If
        TvMain.SaveLog(tag, 0, "??", 1, "Инициализация соединения")
        lblError.Text = lblError.Text & vbCrLf & "Подключение " & DateTime.Now.ToString
        Application.DoEvents()

        TvMain.connect()
        If Not TvMain.isConnected() Then
            TvMain.DeviceClose()
            'TvMain.WriteErrToDB(tag, DateTime.Now, "Ошибка соединения с тепловычислителем")
            lblError.Text = lblError.Text & vbCrLf & "Ошибка соединения с тепловычислителем " & DateTime.Now.ToString
            TvMain.SaveLog(tag, 0, "??", 1, "Ошибка соединения с тепловычислителем")
            Application.DoEvents()
            MsgBox("Ошибка соединения с тепловычислителем")
            ReadTimer.Enabled = False
            Return
        End If
        TvMain.SaveLog(tag, 0, "??", 1, "Подключение к тепловычислителю")

        lblError.Text = lblError.Text & vbCrLf & "Начало опроса " & DateTime.Now.ToString
        Application.DoEvents()


        StopReading = False
        cmdStop.Enabled = True
        ButtonReadArch.Enabled = False


        If (chkByList.Checked = True) Then
            If lstDT.Rows.Count > 0 Then
                Dim iii As Integer
                Dim atype As String
                Dim adat As DateTime
                For iii = 0 To lstDT.Rows.Count - 1

                    adat = lstDT.Rows(iii)("atime")
                    atype = lstDT.Rows(iii)("atype")
                    lblError.Text = lblError.Text & vbCrLf & "Читаем по списку." & lstDT.Rows(iii)("name")

                    Try

                        If (TvMain.LockDevice(tag, 60, True)) Then
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
                                    lblError.Text = lblError.Text & vbCrLf & str
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
                                    lblError.Text = lblError.Text & vbCrLf & str
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

                If (TvMain.LockDevice(tag, 60, True)) Then
                    lblError.Text = lblError.Text & vbCrLf & "Чтение мгновенного архива"
                    Application.DoEvents()

                    TvMain.ClearDuration()
                    TvMain.readmarch()
                    TvMain.SaveLog(tag, archType_moment, "??", 1, "Чтение мгновенного архива")

                    If (TvMain.TVD.isMArchToDBWrite) Then
                        lblError.Text = lblError.Text & vbCrLf & "Сохранение мгновенного архива"
                        Application.DoEvents()
                        TvMain.WritemArchToDB()

                    End If
                    lblError.Text = lblError.Text & vbCrLf & "Чтение мгновенного архива завершено"
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

                If (TvMain.LockDevice(tag, 60, True)) Then

                    lblError.Text = lblError.Text & vbCrLf & "Чтение итогового архива"
                    Application.DoEvents()
                    Dim sss As String


                    TvMain.ClearDuration()
                    sss = TvMain.readtarch()
                    TvMain.SaveLog(tag, archType_total, "??", 1, "Чтение итогового архива")

                    If TvMain.TVD.isTArchToDBWrite Then
                        lblError.Text = lblError.Text & vbCrLf & "Сохранение итогового архива"
                        Application.DoEvents()
                        TvMain.WriteTArchToDB()

                    End If
                    lblError.Text = lblError.Text & vbCrLf & "Чтение итогового архива завершено"
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
            ProgressBar1.Maximum = razn.Hours + razn.Days * 24 + 1
            ProgressBar1.Value = 1
            ProgressBar1.Step = 1
            'Dim transaction As OracleTransaction
            lblError.Text = lblError.Text & vbCrLf & "Чтение часовых архивов"
            Application.DoEvents()
            For i = 0 To razn.Hours + razn.Days * 24


                tempdate = DateTimePickerAfter.Value
                tempdate = tempdate.AddHours(i)


                lblError.Text = lblError.Text & vbCrLf & "Чтение архива за " & tempdate.ToString
                Application.DoEvents()
                Try




                    If (TvMain.LockDevice(tag, 60, True)) Then

                        TvMain.ClearDuration()


                        str = TvMain.readarch(archType_hour, tempdate.Year, tempdate.Month, tempdate.Day, tempdate.Hour)

                        If str.Substring(0, 6) = "Ошибка" Then
                            'transaction.Rollback()
                            TvMain.UnLockDevice(tag)
                            lblError.Text = lblError.Text & vbCrLf & str
                            TvMain.SaveLog(tag, archType_hour, "??", 1, "Чтение архива за " & tempdate.ToString & " " & str)
                            Application.DoEvents()
                            Continue For
                        Else
                            TvMain.SaveLog(tag, archType_hour, "??", 1, "Чтение архива за " & tempdate.ToString)
                        End If

                        If TvMain.TVD.isArchToDBWrite Then
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

                    lblError.Text = lblError.Text & vbCrLf & ex.Message
                    Application.DoEvents()
                    MsgBox(ex.Message)

                End Try

            Next

            If StopReading Then GoTo Stopping
            ProgressBar1.Visible = False
            lblError.Text = lblError.Text & vbCrLf & "Чтение часовых архивов завершено"
            Application.DoEvents()
        End If
        If (CheckBoxDay.Checked = True) Then
            Dim i As Double
            Dim razn As TimeSpan
            Dim tempdate As Date
            razn = DateTimePickerBefor.Value - DateTimePickerAfter.Value
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = razn.Days + 1
            ProgressBar1.Value = 1
            ProgressBar1.Step = 1
            lblError.Text = lblError.Text & vbCrLf & "Чтение суточных архивов"
            Application.DoEvents()
            For i = 0 To razn.Days

                timesp = DateTime.Now - queryStartTime



                tempdate = DateTimePickerAfter.Value
                tempdate = tempdate.AddDays(i)

                lblError.Text = lblError.Text & vbCrLf & "Чтение архива за " & tempdate.ToString
                Application.DoEvents()
                Try

                    If (TvMain.LockDevice(tag, 60, True)) Then

                        TvMain.ClearDuration()
                        str = TvMain.readarch(archType_day, tempdate.Year, tempdate.Month, tempdate.Day, 0)
                        If str.Substring(0, 6) = "Ошибка" Then

                            TvMain.UnLockDevice(tag)
                            lblError.Text = lblError.Text & vbCrLf & str
                            TvMain.SaveLog(tag, archType_day, "??", 1, "Чтение архива за " & tempdate.ToString & " " & str)
                            Application.DoEvents()
                            Continue For
                        Else
                            TvMain.SaveLog(tag, archType_day, "??", 1, "Чтение архива за " & tempdate.ToString)
                        End If
                        If TvMain.TVD.isArchToDBWrite Then
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

                    lblError.Text = lblError.Text & vbCrLf & ex.Message
                    Application.DoEvents()
                    MsgBox(ex.Message)
                End Try
            Next
            ProgressBar1.Visible = False
            lblError.Text = lblError.Text & vbCrLf & "Чтение суточных архивов завершено"
            Application.DoEvents()

        End If
Stopping:
        If StopReading Then
            ProgressBar1.Visible = False
            lblError.Text = lblError.Text & vbCrLf & "Чтение данных прервано"
            TvMain.ClearDuration()
            TvMain.SaveLog(tag, 0, "??", 1, "Чтение данных прервано")
            Application.DoEvents()
        End If

        TvMain.ClearDuration()
        TvMain.CloseTransportConnect()
        TvMain.DeviceClose()
        TvMain.SaveLog(tag, 0, "??", 1, "Закрытие канала")
        lblRcv.BackColor = Color.LightGray
        lblSnd.BackColor = Color.LightGray
        cmdStop.Enabled = False
        RefreshData()
        ButtonReadArch.Enabled = True
        ReadTimer.Enabled = False
    End Sub

    Private Sub ButtonExportMoment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportMoment.Click
        'If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    expExcel.Export(DataGridMoment, SaveFileDialog1.FileName)
        'End If

    End Sub


    Private Sub ButtonExportHour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportHour.Click
        'If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    expExcel.Export(DataGridHour, SaveFileDialog1.FileName)
        'End If

    End Sub

    Private Sub ButtonExportDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportDay.Click

        'If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    expExcel.Export(DataGridDay, SaveFileDialog1.FileName)
        'End If
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        RefreshData()
    End Sub




    Private Sub ButtonExportTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportTotal.Click
        'If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    expExcel.Export(DataGridTotal, SaveFileDialog1.FileName)
        'End If

    End Sub

    Private Sub RefreshDatabyID(ByVal tag As Integer, ByVal GCaption As String)
        Dim dTo As Date
        Dim dfrom As Date
        dTo = Date.Now
        Dim edt As DataTable
        edt = New DataTable


        Select Case (TabControl1.SelectedTab.Text)
            Case "Мгновенный"
                If optMoment.CheckedItem.DataValue = 0 Then
                    dfrom = DateTimePickerAfter.Value
                    dTo = DateTimePickerBefor.Value
                Else
                    dfrom = dTo.AddDays(-optMoment.CheckedItem.DataValue)
                End If
                DataGridMoment.DataSource = edt
                DataGridMoment.DataSource = GetTVData(tag, dfrom, dTo, archType_moment)
                Utils.SetupGridMS(DataGridMoment, tag, archType_moment)
                lblMoment.Text = GCaption
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

            Case "Пропущенные архивы"
                DataGridMissing.DataSource = GetmissingData(tag)
                DataGridMissing.Tag = tag
                DataGridMissing.Columns("NodeName").HeaderText = "Узел и тип архива"
                DataGridMissing.Columns(0).MinimumWidth = 300
                DataGridMissing.Columns(0).MinimumWidth = 300
                DataGridMissing.Columns(0).DefaultCellStyle.Format = "G"
                DataGridMissing.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                lblMissing.Text = GCaption
        End Select
    End Sub

    Public Sub RefreshData()

        Dim tag As Object
        tag = id
        RefreshDatabyID(tag, NodeName)
    End Sub

    Private Sub DateTimePickerAfter_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerAfter.ValueChanged
        'RefreshData()
    End Sub

    Private Sub DateTimePickerBefor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerBefor.ValueChanged
        'RefreshData()
    End Sub







    Private Sub SetupMoment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetupMoment.Click



        Dim tag As Object
        tag = id
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridMoment
        f.id_bd = tag
        f.ptype = archType_moment
        f.LoadData()

        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData()
        End If
    End Sub


    Private Sub cmdHourSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHourSetup.Click

        Dim tag As Object
        tag = id
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridHour
        f.id_bd = tag
        f.ptype = archType_hour
        f.LoadData()
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData()
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
        tag = id
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridDay
        f.id_bd = tag
        f.ptype = archType_day
        f.LoadData()
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTotalSetup.Click

        Dim tag As Object
        tag = id
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.Grid = DataGridTotal
        f.id_bd = tag
        f.ptype = archType_total
        f.LoadData()
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            RefreshData()
        End If
    End Sub

    Private Sub CheckBoxHour_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxHour.CheckedChanged

    End Sub


    'Private mTVCmd As OracleCommand = Nothing

    Private Function getTVCmd(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + " from datacurr where id_bd=:id_bd and  id_ptype=:id_ptype and dcounter >=:dfrom and dcounter<=:dto order by dcounter desc" + " "

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

    Private Function getTVCmdM(Optional ByVal FldList As String = "*") As OracleCommand
        Dim cmd As OracleCommand
        Dim ctext As String
        ctext = "select " + FldList + " from datacurr where id_bd=:id_bd and  id_ptype=:id_ptype and dcall >=:dfrom and (dcall-0.07)<=:dto order by dcall desc" + " "

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

    Private mMissingCmd As OracleCommand = Nothing

    Private Function getMissingCmd() As OracleCommand
        Dim cmd As OracleCommand
        If mMissingCmd Is Nothing Then
            cmd = New OracleCommand
            cmd.CommandText = "select DEVNAME as NodeName, ARCHDATE as ""Дата"" from missingarch where missingarch.ARCHDATE >= sysdate-40 and id_bd=:id_bd" + " "
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add("id_bd", OracleDbType.Int16)

            cmd.Connection = TvMain.dbconnect()
            cmd.Prepare()
            mMissingcmd = cmd
            Return cmd
        Else
            Return mMissingcmd
        End If

    End Function

    Private Function GetTVData(ByVal id_bd As Integer, ByVal dfrom As Date, ByVal dto As Date, ByVal id_ptype As Integer) As DataTable
        Dim cmd As OracleCommand
        If id_ptype = 1 Then
            cmd = getTVCmdM(Utils.ArchFieldList(id_bd, id_ptype))
        Else
            cmd = getTVCmd(Utils.ArchFieldList(id_bd, id_ptype))
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

        Return dt
    End Function

    Private Sub ButtonExportMissing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportMissing.Click
        'If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
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
        RefreshData()
    End Sub

    Private Sub optHour_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optHour.ValueChanged
        RefreshData()
    End Sub

    Private Sub optDay_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDay.ValueChanged
        RefreshData()
    End Sub

    Private Sub optTotal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTotal.ValueChanged
        RefreshData()
    End Sub

    Private Sub ClientForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bActivated = True
        RefreshData()

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
        Dim an As VIPAnalizer.NodeAnalizer = New VIPAnalizer.NodeAnalizer
        an.AnalizeNode(TvMain, DataGridMissing.Tag, 31)
       
        an = Nothing
        RefreshDatabyID(DataGridMissing.Tag, DataGridMissing.Text)
    End Sub


   

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        StopReading = True
        cmdStop.Enabled = False
        MsgBox("Опрос будет  остановлен." & vbCrLf & "Дождитесь окончания текущей операции.", MsgBoxStyle.Information, "Оcтановка опроса")
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

        End If
    End Sub



    Private Sub cmdRefreshMoment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshMoment.Click
        RefreshData()
    End Sub

    Private Sub cmdRefreshTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshTotal.Click
        RefreshData()
    End Sub

    Private Sub cmdRefreshDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshDay.Click
        RefreshData()
    End Sub

    Private Sub cmdRefreshHour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshHour.Click
        RefreshData()
    End Sub



    Private Sub cmdClearLst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearLst.Click

        BuildLstDT()
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

    End Sub

    Private Sub DataGridDay_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridDay.CellMouseDoubleClick
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
    End Sub


    Private Sub ClientForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MyfrmManual = Nothing
    End Sub

    Private Sub DataGridMissing_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridMissing.CellContentClick

    End Sub
End Class
