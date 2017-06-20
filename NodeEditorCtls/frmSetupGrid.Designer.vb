<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetupGrid
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.grpCol = New Infragistics.Win.Misc.UltraGroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmbFormat = New System.Windows.Forms.ComboBox()
        Me.txtWidth = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtHeader = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmdUp = New Infragistics.Win.Misc.UltraButton()
        Me.cmdDown = New Infragistics.Win.Misc.UltraButton()
        Me.lstParams = New System.Windows.Forms.CheckedListBox()
        Me.grpMAsk = New Infragistics.Win.Misc.UltraGroupBox()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grpCol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCol.SuspendLayout()
        CType(Me.txtWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpMAsk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMAsk.SuspendLayout()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(349, 454)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'grpCol
        '
        Me.grpCol.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCol.Controls.Add(Me.Button1)
        Me.grpCol.Controls.Add(Me.cmbFormat)
        Me.grpCol.Controls.Add(Me.txtWidth)
        Me.grpCol.Controls.Add(Me.UltraLabel3)
        Me.grpCol.Controls.Add(Me.UltraLabel2)
        Me.grpCol.Controls.Add(Me.txtHeader)
        Me.grpCol.Controls.Add(Me.UltraLabel1)
        Me.grpCol.Controls.Add(Me.cmdUp)
        Me.grpCol.Controls.Add(Me.cmdDown)
        Me.grpCol.Controls.Add(Me.lstParams)
        Me.grpCol.Location = New System.Drawing.Point(12, 97)
        Me.grpCol.Name = "grpCol"
        Me.grpCol.Size = New System.Drawing.Size(486, 351)
        Me.grpCol.TabIndex = 10
        Me.grpCol.Text = "Настройка колонок"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(15, 316)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(135, 20)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "Добавить параметры"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmbFormat
        '
        Me.cmbFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFormat.FormattingEnabled = True
        Me.cmbFormat.Items.AddRange(New Object() {"?", "D", "T", "S", "N", "F", "I", "B"})
        Me.cmbFormat.Location = New System.Drawing.Point(229, 85)
        Me.cmbFormat.Name = "cmbFormat"
        Me.cmbFormat.Size = New System.Drawing.Size(248, 21)
        Me.cmbFormat.TabIndex = 19
        '
        'txtWidth
        '
        Me.txtWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWidth.Location = New System.Drawing.Point(226, 140)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(251, 21)
        Me.txtWidth.TabIndex = 18
        '
        'UltraLabel3
        '
        Me.UltraLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraLabel3.Location = New System.Drawing.Point(229, 123)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(249, 22)
        Me.UltraLabel3.TabIndex = 17
        Me.UltraLabel3.Text = "Ширина колонки"
        '
        'UltraLabel2
        '
        Me.UltraLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraLabel2.Location = New System.Drawing.Point(228, 64)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(251, 15)
        Me.UltraLabel2.TabIndex = 15
        Me.UltraLabel2.Text = "Тип парметра"
        '
        'txtHeader
        '
        Me.txtHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHeader.Location = New System.Drawing.Point(229, 37)
        Me.txtHeader.Name = "txtHeader"
        Me.txtHeader.Size = New System.Drawing.Size(251, 21)
        Me.txtHeader.TabIndex = 14
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraLabel1.Location = New System.Drawing.Point(228, 19)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(140, 22)
        Me.UltraLabel1.TabIndex = 13
        Me.UltraLabel1.Text = "Заголовок"
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(84, 23)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(67, 23)
        Me.cmdUp.TabIndex = 12
        Me.cmdUp.Text = "выше"
        '
        'cmdDown
        '
        Me.cmdDown.Location = New System.Drawing.Point(14, 23)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(63, 23)
        Me.cmdDown.TabIndex = 11
        Me.cmdDown.Text = "ниже"
        '
        'lstParams
        '
        Me.lstParams.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstParams.FormattingEnabled = True
        Me.lstParams.Location = New System.Drawing.Point(14, 52)
        Me.lstParams.Name = "lstParams"
        Me.lstParams.Size = New System.Drawing.Size(193, 244)
        Me.lstParams.TabIndex = 10
        '
        'grpMAsk
        '
        Me.grpMAsk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpMAsk.Controls.Add(Me.UltraLabel4)
        Me.grpMAsk.Controls.Add(Me.txtName)
        Me.grpMAsk.Location = New System.Drawing.Point(12, 13)
        Me.grpMAsk.Name = "grpMAsk"
        Me.grpMAsk.Size = New System.Drawing.Size(486, 78)
        Me.grpMAsk.TabIndex = 11
        Me.grpMAsk.Text = "Маска"
        '
        'UltraLabel4
        '
        Me.UltraLabel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraLabel4.Location = New System.Drawing.Point(11, 18)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(448, 18)
        Me.UltraLabel4.TabIndex = 1
        Me.UltraLabel4.Text = "Название Маски"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(12, 46)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(465, 21)
        Me.txtName.TabIndex = 0
        '
        'frmSetupGrid
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(507, 495)
        Me.Controls.Add(Me.grpMAsk)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.grpCol)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 380)
        Me.Name = "frmSetupGrid"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Настройка маски"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grpCol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCol.ResumeLayout(False)
        Me.grpCol.PerformLayout()
        CType(Me.txtWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpMAsk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMAsk.ResumeLayout(False)
        Me.grpMAsk.PerformLayout()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents grpCol As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents txtWidth As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtHeader As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmdUp As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmdDown As Infragistics.Win.Misc.UltraButton
    Friend WithEvents lstParams As System.Windows.Forms.CheckedListBox
    Friend WithEvents grpMAsk As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
