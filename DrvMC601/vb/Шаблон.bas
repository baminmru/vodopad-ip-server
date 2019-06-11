Attribute VB_Name = "������"
Option Explicit

Dim mStr As Long, mPos As Long          ' ������ ������� �������

' ������ �������
Global Const Sect_All = 0
Global Const Sect_Inf = 1
Global Const Sect_Hdr = 2
Global Const Sect_Tbl = 3
Global Const Sect_Sum = 4
Global Const Sect_Cur = 5
Global Const Sect_Mem = 6
Global Const Sect_End = 7

Global Const mSect = 7                  ' ����� ������
Global Sect(1 To mSect, 1 To 2) As Long    ' ��������� � ������ ������
Const ��������� = "����      �����     �������   �����     �������   ��������� �����     "
'                 "1234567890123456789012345678901234567890123456789012345678901234567890"

' ������ ������� (��� �������������� ������)
Const �������� = "���"
Const �������� = "���"
Global TblNum As Long      ' ����� ����� (�����)
Global TblBeg As Long      ' �������� ������ ������
Global TblDat As Long      ' ����� ����� ������
Global TblLen As Long      ' ������ ������

' ������ ������� (��� �������������� ������)
Global Const mTab = 32
Global kTab As Long                        ' ����� ��������  ��� ������ � �������
Global sTab(1 To 2, 1 To mTab) As Long     ' �������, �������

' *****************
' *** ��������� ***
' *****************
Public Sub ����������������(Wrs As Excel.Worksheet)
' ���������� �������� ������� ������� (����� ��������� ��������)
Dim i As Long, k As Long
Dim r As Excel.Range, Q As Excel.Range
Dim s As String
  ' �������� ��� ������
    For i = 1 To mSect
        Sect(i, 1) = 0
        Sect(i, 2) = 0
      Next i
  ' ���������� ������� �������
    mStr = Wrs.UsedRange.Row + Wrs.UsedRange.Rows.Count - 1
    mPos = Wrs.UsedRange.Column + Wrs.UsedRange.Columns.Count - 1
  ' ���� ���������
  With Wrs
    ' ������ ��������� ������ ���� "����"
    If .Cells(1, 1) <> Trim(Mid(���������, 1, 10)) Then Exit Sub  ' ��� �� ������!
    Sect(Sect_Inf, 1) = 1
    ' ���������� ��������� ���������
    Set r = .Range(.Cells(Sect_Inf, 1), .Cells(mStr, 1))
    For i = 2 To mSect
        s = Trim(Mid(���������, 10 * (i - 1) + 1, 10))
        Set Q = r.Find(s, LookAt:=xlWhole)
        If Not (Q Is Nothing) Then
            Sect(i, 1) = Q.Row
            Set r = .Range(Q, .Cells(mStr, 1))
          End If
      Next i
    End With
  ' ��������� ������� ������
    k = mStr + 1
    For i = mSect To 1 Step -1
        If Sect(i, 1) <> 0 Then
            Sect(i, 2) = k - Sect(i, 1) - 1
            k = Sect(i, 1)
          End If
      Next i
End Sub

' ****************
' *** �������� ***
' ****************
'   �������� ���� ��������� � �����: ����� ������ ����� �� ��������
'
Public Sub �������������(Wrs As Excel.Worksheet, ������ As Integer)
' ������� ��� ������ ������ � ����������
Dim i As Long, k As Long
  With Wrs
    i = Sect(������, 1)
        If i = 0 Then Exit Sub
    k = Sect(������, 2)
    .Range(.Cells(i, 1), .Cells(i + k, 1)).EntireRow.Delete
    End With
End Sub

Public Sub �������������������(Wrs As Excel.Worksheet, ByVal ����� As Integer, ByVal ������ As Integer)
' ������� ��������� ������. ����� -- �������� �� ��������� (��������� ����� ����� 0)
Dim i As Long
  With Wrs
    i = Sect(������, 1)
        If i = 0 Then Exit Sub
    .Cells(i + �����, 1).EntireRow.Delete
    Sect(������, 2) = Sect(������, 2) - 1
    End With
End Sub

Public Sub ����������������(Wrs As Excel.Worksheet, ByVal ������ As Integer)
' ������� ������ ��������� ������
Dim i As Long
  With Wrs
    i = Sect(������, 1)
        If i = 0 Then Exit Sub
    .Cells(i, 1).EntireRow.Delete
    End With
End Sub

Public Sub ��������������������(Wrs As Excel.Worksheet)
  With Wrs
    .Cells(1, 1).EntireColumn.Delete
    End With
End Sub

' ******************
' *** ������ ***
' ******************
Public Sub �������������������(Wrs As Excel.Worksheet, ByVal ������ As Integer, ByVal �������������� As Integer)
' ��������� ���� ������� � ��������� ��� ������
Dim i As Integer, j As Integer
Dim s As String
    With Wrs
    If ������ = Sect_All Then
        For i = 1 To mStr
        For j = 1 To mPos
            If Not IsError(.Cells(i, j)) Then
                s = .Cells(i, j)
                If �����������������(s, ��������������) Then .Cells(i, j) = s
              End If
          Next j
          Next i
      Else
        If Sect(������, 1) = 0 Then Exit Sub
        If Sect(������, 2) = 0 Then Exit Sub
        For i = Sect(������, 1) + 1 To Sect(������, 1) + Sect(������, 2)
        For j = 2 To mPos
            If Not IsError(.Cells(i, j)) Then
                s = .Cells(i, j)
                If �����������������(s, ��������������) Then .Cells(i, j) = s
              End If
          Next j
          Next i
      End If
  End With
End Sub

' *************************
' *** ��������� ������� ***
' *************************
Public Sub �������������(Wrs As Excel.Worksheet)
' ����������� ������ ��������� ��� ������ � �������
Dim ii As Long, jj As Long
Dim i As Long, k As Long
Dim s As String
  With Wrs
    kTab = 0
    k = Sect(Sect_Tbl, 1)
        If k = 0 Then Exit Sub
    i = 2
    Do While i <= mPos
        s = .Cells(k, i)
            If s = "" Then GoTo 1
        s = ����������(s, 1, ii, jj)
            If s = "" Then GoTo 1
            If Mid(s, 1, 1) <> "S" Then GoTo 1
        s = Mid(s, 2)
            If Not IsNumeric(s) Then GoTo 1
        kTab = kTab + 1
        sTab(1, kTab) = i
        sTab(2, kTab) = CLng(s)
1:      i = i + 1
      Loop
    End With
End Sub

Public Sub �������������(Wrs As Excel.Worksheet)
' ��������� �������� �� ������ ������ (���) ������ � ������ ������ ������ �� ����� (���)
' � ������ ������ ��� ������ ������ ���� ������ �������
Dim r As Excel.Range, Q As Excel.Range
Dim i As Long, k As Long
    TblBeg = 0
    TblDat = 0
    TblLen = 0
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    TblLen = Sect(Sect_Tbl, 2)
  With Wrs
    Set r = .Range(.Cells(i + 1, 1), .Cells(i + TblLen, 1))
    Set Q = r.Find(��������, LookAt:=xlWhole)
        If Q Is Nothing Then Exit Sub
    TblBeg = Q.Row - i
    Set r = .Range(Q, .Cells(i + TblLen, 1))
    Set Q = r.Find(��������, LookAt:=xlWhole)
        If Q Is Nothing Then Exit Sub
    TblDat = Q.Row - i - TblBeg
  End With
End Sub


' ************************************
' *** ���������� ������ ���������� ***
' ************************************
' ���������� ������, � ������� ����� ���������� ������.
'
Public Sub �������������������������(Wrs As Excel.Worksheet, ByVal ����� As Long)
' ������� ��������� �����, ������� ������ ������ ������ ����� ���
Dim i As Long, k As Long
        If ����� <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + 1
  With Wrs
    ' ������� ��� ������, ����� ������
    k = Sect(Sect_Tbl, 2) - 1
    If k > 0 Then
        .Range(.Cells(i + 1, 1), .Cells(i + k, 1)).EntireRow.Delete
      End If
    ' �������� ���� ������ ������, � ������� ������� ������
    If kTab <> 0 Then
        For k = 1 To kTab
            .Cells(i, sTab(1, k)) = Null
          Next k
      End If
    ' �������� 1-� ������ �����-1 ���
    If ����� > 1 Then
        For k = 1 To ����� - 1
            .Cells(i, 1).EntireRow.Copy
            i = i + 1
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    End With
    ' ������������ ��������� ������
    ���������������� Wrs
End Sub

Public Sub �������������������������(Wrs As Excel.Worksheet, ByVal ����� As Long)
' ������� ��������� �����, ������� ���� ������� ������ ����� ���
Dim i As Long, k As Long
        If ����� <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + TblBeg
  With Wrs
    ' �������� �������, � ������� ������� ������
    If kTab <> 0 Then
        For k = 1 To kTab
        .Range(.Cells(i, sTab(1, k)), .Cells(i + TblDat, sTab(1, k))) = Null
'            .Cells(i, sTab(1, k)) = Null
          Next k
      End If
    ' �������� ���� �����-1 ���
    i = Sect(Sect_Tbl, 1) + 1
    If ����� > 1 Then
        For k = 1 To ����� - 1
            .Range(.Cells(i, 1), .Cells(i + TblLen - 1, 1)).EntireRow.Copy
            i = i + TblLen
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    End With
    ' ������������ ��������� ������
    ���������������� Wrs
End Sub

' ********************************
' *** ���������� ������ ������ ***
' ********************************
' ��������� ������� ������ (������, ����������� �� ������� ������)
'
Public Sub ������������������������(Wrs As Excel.Worksheet, ByVal ����� As Long)
Dim i As Long, k As Long
        If ����� <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + 1
  With Wrs
    ' ������� ��� ������, ����� ������
    k = Sect(Sect_Tbl, 2) - 1
    If k > 0 Then
        .Range(.Cells(i + 1, 1), .Cells(i + k, 1)).EntireRow.Delete
      End If
    ' �������� 1-� ������ �����-1 ���
    If ����� > 1 Then
        For k = 1 To ����� - 1
            .Cells(i, 1).EntireRow.Copy
            i = i + 1
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    End With
    ' ������������ ��������� ������
    ���������������� Wrs
End Sub

Public Sub �������������������������(Wrs As Excel.Worksheet, ByVal ����� As Long)
' ��������� ������������ ������� �������.
Dim r As Excel.Range
Dim i As Long, k As Long
        If ����� <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + 1
  With Wrs
  ' ��������� �������
    ' ������� ��� ������, ����� ������
    k = Sect(Sect_Tbl, 2) - 1
    If k > 0 Then
        .Range(.Cells(i + 1, 1), .Cells(i + k, 1)).EntireRow.Delete
      End If
    ' �������� 1-� ������ � �������� �
    If TblLen > 1 Then
        .Cells(i, 1).EntireRow.Copy
        i = i + 1
        .Cells(i, 1).EntireRow.Insert
        .Range(.Cells(i, 2), .Cells(i, mPos)) = Null
       End If
    ' ��������� ��� TblLen-2 �����
    If TblLen > 2 Then
        For k = 1 To TblLen - 2
            .Cells(i, 1).EntireRow.Copy
            i = i + 1
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
  ' �������� "����������" ���� �����-1 ���
    i = Sect(Sect_Tbl, 1) + 1
    If ����� > 1 Then
        For k = 1 To ����� - 1
            .Range(.Cells(i, 1), .Cells(i + TblLen - 1, 1)).EntireRow.Copy
            i = i + TblLen
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    ' ������� �������
    If TblLen > 1 Then
        For k = 1 To �����
            .Range(.Cells(i + 1, 1), .Cells(i + TblLen - 1, 1)).EntireRow.Delete
            i = i - TblLen
          Next k
      End If
    End With
    ' ������������ ��������� ������
    ���������������� Wrs
End Sub

Public Sub �����������������(Wrs As Excel.Worksheet)
' ������ � ��������� � ������� ��������� ������ (Sect_Mem)
Dim i As Integer, j As Integer
Dim s As String
        If Sect(Sect_Mem, 1) = 0 Then Exit Sub
        If Sect(Sect_Mem, 2) = 0 Then Exit Sub
  With Wrs
    For i = Sect(Sect_Mem, 1) + 1 To Sect(Sect_Mem, 1) + Sect(Sect_Mem, 2)
        s = .Cells(i, 1)
        If IsNumeric(s) Then
            ��������������_P CInt(s), .Cells(i, 3)
          End If
      Next i
    End With
    ����������������
End Sub

