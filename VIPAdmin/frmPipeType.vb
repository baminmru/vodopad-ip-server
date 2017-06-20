Public Class frmPipeType

    Private id As Integer
    Private txtName As String

    Private Sub frmPipeType_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fpipe = Nothing
    End Sub
    Private Sub frmPipeType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        refreshGrid()


    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select *  from PIPETYPE order by id_pipe")
        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "id_pipe"
                        .HeaderText = "ID"
                        .Visible = True
                        .MinimumWidth = 80
                    Case "pipename"
                        .HeaderText = "Название"
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
            If view("id_pipe") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("pipename")
                Exit For
            End If
        Next
    End Sub

    Private Sub GV_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmPipeTypeEdit
        f = New frmPipeTypeEdit
        f.id = id
        f.txtID.Text = f.id.ToString
        f.txtName.Text = txtName
        f.ShowDialog()
        refreshGrid()
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
        txtName = view("pipename")

        id = view("id_pipe")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmPipeTypeEdit
        f = New frmPipeTypeEdit
        f.id = id
        f.txtID.Text = f.id.ToString
        f.txtName.Text = txtName
        f.ShowDialog()
        refreshGrid()


    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim f As frmPipeTypeEdit
        f = New frmPipeTypeEdit
        f.id = 0
        f.txtID.Text = f.id.ToString
        f.txtName.Text = ""
        f.ShowDialog()
        refreshGrid()

    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "delete from PIPETYPE where id_pipe=" + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub


End Class