Attribute VB_Name = "D2_2__Операции"
Option Explicit

' Обеспечивает формирование периодов и границ отчётов в разных мастерах

'   *****************
'   ***   Д Н И   ***
'   *****************
'
Public Function ДеньПоСчётчику(ByVal ДеньХ, ByVal ТипПериода)
' Возвращает "значение по умолчанию" для дня "П" на основании дня "Х".
Dim m As Integer
    ДеньПоСчётчику = Null
    If NoData(ДеньХ) Or (ДеньХ = 0) Then ДеньХ = 101
    m = 0   ' Дополнительный сдвиг
    Select Case ТипПериода
      Case 1    ' Месяц
        If ДеньХ Mod 100 < 20 Then m = 100    ' Если в начале месяца, то следующий месяц
      Case 2    ' Год
        If ДеньХ \ 100 < 8 Then m = 1200      ' Если в начале года, то следующий год
      Case Else
        Exit Function
      End Select
    ДеньПоСчётчику = ДеньХ + m
End Function

'Public Function ДеньП_ОК(ByVal ДеньП, ByVal ДеньХ, ByVal ТипПериода)
'' Возвращает всегда правильный день "П" (если период указан)
'    If NoData(ДеньП) Or (ДеньП = 0) Then
'        ПлановыйДень = ДеньПоСчётчику(ДеньХ, ТипПериода)
'      Else
'        ПлановыйДень = ДеньП
'      End If
'End Function

Public Function ПлановаяДата(ByVal Период, ByVal ДеньП)
' Вычисляет плановую дату по отчётному периоду. "День" -- обычно ДеньП
    ПлановаяДата = Null
    If NoData(Период) Or NoData(ДеньП) Then Exit Function
    If Период = 0 Or ДеньП = 0 Then Exit Function
    ПлановаяДата = DateSerial(100 * (Year(Date) \ 100) + (Период \ 100), _
                        НачалоПериода(Период) + (ДеньП \ 100), (ДеньП Mod 100))
End Function
    
'Public Function ПлановаяДатаРасширенная(ByVal Период, ByVal ДеньП, ByVal ДеньХ, ByVal ТипПериода)
'    ПлановаяДатаРасширенная = Null
'    If NoData(ДеньП) Or (ДеньП = 0) Then ДеньП = ДеньПоСчётчику(ДеньХ, ТипПериода)
'    ПлановаяДатаРасширенная = ПлановаяДата(Период, ДеньП)
'End Function


'   *****************
'   ***  ПЕРИОДЫ  ***
'   *****************
'
Public Function СледующийПериод(ByVal Период As Variant) As Variant
' Вычисляет следующий период
Dim m As Integer, Y As Integer
    СледующийПериод = Null
        If NoData(Период) Then Exit Function
    m = Период Mod 100
    Y = Период \ 100
    If m = 0 Then   ' Год
        If Y < 320 Then Y = Y + 1
      Else          ' Месяц
        If m < 12 Then
            m = m + 1
          Else
            m = 1
            If Y < 320 Then Y = Y + 1
          End If
      End If
    СледующийПериод = 100 * Y + m
End Function

Public Function ПредыдущийПериод(ByVal Период As Variant) As Variant
' Вычисляет предыдущий период
Dim m As Integer, Y As Integer
    ПредыдущийПериод = Null
        If NoData(Период) Then Exit Function
    m = Период Mod 100
    Y = Период \ 100
    If m = 0 Then   ' Год
        If Y > 0 Then Y = Y - 1
      Else          ' Месяц
        If m > 1 Then
            m = m - 1
          Else
            If Y > 0 Then
                Y = Y - 1
                m = 12
              End If
          End If
      End If
    ПредыдущийПериод = 100 * Y + m
End Function

Public Function НачалоПериода(ByVal Период As Integer) As Integer
' Возвращает месяц начала периода МИНУС 1.
' Для месяцев -- это 0,1,2 и т.д.,
' Для года -- всегда 0.
Dim i As Integer
    i = Период Mod 100
    If i <> 0 Then i = i - 1
    НачалоПериода = i
End Function

Public Function Дата2Период(ByVal Дата As Date, ByVal ТипПериода As Integer) As Variant
' Определяет "календарный" период, соответствующий дате
    Дата2Период = Null
    Select Case ТипПериода
      Case 1    ' Месяц
        Дата2Период = Month(Дата) + 100 * (Year(Дата) - 2000)
      Case 2    ' Год
        Дата2Период = 100 * (Year(Дата) - 2000)
      End Select
End Function

Public Function ПериодПоДате(ByVal Дата As Variant, ByVal ТипПериода As Variant, ByVal ДеньП As Variant)
' Предлагает отчётный период, который включает текущую дату
' Параметр "День" -- либо ДеньП, либо ДеньХ
' Используется в мастерах.
Dim i As Integer
    ПериодПоДате = Null
    If NoData(Дата) Then Exit Function
    If NoData(ТипПериода) Or NoData(ДеньП) Then Exit Function
    i = Дата2Период(Дата, ТипПериода)
    If ПлановаяДата(i, ДеньП) < Дата Then
        i = СледующийПериод(i)
      End If
    ПериодПоДате = i
End Function

Public Function ПериодНазад(ByVal Период As Integer, ByVal Сдвиг As Integer) As Integer
' Вычисляет период, отстоящий на указанный интервал (параметр Сдвиг)
Dim Y As Integer, m As Integer
    m = Период Mod 100
    Y = Период \ 100
    Select Case Сдвиг
      Case 1    ' Месяц
        ПериодНазад = ПредыдущийПериод(Период)
        Exit Function
      Case 2    ' Год
        Y = Y - 1
      Case 3    ' 12 лет
        Y = Y - 12
      End Select
    If Y < 0 Then Y = 0
    ПериодНазад = Y * 100 + m
End Function


'   *****************
'   ***  ГРАНИЦЫ  ***
'   *****************
'
Public Function ДлинаОтчёта(ByVal ТипПериода, ByVal ТипАрхива) As Integer
' Возвращает период, охватываемый отчётом:
' 0 -- неопр., 1 -- месяц, 2 -- год, 3 -- 12 лет
    If NoData(ТипАрхива) Then ТипАрхива = 0
    If NoData(ТипПериода) Then ТипПериода = 0
    If ТипПериода <> 0 Then
        ДлинаОтчёта = ТипПериода
        Exit Function
      End If
    Select Case ТипАрхива
      Case 1, 2
        ДлинаОтчёта = 1
      Case 3
        ДлинаОтчёта = 2
      Case 4
        ДлинаОтчёта = 3
      Case Else
        ДлинаОтчёта = 0
      End Select
End Function

Public Function МеткаЗаписиПоДате(ByVal Дата As Date, ByVal ДеньХ As Integer, ByVal ТипАрхива As Integer) As Long
' Возвращает дату (метку) записи, сохранённую в архиве перед указанной датой.
' Метка имеет значение даты, месяца или года, в зависимости от типа архива.
Dim d As Date
    Select Case ТипАрхива
      Case 1, 2 ' Суточный/Часовой -- (плановый день -1)
        d = Дата
      Case 3    ' Месячный
        d = DateSerial(Year(Дата), Month(Дата), ДеньХ Mod 100)
        If Дата < d Then
            d = DateSerial(Year(Дата), Month(Дата) - 1, ДеньХ Mod 100)
          End If
      Case 4    ' Годовой
        d = DateSerial(Year(Дата), ДеньХ \ 100, ДеньХ Mod 100)
        If Дата < d Then
            d = DateSerial(Year(Дата) - 1, ДеньХ \ 100, ДеньХ Mod 100)
          End If
      Case Else
        МеткаЗаписиПоДате = 0
        Exit Function
      End Select
    d = d - 1
'    ДатаВАрхиве = D
    МеткаЗаписиПоДате = Date2YYMMDD(d, ТипАрхива)
End Function

Public Function ДатаМеткиЗаписи(ByVal Метка As Long, ByVal DayX As Integer) As Date
' Вычисляет дату, когда была сделана запись, по её временной метке
Dim DD As Integer, YM As Integer
    YM = Метка \ 100
    DD = Метка Mod 100
    If DD = 0 Then
    ' Месяц и год. Для дней метка и дата одинаковы
        If (YM Mod 100) = 0 Then YM = YM + (DayX \ 100) ' Для года учитываем МесяцХ
        DD = (DayX Mod 100) - 1
        If DD = 0 Then YM = YM + 1
      End If
    ДатаМеткиЗаписи = DateSerial(2000 + (YM \ 100), YM Mod 100, DD)
End Function

Public Function СледующаяМетка(ByVal Метка As Long, ByVal ТипАрхива As Integer) As Long
' Возвращает метку следующей записи
    Select Case ТипАрхива
      Case 1, 2
        СледующаяМетка = Date2YYMMDD(YYMMDD2Date(Метка) + 1, ТипАрхива)
      Case 3, 4
        СледующаяМетка = CLng(СледующийПериод(Метка \ 100)) * 100
      End Select
End Function

Public Function ПредыдущаяМетка(ByVal Метка As Long, ByVal ТипАрхива As Integer) As Long
' Возвращает метку следующей записи
    Select Case ТипАрхива
      Case 1, 2
        ПредыдущаяМетка = Date2YYMMDD(YYMMDD2Date(Метка) - 1, ТипАрхива)
      Case 3, 4
        ПредыдущаяМетка = CLng(ПредыдущийПериод(Метка \ 100)) * 100
      End Select
End Function

Public Function МеткаНазад(ByVal Метка As Long, ByVal Сдвиг As Integer) As Long
' Вычисляет период, отстоящий на указанный интервал (параметр Сдвиг)
Dim V As Long, d As Integer, m As Integer, Q As Date
    d = Метка Mod 100
    V = CLng(ПериодНазад(Метка \ 100, Сдвиг)) * 100
    If d = 0 Then       ' Если это месяц или год -- то всё ОК
        МеткаНазад = V
        Exit Function
      End If
    ' С обыными датами хуже. Если дата -- 31-е число,
    ' то сдвиг на месяц должен давать последний день предыдущего месяца, а не 1-е того же.
    m = (Метка \ 100) Mod 100
    Q = YYMMDD2Date(V + d)
'    If Month(Q) <> M Then Q = DateSerial(Year(Q), Month(Q) + 1, 0)
'    If Month(Q) <> M Then Q = DateSerial(Year(Q), Month(Q) + 1, 1)
    МеткаНазад = Date2YYMMDD(Q, 1)
End Function

Public Function РазностьМеток(ByVal Начало, ByVal Конец) As Variant
' Вычисляет разность меток. Начало -- метка первой записи, конец -- последней записи.
' Для вычисления числа записей надо увеличить на 1
Dim i As Integer
    If NoData(Начало) Or NoData(Конец) Then
        РазностьМеток = Null
        Exit Function
      End If
    If Начало Mod 10000 = 0 Then
        РазностьМеток = DateDiff("yyyy", YYMMDD2Date(Начало), YYMMDD2Date(Конец))
      ElseIf Начало Mod 100 = 0 Then
        РазностьМеток = DateDiff("m", YYMMDD2Date(Начало), YYMMDD2Date(Конец))
      Else
        РазностьМеток = DateDiff("d", YYMMDD2Date(Начало), YYMMDD2Date(Конец))
      End If
End Function


