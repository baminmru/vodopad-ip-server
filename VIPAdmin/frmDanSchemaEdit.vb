Public Class frmDanSchemaEdit

    Public id As Integer
    Public paramid As Integer
    Public cfgid As Integer
    Private fname As String
    Private txtInput As Integer
    Private txtPipe As Integer
    Private txtPipeType As Integer
    Private txtedizm As Integer
    Private txtParam As String
    Private txtSPipe As Integer
    Private hidefs As Integer


    Public Sub LoadData()

        Try
            If fname <> "" Then
                Kill(fname)
            End If
        Catch ex As Exception

        End Try

        fname = ""

        fname = System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\schema\" & Guid.NewGuid.ToString() & ".bmp"

        TvMain.SaveFileFromField(fname, "danschema", "schema_image", "das_id", id)
        Try
            picSchema.Load(fname)
        Catch ex As Exception

        End Try


        If id = 0 Then
            TabPage2.Hide()
        End If

    End Sub



    Public Sub refreshGrid()

        Dim previd As Integer
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim i As Integer

        previd = cfgid
        dt = TvMain.QuerySelect("select * from dan310cfg  where das_id=" & id & " order by SPNU")
        GV.DataSource = dt

        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "das_id"
                    Case "dcfg_id"
                    Case "nuse"
                    Case "ntypeno"
                    Case "app_name"
                        .HeaderText = "Приложение"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "sgroup"
                        .HeaderText = "Группа"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "sparam"
                        .HeaderText = "Параметр"
                        .Visible = True
                        .MinimumWidth = 60

                    Case "sunit"
                        .HeaderText = "Единицы"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "stype"
                        .HeaderText = "Тип"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "nscale"
                        .HeaderText = "Множитель"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "spnu"
                        .HeaderText = "Номер"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "nmin"
                        .HeaderText = "Мин."
                        .Visible = True
                        .MinimumWidth = 60
                    Case "nmax"
                        .HeaderText = "Макс."
                        .Visible = True
                        .MinimumWidth = 60
                    Case "sdefault_val"
                        .HeaderText = "По умолчанию"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "swritable"
                        .HeaderText = "Запись разрешена"
                        .Visible = True
                        .MinimumWidth = 60

                End Select
            End With
        Next

        For i = 0 To GV.Rows.Count - 1
            Dim dgvr As DataGridViewRow = GV.Rows(i)

            Dim view As DataRowView = Nothing
            Try


                view = dgvr.DataBoundItem

            Catch ex As Exception

            End Try
            If view("dcfg_id") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("sparam")
                Exit For
            End If
        Next


        ''''''''''''''''''''''' params
        previd = paramid
        dt2 = TvMain.QuerySelect(" select DANSCHEMAPARAM.*,'(' ||CFG.SPNU || ') ' || CFG.sparam  sname from DANSCHEMAPARAM  left join DAN310CFG  CFG on DANSCHEMAPARAM.DCFG_ID =CFG.dcfg_id  where danschemaparam.das_id=" & id & " order by DANSCHEMAPARAM.name")
        GV2.DataSource = dt2

        For i = 0 To GV2.Columns.Count - 1

            With GV2.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "das_id"
                    Case "dasp_id"
                    Case "pos_left"
                    Case "pos_top"
                    Case "hideparam"
                  
                    Case "name"
                        .HeaderText = "Название"
                        .Visible = True
                        .MinimumWidth = 60

                    Case "sname"
                        .HeaderText = "Параметр"
                        .Visible = True
                        .MinimumWidth = 150

                    Case "hidefromschema"
                        .HeaderText = "Не отображать на схеме"
                        .Visible = True
                        .MinimumWidth = 150

                End Select
            End With
        Next
        For i = 0 To GV.Rows.Count - 1
            Dim dgvr As DataGridViewRow = GV.Rows(i)

            Dim view As DataRowView = Nothing
            Try


                view = dgvr.DataBoundItem

            Catch ex As Exception

            End Try
            If view("dcfg_id") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("name")
                Exit For
            End If
        Next









    End Sub


    Private Sub GV_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.RowEnter
        Dim dgv As DataTable = GV.DataSource
        Dim rowIndex As Integer = e.RowIndex
        Dim dgvr As DataGridViewRow = GV.Rows(rowIndex)

        Dim view As DataRowView = Nothing
        Try


            view = dgvr.DataBoundItem

        Catch ex As Exception
            Return
        End Try
        'txtInput = view("inputnumber")
        'txtPipe = view("pipenumber")
        'txtPipeType = view("pipetype_id")
        cfgid = view("dcfg_id")

    End Sub



    Private Sub GV2_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV2.CellDoubleClick
        If id = 0 Then Exit Sub
        Dim f As frmDanSchemaParam
        f = New frmDanSchemaParam
        f.id = id
        f.paramid = paramid
        f.cfgid = cfgid
        f.txtName.Text = txtParam
        f.hidefs = hidefs

        f.ShowDialog()
        refreshGrid()
    End Sub


    Private Sub GV2_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV2.RowEnter
        Dim dgv As DataTable = GV2.DataSource
        Dim rowIndex As Integer = e.RowIndex
        Dim dgvr As DataGridViewRow = GV2.Rows(rowIndex)

        Dim view As DataRowView = Nothing
        Try


            view = dgvr.DataBoundItem

        Catch ex As Exception
            Return
        End Try
        paramid = view("dasp_id")
        txtParam = view("name")
        cfgid = view("dcfg_id")
        hidefs = view("hidefromschema")


    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select max(das_id) das_id from danschema")
                If dt.Rows.Count = 0 Then
                    ii = 0
                Else
                    Try
                        ii = dt.Rows(0)("das_id")
                    Catch ex As Exception
                        ii = 0
                    End Try

                End If

                Dim s As String
                s = "insert into danschema(das_id,name) values(" + (ii + 1).ToString + ",'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update danschema set  name='" + txtName.Text + "'   where das_id=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton.Click
        If openFile.ShowDialog() = DialogResult.OK Then
            txtPath.Text = openFile.FileName
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If txtPath.Text <> "" Then
            TvMain.LoadFileToField(txtPath.Text, "danschema", "schema_image", "das_id", id)
            LoadData()
        End If
    End Sub

    Private Sub frmSchemaEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        refreshGrid()
        If id <> 0 And fname <> "" Then
            LoadParams()

        End If
    End Sub

    Private Sub frmSchemaEdit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If fname <> "" Then
                Kill(fname)
            End If
        Catch ex As Exception

        End Try

        fname = ""
    End Sub









    Private Sub cmdAdd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd2.Click
        If id = 0 Then Exit Sub
        Dim f As frmDanSchemaParam
        f = New frmDanSchemaParam
        f.id = id
        f.paramid = 0
        f.hidefs = 0


        f.ShowDialog()
        refreshGrid()
    End Sub



    Private Sub cmdOpen2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen2.Click
        If id = 0 Then Exit Sub
        Dim f As frmDanSchemaParam
        f = New frmDanSchemaParam
        f.id = id
        f.paramid = paramid
        f.cfgid = cfgid
        f.hidefs = hidefs
        f.txtName.Text = txtParam


        f.ShowDialog()
        refreshGrid()
    End Sub



    'Sub ReadDeviceFile()
    '    Dim filename As String
    '    filename = GetSetting("Danfoss310Client", "setup", "fileDev", "")
    '    If filename = "" Then Exit Sub
    '    Dim xml As System.Xml.XmlDocument
    '    xml = New System.Xml.XmlDocument
    '    xml.Load(filename)
    '    Dim xn As System.Xml.XmlNode
    '    Dim gr As System.Xml.XmlNode
    '    Dim pr As System.Xml.XmlNode
    '    Dim xnl As System.Xml.XmlNodeList
    '    Dim pl As System.Xml.XmlNodeList
    '    Dim unit As String = ""
    '    Dim scale As String

    '    Dim dr As DataRow


    '    xn = xml.LastChild()
    '    If xn.Name <> "ECL_configuration" Then Exit Sub
    '    xn = xn.FirstChild
    '    If xn.Name <> "Device_Sys" Then Exit Sub

    '    xnl = xn.ChildNodes ' список групп параметров
    '    For Each gr In xnl
    '        If gr.Name = "Group" Then
    '            pl = gr.ChildNodes  ' список параметров в группе
    '            For Each pr In pl
    '                If pr.Name = "Param" Then
    '                    'Try
    '                    '    unit = pr.Attributes("Unit").Value
    '                    'Catch ex As Exception
    '                    '    unit = ""
    '                    'End Try

    '                    Try
    '                        scale = pr.Attributes("Scale").Value
    '                    Catch ex As Exception
    '                        scale = "1"
    '                    End Try

    '                    dr = dt.NewRow

    '                    dr("Раздел") = "Устройство"
    '                    dr("Группа") = gr.FirstChild.Value
    '                    dr("Название") = pr.FirstChild.Value
    '                    If scale = "1" Then
    '                        dr("Единицы") = unit
    '                    Else
    '                        dr("Единицы") = unit + " * " + scale
    '                    End If
    '                    dr("Запись") = pr.Attributes("Writable").Value
    '                    dr("Значение") = ""
    '                    dr("Новое значение") = ""
    '                    dr("Номер параметра") = pr.Attributes("PNU").Value
    '                    dr("Примечание") = pr.Attributes("Descrip").Value
    '                    dr("Тип") = pr.Attributes("Type").Value
    '                    dr("Состояние") = "?"
    '                    dr("Использовать") = "0"
    '                    dt.Rows.Add(dr)
    '                    SaveRow(dr)
    '                    lblStatus.Text = "загрузка списка " + dr("Группа") + "\" + dr("Название")
    '                    Application.DoEvents()
    '                End If
    '            Next
    '        End If

    '    Next
    'End Sub
    Sub ReaddAppFile(ByVal filename As String)
        If filename = "" Then Exit Sub
        Dim xml As System.Xml.XmlDocument
        xml = New System.Xml.XmlDocument
        xml.Load(filename)
        Dim xn As System.Xml.XmlNode
        Dim gr As System.Xml.XmlNode
        Dim pr As System.Xml.XmlNode
        Dim xnl As System.Xml.XmlNodeList
        Dim pl As System.Xml.XmlNodeList

        Dim unit As String
        Dim scale As String
        Dim dt As DataTable
        Dim dr As DataRow
        Dim app As String = ""

        Dim col As System.Data.DataColumn


        xn = xml.LastChild()
        If xn.Name <> "ECL_configuration" Then Exit Sub
        xn = xn.FirstChild
        If xn.Name <> "Device_App" And xn.Name <> "Device_Sys" Then Exit Sub
        If xn.Name = "Device_App" Then
            app = "Приложение"
        Else
            app = "Устройство"
        End If

        xnl = xn.ChildNodes ' список групп параметров



        dt = New DataTable
        col = dt.Columns.Add("Раздел")
        col = dt.Columns.Add("Группа")
        col = dt.Columns.Add("Название")
        col = dt.Columns.Add("Единицы")
        col = dt.Columns.Add("Множитель")
        col = dt.Columns.Add("Запись")
        col = dt.Columns.Add("Мин")
        col = dt.Columns.Add("Макс")
        col = dt.Columns.Add("Значение")
        col = dt.Columns.Add("Номер параметра")
        col = dt.Columns.Add("Тип")
        col = dt.Columns.Add("Состояние")
        col = dt.Columns.Add("Примечание")

        For Each gr In xnl
            If gr.Name = "Group" Then


                pl = gr.ChildNodes  ' список параметров в группе
                For Each pr In pl
                    If pr.Name = "Param" Then
                        dr = dt.NewRow

                        Try
                            unit = pr.Attributes("Unit").Value
                        Catch ex As Exception
                            unit = ""
                        End Try

                        Try
                            scale = pr.Attributes("Scale").Value
                        Catch ex As Exception
                            scale = "1"
                        End Try

                        dr("Раздел") = app
                        dr("Группа") = gr.FirstChild.Value
                        dr("Название") = pr.FirstChild.Value
                        dr("Множитель") = scale

                        dr("Единицы") = unit


                        dr("Запись") = pr.Attributes("Writable").Value
                        dr("Значение") = pr.Attributes("Default_val").Value
                        dr("Мин") = pr.Attributes("Min").Value
                        dr("Макс") = pr.Attributes("Max").Value

                        dr("Номер параметра") = pr.Attributes("PNU").Value
                        dr("Тип") = pr.Attributes("Type").Value
                        dt.Rows.Add(dr)
                        SaveRow(dr)
                        lblOut.Text = dr("Номер параметра").ToString()
                        Application.DoEvents()
                    End If
                Next
            End If

        Next
        lblOut.Text = ""
    End Sub



    Sub SaveRow(ByRef dr As DataRow, Optional ByVal DropFirst As Boolean = False)
        Dim q As String
        Dim dt As DataTable



        If DropFirst Then
            q = "delete  from DAN310CFG where das_id=" & id.ToString & " and sPNU='" & dr("Номер параметра").ToString() & "'"

            Try
                TvMain.QueryExec(q)
            Catch ex As Exception

            End Try
        End If

        q = "select count(*) cnt  from DAN310CFG where das_id=" & id.ToString & " and sPNU='" & dr("Номер параметра").ToString() & "'"

        dt = TvMain.QuerySelect(q)
        Dim nr As Boolean
        nr = False

        If dt.Rows.Count = 0 Then
            nr = True

        Else
            If dt.Rows(0)("cnt") = 0 Then
                nr = True
            End If

        End If



        If nr Then

            'q = "insert into DAN310CFG(id_bd,SPART,SGROUP,SPARAM,SUNIT,NSCALE,STYPE,NTYPENO,NMIN,NMAX,SDEFAULT_VAL,SDESCRIP,SWRITABLE,SPNU,SVALUE,NUSE)  values ("
            q = "insert into DAN310CFG(dcfg_id,das_id,APP_NAME,SGROUP,SPARAM,SDESCRIP,SUNIT,STYPE,SWRITABLE,SPNU,NSCALE,NMIN,NMAX,SDEFAULT_VAL)  values (dancfg_seq.nextval,"
            q = q & id & ",'" & dr("Раздел") & "','" & dr("Группа") & "','" & dr("Название") & "','" & dr("Примечание") & "','" & dr("Единицы") & "','" & dr("Тип") & "','" & dr("Запись") & "','" & dr("Номер параметра") & "'," & dr("Множитель").ToString & "," & dr("Мин").ToString & "," & dr("Макс").ToString & ",'" & dr("Значение").ToString & "' )"

            Try
                TvMain.QueryExec(q)
            Catch ex As Exception

            End Try
            'Else
            'q = "update DAN310CFG set SVALUE='" & dr("Значение") & "',NUSE=" & dr("Использовать").ToString & "  where das_id=" & id.ToString & " and sPNU='" & dr("Номер параметра").ToString()

            'Try
            '    tvmain.QueryExec(q)
            'Catch ex As Exception

            'End Try
        End If




    End Sub


    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        If openFile.ShowDialog() = DialogResult.OK Then
            ReaddAppFile(openFile.FileName)
            refreshGrid()
        End If

    End Sub


    Private Sub cmdDel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel2.Click
        If paramid = 0 Then Exit Sub
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "delete from danschemaparam where dasp_id=" + paramid.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
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
            'Try
            '    cPfx = ctl.Tag.Substring(ctl.Tag.ToString.Length - 4, 4)
            'Catch
            'End Try

            'If cPfx <> "_min" And cPfx <> "_max" Then
            shape = New NetronLight.ShapeBase(f.grc)
            Dim loc As System.Drawing.Point
            loc = ctl.Location
            ' loc.Y -= 23
            shape = f.grc.AddShape(NetronLight.ShapeTypes.Rectangular, loc)
            shape.Text = ctl.Tag
            shape.Width = ctl.Width
            shape.Height = ctl.Height
            shape.ShapeColor = System.Drawing.Color.White
            'End If
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
                        
                        TvMain.QueryExec("update  danschemaparam set pos_left=" + ctl.Location.X.ToString + ", pos_top=" + ctl.Location.Y.ToString + "  where das_id=" + id.ToString + " and name='" + ctl.Tag + "'")
                        
                    End If
                Next

            Next



        End If

        LoadParams()

    End Sub


    Public Sub LoadParams()

        picSchema.Controls.Clear()

        If fname <> "" Then


            Dim dt As DataTable
            dt = TvMain.QuerySelect("select * from danschemaparam where HIDEPARAM=0 and HIDEFROMSCHEMA=0 and das_id=" + id.ToString)


            Dim lbl As Label
            'Dim chk As CheckBox
            'Dim txt As TextBox
            Dim EditParam As Boolean = False

            Dim i As Integer


            For i = 0 To dt.Rows.Count - 1


                'If EditParam Then
                '    txt = New TextBox
                '    txt.BorderStyle = BorderStyle.FixedSingle
                '    txt.Left = dt.Rows(i)("pos_left")
                '    txt.Top = dt.Rows(i)("pos_top")
                '    txt.Height = 22
                '    txt.Width = 50
                '    txt.Text = ""
                '    txt.Tag = dt.Rows(i)("name") + "_max"
                '    txt.ForeColor = Color.Red
                '    txt.BackColor = Color.White
                '    picSchema.Controls.Add(txt)
                '    Me.ToolTip1.SetToolTip(txt, dt.Rows(i)("name"))

                '    txt.Show()


                '    chk = New CheckBox
                '    chk.Left = dt.Rows(i)("pos_left") + 51
                '    chk.Top = dt.Rows(i)("pos_top")
                '    chk.Height = 20
                '    chk.Width = 20
                '    chk.Text = ""
                '    chk.Tag = "chk_" + dt.Rows(i)("name") + "_max"
                '    chk.BackColor = Color.White

                '    picSchema.Controls.Add(chk)

                '    Me.ToolTip1.SetToolTip(chk, "Проверять максимум для " & dt.Rows(i)("name"))
                '    chk.Show()

                'Else


                '    lbl = New Label
                '    lbl.BorderStyle = BorderStyle.FixedSingle
                '    lbl.Left = dt.Rows(i)("pos_left")
                '    lbl.Top = dt.Rows(i)("pos_top")
                '    lbl.Height = 22
                '    lbl.Width = 50
                '    lbl.Text = ""
                '    lbl.Tag = dt.Rows(i)("name") + "_max"
                '    lbl.ForeColor = Color.Red
                '    lbl.BackColor = Color.White

                '    picSchema.Controls.Add(lbl)
                '    Me.ToolTip1.SetToolTip(lbl, "Максимум " & dt.Rows(i)("name"))
                '    lbl.Show()
                'End If

                lbl = New Label
                lbl.BorderStyle = BorderStyle.FixedSingle
                lbl.Left = dt.Rows(i)("pos_left")
                lbl.Top = dt.Rows(i)("pos_top")
                lbl.Height = 22
                lbl.Width = 50
                lbl.Text = dt.Rows(i)("name")
                lbl.Tag = dt.Rows(i)("name")
                lbl.ForeColor = Color.Black
                lbl.BackColor = Color.White

                picSchema.Controls.Add(lbl)
                Me.ToolTip1.SetToolTip(lbl, dt.Rows(i)("name"))
                lbl.Show()


                'If EditParam Then
                '    txt = New TextBox
                '    txt.BorderStyle = BorderStyle.FixedSingle
                '    txt.Left = dt.Rows(i)("pos_left")
                '    txt.Top = dt.Rows(i)("pos_top") + 46
                '    txt.Height = 22
                '    txt.Width = 50
                '    txt.Text = ""
                '    txt.Tag = dt.Rows(i)("name") + "_min"
                '    txt.ForeColor = Color.Blue
                '    txt.BackColor = Color.White

                '    picSchema.Controls.Add(txt)
                '    Me.ToolTip1.SetToolTip(txt, "Минимум " & dt.Rows(i)("name"))
                '    txt.Show()

                '    chk = New CheckBox

                '    chk.Left = dt.Rows(i)("pos_left") + 50
                '    chk.Top = dt.Rows(i)("pos_top") + 46
                '    chk.Height = 20
                '    chk.Width = 20
                '    chk.Text = ""
                '    chk.Tag = "chk_" + dt.Rows(i)("name") + "_min"
                '    chk.BackColor = Color.White
                '    picSchema.Controls.Add(chk)
                '    Me.ToolTip1.SetToolTip(chk, "Проверять минимум для " & dt.Rows(i)("name"))
                '    chk.Show()
                'Else

                '    lbl = New Label
                '    lbl.BorderStyle = BorderStyle.FixedSingle
                '    lbl.Left = dt.Rows(i)("pos_left")
                '    lbl.Top = dt.Rows(i)("pos_top") + 46
                '    lbl.Height = 22
                '    lbl.Width = 50
                '    lbl.Text = ""
                '    lbl.Tag = dt.Rows(i)("name") + "_min"
                '    lbl.ForeColor = Color.Blue
                '    lbl.BackColor = Color.White
                '    picSchema.Controls.Add(lbl)
                '    Me.ToolTip1.SetToolTip(lbl, "Минимум " & dt.Rows(i)("name"))
                '    lbl.Show()
                'End If

            Next

        End If

    End Sub


    Private Sub cmdSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetup.Click
        If fname <> "" Then
            SetupPanel()
        End If
    End Sub

    Private Sub frmDanSchemaEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class