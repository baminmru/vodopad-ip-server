Attribute VB_Name = "C2_Mdm_��������"
Option Explicit
'               **********************
'               *  ������ � �������  *
'               **********************
'
'
' ������ ������������ ������ � �������
'
' ��� ����������
Global MdmKWord As Integer    ' ��� ����������
    Global Const AC_None = -1       ' ��� �� �������
    Global Const AC_OK = 0          ' OK
    Global Const AC_CT = 1          ' CONNECT
    Global Const AC_RI = 2          ' RING
    Global Const AC_NC = 3          ' NO CARRIER
    Global Const AC_ER = 4          ' ERROR
    Global Const AC_ND = 6          ' NO DIALTONE
    Global Const AC_BS = 7          ' BUSY
    Global Const AC_NA = 8          ' NO ANSWER
    
Dim MdmError As Integer  ' ��� ������ ���������
'    Global Const EP_None = 0    ' �� �� (������ ���)
'    Global Const EP_TooMany = 1 ' ������� ����� ������
'    Global Const EP_Raving = 2  ' ������ ��� ������� (����)
    
Dim MdmState As Integer      ' ��������� �������� �����
     Const IP_Off = 0               ' ���� ��������
     Const IP_On = 1                ' ��� ����

' ����� ��������� �����
Const mWrd = 16
Dim KeyWrd(0 To mWrd - 1) As Byte   ' �����
Dim nWrd As Integer                 ' ����������� ������
' ����� �����
Const mAns = 1024
Global MdmAns(0 To mAns - 1) As Byte   ' ����� ������ ������
Global nAns As Integer                 ' ����� ������

Const InitPar = "1200,N,8,1"    ' ������ ���������� ������ �����

' *** ������������ ������� ***
Public Function Mdm_Cmd2Rqs(Cmd As String, Rqs() As Byte) As Integer
' ������ ������ (� ������� Rqs) ��� �������� �� �������.
' ���������� ������ ������� � ������
' Cmd -- ��������� ������
' Rqs -- ������ � ��������� ASCII � ����������� �������� CR �� ����� (����� ������� +++)
Dim i As Integer
    i = Str2ASCII(Cmd, Rqs)
    If Cmd <> "+++" Then
        Rqs(i) = &HD
        i = i + 1
      End If
    Mdm_Cmd2Rqs = i
End Function

'  ****************************************
'  *  � � � � � � � � � �  � � � � � � �  *
'  ****************************************
Public Sub Mdm_����������()
' �������� ������� �����. �������������� ��� �����. ������.
    nAns = 0
    nWrd = 0
    MdmKWord = AC_None
    MdmError = AC_OK
    MdmState = IP_On
End Sub

Public Sub Mdm_������������()
' ��������� �������. � ������� �������� �� ����������� ���.
    MdmState = IP_Off
End Sub

Public Function Mdm_�����������() As Boolean
' ���������, �������� �� ����.
    Mdm_����������� = (MdmState = IP_Off)
End Function

Public Function Mdm_�����������() As Integer
' ���������� ��� ������ ����� ������
    Mdm_����������� = MdmError
End Function

'   *********************************
'   *   � � � � � � �  � � � � � �  *
'   *********************************
Public Sub Mdm_��������������(ByVal b As Byte)
' ������� �����. ����������� ����, ����� ��������� �����
    Select Case MdmState
      Case IP_Off   ' ���� �� ���������. ����� ���� -- ������
        If MdmError <> 0 Then MdmError = EP_Raving
      Case IP_On    ' ���� �������
        If nAns >= mAns Then        ' ��������� � ������� ������
            MdmState = IP_Off
            MdmError = EP_TooMany
          Else
            MdmAns(nAns) = b
            nAns = nAns + 1
          End If
        MdmKWord = IsWord2(b)       ' ��������� �������� �����
        If MdmKWord <> AC_None Then
            MdmState = IP_Off
          End If
    End Select
End Sub


Function IsCRLF(ByVal b As Byte) As Boolean
' ��������� "���������" ������� ����� �� ������� ������������������ CRLF
' ��� ��������� ���������� True
Static HaveCR As Integer    ' ��������� �������� �����
    IsCRLF = False
    If b = &HD Then
        HaveCR = 1          ' ���� CR; ����� ����� LF
        Exit Function
      End If
    If (b = &HA) And (HaveCR = 1) Then
        IsCRLF = True       ' ����� LF � �� ������
'        AttCRLF = 0
'        Exit Function
      End If
    HaveCR = 0              ' �������� �� � ������
End Function

Function IsWord1(ByVal b As Byte) As Integer
' ��������� ��������� ������� ����� �� �������� ����� OK ��� ERROR.
' ������������ ��� ������ �������� ��������� � �������.
' ���������� ����� ��������� �����
Dim s As String
    IsWord1 = AC_None
' ���������� �������� � ������������� �����
    If nWrd < mWrd Then
        KeyWrd(nWrd) = b
        nWrd = nWrd + 1
      End If
' ���� �� ��������� �� �������� �����
    If Not IsCRLF(b) Then Exit Function
' ���������� � �������� ������
    If nWrd > 2 Then    ' ����� ����� ����������?
        s = ASCII2Str(KeyWrd, nWrd - 2)
        If s = "OK" Then
            IsWord1 = AC_OK
          ElseIf s = "ERROR" Then
            IsWord1 = AC_ER
          End If
      End If
    nWrd = 0            ' �������� ����� ����� ������
End Function

Function IsWord2(ByVal b As Byte) As Integer
' ��������� ��������� ������� ����� �� �������� ����� OK ��� ERROR.
' ������������ ��� �������� �����������
' ���������� ����� ��������� �����
Dim s As String
    IsWord2 = AC_None
    If nWrd < mWrd Then
        KeyWrd(nWrd) = b
        nWrd = nWrd + 1
      End If
' ���� �� ��������� �� �������� �����
    If Not IsCRLF(b) Then Exit Function
' ���������� � �������� ������
    If nWrd > 2 Then    ' ����� ����� ����������?
        s = ASCII2Str(KeyWrd, nWrd - 2)
        Select Case s
          Case "OK"
            IsWord2 = AC_OK
          Case "ERROR"
            IsWord2 = AC_ER
          Case "NO DIALTONE"
            IsWord2 = AC_ND
          Case "BUSY"
            IsWord2 = AC_BS
          Case "NO ANSWER"
            IsWord2 = AC_NA
          Case "NO CARRIER"
            IsWord2 = AC_NC
          Case Else
            If Left(s, 7) = "CONNECT" Then
                IsWord2 = AC_CT
              End If
          End Select
      End If
    nWrd = 0            ' �������� ����� ����� ������
End Function

Function IsNoCarrier(ByVal b As Byte) As Integer
' ��������� ��������� ������� ����� �� �������� ����� NO CARRIER
' ������������ � �������� ����� ������
' ���������� ����� ��������� �����
Dim s As String
    IsNoCarrier = AC_None
' ���������� �������� � ������������� �����
    If nWrd < mWrd Then
        KeyWrd(nWrd) = b
        nWrd = nWrd + 1
      End If
' ���� �� ��������� �� �������� �����
    If Not IsCRLF(b) Then Exit Function
' ���������� � �������� ������
    If nWrd = 12 Then   ' ����� ��������?
        If ASCII2Str(KeyWrd, 10) = "NO CARRIER" Then
            IsNoCarrier = AC_NC
          End If
      End If
    nWrd = 0            ' �������� ����� ����� ������
End Function

Function IsKeyWord(ByVal b As Byte, ByRef iState As Integer, Sampler() As Byte, ByVal n As Integer) As Boolean
' ��������� "���������" ������� ����� �� ������� �������� ������������������ ����
' (�������� Sampler, ���������� �� 0, ����� ���� � ������� -- �������� n).
' ������� ����� ��������� � ���� ���������� ����� (�������� b), ������� ������������
' � ���, ��� ��������� � i-� ������� (�������� iState). ������� iState -- "������" ��������.
' �������� 0 -- �������� ���������, >0 -- ������ ������� ����������, ��������� ��������� ������.
' ��� ��������� ������� ������� ���������� True
    IsKeyWord = False
    If b = Sampler(iState) Then
        iState = iState + 1
        If iState = n Then
            iState = 0
            IsKeyWord = True
          End If
      Else
        iState = 0
      End If
End Function

Function ���������������(ByVal i As Integer) As String
    Select Case i
      Case AC_OK
        ��������������� = "������� ���������"
      Case AC_CT
        ��������������� = "����������� �����������"
      Case CE_CPr
        ��������������� = "������ ���������� ���������� �����"
      Case AC_RI
        ��������������� = "������!"
      Case AC_NC
        ��������������� = "�� ������� ���������� ����������"
      Case AC_ER
        ��������������� = "�������� ������� ��� �����"
      Case AC_ND
        ��������������� = "��� ����� � �����"
      Case AC_BS
        ��������������� = "����� ������"
      Case AC_NA
        ��������������� = "��� ������"
      End Select
End Function
