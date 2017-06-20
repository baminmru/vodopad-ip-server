Public Class frmIPEdit

    Public id As Integer


    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Dim s As String
            Try
                
                s = "insert into ipaddr(id_ip,cipaddr,machine,terminal) values(ipaddr_seq.nextval,'" + txtIP.Text + "','" + txtName.Text + "','" + txtName.Text + "')"


                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
               
                s = "update ipaddr set  cipaddr='" + txtIP.Text + "',machine='" + txtName.Text + "' ,terminal='" + txtName.Text + "'    where id_ip=" + id.ToString


                TvMain.QueryExec(s)

               

                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub






    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub




End Class
