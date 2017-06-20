

Public Class EditAnalizerConfig

    Private Sub k0_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs)

        If k0.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(k0.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(k0.Text) < -2000000000 Or Val(k0.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If

    End Sub

    Private Sub k1_TextChanged(sender As System.Object, e As System.EventArgs) Handles k0.TextChanged

    End Sub

    Private Sub k1_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles k0.Validating, k1.Validating
        If k0.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(k0.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(k0.Text) < -2000000000 Or Val(k0.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub

    Private Sub k2_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles k2.Validating
        If k2.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(k2.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(k2.Text) < -2000000000 Or Val(k2.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub

    Private Sub k3_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles k3.Validating
        If k3.Text <> "" Then
            On Error Resume Next
            If Not IsNumeric(k3.Text) Then
                e.Cancel = True
                MsgBox("Ожидалось число", vbOKOnly + vbExclamation, "Внимание")
            ElseIf Val(k3.Text) < -2000000000 Or Val(k3.Text) > 2000000000 Then
                e.Cancel = True
                MsgBox("Значение вне допустимого диапазона", vbOKOnly + vbExclamation, "Внимание")
            End If
        End If
    End Sub


    Public Item As DataRow
    Private mRowReadOnly As Boolean
    Private mOnInit As Boolean


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
        On Error Resume Next
        If Item("ANALIZENODE") = 1 Then chkANALIZENODE.Checked = True Else chkANALIZENODE.Checked = False
        If Item("OPENSYSTEM") = 1 Then chkOpenSystem.Checked = True Else chkOpenSystem.Checked = False
        If Item("SEZON") = 0 Then chkSEZON.Checked = True Else chkSEZON.Checked = False
        On Error Resume Next
        If chkSEZON.Checked Then
            T1.Text = Item("T1").ToString()
            T2.Text = Item("T2").ToString()
            T3.Text = Item("T3").ToString()
            T4.Text = Item("T4").ToString()
            T5.Text = Item("T5").ToString()
            T6.Text = Item("T6").ToString()
            G1.Text = Item("G1").ToString()
            G2.Text = Item("G2").ToString()
            G3.Text = Item("G3").ToString()
            G4.Text = Item("G4").ToString()
            G5.Text = Item("G5").ToString()
            G6.Text = Item("G6").ToString()


            V1.Text = Item("V1").ToString()
            V2.Text = Item("V2").ToString()
            V3.Text = Item("V3").ToString()
            V4.Text = Item("V4").ToString()
            V5.Text = Item("V5").ToString()
            V6.Text = Item("V6").ToString()


            M1.Text = Item("M1").ToString()
            M2.Text = Item("M2").ToString()
            M3.Text = Item("M3").ToString()
            M4.Text = Item("M4").ToString()
            M5.Text = Item("M5").ToString()
            M6.Text = Item("M6").ToString()

            p1.Text = Item("P1").ToString()
            p2.Text = Item("P2").ToString()
            p3.Text = Item("P3").ToString()
            p4.Text = Item("P4").ToString()
            p5.Text = Item("P5").ToString()
            p6.Text = Item("P6").ToString()


            M_G1.Text = Item("M_G1").ToString()
            M_G2.Text = Item("M_G2").ToString()
            M_G3.Text = Item("M_G3").ToString()
            M_G4.Text = Item("M_G4").ToString()
            M_G5.Text = Item("M_G5").ToString()
            M_G6.Text = Item("M_G6").ToString()


            M_V1.Text = Item("M_V1").ToString()
            M_V2.Text = Item("M_V2").ToString()
            M_V3.Text = Item("M_V3").ToString()
            M_V4.Text = Item("M_V4").ToString()
            M_V5.Text = Item("M_V5").ToString()
            M_V6.Text = Item("M_V6").ToString()


            M_M1.Text = Item("M_M1").ToString()
            M_M2.Text = Item("M_M2").ToString()
            M_M3.Text = Item("M_M3").ToString()
            M_M4.Text = Item("M_M4").ToString()
            M_M5.Text = Item("M_M5").ToString()
            M_M6.Text = Item("M_M6").ToString()

            M_P1.Text = Item("M_P1").ToString()
            M_P2.Text = Item("M_P2").ToString()
            M_P3.Text = Item("M_P3").ToString()
            M_P4.Text = Item("M_P4").ToString()
            M_P5.Text = Item("M_P5").ToString()
            M_P6.Text = Item("M_P6").ToString()
        Else
            T1.Text = Item("L_T1").ToString()
            T2.Text = Item("L_T2").ToString()
            T3.Text = Item("L_T3").ToString()
            T4.Text = Item("L_T4").ToString()
            T5.Text = Item("L_T5").ToString()
            T6.Text = Item("L_T6").ToString()
            G1.Text = Item("L_G1").ToString()
            G2.Text = Item("L_G2").ToString()
            G3.Text = Item("L_G3").ToString()
            G4.Text = Item("L_G4").ToString()
            G5.Text = Item("L_G5").ToString()
            G6.Text = Item("L_G6").ToString()


            V1.Text = Item("L_V1").ToString()
            V2.Text = Item("L_V2").ToString()
            V3.Text = Item("L_V3").ToString()
            V4.Text = Item("L_V4").ToString()
            V5.Text = Item("L_V5").ToString()
            V6.Text = Item("L_V6").ToString()


            M1.Text = Item("L_M1").ToString()
            M2.Text = Item("L_M2").ToString()
            M3.Text = Item("L_M3").ToString()
            M4.Text = Item("L_M4").ToString()
            M5.Text = Item("L_M5").ToString()
            M6.Text = Item("L_M6").ToString()

            p1.Text = Item("L_P1").ToString()
            p2.Text = Item("L_P2").ToString()
            p3.Text = Item("L_P3").ToString()
            p4.Text = Item("L_P4").ToString()
            p5.Text = Item("L_P5").ToString()
            p6.Text = Item("L_P6").ToString()


            M_G1.Text = Item("L_M_G1").ToString()
            M_G2.Text = Item("L_M_G2").ToString()
            M_G3.Text = Item("L_M_G3").ToString()
            M_G4.Text = Item("L_M_G4").ToString()
            M_G5.Text = Item("L_M_G5").ToString()
            M_G6.Text = Item("L_M_G6").ToString()


            M_V1.Text = Item("L_M_V1").ToString()
            M_V2.Text = Item("L_M_V2").ToString()
            M_V3.Text = Item("L_M_V3").ToString()
            M_V4.Text = Item("L_M_V4").ToString()
            M_V5.Text = Item("L_M_V5").ToString()
            M_V6.Text = Item("L_M_V6").ToString()


            M_M1.Text = Item("L_M_M1").ToString()
            M_M2.Text = Item("L_M_M2").ToString()
            M_M3.Text = Item("L_M_M3").ToString()
            M_M4.Text = Item("L_M_M4").ToString()
            M_M5.Text = Item("L_M_M5").ToString()
            M_M6.Text = Item("L_M_M6").ToString()

            M_P1.Text = Item("L_M_P1").ToString()
            M_P2.Text = Item("L_M_P2").ToString()
            M_P3.Text = Item("L_M_P3").ToString()
            M_P4.Text = Item("L_M_P4").ToString()
            M_P5.Text = Item("L_M_P5").ToString()
            M_P6.Text = Item("L_M_P6").ToString()

        End If


        k0.Text = Item("k0").ToString
        k1.Text = Item("k1").ToString
        k2.Text = Item("k2").ToString
        k3.Text = Item("k3").ToString

        lk0.Text = Item("lk0").ToString
        lk1.Text = Item("lk1").ToString
        lk2.Text = Item("lk2").ToString
        lk3.Text = Item("lk3").ToString

        T_METHOD.Text = Item("T_METHOD").ToString
        mOnInit = False

    End Sub

    Public Sub Save()
        If mRowReadOnly = False Then
            If chkANALIZENODE.Checked = True Then Item("ANALIZENODE") = 1 Else Item("ANALIZENODE") = 0
            If chkOpenSystem.Checked = True Then Item("OPENSYSTEM") = 1 Else Item("OPENSYSTEM") = 0
            If chkSEZON.Checked = True Then Item("sezon") = 0 Else Item("sezon") = 1
            If chkSEZON.Checked Then
                Item("T1") = 0
                Item("T2") = 0
                Item("T3") = 0
                Item("T4") = 0
                Item("T5") = 0
                Item("T6") = 0
                Item("G1") = 0
                Item("G2") = 0
                Item("G3") = 0
                Item("G4") = 0
                Item("G5") = 0
                Item("G6") = 0
                Item("M1") = 0
                Item("M2") = 0
                Item("M3") = 0
                Item("M4") = 0
                Item("M5") = 0
                Item("M6") = 0
                Item("V1") = 0
                Item("V2") = 0
                Item("V3") = 0
                Item("V4") = 0
                Item("V5") = 0
                Item("V6") = 0
                Item("P1") = 0
                Item("P2") = 0
                Item("P3") = 0
                Item("P4") = 0
                Item("P5") = 0
                Item("P6") = 0

                On Error Resume Next
                Item("T1") = Integer.Parse(T1.Text)
                Item("T2") = Integer.Parse(T2.Text)
                Item("T3") = Integer.Parse(T3.Text)
                Item("T4") = Integer.Parse(T4.Text)
                Item("T5") = Integer.Parse(T5.Text)
                Item("T6") = Integer.Parse(T6.Text)

                Item("G1") = Integer.Parse(G1.Text)
                Item("G2") = Integer.Parse(G2.Text)
                Item("G3") = Integer.Parse(G3.Text)
                Item("G4") = Integer.Parse(G4.Text)
                Item("G5") = Integer.Parse(G5.Text)
                Item("G6") = Integer.Parse(G6.Text)

                Item("V1") = Integer.Parse(V1.Text)
                Item("V2") = Integer.Parse(V2.Text)
                Item("V3") = Integer.Parse(V3.Text)
                Item("V4") = Integer.Parse(V4.Text)
                Item("V5") = Integer.Parse(V5.Text)
                Item("V6") = Integer.Parse(V6.Text)

                Item("M1") = Integer.Parse(M1.Text)
                Item("M2") = Integer.Parse(M2.Text)
                Item("M3") = Integer.Parse(M3.Text)
                Item("M4") = Integer.Parse(M4.Text)
                Item("M5") = Integer.Parse(M5.Text)
                Item("M6") = Integer.Parse(M6.Text)

                Item("P1") = Integer.Parse(p1.Text)
                Item("P2") = Integer.Parse(p2.Text)
                Item("P3") = Integer.Parse(p3.Text)
                Item("P4") = Integer.Parse(p4.Text)
                Item("P5") = Integer.Parse(p5.Text)
                Item("P6") = Integer.Parse(p6.Text)


                Item("M_G1") = Integer.Parse(M_G1.Text)
                Item("M_G2") = Integer.Parse(M_G2.Text)
                Item("M_G3") = Integer.Parse(M_G3.Text)
                Item("M_G4") = Integer.Parse(M_G4.Text)
                Item("M_G5") = Integer.Parse(M_G5.Text)
                Item("M_G6") = Integer.Parse(M_G6.Text)

                Item("M_V1") = Integer.Parse(M_V1.Text)
                Item("M_V2") = Integer.Parse(M_V2.Text)
                Item("M_V3") = Integer.Parse(M_V3.Text)
                Item("M_V4") = Integer.Parse(M_V4.Text)
                Item("M_V5") = Integer.Parse(M_V5.Text)
                Item("M_V6") = Integer.Parse(M_V6.Text)

                Item("M_M1") = Integer.Parse(M_M1.Text)
                Item("M_M2") = Integer.Parse(M_M2.Text)
                Item("M_M3") = Integer.Parse(M_M3.Text)
                Item("M_M4") = Integer.Parse(M_M4.Text)
                Item("M_M5") = Integer.Parse(M_M5.Text)
                Item("M_M6") = Integer.Parse(M_M6.Text)

                Item("M_P1") = Integer.Parse(M_P1.Text)
                Item("M_P2") = Integer.Parse(M_P2.Text)
                Item("M_P3") = Integer.Parse(M_P3.Text)
                Item("M_P4") = Integer.Parse(M_P4.Text)
                Item("M_P5") = Integer.Parse(M_P5.Text)
                Item("M_P6") = Integer.Parse(M_P6.Text)

            Else
                Item("L_T1") = 0
                Item("L_T2") = 0
                Item("L_T3") = 0
                Item("L_T4") = 0
                Item("L_T5") = 0
                Item("L_T6") = 0
                Item("L_G1") = 0
                Item("L_G2") = 0
                Item("L_G3") = 0
                Item("L_G4") = 0
                Item("L_G5") = 0
                Item("L_G6") = 0
                Item("L_M1") = 0
                Item("L_M2") = 0
                Item("L_M3") = 0
                Item("L_M4") = 0
                Item("L_M5") = 0
                Item("L_M6") = 0
                Item("L_V1") = 0
                Item("L_V2") = 0
                Item("L_V3") = 0
                Item("L_V4") = 0
                Item("L_V5") = 0
                Item("L_V6") = 0
                Item("L_P1") = 0
                Item("L_P2") = 0
                Item("L_P3") = 0
                Item("L_P4") = 0
                Item("L_P5") = 0
                Item("L_P6") = 0

                On Error Resume Next
                Item("L_T1") = Integer.Parse(T1.Text)
                Item("L_T2") = Integer.Parse(T2.Text)
                Item("L_T3") = Integer.Parse(T3.Text)
                Item("L_T4") = Integer.Parse(T4.Text)
                Item("L_T5") = Integer.Parse(T5.Text)
                Item("L_T6") = Integer.Parse(T6.Text)

                Item("L_G1") = Integer.Parse(G1.Text)
                Item("L_G2") = Integer.Parse(G2.Text)
                Item("L_G3") = Integer.Parse(G3.Text)
                Item("L_G4") = Integer.Parse(G4.Text)
                Item("L_G5") = Integer.Parse(G5.Text)
                Item("L_G6") = Integer.Parse(G6.Text)

                Item("L_V1") = Integer.Parse(V1.Text)
                Item("L_V2") = Integer.Parse(V2.Text)
                Item("L_V3") = Integer.Parse(V3.Text)
                Item("L_V4") = Integer.Parse(V4.Text)
                Item("L_V5") = Integer.Parse(V5.Text)
                Item("L_V6") = Integer.Parse(V6.Text)

                Item("L_M1") = Integer.Parse(M1.Text)
                Item("L_M2") = Integer.Parse(M2.Text)
                Item("L_M3") = Integer.Parse(M3.Text)
                Item("L_M4") = Integer.Parse(M4.Text)
                Item("L_M5") = Integer.Parse(M5.Text)
                Item("L_M6") = Integer.Parse(M6.Text)

                Item("L_P1") = Integer.Parse(p1.Text)
                Item("L_P2") = Integer.Parse(p2.Text)
                Item("L_P3") = Integer.Parse(p3.Text)
                Item("L_P4") = Integer.Parse(p4.Text)
                Item("L_P5") = Integer.Parse(p5.Text)
                Item("L_P6") = Integer.Parse(p6.Text)


                Item("L_M_G1") = Integer.Parse(M_G1.Text)
                Item("L_M_G2") = Integer.Parse(M_G2.Text)
                Item("L_M_G3") = Integer.Parse(M_G3.Text)
                Item("L_M_G4") = Integer.Parse(M_G4.Text)
                Item("L_M_G5") = Integer.Parse(M_G5.Text)
                Item("L_M_G6") = Integer.Parse(M_G6.Text)

                Item("L_M_V1") = Integer.Parse(M_V1.Text)
                Item("L_M_V2") = Integer.Parse(M_V2.Text)
                Item("L_M_V3") = Integer.Parse(M_V3.Text)
                Item("L_M_V4") = Integer.Parse(M_V4.Text)
                Item("L_M_V5") = Integer.Parse(M_V5.Text)
                Item("L_M_V6") = Integer.Parse(M_V6.Text)

                Item("L_M_M1") = Integer.Parse(M_M1.Text)
                Item("L_M_M2") = Integer.Parse(M_M2.Text)
                Item("L_M_M3") = Integer.Parse(M_M3.Text)
                Item("L_M_M4") = Integer.Parse(M_M4.Text)
                Item("L_M_M5") = Integer.Parse(M_M5.Text)
                Item("L_M_M6") = Integer.Parse(M_M6.Text)

                Item("L_M_P1") = Integer.Parse(M_P1.Text)
                Item("L_M_P2") = Integer.Parse(M_P2.Text)
                Item("L_M_P3") = Integer.Parse(M_P3.Text)
                Item("L_M_P4") = Integer.Parse(M_P4.Text)
                Item("L_M_P5") = Integer.Parse(M_P5.Text)
                Item("L_M_P6") = Integer.Parse(M_P6.Text)

            End If


            Item("T_METHOD") = Integer.Parse(T_METHOD.Text)

            On Error Resume Next
            Item("k0") = Double.Parse("0" & k0.Text)
            Item("k1") = Double.Parse("0" & k1.Text)
            Item("k2") = Double.Parse("0" & k2.Text)
            Item("k3") = Double.Parse("0" & k3.Text)

            Item("lk0") = Double.Parse("0" & lk0.Text)
            Item("lk1") = Double.Parse("0" & lk1.Text)
            Item("lk2") = Double.Parse("0" & lk2.Text)
            Item("lk3") = Double.Parse("0" & lk3.Text)


        End If
    End Sub



    Private Sub EditAnalizerConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub chkSEZON_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSEZON.CheckedChanged
        If mOnInit Then
            Exit Sub
        End If
        If Item Is Nothing Then Exit Sub
        On Error Resume Next
        If chkSEZON.Checked Then
            T1.Text = Item("T1").ToString()
            T2.Text = Item("T2").ToString()
            T3.Text = Item("T3").ToString()
            T4.Text = Item("T4").ToString()
            T5.Text = Item("T5").ToString()
            T6.Text = Item("T6").ToString()
            G1.Text = Item("G1").ToString()
            G2.Text = Item("G2").ToString()
            G3.Text = Item("G3").ToString()
            G4.Text = Item("G4").ToString()
            G5.Text = Item("G5").ToString()
            G6.Text = Item("G6").ToString()


            V1.Text = Item("V1").ToString()
            V2.Text = Item("V2").ToString()
            V3.Text = Item("V3").ToString()
            V4.Text = Item("V4").ToString()
            V5.Text = Item("V5").ToString()
            V6.Text = Item("V6").ToString()


            M1.Text = Item("M1").ToString()
            M2.Text = Item("M2").ToString()
            M3.Text = Item("M3").ToString()
            M4.Text = Item("M4").ToString()
            M5.Text = Item("M5").ToString()
            M6.Text = Item("M6").ToString()

            p1.Text = Item("P1").ToString()
            p2.Text = Item("P2").ToString()
            p3.Text = Item("P3").ToString()
            p4.Text = Item("P4").ToString()
            p5.Text = Item("P5").ToString()
            p6.Text = Item("P6").ToString()


            M_G1.Text = Item("M_G1").ToString()
            M_G2.Text = Item("M_G2").ToString()
            M_G3.Text = Item("M_G3").ToString()
            M_G4.Text = Item("M_G4").ToString()
            M_G5.Text = Item("M_G5").ToString()
            M_G6.Text = Item("M_G6").ToString()


            M_V1.Text = Item("M_V1").ToString()
            M_V2.Text = Item("M_V2").ToString()
            M_V3.Text = Item("M_V3").ToString()
            M_V4.Text = Item("M_V4").ToString()
            M_V5.Text = Item("M_V5").ToString()
            M_V6.Text = Item("M_V6").ToString()


            M_M1.Text = Item("M_M1").ToString()
            M_M2.Text = Item("M_M2").ToString()
            M_M3.Text = Item("M_M3").ToString()
            M_M4.Text = Item("M_M4").ToString()
            M_M5.Text = Item("M_M5").ToString()
            M_M6.Text = Item("M_M6").ToString()

            M_P1.Text = Item("M_P1").ToString()
            M_P2.Text = Item("M_P2").ToString()
            M_P3.Text = Item("M_P3").ToString()
            M_P4.Text = Item("M_P4").ToString()
            M_P5.Text = Item("M_P5").ToString()
            M_P6.Text = Item("M_P6").ToString()
        Else
            T1.Text = Item("L_T1").ToString()
            T2.Text = Item("L_T2").ToString()
            T3.Text = Item("L_T3").ToString()
            T4.Text = Item("L_T4").ToString()
            T5.Text = Item("L_T5").ToString()
            T6.Text = Item("L_T6").ToString()
            G1.Text = Item("L_G1").ToString()
            G2.Text = Item("L_G2").ToString()
            G3.Text = Item("L_G3").ToString()
            G4.Text = Item("L_G4").ToString()
            G5.Text = Item("L_G5").ToString()
            G6.Text = Item("L_G6").ToString()


            V1.Text = Item("L_V1").ToString()
            V2.Text = Item("L_V2").ToString()
            V3.Text = Item("L_V3").ToString()
            V4.Text = Item("L_V4").ToString()
            V5.Text = Item("L_V5").ToString()
            V6.Text = Item("L_V6").ToString()


            M1.Text = Item("L_M1").ToString()
            M2.Text = Item("L_M2").ToString()
            M3.Text = Item("L_M3").ToString()
            M4.Text = Item("L_M4").ToString()
            M5.Text = Item("L_M5").ToString()
            M6.Text = Item("L_M6").ToString()

            p1.Text = Item("L_P1").ToString()
            p2.Text = Item("L_P2").ToString()
            p3.Text = Item("L_P3").ToString()
            p4.Text = Item("L_P4").ToString()
            p5.Text = Item("L_P5").ToString()
            p6.Text = Item("L_P6").ToString()


            M_G1.Text = Item("L_M_G1").ToString()
            M_G2.Text = Item("L_M_G2").ToString()
            M_G3.Text = Item("L_M_G3").ToString()
            M_G4.Text = Item("L_M_G4").ToString()
            M_G5.Text = Item("L_M_G5").ToString()
            M_G6.Text = Item("L_M_G6").ToString()


            M_V1.Text = Item("L_M_V1").ToString()
            M_V2.Text = Item("L_M_V2").ToString()
            M_V3.Text = Item("L_M_V3").ToString()
            M_V4.Text = Item("L_M_V4").ToString()
            M_V5.Text = Item("L_M_V5").ToString()
            M_V6.Text = Item("L_M_V6").ToString()


            M_M1.Text = Item("L_M_M1").ToString()
            M_M2.Text = Item("L_M_M2").ToString()
            M_M3.Text = Item("L_M_M3").ToString()
            M_M4.Text = Item("L_M_M4").ToString()
            M_M5.Text = Item("L_M_M5").ToString()
            M_M6.Text = Item("L_M_M6").ToString()

            M_P1.Text = Item("L_M_P1").ToString()
            M_P2.Text = Item("L_M_P2").ToString()
            M_P3.Text = Item("L_M_P3").ToString()
            M_P4.Text = Item("L_M_P4").ToString()
            M_P5.Text = Item("L_M_P5").ToString()
            M_P6.Text = Item("L_M_P6").ToString()

        End If


    End Sub
End Class
