Attribute VB_Name = "P_���������_P"
Option Explicit
'
Global Const ����_����� = 219
Global Const ����_������ = 221
Global Const ����_����� = 222


Global ������������� As Boolean
Private Exl As Excel.Application
Private Wbk As Excel.Workbook

Const iMax = 256

'   ******************************************
'   * ���������� ���������� �� ����� ������� *
'   ******************************************
''
Public Function ��������������(ByVal ������� As String) As Boolean
' ���������� True ��� �������� ��������
    On Error Resume Next
    Set Exl = New Excel.Application
    Set Wbk = Exl.Workbooks.Open(�������������(�������, ����������(����_�������)))
    If Err <> 0 Then
        MsgBox "�� ������ ������� ���� �������!", vbOKOnly, "��������!"
        Wbk.Close
        Exl.Quit
        Exit Function
      End If
    ������������� = True
    �������������� = True
End Function

Public Sub ��������������()
    On Error Resume Next
    Wbk.Close
    Exl.Quit
    Set Exl = Nothing
    ������������� = False
End Sub

Public Function ����������������() As Boolean
    On Error Resume Next
    Wbk.Save
    ���������������� = (Err <> 0)
End Function

Public Function ��������������_P(ByVal ����� As Long) As Variant
' ��������� ����� ��������� � ����� �������
Dim i As Integer
Dim Q
        If Not ������������� Then Exit Function
    With Wbk.Worksheets(1)
    i = 0
    Do
        i = i + 1
        Q = .Cells(i, 1)
        If (Q = �����) Then
            ��������������_P = .Cells(i, 4)
            Exit Do
          End If
      Loop While i <= iMax
  End With
End Function

Public Function ��������������_P(ByVal ����� As Long, ByVal ��������) As Boolean
' ���������� �������� ��������� � ����� �������.
' ���� ��������� � ����� ������� ���, ���������� False
Dim i As Integer
Dim Q
        If Not ������������� Then Exit Function
    With Wbk.Worksheets(1)
    i = 0
    Do
        i = i + 1
        Q = .Cells(i, 1)
        If (Q = �����) Then
            .Cells(i, 4) = ��������
            ��������������_P = True
            Exit Do
          End If
      Loop While i <= iMax
  End With
End Function


