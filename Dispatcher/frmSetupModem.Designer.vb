<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetupModem
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdFreeModem = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbM = New System.Windows.Forms.ComboBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optPulse = New System.Windows.Forms.RadioButton()
        Me.OptTone = New System.Windows.Forms.RadioButton()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.lstCom = New System.Windows.Forms.ListBox()
        Me.cmdDelModem = New System.Windows.Forms.Button()
        Me.cmdAddModem = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmdSaveM = New System.Windows.Forms.Button()
        Me.txtINI = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFull = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtShort = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstM = New System.Windows.Forms.ListBox()
        Me.cmdDelM = New System.Windows.Forms.Button()
        Me.cmdAddM = New System.Windows.Forms.Button()
        Me.optGSM = New System.Windows.Forms.RadioButton()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdFreeModem)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmbM)
        Me.GroupBox2.Controls.Add(Me.cmdSave)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.txtPort)
        Me.GroupBox2.Controls.Add(Me.lstCom)
        Me.GroupBox2.Controls.Add(Me.cmdDelModem)
        Me.GroupBox2.Controls.Add(Me.cmdAddModem)
        Me.GroupBox2.Location = New System.Drawing.Point(17, 248)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(516, 204)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Порты"
        '
        'cmdFreeModem
        '
        Me.cmdFreeModem.Location = New System.Drawing.Point(326, 16)
        Me.cmdFreeModem.Name = "cmdFreeModem"
        Me.cmdFreeModem.Size = New System.Drawing.Size(172, 26)
        Me.cmdFreeModem.TabIndex = 18
        Me.cmdFreeModem.Text = "Освободить принудительно"
        Me.cmdFreeModem.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(197, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Порт"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(202, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Модем"
        '
        'cmbM
        '
        Me.cmbM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbM.FormattingEnabled = True
        Me.cmbM.Location = New System.Drawing.Point(200, 126)
        Me.cmbM.Name = "cmbM"
        Me.cmbM.Size = New System.Drawing.Size(303, 21)
        Me.cmbM.TabIndex = 16
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(198, 160)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(305, 27)
        Me.cmdSave.TabIndex = 17
        Me.cmdSave.Text = "Сохранить"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optGSM)
        Me.GroupBox1.Controls.Add(Me.optPulse)
        Me.GroupBox1.Controls.Add(Me.OptTone)
        Me.GroupBox1.Location = New System.Drawing.Point(281, 59)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 44)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Тип линии"
        '
        'optPulse
        '
        Me.optPulse.AutoSize = True
        Me.optPulse.Location = New System.Drawing.Point(83, 16)
        Me.optPulse.Name = "optPulse"
        Me.optPulse.Size = New System.Drawing.Size(56, 17)
        Me.optPulse.TabIndex = 1
        Me.optPulse.Text = "Пульс"
        Me.optPulse.UseVisualStyleBackColor = True
        '
        'OptTone
        '
        Me.OptTone.AutoSize = True
        Me.OptTone.Checked = True
        Me.OptTone.Location = New System.Drawing.Point(6, 17)
        Me.OptTone.Name = "OptTone"
        Me.OptTone.Size = New System.Drawing.Size(44, 17)
        Me.OptTone.TabIndex = 0
        Me.OptTone.TabStop = True
        Me.OptTone.Text = "Тон"
        Me.OptTone.UseVisualStyleBackColor = True
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(200, 75)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(75, 20)
        Me.txtPort.TabIndex = 13
        '
        'lstCom
        '
        Me.lstCom.FormattingEnabled = True
        Me.lstCom.Location = New System.Drawing.Point(15, 58)
        Me.lstCom.Name = "lstCom"
        Me.lstCom.Size = New System.Drawing.Size(173, 134)
        Me.lstCom.TabIndex = 12
        '
        'cmdDelModem
        '
        Me.cmdDelModem.Location = New System.Drawing.Point(102, 17)
        Me.cmdDelModem.Name = "cmdDelModem"
        Me.cmdDelModem.Size = New System.Drawing.Size(86, 26)
        Me.cmdDelModem.TabIndex = 11
        Me.cmdDelModem.Text = "Удалить"
        Me.cmdDelModem.UseVisualStyleBackColor = True
        '
        'cmdAddModem
        '
        Me.cmdAddModem.Location = New System.Drawing.Point(9, 17)
        Me.cmdAddModem.Name = "cmdAddModem"
        Me.cmdAddModem.Size = New System.Drawing.Size(87, 27)
        Me.cmdAddModem.TabIndex = 10
        Me.cmdAddModem.Text = "Создать"
        Me.cmdAddModem.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdSaveM)
        Me.GroupBox3.Controls.Add(Me.txtINI)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtFull)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.txtShort)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.lstM)
        Me.GroupBox3.Controls.Add(Me.cmdDelM)
        Me.GroupBox3.Controls.Add(Me.cmdAddM)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 11)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(520, 231)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Модемы"
        '
        'cmdSaveM
        '
        Me.cmdSaveM.Location = New System.Drawing.Point(207, 184)
        Me.cmdSaveM.Name = "cmdSaveM"
        Me.cmdSaveM.Size = New System.Drawing.Size(298, 24)
        Me.cmdSaveM.TabIndex = 9
        Me.cmdSaveM.Text = "Сохранить"
        Me.cmdSaveM.UseVisualStyleBackColor = True
        '
        'txtINI
        '
        Me.txtINI.Location = New System.Drawing.Point(207, 158)
        Me.txtINI.Name = "txtINI"
        Me.txtINI.Size = New System.Drawing.Size(299, 20)
        Me.txtINI.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(209, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(124, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Строка инициализации"
        '
        'txtFull
        '
        Me.txtFull.Location = New System.Drawing.Point(207, 119)
        Me.txtFull.Name = "txtFull"
        Me.txtFull.Size = New System.Drawing.Size(300, 20)
        Me.txtFull.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(209, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Полное название"
        '
        'txtShort
        '
        Me.txtShort.Location = New System.Drawing.Point(208, 80)
        Me.txtShort.Name = "txtShort"
        Me.txtShort.Size = New System.Drawing.Size(299, 20)
        Me.txtShort.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(205, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Краткое название"
        '
        'lstM
        '
        Me.lstM.FormattingEnabled = True
        Me.lstM.Location = New System.Drawing.Point(15, 57)
        Me.lstM.Name = "lstM"
        Me.lstM.Size = New System.Drawing.Size(176, 160)
        Me.lstM.TabIndex = 2
        '
        'cmdDelM
        '
        Me.cmdDelM.Location = New System.Drawing.Point(108, 20)
        Me.cmdDelM.Name = "cmdDelM"
        Me.cmdDelM.Size = New System.Drawing.Size(83, 25)
        Me.cmdDelM.TabIndex = 1
        Me.cmdDelM.Text = "Удалить"
        Me.cmdDelM.UseVisualStyleBackColor = True
        '
        'cmdAddM
        '
        Me.cmdAddM.Location = New System.Drawing.Point(13, 20)
        Me.cmdAddM.Name = "cmdAddM"
        Me.cmdAddM.Size = New System.Drawing.Size(86, 25)
        Me.cmdAddM.TabIndex = 0
        Me.cmdAddM.Text = "Создать"
        Me.cmdAddM.UseVisualStyleBackColor = True
        '
        'optGSM
        '
        Me.optGSM.AutoSize = True
        Me.optGSM.Location = New System.Drawing.Point(150, 16)
        Me.optGSM.Name = "optGSM"
        Me.optGSM.Size = New System.Drawing.Size(49, 17)
        Me.optGSM.TabIndex = 2
        Me.optGSM.TabStop = True
        Me.optGSM.Text = "GSM"
        Me.optGSM.UseVisualStyleBackColor = True
        '
        'frmSetupModem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 456)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetupModem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Настройка модемов"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbM As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optPulse As System.Windows.Forms.RadioButton
    Friend WithEvents OptTone As System.Windows.Forms.RadioButton
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents lstCom As System.Windows.Forms.ListBox
    Friend WithEvents cmdDelModem As System.Windows.Forms.Button
    Friend WithEvents cmdAddModem As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSaveM As System.Windows.Forms.Button
    Friend WithEvents txtINI As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFull As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShort As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstM As System.Windows.Forms.ListBox
    Friend WithEvents cmdDelM As System.Windows.Forms.Button
    Friend WithEvents cmdAddM As System.Windows.Forms.Button
    Friend WithEvents cmdFreeModem As System.Windows.Forms.Button
    Friend WithEvents optGSM As System.Windows.Forms.RadioButton
End Class
