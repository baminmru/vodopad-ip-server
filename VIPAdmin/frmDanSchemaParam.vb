Public Class frmDanSchemaParam

    Public id As Integer
    Public cfgid As Integer
    Public paramid As Integer
    Public hidefs As Integer = 0


    Private Sub frmSchemaParam_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       

        Dim tdt As DataTable
        tdt = TvMain.QuerySelect("select '(' || SPNU || ') ' || SPARAM   name, dcfg_id from DAN310CFG  where dAs_id=" & id.ToString() & " order by SPNU ")
        cmbParam.DisplayMember = "name"
        cmbParam.ValueMember = "dcfg_id"
        cmbParam.DataSource = tdt

        If paramid <> 0 And cfgid <> 0 Then
            cmbParam.SelectedValue = cfgid
        End If


        chkHideFromSchema.Checked = (hidefs = 1)



    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim hh As Integer
        If chkHideFromSchema.Checked Then
            hh = 1
        Else
            hh = 0
        End If
        If paramid = 0 Then
            Try
                Dim s As String
                s = "insert into danschemaparam(das_id,dasp_id,name,dcfg_id,hidefromschema) values( " & id.ToString & ",DanSCHEMADATA_SEQ.nextval,'" + txtName.Text + "'," + cmbParam.SelectedValue.ToString() + "," + hh.ToString + ")"
                TvMain.QueryExec(s)

                s = "select DANSCHEMADATA_SEQ.currval id from dual"
                Dim ddd As DataTable
                ddd = TvMain.QuerySelect(s)
                paramid = ddd.Rows(0)("ID")
            Catch ex As Exception

            End Try


        Else

            Try
                Dim s As String
                s = "update danschemaparam set hidefromschema=" + hh.ToString + ", name='" & txtName.Text & "',dcfg_id=" & cmbParam.SelectedValue.ToString & " where dasp_id=" + paramid.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
        End If
        Me.Close()
    End Sub

   
    Private Sub cmbParam_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbParam.SelectedIndexChanged
        'txtName.Text = cmbParam.Text
    End Sub
End Class