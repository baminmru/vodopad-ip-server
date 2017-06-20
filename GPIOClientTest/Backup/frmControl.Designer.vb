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
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(381, 9)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(120, 57)
        Me.cmdConnect.TabIndex = 0
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(530, 10)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(120, 55)
        Me.cmdDisconnect.TabIndex = 1
        Me.cmdDisconnect.Text = "Disconnect"
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
        Me.GroupBox1.Location = New System.Drawing.Point(12, 152)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(204, 191)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Задвижка 1"
        '
        'cmdClose1
        '
        Me.cmdClose1.Location = New System.Drawing.Point(19, 144)
        Me.cmdClose1.Name = "cmdClose1"
        Me.cmdClose1.Size = New System.Drawing.Size(157, 29)
        Me.cmdClose1.TabIndex = 5
        Me.cmdClose1.Text = "Закрыть"
        Me.cmdClose1.UseVisualStyleBackColor = True
        '
        'cmdOpen1
        '
        Me.cmdOpen1.Location = New System.Drawing.Point(18, 106)
        Me.cmdOpen1.Name = "cmdOpen1"
        Me.cmdOpen1.Size = New System.Drawing.Size(158, 27)
        Me.cmdOpen1.TabIndex = 4
        Me.cmdOpen1.Text = "Открыть"
        Me.cmdOpen1.UseVisualStyleBackColor = True
        '
        'lblClose1
        '
        Me.lblClose1.BackColor = System.Drawing.Color.LightGray
        Me.lblClose1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClose1.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose1.Location = New System.Drawing.Point(146, 64)
        Me.lblClose1.Name = "lblClose1"
        Me.lblClose1.Size = New System.Drawing.Size(31, 13)
        Me.lblClose1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Концевик Закрыто:"
        '
        'lblOpen1
        '
        Me.lblOpen1.BackColor = System.Drawing.Color.LightGray
        Me.lblOpen1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpen1.ForeColor = System.Drawing.Color.LightGray
        Me.lblOpen1.Location = New System.Drawing.Point(146, 25)
        Me.lblOpen1.Name = "lblOpen1"
        Me.lblOpen1.Size = New System.Drawing.Size(31, 13)
        Me.lblOpen1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Концевик Открыто:"
        '
        'cmdPowerOn
        '
        Me.cmdPowerOn.Location = New System.Drawing.Point(12, 99)
        Me.cmdPowerOn.Name = "cmdPowerOn"
        Me.cmdPowerOn.Size = New System.Drawing.Size(243, 35)
        Me.cmdPowerOn.TabIndex = 3
        Me.cmdPowerOn.Text = "Подключить питание"
        Me.cmdPowerOn.UseVisualStyleBackColor = True
        '
        'cmdPowerOff
        '
        Me.cmdPowerOff.Location = New System.Drawing.Point(380, 99)
        Me.cmdPowerOff.Name = "cmdPowerOff"
        Me.cmdPowerOff.Size = New System.Drawing.Size(270, 36)
        Me.cmdPowerOff.TabIndex = 4
        Me.cmdPowerOff.Text = "Отключить питание"
        Me.cmdPowerOff.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
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
        Me.GroupBox2.Location = New System.Drawing.Point(232, 152)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(204, 191)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Задвижка 2"
        '
        'cmdClose2
        '
        Me.cmdClose2.Location = New System.Drawing.Point(19, 144)
        Me.cmdClose2.Name = "cmdClose2"
        Me.cmdClose2.Size = New System.Drawing.Size(157, 29)
        Me.cmdClose2.TabIndex = 5
        Me.cmdClose2.Text = "Закрыть"
        Me.cmdClose2.UseVisualStyleBackColor = True
        '
        'cmdOpen2
        '
        Me.cmdOpen2.Location = New System.Drawing.Point(18, 106)
        Me.cmdOpen2.Name = "cmdOpen2"
        Me.cmdOpen2.Size = New System.Drawing.Size(158, 27)
        Me.cmdOpen2.TabIndex = 4
        Me.cmdOpen2.Text = "Открыть"
        Me.cmdOpen2.UseVisualStyleBackColor = True
        '
        'lblClose2
        '
        Me.lblClose2.BackColor = System.Drawing.Color.LightGray
        Me.lblClose2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClose2.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose2.Location = New System.Drawing.Point(146, 64)
        Me.lblClose2.Name = "lblClose2"
        Me.lblClose2.Size = New System.Drawing.Size(31, 13)
        Me.lblClose2.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Концевик Закрыто:"
        '
        'lblOpen2
        '
        Me.lblOpen2.BackColor = System.Drawing.Color.LightGray
        Me.lblOpen2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpen2.ForeColor = System.Drawing.Color.LightGray
        Me.lblOpen2.Location = New System.Drawing.Point(146, 25)
        Me.lblOpen2.Name = "lblOpen2"
        Me.lblOpen2.Size = New System.Drawing.Size(31, 13)
        Me.lblOpen2.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Концевик Открыто:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdClose3)
        Me.GroupBox3.Controls.Add(Me.cmdOpen3)
        Me.GroupBox3.Controls.Add(Me.lblClose3)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblOpen3)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Location = New System.Drawing.Point(446, 152)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(204, 191)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Задвижка 3"
        '
        'cmdClose3
        '
        Me.cmdClose3.Location = New System.Drawing.Point(19, 144)
        Me.cmdClose3.Name = "cmdClose3"
        Me.cmdClose3.Size = New System.Drawing.Size(157, 29)
        Me.cmdClose3.TabIndex = 5
        Me.cmdClose3.Text = "Закрыть"
        Me.cmdClose3.UseVisualStyleBackColor = True
        '
        'cmdOpen3
        '
        Me.cmdOpen3.Location = New System.Drawing.Point(18, 106)
        Me.cmdOpen3.Name = "cmdOpen3"
        Me.cmdOpen3.Size = New System.Drawing.Size(158, 27)
        Me.cmdOpen3.TabIndex = 4
        Me.cmdOpen3.Text = "Открыть"
        Me.cmdOpen3.UseVisualStyleBackColor = True
        '
        'lblClose3
        '
        Me.lblClose3.BackColor = System.Drawing.Color.LightGray
        Me.lblClose3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClose3.ForeColor = System.Drawing.Color.LightGray
        Me.lblClose3.Location = New System.Drawing.Point(146, 64)
        Me.lblClose3.Name = "lblClose3"
        Me.lblClose3.Size = New System.Drawing.Size(31, 13)
        Me.lblClose3.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 64)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(106, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Концевик Закрыто:"
        '
        'lblOpen3
        '
        Me.lblOpen3.BackColor = System.Drawing.Color.LightGray
        Me.lblOpen3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpen3.ForeColor = System.Drawing.Color.LightGray
        Me.lblOpen3.Location = New System.Drawing.Point(146, 25)
        Me.lblOpen3.Name = "lblOpen3"
        Me.lblOpen3.Size = New System.Drawing.Size(31, 13)
        Me.lblOpen3.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(106, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Концевик Открыто:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 49)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Порт:"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(57, 47)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.ReadOnly = True
        Me.txtPort.Size = New System.Drawing.Size(198, 20)
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
        Me.txtIP.Location = New System.Drawing.Point(57, 13)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(198, 21)
        Me.txtIP.TabIndex = 11
        '
        'frmControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 353)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmControl"
        Me.Text = "Управление задвижками. Версия от 16.12.2010"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
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
End Class
