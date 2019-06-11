Imports System.Windows.Forms
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Oracle.DataAccess.Client

Public Class frmSetupGrid
    Public TVMain As STKTVMain.TVMain
   
    Public id_bd As Integer
    Public ptype As Integer
    Public mask_id As Integer
    Dim cdt As DataTable
    Dim dt As DataTable

    Dim InLoad As Boolean = False
    Public Sub LoadData()

        cdt = New DataTable
        cdt.Columns.Add("CFLD", GetType(System.String))
        cdt.Columns.Add("CHEADER", GetType(System.String))
        cdt.Columns.Add("SEQUENCE", GetType(Integer))
        cdt.Columns.Add("COLWIDTH", GetType(Integer))
        cdt.Columns.Add("COLFORMAT", GetType(String))
        cdt.Columns.Add("COLHIDDEN", GetType(String))
        Dim cnstr As Constraint
        cnstr = New UniqueConstraint("UK", cdt.Columns.Item("CFLD"))
        cdt.Constraints.Add(cnstr)




        Dim cmd As System.Data.Common.DbCommand
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        Dim sqlstr As String = ""
        
        sqlstr = "select ml.* from  masksline ml  where id_mask=" + mask_id.ToString + " order by sequence"


        If sqlstr = "" Then Exit Sub

        cmd.CommandText = sqlstr


        Dim da As OracleDataAdapter
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()


        Dim i As Integer
        Dim hasDcounter As Boolean = False
        Dim hasDcall As Boolean = False


        'Dim column As DataGridViewColumn
        Dim r As DataRow

     
        Dim sq As Integer
        sq = 1

        For i = 0 To dt.Rows.Count - 1

            r = cdt.NewRow
            r("CFLD") = dt.Rows(i)("CFLD")
            r("CHEADER") = dt.Rows(i)("CHEADER")
            r("SEQUENCE") = sq + cdt.Rows.Count
            r("COLWIDTH") = dt.Rows(i)("COLWIDTH")
            r("COLFORMAT") = dt.Rows(i)("COLFORMAT")
            r("COLHIDDEN") = dt.Rows(i)("COLHIDDEN")
            Try
                cdt.Rows.Add(r)
                sq += 1
            Catch ex As Exception
                ' найти колонку  и задать формат !
                Try
                    GetRowByField(dt.Rows(i)("CFLD"))("COLFORMAT") = dt.Rows(i)("COLFORMAT")
                Catch ex2 As Exception

                End Try

            End Try
            
        Next



        LoadItems()


       

    End Sub


    Private Sub LoadItems()
        InLoad = True
        Dim i As Integer
        cdt.DefaultView.Sort = "SEQUENCE"
        lstParams.Items.Clear()
        Dim drv As DataRowView
        For i = 0 To cdt.Rows.Count - 1
            drv = cdt.DefaultView.Item(i)
            lstParams.Items.Add(drv.Item("cfld"), drv.Item("COLHIDDEN") = 0)
            'If drv.Item("COLHIDDEN") = 1 Then
            '    i = i
            'End If
        Next
        InLoad = False
    End Sub

    Public Sub Save()
        Dim cmd As System.Data.Common.DbCommand
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        Dim sqlstr As String = ""
        Dim fld As String
        Debug.Print("Saving mask #" + mask_id.ToString)
        TVMain.ClearDuration()

        TVMain.SaveLog(id_bd, ptype, "??", 0, "Сохраняется маска (" + mask_id.ToString + ") " + txtName.Text + " на компьютере: " + Environment.MachineName)

        sqlstr = "update masks set cname='" + txtName.Text + "' where id_mask=:ID"

        cmd.CommandText = sqlstr
        cmd.Parameters.Add(New OracleParameter("ID", mask_id))
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Dim i As Long
        Dim dr As OracleDataReader

        For i = 0 To cdt.Rows.Count - 1
            If TypeOf (cdt.Rows(i)("CFLD")) Is DBNull Then Continue For
            fld = cdt.Rows(i)("CFLD").ToString
            If cdt.Rows(i)("CFLD") <> "DCOUNTER" Then
                Try
                    cmd = New OracleCommand
                    cmd.Connection = TvMain.dbconnect
                    cmd.CommandType = CommandType.Text

                    ' проверить есть ли такакя колонка
                    sqlstr = "select id_maskl from masksline  where id_mask=:ID and CFLD=:FLD"
                    cmd.CommandText = sqlstr

                    cmd.Parameters.Add(New OracleParameter("ID", mask_id))
                    cmd.Parameters.Add(New OracleParameter("FLD", cdt.Rows(i)("CFLD")))



                    SyncLock cmd.Connection
                        dr = cmd.ExecuteReader
                        If Not dr.HasRows Then
                            sqlstr = "insert into   masksline(id_maskl,id_mask,cfld,cheader,colwidth,colformat,colhidden) values(MASKSLINE_SEQ.nextval,:ID,:FLD,'" + cdt.Rows(i)("CHEADER") + "'," + cdt.Rows(i)("COLWIDTH").ToString() + ",'" + cdt.Rows(i)("COLFORMAT").ToString() + "'," + cdt.Rows(i)("COLHIDDEN").ToString() + ")"
                            cmd.CommandText = sqlstr
                            cmd.ExecuteNonQuery()
                            TVMain.SaveLog(id_bd, ptype, "??", 0, "Сохраняется маска (" + mask_id.ToString + ") " + txtName.Text + " Новое поле:" + fld + " на компьютере: " + Environment.MachineName)

                        End If
                        dr.Close()
                    End SyncLock

                    TVMain.SaveLog(id_bd, ptype, "??", 0, "Сохраняется маска (" + mask_id.ToString + ") " + txtName.Text + " Обновление поля:" + fld + " на компьютере: " + Environment.MachineName)

                    Try
                        sqlstr = "update masksline set cheader='" + cdt.Rows(i)("CHEADER") + "' where id_mask=:ID and CFLD=:FLD"
                        cmd.CommandText = sqlstr
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    Try
                        sqlstr = "update masksline set colwidth='" + cdt.Rows(i)("COLWIDTH").ToString().Replace(",", ".") + "' where id_mask=:ID and CFLD=:FLD"
                        cmd.CommandText = sqlstr
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    Try
                        If TypeName(cdt.Rows(i)("COLFORMAT")) <> "DBNull" Then
                            If cdt.Rows(i)("COLFORMAT") <> "?" Then
                                sqlstr = "update masksline set COLFORMAT='" + cdt.Rows(i)("COLFORMAT") + "' where id_mask=:ID and CFLD=:FLD"
                                cmd.CommandText = sqlstr
                                cmd.ExecuteNonQuery()
                            End If
                        Else

                            sqlstr = "update masksline set COLFORMAT='N' where id_mask=:ID and CFLD=:FLD"
                            cmd.CommandText = sqlstr
                            cmd.ExecuteNonQuery()

                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    Try
                        sqlstr = "update masksline set colhidden='" + cdt.Rows(i)("COLHIDDEN").ToString + "' where id_mask=:ID and CFLD=:FLD"
                        cmd.CommandText = sqlstr
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    Debug.Print("SEQ:" & cdt.Rows(i)("SEQUENCE").ToString & " fld=" & cdt.Rows(i)("CFLD"))
                    Try
                        sqlstr = "update masksline set SEQUENCE=" + cdt.Rows(i)("SEQUENCE").ToString + " where id_mask=:ID and CFLD=:FLD"
                        cmd.CommandText = sqlstr
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Catch ex As Exception
                    Debug.Print(ex.Message)
                    MsgBox(ex.Message)
                End Try

            End If

        Next
        'Debug.Print("End saving mask #" + mask_id.ToString)
        TvMain.QueryExec("delete from masksline where COLHIDDEN=1")
    End Sub


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Save()
        Me.DialogResult = DialogResult.OK
        'Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        'Me.Close()
    End Sub


    Private inSelChange As Boolean = False

    Private Sub lstParams_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstParams.ItemCheck
        Dim key As String
        If InLoad Then Exit Sub

        key = lstParams.Items(e.Index)


        Dim dr As DataRow
        dr = GetRowByField(key)
        If e.NewValue = CheckState.Checked Then
            dr("COLHIDDEN") = 0
        Else
            dr("COLHIDDEN") = 1
        End If
    End Sub

    Private Sub lstParams_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstParams.SelectedIndexChanged
        If inSelChange Then Exit Sub
        inSelChange = True
        Dim key As String
        key = lstParams.SelectedItem
        Dim i As Integer
        Dim sz As Integer
        sz = GetWidth(key)

        Dim dr As DataRow
        dr = GetRowByField(key)

        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("CFLD") = key Then
                txtHeader.Text = "" & dt.Rows(i)("CHEADER")

                txtWidth.Value = dt.Rows(i)("colwidth")
                If sz > 0 Then
                    txtWidth.Value = sz
                End If
                If TypeName(dt.Rows(i)("colformat")) = "DBNull" Then
                    cmbFormat.Text = "N"
                    dt.Rows(i)("colformat") = "N"
                Else
                    cmbFormat.Text = "" & dt.Rows(i)("colformat")
                End If

                If Not dr Is Nothing Then
                    If TypeName(dr("COLFORMAT")) <> "DBNull" Then
                        If dr("COLFORMAT") = "?" Then
                            dr("COLFORMAT") = dt.Rows(i)("colformat")
                        Else
                            cmbFormat.Text = dr("COLFORMAT")
                        End If
                    Else
                        dr("COLFORMAT") = "N"
                        cmbFormat.Text = "N"
                    End If
                End If



                inSelChange = False
                Exit Sub
            End If
        Next

        If key = "DCALL" Then
            txtHeader.Text = "Дата опроса"
            txtWidth.Value = 60
            If sz > 0 Then
                txtWidth.Value = sz
            End If
            cmbFormat.Text = "D"
            If Not dr Is Nothing Then
                If dr("COLFORMAT") = "?" Then
                    dr("COLFORMAT") = "D"
                End If
            End If

            inSelChange = False
            Exit Sub
        End If
        If key = "DCOUNTER" Then
            txtHeader.Text = "Дата счетчика"
            txtWidth.Value = 60
            If sz > 0 Then
                txtWidth.Value = sz
            End If
            cmbFormat.Text = "D"
            If Not dr Is Nothing Then
                If dr("COLFORMAT") = "?" Then
                    dr("COLFORMAT") = "D"
                End If
            End If
            inSelChange = False
            Exit Sub
        End If
        inSelChange = False
    End Sub

    Private Function GetWidth(ByVal key As String) As Integer
        Dim sz As Integer = -1
        'Dim info As RowLayoutColumnInfo

        Dim dr As DataRow
        dr = GetRowByField(key)
        If Not dr Is Nothing Then
            Try
                sz = dr("COLWIDTH")

                If sz <= 0 Then
                    sz = -1
                Else
                    Return (sz)
                End If
            Catch
            End Try


        End If

       
        Return sz
    End Function

    Private Function GetRowByField(ByVal Field As String) As DataRow
        Dim i As Integer
        For i = 0 To cdt.Rows.Count - 1
            If cdt.Rows(i)("CFLD") = Field Then
                Return cdt.Rows(i)
            End If
        Next
        Return Nothing
    End Function

    Private Sub txtHeader_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHeader.ValueChanged
        If inSelChange Then Exit Sub
        Dim key As String
        key = lstParams.SelectedItem
        Try
            GetRowByField(key)("CHEADER") = txtHeader.Text
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFormat.SelectedIndexChanged
        If inSelChange Then Exit Sub
        Dim key As String
        key = lstParams.SelectedItem
        Try
            GetRowByField(key)("COLFORMAT") = cmbFormat.Text
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWidth.ValueChanged
        If inSelChange Then Exit Sub
        Dim key As String
        key = lstParams.SelectedItem
        Try
            GetRowByField(key)("COLWIDTH") = txtWidth.Value
        Catch ex As Exception

        End Try

    End Sub

    Private Sub lstParams_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstParams.VisibleChanged

    End Sub

    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
        Dim key As String
        key = lstParams.SelectedItem
        If key Is Nothing Then Exit Sub
        Dim dr As DataRow
        dr = GetRowByField(key)
        MoveDown(dr("sequence"), key)
        LoadItems()
        lstParams.SelectedItem = key

    End Sub

    Private Sub MoveDown(ByVal pos As Integer, ByVal fld As String)
        Dim i As Integer
        If pos = cdt.Rows.Count Then Exit Sub
        For i = 0 To cdt.Rows.Count - 1
            If cdt.Rows(i)("sequence") = pos And cdt.Rows(i)("CFLD") = fld Then
                cdt.Rows(i)("sequence") = pos + 1
            End If
            If cdt.Rows(i)("sequence") = pos + 1 And cdt.Rows(i)("CFLD") <> fld Then
                cdt.Rows(i)("sequence") = pos
            End If
        Next

    End Sub
    Private Sub MoveUp(ByVal pos As Integer, ByVal fld As String)
        Dim i As Integer
        If pos = 0 Then Exit Sub
        For i = 0 To cdt.Rows.Count - 1
            If cdt.Rows(i)("sequence") = pos And cdt.Rows(i)("CFLD") = fld Then
                cdt.Rows(i)("sequence") = pos - 1
            End If
            If cdt.Rows(i)("sequence") = pos - 1 And cdt.Rows(i)("CFLD") <> fld Then
                cdt.Rows(i)("sequence") = pos
            End If
        Next

    End Sub

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        Dim key As String
        key = lstParams.SelectedItem
        If key Is Nothing Then Exit Sub
        Dim dr As DataRow
        dr = GetRowByField(key)
        MoveUp(dr("sequence"), key)
        LoadItems()
        lstParams.SelectedItem = key
    End Sub

    Private Sub frmSetupGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TVMain.SetupGrid = False Then
            cmdAddParam.Enabled = False
            lstParams.Enabled = False
        End If
        LoadData()
    End Sub

    Private Sub cmdAddParam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddParam.Click
        Dim f As frmSelectParam2Mask
        f = New frmSelectParam2Mask
        f.TVMain = Me.TVMain
        f.mask_id = mask_id
        f.id_ptype = ptype
        If f.ShowDialog() = DialogResult.OK Then
            LoadData()
        End If

    End Sub

    Private Sub grpCol_Click(sender As Object, e As EventArgs) Handles grpCol.Click

    End Sub

    Private Sub grpMAsk_Click(sender As Object, e As EventArgs) Handles grpMAsk.Click

    End Sub
End Class
