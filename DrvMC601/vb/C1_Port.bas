Attribute VB_Name = "C1_Port"
Option Explicit
' ������ ��������� ����� "����"
' ���������� ��������� � ����������.
' ����������� ������� ����� �� ������-�����, ������� �������� ���������.
'
Dim MyPort As Object            ' ������ �� �����-�������� MSComm32
Global MasterLoaded As Boolean  ' ����� ������� ���������

Global WrkPortNumber As Integer ' ����� ��������� �����. ����� ��� ���������� ��������
Global OnPort As Boolean        ' ���� ������
Global OnLine As Boolean        ' ����� ����� ����� �����������

Global ChannalMode As Integer   ' ���� ������ (���������� ����������)
    Global Const CM_Null = 0
    Global Const CM_Modem = 1   ' ������ � �������
    Global Const CM_Cable = 2   ' ������ �� ��������� �� ������
    Global Const CM_Line = 3    ' ������ �� ��������� ����� ����� (2 + 1)

' ��� ������ ����������� �� ������ ������������ ����� �� ���������
' ����� ������ ��� �����.
Global ChannalErr As Integer    ' ������ �����������.
    Global Const CE_OK = 0
    Global Const CE_Com = 1     ' ������ ��������� � ������ Comm32
    Global Const CE_CPr = 2     ' ������ ���������� ���������� �����
    Global Const CE_Opn = 3     ' �� ������� ������� ����
    Global Const CE_Cal = 4     ' ������� �� ��������
    Global Const CE_NoC = 5     ' �� ������� ���������� �������
    Global Const CE_Mdm = 6     ' ����� �� ��������
    Global Const CE_NoM = 7     ' �� ������� ���������� �����
    Global Const CE_MPr = 8     ' �� ������� ���������������� �����
    Global Const CE_Con = 9     ' �� ������� ���������� ����������

' ������� ���������� ���������� ����� ����� ������
' ����� �������� ������ ����� ��� ��������� ����������� ��������
Global ReceiverState As Integer ' ������� ����������� �����
    Global Const RS_OK = 0      ' �� �� (������ ���, ��������� �������)
    Global Const RS_TimeOut = 1 ' ��� ������ (�������)
    Global Const RS_LostCon = 2 ' ������ ����������
    Global Const RS_LineErr = 3 ' ������ �� ����� �����
    Global Const RS_BadData = 4 ' ������������ �����
    Global Const RS_UserEsc = 5 ' �������� �������������

' ��� -- ������ �����. � ��������, �� �� ��������� ������ ��-��
' �����������. �� ����� ���� ��� ��� �� �����-�� �������.
Global PortErr As Integer       ' ������ �����
    Global Const PE_None = 0    ' ������ ���
    Global Const PE_Frame = 1   ' ��������� ��������� ������
    Global Const PE_Parit = 2   ' ������ ��������
    Global Const PE_Break = 3   ' ������ "Break". �� ������� �� ����� ������� ������
    Global Const PE_LostD = 4   ' ���� ������� ���������� ������
    Global Const PE_OverI = 5   ' ������������ �������� ������
    Global Const PE_OverO = 6   ' ������������ ��������� ������
    Global Const PE_Port = 7    ' ����� �� ������ ��������� � �����
    Global Const PE_DCB = 8     ' �����-�� ��������� ������ ��������� � �����
    Global Const PE_Line = 10   ' ����� ������ �����

' Global InWait As Boolean        ' ���� �������� �������


Global Rqs() As Byte        ' ������ ��� �������� ����� ����
Global nRqs As Integer      ' ����� ������ �������

Dim InPolling As Boolean
' *** ����� �����
Public Sub StartPoll()
    InPolling = True
    Do While InPolling
        DoEvents
        DoPoll
        Sleep 20
'        If Not (MyPort Is Nothing) Then MyPort.Poll
      Loop
End Sub

Public Sub DoPoll()
    If Not (MyPort Is Nothing) Then MyPort.Poll
End Sub

Public Sub StopPoll()
    InPolling = False
End Sub


Public Function �����������() As Integer
    ����������� = ReceiverState
End Function

Public Function ����������������(ByVal ErrNum As Integer) As String
' ������ "����������" ������
    Select Case ErrNum
'      Case CE_OK
'        ���������������� = "���� ������"
      Case CE_Com
        ���������������� = "������ ��������� � ������ Comm32"
      Case CE_CPr
        ���������������� = "������ ���������� ���������� �����"
      Case CE_Opn
        ���������������� = "�� ������� ������� ����"
      Case CE_Cal
        ���������������� = "������� �� ��������"
      Case CE_NoC
        ���������������� = "�� ������� ���������� �������"
      Case CE_Mdm
        ���������������� = "����� �� ��������"
      Case CE_NoM
        ���������������� = "�� ������� ���������� �����"
      Case CE_MPr
        ���������������� = "�� ������� ���������������� �����"
      Case CE_Con
        ���������������� = "�� ������� ���������� ����������"
      End Select
End Function

Public Sub ���������������()
' ��������� ��������� ������ ���������
'
End Sub

'   **************************************
'   ***** ������ � ���������� ������ *****
'   **************************************
' *** ��������� � ����������� �����-�������� ***
Public Function StartPortMaster() As Boolean
' ���������� ������ � ������
    MasterLoaded = True
    StartPortMaster = True
End Function

Public Sub StopPortMaster()
' ��������� ������ � ������
    MasterLoaded = False
End Sub

' *** �������� � �������� ����� ***
Public Function OpenPort(ByVal Number As Integer, ByVal Params As String) As Integer
Dim iRes As Long
    OpenPort = 0
        If Not MasterLoaded Then Exit Function
    Set MyPort = New API_Comm
    iRes = MyPort.OpenComm("COM" & CStr(Number))
    If iRes <> 0 Then
        OpenPort = iRes
        Exit Function
      End If
    If MyPort.BuildDCB(Params) = 0 Then
        OpenPort = CE_CPr
        MyPort.CloseComm
        Exit Function
      End If
    ' ���� ����, ������������� ������ ���������
    MyPort.SetState
End Function

Public Sub ClosePort()
        If Not MasterLoaded Then Exit Sub
    MyPort.CloseComm
    Set MyPort = Nothing
'    MasterLoaded = False
End Sub

'   ***************************
'   ***** ������ � ������ *****
'   ***************************

' *** ������� ��������������� ���������
Public Sub SendDataToPort(Q As Variant)
' �������� ������ �� ����
        If Not MasterLoaded Then Exit Sub
    ReceiverState = 0
    PortErr = 0
    MyPort.Output Q
End Sub

Public Sub TakePortData(Q As Variant)
' ��������� ���������� ������ � ���������� �� ����� �� ���������
    Select Case ChannalMode
      Case CM_Null
'        ��������
      Case CM_Modem
        Mdm_����������������� Q
      Case CM_Cable, CM_Line
        MC601����������������� Q
      End Select
End Sub

Public Sub TakePortEvent(ByVal i As Long)
' ����� �� ���������� y������������ ���������
' ��������� ������� �����. ��, � ��������, ��� �������� � ��� ������
' ����� ����� ����������� ���������.
    If i <> 0 Then PortErr = PE_Line
    If PortErr <> PE_None Then
        ReceiverState = RS_LineErr
      End If
End Sub
