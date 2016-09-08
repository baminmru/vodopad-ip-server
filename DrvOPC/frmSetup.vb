Imports System.IO

Public Class frmSetup
    Public tvMain As STKTVMain.TVMain
    Public IDBD As Integer

    Private Sub SaveDataToBase()
        Dim dt As DataTable
        dt = tvMain.QuerySelect("select * from DRIVERSETUP where ID_BD=" + IDBD.ToString())
        If dt.Rows.Count > 0 Then
            tvMain.QueryExec("delete from DRIVERSETUP where ID_BD=" + IDBD.ToString())
        End If

        Dim sText As String
        sText = txtInfo.Text
        sText = sText.Replace("'", "''")
        tvMain.QueryExec("insert into DRIVERSETUP( ID_BD,SETUPXML) VALUES(" + IDBD.ToString() + ",null)")

        tvMain.LoadStringToField(txtInfo.Text, "DRIVERSETUP", "SETUPXML", "ID_BD", IDBD.ToString())

    End Sub

    Private Sub LoadTextFromBase()
        Dim sData As String = ""
        tvMain.GetStringFromField(sData, "DRIVERSETUP", "SETUPXML", "ID_BD", IDBD.ToString())


        txtInfo.Text = sData

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        SaveDataToBase()
        Me.Close()
    End Sub

    Private Sub frmSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTextFromBase()
    End Sub

    Private Sub cmdPath_Click(sender As Object, e As EventArgs) Handles cmdPath.Click
        opf.CheckFileExists = True
        opf.Multiselect = False
        If opf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtPath.Text = opf.FileName
            txtInfo.Text = File.ReadAllText(txtPath.Text)
        End If

    End Sub
End Class