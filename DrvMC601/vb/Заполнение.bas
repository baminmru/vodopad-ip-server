Attribute VB_Name = "����������"
Option Explicit
'  ��������� ���� ��������� �������� ������� �� ��������
'
Public Function �����������������(Wrs As Excel.Worksheet, ByVal �� As Date, ByVal ����� As Integer) As Integer
Dim i As Integer, j As Integer, k As Integer
Dim ������� As Integer, iDay As Integer
Dim iStr As Integer, iPos As Integer
Dim ���� As Date
Dim a
  With Wrs
        If kTab = 0 Then Exit Function
    ���� = �� - ����� + 1
    iDay = Sect(Sect_Tbl, 1) + TblBeg
    For k = 1 To �����
        For i = 1 To kTab               ' ���� �� ���������
         If �������������� Then
            ����������������� = 1
            Exit Function
          End If
           ������� = sTab(2, i)
            iPos = sTab(1, i)
            iStr = iDay
            ' *************************************
            a = MC601_��������������(�������, ����)
            ' *************************************
 ������������ (1)
            For j = 24 To 1 Step -1             ' ���������� � ����
                .Cells(iStr, iPos) = a(j)
                iStr = iStr + 1
              Next j
          Next i
        iDay = iDay + TblLen
        ���� = ���� + 1
      Next k
  End With
End Function

Public Function ������������������(Wrs As Excel.Worksheet, ByVal ������ As Integer, ByVal ����� As Integer) As Integer
' ��������� ������ ������ � ���� ������� (� �������� �������)
' ���������� ������� ������. 0 -- ������ ������ �������
Dim i As Integer, j As Integer
Dim ������� As Integer, ������� As Integer
Dim iStr As Integer, iPos As Integer
Dim a
  With Wrs
        If kTab = 0 Then Exit Function
    For i = 1 To kTab
        If �������������� Then
            ������������������ = 1
            Exit Function
          End If
        iPos = sTab(1, i)
        ������� = sTab(2, i)
        ' *************************************
        a = MC601_���������������(�������, ������, �����, �������)
        ' *************************************
'            If n = 0 Then Exit Sub
        If ������� > ����� Then ������� = �����
 ������������ (�������)
        iStr = Sect(Sect_Tbl, 1) + �����
        For j = 1 To �������
            If ������� = 1003 Then
                If a(j) = 0 Then
                    .Cells(iStr, iPos) = Null
                  Else
                    .Cells(iStr, iPos) = YYMMDD2Date(a(j))
                  End If
              Else
                .Cells(iStr, iPos) = a(j)
              End If
            iStr = iStr - 1
          Next j
      Next i
  End With
End Function

Public Function ������������������(Wrs As Excel.Worksheet, ByVal ������ As Integer, ByVal ����� As Integer) As Integer
' ��������� ������ ������ � ���� ������� (� �������� �������)
' ���������� ������� ������. 0 -- ������ ������ �������
Dim i As Integer, j As Integer
Dim ������� As Integer, ������� As Integer
Dim iStr As Integer, iPos As Integer
Dim a
  With Wrs
        If kTab = 0 Then Exit Function
    For i = 1 To kTab
         If �������������� Then
            ������������������ = 1
            Exit Function
          End If
        iPos = sTab(1, i)
        ������� = sTab(2, i)
        ' *************************************
        a = MC601_���������������(�������, ������, �����, �������)
        ' *************************************
'            If n = 0 Then Exit Sub
        If ������� > ����� Then ������� = �����
 ������������ (�������)
        iStr = Sect(Sect_Tbl, 1) + �����
        For j = 1 To �������
            If ������� = 1003 Then
                If a(j) = 0 Then
                    .Cells(iStr, iPos) = Null
                  Else
                    .Cells(iStr, iPos) = YYMMDD2Date(a(j))
                  End If
              Else
                .Cells(iStr, iPos) = a(j)
              End If
            iStr = iStr - 1
          Next j
      Next i
  End With
End Function

