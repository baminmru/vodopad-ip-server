Attribute VB_Name = "C2_Mdm_�����������"
Option Explicit


Global �������������� As Boolean

Function ������������(������� As String) As Boolean
    �������������� = True
    �����������.Show
    �����������.��� �������
    �����������.��������������������
    Do                      ' ��� ���������� �����
        DoEvents
        DoPoll
        Sleep 50
      Loop Until �������������� = False
    Unload �����������
    ������������ = (MdmKWord = AC_CT)
End Function

