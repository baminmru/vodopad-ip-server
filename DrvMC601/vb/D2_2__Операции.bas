Attribute VB_Name = "D2_2__��������"
Option Explicit

' ������������ ������������ �������� � ������ ������� � ������ ��������

'   *****************
'   ***   � � �   ***
'   *****************
'
Public Function ��������������(ByVal �����, ByVal ����������)
' ���������� "�������� �� ���������" ��� ��� "�" �� ��������� ��� "�".
Dim m As Integer
    �������������� = Null
    If NoData(�����) Or (����� = 0) Then ����� = 101
    m = 0   ' �������������� �����
    Select Case ����������
      Case 1    ' �����
        If ����� Mod 100 < 20 Then m = 100    ' ���� � ������ ������, �� ��������� �����
      Case 2    ' ���
        If ����� \ 100 < 8 Then m = 1200      ' ���� � ������ ����, �� ��������� ���
      Case Else
        Exit Function
      End Select
    �������������� = ����� + m
End Function

'Public Function �����_��(ByVal �����, ByVal �����, ByVal ����������)
'' ���������� ������ ���������� ���� "�" (���� ������ ������)
'    If NoData(�����) Or (����� = 0) Then
'        ������������ = ��������������(�����, ����������)
'      Else
'        ������������ = �����
'      End If
'End Function

Public Function ������������(ByVal ������, ByVal �����)
' ��������� �������� ���� �� ��������� �������. "����" -- ������ �����
    ������������ = Null
    If NoData(������) Or NoData(�����) Then Exit Function
    If ������ = 0 Or ����� = 0 Then Exit Function
    ������������ = DateSerial(100 * (Year(Date) \ 100) + (������ \ 100), _
                        �������������(������) + (����� \ 100), (����� Mod 100))
End Function
    
'Public Function �����������������������(ByVal ������, ByVal �����, ByVal �����, ByVal ����������)
'    ����������������������� = Null
'    If NoData(�����) Or (����� = 0) Then ����� = ��������������(�����, ����������)
'    ����������������������� = ������������(������, �����)
'End Function


'   *****************
'   ***  �������  ***
'   *****************
'
Public Function ���������������(ByVal ������ As Variant) As Variant
' ��������� ��������� ������
Dim m As Integer, Y As Integer
    ��������������� = Null
        If NoData(������) Then Exit Function
    m = ������ Mod 100
    Y = ������ \ 100
    If m = 0 Then   ' ���
        If Y < 320 Then Y = Y + 1
      Else          ' �����
        If m < 12 Then
            m = m + 1
          Else
            m = 1
            If Y < 320 Then Y = Y + 1
          End If
      End If
    ��������������� = 100 * Y + m
End Function

Public Function ����������������(ByVal ������ As Variant) As Variant
' ��������� ���������� ������
Dim m As Integer, Y As Integer
    ���������������� = Null
        If NoData(������) Then Exit Function
    m = ������ Mod 100
    Y = ������ \ 100
    If m = 0 Then   ' ���
        If Y > 0 Then Y = Y - 1
      Else          ' �����
        If m > 1 Then
            m = m - 1
          Else
            If Y > 0 Then
                Y = Y - 1
                m = 12
              End If
          End If
      End If
    ���������������� = 100 * Y + m
End Function

Public Function �������������(ByVal ������ As Integer) As Integer
' ���������� ����� ������ ������� ����� 1.
' ��� ������� -- ��� 0,1,2 � �.�.,
' ��� ���� -- ������ 0.
Dim i As Integer
    i = ������ Mod 100
    If i <> 0 Then i = i - 1
    ������������� = i
End Function

Public Function ����2������(ByVal ���� As Date, ByVal ���������� As Integer) As Variant
' ���������� "�����������" ������, ��������������� ����
    ����2������ = Null
    Select Case ����������
      Case 1    ' �����
        ����2������ = Month(����) + 100 * (Year(����) - 2000)
      Case 2    ' ���
        ����2������ = 100 * (Year(����) - 2000)
      End Select
End Function

Public Function ������������(ByVal ���� As Variant, ByVal ���������� As Variant, ByVal ����� As Variant)
' ���������� �������� ������, ������� �������� ������� ����
' �������� "����" -- ���� �����, ���� �����
' ������������ � ��������.
Dim i As Integer
    ������������ = Null
    If NoData(����) Then Exit Function
    If NoData(����������) Or NoData(�����) Then Exit Function
    i = ����2������(����, ����������)
    If ������������(i, �����) < ���� Then
        i = ���������������(i)
      End If
    ������������ = i
End Function

Public Function �����������(ByVal ������ As Integer, ByVal ����� As Integer) As Integer
' ��������� ������, ��������� �� ��������� �������� (�������� �����)
Dim Y As Integer, m As Integer
    m = ������ Mod 100
    Y = ������ \ 100
    Select Case �����
      Case 1    ' �����
        ����������� = ����������������(������)
        Exit Function
      Case 2    ' ���
        Y = Y - 1
      Case 3    ' 12 ���
        Y = Y - 12
      End Select
    If Y < 0 Then Y = 0
    ����������� = Y * 100 + m
End Function


'   *****************
'   ***  �������  ***
'   *****************
'
Public Function �����������(ByVal ����������, ByVal ���������) As Integer
' ���������� ������, ������������ �������:
' 0 -- �����., 1 -- �����, 2 -- ���, 3 -- 12 ���
    If NoData(���������) Then ��������� = 0
    If NoData(����������) Then ���������� = 0
    If ���������� <> 0 Then
        ����������� = ����������
        Exit Function
      End If
    Select Case ���������
      Case 1, 2
        ����������� = 1
      Case 3
        ����������� = 2
      Case 4
        ����������� = 3
      Case Else
        ����������� = 0
      End Select
End Function

Public Function �����������������(ByVal ���� As Date, ByVal ����� As Integer, ByVal ��������� As Integer) As Long
' ���������� ���� (�����) ������, ���������� � ������ ����� ��������� �����.
' ����� ����� �������� ����, ������ ��� ����, � ����������� �� ���� ������.
Dim d As Date
    Select Case ���������
      Case 1, 2 ' ��������/������� -- (�������� ���� -1)
        d = ����
      Case 3    ' ��������
        d = DateSerial(Year(����), Month(����), ����� Mod 100)
        If ���� < d Then
            d = DateSerial(Year(����), Month(����) - 1, ����� Mod 100)
          End If
      Case 4    ' �������
        d = DateSerial(Year(����), ����� \ 100, ����� Mod 100)
        If ���� < d Then
            d = DateSerial(Year(����) - 1, ����� \ 100, ����� Mod 100)
          End If
      Case Else
        ����������������� = 0
        Exit Function
      End Select
    d = d - 1
'    ����������� = D
    ����������������� = Date2YYMMDD(d, ���������)
End Function

Public Function ���������������(ByVal ����� As Long, ByVal DayX As Integer) As Date
' ��������� ����, ����� ���� ������� ������, �� � ��������� �����
Dim DD As Integer, YM As Integer
    YM = ����� \ 100
    DD = ����� Mod 100
    If DD = 0 Then
    ' ����� � ���. ��� ���� ����� � ���� ���������
        If (YM Mod 100) = 0 Then YM = YM + (DayX \ 100) ' ��� ���� ��������� ������
        DD = (DayX Mod 100) - 1
        If DD = 0 Then YM = YM + 1
      End If
    ��������������� = DateSerial(2000 + (YM \ 100), YM Mod 100, DD)
End Function

Public Function ��������������(ByVal ����� As Long, ByVal ��������� As Integer) As Long
' ���������� ����� ��������� ������
    Select Case ���������
      Case 1, 2
        �������������� = Date2YYMMDD(YYMMDD2Date(�����) + 1, ���������)
      Case 3, 4
        �������������� = CLng(���������������(����� \ 100)) * 100
      End Select
End Function

Public Function ���������������(ByVal ����� As Long, ByVal ��������� As Integer) As Long
' ���������� ����� ��������� ������
    Select Case ���������
      Case 1, 2
        ��������������� = Date2YYMMDD(YYMMDD2Date(�����) - 1, ���������)
      Case 3, 4
        ��������������� = CLng(����������������(����� \ 100)) * 100
      End Select
End Function

Public Function ����������(ByVal ����� As Long, ByVal ����� As Integer) As Long
' ��������� ������, ��������� �� ��������� �������� (�������� �����)
Dim V As Long, d As Integer, m As Integer, Q As Date
    d = ����� Mod 100
    V = CLng(�����������(����� \ 100, �����)) * 100
    If d = 0 Then       ' ���� ��� ����� ��� ��� -- �� �� ��
        ���������� = V
        Exit Function
      End If
    ' � ������� ������ ����. ���� ���� -- 31-� �����,
    ' �� ����� �� ����� ������ ������ ��������� ���� ����������� ������, � �� 1-� ���� ��.
    m = (����� \ 100) Mod 100
    Q = YYMMDD2Date(V + d)
'    If Month(Q) <> M Then Q = DateSerial(Year(Q), Month(Q) + 1, 0)
'    If Month(Q) <> M Then Q = DateSerial(Year(Q), Month(Q) + 1, 1)
    ���������� = Date2YYMMDD(Q, 1)
End Function

Public Function �������������(ByVal ������, ByVal �����) As Variant
' ��������� �������� �����. ������ -- ����� ������ ������, ����� -- ��������� ������.
' ��� ���������� ����� ������� ���� ��������� �� 1
Dim i As Integer
    If NoData(������) Or NoData(�����) Then
        ������������� = Null
        Exit Function
      End If
    If ������ Mod 10000 = 0 Then
        ������������� = DateDiff("yyyy", YYMMDD2Date(������), YYMMDD2Date(�����))
      ElseIf ������ Mod 100 = 0 Then
        ������������� = DateDiff("m", YYMMDD2Date(������), YYMMDD2Date(�����))
      Else
        ������������� = DateDiff("d", YYMMDD2Date(������), YYMMDD2Date(�����))
      End If
End Function


