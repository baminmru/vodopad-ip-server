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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НастройкаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuConnect = New System.Windows.Forms.ToolStripMenuItem()
        Me.ВыходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gv = New System.Windows.Forms.DataGridView()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ФайлToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(832, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ФайлToolStripMenuItem
        '
        Me.ФайлToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.НастройкаToolStripMenuItem, Me.mnuConnect, Me.ВыходToolStripMenuItem})
        Me.ФайлToolStripMenuItem.Name = "ФайлToolStripMenuItem"
        Me.ФайлToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ФайлToolStripMenuItem.Text = "Файл"
        '
        'НастройкаToolStripMenuItem
        '
        Me.НастройкаToolStripMenuItem.Name = "НастройкаToolStripMenuItem"
        Me.НастройкаToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.НастройкаToolStripMenuItem.Text = "Настройка"
        '
        'mnuConnect
        '
        Me.mnuConnect.Name = "mnuConnect"
        Me.mnuConnect.Size = New System.Drawing.Size(144, 22)
        Me.mnuConnect.Text = "Подключить"
        '
        'ВыходToolStripMenuItem
        '
        Me.ВыходToolStripMenuItem.Name = "ВыходToolStripMenuItem"
        Me.ВыходToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.ВыходToolStripMenuItem.Text = "Выход"
        '
        'gv
        '
        Me.gv.AllowUserToAddRows = False
        Me.gv.AllowUserToDeleteRows = False
        Me.gv.AllowUserToOrderColumns = True
        Me.gv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gv.Location = New System.Drawing.Point(14, 27)
        Me.gv.Name = "gv"
        Me.gv.Size = New System.Drawing.Size(806, 341)
        Me.gv.TabIndex = 1
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.Location = New System.Drawing.Point(486, 374)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(156, 27)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Сохранить изменения"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRefresh.Location = New System.Drawing.Point(648, 374)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(172, 26)
        Me.cmdRefresh.TabIndex = 3
        Me.cmdRefresh.Text = "Обновить"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.Location = New System.Drawing.Point(13, 380)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(464, 19)
        Me.lblStatus.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 413)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.gv)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Danfoss 310 client"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ФайлToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents НастройкаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuConnect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ВыходToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gv As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label

End Class
