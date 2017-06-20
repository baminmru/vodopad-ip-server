<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBounds
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
        Dim UltraToolTipInfo2 As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Выбор показателей для отображения на схеме", Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
        Dim UltraToolTipInfo1 As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Задать граничные значения показателей", Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
        Me.cmdParams = New Infragistics.Win.Misc.UltraButton()
        Me.UltraToolTipManager1 = New Infragistics.Win.UltraWinToolTip.UltraToolTipManager(Me.components)
        Me.cmdValues = New Infragistics.Win.Misc.UltraButton()
        Me.chkMoment = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chkHour = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chkDay = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.myPanel = New System.Windows.Forms.Panel()
        Me.chkSEZON = New System.Windows.Forms.CheckBox()
        Me.gv = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdParams
        '
        Me.cmdParams.Location = New System.Drawing.Point(368, 395)
        Me.cmdParams.Name = "cmdParams"
        Me.cmdParams.Size = New System.Drawing.Size(148, 27)
        Me.cmdParams.TabIndex = 6
        Me.cmdParams.Text = "Выбор показателей"
        UltraToolTipInfo2.ToolTipText = "Выбор показателей для отображения на схеме"
        Me.UltraToolTipManager1.SetUltraToolTip(Me.cmdParams, UltraToolTipInfo2)
        '
        'UltraToolTipManager1
        '
        Me.UltraToolTipManager1.ContainingControl = Me
        '
        'cmdValues
        '
        Me.cmdValues.Location = New System.Drawing.Point(368, 194)
        Me.cmdValues.Name = "cmdValues"
        Me.cmdValues.Size = New System.Drawing.Size(148, 28)
        Me.cmdValues.TabIndex = 11
        Me.cmdValues.Text = "Сохранить"
        UltraToolTipInfo1.ToolTipText = "Задать граничные значения показателей"
        Me.UltraToolTipManager1.SetUltraToolTip(Me.cmdValues, UltraToolTipInfo1)
        '
        'chkMoment
        '
        Me.chkMoment.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkMoment.Checked = True
        Me.chkMoment.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMoment.Location = New System.Drawing.Point(368, 65)
        Me.chkMoment.Name = "chkMoment"
        Me.chkMoment.Size = New System.Drawing.Size(148, 29)
        Me.chkMoment.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkMoment.TabIndex = 8
        Me.chkMoment.Text = "Мгновенные"
        '
        'chkHour
        '
        Me.chkHour.Location = New System.Drawing.Point(368, 100)
        Me.chkHour.Name = "chkHour"
        Me.chkHour.Size = New System.Drawing.Size(148, 29)
        Me.chkHour.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkHour.TabIndex = 9
        Me.chkHour.Text = "Часовые"
        '
        'chkDay
        '
        Me.chkDay.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkDay.Location = New System.Drawing.Point(368, 135)
        Me.chkDay.Name = "chkDay"
        Me.chkDay.Size = New System.Drawing.Size(148, 29)
        Me.chkDay.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkDay.TabIndex = 10
        Me.chkDay.Text = "Суточные"
        '
        'myPanel
        '
        Me.myPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.myPanel.AutoScroll = True
        Me.myPanel.BackColor = System.Drawing.Color.White
        Me.myPanel.Location = New System.Drawing.Point(8, 6)
        Me.myPanel.Name = "myPanel"
        Me.myPanel.Size = New System.Drawing.Size(354, 416)
        Me.myPanel.TabIndex = 15
        '
        'chkSEZON
        '
        Me.chkSEZON.AutoSize = True
        Me.chkSEZON.Checked = True
        Me.chkSEZON.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSEZON.Location = New System.Drawing.Point(374, 15)
        Me.chkSEZON.Name = "chkSEZON"
        Me.chkSEZON.Size = New System.Drawing.Size(133, 17)
        Me.chkSEZON.TabIndex = 16
        Me.chkSEZON.Text = "Отопительный сезон"
        Me.chkSEZON.UseVisualStyleBackColor = True
        '
        'gv
        '
        Me.gv.AllowUserToAddRows = False
        Me.gv.AllowUserToDeleteRows = False
        Me.gv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gv.Location = New System.Drawing.Point(522, 37)
        Me.gv.MultiSelect = False
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RowHeadersVisible = False
        Me.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gv.ShowCellErrors = False
        Me.gv.ShowCellToolTips = False
        Me.gv.ShowEditingIcon = False
        Me.gv.ShowRowErrors = False
        Me.gv.Size = New System.Drawing.Size(316, 384)
        Me.gv.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(521, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(267, 16)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Договорные значения (справочно)"
        '
        'frmBounds
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(852, 436)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.gv)
        Me.Controls.Add(Me.chkSEZON)
        Me.Controls.Add(Me.cmdValues)
        Me.Controls.Add(Me.myPanel)
        Me.Controls.Add(Me.cmdParams)
        Me.Controls.Add(Me.chkDay)
        Me.Controls.Add(Me.chkHour)
        Me.Controls.Add(Me.chkMoment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBounds"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Границы параметров"
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdParams As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraToolTipManager1 As Infragistics.Win.UltraWinToolTip.UltraToolTipManager
    Friend WithEvents chkMoment As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkDay As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkHour As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents cmdValues As Infragistics.Win.Misc.UltraButton
    Friend WithEvents myPanel As System.Windows.Forms.Panel
    Friend WithEvents chkSEZON As System.Windows.Forms.CheckBox
    Friend WithEvents gv As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
