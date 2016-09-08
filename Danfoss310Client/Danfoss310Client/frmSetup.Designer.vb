<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDev = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtApp = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optSer2Net = New System.Windows.Forms.RadioButton()
        Me.txtIPPORT = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBaud = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.optCOM = New System.Windows.Forms.RadioButton()
        Me.optTCP = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.opf = New System.Windows.Forms.OpenFileDialog()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtID_BD = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 367)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(177, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Файл с параметрами устройства"
        '
        'txtDev
        '
        Me.txtDev.Location = New System.Drawing.Point(18, 35)
        Me.txtDev.Name = "txtDev"
        Me.txtDev.ReadOnly = True
        Me.txtDev.Size = New System.Drawing.Size(359, 20)
        Me.txtDev.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(385, 35)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(34, 19)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(182, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Файл с параметрами приложения"
        '
        'txtApp
        '
        Me.txtApp.Location = New System.Drawing.Point(16, 92)
        Me.txtApp.Name = "txtApp"
        Me.txtApp.ReadOnly = True
        Me.txtApp.Size = New System.Drawing.Size(361, 20)
        Me.txtApp.TabIndex = 5
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(385, 92)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(35, 19)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optSer2Net)
        Me.GroupBox1.Controls.Add(Me.txtIPPORT)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtBaud)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPort)
        Me.GroupBox1.Controls.Add(Me.txtIP)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.optCOM)
        Me.GroupBox1.Controls.Add(Me.optTCP)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 122)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(399, 165)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Параметры подключения"
        '
        'optSer2Net
        '
        Me.optSer2Net.AutoSize = True
        Me.optSer2Net.Location = New System.Drawing.Point(85, 36)
        Me.optSer2Net.Name = "optSer2Net"
        Me.optSer2Net.Size = New System.Drawing.Size(70, 17)
        Me.optSer2Net.TabIndex = 10
        Me.optSer2Net.TabStop = True
        Me.optSer2Net.Text = "Ser 2 Net"
        Me.optSer2Net.UseVisualStyleBackColor = True
        '
        'txtIPPORT
        '
        Me.txtIPPORT.Location = New System.Drawing.Point(62, 89)
        Me.txtIPPORT.Name = "txtIPPORT"
        Me.txtIPPORT.Size = New System.Drawing.Size(116, 20)
        Me.txtIPPORT.TabIndex = 9
        Me.txtIPPORT.Text = "502"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Порт"
        '
        'txtBaud
        '
        Me.txtBaud.Location = New System.Drawing.Point(261, 89)
        Me.txtBaud.Name = "txtBaud"
        Me.txtBaud.Size = New System.Drawing.Size(116, 20)
        Me.txtBaud.TabIndex = 7
        Me.txtBaud.Text = "38400"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(195, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Скорость"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(199, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Порт"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(258, 61)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(120, 20)
        Me.txtPort.TabIndex = 4
        Me.txtPort.Text = "COM1"
        '
        'txtIP
        '
        Me.txtIP.Location = New System.Drawing.Point(62, 60)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(116, 20)
        Me.txtIP.TabIndex = 3
        Me.txtIP.Text = "192.168.1.100"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "IP"
        '
        'optCOM
        '
        Me.optCOM.AutoSize = True
        Me.optCOM.Location = New System.Drawing.Point(202, 36)
        Me.optCOM.Name = "optCOM"
        Me.optCOM.Size = New System.Drawing.Size(49, 17)
        Me.optCOM.TabIndex = 1
        Me.optCOM.Text = "COM"
        Me.optCOM.UseVisualStyleBackColor = True
        '
        'optTCP
        '
        Me.optTCP.AutoSize = True
        Me.optTCP.Checked = True
        Me.optTCP.Location = New System.Drawing.Point(20, 36)
        Me.optTCP.Name = "optTCP"
        Me.optTCP.Size = New System.Drawing.Size(46, 17)
        Me.optTCP.TabIndex = 0
        Me.optTCP.TabStop = True
        Me.optTCP.Text = "TCP"
        Me.optTCP.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 298)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "MODBUS ID устройства"
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(192, 296)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(225, 20)
        Me.txtID.TabIndex = 9
        Me.txtID.Text = "1"
        '
        'opf
        '
        Me.opf.DefaultExt = "xml"
        Me.opf.Filter = "xml files|*.xml"
        Me.opf.InitialDirectory = "."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 331)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "ID_BD"
        '
        'txtID_BD
        '
        Me.txtID_BD.Location = New System.Drawing.Point(193, 326)
        Me.txtID_BD.Name = "txtID_BD"
        Me.txtID_BD.Size = New System.Drawing.Size(223, 20)
        Me.txtID_BD.TabIndex = 11
        '
        'frmSetup
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 408)
        Me.Controls.Add(Me.txtID_BD)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtApp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtDev)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Настройки"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDev As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtApp As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents optCOM As System.Windows.Forms.RadioButton
    Friend WithEvents optTCP As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents opf As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtBaud As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIPPORT As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents optSer2Net As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtID_BD As System.Windows.Forms.TextBox

End Class
