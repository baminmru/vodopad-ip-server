Public Class frmPipeTypeEdit

    Public id As Integer




    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select max(id_pipe) id from pipetype")
                ii = dt.Rows(0)("ds_id")
                Dim s As String
                s = "insert into pipetype(id_pipe,pipename) values(" + (ii + 1).ToString + ",'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update pipetype set  pipename='" + txtName.Text + "'   where id_pipe=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class