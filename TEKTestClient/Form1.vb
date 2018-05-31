Public Class Form1

    Private Sub cmdMReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMReq.Click
        'Dim s As TekServiceRef.TEkServiceSoapClient
        's = New TekServiceRef.TEkServiceSoapClient

        Dim s As SmeTEKService.TEkServiceSoapClient
        s = New SmeTEKService.TEkServiceSoapClient

        Dim str As String
        str = s.MeasurementsGet(txtMReq.Text)
        txtOut.Text = str
    End Sub

    Private Sub cmdNSReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNSReq.Click
        Dim s As SmeTEKService.TEkServiceSoapClient
        s = New SmeTEKService.TEkServiceSoapClient

        Dim str As String
        str = s.NSGet(txtNSReq.Text)
        txtOut.Text = str
    End Sub
End Class
