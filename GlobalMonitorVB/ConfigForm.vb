Public Class ConfigForm

    Public ID As Int32
    Public BName As String = ""
    Dim dt As DataTable

    Public Sub LoadData()
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

    Private Sub ConfigForm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MyfrmSetup = Nothing
    End Sub
    Private Sub ConfigForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
      
        LoadData()
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

        If ok Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            MsgBox("Информация сохранена не полностью." & vbCrLf & "Проверьте првильность ввода данных!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Ошибка")
        End If

    End Sub

    Private Sub TextBoxPassword_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ConfigForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class