Attribute VB_Name = "D1_2__МассивыБайт"
Option Explicit
' Вспомогательный модуль. Содержит процедуры считывания байтового
' массива в 16-ричной записи (типа 3F,06,78,A4,F6) и, наобором,
' вывод значений байтового массива в текстовую строку.
'   Кроме того -- процедуры копирования и вставки.
'
' ***********************************
' **** УПАКОВКА/РАСПАКОВКА ЦЕЛЫХ ****
' ***********************************
Function Bytes2I(b() As Byte, ByVal n As Integer, ByVal iPos As Integer) As Integer
' XX, YY -> XXYY
' Преобразует последовательность байтов в одинарное целое. Первый байт -- старший.
' ВНИМАНИЕ! В целом останутся ПОСЛЕДНИЕ 2 байта!
Dim k As Integer
Dim i As Integer
    k = 0
    i = 0
    Do While i < n
        ChLInt k
        k = k Or b(i + iPos)
        i = i + 1
      Loop
    Bytes2I = k
End Function

Sub I2Bytes(ByVal k As Integer, b() As Byte, ByVal iPos As Integer)
' XXYY -> XX, YY
' Преобразует одинарное целое в массив из 2 байтов. Первый байт -- старший.
Dim i As Integer
    For i = 0 To 1
        b(i + iPos) = ChLInt(k)
      Next i
End Sub

Function Bytes2L(b() As Byte, ByVal n As Integer, ByVal iPos As Integer) As Long
' XX, YY, ZZ, UU -> XXYYZZUU
' Преобразует последовательность байтов в длинное целое. Первый байт -- старший.
' ВНИМАНИЕ! В длинном целом останутся ПОСЛЕДНИЕ 4 байта!
Dim l As Long
Dim i As Integer
    l = 0
    i = 0
    Do While i < n
        ChLLng l
        l = l Or b(i + iPos)
        i = i + 1
      Loop
    Bytes2L = l
End Function

Sub L2Bytes(ByVal l As Long, b() As Byte, ByVal iPos As Integer)
' Преобразует длинное целое в массив из 4 байтов. Первый байт -- старший.
Dim i As Integer
    For i = 0 To 3
        b(i + iPos) = ChLLng(l)
      Next i
End Sub


' *********************************
' **** ДЕКОДИРОВАНИЕ РЕГИСТРОВ ****
' *********************************
Function Bytes2Float(b() As Byte, ByVal iPos As Integer) As Double
' Разбор вещественных в формате протокола Kamstrup.
'
Dim i As Integer, n As Integer
Dim a As Double, d As Double
    a = 0
    i = 0
    n = b(iPos)     ' Берём число байтов
    Do While i < n
        a = a * 256
        i = i + 1
        a = a + b(iPos + 1 + i)
      Loop
    d = 1
    n = b(iPos + 1)
    i = n And &H3F
    Do While i > 0
        d = d * 10
        i = i - 1
      Loop
    If (n And &H40) <> 0 Then d = 1 / d
    a = a * d
    If (n And &H80) <> 0 Then a = -a
    Bytes2Float = a
End Function

Function Byte2Factor(ByVal b As Byte) As Double
Dim d As Double
Dim i As Integer
    d = 1
    i = b And &H3F
    Do While i > 0
        d = d * 10
        i = i - 1
      Loop
    If (b And &H40) <> 0 Then d = 1 / d
    If (b And &H80) <> 0 Then d = -d
    Byte2Factor = d
End Function

Function Byte2Mantiss(b() As Byte, ByVal n As Integer, ByVal iPos As Integer) As Double
Dim a As Double
Dim i As Integer
    a = 0
    i = 0
    Do While i < n
        a = a * 256
        a = a + b(iPos + i)
        i = i + 1
      Loop
    Byte2Mantiss = a
End Function
