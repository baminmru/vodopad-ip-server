Public Class NodeAnalizer
    
    Private m_tvmain As STKTVMain.TVMain


    Public Sub AnalizeNode(tvmain As STKTVMain.TVMain, id_bd As Integer, ByVal deep As Integer, ByVal IgnoreTime As Boolean)
        Dim NodeName As String = ""
        Dim dt As DataTable
        Dim dt2 As DataTable
        m_tvmain = tvmain
        dt = tvmain.QuerySelect("select CSHORT NodeName from  bbuildings b join bdevices d on b.id_bu= d.id_bu and d.id_bd=" + id_bd.ToString)
        dt2 = tvmain.QuerySelect("select t_method,sezon from  analizer_cfg where id_bd=" + id_bd.ToString)
        Dim t_method As Integer
        Dim sezon As Integer = 0
        If dt2.Rows.Count > 0 Then
            Try
                sezon = dt2.Rows(0)("sezon")
                t_method = dt2.Rows(0)("t_method")
            Catch ex As Exception
                t_method = 0
                sezon = 0
            End Try

        Else
            t_method = 0
            sezon = 0
        End If

        ErazeAnalizer(id_bd)

        If dt.Rows.Count > 0 Then
            NodeName = dt.Rows(0)("NodeName") & ""
        End If
        Dim an As VIPAnalizer.NCAnalizer
        Try
            an = New VIPAnalizer.NCAnalizer
            an.tvmain = tvmain
            an.id_bd = id_bd
            an.NodeName = NodeName
            If IgnoreTime Or (Hour(DateTime.Now) >= 5 And Hour(DateTime.Now) <= 7) Then
                an.AnalizeDayNC(ApplicationTypeEnum.AppAuto, 2 * 24, sezon, t_method)
            End If

            an.AnalizeHourNC(ApplicationTypeEnum.AppAuto, 2, sezon, True)
            an.AnalizeMomentNC(ApplicationTypeEnum.AppAuto, 2, sezon, True)
            an = Nothing

        Catch ex As Exception

        End Try

        Try
            
            If IgnoreTime Or (Hour(DateTime.Now) >= 19 And Hour(DateTime.Now) <= 21) Then
            Dim lan As LostAnalizer
            lan = New LostAnalizer
            lan.tvmain = tvmain
            lan.id_bd = id_bd
            lan.NodeName = NodeName
            lan.AnalizeLost(deep)
            lan = Nothing
            End If


        Catch ex As Exception

        End Try


        ' проверка скачков
        AnalizeJumps(id_bd, 1, NodeName)

        ' установка флага проверки архива
        Dim q As String

        q = "update datacurr set check_a=1 where id_bd=" + id_bd.ToString + " and id_ptype=3 and dcounter>(sysdate-6/24) and (check_a <> 1 or check_a is null)"
        Try
            tvmain.QueryExec(q)
        Catch ex As Exception
            ' Exit Do
        End Try

        CheckStatus(Nothing, id_bd, deep, 2)


    End Sub



    Private Function GetLastHourArchive(ByVal id_bd As Integer) As DataRow
        Dim dt As DataTable
        Dim dcounter As Date
        If m_tvmain Is Nothing Then Return Nothing
        dt = m_tvmain.QuerySelect("select max(dcounter) dcounter from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=3")
        If dt.Rows.Count > 0 Then
            Try
                dcounter = dt.Rows(0)("dcounter")
            Catch ex As Exception
                Return Nothing
            End Try

        Else
            Return Nothing
        End If
        dt = m_tvmain.QuerySelect("select *from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=3 and dcounter>=" & m_tvmain.OracleDate(dcounter))
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Function GetLastMomentArchive(ByVal id_bd As Integer) As DataRow
        Dim dt As DataTable
        Dim dcounter As Date
        If m_tvmain Is Nothing Then Return Nothing
        'dt = m_tvmain.QuerySelect("select max(dcounter) dcounter from datacurr where id_bd=" + id_bd.ToString + " and t1 is not null and id_ptype=1")
        dt = m_tvmain.QuerySelect("select max(dcounter) dcounter from datacurr where id_bd=" + id_bd.ToString + "  and id_ptype=1")
        If dt.Rows.Count > 0 Then
            Try
                dcounter = dt.Rows(0)("dcounter")
            Catch ex As Exception
                Return Nothing
            End Try
        Else
            Return Nothing
        End If

        dt = m_tvmain.QuerySelect("select * from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=1 and dcounter<=" & m_tvmain.OracleDate(dcounter) + " and dcounter>=" & m_tvmain.OracleDate(dcounter.AddDays(-10)) + " order by dcounter desc")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1

            If Not IsDBNull(dt.Rows(i)("t1")) Then
                Return dt.Rows(i)
        End If

        Next
        
        Return Nothing

    End Function

    Private Function GetPrevHourArchive(ByVal id_bd As Integer, ByVal dcounter As DateTime) As DataRow
        Dim dt As DataTable
        Dim dcounter2 As Date
        If m_tvmain Is Nothing Then Return Nothing
        dt = m_tvmain.QuerySelect("select max(dcounter) dcounter from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=3 and dcounter<" & m_tvmain.OracleDate(dcounter))
        If dt.Rows.Count > 0 Then
            Try
                dcounter2 = dt.Rows(0)("dcounter")
            Catch ex As Exception
                Return Nothing
            End Try

        Else
            Return Nothing
        End If
        dt = m_tvmain.QuerySelect("select *from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=3 and dcounter=" & m_tvmain.OracleDate(dcounter2))
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Private Function GetPrevMomentArchive(ByVal id_bd As Integer, ByVal dcounter As DateTime) As DataRow
        Dim dt As DataTable
        Dim dcounter2 As Date
        If m_tvmain Is Nothing Then Return Nothing
        'dt = m_tvmain.QuerySelect("select max(dcounter) dcounter from datacurr where id_bd=" + id_bd.ToString + " and t1 is not null and  id_ptype=1 and dcounter<" & m_tvmain.OracleDate(dcounter))
        dt = m_tvmain.QuerySelect("select max(dcounter) dcounter from datacurr where id_bd=" + id_bd.ToString + " and   id_ptype=1 and dcounter<" & m_tvmain.OracleDate(dcounter))
        If dt.Rows.Count > 0 Then
            Try
                dcounter2 = dt.Rows(0)("dcounter")
            Catch ex As Exception
                Return Nothing
            End Try

        Else
            Return Nothing
        End If


        dt = m_tvmain.QuerySelect("select * from datacurr where id_bd=" + id_bd.ToString + " and id_ptype=1 and dcounter<=" & m_tvmain.OracleDate(dcounter2) + " and dcounter>=" & m_tvmain.OracleDate(dcounter2.AddDays(-10)) + " order by dcounter desc")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1

            If Not IsDBNull(dt.Rows(i)("t1")) Then
                Return dt.Rows(i)
        End If

        Next

        Return Nothing


        'dt = m_tvmain.QuerySelect("select * from datacurr where id_bd=" + id_bd.ToString + " and t1 is not null and  id_ptype=1 and dcounter=" & m_tvmain.OracleDate(dcounter2))
        'If dt.Rows.Count > 0 Then
        '    Return dt.Rows(0)
        'Else
        '    Return Nothing
        'End If
    End Function


    Private Function GetT(ByVal dr As DataRow, ByVal tidx As Integer) As Double
        Try
            If tidx > 0 And tidx < 6 Then
                If TypeName(dr("T" + tidx.ToString)) <> "DBNull" Then
                    Return dr("T" + tidx.ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
       
    End Function

    Private Function GetP(ByVal isHour As Boolean, ByVal dr As DataRow, ByVal pidx As Integer) As Double
        Dim n As String
        'If isHour Then
        n = "P"
        'Else
        '    n = "M_P"
        'End If
        Try
            If pidx > 0 And pidx < 6 Then
                If TypeName(dr(n + pidx.ToString)) <> "DBNull" Then
                    Return dr(n + pidx.ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function


    Private Function GetG(ByVal isHour As Boolean, ByVal dr As DataRow, ByVal gidx As Integer) As Double
        Dim n As String
        'If isHour Then
        n = "G"
        'Else
        '    n = "M_G"
        'End If
        Try
            If gidx > 0 And gidx < 6 Then
                If TypeName(dr(n + gidx.ToString)) <> "DBNull" Then
                    Return dr(n + gidx.ToString)
                Else
                    Return 0
                End If

            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function


    Private Function GetM(ByVal isHour As Boolean, ByVal dr As DataRow, ByVal gidx As Integer) As Double
        Dim n As String
        'If isHour Then
        n = "M"
        'Else
        '    n = "M_M"
        'End If
        Try
            If gidx > 0 And gidx < 6 Then
                If TypeName(dr(n + gidx.ToString)) <> "DBNull" Then
                    Return dr(n + gidx.ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function




    Private Function GetV(ByVal isHour As Boolean, ByVal dr As DataRow, ByVal gidx As Integer) As Double
        Dim n As String
        'If isHour Then
        n = "V"
        'Else
        'n = "M_V"
        'End If
        Try
            If gidx > 0 And gidx < 6 Then
                If TypeName(dr(n + gidx.ToString)) <> "DBNull" Then
                    Return dr(n + gidx.ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Private Sub ErazeAnalizer(ByVal id_bd As Integer)
        Dim dt As DataTable
        dt = m_tvmain.QuerySelect("select * from analizer where id_bd=" + id_bd.ToString())
        If dt.Rows.Count = 0 Then
            m_tvmain.QueryExec("insert into analizer (ID_BD ,DLAST ,LASTLINK ,LASTWHOLECHECK ,HMISSING ,DMISSING ,HCCOUNT ,STATUS ,LINKERRORS ,PERROR ,TERROR ,GERROR ,COLOR ,INFO) values( " + id_bd.ToString() + ",sysdate-100,sysdate-100,sysdate-100,0,0,0,0,0,0,0,0,'','')")
        End If

        'm_tvmain.QueryExec("insert into ANALIZER_HIST( ID_BD ,DLAST ,LASTLINK ,LASTWHOLECHECK ,HMISSING ,DMISSING ,HCCOUNT ,STATUS ,LINKERRORS ,PERROR ,TERROR ,GERROR ,COLOR ,INFO )select  ID_BD ,DLAST ,LASTLINK ,LASTWHOLECHECK ,HMISSING ,DMISSING ,HCCOUNT ,STATUS ,LINKERRORS ,PERROR ,TERROR ,GERROR ,COLOR ,INFO  from analizer where id_bd=" & id_bd.ToString)
        m_tvmain.QueryExec("Update Analizer set DLAST=SYSDATE, HMISSING =0 ,DMISSING =0 ,HCCOUNT =0 ,STATUS =0 ,LINKERRORS =0 ,PERROR =0 ,TERROR =0 ,GERROR =0 ,COLOR ='' ,INFO ='' where id_bd=" & id_bd.ToString)
    End Sub


    Private Sub AppendAnalizerInfo(ByVal id_bd As Integer, ByVal s As String, ByVal sColor As String)
        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & "" + s + "', color='" + sColor + "' where id_bd=" & id_bd.ToString)
    End Sub
    Public Sub AnalizeJumps(ByVal id_bd As Integer, ByVal deep As Integer, ByVal NodeName As String)
        Dim aLast As DataRow
        Dim aPrev As DataRow
        Dim acfg As DataTable
        acfg = m_tvmain.QuerySelect("select * from analizer_cfg where id_bd=" + id_bd.ToString)
        If acfg.Rows.Count = 0 Then
            m_tvmain.QueryExec("insert into analizer_cfg ( ID_BD , ANALIZENODE, OPENSYSTEM , T1 , T2 , T3 , T4 , T5 , T6 , V1 , V2 , V3 , V4 , V5 , V6 , M1 , M2 , M3 , M4 , M5 , M6 , P1 , P2 , P3 , P4 , P5 , P6 , G1 , G2 , G3 , G4 , G5 , G6 , Q1 , Q2 , Q3 , Q4 , Q5 , K0 , K1 , K2 , K3 , K4 , K5 )  values ( " & id_bd.ToString() & ", 0 ,0  ,0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0,0  ,0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0  ,1,1,1,1,1,1)")
            AppendAnalizerInfo(id_bd, "Не задана конфигурация анализатора", "")
            Exit Sub
        End If

        If acfg.Rows(0)("ANALIZENODE") = 1 Then


            Dim GVSMax As Double
            Dim cn As DataTable
            Dim ok As Boolean
            GVSMax = 0
            cn = m_tvmain.QuerySelect("select * from contract where id_bd=" + id_bd.ToString)
            If cn.Rows.Count > 0 Then
                If Double.TryParse(cn.Rows(0)("fld21").ToString.Replace(",", "."), GVSMax) Then
                    ok = True
                ElseIf Double.TryParse(cn.Rows(0)("fld21").ToString.Replace(".", ","), GVSMax) Then
                    ok = True
                Else
                    ok = False
                End If
            Else
                ok = False
            End If

            If ok And GVSMax <= 0 Then
                ok = False
            End If

            aLast = GetLastMomentArchive(id_bd)
            If aLast Is Nothing Then
                Dim dtVkt4 As DataTable
                dtVkt4 = m_tvmain.QuerySelect("select count(*) cnt from bdevices where id_dev in (15) and id_bd=" + id_bd.ToString)
                If dtVkt4.Rows(0)("cnt") = 0 Then
                    AppendAnalizerInfo(id_bd, "Не обнаружены мгновенные архивы", "YELLOW")
                End If
            End If
            Dim a_ok As Boolean = False
            Dim dtmp As Date
            If Not aLast Is Nothing Then
                If IsDBNull(aLast("check_a")) Then
                    a_ok = True
                ElseIf aLast("check_a") <> 1 Then
                    a_ok = True
                End If
                If a_ok Then
                    AnalizeLast(id_bd, aLast, acfg.Rows(0), NodeName, False)
                End If




                dtmp = aLast("DCOUNTER")
                aPrev = GetPrevMomentArchive(id_bd, dtmp)
                If a_ok And Not aPrev Is Nothing Then

                  
                    AnalizePair(id_bd, aLast, aPrev, acfg.Rows(0), NodeName, False, GVSMax, ok)


                End If
            End If



            aLast = GetLastHourArchive(id_bd)
            If aLast Is Nothing Then

                Dim checkdt As DataTable

                checkdt = m_tvmain.QuerySelect("select * from plancall where id_bd=" + id_bd.ToString())
                If checkdt.Rows.Count > 0 Then
                    If checkdt.Rows(0)("CHOUR") = 1 Then
                        AppendAnalizerInfo(id_bd, "Не обнаружены часовые архивы", "YELLOW")

                    End If
                End If
                Exit Sub
            End If

            If IsDBNull(aLast("check_a")) Then
                a_ok = True
            ElseIf aLast("check_a") <> 1 Then
                a_ok = True
            End If
            If a_ok Then
                AnalizeLast(id_bd, aLast, acfg.Rows(0), NodeName, True)
            End If

            While deep
                deep -= 1
                If Not aLast Is Nothing Then
                    dtmp = aLast("DCOUNTER")
                    aPrev = GetPrevHourArchive(id_bd, dtmp)
                    If a_ok And Not aPrev Is Nothing Then

                        AnalizePair(id_bd, aLast, aPrev, acfg.Rows(0), NodeName, True, GVSMax, ok)
                        aLast = aPrev

                    End If
                End If


            End While
        End If

    End Sub


    Public Sub WriteNC(ByVal SaveAppType As ApplicationTypeEnum, ByVal Message As String, ByVal dt As Date, ByVal id_bd As String, ByVal NodeName As String)
        Dim a As String

        a = "insert into hcmessages (id,id_bd,id_ip_got,msg_type,msg_text,was_reg,dt_got,node_name,appl_type) values(" + _
             "hcmessages_seq.nextval," + _
        id_bd.ToString() + "," + _
        "null" + "," + _
        "1," + _
        "'" + Message + "'," + _
        "0," + _
        "sysdate," + _
        "'" + NodeName + "',0)"
        m_tvmain.QueryExec(a)
    End Sub

    Private Sub AnalizeLast(ByVal id_bd As Integer, ByVal aLast As DataRow, ByVal aCfg As DataRow, ByVal nodename As String, ByVal isHour As Boolean)
        Dim isOpen As Boolean
        Dim prefix As String
        Dim cfgprefix As String
        If aCfg("SEZON") = 0 Then
            If isHour Then
                prefix = "Часовые. "
                cfgprefix = ""
            Else
                prefix = "Мгновенные. "
                cfgprefix = "m_"
            End If
        Else
            If isHour Then
                prefix = "Часовые. "
                cfgprefix = "L_"
            Else
                prefix = "Мгновенные. "
                cfgprefix = "L_m_"
            End If
        End If

        If aCfg("OpenSystem") = 1 Then isOpen = True Else isOpen = False

        ' проверяем разницу дат  на вычислителе и дату  на сервере
        If isHour = False Then
            If Not aLast Is Nothing Then
                If Math.Abs(DateDiff(DateInterval.Hour, aLast("DCALL"), aLast("DCOUNTER"))) > 2 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Ошибка даты вычислителя!', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                    Try
                        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Ошибка даты вычислителя!", aLast("DCOUNTER"), id_bd, nodename)
                    Catch ex As Exception

                    End Try
                End If
            Else
                Exit Sub
            End If
        End If


        ' давление между каналами

        If aCfg(cfgprefix + "p1") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p1")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p1").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                Try
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p1").ToString, aLast("DCOUNTER"), id_bd, nodename)
                Catch ex As Exception

                End Try

            End If
        End If


        If aCfg(cfgprefix + "p2") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p2")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p2").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p2").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If


        If aCfg(cfgprefix + "p3") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p3")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p3").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p3").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If


        If aCfg(cfgprefix + "p4") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p4")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p4").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p4").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If

        If aCfg(cfgprefix + "p5") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p5")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p5").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p5").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If


        If aCfg(cfgprefix + "p6") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p6")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p6").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p6").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If



        If aCfg(cfgprefix + "p1") > 0 And aCfg(cfgprefix + "p2") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p2")) >= GetP(isHour, aLast, aCfg(cfgprefix + "p1")) Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница давлений между каналами " + aCfg(cfgprefix + "p1").ToString + " и " + aCfg(cfgprefix + "p2").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница давлений между каналами " + aCfg(cfgprefix + "p1").ToString + " и " + aCfg(cfgprefix + "p2").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If



        If aCfg(cfgprefix + "p3") > 0 And aCfg(cfgprefix + "p4") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p4")) >= GetP(isHour, aLast, aCfg(cfgprefix + "p3")) Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница давлений между каналами " + aCfg(cfgprefix + "p3").ToString + " и " + aCfg(cfgprefix + "p4").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница давлений между каналами " + aCfg(cfgprefix + "p3").ToString + " и " + aCfg(cfgprefix + "p4").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If



        If aCfg(cfgprefix + "p5") > 0 And aCfg(cfgprefix + "p6") > 0 Then
            If GetP(isHour, aLast, aCfg(cfgprefix + "p5")) >= GetP(isHour, aLast, aCfg(cfgprefix + "p6")) Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница давлений между каналами " + aCfg(cfgprefix + "p4").ToString + " и " + aCfg(cfgprefix + "p5").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница давлений между каналами " + aCfg(cfgprefix + "p5").ToString + " и " + aCfg(cfgprefix + "p6").ToString, aLast("DCOUNTER"), id_bd, nodename)
            End If
        End If



    End Sub


    Private Function aCfgSezon(ByRef acfg As DataRow, ByVal s As String) As Double
        Try
            If acfg("SEZON") = 0 Then
                Return acfg(s)
            Else
                Return acfg("l" + s)
            End If
        Catch ex As Exception

        End Try

        Return "0"
    End Function

    Private Sub AnalizePair(ByVal id_bd As Integer, ByVal aLast As DataRow, ByVal aPrev As DataRow, ByVal aCfg As DataRow, ByVal NodeNAME As String, ByVal isHour As Boolean, ByVal GVSMax As Double, ByVal UseGVSMax As Boolean)
        Dim isOpen As Boolean
        Dim prefix As String
        Dim cfgprefix As String
        If aCfg("SEZON") = 0 Then
            If isHour Then
                prefix = "Часовые. "
                cfgprefix = ""
            Else
                prefix = "Мгновенные. "
                cfgprefix = "m_"
            End If
        Else
            If isHour Then
                prefix = "Часовые. "
                cfgprefix = "L_"
            Else
                prefix = "Мгновенные. "
                cfgprefix = "L_m_"
            End If
        End If
        If aCfg("OpenSystem") = 1 Then isOpen = True Else isOpen = False

        ' давление между циклами
        If aCfg(cfgprefix + "p1") > 0 Then
            If GetP(isHour, aPrev, aCfg(cfgprefix + "p1")) - GetP(isHour, aLast, aCfg(cfgprefix + "p1")) >= 1 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p1").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p1")) < 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p1").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p1")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p1").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg(cfgprefix + "p2") > 0 Then
            If GetP(isHour, aPrev, aCfg(cfgprefix + "p2")) - GetP(isHour, aLast, aCfg(cfgprefix + "p2")) >= 1 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p2").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p2")) < 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p2").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p2")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p2").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg(cfgprefix + "p3") > 0 Then
            If GetP(isHour, aPrev, aCfg(cfgprefix + "p3")) - GetP(isHour, aLast, aCfg(cfgprefix + "p3")) >= 1 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p3").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p3")) < 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p3").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p3")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p3").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If

        End If

        If aCfg(cfgprefix + "p4") > 0 Then
            If GetP(isHour, aPrev, aCfg(cfgprefix + "p4")) - GetP(isHour, aLast, aCfg(cfgprefix + "p4")) >= 1 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p4").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p4")) < 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p4").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p4")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p4").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If

        End If

        If aCfg(cfgprefix + "p5") > 0 Then
            If GetP(isHour, aPrev, aCfg(cfgprefix + "p5")) - GetP(isHour, aLast, aCfg(cfgprefix + "p5")) >= 1 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p5").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p5")) < 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p5").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p5")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p5").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If


        If aCfg(cfgprefix + "p6") > 0 Then
            If GetP(isHour, aPrev, aCfg(cfgprefix + "p6")) - GetP(isHour, aLast, aCfg(cfgprefix + "p6")) >= 1 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p6").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок давления по каналу" + aCfg(cfgprefix + "p6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p6")) < 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p6").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Отрицательное давление по каналу " + aCfg(cfgprefix + "p6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
            If GetP(isHour, aLast, aCfg(cfgprefix + "p6")) = 0 Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p6").ToString + "', color='RED',PERROR=PERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевое давление по каналу " + aCfg(cfgprefix + "p6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If


        ' Температура между циклами
        If aCfg("t1") > 0 Then
            If GetT(aPrev, aCfg("t1")) - GetT(aLast, aCfg("t1")) >= aCfgSezon(aCfg, "k2") Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок Температуры по каналу" + aCfg("t1").ToString + "', color='RED',TERROR=TERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок Температуры по каналу" + aCfg("t1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg("t2") > 0 Then
            If GetT(aPrev, aCfg("t2")) - GetT(aLast, aCfg("t2")) >= aCfgSezon(aCfg, "k2") Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок Температуры по каналу" + aCfg("t2").ToString + "', color='RED',TERROR=TERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок Температуры по каналу" + aCfg("t2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg("t3") > 0 Then
            If GetT(aPrev, aCfg("t3")) - GetT(aLast, aCfg("t3")) >= aCfgSezon(aCfg, "k2") Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок Температуры по каналу" + aCfg("t3").ToString + "', color='RED',TERROR=TERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок Температуры по каналу" + aCfg("t3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg("t4") > 0 Then
            If GetT(aPrev, aCfg("t4")) - GetT(aLast, aCfg("t4")) >= aCfgSezon(aCfg, "k2") Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок Температуры по каналу" + aCfg("t4").ToString + "', color='RED',TERROR=TERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок Температуры по каналу" + aCfg("t4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg("t5") > 0 Then
            If GetT(aPrev, aCfg("t5")) - GetT(aLast, aCfg("t5")) >= aCfgSezon(aCfg, "k2") Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок Температуры по каналу" + aCfg("t5").ToString + "', color='RED',TERROR=TERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок Температуры по каналу" + aCfg("t5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        If aCfg("t6") > 0 Then
            If GetT(aPrev, aCfg("t6")) - GetT(aLast, aCfg("t6")) >= aCfgSezon(aCfg, "k2") Then
                m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачок Температуры по каналу" + aCfg("t6").ToString + "', color='RED',TERROR=TERROR+1 where id_bd=" & id_bd.ToString)
                WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачок Температуры по каналу" + aCfg("t6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
            End If
        End If

        Dim VPrev1 As Double, VPrev2 As Double, VLast1 As Double, VLast2 As Double
        '''''''''''''''''''''''''' РАСХОДЫ G
        If isOpen Then
            If aCfg(cfgprefix + "g1") > 0 And aCfg(cfgprefix + "g2") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g1"))
                VPrev2 = GetG(isHour, aPrev, aCfg(cfgprefix + "g2"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g1"))
                VLast2 = GetG(isHour, aLast, aCfg(cfgprefix + "g2"))

                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                'If UseGVSMax Then
                '    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(acfg,"k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                '        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом G " + aCfg(cfgprefix + "g1").ToString + " и " + aCfg(cfgprefix + "g2").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                '        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом G " + aCfg(cfgprefix + "g1").ToString + " и " + aCfg(cfgprefix + "g2").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                '    End If
                'End If
            End If
        Else
            If aCfg(cfgprefix + "g1") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g1"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g1"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "g2") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g2"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g2"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If

        If isOpen Then
            If aCfg(cfgprefix + "g3") > 0 And aCfg(cfgprefix + "g4") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g3"))
                VPrev2 = GetG(isHour, aPrev, aCfg(cfgprefix + "g4"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g3"))
                VLast2 = GetG(isHour, aLast, aCfg(cfgprefix + "g4"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                'If UseGVSMax Then
                '    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(acfg,"k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                '        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом G " + aCfg(cfgprefix + "g3").ToString + " и " + aCfg(cfgprefix + "g4").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                '        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом G " + aCfg(cfgprefix + "g3").ToString + " и " + aCfg(cfgprefix + "g4").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                '    End If
                'End If
            End If
        Else
            If aCfg(cfgprefix + "g3") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g3"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g3"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "g4") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g4"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g4"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If




        If isOpen Then
            If aCfg(cfgprefix + "g5") > 0 And aCfg(cfgprefix + "g6") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g5"))
                VPrev2 = GetG(isHour, aPrev, aCfg(cfgprefix + "g6"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g5"))
                VLast2 = GetG(isHour, aLast, aCfg(cfgprefix + "g6"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами G " + aCfg(cfgprefix + "g6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                'If UseGVSMax Then
                '    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(aCfg, "k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                '        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом G " + aCfg(cfgprefix + "g5").ToString + " и " + aCfg(cfgprefix + "g6").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                '        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом G " + aCfg(cfgprefix + "g5").ToString + " и " + aCfg(cfgprefix + "g6").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                '    End If
                'End If
            End If
        Else
            If aCfg(cfgprefix + "g5") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g5"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g5"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "g6") > 0 Then
                VPrev1 = GetG(isHour, aPrev, aCfg(cfgprefix + "g6"))
                VLast1 = GetG(isHour, aLast, aCfg(cfgprefix + "g6"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход G " + aCfg(cfgprefix + "g6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход G " + aCfg(cfgprefix + "g6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами G" + aCfg(cfgprefix + "g6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If

        ''''''''''''''''''''''''''''''''''''''' массовые расходы

        If isOpen Then
            If aCfg(cfgprefix + "m1") > 0 And aCfg(cfgprefix + "m2") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m1"))
                VPrev2 = GetM(isHour, aPrev, aCfg(cfgprefix + "m2"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m1"))
                VLast2 = GetM(isHour, aLast, aCfg(cfgprefix + "m2"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами M " + aCfg(cfgprefix + "m1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами М " + aCfg(cfgprefix + "m1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами M " + aCfg(cfgprefix + "m2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами М " + aCfg(cfgprefix + "m2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                'If UseGVSMax Then
                '    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(acfg,"k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                '        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом M " + aCfg(cfgprefix + "m1").ToString + " и " + aCfg(cfgprefix + "m2").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                '        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом M " + aCfg(cfgprefix + "m1").ToString + " и " + aCfg(cfgprefix + "m2").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                '    End If
                'End If
            End If
        Else
            If aCfg(cfgprefix + "m1") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m1"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m1"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VPrev2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "m2") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m2"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m2"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VPrev2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If

        If isOpen Then
            If aCfg(cfgprefix + "m3") > 0 And aCfg(cfgprefix + "m4") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m3"))
                VPrev2 = GetM(isHour, aPrev, aCfg(cfgprefix + "m4"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m3"))
                VLast2 = GetM(isHour, aLast, aCfg(cfgprefix + "m4"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами M " + aCfg(cfgprefix + "m3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами М " + aCfg(cfgprefix + "m3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами M " + aCfg(cfgprefix + "m4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами М " + aCfg(cfgprefix + "m4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If UseGVSMax Then
                    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(aCfg, "k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом M " + aCfg(cfgprefix + "m3").ToString + " и " + aCfg(cfgprefix + "m4").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом M " + aCfg(cfgprefix + "m3").ToString + " и " + aCfg(cfgprefix + "m4").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                    End If
                End If
            End If
        Else
            If aCfg(cfgprefix + "m3") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m3"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m3"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "m4") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m4"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m4"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If




        If isOpen Then
            If aCfg(cfgprefix + "m5") > 0 And aCfg(cfgprefix + "m6") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m5"))
                VPrev2 = GetM(isHour, aPrev, aCfg(cfgprefix + "m6"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m5"))
                VLast2 = GetM(isHour, aLast, aCfg(cfgprefix + "m6"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами M " + aCfg(cfgprefix + "m5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами М " + aCfg(cfgprefix + "m5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами M " + aCfg(cfgprefix + "m6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами М " + aCfg(cfgprefix + "m6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If UseGVSMax Then
                    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(aCfg, "k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом M " + aCfg(cfgprefix + "m5").ToString + " и " + aCfg(cfgprefix + "m6").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом M " + aCfg(cfgprefix + "m5").ToString + " и " + aCfg(cfgprefix + "m6").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                    End If
                End If
            End If
        Else
            If aCfg(cfgprefix + "m5") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m5"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m5"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "m6") > 0 Then
                VPrev1 = GetM(isHour, aPrev, aCfg(cfgprefix + "m6"))
                VLast1 = GetM(isHour, aLast, aCfg(cfgprefix + "m6"))

                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход M " + aCfg(cfgprefix + "m6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход M " + aCfg(cfgprefix + "m6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами M" + aCfg(cfgprefix + "m6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If





        ''''''''''''''''''''''''''''''''''''''' объемные расходы

        If isOpen Then
            If aCfg(cfgprefix + "v1") > 0 And aCfg(cfgprefix + "v2") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v1"))
                VPrev2 = GetV(isHour, aPrev, aCfg(cfgprefix + "v2"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v1"))
                VLast2 = GetV(isHour, aLast, aCfg(cfgprefix + "v2"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If UseGVSMax Then
                    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(aCfg, "k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом V " + aCfg(cfgprefix + "v1").ToString + " и " + aCfg(cfgprefix + "v2").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом V " + aCfg(cfgprefix + "v1").ToString + " и " + aCfg(cfgprefix + "v2").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                    End If
                End If

            End If
        Else
            If aCfg(cfgprefix + "v1") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v1"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v1"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v1").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v1").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "v2") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v2"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v2"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v2").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v2").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If

        If isOpen Then
            If aCfg(cfgprefix + "v3") > 0 And aCfg(cfgprefix + "v4") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v3"))
                VPrev2 = GetV(isHour, aPrev, aCfg(cfgprefix + "v4"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v3"))
                VLast2 = GetV(isHour, aLast, aCfg(cfgprefix + "v4"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If UseGVSMax Then
                    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(aCfg, "k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом V " + aCfg(cfgprefix + "v3").ToString + " и " + aCfg(cfgprefix + "v4").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом V " + aCfg(cfgprefix + "v3").ToString + " и " + aCfg(cfgprefix + "v4").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                    End If
                End If
            End If
        Else
            If aCfg(cfgprefix + "v3") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v3"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v3"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v3").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v3").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "v4") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v4"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v4"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v4").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v4").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If




        If isOpen Then
            If aCfg(cfgprefix + "v5") > 0 And aCfg(cfgprefix + "v6") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v5"))
                VPrev2 = GetV(isHour, aPrev, aCfg(cfgprefix + "v6"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v5"))
                VLast2 = GetV(isHour, aLast, aCfg(cfgprefix + "v6"))

                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If VLast2 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k0") And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev2 - VLast2) >= aCfgSezon(aCfg, "k0") And VPrev2 <> 0 And VLast2 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Скачек расходов между циклами V " + aCfg(cfgprefix + "v6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

                If UseGVSMax Then
                    If Math.Abs(VLast2 - VLast1) >= aCfgSezon(aCfg, "k3") * GVSMax And VLast2 <> 0 And VLast1 <> 0 Then
                        m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между прямым и обратным трубопроводом V " + aCfg(cfgprefix + "v5").ToString + " и " + aCfg(cfgprefix + "v6").ToString + " >=GVSMAX', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                        WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между прямым и обратным трубопроводом V " + aCfg(cfgprefix + "v5").ToString + " и " + aCfg(cfgprefix + "v6").ToString + " >=GVSMAX", aLast("DCOUNTER"), id_bd, NodeNAME)
                    End If
                End If
            End If
        Else
            If aCfg(cfgprefix + "v5") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v5"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v5"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v5").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v5").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
            If aCfg(cfgprefix + "v6") > 0 Then
                VPrev1 = GetV(isHour, aPrev, aCfg(cfgprefix + "v6"))
                VLast1 = GetV(isHour, aLast, aCfg(cfgprefix + "v6"))
                If VLast1 = 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Нулевой расход V " + aCfg(cfgprefix + "v6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Нулевой расход V " + aCfg(cfgprefix + "v6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If
                If Math.Abs(VPrev1 - VLast1) >= aCfgSezon(aCfg, "k1") * Math.Abs(VPrev1) And VPrev1 <> 0 And VLast1 <> 0 Then
                    m_tvmain.QueryExec("Update Analizer set INFO =INFO||'" & vbCrLf & prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v6").ToString + "', color='RED',GERROR=GERROR+1 where id_bd=" & id_bd.ToString)
                    WriteNC(ApplicationTypeEnum.AppAuto, prefix + "Разница расходов между циклами V" + aCfg(cfgprefix + "v6").ToString, aLast("DCOUNTER"), id_bd, NodeNAME)
                End If

            End If
        End If



    End Sub


    Public Sub CheckStatus(ByVal myTVM As STKTVMain.TVMain, ByVal id_bd As Integer, ByVal daylag As Integer, ByVal HourLag As Integer)
        Dim query As String
        Dim linkerror As Integer
        Dim lastlink As Date
        Dim lastcall As Date
        Dim HCCount As Integer = 0
        Dim ErCount As Integer = 0
        Dim HMissing As Integer
        Dim DMissing As Integer
        Dim status As Integer
        Dim dt As DataTable
        If Not myTVM Is Nothing Then
            m_tvmain = myTVM
        End If



        dt = m_tvmain.QuerySelect("select * from Analizer where id_bd=" + id_bd.ToString)
        If dt.Rows.Count > 0 Then
            HCCount = dt.Rows(0)("HCCOUNT")
            ErCount += dt.Rows(0)("PERROR")
            ErCount += dt.Rows(0)("TERROR")
            ErCount += dt.Rows(0)("GERROR")
        End If
        query = ""
        query = query + "        select count(*) linkerror from datacurr where "
        query = query + " dcall >sysdate-" + HourLag.ToString + ".0/24 and id_ptype=1 and t1 is null and t2 is null and t3 is null"
        query = query + " and not HC is null"
        query = query + " and id_bd=" + id_bd.ToString
        dt = m_tvmain.QuerySelect(query)
        If dt.Rows.Count > 0 Then
            linkerror = dt.Rows(0)("linkerror")
        End If

        query = ""
        query = query + " select max( dcounter) lastlink, max(dcall) lastcall from datacurr where "
        query = query + " dcall >sysdate-" + daylag.ToString + " and ( (not t1 is null) or (not t2 is null) or (not t3 is null) )"
        query = query + " and id_bd=" + id_bd.ToString
        dt = m_tvmain.QuerySelect(query)
        If dt.Rows.Count > 0 Then
            Try
                lastlink = dt.Rows(0)("lastlink")
                lastcall = dt.Rows(0)("lastcall")
            Catch ex As Exception
                lastlink = DateTime.Now.AddDays(-daylag)
                lastcall = DateTime.Now.AddDays(-daylag)
            End Try

        Else
            lastcall = DateTime.Now.AddDays(-daylag)
            lastlink = DateTime.Now.AddDays(-daylag)
        End If



        query = ""
        query = query + " select count(*) hMissing from MISSINGARCH where ARCHDATE > sysdate-" + HourLag.ToString + ".0/24"
        query = query + " and DEVNAME like '%Часовой%' and DEVNAME  not like '%НУЛИ%' "
        query = query + " and id_bd=" + id_bd.ToString
        dt = m_tvmain.QuerySelect(query)
        If dt.Rows.Count > 0 Then
            HMissing = dt.Rows(0)("hMissing")
        End If

        query = ""
        query = query + " select count(*) DMissing from MISSINGARCH where ARCHDATE > sysdate-" + daylag.ToString
        query = query + " and DEVNAME like '%Суточный%' and DEVNAME  not like '%НУЛИ%'"
        query = query + " and id_bd=" + id_bd.ToString
        dt = m_tvmain.QuerySelect(query)
        If dt.Rows.Count > 0 Then
            DMissing = dt.Rows(0)("DMissing")
        End If



        status = 0


        status += linkerror
        status += HCCount
        status += HMissing
        status += DMissing
        status += ErCount
        query = ""
        query = query + " update ANALIZER set LASTLINK=" + m_tvmain.OracleDate(lastlink) + ",HMISSING=" + HMissing.ToString + ",DMISSING=" + DMissing.ToString + ",HCCOUNT=" + HCCount.ToString + ",STATUS=" + status.ToString + ",LINKERRORS=" + linkerror.ToString
        query = query + " where ID_BD=" + id_bd.ToString

        m_tvmain.QueryExec(query)
        If linkerror > 0 And (lastlink <= Now.AddMinutes(-90) And lastlink >= Now.AddHours(-3)) Then
            m_tvmain.QueryExec("Update Analizer set color='YELLOW' where  (color <>'RED' or color is null) and id_bd=" & id_bd.ToString)
            m_tvmain.QueryExec("Update Analizer set INFO='" & vbCrLf & "Ошибки по связи' where id_bd=" & id_bd.ToString)
        End If


        If lastlink < Now.AddHours(-3) Then
            If Math.Abs(DateDiff(DateInterval.Hour, lastlink, lastcall)) > 5 Then
                m_tvmain.QueryExec("Update Analizer set color='RED' where  id_bd=" & id_bd.ToString)
                m_tvmain.QueryExec("Update Analizer set INFO='" & vbCrLf & "Ошибка даты счетчика' where id_bd=" & id_bd.ToString)

            Else

            m_tvmain.QueryExec("Update Analizer set color='RED' where  id_bd=" & id_bd.ToString)
            m_tvmain.QueryExec("Update Analizer set INFO='" & vbCrLf & "Ошибки по связи (нет связи > 3 часов)' where id_bd=" & id_bd.ToString)
        End If

        End If

    End Sub

End Class
