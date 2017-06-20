Option Strict Off
Option Explicit On
Friend Class Form1
    Inherits System.Windows.Forms.Form
    Public DEVID As Short
    Public Sb, Br, Db, Pt As Object
    Public Fc As Short
    Public RBUF As String
    Public Bcnt As Short
    Public ArchType_hour As Short = 3
    Public ArchType_day As Short = 4



    Private TvMain As STKTVMain.TVMain

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        ComboBoxArchType.Items.Add("Часовой")
        ComboBoxArchType.Items.Add("Суточный")
        TvMain = New STKTVMain.TVMain
        Dim ret As Boolean
        ret = TvMain.Init()
        If ret = False Then
            Application.Exit()
        End If
        'MsgBox(TvMain.CounterName)
        If TvMain.DeviceInit(DEVID) Then
            Me.Text = "Тест соединения с устройством ID=" + DEVID.ToString()

            TextBoxArchYear.Text = Date.Today.Year.ToString()
            TextBoxArchMonth.Text = Date.Today.Month.ToString()
            TextBoxAcrhDay.Text = Date.Today.Day.ToString()

            TvMain.connect()
            If TvMain.isConnected Then
                Text3.Text = TvMain.ConnectStatus
                Text3.Text += vbCrLf & "Соединение установлено"
                Timer1.Start()
            Else
                Text3.Text = TvMain.ConnectStatus
            End If
        Else
            Text3.Text = TvMain.ConnectStatus
        End If




    End Sub

    Private Sub Form1_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        TvMain.CloseTransportConnect()
        TvMain.DeviceClose()
        TvMain.CloseDBConnection()
    End Sub

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
        Static retstr As String
        retstr = TvMain.bufcheck()
        If (retstr <> "") Then
            Text3.Text = Text3.Text + retstr
        End If
        If AutoStarted Then
            AutoStartCount -= 1
            If AutoStartCount = 0 Then
                RunAuto()
                If Not Integer.TryParse(txtInterval.Text, AutoStartCount) Then
                    AutoStartCount = 30
                End If
            End If
        End If

    End Sub

    Private Sub ButtonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonConnect.Click
        Try

            TvMain.connect()
        Catch exc As Exception
        End Try
        If TvMain.isConnected Then
            Text3.Text = Text3.Text + vbCrLf + "Соединение установлено"
        Else
            Text3.Text = Text3.Text + vbCrLf + "Не удалось установить соединение"
        End If
    End Sub



    Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        Text3.Text = ""
    End Sub

    Private Sub ComboBoxToShow_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ButtonReadArch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReadArch.Click
       


        If TvMain.isConnected Then
            Try
                Dim archtype As Short


                If (ComboBoxArchType.Text = "") Then
                    MsgBox("Задайте тип архива")
                    Exit Sub
                End If
                If (ComboBoxArchType.Text = "Часовой") Then
                    archtype = ArchType_hour
                End If

                If (ComboBoxArchType.Text = "Суточный") Then
                    archtype = ArchType_day
                End If


                Text3.Text = Text3.Text + TvMain.readarch(archtype, Int(TextBoxArchYear.Text), Int(TextBoxArchMonth.Text), _
                Int(TextBoxAcrhDay.Text), Int(TextBoxAcrhHour.Text))
                Text3.Text = Text3.Text & vbCrLf
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try
        Else
            Text3.Text += vbCrLf + "Соединение с устройством не установлено"
        End If
    End Sub

  

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not TvMain.TVD Is Nothing Then
            'TvMain.buffersClear()
        Else
            Text3.Text += vbCrLf + "Драйвер не инициализирован"
        End If

    End Sub

   


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If TvMain.isConnected Then
            Try
                Text3.Text = Text3.Text + TvMain.readmarch()
                Text3.Text = Text3.Text & vbCrLf
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try
        Else
            Text3.Text += vbCrLf + "Соединение с устройством не установлено"
        End If
       
    End Sub

    
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        If TvMain.isConnected Then
            Try
                Dim archtype As Short
                archtype = ArchType_hour
                Dim i As Integer
                For i = 0 To 23
                    Text3.Text = Text3.Text & vbCrLf & i.ToString & _
                    TvMain.readarch(archtype, Int(TextBoxArchYear.Text), Int(TextBoxArchMonth.Text), _
                               Int(TextBoxAcrhDay.Text), i)
                    ' tt = Microsoft.VisualBasic.Timer()
                    'While tt + 8 < Microsoft.VisualBasic.Timer()
                    '    Application.DoEvents()
                    'End While
                    Application.DoEvents()

                Next
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try
        Else
            Text3.Text += vbCrLf + "Соединение с устройством не установлено"
        End If


    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


        If TvMain.isConnected Then
            Try
                Dim archtype As Short
                archtype = ArchType_day
                Dim i As Integer
                For i = 1 To 31
                    Text3.Text = Text3.Text & vbCrLf & i.ToString & _
                    TvMain.readarch(archtype, Int(TextBoxArchYear.Text), Int(TextBoxArchMonth.Text), _
                               i, 0)
                    'tt = Microsoft.VisualBasic.Timer()
                    'While tt + 8 < Microsoft.VisualBasic.Timer()
                    '    Application.DoEvents()
                    'End While
                    Application.DoEvents()

                Next
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try
        Else
            Text3.Text += vbCrLf + "Соединение с устройством не установлено"
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
      
        If TvMain.isConnected Then
            Try
                Text3.Text = Text3.Text + TvMain.readtarch()
                Text3.Text = Text3.Text + TvMain.TVD.WriteTArchToDB()
                Text3.Text = Text3.Text & vbCrLf
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try
        Else
            Text3.Text += vbCrLf + "Соединение с устройством не установлено"
        End If
    End Sub

  

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub cmdOpenPort_Click(sender As System.Object, e As System.EventArgs) Handles cmdOpenPort.Click
        If Not TvMain.TVD Is Nothing Then
            If TvMain.TVD.OpenPort() Then
                Text3.Text += vbCrLf + "транспорт инициализирован"
            Else
                Text3.Text += vbCrLf + "ошибка открытия транспортного уровня"
            End If
        Else
            Text3.Text += vbCrLf + "Драйвер не инициализирован"
        End If
    End Sub

    Private Sub cmdSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSystem.Click
        If TvMain.isConnected Then
            Try
                Dim dt As DataTable
                Dim i As Integer
                dt = TvMain.ReadSystemParameters

                For i = 0 To dt.Rows.Count - 1
                    Text3.Text = Text3.Text & dt.Rows(i)(0) & ":" & dt.Rows(i)(1) & vbCrLf
                Next

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly)
            End Try
        Else
            Text3.Text += vbCrLf + "Соединение с устройством не установлено"
        End If
    End Sub
    Private AutoStarted As Boolean = False
    Private AutoStartCount As Integer = 30

    Private Sub cmdStart_Click(sender As System.Object, e As System.EventArgs) Handles cmdStart.Click
        If chkMoment.Checked Or chkSystem.Checked Or chkTotal.Checked Then
            If Not Integer.TryParse(txtInterval.Text, AutoStartCount) Then
                AutoStartCount = 30
            End If
            AutoStarted = True
            RunAuto()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub cmdStop_Click(sender As System.Object, e As System.EventArgs) Handles cmdStop.Click
        AutoStarted = False
    End Sub
    Private Sub RunAuto()
        If chkMoment.Checked Then
            Button5_Click(Me, Nothing)
        End If
        If chkSystem.Checked Then
            cmdSystem_Click(Me, Nothing)
        End If
        If chkTotal.Checked Then
            Button9_Click(Me, Nothing)
        End If
    End Sub
End Class