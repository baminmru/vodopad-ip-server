Option Explicit

Sub SplitTable()

    Dim r As Range, cr As Range
    Dim i As Integer, j As Integer, d As Integer, ins_str As Integer
    Dim copy_r As Integer, vtype As Integer, k As Integer
    Dim f_str As Integer, l_str As Integer, t_str As Integer, s_str As String
    Dim q_str As Integer, copy_date As Integer
    Dim vval As Variant, val As String, over As String
    Dim dt As Date
            
    Set r = ActiveWorkbook.ActiveSheet.UsedRange
    
    d = 0
    ins_str = 0
    l_str = 0
    copy_date = 13
    For i = 1 To r.Rows.Count
'        If (r.Cells(i, 1).Value = "[Report:Архив]") Then
            vval = r.Cells(i, 2).Value
            vtype = VarType(vval)
            If vtype = vbDate Then
                vval = DatePart("d", vval)
                If d < vval Then
                    If d = 0 Then
                        f_str = i
                    End If
                    d = vval
                Else
                    If ins_str = 0 Then
                        ins_str = i
                    End If
                End If
'            End If
        Else
            If d > 0 And l_str = 0 Then
                l_str = i - 1
            End If
            If (r.Cells(i, 2).Value = "Фактическое потребление за предыдущий отчетный период") Then
                copy_r = r.Columns.Count
                t_str = i
                For j = 3 To r.Columns.Count
                    vval = r.Cells(i + 2, j).Value
                    If vval = Empty Then
                        copy_r = j - 1
                        Exit For
                    End If
                    vval = r.Cells(i, j).Value
                    If vval = "{}" Then
                        copy_date = j
                    End If
                Next
            End If
            If (r.Cells(i, 2).Value = "Итого по приборам учета:") Then
                s_str = i
            End If
        End If
    Next
    q_str = l_str - f_str + 1
    If ins_str > 0 Then
        With ActiveWorkbook.ActiveSheet
            .Range(.Cells(ins_str, 2), .Cells(ins_str, 2)).Select
        End With
        For i = 1 To 6
            Selection.EntireRow.Insert
        Next
        l_str = l_str + 6
        s_str = s_str + 6
        With ActiveWorkbook.ActiveSheet
            Set cr = .Range(.Cells(ins_str, 2), .Cells(ins_str, 2))
            .Range(.Cells(l_str + 1, 2), .Cells(l_str + 2, copy_r)).Copy Destination:=cr
            Set cr = .Range(.Cells(ins_str + 2, 2), .Cells(ins_str + 2, 2))
            .Range(.Cells(t_str, 2), .Cells(t_str + 3, copy_r)).Copy Destination:=cr
        End With
        vval = r.Cells(f_str, 2).Value
        val = "с " + Format(vval, "dd.mm.yyyy") + " по "
        vval = r.Cells(ins_str - 1, 2).Value
        val = val + Format(vval, "dd.mm.yyyy")
        r.Cells(t_str, copy_date).Value = val
        vval = r.Cells(ins_str + 6, 2).Value
        val = "с " + Format(vval, "dd.mm.yyyy") + " по "
        vval = r.Cells(l_str, 2).Value
        val = val + Format(vval, "dd.mm.yyyy")
        r.Cells(ins_str + 2, copy_date).Value = val
        For i = ins_str To ins_str + 1
            For j = 3 To copy_r
                vval = r.Cells(i, j).Formula
                If vval <> "" Then
                    With ActiveWorkbook.ActiveSheet
                        Set cr = .Range(.Cells(f_str, j), .Cells(ins_str - 1, j))
                    End With
                    val = cr.Address(RowAbsolute:=False, ColumnAbsolute:=False)
                    vval = Left(vval, InStr(vval, "(")) + val + ")"
                    r.Cells(i, j).Formula = vval
                End If
            Next
        Next
        For i = l_str + 1 To l_str + 2
            k = i + s_str - l_str
            For j = 3 To copy_r
                vval = r.Cells(i, j).Formula
                If vval <> "" Then
                    With ActiveWorkbook.ActiveSheet
                        Set cr = .Range(.Cells(ins_str + 6, j), .Cells(l_str, j))
                    End With
                    val = cr.Address(RowAbsolute:=False, ColumnAbsolute:=False)
                    over = Left(vval, InStr(vval, "("))
                    vval = over + val + ")"
                    r.Cells(i, j).Formula = vval
                    With ActiveWorkbook.ActiveSheet
                        Set cr = .Range(.Cells(f_str, j), .Cells(ins_str - 1, j))
                    End With
                    
                    If over = "=SUM(" Then
                        vval = over + cr.Address(RowAbsolute:=False, ColumnAbsolute:=False) + ")+SUM(" + val + ")"
                    Else
                        vval = "=(SUM(" + cr.Address(RowAbsolute:=False, ColumnAbsolute:=False) + ")+SUM(" + val + "))/" + Str(q_str)
                    End If
                    
                    'val = cr.Address(RowAbsolute:=False, ColumnAbsolute:=False) + ";" + val
                    'r.Cells(k, j).Formula = vval
                    'vval = over + val + ")"
                    r.Cells(k, j).Formula = vval
                End If
            Next
        Next
    End If
End Sub

