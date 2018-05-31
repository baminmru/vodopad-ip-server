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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuManualQuery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNC = New System.Windows.Forms.ToolStripMenuItem()
        Me.nmuGraph = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSchema = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSezon0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSezon1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSetupModems = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuStopAlert = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartAlert = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHideWhite = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRefreshLost = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVerifyD24H = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.utm = New Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(Me.components)
        Me.ImgNodeType = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.UltraToolTipManager1 = New Infragistics.Win.UltraWinToolTip.UltraToolTipManager(Me.components)
        Me.udm = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components)
        Me._frmMainUnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._frmMainUnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._frmMainUnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._frmMainUnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._frmMainAutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl()
        Me.Узлы = New Infragistics.Win.Misc.UltraPanel()
        Me.ListView1 = New Infragistics.Win.UltraWinListView.UltraListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdTest = New System.Windows.Forms.Button()
        Me.cmdBounds = New System.Windows.Forms.Button()
        Me.cmdRefreshList = New System.Windows.Forms.Button()
        Me.chkShoHidden = New System.Windows.Forms.CheckBox()
        Me.tree = New Infragistics.Win.UltraWinTree.UltraTree()
        Me.ButtonConfig = New System.Windows.Forms.Button()
        Me.MenuStrip.SuspendLayout()
        CType(Me.utm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Узлы.ClientArea.SuspendLayout()
        Me.Узлы.SuspendLayout()
        CType(Me.ListView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.HelpMenu})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(853, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'FileMenu
        '
        Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator3, Me.mnuManualQuery, Me.mnuNC, Me.nmuGraph, Me.mnuSchema, Me.ToolStripSeparator6, Me.mnuSezon0, Me.mnuSezon1, Me.ToolStripSeparator5, Me.mnuSetupModems, Me.ToolStripSeparator1, Me.mnuStopAlert, Me.mnuStartAlert, Me.ToolStripSeparator4, Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.mnuHideWhite, Me.mnuRefreshLost, Me.mnuVerifyD24H, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder
        Me.FileMenu.Name = "FileMenu"
        Me.FileMenu.Size = New System.Drawing.Size(48, 20)
        Me.FileMenu.Text = "&Файл"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(287, 6)
        '
        'mnuManualQuery
        '
        Me.mnuManualQuery.Name = "mnuManualQuery"
        Me.mnuManualQuery.Size = New System.Drawing.Size(290, 22)
        Me.mnuManualQuery.Text = "Данные"
        '
        'mnuNC
        '
        Me.mnuNC.Name = "mnuNC"
        Me.mnuNC.Size = New System.Drawing.Size(290, 22)
        Me.mnuNC.Text = "Нештатные ситуации"
        '
        'nmuGraph
        '
        Me.nmuGraph.Name = "nmuGraph"
        Me.nmuGraph.Size = New System.Drawing.Size(290, 22)
        Me.nmuGraph.Text = "Графики"
        '
        'mnuSchema
        '
        Me.mnuSchema.Name = "mnuSchema"
        Me.mnuSchema.Size = New System.Drawing.Size(290, 22)
        Me.mnuSchema.Text = "Схема подключения"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(287, 6)
        '
        'mnuSezon0
        '
        Me.mnuSezon0.Name = "mnuSezon0"
        Me.mnuSezon0.Size = New System.Drawing.Size(290, 22)
        Me.mnuSezon0.Text = "Режим: отопительный сезон"
        '
        'mnuSezon1
        '
        Me.mnuSezon1.Name = "mnuSezon1"
        Me.mnuSezon1.Size = New System.Drawing.Size(290, 22)
        Me.mnuSezon1.Text = "Режим: межотопительный период"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(287, 6)
        '
        'mnuSetupModems
        '
        Me.mnuSetupModems.Name = "mnuSetupModems"
        Me.mnuSetupModems.Size = New System.Drawing.Size(290, 22)
        Me.mnuSetupModems.Text = "Настроить модемы"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(287, 6)
        '
        'mnuStopAlert
        '
        Me.mnuStopAlert.Name = "mnuStopAlert"
        Me.mnuStopAlert.Size = New System.Drawing.Size(290, 22)
        Me.mnuStopAlert.Text = "Запретить оповещение"
        '
        'mnuStartAlert
        '
        Me.mnuStartAlert.Name = "mnuStartAlert"
        Me.mnuStartAlert.Size = New System.Drawing.Size(290, 22)
        Me.mnuStartAlert.Text = "Разрешить оповещение"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(287, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(290, 22)
        Me.ToolStripMenuItem1.Text = "Узлы слева"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(290, 22)
        Me.ToolStripMenuItem2.Text = "Узлы сверху"
        '
        'mnuHideWhite
        '
        Me.mnuHideWhite.Name = "mnuHideWhite"
        Me.mnuHideWhite.Size = New System.Drawing.Size(290, 22)
        Me.mnuHideWhite.Text = "Скрыть узлы, не требующие внимания"
        '
        'mnuRefreshLost
        '
        Me.mnuRefreshLost.Name = "mnuRefreshLost"
        Me.mnuRefreshLost.Size = New System.Drawing.Size(290, 22)
        Me.mnuRefreshLost.Text = "Обновить статус всех узлов"
        '
        'mnuVerifyD24H
        '
        Me.mnuVerifyD24H.Name = "mnuVerifyD24H"
        Me.mnuVerifyD24H.Size = New System.Drawing.Size(290, 22)
        Me.mnuVerifyD24H.Text = "Суточные = Часовые ?"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(287, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(290, 22)
        Me.ExitToolStripMenuItem.Text = "В&ыход"
        '
        'HelpMenu
        '
        Me.HelpMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator8, Me.AboutToolStripMenuItem})
        Me.HelpMenu.Name = "HelpMenu"
        Me.HelpMenu.Size = New System.Drawing.Size(44, 20)
        Me.HelpMenu.Text = "&Help"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(116, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.AboutToolStripMenuItem.Text = "&About ..."
        '
        'txtFilter
        '
        Me.txtFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFilter.Location = New System.Drawing.Point(6, 418)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(184, 20)
        Me.txtFilter.TabIndex = 39
        Me.ToolTip.SetToolTip(Me.txtFilter, "Фильтр")
        '
        'utm
        '
        Me.utm.AllowMaximize = True
        Me.utm.MdiParent = Me
        Me.utm.SaveSettings = True
        Me.utm.SaveSettingsFormat = Infragistics.Win.SaveSettingsFormat.Xml
        Me.utm.SettingsKey = "frmMain.utm"
        '
        'ImgNodeType
        '
        Me.ImgNodeType.ImageStream = CType(resources.GetObject("ImgNodeType.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgNodeType.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgNodeType.Images.SetKeyName(0, "1348225235_folder_horizontal_open.png")
        Me.ImgNodeType.Images.SetKeyName(1, "phone.jpg")
        Me.ImgNodeType.Images.SetKeyName(2, "ico_ip.gif")
        Me.ImgNodeType.Images.SetKeyName(3, "grps32.jpg")
        Me.ImgNodeType.Images.SetKeyName(4, "GSM.png")
        Me.ImgNodeType.Images.SetKeyName(5, "phone_cold.jpg")
        Me.ImgNodeType.Images.SetKeyName(6, "ico_ip_cold.gif")
        Me.ImgNodeType.Images.SetKeyName(7, "grps_cold32.jpg")
        Me.ImgNodeType.Images.SetKeyName(8, "GSM_COLD.png")
        '
        'Timer1
        '
        Me.Timer1.Interval = 60000
        '
        'UltraToolTipManager1
        '
        Me.UltraToolTipManager1.ContainingControl = Me
        '
        'udm
        '
        Me.udm.CaptionStyle = Infragistics.Win.UltraWinDock.CaptionStyle.Windows
        Me.udm.CompressUnpinnedTabs = False
        Me.udm.HostControl = Me
        Me.udm.SettingsKey = "frmMain.udm"
        Me.udm.ShowCloseButton = False
        Me.udm.ShowPinButton = False
        Me.udm.UnpinnedTabStyle = Infragistics.Win.UltraWinTabs.TabStyle.PropertyPage
        Me.udm.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.Windows
        '
        '_frmMainUnpinnedTabAreaLeft
        '
        Me._frmMainUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me._frmMainUnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._frmMainUnpinnedTabAreaLeft.Location = New System.Drawing.Point(0, 24)
        Me._frmMainUnpinnedTabAreaLeft.Name = "_frmMainUnpinnedTabAreaLeft"
        Me._frmMainUnpinnedTabAreaLeft.Owner = Me.udm
        Me._frmMainUnpinnedTabAreaLeft.Size = New System.Drawing.Size(0, 474)
        Me._frmMainUnpinnedTabAreaLeft.TabIndex = 7
        '
        '_frmMainUnpinnedTabAreaRight
        '
        Me._frmMainUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right
        Me._frmMainUnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._frmMainUnpinnedTabAreaRight.Location = New System.Drawing.Point(853, 24)
        Me._frmMainUnpinnedTabAreaRight.Name = "_frmMainUnpinnedTabAreaRight"
        Me._frmMainUnpinnedTabAreaRight.Owner = Me.udm
        Me._frmMainUnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 474)
        Me._frmMainUnpinnedTabAreaRight.TabIndex = 8
        '
        '_frmMainUnpinnedTabAreaTop
        '
        Me._frmMainUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top
        Me._frmMainUnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._frmMainUnpinnedTabAreaTop.Location = New System.Drawing.Point(0, 24)
        Me._frmMainUnpinnedTabAreaTop.Name = "_frmMainUnpinnedTabAreaTop"
        Me._frmMainUnpinnedTabAreaTop.Owner = Me.udm
        Me._frmMainUnpinnedTabAreaTop.Size = New System.Drawing.Size(853, 0)
        Me._frmMainUnpinnedTabAreaTop.TabIndex = 9
        '
        '_frmMainUnpinnedTabAreaBottom
        '
        Me._frmMainUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me._frmMainUnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._frmMainUnpinnedTabAreaBottom.Location = New System.Drawing.Point(0, 498)
        Me._frmMainUnpinnedTabAreaBottom.Name = "_frmMainUnpinnedTabAreaBottom"
        Me._frmMainUnpinnedTabAreaBottom.Owner = Me.udm
        Me._frmMainUnpinnedTabAreaBottom.Size = New System.Drawing.Size(853, 0)
        Me._frmMainUnpinnedTabAreaBottom.TabIndex = 10
        '
        '_frmMainAutoHideControl
        '
        Me._frmMainAutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me._frmMainAutoHideControl.Location = New System.Drawing.Point(0, 0)
        Me._frmMainAutoHideControl.Name = "_frmMainAutoHideControl"
        Me._frmMainAutoHideControl.Owner = Me.udm
        Me._frmMainAutoHideControl.Size = New System.Drawing.Size(0, 0)
        Me._frmMainAutoHideControl.TabIndex = 11
        '
        'Узлы
        '
        '
        'Узлы.ClientArea
        '
        Me.Узлы.ClientArea.Controls.Add(Me.txtFilter)
        Me.Узлы.ClientArea.Controls.Add(Me.ListView1)
        Me.Узлы.ClientArea.Controls.Add(Me.Button4)
        Me.Узлы.ClientArea.Controls.Add(Me.Button3)
        Me.Узлы.ClientArea.Controls.Add(Me.Button2)
        Me.Узлы.ClientArea.Controls.Add(Me.Button1)
        Me.Узлы.ClientArea.Controls.Add(Me.cmdTest)
        Me.Узлы.ClientArea.Controls.Add(Me.cmdBounds)
        Me.Узлы.ClientArea.Controls.Add(Me.cmdRefreshList)
        Me.Узлы.ClientArea.Controls.Add(Me.chkShoHidden)
        Me.Узлы.ClientArea.Controls.Add(Me.tree)
        Me.Узлы.ClientArea.Controls.Add(Me.ButtonConfig)
        Me.Узлы.Dock = System.Windows.Forms.DockStyle.Left
        Me.Узлы.Location = New System.Drawing.Point(0, 24)
        Me.Узлы.MinimumSize = New System.Drawing.Size(180, 180)
        Me.Узлы.Name = "Узлы"
        Me.Узлы.Size = New System.Drawing.Size(732, 474)
        Me.Узлы.TabIndex = 12
        Me.Узлы.Visible = False
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.ItemSettings.AllowEdit = Infragistics.Win.DefaultableBoolean.[False]
        Me.ListView1.ItemSettings.HideSelection = False
        Me.ListView1.ItemSettings.HotTracking = True
        Me.ListView1.Location = New System.Drawing.Point(212, 41)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(510, 429)
        Me.ListView1.TabIndex = 38
        Me.ListView1.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.List
        Me.ListView1.ViewSettingsDetails.ImageList = Me.ImgNodeType
        Me.ListView1.ViewSettingsIcons.ImageList = Me.ImageList1
        Me.ListView1.ViewSettingsList.ImageList = Me.ImgNodeType
        Me.ListView1.ViewSettingsThumbnails.ImageList = Me.ImgNodeType
        Me.ListView1.ViewSettingsTiles.ImageList = Me.ImgNodeType
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "ico_ip.gif")
        Me.ImageList1.Images.SetKeyName(1, "phone.jpg")
        Me.ImageList1.Images.SetKeyName(2, "ico_ip.gif")
        Me.ImageList1.Images.SetKeyName(3, "grps.jpg")
        Me.ImageList1.Images.SetKeyName(4, "GSM.png")
        Me.ImageList1.Images.SetKeyName(5, "phone_cold.jpg")
        Me.ImageList1.Images.SetKeyName(6, "ico_ip_cold.gif")
        Me.ImageList1.Images.SetKeyName(7, "grps_cold.jpg")
        Me.ImageList1.Images.SetKeyName(8, "GSM_COLD.png")
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(419, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(52, 23)
        Me.Button4.TabIndex = 37
        Me.Button4.Text = "пазл"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(341, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(67, 23)
        Me.Button3.TabIndex = 36
        Me.Button3.Text = "малые"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(276, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(59, 24)
        Me.Button2.TabIndex = 35
        Me.Button2.Text = "большие"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(214, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 24)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "список"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmdTest
        '
        Me.cmdTest.Location = New System.Drawing.Point(145, 3)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(52, 25)
        Me.cmdTest.TabIndex = 28
        Me.cmdTest.Text = "Тест"
        Me.cmdTest.UseVisualStyleBackColor = True
        '
        'cmdBounds
        '
        Me.cmdBounds.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdBounds.Location = New System.Drawing.Point(4, 447)
        Me.cmdBounds.Name = "cmdBounds"
        Me.cmdBounds.Size = New System.Drawing.Size(98, 24)
        Me.cmdBounds.TabIndex = 32
        Me.cmdBounds.Text = "Границы"
        Me.cmdBounds.UseVisualStyleBackColor = True
        '
        'cmdRefreshList
        '
        Me.cmdRefreshList.Location = New System.Drawing.Point(4, 4)
        Me.cmdRefreshList.Name = "cmdRefreshList"
        Me.cmdRefreshList.Size = New System.Drawing.Size(64, 24)
        Me.cmdRefreshList.TabIndex = 31
        Me.cmdRefreshList.Text = "Обновить"
        Me.cmdRefreshList.UseVisualStyleBackColor = True
        '
        'chkShoHidden
        '
        Me.chkShoHidden.AutoSize = True
        Me.chkShoHidden.BackColor = System.Drawing.Color.Transparent
        Me.chkShoHidden.Location = New System.Drawing.Point(74, 7)
        Me.chkShoHidden.Name = "chkShoHidden"
        Me.chkShoHidden.Size = New System.Drawing.Size(72, 17)
        Me.chkShoHidden.TabIndex = 30
        Me.chkShoHidden.Text = "Скрытые"
        Me.chkShoHidden.UseVisualStyleBackColor = False
        '
        'tree
        '
        Me.tree.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tree.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tree.HideSelection = False
        Me.tree.ImageList = Me.ImgNodeType
        Me.tree.Location = New System.Drawing.Point(4, 34)
        Me.tree.Name = "tree"
        Me.tree.NodeConnectorStyle = Infragistics.Win.UltraWinTree.NodeConnectorStyle.None
        Me.tree.Scrollable = Infragistics.Win.UltraWinTree.Scrollbar.Show
        Me.tree.Size = New System.Drawing.Size(187, 379)
        Me.tree.TabIndex = 29
        '
        'ButtonConfig
        '
        Me.ButtonConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonConfig.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonConfig.Location = New System.Drawing.Point(108, 447)
        Me.ButtonConfig.Name = "ButtonConfig"
        Me.ButtonConfig.Size = New System.Drawing.Size(89, 24)
        Me.ButtonConfig.TabIndex = 27
        Me.ButtonConfig.Text = "Настройка"
        Me.ButtonConfig.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 498)
        Me.Controls.Add(Me._frmMainAutoHideControl)
        Me.Controls.Add(Me.Узлы)
        Me.Controls.Add(Me._frmMainUnpinnedTabAreaLeft)
        Me.Controls.Add(Me._frmMainUnpinnedTabAreaTop)
        Me.Controls.Add(Me._frmMainUnpinnedTabAreaBottom)
        Me.Controls.Add(Me._frmMainUnpinnedTabAreaRight)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ВОДОПАД-IP. Диспетчер"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        CType(Me.utm, System.Configuration.IPersistComponentSettings).LoadComponentSettings()
        CType(Me.utm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Узлы.ClientArea.ResumeLayout(False)
        Me.Узлы.ClientArea.PerformLayout()
        Me.Узлы.ResumeLayout(False)
        CType(Me.ListView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HelpMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents utm As Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager
    Friend WithEvents mnuManualQuery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents nmuGraph As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSchema As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetupModems As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents UltraToolTipManager1 As Infragistics.Win.UltraWinToolTip.UltraToolTipManager
    Friend WithEvents ImgNodeType As System.Windows.Forms.ImageList
    Friend WithEvents mnuStartAlert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStopAlert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents _frmMainAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl
    Friend WithEvents udm As Infragistics.Win.UltraWinDock.UltraDockManager
    Friend WithEvents _frmMainUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _frmMainUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _frmMainUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _frmMainUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents Узлы As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents cmdBounds As System.Windows.Forms.Button
    Friend WithEvents cmdRefreshList As System.Windows.Forms.Button
    Friend WithEvents chkShoHidden As System.Windows.Forms.CheckBox
    Friend WithEvents tree As Infragistics.Win.UltraWinTree.UltraTree
    Friend WithEvents cmdTest As System.Windows.Forms.Button
    Friend WithEvents ButtonConfig As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents mnuHideWhite As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListView1 As Infragistics.Win.UltraWinListView.UltraListView
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSezon0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSezon1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRefreshLost As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVerifyD24H As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtFilter As TextBox
End Class
