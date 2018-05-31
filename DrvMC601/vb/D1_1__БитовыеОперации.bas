Attribute VB_Name = "D1_1__���������������"
Option Explicit
' ������ ������������� ��� �������� ��� ������ � �������� ���������.
' ����� ������� -- ������ ����� � ������ �� 1 ���, ������ ������, ���������� ������,
' ������ ������ � �.�.
'                                                                       07.12.07
'   1. �������� ������� ������� �����/������ ��� Byte, Integer � Long
'   2. ���������� /������� ��������/�������� ����� ��� Integer
'   3. ������ ������ �����/������ � Integer
'
' ************************
' ***** ������ ����� *****
' ************************
Function ShLByte(ByRef b As Byte) As Boolean
' �������� ���� � ����� �� 1 �����. ���������� True, ���� ��� �������
    ShLByte = (b And &H80) <> 0
    b = b And &H7F
    b = b * 2
End Function

Function ShRByte(ByRef b As Byte) As Boolean
' �������� ���� � ����� �� 1 ������. ���������� True, ���� ��� �������
    ShRByte = (b And 1) <> 0
    b = b \ 2
End Function

Function ShLInt(ByRef i As Integer) As Boolean
' �������� ���� � ����� �� 1 �����. ���������� True, ���� ��� �������
Dim k As Integer
    ShLInt = (i And &H8000) <> 0
    k = i And &H4000        ' ���������� 14-� ��� � ���������� k
    i = i And &H3FFF        ' ������� ��� ���� 14 � 15
    i = i * 2               ' ��������� ����� �����
    If k <> 0 Then i = i Or &H8000  ' ���������� ������ 14-� ��� �� ����� 15-��
End Function

Function ShRInt(ByRef i As Integer) As Boolean
' �������� ���� � ����� �� 1 �����. ���������� True, ���� ��� �������
    ShRInt = (i And 1) <> 0
    i = i And &HFFFE        ' ������� 0-� ���
    i = i \ 2               ' ����� ������
    i = i And &H7FFF        ' ������� 15-� ���
End Function

Function ShLLong(ByRef l As Long) As Boolean
' �������� ���� � ������� ����� �� 1 �����. ���������� True, ���� ��� �������
Dim k As Long
    ShLLong = (l And &H80000000) <> 0
    k = l And &H40000000    ' ���������� 30-� ��� � ���������� k
    l = l And &H3FFF0000    ' ������� ��� ���� 14 � 15
    l = l * 2               ' ��������� ����� �����
    If k <> 0 Then l = l Or &H80000000  ' ���������� ������ 30-� ��� �� ����� 31-��
End Function

Function ShRLong(ByRef l As Long) As Boolean
' �������� ���� � ������� ����� �� 1 �����. ���������� True, ���� ��� �������
    ShRLong = (l And 1) <> 0
    l = l And &HFFFFFFFE    ' ������� 0-� ���
    l = l \ 2               ' ����� ������
    l = l And &H7FFFFFFF    ' ������� 15-� ���
End Function


' ********************************
' ***** ����� ������ � ����� *****
' ********************************
Function I2B_Lo(ByVal i As Integer) As Byte
' XXYY -> YY. ���������� ������ (�������) ���� ���������� ������
    I2B_Lo = i And &HFF
End Function

Function I2B_Hi(ByVal i As Integer) As Byte
' XXYY -> XX. ���������� ����� (�������) ���� ���������� ������
    i = i And &HFF00
    i = i \ 256
    I2B_Hi = i And &HFF
End Function

Function B2I_Lo(ByVal b As Byte) As Integer
' XX -> 00XX. ���������� ��������� ����� � �������� ����� ������ � ������� ������
    B2I_Lo = b
End Function

Function B2I_Hi(ByVal b As Byte) As Integer
' XX -> XX00. ���������� ��������� ����� � �������� ������ ������ � ������� �����
Dim i As Integer
    i = b And &H7F
    i = i * 256
    If (b And &H80) <> 0 Then i = i Or &H8000
    B2I_Hi = i
End Function

' ***** ����� ������� � ����� *****
Function L2I_Lo(ByVal l As Long) As Integer
' XXXXYYYY -> YYYY. ���������� ������� ����� �������� ������
Dim i As Integer
    i = l And (Not &HFFFF8000)      ' ������� -- ��� 31-�� ����
    If (l And (Not &HFFFF7FFF)) <> 0 Then i = i Or &H8000
    L2I_Lo = i
End Function

Function L2I_Hi(ByVal l As Long) As Integer
' XXXXYYYY -> XXXX. ���������� ������� ����� �������� ������
    l = l And &HFFFF0000
    l = l \ &H10000
    L2I_Hi = L2I_Lo(l)
End Function

Function I2L_Lo(ByVal i As Integer) As Long
' XXXX -> 0000XXXX. ���������� ������� ����� � �������� ������� ������
    I2L_Lo = (i And &HFFFF) And (Not &HFFFF0000)
End Function

Function I2L_Hi(ByVal i As Integer) As Long
' XXXX -> XXXX0000. ���������� ������� ����� � �������� ������� ������
Dim l As Long
    l = i And &H7FFF
    l = i And &H7FFF
    l = l * &H10000
    If (i And &H8000) <> 0 Then l = l Or &H80000000
    I2L_Hi = l
End Function


' ***************************
' ***** �������� ������ *****
' ***************************
Function ChLInt(ByRef i As Integer) As Byte
' XXYY -> YY00. ��������� ����� ���������� ������ �� ���� �����.
' XX. ���������� ����� (�������) ����. ������� ��������� ������.
Dim j As Integer
    ChLInt = I2B_Hi(i)
    j = (i And &H80) <> 0
    i = i And &H7F
    i = i * 256
    If j Then i = i Or &H8000
End Function

Function ChRInt(ByRef i As Integer) As Byte
' XXYY -> 00XX. ��������� ����� ���������� ������ �� ���� ������.
' YY. ���������� ������ (�������) ����. ������� ��������� ������.
    ChRInt = I2B_Lo(i)
    i = i And &HFF00
    i = i \ 256
    i = i And &HFF
End Function

Function ChLLng(ByRef l As Long) As Byte
' XXYYYYYY -> YYYYYY00. ��������� ����� �������� ������ �� ���� �����.
' XX. ���������� ����� (�������) ����. ������� ��������� ������.
Dim i As Integer
    ChLLng = I2B_Hi(L2I_Hi(l))
    i = (l And &H800000) <> 0
    l = l And &H7FFFFF
    l = l * 256
    If i Then l = l Or &H80000000
End Function

Function ChRLng(ByRef l As Long) As Byte
' XXXXXXYY -> 00XXXXXX. ��������� ����� �������� ������ �� ���� ������.
' YY. ���������� ������ (�������) ����. ������� ��������� ������.
Dim b As Byte
    ChRLng = l And &HFF
    l = l And &HFFFFFF00
    l = l \ 256
    l = l And &HFFFFFF
End Function
