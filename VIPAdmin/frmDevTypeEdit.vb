Public Class frmDevTypeEdit

    Public id As Integer
    Public idClass As Integer


    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim a As Int32
        Try
            a = Int32.Parse(txtAddMS.Text)
        Catch ex As Exception
            a = 0
        End Try
        If id = 0 Then
            Try
                Dim ii As Integer
                Dim dt As DataTable
                dt = TvMain.QuerySelect("select nvl(max(id_dev),0) id_dev from  devices")
                ii = dt.Rows(0)("id_dev")
                Dim s As String

                s = "insert into devices(id_dev,cdevname,cdevdesc,dllname,VERIFYCOLS,id_class,addms) values(" + (ii + 1).ToString + ",'" + txtName.Text + "','" + txtDesc.Text + "','" + txtDLLNAME.Text + "','" + txtVerifyCOLS.Text + "'," + cmbClass.SelectedValue.ToString + "," + a.ToString + ")"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                s = "update devices set  cdevname='" + txtName.Text + "' ,cdevdesc='" + txtDesc.Text + "' ,DLLNAME='" + txtDLLNAME.Text + "', verifycols='" + txtVerifyCOLS.Text + "', id_class=" + cmbClass.SelectedValue.ToString + ", addms=" + a.ToString + "  where id_dev=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs)
       

    End Sub



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmDevTypeEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ddt As DataTable
        ddt = TvMain.QuerySelect("select * from devclasses  order by id_class")
        cmbClass.DisplayMember = "classname"
        cmbClass.ValueMember = "id_class"
        cmbClass.DataSource = ddt

        If id <> 0 And idClass > 0 Then
            cmbClass.SelectedValue = idClass
        Else
            cmbClass.SelectedIndex = 0
        End If
    End Sub
End Class