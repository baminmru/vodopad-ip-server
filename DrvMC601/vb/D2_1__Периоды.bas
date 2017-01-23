Attribute VB_Name = "D2_1__ѕериоды"
Option Explicit


    Const ћес€цы = "€нв фев мар апр май июн июл авг сен окт но€ дек"
    Const ѕериоды = "«а мес€ц  «а год    "
    Const јрхивы = "„асовой   —уточный   ћес€чный √одовой   "
'                  "1234567890123456789012345678901234567890"

Public Function ¬ывести“ипѕериода(ByVal “ипѕериода As Variant) As Variant
    Select Case “ипѕериода
      Case 1
        ¬ывести“ипѕериода = "«а мес€ц"
      Case 2
        ¬ывести“ипѕериода = "«а год"
      Case Else
        ¬ывести“ипѕериода = Null
      End Select
End Function

Public Function ¬ывести“ипјрхива(ByVal “ипјрхива As Variant) As Variant
    ¬ывести“ипјрхива = Null
        If NoData(“ипјрхива) Then Exit Function
    If IsNumeric(“ипјрхива) Then
        If (“ипјрхива > 4) And (“ипјрхива <= 0) Then Exit Function
      End If
     ¬ывести“ипјрхива = Mid(јрхивы, (“ипјрхива - 1) * 10 + 1, 10)
End Function


'   ***************************
'   ***  онтроль диапазонов ***
'   ***************************
Public Function BadDate(ByVal X) As Boolean
' ѕровер€ет правильность дат
    BadDate = False
    If IsNull(X) Then Exit Function
    If IsNumeric(X) Then
        If (X > 0) And (X < 31) Then Exit Function
      End If
    MsgBox "ƒолжно быть число от 1 до 31", vbOKOnly, "ќшибка!"
    BadDate = True
End Function

Public Function BadMonth(ByVal X) As Boolean
' ѕровер€ет правильность дат
    BadMonth = False
    If IsNull(X) Then Exit Function
    If IsNumeric(X) Then
        If (X > 0) And (X <= 12) Then Exit Function
      End If
    MsgBox "ƒолжно быть число от 1 до 12", vbOKOnly, "ќшибка!"
    BadMonth = True
End Function

Public Function BadYear(ByVal X) As Boolean
' ѕровер€ет правильность дат
    BadYear = False
    If IsNull(X) Then Exit Function
    If IsNumeric(X) Then
    If (X > 0) And (X <= 2320) Then
        If (X Mod 1000) <= 320 Then Exit Function
      End If
      End If
    MsgBox "Ќеправильно указан год!", vbOKOnly, "ќшибка!"
    BadYear = True
End Function

'   *****************
'   *** ‘ќ–ћј“ MM ***
'   *****************
' »спользуетс€ дл€ кодирование мес€цев
' »спользуетс€ в следующих процедурах
'
Public Function MM2Str(ByVal MM As Integer) As String
' ¬озвращает название мес€ца или квартала: "€нв", "фев", ...
    MM2Str = ""
        If MM <= 0 Then Exit Function
        If MM > 12 Then Exit Function
    MM2Str = Mid(ћес€цы, (MM - 1) * 4 + 1, 3)
End Function

Public Function Str2MM(s As String) As Integer
' –аспознаЄт периоды по написанию или по номерам
' ¬ случае ошибки возвращает 0.
Dim i As Integer
    i = InStr(1, ћес€цы, Mid(s, 1, 3))
    If i <> 0 Then
        i = i \ 4 + 1
      Else
        i = 0
        On Error Resume Next
        i = CInt(s)
        If (i <= 0) Or (i > 12) Then i = 0
        If CStr(i) <> s Then i = 0      ' «ащита от дробного числа
        If Err() <> 0 Then i = 0
      End If
    Str2MM = i
End Function


'   *******************
'   *** ‘ќ–ћј“ YYMM ***
'   *******************
'    »спользуетс€ дл€ кодирование полных отчЄтных периодов (мес€ц и/или год),
' ¬нешнее представление дл€ мес€цев: "€нв-09", "авг-08"
' ƒл€ годовых периодов -- просто год 2001, 2002 и т.д.
' ¬нутренне представление:
'   YY -- год минус 2000, MM -- номер мес€ца
'   ≈сли YY00 -- то просто год, ≈сли 00MM -- просто мес€ц
'
Public Function ¬ывестиѕериод( од)
    If Not IsNull( од) Then ¬ывестиѕериод = YYMM2Str( од)
End Function
    
Public Function —читатьѕериод(¬ид, “ипѕериода)
    If Not IsNull(¬ид) And Not IsNull(“ипѕериода) Then —читатьѕериод = Str2YYMM(¬ид, “ипѕериода)
End Function
    
Public Function YYMM2Str(ByVal YYMM As Integer) As String
' ¬озвращает внешнее представление периода
    YYMM2Str = ""
        If YYMM <= 0 Then Exit Function
    If YYMM Mod 100 = 0 Then
        YYMM2Str = CStr(2000 + (YYMM \ 100))
      Else
        If YYMM \ 100 = 0 Then
            YYMM2Str = MM2Str(YYMM Mod 100) ' если YY = 0, только мес€ц
          Else
            YYMM2Str = MM2Str(YYMM Mod 100) & "-" & Format$(YYMM \ 100, "00")
'            YYMM2Str = Format$(YYMM Mod 100, "00") & "." & CStr(2000 + YYMM \ 100)
          End If
      End If
End Function

Public Function Str2YYMM(ByVal s As String, ByVal “ипѕериода As Integer) As Integer
' –аспознаЄт полные периоды.
' " ушает" записи вида "€нв-08", "€нв 08", "€нв08", "€нв 2008" и даже "01.08"
' ≈сли не удаЄтс€ распознать, возвращает 0.
Dim MM As Long, YY As Long
Dim i As Integer
    On Error Resume Next
    MM = 0
    YY = 0
    Select Case “ипѕериода
      Case 1    ' ћес€цы
        s = Trim(s)
        MM = Str2MM(s)  '
        If MM <> 0 Then
        ' –аспознан мес€ц
            If InStr(1, s, "-") = 0 Then
                s = Trim(Mid(s, 4))
              Else
                s = Trim(Mid(s, 5))
              End If
          Else
        ' ¬озможно, это запись MM.YY
            i = InStr(1, s, ".")
            If i <> 0 Then
                MM = Str2MM(Mid(s, 1, i - 1))
                s = Mid(s, i + 1)
              End If
          End If
        If MM = 0 Then Exit Function
        If Len(s) <> 0 Then YY = CInt(s)
        If YY = 0 Then YY = Year(Date) - 2000
      Case 2    ' √оды
        i = InStr(1, s, ".")
        If i <> 0 Then
            MM = Str2MM(Mid(s, 1, i - 1))
            If MM <> 0 Then YY = CInt(Mid(s, i + 1))
          Else
            YY = CInt(s)
            If YY <> s Then YY = 0
          End If
        MM = 0
      End Select
      
    If Err() <> 0 Then Exit Function
    If YY < 0 Then Exit Function
    YY = YY Mod 1000
    If YY > 320 Then Exit Function
    Str2YYMM = 100 * YY + MM
End Function


'   ******************
'   *** ‘ќ–ћј“ MDD ***
'   ******************
' »спользуетс€ дл€ заданий плановых дней (относительно начала периода)
'  одировка: DD -- день мес€ца (1..28)
'             M -- мес€ц от начала периода
' ѕредставление:
'   ћес€чные
'       "28"        -- 28-го числа отчЄтного мес€ца (M = 1)
'       "05 с.м."   -- 5-го числа следующего мес€ца (M = 2)
'   √одовые
'       "15.12"     -- 15 декар€ текущего года      (M = 1..12)
'       "15.01 с.г."-- 15 €нвар€ следующего года    (M = 13..24)
'
Public Function ¬ывестиƒень( од, “ипѕериода)
    If Not NoData( од) And Not NoData(“ипѕериода) Then ¬ывестиƒень = MDD2Str( од, “ипѕериода)
End Function
    
Public Function —читатьƒень(¬ид, “ипѕериода)
    If Not NoData(¬ид) And Not NoData(“ипѕериода) Then —читатьƒень = Str2MDD(¬ид, “ипѕериода)
End Function
    
Public Function MDD2Str(ByVal MDD As Integer, ByVal “ипѕериода As Integer) As String
'  онвертор MDD в строчное представление
Dim DD As Integer
Dim s As String
    s = Format$(MDD Mod 100, "00")
    Select Case “ипѕериода
      Case 1    ' ћес€чные отчЄты
        If (MDD \ 100) = 1 Then
            MDD2Str = s
          ElseIf (MDD \ 100) = 2 Then
            MDD2Str = s & " с.м."
          Else
            MDD2Str = "ќшибка"
          End If
      Case 2    ' √одовые отчЄты
        If (MDD \ 100) < 13 Then
            MDD2Str = s & "." & Format$((MDD \ 100), "00")
          ElseIf (MDD \ 100) <= 24 Then
            MDD2Str = s & "." & Format$((MDD \ 100) - 12, "00") & " с.г."
          Else
            MDD2Str = "ќшибка"
          End If
      End Select
End Function

Public Function Str2MDD(ByVal s As String, ByVal “ипѕериода As Integer) As Integer
' ѕреобразует полное представление плановых дней во внутренний формат MDD.
' ѕри ошибке возвращает 0.
Dim i As Integer, d As Integer, m As Integer
Dim Q As String
    Str2MDD = 0
    Q = Trim(s)
    m = 0
    On Error Resume Next
    Select Case “ипѕериода
      Case 1    ' ћес€чные отчЄты
        ' »щем "с.м."
        i = InStr(1, Q, "с.м.")
        If i = 0 Then i = InStr(1, Q, "см")
        If i <> 0 Then
            Q = Trim(Mid(Q, 1, i - 1))
            m = 1
          End If
        If Not IsNumeric(Q) Then Exit Function
        d = CInt(Q)
        m = m + 1
      Case 2    ' √одовые отчЄты
        ' »щем "с.г."
        i = InStr(1, Q, "с.г.")
        If i = 0 Then i = InStr(1, Q, "сг")
        If i <> 0 Then
            Q = Trim(Mid(Q, 1, i - 1))
            m = 12
          End If
        i = InStr(1, Q, ".")
            If i = 0 Then Exit Function
        If Not IsNumeric(Trim(Mid(Q, 1, i - 1))) Then Exit Function
        If Not IsNumeric(Trim(Mid(Q, i + 1))) Then Exit Function
        d = CInt(Trim(Mid(Q, 1, i - 1)))
        m = m + CInt(Trim(Mid(Q, i + 1)))
      End Select
      
    If Err() <> 0 Then Exit Function
    If d > 31 Then Exit Function
    If m > 24 Then Exit Function
    Str2MDD = m * 100 + d
End Function

'   *********************
'   *** ‘ќ–ћј“ YYMMDD ***
'   *********************
' «адание "точек отсчЄта" -- моментов начала и конца считывани€.
'   ƒл€ суточных архивов -- обычные даты:   YYMMDD
'   ƒл€ мес€чных архивов -- мес€ца и годы:  YYMM00
'   ƒл€ годовых архивов -- просто годы:     YY0000
'
Public Function ¬ывести√раницу( од)
    If Not IsNull( од) Then ¬ывести√раницу = YYMMDD2Str( од)
End Function
    
Public Function —читать√раницу(¬ид, “ипјрхива)
    If Not IsNull(¬ид) And Not IsNull(“ипјрхива) Then —читать√раницу = Str2YYMMDD(¬ид, “ипјрхива)
End Function
    
Public Function YYMMDD2Str(ByVal YYMMDD As Long) As String
'  онвертор YYMMDD в строчное представление
Dim DD As Long, YM As Long

    YM = YYMMDD \ 100
    DD = YYMMDD Mod 100
    If DD <> 0 Then
    ' ѕросто даты
        YYMMDD2Str = Format$(DateSerial(2000 + YM \ 100, YM Mod 100, DD), "dd\.mm\.yyyy")
      ElseIf (YM Mod 100) <> 0 Then
    ' мес€ца и годы
        YYMMDD2Str = YYMM2Str(YM)
'        YYMMDD2Str = Mid(Format$(DateSerial(YM \ 100, YM Mod 100, 1), "dd\.mm\.yy"), 4)
      Else
        YYMMDD2Str = CStr(2000 + (YM \ 100))
      End If
End Function

Public Function Str2YYMMDD(ByVal s As String, ByVal “ипјрхива As Integer) As Long
' —читывает границы
Dim l As Long
Dim d As Date
    On Error Resume Next
    Str2YYMMDD = 0
    Select Case “ипјрхива
      Case 1, 2    ' ƒни
        d = CDate(s)
        If (Year(d) < 2000) Then Exit Function
        Str2YYMMDD = ((Year(d) - 2000) * 100 + Month(d)) * 100 + Day(d)
      Case 3    ' ћес€цы
        l = Str2YYMM(s, 1)
        If l <> 0 Then Str2YYMMDD = 100 * l
      Case 4    ' √оды
        l = Str2YYMM(s, 2)
        If l <> 0 Then Str2YYMMDD = 100 * l
      End Select
    If Err() <> 0 Then Str2YYMMDD = 0
End Function

Public Function Date2YYMMDD(ByVal d As Variant, ByVal “ипјрхива As Variant) As Variant
' ѕо дате вычисл€ет внутреннее представление
        Date2YYMMDD = Null
    If NoData(d) Then Exit Function
    Select Case “ипјрхива
      Case 1, 2 ' ƒни
        Date2YYMMDD = ((Year(d) - 2000) * 100 + Month(d)) * 100 + Day(d)
      Case 3    ' ћес€цы
        Date2YYMMDD = ((Year(d) - 2000) * 100 + Month(d)) * 100
      Case 4    ' √оды
        Date2YYMMDD = (Year(d) - 2000) * 100 * 100
      End Select
End Function

Public Function YYMMDD2Date(ByVal YYMMDD As Variant) As Variant
' ѕо дате вычисл€ет внутреннее представление
Dim DD As Long, YM As Long
    YYMMDD2Date = Null
        If NoData(YYMMDD) Then Exit Function
    YM = YYMMDD \ 100
    DD = YYMMDD Mod 100
    If DD <> 0 Then ' ѕросто даты
        YYMMDD2Date = DateSerial(2000 + YM \ 100, YM Mod 100, DD)
      ElseIf (YM Mod 100) <> 0 Then ' ћес€цы и годы
        YYMMDD2Date = DateSerial(2000 + YM \ 100, YM Mod 100, 1)
      Else ' √оды
        YYMMDD2Date = DateSerial(2000 + YM \ 100, 1, 1)
      End If
End Function

'   *****************************
'   *** ‘ќ–ћј“џ HHMM и HHMMSS ***
'   *****************************
'   ƒес€тично-кодированной представление времени
'
Public Function HHMM2Time(ByVal HHMM As Variant) As Variant
    HHMM2Time = Null
        If NoData(HHMM) Then Exit Function
    HHMM2Time = TimeSerial(HHMM \ 100, HHMM Mod 100, 0)
End Function

Public Function Time2HHMM(ByVal T As Variant) As Variant
    Time2HHMM = Null
        If NoData(T) Then Exit Function
    Time2HHMM = Hour(T) * 100 + Minute(T)
End Function

Public Function HHMMSS2Time(ByVal HHMMSS As Variant) As Variant
Dim HHMM As Long
    HHMMSS2Time = Null
        If NoData(HHMMSS) Then Exit Function
    HHMM = HHMMSS \ 100
    HHMMSS2Time = TimeSerial(HHMM \ 100, HHMM Mod 100, HHMMSS Mod 100)
End Function

Public Function Time2HHMMSS(ByVal T As Variant) As Variant
    Time2HHMMSS = Null
        If NoData(T) Then Exit Function
    Time2HHMMSS = (Hour(T) * 100 + Minute(T)) * 100 + Second(T)
End Function


