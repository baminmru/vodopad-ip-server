Imports System.Windows.Forms

Public Class frmAlert

    Private Declare Function FlashWindow Lib "user32" (ByVal hwnd As Long, ByVal bInvert As Long) As Long


    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Timer1.Enabled = False
        Me.Hide()
    End Sub

    Private Sub frmAlert_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Timer1.Enabled = True
    End Sub

    Private Sub frmAlert_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fAlert = Nothing
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        FlashWindow(Me.Handle, 1)
    End Sub

    Private Sub frmAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub
End Class
