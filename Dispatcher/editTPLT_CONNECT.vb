
Imports System.Windows.Forms
Imports Microsoft.VisualBasic



''' <summary>
'''Контрол редактирования раздела Параметры соединения режим:
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Class editTPLT_CONNECT
    Inherits System.Windows.Forms.UserControl


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Private mOnInit As Boolean = False
    Friend WithEvents txtCPARITY As System.Windows.Forms.TextBox
    private mChanged as boolean = false
    Public Event Changed()
    Public Event Saved()
    Public Event Refreshed()
    Public Sub Changing()
      if not mOnInit then
        mChanged = true
        raiseevent Changed()
      end if
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose (disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

 dim iii as integer
    Friend cmbConnectionEnabledDATA As DataTable
    Friend cmbConnectionEnabledDATAROW As DataRow
    Friend WithEvents lblCSPEED As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCSPEED As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblCDATABIT As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCDATABIT As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblCPARITY As Infragistics.Win.Misc.UltraLabel
    Friend cmbCPARITYDATA As DataTable
    Friend cmbCPARITYDATAROW As DataRow
    Friend WithEvents lblCSTOPBITS As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCSTOPBITS As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend cmbCDTRDATA As DataTable
    Friend cmbCDTRDATAROW As DataRow
    Friend cmbCDSRDATA As DataTable
    Friend cmbCDSRDATAROW As DataRow
    Friend WithEvents lblCTOWNCODE As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCTOWNCODE As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblCPHONE As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCPHONE As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblCPREFPHONE As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCPREFPHONE As Infragistics.Win.UltraWinEditors.UltraTextEditor

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.lblCSPEED = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCSPEED = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCDATABIT = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCDATABIT = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCPARITY = New Infragistics.Win.Misc.UltraLabel()
        Me.lblCSTOPBITS = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCSTOPBITS = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCTOWNCODE = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCTOWNCODE = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCPHONE = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCPHONE = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCPREFPHONE = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCPREFPHONE = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtCPARITY = New System.Windows.Forms.TextBox()
        CType(Me.txtCSPEED, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCDATABIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCSTOPBITS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCTOWNCODE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPHONE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPREFPHONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCSPEED
        '
        Appearance1.ForeColor = System.Drawing.Color.Black
        Me.lblCSPEED.Appearance = Appearance1
        Me.lblCSPEED.ForeColor = System.Drawing.Color.Blue
        Me.lblCSPEED.Location = New System.Drawing.Point(20, 5)
        Me.lblCSPEED.Name = "lblCSPEED"
        Me.lblCSPEED.Size = New System.Drawing.Size(200, 20)
        Me.lblCSPEED.TabIndex = 6
        Me.lblCSPEED.Text = "Скорость бод"
        '
        'txtCSPEED
        '
        Me.txtCSPEED.Location = New System.Drawing.Point(20, 27)
        Me.txtCSPEED.Name = "txtCSPEED"
        Me.txtCSPEED.Size = New System.Drawing.Size(200, 21)
        Me.txtCSPEED.TabIndex = 7
        '
        'lblCDATABIT
        '
        Appearance2.ForeColor = System.Drawing.Color.Black
        Me.lblCDATABIT.Appearance = Appearance2
        Me.lblCDATABIT.ForeColor = System.Drawing.Color.Blue
        Me.lblCDATABIT.Location = New System.Drawing.Point(20, 52)
        Me.lblCDATABIT.Name = "lblCDATABIT"
        Me.lblCDATABIT.Size = New System.Drawing.Size(200, 20)
        Me.lblCDATABIT.TabIndex = 8
        Me.lblCDATABIT.Text = "Биты данных"
        '
        'txtCDATABIT
        '
        Me.txtCDATABIT.Location = New System.Drawing.Point(20, 74)
        Me.txtCDATABIT.Name = "txtCDATABIT"
        Me.txtCDATABIT.Size = New System.Drawing.Size(200, 21)
        Me.txtCDATABIT.TabIndex = 9
        '
        'lblCPARITY
        '
        Appearance3.ForeColor = System.Drawing.Color.Black
        Me.lblCPARITY.Appearance = Appearance3
        Me.lblCPARITY.ForeColor = System.Drawing.Color.Blue
        Me.lblCPARITY.Location = New System.Drawing.Point(20, 99)
        Me.lblCPARITY.Name = "lblCPARITY"
        Me.lblCPARITY.Size = New System.Drawing.Size(200, 20)
        Me.lblCPARITY.TabIndex = 10
        Me.lblCPARITY.Text = "Четность"
        '
        'lblCSTOPBITS
        '
        Appearance4.ForeColor = System.Drawing.Color.Black
        Me.lblCSTOPBITS.Appearance = Appearance4
        Me.lblCSTOPBITS.ForeColor = System.Drawing.Color.Blue
        Me.lblCSTOPBITS.Location = New System.Drawing.Point(20, 146)
        Me.lblCSTOPBITS.Name = "lblCSTOPBITS"
        Me.lblCSTOPBITS.Size = New System.Drawing.Size(200, 20)
        Me.lblCSTOPBITS.TabIndex = 12
        Me.lblCSTOPBITS.Text = "Стоповые биты"
        '
        'txtCSTOPBITS
        '
        Me.txtCSTOPBITS.Location = New System.Drawing.Point(20, 168)
        Me.txtCSTOPBITS.MaxLength = 15
        Me.txtCSTOPBITS.Name = "txtCSTOPBITS"
        Me.txtCSTOPBITS.Size = New System.Drawing.Size(200, 21)
        Me.txtCSTOPBITS.TabIndex = 13
        '
        'lblCTOWNCODE
        '
        Appearance7.ForeColor = System.Drawing.Color.Black
        Me.lblCTOWNCODE.Appearance = Appearance7
        Me.lblCTOWNCODE.ForeColor = System.Drawing.Color.Blue
        Me.lblCTOWNCODE.Location = New System.Drawing.Point(234, 5)
        Me.lblCTOWNCODE.Name = "lblCTOWNCODE"
        Me.lblCTOWNCODE.Size = New System.Drawing.Size(200, 20)
        Me.lblCTOWNCODE.TabIndex = 32
        Me.lblCTOWNCODE.Text = "Код города"
        '
        'txtCTOWNCODE
        '
        Me.txtCTOWNCODE.Location = New System.Drawing.Point(234, 27)
        Me.txtCTOWNCODE.Name = "txtCTOWNCODE"
        Me.txtCTOWNCODE.Size = New System.Drawing.Size(200, 21)
        Me.txtCTOWNCODE.TabIndex = 33
        '
        'lblCPHONE
        '
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.lblCPHONE.Appearance = Appearance6
        Me.lblCPHONE.ForeColor = System.Drawing.Color.Blue
        Me.lblCPHONE.Location = New System.Drawing.Point(234, 52)
        Me.lblCPHONE.Name = "lblCPHONE"
        Me.lblCPHONE.Size = New System.Drawing.Size(200, 20)
        Me.lblCPHONE.TabIndex = 34
        Me.lblCPHONE.Text = "Телефон"
        '
        'txtCPHONE
        '
        Me.txtCPHONE.Location = New System.Drawing.Point(234, 74)
        Me.txtCPHONE.Name = "txtCPHONE"
        Me.txtCPHONE.Size = New System.Drawing.Size(200, 21)
        Me.txtCPHONE.TabIndex = 35
        '
        'lblCPREFPHONE
        '
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.lblCPREFPHONE.Appearance = Appearance5
        Me.lblCPREFPHONE.ForeColor = System.Drawing.Color.Blue
        Me.lblCPREFPHONE.Location = New System.Drawing.Point(234, 99)
        Me.lblCPREFPHONE.Name = "lblCPREFPHONE"
        Me.lblCPREFPHONE.Size = New System.Drawing.Size(200, 20)
        Me.lblCPREFPHONE.TabIndex = 36
        Me.lblCPREFPHONE.Text = "Префикс местной АТС"
        '
        'txtCPREFPHONE
        '
        Me.txtCPREFPHONE.Location = New System.Drawing.Point(234, 121)
        Me.txtCPREFPHONE.Name = "txtCPREFPHONE"
        Me.txtCPREFPHONE.Size = New System.Drawing.Size(200, 21)
        Me.txtCPREFPHONE.TabIndex = 37
        '
        'txtCPARITY
        '
        Me.txtCPARITY.Location = New System.Drawing.Point(20, 117)
        Me.txtCPARITY.Name = "txtCPARITY"
        Me.txtCPARITY.Size = New System.Drawing.Size(200, 20)
        Me.txtCPARITY.TabIndex = 38
        '
        'editTPLT_CONNECT
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.txtCPARITY)
        Me.Controls.Add(Me.lblCSPEED)
        Me.Controls.Add(Me.txtCSPEED)
        Me.Controls.Add(Me.lblCDATABIT)
        Me.Controls.Add(Me.txtCDATABIT)
        Me.Controls.Add(Me.lblCPARITY)
        Me.Controls.Add(Me.lblCSTOPBITS)
        Me.Controls.Add(Me.txtCSTOPBITS)
        Me.Controls.Add(Me.lblCTOWNCODE)
        Me.Controls.Add(Me.txtCTOWNCODE)
        Me.Controls.Add(Me.lblCPHONE)
        Me.Controls.Add(Me.txtCPHONE)
        Me.Controls.Add(Me.lblCPREFPHONE)
        Me.Controls.Add(Me.txtCPREFPHONE)
        Me.Name = "editTPLT_CONNECT"
        Me.Size = New System.Drawing.Size(473, 231)
        CType(Me.txtCSPEED, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCDATABIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCSTOPBITS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCTOWNCODE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPHONE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPREFPHONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private Sub cmbConnectionEnabled_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub txtConnectType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub

    Private Sub txtCSPEED_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCSPEED.TextChanged
        Changing()

    End Sub
    Private Sub txtCDATABIT_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCDATABIT.TextChanged
        Changing()

    End Sub
    Private Sub cmbCPARITY_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Changing()

    End Sub
    Private Sub txtCSTOPBITS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCSTOPBITS.Validating
        If txtCSTOPBITS.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(txtCSTOPBITS.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(txtCSTOPBITS.Text) < -2000000000 Or Val(txtCSTOPBITS.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub
    Private Sub txtCSTOPBITS_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCSTOPBITS.TextChanged
        Changing()

    End Sub

    Private Sub txtComPortNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Changing()

    End Sub

    Private Sub txtCPHONE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPHONE.TextChanged
        Changing()

    End Sub
    Private Sub txtCPREFPHONE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPREFPHONE.TextChanged
        Changing()

    End Sub

    'Private Sub txtATCommand_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Changing()

    'End Sub


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
        If Item Is Nothing Then Exit Sub
        mOnInit = True


        txtCSPEED.Text = Item("CSPEED").ToString()
        txtCDATABIT.Text = Item("CDATABIT").ToString()
        txtCPARITY.Text = Item("CPARITY").ToString
        txtCSTOPBITS.Text = Item("CSTOPBITS").ToString()
        cmbCDTRDATA = New DataTable
        cmbCDTRDATA.Columns.Add("name", GetType(System.String))
        cmbCDTRDATA.Columns.Add("Value", GetType(System.Int32))

        'txtFlowControl.Text = Item("FlowControl").ToString()
        'txtComPortNum.Text = Item("ComPortNum").ToString()
        txtCPHONE.Text = Item("CPHONE").ToString()
        txtCPREFPHONE.Text = Item("CPREFPHONE").ToString()
        txtCTOWNCODE.Text = Item("CTOWNCODE").ToString()

        mOnInit = False
        RaiseEvent Refreshed()
    End Sub


    ''' <summary>
    '''Сохранения данных в полях объекта
    ''' </summary>
    ''' <remarks>
    '''
    ''' </remarks>
    Public Sub Save()
        If mRowReadOnly = False Then


            Item("CSPEED") = Val(txtCSPEED.Text)
            Item("CDATABIT") = Val(txtCDATABIT.Text)
            Item("CPARITY") = txtCPARITY.Text
            Item("CSTOPBITS") = Val(txtCSTOPBITS.Text)
         
            Item("CTOWNCODE") = txtCTOWNCODE.Text
            Item("CPHONE") = txtCPHONE.Text
            Item("CPREFPHONE") = txtCPREFPHONE.Text
            'Item("CONNECTLIMIT") = CDbl(txtCONNECTLIMIT.Text)
            'Item("ATCommand") = txtATCommand.Text
          
        End If
        mChanged = False
        RaiseEvent Saved()
    End Sub
    Public Function IsOK() As Boolean
        Dim mIsOK As Boolean
        mIsOK = True
        If mRowReadOnly Then Return True
        Return mIsOK
    End Function
    Public Function IsChanged() As Boolean
        Return mChanged
    End Function

end class

