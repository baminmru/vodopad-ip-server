Imports System.Diagnostics
Imports System.Text
Public Module modMain
    Private mobNotifyIcon As NotifyIcon
    Private WithEvents mobContextMenu As ContextMenu
    Private WithEvents mobTimer As Timers.Timer
    Private mobServiceController As System.ServiceProcess.ServiceController

    Public Sub Main()
        Try
            mobServiceController = New System.ServiceProcess.ServiceController("AREALMODBUSService")
            mobNotifyIcon = New NotifyIcon()
            mobNotifyIcon.Visible = False
            mobContextMenu = New ContextMenu()
            CreateMenu()
            mobNotifyIcon.ContextMenu = mobContextMenu
            SetUpTimer()
            mobNotifyIcon.Visible = True
            Application.Run()
        Catch obEx As Exception
            MsgBox(obEx.Message.ToString, MsgBoxStyle.Critical)
            End
        End Try
    End Sub

    Private Sub SetUpTimer()
        Try
            mobTimer = New Timers.Timer()
            With mobTimer
                .AutoReset = True
                .Interval = 1500
                .Start()
            End With
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub CreateMenu()
        Try
            mobContextMenu.MenuItems.Add(New MenuItem("Stop", New EventHandler(AddressOf StopService)))
            mobContextMenu.MenuItems.Add(New MenuItem("Pause", New EventHandler(AddressOf PauseService)))
            mobContextMenu.MenuItems.Add(New MenuItem("Continue", New EventHandler(AddressOf ContinueService)))
            mobContextMenu.MenuItems.Add(New MenuItem("Start", New EventHandler(AddressOf StartService)))
            mobContextMenu.MenuItems.Add("-")
            mobContextMenu.MenuItems.Add(New MenuItem("About", New EventHandler(AddressOf AboutBox)))
            mobContextMenu.MenuItems.Add(New MenuItem("Exit", New EventHandler(AddressOf ExitController)))
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub GetServiceStatus()
        Try
            mobServiceController.Refresh()
            Select Case mobServiceController.Status()
                Case ServiceProcess.ServiceControllerStatus.Paused
                    mobNotifyIcon.Icon = New Icon("PausedM.ico")
                    mobContextMenu.MenuItems(0).Enabled = False
                    mobContextMenu.MenuItems(1).Enabled = False
                    mobContextMenu.MenuItems(2).Enabled = True
                    mobContextMenu.MenuItems(3).Enabled = False
                Case ServiceProcess.ServiceControllerStatus.Running
                    mobNotifyIcon.Icon = New Icon("RunningM.ico")
                    mobContextMenu.MenuItems(0).Enabled = True
                    mobContextMenu.MenuItems(1).Enabled = True
                    mobContextMenu.MenuItems(2).Enabled = False
                    mobContextMenu.MenuItems(3).Enabled = False
                Case ServiceProcess.ServiceControllerStatus.Stopped
                    mobNotifyIcon.Icon = New Icon("StoppedM.ico")
                    mobContextMenu.MenuItems(0).Enabled = False
                    mobContextMenu.MenuItems(1).Enabled = False
                    mobContextMenu.MenuItems(2).Enabled = False
                    mobContextMenu.MenuItems(3).Enabled = True
                Case ServiceProcess.ServiceControllerStatus.ContinuePending, _
                        ServiceProcess.ServiceControllerStatus.PausePending, _
                        ServiceProcess.ServiceControllerStatus.StartPending, _
                        ServiceProcess.ServiceControllerStatus.StopPending
                    mobNotifyIcon.Icon = New Icon("PausedM.ico")
                    mobContextMenu.MenuItems(0).Enabled = False
                    mobContextMenu.MenuItems(1).Enabled = False
                    mobContextMenu.MenuItems(2).Enabled = False
                    mobContextMenu.MenuItems(3).Enabled = False
            End Select
            If mobServiceController.CanPauseAndContinue = False Then
                mobContextMenu.MenuItems(1).Enabled = False
                mobContextMenu.MenuItems(2).Enabled = False
            End If

        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub StopService(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mobServiceController.Status = ServiceProcess.ServiceControllerStatus.Running Then
                If mobServiceController.CanStop = True Then
                    mobServiceController.Stop()
                End If
            End If
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub PauseService(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not mobServiceController.Status = ServiceProcess.ServiceControllerStatus.Paused = True Then
                If mobServiceController.CanPauseAndContinue = True Then
                    mobServiceController.Pause()
                End If
            End If
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub ContinueService(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mobServiceController.Status = ServiceProcess.ServiceControllerStatus.Paused = True Then
                If mobServiceController.CanPauseAndContinue = True Then
                    mobServiceController.[Continue]()
                End If
            End If
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub StartService(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If mobServiceController.Status = ServiceProcess.ServiceControllerStatus.Stopped Then
                mobServiceController.Start()
            End If
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub AboutBox(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim obStringBuilder As New StringBuilder()
            With obStringBuilder
                .Append("STKServiceModem Controller v.1.0")
                '.Append(vbCrLf)
                '.Append("CLR Version: ")
                '.Append(Environment.Version.ToString)
                MsgBox(.ToString, MsgBoxStyle.Information)
            End With
            obStringBuilder = Nothing
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

    Private Sub ExitController(ByVal sender As Object, ByVal e As EventArgs)
        Try
            mobTimer.Stop()
            mobTimer.Dispose()
            mobNotifyIcon.Visible = False
            mobNotifyIcon.Dispose()
            mobServiceController.Dispose()
            Application.Exit()
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub


    Public Sub mobTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles mobTimer.Elapsed
        Try
            GetServiceStatus()
        Catch obEx As Exception
            Throw obEx
        End Try
    End Sub

End Module
