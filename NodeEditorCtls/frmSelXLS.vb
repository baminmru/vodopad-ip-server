Imports System.IO
Imports System.Windows.Forms

Public Class frmSelXLS
    Public lastFile As String
    Public lastPage As String
    Public subdir As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSelXLS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim di As DirectoryInfo
            di = New DirectoryInfo(GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\") & "\" & subdir & "\")
            Dim fi As FileInfo
            For Each fi In di.GetFiles("*.xl*")
                cmbXLS.Items.Add(fi.Name)
            Next
        Catch ex As Exception

        End Try
        Try
            cmbXLS.Text = lastFile
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbXLS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbXLS.SelectedIndexChanged
        Dim p As String
        p = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:") & "\" & subdir & "\" & cmbXLS.Text
        Dim workbook As SpreadsheetGear.IWorkbook
        Try
            cmbPage.Items.Clear()
            workbook = _
            SpreadsheetGear.Factory.GetWorkbook(p, _
            System.Globalization.CultureInfo.CurrentCulture)
            Dim wi As Integer
            For wi = 0 To workbook.Worksheets.Count - 1
                cmbPage.Items.Add(workbook.Worksheets.Item(wi).Name)
            Next
        Catch ex As Exception

        End Try
        Try
            cmbPage.Text = lastPage
        Catch ex As Exception

        End Try
    End Sub
End Class
