Attribute VB_Name = "Заполнение"
Option Explicit
'  Процедуры ниже заполняют протокол данными со счётчика
'
Public Function ОбработатьЧасовой(Wrs As Excel.Worksheet, ByVal До As Date, ByVal Всего As Integer) As Integer
Dim i As Integer, j As Integer, k As Integer
Dim Регистр As Integer, iDay As Integer
Dim iStr As Integer, iPos As Integer
Dim День As Date
Dim a
  With Wrs
        If kTab = 0 Then Exit Function
    День = До - Всего + 1
    iDay = Sect(Sect_Tbl, 1) + TblBeg
    For k = 1 To Всего
        For i = 1 To kTab               ' Цикл по регистрам
         If ПрерватьЧтение Then
            ОбработатьЧасовой = 1
            Exit Function
          End If
           Регистр = sTab(2, i)
            iPos = sTab(1, i)
            iStr = iDay
            ' *************************************
            a = MC601_СчитатьЧасовой(Регистр, День)
            ' *************************************
 ИндикаторЕщё (1)
            For j = 24 To 1 Step -1             ' Записываем в блок
                .Cells(iStr, iPos) = a(j)
                iStr = iStr + 1
              Next j
          Next i
        iDay = iDay + TblLen
        День = День + 1
      Next k
  End With
End Function

Public Function ОбработатьСуточный(Wrs As Excel.Worksheet, ByVal Отступ As Integer, ByVal Всего As Integer) As Integer
' Выполняет запись данных в поля таблицы (в обратном порядке)
' Возвращает признак ошибки. 0 -- чтение прошло успешно
Dim i As Integer, j As Integer
Dim Регистр As Integer, Считано As Integer
Dim iStr As Integer, iPos As Integer
Dim a
  With Wrs
        If kTab = 0 Then Exit Function
    For i = 1 To kTab
        If ПрерватьЧтение Then
            ОбработатьСуточный = 1
            Exit Function
          End If
        iPos = sTab(1, i)
        Регистр = sTab(2, i)
        ' *************************************
        a = MC601_СчитатьСуточный(Регистр, Отступ, Всего, Считано)
        ' *************************************
'            If n = 0 Then Exit Sub
        If Считано > Всего Then Считано = Всего
 ИндикаторЕщё (Считано)
        iStr = Sect(Sect_Tbl, 1) + Всего
        For j = 1 To Считано
            If Регистр = 1003 Then
                If a(j) = 0 Then
                    .Cells(iStr, iPos) = Null
                  Else
                    .Cells(iStr, iPos) = YYMMDD2Date(a(j))
                  End If
              Else
                .Cells(iStr, iPos) = a(j)
              End If
            iStr = iStr - 1
          Next j
      Next i
  End With
End Function

Public Function ОбработатьМесячный(Wrs As Excel.Worksheet, ByVal Отступ As Integer, ByVal Всего As Integer) As Integer
' Выполняет запись данных в поля таблицы (в обратном порядке)
' Возвращает признак ошибки. 0 -- чтение прошло успешно
Dim i As Integer, j As Integer
Dim Регистр As Integer, Считано As Integer
Dim iStr As Integer, iPos As Integer
Dim a
  With Wrs
        If kTab = 0 Then Exit Function
    For i = 1 To kTab
         If ПрерватьЧтение Then
            ОбработатьМесячный = 1
            Exit Function
          End If
        iPos = sTab(1, i)
        Регистр = sTab(2, i)
        ' *************************************
        a = MC601_СчитатьМесячный(Регистр, Отступ, Всего, Считано)
        ' *************************************
'            If n = 0 Then Exit Sub
        If Считано > Всего Then Считано = Всего
 ИндикаторЕщё (Считано)
        iStr = Sect(Sect_Tbl, 1) + Всего
        For j = 1 To Считано
            If Регистр = 1003 Then
                If a(j) = 0 Then
                    .Cells(iStr, iPos) = Null
                  Else
                    .Cells(iStr, iPos) = YYMMDD2Date(a(j))
                  End If
              Else
                .Cells(iStr, iPos) = a(j)
              End If
            iStr = iStr - 1
          Next j
      Next i
  End With
End Function

