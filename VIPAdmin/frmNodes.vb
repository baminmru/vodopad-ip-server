Public Class frmNodes

    'Private Sub frmNodes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    ''        select bdevices.id_bd,bdevices.id_bu,bdevices.id_dev,
    ''bbuildings.id_grp,bbuildings.cshort,bbuildings.CFIO1,bbuildings.CPHONE1,bbuildings.CFIO2,bbuildings.CPHONE2,bbuildings.CADDRESS,
    ''cgrpnm,cdevname,devschema.DS_id,devschema.name,whogive.cname from bdevices 
    ''join bbuildings on bdevices.id_bu=bbuildings.id_bu
    ''join devices on bdevices.id_dev = devices.id_dev 
    ''join bgroups on bbuildings.id_grp = bgroups.id_grp
    ''join devschema on bdevices.scheme_name = devschema.name
    ''join whogive on bbuildings.id_who=whogive.id_who
    'End Sub



    Private id As Integer
    Private id_bu As Integer

    Private Sub frmNodes_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fnodes = Nothing
    End Sub
    Private Sub frmNodes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        refreshGrid()
      
    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable
        Dim q As String = ""

        q = q + "select bdevices.id_bd,bdevices.id_bu,bdevices.id_dev,"
        q = q + " bbuildings.id_who,bbuildings.id_grp,bbuildings.cshort,bbuildings.CFIO1,bbuildings.CPHONE1,bbuildings.CFIO2,bbuildings.CPHONE2,bbuildings.CADDRESS,"
        q = q + " cgrpnm,cdevname,devschema.DS_id,devschema.name sname,whogive.cname wname from bdevices "
        q = q + " left join bbuildings on bdevices.id_bu=bbuildings.id_bu"
        q = q + " left join devices on bdevices.id_dev = devices.id_dev "
        q = q + " left join bgroups on bbuildings.id_grp = bgroups.id_grp"
        q = q + " left join devschema on bdevices.scheme_name = devschema.name"
        q = q + " left join whogive on bbuildings.id_who=whogive.id_who"
        If txtFilter.Text <> "" Then
            q = q + " where (bdevices.id_bd || ';' || bbuildings.cshort || ';' ||cdevname || ';' || bbuildings.caddress || ';' || cgrpnm ) like '%" + txtFilter.Text + "%' "
        End If

        q = q + " order by cshort"
        dt = TvMain.QuerySelect(q)
        GV.DataSource = dt
        Dim i As Integer

        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "id_bd"
                        .HeaderText = "ID"
                        .Visible = True
                        .MinimumWidth = 30
                    Case "cshort"
                        .HeaderText = "узел"
                        .Visible = True
                        .MinimumWidth = 100

                    Case "caddress"
                        .HeaderText = "адрес"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "cfio1"
                        .HeaderText = "ФИО 1"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "cphone1"
                        .HeaderText = "тел 1"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "cfio2"
                        .HeaderText = "ФИО 2"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "cphone2"
                        .HeaderText = "тел 2"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "cgrpnm"
                        .HeaderText = "группа"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "cdevname"
                        .HeaderText = "тип вычислителя"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "sname"
                        .HeaderText = "схема"
                        .Visible = True
                        .MinimumWidth = 100
                    Case "wname"
                        .HeaderText = "поставщик"
                        .Visible = True
                        .MinimumWidth = 100

                End Select
            End With
        Next

        For i = 0 To GV.Rows.Count - 1
            Dim dgvr As DataGridViewRow = GV.Rows(i)

            Dim view As DataRowView = Nothing
            Try


                view = dgvr.DataBoundItem

            Catch ex As Exception

            End Try
            If view("id_bd") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("id_bd")
                Exit For
            End If
        Next


    End Sub
    Private Sub GV_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellContentClick

    End Sub

  

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Try
            Dim s As String
            s = "insert into bbuildings(id_bu) values(bbuildings_seq.nextval)"
            TvMain.QueryExec(s)
            Dim dt As DataTable

            s = "select bbuildings_seq.currval id_bu from dual"
            dt = TvMain.QuerySelect(s)
            id_bu = dt.Rows(0)("id_bu")


            s = "insert into bdevices(id_bd,id_bu,id_dev)values( bdevices_seq.nextval,bbuildings_seq.currval,1)"
            TvMain.QueryExec(s)

            s = "select bdevices_seq.currval id from dual"
            dt = TvMain.QuerySelect(s)
            id = dt.Rows(0)("id")

            cmdSetupNode_Click(sender, e)

        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "delete from bmodems where id_bd=" + id.ToString
                TvMain.QueryExec(s)
                s = "delete from plancall where id_bd=" + id.ToString
                TvMain.QueryExec(s)
                s = "delete from bdevices where id_bd=" + id.ToString
                TvMain.QueryExec(s)
                s = "delete from bbuildings where id_bu=" + id_bu.ToString
                TvMain.QueryExec(s)

                refreshGrid()
            Catch ex As Exception

            End Try
           
        End If
    End Sub

    Dim ne As NodeEditorLib.NodeEditor
    Private Sub cmdSetupNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetupNode.Click
        Dim f As Form
        If ne Is Nothing Then
            ne = New NodeEditorLib.NodeEditor
        End If
        f = ne.GetForm(id, TvMain)

        f.ShowDialog()
        f = Nothing
        refreshGrid()
    End Sub

    Private Sub GV_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        cmdSetupNode_Click(sender, e)
    End Sub
    Private Sub GV_RowEnter(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.RowEnter
        Dim dgv As DataTable = GV.DataSource
        Dim rowIndex As Integer = e.RowIndex
        Dim dgvr As DataGridViewRow = GV.Rows(rowIndex)

        Dim view As DataRowView = Nothing
        Try


            view = dgvr.DataBoundItem

        Catch ex As Exception
            Return
        End Try
      


        id = view("id_bd")
        id_bu = view("id_bu")

    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        refreshGrid()
    End Sub
End Class