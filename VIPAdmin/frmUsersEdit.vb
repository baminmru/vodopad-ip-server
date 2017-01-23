Public Class frmUsersEdit

    Public id As Integer


    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim ar As String
        If chkRequery.Checked Then
            ar = "1"
        Else
            ar = "0"
        End If


        Dim arpt As String
        If chkReport.Checked Then
            arpt = "1"
        Else
            arpt = "0"
        End If

        Dim at As String
        If chkTpl.Checked Then
            at = "1"
        Else
            at = "0"
        End If

        If id = 0 Then
            Dim s As String



            Try
                If chkLocked.Checked Then
                    s = "insert into users(usersid,login,password,locked,allowrequery,allowreport,allowtemplate) values(users_seq.nextval,'" + txtLogin.Text + "','" + txtPassword.Text + "',1," + ar + "," + arpt + "," + at + ")"
                Else
                    s = "insert into users(usersid,login,password,locked,allowrequery,allowreport,allowtemplate) values(users_seq.nextval,'" + txtLogin.Text + "','" + txtPassword.Text + "',0," + ar + "," + arpt + "," + at + ")"
                End If

                TvMain.QueryExec(s)
                Me.Close()
            Catch ex As Exception

            End Try
        Else
            Try
                Dim s As String
                If chkLocked.Checked Then
                    s = "update users set  login='" + txtLogin.Text + "',password='" + txtPassword.Text + "' ,locked=1,allowrequery=" + ar + ",allowreport=" + arpt + ",allowtemplate=" + at + " where usersid=" + id.ToString
                Else
                    s = "update users set  login='" + txtLogin.Text + "',password='" + txtPassword.Text + "' , locked=0,allowrequery=" + ar + ",allowreport=" + arpt + ",allowtemplate=" + at + "   where usersid=" + id.ToString
                End If

                TvMain.QueryExec(s)

                Dim j As Long, i As Long

                Dim dt As DataTable
                dt = TvMain.QuerySelect("select bgroups.id_grp id,bgroups.cgrpnm name ,nvl(usersid,0) used from bgroups left join usergroup on  bgroups.id_grp=usergroup.id_grp and usersid=" + id.ToString)

                For j = 0 To chkGroups.Items.Count - 1
                    For i = 0 To dt.Rows.Count - 1
                        If chkGroups.Items(j) = dt.Rows(i)("name") Then
                            If chkGroups.GetItemChecked(j) Then
                                Try
                                    TvMain.QueryExec("insert into usergroup(usersid,id_grp) values(" + id.ToString + "," + dt.Rows(i)("id").ToString + " )")
                                Catch ex As Exception

                                End Try

                            Else
                                Try
                                    TvMain.QueryExec("delete from usergroup where usersid=" + id.ToString + " and id_grp=" + dt.Rows(i)("id").ToString)
                                Catch ex As Exception

                                End Try
                            End If


                        End If


                    Next
                Next
                LoadGroups()

                Me.Close()
            Catch ex As Exception

            End Try
        End If


    End Sub

   


 

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

  


    Public Sub LoadGroups()
        Dim dt As DataTable
        dt = TvMain.QuerySelect("select bgroups.id_grp id,bgroups.cgrpnm name ,nvl(usersid,0) used from bgroups left join usergroup on  bgroups.id_grp=usergroup.id_grp and usersid=" + id.ToString)
        Dim i As Long


        chkGroups.Items.Clear()

        For i = 0 To dt.Rows.Count - 1
            chkGroups.Items.Add(dt.Rows(i)("name"), dt.Rows(i)("used") <> 0)
        Next

    End Sub
End Class
