Imports System.Windows.Forms

Public Class frmSetup

    Private transport As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SaveSetting("Danfoss310Client", "setup", "fileDev", txtDev.Text)
        SaveSetting("Danfoss310Client", "setup", "fileApp", txtApp.Text)
        SaveSetting("Danfoss310Client", "setup", "DevID", txtID.Text)
        SaveSetting("Danfoss310Client", "setup", "IP", txtIP.Text)
        SaveSetting("Danfoss310Client", "setup", "IPPORT", txtIPPORT.Text)
        SaveSetting("Danfoss310Client", "setup", "PORT", txtPort.Text)

        If optTCP.Checked Then
            SaveSetting("Danfoss310Client", "setup", "TRANSPORT", "IP")
        End If

        If optSER2NET.Checked Then
            SaveSetting("Danfoss310Client", "setup", "TRANSPORT", "SER2NET")
        End If

        If optCOM.Checked Then
            SaveSetting("Danfoss310Client", "setup", "TRANSPORT", "COM")
        End If



        SaveSetting("Danfoss310Client", "setup", "IDBD", txtID_BD.Text)

        SaveSetting("Danfoss310Client", "setup", "BAUD", txtBaud.Text)

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If opf.ShowDialog = DialogResult.OK Then
            txtDev.Text = opf.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If opf.ShowDialog = DialogResult.OK Then
            txtApp.Text = opf.FileName
        End If
    End Sub

    Private Sub frmSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtDev.Text = GetSetting("Danfoss310Client", "setup", "fileDev", "")
        txtApp.Text = GetSetting("Danfoss310Client", "setup", "fileApp", "")
        txtID.Text = GetSetting("Danfoss310Client", "setup", "DevID", "1")
        txtID_BD.Text = GetSetting("Danfoss310Client", "setup", "IDBD", "")
        txtIP.Text = GetSetting("Danfoss310Client", "setup", "IP", "192.168.1.100")
        txtIPPORT.Text = GetSetting("Danfoss310Client", "setup", "IPPORT", "502")
        txtPort.Text = GetSetting("Danfoss310Client", "setup", "PORT", "COM2")
        txtBaud.Text = GetSetting("Danfoss310Client", "setup", "BAUD", "38400")
        transport = GetSetting("Danfoss310Client", "setup", "TRANSPORT", "IP")
        If transport = "IP" Then
            optTCP.Checked = True
        End If

        If transport = "SER2NET" Then
            optSer2Net.Checked = True
        End If


        If transport = "COM" Then
            optCOM.Checked = True
        End If
       
    End Sub
End Class
