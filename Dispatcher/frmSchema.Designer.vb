<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchema
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
        Dim UltraToolTipInfo3 As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Выбор показателей для отображения на схеме", Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
        Dim UltraToolTipInfo2 As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Размещение показателей на схеме", Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
        Dim UltraToolTipInfo1 As Infragistics.Win.UltraWinToolTip.UltraToolTipInfo = New Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Задать граничные значения показателей", Infragistics.Win.ToolTipImage.[Default], Nothing, Infragistics.Win.DefaultableBoolean.[Default])
        Me.picSchema = New System.Windows.Forms.PictureBox()
        Me.cmdParams = New Infragistics.Win.Misc.UltraButton()
        Me.UltraToolTipManager1 = New Infragistics.Win.UltraWinToolTip.UltraToolTipManager(Me.components)
        Me.cmdSetup = New Infragistics.Win.Misc.UltraButton()
        Me.cmdValues = New Infragistics.Win.Misc.UltraButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.chkMoment = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chkHour = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chkDay = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.UltraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox()
        Me.chkSEZON = New System.Windows.Forms.CheckBox()
        Me.gv = New System.Windows.Forms.DataGridView()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        CType(Me.picSchema, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraGroupBox1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picSchema
        '
        Me.picSchema.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picSchema.Location = New System.Drawing.Point(12, 5)
        Me.picSchema.Name = "picSchema"
        Me.picSchema.Size = New System.Drawing.Size(630, 500)
        Me.picSchema.TabIndex = 1
        Me.picSchema.TabStop = False
        '
        'cmdParams
        '
        Me.cmdParams.Location = New System.Drawing.Point(6, 56)
        Me.cmdParams.Name = "cmdParams"
        Me.cmdParams.Size = New System.Drawing.Size(148, 27)
        Me.cmdParams.TabIndex = 6
        Me.cmdParams.Text = "Выбор показателей"
        UltraToolTipInfo3.ToolTipText = "Выбор показателей для отображения на схеме"
        Me.UltraToolTipManager1.SetUltraToolTip(Me.cmdParams, UltraToolTipInfo3)
        '
        'UltraToolTipManager1
        '
        Me.UltraToolTipManager1.ContainingControl = Me
        '
        'cmdSetup
        '
        Me.cmdSetup.Location = New System.Drawing.Point(6, 89)
        Me.cmdSetup.Name = "cmdSetup"
        Me.cmdSetup.Size = New System.Drawing.Size(148, 27)
        Me.cmdSetup.TabIndex = 7
        Me.cmdSetup.Text = "Рамещение показателей"
        UltraToolTipInfo2.ToolTipText = "Размещение показателей на схеме"
        Me.UltraToolTipManager1.SetUltraToolTip(Me.cmdSetup, UltraToolTipInfo2)
        '
        'cmdValues
        '
        Me.cmdValues.Location = New System.Drawing.Point(6, 22)
        Me.cmdValues.Name = "cmdValues"
        Me.cmdValues.Size = New System.Drawing.Size(148, 28)
        Me.cmdValues.TabIndex = 11
        Me.cmdValues.Text = "Граничные значения"
        UltraToolTipInfo1.ToolTipText = "Задать граничные значения показателей"
        Me.UltraToolTipManager1.SetUltraToolTip(Me.cmdValues, UltraToolTipInfo1)
        '
        'Timer1
        '
        Me.Timer1.Interval = 59000
        '
        'chkMoment
        '
        Me.chkMoment.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkMoment.Checked = True
        Me.chkMoment.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMoment.Location = New System.Drawing.Point(654, 57)
        Me.chkMoment.Name = "chkMoment"
        Me.chkMoment.Size = New System.Drawing.Size(148, 29)
        Me.chkMoment.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkMoment.TabIndex = 8
        Me.chkMoment.Text = "Мгновенные"
        '
        'chkHour
        '
        Me.chkHour.Location = New System.Drawing.Point(654, 92)
        Me.chkHour.Name = "chkHour"
        Me.chkHour.Size = New System.Drawing.Size(148, 29)
        Me.chkHour.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkHour.TabIndex = 9
        Me.chkHour.Text = "Часовые"
        '
        'chkDay
        '
        Me.chkDay.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkDay.Location = New System.Drawing.Point(654, 127)
        Me.chkDay.Name = "chkDay"
        Me.chkDay.Size = New System.Drawing.Size(148, 29)
        Me.chkDay.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkDay.TabIndex = 10
        Me.chkDay.Text = "Суточные"
        '
        'UltraGroupBox1
        '
        Me.UltraGroupBox1.Controls.Add(Me.cmdValues)
        Me.UltraGroupBox1.Controls.Add(Me.cmdSetup)
        Me.UltraGroupBox1.Controls.Add(Me.cmdParams)
        Me.UltraGroupBox1.Location = New System.Drawing.Point(648, 175)
        Me.UltraGroupBox1.Name = "UltraGroupBox1"
        Me.UltraGroupBox1.Size = New System.Drawing.Size(165, 136)
        Me.UltraGroupBox1.TabIndex = 14
        Me.UltraGroupBox1.Text = "Настройка узла"
        '
        'chkSEZON
        '
        Me.chkSEZON.AutoSize = True
        Me.chkSEZON.Checked = True
        Me.chkSEZON.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSEZON.Location = New System.Drawing.Point(654, 12)
        Me.chkSEZON.Name = "chkSEZON"
        Me.chkSEZON.Size = New System.Drawing.Size(133, 17)
        Me.chkSEZON.TabIndex = 17
        Me.chkSEZON.Text = "Отопительный сезон"
        Me.chkSEZON.UseVisualStyleBackColor = True
        '
        'gv
        '
        Me.gv.AllowUserToAddRows = False
        Me.gv.AllowUserToDeleteRows = False
        Me.gv.AllowUserToResizeColumns = False
        Me.gv.AllowUserToResizeRows = False
        Me.gv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gv.Location = New System.Drawing.Point(334, 514)
        Me.gv.MultiSelect = False
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.RowHeadersVisible = False
        Me.gv.Size = New System.Drawing.Size(479, 68)
        Me.gv.TabIndex = 18
        '
        'txtInfo
        '
        Me.txtInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtInfo.Location = New System.Drawing.Point(12, 514)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.Size = New System.Drawing.Size(308, 66)
        Me.txtInfo.TabIndex = 19
        '
        'frmSchema
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(822, 594)
        Me.Controls.Add(Me.txtInfo)
        Me.Controls.Add(Me.gv)
        Me.Controls.Add(Me.chkSEZON)
        Me.Controls.Add(Me.UltraGroupBox1)
        Me.Controls.Add(Me.chkDay)
        Me.Controls.Add(Me.chkHour)
        Me.Controls.Add(Me.chkMoment)
        Me.Controls.Add(Me.picSchema)
        Me.Name = "frmSchema"
        Me.Text = "Схема подключения"
        CType(Me.picSchema, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraGroupBox1.ResumeLayout(False)
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picSchema As System.Windows.Forms.PictureBox
    Friend WithEvents cmdParams As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraToolTipManager1 As Infragistics.Win.UltraWinToolTip.UltraToolTipManager
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmdSetup As Infragistics.Win.Misc.UltraButton
    Friend WithEvents chkMoment As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkDay As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkHour As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents cmdValues As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents chkSEZON As System.Windows.Forms.CheckBox
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents gv As System.Windows.Forms.DataGridView

End Class
