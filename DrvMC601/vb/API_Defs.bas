Attribute VB_Name = "API_Defs"
Option Explicit

' *** Общие функции ***
Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
Declare Function GetLastError Lib "kernel32" () As Long
Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long

' *** Структура DCB ***
Type DCB
    DCBlength As Long       ' Размер DCB
    BaudRate As Long        ' Скорость передачи: 110, 300, 600, 1200, 2400 и т.д.
    Flags As Long           ' Поле флагов
    wReserved As Integer
    XonLim As Integer       ' Порог посылки Xon (разрешение на передачу)
    XoffLim As Integer      ' Порог посылки Xoff (приостановить передачу)
    ByteSize As Byte        ' Число бит: 5,6,7,8
    Parity As Byte          ' Чётность: 0,1,2,3,4 -- соответственно N,O,E,M,S
    StopBits As Byte        ' Стоп-биты: 0,1,2 -- соответстенно 1, 1.5 или 2
    XonChar As Byte         ' Символ Xon
    XoffChar As Byte        ' Символ Xoff
    ErrorChar As Byte       ' Символ замены для символов, принятых с ошибкой чётности
    EofChar As Byte         ' Символ для EOF
    EvtChar As Byte         ' Символ для сигнализации о событии (обычно код 13 -- CR)
    wReserved2 As Integer
  End Type

Public Const FLAG_fBinary = &H1             ' Бинарный режим
Public Const FLAG_fParity = &H2             ' Проверять чётность
Public Const FLAG_fOutxCtsFlow = &H4        ' Ждать CTS для передачи
Public Const FLAG_fOutxDsrFlow = &H8        ' Ждать DSR для передачи
Public Const FLAG_fDtrControl = &H30        ' 2 бита: 3 режима DTR
Public Const FLAG_fDsrSensitivity = &H40    ' Игнорировать приём без DSR
Public Const FLAG_fTXContinueOnXoff = &H80
Public Const FLAG_fOutX = &H100             ' Использовать Xon/Xoff протокол при передаче
Public Const FLAG_fInX = &H200              ' Использовать Xon/Xoff протокол при приёме
Public Const FLAG_fErrorChar = &H400        ' Использовать замену на ErrorChar
Public Const FLAG_fNull = &H800             ' Игнорировать 0-байты
Public Const FLAG_fRtsControl = &H3000      ' 2 бита: 4 режима RTS
Public Const FLAG_fAbortOnError = &H4000    ' Стоп при любой ошибке

Declare Function BuildCommDCB Lib "kernel32" Alias "BuildCommDCBA" (ByVal Mode As String, lpDCB As DCB) As Long
Declare Function SetCommState Lib "kernel32" (ByVal hDev As Long, lpDCB As DCB) As Long
Declare Function GetCommState Lib "kernel32" (ByVal hDev As Long, lpDCB As DCB) As Long

' *** Открытие и закрытие ***
Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal FileName As String, ByVal DesiredAccess As Long, ByVal ShareMode As Long, ByVal SecurityAttributes As Long, ByVal CreationDisposition As Long, ByVal FlagsAndAttributes As Long, ByVal TemplateFile As Long) As Long

    Public Const GENERIC_READ = &H80000000
    Public Const GENERIC_WRITE = &H40000000
    Public Const OPEN_EXISTING = 3
    Public Const FILE_FLAG_OVERLAPPED = &H40000000
    Public Const INVALID_HANDLE_VALUE = -1

Declare Function SetupComm Lib "kernel32" (ByVal hFile As Long, ByVal dwInQueue As Long, ByVal dwOutQueue As Long) As Long

' *** Тайм-ауты ***
Type COMMTIMEOUTS
    ReadIntervalTimeout As Long
    ReadTotalTimeoutMultiplier As Long
    ReadTotalTimeoutConstant As Long
    WriteTotalTimeoutMultiplier As Long
    WriteTotalTimeoutConstant As Long
  End Type

Declare Function SetCommTimeouts Lib "kernel32" (ByVal hDev As Long, lpTO As COMMTIMEOUTS) As Long
Declare Function GetCommTimeouts Lib "kernel32" (ByVal hDev As Long, lpTO As COMMTIMEOUTS) As Long

' *** Асинхронный обмен ***
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

Public Const EV_RXCHAR = &H1           ' Пришли данные
Public Const EV_RXFLAG = &H2           ' Получен сигнальный символ (указанный в поле EvtChar DCB)
Public Const EV_TXEMPTY = &H4          ' Очередь передачи пуста
Public Const EV_CTS = &H8              ' Изменение на линии CTS
Public Const EV_DSR = &H10             ' Изменение на линии DSR
Public Const EV_RLSD = &H20            ' Изменение на линии RLSD
Public Const EV_BREAK = &H40           ' Получен сигнал BREAK
Public Const EV_ERR = &H80             ' Ошибка линии (чётности, обрамления, т.д.)
Public Const EV_RING = &H100           ' Получен сигнал Ring

Public Const EV_PERR = &H200           ' Ошибка принтера
Public Const EV_RX80FULL = &H400       ' Приёмный буфер полон на 80%
Public Const EV_EVENT1 = &H800         ' Специальное событие 1
Public Const EV_EVENT2 = &H1000        ' Специальное событие 2

' *** Чтение/Запись/Ожидание событий
Declare Function WaitCommEvent Lib "kernel32" (ByVal hDev As Long, Mask As Long, lpOverlapped As OVERLAPPED) As Long
Declare Function WriteFile Lib "kernel32" (ByVal hDev As Long, ByRef Buffer As Byte, ByVal BytesToWrite As Long, BytesWritten As Long, lpOverlapped As OVERLAPPED) As Long
Declare Function ReadFile Lib "kernel32" (ByVal hDev As Long, ByRef Buffer As Byte, ByVal BytesToRead As Long, BytesRead As Long, lpOverlapped As OVERLAPPED) As Long

Declare Function WaitForSingleObject Lib "kernel32" (ByVal hDev As Long, ByVal dwMilliseconds As Long) As Long
Declare Function GetOverlappedResult Lib "kernel32" (ByVal hDev As Long, lpOverlapped As OVERLAPPED, lpBytesTransferred As Long, ByVal bWait As Long) As Long

Public Const ERROR_IO_PENDING = 997    ' Запрос в ожидании
Public Const WAIT_TIMEOUT = &H102      ' Тайм-аут

' *** Состояние ***
Declare Function PurgeComm Lib "kernel32" (ByVal hFile As Long, _
        ByVal dwFlags As Long) As Long
    Public Const PURGE_TXABORT = &H1     ' Прекращает все операции записи, даже незавершённые
    Public Const PURGE_RXABORT = &H2     ' Прекращает все операции чтения
    Public Const PURGE_TXCLEAR = &H4     ' Очищает очередь передачи в драйвере
    Public Const PURGE_RXCLEAR = &H8     ' Очищает очередь приема в драйвере

Declare Function EscapeCommFunction Lib "kernel32" (ByVal hFile As Long, ByVal nFunc As Long) As Long
    Public Const SETXOFF = 1    ' Симулирует прием символа XOFF
    Public Const SETXON = 2     ' Симулирует прием символа XON
    Public Const SETRTS = 3     ' Устанавливает сигнал RTS
    Public Const CLRRTS = 4     ' Сбрасывает сигнал RTS
    Public Const SETDTR = 5     ' Устанавливает сигнал DTR
    Public Const CLRDTR = 6     ' Сбрасывает сигнал DTR
    Public Const RESETDEV = 7   ' Проводит сброс устройства (если возможно)
    Public Const SETBREAK = 8   ' Переводит выходную линию передатчика в состояние разрыва break
    Public Const CLRBREAK = 9   ' Снимает состояние разрыва break на выходной линии передатчика

Declare Function ClearCommError Lib "kernel32" (ByVal hDev As Long, Errors As Long, ByVal l As Long) As Long
    Public Const CE_RXOVER = &H1           ' Заполнение входной очереди или символ после EOF
    Public Const CE_OVERRUN = &H2          ' Переполнение входной очереди. Последний символ потерян
    Public Const CE_RXPARITY = &H4         ' Ошибка чётности
    Public Const CE_FRAME = &H8            ' Ошибка обрасления (фрейма)
    Public Const CE_BREAK = &H10           ' Зафиксирован Break

Declare Function GetCommModemStatus Lib "kernel32" (ByVal hDev As Long, ModemStat As Long) As Long
    Public Const MS_CTS_ON = &H10&
    Public Const MS_DSR_ON = &H20&
    Public Const MS_RING_ON = &H40&
    Public Const MS_RLSD_ON = &H80&

