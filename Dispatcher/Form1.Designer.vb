<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form1
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents Text3 As System.Windows.Forms.TextBox
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.Text3 = New System.Windows.Forms.TextBox()
        Me.ButtonConnect = New System.Windows.Forms.Button()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.ButtonReadArch = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxArchType = New System.Windows.Forms.ComboBox()
        Me.TextBoxAcrhHour = New System.Windows.Forms.TextBox()
        Me.TextBoxAcrhDay = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Labell = New System.Windows.Forms.Label()
        Me.TextBoxArchMonth = New System.Windows.Forms.TextBox()
        Me.TextBoxArchYear = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.cmdOpenPort = New System.Windows.Forms.Button()
        Me.cmdSystem = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkSystem = New System.Windows.Forms.CheckBox()
        Me.chkTotal = New System.Windows.Forms.CheckBox()
        Me.chkMoment = New System.Windows.Forms.CheckBox()
        Me.Frame2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Text3)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(22, 268)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(644, 147)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Протокол"
        '
        'Text3
        '
        Me.Text3.AcceptsReturn = True
        Me.Text3.BackColor = System.Drawing.SystemColors.Window
        Me.Text3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text3.Location = New System.Drawing.Point(4, 19)
        Me.Text3.MaxLength = 0
        Me.Text3.Multiline = True
        Me.Text3.Name = "Text3"
        Me.Text3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Text3.Size = New System.Drawing.Size(632, 111)
        Me.Text3.TabIndex = 14
        '
        'ButtonConnect
        '
        Me.ButtonConnect.Location = New System.Drawing.Point(229, 12)
        Me.ButtonConnect.Name = "ButtonConnect"
        Me.ButtonConnect.Size = New System.Drawing.Size(209, 24)
        Me.ButtonConnect.TabIndex = 19
        Me.ButtonConnect.Text = "Соединение с ТВ"
        Me.ButtonConnect.UseVisualStyleBackColor = True
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(457, 12)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(195, 24)
        Me.ButtonClear.TabIndex = 24
        Me.ButtonClear.Text = "Очистка протокола"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'ButtonReadArch
        '
        Me.ButtonReadArch.Location = New System.Drawing.Point(439, 14)
        Me.ButtonReadArch.Name = "ButtonReadArch"
        Me.ButtonReadArch.Size = New System.Drawing.Size(195, 24)
        Me.ButtonReadArch.TabIndex = 30
        Me.ButtonReadArch.Text = "Получить архив"
        Me.ButtonReadArch.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.ComboBoxArchType)
        Me.GroupBox3.Controls.Add(Me.TextBoxAcrhHour)
        Me.GroupBox3.Controls.Add(Me.TextBoxAcrhDay)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Labell)
        Me.GroupBox3.Controls.Add(Me.TextBoxArchMonth)
        Me.GroupBox3.Controls.Add(Me.TextBoxArchYear)
        Me.GroupBox3.Controls.Add(Me.ButtonReadArch)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 42)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(640, 81)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Параметры Архива"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(293, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Тип архива"
        '
        'ComboBoxArchType
        '
        Me.ComboBoxArchType.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBoxArchType.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboBoxArchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxArchType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboBoxArchType.Location = New System.Drawing.Point(295, 41)
        Me.ComboBoxArchType.Name = "ComboBoxArchType"
        Me.ComboBoxArchType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboBoxArchType.Size = New System.Drawing.Size(125, 21)
        Me.ComboBoxArchType.TabIndex = 30
        '
        'TextBoxAcrhHour
        '
        Me.TextBoxAcrhHour.Location = New System.Drawing.Point(211, 46)
        Me.TextBoxAcrhHour.Name = "TextBoxAcrhHour"
        Me.TextBoxAcrhHour.Size = New System.Drawing.Size(62, 20)
        Me.TextBoxAcrhHour.TabIndex = 27
        Me.TextBoxAcrhHour.Text = "1"
        '
        'TextBoxAcrhDay
        '
        Me.TextBoxAcrhDay.Location = New System.Drawing.Point(211, 18)
        Me.TextBoxAcrhDay.Name = "TextBoxAcrhDay"
        Me.TextBoxAcrhDay.Size = New System.Drawing.Size(62, 20)
        Me.TextBoxAcrhDay.TabIndex = 26
        Me.TextBoxAcrhDay.Text = "1"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(163, 49)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(27, 13)
        Me.Label20.TabIndex = 25
        Me.Label20.Text = "Час"
        '
        'Labell
        '
        Me.Labell.AutoSize = True
        Me.Labell.Location = New System.Drawing.Point(163, 21)
        Me.Labell.Name = "Labell"
        Me.Labell.Size = New System.Drawing.Size(34, 13)
        Me.Labell.TabIndex = 24
        Me.Labell.Text = "День"
        '
        'TextBoxArchMonth
        '
        Me.TextBoxArchMonth.Location = New System.Drawing.Point(64, 46)
        Me.TextBoxArchMonth.Name = "TextBoxArchMonth"
        Me.TextBoxArchMonth.Size = New System.Drawing.Size(62, 20)
        Me.TextBoxArchMonth.TabIndex = 23
        Me.TextBoxArchMonth.Text = "1"
        '
        'TextBoxArchYear
        '
        Me.TextBoxArchYear.Location = New System.Drawing.Point(64, 18)
        Me.TextBoxArchYear.Name = "TextBoxArchYear"
        Me.TextBoxArchYear.Size = New System.Drawing.Size(62, 20)
        Me.TextBoxArchYear.TabIndex = 22
        Me.TextBoxArchYear.Text = "2007"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(16, 49)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(40, 13)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "Месяц"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(16, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(25, 13)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Год"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(229, 129)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(209, 24)
        Me.Button5.TabIndex = 36
        Me.Button5.Text = "Мгновенный архив"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(20, 129)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(195, 24)
        Me.Button9.TabIndex = 40
        Me.Button9.Text = "Итоговый архив"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'cmdOpenPort
        '
        Me.cmdOpenPort.Location = New System.Drawing.Point(22, 12)
        Me.cmdOpenPort.Name = "cmdOpenPort"
        Me.cmdOpenPort.Size = New System.Drawing.Size(193, 24)
        Me.cmdOpenPort.TabIndex = 44
        Me.cmdOpenPort.Text = "Открыть соединение"
        Me.cmdOpenPort.UseVisualStyleBackColor = True
        '
        'cmdSystem
        '
        Me.cmdSystem.Location = New System.Drawing.Point(457, 129)
        Me.cmdSystem.Name = "cmdSystem"
        Me.cmdSystem.Size = New System.Drawing.Size(197, 25)
        Me.cmdSystem.TabIndex = 45
        Me.cmdSystem.Text = "Системные параметры"
        Me.cmdSystem.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdStop)
        Me.GroupBox1.Controls.Add(Me.cmdStart)
        Me.GroupBox1.Controls.Add(Me.txtInterval)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.chkSystem)
        Me.GroupBox1.Controls.Add(Me.chkTotal)
        Me.GroupBox1.Controls.Add(Me.chkMoment)
        Me.GroupBox1.Location = New System.Drawing.Point(25, 163)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(626, 99)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Автоповтор"
        '
        'cmdStop
        '
        Me.cmdStop.Location = New System.Drawing.Point(432, 55)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(188, 21)
        Me.cmdStop.TabIndex = 6
        Me.cmdStop.Text = "Завершить опрос"
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'cmdStart
        '
        Me.cmdStart.Location = New System.Drawing.Point(432, 20)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(188, 20)
        Me.cmdStart.TabIndex = 5
        Me.cmdStart.Text = "Начать опрос"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(204, 45)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(209, 20)
        Me.txtInterval.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(215, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Интервал (сек)"
        '
        'chkSystem
        '
        Me.chkSystem.AutoSize = True
        Me.chkSystem.Location = New System.Drawing.Point(12, 68)
        Me.chkSystem.Name = "chkSystem"
        Me.chkSystem.Size = New System.Drawing.Size(84, 17)
        Me.chkSystem.TabIndex = 2
        Me.chkSystem.Text = "Системный"
        Me.chkSystem.UseVisualStyleBackColor = True
        '
        'chkTotal
        '
        Me.chkTotal.AutoSize = True
        Me.chkTotal.Location = New System.Drawing.Point(12, 45)
        Me.chkTotal.Name = "chkTotal"
        Me.chkTotal.Size = New System.Drawing.Size(76, 17)
        Me.chkTotal.TabIndex = 1
        Me.chkTotal.Text = "Итоговый"
        Me.chkTotal.UseVisualStyleBackColor = True
        '
        'chkMoment
        '
        Me.chkMoment.AutoSize = True
        Me.chkMoment.Location = New System.Drawing.Point(11, 22)
        Me.chkMoment.Name = "chkMoment"
        Me.chkMoment.Size = New System.Drawing.Size(90, 17)
        Me.chkMoment.TabIndex = 0
        Me.chkMoment.Text = "Мгновенный"
        Me.chkMoment.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(672, 427)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSystem)
        Me.Controls.Add(Me.cmdOpenPort)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.ButtonClear)
        Me.Controls.Add(Me.ButtonConnect)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Тест работы с устройством"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonConnect As System.Windows.Forms.Button
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
    Friend WithEvents ButtonReadArch As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxAcrhHour As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAcrhDay As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Labell As System.Windows.Forms.Label
    Friend WithEvents TextBoxArchMonth As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxArchYear As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents ComboBoxArchType As System.Windows.Forms.ComboBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents cmdOpenPort As System.Windows.Forms.Button
    Friend WithEvents cmdSystem As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkSystem As System.Windows.Forms.CheckBox
    Friend WithEvents chkTotal As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoment As System.Windows.Forms.CheckBox
#End Region
End Class