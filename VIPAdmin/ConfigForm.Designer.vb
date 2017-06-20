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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfigForm))
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.chkIP = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
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
        Me.chkHideRow = New System.Windows.Forms.CheckBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.TextBoxID_BD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlPlanCall = New VIPAdmin.editTPLT_PLANCALL()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.pnlBModems = New VIPAdmin.editTPLT_CONNECT()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.pnlContract = New VIPAdmin.editTPLT_CONTRACT()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
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
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(2, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(772, 512)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.UltraPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(764, 486)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Основные данные"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'UltraPanel1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Control
        Me.UltraPanel1.Appearance = Appearance1
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.chkIP)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label5)
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
        Me.UltraPanel1.ClientArea.Controls.Add(Me.GroupBox1)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.chkHideRow)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtName)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.TextBoxID_BD)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label7)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label6)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label4)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label1)
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(764, 480)
        Me.UltraPanel1.TabIndex = 23
        '
        'chkIP
        '
        Me.chkIP.AutoSize = True
        Me.chkIP.Location = New System.Drawing.Point(719, 201)
        Me.chkIP.Name = "chkIP"
        Me.chkIP.Size = New System.Drawing.Size(15, 14)
        Me.chkIP.TabIndex = 64
        Me.chkIP.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(621, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "IP-Опрос"
        '
        'txtphone2
        '
        Me.txtphone2.Location = New System.Drawing.Point(507, 160)
        Me.txtphone2.Name = "txtphone2"
        Me.txtphone2.Size = New System.Drawing.Size(239, 20)
        Me.txtphone2.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(408, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "Тел. 2"
        '
        'txtcphone1
        '
        Me.txtcphone1.Location = New System.Drawing.Point(507, 75)
        Me.txtcphone1.Name = "txtcphone1"
        Me.txtcphone1.Size = New System.Drawing.Size(239, 20)
        Me.txtcphone1.TabIndex = 60
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(408, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Тел. 1"
        '
        'txtcfio2
        '
        Me.txtcfio2.Location = New System.Drawing.Point(507, 115)
        Me.txtcfio2.Name = "txtcfio2"
        Me.txtcfio2.Size = New System.Drawing.Size(239, 20)
        Me.txtcfio2.TabIndex = 58
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(408, 122)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 13)
        Me.Label9.TabIndex = 57
        Me.Label9.Text = "ФИО 2"
        '
        'txtcfio1
        '
        Me.txtcfio1.Location = New System.Drawing.Point(507, 43)
        Me.txtcfio1.Name = "txtcfio1"
        Me.txtcfio1.Size = New System.Drawing.Size(239, 20)
        Me.txtcfio1.TabIndex = 56
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(408, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "ФИО 1"
        '
        'txtcaddress
        '
        Me.txtcaddress.Location = New System.Drawing.Point(507, 11)
        Me.txtcaddress.Name = "txtcaddress"
        Me.txtcaddress.Size = New System.Drawing.Size(239, 20)
        Me.txtcaddress.TabIndex = 54
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(408, 14)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(38, 13)
        Me.Label17.TabIndex = 53
        Me.Label17.Text = "Адрес"
        '
        'cmbSchema
        '
        Me.cmbSchema.FormattingEnabled = True
        Me.cmbSchema.Location = New System.Drawing.Point(140, 195)
        Me.cmbSchema.Name = "cmbSchema"
        Me.cmbSchema.Size = New System.Drawing.Size(243, 21)
        Me.cmbSchema.TabIndex = 52
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(20, 194)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 13)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "Схема подключения"
        '
        'cmbGRP
        '
        Me.cmbGRP.FormattingEnabled = True
        Me.cmbGRP.Location = New System.Drawing.Point(140, 159)
        Me.cmbGRP.Name = "cmbGRP"
        Me.cmbGRP.Size = New System.Drawing.Size(243, 21)
        Me.cmbGRP.TabIndex = 50
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(20, 158)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 13)
        Me.Label16.TabIndex = 49
        Me.Label16.Text = "Группа"
        '
        'cmbDevtype
        '
        Me.cmbDevtype.FormattingEnabled = True
        Me.cmbDevtype.Location = New System.Drawing.Point(140, 40)
        Me.cmbDevtype.Name = "cmbDevtype"
        Me.cmbDevtype.Size = New System.Drawing.Size(243, 21)
        Me.cmbDevtype.TabIndex = 48
        '
        'cmbWHO
        '
        Me.cmbWHO.FormattingEnabled = True
        Me.cmbWHO.Location = New System.Drawing.Point(140, 118)
        Me.cmbWHO.Name = "cmbWHO"
        Me.cmbWHO.Size = New System.Drawing.Size(243, 21)
        Me.cmbWHO.TabIndex = 47
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(20, 122)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "Поставщик ТЭ"
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
        Me.GroupBox1.Location = New System.Drawing.Point(11, 246)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(735, 191)
        Me.GroupBox1.TabIndex = 45
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
        Me.cmdEditD.UseVisualStyleBackColor = True
        '
        'cmdEditH
        '
        Me.cmdEditH.Image = CType(resources.GetObject("cmdEditH.Image"), System.Drawing.Image)
        Me.cmdEditH.Location = New System.Drawing.Point(670, 33)
        Me.cmdEditH.Name = "cmdEditH"
        Me.cmdEditH.Size = New System.Drawing.Size(47, 50)
        Me.cmdEditH.TabIndex = 55
        Me.cmdEditH.UseVisualStyleBackColor = True
        '
        'cmdSelD
        '
        Me.cmdSelD.Location = New System.Drawing.Point(613, 119)
        Me.cmdSelD.Name = "cmdSelD"
        Me.cmdSelD.Size = New System.Drawing.Size(49, 51)
        Me.cmdSelD.TabIndex = 54
        Me.cmdSelD.Text = "..."
        Me.cmdSelD.UseVisualStyleBackColor = True
        '
        'cmdSelH
        '
        Me.cmdSelH.Location = New System.Drawing.Point(612, 33)
        Me.cmdSelH.Name = "cmdSelH"
        Me.cmdSelH.Size = New System.Drawing.Size(49, 51)
        Me.cmdSelH.TabIndex = 53
        Me.cmdSelH.Text = "..."
        Me.cmdSelH.UseVisualStyleBackColor = True
        '
        'txtD_PAGE
        '
        Me.txtD_PAGE.Location = New System.Drawing.Point(182, 156)
        Me.txtD_PAGE.Name = "txtD_PAGE"
        Me.txtD_PAGE.ReadOnly = True
        Me.txtD_PAGE.Size = New System.Drawing.Size(414, 20)
        Me.txtD_PAGE.TabIndex = 52
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(20, 156)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(149, 13)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Лист шаблона для суточных"
        '
        'txtD_SH
        '
        Me.txtD_SH.Location = New System.Drawing.Point(183, 119)
        Me.txtD_SH.Name = "txtD_SH"
        Me.txtD_SH.ReadOnly = True
        Me.txtD_SH.Size = New System.Drawing.Size(414, 20)
        Me.txtD_SH.TabIndex = 50
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 119)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(152, 13)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Шаблон отчета для суточных"
        '
        'txtH_PAGE
        '
        Me.txtH_PAGE.Location = New System.Drawing.Point(181, 68)
        Me.txtH_PAGE.Name = "txtH_PAGE"
        Me.txtH_PAGE.ReadOnly = True
        Me.txtH_PAGE.Size = New System.Drawing.Size(415, 20)
        Me.txtH_PAGE.TabIndex = 48
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(20, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(145, 13)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Лист шаблона для часовых"
        '
        'txtH_SH
        '
        Me.txtH_SH.Location = New System.Drawing.Point(182, 33)
        Me.txtH_SH.Name = "txtH_SH"
        Me.txtH_SH.ReadOnly = True
        Me.txtH_SH.Size = New System.Drawing.Size(414, 20)
        Me.txtH_SH.TabIndex = 46
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(21, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(148, 13)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Шаблон отчета для часовых"
        '
        'chkHideRow
        '
        Me.chkHideRow.AutoSize = True
        Me.chkHideRow.Location = New System.Drawing.Point(507, 202)
        Me.chkHideRow.Name = "chkHideRow"
        Me.chkHideRow.Size = New System.Drawing.Size(15, 14)
        Me.chkHideRow.TabIndex = 35
        Me.chkHideRow.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(140, 79)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(243, 20)
        Me.txtName.TabIndex = 32
        '
        'TextBoxID_BD
        '
        Me.TextBoxID_BD.Location = New System.Drawing.Point(140, 14)
        Me.TextBoxID_BD.Name = "TextBoxID_BD"
        Me.TextBoxID_BD.ReadOnly = True
        Me.TextBoxID_BD.Size = New System.Drawing.Size(243, 20)
        Me.TextBoxID_BD.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(408, 199)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Не отображать"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Название"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Тип устройства"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "ID устройства"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pnlPlanCall)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(764, 486)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "План опроса"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnlPlanCall
        '
        Me.pnlPlanCall.AutoScroll = True
        Me.pnlPlanCall.Location = New System.Drawing.Point(3, 3)
        Me.pnlPlanCall.Name = "pnlPlanCall"
        Me.pnlPlanCall.Size = New System.Drawing.Size(764, 377)
        Me.pnlPlanCall.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlBModems)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(764, 486)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Настройки протокола"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'pnlBModems
        '
        Me.pnlBModems.AutoScroll = True
        Me.pnlBModems.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pnlBModems.Location = New System.Drawing.Point(3, 3)
        Me.pnlBModems.Name = "pnlBModems"
        Me.pnlBModems.Size = New System.Drawing.Size(765, 362)
        Me.pnlBModems.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.pnlContract)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(764, 486)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Договорные настройки"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'pnlContract
        '
        Me.pnlContract.AutoScroll = True
        Me.pnlContract.Location = New System.Drawing.Point(3, 3)
        Me.pnlContract.Name = "pnlContract"
        Me.pnlContract.Size = New System.Drawing.Size(754, 480)
        Me.pnlContract.TabIndex = 0
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
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents chkHideRow As System.Windows.Forms.CheckBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxID_BD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents pnlPlanCall As editTPLT_PLANCALL
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents pnlBModems As editTPLT_CONNECT
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents pnlContract As VIPAdmin.editTPLT_CONTRACT
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
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
    Friend WithEvents cmdEditD As System.Windows.Forms.Button
    Friend WithEvents cmdEditH As System.Windows.Forms.Button
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
    Friend WithEvents chkIP As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
