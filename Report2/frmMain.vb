Imports System.Windows.Forms
'Imports SpreadsheetGear

Imports System.Diagnostics.Process
Imports System.IO
Imports System.Data
Imports Microsoft.Office.Interop.Excel





Public Class frmMain

    Private tvMain As STKTVMain.TVMain
    Private ff As String
    Public app As Microsoft.Office.Interop.Excel.Application


    Private Structure TArchive
        Public DateArch As DateTime


        Public V1 As Double
        Public V2 As Double
        Public V3 As Double
        Public V4 As Double
        Public V5 As Double
        Public V6 As Double

        Public M1 As Double
        Public M2 As Double
        Public M3 As Double
        Public M4 As Double
        Public M5 As Double
        Public M6 As Double
        Public Q1 As Double
        Public Q2 As Double

        Public WORKTIME1 As Double
        Public WORKTIME2 As Double

        Public archType As Short
    End Structure


    Private Sub CleanArchive(ByRef marc As TArchive)
        marc.DateArch = DTo.Value
        marc.V1 = 0
        marc.V2 = 0
        marc.V3 = 0
        marc.V4 = 0
        marc.V5 = 0
        marc.V6 = 0
        marc.M1 = 0
        marc.M2 = 0
        marc.M3 = 0
        marc.M4 = 0
        marc.M5 = 0
        marc.M6 = 0
        marc.Q1 = 0
        marc.Q2 = 0
        marc.WORKTIME1 = 0
        marc.WORKTIME2 = 0
        marc.archType = 5
    End Sub

    Private Sub SetByName(ByRef marc As TArchive, ByVal name As String, ByVal v As Double)
        Dim n As String
        n = name.ToUpper
        If n = "V1" Then marc.V1 = v
        If n = "V2" Then marc.V2 = v
        If n = "V3" Then marc.V3 = v
        If n = "V4" Then marc.V4 = v
        If n = "V5" Then marc.V5 = v
        If n = "V6" Then marc.V6 = v
        If n = "M1" Then marc.M1 = v
        If n = "M2" Then marc.M2 = v
        If n = "M3" Then marc.M3 = v
        If n = "M4" Then marc.M4 = v
        If n = "M5" Then marc.M5 = v
        If n = "M6" Then marc.M6 = v
        If n = "Q1" Then marc.Q1 = v
        If n = "Q2" Then marc.Q2 = v
        If n = "TW1" Then marc.WORKTIME1 = v
        If n = "TW2" Then marc.WORKTIME2 = v

    End Sub


    Public Function ClearDB(ByVal after As Date, ByVal befor As Date, ByVal archtype As Short, ByVal id_bd As String) As String
        Dim s As String
        after = after.AddSeconds(-1)
        befor = befor.AddSeconds(1)
        s = "delete from datawork where dcounter>=" +
        "to_date('" + after.Year.ToString() + "-" + after.Month.ToString() + "-" + after.Day.ToString() +
        " " + after.Hour.ToString() + ":" + after.Minute.ToString() + ":" + after.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS') and dcounter<=" +
        "to_date('" + befor.Year.ToString() + "-" + befor.Month.ToString() + "-" + befor.Day.ToString() +
        " " + befor.Hour.ToString() + ":" + befor.Minute.ToString() + ":" + befor.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS') and id_ptype=" + archtype.ToString() +
        "and id_bd=" + id_bd.ToString()
        Return s
    End Function

    Private Function WriteDatawork(ByVal id As String, ByVal tArch As TArchive) As String
        WriteDatawork = "INSERT INTO datawork(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,Q1,Q2,M1,M2,M3,M4,M5,M6,v1,v2,v3,v4,v5,v6,TSUM1,TSUM2) values ("
        WriteDatawork = WriteDatawork + id + ","
        WriteDatawork = WriteDatawork + "SYSDATE" + ","
        WriteDatawork = WriteDatawork + OracleDate(tArch.DateArch) + ","
        WriteDatawork = WriteDatawork + OracleDate(tArch.DateArch) + ","
        WriteDatawork = WriteDatawork + tArch.archType.ToString + ","
        WriteDatawork = WriteDatawork + tArch.Q1.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.Q2.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.M1.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.M2.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.M3.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.M4.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.M5.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.M6.ToString.Replace(",", ".") + ","

        WriteDatawork = WriteDatawork + tArch.V1.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.V2.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.V3.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.V4.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.V5.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.V6.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.WORKTIME1.ToString.Replace(",", ".") + ","
        WriteDatawork = WriteDatawork + tArch.WORKTIME2.ToString.Replace(",", ".")
        WriteDatawork = WriteDatawork + ")"
    End Function

    Public Function checkInstance() As Process
        Dim cProcess As Process = Process.GetCurrentProcess()
        Dim aProcesses() As Process = Process.GetProcessesByName(cProcess.ProcessName)
        'loop through all the processes that are currently running on the
        'system that have the same name
        For Each process As Process In aProcesses
            'Ignore the currently running process
            If process.Id <> cProcess.Id Then
                'Check if the process is running using the same EXE as this one
                If System.Reflection.Assembly.GetExecutingAssembly().Location = cProcess.MainModule.FileName Then
                    'if so return to the calling function with the instance of the process
                    Return process
                End If
            End If
        Next
        'if nothing was found then this is the only instance, so return null
        Return Nothing
    End Function



    Public Sub New()

        Dim tempProcess As Process
        tempProcess = checkInstance()
        If Not tempProcess Is Nothing Then
            ShowWindowAsync(tempProcess.MainWindowHandle, ShowWindowConstants.SW_SHOWMINIMIZED)

            ShowWindowAsync(tempProcess.MainWindowHandle, ShowWindowConstants.SW_RESTORE)
            End
        End If

        tvMain = New STKTVMain.TVMain()
        If (tvMain.Init() = False) Then System.Windows.Forms.Application.Exit()



        Me.InitializeComponent()




    End Sub

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() +
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Private Function GetData(ByVal deviceID As Integer, ByVal ID As Integer) As System.Data.DataTable

        'Me.FDataSet = New DataSet
        Dim table As System.Data.DataTable


        'CCA754A0-C71A-4680-A7A6-3A03076D884D	Мгновенный	1
        'DB43A76B-E83A-4475-900E-67BBA84B8FD6	Итоговый	2
        '8BFFB611-147A-4FD3-ACEB-9B542DB643DD	Часовой	3
        'D5B21EF5-F1C9-4DF4-8BA8-E09906EF15BA	Суточный	4


        'If ID = 5 Then
        '    table = tvMain.QuerySelect("select * from datawork where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER=" + OracleDate(DTo.Value.AddMonths(-1)) + " order by DCOUNTER")
        '    table.TableName = "Data_T"
        '    Return table
        'End If

        If ID = 1 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom.Value) + "  and DCOUNTER<= " + OracleDate(DTo.Value) + " order by DCOUNTER")
            table.TableName = "Data_M"
            Return table
        End If


        If ID = 4 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom.Value) + "  and DCOUNTER<= " + OracleDate(DTo.Value) + " order by DCOUNTER")
            table.TableName = "Data_D"
            Return table
        End If



        If ID = 3 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom.Value) + "  and DCOUNTER<= " + OracleDate(DTo.Value) + " order by DCOUNTER")
            table.TableName = "Data_H"
            Return table
        End If


        If ID = 2 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom.Value) + "  and DCOUNTER< " + OracleDate(DTo.Value.AddHours(24)) + " order by DCOUNTER")
            table.TableName = "Data_T"
            Return table
        End If


        If ID = 0 Then
            table = tvMain.QuerySelect("select contract.*,bbuildings.cshort bname,bbuildings.caddress,bbuildings.cphone1,devices.CDEVNAME from bdevices  join contract on bdevices.id_bd= contract.id_bd join bbuildings on bbuildings.id_bu=bdevices.id_bu join devices on bdevices.id_Dev=devices.id_dev where bdevices.id_bd=" + deviceID.ToString)
            table.TableName = "CONTRACT"
            Return table
        End If
        Return Nothing


    End Function

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        txtPath.Text = GetSetting("VIP", "REPORTGENXL", "OUTPATH", "C:\")

        txtTPath.Text = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\")
        Try
            dFrom.Value = CDate(GetSetting("VIP", "REPORTGENXL", "DFROM", Now()))
        Catch ex As Exception
            dFrom.Value = Now
        End Try


        Try
            DTo.Value = CDate(GetSetting("VIP", "REPORTGENXL", "DTO", Now()))
        Catch ex As Exception
            DTo.Value = Now
        End Try


        Dim dt As System.Data.DataTable
        dt = tvMain.QuerySelect("select nodename,id_bd  id from v_dev2 order by nodeName ")
        cmbNode.MultiColumn = True
        cmbNode.DataSource = dt
        cmbNode.DisplayMember = "nodename"

        cmbNode.ValueMember = "id"


        'Dim dt1 As System.Data.DataTable
        'dt1 = tvMain.QuerySelect("select whogive.id_who,whogive.id_whotop, whogive.cname, whogivetop.cname topname from whogive join whogivetop on whogive.id_whotop=whogivetop.id_whotop order by CName ")
        'cmbSupplier.MultiColumn = True
        'cmbSupplier.DataSource = dt1
        'cmbSupplier.DisplayMember = "topname"
        'cmbSupplier.ValueMember = "id_who"

        Dim dt1 As System.Data.DataTable
        dt1 = tvMain.QuerySelect("select  * from  whogivetop  order by CName ")
        cmbSupplier.MultiColumn = True
        cmbSupplier.DataSource = dt1
        cmbSupplier.DisplayMember = "cname"
        cmbSupplier.ValueMember = "id_whotop"


        Dim dt2 As System.Data.DataTable
        dt2 = tvMain.QuerySelect("select * from PARAMTYPE where id_type in( 3,4) order by ctype ")
        cmbType.DataSource = dt2
        cmbType.DisplayMember = "ctype"
        cmbType.ValueMember = "id_type"

        ReloadGroups()

    End Sub


    Private Sub ReloadGroups()
        Dim di As DirectoryInfo
        di = New DirectoryInfo(txtPath.Text & "\")
        Dim fi As FileInfo
        cmbGroups.Items.Clear()
        cmbGroups.Items.Add("-")
        For Each fi In di.GetFiles("*.grp")
            cmbGroups.Items.Add(fi.Name.Replace(".grp", ""))
        Next
        cmbGroups.SelectedIndex = 0
    End Sub

    Private WithEvents workbook As Workbook
    Private WithEvents outworkbook As Workbook
    Private WithEvents ws As Worksheet
    Private WithEvents outws As Worksheet


    Private Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
        Dim i As Integer
        For i = 0 To cmbNode.Items.Count - 1
            cmbNode.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub cmdClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearAll.Click
        Dim i As Integer
        For i = 0 To cmbNode.Items.Count - 1
            cmbNode.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        If (cmbNode.CheckedItems.Count > 0) Then

            SaveSetting("VIP", "REPORTGENXL", "DFROM", dFrom.Value)

            SaveSetting("VIP", "REPORTGENXL", "DTO", DTo.Value)

            txtError.Text = ""
            Dim j As Integer
            pb.Minimum = 0
            pb.Maximum = cmbNode.CheckedIndices.Count
            pb.Value = 0
            pb.Visible = True
            Dim id As Integer


            app = New Microsoft.Office.Interop.Excel.Application
            outworkbook = app.Workbooks.Add



            ff = txtPath.Text & "\REPORT_" & DateTime.Now.ToString("yyyyMMddHHmmssffff") & ".xls"
            outworkbook.SaveAs(ff, , , , False, , XlSaveAsAccessMode.xlExclusive)

            While outworkbook.Worksheets.Count > 1
                outworkbook.Worksheets.Item(1).Delete()
            End While
            outworkbook.Worksheets.Item(1).Cells().Clear()

            Dim isfirstpage As Boolean
            isfirstpage = True
            For j = 0 To cmbNode.CheckedItems.Count - 1
                pb.Value = j + 1
                pb.Refresh()

                id = cmbNode.CheckedItems.Item(j)(cmbNode.ValueMember)

                If ProcessReport(id, isfirstpage) Then
                    isfirstpage = False
                End If



            Next


            app.Quit()
            pb.Visible = False
            'MsgBox("Генерация отчетов завершена.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Построение отчетов")
            If ff <> "" Then
                Try
                    Process.Start(ff)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        Else
            MsgBox("Не выбрано ни одного узла")
        End If






    End Sub

    Private Function TryDouble(ByVal s As String) As Object
        Dim d As Double
        Try
            d = Double.Parse(s)
            Return d
        Catch ex As Exception
            Try
                d = Double.Parse(s.Replace(".", ","))
                Return d
            Catch ex2 As Exception
                Return s
            End Try

        End Try
    End Function

    'Private Sub ' CopyBorders(ByRef RFrom As Excel.Range, ByRef RTo As IRange, Optional ByVal NoTop As Boolean = False)
    '    Dim bfrom As SpreadsheetGear.IBorder

    '    If RFrom.ColumnWidthDefined Then
    '        RTo.ColumnWidth = RFrom.ColumnWidth
    '    End If

    '    If RFrom.RowHeightDefined Then
    '        RTo.RowHeight = RFrom.RowHeight ' + 1.2
    '    End If
    '    If RFrom.VerticalAlignmentDefined Then
    '        RTo.VerticalAlignment = RFrom.VerticalAlignment
    '    End If
    '    If RFrom.HorizontalAlignmentDefined Then
    '        RTo.HorizontalAlignment = RTo.HorizontalAlignment
    '    End If

    '    If RFrom.StyleDefined Then
    '        RTo.Style = RFrom.Style
    '    End If


    '    If RFrom.StyleDefined Then
    '        RTo.Style = RFrom.Style
    '    End If

    '    ''''''''''  font '''''''''''
    '    If RFrom.Font.SizeDefined Then
    '        RTo.Font.Size = RFrom.Font.Size
    '    End If


    '    If RFrom.Font.BoldDefined Then
    '        RTo.Font.Bold = RFrom.Font.Bold
    '    End If

    '    If RFrom.Font.ColorDefined Then
    '        RTo.Font.Color = RFrom.Font.Color
    '    End If

    '    If RFrom.Font.ItalicDefined Then
    '        RTo.Font.Italic = RFrom.Font.Italic
    '    End If


    '    If RFrom.Font.NameDefined Then
    '        RTo.Font.Name = RFrom.Font.Name
    '    End If


    '    If RFrom.Font.OutlineFontDefined Then
    '        RTo.Font.OutlineFont = RFrom.Font.OutlineFont
    '    End If


    '    If RFrom.Font.ShadowDefined Then
    '        RTo.Font.Shadow = RFrom.Font.Shadow
    '    End If

    '    If RFrom.Font.StrikethroughDefined Then
    '        RTo.Font.Strikethrough = RFrom.Font.Strikethrough
    '    End If


    '    If RFrom.ShrinkToFitDefined Then
    '        RTo.ShrinkToFit = RFrom.ShrinkToFit
    '    End If





    '    If RFrom.NumberFormatDefined Then
    '        RTo.NumberFormat = RFrom.NumberFormat
    '    End If

    '    If RFrom.AddIndentDefined Then
    '        RTo.AddIndent = RFrom.AddIndent
    '    End If

    '    If RFrom.OrientationDefined Then
    '        RTo.Orientation = RFrom.Orientation
    '    End If

    '    If RFrom.OutlineLevelDefined Then
    '        RTo.OutlineLevel = RFrom.OutlineLevel
    '    End If


    '    'If RFrom.MergeCellsDefined Then
    '    '    RTo.MergeCells = RFrom.MergeCells
    '    'End If

    '    If RFrom.WrapTextDefined Then
    '        RTo.WrapText = RFrom.WrapText
    '    End If



    '    Dim border As SpreadsheetGear.IBorder

    '    If Not NoTop Then
    '        bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeTop)
    '        border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeTop)
    '        border.LineStyle = bfrom.LineStyle
    '        border.Weight = bfrom.Weight
    '        border.Color = bfrom.Color
    '    End If


    '    bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeLeft)
    '    border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeLeft)
    '    border.LineStyle = bfrom.LineStyle
    '    border.Weight = bfrom.Weight
    '    border.Color = bfrom.Color

    '    'If Not NoTop Then
    '    bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeRight)
    '    border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeRight)
    '    border.LineStyle = bfrom.LineStyle
    '    border.Weight = bfrom.Weight
    '    border.Color = bfrom.Color
    '    'End If


    '    bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeBottom)
    '    border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeBottom)
    '    border.LineStyle = bfrom.LineStyle
    '    border.Weight = bfrom.Weight
    '    border.Color = bfrom.Color



    '    Application.DoEvents()
    'End Sub

    Private Function ProcessReport(ByVal id As Integer, ByVal firstpage As Boolean) As Boolean


        Dim paramid As Integer = cmbType.SelectedValue
        Dim dt As System.Data.DataTable
        Dim dt_itog As System.Data.DataTable
        Dim dt_prognoz As System.Data.DataTable
        Dim dt_contract As System.Data.DataTable
        Dim dt_inf As System.Data.DataTable
        Dim firstDetailRow As Integer = -1
        Dim lastDetailRow As Integer = -1



        Dim fname As String
        Dim sheetname As String



        dt_contract = GetData(id, 0)
        If dt_contract.Rows.Count = 0 Then
            txtError.Text = txtError.Text & vbCrLf & "!!! не удалось получить данные по настройкам для id=" & id.ToString
            Return False
        End If
        dt = tvMain.QuerySelect("select id_bd,repmaskfileH,repmasksheeth,cxlsfile,csheet,devices.cdevname from bdevices join devices on bdevices.id_dev=devices.id_dev where id_bd=" & id.ToString)
        If dt.Rows.Count > 0 Then
            Try
                If cmbType.SelectedValue = 3 Then
                    fname = "HMasks\" + dt.Rows(0)("cdevname") + "\" + dt.Rows(0)("repmaskfileH")
                    sheetname = dt.Rows(0)("repmasksheeth")
                Else
                    fname = "DMasks\" + dt.Rows(0)("cdevname") + "\" + dt.Rows(0)("cxlsfile")
                    sheetname = dt.Rows(0)("csheet")
                End If
            Catch ex As Exception
                fname = ""
                sheetname = ""
            End Try

            If fname = "" Then
                txtError.Text = txtError.Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не удалось прочитать данные о шаблоне"
                Return False
            End If


            Try
                ' workbook = _
                'SpreadsheetGear.Factory.GetWorkbook(txtTPath.Text + "\" + fname, _
                'System.Globalization.CultureInfo.CurrentCulture)

                workbook = app.Workbooks.Open(txtTPath.Text + "\" + fname, , True)




                Dim wi As Integer
                For wi = 1 To workbook.Worksheets.Count
                    ws = workbook.Worksheets(wi)
                    If ws.Name.ToLower.Trim = sheetname.ToLower.Trim Then
                        Exit For
                    End If
                    ws = Nothing
                Next
                If ws Is Nothing Then
                    txtError.Text = txtError.Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не найден лист :" & sheetname
                    Return False
                End If

            Catch ex As Exception
                txtError.Text = txtError.Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не найден шаблон :" & fname
                Return False
            End Try





            dt_inf = GetData(id, cmbType.SelectedValue)

            If dt_inf.Rows.Count = 0 Then
                txtError.Text = txtError.Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не обнаружены данные за запрашиваемый период"
                Return False
            End If

            dt_itog = GetData(id, 2)


            Dim condt As System.Data.DataTable
            condt = tvMain.QuerySelect("SELECT COLUMN_NAME,COMMENTS FROM user_col_comments WHERE table_name = 'CONTRACT'")



            If firstpage Then

                outws = outworkbook.Worksheets(1)
            Else
                Dim nws As Worksheet = outworkbook.Worksheets.Add()

                outws = nws
            End If

            Try
                outws.Name = dt_contract.Rows(0)("bname").ToString().Replace("/", " ").Replace("\", " ")
            Catch ex As Exception

            End Try



            Dim row As Integer
            Dim outrow As Integer
            Dim col As Integer
            Dim cell As Range
            Dim cell2 As Range

            cell = ws.Cells
            cell2 = outws.Cells
            cell.Copy(cell2)
            'app.Visible = True

            With outws.PageSetup
                .BottomMargin = ws.PageSetup.BottomMargin
                .LeftMargin = ws.PageSetup.LeftMargin
                .RightMargin = ws.PageSetup.RightMargin
                .TopMargin = ws.PageSetup.TopMargin
                .PrintArea = ws.PageSetup.PrintArea
                .Orientation = ws.PageSetup.Orientation
                '.HeaderMargin = ws.PageSetup.HeaderMargin


            End With

            pbrow.Maximum = 250
            pbrow.Minimum = 0
            pbrow.Value = 0




            outrow = 1
            For row = 1 To 150
                Try
                    pbrow.Value = outrow
                Catch ex As Exception
                    pbrow.Value = 250
                End Try



                cell = ws.Cells(row, 1)

                If Not cell.Value Is Nothing Then
                    If cell.Value.ToString = "[Report:Информация]" Then

                        Dim allcells As Range
                        Dim arr(100) As Object
                        allcells = outws.Cells(outrow, 1).EntireRow

                        Dim contIdx As Integer
                        For col = 2 To 100
                            'cell = ws.Cells(row, col)
                            cell2 = outws.Cells(outrow, col)
                            '' CopyBorders(cell, cell2)

                            If Not cell2.Value Is Nothing Then
                                If cell2.Value.ToString().Substring(0, 1) = "[" Then


                                    If cell2.Value.ToString() = "[Имя Узла Учета]" Then arr(col - 1) = dt_contract.Rows(0)("bName").ToString : Continue For
                                    If cell2.Value.ToString() = "[Адрес Узла Учета]" Then arr(col - 1) = dt_contract.Rows(0)("caddress").ToString : Continue For
                                    If cell2.Value.ToString() = "[Конечная дата]" Then arr(col - 1) = DTo.Value.ToString("y") & " г." : Continue For
                                    If cell2.Value.ToString() = "[Марка Счетчика]" Then
                                        'cell2.Value = "" : cell2.Value = dt_contract.Rows(0)("cdevname").ToString : Continue For
                                        For contIdx = 0 To condt.Rows.Count - 1
                                            If "[" + condt.Rows(contIdx)("COMMENTS") + "]" = "[Наименование счетчика]" Then arr(col - 1) = dt_contract.Rows(0)(condt.Rows(contIdx)("COLUMN_NAME")).ToString : Exit For
                                        Next

                                    End If


                                    For contIdx = 0 To condt.Rows.Count - 1
                                        If cell2.Value.ToString() = "[" + condt.Rows(contIdx)("COMMENTS") + "]" Then arr(col - 1) = dt_contract.Rows(0)(condt.Rows(contIdx)("COLUMN_NAME")).ToString : Exit For
                                    Next
                                Else
                                    arr(col - 1) = cell2.Value
                                End If
                            Else
                            End If

                        Next
                        outrow += 1
                        allcells.Value = arr
                    Else '[Report:Информация]


                        If cell.Value.ToString = "[Report:Архив]" Then
                            Dim ar As Integer
                            Dim v As String

                            firstDetailRow = outrow

                            For ar = dt_inf.Rows.Count - 1 To 0 Step -1
                                cell = ws.Cells(row, 1).EntireRow
                                'cell2 = outws.Range(outws.Cells(row, 1), outws.Cells(row, 100))
                                If firstDetailRow < outrow Then
                                    cell2 = outws.Cells(firstDetailRow, 1)
                                    cell2.EntireRow.Insert()
                                End If

                                cell2 = outws.Cells(firstDetailRow, 1).EntireRow


                                cell.Copy(cell2)

                                Dim allcells As Range
                                Dim arr(100) As Object
                                allcells = cell2

                                For col = 2 To 100
                                    'cell = ws.Cells(row, col)
                                    cell2 = outws.Cells(firstDetailRow, col)
                                    '' CopyBorders(cell, cell2, True)
                                    If Not cell2.Value Is Nothing Then
                                        If cell2.Formula.Substring(0, 1) <> "=" Then
                                            If cell2.Value.ToString().Substring(0, 1) = "[" Then
                                                v = cell2.Value.ToString

                                                v = PrepareV(v)

                                                cell2 = outws.Cells(firstDetailRow, col)
                                                Try
                                                    'cell2.Value = dt_inf.Rows(ar)(v)
                                                    arr(col - 1) = dt_inf.Rows(ar)(v)

                                                Catch ex As Exception
                                                    arr(col - 1) = ex.Message
                                                End Try
                                            Else
                                                arr(col - 1) = cell2.Value
                                            End If
                                        Else
                                            arr(col - 1) = cell2.Formula
                                        End If


                                    End If


                                Next

                                allcells.Value = arr

                                outrow += 1
                            Next
                            lastDetailRow = outrow - 1

                        Else '[Report:Архив]

                            If cell.Value.ToString = "[Report:Итоговые]" And dt_itog.Rows.Count > 0 Then

                                Dim allcells2 As Range
                                Dim arr2(100) As Object

                                ' cell.Application.Visible = True

                                allcells2 = outws.Cells(outrow, 1)
                                allcells2 = allcells2.EntireRow
                                allcells2.Copy()
                                allcells2.Insert()
                                allcells2 = outws.Cells(outrow, 1)
                                allcells2 = allcells2.EntireRow
                                allcells2.PasteSpecial(XlPasteType.xlPasteAll)



                                For col = 2 To 100
                                    'cell = ws.Cells(row, col)
                                    cell2 = outws.Cells(outrow, col)
                                    ' CopyBorders(cell, cell2)
                                    If Not cell2.Value Is Nothing Then
                                        If cell2.Value.ToString().Substring(0, 1) = "[" And cell2.Formula.ToString.Substring(0, 1) <> "=" Then
                                            Dim V As String
                                            V = cell2.Value.ToString()
                                            V = PrepareV(V)
                                            Try
                                                'cell2.Value = dt_itog.Rows(dt_itog.Rows.Count - 1)(V)
                                                arr2(col - 1) = dt_itog.Rows(0)(V)
                                            Catch ex As Exception
                                                'cell2.Value = ex.Message
                                                arr2(col - 1) = ex.Message
                                            End Try
                                        Else
                                            arr2(col - 1) = cell2.Formula
                                        End If
                                    End If

                                Next

                                allcells2.Value = arr2
                                outrow += 1
                                cell2 = outws.Cells(outrow, 1)
                                cell2 = cell2.EntireRow


                                Dim allcells As Range
                                Dim arr(100) As Object
                                allcells = cell2

                                For col = 2 To 100

                                    ''cell = ws.Cells(row, col)
                                    cell2 = outws.Cells(outrow, col)

                                    '' CopyBorders(cell, cell2)
                                    If Not cell2.Value Is Nothing Then

                                        If cell2.Value.ToString().Substring(0, 1) = "[" And cell2.Formula.ToString.Substring(0, 1) <> "=" Then
                                            Dim V As String
                                            V = cell2.Value.ToString()
                                            V = PrepareV(V)
                                            Try

                                                arr(col - 1) = dt_itog.Rows(dt_itog.Rows.Count - 1)(V)
                                            Catch ex As Exception
                                                If Not ex Is Nothing Then
                                                    arr(col - 1).Value = ex.Message
                                                End If
                                            End Try

                                        Else
                                            arr(col - 1) = cell2.Formula
                                        End If

                                    End If

                                Next
                                outrow += 1
                                allcells.Value = arr

                            Else ' end [Report:Итоговые]

                                If cell.Value.ToString = "[Report:Прогноз]" Then
                                    Dim tt As TArchive
                                    tt = New TArchive
                                    'CleanArchive(tt)

                                    Dim allcells As Range
                                    Dim arr(100) As Object
                                    allcells = outws.Cells(outrow, 1).EntireRow

                                    For col = 2 To 100
                                        'cell = ws.Cells(row, col)
                                        cell2 = outws.Cells(outrow, col)
                                        '' CopyBorders(cell, cell2)
                                        Dim days As Integer
                                        days = 7
                                        If Not cell2.Value Is Nothing Then

                                            If cell2.Formula.Substring(0, 1) <> "=" And cell2.Value.ToString().Substring(0, 1) = "[" Then

                                                If cell2.Value = "[Период прогноза+]" Then
                                                    Dim S As String
                                                    Dim BOM As Date
                                                    BOM = DTo.Value
                                                    BOM = BOM.AddDays(-BOM.Day + 1)
                                                    BOM = BOM.AddMonths(1)
                                                    BOM = BOM.AddDays(-1)
                                                    days = Math.Abs(DateDiff(DateInterval.Day, BOM, DTo.Value))
                                                    If days = 0 Then days = 1
                                                    S = "По среднему с " + DTo.Value.ToString("dd.MM") + " по " + BOM.ToString("dd.MM") + " (+)"
                                                    arr(col - 1) = S
                                                    'Else
                                                    '    Dim V As String
                                                    '    V = cell.Value.ToString()
                                                    '    V = PrepareV(V)
                                                    '    Dim ca As String
                                                    '    ca = cell.Address
                                                    '    ca = ca.Replace((row + 1).ToString, "")
                                                    '    ca = ca.Replace("$", "")
                                                    '    cell2.Formula = "=sum(" + ca + (lastDetailRow + 1).ToString + ":" + ca + (lastDetailRow - 6 + 1).ToString + ")/7*" + days.ToString

                                                    '    SetByName(tt, V, cell2.Value)

                                                End If



                                            End If
                                        End If

                                    Next



                                    ' Сохранить данные в таблице datacurr с типом 5


                                    ' fill data from cells ????
                                    tvMain.QueryExec(ClearDB(DTo.Value, DTo.Value, 5, id.ToString))
                                    tvMain.QueryExec(WriteDatawork(id.ToString, tt))

                                    outrow += 1 '"[Report:Прогноз]"
                                    allcells.Value = arr
                                Else
                                    If cell.Value.ToString = "[Report:ПредПрогноз]" Then

                                        dt_prognoz = GetData(id, 5)

                                        Dim allcells As Range
                                        Dim arr(100) As Object
                                        allcells = outws.Cells(outrow, 1).EntireRow

                                        For col = 2 To 100
                                            'cell = ws.Cells(row, col)
                                            cell2 = outws.Cells(outrow, col)
                                            ' CopyBorders(cell, cell2)
                                            If Not cell2.Value Is Nothing Then
                                                If cell2.Formula.Substring(0, 1) <> "=" And cell2.Value.ToString().Substring(0, 1) = "[" Then
                                                    If cell2.Value = "[Период прогноза-]" Then
                                                        Dim S As String
                                                        Dim eom As Date
                                                        Dim BOM As Date
                                                        eom = DTo.Value
                                                        eom = eom.AddDays(-eom.Day)
                                                        BOM = DTo.Value
                                                        BOM = BOM.AddMonths(-1)


                                                        S = "По среднему с " + BOM.ToString("dd.MM") + " по " + eom.ToString("dd.MM") + " (-)"
                                                        arr(col - 1) = S
                                                    Else

                                                        Dim V As String
                                                        V = cell.Value.ToString()
                                                        V = PrepareV(V)
                                                        If dt_prognoz.Rows.Count > 0 Then
                                                            Try
                                                                arr(col - 1) = dt_prognoz.Rows(0)(V)
                                                            Catch ex As Exception
                                                                arr(col - 1) = ex.Message
                                                            End Try
                                                        Else
                                                            arr(col - 1) = 0
                                                        End If

                                                    End If


                                                End If
                                            End If

                                        Next
                                        outrow += 1 ' "[Report:ПредПрогноз]"
                                        allcells.Value = arr
                                    Else
                                        'cell = ws.Range(ws.Cells(row, 1), ws.Cells(row, 100))
                                        'cell2 = outws.Range(outws.Cells(outrow, 1), outws.Cells(outrow, 100))
                                        'Try
                                        '    cell.Copy(cell2)
                                        '    'cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                        'Catch ex As Exception
                                        '    'cell.Copy(cell2)
                                        'End Try

                                        'cell2 = outws.Cells(outrow, 1)
                                        'cell2.Columns.Hidden = True


                                        outrow += 1 ' просто строка

                                    End If

                                End If

                            End If
                        End If
                    End If
                Else ' cell 0 is nothing
                    'cell = ws.Range(ws.Cells(row, 1), ws.Cells(row, 100))
                    'cell2 = outws.Range(outws.Cells(outrow, 1), outws.Cells(outrow, 100))
                    '' cell.Copy()


                    'Try
                    '    cell.Copy(cell2)
                    '    'cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                    'Catch ex As Exception
                    '    'cell.Copy(cell2)
                    'End Try


                    outrow += 1
                End If ' просто строка

            Next ' excel row ... 

            pbrow.Value = 0
            'Catch ex As Exception

            'End Try
            cell2 = outws.Cells(outrow, 1)
            cell2.Columns.Hidden = True

            outworkbook.Save()

            workbook.Close()
        End If

        Return True

    End Function

    Private Function PrepareV(ByVal V1 As String) As String
        Dim V As String
        V = V1
        V = V.Replace("Сообщение HC", "HC")
        V = V.Replace("Время Опроса", "DCOUNTER")
        V = V.Replace(" сут", "")
        V = V.Replace(" сум", "")
        V = V.Replace(" час", "")
        V = V.Replace("сут", "")
        V = V.Replace("итог", "")
        V = V.Replace("сум", "")
        V = V.Replace("час", "")
        V = V.Replace("тв", "")
        V = V.Replace("[", "")
        V = V.Replace("]", "")
        V = V.Replace("(", "")
        V = V.Replace(")", "")

        V = V.Replace(" ", "")
        If V = "WORKTIME" Then
            Return V
        End If
        'V = V.Replace("dQ", "QQ")
        V = V.Replace("Т", "T")
        V = V.Replace("М", "M")
        V = V.Replace("W", "Q")
        V = V.Replace("Р", "P")
        V = V.Replace("G", "M")

        V = V.Replace("dT", "DT12")

        V = V.Replace("HC", "HC")


        Return V
        'maskWnum = 'T|Т|Q|W|P|Р|M|М|G|dQ|Qтв|';
        'baseWnum = 'T|T|Q|Q|P|P|M|M|M|QQ|Q  |';
        'maskWOnum = 'dQ|dT  |Сообщение HC|Время Опроса|HC|';
        'baseWOnum = 'DQ|DT12|HC          |DCOUNTER    |HC|';
    End Function

    Private Function IDX2COL(ByVal idx As Int32) As String
        Dim tmp As Int32
        Dim rm As Int32
        Dim retstr As String
        Dim coder As String
        coder = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?"
        tmp = idx
        retstr = ""
        Do
            tmp = tmp - 1
            rm = tmp Mod 26
            retstr = coder.Substring(rm, 1) + retstr
            tmp = Math.Floor(tmp / 26)
        Loop Until tmp = 0
        Return retstr

    End Function




    Private Sub cmbCheckBySupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCheckBySupplier.Click
        Dim j As Integer
        Dim id As Integer
        For j = 0 To cmbSupplier.CheckedItems.Count - 1

            id = cmbSupplier.CheckedItems.Item(j)(cmbSupplier.ValueMember)
            SelectById(id)

        Next

    End Sub

    Private Sub SelectById(ByVal id As Integer)
        Dim dt As System.Data.DataTable
        dt = tvMain.QuerySelect("select id_whotop,id_bd from bbuildings tb,bdevices tbd, whogive      where tbd.id_bu=tb.id_bu  and tb.id_who =whogive.id_who and id_whotop=" + id.ToString())
        Dim i As Integer
        Dim j As Integer
        Dim nid As Integer
        For i = 0 To dt.Rows.Count - 1
            nid = dt.Rows(i)("id_bd")
            For j = 0 To cmbNode.Items.Count - 1
                If cmbNode.Items(j)(cmbNode.ValueMember) = nid Then
                    cmbNode.SetItemChecked(j, True)
                End If
            Next
        Next
    End Sub

    Private Sub SelectByNodeId(ByVal nid As Integer)

        Dim j As Integer


        For j = 0 To cmbNode.Items.Count - 1
            If cmbNode.Items(j)(cmbNode.ValueMember) = nid Then
                cmbNode.SetItemChecked(j, True)
            End If
        Next

    End Sub

    Private Sub cmdSelSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelSup.Click
        Dim i As Integer
        For i = 0 To cmbSupplier.Items.Count - 1
            cmbSupplier.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub cmdUnSelSup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnSelSup.Click
        Dim i As Integer
        For i = 0 To cmbSupplier.Items.Count - 1
            cmbSupplier.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub cmdPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPath.Click
        If cdlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            txtPath.Text = cdlg.SelectedPath
            SaveSetting("VIP", "REPORTGENXL", "OUTPATH", txtPath.Text)

        End If
    End Sub

    Private Sub cmdTPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTPath.Click
        If cdlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            txtTPath.Text = cdlg.SelectedPath
            SaveSetting("VIP", "REPORTGENXL", "MASKPATH", txtTPath.Text)
        End If
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ff <> "" Then
            Try
                Process.Start(ff)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Dim ne As NodeEditorLib.NodeEditor
    Private Sub ShowProperty()
        If ne Is Nothing Then
            ne = New NodeEditorLib.NodeEditor
        End If

        Dim f As Form
        f = ne.GetForm(cmbNode.SelectedValue, tvMain)

        f.ShowDialog()
        f = Nothing
    End Sub


    Private Sub cmdProperty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProperty.Click
        If cmbNode.SelectedIndex >= 0 Then
            ShowProperty()
        End If
    End Sub

    Private Sub cmbNode_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbNode.MouseClick

    End Sub

    Private Sub cmbNode_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbNode.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            ShowProperty()
        End If
    End Sub

    Private Sub cmdSaveGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveGroup.Click
        Dim s As String
        If cmbNode.CheckedItems.Count > 0 Then
            s = InputBox("Имя группы", "Задайте имя группы", cmbGroups.Text)
            If s <> "" Then
                Dim inf As String = ""
                Dim j As Integer
                Dim id As Integer
                For j = 0 To cmbNode.CheckedItems.Count - 1

                    id = cmbNode.CheckedItems.Item(j)(cmbNode.ValueMember)
                    If inf <> "" Then
                        inf = inf & vbCr
                    End If
                    inf = inf + id.ToString + ":" + cmbNode.CheckedItems.Item(j)(cmbNode.DisplayMember)
                Next
                System.IO.File.WriteAllText(txtPath.Text & "\" & s & ".grp", inf)
                ReloadGroups()
            End If
        Else
            MsgBox("Для сохранения группы надо отметить узлы")
        End If

    End Sub

    Private Sub cmbGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroups.SelectedIndexChanged
        If cmbGroups.SelectedIndex = 0 Then
            Dim i As Integer
            For i = 0 To cmbNode.Items.Count - 1
                cmbNode.SetItemChecked(i, False)
            Next
        Else
            Dim inf As String
            inf = System.IO.File.ReadAllText(txtPath.Text & "\" & cmbGroups.Text & ".grp")
            Dim l() As String
            Dim fld() As String
            Dim i As Integer
            l = Split(inf, vbCr)

            For i = 0 To cmbNode.Items.Count - 1
                cmbNode.SetItemChecked(i, False)
            Next
            For i = LBound(l) To UBound(l)
                fld = Split(l(i), ":")
                If fld(0) <> "" Then
                    SelectByNodeId(Integer.Parse(fld(0)))
                End If
            Next

        End If
    End Sub

    Private Sub cmdDelGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelGroup.Click
        Dim s As String
        If cmbGroups.Text <> "-" Then
            s = cmbGroups.Text
            If MsgBox("Удалить группу " + s + " ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Удаление сохраненной группы") = MsgBoxResult.Yes Then
                Try
                    System.IO.File.Delete(txtPath.Text & "\" & s & ".grp")
                    ReloadGroups()
                Catch ex As Exception

                End Try

            End If
        End If
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
    Private xlsDay As String
    Private xlsHour As String
    Private xlsDayPage As String
    Private xlsHourPage As String
    Private xlsDevtype As String


    Private Sub cmbNode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNode.SelectedIndexChanged
        If cmbNode.SelectedIndex >= 0 Then
            Dim drv As DataRowView
            drv = cmbNode.SelectedItem

            Dim dtB As System.Data.DataTable = tvMain.QuerySelect("select bdevices.*,id_who from bdevices  join bbuildings on bdevices.id_bu= bbuildings.id_bu where id_bd =" & drv("id").ToString)

            If dtB.Rows.Count > 0 Then
                Try
                    Dim q As String = "select cdevname from devices where id_dev=" + dtB.Rows(0)("id_dev").ToString
                    Dim ddt As System.Data.DataTable
                    ddt = tvMain.QuerySelect(q)
                    xlsDevtype = ddt.Rows(0)("cdevname")
                Catch ex As Exception
                    xlsDevtype = "unknown"
                End Try


                Try
                    lblHTPL.Text = dtB.Rows(0)("REPMASKFILEH") & ""
                    xlsHour = dtB.Rows(0)("REPMASKFILEH") & ""
                Catch ex As Exception
                    lblHTPL.Text = "Шаблон не определен"
                    xlsHour = ""
                End Try
                Try
                    lblHTPL.Text = lblHTPL.Text & "  ( " & dtB.Rows(0)("REPMASKSHEETH") & " )"
                    xlsHourPage = dtB.Rows(0)("REPMASKSHEETH") & ""
                Catch ex As Exception
                    xlsHourPage = ""
                End Try

                Try
                    lblDTPL.Text = dtB.Rows(0)("CXLSFILE") & ""
                    xlsDay = dtB.Rows(0)("CXLSFILE") & ""
                Catch ex As Exception
                    xlsDay = ""
                    lblDTPL.Text = "Шаблон не определен"
                End Try
                Try
                    lblDTPL.Text = lblDTPL.Text & "  ( " & dtB.Rows(0)("CSHEET") & " )"
                    xlsDayPage = dtB.Rows(0)("CSHEET") & ""
                Catch ex As Exception
                    xlsDayPage = ""
                End Try
            Else
                xlsDay = ""
                xlsHour = ""
                xlsDayPage = ""
                xlsHourPage = ""
                xlsDevtype = ""
            End If


        End If
    End Sub

    Private Sub cmdOpenHour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenHour.Click
        Dim p As String = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:") & "\DMasks\" & xlsDevtype & "\" & xlsDay
        Dim excel As Microsoft.Office.Interop.Excel.Application
        Dim wb As Microsoft.Office.Interop.Excel.Workbook
        Dim ws As Microsoft.Office.Interop.Excel.Worksheet
        excel = New Microsoft.Office.Interop.Excel.Application
        wb = Nothing
        Try
            wb = excel.Workbooks.Open(p)
            wb.Activate()
            For Each ws In wb.Worksheets
                If ws.Name.ToLower = xlsDayPage.ToLower Then
                    ws.Activate()
                    wb = Nothing
                    excel.Visible = True
                    excel = Nothing
                    ws = Nothing
                    Return
                End If
            Next
            excel.Visible = True
            wb = Nothing
            ws = Nothing
            excel = Nothing
            MsgBox("Не найден лист шаблона", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Ошибка при открытии шаблона")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Ошибка при открытии шаблона")
            If Not wb Is Nothing Then
                wb.Close()
                wb = Nothing
            End If
            excel = Nothing
        End Try
        

    End Sub

    Private Sub cmdOpenDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenDay.Click
        Dim p As String = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:") & "\HMasks\" & xlsDevtype & "\" & xlsHour
        Dim excel As Microsoft.Office.Interop.Excel.Application
        Dim wb As Microsoft.Office.Interop.Excel.Workbook
        Dim ws As Microsoft.Office.Interop.Excel.Worksheet
        excel = New Microsoft.Office.Interop.Excel.Application
        wb = Nothing
        Try
            wb = excel.Workbooks.Open(p)
            wb.Activate()

            For Each ws In wb.Worksheets
                If ws.Name.ToLower = xlsHourPage.ToLower Then
                    ws.Activate()
                    wb = Nothing
                    excel.Visible = True
                    excel = Nothing
                    ws = Nothing
                    Return
                End If
            Next
            excel.Visible = True
            wb = Nothing
            ws = Nothing
            excel = Nothing
            MsgBox("Не найден лист шаблона", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Ошибка при открытии шаблона")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Ошибка при открытии шаблона")
            If Not wb Is Nothing Then
                wb.Close()
                wb = Nothing
            End If
            excel = Nothing
        End Try

        
        'excel.Visible = True
    End Sub
End Class