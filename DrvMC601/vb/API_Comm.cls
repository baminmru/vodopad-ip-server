VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "API_Comm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
' ��� ������ � ������ ��������������� � ������� Comm.
' ����� ������ ��� ��������� ����������: DCB, COMMTIMEOUTS � OVERLAPPED
'
Private InPolling As Boolean

Private Const InpQueSize = 512  ' ������ ������� �������
Private Const OutQueSize = 128  ' ������ �������� �������
Private Const InpBufSize = 512  ' ������ �������� ������
Private Const OutBufSize = 128  ' ������ ��������� ������

Private Const EventMask = &H1FF ' ����� ������������� �������

Private nPort As Long           ' ����� �����
Private hPort As Long           ' ����� �����

Private PortDCB As DCB          ' DCB �����
Private PortTOs As COMMTIMEOUTS ' ����-����

Private OverTr As OVERLAPPED, InWaitTr As Boolean
Private OverRs As OVERLAPPED, InWaitRs As Boolean
Private OverEv As OVERLAPPED, InWaitEv As Boolean

Private InpBuffer(0 To InpBufSize - 1) As Byte
Private OutBuffer(0 To OutBufSize - 1) As Byte
Private DataToWrite As Long
Private DataWritten As Long
Private DataRead As Long
Private EventResults As Long


' ******************************
' * ������������� � ���������� *
' ******************************
Private Sub Class_Initialize()
Dim i As Integer
    PortDCB.DCBlength = Len(PortDCB)
        OverTr.hEvent = CreateEvent(0, True, False, vbNullString)
        OverRs.hEvent = CreateEvent(0, True, False, vbNullString)
        OverEv.hEvent = CreateEvent(0, True, False, vbNullString)
End Sub

Private Sub Class_Terminate()
Dim i As Integer
    Call CloseComm
    CloseHandle (OverTr.hEvent)
    CloseHandle (OverRs.hEvent)
    CloseHandle (OverEv.hEvent)
End Sub

' ************
' * �������� *
' ************
Public Sub GetTimeouts()
        If hPort = 0 Then Exit Sub
    Call GetCommTimeouts(hPort, PortTOs)
End Sub

Public Function SetTimeouts() As Long
        If hPort = 0 Then Exit Function
    SetTimeouts = SetCommTimeouts(hPort, PortTOs) <> 0
End Function


' ***********************
' * �������� � �������� *
' ***********************
Public Function OpenComm(CommName As String) As Long
' ��������� (�������������) ����. ���������� ����� ������
        If hPort <> 0 Then CloseComm
    hPort = CreateFile(CommName, GENERIC_READ Or GENERIC_WRITE, 0, 0, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0)
    If hPort = INVALID_HANDLE_VALUE Then
        OpenComm = 1
        Exit Function
      End If
      
    ' ��������� �������
    Call SetupComm(hPort, 2048, 256)
    
    ' �������������� ����-����
    GetTimeouts
        PortTOs.ReadIntervalTimeout = 1
        PortTOs.ReadTotalTimeoutMultiplier = 0
        PortTOs.ReadTotalTimeoutConstant = 10
        PortTOs.WriteTotalTimeoutMultiplier = 2
        PortTOs.WriteTotalTimeoutConstant = 10
    SetTimeouts
    
    ' �������������� DCB
    GetState
    
    ' ������ �����
    SetCommMask hPort, EventMask
'    StartInput
End Function

' Close the comm port
Public Function CloseComm() As Long
    ' Already closed, just exit
        If hPort = 0 Then Exit Function
    CloseHandle hPort
    hPort = 0
End Function


' ****************
' * ������ � DCB *
' ****************
Public Function BuildDCB(Mode As String) As Long
    BuildDCB = BuildCommDCB(Mode, PortDCB)
End Function

Public Function GetState() As Long
        If hPort = 0 Then Exit Function
    GetState = GetCommState(hPort, PortDCB)
End Function

Public Function SetState() As Long
        If hPort = 0 Then Exit Function
    SetState = SetCommState(hPort, PortDCB)
End Function

Public Function GetFlags() As Long
    GetFlags = PortDCB.Flags
End Function


' ************
' * �������� *
' ************
' �������� ������������ ������� �����. ��������� �������� �����
' �������� �������.
Public Function Output(Q As Variant) As Long
' ������ ������ �� ��������. ���������� ��� ������
Dim i As Long, k As Long
    Output = 1
        If hPort = 0 Then Exit Function ' ���� �� ������
    Output = 2
        If InWaitTr Then Exit Function  ' ���������� �������� ��� �� ���������
    Output = 3
        If IsNull(Q) Then Exit Function ' ���������� ������
    k = LenB(Q)
        If k = 0 Then Exit Function     ' ���������� ������
    Output = 4
        If k > OutBufSize Then Exit Function    ' ������� ����� ������

    DataToWrite = k
    For i = 0 To k - 1
        OutBuffer(i) = Q(i)
      Next i
    
    ' ��������� ��������
    Output = 0
    i = WriteFile(hPort, OutBuffer(0), DataToWrite, DataWritten, OverTr)
    If i <> 0 Then
        TrDone                      ' �������� ���������
      ElseIf GetLastError() = ERROR_IO_PENDING Then
        InWaitTr = True             ' ������ ���������. ����� ����� ����������.
      Else
        Output = 5                  ' ������ ������ WriteFile
      End If
End Function

Private Sub TrWait()
Dim i As Long
        If Not InWaitTr Then Exit Sub
    i = WaitForSingleObject(OverTr.hEvent, 0)
    If i = 0 Then
        TrDone
      ElseIf i <> WAIT_TIMEOUT Then
        ' ������ WaitForSingleObject
      End If
End Sub

Private Sub TrDone()
    InWaitTr = False
    GetOverlappedResult hPort, OverTr, DataWritten, 0
    If DataWritten = DataToWrite Then
        ' ������ ������� ��������
      Else
        ' ������: ������ �������� �� ���������
      End If
End Sub


' *********
' * ��Ȩ� *
' *********
' ���� ���������� �����. ������ �������� ����������� ������ ����������
' ���������� � ��������� ���������� ����� �������� ����� Client.CommInput
Private Sub StartInput()
Dim i As Long
        If InWaitRs Then Exit Sub
        If hPort = 0 Then Exit Sub
    i = ReadFile(hPort, InpBuffer(0), InpBufSize, DataRead, OverRs)
    If i <> 0 Then
        RsDone
      ElseIf GetLastError() = ERROR_IO_PENDING Then
        InWaitRs = True
      Else
            ' ������ ReadFile
      End If
End Sub

Public Sub RsWait()
Dim i As Long
    If Not InWaitRs Then
        StartInput
        Exit Sub
    End If
        
    i = WaitForSingleObject(OverRs.hEvent, 0)
    If i = 0 Then
        RsDone
      ElseIf i <> WAIT_TIMEOUT Then
        ' ������ WaitForSingleObject
      End If
End Sub

Public Sub RsDone()
Dim b() As Byte
Dim Q As Variant
Dim i As Long, k As Long
    If InWaitRs Then
        DataRead = OverRs.InternalHigh
        InWaitRs = False
    End If
    If DataRead <> 0 Then
        ReDim b(0 To 127)
        For i = 0 To DataRead - 1
            b(i) = InpBuffer(i)
          Next i
        ReDim Preserve b(0 To DataRead - 1)
        Q = b
        TakePortData b
      End If
End Sub


' *************************
' * �������� �� ��������� *
' *************************
' �������� ������� ������� ���������. ������� ��������� � �������
' ���������� ����� �������� �����.
Private Sub StartWatch()
Dim i As Long
        If InWaitEv Then Exit Sub
        If hPort = 0 Then Exit Sub
    EventResults = 0
    i = WaitCommEvent(hPort, EventResults, OverEv)
    If i <> 0 Then
        EvDone
      ElseIf GetLastError() = ERROR_IO_PENDING Then
        InWaitEv = True
      Else
        ' ������ WaitCommEvent
      End If
End Sub

Private Sub EvWait()
Dim i As Long
    If Not InWaitEv Then
        StartWatch
        Exit Sub
    End If
        
    i = WaitForSingleObject(OverEv.hEvent, 0)
    If i = 0 Then
        EvDone
      ElseIf i <> WAIT_TIMEOUT Then
        ' ������ WaitForSingleObject
      End If
End Sub

Private Sub EvDone()
Dim Errors As Long
    InWaitEv = False
        If EventResults = 0 Then Exit Sub
    ClearCommError hPort, Errors, 0
        If Errors = 0 Then Exit Sub
    TakePortEvent Errors
End Sub

Public Sub Poll()
    If hPort = 0 Then Exit Sub
    EvWait
    TrWait
    RsWait
End Sub


