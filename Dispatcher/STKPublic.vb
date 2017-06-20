Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinTree
Imports Infragistics.Win.UltraWinListView

Module STKPublic
    Public WithEvents TvMain As STKTVMain.TVMain
    Public applicationStartTime As DateTime
    Public queryStartTime As DateTime
    Public LastAction As STKTVMain.UnitransportAction = STKTVMain.UnitransportAction.NoAction
    Public LastMSG As String
    Public HideWhite As Boolean = False


    Public Function MyVal(ByVal S As String) As Double
        Return Val("0" & Replace(Replace(S, ",", "."), " ", ""))
    End Function


    Public Sub RedrawNode(ByVal node As UltraTreeNode, ByVal Name As String)

        Try
            node.Text = Name
            Dim dt1 As DataTable
            dt1 = TvMain.QuerySelect("select bdevices.id_bd, npquery,cstatus from bdevices join  plancall on bdevices.id_bd=plancall.id_bd where bdevices.id_bd=" & node.Tag.ToString)
            If dt1.Rows.Count > 0 Then

                If dt1.Rows(0)("cstatus") = 1 And dt1.Rows(0)("npquery") = 0 Then
                    node.Override.NodeAppearance.BackColor = Color.White
                    node.Override.NodeAppearance.ForeColor = Color.LightGray
                End If

                If dt1.Rows(0)("cstatus") = 1 And dt1.Rows(0)("npquery") = 1 Then
                    node.Override.NodeAppearance.BackColor = Color.White
                    node.Override.NodeAppearance.ForeColor = Color.Blue
                End If

                If dt1.Rows(0)("cstatus") = 0 Then
                    node.Override.NodeAppearance.BackColor = Color.White
                    node.Override.NodeAppearance.ForeColor = Color.Black
                End If


            End If



        Catch
            MsgBox(Err.Description)
        End Try
    End Sub

    Public Sub LoadLevelData(ByVal ID As String, ByVal dt As DataTable, ByRef tv As UltraTree)
        Dim i As Integer
        Dim rootnode As UltraTreeNode
        Dim node As UltraTreeNode
        If dt Is Nothing Then Exit Sub

        If ID = "" Then
            tv.Nodes.Clear()
            tv.ViewStyle = UltraWinTree.ViewStyle.Standard
            For i = 0 To dt.Rows.Count - 1
                Try
                    node = tv.Nodes.Add()
                    node.Text = dt.Rows(i).Item("GroupName").ToString()
                    node.Tag = dt.Rows(i).Item("ID_GRP").ToString
                    node.Override.NodeAppearance.BackColor = Color.White
                    node.Override.NodeAppearance.ForeColor = Color.Black
                    node.LeftImages.Add(0)

                    node.Key = "G" & dt.Rows(i).Item("ID_GRP").ToString

                Catch
                    MsgBox(Err.Description)
                End Try
            Next
            tv.RefreshSort(tv.Nodes)
        Else
            rootnode = tv.Nodes.Item("G" & ID)
            rootnode.Nodes.Clear()
            'Dim cfg As STKTVMain.TVMain.ConfigStruct
            For i = 0 To dt.Rows.Count - 1
                Try
                    node = rootnode.Nodes.Add()
                    node.Text = dt.Rows(i).Item("NodeName").ToString() ' + " (" + cfg.Transport + ")"
                    node.Tag = dt.Rows(i).Item("ID_BD")

                 

                    node.Key = "N" + dt.Rows(i).Item("ID_BD").ToString()

                Catch
                    MsgBox(Err.Description)
                End Try
            Next
            tv.RefreshSort(rootnode.Nodes, False)
        End If

    End Sub

    Public Sub LoadLV(ByVal dt As DataTable, ByRef lv As Infragistics.Win.UltraWinListView.UltraListView)
        Dim i As Integer
        If dt Is Nothing Then Exit Sub

        lv.BeginUpdate()
        lv.Items.Clear()


        Dim TempStr(1) As String
        Dim TempNode As Infragistics.Win.UltraWinListView.UltraListViewItem = Nothing
        Dim CanHide As Boolean

        For i = 0 To dt.Rows.Count - 1
            CanHide = True
            Try
                Dim dt1 As DataTable
                Dim dt2 As DataTable
                dt1 = TvMain.QuerySelect("select bdevices.id_bd, npquery,cstatus,transport,coldwater from bdevices join  plancall on bdevices.id_bd=plancall.id_bd where bdevices.id_bd=" & dt.Rows(i).Item("ID_BD").ToString)
                dt2 = TvMain.QuerySelect("select * from analizer where id_bd=" & dt.Rows(i).Item("ID_BD").ToString)
                If dt1.Rows.Count > 0 Then
                    If dt1.Rows(0)("transport") = 0 Then
                        TempNode = lv.Items.Add(Nothing, dt.Rows(i).Item("NodeName").ToString())
                        TempNode.Tag = dt.Rows(i).Item("ID_BD")
                    Else
                        TempNode = lv.Items.Add(Nothing, dt.Rows(i).Item("NodeName").ToString())
                        TempNode.Tag = dt.Rows(i).Item("ID_BD")
                    End If
                End If

                Dim bcolor As System.Drawing.Color = Color.White
                If dt2.Rows.Count > 0 Then
                    If dt2.Rows(0)("COLOR") & "" = "RED" Then
                        bcolor = Color.Red
                        CanHide = False
                    End If
                    If dt2.Rows(0)("COLOR") & "" = "YELLOW" Then
                        bcolor = Color.Yellow
                        CanHide = False
                    End If
                End If

                If dt1.Rows.Count > 0 Then
                    Dim cw As Integer = 0
                    Try
                        If dt1.Rows(0)("coldwater") = 1 Then
                            cw = 4
                        End If
                    Catch ex As Exception

                    End Try

                    If dt1.Rows(0)("cstatus") = 1 And dt1.Rows(0)("npquery") = 0 Then
                        TempNode.Appearance.BackColor = Color.White
                        TempNode.Appearance.ForeColor = Color.LightGray

                    End If

                    If dt1.Rows(0)("cstatus") = 1 And dt1.Rows(0)("npquery") = 1 Then
                        TempNode.Appearance.BackColor = bcolor
                        TempNode.Appearance.ForeColor = Color.Blue
                    End If

                    If dt1.Rows(0)("cstatus") = 0 Then
                        TempNode.Appearance.BackColor = bcolor
                        TempNode.Appearance.ForeColor = Color.Black
                        CanHide = False
                    End If

                    Select Case CInt(dt1.Rows(0)("transport"))
                        Case 0, 1
                            TempNode.Appearance.Image = 1 + cw
                        Case 2, 3, 9
                            TempNode.Appearance.Image = 2 + cw
                        Case 4
                            TempNode.Appearance.Image = 4 + cw
                        Case 5, 6
                            TempNode.Appearance.Image = 3 + cw
                        Case Else
                            TempNode.Appearance.Image = 1 + cw
                    End Select
                    'If dt1.Rows(0)("transport") = 0 Then
                    '    TempNode.Appearance.Image = 1 + cw
                    'Else
                    '    TempNode.Appearance.Image = 2 + cw
                    'End If



                End If

                If HideWhite Then
                    If CanHide Then
                        TempNode.Visible = False

                    Else
                        TempNode.Visible = True


                    End If
                Else
                    TempNode.Visible = True
                End If


            Catch
                MsgBox(Err.Description)
            End Try
        Next
        lv.EndUpdate()
    End Sub


    Private Sub TvMain_Idle() Handles TvMain.Idle
        Application.DoEvents()
    End Sub

    Private Sub TvMain_TransportStatus(ByVal Action As STKTVMain.UnitransportAction, ByVal msg As String) Handles TvMain.TransportStatus

        LastAction = Action

        LastMSG = msg


        Application.DoEvents()
    End Sub
End Module
