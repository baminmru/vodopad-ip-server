Attribute VB_Name = "P_ѕараметры_Q"
Option Explicit

Const ћес€цы»ѕ = "€нварь    февраль   март      апрель    май       июнь      июль      август    сент€брь  окт€брь   но€брь    декабрь"
Const ћес€цы–ѕ = "€нвар€    феврал€   марта     апрел€    ма€       июн€      июл€      августа   сент€бр€  окт€бр€   но€бр€    декабр€"
'                 12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
'   *******************
'   *** ѕј–јћ≈“–џ Q ***
'   *******************
Public Function „итатьѕараметр_Q(ByVal Ќомер As Long) As Variant
Dim Code As Integer
    „итатьѕараметр_Q = ""
' ќт 1 до 19
    If Ќомер < 20 Then
    Select Case Ќомер
      Case 1   ' ћодель счЄтчика
        „итатьѕараметр_Q = —чЄтчикћодель
      Case 2   ' Ќомер отчЄта
        „итатьѕараметр_Q = —чЄтчикЌомер
      Case 3    ' “ип архива
        „итатьѕараметр_Q = “ипјрхива
      Case 4   ' “ип периода
        „итатьѕараметр_Q = “ипѕериода
      Case 5   ' ќтчЄтный период, YYMM
        „итатьѕараметр_Q = ќтчЄт«а
      Case 6   ' ƒата сн€ти€, YYMMDD
        „итатьѕараметр_Q = ќтчЄтƒата
      Case 7   ' ¬рем€ сн€ти€, HHMMSS
        „итатьѕараметр_Q = ќтчЄт¬рем€
      Case 8   ' Ќижн€€ граница, YYMMDD
        „итатьѕараметр_Q = ќтчЄтќт
      Case 9   ' ¬ерхн€€ граница, , YYMMDD
        „итатьѕараметр_Q = ќтчЄтƒо
            
      Case 10   ' ћнемокод узла
        „итатьѕараметр_Q = —чЄтчик”зел
      Case 11   ' Ќазвание отчЄта
        „итатьѕараметр_Q = ќтчЄтЌазвание
      Case 12   ' »м€ файла отчЄта
        „итатьѕараметр_Q = ќтчЄт‘айл
      Case 16   ' »м€ файла отчЄта
        „итатьѕараметр_Q = YYMMDD2Date(ќтчЄтƒата) + HHMMSS2Time(ќтчЄт¬рем€)
      End Select
    Exit Function
    End If
      
    If Ќомер < 100 Then
    Code = Ќомер Mod 10
    Ќомер = Ќомер \ 10
    Select Case Ќомер
      Case 5   ' ѕериод (формат мес-YY)
        If Code = 0 Then
            „итатьѕараметр_Q = ¬ывестиѕериод(ќтчЄт«а)
          Else
            „итатьѕараметр_Q =  алендарна€≈диница(ќтчЄт«а * 100, Code)
          End If
      Case 6  ' ƒата сн€ти€ (формат Date)
        If Code = 0 Then
            „итатьѕараметр_Q = YYMMDD2Date(ќтчЄтƒата)
          Else
            „итатьѕараметр_Q =  алендарна€≈диница(ќтчЄтƒата, Code)
          End If
      Case 7   ' ¬рем€ сн€ти€ (формат Date)
        If Code = 0 Then
            „итатьѕараметр_Q = HHMMSS2Time(ќтчЄт¬рем€)
          End If
      Case 8   ' Ќижн€€ граница
        If Code = 0 Then
            „итатьѕараметр_Q = ¬ывести√раницу(ќтчЄтќт)
          Else
            „итатьѕараметр_Q =  алендарна€≈диница(ќтчЄтќт, Code)
          End If
      Case 9   ' ¬ерхн€€ граница
        If Code = 0 Then
            „итатьѕараметр_Q = ¬ывести√раницу(ќтчЄтƒо)
          Else
            „итатьѕараметр_Q =  алендарна€≈диница(ќтчЄтƒо, Code)
          End If
      End Select
    End If
End Function

Private Function  алендарна€≈диница(ByVal YYMMDD As Variant, Code As Integer)
Dim YY As Integer, MM As Integer, DD As Integer
     алендарна€≈диница = ""
        If NoData(YYMMDD) Then Exit Function
        If Not IsNumeric(YYMMDD) Then Exit Function
    DD = YYMMDD Mod 100
    YY = YYMMDD \ 100
    MM = YY Mod 100
    YY = YY \ 100
    Select Case Code
      Case 1    ' ƒата "DD"
         алендарна€≈диница = „ислоƒве÷ифры(DD)
      Case 4    ' ћес€ц "MM"
         алендарна€≈диница = „ислоƒве÷ифры(MM)
      Case 5    ' ћес€ц "MMM"
         алендарна€≈диница = ћес€цћћћ(MM)
      Case 6    ' ћес€ц "MMMM", им. падеж
         алендарна€≈диница = ћес€цћћћћ_»ѕ(MM)
      Case 7    ' ћес€ц "MMMM", род. падеж
         алендарна€≈диница = ћес€цћћћћ_–ѕ(MM)
      Case 8    ' √од "√√"
         алендарна€≈диница = „ислоƒве÷ифры(YY)
      Case 9    ' √од "√√√√"
         алендарна€≈диница = √од√√√√(YY)
      End Select
End Function

' ***** ¬спомогательные функции *****
Private Function „ислоƒве÷ифры(ByVal n As Integer) As String
' Ќомер мес€ца в виде "07"
    „ислоƒве÷ифры = Format$(n \ 100, "00")
End Function

Private Function ћес€цћћћ(ByVal n As Integer) As String
'  ороткое 3-буквенное название мес€ца: "€нв", "сен", ....
    ћес€цћћћ = MM2Str(n)
End Function

Private Function ћес€цћћћћ_»ѕ(ByVal n As Integer) As String
' ѕолное название мес€ца в именительном падеже
    ћес€цћћћћ_»ѕ = ""
        If n <= 0 Then Exit Function
        If n > 12 Then Exit Function
    ћес€цћћћћ_»ѕ = Trim(Mid(ћес€цы»ѕ, (n - 1) * 10 + 1, 10))
End Function

Private Function ћес€цћћћћ_–ѕ(ByVal n As Integer) As String
' ѕолное название мес€ца в родительном падеже
    ћес€цћћћћ_–ѕ = ""
        If n <= 0 Then Exit Function
        If n > 12 Then Exit Function
    ћес€цћћћћ_–ѕ = Trim(Mid(ћес€цы–ѕ, (n - 1) * 10 + 1, 10))
End Function

Private Function √од√√√√(ByVal YY As Integer) As String
' дополн€ет 2-цифровую запись года до полного года
    √од√√√√ = CStr((Year(Date) \ 100) * 100 + (YY Mod 100))
End Function


