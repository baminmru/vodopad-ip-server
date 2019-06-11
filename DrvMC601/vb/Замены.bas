Attribute VB_Name = "������"
Option Explicit

Global Const Subs_P = 1
Global Const Subs_Q = 2
Global Const Subs_S = 4
Global Const Subs_All = 255
Global Const Subs_PQ = 3

'   ***************
'   * ����������� *
'   ***************
Public Function ����������(s As String, ����� As Long, ������ As Long, ����� As Long) As String
' ���� ������ ������� ���� {%Annn_XXXXX} � ������.
' ������ ��������� �� ������� "{", ����� -- �� ��������� ����� "}".
' ���� �� �������, �� ������ = ����� � ��� ��������� �� ����� ������
' �������� � ���������� ��� Annn
Dim i As Long
Dim r As String
    ����� = Len(s) + 1
    ������ = �����
    i = InStr(�����, s, "{%")
        If i = 0 Then Exit Function     ' ��� ������
    ������ = i
    i = InStr(i + 2, s, "}")
        If i = 0 Then Exit Function     ' ��� �����
    ����� = i + 1
    r = Mid(s, ������ + 2, ����� - ������ - 3)
    i = InStr(1, r, "_")
    If i <> 0 Then                      ' ���� �����
        r = Left(r, i - 1)
      End If
    ���������� = r
End Function

Public Function �����������������(ByRef s As String, ByVal ��� As Integer) As Boolean
' ������������ ������.
' �������� ���� ��� ��������� ����������. ��� -- ��� ����� ���������
Dim ����� As Long, ����� As Long
Dim i As Long, j As Long, k As Long
Dim iMacro As Long, pMacro As String
Dim Q As String, r As String
    ����� = Len(s)
    ����� = 1
    Q = ""
  ' ���� ������ ���� �������� � ������
    Do
        r = ����������(s, �����, i, j)
        If i = j Then
            Q = Q & Mid(s, �����, ����� - ����� + 1)
            Exit Do
          End If
        Q = Q & Mid(s, �����, i - �����)
        k = InStr("PQ S", Mid(r, 1, 1))
        k = k And ���               ' �������� ������ ������ ������
        If k <> 0 Then
          ' ������ �������
            r = Mid(r, 2)
            If IsNumeric(r) Then    '
              ' ������ ����� ���������
                ' ****************
                Select Case k
                  Case Subs_P    ' P
                    Q = Q & ��������������_P(CLng(r))
                  Case Subs_Q    ' Q
                    Q = Q & ��������������_Q(CLng(r))
                  Case Subs_S    ' S
                    Q = Q & ��������������_S(CLng(r))
                  End Select
                ����������������� = True
                ' ****************
              Else
              ' ��� �� ����� -- ��������� ��� ���� ��� ������
                Q = Q & Mid(s, i, j - i)
              End If
          Else
          ' ��� �� ����� -- ��������� ��� ���� ��� ������
            Q = Q & Mid(s, i, j - i)
          End If
        ����� = j
      Loop
    s = Q
End Function
