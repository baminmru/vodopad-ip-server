Imports Oracle.DataAccess.Client



Public Class frmSchema
    Public id As Integer
    Dim fname As String
    Dim LastShowD As Date
    Dim ds_id As String
    Dim EditParam As Boolean = False
    Dim ptype As Integer = 1
    Dim bActivated As Boolean = False
    Dim sezon As Integer = 0

    Dim myParam() As String = {
    "Схема",
    "Наименование счетчика",
    "№ прибора",
    "Расходомер",
    "Расходомер ГВС",
    "Расходомер ГВСц",
    "Расходомер M2",
    "Способ отбора",
    "Теп_камера",
    "Тип расходомера",
    "тип термометра",
    "Термопреобр ГВС",
    "Термопреобр",
    "Преобразователь давления"}

    '"Магистраль",
    '"Источник",
    '"Схема",
    '"Сист_теплопотребления",
    '"№ прибора",

    Private Sub LoadDogData()
        If Not bActivated Then Exit Sub
        If id = 0 Then Exit Sub
        Dim conDT As DataTable
        conDT = TvMain.QuerySelect("SELECT COLUMN_NAME,COMMENTS FROM user_col_comments WHERE table_name = 'CONTRACT' and COLUMN_NAME like 'FLD%'")
        Dim dtContract As DataTable
        dtContract = TvMain.QuerySelect("select * from contract where id_bd=" & id.ToString())

        Dim i As Integer
        Dim j As Integer
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow
        Dim ok As Boolean

        Dim Item As DataRow
        Item = dtContract.Rows(0)

        For j = 0 To UBound(myParam)
            ok = False

            For i = 0 To conDT.Rows.Count - 1
                If conDT.Rows(i)("COMMENTS") = myParam(j) Then
                    ok = True
                    Exit For
                End If

            Next

            If ok Then
                If Item(conDT.Rows(i)("COLUMN_NAME")).GetType().Name = "DBNull" Then
                    dr = dt.NewRow
                    dr("Название") = conDT.Rows(i)("COMMENTS")
                    dr("Значение") = "-"
                    dt.Rows.Add(dr)
                Else
                    If Item(conDT.Rows(i)("COLUMN_NAME")) & "" = "" Then
                        dr = dt.NewRow
                        dr("Название") = conDT.Rows(i)("COMMENTS")
                        dr("Значение") = "-"
                        dt.Rows.Add(dr)
                    Else
                        dr = dt.NewRow
                        dr("Название") = conDT.Rows(i)("COMMENTS")
                        dr("Значение") = Item(conDT.Rows(i)("COLUMN_NAME")) & ""
                        dt.Rows.Add(dr)
                    End If
                End If
            End If


        Next

        gv.DataSource = dt

        Dim q As String = ""

        q = q + "select bdevices.id_bd,bdevices.id_bu,bdevices.id_dev,bdevices.hiderow,bdevices.npquery,"
        q = q + " id_mask_curr,id_mask_hour,id_mask_24,id_mask_sum,"
        q = q + " bbuildings.id_who,bbuildings.id_grp,bbuildings.cshort,bbuildings.CFIO1,bbuildings.CPHONE1,bbuildings.CFIO2,bbuildings.CPHONE2,bbuildings.CADDRESS,bbuildings.FULLADDRESS,"
        q = q + " cgrpnm,cdevname,devschema.DS_id,devschema.name sname,whogive.cname wname from bdevices "
        q = q + " join bbuildings on bdevices.id_bu=bbuildings.id_bu"
        q = q + " left join devices on bdevices.id_dev = devices.id_dev "
        q = q + " left join bgroups on bbuildings.id_grp = bgroups.id_grp"
        q = q + " left join devschema on bdevices.scheme_name = devschema.name"
        q = q + " left join whogive on bbuildings.id_who=whogive.id_who  where bdevices.id_bd=" & id.ToString() + " order by cshort"
        Dim dtMain As DataTable
        dtMain = TvMain.QuerySelect(q)

        txtInfo.Text = "Узел: " & dtMain.Rows(0)("cshort") & ""
        txtInfo.Text += vbCrLf & "Прибор: " & dtMain.Rows(0)("cdevname") & ""
        txtInfo.Text += vbCrLf & "Группа: " & dtMain.Rows(0)("cgrpnm") & ""
        'txtInfo.Text += vbCrLf & dtMain.Rows(0)("caddress") & ""
        txtInfo.Text += vbCrLf & "Адрес:" & dtMain.Rows(0)("fulladdress") & ""
        txtInfo.Text += vbCrLf & "Поставщик: " & dtMain.Rows(0)("wname") & ""
        txtInfo.Text += vbCrLf & "Конткат1: " & dtMain.Rows(0)("cfio1") & ""
        txtInfo.Text += vbCrLf & dtMain.Rows(0)("cphone1") & ""
        txtInfo.Text += vbCrLf & "Контакт2: " & dtMain.Rows(0)("cfio2") & ""
        txtInfo.Text += vbCrLf & dtMain.Rows(0)("cphone2") & ""
    End Sub

    Private Sub FillSchemas()
        Dim di As System.IO.DirectoryInfo
        Dim fi As System.IO.FileInfo()
        ' Dim fileData As Byte()
        Dim i As Long

        di = New System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\schema\")
        fi = di.GetFiles("*.bmp")

        For i = LBound(fi, 1) To UBound(fi, 1)

            ' читаем файл
            'fileData = My.Computer.FileSystem.ReadAllBytes(fi(i).FullName)

            TvMain.LoadFileToField(fi(i).FullName, "devschema", "schema_image", "ds_id", fi(i).Name.Replace(".bmp", ""))
        Next


    End Sub
    Public Sub LoadData(ByVal newID As Integer)
        id = newID
        If Not bActivated Then Exit Sub

        Try
            If fname <> "" Then
                Kill(fname)
            End If
        Catch ex As Exception

        End Try

        fname = ""



        Dim cmd As OracleCommand


        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select  ds_id  from v_devschema_image where id_bd=" & id.ToString


        ds_id = cmd.ExecuteScalar
        If ds_id = "" Then
            fname = System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\schema\no.bmp"
            picSchema.Load(fname)
            fname = ""
        Else
            fname = System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\schema\" & Guid.NewGuid.ToString() & ".bmp"

            TvMain.SaveFileFromField(fname, "devschema", "schema_image", "ds_id", ds_id)
            Try
                picSchema.Load(fname)
            Catch ex As Exception

            End Try

        End If

        picSchema.Controls.Clear()

        If fname <> "" Then

            Dim da As OracleDataAdapter
            Dim dt As DataTable
            dt = New DataTable
            cmd = New OracleCommand
            cmd.Connection = TvMain.dbconnect
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from devschemaparam where HIDEPARAM=0 and ds_id=" + ds_id
            da = New OracleDataAdapter
            da.SelectCommand = cmd
            da.Fill(dt)
            cmd.Dispose()
            da.Dispose()
            Dim lbl As Label
            Dim chk As CheckBox
            Dim txt As TextBox

            Dim i As Integer
            Dim lblToolTipInfo As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo

            For i = 0 To dt.Rows.Count - 1


                If EditParam Then
                    txt = New TextBox
                    txt.BorderStyle = BorderStyle.FixedSingle
                    txt.Left = dt.Rows(i)("pos_left")
                    txt.Top = dt.Rows(i)("pos_top")
                    txt.Height = 22
                    txt.Width = 50
                    txt.Text = ""
                    txt.Tag = dt.Rows(i)("name") + "_max"
                    txt.ForeColor = Color.Red
                    txt.BackColor = Color.White
                    lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                    lblToolTipInfo.ToolTipText = "Максимум " & dt.Rows(i)("name")
                    picSchema.Controls.Add(txt)
                    Me.UltraToolTipManager1.SetUltraToolTip(txt, lblToolTipInfo)
                    txt.Show()


                    chk = New CheckBox
                    chk.Left = dt.Rows(i)("pos_left") + 51
                    chk.Top = dt.Rows(i)("pos_top")
                    chk.Height = 20
                    chk.Width = 20
                    chk.Text = ""
                    chk.Tag = "chk_" + dt.Rows(i)("name") + "_max"
                    chk.BackColor = Color.White
                    lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                    lblToolTipInfo.ToolTipText = "Проверять максимум для " & dt.Rows(i)("name")
                    picSchema.Controls.Add(chk)
                    Me.UltraToolTipManager1.SetUltraToolTip(chk, lblToolTipInfo)
                    chk.Show()

                Else


                    lbl = New Label
                    lbl.BorderStyle = BorderStyle.FixedSingle
                    lbl.Left = dt.Rows(i)("pos_left")
                    lbl.Top = dt.Rows(i)("pos_top")
                    lbl.Height = 22
                    lbl.Width = 50
                    lbl.Text = ""
                    lbl.Tag = dt.Rows(i)("name") + "_max"
                    lbl.ForeColor = Color.Red
                    lbl.BackColor = Color.White
                    lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                    lblToolTipInfo.ToolTipText = "Максимум " & dt.Rows(i)("name")
                    picSchema.Controls.Add(lbl)
                    Me.UltraToolTipManager1.SetUltraToolTip(lbl, lblToolTipInfo)
                    lbl.Show()
                End If

                lbl = New Label
                lbl.BorderStyle = BorderStyle.FixedSingle
                lbl.Left = dt.Rows(i)("pos_left")
                lbl.Top = dt.Rows(i)("pos_top") + 23
                lbl.Height = 22
                lbl.Width = 50
                lbl.Text = ""
                lbl.Tag = dt.Rows(i)("name")
                lbl.ForeColor = Color.Black
                lbl.BackColor = Color.White
                lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                lblToolTipInfo.ToolTipText = dt.Rows(i)("name")
                picSchema.Controls.Add(lbl)
                Me.UltraToolTipManager1.SetUltraToolTip(lbl, lblToolTipInfo)
                lbl.Show()


                If EditParam Then
                    txt = New TextBox
                    txt.BorderStyle = BorderStyle.FixedSingle
                    txt.Left = dt.Rows(i)("pos_left")
                    txt.Top = dt.Rows(i)("pos_top") + 46
                    txt.Height = 22
                    txt.Width = 50
                    txt.Text = ""
                    txt.Tag = dt.Rows(i)("name") + "_min"
                    txt.ForeColor = Color.Blue
                    txt.BackColor = Color.White
                    lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                    lblToolTipInfo.ToolTipText = "Минимум " & dt.Rows(i)("name")
                    picSchema.Controls.Add(txt)
                    Me.UltraToolTipManager1.SetUltraToolTip(txt, lblToolTipInfo)
                    txt.Show()

                    chk = New CheckBox

                    chk.Left = dt.Rows(i)("pos_left") + 50
                    chk.Top = dt.Rows(i)("pos_top") + 46
                    chk.Height = 20
                    chk.Width = 20
                    chk.Text = ""
                    chk.Tag = "chk_" + dt.Rows(i)("name") + "_min"
                    chk.BackColor = Color.White
                    lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                    lblToolTipInfo.ToolTipText = "Проверять минимум для " & dt.Rows(i)("name")
                    picSchema.Controls.Add(chk)
                    Me.UltraToolTipManager1.SetUltraToolTip(chk, lblToolTipInfo)
                    chk.Show()
                Else

                    lbl = New Label
                    lbl.BorderStyle = BorderStyle.FixedSingle
                    lbl.Left = dt.Rows(i)("pos_left")
                    lbl.Top = dt.Rows(i)("pos_top") + 46
                    lbl.Height = 22
                    lbl.Width = 50
                    lbl.Text = ""
                    lbl.Tag = dt.Rows(i)("name") + "_min"
                    lbl.ForeColor = Color.Blue
                    lbl.BackColor = Color.White
                    lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("name"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
                    lblToolTipInfo.ToolTipText = "Минимум " & dt.Rows(i)("name")
                    picSchema.Controls.Add(lbl)
                    Me.UltraToolTipManager1.SetUltraToolTip(lbl, lblToolTipInfo)
                    lbl.Show()
                End If

            Next
            LastShowD = Date.MinValue

            ShowMinMax()
            ShowData()
            LoadDogData()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FillSchemas()
    End Sub

    Private Sub frmSchema_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bActivated = True
        LoadData(id)

    End Sub

    Private Sub frmSchema_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        bActivated = False
    End Sub

    Private Sub frmSchema_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If fname <> "" Then
                Kill(fname)
            End If
        Catch ex As Exception

        End Try

        fname = ""
    End Sub

    Private Sub frmSchema_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub



    Private Function GetByName(ByVal name As String) As Control

        Dim i As Integer
        For i = 0 To picSchema.Controls.Count - 1
            If picSchema.Controls.Item(i).Tag.ToString().ToLower = name.ToLower Then
                Return picSchema.Controls.Item(i)
            End If
        Next
        Return Nothing
    End Function

    Private Sub ShowMinMax()
        Dim vdt As DataTable
        Dim cmd As OracleCommand

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from valuebounds  where sezon=" + sezon.ToString + " and  id_bd=" + id.ToString + " and ptype=" + ptype.ToString()

        da.SelectCommand = cmd
        vdt = New DataTable

        da.Fill(vdt)

        Dim ctl As Control
        Dim lbl As Label
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim i As Long
        For i = 0 To vdt.Rows.Count - 1




            If EditParam Then
                'ctl = GetByName(vdt.Rows(i)("pname"))
                'lbl = ctl

                ctl = GetByName(vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    txt = ctl
                    txt.Text = vdt.Rows(i)("PMIN")
                End If

                ctl = GetByName(vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    txt = ctl
                    txt.Text = vdt.Rows(i)("PMAX")
                End If

                ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    chk = ctl
                    If vdt.Rows(i)("ISMIN") = 1 Then
                        chk.Checked = True
                    Else
                        chk.Checked = False
                    End If
                End If

                ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    chk = ctl
                    If vdt.Rows(i)("ISMAX") = 1 Then
                        chk.Checked = True
                    Else
                        chk.Checked = False
                    End If
                End If


            Else


                ctl = GetByName(vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    lbl = ctl
                    lbl.Text = vdt.Rows(i)("PMIN").ToString()
                End If

                ctl = GetByName(vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    lbl = ctl
                    lbl.Text = vdt.Rows(i)("PMAX").ToString()
                End If

            End If


        Next

    End Sub


    Private Sub SaveParams()

        Dim cmd2 As OracleCommand
        cmd2 = New OracleCommand
        cmd2.Connection = TvMain.dbconnect
        cmd2.CommandType = CommandType.Text
        cmd2.CommandText = "update valuebounds set sezon=:SEZON,pmin=:PMIN,pmax=:PMAX,ismin=:ISMIN,ismax=:ISMAX where pname=:PNAME and id_bd=" + id.ToString + " and ptype=" + ptype.ToString()
        cmd2.Parameters.Add("SEZON", OracleDbType.Int16)
        cmd2.Parameters.Add("PMIN", OracleDbType.Decimal)
        cmd2.Parameters.Add("PMAX", OracleDbType.Decimal)
        cmd2.Parameters.Add("ISMIN", OracleDbType.Int16)
        cmd2.Parameters.Add("ISMAX", OracleDbType.Int16)
        cmd2.Parameters.Add("PNAME", OracleDbType.Varchar2)

        Dim vdt As DataTable
        Dim cmd As OracleCommand

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from valuebounds  where id_bd=" + id.ToString + " and ptype=" + ptype.ToString()

        da.SelectCommand = cmd
        vdt = New DataTable

        da.Fill(vdt)
        cmd.Dispose()
        da.Dispose()

        Dim ctl As Control

        Dim txt As TextBox
        Dim chk As CheckBox
        Dim i As Long

        cmd2.Parameters.Item("SEZON").Value = sezon

        For i = 0 To vdt.Rows.Count - 1
            If EditParam Then


                ctl = GetByName(vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    txt = ctl
                    'txt.Text = vdt.Rows(i)("PMIN")
                    cmd2.Parameters.Item("PMIN").Value = MyVal(txt.Text)
                End If

                ctl = GetByName(vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    txt = ctl
                    'txt.Text = vdt.Rows(i)("PMAX")
                    cmd2.Parameters.Item("PMAX").Value = MyVal(txt.Text)
                End If

                ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    chk = ctl
                    If chk.Checked = True Then
                        cmd2.Parameters.Item("ISMIN").Value = 1

                    Else
                        cmd2.Parameters.Item("ISMIN").Value = 0
                    End If
                End If

                ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    chk = ctl
                    If chk.Checked = True Then
                        cmd2.Parameters.Item("ISMAX").Value = 1

                    Else
                        cmd2.Parameters.Item("ISMAX").Value = 0
                    End If
                End If

                ctl = GetByName(vdt.Rows(i)("pname"))
                If Not ctl Is Nothing Then
                    cmd2.Parameters.Item("PNAME").Value = vdt.Rows(i)("pname")
                    cmd2.ExecuteNonQuery()
                End If

            End If


        Next
        cmd2.Dispose()

    End Sub

    Private Sub chkSEZON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSEZON.CheckedChanged
        If chkSEZON.Checked Then
            sezon = 0
        Else
            sezon = 1
        End If
        LoadData(id)

    End Sub

    Private Sub ShowData()
        Dim d As Date = Date.Today
        Dim cmd As OracleCommand
        'Dim ctl As Control


        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(dcounter) md from datacurr where id_bd=" + id.ToString + " and id_ptype=" + ptype.ToString()
        Try
            d = cmd.ExecuteScalar
        Catch ex As Exception

        End Try


        If d <= LastShowD Then Exit Sub
        LastShowD = d

        Dim dt As DataTable
        dt = New DataTable
        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        cmd.CommandText = "select * from datacurr where id_bd=" + id.ToString + " and id_ptype=" + ptype.ToString() + " and dcounter=:d"
        cmd.Parameters.Add("D", OracleDbType.Date)
        cmd.Parameters("D").Value = d
        da.SelectCommand = cmd
        da.Fill(dt)
        If dt.Rows.Count > 0 Then

            Dim i As Integer
            Dim lbl As Label
            For i = 0 To picSchema.Controls.Count - 1
                lbl = Nothing
                Try
                    lbl = picSchema.Controls.Item(i)
                Catch
                End Try

                If Not lbl Is Nothing Then
                    Try
                        lbl.Text = dt.Rows(0)(lbl.Tag)
                    Catch ex As Exception

                    End Try
                End If


            Next
        End If
        cmd.Dispose()
        da.Dispose()
        CheckBounds()

    End Sub


    Private Sub CheckBounds()
        Dim vdt As DataTable
        Dim cmd As OracleCommand

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from valuebounds  where id_bd=" + id.ToString + " and ptype=" + ptype.ToString()

        da.SelectCommand = cmd
        vdt = New DataTable

        da.Fill(vdt)
        cmd.Dispose()
        da.Dispose()

        Dim ctl As Control
        Dim lbl As Label
        'Dim txt As TextBox
        ' Dim chk As CheckBox
        Dim i As Long
        For i = 0 To vdt.Rows.Count - 1

            ctl = GetByName(vdt.Rows(i)("pname"))
            If Not ctl Is Nothing Then
                lbl = ctl
                If vdt.Rows(i)("ISMIN") = 1 Then
                    If MyVal(lbl.Text) < vdt.Rows(i)("PMIN") Then
                        lbl.BackColor = Color.Red
                        lbl.ForeColor = Color.White
                    End If
                End If
                If vdt.Rows(i)("ISMAX") = 1 Then
                    If MyVal(lbl.Text) > vdt.Rows(i)("PMAX") Then
                        lbl.BackColor = Color.Red
                        lbl.ForeColor = Color.White
                    End If
                End If
            End If

        Next


    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not bActivated Then Exit Sub
        ShowData()
    End Sub

    Public Sub SetupPanel()
        Dim f As frmPanelSetup
        f = New frmPanelSetup

        Dim i As Integer, J As Integer
        Dim ctl As Control
        Dim shape As NetronLight.ShapeBase
        f.grc.Shapes.Clear()
        f.grc.Invalidate()
        f.Refresh()
        ' f.BackgroundImage = picSchema.Image
        Dim img As System.Drawing.Image


        img = picSchema.Image.Clone
        f.grc.BackgroundImage = CType(img, System.Drawing.Image)
        f.grc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Dim cPfx As String

        For i = 0 To picSchema.Controls.Count - 1
            ctl = picSchema.Controls.Item(i)
            cPfx = ctl.Tag
            Try
                cPfx = ctl.Tag.Substring(ctl.Tag.ToString.Length - 4, 4)
            Catch
            End Try

            If cPfx <> "_min" And cPfx <> "_max" Then
                shape = New NetronLight.ShapeBase(f.grc)
                Dim loc As System.Drawing.Point
                loc = ctl.Location
                loc.Y -= 23
                shape = f.grc.AddShape(NetronLight.ShapeTypes.Rectangular, loc)
                shape.Text = ctl.Tag
                shape.Width = ctl.Width
                shape.Height = ctl.Height * 3
                shape.ShapeColor = System.Drawing.Color.White
            End If
        Next
        If f.ShowDialog() = DialogResult.OK Then
            For i = 0 To picSchema.Controls.Count - 1
                ctl = picSchema.Controls.Item(i)
                For J = 0 To f.grc.Shapes.Count - 1
                    shape = f.grc.Shapes.Item(J)

                    If shape.Text = ctl.Tag Then
                        ctl.Location = shape.Location
                        ctl.Width = shape.Width
                        ctl.Height = shape.Height

                        ' сохранение в базе данных
                        Dim cmd As OracleCommand


                        cmd = New OracleCommand
                        cmd.Connection = TvMain.dbconnect
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "update  devschemaparam set pos_left=" + ctl.Location.X.ToString + ", pos_top=" + ctl.Location.Y.ToString + "  where ds_id=" + ds_id + " and name='" + ctl.Tag + "'"
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()

                    End If
                Next

            Next



        End If

        LoadData(id)

    End Sub

    Private Sub cmdSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetup.Click
        If fname <> "" Then
            SetupPanel()
        End If
    End Sub

    'Private Sub UltraButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelSchema.Click
    '    Dim f As frmSelectSchema
    '    f = New frmSelectSchema
    '    f.id_bd = id
    '    If f.ShowDialog = Windows.Forms.DialogResult.OK Then
    '        LoadData(id)
    '    End If
    '    f = Nothing
    'End Sub

    Private Sub chkMoment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoment.CheckedChanged
        If chkMoment.Checked Then
            ptype = 1
            chkHour.Checked = False
            chkDay.Checked = False
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
        End If

    End Sub

    Private Sub chkHour_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHour.CheckedChanged
        If chkHour.Checked Then
            ptype = 3
            chkMoment.Checked = False
            chkDay.Checked = False
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
        End If

    End Sub

    Private Sub chkDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDay.CheckedChanged
        If chkDay.Checked Then
            ptype = 4
            chkMoment.Checked = False
            chkHour.Checked = False
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
        End If

    End Sub

    Private Sub cmdParams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdParams.Click
        Dim f As frmSelectParam
        f = New frmSelectParam
        f.LoadData(ds_id)
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            LoadData(id)
        End If
        f = Nothing
    End Sub

    Private Sub cmdCreateSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim s As String
        s = InputBox("Задайте имя схемы", "Схема")
        If s = "" Then Exit Sub
        Dim cmd As OracleCommand


        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "insert into devschema(ds_id,name) values(devschema_seq.nextval,'" + s + "')"
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    'Private Sub cmdChangePicture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangePicture.Click
    '    Dim f As frmChangeSchemaImage
    '    f = New frmChangeSchemaImage
    '    If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        LoadData(id)
    '    End If
    'End Sub

    Private Sub cmdValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValues.Click
        If EditParam Then
            ' сохранить изменения
            cmdValues.Text = "Граничные значения"
            SaveParams()
        Else
            cmdValues.Text = "Сохранить границы"
        End If



        EditParam = Not EditParam

        LoadData(id)

    End Sub
End Class