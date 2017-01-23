Public Class frmSchemaPipe
    Public id As Integer
    Public pipeid As Integer
    Public pipetypeid As Integer



    Private Sub frmSchemaPipe_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tdt As DataTable
        tdt = TvMain.QuerySelect("select pipetype.* , '(' || id_pipe || ') ' || pipename name   from pipetype order by id_pipe")
        cmbPipeType.DisplayMember = "name"
        cmbPipeType.ValueMember = "id_pipe"
        cmbPipeType.DataSource = tdt

        If pipeid <> 0 Then
            cmbPipeType.SelectedValue = pipetypeid
        End If


    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If pipeid = 0 Then
            Try
                Dim s As String
                s = "insert into devschemapipes(ds_id,dspipe_id,inputnumber,pipenumber,pipetype_id) values( " & id.ToString & ",DEVSCHEMAPIPES_SEQ.nextval," + txtInputNumber.Text + "," + txtPipeNumber.Text + ",0)"
                TvMain.QueryExec(s)


                s = "select DEVSCHEMAPIPES_SEQ.currval id from dual"
                Dim ddd As DataTable
                ddd = TvMain.QuerySelect(s)
                pipeid = ddd.Rows(0)("ID")
            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update devschemapipes set   pipetype_id = " + cmbPipeType.SelectedValue.ToString + "   where dspipe_id=" + pipeid.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
         
        Else

            Try
                Dim s As String
                s = "update devschemapipes set inputnumber=" & txtInputNumber.Text & ",PipeNumber=" & txtPipeNumber.Text & ",  pipetype_id = " + cmbPipeType.SelectedValue.ToString + "   where dspipe_id=" + pipeid.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
        End If
        Me.Close()
    End Sub
End Class