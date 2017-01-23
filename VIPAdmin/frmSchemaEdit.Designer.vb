<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchemaEdit
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
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.picSchema = New System.Windows.Forms.PictureBox()
        Me.cmdButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cmdDel = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.GV = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cmdDel2 = New System.Windows.Forms.Button()
        Me.cmdAdd2 = New System.Windows.Forms.Button()
        Me.cmdOpen2 = New System.Windows.Forms.Button()
        Me.GV2 = New System.Windows.Forms.DataGridView()
        Me.openFile = New System.Windows.Forms.OpenFileDialog()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.picSchema, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.GV2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(450, 410)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(107, 30)
        Me.cmdSave.TabIndex = 21
        Me.cmdSave.Text = "Сохранить"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(563, 410)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(101, 30)
        Me.Button1.TabIndex = 25
        Me.Button1.Text = "Отмена"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(652, 392)
        Me.TabControl1.TabIndex = 26
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtPath)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.picSchema)
        Me.TabPage1.Controls.Add(Me.cmdButton)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtName)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(644, 366)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Информация"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(547, 22)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(74, 30)
        Me.Button2.TabIndex = 31
        Me.Button2.Text = "Заменить"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'picSchema
        '
        Me.picSchema.Location = New System.Drawing.Point(130, 86)
        Me.picSchema.Name = "picSchema"
        Me.picSchema.Size = New System.Drawing.Size(364, 274)
        Me.picSchema.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSchema.TabIndex = 30
        Me.picSchema.TabStop = False
        '
        'cmdButton
        '
        Me.cmdButton.Location = New System.Drawing.Point(507, 22)
        Me.cmdButton.Name = "cmdButton"
        Me.cmdButton.Size = New System.Drawing.Size(34, 29)
        Me.cmdButton.TabIndex = 29
        Me.cmdButton.Text = "..."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(310, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Файл с изображением"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(6, 32)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(282, 20)
        Me.txtName.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Название"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmdDel)
        Me.TabPage2.Controls.Add(Me.cmdAdd)
        Me.TabPage2.Controls.Add(Me.cmdOpen)
        Me.TabPage2.Controls.Add(Me.GV)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(644, 366)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Вводы и трубы"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdDel
        '
        Me.cmdDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDel.Location = New System.Drawing.Point(533, 330)
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.Size = New System.Drawing.Size(105, 30)
        Me.cmdDel.TabIndex = 29
        Me.cmdDel.Text = "Удалить"
        Me.cmdDel.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Location = New System.Drawing.Point(410, 330)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(104, 30)
        Me.cmdAdd.TabIndex = 28
        Me.cmdAdd.Text = "Добавить"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdOpen
        '
        Me.cmdOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpen.Location = New System.Drawing.Point(287, 330)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(107, 30)
        Me.cmdOpen.TabIndex = 27
        Me.cmdOpen.Text = "Открыть"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'GV
        '
        Me.GV.AllowUserToAddRows = False
        Me.GV.AllowUserToDeleteRows = False
        Me.GV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GV.Location = New System.Drawing.Point(6, 6)
        Me.GV.MultiSelect = False
        Me.GV.Name = "GV"
        Me.GV.ReadOnly = True
        Me.GV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GV.Size = New System.Drawing.Size(632, 318)
        Me.GV.TabIndex = 26
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cmdDel2)
        Me.TabPage3.Controls.Add(Me.cmdAdd2)
        Me.TabPage3.Controls.Add(Me.cmdOpen2)
        Me.TabPage3.Controls.Add(Me.GV2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(644, 366)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Получаемые параметры"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cmdDel2
        '
        Me.cmdDel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDel2.Location = New System.Drawing.Point(528, 324)
        Me.cmdDel2.Name = "cmdDel2"
        Me.cmdDel2.Size = New System.Drawing.Size(105, 30)
        Me.cmdDel2.TabIndex = 33
        Me.cmdDel2.Text = "Удалить"
        Me.cmdDel2.UseVisualStyleBackColor = True
        '
        'cmdAdd2
        '
        Me.cmdAdd2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd2.Location = New System.Drawing.Point(405, 324)
        Me.cmdAdd2.Name = "cmdAdd2"
        Me.cmdAdd2.Size = New System.Drawing.Size(104, 30)
        Me.cmdAdd2.TabIndex = 32
        Me.cmdAdd2.Text = "Добавить"
        Me.cmdAdd2.UseVisualStyleBackColor = True
        '
        'cmdOpen2
        '
        Me.cmdOpen2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOpen2.Location = New System.Drawing.Point(282, 324)
        Me.cmdOpen2.Name = "cmdOpen2"
        Me.cmdOpen2.Size = New System.Drawing.Size(107, 30)
        Me.cmdOpen2.TabIndex = 31
        Me.cmdOpen2.Text = "Открыть"
        Me.cmdOpen2.UseVisualStyleBackColor = True
        '
        'GV2
        '
        Me.GV2.AllowUserToAddRows = False
        Me.GV2.AllowUserToDeleteRows = False
        Me.GV2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GV2.Location = New System.Drawing.Point(6, 13)
        Me.GV2.MultiSelect = False
        Me.GV2.Name = "GV2"
        Me.GV2.ReadOnly = True
        Me.GV2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GV2.Size = New System.Drawing.Size(627, 305)
        Me.GV2.TabIndex = 30
        '
        'openFile
        '
        Me.openFile.DefaultExt = "bmp"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(316, 34)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(177, 20)
        Me.txtPath.TabIndex = 32
        '
        'frmSchemaEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 452)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSchemaEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Схемы подключения"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.picSchema, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.GV2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents picSchema As System.Windows.Forms.PictureBox
    Friend WithEvents cmdButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents openFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdDel As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents GV As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents cmdDel2 As System.Windows.Forms.Button
    Friend WithEvents cmdAdd2 As System.Windows.Forms.Button
    Friend WithEvents cmdOpen2 As System.Windows.Forms.Button
    Friend WithEvents GV2 As System.Windows.Forms.DataGridView
    Friend WithEvents txtPath As TextBox
End Class
