<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNC
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem4 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem5 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem6 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem7 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem8 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem16 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem17 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem18 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem19 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem20 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem21 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabNew = New System.Windows.Forms.TabPage()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtNewTo = New System.Windows.Forms.DateTimePicker()
        Me.dtNewFrom = New System.Windows.Forms.DateTimePicker()
        Me.optNew = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.cmdRegAll = New Infragistics.Win.Misc.UltraButton()
        Me.cmdRegister = New Infragistics.Win.Misc.UltraButton()
        Me.GridNC = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.tabReg = New System.Windows.Forms.TabPage()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtRegTo = New System.Windows.Forms.DateTimePicker()
        Me.dtRegFrom = New System.Windows.Forms.DateTimePicker()
        Me.optReg = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.GridReg = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.TabControl1.SuspendLayout()
        Me.tabNew.SuspendLayout()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        CType(Me.optNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridNC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabReg.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.optReg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridReg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabNew)
        Me.TabControl1.Controls.Add(Me.tabReg)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(872, 393)
        Me.TabControl1.TabIndex = 0
        '
        'tabNew
        '
        Me.tabNew.Controls.Add(Me.UltraPanel1)
        Me.tabNew.Controls.Add(Me.GridNC)
        Me.tabNew.Location = New System.Drawing.Point(4, 22)
        Me.tabNew.Name = "tabNew"
        Me.tabNew.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNew.Size = New System.Drawing.Size(864, 367)
        Me.tabNew.TabIndex = 0
        Me.tabNew.Text = "Новые"
        Me.tabNew.UseVisualStyleBackColor = True
        '
        'UltraPanel1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.ButtonFace
        Appearance1.BackColor2 = System.Drawing.SystemColors.ButtonFace
        Me.UltraPanel1.Appearance = Appearance1
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label3)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.dtNewTo)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.dtNewFrom)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.optNew)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmdRegAll)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmdRegister)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel1.Location = New System.Drawing.Point(3, 3)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(858, 71)
        Me.UltraPanel1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(538, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "С"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(538, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "По"
        '
        'dtNewTo
        '
        Me.dtNewTo.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtNewTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtNewTo.Location = New System.Drawing.Point(572, 35)
        Me.dtNewTo.Name = "dtNewTo"
        Me.dtNewTo.Size = New System.Drawing.Size(163, 20)
        Me.dtNewTo.TabIndex = 11
        '
        'dtNewFrom
        '
        Me.dtNewFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtNewFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtNewFrom.Location = New System.Drawing.Point(572, 9)
        Me.dtNewFrom.Name = "dtNewFrom"
        Me.dtNewFrom.Size = New System.Drawing.Size(163, 20)
        Me.dtNewFrom.TabIndex = 10
        '
        'optNew
        '
        Me.optNew.CheckedIndex = 0
        ValueListItem1.DataValue = CType(1, Short)
        ValueListItem1.DisplayText = "1 C."
        ValueListItem2.DataValue = CType(5, Short)
        ValueListItem2.DisplayText = "5 C."
        ValueListItem4.CheckState = System.Windows.Forms.CheckState.Checked
        ValueListItem4.DataValue = CType(10, Short)
        ValueListItem4.DisplayText = "10 C."
        ValueListItem5.DataValue = CType(15, Short)
        ValueListItem5.DisplayText = "15 C."
        ValueListItem6.DataValue = CType(20, Short)
        ValueListItem6.DisplayText = "20 C."
        ValueListItem7.DataValue = CType(30, Short)
        ValueListItem7.DisplayText = "30 C."
        ValueListItem8.DataValue = CType(0, Short)
        ValueListItem8.DisplayText = "С-По"
        Me.optNew.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem7, ValueListItem8})
        Me.optNew.Location = New System.Drawing.Point(197, 9)
        Me.optNew.Name = "optNew"
        Me.optNew.Size = New System.Drawing.Size(333, 23)
        Me.optNew.TabIndex = 9
        Me.optNew.Text = "1 C."
        '
        'cmdRegAll
        '
        Me.cmdRegAll.Location = New System.Drawing.Point(12, 35)
        Me.cmdRegAll.Name = "cmdRegAll"
        Me.cmdRegAll.Size = New System.Drawing.Size(168, 26)
        Me.cmdRegAll.TabIndex = 1
        Me.cmdRegAll.Text = "Зарегистрировать все"
        '
        'cmdRegister
        '
        Me.cmdRegister.Location = New System.Drawing.Point(12, 6)
        Me.cmdRegister.Name = "cmdRegister"
        Me.cmdRegister.Size = New System.Drawing.Size(168, 26)
        Me.cmdRegister.TabIndex = 0
        Me.cmdRegister.Text = "Зарегистрировать"
        '
        'GridNC
        '
        Me.GridNC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.BackColor = System.Drawing.Color.White
        Me.GridNC.DisplayLayout.Appearance = Appearance3
        Me.GridNC.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[True]
        Me.GridNC.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.GridNC.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free
        Me.GridNC.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.GridNC.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.GridNC.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[True]
        Me.GridNC.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.HeadersOnly
        Me.GridNC.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.GridNC.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[True]
        Me.GridNC.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.[True]
        Me.GridNC.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.GridNC.Location = New System.Drawing.Point(3, 80)
        Me.GridNC.Name = "GridNC"
        Me.GridNC.Size = New System.Drawing.Size(855, 287)
        Me.GridNC.TabIndex = 0
        Me.GridNC.Text = "UltraGrid1"
        '
        'tabReg
        '
        Me.tabReg.Controls.Add(Me.UltraPanel2)
        Me.tabReg.Controls.Add(Me.GridReg)
        Me.tabReg.Location = New System.Drawing.Point(4, 22)
        Me.tabReg.Name = "tabReg"
        Me.tabReg.Padding = New System.Windows.Forms.Padding(3)
        Me.tabReg.Size = New System.Drawing.Size(864, 367)
        Me.tabReg.TabIndex = 1
        Me.tabReg.Text = "Зарегистрированные"
        Me.tabReg.UseVisualStyleBackColor = True
        '
        'UltraPanel2
        '
        Appearance2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.UltraPanel2.Appearance = Appearance2
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.Label1)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.Label4)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.dtRegTo)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.dtRegFrom)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.optReg)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(3, 3)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(858, 71)
        Me.UltraPanel2.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(350, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "С"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(350, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "По"
        '
        'dtRegTo
        '
        Me.dtRegTo.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtRegTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRegTo.Location = New System.Drawing.Point(384, 32)
        Me.dtRegTo.Name = "dtRegTo"
        Me.dtRegTo.Size = New System.Drawing.Size(163, 20)
        Me.dtRegTo.TabIndex = 11
        '
        'dtRegFrom
        '
        Me.dtRegFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtRegFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRegFrom.Location = New System.Drawing.Point(384, 6)
        Me.dtRegFrom.Name = "dtRegFrom"
        Me.dtRegFrom.Size = New System.Drawing.Size(163, 20)
        Me.dtRegFrom.TabIndex = 10
        '
        'optReg
        '
        Me.optReg.CheckedIndex = 0
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
        Me.optReg.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem16, ValueListItem17, ValueListItem18, ValueListItem19, ValueListItem20, ValueListItem21, ValueListItem3})
        Me.optReg.Location = New System.Drawing.Point(5, 6)
        Me.optReg.Name = "optReg"
        Me.optReg.Size = New System.Drawing.Size(333, 23)
        Me.optReg.TabIndex = 9
        Me.optReg.Text = "1 C."
        '
        'GridReg
        '
        Me.GridReg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance4.BackColor = System.Drawing.Color.White
        Me.GridReg.DisplayLayout.Appearance = Appearance4
        Me.GridReg.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[True]
        Me.GridReg.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.GridReg.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.GridReg.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.GridReg.Location = New System.Drawing.Point(3, 79)
        Me.GridReg.Name = "GridReg"
        Me.GridReg.Size = New System.Drawing.Size(858, 285)
        Me.GridReg.TabIndex = 0
        Me.GridReg.Text = "UltraGrid1"
        '
        'frmNC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 393)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmNC"
        Me.Text = "Нештатные ситуации"
        Me.TabControl1.ResumeLayout(False)
        Me.tabNew.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        CType(Me.optNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridNC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabReg.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.optReg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridReg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabNew As System.Windows.Forms.TabPage
    Friend WithEvents tabReg As System.Windows.Forms.TabPage
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents GridNC As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents cmdRegAll As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmdRegister As Infragistics.Win.Misc.UltraButton
    Friend WithEvents GridReg As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents optNew As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtNewFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtRegTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtRegFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents optReg As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents dtNewTo As System.Windows.Forms.DateTimePicker
End Class
