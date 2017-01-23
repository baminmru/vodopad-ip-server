<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUsersEdit
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
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lbl3 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.chkGroups = New System.Windows.Forms.CheckedListBox()
        Me.chkRequery = New System.Windows.Forms.CheckBox()
        Me.chkReport = New System.Windows.Forms.CheckBox()
        Me.chkTpl = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(10, 24)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(243, 20)
        Me.txtLogin.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Имя пользователя"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(270, 339)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(107, 30)
        Me.cmdSave.TabIndex = 15
        Me.cmdSave.Text = "Сохранить"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(396, 339)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 30)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Отмена"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lbl3
        '
        Me.lbl3.AutoSize = True
        Me.lbl3.Location = New System.Drawing.Point(14, 47)
        Me.lbl3.Name = "lbl3"
        Me.lbl3.Size = New System.Drawing.Size(45, 13)
        Me.lbl3.TabIndex = 20
        Me.lbl3.Text = "Пароль"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(10, 63)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(243, 20)
        Me.txtPassword.TabIndex = 21
        '
        'chkLocked
        '
        Me.chkLocked.AutoSize = True
        Me.chkLocked.Location = New System.Drawing.Point(12, 106)
        Me.chkLocked.Name = "chkLocked"
        Me.chkLocked.Size = New System.Drawing.Size(99, 17)
        Me.chkLocked.TabIndex = 23
        Me.chkLocked.Text = "Заблокирован"
        Me.chkLocked.UseVisualStyleBackColor = True
        '
        'chkGroups
        '
        Me.chkGroups.FormattingEnabled = True
        Me.chkGroups.Location = New System.Drawing.Point(12, 147)
        Me.chkGroups.Name = "chkGroups"
        Me.chkGroups.Size = New System.Drawing.Size(501, 184)
        Me.chkGroups.TabIndex = 24
        '
        'chkRequery
        '
        Me.chkRequery.AutoSize = True
        Me.chkRequery.Location = New System.Drawing.Point(321, 26)
        Me.chkRequery.Name = "chkRequery"
        Me.chkRequery.Size = New System.Drawing.Size(172, 17)
        Me.chkRequery.TabIndex = 25
        Me.chkRequery.Text = "Разрешен переопрос ссайта"
        Me.chkRequery.UseVisualStyleBackColor = True
        '
        'chkReport
        '
        Me.chkReport.AutoSize = True
        Me.chkReport.Location = New System.Drawing.Point(321, 66)
        Me.chkReport.Name = "chkReport"
        Me.chkReport.Size = New System.Drawing.Size(129, 17)
        Me.chkReport.TabIndex = 26
        Me.chkReport.Text = "Построение отчетов"
        Me.chkReport.UseVisualStyleBackColor = True
        '
        'chkTpl
        '
        Me.chkTpl.AutoSize = True
        Me.chkTpl.Location = New System.Drawing.Point(321, 106)
        Me.chkTpl.Name = "chkTpl"
        Me.chkTpl.Size = New System.Drawing.Size(142, 17)
        Me.chkTpl.TabIndex = 27
        Me.chkTpl.Text = "Добавление шаблонов"
        Me.chkTpl.UseVisualStyleBackColor = True
        '
        'frmUsersEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 378)
        Me.Controls.Add(Me.chkTpl)
        Me.Controls.Add(Me.chkReport)
        Me.Controls.Add(Me.chkRequery)
        Me.Controls.Add(Me.chkGroups)
        Me.Controls.Add(Me.chkLocked)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lbl3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdSave)
        Me.Name = "frmUsersEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WEB  пользователь"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbl3 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents chkLocked As System.Windows.Forms.CheckBox
    Friend WithEvents chkGroups As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkRequery As System.Windows.Forms.CheckBox
    Friend WithEvents chkReport As System.Windows.Forms.CheckBox
    Friend WithEvents chkTpl As System.Windows.Forms.CheckBox
End Class
