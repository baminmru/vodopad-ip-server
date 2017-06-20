<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStatus
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
    'It can be modified Imports the Windows Form Designer.  
    'Do not modify it Imports the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdOpenDisp = New System.Windows.Forms.Button()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtAddr = New System.Windows.Forms.TextBox()
        Me.txtPhones = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cmdSetup = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(431, 242)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(143, 24)
        Me.cmdClose.TabIndex = 20
        Me.cmdClose.Text = "Закрыть"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdOpenDisp
        '
        Me.cmdOpenDisp.Location = New System.Drawing.Point(245, 241)
        Me.cmdOpenDisp.Name = "cmdOpenDisp"
        Me.cmdOpenDisp.Size = New System.Drawing.Size(140, 25)
        Me.cmdOpenDisp.TabIndex = 19
        Me.cmdOpenDisp.Text = "Опрос"
        Me.cmdOpenDisp.UseVisualStyleBackColor = True
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(277, 15)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(88, 13)
        Me.label4.TabIndex = 18
        Me.label4.Text = "Текущий статус"
        '
        'txtInfo
        '
        Me.txtInfo.Location = New System.Drawing.Point(277, 43)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtInfo.Size = New System.Drawing.Size(298, 177)
        Me.txtInfo.TabIndex = 17
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(14, 127)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(38, 13)
        Me.label2.TabIndex = 16
        Me.label2.Text = "Адрес"
        '
        'txtAddr
        '
        Me.txtAddr.Location = New System.Drawing.Point(15, 156)
        Me.txtAddr.Multiline = True
        Me.txtAddr.Name = "txtAddr"
        Me.txtAddr.ReadOnly = True
        Me.txtAddr.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAddr.Size = New System.Drawing.Size(242, 65)
        Me.txtAddr.TabIndex = 15
        '
        'txtPhones
        '
        Me.txtPhones.Location = New System.Drawing.Point(13, 95)
        Me.txtPhones.Name = "txtPhones"
        Me.txtPhones.ReadOnly = True
        Me.txtPhones.Size = New System.Drawing.Size(243, 20)
        Me.txtPhones.TabIndex = 14
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(10, 73)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(60, 13)
        Me.label3.TabIndex = 13
        Me.label3.Text = "Телефоны"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(12, 41)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(245, 20)
        Me.txtName.TabIndex = 12
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 14)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(49, 13)
        Me.label1.TabIndex = 11
        Me.label1.Text = "Станция"
        '
        'cmdSetup
        '
        Me.cmdSetup.Location = New System.Drawing.Point(17, 241)
        Me.cmdSetup.Name = "cmdSetup"
        Me.cmdSetup.Size = New System.Drawing.Size(187, 24)
        Me.cmdSetup.TabIndex = 21
        Me.cmdSetup.Text = "Настройки узла"
        Me.cmdSetup.UseVisualStyleBackColor = True
        '
        'frmStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 284)
        Me.Controls.Add(Me.cmdSetup)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdOpenDisp)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.txtInfo)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.txtAddr)
        Me.Controls.Add(Me.txtPhones)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Состояние"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents cmdClose As System.Windows.Forms.Button
    Private WithEvents cmdOpenDisp As System.Windows.Forms.Button
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents txtInfo As System.Windows.Forms.TextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents txtAddr As System.Windows.Forms.TextBox
    Private WithEvents txtPhones As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSetup As System.Windows.Forms.Button
End Class
