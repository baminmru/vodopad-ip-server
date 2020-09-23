
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
    Friend WithEvents cmbTransport As System.Windows.Forms.ComboBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxIP As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCALLERID As System.Windows.Forms.TextBox
    Private mChanged As Boolean = False
    Public Event Changed()
    Public Event Saved()
    Public Event Refreshed()
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
    Friend cmbConnectionEnabledDATA As DataTable
    Friend cmbConnectionEnabledDATAROW As DataRow
    Friend WithEvents lblCSPEED As System.Windows.Forms.Label
    Friend WithEvents txtCSPEED As System.Windows.Forms.TextBox
    Friend WithEvents lblCDATABIT As System.Windows.Forms.Label
    Friend WithEvents txtCDATABIT As System.Windows.Forms.TextBox
    Friend WithEvents lblCPARITY As System.Windows.Forms.Label
    Friend cmbCPARITYDATA As DataTable
    Friend cmbCPARITYDATAROW As DataRow
    Friend WithEvents lblCSTOPBITS As System.Windows.Forms.Label
    Friend WithEvents txtCSTOPBITS As System.Windows.Forms.TextBox
    Friend cmbCDTRDATA As DataTable
    Friend cmbCDTRDATAROW As DataRow
    Friend cmbCDSRDATA As DataTable
    Friend cmbCDSRDATAROW As DataRow
    Friend WithEvents lblCTOWNCODE As System.Windows.Forms.Label
    Friend WithEvents txtCTOWNCODE As System.Windows.Forms.TextBox
    Friend WithEvents lblCPHONE As System.Windows.Forms.Label
    Friend WithEvents txtCPHONE As System.Windows.Forms.TextBox
    Friend WithEvents lblCPREFPHONE As System.Windows.Forms.Label
    Friend WithEvents txtCPREFPHONE As System.Windows.Forms.TextBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblCSPEED = New System.Windows.Forms.Label()
        Me.txtCSPEED = New System.Windows.Forms.TextBox()
        Me.lblCDATABIT = New System.Windows.Forms.Label()
        Me.txtCDATABIT = New System.Windows.Forms.TextBox()
        Me.lblCPARITY = New System.Windows.Forms.Label()
        Me.lblCSTOPBITS = New System.Windows.Forms.Label()
        Me.txtCSTOPBITS = New System.Windows.Forms.TextBox()
        Me.lblCTOWNCODE = New System.Windows.Forms.Label()
        Me.txtCTOWNCODE = New System.Windows.Forms.TextBox()
        Me.lblCPHONE = New System.Windows.Forms.Label()
        Me.txtCPHONE = New System.Windows.Forms.TextBox()
        Me.lblCPREFPHONE = New System.Windows.Forms.Label()
        Me.txtCPREFPHONE = New System.Windows.Forms.TextBox()
        Me.txtCPARITY = New System.Windows.Forms.TextBox()
        Me.cmbTransport = New System.Windows.Forms.ComboBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.TextBoxIP = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCALLERID = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblCSPEED
        '
        Me.lblCSPEED.ForeColor = System.Drawing.Color.Blue
        Me.lblCSPEED.Location = New System.Drawing.Point(14, 12)
        Me.lblCSPEED.Name = "lblCSPEED"
        Me.lblCSPEED.Size = New System.Drawing.Size(200, 20)
        Me.lblCSPEED.TabIndex = 6
        Me.lblCSPEED.Text = "Скорость бод"
        '
        'txtCSPEED
        '
        Me.txtCSPEED.Location = New System.Drawing.Point(14, 34)
        Me.txtCSPEED.Name = "txtCSPEED"
        Me.txtCSPEED.Size = New System.Drawing.Size(200, 20)
        Me.txtCSPEED.TabIndex = 7
        '
        'lblCDATABIT
        '
        Me.lblCDATABIT.ForeColor = System.Drawing.Color.Blue
        Me.lblCDATABIT.Location = New System.Drawing.Point(14, 59)
        Me.lblCDATABIT.Name = "lblCDATABIT"
        Me.lblCDATABIT.Size = New System.Drawing.Size(200, 20)
        Me.lblCDATABIT.TabIndex = 8
        Me.lblCDATABIT.Text = "Биты данных"
        '
        'txtCDATABIT
        '
        Me.txtCDATABIT.Location = New System.Drawing.Point(14, 81)
        Me.txtCDATABIT.Name = "txtCDATABIT"
        Me.txtCDATABIT.Size = New System.Drawing.Size(200, 20)
        Me.txtCDATABIT.TabIndex = 9
        '
        'lblCPARITY
        '
        Me.lblCPARITY.ForeColor = System.Drawing.Color.Blue
        Me.lblCPARITY.Location = New System.Drawing.Point(14, 106)
        Me.lblCPARITY.Name = "lblCPARITY"
        Me.lblCPARITY.Size = New System.Drawing.Size(200, 20)
        Me.lblCPARITY.TabIndex = 10
        Me.lblCPARITY.Text = "Четность"
        '
        'lblCSTOPBITS
        '
        Me.lblCSTOPBITS.ForeColor = System.Drawing.Color.Blue
        Me.lblCSTOPBITS.Location = New System.Drawing.Point(14, 153)
        Me.lblCSTOPBITS.Name = "lblCSTOPBITS"
        Me.lblCSTOPBITS.Size = New System.Drawing.Size(200, 20)
        Me.lblCSTOPBITS.TabIndex = 12
        Me.lblCSTOPBITS.Text = "Стоповые биты"
        '
        'txtCSTOPBITS
        '
        Me.txtCSTOPBITS.Location = New System.Drawing.Point(14, 175)
        Me.txtCSTOPBITS.MaxLength = 15
        Me.txtCSTOPBITS.Name = "txtCSTOPBITS"
        Me.txtCSTOPBITS.Size = New System.Drawing.Size(200, 20)
        Me.txtCSTOPBITS.TabIndex = 13
        '
        'lblCTOWNCODE
        '
        Me.lblCTOWNCODE.ForeColor = System.Drawing.Color.Blue
        Me.lblCTOWNCODE.Location = New System.Drawing.Point(14, 208)
        Me.lblCTOWNCODE.Name = "lblCTOWNCODE"
        Me.lblCTOWNCODE.Size = New System.Drawing.Size(200, 20)
        Me.lblCTOWNCODE.TabIndex = 32
        Me.lblCTOWNCODE.Text = "Код города"
        '
        'txtCTOWNCODE
        '
        Me.txtCTOWNCODE.Location = New System.Drawing.Point(14, 230)
        Me.txtCTOWNCODE.Name = "txtCTOWNCODE"
        Me.txtCTOWNCODE.Size = New System.Drawing.Size(200, 20)
        Me.txtCTOWNCODE.TabIndex = 33
        '
        'lblCPHONE
        '
        Me.lblCPHONE.ForeColor = System.Drawing.Color.Blue
        Me.lblCPHONE.Location = New System.Drawing.Point(14, 255)
        Me.lblCPHONE.Name = "lblCPHONE"
        Me.lblCPHONE.Size = New System.Drawing.Size(200, 20)
        Me.lblCPHONE.TabIndex = 34
        Me.lblCPHONE.Text = "Телефон"
        '
        'txtCPHONE
        '
        Me.txtCPHONE.Location = New System.Drawing.Point(14, 277)
        Me.txtCPHONE.Name = "txtCPHONE"
        Me.txtCPHONE.Size = New System.Drawing.Size(200, 20)
        Me.txtCPHONE.TabIndex = 35
        '
        'lblCPREFPHONE
        '
        Me.lblCPREFPHONE.ForeColor = System.Drawing.Color.Blue
        Me.lblCPREFPHONE.Location = New System.Drawing.Point(14, 302)
        Me.lblCPREFPHONE.Name = "lblCPREFPHONE"
        Me.lblCPREFPHONE.Size = New System.Drawing.Size(200, 20)
        Me.lblCPREFPHONE.TabIndex = 36
        Me.lblCPREFPHONE.Text = "Префикс местной АТС"
        '
        'txtCPREFPHONE
        '
        Me.txtCPREFPHONE.Location = New System.Drawing.Point(14, 324)
        Me.txtCPREFPHONE.Name = "txtCPREFPHONE"
        Me.txtCPREFPHONE.Size = New System.Drawing.Size(200, 20)
        Me.txtCPREFPHONE.TabIndex = 37
        '
        'txtCPARITY
        '
        Me.txtCPARITY.Location = New System.Drawing.Point(14, 124)
        Me.txtCPARITY.Name = "txtCPARITY"
        Me.txtCPARITY.Size = New System.Drawing.Size(200, 20)
        Me.txtCPARITY.TabIndex = 38
        '
        'cmbTransport
        '
        Me.cmbTransport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTransport.FormattingEnabled = True
        Me.cmbTransport.Items.AddRange(New Object() {"MODEM", "COM", "NPORT", "VSX", "GSM Modem", "АССВ", "ROBUSTEL", "ATM", "DUMMY", "SER2NET"})
        Me.cmbTransport.Location = New System.Drawing.Point(234, 35)
        Me.cmbTransport.Name = "cmbTransport"
        Me.cmbTransport.Size = New System.Drawing.Size(168, 21)
        Me.cmbTransport.TabIndex = 48
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(234, 124)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(163, 20)
        Me.txtPort.TabIndex = 46
        '
        'TextBoxIP
        '
        Me.TextBoxIP.Location = New System.Drawing.Point(234, 81)
        Me.TextBoxIP.Name = "TextBoxIP"
        Me.TextBoxIP.Size = New System.Drawing.Size(163, 20)
        Me.TextBoxIP.TabIndex = 44
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(234, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(126, 13)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "IP-Порт \ Индекс порта"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(234, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "Транспорт"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(234, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "IP-адрес"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(234, 153)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Код прибора"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(232, 175)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(164, 20)
        Me.txtCode.TabIndex = 50
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(234, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(224, 13)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Идентификатор вызывающего устройства"
        '
        'txtCALLERID
        '
        Me.txtCALLERID.Location = New System.Drawing.Point(231, 227)
        Me.txtCALLERID.Name = "txtCALLERID"
        Me.txtCALLERID.Size = New System.Drawing.Size(164, 20)
        Me.txtCALLERID.TabIndex = 52
        '
        'editTPLT_CONNECT
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.txtCALLERID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbTransport)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.TextBoxIP)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label2)
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
        Me.Size = New System.Drawing.Size(526, 367)
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
        txtCALLERID.Text = Item("CALLERID").ToString()

        TextBoxIP.Text = "" & Item("npip")
        txtCode.Text = "" & Item("nppassword")

        If Item("transport") = 0 Then
            cmbTransport.Text = "MODEM"
        End If

        If Item("transport") = 1 Then
            cmbTransport.Text = "COM"
        End If

        If Item("transport") = 2 Then
            cmbTransport.Text = "NPORT"
        End If

        If Item("transport") = 3 Then
            cmbTransport.Text = "VSX"
        End If

        If Item("transport") = 4 Then
            cmbTransport.Text = "GSM Modem"
        End If

        If Item("transport") = 5 Then
            cmbTransport.Text = "АССВ"
        End If

        If Item("transport") = 6 Then
            cmbTransport.Text = "ROBUSTEL"
        End If
        If Item("transport") = 7 Then
            cmbTransport.Text = "ATM"
        End If

        If Item("transport") = 9 Then
            cmbTransport.Text = "DUMMY"
        End If

        If Item("transport") = 10 Then
            cmbTransport.Text = "SER2NET"
        End If

        txtPort.Text = "" & Item("ipport").ToString
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
            Item("CALLERID") = txtCALLERID.Text
            Item("CPHONE") = txtCPHONE.Text
            Item("CPREFPHONE") = txtCPREFPHONE.Text
            Item("npip") = TextBoxIP.Text
            Item("nppassword") = txtCode.Text

            If cmbTransport.Text = "MODEM" Then
                Item("transport") = 0
            End If

            If cmbTransport.Text = "COM" Then

                Item("transport") = 1
            End If

            If cmbTransport.Text = "NPORT" Then

                Item("transport") = 2
            End If

            If cmbTransport.Text = "VSX" Then

                Item("transport") = 3
            End If

            If cmbTransport.Text = "GSM Modem" Then

                Item("transport") = 4
            End If

            If cmbTransport.Text = "АССВ" Then

                Item("transport") = 5
            End If

            If cmbTransport.Text = "ROBUSTEL" Then

                Item("transport") = 6
            End If

            If cmbTransport.Text = "ATM" Then

                Item("transport") = 7
            End If

            If cmbTransport.Text = "DUMMY" Then

                Item("transport") = 9
            End If

            If cmbTransport.Text = "SER2NET" Then

                Item("transport") = 10
            End If

            Try
                Item("ipport") = Integer.Parse(txtPort.Text)
            Catch ex As Exception
                Item("ipport") = 0
            End Try


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

End Class

