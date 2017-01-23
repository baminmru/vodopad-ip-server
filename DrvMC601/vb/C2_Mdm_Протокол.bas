Attribute VB_Name = "C2_Mdm_Протокол"
Option Explicit
'               **********************
'               *  Работа с модемом  *
'               **********************
'
'
' Модуль обеспечивает работу с модемом
'
' Код результата
Global MdmKWord As Integer    ' Код результата
    Global Const AC_None = -1       ' ещё не получен
    Global Const AC_OK = 0          ' OK
    Global Const AC_CT = 1          ' CONNECT
    Global Const AC_RI = 2          ' RING
    Global Const AC_NC = 3          ' NO CARRIER
    Global Const AC_ER = 4          ' ERROR
    Global Const AC_ND = 6          ' NO DIALTONE
    Global Const AC_BS = 7          ' BUSY
    Global Const AC_NA = 8          ' NO ANSWER
    
Dim MdmError As Integer  ' Код ошибки протокола
'    Global Const EP_None = 0    ' Всё ОК (Ошибок нет)
'    Global Const EP_TooMany = 1 ' Слишком много данных
'    Global Const EP_Raving = 2  ' Данные без запроса (бред)
    
Dim MdmState As Integer      ' Состояние автомата приёма
     Const IP_Off = 0               ' Приём выключен
     Const IP_On = 1                ' Идёт приём

' Буфер ключевого слова
Const mWrd = 16
Dim KeyWrd(0 To mWrd - 1) As Byte   ' Буфер
Dim nWrd As Integer                 ' Накопленный размер
' Буфер приёма
Const mAns = 1024
Global MdmAns(0 To mAns - 1) As Byte   ' Буфер ответа модема
Global nAns As Integer                 ' Длина ответа

Const InitPar = "1200,N,8,1"    ' Строка параметров обмена порта

' *** Формирование запроса ***
Public Function Mdm_Cmd2Rqs(Cmd As String, Rqs() As Byte) As Integer
' Строит запрос (в массиве Rqs) для отправки на счётчик.
' Возвращает размер запроса в байтах
' Cmd -- текстовая строка
' Rqs -- строка в кодировке ASCII с добавленным символом CR на конце (кроме команды +++)
Dim i As Integer
    i = Str2ASCII(Cmd, Rqs)
    If Cmd <> "+++" Then
        Rqs(i) = &HD
        i = i + 1
      End If
    Mdm_Cmd2Rqs = i
End Function

'  ****************************************
'  *  У П Р А В Л Е Н И Е  П Р И Ё М О М  *
'  ****************************************
Public Sub Mdm_НачатьПриём()
' Включает автомат приёма. Подготавливает все необх. данные.
    nAns = 0
    nWrd = 0
    MdmKWord = AC_None
    MdmError = AC_OK
    MdmState = IP_On
End Sub

Public Sub Mdm_ПрерватьПриём()
' Выключает автомат. В обычной ситуации он выключается сам.
    MdmState = IP_Off
End Sub

Public Function Mdm_ПриёмОкончен() As Boolean
' Проверяет, завершён ли приём.
    Mdm_ПриёмОкончен = (MdmState = IP_Off)
End Function

Public Function Mdm_ОшибкаПриёма() As Integer
' Возвращает код ошибки приёма данных
    Mdm_ОшибкаПриёма = MdmError
End Function

'   *********************************
'   *   А В Т О М А Т  П Р И Ё М А  *
'   *********************************
Public Sub Mdm_ОбработатьБайт(ByVal b As Byte)
' Автомат приёма. Накапливает ввод, ловит служебные слова
    Select Case MdmState
      Case IP_Off   ' Приём не ожидается. Любой вход -- ошибка
        If MdmError <> 0 Then MdmError = EP_Raving
      Case IP_On    ' Приём включён
        If nAns >= mAns Then        ' Сохраняем в приёмном буфере
            MdmState = IP_Off
            MdmError = EP_TooMany
          Else
            MdmAns(nAns) = b
            nAns = nAns + 1
          End If
        MdmKWord = IsWord2(b)       ' Фильтруем ключевое слово
        If MdmKWord <> AC_None Then
            MdmState = IP_Off
          End If
    End Select
End Sub


Function IsCRLF(ByVal b As Byte) As Boolean
' Процедура "фильтрует" входной поток на предмет последовательности CRLF
' При выявлении возвращает True
Static HaveCR As Integer    ' Состояние автомата приёма
    IsCRLF = False
    If b = &HD Then
        HaveCR = 1          ' Есть CR; Будем ждать LF
        Exit Function
      End If
    If (b = &HA) And (HaveCR = 1) Then
        IsCRLF = True       ' Ждали LF и он пришёл
'        AttCRLF = 0
'        Exit Function
      End If
    HaveCR = 0              ' Начинаем всё с начала
End Function

Function IsWord1(ByVal b As Byte) As Integer
' Процедура проверяет входной поток на ключевые слова OK или ERROR.
' Используется при обмене простыми командами с модемом.
' Возвращает номер ключевого слова
Dim s As String
    IsWord1 = AC_None
' Записываем входящий в накопительный буфер
    If nWrd < mWrd Then
        KeyWrd(nWrd) = b
        nWrd = nWrd + 1
      End If
' Надо ли проверять на ключевой слово
    If Not IsCRLF(b) Then Exit Function
' Сравниваем с ключевым словом
    If nWrd > 2 Then    ' Длина слова достаточна?
        s = ASCII2Str(KeyWrd, nWrd - 2)
        If s = "OK" Then
            IsWord1 = AC_OK
          ElseIf s = "ERROR" Then
            IsWord1 = AC_ER
          End If
      End If
    nWrd = 0            ' Начинаем набор слова заново
End Function

Function IsWord2(ByVal b As Byte) As Integer
' Процедура проверяет входной поток на ключевые слова OK или ERROR.
' Используется при командах подключения
' Возвращает номер ключевого слова
Dim s As String
    IsWord2 = AC_None
    If nWrd < mWrd Then
        KeyWrd(nWrd) = b
        nWrd = nWrd + 1
      End If
' Надо ли проверять на ключевой слово
    If Not IsCRLF(b) Then Exit Function
' Сравниваем с ключевым словом
    If nWrd > 2 Then    ' Длина слова достаточна?
        s = ASCII2Str(KeyWrd, nWrd - 2)
        Select Case s
          Case "OK"
            IsWord2 = AC_OK
          Case "ERROR"
            IsWord2 = AC_ER
          Case "NO DIALTONE"
            IsWord2 = AC_ND
          Case "BUSY"
            IsWord2 = AC_BS
          Case "NO ANSWER"
            IsWord2 = AC_NA
          Case "NO CARRIER"
            IsWord2 = AC_NC
          Case Else
            If Left(s, 7) = "CONNECT" Then
                IsWord2 = AC_CT
              End If
          End Select
      End If
    nWrd = 0            ' Начинаем набор слова заново
End Function

Function IsNoCarrier(ByVal b As Byte) As Integer
' Процедура проверяет входной поток на ключевое слово NO CARRIER
' Используется в процессе приёма данных
' Возвращает номер ключевого слова
Dim s As String
    IsNoCarrier = AC_None
' Записываем входящий в накопительный буфер
    If nWrd < mWrd Then
        KeyWrd(nWrd) = b
        nWrd = nWrd + 1
      End If
' Надо ли проверять на ключевой слово
    If Not IsCRLF(b) Then Exit Function
' Сравниваем с ключевым словом
    If nWrd = 12 Then   ' Длина подходит?
        If ASCII2Str(KeyWrd, 10) = "NO CARRIER" Then
            IsNoCarrier = AC_NC
          End If
      End If
    nWrd = 0            ' Начинаем набор слова заново
End Function

Function IsKeyWord(ByVal b As Byte, ByRef iState As Integer, Sampler() As Byte, ByVal n As Integer) As Boolean
' Процедура "фильтрует" входной поток на предмет заданной последовательности байт
' (параметр Sampler, индексация от 0, число байт в образце -- параметр n).
' Входной поток поступает в виде очередного байта (параметр b), который сравнивается
' с тем, что ожидается в i-й позиции (параметр iState). Парамет iState -- "память" автомата.
' Значение 0 -- исходное состояние, >0 -- начало образца распознано, ожидается следующий символ.
' При выявлении образца функция возвращает True
    IsKeyWord = False
    If b = Sampler(iState) Then
        iState = iState + 1
        If iState = n Then
            iState = 0
            IsKeyWord = True
          End If
      Else
        iState = 0
      End If
End Function

Function СообщениеМодема(ByVal i As Integer) As String
    Select Case i
      Case AC_OK
        СообщениеМодема = "Команда выполнена"
      Case AC_CT
        СообщениеМодема = "Подключение установлено"
      Case CE_CPr
        СообщениеМодема = "Ошибка назначения параметров порта"
      Case AC_RI
        СообщениеМодема = "Звонок!"
      Case AC_NC
        СообщениеМодема = "Не удалось установить соединение"
      Case AC_ER
        СообщениеМодема = "Неверная команда или номер"
      Case AC_ND
        СообщениеМодема = "Нет гудка в линии"
      Case AC_BS
        СообщениеМодема = "Линия занята"
      Case AC_NA
        СообщениеМодема = "Нет ответа"
      End Select
End Function
