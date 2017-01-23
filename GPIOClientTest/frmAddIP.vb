Imports System.Windows.Forms
Imports System.Net

Public Class frmAddIP

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim addr As System.Net.IPAddress
        addr = New System.Net.IPAddress(0)
        If txtName.Text <> "" And System.Net.IPAddress.TryParse(txtAddress.Text, addr) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MsgBox("Задайте оба параметра")
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
