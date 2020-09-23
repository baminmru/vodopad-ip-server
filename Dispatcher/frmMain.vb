Imports System.Windows.Forms
Imports System.Array
Imports VIPAnalizer
Imports Infragistics.Win.UltraWinListView

Public Class frmMain
    'Public WithEvents tree As Infragistics.Win.UltraWinTree.UltraTree
    'Public fTree As frmTree


    


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    


   

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim f As SplashScreen1
        f = New SplashScreen1
        f.Show()

    End Sub

    Private Sub mnuManualQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManualQuery.Click
        If MyfrmManual Is Nothing Then
            MyfrmManual = New ClientForm
            MyfrmManual.MdiParent = Me
        End If
        MyfrmManual.Show()
        MyfrmManual.RefreshData(tree)
        MyfrmManual.Select()
        SizeNodes()


    End Sub

    Private Sub mnuNC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNC.Click
        If MyfrmNC Is Nothing Then
            MyfrmNC = New frmNC
            MyfrmNC.MdiParent = Me
        End If

        MyfrmNC.Show()
        Try
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then

                MyfrmNC.LoadData(CInt(tree.SelectedNodes.Item(0).Tag))
            End If
        Catch
        End Try
        MyfrmNC.Select()
        SizeNodes()
        
    End Sub

    Private Sub nmuGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nmuGraph.Click
        If MyfrmGraph Is Nothing Then
            MyfrmGraph = New frmGraph
            MyfrmGraph.MdiParent = Me
        End If

        MyfrmGraph.Show()
        Try
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then

                MyfrmGraph.LoadData(CInt(tree.SelectedNodes.Item(0).Tag))
            End If
        Catch
        End Try
        MyfrmGraph.Select()

        SizeNodes()
    End Sub



    ' bami
    Public Sub ListBoxInit()

        ListView1.View = View.SmallIcon
        ListView1.Items.Clear()

        Dim dt As DataTable
        Dim i As Integer
        Dim GrpPostFix As String

        If TvMain.ShowHidden Then
            GrpPostFix = "_ALL"
        Else
            GrpPostFix = ""
        End If

        dt = TvMain.GetTable("v_grp2" & GrpPostFix, " order by GroupName")


        If Not dt Is Nothing Then
            LoadLevelData("", TvMain.GetTable("v_grp2" & GrpPostFix, " order by GroupName"), tree)
            For i = 0 To dt.Rows.Count - 1
                Dim q As String
                q = "select * from v_dev2" & GrpPostFix & " where "
                If txtFilter.Text <> "" Then
                    q = q & "(nodename || ';' || cdevname || ';' || cdevdesc) like'%" & txtFilter.Text & "%' and "
                End If
                q = q & "ID_GRP=" + dt.Rows.Item(i)("ID_GRP").ToString + " order by NodeName"

                LoadLevelData(dt.Rows.Item(i)("ID_GRP").ToString, TvMain.QuerySelect(q), tree)
            Next
        End If

        tree.HideSelection = False
        SizeNodes()
        ColorizeNodes()

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        TvMain.CloseDBConnection()
    End Sub

    Private Sub SizeNodes()
        'Dim dcp As Infragistics.Win.UltraWinDock.DockableControlPane = udm.PaneFromControl(Узлы)

        'dcp.Maximized = True



        'dcp.FlyoutSize = New Size(dcp.FlyoutSize.Width, Me.Height)


        'With udm.DockAreas("doc_left")
        '    If .DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked Then
        '        If .Size.Width < 100 Then
        '            .Size = New Size(100, .Size.Height)
        '        End If
        '    End If
        'End With
        'With udm.DockAreas("doc_top")
        '    If .DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked Then
        '        If .Size.Height < 100 Then
        '            .Size = New Size(.Size.Width, 100)
        '        End If

        '    End If


        'End With

        'With udm.DockAreas("doc_right")
        '    If .DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked Then
        '        If .Size.Width < 100 Then
        '            .Size = New Size(100, .Size.Height)
        '        End If

        '    End If
        'End With

        'With udm.DockAreas("doc_bottom")
        '    If .DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked Then
        '        If .Size.Height < 100 Then
        '            .Size = New Size(.Size.Width, 100)
        '        End If

        '    End If


        'End With


    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TvMain = New STKTVMain.TVMain
        If TvMain.Init() = False Then
            Application.Exit()
            End
        End If

        HideWhite = (GetSetting("VIP", "View", "HideWhite", "false") = "true")
        If HideWhite Then
            mnuHideWhite.Checked = True
        Else
            mnuHideWhite.Checked = False
        End If

        If GetSetting("VIP", "View", "NodePosition", "left") = "left" Then
            ListView1.Visible = False
            Узлы.Dock = DockStyle.Left
            Узлы.Width = 200
            Узлы.Visible = True
        Else
            Узлы.Dock = DockStyle.Top
            Узлы.Height = 200
            ListView1.Visible = True
            Узлы.Visible = True
        End If
        


        ListBoxInit()
        Me.Text = "ВОДОПАД-IP. Диспетчер." & " Источник данных: " + TvMain.DataSourceName
        'Me.Text = Me.Text & " Источник данных: " + TvMain.DataSourceName
        applicationStartTime = DateTime.Now.AddMinutes(-2)


        Dim mnu() As String
        Dim i As Integer
        mnu = TvMain.AutoOpen.Split(";")
        For i = LBound(mnu) To UBound(mnu)
            If mnu(i).ToLower = "s" Then mnuSchema_Click(sender, e)
            If mnu(i).ToLower = "n" Then mnuNC_Click(sender, e)
            If mnu(i).ToLower = "g" Then nmuGraph_Click(sender, e)

        Next

        For i = LBound(mnu) To UBound(mnu)
            If mnu(i).ToLower = "d" Then mnuManualQuery_Click(sender, e)
        Next





        Timer1.Enabled = True
    End Sub

    Private Sub chkShoHidden_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShoHidden.CheckedChanged
        TvMain.ShowHidden = chkShoHidden.Checked
        ListBoxInit()
    End Sub

    Dim ne As NodeEditorLib.NodeEditor
    Private Sub ButtonConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonConfig.Click

        Dim tag As Object

        If tree.SelectedNodes.Count > 0 Then
            tag = tree.SelectedNodes.Item(0).Tag
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                If ne Is Nothing Then
                    ne = New NodeEditorLib.NodeEditor
                End If
                Dim f As Form
                f = ne.GetForm(tag, TvMain)
                'f.ID = tag.ToString()
                'f.TvMain = TvMain
                'f.BName = tree.SelectedNodes.Item(0).Text
                If (f.ShowDialog() = DialogResult.OK) Then
                    Me.Focus()
                End If
                f = Nothing
            Else
                Dim f As frmSetupGroup
                f = New frmSetupGroup
                f.ID = tag.ToString()
                f.TvMain = TvMain
                If (f.ShowDialog() = DialogResult.OK) Then
                    Me.Focus()
                End If
                f = Nothing

            End If
        Else
            Exit Sub

        End If




    End Sub

    Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
        Dim f As Form1
        Dim tag As Object

        If tree.SelectedNodes.Count > 0 Then
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                tag = tree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If
        f = New Form1
        f.DEVID = tag.ToString
        f.ShowDialog()


    End Sub

    Private InAfterSelect As Boolean
    Private Sub tree_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles tree.AfterSelect
        If tree.SelectedNodes.Count = 0 Then Exit Sub

        If InAfterSelect Then Exit Sub
        InAfterSelect = True

        If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
            SetNodeTip(tree.SelectedNodes.Item(0))
        Else
            Dim GrpPostFix As String

            If TvMain.ShowHidden Then
                GrpPostFix = "_ALL"
            Else
                GrpPostFix = ""
            End If

            Dim q As String
            q = "select * from v_dev2" & GrpPostFix & " where "
            If txtFilter.Text <> "" Then
                q = q & "(nodename || ';' || cdevname || ';' || cdevdesc) like'%" & txtFilter.Text & "%' and "
            End If
            q = q & "ID_GRP=" + tree.SelectedNodes.Item(0).Tag + " order by NodeName"


            LoadLV(TvMain.QuerySelect(q), ListView1)
        End If
        If Not MyfrmManual Is Nothing Then
            If MyfrmManual.Visible = True Then
                If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                    MyfrmManual.RefreshData(tree)
                End If
            Else
                MyfrmManual = Nothing
            End If

        End If

        If Not MyfrmGraph Is Nothing Then
            If MyfrmGraph.Visible = True Then
                Try
                    If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                        MyfrmGraph.LoadData(CInt(tree.SelectedNodes.Item(0).Tag))
                    End If
                Catch
                End Try
            Else

                MyfrmGraph = Nothing
            End If

        End If


        If Not MyfrmNC Is Nothing Then
            If MyfrmNC.Visible = True Then
                Try
                    If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then

                        MyfrmNC.LoadData(CInt(tree.SelectedNodes.Item(0).Tag), tree.SelectedNodes.Item(0).Text)
                    End If
                Catch
                End Try
            Else

                MyfrmNC = Nothing
            End If

        End If


        If Not MyfrmSchema Is Nothing Then
            If MyfrmSchema.Visible = True Then
                Try
                    If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then

                        MyfrmSchema.LoadData(CInt(tree.SelectedNodes.Item(0).Tag))
                    End If
                Catch
                End Try
            Else

                MyfrmSchema = Nothing
            End If

        End If

        InAfterSelect = False
    End Sub

    Private Sub mnuSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSchema.Click
        If MyfrmSchema Is Nothing Then
            MyfrmSchema = New frmSchema
            MyfrmSchema.MdiParent = Me
        End If

        MyfrmSchema.Show()
        Try
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then

                MyfrmSchema.LoadData(CInt(tree.SelectedNodes.Item(0).Tag))
            End If
        Catch
        End Try
        MyfrmSchema.Select()

    End Sub

    Private Sub MenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub

    Private Sub mnuRefreshLost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefreshLost.Click
        Dim dt As DataTable
        'Dim an As LostAnalizer
        dt = TvMain.GetTable("v_dev2_all")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            Dim an As NodeAnalizer = New NodeAnalizer

            an.AnalizeNode(TvMain, dt.Rows(i)("id_bd"), 5, True)
            Me.Text = dt.Rows(i)("NodeName") + " " + i.ToString + " из " + dt.Rows.Count.ToString
            Application.DoEvents()
        Next
        Me.Text = "ВОДОПАД-IP. Диспетчер." & " Источник данных: " + TvMain.DataSourceName
    End Sub


    Private Sub mnuSetupModems_Click(sender As System.Object, e As System.EventArgs) Handles mnuSetupModems.Click
        Dim f As frmSetupModem
        f = New frmSetupModem
        f.ShowDialog()
        f = Nothing

    End Sub

 

    Private Sub cmdRefreshList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefreshList.Click
        ListBoxInit()
    End Sub

    Private Sub FileMenu_Click(sender As System.Object, e As System.EventArgs) Handles FileMenu.Click

    End Sub

    Private lastStatus As DataTable

    Private Sub ColorizeNodes()
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim ng As Infragistics.Win.UltraWinTree.UltraTreeNode
        Dim node As Infragistics.Win.UltraWinTree.UltraTreeNode
        Me.Cursor = Cursors.Default
        Application.DoEvents()
        Dim lvi As UltraListViewItem
        Dim id_bd As Integer
        Dim alert_msg As String = ""
        Dim dt1 As DataTable
        lastStatus = TvMain.QuerySelect("select color,Info,id_bd from analizer ")
        dt1 = TvMain.QuerySelect("select bdevices.id_bd, npquery,cstatus,transport,coldwater from bdevices join  plancall on bdevices.id_bd=plancall.id_bd ")
        Dim ai As Integer
        Dim bi As Integer
        Dim CanHide As Boolean
        Application.DoEvents()
        For Each ng In tree.Nodes
            For Each node In ng.Nodes
                CanHide = True
                Application.DoEvents()
                If node.Key.ToString() <> "" Then
                    If node.Key.ToString().Substring(0, 1) = "N" Then
                        Dim alert As Boolean
                        alert = False
                        id_bd = CInt(node.Tag)

                        Application.DoEvents()
                        For bi = 0 To dt1.Rows.Count - 1
                            If dt1.Rows(bi)("id_bd") = id_bd Then
                                Exit For
                            End If
                        Next


                        Application.DoEvents()
                        For ai = 0 To lastStatus.Rows.Count - 1
                            If lastStatus.Rows(ai)("id_bd") = id_bd Then
                                Exit For
                            End If
                        Next

                        Dim bcolor As System.Drawing.Color = Color.White
                        If ai < lastStatus.Rows.Count Then
                            If lastStatus.Rows(ai)("COLOR") & "" = "RED" Then
                                bcolor = Color.Red
                                CanHide = False
                                alert = True
                            End If
                            If lastStatus.Rows(ai)("COLOR") & "" = "YELLOW" Then
                                CanHide = False
                                bcolor = Color.Yellow
                            End If
                        End If


                        If bi < dt1.Rows.Count Then

                            Dim cw As Integer = 0
                            Try
                                If dt1.Rows(bi)("coldwater") = 1 Then
                                    cw = 4
                                End If
                            Catch ex As Exception

                            End Try

                            If dt1.Rows(bi)("cstatus") = 1 And dt1.Rows(bi)("npquery") = 0 Then
                                node.Override.NodeAppearance.BackColor = Color.White
                                node.Override.NodeAppearance.ForeColor = Color.LightGray
                                node.LeftImages.Clear()



                                Select Case CInt(dt1.Rows(bi)("transport"))
                                    Case 0, 1
                                    node.LeftImages.Add(1 + cw)
                                    Case 2, 3, 9, 10
                                        node.LeftImages.Add(2 + cw)
                                    Case 4
                                        node.LeftImages.Add(4 + cw)
                                    Case 5, 6, 7
                                        node.LeftImages.Add(3 + cw)
                                    Case Else
                                        node.LeftImages.Add(1 + cw)
                                End Select

                                alert = False
                            End If

                            If dt1.Rows(bi)("cstatus") = 1 And dt1.Rows(bi)("npquery") = 1 Then
                                node.Override.NodeAppearance.BackColor = bcolor
                                node.Override.NodeAppearance.ForeColor = Color.Blue
                                node.LeftImages.Clear()


                                Select Case CInt(dt1.Rows(bi)("transport"))
                                    Case 0, 1
                                    node.LeftImages.Add(1 + cw)
                                    Case 2, 3, 9, 10
                                        node.LeftImages.Add(2 + cw)
                                    Case 4
                                        node.LeftImages.Add(4 + cw)
                                    Case 5, 6, 7
                                        node.LeftImages.Add(3 + cw)
                                    Case Else
                                        node.LeftImages.Add(1 + cw)
                                End Select

                            End If

                            If dt1.Rows(bi)("cstatus") = 0 Then
                                node.Override.NodeAppearance.BackColor = bcolor
                                node.Override.NodeAppearance.ForeColor = Color.Black
                                CanHide = False
                                node.LeftImages.Clear()
                                'If dt1.Rows(bi)("transport") = 0 Or dt1.Rows(bi)("transport") = 4 Then
                                '    node.LeftImages.Add(1 + cw)
                                'Else
                                '    node.LeftImages.Add(2 + cw)
                                'End If


                                Select Case CInt(dt1.Rows(bi)("transport"))
                                    Case 0, 1
                                    node.LeftImages.Add(1 + cw)
                                    Case 2, 3, 9
                                        node.LeftImages.Add(2 + cw)
                                    Case 4
                                        node.LeftImages.Add(4 + cw)
                                    Case 5, 6, 7
                                        node.LeftImages.Add(3 + cw)
                                    Case Else
                                        node.LeftImages.Add(1 + cw)
                                End Select

                            End If


                        End If
                        If HideWhite Then
                            If CanHide Then
                                node.Visible = False
                            Else
                                node.Visible = True
                            End If
                        Else
                            node.Visible = True
                        End If


                        Try
                            If alert Then
                                If alert_msg <> "" Then
                                    alert_msg = alert_msg & vbCrLf
                                End If
                                alert_msg = alert_msg & node.Text & vbCrLf & lastStatus.Rows(ai)("INFO")
                            End If
                        Catch ex As Exception

                        End Try


                    End If
                End If
            Next
        Next


        Try




            For Each lvi In ListView1.Items
                CanHide = True
                id_bd = CInt(lvi.Tag)
                Application.DoEvents()
                For bi = 0 To dt1.Rows.Count - 1
                    If dt1.Rows(bi)("id_bd") = id_bd Then
                        Exit For
                    End If
                Next



                For ai = 0 To lastStatus.Rows.Count - 1
                    If lastStatus.Rows(ai)("id_bd") = id_bd Then
                        Exit For
                    End If
                Next

                Dim cw As Integer = 0
                Try
                    If dt1.Rows(bi)("coldwater") = 1 Then
                        cw = 4
                    End If
                Catch ex As Exception

                End Try

                Dim bcolor As System.Drawing.Color = Color.White
                If ai < lastStatus.Rows.Count Then
                    If lastStatus.Rows(ai)("COLOR") & "" = "RED" Then
                        bcolor = Color.Red
                        CanHide = False
                    End If
                    If lastStatus.Rows(ai)("COLOR") & "" = "YELLOW" Then
                        bcolor = Color.Yellow
                        CanHide = False
                    End If
                End If

                If bi < dt1.Rows.Count Then
                    If dt1.Rows(bi)("cstatus") = 1 And dt1.Rows(bi)("npquery") = 0 Then
                        lvi.Appearance.BackColor = Color.White
                        lvi.Appearance.ForeColor = Color.LightGray

                    End If

                    If dt1.Rows(bi)("cstatus") = 1 And dt1.Rows(bi)("npquery") = 1 Then
                        lvi.Appearance.BackColor = bcolor
                        lvi.Appearance.ForeColor = Color.Blue
                    End If

                    If dt1.Rows(bi)("cstatus") = 0 Then
                        lvi.Appearance.BackColor = bcolor
                        lvi.Appearance.ForeColor = Color.Black
                        CanHide = False
                    End If

                    'If dt1.Rows(bi)("transport") = 0 Or dt1.Rows(bi)("transport") = 4 Then
                    '    lvi.Appearance.Image = 1 + cw
                    'Else
                    '    lvi.Appearance.Image = 2 + cw
                    'End If


                    Select Case CInt(dt1.Rows(bi)("transport"))
                        Case 0, 1
                        lvi.Appearance.Image = 1 + cw
                        Case 2, 3, 9, 10
                            lvi.Appearance.Image = 2 + cw
                        Case 4
                            lvi.Appearance.Image = 4 + cw
                        Case 5, 6
                            lvi.Appearance.Image = 3 + cw
                        Case Else
                            lvi.Appearance.Image = 1 + cw
                    End Select



                End If


                If HideWhite Then
                    If CanHide Then
                        lvi.Visible = False

                    Else
                        lvi.Visible = True


                    End If
                Else
                    lvi.Visible = True
                End If
            Next
            If HideWhite Then


            End If
        Catch ex As Exception

        End Try

        If alert_msg <> "" Then
            If GetSetting("VIP", "Setting", "ALERT", "false") = "true" Then
                If fAlert Is Nothing Then
                    fAlert = New frmAlert
                End If
                fAlert.txtInfo.Text = alert_msg
                fAlert.Show()
                fAlert.Focus()
            End If

        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private InTimer As Boolean
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If InTimer Then Exit Sub
        InTimer = True
        ColorizeNodes()
        InTimer = False

    End Sub

    Private Sub SetNodeTip(ByRef n As Infragistics.Win.UltraWinTree.UltraTreeNode)
        Dim msg As String = ""
        Dim id_bd As Integer
        Dim ai As Integer
        If lastStatus Is Nothing Then Exit Sub
        If Not n Is Nothing Then
            If n.Key.ToString().Substring(0, 1) = "N" Then
                id_bd = CInt(n.Tag)


                For ai = 0 To lastStatus.Rows.Count - 1
                    If lastStatus.Rows(ai)("id_bd") = id_bd Then
                        Exit For
                    End If
                Next
                If ai >= lastStatus.Rows.Count Then
                    ToolTip.SetToolTip(tree, "")
                    ToolTip.SetToolTip(ListView1, "")
                    Exit Sub
                End If

                msg = msg & n.Text & lastStatus.Rows(ai)("INFO")
            End If
        End If

        If lastStatus.Rows(ai)("INFO") & "" <> "" And (lastStatus.Rows(ai)("COLOR") & "" = "RED" Or lastStatus.Rows(ai)("COLOR") & "" = "YELLOW") Then
            If msg.Length > 2000 Then
                msg = msg.Substring(0, 2000) + "..."
            End If
            ToolTip.SetToolTip(tree, msg)
            ToolTip.SetToolTip(ListView1, msg)
            ToolTip.AutomaticDelay = 60000
            ToolTip.InitialDelay = 1


        Else
            ToolTip.SetToolTip(tree, "")
            ToolTip.SetToolTip(ListView1, "")
        End If
    End Sub

    Private Sub cmdBounds_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBounds.Click
        Dim f As frmBounds
        Dim tag As Object
        ' Dim id_bd As String

        If tree.SelectedNodes.Count > 0 Then
            If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then

                tag = tree.SelectedNodes.Item(0).Tag
            Else
                Exit Sub
            End If
        Else
            Exit Sub

        End If


        f = New frmBounds
        f.id = Integer.Parse(tag.ToString)
        f.ShowDialog()
        Me.Focus()
    End Sub



    Private Sub tree_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tree.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim n As Infragistics.Win.UltraWinTree.UltraTreeNode
            Dim tag As Object

            If tree.SelectedNodes.Count > 0 Then
                If tree.SelectedNodes.Item(0).Key.ToString().Substring(0, 1) = "N" Then
                    n = tree.SelectedNodes.Item(0)
                    tag = tree.SelectedNodes.Item(0).Tag
                Else
                    Exit Sub
                End If
            Else
                Exit Sub

            End If
            If n.Override.NodeAppearance.BackColor = Color.Yellow Or n.Override.NodeAppearance.BackColor = Color.Red Then
                If fInf Is Nothing Then
                    fInf = New frmInfo
                End If

                Dim id_bd As String
                id_bd = CInt(n.Tag)
                fInf.ID = id_bd
                Dim dt As DataTable


                dt = TvMain.QuerySelect("select color,INFO from analizer where id_bd=" + id_bd.ToString)
                If dt.Rows.Count > 0 Then
                    fInf.txtInfo.Text = dt.Rows(0)("INFO")
                End If

                fInf.Show()
                fInf.Focus()
            End If
        End If
    End Sub


    Private Sub mnuStopAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStopAlert.Click
        SaveSetting("VIP", "Setting", "ALERT", "false")
        If Not fAlert Is Nothing Then
            Try
                fAlert.Close()
            Catch ex As Exception

            End Try

            fAlert = Nothing
        End If
    End Sub

    Private Sub mnuStartAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStartAlert.Click
        SaveSetting("VIP", "Setting", "ALERT", "true")
    End Sub

    'Private Sub udm_AfterDockChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PaneEventArgs) Handles udm.AfterDockChange
    '    If (e.Pane.DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked) Then

    '        If e.Pane.Index = 0 Or e.Pane.Index = 2 Then

    '            e.Pane.MaximumSize = New Size(220, 0)

    '            e.Pane.Size = New Size(220, 0)

    '            e.Pane.MinimumSize = New Size(220, 0)
    '        Else
    '            e.Pane.MaximumSize = New Size(0, 220)

    '            e.Pane.Size = New Size(0, 220)

    '            e.Pane.MinimumSize = New Size(0, 220)
    '        End If

    '    Else

    '        e.Pane.MaximumSize = New Size(0, 0)

    '        e.Pane.MinimumSize = New Size(0, 0)

    '    End If
    'End Sub

    'Private Sub udm_AfterToggleDockState(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinDock.PaneEventArgs) Handles udm.AfterToggleDockState
    '    If (e.Pane.DockedState = Infragistics.Win.UltraWinDock.DockedState.Docked) Then

    '        If e.Pane.Index = 0 Or e.Pane.Index = 2 Then

    '            e.Pane.MaximumSize = New Size(220, 0)

    '            e.Pane.Size = New Size(220, 0)

    '            e.Pane.MinimumSize = New Size(220, 0)
    '        Else
    '            e.Pane.MaximumSize = New Size(0, 220)

    '            e.Pane.Size = New Size(0, 220)

    '            e.Pane.MinimumSize = New Size(0, 220)
    '        End If

    '    Else

    '        e.Pane.MaximumSize = New Size(0, 0)

    '        e.Pane.MinimumSize = New Size(0, 0)

    '    End If
    'End Sub




    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Узлы.Dock = DockStyle.Top
        Узлы.Height = 200
        ListView1.Visible = True
        SaveSetting("VIP", "View", "NodePosition", "top")
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        ListView1.Visible = False
        Узлы.Dock = DockStyle.Left
        Узлы.Width = 200
        SaveSetting("VIP", "View", "NodePosition", "left")
    End Sub

    Private Sub ListView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
        If e.Button = MouseButtons.Right Then
            Dim n As UltraListViewItem
            Dim tag As Object

            If ListView1.SelectedItems().Count > 0 Then

                n = ListView1.SelectedItems().Item(0)
                tag = n.Tag

            Else
                Exit Sub

            End If
            If n.Appearance.BackColor = Color.Yellow Or n.Appearance.BackColor = Color.Red Then
                If fInf Is Nothing Then
                    fInf = New frmInfo
                End If

                Dim id_bd As String
                id_bd = CInt(n.Tag)
                Dim dt As DataTable
                fInf.ID = id_bd


                dt = TvMain.QuerySelect("select color,INFO from analizer where id_bd=" + id_bd.ToString)
                If dt.Rows.Count > 0 Then
                    Try
                        fInf.txtInfo.Text = "" & dt.Rows(0)("INFO")
                    Catch ex As Exception

                    End Try


                End If

                fInf.Show()
                fInf.Focus()
            End If
        End If
    End Sub


    

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListView1.View = View.List
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListView1.View = View.LargeIcon
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListView1.View = View.SmallIcon
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ListView1.View = View.Tile
    End Sub

    Private Sub mnuHideWhite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHideWhite.Click
        HideWhite = Not HideWhite
        If HideWhite Then
            mnuHideWhite.Checked = True
        Else
            mnuHideWhite.Checked = False
        End If
        If HideWhite Then
            SaveSetting("VIP", "View", "HideWhite", "true")
        Else
            SaveSetting("VIP", "View", "HideWhite", "false")
        End If



    End Sub

    Private Sub ListView1_ItemActivated(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinListView.ItemActivatedEventArgs) Handles ListView1.ItemActivated

    End Sub

    Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinListView.ItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        Dim TempNode As UltraListViewItem = Nothing
        Try
            TempNode = ListView1.SelectedItems().Item(0)
        Catch ex As Exception

        End Try
        If Not TempNode Is Nothing Then
            tree.SelectedNodes.Clear()
            Dim ng As Infragistics.Win.UltraWinTree.UltraTreeNode
            Dim node As Infragistics.Win.UltraWinTree.UltraTreeNode


            For Each ng In tree.Nodes
                For Each node In ng.Nodes
                    If node.Tag = TempNode.Tag Then
                        node.Selected = True
                        Exit For
                    End If
                Next
            Next
            tree.CollapseAll()

        End If
    End Sub

    Private Sub Узлы_PaintClient(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Узлы.PaintClient

    End Sub

    Private Sub mnuSezon0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSezon0.Click
        If MsgBox("Установить режим : Отопительный сезон для всех узлов?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Подтверждение") = MsgBoxResult.Yes Then
            TvMain.QueryExec("update analizer_cfg set sezon =0")
        End If

    End Sub

    Private Sub mnuSezon1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSezon1.Click
        If MsgBox("Установить режим : Межопительный период для всех узлов?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Подтверждение") = MsgBoxResult.Yes Then
            TvMain.QueryExec("update analizer_cfg set sezon =1")
        End If

    End Sub

    
    Private Sub mnuVerifyD24H_Click(sender As System.Object, e As System.EventArgs) Handles mnuVerifyD24H.Click
        Dim dt As DataTable
        Dim an As LostAnalizer
        an = New LostAnalizer()
        an.tvmain = TvMain
        dt = TvMain.GetTable("v_dev2_all")
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1

            an.NodeName = dt.Rows(i)("NodeName") & ""
            an.CheckD24H(dt.Rows(i)("id_bd"), 31)
            Me.Text = dt.Rows(i)("NodeName") + " " + i.ToString + " из " + dt.Rows.Count.ToString
            Application.DoEvents()
        Next
        Me.Text = "ВОДОПАД-IP. Диспетчер." & " Источник данных: " + TvMain.DataSourceName
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        ListBoxInit()
    End Sub
End Class
