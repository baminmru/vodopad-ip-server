Attribute VB_Name = "D2_1__�������"
Option Explicit


    Const ������ = "��� ��� ��� ��� ��� ��� ��� ��� ��� ��� ��� ���"
    Const ������� = "�� �����  �� ���    "
    Const ������ = "�������   ��������   �������� �������   "
'                  "1234567890123456789012345678901234567890"

Public Function �����������������(ByVal ���������� As Variant) As Variant
    Select Case ����������
      Case 1
        ����������������� = "�� �����"
      Case 2
        ����������������� = "�� ���"
      Case Else
        ����������������� = Null
      End Select
End Function

Public Function ����������������(ByVal ��������� As Variant) As Variant
    ���������������� = Null
        If NoData(���������) Then Exit Function
    If IsNumeric(���������) Then
        If (��������� > 4) And (��������� <= 0) Then Exit Function
      End If
     ���������������� = Mid(������, (��������� - 1) * 10 + 1, 10)
End Function


'   ***************************
'   *** �������� ���������� ***
'   ***************************
Public Function BadDate(ByVal X) As Boolean
' ��������� ������������ ���
    BadDate = False
    If IsNull(X) Then Exit Function
    If IsNumeric(X) Then
        If (X > 0) And (X < 31) Then Exit Function
      End If
    MsgBox "������ ���� ����� �� 1 �� 31", vbOKOnly, "������!"
    BadDate = True
End Function

Public Function BadMonth(ByVal X) As Boolean
' ��������� ������������ ���
    BadMonth = False
    If IsNull(X) Then Exit Function
    If IsNumeric(X) Then
        If (X > 0) And (X <= 12) Then Exit Function
      End If
    MsgBox "������ ���� ����� �� 1 �� 12", vbOKOnly, "������!"
    BadMonth = True
End Function

Public Function BadYear(ByVal X) As Boolean
' ��������� ������������ ���
    BadYear = False
    If IsNull(X) Then Exit Function
    If IsNumeric(X) Then
    If (X > 0) And (X <= 2320) Then
        If (X Mod 1000) <= 320 Then Exit Function
      End If
      End If
    MsgBox "����������� ������ ���!", vbOKOnly, "������!"
    BadYear = True
End Function

'   *****************
'   *** ������ MM ***
'   *****************
' ������������ ��� ����������� �������
' ������������ � ��������� ����������
'
Public Function MM2Str(ByVal MM As Integer) As String
' ���������� �������� ������ ��� ��������: "���", "���", ...
    MM2Str = ""
        If MM <= 0 Then Exit Function
        If MM > 12 Then Exit Function
    MM2Str = Mid(������, (MM - 1) * 4 + 1, 3)
End Function

Public Function Str2MM(s As String) As Integer
' ��������� ������� �� ��������� ��� �� �������
' � ������ ������ ���������� 0.
Dim i As Integer
    i = InStr(1, ������, Mid(s, 1, 3))
    If i <> 0 Then
        i = i \ 4 + 1
      Else
        i = 0
        On Error Resume Next
        i = CInt(s)
        If (i <= 0) Or (i > 12) Then i = 0
        If CStr(i) <> s Then i = 0      ' ������ �� �������� �����
        If Err() <> 0 Then i = 0
      End If
    Str2MM = i
End Function


'   *******************
'   *** ������ YYMM ***
'   *******************
'    ������������ ��� ����������� ������ �������� �������� (����� �/��� ���),
' ������� ������������� ��� �������: "���-09", "���-08"
' ��� ������� �������� -- ������ ��� 2001, 2002 � �.�.
' ��������� �������������:
'   YY -- ��� ����� 2000, MM -- ����� ������
'   ���� YY00 -- �� ������ ���, ���� 00MM -- ������ �����
'
Public Function �������������(���)
    If Not IsNull(���) Then ������������� = YYMM2Str(���)
End Function
    
Public Function �������������(���, ����������)
    If Not IsNull(���) And Not IsNull(����������) Then ������������� = Str2YYMM(���, ����������)
End Function
    
Public Function YYMM2Str(ByVal YYMM As Integer) As String
' ���������� ������� ������������� �������
    YYMM2Str = ""
        If YYMM <= 0 Then Exit Function
    If YYMM Mod 100 = 0 Then
        YYMM2Str = CStr(2000 + (YYMM \ 100))
      Else
        If YYMM \ 100 = 0 Then
            YYMM2Str = MM2Str(YYMM Mod 100) ' ���� YY = 0, ������ �����
          Else
            YYMM2Str = MM2Str(YYMM Mod 100) & "-" & Format$(YYMM \ 100, "00")
'            YYMM2Str = Format$(YYMM Mod 100, "00") & "." & CStr(2000 + YYMM \ 100)
          End If
      End If
End Function

Public Function Str2YYMM(ByVal s As String, ByVal ���������� As Integer) As Integer
' ��������� ������ �������.
' "������" ������ ���� "���-08", "��� 08", "���08", "��� 2008" � ���� "01.08"
' ���� �� ������ ����������, ���������� 0.
Dim MM As Long, YY As Long
Dim i As Integer
    On Error Resume Next
    MM = 0
    YY = 0
    Select Case ����������
      Case 1    ' ������
        s = Trim(s)
        MM = Str2MM(s)  '
        If MM <> 0 Then
        ' ��������� �����
            If InStr(1, s, "-") = 0 Then
                s = Trim(Mid(s, 4))
              Else
                s = Trim(Mid(s, 5))
              End If
          Else
        ' ��������, ��� ������ MM.YY
            i = InStr(1, s, ".")
            If i <> 0 Then
                MM = Str2MM(Mid(s, 1, i - 1))
                s = Mid(s, i + 1)
              End If
          End If
        If MM = 0 Then Exit Function
        If Len(s) <> 0 Then YY = CInt(s)
        If YY = 0 Then YY = Year(Date) - 2000
      Case 2    ' ����
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
'   *** ������ MDD ***
'   ******************
' ������������ ��� ������� �������� ���� (������������ ������ �������)
' ���������: DD -- ���� ������ (1..28)
'             M -- ����� �� ������ �������
' �������������:
'   ��������
'       "28"        -- 28-�� ����� ��������� ������ (M = 1)
'       "05 �.�."   -- 5-�� ����� ���������� ������ (M = 2)
'   �������
'       "15.12"     -- 15 ������ �������� ����      (M = 1..12)
'       "15.01 �.�."-- 15 ������ ���������� ����    (M = 13..24)
'
Public Function �����������(���, ����������)
    If Not NoData(���) And Not NoData(����������) Then ����������� = MDD2Str(���, ����������)
End Function
    
Public Function �����������(���, ����������)
    If Not NoData(���) And Not NoData(����������) Then ����������� = Str2MDD(���, ����������)
End Function
    
Public Function MDD2Str(ByVal MDD As Integer, ByVal ���������� As Integer) As String
' ��������� MDD � �������� �������������
Dim DD As Integer
Dim s As String
    s = Format$(MDD Mod 100, "00")
    Select Case ����������
      Case 1    ' �������� ������
        If (MDD \ 100) = 1 Then
            MDD2Str = s
          ElseIf (MDD \ 100) = 2 Then
            MDD2Str = s & " �.�."
          Else
            MDD2Str = "������"
          End If
      Case 2    ' ������� ������
        If (MDD \ 100) < 13 Then
            MDD2Str = s & "." & Format$((MDD \ 100), "00")
          ElseIf (MDD \ 100) <= 24 Then
            MDD2Str = s & "." & Format$((MDD \ 100) - 12, "00") & " �.�."
          Else
            MDD2Str = "������"
          End If
      End Select
End Function

Public Function Str2MDD(ByVal s As String, ByVal ���������� As Integer) As Integer
' ����������� ������ ������������� �������� ���� �� ���������� ������ MDD.
' ��� ������ ���������� 0.
Dim i As Integer, d As Integer, m As Integer
Dim Q As String
    Str2MDD = 0
    Q = Trim(s)
    m = 0
    On Error Resume Next
    Select Case ����������
      Case 1    ' �������� ������
        ' ���� "�.�."
        i = InStr(1, Q, "�.�.")
        If i = 0 Then i = InStr(1, Q, "��")
        If i <> 0 Then
            Q = Trim(Mid(Q, 1, i - 1))
            m = 1
          End If
        If Not IsNumeric(Q) Then Exit Function
        d = CInt(Q)
        m = m + 1
      Case 2    ' ������� ������
        ' ���� "�.�."
        i = InStr(1, Q, "�.�.")
        If i = 0 Then i = InStr(1, Q, "��")
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
'   *** ������ YYMMDD ***
'   *********************
' ������� "����� �������" -- �������� ������ � ����� ����������.
'   ��� �������� ������� -- ������� ����:   YYMMDD
'   ��� �������� ������� -- ������ � ����:  YYMM00
'   ��� ������� ������� -- ������ ����:     YY0000
'
Public Function ��������������(���)
    If Not IsNull(���) Then �������������� = YYMMDD2Str(���)
End Function
    
Public Function ��������������(���, ���������)
    If Not IsNull(���) And Not IsNull(���������) Then �������������� = Str2YYMMDD(���, ���������)
End Function
    
Public Function YYMMDD2Str(ByVal YYMMDD As Long) As String
' ��������� YYMMDD � �������� �������������
Dim DD As Long, YM As Long

    YM = YYMMDD \ 100
    DD = YYMMDD Mod 100
    If DD <> 0 Then
    ' ������ ����
        YYMMDD2Str = Format$(DateSerial(2000 + YM \ 100, YM Mod 100, DD), "dd\.mm\.yyyy")
      ElseIf (YM Mod 100) <> 0 Then
    ' ������ � ����
        YYMMDD2Str = YYMM2Str(YM)
'        YYMMDD2Str = Mid(Format$(DateSerial(YM \ 100, YM Mod 100, 1), "dd\.mm\.yy"), 4)
      Else
        YYMMDD2Str = CStr(2000 + (YM \ 100))
      End If
End Function

Public Function Str2YYMMDD(ByVal s As String, ByVal ��������� As Integer) As Long
' ��������� �������
Dim l As Long
Dim d As Date
    On Error Resume Next
    Str2YYMMDD = 0
    Select Case ���������
      Case 1, 2    ' ���
        d = CDate(s)
        If (Year(d) < 2000) Then Exit Function
        Str2YYMMDD = ((Year(d) - 2000) * 100 + Month(d)) * 100 + Day(d)
      Case 3    ' ������
        l = Str2YYMM(s, 1)
        If l <> 0 Then Str2YYMMDD = 100 * l
      Case 4    ' ����
        l = Str2YYMM(s, 2)
        If l <> 0 Then Str2YYMMDD = 100 * l
      End Select
    If Err() <> 0 Then Str2YYMMDD = 0
End Function

Public Function Date2YYMMDD(ByVal d As Variant, ByVal ��������� As Variant) As Variant
' �� ���� ��������� ���������� �������������
        Date2YYMMDD = Null
    If NoData(d) Then Exit Function
    Select Case ���������
      Case 1, 2 ' ���
        Date2YYMMDD = ((Year(d) - 2000) * 100 + Month(d)) * 100 + Day(d)
      Case 3    ' ������
        Date2YYMMDD = ((Year(d) - 2000) * 100 + Month(d)) * 100
      Case 4    ' ����
        Date2YYMMDD = (Year(d) - 2000) * 100 * 100
      End Select
End Function

Public Function YYMMDD2Date(ByVal YYMMDD As Variant) As Variant
' �� ���� ��������� ���������� �������������
Dim DD As Long, YM As Long
    YYMMDD2Date = Null
        If NoData(YYMMDD) Then Exit Function
    YM = YYMMDD \ 100
    DD = YYMMDD Mod 100
    If DD <> 0 Then ' ������ ����
        YYMMDD2Date = DateSerial(2000 + YM \ 100, YM Mod 100, DD)
      ElseIf (YM Mod 100) <> 0 Then ' ������ � ����
        YYMMDD2Date = DateSerial(2000 + YM \ 100, YM Mod 100, 1)
      Else ' ����
        YYMMDD2Date = DateSerial(2000 + YM \ 100, 1, 1)
      End If
End Function

'   *****************************
'   *** ������� HHMM � HHMMSS ***
'   *****************************
'   ���������-������������ ������������� �������
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


