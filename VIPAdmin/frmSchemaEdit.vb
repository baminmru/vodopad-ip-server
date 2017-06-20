Public Class frmSchemaEdit

    Public id As Integer
    Public paramid As Integer
    Private fname As String
    Private txtInput As Integer
    Private txtPipe As Integer
    Private txtPipeType As Integer
    Private txtedizm As Integer
    Private txtParam As String
    Private txtSPipe As Integer


    Public Sub LoadData()

        Try
            If fname <> "" Then
                Kill(fname)
            End If
        Catch ex As Exception

        End Try

        fname = ""

        fname = System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\schema\" & Guid.NewGuid.ToString() & ".bmp"

        TvMain.SaveFileFromField(fname, "devschema", "schema_image", "ds_id", id)
        Try
            picSchema.Load(fname)
        Catch ex As Exception

        End Try


        If id = 0 Then
            TabPage2.Hide()
        End If

    End Sub

    Private pipeid As Integer = 0

    Public Sub refreshGrid()

        Dim previd As Integer
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim i As Integer

        previd = pipeid
        dt = TvMain.QuerySelect("select * from devschemapipes join pipetype on devschemapipes.pipetype_id= pipetype.id_pipe and devschemapipes.ds_id=" & id & " order by inputnumber, pipenumber")
        GV.DataSource = dt

        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "ds_id"
                    Case "dspipe_id"

                    Case "inputnumber"
                        .HeaderText = "Тепловой ввод"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "pipenumber"
                        .HeaderText = "Труба"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "pipename"
                        .HeaderText = "Тип трубы"
                        .Visible = True
                        .MinimumWidth = 400


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
            If view("dspipe_id") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("inputnumber")
                Exit For
            End If
        Next


        ''''''''''''''''''''''' params
        previd = paramid
        dt2 = TvMain.QuerySelect(" select DEVSCHEMAPARAM.*,edizm.name ename,inputnumber,pipenumber from DEVSCHEMAPARAM  left join devschemapipes on DEVSCHEMAPARAM.DSPIPE_ID =devschemapipes.dspipe_id left join edizm on DEVSCHEMAPARAM.EDIZM_ID=edizm.edizm_id where devschemaparam.ds_id=" & id & " order by DEVSCHEMAPARAM.name")
        GV2.DataSource = dt2

        For i = 0 To GV2.Columns.Count - 1

            With GV2.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "ds_id"
                    Case "dsp_id"
                    Case "pos_left"
                    Case "pos_top"
                    Case "hideparam"
                    Case "edizm_id"
                    Case "dspipe_id"



                    Case "inputnumber"
                        .HeaderText = "Тепловой ввод"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "pipenumber"
                        .HeaderText = "Труба"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "name"
                        .HeaderText = "Название"
                        .Visible = True
                        .MinimumWidth = 60

                    Case "ename"
                        .HeaderText = "ед. изм"
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
            If view("dspipe_id") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("inputnumber")
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
        txtInput = view("inputnumber")
        txtPipe = view("pipenumber")
        txtPipeType = view("pipetype_id")
        pipeid = view("dspipe_id")

    End Sub


    Private Sub GV_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        If id = 0 Then Exit Sub
        Dim f As frmSchemaPipe
        f = New frmSchemaPipe
        f.id = id
        f.pipeid = pipeid

        f.txtInputNumber.Text = txtInput
        f.txtPipeNumber.Text = txtPipe
        f.pipetypeid = txtPipeType
        f.ShowDialog()
        refreshGrid()
    End Sub


    Private Sub GV2_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV2.CellDoubleClick
        If id = 0 Then Exit Sub
        Dim f As frmSchemaParam
        f = New frmSchemaParam
        f.id = id
        f.paramid = paramid
        f.pipeid = txtSPipe
        f.edizmid = txtedizm
        f.nParam = txtParam

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
        paramid = view("dsp_id")
        Try
            txtedizm = view("edizm_id")
        Catch ex As Exception
            txtedizm = 0
        End Try

        Try
            txtSPipe = view("dspipe_id")
        Catch ex As Exception
            txtSPipe = 0
        End Try

        txtParam = view("name")


    End Sub


    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select max(ds_id) ds_id from devschema")
                ii = dt.Rows(0)("ds_id")
                Dim s As String
                s = "insert into devschema(ds_id,name) values(" + (ii + 1).ToString + ",'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update devschema set  name='" + txtName.Text + "'   where ds_id=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If
       

    End Sub

   
    

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton.Click
        If openFile.ShowDialog() = DialogResult.OK Then
            txtPath.Text = openFile.FileName
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If txtPath.Text <> "" Then
            TvMain.LoadFileToField(txtPath.Text, "devschema", "schema_image", "ds_id", id)
            LoadData()
        End If
    End Sub

    Private Sub frmSchemaEdit_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        refreshGrid()
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

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If id = 0 Then Exit Sub
        Dim f As frmSchemaPipe
        f = New frmSchemaPipe
        f.id = id
        f.pipeid = 0
        f.txtInputNumber.Text = "0"
        f.txtPipeNumber.Text = "0"
        f.cmbPipeType.SelectedIndex = -1
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If pipeid = 0 Then Exit Sub
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "delete from devschemapipes where dspipe_id=" + pipeid.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub



    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        If id = 0 Then Exit Sub
        Dim f As frmSchemaPipe
        f = New frmSchemaPipe
        f.id = id
        f.pipeid = pipeid

        f.txtInputNumber.Text = txtInput
        f.txtPipeNumber.Text = txtPipe
        f.pipetypeid = txtPipeType
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub cmdAdd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd2.Click
        If id = 0 Then Exit Sub
        Dim f As frmSchemaParam
        f = New frmSchemaParam
        f.id = id
        f.paramid = 0
        f.pipeid = 0
        f.edizmid = 0
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub GV_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellContentClick

    End Sub

    Private Sub cmdOpen2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen2.Click
        If id = 0 Then Exit Sub
        Dim f As frmSchemaParam
        f = New frmSchemaParam
        f.id = id
        f.paramid = paramid
        f.pipeid = txtSPipe
        f.edizmid = txtedizm
        f.nParam = txtParam
        
        f.ShowDialog()
        refreshGrid()
    End Sub
End Class