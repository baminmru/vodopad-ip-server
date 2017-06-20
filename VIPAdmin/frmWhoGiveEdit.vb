
Public Class frmWhoGiveEdit

    Public id As Integer
    Public id_top As Integer
    Private Sub frmWhoGive_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
     
        Dim tdt As DataTable
        tdt = TvMain.QuerySelect("select * from whogivetop order by cname")
        cmbTop.DisplayMember = "cname"
        cmbTop.ValueMember = "id_whotop"
        cmbTop.DataSource = tdt

        If id <> 0 Then
            cmbTop.SelectedValue = id_top
        End If
    End Sub

  
    

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim s As String
                s = "insert into whogive(id_who,id_whotop,cname) values(whogive_seq.nextval," + cmbTop.SelectedValue.ToString + ",'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else

            Try
                Dim s As String
                s = "update whogive set  cname='" + txtName.Text + "' , id_whotop = " + cmbTop.SelectedValue.ToString + "   where id_who=" + id.ToString
                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub

   

   
    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class