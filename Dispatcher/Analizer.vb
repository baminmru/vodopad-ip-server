Imports System.Data.OracleClient





Public Class LostAnalizer
    Public tvmain As STKTVMain.TVMain
    Public id_bd As Integer
    Public NodeName As String
   



    Public Function AnalizeLost() As Boolean
        Dim dt As DataTable
        dt = tvmain.QuerySelect("select count(*) cnt  from plancall join bdevices on plancall.id_bd=bdevices.id_bd " & _
         " where bdevices.id_bd=" + id_bd.ToString + " and ( bdevices.npquery = 1 Or plancall.Cstatus = 0 )")

        Dim i As Integer = 0

        i = dt.Rows(0)("cnt")
        If i <> 0 Then
            CheckMissing(id_bd)
            CheckLost(id_bd, 3)
            CheckLost(id_bd, 4)
            If GetSetting("vodopadip", "setting", "checknulls", "1") = "1" Then
                CheckSuspect(id_bd, 3)
                CheckSuspect(id_bd, 4)
            End If

        End If

    End Function

    Private Sub CheckMissing(ByVal id As Integer)
        Dim dt As DataTable
        Dim i As Integer
        Dim dat As DateTime
        Dim ok As Boolean

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

    Private Sub CheckLost(ByVal id As Integer, ByVal ptype As Integer)
        Dim dt As DataTable
        Dim dtcheck As DataTable
        dt = tvmain.QuerySelect("select dcounter from  datacurr where id_bd= " + id.ToString() + " and id_ptype=" + ptype.ToString + " and dcounter>=SYSDATE-31 order by dcounter")
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
                    Next
                End If
            End If
         
        End If


    End Sub



    Private Sub CheckSuspect(ByVal id As Integer, ByVal ptype As Integer)
        Dim dt As DataTable
        Dim dtcheck As DataTable
        dt = tvmain.QuerySelect("select dcounter from  datacurr where  T1=0 AND  t2=0 AND  m1=0 AND  m2=0 AND  m4=0 AND m5=0 and id_bd= " + id.ToString() + " and id_ptype=" + ptype.ToString + " and dcounter>=SYSDATE-31 order by dcounter")
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
                    dtcheck = tvmain.QuerySelect("select count(*) cnt from missingarch where DEVNAME like '% Часовой' and id_bd= " + id.ToString() + " and archdate=" + OracleDate(dcur))
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




End Class

