<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigForm
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfigForm))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.cmbDanSchema = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtL_NZCOLS = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtNZCOLS = New System.Windows.Forms.TextBox()
        Me.txtLINKED = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtHourShift = New System.Windows.Forms.NumericUpDown()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.chkCOLDWATER = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbSRV = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkIP = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkHideRow = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtFULLADDRESS = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbMaskT = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbMaskD = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbMaskH = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbMaskM = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtphone2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtcphone1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtcfio2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtcfio1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtcaddress = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbSchema = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbGRP = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbDevtype = New System.Windows.Forms.ComboBox()
        Me.cmbWHO = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.TextBoxID_BD = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdEditD = New System.Windows.Forms.Button()
        Me.cmdEditH = New System.Windows.Forms.Button()
        Me.cmdSelD = New System.Windows.Forms.Button()
        Me.cmdSelH = New System.Windows.Forms.Button()
        Me.txtD_PAGE = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtD_SH = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtH_PAGE = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtH_SH = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlPLANCALL = New NodeEditorCtl.editTPLT_PLANCALL()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.pnlBModems = New NodeEditorCtl.editTPLT_CONNECT()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.pnlContract = New NodeEditorCtl.pGrid()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.chkSEZON = New System.Windows.Forms.CheckBox()
        Me.cmdValues = New Infragistics.Win.Misc.UltraButton()
        Me.myPanel = New System.Windows.Forms.Panel()
        Me.cmdParams = New Infragistics.Win.Misc.UltraButton()
        Me.chkDay = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chkHour = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.chkMoment = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.EditAnalizerConfig1 = New NodeEditorCtl.EditAnalizerConfig()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSetup = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.txtHourShift, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(588, 521)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(88, 22)
        Me.ButtonOK.TabIndex = 8
        Me.ButtonOK.Text = "Ок"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(682, 521)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 22)
        Me.ButtonCancel.TabIndex = 9
        Me.ButtonCancel.Text = "Отмена"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(1, 20)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(535, 334)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(2, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(772, 512)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.UltraPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 33)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(764, 475)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Основные данные"
        '
        'UltraPanel1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Control
        Me.UltraPanel1.Appearance = Appearance1
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnSetup)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbDanSchema)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label29)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.GroupBox3)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtLINKED)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label27)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtHourShift)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label25)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.chkCOLDWATER)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label24)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbSRV)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label23)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button5)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button6)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button7)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button8)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.GroupBox2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button4)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button3)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Button1)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtFULLADDRESS)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label22)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbMaskT)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label21)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbMaskD)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label20)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbMaskH)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label19)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbMaskM)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label18)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtphone2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label3)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtcphone1)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label8)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtcfio2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label9)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtcfio1)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtcaddress)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label17)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbSchema)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label15)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbGRP)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label16)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbDevtype)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbWHO)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label14)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtName)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.TextBoxID_BD)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label6)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label4)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label1)
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(764, 480)
        Me.UltraPanel1.TabIndex = 23
        '
        'cmbDanSchema
        '
        Me.cmbDanSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDanSchema.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbDanSchema.FormattingEnabled = True
        Me.cmbDanSchema.Location = New System.Drawing.Point(140, 205)
        Me.cmbDanSchema.Name = "cmbDanSchema"
        Me.cmbDanSchema.Size = New System.Drawing.Size(243, 23)
        Me.cmbDanSchema.TabIndex = 81
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label29.Location = New System.Drawing.Point(10, 208)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(90, 15)
        Me.Label29.TabIndex = 80
        Me.Label29.Text = "Схема danfoss"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.txtL_NZCOLS)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.txtNZCOLS)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(409, 272)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(336, 93)
        Me.GroupBox3.TabIndex = 66
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Проверка полей"
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label28.Location = New System.Drawing.Point(13, 24)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(70, 21)
        Me.Label28.TabIndex = 67
        Me.Label28.Text = "Отопл."
        '
        'txtL_NZCOLS
        '
        Me.txtL_NZCOLS.Location = New System.Drawing.Point(103, 57)
        Me.txtL_NZCOLS.Name = "txtL_NZCOLS"
        Me.txtL_NZCOLS.Size = New System.Drawing.Size(227, 22)
        Me.txtL_NZCOLS.TabIndex = 70
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(13, 60)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(70, 16)
        Me.Label26.TabIndex = 69
        Me.Label26.Text = "Без отоп."
        '
        'txtNZCOLS
        '
        Me.txtNZCOLS.Location = New System.Drawing.Point(103, 21)
        Me.txtNZCOLS.Name = "txtNZCOLS"
        Me.txtNZCOLS.Size = New System.Drawing.Size(227, 22)
        Me.txtNZCOLS.TabIndex = 68
        '
        'txtLINKED
        '
        Me.txtLINKED.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtLINKED.Location = New System.Drawing.Point(141, 368)
        Me.txtLINKED.Name = "txtLINKED"
        Me.txtLINKED.Size = New System.Drawing.Size(247, 22)
        Me.txtLINKED.TabIndex = 49
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label27.Location = New System.Drawing.Point(10, 368)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(106, 16)
        Me.Label27.TabIndex = 48
        Me.Label27.Text = "Связный архив"
        '
        'txtHourShift
        '
        Me.txtHourShift.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtHourShift.Location = New System.Drawing.Point(686, 371)
        Me.txtHourShift.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtHourShift.Minimum = New Decimal(New Integer() {50, 0, 0, -2147483648})
        Me.txtHourShift.Name = "txtHourShift"
        Me.txtHourShift.Size = New System.Drawing.Size(60, 26)
        Me.txtHourShift.TabIndex = 72
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label25.Location = New System.Drawing.Point(415, 377)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(255, 15)
        Me.Label25.TabIndex = 71
        Me.Label25.Text = "Сдвиг при контроле часовых архивов (час)"
        '
        'chkCOLDWATER
        '
        Me.chkCOLDWATER.AutoSize = True
        Me.chkCOLDWATER.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkCOLDWATER.Location = New System.Drawing.Point(140, 445)
        Me.chkCOLDWATER.Name = "chkCOLDWATER"
        Me.chkCOLDWATER.Size = New System.Drawing.Size(15, 14)
        Me.chkCOLDWATER.TabIndex = 53
        Me.chkCOLDWATER.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label24.Location = New System.Drawing.Point(10, 444)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(83, 15)
        Me.Label24.TabIndex = 52
        Me.Label24.Text = "Контроль ХВ"
        '
        'cmbSRV
        '
        Me.cmbSRV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSRV.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbSRV.FormattingEnabled = True
        Me.cmbSRV.Location = New System.Drawing.Point(141, 411)
        Me.cmbSRV.Name = "cmbSRV"
        Me.cmbSRV.Size = New System.Drawing.Size(242, 23)
        Me.cmbSRV.TabIndex = 51
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label23.Location = New System.Drawing.Point(10, 415)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 16)
        Me.Label23.TabIndex = 50
        Me.Label23.Text = "Сервер опроса"
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(361, 333)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(27, 22)
        Me.Button5.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.Button5, "Настроить маску")
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.Location = New System.Drawing.Point(361, 302)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(27, 22)
        Me.Button6.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.Button6, "Настроить маску")
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.Location = New System.Drawing.Point(361, 273)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(27, 22)
        Me.Button7.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.Button7, "Настроить маску")
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.Location = New System.Drawing.Point(361, 244)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(27, 22)
        Me.Button8.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.Button8, "Настроить маску")
        Me.Button8.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkIP)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.chkHideRow)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(412, 388)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(334, 82)
        Me.GroupBox2.TabIndex = 79
        Me.GroupBox2.TabStop = False
        '
        'chkIP
        '
        Me.chkIP.AutoSize = True
        Me.chkIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkIP.Location = New System.Drawing.Point(228, 27)
        Me.chkIP.Name = "chkIP"
        Me.chkIP.Size = New System.Drawing.Size(15, 14)
        Me.chkIP.TabIndex = 74
        Me.chkIP.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 16)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Опрос включен"
        '
        'chkHideRow
        '
        Me.chkHideRow.AutoSize = True
        Me.chkHideRow.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.chkHideRow.Location = New System.Drawing.Point(228, 56)
        Me.chkHideRow.Name = "chkHideRow"
        Me.chkHideRow.Size = New System.Drawing.Size(15, 14)
        Me.chkHideRow.TabIndex = 76
        Me.chkHideRow.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 15)
        Me.Label7.TabIndex = 75
        Me.Label7.Text = "Не отображать"
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button4.Location = New System.Drawing.Point(328, 333)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(27, 22)
        Me.Button4.TabIndex = 46
        Me.Button4.Text = "+"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button3.Location = New System.Drawing.Point(328, 302)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(27, 22)
        Me.Button3.TabIndex = 42
        Me.Button3.Text = "+"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button2.Location = New System.Drawing.Point(328, 273)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(27, 22)
        Me.Button2.TabIndex = 38
        Me.Button2.Text = "+"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Button1.Location = New System.Drawing.Point(328, 244)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(27, 22)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtFULLADDRESS
        '
        Me.txtFULLADDRESS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtFULLADDRESS.Location = New System.Drawing.Point(519, 40)
        Me.txtFULLADDRESS.Multiline = True
        Me.txtFULLADDRESS.Name = "txtFULLADDRESS"
        Me.txtFULLADDRESS.Size = New System.Drawing.Size(227, 60)
        Me.txtFULLADDRESS.TabIndex = 57
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label22.Location = New System.Drawing.Point(406, 44)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(90, 15)
        Me.Label22.TabIndex = 56
        Me.Label22.Text = "Полный адрес"
        '
        'cmbMaskT
        '
        Me.cmbMaskT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaskT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbMaskT.FormattingEnabled = True
        Me.cmbMaskT.Location = New System.Drawing.Point(140, 332)
        Me.cmbMaskT.Name = "cmbMaskT"
        Me.cmbMaskT.Size = New System.Drawing.Size(182, 23)
        Me.cmbMaskT.TabIndex = 45
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label21.Location = New System.Drawing.Point(10, 331)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(102, 15)
        Me.Label21.TabIndex = 44
        Me.Label21.Text = "Маска итоговых"
        '
        'cmbMaskD
        '
        Me.cmbMaskD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaskD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbMaskD.FormattingEnabled = True
        Me.cmbMaskD.Location = New System.Drawing.Point(140, 301)
        Me.cmbMaskD.Name = "cmbMaskD"
        Me.cmbMaskD.Size = New System.Drawing.Size(182, 23)
        Me.cmbMaskD.TabIndex = 41
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label20.Location = New System.Drawing.Point(10, 300)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 15)
        Me.Label20.TabIndex = 40
        Me.Label20.Text = "Маска суточных"
        '
        'cmbMaskH
        '
        Me.cmbMaskH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaskH.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbMaskH.FormattingEnabled = True
        Me.cmbMaskH.Location = New System.Drawing.Point(140, 272)
        Me.cmbMaskH.Name = "cmbMaskH"
        Me.cmbMaskH.Size = New System.Drawing.Size(182, 23)
        Me.cmbMaskH.TabIndex = 37
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label19.Location = New System.Drawing.Point(10, 271)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(95, 15)
        Me.Label19.TabIndex = 36
        Me.Label19.Text = "Маска часовых"
        '
        'cmbMaskM
        '
        Me.cmbMaskM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaskM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbMaskM.FormattingEnabled = True
        Me.cmbMaskM.Location = New System.Drawing.Point(140, 243)
        Me.cmbMaskM.Name = "cmbMaskM"
        Me.cmbMaskM.Size = New System.Drawing.Size(182, 23)
        Me.cmbMaskM.TabIndex = 33
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label18.Location = New System.Drawing.Point(10, 242)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(118, 15)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "Маска мгновенных"
        '
        'txtphone2
        '
        Me.txtphone2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtphone2.Location = New System.Drawing.Point(519, 236)
        Me.txtphone2.Name = "txtphone2"
        Me.txtphone2.Size = New System.Drawing.Size(227, 21)
        Me.txtphone2.TabIndex = 65
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(409, 239)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 15)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "Тел. 2"
        '
        'txtcphone1
        '
        Me.txtcphone1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtcphone1.Location = New System.Drawing.Point(518, 155)
        Me.txtcphone1.Name = "txtcphone1"
        Me.txtcphone1.Size = New System.Drawing.Size(227, 21)
        Me.txtcphone1.TabIndex = 61
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label8.Location = New System.Drawing.Point(405, 158)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 15)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "Тел. 1"
        '
        'txtcfio2
        '
        Me.txtcfio2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtcfio2.Location = New System.Drawing.Point(518, 195)
        Me.txtcfio2.Name = "txtcfio2"
        Me.txtcfio2.Size = New System.Drawing.Size(227, 21)
        Me.txtcfio2.TabIndex = 63
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label9.Location = New System.Drawing.Point(404, 198)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 15)
        Me.Label9.TabIndex = 62
        Me.Label9.Text = "ФИО 2"
        '
        'txtcfio1
        '
        Me.txtcfio1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtcfio1.Location = New System.Drawing.Point(518, 118)
        Me.txtcfio1.Name = "txtcfio1"
        Me.txtcfio1.Size = New System.Drawing.Size(227, 21)
        Me.txtcfio1.TabIndex = 59
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(405, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 15)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "ФИО 1"
        '
        'txtcaddress
        '
        Me.txtcaddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtcaddress.Location = New System.Drawing.Point(519, 11)
        Me.txtcaddress.Name = "txtcaddress"
        Me.txtcaddress.Size = New System.Drawing.Size(227, 21)
        Me.txtcaddress.TabIndex = 55
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label17.Location = New System.Drawing.Point(406, 14)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(41, 15)
        Me.Label17.TabIndex = 54
        Me.Label17.Text = "Адрес"
        '
        'cmbSchema
        '
        Me.cmbSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSchema.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbSchema.FormattingEnabled = True
        Me.cmbSchema.Location = New System.Drawing.Point(141, 176)
        Me.cmbSchema.Name = "cmbSchema"
        Me.cmbSchema.Size = New System.Drawing.Size(243, 23)
        Me.cmbSchema.TabIndex = 31
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label15.Location = New System.Drawing.Point(11, 179)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(124, 15)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Схема подключения"
        '
        'cmbGRP
        '
        Me.cmbGRP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGRP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbGRP.FormattingEnabled = True
        Me.cmbGRP.Location = New System.Drawing.Point(140, 147)
        Me.cmbGRP.Name = "cmbGRP"
        Me.cmbGRP.Size = New System.Drawing.Size(243, 23)
        Me.cmbGRP.TabIndex = 29
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label16.Location = New System.Drawing.Point(10, 150)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(47, 15)
        Me.Label16.TabIndex = 28
        Me.Label16.Text = "Группа"
        '
        'cmbDevtype
        '
        Me.cmbDevtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDevtype.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbDevtype.FormattingEnabled = True
        Me.cmbDevtype.Location = New System.Drawing.Point(140, 40)
        Me.cmbDevtype.Name = "cmbDevtype"
        Me.cmbDevtype.Size = New System.Drawing.Size(147, 23)
        Me.cmbDevtype.TabIndex = 23
        '
        'cmbWHO
        '
        Me.cmbWHO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWHO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbWHO.FormattingEnabled = True
        Me.cmbWHO.Location = New System.Drawing.Point(140, 118)
        Me.cmbWHO.Name = "cmbWHO"
        Me.cmbWHO.Size = New System.Drawing.Size(243, 23)
        Me.cmbWHO.TabIndex = 27
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label14.Location = New System.Drawing.Point(10, 121)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(90, 15)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Поставщик ТЭ"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.txtName.Location = New System.Drawing.Point(140, 79)
        Me.txtName.MaxLength = 24
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(243, 21)
        Me.txtName.TabIndex = 35
        '
        'TextBoxID_BD
        '
        Me.TextBoxID_BD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TextBoxID_BD.Location = New System.Drawing.Point(140, 14)
        Me.TextBoxID_BD.Name = "TextBoxID_BD"
        Me.TextBoxID_BD.ReadOnly = True
        Me.TextBoxID_BD.Size = New System.Drawing.Size(243, 21)
        Me.TextBoxID_BD.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 15)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Название"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 15)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Тип устройства"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 15)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "ID устройства"
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage5.Controls.Add(Me.GroupBox1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 33)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(764, 475)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Шаблоны"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdEditD)
        Me.GroupBox1.Controls.Add(Me.cmdEditH)
        Me.GroupBox1.Controls.Add(Me.cmdSelD)
        Me.GroupBox1.Controls.Add(Me.cmdSelH)
        Me.GroupBox1.Controls.Add(Me.txtD_PAGE)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtD_SH)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtH_PAGE)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtH_SH)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(735, 191)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Отчеты"
        '
        'cmdEditD
        '
        Me.cmdEditD.Image = CType(resources.GetObject("cmdEditD.Image"), System.Drawing.Image)
        Me.cmdEditD.Location = New System.Drawing.Point(673, 118)
        Me.cmdEditD.Name = "cmdEditD"
        Me.cmdEditD.Size = New System.Drawing.Size(44, 51)
        Me.cmdEditD.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.cmdEditD, "Редактирование файла шаблона для суточных")
        Me.cmdEditD.UseVisualStyleBackColor = True
        '
        'cmdEditH
        '
        Me.cmdEditH.Image = CType(resources.GetObject("cmdEditH.Image"), System.Drawing.Image)
        Me.cmdEditH.Location = New System.Drawing.Point(670, 33)
        Me.cmdEditH.Name = "cmdEditH"
        Me.cmdEditH.Size = New System.Drawing.Size(47, 50)
        Me.cmdEditH.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.cmdEditH, "Редактирование файла шаблона для часовых")
        Me.cmdEditH.UseVisualStyleBackColor = True
        '
        'cmdSelD
        '
        Me.cmdSelD.Location = New System.Drawing.Point(613, 119)
        Me.cmdSelD.Name = "cmdSelD"
        Me.cmdSelD.Size = New System.Drawing.Size(49, 51)
        Me.cmdSelD.TabIndex = 54
        Me.cmdSelD.Text = "..."
        Me.ToolTip1.SetToolTip(Me.cmdSelD, "Выбор шаблона для суточных")
        Me.cmdSelD.UseVisualStyleBackColor = True
        '
        'cmdSelH
        '
        Me.cmdSelH.Location = New System.Drawing.Point(612, 33)
        Me.cmdSelH.Name = "cmdSelH"
        Me.cmdSelH.Size = New System.Drawing.Size(49, 51)
        Me.cmdSelH.TabIndex = 53
        Me.cmdSelH.Text = "..."
        Me.ToolTip1.SetToolTip(Me.cmdSelH, "Выбор шаблона для часовых")
        Me.cmdSelH.UseVisualStyleBackColor = True
        '
        'txtD_PAGE
        '
        Me.txtD_PAGE.Location = New System.Drawing.Point(297, 156)
        Me.txtD_PAGE.Name = "txtD_PAGE"
        Me.txtD_PAGE.ReadOnly = True
        Me.txtD_PAGE.Size = New System.Drawing.Size(299, 29)
        Me.txtD_PAGE.TabIndex = 52
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(20, 156)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(261, 24)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Лист шаблона для суточных"
        '
        'txtD_SH
        '
        Me.txtD_SH.Location = New System.Drawing.Point(298, 119)
        Me.txtD_SH.Name = "txtD_SH"
        Me.txtD_SH.ReadOnly = True
        Me.txtD_SH.Size = New System.Drawing.Size(299, 29)
        Me.txtD_SH.TabIndex = 50
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 119)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(270, 24)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Шаблон отчета для суточных"
        '
        'txtH_PAGE
        '
        Me.txtH_PAGE.Location = New System.Drawing.Point(296, 68)
        Me.txtH_PAGE.Name = "txtH_PAGE"
        Me.txtH_PAGE.ReadOnly = True
        Me.txtH_PAGE.Size = New System.Drawing.Size(300, 29)
        Me.txtH_PAGE.TabIndex = 48
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(20, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(252, 24)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Лист шаблона для часовых"
        '
        'txtH_SH
        '
        Me.txtH_SH.Location = New System.Drawing.Point(297, 33)
        Me.txtH_SH.Name = "txtH_SH"
        Me.txtH_SH.ReadOnly = True
        Me.txtH_SH.Size = New System.Drawing.Size(299, 29)
        Me.txtH_SH.TabIndex = 46
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(261, 24)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Шаблон отчета для часовых"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.pnlPLANCALL)
        Me.TabPage2.Location = New System.Drawing.Point(4, 33)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(764, 475)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "План опроса"
        '
        'pnlPLANCALL
        '
        Me.pnlPLANCALL.AutoScroll = True
        Me.pnlPLANCALL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.pnlPLANCALL.Location = New System.Drawing.Point(5, 6)
        Me.pnlPLANCALL.Name = "pnlPLANCALL"
        Me.pnlPLANCALL.Size = New System.Drawing.Size(756, 463)
        Me.pnlPLANCALL.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.pnlBModems)
        Me.TabPage3.Location = New System.Drawing.Point(4, 33)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(764, 475)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Настройки протокола"
        '
        'pnlBModems
        '
        Me.pnlBModems.AutoScroll = True
        Me.pnlBModems.BackColor = System.Drawing.SystemColors.Control
        Me.pnlBModems.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.pnlBModems.Location = New System.Drawing.Point(3, 3)
        Me.pnlBModems.Name = "pnlBModems"
        Me.pnlBModems.Size = New System.Drawing.Size(765, 362)
        Me.pnlBModems.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage4.Controls.Add(Me.pnlContract)
        Me.TabPage4.Location = New System.Drawing.Point(4, 33)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(764, 475)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Договорные настройки"
        '
        'pnlContract
        '
        Me.pnlContract.AutoScroll = True
        Me.pnlContract.BackColor = System.Drawing.SystemColors.Control
        Me.pnlContract.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.pnlContract.Location = New System.Drawing.Point(3, 3)
        Me.pnlContract.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlContract.Name = "pnlContract"
        Me.pnlContract.Size = New System.Drawing.Size(761, 468)
        Me.pnlContract.TabIndex = 0
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.chkSEZON)
        Me.TabPage7.Controls.Add(Me.cmdValues)
        Me.TabPage7.Controls.Add(Me.myPanel)
        Me.TabPage7.Controls.Add(Me.cmdParams)
        Me.TabPage7.Controls.Add(Me.chkDay)
        Me.TabPage7.Controls.Add(Me.chkHour)
        Me.TabPage7.Controls.Add(Me.chkMoment)
        Me.TabPage7.Location = New System.Drawing.Point(4, 33)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(764, 475)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Граничные значения"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'chkSEZON
        '
        Me.chkSEZON.AutoSize = True
        Me.chkSEZON.Checked = True
        Me.chkSEZON.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSEZON.Location = New System.Drawing.Point(372, 12)
        Me.chkSEZON.Name = "chkSEZON"
        Me.chkSEZON.Size = New System.Drawing.Size(222, 28)
        Me.chkSEZON.TabIndex = 23
        Me.chkSEZON.Text = "Отопительный сезон"
        Me.chkSEZON.UseVisualStyleBackColor = True
        '
        'cmdValues
        '
        Me.cmdValues.Location = New System.Drawing.Point(366, 191)
        Me.cmdValues.Name = "cmdValues"
        Me.cmdValues.Size = New System.Drawing.Size(227, 28)
        Me.cmdValues.TabIndex = 21
        Me.cmdValues.Text = "Сохранить"
        '
        'myPanel
        '
        Me.myPanel.AutoScroll = True
        Me.myPanel.BackColor = System.Drawing.Color.White
        Me.myPanel.Location = New System.Drawing.Point(6, 3)
        Me.myPanel.Name = "myPanel"
        Me.myPanel.Size = New System.Drawing.Size(349, 416)
        Me.myPanel.TabIndex = 22
        '
        'cmdParams
        '
        Me.cmdParams.Location = New System.Drawing.Point(366, 392)
        Me.cmdParams.Name = "cmdParams"
        Me.cmdParams.Size = New System.Drawing.Size(227, 27)
        Me.cmdParams.TabIndex = 17
        Me.cmdParams.Text = "Выбор показателей"
        '
        'chkDay
        '
        Me.chkDay.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkDay.Location = New System.Drawing.Point(366, 132)
        Me.chkDay.Name = "chkDay"
        Me.chkDay.Size = New System.Drawing.Size(227, 29)
        Me.chkDay.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkDay.TabIndex = 20
        Me.chkDay.Text = "Суточные"
        '
        'chkHour
        '
        Me.chkHour.Location = New System.Drawing.Point(366, 97)
        Me.chkHour.Name = "chkHour"
        Me.chkHour.Size = New System.Drawing.Size(227, 29)
        Me.chkHour.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkHour.TabIndex = 19
        Me.chkHour.Text = "Часовые"
        '
        'chkMoment
        '
        Me.chkMoment.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkMoment.Checked = True
        Me.chkMoment.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMoment.Location = New System.Drawing.Point(366, 62)
        Me.chkMoment.Name = "chkMoment"
        Me.chkMoment.Size = New System.Drawing.Size(227, 29)
        Me.chkMoment.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkMoment.TabIndex = 18
        Me.chkMoment.Text = "Мгновенные"
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage6.Controls.Add(Me.EditAnalizerConfig1)
        Me.TabPage6.Location = New System.Drawing.Point(4, 33)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(764, 475)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Настройки анализатора"
        '
        'EditAnalizerConfig1
        '
        Me.EditAnalizerConfig1.AutoScroll = True
        Me.EditAnalizerConfig1.BackColor = System.Drawing.SystemColors.Control
        Me.EditAnalizerConfig1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.EditAnalizerConfig1.Location = New System.Drawing.Point(3, 3)
        Me.EditAnalizerConfig1.Name = "EditAnalizerConfig1"
        Me.EditAnalizerConfig1.Size = New System.Drawing.Size(758, 476)
        Me.EditAnalizerConfig1.TabIndex = 0
        '
        'btnSetup
        '
        Me.btnSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnSetup.Location = New System.Drawing.Point(293, 40)
        Me.btnSetup.Name = "btnSetup"
        Me.btnSetup.Size = New System.Drawing.Size(89, 23)
        Me.btnSetup.TabIndex = 82
        Me.btnSetup.Text = "Настроить"
        Me.btnSetup.UseVisualStyleBackColor = True
        '
        'ConfigForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 555)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConfigForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Настройки узла"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.txtHourShift, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.TabPage6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxID_BD As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage

    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents pnlBModems As NodeEditorCtl.editTPLT_CONNECT
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents cmbWHO As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbDevtype As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSchema As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbGRP As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtcaddress As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtcfio2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtcfio1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtphone2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtcphone1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdEditD As System.Windows.Forms.Button
    Friend WithEvents cmdEditH As System.Windows.Forms.Button
    Friend WithEvents cmdSelD As System.Windows.Forms.Button
    Friend WithEvents cmdSelH As System.Windows.Forms.Button
    Friend WithEvents txtD_PAGE As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtD_SH As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtH_PAGE As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtH_SH As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbMaskT As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbMaskD As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbMaskH As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbMaskM As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents EditAnalizerConfig1 As NodeEditorCtl.EditAnalizerConfig
    Friend WithEvents txtFULLADDRESS As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkIP As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkHideRow As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents chkSEZON As System.Windows.Forms.CheckBox
    Friend WithEvents cmdValues As Infragistics.Win.Misc.UltraButton
    Friend WithEvents myPanel As System.Windows.Forms.Panel
    Friend WithEvents cmdParams As Infragistics.Win.Misc.UltraButton
    Friend WithEvents chkDay As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkHour As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkMoment As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents cmbSRV As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents pnlPLANCALL As NodeEditorCtl.editTPLT_PLANCALL
    Friend WithEvents chkCOLDWATER As System.Windows.Forms.CheckBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtHourShift As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlContract As NodeEditorCtl.pGrid
    Friend WithEvents txtLINKED As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtL_NZCOLS As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtNZCOLS As System.Windows.Forms.TextBox
    Friend WithEvents cmbDanSchema As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents btnSetup As System.Windows.Forms.Button
End Class
