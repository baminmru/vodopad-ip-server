Attribute VB_Name = "C1_Port"
Option Explicit
' Модуль дополняет форму "Порт"
' Определяет константы и переменные.
' Транслирует команды порта на мастер-форму, скрывая механизм обращения.
'
Dim MyPort As Object            ' Ссылка на форму-носитель MSComm32
Global MasterLoaded As Boolean  ' Форма успешно загружена

Global WrkPortNumber As Integer ' Номер открытого порта. Нужно для повторного открытия
Global OnPort As Boolean        ' Порт открыт
Global OnLine As Boolean        ' Связь через модем установлена

Global ChannalMode As Integer   ' Этап работы (определяет обработчик)
    Global Const CM_Null = 0
    Global Const CM_Modem = 1   ' Работа с модемом
    Global Const CM_Cable = 2   ' Работа со счётчиком по кабелю
    Global Const CM_Line = 3    ' Работа со счётчиком через модем (2 + 1)

' Эти ошибки фиксируются на стадии установления связи со счётчиком
' через кабель или модем.
Global ChannalErr As Integer    ' Ошибки подключения.
    Global Const CE_OK = 0
    Global Const CE_Com = 1     ' Ошибка обращения к модулю Comm32
    Global Const CE_CPr = 2     ' Ошибка назначения параметров порта
    Global Const CE_Opn = 3     ' Не удалось открыть порт
    Global Const CE_Cal = 4     ' Счётчик не отвечает
    Global Const CE_NoC = 5     ' Не удалось обнаружить счётчик
    Global Const CE_Mdm = 6     ' Модем не отвечает
    Global Const CE_NoM = 7     ' Не удалось обнаружить модем
    Global Const CE_MPr = 8     ' Не удалось сконфигурировать модем
    Global Const CE_Con = 9     ' Не удалось установить соединения

' Причины нештатного прерывания цикла приёма данных
' Более подробно ошибки линии или протокола фиксируются отдельно
Global ReceiverState As Integer ' Причина прекращения приёма
    Global Const RS_OK = 0      ' Всё ОК (Ошибок нет, нормально принято)
    Global Const RS_TimeOut = 1 ' Нет ответа (таймаут)
    Global Const RS_LostCon = 2 ' Разрыв соединения
    Global Const RS_LineErr = 3 ' Ошибки на линии связи
    Global Const RS_BadData = 4 ' Неправильный ответ
    Global Const RS_UserEsc = 5 ' Прервано пользователем

' Это -- ошибки порта. В принципе, мы их фоксируем просто из-за
' любопытства. На самом деле они нас не очень-то волнуют.
Global PortErr As Integer       ' Ошибки порта
    Global Const PE_None = 0    ' Ошибок нет
    Global Const PE_Frame = 1   ' Нарушение структура пакета
    Global Const PE_Parit = 2   ' Ошибка чётности
    Global Const PE_Break = 3   ' Сигнал "Break". Та сторона не может принять данные
    Global Const PE_LostD = 4   ' Порт потерял предыдущие данные
    Global Const PE_OverI = 5   ' Переполнение входного буфера
    Global Const PE_OverO = 6   ' Переполнение выходного буфера
    Global Const PE_Port = 7    ' Опять же ошибка обращения к порту
    Global Const PE_DCB = 8     ' Какая-то системная ошибка обращения к порту
    Global Const PE_Line = 10   ' Любая ошибка порта

' Global InWait As Boolean        ' Цикл ожидания активен


Global Rqs() As Byte        ' Данные для отправки через порт
Global nRqs As Integer      ' Число байтов запроса

Dim InPolling As Boolean
' *** Опрос порта
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


Public Function ПриёмПрерван() As Integer
    ПриёмПрерван = ReceiverState
End Function

Public Function ОшибкаСоединения(ByVal ErrNum As Integer) As String
' Просто "озвучивает" ошибки
    Select Case ErrNum
'      Case CE_OK
'        ОшибкаСоединения = "Порт открыт"
      Case CE_Com
        ОшибкаСоединения = "Ошибка обращения к модулю Comm32"
      Case CE_CPr
        ОшибкаСоединения = "Ошибка назначения параметров порта"
      Case CE_Opn
        ОшибкаСоединения = "Не удалось открыть порт"
      Case CE_Cal
        ОшибкаСоединения = "Счётчик не отвечает"
      Case CE_NoC
        ОшибкаСоединения = "Не удалось обнаружить счётчик"
      Case CE_Mdm
        ОшибкаСоединения = "Модем не отвечает"
      Case CE_NoM
        ОшибкаСоединения = "Не удалось обнаружить модем"
      Case CE_MPr
        ОшибкаСоединения = "Не удалось сконфигурировать модем"
      Case CE_Con
        ОшибкаСоединения = "Не удалось установить соединения"
      End Select
End Function

Public Sub ФиксируемОшибку()
' Процедура фиксирует ошибку протокола
'
End Sub

'   **************************************
'   ***** Начало и завершение работы *****
'   **************************************
' *** Активация и деактивация формы-носителя ***
Public Function StartPortMaster() As Boolean
' Инициирует работу с портом
    MasterLoaded = True
    StartPortMaster = True
End Function

Public Sub StopPortMaster()
' Завершает работу с портом
    MasterLoaded = False
End Sub

' *** Открытие и закрытие порта ***
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
    ' Если надо, устанавливаем другие параметры
    MyPort.SetState
End Function

Public Sub ClosePort()
        If Not MasterLoaded Then Exit Sub
    MyPort.CloseComm
    Set MyPort = Nothing
'    MasterLoaded = False
End Sub

'   ***************************
'   ***** Работа с портом *****
'   ***************************

' *** Посылка подготовленного сообщения
Public Sub SendDataToPort(Q As Variant)
' Посылает данные на порт
        If Not MasterLoaded Then Exit Sub
    ReceiverState = 0
    PortErr = 0
    MyPort.Output Q
End Sub

Public Sub TakePortData(Q As Variant)
' Принимает полученные данные и отправляет их далее на обработку
    Select Case ChannalMode
      Case CM_Null
'        НульПриём
      Case CM_Modem
        Mdm_ОбработатьПосылку Q
      Case CM_Cable, CM_Line
        MC601ОбработатьПосылку Q
      End Select
End Sub

Public Sub TakePortEvent(ByVal i As Long)
' Здесь мы используем yниверсальную процедуру
' обработки событий порта. Но, в принципе, для счётчика и для модема
' можно исеть собственные процедуры.
    If i <> 0 Then PortErr = PE_Line
    If PortErr <> PE_None Then
        ReceiverState = RS_LineErr
      End If
End Sub
