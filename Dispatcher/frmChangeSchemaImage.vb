Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Public Class frmChangeSchemaImage

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If txtPath.Text <> "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK

            TvMain.LoadFileToField(txtPath.Text, "devschema", "schema_image", "ds_id", cmbSchema.SelectedValue.ToString())
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmChangeSchemaImage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton.Click
        If openFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPath.Text = openFile.FileName
        End If
    End Sub
End Class
