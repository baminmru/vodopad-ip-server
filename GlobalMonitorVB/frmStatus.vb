Imports OsmExplorer.Routing

Public Class frmStatus
    Public rstop As RouteStop

    Private Sub frmStatus_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MyfrmStatus = Nothing
    End Sub
    Private Sub frmStatus_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Public Sub LoadData()
        If Not rstop Is Nothing Then
            txtAddr.Text = rstop.Address
            txtName.Text = rstop.Text
            txtPhones.Text = rstop.Contact
            Dim dt As DataTable
            Dim dr As DataRow
            Dim s As String = ""
            dt = TvMain.QuerySelect("select * from ANALIZER where id_bd=" + rstop.ID.ToString)
            If dt.Rows.Count >= 0 Then
                dr = dt.Rows(0)
                With dr
                    s = "Последние данные получены:" + .Item("LASTLINK").ToString + vbCrLf
                    s = s + "Сообщения :" + .Item("INFO").ToString
                    txtInfo.Text = s
                End With

            End If
        End If
    End Sub
    Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdOpenDisp_Click(sender As System.Object, e As System.EventArgs) Handles cmdOpenDisp.Click
        CallOpros()
    End Sub

    Private Sub cmdSetup_Click(sender As System.Object, e As System.EventArgs) Handles cmdSetup.Click
        CallSetup()
    End Sub
End Class