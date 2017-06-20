Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Public Class frmInfo

    Public ID As Integer
    Private Sub CheckAnalizer()
        Dim dt As DataTable
        dt = TvMain.QuerySelect("select count(*) cnt from hcmessages where was_reg=0 and id_bd=" & id.ToString & " and  dt_got >=sysdate-2/24  ")
        If dt.Rows.Count > 0 Then
            If Integer.Parse(dt.Rows(0)("cnt").ToString) = 0 Then
                TvMain.QueryExec("Update Analizer set INFO ='', color='' where id_bd=" & id.ToString)
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub frmInfo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fInf = Nothing
    End Sub

    Private Sub frmInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
       
        TvMain.QueryExec("update hcmessages set was_reg=1, dt_Reg=sysdate where was_reg=0 and id_bd=" & ID.ToString & "  and dt_got >=sysdate-3/24  ")
        CheckAnalizer()
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub
End Class
