Attribute VB_Name = "C1_TimeOut"
Option Explicit

'   *************************************
'   *  � � � � � �  �  � � � � � � � �  *
'   *************************************
' ��������� ���� -- ��� ����������� ����-����.
' � Excel'� ���� ������ 1 ��� � �������!

Dim TOLong As Integer       ' ������������ ����-����
Dim TOTime As Date          ' ������ ��������� ����-����
Dim TOWait As Boolean       ' ����-��� �������

Public Sub ����������������(ByVal Sec As Integer)
    TOLong = Sec
End Sub


Public Sub ���������������()
' ��������� ������ �� ��������� ����� ������.
    TOTime = Now() + TimeSerial(0, 0, TOLong)
    TOWait = True
End Sub

Public Sub ����������������()
    TOWait = False
End Sub

Public Sub ���������������()
' ���������� ���� ����-���� (��������� ����������� ���������)
    If TOWait Then
        If Now() > TOTime Then
            ReceiverState = RS_TimeOut
          End If
      End If
End Sub

'Public Sub ��������������������(Sec As Integer)
'    ����������������
'    ��������������� Sec
'End Sub

