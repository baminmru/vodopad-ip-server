Attribute VB_Name = "D1_3__СтрокиБайт"
Option Explicit
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
        If Err() <> 0 Then
            MsgBox "Ошибка в группе " & CStr(i), vbOKOnly, "Внимание!"
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

' *** ASCII-строки ***
' Преобразование строки символов в ANSI-кодировке в массив байт,
' содержащих ASCII-кодировку тех же символов
Function Str2ASCII(ByRef s As String, b() As Byte) As Integer
' Переводит строку символов в последовательность ASCII-кодов
' Возвращает число байт
Dim i As Integer, k As Long, n As Integer, a As String
    n = Len(s)
    Str2ASCII = n
        If n = 0 Then Exit Function
    For i = 0 To n - 1
        a = Mid(s, i + 1, 1)
        k = Asc(a)
        If (k < 0) Or (k > 255) Then k = 0
        b(i) = k
      Next i
End Function

Function ASCII2Str(b() As Byte, ByRef n As Integer) As String
' Выполняет обратное преобразование
Dim i As Integer, s As String
        If n = 0 Then Exit Function
    s = ""
    For i = 0 To n - 1
        s = s & Chr(b(i))
      Next i
    ASCII2Str = s
End Function


