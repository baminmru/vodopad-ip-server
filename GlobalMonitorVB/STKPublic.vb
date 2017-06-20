Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinTree

Module STKPublic
    Public WithEvents TvMain As STKTVMain.TVMain
    Public applicationStartTime As DateTime
    Public queryStartTime As DateTime
    Public LastAction As STKTVMain.UnitransportAction = STKTVMain.UnitransportAction.Wait
    Public LastMSG As String

    Public Sub RedrawNode(node As UltraTreeNode, ByVal Name As String)

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

                    node.Key = "G" & dt.Rows(i).Item("ID_GRP").ToString

                Catch
                    MsgBox(Err.Description)
                End Try
            Next
            tv.RefreshSort(tv.Nodes)
        Else
            rootnode = tv.Nodes.Item("G" & ID)
            rootnode.Nodes.Clear()
            For i = 0 To dt.Rows.Count - 1
                Try
                    node = rootnode.Nodes.Add()
                    node.Text = dt.Rows(i).Item("NodeName").ToString()
                    node.Tag = dt.Rows(i).Item("ID_BD")
                    Dim dt1 As DataTable
                    dt1 = TvMain.QuerySelect("select bdevices.id_bd, npquery,cstatus from bdevices join  plancall on bdevices.id_bd=plancall.id_bd where bdevices.id_bd=" & dt.Rows(i).Item("ID_BD").ToString)
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

                    node.Key = "N" + dt.Rows(i).Item("ID_BD").ToString()

                Catch
                    MsgBox(Err.Description)
                End Try
            Next
            tv.RefreshSort(rootnode.Nodes, False)
        End If

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
