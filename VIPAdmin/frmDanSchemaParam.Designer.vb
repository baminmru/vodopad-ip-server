<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDanSchemaParam
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
        Me.cmbParam = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.chkHideFromSchema = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'cmbParam
        '
        Me.cmbParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParam.FormattingEnabled = True
        Me.cmbParam.Location = New System.Drawing.Point(12, 32)
        Me.cmbParam.Name = "cmbParam"
        Me.cmbParam.Size = New System.Drawing.Size(498, 21)
        Me.cmbParam.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Параметр"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(376, 178)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(134, 30)
        Me.cmdCancel.TabIndex = 18
        Me.cmdCancel.Text = "Отмена"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(246, 178)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(107, 30)
        Me.cmdSave.TabIndex = 17
        Me.cmdSave.Text = "Сохранить"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Название"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(15, 84)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(486, 20)
        Me.txtName.TabIndex = 20
        '
        'chkHideFromSchema
        '
        Me.chkHideFromSchema.AutoSize = True
        Me.chkHideFromSchema.Location = New System.Drawing.Point(15, 124)
        Me.chkHideFromSchema.Name = "chkHideFromSchema"
        Me.chkHideFromSchema.Size = New System.Drawing.Size(152, 17)
        Me.chkHideFromSchema.TabIndex = 21
        Me.chkHideFromSchema.Text = "Не отображать на схеме"
        Me.chkHideFromSchema.UseVisualStyleBackColor = True
        '
        'frmDanSchemaParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 222)
        Me.Controls.Add(Me.chkHideFromSchema)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmbParam)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmDanSchemaParam"
        Me.Text = "Параметры"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbParam As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents chkHideFromSchema As System.Windows.Forms.CheckBox
End Class
