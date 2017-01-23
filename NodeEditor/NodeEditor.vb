Public Class NodeEditor
    Public Function GetForm(ByVal ID As Integer, ByVal tvMain As STKTVMain.TVMain) As System.Windows.Forms.Form
        Dim f As ConfigForm
        f = New ConfigForm
        f.ID = ID
        f.TvMain = tvMain
        Return f
    End Function

End Class
