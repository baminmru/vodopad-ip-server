Attribute VB_Name = "P_Параметры_P"
Option Explicit
'
Global Const Узел_Мнемо = 219
Global Const Узел_Модель = 221
Global Const Узел_Номер = 222


Global ПрофильОткрыт As Boolean
Private Exl As Excel.Application
Private Wbk As Excel.Workbook

Const iMax = 256

'   ******************************************
'   * ИЗВЛЕЧЕНИЕ ПАРАМЕТРОВ ИЗ ФАЙЛА ПРОФИЛЯ *
'   ******************************************
''
Public Function ОткрытьПрофиль(ByVal Профиль As String) As Boolean
' Возвращает True при успешном открытии
    On Error Resume Next
    Set Exl = New Excel.Application
    Set Wbk = Exl.Workbooks.Open(ДополнитьПуть(Профиль, ПутьКПапке(Путь_Профили)))
    If Err <> 0 Then
        MsgBox "Не удаётся открыть файл профиля!", vbOKOnly, "Внимание!"
        Wbk.Close
        Exl.Quit
        Exit Function
      End If
    ПрофильОткрыт = True
    ОткрытьПрофиль = True
End Function

Public Sub ЗакрытьПрофиль()
    On Error Resume Next
    Wbk.Close
    Exl.Quit
    Set Exl = Nothing
    ПрофильОткрыт = False
End Sub

Public Function СохранитьПрофиль() As Boolean
    On Error Resume Next
    Wbk.Save
    СохранитьПрофиль = (Err <> 0)
End Function

Public Function ЧитатьПараметр_P(ByVal Номер As Long) As Variant
' Выполняет поиск параметра в файле профиля
Dim i As Integer
Dim Q
        If Not ПрофильОткрыт Then Exit Function
    With Wbk.Worksheets(1)
    i = 0
    Do
        i = i + 1
        Q = .Cells(i, 1)
        If (Q = Номер) Then
            ЧитатьПараметр_P = .Cells(i, 4)
            Exit Do
          End If
      Loop While i <= iMax
  End With
End Function

Public Function ПисатьПараметр_P(ByVal Номер As Long, ByVal Значение) As Boolean
' Записывает значение параметра в файле профиля.
' Если параметра с таким номером нет, возвращает False
Dim i As Integer
Dim Q
        If Not ПрофильОткрыт Then Exit Function
    With Wbk.Worksheets(1)
    i = 0
    Do
        i = i + 1
        Q = .Cells(i, 1)
        If (Q = Номер) Then
            .Cells(i, 4) = Значение
            ПисатьПараметр_P = True
            Exit Do
          End If
      Loop While i <= iMax
  End With
End Function


