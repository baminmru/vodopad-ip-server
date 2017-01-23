VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "API_Comm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
' Вся работа с портом инкапсулирована в объекте Comm.
' Здесь скрыты все структуры управления: DCB, COMMTIMEOUTS и OVERLAPPED
'
Private InPolling As Boolean

Private Const InpQueSize = 512  ' Размер входной очереди
Private Const OutQueSize = 128  ' Размер выходной очереди
Private Const InpBufSize = 512  ' Размер входного буфера
Private Const OutBufSize = 128  ' Размер выходного буфера

Private Const EventMask = &H1FF ' Маска отслеживаемых событий

Private nPort As Long           ' Номер порта
Private hPort As Long           ' Хендл порта

Private PortDCB As DCB          ' DCB порта
Private PortTOs As COMMTIMEOUTS ' Тайм-ауты

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
' * ИНИЦИАЛИЗАЦИЯ И ТЕРМИНАЦИЯ *
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
' * ТАЙМАУТЫ *
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
' * ОТКРЫТИЕ И ЗАКРЫТИЕ *
' ***********************
Public Function OpenComm(CommName As String) As Long
' Открывает (переоткрывает) порт. Возвращает номер ошибки
        If hPort <> 0 Then CloseComm
    hPort = CreateFile(CommName, GENERIC_READ Or GENERIC_WRITE, 0, 0, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, 0)
    If hPort = INVALID_HANDLE_VALUE Then
        OpenComm = 1
        Exit Function
      End If
      
    ' Назначаем очереди
    Call SetupComm(hPort, 2048, 256)
    
    ' Инициализируем тайм-ауты
    GetTimeouts
        PortTOs.ReadIntervalTimeout = 1
        PortTOs.ReadTotalTimeoutMultiplier = 0
        PortTOs.ReadTotalTimeoutConstant = 10
        PortTOs.WriteTotalTimeoutMultiplier = 2
        PortTOs.WriteTotalTimeoutConstant = 10
    SetTimeouts
    
    ' Подготавливаем DCB
    GetState
    
    ' Ставим маску
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
' * РАБОТА С DCB *
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
' * ПЕРЕДАЧА *
' ************
' Передача инициируется вызовом извне. Результат операции будет
' известен позднее.
Public Function Output(Q As Variant) As Long
' Ставит запрос на передачу. Возвращает код ошибки
Dim i As Long, k As Long
    Output = 1
        If hPort = 0 Then Exit Function ' Порт не открыт
    Output = 2
        If InWaitTr Then Exit Function  ' Предыдущая передача ещё не закончена
    Output = 3
        If IsNull(Q) Then Exit Function ' Передавать нечего
    k = LenB(Q)
        If k = 0 Then Exit Function     ' Передавать нечего
    Output = 4
        If k > OutBufSize Then Exit Function    ' Слишком много данных

    DataToWrite = k
    For i = 0 To k - 1
        OutBuffer(i) = Q(i)
      Next i
    
    ' Запускаем операцию
    Output = 0
    i = WriteFile(hPort, OutBuffer(0), DataToWrite, DataWritten, OverTr)
    If i <> 0 Then
        TrDone                      ' Передача завершена
      ElseIf GetLastError() = ERROR_IO_PENDING Then
        InWaitTr = True             ' Запрос поставлен. Будем ждать завершения.
      Else
        Output = 5                  ' Ошибка вызова WriteFile
      End If
End Function

Private Sub TrWait()
Dim i As Long
        If Not InWaitTr Then Exit Sub
    i = WaitForSingleObject(OverTr.hEvent, 0)
    If i = 0 Then
        TrDone
      ElseIf i <> WAIT_TIMEOUT Then
        ' Ошибка WaitForSingleObject
      End If
End Sub

Private Sub TrDone()
    InWaitTr = False
    GetOverlappedResult hPort, OverTr, DataWritten, 0
    If DataWritten = DataToWrite Then
        ' Данные успешно переданы
      Else
        ' Ошибка: данные переданы не полностью
      End If
End Sub


' *********
' * ПРИЁМ *
' *********
' Приём включается сразу. Однако реальное поступление данных происходит
' асинхронно и результат сообщается через обратный вызов Client.CommInput
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
            ' Ошибка ReadFile
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
        ' Ошибка WaitForSingleObject
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
' * СЛЕЖЕНИЕ ЗА СОБЫТИЯМИ *
' *************************
' Перехват событий активен постоянно. Внешней программе о событии
' сообщается через обратный вызов.
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
        ' Ошибка WaitCommEvent
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
        ' Ошибка WaitForSingleObject
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


