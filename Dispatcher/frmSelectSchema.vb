Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Public Class frmSelectSchema
    Public id_bd As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        Dim cmd As OracleCommand

        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update bdevices set scheme_fn ='" + cmbSchema.SelectedValue.ToString() + ".bmp', scheme_name='" + cmbSchema.Text + "' where id_bd=" + id_bd.ToString()
        cmd.ExecuteNonQuery()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSelectSchema_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbSchema.Items.Clear()
        cmbSchema.DisplayMember = "name"
        cmbSchema.ValueMember = "ds_id"
        Dim cmd As OracleCommand

        Dim dt As DataTable
        dt = New DataTable
        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select ds_id,name from devschema"
        da.SelectCommand = cmd
        da.Fill(dt)
        cmbSchema.DataSource = dt


    End Sub
End Class
