Attribute VB_Name = "D1_1__БитовыеОперации"
Option Explicit
' Модуль предоставляет ряд процедур для работы с битовыми массивами.
' Среди таковых -- сдвиги влево и вправо на 1 бит, сдвиги байтов, извлечения байтов,
' сдвиги байтов и т.д.
'                                                                       07.12.07
'   1. Операции битовых сдвигов влево/вправо для Byte, Integer и Long
'   2. Извлечение /вставка старшего/младшего байта для Integer
'   3. Сдвиги байтов слево/вправо в Integer
'
' ************************
' ***** СДВИГИ БИТОВ *****
' ************************
Function ShLByte(ByRef b As Byte) As Boolean
' Сдвигает биты в байте на 1 влево. Возвращает True, если был перенос
    ShLByte = (b And &H80) <> 0
    b = b And &H7F
    b = b * 2
End Function

Function ShRByte(ByRef b As Byte) As Boolean
' Сдвигает биты в байте на 1 вправо. Возвращает True, если был перенос
    ShRByte = (b And 1) <> 0
    b = b \ 2
End Function

Function ShLInt(ByRef i As Integer) As Boolean
' Сдвигает биты в целом на 1 влево. Возвращает True, если был перенос
Dim k As Integer
    ShLInt = (i And &H8000) <> 0
    k = i And &H4000        ' Запоминаем 14-й бит в переменной k
    i = i And &H3FFF        ' Очищаем оба бита 14 и 15
    i = i * 2               ' Выполняем сдвиг влево
    If k <> 0 Then i = i Or &H8000  ' Записываем старый 14-й бит на место 15-го
End Function

Function ShRInt(ByRef i As Integer) As Boolean
' Сдвигает биты в целом на 1 влево. Возвращает True, если был перенос
    ShRInt = (i And 1) <> 0
    i = i And &HFFFE        ' Очищаем 0-й бит
    i = i \ 2               ' Сдвиг вправо
    i = i And &H7FFF        ' Очищаем 15-й бит
End Function

Function ShLLong(ByRef l As Long) As Boolean
' Сдвигает биты в длинном целом на 1 влево. Возвращает True, если был перенос
Dim k As Long
    ShLLong = (l And &H80000000) <> 0
    k = l And &H40000000    ' Запоминаем 30-й бит в переменной k
    l = l And &H3FFF0000    ' Очищаем оба бита 14 и 15
    l = l * 2               ' Выполняем сдвиг влево
    If k <> 0 Then l = l Or &H80000000  ' Записываем старый 30-й бит на место 31-го
End Function

Function ShRLong(ByRef l As Long) As Boolean
' Сдвигает биты в длинном целом на 1 влево. Возвращает True, если был перенос
    ShRLong = (l And 1) <> 0
    l = l And &HFFFFFFFE    ' Очищаем 0-й бит
    l = l \ 2               ' Сдвиг вправо
    l = l And &H7FFFFFFF    ' Очищаем 15-й бит
End Function


' ********************************
' ***** ОБМЕН БАЙТОВ И ЦЕЛЫХ *****
' ********************************
Function I2B_Lo(ByVal i As Integer) As Byte
' XXYY -> YY. Возвращает правый (младший) байт одинарного целого
    I2B_Lo = i And &HFF
End Function

Function I2B_Hi(ByVal i As Integer) As Byte
' XXYY -> XX. Возвращает левый (старший) байт одинарного целого
    i = i And &HFF00
    i = i \ 256
    I2B_Hi = i And &HFF
End Function

Function B2I_Lo(ByVal b As Byte) As Integer
' XX -> 00XX. Возвращает одинарное целое с заданным левым байтом и нулевым правым
    B2I_Lo = b
End Function

Function B2I_Hi(ByVal b As Byte) As Integer
' XX -> XX00. Возвращает одинарное целое с заданным правым байтом и нулевым левым
Dim i As Integer
    i = b And &H7F
    i = i * 256
    If (b And &H80) <> 0 Then i = i Or &H8000
    B2I_Hi = i
End Function

' ***** ОБМЕН ДЛИННЫХ И ЦЕЛЫХ *****
Function L2I_Lo(ByVal l As Long) As Integer
' XXXXYYYY -> YYYY. Возвращает младшую часть длинного целого
Dim i As Integer
    i = l And (Not &HFFFF8000)      ' Сначала -- без 31-го бита
    If (l And (Not &HFFFF7FFF)) <> 0 Then i = i Or &H8000
    L2I_Lo = i
End Function

Function L2I_Hi(ByVal l As Long) As Integer
' XXXXYYYY -> XXXX. Возвращает старшую часть длинного целого
    l = l And &HFFFF0000
    l = l \ &H10000
    L2I_Hi = L2I_Lo(l)
End Function

Function I2L_Lo(ByVal i As Integer) As Long
' XXXX -> 0000XXXX. Возвращает двойное целое с заданной младшей частью
    I2L_Lo = (i And &HFFFF) And (Not &HFFFF0000)
End Function

Function I2L_Hi(ByVal i As Integer) As Long
' XXXX -> XXXX0000. Возвращает двойное целое с заданной старшей частью
Dim l As Long
    l = i And &H7FFF
    l = i And &H7FFF
    l = l * &H10000
    If (i And &H8000) <> 0 Then l = l Or &H80000000
    I2L_Hi = l
End Function


' ***************************
' ***** БАЙТОВЫЕ СДВИГИ *****
' ***************************
Function ChLInt(ByRef i As Integer) As Byte
' XXYY -> YY00. Выполняет сдвиг одинарного целого на байт влево.
' XX. Возвращает левый (старший) байт. Младший заполняет нулями.
Dim j As Integer
    ChLInt = I2B_Hi(i)
    j = (i And &H80) <> 0
    i = i And &H7F
    i = i * 256
    If j Then i = i Or &H8000
End Function

Function ChRInt(ByRef i As Integer) As Byte
' XXYY -> 00XX. Выполняет сдвиг одинарного целого на байт вправо.
' YY. Возвращает правый (младший) байт. Старший заполняет нулями.
    ChRInt = I2B_Lo(i)
    i = i And &HFF00
    i = i \ 256
    i = i And &HFF
End Function

Function ChLLng(ByRef l As Long) As Byte
' XXYYYYYY -> YYYYYY00. Выполняет сдвиг двойного целого на байт влево.
' XX. Возвращает левый (старший) байт. Младший заполняет нулями.
Dim i As Integer
    ChLLng = I2B_Hi(L2I_Hi(l))
    i = (l And &H800000) <> 0
    l = l And &H7FFFFF
    l = l * 256
    If i Then l = l Or &H80000000
End Function

Function ChRLng(ByRef l As Long) As Byte
' XXXXXXYY -> 00XXXXXX. Выполняет сдвиг двойного целого на байт вправо.
' YY. Возвращает правый (младший) байт. Старший заполняет нулями.
Dim b As Byte
    ChRLng = l And &HFF
    l = l And &HFFFFFF00
    l = l \ 256
    l = l And &HFFFFFF
End Function
