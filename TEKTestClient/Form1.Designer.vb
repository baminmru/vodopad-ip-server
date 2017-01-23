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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMReq = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNSReq = New System.Windows.Forms.TextBox()
        Me.cmdMReq = New System.Windows.Forms.Button()
        Me.cmdNSReq = New System.Windows.Forms.Button()
        Me.txtOut = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Запрос данных"
        '
        'txtMReq
        '
        Me.txtMReq.Location = New System.Drawing.Point(22, 46)
        Me.txtMReq.Multiline = True
        Me.txtMReq.Name = "txtMReq"
        Me.txtMReq.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMReq.Size = New System.Drawing.Size(350, 180)
        Me.txtMReq.TabIndex = 1
        Me.txtMReq.Text = resources.GetString("txtMReq.Text")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(401, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Запрос нештаток"
        '
        'txtNSReq
        '
        Me.txtNSReq.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNSReq.Location = New System.Drawing.Point(390, 50)
        Me.txtNSReq.Multiline = True
        Me.txtNSReq.Name = "txtNSReq"
        Me.txtNSReq.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNSReq.Size = New System.Drawing.Size(331, 175)
        Me.txtNSReq.TabIndex = 3
        Me.txtNSReq.Text = resources.GetString("txtNSReq.Text")
        '
        'cmdMReq
        '
        Me.cmdMReq.Location = New System.Drawing.Point(132, 3)
        Me.cmdMReq.Name = "cmdMReq"
        Me.cmdMReq.Size = New System.Drawing.Size(161, 33)
        Me.cmdMReq.TabIndex = 4
        Me.cmdMReq.Text = "Выполнить"
        Me.cmdMReq.UseVisualStyleBackColor = True
        '
        'cmdNSReq
        '
        Me.cmdNSReq.Location = New System.Drawing.Point(547, 3)
        Me.cmdNSReq.Name = "cmdNSReq"
        Me.cmdNSReq.Size = New System.Drawing.Size(156, 33)
        Me.cmdNSReq.TabIndex = 5
        Me.cmdNSReq.Text = "Выполнить"
        Me.cmdNSReq.UseVisualStyleBackColor = True
        '
        'txtOut
        '
        Me.txtOut.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOut.Location = New System.Drawing.Point(23, 247)
        Me.txtOut.Multiline = True
        Me.txtOut.Name = "txtOut"
        Me.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOut.Size = New System.Drawing.Size(697, 209)
        Me.txtOut.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 478)
        Me.Controls.Add(Me.txtOut)
        Me.Controls.Add(Me.cmdNSReq)
        Me.Controls.Add(Me.cmdMReq)
        Me.Controls.Add(Me.txtNSReq)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMReq)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Тестовый клиент к  сервису интеграции с ТЭК"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMReq As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNSReq As System.Windows.Forms.TextBox
    Friend WithEvents cmdMReq As System.Windows.Forms.Button
    Friend WithEvents cmdNSReq As System.Windows.Forms.Button
    Friend WithEvents txtOut As System.Windows.Forms.TextBox

End Class
