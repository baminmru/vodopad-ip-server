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
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.DTo = New System.Windows.Forms.DateTimePicker()
        Me.dFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.cmbSupplier = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCheckBySupplier = New System.Windows.Forms.Button()
        Me.cmdUnSelSup = New System.Windows.Forms.Button()
        Me.cmdSelSup = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.cmdPath = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTPath = New System.Windows.Forms.TextBox()
        Me.cmdTPath = New System.Windows.Forms.Button()
        Me.cdlg = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtError = New System.Windows.Forms.TextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cmdOpenDay = New System.Windows.Forms.Button()
        Me.cmdOpenHour = New System.Windows.Forms.Button()
        Me.lblDTPL = New System.Windows.Forms.Label()
        Me.lblHTPL = New System.Windows.Forms.Label()
        Me.cmdDelGroup = New System.Windows.Forms.Button()
        Me.cmbGroups = New System.Windows.Forms.ComboBox()
        Me.cmdSaveGroup = New System.Windows.Forms.Button()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.cmdProperty = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdClearAll = New System.Windows.Forms.Button()
        Me.cmdSelectAll = New System.Windows.Forms.Button()
        Me.cmbNode = New System.Windows.Forms.CheckedListBox()
        Me.wb = New SpreadsheetGear.Windows.Forms.WorkbookView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdRefresh.Location = New System.Drawing.Point(683, 5)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(171, 99)
        Me.cmdRefresh.TabIndex = 23
        Me.cmdRefresh.Text = "Генерировать отчет"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(472, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 18)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "По:"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(273, 60)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(23, 18)
        Me.lblFrom.TabIndex = 21
        Me.lblFrom.Text = "С:"
        '
        'DTo
        '
        Me.DTo.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.DTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.DTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTo.Location = New System.Drawing.Point(501, 57)
        Me.DTo.Name = "DTo"
        Me.DTo.Size = New System.Drawing.Size(166, 24)
        Me.DTo.TabIndex = 20
        '
        'dFrom
        '
        Me.dFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dFrom.Location = New System.Drawing.Point(296, 57)
        Me.dFrom.Name = "dFrom"
        Me.dFrom.Size = New System.Drawing.Size(170, 24)
        Me.dFrom.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 18)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Тип отчета:"
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(103, 57)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(173, 26)
        Me.cmbType.TabIndex = 26
        '
        'cmbSupplier
        '
        Me.cmbSupplier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSupplier.ColumnWidth = 400
        Me.cmbSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(12, 140)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(935, 124)
        Me.cmbSupplier.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 18)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Поставщик ТЭ"
        '
        'cmbCheckBySupplier
        '
        Me.cmbCheckBySupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbCheckBySupplier.Location = New System.Drawing.Point(396, 105)
        Me.cmbCheckBySupplier.Name = "cmbCheckBySupplier"
        Me.cmbCheckBySupplier.Size = New System.Drawing.Size(271, 29)
        Me.cmbCheckBySupplier.TabIndex = 32
        Me.cmbCheckBySupplier.Text = "Выбрать узлы для отмеченных поставщиков"
        Me.cmbCheckBySupplier.UseVisualStyleBackColor = True
        '
        'cmdUnSelSup
        '
        Me.cmdUnSelSup.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdUnSelSup.Location = New System.Drawing.Point(257, 105)
        Me.cmdUnSelSup.Name = "cmdUnSelSup"
        Me.cmdUnSelSup.Size = New System.Drawing.Size(133, 29)
        Me.cmdUnSelSup.TabIndex = 35
        Me.cmdUnSelSup.Text = "Отменить всех"
        Me.cmdUnSelSup.UseVisualStyleBackColor = True
        '
        'cmdSelSup
        '
        Me.cmdSelSup.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdSelSup.Location = New System.Drawing.Point(132, 105)
        Me.cmdSelSup.Name = "cmdSelSup"
        Me.cmdSelSup.Size = New System.Drawing.Size(119, 29)
        Me.cmdSelSup.TabIndex = 34
        Me.cmdSelSup.Text = "Выбрать всех"
        Me.cmdSelSup.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(167, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Папка для сохранения отчетов:"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(185, 5)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(444, 20)
        Me.txtPath.TabIndex = 37
        Me.txtPath.Text = "c:\reports\"
        '
        'cmdPath
        '
        Me.cmdPath.Location = New System.Drawing.Point(635, 4)
        Me.cmdPath.Name = "cmdPath"
        Me.cmdPath.Size = New System.Drawing.Size(32, 20)
        Me.cmdPath.TabIndex = 38
        Me.cmdPath.Text = "..."
        Me.cmdPath.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 13)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Папка с шаблонами:"
        '
        'txtTPath
        '
        Me.txtTPath.Location = New System.Drawing.Point(185, 29)
        Me.txtTPath.Name = "txtTPath"
        Me.txtTPath.Size = New System.Drawing.Size(444, 20)
        Me.txtTPath.TabIndex = 41
        Me.txtTPath.Text = "C:\bami\Projects\Szt_2012\Service_2.1\ReportXLGen\Masks\Luda"
        '
        'cmdTPath
        '
        Me.cmdTPath.Location = New System.Drawing.Point(635, 28)
        Me.cmdTPath.Name = "cmdTPath"
        Me.cmdTPath.Size = New System.Drawing.Size(32, 20)
        Me.cmdTPath.TabIndex = 42
        Me.cmdTPath.Text = "..."
        Me.cmdTPath.UseVisualStyleBackColor = True
        '
        'txtError
        '
        Me.txtError.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtError.Location = New System.Drawing.Point(860, 4)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtError.Size = New System.Drawing.Size(87, 100)
        Me.txtError.TabIndex = 43
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 279)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdOpenDay)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdOpenHour)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDTPL)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblHTPL)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdDelGroup)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbGroups)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSaveGroup)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pb)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdProperty)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdClearAll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSelectAll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbNode)
        Me.SplitContainer1.Panel1MinSize = 350
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.wb)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Panel2MinSize = 0
        Me.SplitContainer1.Size = New System.Drawing.Size(935, 314)
        Me.SplitContainer1.SplitterDistance = 371
        Me.SplitContainer1.TabIndex = 45
        '
        'cmdOpenDay
        '
        Me.cmdOpenDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOpenDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdOpenDay.Location = New System.Drawing.Point(473, 243)
        Me.cmdOpenDay.Name = "cmdOpenDay"
        Me.cmdOpenDay.Size = New System.Drawing.Size(99, 36)
        Me.cmdOpenDay.TabIndex = 50
        Me.cmdOpenDay.Text = "Часовые"
        Me.ToolTip1.SetToolTip(Me.cmdOpenDay, "Открыть шаблон для редактирования")
        Me.cmdOpenDay.UseVisualStyleBackColor = True
        '
        'cmdOpenHour
        '
        Me.cmdOpenHour.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOpenHour.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdOpenHour.Location = New System.Drawing.Point(8, 243)
        Me.cmdOpenHour.Name = "cmdOpenHour"
        Me.cmdOpenHour.Size = New System.Drawing.Size(104, 36)
        Me.cmdOpenHour.TabIndex = 49
        Me.cmdOpenHour.Text = "Суточные"
        Me.ToolTip1.SetToolTip(Me.cmdOpenHour, "Открыть шаблон для редактирования")
        Me.cmdOpenHour.UseVisualStyleBackColor = True
        '
        'lblDTPL
        '
        Me.lblDTPL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDTPL.BackColor = System.Drawing.SystemColors.Control
        Me.lblDTPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDTPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDTPL.Location = New System.Drawing.Point(120, 254)
        Me.lblDTPL.Name = "lblDTPL"
        Me.lblDTPL.Size = New System.Drawing.Size(347, 25)
        Me.lblDTPL.TabIndex = 48
        '
        'lblHTPL
        '
        Me.lblHTPL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHTPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHTPL.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblHTPL.Location = New System.Drawing.Point(578, 254)
        Me.lblHTPL.Name = "lblHTPL"
        Me.lblHTPL.Size = New System.Drawing.Size(354, 25)
        Me.lblHTPL.TabIndex = 46
        '
        'cmdDelGroup
        '
        Me.cmdDelGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelGroup.Location = New System.Drawing.Point(907, 11)
        Me.cmdDelGroup.Name = "cmdDelGroup"
        Me.cmdDelGroup.Size = New System.Drawing.Size(24, 19)
        Me.cmdDelGroup.TabIndex = 44
        Me.cmdDelGroup.Text = "X"
        Me.cmdDelGroup.UseVisualStyleBackColor = True
        '
        'cmbGroups
        '
        Me.cmbGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroups.FormattingEnabled = True
        Me.cmbGroups.Location = New System.Drawing.Point(719, 10)
        Me.cmbGroups.Name = "cmbGroups"
        Me.cmbGroups.Size = New System.Drawing.Size(182, 21)
        Me.cmbGroups.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.cmbGroups, "Выбрать  сохраненную группу")
        '
        'cmdSaveGroup
        '
        Me.cmdSaveGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdSaveGroup.Location = New System.Drawing.Point(525, 6)
        Me.cmdSaveGroup.Name = "cmdSaveGroup"
        Me.cmdSaveGroup.Size = New System.Drawing.Size(189, 29)
        Me.cmdSaveGroup.TabIndex = 42
        Me.cmdSaveGroup.Text = "Сохранить как группу"
        Me.cmdSaveGroup.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pb.Location = New System.Drawing.Point(8, 283)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(924, 24)
        Me.pb.TabIndex = 41
        '
        'cmdProperty
        '
        Me.cmdProperty.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdProperty.Location = New System.Drawing.Point(384, 6)
        Me.cmdProperty.Name = "cmdProperty"
        Me.cmdProperty.Size = New System.Drawing.Size(129, 29)
        Me.cmdProperty.TabIndex = 38
        Me.cmdProperty.Text = "Свойства"
        Me.cmdProperty.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 18)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Узлы"
        '
        'cmdClearAll
        '
        Me.cmdClearAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdClearAll.Location = New System.Drawing.Point(245, 6)
        Me.cmdClearAll.Name = "cmdClearAll"
        Me.cmdClearAll.Size = New System.Drawing.Size(133, 29)
        Me.cmdClearAll.TabIndex = 36
        Me.cmdClearAll.Text = "Отменить все узлы"
        Me.cmdClearAll.UseVisualStyleBackColor = True
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmdSelectAll.Location = New System.Drawing.Point(120, 6)
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(119, 29)
        Me.cmdSelectAll.TabIndex = 35
        Me.cmdSelectAll.Text = "Выбрать все узлы"
        Me.cmdSelectAll.UseVisualStyleBackColor = True
        '
        'cmbNode
        '
        Me.cmbNode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbNode.ColumnWidth = 300
        Me.cmbNode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cmbNode.FormattingEnabled = True
        Me.cmbNode.Location = New System.Drawing.Point(8, 41)
        Me.cmbNode.Name = "cmbNode"
        Me.cmbNode.Size = New System.Drawing.Size(924, 196)
        Me.cmbNode.TabIndex = 34
        '
        'wb
        '
        Me.wb.AllowChartExplorer = False
        Me.wb.AllowEditCommands = False
        Me.wb.AllowRangeExplorer = False
        Me.wb.AllowShapeExplorer = False
        Me.wb.AllowWorkbookDesigner = False
        Me.wb.AllowWorkbookExplorer = False
        Me.wb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.wb.Location = New System.Drawing.Point(422, 196)
        Me.wb.Name = "wb"
        Me.wb.Size = New System.Drawing.Size(61, 138)
        Me.wb.TabIndex = 41
        Me.wb.WorkbookSetState = resources.GetString("wb.WorkbookSetState")
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(959, 595)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.txtError)
        Me.Controls.Add(Me.cmdTPath)
        Me.Controls.Add(Me.txtTPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmdPath)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdUnSelSup)
        Me.Controls.Add(Me.cmdSelSup)
        Me.Controls.Add(Me.cmbCheckBySupplier)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbSupplier)
        Me.Controls.Add(Me.cmbType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.DTo)
        Me.Controls.Add(Me.dFrom)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(950, 600)
        Me.Name = "frmMain"
        Me.Text = "Генератор отчетов"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents DTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSupplier As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbCheckBySupplier As System.Windows.Forms.Button
    Friend WithEvents cmdUnSelSup As System.Windows.Forms.Button
    Friend WithEvents cmdSelSup As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents cmdPath As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTPath As System.Windows.Forms.TextBox
    Friend WithEvents cmdTPath As System.Windows.Forms.Button
    Friend WithEvents cdlg As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdClearAll As System.Windows.Forms.Button
    Friend WithEvents cmdSelectAll As System.Windows.Forms.Button
    Friend WithEvents cmbNode As System.Windows.Forms.CheckedListBox
    Friend WithEvents wb As SpreadsheetGear.Windows.Forms.WorkbookView
    Friend WithEvents cmdProperty As System.Windows.Forms.Button
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents cmbGroups As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSaveGroup As System.Windows.Forms.Button
    Friend WithEvents cmdDelGroup As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblDTPL As System.Windows.Forms.Label
    Friend WithEvents lblHTPL As System.Windows.Forms.Label
    Friend WithEvents cmdOpenDay As System.Windows.Forms.Button
    Friend WithEvents cmdOpenHour As System.Windows.Forms.Button
End Class
