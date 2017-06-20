<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectParam
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
        Me.chkParams = New System.Windows.Forms.CheckedListBox()
        Me.cmdOK = New Infragistics.Win.Misc.UltraButton()
        Me.cmdCancel = New Infragistics.Win.Misc.UltraButton()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'chkParams
        '
        Me.chkParams.FormattingEnabled = True
        Me.chkParams.Location = New System.Drawing.Point(17, 60)
        Me.chkParams.Name = "chkParams"
        Me.chkParams.Size = New System.Drawing.Size(259, 184)
        Me.chkParams.TabIndex = 0
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(17, 262)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(122, 28)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "ОК"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(152, 262)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(124, 28)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Отмена"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(17, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 37)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Выделить все"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(163, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 37)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Отменить все"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmSelectParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 301)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.chkParams)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmSelectParam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Выбор параметров"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkParams As System.Windows.Forms.CheckedListBox
    Friend WithEvents cmdOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmdCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
End Class
