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
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.cmbTransport = New System.Windows.Forms.ComboBox()
        Me.chkHideRow = New System.Windows.Forms.CheckBox()
        Me.chkIP = New System.Windows.Forms.CheckBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.TextBoxDevice = New System.Windows.Forms.TextBox()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.TextBoxIP = New System.Windows.Forms.TextBox()
        Me.TextBoxID_BD = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.pnlPlanCall = New STKDispatcher.editTPLT_PLANCALL()
        Me.pnlBModems = New STKDispatcher.editTPLT_CONNECT()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(588, 403)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(88, 22)
        Me.ButtonOK.TabIndex = 8
        Me.ButtonOK.Text = "Ок"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(682, 403)
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
        Me.TabControl1.Location = New System.Drawing.Point(2, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(772, 394)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.UltraPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(764, 368)
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
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmbTransport)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.chkHideRow)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.chkIP)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtPort)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txtName)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.TextBoxDevice)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.TextBoxPassword)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.TextBoxIP)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.TextBoxID_BD)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label9)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label8)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label7)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label6)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label5)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label4)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label3)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Label1)
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(764, 368)
        Me.UltraPanel1.TabIndex = 23
        '
        'cmbTransport
        '
        Me.cmbTransport.FormattingEnabled = True
        Me.cmbTransport.Items.AddRange(New Object() {"MODEM", "COM", "NPORT", "VSX"})
        Me.cmbTransport.Location = New System.Drawing.Point(118, 124)
        Me.cmbTransport.Name = "cmbTransport"
        Me.cmbTransport.Size = New System.Drawing.Size(338, 21)
        Me.cmbTransport.TabIndex = 36
        '
        'chkHideRow
        '
        Me.chkHideRow.AutoSize = True
        Me.chkHideRow.Location = New System.Drawing.Point(119, 102)
        Me.chkHideRow.Name = "chkHideRow"
        Me.chkHideRow.Size = New System.Drawing.Size(15, 14)
        Me.chkHideRow.TabIndex = 35
        Me.chkHideRow.UseVisualStyleBackColor = True
        '
        'chkIP
        '
        Me.chkIP.AutoSize = True
        Me.chkIP.Location = New System.Drawing.Point(118, 229)
        Me.chkIP.Name = "chkIP"
        Me.chkIP.Size = New System.Drawing.Size(15, 14)
        Me.chkIP.TabIndex = 34
        Me.chkIP.UseVisualStyleBackColor = True
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(117, 177)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(338, 20)
        Me.txtPort.TabIndex = 33
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(117, 69)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(339, 20)
        Me.txtName.TabIndex = 32
        '
        'TextBoxDevice
        '
        Me.TextBoxDevice.Location = New System.Drawing.Point(117, 40)
        Me.TextBoxDevice.Name = "TextBoxDevice"
        Me.TextBoxDevice.ReadOnly = True
        Me.TextBoxDevice.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxDevice.TabIndex = 31
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(117, 203)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.Size = New System.Drawing.Size(338, 20)
        Me.TextBoxPassword.TabIndex = 30
        '
        'TextBoxIP
        '
        Me.TextBoxIP.Location = New System.Drawing.Point(117, 151)
        Me.TextBoxIP.Name = "TextBoxIP"
        Me.TextBoxIP.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxIP.TabIndex = 29
        '
        'TextBoxID_BD
        '
        Me.TextBoxID_BD.Location = New System.Drawing.Point(117, 14)
        Me.TextBoxID_BD.Name = "TextBoxID_BD"
        Me.TextBoxID_BD.ReadOnly = True
        Me.TextBoxID_BD.Size = New System.Drawing.Size(339, 20)
        Me.TextBoxID_BD.TabIndex = 28
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 177)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "IP-Порт"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Транспорт"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Не отображать"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Название"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 227)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "IP-Опрос"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Тип устройства"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 203)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Пароль"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "IP-адрес"
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
        Me.TabPage2.Size = New System.Drawing.Size(764, 368)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "План опроса"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlBModems)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(764, 368)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Настройки протокола"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'pnlPlanCall
        '
        Me.pnlPlanCall.AutoScroll = True
        Me.pnlPlanCall.Location = New System.Drawing.Point(3, 3)
        Me.pnlPlanCall.Name = "pnlPlanCall"
        Me.pnlPlanCall.Size = New System.Drawing.Size(764, 377)
        Me.pnlPlanCall.TabIndex = 0
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
        'ConfigForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 434)
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
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmbTransport As System.Windows.Forms.ComboBox
    Friend WithEvents chkHideRow As System.Windows.Forms.CheckBox
    Friend WithEvents chkIP As System.Windows.Forms.CheckBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDevice As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxIP As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxID_BD As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents pnlPlanCall As STKDispatcher.editTPLT_PLANCALL
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents pnlBModems As STKDispatcher.editTPLT_CONNECT
End Class
