Public Class frmDanCFG

    Public id As Integer
    Public cfgid As Integer


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select nvl(max(id_dev),0) id_dev from  devices")
                ii = dt.Rows(0)("id_dev")
                Dim s As String
                s = "insert into devices(id_dev,cdevname,cdevdesc,dllname,VERIFYCOLS) values(" + (ii + 1).ToString + ",'" + txtApp.Text + "','" + txtGroup.Text + "','" + txtParam.Text + "','" + txtUnit.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update devices set  cdevname='" + txtApp.Text + "' ,cdevdesc='" + txtGroup.Text + "' ,DLLNAME='" + txtParam.Text + "', verifycols='" + txtUnit.Text + "'   where id_dev=" + id.ToString
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
