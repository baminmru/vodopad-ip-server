Imports System.Windows.Forms

Public Class frmSetup

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SaveSetting("Danfoss310Client", "setup", "fileDev", txtDev.Text)
        SaveSetting("Danfoss310Client", "setup", "fileApp", txtApp.Text)
        SaveSetting("Danfoss310Client", "setup", "DevID", txtID.Text)
        SaveSetting("Danfoss310Client", "setup", "IP", txtIP.Text)
        SaveSetting("Danfoss310Client", "setup", "IPPORT", txtIPPORT.Text)
        SaveSetting("Danfoss310Client", "setup", "PORT", txtPort.Text)
        SaveSetting("Danfoss310Client", "setup", "USEIP", IIf(optTCP.Checked, "TRUE", "FALSE"))
        SaveSetting("Danfoss310Client", "setup", "BAUD", txtBaud.Text)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtDev.Text = opf.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtApp.Text = opf.FileName
        End If
    End Sub

    Private Sub frmSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtDev.Text = GetSetting("Danfoss310Client", "setup", "fileDev", "")
        txtApp.Text = GetSetting("Danfoss310Client", "setup", "fileApp", "")
        txtID.Text = GetSetting("Danfoss310Client", "setup", "DevID", "1")
        txtIP.Text = GetSetting("Danfoss310Client", "setup", "IP", "192.168.1.100")
        txtIPPORT.Text = GetSetting("Danfoss310Client", "setup", "IPPORT", "502")
        txtPort.Text = GetSetting("Danfoss310Client", "setup", "PORT", "COM2")
        txtBaud.Text = GetSetting("Danfoss310Client", "setup", "BAUD", "38400")
        If "TRUE" = GetSetting("Danfoss310Client", "setup", "USEIP", "TRUE") Then
            optTCP.Checked = True
        Else
            optCOM.Checked = True
        End If
    End Sub
End Class
