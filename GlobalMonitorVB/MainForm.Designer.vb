<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
    'It can be modified Imports the Windows Form Designer.  
    'Do not modify it Imports the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.txtInfo = New System.Windows.Forms.TextBox()
        Me.txtCenterPoint = New System.Windows.Forms.TextBox()
        Me.trackBar1 = New System.Windows.Forms.TrackBar()
        Me.btnZoomIn = New System.Windows.Forms.Button()
        Me.btnZoomOut = New System.Windows.Forms.Button()
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СтатусToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОпросToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGraph = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNodesetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mapExplorer = New OsmExplorer.Components.MapExplorer()
        Me.РасчетКоординатToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtInfo
        '
        Me.txtInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInfo.Location = New System.Drawing.Point(467, 27)
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.Size = New System.Drawing.Size(327, 20)
        Me.txtInfo.TabIndex = 25
        '
        'txtCenterPoint
        '
        Me.txtCenterPoint.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtCenterPoint.Enabled = False
        Me.txtCenterPoint.Location = New System.Drawing.Point(274, 27)
        Me.txtCenterPoint.Name = "txtCenterPoint"
        Me.txtCenterPoint.ReadOnly = True
        Me.txtCenterPoint.Size = New System.Drawing.Size(187, 20)
        Me.txtCenterPoint.TabIndex = 24
        '
        'trackBar1
        '
        Me.trackBar1.LargeChange = 1
        Me.trackBar1.Location = New System.Drawing.Point(40, 27)
        Me.trackBar1.Maximum = 0
        Me.trackBar1.Minimum = -20
        Me.trackBar1.Name = "trackBar1"
        Me.trackBar1.Size = New System.Drawing.Size(180, 42)
        Me.trackBar1.TabIndex = 19
        Me.trackBar1.Value = -5
        '
        'btnZoomIn
        '
        Me.btnZoomIn.Location = New System.Drawing.Point(226, 24)
        Me.btnZoomIn.Name = "btnZoomIn"
        Me.btnZoomIn.Size = New System.Drawing.Size(33, 23)
        Me.btnZoomIn.TabIndex = 20
        Me.btnZoomIn.Text = "-"
        Me.btnZoomIn.UseVisualStyleBackColor = True
        '
        'btnZoomOut
        '
        Me.btnZoomOut.Location = New System.Drawing.Point(1, 27)
        Me.btnZoomOut.Name = "btnZoomOut"
        Me.btnZoomOut.Size = New System.Drawing.Size(33, 23)
        Me.btnZoomOut.TabIndex = 21
        Me.btnZoomOut.Text = "+"
        Me.btnZoomOut.UseVisualStyleBackColor = True
        '
        'timer1
        '
        Me.timer1.Interval = 10000
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ФайлToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(806, 24)
        Me.MenuStrip1.TabIndex = 26
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ФайлToolStripMenuItem
        '
        Me.ФайлToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.СтатусToolStripMenuItem, Me.ОпросToolStripMenuItem, Me.mnuGraph, Me.mnuNodesetup, Me.РасчетКоординатToolStripMenuItem})
        Me.ФайлToolStripMenuItem.Name = "ФайлToolStripMenuItem"
        Me.ФайлToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.ФайлToolStripMenuItem.Text = "Файл"
        '
        'СтатусToolStripMenuItem
        '
        Me.СтатусToolStripMenuItem.Name = "СтатусToolStripMenuItem"
        Me.СтатусToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.СтатусToolStripMenuItem.Text = "Статус"
        '
        'ОпросToolStripMenuItem
        '
        Me.ОпросToolStripMenuItem.Name = "ОпросToolStripMenuItem"
        Me.ОпросToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ОпросToolStripMenuItem.Text = "Опрос"
        '
        'mnuGraph
        '
        Me.mnuGraph.Name = "mnuGraph"
        Me.mnuGraph.Size = New System.Drawing.Size(167, 22)
        Me.mnuGraph.Text = "Графики"
        '
        'mnuNodesetup
        '
        Me.mnuNodesetup.Name = "mnuNodesetup"
        Me.mnuNodesetup.Size = New System.Drawing.Size(167, 22)
        Me.mnuNodesetup.Text = "Настройки узла"
        '
        'mapExplorer
        '
        Me.mapExplorer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mapExplorer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.mapExplorer.HighlightedCoordinate = Nothing
        Me.mapExplorer.Location = New System.Drawing.Point(12, 75)
        Me.mapExplorer.Manager = Nothing
        Me.mapExplorer.MapCenterPoint = CType(resources.GetObject("mapExplorer.MapCenterPoint"), OsmExplorer.Spatial.LatLon)
        Me.mapExplorer.Name = "mapExplorer"
        Me.mapExplorer.Size = New System.Drawing.Size(782, 333)
        Me.mapExplorer.TabIndex = 0
        Me.mapExplorer.ZoomLevel = 25
        '
        'РасчетКоординатToolStripMenuItem
        '
        Me.РасчетКоординатToolStripMenuItem.Name = "РасчетКоординатToolStripMenuItem"
        Me.РасчетКоординатToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.РасчетКоординатToolStripMenuItem.Text = "Расчет координат"
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(806, 420)
        Me.Controls.Add(Me.mapExplorer)
        Me.Controls.Add(Me.txtInfo)
        Me.Controls.Add(Me.txtCenterPoint)
        Me.Controls.Add(Me.btnZoomIn)
        Me.Controls.Add(Me.btnZoomOut)
        Me.Controls.Add(Me.trackBar1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Водопад-IP.  Глобальный монитор"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.trackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mapExplorer As OsmExplorer.Components.MapExplorer
    Private WithEvents txtInfo As System.Windows.Forms.TextBox
    Private WithEvents txtCenterPoint As System.Windows.Forms.TextBox
    Private WithEvents trackBar1 As System.Windows.Forms.TrackBar
    Private WithEvents btnZoomIn As System.Windows.Forms.Button
    Private WithEvents btnZoomOut As System.Windows.Forms.Button
    Private WithEvents timer1 As System.Windows.Forms.Timer
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ФайлToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents СтатусToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОпросToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGraph As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNodesetup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents РасчетКоординатToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
 

End Class
