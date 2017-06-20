<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pGrid
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Grd = New PropertyGridEx.PropertyGridEx.PropertyGridEx()
        Me.SuspendLayout()
        '
        'Grd
        '
        Me.Grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Grd.HelpVisible = False
        Me.Grd.Location = New System.Drawing.Point(17, 17)
        Me.Grd.Name = "Grd"
        Me.Grd.Size = New System.Drawing.Size(727, 432)
        Me.Grd.TabIndex = 0
        '
        'pGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Grd)
        Me.Name = "pGrid"
        Me.Size = New System.Drawing.Size(760, 465)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grd As PropertyGridEx.PropertyGridEx.PropertyGridEx

End Class
