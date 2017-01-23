Attribute VB_Name = "D1_3__CRC"
Option Explicit
' ������ ������������� ��������� ���������� ������������
' ������������ ���� �� ��������� CCITT CRC-16

Function ShortResidue(ByVal P As Byte, ByVal Z As Integer) As Integer
' ������� ��������� ������� �� ������� �������� p00 �� �������� ���� 1zz.
' P -- ������� ���� ��������, Z -- ����� ��������
' ���������� ������� �� ������� (2 �������� �����).
Dim i As Integer
Dim r As Integer       ' ���������� �������
    r = 0
    For i = 1 To 8
        If ShLInt(r) <> ShLByte(P) Then r = r Xor Z
      Next i
    ShortResidue = r
End Function

Function CRC_CCITT16(a() As Byte, n As Integer, iPos As Integer) As Integer
' ������� ��������� CCITT CRC-16 ��� ������� ������ A().
' iPos -- ��������� ������� � �������.
' ���������� ��� CRC (2 �������� �����).
Const InitR = 0         ' ��������� �������
Const MaskZ = &H1021    ' ����� ��������

Dim i As Integer
Dim P As Byte           ' �������� ����
Dim r As Integer        ' �������
    r = InitR
    If n > 0 Then
        For i = 0 To n - 1
            P = ChLInt(r) Xor a(iPos + i)
            r = r Xor ShortResidue(P, MaskZ)
          Next i
      End If
    CRC_CCITT16 = r
End Function

