<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdPath = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.cdlg = New System.Windows.Forms.OpenFileDialog()
        Me.txtOut = New System.Windows.Forms.TextBox()
        Me.txtSQL = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(282, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(231, 24)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmdPath
        '
        Me.cmdPath.Location = New System.Drawing.Point(216, 19)
        Me.cmdPath.Name = "cmdPath"
        Me.cmdPath.Size = New System.Drawing.Size(43, 24)
        Me.cmdPath.TabIndex = 1
        Me.cmdPath.Text = "..."
        Me.cmdPath.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(23, 22)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(179, 20)
        Me.txtPath.TabIndex = 2
        '
        'cdlg
        '
        Me.cdlg.FileName = "OpenFileDialog1"
        '
        'txtOut
        '
        Me.txtOut.Location = New System.Drawing.Point(27, 81)
        Me.txtOut.Multiline = True
        Me.txtOut.Name = "txtOut"
        Me.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOut.Size = New System.Drawing.Size(614, 130)
        Me.txtOut.TabIndex = 3
        '
        'txtSQL
        '
        Me.txtSQL.Location = New System.Drawing.Point(31, 217)
        Me.txtSQL.Multiline = True
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSQL.Size = New System.Drawing.Size(609, 110)
        Me.txtSQL.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 346)
        Me.Controls.Add(Me.txtSQL)
        Me.Controls.Add(Me.txtOut)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.cmdPath)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmdPath As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents cdlg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtOut As System.Windows.Forms.TextBox
    Friend WithEvents txtSQL As System.Windows.Forms.TextBox

End Class
