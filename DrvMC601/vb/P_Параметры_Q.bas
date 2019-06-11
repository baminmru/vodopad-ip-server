Attribute VB_Name = "P_���������_Q"
Option Explicit

Const �������� = "������    �������   ����      ������    ���       ����      ����      ������    ��������  �������   ������    �������"
Const �������� = "������    �������   �����     ������    ���       ����      ����      �������   ��������  �������   ������    �������"
'                 12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
'   *******************
'   *** ��������� Q ***
'   *******************
Public Function ��������������_Q(ByVal ����� As Long) As Variant
Dim Code As Integer
    ��������������_Q = ""
' �� 1 �� 19
    If ����� < 20 Then
    Select Case �����
      Case 1   ' ������ ��������
        ��������������_Q = �������������
      Case 2   ' ����� ������
        ��������������_Q = ������������
      Case 3    ' ��� ������
        ��������������_Q = ���������
      Case 4   ' ��� �������
        ��������������_Q = ����������
      Case 5   ' �������� ������, YYMM
        ��������������_Q = �������
      Case 6   ' ���� ������, YYMMDD
        ��������������_Q = ���������
      Case 7   ' ����� ������, HHMMSS
        ��������������_Q = ����������
      Case 8   ' ������ �������, YYMMDD
        ��������������_Q = �������
      Case 9   ' ������� �������, , YYMMDD
        ��������������_Q = �������
            
      Case 10   ' �������� ����
        ��������������_Q = �����������
      Case 11   ' �������� ������
        ��������������_Q = �������������
      Case 12   ' ��� ����� ������
        ��������������_Q = ���������
      Case 16   ' ��� ����� ������
        ��������������_Q = YYMMDD2Date(���������) + HHMMSS2Time(����������)
      End Select
    Exit Function
    End If
      
    If ����� < 100 Then
    Code = ����� Mod 10
    ����� = ����� \ 10
    Select Case �����
      Case 5   ' ������ (������ ���-YY)
        If Code = 0 Then
            ��������������_Q = �������������(�������)
          Else
            ��������������_Q = ������������������(������� * 100, Code)
          End If
      Case 6  ' ���� ������ (������ Date)
        If Code = 0 Then
            ��������������_Q = YYMMDD2Date(���������)
          Else
            ��������������_Q = ������������������(���������, Code)
          End If
      Case 7   ' ����� ������ (������ Date)
        If Code = 0 Then
            ��������������_Q = HHMMSS2Time(����������)
          End If
      Case 8   ' ������ �������
        If Code = 0 Then
            ��������������_Q = ��������������(�������)
          Else
            ��������������_Q = ������������������(�������, Code)
          End If
      Case 9   ' ������� �������
        If Code = 0 Then
            ��������������_Q = ��������������(�������)
          Else
            ��������������_Q = ������������������(�������, Code)
          End If
      End Select
    End If
End Function

Private Function ������������������(ByVal YYMMDD As Variant, Code As Integer)
Dim YY As Integer, MM As Integer, DD As Integer
    ������������������ = ""
        If NoData(YYMMDD) Then Exit Function
        If Not IsNumeric(YYMMDD) Then Exit Function
    DD = YYMMDD Mod 100
    YY = YYMMDD \ 100
    MM = YY Mod 100
    YY = YY \ 100
    Select Case Code
      Case 1    ' ���� "DD"
        ������������������ = �������������(DD)
      Case 4    ' ����� "MM"
        ������������������ = �������������(MM)
      Case 5    ' ����� "MMM"
        ������������������ = ��������(MM)
      Case 6    ' ����� "MMMM", ��. �����
        ������������������ = ���������_��(MM)
      Case 7    ' ����� "MMMM", ���. �����
        ������������������ = ���������_��(MM)
      Case 8    ' ��� "��"
        ������������������ = �������������(YY)
      Case 9    ' ��� "����"
        ������������������ = �������(YY)
      End Select
End Function

' ***** ��������������� ������� *****
Private Function �������������(ByVal n As Integer) As String
' ����� ������ � ���� "07"
    ������������� = Format$(n \ 100, "00")
End Function

Private Function ��������(ByVal n As Integer) As String
' �������� 3-��������� �������� ������: "���", "���", ....
    �������� = MM2Str(n)
End Function

Private Function ���������_��(ByVal n As Integer) As String
' ������ �������� ������ � ������������ ������
    ���������_�� = ""
        If n <= 0 Then Exit Function
        If n > 12 Then Exit Function
    ���������_�� = Trim(Mid(��������, (n - 1) * 10 + 1, 10))
End Function

Private Function ���������_��(ByVal n As Integer) As String
' ������ �������� ������ � ����������� ������
    ���������_�� = ""
        If n <= 0 Then Exit Function
        If n > 12 Then Exit Function
    ���������_�� = Trim(Mid(��������, (n - 1) * 10 + 1, 10))
End Function

Private Function �������(ByVal YY As Integer) As String
' ��������� 2-�������� ������ ���� �� ������� ����
    ������� = CStr((Year(Date) \ 100) * 100 + (YY Mod 100))
End Function


