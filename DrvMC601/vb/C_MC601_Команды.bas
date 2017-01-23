Attribute VB_Name = "C_MC601_�������"
Option Explicit

Const InitPar = "1200,N,8,1"    ' ������ ���������� ������ �����

Global Const MC601_AdrMain = &H3F       ' ����� ��������� ������
Global Const MC601_AdrHour = &H7F       ' ����� ��������������� ������ (��� ������� �������)

Global MC601�������(0 To 15) As Byte    ' ������� �� �������

Global Const MaxRsp = 1023
Global MC601�����(0 To MaxRsp) As Byte  ' ����� ������ ��������
Global MC601������� As Integer          ' ����� ������



' ************************
' *** ������ � ������� ***
' ************************
Public Function MC601�����������(ByVal ����� As Integer) As Integer
    ChannalMode = CM_Cable
    MC601����������� = OpenPort(�����, InitPar)
End Function

Public Function MC601_������������(����� As Integer) As Integer
' ���� ����. �������� ����� ���������������� �����
' ���������� ����� �����, ������� �������, 0 -- � ������ �������
' � ������ ���� ����� ������
Dim i As Integer, k As Integer
    MC601_������������ = 0
  ' ������� ������� ���������� ����� �����
    If ����� <> 0 Then
        i = �����
        If MC601�����������(i) = 0 Then
            If MC601_������������ <> 0 Then
                MC601_������������ = i
                Exit Function
              End If
            ClosePort
          End If
      End If
    ' ����� �� �������, ���� ���������
    For i = 1 To 8
    If i <> ����� Then
        If MC601�����������(i) = 0 Then
            If MC601_������������ <> 0 Then
                MC601_������������ = i
                Exit Function
              End If
            ClosePort
          End If
      End If
      Next i
End Function


' ************************
' *** ������� �������� ***
' ************************
Public Function MC601_����������()
' � ������ ������� ���������� Null
    If Not MasterLoaded Then
        MC601_���������� = "����"
        Exit Function
      End If
    MC601�������(0) = &H3F
    MC601�������(1) = &H1
        If MC601����������������(MC601�������, 2) <> 0 Then Exit Function
    MC601_���������� = Bytes2I(MC601�����, 2, 2)
End Function

Public Function MC601_������������() As Variant
' � ������ ������� ���������� 0. �����, � ��������, ����� ���� ��������-��������
    MC601_������������ = Null
    If Not MasterLoaded Then Exit Function
'    If Not MasterLoaded Then
'        MC601_������������ = "����"
'        Exit Function
'      End If
    MC601�������(0) = &H3F
    MC601�������(1) = &H2
        If MC601����������������(MC601�������, 2) <> 0 Then Exit Function
    MC601_������������ = Bytes2L(MC601�����, 4, 2)
End Function

Public Function MC601_��������������(������� As Integer)
    If Not MasterLoaded Then
        MC601_�������������� = 0
        Exit Function
      End If
    MC601�������(0) = &H3F
    MC601�������(1) = &H10
    MC601�������(2) = &H1
    I2Bytes �������, MC601�������, 3
        If MC601����������������(MC601�������, 5) <> 0 Then Exit Function  ' ������
        If MC601������� <= 7 Then Exit Function                     ' ������ �����
    '   MC601�����(4)                           ' ������� ���������
    MC601_�������������� = Byte2Mantiss(MC601�����, MC601�����(5), 7) _
                        * Byte2Factor(MC601�����(6))
End Function


' *********************
' *** ������ ������ ***
' *********************

Public Function MC601_��������������(������� As Integer, _
                �������� As Integer, ������� As Integer, ByRef ������� As Integer) As Variant
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    For i = 1 To �������
        a(i) = 0
      Next i
    ������� = �������
    MC601_�������������� = a
End Function

Public Function MC601_�������������(������� As Integer, ���� As Date) As Variant
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
            a(i) = 25 - i
          Else
            a(i) = 0
          End If
      Next i
    MC601_������������� = a
End Function


Public Function MC601_��������������(ByVal ������� As Integer, ByVal ���� As Date) As Variant
Dim a(1 To 24) As Double
Dim i As Integer, j As Integer, k As Integer
Dim l As Integer, m As Integer, n As Integer
Dim iErr As Integer, iPos As Integer
Dim f As Double, b As Double
Dim DateMark As Date
Dim iHrs As Integer

    If Not MasterLoaded Or (������� = 1003) Or (������� = 1002) Then
        MC601_�������������� = MC601_�������������(�������, ����)
        Exit Function
      End If
     
    For j = 1 To 24
        a(j) = 0
      Next j
     
    ���� = ���� + 1
    MC601�������(0) = &H7F
    MC601�������(1) = &H63
    I2Bytes �������, MC601�������, 2
    MC601�������(4) = Year(����) - 2000
    MC601�������(5) = Month(����)
    MC601�������(6) = Day(����)
    MC601�������(7) = 0
    iErr = MC601����������������(MC601�������, 8)
        If iErr <> 0 Then Exit Function
    '   MC601�����(4)               ' ������� ���������
    l = MC601�����(5)               ' ������ ��������
    f = Byte2Factor(MC601�����(6))  ' ������
    k = (MC601������� - 8) \ l      ' ���������� �������� ���������� (24)
    If k = 0 Then
        iErr = 99
        MC601_�������������� = a
        Exit Function
      End If
    iPos = 8
    If k > 24 Then k = 24
    For j = 1 To k
        a(j) = Byte2Mantiss(MC601�����, l, iPos) * f
        iPos = iPos + l
      Next j
    MC601_�������������� = a
End Function


Public Function MC601_���������������(������� As Integer, _
                �������� As Integer, ������� As Integer, ByRef n As Integer) As Variant
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    If Not MasterLoaded Then
        MC601_��������������� = MC601_��������������(�������, ��������, �������, n)
        Exit Function
      End If
      
    MC601�������(0) = &H3F
    MC601�������(1) = &H66
    I2Bytes �������, MC601�������, 2
    i = ��������                      ' ����� ����������
    n = 0
    Do While n < �������
        I2Bytes i, MC601�������, 4
        ' ������� ������ � ������� ������
        iErr = MC601����������������(MC601�������, 6)
        If iErr <> 0 Then GoTo 1
        '   �����(4)                    ' ������� ���������
        l = MC601�����(5)               ' ������ ��������
        f = Byte2Factor(MC601�����(6))  ' ������
        k = (MC601������� - 7) \ l      ' ���������� �������� ����������
        If k = 0 Then GoTo 1
        iPos = 7
        For j = 1 To k
            n = n + 1
            a(n) = Byte2Mantiss(MC601�����, l, iPos) * f
            iPos = iPos + l
          Next j
        i = i + k
      Loop
1:
    MC601_��������������� = a
End Function

Public Function MC601_���������������(������� As Integer, _
                �������� As Integer, ������� As Integer, ByRef n As Integer) As Variant
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer

    If Not MasterLoaded Then
        MC601_��������������� = MC601_��������������(�������, ��������, �������, n)
        Exit Function
      End If
      
    MC601�������(0) = &H3F
    MC601�������(1) = &H65
    I2Bytes �������, MC601�������, 2
    i = ��������                      ' ����� ����������
    n = 0
    Do While n < �������
        I2Bytes i, MC601�������, 4
        ' ������� ������ � ������� ������
        iErr = MC601����������������(MC601�������, 6)
        If iErr <> 0 Then GoTo 1
        '   �����(4)                    ' ������� ���������
        l = MC601�����(5)               ' ������ ��������
        f = Byte2Factor(MC601�����(6))  ' ������
        k = (MC601������� - 7) \ l      ' ���������� �������� ����������
        If k = 0 Then GoTo 1
        iPos = 7
        For j = 1 To k
            n = n + 1
            a(n) = Byte2Mantiss(MC601�����, l, iPos) * f
            iPos = iPos + l
          Next j
        i = i + k
      Loop
1:
    MC601_��������������� = a
End Function


