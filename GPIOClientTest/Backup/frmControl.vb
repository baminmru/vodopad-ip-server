Imports System.Threading
Imports System.IO
Imports STKTVMain
Imports System.Data
Imports System.Data.OracleClient


Public Class frmControl

    Dim TVMain As STKTVMain.TVMain

    Private sp As UniTransport
    Dim PortInUse As Boolean = True

    Dim eBuf(2) As Byte
    Dim rBuf(10) As Byte
    Dim wBuf(10) As Byte
    Dim inBuf(10) As Byte

    Dim cnt As Integer
    Dim mPorts(4) As Byte
    Dim dPorts(4) As Byte

    Dim pMask(4) As Byte
    Dim t As Integer

    Dim bOpen1 As Byte = &H2
    Dim bClose1 As Byte = &H1
    Dim bOpen2 As Byte = &H8
    Dim bClose2 As Byte = &H4
    Dim bOpen3 As Byte = &H20
    Dim bClose3 As Byte = &H10
    Dim bOn As Byte = &H80
    Dim bOff As Byte = &H40




    Private Sub Init()
        eBuf(0) = Asc("e")
        rBuf(0) = Asc("r")
        wBuf(0) = Asc("w")

        mPorts(0) = &H98
        mPorts(1) = &H99
        mPorts(2) = &H9A
        mPorts(3) = &H9B

        dPorts(0) = &H78
        dPorts(1) = &H79
        dPorts(2) = &H7A
        dPorts(3) = &H7B

        pMask(0) = &HC0
        pMask(1) = &HFF
        pMask(2) = &HFF
        pMask(3) = &HFF
        PortInUse = True

    End Sub

    Private Sub DisableAll()
        txtIP.Enabled = True
        txtPort.Enabled = True
        cmdClose1.Enabled = False
        cmdOpen1.Enabled = False
        lblClose1.Enabled = False
        lblOpen1.Enabled = False

        cmdClose2.Enabled = False
        cmdOpen2.Enabled = False
        lblClose2.Enabled = False
        lblOpen2.Enabled = False

        cmdClose3.Enabled = False
        cmdOpen3.Enabled = False
        lblClose3.Enabled = False
        lblOpen3.Enabled = False
        cmdPowerOff.Enabled = False
        cmdPowerOn.Enabled = False

        cmdConnect.Enabled = True
        cmdDisconnect.Enabled = False
    End Sub


    Private Sub SetConnected()
        txtIP.Enabled = False
        txtPort.Enabled = False
        cmdClose1.Enabled = False
        cmdOpen1.Enabled = False
        lblClose1.Enabled = True
        lblOpen1.Enabled = True

        cmdClose2.Enabled = False
        cmdOpen2.Enabled = False
        lblClose2.Enabled = True
        lblOpen2.Enabled = True

        cmdClose3.Enabled = False
        cmdOpen3.Enabled = False
        lblClose3.Enabled = True
        lblOpen3.Enabled = True

        SetGray(lblClose1)
        SetGray(lblClose2)
        SetGray(lblClose3)
        SetGray(lblOpen1)
        SetGray(lblOpen2)
        SetGray(lblOpen3)

        cmdPowerOff.Enabled = False
        cmdPowerOn.Enabled = True

        cmdDisconnect.Enabled = True
        cmdConnect.Enabled = False
    End Sub

    Private Sub SetGray(ByVal lbl As Label)
        lbl.BackColor = Color.Gray
        lbl.ForeColor = Color.Gray
    End Sub

    Private Sub SetGreen(ByVal lbl As Label)
        lbl.BackColor = Color.Green
        lbl.ForeColor = Color.Green
    End Sub

    Private Sub SetPowered()
        cmdClose1.Enabled = True
        cmdOpen1.Enabled = True
        lblClose1.Enabled = True
        lblOpen1.Enabled = True

        cmdClose2.Enabled = True
        cmdOpen2.Enabled = True
        lblClose2.Enabled = True
        lblOpen2.Enabled = True

        cmdClose3.Enabled = True
        cmdOpen3.Enabled = True
        lblClose3.Enabled = True
        lblOpen3.Enabled = True


        cmdPowerOff.Enabled = True
        cmdPowerOn.Enabled = False

    End Sub

    Private Sub Processing()
        cmdClose1.Enabled = False
        cmdOpen1.Enabled = False

        cmdClose2.Enabled = False
        cmdOpen2.Enabled = False

        cmdClose3.Enabled = False
        cmdOpen3.Enabled = False
    End Sub


    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        Timer1.Enabled = False
        PortInUse = True
        Try


            Dim spd As VortexTransportSetupData
            spd = New VortexTransportSetupData
            spd.Host = txtIP.SelectedValue
            spd.Port = Val(txtPort.Text)

            Dim np As VortexTransport
            np = New VortexTransport


            np.SetupTransport(spd)
            sp = np
            np = Nothing
            If sp.Connect() Then
                SetConnected()

                ' init port mask
                For p = 0 To 1
                    Application.DoEvents()
                    wBuf(1) = mPorts(p)
                    wBuf(2) = pMask(p)
                    sp.Write(wBuf, 0, 3)

                    t = 0
                    While (sp.BytesToRead = 0 And t < 8)
                        Application.DoEvents()
                        Thread.Sleep(100)
                        t += 1
                    End While

                    If (sp.BytesToRead > 0) Then
                        cnt = sp.BytesToRead
                        If (cnt > 10) Then cnt = 10
                        sp.Read(inBuf, 0, cnt)
                    End If
                Next


                'set initial values
                Application.DoEvents()
                wBuf(1) = dPorts(1)
                wBuf(2) = 0
                sp.Write(wBuf, 0, 3)

                t = 0
                While (sp.BytesToRead = 0 And t < 8)
                    Application.DoEvents()
                    Thread.Sleep(100)
                    t += 1
                End While

                If (sp.BytesToRead > 0) Then
                    cnt = sp.BytesToRead
                    If (cnt > 10) Then cnt = 10
                    sp.Read(inBuf, 0, cnt)
                End If


                Timer1.Enabled = True
            End If
        Catch ex As Exception

        End Try
        PortInUse = False
    End Sub

    Private Sub cmdPowerOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPowerOn.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True

        Try



            wBuf(1) = dPorts(1)
            wBuf(2) = bOn
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

        Catch ex As Exception

        End Try
        PortInUse = False
        SetPowered()
    End Sub

    Private Sub cmdPowerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPowerOff.Click
        While (PortInUse)
            Application.DoEvents()
        End While

        PortInUse = True
        Try


            wBuf(1) = dPorts(1)
            wBuf(2) = bOff
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If


        Catch ex As Exception

        End Try
        SetConnected()
        PortInUse = False
    End Sub

    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click
        Timer1.Enabled = False
        While (PortInUse)
            Application.DoEvents()
        End While
        PortInUse = True
        Try

            If sp.DisConnect() Then
                DisableAll()
            End If
        Catch ex As Exception
            DisableAll()
        End Try
        PortInUse = False


    End Sub



    Private Sub frmControl_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If cmdPowerOff.Enabled = True Then
            cmdPowerOff_Click(sender, e)
        End If

        If cmdDisconnect.Enabled = True Then
            cmdDisconnect_Click(sender, e)
        End If
        End
    End Sub
    Private OnLoadForm As Boolean = False

    Private Function QuerySelect(ByVal s As String) As DataTable
        Dim cmd As OracleCommand
        cmd = New OracleCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = s
        cmd.Connection = tvmain.dbconnect
        Dim dt As DataTable
        Dim da As OracleDataAdapter
        dt = New DataTable
        da = New OracleDataAdapter
        da.SelectCommand = cmd
        da.Fill(dt)
        Return dt
    End Function


    Private Sub frmControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OnLoadForm = True

        TVMain = New STKTVMain.TVMain
        TVMain.Init()


        Dim dt As DataTable
        dt = QuerySelect("select CSHORT,NPIP from  bbuildings b join bdevices d on b.id_bu= d.id_bu where transport=3")

        txtIP.DisplayMember = "CSHORT"
        txtIP.ValueMember = "NPIP"
        txtIP.DataSource = dt


        OnLoadForm = False
        Init()
        DisableAll()
    End Sub

    Private Sub cmdOpen1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen1.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True
        Try
            wBuf(1) = dPorts(1)
            wBuf(2) = bOpen1
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)


            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

        Catch ex As Exception

        End Try
        PortInUse = False
        SetPowered()
    End Sub

    Private Sub cmdClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose1.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True

        Try

            wBuf(1) = dPorts(1)
            wBuf(2) = bClose1
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

        Catch ex As Exception

        End Try
        PortInUse = False
        SetPowered()
    End Sub

    Private Sub cmdOpen2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen2.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True

        Try


            wBuf(1) = dPorts(1)
            wBuf(2) = bOpen2
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If


            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If


        Catch ex As Exception

        End Try
        PortInUse = False
        SetPowered()
    End Sub

    Private Sub cmdClose2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose2.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True

        Try
            wBuf(1) = dPorts(1)
            wBuf(2) = bClose2
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

        Catch ex As Exception

        End Try

        PortInUse = False
        SetPowered()
    End Sub

    Private Sub cmdOpen3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen3.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True

        Try
            wBuf(1) = dPorts(1)
            wBuf(2) = bOpen3
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

        Catch ex As Exception

        End Try
        PortInUse = False
        SetPowered()
    End Sub

    Private Sub cmdClose3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose3.Click
        While (PortInUse)
            Application.DoEvents()
        End While
        Processing()
        PortInUse = True

        Try


            wBuf(1) = dPorts(1)
            wBuf(2) = bClose3
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

            Thread.Sleep(1000)

            wBuf(1) = dPorts(1)
            wBuf(2) = 0
            sp.Write(wBuf, 0, 3)

            t = 0
            While (sp.BytesToRead = 0 And t < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                t += 1
            End While

            If (sp.BytesToRead > 0) Then
                cnt = sp.BytesToRead
                If (cnt > 10) Then cnt = 10
                sp.Read(inBuf, 0, cnt)
            End If

        Catch ex As Exception

        End Try
        PortInUse = False
        SetPowered()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If PortInUse Then Exit Sub
        PortInUse = True
        Try


            rBuf(1) = dPorts(0)
            rBuf(2) = 0
            sp.Write(rBuf, 0, 3)
            Dim tt As Integer
            Dim tcnt As Integer
            Dim tinbuf(10) As Byte
            tt = 0
            While (sp.BytesToRead = 0 And tt < 8)
                Application.DoEvents()
                Thread.Sleep(100)
                tt += 1
            End While

            If (sp.BytesToRead > 0) Then
                tcnt = sp.BytesToRead
                If (tcnt > 10) Then tcnt = 10
                sp.Read(tinbuf, 0, tcnt)

                tinbuf(1) = (tinbuf(1) And &H3F)
                ' For i As Integer = 0 To tcnt - 1
                If (tinbuf(1) And 1) Then
                    SetGreen(lblOpen1)
                Else
                    SetGray(lblOpen1)
                End If
                If (tinbuf(1) And 2) Then
                    SetGreen(lblClose1)
                Else
                    SetGray(lblClose1)
                End If
                If (tinbuf(1) And 4) Then
                    SetGreen(lblOpen2)
                Else
                    SetGray(lblOpen2)
                End If
                If (tinbuf(1) And 8) Then
                    SetGreen(lblClose2)
                Else
                    SetGray(lblClose2)
                End If
                If (tinbuf(1) And 16) Then
                    SetGreen(lblOpen3)
                Else
                    SetGray(lblOpen3)
                End If
                If (tinbuf(1) And 32) Then
                    SetGreen(lblClose3)
                Else
                    SetGray(lblClose3)
                End If
                ' Next


            End If
        Catch ex As Exception

        End Try
        PortInUse = False
    End Sub

    Private Sub txtIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtIP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIP.SelectedIndexChanged
        If OnLoadForm Then Exit Sub
        SaveSetting("GPIOClient", "IP", "LASTIP", txtIP.Text)
    End Sub
End Class