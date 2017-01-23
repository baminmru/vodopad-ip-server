Attribute VB_Name = "C_MC601_��������������"
Option Explicit
'               ****************************
'               *  M U L T I C A L  6 0 1  *
'               ****************************
' ������ ������������ �������� ������ �� ��������� KUMSTRUP MULTICAL 601
' ������, �� ����������� � ��������� ����������� ��������� (���������)
' ����� �� �����������. ������� ����� ��� �� ��������!
'
' ������ ���������
Dim ��������������� As Integer  ' ������ ���������
    Global Const EP_None = 0    ' �� �� (������ ���)
    Global Const EP_TooMany = 1 ' ������� ����� ������
    Global Const EP_Raving = 2  ' ������ ��� ������� (����)
    Global Const EP_Head = 3    ' ��� ���������� �����
    Global Const EP_Body = 4    ' ��� ���������� ����� ����� ���
    Global Const EP_Stuff = 5   ' ������������ ������
    Global Const EP_CRC = 6     ' ������ CRC

' ��������� ��������� �������� �����
Dim �������������� As Integer      ' ��������� �������� �����
     Const IW_Off = 0               ' ���� ��������
     Const IW_Wait = 1              ' ��� ���
     Const IW_Echo = 2              ' ���� ���
     Const IW_Paus = 3              ' ��� ���������� �����
     Const IW_Resp = 4              ' ���� ������
     Const IW_Stuf = 5              ' ��� ������

' ��������� ���� (� ������)
Const SB_HdQ = &H80, St_HdQ = &H7F  ' �����-���� �������
Const SB_HdR = &H40, St_HdR = &HBF  ' �����-���� ������
Const SB_End = &HD, St_End = &HF2   ' ����-����
Const SB_Stf = &H1B, St_Stf = &HE4  ' ���� �������� ������
Const SB_Ack = &H6, St_Ack = &HF9   ' �������������


'  ********************************************
'  *  � � � � � � � � � � � �  � � � � � � �  *
'  ********************************************
Public Function MC601Cmd2Rqs(Cmd() As Byte, ByVal n As Integer, Rqs() As Byte) As Integer
' ������ ������ �������� ��������� ������ (� ������� Rqs) ��� �������� �� �������.
' ���������� ������ ������� � ������
' Cmd -- �������� ������ � �������� � �������:
'       ���� 0  -- ����� ����������
'       ���� 1  -- ����� �������
'       ���� 2 �  �.�. -- ��������� �������
'   ����� n ����
' ������� � ������� Cmd ��������� ���������� ���������.
' ������ Cmd ������ ��������� ���������� �������������� 2 ������ CRC.
Dim b As Byte
Dim i As Integer, j As Integer
        If n <= 0 Then Exit Function
  ' � ������� ���������� CRC
    i = CRC_CCITT16(Cmd, n, 0)
    Cmd(n + 0) = I2B_Hi(i)
    Cmd(n + 1) = I2B_Lo(i)
  ' ������ ���������
    Rqs(0) = SB_HdQ         ' ��������� ��������� ����
    j = 1
    For i = 0 To n + 1
        b = Cmd(i)          ' ��������� ����� �������,
        If EnStuff(b) Then ' �� ���� �������� ������
            Rqs(j) = SB_Stf
            j = j + 1
          End If
        Rqs(j) = b
        j = j + 1
      Next i
    Rqs(j) = SB_End         ' ��������� �������� ����
    j = j + 1
    MC601Cmd2Rqs = j
End Function


'  ****************************************
'  *  � � � � � � � � � �  � � � � � � �  *
'  ****************************************
Public Sub MC601����������()
' �������� ������� �����. �������������� ��� �����. ������.
    MC601������� = 0
    ��������������� = EP_None
    �������������� = IW_Wait
End Sub

Public Sub MC601������������()
' ��������� �������. � ������� �������� �� ����������� ���.
' ������������ ��� ������������� ������ ����� ��� ��������
        �������������� = IW_Off
End Sub

Public Function MC601�����������() As Boolean
' ���������, �������� �� ����.
    MC601����������� = (�������������� = IW_Off)
End Function

Public Sub MC601���������CRC()
' ��������� � �������� CRC
' �������� ���� ��������� ����� �� ����� �����!!!
If ��������������� = EP_None Then
    If CRC_CCITT16(MC601�����, MC601�������, 0) = 0 Then
        MC601������� = MC601������� - 2
      Else
        ��������������� = EP_CRC
      End If
  End If
End Sub

Public Function MC601�����������() As Integer
' ���������� ��� ������ ����� ������
    MC601����������� = ���������������
End Function


'   *********************************
'   *   � � � � � � �  � � � � � �  *
'   *********************************
Public Sub MC601��������������(ByVal b As Byte)
' ������� �����. �������� ���, ������� ��������� � �������� ������ � �.�.
    Select Case ��������������
      Case IW_Off   ' ���� �� ���������. ����� ���� -- ������
        If ��������������� <> 0 Then ��������������� = EP_Raving
      Case IW_Wait  ' ��� ��� ��� ���������� ������.
        If b = SB_HdQ Then              ' ������ ��� � ����-�������
            �������������� = IW_Echo
        ElseIf b = SB_HdR Then
            �������������� = IW_Resp   ' ������ ������
          Else
            �������������� = IW_Off
            ��������������� = EP_Head   ' �� ��������� ����
          End If
      Case IW_Echo  ' ��������� ���. �� ����-����� ����������
        If b = SB_End Then
            �������������� = IW_Paus   ' ��������� ���� ���
          Else
            ' � ��������, ����� ���������� � ��������
            ' �� �� ���� ������ �� ������
          End If
      Case IW_Paus  ' ��� ������. �������� ������ ��������� ����
        If b = SB_HdR Then
            �������������� = IW_Resp
          ElseIf b = SB_Ack Then
            �������������� = IW_Off
            ������������ b
          Else
            �������������� = IW_Off
            ��������������� = EP_Body   ' �� ��������� ����
            ������������ b
          End If
      Case IW_Resp  ' ��������� �����
        If b = SB_End Then
            �������������� = IW_Off
          ElseIf b = SB_Stf Then
            �������������� = IW_Stuf
          Else
            ������������ b
          End If
      Case IW_Stuf  ' ��� ���� ������
        If DeStuff(b) Then
            �������������� = IW_Resp
          Else
            �������������� = IW_Off
            ��������������� = EP_Stuff  ' ������������ ������
          End If
        ������������ b
      End Select
End Sub

Private Sub ������������(ByVal b As Byte)
' ���������� ��������� ���� � ������� �����. ����� ��� ���������� ���������
    If MC601������� >= MaxRsp Then
        �������������� = IW_Off
        ��������������� = EP_TooMany
      Else
        MC601�����(MC601�������) = b
        MC601������� = MC601������� + 1
      End If
End Sub


'  ************************************
'  *  S T U F F I N G -- � � � � � �  *
'  ************************************
Private Function EnStuff(ByRef b As Byte) As Boolean
' ���������, ������� �� ���� ������. ���� ��, �� �������� ��� � ���������� True
    EnStuff = True
    Select Case b
      Case SB_HdQ
        b = St_HdQ
      Case SB_HdR
        b = St_HdR
      Case SB_End
        b = St_End
      Case SB_Stf
        b = St_Stf
      Case SB_Ack
        b = St_Ack
      Case Else
        EnStuff = False
      End Select
End Function

Private Function DeStuff(ByRef b As Byte) As Boolean
' ���������, �������� �� ���� �����������. ���� ��, �� ���������� ������.
' ���������� -- �������� �������� � Stuffing.
    DeStuff = True
    Select Case b
      Case St_HdQ
        b = SB_HdQ
      Case St_HdR
        b = SB_HdR
      Case St_End
        b = SB_End
      Case St_Stf
        b = SB_Stf
      Case St_Ack
        b = SB_Ack
      Case Else
        DeStuff = False
      End Select
End Function

