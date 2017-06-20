Imports System.Windows.Forms
Imports Oracle.DataAccess.Client


Public Class frmSetupGroup
    Public TvMain As STKTVMain.TVMain
    Public ID As Int32
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim cmd As OracleCommand
        cmd = New OracleCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update BGROUPS set CGRPNM='" & txtName.Text & "' , HIDEROW=" & IIf(chkHideRow.Checked, "1", "0") & " where id_grp=" & ID.ToString()
        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch
            cmd.Dispose()
            Exit Sub
        End Try

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSetupGroup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim dt As DataTable
        dt = TvMain.GetTable("BGROUPS", " where id_GRP=" & ID.ToString)
        If dt.Rows.Count > 0 Then
            txtName.Text = dt.Rows(0)("CGRPNM").ToString()
            If dt.Rows(0)("HIDEROW") = 0 Then
                chkHideRow.Checked = False
            Else
                chkHideRow.Checked = True
            End If
        End If
    End Sub
End Class
