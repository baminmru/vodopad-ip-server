
Option Explicit On

Imports System.Threading.Thread
Module Module1


    Public Const MC601_AdrMain = &H3F       ' Адрес основного модуля
    Public Const MC601_AdrHour = &H7F       ' Адрес дополнительного модуля (для часовых архивов)

    Public MC601Command(0 To 15) As Byte    ' Команда на счётчик

    Public Const MaxRsp = 1023
    Public MC601Resp(0 To MaxRsp) As Byte  ' Буфер ответа счётчика
    Public MC601Rsvd As Integer          ' Длина ответа




    ' ************************
    ' *** РАБОТА С КАНАЛОМ ***
    ' ************************


    Private Function MC601_НайтиСчётчик(Номер As Integer) As Integer
        ' Ищет порт. Аргумент задаёт предпочтительный номер
        ' Возвращает номер порта, который ответил, 0 -- в случае неудачи
        ' В случае порт будет открыт
        Dim i As Integer, k As Integer
        MC601_НайтиСчётчик = 0
        ' Сначала пробуем переданный номер порта
        If Номер <> 0 Then
            i = Номер
            If MC601ОткрытьПорт(i) = 0 Then
                If MC601_СчитатьНомер() <> 0 Then
                    MC601_НайтиСчётчик = i
                    Exit Function
                End If
                ClosePort()
            End If
        End If
        ' Атака не удалась, ищем перебором
        For i = 1 To 8
            If i <> Номер Then
                If MC601ОткрытьПорт(i) = 0 Then
                    If MC601_СчитатьНомер() <> 0 Then
                        MC601_НайтиСчётчик = i
                        Exit Function
                    End If
                    ClosePort()
                End If
            End If
        Next i
    End Function


    ' ************************
    ' *** ПРОСТЫЕ ОПЕРАЦИИ ***
    ' ************************
    Private Function MC601_СчитатьТип()
        ' В случае неудачи возвращает Null

        MC601Command(0) = &H3F
        MC601Command(1) = &H1
        If MC601ВыполнитьКоманду(MC601Command, 2) <> 0 Then Return 0
        MC601_СчитатьТип = Bytes2I(MC601Resp, 2, 2)
    End Function

    Private Function MC601_СчитатьНомер() As Object
        ' В случае неудачи возвращает 0. Номер, в принципе, может быть буквенно-цифровым
        MC601_СчитатьНомер = Nothing

        MC601Command(0) = &H3F
        MC601Command(1) = &H2
        If MC601ВыполнитьКоманду(MC601Command, 2) <> 0 Then Exit Function
        MC601_СчитатьНомер = Bytes2L(MC601Resp, 4, 2)
    End Function

    Private Function MC601_СчитатьТекущий(Регистр As Integer)

        MC601Command(0) = &H3F
        MC601Command(1) = &H10
        MC601Command(2) = &H1
        I2Bytes(Регистр, MC601Command, 3)
        If MC601ВыполнитьКоманду(MC601Command, 5) <> 0 Then Exit Function ' Ошибка
        If MC601Rsvd <= 7 Then Exit Function ' Пустой ответ
        '   MC601Resp(4)                           ' Единица измерения
        MC601_СчитатьТекущий = Byte2Mantiss(MC601Resp, MC601Resp(5), 7) _
                            * Byte2Factor(MC601Resp(6))
    End Function


    ' *********************
    ' *** ЧТЕНИЕ ДАННЫХ ***
    ' *********************

    Private Function MC601_ПустойСуточный(Регистр As Integer, _
                    ЧитатьОт As Integer, НеМенее As Integer, ByRef Считано As Integer) As Object
        Dim a(64) As Double
        Dim i As Integer
        For i = 1 To НеМенее
            a(i) = 0
        Next i
        Считано = НеМенее
        MC601_ПустойСуточный = a
    End Function

    Private Function MC601_ПустойЧасовой(Регистр As Integer, Дата As Date) As Object
        ' Читает из регистр из часового архива за указанную дату. Возвращает массив из 24 показаний.
        Dim a(24)
        Dim i As Integer

        For i = 1 To 24
            If Регистр = 1003 Then
                a(i) = Дата
            ElseIf Регистр = 1002 Then
                a(i) = 25 - i
            Else
                a(i) = 0
            End If
        Next i
        MC601_ПустойЧасовой = a
    End Function


    Private Function MC601_СчитатьЧасовой(ByVal Регистр As Integer, ByVal Дата As Date) As Object
        Dim a(24) As Double
        Dim i As Integer, j As Integer, k As Integer
        Dim l As Integer, m As Integer, n As Integer
        Dim iErr As Integer, iPos As Integer
        Dim f As Double, b As Double
        Dim DateMark As Date
        Dim iHrs As Integer

        If (Регистр = 1003) Or (Регистр = 1002) Then
            MC601_СчитатьЧасовой = MC601_ПустойЧасовой(Регистр, Дата)
            Exit Function
        End If

        For j = 1 To 24
            a(j) = 0
        Next j

        Дата = Дата.AddDays(1)
        MC601Command(0) = &H7F
        MC601Command(1) = &H63
        I2Bytes(Регистр, MC601Command, 2)
        MC601Command(4) = Year(Дата) - 2000
        MC601Command(5) = Month(Дата)
        MC601Command(6) = Day(Дата)
        MC601Command(7) = 0
        iErr = MC601ВыполнитьКоманду(MC601Command, 8)
        If iErr <> 0 Then Exit Function
        '   MC601Resp(4)               ' Единица измерения
        l = MC601Resp(5)               ' Размер мантиссы
        f = Byte2Factor(MC601Resp(6))  ' Фактор
        k = (MC601Rsvd - 8) \ l      ' Количество отсчётов полученное (24)
        If k = 0 Then
            iErr = 99
            MC601_СчитатьЧасовой = a
            Exit Function
        End If
        iPos = 8
        If k > 24 Then k = 24
        For j = 1 To k
            a(j) = Byte2Mantiss(MC601Resp, l, iPos) * f
            iPos = iPos + l
        Next j
        MC601_СчитатьЧасовой = a
    End Function


    Private Function MC601_СчитатьСуточный(Регистр As Integer, _
                    ЧитатьОт As Integer, НеМенее As Integer, ByRef n As Integer) As Object
        Dim a(64) As Double
        Dim f As Double, b As Double
        Dim i As Integer, j As Integer
        Dim k As Integer, l As Integer
        Dim iPos As Integer, iErr As Integer


        MC601Command(0) = &H3F
        MC601Command(1) = &H66
        I2Bytes(Регистр, MC601Command, 2)
        i = ЧитатьОт                      ' Рамка считывания
        n = 0
        Do While n < НеМенее
            I2Bytes(i, MC601Command, 4)
            ' Послать запрос и принять данные
            iErr = MC601ВыполнитьКоманду(MC601Command, 6)
            If iErr <> 0 Then GoTo 1
            '   Ответ(4)                    ' Единица измерения
            l = MC601Resp(5)               ' Размер мантиссы
            f = Byte2Factor(MC601Resp(6))  ' Фактор
            k = (MC601Rsvd - 7) \ l      ' Количество отсчётов полученное
            If k = 0 Then GoTo 1
            iPos = 7
            For j = 1 To k
                n = n + 1
                a(n) = Byte2Mantiss(MC601Resp, l, iPos) * f
                iPos = iPos + l
            Next j
            i = i + k
        Loop
1:
        MC601_СчитатьСуточный = a
    End Function

    Private Function MC601_СчитатьМесячный(Регистр As Integer, _
                    ЧитатьОт As Integer, НеМенее As Integer, ByRef n As Integer) As Object
        Dim a(64) As Double
        Dim f As Double, b As Double
        Dim i As Integer, j As Integer
        Dim k As Integer, l As Integer
        Dim iPos As Integer, iErr As Integer



        MC601Command(0) = &H3F
        MC601Command(1) = &H65
        I2Bytes(Регистр, MC601Command, 2)
        i = ЧитатьОт                      ' Рамка считывания
        n = 0
        Do While n < НеМенее
            I2Bytes(i, MC601Command, 4)
            ' Послать запрос и принять данные
            iErr = MC601ВыполнитьКоманду(MC601Command, 6)
            If iErr <> 0 Then GoTo 1
            '   Ответ(4)                    ' Единица измерения
            l = MC601Resp(5)               ' Размер мантиссы
            f = Byte2Factor(MC601Resp(6))  ' Фактор
            k = (MC601Rsvd - 7) \ l      ' Количество отсчётов полученное
            If k = 0 Then GoTo 1
            iPos = 7
            For j = 1 To k
                n = n + 1
                a(n) = Byte2Mantiss(MC601Resp, l, iPos) * f
                iPos = iPos + l
            Next j
            i = i + k
        Loop
1:
        MC601_СчитатьМесячный = a
    End Function



    Private Function MC601ВыполнитьКоманду(Cmd() As Byte, ByVal n As Integer) As Integer
        ' Готовит из команды запрос, посылает его, ждёт ответа
        ' Готовим запрос
        ReDim Rqs(127)                 ' Готовим массив запроса
        nRqs = MC601Cmd2Rqs(Cmd, n, Rqs)    ' Формируем в нём запрос
        ReDim Preserve Rqs(0 To nRqs - 1)   ' Отсекаем хвосты
        MC601НачатьПриём()        ' Активируем автомат приёма
        SendDataToPort(Rqs)      ' Посылаем запрос
        ' Посылаем и ждём ответа
        ЗапуститьТаймер()         ' Включаем счётчик тайм-аута
        Do                      ' Ждём завершения приёма


            If MC601ПриёмОкончен() Then Exit Do
            ПроверитьТаймер()
            If ПриёмПрерван() Then Exit Do
            System.Threading.Thread.Sleep(20)
        Loop
        ' Завершаем получение
        ОстановитьТаймер()        ' Отключаем слежение за тайм-аутом
        If ReceiverState = 0 Then
            MC601ПроверитьCRC()       ' Проверям и удаляем CRC
            If MC601ОшибкаПриёма() <> 0 Then
                ReceiverState = RS_BadData
            End If
        End If
        MC601ВыполнитьКоманду = ReceiverState
    End Function

    Public Sub MC601ОбработатьПосылку(Q As Object)
        ' Направляет очередную порцию полученных байтов в автомат приёма
        ' Вызывается из коммуникационной программы
        Dim i As Integer
        For i = 0 To Q.ToString().Length - 1
            MC601ОбработатьБайт(Q(i))
        Next i
    End Sub


    '               ****************************
    '               *  M U L T I C A L  6 0 1  *
    '               ****************************
    ' Модуль обеспечивает протокол обмена со счётчиком KUMSTRUP MULTICAL 601
    ' Ошибки, не относящиеся к структуре получаемого сообщения (протокола)
    ' здесь не фиксируются. Автомат приёма они не касаются!
    '
    ' Ошибки протокола
    Dim ОшибкаПротокола As Integer  ' Ошибка протокола
    Public Const EP_None = 0    ' Всё ОК (Ошибок нет)
    Public Const EP_TooMany = 1 ' Слишком много данных
    Public Const EP_Raving = 2  ' Данные без запроса (бред)
    Public Const EP_Head = 3    ' Нет стартового байта
    Public Const EP_Body = 4    ' Нет стартового байта после эха
    Public Const EP_Stuff = 5   ' Неправильная замена
    Public Const EP_CRC = 6     ' Ошибка CRC

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
    Private Function MC601Cmd2Rqs(Cmd() As Byte, ByVal n As Integer, Rqs() As Byte) As Integer
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
        MC601Rsvd = 0
        ОшибкаПротокола = EP_None
        СостояниеПриёма = IW_Wait
    End Sub

    Public Sub MC601ПрерватьПриём()
        ' Выключает автомат. В обычной ситуации он выключается сам.
        ' Используется при возникновении ошибок линии или таймаута
        СостояниеПриёма = IW_Off
    End Sub

    Private Function MC601ПриёмОкончен() As Boolean
        ' Проверяет, завершён ли приём.
        MC601ПриёмОкончен = (СостояниеПриёма = IW_Off)
    End Function

    Public Sub MC601ПроверитьCRC()
        ' Проверяет и отсекает CRC
        ' Проверку надо выполнять сразу же после приёма!!!
        If ОшибкаПротокола = EP_None Then
            If CRC_CCITT16(MC601Resp, MC601Rsvd, 0) = 0 Then
                MC601Rsvd = MC601Rsvd - 2
            Else
                ОшибкаПротокола = EP_CRC
            End If
        End If
    End Sub

    Private Function MC601ОшибкаПриёма() As Integer
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
                    ЗаписатьБайт(b)
                Else
                    СостояниеПриёма = IW_Off
                    ОшибкаПротокола = EP_Body   ' Не стартовый байт
                    ЗаписатьБайт(b)
                End If
            Case IW_Resp  ' Принимаем ответ
                If b = SB_End Then
                    СостояниеПриёма = IW_Off
                ElseIf b = SB_Stf Then
                    СостояниеПриёма = IW_Stuf
                Else
                    ЗаписатьБайт(b)
                End If
            Case IW_Stuf  ' Ждём байт замены
                If DeStuff(b) Then
                    СостояниеПриёма = IW_Resp
                Else
                    СостояниеПриёма = IW_Off
                    ОшибкаПротокола = EP_Stuff  ' Неправильная замена
                End If
                ЗаписатьБайт(b)
        End Select
    End Sub

    Private Sub ЗаписатьБайт(ByVal b As Byte)
        ' Записывает очередной байт в приёмный буфер. Нужна для предыдущей процедуры
        If MC601Rsvd >= MaxRsp Then
            СостояниеПриёма = IW_Off
            ОшибкаПротокола = EP_TooMany
        Else
            MC601Resp(MC601Rsvd) = b
            MC601Rsvd = MC601Rsvd + 1
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


    Function ShortResidue(ByVal P As Byte, ByVal Z As Integer) As Integer
        ' Функция вычисляет остаток от деления полинома p00 на делитель вида 1zz.
        ' P -- старший байт делимого, Z -- маска делителя
        ' Возвращает остаток от деления (2 байтовое число).
        Dim i As Integer
        Dim r As Integer       ' Накопитель остатка
        r = 0
        For i = 1 To 8
            If ShLInt(r) <> ShLByte(P) Then r = r Xor Z
        Next i
        ShortResidue = r
    End Function

    Function CRC_CCITT16(a() As Byte, n As Integer, iPos As Integer) As Integer
        ' Функция вычисляет CCITT CRC-16 для массива байтов A().
        ' iPos -- начальная позиция в массиве.
        ' Возвращает код CRC (2 байтовое число).
        Const InitR = 0         ' Начальный остаток
        Const MaskZ = &H1021    ' Маска делителя

        Dim i As Integer
        Dim P As Byte           ' Головной байт
        Dim r As Integer        ' Остаток
        r = InitR
        If n > 0 Then
            For i = 0 To n - 1
                P = ChLInt(r) Xor a(iPos + i)
                r = r Xor ShortResidue(P, MaskZ)
            Next i
        End If
        CRC_CCITT16 = r
    End Function

    ' Вспомогательный модуль. Содержит процедуры считывания байтового
    ' массива в 16-ричной записи (типа 3F,06,78,A4,F6) и, наобором,
    ' вывод значений байтового массива в текстовую строку.
    '   Кроме того -- процедуры копирования и вставки.
    '

    ' *** Hex-строки ***
    ' Преобразование строки чисел в Hex-кодировке в массив байт и обратно
    '
    Function GetNextPart(ByRef s As String, d As String) As String
        ' Возвращает очередное слово, пропускает все резделители (в строке Q).
        ' В переменной S оставляет остаток строки
        Dim i As Long
        s = LTrim(s)
        i = 1
        Do While i <= Len(s)
            If InStr(1, d, Mid(s, i, 1)) <> 0 Then Exit Do
            i = i + 1
        Loop
        GetNextPart = Mid(s, 1, i - 1)
        s = Mid(s, i + 1)
    End Function

    Function HexString2Bytes(ByRef s As String, b() As Byte) As Integer
        ' Разбирает строку байтов в 16-ричной записи.
        ' Возращает число введённых значений (или номер ошибочного числа зо знаком минус)
        Dim i As Integer
        i = 0
        On Error Resume Next
        Do Until Len(s) = 0
            b(i) = CByte("&h" & GetNextPart(s, " ,;"))
            i = i + 1
            If Err.Number <> 0 Then
                MsgBox("Ошибка в группе " & CStr(i), vbOKOnly, "Внимание!")
                HexString2Bytes = True
                i = -i
                Exit Do
            End If
        Loop
        HexString2Bytes = i
    End Function

    Function Bytes2HexString(b() As Byte, ByVal n As Integer) As String
        Dim s As String
        Dim i As Integer
        s = ""
        For i = 0 To n - 1
            If s <> "" Then s = s & ", "
            s = s & Hex(b(i))
        Next i
        Bytes2HexString = s
    End Function

    ' *** ASCII-строки ***
    ' Преобразование строки символов в ANSI-кодировке в массив байт,
    ' содержащих ASCII-кодировку тех же символов
    Function Str2ASCII(ByRef s As String, b() As Byte) As Integer
        ' Переводит строку символов в последовательность ASCII-кодов
        ' Возвращает число байт
        Dim i As Integer, k As Long, n As Integer, a As String
        n = Len(s)
        Str2ASCII = n
        If n = 0 Then Exit Function
        For i = 0 To n - 1
            a = Mid(s, i + 1, 1)
            k = Asc(a)
            If (k < 0) Or (k > 255) Then k = 0
            b(i) = k
        Next i
    End Function

    Function ASCII2Str(b() As Byte, ByRef n As Integer) As String
        ' Выполняет обратное преобразование
        Dim i As Integer, s As String
        If n = 0 Then Return ""
        s = ""
        For i = 0 To n - 1
            s = s & Chr(b(i))
        Next i
        ASCII2Str = s
    End Function




    Public WrkPortNumber As Integer ' Номер открытого порта. Нужно для повторного открытия
    Public OnPort As Boolean        ' Порт открыт
    Public OnLine As Boolean        ' Связь через модем установлена



    ' Эти ошибки фиксируются на стадии установления связи со счётчиком
    ' через кабель или модем.
    Public ChannalErr As Integer    ' Ошибки подключения.
    Public Const CE_OK = 0
    Public Const CE_Com = 1     ' Ошибка обращения к модулю Comm32
    Public Const CE_CPr = 2     ' Ошибка назначения параметров порта
    Public Const CE_Opn = 3     ' Не удалось открыть порт
    Public Const CE_Cal = 4     ' Счётчик не отвечает
    Public Const CE_NoC = 5     ' Не удалось обнаружить счётчик
    Public Const CE_Mdm = 6     ' Модем не отвечает
    Public Const CE_NoM = 7     ' Не удалось обнаружить модем
    Public Const CE_MPr = 8     ' Не удалось сконфигурировать модем
    Public Const CE_Con = 9     ' Не удалось установить соединения

    ' Причины нештатного прерывания цикла приёма данных
    ' Более подробно ошибки линии или протокола фиксируются отдельно
    Public ReceiverState As Integer ' Причина прекращения приёма
    Public Const RS_OK = 0      ' Всё ОК (Ошибок нет, нормально принято)
    Public Const RS_TimeOut = 1 ' Нет ответа (таймаут)
    Public Const RS_LostCon = 2 ' Разрыв соединения
    Public Const RS_LineErr = 3 ' Ошибки на линии связи
    Public Const RS_BadData = 4 ' Неправильный ответ
    Public Const RS_UserEsc = 5 ' Прервано пользователем

    ' Это -- ошибки порта. В принципе, мы их фоксируем просто из-за
    ' любопытства. На самом деле они нас не очень-то волнуют.
    Public PortErr As Integer       ' Ошибки порта
    Public Const PE_None = 0    ' Ошибок нет
    Public Const PE_Frame = 1   ' Нарушение структура пакета
    Public Const PE_Parit = 2   ' Ошибка чётности
    Public Const PE_Break = 3   ' Сигнал "Break". Та сторона не может принять данные
    Public Const PE_LostD = 4   ' Порт потерял предыдущие данные
    Public Const PE_OverI = 5   ' Переполнение входного буфера
    Public Const PE_OverO = 6   ' Переполнение выходного буфера
    Public Const PE_Port = 7    ' Опять же ошибка обращения к порту
    Public Const PE_DCB = 8     ' Какая-то системная ошибка обращения к порту
    Public Const PE_Line = 10   ' Любая ошибка порта

    ' Public InWait As Boolean        ' Цикл ожидания активен


    Public Rqs() As Byte        ' Данные для отправки через порт
    Public nRqs As Integer      ' Число байтов запроса





    Private Function ПриёмПрерван() As Integer
        ПриёмПрерван = ReceiverState
    End Function

    Private Function ОшибкаСоединения(ByVal ErrNum As Integer) As String
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


    '   ***************************
    '   ***** Работа с портом *****
    '   ***************************

    ' *** Посылка подготовленного сообщения
    Public Sub SendDataToPort(Q As Object)
        ' Посылает данные на порт

        ReceiverState = 0
        PortErr = 0
        MyPort.Output(Q)
    End Sub

    Public Sub TakePortData(Q As Object)
        MC601ОбработатьПосылку(Q)
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

    ' *********************************
    ' **** ДЕКОДИРОВАНИЕ РЕГИСТРОВ ****
    ' *********************************
    Function Bytes2Float(b() As Byte, ByVal iPos As Integer) As Double
        ' Разбор вещественных в формате протокола Kamstrup.
        '
        Dim i As Integer, n As Integer
        Dim a As Double, d As Double
        a = 0
        i = 0
        n = b(iPos)     ' Берём число байтов
        Do While i < n
            a = a * 256
            i = i + 1
            a = a + b(iPos + 1 + i)
        Loop
        d = 1
        n = b(iPos + 1)
        i = n And &H3F
        Do While i > 0
            d = d * 10
            i = i - 1
        Loop
        If (n And &H40) <> 0 Then d = 1 / d
        a = a * d
        If (n And &H80) <> 0 Then a = -a
        Bytes2Float = a
    End Function

    Function Byte2Factor(ByVal b As Byte) As Double
        Dim d As Double
        Dim i As Integer
        d = 1
        i = b And &H3F
        Do While i > 0
            d = d * 10
            i = i - 1
        Loop
        If (b And &H40) <> 0 Then d = 1 / d
        If (b And &H80) <> 0 Then d = -d
        Byte2Factor = d
    End Function

    Function Byte2Mantiss(b() As Byte, ByVal n As Integer, ByVal iPos As Integer) As Double
        Dim a As Double
        Dim i As Integer
        a = 0
        i = 0
        Do While i < n
            a = a * 256
            a = a + b(iPos + i)
            i = i + 1
        Loop
        Byte2Mantiss = a
    End Function

    ' ***********************************
    ' **** УПАКОВКА/РАСПАКОВКА ЦЕЛЫХ ****
    ' ***********************************
    Function Bytes2I(b() As Byte, ByVal n As Integer, ByVal iPos As Integer) As Integer
        ' XX, YY -> XXYY
        ' Преобразует последовательность байтов в одинарное целое. Первый байт -- старший.
        ' ВНИМАНИЕ! В целом останутся ПОСЛЕДНИЕ 2 байта!
        Dim k As Integer
        Dim i As Integer
        k = 0
        i = 0
        Do While i < n
            ChLInt(k)
            k = k Or b(i + iPos)
            i = i + 1
        Loop
        Bytes2I = k
    End Function

    Sub I2Bytes(ByVal k As Integer, b() As Byte, ByVal iPos As Integer)
        ' XXYY -> XX, YY
        ' Преобразует одинарное целое в массив из 2 байтов. Первый байт -- старший.
        Dim i As Integer
        For i = 0 To 1
            b(i + iPos) = ChLInt(k)
        Next i
    End Sub

    Function Bytes2L(b() As Byte, ByVal n As Integer, ByVal iPos As Integer) As Long
        ' XX, YY, ZZ, UU -> XXYYZZUU
        ' Преобразует последовательность байтов в длинное целое. Первый байт -- старший.
        ' ВНИМАНИЕ! В длинном целом останутся ПОСЛЕДНИЕ 4 байта!
        Dim l As Long
        Dim i As Integer
        l = 0
        i = 0
        Do While i < n
            ChLLng(l)
            l = l Or b(i + iPos)
            i = i + 1
        Loop
        Bytes2L = l
    End Function

    Sub L2Bytes(ByVal l As Long, b() As Byte, ByVal iPos As Integer)
        ' Преобразует длинное целое в массив из 4 байтов. Первый байт -- старший.
        Dim i As Integer
        For i = 0 To 3
            b(i + iPos) = ChLLng(l)
        Next i
    End Sub

    ' ************************
    ' ***** СДВИГИ БИТОВ *****
    ' ************************
    Function ShLByte(ByRef b As Byte) As Boolean
        ' Сдвигает биты в байте на 1 влево. Возвращает True, если был перенос
        ShLByte = (b And &H80) <> 0
        b = b And &H7F
        b = b * 2
    End Function

    Function ShRByte(ByRef b As Byte) As Boolean
        ' Сдвигает биты в байте на 1 вправо. Возвращает True, если был перенос
        ShRByte = (b And 1) <> 0
        b = b \ 2
    End Function

    Function ShLInt(ByRef i As Integer) As Boolean
        ' Сдвигает биты в целом на 1 влево. Возвращает True, если был перенос
        Dim k As Integer
        ShLInt = (i And &H8000) <> 0
        k = i And &H4000        ' Запоминаем 14-й бит в переменной k
        i = i And &H3FFF        ' Очищаем оба бита 14 и 15
        i = i * 2               ' Выполняем сдвиг влево
        If k <> 0 Then i = i Or &H8000 ' Записываем старый 14-й бит на место 15-го
    End Function

    Function ShRInt(ByRef i As Integer) As Boolean
        ' Сдвигает биты в целом на 1 влево. Возвращает True, если был перенос
        ShRInt = (i And 1) <> 0
        i = i And &HFFFE        ' Очищаем 0-й бит
        i = i \ 2               ' Сдвиг вправо
        i = i And &H7FFF        ' Очищаем 15-й бит
    End Function

    Function ShLLong(ByRef l As Long) As Boolean
        ' Сдвигает биты в длинном целом на 1 влево. Возвращает True, если был перенос
        Dim k As Long
        ShLLong = (l And &H80000000) <> 0
        k = l And &H40000000    ' Запоминаем 30-й бит в переменной k
        l = l And &H3FFF0000    ' Очищаем оба бита 14 и 15
        l = l * 2               ' Выполняем сдвиг влево
        If k <> 0 Then l = l Or &H80000000 ' Записываем старый 30-й бит на место 31-го
    End Function

    Function ShRLong(ByRef l As Long) As Boolean
        ' Сдвигает биты в длинном целом на 1 влево. Возвращает True, если был перенос
        ShRLong = (l And 1) <> 0
        l = l And &HFFFFFFFE    ' Очищаем 0-й бит
        l = l \ 2               ' Сдвиг вправо
        l = l And &H7FFFFFFF    ' Очищаем 15-й бит
    End Function


    ' ********************************
    ' ***** ОБМЕН БАЙТОВ И ЦЕЛЫХ *****
    ' ********************************
    Function I2B_Lo(ByVal i As Integer) As Byte
        ' XXYY -> YY. Возвращает правый (младший) байт одинарного целого
        I2B_Lo = i And &HFF
    End Function

    Function I2B_Hi(ByVal i As Integer) As Byte
        ' XXYY -> XX. Возвращает левый (старший) байт одинарного целого
        i = i And &HFF00
        i = i \ 256
        I2B_Hi = i And &HFF
    End Function

    Function B2I_Lo(ByVal b As Byte) As Integer
        ' XX -> 00XX. Возвращает одинарное целое с заданным левым байтом и нулевым правым
        B2I_Lo = b
    End Function

    Function B2I_Hi(ByVal b As Byte) As Integer
        ' XX -> XX00. Возвращает одинарное целое с заданным правым байтом и нулевым левым
        Dim i As Integer
        i = b And &H7F
        i = i * 256
        If (b And &H80) <> 0 Then i = i Or &H8000
        B2I_Hi = i
    End Function

    ' ***** ОБМЕН ДЛИННЫХ И ЦЕЛЫХ *****
    Function L2I_Lo(ByVal l As Long) As Integer
        ' XXXXYYYY -> YYYY. Возвращает младшую часть длинного целого
        Dim i As Integer
        i = l And (Not &HFFFF8000)      ' Сначала -- без 31-го бита
        If (l And (Not &HFFFF7FFF)) <> 0 Then i = i Or &H8000
        L2I_Lo = i
    End Function

    Function L2I_Hi(ByVal l As Long) As Integer
        ' XXXXYYYY -> XXXX. Возвращает старшую часть длинного целого
        l = l And &HFFFF0000
        l = l \ &H10000
        L2I_Hi = L2I_Lo(l)
    End Function

    Function I2L_Lo(ByVal i As Integer) As Long
        ' XXXX -> 0000XXXX. Возвращает двойное целое с заданной младшей частью
        I2L_Lo = (i And &HFFFF) And (Not &HFFFF0000)
    End Function

    Function I2L_Hi(ByVal i As Integer) As Long
        ' XXXX -> XXXX0000. Возвращает двойное целое с заданной старшей частью
        Dim l As Long
        l = i And &H7FFF
        l = i And &H7FFF
        l = l * &H10000
        If (i And &H8000) <> 0 Then l = l Or &H80000000
        I2L_Hi = l
    End Function


    ' ***************************
    ' ***** БАЙТОВЫЕ СДВИГИ *****
    ' ***************************
    Function ChLInt(ByRef i As Integer) As Byte
        ' XXYY -> YY00. Выполняет сдвиг одинарного целого на байт влево.
        ' XX. Возвращает левый (старший) байт. Младший заполняет нулями.
        Dim j As Integer
        ChLInt = I2B_Hi(i)
        j = (i And &H80) <> 0
        i = i And &H7F
        i = i * 256
        If j Then i = i Or &H8000
    End Function

    Function ChRInt(ByRef i As Integer) As Byte
        ' XXYY -> 00XX. Выполняет сдвиг одинарного целого на байт вправо.
        ' YY. Возвращает правый (младший) байт. Старший заполняет нулями.
        ChRInt = I2B_Lo(i)
        i = i And &HFF00
        i = i \ 256
        i = i And &HFF
    End Function

    Function ChLLng(ByRef l As Long) As Byte
        ' XXYYYYYY -> YYYYYY00. Выполняет сдвиг двойного целого на байт влево.
        ' XX. Возвращает левый (старший) байт. Младший заполняет нулями.
        Dim i As Integer
        ChLLng = I2B_Hi(L2I_Hi(l))
        i = (l And &H800000) <> 0
        l = l And &H7FFFFF
        l = l * 256
        If i Then l = l Or &H80000000
    End Function

    Function ChRLng(ByRef l As Long) As Byte
        ' XXXXXXYY -> 00XXXXXX. Выполняет сдвиг двойного целого на байт вправо.
        ' YY. Возвращает правый (младший) байт. Старший заполняет нулями.
        Dim b As Byte
        ChRLng = l And &HFF
        l = l And &HFFFFFF00
        l = l \ 256
        l = l And &HFFFFFF
    End Function

End Module
