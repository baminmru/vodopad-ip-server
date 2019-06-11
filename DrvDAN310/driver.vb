
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

'    Public Q1 As Single
'    Public Q2 As Single
'    Public Q3 As Single
'    Public Q4 As Single
'    Public Q5 As Single
'    Public Q6 As Single

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

'    Public v1 As Single
'    Public v2 As Single
'    Public v3 As Single
'    Public v4 As Single
'    Public v5 As Single
'    Public v6 As Single
'    Public vh1 As Single
'    Public vh2 As Single

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

    

    Private mIsConnected As Boolean

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

    Dim AppVersion As String

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


    Public Overrides Function CounterName() As String
        Return "DANFOSS210_310"
    End Function

    Private Function DecodeError(e As Byte) As String
       
                Return "Ошибка прибора"


    End Function






   


    Private Function DevInit() As Boolean

        'AppVersion = DAN_GetApplication()
        'If AppVersion <> "" Then
        Return True
        'End If
        'Return False
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
                isArchToDBWrite = False
                Return "Устройство не содержит архивов"

            End If


            If ArchType = archType_day Then
                    isArchToDBWrite = False
                Return "Устройство не содержит архивов"
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



    Private Function FindValue(ByVal dt As DataTable, ByVal name As String) As Double
        Dim i As Integer
        Dim v As Double = 0.0
        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("Название") = name Then
                v = dt.Rows(i)("Значение")
                Return v
            End If
        Next
        Return Double.NaN

    End Function


    Public Overrides Function ReadMArch() As String
        clearMarchive(mArch)


        Dim AErr As String = ""

        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow

     


       

        Dim retsum As String
        'retsum = "Мгновенный архив прочитан"
        'If AErr = "" Then
        '    'retsum = "Мгновенный архив прочитан"
        '    retsum = retsum & vbCrLf
        '    EraseInputQueue()
        '    isMArchToDBWrite = True
        '    Return retsum
        'Else
        retsum = "Чтение мгновенного архива не реализовано"
        retsum = retsum & AErr & vbCrLf
        EraseInputQueue()
        isMArchToDBWrite = False
        Return retsum
        'End If

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
        Dim Frame(10) As Byte
        Dim b(4096) As Byte
        Dim AErr As String = ""




        tArch.DateArch = GetDeviceDate()

        If SequenceErrorCount > 5 Then GoTo ArchErr




        Dim retsum As String
        retsum = "Итоговый архив прочитан"

        AErr = "Устройство не содержит архивов"
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



    
    Private Function GetDeviceDate() As Date
        EraseInputQueue()
        Return Date.Now
    End Function


   

    


End Class
