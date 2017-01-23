Attribute VB_Name = "D1_3__CRC"
Option Explicit
' Модуль предоставляет процедуру вычисления циклического
' контрольного кода по стандарту CCITT CRC-16

Function ShortResidue(ByVal P As Byte, ByVal Z As Integer) As Integer
' Функция вычисляет остаток от деления полинома p00 на делитель вида 1zz.
' P -- старший байт делимого, Z -- маска делителя
' Возвращает остаток от деления (2 байтовое число).
Dim i As Integer
Dim r As Integer       ' Накопитель остатка
    r = 0
    For i = 1 To 8
        If ShLInt(r) <> ShLByte(P) Then r = r Xor Z
      Next i
    ShortResidue = r
End Function

Function CRC_CCITT16(a() As Byte, n As Integer, iPos As Integer) As Integer
' Функция вычисляет CCITT CRC-16 для массива байтов A().
' iPos -- начальная позиция в массиве.
' Возвращает код CRC (2 байтовое число).
Const InitR = 0         ' Начальный остаток
Const MaskZ = &H1021    ' Маска делителя

Dim i As Integer
Dim P As Byte           ' Головной байт
Dim r As Integer        ' Остаток
    r = InitR
    If n > 0 Then
        For i = 0 To n - 1
            P = ChLInt(r) Xor a(iPos + i)
            r = r Xor ShortResidue(P, MaskZ)
          Next i
      End If
    CRC_CCITT16 = r
End Function

