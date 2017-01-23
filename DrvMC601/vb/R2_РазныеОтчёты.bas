Attribute VB_Name = "R2_РазныеОтчёты"
Option Explicit
Dim Exl As Excel.Application
Dim Wbk As Excel.Workbook
Dim Wrs As Excel.Worksheet

'   *************************
'   *** УПРАВЛЕНИЕ СРЕДОЙ ***
'   *************************
'
Public Sub СчитатьДатуВремя()
    ОтчётДата = MC601_СчитатьТекущий(1003)
        If ОтчётДата = 0 Then ОтчётДата = Date2YYMMDD(Date, 1)
    ОтчётВремя = MC601_СчитатьТекущий(1002)
        If ОтчётВремя = 0 Then ОтчётВремя = Time2HHMMSS(Time())
End Sub


Public Function НачатьОтчёт(Шаблон As String, ИмяОтчёта As String)
' Открывает новый отчёт. Шаблон -- это шаблон, ИмяОтчёта -- это имя нового отчёта.
Dim s As String
'    If ИмяОтчёта = "" Then
'          MsgBox "Не указано имя файла отчёта!", vbOKOnly, "Ошибка!"
'          Exit Function
'        End If
'    s = ДополнитьПуть(ИмяОтчёта & ".xls", ПутьКПапке(Путь_Отчёты))
'    If Dir(s) <> "" Then
'        MsgBox "Отчёт с таким именем уже есть!" & vbCrLf & _
'            "Измените имя нового или старого отчёта" & vbCrLf & _
'            "и повторите операцию!", _
'            vbOKOnly, "Ошибка!"
'        Exit Function
'      End If

    s = ПутьКФайлу(Шаблон, Путь_Шаблоны)
        If s = "" Then Exit Function
    On Error Resume Next
    Set Exl = New Excel.Application
    If Err <> 0 Then
          MsgBox "Невозможно запустить Microsoft Excel!" & vbCrLf & _
            "Попробуйте запустить Excel из главного меню." & vbCrLf & _
            "В случае сохранения ошибке обратитесь к руководству.", _
            vbOKOnly, "Ошибка!"
          Exit Function
        End If
        Set Wbk = Exl.Workbooks.Add(s)
    If Err <> 0 Then
          MsgBox "Не удаётся создать отчёт!" & vbCrLf & _
            "Убедитель, что шаблон имеется" & vbCrLf & _
            "и не повреждён.", _
            vbOKOnly, "Ошибка!"
        Wbk.Close
        Exl.Quit
        Exit Function
      End If
    Wbk.SaveAs FileName:=ДополнитьПуть(ИмяОтчёта, ПутьКПапке(Путь_Отчёты))
'    Exl.Visible = True
End Function

Public Sub ЗавершитьОтчёт()
    Exl.Visible = True
    On Error Resume Next
    Set Wrs = Nothing
    Set Exl = Nothing
    Set Wbk = Nothing
End Sub


'   *************************
'   *** СОБСТВЕННО ОТЧЁТЫ ***
'   *************************
Public Sub ОтчётНикакой(Шаблон As String, ИмяОтчёта As String, ByVal Отступ, ByVal Всего)
    ИндикаторНачать
    НачатьОтчёт Шаблон, ИмяОтчёта
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    СчитатьДатуВремя
    ОбработатьПараметры Wrs, Sect_All, Subs_All
1:  ЗавершитьОтчёт
    ИндикаторГотово
    ИндикаторЗакрыть
End Sub

Public Sub ОтчётФорма101(Шаблон As String, ИмяОтчёта As String, ByVal Отступ, ByVal Всего)
' *** Тип 101 *** Почасовой отчёт за месяц Свёрнут по суткам. 3-х страничная форма.
Dim i As Integer
        If NoData(Всего) Or (Всего = 0) Then Exit Sub
        If NoData(Отступ) Then Exit Sub
  ' Подготовка формы
    ИндикаторНачать
    НачатьОтчёт Шаблон, ИмяОтчёта
    ' Протокол
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    СоставТаблицы Wrs
    СекцииТаблицы Wrs
    ПодготовитьСложнуюТаблицу Wrs, Всего
    ' Свёртка
    ИндикаторДоля 0.03
    Set Wrs = Wbk.Worksheets(3)
    ОпределитьСекции Wrs
    РазвернутьСвёрнутуюТаблицы Wrs, Всего
    ' Отчёт
    ИндикаторДоля 0.04
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    РазвернутьПростуюТаблицу Wrs, Всего
    
  ' Считывание данных
    Set Wrs = Wbk.Worksheets(2)
    ИндикаторПодговить kTab * Всего, 0.05, 0.95
    ОпределитьСекции Wrs
    '   *****************************************
    If ОбработатьЧасовой(Wrs, Date - Отступ, Всего) <> 0 Then GoTo 1
    '   *****************************************
    ИндикаторДоля 0.95
    СчитатьДатуВремя
    ОбработатьПараметры Wrs, Sect_Inf, Subs_All
    ОбработатьПараметры Wrs, Sect_Cur, Subs_All
    
    ' Подготовка отчёта
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    ОбработатьПараметры Wrs, Sect_Hdr, Subs_All
    ОбработатьПараметры Wrs, Sect_Sum, Subs_All

    ' Сохранение
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    If Плановый Then СохранитьВПрофиле Wrs

  ' Зачистка отчёта (с низу)
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    ИндикаторДоля 0.97
    УдалитьЗаголовок Wrs, Sect_End
    УдалитьСекцию Wrs, Sect_Mem
    УдалитьЗаголовок Wrs, Sect_Sum
    УдалитьЗаголовок Wrs, Sect_Tbl
    УдалитьЗаголовок Wrs, Sect_Hdr
    УдалитьСекцию Wrs, Sect_Inf
    УдалитьПервыйСтолбец Wrs

1:
    ИндикаторГотово
    ЗавершитьОтчёт
    ИндикаторЗакрыть
End Sub
'

Public Sub ОтчётФорма201(Шаблон As String, ИмяОтчёта As String, ByVal Отступ, ByVal Всего)
' *** Тип 201 *** Посуточный отчёт за месяц. 2-х страничная форма.
        If NoData(Всего) Or (Всего = 0) Then Exit Sub
        If NoData(Отступ) Then Exit Sub
    ИндикаторНачать
    НачатьОтчёт Шаблон, ИмяОтчёта
    ' Подготовка протокола
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    СоставТаблицы Wrs
    ПодготовитьПростуюТаблицу Wrs, Всего
    
    Set Wrs = Wbk.Worksheets(1)
     ИндикаторДоля 0.05
    ОпределитьСекции Wrs
    РазвернутьПростуюТаблицу Wrs, Всего

    ' Считывание данных
    ИндикаторПодговить kTab * Всего, 0.1, 0.9
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    '   *****************************************
        If ОбработатьСуточный(Wrs, Отступ, Всего) <> 0 Then GoTo 1
    '   *****************************************
    СчитатьДатуВремя
    ОбработатьПараметры Wrs, Sect_Inf, Subs_All
    ОбработатьПараметры Wrs, Sect_Cur, Subs_All
    
    ' Подготовка отчёта
    ИндикаторДоля 0.95
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    ОбработатьПараметры Wrs, Sect_Hdr, Subs_All
    ОбработатьПараметры Wrs, Sect_Sum, Subs_All

    ' Сохранение
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    If Плановый Then СохранитьВПрофиле Wrs

    ' Зачистка отчёта (с низу)
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    УдалитьЗаголовок Wrs, Sect_End
    УдалитьСекцию Wrs, Sect_Mem
    УдалитьЗаголовок Wrs, Sect_Sum
    УдалитьЗаголовок Wrs, Sect_Tbl
    УдалитьЗаголовок Wrs, Sect_Hdr
    УдалитьСекцию Wrs, Sect_Inf
    УдалитьПервыйСтолбец Wrs
    
1:  ЗавершитьОтчёт
    ИндикаторГотово
    ИндикаторЗакрыть
End Sub

Public Sub ОтчётФорма301(Шаблон As String, ИмяОтчёта As String, ByVal Отступ, ByVal Всего)
' *** Тип 301 *** Помесячный отчёт за год. 2-х страничная форма.
' Считывается на одну запись больше -- за предшествующий период, т.к. выводятся разности.
        If NoData(Всего) Or (Всего = 0) Then Exit Sub
        If NoData(Отступ) Then Exit Sub
    ИндикаторНачать
    НачатьОтчёт Шаблон, ИмяОтчёта
    ' Подготовка протокола
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    СоставТаблицы Wrs
    ПодготовитьПростуюТаблицу Wrs, Всего + 1
    
    Set Wrs = Wbk.Worksheets(1)
     ИндикаторДоля 0.05
    ОпределитьСекции Wrs
    РазвернутьПростуюТаблицу Wrs, Всего + 1

    ' Считывание данных
    ИндикаторПодговить kTab * (Всего + 1), 0.1, 0.9
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    '   *****************************************
        If ОбработатьМесячный(Wrs, Отступ, Всего + 1) <> 0 Then GoTo 1
    '   *****************************************
    СчитатьДатуВремя
    ОбработатьПараметры Wrs, Sect_Inf, Subs_All
    ОбработатьПараметры Wrs, Sect_Cur, Subs_All
    
    ' Обработка отчёта
    ИндикаторДоля 0.95
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    ОбработатьПараметры Wrs, Sect_Hdr, Subs_All
    ОбработатьПараметры Wrs, Sect_Sum, Subs_All

    ' Сохранение
    Set Wrs = Wbk.Worksheets(2)
    ОпределитьСекции Wrs
    If Плановый Then СохранитьВПрофиле Wrs

    ' Зачистка отчёта (с низу)
    Set Wrs = Wbk.Worksheets(1)
    ОпределитьСекции Wrs
    УдалитьЗаголовок Wrs, Sect_End
    УдалитьСекцию Wrs, Sect_Mem
    УдалитьЗаголовок Wrs, Sect_Sum
    УдалитьСтрокуСекции Wrs, 1, Sect_Tbl
    УдалитьЗаголовок Wrs, Sect_Tbl
    УдалитьЗаголовок Wrs, Sect_Hdr
    УдалитьСекцию Wrs, Sect_Inf
    УдалитьПервыйСтолбец Wrs
    
1:  ЗавершитьОтчёт
    ИндикаторГотово
    ИндикаторЗакрыть
End Sub

