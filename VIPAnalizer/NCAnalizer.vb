'Imports System.Data.OracleClient

Public Class TAnalParam
    Public name As String
    Public max As Double, min As Double
    Public IsMAx As Boolean, IsMin As Boolean
End Class

Public Enum msgTypeEnum
    ConnectMsg = 0 '- HC по связи или из тек. HC со счетчика, 
    HourMsg = 1 '- по час. арх., 
    DayMsg = 2 '- по сут. арх.
End Enum

Public Enum ApplicationTypeEnum
    AppAuto = 0 '- авомат., 
    AppManual = 1 '- ручной, 
    AppDisp = 2 '- диспетчер
End Enum


Public Class NCAnalizer
    Public tvmain As STKTVMain.TVMain
    Public id_bd As Integer
    Public NodeName As String


    Public Sub WriteHourNC(ByVal SaveAppType As ApplicationTypeEnum, ByVal ParamName As String, ByVal dt As Date, ByVal val As Double, ByVal minmax As Double, ByVal MaxNotMin As Boolean)
        Dim a As String
        Dim a1 As String
        If (MaxNotMin) Then
            a1 = "Выход за границы значения " + ParamName + " часового архива за " + Format(dt, "dd.MM.yyyy HH:ss") + "; " + ParamName + "=" + val.ToString() + " > " + minmax.ToString()
        Else
            a1 = "Выход за границы значения " + ParamName + " часового архива за " + Format(dt, "dd.MM.yyyy HH:ss") + "; " + ParamName + "=" + val.ToString() + " < " + minmax.ToString()

        End If
        a = "insert into hcmessages (id,id_bd,id_ip_got,msg_type,msg_text,was_reg,dt_got,node_name,appl_type) values(" + _
             "hcmessages_seq.nextval," + _
        id_bd.ToString() + "," + _
        "null" + "," + _
        "1," + _
        "'" + a1 + "'," + _
        "0," + _
        "sysdate," + _
        "'" + NodeName + "',0)"
        tvmain.QueryExec(a)

        tvmain.QueryExec("Update Analizer set HCCOUNT=HCCOUNT+1 where id_bd=" & id_bd.ToString)
        tvmain.QueryExec("Update Analizer set color='YELLOW' where (COLOR <> 'RED' or color is null) and id_bd=" & id_bd.ToString)
        tvmain.QueryExec("Update Analizer set INFO=INFO||'" & vbCrLf & a1 & "' where  id_bd=" & id_bd.ToString)

    End Sub

    Public Sub WriteDayNC(ByVal SaveAppType As ApplicationTypeEnum, ByVal ParamName As String, ByVal dt As Date, ByVal val As Double, ByVal minmax As Double, ByVal MaxNotMin As Boolean, Optional ByVal msg As String = "")
        Dim a As String
        Dim a1 As String
       

        If msg = "" Then
            If (MaxNotMin) Then
                a1 = "Выход за границы значения " + ParamName + " суточного архива за " + Format(dt, "dd.MM.yyyy HH:ss") + "; " + ParamName + "=" + val.ToString() + " > " + minmax.ToString()
            Else
                a1 = "Выход за границы значения " + ParamName + " суточного архива за " + Format(dt, "dd.MM.yyyy HH:ss") + "; " + ParamName + "=" + val.ToString() + " < " + minmax.ToString()

            End If
        Else
            a1 = msg
        End If

        a = "insert into hcmessages (id,id_bd,id_ip_got,msg_type,msg_text,was_reg,dt_got,node_name,appl_type) values(" + _
             "hcmessages_seq.nextval," + _
        id_bd.ToString() + "," + _
        "null" + "," + _
        "2," + _
        "'" + a1 + "'," + _
        "0," + _
        "sysdate," + _
        "'" + NodeName + "',0)"
        tvmain.QueryExec(a)

        tvmain.QueryExec("Update Analizer set HCCOUNT=HCCOUNT+1 where id_bd=" & id_bd.ToString)
        tvmain.QueryExec("Update Analizer set color='YELLOW' where (COLOR <> 'RED' or color is null) and id_bd=" & id_bd.ToString)
        tvmain.QueryExec("Update Analizer set INFO=INFO||'" & vbCrLf & a1 & "' where  id_bd=" & id_bd.ToString)

    End Sub

    Public Sub WriteMomentNC(ByVal SaveAppType As ApplicationTypeEnum, ByVal ParamName As String, ByVal dt As Date, ByVal val As Double, ByVal minmax As Double, ByVal MaxNotMin As Boolean)
        Dim a As String
        Dim a1 As String
        If (MaxNotMin) Then
            a1 = "Выход за границы значения " + ParamName + " текущих показателей за " + Format(dt, "dd.MM.yyyy HH:ss") + "; " + ParamName + "=" + val.ToString() + " > " + minmax.ToString()
        Else
            a1 = "Выход за границы значения " + ParamName + " текущих показателей за " + Format(dt, "dd.MM.yyyy HH:ss") + "; " + ParamName + "=" + val.ToString() + " < " + minmax.ToString()
        End If
        a = "insert into hcmessages (id,id_bd,id_ip_got,msg_type,msg_text,was_reg,dt_got,node_name,appl_type) values(" + _
             "hcmessages_seq.nextval," + _
        id_bd.ToString() + "," + _
        "null" + "," + _
        "1," + _
        "'" + a1 + "'," + _
        "0," + _
        "sysdate," + _
        "'" + NodeName + "',0)"
        tvmain.QueryExec(a)

        tvmain.QueryExec("Update Analizer set HCCOUNT=HCCOUNT+1 where id_bd=" & id_bd.ToString)
        tvmain.QueryExec("Update Analizer set color='YELLOW' where (COLOR <> 'RED' or color is null) and id_bd=" & id_bd.ToString)
        tvmain.QueryExec("Update Analizer set INFO=INFO||'" & vbCrLf & a1 & "' where  id_bd=" & id_bd.ToString)

    End Sub



    Public Function AnalizeHourNC(ByVal AppType As ApplicationTypeEnum, ByVal AnalHD As Double, ByVal sezon As Integer, ByVal Noheck As Boolean) As Boolean

        Dim param(64) As TAnalParam
        Dim i As Integer, paramq As Integer
        Dim value As Double
        Dim dt As Date
        Dim a As String
        Dim ok As Boolean = False
        Dim q As DataTable

        '// analize temperature and consumption values
        Do

            '// get values bounds
            q = tvmain.QuerySelect("select * from valuebounds where sezon=" + sezon.ToString + " and id_bd=" + id_bd.ToString + _
                            " and (substr(pname, 1, 1)='P' or substr(pname, 1, 1)='V' or substr(pname, 1, 1)='G' or substr(pname, 1, 1)='T'  or " + _
                            "substr(pname, 1, 1)='M' or substr(pname, 1, 1)='D' ) and ptype=3 and (ismin=1 or ismax=1)")

            If (q.Rows.Count = 0) Then
                Exit Do
            End If
            Dim ii As Integer
            paramq = 0
            For ii = 0 To q.Rows.Count - 1
                param(paramq) = New TAnalParam
                param(paramq).name = q.Rows(ii)("PNAME")
                param(paramq).IsMAx = False
                param(paramq).IsMin = False

                a = q.Rows(ii)("ISMAX")
                If a = "1" Then
                    param(paramq).max = q.Rows(ii)("PMAX")
                    param(paramq).IsMAx = True
                End If

                a = q.Rows(ii)("ISMIN")
                If a = "1" Then
                    param(paramq).min = q.Rows(ii)("PMIN")
                    param(paramq).IsMin = True
                End If

                If (param(paramq).IsMin Or param(paramq).IsMAx) Then
                    paramq += 1
                End If
            Next
            If (paramq = 0) Then
                Exit Do
            End If


            '// get archive records
            a = "select * from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=3 and dcounter>(sysdate-" + AnalHD.ToString().Replace(",", ".") + "/24) and (check_a <> 1 or check_a is null) order by dcounter"
            q = tvmain.QuerySelect(a)

            If (q.Rows.Count = 0) Then
                Exit Do
            End If
            For ii = 0 To q.Rows.Count
                '// get values for each string
                dt = q.Rows(ii)("DCOUNTER")

                For i = 0 To paramq
                    Try
                        value = q.Rows(ii)(param(i).name)

                        'value = Val(a.Replace(",", "."))
                        '          // check minimum
                        If param(i).IsMin And (value < param(i).min) Then
                            WriteHourNC(AppType, param(i).name, dt, value, param(i).min, False)
                        End If

                        '              // check maximum
                        If param(i).IsMAx And (value > param(i).max) Then
                            WriteHourNC(AppType, param(i).name, dt, value, param(i).max, True)
                        End If

                    Catch
                    End Try
                Next

                If Noheck = False Then
                    'write checked flag
                    a = "update datacurr set check_a=1 where id_bd=" + id_bd + " and id_ptype=3 and dcounter=" + tvmain.OracleDate(dt)
                    Try
                        tvmain.QueryExec(a)
                    Catch ex As Exception
                        ' Exit Do
                    End Try

                End If

            Next
            ok = True

        Loop While (True)
        Return ok
    End Function


    Public Function AnalizeDayNC(ByVal AppType As ApplicationTypeEnum, ByVal AnalHD As Double, ByVal sezon As Integer, ByVal method As Integer) As Boolean

        Dim param(64) As TAnalParam

        Dim i As Integer, paramq As Integer
        Dim value As Double
        Dim dt As Date
        Dim a As String
        Dim ok As Boolean = False
        Dim q As DataTable

        '// analize temperature and consumption values
        Do
            '// get values bounds
            q = tvmain.QuerySelect("select * from valuebounds where sezon=" + sezon.ToString + " and id_bd=" + id_bd.ToString() + _
                            " and (substr(pname, 1, 1)='P' or substr(pname, 1, 1)='V' or substr(pname, 1, 1)='G' or substr(pname, 1, 1)='M' or substr(pname, 1, 1)='D') and ptype=4 and (ismin=1 or ismax=1)")
            If (q.Rows.Count = 0) Then
                Exit Do
            End If
            Dim ii As Integer
            paramq = 0
            For ii = 0 To q.Rows.Count - 1
                param(paramq) = New TAnalParam
                param(paramq).name = q.Rows(ii)("PNAME")
                param(paramq).IsMAx = False
                param(paramq).IsMin = False

                a = q.Rows(ii)("ISMAX")
                If a = "1" Then
                    param(paramq).max = q.Rows(ii)("PMAX")
                    param(paramq).IsMAx = True

                End If

                a = q.Rows(ii)("ISMIN")
                If a = "1" Then
                    param(paramq).min = q.Rows(ii)("PMIN")
                    param(paramq).IsMin = True
                End If

                If (param(paramq).IsMin Or param(paramq).IsMAx) Then
                    paramq += 1
                End If
            Next
            If (paramq = 0) Then
                Exit Do
            End If

            '// get archive records
            a = "select * from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=4 and " + _
            "dcounter>(sysdate-" + AnalHD.ToString().Replace(",", ".") + "/24) and (check_a <> 1 or check_a is null) order by dcounter"
            q = tvmain.QuerySelect(a)

            If (q.Rows.Count = 0) Then
                Exit Do
            End If

            For ii = 0 To q.Rows.Count
                '// get values for each string
                dt = q.Rows(ii)("DCOUNTER")

                For i = 0 To paramq
                    Try
                        value = q.Rows(ii)(param(i).name)

                        'value = Val(a)
                        '          // check minimum
                        If param(i).IsMin And (value < param(i).min) Then
                            WriteDayNC(AppType, param(i).name, dt, value, param(i).min, False)
                        End If

                        '              // check maximum
                        If param(i).IsMAx And (value > param(i).max) Then
                            WriteDayNC(AppType, param(i).name, dt, value, param(i).max, True)
                        End If

                    Catch
                    End Try
                Next

                If method = 1 Then
                    Dim t1 As Double, t2 As Double
                    t1 = q.Rows(ii)("T1")
                    t2 = q.Rows(ii)("T2")
                    If (t2 > (t1 / 2 + 5)) Then
                        WriteDayNC(AppType, "T1", dt, t2, t1 / 2 + 5, True, "T2 > T1/2+5, где T1=" + t1.ToString())
                    End If
                End If

                'write checked flag
                a = "update datacurr set check_a=1 where id_bd=" + id_bd + " and id_ptype=4 and dcounter=to_date(" + _
                                Format(dt, "yyyyMMddHHmmss") + ",'yyyymmddhh24miss')"
                Try
                    tvmain.QueryExec(a)
                Catch ex As Exception
                    ' Exit Do
                End Try
            Next
            ok = True

        Loop While (True)
        Return ok
    End Function




    Public Function AnalizeMomentNC(ByVal AppType As ApplicationTypeEnum, ByVal AnalHD As Double, ByVal sezon As Integer, ByVal Noheck As Boolean) As Boolean

        Dim param(64) As TAnalParam
        Dim i As Integer, paramq As Integer
        Dim value As Double
        Dim dt As Date
        Dim a As String
        Dim ok As Boolean = False
        Dim q As DataTable

        '// analize temperature and consumption values
        Do

            '// get values bounds
            q = tvmain.QuerySelect("select * from valuebounds where sezon=" + sezon.ToString + " and id_bd=" + id_bd.ToString + _
                            " and (substr(pname, 1, 1)='P' or substr(pname, 1, 1)='V' or substr(pname, 1, 1)='G' or substr(pname, 1, 1)='T'  or " + _
                            "substr(pname, 1, 1)='M' or substr(pname, 1, 1)='D' ) and ptype=1 and (ismin=1 or ismax=1)")

            If (q.Rows.Count = 0) Then
                Exit Do
            End If
            Dim ii As Integer
            paramq = 0
            For ii = 0 To q.Rows.Count - 1
                param(paramq) = New TAnalParam
                param(paramq).name = q.Rows(ii)("PNAME")
                param(paramq).IsMAx = False
                param(paramq).IsMin = False

                a = q.Rows(ii)("ISMAX")
                If a = "1" Then
                    param(paramq).max = q.Rows(ii)("PMAX")
                    param(paramq).IsMAx = True
                End If

                a = q.Rows(ii)("ISMIN")
                If a = "1" Then
                    param(paramq).min = q.Rows(ii)("PMIN")
                    param(paramq).IsMin = True
                End If

                If (param(paramq).IsMin Or param(paramq).IsMAx) Then
                    paramq += 1
                End If
            Next
            If (paramq = 0) Then
                Exit Do
            End If


            '// get archive records
            a = "select * from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=1 and dcounter>(sysdate-" + AnalHD.ToString().Replace(",", ".") + "/24) and (check_a <> 1 or check_a is null) order by dcounter"
            q = tvmain.QuerySelect(a)

            If (q.Rows.Count = 0) Then
                Exit Do
            End If
            For ii = 0 To q.Rows.Count
                '// get values for each string
                dt = q.Rows(ii)("DCOUNTER")

                For i = 0 To paramq
                    Try
                        value = q.Rows(ii)(param(i).name)

                        'value = Val(a.Replace(",", "."))
                        '          // check minimum
                        If param(i).IsMin And (value < param(i).min) Then
                            WriteMomentNC(AppType, param(i).name, dt, value, param(i).min, False)
                        End If

                        '              // check maximum
                        If param(i).IsMAx And (value > param(i).max) Then
                            WriteMomentNC(AppType, param(i).name, dt, value, param(i).max, True)
                        End If

                    Catch
                    End Try
                Next

                If Noheck = False Then
                    'write checked flag
                    a = "update datacurr set check_a=1 where id_bd=" + id_bd + " and id_ptype=1 and dcounter=" + tvmain.OracleDate(dt)
                    Try
                        tvmain.QueryExec(a)
                    Catch ex As Exception
                        ' Exit Do
                    End Try

                End If

            Next
            ok = True

        Loop While (True)
        Return ok
    End Function


End Class

