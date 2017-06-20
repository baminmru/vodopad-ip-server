Imports System
Imports System.IO
Imports System.Data

Public Class frmSetupModem

    Private id_ip As Integer = 0

    Private Sub frmSetupModem_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dt As DataTable
        Me.Text = "Настройка модемов для станции: " & Environment.MachineName
        dt = TvMain.QuerySelect("select * from ipaddr where terminal='" & Environment.MachineName & "'")
        If dt.Rows.Count = 0 Then
            TvMain.QueryExec("insert into ipaddr(id_ip,cipaddr,MACHINE,TERMINAL) values(ipaddr_Seq.nextval,'127.0.0.1','" & Environment.MachineName & "','" & Environment.MachineName & "')")

            dt = TvMain.QuerySelect("select * from ipaddr where terminal='" & Environment.MachineName & "'")
        End If


        If dt.Rows.Count > 0 Then
            id_ip = dt.Rows(0)("id_ip")

        End If

        RefreshM()
       RefreshCOM()



    End Sub

    Private Sub lstCom_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstCom.SelectedIndexChanged
        Dim dt As DataTable
        dt = TvMain.QuerySelect("select * from comports where id_cp=" & lstCom.SelectedValue.ToString)
        txtPort.Text = dt.Rows(0)("COMPORT")
        If dt.Rows(0)("CTYPECALL").ToString.ToUpper() = "T" Then
            OptTone.Checked = True
        ElseIf dt.Rows(0)("CTYPECALL").ToString.ToUpper() = "P" Then
            optPulse.Checked = True
        Else
            optGSM.Checked = True
        End If

        dt = TvMain.QuerySelect("select terminal,comport,cinit,comports.id_cp,modems.id_modem,connections.id_conn,comports.id_ip from connections join comports on connections.id_cp=comports.id_cp left join modems on connections.id_modem = modems.id_modem join  ipaddr on comports.id_ip =ipaddr.id_ip  where comports.id_cp=" & lstCom.SelectedValue.ToString)
        If dt.Rows.Count > 0 Then
            cmbM.SelectedValue = dt.Rows(0)("id_modem")
        End If

    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If lstCom.SelectedIndex >= 0 Then
            Dim s As String
            Dim ct As String
            If OptTone.Checked Then
                ct = "T"
            ElseIf optPulse.Checked Then
                ct = "P"
            Else
                ct = "G"
            End If
            s = "update comports set COMPORT='" & txtPort.Text & "',CTYPECALL='" & ct & "' where ID_CP=" & lstCom.SelectedValue.ToString
            TvMain.QueryExec(s)

            If cmbM.SelectedIndex >= 0 Then
                s = "delete from connections where id_cp=" & lstCom.SelectedValue.ToString
                TvMain.QueryExec(s)

                s = "insert into connections(id_conn,id_ip,id_cp,id_modem,connected,id_md) values(connections_seq.nextval," & id_ip.ToString & "," & lstCom.SelectedValue.ToString & "," & cmbM.SelectedValue.ToString & ",1,2)"
                TvMain.QueryExec(s)
            End If

            RefreshCOM()
        End If
    End Sub

    Private Sub RefreshCOM()
        Dim dt As DataTable

        dt = TvMain.QuerySelect("select * from comports where id_ip=" & id_ip.ToString)
        lstCom.DisplayMember = "COMPORT"
        lstCom.ValueMember = "ID_CP"
        lstCom.DataSource = dt


        If lstCom.Items.Count > 0 Then
            lstCom.SelectedIndex = 0
        End If
    End Sub

    Private Sub RefreshM()
        Dim dt As DataTable
        Dim dt1 As DataTable

        dt = TvMain.QuerySelect("select * from modems")
        lstM.DisplayMember = "CSHORT"
        lstM.ValueMember = "ID_MODEM"
        lstM.DataSource = dt

        dt1 = TvMain.QuerySelect("select * from modems")
        cmbM.DisplayMember = "CSHORT"
        cmbM.ValueMember = "ID_MODEM"
        cmbM.DataSource = dt1


        If lstM.Items.Count > 0 Then
            lstM.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmdDelModem_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelModem.Click
        If lstCom.SelectedIndex >= 0 Then
            If MsgBox("Удалить запись о порте: " & lstCom.Text & "?", MsgBoxStyle.YesNo, "Подтвердите") = MsgBoxResult.Yes Then
                TvMain.QueryExec("delete from connections where id_cp=" & lstCom.SelectedValue.ToString)
                TvMain.QueryExec("delete from comports where id_cp=" & lstCom.SelectedValue.ToString())
                RefreshCOM()
            End If

        End If

    End Sub

    Private Sub cmdAddModem_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddModem.Click
        Dim s As String
        s = "insert into comports(id_cp,id_ip, COMPORT,CTYPECALL) values(comports_seq.nextval," & id_ip.ToString() & ",'new" & (lstCom.Items.Count + 1).ToString & "','T')"
        TvMain.QueryExec(s)
        RefreshCOM()
    End Sub

    Private Sub lstM_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstM.SelectedIndexChanged
        Dim dt As DataTable
        dt = TvMain.QuerySelect("select * from modems where id_modem=" & lstM.SelectedValue.ToString)
        txtShort.Text = dt.Rows(0)("CSHORT") & ""

        If TypeName(dt.Rows(0)("CFULL")) = "DBNull" Then
            txtFull.Text = dt.Rows(0)("CSHORT") & ""
        Else
            txtFull.Text = dt.Rows(0)("CFULL") & ""
        End If

        txtINI.Text = dt.Rows(0)("CINIT") & ""
    End Sub


    Private Sub cmdAddM_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddM.Click
        Dim s As String
        s = "insert into modems(id_modem,cshort, Cfull,CINIT,CDTR,CDSR) values(modems_seq.nextval,'new" & (lstM.Items.Count + 1).ToString & "','new" & (lstM.Items.Count + 1).ToString & "','at',1,1)"
        TvMain.QueryExec(s)
        RefreshM()
    End Sub

    Private Sub cmdDelM_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelM.Click
        If lstM.SelectedIndex >= 0 Then
            If MsgBox("Удалить запись о модеме: " & lstM.Text & "?", MsgBoxStyle.YesNo, "Подтвердите") = MsgBoxResult.Yes Then
                TvMain.QueryExec("delete from modems where id_modem=" & lstM.SelectedValue.ToString())
                RefreshM()
            End If

        End If
    End Sub

    
    Private Sub cmdSaveM_Click(sender As System.Object, e As System.EventArgs) Handles cmdSaveM.Click
        If lstM.SelectedIndex >= 0 Then
            Dim s As String

           
            s = "update modems set CSHORT='" & txtShort.Text & "',Cfull='" & txtFull.Text & "',Cinit='" & txtINI.Text & "' where ID_modem=" & lstM.SelectedValue.ToString
            TvMain.QueryExec(s)
            RefreshM()
        End If
    End Sub

    Private Sub cmdFreeModem_Click(sender As System.Object, e As System.EventArgs) Handles cmdFreeModem.Click
        If lstCom.SelectedIndex >= 0 Then
            Dim s As String
            
            s = "update comports set UsedUntil = null where ID_CP=" & lstCom.SelectedValue.ToString
            TvMain.QueryExec(s)

            
            RefreshCOM()
        End If
    End Sub
End Class