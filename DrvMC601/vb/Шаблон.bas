Attribute VB_Name = "Шаблон"
Option Explicit

Dim mStr As Long, mPos As Long          ' Размер рабочей области

' Секции шаблона
Global Const Sect_All = 0
Global Const Sect_Inf = 1
Global Const Sect_Hdr = 2
Global Const Sect_Tbl = 3
Global Const Sect_Sum = 4
Global Const Sect_Cur = 5
Global Const Sect_Mem = 6
Global Const Sect_End = 7

Global Const mSect = 7                  ' Число секций
Global Sect(1 To mSect, 1 To 2) As Long    ' Положение и размер секций
Const Заголовки = "ИНФО      Шапка     Таблица   Итоги     Текущие   Сохранить Конец     "
'                 "1234567890123456789012345678901234567890123456789012345678901234567890"

' Секции таблицы (для группированных таблиц)
Const МеткаНач = "Нач"
Const МеткаКон = "Кон"
Global TblNum As Long      ' Число групп (Всего)
Global TblBeg As Long      ' Смещение первой записи
Global TblDat As Long      ' Число строк данных
Global TblLen As Long      ' Размер группы

' Состав таблицы (для группированных таблиц)
Global Const mTab = 32
Global kTab As Long                        ' Число столбцов  для вывода в таблице
Global sTab(1 To 2, 1 To mTab) As Long     ' Колонка, Регистр

' *****************
' *** Геометрия ***
' *****************
Public Sub ОпределитьСекции(Wrs As Excel.Worksheet)
' Определяет реальный размеры шаблона (чтобы сократить рысканье)
Dim i As Long, k As Long
Dim r As Excel.Range, Q As Excel.Range
Dim s As String
  ' Обнуляем все секции
    For i = 1 To mSect
        Sect(i, 1) = 0
        Sect(i, 2) = 0
      Next i
  ' Определяем размеры области
    mStr = Wrs.UsedRange.Row + Wrs.UsedRange.Rows.Count - 1
    mPos = Wrs.UsedRange.Column + Wrs.UsedRange.Columns.Count - 1
  ' Ищем заголовки
  With Wrs
    ' Первый заголовок должен быть "ИНФО"
    If .Cells(1, 1) <> Trim(Mid(Заголовки, 1, 10)) Then Exit Sub  ' это не шаблон!
    Sect(Sect_Inf, 1) = 1
    ' Отыскиваем остальные заголовки
    Set r = .Range(.Cells(Sect_Inf, 1), .Cells(mStr, 1))
    For i = 2 To mSect
        s = Trim(Mid(Заголовки, 10 * (i - 1) + 1, 10))
        Set Q = r.Find(s, LookAt:=xlWhole)
        If Not (Q Is Nothing) Then
            Sect(i, 1) = Q.Row
            Set r = .Range(Q, .Cells(mStr, 1))
          End If
      Next i
    End With
  ' Вычисляем размеры секций
    k = mStr + 1
    For i = mSect To 1 Step -1
        If Sect(i, 1) <> 0 Then
            Sect(i, 2) = k - Sect(i, 1) - 1
            k = Sect(i, 1)
          End If
      Next i
End Sub

' ****************
' *** Зачистка ***
' ****************
'   Удаление надо проводить с конца: тогда номера строк не меняются
'
Public Sub УдалитьСекцию(Wrs As Excel.Worksheet, Секция As Integer)
' Удаляет всю секцию вместе с заголовком
Dim i As Long, k As Long
  With Wrs
    i = Sect(Секция, 1)
        If i = 0 Then Exit Sub
    k = Sect(Секция, 2)
    .Range(.Cells(i, 1), .Cells(i + k, 1)).EntireRow.Delete
    End With
End Sub

Public Sub УдалитьСтрокуСекции(Wrs As Excel.Worksheet, ByVal Номер As Integer, ByVal Секция As Integer)
' Удаляет выбранную строку. Номер -- смещение от заголовка (заголовок имеет номер 0)
Dim i As Long
  With Wrs
    i = Sect(Секция, 1)
        If i = 0 Then Exit Sub
    .Cells(i + Номер, 1).EntireRow.Delete
    Sect(Секция, 2) = Sect(Секция, 2) - 1
    End With
End Sub

Public Sub УдалитьЗаголовок(Wrs As Excel.Worksheet, ByVal Секция As Integer)
' Удаляет только заголовок секции
Dim i As Long
  With Wrs
    i = Sect(Секция, 1)
        If i = 0 Then Exit Sub
    .Cells(i, 1).EntireRow.Delete
    End With
End Sub

Public Sub УдалитьПервыйСтолбец(Wrs As Excel.Worksheet)
  With Wrs
    .Cells(1, 1).EntireColumn.Delete
    End With
End Sub

' ******************
' *** Замены ***
' ******************
Public Sub ОбработатьПараметры(Wrs As Excel.Worksheet, ByVal Секция As Integer, ByVal КакиеПараметры As Integer)
' Открывает файл профиля и выполняет все замены
Dim i As Integer, j As Integer
Dim s As String
    With Wrs
    If Секция = Sect_All Then
        For i = 1 To mStr
        For j = 1 To mPos
            If Not IsError(.Cells(i, j)) Then
                s = .Cells(i, j)
                If ЗаменитьПараметры(s, КакиеПараметры) Then .Cells(i, j) = s
              End If
          Next j
          Next i
      Else
        If Sect(Секция, 1) = 0 Then Exit Sub
        If Sect(Секция, 2) = 0 Then Exit Sub
        For i = Sect(Секция, 1) + 1 To Sect(Секция, 1) + Sect(Секция, 2)
        For j = 2 To mPos
            If Not IsError(.Cells(i, j)) Then
                s = .Cells(i, j)
                If ЗаменитьПараметры(s, КакиеПараметры) Then .Cells(i, j) = s
              End If
          Next j
          Next i
      End If
  End With
End Sub

' *************************
' *** Структура таблицы ***
' *************************
Public Sub СоставТаблицы(Wrs As Excel.Worksheet)
' Анализирует состав регистров для вывода в таблицу
Dim ii As Long, jj As Long
Dim i As Long, k As Long
Dim s As String
  With Wrs
    kTab = 0
    k = Sect(Sect_Tbl, 1)
        If k = 0 Then Exit Sub
    i = 2
    Do While i <= mPos
        s = .Cells(k, i)
            If s = "" Then GoTo 1
        s = НайтиМакро(s, 1, ii, jj)
            If s = "" Then GoTo 1
            If Mid(s, 1, 1) <> "S" Then GoTo 1
        s = Mid(s, 2)
            If Not IsNumeric(s) Then GoTo 1
        kTab = kTab + 1
        sTab(1, kTab) = i
        sTab(2, kTab) = CLng(s)
1:      i = i + 1
      Loop
    End With
End Sub

Public Sub СекцииТаблицы(Wrs As Excel.Worksheet)
' Вычисляет смещение до первой записи (Нач) данных и размер группы данных дл метки (Кон)
' В данной версии все группы должны быть одного размера
Dim r As Excel.Range, Q As Excel.Range
Dim i As Long, k As Long
    TblBeg = 0
    TblDat = 0
    TblLen = 0
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    TblLen = Sect(Sect_Tbl, 2)
  With Wrs
    Set r = .Range(.Cells(i + 1, 1), .Cells(i + TblLen, 1))
    Set Q = r.Find(МеткаНач, LookAt:=xlWhole)
        If Q Is Nothing Then Exit Sub
    TblBeg = Q.Row - i
    Set r = .Range(Q, .Cells(i + TblLen, 1))
    Set Q = r.Find(МеткаКон, LookAt:=xlWhole)
        If Q Is Nothing Then Exit Sub
    TblDat = Q.Row - i - TblBeg
  End With
End Sub


' ************************************
' *** Подготовка таблиц заполнения ***
' ************************************
' Подготовка таблиц, в которые будут заноситься данные.
'
Public Sub ПодготовитьПростуюТаблицу(Wrs As Excel.Worksheet, ByVal Всего As Long)
' Готовит табличную часть, копируя первую строку нужное число раз
Dim i As Long, k As Long
        If Всего <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + 1
  With Wrs
    ' Удаляем все строки, кроме первой
    k = Sect(Sect_Tbl, 2) - 1
    If k > 0 Then
        .Range(.Cells(i + 1, 1), .Cells(i + k, 1)).EntireRow.Delete
      End If
    ' Зачищаем поля первой строки, в которые пишутся данные
    If kTab <> 0 Then
        For k = 1 To kTab
            .Cells(i, sTab(1, k)) = Null
          Next k
      End If
    ' Копируем 1-ю строку Всего-1 раз
    If Всего > 1 Then
        For k = 1 To Всего - 1
            .Cells(i, 1).EntireRow.Copy
            i = i + 1
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    End With
    ' Корректируем положения секций
    ОпределитьСекции Wrs
End Sub

Public Sub ПодготовитьСложнуюТаблицу(Wrs As Excel.Worksheet, ByVal Всего As Long)
' Готовит табличную часть, копируя блок таблицы нужное число раз
Dim i As Long, k As Long
        If Всего <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + TblBeg
  With Wrs
    ' Зачищаем столбцы, в которые пишутся данные
    If kTab <> 0 Then
        For k = 1 To kTab
        .Range(.Cells(i, sTab(1, k)), .Cells(i + TblDat, sTab(1, k))) = Null
'            .Cells(i, sTab(1, k)) = Null
          Next k
      End If
    ' Копируем Блок Всего-1 раз
    i = Sect(Sect_Tbl, 1) + 1
    If Всего > 1 Then
        For k = 1 To Всего - 1
            .Range(.Cells(i, 1), .Cells(i + TblLen - 1, 1)).EntireRow.Copy
            i = i + TblLen
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    End With
    ' Корректируем положения секций
    ОпределитьСекции Wrs
End Sub

' ********************************
' *** Подготовка таблиц ссылок ***
' ********************************
' Формирует таблицы ссылок (таблиц, ссылающихся на таблицы данных)
'
Public Sub РазвернутьПростуюТаблицу(Wrs As Excel.Worksheet, ByVal Всего As Long)
Dim i As Long, k As Long
        If Всего <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + 1
  With Wrs
    ' Удаляем все строки, кроме первой
    k = Sect(Sect_Tbl, 2) - 1
    If k > 0 Then
        .Range(.Cells(i + 1, 1), .Cells(i + k, 1)).EntireRow.Delete
      End If
    ' Копируем 1-ю строку Всего-1 раз
    If Всего > 1 Then
        For k = 1 To Всего - 1
            .Cells(i, 1).EntireRow.Copy
            i = i + 1
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    End With
    ' Корректируем положения секций
    ОпределитьСекции Wrs
End Sub

Public Sub РазвернутьСвёрнутуюТаблицы(Wrs As Excel.Worksheet, ByVal Всего As Long)
' Выполняет формирование сводной таблицы.
Dim r As Excel.Range
Dim i As Long, k As Long
        If Всего <= 0 Then Exit Sub
    i = Sect(Sect_Tbl, 1)
        If i = 0 Then Exit Sub
    i = i + 1
  With Wrs
  ' Добавляем балласт
    ' Удаляем все строки, кроме первой
    k = Sect(Sect_Tbl, 2) - 1
    If k > 0 Then
        .Range(.Cells(i + 1, 1), .Cells(i + k, 1)).EntireRow.Delete
      End If
    ' Копируем 1-ю строку и зачищаем её
    If TblLen > 1 Then
        .Cells(i, 1).EntireRow.Copy
        i = i + 1
        .Cells(i, 1).EntireRow.Insert
        .Range(.Cells(i, 2), .Cells(i, mPos)) = Null
       End If
    ' Добавляем ещё TblLen-2 строк
    If TblLen > 2 Then
        For k = 1 To TblLen - 2
            .Cells(i, 1).EntireRow.Copy
            i = i + 1
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
  ' Копируем "балластный" блок Всего-1 раз
    i = Sect(Sect_Tbl, 1) + 1
    If Всего > 1 Then
        For k = 1 To Всего - 1
            .Range(.Cells(i, 1), .Cells(i + TblLen - 1, 1)).EntireRow.Copy
            i = i + TblLen
            .Cells(i, 1).EntireRow.Insert
          Next k
      End If
    ' Удаляем балласт
    If TblLen > 1 Then
        For k = 1 To Всего
            .Range(.Cells(i + 1, 1), .Cells(i + TblLen - 1, 1)).EntireRow.Delete
            i = i - TblLen
          Next k
      End If
    End With
    ' Корректируем положения секций
    ОпределитьСекции Wrs
End Sub

Public Sub СохранитьВПрофиле(Wrs As Excel.Worksheet)
' Читает и сохраняет в профиле параметры секции (Sect_Mem)
Dim i As Integer, j As Integer
Dim s As String
        If Sect(Sect_Mem, 1) = 0 Then Exit Sub
        If Sect(Sect_Mem, 2) = 0 Then Exit Sub
  With Wrs
    For i = Sect(Sect_Mem, 1) + 1 To Sect(Sect_Mem, 1) + Sect(Sect_Mem, 2)
        s = .Cells(i, 1)
        If IsNumeric(s) Then
            ПисатьПараметр_P CInt(s), .Cells(i, 3)
          End If
      Next i
    End With
    СохранитьПрофиль
End Sub

