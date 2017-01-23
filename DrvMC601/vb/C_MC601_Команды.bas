Attribute VB_Name = "C_MC601_Команды"
Option Explicit

Const InitPar = "1200,N,8,1"    ' Строка параметров обмена порта

Global Const MC601_AdrMain = &H3F       ' Адрес основного модуля
Global Const MC601_AdrHour = &H7F       ' Адрес дополнительного модуля (для часовых архивов)

Global MC601Команда(0 To 15) As Byte    ' Команда на счётчик

Global Const MaxRsp = 1023
Global MC601Ответ(0 To MaxRsp) As Byte  ' Буфер ответа счётчика
Global MC601Принято As Integer          ' Длина ответа



' ************************
' *** РАБОТА С КАНАЛОМ ***
' ************************
Public Function MC601ОткрытьПорт(ByVal Номер As Integer) As Integer
    ChannalMode = CM_Cable
    MC601ОткрытьПорт = OpenPort(Номер, InitPar)
End Function

Public Function MC601_НайтиСчётчик(Номер As Integer) As Integer
' Ищет порт. Аргумент задаёт предпочтительный номер
' Возвращает номер порта, который ответил, 0 -- в случае неудачи
' В случае порт будет открыт
Dim i As Integer, k As Integer
    MC601_НайтиСчётчик = 0
  ' Сначала пробуем переданный номер порта
    If Номер <> 0 Then
        i = Номер
        If MC601ОткрытьПорт(i) = 0 Then
            If MC601_СчитатьНомер <> 0 Then
                MC601_НайтиСчётчик = i
                Exit Function
              End If
            ClosePort
          End If
      End If
    ' Атака не удалась, ищем перебором
    For i = 1 To 8
    If i <> Номер Then
        If MC601ОткрытьПорт(i) = 0 Then
            If MC601_СчитатьНомер <> 0 Then
                MC601_НайтиСчётчик = i
                Exit Function
              End If
            ClosePort
          End If
      End If
      Next i
End Function


' ************************
' *** ПРОСТЫЕ ОПЕРАЦИИ ***
' ************************
Public Function MC601_СчитатьТип()
' В случае неудачи возвращает Null
    If Not MasterLoaded Then
        MC601_СчитатьТип = "Тест"
        Exit Function
      End If
    MC601Команда(0) = &H3F
    MC601Команда(1) = &H1
        If MC601ВыполнитьКоманду(MC601Команда, 2) <> 0 Then Exit Function
    MC601_СчитатьТип = Bytes2I(MC601Ответ, 2, 2)
End Function

Public Function MC601_СчитатьНомер() As Variant
' В случае неудачи возвращает 0. Номер, в принципе, может быть буквенно-цифровым
    MC601_СчитатьНомер = Null
    If Not MasterLoaded Then Exit Function
'    If Not MasterLoaded Then
'        MC601_СчитатьНомер = "Тест"
'        Exit Function
'      End If
    MC601Команда(0) = &H3F
    MC601Команда(1) = &H2
        If MC601ВыполнитьКоманду(MC601Команда, 2) <> 0 Then Exit Function
    MC601_СчитатьНомер = Bytes2L(MC601Ответ, 4, 2)
End Function

Public Function MC601_СчитатьТекущий(Регистр As Integer)
    If Not MasterLoaded Then
        MC601_СчитатьТекущий = 0
        Exit Function
      End If
    MC601Команда(0) = &H3F
    MC601Команда(1) = &H10
    MC601Команда(2) = &H1
    I2Bytes Регистр, MC601Команда, 3
        If MC601ВыполнитьКоманду(MC601Команда, 5) <> 0 Then Exit Function  ' Ошибка
        If MC601Принято <= 7 Then Exit Function                     ' Пустой ответ
    '   MC601Ответ(4)                           ' Единица измерения
    MC601_СчитатьТекущий = Byte2Mantiss(MC601Ответ, MC601Ответ(5), 7) _
                        * Byte2Factor(MC601Ответ(6))
End Function


' *********************
' *** ЧТЕНИЕ ДАННЫХ ***
' *********************

Public Function MC601_ПустойСуточный(Регистр As Integer, _
                ЧитатьОт As Integer, НеМенее As Integer, ByRef Считано As Integer) As Variant
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    For i = 1 To НеМенее
        a(i) = 0
      Next i
    Считано = НеМенее
    MC601_ПустойСуточный = a
End Function

Public Function MC601_ПустойЧасовой(Регистр As Integer, Дата As Date) As Variant
' Читает из регистр из часового архива за указанную дату. Возвращает массив из 24 показаний.
Dim a(1 To 24)
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
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


Public Function MC601_СчитатьЧасовой(ByVal Регистр As Integer, ByVal Дата As Date) As Variant
Dim a(1 To 24) As Double
Dim i As Integer, j As Integer, k As Integer
Dim l As Integer, m As Integer, n As Integer
Dim iErr As Integer, iPos As Integer
Dim f As Double, b As Double
Dim DateMark As Date
Dim iHrs As Integer

    If Not MasterLoaded Or (Регистр = 1003) Or (Регистр = 1002) Then
        MC601_СчитатьЧасовой = MC601_ПустойЧасовой(Регистр, Дата)
        Exit Function
      End If
     
    For j = 1 To 24
        a(j) = 0
      Next j
     
    Дата = Дата + 1
    MC601Команда(0) = &H7F
    MC601Команда(1) = &H63
    I2Bytes Регистр, MC601Команда, 2
    MC601Команда(4) = Year(Дата) - 2000
    MC601Команда(5) = Month(Дата)
    MC601Команда(6) = Day(Дата)
    MC601Команда(7) = 0
    iErr = MC601ВыполнитьКоманду(MC601Команда, 8)
        If iErr <> 0 Then Exit Function
    '   MC601Ответ(4)               ' Единица измерения
    l = MC601Ответ(5)               ' Размер мантиссы
    f = Byte2Factor(MC601Ответ(6))  ' Фактор
    k = (MC601Принято - 8) \ l      ' Количество отсчётов полученное (24)
    If k = 0 Then
        iErr = 99
        MC601_СчитатьЧасовой = a
        Exit Function
      End If
    iPos = 8
    If k > 24 Then k = 24
    For j = 1 To k
        a(j) = Byte2Mantiss(MC601Ответ, l, iPos) * f
        iPos = iPos + l
      Next j
    MC601_СчитатьЧасовой = a
End Function


Public Function MC601_СчитатьСуточный(Регистр As Integer, _
                ЧитатьОт As Integer, НеМенее As Integer, ByRef n As Integer) As Variant
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    If Not MasterLoaded Then
        MC601_СчитатьСуточный = MC601_ПустойСуточный(Регистр, ЧитатьОт, НеМенее, n)
        Exit Function
      End If
      
    MC601Команда(0) = &H3F
    MC601Команда(1) = &H66
    I2Bytes Регистр, MC601Команда, 2
    i = ЧитатьОт                      ' Рамка считывания
    n = 0
    Do While n < НеМенее
        I2Bytes i, MC601Команда, 4
        ' Послать запрос и принять данные
        iErr = MC601ВыполнитьКоманду(MC601Команда, 6)
        If iErr <> 0 Then GoTo 1
        '   Ответ(4)                    ' Единица измерения
        l = MC601Ответ(5)               ' Размер мантиссы
        f = Byte2Factor(MC601Ответ(6))  ' Фактор
        k = (MC601Принято - 7) \ l      ' Количество отсчётов полученное
        If k = 0 Then GoTo 1
        iPos = 7
        For j = 1 To k
            n = n + 1
            a(n) = Byte2Mantiss(MC601Ответ, l, iPos) * f
            iPos = iPos + l
          Next j
        i = i + k
      Loop
1:
    MC601_СчитатьСуточный = a
End Function

Public Function MC601_СчитатьМесячный(Регистр As Integer, _
                ЧитатьОт As Integer, НеМенее As Integer, ByRef n As Integer) As Variant
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer

    If Not MasterLoaded Then
        MC601_СчитатьМесячный = MC601_ПустойСуточный(Регистр, ЧитатьОт, НеМенее, n)
        Exit Function
      End If
      
    MC601Команда(0) = &H3F
    MC601Команда(1) = &H65
    I2Bytes Регистр, MC601Команда, 2
    i = ЧитатьОт                      ' Рамка считывания
    n = 0
    Do While n < НеМенее
        I2Bytes i, MC601Команда, 4
        ' Послать запрос и принять данные
        iErr = MC601ВыполнитьКоманду(MC601Команда, 6)
        If iErr <> 0 Then GoTo 1
        '   Ответ(4)                    ' Единица измерения
        l = MC601Ответ(5)               ' Размер мантиссы
        f = Byte2Factor(MC601Ответ(6))  ' Фактор
        k = (MC601Принято - 7) \ l      ' Количество отсчётов полученное
        If k = 0 Then GoTo 1
        iPos = 7
        For j = 1 To k
            n = n + 1
            a(n) = Byte2Mantiss(MC601Ответ, l, iPos) * f
            iPos = iPos + l
          Next j
        i = i + k
      Loop
1:
    MC601_СчитатьМесячный = a
End Function


