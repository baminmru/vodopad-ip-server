VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} ����������� 
   Caption         =   "�����������"
   ClientHeight    =   1935
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   5175
   OleObjectBlob   =   "�����������.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "�����������"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Const cBase = &HFFC0C0
Const cFore = &HFFFF80
Dim ��� As Boolean
Dim ����� As String

Private Sub UserForm_Terminate()
    ��� = False                ' ������������� ���������
    �������������� = False     ' �������� �������
'    DoEvents
End Sub


' *** ���������� ����������� ***
Sub �����������������()
Dim i As Integer
    For i = 0 To 7
        Me("L" & CInt(i)).BackColor = cBase
      Next i
End Sub

Sub �������������������(ByVal i As Integer)
Static m As Integer
    Me("L" & CInt(m)).BackColor = cBase
    m = i Mod 8
    Me("L" & CInt(m)).BackColor = cFore
End Sub

Public Sub ���(�������� As String)
    ����� = ��������
End Sub

Public Sub ��������������������()
    Me![����������].Visible = True
    Me![����������].SetFocus
    Me![�����������].Visible = False
    Me![��������������] = "������� ����������"
    
    If �����������() Then
        �������
      End If
        
'    �������������� = False     ' �����������
  
  On Error Resume Next
    Me![�����������].Visible = True
    Me![�����������].SetFocus
    Me![����������].Visible = False
End Sub


Private Function �����������() As Boolean
Static T As Double
Dim i As Long

    ����������� = False
    T = Now()
    i = 0
    ��� = True
    Mdm_�������������� "ATD" & �����
    ���������������� 60
    ���������������

    Do                      ' ��� ���������� ������
        DoEvents
        DoPoll
        If Mdm_�����������() Then Exit Do
        ���������������
'        If ��� = False Then
'            ReceiverState = RS_UserEsc
'          End If
        If ����������� Then Exit Do
        If T <> Now() Then
            T = Now()
            ������������������� i
            i = i + 1
          End If
        Sleep 20
     Loop
    ����������������        ' ��������� �������� �� ����-�����
    
  On Error Resume Next      ' ������ �� �������� �����
    �����������������
    If Mdm_�����������() <> 0 Then
        ReceiverState = RS_BadData
      End If
      
' ����� ��������� ���������
    If ReceiverState = 0 Then
        Me![��������������] = ���������������(MdmKWord)
        If MdmKWord = AC_CT Then
            ����������� = True      ' �����������!
          End If
        Exit Function
      End If
          
' ����������� ���� ��������
    If ReceiverState = RS_UserEsc Then
        Me![��������������] = "�������� �������������"
        �������������� = False
      ElseIf ReceiverState = RS_TimeOut Then
        Me![��������������] = "����-���"
      Else
        Me![��������������] = "������ �����"
       End If
  On Error GoTo 0
End Function

Public Function �������() As Boolean
' �������� ����� ��������� ��������� �� ������ � �����������.
' � ����� ������ ����� ������ "�����" ���������.
' ���������� True, ���� �� ��

    ������� = True          ' �� ��������� �� OK
    Mdm_����������
    ���������������� 3
    ���������������
    Do                      ' ���
        DoEvents
        DoPoll
'        If Mdm_�����������() Then Exit Do
        ���������������
        If ����������� Then Exit Do
        Sleep 20
     Loop
    ����������������        ' ��������� �������� �� ����-�����
    
  On Error Resume Next      ' ������ �� �������� �����
' ���� �� �����-�� ��������� �� �����?
'    If ReceiverState = 0 Then
'        If MdmKWord = AC_NC Then
'            Me![��������������] = ���������������(MdmKWord)
'            ������� = False     ' ���� ��������� "NO CARRIER"
'          Else
'            MdmKWord = AC_CT
'            �������������� = False
'          End If
'        On Error GoTo 0
'        Exit Function
'      End If
          
' ����������� ���� ��������
    If ReceiverState = RS_UserEsc Then
        Me![��������������] = "�������� �������������"
        �������������� = False
        ������� = False
        On Error GoTo 0
        Exit Function
      End If
      
' ���� ��������� "NO CARRIER"
    If ReceiverState = RS_TimeOut Then
        If MdmKWord = AC_NC Then
            Me![��������������] = ���������������(MdmKWord)
            ������� = False     ' ���� ��������� "NO CARRIER"
            On Error GoTo 0
            Exit Function
          End If
      End If
    '
    MdmKWord = AC_CT
    �������������� = False
End Function


' *** ������ �������� ***
Sub �����������_Click()
    ��������������������
End Sub

Private Sub ����������_Click()
    ReceiverState = RS_UserEsc
'    ��� = False
    ClosePort
    Mdm_����������� WrkPortNumber
End Sub

