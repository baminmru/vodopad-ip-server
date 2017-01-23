Imports System.Windows.Forms

Public Class frmMDIMain

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

   

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer


    Private Sub ГоловныеПоставщикиToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ГоловныеПоставщикиToolStripMenuItem.Click
        If fwhotop Is Nothing Then
            fwhotop = New frmWhoGiveTop
        End If
        fwhotop.MdiParent = Me
        fwhotop.Show()
        fwhotop.Select()
    End Sub

    Private Sub frmMDIMain_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        TvMain.CloseDBConnection()
    End Sub

    Private Sub frmMDIMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TvMain = New STKTVMain.TVMain
        If TvMain.Init() = False Then Application.Exit()

    End Sub

    Private Sub ПоставщикиToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ПоставщикиToolStripMenuItem.Click

        If fwho Is Nothing Then
            fwho = New frmWhoGive
        End If
        fwho.MdiParent = Me
        fwho.Show()
        fwho.Select()
    End Sub

    Private Sub ГруппыУзловToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ГруппыУзловToolStripMenuItem.Click
        If fgrp Is Nothing Then
            fgrp = New frmGroups
        End If
        fgrp.MdiParent = Me
        fgrp.Show()
        fgrp.Select()
    End Sub


    Private Sub СхемыПодключенияToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles СхемыПодключенияToolStripMenuItem.Click
        If fsch Is Nothing Then
            fsch = New frmSchema
        End If
        fsch.MdiParent = Me
        fsch.Show()
        fsch.Select()
    End Sub

    Private Sub ТипыУстройствToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ТипыУстройствToolStripMenuItem.Click
        If fdev Is Nothing Then
            fdev = New frmDevType
        End If
        fdev.MdiParent = Me
        fdev.Show()
        fdev.Select()
    End Sub


    Private Sub ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem2.Click

        If fnodes Is Nothing Then
            fnodes = New frmNodes
        End If

        fnodes.MdiParent = Me

        fnodes.Show()
        fnodes.Select()
    End Sub


    Public Function checkInstance() As Process
        Dim cProcess As Process = process.GetCurrentProcess()
        Dim aProcesses() As Process = process.GetProcessesByName(cProcess.ProcessName)
        'loop through all the processes that are currently running on the
        'system that have the same name
        For Each process As Process In aProcesses
            'Ignore the currently running process
            If process.Id <> cProcess.Id Then
                'Check if the process is running using the same EXE as this one
                If System.Reflection.Assembly.GetExecutingAssembly().Location = cProcess.MainModule.FileName Then
                    'if so return to the calling function with the instance of the process
                    Return process
                End If
            End If
        Next
        'if nothing was found then this is the only instance, so return null
        Return Nothing
    End Function





    Public Sub New()

        Dim tempProcess As Process
        tempProcess = checkInstance()
        If Not tempProcess Is Nothing Then
            ShowWindowAsync(tempProcess.MainWindowHandle, ShowWindowConstants.SW_SHOWMINIMIZED)

            ShowWindowAsync(tempProcess.MainWindowHandle, ShowWindowConstants.SW_RESTORE)
            End
        End If
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ШаблоныToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ШаблоныToolStripMenuItem.Click
        If fmasks Is Nothing Then
            fmasks = New frmMasks
        End If
        fmasks.MdiParent = Me
        fmasks.Show()
        fmasks.Select()
    End Sub

    Private Sub ДоговорныеПараметрыToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ДоговорныеПараметрыToolStripMenuItem.Click
        If fpar Is Nothing Then
            fpar = New frmParams
        End If
        fpar.MdiParent = Me
        fpar.Show()
        fpar.Select()
    End Sub

    Private Sub ToolStripSeparator3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSeparator3.Click
        Dim f As SplashScreen1
        f = New SplashScreen1
        f.ShowDialog()

    End Sub

    Private Sub mnuUsers_Click(sender As System.Object, e As System.EventArgs) Handles mnuUsers.Click
        If fusr Is Nothing Then
            fusr = New frmUsers
        End If
        fusr.MdiParent = Me
        fusr.Show()
        fusr.Select()
    End Sub

    Private Sub РабочиеСтанцииToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles РабочиеСтанцииToolStripMenuItem.Click
        If fip Is Nothing Then
            fip = New frmIP
        End If
        fip.MdiParent = Me
        fip.Show()
        fip.Select()
    End Sub

    Private Sub mnuPipeType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPipeType.Click
        If fpipe Is Nothing Then
            fpipe = New frmPipeType
        End If
        fpipe.MdiParent = Me
        fpipe.Show()
        fpipe.Select()
    End Sub

    Private Sub mnuEdizm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEdizm.Click
        If fed Is Nothing Then
            fed = New frmEDIZM
        End If
        fed.MdiParent = Me
        fed.Show()
        fed.Select()
    End Sub

    Private Sub mnuDanfossSchema_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDanfossSchema.Click
        If fdansch Is Nothing Then
            fdansch = New frmDanSchema
        End If
        fdansch.MdiParent = Me
        fdansch.Show()
        fdansch.Select()
    End Sub
End Class
