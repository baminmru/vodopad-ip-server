
Imports System
Imports System.IO


Public Class Form1
    Private r As Resolver
    Private codes As Dictionary(Of String, String)


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim s As String

        If txtPath.Text = "" Then
            Exit Sub
        End If
        s = File.ReadAllText(txtPath.Text)
        Dim sl() As String
        sl = s.Split(vbCrLf)


     

        Dim i As Integer
        Dim j As Integer
        Dim ll() As String
        Dim ra As String
        Dim rr() As String


        txtOut.Text = ""
        For i = LBound(sl) To UBound(sl)

            ll = sl(i).Split(";")
            r = New Resolver
            If ll.Length > 1 Then
                ra = r.ResolveAddresses(ll(1))

                txtOut.Text += sl(i) + ";" + ra
                txtOut.Text += vbCrLf
                rr = ra.Split(";")
                txtSQL.Text += "update bbuildings set caddress='" + ll(1) + "',MAPX=" + rr(0) + ",MAPY=" + rr(1) + " where id_bu in (select id_bu from bdevices where id_bd=" + ll(0) + ")"
                txtSQL.Text += vbCrLf
                Application.DoEvents()

            End If

        Next
      


    End Sub

    Private Sub cmdPath_Click(sender As System.Object, e As System.EventArgs) Handles cmdPath.Click
        cdlg.Multiselect = False
        cdlg.CheckFileExists = True

        If cdlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtPath.Text = cdlg.FileName
        End If
    End Sub

   
End Class
