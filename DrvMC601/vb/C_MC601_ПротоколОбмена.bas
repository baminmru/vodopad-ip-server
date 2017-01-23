Attribute VB_Name = "C_MC601_ПротоколОбмена"
Option Explicit
'               ****************************
'               *  M U L T I C A L  6 0 1  *
'               ****************************
' Модуль обеспечивает протокол обмена со счётчиком KUMSTRUP MULTICAL 601
' Ошибки, не относящиеся к структуре получаемого сообщения (протокола)
' здесь не фиксируются. Автомат приёма они не касаются!
'
' Ошибки протокола
Dim ОшибкаПротокола As Integer  ' Ошибка протокола
    Global Const EP_None = 0    ' Всё ОК (Ошибок нет)
    Global Const EP_TooMany = 1 ' Слишком много данных
    Global Const EP_Raving = 2  ' Данные без запроса (бред)
    Global Const EP_Head = 3    ' Нет стартового байта
    Global Const EP_Body = 4    ' Нет стартового байта после эха
    Global Const EP_Stuff = 5   ' Неправильная замена
    Global Const EP_CRC = 6     ' Ошибка CRC

' Состояния конечного автомата приёма
Dim СостояниеПриёма As Integer      ' Состояние автомата приёма
     Const IW_Off = 0               ' Приём выключен
     Const IW_Wait = 1              ' Ждём эхо
     Const IW_Echo = 2              ' Приём эха
     Const IW_Paus = 3              ' Ждём собственно ответ
     Const IW_Resp = 4              ' Приём ответа
     Const IW_Stuf = 5              ' Ждём замену

' Служебные коды (и замены)
Const SB_HdQ = &H80, St_HdQ = &H7F  ' Старт-байт запроса
Const SB_HdR = &H40, St_HdR = &HBF  ' Старт-байт ответа
Const SB_End = &HD, St_End = &HF2   ' Стоп-байт
Const SB_Stf = &H1B, St_Stf = &HE4  ' Байт префикса замены
Const SB_Ack = &H6, St_Ack = &HF9   ' Подтверждение


'  ********************************************
'  *  Ф О Р М И Р О В А Н И Е  З А П Р О С А  *
'  ********************************************
Public Function MC601Cmd2Rqs(Cmd() As Byte, ByVal n As Integer, Rqs() As Byte) As Integer
' Строит запрос согласно протоколу обмена (в массиве Rqs) для отправки на счётчик.
' Возвращает размер запроса в байтах
' Cmd -- байтовый массив с командой в формате:
'       Байт 0  -- адрес назначения
'       Байт 1  -- номер команды
'       Байт 2 и  т.д. -- аргументы команды
'   Всего n байт
' Команду в массиве Cmd формирует вызывающая программа.
' Массив Cmd должен допускать размещение дополнительных 2 байтов CRC.
Dim b As Byte
Dim i As Integer, j As Integer
        If n <= 0 Then Exit Function
  ' К команде дописываем CRC
    i = CRC_CCITT16(Cmd, n, 0)
    Cmd(n + 0) = I2B_Hi(i)
    Cmd(n + 1) = I2B_Lo(i)
  ' Строим сообщение
    Rqs(0) = SB_HdQ         ' Добавляем стартовый байт
    j = 1
    For i = 0 To n + 1
        b = Cmd(i)          ' Переносим байты команды,
        If EnStuff(b) Then ' по ходу выполняя замену
            Rqs(j) = SB_Stf
            j = j + 1
          End If
        Rqs(j) = b
        j = j + 1
      Next i
    Rqs(j) = SB_End         ' Добавляем стоповый байт
    j = j + 1
    MC601Cmd2Rqs = j
End Function


'  ****************************************
'  *  У П Р А В Л Е Н И Е  П Р И Ё М О М  *
'  ****************************************
Public Sub MC601НачатьПриём()
' Включает автомат приёма. Подготавливает все необх. данные.
    MC601Принято = 0
    ОшибкаПротокола = EP_None
    СостояниеПриёма = IW_Wait
End Sub

Public Sub MC601ПрерватьПриём()
' Выключает автомат. В обычной ситуации он выключается сам.
' Используется при возникновении ошибок линии или таймаута
        СостояниеПриёма = IW_Off
End Sub

Public Function MC601ПриёмОкончен() As Boolean
' Проверяет, завершён ли приём.
    MC601ПриёмОкончен = (СостояниеПриёма = IW_Off)
End Function

Public Sub MC601ПроверитьCRC()
' Проверяет и отсекает CRC
' Проверку надо выполнять сразу же после приёма!!!
If ОшибкаПротокола = EP_None Then
    If CRC_CCITT16(MC601Ответ, MC601Принято, 0) = 0 Then
        MC601Принято = MC601Принято - 2
      Else
        ОшибкаПротокола = EP_CRC
      End If
  End If
End Sub

Public Function MC601ОшибкаПриёма() As Integer
' Возвращает код ошибки приёма данных
    MC601ОшибкаПриёма = ОшибкаПротокола
End Function


'   *********************************
'   *   А В Т О М А Т  П Р И Ё М А  *
'   *********************************
Public Sub MC601ОбработатьБайт(ByVal b As Byte)
' Автомат приёма. Отсекает эхо, убирает стартовый и стоповый байтвы и т.д.
    Select Case СостояниеПриёма
      Case IW_Off   ' Приём не ожидается. Любой вход -- ошибка
        If ОшибкаПротокола <> 0 Then ОшибкаПротокола = EP_Raving
      Case IW_Wait  ' Ждём эха или собственно ответа.
        If b = SB_HdQ Then              ' Начало эха с опто-головки
            СостояниеПриёма = IW_Echo
        ElseIf b = SB_HdR Then
            СостояниеПриёма = IW_Resp   ' Начало ответа
          Else
            СостояниеПриёма = IW_Off
            ОшибкаПротокола = EP_Head   ' Не стартовый байт
          End If
      Case IW_Echo  ' Принимаем эхо. По стоп-байту прекращаем
        If b = SB_End Then
            СостояниеПриёма = IW_Paus   ' Завершаем приём эха
          Else
            ' В принципе, можно сравнивать с запросом
            ' Но мы пока ничего не делаем
          End If
      Case IW_Paus  ' Ждём ответа. Допустим только стартовый байт
        If b = SB_HdR Then
            СостояниеПриёма = IW_Resp
          ElseIf b = SB_Ack Then
            СостояниеПриёма = IW_Off
            ЗаписатьБайт b
          Else
            СостояниеПриёма = IW_Off
            ОшибкаПротокола = EP_Body   ' Не стартовый байт
            ЗаписатьБайт b
          End If
      Case IW_Resp  ' Принимаем ответ
        If b = SB_End Then
            СостояниеПриёма = IW_Off
          ElseIf b = SB_Stf Then
            СостояниеПриёма = IW_Stuf
          Else
            ЗаписатьБайт b
          End If
      Case IW_Stuf  ' Ждём байт замены
        If DeStuff(b) Then
            СостояниеПриёма = IW_Resp
          Else
            СостояниеПриёма = IW_Off
            ОшибкаПротокола = EP_Stuff  ' Неправильная замена
          End If
        ЗаписатьБайт b
      End Select
End Sub

Private Sub ЗаписатьБайт(ByVal b As Byte)
' Записывает очередной байт в приёмный буфер. Нужна для предыдущей процедуры
    If MC601Принято >= MaxRsp Then
        СостояниеПриёма = IW_Off
        ОшибкаПротокола = EP_TooMany
      Else
        MC601Ответ(MC601Принято) = b
        MC601Принято = MC601Принято + 1
      End If
End Sub


'  ************************************
'  *  S T U F F I N G -- З А М Е Н А  *
'  ************************************
Private Function EnStuff(ByRef b As Byte) As Boolean
' Проверяет, требует ли байт замены. Если да, то заменяет его и возвращает True
    EnStuff = True
    Select Case b
      Case SB_HdQ
        b = St_HdQ
      Case SB_HdR
        b = St_HdR
      Case SB_End
        b = St_End
      Case SB_Stf
        b = St_Stf
      Case SB_Ack
        b = St_Ack
      Case Else
        EnStuff = False
      End Select
End Function

Private Function DeStuff(ByRef b As Byte) As Boolean
' Проверяет, является ли быйт заменителем. Если да, то возвращает нужный.
' Фактически -- обратная операция к Stuffing.
    DeStuff = True
    Select Case b
      Case St_HdQ
        b = SB_HdQ
      Case St_HdR
        b = SB_HdR
      Case St_End
        b = SB_End
      Case St_Stf
        b = SB_Stf
      Case St_Ack
        b = SB_Ack
      Case Else
        DeStuff = False
      End Select
End Function

