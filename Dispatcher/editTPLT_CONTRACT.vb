
Imports System.Windows.Forms
Imports Microsoft.VisualBasic



''' <summary>
'''Контрол редактирования раздела Договорные установки режим:
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Class editTPLT_CONTRACT
    Inherits System.Windows.Forms.UserControl
    Implements LATIRGuiControls.IRowEditor

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    private mOnInit as boolean = false
    private mChanged as boolean = false
    public event Changed() Implements LATIRGuiControls.IRowEditor.Changed 
    Public Event Saved() Implements LATIRGUIControls.IRowEditor.Saved
    Public Event Refreshed() Implements LATIRGUIControls.IRowEditor.Refreshed
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
    Friend WithEvents HolderPanel As LATIRGUIControls.AutoPanel
Friend WithEvents lblFLD12  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD12 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD13  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD13 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD14  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD14 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD15  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD15 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD16  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD16 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD17  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD17 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD18  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD18 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD19  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD19 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD20  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD20 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD21  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD21 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD22  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD22 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD23  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD23 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD24  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD24 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD25  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD25 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD26  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD26 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD27  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD27 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD28  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD28 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD29  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD29 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD30  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD30 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD31  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD31 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD32  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD32 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD33  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD33 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD34  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD34 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD35  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD35 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD36  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD36 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD37  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD37 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD40  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD40 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD41  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD41 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD42  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD42 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD43  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD43 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD45  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD45 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD46  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD46 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD47  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD47 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD48  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD48 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD49  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD49 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD50  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD50 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD51  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD51 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD52  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD52 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD53  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD53 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD54  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD54 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD55  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD55 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD56  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD56 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD57  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD57 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD58  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD58 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD59  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD59 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD60  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD60 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD61  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD61 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD62  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD62 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD63  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD63 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD64  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD64 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD65  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD65 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD66  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD66 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD67  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD67 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD68  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD68 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD69  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD69 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD70  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD70 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD71  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD71 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD72  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD72 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD73  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD73 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD81  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD81 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD82  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD82 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD83  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD83 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD84  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD84 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD85  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD85 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD86  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD86 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD87  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD87 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD88  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD88 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD89  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD89 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD90  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD90 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD92  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD92 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD93  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD93 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD94  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD94 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD95  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD95 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD96  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD96 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD97  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD97 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD98  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD98 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD99  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD99 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD100  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD100 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD101  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD101 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD102  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD102 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD103  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD103 As Infragistics.Win.UltraWinEditors.UltraTextEditor
Friend WithEvents lblFLD104  as  Infragistics.Win.Misc.UltraLabel
Friend WithEvents txtFLD104 As Infragistics.Win.UltraWinEditors.UltraTextEditor

<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

 Me.HolderPanel = New LATIRGUIControls.AutoPanel
 Me.HolderPanel.SuspendLayout()
Me.SuspendLayout()
 '
'HolderPanel
'
Me.HolderPanel.AllowDrop = True
Me.HolderPanel.BackColor = System.Drawing.SystemColors.Control
Me.HolderPanel.Dock = System.Windows.Forms.DockStyle.Fill
Me.HolderPanel.Location = New System.Drawing.Point(0, 0)
Me.HolderPanel.Name = "HolderPanel"
Me.HolderPanel.Size = New System.Drawing.Size(232, 120)
Me.HolderPanel.TabIndex = 0
Me.lblFLD12 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD12 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD13 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD13 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD14 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD14 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD15 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD15 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD16 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD16 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD17 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD17 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD18 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD18 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD19 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD19 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD20 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD20 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD21 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD21 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD22 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD22 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD23 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD23 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD24 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD24 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD25 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD25 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD26 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD26 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD27 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD27 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD28 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD28 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD29 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD29 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD30 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD30 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD31 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD31 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD32 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD32 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD33 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD33 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD34 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD34 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD35 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD35 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD36 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD36 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD37 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD37 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD40 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD40 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD41 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD41 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD42 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD42 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD43 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD43 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD45 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD45 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD46 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD46 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD47 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD47 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD48 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD48 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD49 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD49 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD50 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD50 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD51 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD51 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD52 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD52 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD53 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD53 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD54 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD54 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD55 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD55 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD56 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD56 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD57 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD57 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD58 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD58 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD59 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD59 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD60 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD60 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD61 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD61 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD62 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD62 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD63 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD63 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD64 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD64 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD65 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD65 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD66 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD66 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD67 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD67 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD68 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD68 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD69 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD69 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD70 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD70 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD71 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD71 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD72 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD72 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD73 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD73 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD81 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD81 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD82 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD82 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD83 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD83 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD84 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD84 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD85 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD85 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD86 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD86 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD87 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD87 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD88 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD88 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD89 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD89 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD90 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD90 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD92 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD92 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD93 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD93 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD94 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD94 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD95 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD95 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD96 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD96 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD97 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD97 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD98 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD98 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD99 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD99 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD100 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD100 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD101 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD101 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD102 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD102 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD103 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD103 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
Me.lblFLD104 = New Infragistics.Win.Misc.UltraLabel
Me.txtFLD104 = New Infragistics.Win.UltraWinEditors.UltraTextEditor

Me.lblFLD12.Location = New System.Drawing.Point(20,5)
Me.lblFLD12.name = "lblFLD12"
Me.lblFLD12.Size = New System.Drawing.Size(200, 20)
Me.lblFLD12.TabIndex = 1
Me.lblFLD12.Text = "№ прибора"
Me.lblFLD12.ForeColor = System.Drawing.Color.Blue
Me.txtFLD12.Location = New System.Drawing.Point(20,27)
Me.txtFLD12.name = "txtFLD12"
Me.txtFLD12.Size = New System.Drawing.Size(200, 20)
Me.txtFLD12.TabIndex = 2
Me.txtFLD12.Text = "" 
Me.lblFLD13.Location = New System.Drawing.Point(20,52)
Me.lblFLD13.name = "lblFLD13"
Me.lblFLD13.Size = New System.Drawing.Size(200, 20)
Me.lblFLD13.TabIndex = 3
Me.lblFLD13.Text = "№ключа"
Me.lblFLD13.ForeColor = System.Drawing.Color.Blue
Me.txtFLD13.Location = New System.Drawing.Point(20,74)
Me.txtFLD13.name = "txtFLD13"
Me.txtFLD13.Size = New System.Drawing.Size(200, 20)
Me.txtFLD13.TabIndex = 4
Me.txtFLD13.Text = "" 
Me.lblFLD14.Location = New System.Drawing.Point(20,99)
Me.lblFLD14.name = "lblFLD14"
Me.lblFLD14.Size = New System.Drawing.Size(200, 20)
Me.lblFLD14.TabIndex = 5
Me.lblFLD14.Text = "D20ОБ"
Me.lblFLD14.ForeColor = System.Drawing.Color.Blue
Me.txtFLD14.Location = New System.Drawing.Point(20,121)
Me.txtFLD14.name = "txtFLD14"
Me.txtFLD14.Size = New System.Drawing.Size(200, 20)
Me.txtFLD14.TabIndex = 6
Me.txtFLD14.Text = "" 
Me.lblFLD15.Location = New System.Drawing.Point(20,146)
Me.lblFLD15.name = "lblFLD15"
Me.lblFLD15.Size = New System.Drawing.Size(200, 20)
Me.lblFLD15.TabIndex = 7
Me.lblFLD15.Text = "D20ПР"
Me.lblFLD15.ForeColor = System.Drawing.Color.Blue
Me.txtFLD15.Location = New System.Drawing.Point(20,168)
Me.txtFLD15.name = "txtFLD15"
Me.txtFLD15.Size = New System.Drawing.Size(200, 20)
Me.txtFLD15.TabIndex = 8
Me.txtFLD15.Text = "" 
Me.lblFLD16.Location = New System.Drawing.Point(20,193)
Me.lblFLD16.name = "lblFLD16"
Me.lblFLD16.Size = New System.Drawing.Size(200, 20)
Me.lblFLD16.TabIndex = 9
Me.lblFLD16.Text = "DyГВС"
Me.lblFLD16.ForeColor = System.Drawing.Color.Blue
Me.txtFLD16.Location = New System.Drawing.Point(20,215)
Me.txtFLD16.name = "txtFLD16"
Me.txtFLD16.Size = New System.Drawing.Size(200, 20)
Me.txtFLD16.TabIndex = 10
Me.txtFLD16.Text = "" 
Me.lblFLD17.Location = New System.Drawing.Point(20,240)
Me.lblFLD17.name = "lblFLD17"
Me.lblFLD17.Size = New System.Drawing.Size(200, 20)
Me.lblFLD17.TabIndex = 11
Me.lblFLD17.Text = "DyОБР"
Me.lblFLD17.ForeColor = System.Drawing.Color.Blue
Me.txtFLD17.Location = New System.Drawing.Point(20,262)
Me.txtFLD17.name = "txtFLD17"
Me.txtFLD17.Size = New System.Drawing.Size(200, 20)
Me.txtFLD17.TabIndex = 12
Me.txtFLD17.Text = "" 
Me.lblFLD18.Location = New System.Drawing.Point(20,287)
Me.lblFLD18.name = "lblFLD18"
Me.lblFLD18.Size = New System.Drawing.Size(200, 20)
Me.lblFLD18.TabIndex = 13
Me.lblFLD18.Text = "DyПР"
Me.lblFLD18.ForeColor = System.Drawing.Color.Blue
Me.txtFLD18.Location = New System.Drawing.Point(20,309)
Me.txtFLD18.name = "txtFLD18"
Me.txtFLD18.Size = New System.Drawing.Size(200, 20)
Me.txtFLD18.TabIndex = 14
Me.txtFLD18.Text = "" 
Me.lblFLD19.Location = New System.Drawing.Point(20,334)
Me.lblFLD19.name = "lblFLD19"
Me.lblFLD19.Size = New System.Drawing.Size(200, 20)
Me.lblFLD19.TabIndex = 15
Me.lblFLD19.Text = "dРпрОБ"
Me.lblFLD19.ForeColor = System.Drawing.Color.Blue
Me.txtFLD19.Location = New System.Drawing.Point(20,356)
Me.txtFLD19.name = "txtFLD19"
Me.txtFLD19.Size = New System.Drawing.Size(200, 20)
Me.txtFLD19.TabIndex = 16
Me.txtFLD19.Text = "" 
Me.lblFLD20.Location = New System.Drawing.Point(20,381)
Me.lblFLD20.name = "lblFLD20"
Me.lblFLD20.Size = New System.Drawing.Size(200, 20)
Me.lblFLD20.TabIndex = 17
Me.lblFLD20.Text = "dРпрПР"
Me.lblFLD20.ForeColor = System.Drawing.Color.Blue
Me.txtFLD20.Location = New System.Drawing.Point(20,403)
Me.txtFLD20.name = "txtFLD20"
Me.txtFLD20.Size = New System.Drawing.Size(200, 20)
Me.txtFLD20.TabIndex = 18
Me.txtFLD20.Text = "" 
Me.lblFLD21.Location = New System.Drawing.Point(230,5)
Me.lblFLD21.name = "lblFLD21"
Me.lblFLD21.Size = New System.Drawing.Size(200, 20)
Me.lblFLD21.TabIndex = 19
Me.lblFLD21.Text = "G(гвс)ПР"
Me.lblFLD21.ForeColor = System.Drawing.Color.Blue
Me.txtFLD21.Location = New System.Drawing.Point(230,27)
Me.txtFLD21.name = "txtFLD21"
Me.txtFLD21.Size = New System.Drawing.Size(200, 20)
Me.txtFLD21.TabIndex = 20
Me.txtFLD21.Text = "" 
Me.lblFLD22.Location = New System.Drawing.Point(230,52)
Me.lblFLD22.name = "lblFLD22"
Me.lblFLD22.Size = New System.Drawing.Size(200, 20)
Me.lblFLD22.TabIndex = 21
Me.lblFLD22.Text = "Gгвс"
Me.lblFLD22.ForeColor = System.Drawing.Color.Blue
Me.txtFLD22.Location = New System.Drawing.Point(230,74)
Me.txtFLD22.name = "txtFLD22"
Me.txtFLD22.Size = New System.Drawing.Size(200, 20)
Me.txtFLD22.TabIndex = 22
Me.txtFLD22.Text = "" 
Me.lblFLD23.Location = New System.Drawing.Point(230,99)
Me.lblFLD23.name = "lblFLD23"
Me.lblFLD23.Size = New System.Drawing.Size(200, 20)
Me.lblFLD23.TabIndex = 23
Me.lblFLD23.Text = "Gоб(гвс min)"
Me.lblFLD23.ForeColor = System.Drawing.Color.Blue
Me.txtFLD23.Location = New System.Drawing.Point(230,121)
Me.txtFLD23.name = "txtFLD23"
Me.txtFLD23.Size = New System.Drawing.Size(200, 20)
Me.txtFLD23.TabIndex = 24
Me.txtFLD23.Text = "" 
Me.lblFLD24.Location = New System.Drawing.Point(230,146)
Me.lblFLD24.name = "lblFLD24"
Me.lblFLD24.Size = New System.Drawing.Size(200, 20)
Me.lblFLD24.TabIndex = 25
Me.lblFLD24.Text = "Gов"
Me.lblFLD24.ForeColor = System.Drawing.Color.Blue
Me.txtFLD24.Location = New System.Drawing.Point(230,168)
Me.txtFLD24.name = "txtFLD24"
Me.txtFLD24.Size = New System.Drawing.Size(200, 20)
Me.txtFLD24.TabIndex = 26
Me.txtFLD24.Text = "" 
Me.lblFLD25.Location = New System.Drawing.Point(230,193)
Me.lblFLD25.name = "lblFLD25"
Me.lblFLD25.Size = New System.Drawing.Size(200, 20)
Me.lblFLD25.TabIndex = 27
Me.lblFLD25.Text = "Gпр(гвс min)"
Me.lblFLD25.ForeColor = System.Drawing.Color.Blue
Me.txtFLD25.Location = New System.Drawing.Point(230,215)
Me.txtFLD25.name = "txtFLD25"
Me.txtFLD25.Size = New System.Drawing.Size(200, 20)
Me.txtFLD25.TabIndex = 28
Me.txtFLD25.Text = "" 
Me.lblFLD26.Location = New System.Drawing.Point(230,240)
Me.lblFLD26.name = "lblFLD26"
Me.lblFLD26.Size = New System.Drawing.Size(200, 20)
Me.lblFLD26.TabIndex = 29
Me.lblFLD26.Text = "Gпр_minОБ"
Me.lblFLD26.ForeColor = System.Drawing.Color.Blue
Me.txtFLD26.Location = New System.Drawing.Point(230,262)
Me.txtFLD26.name = "txtFLD26"
Me.txtFLD26.Size = New System.Drawing.Size(200, 20)
Me.txtFLD26.TabIndex = 30
Me.txtFLD26.Text = "" 
Me.lblFLD27.Location = New System.Drawing.Point(230,287)
Me.lblFLD27.name = "lblFLD27"
Me.lblFLD27.Size = New System.Drawing.Size(200, 20)
Me.lblFLD27.TabIndex = 31
Me.lblFLD27.Text = "Gпр_minПР"
Me.lblFLD27.ForeColor = System.Drawing.Color.Blue
Me.txtFLD27.Location = New System.Drawing.Point(230,309)
Me.txtFLD27.name = "txtFLD27"
Me.txtFLD27.Size = New System.Drawing.Size(200, 20)
Me.txtFLD27.TabIndex = 32
Me.txtFLD27.Text = "" 
Me.lblFLD28.Location = New System.Drawing.Point(230,334)
Me.lblFLD28.name = "lblFLD28"
Me.lblFLD28.Size = New System.Drawing.Size(200, 20)
Me.lblFLD28.TabIndex = 33
Me.lblFLD28.Text = "GпрОБ"
Me.lblFLD28.ForeColor = System.Drawing.Color.Blue
Me.txtFLD28.Location = New System.Drawing.Point(230,356)
Me.txtFLD28.name = "txtFLD28"
Me.txtFLD28.Size = New System.Drawing.Size(200, 20)
Me.txtFLD28.TabIndex = 34
Me.txtFLD28.Text = "" 
Me.lblFLD29.Location = New System.Drawing.Point(230,381)
Me.lblFLD29.name = "lblFLD29"
Me.lblFLD29.Size = New System.Drawing.Size(200, 20)
Me.lblFLD29.TabIndex = 35
Me.lblFLD29.Text = "GпрПР"
Me.lblFLD29.ForeColor = System.Drawing.Color.Blue
Me.txtFLD29.Location = New System.Drawing.Point(230,403)
Me.txtFLD29.name = "txtFLD29"
Me.txtFLD29.Size = New System.Drawing.Size(200, 20)
Me.txtFLD29.TabIndex = 36
Me.txtFLD29.Text = "" 
Me.lblFLD30.Location = New System.Drawing.Point(440,5)
Me.lblFLD30.name = "lblFLD30"
Me.lblFLD30.Size = New System.Drawing.Size(200, 20)
Me.lblFLD30.TabIndex = 37
Me.lblFLD30.Text = "Gут"
Me.lblFLD30.ForeColor = System.Drawing.Color.Blue
Me.txtFLD30.Location = New System.Drawing.Point(440,27)
Me.txtFLD30.name = "txtFLD30"
Me.txtFLD30.Size = New System.Drawing.Size(200, 20)
Me.txtFLD30.TabIndex = 38
Me.txtFLD30.Text = "" 
Me.lblFLD31.Location = New System.Drawing.Point(440,52)
Me.lblFLD31.name = "lblFLD31"
Me.lblFLD31.Size = New System.Drawing.Size(200, 20)
Me.lblFLD31.TabIndex = 39
Me.lblFLD31.Text = "д20ОБ"
Me.lblFLD31.ForeColor = System.Drawing.Color.Blue
Me.txtFLD31.Location = New System.Drawing.Point(440,74)
Me.txtFLD31.name = "txtFLD31"
Me.txtFLD31.Size = New System.Drawing.Size(200, 20)
Me.txtFLD31.TabIndex = 40
Me.txtFLD31.Text = "" 
Me.lblFLD32.Location = New System.Drawing.Point(440,99)
Me.lblFLD32.name = "lblFLD32"
Me.lblFLD32.Size = New System.Drawing.Size(200, 20)
Me.lblFLD32.TabIndex = 41
Me.lblFLD32.Text = "д20ПР"
Me.lblFLD32.ForeColor = System.Drawing.Color.Blue
Me.txtFLD32.Location = New System.Drawing.Point(440,121)
Me.txtFLD32.name = "txtFLD32"
Me.txtFLD32.Size = New System.Drawing.Size(200, 20)
Me.txtFLD32.TabIndex = 42
Me.txtFLD32.Text = "" 
Me.lblFLD33.Location = New System.Drawing.Point(440,146)
Me.lblFLD33.name = "lblFLD33"
Me.lblFLD33.Size = New System.Drawing.Size(200, 20)
Me.lblFLD33.TabIndex = 43
Me.lblFLD33.Text = "Договор"
Me.lblFLD33.ForeColor = System.Drawing.Color.Blue
Me.txtFLD33.Location = New System.Drawing.Point(440,168)
Me.txtFLD33.name = "txtFLD33"
Me.txtFLD33.Size = New System.Drawing.Size(200, 20)
Me.txtFLD33.TabIndex = 44
Me.txtFLD33.Text = "" 
Me.lblFLD34.Location = New System.Drawing.Point(440,193)
Me.lblFLD34.name = "lblFLD34"
Me.lblFLD34.Size = New System.Drawing.Size(200, 20)
Me.lblFLD34.TabIndex = 45
Me.lblFLD34.Text = "Договор G2"
Me.lblFLD34.ForeColor = System.Drawing.Color.Blue
Me.txtFLD34.Location = New System.Drawing.Point(440,215)
Me.txtFLD34.name = "txtFLD34"
Me.txtFLD34.Size = New System.Drawing.Size(200, 20)
Me.txtFLD34.TabIndex = 46
Me.txtFLD34.Text = "" 
Me.lblFLD35.Location = New System.Drawing.Point(440,240)
Me.lblFLD35.name = "lblFLD35"
Me.lblFLD35.Size = New System.Drawing.Size(200, 20)
Me.lblFLD35.TabIndex = 47
Me.lblFLD35.Text = "Договор G1"
Me.lblFLD35.ForeColor = System.Drawing.Color.Blue
Me.txtFLD35.Location = New System.Drawing.Point(440,262)
Me.txtFLD35.name = "txtFLD35"
Me.txtFLD35.Size = New System.Drawing.Size(200, 20)
Me.txtFLD35.TabIndex = 48
Me.txtFLD35.Text = "" 
Me.lblFLD36.Location = New System.Drawing.Point(440,287)
Me.lblFLD36.name = "lblFLD36"
Me.lblFLD36.Size = New System.Drawing.Size(200, 20)
Me.lblFLD36.TabIndex = 49
Me.lblFLD36.Text = "Источник"
Me.lblFLD36.ForeColor = System.Drawing.Color.Blue
Me.txtFLD36.Location = New System.Drawing.Point(440,309)
Me.txtFLD36.name = "txtFLD36"
Me.txtFLD36.Size = New System.Drawing.Size(200, 20)
Me.txtFLD36.TabIndex = 50
Me.txtFLD36.Text = "" 
Me.lblFLD37.Location = New System.Drawing.Point(440,334)
Me.lblFLD37.name = "lblFLD37"
Me.lblFLD37.Size = New System.Drawing.Size(200, 20)
Me.lblFLD37.TabIndex = 51
Me.lblFLD37.Text = "Магистраль"
Me.lblFLD37.ForeColor = System.Drawing.Color.Blue
Me.txtFLD37.Location = New System.Drawing.Point(440,356)
Me.txtFLD37.name = "txtFLD37"
Me.txtFLD37.Size = New System.Drawing.Size(200, 20)
Me.txtFLD37.TabIndex = 52
Me.txtFLD37.Text = "" 
Me.lblFLD40.Location = New System.Drawing.Point(440,381)
Me.lblFLD40.name = "lblFLD40"
Me.lblFLD40.Size = New System.Drawing.Size(200, 20)
Me.lblFLD40.TabIndex = 53
Me.lblFLD40.Text = "Расходомер"
Me.lblFLD40.ForeColor = System.Drawing.Color.Blue
Me.txtFLD40.Location = New System.Drawing.Point(440,403)
Me.txtFLD40.name = "txtFLD40"
Me.txtFLD40.Size = New System.Drawing.Size(200, 20)
Me.txtFLD40.TabIndex = 54
Me.txtFLD40.Text = "" 
Me.lblFLD41.Location = New System.Drawing.Point(650,5)
Me.lblFLD41.name = "lblFLD41"
Me.lblFLD41.Size = New System.Drawing.Size(200, 20)
Me.lblFLD41.TabIndex = 55
Me.lblFLD41.Text = "Расходомер ГВС"
Me.lblFLD41.ForeColor = System.Drawing.Color.Blue
Me.txtFLD41.Location = New System.Drawing.Point(650,27)
Me.txtFLD41.name = "txtFLD41"
Me.txtFLD41.Size = New System.Drawing.Size(200, 20)
Me.txtFLD41.TabIndex = 56
Me.txtFLD41.Text = "" 
Me.lblFLD42.Location = New System.Drawing.Point(650,52)
Me.lblFLD42.name = "lblFLD42"
Me.lblFLD42.Size = New System.Drawing.Size(200, 20)
Me.lblFLD42.TabIndex = 57
Me.lblFLD42.Text = "Робр"
Me.lblFLD42.ForeColor = System.Drawing.Color.Blue
Me.txtFLD42.Location = New System.Drawing.Point(650,74)
Me.txtFLD42.name = "txtFLD42"
Me.txtFLD42.Size = New System.Drawing.Size(200, 20)
Me.txtFLD42.TabIndex = 58
Me.txtFLD42.Text = "" 
Me.lblFLD43.Location = New System.Drawing.Point(650,99)
Me.lblFLD43.name = "lblFLD43"
Me.lblFLD43.Size = New System.Drawing.Size(200, 20)
Me.lblFLD43.TabIndex = 59
Me.lblFLD43.Text = "Рпр"
Me.lblFLD43.ForeColor = System.Drawing.Color.Blue
Me.txtFLD43.Location = New System.Drawing.Point(650,121)
Me.txtFLD43.name = "txtFLD43"
Me.txtFLD43.Size = New System.Drawing.Size(200, 20)
Me.txtFLD43.TabIndex = 60
Me.txtFLD43.Text = "" 
Me.lblFLD45.Location = New System.Drawing.Point(650,146)
Me.lblFLD45.name = "lblFLD45"
Me.lblFLD45.Size = New System.Drawing.Size(200, 20)
Me.lblFLD45.TabIndex = 61
Me.lblFLD45.Text = "Способ отбора"
Me.lblFLD45.ForeColor = System.Drawing.Color.Blue
Me.txtFLD45.Location = New System.Drawing.Point(650,168)
Me.txtFLD45.name = "txtFLD45"
Me.txtFLD45.Size = New System.Drawing.Size(200, 20)
Me.txtFLD45.TabIndex = 62
Me.txtFLD45.Text = "" 
Me.lblFLD46.Location = New System.Drawing.Point(650,193)
Me.lblFLD46.name = "lblFLD46"
Me.lblFLD46.Size = New System.Drawing.Size(200, 20)
Me.lblFLD46.TabIndex = 63
Me.lblFLD46.Text = "Т_график"
Me.lblFLD46.ForeColor = System.Drawing.Color.Blue
Me.txtFLD46.Location = New System.Drawing.Point(650,215)
Me.txtFLD46.name = "txtFLD46"
Me.txtFLD46.Size = New System.Drawing.Size(200, 20)
Me.txtFLD46.TabIndex = 64
Me.txtFLD46.Text = "" 
Me.lblFLD47.Location = New System.Drawing.Point(650,240)
Me.lblFLD47.name = "lblFLD47"
Me.lblFLD47.Size = New System.Drawing.Size(200, 20)
Me.lblFLD47.TabIndex = 65
Me.lblFLD47.Text = "Теп_камера"
Me.lblFLD47.ForeColor = System.Drawing.Color.Blue
Me.txtFLD47.Location = New System.Drawing.Point(650,262)
Me.txtFLD47.name = "txtFLD47"
Me.txtFLD47.Size = New System.Drawing.Size(200, 20)
Me.txtFLD47.TabIndex = 66
Me.txtFLD47.Text = "" 
Me.lblFLD48.Location = New System.Drawing.Point(650,287)
Me.lblFLD48.name = "lblFLD48"
Me.lblFLD48.Size = New System.Drawing.Size(200, 20)
Me.lblFLD48.TabIndex = 67
Me.lblFLD48.Text = "Тип расходомера"
Me.lblFLD48.ForeColor = System.Drawing.Color.Blue
Me.txtFLD48.Location = New System.Drawing.Point(650,309)
Me.txtFLD48.name = "txtFLD48"
Me.txtFLD48.Size = New System.Drawing.Size(200, 20)
Me.txtFLD48.TabIndex = 68
Me.txtFLD48.Text = "" 
Me.lblFLD49.Location = New System.Drawing.Point(650,334)
Me.lblFLD49.name = "lblFLD49"
Me.lblFLD49.Size = New System.Drawing.Size(200, 20)
Me.lblFLD49.TabIndex = 69
Me.lblFLD49.Text = "тип термометра"
Me.lblFLD49.ForeColor = System.Drawing.Color.Blue
Me.txtFLD49.Location = New System.Drawing.Point(650,356)
Me.txtFLD49.name = "txtFLD49"
Me.txtFLD49.Size = New System.Drawing.Size(200, 20)
Me.txtFLD49.TabIndex = 70
Me.txtFLD49.Text = "" 
Me.lblFLD50.Location = New System.Drawing.Point(650,381)
Me.lblFLD50.name = "lblFLD50"
Me.lblFLD50.Size = New System.Drawing.Size(200, 20)
Me.lblFLD50.TabIndex = 71
Me.lblFLD50.Text = "Формула"
Me.lblFLD50.ForeColor = System.Drawing.Color.Blue
Me.txtFLD50.Location = New System.Drawing.Point(650,403)
Me.txtFLD50.name = "txtFLD50"
Me.txtFLD50.Size = New System.Drawing.Size(200, 20)
Me.txtFLD50.TabIndex = 72
Me.txtFLD50.Text = "" 
Me.lblFLD51.Location = New System.Drawing.Point(860,5)
Me.lblFLD51.name = "lblFLD51"
Me.lblFLD51.Size = New System.Drawing.Size(200, 20)
Me.lblFLD51.TabIndex = 73
Me.lblFLD51.Text = "Наименование счетчика"
Me.lblFLD51.ForeColor = System.Drawing.Color.Blue
Me.txtFLD51.Location = New System.Drawing.Point(860,27)
Me.txtFLD51.name = "txtFLD51"
Me.txtFLD51.Size = New System.Drawing.Size(200, 20)
Me.txtFLD51.TabIndex = 74
Me.txtFLD51.Text = "" 
Me.lblFLD52.Location = New System.Drawing.Point(860,52)
Me.lblFLD52.name = "lblFLD52"
Me.lblFLD52.Size = New System.Drawing.Size(200, 20)
Me.lblFLD52.TabIndex = 75
Me.lblFLD52.Text = "Схема"
Me.lblFLD52.ForeColor = System.Drawing.Color.Blue
Me.txtFLD52.Location = New System.Drawing.Point(860,74)
Me.txtFLD52.name = "txtFLD52"
Me.txtFLD52.Size = New System.Drawing.Size(200, 20)
Me.txtFLD52.TabIndex = 76
Me.txtFLD52.Text = "" 
Me.lblFLD53.Location = New System.Drawing.Point(860,99)
Me.lblFLD53.name = "lblFLD53"
Me.lblFLD53.Size = New System.Drawing.Size(200, 20)
Me.lblFLD53.TabIndex = 77
Me.lblFLD53.Text = "Qот"
Me.lblFLD53.ForeColor = System.Drawing.Color.Blue
Me.txtFLD53.Location = New System.Drawing.Point(860,121)
Me.txtFLD53.name = "txtFLD53"
Me.txtFLD53.Size = New System.Drawing.Size(200, 20)
Me.txtFLD53.TabIndex = 78
Me.txtFLD53.Text = "" 
Me.lblFLD54.Location = New System.Drawing.Point(860,146)
Me.lblFLD54.name = "lblFLD54"
Me.lblFLD54.Size = New System.Drawing.Size(200, 20)
Me.lblFLD54.TabIndex = 79
Me.lblFLD54.Text = "Qв"
Me.lblFLD54.ForeColor = System.Drawing.Color.Blue
Me.txtFLD54.Location = New System.Drawing.Point(860,168)
Me.txtFLD54.name = "txtFLD54"
Me.txtFLD54.Size = New System.Drawing.Size(200, 20)
Me.txtFLD54.TabIndex = 80
Me.txtFLD54.Text = "" 
Me.lblFLD55.Location = New System.Drawing.Point(860,193)
Me.lblFLD55.name = "lblFLD55"
Me.lblFLD55.Size = New System.Drawing.Size(200, 20)
Me.lblFLD55.TabIndex = 81
Me.lblFLD55.Text = "Qгвс"
Me.lblFLD55.ForeColor = System.Drawing.Color.Blue
Me.txtFLD55.Location = New System.Drawing.Point(860,215)
Me.txtFLD55.name = "txtFLD55"
Me.txtFLD55.Size = New System.Drawing.Size(200, 20)
Me.txtFLD55.TabIndex = 82
Me.txtFLD55.Text = "" 
Me.lblFLD56.Location = New System.Drawing.Point(860,240)
Me.lblFLD56.name = "lblFLD56"
Me.lblFLD56.Size = New System.Drawing.Size(200, 20)
Me.lblFLD56.TabIndex = 83
Me.lblFLD56.Text = "Qну"
Me.lblFLD56.ForeColor = System.Drawing.Color.Blue
Me.txtFLD56.Location = New System.Drawing.Point(860,262)
Me.txtFLD56.name = "txtFLD56"
Me.txtFLD56.Size = New System.Drawing.Size(200, 20)
Me.txtFLD56.TabIndex = 84
Me.txtFLD56.Text = "" 
Me.lblFLD57.Location = New System.Drawing.Point(860,287)
Me.lblFLD57.name = "lblFLD57"
Me.lblFLD57.Size = New System.Drawing.Size(200, 20)
Me.lblFLD57.TabIndex = 85
Me.lblFLD57.Text = "Gот"
Me.lblFLD57.ForeColor = System.Drawing.Color.Blue
Me.txtFLD57.Location = New System.Drawing.Point(860,309)
Me.txtFLD57.name = "txtFLD57"
Me.txtFLD57.Size = New System.Drawing.Size(200, 20)
Me.txtFLD57.TabIndex = 86
Me.txtFLD57.Text = "" 
Me.lblFLD58.Location = New System.Drawing.Point(860,334)
Me.lblFLD58.name = "lblFLD58"
Me.lblFLD58.Size = New System.Drawing.Size(200, 20)
Me.lblFLD58.TabIndex = 87
Me.lblFLD58.Text = "Gв"
Me.lblFLD58.ForeColor = System.Drawing.Color.Blue
Me.txtFLD58.Location = New System.Drawing.Point(860,356)
Me.txtFLD58.name = "txtFLD58"
Me.txtFLD58.Size = New System.Drawing.Size(200, 20)
Me.txtFLD58.TabIndex = 88
Me.txtFLD58.Text = "" 
Me.lblFLD59.Location = New System.Drawing.Point(860,381)
Me.lblFLD59.name = "lblFLD59"
Me.lblFLD59.Size = New System.Drawing.Size(200, 20)
Me.lblFLD59.TabIndex = 89
Me.lblFLD59.Text = "Gну"
Me.lblFLD59.ForeColor = System.Drawing.Color.Blue
Me.txtFLD59.Location = New System.Drawing.Point(860,403)
Me.txtFLD59.name = "txtFLD59"
Me.txtFLD59.Size = New System.Drawing.Size(200, 20)
Me.txtFLD59.TabIndex = 90
Me.txtFLD59.Text = "" 
Me.lblFLD60.Location = New System.Drawing.Point(1070,5)
Me.lblFLD60.name = "lblFLD60"
Me.lblFLD60.Size = New System.Drawing.Size(200, 20)
Me.lblFLD60.TabIndex = 91
Me.lblFLD60.Text = "Часов_архив"
Me.lblFLD60.ForeColor = System.Drawing.Color.Blue
Me.txtFLD60.Location = New System.Drawing.Point(1070,27)
Me.txtFLD60.name = "txtFLD60"
Me.txtFLD60.Size = New System.Drawing.Size(200, 20)
Me.txtFLD60.TabIndex = 92
Me.txtFLD60.Text = "" 
Me.lblFLD61.Location = New System.Drawing.Point(1070,52)
Me.lblFLD61.name = "lblFLD61"
Me.lblFLD61.Size = New System.Drawing.Size(200, 20)
Me.lblFLD61.TabIndex = 93
Me.lblFLD61.Text = "Сут_архив"
Me.lblFLD61.ForeColor = System.Drawing.Color.Blue
Me.txtFLD61.Location = New System.Drawing.Point(1070,74)
Me.txtFLD61.name = "txtFLD61"
Me.txtFLD61.Size = New System.Drawing.Size(200, 20)
Me.txtFLD61.TabIndex = 94
Me.txtFLD61.Text = "" 
Me.lblFLD62.Location = New System.Drawing.Point(1070,99)
Me.lblFLD62.name = "lblFLD62"
Me.lblFLD62.Size = New System.Drawing.Size(200, 20)
Me.lblFLD62.TabIndex = 95
Me.lblFLD62.Text = "Термопреобр ГВС"
Me.lblFLD62.ForeColor = System.Drawing.Color.Blue
Me.txtFLD62.Location = New System.Drawing.Point(1070,121)
Me.txtFLD62.name = "txtFLD62"
Me.txtFLD62.Size = New System.Drawing.Size(200, 20)
Me.txtFLD62.TabIndex = 96
Me.txtFLD62.Text = "" 
Me.lblFLD63.Location = New System.Drawing.Point(1070,146)
Me.lblFLD63.name = "lblFLD63"
Me.lblFLD63.Size = New System.Drawing.Size(200, 20)
Me.lblFLD63.TabIndex = 97
Me.lblFLD63.Text = "Т1"
Me.lblFLD63.ForeColor = System.Drawing.Color.Blue
Me.txtFLD63.Location = New System.Drawing.Point(1070,168)
Me.txtFLD63.name = "txtFLD63"
Me.txtFLD63.Size = New System.Drawing.Size(200, 20)
Me.txtFLD63.TabIndex = 98
Me.txtFLD63.Text = "" 
Me.lblFLD64.Location = New System.Drawing.Point(1070,193)
Me.lblFLD64.name = "lblFLD64"
Me.lblFLD64.Size = New System.Drawing.Size(200, 20)
Me.lblFLD64.TabIndex = 99
Me.lblFLD64.Text = "Т2"
Me.lblFLD64.ForeColor = System.Drawing.Color.Blue
Me.txtFLD64.Location = New System.Drawing.Point(1070,215)
Me.txtFLD64.name = "txtFLD64"
Me.txtFLD64.Size = New System.Drawing.Size(200, 20)
Me.txtFLD64.TabIndex = 100
Me.txtFLD64.Text = "" 
Me.lblFLD65.Location = New System.Drawing.Point(1070,240)
Me.lblFLD65.name = "lblFLD65"
Me.lblFLD65.Size = New System.Drawing.Size(200, 20)
Me.lblFLD65.TabIndex = 101
Me.lblFLD65.Text = "Т3"
Me.lblFLD65.ForeColor = System.Drawing.Color.Blue
Me.txtFLD65.Location = New System.Drawing.Point(1070,262)
Me.txtFLD65.name = "txtFLD65"
Me.txtFLD65.Size = New System.Drawing.Size(200, 20)
Me.txtFLD65.TabIndex = 102
Me.txtFLD65.Text = "" 
Me.lblFLD66.Location = New System.Drawing.Point(1070,287)
Me.lblFLD66.name = "lblFLD66"
Me.lblFLD66.Size = New System.Drawing.Size(200, 20)
Me.lblFLD66.TabIndex = 103
Me.lblFLD66.Text = "Т4"
Me.lblFLD66.ForeColor = System.Drawing.Color.Blue
Me.txtFLD66.Location = New System.Drawing.Point(1070,309)
Me.txtFLD66.name = "txtFLD66"
Me.txtFLD66.Size = New System.Drawing.Size(200, 20)
Me.txtFLD66.TabIndex = 104
Me.txtFLD66.Text = "" 
Me.lblFLD67.Location = New System.Drawing.Point(1070,334)
Me.lblFLD67.name = "lblFLD67"
Me.lblFLD67.Size = New System.Drawing.Size(200, 20)
Me.lblFLD67.TabIndex = 105
Me.lblFLD67.Text = "Gтех"
Me.lblFLD67.ForeColor = System.Drawing.Color.Blue
Me.txtFLD67.Location = New System.Drawing.Point(1070,356)
Me.txtFLD67.name = "txtFLD67"
Me.txtFLD67.Size = New System.Drawing.Size(200, 20)
Me.txtFLD67.TabIndex = 106
Me.txtFLD67.Text = "" 
Me.lblFLD68.Location = New System.Drawing.Point(1070,381)
Me.lblFLD68.name = "lblFLD68"
Me.lblFLD68.Size = New System.Drawing.Size(200, 20)
Me.lblFLD68.TabIndex = 107
Me.lblFLD68.Text = "Gтех_гвс"
Me.lblFLD68.ForeColor = System.Drawing.Color.Blue
Me.txtFLD68.Location = New System.Drawing.Point(1070,403)
Me.txtFLD68.name = "txtFLD68"
Me.txtFLD68.Size = New System.Drawing.Size(200, 20)
Me.txtFLD68.TabIndex = 108
Me.txtFLD68.Text = "" 
Me.lblFLD69.Location = New System.Drawing.Point(1280,5)
Me.lblFLD69.name = "lblFLD69"
Me.lblFLD69.Size = New System.Drawing.Size(200, 20)
Me.lblFLD69.TabIndex = 109
Me.lblFLD69.Text = "Gгвс_м"
Me.lblFLD69.ForeColor = System.Drawing.Color.Blue
Me.txtFLD69.Location = New System.Drawing.Point(1280,27)
Me.txtFLD69.name = "txtFLD69"
Me.txtFLD69.Size = New System.Drawing.Size(200, 20)
Me.txtFLD69.TabIndex = 110
Me.txtFLD69.Text = "" 
Me.lblFLD70.Location = New System.Drawing.Point(1280,52)
Me.lblFLD70.name = "lblFLD70"
Me.lblFLD70.Size = New System.Drawing.Size(200, 20)
Me.lblFLD70.TabIndex = 111
Me.lblFLD70.Text = "Qтех"
Me.lblFLD70.ForeColor = System.Drawing.Color.Blue
Me.txtFLD70.Location = New System.Drawing.Point(1280,74)
Me.txtFLD70.name = "txtFLD70"
Me.txtFLD70.Size = New System.Drawing.Size(200, 20)
Me.txtFLD70.TabIndex = 112
Me.txtFLD70.Text = "" 
Me.lblFLD71.Location = New System.Drawing.Point(1280,99)
Me.lblFLD71.name = "lblFLD71"
Me.lblFLD71.Size = New System.Drawing.Size(200, 20)
Me.lblFLD71.TabIndex = 113
Me.lblFLD71.Text = "Qвент"
Me.lblFLD71.ForeColor = System.Drawing.Color.Blue
Me.txtFLD71.Location = New System.Drawing.Point(1280,121)
Me.txtFLD71.name = "txtFLD71"
Me.txtFLD71.Size = New System.Drawing.Size(200, 20)
Me.txtFLD71.TabIndex = 114
Me.txtFLD71.Text = "" 
Me.lblFLD72.Location = New System.Drawing.Point(1280,146)
Me.lblFLD72.name = "lblFLD72"
Me.lblFLD72.Size = New System.Drawing.Size(200, 20)
Me.lblFLD72.TabIndex = 115
Me.lblFLD72.Text = "Тхв"
Me.lblFLD72.ForeColor = System.Drawing.Color.Blue
Me.txtFLD72.Location = New System.Drawing.Point(1280,168)
Me.txtFLD72.name = "txtFLD72"
Me.txtFLD72.Size = New System.Drawing.Size(200, 20)
Me.txtFLD72.TabIndex = 116
Me.txtFLD72.Text = "" 
Me.lblFLD73.Location = New System.Drawing.Point(1280,193)
Me.lblFLD73.name = "lblFLD73"
Me.lblFLD73.Size = New System.Drawing.Size(200, 20)
Me.lblFLD73.TabIndex = 117
Me.lblFLD73.Text = "Расходомер ГВСц"
Me.lblFLD73.ForeColor = System.Drawing.Color.Blue
Me.txtFLD73.Location = New System.Drawing.Point(1280,215)
Me.txtFLD73.name = "txtFLD73"
Me.txtFLD73.Size = New System.Drawing.Size(200, 20)
Me.txtFLD73.TabIndex = 118
Me.txtFLD73.Text = "" 
Me.lblFLD81.Location = New System.Drawing.Point(1280,240)
Me.lblFLD81.name = "lblFLD81"
Me.lblFLD81.Size = New System.Drawing.Size(200, 20)
Me.lblFLD81.TabIndex = 119
Me.lblFLD81.Text = "Формула2"
Me.lblFLD81.ForeColor = System.Drawing.Color.Blue
Me.txtFLD81.Location = New System.Drawing.Point(1280,262)
Me.txtFLD81.name = "txtFLD81"
Me.txtFLD81.Size = New System.Drawing.Size(200, 20)
Me.txtFLD81.TabIndex = 120
Me.txtFLD81.Text = "" 
Me.lblFLD82.Location = New System.Drawing.Point(1280,287)
Me.lblFLD82.name = "lblFLD82"
Me.lblFLD82.Size = New System.Drawing.Size(200, 20)
Me.lblFLD82.TabIndex = 121
Me.lblFLD82.Text = "Термопреобр"
Me.lblFLD82.ForeColor = System.Drawing.Color.Blue
Me.txtFLD82.Location = New System.Drawing.Point(1280,309)
Me.txtFLD82.name = "txtFLD82"
Me.txtFLD82.Size = New System.Drawing.Size(200, 20)
Me.txtFLD82.TabIndex = 122
Me.txtFLD82.Text = "" 
Me.lblFLD83.Location = New System.Drawing.Point(1280,334)
Me.lblFLD83.name = "lblFLD83"
Me.lblFLD83.Size = New System.Drawing.Size(200, 20)
Me.lblFLD83.TabIndex = 123
Me.lblFLD83.Text = "Gвент"
Me.lblFLD83.ForeColor = System.Drawing.Color.Blue
Me.txtFLD83.Location = New System.Drawing.Point(1280,356)
Me.txtFLD83.name = "txtFLD83"
Me.txtFLD83.Size = New System.Drawing.Size(200, 20)
Me.txtFLD83.TabIndex = 124
Me.txtFLD83.Text = "" 
Me.lblFLD84.Location = New System.Drawing.Point(1280,381)
Me.lblFLD84.name = "lblFLD84"
Me.lblFLD84.Size = New System.Drawing.Size(200, 20)
Me.lblFLD84.TabIndex = 125
Me.lblFLD84.Text = "Код УУТЭ"
Me.lblFLD84.ForeColor = System.Drawing.Color.Blue
Me.txtFLD84.Location = New System.Drawing.Point(1280,403)
Me.txtFLD84.name = "txtFLD84"
Me.txtFLD84.Size = New System.Drawing.Size(200, 20)
Me.txtFLD84.TabIndex = 126
Me.txtFLD84.Text = "" 
Me.lblFLD85.Location = New System.Drawing.Point(1490,5)
Me.lblFLD85.name = "lblFLD85"
Me.lblFLD85.Size = New System.Drawing.Size(200, 20)
Me.lblFLD85.TabIndex = 127
Me.lblFLD85.Text = "Сист_теплопотребления"
Me.lblFLD85.ForeColor = System.Drawing.Color.Blue
Me.txtFLD85.Location = New System.Drawing.Point(1490,27)
Me.txtFLD85.name = "txtFLD85"
Me.txtFLD85.Size = New System.Drawing.Size(200, 20)
Me.txtFLD85.TabIndex = 128
Me.txtFLD85.Text = "" 
Me.lblFLD86.Location = New System.Drawing.Point(1490,52)
Me.lblFLD86.name = "lblFLD86"
Me.lblFLD86.Size = New System.Drawing.Size(200, 20)
Me.lblFLD86.TabIndex = 129
Me.lblFLD86.Text = "Qтех_гвс"
Me.lblFLD86.ForeColor = System.Drawing.Color.Blue
Me.txtFLD86.Location = New System.Drawing.Point(1490,74)
Me.txtFLD86.name = "txtFLD86"
Me.txtFLD86.Size = New System.Drawing.Size(200, 20)
Me.txtFLD86.TabIndex = 130
Me.txtFLD86.Text = "" 
Me.lblFLD87.Location = New System.Drawing.Point(1490,99)
Me.lblFLD87.name = "lblFLD87"
Me.lblFLD87.Size = New System.Drawing.Size(200, 20)
Me.lblFLD87.TabIndex = 131
Me.lblFLD87.Text = "Qтех_гвс ср"
Me.lblFLD87.ForeColor = System.Drawing.Color.Blue
Me.txtFLD87.Location = New System.Drawing.Point(1490,121)
Me.txtFLD87.name = "txtFLD87"
Me.txtFLD87.Size = New System.Drawing.Size(200, 20)
Me.txtFLD87.TabIndex = 132
Me.txtFLD87.Text = "" 
Me.lblFLD88.Location = New System.Drawing.Point(1490,146)
Me.lblFLD88.name = "lblFLD88"
Me.lblFLD88.Size = New System.Drawing.Size(200, 20)
Me.lblFLD88.TabIndex = 133
Me.lblFLD88.Text = "Qгвс ср"
Me.lblFLD88.ForeColor = System.Drawing.Color.Blue
Me.txtFLD88.Location = New System.Drawing.Point(1490,168)
Me.txtFLD88.name = "txtFLD88"
Me.txtFLD88.Size = New System.Drawing.Size(200, 20)
Me.txtFLD88.TabIndex = 134
Me.txtFLD88.Text = "" 
Me.lblFLD89.Location = New System.Drawing.Point(1490,193)
Me.lblFLD89.name = "lblFLD89"
Me.lblFLD89.Size = New System.Drawing.Size(200, 20)
Me.lblFLD89.TabIndex = 135
Me.lblFLD89.Text = "Дата поверки"
Me.lblFLD89.ForeColor = System.Drawing.Color.Blue
Me.txtFLD89.Location = New System.Drawing.Point(1490,215)
Me.txtFLD89.name = "txtFLD89"
Me.txtFLD89.Size = New System.Drawing.Size(200, 20)
Me.txtFLD89.TabIndex = 136
Me.txtFLD89.Text = "" 
Me.lblFLD90.Location = New System.Drawing.Point(1490,240)
Me.lblFLD90.name = "lblFLD90"
Me.lblFLD90.Size = New System.Drawing.Size(200, 20)
Me.lblFLD90.TabIndex = 137
Me.lblFLD90.Text = "Фамилия"
Me.lblFLD90.ForeColor = System.Drawing.Color.Blue
Me.txtFLD90.Location = New System.Drawing.Point(1490,262)
Me.txtFLD90.name = "txtFLD90"
Me.txtFLD90.Size = New System.Drawing.Size(200, 20)
Me.txtFLD90.TabIndex = 138
Me.txtFLD90.Text = "" 
Me.lblFLD92.Location = New System.Drawing.Point(1490,287)
Me.lblFLD92.name = "lblFLD92"
Me.lblFLD92.Size = New System.Drawing.Size(200, 20)
Me.lblFLD92.TabIndex = 139
Me.lblFLD92.Text = "Узел учета"
Me.lblFLD92.ForeColor = System.Drawing.Color.Blue
Me.txtFLD92.Location = New System.Drawing.Point(1490,309)
Me.txtFLD92.name = "txtFLD92"
Me.txtFLD92.Size = New System.Drawing.Size(200, 20)
Me.txtFLD92.TabIndex = 140
Me.txtFLD92.Text = "" 
Me.lblFLD93.Location = New System.Drawing.Point(1490,334)
Me.lblFLD93.name = "lblFLD93"
Me.lblFLD93.Size = New System.Drawing.Size(200, 20)
Me.lblFLD93.TabIndex = 141
Me.lblFLD93.Text = "Стр.адрес"
Me.lblFLD93.ForeColor = System.Drawing.Color.Blue
Me.txtFLD93.Location = New System.Drawing.Point(1490,356)
Me.txtFLD93.name = "txtFLD93"
Me.txtFLD93.Size = New System.Drawing.Size(200, 20)
Me.txtFLD93.TabIndex = 142
Me.txtFLD93.Text = "" 
Me.lblFLD94.Location = New System.Drawing.Point(1490,381)
Me.lblFLD94.name = "lblFLD94"
Me.lblFLD94.Size = New System.Drawing.Size(200, 20)
Me.lblFLD94.TabIndex = 143
Me.lblFLD94.Text = "G(гвс)ОБР"
Me.lblFLD94.ForeColor = System.Drawing.Color.Blue
Me.txtFLD94.Location = New System.Drawing.Point(1490,403)
Me.txtFLD94.name = "txtFLD94"
Me.txtFLD94.Size = New System.Drawing.Size(200, 20)
Me.txtFLD94.TabIndex = 144
Me.txtFLD94.Text = "" 
Me.lblFLD95.Location = New System.Drawing.Point(1700,5)
Me.lblFLD95.name = "lblFLD95"
Me.lblFLD95.Size = New System.Drawing.Size(200, 20)
Me.lblFLD95.TabIndex = 145
Me.lblFLD95.Text = "DyГВСц"
Me.lblFLD95.ForeColor = System.Drawing.Color.Blue
Me.txtFLD95.Location = New System.Drawing.Point(1700,27)
Me.txtFLD95.name = "txtFLD95"
Me.txtFLD95.Size = New System.Drawing.Size(200, 20)
Me.txtFLD95.TabIndex = 146
Me.txtFLD95.Text = "" 
Me.lblFLD96.Location = New System.Drawing.Point(1700,52)
Me.lblFLD96.name = "lblFLD96"
Me.lblFLD96.Size = New System.Drawing.Size(200, 20)
Me.lblFLD96.TabIndex = 147
Me.lblFLD96.Text = "Цена_имп_M1"
Me.lblFLD96.ForeColor = System.Drawing.Color.Blue
Me.txtFLD96.Location = New System.Drawing.Point(1700,74)
Me.txtFLD96.name = "txtFLD96"
Me.txtFLD96.Size = New System.Drawing.Size(200, 20)
Me.txtFLD96.TabIndex = 148
Me.txtFLD96.Text = "" 
Me.lblFLD97.Location = New System.Drawing.Point(1700,99)
Me.lblFLD97.name = "lblFLD97"
Me.lblFLD97.Size = New System.Drawing.Size(200, 20)
Me.lblFLD97.TabIndex = 149
Me.lblFLD97.Text = "Цена_имп_M2"
Me.lblFLD97.ForeColor = System.Drawing.Color.Blue
Me.txtFLD97.Location = New System.Drawing.Point(1700,121)
Me.txtFLD97.name = "txtFLD97"
Me.txtFLD97.Size = New System.Drawing.Size(200, 20)
Me.txtFLD97.TabIndex = 150
Me.txtFLD97.Text = "" 
Me.lblFLD98.Location = New System.Drawing.Point(1700,146)
Me.lblFLD98.name = "lblFLD98"
Me.lblFLD98.Size = New System.Drawing.Size(200, 20)
Me.lblFLD98.TabIndex = 151
Me.lblFLD98.Text = "Цена_имп_M1гв"
Me.lblFLD98.ForeColor = System.Drawing.Color.Blue
Me.txtFLD98.Location = New System.Drawing.Point(1700,168)
Me.txtFLD98.name = "txtFLD98"
Me.txtFLD98.Size = New System.Drawing.Size(200, 20)
Me.txtFLD98.TabIndex = 152
Me.txtFLD98.Text = "" 
Me.lblFLD99.Location = New System.Drawing.Point(1700,193)
Me.lblFLD99.name = "lblFLD99"
Me.lblFLD99.Size = New System.Drawing.Size(200, 20)
Me.lblFLD99.TabIndex = 153
Me.lblFLD99.Text = "Цена_имп_M2гв"
Me.lblFLD99.ForeColor = System.Drawing.Color.Blue
Me.txtFLD99.Location = New System.Drawing.Point(1700,215)
Me.txtFLD99.name = "txtFLD99"
Me.txtFLD99.Size = New System.Drawing.Size(200, 20)
Me.txtFLD99.TabIndex = 154
Me.txtFLD99.Text = "" 
Me.lblFLD100.Location = New System.Drawing.Point(1700,240)
Me.lblFLD100.name = "lblFLD100"
Me.lblFLD100.Size = New System.Drawing.Size(200, 20)
Me.lblFLD100.TabIndex = 155
Me.lblFLD100.Text = "Доп_погр_изм_M1%"
Me.lblFLD100.ForeColor = System.Drawing.Color.Blue
Me.txtFLD100.Location = New System.Drawing.Point(1700,262)
Me.txtFLD100.name = "txtFLD100"
Me.txtFLD100.Size = New System.Drawing.Size(200, 20)
Me.txtFLD100.TabIndex = 156
Me.txtFLD100.Text = "" 
Me.lblFLD101.Location = New System.Drawing.Point(1700,287)
Me.lblFLD101.name = "lblFLD101"
Me.lblFLD101.Size = New System.Drawing.Size(200, 20)
Me.lblFLD101.TabIndex = 157
Me.lblFLD101.Text = "Доп_погр_изм_M2%"
Me.lblFLD101.ForeColor = System.Drawing.Color.Blue
Me.txtFLD101.Location = New System.Drawing.Point(1700,309)
Me.txtFLD101.name = "txtFLD101"
Me.txtFLD101.Size = New System.Drawing.Size(200, 20)
Me.txtFLD101.TabIndex = 158
Me.txtFLD101.Text = "" 
Me.lblFLD102.Location = New System.Drawing.Point(1700,334)
Me.lblFLD102.name = "lblFLD102"
Me.lblFLD102.Size = New System.Drawing.Size(200, 20)
Me.lblFLD102.TabIndex = 159
Me.lblFLD102.Text = "Доп_погр_изм_M1гв%"
Me.lblFLD102.ForeColor = System.Drawing.Color.Blue
Me.txtFLD102.Location = New System.Drawing.Point(1700,356)
Me.txtFLD102.name = "txtFLD102"
Me.txtFLD102.Size = New System.Drawing.Size(200, 20)
Me.txtFLD102.TabIndex = 160
Me.txtFLD102.Text = "" 
Me.lblFLD103.Location = New System.Drawing.Point(1700,381)
Me.lblFLD103.name = "lblFLD103"
Me.lblFLD103.Size = New System.Drawing.Size(200, 20)
Me.lblFLD103.TabIndex = 161
Me.lblFLD103.Text = "Доп_погр_изм_M2гв%"
Me.lblFLD103.ForeColor = System.Drawing.Color.Blue
Me.txtFLD103.Location = New System.Drawing.Point(1700,403)
Me.txtFLD103.name = "txtFLD103"
Me.txtFLD103.Size = New System.Drawing.Size(200, 20)
Me.txtFLD103.TabIndex = 162
Me.txtFLD103.Text = "" 
Me.lblFLD104.Location = New System.Drawing.Point(1910,5)
Me.lblFLD104.name = "lblFLD104"
Me.lblFLD104.Size = New System.Drawing.Size(200, 20)
Me.lblFLD104.TabIndex = 163
Me.lblFLD104.Text = "Расходомер M2"
Me.lblFLD104.ForeColor = System.Drawing.Color.Blue
Me.txtFLD104.Location = New System.Drawing.Point(1910,27)
Me.txtFLD104.name = "txtFLD104"
Me.txtFLD104.Size = New System.Drawing.Size(200, 20)
Me.txtFLD104.TabIndex = 164
Me.txtFLD104.Text = "" 
        Me.AutoScroll = True

Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD12)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD12)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD13)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD13)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD14)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD14)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD15)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD15)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD16)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD16)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD17)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD17)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD18)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD18)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD19)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD19)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD20)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD20)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD21)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD21)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD22)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD22)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD23)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD23)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD24)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD24)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD25)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD25)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD26)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD26)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD27)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD27)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD28)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD28)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD29)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD29)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD30)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD30)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD31)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD31)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD32)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD32)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD33)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD33)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD34)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD34)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD35)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD35)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD36)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD36)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD37)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD37)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD40)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD40)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD41)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD41)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD42)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD42)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD43)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD43)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD45)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD45)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD46)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD46)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD47)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD47)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD48)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD48)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD49)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD49)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD50)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD50)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD51)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD51)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD52)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD52)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD53)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD53)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD54)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD54)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD55)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD55)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD56)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD56)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD57)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD57)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD58)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD58)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD59)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD59)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD60)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD60)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD61)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD61)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD62)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD62)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD63)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD63)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD64)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD64)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD65)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD65)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD66)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD66)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD67)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD67)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD68)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD68)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD69)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD69)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD70)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD70)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD71)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD71)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD72)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD72)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD73)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD73)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD81)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD81)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD82)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD82)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD83)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD83)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD84)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD84)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD85)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD85)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD86)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD86)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD87)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD87)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD88)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD88)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD89)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD89)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD90)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD90)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD92)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD92)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD93)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD93)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD94)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD94)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD95)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD95)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD96)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD96)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD97)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD97)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD98)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD98)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD99)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD99)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD100)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD100)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD101)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD101)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD102)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD102)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD103)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD103)
Me.HolderPanel.ClientArea.Controls.Add(Me.lblFLD104)
Me.HolderPanel.ClientArea.Controls.Add(Me.txtFLD104)
        Me.Controls.Add(Me.HolderPanel)
        Me.HolderPanel.ResumeLayout(False)
        Me.HolderPanel.PerformLayout()
        Me.name = "editTPLT_CONTRACT"
        Me.Size = New System.Drawing.Size(232, 120)
        Me.ResumeLayout (False)
    End Sub
#End Region

private sub txtFLD12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD12.TextChanged
  Changing

end sub
private sub txtFLD13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD13.TextChanged
  Changing

end sub
private sub txtFLD14_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD14.TextChanged
  Changing

end sub
private sub txtFLD15_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD15.TextChanged
  Changing

end sub
private sub txtFLD16_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD16.TextChanged
  Changing

end sub
private sub txtFLD17_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD17.TextChanged
  Changing

end sub
private sub txtFLD18_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD18.TextChanged
  Changing

end sub
private sub txtFLD19_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD19.TextChanged
  Changing

end sub
private sub txtFLD20_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD20.TextChanged
  Changing

end sub
private sub txtFLD21_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD21.TextChanged
  Changing

end sub
private sub txtFLD22_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD22.TextChanged
  Changing

end sub
private sub txtFLD23_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD23.TextChanged
  Changing

end sub
private sub txtFLD24_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD24.TextChanged
  Changing

end sub
private sub txtFLD25_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD25.TextChanged
  Changing

end sub
private sub txtFLD26_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD26.TextChanged
  Changing

end sub
private sub txtFLD27_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD27.TextChanged
  Changing

end sub
private sub txtFLD28_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD28.TextChanged
  Changing

end sub
private sub txtFLD29_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD29.TextChanged
  Changing

end sub
private sub txtFLD30_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD30.TextChanged
  Changing

end sub
private sub txtFLD31_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD31.TextChanged
  Changing

end sub
private sub txtFLD32_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD32.TextChanged
  Changing

end sub
private sub txtFLD33_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD33.TextChanged
  Changing

end sub
private sub txtFLD34_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD34.TextChanged
  Changing

end sub
private sub txtFLD35_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD35.TextChanged
  Changing

end sub
private sub txtFLD36_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD36.TextChanged
  Changing

end sub
private sub txtFLD37_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD37.TextChanged
  Changing

end sub
private sub txtFLD40_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD40.TextChanged
  Changing

end sub
private sub txtFLD41_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD41.TextChanged
  Changing

end sub
private sub txtFLD42_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD42.TextChanged
  Changing

end sub
private sub txtFLD43_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD43.TextChanged
  Changing

end sub
private sub txtFLD45_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD45.TextChanged
  Changing

end sub
private sub txtFLD46_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD46.TextChanged
  Changing

end sub
private sub txtFLD47_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD47.TextChanged
  Changing

end sub
private sub txtFLD48_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD48.TextChanged
  Changing

end sub
private sub txtFLD49_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD49.TextChanged
  Changing

end sub
private sub txtFLD50_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD50.TextChanged
  Changing

end sub
private sub txtFLD51_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD51.TextChanged
  Changing

end sub
private sub txtFLD52_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD52.TextChanged
  Changing

end sub
private sub txtFLD53_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD53.TextChanged
  Changing

end sub
private sub txtFLD54_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD54.TextChanged
  Changing

end sub
private sub txtFLD55_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD55.TextChanged
  Changing

end sub
private sub txtFLD56_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD56.TextChanged
  Changing

end sub
private sub txtFLD57_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD57.TextChanged
  Changing

end sub
private sub txtFLD58_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD58.TextChanged
  Changing

end sub
private sub txtFLD59_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD59.TextChanged
  Changing

end sub
private sub txtFLD60_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD60.TextChanged
  Changing

end sub
private sub txtFLD61_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD61.TextChanged
  Changing

end sub
private sub txtFLD62_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD62.TextChanged
  Changing

end sub
private sub txtFLD63_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD63.TextChanged
  Changing

end sub
private sub txtFLD64_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD64.TextChanged
  Changing

end sub
private sub txtFLD65_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD65.TextChanged
  Changing

end sub
private sub txtFLD66_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD66.TextChanged
  Changing

end sub
private sub txtFLD67_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD67.TextChanged
  Changing

end sub
private sub txtFLD68_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD68.TextChanged
  Changing

end sub
private sub txtFLD69_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD69.TextChanged
  Changing

end sub
private sub txtFLD70_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD70.TextChanged
  Changing

end sub
private sub txtFLD71_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD71.TextChanged
  Changing

end sub
private sub txtFLD72_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD72.TextChanged
  Changing

end sub
private sub txtFLD73_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD73.TextChanged
  Changing

end sub
private sub txtFLD81_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD81.TextChanged
  Changing

end sub
private sub txtFLD82_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD82.TextChanged
  Changing

end sub
private sub txtFLD83_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD83.TextChanged
  Changing

end sub
private sub txtFLD84_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD84.TextChanged
  Changing

end sub
private sub txtFLD85_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD85.TextChanged
  Changing

end sub
private sub txtFLD86_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD86.TextChanged
  Changing

end sub
private sub txtFLD87_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD87.TextChanged
  Changing

end sub
private sub txtFLD88_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD88.TextChanged
  Changing

end sub
private sub txtFLD89_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD89.TextChanged
  Changing

end sub
private sub txtFLD90_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD90.TextChanged
  Changing

end sub
private sub txtFLD92_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD92.TextChanged
  Changing

end sub
private sub txtFLD93_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD93.TextChanged
  Changing

end sub
private sub txtFLD94_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD94.TextChanged
  Changing

end sub
private sub txtFLD95_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD95.TextChanged
  Changing

end sub
private sub txtFLD96_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD96.TextChanged
  Changing

end sub
private sub txtFLD97_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD97.TextChanged
  Changing

end sub
private sub txtFLD98_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD98.TextChanged
  Changing

end sub
private sub txtFLD99_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD99.TextChanged
  Changing

end sub
private sub txtFLD100_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD100.TextChanged
  Changing

end sub
private sub txtFLD101_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD101.TextChanged
  Changing

end sub
private sub txtFLD102_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD102.TextChanged
  Changing

end sub
private sub txtFLD103_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD103.TextChanged
  Changing

end sub
private sub txtFLD104_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFLD104.TextChanged
  Changing

end sub

Public Item As TPLT.TPLT.TPLT_CONTRACT
Private mRowReadOnly As Boolean
Public GuiManager As LATIRGuiManager.LATIRGuiManager


''' <summary>
'''Инициализация
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Sub Attach(ByVal gm As LATIRGuiManager.LATIRGuiManager, ByVal ri As LATIR.Document.DocRow_Base,byval RowReadOnly as boolean  ) Implements LATIRGuiControls.IRowEditor.Attach
        Item = Ctype(ri,TPLT.TPLT.TPLT_CONTRACT)
        GuiManager = gm
        mRowReadOnly = RowReadOnly
        If Item Is Nothing Then Exit Sub
        mOnInit = true

txtFLD12.text = item.FLD12
txtFLD13.text = item.FLD13
txtFLD14.text = item.FLD14
txtFLD15.text = item.FLD15
txtFLD16.text = item.FLD16
txtFLD17.text = item.FLD17
txtFLD18.text = item.FLD18
txtFLD19.text = item.FLD19
txtFLD20.text = item.FLD20
txtFLD21.text = item.FLD21
txtFLD22.text = item.FLD22
txtFLD23.text = item.FLD23
txtFLD24.text = item.FLD24
txtFLD25.text = item.FLD25
txtFLD26.text = item.FLD26
txtFLD27.text = item.FLD27
txtFLD28.text = item.FLD28
txtFLD29.text = item.FLD29
txtFLD30.text = item.FLD30
txtFLD31.text = item.FLD31
txtFLD32.text = item.FLD32
txtFLD33.text = item.FLD33
txtFLD34.text = item.FLD34
txtFLD35.text = item.FLD35
txtFLD36.text = item.FLD36
txtFLD37.text = item.FLD37
txtFLD40.text = item.FLD40
txtFLD41.text = item.FLD41
txtFLD42.text = item.FLD42
txtFLD43.text = item.FLD43
txtFLD45.text = item.FLD45
txtFLD46.text = item.FLD46
txtFLD47.text = item.FLD47
txtFLD48.text = item.FLD48
txtFLD49.text = item.FLD49
txtFLD50.text = item.FLD50
txtFLD51.text = item.FLD51
txtFLD52.text = item.FLD52
txtFLD53.text = item.FLD53
txtFLD54.text = item.FLD54
txtFLD55.text = item.FLD55
txtFLD56.text = item.FLD56
txtFLD57.text = item.FLD57
txtFLD58.text = item.FLD58
txtFLD59.text = item.FLD59
txtFLD60.text = item.FLD60
txtFLD61.text = item.FLD61
txtFLD62.text = item.FLD62
txtFLD63.text = item.FLD63
txtFLD64.text = item.FLD64
txtFLD65.text = item.FLD65
txtFLD66.text = item.FLD66
txtFLD67.text = item.FLD67
txtFLD68.text = item.FLD68
txtFLD69.text = item.FLD69
txtFLD70.text = item.FLD70
txtFLD71.text = item.FLD71
txtFLD72.text = item.FLD72
txtFLD73.text = item.FLD73
txtFLD81.text = item.FLD81
txtFLD82.text = item.FLD82
txtFLD83.text = item.FLD83
txtFLD84.text = item.FLD84
txtFLD85.text = item.FLD85
txtFLD86.text = item.FLD86
txtFLD87.text = item.FLD87
txtFLD88.text = item.FLD88
txtFLD89.text = item.FLD89
txtFLD90.text = item.FLD90
txtFLD92.text = item.FLD92
txtFLD93.text = item.FLD93
txtFLD94.text = item.FLD94
txtFLD95.text = item.FLD95
txtFLD96.text = item.FLD96
txtFLD97.text = item.FLD97
txtFLD98.text = item.FLD98
txtFLD99.text = item.FLD99
txtFLD100.text = item.FLD100
txtFLD101.text = item.FLD101
txtFLD102.text = item.FLD102
txtFLD103.text = item.FLD103
txtFLD104.text = item.FLD104
        mOnInit = false
  raiseevent Refreshed()
end sub


''' <summary>
'''Сохранения данных в полях объекта
''' </summary>
''' <remarks>
'''
''' </remarks>
Public Sub Save() Implements LATIRGuiControls.IRowEditor.Save
  if mRowReadOnly =false then

item.FLD12 = txtFLD12.text
item.FLD13 = txtFLD13.text
item.FLD14 = txtFLD14.text
item.FLD15 = txtFLD15.text
item.FLD16 = txtFLD16.text
item.FLD17 = txtFLD17.text
item.FLD18 = txtFLD18.text
item.FLD19 = txtFLD19.text
item.FLD20 = txtFLD20.text
item.FLD21 = txtFLD21.text
item.FLD22 = txtFLD22.text
item.FLD23 = txtFLD23.text
item.FLD24 = txtFLD24.text
item.FLD25 = txtFLD25.text
item.FLD26 = txtFLD26.text
item.FLD27 = txtFLD27.text
item.FLD28 = txtFLD28.text
item.FLD29 = txtFLD29.text
item.FLD30 = txtFLD30.text
item.FLD31 = txtFLD31.text
item.FLD32 = txtFLD32.text
item.FLD33 = txtFLD33.text
item.FLD34 = txtFLD34.text
item.FLD35 = txtFLD35.text
item.FLD36 = txtFLD36.text
item.FLD37 = txtFLD37.text
item.FLD40 = txtFLD40.text
item.FLD41 = txtFLD41.text
item.FLD42 = txtFLD42.text
item.FLD43 = txtFLD43.text
item.FLD45 = txtFLD45.text
item.FLD46 = txtFLD46.text
item.FLD47 = txtFLD47.text
item.FLD48 = txtFLD48.text
item.FLD49 = txtFLD49.text
item.FLD50 = txtFLD50.text
item.FLD51 = txtFLD51.text
item.FLD52 = txtFLD52.text
item.FLD53 = txtFLD53.text
item.FLD54 = txtFLD54.text
item.FLD55 = txtFLD55.text
item.FLD56 = txtFLD56.text
item.FLD57 = txtFLD57.text
item.FLD58 = txtFLD58.text
item.FLD59 = txtFLD59.text
item.FLD60 = txtFLD60.text
item.FLD61 = txtFLD61.text
item.FLD62 = txtFLD62.text
item.FLD63 = txtFLD63.text
item.FLD64 = txtFLD64.text
item.FLD65 = txtFLD65.text
item.FLD66 = txtFLD66.text
item.FLD67 = txtFLD67.text
item.FLD68 = txtFLD68.text
item.FLD69 = txtFLD69.text
item.FLD70 = txtFLD70.text
item.FLD71 = txtFLD71.text
item.FLD72 = txtFLD72.text
item.FLD73 = txtFLD73.text
item.FLD81 = txtFLD81.text
item.FLD82 = txtFLD82.text
item.FLD83 = txtFLD83.text
item.FLD84 = txtFLD84.text
item.FLD85 = txtFLD85.text
item.FLD86 = txtFLD86.text
item.FLD87 = txtFLD87.text
item.FLD88 = txtFLD88.text
item.FLD89 = txtFLD89.text
item.FLD90 = txtFLD90.text
item.FLD92 = txtFLD92.text
item.FLD93 = txtFLD93.text
item.FLD94 = txtFLD94.text
item.FLD95 = txtFLD95.text
item.FLD96 = txtFLD96.text
item.FLD97 = txtFLD97.text
item.FLD98 = txtFLD98.text
item.FLD99 = txtFLD99.text
item.FLD100 = txtFLD100.text
item.FLD101 = txtFLD101.text
item.FLD102 = txtFLD102.text
item.FLD103 = txtFLD103.text
item.FLD104 = txtFLD104.text
  end if
  mChanged = false
  raiseevent saved()
end sub
Public function IsOK() as boolean Implements LATIRGuiControls.IRowEditor.IsOK
 Dim mIsOK as boolean
 mIsOK=true
 if mRowReadOnly  then return true

 return mIsOK
end function
Public function IsChanged() as boolean Implements LATIRGuiControls.IRowEditor.IsChanged
 return mChanged
end function
Public Sub SetupPanel()
    HolderPanel.SetupPanel()
End Sub
Public Overridable Function GetMaxX() As Double
    Return HolderPanel.GetMaxX()
End Function
Public Overridable Function GetMaxY() As Double
    Return HolderPanel.GetMaxY()
End Function
end class

