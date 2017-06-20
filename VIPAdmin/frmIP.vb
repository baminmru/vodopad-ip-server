Public Class frmIP

    Private id As Integer
    Private txtip As String
    Private txtname As String

    Private Sub frm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fip = Nothing
    End Sub
    Private Sub frm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        refreshGrid()


    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select *  from ipaddr order by terminal")
        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "id_ip"

                    Case "cipaddr"
                        .HeaderText = "IP"
                        .Visible = True
                        .MinimumWidth = 250
                    Case "machine"
                        .HeaderText = "Станция"
                        .Visible = True
                        .MinimumWidth = 200


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
            If view("ID_IP") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("machine")
                Exit For
            End If
        Next
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
        txtip = view("cipaddr")

        id = view("id_ip")

        txtname = view("machine")
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmIPEdit
        f = New frmIPEdit
        f.id = id
        f.txtIP.Text = txtIP
        f.txtName.Text = txtname
   
        f.ShowDialog()
        refreshGrid()

    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim f As frmIPEdit
        f = New frmIPEdit
        f.id = 0
        f.txtIP.Text = ""
        f.txtName.Text = ""

        f.ShowDialog()
        refreshGrid()


    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String

                s = "delete from connections where id_ip=" + id.ToString
                TvMain.QueryExec(s)

                s = "delete from comports where id_ip=" + id.ToString
                TvMain.QueryExec(s)

                s = "delete from ipaddr where id_ip=" + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GV_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmIPEdit
        f = New frmIPEdit
        f.id = id
        f.txtIP.Text = txtip
        f.txtName.Text = txtname
       
        f.ShowDialog()
        refreshGrid()
    End Sub
End Class
