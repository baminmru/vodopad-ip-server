Public Class frmUsers

    Private id As Integer
    Private txtlogin As String
    Private txtpassword As String
    Private txtlocked As Integer
    Private txtrequery As Integer
    Private txtreport As Integer
    Private txttemplate As Integer
    Private Sub frm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fusr = Nothing
    End Sub
    Private Sub frm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        refreshGrid()


    End Sub

    Private Sub refreshGrid()
        Dim previd As Integer
        previd = id
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select *  from users order by login")
        GV.DataSource = dt
        Dim i As Integer
        For i = 0 To GV.Columns.Count - 1

            With GV.Columns.Item(i)
                .Visible = False
                Select Case .DataPropertyName.ToLower
                    Case "usersid"

                    Case "login"
                        .HeaderText = "Имя для входа"
                        .Visible = True
                        .MinimumWidth = 250
                    Case "locked"
                        .HeaderText = "Заблокирован"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "allowrequery"
                        .HeaderText = "Разрешен переопрос"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "allowreport"
                        .HeaderText = "Отчеты"
                        .Visible = True
                        .MinimumWidth = 60
                    Case "allowtemplate"
                        .HeaderText = "Добавление шаблонов"
                        .Visible = True
                        .MinimumWidth = 60


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
            If view("usersid") = previd Then
                GV.Rows(i).Selected = True
                GV.CurrentCell = GV.Rows(i).Cells("login")
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
        txtlogin = view("login")

        id = view("usersid")
        txtlocked = view("locked")
        txtpassword = view("password")
        txtrequery = view("allowrequery")
        txtreport = view("allowreport")
        txttemplate = view("allowtemplate")

    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim f As frmUsersEdit
        f = New frmUsersEdit
        f.id = id
        f.txtLogin.Text = txtlogin
        f.txtPassword.Text = txtpassword
        If txtlocked = 0 Then
            f.chkLocked.Checked = False
        Else
            f.chkLocked.Checked = True
        End If

        If txtrequery = 0 Then
            f.chkRequery.Checked = False
        Else
            f.chkRequery.Checked = True
        End If


        If txttemplate = 0 Then
            f.chkTpl.Checked = False
        Else
            f.chkTpl.Checked = True
        End If

        If txtreport = 0 Then
            f.chkReport.Checked = False
        Else
            f.chkReport.Checked = True
        End If

    

        f.chkGroups.Visible = True
        f.LoadGroups()
        f.ShowDialog()
        refreshGrid()

    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim f As frmusersEdit
        f = New frmusersEdit
        f.id = 0
        f.txtLogin.Text = ""
        f.txtPassword.Text = ""
        f.chkLocked.Checked = False
        f.chkReport.Checked = False
        f.chkTpl.Checked = False
        f.chkRequery.Checked = False
        f.chkGroups.Visible = False
        f.ShowDialog()
        refreshGrid()


    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        If MsgBox("Удалить текущую запись ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim s As String


                s = "delete from webreport where usersid=" + id.ToString
                TvMain.QueryExec(s)

                s = "delete from usergroup where usersid=" + id.ToString
                TvMain.QueryExec(s)

                s = "delete from users where usersid=" + id.ToString
                TvMain.QueryExec(s)
                refreshGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GV_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GV.CellDoubleClick
        Dim f As frmUsersEdit
        f = New frmUsersEdit
        f.id = id
        f.txtLogin.Text = txtlogin
        f.txtPassword.Text = txtpassword
        If txtlocked = 0 Then
            f.chkLocked.Checked = False
        Else
            f.chkLocked.Checked = True
        End If

        If txtrequery = 0 Then
            f.chkRequery.Checked = False
        Else
            f.chkRequery.Checked = True
        End If

        If txttemplate = 0 Then
            f.chkTpl.Checked = False
        Else
            f.chkTpl.Checked = True
        End If

        If txtreport = 0 Then
            f.chkReport.Checked = False
        Else
            f.chkReport.Checked = True
        End If

        f.chkGroups.Visible = True
        f.LoadGroups()
        f.ShowDialog()
        refreshGrid()
    End Sub
End Class
