
Imports Oracle.DataAccess.Client

Public Class frmBounds
    Public id As Integer
    Dim EditParam As Boolean = False
    Dim ptype As Integer = 1
    Dim sezon As Integer = 0
    Dim bActivated As Boolean = False

    Public Sub LoadData(ByVal newID As Integer)
        id = newID


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
        da.Dispose()
        cmd.Dispose()

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
            Me.UltraToolTipManager1.SetUltraToolTip(txt, lblToolTipInfo)
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
            Me.UltraToolTipManager1.SetUltraToolTip(chk, lblToolTipInfo)
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
            Me.UltraToolTipManager1.SetUltraToolTip(txt, lblToolTipInfo)
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
            Me.UltraToolTipManager1.SetUltraToolTip(chk, lblToolTipInfo)
            chk.Show()


            pos_top += 30
        Next
        ShowMinMax()


    End Sub



    Private Sub frmSchema_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bActivated = True

    End Sub

    Private Sub frmSchema_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        bActivated = False
    End Sub

    Private Sub frmSchema_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmSchema_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData(id)
        LoadDogData()

    End Sub

    Private Sub LoadDogData()
        Dim conDT As DataTable
        conDT = TvMain.QuerySelect("SELECT COLUMN_NAME,COMMENTS FROM user_col_comments WHERE table_name = 'CONTRACT' and COLUMN_NAME like 'FLD%'")
        Dim dtContract As DataTable
        dtContract = TvMain.QuerySelect("select * from contract where id_bd=" & id.ToString())

        Dim i As Integer
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow


        Dim Item As DataRow
        Item = dtContract.Rows(0)

        For i = 0 To conDT.Rows.Count - 1
            If Item(conDT.Rows(i)("COLUMN_NAME")).GetType().Name = "DBNull" Then
                dr = dt.NewRow
                dr("Название") = conDT.Rows(i)("COMMENTS")
                dr("Значение") = "-"
                dt.Rows.Add(dr)
            Else
                If Item(conDT.Rows(i)("COLUMN_NAME")) & "" = "" Then
                    dr = dt.NewRow
                    dr("Название") = conDT.Rows(i)("COMMENTS")
                    dr("Значение") = "-"
                    dt.Rows.Add(dr)
                Else
                    dr = dt.NewRow
                    dr("Название") = conDT.Rows(i)("COMMENTS")
                    dr("Значение") = Item(conDT.Rows(i)("COLUMN_NAME")) & ""
                    dt.Rows.Add(dr)
                End If
            End If
        Next

        gv.DataSource = dt
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
        da.Dispose()
        cmd.Dispose()

    End Sub


    Private Sub SaveParams()

        Dim cmd2 As OracleCommand
        cmd2 = New OracleCommand
        cmd2.Connection = TvMain.dbconnect
        cmd2.CommandType = CommandType.Text
        cmd2.CommandText = "update valuebounds set sezon=:SEZON,pmin=:PMIN,pmax=:PMAX,ismin=:ISMIN,ismax=:ISMAX where pname=:PNAME and id_bd=" + id.ToString + " and ptype=" + ptype.ToString()
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
        cmd.CommandText = "select * from valuebounds   where sezon=" + sezon.ToString + " and  id_bd=" + id.ToString + " and ptype=" + ptype.ToString()

        da.SelectCommand = cmd
        vdt = New DataTable

        da.Fill(vdt)

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
                        TvMain.QueryExec("delete from valuebounds  where sezon=" + sezon.ToString + " and  id_bd=" + id.ToString + " and ptype=" + ptype.ToString() + " and pname='" + vdt.Rows(i)("pname") + "'")
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
        da.Dispose()
        cmd.Dispose()
        cmd2.Dispose()

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
        f.id_bd = id.ToString
        f.id_ptype = ptype.ToString
        f.Sezon = sezon

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

    Private Sub gv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gv.CellContentClick

    End Sub
End Class