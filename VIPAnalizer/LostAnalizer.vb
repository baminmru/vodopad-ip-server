Imports System.Data.OracleClient





Public Class LostAnalizer
    Public tvmain As STKTVMain.TVMain
    Public id_bd As Integer
    Public NodeName As String
   

    Public Function VerifyD24H(ByVal checkday As Date) As Boolean

        Dim q1 As String
        Dim q2 As String
        Dim d1 As Date
        Dim d2 As Date
        Dim dt1 As DataTable, dt2 As DataTable, dt0 As DataTable
        Dim delta As Double
        Dim tdelta As Double
        Dim vcols As String = ""
        Dim hs As Integer = 0


        delta = 0.01
        tdelta = 1

        d1 = DateSerial(checkday.Year, checkday.Month, checkday.Day)


        d2 = d1.AddDays(1)

        dt0 = tvmain.QuerySelect("select HOURSHIFT,VERIFYCOLS from bdevices join devices on bdevices.id_dev=devices.id_dev where id_bd=" & id_bd.ToString)

        If dt0.Rows.Count = 1 Then
            Try
                vcols = dt0.Rows(0)("VERIFYCOLS")
                hs = dt0.Rows(0)("HOURSHIFT")
            Catch ex As Exception

            End Try
        End If


        q1 = "select sum(nvl(t1,0)) t1 , sum(nvl(t2,0)) t2 , sum(q1) q1 ,sum(q2) q2,sum(v1) v1, sum(v2) v2, sum(m1) m1, sum(m2) m2, count(*) hcnt from datacurr " + _
            " where id_bd=" + id_bd.ToString + " and id_ptype=3 and " + _
            " dcounter>=" + OracleDate(d1.AddHours(hs)) + "  and " + _
            " dcounter<" + OracleDate(d2.AddHours(hs)) + " "

        q2 = "select sum(nvl(t1,0)) t1 , sum(nvl(t2,0)) t2 , sum(q1)q1 ,sum(q2) q2,sum(v1) v1, sum(v2) v2, sum(m1) m1, sum(m2) m2 from datacurr " + _
          " where id_bd=" + id_bd.ToString + " and id_ptype=4 and " + _
            " dcounter>=" + OracleDate(d1) + "  and " + _
            " dcounter<" + OracleDate(d2) + " "
        dt1 = tvmain.QuerySelect(q1)
        dt2 = tvmain.QuerySelect(q2)

        Dim rrr As Integer = 0
        If dt1.Rows.Count = 1 And dt2.Rows.Count = 1 Then
            If dt1.Rows(0)("hcnt") <> 24 Then
                rrr = 3
            Else
                rrr = 1

                If vcols.ToUpper().IndexOf("T1;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("t1") - dt2.Rows(0)("t1") * 24) > tdelta Then rrr = 2
                        Debug.Print("DT=" + (dt1.Rows(0)("t1") - dt2.Rows(0)("t1") * 24).ToString())
                    Catch ex As Exception

                    End Try

                End If

                If vcols.ToUpper().IndexOf("T2;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("t2") / 24 - dt2.Rows(0)("t2")) > tdelta Then rrr = 2
                        Debug.Print("DT=" + (dt1.Rows(0)("t2") - dt2.Rows(0)("t2") * 24).ToString())
                    Catch ex As Exception

                    End Try
                End If

                If vcols.ToUpper().IndexOf("Q1;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("q1") - dt2.Rows(0)("q1")) > delta Then rrr = 2
                    Catch ex As Exception

                    End Try
                End If

                If vcols.ToUpper().IndexOf("Q2;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("q2") - dt2.Rows(0)("q2")) > delta Then rrr = 2
                    Catch ex As Exception

                    End Try
                End If

                If vcols.ToUpper().IndexOf("V1;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("v1") - dt2.Rows(0)("v1")) > delta Then rrr = 2
                    Catch ex As Exception

                    End Try
                End If

                If vcols.ToUpper().IndexOf("V2;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("v2") - dt2.Rows(0)("v2")) > delta Then rrr = 2
                    Catch ex As Exception

                    End Try
                End If

                If vcols.ToUpper().IndexOf("M1;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("m1") - dt2.Rows(0)("m1")) > delta Then rrr = 2
                    Catch ex As Exception

                    End Try
                End If

                If vcols.ToUpper().IndexOf("M2;") >= 0 Then
                    Try
                        If Math.Abs(dt1.Rows(0)("m2") - dt2.Rows(0)("m2")) > delta Then rrr = 2
                    Catch ex As Exception

                    End Try
                End If

                End If

                tvmain.QueryExec("update datacurr  set d_eql_24='" + rrr.ToString() + "'" + _
                " where id_bd=" + id_bd.ToString + " and id_ptype=4 and " + _
                " dcounter>=" + OracleDate(d1) + "  and " + _
                " dcounter<" + OracleDate(d2) + " and d_eql_24<>'" + rrr.ToString() + "'")

        End If
        If rrr = 1 Then Return True

        Return False
    End Function
   


    Public Function AnalizeLost(ByVal deep As Integer) As Boolean
        Dim dt As DataTable
        dt = tvmain.QuerySelect("select count(*) cnt  from plancall join bdevices on plancall.id_bd=bdevices.id_bd " & _
         " where bdevices.id_bd=" + id_bd.ToString + " and ( bdevices.npquery = 1 Or plancall.Cstatus = 0 )")

        Dim i As Integer = 0

        i = dt.Rows(0)("cnt")
        If i <> 0 Then
            CheckMissing(id_bd)
            'Application.DoEvents()
            CheckLost(id_bd, 3, deep)
            'Application.DoEvents()
            CheckLost(id_bd, 4, deep)
            'Application.DoEvents()


            If GetSetting("vodopadip", "setting", "checknulls", "1") = "1" Then
                CheckSuspect(id_bd, 3, deep)
                'Application.DoEvents()
                CheckSuspect(id_bd, 4, deep)
                'Application.DoEvents()
            End If


            CheckD24H(id_bd, deep)

            Dim query As String
            query = ""
            query = query + " update  ANALIZER set LASTWHOLECHECK=sysdate"
            query = query + " where ID_BD=" + id_bd.ToString

            tvmain.QueryExec(query)
        End If
        Return True
    End Function


    Private Sub CheckMissing(ByVal id As Integer)
        'Dim dt As DataTable
        'Dim i As Integer
        'Dim dat As DateTime
        'Dim ok As Boolean

        ' drop old data
        tvmain.QueryExec("delete from missingarch where   id_bd= " + id.ToString()) ' + " and archdate < SYSDATE-40")

        'dt = tvmain.QuerySelect("select * from missingarch where DEVNAME like '%Часовой' and id_bd= " + id.ToString() + " and archdate>=SYSDATE-40")

        'For i = 0 To dt.Rows.Count - 1
        '    dat = dt.Rows(i)("ARCHDATE")
        '    ok = False
        '    If tvmain.CheckForArch(3, dat.Year, dat.Month, dat.Day, dat.Hour, id) Then
        '        ok = True
        '    End If


        '    If ok Then
        '        tvmain.QueryExec("delete from missingarch where  DEVNAME like '%Часовой' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dat))
        '    End If
        'Next


        'dt = tvmain.QuerySelect("select * from missingarch where DEVNAME like '%Суточный' and id_bd= " + id.ToString() + " and archdate>=SYSDATE-31")

        'For i = 0 To dt.Rows.Count - 1
        '    dat = dt.Rows(i)("ARCHDATE")
        '    ok = False

        '    If dat.Hour > 20 Then
        '        dat.AddHours(4)
        '    End If
        '    If tvmain.CheckForArch(4, dat.Year, dat.Month, dat.Day, 0, id) Then
        '        ok = True
        '    End If

        '    If ok Then
        '        tvmain.QueryExec("delete from missingarch where  DEVNAME like '%Суточный' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dat))
        '    End If
        'Next
    End Sub

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Private Sub CheckLost(ByVal id As Integer, ByVal ptype As Integer, ByVal deep As Integer)
        Dim dt As DataTable
        Dim dtcheck As DataTable
        dt = tvmain.QuerySelect("select dcounter from  datacurr where id_bd= " + id.ToString() + " and id_ptype=" + ptype.ToString + " and dcounter>=SYSDATE-" + deep.ToString + " order by dcounter")
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim dcur As DateTime
        Dim dprev As DateTime
        If dt.Rows.Count > 0 Then
            dprev = dt.Rows(0)("DCOUNTER")
            For i = 1 To dt.Rows.Count - 1
                dcur = dt.Rows(i)("DCOUNTER")

                If ptype = 3 Then
                    j = DateDiff(DateInterval.Hour, dprev, dcur)
                    If j > 1 Then
                        For k = 1 To j - 1
                            ' save to missing archive
                            dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '% Часовой' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dprev.AddHours(k)))
                            If dtcheck.Rows(0)("cnt") = 0 Then
                                tvmain.QueryExec("insert into missingarch(id_bd,ARCHDATE,DEVNAME) values(" + id_bd.ToString + "," + OracleDate(dprev.AddHours(k)) + ",'" + NodeName + "; Часовой')")
                            End If
                        Next


                    End If
                End If


                If ptype = 4 Then
                    j = DateDiff(DateInterval.Day, dprev, dcur)
                    If j > 1 Then
                        For k = 1 To j - 1
                            ' save to missing archive
                            dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '%Суточный' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dprev.AddHours(k)))
                            If dtcheck.Rows(0)("cnt") = 0 Then
                                tvmain.QueryExec("insert into missingarch(id_bd,ARCHDATE,DEVNAME) values(" + id_bd.ToString + "," + OracleDate(dprev.AddDays(k)) + ",'" + NodeName + ";Суточный')")
                            End If
                        Next
                    End If
                End If
                dprev = dcur
                'Application.DoEvents()
            Next

            dcur = tvmain.GetDBDateTime
            If ptype = 3 Then
                j = DateDiff(DateInterval.Hour, dprev, dcur)
                If j > 1 Then
                    For k = 1 To j - 1
                        ' save to missing archive
                        dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '% Часовой' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dprev.AddHours(k)))
                        If dtcheck.Rows(0)("cnt") = 0 Then
                            tvmain.QueryExec("insert into missingarch(id_bd,ARCHDATE,DEVNAME) values(" + id_bd.ToString + "," + OracleDate(dprev.AddHours(k)) + ",'" + NodeName + "; Часовой')")
                        End If
                        'Application.DoEvents()
                    Next


                End If
            End If

            If ptype = 4 Then
                j = DateDiff(DateInterval.Day, dprev, dcur)
                If j > 1 Then
                    For k = 1 To j - 1
                        ' save to missing archive
                        dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '% Суточный' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dprev.AddHours(k)))
                        If dtcheck.Rows(0)("cnt") = 0 Then
                            tvmain.QueryExec("insert into missingarch(id_bd,ARCHDATE,DEVNAME) values(" + id_bd.ToString + "," + OracleDate(dprev.AddDays(k)) + ",'" + NodeName + "; Суточный')")
                        End If
                        'Application.DoEvents()
                    Next
                End If
            End If

        End If


    End Sub



    Private Sub CheckSuspect(ByVal id As Integer, ByVal ptype As Integer, ByVal deep As Integer)
        Dim dt As DataTable
        Dim dtcheck As DataTable
        Dim dtVerify As DataTable
        Dim vcol As String = ""
        Dim ww As String

        Try
            dtVerify = tvmain.QuerySelect("select * from  bdevices  where id_bd= " + id.ToString())
            vcol = dtVerify.Rows(0)("nzcols").ToString().ToUpper()
        Catch ex As Exception
            vcol = ""
        End Try
        

        ww = " (1=0) "


        Dim carr() As String
        carr = Split(vcol, ";")

        Dim i As Integer
        Try

            For i = 0 To carr.GetUpperBound(0)
                If carr(i).Trim <> "" Then
                    ww = ww + " or (" + carr(i) + "=0 or " + carr(i) + " is null) "
                End If
            Next

        Catch ex As Exception

        End Try

        Try
            dt = tvmain.QuerySelect("select dcounter from  datacurr where (  " + ww + "  ) and id_bd= " + id.ToString() + " and id_ptype=" + ptype.ToString + " and dcounter>=SYSDATE-" + deep.ToString() + " order by dcounter")
        Catch ex As Exception
            dt = New DataTable
        End Try

        



        Dim dcur As DateTime
        'Dim dprev As DateTime
        If dt.Rows.Count > 0 Then
            'dprev = dt.Rows(0)("DCOUNTER")
            For i = 0 To dt.Rows.Count - 1
                dcur = dt.Rows(i)("DCOUNTER")

                If ptype = 3 Then
                    dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '%Часовой' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dcur))
                    If dtcheck.Rows(0)("cnt") = 0 Then
                        tvmain.QueryExec("insert into missingarch(id_bd,ARCHDATE,DEVNAME) values(" + id_bd.ToString + "," + OracleDate(dcur) + ",'" + NodeName + "; НУЛИ; Часовой')")
                    End If

                End If

                If ptype = 4 Then
                    dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '%Суточный' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dcur))
                    If dtcheck.Rows(0)("cnt") = 0 Then
                        tvmain.QueryExec("insert into missingarch(id_bd,ARCHDATE,DEVNAME) values(" + id_bd.ToString + "," + OracleDate(dcur) + ",'" + NodeName + "; НУЛИ; Суточный')")
                    End If
                End If
            Next

        End If


    End Sub

    Public Sub CheckD24H(ByVal nid_bd As Integer, ByVal deep As Integer)
        id_bd = nid_bd
        For j = 1 To deep
            If Not VerifyD24H(Today.AddDays(-j)) Then


                Dim a As String
                Dim a1 As String

                a1 = "Несоответствие часовых и суточных данных за " + Format(Today.AddDays(-j), "dd.MM.yyyy")
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
                'tvmain.QueryExec("Update Analizer set HCCOUNT=HCCOUNT+1 where id_bd=" & id_bd.ToString)
                'tvmain.QueryExec("Update Analizer set color='YELLOW' where (COLOR <> 'RED' or color is null) and id_bd=" & id_bd.ToString)
                'tvmain.QueryExec("Update Analizer set INFO=INFO||'" & vbCrLf & a1 & "' where  id_bd=" & id_bd.ToString)

            End If
    
        Next
    End Sub


End Class

