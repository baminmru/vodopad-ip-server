Attribute VB_Name = "C2_Mdm_������������"
Option Explicit

Global Mdm_TimeOut As Integer        ' ����-��� ��� ��������

Public Sub Mdm_��������������(Cmd As String)
' �������� ������� �� �����. � ������!!!
    ReDim Rqs(0 To 127)             ' ������� ������ �������
    nRqs = Mdm_Cmd2Rqs(Cmd, Rqs)    ' ��������� ������
    ReDim Preserve Rqs(0 To nRqs - 1) ' �������� ������
    Mdm_����������                 ' ���������� ������� �����
    SendDataToPort Rqs              ' �������� ������
End Sub

Public Function Mdm_������������(ByVal Delay As Integer) As Integer
' ���������� ���������� �������.
    ���������������� Delay
    ���������������
    Do  ' ��� ���������� �����
        DoEvents
        DoPoll
        If Mdm_�����������() Then Exit Do
        ���������������
        If ����������� Then Exit Do
        Sleep 20
      Loop
  ' ��������� ��������
    ����������������        ' ��������� �������� �� ����-�����
    If Mdm_�����������() <> 0 Then
        ReceiverState = RS_BadData
      End If
    Mdm_������������ = ReceiverState
End Function

Public Function Mdm_����������������(Cmd As String) As Integer
    Mdm_�������������� Cmd
    Mdm_���������������� = Mdm_������������(3)
End Function

Public Sub Mdm_�����������������(Q As Variant)
' ���������� ��������� ������ ���������� ������ � ������� �����
' ���������� �� ���������������� ���������
Dim i As Integer
    For i = 0 To LenB(Q) - 1
        Mdm_�������������� Q(i)
      Next i
End Sub

