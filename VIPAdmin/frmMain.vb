Public Class frmMain
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        TvMain.CloseDBConnection()
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TvMain = New STKTVMain.TVMain
        If TvMain.Init() = False Then Application.Exit()
      

    End Sub

    Private Sub cmdWhoGive_Click(sender As System.Object, e As System.EventArgs) Handles cmdWhoGive.Click
        Dim f As frmWhoGive
        f = New frmWhoGive
        f.ShowDialog()
        f = Nothing
    End Sub

    Private Sub cmdWhoGiveTop_Click(sender As System.Object, e As System.EventArgs) Handles cmdWhoGiveTop.Click
        Dim f As frmWhoGiveTop
        f = New frmWhoGiveTop
        f.ShowDialog()
        f = Nothing
    End Sub

    Private Sub cmdGroups_Click(sender As System.Object, e As System.EventArgs) Handles cmdGroups.Click
        Dim f As frmGroups
        f = New frmGroups
        f.ShowDialog()
        f = Nothing
    End Sub

    Private Sub cmdDevType_Click(sender As System.Object, e As System.EventArgs) Handles cmdDevType.Click
        Dim f As frmDevType
        f = New frmDevType
        f.ShowDialog()
        f = Nothing
    End Sub

    Private Sub cmdSchema_Click(sender As System.Object, e As System.EventArgs) Handles cmdSchema.Click
        Dim f As frmSchema
        f = New frmSchema
        f.ShowDialog()
        f = Nothing
    End Sub

    Private Sub cmdNewNode_Click(sender As System.Object, e As System.EventArgs) Handles cmdNewNode.Click
        Dim f As frmNodes

        f = New frmNodes
        f.ShowDialog()
        f = Nothing
    End Sub
End Class
