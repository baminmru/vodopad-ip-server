VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} �������� 
   Caption         =   "��� ����������"
   ClientHeight    =   1572
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   5790
   OleObjectBlob   =   "��������.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "��������"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim ������ As Long  ' ������ ������ ����������

' *** �������� ***
Public Sub ������()
'    ������������� 0
    ���� 0
    Me![����������].Visible = False
    Me![�����������].Visible = True
    Me![����������].Enabled = True
End Sub

Public Sub ������()
'    ������������� 1
    ���� 1
    Me![�����������].Visible = False
    Me![����������].Visible = True
    Me![����������].Enabled = False
End Sub

Public Sub ����(ByVal Q As Single)
' ������������� ������ ����������. Q -- ���� �������
    If Q < 0# Then Q = 0#
    If Q > 1# Then Q = 1#
    Me![�������].Width = Me![����].Width * Q
End Sub

Private Sub ����������_Click()
    �������������� = True
End Sub
