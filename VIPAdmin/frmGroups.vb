Public Class frmGroups

    Private id As Integer
    Private txtname As String

    Private Sub frmGroups_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fgrp = Nothing
    End Sub
    Private Sub frm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        refreshGrid()


    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select *  from bgroups order by cgrpnm")
        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "id_grp"

                    Case "cgrpnm"
                        .HeaderText = "Название группы"
                        .Visible = True
                        .MinimumWidth = 400


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
            If view("id_grp") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("cgrpnm")
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
        txtName = view("cgrpnm")

        id = view("id_grp")
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmGroupsEdit
        f = New frmGroupsEdit
        f.id = id
        f.txtName.Text = txtname
        f.ShowDialog()
        refreshGrid()

    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim f As frmGroupsEdit
        f = New frmGroupsEdit
        f.id = 0
        f.txtName.Text = ""
        f.ShowDialog()
        refreshGrid()


    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String
                s = "delete from bgroups where id_grp=" + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GV_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmGroupsEdit
        f = New frmGroupsEdit
        f.id = id
        f.txtName.Text = txtname
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub GV_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellContentClick

    End Sub
End Class
