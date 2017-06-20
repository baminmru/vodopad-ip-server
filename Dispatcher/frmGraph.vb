Imports Infragistics.UltraChart.Shared.Styles
Imports Oracle.DataAccess.Client

Public Class frmGraph
    Public id As Integer
    Public ptype As Integer = 1
    Private dto As Date
    Private dfrom As Date
    Private GraphDT As DataTable


    Private bActivated As Boolean = False


    Private Sub ClearGraph()
        Dim dt As DataTable
        dt = New DataTable
        Dim col As DataColumn
        col = New DataColumn("dcounter", GetType(System.DateTime))
        dt.Columns.Add(col)
        col = New DataColumn("value", GetType(System.Double))
        dt.Columns.Add(col)

        Dim dr As DataRow
        dr = dt.NewRow
        dr("dcounter") = Date.Now
        dr("value") = 0
        dt.Rows.Add(dr)

        CHART_A.DataSource = dt
        CHART_B.DataSource = dt
        CHART_C.DataSource = dt


        CHART_A.DataBind()
        CHART_B.DataBind()
        CHART_C.DataBind()


    End Sub
    Private Function MakeChartQuery(ByVal chart As Infragistics.Win.UltraWinChart.UltraChart, ByVal Chartnum As Integer) As String
        Dim plist As String = ""
        Dim joinlist As String = ""
        Dim where As String = ""

        Dim dt As DataTable
        Dim dt2 As DataTable


        Dim cmd2 As OracleCommand
        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter

        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from chartsettings  where id_bd=" + id.ToString() + " and ptype=" + ptype.ToString() + " and CHARTNUM=" + Chartnum.ToString() + " and Enable =1"
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()
        GraphDT = dt
        Try
            While (Not chart.ColorModel.Skin.PEs.Item(0) Is Nothing)
                chart.ColorModel.Skin.PEs.Remove(chart.ColorModel.Skin.PEs.Item(0))
            End While
        Catch
        End Try

        Dim paintElement1 As Infragistics.UltraChart.Resources.Appearance.PaintElement

        Dim i As Integer
        Dim pp As String
        For i = 0 To dt.Rows.Count - 1




            pp = dt.Rows(i)("pname")
            plist = plist + "," + pp

            cmd2 = New OracleCommand
            cmd2.Connection = TvMain.dbconnect
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "select ismin,ismax from valuebounds  where sezon=0 and id_bd=" + id.ToString() + " and ptype=" + ptype.ToString() + " and pname='" + pp + "'"
            dt2 = New DataTable

            da = New OracleDataAdapter
            da.SelectCommand = cmd2
            da.Fill(dt2)
            da.Dispose()
            cmd2.Dispose()

            paintElement1 = New Infragistics.UltraChart.Resources.Appearance.PaintElement()
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.Gradient
            paintElement1.Fill = System.Drawing.Color.FromArgb(dt.Rows(i)("COLOR"))
            paintElement1.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.Horizontal
            paintElement1.FillStopColor = System.Drawing.Color.FromArgb(dt.Rows(i)("COLOR"))
            paintElement1.Stroke = System.Drawing.Color.Transparent
            chart.ColorModel.Skin.PEs.Add(paintElement1)

            If dt2.Rows.Count > 0 Then

                ' check for minimum
                If dt2.Rows(0)("ISMIN") = 1 Then
                    plist = plist + ",nvl(" + pp + "min.pmin,0) " + pp + "_min"
                    joinlist = joinlist + " left join valuebounds " + pp + "min on " + pp + "min.sezon=0 and " + pp + "min.id_bd = d.id_bd and " + pp + "min.ptype=d.id_ptype and " + pp + "min.pname='" + pp + "' and " + pp + "min.ismin=1"
                    paintElement1 = New Infragistics.UltraChart.Resources.Appearance.PaintElement()
                    paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.Gradient
                    paintElement1.Fill = System.Drawing.Color.FromArgb(dt.Rows(i)("COLORMIN"))
                    paintElement1.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.Horizontal
                    paintElement1.FillStopColor = System.Drawing.Color.FromArgb(dt.Rows(i)("COLORMIN"))
                    paintElement1.Stroke = System.Drawing.Color.Transparent
                    chart.ColorModel.Skin.PEs.Add(paintElement1)
                End If

                If dt2.Rows(0)("ISMAX") = 1 Then
                    ' check for maximum
                    plist = plist + ",nvl(" + pp + "max.pmax,0) " + pp + "_max"
                    joinlist = joinlist + " left join valuebounds " + pp + "max on " + pp + "max.sezon=0 and " + pp + "max.id_bd = d.id_bd and " + pp + "max.ptype=d.id_ptype and " + pp + "max.pname='" + pp + "' and " + pp + "max.ismax=1"
                    paintElement1 = New Infragistics.UltraChart.Resources.Appearance.PaintElement()
                    paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.Gradient
                    paintElement1.Fill = System.Drawing.Color.FromArgb(dt.Rows(i)("COLORMAX"))
                    paintElement1.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.Horizontal
                    paintElement1.FillStopColor = System.Drawing.Color.FromArgb(dt.Rows(i)("COLORMAX"))
                    paintElement1.Stroke = System.Drawing.Color.Transparent
                    chart.ColorModel.Skin.PEs.Add(paintElement1)
                End If
            End If

        Next
        where = "where d.id_bd=:ID_BD  and d.dcounter >=:DFROM and d.dcounter <=:DTO and d.id_ptype=:PTYPE"
        Return "select d.dcounter " + plist + " from datacurr d " + joinlist + " " + where
    End Function

    Private Sub SetupSeries(ByVal chart As Infragistics.Win.UltraWinChart.UltraChart)
        'Dim i As Integer
        'Dim j As Integer

        'Dim paintElement1 As New Infragistics.UltraChart.Resources.Appearance.PaintElement()
        'Dim paintElement2 As New Infragistics.UltraChart.Resources.Appearance.PaintElement()
        'Dim paintElement3 As New Infragistics.UltraChart.Resources.Appearance.PaintElement()
        'Dim paintElement4 As New Infragistics.UltraChart.Resources.Appearance.PaintElement()
        'Dim paintElement5 As New Infragistics.UltraChart.Resources.Appearance.PaintElement()

        'paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.Gradient
        'paintElement1.Fill = System.Drawing.Color.FromArgb(  .FromArgb(CType(108, System.Byte), CType(162, System.Byte), CType(36, System.Byte))
        'paintElement1.FillGradientStyle = Infragistics.UltraChart.Shared.Styles.GradientStyle.Horizontal
        'paintElement1.FillStopColor = System.Drawing.Color.FromArgb(CType(148, System.Byte), CType(244, System.Byte), CType(17, System.Byte))
        'paintElement1.Stroke = System.Drawing.Color.Transparent

        'For i = 0 To GraphDT.Rows.Count - 1
        '    For j = 0 To chart.Series.Count - 1
        '        If GraphDT.Rows(i)("PNAME") = chart.Series.Item(j).Key Then
        '            chart.Series.Item(j).PEs.Item(0).FillGradientStyle = GradientStyle.None
        '            chart.Series.Item(j).PEs.Item(0).Fill = GraphDT.Rows(i)("COLOR")
        '        End If
        '        If GraphDT.Rows(i)("PNAME") + "_min" = chart.Series.Item(j).Key Then
        '            chart.Series.Item(j).PEs.Item(0).FillGradientStyle = GradientStyle.None
        '            chart.Series.Item(j).PEs.Item(0).Fill = GraphDT.Rows(i)("COLORMIN")
        '        End If

        '        If GraphDT.Rows(i)("PNAME") + "_max" = chart.Series.Item(j).Key Then
        '            chart.Series.Item(j).PEs.Item(0).FillGradientStyle = GradientStyle.None
        '            chart.Series.Item(j).PEs.Item(0).Fill = GraphDT.Rows(i)("COLORMAX")
        '        End If

        '    Next
        'Next


    End Sub

    Public Sub LoadData(ByVal newID As Integer)
        id = newID
        If Not bActivated Then Exit Sub
        ClearGraph()

        dto = Date.Now
        If optG.CheckedItem.DataValue = 0 Then
            dfrom = dtNewFrom.Value
            dto = dtNewTo.Value
        Else
            dfrom = dto.AddHours(-optG.CheckedItem.DataValue)
        End If

        Dim dt As DataTable

        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter

        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = MakeChartQuery(CHART_A, 0)
        cmd.Parameters.Add("ID_BD", OracleDbType.Int32)

        cmd.Parameters.Add("DFROM", OracleDbType.Date)
        cmd.Parameters.Add("DTO", OracleDbType.Date)
        cmd.Parameters.Add("PTYPE", OracleDbType.Int32)

        cmd.Parameters.Item("ID_BD").Value = id
        cmd.Parameters.Item("PTYPE").Value = ptype
        cmd.Parameters.Item("DFROM").Value = dfrom
        cmd.Parameters.Item("DTO").Value = dto


        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()

        If dt.Rows.Count = 0 Then Exit Sub

        If dt.Rows.Count > 0 And dt.Columns.Count > 1 Then
            CHART_A.DataSource = dt
            CHART_A.Axis.X.TickmarkStyle = AxisTickStyle.Smart
            CHART_A.Axis.X.TickmarkIntervalType = AxisIntervalType.Hours
            CHART_A.Axis.X.TickmarkInterval = 2
            CHART_A.DataBind()
            SetupSeries(CHART_A)

        End If



        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = MakeChartQuery(CHART_B, 1)
        cmd.Parameters.Add("ID_BD", OracleDbType.Int32)
        cmd.Parameters.Add("DFROM", OracleDbType.Date)
        cmd.Parameters.Add("DTO", OracleDbType.Date)
        cmd.Parameters.Add("PTYPE", OracleDbType.Int32)

        cmd.Parameters.Item("ID_BD").Value = id
        cmd.Parameters.Item("PTYPE").Value = ptype
        cmd.Parameters.Item("DFROM").Value = dfrom
        cmd.Parameters.Item("DTO").Value = dto
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()
        If dt.Rows.Count > 0 And dt.Columns.Count > 1 Then
            CHART_B.DataSource = dt
            CHART_B.Axis.X.TickmarkStyle = AxisTickStyle.Smart
            CHART_B.Axis.X.TickmarkIntervalType = AxisIntervalType.Hours
            CHART_B.Axis.X.TickmarkInterval = 2
            CHART_B.DataBind()
            SetupSeries(CHART_B)
        End If


        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = MakeChartQuery(CHART_C, 2)
        cmd.Parameters.Add("ID_BD", OracleDbType.Int32)
        cmd.Parameters.Add("DFROM", OracleDbType.Date)
        cmd.Parameters.Add("DTO", OracleDbType.Date)
        cmd.Parameters.Add("PTYPE", OracleDbType.Int32)

        cmd.Parameters.Item("ID_BD").Value = id
        cmd.Parameters.Item("DFROM").Value = dfrom
        cmd.Parameters.Item("DTO").Value = dto
        cmd.Parameters.Item("PTYPE").Value = ptype
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()
        If dt.Rows.Count > 0 And dt.Columns.Count > 1 Then
            CHART_C.DataSource = dt
            CHART_C.Axis.X.TickmarkStyle = AxisTickStyle.Smart
            CHART_C.Axis.X.TickmarkIntervalType = AxisIntervalType.Hours
            CHART_C.Axis.X.TickmarkInterval = 2
            CHART_C.DataBind()
            SetupSeries(CHART_C)
        End If

        'dt = New DataTable
        'cmd = New OracleCommand
        'cmd.Connection = TvMain.dbconnect
        'cmd.CommandType = CommandType.Text
        'cmd.CommandText = "select dcounter,P1,P2, P3,P4,P5,P6 from datacurr where id_bd=" & id.ToString() & " and id_ptype=3 and dcounter > sysdate-360 and dcounter < sysdate-359"
        'da = New OracleDataAdapter
        'da.SelectCommand = cmd
        'da.Fill(dt)
        'cmd.Dispose()
        'da.Dispose()
        'CHART_D.DataSource = dt
        'CHART_D.Axis.X.TickmarkStyle = AxisTickStyle.Smart
        'CHART_D.Axis.X.TickmarkIntervalType = AxisIntervalType.Hours
        'CHART_D.Axis.X.TickmarkInterval = 2
        'CHART_D.DataBind()

    End Sub



    Private Sub frmGraph_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bActivated = True
        LoadData(id)
    End Sub

    Private Sub frmGraph_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        bActivated = False
    End Sub

    Private Sub chkMoment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoment.CheckedChanged
        If chkMoment.Checked Then
            ptype = 1
            chkHour.Checked = False
            chkDay.Checked = False
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
        End If

    End Sub

    Private Sub chkHour_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHour.CheckedChanged
        If chkHour.Checked Then
            ptype = 3
            chkMoment.Checked = False
            chkDay.Checked = False
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
        End If

    End Sub

    Private Sub chkDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDay.CheckedChanged
        If chkDay.Checked Then
            ptype = 4
            chkMoment.Checked = False
            chkHour.Checked = False
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
        End If

    End Sub

    Private Sub optG_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optG.ValueChanged
        LoadData(id)
    End Sub

    Private Sub frmGraph_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MyfrmGraph = Nothing
    End Sub

    Private Sub cmdInitChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInitChart.Click
        TvMain.QueryExec("begin  initcharts; end; ")
    End Sub
End Class