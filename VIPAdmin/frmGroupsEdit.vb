Public Class frmGroupsEdit

    Public id As Integer


    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Dim s As String
            Try
                s = "insert into bgroups(id_grp,cgrpnm) values(bgroups_seq.nextval,'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update bgroups set  cgrpnm='" + txtName.Text + "'   where id_grp=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs)



    End Sub


    Private Sub txtName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtName.TextChanged

    End Sub
    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
