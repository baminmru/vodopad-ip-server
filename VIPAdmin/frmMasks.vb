
Public Class frmMasks

    Private id As Integer
    Private txtName As String
    Private cmbType As Integer
    Private cmbdev As Integer

    Private Sub frmMasks_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fmasks = Nothing
    End Sub
    Private Sub frmMasks_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RefreshGrid()


    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select t.id_mask,t.cname, paramtype.ctype, devices.cdevname,t.id_type,t.id_dev from masks t left join paramtype on paramtype.id_type=t.id_type left join devices on devices.id_dev=t.id_dev order by t.cname")
        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "id_mask"


                    Case "cname"
                        .HeaderText = "Название шаблона"
                        .Visible = True
                        .MinimumWidth = 200
                    Case "ctype"
                        .HeaderText = "Тип архива"
                        .Visible = True
                        .MinimumWidth = 200
                    Case "cdevname"
                        .HeaderText = "Тип устройства"
                        .Visible = True
                        .MinimumWidth = 200

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
            If view("id_mask") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("cname")
                Exit For
            End If
        Next
    End Sub


    Private Sub GV_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmMasksEdit
        f = New frmMasksEdit
        f.id = id
        f.txtName.Text = txtName
        f.id_type = cmbType
        f.id_dev = cmbdev
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub GV_RowEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.RowEnter
        Dim dgv As DataTable = GV.DataSource
        Dim rowIndex As Integer = e.RowIndex
        Dim dgvr As DataGridViewRow = GV.Rows(rowIndex)

        Dim view As DataRowView = Nothing
        Try


            view = dgvr.DataBoundItem

        Catch ex As Exception
            Return
        End Try
        txtName = view("cname")
        Try
            cmbType = view("id_type")
        Catch ex As Exception

        End Try
        Try
            cmbdev = view("id_dev")
        Catch ex As Exception

        End Try

        id = view("id_mask")
    End Sub



    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click

        Dim f As frmMasksEdit
        f = New frmMasksEdit
        f.id = 0
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "delete from Masksline where id_mask=" + id.ToString
                TvMain.QueryExec(s)

                s = "delete from Masks where id_mask=" + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmMasksEdit
        f = New frmMasksEdit
        f.id = id
        f.txtName.Text = txtName
        f.id_type = cmbType
        f.id_dev = cmbdev
        f.ShowDialog()
        refreshGrid()
    End Sub
End Class