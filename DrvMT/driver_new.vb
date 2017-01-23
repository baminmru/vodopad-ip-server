
Imports System
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Structure MArchive
    Public DateArch As DateTime
    Public HC As Int32
    Public MsgHC As String

    Public HCtv1 As Long
    Public MsgHCtv1 As String

    Public HCtv2 As Long
    Public MsgHCtv2 As String

    Public G1 As Single
    Public G2 As Single
    Public G3 As Single
    Public G4 As Single
    Public G5 As Single
    Public G6 As Single

    Public t1 As Single
    Public t2 As Single
    Public t3 As Single
    Public t4 As Single
    Public t5 As Single
    Public t6 As Single

    Public p1 As Single
    Public p2 As Single
    Public p3 As Single
    Public p4 As Single
    Public p5 As Single

    Public dt12 As Single
    Public dt45 As Single

    Public tx1 As Single
    Public tx2 As Single

    Public tair1 As Single
    Public tair2 As Single

    Public SP As Long
    Public SPtv1 As Long
    Public SPtv2 As Long

    Public dQ1 As Single
    Public dQ2 As Single


    Public archType As Short
End Structure

Public Structure Archive
    Public DateArch As DateTime
    Public Errtime As Long
    Public ErrtimeH As Long
    Public oktime As Long
    Public HC As Long
    Public MsgHC As String

    Public HCtv1 As Long
    Public MsgHCtv1 As String

    Public HCtv2 As Long
    Public MsgHCtv2 As String

    Public Tw1 As Single
    Public Tw2 As Single

    Public P1 As Single
    Public T1 As Single
    Public M2 As Single
    Public V1 As Single

    Public P2 As Single
    Public T2 As Single
    Public M3 As Single
    Public V2 As Single

    Public V3 As Single
    Public M1 As Single

    Public Q1 As Single
    Public Q2 As Single

    Public QG1 As Single
    Public QG2 As Single

    Public SP As Long
    Public SPtv1 As Long
    Public SPtv2 As Long

    Public tx1 As Long
    Public tx2 As Long
    Public tair1 As Long
    Public tair2 As Long

    Public T3 As Single
    Public T4 As Single
    Public T5 As Single
    Public T6 As Single
    Public P3 As Single
    Public P4 As Single
    Public P5 As Single
    Public v4 As Single
    Public v5 As Single
    Public v6 As Single
    Public M4 As Single
    Public M5 As Single
    Public M6 As Single
    Public V1h As Double
    Public V2h As Double
    Public V4h As Double
    Public V5h As Double
    Public Q1H As Double
    Public Q2H As Double

    Public archType As Short
End Structure

Public Structure TArchive
    Public DateArch As DateTime


    Public V1 As Double
    Public V2 As Double
    Public V3 As Double
    Public V4 As Double
    Public V5 As Double
    Public V6 As Double

    Public M1 As Double
    Public M2 As Double
    Public M3 As Double
    Public M4 As Double
    Public M5 As Double
    Public M6 As Double
    Public Q1 As Double
    Public Q2 As Double

    Public TW1 As Double
    Public TW2 As Double
    Public Q3 As Double
    Public Q4 As Double
    Public HC As Int32

    Public archType As Short
End Structure


'Смещение	Размер поля (в байтах)	Название	Тип значения	Диапазон	Единицы измерения	Примечание
'0х00	1	День	Беззнаковое целое	1 – 31	-	-
'0х01	1	Месяц	Беззнаковое целое	1 – 12	-	-
'0х02	1	Год	Беззнаковое целое	0 – 99	-	-
'0х03	1	Час	Беззнаковое целое	0 – 23	час	-
'0х04	4	Тепло по 1 теплосистеме	32-битный IEEE-754 формат	0 – 999999999	МДж	Накопительный счетчик
'0х08	4	Тепло по 2 теплосистеме	32-битный IEEE-754 формат	0 – 999999999	МДж	Накопительный счетчик
'0х0С	4	Общий расход по 1 трубопроводу	32-битный IEEE-754 формат	0 – 999999	В установленных единицах (т, м3)	Накопительный счетчик
'0х10	4	Общий расход по 2 трубопроводу	32-битный IEEE-754 формат	0 – 999999	В установленных единицах (т, м3)	Накопительный счетчик
'0х14	4	Общий расход по 3 трубопроводу	32-битный IEEE-754 формат	0 – 999999	В установленных единицах (т, м3)	Накопительный счетчик
'0х18	4	Общий расход по 4 трубопроводу	32-битный IEEE-754 формат	0 – 999999	В установленных единицах (т, м3)	Накопительный счетчик
'0х1С	2	Температура по 1 трубопроводу	Беззнаковое целое	0 – 25000	10-2 ºС	Среднее значение за 1 час
'0х1E	2	Температура по 3 трубопроводу	Беззнаковое целое	0 – 25000	10-2 ºС	Среднее значение за 1 час
'0х20	2	Температура по 2 трубопроводу	Беззнаковое целое	0 – 25000	10-2 ºС	Среднее значение за 1 час
'0х22	2	Температура по 4 трубопроводу	Беззнаковое целое	0 – 25000	10-2 ºС	Среднее значение за 1 час
'0х24	2	Слово состояния	Беззнаковое целое	0 – 65535	-	Смотри таблицу 4
'0х26	4	Аварийное время	Беззнаковое целое	0 – 999999999	мин	Накопительный счетчик

Public Structure HArch
    Public day As Byte
    Public month As Byte
    Public year As Byte
    Public hour As Byte
    Public Q1 As Single
    Public Q2 As Single
    Public V1 As Single
    Public V2 As Single
    Public V3 As Single
    Public V4 As Single
    Public T1 As UInteger
    Public T2 As UInteger
    Public T3 As UInteger
    Public T4 As UInteger
    Public Status As UInteger
    Public CrashTime As UInteger
    Public P1 As Single
    Public P2 As Single
    Public P3 As Single
    Public P4 As Single
    Public OKTime As UInteger
    Public OK As Boolean
    Public V1h As Single
    Public V2h As Single
    Public V3h As Single
    Public V4h As Single
    Public Q1H As Single
    Public Q2H As Single
    Public M1 As Single
    Public M2 As Single
    Public M3 As Single
    Public M4 As Single
End Structure


Public Structure DArch
    Public day As Byte
    Public month As Byte
    Public year As Byte
    Public hour As Byte
    Public Q1 As Single
    Public Q2 As Single
    Public V1 As Single
    Public V2 As Single
    Public V3 As Single
    Public V4 As Single
    Public T1 As UInteger
    Public T2 As UInteger
    Public T3 As UInteger
    Public T4 As UInteger
    Public CrashTime As UInteger
    Public Status As UInteger
    Public P1 As Single
    Public P2 As Single
    Public P3 As Single
    Public P4 As Single
    Public OKTime As UInteger
    Public OK As Boolean
    Public V1h As Single
    Public V2h As Single
    Public V3h As Single
    Public V4h As Single
    Public Q1H As Single
    Public Q2H As Single
    Public M1 As Single
    Public M2 As Single
    Public M3 As Single
    Public M4 As Single
End Structure

Public Class DataPass
    Public passdata(100) As Byte
    Public bDup As Boolean
    Public Cnt As Int16
    Public Size As Long

    Function GetHash() As String
        ' Create a new instance of the MD5 object.
        Dim md5Hasher As MD5 = MD5.Create()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hasher.ComputeHash(passdata)

        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function


    ' Verify a hash against a string.
    Function VerifyHash(ByVal hash As String) As Boolean
        ' Hash the input.
        Dim hashOfInput As String = GetHash()

        ' Create a StringComparer an comare the hashes.
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If

    End Function

End Class

Public Class driver
    Inherits STKTVMain.TVDriver

    Public Const c_lng256 As Long = 256&

    Private mIsConnected As Boolean

    'Private MyManager As LATIR.Manager
    Private isTCP As Boolean
    Private SleepTime As Long

    ' Тип собираемого параметра
    Public RI(5) As Integer

    ' Плотность теплоносителя
    Public PT(5) As Double



    Private Sub FillHArch(ByRef buf() As Byte, ByRef h As HArch)
        Dim st As Integer
        st = 3
        h.day = buf(0 + st)
        h.month = buf(1 + st)
        h.year = buf(2 + st)
        h.hour = buf(3 + st)
        h.Q1 = BToSingle(buf, 4 + st)
        h.Q2 = BToSingle(buf, 8 + st)


        If RI(1) = 1 Then
            h.V1 = BToSingle(buf, st + 12)
            h.M1 = h.V1 * PT(1)
        Else
            h.M1 = BToSingle(buf, st + 12)
            h.V1 = h.M1 / PT(1)
        End If

        If RI(2) = 1 Then
            h.V2 = BToSingle(buf, st + 16)
            h.M2 = h.V2 * PT(2)
        Else
            h.M2 = BToSingle(buf, st + 16)
            h.V2 = h.M2 / PT(2)
        End If

        If RI(3) = 1 Then
            h.V3 = BToSingle(buf, st + 20)
            h.M3 = h.V3 * PT(3)
        Else
            h.M3 = BToSingle(buf, st + 20)
            h.V3 = h.M3 / PT(3)
        End If

        If RI(4) = 1 Then
            h.V4 = BToSingle(buf, st + 24)
            h.M4 = h.V4 * PT(4)
        Else
            h.M4 = BToSingle(buf, st + 24)
            h.V4 = h.M4 / PT(4)
        End If




        'h.V1 = BToSingle(buf, 12 + st)
        'h.V2 = BToSingle(buf, 16 + st)
        'h.V3 = BToSingle(buf, 20 + st)
        'h.V4 = BToSingle(buf, 24 + st)

        Flip2(buf, 28 + st)
        h.T1 = BitConverter.ToUInt16(buf, 28 + st)
        Flip2(buf, 30 + st)
        h.T3 = BitConverter.ToUInt16(buf, 30 + st)
        Flip2(buf, 32 + st)
        h.T2 = BitConverter.ToUInt16(buf, 32 + st)
        Flip2(buf, 34 + st)
        h.T4 = BitConverter.ToUInt16(buf, 34 + st)
 if isTCP then
        Flip4(buf, 36 + st)
        h.Status = BitConverter.ToUInt32(buf, 36 + st) '256 * buf(36 + st) + buf(37 + st) '
  else
        Flip2(buf, 36 + st)
        h.Status = BitConverter.ToUInt16(buf, 36 + st) '256 * buf(36 + st) + buf(37 + st) '
        Flip4(buf, 38 + st)
        h.CrashTime = BitConverter.ToUInt32(buf, 38 + st)
 end if

    End Sub
    Private Sub FillHArchTCP(ByRef buf() As Byte, ByRef h As HArch)
        Dim st As Integer
        st = 3
        h.P1 = BitConverter.ToSingle(buf, 0 + st)
        h.P2 = BitConverter.ToSingle(buf, 4 + st)
        h.P3 = BitConverter.ToSingle(buf, 8 + st)
        h.P4 = BitConverter.ToSingle(buf, 12 + st)
        Flip4(buf, &h14 + st)
        h.CrashTime = BitConverter.ToInt32(buf, &H14 + st)
    End Sub

    Private Sub FillHArchMT(ByRef buf() As Byte, ByRef h As HArch)
        Dim st As Integer
        st = 3
        Flip2(buf, 0 + st)
        h.P1 = BitConverter.ToInt16(buf, 0 + st)
        Flip2(buf, 2 + st)
        h.P2 = BitConverter.ToInt16(buf, 2 + st)
        Flip2(buf, 4 + st)
        h.P3 = BitConverter.ToInt16(buf, 4 + st)
        Flip2(buf, 6 + st)
        h.P4 = BitConverter.ToInt16(buf, 6 + st)
    End Sub


    Private Sub FillDArch(ByRef buf() As Byte, ByRef h As DArch)
        Dim st As Int16
        st = 3
        h.day = buf(st + 0)
        h.month = buf(st + 1)
        h.year = buf(st + 2)
        h.hour = buf(st + 3)

        'Flip4(buf, 4)
        h.Q1 = BToSingle(buf, st + 4)
        h.Q2 = BToSingle(buf, st + 8)

        If RI(1) = 1 Then
            h.V1 = BToSingle(buf, st + 12)
            h.M1 = h.V1 * PT(1)
        Else
            h.M1 = BToSingle(buf, st + 12)
            h.V1 = h.M1 / PT(1)
        End If

        If RI(2) = 1 Then
            h.V2 = BToSingle(buf, st + 16)
            h.M2 = h.V2 * PT(2)
        Else
            h.M2 = BToSingle(buf, st + 16)
            h.V2 = h.M2 / PT(2)
        End If

        If RI(3) = 1 Then
            h.V3 = BToSingle(buf, st + 20)
            h.M3 = h.V3 * PT(3)
        Else
            h.M3 = BToSingle(buf, st + 20)
            h.V3 = h.M3 / PT(3)
        End If

        If RI(4) = 1 Then
            h.V4 = BToSingle(buf, st + 24)
            h.M4 = h.V4 * PT(4)
        Else
            h.M4 = BToSingle(buf, st + 24)
            h.V4 = h.M4 / PT(4)
        End If


        'h.V2 = BToSingle(buf, st + 16)

        'h.V3 = BToSingle(buf, st + 20)
        'h.V4 = BToSingle(buf, st + 24)

        Flip2(buf, st + 28)
        h.T1 = BitConverter.ToUInt16(buf, st + 28)
        Flip2(buf, st + 30)
        h.T3 = BitConverter.ToUInt16(buf, st + 30)
        Flip2(buf, st + 32)
        h.T2 = BitConverter.ToUInt16(buf, st + 32)
        Flip2(buf, st + 34)
        h.T4 = BitConverter.ToUInt16(buf, st + 34)
if IsTCP then
        Flip4(buf, 36 + st)
        h.Status = BitConverter.ToUInt32(buf, 36 + st)
else
        Flip2(buf, st + 36)
        h.Status = BitConverter.ToUInt16(buf, st + 36)
        Flip4(buf, st + 38)
        h.CrashTime = BitConverter.ToUInt32(buf, st + 38)
end if
        'Flip4(buf, st + 36)
        'h.CrashTime = BitConverter.ToUInt32(buf, st + 36)

    End Sub
    Private Sub FillDArchTCP(ByRef buf() As Byte, ByRef h As DArch)
        Dim st As Integer
        st = 3
        h.P1 = BToSingle(buf, 0 + st)
        h.P2 = BToSingle(buf, 4 + st)
        h.P3 = BToSingle(buf, 8 + st)
        h.P4 = BToSingle(buf, 12 + st)

        Flip4(buf, &h14 + st)
        h.CrashTime = BitConverter.ToInt32(buf, &H14 + st)

    End Sub

    Private Sub FillDArchMT(ByRef buf() As Byte, ByRef h As DArch)
        Dim st As Integer
        st = 3
        Flip2(buf, 0 + st)
        h.P1 = BitConverter.ToInt16(buf, 0 + st)
        Flip2(buf, 2 + st)
        h.P2 = BitConverter.ToInt16(buf, 2 + st)
        Flip2(buf, 4 + st)
        h.P3 = BitConverter.ToInt16(buf, 4 + st)
        Flip2(buf, 6 + st)
        h.P4 = BitConverter.ToInt16(buf, 6 + st)

      

        h.CrashTime = BitConverter.ToInt16(buf, &H14 + st)

    End Sub
    Dim tArch As TArchive
    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

    Dim tv As Short

    Dim archType_hour = 3
    Dim archType_day = 4


    Dim Arch As Archive
    Dim mArch As MArchive

    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False

    Dim buffer(0 To 73) As Byte
    Dim bufferindex As Short = 0

    Dim m_isArchToDBWrite As Boolean = False
    Public Overrides Property isArchToDBWrite() As Boolean

        Get
            Return m_isArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isArchToDBWrite = value
        End Set
    End Property
    Dim m_isMArchToDBWrite As Boolean = False
   


    'Public inputbuffer(69) As Byte

    Public Overrides Function CounterName() As String
        Return "МТТСР"
    End Function

    
 

    Private Function ChSum(ByVal buf() As Byte, ByVal sz As Long) As Byte
        Dim i As Long
        Dim bChSum As Byte = 0
        For i = 0 To sz - 1

            bChSum = bChSum Xor buf(i)
        Next i
        If (bChSum <> 0) Then
            bChSum = (Not bChSum) + 1
        End If

        Return bChSum
    End Function

    Private Sub ReadVersion()
        Dim buf(1000) As Byte
        Dim j As Integer
        buf(0) = 4
        buf(1) = &H4F
        buf(2) = 0
        buf(3) = ChSum(buf, 3)

        MyTransport.CleanPort()
        MyTransport.Write(buf, 0, 4)

        WaitForData()

        Dim btr As Long
        Dim sz As Long = 0
        Dim bufStr As String = ""
        'Dim t As Integer

      
            btr = MyTransport.BytesToRead
            While btr > 0

                MyTransport.Read(buf, sz, btr)
                sz += btr
                RaiseIdle()
                System.Threading.Thread.Sleep(CalcInterval(10))
            btr = MyTransport.BytesToRead
            End While

            

            If sz > 0 Then
                If buf(0) = 0 And buf(sz - 1) = 0 Then
                    buf = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 1, sz - 2)
                    For j = 0 To sz - 3
                        bufStr = bufStr + Chr(buf(j))
                    Next
                    'txtOUT.Text = bufStr
                End If
            End If
    End Sub

    Private Sub ReadNumber()
        Dim buf(1000) As Byte
        Dim j As Integer
        buf(0) = 4
        buf(1) = &H50
        buf(2) = 0
        buf(3) = ChSum(buf, 3)

        MyTransport.CleanPort()
        MyTransport.Write(buf, 0, 4)
    
        WaitForData()

        Dim btr As Long
        Dim sz As Long = 0
        Dim bufStr As String = ""


        btr = MyTransport.BytesToRead
        While btr > 0

            MyTransport.Read(buf, sz, btr)
            sz += btr
            RaiseIdle()
            System.Threading.Thread.Sleep(CalcInterval(10))
            btr = MyTransport.BytesToRead
        End While
       


        If sz > 0 Then
            If buf(0) = 0 And buf(sz - 1) = 0 Then
                buf = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 1, sz - 2)
                For j = 0 To sz - 3
                    bufStr = bufStr + Chr(buf(j))
                Next
                'txtOUT.Text = txtOUT.Text & vbCrLf & bufStr
            End If
        End If
    End Sub

   

    Private Connecting As Boolean = False
    Public Function TryConnect() As Boolean

        Connecting = True
        EraseInputQueue()

        Try

            Dim sret As String
            sret = ReadCommand(&H4F, 255) & ""
            If (sret.Length > 5) Then
                If (sret.Substring(0, 5) = "Error") Then
                    EraseInputQueue()
                    Connecting = False
                    Return False
                End If
                If Left(sret, 9) = "ВЗЛЁТ ТСР" Then

                    isTCP = True
                    Connecting = False
                    Return True
                End If

                If sret.Substring(0, 7) = "MT200DS" Then
                    isTCP = False
                    Connecting = False
                    Return True
                End If

                If sret = "Not implemented" Then
                    sret = ReadCommand(&H46, 255)
                    If sret.Substring(0, 8) = "MT-200DS" Then
                        isTCP = False
                        Connecting = False
                        Return True
                    Else
                        Connecting = False
                        Return False
                    End If
                End If


            End If

            If sret.Length = 0 Then
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            End If

            Connecting = False
            Return False
        Catch exc As Exception
            Connecting = False
            Return False
        End Try

    End Function

    Public Overrides Sub Connect()

        mIsConnected = False
        Dim cnt As Integer = 5
        While cnt > 0
            mIsConnected = TryConnect()
            If mIsConnected Then
                Dim i As Integer

                For i = 1 To 5
                    RI(i) = RashodInfo(i)
                    PT(i) = 1.0
                    PT(i) = Plotnost(i)
                    Debug.Print("Chanel:" + i.ToString + "  RI=" + RI(i).ToString + "  PT=" + PT(i).ToString())
                    If Math.Abs(PT(i)) > 500 Then PT(i) = PT(i) / 1000
                    If PT(i) <= 0 Then PT(i) = 1.0
                    If PT(i) > 2 Then PT(i) = 1.0
                Next

                Return
            End If

            cnt = cnt - 1
        End While
    End Sub

    Private m_readRAMByteCount As Short

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
        'Dim trycnt As Int32
        Dim retsum As String
        Dim ok As Boolean
        Try


            cleararchive(Arch)
            EraseInputQueue()
            Dim d1 As DArch, d2 As DArch
            Dim h1 As HArch, h2 As HArch
            Dim dt1 As Date, dt2 As Date
            Dim devdate As Date
            devdate = GetDeviceDate()
            devdate = devdate.AddMinutes(-1)
            If ArchType = archType_hour Then
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            End If
            If ArchType = archType_day Then
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            End If

            Arch.archType = ArchType

            If (ArchType = 3 And dt2 <= devdate.AddHours(-1)) Or (ArchType = 4 And dt2 <= devdate.AddHours(-25)) Then

                If ArchType = archType_hour Then
                    dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
                    'dt2 = dt2.AddHours(-1)
                    dt1 = dt2.AddHours(-1)
                    Try
                        h1 = GetArchiv(dt1, False)
                    Catch ex As Exception
                        h1.OK = False
                    End Try
                    Try
                        h2 = GetArchiv(dt2, False)
                    Catch ex As Exception
                        h2.OK = False
                    End Try

                    If h1.OK And h2.OK Then
                        If dt1.Year = 2000 + h1.year And dt1.Month = h1.month And dt1.Day = h1.day And dt1.Hour = h1.hour And dt2.Year = 2000 + h2.year And dt2.Month = h2.month And dt2.Day = h2.day And dt2.Hour = h2.hour Then

                            Arch.Q1 = h2.Q1 - h1.Q1
                            Arch.Q2 = h2.Q2 - h1.Q2
                            Arch.M1 = h2.M1 - h1.M1
                            Arch.M2 = h2.M2 - h1.M2
                            Arch.M4 = h2.M3 - h1.M3
                            Arch.M5 = h2.M4 - h1.M4

                            Arch.V1 = h2.V1 - h1.V1
                            Arch.V2 = h2.V2 - h1.V2
                            Arch.v4 = h2.V3 - h1.V3
                            Arch.v5 = h2.V4 - h1.V4

                            Arch.V1h = h2.V1
                            Arch.V2h = h2.V2
                            Arch.V4h = h2.V3
                            Arch.V5h = h2.V4
                            Arch.Q1H = h2.Q1
                            Arch.Q2H = h2.Q2


                            Arch.T1 = CDbl(h2.T1) / 100
                            Arch.T2 = CDbl(h2.T2) / 100
                            Arch.T4 = CDbl(h2.T3) / 100
                            Arch.T5 = CDbl(h2.T4) / 100

                            Arch.HC = h2.Status
                            If isTCP Then
                                Arch.P1 = h2.P1
                                Arch.P2 = h2.P2
                                Arch.P4 = h2.P3
                                Arch.P5 = h2.P4
                            End If
                            Arch.DateArch = dt2 ' .AddHours(1)
                            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)

                            Arch.Errtime = CLng(h2.CrashTime) - CLng(h1.CrashTime)
                            Arch.oktime = 60 - Arch.Errtime
                            Arch.ErrtimeH = h2.CrashTime
                            ok = True
                        Else
                            ok = False
                        End If
                    Else
                        ok = False
                    End If

                End If

                If ArchType = archType_day Then
                    dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                    'dt2 = dt2.AddDays(-1)
                    dt1 = dt2.AddDays(-1)
                    Try
                        d1 = GetArchiv(dt1, True)
                    Catch ex As Exception
                        d1.OK = False
                    End Try
                    Try
                        d2 = GetArchiv(dt2, True)
                    Catch ex As Exception
                        d2.OK = False
                    End Try

                    If d1.OK And d2.OK Then
                        If dt1.Year = 2000 + d1.year And dt1.Month = d1.month And dt1.Day = d1.day And dt2.Year = 2000 + d2.year And dt2.Month = d2.month And dt2.Day = d2.day Then

                            Arch.M1 = d2.M1 - d1.M1
                            Arch.M2 = d2.M2 - d1.M2

                            Arch.M4 = d2.M3 - d1.M3
                            Arch.M5 = d2.M4 - d1.M4

                            Arch.V1 = d2.V1 - d1.V1
                            Arch.V2 = d2.V2 - d1.V2

                            Arch.v4 = d2.V3 - d1.V3
                            Arch.v5 = d2.V4 - d1.V4

                            Arch.Q1 = d2.Q1 - d1.Q1
                            Arch.Q2 = d2.Q2 - d1.Q2

                            Arch.T1 = CDbl(d2.T1) / 100
                            Arch.T2 = CDbl(d2.T2) / 100

                            Arch.T4 = CDbl(d2.T3) / 100
                            Arch.T5 = CDbl(d2.T4) / 100

                            Arch.V1h = d2.V1
                            Arch.V2h = d2.V2
                            Arch.V4h = d2.V3
                            Arch.V5h = d2.V4
                            Arch.Q1H = d2.Q1
                            Arch.Q2H = d2.Q2

                            Arch.HC = d2.Status
                            Arch.DateArch = dt2
                            If isTCP Then
                                Arch.P1 = d2.P1
                                Arch.P2 = d2.P2
                                Arch.P4 = d2.P3
                                Arch.P5 = d2.P4
                            End If
                            Arch.Errtime = CLng(d2.CrashTime) - CLng(d1.CrashTime)
                            Arch.ErrtimeH = d2.CrashTime
                            Arch.oktime = 1440 - Arch.Errtime
                            ok = True
                        Else
                            ok = False
                        End If
                    Else
                        ok = False
                    End If

                End If
            Else
                ok = False
                retsum = "Архив еще не сформирован"
                EraseInputQueue()
                isArchToDBWrite = False
                Return retsum
            End If

            If ok Then
                retsum = "Архив прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = True
            Else
                retsum = "Ошибка: не удалось получить архив"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = False
            End If

            Return retsum
        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function
    Public Function ProcessRcvData(ByVal buf() As Byte, ByVal ret As Short) As String


        Return "Ошибка!Пакет не распознан"
    End Function
    Public Function GetAndProcessData() As String
        Dim buf(0 To 73) As Byte
        Dim i As Int16
        For i = 0 To 73
            buf(i) = 0
        Next

        Dim ret As Long

        If (IsBytesToRead = False) Then
            Return ""
        End If

        Try

            ret = MyRead(buf, 0, WillCountToRead, CalcInterval(WillCountToRead))
            If (buf(2) = &H21) Then
                'tim.Stop()
                EraseInputQueue()
                Return "Ошибка. Код ошибки:" + Hex(buf(3))
            End If
            If (ret > 0) Then
                If (ret = WillCountToRead) Then
                    If (ispackageError = True) Then
                        'tim.Stop()
                        For i = bufferindex + 1 To bufferindex + ret
                            buffer(i) = buf(i - bufferindex - 1)
                        Next
                        If (pagesToRead < 2) Then IsBytesToRead = False
                        bufferindex = 0
                        For i = 0 To 73
                            buffer(i) = 0
                        Next
                        If (pagesToRead < 2) Then EraseInputQueue()
                        ispackageError = False
                        Return ProcessRcvData(buffer, bufferindex)
                    End If
                    If (pagesToRead > 1) Then
                        pagesToRead = pagesToRead - 1
                        Return ProcessRcvData(buf, ret)
                    End If
                    'tim.Stop()
                    IsBytesToRead = False
                    EraseInputQueue()
                    Return ProcessRcvData(buf, ret)
                End If
                If (ret < WillCountToRead) Then
                    For i = bufferindex To bufferindex + ret - 1
                        buffer(i) = buf(i)
                    Next
                    ispackageError = True
                    WillCountToRead = WillCountToRead - ret
                    bufferindex = bufferindex + ret - 1
                End If
            End If
        Catch ex As Exception
            Return "Ошибка." + ex.Message
        End Try
        Return ""
    End Function
    Public Function DeCodeHCNumber(ByVal CodeHC As Long) As String

        DeCodeHCNumber = ""
        'CodeHC = CodeHC And ( 2 ^ 5 + 2 ^ 4 + 2 ^ 3 + 2 ^ 2 + 2 ^ 1 + 2 ^ 0)
        If CodeHC And 2 ^ 0 Then
            'DeCodeHCNumber = "НС00" & ";"
            DeCodeHCNumber = "1"
        Else
            DeCodeHCNumber = "0"
        End If

        If CodeHC And 2 ^ 1 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС01" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 2 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС02" + ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If
        If CodeHC And 2 ^ 3 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС03" + ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If
        If CodeHC And 2 ^ 4 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС04" + ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If
        If CodeHC And 2 ^ 5 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС05" + ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If
        If CodeHC And 2 ^ 6 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС06" + ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If
        If CodeHC And 2 ^ 7 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС07" + ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If



        If CodeHC And 2 ^ 8 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС08" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 9 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС09" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 10 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС10" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 11 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС11 " & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 12 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС12" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 13 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС13" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 14 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС14" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If

        If CodeHC And 2 ^ 15 Then
            'DeCodeHCNumber = DeCodeHCNumber + "НС15" & ";"
            DeCodeHCNumber = DeCodeHCNumber + "1"
        Else
            DeCodeHCNumber = DeCodeHCNumber + "0"
        End If



    End Function
    Public Function DeCodeHCText(ByVal CodeHC As Long) As String
        '0	Расход ПР1 выше максимального расхода
        '1	Расход ПР1 ниже минимального расхода
        '2	Расход ПР4 выше максимального расхода
        '3	Расход ПР4 ниже минимального расхода
        '4	Расход ПР2 выше максимального расхода
        '5	Расход ПР2 ниже минимального расхода
        '6	Расход ПР5 выше максимального расхода
        '7	Расход ПР5 ниже минимального расхода
        '8	Расход ПР1 ниже расхода ПР2
        '9	Расход ПР4 выше расхода ПР5
        '10	Температура ПТ4 ниже температуры ПТ5
        '11	Температура ПТ1 ниже температуры ПТ2
        '12	Напряжение сети отсутствовало
        '13:     Прочие(ошибки)
        '14	Отказ канала температуры
        '15	Отказ канала давления
        DeCodeHCText = ""
        If CodeHC = 0 Then
            DeCodeHCText = "Нет НС"
        End If
        If CodeHC And 2 ^ 0 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР1 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР1 ниже минимального расхода" & ";"
        End If


        If CodeHC And 2 ^ 2 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР4 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР4 ниже минимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР2 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР2 ниже минимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР5 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР5 ниже минимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР1 ниже расхода ПР2" & ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCText = DeCodeHCText _
                    & "Расход ПР4 выше расхода ПР5" & ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCText = DeCodeHCText _
                    & "Температура ПТ4 ниже температуры ПТ5" & ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCText = DeCodeHCText _
                    & "Температура ПТ1 ниже температуры ПТ2" & ";"
        End If
        If CodeHC And 2 ^ 12 Then
            DeCodeHCText = DeCodeHCText _
                    & "Напряжение сети отсутствовало" & ";"
        End If


        If CodeHC And 2 ^ 13 Then
            DeCodeHCText = DeCodeHCText _
                    & "Отказ EEPROM" & ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCText = DeCodeHCText _
                    & "Отказ канала температуры" & ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCText = DeCodeHCText _
                    & "Отказ канала давления" & ";"
        End If

    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String


        '0	Расход ПР1 выше максимального расхода
        '1	Расход ПР1 ниже минимального расхода
        '2	Расход ПР4 выше максимального расхода
        '3	Расход ПР4 ниже минимального расхода
        '4	Расход ПР2 выше максимального расхода
        '5	Расход ПР2 ниже минимального расхода
        '6	Расход ПР5 выше максимального расхода
        '7	Расход ПР5 ниже минимального расхода
        '8	Расход ПР1 ниже расхода ПР2
        '9	Расход ПР4 выше расхода ПР5
        '10	Температура ПТ4 ниже температуры ПТ5
        '11	Температура ПТ1 ниже температуры ПТ2
        '12	Напряжение сети отсутствовало
        '13:     Прочие(ошибки)
        '14	Отказ канала температуры
        '15	Отказ канала давления


        DeCodeHC = ""

        If CodeHC And 2 ^ 0 Then
            DeCodeHC = DeCodeHC _
                    & "HC:0 - Расход ПР1 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHC = DeCodeHC _
                    & "HC:1 - Расход ПР1 ниже минимального расхода" & ";"
        End If


        If CodeHC And 2 ^ 2 Then
            DeCodeHC = DeCodeHC _
                    & "HC:2 - Расход ПР4 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 3 Then
            DeCodeHC = DeCodeHC _
                    & "HC:3 - Расход ПР4 ниже минимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 4 Then
            DeCodeHC = DeCodeHC _
                    & "HC:4 - Расход ПР2 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 5 Then
            DeCodeHC = DeCodeHC _
                    & "HC:5 - Расход ПР2 ниже минимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 6 Then
            DeCodeHC = DeCodeHC _
                    & "HC:6 - Расход ПР5 выше максимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 7 Then
            DeCodeHC = DeCodeHC _
                    & "HC:7 - Расход ПР5 ниже минимального расхода" & ";"
        End If

        If CodeHC And 2 ^ 8 Then
            DeCodeHC = DeCodeHC _
                    & "HC:8 - Расход ПР1 ниже расхода ПР2" & ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHC = DeCodeHC _
                    & "HC:9 - Расход ПР4 выше расхода ПР5" & ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHC = DeCodeHC _
                    & "HC:10 - Температура ПТ4 ниже температуры ПТ5" & ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHC = DeCodeHC _
                    & "HC:11 - Температура ПТ1 ниже температуры ПТ2" & ";"
        End If
        If CodeHC And 2 ^ 12 Then
            DeCodeHC = DeCodeHC _
                    & "HC:12 - Напряжение сети отсутствовало" & ";"
        End If


        If CodeHC And 2 ^ 13 Then
            DeCodeHC = DeCodeHC _
                    & "HC:13 - Прочие(ошибки)" & ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHC = DeCodeHC _
                    & "HC:14 - Отказ канала температуры" & ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHC = DeCodeHC _
                    & "HC:15 - Отказ канала давления" & ";"
        End If
    End Function

    Public Function Bytes2Float(ByVal fbytes() As Byte) As Single
        If UBound(fbytes) < 3 Then
            Return 0
        End If
        Return System.BitConverter.ToSingle(fbytes, 0)
    End Function


    Private Function BToSingle(ByVal hexValue() As Byte, ByVal index As Int16) As Single

        Try

            Dim iInputIndex As Integer = 0

            Dim iOutputIndex As Integer = 0

            Dim bArray(3) As Byte



            For iInputIndex = 0 To 3

                bArray(iOutputIndex) = hexValue(index + iInputIndex)

                iOutputIndex += 1

            Next
            'Array.Reverse(bArray)
            Return BitConverter.ToSingle(bArray, 0)

        Catch ex As Exception

        End Try
    End Function







    Private Sub Flip4(ByRef b() As Byte, ByVal index As Int16)
        Dim t(4) As Byte
        Dim i As Int16
        For i = 0 To 3
            t(i) = b(index + i)
        Next
        For i = 0 To 3
            b(index + i) = t(3 - i)
        Next
    End Sub

    Private Sub Flip2(ByRef b() As Byte, ByVal index As Int16)
        Dim t(2) As Byte
        Dim i As Int16
        For i = 0 To 1
            t(i) = b(index + i)
        Next
        For i = 0 To 1
            b(index + i) = t(1 - i)
        Next
    End Sub


    Public Overrides Function WriteArchToDB() As String

        'If Arch.archType <> 4 Then
        '    Arch.DateArch = Arch.DateArch.AddSeconds(1)
        'End If

        WriteArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,dm12,V1H,V2H,V4H,V5H,q1h,q2h,sp_TB1,sp_TB2,q1,q2,q4,q5,TSUM1,TSUM2,hc_code,hc,errtime, errtimeh,oktime,hcraw) values ("
        WriteArchToDB = WriteArchToDB + "'" + DeviceID.ToString() + "',"
        WriteArchToDB = WriteArchToDB + "'" + Arch.archType.ToString() + "',"
        WriteArchToDB = WriteArchToDB + "SYSDATE" + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + OracleDate(Arch.DateArch) + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.T6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tx1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tx2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tair1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.tair2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.P5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.v6, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M3, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M4, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M5, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.M6, "##############0.000").Replace(",", ".") + ","
        If Not Single.IsNaN(Arch.M1) And Not Single.IsNaN(Arch.M2) Then
            WriteArchToDB = WriteArchToDB + Format(Arch.M1 - Arch.M2, "##############0.000").Replace(",", ".") + ","
        Else
            WriteArchToDB = WriteArchToDB + "NULL,"
        End If

        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V1h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V2h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V4h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.V5h, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1H, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2H, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv1.ToString + ","
        WriteArchToDB = WriteArchToDB + Arch.SPtv2.ToString + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.QG1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.QG2, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Tw1, "##############0.000").Replace(",", ".") + ","
        WriteArchToDB = WriteArchToDB + NanFormat(Arch.Tw2, "##############0.000").Replace(",", ".") + ","



        If DeCodeHCNumber(Arch.HC) = "0000000000000000" Then
            WriteArchToDB = WriteArchToDB + "'','Нет НС'"
        Else
            WriteArchToDB = WriteArchToDB + "'" + S180(DeCodeHCNumber(Arch.HC)) + "','" + S180(DeCodeHCNumber(Arch.HC) + " " + DeCodeHCText(Arch.HC)) + "'"
        End If
        WriteArchToDB = WriteArchToDB + "," + Format(Arch.Errtime, "##############0").Replace(",", ".")

        WriteArchToDB = WriteArchToDB + "," + Format(Arch.ErrtimeH, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + Format(Arch.oktime, "##############0").Replace(",", ".")
        WriteArchToDB = WriteArchToDB + "," + "'" + Arch.HC.ToString().Replace(",", ".") + "'"
        WriteArchToDB = WriteArchToDB + ")"
        Debug.Print(WriteArchToDB)
    End Function

    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function
    Public Overrides Function WriteMArchToDB() As String
        WriteMArchToDB = ""
        Try
            WriteMArchToDB = "INSERT INTO DATACURR(id_bd, id_ptype,DCALL,DCOUNTER,DATECOUNTER,q1,q2,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,M1,M2,M3,M4,M5,M6,dt12,dt45,sp_TB1,sp_TB2,tce1,tce2,tair1,tair2,hc_code,hc,hcraw) values ("
            WriteMArchToDB = WriteMArchToDB + DeviceID.ToString() + ","
            WriteMArchToDB = WriteMArchToDB + "1,"
            WriteMArchToDB = WriteMArchToDB + "SYSDATE" + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + OracleDate(mArch.DateArch) + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.dQ1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.dQ2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.t6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p3, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.p4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G2, "##############0.000").Replace(",", ".") + ","
            If Not Single.IsNaN(mArch.G3) Then
                WriteMArchToDB = WriteMArchToDB + Format(mArch.G3 / 1000, "##############0.000").Replace(",", ".") + ","
            Else
                WriteMArchToDB = WriteMArchToDB + "NULL,"
            End If

            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G4, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G5, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.G6, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.dt12, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.dt45, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + mArch.SPtv1.ToString + ","
            WriteMArchToDB = WriteMArchToDB + mArch.SPtv2.ToString + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tx1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tx2, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair1, "##############0.000").Replace(",", ".") + ","
            WriteMArchToDB = WriteMArchToDB + NanFormat(mArch.tair2, "##############0.000").Replace(",", ".") + ","



            If DeCodeHCNumber(mArch.HC) = "0000000000000000" Then
                WriteMArchToDB = WriteMArchToDB + "'-','Нет НС',"
            Else
                WriteMArchToDB = WriteMArchToDB + "'" + DeCodeHCNumber(mArch.HC) + "','" + S180(DeCodeHCNumber(mArch.HC) + " " + DeCodeHCText(mArch.HC)) + "',"
            End If

            WriteMArchToDB = WriteMArchToDB + "'" + mArch.HC.ToString().Replace(",", ".") + "'"
            WriteMArchToDB = WriteMArchToDB + ")"
        Catch
        End Try
        'Return WriteMArchToDB
    End Function




    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
    End Sub
    Private Sub cleararchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue

        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHCtv1 = ""

        arc.HCtv2 = 0
        arc.MsgHCtv2 = ""

        arc.Tw1 = Single.NaN
        arc.Tw2 = Single.NaN

        arc.P1 = Single.NaN
        arc.T1 = Single.NaN
        arc.M2 = Single.NaN
        arc.V1 = Single.NaN

        arc.P2 = Single.NaN
        arc.T2 = Single.NaN
        arc.M3 = Single.NaN
        arc.V2 = Single.NaN

        arc.V3 = Single.NaN
        arc.M1 = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN

        arc.QG1 = Single.NaN
        arc.QG2 = Single.NaN

        arc.SP = 0
        arc.SPtv1 = 0
        arc.SPtv2 = 0

        arc.tx1 = 0
        arc.tx2 = 0
        arc.tair1 = 0
        arc.tair2 = 0

        arc.T3 = Single.NaN
        arc.T4 = Single.NaN
        arc.T5 = Single.NaN
        arc.T6 = Single.NaN
        arc.P3 = Single.NaN
        arc.P4 = Single.NaN
        arc.v4 = Single.NaN
        arc.v5 = Single.NaN
        arc.v6 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.archType = 0
        isArchToDBWrite = False
    End Sub
    Private Sub clearMarchive(ByRef marc As MArchive)
        marc.DateArch = DateTime.MinValue
        marc.HC = 0
        marc.MsgHC = ""

        marc.HCtv1 = 0
        marc.MsgHCtv1 = ""

        marc.HCtv2 = 0
        marc.MsgHCtv2 = ""

        marc.G1 = Single.NaN
        marc.G2 = Single.NaN
        marc.G3 = Single.NaN
        marc.G4 = Single.NaN
        marc.G5 = Single.NaN
        marc.G6 = Single.NaN

        marc.t1 = Single.NaN
        marc.t2 = Single.NaN
        marc.t3 = Single.NaN
        marc.t4 = Single.NaN
        marc.t5 = Single.NaN
        marc.t6 = Single.NaN

        marc.p1 = Single.NaN
        marc.p2 = Single.NaN
        marc.p3 = Single.NaN
        marc.p4 = Single.NaN

        marc.dt12 = Single.NaN
        marc.dt45 = Single.NaN

        marc.tx1 = Single.NaN
        marc.tx2 = Single.NaN

        marc.tair1 = Single.NaN
        marc.tair2 = Single.NaN

        marc.SP = 0
        marc.SPtv1 = 0
        marc.SPtv2 = 0


        marc.archType = 1
        isMArchToDBWrite = False
    End Sub

    Private Function ChanelToByte(ByVal ch As Byte) As Byte
        '1 – первый канал,
        '0 – второй канал,
        '2 – холодная вода,
        '4 – четвертый канал,
        '3 – пятый канал.

        Select Case ch
            Case 1
                Return 1
            Case 2
                Return 0
            Case 3
                Return 2
            Case 4
                Return 4
            Case 5
                Return 3
            Case Else
                Return ch
        End Select
    End Function

    Private Function ReadCommand(ByVal Code As Byte, ByVal chanel As Byte) As String
        Dim cmdBuf(1000) As Byte
        Dim sRes(10) As String
        Dim bDup(10) As Boolean
        Dim Cnt(10) As Integer
        Dim bsz(10) As Integer
        Dim buf(1000) As Byte
        Dim maxPass As Int16

        maxPass = 4
        EraseInputQueue()
        Debug.Print("Command:" & Code.ToString())
        'Dim result As String
        'result = "Error reading data"

        cmdBuf(0) = 4
        cmdBuf(1) = Code

        If chanel = 255 Then

            cmdBuf(2) = 0
            cmdBuf(3) = ChSum(cmdBuf, 3)

        Else
            cmdBuf(2) = ChanelToByte(chanel)
            cmdBuf(3) = ChSum(cmdBuf, 3)


        End If

        Dim j As Int16, pass As Int16
        Dim btr As Long
        Dim sz As Long = 0
        Dim bufStr As String
        bsz(pass) = 0
        bufStr = ""
        ReDim buf(1000)
        Dim maxIdx As Integer
        Dim check As Integer
        maxIdx = 0

        For pass = 0 To maxPass
            If Connecting Then
                If Not MyTransport.IsConnected Then
                    Return "Error reading data"
                End If
            Else
                If Not IsConnected() Then
                    Return "Error reading data"
                End If
            End If

            MyTransport.CleanPort()
            MyTransport.Write(cmdBuf, 0, 4)

            WaitForData()
            bufStr = ""
            ReDim buf(1000)


            btr = MyTransport.BytesToRead
            While btr > 0
                MyTransport.Read(buf, bsz(pass), btr)
                bsz(pass) += btr
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
            End While



            If bsz(pass) > 0 Then
                If buf(0) = 0 And buf(bsz(pass) - 1) = 0 Then
                    buf = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 1, bsz(pass) - 2)
                    For j = 0 To bsz(pass) - 3
                        bufStr = bufStr + Chr(buf(j))
                    Next
                    sRes(pass) = bufStr
                End If
            End If

            If pass > 0 Then

                For check = 0 To pass - 1

                    If sRes(pass) = sRes(check) And bsz(pass) > 0 Then
                        bDup(check) = True
                        Cnt(check) = 2
                        maxIdx = check
                        GoTo loadbuf
                    End If
                Next
            End If

        Next


        ' проверяем правильность получения данных
        For pass = 0 To maxPass
            If bsz(pass) > 0 Then
                If bDup(pass) = False Then
                    Cnt(pass) = 1
                    For j = pass + 1 To maxPass
                        If sRes(pass) = sRes(j) And bDup(j) = False Then
                            Cnt(pass) += 1
                            bDup(j) = True
                        End If
                    Next
                End If
            End If
        Next


        For pass = 0 To maxPass

            If Cnt(pass) >= Cnt(maxIdx) And bDup(pass) = False Then
                maxIdx = pass
            End If
        Next

        If Cnt(maxIdx) = 1 Then
            Return "Error unstable reads"
        End If
loadbuf:

        Debug.Print(sRes(maxIdx))
        Return sRes(maxIdx)
    End Function


    Private Function ReadCommand5(ByVal Code As Byte, ByVal b As Byte, ByVal chanel As Byte) As String
        Dim cmdBuf(1000) As Byte
        Dim sRes(10) As String
        Dim bDup(10) As Boolean
        Dim Cnt(10) As Integer
        Dim bsz(10) As Integer
        Dim buf(1000) As Byte
        Dim maxPass As Int16
        maxPass = 4

        'Dim result As String
        'result = "Error reading data"

        cmdBuf(0) = 5
        cmdBuf(1) = Code

        If chanel = 255 Then

            cmdBuf(2) = 0
            cmdBuf(3) = b
            cmdBuf(4) = ChSum(cmdBuf, 4)

        Else
            cmdBuf(2) = ChanelToByte(chanel)
            cmdBuf(3) = b
            cmdBuf(4) = ChSum(cmdBuf, 4)


        End If

        Dim j As Int16, pass As Int16
        Dim maxIdx As Integer
        maxIdx = 0
        Dim check As Integer
        For pass = 0 To maxPass
            If Not IsConnected() Then
                Return "Error reading data"
            End If
            MyTransport.CleanPort()
            MyTransport.Write(cmdBuf, 0, 5)
            WaitForData()


            Dim btr As Long
            Dim sz As Long = 0
            Dim bufStr As String
            bsz(pass) = 0
            bufStr = ""
            ReDim buf(1000)


            btr = MyTransport.BytesToRead
            While btr > 0
                MyTransport.Read(buf, bsz(pass), btr)
                bsz(pass) += btr
                RaiseIdle()
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
            End While




            If bsz(pass) > 0 Then
                If buf(0) = 0 And buf(bsz(pass) - 1) = 0 Then
                    buf = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(866), System.Text.Encoding.Default, buf, 1, bsz(pass) - 2)
                    For j = 0 To bsz(pass) - 3
                        bufStr = bufStr + Chr(buf(j))
                    Next
                    sRes(pass) = bufStr
                End If
            End If

            If pass > 1 Then

                For check = 1 To pass - 1

                    If sRes(pass) = sRes(check) And bsz(pass) > 0 Then
                        bDup(check) = True
                        Cnt(check) = 2
                        maxIdx = check
                        GoTo loadbuf
                    End If
                Next
            End If

        Next

        ' проверяем правильность получения данных
        For pass = 0 To maxPass
            If bDup(pass) = False Then
                Cnt(pass) = 1
                For j = pass + 1 To maxPass
                    If sRes(pass) = sRes(j) And bDup(j) = False Then
                        Cnt(pass) += 1
                        bDup(j) = True
                    End If
                Next
            End If
        Next


        For pass = 0 To maxPass
            If Cnt(pass) >= Cnt(maxIdx) And bDup(pass) = False Then
                maxIdx = pass
            End If
        Next

        If Cnt(maxIdx) = 1 Then
            Return "Error unstable reads"
        End If
loadbuf:

        Return sRes(maxIdx)
    End Function

    Public Function RashodInfo(ByVal chanel As Integer) As Integer
        Dim str As String
        Dim ri As Integer = 1
        Try

            Dim tCnt As Integer
            Dim DBUF As String = ""
            tCnt = 0
            str = "Error"
            While (tCnt < 4 And Left(str, 5) = "Error")
                tCnt += 1

                str = ReadCommand(&H9E, chanel)
                If Left(str, 5) <> "Error" Then
                    DBUF = str
                    Exit While
                End If
            End While

            If Integer.TryParse(DBUF, ri) Then
                Return ri
            End If
            Return 1

        Catch
        End Try
        Return 1
    End Function

    Public Function Plotnost(ByVal chanel As Integer) As Double
        Dim str As String
        Dim ri As Double = 1.0
        Try

            Dim tCnt As Integer
            Dim DBUF As String = ""
            tCnt = 0
            str = "Error"
            While (tCnt < 4 And Left(str, 5) = "Error")
                tCnt += 1

                str = ReadCommand(&H7B, chanel)
                If Left(str, 5) <> "Error" Then
                    DBUF = str
                    Exit While
                End If
            End While

            If Double.TryParse(DBUF, ri) Then
                Return ri
            End If
            Return 1.0

        Catch
        End Try
        Return 1.0
    End Function



    Private GoodDeviceDate As Date = Date.MinValue

    Public Function GetDeviceDate() As Date
        Dim str As String
        Dim DateArch As Date = Date.MinValue
        If GoodDeviceDate <> Date.MinValue Then
            Return GoodDeviceDate
        End If
        Try

            Dim tCnt As Integer
            Dim DBUF As String = ""
            tCnt = 0
            str = "Error"
            While (tCnt < 4 And Left(str, 5) = "Error")
                tCnt += 1
                '0x42	DATE	Системные	Дата	Системная Дата счетчика

                str = ReadCommand(&H42, 255)
                If Left(str, 5) <> "Error" Then
                    DBUF = str
                    Exit While
                End If

            End While


            tCnt = 0
            str = "Error"
            '0x43	DATE	Системные	Время	Системное Время счетчика

            While (tCnt < 4 And Left(str, 5) = "Error")
                tCnt += 1
                str = ReadCommand(&H43, 255)
                If Left(str, 5) <> "Error" Then
                    If DBUF <> "" Then
                        DateArch = Date.Parse(DBUF + " " + str)
                        GoodDeviceDate = DateArch
                        Exit While
                    Else
                        DateArch = Date.Parse(str)
                        Exit While
                    End If
                End If
            End While
            If DateArch <= DateAdd(DateInterval.Day, 1, Date.MinValue) Then
                DateArch = DateTime.Now
                GoodDeviceDate = DateArch

            End If

        Catch
        End Try
        Return DateArch
    End Function



    Public Overrides Function ReadMArch() As String

        '0x31 т1	SINGLE	Мгновенные	G1
        '0x31 т2	SINGLE	Мгновенные	G2
        '0x31 т4	SINGLE	Мгновенные	G4
        '0x31 т5	SINGLE	Мгновенные	G5
        '0x33	SINGLE	Мгновенные	Tхк1
        '0x33 т2	SINGLE	Мгновенные	Tхк2
        '0x34 4	SINGLE	Мгновенные	T4
        '0x34 5	SINGLE	Мгновенные	T5
        '0x34 т1	SINGLE	Мгновенные	T1
        '0x34 т2	SINGLE	Мгновенные	T2
        '0x35 т1-т2	SINGLE	Мгновенные	dT
        '0x61	SINGLE	Мгновенные	Gгвс
        '0xA4	SINGLE	Мгновенные	KQ
        '0xA5 т1	SINGLE	Мгновенные	dQ1
        '0xA5 т2	SINGLE	Мгновенные	dQ2
        '0xD1	SINGLE	Мгновенные	Tхв
        '0xD3 т1	SINGLE	Мгновенные	P1
        '0xD3 т1т2	SINGLE	Мгновенные	dP
        '0xD3 т2	SINGLE	Мгновенные	P2
        '0xD3 хв	SINGLE	Мгновенные	Pхв
        '0xD3 к4	SINGLE	Мгновенные	P4
        '0xD3 к5	SINGLE	Мгновенные	P5

        clearMarchive(mArch)
        Dim str As String
        Dim ok As Boolean = False
        str = ReadCommand(&H31, 1)

        If Left(str, 5) <> "Error" Then mArch.G1 = Val(str) : ok = True
        str = ReadCommand(&H31, 2)
        If Left(str, 5) <> "Error" Then mArch.G2 = Val(str) : ok = True
        str = ReadCommand(&H31, 3)
        If Left(str, 5) <> "Error" Then mArch.G3 = Val(str) : ok = True
        str = ReadCommand(&H31, 4)
        If Left(str, 5) <> "Error" Then mArch.G4 = Val(str) : ok = True
        str = ReadCommand(&H31, 5)
        If Left(str, 5) <> "Error" Then mArch.G5 = Val(str) : ok = True
        str = ReadCommand(&H34, 1)
        If Left(str, 5) <> "Error" Then mArch.t1 = Val(str) : ok = True
        str = ReadCommand(&H34, 2)
        If Left(str, 5) <> "Error" Then mArch.t2 = Val(str) : ok = True
        'str = ReadCommand(&H34, 3)
        'If Left(str, 5) <> "Error" Then mArch.t3 = Val(str) : ok = True
        str = ReadCommand(&H34, 4)
        If Left(str, 5) <> "Error" Then mArch.t4 = Val(str) : ok = True
        str = ReadCommand(&H34, 5)
        If Left(str, 5) <> "Error" Then mArch.t5 = Val(str) : ok = True
        str = ReadCommand(&H35, 255)
        If Left(str, 5) <> "Error" Then mArch.dt12 = Val(str) : ok = True
        str = ReadCommand(&H61, 255)
        If Left(str, 5) <> "Error" Then mArch.G3 = Val(str) : ok = True
        str = ReadCommand(&HA5, 1)
        If Left(str, 5) <> "Error" Then mArch.dQ1 = Val(str) : ok = True
        str = ReadCommand(&HA5, 2)
        If Left(str, 5) <> "Error" Then mArch.dQ2 = Val(str) : ok = True

        str = ReadCommand(&HD1, 2)
        If Left(str, 5) <> "Error" Then mArch.tx1 = Val(str) : ok = True

        str = ReadCommand(&HD3, 1)
        If Left(str, 5) <> "Error" Then mArch.p1 = Val(str) : ok = True

        str = ReadCommand(&HD3, 2)
        If Left(str, 5) <> "Error" Then mArch.p2 = Val(str) : ok = True

        str = ReadCommand(&HD3, 3)
        If Left(str, 5) <> "Error" Then mArch.p3 = Val(str) : ok = True

        str = ReadCommand(&HD3, 4)
        If Left(str, 5) <> "Error" Then mArch.p4 = Val(str) : ok = True

        str = ReadCommand(&HD3, 5)
        If Left(str, 5) <> "Error" Then mArch.p5 = Val(str) : ok = True


        mArch.DateArch = DateTime.Now
        Try
            '0x42	DATE	Системные	Дата	Системная Дата счетчика
            Dim DBUF As String = ""
            str = ReadCommand(&H42, 255)
            If Left(str, 5) <> "Error" Then DBUF = str
            '0x43	DATE	Системные	Время	Системное Время счетчика
            str = ReadCommand(&H43, 255)
            If Left(str, 5) <> "Error" Then
                If DBUF <> "" Then
                    mArch.DateArch = Date.Parse(DBUF + " " + str) : ok = True
                Else
                    mArch.DateArch = Date.Parse(str) : ok = True
                End If


            End If
            If mArch.DateArch.Year < 1950 Then
                mArch.DateArch = DateTime.Now
            End If
        Catch
        End Try

        Try
            '0x3b	HC	Нештатная Ситуация	Статус	Статус (Код Состояния)
            str = ReadCommand(&H3B, 255)
            If Left(str, 5) <> "Error" Then
                str = StrReverse(str)
                mArch.HC = Convert.ToInt32(str, 2)
                ok = True
            End If

        Catch
        End Try
        If ok Then

            isMArchToDBWrite = True
            Return "Мгновенный архив прочитан"
        Else
            Return "Ошибка чтения мгновенного архива"
        End If

    End Function
    Dim m_isTArchToDBWrite As Boolean = False
    Public Overrides Property isTArchToDBWrite() As Boolean
        Get
            Return m_isTArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isTArchToDBWrite = value
        End Set
    End Property

    Public Function ReadFlashSync(ByVal fistpage As Integer, ByVal ReadPageCount As Integer) As String
        Return ""
    End Function

    Public Function ReadRAMSync(ByVal fistbyte As Integer, ByVal byteCount As Integer) As String
        Return ""
    End Function

    Private Sub clearTarchive(ByRef marc As TArchive)
        marc.DateArch = DateTime.MinValue


        marc.V1 = Single.NaN
        marc.V2 = Single.NaN
        marc.V3 = Single.NaN
        marc.V4 = Single.NaN
        marc.V5 = Single.NaN
        marc.V6 = Single.NaN
        marc.M1 = Single.NaN
        marc.M2 = Single.NaN
        marc.M3 = Single.NaN
        marc.M4 = Single.NaN
        marc.M5 = Single.NaN
        marc.M6 = Single.NaN
        marc.Q1 = Single.NaN
        marc.Q2 = Single.NaN
        marc.TW1 = Single.NaN
        marc.TW2 = Single.NaN

        marc.archType = 2
        isTArchToDBWrite = False
    End Sub

    Public Overrides Function ReadTArch() As String
        Dim bArr(0 To 8) As Byte
        Dim temptv As Short
        temptv = tv
        clearTarchive(tArch)
        EraseInputQueue()

        '0x30 т1	SINGLE	Итоговые	Q1	Значение Объемного расхода канал 1
        '0x30 т2	SINGLE	Итоговые	Q2	Значение Объемного расхода канал 2
        '0x30 т4	SINGLE	Итоговые	Q4	Объем воды по каналу 4
        '0x30 т5	SINGLE	Итоговые	Q5	Объем воды по каналу 5
        '0x36 т1	SINGLE	Итоговые	W1	Значение теплоты канал 1
        '0x36 т2	SINGLE	Итоговые	W2	Значение теплоты канал 2
        '0x39	SINGLE	Итоговые	tработы	Время работы счетчика
        '0x3A	SINGLE	Итоговые	tаварии	Время аварии счетчика
        '0x63	SINGLE	Итоговые	dW	Разность теплоты (расход теплоты)

        Dim str As String
        Dim ok As Boolean = False

        str = ReadCommand(&H30, 1)
        If Left(str, 5) <> "Error" Then tArch.V1 = Val(str) : ok = True
        str = ReadCommand(&H30, 2)
        If Left(str, 5) <> "Error" Then tArch.V2 = Val(str) : ok = True
        str = ReadCommand(&H30, 3)
        If Left(str, 5) <> "Error" Then tArch.V3 = Val(str) : ok = True
        str = ReadCommand(&H30, 4)
        If Left(str, 5) <> "Error" Then tArch.V4 = Val(str) : ok = True
        str = ReadCommand(&H30, 5)
        If Left(str, 5) <> "Error" Then tArch.V5 = Val(str) : ok = True

        str = ReadCommand(&H36, 1)
        If Left(str, 5) <> "Error" Then tArch.Q1 = Val(str) : ok = True
        str = ReadCommand(&H36, 2)
        If Left(str, 5) <> "Error" Then tArch.Q2 = Val(str) : ok = True

        str = ReadCommand(&H39, 255)
        If Left(str, 5) <> "Error" Then tArch.TW1 = Val(str) : ok = True
        str = ReadCommand(&H3A, 255)
        If Left(str, 5) <> "Error" Then tArch.TW2 = Val(str) : ok = True
        str = ReadCommand(&H63, 255)
        If Left(str, 5) <> "Error" Then tArch.Q4 = Val(str) : ok = True

        tArch.DateArch = DateTime.Now
        Try
            '0x42	DATE	Системные	Дата	Системная Дата счетчика
            Dim DBUF As String = ""
            str = ReadCommand(&H42, 255)
            If Left(str, 5) <> "Error" Then DBUF = str
            '0x43	DATE	Системные	Время	Системное Время счетчика
            str = ReadCommand(&H43, 255)
            If Left(str, 5) <> "Error" Then
                If DBUF <> "" Then
                    tArch.DateArch = Date.Parse(DBUF + " " + str) : ok = True
                Else
                    tArch.DateArch = Date.Parse(str) : ok = True
                End If


            End If
            If tArch.DateArch.Year < 1950 Then
                tArch.DateArch = DateTime.Now
            End If
        Catch
        End Try

        Try
            '0x3b	HC	Нештатная Ситуация	Статус	Статус (Код Состояния)
            str = ReadCommand(&H3B, 255)
            If Left(str, 5) <> "Error" Then tArch.HC = Convert.ToInt32(str, 2) : ok = True
        Catch
        End Try
        If ok Then
            isTArchToDBWrite = True


            Return "Итоговый архив прочитан"
        Else
            Return "Ошибка чтения итогового архива"
        End If

    End Function

    Public Overrides Function WriteTArchToDB() As String
        WriteTArchToDB = "INSERT INTO DATACURR(id_bd,id_ptype,DCALL,DCOUNTER,DATECOUNTER,Q1,Q2,Q3,Q1H,M1,M2,M3,M4,M5,M6,v1,v2,v3,v4,v5,v6,TSUM1,TSUM2,worktime,ERRTIME) values ("
        WriteTArchToDB = WriteTArchToDB + DeviceID.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + tArch.archType.ToString() + ","
        WriteTArchToDB = WriteTArchToDB + "SYSDATE" + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + OracleDate(tArch.DateArch) + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.Q4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V6, "##############0.000").Replace(",", ".") + ","

        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V3, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V4, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V5, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.V6, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.TW1, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + NanFormat(tArch.TW2, "##############0.000").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.TW1, "##############0").Replace(",", ".") + ","
        WriteTArchToDB = WriteTArchToDB + Format(tArch.TW2, "##############0").Replace(",", ".")
        WriteTArchToDB = WriteTArchToDB + ")"
    End Function

    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function


    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function
    Private Function S180(ByVal s As String) As String

        Dim outs As String
        outs = s
        If outs.Length <= 180 Then
            Return outs
        End If
        outs = outs.Substring(0, 180)
        Return outs
    End Function




    Private Function TableForArch(ByVal ArchType As Short) As String
        Dim tName As String = ""
        If ArchType = 1 Then
            tName = "TPLC_M"
        End If

        If ArchType = 3 Then
            tName = "TPLC_H"
        End If
        If ArchType = 4 Then
            tName = "TPLC_D"
        End If
        If ArchType = 2 Then
            tName = "TPLC_T"
        End If
        Return tName
    End Function

    Public Overrides Function ReadSystemParameters() As DataTable
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")

        Dim dr As DataRow

        '0x3E 0	SINGLE	Системные	DeltaT	Значение DeltaT
        dr = dt.NewRow
        dr("Название") = "Значение DeltaT"
        dr("Значение") = ReadCommand(&H3E, 0)
        dt.Rows.Add(dr)


        '0x3E 01	SINGLE	Системные	DeltaT2	Значение DeltaT2
        dr = dt.NewRow
        dr("Название") = "Значение DeltaT2"
        dr("Значение") = ReadCommand(&H3E, 1)
        dt.Rows.Add(dr)




        '0x40	LONG	Системные	Adr RS485	Адрес по интерфейсу RS485
        dr = dt.NewRow
        dr("Название") = "Адрес по интерфейсу RS485"
        dr("Значение") = ReadCommand(&H40, 255)
        dt.Rows.Add(dr)

        '0x42	DATE	Системные	Дата	Системная Дата счетчика
        dr = dt.NewRow
        dr("Название") = "Системная Дата счетчика"
        dr("Значение") = ReadCommand(&H42, 255)
        dt.Rows.Add(dr)


        '0x43	DATE	Системные	Время	Системное Время счетчика
        dr = dt.NewRow
        dr("Название") = "Системное Время счетчика"
        dr("Значение") = ReadCommand(&H43, 255)
        dt.Rows.Add(dr)


        '0x46	STRING	Системные	Версия	Информация о Версии Счетчика
        dr = dt.NewRow
        dr("Название") = "Информация о Версии Счетчика"
        dr("Значение") = ReadCommand(&H46, 255)
        dt.Rows.Add(dr)


        '0x4D т1	SINGLE	Системные	K const т1	Значение константы K канал 1
        dr = dt.NewRow
        dr("Название") = "Значение константы K канал 1"
        dr("Значение") = ReadCommand(&H4D, 1)
        dt.Rows.Add(dr)

        '0x4D т2	SINGLE	Системные	K const т2	Значение константы K канал 2
        dr = dt.NewRow
        dr("Название") = "Значение константы K канал 2"
        dr("Значение") = ReadCommand(&H4D, 2)
        dt.Rows.Add(dr)

        '0x4E т1	SINGLE	Системные	P const т1	Значение константы P канал 1
        dr = dt.NewRow
        dr("Название") = "Значение константы P канал 1"
        dr("Значение") = ReadCommand(&H4E, 1)
        dt.Rows.Add(dr)

        '0x4E т2	SINGLE	Системные	P const т2	Значение константы P канал 2
        dr = dt.NewRow
        dr("Название") = "Значение константы P канал 2"
        dr("Значение") = ReadCommand(&H4E, 2)
        dt.Rows.Add(dr)


        '0x4F	STRING	Системные	Информ1	Информация о продукте 1
        dr = dt.NewRow
        dr("Название") = "Информация о продукте 1"
        dr("Значение") = ReadCommand(&H4F, 255)
        dt.Rows.Add(dr)


        '0x50	STRING	Системные	Nиздел.	Идентификационный номер счетчика
        dr = dt.NewRow
        dr("Название") = "Идентификационный номер счетчика"
        dr("Значение") = ReadCommand(&H50, 255)
        dt.Rows.Add(dr)

        '0x52 1	SINGLE	Системные	0 реф 1	Нулевая референция 1
        dr = dt.NewRow
        dr("Название") = "Нулевая референция 1"
        dr("Значение") = ReadCommand(&H52, 1)
        dt.Rows.Add(dr)


        '0x52 2	SINGLE	Системные	0 реф 2	Нулевая референция 2
        dr = dt.NewRow
        dr("Название") = "Нулевая референция 2"
        dr("Значение") = ReadCommand(&H52, 2)
        dt.Rows.Add(dr)

        '0x53 1	SINGLE	Системные	Не 0 реф 1	Ненулевая референция 1
        dr = dt.NewRow
        dr("Название") = "Ненулевая референция 1"
        dr("Значение") = ReadCommand(&H53, 1)
        dt.Rows.Add(dr)

        '0x53 2	SINGLE	Системные	Не 0 реф 2	Ненулевая референция 2
        dr = dt.NewRow
        dr("Название") = "Ненулевая референция 2"
        dr("Значение") = ReadCommand(&H53, 2)
        dt.Rows.Add(dr)

        '0x81 т1	SINGLE	Системные	ТипДатчика т1	Тип Датчика канала 1
        dr = dt.NewRow
        dr("Название") = "Тип Датчика канала 1"
        dr("Значение") = ReadCommand(&H81, 1)
        dt.Rows.Add(dr)

        '0x81 т2	SINGLE	Системные	ТипДатчика т2	Тип Датчика канала 2
        dr = dt.NewRow
        dr("Название") = "Тип Датчика канала 2"
        dr("Значение") = ReadCommand(&H81, 2)
        dt.Rows.Add(dr)

        '0x83 т1	SINGLE	Системные	KP const т1	Константа преобразования импульс/литр канал 1
        dr = dt.NewRow
        dr("Название") = "Константа преобразования импульс/литр канал 1"
        dr("Значение") = ReadCommand(&H83, 1)
        dt.Rows.Add(dr)

        '0x83 т2	SINGLE	Системные	KP const т2	Константа преобразования импульс/литр канал 2
        dr = dt.NewRow
        dr("Название") = "Константа преобразования импульс/литр канал 2"
        dr("Значение") = ReadCommand(&H83, 2)
        dt.Rows.Add(dr)


        '0x84 т1	SINGLE	Системные	ЧастВых G1	Значение выходной частоты Расхода канал 1
        dr = dt.NewRow
        dr("Название") = "Значение выходной частоты Расхода канал 1"
        dr("Значение") = ReadCommand(&H84, 1)
        dt.Rows.Add(dr)
        '0x84 т2	SINGLE	Системные	ЧастВых G2	Значение выходной частоты Расхода канал 2
        dr = dt.NewRow
        dr("Название") = "Значение выходной частоты Расхода канал 2"
        dr("Значение") = ReadCommand(&H84, 2)
        dt.Rows.Add(dr)


        '0x85 т1	SINGLE	Системные	ЧастВход т1	Входная частота канал 1
        dr = dt.NewRow
        dr("Название") = "Входная частота канал 1"
        dr("Значение") = ReadCommand(&H85, 1)
        dt.Rows.Add(dr)

        '0x85 т2	SINGLE	Системные	ЧастВход т2	Входная частота канал 2
        dr = dt.NewRow
        dr("Название") = "Входная частота канал 2"
        dr("Значение") = ReadCommand(&H85, 2)
        dt.Rows.Add(dr)


        '0x8D	SINGLE	Системные	tперекалибровки	Значение Времени перекалибровки
        dr = dt.NewRow
        dr("Название") = "Значение Времени перекалибровки"
        dr("Значение") = ReadCommand(&H8D, 255)
        dt.Rows.Add(dr)

        '0x9e к4	STRING	Системные	Ед.Изм.G4 V4	Единицы измерения объема и расхода канала 4
        dr = dt.NewRow
        dr("Название") = "Единицы измерения объема и расхода канала 4"
        dr("Значение") = ReadCommand(&H9E, 4)
        dt.Rows.Add(dr)

        '0x9e к5	STRING	Системные	Ед.Изм.G5 V5	Единицы измерения объема и расхода канала 5
        dr = dt.NewRow
        dr("Название") = "Единицы измерения объема и расхода канала 5"
        dr("Значение") = ReadCommand(&H9E, 5)
        dt.Rows.Add(dr)

        '0x9E т1	STRING	Системные	Ед.Изм.G1 V1	Единицы измерения Объемов и Расходов канала 1
        dr = dt.NewRow
        dr("Название") = "Единицы измерения объема и расхода канала 1"
        dr("Значение") = ReadCommand(&H9E, 1)
        dt.Rows.Add(dr)

        '0x9E т2	STRING	Системные	Ед.Изм.G2 V2	Единицы измерения Объемов и Расходов канала 2
        dr = dt.NewRow
        dr("Название") = "Единицы измерения объема и расхода канала 2"
        dr("Значение") = ReadCommand(&H9E, 2)
        dt.Rows.Add(dr)


        '0xA2	STRING	Системные	Скорость RS485	Значение скорости коммуникации по интерфейсу RS485
        dr = dt.NewRow
        dr("Название") = "Значение скорости коммуникации по интерфейсу RS485"
        dr("Значение") = ReadCommand(&HA2, 2)
        dt.Rows.Add(dr)

        '0xA4	SINGLE	Системные	KQ не отечает счетчик	Значение KQ


        '0xA9 т1	STRING	Системные	Ед.Изм.W1	Единицы измерения энергии канала 1
        dr = dt.NewRow
        dr("Название") = "Единицы измерения энергии канала 1"
        dr("Значение") = ReadCommand(&HA9, 1)
        dt.Rows.Add(dr)




        '0xA9 т2	STRING	Системные	Ед.Изм.W2	Единицы измерения энергии канала 2
        dr = dt.NewRow
        dr("Название") = "Единицы измерения энергии канала 2"
        dr("Значение") = ReadCommand(&HA9, 2)
        dt.Rows.Add(dr)

        '0xAA	STRING	Системные	Датч.исп.	Использование Датчиков
        dr = dt.NewRow
        dr("Название") = "Использование Датчиков"
        dr("Значение") = ReadCommand(&HAA, 255)
        dt.Rows.Add(dr)

        '0xAE 4	SINGLE	Системные	№ T4датч	Номер термодатчика 4
        dr = dt.NewRow
        dr("Название") = "Номер термодатчика 4"
        dr("Значение") = ReadCommand(&HAE, 4)
        dt.Rows.Add(dr)


        '0xAE 5	SINGLE	Системные	№ T5датч	Номер термодатчика 5
        dr = dt.NewRow
        dr("Название") = "Номер термодатчика 5"
        dr("Значение") = ReadCommand(&HAE, 5)
        dt.Rows.Add(dr)

        '0xC8	SINGLE	Системные	Iвкат	Ток возбуждения катушки
        dr = dt.NewRow
        dr("Название") = "Ток возбуждения катушки"
        dr("Значение") = ReadCommand(&HC8, 255)
        dt.Rows.Add(dr)

        '0xC9 1	SINGLE	Системные	Kкор1	Коэффициент коррекции 1
        dr = dt.NewRow
        dr("Название") = "Коэффициент коррекции 1"
        dr("Значение") = ReadCommand(&HC9, 1)
        dt.Rows.Add(dr)

        '0xC9 2	SINGLE	Системные	Kкор2	Коэффициент коррекции 2
        dr = dt.NewRow
        dr("Название") = "Коэффициент коррекции 2"
        dr("Значение") = ReadCommand(&HC9, 2)
        dt.Rows.Add(dr)

        '0xCA 1	SINGLE	Системные	СмВхд1	Входное смещение 1
        dr = dt.NewRow
        dr("Название") = "Входное смещение 1"
        dr("Значение") = ReadCommand(&HCA, 1)
        dt.Rows.Add(dr)
        '0xCA 2	SINGLE	Системные	СмВхд2	Входное смещение 2
        dr = dt.NewRow
        dr("Название") = "Входное смещение 2"
        dr("Значение") = ReadCommand(&HCA, 2)
        dt.Rows.Add(dr)

        '0xCB	SINGLE	Системные	KdG	Коэффициент разницы расходов
        dr = dt.NewRow
        dr("Название") = "Коэффициент разницы расходов"
        dr("Значение") = ReadCommand(&HCB, 255)
        dt.Rows.Add(dr)

        '0xCD	SINGLE	Системные	AdrRS485_EEPROM	Адрес RS485 из EEPROM
        dr = dt.NewRow
        dr("Название") = "Адрес RS485 из EEPROM"
        dr("Значение") = ReadCommand(&HCD, 255)
        dt.Rows.Add(dr)

        '0xD5	STRING	Системные	ЗадачаСезона	Сезонная задача счетчика Зима/Лето
        dr = dt.NewRow
        dr("Название") = "Сезонная задача счетчика Зима/Лето"
        dr("Значение") = ReadCommand(&HD5, 255)
        dt.Rows.Add(dr)


        '0xD7	STRING	Системные	Тип ТСП	Тип Термопары
        dr = dt.NewRow
        dr("Название") = "Тип ТСП	Тип Термопары"
        dr("Значение") = ReadCommand(&HD7, 255)
        dt.Rows.Add(dr)



        dr = dt.NewRow
        dr("Название") = "Плотность 1: "
        dr("Значение") = PT(1).ToString
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Плотность 2: "
        dr("Значение") = PT(2).ToString
        dt.Rows.Add(dr)
        dr = dt.NewRow

        dr("Название") = "Плотность 3: "
        dr("Значение") = PT(3).ToString
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Плотность 4: "
        dr("Значение") = PT(4).ToString
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Плотность 5: "
        dr("Значение") = PT(5).ToString
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Масса \ Объем  1: "
        dr("Значение") = IIf(RI(1) = 2, "МАССА", "ОБЪЕМ")
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Масса \ Объем 2: "
        dr("Значение") = IIf(RI(2) = 2, "МАССА", "ОБЪЕМ")
        dt.Rows.Add(dr)
        dr = dt.NewRow

        dr("Название") = "Масса \ Объем 3: "
        dr("Значение") = IIf(RI(3) = 2, "МАССА", "ОБЪЕМ")
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Название") = "Масса \ Объем 4: "
        dr("Значение") = IIf(RI(4) = 2, "МАССА", "ОБЪЕМ")
        dt.Rows.Add(dr)
        dr = dt.NewRow
        dr("Название") = "Масса \ Объем 5: "
        dr("Значение") = IIf(RI(5) = 2, "МАССА", "ОБЪЕМ")
        dt.Rows.Add(dr)

        Return dt
    End Function


    Private Function GetArchiv(ByVal ArchDate As Date, ByVal ArcDayly As Boolean) As Object
        Dim DayInYear As Integer, N As Integer
        Dim AbsDay As Long, Adr As Long
        Dim i As Long
        DayInYear = 0

        For i = 1 To ArchDate.Month - 1
            Select Case i
                Case 1, 3, 5, 7, 8, 10, 12
                    DayInYear = DayInYear + 31
                Case 4, 6, 9, 11
                    DayInYear = DayInYear + 30
                Case 2
                    N = ArchDate.Year
                    DayInYear = DayInYear + 28 + IIf((N Mod 4 = 0 And N Mod 100 <> 0) Or (N Mod 400 = 0), 1, 0)
            End Select
        Next
        DayInYear = DayInYear + ArchDate.Day
        AbsDay = (Year(ArchDate) - 1) * 365 + ((Year(ArchDate) - 1) \ 4) + DayInYear


        If ArcDayly = True Then
            Adr = (AbsDay Mod 60) * 40 + 62048
        ElseIf ArcDayly = False Then
            Adr = ((AbsDay * 24 + Hour(ArchDate)) Mod 1428) * 42 + 2048
        End If

        Dim buf(1000) As Byte
        For i = 0 To 999
            buf(i) = 0
        Next
        Dim dar As DArch
        Dim har As HArch
        If ArcDayly = True Then
            dar.OK = ReadDayArchivTSR(Adr, buf)
            FillDArch(buf, dar)
            If isTCP Then
                ReDim buf(1000)
                For i = 0 To 999
                    buf(i) = 0
                Next
                dar.OK = dar.OK And ReadDayArchivTSR2(Adr, buf)
                FillDArchTCP(buf, dar)
            Else
                ReDim buf(1000)
                For i = 0 To 999
                    buf(i) = 0
                Next
                If ReadDayArchivTSR2(Adr, buf) Then
                    FillDArchMT(buf, dar)
                End If
            End If

                Return dar
        ElseIf ArcDayly = False Then
                har.OK = ReadHourArchivTSR(Adr, buf)
                FillHArch(buf, har)
                If isTCP Then
                    ReDim buf(1000)
                    For i = 0 To 999
                        buf(i) = 0
                    Next
                    har.OK = har.OK And ReadHourArchivTSR2(Adr, buf)
                    FillHArchTCP(buf, har)
                Else
                    ReDim buf(1000)
                    For i = 0 To 999
                        buf(i) = 0
                    Next
                If ReadHourArchivTSR2(Adr, buf) Then
                    FillHArchMT(buf, har)
                End If
            End If

            Return har
        End If
        Return buf

    End Function

    Private Function ReadDayArchivTSR(ByVal Adr As Long, ByRef buf() As Byte) As Boolean

        Dim btr As Long
        Dim sz As Long = 0
        Dim i As Long
        Dim cmdbuf(100) As Byte



        cmdbuf(0) = &H2F
        cmdbuf(1) = &H8
        cmdbuf(2) = Adr \ c_lng256
        cmdbuf(3) = Adr Mod c_lng256
        For i = 1 To 40
            cmdbuf(3 + i) = 32
        Next
        cmdbuf(44) = 0
        cmdbuf(45) = 0

        cmdbuf(46) = ChSum(cmdbuf, 45)


        Dim j As Long
        Dim pass(10) As DataPass
        Dim maxpass As Int16
        Dim check As Int16
        Dim maxIdx As Integer
        Dim ok As Boolean
        ok = False
        maxIdx = 0

        maxpass = 4
        For j = 0 To maxpass
            pass(j) = New DataPass
            sz = 0
            btr = 0
            If Not IsConnected() Then
                Return False
            End If
            MyTransport.CleanPort()
            MyTransport.Write(cmdbuf, 0, 47)
            WaitForData()

            btr = MyTransport.BytesToRead
            While btr > 0
                MyTransport.Read(pass(j).passdata, sz, btr)
                sz += btr
                RaiseIdle()
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
            End While

            pass(j).Size = sz

            If j > 0 Then
                For check = 0 To j - 1
                    If pass(j).GetHash() = pass(check).GetHash() Then
                        ok = True
                        maxIdx = check
                        pass(check).bDup = True
                        GoTo loadbuf
                    End If
                Next
            End If
        Next


        ' проверяем правильность получения данных


        For check = 0 To maxpass
            If pass(check).Size > 0 Then

                If pass(check).bDup = False Then
                    pass(check).Cnt = 1
                    For j = check + 1 To maxpass
                        If pass(check).GetHash() = pass(j).GetHash And pass(j).bDup = False Then
                            pass(check).Cnt += 1
                            pass(j).bDup = True
                        End If
                    Next
                End If
            End If
        Next



        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                ok = True
                If pass(check).Cnt >= pass(maxIdx).Cnt And pass(check).bDup = False Then
                    maxIdx = check
                End If
            End If
        Next

        If pass(maxIdx).Cnt = 1 Then
            Return False
        End If
loadbuf:

        For i = 0 To 100
            buf(i) = pass(maxIdx).passdata(i)
        Next
        Return ok
    End Function

    Private Function ReadDayArchivTSR2(ByVal Adr As Long, ByRef buf() As Byte) As Boolean

        Dim btr As Long
        Dim sz As Long = 0
        Dim i As Long


        Dim cmdbuf(100) As Byte



        cmdbuf(0) = &H2F
        cmdbuf(1) = &H18
        cmdbuf(2) = Adr \ c_lng256
        cmdbuf(3) = Adr Mod c_lng256
        For i = 1 To 40
            cmdbuf(3 + i) = 32
        Next
        cmdbuf(44) = 0
        cmdbuf(45) = 0

        cmdbuf(46) = ChSum(cmdbuf, 45)


        Dim j As Long
        Dim pass(10) As DataPass
        Dim maxpass As Int16
        Dim check As Int16
        Dim maxIdx As Integer
        maxIdx = 0
        Dim ok As Boolean
        ok = False

        maxpass = 4
        For j = 0 To maxpass
            pass(j) = New DataPass
            sz = 0
            btr = 0
            If Not IsConnected() Then
                Return False
            End If
            MyTransport.CleanPort()
            MyTransport.Write(cmdbuf, 0, 47)

            WaitForData()



            btr = MyTransport.BytesToRead
            While btr > 0
                MyTransport.Read(pass(j).passdata, sz, btr)
                sz += btr
                RaiseIdle()
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
            End While



            pass(j).Size = sz


            If j > 0 Then
                For check = 0 To j - 1
                    If pass(j).GetHash() = pass(check).GetHash() Then
                        ok = True
                        maxIdx = check
                        pass(check).bDup = True
                        GoTo loadbuf
                    End If
                Next
            End If
        Next

        ' проверяем правильность получения данных

        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                If pass(check).bDup = False Then
                    pass(check).Cnt = 1
                    For j = check + 1 To maxpass
                        If pass(check).GetHash() = pass(j).GetHash And pass(j).bDup = False Then
                            pass(check).Cnt += 1
                            pass(j).bDup = True
                        End If
                    Next
                End If
            End If
        Next


        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                ok = True
                If pass(check).Cnt >= pass(maxIdx).Cnt And pass(check).bDup = False Then
                    maxIdx = check
                End If
            End If
        Next

        If pass(maxIdx).Cnt = 1 Then
            Return False
        End If
loadbuf:
        For i = 0 To 100
            buf(i) = pass(maxIdx).passdata(i)
        Next
        Return ok
    End Function
    Private Function ReadHourArchivTSR(ByVal Adr As Long, ByRef buf() As Byte) As Boolean
        Dim btr As Long
        Dim sz As Long = 0
        Dim i As Long
        Dim cmdbuf(100) As Byte

        cmdbuf(0) = &H31
        cmdbuf(1) = &H8
        cmdbuf(2) = Adr \ c_lng256
        cmdbuf(3) = Adr Mod c_lng256
        For i = 1 To 42
            cmdbuf(3 + i) = 32
        Next
        cmdbuf(46) = 0
        cmdbuf(47) = 0

        cmdbuf(48) = ChSum(cmdbuf, 47)

        Dim j As Long
        Dim pass(10) As DataPass
        Dim maxpass As Int16
        Dim check As Int16
        Dim maxIdx As Integer
        maxIdx = 0
        Dim ok As Boolean
        ok = False

        maxpass = 4
        For j = 0 To maxpass
            pass(j) = New DataPass
            sz = 0
            btr = 0
            If Not IsConnected() Then
                Return False
            End If
            MyTransport.CleanPort()
            MyTransport.Write(cmdbuf, 0, 49)
            WaitForData()



            btr = MyTransport.BytesToRead
            While btr > 0
                MyTransport.Read(pass(j).passdata, sz, btr)
                sz += btr
                RaiseIdle()
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
            End While

            pass(j).Size = sz

            If j > 0 Then
                For check = 0 To j - 1
                    If pass(j).GetHash() = pass(check).GetHash() Then
                        ok = True
                        maxIdx = check
                        pass(check).bDup = True
                        GoTo loadbuf
                    End If
                Next
            End If
        Next


        ' проверяем правильность получения данных

        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                If pass(check).bDup = False Then
                    pass(check).Cnt = 1
                    For j = check + 1 To maxpass
                        If pass(check).GetHash() = pass(j).GetHash And pass(j).bDup = False Then
                            pass(check).Cnt += 1
                            pass(j).bDup = True
                        End If
                    Next
                End If
            End If
        Next


        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                ok = True
                If pass(check).Cnt >= pass(maxIdx).Cnt And pass(check).bDup = False Then
                    maxIdx = check
                End If
            End If
        Next

        If pass(maxIdx).Cnt = 1 Then
            Return False
        End If
loadbuf:
        For i = 0 To 100
            buf(i) = pass(maxIdx).passdata(i)
        Next
        Return ok

    End Function

    Private Function ReadHourArchivTSR2(ByVal Adr As Long, ByRef buf() As Byte) As Boolean
        Dim btr As Long
        Dim sz As Long = 0
        Dim i As Long

        Dim cmdbuf(100) As Byte

        cmdbuf(0) = &H31
        cmdbuf(1) = &H18
        cmdbuf(2) = Adr \ c_lng256
        cmdbuf(3) = Adr Mod c_lng256
        For i = 1 To 42
            cmdbuf(3 + i) = 32
        Next
        cmdbuf(46) = 0
        cmdbuf(47) = 0

        cmdbuf(48) = ChSum(cmdbuf, 47)

        Dim j As Long
        Dim pass(10) As DataPass
        Dim maxpass As Int16
        Dim check As Int16
        Dim maxIdx As Integer
        maxIdx = 0
        Dim ok As Boolean
        ok = False

        maxpass = 4
        For j = 0 To maxpass
            pass(j) = New DataPass
            sz = 0
            btr = 0
            If Not IsConnected() Then
                Return False
            End If
            MyTransport.CleanPort()
            MyTransport.Write(cmdbuf, 0, 49)

            WaitForData()

            btr = MyTransport.BytesToRead
            While btr > 0
                MyTransport.Read(pass(j).passdata, sz, btr)
                sz += btr
                RaiseIdle()
                System.Threading.Thread.Sleep(CalcInterval(10))
                btr = MyTransport.BytesToRead
            End While

            pass(j).Size = sz

            If j > 0 Then
                For check = 0 To j - 1
                    If pass(j).GetHash() = pass(check).GetHash() Then
                        ok = True
                        maxIdx = check
                        pass(check).bDup = True
                        GoTo loadbuf
                    End If
                Next
            End If
        Next


        ' проверяем правильность получения данных

        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                If pass(check).bDup = False Then
                    pass(check).Cnt = 1
                    For j = check + 1 To maxpass
                        If pass(check).GetHash() = pass(j).GetHash And pass(j).bDup = False Then
                            pass(check).Cnt += 1
                            pass(j).bDup = True
                        End If
                    Next
                End If
            End If
        Next


        For check = 0 To maxpass
            If pass(check).Size > 0 Then
                ok = True
                If pass(check).Cnt >= pass(maxIdx).Cnt And pass(check).bDup = False Then
                    maxIdx = check
                End If
            End If
        Next

        If pass(maxIdx).Cnt = 1 Then
            Return False
        End If
loadbuf:
        For i = 0 To 100
            buf(i) = pass(maxIdx).passdata(i)
        Next
        Return ok

    End Function

    Public Overrides Property isMArchToDBWrite() As Boolean
        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property





End Class
