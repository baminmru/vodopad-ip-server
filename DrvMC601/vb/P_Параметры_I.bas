Attribute VB_Name = "P_Параметры_I"
Option Explicit


Global Const ИНФО_Тип = 1
Global Const ИНФО_ТипАрхива = 2
Global Const ИНФО_ТипПериода = 3
Global Const ИНФО_ЧислоМесяцев = 4
Global Const ИНФО_Форма = 11
Global Const ИНФО_Версия = 12
Global Const ИНФО_ИмяОтчёта = 13
Global Const ИНФО_ДеньП = 14
Global Const ИНФО_Строго = 15
Global Const ИНФО_Маска = 16

Global Const ИнфоМакс = 20
Global ИНФО(1 To ИнфоМакс) As String
Const nMax = 256

Public Function ВзятьИНФО(ByVal i As Integer)
    If (i <= 0) Or (i > ИнфоМакс) Then Exit Function
    ВзятьИНФО = ИНФО(i)
End Function

Public Function ЧитатьИНФО(ByVal Шаблон As Variant) As Integer
' Читает ИНФО шаблона во внутренний массив. Возвращает 0 в случае успеха.
'
Dim Exl As Excel.Application   ' Описание
Dim Wbk As Excel.Workbook
Dim i As Integer, j As Integer
Dim iErr As Integer, s As String

    ' Очищаем параметры
    For i = 1 To ИнфоМакс
        ИНФО(i) = ""
      Next i
    ' Проверяем указание шаблона
    iErr = 1
    If NoData(Шаблон) Then
'        MsgBox "Не указан шаблон!", vbOKOnly, "Ошибка!"
        GoTo 101
      End If
      
    ' Открываем шаблон
    iErr = 2
    s = ДополнитьПуть(Шаблон, ПутьКПапке(Путь_Шаблоны))
    On Error Resume Next
    Set Exl = New Excel.Application
    Set Wbk = Exl.Workbooks.Open(s)
    If Err <> 0 Then
        MsgBox "Не удаётся открыть файл шаблона!" & Chr(10) _
        & "Проверьте правильность имени файла!", vbOKOnly, "Ошибка!"
        GoTo 100
      End If
      
    ' Проверяем шаблон
    On Error GoTo 0
    iErr = 3
    With Exl.Worksheets(1)
        If .Cells(1, 1) <> "ИНФО" Then
        MsgBox "Это, похоже, не шаблон --" & Chr(10) _
        & "отсутствует раздел ""ИНФО""!", vbOKOnly, "Ошибка!"
        GoTo 100
      End If
      
    ' Читаем данные
    iErr = 0
    i = 1
    Do While i < nMax
        i = i + 1
            If .Cells(i, 1) = "Шапка" Then Exit Do
        s = .Cells(i, 1)
        If IsNumeric(s) Then
            j = CInt(s)
            If (j > 0) And (j <= ИнфоМакс) Then
                ИНФО(j) = .Cells(i, 3)
              End If
          End If
      Loop
    End With
    
100:
    Wbk.Close
    Exl.Quit
101:
    ЧитатьИНФО = iErr
End Function



