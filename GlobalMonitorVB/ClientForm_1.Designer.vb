<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClientForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ValueListItem4 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem5 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem6 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem7 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem8 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem9 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem16 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem17 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem18 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem19 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem20 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem21 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem10 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem11 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem12 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem13 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem14 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem15 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem22 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblSnd = New System.Windows.Forms.Label()
        Me.lblRcv = New System.Windows.Forms.Label()
        Me.labelTime = New System.Windows.Forms.Label()
        Me.lblActiopn = New System.Windows.Forms.Label()
        Me.cmdClearLst = New System.Windows.Forms.Button()
        Me.chkByList = New System.Windows.Forms.CheckBox()
        Me.lstReads = New System.Windows.Forms.ListBox()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.chkQuestionOnRewrite = New System.Windows.Forms.CheckBox()
        Me.lblError = New System.Windows.Forms.TextBox()
        Me.chkTotal = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ButtonReadArch = New System.Windows.Forms.Button()
        Me.DateTimePickerBefor = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerAfter = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxDay = New System.Windows.Forms.CheckBox()
        Me.CheckBoxHour = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMoment = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cmdRefreshMoment = New System.Windows.Forms.Button()
        Me.lblMoment = New System.Windows.Forms.Label()
        Me.DataGridMoment = New System.Windows.Forms.DataGridView()
        Me.optMoment = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.SetupMoment = New System.Windows.Forms.Button()
        Me.MomentPrint = New System.Windows.Forms.Button()
        Me.ButtonExportMoment = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cmdRefreshHour = New System.Windows.Forms.Button()
        Me.lblHour = New System.Windows.Forms.Label()
        Me.DataGridHour = New System.Windows.Forms.DataGridView()
        Me.optHour = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.cmdHourSetup = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ButtonExportHour = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cmdRefreshDay = New System.Windows.Forms.Button()
        Me.lblDay = New System.Windows.Forms.Label()
        Me.DataGridDay = New System.Windows.Forms.DataGridView()
        Me.optDay = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.cmdDaySetup = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ButtonExportDay = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.cmdRefreshTotal = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.DataGridTotal = New System.Windows.Forms.DataGridView()
        Me.optTotal = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.cmdTotalSetup = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.ButtonExportTotal = New System.Windows.Forms.Button()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.lblMissing = New System.Windows.Forms.Label()
        Me.DataGridMissing = New System.Windows.Forms.DataGridView()
        Me.cmdRefreshLost = New System.Windows.Forms.Button()
        Me.ButtonSetupMissing = New System.Windows.Forms.Button()
        Me.ButtonPrintMissing = New System.Windows.Forms.Button()
        Me.ButtonExportMissing = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.expExcel = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ReadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridMoment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optMoment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridHour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optHour, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.DataGridMissing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblSnd)
        Me.Panel2.Controls.Add(Me.lblRcv)
        Me.Panel2.Controls.Add(Me.labelTime)
        Me.Panel2.Controls.Add(Me.lblActiopn)
        Me.Panel2.Controls.Add(Me.cmdClearLst)
        Me.Panel2.Controls.Add(Me.chkByList)
        Me.Panel2.Controls.Add(Me.lstReads)
        Me.Panel2.Controls.Add(Me.cmdStop)
        Me.Panel2.Controls.Add(Me.chkQuestionOnRewrite)
        Me.Panel2.Controls.Add(Me.lblError)
        Me.Panel2.Controls.Add(Me.chkTotal)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Controls.Add(Me.ButtonReadArch)
        Me.Panel2.Controls.Add(Me.DateTimePickerBefor)
        Me.Panel2.Controls.Add(Me.DateTimePickerAfter)
        Me.Panel2.Controls.Add(Me.CheckBoxDay)
        Me.Panel2.Controls.Add(Me.CheckBoxHour)
        Me.Panel2.Controls.Add(Me.CheckBoxMoment)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(963, 167)
        Me.Panel2.TabIndex = 1
        '
        'lblSnd
        '
        Me.lblSnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSnd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSnd.Location = New System.Drawing.Point(916, 41)
        Me.lblSnd.Name = "lblSnd"
        Me.lblSnd.Size = New System.Drawing.Size(23, 23)
        Me.lblSnd.TabIndex = 23
        Me.lblSnd.Text = "S"
        Me.lblSnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblSnd, "Отправка данных")
        '
        'lblRcv
        '
        Me.lblRcv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRcv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRcv.Location = New System.Drawing.Point(887, 41)
        Me.lblRcv.Name = "lblRcv"
        Me.lblRcv.Size = New System.Drawing.Size(23, 23)
        Me.lblRcv.TabIndex = 22
        Me.lblRcv.Text = "R"
        Me.lblRcv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.lblRcv, "Чтение данных")
        '
        'labelTime
        '
        Me.labelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.labelTime.Location = New System.Drawing.Point(376, 40)
        Me.labelTime.Name = "labelTime"
        Me.labelTime.Size = New System.Drawing.Size(77, 23)
        Me.labelTime.TabIndex = 21
        '
        'lblActiopn
        '
        Me.lblActiopn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblActiopn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActiopn.Location = New System.Drawing.Point(459, 41)
        Me.lblActiopn.Name = "lblActiopn"
        Me.lblActiopn.Size = New System.Drawing.Size(422, 23)
        Me.lblActiopn.TabIndex = 20
        '
        'cmdClearLst
        '
        Me.cmdClearLst.Location = New System.Drawing.Point(93, 101)
        Me.cmdClearLst.Name = "cmdClearLst"
        Me.cmdClearLst.Size = New System.Drawing.Size(71, 23)
        Me.cmdClearLst.TabIndex = 19
        Me.cmdClearLst.Text = "Сброс ->>"
        Me.cmdClearLst.UseVisualStyleBackColor = True
        '
        'chkByList
        '
        Me.chkByList.AutoSize = True
        Me.chkByList.Location = New System.Drawing.Point(9, 101)
        Me.chkByList.Name = "chkByList"
        Me.chkByList.Size = New System.Drawing.Size(78, 17)
        Me.chkByList.TabIndex = 18
        Me.chkByList.Text = "По списку"
        Me.chkByList.UseVisualStyleBackColor = True
        '
        'lstReads
        '
        Me.lstReads.FormattingEnabled = True
        Me.lstReads.Location = New System.Drawing.Point(170, 77)
        Me.lstReads.Name = "lstReads"
        Me.lstReads.Size = New System.Drawing.Size(200, 82)
        Me.lstReads.TabIndex = 17
        '
        'cmdStop
        '
        Me.cmdStop.Enabled = False
        Me.cmdStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdStop.ForeColor = System.Drawing.Color.Red
        Me.cmdStop.Location = New System.Drawing.Point(459, 77)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(73, 81)
        Me.cmdStop.TabIndex = 16
        Me.cmdStop.Text = "Стоп"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'chkQuestionOnRewrite
        '
        Me.chkQuestionOnRewrite.AutoSize = True
        Me.chkQuestionOnRewrite.Location = New System.Drawing.Point(9, 144)
        Me.chkQuestionOnRewrite.Name = "chkQuestionOnRewrite"
        Me.chkQuestionOnRewrite.Size = New System.Drawing.Size(166, 17)
        Me.chkQuestionOnRewrite.TabIndex = 15
        Me.chkQuestionOnRewrite.Text = "Подтверждать  перезапись"
        Me.chkQuestionOnRewrite.UseVisualStyleBackColor = True
        '
        'lblError
        '
        Me.lblError.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblError.Location = New System.Drawing.Point(548, 77)
        Me.lblError.Multiline = True
        Me.lblError.Name = "lblError"
        Me.lblError.ReadOnly = True
        Me.lblError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblError.Size = New System.Drawing.Size(391, 81)
        Me.lblError.TabIndex = 14
        '
        'chkTotal
        '
        Me.chkTotal.AutoSize = True
        Me.chkTotal.Location = New System.Drawing.Point(9, 78)
        Me.chkTotal.Name = "chkTotal"
        Me.chkTotal.Size = New System.Drawing.Size(76, 17)
        Me.chkTotal.TabIndex = 12
        Me.chkTotal.Text = "Итоговые"
        Me.chkTotal.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(173, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "С"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(173, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "По"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(376, 15)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(563, 19)
        Me.ProgressBar1.TabIndex = 6
        Me.ProgressBar1.Visible = False
        '
        'ButtonReadArch
        '
        Me.ButtonReadArch.Location = New System.Drawing.Point(376, 78)
        Me.ButtonReadArch.Name = "ButtonReadArch"
        Me.ButtonReadArch.Size = New System.Drawing.Size(77, 81)
        Me.ButtonReadArch.TabIndex = 5
        Me.ButtonReadArch.Text = "Прочитать архивы"
        Me.ButtonReadArch.UseVisualStyleBackColor = True
        '
        'DateTimePickerBefor
        '
        Me.DateTimePickerBefor.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.DateTimePickerBefor.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerBefor.Location = New System.Drawing.Point(207, 43)
        Me.DateTimePickerBefor.Name = "DateTimePickerBefor"
        Me.DateTimePickerBefor.Size = New System.Drawing.Size(163, 20)
        Me.DateTimePickerBefor.TabIndex = 4
        '
        'DateTimePickerAfter
        '
        Me.DateTimePickerAfter.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.DateTimePickerAfter.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerAfter.Location = New System.Drawing.Point(207, 17)
        Me.DateTimePickerAfter.Name = "DateTimePickerAfter"
        Me.DateTimePickerAfter.Size = New System.Drawing.Size(163, 20)
        Me.DateTimePickerAfter.TabIndex = 3
        '
        'CheckBoxDay
        '
        Me.CheckBoxDay.AutoSize = True
        Me.CheckBoxDay.Location = New System.Drawing.Point(9, 57)
        Me.CheckBoxDay.Name = "CheckBoxDay"
        Me.CheckBoxDay.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxDay.TabIndex = 2
        Me.CheckBoxDay.Text = "Суточный"
        Me.CheckBoxDay.UseVisualStyleBackColor = True
        '
        'CheckBoxHour
        '
        Me.CheckBoxHour.AutoSize = True
        Me.CheckBoxHour.Location = New System.Drawing.Point(9, 36)
        Me.CheckBoxHour.Name = "CheckBoxHour"
        Me.CheckBoxHour.Size = New System.Drawing.Size(70, 17)
        Me.CheckBoxHour.TabIndex = 1
        Me.CheckBoxHour.Text = "Часовой"
        Me.CheckBoxHour.UseVisualStyleBackColor = True
        '
        'CheckBoxMoment
        '
        Me.CheckBoxMoment.AutoSize = True
        Me.CheckBoxMoment.Location = New System.Drawing.Point(9, 15)
        Me.CheckBoxMoment.Name = "CheckBoxMoment"
        Me.CheckBoxMoment.Size = New System.Drawing.Size(90, 17)
        Me.CheckBoxMoment.TabIndex = 0
        Me.CheckBoxMoment.Text = "Мгновенный"
        Me.CheckBoxMoment.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TabControl1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 167)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(963, 320)
        Me.Panel3.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(0, 6)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(963, 314)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.cmdRefreshMoment)
        Me.TabPage1.Controls.Add(Me.lblMoment)
        Me.TabPage1.Controls.Add(Me.DataGridMoment)
        Me.TabPage1.Controls.Add(Me.optMoment)
        Me.TabPage1.Controls.Add(Me.SetupMoment)
        Me.TabPage1.Controls.Add(Me.MomentPrint)
        Me.TabPage1.Controls.Add(Me.ButtonExportMoment)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(955, 288)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Мгновенный"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cmdRefreshMoment
        '
        Me.cmdRefreshMoment.Location = New System.Drawing.Point(202, 7)
        Me.cmdRefreshMoment.Name = "cmdRefreshMoment"
        Me.cmdRefreshMoment.Size = New System.Drawing.Size(100, 22)
        Me.cmdRefreshMoment.TabIndex = 8
        Me.cmdRefreshMoment.Text = "Обновить"
        Me.cmdRefreshMoment.UseVisualStyleBackColor = True
        '
        'lblMoment
        '
        Me.lblMoment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMoment.BackColor = System.Drawing.SystemColors.Control
        Me.lblMoment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMoment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblMoment.Location = New System.Drawing.Point(8, 38)
        Me.lblMoment.Name = "lblMoment"
        Me.lblMoment.Size = New System.Drawing.Size(939, 22)
        Me.lblMoment.TabIndex = 7
        Me.lblMoment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridMoment
        '
        Me.DataGridMoment.AllowUserToAddRows = False
        Me.DataGridMoment.AllowUserToDeleteRows = False
        Me.DataGridMoment.AllowUserToOrderColumns = True
        Me.DataGridMoment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridMoment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMoment.Location = New System.Drawing.Point(8, 60)
        Me.DataGridMoment.MultiSelect = False
        Me.DataGridMoment.Name = "DataGridMoment"
        Me.DataGridMoment.ReadOnly = True
        Me.DataGridMoment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridMoment.Size = New System.Drawing.Size(939, 227)
        Me.DataGridMoment.TabIndex = 6
        '
        'optMoment
        '
        Me.optMoment.CheckedIndex = 1
        ValueListItem4.DataValue = CType(1, Short)
        ValueListItem4.DisplayText = "1 C."
        ValueListItem5.DataValue = CType(5, Short)
        ValueListItem5.DisplayText = "5 C."
        ValueListItem6.DataValue = CType(10, Short)
        ValueListItem6.DisplayText = "10 C."
        ValueListItem1.DataValue = CType(0, Short)
        ValueListItem1.DisplayText = "С-По"
        Me.optMoment.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem1})
        Me.optMoment.Location = New System.Drawing.Point(8, 6)
        Me.optMoment.Name = "optMoment"
        Me.optMoment.Size = New System.Drawing.Size(187, 23)
        Me.optMoment.TabIndex = 5
        Me.optMoment.Text = "5 C."
        '
        'SetupMoment
        '
        Me.SetupMoment.Location = New System.Drawing.Point(307, 6)
        Me.SetupMoment.Name = "SetupMoment"
        Me.SetupMoment.Size = New System.Drawing.Size(101, 23)
        Me.SetupMoment.TabIndex = 4
        Me.SetupMoment.Text = "Настройка"
        Me.SetupMoment.UseVisualStyleBackColor = True
        '
        'MomentPrint
        '
        Me.MomentPrint.Location = New System.Drawing.Point(604, 6)
        Me.MomentPrint.Name = "MomentPrint"
        Me.MomentPrint.Size = New System.Drawing.Size(101, 23)
        Me.MomentPrint.TabIndex = 3
        Me.MomentPrint.Text = "Печать"
        Me.MomentPrint.UseVisualStyleBackColor = True
        Me.MomentPrint.Visible = False
        '
        'ButtonExportMoment
        '
        Me.ButtonExportMoment.Location = New System.Drawing.Point(508, 6)
        Me.ButtonExportMoment.Name = "ButtonExportMoment"
        Me.ButtonExportMoment.Size = New System.Drawing.Size(90, 23)
        Me.ButtonExportMoment.TabIndex = 1
        Me.ButtonExportMoment.Text = "Экспорт"
        Me.ButtonExportMoment.UseVisualStyleBackColor = True
        Me.ButtonExportMoment.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmdRefreshHour)
        Me.TabPage2.Controls.Add(Me.lblHour)
        Me.TabPage2.Controls.Add(Me.DataGridHour)
        Me.TabPage2.Controls.Add(Me.optHour)
        Me.TabPage2.Controls.Add(Me.cmdHourSetup)
        Me.TabPage2.Controls.Add(Me.Button3)
        Me.TabPage2.Controls.Add(Me.ButtonExportHour)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(955, 288)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Часовой"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdRefreshHour
        '
        Me.cmdRefreshHour.Location = New System.Drawing.Point(202, 6)
        Me.cmdRefreshHour.Name = "cmdRefreshHour"
        Me.cmdRefreshHour.Size = New System.Drawing.Size(100, 22)
        Me.cmdRefreshHour.TabIndex = 11
        Me.cmdRefreshHour.Text = "Обновить"
        Me.cmdRefreshHour.UseVisualStyleBackColor = True
        '
        'lblHour
        '
        Me.lblHour.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHour.BackColor = System.Drawing.SystemColors.Control
        Me.lblHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHour.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblHour.Location = New System.Drawing.Point(10, 38)
        Me.lblHour.Name = "lblHour"
        Me.lblHour.Size = New System.Drawing.Size(937, 25)
        Me.lblHour.TabIndex = 10
        Me.lblHour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridHour
        '
        Me.DataGridHour.AllowUserToAddRows = False
        Me.DataGridHour.AllowUserToDeleteRows = False
        Me.DataGridHour.AllowUserToOrderColumns = True
        Me.DataGridHour.AllowUserToResizeColumns = False
        Me.DataGridHour.AllowUserToResizeRows = False
        Me.DataGridHour.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridHour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridHour.Location = New System.Drawing.Point(9, 63)
        Me.DataGridHour.Name = "DataGridHour"
        Me.DataGridHour.ReadOnly = True
        Me.DataGridHour.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridHour.Size = New System.Drawing.Size(938, 224)
        Me.DataGridHour.TabIndex = 9
        '
        'optHour
        '
        Me.optHour.CheckedIndex = 1
        ValueListItem7.DataValue = CType(1, Short)
        ValueListItem7.DisplayText = "1 C."
        ValueListItem8.DataValue = CType(5, Short)
        ValueListItem8.DisplayText = "5 C."
        ValueListItem9.DataValue = CType(10, Short)
        ValueListItem9.DisplayText = "10 C."
        ValueListItem2.DataValue = CType(0, Short)
        ValueListItem2.DisplayText = "С-По"
        Me.optHour.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem7, ValueListItem8, ValueListItem9, ValueListItem2})
        Me.optHour.Location = New System.Drawing.Point(9, 6)
        Me.optHour.Name = "optHour"
        Me.optHour.Size = New System.Drawing.Size(187, 23)
        Me.optHour.TabIndex = 8
        Me.optHour.Text = "5 C."
        '
        'cmdHourSetup
        '
        Me.cmdHourSetup.Location = New System.Drawing.Point(308, 5)
        Me.cmdHourSetup.Name = "cmdHourSetup"
        Me.cmdHourSetup.Size = New System.Drawing.Size(101, 23)
        Me.cmdHourSetup.TabIndex = 7
        Me.cmdHourSetup.Text = "Настройка"
        Me.cmdHourSetup.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(608, 6)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(101, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Печать"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'ButtonExportHour
        '
        Me.ButtonExportHour.Location = New System.Drawing.Point(512, 6)
        Me.ButtonExportHour.Name = "ButtonExportHour"
        Me.ButtonExportHour.Size = New System.Drawing.Size(90, 23)
        Me.ButtonExportHour.TabIndex = 3
        Me.ButtonExportHour.Text = "Экспорт"
        Me.ButtonExportHour.UseVisualStyleBackColor = True
        Me.ButtonExportHour.Visible = False
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cmdRefreshDay)
        Me.TabPage3.Controls.Add(Me.lblDay)
        Me.TabPage3.Controls.Add(Me.DataGridDay)
        Me.TabPage3.Controls.Add(Me.optDay)
        Me.TabPage3.Controls.Add(Me.cmdDaySetup)
        Me.TabPage3.Controls.Add(Me.Button5)
        Me.TabPage3.Controls.Add(Me.ButtonExportDay)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(955, 288)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Суточный"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cmdRefreshDay
        '
        Me.cmdRefreshDay.Location = New System.Drawing.Point(349, 7)
        Me.cmdRefreshDay.Name = "cmdRefreshDay"
        Me.cmdRefreshDay.Size = New System.Drawing.Size(100, 22)
        Me.cmdRefreshDay.TabIndex = 11
        Me.cmdRefreshDay.Text = "Обновить"
        Me.cmdRefreshDay.UseVisualStyleBackColor = True
        '
        'lblDay
        '
        Me.lblDay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDay.BackColor = System.Drawing.SystemColors.Control
        Me.lblDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDay.Location = New System.Drawing.Point(10, 32)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(937, 25)
        Me.lblDay.TabIndex = 10
        Me.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridDay
        '
        Me.DataGridDay.AllowUserToAddRows = False
        Me.DataGridDay.AllowUserToDeleteRows = False
        Me.DataGridDay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridDay.Location = New System.Drawing.Point(10, 57)
        Me.DataGridDay.MultiSelect = False
        Me.DataGridDay.Name = "DataGridDay"
        Me.DataGridDay.ReadOnly = True
        Me.DataGridDay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridDay.Size = New System.Drawing.Size(937, 232)
        Me.DataGridDay.TabIndex = 9
        '
        'optDay
        '
        Me.optDay.CheckedIndex = 2
        ValueListItem16.DataValue = CType(1, Short)
        ValueListItem16.DisplayText = "1 C."
        ValueListItem17.DataValue = CType(5, Short)
        ValueListItem17.DisplayText = "5 C."
        ValueListItem18.CheckState = System.Windows.Forms.CheckState.Checked
        ValueListItem18.DataValue = CType(10, Short)
        ValueListItem18.DisplayText = "10 C."
        ValueListItem19.DataValue = CType(15, Short)
        ValueListItem19.DisplayText = "15 C."
        ValueListItem20.DataValue = CType(20, Short)
        ValueListItem20.DisplayText = "20 C."
        ValueListItem21.DataValue = CType(30, Short)
        ValueListItem21.DisplayText = "30 C."
        ValueListItem3.DataValue = CType(0, Short)
        ValueListItem3.DisplayText = "С-По"
        Me.optDay.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem16, ValueListItem17, ValueListItem18, ValueListItem19, ValueListItem20, ValueListItem21, ValueListItem3})
        Me.optDay.Location = New System.Drawing.Point(10, 6)
        Me.optDay.Name = "optDay"
        Me.optDay.Size = New System.Drawing.Size(333, 23)
        Me.optDay.TabIndex = 8
        Me.optDay.Text = "10 C."
        '
        'cmdDaySetup
        '
        Me.cmdDaySetup.Location = New System.Drawing.Point(455, 7)
        Me.cmdDaySetup.Name = "cmdDaySetup"
        Me.cmdDaySetup.Size = New System.Drawing.Size(101, 23)
        Me.cmdDaySetup.TabIndex = 7
        Me.cmdDaySetup.Text = "Настройка"
        Me.cmdDaySetup.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(658, 7)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(101, 23)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "Печать"
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'ButtonExportDay
        '
        Me.ButtonExportDay.Location = New System.Drawing.Point(562, 7)
        Me.ButtonExportDay.Name = "ButtonExportDay"
        Me.ButtonExportDay.Size = New System.Drawing.Size(90, 23)
        Me.ButtonExportDay.TabIndex = 3
        Me.ButtonExportDay.Text = "Экспорт"
        Me.ButtonExportDay.UseVisualStyleBackColor = True
        Me.ButtonExportDay.Visible = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmdRefreshTotal)
        Me.TabPage4.Controls.Add(Me.lblTotal)
        Me.TabPage4.Controls.Add(Me.DataGridTotal)
        Me.TabPage4.Controls.Add(Me.optTotal)
        Me.TabPage4.Controls.Add(Me.cmdTotalSetup)
        Me.TabPage4.Controls.Add(Me.Button7)
        Me.TabPage4.Controls.Add(Me.ButtonExportTotal)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(955, 288)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Итоговые"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'cmdRefreshTotal
        '
        Me.cmdRefreshTotal.Location = New System.Drawing.Point(345, 6)
        Me.cmdRefreshTotal.Name = "cmdRefreshTotal"
        Me.cmdRefreshTotal.Size = New System.Drawing.Size(100, 22)
        Me.cmdRefreshTotal.TabIndex = 12
        Me.cmdRefreshTotal.Text = "Обновить"
        Me.cmdRefreshTotal.UseVisualStyleBackColor = True
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(8, 37)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(944, 25)
        Me.lblTotal.TabIndex = 11
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridTotal
        '
        Me.DataGridTotal.AllowUserToAddRows = False
        Me.DataGridTotal.AllowUserToDeleteRows = False
        Me.DataGridTotal.AllowUserToOrderColumns = True
        Me.DataGridTotal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridTotal.Location = New System.Drawing.Point(9, 62)
        Me.DataGridTotal.MultiSelect = False
        Me.DataGridTotal.Name = "DataGridTotal"
        Me.DataGridTotal.ReadOnly = True
        Me.DataGridTotal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridTotal.Size = New System.Drawing.Size(943, 226)
        Me.DataGridTotal.TabIndex = 10
        '
        'optTotal
        '
        Me.optTotal.CheckedIndex = 2
        ValueListItem10.DataValue = CType(1, Short)
        ValueListItem10.DisplayText = "1 C."
        ValueListItem11.CheckState = System.Windows.Forms.CheckState.Checked
        ValueListItem11.DataValue = CType(5, Short)
        ValueListItem11.DisplayText = "5 C."
        ValueListItem12.DataValue = CType(10, Short)
        ValueListItem12.DisplayText = "10 C."
        ValueListItem13.DataValue = CType(15, Short)
        ValueListItem13.DisplayText = "15 C."
        ValueListItem14.DataValue = CType(20, Short)
        ValueListItem14.DisplayText = "20 C."
        ValueListItem15.DataValue = CType(30, Short)
        ValueListItem15.DisplayText = "30 C."
        ValueListItem22.DataValue = CType(0, Short)
        ValueListItem22.DisplayText = "С-По"
        Me.optTotal.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem10, ValueListItem11, ValueListItem12, ValueListItem13, ValueListItem14, ValueListItem15, ValueListItem22})
        Me.optTotal.Location = New System.Drawing.Point(9, 6)
        Me.optTotal.Name = "optTotal"
        Me.optTotal.Size = New System.Drawing.Size(330, 23)
        Me.optTotal.TabIndex = 9
        Me.optTotal.Text = "10 C."
        '
        'cmdTotalSetup
        '
        Me.cmdTotalSetup.Location = New System.Drawing.Point(451, 6)
        Me.cmdTotalSetup.Name = "cmdTotalSetup"
        Me.cmdTotalSetup.Size = New System.Drawing.Size(101, 23)
        Me.cmdTotalSetup.TabIndex = 7
        Me.cmdTotalSetup.Text = "Настройка"
        Me.cmdTotalSetup.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(804, 6)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(101, 23)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "Печать"
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'ButtonExportTotal
        '
        Me.ButtonExportTotal.Location = New System.Drawing.Point(708, 6)
        Me.ButtonExportTotal.Name = "ButtonExportTotal"
        Me.ButtonExportTotal.Size = New System.Drawing.Size(90, 23)
        Me.ButtonExportTotal.TabIndex = 0
        Me.ButtonExportTotal.Text = "Экспорт"
        Me.ButtonExportTotal.UseVisualStyleBackColor = True
        Me.ButtonExportTotal.Visible = False
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.lblMissing)
        Me.TabPage5.Controls.Add(Me.DataGridMissing)
        Me.TabPage5.Controls.Add(Me.cmdRefreshLost)
        Me.TabPage5.Controls.Add(Me.ButtonSetupMissing)
        Me.TabPage5.Controls.Add(Me.ButtonPrintMissing)
        Me.TabPage5.Controls.Add(Me.ButtonExportMissing)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(955, 288)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Пропущенные архивы"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'lblMissing
        '
        Me.lblMissing.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMissing.BackColor = System.Drawing.SystemColors.Control
        Me.lblMissing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMissing.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblMissing.Location = New System.Drawing.Point(5, 34)
        Me.lblMissing.Name = "lblMissing"
        Me.lblMissing.Size = New System.Drawing.Size(942, 25)
        Me.lblMissing.TabIndex = 11
        Me.lblMissing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridMissing
        '
        Me.DataGridMissing.AllowUserToAddRows = False
        Me.DataGridMissing.AllowUserToDeleteRows = False
        Me.DataGridMissing.AllowUserToOrderColumns = True
        Me.DataGridMissing.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridMissing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMissing.Location = New System.Drawing.Point(4, 59)
        Me.DataGridMissing.MultiSelect = False
        Me.DataGridMissing.Name = "DataGridMissing"
        Me.DataGridMissing.ReadOnly = True
        Me.DataGridMissing.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridMissing.Size = New System.Drawing.Size(943, 228)
        Me.DataGridMissing.TabIndex = 10
        '
        'cmdRefreshLost
        '
        Me.cmdRefreshLost.Location = New System.Drawing.Point(4, 8)
        Me.cmdRefreshLost.Name = "cmdRefreshLost"
        Me.cmdRefreshLost.Size = New System.Drawing.Size(154, 23)
        Me.cmdRefreshLost.TabIndex = 9
        Me.cmdRefreshLost.Text = "Обновить список по узлу"
        Me.cmdRefreshLost.UseVisualStyleBackColor = True
        '
        'ButtonSetupMissing
        '
        Me.ButtonSetupMissing.Location = New System.Drawing.Point(675, 6)
        Me.ButtonSetupMissing.Name = "ButtonSetupMissing"
        Me.ButtonSetupMissing.Size = New System.Drawing.Size(101, 23)
        Me.ButtonSetupMissing.TabIndex = 8
        Me.ButtonSetupMissing.Text = "Настройка"
        Me.ButtonSetupMissing.UseVisualStyleBackColor = True
        Me.ButtonSetupMissing.Visible = False
        '
        'ButtonPrintMissing
        '
        Me.ButtonPrintMissing.Location = New System.Drawing.Point(568, 6)
        Me.ButtonPrintMissing.Name = "ButtonPrintMissing"
        Me.ButtonPrintMissing.Size = New System.Drawing.Size(101, 23)
        Me.ButtonPrintMissing.TabIndex = 7
        Me.ButtonPrintMissing.Text = "Печать"
        Me.ButtonPrintMissing.UseVisualStyleBackColor = True
        Me.ButtonPrintMissing.Visible = False
        '
        'ButtonExportMissing
        '
        Me.ButtonExportMissing.Location = New System.Drawing.Point(472, 6)
        Me.ButtonExportMissing.Name = "ButtonExportMissing"
        Me.ButtonExportMissing.Size = New System.Drawing.Size(90, 23)
        Me.ButtonExportMissing.TabIndex = 5
        Me.ButtonExportMissing.Text = "Экспорт"
        Me.ButtonExportMissing.UseVisualStyleBackColor = True
        Me.ButtonExportMissing.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(5, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Экспорт"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "xls"
        Me.SaveFileDialog1.Filter = "Excel file|*.xls"
        Me.SaveFileDialog1.Title = "Сохранение архива в EXCEL"
        '
        'expExcel
        '
        Me.expExcel.DefaultWorkbookPaletteMode = Infragistics.Excel.WorkbookPaletteMode.StandardPalette
        Me.expExcel.FileLimitBehaviour = Infragistics.Win.UltraWinGrid.ExcelExport.FileLimitBehaviour.TruncateData
        '
        'ReadTimer
        '
        Me.ReadTimer.Interval = 60
        '
        'ClientForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 487)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.MinimumSize = New System.Drawing.Size(700, 400)
        Me.Name = "ClientForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Данные"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridMoment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optMoment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridHour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optHour, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DataGridTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optTotal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.DataGridMissing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonReadArch As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerBefor As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerAfter As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxDay As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxHour As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMoment As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonExportMoment As System.Windows.Forms.Button
    Friend WithEvents ButtonExportHour As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonExportDay As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkTotal As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonExportTotal As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents expExcel As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SetupMoment As System.Windows.Forms.Button
    Friend WithEvents MomentPrint As System.Windows.Forms.Button
    Friend WithEvents cmdHourSetup As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents cmdDaySetup As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents cmdTotalSetup As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents lblError As System.Windows.Forms.TextBox
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonSetupMissing As System.Windows.Forms.Button
    Friend WithEvents ButtonPrintMissing As System.Windows.Forms.Button
    Friend WithEvents ButtonExportMissing As System.Windows.Forms.Button
    Friend WithEvents optMoment As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents optHour As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents optDay As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents optTotal As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents cmdRefreshLost As System.Windows.Forms.Button
    Friend WithEvents chkQuestionOnRewrite As System.Windows.Forms.CheckBox
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents DataGridMoment As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridHour As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridDay As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridTotal As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridMissing As System.Windows.Forms.DataGridView
    Friend WithEvents lblMoment As System.Windows.Forms.Label
    Friend WithEvents lblHour As System.Windows.Forms.Label
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblMissing As System.Windows.Forms.Label
    Friend WithEvents cmdRefreshMoment As System.Windows.Forms.Button
    Friend WithEvents cmdRefreshHour As System.Windows.Forms.Button
    Friend WithEvents cmdRefreshDay As System.Windows.Forms.Button
    Friend WithEvents cmdRefreshTotal As System.Windows.Forms.Button
    Friend WithEvents chkByList As System.Windows.Forms.CheckBox
    Friend WithEvents lstReads As System.Windows.Forms.ListBox
    Friend WithEvents cmdClearLst As System.Windows.Forms.Button
    Friend WithEvents ReadTimer As System.Windows.Forms.Timer
    Friend WithEvents lblSnd As System.Windows.Forms.Label
    Friend WithEvents lblRcv As System.Windows.Forms.Label
    Friend WithEvents labelTime As System.Windows.Forms.Label
    Friend WithEvents lblActiopn As System.Windows.Forms.Label

End Class
