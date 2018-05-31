﻿Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Class Driver
    Inherits STKTVMain.TVDriver
    Private mIsConnected As Boolean

    'Private Structure MArchive
    '    Public DateArch As DateTime

    '    Public DateArchYear As Long
    '    Public DateArchMonth As Long
    '    Public DateArchDay As Long
    '    Public DateArchHour As Long

    '    Public DateNowYear As Long
    '    Public DateNowMonth As Long
    '    Public DateNowDay As Long
    '    Public DateNowHour As Long

    '    Public archType As Short
    'End Structure

    'Private Structure TArchive
    '    Public DateArch As DateTime

    '    Public V1 As Double
    '    Public V2 As Double
    '    Public V3 As Double
    '    Public V4 As Double

    '    Public Q1 As Double
    '    Public Q2 As Double

    '    Public archType As Short
    'End Structure

    'Private Structure Archive
    '    Public DateArch As DateTime

    '    Public HC As Long
    '    Public TНСEnt1min As Long

    '    Public ERRTIME2 As Long
    '    Public TНСEnt2min As Long

    '    Public TНСEnt1hourDay As Long
    '    Public TНСEnt1minDay As Long
    '    Public TНСEnt2hourDay As Long
    '    Public TНСEnt2minDay As Long

    '    Public Q1 As Single
    '    Public Q2 As Single

    '    Public T1 As Single
    '    Public T2 As Single
    '    Public T3 As Single
    '    Public T4 As Single

    '    Public M1 As Single
    '    Public M2 As Single
    '    Public M3 As Single
    '    Public M4 As Single

    '    Public V1 As Single
    '    Public V2 As Single
    '    Public V3 As Single
    '    Public V4 As Single

    '    Public archType As Short
    'End Structure



    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
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
        Return "VKT4"
    End Function

    Public Function GetAndProcessData() As String
        Dim buf(80) As Byte
        Dim i As Int16
        For i = 0 To 80
            buf(i) = 0
        Next

        Dim ret As Long

        If (IsBytesToRead = False) Then
            Return ""
        End If

        Try
            ret = MyRead(buf, 0, WillCountToRead, CalcInterval(WillCountToRead))
            If (buf(2) > &HC1) Then
                '
                EraseInputQueue()
                Return "Ошибка. Код ошибки:" + Hex(buf(4))
            End If
            If (ret > 0) Then
                If (ret = WillCountToRead) Then
                    If (ispackageError = True) Then

                        For i = bufferindex + 1 To bufferindex + ret
                            buffer(i) = buf(i - bufferindex - 1)
                        Next
                        If (pagesToRead < 2) Then IsBytesToRead = False
                        bufferindex = 0
                        For i = 0 To 65
                            buffer(i) = 0
                        Next
                        If (pagesToRead < 2) Then EraseInputQueue()
                        ispackageError = False
                        Return writeMessage(buffer, bufferindex)
                    End If
                    If (pagesToRead > 1) Then
                        pagesToRead = pagesToRead - 1
                        Return writeMessage(buf, ret)
                    End If
                    IsBytesToRead = False
                    EraseInputQueue()
                    Return writeMessage(buf, ret)
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

    Public Overrides Sub Connect()
        Dim i As Integer
        'Dim sd As STKTVMain.NportTransportSetupData
        'sd = CType(MyTransport.SetupData(), STKTVMain.NportTransportSetupData)
        'MyTransport.DisConnect()
        'sd.Handshake = Ports.Handshake.RequestToSend
        'MyTransport.SetupTransport(sd)
        'MyTransport.Connect()
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


        If (IsBytesToRead = True) Then
            Return False
        End If



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
            IsBytesToRead = True
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

        If (IsBytesToRead = True) Then
            Return ""
        End If

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next

        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(15))

        cleararchive(Arch)
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
            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)
            WillCountToRead = 56
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
            'Arch.DateArch = Arch.DateArch.AddSeconds(-1)
            WillCountToRead = 66
        End If

        If ArchType = archType_hour And Arch.DateArch > Date.Now.AddHours(-1) Then GoTo finalRet
        If ArchType = archType_day And Arch.DateArch > Date.Now.AddDays(-1) Then GoTo finalRet

        bArr(0) = &H10
        bArr(1) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        tv1OK = True

        IsBytesToRead = True

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

    Public Function writeMessage(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""

        Dim KC As Long = 0
        Try

            If (buf(2) = &H41) Then 'установка связи
                Dim i As Integer
                For i = 0 To 10
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                KC = (256 - ((Int(buf(1)) + Int(buf(2)) + Int(buf(3)) + Int(buf(4)) + Int(buf(5)))) Mod 256) Mod 256
                retstring += vbCrLf
                If (KC <> buf(6)) Then
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
                For i = 1 To 28
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(29)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 5 To 28
                    str = str + Chr(buf(i))
                Next

                tArch.archType = 2
                Dim Adr As Long
                Adr = 1


                tArch.V1 = FloatExt(Mid(str, Adr + 4 * 0, 4))
                tArch.V2 = FloatExt(Mid(str, Adr + 4 * 1, 4))
                tArch.V3 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                tArch.V4 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                tArch.Q1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                tArch.Q2 = FloatExt(Mid(str, Adr + 4 * 5, 4))

                m_isTArchToDBWrite = True
                Return "архив прочитан"
            End If

            If (buf(2) = &H45 And IsmArchToRead = True) Then 'чтение мгновенного архива
                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                KC = 0
                For i = 1 To 14
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(15)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 5 To 14
                    str = str + Chr(buf(i))
                Next

                mArch.archType = 1
                Dim Adr As Long
                Adr = 1

                Dim DateArchYear As Integer = ExtLong2(Mid(str, Adr, 2))
                Dim DateArchMonth As Integer = Asc(Mid(str, Adr + 2, 1))
                Dim DateArchDay As Integer = Asc(Mid(str, Adr + 3, 1))
                Dim DateArchHour As Integer = Asc(Mid(str, Adr + 4, 1))

                Dim DateNowYear As Integer = ExtLong2(Mid(str, Adr + 5, 2))
                Dim DateNowMonth As Integer = Asc(Mid(str, Adr + 7, 1))
                Dim DateNowDay As Integer = Asc(Mid(str, Adr + 8, 1))
                Dim DateNowHour As Integer = Asc(Mid(str, Adr + 9, 1))


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
                For i = 1 To 53
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(54)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 10 To 53
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1

                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 0, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 1, 4))
                Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))

                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                Arch.M4 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))

                Arch.T1 = ExtLong2(Mid(hourstr, Adr + 4 * 8, 2)) / 100.0
                Arch.T2 = ExtLong2(Mid(hourstr, Adr + 4 * 8 + 2, 2)) / 100.0
                Arch.T3 = ExtLong2(Mid(hourstr, Adr + 4 * 9, 2)) / 100.0
                Arch.T4 = ExtLong2(Mid(hourstr, Adr + 4 * 9 + 2, 2)) / 100.0

                Arch.HC = Asc(Mid(hourstr, Adr + 4 * 10, 1))
                Arch.HCtv2 = Asc(Mid(hourstr, Adr + 4 * 10 + 1, 1))
                Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + 4 * 10 + 2, 1))
                Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + 4 * 10 + 3, 1))

                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "архив прочитан"
            End If
            If (buf(2) = &H43) Then 'суотчный архив
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32

                For i = 1 To 63
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(64)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 10 To 63
                    hourstr = hourstr + Chr(buf(i))
                Next


                Arch.archType = archType_day
                Dim Adr As Long
                Adr = 1

                Arch.V1 = FloatExt(Mid(hourstr, Adr + 4 * 0, 4))
                Arch.V2 = FloatExt(Mid(hourstr, Adr + 4 * 1, 4))
                Arch.V3 = FloatExt(Mid(hourstr, Adr + 4 * 2, 4))
                Arch.v4 = FloatExt(Mid(hourstr, Adr + 4 * 3, 4))

                Arch.M1 = FloatExt(Mid(hourstr, Adr + 4 * 4, 4))
                Arch.M2 = FloatExt(Mid(hourstr, Adr + 4 * 5, 4))
                Arch.M3 = FloatExt(Mid(hourstr, Adr + 4 * 6, 4))
                Arch.M4 = FloatExt(Mid(hourstr, Adr + 4 * 7, 4))

                Arch.T1 = ExtLong2(Mid(hourstr, Adr + 4 * 8, 2)) / 100.0
                Arch.T2 = ExtLong2(Mid(hourstr, Adr + 4 * 8 + 2, 2)) / 100.0
                Arch.T3 = ExtLong2(Mid(hourstr, Adr + 4 * 9, 2)) / 100.0
                Arch.T4 = ExtLong2(Mid(hourstr, Adr + 4 * 9 + 2, 2)) / 100.0

                Arch.HC = Asc(Mid(hourstr, Adr + 4 * 10, 1))
                Arch.HCtv2 = Asc(Mid(hourstr, Adr + 4 * 10 + 1, 1))
                Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + 4 * 10 + 2, 1))
                ' Arch.TНСEnt1minDay = Asc(Mid(hourstr, Adr + 4 * 10 + 3, 1))

                Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + 4 * 11, 1))
                ' Arch.TНСEnt2minDay = Asc(Mid(hourstr, Adr + 4 * 11 + 1, 1))

                Arch.Q1 = FloatExt(Mid(hourstr, Adr + 4 * 11 + 2, 4))
                Arch.Q2 = FloatExt(Mid(hourstr, Adr + 4 * 12 + 2, 4))


                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "Архив прочитан"
            End If
            If (buf(2) = &H46) Then 'чтение параметров
                IsTArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                KC = 0
                For i = 1 To 10
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(11)) Then
                    Return "Ошибка!Контрольная сумма не совпала!" ', MsgBoxStyle.OkOnly, "Контрольная сумма")
                    'Return ""
                End If

                For i = 5 To 10
                    str = str + Chr(buf(i))
                Next

                m_Param = str
                Return Param
                'Return "Параметры прочитаны"
            End If
            'MsgBox("Пакет распознан некорректно!", MsgBoxStyle.OkOnly, "Ошибка")
            retstring = "Ошибка"
            Return retstring
        Catch exc As Exception
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
        If (IsBytesToRead = True) Then
            Return ""
        End If

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
        clearMarchive(mArch)
        EraseInputQueue()

        WillCountToRead = 17
        IsBytesToRead = True

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
        If (IsBytesToRead = True) Then
            Return ""
        End If

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
        clearTarchive(tArch)
        EraseInputQueue()

        WillCountToRead = 31
        IsBytesToRead = True

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

        IsBytesToRead = True

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
       
       
        Return BCD(Mid(B, 1, 1)) & BCD(Mid(B, 2, 1)) & BCD(Mid(B, 3, 1))
    End Function

   Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
    End Sub

End Class
