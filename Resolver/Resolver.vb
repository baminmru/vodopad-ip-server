Imports System.Web
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms

Public Class XY

    Public Address As String
    Public X As String
    Public Y As String
    Public Resolved As Boolean
   

End Class

Public Class Resolver
    Public ResolvedCount As Integer
    Public Event Resolving(ByVal Addr As String, ByVal ok As Boolean)

    Private Function getCoords(ByVal address As String) As XY

        Dim myReq As System.Net.WebRequest
        Dim coords As XY = New XY()
        Dim xml_reader As XmlReader
        Dim resp As Stream
        Dim doc As XmlDocument = New System.Xml.XmlDocument()
        Dim hdoc As HtmlDocument
        Dim root As XmlNode
        Dim hroot As HtmlElement
        Dim myCoords As String
        Dim ind As Integer = 0
        Dim initial_str As String = "http://geocode-maps.yandex.ru/1.x/?geocode="
        Dim coda_str As String = "&results=1&key=AEXnz0wBAAAAuIknOwIAVCSGNcmL3JRmL4oleFvd4sUhi6gAAAAAAAAAAAC17rWD4CY5R4vkVh3Btj238dtOPg=="
        Try
            'DocOK = False
            'wb.Navigate(initial_str + address + coda_str)
            'While Not DocOK
            '    Application.DoEvents()
            'End While
            myReq = System.Net.WebRequest.Create(initial_str + address + coda_str)

            resp = myReq.GetResponse().GetResponseStream()

            xml_reader = XmlReader.Create(resp)
            doc.Load(xml_reader)
            'hdoc = wb.Document
            root = doc.ChildNodes(1)
            myCoords = root.ChildNodes(0).ChildNodes(1).ChildNodes(0).ChildNodes(3).ChildNodes(0).ChildNodes(0).InnerText
            'resp.Close()
            'xml_reader.Close()
            ind = myCoords.IndexOf(" ")
            coords.Address = address
            coords.X = myCoords.Substring(0, ind)
            coords.Y = myCoords.Substring(ind + 1, myCoords.Length - ind - 1)

            coords.Resolved = True


        Catch ex As System.Exception
            ' MsgBox(ex.Message & " " & address)

            coords.Resolved = False
            coords.X = "0"
            coords.Y = "0"
            coords.Address = address
        End Try

        Return coords
    End Function


    Public Function ResolveAddresses(ByVal Addr As String) As String


        ResolvedCount = 0
     
        Dim i As Integer
        Dim res As XY = Nothing
        Dim tmp As String
        Dim sOut As String = ""
      
        tmp = Addr


            res = getCoords(tmp)
            If Not (res Is Nothing) Then

                If (res.Resolved = True) Then

                sOut += res.X + ";" + res.Y + ";"
                   
                    ResolvedCount += 1

                    RaiseEvent Resolving(tmp, True)

                Else

                sOut += " failed;failed;"
                 
                    RaiseEvent Resolving(tmp, False)
                End If

            Else

            sOut += "  error;error;"
            End If
        


        Return sOut




    End Function

   
End Class