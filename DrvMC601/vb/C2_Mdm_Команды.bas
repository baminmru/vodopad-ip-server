Attribute VB_Name = "C2_Mdm_�������"
Option Explicit

Const InitPar = "1200,N,8,1"    ' ������ ���������� ������ �����

' ************************
' *** ������ � ������� ***
' ************************
Public Function Mdm_�����������(ByVal ����� As Integer) As Integer
    ChannalMode = CM_Modem
    Mdm_����������� = OpenPort(�����, InitPar)
End Function

Public Function Mdm_���������() As Boolean
' � ������ ������� ���������� 0
    Mdm_���������������� "AT"
    Mdm_��������� = (MdmKWord = AC_OK)
End Function

Public Function Mdm_����������(����� As Integer) As Integer
' ���� �����. �������� ����� ���������������� ����� �����
' ���������� ����� �����, ������� �������, 0 -- � ������ �������
Dim i As Integer, k As Integer
    Mdm_���������� = 0
  ' ������� ������� ���������� ����� �����
    If ����� <> 0 Then
        i = �����
        If Mdm_�����������(i) = 0 Then
            If Mdm_��������� Then
                Mdm_���������� = i
                Exit Function
              End If
            ClosePort
          End If
      End If
    ' ����� �� �������, ���� ���������
    For i = 1 To 8
    If i <> ����� Then
        If Mdm_�����������(i) = 0 Then
            If Mdm_��������� Then
                Mdm_���������� = i
                Exit Function
              End If
            ClosePort
          End If
      End If
      Next i
End Function

Public Function Mdm_��������������������(������� As String) As Integer
' ����� ������� �� ��������� ����������.
' ���������� 0, ���� ���������� �����������, ��� ��� ����������.
    Mdm_�������������������� = ������������(�������)
End Function

Public Sub Mdm_�������������������()
' ��������� ����������
'
    ClosePort
    Mdm_����������� WrkPortNumber
End Sub


