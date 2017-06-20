Imports Oracle.DataAccess.Client

Public Class frmSelectParam
    Public ds_id As String

    Private Sub FillParam()
        chkParams.Items.Clear()
        chkParams.Items.Add("Q1", False)
        chkParams.Items.Add("Q2", False)
        chkParams.Items.Add("Q3", False)
        chkParams.Items.Add("Q4", False)
        chkParams.Items.Add("Q5", False)
        chkParams.Items.Add("Q6", False)
        chkParams.Items.Add("DQ12", False)
        chkParams.Items.Add("DQ45", False)
        chkParams.Items.Add("Q1H", False)
        chkParams.Items.Add("Q2H", False)

        chkParams.Items.Add("T1", False)
        chkParams.Items.Add("T2", False)
        chkParams.Items.Add("DT12", False)
        chkParams.Items.Add("T3", False)
        chkParams.Items.Add("T4", False)
        chkParams.Items.Add("T5", False)
        chkParams.Items.Add("DT45", False)
        chkParams.Items.Add("T6", False)

        chkParams.Items.Add("V1", False)
        chkParams.Items.Add("V2", False)
        chkParams.Items.Add("DV12", False)
        chkParams.Items.Add("V3", False)
        chkParams.Items.Add("V4", False)
        chkParams.Items.Add("V5", False)
        chkParams.Items.Add("DV45", False)
        chkParams.Items.Add("V6", False)

        chkParams.Items.Add("M1", False)
        chkParams.Items.Add("M2", False)
        chkParams.Items.Add("DM12", False)
        chkParams.Items.Add("M3", False)
        chkParams.Items.Add("M4", False)
        chkParams.Items.Add("M5", False)
        chkParams.Items.Add("DM45", False)
        chkParams.Items.Add("M6", False)

        chkParams.Items.Add("P1", False)
        chkParams.Items.Add("P2", False)
        chkParams.Items.Add("P3", False)
        chkParams.Items.Add("P4", False)
        chkParams.Items.Add("P5", False)
        chkParams.Items.Add("P6", False)

        chkParams.Items.Add("G1", False)
        chkParams.Items.Add("G2", False)
        chkParams.Items.Add("G3", False)
        chkParams.Items.Add("G4", False)
        chkParams.Items.Add("G5", False)
        chkParams.Items.Add("G6", False)

        chkParams.Items.Add("TCOOL", False)
        chkParams.Items.Add("TCE1", False)
        chkParams.Items.Add("TCE2", False)
        chkParams.Items.Add("TSUM1", False)
        chkParams.Items.Add("TSUM2", False)
        
        chkParams.Items.Add("V1H", False)
        chkParams.Items.Add("V2H", False)
        chkParams.Items.Add("V4H", False)
        chkParams.Items.Add("V5H", False)
        chkParams.Items.Add("DG12", False)
        chkParams.Items.Add("DG45", False)
        chkParams.Items.Add("DP12", False)
        chkParams.Items.Add("DP45", False)
        
        chkParams.Items.Add("PATM", False)
        chkParams.Items.Add("PXB", False)
        chkParams.Items.Add("DQ", False)
        chkParams.Items.Add("THOT", False)
        chkParams.Items.Add("DANS1", False)
        chkParams.Items.Add("DANS2", False)
        chkParams.Items.Add("DANS3", False)
        chkParams.Items.Add("DANS4", False)
        chkParams.Items.Add("DANS5", False)
        chkParams.Items.Add("DANS6", False)
        chkParams.Items.Add("OKTIME", False)
        chkParams.Items.Add("ERRTIME", False)
        chkParams.Items.Add("WORKTIME", False)
        chkParams.Items.Add("OKTIME2", False)
        chkParams.Items.Add("ERRTIME2", False)
        chkParams.Items.Add("TAIR1", False)
        chkParams.Items.Add("TAIR2", False)

        chkParams.Items.Add("HCRAW", False)
        chkParams.Items.Add("HCRAW1", False)
        chkParams.Items.Add("HCRAW2", False)

    End Sub

    Public Sub LoadData(ByVal newid As String)
        ds_id = newid
        FillParam()
        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter
        Dim dt As DataTable
        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from devschemaparam where HIDEPARAM=0 and ds_id=" + ds_id
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()

        Dim i As Long
        Dim j As Long
        For i = 0 To dt.Rows.Count - 1
            j = chkParams.Items.IndexOf(dt.Rows(i)("name"))
            If j >= 0 Then
                chkParams.SetItemChecked(j, True)
            End If
        Next

    End Sub



    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = DialogResult.OK

        ' сохраняем состояние выбраных параметров
        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter
        Dim dt As DataTable
        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from devschemaparam where  ds_id=" + ds_id
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)

        cmd.Dispose()
        da.Dispose()

        ' 1 если параметр выбран и он есть в базе - HIDEPARAM=0
        ' 2 если параметр выбран и его нет в базе - вставка новой записи
        ' 3 если параметр не выбран и он есть в базе - HIDEPARAM=1

        Dim i As Long
        Dim j As Long
        Dim insertRow As Boolean
        For j = 0 To chkParams.CheckedItems.Count - 1
            insertRow = True
            For i = 0 To dt.Rows.Count - 1
                If chkParams.CheckedItems.Item(j) = dt.Rows(i)("name") Then
                    'HIDEPARAM=0
                    cmd = New OracleCommand
                    cmd.Connection = TvMain.dbconnect
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "update devschemaparam set HIDEPARAM=0 where name='" & dt.Rows(i)("name") & "' and ds_id=" + ds_id
                    cmd.ExecuteNonQuery()
                    insertRow = False
                End If


            Next

            If insertRow Then
                cmd = New OracleCommand
                cmd.Connection = TvMain.dbconnect
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "insert into devschemaparam (HIDEPARAM,ds_id,dsp_id,name,pos_left,pos_top) values(0," + ds_id + ",DEVSCHEMADATA_SEQ.nextval,'" & chkParams.CheckedItems.Item(j).ToString() & "',0,0)"
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        Next

        For i = 0 To dt.Rows.Count - 1

            If Not chkParams.CheckedItems.Contains(dt.Rows(i)("name")) Then
                'HIDEPARAM=0
                cmd = New OracleCommand
                cmd.Connection = TvMain.dbconnect
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "update devschemaparam set HIDEPARAM=1 where name='" & dt.Rows(i)("name") & "' and ds_id=" + ds_id
                cmd.ExecuteNonQuery()

            End If

        Next

        cmd.Dispose()
        Me.Hide()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub frmSelectParam_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim i As Integer
        For i = 0 To chkParams.Items.Count - 1
            chkParams.SetItemChecked(i, True)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer
        For i = 0 To chkParams.Items.Count - 1
            chkParams.SetItemChecked(i, False)
        Next
    End Sub
End Class