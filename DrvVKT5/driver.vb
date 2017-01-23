
Imports System.Security.Cryptography
Imports System.Text
Imports STKTVMain
Imports System.IO
Imports System.Threading






'Public Structure MArchive
'    Public DateArch As DateTime
'    Public HC As Int32
'    Public MsgHC As String

'    Public HCtv1 As Long
'    Public MsgHC_1 As String

'    Public HCtv2 As Long
'    Public MsgHC_2 As String

'    Public G1 As Single
'    Public G2 As Single
'    Public G3 As Single
'    Public G4 As Single
'    Public G5 As Single
'    Public G6 As Single

'    Public t1 As Single
'    Public t2 As Single
'    Public t3 As Single
'    Public t4 As Single
'    Public t5 As Single
'    Public t6 As Single

'    Public p1 As Single
'    Public p2 As Single
'    Public p3 As Single
'    Public p4 As Single
'    Public p5 As Single
'    Public p6 As Single



'    Public m1 As Single
'    Public m2 As Single
'    Public m3 As Single
'    Public m4 As Single
'    Public m5 As Single
'    Public m6 As Single

'    Public dt12 As Single
'    Public dt45 As Single

'    Public tx1 As Single
'    Public tx2 As Single

'    Public tair1 As Single
'    Public tair2 As Single

'    Public MyTransport As Long
'    Public SPtv1 As Long
'    Public SPtv2 As Long

'    Public dQ1 As Single
'    Public dQ2 As Single


'    Public archType As Short
'End Structure

'Public Structure Archive
'    Public DateArch As DateTime
'    Public Errtime As Long
'    Public HC As Long
'    Public MsgHC As String
'    Public MsgHC_1 As String
'    Public MsgHC_2 As String

'    Public HCtv1 As Long

'    Public HCtv2 As Long


'    Public Tw1 As Single
'    Public Tw2 As Single

'    Public P1 As Single
'    Public T1 As Single
'    Public M1 As Single
'    Public V1 As Single

'    Public P2 As Single
'    Public T2 As Single
'    Public M2 As Single
'    Public V2 As Single

'    Public P3 As Single
'    Public T3 As Single
'    Public M3 As Single
'    Public V3 As Single

'    Public P4 As Single
'    Public T4 As Single
'    Public M4 As Single
'    Public V4 As Single


'    Public P5 As Single
'    Public T5 As Single
'    Public M5 As Single
'    Public V5 As Single

'    Public P6 As Single
'    Public T6 As Single
'    Public M6 As Single
'    Public V6 As Single



'    Public Q1 As Single
'    Public Q2 As Single
'    Public Q3 As Single
'    Public Q4 As Single
'    Public Q5 As Single
'    Public Q6 As Single

'    Public QG1 As Single
'    Public QG2 As Single

'    Public MyTransport As Long
'    Public SPtv1 As Long
'    Public SPtv2 As Long

'    Public tx1 As Long
'    Public tx2 As Long
'    Public tair1 As Long
'    Public tair2 As Long

'    Public V1h As Single
'    Public V2h As Single
'    Public V3h As Single
'    Public V4h As Single
'    Public Q1H As Single
'    Public Q2H As Single

'    Public errtime1 As Int64
'    Public errtime2 As Int64
'    Public oktime1 As Int64
'    Public oktime2 As Int64


'    Public archType As Short
'End Structure

'Public Structure TArchive
'    Public DateArch As DateTime
'    Public MsgHC As String
'    Public MsgHC_1 As String
'    Public MsgHC_2 As String

'    Public V1 As Single
'    Public V2 As Single
'    Public V3 As Single
'    Public V4 As Single
'    Public V5 As Single
'    Public V6 As Single

'    Public M1 As Single
'    Public M2 As Single
'    Public M3 As Single
'    Public M4 As Single
'    Public M5 As Single
'    Public M6 As Single
'    Public Q1 As Single
'    Public Q2 As Single

'    Public TW1 As Single
'    Public TW2 As Single
'    Public Q3 As Single
'    Public Q4 As Single
'    Public Q5 As Single
'    Public Q6 As Single

'    Public HC As Int32
'    Public errtime1 As Int64
'    Public errtime2 As Int64
'    Public oktime1 As Int64
'    Public oktime2 As Int64

'    Public archType As Short

'End Structure





Public Class driver
    Inherits STKTVMain.TVDriver

    Public CurHC As String
    Public IsHC As Boolean = False


    Public Const c_lng256 As Long = 256&

    Private Const A_day As Byte = &H0
    Private Const A_Hour As Byte = &H40
    Private Const A_Total As Byte = &H80


    Private mIsConnected As Boolean

    Private isTCP As Boolean
    Private SleepTime As Long
    Private SequenceErrorCount As Integer = 0


    Dim IsTArchToRead As Boolean = False

    Dim tv As Short


    Dim ActiveCount As Integer




    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False

    Dim bufferindex As Short = 0



    Dim m_isArchToDBWrite As Boolean = False

    Private Enum VKT5TubeType
        tubeIn = 0
        tubeOut = 1
        tubeHot = 2
        tubeAdd = 3
        electro = 4
        tubeCold = 5
    End Enum

    Private Class VKT5TubeCFG
        Public tubeInpNumber As Byte
        Public tubeType As VKT5TubeType
        Public meterT As Byte
        Public meterP As Byte
        Public materTAdd As Byte
        Public NositelType As Byte
        Public rashodomer As Byte
    End Class

    Private sysCFG(8) As VKT5TubeCFG

    Public Overrides Property isArchToDBWrite() As Boolean

        Get
            Return m_isArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isArchToDBWrite = value
        End Set
    End Property
    Dim m_isMArchToDBWrite As Boolean = False


    Public Overrides Function CounterName() As String
        Return "VKT5"
    End Function

    Private Function DecodeError(e As Byte) As String
        Select Case e
            
            Case 0
                Return "Выбранный тепловой ввод не используется"
            Case 1
                Return "Выбранная труба не используется"
            Case 2
                Return "Нет данных за указанную дату"
            Case 3
                Return "Выход за пределы памяти области настроек"

            Case 4
                Return "Несуществующий номер архивной записи"
            Case 5
                Return "Архив в приборе пуст"
            Case 6
                Return "Несуществующий код клавиши"
            Case 7
                Return "Прибор не поддерживает данный запрос"
            Case 8
                Return "Ошибка записи воFLASH-память"
            Case 9
                Return "Доступ к записи настроек закрыт"
            Case Else
                Return "Ошибка прибора"
        End Select


    End Function

    Private MaxTvNumber As Integer = 0
    Private nTr(8) As Integer

    Private Function GetCFG() As Boolean
        Dim Frame(15) As Byte
        Dim ch As UInt16
        Frame(0) = &H0
        Frame(1) = &H3
        Frame(2) = &HA
        Frame(3) = &H0
        Frame(4) = &H0
        Frame(5) = 28
        ch = CheckSum(Frame, 0, 6)
        Frame(6) = ch Mod 256
        Frame(7) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 8)

        WaitForData()


        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim cfgSz As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                MyTransport.Read(b, ptr, cnt)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then

                    If (b(1) And &H80) = &H80 Then
                        DriverTransport.SendEvent(UnitransportAction.LowLevelStop, DecodeError(b(2)))
                        Return False
                    Else



                        ' save to CFG
                        cfgSz = b(3) * 255 + b(2)
                        Dim i As Integer
                        Dim idx As Integer
                        For i = 1 To 8
                            nTr(i) = 0
                        Next
                        If cfgSz >= 56 Then
                            For i = 1 To 8
                                idx = 7 * (i - 1) + 4
                                If b(idx) > 0 And b(idx) > MaxTvNumber Then
                                    MaxTvNumber = b(idx)
                                End If
                                If b(idx) > 0 Then
                                    nTr(b(idx)) += 1
                                End If
                                sysCFG(i) = New VKT5TubeCFG()
                                sysCFG(i).tubeInpNumber = b(idx)
                                sysCFG(i).tubeType = b(idx + 1)
                                sysCFG(i).meterT = b(idx + 2)
                                sysCFG(i).meterP = b(idx + 3)
                                sysCFG(i).materTAdd = b(idx + 4)
                                sysCFG(i).NositelType = b(idx + 5)
                                sysCFG(i).rashodomer = b(idx + 6)
                            Next

                            Return True
                        End If
                    End If
                End If


                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        Else
            DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            Return False
        End If

    End Function


    Private Function DevInit() As Boolean



        'FF FF 00 10 3F FF 00 00 CC 63 00 00 00 64 54

        '00 03 0E 00 00 01 87 33 
        Dim Frame(15) As Byte
        Dim ch As UInt16
        Frame(0) = &H0
        Frame(1) = &H3
        Frame(2) = &HE
        Frame(3) = &H0
        Frame(4) = &H0
        Frame(5) = &H1
        ch = CheckSum(Frame, 0, 6)
        Frame(6) = ch Mod 256
        Frame(7) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 8)

        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                MyTransport.Read(b, ptr, cnt)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    If (b(1) And &H80) = &H80 Then
                        DriverTransport.SendEvent(UnitransportAction.LowLevelStop, DecodeError(b(2)))
                        Return False
                    Else

                        Version = b(4)
                        If (Version >> 4) = 0 Then
                            Version = Version And &HF
                        Else
                            Version = (Version >> 4) And &HF
                        End If

                        Return True
                    End If

                End If
                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        Else
            DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            Return False
        End If

    End Function


    


    Private Version As Byte

    Public Overrides Sub Connect()
        SleepTime = 700
        MyTransport.CleanPort()
        EraseInputQueue()
        mIsConnected = False

        Dim t As Integer
        t = 7
        While Not mIsConnected And t > 0
            Try

                If DevInit() Then
                    mIsConnected = True

                    Dim dp As Integer
                    Dim ok As Boolean
                    dp = 7
                    ok = GetCFG()
                    While Not ok And dp > 0
                        dp = dp - 1
                        ok = GetCFG()
                    End While
                    If Not ok Then
                        mIsConnected = False
                    End If

                    If ok Then
                        GetArchiveDates()
                    End If

                End If


            Catch exc As Exception
                'Return
            End Try
            t = t - 1
        End While


    End Sub

    Private m_readRAMByteCount As Short

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short, _
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String

        Dim retsum As String
        'Dim ok As Boolean
        SequenceErrorCount = 0
        Try


            cleararchive(Arch)
            EraseInputQueue()


            Dim dt1 As Date
            Dim dt2 As Date



            If SequenceErrorCount > 5 Then GoTo ArchErr

            If ArchType = archType_hour Then

                dt2 = New Date(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)

                If dt2 <= MaxArchDate And dt2 >= MinArchDate Then
                    Arch.archType = ArchType

                    If IsError Then
                        Return ErrorMessage
                    End If
                    SetArchDate(dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    GetArchData(ArchType, dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                Else

                    isArchToDBWrite = False
                    Return "Ошибка: Часовой архив еще не сформирован"
                End If
            End If


            If ArchType = archType_day Then
                dt2 = New Date(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
                dt1 = dt2.AddHours(23)
                If dt1 <= MaxArchDate And dt1 >= MinArchDate Then
                    Arch.archType = ArchType

                    If IsError Then
                        Return ErrorMessage
                    End If
                    SetArchDate(dt1)
                    If IsError Then
                        Return ErrorMessage
                    End If
                    GetArchData(ArchType, dt2)
                    If IsError Then
                        Return ErrorMessage
                    End If
                Else
                    isArchToDBWrite = False
                    Return "Ошибка: Суточный архив еще не сформирован"
                End If
            End If

          
       
            Dim AErr As String
            AErr = ErrorMessage

            If AErr = "" Then
                retsum = "Архив прочитан"
                retsum = retsum & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = True
            Else


                retsum = "Ошибка: не удалось получить часть параметров " & dt2.ToString() + "->"
                retsum = retsum & AErr & vbCrLf
                EraseInputQueue()
                isArchToDBWrite = False
            End If

            Return retsum


archErr:

            retsum = "Ошибка не удалось получить архив "
            EraseInputQueue()
            isArchToDBWrite = False
            Return retsum

        Catch ex As System.Exception
            Return "Ошибка:" & ex.Message
        End Try
    End Function

     Public overrides Function DeCodeHCNumber(ByVal CodeHC As Long, optional tv As Integer=0) as string

        Try
            'If CodeHC >= 32 And CodeHC < 127 Then
            '    DeCodeHCNumber = Chr(CodeHC) + " "
            'Else
            DeCodeHCNumber = "-"
            'End If

        Catch ex As Exception
            DeCodeHCNumber = "-"
        End Try






    End Function
    Public overrides Function DeCodeHCText(ByVal CodeHC As Long) As String
        Try
            'If CodeHC >= 32 And CodeHC < 127 Then
            '    DeCodeHCText = Chr(CodeHC) + " "
            'Else
            DeCodeHCText = "-"
            'End If

        Catch ex As Exception
            DeCodeHCText = "-"
        End Try


    End Function
    Public overrides Function DeCodeHC(ByVal CodeHC As Long) As String
        Try
            'If CodeHC >= 32 And CodeHC < 127 Then
            '    DeCodeHC = Chr(CodeHC) + " "
            'Else
            DeCodeHC = "-"
            'End If

        Catch ex As Exception
            DeCodeHC = "-"
        End Try


    End Function





    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0
        System.Threading.Thread.Sleep(250)
        MyTransport.CleanPort()
    End Sub


    Private Function GetArchData(ByVal aType As Integer, ByVal dt2 As Date) As Boolean
        clearArchive(Arch)


        ErrorMessage = ""
        IsError = False

        Dim tv As Integer
        Dim Frame(10) As Byte
        Dim ch As UInt16
        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim tubeNo As Integer
        Dim maxTubeNo As Integer
        Dim ptr As Integer
        Dim sz As Integer
        maxTubeNo = 0
        tubeNo = 0
        If aType = 3 Then
            Arch.OKTIME1 = 60
            Arch.OKTIME2 = 60
        Else
            Arch.OKTIME1 = 60 * 24
            Arch.OKTIME2 = 60 * 24
        End If

        ' цикл по тепловым вводам
        For tv = 1 To MaxTvNumber


            ' запрос данных
            Frame(0) = &H0
            Frame(1) = &H4

            If aType = 3 Then
                Frame(2) = A_Hour
            Else
                Frame(2) = A_day
            End If

            Frame(3) = tv * 28
            Frame(4) = &H0
            Frame(5) = (nTr(tv) * 3 + 4) * 2  ' количество труб +++
            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch Mod 256
            Frame(7) = ch \ 256

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 8)

            WaitForData()

            tubeNo = 0


            cnt = MyTransport.BytesToRead
            If cnt > 0 Then

                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt


                    If VerifySumm(b, 0, sz) Then
                        SequenceErrorCount = 0

                        If (b(1) And &H80) = &H80 Then
                            ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                            SequenceErrorCount += 1
                            IsError = True
                        Else


                            If b(2) > 20 Then
                                tubeNo += 1
                                Dim mx As Integer
                                If Version < 6 Then
                                    mx = 16
                                Else
                                    mx = 20
                                End If

                                While tubeNo * 12 + mx <= b(2)

                                    Select Case maxTubeNo + tubeNo
                                        Case 1
                                            Arch.T1 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            Arch.P1 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            Arch.M1 = BToSingle(b, (tubeNo - 1) * 12 + 11)

                                        Case 2
                                            Arch.T2 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            Arch.P2 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            Arch.M2 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Case 3
                                            Arch.T3 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            Arch.P3 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            Arch.M3 = BToSingle(b, (tubeNo - 1) * 12 + 11)

                                        Case 4
                                            Arch.T4 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            Arch.P4 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            Arch.M4 = BToSingle(b, (tubeNo - 1) * 12 + 11)

                                        Case 5
                                            Arch.T5 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            Arch.P5 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            Arch.M5 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Case 6
                                            Arch.T6 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            Arch.P6 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            Arch.M6 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                    End Select
                                    tubeNo += 1
                                End While

                                Select Case tv
                                    Case 1
                                        Arch.Q1 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Arch.Q2 = BToSingle(b, (tubeNo - 1) * 12 + 15)
                                        'If Version >= 6 Then
                                        '    Try
                                        '        Arch.oktime1 = BToSingle(b, (tubeNo - 1) * 12 + 19)
                                        '    Catch ex As Exception

                                        '    End Try

                                        'End If
                                    Case 2
                                        Arch.Q3 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Arch.Q4 = BToSingle(b, (tubeNo - 1) * 12 + 15)
                                        'If Version >= 6 Then
                                        '    Try
                                        '        Arch.oktime2 = BToSingle(b, (tubeNo - 1) * 12 + 19)
                                        '    Catch ex As Exception

                                        '    End Try

                                        'End If
                                    Case 3
                                        Arch.Q5 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Arch.Q6 = BToSingle(b, (tubeNo - 1) * 12 + 15)

                                End Select


                            End If
                        End If
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                SequenceErrorCount += 1
            End If

            maxTubeNo += (tubeNo - 1)


            ' запрос нештатных ситуаций по вводу
            Dim ncTubeNo As Integer
            Frame(0) = &H0
            Frame(1) = &H4

            If aType = 3 Then
                Frame(2) = A_Hour + 4
            Else
                Frame(2) = A_day + 4
            End If

            Frame(3) = tv
            Frame(4) = &H0
            Frame(5) = nTr(tv) * 9 + 2 ' количество труб +++
            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch Mod 256
            Frame(7) = ch \ 256

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 8)

            WaitForData()

            ncTubeNo = 0


            cnt = MyTransport.BytesToRead
            If cnt > 0 Then

                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt


                    If VerifySumm(b, 0, sz) Then
                        SequenceErrorCount = 0

                        If (b(1) And &H80) = &H80 Then
                            ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                            SequenceErrorCount += 1
                            IsError = True
                        Else


                            If b(2) > 20 Then
                                ncTubeNo += 1
                                Dim mNC As String = ""
                                While ncTubeNo * 18 + 4 <= b(2)
                                    Dim tNc As Int16
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                    If tNc > 0 Then
                                        mNC += "Tmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                    If tNc > 0 Then
                                        mNC += "Tmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 7)
                                    If tNc > 0 Then
                                        mNC += "Pmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 9)
                                    If tNc > 0 Then
                                        mNC += "Pmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 11)
                                    If tNc > 0 Then
                                        mNC += "Gmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 13)
                                    If tNc > 0 Then
                                        mNC += "Gmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 15)
                                    If tNc > 0 Then
                                        mNC += "Gsit" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 15)
                                    If tNc > 0 Then
                                        mNC += "Sost" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If


                                    ncTubeNo += 1
                                End While


                                Select Case tv
                                    Case 1
                                        Arch.HCtv1 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        Arch.ERRTIME1 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                        Arch.OKTIME1 -= Arch.ERRTIME1
                                        Arch.OKTIME1 -= Arch.HCtv1

                                    Case 2
                                        Arch.HCtv2 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        Arch.ERRTIME2 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                        Arch.OKTIME2 -= Arch.ERRTIME2
                                        Arch.OKTIME2 -= Arch.HCtv2
                                    Case 3
                                        Arch.HC = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        Arch.ERRTIME = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)

                                End Select

                                If mNC <> "" Then
                                    Select Case tv
                                        Case 1
                                            Arch.MsgHC_1 += mNC

                                        Case 2
                                            Arch.MsgHC_2 += mNC

                                        Case 3
                                            Arch.MsgHC += mNC
                                    End Select
                                End If
                            End If
                        End If
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                SequenceErrorCount += 1
            End If





        Next


        If SequenceErrorCount > 5 Then GoTo ArchErr

        Dim AErr As String = ""
        Arch.archType = aType
        Arch.DateArch = dt2
        If SequenceErrorCount > 5 Then GoTo ArchErr

        Try
            'pVal = GetParam0(VKT7ElemType.NSPrintTypeM_1)
            'arch.HCtv1 = Convert.ToInt32(pVal, 2)


        Catch
        End Try

        Try
            'pVal = GetParam0(VKT7ElemType.NSPrintTypeM_2)
            'arch.HCtv2 = Convert.ToInt32(pVal, 2)


        Catch
        End Try

        Dim retsum As String
        retsum = "Архив прочитан"
        If IsError = False Then
            'retsum = "Мгновенный архив прочитан"
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return True
        Else
            retsum = "Не удалось получить часть параметров "
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return True
        End If

ArchErr:

        EraseInputQueue()
        isArchToDBWrite = False
        Return False

    End Function


    Public Overrides Function ReadMArch() As String
        clearMarchive(mArch)


        '00 03 00 1C 00 00 85 DD 
        Dim tv As Integer
        Dim Frame(10) As Byte
        Dim ch As UInt16
        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim tubeNo As Integer
        Dim MaxTubeNo As Integer
        Dim ptr As Integer
        Dim sz As Integer
        Dim AErr As String = ""

        MaxTubeNo = 0
        tubeNo = 0

        ' цикл по тепловым вводам
        For tv = 1 To MaxTvNumber


            ' запрос данных
            Frame(0) = &H0
            Frame(1) = &H3
            Frame(2) = 0
            Frame(3) = tv * 28
            Frame(4) = &H0
            Frame(5) = (nTr(tv) * 3 + 4) * 2  ' количество труб +++
            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch Mod 256
            Frame(7) = ch \ 256

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 8)

            WaitForData()

            tubeNo = 0


            cnt = MyTransport.BytesToRead
            If cnt > 0 Then

                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt


                    If VerifySumm(b, 0, sz) Then
                        SequenceErrorCount = 0

                        If (b(1) And &H80) = &H80 Then
                            ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                            AErr = ErrorMessage
                            SequenceErrorCount += 1
                            IsError = True
                        Else


                            If b(2) > 20 Then
                                tubeNo += 1
                                Dim mx As Integer
                                If Version < 6 Then
                                    mx = 16
                                Else
                                    mx = 20
                                End If


                                While tubeNo * 12 + mx <= b(2)
                                    Select Case MaxTubeNo + tubeNo
                                        Case 1
                                            mArch.t1 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            mArch.p1 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            mArch.M1 = BToSingle(b, (tubeNo - 1) * 12 + 11)

                                        Case 2
                                            mArch.t2 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            mArch.p2 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            mArch.M2 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Case 3
                                            mArch.t3 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            mArch.p3 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            mArch.M3 = BToSingle(b, (tubeNo - 1) * 12 + 11)

                                        Case 4
                                            mArch.t4 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            mArch.p4 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            mArch.M4 = BToSingle(b, (tubeNo - 1) * 12 + 11)

                                        Case 5
                                            mArch.t5 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            mArch.p5 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            mArch.M5 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                        Case 6
                                            mArch.t6 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            mArch.p6 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            mArch.M6 = BToSingle(b, (tubeNo - 1) * 12 + 11)
                                    End Select
                                    tubeNo += 1
                                End While

                                Select Case tv
                                    Case 1

                                        mArch.G1 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                        mArch.G2 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                    Case 2
                                        mArch.G3 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                        mArch.G4 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                    Case 3
                                        mArch.G5 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                        mArch.G6 = BToSingle(b, (tubeNo - 1) * 12 + 7)

                                End Select

                            End If
                        End If
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                SequenceErrorCount += 1
                AErr = "Ошибка: данные не получены. Ввод " + tv.ToString + ". "
            End If

            MaxTubeNo += (tubeNo - 1)


            ' запрос нештатных ситуаций по вводу
            Dim ncTubeNo As Integer
            Frame(0) = &H0
            Frame(1) = &H3
            Frame(2) = 4
            Frame(3) = tv
            Frame(4) = &H0
            Frame(5) = nTr(tv) * 9 + 2 ' количество труб +++
            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch Mod 256
            Frame(7) = ch \ 256

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 8)

            WaitForData()

            ncTubeNo = 0


            cnt = MyTransport.BytesToRead
            If cnt > 0 Then

                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt


                    If VerifySumm(b, 0, sz) Then
                        SequenceErrorCount = 0

                        If (b(1) And &H80) = &H80 Then
                            ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                            AErr = ErrorMessage
                            SequenceErrorCount += 1
                            IsError = True
                        Else


                            If b(2) > 20 Then
                                ncTubeNo += 1
                                Dim mNC As String = ""
                                While ncTubeNo * 18 + 4 <= b(2)
                                    Dim tNc As Int16
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                    If tNc > 0 Then
                                        mNC += "Tmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                    If tNc > 0 Then
                                        mNC += "Tmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 7)
                                    If tNc > 0 Then
                                        mNC += "Pmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 9)
                                    If tNc > 0 Then
                                        mNC += "Pmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 11)
                                    If tNc > 0 Then
                                        mNC += "Gmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 13)
                                    If tNc > 0 Then
                                        mNC += "Gmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 15)
                                    If tNc > 0 Then
                                        mNC += "Gsit" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 15)
                                    If tNc > 0 Then
                                        mNC += "Sost" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If


                                    ncTubeNo += 1
                                End While


                                Select Case tv
                                    Case 1
                                        mArch.HCtv1 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        'mArch.errtime1 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                        'mArch.oktime1 -= mArch.errtime1
                                        'mArch.oktime1 -= mArch.HCtv1

                                    Case 2
                                        mArch.HCtv2 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        'mArch.errtime2 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                        'mArch.oktime2 -= mArch.errtime2
                                        'mArch.oktime2 -= mArch.HCtv2
                                    Case 3
                                        mArch.HC = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        'mArch.Errtime = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)

                                End Select

                                If mNC <> "" Then
                                    Select Case tv
                                        Case 1
                                            mArch.MsgHC_1 += mNC

                                        Case 2
                                            mArch.MsgHC_2 += mNC

                                        Case 3
                                            mArch.MsgHC += mNC
                                    End Select
                                End If
                            End If
                        End If
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                SequenceErrorCount += 1
                AErr = "Ошибка: данные НС не получены. Ввод " + tv.ToString + ". "
            End If





        Next


        mArch.DateArch = GetDeviceDate()



        Dim retsum As String
        retsum = "Мгновенный архив прочитан"
        If AErr = "" Then
            'retsum = "Мгновенный архив прочитан"
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isMArchToDBWrite = True
            Return retsum
        Else
            retsum = "Не удалось получить часть параметров "
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isMArchToDBWrite = False
            Return retsum
        End If

mArchErr:

        EraseInputQueue()
        isMArchToDBWrite = False
        Return "Ошибка чтения мгновенного архива"

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




    Public Overrides Function ReadTArch() As String

        clearTarchive(tArch)



        '00 03 00 1C 00 00 85 DD 
        Dim tv As Integer
        Dim Frame(10) As Byte
        Dim ch As UInt16
        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim tubeNo As Integer
        Dim MaxTubeNo As Integer
        Dim ptr As Integer
        Dim sz As Integer
        Dim AErr As String = ""

        MaxTubeNo = 0
        tubeNo = 0

        ' цикл по тепловым вводам
        For tv = 1 To MaxTvNumber


            ' запрос данных
            Frame(0) = &H0
            Frame(1) = &H4
            Frame(2) = &H80
            Frame(3) = tv * 28
            Frame(4) = &H0
            Frame(5) = (nTr(tv) * 3 + 4) * 2  ' количество труб +++
            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch Mod 256
            Frame(7) = ch \ 256

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 8)

            WaitForData()

            tubeNo = 0


            cnt = MyTransport.BytesToRead
            If cnt > 0 Then

                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt


                    If VerifySumm(b, 0, sz) Then
                        SequenceErrorCount = 0

                        If (b(1) And &H80) = &H80 Then
                            ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                            AErr = ErrorMessage
                            SequenceErrorCount += 1
                            IsError = True
                        Else


                            If b(2) > 20 Then
                                tubeNo += 1
                                Dim mx As Integer
                                If Version < 6 Then
                                    mx = 32
                                Else
                                    mx = 40
                                End If

                                While tubeNo * 16 + mx <= b(2)
                                    Select Case MaxTubeNo + tubeNo
                                        Case 1
                                            'tArch.t1 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            'tArch.p1 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            tArch.M1 = BToDouble(b, (tubeNo - 1) * 16 + 11)

                                        Case 2
                                            'tArch.t2 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            'tArch.p2 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            tArch.M2 = BToDouble(b, (tubeNo - 1) * 16 + 11)
                                        Case 3
                                            'tArch.t3 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            'tArch.p3 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            tArch.M3 = BToDouble(b, (tubeNo - 1) * 16 + 11)

                                        Case 4
                                            'tArch.t4 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            'tArch.p4 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            tArch.M4 = BToDouble(b, (tubeNo - 1) * 16 + 11)

                                        Case 5
                                            'tArch.t5 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            'tArch.p5 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            tArch.M5 = BToDouble(b, (tubeNo - 1) * 16 + 11)
                                        Case 6
                                            'tArch.t6 = BToSingle(b, (tubeNo - 1) * 12 + 3)
                                            'tArch.p6 = BToSingle(b, (tubeNo - 1) * 12 + 7)
                                            tArch.M6 = BToDouble(b, (tubeNo - 1) * 16 + 11)
                                    End Select
                                    tubeNo += 1
                                End While



                                Select Case tv
                                    Case 1

                                        tArch.Q1 = BToDouble(b, (tubeNo - 1) * 16 + 19)
                                        tArch.Q2 = BToDouble(b, (tubeNo - 1) * 16 + 27)
                                        If Version >= 6 Then
                                            Try
                                                Arch.OKTIME1 = BToDouble(b, (tubeNo - 1) * 16 + 35)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Case 2
                                        tArch.Q3 = BToDouble(b, (tubeNo - 1) * 16 + 19)
                                        tArch.Q4 = BToDouble(b, (tubeNo - 1) * 16 + 27)
                                        If Version >= 6 Then
                                            Try
                                                Arch.OKTIME2 = BToDouble(b, (tubeNo - 1) * 16 + 35)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Case 3
                                        tArch.Q5 = BToSingle(b, (tubeNo - 1) * 16 + 19)
                                        tArch.Q6 = BToDouble(b, (tubeNo - 1) * 16 + 27)

                                End Select


                            End If
                        End If
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                SequenceErrorCount += 1
                AErr = "Ошибка: данные не получены. Ввод " + tv.ToString + ". "
            End If

            MaxTubeNo += (tubeNo - 1)


            ' запрос нештатных ситуаций по вводу

            Dim ncTubeNo As Integer
            Frame(0) = &H0
            Frame(1) = &H3
            Frame(2) = &H84
            Frame(3) = tv
            Frame(4) = &H0
            Frame(5) = nTr(tv) * 9 + 2 ' количество труб +++
            ch = CheckSum(Frame, 0, 6)
            Frame(6) = ch Mod 256
            Frame(7) = ch \ 256

            MyTransport.CleanPort()
            MyTransport.Write(Frame, 0, 8)

            WaitForData()

            ncTubeNo = 0


            cnt = MyTransport.BytesToRead
            If cnt > 0 Then

                ptr = 0
                sz = 0
                While cnt > 0
                    MyTransport.Read(b, ptr, cnt)
                    ptr += cnt
                    sz += cnt


                    If VerifySumm(b, 0, sz) Then
                        SequenceErrorCount = 0

                        If (b(1) And &H80) = &H80 Then
                            ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                            AErr = ErrorMessage
                            SequenceErrorCount += 1
                            IsError = True
                        Else


                            If b(2) > 20 Then
                                ncTubeNo += 1
                                Dim mNC As String = ""
                                While ncTubeNo * 18 + 4 <= b(2)
                                    Dim tNc As Int16
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                    If tNc > 0 Then
                                        mNC += "Tmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                    If tNc > 0 Then
                                        mNC += "Tmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 7)
                                    If tNc > 0 Then
                                        mNC += "Pmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 9)
                                    If tNc > 0 Then
                                        mNC += "Pmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 11)
                                    If tNc > 0 Then
                                        mNC += "Gmax" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 13)
                                    If tNc > 0 Then
                                        mNC += "Gmin" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 15)
                                    If tNc > 0 Then
                                        mNC += "Gsit" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If
                                    tNc = Bytes2Int(b, (ncTubeNo - 1) * 18 + 15)
                                    If tNc > 0 Then
                                        mNC += "Sost" + ncTubeNo.ToString() + ":" + tNc.ToString + ";"
                                    End If


                                    ncTubeNo += 1
                                End While


                                Select Case tv
                                    Case 1
                                        'tArch.HCtv1 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        tArch.ERRTIME1 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                        'tArch.oktime1 -= tArch.errtime1
                                        'tArch.oktime1 -= tArch.HCtv1

                                        'If Version >= 6 Then
                                        '    Try
                                        '        Arch.oktime1 = BToDouble(b, (tubeNo - 1) * 18 + 17)
                                        '    Catch ex As Exception
                                        '    End Try
                                        'End If

                                    Case 2
                                        'tArch.HCtv2 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        tArch.ERRTIME2 = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)
                                        'tArch.oktime2 -= tArch.errtime2
                                        'tArch.oktime2 -= tArch.HCtv2

                                        'If Version >= 6 Then
                                        '    Try
                                        '        Arch.oktime2 = BToDouble(b, (tubeNo - 1) * 18 + 17)
                                        '    Catch ex As Exception
                                        '    End Try
                                        'End If
                                    Case 3
                                        tArch.HC = Bytes2Int(b, (ncTubeNo - 1) * 18 + 3)
                                        'tArch.Errtime = Bytes2Int(b, (ncTubeNo - 1) * 18 + 5)

                                End Select

                                If mNC <> "" Then
                                    Select Case tv
                                        Case 1
                                            tArch.MsgHC_1 += mNC

                                        Case 2
                                            tArch.MsgHC_2 += mNC

                                        Case 3
                                            tArch.MsgHC += mNC
                                    End Select
                                End If
                            End If
                        End If
                    End If

                    RaiseIdle()
                    Thread.Sleep(CalcInterval(2))
                    cnt = MyTransport.BytesToRead
                End While
            Else
                SequenceErrorCount += 1
                AErr = "Ошибка: данные НС не получены. Ввод " + tv.ToString + ". "
            End If





        Next


        If SequenceErrorCount > 5 Then GoTo archerr




        tArch.DateArch = GetDeviceDate()

        If SequenceErrorCount > 5 Then GoTo archerr




        Dim retsum As String
        retsum = "Итоговый архив прочитан"
        If AErr = "" Then

            retsum = retsum & vbCrLf
            EraseInputQueue()
            isTArchToDBWrite = True
            Return retsum
        Else
            retsum = "Не удалось получить часть параметров "
            retsum = retsum & AErr & vbCrLf
            EraseInputQueue()
            isTArchToDBWrite = True
            Return retsum
        End If
archerr:
        retsum = "Ошибка чтения итогового архива"
        EraseInputQueue()
        isTArchToDBWrite = False
        Return retsum

    End Function



    Private Function ExtLong4(ByVal extStr As String) As Single
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


    Private mIsError As Boolean

    Public Property IsError() As Boolean
        Get
            Return mIsError
        End Get
        Private Set(ByVal value As Boolean)
            mIsError = value
        End Set
    End Property

    Private mErrorMessage As String

    Public Property ErrorMessage() As String
        Get
            Return mErrorMessage
        End Get
        Private Set(ByVal value As String)
            mErrorMessage = value
        End Set
    End Property

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
        SequenceErrorCount = 0
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow


        ''0x00 0x03 0x3f 0xf9 0x00 0x00 0x98 0x3e
        'Dim Frame(10) As Byte
        'Dim ch As UInt16
        'Frame(0) = &HFF
        'Frame(1) = &HFF
        'Frame(2) = &H0
        'Frame(3) = &H3
        'Frame(4) = &H3F
        'Frame(5) = &HF9
        'Frame(6) = &H0
        'Frame(7) = &H0
        'ch = CheckSum(Frame, 2, 6)
        'Frame(8) = ch Mod 256 '&H98
        'Frame(9) = ch \ 256 '&H3E

        'MyTransport.CleanPort()
        'MyTransport.Write(Frame, 0, 10)

        'WaitForData()

        'Dim b(4096) As Byte
        'Dim cnt As Integer
        'cnt = MyTransport.BytesToRead
        'If cnt > 0 Then
        '    Dim ptr As Integer
        '    Dim sz As Integer
        '    ptr = 0
        '    sz = 0
        '    While cnt > 0
        '        MyTransport.Read(b, ptr, cnt)
        '        ptr += cnt
        '        sz += cnt

        '        RaiseIdle()
        '        Thread.Sleep(CalcInterval(2))
        '        cnt = MyTransport.BytesToRead
        '    End While
        'End If

        dr = dt.NewRow
        dr("Название") = "Номер версии ПО "
        dr("Значение") = Version.ToString
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

    Public Overrides Property isMArchToDBWrite() As Boolean
        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property




   


    Private ActiveElements(100) As Integer
    Private ElemSize(100) As Byte
    Private PropVal(100) As Byte

 
    Public Function VerifySumm(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As Boolean
        Dim ch As Long
        If sz <= 2 Then Return False
        ch = CheckSum(Data, offset, sz - 2)
        If Data(offset + sz - 2) = ch Mod 256 And Data(offset + sz - 1) = ch \ 256 Then
            Return True
        End If
        Return False
    End Function



    Private Function CheckSum(ByVal Data() As Byte, ByVal offset As Integer, ByVal sz As Integer) As UInt16
        Dim s As UInt16
        Dim sl As Long
        sl = &HFFFF
        On Error Resume Next
        For i As Integer = 0 To sz - 1

            sl = ((sl \ 256) * 256 + ((sl Mod 256) Xor Data(offset + i))) And &HFFFF
            For sh As Integer = 0 To 7
                If (sl And 1) = 1 Then
                    sl = ((sl \ 2) Xor &HA001) And &HFFFF
                Else
                    sl = (sl \ 2) And &HFFFF
                End If


            Next
        Next
        s = sl And &HFFFF
        Return s
    End Function

    Public Function Bytes2Float(ByVal fbytes() As Byte, ByVal index As Int16) As Single
        If UBound(fbytes) - index < 3 Then
            Return 0
        End If
        Return System.BitConverter.ToSingle(fbytes, index)
    End Function

    Public Function Bytes2Int(ByVal fbytes() As Byte, ByVal index As Int16) As Int16
        If UBound(fbytes) - index < 2 Then
            Return 0
        End If
        Dim iInputIndex As Integer, iOutputIndex As Integer = 0
        Dim bArray(0 To 1) As Byte
        For iInputIndex = 0 To 1

            bArray(iOutputIndex) = fbytes(index + iInputIndex)

            iOutputIndex += 1

        Next
        Array.Reverse(bArray)
        Return System.BitConverter.ToInt16(bArray, 0)
    End Function


    Private Function BToSingle(ByVal data() As Byte, ByVal index As Int16) As Single

        Try

            Dim iInputIndex As Integer = 0

            Dim iOutputIndex As Integer = 0

            Dim bArray(3) As Byte



            For iInputIndex = 0 To 3

                bArray(iOutputIndex) = data(index + iInputIndex)

                iOutputIndex += 1

            Next
            Array.Reverse(bArray)

            Return BitConverter.ToSingle(bArray, 0)

        Catch ex As Exception

        End Try
    End Function


    Private Function BToDouble(ByVal data() As Byte, ByVal index As Int16) As Double

        Try

            Dim iInputIndex As Integer = 0

            Dim iOutputIndex As Integer = 0

            Dim bArray(7) As Byte



            For iInputIndex = 0 To 7

                bArray(iOutputIndex) = data(index + iInputIndex)

                iOutputIndex += 1

            Next
            Array.Reverse(bArray)

            Return BitConverter.ToDouble(bArray, 0)

        Catch ex As Exception

        End Try
    End Function




    Private Function SetDot(ByVal S As String, ByVal dig As Integer) As String
        Dim oo As String
        If dig > 0 Then
            oo = S
            While Len(oo) < dig + 1
                oo = "0" + oo
            End While

            Return oo.Substring(0, oo.Length - dig) + "." + oo.Substring(oo.Length - dig, dig)
        Else
            Return S
        End If
    End Function

    Private MinArchDate As Date
    Private MaxArchDate As Date

    Private Function GetArchiveDates() As Boolean
        EraseInputQueue()
        MaxArchDate = Date.Today
        MinArchDate = Date.Today.AddDays(-365)

        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &H0
        Frame(1) = &H3
        Frame(2) = &H14
        Frame(3) = &H0
        Frame(4) = &H0
        Frame(5) = &H0  ' 0A ??
        ch = CheckSum(Frame, 0, 6)
        Frame(6) = ch Mod 256
        Frame(7) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 8)

        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer

        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                MyTransport.Read(b, ptr, cnt)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    SequenceErrorCount = 0

                    If (b(1) And &H80) = &H80 Then
                        ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                        Return False
                    End If

                    If b(2) = 20 Or b(2) = 30 Then
                        Try
                            MinArchDate = New DateTime(256 * b(3) + b(4), b(6), b(8), b(10), b(12), 0)
                            MaxArchDate = New DateTime(256 * b(13) + b(14), b(16), b(18), b(20), b(22), 0)
                            Return True
                        Catch ex As Exception
                           
                            Return False
                        End Try


                    Else
                        Return False
                    End If
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1
        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return False

    End Function





    
    Private Function GetDeviceDate() As Date
        EraseInputQueue()

        '00 03 0B 00 00 0A C6 38 
        Dim Frame(10) As Byte
        Dim ch As UInt16
        Frame(0) = &H0
        Frame(1) = &H3
        Frame(2) = &HB
        Frame(3) = &H0
        Frame(4) = &H0
        Frame(5) = &H0  ' 0A ??
        ch = CheckSum(Frame, 0, 6)
        Frame(6) = ch Mod 256
        Frame(7) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 8)

        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        Dim d As DateTime
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                MyTransport.Read(b, ptr, cnt)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    SequenceErrorCount = 0

                    If (b(1) And &H80) = &H80 Then
                        ErrorMessage = "Ошибка запроса :" + DecodeError(b(2))
                        Return Date.Now
                    End If

                    If b(2) = 10 Then
                        Try
                            d = New DateTime(256 * b(3) + b(4), b(6), b(8), b(10), b(12), 0)
                        Catch ex As Exception
                            d = Now
                        End Try

                        Return d
                    Else
                        Return Date.Now
                    End If
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While
        Else
            SequenceErrorCount += 1
        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return Date.Now

    End Function


    'Private Function SetArchType(ByVal Atype As VKT7ArchType) As Boolean
    '    IsError = False
    '    ErrorMessage = ""
    '    RaiseIdle()
    '    Thread.Sleep(60)

    '    Dim Frame(12) As Byte
    '    Dim ch As UInt16
    '    Frame(0) = &HFF
    '    Frame(1) = &HFF
    '    Frame(2) = &H0
    '    Frame(3) = &H10
    '    Frame(4) = &H3F
    '    Frame(5) = &HFD
    '    Frame(6) = &H0
    '    Frame(7) = &H0
    '    Frame(8) = &H2
    '    Frame(9) = Atype
    '    Frame(10) = &H0
    '    ch = CheckSum(Frame, 2, 9)
    '    Frame(11) = ch Mod 256
    '    Frame(12) = ch \ 256

    '    EraseInputQueue()
    '    MyTransport.Write(Frame, 0, 13)

    '    'Dim t As Integer
    '    'RaiseIdle()
    '    Thread.Sleep(200)
    '    't = 0
    '    'While MyTransport.BytesToRead = 0 And t < 20
    '    '    RaiseIdle()
    '    '    Thread.Sleep(CalcInterval(2))
    '    '    t = t + 1
    '    'End While

    '    WaitForData()

    '    Dim b(4096) As Byte
    '    Dim cnt As Integer
    '    cnt = MyTransport.BytesToRead
    '    If cnt > 0 Then
    '        Dim ptr As Integer
    '        Dim sz As Integer
    '        ptr = 0
    '        sz = 0
    '        While cnt > 0
    '            MyTransport.Read(b, ptr, cnt)
    '            ptr += cnt
    '            sz += cnt


    '            If VerifySumm(b, 0, sz) Then
    '                SequenceErrorCount = 0
    '                If (b(1) = &H83 Or b(1) = &H90) Then
    '                    IsError = True
    '                    ErrorMessage = "Ошибка запроса код:" & b(2)
    '                    Return False
    '                End If
    '                Return True
    '            End If

    '            RaiseIdle()
    '            Thread.Sleep(CalcInterval(2))
    '            cnt = MyTransport.BytesToRead
    '        End While
    '    Else
    '        SequenceErrorCount += 1

    '    End If
    '    IsError = True
    '    ErrorMessage = "Ошибка получения данных"
    '    Return False
    'End Function

    'Public Enum VKT7ArchType
    '    AT_Hour = 0 '-часовой архив;
    '    AT_Day = 1  '-суточный архив;
    '    AT_Month = 2 '-месячный архив;
    '    AT_Itog = 3 '-итоговый архив;
    '    AT_Current = 4 '-текущие значения;
    '    AT_CurItog = 5 '-итоговые текущие;
    '    AT_Properties = 6 '-свойства.
    'End Enum

    Private Function SetArchDate(ByVal Dat As Date) As Boolean
        IsError = False
        ErrorMessage = ""
        EraseInputQueue()
        Dim Frame(20) As Byte
        Dim ch As UInt16
        Frame(0) = &H0
        Frame(1) = &H10
        Frame(2) = &HB
        Frame(3) = &H0
        Frame(4) = &H0
        Frame(5) = &H4
        Frame(6) = &H8
        Frame(7) = Dat.Year \ 256
        Frame(8) = Dat.Year Mod 256
        Frame(9) = 0
        Frame(10) = Dat.Month
        Frame(11) = 0
        Frame(12) = Dat.Day
        Frame(13) = 0
        Frame(14) = Dat.Hour
        ch = CheckSum(Frame, 0, 15)
        Frame(15) = ch Mod 256
        Frame(16) = ch \ 256

        MyTransport.CleanPort()
        MyTransport.Write(Frame, 0, 17)


        WaitForData()

        Dim b(4096) As Byte
        Dim cnt As Integer
        cnt = MyTransport.BytesToRead
        If cnt > 0 Then
            Dim ptr As Integer
            Dim sz As Integer
            ptr = 0
            sz = 0
            While cnt > 0
                MyTransport.Read(b, ptr, cnt)
                ptr += cnt
                sz += cnt


                If VerifySumm(b, 0, sz) Then
                    SequenceErrorCount = 0
                    If (b(1) And &H80) = &H80 Then
                        IsError = True
                        ErrorMessage = "Ошибка запроса код:" & DecodeError(b(2))
                        Return False
                    End If
                    Return True
                End If

                RaiseIdle()
                Thread.Sleep(CalcInterval(2))
                cnt = MyTransport.BytesToRead
            End While

        Else
            SequenceErrorCount += 1
        End If
        IsError = True
        ErrorMessage = "Ошибка получения данных"
        Return False
    End Function




End Class
