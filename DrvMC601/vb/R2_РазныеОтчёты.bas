Attribute VB_Name = "R2_������������"
Option Explicit
Dim Exl As Excel.Application
Dim Wbk As Excel.Workbook
Dim Wrs As Excel.Worksheet

'   *************************
'   *** ���������� ������ ***
'   *************************
'
Public Sub ����������������()
    ��������� = MC601_��������������(1003)
        If ��������� = 0 Then ��������� = Date2YYMMDD(Date, 1)
    ���������� = MC601_��������������(1002)
        If ���������� = 0 Then ���������� = Time2HHMMSS(Time())
End Sub


Public Function �����������(������ As String, ��������� As String)
' ��������� ����� �����. ������ -- ��� ������, ��������� -- ��� ��� ������ ������.
Dim s As String
'    If ��������� = "" Then
'          MsgBox "�� ������� ��� ����� ������!", vbOKOnly, "������!"
'          Exit Function
'        End If
'    s = �������������(��������� & ".xls", ����������(����_������))
'    If Dir(s) <> "" Then
'        MsgBox "����� � ����� ������ ��� ����!" & vbCrLf & _
'            "�������� ��� ������ ��� ������� ������" & vbCrLf & _
'            "� ��������� ��������!", _
'            vbOKOnly, "������!"
'        Exit Function
'      End If

    s = ����������(������, ����_�������)
        If s = "" Then Exit Function
    On Error Resume Next
    Set Exl = New Excel.Application
    If Err <> 0 Then
          MsgBox "���������� ��������� Microsoft Excel!" & vbCrLf & _
            "���������� ��������� Excel �� �������� ����." & vbCrLf & _
            "� ������ ���������� ������ ���������� � �����������.", _
            vbOKOnly, "������!"
          Exit Function
        End If
        Set Wbk = Exl.Workbooks.Add(s)
    If Err <> 0 Then
          MsgBox "�� ������ ������� �����!" & vbCrLf & _
            "���������, ��� ������ �������" & vbCrLf & _
            "� �� ��������.", _
            vbOKOnly, "������!"
        Wbk.Close
        Exl.Quit
        Exit Function
      End If
    Wbk.SaveAs FileName:=�������������(���������, ����������(����_������))
'    Exl.Visible = True
End Function

Public Sub ��������������()
    Exl.Visible = True
    On Error Resume Next
    Set Wrs = Nothing
    Set Exl = Nothing
    Set Wbk = Nothing
End Sub


'   *************************
'   *** ���������� ��ר�� ***
'   *************************
Public Sub ������������(������ As String, ��������� As String, ByVal ������, ByVal �����)
    ���������������
    ����������� ������, ���������
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ����������������
    ������������������� Wrs, Sect_All, Subs_All
1:  ��������������
    ���������������
    ����������������
End Sub

Public Sub ����������101(������ As String, ��������� As String, ByVal ������, ByVal �����)
' *** ��� 101 *** ��������� ����� �� ����� ������ �� ������. 3-� ���������� �����.
Dim i As Integer
        If NoData(�����) Or (����� = 0) Then Exit Sub
        If NoData(������) Then Exit Sub
  ' ���������� �����
    ���������������
    ����������� ������, ���������
    ' ��������
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    ������������� Wrs
    ������������� Wrs
    ������������������������� Wrs, �����
    ' ������
    ������������� 0.03
    Set Wrs = Wbk.Worksheets(3)
    ���������������� Wrs
    ������������������������� Wrs, �����
    ' �����
    ������������� 0.04
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ������������������������ Wrs, �����
    
  ' ���������� ������
    Set Wrs = Wbk.Worksheets(2)
    ������������������ kTab * �����, 0.05, 0.95
    ���������������� Wrs
    '   *****************************************
    If �����������������(Wrs, Date - ������, �����) <> 0 Then GoTo 1
    '   *****************************************
    ������������� 0.95
    ����������������
    ������������������� Wrs, Sect_Inf, Subs_All
    ������������������� Wrs, Sect_Cur, Subs_All
    
    ' ���������� ������
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ������������������� Wrs, Sect_Hdr, Subs_All
    ������������������� Wrs, Sect_Sum, Subs_All

    ' ����������
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    If �������� Then ����������������� Wrs

  ' �������� ������ (� ����)
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ������������� 0.97
    ���������������� Wrs, Sect_End
    ������������� Wrs, Sect_Mem
    ���������������� Wrs, Sect_Sum
    ���������������� Wrs, Sect_Tbl
    ���������������� Wrs, Sect_Hdr
    ������������� Wrs, Sect_Inf
    �������������������� Wrs

1:
    ���������������
    ��������������
    ����������������
End Sub
'

Public Sub ����������201(������ As String, ��������� As String, ByVal ������, ByVal �����)
' *** ��� 201 *** ���������� ����� �� �����. 2-� ���������� �����.
        If NoData(�����) Or (����� = 0) Then Exit Sub
        If NoData(������) Then Exit Sub
    ���������������
    ����������� ������, ���������
    ' ���������� ���������
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    ������������� Wrs
    ������������������������� Wrs, �����
    
    Set Wrs = Wbk.Worksheets(1)
     ������������� 0.05
    ���������������� Wrs
    ������������������������ Wrs, �����

    ' ���������� ������
    ������������������ kTab * �����, 0.1, 0.9
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    '   *****************************************
        If ������������������(Wrs, ������, �����) <> 0 Then GoTo 1
    '   *****************************************
    ����������������
    ������������������� Wrs, Sect_Inf, Subs_All
    ������������������� Wrs, Sect_Cur, Subs_All
    
    ' ���������� ������
    ������������� 0.95
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ������������������� Wrs, Sect_Hdr, Subs_All
    ������������������� Wrs, Sect_Sum, Subs_All

    ' ����������
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    If �������� Then ����������������� Wrs

    ' �������� ������ (� ����)
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ���������������� Wrs, Sect_End
    ������������� Wrs, Sect_Mem
    ���������������� Wrs, Sect_Sum
    ���������������� Wrs, Sect_Tbl
    ���������������� Wrs, Sect_Hdr
    ������������� Wrs, Sect_Inf
    �������������������� Wrs
    
1:  ��������������
    ���������������
    ����������������
End Sub

Public Sub ����������301(������ As String, ��������� As String, ByVal ������, ByVal �����)
' *** ��� 301 *** ���������� ����� �� ���. 2-� ���������� �����.
' ����������� �� ���� ������ ������ -- �� �������������� ������, �.�. ��������� ��������.
        If NoData(�����) Or (����� = 0) Then Exit Sub
        If NoData(������) Then Exit Sub
    ���������������
    ����������� ������, ���������
    ' ���������� ���������
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    ������������� Wrs
    ������������������������� Wrs, ����� + 1
    
    Set Wrs = Wbk.Worksheets(1)
     ������������� 0.05
    ���������������� Wrs
    ������������������������ Wrs, ����� + 1

    ' ���������� ������
    ������������������ kTab * (����� + 1), 0.1, 0.9
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    '   *****************************************
        If ������������������(Wrs, ������, ����� + 1) <> 0 Then GoTo 1
    '   *****************************************
    ����������������
    ������������������� Wrs, Sect_Inf, Subs_All
    ������������������� Wrs, Sect_Cur, Subs_All
    
    ' ��������� ������
    ������������� 0.95
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ������������������� Wrs, Sect_Hdr, Subs_All
    ������������������� Wrs, Sect_Sum, Subs_All

    ' ����������
    Set Wrs = Wbk.Worksheets(2)
    ���������������� Wrs
    If �������� Then ����������������� Wrs

    ' �������� ������ (� ����)
    Set Wrs = Wbk.Worksheets(1)
    ���������������� Wrs
    ���������������� Wrs, Sect_End
    ������������� Wrs, Sect_Mem
    ���������������� Wrs, Sect_Sum
    ������������������� Wrs, 1, Sect_Tbl
    ���������������� Wrs, Sect_Tbl
    ���������������� Wrs, Sect_Hdr
    ������������� Wrs, Sect_Inf
    �������������������� Wrs
    
1:  ��������������
    ���������������
    ����������������
End Sub

