Public Class frmParams

    Private id As String
    Private txtname As String

    Private Sub frm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fpar = Nothing
    End Sub
    Private Sub frm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        refreshGrid()


    End Sub

    Private Sub refreshGrid()
        Dim previd As String
        previd = id
        Dim dt As DataTable



        dt = TvMain.QuerySelect("SELECT COLUMN_NAME,COMMENTS FROM user_col_comments WHERE table_name = 'CONTRACT' and COLUMN_NAME like 'FLD%' order by COMMENTS")


   


        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName
                    Case "COLUMN_NAME"

                    Case "COMMENTS"
                        .HeaderText = "Название параметра"
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
            If view("COLUMN_NAME") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("COMMENTS")
                Exit For
            End If
        Next
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
        txtname = view("COMMENTS")

        id = view("COLUMN_NAME")
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmParamsEdit
        f = New frmParamsEdit
        f.id = id
        f.txtName.Text = txtname
        f.ShowDialog()
        refreshGrid()

    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim f As frmParamsEdit
        f = New frmParamsEdit
        f.id = ""
        f.txtName.Text = ""
        f.ShowDialog()
        refreshGrid()


    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "alter table CONTRACT drop column " + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GV_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmParamsEdit
        f = New frmParamsEdit
        f.id = id
        f.txtName.Text = txtname
        f.ShowDialog()
        refreshGrid()
    End Sub
End Class
