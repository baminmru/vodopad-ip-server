Attribute VB_Name = "Замены"
Option Explicit

Global Const Subs_P = 1
Global Const Subs_Q = 2
Global Const Subs_S = 4
Global Const Subs_All = 255
Global Const Subs_PQ = 3

'   ***************
'   * МАКРОЗАМЕНЫ *
'   ***************
Public Function НайтиМакро(s As String, Старт As Long, Начало As Long, Конец As Long) As String
' Ищет начало макроса вида {%Annn_XXXXX} в тексте.
' Начало указывает на позицию "{", Конец -- на следующую после "}".
' Если не находит, то Начало = Конец и оба указывают за конец строки
' Выделяет и возвращает код Annn
Dim i As Long
Dim r As String
    Конец = Len(s) + 1
    Начало = Конец
    i = InStr(Старт, s, "{%")
        If i = 0 Then Exit Function     ' Нет начала
    Начало = i
    i = InStr(i + 2, s, "}")
        If i = 0 Then Exit Function     ' Нет конца
    Конец = i + 1
    r = Mid(s, Начало + 2, Конец - Начало - 3)
    i = InStr(1, r, "_")
    If i <> 0 Then                      ' Есть мнемо
        r = Left(r, i - 1)
      End If
    НайтиМакро = r
End Function

Public Function ЗаменитьПараметры(ByRef s As String, ByVal Тип As Integer) As Boolean
' Обрабатывает строку.
' Заменяет одни или несколько параметров. Тип -- это маска параметра
Dim Старт As Long, Конец As Long
Dim i As Long, j As Long, k As Long
Dim iMacro As Long, pMacro As String
Dim Q As String, r As String
    Конец = Len(s)
    Старт = 1
    Q = ""
  ' Цикл поиска всех макросов в строке
    Do
        r = НайтиМакро(s, Старт, i, j)
        If i = j Then
            Q = Q & Mid(s, Старт, Конец - Старт + 1)
            Exit Do
          End If
        Q = Q & Mid(s, Старт, i - Старт)
        k = InStr("PQ S", Mid(r, 1, 1))
        k = k And Тип               ' Выбираем только нужный макрос
        If k <> 0 Then
          ' Замена макроса
            r = Mid(r, 2)
            If IsNumeric(r) Then    '
              ' Указан номер параметра
                ' ****************
                Select Case k
                  Case Subs_P    ' P
                    Q = Q & ЧитатьПараметр_P(CLng(r))
                  Case Subs_Q    ' Q
                    Q = Q & ЧитатьПараметр_Q(CLng(r))
                  Case Subs_S    ' S
                    Q = Q & ЧитатьПараметр_S(CLng(r))
                  End Select
                ЗаменитьПараметры = True
                ' ****************
              Else
              ' Это не номер -- переносим как есть без замены
                Q = Q & Mid(s, i, j - i)
              End If
          Else
          ' Это не макро -- переносим как есть без замены
            Q = Q & Mid(s, i, j - i)
          End If
        Старт = j
      Loop
    s = Q
End Function
