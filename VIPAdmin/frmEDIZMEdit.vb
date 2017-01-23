
Public Class frmEDIZMEdit

    Public id As Integer
    Public id_type As Integer
    Public id_base As Integer
    Private Sub frmEDIZMEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        

        Dim ddt As DataTable
        ddt = TvMain.QuerySelect("select * from edizm where edizm_id <>" & id.ToString() & "  order by name")
        cmbBase.DisplayMember = "name"
        cmbBase.ValueMember = "edizm_id"
        cmbBase.DataSource = ddt

        If id <> 0 And id_base > 0 Then
            cmbBase.SelectedValue = id_base
        Else
            cmbBase.SelectedIndex = -1
        End If
    End Sub




    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim s As String
                s = "insert into edizm(edizm_id,name) values(edizm_seq.nextval,'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()

                s = "select edizm_seq.currval id from dual"
                Dim ddd As DataTable
                ddd = TvMain.QuerySelect(s)
                id = ddd.Rows(0)("ID")
            Catch ex As Exception

            End Try



            Try
                Dim s As String
                s = "update edizm set possibleparam= '" + txtPOSSIBLE.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try

            Try
                Dim s As String
                s = "update edizm set divider= '" + txtDIV.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try

            Try
                Dim s As String
                s = "update edizm set multipicator= '" + txtMUL.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try


            If cmbBase.SelectedIndex > 0 Then
                Try
                    Dim s As String
                    s = "update edizm set  edizm_base = " + cmbBase.SelectedValue.ToString + "   where edizm_id=" + id.ToString
                    TvMain.QueryExec(s)

                Catch ex As Exception

                End Try
            Else
                Try
                    Dim s As String
                    s = "update edizm set  edizm_base = null   where edizm_id=" + id.ToString
                    TvMain.QueryExec(s)

                Catch ex As Exception

                End Try
            End If


           
            Me.Close()

        Else

     
            Try
                Dim s As String
                s = "update edizm set  name='" + txtName.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update edizm set possibleparam= '" + txtPOSSIBLE.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try

            Try
                Dim s As String
                s = "update edizm set divider= '" + txtDIV.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try

            Try
                Dim s As String
                s = "update edizm set multipicator= '" + txtMUL.Text + "'   where edizm_id=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try


            If cmbBase.SelectedIndex >= 0 Then
                Try
                    Dim s As String
                    s = "update edizm set  edizm_base = " + cmbBase.SelectedValue.ToString + "   where edizm_id=" + id.ToString
                    TvMain.QueryExec(s)

                Catch ex As Exception

                End Try
            Else
                Try
                    Dim s As String
                    s = "update edizm set  edizm_base = null   where edizm_id=" + id.ToString
                    TvMain.QueryExec(s)

                Catch ex As Exception

                End Try
            End If
            Me.Close()
        End If


    End Sub




    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        cmbBase.SelectedIndex = -1
    End Sub
End Class