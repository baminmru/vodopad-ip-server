
Public Class pGrid
    Private mOnInit As Boolean = False
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

    Public Item As DataRow
    Private mRowReadOnly As Boolean
    Private tMain As STKTVMain.TVMain
    Private conDT As DataTable

    ''' <summary>
    '''Инициализация
    ''' </summary>
    ''' <remarks>
    '''
    ''' </remarks>
    Public Sub Attach(ByVal nItem As DataRow, ByVal RowReadOnly As Boolean, ByVal tvMain As STKTVMain.TVMain)
        Item = nItem

        mRowReadOnly = RowReadOnly
        tMain = tvMain
        If Item Is Nothing Then Exit Sub
        mOnInit = True

        conDT = tMain.QuerySelect("SELECT COLUMN_NAME,COMMENTS FROM user_col_comments WHERE table_name = 'CONTRACT' and COLUMN_NAME like 'FLD%'")
        Dim i As Integer

        With Grd
            .ShowCustomProperties = True
            .Item.Clear()

            For i = 0 To conDT.Rows.Count - 1
                If Item(conDT.Rows(i)("COLUMN_NAME")).GetType().Name = "DBNull" Then
                    .Item.Add(conDT.Rows(i)("COMMENTS"), "-", False, "Договорные настройки", conDT.Rows(i)("COMMENTS"), True)
                Else
                    If Item(conDT.Rows(i)("COLUMN_NAME")) & "" = "" Then
                        .Item.Add(conDT.Rows(i)("COMMENTS"), "-", False, "Договорные настройки", conDT.Rows(i)("COMMENTS"), True)
                    Else

                        .Item.Add(conDT.Rows(i)("COMMENTS"), Item(conDT.Rows(i)("COLUMN_NAME")) & "", False, "Договорные настройки", conDT.Rows(i)("COMMENTS"), True)
                    End If
                End If
            Next

        End With

        Grd.Refresh()
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

            Dim j As Integer

            With Grd
                For j = 0 To conDT.Rows.Count - 1
                    For i = 0 To conDT.Rows.Count - 1
                        If .Item(j).Name = conDT.Rows(i)("COMMENTS") Then
                            Item(conDT.Rows(i)("COLUMN_NAME")) = .Item(j).Value
                            Exit For
                        End If
                    Next

                Next

            End With

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

    Private Sub Grd_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grd.Resize
        Debug.Print("resize:" + Grd.Width.ToString + " " + Grd.Height.ToString)
        Grd.Width = Me.Width - 10
        Grd.Height = Me.Height - 10
        Grd.Top = 5
        Grd.Left = 5
    End Sub

    Private Sub Grd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grd.Click

    End Sub
End Class
