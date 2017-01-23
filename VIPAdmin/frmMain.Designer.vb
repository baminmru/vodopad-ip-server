<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.cmdGroups = New System.Windows.Forms.Button()
        Me.cmdWhoGiveTop = New System.Windows.Forms.Button()
        Me.cmdWhoGive = New System.Windows.Forms.Button()
        Me.cmdDevType = New System.Windows.Forms.Button()
        Me.cmdNewNode = New System.Windows.Forms.Button()
        Me.cmdSchema = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdGroups
        '
        Me.cmdGroups.Location = New System.Drawing.Point(445, 103)
        Me.cmdGroups.Name = "cmdGroups"
        Me.cmdGroups.Size = New System.Drawing.Size(197, 68)
        Me.cmdGroups.TabIndex = 0
        Me.cmdGroups.Text = "Группы"
        Me.cmdGroups.UseVisualStyleBackColor = True
        '
        'cmdWhoGiveTop
        '
        Me.cmdWhoGiveTop.Location = New System.Drawing.Point(226, 17)
        Me.cmdWhoGiveTop.Name = "cmdWhoGiveTop"
        Me.cmdWhoGiveTop.Size = New System.Drawing.Size(197, 68)
        Me.cmdWhoGiveTop.TabIndex = 1
        Me.cmdWhoGiveTop.Text = "Головные организции - поставщики"
        Me.cmdWhoGiveTop.UseVisualStyleBackColor = True
        '
        'cmdWhoGive
        '
        Me.cmdWhoGive.Location = New System.Drawing.Point(445, 17)
        Me.cmdWhoGive.Name = "cmdWhoGive"
        Me.cmdWhoGive.Size = New System.Drawing.Size(197, 68)
        Me.cmdWhoGive.TabIndex = 2
        Me.cmdWhoGive.Text = "Поставщики"
        Me.cmdWhoGive.UseVisualStyleBackColor = True
        '
        'cmdDevType
        '
        Me.cmdDevType.Location = New System.Drawing.Point(18, 103)
        Me.cmdDevType.Name = "cmdDevType"
        Me.cmdDevType.Size = New System.Drawing.Size(197, 68)
        Me.cmdDevType.TabIndex = 3
        Me.cmdDevType.Text = "Типы устройств"
        Me.cmdDevType.UseVisualStyleBackColor = True
        '
        'cmdNewNode
        '
        Me.cmdNewNode.Location = New System.Drawing.Point(18, 17)
        Me.cmdNewNode.Name = "cmdNewNode"
        Me.cmdNewNode.Size = New System.Drawing.Size(197, 68)
        Me.cmdNewNode.TabIndex = 4
        Me.cmdNewNode.Text = "Узлы"
        Me.cmdNewNode.UseVisualStyleBackColor = True
        '
        'cmdSchema
        '
        Me.cmdSchema.Location = New System.Drawing.Point(226, 103)
        Me.cmdSchema.Name = "cmdSchema"
        Me.cmdSchema.Size = New System.Drawing.Size(197, 68)
        Me.cmdSchema.TabIndex = 5
        Me.cmdSchema.Text = "Схемы подключения"
        Me.cmdSchema.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 201)
        Me.Controls.Add(Me.cmdSchema)
        Me.Controls.Add(Me.cmdNewNode)
        Me.Controls.Add(Me.cmdDevType)
        Me.Controls.Add(Me.cmdWhoGive)
        Me.Controls.Add(Me.cmdWhoGiveTop)
        Me.Controls.Add(Me.cmdGroups)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Водопад IP. Настройки"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdGroups As System.Windows.Forms.Button
    Friend WithEvents cmdWhoGiveTop As System.Windows.Forms.Button
    Friend WithEvents cmdWhoGive As System.Windows.Forms.Button
    Friend WithEvents cmdDevType As System.Windows.Forms.Button
    Friend WithEvents cmdNewNode As System.Windows.Forms.Button
    Friend WithEvents cmdSchema As System.Windows.Forms.Button

End Class
