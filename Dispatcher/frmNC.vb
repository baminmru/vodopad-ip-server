Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Oracle.DataAccess.Client

Public Class frmNC
    Public id As Integer
    Public FName As String = ""
    Dim bActivated As Boolean = False

    Dim dtNew As DataTable

    Public Sub LoadData(ByVal newID As Integer, Optional ByVal NodeName As String = "")
        id = newID

        FName = NodeName

        If Not bActivated Then Exit Sub

        'If Me.Visible Then
        Loadreg()
        Loadnew()
        'End If
        SetupGridNew()
        SetupGridreg()



    End Sub

    Private Sub Loadnew()

        Dim dTo As Date
        Dim dfrom As Date
        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter

        dTo = Date.Now
        If optNew.CheckedItem.DataValue = 0 Then
            dfrom = dtNewFrom.Value
            dTo = dtNewTo.Value
        Else
            dfrom = dTo.AddDays(-optNew.CheckedItem.DataValue)
        End If

        dtNew = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select ID, MSG_TEXT  Message,node_name NodeName,dt_got ReceiveTime ,was_reg  from hcmessages where was_reg=0 and id_bd=" & id.ToString & " and dt_got <=:dtNew and dt_got >=:DF  "
        cmd.Parameters.Add("dtNew", OracleDbType.Date, dTo, ParameterDirection.Input)
        cmd.Parameters.Add("DF", OracleDbType.Date, dfrom, ParameterDirection.Input)

        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dtNew)
        cmd.Dispose()
        da.Dispose()
        'If dtNew.Rows.Count = 0 Then Exit Sub
        GridNC.DataSource = dtNew
        GridNC.DataBind()
        GridNC.Text = FName
    End Sub

    Private Sub Loadreg()

        Dim dt As DataTable
        Dim dTo As Date
        Dim dfrom As Date
        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter
        'If optReg.CheckedIndex = -1 Then
        '    optReg.CheckedIndex = 1
        'End If
        dTo = Date.Now
        If optReg.CheckedItem.DataValue = 0 Then
            dfrom = dtRegFrom.Value
            dTo = dtRegTo.Value
        Else
            dfrom = dTo.AddDays(-optReg.CheckedItem.DataValue)
        End If

        dt = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select  MSG_TEXT  Message,node_name NodeName,dt_got ReceiveTime ,dt_Reg Registed  from hcmessages where was_reg=1 and id_bd=" & id.ToString & " and dt_got <=:DT and dt_got >=:DF  "
    
        cmd.Parameters.Add("DT", OracleDbType.Date, dTo, ParameterDirection.Input)
        cmd.Parameters.Add("DF", OracleDbType.Date, dfrom, ParameterDirection.Input)
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        cmd.Dispose()
        da.Dispose()
        'If dt.Rows.Count = 0 Then Exit Sub
        GridReg.DataSource = dt
        GridReg.DataBind()
        GridReg.Text = FName
    End Sub

    Private Sub optNew_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNew.ValueChanged
        Loadnew()
    End Sub

    Private Sub optReg_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optReg.ValueChanged
        Loadreg()
    End Sub



    Private Sub SetupGridNew()
        GridNC.Text = FName
        GridNC.DisplayLayout.CaptionVisible = DefaultableBoolean.True
        GridNC.DisplayLayout.CaptionAppearance.FontData.Bold = DefaultableBoolean.True


        If GridNC.DisplayLayout.Bands(0).Columns.Count = 0 Then Return

        GridNC.DisplayLayout.Scrollbars = Scrollbars.Both
        GridNC.DisplayLayout.Override.WrapHeaderText = DefaultableBoolean.True
        GridNC.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid
        GridNC.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid
        GridNC.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid
        GridNC.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid

        Dim column As Infragistics.Win.UltraWinGrid.UltraGridColumn
        Dim info As RowLayoutColumnInfo

        column = GridNC.DisplayLayout.Bands(0).Columns("WAS_REG")
        column.Header.Caption = "Зарегистрирована"

        info = column.RowLayoutColumnInfo
        'info.PreferredCellSize = New Size(colsz, 24)
        'column.MinWidth = colsz

        column.Style = ColumnStyle.CheckBox
        'column.Format = "##,###,####"
        column.CellAppearance.TextHAlign = HAlign.Right
        column.CellAppearance.TextVAlign = VAlign.Middle
        column.CellActivation = Activation.AllowEdit

        column = GridNC.DisplayLayout.Bands(0).Columns("Message")
        column.Header.Caption = "Сообщение"

        'info = column.RowLayoutColumnInfo
        'info.PreferredCellSize = New Size(colsz, 24)
        column.MinWidth = 200

        column.Style = ColumnStyle.Edit
        'column.Format = "##,###,####"
        column.CellAppearance.TextHAlign = HAlign.Right
        column.CellAppearance.TextVAlign = VAlign.Middle
        column.CellActivation = Activation.ActivateOnly

        column = GridNC.DisplayLayout.Bands(0).Columns("NodeName")
        column.Style = ColumnStyle.Edit
        column.CellActivation = Activation.ActivateOnly
        column.Header.Caption = "Узел"

        column = GridNC.DisplayLayout.Bands(0).Columns("ReceiveTime")
        column.Style = ColumnStyle.DateTime
        column.Format = "HH:mm   dd.MM.yyyy"
        column.CellActivation = Activation.ActivateOnly
        column.Header.Caption = "Время получения"

        column = GridNC.DisplayLayout.Bands(0).Columns("ID")
        column.Hidden = True

    End Sub


    Private Sub SetupGridreg()
        GridReg.Text = FName
        GridReg.DisplayLayout.CaptionVisible = DefaultableBoolean.True
        GridReg.DisplayLayout.CaptionAppearance.FontData.Bold = DefaultableBoolean.True


        If GridReg.DisplayLayout.Bands(0).Columns.Count = 0 Then Return

        GridReg.DisplayLayout.Scrollbars = Scrollbars.Both
        GridReg.DisplayLayout.Override.WrapHeaderText = DefaultableBoolean.True
        GridReg.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid
        GridReg.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Solid
        GridReg.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid
        GridReg.DisplayLayout.Override.BorderStyleHeader = UIElementBorderStyle.Solid

        Dim column As Infragistics.Win.UltraWinGrid.UltraGridColumn
        'Dim info As RowLayoutColumnInfo
        'column = GridReg.DisplayLayout.Bands(0).Columns("Зарегистрирована")

        'info = column.RowLayoutColumnInfo
        ''info.PreferredCellSize = New Size(colsz, 24)
        ''column.MinWidth = colsz

        'column.Style = ColumnStyle.CheckBox
        ''column.Format = "##,###,####"
        'column.CellAppearance.TextHAlign = HAlign.Right
        'column.CellAppearance.TextVAlign = VAlign.Middle
        'column.CellActivation = Activation.AllowEdit

        column = GridReg.DisplayLayout.Bands(0).Columns("Message")
        column.Header.Caption = "Сообщение"

        'info = column.RowLayoutColumnInfo
        'info.PreferredCellSize = New Size(colsz, 24)
        column.MinWidth = 200

        column.Style = ColumnStyle.Edit
        'column.Format = "##,###,####"
        column.CellAppearance.TextHAlign = HAlign.Right
        column.CellAppearance.TextVAlign = VAlign.Middle
        column.CellActivation = Activation.ActivateOnly

        column = GridReg.DisplayLayout.Bands(0).Columns("NodeName")
        column.Style = ColumnStyle.Edit
        column.CellActivation = Activation.ActivateOnly
        column.Header.Caption = "Узел"

        column = GridReg.DisplayLayout.Bands(0).Columns("ReceiveTime")
        column.Style = ColumnStyle.DateTime
        column.Format = "HH:mm   dd.MM.yyyy"
        column.CellActivation = Activation.ActivateOnly
        column.Header.Caption = "Время получения"

        column = GridReg.DisplayLayout.Bands(0).Columns("Registed")
        column.Style = ColumnStyle.DateTime
        column.Format = "HH:mm   dd.MM.yyyy"
        column.CellActivation = Activation.ActivateOnly
        column.Header.Caption = "Дата регистрации"

    End Sub

    Private Sub cmdRegAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegAll.Click
        Dim dTo As Date
        Dim dfrom As Date
        Dim cmd As OracleCommand
        dTo = Date.Now
        If optNew.CheckedItem.DataValue = 0 Then
            dfrom = dtNewFrom.Value
            dTo = dtNewTo.Value
        Else
            dfrom = dTo.AddDays(-optNew.CheckedItem.DataValue)
        End If

        dtNew = New DataTable
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update hcmessages set was_reg=1, dt_Reg=sysdate where was_reg=0 and id_bd=" & id.ToString & " and dt_got <=:dtNew and dt_got >=:DF  "
        cmd.Parameters.Add("dtNew", OracleDbType.Date, dTo, ParameterDirection.Input)
        cmd.Parameters.Add("DF", OracleDbType.Date, dfrom, ParameterDirection.Input)
        cmd.ExecuteNonQuery()
        cmd.Dispose()

        CheckAnalizer(id)
        Loadreg()
        Loadnew()
    End Sub

    Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
        Dim i As Integer
        Dim cmd As OracleCommand
        cmd = New OracleCommand
        cmd.Connection = TvMain.dbconnect
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update hcmessages set was_reg=1, dt_Reg=sysdate where id=:ID"
        cmd.Parameters.Add("ID", OracleDbType.Int32)

        For i = 0 To dtNew.Rows.Count - 1
            If dtNew.Rows(i)("was_reg") <> 0 Then
                cmd.Parameters.Item("ID").Value = dtNew.Rows(i)("ID")
                cmd.ExecuteNonQuery()
            End If
        Next
        cmd.Dispose()

        CheckAnalizer(id)
        Loadreg()
        Loadnew()
    End Sub


    Private Sub CheckAnalizer(ByVal id As String)
        Dim dt As DataTable
        dt = TvMain.QuerySelect("select count(*) cnt from hcmessages where was_reg=0 and id_bd=" & id.ToString & " and  dt_got >=sysdate-2/24  ")
        If dt.Rows.Count > 0 Then
            If Integer.Parse(dt.Rows(0)("cnt").ToString) = 0 Then
                TvMain.QueryExec("Update Analizer set INFO ='', color='' where id_bd=" & id.ToString)
            End If
        End If
    End Sub

    Private Sub frmNC_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        bActivated = True
        LoadData(id, FName)
    End Sub

    Private Sub frmNC_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        bActivated = False
    End Sub

    Private Sub frmNC_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MyfrmNC = Nothing
    End Sub
End Class