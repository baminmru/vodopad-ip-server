Public Class Form1
    Private TvMain As STKTVMain.TVMain
    Private inTimer As Boolean = False

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
        Me.InitializeComponent()
    End Sub
    Private TickCount As Integer = 0

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If chkRefresh.Checked = False Then
            Exit Sub
        End If

        If Not inTimer Then
            inTimer = True
            Dim dd As DataTable
            Dim dt As DataTable
            If txtFilter.Text = "" Then
                dt = TvMain.QuerySelect("select cshort as Узел,dbeg as Дата,ctype as Тип,duration as Длительность,cresult as Текст from  v_logcall where dbeg >sysdate-10.0/24.0/60.0 order by dbeg desc")
            Else
                dt = TvMain.QuerySelect("select cshort as Узел,dbeg as Дата,ctype as Тип,duration as Длительность,cresult as Текст from  v_logcall where dbeg >sysdate-10.0/24.0/60.0  and ( cshort like '%" + txtFilter.Text + "%'  or cresult like '%" + txtFilter.Text + "%')  order by dbeg desc")
            End If
            If Not DataGridView1.DataSource Is Nothing Then
                dd = DataGridView1.DataSource
                dd.Dispose()
            End If

            DataGridView1.DataSource = dt



            If TickCount = 0 Then
                Dim dt2 As DataTable
                Dim q As String

                q = "select bgroups.cgrpnm as Группа, bbuildings.cshort as Узел, " & _
                        " case when (CCURR =1 and DNEXTCURR <sysdate) then ceil((sysdate-DNEXTCURR)*24 *60) else 0 end as Текущие   ," & _
                        " case when (CHOUR =1 and DNEXTHOUR <sysdate)then ceil((sysdate-DNEXTHOUR)*24 *60) else 0 end as Часовые ," & _
                        " case when (C24 =1 and DNEXT24 <sysdate) then ceil((sysdate-DNEXT24)*24 *60) else 0 end as Суточные ," & _
                        " case when (CSUM =1 and DNEXTSUM <sysdate) then ceil((sysdate-DNEXTSUM )*24 *60) else 0 end as Итоговые " & _
                        " from plancall join bdevices on " & _
                        " plancall.id_bd = bdevices.id_bd join" & _
                        " bbuildings on bdevices.id_bu = bbuildings.id_bu" & _
                        " join bgroups on bbuildings.id_grp = bgroups.id_grp" & _
                        "            where(bdevices.hiderow = 0 And bgroups.hiderow = 0 And (plancall.CSTATUS = 0 Or NPQUERY = 1))" & _
                        " and (" & _
                        " (CCURR =1 and sysdate - DNEXTCURR > 15.0 /60 /24)" & _
                        " or " & _
                        " (CHOUR =1 and sysdate -DNEXTHOUR > 15.0 /60 /24)" & _
                        " or " & _
                        " (C24 =1 and sysdate - DNEXT24 > 15.0 /60 /24)" & _
                        " or" & _
                        " (CSUM =1 and sysdate -DNEXTSUM > 15.0 /60 /24)" & _
                        " ) order by bgroups.cgrpnm,bbuildings.cshort"
                Debug.Print(q)
                dt2 = TvMain.QuerySelect(q)
                If Not DataGridView2.DataSource Is Nothing Then
                    dd = DataGridView2.DataSource
                    dd.Dispose()
                End If
                DataGridView2.DataSource = dt2

            End If
            TickCount += 1
            If TickCount = 60 Then
                TickCount = 0
            End If

            inTimer = False
        End If
        '
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Timer1.Enabled = False
        TvMain.CloseDBConnection()
    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TvMain = New STKTVMain.TVMain
        If TvMain.Init() = False Then Application.Exit()
        Timer1.Enabled = True
    End Sub
End Class
