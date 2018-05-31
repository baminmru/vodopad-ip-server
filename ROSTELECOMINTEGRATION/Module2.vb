Module Module2
  
    Sub Main()
        Dim sndr As Sender

        Dim s As String
        s = Command()
        sndr = New Sender()
        Dim w() As String
        w = Split(s, " ", -1, CompareMethod.Text)
        Dim allBool As Boolean = False
        Dim dfBool As Boolean = False
        Dim dtBool As Boolean = False
        Dim df As Date = Date.Today.AddDays(-1)
        Dim dt As Date = Date.Today.AddDays(-1)

        Dim i As Integer
        Try
            For i = w.GetLowerBound(0) To w.GetUpperBound(0)

                If w(i).ToLower = "?" Or w(i).ToLower = "help" Or w(i).ToLower = "h" Then
                    MsgBox("RTI.exe [all] [from  start_date] [to end_date] [help] " & vbCrLf & "all -  отослать за все за последние 365 дней", MsgBoxStyle.Information, "Интеграция с сервисом сбора данных")
                    Return
                End If

                If w(i).ToLower() = "all" Then
                    allBool = True
                    Exit For
                End If

                If w(i).ToLower() = "from" Then
                    dfBool = True
                    Try
                        df = Date.Parse(w(i + 1))
                    Catch ex As Exception
                        df = Date.Today.AddDays(-1)
                        dfBool = False
                    End Try

                    i += 1

                End If

                If w(i).ToLower() = "to" Then
                    dtBool = True
                    Try
                        dt = Date.Parse(w(i + 1))
                    Catch ex As Exception
                        dt = Date.Today.AddDays(-1)
                        dtBool = False
                    End Try

                    i += 1

                End If



            Next
        Catch ex As Exception
            allBool = False
            dfBool = False
            dtBool = False
        End Try
        

        If allBool Then
            sndr.SendAllToRTI(False, True, Date.Today.AddDays(-366).Ticks, Date.Today.AddDays(-1).Ticks)
            Return
        End If
        If dfBool And dtBool Then
            sndr.SendAllToRTI(False, True, df.Ticks, dt.Ticks)
            Return
        End If

        If dfBool And Not dtBool Then
            sndr.SendAllToRTI(False, True, df.Ticks, df.Ticks)
            Return
        End If

        If Not dfBool And dtBool Then
            sndr.SendAllToRTI(False, True, dt.Ticks, dt.Ticks)
            Return
        End If

        sndr.SendAllToRTI()



    End Sub

End Module
