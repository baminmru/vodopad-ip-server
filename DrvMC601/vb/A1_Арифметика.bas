Attribute VB_Name = "A1_Арифметика"
Option Explicit

Public Function NoData(ByVal X As Variant) As Boolean
' Проверяет, содержит ли переменная данные (т.е. не Null и не пустую строку)
' Обычно требуется для проверки значения поля формы или ячейки таблицы
    If IsNull(X) Then
        NoData = True
      ElseIf X = "" Then
        NoData = True
      Else
        NoData = False
      End If
End Function


