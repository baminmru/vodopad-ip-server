Attribute VB_Name = "P_���������_I"
Option Explicit


Global Const ����_��� = 1
Global Const ����_��������� = 2
Global Const ����_���������� = 3
Global Const ����_������������ = 4
Global Const ����_����� = 11
Global Const ����_������ = 12
Global Const ����_��������� = 13
Global Const ����_����� = 14
Global Const ����_������ = 15
Global Const ����_����� = 16

Global Const �������� = 20
Global ����(1 To ��������) As String
Const nMax = 256

Public Function ���������(ByVal i As Integer)
    If (i <= 0) Or (i > ��������) Then Exit Function
    ��������� = ����(i)
End Function

Public Function ����������(ByVal ������ As Variant) As Integer
' ������ ���� ������� �� ���������� ������. ���������� 0 � ������ ������.
'
Dim Exl As Excel.Application   ' ��������
Dim Wbk As Excel.Workbook
Dim i As Integer, j As Integer
Dim iErr As Integer, s As String

    ' ������� ���������
    For i = 1 To ��������
        ����(i) = ""
      Next i
    ' ��������� �������� �������
    iErr = 1
    If NoData(������) Then
'        MsgBox "�� ������ ������!", vbOKOnly, "������!"
        GoTo 101
      End If
      
    ' ��������� ������
    iErr = 2
    s = �������������(������, ����������(����_�������))
    On Error Resume Next
    Set Exl = New Excel.Application
    Set Wbk = Exl.Workbooks.Open(s)
    If Err <> 0 Then
        MsgBox "�� ������ ������� ���� �������!" & Chr(10) _
        & "��������� ������������ ����� �����!", vbOKOnly, "������!"
        GoTo 100
      End If
      
    ' ��������� ������
    On Error GoTo 0
    iErr = 3
    With Exl.Worksheets(1)
        If .Cells(1, 1) <> "����" Then
        MsgBox "���, ������, �� ������ --" & Chr(10) _
        & "����������� ������ ""����""!", vbOKOnly, "������!"
        GoTo 100
      End If
      
    ' ������ ������
    iErr = 0
    i = 1
    Do While i < nMax
        i = i + 1
            If .Cells(i, 1) = "�����" Then Exit Do
        s = .Cells(i, 1)
        If IsNumeric(s) Then
            j = CInt(s)
            If (j > 0) And (j <= ��������) Then
                ����(j) = .Cells(i, 3)
              End If
          End If
      Loop
    End With
    
100:
    Wbk.Close
    Exl.Quit
101:
    ���������� = iErr
End Function



