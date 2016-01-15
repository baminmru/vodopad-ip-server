Option Strict Off
Option Explicit On
Module IPSerial
	'********************************************************
	'    IPSerial.bas   ver 1.00
	'     -- IPSerial module for Visual Basic(5.0 above)
	'
	'    Description:
	'       When you want to develop one VB application with IPSerial,
	'       you should add this module to your project.
	'
	'    History:
	'           Date    Author      Comment
	'            3/17/03  Shinhay     wrote it.
	'
	'********************************************************
	
	'Baud Rate Setting
	
	Public Const B50 As Short = 0
	Public Const B75 As Short = 1
	Public Const B110 As Short = 2
	Public Const B134 As Short = 3
	Public Const B150 As Short = 4
	Public Const B300 As Short = 5
	Public Const B600 As Short = 6
	Public Const B1200 As Short = 7
	Public Const B2400 As Short = 9
	Public Const B4800 As Short = 10
	Public Const B7200 As Short = 11
	Public Const B9600 As Short = 12
	Public Const B19200 As Short = 13
	Public Const B38400 As Short = 14
	Public Const B57600 As Short = 15
	Public Const B115200 As Short = 16
	Public Const B230400 As Short = 17
	Public Const B460800 As Short = 18
	Public Const B921600 As Short = 19
	
	' Mode setting
	Public Const BIT_5 As Short = 0 ' Data bits define
	Public Const BIT_6 As Short = 1
	Public Const BIT_7 As Short = 2
	Public Const BIT_8 As Short = 3
	
	Public Const STOP_1 As Short = 0 ' Stop bits define
	Public Const STOP_2 As Short = 4
	
	Public Const P_IP_SERIAL_NONE As Short = 0
	Public Const P_IP_SERIAL_EVEN As Short = 8 ' Parity define
	Public Const P_IP_SERIAL_ODD As Short = 16
	Public Const P_IP_SERIAL_MARK As Short = 24
	Public Const P_IP_SERIAL_SPACE As Short = 32
	
	Public Const F_NONE As Short = &H0s ' Flow Control : None
	Public Const F_CTS As Short = &H1s ' Flow Control : CTS
	Public Const F_RTS As Short = &H2s ' Flow Control : RTS
	Public Const F_XON As Short = &H4s ' Flow Control : XON
	Public Const F_XOFF As Short = &H8s ' Flow Control : XOFF
	Public Const F_RTS_CTS As Boolean = F_RTS Or F_CTS
	Public Const F_XON_XOFF As Boolean = F_XON Or F_XOFF
	Public Const F_BOTH As Boolean = F_RTS Or F_CTS Or F_XON Or F_XOFF
	
	
	'linectrl command
	Public Const LCTRL_DTR As Short = 1 ' set DTR on
	Public Const LCTRL_RTS As Short = 2 ' set RTS on
	
	' FLUSH command
	Public Const FLUSH_RXBUFFER As Short = 0 ' flush Rx buffer
	Public Const FLUSH_TXBUFFER As Short = 1 ' flush Tx buffer
	Public Const FLUSH_ALLBUFFER As Short = 2 ' flush Rx & Tx buffer
	
	' LSTATUS command }
	Public Const S_CTS As Short = 1 ' line status : CTS on
	Public Const S_DSR As Short = 2 ' line status : DSR on
	Public Const S_DCD As Short = 8 ' line status : DCD on
	
	
	' Error code
	Public Const NSIO_OK As Short = 0
	
	Public Const NSIO_BADPORT As Short = -1 ' no such port or port not opened
	Public Const NSIO_BADPARM As Short = -2 ' bad parameter
	Public Const NSIO_THREAD_ERR As Short = -3
	Public Const NSIO_MEMALLOCERR As Short = -4 ' memory allocate error
	
	Public Const NSIO_INVALID_PASSWORD As Short = -100 'Invalid console password
	Public Const NSIO_RESET_TIMEOUT As Short = -101 ' Reset port timeout(fail).
	Public Const NSIO_NOT_ALIVE As Short = -102 ' This com port is not alive.
	
	Public Const NSIO_CONNECT_FAIL As Short = -200 ' Connect to Server fail.
	Public Const NSIO_SOCK_INIT_FAIL As Short = -201 ' socket initial error
	Public Const NSIO_SOCK_ERR As Short = -202 ' socket error
	Public Const NSIO_TIMEOUT As Short = -203
	
	
	
	' Server Control
	Declare Function nsio_init Lib "IPSerial.dll" () As Integer
	Declare Function nsio_end Lib "IPSerial.dll" () As Integer
	Declare Function nsio_resetserver Lib "IPSerial.dll" (ByVal server_ip As String, ByVal password As String) As Integer
	Declare Function nsio_checkalive Lib "IPSerial.dll" (ByVal server_ip As String, ByVal timeout As Integer) As Integer
	
	' Port Control
	Declare Function nsio_open Lib "IPSerial.dll" (ByVal server_ip As String, ByVal port_index As Integer, ByVal timeouts As Integer) As Integer
	Declare Function nsio_close Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	
	Declare Function nsio_ioctl Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal baud As Integer, ByVal mode As Integer) As Integer
	Declare Function nsio_flowctrl Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal mode As Integer) As Integer
	Declare Function nsio_flush Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal func As Integer) As Integer
	Declare Function nsio_DTR Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal mode As Integer) As Integer
	Declare Function nsio_RTS Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal mode As Integer) As Integer
	Declare Function nsio_lctrl Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal mode As Integer) As Integer
	Declare Function nsio_baud Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal speed As Integer) As Integer
	Declare Function nsio_resetport Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal password As String) As Integer
	
	' Input/Output data
	Declare Function nsio_read Lib "IPSerial.dll" (ByVal port_id As Integer, ByRef buf As Byte, ByVal buf_len As Integer) As Integer
	Declare Function nsio_SetReadTimeouts Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal timeouts As Integer) As Integer
	Declare Function nsio_write Lib "IPSerial.dll" (ByVal port_id As Integer, ByRef buf As Byte, ByVal buf_len As Integer) As Integer
	Declare Function nsio_SetWriteTimeouts Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal timeouts As Integer) As Integer
	
	' Port Status Inquiry
	Declare Function nsio_lstatus Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	Declare Function nsio_iqueue Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	Declare Function nsio_oqueue Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	Declare Function nsio_data_status Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	
	
	' Miscellaneous
	Declare Function nsio_break Lib "IPSerial.dll" (ByVal port_id As Integer, ByVal time As Integer) As Integer
	Declare Function nsio_break_on Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	Declare Function nsio_break_off Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
	Declare Function nsio_breakcount Lib "IPSerial.dll" (ByVal port_id As Integer) As Integer
End Module