Attribute VB_Name = "��������"
Option Explicit

Public Function ��������������_S(����� As Integer)
    ��������������_S = MC601_��������������(�����)
'    ��������������_S = "SSSSS"
End Function

'Public Function ��������������_Q(����� As Integer)
'    ��������������_Q = "QQQQQ"
'End Function

'Public Function ��������������_P(����� As Integer)
'    ��������������_P = "PPPPP"
'End Function


Public Function ��������������1(������� As Integer, _
                �������� As Integer, ������� As Integer, ByRef ������� As Integer) As Variant
' ������ �� ��������� ������ ��������� �������. ���������� ������ ��������� ������.
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    For i = 1 To �������
        a(i) = 0
      Next i
    ������� = �������
    ��������������1 = a
End Function

Public Function �������������1(������� As Integer, ���� As Date) As Variant
' ������ �� ������� �� �������� ������ �� ��������� ����. ���������� ������ �� 24 ���������.
Dim a(1 To 24)
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    For i = 1 To 24
        If ������� = 1003 Then
            a(i) = ����
          ElseIf ������� = 1002 Then
            a(i) = i
          Else
            a(i) = 0
          End If
      Next i
    �������������1 = a
End Function


