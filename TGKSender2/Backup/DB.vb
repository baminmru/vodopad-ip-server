Imports System
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Data
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class DB


    Dim connection As OracleConnection
    Dim command As OracleCommand
    Dim m_RequestInterval As Integer
    Dim m_DataSourceName As String

    Dim m_FileDirectory As String
    'Dim m_VPNGUIPATH As String
    'Dim m_VPNGUIARGS As String
    'Dim m_TEST_VPN_IP As String
    'Dim m_VPNGUINAME As String
    'Dim m_TEST_VPN_PORT As Integer


    'Public Property TEST_VPN_PORT() As Integer
    '    Get
    '        Return m_TEST_VPN_PORT
    '    End Get
    '    Set(ByVal value As Integer)
    '        m_TEST_VPN_PORT = value
    '    End Set
    'End Property

    'Public Property TEST_VPN_IP() As String
    '    Get
    '        Return m_TEST_VPN_IP
    '    End Get
    '    Set(ByVal value As String)
    '        m_TEST_VPN_IP = value
    '    End Set
    'End Property

    'Public Property VPNGUIARGS() As String
    '    Get
    '        Return m_VPNGUIARGS
    '    End Get
    '    Set(ByVal value As String)
    '        m_VPNGUIARGS = value
    '    End Set
    'End Property

    'Public Property VPNGUINAME() As String
    '    Get
    '        Return m_VPNGUINAME
    '    End Get
    '    Set(ByVal value As String)
    '        m_VPNGUINAME = value
    '    End Set
    'End Property
    'Public Property VPNGUIPATH() As String
    '    Get
    '        Return m_VPNGUIPATH
    '    End Get
    '    Set(ByVal value As String)
    '        m_VPNGUIPATH = value
    '    End Set
    'End Property
    Public Property FileDirectory() As String
        Get
            Return m_FileDirectory
        End Get
        Set(ByVal value As String)
            m_FileDirectory = value
        End Set
    End Property

    Public Property DataSourceName() As String
        Get
            Return m_DataSourceName
        End Get
        Set(ByVal value As String)
            m_DataSourceName = value
        End Set
    End Property

    Public Function dbconnect() As OracleConnection
        Return connection
    End Function






    Public Function CheckForArch(ByVal ArchType As Int32, ByVal ArchYear As Int32, _
    ByVal ArchMonth As Int32, ByVal ArchDay As Int32, ByVal ArchHour As Int32, ByVal id_bd As Int32) As Boolean
        Dim cmd As New OracleCommand()
        Dim datearch As DateTime
        Dim after As Date, befor As Date
        datearch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)



        after = datearch.AddSeconds(-1)
        befor = datearch.AddSeconds(1)
        cmd.Connection = connection
        cmd.CommandText = "select count(*) CNT  from datacurr where dcounter>=" + _
        "to_date('" + after.Year.ToString() + "-" + after.Month.ToString() + "-" + after.Day.ToString() + _
        " " + after.Hour.ToString() + ":" + after.Minute.ToString() + ":" + after.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS') and dcounter<=" + _
        "to_date('" + befor.Year.ToString() + "-" + befor.Month.ToString() + "-" + befor.Day.ToString() + _
        " " + befor.Hour.ToString() + ":" + befor.Minute.ToString() + ":" + befor.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS') and id_ptype=" + ArchType.ToString() + _
        "and id_bd=" + id_bd.ToString()

        Dim rs As OracleDataReader = Nothing
        Try
            SyncLock connection
                cmd.Connection = connection
                rs = cmd.ExecuteReader()
            End SyncLock
            If Not rs Is Nothing Then
                If rs.HasRows Then
                    rs.Read()
                    If rs.Item("CNT") > 0 Then
                        rs.Close()
                        Return True
                    End If
                Else
                    rs.Close()
                End If
            End If


        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        Return False
    End Function










    Public Function LockDevice(ByVal DevID As Integer, ByVal LockSeconds As Integer) As Boolean
        'Dim da As System.Data.Common.DbDataAdapter
        Dim da As OracleDataAdapter
        Dim dt As DataTable
        da = New OracleDataAdapter
        Dim cmd As New OracleCommand()
        cmd.Connection = connection


        cmd.CommandText = "select npip,sysdate ServerDate from bdevices  where bdevices.id_dev in (3,7,12) and ( nplock is null or nplock <sysdate ) and  not npip is null and npquery=1  and bdevices.id_bd=" & DevID.ToString()

        da.SelectCommand = cmd
        dt = New DataTable
        Try
            SyncLock connection
                da.Fill(dt)
            End SyncLock
        Catch
        End Try

        If dt.Rows.Count > 0 Then
            cmd.CommandText = "update bdevices set nplock =sysdate + (0.00001*" + LockSeconds.ToString + ") where bdevices.id_bd=" + DevID.ToString()
            cmd.ExecuteNonQuery()
            Return True
        End If
        Return False
    End Function

    Public Function UnLockDevice(ByVal DevID As Integer) As Boolean

        Dim da As OracleDataAdapter
        Dim dt As DataTable
        da = New OracleDataAdapter

        Dim cmd As New OracleCommand()
        cmd.Connection = connection


        cmd.CommandText = "select npip,sysdate ServerDate from bdevices  where bdevices.id_dev in (3,7,12) and ( nplock >=sysdate ) and  not npip is null and npquery=1  and bdevices.id_bd=" + DevID.ToString()

        da.SelectCommand = cmd
        dt = New DataTable
        Try
            SyncLock connection
                da.Fill(dt)
            End SyncLock
        Catch
        End Try

        If dt.Rows.Count > 0 Then
            cmd.CommandText = "update bdevices set nplock = null where bdevices.id_bd=" + DevID.ToString()
            cmd.ExecuteNonQuery()
            Return True
        End If
        Return False
    End Function



    Public Function GetDBDateTime() As DateTime
        'Dim da As System.Data.Common.DbDataAdapter
        Dim da As OracleDataAdapter
        Dim dt As DataTable
        da = New OracleDataAdapter

        Dim cmd As New OracleCommand()
        cmd.Connection = connection
        'cmd.CommandText = "select * from plancall where cstatus=0"
        cmd.CommandText = "select sysdate d from dual"

        da.SelectCommand = cmd
        dt = New DataTable
        Try
            SyncLock connection
                da.Fill(dt)
            End SyncLock


            Return Convert.ToDateTime(dt.Rows(0)("d"))
        Catch
            Return System.DateTime.Now
        End Try

    End Function


    Public Function ExecTGK1Proc(ByVal id_bd As String, ByVal ProcName As String, ByVal devName As String, ByVal sdate As Date, ByVal edate As Date) As String
        Dim cmd1 As New OracleCommand()
        Dim sdat As Object
        cmd1.Connection = connection
        cmd1.CommandType = CommandType.Text

        cmd1.CommandText = "select to_char(sysdate-1,'DD-MON') from dual"

        sdat = cmd1.ExecuteScalar()


        Dim cmd As New OracleCommand()
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure

        cmd.CommandText = ProcName
        'p IN varchar2,sdate IN varchar2,edate IN varchar2
        cmd.Parameters.Add("p", OracleDbType.VarChar2)
        cmd.Parameters.Add("sdate", OracleDbType.VarChar2)
        cmd.Parameters.Add("edate", OracleDbType.VarChar2)

        cmd.Parameters.Item(0).Value = devName
        If edate < sdate Then
            cmd.Parameters.Item(1).Value = Format(DateAdd(DateInterval.Hour, -2, edate), "dd.MM.yyyy HH:mm:ss")
            cmd.Parameters.Item(2).Value = Format(DateAdd(DateInterval.Hour, +2, edate), "dd.MM.yyyy HH:mm:ss")
            SaveMissing(id_bd, edate, devName)
        Else
            cmd.Parameters.Item(1).Value = Format(sdate, "dd.MM.yyyy HH:mm:ss")
            cmd.Parameters.Item(2).Value = Format(edate, "dd.MM.yyyy HH:mm:ss")

        End If

        Try
            cmd.ExecuteNonQuery()

            Return sdat.ToString() & "_" & devName & ".txt"
        Catch ex As System.Exception


            Return ""
        End Try
    End Function

    Public Sub SaveMissing(ByVal ID As Integer, ByVal Archdate As Date, ByVal Devname As String)
        Dim cmd1 As New OracleCommand()
        cmd1.Connection = connection
        cmd1.CommandType = CommandType.Text

        cmd1.CommandText = "insert into MISSINGARCH(id_bd,archdate,devname) values(" & ID.ToString() & ",to_date('" & Format(Archdate, "dd.MM.yyyy HH:mm") & "','dd.mm.yyyy hh24:mi'),'" & Devname & "')"
        Try
            cmd1.ExecuteNonQuery()
        Catch
        End Try

    End Sub
    Public Function ExecTGK1Proc2(ByVal ProcName As String, ByVal devName As String, ByVal sdate As String, ByVal edate As String) As String
        Dim cmd1 As New OracleCommand()
        Dim sdat As Object
        cmd1.Connection = connection
        cmd1.CommandType = CommandType.Text

        cmd1.CommandText = "select to_char(sysdate-1,'DD-MON') from dual"

        sdat = cmd1.ExecuteScalar()


        Dim cmd As New OracleCommand()
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure

        cmd.CommandText = ProcName
        'p IN varchar2,sdate IN varchar2,edate IN varchar2
        cmd.Parameters.Add("p", OracleDbType.Varchar2)
        cmd.Parameters.Add("sdate", OracleDbType.Varchar2)
        cmd.Parameters.Add("edate", OracleDbType.Varchar2)

        cmd.Parameters.Item(0).Value = devName
        cmd.Parameters.Item(1).Value = sdate
        cmd.Parameters.Item(2).Value = edate

        Try
            cmd.ExecuteNonQuery()

            Return sdat.ToString() & "_" & devName & ".txt"
        Catch ex As System.Exception


            Return ""
        End Try
    End Function


    Public Function CheckDeviceAtTGK(ByVal EQType As String, ByVal SerNum As String, ByRef sDate As Date, ByRef eDate As Date) As Boolean

        Dim psEqType As String
        Dim psSerNumber As String
        Dim piDayStart As Short
        Dim piMonStart As Short
        Dim piYearStart As Short
        Dim piHourStart As Short
        Dim piMinStart As Short
        Dim piDayFin As Short
        Dim piMonFin As Short
        Dim piYearFin As Short
        Dim piHourFin As Short
        Dim piMinFin As Short
        Dim iArcType As Short
        Dim result As Short

        psEqType = EQType
        psSerNumber = SerNum
        piDayStart = sDate.Day
        piMonStart = Month(sDate)
        piYearStart = Year(sDate)
        piHourStart = sDate.Hour
        piMinStart = sDate.Minute
        piDayFin = eDate.Day
        piMonFin = Month(eDate)
        piYearFin = Year(eDate)
        piHourFin = Hour(eDate)
        piMinFin = Minute(eDate)
        iArcType = 0

        Dim pute As New puteclient.PuteClient()


        result = pute.COMGetInterval(psEqType, psSerNumber, piDayStart, piMonStart, piYearStart, piHourStart, piMinStart, piDayFin, piMonFin, piYearFin, piHourFin, piMinFin, iArcType)

        If result = 0 Then
            sDate = New Date(piYearStart, piMonStart, piDayStart, piHourStart, piMinStart, 0)
            eDate = New Date(piYearFin, piMonFin, piDayFin, piHourFin, piMinFin, 0)
            Return True
        Else
            Return False
        End If

    End Function

    'Public Function GetMISSING() As DataTable
    '    Dim dt As DataTable = Nothing
    '    Dim da As OracleDataAdapter
    '    Dim cmd As New OracleCommand()
    '    cmd.Connection = connection
    '    cmd.CommandText = "select * from MISSINGARCH"

    '    da = New OracleDataAdapter(cmd)
    '    dt = New DataTable
    '    Try
    '        da.Fill(dt)
    '    Catch
    '    End Try
    '    Return dt
    'End Function


    'Public Function DeleteMISSING(ByVal id_bd As Integer, ByVal archdate As Date) As Boolean

    '    Dim cmd As New OracleCommand()
    '    cmd.Connection = connection
    '    cmd.CommandText = "delete from MISSINGARCH where id_bd = " + id_bd.ToString() + "  and archdate =to_date('" + Format(archdate, "dd.MM.yyyy HH:mm:ss") + "','DD.MM.YYYY hh24:mi:ss')"
    '    Try
    '        cmd.ExecuteNonQuery()
    '    Catch
    '        Return False
    '    End Try
    '    Return True
    'End Function

    Public Function GetTGK1Dev() As DataTable
        Dim dt As DataTable = Nothing
        Dim da As OracleDataAdapter
        Dim cmd As New OracleCommand()
        cmd.Connection = connection
        cmd.CommandText = "select * from v_devshema where fld20 like '%ТГК-1%' order by cshort"

        da = New OracleDataAdapter(cmd)
        dt = New DataTable
        Try
            da.Fill(dt)
        Catch
        End Try

        Return dt
    End Function



    Public Function GetEnvInfo() As String
        Dim out As String
        out = "Path:" & System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\TGKSender_Config.xml"
        out = out & vbCrLf & connection.ConnectionString
        Return out
    End Function
    Public Function Init() As Boolean

        Dim xml As XmlDocument
        xml = New XmlDocument
        xml.Load(System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\TGKSender_Config.xml")
        Dim node As XmlElement
        node = xml.FirstChild()

        Dim builder As New OracleConnectionStringBuilder
        DataSourceName = node.Attributes.GetNamedItem("DataSource").Value
        FileDirectory = node.Attributes.GetNamedItem("FileDirectory").Value
        Dim serviceName As String = ""
        Try
            serviceName = node.Attributes.GetNamedItem("Oracle").Value

        Catch ex As Exception
            serviceName = "//192.168.9.35:1521/ora11ape.astrum.local"
        End Try
        builder.DataSource = serviceName 'node.Attributes.GetNamedItem("DataSource").Value
        builder.UserID = node.Attributes.GetNamedItem("UserID").Value
        builder.Password = node.Attributes.GetNamedItem("Password").Value

        'VPNGUIPATH = node.Attributes.GetNamedItem("VPNGUIPATH").Value
        'VPNGUIARGS = node.Attributes.GetNamedItem("VPNGUIARGS").Value
        'VPNGUINAME = node.Attributes.GetNamedItem("VPNGUINAME").Value

        'TEST_VPN_IP = node.Attributes.GetNamedItem("TEST_VPN_IP").Value
        'TEST_VPN_PORT = Val("0" & node.Attributes.GetNamedItem("TEST_VPN_PORT").Value)



        connection = New OracleConnection()
        command = New OracleCommand()

        connection.ConnectionString = builder.ConnectionString
        command.Connection = connection

        Try
            ' SyncLock connection
            connection.Open()
            ' End SyncLock
            If connection.State <> ConnectionState.Open Then
                Console.WriteLine("connection error")
                Return False
            End If
            'm_RequestInterval = Convert.ToDouble(node.Attributes.GetNamedItem("RequestInterval").Value)
            node = Nothing
            xml = Nothing
        Catch ex As Exception

            Console.WriteLine(ex.Message)
        End Try


        Return True
    End Function
    Public Sub EndWork()

        Try
            command.Dispose()
            connection.Close()
            connection.Dispose()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        connection = Nothing
        command = Nothing


    End Sub

    Public Sub SetTimeToPlanCall(ByVal id_bd As String, ByVal FieldName As String, ByVal time As DateTime)
        command.CommandText = "update PlanCall set " + FieldName + _
        "=" + "to_date('" + time.Year.ToString() + "-" + _
        time.Month.ToString() + "-" + time.Day.ToString() + " " + _
        time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString() + _
        "','YYYY-MM-DD HH24:MI:SS') where id_bd=" + id_bd.ToString
        Try
            ' SyncLock connection
            If connection.State = ConnectionState.Open Then
                command.ExecuteNonQuery()
            End If
            ' End SyncLock
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub


End Class
