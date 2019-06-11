Imports VIPAnalizer

Module Module1

    Public tvmain As STKTVMain.TVMain

    Sub Main()
        Dim logged As Boolean
        Try
            tvmain = New STKTVMain.TVMain
            logged = tvmain.Init()
            If logged Then

                Dim dt As DataTable
                Dim an As VIPAnalizer.NodeAnalizer
                dt = tvmain.GetTable("v_dev2_all")
                Dim i As Integer
                For i = 0 To dt.Rows.Count - 1
                    Try
                        an = New VIPAnalizer.NodeAnalizer
                        an.AnalizeNode(tvmain, dt.Rows(i)("id_bd"), 5, True)
                        an = Nothing
                    Catch ex As Exception

                    End Try

                Next
                tvmain.CloseDBConnection()
            End If
            tvmain = Nothing
        Catch ex As Exception

        End Try

    End Sub

End Module
