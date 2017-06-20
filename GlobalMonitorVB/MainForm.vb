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
Imports VIPAnalizer

Public Class MainForm


    Private m_RouteStops As List(Of RouteStop)
    Private m_CursorPosition As LatLon

    Private Sub trackBar1_Scroll(sender As System.Object, e As System.EventArgs) Handles trackBar1.Scroll
        mapExplorer.ZoomLevel = CInt((67.0 / (-trackBar1.Minimum)) * (trackBar1.Value - trackBar1.Minimum))
        mapExplorer.Invalidate()
    End Sub

    Private Sub btnZoomIn_Click(sender As System.Object, e As System.EventArgs) Handles btnZoomIn.Click
        mapExplorer.ZoomLevel += 1
        Me.trackBar1.Value = CInt(mapExplorer.ZoomLevel * ((-trackBar1.Minimum) / mapExplorer.ZoomScales.Count()) + trackBar1.Minimum)
        Me.trackBar1.Invalidate()
        mapExplorer.Invalidate()
    End Sub

    Private Sub btnZoomOut_Click(sender As System.Object, e As System.EventArgs) Handles btnZoomOut.Click
        mapExplorer.ZoomLevel -= 1
        Me.trackBar1.Value = CInt(mapExplorer.ZoomLevel * ((-trackBar1.Minimum) / mapExplorer.ZoomScales.Count()) + trackBar1.Minimum)
        Me.trackBar1.Invalidate()
        mapExplorer.Invalidate()
    End Sub

    Private Sub mapExplorer_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles mapExplorer.MouseDoubleClick

        If Not mapExplorer.HighlightedCoordinate Is Nothing Then

            LastNode = mapExplorer.HighlightedCoordinate
            CallOpros()

        End If

    End Sub

    Private Sub mapExplorer_MouseWheel(sender As System.Object, e As System.EventArgs) Handles mapExplorer.MouseWheel
        Me.trackBar1.Value = CInt(mapExplorer.ZoomLevel * ((-trackBar1.Minimum) / mapExplorer.ZoomScales.Count()) + trackBar1.Minimum)
        Me.trackBar1.Invalidate()
        mapExplorer.Invalidate()
    End Sub

  
    Private Sub mapExplorer_Load(sender As System.Object, e As System.EventArgs) Handles mapExplorer.Load
        Dim fi As FileInfo = New FileInfo(Application.ExecutablePath)
        Dim dt As DataTable
        Dim i As Integer
        dt = TvMain.QuerySelect("select id_bd,cshort,fulladdress ,MAPX,MAPY,CFIO1,CPHONE1 ,CFIO2, CPHONE2  from bbuildings join bdevices on bdevices.id_bu=bbuildings.id_bu where MAPX<>0 and MAPY<>0 and hiderow=0 order by id_bd")
        
        For i = 0 To dt.Rows.Count - 1

            Try



                Dim lalo As LatLon = New LatLon(dt.Rows(i)("MAPY").ToString.Replace(",", ".") + "," + dt.Rows(i)("MAPX").ToString.Replace(",", "."))
                Dim rstop As RouteStop = New RouteStop(lalo)

                m_RouteStops.Add(rstop)
                rstop.Address = dt.Rows(i)("fulladdress")
                rstop.Contact = dt.Rows(i)("cfio1") + " " + dt.Rows(i)("cphone1")
                rstop.ID = dt.Rows(i)("id_bd")
                rstop.Text = dt.Rows(i)("cshort")
                rstop.Image = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\icong.png")
                rstop.HiliteImage = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\iconb.png")
                mapExplorer.renderCollection.Add(rstop)

            Catch
            End Try
        Next

        timer1.Enabled = True
    End Sub

    Private Sub mapExplorer_ViewChanged(sender As System.Object, e As OsmExplorer.Components.ViewChangedEventArgs) Handles mapExplorer.ViewChanged
        mapExplorer.Invalidate()
    End Sub

    Private Sub mapExplorer_MouseMoved(sender As System.Object, e As OsmExplorer.Components.MouseMoveEventArgs) Handles mapExplorer.MouseMoved
        m_CursorPosition = e.CoordinateLocation
        Me.txtCenterPoint.Text = m_CursorPosition.ToString()
        If (Not mapExplorer.HighlightedCoordinate Is Nothing) Then

            Dim rstop As RouteStop = mapExplorer.HighlightedCoordinate
            txtInfo.Text = rstop.Text + ", " + rstop.Address + " т. " + rstop.Contact


        Else

            txtInfo.Text = ""
        End If
    End Sub

    Private Sub mapExplorer_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles mapExplorer.MouseClick


        If Not mapExplorer.HighlightedCoordinate Is Nothing Then
            If Not LastNode Is mapExplorer.HighlightedCoordinate Then
                LastNode = mapExplorer.HighlightedCoordinate
                RefreshStatus()
                RefreshGraph()
                RefreshOpros()

            End If
        End If

        If e.Button = MouseButtons.Right Then
            If Not mapExplorer.HighlightedCoordinate Is Nothing Then
                CallStatus()
            End If
        End If


    End Sub

    Private Sub MainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        trackBar1.Value = CInt(mapExplorer.ZoomLevel * ((-trackBar1.Minimum) / 67) + trackBar1.Minimum)
        'rbOSM.Checked = True

        Me.trackBar1.Value = CInt(mapExplorer.ZoomLevel * ((-trackBar1.Minimum) / mapExplorer.ZoomScales.Count()) + trackBar1.Minimum)

        ShowNodes()
    End Sub

    Public Function checkInstance() As Process
        Dim cProcess As Process = process.GetCurrentProcess()
        Dim aProcesses() As Process = process.GetProcessesByName(cProcess.ProcessName)
        'loop through all the processes that are currently running on the
        'system that have the same name
        For Each process As Process In aProcesses
            'Ignore the currently running process
            If process.Id <> cProcess.Id Then
                'Check if the process is running using the same EXE as this one
                If System.Reflection.Assembly.GetExecutingAssembly().Location = cProcess.MainModule.FileName Then
                    'if so return to the calling function with the instance of the process
                    Return process
                End If
            End If
        Next
        'if nothing was found then this is the only instance, so return null
        Return Nothing
    End Function

    Public Sub New()


        Dim tempProcess As Process
        tempProcess = checkInstance()
        If Not tempProcess Is Nothing Then
            ShowWindowAsync(tempProcess.MainWindowHandle, ShowWindowConstants.SW_SHOWMINIMIZED)

            ShowWindowAsync(tempProcess.MainWindowHandle, ShowWindowConstants.SW_RESTORE)
            End
        End If
        TvMain = New STKTVMain.TVMain()
        If (TvMain.Init() = False) Then Application.Exit()

        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_RouteStops = New List(Of RouteStop)
    End Sub

    Private CheckLostCounter As Integer = 0
    Private inTimer As Boolean = False
    Private DTidx As Integer = -1
    Private XDT As DataTable
    Private Sub timer1_Tick(sender As System.Object, e As System.EventArgs) Handles timer1.Tick
        If inTimer Then Exit Sub
        inTimer = True

        'Dim an As LostAnalizer
        'Dim i As Integer

        'If DTidx = -1 Then
        '    XDT = TvMain.GetTable("v_dev2_all")
        '    DTidx = 0
        'End If


        'For i = DTidx To XDT.Rows.Count - 1
        '    If i < DTidx + 3 Then
        '        an = New LostAnalizer
        '        an.tvmain = TvMain
        '        an.id_bd = XDT.Rows(i)("id_bd")
        '        an.NodeName = XDT.Rows(i)("NodeName")
        '        Application.DoEvents()
        '        an.AnalizeStatus(2)
        '        Application.DoEvents()
        '        an.AnalizeLost()
        '        Application.DoEvents()
        '        an = Nothing
        '    Else
        '        Exit For
        '    End If

        'Next
        'If DTidx + 3 > XDT.Rows.Count Then
        '    DTidx = -1
        'Else
        '    DTidx += 3
        'End If

        ShowNodes()

     
        inTimer = False



    End Sub



    Private Sub ShowNodes()
        Dim dt As DataTable
        dt = TvMain.QuerySelect("select max(status) s_max ,min(status) s_min from ANALIZER where not status is null and status >0")
        Dim s_max As Integer = 0
        Dim s_min As Integer = 0
        Dim s_d As Integer = 1

        If dt.Rows.Count > 0 Then
            Try
                s_max = dt.Rows(0)("s_max")
            Catch ex As Exception

            End Try
            Try
                s_min = dt.Rows(0)("s_min")
            Catch ex As Exception

            End Try

            s_d = (s_max - s_min) / 26
            If s_d < 1 Then s_d = 1
        Else
            s_min = 0
            s_max = 25
        End If

        dt = TvMain.QuerySelect("select id_bd,status,color from ANALIZER where not status is null")

        Dim j As Integer

        Dim st As Integer
        Dim id_bd As Integer
        Dim ncolor As String
        Dim rstop As RouteStop
        Dim r As Integer

        Dim fi As FileInfo = New FileInfo(Application.ExecutablePath)
        For i = 0 To dt.Rows.Count - 1
            st = dt.Rows(i)("status")
            id_bd = dt.Rows(i)("id_bd")
            ncolor = dt.Rows(i)("color") & ""
            If ncolor = "" Then ncolor = "g"
            If ncolor = "YELLOW" Then ncolor = "y"
            If ncolor = "RED" Then ncolor = "r"
            Dim dt1 As DataTable

            dt1 = TvMain.QuerySelect("select bdevices.id_bd, npquery,cstatus from bdevices join  plancall on bdevices.id_bd=plancall.id_bd where bdevices.id_bd=" & id_bd.ToString)
            If dt1.Rows.Count > 0 Then
                If dt1.Rows(0)("cstatus") = 1 And dt1.Rows(0)("npquery") = 0 Then
                    ncolor = "g"
                End If
            End If

            For j = 0 To m_RouteStops.Count - 1
                rstop = m_RouteStops.Item(j)
                If rstop.ID = id_bd Then
                    If st < s_min + s_d Then
                        Try
                            rstop.Image = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\icon" + ncolor + "1.png")
                        Catch ex As Exception

                        End Try

                        rstop.HiliteImage = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\iconb.png")
                        rstop.Status = st
                    Else
                        r = (st - s_min) / s_d
                        If r < 1 Then r = 1
                        If r > 25 Then r = 25
                        'If r <= 5 Then
                        '    rstop.Image = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\icon" + ncolor + r.ToString + ".png")
                        '    rstop.HiliteImage = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\iconb" + r.ToString + ".png")
                        'ElseIf r <= 12 Then
                        '    rstop.Image = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\icon" + ncolor + r.ToString + ".png")
                        '    rstop.HiliteImage = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\iconb" + r.ToString + ".png")
                        'Else
                        rstop.Image = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\icon" + ncolor + r.ToString + ".png")
                        rstop.HiliteImage = System.Drawing.Image.FromFile(fi.DirectoryName + "\icons\iconb" + r.ToString + ".png")
                        'End If

                        rstop.Status = st
                    End If
                Exit For
                End If
            Next
            Application.DoEvents()
        Next
        mapExplorer.Invalidate()
    End Sub
   

    Private Sub СтатусToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles СтатусToolStripMenuItem.Click
        CallStatus()
    End Sub

 
    Private Sub mnuGraph_Click(sender As System.Object, e As System.EventArgs) Handles mnuGraph.Click
        CallGraph()
    End Sub



    Private Sub ОпросToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ОпросToolStripMenuItem.Click
        CallOpros()

    End Sub

    Private Sub mnuNodesetup_Click(sender As System.Object, e As System.EventArgs) Handles mnuNodesetup.Click
        CallSetup()
    End Sub

    Private Sub РасчетКоординатToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles РасчетКоординатToolStripMenuItem.Click
        Dim dt As DataTable
        Dim i As Integer
        dt = TvMain.QuerySelect("select * from bbuildings")
        Dim s As String
        s = Me.Text
        Dim ra As String
        Dim rr() As String
        Dim res As Resolver
        res = New Resolver
        For i = 0 To dt.Rows.Count - 1
            Try
                ra = res.ResolveAddresses(dt.Rows(i)("FULLADDRESS"))
                If ra <> "" Then
                    rr = ra.Split(";")
                    TvMain.QueryExec("update bbuildings set mapx=" + rr(0) + ", MAPY=" + rr(1) + " where id_bu=" + dt.Rows(i)("ID_BU").ToString)
                    Me.Text = dt.Rows(i)("FULLADDRESS") + " --> " + rr(0) + ", " + rr(1)
                    Application.DoEvents()
                End If
            Catch ex As Exception

            End Try
            
        Next

        Me.Text = s



    End Sub
End Class