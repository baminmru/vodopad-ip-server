Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports OsmExplorer
Imports OsmExplorer.Components
Imports OsmExplorer.Data
Imports OsmExplorer.Exceptions
Imports OsmExplorer.Routing
Imports OsmExplorer.Spatial
Imports System.IO
Imports System.Text

Imports System.Runtime.InteropServices


Module MyGlobals
    Public MyfrmManual As ClientForm
    'Public MyfrmNC As frmNC
    Public MyfrmGraph As frmGraph
    ' Public MyfrmSchema As frmSchema
    Public LastNode As RouteStop
    Public MyfrmStatus As frmStatus
    Public MyfrmSetup As Form
    Dim ne As NodeEditorLib.NodeEditor
    Public Enum ShowWindowConstants

        SW_HIDE = 0

        SW_SHOWNORMAL = 1

        SW_NORMAL = 1

        SW_SHOWMINIMIZED = 2

        SW_SHOWMAXIMIZED = 3

        SW_MAXIMIZE = 3

        SW_SHOWNOACTIVATE = 4

        SW_SHOW = 5

        SW_MINIMIZE = 6

        SW_SHOWMINNOACTIVE = 7

        SW_SHOWNA = 8

        SW_RESTORE = 9

        SW_SHOWDEFAULT = 10

        SW_FORCEMINIMIZE = 11

        SW_MAX = 11

    End Enum

    <DllImport("User32.dll")> _
    Public Function ShowWindowAsync(ByVal hWnd As IntPtr, ByVal swCommand As Integer) As Integer

    End Function

    Public Sub CallOpros()
        If MyfrmManual Is Nothing Then
            MyfrmManual = New ClientForm

        End If
        MyfrmManual.Show()
        Try
            If Not LastNode Is Nothing Then
                MyfrmManual.id = LastNode.ID
                MyfrmManual.NodeName = LastNode.Text
                MyfrmManual.RefreshData()
            End If

        Catch
        End Try

        MyfrmManual.Select()
    End Sub


    Public Sub RefreshOpros()
        If Not MyfrmManual Is Nothing Then
            If MyfrmManual.Visible Then
                Try
                    If Not LastNode Is Nothing Then
                        MyfrmManual.id = LastNode.ID
                        MyfrmManual.NodeName = LastNode.Text
                        MyfrmManual.RefreshData()
                    End If

                Catch
                End Try

                MyfrmManual.Select()
            End If
        End If
    End Sub

    Public Sub CallStatus()
        If MyfrmStatus Is Nothing Then
            MyfrmStatus = New frmStatus

        End If

        MyfrmStatus.Show()
        Try
            If Not LastNode Is Nothing Then
                MyfrmStatus.rstop = LastNode
                MyfrmStatus.LoadData()
            End If
        Catch
        End Try
        MyfrmStatus.Select()
    End Sub

    Public Sub CallGraph()
        If MyfrmGraph Is Nothing Then
            MyfrmGraph = New frmGraph

        End If

        MyfrmGraph.Show()
        Try
            If Not LastNode Is Nothing Then

                MyfrmGraph.LoadData(LastNode.ID, LastNode.Text)
            End If

        Catch
        End Try
        MyfrmGraph.Select()
    End Sub


    Public Sub RefreshGraph()
        If Not MyfrmGraph Is Nothing Then
            If MyfrmGraph.Visible Then

                MyfrmGraph.Show()
                Try
                    If Not LastNode Is Nothing Then

                        MyfrmGraph.LoadData(LastNode.ID, LastNode.Text)
                    End If

                Catch
                End Try
                MyfrmGraph.Select()
            End If
        End If
    End Sub
    Public Sub RefreshStatus()
        If Not MyfrmStatus Is Nothing Then
            If MyfrmStatus.Visible Then
                Try
                    If Not LastNode Is Nothing Then
                        MyfrmStatus.rstop = LastNode
                        MyfrmStatus.LoadData()
                    End If
                Catch
                End Try
                MyfrmStatus.Select()
            End If
        End If
    End Sub

    Public Sub CallSetup()
        If ne Is Nothing Then
            ne = New NodeEditorLib.NodeEditor
        End If
        MyfrmSetup = ne.GetForm(LastNode.ID, TvMain)
        MyfrmSetup.ShowDialog()


    End Sub



End Module
