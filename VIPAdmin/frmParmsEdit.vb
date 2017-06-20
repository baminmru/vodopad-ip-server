Public Class frmParamsEdit

    Public id As String


    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = "" Then
            Dim s As String
            Try
                
                Dim dt As DataTable
                Dim fid As Integer
                dt = TvMain.QuerySelect("select CONTRACT_seq.nextval cnt from dual")
                If dt.Rows.Count > 0 Then
                    fid = dt.Rows(0)(0)
                End If
                s = "alter table CONTRACT add FLD" + fid.ToString + " varchar2(48) null"
                TvMain.QueryExec(s)
                s = "comment on column CONTRACT.FLD" + fid.ToString + "  is '" + txtName.Text + "'"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "comment on column CONTRACT." + id + "  is '" + txtName.Text + "'"
                TvMain.QueryExec(s)
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
