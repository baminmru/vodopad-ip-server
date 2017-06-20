<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDevTypeEdit
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
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtDLLNAME = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtVerifyCOLS = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbClass = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAddMS = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(8, 25)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(498, 20)
        Me.txtName.TabIndex = 24
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Название устройства"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(265, 283)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(107, 30)
        Me.cmdSave.TabIndex = 21
        Me.cmdSave.Text = "Сохранить"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(9, 62)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(498, 20)
        Me.txtDesc.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Описание"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(396, 283)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(111, 29)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "Отмена"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtDLLNAME
        '
        Me.txtDLLNAME.Location = New System.Drawing.Point(9, 99)
        Me.txtDLLNAME.Name = "txtDLLNAME"
        Me.txtDLLNAME.Size = New System.Drawing.Size(498, 20)
        Me.txtDLLNAME.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(208, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Название DLL с драйвером устройства"
        '
        'txtVerifyCOLS
        '
        Me.txtVerifyCOLS.Location = New System.Drawing.Point(8, 146)
        Me.txtVerifyCOLS.Name = "txtVerifyCOLS"
        Me.txtVerifyCOLS.Size = New System.Drawing.Size(498, 20)
        Me.txtVerifyCOLS.TabIndex = 32
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(161, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Список проверяемых колонок"
        '
        'cmbClass
        '
        Me.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClass.FormattingEnabled = True
        Me.cmbClass.Location = New System.Drawing.Point(9, 244)
        Me.cmbClass.Name = "cmbClass"
        Me.cmbClass.Size = New System.Drawing.Size(497, 21)
        Me.cmbClass.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 228)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Тип устройства"
        '
        'txtAddMS
        '
        Me.txtAddMS.Location = New System.Drawing.Point(8, 196)
        Me.txtAddMS.Name = "txtAddMS"
        Me.txtAddMS.Size = New System.Drawing.Size(498, 20)
        Me.txtAddMS.TabIndex = 36
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(198, 13)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Коррекция даты архива (милисекунд)"
        '
        'frmDevTypeEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 324)
        Me.Controls.Add(Me.txtAddMS)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbClass)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtVerifyCOLS)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtDLLNAME)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdSave)
        Me.Name = "frmDevTypeEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Типы вычислителей"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtDLLNAME As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtVerifyCOLS As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbClass As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAddMS As TextBox
    Friend WithEvents Label6 As Label
End Class
