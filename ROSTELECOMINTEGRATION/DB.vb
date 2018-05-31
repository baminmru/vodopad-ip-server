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


    Public Function ExecRTIProc(ByVal ProcName As String, ByVal sdate As Date) As String


        Dim cmd As New OracleCommand()
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure

        cmd.CommandText = ProcName

        cmd.Parameters.Add("sdate", OracleDbType.Varchar2)
        cmd.Parameters.Add("edate", OracleDbType.Varchar2)
        cmd.Parameters.Item(0).Value = Format(sdate, "dd.MM.yyyy")
        cmd.Parameters.Item(1).Value = Format(sdate, "dd.MM.yyyy")

        Try
            cmd.ExecuteNonQuery()
            Return ""

        Catch ex As System.Exception


            Return ex.Message
        End Try
    End Function

   

    



    Public Function GetEnvInfo() As String
        Dim out As String
        out = "Path:" & System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\RTI_Config.xml"
        out = out & vbCrLf & connection.ConnectionString
        Return out
    End Function

    Public Function Init() As Boolean

        Dim xml As XmlDocument
        xml = New XmlDocument
        xml.Load(System.IO.Path.GetDirectoryName(Me.GetType().Assembly.Location()) + "\RTI_Config.xml")
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

        builder.DataSource = serviceName ' node.Attributes.GetNamedItem("DataSource").Value
        builder.UserID = node.Attributes.GetNamedItem("UserID").Value
        builder.Password = node.Attributes.GetNamedItem("Password").Value

        
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

    

End Class
