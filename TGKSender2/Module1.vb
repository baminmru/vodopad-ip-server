Module Module1
    ' Declare Ansi Function GetInterval Lib "vspclient.dll" ( _
    'ByVal psEqType As String, ByVal psSerNumber As String, _
    'ByRef piDayStart As Short, ByRef piMonStart As Short, ByRef piYearStart As Short, ByRef piHourStart As Short, ByRef piMinStart As Short, _
    'ByRef piDayFin As Short, ByRef piMonFin As Short, ByRef piYearFin As Short, ByRef piHourFin As Short, ByRef piMinFin As Short, ByVal iArcType As Short) As Integer

    ' 'Declare Ansi Function GetFormat Lib "vspclient.dll" (ByVal dwParameter As Long) As Integer

    ' Declare Ansi Function pute.ComSaveArchive Lib "vspclient.dll" (ByVal psArchive As String) As Integer

    Sub Main()
        Dim sndr As Sender
        Dim log As String
        Dim s As String
        s = Command()
        sndr = New Sender()
        If s.ToLower() = "nodel" Then
            log = sndr.SendAllToTGK(False, False)
        Else
            log = sndr.SendAllToTGK()
        End If

        Console.WriteLine(log)
    End Sub

End Module
