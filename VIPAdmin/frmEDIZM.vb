
Public Class frmEDIZM

    Private id As Integer
    Private txtName As String
    Private txtMUL As String
    Private txtDIV As String
    Private txtPOSSIBLE As String

    Private cmbBase As Integer

    Private Sub frmEDIZM_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fed = Nothing
    End Sub
    Private Sub frmEDIZM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        refreshGrid()
    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select t.*,b.name basename from edizm t left join edizm b on t.edizm_base=b.edizm_id order by t.name")
        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "edizm_id"


                    Case "name"
                        .HeaderText = "Название"
                        .Visible = True
                        .MinimumWidth = 200
                    Case "basename"
                        .HeaderText = "База"
                        .Visible = True
                        .MinimumWidth = 200
                    Case "possibleparam"
                        .HeaderText = "Применимо к"
                        .Visible = True
                        .MinimumWidth = 200
                    Case "multipicator"
                        .HeaderText = "Умножить на"
                        .Visible = True
                        .MinimumWidth = 200
                    Case "divider"
                        .HeaderText = "Разделить на"
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
            If view("edizm_id") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("name")
                Exit For
            End If
        Next
    End Sub


    Private Sub GV_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmEDIZMEdit
        f = New frmEDIZMEdit
        f.id = id
        f.txtName.Text = txtName
        f.txtMUL.Text = txtMUL
        f.txtDIV.Text = txtDIV
        f.txtPOSSIBLE.Text = txtPOSSIBLE

        f.id_base = cmbBase
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub GV_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.RowEnter
        Dim dgv As DataTable = GV.DataSource
        Dim rowIndex As Integer = e.RowIndex
        Dim dgvr As DataGridViewRow = GV.Rows(rowIndex)

        Dim view As DataRowView = Nothing
        Try


            view = dgvr.DataBoundItem

        Catch ex As Exception
            Return
        End Try
        txtName = view("name")
        txtMUL = view("multipicator")
        txtDIV = view("divider")
        txtPOSSIBLE = view("possibleparam")

        Try
            cmbBase = view("edizm_base")
        Catch ex As Exception

        End Try

        id = view("edizm_id")
    End Sub



    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        Dim f As frmEDIZMEdit
        f = New frmEDIZMEdit
        f.id = 0
        f.ShowDialog()
        refreshGrid()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String

                s = "delete from edizm where edizm_id=" + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmEDIZMEdit
        f = New frmEDIZMEdit
        f.id = id
        f.txtName.Text = txtName
        f.txtMUL.Text = txtMUL
        f.txtDIV.Text = txtDIV
        f.txtPOSSIBLE.Text = txtPOSSIBLE

        f.id_base = cmbBase
        f.ShowDialog()
        refreshGrid()
    End Sub
End Class