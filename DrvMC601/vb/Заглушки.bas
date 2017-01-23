Attribute VB_Name = "«аглушки"
Option Explicit

Public Function „итатьѕараметр_S(Ќомер As Integer)
    „итатьѕараметр_S = MC601_—читать“екущий(Ќомер)
'    „итатьѕараметр_S = "SSSSS"
End Function

'Public Function „итатьѕараметр_Q(Ќомер As Integer)
'    „итатьѕараметр_Q = "QQQQQ"
'End Function

'Public Function „итатьѕараметр_P(Ќомер As Integer)
'    „итатьѕараметр_P = "PPPPP"
'End Function


Public Function ѕустой—уточный1(–егистр As Integer, _
                „итатьќт As Integer, Ќећенее As Integer, ByRef —читано As Integer) As Variant
' „итает из суточного архива указанный регистр. ¬озвращает массив считанных данных.
Dim a(1 To 64) As Double
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    For i = 1 To Ќећенее
        a(i) = 0
      Next i
    —читано = Ќећенее
    ѕустой—уточный1 = a
End Function

Public Function ѕустой„асовой1(–егистр As Integer, ƒата As Date) As Variant
' „итает из регистр из часового архива за указанную дату. ¬озвращает массив из 24 показаний.
Dim a(1 To 24)
Dim f As Double, b As Double
Dim i As Integer, j As Integer
Dim k As Integer, l As Integer
Dim iPos As Integer, iErr As Integer
    For i = 1 To 24
        If –егистр = 1003 Then
            a(i) = ƒата
          ElseIf –егистр = 1002 Then
            a(i) = i
          Else
            a(i) = 0
          End If
      Next i
    ѕустой„асовой1 = a
End Function


