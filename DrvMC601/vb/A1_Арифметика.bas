Attribute VB_Name = "A1_����������"
Option Explicit

Public Function NoData(ByVal X As Variant) As Boolean
' ���������, �������� �� ���������� ������ (�.�. �� Null � �� ������ ������)
' ������ ��������� ��� �������� �������� ���� ����� ��� ������ �������
    If IsNull(X) Then
        NoData = True
      ElseIf X = "" Then
        NoData = True
      Else
        NoData = False
      End If
End Function


