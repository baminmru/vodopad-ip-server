Imports System.IO
Public Class ConfigForm
    Public TvMain As STKTVMain.TVMain
    Public ID As Int32
    Public ID_BU As Int32
    Public BName As String = ""
    Dim dt As DataTable
    Dim dtBModems As DataTable
    Dim dtContract As DataTable
    Dim dtB As DataTable

    Private Sub ConfigForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim dtMain As DataTable
        Dim q As String = ""

        q = q + "select bdevices.id_bd,bdevices.id_bu,bdevices.id_dev,bdevices.hiderow,bdevices.npquery,"
        q = q + " bbuildings.id_who,bbuildings.id_grp,bbuildings.cshort,bbuildings.CFIO1,bbuildings.CPHONE1,bbuildings.CFIO2,bbuildings.CPHONE2,bbuildings.CADDRESS,"
        q = q + " cgrpnm,cdevname,devschema.DS_id,devschema.name sname,whogive.cname wname from bdevices "
        q = q + " join bbuildings on bdevices.id_bu=bbuildings.id_bu"
        q = q + " join devices on bdevices.id_dev = devices.id_dev "
        q = q + " join bgroups on bbuildings.id_grp = bgroups.id_grp"
        q = q + " left join devschema on bdevices.scheme_name = devschema.name"
        q = q + " left join whogive on bbuildings.id_who=whogive.id_who  where bdevices.id_bd=" & ID.ToString() + " order by cshort"
        dtMain = TvMain.QuerySelect(q)



        dt = TvMain.QuerySelect("select * from PLANCALL where id_bd=" & ID.ToString())
        If dt.Rows.Count = 1 Then
            pnlPlanCall.Attach(dt.Rows(0), False)
        Else
            TvMain.QueryExec("insert into plancall(id_bd,cstatus,ccurr,c24,chour,csum,icall,icall24,icallcurr,icallsum,numhour,num24) values(" & ID.ToString() & ",1,0,0,0,0,60,24,60,1440,3,3)")
            dt = TvMain.QuerySelect("select * from PLANCALL where id_bd=" & ID.ToString())
            If dt.Rows.Count = 1 Then
                pnlPlanCall.Attach(dt.Rows(0), False)
            End If
        End If



        dtBModems = TvMain.QuerySelect("select bmodems.*,bdevices.npip, bdevices.nppassword, bdevices.npquery,  bdevices.transport, bdevices.ipport from bmodems join bdevices on bmodems.id_bd=bdevices.id_bd  where bmodems.id_bd=" & ID.ToString())
        If dtBModems.Rows.Count = 1 Then
            pnlBModems.Attach(dtBModems.Rows(0), False)
        Else
            TvMain.QueryExec("insert into bmodems(id_modem,id_bd,cspeed,connectlimit,PARAMLIMIT) values(bmodems_seq.nextval," & ID.ToString() & ",2400,60,15)")
            dtBModems = TvMain.QuerySelect("select bmodems.*,bdevices.npip, bdevices.nppassword, bdevices.npquery,  bdevices.transport, bdevices.ipport from bmodems join bdevices  on bmodems.id_bd=bdevices.id_bd  where bmodems.id_bd=" & ID.ToString())
            If dtBModems.Rows.Count = 1 Then
                pnlBModems.Attach(dtBModems.Rows(0), False)
            End If
        End If

        dtContract = TvMain.QuerySelect("select * from contract where id_bd=" & ID.ToString())
        If dtContract.Rows.Count = 1 Then
            pnlContract.Attach(dtContract.Rows(0), False)
        Else
            TvMain.QueryExec("insert into contract(id_bd) values(" & ID.ToString() & ")")
            dtContract = TvMain.QuerySelect("select * from contract where id_bd=" & ID.ToString())
            If dtContract.Rows.Count = 1 Then
                pnlContract.Attach(dtContract.Rows(0), False)
            End If
        End If

 


       
        dtB = TvMain.QuerySelect("select bdevices.*,id_who from bdevices  join bbuildings on bdevices.id_bu= bbuildings.id_bu where id_bd =" & ID.ToString())

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


        txtName.Text = dtMain.Rows(0)("cshort") & ""
        txtcaddress.Text = dtMain.Rows(0)("caddress") & ""
        txtcfio1.Text = dtMain.Rows(0)("cfio1") & ""
        txtcphone1.Text = dtMain.Rows(0)("cphone1") & ""
        txtcfio2.Text = dtMain.Rows(0)("cfio2") & ""
        txtphone2.Text = dtMain.Rows(0)("cphone2") & ""
        cmbDevtype.SelectedValue = dtMain.Rows(0)("id_dev")
        cmbGRP.SelectedValue = dtMain.Rows(0)("id_grp")
        Try
            cmbSchema.SelectedValue = dtMain.Rows(0)("sname")
        Catch ex As Exception

        End Try
        Try
            cmbWHO.SelectedValue = dtMain.Rows(0)("id_who")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Dim ok As Boolean
        ok = True
        'TvMain.setConfigToDB(Convert.ToInt32(TextBoxID_BD.Text), TextBoxIP.Text, TextBoxPassword.Text, cmbDevType.Text, chkIP.Checked, chkHideRow.Checked, cmbTransport.Text, txtPort.Text)
        Dim cmd As System.Data.OracleClient.OracleCommand
        cmd = New System.Data.OracleClient.OracleCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update BBUILDINGS set id_who=" + cmbWHO.SelectedValue.ToString + ", CSHORT='" & txtName.Text & "'  where id_BU in ( select id_bu from bdevices where id_bd=" & ID.ToString() & ")"
        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()

        Catch

        End Try


        Try
            Dim s As String
            s = "update bbuildings set    cshort='" + txtName.Text + "' ,cfio1='" + txtcfio1.Text + "',cfio2='" + txtcfio2.Text + "',cphone1='" + txtcphone1.Text + "' ,cphone2='" + txtphone2.Text + "', id_who = " + cmbWHO.SelectedValue.ToString + " , id_grp = " + cmbGRP.SelectedValue.ToString + "   where id_bu=" + id_bu.ToString
            TvMain.QueryExec(s)

            s = "update bdevices set   scheme_name='" + cmbSchema.SelectedValue.ToString + "' , id_dev = " + cmbDevtype.SelectedValue.ToString + "   where id_bd=" + ID.ToString
            TvMain.QueryExec(s)

        Catch ex As Exception

        End Try

        If chkHideRow.Checked Then

            cmd.CommandText = "update bdevices set hiderow=1  where id_bd =" & ID.ToString()
        Else
            cmd.CommandText = "update bdevices set hiderow=0  where id_bd =" & ID.ToString()
        End If

        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
        Catch
        End Try

        If chkIP.Checked Then

            cmd.CommandText = "update bdevices set NPQUERY=1  where id_bd =" & ID.ToString()
        Else
            cmd.CommandText = "update bdevices set NPQUERY=0  where id_bd =" & ID.ToString()
        End If

        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
        Catch
        End Try


        cmd.CommandText = "update bdevices set REPMASKFILEH='" & txtH_SH.Text & "'  where id_bd =" & ID.ToString()
        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
        Catch
        End Try

        cmd.CommandText = "update bdevices set REPMASKSHEETH='" & txtH_PAGE.Text & "'  where id_bd =" & ID.ToString()
        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
        Catch
        End Try

        cmd.CommandText = "update bdevices set CXLSFILE='" & txtD_SH.Text & "'  where id_bd =" & ID.ToString()
        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
        Catch
        End Try

        cmd.CommandText = "update bdevices set CSHEET='" & txtD_PAGE.Text & "'  where id_bd =" & ID.ToString()
        cmd.Connection = TvMain.dbconnect()
        Try
            cmd.ExecuteNonQuery()
        Catch
        End Try



        If dt.Rows.Count > 0 Then
            If pnlPlanCall.IsOK() Then
                pnlPlanCall.Save()

                cmd = New System.Data.OracleClient.OracleCommand
                cmd.CommandType = CommandType.Text
                Dim dr As DataRow
                dr = dt.Rows(0)

                cmd.CommandText = "update plancall set cstatus=" + dr("CSTATUS").ToString + ", ccurr=" + dr("CCURR").ToString + ",chour=" + dr("CHOUR").ToString + ",c24=" + dr("C24").ToString + ",csum=" + dr("CSUM").ToString + ",  numhour=" + dr("NUMHOUR").ToString + ",num24=" + dr("NUM24").ToString + ", icall=" + dr("ICALL").ToString + ",icall24=" + dr("ICALL24").ToString + ",icallcurr=" + dr("ICALLCURR").ToString + ",icallsum=" + dr("ICALLSUM").ToString & _
                ",dnexthour=" + TvMain.OracleDate(dr("dnexthour")) + ",  dnext24=" + TvMain.OracleDate(dr("dnext24")) + ",dnextcurr=" + TvMain.OracleDate(dr("dnextcurr")) + ",dnextsum=" + TvMain.OracleDate(dr("dnextsum")) + _
                " where id_bd=" & ID.ToString()



                cmd.Connection = TvMain.dbconnect()
                Try
                    cmd.ExecuteNonQuery()

                Catch

                End Try



                cmd.CommandText = "update plancall set cstatus=" + dr("CSTATUS").ToString + ", ccurr=" + dr("CCURR").ToString + ",chour=" + dr("CHOUR").ToString + ",c24=" + dr("C24").ToString + ",csum=" + dr("CSUM").ToString + ",  numhour=" + dr("NUMHOUR").ToString + ",num24=" + dr("NUM24").ToString + ", icall=" + dr("ICALL").ToString + ",icall24=" + dr("ICALL24").ToString + ",icallcurr=" + dr("ICALLCURR").ToString + ",icallsum=" + dr("ICALLSUM").ToString & _
             ",dnexthour=" + TvMain.OracleDate(dr("dnexthour")) + ",  dnext24=" + TvMain.OracleDate(dr("dnext24")) + ",dnextcurr=" + TvMain.OracleDate(dr("dnextcurr")) + ",dnextsum=" + TvMain.OracleDate(dr("dnextsum")) + _
             " where id_bd=" & ID.ToString()



                cmd.Connection = TvMain.dbconnect()
                Try
                    cmd.ExecuteNonQuery()

                Catch

                End Try
            Else
                ok = False
            End If
        End If

        If dtBModems.Rows.Count > 0 Then
            If pnlBModems.IsOK() Then
                pnlBModems.Save()

                cmd = New System.Data.OracleClient.OracleCommand
                cmd.CommandType = CommandType.Text
                Dim dr As DataRow
                dr = dtBModems.Rows(0)

                cmd.CommandText = "update bmodems set CSPEED=" + dr("CSPEED").ToString + ", CDATABIT=" + dr("CDATABIT").ToString + ",CPARITY='" + dr("CPARITY").ToString + "',CSTOPBITS=" + dr("CSTOPBITS").ToString + ",CTOWNCODE='" + dr("CTOWNCODE").ToString + "',  CPHONE='" + dr("CPHONE").ToString + "',CPREFPHONE='" + dr("CPREFPHONE").ToString + "' " & _
                " where id_bd=" & ID.ToString()
                cmd.Connection = TvMain.dbconnect()
                Try
                    cmd.ExecuteNonQuery()

                Catch

                End Try


               
                cmd.CommandText = "update bdevices set npip='" + dr("npip").ToString + "' where id_bd =" & ID.ToString()
              

                cmd.Connection = TvMain.dbconnect()
                Try
                    cmd.ExecuteNonQuery()
                Catch
                End Try


                cmd.CommandText = "update bdevices set ipport='" + dr("ipport").ToString + "' where id_bd =" & ID.ToString()


                cmd.Connection = TvMain.dbconnect()
                Try
                    cmd.ExecuteNonQuery()
                Catch
                End Try

                cmd.CommandText = "update bdevices set transport=" + dr("transport").ToString + " where id_bd =" & ID.ToString()


                cmd.Connection = TvMain.dbconnect()
                Try
                    cmd.ExecuteNonQuery()
                Catch
                End Try
            Else
                ok = False
            End If
        End If


        If dtContract.Rows.Count > 0 Then
            If pnlContract.IsOK() Then
                pnlContract.Save()

                cmd = New System.Data.OracleClient.OracleCommand
                cmd.CommandType = CommandType.Text
                Dim dr As DataRow
                dr = dtContract.Rows(0)
                For Each fld As DataColumn In dtContract.Columns
                    If fld.ColumnName.ToLower <> "id_bd" Then
                        cmd.CommandText = "update contract set " + fld.ColumnName + "='" + dr(fld.ColumnName).ToString + "' " & _
                        " where id_bd=" & ID.ToString()

                        cmd.Connection = TvMain.dbconnect()
                        Try
                            cmd.ExecuteNonQuery()

                        Catch

                        End Try

                    End If
                Next


            Else
                ok = False
            End If
        End If

        If ok Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
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
    End Sub

    Private Sub txtD_SH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtD_SH.TextChanged

    End Sub

    Private Sub txtH_PAGE_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtH_PAGE.TextChanged

    End Sub

    Private Sub txtH_SH_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtH_SH.TextChanged

    End Sub

    Private Sub cmdSelH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelH.Click
        Dim f As frmSelXLS
        f = New frmSelXLS
        f.subdir = "HMasks\" + cmbDevType.Text
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtH_SH.Text = f.cmbXLS.Text
            txtH_PAGE.Text = f.cmbPage.Text
        End If
        f = Nothing

    End Sub

    Private Sub cmdSelD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelD.Click
        Dim f As frmSelXLS
        f = New frmSelXLS
        f.subdir = "DMasks\" + cmbDevType.Text
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtD_SH.Text = f.cmbXLS.Text
            txtD_PAGE.Text = f.cmbPage.Text
        End If
        f = Nothing
    End Sub

    Private Sub cmdEditH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditH.Click

        Dim ff As String

        ff = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\") & "\HMasks\" + cmbDevType.Text + "\" & txtH_SH.Text

        If File.Exists(ff) Then
            Process.Start(ff)
        End If
    End Sub

    Private Sub cmdEditD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditD.Click
        Dim ff As String

        ff = GetSetting("VIP", "REPORTGENXL", "MASKPATH", "C:\") & "\DMasks\" + cmbDevType.Text + "\" & txtD_SH.Text
        If File.Exists(ff) Then
            Process.Start(ff)
        End If

    End Sub
End Class