Public Class frmWhoGiveTopEdit

    Public id As Integer
  

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim s As String
        If id = 0 Then
            Try

                s = "insert into whogivetop(id_whotop,cname) values(whogivetop_seq.nextval,'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                s = "update whogivetop set  cname='" + txtName.Text + "'   where id_whotop=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub

   

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class