Attribute VB_Name = "���������"
Option Explicit

Private ��������� As Object
Private ������� As Boolean
Private Xmin As Single, Xmax As Single
Private ����� As Long, ��� As Long

Public Function ���������������() As Boolean
    On Error Resume Next
    Set ��������� = ��������
    ���������.Show
    ������� = (Err = 0)
    ��������������� = �������
    ���������.������
    ���������.Repaint
End Function

Public Sub �������������(ByVal Q As Single)
    If Not ������� Then Exit Sub
    ���������.���� Q
    ���������.Repaint
End Sub

Public Sub ���������������()
    If Not ������� Then Exit Sub
    ���������.������
    ���������.Repaint
End Sub

Public Sub ����������������()
    On Error Resume Next
    Unload ���������
    ������� = False
End Sub

Public Sub ������������������(����� As Long, Zmin As Single, Zmax As Single)
    If Not ������� Then Exit Sub
    Xmin = Zmin
    Xmax = Zmax
    ����� = �����
    ��� = 0
    ������������� Xmin
End Sub

 Public Sub ������������(ByVal k As Long)
 Dim Q As Single
     If Not ������� Then Exit Sub
    ��� = ��� + k
    Q = Xmin + (Xmax - Xmin) * ��� / �����
     ������������� Q
End Sub


