VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} ��������� 
   Caption         =   "����"
   ClientHeight    =   2412
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   4635
   OleObjectBlob   =   "���������.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "���������"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'' �������� � ������� API

Dim ��� As Boolean

Private Sub UserForm_Terminate()
    ��� = False                ' ������������� ������
End Sub



Private Sub �����������_Click()
    ��� = True
    Do                      ' ��� ���������� �����
        DoEvents
        Sleep 20
        Me![�����] = Time()
      Loop Until ��� = False
End Sub


Private Sub ����������_Click()
    ��� = False
    
End Sub

Private Sub �������������_Click()
    Me![������] = "������ !!!"
    ������������ ("P412-55-45")
    Me![������] = "��������� !!!"
End Sub

