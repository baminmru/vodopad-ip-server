Imports SpreadsheetGear
Imports System.Diagnostics.Process
Imports System.IO

Public Class maker

    Private tvMain As STKTVMain.TVMain
    Private ff As String
    Private txtPath_text As String
    Private txtWWWPath_text As String
    Private txtTPath_text As String
    Private txtError_text As String
    Private dfrom As Date
    Private dto As Date
    Private ptype As Integer
    Private id_bd As Integer




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

        Public TW1 As Double
        Public TW2 As Double

        Public archType As Short
    End Structure


    Private Sub clearTarchive(ByRef marc As TArchive)
        marc.DateArch = Dto
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
        marc.TW1 = 0
        marc.TW2 = 0
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
        If n = "TW1" Then marc.TW1 = v
        If n = "TW2" Then marc.TW2 = v

    End Sub


    Public Function ClearDB(ByVal after As Date, ByVal befor As Date, ByVal archtype As Short, ByVal id_bd As String) As String
        Dim s As String
        after = after.AddSeconds(-1)
        befor = befor.AddSeconds(1)
        s = "delete from datawork where dcounter>=" + _
        "to_date('" + after.Year.ToString() + "-" + after.Month.ToString() + "-" + after.Day.ToString() + _
        " " + after.Hour.ToString() + ":" + after.Minute.ToString() + ":" + after.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS') and dcounter<=" + _
        "to_date('" + befor.Year.ToString() + "-" + befor.Month.ToString() + "-" + befor.Day.ToString() + _
        " " + befor.Hour.ToString() + ":" + befor.Minute.ToString() + ":" + befor.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS') and id_ptype=" + archtype.ToString() + _
        "and id_bd=" + id_bd.ToString()
        Return s
    End Function

    Private Function WriteTArchToDB(ByVal id As String, ByVal tArch As TArchive) As String
        WriteTArchToDB = "INSERT INTO datawork(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,Q1,Q2,M1,M2,M3,M4,M5,M6,v1,v2,v3,v4,v5,v6,TSUM1,TSUM2) values ("
        WriteTArchToDB = WriteTArchToDB + id + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.Q2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M3.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M4.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M5.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.M6.ToString.Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + tArch.V1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V2.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V3.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V4.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V5.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.V6.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.TW1.ToString.Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + tArch.TW2.ToString.Replace(",", ".")
        WriteTArchToDB = WriteTArchToDB + ")"
    End Function

  
  

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Private Function GetData(ByVal deviceID As Integer, ByVal ID As Integer) As DataTable

        'Me.FDataSet = New DataSet
        Dim table As DataTable


        'CCA754A0-C71A-4680-A7A6-3A03076D884D	Мгновенный	1
        'DB43A76B-E83A-4475-900E-67BBA84B8FD6	Итоговый	2
        '8BFFB611-147A-4FD3-ACEB-9B542DB643DD	Часовой	3
        'D5B21EF5-F1C9-4DF4-8BA8-E09906EF15BA	Суточный	4


        If ID = 5 Then
            table = tvMain.QuerySelect("select * from datawork where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER=" + OracleDate(Dto.AddMonths(-1)) + " order by DCOUNTER")
            table.TableName = "Data_T"
            Return table
        End If

        If ID = 1 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom) + "  and DCOUNTER<= " + OracleDate(Dto) + " order by DCOUNTER")
            table.TableName = "Data_M"
            Return table
        End If


        If ID = 4 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom) + "  and DCOUNTER<= " + OracleDate(Dto) + " order by DCOUNTER")
            table.TableName = "Data_D"
            Return table
        End If



        If ID = 3 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom) + "  and DCOUNTER<= " + OracleDate(Dto) + " order by DCOUNTER")
            table.TableName = "Data_H"
            Return table
        End If


        If ID = 2 Then
            table = tvMain.QuerySelect("select * from datacurr where id_ptype=" + ID.ToString + " and  id_bd=" + deviceID.ToString + " and DCOUNTER>=" + OracleDate(dFrom) + "  and DCOUNTER< " + OracleDate(Dto.AddHours(24)) + " order by DCOUNTER")
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

    

    Private WithEvents workbook As IWorkbook
    Private WithEvents outworkbook As IWorkbook
    Private WithEvents ws As IWorksheet
    Private WithEvents outws As IWorksheet


    

    Public Sub GO(ByVal reportID As String)

        tvMain = New STKTVMain.TVMain()
        If (tvMain.Init() = False) Then Exit Sub

        txtPath_text = GetSetting("VIP", "REPORTGENXL", "WEBPATH", "C:\bami\web\rpt")
        txtWWWPath_text = GetSetting("VIP", "REPORTGENXL", "WWWPATH", "/rpt")
        txtTPath_text = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\bin\VODOPAD-IP\Masks\Luda")
        Dim dt As DataTable
        dt = tvMain.QuerySelect("select * from WEBREPORT where WEBREPORTid=" + reportID)

        If dt.Rows.Count = 0 Then Exit Sub

        dfrom = dt.Rows(0)("dfrom")
        dto = dt.Rows(0)("dto")
        ptype = dt.Rows(0)("id_ptype")
        id_bd = dt.Rows(0)("id_bd")

        txtError_text = ""

        outworkbook = SpreadsheetGear.Factory.GetWorkbook(System.Globalization.CultureInfo.CurrentCulture)

        ff = txtPath_text & "\REPORT_" & DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("\", "").Replace(".", "").Replace("/", "") & ".xls"
        outworkbook.SaveAs(ff, FileFormat.Excel8)

        While outworkbook.Worksheets.Count > 1
            outworkbook.Worksheets.Item(0).Delete()
        End While
        outworkbook.Worksheets.Item(0).Cells().Clear()


        If ProcessReport(id_bd, True) Then
            ' update real report name in base
            tvMain.QueryExec("update webreport set reportready=1,reportfile='" + ff.Replace(txtPath_text, txtWWWPath_text).Replace("\", "/") + "' where  WEBREPORTid=" + reportID)
        Else
            tvMain.QueryExec("update webreport set reportready=0,reportmsg='" + txtError_text + "' where  WEBREPORTid=" + reportID)
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

    Private Sub CopyBorders(ByRef RFrom As IRange, ByRef RTo As IRange, Optional ByVal NoTop As Boolean = False)
        Dim bfrom As SpreadsheetGear.IBorder

        If RFrom.ColumnWidthDefined Then
            RTo.ColumnWidth = RFrom.ColumnWidth
        End If

        If RFrom.RowHeightDefined Then
            RTo.RowHeight = RFrom.RowHeight ' + 1.2
        End If
        If RFrom.VerticalAlignmentDefined Then
            RTo.VerticalAlignment = RFrom.VerticalAlignment
        End If
        If RFrom.HorizontalAlignmentDefined Then
            RTo.HorizontalAlignment = RTo.HorizontalAlignment
        End If

        If RFrom.StyleDefined Then
            RTo.Style = RFrom.Style
        End If


        If RFrom.StyleDefined Then
            RTo.Style = RFrom.Style
        End If

        ''''''''''  font '''''''''''
        If RFrom.Font.SizeDefined Then
            RTo.Font.Size = RFrom.Font.Size
        End If


        If RFrom.Font.BoldDefined Then
            RTo.Font.Bold = RFrom.Font.Bold
        End If

        If RFrom.Font.ColorDefined Then
            RTo.Font.Color = RFrom.Font.Color
        End If

        If RFrom.Font.ItalicDefined Then
            RTo.Font.Italic = RFrom.Font.Italic
        End If


        If RFrom.Font.NameDefined Then
            RTo.Font.Name = RFrom.Font.Name
        End If


        If RFrom.Font.OutlineFontDefined Then
            RTo.Font.OutlineFont = RFrom.Font.OutlineFont
        End If


        If RFrom.Font.ShadowDefined Then
            RTo.Font.Shadow = RFrom.Font.Shadow
        End If

        If RFrom.Font.StrikethroughDefined Then
            RTo.Font.Strikethrough = RFrom.Font.Strikethrough
        End If


        If RFrom.ShrinkToFitDefined Then
            RTo.ShrinkToFit = RFrom.ShrinkToFit
        End If


        If RFrom.NumberFormatDefined Then
            RTo.NumberFormat = RFrom.NumberFormat
        End If

        If RFrom.AddIndentDefined Then
            RTo.AddIndent = RFrom.AddIndent
        End If

        If RFrom.OrientationDefined Then
            RTo.Orientation = RFrom.Orientation
        End If

        If RFrom.OutlineLevelDefined Then
            RTo.OutlineLevel = RFrom.OutlineLevel
        End If


        'If RFrom.MergeCellsDefined Then
        '    RTo.MergeCells = RFrom.MergeCells
        'End If

        If RFrom.WrapTextDefined Then
            RTo.WrapText = RFrom.WrapText
        End If



        Dim border As SpreadsheetGear.IBorder

        If Not NoTop Then
            bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeTop)
            border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeTop)
            border.LineStyle = bfrom.LineStyle
            border.Weight = bfrom.Weight
            border.Color = bfrom.Color
        End If


        bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeLeft)
        border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeLeft)
        border.LineStyle = bfrom.LineStyle
        border.Weight = bfrom.Weight
        border.Color = bfrom.Color

        'If Not NoTop Then
        bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeRight)
        border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeRight)
        border.LineStyle = bfrom.LineStyle
        border.Weight = bfrom.Weight
        border.Color = bfrom.Color
        'End If


        bfrom = RFrom.Borders(SpreadsheetGear.BordersIndex.EdgeBottom)
        border = RTo.Borders(SpreadsheetGear.BordersIndex.EdgeBottom)
        border.LineStyle = bfrom.LineStyle
        border.Weight = bfrom.Weight
        border.Color = bfrom.Color

        'RTo.ColumnWidth = RFrom.ColumnWidth
        'RTo.RowHeight = RFrom.RowHeight + 1.2
        'RTo.VerticalAlignment = RFrom.VerticalAlignment
        'RTo.HorizontalAlignment = RTo.HorizontalAlignment


        Application.DoEvents()
    End Sub

    Private Function ProcessReport(ByVal id As Integer, ByVal firstpage As Boolean) As Boolean


        Dim paramid As Integer = ptype
        Dim dt As DataTable
        Dim dt_itog As DataTable
        Dim dt_prognoz As DataTable
        Dim dt_contract As DataTable
        Dim dt_inf As DataTable
        Dim firstDetailRow As Integer = -1
        Dim lastDetailRow As Integer = -1



        Dim fname As String
        Dim sheetname As String

        dt_contract = GetData(id, 0)
        If dt_contract.Rows.Count = 0 Then
            txtError_Text = txtError_Text & vbCrLf & "!!! не удалось получить данные по настройкам для id=" & id.ToString
            Return False
        End If
        dt = tvMain.QuerySelect("select id_bd,repmaskfileH,repmasksheeth,cxlsfile,csheet,devices.cdevname from bdevices join devices on bdevices.id_dev=devices.id_dev where id_bd=" & id.ToString)
        If dt.Rows.Count > 0 Then
            Try
                If ptype = 3 Then
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
                txtError_Text = txtError_Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не удалось прочитать данные о шаблоне"
                Return False
            End If


            Try
                workbook = _
               SpreadsheetGear.Factory.GetWorkbook(txtTPath_Text + "\" + fname, _
               System.Globalization.CultureInfo.CurrentCulture)
                Dim wi As Integer
                For wi = 0 To workbook.Worksheets.Count - 1
                    ws = workbook.Worksheets.Item(wi)
                    If ws.Name.ToLower.Trim = sheetname.ToLower.Trim Then
                        Exit For
                    End If
                    ws = Nothing
                Next
                If ws Is Nothing Then
                    txtError_Text = txtError_Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не найден лист :" & sheetname
                    Return False
                End If

            Catch ex As Exception
                txtError_Text = txtError_Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не найден шаблон :" & fname
                Return False
            End Try





            dt_inf = GetData(id, ptype)

            If dt_inf.Rows.Count = 0 Then
                txtError_Text = txtError_Text & vbCrLf & dt_contract.Rows(0)("bname").ToString() & "-> не обнаружены данные за запрашиваемый период"
                Return False
            End If

            dt_itog = GetData(id, 2)


            Dim condt As DataTable
            condt = tvMain.QuerySelect("SELECT COLUMN_NAME,COMMENTS FROM user_col_comments WHERE table_name = 'CONTRACT'")



            If firstpage Then

                outws = outworkbook.Worksheets.Item(0)
            Else
                Dim nws As IWorksheet = outworkbook.Worksheets.Add()

                outws = nws
            End If

            Try
                outws.Name = dt_contract.Rows(0)("bname").ToString().Replace("/", " ").Replace("\", " ")
            Catch ex As Exception

            End Try


            Dim IST As IStyle
            IST = outworkbook.Styles.Item("Normal")
            IST.Font.Name = "Aryal"
            IST.Font.Size = 9

            outws.PageSetup.RightMargin = ws.PageSetup.RightMargin
            outws.PageSetup.LeftMargin = ws.PageSetup.LeftMargin
            outws.PageSetup.TopMargin = ws.PageSetup.TopMargin
            outws.PageSetup.BottomMargin = ws.PageSetup.BottomMargin
            outws.PageSetup.FooterMargin = ws.PageSetup.FooterMargin
            outws.PageSetup.HeaderMargin = ws.PageSetup.HeaderMargin





            'Try
            'Dim style_in As IStyle
            'Dim style_out As IStyle
            'For Each style_in In workbook.Styles
            '    If style_in.BuiltIn = False Then
            '        style_out = outworkbook.Styles.Add(style_in.Name)
            '        style_out.AddIndent = style_in.AddIndent
            '        'style_out.Borders = style_in.Borders
            '        style_out.Font = style_in.Font

            '    End If

            'Next

            Dim row As Integer
            Dim outrow As Integer
            Dim col As Integer
            Dim cell As IRange
            Dim cell2 As IRange
            outrow = 0
            For row = 0 To 100


                cell = ws.Cells(row, 0)

                If Not cell.Value Is Nothing Then
                    If cell.Value.ToString = "[Report:Информация]" Then
                        cell = ws.Range(row, 1, row, 255)
                        cell2 = outws.Range(outrow, 1, outrow, 255)
                        Try
                            cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                            cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                            cell.Copy(cell2, PasteType.Formats, PasteOperation.None, True, False)

                        Catch ex As Exception
                            cell.Copy(cell2)
                        End Try

                        cell2 = outws.Cells(outrow, 0)
                        cell2.Columns.Hidden = True

                        Dim contIdx As Integer
                        For col = 1 To 255
                            cell = ws.Cells(row, col)
                            cell2 = outws.Cells(outrow, col)
                            CopyBorders(cell, cell2)

                            If Not cell.Value Is Nothing Then
                                If cell.Value.ToString().Substring(0, 1) = "[" Then


                                    If cell.Value.ToString() = "[Имя Узла Учета]" Then cell2.Value = "" : cell2.Value = dt_contract.Rows(0)("bName").ToString : Continue For
                                    If cell.Value.ToString() = "[Адрес Узла Учета]" Then cell2.Value = "" : cell2.Value = dt_contract.Rows(0)("caddress").ToString : Continue For
                                    If cell.Value.ToString() = "[Конечная дата]" Then cell2.Value = "" : cell2.Value = dto : Continue For
                                    If cell.Value.ToString() = "[Марка Счетчика]" Then
                                        'cell2.Value = "" : cell2.Value = dt_contract.Rows(0)("cdevname").ToString : Continue For
                                        For contIdx = 0 To condt.Rows.Count - 1
                                            If "[" + condt.Rows(contIdx)("COMMENTS") + "]" = "[Наименование счетчика]" Then cell2.Value = "" : cell2.Value = dt_contract.Rows(0)(condt.Rows(contIdx)("COLUMN_NAME")).ToString : Exit For
                                        Next

                                    End If


                                    For contIdx = 0 To condt.Rows.Count - 1
                                        If cell.Value.ToString() = "[" + condt.Rows(contIdx)("COMMENTS") + "]" Then cell2.Value = "" : cell2.Value = dt_contract.Rows(0)(condt.Rows(contIdx)("COLUMN_NAME")).ToString : Exit For
                                    Next

                                End If
                            End If
                        Next

                        cell2 = outws.Cells(outrow, 0)
                        cell2.Columns.Hidden = True

                        For col = 1 To 255
                            cell = ws.Cells(row, col)
                            cell2 = outws.Cells(outrow, col)
                            CopyBorders(cell, cell2)
                            If Not cell.Value Is Nothing Then

                                Dim f As String
                                f = cell2.Formula
                                If f <> "" Then
                                    If f.Substring(0, 1) = "=" Then
                                        If lastDetailRow <> -1 And firstDetailRow <> -1 Then
                                        If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                            f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                            cell2.Formula = f
                                        End If
                                    End If

                                    End If
                                End If
                            End If

                        Next
                        outrow += 1

                    Else '[Report:Информация]


                        If cell.Value.ToString = "[Report:Архив]" Then
                            Dim ar As Integer
                            Dim v As String

                            firstDetailRow = outrow

                            For ar = 0 To dt_inf.Rows.Count - 1
                                cell = ws.Range(row, 1, row, 255)
                                cell2 = outws.Range(outrow, 1, outrow, 255)

                                Try
                                    cell.Copy(cell2, PasteType.FormulasAndNumberFormats, PasteOperation.None, True, False)
                                    'cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                    'cell.Copy(cell2, PasteType.Formats, PasteOperation.None, True, False)
                                Catch ex As Exception
                                    cell.Copy(cell2)
                                End Try

                                cell2 = outws.Cells(outrow, 0)
                                cell2.Columns.Hidden = True

                                For col = 1 To 255
                                    cell = ws.Cells(row, col)
                                    cell2 = outws.Cells(outrow, col)
                                    CopyBorders(cell, cell2, True)
                                    If Not cell.Value Is Nothing Then
                                        If cell.Formula.Substring(0, 1) <> "=" Then
                                        If cell.Value.ToString().Substring(0, 1) = "[" Then
                                            v = cell2.Value.ToString

                                            v = PrepareV(v)

                                            cell2 = outws.Cells(outrow, col)
                                            Try
                                                cell2.Value = dt_inf.Rows(ar)(v)
                                            Catch ex As Exception
                                                cell2.Value = ex.Message
                                            End Try

                                        End If
                                        Else
                                            Dim f As String
                                            f = cell2.Formula
                                            'If f <> "" Then
                                            '    If f.Substring(0, 1) = "=" Then
                                            '        'If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                            '        'f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                            '        cell2.Formula = f
                                            '        'End If
                                            '    End If
                                            'End If
                                        End If
                                    End If


                                Next

                                outrow += 1
                            Next
                            lastDetailRow = outrow - 1

                        Else '[Report:Архив]

                            If cell.Value.ToString = "[Report:Итоговые]" And dt_itog.Rows.Count > 0 Then
                                cell = ws.Range(row, 1, row, 255)
                                cell2 = outws.Range(outrow, 1, outrow, 255)
                                Try
                                    cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                                    cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                    cell.Copy(cell2, PasteType.Formats, PasteOperation.None, True, False)
                                Catch ex As Exception
                                    cell.Copy(cell2)
                                End Try

                                cell2 = outws.Cells(outrow, 0)
                                cell2.Columns.Hidden = True
                                For col = 1 To 255
                                    cell = ws.Cells(row, col)
                                    cell2 = outws.Cells(outrow, col)
                                    CopyBorders(cell, cell2)
                                    If Not cell.Value Is Nothing Then

                                        If cell.Formula.Substring(0, 1) <> "=" And cell.Value.ToString().Substring(0, 1) = "[" Then
                                            Dim V As String
                                            V = cell.Value.ToString()
                                            V = PrepareV(V)
                                            Try
                                                cell2.Value = dt_itog.Rows(0)(V)
                                            Catch ex As Exception
                                                cell2.Value = ex.Message
                                            End Try
                                        Else
                                            Dim f As String
                                            f = cell2.Formula
                                            If f <> "" Then
                                                If f.Substring(0, 1) = "=" Then
                                                    If lastDetailRow <> -1 And firstDetailRow <> -1 Then
                                                    If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                                        f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                                        cell2.Formula = f
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    End If

                                Next
                                outrow += 1

                                cell = ws.Range(row, 1, row, 255)
                                cell2 = outws.Range(outrow, 1, outrow, 255)
                                Try
                                    cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                                    cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                    cell.Copy(cell2, PasteType.Formats, PasteOperation.None, True, False)
                                Catch ex As Exception
                                    cell.Copy(cell2)
                                End Try

                                cell2 = outws.Cells(outrow, 0)
                                cell2.Columns.Hidden = True
                                For col = 1 To 255
                                    cell = ws.Cells(row, col)
                                    cell2 = outws.Cells(outrow, col)
                                    CopyBorders(cell, cell2)
                                    If Not cell.Value Is Nothing Then
                                        If cell.Formula.Substring(0, 1) <> "=" And cell.Value.ToString().Substring(0, 1) = "[" Then
                                            Dim V As String
                                            V = cell.Value.ToString()
                                            V = PrepareV(V)
                                            Try
                                                cell2.Value = dt_itog.Rows(dt_itog.Rows.Count - 1)(V)
                                            Catch ex As Exception
                                                cell2.Value = ex.Message
                                            End Try
                                        Else
                                            Dim f As String
                                            f = cell2.Formula
                                            If f <> "" Then
                                                If f.Substring(0, 1) = "=" Then
                                                    If lastDetailRow <> -1 And firstDetailRow <> -1 Then
                                                    If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                                        f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                                        cell2.Formula = f
                                                    End If
                                                End If
                                            End If

                                            End If
                                        End If
                                    End If

                                Next
                                outrow += 1
                            Else ' end [Report:Итоговые]

                                If cell.Value.ToString = "[Report:Прогноз]" Then
                                    Dim tt As TArchive
                                    tt = New TArchive
                                    clearTarchive(tt)





                                    cell = ws.Range(row, 1, row, 255)
                                    cell2 = outws.Range(outrow, 1, outrow, 255)
                                    Try
                                        cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                                        cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                        cell.Copy(cell2, PasteType.Formats, PasteOperation.None, True, False)
                                    Catch ex As Exception
                                        cell.Copy(cell2)
                                    End Try

                                    cell2 = outws.Cells(outrow, 0)
                                    cell2.Columns.Hidden = True
                                    For col = 1 To 255
                                        cell = ws.Cells(row, col)
                                        cell2 = outws.Cells(outrow, col)
                                        CopyBorders(cell, cell2)
                                        Dim days As Integer
                                        days = 7
                                        If Not cell.Value Is Nothing Then

                                            If cell.Formula.Substring(0, 1) <> "=" And cell.Value.ToString().Substring(0, 1) = "[" Then

                                                If cell.Value = "[Период прогноза+]" Then
                                                    Dim S As String
                                                    Dim BOM As Date
                                                    BOM = dto
                                                    BOM = BOM.AddDays(-BOM.Day + 1)
                                                    BOM = BOM.AddMonths(1)
                                                    BOM = BOM.AddDays(-1)
                                                    days = Math.Abs(DateDiff(DateInterval.Day, BOM, dto))
                                                    If days = 0 Then days = 1
                                                    S = "По среднему с " + dto.ToString("dd.MM") + " по " + BOM.ToString("dd.MM") + " (+)"
                                                    cell2.Value = S
                                                Else
                                                    Dim V As String
                                                    V = cell.Value.ToString()
                                                    V = PrepareV(V)
                                                    Dim ca As String
                                                    ca = cell.Address
                                                    ca = ca.Replace((row + 1).ToString, "")
                                                    ca = ca.Replace("$", "")
                                                    cell2.Formula = "=sum(" + ca + (lastDetailRow + 1).ToString + ":" + ca + (lastDetailRow - 6 + 1).ToString + ")/7*" + days.ToString

                                                    SetByName(tt, V, cell2.Value)

                                                End If


                                            Else
                                                Dim f As String
                                                f = cell2.Formula
                                                If f <> "" Then
                                                    If f.Substring(0, 1) = "=" Then
                                                        If lastDetailRow <> -1 And firstDetailRow <> -1 Then
                                                        If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                                            f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                                            cell2.Formula = f
                                                        End If
                                                    End If
                                                End If

                                                End If
                                            End If
                                        End If

                                    Next



                                    ' Сохранить данные в таблице datacurr с типом 5


                                    ' fill data from cells ????
                                    tvMain.QueryExec(ClearDB(dto, dto, 5, id.ToString))
                                    tvMain.QueryExec(WriteTArchToDB(id.ToString, tt))

                                    outrow += 1 '"[Report:Прогноз]"

                                Else
                                    If cell.Value.ToString = "[Report:ПредПрогноз]" Then

                                        dt_prognoz = GetData(id, 5)
                                        cell = ws.Range(row, 1, row, 255)
                                        cell2 = outws.Range(outrow, 1, outrow, 255)
                                        Try
                                            cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                                            cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                            cell.Copy(cell2, PasteType.Formats, PasteOperation.None, True, False)
                                        Catch ex As Exception
                                            cell.Copy(cell2)
                                        End Try

                                        cell2 = outws.Cells(outrow, 0)
                                        cell2.Columns.Hidden = True
                                        For col = 1 To 255
                                            cell = ws.Cells(row, col)
                                            cell2 = outws.Cells(outrow, col)
                                            CopyBorders(cell, cell2)
                                            If Not cell.Value Is Nothing Then
                                                If cell.Formula.Substring(0, 1) <> "=" And cell.Value.ToString().Substring(0, 1) = "[" Then
                                                    If cell.Value = "[Период прогноза-]" Then
                                                        Dim S As String
                                                        Dim eom As Date
                                                        Dim BOM As Date
                                                        eom = dto
                                                        eom = eom.AddDays(-eom.Day)
                                                        BOM = dto
                                                        BOM = BOM.AddMonths(-1)


                                                        S = "По среднему с " + BOM.ToString("dd.MM") + " по " + eom.ToString("dd.MM") + " (-)"
                                                        cell2.Value = S
                                                    Else

                                                        Dim V As String
                                                        V = cell.Value.ToString()
                                                        V = PrepareV(V)
                                                        If dt_prognoz.Rows.Count > 0 Then
                                                            Try
                                                                cell2.Value = dt_prognoz.Rows(0)(V)
                                                            Catch ex As Exception
                                                                cell2.Value = ex.Message
                                                            End Try
                                                        Else
                                                            cell2.Value = 0
                                                        End If

                                                    End If

                                                Else
                                                    Dim f As String
                                                    f = cell2.Formula
                                                    If f <> "" Then
                                                        If f.Substring(0, 1) = "=" Then
                                                            If lastDetailRow <> -1 And firstDetailRow <> -1 Then
                                                            If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                                                f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                                                cell2.Formula = f
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            End If

                                        Next
                                        outrow += 1 ' "[Report:ПредПрогноз]"
                                    Else
                                        cell = ws.Range(row, 1, row, 255)
                                        cell2 = outws.Range(outrow, 1, outrow, 255)
                                        Try
                                            cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                                            cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                                        Catch ex As Exception
                                            cell.Copy(cell2)
                                        End Try

                                        cell2 = outws.Cells(outrow, 0)
                                        cell2.Columns.Hidden = True
                                        For col = 1 To 255
                                            cell = ws.Cells(row, col)
                                            cell2 = outws.Cells(outrow, col)
                                            CopyBorders(cell, cell2)
                                            If Not cell.Value Is Nothing Then

                                                Dim f As String
                                                f = cell2.Formula
                                                'If f <> "" Then
                                                '    If f.Substring(0, 1) = "=" Then
                                                '        'If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                                '        'f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                                '        cell2.Formula = f
                                                '        'End If
                                                '    End If
                                                'End If

                                            End If

                                        Next

                                        outrow += 1 ' просто строка

                                    End If

                                End If

                            End If
                        End If
                    End If
                Else ' cell 0 is nothing
                    cell = ws.Range(row, 1, row, 255)
                    cell2 = outws.Range(outrow, 1, outrow, 255)


                    Try
                        cell.Copy(cell2, PasteType.All, PasteOperation.None, True, False)
                        cell.Copy(cell2, PasteType.ColumnWidths, PasteOperation.None, True, False)
                    Catch ex As Exception
                        cell.Copy(cell2)
                    End Try

                    cell2 = outws.Cells(outrow, 0)
                    cell2.Columns.Hidden = True
                    For col = 1 To 255
                        cell = ws.Cells(row, col)
                        cell2 = outws.Cells(outrow, col)
                        CopyBorders(cell, cell2)
                        If Not cell.Value Is Nothing Then

                            Dim f As String
                            f = cell2.Formula
                            If f <> "" Then
                                If f.Substring(0, 1) = "=" Then
                                    If lastDetailRow <> -1 And firstDetailRow <> -1 Then
                                    If InStr(f, (lastDetailRow).ToString) >= 0 Then
                                        f = f.Replace((lastDetailRow).ToString, (firstDetailRow + 1).ToString)
                                        cell2.Formula = f
                                    End If
                                End If
                            End If
                        End If
                        End If

                    Next
                    outrow += 1
                End If ' просто строка

            Next ' excel row ... 


            'Catch ex As Exception

            'End Try

            outworkbook.Save()
            outworkbook.Close()
        End If

        Return True

    End Function

    Private Function PrepareV(ByVal V1 As String) As String
        Dim V As String
        V = V1
        V = V.Replace("Сообщение НС", "HC")
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
        'maskWOnum = 'dQ|dT  |Сообщение НС|Время Опроса|HC|';
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




    
    Private xlsDay As String
    Private xlsHour As String
    Private xlsDayPage As String
    Private xlsHourPage As String
    Private xlsDevtype As String


    
End Class