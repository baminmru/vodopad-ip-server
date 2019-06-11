Attribute VB_Name = "C_MC601_������������"
Option Explicit
'               ****************************
'               *  M U L T I C A L  6 0 1  *
'               ****************************
' ����� � ������
'
'Dim MC601_TimeOut As Integer        ' ����-��� ��� ��������

'' �������� � ������� API

Public Function MC601����������������(Cmd() As Byte, ByVal n As Integer) As Integer
' ������� �� ������� ������, �������� ���, ��� ������
  ' ������� ������
    ReDim Rqs(0 To 127)                 ' ������� ������ �������
    nRqs = MC601Cmd2Rqs(Cmd, n, Rqs)    ' ��������� � �� ������
    ReDim Preserve Rqs(0 To nRqs - 1)   ' �������� ������
    MC601����������        ' ���������� ������� �����
    SendDataToPort Rqs      ' �������� ������
  ' �������� � ��� ������
    ���������������         ' �������� ������� ����-����
    Do                      ' ��� ���������� �����
        DoEvents
        DoPoll
        If MC601�����������() Then Exit Do
        ���������������
        If ����������� Then Exit Do
        Sleep 20
      Loop
  ' ��������� ���������
    ����������������        ' ��������� �������� �� ����-�����
    If ReceiverState = 0 Then
        MC601���������CRC       ' �������� � ������� CRC
        If MC601�����������() <> 0 Then
            ReceiverState = RS_BadData
          End If
      End If
    MC601���������������� = ReceiverState
End Function

Public Sub MC601�����������������(Q As Variant)
' ���������� ��������� ������ ���������� ������ � ������� �����
' ���������� �� ���������������� ���������
Dim i As Integer
    For i = 0 To LenB(Q) - 1
        MC601�������������� Q(i)
      Next i
End Sub

