
Public Class frmMasksEdit

    Public id As Integer
    Public id_type As Integer
    Public id_dev As Integer
    Private Sub frmMasks_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim tdt As DataTable
        tdt = TvMain.QuerySelect("select * from paramtype order by ctype")
        cmbType.DisplayMember = "ctype"
        cmbType.ValueMember = "id_type"
        cmbType.DataSource = tdt


        Dim ddt As DataTable
        ddt = TvMain.QuerySelect("select * from devices order by cdevname")
        cmbDev.DisplayMember = "cdevname"
        cmbDev.ValueMember = "id_dev"
        cmbDev.DataSource = ddt

        If id <> 0 Then
            cmbType.SelectedValue = id_type
            cmbDev.SelectedValue = id_dev

        End If
    End Sub




    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If id = 0 Then
            Try
                Dim s As String
                s = "insert into Masks(id_mask,cname) values(Masks_seq.nextval,'" + txtName.Text + "')"
                TvMain.QueryExec(s)
                Me.Close()

                s = "select masks_seq.currval id from dual"
                Dim ddd As DataTable
                ddd = TvMain.QuerySelect(s)
                id = ddd.Rows(0)("ID")
            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update Masks set   id_type = " + cmbType.SelectedValue.ToString + "   where id_mask=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update Masks set   id_dev = " + cmbDev.SelectedValue.ToString + "   where id_mask=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try




            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q1'             ,'Q1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q2'             ,'Q2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'T1'             ,'T1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'T2'             ,'T2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DT12'           ,'DT12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'T3'             ,'T3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'T4'             ,'T4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'T5'             ,'T5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DT45'           ,'DT45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'T6'             ,'T6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V1'             ,'V1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V2'             ,'V2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DV12'           ,'DV12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V3'             ,'V3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V4'             ,'V4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V5'             ,'V5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DV45'           ,'DV45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V6'             ,'V6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'M1'             ,'M1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'M2'             ,'M2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DM12'           ,'DM12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'M3'             ,'M3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'M4'             ,'M4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'M5'             ,'M5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DM45'           ,'DM45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'M6'             ,'M6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'P1'             ,'P1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'P2'             ,'P2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'P3'             ,'P3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'P4'             ,'P4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'P5'             ,'P5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'P6'             ,'P6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'G1'             ,'G1'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'G2'             ,'G2'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'G3'             ,'G3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'G4'             ,'G4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'G5'             ,'G5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'G6'             ,'G6'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TCOOL'          ,'TCOOL'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TCE1'           ,'TCE1'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TCE2'           ,'TCE2'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TSUM1'          ,'TSUM1'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TSUM2'          ,'TSUM2'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q1H'            ,'Q1H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q2H'            ,'Q2H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V1H'            ,'V1H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V2H'            ,'V2H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V4H'            ,'V4H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'V5H'            ,'V5H'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DG12'           ,'DG12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DG45'           ,'DG45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DP12'           ,'DP12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DP45'           ,'DP45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q3'             ,'Q3'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q4'             ,'Q4'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'PATM'           ,'PATM'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'Q5'             ,'Q5'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DQ12'           ,'DQ12'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DQ45'           ,'DQ45'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'PXB'            ,'PXB'            ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DQ'             ,'DQ'             ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'THOT'           ,'THOT'           ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DANS1'          ,'DANS1'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DANS2'          ,'DANS2'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DANS3'          ,'DANS3'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DANS4'          ,'DANS4'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DANS5'          ,'DANS5'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'DANS6'          ,'DANS6'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'OKTIME'         ,'OKTIME'         ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'WORKTIME'       ,'WORKTIME'       ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TAIR1'          ,'TAIR1'          ,80,'N',0)"
                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Try
                Dim s As String
                s = "insert into masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values( masksline_seq.nextval," + id.ToString() + ",'TAIR2'          ,'TAIR2'          ,80,'N',0)"

                TvMain.QueryExec(s)
            Catch ex As Exception
            End Try
            Me.Close()

        Else

            Try
                Dim s As String
                s = "update Masks set  cname='" + txtName.Text + "' , id_type = " + cmbType.SelectedValue.ToString + "   where id_mask=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update Masks set  cname='" + txtName.Text + "'   where id_mask=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update Masks set   id_type = " + cmbType.SelectedValue.ToString + "   where id_mask=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
            Try
                Dim s As String
                s = "update Masks set   id_dev = " + cmbDev.SelectedValue.ToString + "   where id_mask=" + id.ToString
                TvMain.QueryExec(s)

            Catch ex As Exception

            End Try
            Me.Close()
        End If


    End Sub




    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class