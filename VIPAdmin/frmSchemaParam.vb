Public Class frmSchemaParam

    Public id As Integer
    Public pipeid As Integer
    Public paramid As Integer
    Public edizmid As Integer
    Public nParam As String


    Private Sub frmSchemaParam_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tdt As DataTable
        tdt = TvMain.QuerySelect("select DSPIPE_ID,'ТВ'|| inputnumber || ' ТР' || pipenumber || '-'|| pipename name, dspipe_id from DEVSCHEMAPIPES join pipetype on DEVSCHEMAPIPES.pipetype_id=pipetype.id_pipe  where ds_id=" & id.ToString() & " order by name")
        cmbPipeType.DisplayMember = "name"
        cmbPipeType.ValueMember = "dspipe_id"
        cmbPipeType.DataSource = tdt

        If paramid <> 0 And pipeid <> 0 Then
            cmbPipeType.SelectedValue = pipeid
        End If


        Dim tdt2 As DataTable
        tdt2 = TvMain.QuerySelect("select EDIZM_ID, name from EDIZM order by name")
        cmbEdizm.DisplayMember = "name"
        cmbEdizm.ValueMember = "EDIZM_ID"
        cmbEdizm.DataSource = tdt2

        If paramid <> 0 And edizmid <> 0 Then
            cmbEdizm.SelectedValue = edizmid
        End If



        cmbParam.Items.Clear()
        cmbParam.Items.Add("Q1")
        cmbParam.Items.Add("Q2")
        cmbParam.Items.Add("Q3")
        cmbParam.Items.Add("Q4")
        cmbParam.Items.Add("Q5")
        cmbParam.Items.Add("Q6")
        cmbParam.Items.Add("DQ12")
        cmbParam.Items.Add("DQ45")
        cmbParam.Items.Add("Q1H")
        cmbParam.Items.Add("Q2H")

        cmbParam.Items.Add("T1")
        cmbParam.Items.Add("T2")
        cmbParam.Items.Add("DT12")
        cmbParam.Items.Add("T3")
        cmbParam.Items.Add("T4")
        cmbParam.Items.Add("T5")
        cmbParam.Items.Add("DT45")
        cmbParam.Items.Add("T6")

        cmbParam.Items.Add("V1")
        cmbParam.Items.Add("V2")
        cmbParam.Items.Add("DV12")
        cmbParam.Items.Add("V3")
        cmbParam.Items.Add("V4")
        cmbParam.Items.Add("V5")
        cmbParam.Items.Add("DV45")
        cmbParam.Items.Add("V6")

        cmbParam.Items.Add("M1")
        cmbParam.Items.Add("M2")
        cmbParam.Items.Add("DM12")
        cmbParam.Items.Add("M3")
        cmbParam.Items.Add("M4")
        cmbParam.Items.Add("M5")
        cmbParam.Items.Add("DM45")
        cmbParam.Items.Add("M6")

        cmbParam.Items.Add("P1")
        cmbParam.Items.Add("P2")
        cmbParam.Items.Add("P3")
        cmbParam.Items.Add("P4")
        cmbParam.Items.Add("P5")
        cmbParam.Items.Add("P6")

        cmbParam.Items.Add("G1")
        cmbParam.Items.Add("G2")
        cmbParam.Items.Add("G3")
        cmbParam.Items.Add("G4")
        cmbParam.Items.Add("G5")
        cmbParam.Items.Add("G6")


       
       

        cmbParam.Items.Add("V1H")
        cmbParam.Items.Add("V2H")
        cmbParam.Items.Add("V4H")
        cmbParam.Items.Add("V5H")
        cmbParam.Items.Add("DG12")
        cmbParam.Items.Add("DG45")
        cmbParam.Items.Add("DP12")
        cmbParam.Items.Add("DP45")

        cmbParam.Items.Add("PATM")
        cmbParam.Items.Add("PXB")
        cmbParam.Items.Add("DQ")

        cmbParam.Items.Add("TCOOL")
        cmbParam.Items.Add("THOT")
        cmbParam.Items.Add("TAIR1")
        cmbParam.Items.Add("TAIR2")
        cmbParam.Items.Add("TCE1")
        cmbParam.Items.Add("TCE2")

        cmbParam.Items.Add("DANS1")
        cmbParam.Items.Add("DANS2")
        cmbParam.Items.Add("DANS3")
        cmbParam.Items.Add("DANS4")
        cmbParam.Items.Add("DANS5")
        cmbParam.Items.Add("DANS6")

        cmbParam.Items.Add("OKTIME")
        cmbParam.Items.Add("ERRTIME")
        cmbParam.Items.Add("WORKTIME")
        cmbParam.Items.Add("OKTIME2")
        cmbParam.Items.Add("ERRTIME2")
        cmbParam.Items.Add("TSUM1")
        cmbParam.Items.Add("TSUM2")
      

        cmbParam.Items.Add("HCRAW")
        cmbParam.Items.Add("HCRAW1")
        cmbParam.Items.Add("HCRAW2")

        If nParam <> "" Then
            cmbParam.Text = nParam
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If paramid = 0 Then
            Try
                Dim s As String
                s = "insert into devschemaparam(ds_id,dsp_id,name,edizm_id,dspipe_id) values( " & id.ToString & ",DEVSCHEMADATA_SEQ.nextval,'" + cmbParam.Text + "'," + cmbEdizm.SelectedValue.ToString() + "," + cmbPipeType.SelectedValue.ToString + ")"
                TvMain.QueryExec(s)

                s = "select DEVSCHEMADATA_SEQ.currval id from dual"
                Dim ddd As DataTable
                ddd = TvMain.QuerySelect(s)
                paramid = ddd.Rows(0)("ID")
            Catch ex As Exception

            End Try
          

        Else

            Try
                Dim s As String
                s = "update devschemaparam set name='" & cmbParam.Text & "',dspipe_id=" & cmbPipeType.SelectedValue.ToString & ",  edizm_id = " + cmbEdizm.SelectedValue.ToString + "   where dsp_id=" + paramid.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
        End If
        Me.Close()
    End Sub

    Private Sub cmbParam_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbParam.SelectedIndexChanged
        edizmid = cmbEdizm.SelectedValue

        Dim tdt2 As DataTable
        tdt2 = TvMain.QuerySelect("select EDIZM_ID, name from EDIZM  where possibleparam like '%" + cmbParam.Text.ToLower() + "%' order by name")
        cmbEdizm.DisplayMember = "name"
        cmbEdizm.ValueMember = "EDIZM_ID"
        cmbEdizm.DataSource = tdt2

        Try
            cmbEdizm.SelectedValue = edizmid
        Catch ex As Exception

        End Try


    End Sub

    Private Sub cmbParam_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbParam.TextChanged
        edizmid = cmbEdizm.SelectedValue

        Dim tdt2 As DataTable
        tdt2 = TvMain.QuerySelect("select EDIZM_ID, name from EDIZM  where possibleparam like '%" + cmbParam.Text.ToLower() + "%' order by name")
        cmbEdizm.DisplayMember = "name"
        cmbEdizm.ValueMember = "EDIZM_ID"
        cmbEdizm.DataSource = tdt2

        Try
            cmbEdizm.SelectedValue = edizmid
        Catch ex As Exception

        End Try
    End Sub
End Class