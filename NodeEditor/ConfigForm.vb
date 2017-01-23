Imports System.IO
Imports System.Windows.Forms
Imports Oracle.ManagedDataAccess.Client
Imports System.Drawing

Public Class ConfigForm
    Public TvMain As STKTVMain.TVMain
    Public ID As Int32
    Public ID_BU As Int32
    Public BName As String = ""
    Dim dt As DataTable
    Dim dtBModems As DataTable
    Dim dtContract As DataTable
    Dim dtAn As DataTable
    Dim dtB As DataTable


    Dim dtMain As DataTable

    Private Sub ConfigForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub
    Private Sub ConfigForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        Dim q As String = ""

        q = q + "select bdevices.das_id,bdevices.l_nzcols,bdevices.nzcols,bdevices.id_bd,bdevices.id_bu,bdevices.id_dev,bdevices.hiderow,bdevices.npquery,coldwater,hourshift,"
        q = q + " id_mask_curr,id_mask_hour,id_mask_24,id_mask_sum,bdevices.nppassword,"
        q = q + " bbuildings.id_who,bbuildings.id_grp,bbuildings.cshort,bbuildings.CFIO1,bbuildings.CPHONE1,bbuildings.CFIO2,bbuildings.CPHONE2,bbuildings.CADDRESS,bbuildings.FULLADDRESS,"
        q = q + " cgrpnm,cdevname,devschema.DS_id,devschema.name sname,whogive.cname wname from bdevices "
        q = q + " join bbuildings on bdevices.id_bu=bbuildings.id_bu"
        q = q + " left join devices on bdevices.id_dev = devices.id_dev "
        q = q + " left join bgroups on bbuildings.id_grp = bgroups.id_grp"
        q = q + " left join devschema on bdevices.scheme_name = devschema.name"
        q = q + " left join whogive on bbuildings.id_who=whogive.id_who  where bdevices.id_bd=" & ID.ToString() + " order by cshort"
        dtMain = TvMain.QuerySelect(q)

        If dtMain.Rows.Count = 0 Then
            dtMain = TvMain.QuerySelect("select * From bdevices where id_bd=" + ID.ToString)
            If dtMain.Rows.Count > 0 Then

                Try
                    Dim s As String
                    s = "insert into bbuildings(id_bu) values( " + dtMain.Rows(0)("id_bu").ToString() + " )"
                    TvMain.QueryExec(s)


                Catch ex As Exception

                End Try
                dtMain = TvMain.QuerySelect(q)
            End If
        End If

        dt = TvMain.QuerySelect("select * from PLANCALL where id_bd=" & ID.ToString())
        If dt.Rows.Count = 1 Then
            pnlPlanCall.Attach(dt.Rows(0), False)
        Else
            TvMain.QueryExec("insert into plancall(id_bd,cstatus,ccurr,c24,chour,csum,icall,icall24,icallcurr,icallsum,numhour,num24,NMAXCALL,MINREPEAT) values(" & ID.ToString() & ",1,0,0,0,0,60,24,60,1440,3,3,4,5)")
            dt = TvMain.QuerySelect("select * from PLANCALL where id_bd=" & ID.ToString())
            If dt.Rows.Count = 1 Then
                pnlPlanCall.Attach(dt.Rows(0), False)
            End If
        End If



        dtBModems = TvMain.QuerySelect("select bmodems.*,bdevices.npip, bdevices.nppassword, bdevices.npquery,  bdevices.transport, bdevices.ipport,bdevices.callerid from bmodems join bdevices on bmodems.id_bd=bdevices.id_bd  where bmodems.id_bd=" & ID.ToString())
        If dtBModems.Rows.Count = 1 Then
            pnlBModems.Attach(dtBModems.Rows(0), False)
        Else
            TvMain.QueryExec("insert into bmodems(id_modem,id_bd,cspeed,connectlimit,PARAMLIMIT) values(bmodems_seq.nextval," & ID.ToString() & ",2400,60,15)")
            dtBModems = TvMain.QuerySelect("select bmodems.*,bdevices.npip, bdevices.nppassword, bdevices.npquery,  bdevices.transport, bdevices.ipport,bdevices.callerid from bmodems join bdevices  on bmodems.id_bd=bdevices.id_bd  where bmodems.id_bd=" & ID.ToString())
            If dtBModems.Rows.Count = 1 Then
                pnlBModems.Attach(dtBModems.Rows(0), False)
            End If
        End If

        dtContract = TvMain.QuerySelect("select * from contract where id_bd=" & ID.ToString())
        If dtContract.Rows.Count = 1 Then
            pnlContract.Attach(dtContract.Rows(0), False, TvMain)
        Else
            TvMain.QueryExec("insert into contract(id_bd) values(" & ID.ToString() & ")")
            dtContract = TvMain.QuerySelect("select * from contract where id_bd=" & ID.ToString())
            If dtContract.Rows.Count = 1 Then
                pnlContract.Attach(dtContract.Rows(0), False, TvMain)
            End If
        End If





        dtB = TvMain.QuerySelect("select bdevices.*,id_who from bdevices  join bbuildings on bdevices.id_bu= bbuildings.id_bu where id_bd =" & ID.ToString())

        If TypeName(dtB.Rows(0)("srv_ip_id")) <> "DBNull" Then
            cmbSRV.SelectedValue = dtB.Rows(0)("srv_ip_id")
        Else
            cmbSRV.SelectedValue = 0
        End If

        cmbWHO.SelectedValue = dtB.Rows(0)("id_who")
        txtH_SH.Text = dtB.Rows(0)("REPMASKFILEH") & ""
        txtH_PAGE.Text = dtB.Rows(0)("REPMASKSHEETH") & ""
        txtD_SH.Text = dtB.Rows(0)("CXLSFILE") & ""
        txtD_PAGE.Text = dtB.Rows(0)("CSHEET") & ""

        TextBoxID_BD.Text = ID
        ID_BU = dtMain.Rows(0)("id_bu")

        Dim i As Integer
        i = dtMain.Rows(0)("NPQuery")

        If i = 0 Then
            chkIP.Checked = False
        Else
            chkIP.Checked = True
        End If

        i = dtMain.Rows(0)("HideRow")

        If i = 0 Then
            chkHideRow.Checked = False
        Else
            chkHideRow.Checked = True
        End If

        Try
            i = dtMain.Rows(0)("ColdWater")

            If i = 0 Then
                chkCOLDWATER.Checked = False
            Else
                chkCOLDWATER.Checked = True
            End If
        Catch ex As Exception
            chkCOLDWATER.Checked = False
        End Try
        
        Try
            txtHourShift.Value = Val(dtMain.Rows(0)("hourshift"))
        Catch ex As Exception
            txtHourShift.Value = 0
        End Try

        txtLINKED.Text = dtB.Rows(0)("linked_id_bd") & ""
        txtName.Text = dtMain.Rows(0)("cshort") & ""
        txtcaddress.Text = dtMain.Rows(0)("caddress") & ""
        txtFULLADDRESS.Text = dtMain.Rows(0)("fulladdress") & ""
        txtcfio1.Text = dtMain.Rows(0)("cfio1") & ""
        txtcphone1.Text = dtMain.Rows(0)("cphone1") & ""
        txtcfio2.Text = dtMain.Rows(0)("cfio2") & ""
        txtphone2.Text = dtMain.Rows(0)("cphone2") & ""
        cmbDevtype.SelectedValue = dtMain.Rows(0)("id_dev")
        cmbGRP.SelectedValue = dtMain.Rows(0)("id_grp")
        txtNZCOLS.Text = dtMain.Rows(0)("nzcols") & ""
        txtL_NZCOLS.Text = dtMain.Rows(0)("l_nzcols") & ""
        Try
            cmbSchema.SelectedValue = dtMain.Rows(0)("sname")
        Catch ex As Exception

        End Try

        Try
            cmbDanSchema.SelectedValue = dtMain.Rows(0)("das_id")
        Catch ex As Exception
        End Try

        Try
            cmbWHO.SelectedValue = dtMain.Rows(0)("id_who")
        Catch ex As Exception

        End Try

        cmbMaskM.SelectedValue = dtMain.Rows(0)("id_mask_curr")
        cmbMaskH.SelectedValue = dtMain.Rows(0)("id_mask_hour")
        cmbMaskD.SelectedValue = dtMain.Rows(0)("id_mask_24")
        cmbMaskT.SelectedValue = dtMain.Rows(0)("id_mask_sum")




        dtAn = TvMain.QuerySelect("select * from analizer_cfg where id_bd =" & ID.ToString())
        If dtAn.Rows.Count = 0 Then
            Dim q1 As String
            q1 = "insert into analizer_cfg ( ID_BD , ANALIZENODE, OPENSYSTEM , " + _
                " T1 , T2 , T3 , T4 , T5 , T6 , V1 , V2 , V3 , V4 , V5 , V6 , M1 , M2 , M3 , M4 , M5 , M6 , P1 , P2 , P3 , P4 , P5 , P6 , G1 , G2 , G3 , G4 , G5 , G6 , Q1 , Q2 , Q3 , Q4 , Q5 ," + _
                " M_V1 , M_V2 , M_V3 , M_V4 , M_V5 , M_V6 , M_M1 , M_M2 , M_M3 , M_M4 , M_M5 , M_M6 , M_P1 , M_P2 , M_P3 , M_P4 , M_P5 , M_P6 ,M_G1 , M_G2 , M_G3 , M_G4 , M_G5 , M_G6, " + _
                " L_T1 , L_T2 , L_T3 , L_T4 , L_T5 , L_T6 , L_V1 , L_V2 , L_V3 , L_V4 , L_V5 , L_V6 , L_M1 , L_M2 , L_M3 , L_M4 , L_M5 , L_M6 , L_P1 , L_P2 , L_P3 , L_P4 , L_P5 , L_P6 , L_G1 , L_G2 , L_G3 , L_G4 , L_G5 , L_G6 , L_Q1 , L_Q2 , L_Q3 , L_Q4 , L_Q5 ," + _
                " L_M_V1 , L_M_V2 , L_M_V3 , L_M_V4 , L_M_V5 , L_M_V6 , L_M_M1 , L_M_M2 , L_M_M3 , L_M_M4 , L_M_M5 , L_M_M6 , L_M_P1 , L_M_P2 , L_M_P3 , L_M_P4 , L_M_P5 , L_M_P6 ,L_M_G1 , L_M_G2 , L_M_G3 , L_M_G4 , L_M_G5 , L_M_G6, " + _
                " K0 , K1 , K2 , K3 , K4 , K5, " + _
                " T_METHOD  ) " + _
                " values ( " & ID.ToString() & ", 0 ,0, " +
                " 0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0,0  ,0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0, " + _
                " 0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0,0  ,0,0,0,0,0,0, " + _
                " 0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0,0  ,0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0, " + _
                " 0,0,0,0,0,0   ,0,0,0,0,0,0   ,0,0,0,0,0,0  ,0,0,0,0,0,0, " + _
                " 1,1,1,1,1,1, " + _
                " 0)"
            TvMain.QueryExec(q1)
            dtAn = TvMain.QuerySelect("select * from analizer_cfg where id_bd =" & ID.ToString())
        End If

        EditAnalizerConfig1.Attach(dtAn.Rows(0), False)

    End Sub

    Private Sub ReloadMasks(ByVal mmask As Integer, ByVal hmask As Integer, ByVal dmask As Integer, ByVal tmask As Integer)

        Dim q As String = ""

        Dim prev1 As Integer
        Dim prev2 As Integer
        Dim prev3 As Integer
        Dim prev4 As Integer
        prev1 = cmbMaskM.SelectedValue
        prev2 = cmbMaskH.SelectedValue
        prev3 = cmbMaskD.SelectedValue
        prev4 = cmbMaskT.SelectedValue


        q = "select * from masks where id_type=1 order by cname"
        Dim m1dt As DataTable
        m1dt = TvMain.QuerySelect(q)
        cmbMaskM.DisplayMember = "cname"
        cmbMaskM.ValueMember = "id_mask"
        cmbMaskM.DataSource = m1dt

        q = "select * from masks where id_type=2 order by cname"
        Dim m2dt As DataTable
        m2dt = TvMain.QuerySelect(q)
        cmbMaskT.DisplayMember = "cname"
        cmbMaskT.ValueMember = "id_mask"
        cmbMaskT.DataSource = m2dt


        q = "select * from masks where id_type=3 order by cname"
        Dim m3dt As DataTable
        m3dt = TvMain.QuerySelect(q)
        cmbMaskH.DisplayMember = "cname"
        cmbMaskH.ValueMember = "id_mask"
        cmbMaskH.DataSource = m3dt

        q = "select * from masks where id_type=4 order by cname"
        Dim m4dt As DataTable
        m4dt = TvMain.QuerySelect(q)
        cmbMaskD.DisplayMember = "cname"
        cmbMaskD.ValueMember = "id_mask"
        cmbMaskD.DataSource = m4dt

        cmbMaskM.SelectedValue = prev1
        cmbMaskH.SelectedValue = prev2
        cmbMaskD.SelectedValue = prev3
        cmbMaskT.SelectedValue = prev4

        If mmask <> 0 Then
            cmbMaskM.SelectedValue = mmask
        End If

        If hmask <> 0 Then
            cmbMaskH.SelectedValue = hmask
        End If
        If dmask <> 0 Then
            cmbMaskD.SelectedValue = dmask
        End If
        If tmask <> 0 Then
            cmbMaskT.SelectedValue = tmask
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Dim ok As Boolean
        ok = True
      
        Dim s As String
        Dim qry As String
        s = "update BBUILDINGS set  CSHORT='" & txtName.Text & "'" '  where id_BU in ( select id_bu from bdevices where id_bd=" & ID.ToString() & ")"
        s = s & ",  CADDRESS='" & txtcaddress.Text & "' " ' where id_BU in ( select id_bu from bdevices where id_bd=" & ID.ToString() & ")"
        s = s & ",  FULLADDRESS='" & txtFULLADDRESS.Text & "'" ' where id_BU in ( select id_bu from bdevices where id_bd=" & ID.ToString() & ")"
      
        Try
            If Not cmbGRP.SelectedValue Is Nothing Then
                s = s & ",id_grp = " + cmbGRP.SelectedValue.ToString
            Else

                MsgBox("Следует задать группу")
                ok = False
            End If
        Catch ex As Exception
            ok = False
        End Try



        Try

            s = s & ", cfio1='" + txtcfio1.Text + "',cfio2='" + txtcfio2.Text + "',cphone1='" + txtcphone1.Text + "' ,cphone2='" + txtphone2.Text + "'"
            
            If Not cmbWHO.SelectedValue Is Nothing Then
                s = s & ",  id_who = " + cmbWHO.SelectedValue.ToString

            End If


            s = s & "  where id_bu=" + ID_BU.ToString
            TvMain.QueryExec(s)

        Catch ex As Exception

        End Try



        s = "update bdevices set "
        If Not cmbSchema.SelectedValue Is Nothing Then
            s = s & " scheme_name='" + cmbSchema.SelectedValue.ToString + "'"
        Else
            s = s & " scheme_name=''"
        End If

        If cmbDanSchema.SelectedIndex >= 0 Then
            s = s & ", das_id=" + cmbDanSchema.SelectedValue.ToString
        Else
            s = s & ", das_id=null "
        End If




        If Not cmbSRV.SelectedValue Is Nothing Then
            s = s & ", srv_ip_id=" + cmbSRV.SelectedValue.ToString
        Else
            s = s & ", srv_ip_id=null"
        End If


        If Not cmbDevtype.SelectedValue Is Nothing Then
            s = s & ",  id_dev = " + cmbDevtype.SelectedValue.ToString

        End If

        If Not cmbMaskM.SelectedValue Is Nothing Then
            s = s & ",  id_mask_curr = " + cmbMaskM.SelectedValue.ToString

        End If

        If Not cmbMaskH.SelectedValue Is Nothing Then
            s = s & ",  id_mask_hour = " + cmbMaskH.SelectedValue.ToString

        End If

        If Not cmbMaskD.SelectedValue Is Nothing Then
            s = s & ", id_mask_24 = " + cmbMaskD.SelectedValue.ToString

        End If

        If Not cmbMaskT.SelectedValue Is Nothing Then
            s = s & ",  id_mask_sum = " + cmbMaskT.SelectedValue.ToString
        End If


        If chkHideRow.Checked Then

            s = s & ", hiderow=1 "
        Else
            s = s & ", hiderow=0  "
        End If
        If -50 <= txtHourShift.Value And txtHourShift.Value <= 50 Then
            s = s & ", hourshift=" & txtHourShift.Value.ToString
        End If

        If txtLINKED.Text <> "" Then
            If IsNumeric(txtLINKED.Text) Then
                s = s & ", LINKED_ID_BD=" & txtLINKED.Text
            End If
        End If

        If chkCOLDWATER.Checked Then

            s = s & ", ColdWater=1 "
        Else
            s = s & ", ColdWater=0  "
        End If

        If chkIP.Checked Then

            s = s & ", NPQUERY=1 "
        Else
            s = s & ", NPQUERY=0 "
        End If

        s = s & " , NZCOLS='" + txtNZCOLS.Text + "'"
        s = s & " , L_NZCOLS='" + txtL_NZCOLS.Text + "'"

        s = s & ", REPMASKFILEH='" & txtH_SH.Text & "' "


        s = s & ", REPMASKSHEETH='" & txtH_PAGE.Text & "' "


        s = s & ", CXLSFILE='" & txtD_SH.Text & "'  "


        s = s & ", CSHEET='" & txtD_PAGE.Text & "'  where id_bd =" & ID.ToString()
        TvMain.QueryExec(s)



        If dt.Rows.Count > 0 Then
            If pnlPLANCALL.IsOK() Then
                pnlPLANCALL.Save()


                Dim dr As DataRow
                dr = dt.Rows(0)

                qry = "update plancall set minrepeat=" + dr("MINREPEAT").ToString + ", nmaxcall=" + dr("NMAXCALL").ToString + ",cstatus=" + dr("CSTATUS").ToString + ", ccurr=" + dr("CCURR").ToString + ",chour=" + dr("CHOUR").ToString + ",c24=" + dr("C24").ToString + ",csum=" + dr("CSUM").ToString + ",  numhour=" + dr("NUMHOUR").ToString + ",num24=" + dr("NUM24").ToString + ", icall=" + dr("ICALL").ToString + ",icall24=" + dr("ICALL24").ToString + ",icallcurr=" + dr("ICALLCURR").ToString + ",icallsum=" + dr("ICALLSUM").ToString & _
                 ",dnexthour=" + TvMain.OracleDate(dr("dnexthour")) + ",  dnext24=" + TvMain.OracleDate(dr("dnext24")) + ",dnextcurr=" + TvMain.OracleDate(dr("dnextcurr")) + ",dnextsum=" + TvMain.OracleDate(dr("dnextsum")) + _
                 ",dbegcurr=" + TvMain.OracleDate(dr("dbegcurr")) + " where id_bd=" & ID.ToString()

                TvMain.QueryExec(qry)

            Else
                ok = False
            End If
        End If

        If dtBModems.Rows.Count > 0 Then
            If pnlBModems.IsOK() Then
                pnlBModems.Save()


                Dim dr As DataRow
                dr = dtBModems.Rows(0)

                qry = "update bmodems set CSPEED=" + dr("CSPEED").ToString + ", CDATABIT=" + dr("CDATABIT").ToString + ",CPARITY='" + dr("CPARITY").ToString + "',CSTOPBITS=" + dr("CSTOPBITS").ToString + ",CTOWNCODE='" + dr("CTOWNCODE").ToString + "',  CPHONE='" + dr("CPHONE").ToString + "',CPREFPHONE='" + dr("CPREFPHONE").ToString + "'  where id_bd =" & ID.ToString()

                TvMain.QueryExec(qry)

                qry = "update bdevices set npip='" + dr("npip").ToString + "'"

                qry = qry & ", nppassword='" + dr("nppassword").ToString + "'"

                qry = qry & ", ipport='" + dr("ipport").ToString + "'"

                qry = qry & ", callerid='" + dr("callerid").ToString + "'"

                qry = qry & ", transport=" + dr("transport").ToString + " where id_bd =" & ID.ToString()

                TvMain.QueryExec(qry)

            Else
                ok = False
            End If
        End If


        If dtContract.Rows.Count > 0 Then
            If pnlContract.IsOK() Then
                pnlContract.Save()

                qry = ""

                Dim dr As DataRow
                dr = dtContract.Rows(0)
                For Each fld As DataColumn In dtContract.Columns
                    If fld.ColumnName.ToLower <> "id_bd" Then
                        If qry <> "" Then
                            qry = qry & ","
                        End If
                        qry = qry & fld.ColumnName + "='" + dr(fld.ColumnName).ToString + "' "
                    End If
                Next

                qry = "update contract set " & qry & " where id_bd=" & ID.ToString()
                TvMain.QueryExec(qry)
            Else
                ok = False
            End If
        End If


        If dtAn.Rows.Count > 0 Then
            EditAnalizerConfig1.Save()
            qry = ""
            qry = "update analizer_cfg set ANALIZENODE=" & dtAn.Rows(0)("ANALIZENODE").ToString
            qry = qry & ", OPENSYSTEM=" & dtAn.Rows(0)("OPENSYSTEM").ToString
            qry = qry & ", T1=" & dtAn.Rows(0)("T1").ToString
            qry = qry & ", T2=" & dtAn.Rows(0)("T2").ToString
            qry = qry & ", T3=" & dtAn.Rows(0)("T3").ToString
            qry = qry & ", T4=" & dtAn.Rows(0)("T4").ToString
            qry = qry & ", T5=" & dtAn.Rows(0)("T5").ToString
            qry = qry & ", T6=" & dtAn.Rows(0)("T6").ToString
            qry = qry & ", V1=" & dtAn.Rows(0)("V1").ToString
            qry = qry & ", V2=" & dtAn.Rows(0)("V2").ToString
            qry = qry & ", V3=" & dtAn.Rows(0)("V3").ToString
            qry = qry & ", V4=" & dtAn.Rows(0)("V4").ToString
            qry = qry & ", V5=" & dtAn.Rows(0)("V5").ToString
            qry = qry & ", V6=" & dtAn.Rows(0)("V6").ToString
            qry = qry & ", M1=" & dtAn.Rows(0)("M1").ToString
            qry = qry & ", M2=" & dtAn.Rows(0)("M2").ToString
            qry = qry & ", M3=" & dtAn.Rows(0)("M3").ToString
            qry = qry & ", M4=" & dtAn.Rows(0)("M4").ToString
            qry = qry & ", M5=" & dtAn.Rows(0)("M5").ToString
            qry = qry & ", M6=" & dtAn.Rows(0)("M6").ToString
            qry = qry & ", P1=" & dtAn.Rows(0)("P1").ToString
            qry = qry & ", P2=" & dtAn.Rows(0)("P2").ToString
            qry = qry & ", P3=" & dtAn.Rows(0)("P3").ToString
            qry = qry & ", P4=" & dtAn.Rows(0)("P4").ToString
            qry = qry & ", P5=" & dtAn.Rows(0)("P5").ToString
            qry = qry & ", P6=" & dtAn.Rows(0)("P6").ToString
            qry = qry & ", G1=" & dtAn.Rows(0)("G1").ToString
            qry = qry & ", G2=" & dtAn.Rows(0)("G2").ToString
            qry = qry & ", G3=" & dtAn.Rows(0)("G3").ToString
            qry = qry & ", G4=" & dtAn.Rows(0)("G4").ToString
            qry = qry & ", G5=" & dtAn.Rows(0)("G5").ToString
            qry = qry & ", G6=" & dtAn.Rows(0)("G6").ToString

            qry = qry & ", M_V1=" & dtAn.Rows(0)("M_V1").ToString
            qry = qry & ", M_V2=" & dtAn.Rows(0)("M_V2").ToString
            qry = qry & ", M_V3=" & dtAn.Rows(0)("M_V3").ToString
            qry = qry & ", M_V4=" & dtAn.Rows(0)("M_V4").ToString
            qry = qry & ", M_V5=" & dtAn.Rows(0)("M_V5").ToString
            qry = qry & ", M_V6=" & dtAn.Rows(0)("M_V6").ToString
            qry = qry & ", M_M1=" & dtAn.Rows(0)("M_M1").ToString
            qry = qry & ", M_M2=" & dtAn.Rows(0)("M_M2").ToString
            qry = qry & ", M_M3=" & dtAn.Rows(0)("M_M3").ToString
            qry = qry & ", M_M4=" & dtAn.Rows(0)("M_M4").ToString
            qry = qry & ", M_M5=" & dtAn.Rows(0)("M_M5").ToString
            qry = qry & ", M_M6=" & dtAn.Rows(0)("M_M6").ToString
            qry = qry & ", M_P1=" & dtAn.Rows(0)("M_P1").ToString
            qry = qry & ", M_P2=" & dtAn.Rows(0)("M_P2").ToString
            qry = qry & ", M_P3=" & dtAn.Rows(0)("M_P3").ToString
            qry = qry & ", M_P4=" & dtAn.Rows(0)("M_P4").ToString
            qry = qry & ", M_P5=" & dtAn.Rows(0)("M_P5").ToString
            qry = qry & ", M_P6=" & dtAn.Rows(0)("M_P6").ToString
            qry = qry & ", M_G1=" & dtAn.Rows(0)("M_G1").ToString
            qry = qry & ", M_G2=" & dtAn.Rows(0)("M_G2").ToString
            qry = qry & ", M_G3=" & dtAn.Rows(0)("M_G3").ToString
            qry = qry & ", M_G4=" & dtAn.Rows(0)("M_G4").ToString
            qry = qry & ", M_G5=" & dtAn.Rows(0)("M_G5").ToString
            qry = qry & ", M_G6=" & dtAn.Rows(0)("M_G6").ToString



            qry = qry & ", Q1=" & dtAn.Rows(0)("Q1").ToString
            qry = qry & ", Q2=" & dtAn.Rows(0)("Q2").ToString
            qry = qry & ", Q3=" & dtAn.Rows(0)("Q3").ToString
            qry = qry & ", Q4=" & dtAn.Rows(0)("Q4").ToString
            qry = qry & ", Q5=" & dtAn.Rows(0)("Q5").ToString

            qry = qry & ", L_T1=" & dtAn.Rows(0)("L_T1").ToString
            qry = qry & ", L_T2=" & dtAn.Rows(0)("L_T2").ToString
            qry = qry & ", L_T3=" & dtAn.Rows(0)("L_T3").ToString
            qry = qry & ", L_T4=" & dtAn.Rows(0)("L_T4").ToString
            qry = qry & ", L_T5=" & dtAn.Rows(0)("L_T5").ToString
            qry = qry & ", L_T6=" & dtAn.Rows(0)("L_T6").ToString
            qry = qry & ", L_V1=" & dtAn.Rows(0)("L_V1").ToString
            qry = qry & ", L_V2=" & dtAn.Rows(0)("L_V2").ToString
            qry = qry & ", L_V3=" & dtAn.Rows(0)("L_V3").ToString
            qry = qry & ", L_V4=" & dtAn.Rows(0)("L_V4").ToString
            qry = qry & ", L_V5=" & dtAn.Rows(0)("L_V5").ToString
            qry = qry & ", L_V6=" & dtAn.Rows(0)("L_V6").ToString
            qry = qry & ", L_M1=" & dtAn.Rows(0)("L_M1").ToString
            qry = qry & ", L_M2=" & dtAn.Rows(0)("L_M2").ToString
            qry = qry & ", L_M3=" & dtAn.Rows(0)("L_M3").ToString
            qry = qry & ", L_M4=" & dtAn.Rows(0)("L_M4").ToString
            qry = qry & ", L_M5=" & dtAn.Rows(0)("L_M5").ToString
            qry = qry & ", L_M6=" & dtAn.Rows(0)("L_M6").ToString
            qry = qry & ", L_P1=" & dtAn.Rows(0)("L_P1").ToString
            qry = qry & ", L_P2=" & dtAn.Rows(0)("L_P2").ToString
            qry = qry & ", L_P3=" & dtAn.Rows(0)("L_P3").ToString
            qry = qry & ", L_P4=" & dtAn.Rows(0)("L_P4").ToString
            qry = qry & ", L_P5=" & dtAn.Rows(0)("L_P5").ToString
            qry = qry & ", L_P6=" & dtAn.Rows(0)("L_P6").ToString
            qry = qry & ", L_G1=" & dtAn.Rows(0)("L_G1").ToString
            qry = qry & ", L_G2=" & dtAn.Rows(0)("L_G2").ToString
            qry = qry & ", L_G3=" & dtAn.Rows(0)("L_G3").ToString
            qry = qry & ", L_G4=" & dtAn.Rows(0)("L_G4").ToString
            qry = qry & ", L_G5=" & dtAn.Rows(0)("L_G5").ToString
            qry = qry & ", L_G6=" & dtAn.Rows(0)("L_G6").ToString

            qry = qry & ", L_M_V1=" & dtAn.Rows(0)("L_M_V1").ToString
            qry = qry & ", L_M_V2=" & dtAn.Rows(0)("L_M_V2").ToString
            qry = qry & ", L_M_V3=" & dtAn.Rows(0)("L_M_V3").ToString
            qry = qry & ", L_M_V4=" & dtAn.Rows(0)("L_M_V4").ToString
            qry = qry & ", L_M_V5=" & dtAn.Rows(0)("L_M_V5").ToString
            qry = qry & ", L_M_V6=" & dtAn.Rows(0)("L_M_V6").ToString
            qry = qry & ", L_M_M1=" & dtAn.Rows(0)("L_M_M1").ToString
            qry = qry & ", L_M_M2=" & dtAn.Rows(0)("L_M_M2").ToString
            qry = qry & ", L_M_M3=" & dtAn.Rows(0)("L_M_M3").ToString
            qry = qry & ", L_M_M4=" & dtAn.Rows(0)("L_M_M4").ToString
            qry = qry & ", L_M_M5=" & dtAn.Rows(0)("L_M_M5").ToString
            qry = qry & ", L_M_M6=" & dtAn.Rows(0)("L_M_M6").ToString
            qry = qry & ", L_M_P1=" & dtAn.Rows(0)("L_M_P1").ToString
            qry = qry & ", L_M_P2=" & dtAn.Rows(0)("L_M_P2").ToString
            qry = qry & ", L_M_P3=" & dtAn.Rows(0)("L_M_P3").ToString
            qry = qry & ", L_M_P4=" & dtAn.Rows(0)("L_M_P4").ToString
            qry = qry & ", L_M_P5=" & dtAn.Rows(0)("L_M_P5").ToString
            qry = qry & ", L_M_P6=" & dtAn.Rows(0)("L_M_P6").ToString
            qry = qry & ", L_M_G1=" & dtAn.Rows(0)("L_M_G1").ToString
            qry = qry & ", L_M_G2=" & dtAn.Rows(0)("L_M_G2").ToString
            qry = qry & ", L_M_G3=" & dtAn.Rows(0)("L_M_G3").ToString
            qry = qry & ", L_M_G4=" & dtAn.Rows(0)("L_M_G4").ToString
            qry = qry & ", L_M_G5=" & dtAn.Rows(0)("L_M_G5").ToString
            qry = qry & ", L_M_G6=" & dtAn.Rows(0)("L_M_G6").ToString



            qry = qry & ", L_Q1=" & dtAn.Rows(0)("L_Q1").ToString
            qry = qry & ", L_Q2=" & dtAn.Rows(0)("L_Q2").ToString
            qry = qry & ", L_Q3=" & dtAn.Rows(0)("L_Q3").ToString
            qry = qry & ", L_Q4=" & dtAn.Rows(0)("L_Q4").ToString
            qry = qry & ", L_Q5=" & dtAn.Rows(0)("L_Q5").ToString


            qry = qry & ", K0=" & dtAn.Rows(0)("K0").ToString.Replace(",", ".")
            qry = qry & ", K1=" & dtAn.Rows(0)("K1").ToString.Replace(",", ".")
            qry = qry & ", K2=" & dtAn.Rows(0)("K2").ToString.Replace(",", ".")
            qry = qry & ", K3=" & dtAn.Rows(0)("K3").ToString.Replace(",", ".")
            'qry = qry & ", K4=" & dtAn.Rows(0)("K4").ToString.Replace(",", ".")
            'qry = qry & ", K5=" & dtAn.Rows(0)("K5").ToString.Replace(",", ".")
            qry = qry & ", lK0=" & dtAn.Rows(0)("LK0").ToString.Replace(",", ".")
            qry = qry & ", lK1=" & dtAn.Rows(0)("LK1").ToString.Replace(",", ".")
            qry = qry & ", lK2=" & dtAn.Rows(0)("LK2").ToString.Replace(",", ".")
            qry = qry & ", lK3=" & dtAn.Rows(0)("LK3").ToString.Replace(",", ".")
            'qry = qry & ", lK4=" & dtAn.Rows(0)("LK4").ToString.Replace(",", ".")
            'qry = qry & ", lK5=" & dtAn.Rows(0)("LK5").ToString.Replace(",", ".")
            qry = qry & ", T_METHOD=" & dtAn.Rows(0)("T_METHOD").ToString
            qry = qry & ", SEZON=" & dtAn.Rows(0)("SEZON").ToString

            qry = qry & " where id_bd=" & dtAn.Rows(0)("ID_BD").ToString
            TvMain.QueryExec(qry)
        End If

        If ok Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MsgBox("Информация сохранена не полностью." & vbCrLf & "Проверьте првильность ввода данных!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Ошибка")
        End If

    End Sub

    Private Sub TextBoxPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ConfigForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim q As String = ""

        q = "select * from whogive order by cname"
        Dim tdt As DataTable
        tdt = TvMain.QuerySelect(q)
        cmbWHO.DisplayMember = "cname"
        cmbWHO.ValueMember = "id_who"
        cmbWHO.DataSource = tdt


        q = "select name from devschema order by name"
        Dim sdt As DataTable
        sdt = TvMain.QuerySelect(q)
        cmbSchema.DisplayMember = "name"
        cmbSchema.ValueMember = "name"
        cmbSchema.DataSource = sdt

        q = "select name,das_id from danschema order by name"
        Dim cdt As DataTable
        cdt = TvMain.QuerySelect(q)
        cmbDanSchema.DisplayMember = "name"
        cmbDanSchema.ValueMember = "das_id"
        cmbDanSchema.DataSource = cdt

        q = "select * from bgroups order by cgrpnm"
        Dim gdt As DataTable
        gdt = TvMain.QuerySelect(q)
        cmbGRP.DisplayMember = "cgrpnm"
        cmbGRP.ValueMember = "id_grp"
        cmbGRP.DataSource = gdt

        q = "select * from devices order by cdevname"
        Dim ddt As DataTable
        ddt = TvMain.QuerySelect(q)
        cmbDevtype.DisplayMember = "cdevname"
        cmbDevtype.ValueMember = "id_dev"
        cmbDevtype.DataSource = ddt


        q = "select * from masks where id_type=1 order by cname"
        Dim m1dt As DataTable
        m1dt = TvMain.QuerySelect(q)
        cmbMaskM.DisplayMember = "cname"
        cmbMaskM.ValueMember = "id_mask"
        cmbMaskM.DataSource = m1dt

        q = "select * from masks where id_type=2 order by cname"
        Dim m2dt As DataTable
        m2dt = TvMain.QuerySelect(q)
        cmbMaskT.DisplayMember = "cname"
        cmbMaskT.ValueMember = "id_mask"
        cmbMaskT.DataSource = m2dt


        q = "select * from masks where id_type=3 order by cname"
        Dim m3dt As DataTable
        m3dt = TvMain.QuerySelect(q)
        cmbMaskH.DisplayMember = "cname"
        cmbMaskH.ValueMember = "id_mask"
        cmbMaskH.DataSource = m3dt

        q = "select * from masks where id_type=4 order by cname"
        Dim m4dt As DataTable
        m4dt = TvMain.QuerySelect(q)
        cmbMaskD.DisplayMember = "cname"
        cmbMaskD.ValueMember = "id_mask"
        cmbMaskD.DataSource = m4dt


        q = "select * from ipaddr order by terminal"
        Dim srvdt As DataTable
        srvdt = TvMain.QuerySelect(q)
        cmbSRV.DisplayMember = "terminal"
        cmbSRV.ValueMember = "id_ip"
        cmbSRV.DataSource = srvdt

    End Sub

    Private Sub txtD_SH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtH_PAGE_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtH_SH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdSelH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelH.Click
        Dim f As frmSelXLS
        f = New frmSelXLS
        f.subdir = "HMasks\" + cmbDevtype.Text
        f.lastFile = txtH_SH.Text
        f.lastPage = txtH_PAGE.Text
        If f.ShowDialog() = DialogResult.OK Then
            txtH_SH.Text = f.cmbXLS.Text
            txtH_PAGE.Text = f.cmbPage.Text
        End If
        f = Nothing

    End Sub

    Private Sub cmdSelD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelD.Click
        Dim f As frmSelXLS
        f = New frmSelXLS
        f.subdir = "DMasks\" + cmbDevtype.Text
        f.lastFile = txtD_SH.Text
        f.lastPage = txtD_PAGE.Text
        If f.ShowDialog() = DialogResult.OK Then
            txtD_SH.Text = f.cmbXLS.Text
            txtD_PAGE.Text = f.cmbPage.Text
        End If
        f = Nothing
    End Sub

    Private Sub cmdEditH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditH.Click


        Dim ff As String

        ff = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\") & "\HMasks\" + cmbDevtype.Text + "\" & txtH_SH.Text

        If File.Exists(ff) Then
            Process.Start(ff)
        End If
    End Sub

    Private Sub cmdEditD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditD.Click
        Dim ff As String

        ff = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\") & "\DMasks\" + cmbDevtype.Text + "\" & txtD_SH.Text
        If File.Exists(ff) Then
            Process.Start(ff)
        End If

    End Sub

    Private Sub cmbDevtype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDevtype.SelectedIndexChanged
        Dim q As String
        q = "select * from masks where id_type=1 and id_dev=" + cmbDevtype.SelectedValue.ToString + " order by cname"
        Dim m1dt As DataTable
        m1dt = TvMain.QuerySelect(q)
        cmbMaskM.DisplayMember = "cname"
        cmbMaskM.ValueMember = "id_mask"
        cmbMaskM.DataSource = m1dt

        q = "select * from masks where id_type=2 and id_dev=" + cmbDevtype.SelectedValue.ToString + " order by cname"
        Dim m2dt As DataTable
        m2dt = TvMain.QuerySelect(q)
        cmbMaskT.DisplayMember = "cname"
        cmbMaskT.ValueMember = "id_mask"
        cmbMaskT.DataSource = m2dt


        q = "select * from masks where id_type=3 and id_dev=" + cmbDevtype.SelectedValue.ToString + " order by cname"
        Dim m3dt As DataTable
        m3dt = TvMain.QuerySelect(q)
        cmbMaskH.DisplayMember = "cname"
        cmbMaskH.ValueMember = "id_mask"
        cmbMaskH.DataSource = m3dt

        q = "select * from masks where id_type=4 and id_dev=" + cmbDevtype.SelectedValue.ToString + " order by cname"
        Dim m4dt As DataTable
        m4dt = TvMain.QuerySelect(q)
        cmbMaskD.DisplayMember = "cname"
        cmbMaskD.ValueMember = "id_mask"
        cmbMaskD.DataSource = m4dt
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim s As String
        

        s = InputBox("Название:", "Создание маски мгновенного архива", "Текущие." + txtName.Text)
        If s <> "" Then

            Dim mid As Integer
            mid = AddNewMask(s, 1, cmbDevtype.SelectedValue, cmbMaskM.SelectedValue)
            If mid <> 0 Then
                Dim f As frmSetupGrid
                f = New frmSetupGrid
                f.txtName.Text = s
                f.ptype = 1
                f.mask_id = mid
                f.TVMain = Me.TvMain
                f.ShowDialog()
                f = Nothing
                ReloadMasks(mid, 0, 0, 0)
            Else
                MsgBox("Имя маски уже определено для такого типа  прибора")
            End If

        End If
    End Sub

    Private Function AddNewMask(ByVal Name As String, ByVal id_type As Integer, ByVal id_dev As Integer, ByVal prevmask_id As Integer) As Integer
        Dim ID As Int32
        Try
            Dim s As String
            s = "insert into Masks(id_mask,cname,id_type) values(Masks_seq.nextval,'" + Name + "'," + id_type.ToString + ")"
            If Not TvMain.QueryExec(s) Then
                Return 0
            End If


            s = "select masks_seq.currval id from dual"
            Dim ddd As DataTable
            ddd = TvMain.QuerySelect(s)
            ID = ddd.Rows(0)("ID")
        Catch ex As Exception
            Return 0
        End Try

        Try
            Dim s As String
            s = "update Masks set   id_dev = " + id_dev.ToString + "   where id_mask=" + ID.ToString
            TvMain.QueryExec(s)

        Catch ex As Exception

        End Try


        If prevmask_id >= 0 Then
            Dim pdt As DataTable
            pdt = TvMain.QuerySelect("select * from masksline where colhidden=0 and id_mask=" + prevmask_id.ToString())
            Dim pi As Integer
            For pi = 0 To pdt.Rows.Count - 1
                Try
                    Dim s As String
                    s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'" + pdt.Rows(pi)("cfld") + "' ,'" + pdt.Rows(pi)("cheader") + "'," + pdt.Rows(pi)("colwidth").ToString() + ",'" + pdt.Rows(pi)("colformat") + "',0)"
                    TvMain.QueryExec(s)
                Catch ex As Exception
                End Try
            Next
        Else



            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q1'             ,'Q1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q2'             ,'Q2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'T1'             ,'T1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'T2'             ,'T2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DT12'           ,'DT12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'T3'             ,'T3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'T4'             ,'T4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'T5'             ,'T5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DT45'           ,'DT45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'T6'             ,'T6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V1'             ,'V1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V2'             ,'V2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DV12'           ,'DV12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V3'             ,'V3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V4'             ,'V4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V5'             ,'V5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DV45'           ,'DV45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V6'             ,'V6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'M1'             ,'M1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'M2'             ,'M2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DM12'           ,'DM12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'M3'             ,'M3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'M4'             ,'M4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'M5'             ,'M5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DM45'           ,'DM45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'M6'             ,'M6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'P1'             ,'P1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'P2'             ,'P2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'P3'             ,'P3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'P4'             ,'P4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'P5'             ,'P5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'P6'             ,'P6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'G1'             ,'G1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'G2'             ,'G2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'G3'             ,'G3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'G4'             ,'G4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'G5'             ,'G5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'G6'             ,'G6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TCOOL'          ,'TCOOL'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TCE1'           ,'TCE1'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TCE2'           ,'TCE2'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TSUM1'          ,'TSUM1'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TSUM2'          ,'TSUM2'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q1H'            ,'Q1H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q2H'            ,'Q2H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V1H'            ,'V1H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V2H'            ,'V2H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V4H'            ,'V4H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'V5H'            ,'V5H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DG12'           ,'DG12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DG45'           ,'DG45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DP12'           ,'DP12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DP45'           ,'DP45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q3'             ,'Q3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q4'             ,'Q4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'PATM'           ,'PATM'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'Q5'             ,'Q5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DQ12'           ,'DQ12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DQ45'           ,'DQ45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'PXB'            ,'PXB'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DQ'             ,'DQ'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'THOT'           ,'THOT'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DANS1'          ,'DANS1'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DANS2'          ,'DANS2'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DANS3'          ,'DANS3'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DANS4'          ,'DANS4'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DANS5'          ,'DANS5'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'DANS6'          ,'DANS6'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'OKTIME'         ,'OKTIME'         ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'WORKTIME'       ,'WORKTIME'       ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TAIR1'          ,'TAIR1'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + ID.ToString() + ",'TAIR2'          ,'TAIR2'          ,80,'N',0)"

                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
        End If
        Return (ID)
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim s As String
        s = InputBox("Название:", "Создание маски часового архива", "Часовые." + txtName.Text)
        If s <> "" Then
            Dim mid As Integer
            mid = AddNewMask(s, 3, cmbDevtype.SelectedValue, cmbMaskH.SelectedValue)
            If mid <> 0 Then
                Dim f As frmSetupGrid
                f = New frmSetupGrid
                f.txtName.Text = s
                f.TVMain = Me.TvMain
                f.ptype = 3
                f.mask_id = mid
                f.ShowDialog()
                ReloadMasks(0, mid, 0, 0)
            Else
                MsgBox("Имя маски уже определено для такого типа  архива")
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim s As String
        s = InputBox("Название:", "Создание маски суточного архива", "Суточные." + txtName.Text)
        If s <> "" Then

            Dim mid As Integer
            mid = AddNewMask(s, 4, cmbDevtype.SelectedValue, cmbMaskD.SelectedValue)
            If mid <> 0 Then
                Dim f As frmSetupGrid
                f = New frmSetupGrid
                f.txtName.Text = s
                f.ptype = 4
                f.mask_id = mid
                f.TVMain = Me.TvMain
                f.ShowDialog()
                ReloadMasks(0, 0, mid, 0)
            Else
                MsgBox("Имя маски уже определено для такого типа архива")
            End If

        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim s As String
        s = InputBox("Название:", "Создание маски итогового архива", "Итоговые." + txtName.Text)
        If s <> "" Then
            Dim mid As Integer
            mid = AddNewMask(s, 2, cmbDevtype.SelectedValue, cmbMaskT.SelectedValue)
            If mid <> 0 Then
                Dim f As frmSetupGrid
                f = New frmSetupGrid
                f.txtName.Text = s
                f.ptype = 2
                f.mask_id = mid
                f.TVMain = Me.TvMain
                f.ShowDialog()
                ReloadMasks(0, 0, 0, mid)
            Else
                MsgBox("Имя маски уже определено для такого типа архива")
            End If

        End If
    End Sub

    ' BOUNDS
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    Dim EditParam As Boolean = False
    Dim ptype As Integer = 1
    Dim sezon As Integer = 0

    Dim BoundsLoaded As Boolean = False

    Public Sub LoadData(ByVal newID As Integer)
        ID = newID
        If TvMain Is Nothing Then Exit Sub
        BoundsLoaded = False

        Dim cmd As OracleCommand




        Dim da As OracleDataAdapter
        Dim dt As DataTable
        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from valuebounds where sezon=" + sezon.ToString + " and id_bd=" + id.ToString + " and ptype=" + ptype.ToString + " order by  pname"
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()

        Dim pos_left As Integer = 5
        Dim pos_top As Integer = 5
        Dim lbl As Label
        Dim chk As CheckBox
        Dim txt As TextBox

        Dim i As Integer
        Dim lblToolTipInfo As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo
        myPanel.Controls.Clear()

        For i = 0 To dt.Rows.Count - 1
            Debug.Print(i.ToString + ": " + dt.Rows(i)("pname"))
            lbl = New Label
            lbl.Left = pos_left
            lbl.Top = pos_top
            lbl.Height = 22
            lbl.Width = 50
            lbl.TextAlign = ContentAlignment.MiddleLeft
            lbl.Text = dt.Rows(i)("pname") & " min:"
            lbl.Tag = dt.Rows(i)("pname")
            lbl.ForeColor = Color.Black
            lbl.BackColor = Color.White
            myPanel.Controls.Add(lbl)
            lbl.Show()

            txt = New TextBox
            txt.BorderStyle = BorderStyle.FixedSingle
            txt.Left = pos_left + 60
            txt.Top = pos_top
            txt.Height = 22
            txt.Width = 50
            txt.Text = ""
            txt.Tag = dt.Rows(i)("pname") + "_min"
            txt.ForeColor = Color.Blue
            txt.BackColor = Color.White
            lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("pname"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
            lblToolTipInfo.ToolTipText = "Минимум " & dt.Rows(i)("pname")
            'Me.UltraToolTipManager1.SetUltraToolTip(txt, lblToolTipInfo)
            myPanel.Controls.Add(txt)
            txt.Show()

            chk = New CheckBox

            chk.Left = pos_left + 120
            chk.Top = pos_top
            chk.Height = 20
            chk.Width = 20
            chk.Text = ""
            chk.Tag = "chk_" + dt.Rows(i)("pname") + "_min"
            chk.BackColor = Color.White
            lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("pname"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
            lblToolTipInfo.ToolTipText = "Проверять минимум для " & dt.Rows(i)("pname")
            'Me.UltraToolTipManager1.SetUltraToolTip(chk, lblToolTipInfo)
            myPanel.Controls.Add(chk)
            chk.Show()

            lbl = New Label
            lbl.Left = pos_left + 150
            lbl.Top = pos_top
            lbl.Height = 22
            lbl.Width = 50
            lbl.Text = "max:"
            lbl.TextAlign = ContentAlignment.MiddleRight
            lbl.Tag = dt.Rows(i)("pname")
            lbl.ForeColor = Color.Black
            lbl.BackColor = Color.White
            myPanel.Controls.Add(lbl)
            lbl.Show()

            txt = New TextBox
            txt.BorderStyle = BorderStyle.FixedSingle
            txt.Left = pos_left + 210
            txt.Top = pos_top
            txt.Height = 22
            txt.Width = 50
            txt.Text = ""
            txt.Tag = dt.Rows(i)("pname") + "_max"
            txt.ForeColor = Color.Red
            txt.BackColor = Color.White
            lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("pname"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
            lblToolTipInfo.ToolTipText = "Максимум " & dt.Rows(i)("pname")
            'Me.UltraToolTipManager1.SetUltraToolTip(txt, lblToolTipInfo)
            myPanel.Controls.Add(txt)
            txt.Show()


            chk = New CheckBox
            chk.Left = pos_left + 270
            chk.Top = pos_top
            chk.Height = 20
            chk.Width = 20
            chk.Text = ""
            chk.Tag = "chk_" + dt.Rows(i)("pname") + "_max"
            chk.BackColor = Color.White
            lblToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo(dt.Rows(i)("pname"), Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
            lblToolTipInfo.ToolTipText = "Проверять максимум для " & dt.Rows(i)("pname")
            myPanel.Controls.Add(chk)
            'Me.UltraToolTipManager1.SetUltraToolTip(chk, lblToolTipInfo)
            chk.Show()


            pos_top += 30
        Next
        ShowMinMax()
        BoundsLoaded = True

    End Sub

    Private Function GetByName(ByVal name As String) As Control

        Dim i As Integer
        For i = 0 To myPanel.Controls.Count - 1
            If Not myPanel.Controls.Item(i).Tag Is Nothing Then
                If myPanel.Controls.Item(i).Tag.ToString().ToLower & "" = name.ToLower Then
                    Return myPanel.Controls.Item(i)
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Sub ShowMinMax()
        Dim vdt As DataTable
        Dim cmd As OracleCommand

        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from valuebounds  where sezon=" + sezon.ToString + " and  id_bd=" + id.ToString + " and ptype=" + ptype.ToString()

        da.SelectCommand = cmd
        vdt = New DataTable

        da.Fill(vdt)
        cmd.Dispose()
        da.Dispose()

        Dim ctl As Control
        'Dim lbl As Label
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim i As Long
        For i = 0 To vdt.Rows.Count - 1




            ' If EditParam Then
            'ctl = GetByName(vdt.Rows(i)("pname"))
            'lbl = ctl

            ctl = GetByName(vdt.Rows(i)("pname") + "_min")
            If Not ctl Is Nothing Then
                txt = ctl
                txt.Text = vdt.Rows(i)("PMIN")
            End If

            ctl = GetByName(vdt.Rows(i)("pname") + "_max")
            If Not ctl Is Nothing Then
                txt = ctl
                txt.Text = vdt.Rows(i)("PMAX")
            End If

            ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_min")
            If Not ctl Is Nothing Then
                chk = ctl
                If vdt.Rows(i)("ISMIN") = 1 Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If
            End If

            ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_max")
            If Not ctl Is Nothing Then
                chk = ctl
                If vdt.Rows(i)("ISMAX") = 1 Then
                    chk.Checked = True
                Else
                    chk.Checked = False
                End If
            End If





        Next

    End Sub
    Public Function MyVal(ByVal S As String) As Double
        Return Val("0" & Replace(Replace(S, ",", "."), " ", ""))
    End Function

    Private Sub SaveParams()
        If boundsloaded Then
            Dim cmd2 As OracleCommand
            cmd2 = New OracleCommand
            cmd2.Connection = TvMain.dbconnect
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "update valuebounds set sezon=:SEZON,pmin=:PMIN,pmax=:PMAX,ismin=:ISMIN,ismax=:ISMAX where pname=:PNAME and id_bd=" + ID.ToString + " and ptype=" + ptype.ToString()
            cmd2.Parameters.Add("SEZON", OracleDbType.Int16)
            cmd2.Parameters.Add("PMIN", OracleDbType.Decimal)
            cmd2.Parameters.Add("PMAX", OracleDbType.Decimal)
            cmd2.Parameters.Add("ISMIN", OracleDbType.Int16)
            cmd2.Parameters.Add("ISMAX", OracleDbType.Int16)
            cmd2.Parameters.Add("PNAME", OracleDbType.Varchar2)

            Dim vdt As DataTable
            Dim cmd As OracleCommand

            Dim da As OracleDataAdapter
            da = New OracleDataAdapter
            cmd = New OracleCommand
            cmd.Connection = TvMain.dbconnect
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from valuebounds   where sezon=" + sezon.ToString + " and  id_bd=" + ID.ToString + " and ptype=" + ptype.ToString()

            da.SelectCommand = cmd
            vdt = New DataTable

            da.Fill(vdt)
            cmd.Dispose()
            da.Dispose()

            Dim ctl As Control

            Dim txt As TextBox
            Dim chk As CheckBox
            Dim i As Long
            Dim cnt As Integer

            cmd2.Parameters.Item("SEZON").Value = sezon

            For i = 0 To vdt.Rows.Count - 1
                cnt = 0


                ctl = GetByName(vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    cnt += 1
                    txt = ctl
                    'txt.Text = vdt.Rows(i)("PMIN")
                    cmd2.Parameters.Item("PMIN").Value = MyVal(txt.Text)
                End If

                ctl = GetByName(vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    cnt += 1
                    txt = ctl
                    'txt.Text = vdt.Rows(i)("PMAX")
                    cmd2.Parameters.Item("PMAX").Value = MyVal(txt.Text)
                End If

                ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_min")
                If Not ctl Is Nothing Then
                    cnt += 1
                    chk = ctl
                    If chk.Checked = True Then
                        cmd2.Parameters.Item("ISMIN").Value = 1

                    Else
                        cmd2.Parameters.Item("ISMIN").Value = 0
                    End If
                End If

                ctl = GetByName("chk_" + vdt.Rows(i)("pname") + "_max")
                If Not ctl Is Nothing Then
                    cnt += 1
                    chk = ctl
                    If chk.Checked = True Then
                        cmd2.Parameters.Item("ISMAX").Value = 1

                    Else
                        cmd2.Parameters.Item("ISMAX").Value = 0
                    End If
                End If

                'ctl = GetByName(vdt.Rows(i)("pname"))
                'If Not ctl Is Nothing Then
                cmd2.Parameters.Item("PNAME").Value = vdt.Rows(i)("pname")

                '  нашли все контролы для параметра
                If cnt = 4 Then
                    If cmd2.Parameters.Item("ISMAX").Value = 0 And cmd2.Parameters.Item("ISMIN").Value = 0 And cmd2.Parameters.Item("PMAX").Value = 0 And cmd2.Parameters.Item("PMIN").Value = 0 Then
                        Try
                            TvMain.QueryExec("delete from valuebounds  where sezon=" + sezon.ToString + " and  id_bd=" + ID.ToString + " and ptype=" + ptype.ToString() + " and pname='" + vdt.Rows(i)("pname") + "'")
                        Catch ex As Exception

                        End Try


                    Else
                        Try

                            cmd2.ExecuteNonQuery()
                            Debug.Print(cmd2.CommandText)
                        Catch ex As Exception

                        End Try

                    End If

                End If




            Next
            cmd2.Dispose()
        End If
    End Sub

    Private Sub chkMoment_BeforeCheckStateChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles chkMoment.BeforeCheckStateChanged
        If chkMoment.Checked Then
            SaveParams()
        End If

    End Sub




    Private Sub chkMoment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoment.CheckedChanged

        If chkMoment.Checked Then

            chkHour.Checked = False
            chkDay.Checked = False
            ptype = 1
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
            LoadData(id)
        End If

    End Sub

    Private Sub chkHour_BeforeCheckStateChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles chkHour.BeforeCheckStateChanged
        If chkHour.Checked Then
            SaveParams()
        End If

    End Sub

    Private Sub chkHour_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHour.CheckedChanged

        If chkHour.Checked Then

            chkMoment.Checked = False
            chkDay.Checked = False
            ptype = 3
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
            LoadData(id)
        End If

    End Sub

    Private Sub chkDay_BeforeCheckStateChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles chkDay.BeforeCheckStateChanged
        If chkDay.Checked Then
            SaveParams()
        End If

    End Sub

    Private Sub chkDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDay.CheckedChanged

        If chkDay.Checked Then

            chkMoment.Checked = False
            chkHour.Checked = False
            ptype = 4
            LoadData(id)
        End If
        If chkMoment.Checked = False And chkHour.Checked = False And chkDay.Checked = False Then
            chkMoment.Checked = True
            ptype = 1
            LoadData(id)
        End If

    End Sub

    Private Sub cmdParams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdParams.Click
        Dim f As frmSelectParam2Bounds
        f = New frmSelectParam2Bounds
        f.sezon = sezon
        f.id_bd = id.ToString
        f.id_ptype = ptype.ToString
        f.TvMain = TvMain

        If f.ShowDialog() = DialogResult.OK Then
            LoadData(id)
        End If
        f = Nothing
    End Sub

    Private Sub cmdValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValues.Click
        SaveParams()
        LoadData(id)
    End Sub


    Private Sub chkSEZON_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSEZON.CheckedChanged
        SaveParams()
        If chkSEZON.Checked Then
            sezon = 0
        Else
            sezon = 1
        End If
        LoadData(id)

    End Sub



    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub TabPage7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage7.Click
        'LoadData(ID)
    End Sub

    Private Sub TabPage7_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage7.Enter
        LoadData(ID)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Name = "TabPage7" Then
            LoadData(ID)
        End If
    End Sub

    Private Sub TabControl1_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.TabIndexChanged

    End Sub

    Private Sub TabPage7_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage7.GotFocus
        If Not BoundsLoaded Then
            LoadData(ID)
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.TVMain = Me.TvMain
        f.id_bd = ID
        f.ptype = 1
        f.mask_id = cmbMaskM.SelectedValue
        f.txtName.Text = cmbMaskM.Text
        f.ShowDialog()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.TVMain = Me.TvMain
        f.mask_id = cmbMaskH.SelectedValue
        f.id_bd = ID
        f.ptype = 3
        f.txtName.Text = cmbMaskH.Text
        f.ShowDialog()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.TVMain = Me.TvMain
        f.id_bd = ID
        f.ptype = 4
        f.mask_id = cmbMaskD.SelectedValue
        f.txtName.Text = cmbMaskD.Text
        f.ShowDialog()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As frmSetupGrid
        f = New frmSetupGrid
        f.TVMain = Me.TvMain
        f.id_bd = ID
        f.ptype = 2
        f.mask_id = cmbMaskT.SelectedValue
        f.txtName.Text = cmbMaskT.Text
        f.ShowDialog()
    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub btnSetup_Click(sender As Object, e As EventArgs) Handles btnSetup.Click
        If TvMain.LoadTVD(ID) Then
            TvMain.TVD.DoSpecificSetup(TvMain)
            TvMain.DeviceClose()
        End If
    End Sub
End Class