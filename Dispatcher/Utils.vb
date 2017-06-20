Imports System.Data
Imports System.IO
Imports System.Data.Common
Imports System.Xml
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports VB = Microsoft.VisualBasic
Imports Oracle.DataAccess.Client

Public Class Utils
    'Public Shared Sub LoadFileToField(ByVal filepath As String, ByVal table As String, ByVal field As String, ByVal idField As String, ByVal RowID As String)


    '    If filepath <> "" Then
    '        Dim file As IO.FileStream
    '        Try
    '            Dim strSQL As String = _
    '                    "UPDATE " + table + " SET " + field + " = :Data WHERE " + idField + "=:ID"
    '            Dim cmd As System.Data.Common.DbCommand

    '            cmd = New OracleCommand
    '            cmd.Connection = TvMain.dbconnect
    '            cmd.CommandType = CommandType.Text
    '            cmd.CommandText = strSQL
    '            Dim aBytes() As Byte
    '            file = New IO.FileStream(filepath, IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
    '            aBytes = Array.CreateInstance(GetType(Byte), file.Length)
    '            file.Read(aBytes, 0, file.Length)
    '            cmd.Parameters.Add(New OracleParameter("Data", OracleDbType.Blob))
    '            cmd.Parameters.Add(New OracleParameter("ID", RowID))
    '            cmd.Parameters("Data").Value = aBytes
    '            cmd.ExecuteNonQuery()
    '            file.Close()
    '        Catch ex As Exception

    '        Finally
    '        End Try
    '    Else
    '        Dim strSQL As String = _
    '                    "UPDATE " + table + " SET " + field + " = null WHERE " + idField + " = :ID"
    '        Dim cmd As System.Data.Common.DbCommand
    '        cmd = New OracleCommand
    '        cmd.Connection = TvMain.dbconnect
    '        cmd.CommandType = CommandType.Text
    '        cmd.CommandText = strSQL
    '        cmd.Parameters.Add(New OracleParameter("ID", RowID))
    '        cmd.ExecuteNonQuery()
    '    End If

    'End Sub

    'Public Shared Function SaveFileFromField(ByVal filepath As String, ByVal table As String, ByVal field As String, ByVal idField As String, ByVal RowID As String) As Long
    '    Dim fs As FileStream                 ' Writes the BLOB to a file (*.bmp).
    '    Dim bw As BinaryWriter               ' Streams the binary data to the FileStream object.
    '    Dim bufferSize As Long = 32000      ' The size of the BLOB buffer.
    '    Dim outbyte(bufferSize - 1) As Byte  ' The BLOB byte() buffer to be filled by GetBytes.
    '    Dim retval As Long                   ' The bytes returned from GetBytes.
    '    Dim startIndex As Long = 0           ' The starting position in the BLOB output.
    '    Dim cmd As System.Data.Common.DbCommand
    '    cmd = New OracleCommand
    '    cmd.Connection = TvMain.dbconnect
    '    cmd.CommandType = CommandType.Text
    '    cmd.CommandText = "select " + field + " from " + table + " where " + idField + "=" + RowID
    '    Dim myReader As OracleDataReader = Nothing
    '    Try
    '        fs = New FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)
    '    Catch
    '        Return 0
    '    End Try
    '    Try
    '        SyncLock cmd.Connection
    '            myReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
    '            Do While myReader.Read()

    '                bw = New BinaryWriter(fs)
    '                startIndex = 0
    '                retval = myReader.GetBytes(0, startIndex, outbyte, 0, bufferSize)
    '                Do While retval = bufferSize
    '                    bw.Write(outbyte)
    '                    bw.Flush()
    '                    startIndex += bufferSize
    '                    Try
    '                        retval = myReader.GetBytes(0, startIndex, outbyte, 0, bufferSize)
    '                    Catch ex As Exception
    '                        retval = 0
    '                    End Try
    '                Loop
    '                'bw.Write(outbyte, 0, retval - 1)
    '                bw.Write(outbyte, 0, retval)
    '                bw.Flush()
    '                bw.Close()
    '            Loop
    '        End SyncLock
    '    Catch

    '    Finally
    '        If (Not myReader Is Nothing) Then
    '            myReader.Close()
    '            myReader.Dispose()
    '        End If
    '        If (Not cmd Is Nothing) Then
    '            cmd.Dispose()
    '        End If
    '    End Try

    '    Try
    '        fs.Close()
    '    Catch
    '    End Try
    '    Return retval
    'End Function

    Public Shared Sub SetupGrid(ByRef Gr As Infragistics.Win.UltraWinGrid.UltraGrid, ByVal id_bd As Integer, ByVal ptype As Integer)
        Dim dt As DataTable
        Dim cmd As System.Data.Common.DbCommand
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        Dim sqlstr As String = ""
        Select Case ptype
            Case 1
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_curr= ml.id_mask  where ml.colhidden = 0 and b.id_bd = :id"

            Case 3
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_hour= ml.id_mask  where  ml.colhidden = 0 and  b.id_bd = :id"

            Case 4
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_24= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id"

            Case 2
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_sum= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id"
        End Select

        If sqlstr = "" Then Exit Sub


        cmd.CommandText = sqlstr
        cmd.Parameters.Add(New OracleParameter("ID", id_bd))

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()

        ' если поля в шаблоне не заданы просто все вываливаем  в таблицу
        If dt.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim i As Integer
        Dim hasDcounter As Boolean = False
        Dim hasDcall As Boolean = False


        Dim column As Infragistics.Win.UltraWinGrid.UltraGridColumn

        Gr.DisplayLayout.CaptionVisible = DefaultableBoolean.True
        Gr.DisplayLayout.CaptionAppearance.FontData.Bold = DefaultableBoolean.True


        If Gr.DisplayLayout.Bands(0).Columns.Count = 0 Then Return

        Gr.DisplayLayout.Scrollbars = Scrollbars.Both
        Gr.DisplayLayout.Override.WrapHeaderText = DefaultableBoolean.True
        Gr.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid
        Gr.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid
        Gr.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid
        Gr.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid



        With Gr.DisplayLayout.Bands(0)



            For i = 0 To .Columns.Count - 1
                .Columns.Item(i).Hidden = True
                .Columns.Item(i).ExcludeFromColumnChooser = ExcludeFromColumnChooser.True
            Next



            Dim info As RowLayoutColumnInfo
            Dim colsz As Double
            Dim fldFormat As String

            '  сначала проверяем есть ли колонки для дат...
            For i = 0 To dt.Rows.Count - 1
                If UCase(dt.Rows(i)("cfld")) = "DCOUNTER" Then hasDcounter = True
                If UCase(dt.Rows(i)("cfld")) = "DCALL" Then hasDcall = True
            Next


            For i = 0 To dt.Rows.Count - 1
                column = Nothing
                Try
                    column = .Columns.Item(dt.Rows(i)("cfld"))
                Catch
                End Try



                If column Is Nothing Then
                    column = .Columns.Add(dt.Rows(i)("cfld").ToString())
                    column.Key = dt.Rows(i)("cfld")
                End If

                If dt.Rows(i)("CHEADER") + "" <> "" Then
                    column.Header.Caption = dt.Rows(i)("CHEADER")
                Else
                    column.Header.Caption = dt.Rows(i)("CFLD")
                End If

                Try
                    column.RowLayoutColumnInfo.SpanX = dt.Rows(i)("SEQUENCE")
                Catch ex As Exception

                End Try


                If dt.Rows(i)("COLWIDTH").ToString() = "" Then
                    colsz = 60
                Else
                    colsz = dt.Rows(i)("COLWIDTH")
                End If
                If colsz < 15 Then
                    colsz = 15
                End If


                fldFormat = UCase(dt.Rows(i)("COLFORMAT") & "")
                Select Case fldFormat
                    Case "S"
                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.Edit
                        'column.Format = "##,###,####"
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly
                    Case "I"
                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.Integer
                        column.Format = "##,###,####"
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly
                        'column.CellAppearance.BackColor = Color.Cyan
                    Case "N"

                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.Double
                        column.Format = "##,###,####.00"
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly
                    Case "F"

                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.Double
                        column.Format = "##,###,####.000"
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly
                    Case "B"
                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.CheckBox
                        'column.Format = "##,###,####.00"
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly
                    Case "D"
                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.DateTime
                        column.Format = "dd.MM.yyyy HH:mm"
                        column.FormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("ru")
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly
                    Case "T"
                        info = column.RowLayoutColumnInfo
                        info.PreferredCellSize = New Size(colsz, 24)
                        column.MinWidth = colsz

                        column.Style = ColumnStyle.Time
                        column.Format = "dd.MM.yyyy HH:mm:ss"
                        column.FormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("ru")
                        column.CellAppearance.TextHAlign = HAlign.Right
                        column.CellAppearance.TextVAlign = VAlign.Middle
                        column.CellActivation = Activation.ActivateOnly

                End Select

                column.Hidden = False
                column.ExcludeFromColumnChooser = ExcludeFromColumnChooser.False


                '                column.CellActivation = Activation.NoEdit
            Next

            If Not hasDcounter Then
                column = Nothing
                Try
                    column = .Columns.Item("DCOUNTER")
                Catch
                End Try


                If column Is Nothing Then
                    column = .Columns.Add("DCOUNTER")
                    column.Key = "DCOUNTER"
                End If

                column.Header.Caption = "Дата счетчика"
                info = column.RowLayoutColumnInfo
                info.PreferredCellSize = New Size(120, 24)
                column.MinWidth = 120

                column.Style = ColumnStyle.DateTime
                column.Format = "dd.MM.yyyy HH:mm:ss"
                column.FormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("ru")
                column.CellAppearance.TextHAlign = HAlign.Right
                column.CellAppearance.TextVAlign = VAlign.Middle
                column.CellActivation = Activation.ActivateOnly
                column.Hidden = False
                column.ExcludeFromColumnChooser = ExcludeFromColumnChooser.False
            End If

            If ptype = 1 And Not hasDcall Then
                column = Nothing
                Try
                    column = .Columns.Item("DCALL")
                Catch
                End Try


                If column Is Nothing Then
                    column = .Columns.Add("DCALL")
                    column.Key = "DCALL"
                End If

                column.Header.Caption = "Дата опроса"
                info = column.RowLayoutColumnInfo
                info.PreferredCellSize = New Size(120, 24)
                column.MinWidth = 120

                column.Style = ColumnStyle.DateTime
                column.Format = "dd.MM.yyyy HH:mm:ss"
                column.FormatInfo = System.Globalization.CultureInfo.CreateSpecificCulture("ru")
                column.CellAppearance.TextHAlign = HAlign.Right
                column.CellAppearance.TextVAlign = VAlign.Middle
                column.CellActivation = Activation.ActivateOnly
                column.Hidden = False
                column.ExcludeFromColumnChooser = ExcludeFromColumnChooser.False
            End If

        End With


    End Sub



    Public Shared Sub SetupGridMS(ByRef Gr As DataGridView, ByVal id_bd As Integer, ByVal ptype As Integer)
        Dim dt As DataTable
        Dim cmd As System.Data.Common.DbCommand
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        Dim sqlstr As String = ""
        Select Case ptype
            Case 1
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_curr= ml.id_mask  where ml.colhidden = 0 and b.id_bd = :id order by sequence"

            Case 3
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_hour= ml.id_mask  where  ml.colhidden = 0 and  b.id_bd = :id  order by sequence"

            Case 4
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_24= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id  order by sequence"

            Case 2
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_sum= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id  order by sequence"
        End Select

        If sqlstr = "" Then Exit Sub


        cmd.CommandText = sqlstr
        cmd.Parameters.Add(New OracleParameter("ID", id_bd))

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()

        ' если поля в шаблоне не заданы просто все вываливаем  в таблицу
        If dt.Rows.Count = 0 Then
            'Debug.Print("Load witout mask")
            Exit Sub
        Else
            'Debug.Print("Load mask #" & dt.Rows(0)("id_mask").ToString)
        End If



        Dim i As Integer
        Dim hasDcounter As Boolean = False
        Dim hasDcall As Boolean = False


        'Dim cstyle As DataGridViewColumnStyle

        Dim myTextColumn As DataGridViewTextBoxColumn = Nothing
        Dim column As DataGridViewColumn
        With Gr
            For i = 0 To .Columns.Count - 1
                .Columns.Item(i).Visible = False
                .Columns.Item(i).Width = 100
            Next
            'Dim info As RowLayoutColumnInfo
            Dim colsz As Double
            Dim fldFormat As String

            For i = 0 To dt.Rows.Count - 1
                If UCase(dt.Rows(i)("cfld")) = "DCOUNTER" Then hasDcounter = True
                If UCase(dt.Rows(i)("cfld")) = "DCALL" Then hasDcall = True

                column = Nothing
                Try
                    column = .Columns.Item(dt.Rows(i)("cfld"))
                Catch
                End Try

                myTextColumn = Nothing
                Try
                    myTextColumn = column
                Catch
                End Try

                If Not column Is Nothing Then
                    If dt.Rows(i)("CHEADER") + "" <> "" Then
                        column.HeaderText = dt.Rows(i)("CHEADER") + ""
                    Else
                        column.HeaderText = dt.Rows(i)("CFLD")
                    End If


                    'Try
                    '    Debug.Print("Set sequence for " & dt.Rows(i)("CFLD").ToString & " to " & dt.Rows(i)("SEQUENCE").ToString)
                    'Catch ex As Exception

                    'End Try

                    Try
                        column.DisplayIndex = dt.Rows(i)("SEQUENCE")
                    Catch ex As Exception

                    End Try


                    If dt.Rows(i)("COLWIDTH").ToString() = "" Then
                        colsz = 60
                    Else
                        colsz = dt.Rows(i)("COLWIDTH")
                    End If
                    If colsz < 35 Then
                        colsz = 35
                    End If


                    fldFormat = UCase(dt.Rows(i)("COLFORMAT") & "")
                    Select Case fldFormat
                        Case "S"

                            column.Width = colsz


                        Case "I"
                            column.Width = colsz

                            column.DefaultCellStyle.Format = "##,###,##0"

                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                        Case "N"
                            column.Width = colsz

                            column.DefaultCellStyle.Format = "##,###,##0.00"

                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                        Case "F"
                            column.Width = colsz
                            column.DefaultCellStyle.Format = "##,###,##0.000"

                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                        Case "B"
                            column.Width = colsz

                            column.DefaultCellStyle.Format = "##,###,##0.00"

                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                        Case "D"
                            column.Width = colsz
                            column.DefaultCellStyle.Format = "G"

                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Case "T"
                            column.Width = colsz
                            column.DefaultCellStyle.Format = "G"

                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Case Else
                            column.Width = colsz
                    End Select

                    column.Visible = True
                End If
            Next

            If Not hasDcounter Then
                column = Nothing
                Try
                    column = .Columns.Item("DCOUNTER")
                Catch
                End Try
                If Not column Is Nothing Then
                    column.HeaderText = "Дата счетчика"
                    column.Visible = True
                    column.Width = 120
                    column.DefaultCellStyle.Format = "G"

                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    If ptype = 1 Then
                        column.DisplayIndex = .Columns.Count - 1
                    Else
                        column.DisplayIndex = 0
                    End If
                End If


            End If

            If ptype = 1 And Not hasDcall Then
                column = Nothing
                Try
                    column = .Columns.Item("DCALL")
                Catch
                End Try
                If Not column Is Nothing Then
                    column.HeaderText = "Дата опроса"
                    column.DefaultCellStyle.Format = "G"

                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    column.Visible = True
                    column.Width = 130
                    column.DisplayIndex = 0
                End If

            End If

        End With

        If ptype = 4 Then
            Try
                Gr.Columns("D_EQL_24").Visible = False
                For i = 0 To Gr.RowCount - 1
                    If Gr("D_EQL_24", i).Value = 3 Then
                        Gr.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                    End If
                    If Gr("D_EQL_24", i).Value = 2 Then
                        Gr.Rows(i).DefaultCellStyle.BackColor = Color.Red
                    End If
                    If Gr("D_EQL_24", i).Value < 2 Then
                        Gr.Rows(i).DefaultCellStyle.BackColor = Color.White
                    End If

                Next
            Catch ex As Exception

            End Try
            
        End If


    End Sub



    Public Shared Function ArchFieldList(ByVal id_bd As Integer, ByVal ptype As Integer) As String
        Dim dt As DataTable
        Dim result As String
        Dim cmd As System.Data.Common.DbCommand
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        Dim sqlstr As String = ""
        Select Case ptype
            Case 1
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_curr= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id order by sequence,cfld"

            Case 3
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_hour= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id  order by sequence,cfld"

            Case 4
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_24= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id  order by sequence,cfld"

            Case 2
                sqlstr = "select ml.* from bdevices b join masksline ml on b.id_mask_sum= ml.id_mask  where ml.colhidden = 0 and  b.id_bd = :id  order by sequence,cfld"
        End Select

        If sqlstr = "" Then
            Return "*"
        End If



        cmd.CommandText = sqlstr
        cmd.Parameters.Add(New OracleParameter("ID", id_bd))

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()

        ' если поля в шаблоне не заданы просто все вываливаем  в таблицу
        If dt.Rows.Count = 0 Then
            Return "*"
        End If

        Dim i As Integer
        result = ""
        For i = 0 To dt.Rows.Count - 1
            If result <> "" Then
                result = result + ","
            End If
            result = result + dt.Rows(i)("cfld")
        Next

        If result.ToUpper().IndexOf("DCOUNTER") < 0 Then
            result = "DCOUNTER," + result
        End If

        Select Case ptype
            Case 1
                If result.ToUpper().IndexOf("DCALL") < 0 Then
                    result = "DCALL," + result
                End If
            Case 4
                If Not result.Contains("D_EQL_24") Then
                    result = "D_EQL_24," + result
                End If
        End Select

        Return result
    End Function
End Class
