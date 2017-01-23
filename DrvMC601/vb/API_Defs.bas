Attribute VB_Name = "API_Defs"
Option Explicit

' *** ����� ������� ***
Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
Declare Function GetLastError Lib "kernel32" () As Long
Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long

' *** ��������� DCB ***
Type DCB
    DCBlength As Long       ' ������ DCB
    BaudRate As Long        ' �������� ��������: 110, 300, 600, 1200, 2400 � �.�.
    Flags As Long           ' ���� ������
    wReserved As Integer
    XonLim As Integer       ' ����� ������� Xon (���������� �� ��������)
    XoffLim As Integer      ' ����� ������� Xoff (������������� ��������)
    ByteSize As Byte        ' ����� ���: 5,6,7,8
    Parity As Byte          ' ׸������: 0,1,2,3,4 -- �������������� N,O,E,M,S
    StopBits As Byte        ' ����-����: 0,1,2 -- ������������� 1, 1.5 ��� 2
    XonChar As Byte         ' ������ Xon
    XoffChar As Byte        ' ������ Xoff
    ErrorChar As Byte       ' ������ ������ ��� ��������, �������� � ������� ��������
    EofChar As Byte         ' ������ ��� EOF
    EvtChar As Byte         ' ������ ��� ������������ � ������� (������ ��� 13 -- CR)
    wReserved2 As Integer
  End Type

Public Const FLAG_fBinary = &H1             ' �������� �����
Public Const FLAG_fParity = &H2             ' ��������� ��������
Public Const FLAG_fOutxCtsFlow = &H4        ' ����� CTS ��� ��������
Public Const FLAG_fOutxDsrFlow = &H8        ' ����� DSR ��� ��������
Public Const FLAG_fDtrControl = &H30        ' 2 ����: 3 ������ DTR
Public Const FLAG_fDsrSensitivity = &H40    ' ������������ ���� ��� DSR
Public Const FLAG_fTXContinueOnXoff = &H80
Public Const FLAG_fOutX = &H100             ' ������������ Xon/Xoff �������� ��� ��������
Public Const FLAG_fInX = &H200              ' ������������ Xon/Xoff �������� ��� �����
Public Const FLAG_fErrorChar = &H400        ' ������������ ������ �� ErrorChar
Public Const FLAG_fNull = &H800             ' ������������ 0-�����
Public Const FLAG_fRtsControl = &H3000      ' 2 ����: 4 ������ RTS
Public Const FLAG_fAbortOnError = &H4000    ' ���� ��� ����� ������

Declare Function BuildCommDCB Lib "kernel32" Alias "BuildCommDCBA" (ByVal Mode As String, lpDCB As DCB) As Long
Declare Function SetCommState Lib "kernel32" (ByVal hDev As Long, lpDCB As DCB) As Long
Declare Function GetCommState Lib "kernel32" (ByVal hDev As Long, lpDCB As DCB) As Long

' *** �������� � �������� ***
Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal FileName As String, ByVal DesiredAccess As Long, ByVal ShareMode As Long, ByVal SecurityAttributes As Long, ByVal CreationDisposition As Long, ByVal FlagsAndAttributes As Long, ByVal TemplateFile As Long) As Long

    Public Const GENERIC_READ = &H80000000
    Public Const GENERIC_WRITE = &H40000000
    Public Const OPEN_EXISTING = 3
    Public Const FILE_FLAG_OVERLAPPED = &H40000000
    Public Const INVALID_HANDLE_VALUE = -1

Declare Function SetupComm Lib "kernel32" (ByVal hFile As Long, ByVal dwInQueue As Long, ByVal dwOutQueue As Long) As Long

' *** ����-���� ***
Type COMMTIMEOUTS
    ReadIntervalTimeout As Long
    ReadTotalTimeoutMultiplier As Long
    ReadTotalTimeoutConstant As Long
    WriteTotalTimeoutMultiplier As Long
    WriteTotalTimeoutConstant As Long
  End Type

Declare Function SetCommTimeouts Lib "kernel32" (ByVal hDev As Long, lpTO As COMMTIMEOUTS) As Long
Declare Function GetCommTimeouts Lib "kernel32" (ByVal hDev As Long, lpTO As COMMTIMEOUTS) As Long

' *** ����������� ����� ***
Type OVERLAPPED
    Internal As Long
    InternalHigh As Long
    offset As Long
    OffsetHigh As Long
    hEvent As Long
  End Type

Declare Function CreateEvent Lib "kernel32" Alias "CreateEventA" (ByVal EventAttributes As Long, ByVal ManualReset As Long, ByVal InitialState As Long, ByVal Name As String) As Long
Declare Function SetCommMask Lib "kernel32" (ByVal hDev As Long, ByVal Mask As Long) As Long
Declare Function GetCommMask Lib "kernel32" (ByVal hDev As Long, Mask As Long) As Long

Public Const EV_RXCHAR = &H1           ' ������ ������
Public Const EV_RXFLAG = &H2           ' ������� ���������� ������ (��������� � ���� EvtChar DCB)
Public Const EV_TXEMPTY = &H4          ' ������� �������� �����
Public Const EV_CTS = &H8              ' ��������� �� ����� CTS
Public Const EV_DSR = &H10             ' ��������� �� ����� DSR
Public Const EV_RLSD = &H20            ' ��������� �� ����� RLSD
Public Const EV_BREAK = &H40           ' ������� ������ BREAK
Public Const EV_ERR = &H80             ' ������ ����� (��������, ����������, �.�.)
Public Const EV_RING = &H100           ' ������� ������ Ring

Public Const EV_PERR = &H200           ' ������ ��������
Public Const EV_RX80FULL = &H400       ' ������� ����� ����� �� 80%
Public Const EV_EVENT1 = &H800         ' ����������� ������� 1
Public Const EV_EVENT2 = &H1000        ' ����������� ������� 2

' *** ������/������/�������� �������
Declare Function WaitCommEvent Lib "kernel32" (ByVal hDev As Long, Mask As Long, lpOverlapped As OVERLAPPED) As Long
Declare Function WriteFile Lib "kernel32" (ByVal hDev As Long, ByRef Buffer As Byte, ByVal BytesToWrite As Long, BytesWritten As Long, lpOverlapped As OVERLAPPED) As Long
Declare Function ReadFile Lib "kernel32" (ByVal hDev As Long, ByRef Buffer As Byte, ByVal BytesToRead As Long, BytesRead As Long, lpOverlapped As OVERLAPPED) As Long

Declare Function WaitForSingleObject Lib "kernel32" (ByVal hDev As Long, ByVal dwMilliseconds As Long) As Long
Declare Function GetOverlappedResult Lib "kernel32" (ByVal hDev As Long, lpOverlapped As OVERLAPPED, lpBytesTransferred As Long, ByVal bWait As Long) As Long

Public Const ERROR_IO_PENDING = 997    ' ������ � ��������
Public Const WAIT_TIMEOUT = &H102      ' ����-���

' *** ��������� ***
Declare Function PurgeComm Lib "kernel32" (ByVal hFile As Long, _
        ByVal dwFlags As Long) As Long
    Public Const PURGE_TXABORT = &H1     ' ���������� ��� �������� ������, ���� �������������
    Public Const PURGE_RXABORT = &H2     ' ���������� ��� �������� ������
    Public Const PURGE_TXCLEAR = &H4     ' ������� ������� �������� � ��������
    Public Const PURGE_RXCLEAR = &H8     ' ������� ������� ������ � ��������

Declare Function EscapeCommFunction Lib "kernel32" (ByVal hFile As Long, ByVal nFunc As Long) As Long
    Public Const SETXOFF = 1    ' ���������� ����� ������� XOFF
    Public Const SETXON = 2     ' ���������� ����� ������� XON
    Public Const SETRTS = 3     ' ������������� ������ RTS
    Public Const CLRRTS = 4     ' ���������� ������ RTS
    Public Const SETDTR = 5     ' ������������� ������ DTR
    Public Const CLRDTR = 6     ' ���������� ������ DTR
    Public Const RESETDEV = 7   ' �������� ����� ���������� (���� ��������)
    Public Const SETBREAK = 8   ' ��������� �������� ����� ����������� � ��������� ������� break
    Public Const CLRBREAK = 9   ' ������� ��������� ������� break �� �������� ����� �����������

Declare Function ClearCommError Lib "kernel32" (ByVal hDev As Long, Errors As Long, ByVal l As Long) As Long
    Public Const CE_RXOVER = &H1           ' ���������� ������� ������� ��� ������ ����� EOF
    Public Const CE_OVERRUN = &H2          ' ������������ ������� �������. ��������� ������ �������
    Public Const CE_RXPARITY = &H4         ' ������ ��������
    Public Const CE_FRAME = &H8            ' ������ ���������� (������)
    Public Const CE_BREAK = &H10           ' ������������ Break

Declare Function GetCommModemStatus Lib "kernel32" (ByVal hDev As Long, ModemStat As Long) As Long
    Public Const MS_CTS_ON = &H10&
    Public Const MS_DSR_ON = &H20&
    Public Const MS_RING_ON = &H40&
    Public Const MS_RLSD_ON = &H80&

