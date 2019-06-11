Imports System.Windows.Forms
Public Class frmSelectParam2Mask
    Public mask_id As String
    Public TVMain As STKTVMain.TVMain
    Public id_ptype As Integer = 1

    Private Sub FillParam()
        chkParams.Items.Clear()

        ' температуры
        chkParams.Items.Add("T1", False)
        chkParams.Items.Add("T2", False)
        chkParams.Items.Add("DT12", False)
        chkParams.Items.Add("T3", False)
        chkParams.Items.Add("DT34", False)
        chkParams.Items.Add("T4", False)
        chkParams.Items.Add("T5", False)
        chkParams.Items.Add("DT45", False)
        chkParams.Items.Add("T6", False)
        chkParams.Items.Add("DT56", False)
        chkParams.Items.Add("TCOOL", False)
        chkParams.Items.Add("TCE1", False)
        chkParams.Items.Add("TCE2", False)
        chkParams.Items.Add("THOT", False)
        chkParams.Items.Add("TAIR1", False)
        chkParams.Items.Add("TAIR2", False)

        ' объемы
        chkParams.Items.Add("V1", False)
        chkParams.Items.Add("V2", False)
        chkParams.Items.Add("DV12", False)
        chkParams.Items.Add("V3", False)
        chkParams.Items.Add("V4", False)
        chkParams.Items.Add("V5", False)
        chkParams.Items.Add("DV45", False)
        chkParams.Items.Add("V6", False)
        chkParams.Items.Add("V1H", False)
        chkParams.Items.Add("V2H", False)
        chkParams.Items.Add("V4H", False)
        chkParams.Items.Add("V5H", False)

        ' массы
        chkParams.Items.Add("M1", False)
        chkParams.Items.Add("M2", False)
        chkParams.Items.Add("DM12", False)
        chkParams.Items.Add("M3", False)
        chkParams.Items.Add("DM34", False)
        chkParams.Items.Add("M4", False)
        chkParams.Items.Add("M5", False)
        chkParams.Items.Add("DM45", False)
        chkParams.Items.Add("M6", False)
        chkParams.Items.Add("DM56", False)

        ' давления
        chkParams.Items.Add("P1", False)
        chkParams.Items.Add("P2", False)
        chkParams.Items.Add("DP12", False)
        chkParams.Items.Add("P3", False)
        chkParams.Items.Add("P4", False)
        chkParams.Items.Add("P5", False)
        chkParams.Items.Add("DP45", False)
        chkParams.Items.Add("P6", False)
        chkParams.Items.Add("PATM", False)
        chkParams.Items.Add("PXB", False)

        ' расходы мгновенных
        chkParams.Items.Add("G1", False)
        chkParams.Items.Add("G2", False)
        chkParams.Items.Add("DG12", False)
        chkParams.Items.Add("G3", False)
        chkParams.Items.Add("G4", False)
        chkParams.Items.Add("G5", False)
        chkParams.Items.Add("DG45", False)
        chkParams.Items.Add("G6", False)


        ' энергия
        chkParams.Items.Add("Q1", False)
        chkParams.Items.Add("Q2", False)
        chkParams.Items.Add("DQ12", False)
        chkParams.Items.Add("Q3", False)
        chkParams.Items.Add("Q4", False)
        chkParams.Items.Add("Q5", False)
        chkParams.Items.Add("Q6", False)
        chkParams.Items.Add("DQ45", False)
        chkParams.Items.Add("DQ", False)
        chkParams.Items.Add("Q1H", False)
        chkParams.Items.Add("Q2H", False)

        ' времена
        chkParams.Items.Add("TSUM1", False)
        chkParams.Items.Add("TSUM2", False)
        chkParams.Items.Add("OKTIME", False)
        chkParams.Items.Add("ERRTIME", False)
        chkParams.Items.Add("WORKTIME", False)
        chkParams.Items.Add("OKTIME2", False)
        chkParams.Items.Add("ERRTIME2", False)

        ' нештатки
        chkParams.Items.Add("HC", False)
        chkParams.Items.Add("HC_1", False)
        chkParams.Items.Add("HC_2", False)
        chkParams.Items.Add("HC_CODE", False)

        chkParams.Items.Add("DANS1", False)
        chkParams.Items.Add("DANS2", False)
        chkParams.Items.Add("DANS3", False)
        chkParams.Items.Add("DANS4", False)
        chkParams.Items.Add("DANS5", False)
        chkParams.Items.Add("DANS6", False)

        If id_ptype <> 2 Then
            chkParams.Items.Add("L_CNT", False)

            chkParams.Items.Add("L_TAIR1", False)
            chkParams.Items.Add("L_Q1", False)
            chkParams.Items.Add("L_Q2", False)
            chkParams.Items.Add("L_Q3", False)
            chkParams.Items.Add("L_Q4", False)



            chkParams.Items.Add("L_T1", False)
            chkParams.Items.Add("L_T2", False)
            chkParams.Items.Add("L_T3", False)
            chkParams.Items.Add("L_T4", False)
            chkParams.Items.Add("L_T5", False)
            chkParams.Items.Add("L_T6", False)

            chkParams.Items.Add("L_V1", False)
            chkParams.Items.Add("L_V2", False)
            chkParams.Items.Add("L_V3", False)
            chkParams.Items.Add("L_V4", False)
            chkParams.Items.Add("L_V5", False)
            chkParams.Items.Add("L_V6", False)



            chkParams.Items.Add("L_P1", False)
            chkParams.Items.Add("L_P2", False)
            chkParams.Items.Add("L_P3", False)
            chkParams.Items.Add("L_P4", False)
            chkParams.Items.Add("L_P5", False)
            chkParams.Items.Add("L_P6", False)



            chkParams.Items.Add("L_G1", False)
            chkParams.Items.Add("L_G2", False)
            chkParams.Items.Add("L_G3", False)
            chkParams.Items.Add("L_G4", False)
            chkParams.Items.Add("L_G5", False)
            chkParams.Items.Add("L_G6", False)
        End If

    End Sub



    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim j As Long
        Dim maxseq As Integer = 0
        Dim dt As DataTable
        dt = TVMain.QuerySelect("select max(sequence) ms from masksline where id_mask=" + mask_id.ToString())
        If dt.Rows.Count > 0 Then
            Try
                maxseq = dt.Rows(0)("ms")
            Catch ex As Exception
                maxseq = 0
            End Try
        End If

        For j = 0 To chkParams.CheckedItems.Count - 1
            maxseq += 1
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden,sequence) values( masksline_seq.nextval," + mask_id.ToString() + ",'" + chkParams.CheckedItems.Item(j) + "','" + chkParams.CheckedItems.Item(j) + "'             ,80,'N',0," + maxseq.ToString() + ")"
                TVMain.QueryExec(s)
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        Next
        Me.Hide()
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Hide()
    End Sub

    Private Sub frmSelectParam_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillParam()
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