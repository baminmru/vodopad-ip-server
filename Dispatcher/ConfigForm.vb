Public Class ConfigForm
    Public TvMain As STKTVMain.TVMain
    Public ID As Int32
    Public BName As String = ""
    Dim dt As DataTable
    Dim dtBModems As DataTable
    Private Sub ConfigForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim cfg As STKTVMain.TVMain.ConfigStruct

        cfg = TvMain.GetConfigStructFromId_BD(ID)
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


        dtBModems = TvMain.QuerySelect("select bmodems.*,bdevices.npip, bdevices.nppassword, bdevices.npquery,  bdevices.transport, bdevices.ipport from bmodems join bdevices  on bmodems.id_bd=bdevices.id_bd  where bmodems.id_bd=" & ID.ToString())
        If dtBModems.Rows.Count = 1 Then
            pnlBModems.Attach(dtBModems.Rows(0), False)
        Else
            TvMain.QueryExec("insert into bmodems(id_modem,id_bd,cspeed,connectlimit,PARAMLIMIT) values(bmodems_seq.nextval," & ID.ToString() & ",2400,60,15)")
            dtBModems = TvMain.QuerySelect("select bmodems.*,bdevices.npip, bdevices.nppassword, bdevices.npquery,  bdevices.transport, bdevices.ipport from bmodems join bdevices  on bmodems.id_bd=bdevices.id_bd  where bmodems.id_bd=" & ID.ToString())
            If dtBModems.Rows.Count = 1 Then
                pnlBModems.Attach(dtBModems.Rows(0), False)
            End If
        End If

        TextBoxID_BD.Text = ID
        TextBoxIP.Text = cfg.ip
        TextBoxPassword.Text = cfg.password
        TextBoxDevice.Text = cfg.device
        cmbTransport.Text = cfg.Transport
        txtPort.Text = cfg.ipport
        txtName.Text = BName
        Dim i As Integer
        i = cfg.NPQuery

        If i = 0 Then
            chkIP.Checked = False
        Else
            chkIP.Checked = True
        End If

        i = cfg.HideRow

        If i = 0 Then
            chkHideRow.Checked = False
        Else
            chkHideRow.Checked = True
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Dim ok As Boolean
        ok = True
        TvMain.setConfigToDB(Convert.ToInt32(TextBoxID_BD.Text), TextBoxIP.Text, TextBoxPassword.Text, TextBoxDevice.Text, chkIP.Checked, chkHideRow.Checked, cmbTransport.Text, txtPort.Text)
        Dim cmd As System.Data.OracleClient.OracleCommand
        cmd = New System.Data.OracleClient.OracleCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update BBUILDINGS set CSHORT='" & txtName.Text & "'  where id_BU in ( select id_bu from bdevices where id_bd=" & ID.ToString() & ")"
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

                cmd.CommandText = "update bmodems set CSPEED=" + dr("CSPEED").ToString + ", CDATABIT=" + dr("CDATABIT").ToString + ",CPARITY='" + dr("CPARITY").ToString + "',CSTOPBITS=" + dr("CSTOPBITS").ToString + ",CTOWNCODE='" + dr("CTOWNCODE").ToString + "',  CPHONE='" + dr("CPHONE").ToString + "',CPREFPHONE='" + dr("CPREFPHONE").ToString + "' " & _
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

        If ok Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MsgBox("Информация сохранена не полностью." & vbCrLf & "Проверьте првильность ввода данных!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Ошибка")
        End If

    End Sub

    
End Class