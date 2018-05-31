
Imports System
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading



Public Structure HArch
    Public day As Byte
    Public month As Byte
    Public year As Byte
    Public hour As Byte
    Public Q1 As Single
    Public Q2 As Single
    Public V1 As Single
    Public V2 As Single
    Public V3 As Single
    Public V4 As Single
    Public T1 As UInteger
    Public T2 As UInteger
    Public T3 As UInteger
    Public T4 As UInteger
    Public Status As UInteger
    Public CrashTime As UInteger
    Public P1 As Single
    Public P2 As Single
    Public P3 As Single
    Public P4 As Single
    Public OKTime As UInteger
    Public OK As Boolean
    Public V1h As Single
    Public V2h As Single
    Public V3h As Single
    Public V4h As Single
    Public Q1H As Single
    Public Q2H As Single

End Structure


Public Structure DArch
    Public day As Byte
    Public month As Byte
    Public year As Byte
    Public hour As Byte
    Public Q1 As Single
    Public Q2 As Single
    Public V1 As Single
    Public V2 As Single
    Public V3 As Single
    Public V4 As Single
    Public T1 As UInteger
    Public T2 As UInteger
    Public T3 As UInteger
    Public T4 As UInteger
    Public CrashTime As UInteger
    Public P1 As Single
    Public P2 As Single
    Public P3 As Single
    Public P4 As Single
    Public OKTime As UInteger
    Public OK As Boolean
    Public V1h As Single
    Public V2h As Single
    Public V3h As Single
    Public V4h As Single
    Public Q1H As Single
    Public Q2H As Single
End Structure



Public Class driver
    Inherits STKTVMain.TVDriver


#Region "Registers"
    Const MCR_DATE As Integer = 1003
    Const MCR_HR As Integer = 1004
    Const MCR_CLOCK As Integer = 1002
    Const MCR_SERIE_NO As Integer = 1001
    Const MCR_METER_NO_1 As Integer = 1010
    Const MCR_E1 As Integer = 60
    Const MCR_E2 As Integer = 94
    Const MCR_E3 As Integer = 63
    Const MCR_E4 As Integer = 61
    Const MCR_E5 As Integer = 62
    Const MCR_E6 As Integer = 95
    Const MCR_E7 As Integer = 96
    Const MCR_E8 As Integer = 97
    Const MCR_E9 As Integer = 110
    Const MCR_TA2 As Integer = 64
    Const MCR_V1 As Integer = 68
    Const MCR_V2 As Integer = 69
    Const MCR_VA As Integer = 84
    Const MCR_VB As Integer = 85
    Const MCR_M1 As Integer = 72
    Const MCR_M2 As Integer = 73
    Const MCR_INFOEVENT As Integer = 113
    Const MCR_INFO As Integer = 99
    Const MCR_T1 As Integer = 86
    Const MCR_T2 As Integer = 87
    Const MCR_T3 As Integer = 88
    Const MCR_T4 As Integer = 122
    Const MCR_T1_sub_T2 As Integer = 89
    Const MCR_P1 As Integer = 91
    Const MCR_P2 As Integer = 92
    Const MCR_FLOW1 As Integer = 74
    Const MCR_FLOW2 As Integer = 75
    Const MCR_EFFEKT1 As Integer = 80
    Const MCR_MAX_FLOW1DATE_AR As Integer = 123
    Const MCR_MAX_FLOW1_AR As Integer = 124
    Const MCR_MIN_FLOW1DATE_AR As Integer = 125
    Const MCR_MIN_FLOW1_AR As Integer = 126
    Const MCR_MAX_EFFEKT1DATE_AR As Integer = 127
    Const MCR_MAX_EFFEKT1_AR As Integer = 128
    Const MCR_MIN_EFFEKT1DATE_AR As Integer = 129
    Const MCR_MIN_EFFEKT1_AR As Integer = 130
    Const MCR_MAX_FLOW1DATE_MANED As Integer = 138
    Const MCR_MAX_FLOW1_MANED As Integer = 139
    Const MCR_MIN_FLOW1DATE_MANED As Integer = 140
    Const MCR_MIN_FLOW1_MANED As Integer = 141
    Const MCR_MAX_EFFEKT1DATE_MANED As Integer = 142
    Const MCR_MAX_EFFEKT1_MANED As Integer = 143
    Const MCR_MIN_EFFEKT1DATE_MANED As Integer = 144
    Const MCR_MIN_EFFEKT1_MANED As Integer = 145
    Const MCR_AVR_T1_AR As Integer = 146
    Const MCR_AVR_T2_AR As Integer = 147
    Const MCR_AVR_T1_MANED As Integer = 149
    Const MCR_AVR_T2_MANED As Integer = 150
    Const MCR_TL2 As Integer = 66
    Const MCR_TL3 As Integer = 67
    Const MCR_XDAY As Integer = 98

#End Region


    Public Const c_lng256 As Long = 256&

    Private mIsConnected As Boolean


    Private isTCP As Boolean
    Private SleepTime As Long



    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

    Dim tv As Short



    Dim IsBytesToRead As Boolean = False
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False

    Dim m_isArchToDBWrite As Boolean = False

#Region "MC601"






    Public Const MC601_AdrMain = &H3F       ' Адрес основного модуля
    Public Const MC601_AdrHour = &H7F       ' Адрес дополнительного модуля (для часовых архивов)

    Public MC601Command(0 To 15) As Byte    ' Команда на счётчик

    Public Const MaxRsp = 1023
    Public MC601Resp(0 To MaxRsp) As Byte  ' Буфер ответа счётчика
    Public MC601Rsvd As Integer          ' Длина ответа







    ' ************************
    ' *** ПРОСТЫЕ ОПЕРАЦИИ ***
    ' ************************
    Private Function MC601_СчитатьТип()
        ' В случае неудачи возвращает 0
        Debug.Print("MC601_СчитатьТип: ")
        MC601Command(0) = &H3F
        MC601Command(1) = &H1
        If MC601RunCommand(MC601Command, 2) <> 0 Then Return 0
        Return Bytes2I(MC601Resp, 2, 2)
    End Function

    Private Function MC601_СчитатьНомер() As Long
        Debug.Print("MC601_СчитатьНомер: ")
        ' В случае неудачи возвращает 0. Номер, в принципе, может быть буквенно-цифровым
        MC601Command(0) = &H3F
        MC601Command(1) = &H2
        If MC601RunCommand(MC601Command, 2) <> 0 Then Return 0
        Return Bytes2L(MC601Resp, 4, 2)
    End Function

    Private Function MC601_СчитатьТекущий(TheRegister As Integer) As Double

        MC601Command(0) = &H3F
        MC601Command(1) = &H10
        MC601Command(2) = &H1
        I2Bytes(TheRegister, MC601Command, 3)
        If MC601RunCommand(MC601Command, 5) <> 0 Then Return 0
        If MC601Rsvd <= 7 Then Return 0 ' Пустой ответ
        '   MC601Resp(4)                           ' Единица измерения
        Return Byte2Mantiss(MC601Resp, MC601Resp(5), 7) * Byte2Factor(MC601Resp(6))
    End Function


    ' *********************
    ' *** ЧТЕНИЕ ДАННЫХ ***
    ' *********************


    Private HourModule As Integer = -1

    Public Function MC601_ПроверитьЧасовойМодуль() As Integer
        Dim НомерВМ As Double
        HourModule = 1
        MC601_ПроверитьЧасовойМодуль = 1


        НомерВМ = MC601_СчитатьТекущий(157) ' Считываем номер верхнего модуля

        HourModule = 0
        MC601Command(1) = &H63
        I2Bytes(186, MC601Command, 2)
        MC601Command(4) = &HFF
        MC601Command(5) = &HFF
        MC601Command(6) = &HFF
        MC601Command(7) = &HFF

        If НомерВМ <> 0 Then        ' При наличии верхнего модуля начинаем с него
            MC601Command(0) = &H7F
            If MC601RunCommand(MC601Command, 8) = 0 Then
                HourModule = 1
                Exit Function
            End If
            MC601Command(0) = &H3F
            If MC601RunCommand(MC601Command, 8) = 0 Then
                HourModule = 2
            End If
        Else                       ' А при отсутствии -- с базового
            MC601Command(0) = &H3F
            If MC601RunCommand(MC601Command, 8) = 0 Then
                HourModule = 2
                Exit Function
            End If
            MC601Command(0) = &H7F
            If MC601RunCommand(MC601Command, 8) = 0 Then
                HourModule = 1
            End If
        End If
    End Function

    Private Function MC601_DayEmpty(TheRegister As Integer,
                    ЧитатьОт As Integer, НеМенее As Integer, ByRef Считано As Integer) As Double()
        Dim a(64) As Double
        Dim i As Integer
        For i = 1 To НеМенее
            a(i) = 0
        Next i
        Считано = НеМенее
        MC601_DayEmpty = a
    End Function

    Private Function MC601_HourEmpty(TheRegister As Integer, TheDate As Date) As Object
        ' Читает из TheRegister из часового архива за указанную дату. Возвращает массив из 24 показаний.
        Dim a(24)
        Dim i As Integer

        For i = 1 To 24
            If TheRegister = 1003 Then
                a(i) = TheDate
            ElseIf TheRegister = 1002 Then
                a(i) = 25 - i
            Else
                a(i) = 0
            End If
        Next i
        MC601_HourEmpty = a
    End Function


    Private Function MC601_СчитатьЧасовой(ByVal TheRegister As Integer, ByVal TheDate As Date) As Double()
        Dim a(24) As Double
        Dim j As Integer, k As Integer
        Dim l As Integer
        Dim iErr As Integer, iPos As Integer
        Dim f As Double

        For j = 1 To 24
            a(j) = 0
        Next j

        If (TheRegister = 1003) Or (TheRegister = 1002) Then
            MC601_СчитатьЧасовой = a ' MC601_HourEmpty(TheRegister, TheDate)
            Exit Function
        End If

        For j = 1 To 24
            a(j) = 0
        Next j

        'TheDate = TheDate.AddDays(1)

        If HourModule = 1 Then
            MC601Command(0) = &H7F      ' Верхний модуль (KM601)
        ElseIf HourModule = 2 Then
            MC601Command(0) = &H3F      ' Основной модуль (KM602)
        Else
            Exit Function
        End If
        MC601Command(1) = &H63
        I2Bytes(TheRegister, MC601Command, 2)
        MC601Command(4) = Year(TheDate) - 2000
        MC601Command(5) = Month(TheDate)
        MC601Command(6) = Day(TheDate)
        MC601Command(7) = 0
        iErr = MC601RunCommand(MC601Command, 8)
        If iErr <> 0 Then Return Nothing
        '   MC601Resp(4)               ' Единица измерения
        l = MC601Resp(5)               ' Размер мантиссы
        f = Byte2Factor(MC601Resp(6))  ' Фактор
        k = (MC601Rsvd - 8) \ l      ' Количество отсчётов полученное (24)
        If k = 0 Then
            iErr = 99
            Return a
        End If
        iPos = 8
        If k > 24 Then k = 24
        For j = 1 To k
            a(j) = Byte2Mantiss(MC601Resp, l, iPos) * f
            iPos = iPos + l
        Next j
        Return a
    End Function


    Private Function MC601_СчитатьСуточный(TheRegister As Integer, ЧитатьОт As DateTime, НеМенее As Integer, ByRef n As Integer) As Double()
        Dim a(64) As Double
        Dim f As Double
        Dim i As Integer, j As Integer, ii As Integer
        Dim k As Integer, l As Integer
        Dim iPos As Integer, iErr As Integer


        MC601Command(0) = &H3F
        MC601Command(1) = &H66
        I2Bytes(TheRegister, MC601Command, 2)
        i = Math.Abs(DateDiff(DateInterval.Day, ЧитатьОт, Date.Today))
        ' Рамка считывания
        n = 0
        Do While n < НеМенее

            I2Bytes(i, MC601Command, 4)
            ' Послать запрос и принять данные
            iErr = MC601RunCommand(MC601Command, 6)
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
        Return a
    End Function

    '    Private Function MC601_СчитатьМесячный(TheRegister As Integer, ЧитатьОт As Integer, НеМенее As Integer, ByRef n As Integer) As Double()
    '        Dim a(64) As Double
    '        Dim f As Double
    '        Dim i As Integer, j As Integer
    '        Dim k As Integer, l As Integer
    '        Dim iPos As Integer, iErr As Integer

    '        MC601Command(0) = &H3F
    '        MC601Command(1) = &H65
    '        I2Bytes(TheRegister, MC601Command, 2)
    '        i = ЧитатьОт                      ' Рамка считывания
    '        n = 0
    '        Do While n < НеМенее
    '            I2Bytes(i, MC601Command, 4)
    '            ' Послать запрос и принять данные
    '            iErr = MC601RunCommand(MC601Command, 6)
    '            If iErr <> 0 Then GoTo 1
    '            '   Ответ(4)                    ' Единица измерения
    '            l = MC601Resp(5)               ' Размер мантиссы
    '            f = Byte2Factor(MC601Resp(6))  ' Фактор
    '            k = (MC601Rsvd - 7) \ l      ' Количество отсчётов полученное
    '            If k = 0 Then GoTo 1
    '            iPos = 7
    '            For j = 1 To k
    '                n = n + 1
    '                a(n) = Byte2Mantiss(MC601Resp, l, iPos) * f
    '                iPos = iPos + l
    '            Next j
    '            i = i + k
    '        Loop
    '1:
    '        Return a
    '    End Function



    Private Function MC601RunCommand(Cmd() As Byte, ByVal n As Integer) As Integer
        ' Готовит из команды запрос, посылает его, ждёт ответа
        ' Готовим запрос
        ReDim Rqs(127)                 ' Готовим массив запроса
        nRqs = MC601Cmd2Rqs(Cmd, n, Rqs)    ' Формируем в нём запрос
        ReDim Preserve Rqs(0 To nRqs - 1)   ' Отсекаем хвосты
        MC601НачатьПриём()        ' Активируем автомат приёма
        SendDataToPort(Rqs, nRqs)      ' Посылаем запрос
        ' Посылаем и ждём ответа
        WaitForData()
        Dim lcnt As Integer
        lcnt = 100
        While lcnt > 0                      ' Ждём завершения приёма
            Dim buffer(1) As Byte
            While MyTransport.BytesToRead > 0
                If MyTransport.Read(buffer, 0, 1) > 0 Then
                    MC601ОбработатьБайт(buffer(0))
                    If MC601ПриёмОкончен() Then Exit While
                End If
            End While

            If MC601ПриёмОкончен() Then Exit While
            lcnt -= 1
            If ПриёмПрерван() Then Exit While
            System.Threading.Thread.Sleep(20)
        End While
        ' Завершаем получение

        If ReceiverState = 0 Then
            If MC601Rsvd = 0 Then
                ReceiverState = RS_BadData
                MC601ПрерватьПриём()
                Return ReceiverState
            End If
            MC601ПроверитьCRC()       ' Проверям и удаляем CRC
            If ОшибкаПротокола <> 0 Then
                ReceiverState = RS_BadData
                MC601ПрерватьПриём()
            End If
        End If
        Return ReceiverState
    End Function



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

    'Private Function MC601ОшибкаПриёма() As Integer
    '    ' Возвращает код ошибки приёма данных
    '    MC601ОшибкаПриёма = ОшибкаПротокола
    'End Function


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
                'MsgBox("Ошибка в группе " & CStr(i), vbOKOnly, "Внимание!")
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




    ' Причины нештатного прерывания цикла приёма данных
    ' Более подробно ошибки линии или протокола фиксируются отдельно
    Public ReceiverState As Integer ' Причина прекращения приёма
    Public Const RS_OK = 0      ' Всё ОК (Ошибок нет, нормально принято)
    Public Const RS_TimeOut = 1 ' Нет ответа (таймаут)
    Public Const RS_LostCon = 2 ' Разрыв соединения
    Public Const RS_LineErr = 3 ' Ошибки на линии связи
    Public Const RS_BadData = 4 ' Неправильный ответ
    Public Const RS_UserEsc = 5 ' Прервано пользователем




    Public Rqs() As Byte        ' Данные для отправки через порт
    Public nRqs As Integer      ' Число байтов запроса





    Private Function ПриёмПрерван() As Integer
        ПриёмПрерван = ReceiverState
    End Function


    '   ***************************
    '   ***** Работа с портом *****
    '   ***************************

    ' *** Посылка подготовленного сообщения
    Private Sub SendDataToPort(Q() As Byte, Cnt As Integer)
        ' Посылает данные на порт
        ReceiverState = 0
        MyTransport.Write(Q, 0, Cnt)
    End Sub

    ' *********************************
    ' **** ДЕКОДИРОВАНИЕ TheRegisterОВ ****
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
        Debug.Print("=" + k.ToString)
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

    Sub I3Bytes(ByVal k As Integer, b() As Byte, ByVal iPos As Integer)
        ' XXYYZZ -> XX, YY, ZZ
        ' Преобразует одинарное целое в массив из 2 байтов. Первый байт -- старший.
        Dim i As Integer
        For i = 0 To 2
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
        Debug.Print("=" + l.ToString)
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
        'Dim b As Byte
        ChRLng = l And &HFF
        l = l And &HFFFFFF00
        l = l \ 256
        l = l And &HFFFFFF
    End Function

#End Region


    Public Overrides Property isArchToDBWrite() As Boolean

        Get
            Return m_isArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isArchToDBWrite = value
        End Set
    End Property
    Dim m_isMArchToDBWrite As Boolean = False



    'Public inputbuffer(69) As Byte

    Public Overrides Function CounterName() As String
        Return "MC601"
    End Function







    Public Function TryConnect() As Boolean

        MyTransport.CleanPort()
        EraseInputQueue()
        If MC601_СчитатьНомер() <> 0 Then
            If MC601_СчитатьТип() <> 0 Then
                Return True
            End If
        End If
        DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
        Return False

    End Function

    Public Overrides Sub Connect()

        mIsConnected = False
        Dim cnt As Integer = 5
        While cnt > 0
            mIsConnected = TryConnect()
            If mIsConnected Then

                'Dim i As Integer
                'For i = 1 To 3
                '    Dim k As Double
                '    k = ReadFloat(49189UL - 1 + (i - 1) * 2)
                '    If k > 0 Then k = -k
                '    PT(i) = 1.0 + k
                '    If PT(i) <= 0 Then PT(i) = 1.0
                '    If IsOld Then
                '        MV(i) = ReadChar(60 - 1 + i - 1)
                '    Else
                '        MV(i) = ReadChar(84 - 1 + i - 1)
                '    End If

                'Next
                Return
            End If

            cnt = cnt - 1
        End While




    End Sub




    Private Function EncodeError(ByVal e As Byte) As String
        Select Case e

            Case 1
                Return "ILLEGAL FUNCTION"
            Case 2
                Return "ILLEGAL DATA ADDRESS"
            Case 3
                Return "ILLEGAL DATA VALUE"
            Case 4
                Return "FAILURE IN ASSOCIATED DEVICE"
            Case 5
                Return "ACKNOWLEDGE"
            Case 6
                Return "BUSY, REJECTED MESSAGE"
            Case 7
                Return "NAK-NEGATIVE ACKNOWLEDGMENT"
            Case Else
                Return "UNKNOWN ERROR"
        End Select

    End Function



    Private m_readRAMByteCount As Short





    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
   ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim retsum As String
        Dim ok As Boolean = False
        Dim buf(1000) As Byte
        clearArchive(Arch)




        Try
            Dim yymmdd As DateTime

            Dim rr As Integer
            Dim a() As Double
            Arch.archType = ArchType
            If ArchType = archType_hour Then

                If HourModule = -1 Then
                    MC601_ПроверитьЧасовойМодуль()
                End If
                If HourModule = 0 Then
                    isArchToDBWrite = False
                    Return "Ошибка: Нет часового модуля"
                End If


                Arch.DateArch = DateSerial(ArchYear, ArchMonth, ArchDay)
                yymmdd = Arch.DateArch ' Date2YYMMDD(Arch.DateArch)
                'Arch.DateArch = DateSerial(ArchYear, ArchMonth, ArchDay)

                ok = True

                a = MC601_СчитатьЧасовой(MCR_V1, Arch.DateArch)
                If Not a Is Nothing Then Arch.V1 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_V2, Arch.DateArch)
                If Not a Is Nothing Then Arch.V2 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_M1, Arch.DateArch)
                If Not a Is Nothing Then Arch.M1 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_M2, Arch.DateArch)
                If Not a Is Nothing Then Arch.M2 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_E1, Arch.DateArch)
                If Not a Is Nothing Then Arch.Q1 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_E2, Arch.DateArch)
                If Not a Is Nothing Then Arch.Q2 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_E3, Arch.DateArch)
                If Not a Is Nothing Then Arch.Q3 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_E4, Arch.DateArch)
                If Not a Is Nothing Then Arch.Q4 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_E5, Arch.DateArch)
                If Not a Is Nothing Then Arch.Q5 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_E6, Arch.DateArch)
                If Not a Is Nothing Then Arch.Q6 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_T1, Arch.DateArch)
                If Not a Is Nothing Then Arch.T1 = a(ArchHour + 1) Else ok = False

                a = MC601_СчитатьЧасовой(MCR_T2, Arch.DateArch)
                If Not a Is Nothing Then Arch.T2 = a(ArchHour + 1) Else ok = False

                Arch.DateArch = Arch.DateArch.AddHours(ArchHour)
                If ok Then
                    isArchToDBWrite = True
                End If



            End If

            If ArchType = archType_day Then


                ok = True
                Arch.DateArch = DateSerial(ArchYear, ArchMonth, ArchDay)
                yymmdd = Arch.DateArch 'Date2YYMMDD(Arch.DateArch)
                a = MC601_СчитатьСуточный(MCR_V1, yymmdd, 1, rr)
                If rr > 0 Then Arch.V1 = a(1)
                a = MC601_СчитатьСуточный(MCR_V2, yymmdd, 1, rr)
                If rr > 0 Then Arch.V2 = a(1)
                a = MC601_СчитатьСуточный(MCR_M1, yymmdd, 1, rr)
                If rr > 0 Then Arch.M1 = a(1)
                a = MC601_СчитатьСуточный(MCR_M2, yymmdd, 1, rr)
                If rr > 0 Then Arch.M2 = a(1)
                a = MC601_СчитатьСуточный(MCR_E1, yymmdd, 1, rr)
                If rr > 0 Then Arch.Q1 = a(1)
                a = MC601_СчитатьСуточный(MCR_E2, yymmdd, 1, rr)
                If rr > 0 Then Arch.Q2 = a(1)
                a = MC601_СчитатьСуточный(MCR_E3, yymmdd, 1, rr)
                If rr > 0 Then Arch.Q3 = a(1)
                a = MC601_СчитатьСуточный(MCR_E4, yymmdd, 1, rr)
                If rr > 0 Then Arch.Q4 = a(1)
                a = MC601_СчитатьСуточный(MCR_E5, yymmdd, 1, rr)
                If rr > 0 Then Arch.Q5 = a(1)
                a = MC601_СчитатьСуточный(MCR_E6, yymmdd, 1, rr)
                If rr > 0 Then Arch.Q6 = a(1)
                a = MC601_СчитатьСуточный(MCR_T1, yymmdd, 1, rr)
                If rr > 0 Then Arch.T1 = a(1)
                a = MC601_СчитатьСуточный(MCR_T2, yymmdd, 1, rr)
                If rr > 0 Then Arch.T2 = a(1)
                If ok Then
                    isArchToDBWrite = True
                End If


            End If

            If ok Then
                retsum = "Архив прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = True
            Else
                retsum = "Ошибка: не удалось получить архив"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = False
            End If

            Return retsum
        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function



    Public Overrides Function DeCodeHC(ByVal CodeHC As Long) As String
        Return Convert.ToString(CodeHC, 2)
    End Function



    Public Overrides Function WriteArchToDB() As String

        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,hc_code,hc,errtime, errtimeh,worktime,oktime,hcraw) values ("
        WriteArchToDB = WriteArchToDB + "'" + DeviceID.ToString() + "',"
        WriteArchToDB = WriteArchToDB + "'" + Arch.archType.ToString() + "',"
        WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T1, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T2, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T3, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T4, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T5, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T6, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P1, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P2, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P3, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P4, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P5, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P6, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V3, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v4, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v5, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v6, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M1, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M2, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M3, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M4, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M5, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M6, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q3, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q4, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q5, "##############0.000000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q6, "##############0.000000").Replace(",", ".") + ","
        If DeCodeHCNumber(Arch.HC) = "0000000000000000" Then
            WriteArchToDB = WriteArchToDB + "'','Нет НС'"
        Else
            WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCNumber(Arch.HC)) + "','" + Arch.MsgHC + "'"
        End If
        WriteArchToDB = WriteArchToDB + "," + Format(Arch.ERRTIME1, "##############0").Replace(",", ".")

        WriteArchToDB = WriteArchToDB + "," + Format(Arch.ErrtimeH, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + NanFormat(Arch.OKTIME1, "##############0.000000").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + NanFormat(Arch.OKTIME1, "##############0.000000").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + "'" + Arch.HC.ToString().Replace(",", ".") + "'"
        WriteArchToDB = WriteArchToDB + ")"
        Debug.Print(WriteArchToDB)
    End Function


    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = ""
        Try
            WriteMArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,v1,v2,v3,v4,v5,v6,M1,M2,M3,M4,M5,M6,P1,P2,P3,P4,P5,P6,g1,G2,g3,g4,g5,g6,hc_code,hc,hcraw) values ("
            WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
            WriteMArchToDB = WriteMArchToDB + "1,"
            WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.V1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.V2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.V3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.V4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.V5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.V6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.M1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.M2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.M3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.M4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.M5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.M6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p6, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G1, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G2, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G3, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G4, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G5, "##############0.000000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G6, "##############0.000000").Replace(",", ".") + ","



            If DeCodeHCNumber(mArch.HC) = "0000000000000000" Then
                WriteMArchToDB = WriteMArchToDB + "'-','Нет НС',"

            Else
                WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HC) + "','" + S180(mArch.MsgHC) + "',"



            End If
            WriteMArchToDB = WriteMArchToDB + "'" + mArch.HC.ToString().Replace(",", ".") + "'"

            WriteMArchToDB = WriteMArchToDB + ")"
        Catch
        End Try
        'Return WriteMArchToDB
    End Function




    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If

        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()

    End Sub




    Private LastDate As DateTime = DateTime.MinValue

    Public Function GetDeviceDate() As Date

        'If LastDate <> DateTime.MinValue Then
        '    Return LastDate
        'End If

        Dim DateArch As Date
        DateArch = Date.Now()

        Try

            MC601Command(0) = &H3F
            MC601Command(1) = &H10
            MC601Command(2) = &H1
            I2Bytes(MCR_DATE, MC601Command, 3)
            If MC601RunCommand(MC601Command, 5) <> 0 Then
                DateArch = YYMMDD2Date(Bytes2L(MC601Resp, 4, 5))
                DateArch = DateTime.Today
            End If


            'If MC601Rsvd <= 7 Then Return 0 ' Пустой ответ
            ''   MC601Resp(4)                           ' Единица измерения
            'Return Byte2Mantiss(MC601Resp, MC601Resp(5), 7) * Byte2Factor(MC601Resp(6))
            '


            MC601Command(0) = &H3F
            MC601Command(1) = &H10
            MC601Command(2) = &H1
            I2Bytes(MCR_CLOCK, MC601Command, 3)
            If MC601RunCommand(MC601Command, 5) <> 0 Then

                DateArch = DateTime.Now
            Else
                DateArch = DateTime.Now
            End If


        Catch
        End Try
        Return DateArch
    End Function

    Public Overrides Function ReadMArch() As String


        clearMarchive(mArch)
        EraseInputQueue()

        mArch.DateArch = GetDeviceDate()


        mArch.t1 = MC601_СчитатьТекущий(MCR_T1)
        mArch.t2 = MC601_СчитатьТекущий(MCR_T2)
        mArch.t3 = MC601_СчитатьТекущий(MCR_T3)
        mArch.t4 = MC601_СчитатьТекущий(MCR_T4)
        mArch.p1 = MC601_СчитатьТекущий(MCR_P1)
        mArch.p2 = MC601_СчитатьТекущий(MCR_P2)
        mArch.V1 = MC601_СчитатьТекущий(MCR_V1)
        mArch.V2 = MC601_СчитатьТекущий(MCR_V2)
        mArch.M1 = MC601_СчитатьТекущий(MCR_M1)
        mArch.M2 = MC601_СчитатьТекущий(MCR_M2)


        isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function
    Dim m_isTArchToDBWrite As Boolean = False
    Public Overrides Property isTArchToDBWrite() As Boolean
        Get
            Return m_isTArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isTArchToDBWrite = value
        End Set
    End Property





    Public Overrides Function ReadTArch() As String


        Dim bArr(0 To 8) As Byte
        Dim temptv As Short
        temptv = tv
        clearTarchive(tArch)
        EraseInputQueue()




        tArch.DateArch = GetDeviceDate()






        tArch.Q1 = MC601_СчитатьТекущий(MCR_E1)
        tArch.Q2 = MC601_СчитатьТекущий(MCR_E2)
        tArch.Q3 = MC601_СчитатьТекущий(MCR_E3)
        tArch.Q4 = MC601_СчитатьТекущий(MCR_E4)
        tArch.Q5 = MC601_СчитатьТекущий(MCR_E5)
        tArch.Q6 = MC601_СчитатьТекущий(MCR_E6)
        tArch.V1 = MC601_СчитатьТекущий(MCR_V1)
        tArch.V2 = MC601_СчитатьТекущий(MCR_V2)
        tArch.M1 = MC601_СчитатьТекущий(MCR_M1)
        tArch.M2 = MC601_СчитатьТекущий(MCR_M2)



        isTArchToDBWrite = True

        Return "Итоговый архив прочитан"
    End Function



    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function


    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function





    Private Function TableForArch(ByVal ArchType As Short) As String
        Dim tName As String = ""
        If ArchType = 1 Then
            tName = "TPLC_M"
        End If

        If ArchType = 3 Then
            tName = "TPLC_H"
        End If
        If ArchType = 4 Then
            tName = "TPLC_D"
        End If
        If ArchType = 2 Then
            tName = "TPLC_T"
        End If
        Return tName
    End Function

    Public Overrides Function ReadSystemParameters() As DataTable
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")

        Dim dr As DataRow



        Dim buf(1000) As Byte
        EraseInputQueue()



        'dr = dt.NewRow
        'dr("Название") = "Масса \ Объем 2: "
        'dr("Значение") = IIf(MV(2) = 0, "МАССА", "ОБЪЕМ")
        'dt.Rows.Add(dr)
        'dr = dt.NewRow

        'dr("Название") = "Масса \ Объем 3: "
        'dr("Значение") = IIf(MV(3) = 0, "МАССА", "ОБЪЕМ")
        'dt.Rows.Add(dr)




        Return dt
    End Function

    Public Overrides Property isMArchToDBWrite() As Boolean
        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property

    Public Function NoData(ByVal X As Object) As Boolean
        ' Проверяет, содержит ли переменная данные (т.е. не Null и не пустую строку)
        ' Обычно требуется для проверки значения поля формы или ячейки таблицы
        If X Is Nothing Then
            NoData = True
        ElseIf X.ToString = "" Then
            NoData = True
        Else
            NoData = False
        End If
    End Function

    Public Function Date2YYMMDD(ByVal d As Date) As Long
        ' По дате вычисляет внутреннее представление
        Date2YYMMDD = 0
        'If NoData(d) Then Exit Function
        '
        Date2YYMMDD = ((Year(d) - 2000) * 256 + Month(d)) * 256 + Day(d)

    End Function

    Public Function YYMMDD2Date(ByVal YYMMDD As Object) As Date
        ' По дате вычисляет внутреннее представление
        Dim DD As Long, YM As Long
        YYMMDD2Date = Date.MinValue
        If NoData(YYMMDD) Then Exit Function
        YM = YYMMDD \ 100
        DD = YYMMDD Mod 100
        If DD <> 0 Then ' Просто даты
            YYMMDD2Date = DateSerial(2000 + YM \ 100, YM Mod 100, DD)
        ElseIf (YM Mod 100) <> 0 Then ' Месяцы и годы
            YYMMDD2Date = DateSerial(2000 + YM \ 100, YM Mod 100, 1)
        Else ' Годы
            YYMMDD2Date = DateSerial(2000 + YM \ 100, 1, 1)
        End If
    End Function

    '   *****************************
    '   *** ФОРМАТЫ HHMM и HHMMSS ***
    '   *****************************
    '   Десятично-кодированной представление времени
    '
    Public Function HHMM2Time(ByVal HHMM As Object) As Date
        HHMM2Time = Date.MinValue
        If NoData(HHMM) Then Exit Function
        HHMM2Time = TimeSerial(HHMM \ 100, HHMM Mod 100, 0)
    End Function

    Public Function Time2HHMM(ByVal T As Object) As Long
        Time2HHMM = 0
        If NoData(T) Then Exit Function
        Time2HHMM = Hour(T) * 100 + Minute(T)
    End Function

    Public Function HHMMSS2Time(ByVal HHMMSS As Object) As Date
        Dim HHMM As Long
        HHMMSS2Time = Date.MinValue
        If NoData(HHMMSS) Then Exit Function
        HHMM = HHMMSS \ 100
        HHMMSS2Time = TimeSerial(HHMM \ 100, HHMM Mod 100, HHMMSS Mod 100)
    End Function

    Public Function Time2HHMMSS(ByVal T As Object) As Long
        Time2HHMMSS = 0
        If NoData(T) Then Exit Function
        Time2HHMMSS = (Hour(T) * 100 + Minute(T)) * 100 + Second(T)
    End Function


End Class
