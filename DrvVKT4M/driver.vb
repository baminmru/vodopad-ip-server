Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Class Driver
    Inherits STKTVMain.TVDriver
    Private mIsConnected As Boolean

    Dim POVer As String = "0.0"
    Dim isVKT4M As Boolean = False
    Dim WillCountToRead As Short = 0
    '  Dim IsBytesToRead As Boolean = False
    'Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False
    Dim IsTArchToRead As Boolean = False
    Dim buffer(32767) As Byte
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
    Public Overrides Property isMArchToDBWrite() As Boolean

        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property
    Dim m_isTArchToDBWrite As Boolean = False

    Public Overrides Property isTArchToDBWrite() As Boolean

        Get
            Return m_isTArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isTArchToDBWrite = value
        End Set
    End Property

    Public Overrides Function CounterName() As String
        Return "VKT4M"
    End Function

    Public Function GetAndProcessData() As String
        Dim buf(100) As Byte
        Dim i As Int16
        For i = 0 To 100
            buf(i) = 0
        Next

        Dim ret As Long = 1
        bufferindex = 0

        While (bufferindex < WillCountToRead And ret > 0)
            Try
                ret = MyRead(buf, bufferindex, WillCountToRead - bufferindex, CalcInterval(WillCountToRead))
                If (ret > 0) Then
                    If (buf(2) > &HC1) Then
                        EraseInputQueue()
                        Return "Ошибка. Код ошибки:" + Hex(buf(4))
                    End If

                    bufferindex += ret
                    If (bufferindex >= WillCountToRead) Then
                        Return ProcessReceivedData(buf, bufferindex)
                    End If
                End If
            Catch ex As Exception
                EraseInputQueue()
                Return "Ошибка."
            End Try
            System.Threading.Thread.Sleep(CalcInterval(2))
        End While
        If (bufferindex >= 5) Then
            Return ProcessReceivedData(buf, bufferindex)
        End If
        Return ""
    End Function

    Public Overrides Sub Connect()
        Dim i As Integer

        For i = 0 To 5
            If TryConnect() Then
                Return ' True
            End If
            Thread.Sleep(3000)
        Next
        Return 'False
    End Sub
    'Private seekIdx As Integer = 0

    Private Function TryConnect() As Boolean
        EraseInputQueue()

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16



        For i = 0 To 15
            startBytes(i) = &HFF
        Next


        write(startBytes, 16)


        System.Threading.Thread.Sleep(CalcInterval(10))


        Dim bArr(0 To 10) As Byte
        Try

            bArr(0) = &H10
            bArr(1) = &H0
            bArr(2) = &H41
            bArr(3) = &H0
            bArr(4) = &H0
            bArr(5) = &H0
            bArr(6) = &H0
            bArr(7) = &H0
            bArr(8) = &H0
            bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
            bArr(10) = &H16


            EraseInputQueue()
            WillCountToRead = 8
            write(bArr, 11)


            Dim sret As String
            WaitForData()
            sret = GetAndProcessData()
            If (sret.Length > 5) Then
                If (sret.Substring(0, 6) = "Ошибка") Then
                    EraseInputQueue()
                    Return False
                End If
                Debug.Print("!connected !!!")

                mIsConnected = True


                bArr(0) = &H10
                bArr(1) = &H0
                bArr(2) = &H46
                bArr(3) = &H0
                bArr(4) = &H0
                bArr(5) = &H0
                bArr(6) = &H0
                bArr(7) = &H0
                bArr(8) = &H0
                bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
                bArr(10) = &H16

                EraseInputQueue()

                WillCountToRead = 17
                write(bArr, 11)

                WaitForData()
                sret = GetAndProcessData()
                If (sret.Length > 5) Then
                    If (sret.Substring(0, 6) = "Ошибка") Then
                        EraseInputQueue()
                        Return False
                    End If
                End If
                EraseInputQueue()

                Return True
            End If
            If sret.Length = 0 Then
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            End If

        Catch exc As Exception
            Return False
        End Try

    End Function

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String



        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next
        EraseInputQueue()
        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(15))

        clearArchive(Arch)
        Arch.archType = ArchType
        EraseInputQueue()
        Dim bArr(0 To 10) As Byte
        Dim ret As String = ""
        Dim retsum As String = ""
        Dim trycnt As Int32
        Dim tv1OK As Boolean = False
        trycnt = 5
tryagain1:


        If (ArchType = archType_hour) Then
            bArr(2) = &H42 'часовой архив
            bArr(3) = ArchYear \ 256
            bArr(4) = ArchYear Mod 256
            bArr(5) = ArchMonth Mod 13
            bArr(6) = ArchDay Mod 32
            bArr(7) = ArchHour Mod 24
            bArr(8) = &H1

            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)


            'If isVKT4M Then
            WillCountToRead = 77 + 7
            'Else
            '    WillCountToRead = 56 '44 + 7
            'End If
        End If

        If (ArchType = archType_day) Then
            bArr(2) = &H43 'суточный архив
            bArr(3) = ArchYear \ 256
            bArr(4) = ArchYear Mod 256
            bArr(5) = ArchMonth Mod 13
            bArr(6) = ArchDay Mod 32
            bArr(7) = &H0
            bArr(8) = &H1

            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)

            'If isVKT4M Then
            WillCountToRead = 77 + 7
            'Else
            '    WillCountToRead = 66 ' 54 + 7
            'End If

        End If

        If ArchType = archType_hour And Arch.DateArch > Date.Now.AddHours(-1) Then GoTo finalRet
        If ArchType = archType_day And Arch.DateArch > Date.Now.AddDays(-1) Then GoTo finalRet

        bArr(0) = &H10
        bArr(1) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        tv1OK = True



        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = retsum + ret
                If trycnt = 0 Then
                    'Return retsum
                    trycnt = 5
                    GoTo finalRet
                Else
                    trycnt -= 1
                    GoTo tryagain1
                End If
            Else
                tv1OK = True
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            retsum = retsum & vbCrLf & "Ошибка чтения архива"
            tv1OK = False

        End If




finalRet:
        If tv1OK Then
            retsum = "Архив прочитан" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return retsum
        Else
            retsum = "Ошибка чтения" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        End If

    End Function

    Dim m_Param As String = ""

    Public Property Param() As String

        Get
            Return m_Param
        End Get
        Set(ByVal value As String)
            m_Param = value
        End Set
    End Property

    Public Function ProcessReceivedData(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""

        Dim KC As Long = 0
        Try

            If (buf(2) = &H41) Then 'установка связи
                Dim i As Integer
                For i = 0 To ret - 2
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If
                Return retstring
            End If




            If (buf(2) = &H44 And IsTArchToRead = True) Then 'чтение тотального архива
                IsTArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 5 To ret - 2
                    str = str + Chr(buf(i))
                Next

                tArch.archType = 2
                Dim Adr As Long
                Adr = 1
                If str.Length > 24 Then

                    tArch.V1 = FloatExt(Mid(str, Adr + 4 * 0, 4))
                    tArch.V2 = FloatExt(Mid(str, Adr + 4 * 1, 4))
                    tArch.V3 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                    tArch.V4 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                    tArch.Q1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                    tArch.Q2 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If
                If isVKT4M Then
                    Try
                        tArch.V5 = FloatExt(Mid(str, Adr + 4 * 6, 4))

                        'tArch.V3 = FloatExt(Mid(str, Adr + 4 * 8, 4))
                        'tArch.V4 = FloatExt(Mid(str, Adr + 4 * 9, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        tArch.V6 = FloatExt(Mid(str, Adr + 4 * 7, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        tArch.Q3 = FloatExt(Mid(str, Adr + 4 * 10, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        tArch.Q4 = FloatExt(Mid(str, Adr + 4 * 11, 4))
                    Catch ex As Exception

                    End Try


                End If

                m_isTArchToDBWrite = True
                Return "архив прочитан"
            End If

            If (buf(2) = &H45 And IsmArchToRead = True) Then 'чтение мгновенного архива
                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 5 To 14
                    str = str + Chr(buf(i))
                Next

                mArch.archType = 1
                Dim Adr As Long
                Adr = 1

                Dim DateArchYear As Integer
                Dim DateArchMonth As Integer
                Dim DateArchDay As Integer
                Dim DateArchHour As Integer

                Dim DateNowYear As Integer
                Dim DateNowMonth As Integer
                Dim DateNowDay As Integer
                Dim DateNowHour As Integer

                If str.Length >= 10 Then
                    DateArchYear = ExtLong2(Mid(str, Adr, 2))
                    DateArchMonth = Asc(Mid(str, Adr + 2, 1))
                    DateArchDay = Asc(Mid(str, Adr + 3, 1))
                    DateArchHour = Asc(Mid(str, Adr + 4, 1))

                    DateNowYear = ExtLong2(Mid(str, Adr + 5, 2))
                    DateNowMonth = Asc(Mid(str, Adr + 7, 1))
                    DateNowDay = Asc(Mid(str, Adr + 8, 1))
                    DateNowHour = Asc(Mid(str, Adr + 9, 1))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If

                If isVKT4M Then
                    Try
                        mArch.p1 = FloatExt(Mid(str, Adr + 10, 4)) / 100
                    Catch ex As Exception

                    End Try
                    Try
                        mArch.p2 = FloatExt(Mid(str, Adr + 10 + 4 * 1, 4)) / 100
                    Catch ex As Exception

                    End Try
                    Try
                        mArch.p3 = FloatExt(Mid(str, Adr + 10 + 4 * 1, 4)) / 100
                    Catch ex As Exception

                    End Try
                    Try
                        mArch.p4 = FloatExt(Mid(str, Adr + 10 + 4 * 1, 4)) / 100
                    Catch ex As Exception

                    End Try

                End If

                Try
                    mArch.DateArch = New DateTime(CType(DateNowYear, Long), CType(DateNowMonth, Long), CType(DateNowDay, Long), CType(DateNowHour, Long), 0, 0)
                Catch ex As Exception
                    mArch.DateArch = DateTime.Now
                End Try

                m_isMArchToDBWrite = True
                Return "архив прочитан"
            End If

            If (buf(2) = &H42) Then 'часовой архив

                Dim hourstr As String = ""
                Dim i As Int32
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 10 To ret - 2
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1
                If hourstr.Length > &H2C Then
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + &H0, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + &H4, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + &H8, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + &HC, 4))

                    Arch.M1 = FloatExt(Mid(hourstr, Adr + &H10, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + &H14, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + &H18, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + &H1C, 4))

                    Arch.T1 = ExtLong2(Mid(hourstr, Adr + &H20, 2)) / 100.0
                    Arch.T2 = ExtLong2(Mid(hourstr, Adr + &H22, 2)) / 100.0
                    Arch.T3 = ExtLong2(Mid(hourstr, Adr + &H24, 2)) / 100.0
                    Arch.T4 = ExtLong2(Mid(hourstr, Adr + &H26, 2)) / 100.0



                    Arch.HC = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + &H29, 1))
                    Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + &H2A, 1))
                    Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + &H2C, 1))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If
                If isVKT4M Then
                    Try
                        Arch.Q1 = FloatExt(Mid(hourstr, Adr + &H2E, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.Q2 = FloatExt(Mid(hourstr, Adr + &H32, 4))
                    Catch ex As Exception

                    End Try

                    Try
                        Arch.P1 = FloatExt(Mid(hourstr, Adr + &H36, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P2 = FloatExt(Mid(hourstr, Adr + &H38, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P3 = FloatExt(Mid(hourstr, Adr + &H3A, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P4 = FloatExt(Mid(hourstr, Adr + &H3C, 4))
                    Catch ex As Exception

                    End Try
                End If


                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "архив прочитан"
            End If
            If (buf(2) = &H43) Then 'суотчный архив
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 10 To ret - 2
                    hourstr = hourstr + Chr(buf(i))
                Next


                Arch.archType = archType_day
                Dim Adr As Long
                Adr = 1

                'Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 0, 4))
                'Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 1, 4))
                'Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                'Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))

                'Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                'Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                'Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                'Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))

                'Arch.T1 = ExtLong2(Mid(hourstr, Adr + 4 * 8, 2)) / 100.0
                'Arch.T2 = ExtLong2(Mid(hourstr, Adr + 4 * 8 + 2, 2)) / 100.0
                'Arch.T3 = ExtLong2(Mid(hourstr, Adr + 4 * 9, 2)) / 100.0
                'Arch.T4 = ExtLong2(Mid(hourstr, Adr + 4 * 9 + 2, 2)) / 100.0

                'Arch.HC = Asc(Mid(hourstr, Adr + 4 * 10, 1))
                'Arch.HCtv2 = Asc(Mid(hourstr, Adr + 4 * 10 + 1, 1))
                'Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + 4 * 10 + 2, 1))
                '' Arch.TНСEnt1minDay = Asc(Mid(hourstr, Adr + 4 * 10 + 3, 1))

                'Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + 4 * 11, 1))
                '' Arch.TНСEnt2minDay = Asc(Mid(hourstr, Adr + 4 * 11 + 1, 1))

                'Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 11 + 2, 4))
                'Arch.Q2 = FloatExt(Mid(hourstr, Adr + 4 * 12 + 2, 4))
                If hourstr.Length > &H2C Then
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + &H0, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + &H4, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + &H8, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + &HC, 4))

                    Arch.M1 = FloatExt(Mid(hourstr, Adr + &H10, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + &H14, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + &H18, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + &H1C, 4))

                    Arch.T1 = ExtLong2(Mid(hourstr, Adr + &H20, 2)) / 100.0
                    Arch.T2 = ExtLong2(Mid(hourstr, Adr + &H22, 2)) / 100.0
                    Arch.T3 = ExtLong2(Mid(hourstr, Adr + &H24, 2)) / 100.0
                    Arch.T4 = ExtLong2(Mid(hourstr, Adr + &H26, 2)) / 100.0

                    Arch.HC = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + &H29, 1))
                    Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + &H2A, 1))
                    Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + &H2C, 1))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If

                If hourstr.Length >= &H36 Then
                    Arch.Q1 = FloatExt(Mid(hourstr, Adr + &H2E, 4))
                    Arch.Q2 = FloatExt(Mid(hourstr, Adr + &H32, 4))
                End If

                If isVKT4M Then
                    Try
                        Arch.P1 = FloatExt(Mid(hourstr, Adr + &H36, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P2 = FloatExt(Mid(hourstr, Adr + &H38, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P3 = FloatExt(Mid(hourstr, Adr + &H3A, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P4 = FloatExt(Mid(hourstr, Adr + &H3C, 4))
                    Catch ex As Exception

                    End Try

                End If


                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "Архив прочитан"
            End If
            If (buf(2) = &H46) Then 'чтение параметров

                Dim i As Integer
                Dim str As String = ""
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                If (buf(5) And &HF0) = &H10 Then
                    isVKT4M = False
                Else
                    isVKT4M = True
                End If
                POVer = BCD(buf(5))

                str = ""
                For i = 5 To ret - 2
                    str = str & Chr(buf(i))
                Next


                m_Param = str
                Return Param
                'Return "Параметры прочитаны"
            End If
            'MsgBox("Пакет распознан некорректно!", MsgBoxStyle.OkOnly, "Ошибка")
            retstring = "Ошибка"
            Return retstring
        Catch exc As Exception
            MsgBox(exc.Message + " " + exc.StackTrace)
        End Try
        Return "Ошибка!Пакет не распознан"
    End Function

    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function

    Private Function ExtLong2(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong2 = 0
        For i = 0 To 1
            ExtLong2 = ExtLong2 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (1 - i))
        Next i
    End Function

    Private Function FloatExt(ByVal floatStr As String) As Single
        Dim tmpStr As String = ""
        Dim E As Long
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Single
        Dim i As Long
        'If floatStr = "" Then Exit Function
        If floatStr.Length <> 4 Then Exit Function
        ' If floatStr = String(4, 0) Then Exit Function
        If floatStr = Chr(0) + Chr(0) + Chr(0) + Chr(0) Then
            Return 0.0
        End If
        'For i = 1 To 4
        '    tmpStr = Chr(Asc(Mid(floatStr, i, 1))) & tmpStr
        'Next i


        'floatStr = tmpStr
        '================ Float число========================
        'ст.байт                                 младший байт
        '====================================================
        'двоич.порядок |ст.байт                  младший байт
        '----------------------------------------------------
        ' xxxx xxxx     | sxxx xxxx | xxxx xxxx | xxxx xxxx |

        ' A = (-1)^s * f * 2^(e-127)
        ' f= сумма от 0 до 23 a(k)*2^(-k), где a(k) бит мантисы с номером k


        E = Asc(Mid(floatStr, 1, 1))
        If Asc(Mid(floatStr, 2, 1)) And (2 ^ 7) Then
            s = 1
        Else
            s = 0
        End If
        'Mantissa = ((Asc(Mid(floatStr, 2, 1)) And &H7F) << 16) _
        '             + (Asc(Mid(floatStr, 3, 1)) << 8) _
        '             + (Asc(Mid(floatStr, 4, 1)))

        Mantissa = (Asc(Mid(floatStr, 2, 1)) And &H7F) * (2 ^ 16) _
                             + Asc(Mid(floatStr, 3, 1)) * (2 ^ 8) _
                             + Asc(Mid(floatStr, 4, 1))

        f = 2 ^ 0
        For i = 22 To 0 Step -1
            If Mantissa And 2& ^ i Then
                f = f + 2 ^ (i - 23)
            End If
        Next i
        FloatExt = (-1) ^ s * f * 2.0! ^ (E - 127)
    End Function


    Public Overrides Function ReadMArch() As String

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next

        EraseInputQueue()
        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(10))
        Dim ret As String
        Dim bArr(0 To 10) As Byte
        bArr(0) = &H10
        bArr(1) = &H0
        bArr(2) = &H45
        bArr(3) = &H0
        bArr(4) = &H0
        bArr(5) = &H0
        bArr(6) = &H0
        bArr(7) = &H0
        bArr(8) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        IsmArchToRead = True
        clearMArchive(mArch)
        EraseInputQueue()

        WillCountToRead = 18 + 7


        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If

        'mArch.DateArch = DateTime.Now
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты мгновенного архива "
        End If
        m_isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function
    Public Overrides Function ReadTArch() As String




        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next
        EraseInputQueue()
        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(10))

        Dim ret As String
        Dim bArr(0 To 10) As Byte
        bArr(0) = &H10
        bArr(1) = &H0
        bArr(2) = &H44
        bArr(3) = &H0
        bArr(4) = &H0
        bArr(5) = &H0
        bArr(6) = &H0
        bArr(7) = &H0
        bArr(8) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        IsTArchToRead = True
        clearTArchive(tArch)
        EraseInputQueue()


        'If isVKT4M Then
        WillCountToRead = 48 + 7
        'Else
        '    WillCountToRead = 24 + 7
        'End If


        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If

        tArch.DateArch = DateTime.Now

        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты тотального архива "
        End If
        isTArchToDBWrite = True
        Return "Тотальный архив прочитан"
    End Function





    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function

    Public Overrides Function ReadSystemParameters() As System.Data.DataTable
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next

        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(10))

        Dim ret As String
        Dim bArr(0 To 10) As Byte
        bArr(0) = &H10
        bArr(1) = &H0
        bArr(2) = &H46
        bArr(3) = &H0
        bArr(4) = &H0
        bArr(5) = &H0
        bArr(6) = &H0
        bArr(7) = &H0
        bArr(8) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16

        EraseInputQueue()
        WillCountToRead = 13



        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then

                ret = ret & vbCrLf
                ret = ret + "Настройки прибора не прочитаны"
                ret = ret & vbCrLf
                EraseInputQueue()
                dr = dt.NewRow
                dr("Название") = "Ошибка: "
                dr("Значение") = ret
                dt.Rows.Add(dr)
                Return dt
            End If
        End If

        If ret.Length = 0 Then


            ret = ret & vbCrLf
            ret = ret + "Настройки прибора не прочитаны"
            ret = ret & vbCrLf
            EraseInputQueue()
            dr = dt.NewRow
            dr("Название") = "Ошибка: "
            dr("Значение") = ret
            dt.Rows.Add(dr)
            Return dt

        End If


        dr = dt.NewRow
        dr("Название") = "Версия ПО"
        dr("Значение") = BCD((Mid(Param, 1, 1)))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Тип схемы подключения"
        dr("Значение") = Asc(Mid(Param, 1 + 1, 1))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Код объекта"
        dr("Значение") = BCD3(Mid(Param, 1 + 2, 3))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Отчетный день"
        dr("Значение") = Asc(Mid(Param, 1 + 5, 1))
        dt.Rows.Add(dr)


        Return dt
    End Function

    Private Function BCD(ByVal B As Byte) As UInteger
        Dim i As UInteger
        Dim o As UInteger
        i = B
        If (i Mod 16) > 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 100)
        ElseIf (i Mod 16) <= 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 10)
        End If

        Return o And &HFF
    End Function

    Private Function BCD(ByVal B As String) As String
        Dim i As UInteger
        Dim o As UInteger
        i = Asc(Mid(B, 1, 1))
        If (i Mod 16) > 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 100)
        ElseIf (i Mod 16) <= 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 10)
        End If

        Return o.ToString

    End Function

    Private Function BCD3(ByVal B As String) As String


        Return BCD(Mid(B, 3, 1)) & BCD(Mid(B, 2, 1)) & BCD(Mid(B, 1, 1))
    End Function

    Public Overrides Sub EraseInputQueue()

        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
    End Sub



End Class
