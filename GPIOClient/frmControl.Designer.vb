<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmControl))
        Me.cmdConnect = New System.Windows.Forms.Button()
        Me.cmdDisconnect = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdClose1 = New System.Windows.Forms.Button()
        Me.cmdOpen1 = New System.Windows.Forms.Button()
        Me.lblClose1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblOpen1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdPowerOn = New System.Windows.Forms.Button()
        Me.cmdPowerOff = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdClose2 = New System.Windows.Forms.Button()
        Me.cmdOpen2 = New System.Windows.Forms.Button()
        Me.lblClose2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblOpen2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmdClose3 = New System.Windows.Forms.Button()
        Me.cmdOpen3 = New System.Windows.Forms.Button()
        Me.lblClose3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblOpen3 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtIP = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.p3 = New System.Windows.Forms.RadioButton()
        Me.p2 = New System.Windows.Forms.RadioButton()
        Me.p0 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(442, 3)
        Me.cmdConnect.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(199, 105)
        Me.cmdConnect.TabIndex = 0
        Me.cmdConnect.Text = "Подключиться"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(666, 3)
        Me.cmdDisconnect.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(199, 105)
        Me.cmdDisconnect.TabIndex = 1
        Me.cmdDisconnect.Text = "Отключиться"
        Me.cmdDisconnect.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdClose1)
        Me.GroupBox1.Controls.Add(Me.cmdOpen1)
        Me.GroupBox1.Controls.Add(Me.lblClose1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblOpen1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 220)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Size = New System.Drawing.Size(258, 353)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Задвижка 1"
        '
        'cmdClose1
        '
        Me.cmdClose1.Location = New System.Drawing.Point(38, 266)
        Me.cmdClose1.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdClose1.Name = "cmdClose1"
        Me.cmdClose1.Size = New System.Drawing.Size(187, 50)
        Me.cmdClose1.TabIndex = 5
        Me.cmdClose1.Text = "Закрыть"
        Me.cmdClose1.UseVisualStyleBackColor = True
        '
        'cmdOpen1
        '
        Me.cmdOpen1.Location = New System.Drawing.Point(36, 196)
        Me.cmdOpen1.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdOpen1.Name = "cmdOpen1"
        Me.cmdOpen1.Size = New System.Drawing.Size(187, 50)
        Me.cmdOpen1.TabIndex = 4
        Me.cmdOpen1.Text = "Открыть"
        Me.cmdOpen1.UseVisualStyleBackColor = True
        '
        'lblClose1
        '
        Me.lblClose1.BackColor = System.Drawing.Color.LightGray
        Me.lblClose1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClose1.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose1.Location = New System.Drawing.Point(138, 117)
        Me.lblClose1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblClose1.Name = "lblClose1"
        Me.lblClose1.Size = New System.Drawing.Size(85, 47)
        Me.lblClose1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 118)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 24)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Закрыто:"
        '
        'lblOpen1
        '
        Me.lblOpen1.BackColor = System.Drawing.Color.LightGray
        Me.lblOpen1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpen1.ForeColor = System.Drawing.Color.LightGray
        Me.lblOpen1.Location = New System.Drawing.Point(138, 46)
        Me.lblOpen1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblOpen1.Name = "lblOpen1"
        Me.lblOpen1.Size = New System.Drawing.Size(85, 47)
        Me.lblOpen1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 46)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Открыто:"
        '
        'cmdPowerOn
        '
        Me.cmdPowerOn.Location = New System.Drawing.Point(442, 120)
        Me.cmdPowerOn.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdPowerOn.Name = "cmdPowerOn"
        Me.cmdPowerOn.Size = New System.Drawing.Size(199, 91)
        Me.cmdPowerOn.TabIndex = 3
        Me.cmdPowerOn.Text = "Подать питание"
        Me.cmdPowerOn.UseVisualStyleBackColor = True
        '
        'cmdPowerOff
        '
        Me.cmdPowerOff.Location = New System.Drawing.Point(666, 120)
        Me.cmdPowerOff.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdPowerOff.Name = "cmdPowerOff"
        Me.cmdPowerOff.Size = New System.Drawing.Size(199, 91)
        Me.cmdPowerOff.TabIndex = 4
        Me.cmdPowerOff.Text = "Выключить питание"
        Me.cmdPowerOff.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 29)
        Me.Label2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 24)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Узел:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdClose2)
        Me.GroupBox2.Controls.Add(Me.cmdOpen2)
        Me.GroupBox2.Controls.Add(Me.lblClose2)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.lblOpen2)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(308, 220)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Size = New System.Drawing.Size(258, 353)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Задвижка 2"
        '
        'cmdClose2
        '
        Me.cmdClose2.Location = New System.Drawing.Point(38, 266)
        Me.cmdClose2.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdClose2.Name = "cmdClose2"
        Me.cmdClose2.Size = New System.Drawing.Size(187, 50)
        Me.cmdClose2.TabIndex = 5
        Me.cmdClose2.Text = "Закрыть"
        Me.cmdClose2.UseVisualStyleBackColor = True
        '
        'cmdOpen2
        '
        Me.cmdOpen2.Location = New System.Drawing.Point(36, 196)
        Me.cmdOpen2.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdOpen2.Name = "cmdOpen2"
        Me.cmdOpen2.Size = New System.Drawing.Size(187, 50)
        Me.cmdOpen2.TabIndex = 4
        Me.cmdOpen2.Text = "Открыть"
        Me.cmdOpen2.UseVisualStyleBackColor = True
        '
        'lblClose2
        '
        Me.lblClose2.BackColor = System.Drawing.Color.LightGray
        Me.lblClose2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClose2.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose2.Location = New System.Drawing.Point(131, 118)
        Me.lblClose2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblClose2.Name = "lblClose2"
        Me.lblClose2.Size = New System.Drawing.Size(85, 47)
        Me.lblClose2.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 118)
        Me.Label5.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 24)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Закрыто:"
        '
        'lblOpen2
        '
        Me.lblOpen2.BackColor = System.Drawing.Color.LightGray
        Me.lblOpen2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpen2.ForeColor = System.Drawing.Color.LightGray
        Me.lblOpen2.Location = New System.Drawing.Point(131, 46)
        Me.lblOpen2.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblOpen2.Name = "lblOpen2"
        Me.lblOpen2.Size = New System.Drawing.Size(85, 47)
        Me.lblOpen2.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 46)
        Me.Label7.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 24)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Открыто:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdClose3)
        Me.GroupBox3.Controls.Add(Me.cmdOpen3)
        Me.GroupBox3.Controls.Add(Me.lblClose3)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblOpen3)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Location = New System.Drawing.Point(607, 220)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox3.Size = New System.Drawing.Size(258, 353)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Задвижка 3"
        '
        'cmdClose3
        '
        Me.cmdClose3.Location = New System.Drawing.Point(38, 266)
        Me.cmdClose3.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdClose3.Name = "cmdClose3"
        Me.cmdClose3.Size = New System.Drawing.Size(187, 50)
        Me.cmdClose3.TabIndex = 5
        Me.cmdClose3.Text = "Закрыть"
        Me.cmdClose3.UseVisualStyleBackColor = True
        '
        'cmdOpen3
        '
        Me.cmdOpen3.Location = New System.Drawing.Point(36, 196)
        Me.cmdOpen3.Margin = New System.Windows.Forms.Padding(6)
        Me.cmdOpen3.Name = "cmdOpen3"
        Me.cmdOpen3.Size = New System.Drawing.Size(187, 50)
        Me.cmdOpen3.TabIndex = 4
        Me.cmdOpen3.Text = "Открыть"
        Me.cmdOpen3.UseVisualStyleBackColor = True
        '
        'lblClose3
        '
        Me.lblClose3.BackColor = System.Drawing.Color.LightGray
        Me.lblClose3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClose3.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose3.Location = New System.Drawing.Point(138, 118)
        Me.lblClose3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblClose3.Name = "lblClose3"
        Me.lblClose3.Size = New System.Drawing.Size(85, 47)
        Me.lblClose3.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(26, 118)
        Me.Label9.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 24)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Закрыто:"
        '
        'lblOpen3
        '
        Me.lblOpen3.BackColor = System.Drawing.Color.LightGray
        Me.lblOpen3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpen3.ForeColor = System.Drawing.Color.LightGray
        Me.lblOpen3.Location = New System.Drawing.Point(138, 46)
        Me.lblOpen3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblOpen3.Name = "lblOpen3"
        Me.lblOpen3.Size = New System.Drawing.Size(85, 47)
        Me.lblOpen3.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(22, 46)
        Me.Label11.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(102, 24)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Открыто:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(28, 82)
        Me.Label12.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(89, 24)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "IP Порт:"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(129, 79)
        Me.txtPort.Margin = New System.Windows.Forms.Padding(6)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.ReadOnly = True
        Me.txtPort.Size = New System.Drawing.Size(290, 29)
        Me.txtPort.TabIndex = 10
        Me.txtPort.Text = "2010"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'txtIP
        '
        Me.txtIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtIP.FormattingEnabled = True
        Me.txtIP.Location = New System.Drawing.Point(131, 26)
        Me.txtIP.Margin = New System.Windows.Forms.Padding(6)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(290, 32)
        Me.txtIP.TabIndex = 11
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioButton1)
        Me.GroupBox4.Controls.Add(Me.p3)
        Me.GroupBox4.Controls.Add(Me.p2)
        Me.GroupBox4.Controls.Add(Me.p0)
        Me.GroupBox4.Location = New System.Drawing.Point(14, 126)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(407, 68)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Порт концевиков"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Enabled = False
        Me.RadioButton1.Location = New System.Drawing.Point(117, 26)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(63, 28)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "P1x"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'p3
        '
        Me.p3.AutoSize = True
        Me.p3.Location = New System.Drawing.Point(313, 26)
        Me.p3.Name = "p3"
        Me.p3.Size = New System.Drawing.Size(63, 28)
        Me.p3.TabIndex = 2
        Me.p3.Text = "P3x"
        Me.p3.UseVisualStyleBackColor = True
        '
        'p2
        '
        Me.p2.AutoSize = True
        Me.p2.Location = New System.Drawing.Point(218, 26)
        Me.p2.Name = "p2"
        Me.p2.Size = New System.Drawing.Size(63, 28)
        Me.p2.TabIndex = 1
        Me.p2.Text = "P2x"
        Me.p2.UseVisualStyleBackColor = True
        '
        'p0
        '
        Me.p0.AutoSize = True
        Me.p0.Checked = True
        Me.p0.Location = New System.Drawing.Point(6, 28)
        Me.p0.Name = "p0"
        Me.p0.Size = New System.Drawing.Size(63, 28)
        Me.p0.TabIndex = 0
        Me.p0.TabStop = True
        Me.p0.Text = "P0x"
        Me.p0.UseVisualStyleBackColor = True
        '
        'frmControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(878, 578)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdPowerOff)
        Me.Controls.Add(Me.cmdPowerOn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdDisconnect)
        Me.Controls.Add(Me.cmdConnect)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmControl"
        Me.Text = "Управление задвижками."
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdClose1 As System.Windows.Forms.Button
    Friend WithEvents cmdOpen1 As System.Windows.Forms.Button
    Friend WithEvents lblClose1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblOpen1 As System.Windows.Forms.Label
    Friend WithEvents cmdPowerOn As System.Windows.Forms.Button
    Friend WithEvents cmdPowerOff As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClose2 As System.Windows.Forms.Button
    Friend WithEvents cmdOpen2 As System.Windows.Forms.Button
    Friend WithEvents lblClose2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblOpen2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClose3 As System.Windows.Forms.Button
    Friend WithEvents cmdOpen3 As System.Windows.Forms.Button
    Friend WithEvents lblClose3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblOpen3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtIP As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents p3 As System.Windows.Forms.RadioButton
    Friend WithEvents p2 As System.Windows.Forms.RadioButton
    Friend WithEvents p0 As System.Windows.Forms.RadioButton
End Class
