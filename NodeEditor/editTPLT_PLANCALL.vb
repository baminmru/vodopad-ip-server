
Imports System.Windows.Forms
Imports Microsoft.VisualBasic



''' <summary>
'''Контрол редактирования раздела План опроса устройств режим:
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Class editTPLT_PLANCALL
    Inherits System.Windows.Forms.UserControl


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Private mOnInit As Boolean = False
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDLASTDAY As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDLASTDAY As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblNUM24 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtNUM24 As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtICALL24 As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblC24 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbC24 As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents lblICALL24 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents lblDNEXT24 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDNEXT24 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDLASTHOUR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDLASTHOUR As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblNUMHOUR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtNUMHOUR As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblICALL As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtICALL As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblCHOUR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbCHOUR As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents lblDNEXTHOUR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDNEXTHOUR As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblDNEXTCURR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDNEXTCURR As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCCURR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbCCURR As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents lblICALLCURR As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtICALLCURR As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCSUM As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbCSUM As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents lblICALLSUM As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtICALLSUM As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblDNEXTSUM As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDNEXTSUM As System.Windows.Forms.DateTimePicker
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtMINREPEAT As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtNMAXCALL As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDBEGCURR As System.Windows.Forms.DateTimePicker
    Private mChanged As Boolean = False
    Public Event Changed() ' Implements LATIRGuiControls.IRowEditor.Changed 
    Public Event Saved() ' Implements LATIRGuiControls.IRowEditor.Saved
    Public Event Refreshed() ' Implements LATIRGuiControls.IRowEditor.Refreshed
    Public Sub Changing()
        If Not mOnInit Then
            mChanged = True
            RaiseEvent Changed()
        End If
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Dim iii As Integer
    Friend WithEvents HolderPanel As System.Windows.Forms.Panel
    Friend WithEvents lblCSTATUS As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbCSTATUS As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend cmbCSTATUSDATA As DataTable
    Friend cmbCSTATUSDATAROW As DataRow
    Friend cmbCCURRDATA As DataTable
    Friend cmbCCURRDATAROW As DataRow
    Friend cmbCHOURDATA As DataTable
    Friend cmbCHOURDATAROW As DataRow
    Friend cmbC24DATA As DataTable
    Friend cmbC24DATAROW As DataRow
    Friend cmbCSUMDATA As DataTable
    Friend cmbCSUMDATAROW As DataRow
    Friend cmbCHNRONLYDATA As DataTable
    Friend cmbCHNRONLYDATAROW As DataRow
    Friend cmbC24NRONLYDATA As DataTable
    Friend cmbC24NRONLYDATAROW As DataRow
    Friend WithEvents lblDLASTCALL As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dtpDLASTCALL As System.Windows.Forms.DateTimePicker
    Friend cmbREPTDAYDATA As DataTable
    Friend cmbREPTDAYDATAROW As DataRow
    Friend cmbREPTHOURDATA As DataTable
    Friend cmbREPTHOURDATAROW As DataRow

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.HolderPanel = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblCSUM = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbCSUM = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.lblICALLSUM = New Infragistics.Win.Misc.UltraLabel()
        Me.txtICALLSUM = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblDNEXTSUM = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDNEXTSUM = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblDLASTDAY = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDLASTDAY = New System.Windows.Forms.DateTimePicker()
        Me.lblNUM24 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtNUM24 = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtICALL24 = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblC24 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbC24 = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.lblICALL24 = New Infragistics.Win.Misc.UltraLabel()
        Me.lblDNEXT24 = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDNEXT24 = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblDLASTHOUR = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDLASTHOUR = New System.Windows.Forms.DateTimePicker()
        Me.lblNUMHOUR = New Infragistics.Win.Misc.UltraLabel()
        Me.txtNUMHOUR = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblICALL = New Infragistics.Win.Misc.UltraLabel()
        Me.txtICALL = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCHOUR = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbCHOUR = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.lblDNEXTHOUR = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDNEXTHOUR = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtMINREPEAT = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtNMAXCALL = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblDNEXTCURR = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDNEXTCURR = New System.Windows.Forms.DateTimePicker()
        Me.lblCCURR = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbCCURR = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.lblICALLCURR = New Infragistics.Win.Misc.UltraLabel()
        Me.txtICALLCURR = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCSTATUS = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbCSTATUS = New Infragistics.Win.UltraWinEditors.UltraOptionSet()
        Me.lblDLASTCALL = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDLASTCALL = New System.Windows.Forms.DateTimePicker()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.dtpDBEGCURR = New System.Windows.Forms.DateTimePicker()
        Me.HolderPanel.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.cmbCSUM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtICALLSUM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.txtNUM24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtICALL24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbC24, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtNUMHOUR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtICALL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCHOUR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtMINREPEAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNMAXCALL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCCURR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtICALLCURR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCSTATUS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HolderPanel
        '
        Me.HolderPanel.AllowDrop = True
        Me.HolderPanel.BackColor = System.Drawing.SystemColors.Control
        Me.HolderPanel.Controls.Add(Me.UltraLabel3)
        Me.HolderPanel.Controls.Add(Me.dtpDBEGCURR)
        Me.HolderPanel.Controls.Add(Me.GroupBox4)
        Me.HolderPanel.Controls.Add(Me.GroupBox3)
        Me.HolderPanel.Controls.Add(Me.GroupBox2)
        Me.HolderPanel.Controls.Add(Me.GroupBox1)
        Me.HolderPanel.Controls.Add(Me.lblCSTATUS)
        Me.HolderPanel.Controls.Add(Me.cmbCSTATUS)
        Me.HolderPanel.Controls.Add(Me.lblDLASTCALL)
        Me.HolderPanel.Controls.Add(Me.dtpDLASTCALL)
        Me.HolderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HolderPanel.Name = "HolderPanel"
        Me.HolderPanel.Size = New System.Drawing.Size(756, 362)
        Me.HolderPanel.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblCSUM)
        Me.GroupBox4.Controls.Add(Me.cmbCSUM)
        Me.GroupBox4.Controls.Add(Me.lblICALLSUM)
        Me.GroupBox4.Controls.Add(Me.txtICALLSUM)
        Me.GroupBox4.Controls.Add(Me.lblDNEXTSUM)
        Me.GroupBox4.Controls.Add(Me.dtpDNEXTSUM)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 267)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(726, 66)
        Me.GroupBox4.TabIndex = 34
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Итоговые"
        '
        'lblCSUM
        '
        Me.lblCSUM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblCSUM.ForeColor = System.Drawing.Color.Black
        Me.lblCSUM.Location = New System.Drawing.Point(17, 19)
        Me.lblCSUM.Name = "lblCSUM"
        Me.lblCSUM.Size = New System.Drawing.Size(124, 20)
        Me.lblCSUM.TabIndex = 35
        Me.lblCSUM.Text = "Опрашивать"
        '
        'cmbCSUM
        '
        Me.cmbCSUM.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched
        Me.cmbCSUM.ItemSpacingHorizontal = 5
        Me.cmbCSUM.ItemSpacingVertical = 5
        Me.cmbCSUM.Location = New System.Drawing.Point(6, 41)
        Me.cmbCSUM.MinColumnWidth = 60
        Me.cmbCSUM.Name = "cmbCSUM"
        Me.cmbCSUM.Size = New System.Drawing.Size(153, 20)
        Me.cmbCSUM.TabIndex = 36
        '
        'lblICALLSUM
        '
        Me.lblICALLSUM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblICALLSUM.ForeColor = System.Drawing.Color.Black
        Me.lblICALLSUM.Location = New System.Drawing.Point(147, 19)
        Me.lblICALLSUM.Name = "lblICALLSUM"
        Me.lblICALLSUM.Size = New System.Drawing.Size(108, 20)
        Me.lblICALLSUM.TabIndex = 37
        Me.lblICALLSUM.Text = "Интервал (мин.)"
        '
        'txtICALLSUM
        '
        Me.txtICALLSUM.Location = New System.Drawing.Point(167, 43)
        Me.txtICALLSUM.MaxLength = 15
        Me.txtICALLSUM.Name = "txtICALLSUM"
        Me.txtICALLSUM.Size = New System.Drawing.Size(80, 21)
        Me.txtICALLSUM.TabIndex = 38
        '
        'lblDNEXTSUM
        '
        Me.lblDNEXTSUM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDNEXTSUM.ForeColor = System.Drawing.Color.Black
        Me.lblDNEXTSUM.Location = New System.Drawing.Point(255, 19)
        Me.lblDNEXTSUM.Name = "lblDNEXTSUM"
        Me.lblDNEXTSUM.Size = New System.Drawing.Size(162, 20)
        Me.lblDNEXTSUM.TabIndex = 39
        Me.lblDNEXTSUM.Text = "Cледующий опрос"
        '
        'dtpDNEXTSUM
        '
        Me.dtpDNEXTSUM.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDNEXTSUM.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDNEXTSUM.Location = New System.Drawing.Point(253, 41)
        Me.dtpDNEXTSUM.Name = "dtpDNEXTSUM"
        Me.dtpDNEXTSUM.ShowCheckBox = True
        Me.dtpDNEXTSUM.Size = New System.Drawing.Size(158, 20)
        Me.dtpDNEXTSUM.TabIndex = 40
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblDLASTDAY)
        Me.GroupBox3.Controls.Add(Me.dtpDLASTDAY)
        Me.GroupBox3.Controls.Add(Me.lblNUM24)
        Me.GroupBox3.Controls.Add(Me.txtNUM24)
        Me.GroupBox3.Controls.Add(Me.txtICALL24)
        Me.GroupBox3.Controls.Add(Me.lblC24)
        Me.GroupBox3.Controls.Add(Me.cmbC24)
        Me.GroupBox3.Controls.Add(Me.lblICALL24)
        Me.GroupBox3.Controls.Add(Me.lblDNEXT24)
        Me.GroupBox3.Controls.Add(Me.dtpDNEXT24)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 195)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(726, 66)
        Me.GroupBox3.TabIndex = 23
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Суточные"
        '
        'lblDLASTDAY
        '
        Me.lblDLASTDAY.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDLASTDAY.ForeColor = System.Drawing.Color.Black
        Me.lblDLASTDAY.Location = New System.Drawing.Point(538, 19)
        Me.lblDLASTDAY.Name = "lblDLASTDAY"
        Me.lblDLASTDAY.Size = New System.Drawing.Size(158, 15)
        Me.lblDLASTDAY.TabIndex = 32
        Me.lblDLASTDAY.Text = "Последний опрос"
        '
        'dtpDLASTDAY
        '
        Me.dtpDLASTDAY.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDLASTDAY.Enabled = False
        Me.dtpDLASTDAY.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDLASTDAY.Location = New System.Drawing.Point(538, 38)
        Me.dtpDLASTDAY.Name = "dtpDLASTDAY"
        Me.dtpDLASTDAY.Size = New System.Drawing.Size(158, 20)
        Me.dtpDLASTDAY.TabIndex = 33
        '
        'lblNUM24
        '
        Me.lblNUM24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblNUM24.ForeColor = System.Drawing.Color.Black
        Me.lblNUM24.Location = New System.Drawing.Point(423, 17)
        Me.lblNUM24.Name = "lblNUM24"
        Me.lblNUM24.Size = New System.Drawing.Size(122, 17)
        Me.lblNUM24.TabIndex = 30
        Me.lblNUM24.Text = "Опрашивать (сут.)"
        '
        'txtNUM24
        '
        Me.txtNUM24.Location = New System.Drawing.Point(426, 38)
        Me.txtNUM24.MaxLength = 15
        Me.txtNUM24.Name = "txtNUM24"
        Me.txtNUM24.Size = New System.Drawing.Size(106, 21)
        Me.txtNUM24.TabIndex = 31
        '
        'txtICALL24
        '
        Me.txtICALL24.Location = New System.Drawing.Point(167, 40)
        Me.txtICALL24.MaxLength = 15
        Me.txtICALL24.Name = "txtICALL24"
        Me.txtICALL24.Size = New System.Drawing.Size(80, 21)
        Me.txtICALL24.TabIndex = 27
        '
        'lblC24
        '
        Me.lblC24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblC24.ForeColor = System.Drawing.Color.Black
        Me.lblC24.Location = New System.Drawing.Point(17, 19)
        Me.lblC24.Name = "lblC24"
        Me.lblC24.Size = New System.Drawing.Size(124, 15)
        Me.lblC24.TabIndex = 24
        Me.lblC24.Text = "Опрашивать"
        '
        'cmbC24
        '
        Me.cmbC24.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched
        Me.cmbC24.ItemSpacingHorizontal = 5
        Me.cmbC24.ItemSpacingVertical = 5
        Me.cmbC24.Location = New System.Drawing.Point(6, 38)
        Me.cmbC24.MinColumnWidth = 60
        Me.cmbC24.Name = "cmbC24"
        Me.cmbC24.Size = New System.Drawing.Size(153, 20)
        Me.cmbC24.TabIndex = 25
        '
        'lblICALL24
        '
        Me.lblICALL24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblICALL24.ForeColor = System.Drawing.Color.Black
        Me.lblICALL24.Location = New System.Drawing.Point(147, 17)
        Me.lblICALL24.Name = "lblICALL24"
        Me.lblICALL24.Size = New System.Drawing.Size(108, 15)
        Me.lblICALL24.TabIndex = 26
        Me.lblICALL24.Text = "Интервал (час.)"
        '
        'lblDNEXT24
        '
        Me.lblDNEXT24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDNEXT24.ForeColor = System.Drawing.Color.Black
        Me.lblDNEXT24.Location = New System.Drawing.Point(255, 19)
        Me.lblDNEXT24.Name = "lblDNEXT24"
        Me.lblDNEXT24.Size = New System.Drawing.Size(162, 15)
        Me.lblDNEXT24.TabIndex = 28
        Me.lblDNEXT24.Text = "Следующий опрос"
        '
        'dtpDNEXT24
        '
        Me.dtpDNEXT24.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDNEXT24.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDNEXT24.Location = New System.Drawing.Point(253, 38)
        Me.dtpDNEXT24.Name = "dtpDNEXT24"
        Me.dtpDNEXT24.ShowCheckBox = True
        Me.dtpDNEXT24.Size = New System.Drawing.Size(158, 20)
        Me.dtpDNEXT24.TabIndex = 29
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblDLASTHOUR)
        Me.GroupBox2.Controls.Add(Me.dtpDLASTHOUR)
        Me.GroupBox2.Controls.Add(Me.lblNUMHOUR)
        Me.GroupBox2.Controls.Add(Me.txtNUMHOUR)
        Me.GroupBox2.Controls.Add(Me.lblICALL)
        Me.GroupBox2.Controls.Add(Me.txtICALL)
        Me.GroupBox2.Controls.Add(Me.lblCHOUR)
        Me.GroupBox2.Controls.Add(Me.cmbCHOUR)
        Me.GroupBox2.Controls.Add(Me.lblDNEXTHOUR)
        Me.GroupBox2.Controls.Add(Me.dtpDNEXTHOUR)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 123)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(726, 66)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Часовые"
        '
        'lblDLASTHOUR
        '
        Me.lblDLASTHOUR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDLASTHOUR.ForeColor = System.Drawing.Color.Black
        Me.lblDLASTHOUR.Location = New System.Drawing.Point(538, 15)
        Me.lblDLASTHOUR.Name = "lblDLASTHOUR"
        Me.lblDLASTHOUR.Size = New System.Drawing.Size(158, 20)
        Me.lblDLASTHOUR.TabIndex = 21
        Me.lblDLASTHOUR.Text = "Последний опрос"
        '
        'dtpDLASTHOUR
        '
        Me.dtpDLASTHOUR.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDLASTHOUR.Enabled = False
        Me.dtpDLASTHOUR.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDLASTHOUR.Location = New System.Drawing.Point(538, 38)
        Me.dtpDLASTHOUR.Name = "dtpDLASTHOUR"
        Me.dtpDLASTHOUR.Size = New System.Drawing.Size(158, 20)
        Me.dtpDLASTHOUR.TabIndex = 22
        '
        'lblNUMHOUR
        '
        Me.lblNUMHOUR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblNUMHOUR.ForeColor = System.Drawing.Color.Black
        Me.lblNUMHOUR.Location = New System.Drawing.Point(423, 15)
        Me.lblNUMHOUR.Name = "lblNUMHOUR"
        Me.lblNUMHOUR.Size = New System.Drawing.Size(122, 20)
        Me.lblNUMHOUR.TabIndex = 19
        Me.lblNUMHOUR.Text = "Опрашивать ( час.)"
        '
        'txtNUMHOUR
        '
        Me.txtNUMHOUR.Location = New System.Drawing.Point(426, 38)
        Me.txtNUMHOUR.MaxLength = 15
        Me.txtNUMHOUR.Name = "txtNUMHOUR"
        Me.txtNUMHOUR.Size = New System.Drawing.Size(106, 21)
        Me.txtNUMHOUR.TabIndex = 20
        '
        'lblICALL
        '
        Me.lblICALL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblICALL.ForeColor = System.Drawing.Color.Black
        Me.lblICALL.Location = New System.Drawing.Point(147, 15)
        Me.lblICALL.Name = "lblICALL"
        Me.lblICALL.Size = New System.Drawing.Size(108, 20)
        Me.lblICALL.TabIndex = 15
        Me.lblICALL.Text = "Интервал (мин.)"
        '
        'txtICALL
        '
        Me.txtICALL.Location = New System.Drawing.Point(167, 38)
        Me.txtICALL.MaxLength = 15
        Me.txtICALL.Name = "txtICALL"
        Me.txtICALL.Size = New System.Drawing.Size(80, 21)
        Me.txtICALL.TabIndex = 16
        '
        'lblCHOUR
        '
        Me.lblCHOUR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblCHOUR.ForeColor = System.Drawing.Color.Black
        Me.lblCHOUR.Location = New System.Drawing.Point(17, 15)
        Me.lblCHOUR.Name = "lblCHOUR"
        Me.lblCHOUR.Size = New System.Drawing.Size(124, 20)
        Me.lblCHOUR.TabIndex = 13
        Me.lblCHOUR.Text = "Опрашивать"
        '
        'cmbCHOUR
        '
        Me.cmbCHOUR.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched
        Me.cmbCHOUR.ItemSpacingHorizontal = 5
        Me.cmbCHOUR.ItemSpacingVertical = 5
        Me.cmbCHOUR.Location = New System.Drawing.Point(6, 38)
        Me.cmbCHOUR.MinColumnWidth = 60
        Me.cmbCHOUR.Name = "cmbCHOUR"
        Me.cmbCHOUR.Size = New System.Drawing.Size(153, 20)
        Me.cmbCHOUR.TabIndex = 14
        '
        'lblDNEXTHOUR
        '
        Me.lblDNEXTHOUR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDNEXTHOUR.ForeColor = System.Drawing.Color.Black
        Me.lblDNEXTHOUR.Location = New System.Drawing.Point(255, 15)
        Me.lblDNEXTHOUR.Name = "lblDNEXTHOUR"
        Me.lblDNEXTHOUR.Size = New System.Drawing.Size(162, 20)
        Me.lblDNEXTHOUR.TabIndex = 17
        Me.lblDNEXTHOUR.Text = "Следующий опрос"
        '
        'dtpDNEXTHOUR
        '
        Me.dtpDNEXTHOUR.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDNEXTHOUR.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDNEXTHOUR.Location = New System.Drawing.Point(253, 38)
        Me.dtpDNEXTHOUR.Name = "dtpDNEXTHOUR"
        Me.dtpDNEXTHOUR.ShowCheckBox = True
        Me.dtpDNEXTHOUR.Size = New System.Drawing.Size(158, 20)
        Me.dtpDNEXTHOUR.TabIndex = 18
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.UltraLabel2)
        Me.GroupBox1.Controls.Add(Me.txtMINREPEAT)
        Me.GroupBox1.Controls.Add(Me.UltraLabel1)
        Me.GroupBox1.Controls.Add(Me.txtNMAXCALL)
        Me.GroupBox1.Controls.Add(Me.lblDNEXTCURR)
        Me.GroupBox1.Controls.Add(Me.dtpDNEXTCURR)
        Me.GroupBox1.Controls.Add(Me.lblCCURR)
        Me.GroupBox1.Controls.Add(Me.cmbCCURR)
        Me.GroupBox1.Controls.Add(Me.lblICALLCURR)
        Me.GroupBox1.Controls.Add(Me.txtICALLCURR)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 51)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(726, 66)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Текущие"
        '
        'UltraLabel2
        '
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.UltraLabel2.ForeColor = System.Drawing.Color.Black
        Me.UltraLabel2.Location = New System.Drawing.Point(535, 19)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(185, 20)
        Me.UltraLabel2.TabIndex = 23
        Me.UltraLabel2.Text = "Интервал между попытками"
        '
        'txtMINREPEAT
        '
        Me.txtMINREPEAT.Location = New System.Drawing.Point(538, 38)
        Me.txtMINREPEAT.MaxLength = 15
        Me.txtMINREPEAT.Name = "txtMINREPEAT"
        Me.txtMINREPEAT.Size = New System.Drawing.Size(158, 21)
        Me.txtMINREPEAT.TabIndex = 24
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.UltraLabel1.ForeColor = System.Drawing.Color.Black
        Me.UltraLabel1.Location = New System.Drawing.Point(423, 19)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(122, 20)
        Me.UltraLabel1.TabIndex = 21
        Me.UltraLabel1.Text = "Число попыток"
        '
        'txtNMAXCALL
        '
        Me.txtNMAXCALL.Location = New System.Drawing.Point(426, 38)
        Me.txtNMAXCALL.MaxLength = 15
        Me.txtNMAXCALL.Name = "txtNMAXCALL"
        Me.txtNMAXCALL.Size = New System.Drawing.Size(106, 21)
        Me.txtNMAXCALL.TabIndex = 22
        '
        'lblDNEXTCURR
        '
        Me.lblDNEXTCURR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDNEXTCURR.ForeColor = System.Drawing.Color.Black
        Me.lblDNEXTCURR.Location = New System.Drawing.Point(255, 14)
        Me.lblDNEXTCURR.Name = "lblDNEXTCURR"
        Me.lblDNEXTCURR.Size = New System.Drawing.Size(162, 20)
        Me.lblDNEXTCURR.TabIndex = 10
        Me.lblDNEXTCURR.Text = "Следующий опрос"
        '
        'dtpDNEXTCURR
        '
        Me.dtpDNEXTCURR.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDNEXTCURR.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDNEXTCURR.Location = New System.Drawing.Point(253, 39)
        Me.dtpDNEXTCURR.Name = "dtpDNEXTCURR"
        Me.dtpDNEXTCURR.ShowCheckBox = True
        Me.dtpDNEXTCURR.Size = New System.Drawing.Size(158, 20)
        Me.dtpDNEXTCURR.TabIndex = 11
        '
        'lblCCURR
        '
        Me.lblCCURR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblCCURR.ForeColor = System.Drawing.Color.Black
        Me.lblCCURR.Location = New System.Drawing.Point(17, 14)
        Me.lblCCURR.Name = "lblCCURR"
        Me.lblCCURR.Size = New System.Drawing.Size(124, 20)
        Me.lblCCURR.TabIndex = 6
        Me.lblCCURR.Text = "Опрашивать"
        '
        'cmbCCURR
        '
        Me.cmbCCURR.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched
        Me.cmbCCURR.ItemSpacingHorizontal = 5
        Me.cmbCCURR.ItemSpacingVertical = 5
        Me.cmbCCURR.Location = New System.Drawing.Point(6, 39)
        Me.cmbCCURR.MinColumnWidth = 60
        Me.cmbCCURR.Name = "cmbCCURR"
        Me.cmbCCURR.Size = New System.Drawing.Size(153, 20)
        Me.cmbCCURR.TabIndex = 7
        '
        'lblICALLCURR
        '
        Me.lblICALLCURR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblICALLCURR.ForeColor = System.Drawing.Color.Black
        Me.lblICALLCURR.Location = New System.Drawing.Point(147, 14)
        Me.lblICALLCURR.Name = "lblICALLCURR"
        Me.lblICALLCURR.Size = New System.Drawing.Size(108, 20)
        Me.lblICALLCURR.TabIndex = 8
        Me.lblICALLCURR.Text = "Интервал (мин.)"
        '
        'txtICALLCURR
        '
        Me.txtICALLCURR.Location = New System.Drawing.Point(167, 38)
        Me.txtICALLCURR.MaxLength = 15
        Me.txtICALLCURR.Name = "txtICALLCURR"
        Me.txtICALLCURR.Size = New System.Drawing.Size(80, 21)
        Me.txtICALLCURR.TabIndex = 9
        '
        'lblCSTATUS
        '
        Me.lblCSTATUS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblCSTATUS.ForeColor = System.Drawing.Color.Black
        Me.lblCSTATUS.Location = New System.Drawing.Point(20, 5)
        Me.lblCSTATUS.Name = "lblCSTATUS"
        Me.lblCSTATUS.Size = New System.Drawing.Size(228, 20)
        Me.lblCSTATUS.TabIndex = 1
        Me.lblCSTATUS.Text = "Заблокирован для старого алгоритма"
        '
        'cmbCSTATUS
        '
        Me.cmbCSTATUS.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched
        Me.cmbCSTATUS.ItemSpacingHorizontal = 5
        Me.cmbCSTATUS.ItemSpacingVertical = 5
        Me.cmbCSTATUS.Location = New System.Drawing.Point(9, 27)
        Me.cmbCSTATUS.MinColumnWidth = 60
        Me.cmbCSTATUS.Name = "cmbCSTATUS"
        Me.cmbCSTATUS.Size = New System.Drawing.Size(235, 20)
        Me.cmbCSTATUS.TabIndex = 2
        '
        'lblDLASTCALL
        '
        Me.lblDLASTCALL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblDLASTCALL.ForeColor = System.Drawing.Color.Black
        Me.lblDLASTCALL.Location = New System.Drawing.Point(258, 3)
        Me.lblDLASTCALL.Name = "lblDLASTCALL"
        Me.lblDLASTCALL.Size = New System.Drawing.Size(200, 20)
        Me.lblDLASTCALL.TabIndex = 3
        Me.lblDLASTCALL.Text = "Дата последнего опроса счетчика"
        '
        'dtpDLASTCALL
        '
        Me.dtpDLASTCALL.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDLASTCALL.Enabled = False
        Me.dtpDLASTCALL.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDLASTCALL.Location = New System.Drawing.Point(254, 25)
        Me.dtpDLASTCALL.Name = "dtpDLASTCALL"
        Me.dtpDLASTCALL.Size = New System.Drawing.Size(157, 20)
        Me.dtpDLASTCALL.TabIndex = 4
        '
        'UltraLabel3
        '
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.UltraLabel3.ForeColor = System.Drawing.Color.Black
        Me.UltraLabel3.Location = New System.Drawing.Point(451, 5)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(200, 20)
        Me.UltraLabel3.TabIndex = 35
        Me.UltraLabel3.Text = "Дата начала сбора данных"
        '
        'dtpDBEGCURR
        '
        Me.dtpDBEGCURR.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtpDBEGCURR.Enabled = True
        Me.dtpDBEGCURR.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDBEGCURR.Location = New System.Drawing.Point(447, 27)
        Me.dtpDBEGCURR.Name = "dtpDBEGCURR"
        Me.dtpDBEGCURR.Size = New System.Drawing.Size(157, 20)
        Me.dtpDBEGCURR.TabIndex = 36
        '
        'editTPLT_PLANCALL
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.HolderPanel)
        Me.Name = "editTPLT_PLANCALL"
        Me.Size = New System.Drawing.Size(756, 365)
        Me.HolderPanel.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.cmbCSUM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtICALLSUM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.txtNUM24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtICALL24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbC24, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtNUMHOUR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtICALL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCHOUR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtMINREPEAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNMAXCALL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCCURR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtICALLCURR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCSTATUS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub cmbCSTATUS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCSTATUS.ValueChanged
        On Error Resume Next
        Changing()

    End Sub
    Private Sub txtICALL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtICALL.Validating
        If txtICALL.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtICALL.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtICALL.Text) < -2000000000 Or Val(txtICALL.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtICALL_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub

    Private Sub cmbCCURR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub cmbCHOUR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub cmbC24_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub cmbCSUM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub txtNUMHOUR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNUMHOUR.Validating
        If txtNUMHOUR.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtNUMHOUR.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtNUMHOUR.Text) < -2000000000 Or Val(txtNUMHOUR.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtNUMHOUR_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub txtNUM24_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNUM24.Validating
        If txtNUM24.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtNUM24.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtNUM24.Text) < -2000000000 Or Val(txtNUM24.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtNUM24_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub


    Private Sub dtpDLOCK_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub txtICALL24_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtICALL24.Validating
        If txtICALL24.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtICALL24.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtICALL24.Text) < -2000000000 Or Val(txtICALL24.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtICALL24_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub txtICALLCURR_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtICALLCURR.Validating
        If txtICALLCURR.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtICALLCURR.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtICALLCURR.Text) < -2000000000 Or Val(txtICALLCURR.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtICALLCURR_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub txtICALLSUM_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If txtICALLSUM.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtICALLSUM.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtICALLSUM.Text) < -2000000000 Or Val(txtICALLSUM.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtICALLSUM_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub cmbCHNRONLY_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub cmbC24NRONLY_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub dtpDNEXTHOUR_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub dtpDNEXT24_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub dtpDNEXTCURR_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub dtpDNEXTSUM_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub dtpDLASTCALL_Change(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDLASTCALL.ValueChanged
        Changing()

    End Sub
    Private Sub dtpDLASTDAY_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub dtpDLASTHOUR_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Changing()

    End Sub
    Private Sub cmbREPTDAY_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub cmbREPTHOUR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub

    Public Item As DataRow
    Private mRowReadOnly As Boolean



    ''' <summary>
    '''Инициализация
    ''' </summary>
    ''' <remarks>
    '''
    ''' </remarks>
    Public Sub Attach(ByVal nItem As DataRow, ByVal RowReadOnly As Boolean)

        Item = nItem
        mRowReadOnly = RowReadOnly

        mOnInit = True

        cmbCSTATUSDATA = New DataTable
        cmbCSTATUSDATA.Columns.Add("name", GetType(System.String))
        cmbCSTATUSDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbCSTATUSDATAROW = cmbCSTATUSDATA.NewRow
        cmbCSTATUSDATAROW("name") = "Да"
        cmbCSTATUSDATAROW("Value") = 1
        cmbCSTATUSDATA.Rows.Add(cmbCSTATUSDATAROW)
        cmbCSTATUSDATAROW = cmbCSTATUSDATA.NewRow
        cmbCSTATUSDATAROW("name") = "Нет"
        cmbCSTATUSDATAROW("Value") = 0
        cmbCSTATUSDATA.Rows.Add(cmbCSTATUSDATAROW)
        cmbCSTATUS.DisplayMember = "name"
        cmbCSTATUS.ValueMember = "Value"
        cmbCSTATUS.DataSource = cmbCSTATUSDATA
        cmbCSTATUS.Value = CInt(Item("CSTATUS"))
        txtICALL.Text = Item("ICALL").ToString()
        cmbCCURRDATA = New DataTable
        cmbCCURRDATA.Columns.Add("name", GetType(System.String))
        cmbCCURRDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbCCURRDATAROW = cmbCCURRDATA.NewRow
        cmbCCURRDATAROW("name") = "Да"
        cmbCCURRDATAROW("Value") = 1
        cmbCCURRDATA.Rows.Add(cmbCCURRDATAROW)
        cmbCCURRDATAROW = cmbCCURRDATA.NewRow
        cmbCCURRDATAROW("name") = "Нет"
        cmbCCURRDATAROW("Value") = 0
        cmbCCURRDATA.Rows.Add(cmbCCURRDATAROW)
        cmbCCURR.DisplayMember = "name"
        cmbCCURR.ValueMember = "Value"
        cmbCCURR.DataSource = cmbCCURRDATA
        cmbCCURR.Value = CInt(Item("CCURR"))
        cmbCHOURDATA = New DataTable
        cmbCHOURDATA.Columns.Add("name", GetType(System.String))
        cmbCHOURDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbCHOURDATAROW = cmbCHOURDATA.NewRow
        cmbCHOURDATAROW("name") = "Да"
        cmbCHOURDATAROW("Value") = 1
        cmbCHOURDATA.Rows.Add(cmbCHOURDATAROW)
        cmbCHOURDATAROW = cmbCHOURDATA.NewRow
        cmbCHOURDATAROW("name") = "Нет"
        cmbCHOURDATAROW("Value") = 0
        cmbCHOURDATA.Rows.Add(cmbCHOURDATAROW)
        cmbCHOUR.DisplayMember = "name"
        cmbCHOUR.ValueMember = "Value"
        cmbCHOUR.DataSource = cmbCHOURDATA
        cmbCHOUR.Value = CInt(Item("CHOUR"))
        cmbC24DATA = New DataTable
        cmbC24DATA.Columns.Add("name", GetType(System.String))
        cmbC24DATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbC24DATAROW = cmbC24DATA.NewRow
        cmbC24DATAROW("name") = "Да"
        cmbC24DATAROW("Value") = 1
        cmbC24DATA.Rows.Add(cmbC24DATAROW)
        cmbC24DATAROW = cmbC24DATA.NewRow
        cmbC24DATAROW("name") = "Нет"
        cmbC24DATAROW("Value") = 0
        cmbC24DATA.Rows.Add(cmbC24DATAROW)
        cmbC24.DisplayMember = "name"
        cmbC24.ValueMember = "Value"
        cmbC24.DataSource = cmbC24DATA
        cmbC24.Value = CInt(Item("C24"))
        cmbCSUMDATA = New DataTable
        cmbCSUMDATA.Columns.Add("name", GetType(System.String))
        cmbCSUMDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbCSUMDATAROW = cmbCSUMDATA.NewRow
        cmbCSUMDATAROW("name") = "Да"
        cmbCSUMDATAROW("Value") = 1
        cmbCSUMDATA.Rows.Add(cmbCSUMDATAROW)
        cmbCSUMDATAROW = cmbCSUMDATA.NewRow
        cmbCSUMDATAROW("name") = "Нет"
        cmbCSUMDATAROW("Value") = 0
        cmbCSUMDATA.Rows.Add(cmbCSUMDATAROW)
        cmbCSUM.DisplayMember = "name"
        cmbCSUM.ValueMember = "Value"
        cmbCSUM.DataSource = cmbCSUMDATA
        cmbCSUM.Value = CInt(Item("CSUM"))
        txtNUMHOUR.Text = Item("NUMHOUR").ToString()
        txtNUM24.Text = Item("NUM24").ToString()

        txtICALL24.Text = Item("ICALL24").ToString()
        txtICALLCURR.Text = Item("ICALLCURR").ToString()
        txtICALLSUM.Text = Item("ICALLSUM").ToString()
        cmbCHNRONLYDATA = New DataTable
        cmbCHNRONLYDATA.Columns.Add("name", GetType(System.String))
        cmbCHNRONLYDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbCHNRONLYDATAROW = cmbCHNRONLYDATA.NewRow
        cmbCHNRONLYDATAROW("name") = "Да"
        cmbCHNRONLYDATAROW("Value") = 1
        cmbCHNRONLYDATA.Rows.Add(cmbCHNRONLYDATAROW)
        cmbCHNRONLYDATAROW = cmbCHNRONLYDATA.NewRow
        cmbCHNRONLYDATAROW("name") = "Нет"
        cmbCHNRONLYDATAROW("Value") = 0
        cmbCHNRONLYDATA.Rows.Add(cmbCHNRONLYDATAROW)

        cmbC24NRONLYDATA = New DataTable
        cmbC24NRONLYDATA.Columns.Add("name", GetType(System.String))
        cmbC24NRONLYDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbC24NRONLYDATAROW = cmbC24NRONLYDATA.NewRow
        cmbC24NRONLYDATAROW("name") = "Да"
        cmbC24NRONLYDATAROW("Value") = 1
        cmbC24NRONLYDATA.Rows.Add(cmbC24NRONLYDATAROW)
        cmbC24NRONLYDATAROW = cmbC24NRONLYDATA.NewRow
        cmbC24NRONLYDATAROW("name") = "Нет"
        cmbC24NRONLYDATAROW("Value") = 0
        cmbC24NRONLYDATA.Rows.Add(cmbC24NRONLYDATAROW)

        dtpDNEXTHOUR.Value = System.DateTime.Now
        If Item("DNEXTHOUR") <> System.DateTime.MinValue Then
            dtpDNEXTHOUR.Value = Item("DNEXTHOUR")
        Else
            dtpDNEXTHOUR.Value = System.DateTime.Today
        End If
        dtpDNEXT24.Value = System.DateTime.Now
        If Item("DNEXT24") <> System.DateTime.MinValue Then
            dtpDNEXT24.Value = Item("DNEXT24")
        Else
            dtpDNEXT24.Value = System.DateTime.Today
        End If
        dtpDNEXTCURR.Value = System.DateTime.Now
        If Item("DNEXTCURR") <> System.DateTime.MinValue Then
            dtpDNEXTCURR.Value = Item("DNEXTCURR")
        Else
            dtpDNEXTCURR.Value = System.DateTime.Today
        End If
        dtpDNEXTSUM.Value = System.DateTime.Now
        If Item("DNEXTSUM") <> System.DateTime.MinValue Then
            dtpDNEXTSUM.Value = Item("DNEXTSUM")
        Else
            dtpDNEXTSUM.Value = System.DateTime.Today
        End If
        dtpDLASTCALL.Value = System.DateTime.Now
        If Item("DLASTCALL") <> System.DateTime.MinValue Then
            dtpDLASTCALL.Value = Item("DLASTCALL")
        Else
            dtpDLASTCALL.Value = System.DateTime.Today
        End If

        dtpDBEGCURR.Value = System.DateTime.Now
        If Item("dbegcurr") <> System.DateTime.MinValue Then
            dtpDBEGCURR.Value = Item("dbegcurr")
        Else
            dtpDBEGCURR.Value = System.DateTime.Today
        End If

        dtpDLASTDAY.Value = System.DateTime.Now
        If Item("DLASTDAY") <> System.DateTime.MinValue Then
            dtpDLASTDAY.Value = Item("DLASTDAY")
        Else
            dtpDLASTDAY.Value = System.DateTime.Today
        End If
        dtpDLASTHOUR.Value = System.DateTime.Now
        If Item("DLASTHOUR") <> System.DateTime.MinValue Then
            dtpDLASTHOUR.Value = Item("DLASTHOUR")
        Else
            dtpDLASTHOUR.Value = System.DateTime.Today
        End If
        cmbREPTDAYDATA = New DataTable
        cmbREPTDAYDATA.Columns.Add("name", GetType(System.String))
        cmbREPTDAYDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbREPTDAYDATAROW = cmbREPTDAYDATA.NewRow
        cmbREPTDAYDATAROW("name") = "Да"
        cmbREPTDAYDATAROW("Value") = 1
        cmbREPTDAYDATA.Rows.Add(cmbREPTDAYDATAROW)
        cmbREPTDAYDATAROW = cmbREPTDAYDATA.NewRow
        cmbREPTDAYDATAROW("name") = "Нет"
        cmbREPTDAYDATAROW("Value") = 0
        cmbREPTDAYDATA.Rows.Add(cmbREPTDAYDATAROW)

        cmbREPTHOURDATA = New DataTable
        cmbREPTHOURDATA.Columns.Add("name", GetType(System.String))
        cmbREPTHOURDATA.Columns.Add("Value", GetType(System.Int32))
        On Error Resume Next
        cmbREPTHOURDATAROW = cmbREPTHOURDATA.NewRow
        cmbREPTHOURDATAROW("name") = "Да"
        cmbREPTHOURDATAROW("Value") = 1
        cmbREPTHOURDATA.Rows.Add(cmbREPTHOURDATAROW)
        cmbREPTHOURDATAROW = cmbREPTHOURDATA.NewRow
        cmbREPTHOURDATAROW("name") = "Нет"
        cmbREPTHOURDATAROW("Value") = 0
        cmbREPTHOURDATA.Rows.Add(cmbREPTHOURDATAROW)

        txtNMAXCALL.Text = Item("NMAXCALL").ToString()
        txtMINREPEAT.Text = Item("MINREPEAT").ToString()

        mOnInit = False
        RaiseEvent Refreshed()
    End Sub


    ''' <summary>
    '''Сохранения данных в полях объекта
    ''' </summary>
    ''' <remarks>
    '''
    ''' </remarks>
    Public Sub Save() ' Implements LATIRGuiControls.IRowEditor.Save
        If mRowReadOnly = False Then

            Item("CSTATUS") = cmbCSTATUS.Value
            Item("ICALL") = Val(txtICALL.Text)
            Item("CCURR") = cmbCCURR.Value
            Item("CHOUR") = cmbCHOUR.Value
            Item("C24") = cmbC24.Value
            Item("CSUM") = cmbCSUM.Value
            Item("NUMHOUR") = Val(txtNUMHOUR.Text)
            Item("NUM24") = Val(txtNUM24.Text)

            Item("NMAXCALL") = Val(txtNMAXCALL.Text)
            Item("MINREPEAT") = Val(txtMINREPEAT.Text)


            Item("ICALL24") = Val(txtICALL24.Text)
            Item("ICALLCURR") = Val(txtICALLCURR.Text)
            Item("ICALLSUM") = Val(txtICALLSUM.Text)
            If dtpDNEXTHOUR.Value = System.DateTime.MinValue Then
                Item("DNEXTHOUR") = System.DateTime.MinValue
            Else
                Item("DNEXTHOUR") = dtpDNEXTHOUR.Value
            End If
            If dtpDNEXT24.Value = System.DateTime.MinValue Then
                Item("DNEXT24") = System.DateTime.MinValue
            Else
                Item("DNEXT24") = dtpDNEXT24.Value
            End If
            If dtpDNEXTCURR.Value = System.DateTime.MinValue Then
                Item("DNEXTCURR") = System.DateTime.MinValue
            Else
                Item("DNEXTCURR") = dtpDNEXTCURR.Value
            End If
            If dtpDNEXTSUM.Value = System.DateTime.MinValue Then
                Item("DNEXTSUM") = System.DateTime.MinValue
            Else
                Item("DNEXTSUM") = dtpDNEXTSUM.Value
            End If
            If dtpDLASTCALL.Value = System.DateTime.MinValue Then
                Item("DLASTCALL") = System.DateTime.MinValue
            Else
                Item("DLASTCALL") = dtpDLASTCALL.Value
            End If
            If dtpDBEGCURR.Value = System.DateTime.MinValue Then
                Item("dbegcurr") = System.DateTime.MinValue
            Else
                Item("dbegcurr") = dtpDBEGCURR.Value
            End If


            If dtpDLASTDAY.Value = System.DateTime.MinValue Then
                Item("DLASTDAY") = System.DateTime.MinValue
            Else
                Item("DLASTDAY") = dtpDLASTDAY.Value
            End If
            If dtpDLASTHOUR.Value = System.DateTime.MinValue Then
                Item("DLASTHOUR") = System.DateTime.MinValue
            Else
                Item("DLASTHOUR") = dtpDLASTHOUR.Value
            End If

        End If
        mChanged = False
        RaiseEvent Saved()
    End Sub
    Public Function IsOK() As Boolean ' Implements LATIRGuiControls.IRowEditor.IsOK
        Dim mIsOK As Boolean
        mIsOK = True
        If mRowReadOnly Then Return True

        Return mIsOK
    End Function
    Public Function IsChanged() As Boolean ' Implements LATIRGuiControls.IRowEditor.IsChanged
        Return mChanged
    End Function

End Class

