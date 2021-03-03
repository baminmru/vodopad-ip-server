Imports System.Text
Imports System.IO

Public Class Form2

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click




        ' Create two different encodings.
        Dim ascii As Encoding = Encoding.GetEncoding(866)

        Dim unicode As Encoding = Encoding.Unicode

        ' Convert the string into a byte array.
        Dim unicodeBytes As Byte() = unicode.GetBytes(TextBox1.Text)

        ' Perform the conversion from one encoding to the other.
        Dim asciiBytes As Byte() = Encoding.Convert(unicode, ascii, unicodeBytes)

        ' Convert the new byte array into a char array and then into a string.
        Dim asciiChars(ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length) - 1) As Char
        ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0)
        Dim asciiString As New String(asciiChars)
        Dim fi As FileInfo = New FileInfo(Application.ExecutablePath)
        Dim di As DirectoryInfo = fi.Directory
        fi = New FileInfo(di.FullName + "\out.txt")
        Dim io As FileStream

        io = fi.Create()
        io.Write(asciiBytes, 0, asciiBytes.Length)
        io.Close()
        TextBox2.Text = asciiString
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim fi As FileInfo = New FileInfo(Application.ExecutablePath)
        Dim di As DirectoryInfo = fi.Directory
        fi = New FileInfo(di.FullName + "\out.txt")
        Dim io As FileStream
        Dim bb(0 To 255) As Byte

        Dim i As Integer
        For i = 0 To 255
            bb(i) = i
        Next
        io = fi.Create()
        io.Write(bb, 0, bb.Length)
        io.Close()
    End Sub
End Class