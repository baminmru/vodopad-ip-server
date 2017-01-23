<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchemaParam
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
        Me.cmbPipeType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbParam = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbEdizm = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmbPipeType
        '
        Me.cmbPipeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPipeType.FormattingEnabled = True
        Me.cmbPipeType.Location = New System.Drawing.Point(12, 87)
        Me.cmbPipeType.Name = "cmbPipeType"
        Me.cmbPipeType.Size = New System.Drawing.Size(498, 21)
        Me.cmbPipeType.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Труба"
        '
        'cmbParam
        '
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
        'cmbEdizm
        '
        Me.cmbEdizm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEdizm.FormattingEnabled = True
        Me.cmbEdizm.Location = New System.Drawing.Point(12, 142)
        Me.cmbEdizm.Name = "cmbEdizm"
        Me.cmbEdizm.Size = New System.Drawing.Size(498, 21)
        Me.cmbEdizm.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Единица измерения"
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
        'frmSchemaParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 222)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmbEdizm)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbParam)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbPipeType)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSchemaParam"
        Me.Text = "Параметры"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbPipeType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbParam As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbEdizm As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
End Class
