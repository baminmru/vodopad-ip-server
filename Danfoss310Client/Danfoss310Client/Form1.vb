Imports System
Imports System.IO.Ports
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports Modbus.Device
Imports Modbus.IO

Public Class Form1
    Private master As IModbusMaster
    Private port As SerialPort
    Private client As TcpClient
    Private slaveId As Byte = 1
    'Private Id_BD As String
    Private dt As DataTable
    'Private tvmain As STKTVMain.TVMain

    Private Sub НастройкаToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles НастройкаToolStripMenuItem.Click
        Dim f As frmSetup
        f = New frmSetup
        f.ShowDialog()
        f = Nothing
    End Sub

    Private Sub ПодключитьToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuConnect.Click
        If Not master Is Nothing Then
            master = Nothing
        End If


        If Not port Is Nothing Then
            Try
                port.Close()
                port = Nothing
            Catch ex As Exception

            End Try
        End If
        If Not client Is Nothing Then
            Try
                client.Close()

                client = Nothing
            Catch ex As Exception

            End Try
        End If
        lblStatus.Text = "подключение..."
        Dim transport As String
        transport = GetSetting("Danfoss310Client", "setup", "TRANSPORT", "IP")
        Try

            If transport = "IP" Then
                client = New TcpClient(GetSetting("Danfoss310Client", "setup", "IP", "192.168.1.100"), Integer.Parse(GetSetting("Danfoss310Client", "setup", "IPPORT", "502")))
                master = ModbusIpMaster.CreateIp(client)
            End If

            If transport = "SER2NET" Then
                client = New TcpClient(GetSetting("Danfoss310Client", "setup", "IP", "192.168.1.100"), Integer.Parse(GetSetting("Danfoss310Client", "setup", "IPPORT", "502")))
                master = ModbusSerialMaster.CreateRtu(client)
            End If

            If transport = "COM" Then
                port = New SerialPort(GetSetting("Danfoss310Client", "setup", "PORT", "COM2"))
                ' configure serial port
                Dim baud As Integer
                Try
                    baud = Integer.Parse(GetSetting("Danfoss310Client", "setup", "BAUD", "38400"))
                Catch ex As Exception
                    baud = 38400
                End Try

                port.BaudRate = baud
                port.DataBits = 8
                port.Parity = Parity.Even
                port.StopBits = StopBits.One
                port.Open()

                master = ModbusSerialMaster.CreateRtu(port)

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try
        slaveId = Integer.Parse(GetSetting("Danfoss310Client", "setup", "DevID", "1"))
      

        ' init dt
        dt = New DataTable
        Dim col As System.Data.DataColumn

        col = dt.Columns.Add("Раздел")
        col = dt.Columns.Add("Группа")
        col = dt.Columns.Add("Название")
        col = dt.Columns.Add("Единицы")
        col = dt.Columns.Add("Множитель")
        col = dt.Columns.Add("Запись")
        col = dt.Columns.Add("Значение")
        col = dt.Columns.Add("Новое значение")
        col = dt.Columns.Add("Номер параметра")
        col = dt.Columns.Add("Тип")
        col = dt.Columns.Add("Состояние")
        col = dt.Columns.Add("Примечание")
        col = dt.Columns.Add("Использовать")


        gv.DataSource = dt

        For Each c As DataGridViewTextBoxColumn In gv.Columns
            If c.Name <> "Новое значение" And c.Name <> "Использовать" Then
                c.ReadOnly = True
            End If


            If c.Name = "Название" Then
                c.MinimumWidth = 120
            End If

            If c.Name = "Примечание" Then
                c.MinimumWidth = 200
            End If


            'If c.Name = "Запись" Then
            '    c.Visible = False
            'End If
            'If c.Name = "Номер параметра" Then
            '    c.Visible = False
            'End If
            'If c.Name = "Тип" Then
            '    c.Visible = False
            'End If
        Next


        ' reading files
        ReadDeviceFile()
        ReaddAppFile()
        ReadParameters()

    End Sub



    Sub ReadDeviceFile()
        Dim filename As String
        filename = GetSetting("Danfoss310Client", "setup", "fileDev", "")
        If filename = "" Then Exit Sub
        Dim xml As System.Xml.XmlDocument
        xml = New System.Xml.XmlDocument
        xml.Load(filename)
        Dim xn As System.Xml.XmlNode
        Dim gr As System.Xml.XmlNode
        Dim pr As System.Xml.XmlNode
        Dim xnl As System.Xml.XmlNodeList
        Dim pl As System.Xml.XmlNodeList
        Dim unit As String = ""
        Dim scale As String

        Dim dr As DataRow


        xn = xml.LastChild()
        If xn.Name <> "ECL_configuration" Then Exit Sub
        xn = xn.FirstChild
        If xn.Name <> "Device_Sys" Then Exit Sub

        xnl = xn.ChildNodes ' список групп параметров
        For Each gr In xnl
            If gr.Name = "Group" Then
                pl = gr.ChildNodes  ' список параметров в группе
                For Each pr In pl
                    If pr.Name = "Param" Then
                        'Try
                        '    unit = pr.Attributes("Unit").Value
                        'Catch ex As Exception
                        '    unit = ""
                        'End Try

                        Try
                            scale = pr.Attributes("Scale").Value
                        Catch ex As Exception
                            scale = "1"
                        End Try

                        dr = dt.NewRow

                        dr("Раздел") = "Устройство"
                        dr("Группа") = gr.FirstChild.Value
                        dr("Название") = pr.FirstChild.Value
                        If scale = "1" Then
                            dr("Единицы") = unit
                        Else
                            dr("Единицы") = unit + " * " + scale
                        End If
                        dr("Запись") = pr.Attributes("Writable").Value
                        dr("Значение") = ""
                        dr("Новое значение") = ""
                        dr("Номер параметра") = pr.Attributes("PNU").Value
                        dr("Примечание") = pr.Attributes("Descrip").Value
                        dr("Тип") = pr.Attributes("Type").Value
                        dr("Состояние") = "?"
                        dr("Использовать") = "0"
                        dt.Rows.Add(dr)
                        'SaveRow(dr)
                        lblStatus.Text = "загрузка списка " + dr("Группа") + "\" + dr("Название")
                        Application.DoEvents()
                    End If
                Next
            End If

        Next
    End Sub
    Sub ReaddAppFile()
        Dim filename As String
        filename = GetSetting("Danfoss310Client", "setup", "fileApp", "")
        If filename = "" Then Exit Sub
        Dim xml As System.Xml.XmlDocument
        xml = New System.Xml.XmlDocument
        xml.Load(filename)
        Dim xn As System.Xml.XmlNode
        Dim gr As System.Xml.XmlNode
        Dim pr As System.Xml.XmlNode
        Dim xnl As System.Xml.XmlNodeList
        Dim pl As System.Xml.XmlNodeList

        Dim unit As String
        Dim scale As String

        Dim dr As DataRow


        xn = xml.LastChild()
        If xn.Name <> "ECL_configuration" Then Exit Sub
        xn = xn.FirstChild
        If xn.Name <> "Device_App" Then Exit Sub

        xnl = xn.ChildNodes ' список групп параметров
        For Each gr In xnl
            If gr.Name = "Group" Then


                pl = gr.ChildNodes  ' список параметров в группе
                For Each pr In pl
                    If pr.Name = "Param" Then
                        dr = dt.NewRow

                        Try
                            unit = pr.Attributes("Unit").Value
                        Catch ex As Exception
                            unit = ""
                        End Try

                        Try
                            scale = pr.Attributes("Scale").Value
                        Catch ex As Exception
                            scale = "1"
                        End Try

                        dr("Раздел") = "Приложение"
                        dr("Группа") = gr.FirstChild.Value
                        dr("Название") = pr.FirstChild.Value
                        If scale = "1" Then
                            dr("Единицы") = unit
                        Else
                            dr("Единицы") = unit + " * " + scale
                        End If

                        dr("Запись") = pr.Attributes("Writable").Value
                        dr("Значение") = ""
                        dr("Новое значение") = ""
                        dr("Номер параметра") = pr.Attributes("PNU").Value
                        dr("Тип") = pr.Attributes("Type").Value
                        dr("Состояние") = "?"
                        dr("Использовать") = "0"
                        dt.Rows.Add(dr)
                        'SaveRow(dr)
                        lblStatus.Text = "загрузка списка " + dr("Группа") + "\" + dr("Название")
                        Application.DoEvents()
                    End If
                Next
            End If

        Next
    End Sub



    'Sub SaveRow(ByRef dr As DataRow, Optional ByVal DropFirst As Boolean = False)
    '    Dim q As String
    '    Dim dt As DataTable

    '    If Id_BD = "" Then Exit Sub

    '    If DropFirst Then
    '        q = "delete  from DAN310CFG where id_bd=" & Id_BD & " and sPNU='" & dr("Номер параметра").ToString()

    '        Try
    '            tvmain.QueryExec(q)
    '        Catch ex As Exception

    '        End Try
    '    End If

    '    q = "select count(*) cnt  from DAN310CFG where id_bd=" & Id_BD & " and sPNU='" & dr("Номер параметра").ToString()

    '    dt = tvmain.QuerySelect(q)
    '    Dim nr As Boolean
    '    nr = False

    '    If dt.Rows.Count = 0 Then
    '        nr = True

    '    Else
    '        If dt.Rows(0)("cnt") = 0 Then
    '            nr = True
    '        End If

    '    End If



    '    If nr Then

    '        'q = "insert into DAN310CFG(id_bd,SPART,SGROUP,SPARAM,SUNIT,NSCALE,STYPE,NTYPENO,NMIN,NMAX,SDEFAULT_VAL,SDESCRIP,SWRITABLE,SPNU,SVALUE,NUSE)  values ("
    '        q = "insert into DAN310CFG(id_bd,SPART,SGROUP,SPARAM,SDESCRIP,SUNIT,STYPE,SWRITABLE,SPNU,SVALUE,NUSE)  values ("
    '        q = q & Id_BD & ",'" & dr("Раздел") & "','" & dr("Группа") & "','" & dr("Название") & "','" & dr("Примечание") & "','" & dr("Единицы") & "','" & dr("Тип") & "','" & dr("Запись") & "','" & dr("Номер параметра") & "','" & dr("Значение") & "'," & dr("Использовать").ToString & ")"

    '        Try
    '            tvmain.QueryExec(q)
    '        Catch ex As Exception

    '        End Try
    '    Else
    '        q = "update DAN310CFG set SVALUE='" & dr("Значение") & "',NUSE=" & dr("Использовать").ToString & "  where id_bd=" & Id_BD & " and sPNU='" & dr("Номер параметра").ToString()

    '        Try
    '            tvmain.QueryExec(q)
    '        Catch ex As Exception

    '        End Try
    '    End If




    'End Sub

    Sub ReadParameters()
        slaveId = Integer.Parse(GetSetting("Danfoss310Client", "setup", "DevID", "1"))
        Dim dr As DataRow
        Dim registers() As UShort
        Dim startAddress As UShort
        master.Transport.ReadTimeout = 500
        master.Transport.Retries = 3
        master.Transport.WaitToRetryMilliseconds = 500
        master.Transport.WriteTimeout = 500




        Dim iii As Integer
        For Each dr In dt.Rows
            Try
                startAddress = UShort.Parse(dr("Номер параметра").ToString())
                lblStatus.Text = "чтение " + dr("Группа") + "\" + dr("Название")

                'registers = master.ReadHoldingRegisters(slaveId, startAddress - 1, 1)
                registers = master.ReadInputRegisters(slaveId, 200 + iii, 1) 'startAddress - 1, 1)
                iii += 1

                dr("Значение") = registers(0).ToString()
                dr("Новое значение") = dr("Значение")
                dr("Состояние") = "OK"
            Catch ex As Exception
                dr("Состояние") = ex.Message
            End Try

            Application.DoEvents()
        Next
        lblStatus.Text = "чтение завершено"
    End Sub

    Private Sub cmdRefresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefresh.Click
        If Not master Is Nothing Then
            If Not dt Is Nothing Then

                ReadParameters()
            End If
        End If
    End Sub

    Private Sub ВыходToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ВыходToolStripMenuItem.Click
        master = Nothing
        Me.Close()
        End
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        If Not master Is Nothing Then
            If Not dt Is Nothing Then

                Dim dr As DataRow
                Dim startAddress As UShort
                Dim newvalue As UShort
                Dim registers() As UShort
                For Each dr In dt.Rows
                    If dr("Значение") <> dr("Новое значение") Then
                        lblStatus.Text = "запись " + dr("Группа") + "\" + dr("Название")
                        If dr("Запись").ToString.ToUpper = "TRUE" Then

                            Try
                                startAddress = UShort.Parse(dr("Номер параметра").ToString())
                                newvalue = UShort.Parse(dr("Новое значение").ToString())
                                master.WriteSingleRegister(slaveId, startAddress - 1, newvalue)
                                registers = master.ReadHoldingRegisters(slaveId, startAddress - 1, 1)
                                dr("Значение") = registers(0).ToString()
                                dr("Состояние") = "OK"

                            Catch ex As Exception
                                dr("Состояние") = ex.Message
                            End Try


                        Else
                            dr("Новое значение") = dr("Значение")
                            dr("Состояние") = "Только чтение"
                        End If
                    End If

                    If dr("Использовать") <> "0" And dr("Использовать") <> "1" Then
                        dr("Использовать") = "0"
                    End If
                    'SaveRow(dr)
                    Application.DoEvents()
                Next
                lblStatus.Text = "сохранение завершено"

            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Dim f As SplashScreen1
        f = New SplashScreen1
        f.Show()
    End Sub

  
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'tvmain = New STKTVMain.TVMain
        'tvmain.Init()

        'Id_BD = GetSetting("Danfoss310Client", "setup", "IDBD", "")
    End Sub
End Class
