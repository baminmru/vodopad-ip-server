VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} ���������� 
   Caption         =   "����������"
   ClientHeight    =   7476
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   7110
   OleObjectBlob   =   "����������.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "����������"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit


Dim ����� As Integer        ' ����� �����������: ������, �����, ����
Dim ����������� As Boolean  ' ����� �������� (������ "�������" ��������������)


' *****************************
' **** �������� � �������� ****
' *****************************
Private Sub UserForm_Initialize()
    ����������
    �����������������_���
    ������������� = "MC601"
    ����������
End Sub

Private Sub UserForm_Terminate()
    ' ��������� ����������
    If OnLine Then
        Mdm_�������������������
      End If
    StopPortMaster
    StopPoll
End Sub

' **********************
' **** ����� ������ ****
' **********************
Private Function ����������()
' ���������� ����� ����� (������) �����
Dim i As Integer
    If Me![������] Then
        i = 1
      ElseIf Me![�����] Then
        i = 2
      ElseIf Me![����] Then
        i = 3
      ElseIf Me![����] Then
        i = 4
      Else
        i = 0
      End If
    ��������������� i
End Function

Private Sub ������_Change()
    ����������
End Sub

Private Sub �����_Change()
    ����������
End Sub

Private Sub ����_Change()
    ����������
End Sub

Private Sub ����_Change()
    ����������
End Sub

Private Sub ���������������(ByVal i As Integer)
        If ����� = i Then Exit Sub
    ����� = i
    Me![��������������] = Null
    Me![����������] = Null
    Me![�������������] = Null
    
    Select Case �����
      Case 1        ' ������
        Me![����������].BackColor = &H80000016
        Me![����������].Locked = False
        Me![�������].BackColor = &H8000000F
        Me![�������].Locked = True
      Case 2        ' �����
        Me![����������].BackColor = &H80000016
        Me![����������].Locked = False
        Me![�������].BackColor = &H80000016
        Me![�������].Locked = False
      Case 4        ' ����
        Me![����������].BackColor = &H8000000F
        Me![����������].Locked = True
        Me![�������].BackColor = &H8000000F
        Me![�������].Locked = True
      Case Else        ' ����
        Me![����������].BackColor = &H8000000F
        Me![����������].Locked = True
        Me![�������].BackColor = &H8000000F
        Me![�������].Locked = True
      End Select
End Sub

Private Sub �������������(ByVal ��������� As Boolean)
'
    OnPort = ���������
    If OnPort = True Then
        Me![����������].Locked = True
        Me![��������������].Caption = "�������"
        Me![����������].BackColor = &HC0FFC0
        Me![�����].Enabled = False
      Else
        Me![����������].Locked = False
        Me![��������������].Caption = "�������"
        Me![���������������].Enabled = False
        Me![����������].BackColor = &H80000016
        Me![�����].Enabled = True
      End If
End Sub

Private Sub ��������������(ByVal ��������� As Boolean)
    '
    OnLine = ���������
    If OnLine = True Then
        Me![�������].Locked = True
        Me![�������].BackColor = &HC0FFC0
        Me![���������������].Caption = "���������"
      Else
        Me![�������].Locked = False
        Me![�������].BackColor = &H80000016
        Me![���������������].Caption = "���������"
      End If
End Sub

Private Sub ���������������(ByVal ��������� As Boolean)
'
    ����������� = ���������
    If ����������� = True Then
        Me![������������].Enabled = True
        ���������������
        ���������������� (3)
      Else
        Me![������������].Enabled = False
      End If
End Sub



' *************************
' **** �������� ������ ****
' *************************
Private Sub ��������������_Click()
Dim i As Integer
  ' ���� ����� ������, ���������
    If OnPort Then
      ' ��������� ����������
        If OnLine Then
            Mdm_�������������������
            �������������� False
          End If
      ' ��������� ����
        StopPortMaster
        ������������� False
        ��������������� False
        Me![�����].Enabled = True
        Me![��������������] = Null
        Me![�������������] = Null
        Me![������������].Enabled = False
        Exit Sub
      End If
      
  ' ��������� ����
    Select Case �����
      Case 1    ' ����� ����� ������
        StartPortMaster
        ���������������� (2)
      ' ��������� ����
        If NoData(Me![����������]) Or (Me![����������] = 0) Then
            ' ����-�����
            i = MC601_������������(0)
            If i = 0 Then
                Me![��������������] = ����������������(CE_NoC)
                StopPortMaster
                Exit Sub
              End If
            Me![����������] = i
          Else
            ' ����� �����. ������� ������� ����
            i = MC601�����������(Me![����������])
            If i <> 0 Then
                Me![��������������] = ����������������(CE_Opn)
                StopPortMaster
                Exit Sub
              End If
          End If
      ' ��������� �����
        
        Me![��������������] = "���� ������"
        Me![�������������] = MC601_������������
        ������������� True
        If NoData(Me![�������������]) Then
            Me![�������������] = ����������������(CE_Cal)
            Exit Sub
          End If
        ����� = MC601_��������������(98)
        ��������������� True
        Exit Sub
        
      Case 2    ' ����� ����� �����
        StartPortMaster
        ���������������� (2)
      ' ��������� ����
        If NoData(Me![����������]) Or (Me![����������] = 0) Then
          ' ����-�����
            i = Mdm_����������(0)
            If i = 0 Then
                Me![��������������] = ����������������(CE_NoM)
                StopPortMaster
                Exit Sub
              End If
            Me![����������] = i
          Else
          ' ����� �����. ������� ������� ����
            i = Mdm_�����������(Me![����������])
            If i <> 0 Then
                Me![��������������] = ����������������(CE_Opn)
                StopPortMaster
                Exit Sub
              End If
          End If
      ' ��������� �����
        i = Mdm_���������()
        ������������� True
        If i = 0 Then ' ��������� ����� ������
            Me![��������������] = ����������������(CE_Mdm)
          Else
            Me![��������������] = "����� ���������"
            Me![���������������].Enabled = True
          End If
        Exit Sub
        
      Case 4    ' �������� �����������
        Me![��������������] = " ���� (������ ����������)"
        ����� = 101
        ��������������� True
        Exit Sub
        
      Case Else
        Exit Sub
      End Select
      
End Sub

Private Sub ���������������_Click()
    If OnLine Then
        Mdm_�������������������
        �������������� False
        Exit Sub
      End If
      
  ' ������������� � ��������� ����������
    If NoData(Me![�������]) Then
        MsgBox "� �����?", vbOKOnly, "��������!"
         Exit Sub
      End If
    If Mdm_��������������������(Me![�������]) = False Then
        Exit Sub
      End If
  '
    ChannalMode = CM_Line
    �������������� True
    Me![�������������] = Null
    ���������������� (3)
    
    Me![�������������] = MC601_������������
    If NoData(Me![�������������]) Then
        Me![�������������] = ����������������(CE_Cal)
        Exit Sub
      End If
    ����� = MC601_��������������(98)
    ��������������� True
End Sub


' **************************
' **** ������ � ��ר��� ****
' **************************

' *** ��������� ***
' ��� ��������� ������� ����������� ����� ������� � ������
' ��� ����� ��� ����� ���������� ������

Private Sub ��������������(ByVal ���������� As Boolean)
    Me![���������].Enabled = ����������
    Me![����1].Enabled = ����������
    Me![�����1].Enabled = ����������
End Sub

Private Sub ���������������(ByVal ���������� As Boolean)
' ������������ ��� ���������/���������� ���������� ����������
Dim i As Integer
    Me![�����].Enabled = ����������
    Me![�����].Enabled = ����������
    For i = 2 To 3
'        Me("������" & CStr(i)).Enabled = ����������
        Me("����" & CStr(i)).Enabled = ����������
        Me("�����" & CStr(i)).Enabled = ����������
      Next i
End Sub

Private Sub ����������()
    If NoData(����������) Or (���������� = 0) Then
        �������������� False
      Else
        �������������� True
      End If
    If NoData(���������) Or (��������� = 0) Then
        ��������������� False
        Me![�����].Visible = False
        Me![����������].Visible = False
      Else
        ��������������� True
        Me![�����].Visible = True
        Me![����������].Visible = True
      End If
End Sub

' *** ���������� ***
Private Sub ������������()
Dim s As String
    If NoData(���������) Then
        s = ����������
        ����������������� s, Subs_Q
        Me![����������] = s
      End If
End Sub

Private Sub ������������()
'    Me![�����] = ��������������(�������)
'    Me![�����] = ��������������(�������)
'    Me![���������] = �������������(�������)
    Me![������������] = ����������������(���������)
    Me![�������������] = �����������������(����������)
    Me![��������] = �����������(�����, ����������)
    ������������
End Sub

Private Sub �����������()
' ���������� ��� ����� ���� ������
' ������������ ���������� �������� �� ���������
    ������� = �����������(�������)
    Me![�����] = ��������������(�������)
    ������� = ����������(�������)
    Me![�����] = ��������������(�������)
    ����������
    �������������������
End Sub

Private Sub ������()
' ���������� ��� ����� ����� ������� � ������
' ������������ ���������� �������� �� ���������
    ������� = ����������()
    Me![���������] = �������������(�������)
    �����������
End Sub

Private Sub �������������������()
    Me![�����] = �������������(�������, �������) + 1
End Sub


' *** ������� � ������ ***
Private Sub ������1_Click()
    Me![�������] = �����������(����_�������)
    ��������������_P Me![�������]
'    ���������������������
'    Me![����������] = �����������
    ������������
    
    If NoData(Me![�������������]) Then Exit Sub
    If NoData(������������) Then Exit Sub
    If CStr(������������) = CStr(Me![�������������]) Then Exit Sub
    MsgBox "����� �������� �� ������������� ������," & _
        vbCrLf & "���������� � ������� ����!", vbOKOnly, "��������!"
End Sub

Private Sub ������2_Click()
    Me![������] = �����������(����_�������)
    ��������������_I Me![������]
    ���������������
    ������
    ������������
End Sub


' *** ���� "�" ***
Private Sub ��������_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
    If NoData(Me![��������]) Then Exit Sub
    If �����������(Me![��������], ����������) = 0 Then
        MsgBox "������������ ������", vbOKOnly, "������!"
        Cancel = True
      End If
End Sub

Private Sub ��������_AfterUpdate()
    ����� = �����������(Me![��������], ����������)
    ���������������
    Me![��������] = �����������(�����, ����������)
    ������
    ������������
End Sub


' *** ������ ***
Public Sub ��������������()
    Me![���������] = �������������(�������)
    �����������
    ������������
End Sub

Private Sub ���������_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
    If NoData(Me![���������]) Then Exit Sub
    If �������������(Me![���������], ����������) = 0 Then
        MsgBox "������������ ������", vbOKOnly, "������!"
        Cancel = True
      End If
End Sub

Private Sub ���������_AfterUpdate()
    ������� = �������������(Me![���������], ����������)
    ��������������
End Sub

Private Sub �����1_Click()
    If NoData(�������) Then
        ������� = ����������
      Else
        ������� = ����������������(�������)
      End If
    ��������������
End Sub

Private Sub ����1_Click()
    If NoData(�������) Then Exit Sub
    ������� = ���������������(�������)
    If ������� > ���������� Then
        ������� = Null
      End If
    ��������������
End Sub


' *** ������� ������� ***
Public Sub ���������������()
    Me.����� = ��������������(�������)
    If NoData(�������) Then
        ������� = Null
        ��������������
      End If
    If ���������������(�������, �����) > Date Then
        ������� = �����������������(Date, �����, ���������)
      End If
    ��������������
End Sub

Private Sub �����_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
        If NoData(Me.�����) Then Exit Sub
        If Str2YYMMDD(Me.�����, ���������) <> 0 Then Exit Sub
    MsgBox "������������ ��������!", vbOKOnly, "��������!"
    Cancel = True
End Sub

Private Sub �����_AfterUpdate()
    If NoData(Me.�����) Or NoData(���������) Then
        ������� = Null
      Else
        ������� = ��������������(Me.�����, ���������)
      End If
    ���������������
End Sub

Private Sub ����3_Click()
Dim d As Date, l As Long
    If NoData(�������) Then Exit Sub
    l = ��������������(�������, ���������)
    d = ���������������(l, �����)
    If d < Date Then ������� = l
    ���������������
End Sub

Private Sub �����3_Click()
Dim d As Integer, m As Integer, Y As Integer
    If NoData(�������) Then Exit Sub
    ������� = ���������������(�������, ���������)
    ���������������
End Sub


' *** ������ ������� ***
Public Sub ��������������()
'    If NoData(�������) Then ������� = Null
    If Not (NoData(�������) Or NoData(�������)) Then
        If (������� > �������) Then ������� = �������
      End If
    Me![�����] = ��������������(�������)
    �������������������
End Sub

Private Sub �����_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
        If NoData(Me![�����]) Then Exit Sub
        If Str2YYMMDD(Me![�����], ���������) <> 0 Then Exit Sub
    MsgBox "������������ ��������!", vbOKOnly, "��������!"
    Cancel = 1
End Sub

Private Sub �����_AfterUpdate()
    If NoData(Me![�����]) Or NoData(���������) Then
        ������� = Null
      Else
        ������� = ��������������(Me![�����], ���������)
    End If
    ��������������
End Sub

Private Sub ����2_Click()
Dim d As Date, l As Long
    If NoData(�������) Then Exit Sub
    ������� = ��������������(�������, ���������)
    ��������������
End Sub

Private Sub �����2_Click()
Dim d As Integer, m As Integer, Y As Integer
    If NoData(�������) Then Exit Sub
    ������� = ���������������(�������, ���������)
    ��������������
End Sub

' ***********************
' ****   � � � � �   ****
' ***********************
Private Sub �������������_Click()
    ����������� Me![�������], ����_�������, True
End Sub

Private Sub ������������_Click()
    ����������� Me![������], ����_�������, True
End Sub

Private Sub ������������_Click()
'    ������������ ����_������
    ������������������� ����_������, True
End Sub



' *************************
' *** ���������  ��ר�� ***
' *************************
Private Sub ������������_Click()
Dim ������, �����
' Dim ���� As Object

    If NoData(Me![�������]) Then
        MsgBox "�� ������ �������!", vbOKOnly, "��������!"
        Exit Sub
      End If
    If NoData(Me![������]) Then
        MsgBox "�� ������ ������!", vbOKOnly, "��������!"
        Exit Sub
      End If
    If NoData(���������) Or (��������� = 0) Then
      Else
        If NoData(�������) Or NoData(�������) Then
            MsgBox "�� ���������� ������� ����������!", vbOKOnly, "��������!"
            Exit Sub
          End If
      End If
    
    If Not ��������������(Me![�������]) Then
        MsgBox "�� ������� ������� �������!", vbOKOnly, "������!"
        Exit Sub
      End If
    
    �������� = Me![�������������]
    ��������� = Me![����������]
    ��������� = Date
    ���������� = Time()
    ����� = �������������(�������, �������) + 1
    If ����� <= 0 Then
        MsgBox "������������ �������!", vbOKOnly, "��������!"
        Exit Sub
      End If
    ������ = �������������(�������, Date2YYMMDD(Date, 2))
    
  ' ***********************
    �������������� = False
    Select Case ���������
      Case 101
        ����������101 Me![������], Me![����������], ������, �����
      Case 201
        ����������201 Me![������], Me![����������], ������, �����
      Case 301
        ����������301 Me![������], Me![����������], ������, �����
      Case Else
        ������������ Me![������], Me![����������], ������, �����
      End Select
  ' ***********************
  
    Me![�������������] = False
    ��������������
    ��������� = ""
End Sub


