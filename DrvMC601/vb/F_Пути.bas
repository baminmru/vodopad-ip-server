Attribute VB_Name = "F_����"
Option Explicit

    Global Const ����_���� = "����"
    Global Const ����_������ = "������"
    Global Const ����_������� = "�������"
    Global Const ����_������� = "�������"
    Global Const ����_������ = "������"
    Global Const ����_IE = "C:\Program Files\Internet Explorer\IEXPLORE.EXE"

'   *****************
'   ***  � � � �  ***
'   *****************
Public Function ����������(ByVal ���� As String) As String
' ���������� ���� � ����������� �����.
' �������� -- ���� � ����� ������������ ���� (����� ����� ����).
Dim ���� As String, ����� As String
    ���� = Application.ActiveWorkbook.Path
    Select Case ����
      Case ����_����
        ���������� = ����
        Exit Function
      Case ����_������
        ����� = ����_������
      Case ����_�������
        ����� = ����_�������
      Case ����_�������
        ����� = ����_�������
      Case Else
        ����� = ����
      End Select
    ���������� = �������������(�����, ����)
End Function

Public Function �������������(ByVal ���� As String, ByVal ���� As String) As String
' ��������� ������ ����. ���� ���� -- �������������, ��������� ����
    If ���� = "" Then Exit Function
    If (InStr(1, ����, ":") = 0) And (InStr(1, ����, "\\") = 0) Then
        If ���� <> "" Then
            ������������� = ���� & "\" & ����
          Else
            ������������� = ����
          End If
      End If
End Function

Public Function �������������(ByVal ���� As String, ByVal ���� As String) As String
' ���� ���� �������� � ���� ������� �����, �� ������� �
' � ��������� ������ "������������� �����"
    ������������� = ����
    If ���� <> "" Then
        If (Left(����, Len(����)) = ����) Then
            ������������� = Mid(����, Len(���� & "\") + 1)
          End If
      End If
End Function


'   *******************
'   ***  � � � � �  ***
'   *******************
Public Function �������������() As String
' ����� xls-����� �� ����������� �����
Dim s As String
    s = ����������(����_�������)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "���� � �������"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "��� �����", "*.*"
    .Filters.Add "������� �������", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Function
    ������������� = �������������(.SelectedItems.Item(1), s)
  End With
End Function

Public Function ��������������() As String
' ����� xls-����� �� ����������� �����
Dim s As String
    s = ����������(����_�������)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "���� � ����� �������"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "��� �����", "*.*"
    .Filters.Add "����� ��������", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Function
    �������������� = �������������(.SelectedItems.Item(1), s)
  End With
End Function

Public Function �����������(������� As String) As String
' ����� xls-����� �� ����������� �����
Dim s As String
    s = ����������(�������)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "���� � �����"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "��� �����", "*.*"
    .Filters.Add "����� Excel", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Function
    ����������� = �������������(.SelectedItems.Item(1), s)
  End With
End Function


Public Function ����������(���� As String, ������� As String) As String
' ���������� ������ ���� � �����. ���� -- ���� � �����. ��� -- ���� �� �����.
Dim s As String
    If ���� = "" Then
          MsgBox "�� ������ ����!", vbOKOnly, "������!"
          Exit Function
        End If
    s = �������������(����, ����������(�������))
    If Dir(s) = "" Then
        MsgBox "���� �� ������! ��������� ��� �����" & vbCrLf & _
        vbCrLf & "��� �������� ��� ������!", _
        vbOKOnly, "������!"
        Exit Function
      End If
    ���������� = s
End Function

Public Function �����������(���� As String, ������� As String, ������ As Boolean) As Integer
' ��������� ���� ���������� ���� (��� ���������� ����������� �����)
' ��� ����� ���������
Dim MyExl As Excel.Application
Dim MyWbk As Excel.Workbook
Dim s As String
    
    If ���� = "" Then
          MsgBox "�� ������ ����!", vbOKOnly, "������!"
          Exit Function
        End If
    s = �������������(����, ����������(�������))
    If Dir(s) = "" Then
        MsgBox "���� �� ������! ��������� ����" & vbCrLf & _
        vbCrLf & "��� ���������� ����� ���� ��������������.", _
        vbOKOnly, "������!"
          Exit Function
        End If
        
    On Error Resume Next
    Set MyExl = New Excel.Application
    If Err <> 0 Then
          MsgBox "���������� ��������� Microsoft Excel!" & vbCrLf & _
            "���������� ��������� Excel �� �������� ����." & vbCrLf & _
            "� ������ ���������� ������ ���������� � �����������.", _
            vbOKOnly, "������!"
          Exit Function
        End If
    Set MyWbk = MyExl.Workbooks.Open(s, , Not ������)
    If Err <> 0 Then
          MsgBox "�� ������ ������� ����." & vbCrLf & _
            "���������, ��� ���� ��������� �" & vbCrLf & _
            "��������� �� ���������� ����.", _
            vbOKOnly, "������!"
          MyExl.Quit
          Exit Function
        End If
    MyExl.Visible = True
End Function

Public Sub �������������������(������� As String, ������ As Boolean)
' ����� xls-����� �� ����������� �����
Dim MyExl As Excel.Application
Dim MyWbk As Excel.Workbook
Dim s As String
    s = ����������(�������)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "����� �����"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "��� �����", "*.*"
    .Filters.Add "����� Excel", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Sub
    s = .SelectedItems.Item(1)
  End With
  
    On Error Resume Next
    Set MyExl = New Excel.Application
    Set MyWbk = MyExl.Workbooks.Open(s, , Not ������)
    If Err <> 0 Then
          MsgBox "�� ������ ������� ����.", vbOKOnly, "������!"
          MyExl.Quit
          Exit Sub
        End If
    MyExl.Visible = True
End Sub


Public Sub ������������(����� As String)
Dim s As String
    If ����� = "" Then
        MsgBox "����� �� �������!", vbOKOnly, "��������!"
        Exit Sub
      End If
    s = ����������(�����)
    If s = "" Then
        MsgBox "��������� ������������ �������" & Chr(10) _
        & "���� � ����� ""������""", vbOKOnly, "��������!"
        Exit Sub
      End If
    On Error Resume Next
    Shell ����_IE & " " & s
'    Shell ����_IE & " " & s, vbNormalFocus
    If Err() <> 0 Then
        MsgBox "�� ������� ������� �����!" & Chr(10) _
        & "��������� ���� � Internet Explorer'�", vbOKOnly, "��������!"
      End If
End Sub

