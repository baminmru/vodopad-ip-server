Imports System
Imports System.Xml
Imports CarlosAg.ExcelXmlWriter

Namespace ExcelXmlWriter.Sample
    
    Public Class App
        
        Public Sub Generate(ByVal filename As String)
            Dim book As Workbook = New Workbook
            '-----------------------------------------------
            ' Properties
            '-----------------------------------------------
            book.Properties.Author = "??????? ? ?"
            book.Properties.LastAuthor = "bami"
            book.Properties.Created = New Date(1997, 10, 22, 15, 13, 26, 0)
            book.Properties.Version = "12.00"
            book.ExcelWorkbook.WindowHeight = 6045
            book.ExcelWorkbook.WindowWidth = 11460
            book.ExcelWorkbook.WindowTopX = -15
            book.ExcelWorkbook.WindowTopY = -15
            book.ExcelWorkbook.ActiveSheetIndex = 2
            book.ExcelWorkbook.ProtectWindows = false
            book.ExcelWorkbook.ProtectStructure = false
            '-----------------------------------------------
            ' Generate Styles
            '-----------------------------------------------
            Me.GenerateStyles(book.Styles)
            '-----------------------------------------------
            ' Generate ????? Worksheet
            '-----------------------------------------------
            Me.GenerateWorksheetSheet(book.Worksheets)
            '-----------------------------------------------
            ' Generate ????? (?) Worksheet
            '-----------------------------------------------
            Me.GenerateWorksheetSheet(book.Worksheets)
            '-----------------------------------------------
            ' Generate ????????? Worksheet
            '-----------------------------------------------
            Me.GenerateWorksheetSheet(book.Worksheets)
            '-----------------------------------------------
            ' Generate ????????? (2) Worksheet
            '-----------------------------------------------
            Me.GenerateWorksheetSheet2(book.Worksheets)
            '-----------------------------------------------
            ' Generate ????? (2) Worksheet
            '-----------------------------------------------
            Me.GenerateWorksheetSheet2(book.Worksheets)
            book.Save(filename)
        End Sub
        
        Private Sub GenerateStyles(ByVal styles As WorksheetStyleCollection)
            '-----------------------------------------------
            ' Default
            '-----------------------------------------------
            Dim [Default] As WorksheetStyle = styles.Add("Default")
            [Default].Name = "Normal"
            [Default].Font.FontName = "Arial Cyr"
            [Default].Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s16
            '-----------------------------------------------
            Dim s16 As WorksheetStyle = styles.Add("s16")
            s16.Name = "???????????"
            s16.Font.Underline = UnderlineStyle.[Single]
            s16.Font.FontName = "Arial Cyr"
            s16.Font.Color = "#0000FF"
            '-----------------------------------------------
            ' s43
            '-----------------------------------------------
            Dim s43 As WorksheetStyle = styles.Add("s43")
            s43.Name = "???????_??????"
            s43.Font.FontName = "Arial Cyr"
            s43.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s43.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' m80092384
            '-----------------------------------------------
            Dim m80092384 As WorksheetStyle = styles.Add("m80092384")
            m80092384.Font.Bold = true
            m80092384.Font.FontName = "Arial Cyr"
            m80092384.Font.Size = 9
            m80092384.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80092384.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80092384.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            m80092384.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            m80092384.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m80092000
            '-----------------------------------------------
            Dim m80092000 As WorksheetStyle = styles.Add("m80092000")
            m80092000.Font.Bold = true
            m80092000.Font.FontName = "Arial Cyr"
            m80092000.Font.Size = 9
            m80092000.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80092000.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80092000.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            m80092000.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            m80092000.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m80091856
            '-----------------------------------------------
            Dim m80091856 As WorksheetStyle = styles.Add("m80091856")
            m80091856.Font.FontName = "Arial Cyr"
            m80091856.Font.Size = 9
            m80091856.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80091856.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80091856.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m80091856.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m80091856.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m80091856.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            m80091856.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' m80091956
            '-----------------------------------------------
            Dim m80091956 As WorksheetStyle = styles.Add("m80091956")
            m80091956.Font.Bold = true
            m80091956.Font.FontName = "Arial Cyr"
            m80091956.Font.Size = 9
            m80091956.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80091956.Alignment.Vertical = StyleVerticalAlignment.Top
            m80091956.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            m80091956.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            m80091956.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m80091956.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m80091552
            '-----------------------------------------------
            Dim m80091552 As WorksheetStyle = styles.Add("m80091552")
            m80091552.Font.FontName = "Arial Cyr"
            m80091552.Font.Size = 9
            m80091552.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80091552.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80091552.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m80091552.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m80091552.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m80091552.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            m80091552.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' m80091572
            '-----------------------------------------------
            Dim m80091572 As WorksheetStyle = styles.Add("m80091572")
            m80091572.Font.Bold = true
            m80091572.Font.FontName = "Arial Cyr"
            m80091572.Font.Size = 9
            m80091572.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80091572.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80091572.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m80091572.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m80091572.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m80091572.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m80091652
            '-----------------------------------------------
            Dim m80091652 As WorksheetStyle = styles.Add("m80091652")
            m80091652.Font.Bold = true
            m80091652.Font.FontName = "Arial Cyr"
            m80091652.Font.Size = 9
            m80091652.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80091652.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80091652.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m80091652.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m80091652.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m80091652.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m80091672
            '-----------------------------------------------
            Dim m80091672 As WorksheetStyle = styles.Add("m80091672")
            m80091672.Font.Bold = true
            m80091672.Font.FontName = "Arial Cyr"
            m80091672.Font.Size = 9
            m80091672.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m80091672.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m80091672.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m80091672.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m80091672.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m80091672.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m85476672
            '-----------------------------------------------
            Dim m85476672 As WorksheetStyle = styles.Add("m85476672")
            m85476672.Font.FontName = "Arial Cyr"
            m85476672.Font.Size = 9
            m85476672.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476672.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476672.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85476672.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85476672.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85476672.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            m85476672.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' m85476692
            '-----------------------------------------------
            Dim m85476692 As WorksheetStyle = styles.Add("m85476692")
            m85476692.Font.Bold = true
            m85476692.Font.FontName = "Arial Cyr"
            m85476692.Font.Size = 9
            m85476692.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476692.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476692.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85476692.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85476692.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85476692.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m85476772
            '-----------------------------------------------
            Dim m85476772 As WorksheetStyle = styles.Add("m85476772")
            m85476772.Font.Bold = true
            m85476772.Font.FontName = "Arial Cyr"
            m85476772.Font.Size = 9
            m85476772.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476772.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476772.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85476772.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85476772.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85476772.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m85476792
            '-----------------------------------------------
            Dim m85476792 As WorksheetStyle = styles.Add("m85476792")
            m85476792.Font.Bold = true
            m85476792.Font.FontName = "Arial Cyr"
            m85476792.Font.Size = 9
            m85476792.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476792.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476792.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85476792.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85476792.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85476792.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m85476608
            '-----------------------------------------------
            Dim m85476608 As WorksheetStyle = styles.Add("m85476608")
            m85476608.Font.FontName = "Arial Cyr"
            m85476608.Font.Size = 9
            m85476608.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476608.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476608.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85476608.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85476608.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85476608.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            m85476608.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' m85476224
            '-----------------------------------------------
            Dim m85476224 As WorksheetStyle = styles.Add("m85476224")
            m85476224.Font.Bold = true
            m85476224.Font.FontName = "Arial Cyr"
            m85476224.Font.Size = 9
            m85476224.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476224.Alignment.Vertical = StyleVerticalAlignment.Top
            m85476224.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            m85476224.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            m85476224.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85476224.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85476244
            '-----------------------------------------------
            Dim m85476244 As WorksheetStyle = styles.Add("m85476244")
            m85476244.Font.Italic = true
            m85476244.Font.FontName = "Arial Cyr"
            m85476244.Font.Size = 9
            m85476244.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476244.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476244.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            m85476244.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            m85476244.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85476180
            '-----------------------------------------------
            Dim m85476180 As WorksheetStyle = styles.Add("m85476180")
            m85476180.Font.Bold = true
            m85476180.Font.FontName = "Arial Cyr"
            m85476180.Font.Size = 9
            m85476180.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85476180.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85476180.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            m85476180.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            m85476180.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85477364
            '-----------------------------------------------
            Dim m85477364 As WorksheetStyle = styles.Add("m85477364")
            m85477364.Font.FontName = "Arial Cyr"
            m85477364.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85477364.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85477364.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85477364.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85477364.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85477364.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m85477200
            '-----------------------------------------------
            Dim m85477200 As WorksheetStyle = styles.Add("m85477200")
            m85477200.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85477200.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85477200.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85477200.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85477200.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85477200.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85477220
            '-----------------------------------------------
            Dim m85477220 As WorksheetStyle = styles.Add("m85477220")
            m85477220.Font.Bold = true
            m85477220.Font.FontName = "Arial Cyr"
            m85477220.Font.Size = 7
            m85477220.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85477220.Alignment.Vertical = StyleVerticalAlignment.Center
            m85477220.Alignment.WrapText = true
            m85477220.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85477220.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85477220.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85477220.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85478096
            '-----------------------------------------------
            Dim m85478096 As WorksheetStyle = styles.Add("m85478096")
            m85478096.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85478096.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85478096.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85478096.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85478096.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85478096.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85477792
            '-----------------------------------------------
            Dim m85477792 As WorksheetStyle = styles.Add("m85477792")
            m85477792.Font.Bold = true
            m85477792.Font.FontName = "Arial Cyr"
            m85477792.Font.Size = 7
            m85477792.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85477792.Alignment.Vertical = StyleVerticalAlignment.Center
            m85477792.Alignment.WrapText = true
            m85477792.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85477792.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85477792.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85477792.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85477832
            '-----------------------------------------------
            Dim m85477832 As WorksheetStyle = styles.Add("m85477832")
            m85477832.Font.FontName = "Arial Cyr"
            m85477832.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85477832.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85477832.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m85477832.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m85477832.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m85477832.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m84779456
            '-----------------------------------------------
            Dim m84779456 As WorksheetStyle = styles.Add("m84779456")
            m84779456.Font.FontName = "Arial Cyr"
            m84779456.Font.Size = 9
            m84779456.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779456.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m84779456.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m84779456.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m84779456.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m84779456.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            m84779456.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' m84779476
            '-----------------------------------------------
            Dim m84779476 As WorksheetStyle = styles.Add("m84779476")
            m84779476.Font.Bold = true
            m84779476.Font.FontName = "Arial Cyr"
            m84779476.Font.Size = 9
            m84779476.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779476.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m84779476.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m84779476.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m84779476.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m84779476.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m84779556
            '-----------------------------------------------
            Dim m84779556 As WorksheetStyle = styles.Add("m84779556")
            m84779556.Font.Bold = true
            m84779556.Font.FontName = "Arial Cyr"
            m84779556.Font.Size = 9
            m84779556.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779556.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m84779556.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m84779556.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m84779556.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m84779556.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m84779576
            '-----------------------------------------------
            Dim m84779576 As WorksheetStyle = styles.Add("m84779576")
            m84779576.Font.Bold = true
            m84779576.Font.FontName = "Arial Cyr"
            m84779576.Font.Size = 9
            m84779576.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779576.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m84779576.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m84779576.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m84779576.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m84779576.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' m84779392
            '-----------------------------------------------
            Dim m84779392 As WorksheetStyle = styles.Add("m84779392")
            m84779392.Font.FontName = "Arial Cyr"
            m84779392.Font.Size = 9
            m84779392.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779392.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m84779392.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            m84779392.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            m84779392.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m84779392.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            m84779392.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' m84779008
            '-----------------------------------------------
            Dim m84779008 As WorksheetStyle = styles.Add("m84779008")
            m84779008.Font.Bold = true
            m84779008.Font.FontName = "Arial Cyr"
            m84779008.Font.Size = 9
            m84779008.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779008.Alignment.Vertical = StyleVerticalAlignment.Top
            m84779008.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            m84779008.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            m84779008.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            m84779008.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m84779028
            '-----------------------------------------------
            Dim m84779028 As WorksheetStyle = styles.Add("m84779028")
            m84779028.Font.Bold = true
            m84779028.Font.FontName = "Arial Cyr"
            m84779028.Font.Size = 9
            m84779028.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m84779028.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m84779028.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            m84779028.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            m84779028.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' m85479316
            '-----------------------------------------------
            Dim m85479316 As WorksheetStyle = styles.Add("m85479316")
            m85479316.Font.Bold = true
            m85479316.Font.FontName = "Arial Cyr"
            m85479316.Font.Size = 9
            m85479316.Alignment.Horizontal = StyleHorizontalAlignment.Center
            m85479316.Alignment.Vertical = StyleVerticalAlignment.Bottom
            m85479316.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            m85479316.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            m85479316.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s71
            '-----------------------------------------------
            Dim s71 As WorksheetStyle = styles.Add("s71")
            '-----------------------------------------------
            ' s72
            '-----------------------------------------------
            Dim s72 As WorksheetStyle = styles.Add("s72")
            s72.Font.FontName = "Arial Cyr"
            '-----------------------------------------------
            ' s73
            '-----------------------------------------------
            Dim s73 As WorksheetStyle = styles.Add("s73")
            s73.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s73.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s73.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s74
            '-----------------------------------------------
            Dim s74 As WorksheetStyle = styles.Add("s74")
            s74.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s74.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s74.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s74.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s75
            '-----------------------------------------------
            Dim s75 As WorksheetStyle = styles.Add("s75")
            s75.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s75.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s75.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s75.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s76
            '-----------------------------------------------
            Dim s76 As WorksheetStyle = styles.Add("s76")
            s76.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s76.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s76.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s76.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s76.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s77
            '-----------------------------------------------
            Dim s77 As WorksheetStyle = styles.Add("s77")
            s77.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s78
            '-----------------------------------------------
            Dim s78 As WorksheetStyle = styles.Add("s78")
            s78.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s78.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s79
            '-----------------------------------------------
            Dim s79 As WorksheetStyle = styles.Add("s79")
            s79.Font.Bold = true
            s79.Font.FontName = "Arial Cyr"
            s79.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s79.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s79.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s79.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s79.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s80
            '-----------------------------------------------
            Dim s80 As WorksheetStyle = styles.Add("s80")
            s80.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s80.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s80.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s81
            '-----------------------------------------------
            Dim s81 As WorksheetStyle = styles.Add("s81")
            s81.Font.Bold = true
            s81.Font.Italic = true
            s81.Font.FontName = "Arial Cyr"
            '-----------------------------------------------
            ' s82
            '-----------------------------------------------
            Dim s82 As WorksheetStyle = styles.Add("s82")
            s82.Font.Bold = true
            s82.Font.FontName = "Arial Cyr"
            '-----------------------------------------------
            ' s83
            '-----------------------------------------------
            Dim s83 As WorksheetStyle = styles.Add("s83")
            s83.Font.Bold = true
            s83.Font.FontName = "Arial Cyr"
            s83.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s83.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s84
            '-----------------------------------------------
            Dim s84 As WorksheetStyle = styles.Add("s84")
            s84.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s84.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s84.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s84.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s85
            '-----------------------------------------------
            Dim s85 As WorksheetStyle = styles.Add("s85")
            s85.Font.Bold = true
            s85.Font.FontName = "Arial Cyr"
            s85.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s85.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s85.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s85.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s86
            '-----------------------------------------------
            Dim s86 As WorksheetStyle = styles.Add("s86")
            s86.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s87
            '-----------------------------------------------
            Dim s87 As WorksheetStyle = styles.Add("s87")
            s87.Font.Bold = true
            s87.Font.FontName = "Arial Cyr"
            s87.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s87.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s87.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s88
            '-----------------------------------------------
            Dim s88 As WorksheetStyle = styles.Add("s88")
            s88.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s88.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s89
            '-----------------------------------------------
            Dim s89 As WorksheetStyle = styles.Add("s89")
            s89.Font.Bold = true
            s89.Font.FontName = "Arial Cyr"
            s89.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s89.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s89.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s89.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s90
            '-----------------------------------------------
            Dim s90 As WorksheetStyle = styles.Add("s90")
            s90.Alignment.Horizontal = StyleHorizontalAlignment.CenterAcrossSelection
            s90.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s90.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s90.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s90.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s91
            '-----------------------------------------------
            Dim s91 As WorksheetStyle = styles.Add("s91")
            s91.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s91.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s91.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s92
            '-----------------------------------------------
            Dim s92 As WorksheetStyle = styles.Add("s92")
            s92.Alignment.Horizontal = StyleHorizontalAlignment.CenterAcrossSelection
            s92.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s92.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s92.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s93
            '-----------------------------------------------
            Dim s93 As WorksheetStyle = styles.Add("s93")
            s93.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s93.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s93.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s93.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s94
            '-----------------------------------------------
            Dim s94 As WorksheetStyle = styles.Add("s94")
            s94.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s94.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s94.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s94.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s94.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s94.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s95
            '-----------------------------------------------
            Dim s95 As WorksheetStyle = styles.Add("s95")
            s95.Font.Bold = true
            s95.Font.FontName = "Arial Cyr"
            '-----------------------------------------------
            ' s96
            '-----------------------------------------------
            Dim s96 As WorksheetStyle = styles.Add("s96")
            s96.Font.FontName = "Arial Cyr"
            s96.Font.Size = 9
            '-----------------------------------------------
            ' s97
            '-----------------------------------------------
            Dim s97 As WorksheetStyle = styles.Add("s97")
            s97.Font.Bold = true
            s97.Font.Italic = true
            s97.Font.FontName = "Arial Cyr"
            s97.Font.Size = 9
            s97.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s97.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s98
            '-----------------------------------------------
            Dim s98 As WorksheetStyle = styles.Add("s98")
            s98.Font.Bold = true
            s98.Font.Italic = true
            s98.Font.FontName = "Arial Cyr"
            s98.Font.Size = 9
            '-----------------------------------------------
            ' s99
            '-----------------------------------------------
            Dim s99 As WorksheetStyle = styles.Add("s99")
            s99.Font.FontName = "Arial Cyr"
            s99.Font.Size = 9
            s99.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s99.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s100
            '-----------------------------------------------
            Dim s100 As WorksheetStyle = styles.Add("s100")
            s100.Font.Bold = true
            s100.Font.FontName = "Arial Cyr"
            s100.Font.Size = 9
            s100.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s100.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s101
            '-----------------------------------------------
            Dim s101 As WorksheetStyle = styles.Add("s101")
            s101.Font.Italic = true
            s101.Font.FontName = "Arial Cyr"
            s101.Font.Size = 9
            '-----------------------------------------------
            ' s102
            '-----------------------------------------------
            Dim s102 As WorksheetStyle = styles.Add("s102")
            s102.Font.Bold = true
            s102.Font.FontName = "Arial Cyr"
            s102.Font.Size = 9
            s102.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s102.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s102.NumberFormat = "@"
            '-----------------------------------------------
            ' s103
            '-----------------------------------------------
            Dim s103 As WorksheetStyle = styles.Add("s103")
            s103.Font.Bold = true
            s103.Font.FontName = "Arial Cyr"
            s103.Font.Size = 9
            s103.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s103.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s104
            '-----------------------------------------------
            Dim s104 As WorksheetStyle = styles.Add("s104")
            s104.Font.Bold = true
            s104.Font.FontName = "Arial Cyr"
            s104.Font.Size = 9
            s104.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s104.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s105
            '-----------------------------------------------
            Dim s105 As WorksheetStyle = styles.Add("s105")
            s105.Font.FontName = "Arial Cyr"
            s105.Font.Size = 9
            s105.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s106
            '-----------------------------------------------
            Dim s106 As WorksheetStyle = styles.Add("s106")
            s106.Font.Bold = true
            s106.Font.FontName = "Arial Cyr"
            s106.Font.Size = 9
            s106.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s106.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s106.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s107
            '-----------------------------------------------
            Dim s107 As WorksheetStyle = styles.Add("s107")
            s107.Font.FontName = "Arial Cyr"
            s107.Font.Size = 9
            s107.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s107.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s107.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s108
            '-----------------------------------------------
            Dim s108 As WorksheetStyle = styles.Add("s108")
            s108.Font.Bold = true
            s108.Font.FontName = "Arial Cyr"
            s108.Font.Size = 9
            s108.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s109
            '-----------------------------------------------
            Dim s109 As WorksheetStyle = styles.Add("s109")
            s109.Font.FontName = "Arial Cyr"
            s109.Font.Size = 9
            '-----------------------------------------------
            ' s110
            '-----------------------------------------------
            Dim s110 As WorksheetStyle = styles.Add("s110")
            s110.Font.Bold = true
            s110.Font.FontName = "Arial Cyr"
            s110.Font.Size = 9
            '-----------------------------------------------
            ' s111
            '-----------------------------------------------
            Dim s111 As WorksheetStyle = styles.Add("s111")
            s111.Font.FontName = "Arial Cyr"
            s111.Font.Size = 9
            s111.Alignment.Horizontal = StyleHorizontalAlignment.Right
            s111.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s112
            '-----------------------------------------------
            Dim s112 As WorksheetStyle = styles.Add("s112")
            s112.Font.Bold = true
            s112.Font.FontName = "Arial Cyr"
            s112.Font.Size = 9
            '-----------------------------------------------
            ' s113
            '-----------------------------------------------
            Dim s113 As WorksheetStyle = styles.Add("s113")
            s113.Font.Bold = true
            s113.Font.FontName = "Arial Cyr"
            s113.Font.Size = 9
            s113.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s113.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s113.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s114
            '-----------------------------------------------
            Dim s114 As WorksheetStyle = styles.Add("s114")
            s114.Font.FontName = "Arial Cyr"
            s114.Font.Size = 9
            s114.Alignment.Horizontal = StyleHorizontalAlignment.Right
            s114.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s114.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s115
            '-----------------------------------------------
            Dim s115 As WorksheetStyle = styles.Add("s115")
            s115.Font.Bold = true
            s115.Font.FontName = "Arial Cyr"
            s115.Font.Size = 9
            s115.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s115.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s115.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s116
            '-----------------------------------------------
            Dim s116 As WorksheetStyle = styles.Add("s116")
            s116.Font.FontName = "Arial Cyr"
            s116.Font.Size = 9
            s116.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s116.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s117
            '-----------------------------------------------
            Dim s117 As WorksheetStyle = styles.Add("s117")
            s117.Font.FontName = "Arial Cyr"
            s117.Font.Size = 9
            s117.Alignment.Horizontal = StyleHorizontalAlignment.Right
            s117.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s117.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s118
            '-----------------------------------------------
            Dim s118 As WorksheetStyle = styles.Add("s118")
            s118.Font.FontName = "Arial Cyr"
            s118.Font.Size = 9
            s118.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s118.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s118.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s119
            '-----------------------------------------------
            Dim s119 As WorksheetStyle = styles.Add("s119")
            s119.Font.FontName = "Arial Cyr"
            s119.Font.Size = 9
            s119.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s119.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s119.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s119.NumberFormat = "0"
            '-----------------------------------------------
            ' s120
            '-----------------------------------------------
            Dim s120 As WorksheetStyle = styles.Add("s120")
            s120.Font.FontName = "Arial Cyr"
            s120.Font.Size = 9
            s120.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s121
            '-----------------------------------------------
            Dim s121 As WorksheetStyle = styles.Add("s121")
            s121.Font.Bold = true
            s121.Font.FontName = "Arial Cyr"
            s121.Font.Size = 9
            s121.Alignment.Vertical = StyleVerticalAlignment.Center
            '-----------------------------------------------
            ' s122
            '-----------------------------------------------
            Dim s122 As WorksheetStyle = styles.Add("s122")
            s122.Font.Bold = true
            s122.Font.FontName = "Arial Cyr"
            s122.Font.Size = 9
            s122.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s122.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s122.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s122.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s122.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s122.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s123
            '-----------------------------------------------
            Dim s123 As WorksheetStyle = styles.Add("s123")
            s123.Font.Bold = true
            s123.Font.FontName = "Arial Cyr"
            s123.Font.Size = 9
            s123.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s123.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s123.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s123.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s123.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s124
            '-----------------------------------------------
            Dim s124 As WorksheetStyle = styles.Add("s124")
            s124.Font.Bold = true
            s124.Font.FontName = "Arial Cyr"
            s124.Font.Size = 9
            s124.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s124.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s124.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s125
            '-----------------------------------------------
            Dim s125 As WorksheetStyle = styles.Add("s125")
            s125.Font.Bold = true
            s125.Font.FontName = "Arial Cyr"
            s125.Font.Size = 9
            s125.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s125.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s125.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s125.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s126
            '-----------------------------------------------
            Dim s126 As WorksheetStyle = styles.Add("s126")
            s126.Font.Bold = true
            s126.Font.FontName = "Arial Cyr"
            s126.Font.Size = 9
            s126.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s126.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s126.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s126.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s126.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s127
            '-----------------------------------------------
            Dim s127 As WorksheetStyle = styles.Add("s127")
            s127.Font.Bold = true
            s127.Font.FontName = "Arial Cyr"
            s127.Font.Size = 9
            s127.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s127.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s127.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s127.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s127.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s127.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s128
            '-----------------------------------------------
            Dim s128 As WorksheetStyle = styles.Add("s128")
            s128.Font.Bold = true
            s128.Font.FontName = "Arial Cyr"
            s128.Font.Size = 9
            s128.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s128.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s128.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s128.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s129
            '-----------------------------------------------
            Dim s129 As WorksheetStyle = styles.Add("s129")
            s129.Font.Bold = true
            s129.Font.FontName = "Arial Cyr"
            s129.Font.Size = 9
            s129.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s129.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s129.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s129.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s129.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s129.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s130
            '-----------------------------------------------
            Dim s130 As WorksheetStyle = styles.Add("s130")
            s130.Font.Bold = true
            s130.Font.FontName = "Arial Cyr"
            s130.Font.Size = 9
            s130.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s130.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s130.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s130.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s130.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s130.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s131
            '-----------------------------------------------
            Dim s131 As WorksheetStyle = styles.Add("s131")
            s131.Font.FontName = "Arial Cyr"
            s131.Font.Size = 9
            s131.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s131.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s131.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s131.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s131.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s132
            '-----------------------------------------------
            Dim s132 As WorksheetStyle = styles.Add("s132")
            s132.Font.FontName = "Arial Cyr"
            s132.Font.Size = 9
            s132.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s132.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s132.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s132.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s132.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s133
            '-----------------------------------------------
            Dim s133 As WorksheetStyle = styles.Add("s133")
            s133.Font.FontName = "Arial Cyr"
            s133.Font.Size = 9
            s133.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s133.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s133.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s133.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s134
            '-----------------------------------------------
            Dim s134 As WorksheetStyle = styles.Add("s134")
            s134.Font.FontName = "Arial Cyr"
            s134.Font.Size = 9
            s134.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s134.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s134.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s134.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s134.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s135
            '-----------------------------------------------
            Dim s135 As WorksheetStyle = styles.Add("s135")
            s135.Font.FontName = "Arial Cyr"
            s135.Font.Size = 9
            s135.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s135.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s135.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s135.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s135.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s136
            '-----------------------------------------------
            Dim s136 As WorksheetStyle = styles.Add("s136")
            s136.Font.FontName = "Arial Cyr"
            s136.Font.Size = 9
            s136.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s136.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s136.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s136.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s136.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s136.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s137
            '-----------------------------------------------
            Dim s137 As WorksheetStyle = styles.Add("s137")
            s137.Font.FontName = "Arial Cyr"
            s137.Font.Size = 9
            s137.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s137.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s137.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s138
            '-----------------------------------------------
            Dim s138 As WorksheetStyle = styles.Add("s138")
            s138.Font.FontName = "Arial Cyr"
            s138.Font.Size = 8
            s138.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s138.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s138.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s138.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s138.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s139
            '-----------------------------------------------
            Dim s139 As WorksheetStyle = styles.Add("s139")
            s139.Font.FontName = "Arial Cyr"
            s139.Font.Size = 9
            s139.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s139.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s139.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s139.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s139.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s140
            '-----------------------------------------------
            Dim s140 As WorksheetStyle = styles.Add("s140")
            s140.Font.Bold = true
            s140.Font.FontName = "Arial Cyr"
            s140.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s140.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s141
            '-----------------------------------------------
            Dim s141 As WorksheetStyle = styles.Add("s141")
            s141.Parent = "s16"
            s141.Font.Underline = UnderlineStyle.[Single]
            s141.Font.FontName = "Arial Cyr"
            s141.Font.Size = 9
            s141.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s142
            '-----------------------------------------------
            Dim s142 As WorksheetStyle = styles.Add("s142")
            s142.Font.FontName = "Arial Cyr"
            s142.Font.Size = 9
            s142.Alignment.Horizontal = StyleHorizontalAlignment.CenterAcrossSelection
            s142.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s142.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s142.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s142.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s142.NumberFormat = "dd/mm/yy"
            '-----------------------------------------------
            ' s143
            '-----------------------------------------------
            Dim s143 As WorksheetStyle = styles.Add("s143")
            s143.Font.FontName = "Arial Cyr"
            s143.Font.Size = 9
            s143.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s143.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s143.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s143.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s144
            '-----------------------------------------------
            Dim s144 As WorksheetStyle = styles.Add("s144")
            s144.Font.FontName = "Arial Cyr"
            s144.Font.Size = 9
            s144.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s144.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s145
            '-----------------------------------------------
            Dim s145 As WorksheetStyle = styles.Add("s145")
            s145.Font.FontName = "Arial Cyr"
            s145.Font.Size = 9
            s145.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s145.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s145.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s145.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s145.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s145.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s146
            '-----------------------------------------------
            Dim s146 As WorksheetStyle = styles.Add("s146")
            s146.Parent = "s43"
            s146.Font.FontName = "Arial Cyr"
            s146.Font.Size = 9
            s146.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s146.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s146.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s146.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s146.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s146.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s147
            '-----------------------------------------------
            Dim s147 As WorksheetStyle = styles.Add("s147")
            s147.Font.FontName = "Arial Cyr"
            s147.Font.Size = 9
            s147.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s147.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s147.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s147.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s147.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s148
            '-----------------------------------------------
            Dim s148 As WorksheetStyle = styles.Add("s148")
            s148.Parent = "s43"
            s148.Font.FontName = "Arial Cyr"
            s148.Font.Size = 9
            s148.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s148.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s148.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s148.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s148.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s148.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s149
            '-----------------------------------------------
            Dim s149 As WorksheetStyle = styles.Add("s149")
            s149.Font.FontName = "Arial Cyr"
            s149.Font.Size = 9
            s149.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s149.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s149.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s149.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s149.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s149.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s150
            '-----------------------------------------------
            Dim s150 As WorksheetStyle = styles.Add("s150")
            s150.Font.FontName = "Arial Cyr"
            s150.Font.Size = 9
            s150.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s150.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s150.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s150.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s150.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s150.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s151
            '-----------------------------------------------
            Dim s151 As WorksheetStyle = styles.Add("s151")
            s151.Parent = "s43"
            s151.Font.FontName = "Arial Cyr"
            s151.Font.Size = 9
            s151.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s151.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s151.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s151.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s151.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s152
            '-----------------------------------------------
            Dim s152 As WorksheetStyle = styles.Add("s152")
            s152.Parent = "s43"
            s152.Font.FontName = "Arial Cyr"
            s152.Font.Size = 9
            s152.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s152.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s152.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s152.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s152.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s152.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s153
            '-----------------------------------------------
            Dim s153 As WorksheetStyle = styles.Add("s153")
            s153.Font.FontName = "Arial Cyr"
            s153.Font.Size = 9
            s153.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s153.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s153.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s153.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s153.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s153.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s154
            '-----------------------------------------------
            Dim s154 As WorksheetStyle = styles.Add("s154")
            s154.Font.FontName = "Arial Cyr"
            s154.Font.Size = 9
            s154.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s154.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s154.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s154.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s154.NumberFormat = "0.000"
            '-----------------------------------------------
            ' s155
            '-----------------------------------------------
            Dim s155 As WorksheetStyle = styles.Add("s155")
            s155.Font.FontName = "Arial Cyr"
            s155.Font.Size = 8
            s155.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s155.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s155.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s155.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s156
            '-----------------------------------------------
            Dim s156 As WorksheetStyle = styles.Add("s156")
            s156.Font.FontName = "Arial Cyr"
            s156.Font.Size = 9
            s156.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s156.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s156.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s156.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s156.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s157
            '-----------------------------------------------
            Dim s157 As WorksheetStyle = styles.Add("s157")
            s157.Font.FontName = "Arial Cyr"
            s157.Font.Size = 9
            s157.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s157.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s157.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s158
            '-----------------------------------------------
            Dim s158 As WorksheetStyle = styles.Add("s158")
            s158.Font.FontName = "Arial Cyr"
            s158.Font.Size = 9
            s158.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s158.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s158.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s158.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s158.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s158.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s158.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s159
            '-----------------------------------------------
            Dim s159 As WorksheetStyle = styles.Add("s159")
            s159.Font.FontName = "Arial Cyr"
            s159.Font.Size = 9
            s159.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s159.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s159.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s159.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s159.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s159.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s160
            '-----------------------------------------------
            Dim s160 As WorksheetStyle = styles.Add("s160")
            s160.Font.FontName = "Arial Cyr"
            s160.Font.Size = 9
            s160.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s160.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s160.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s160.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s160.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s160.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s160.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s161
            '-----------------------------------------------
            Dim s161 As WorksheetStyle = styles.Add("s161")
            s161.Font.FontName = "Arial Cyr"
            s161.Font.Size = 9
            s161.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s161.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s161.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s161.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s161.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s161.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s161.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s162
            '-----------------------------------------------
            Dim s162 As WorksheetStyle = styles.Add("s162")
            s162.Font.FontName = "Arial Cyr"
            s162.Font.Size = 9
            s162.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s162.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s162.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s162.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s162.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s162.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s163
            '-----------------------------------------------
            Dim s163 As WorksheetStyle = styles.Add("s163")
            s163.Font.FontName = "Arial Cyr"
            s163.Font.Size = 9
            s163.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s163.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s163.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s163.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s163.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s163.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s163.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s164
            '-----------------------------------------------
            Dim s164 As WorksheetStyle = styles.Add("s164")
            s164.Font.FontName = "Arial Cyr"
            s164.Font.Size = 9
            s164.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s164.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s164.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s164.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s165
            '-----------------------------------------------
            Dim s165 As WorksheetStyle = styles.Add("s165")
            s165.Font.FontName = "Arial Cyr"
            s165.Font.Size = 9
            s165.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s165.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s165.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s165.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s165.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s165.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s165.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s166
            '-----------------------------------------------
            Dim s166 As WorksheetStyle = styles.Add("s166")
            s166.Font.FontName = "Arial Cyr"
            s166.Font.Size = 9
            s166.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s166.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s166.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s167
            '-----------------------------------------------
            Dim s167 As WorksheetStyle = styles.Add("s167")
            s167.Font.FontName = "Arial Cyr"
            s167.Font.Size = 9
            s167.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s167.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s167.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s167.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s167.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s167.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s167.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s168
            '-----------------------------------------------
            Dim s168 As WorksheetStyle = styles.Add("s168")
            s168.Font.FontName = "Arial Cyr"
            s168.Font.Size = 9
            s168.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s168.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s168.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s168.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s168.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s168.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s169
            '-----------------------------------------------
            Dim s169 As WorksheetStyle = styles.Add("s169")
            s169.Font.FontName = "Arial Cyr"
            s169.Font.Size = 9
            s169.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s169.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s169.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s169.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s170
            '-----------------------------------------------
            Dim s170 As WorksheetStyle = styles.Add("s170")
            s170.Font.FontName = "Arial Cyr"
            s170.Font.Size = 9
            s170.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s170.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s170.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s170.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s171
            '-----------------------------------------------
            Dim s171 As WorksheetStyle = styles.Add("s171")
            s171.Font.FontName = "Arial Cyr"
            s171.Font.Size = 9
            s171.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s171.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s171.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s171.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s171.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s172
            '-----------------------------------------------
            Dim s172 As WorksheetStyle = styles.Add("s172")
            s172.Font.FontName = "Arial Cyr"
            s172.Font.Size = 9
            s172.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s172.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s172.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s172.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s172.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s172.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s172.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s173
            '-----------------------------------------------
            Dim s173 As WorksheetStyle = styles.Add("s173")
            s173.Font.FontName = "Arial Cyr"
            s173.Font.Size = 9
            s173.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s173.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s173.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s173.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s173.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s173.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s173.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s174
            '-----------------------------------------------
            Dim s174 As WorksheetStyle = styles.Add("s174")
            s174.Font.FontName = "Arial Cyr"
            s174.Font.Size = 9
            s174.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s174.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s174.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s175
            '-----------------------------------------------
            Dim s175 As WorksheetStyle = styles.Add("s175")
            s175.Font.FontName = "Arial Cyr"
            s175.Font.Size = 9
            s175.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s175.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s175.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s175.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s175.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s175.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s175.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s176
            '-----------------------------------------------
            Dim s176 As WorksheetStyle = styles.Add("s176")
            s176.Font.FontName = "Arial Cyr"
            s176.Font.Size = 9
            s176.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s176.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s176.NumberFormat = "0.000"
            '-----------------------------------------------
            ' s177
            '-----------------------------------------------
            Dim s177 As WorksheetStyle = styles.Add("s177")
            s177.Font.Bold = true
            s177.Font.FontName = "Arial Cyr"
            s177.Font.Size = 9
            '-----------------------------------------------
            ' s178
            '-----------------------------------------------
            Dim s178 As WorksheetStyle = styles.Add("s178")
            s178.Font.Bold = true
            s178.Font.FontName = "Arial Cyr"
            s178.Font.Size = 9
            s178.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s179
            '-----------------------------------------------
            Dim s179 As WorksheetStyle = styles.Add("s179")
            s179.Font.Bold = true
            s179.Font.FontName = "Arial Cyr"
            s179.Font.Size = 9
            s179.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s180
            '-----------------------------------------------
            Dim s180 As WorksheetStyle = styles.Add("s180")
            s180.Font.FontName = "Arial Cyr"
            s180.Font.Size = 8
            s180.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s180.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s180.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s180.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s181
            '-----------------------------------------------
            Dim s181 As WorksheetStyle = styles.Add("s181")
            s181.Font.FontName = "Arial Cyr"
            s181.Font.Size = 9
            s181.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s181.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s181.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s181.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s181.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s181.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s181.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s182
            '-----------------------------------------------
            Dim s182 As WorksheetStyle = styles.Add("s182")
            s182.Font.FontName = "Arial Cyr"
            s182.Font.Size = 9
            s182.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s182.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s182.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s182.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s182.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s183
            '-----------------------------------------------
            Dim s183 As WorksheetStyle = styles.Add("s183")
            s183.Font.FontName = "Arial Cyr"
            s183.Font.Size = 9
            s183.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s183.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s183.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s183.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s183.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s183.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s184
            '-----------------------------------------------
            Dim s184 As WorksheetStyle = styles.Add("s184")
            s184.Font.FontName = "Arial Cyr"
            s184.Font.Size = 9
            s184.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s184.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s184.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s184.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s184.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s184.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s184.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s185
            '-----------------------------------------------
            Dim s185 As WorksheetStyle = styles.Add("s185")
            s185.Font.FontName = "Arial Cyr"
            s185.Font.Size = 9
            s185.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s185.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s185.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s185.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s185.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s185.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s185.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s186
            '-----------------------------------------------
            Dim s186 As WorksheetStyle = styles.Add("s186")
            s186.Font.FontName = "Arial Cyr"
            s186.Font.Size = 9
            s186.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s186.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s186.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s186.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s186.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s186.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s187
            '-----------------------------------------------
            Dim s187 As WorksheetStyle = styles.Add("s187")
            s187.Font.FontName = "Arial Cyr"
            s187.Font.Size = 9
            s187.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s187.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s187.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s187.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s187.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s188
            '-----------------------------------------------
            Dim s188 As WorksheetStyle = styles.Add("s188")
            s188.Font.FontName = "Arial Cyr"
            s188.Font.Size = 9
            s188.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s188.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s188.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s188.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s189
            '-----------------------------------------------
            Dim s189 As WorksheetStyle = styles.Add("s189")
            s189.Font.FontName = "Arial Cyr"
            s189.Font.Size = 9
            s189.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s189.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s189.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s189.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s190
            '-----------------------------------------------
            Dim s190 As WorksheetStyle = styles.Add("s190")
            s190.Font.FontName = "Arial Cyr"
            s190.Font.Size = 9
            s190.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s190.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s190.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s191
            '-----------------------------------------------
            Dim s191 As WorksheetStyle = styles.Add("s191")
            s191.Font.FontName = "Arial Cyr"
            s191.Font.Size = 9
            s191.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s191.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s191.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s191.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s191.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s192
            '-----------------------------------------------
            Dim s192 As WorksheetStyle = styles.Add("s192")
            s192.Font.FontName = "Arial Cyr"
            s192.Font.Size = 9
            s192.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s192.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s192.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s192.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s192.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s192.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s193
            '-----------------------------------------------
            Dim s193 As WorksheetStyle = styles.Add("s193")
            s193.Font.Bold = true
            s193.Font.FontName = "Arial Cyr"
            s193.Font.Size = 9
            s193.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s193.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s194
            '-----------------------------------------------
            Dim s194 As WorksheetStyle = styles.Add("s194")
            s194.Font.FontName = "Arial Cyr"
            s194.Font.Size = 9
            s194.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s194.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s194.NumberFormat = "0"
            '-----------------------------------------------
            ' s195
            '-----------------------------------------------
            Dim s195 As WorksheetStyle = styles.Add("s195")
            s195.Font.FontName = "Arial Cyr"
            s195.Font.Size = 9
            s195.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s195.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s195.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s196
            '-----------------------------------------------
            Dim s196 As WorksheetStyle = styles.Add("s196")
            s196.Font.FontName = "Arial Cyr"
            s196.Font.Size = 9
            s196.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s197
            '-----------------------------------------------
            Dim s197 As WorksheetStyle = styles.Add("s197")
            s197.Font.FontName = "Arial Cyr"
            s197.Font.Size = 9
            s197.Alignment.Horizontal = StyleHorizontalAlignment.Right
            s197.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s198
            '-----------------------------------------------
            Dim s198 As WorksheetStyle = styles.Add("s198")
            s198.Font.Bold = true
            s198.Font.FontName = "Arial Cyr"
            '-----------------------------------------------
            ' s199
            '-----------------------------------------------
            Dim s199 As WorksheetStyle = styles.Add("s199")
            s199.Font.FontName = "Times New Roman"
            '-----------------------------------------------
            ' s200
            '-----------------------------------------------
            Dim s200 As WorksheetStyle = styles.Add("s200")
            s200.Font.Bold = true
            s200.Font.FontName = "Times New Roman"
            s200.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s200.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s201
            '-----------------------------------------------
            Dim s201 As WorksheetStyle = styles.Add("s201")
            s201.Font.FontName = "Times New Roman"
            s201.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s202
            '-----------------------------------------------
            Dim s202 As WorksheetStyle = styles.Add("s202")
            s202.Font.FontName = "Times New Roman"
            '-----------------------------------------------
            ' s203
            '-----------------------------------------------
            Dim s203 As WorksheetStyle = styles.Add("s203")
            s203.Font.Bold = true
            s203.Font.FontName = "Times New Roman"
            '-----------------------------------------------
            ' s204
            '-----------------------------------------------
            Dim s204 As WorksheetStyle = styles.Add("s204")
            s204.Font.FontName = "Times New Roman"
            s204.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s204.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s205
            '-----------------------------------------------
            Dim s205 As WorksheetStyle = styles.Add("s205")
            s205.Font.FontName = "Arial Cyr"
            s205.Font.Size = 8
            s205.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s205.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s205.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s205.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s205.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s206
            '-----------------------------------------------
            Dim s206 As WorksheetStyle = styles.Add("s206")
            s206.Font.Bold = true
            s206.Font.Italic = true
            s206.Font.FontName = "Arial Cyr"
            s206.Font.Size = 12
            '-----------------------------------------------
            ' s207
            '-----------------------------------------------
            Dim s207 As WorksheetStyle = styles.Add("s207")
            s207.Font.Bold = true
            s207.Font.FontName = "Arial Cyr"
            s207.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s207.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s207.NumberFormat = "mmmm\ yy"
            '-----------------------------------------------
            ' s208
            '-----------------------------------------------
            Dim s208 As WorksheetStyle = styles.Add("s208")
            s208.Font.Bold = true
            s208.Font.FontName = "Arial Cyr"
            s208.Font.Size = 12
            s208.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s208.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s209
            '-----------------------------------------------
            Dim s209 As WorksheetStyle = styles.Add("s209")
            s209.Font.FontName = "Arial Cyr"
            s209.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s209.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s210
            '-----------------------------------------------
            Dim s210 As WorksheetStyle = styles.Add("s210")
            s210.Font.Bold = true
            s210.Font.FontName = "Arial Cyr"
            s210.Font.Size = 12
            s210.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s210.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s211
            '-----------------------------------------------
            Dim s211 As WorksheetStyle = styles.Add("s211")
            s211.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s211.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s212
            '-----------------------------------------------
            Dim s212 As WorksheetStyle = styles.Add("s212")
            s212.Font.FontName = "Arial Cyr"
            s212.Font.Size = 16
            s212.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s212.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s213
            '-----------------------------------------------
            Dim s213 As WorksheetStyle = styles.Add("s213")
            s213.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s213.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s213.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s214
            '-----------------------------------------------
            Dim s214 As WorksheetStyle = styles.Add("s214")
            s214.Parent = "s16"
            s214.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s215
            '-----------------------------------------------
            Dim s215 As WorksheetStyle = styles.Add("s215")
            s215.Alignment.Horizontal = StyleHorizontalAlignment.CenterAcrossSelection
            s215.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s215.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s215.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s216
            '-----------------------------------------------
            Dim s216 As WorksheetStyle = styles.Add("s216")
            s216.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s216.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s216.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s216.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s216.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s217
            '-----------------------------------------------
            Dim s217 As WorksheetStyle = styles.Add("s217")
            s217.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s217.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s218
            '-----------------------------------------------
            Dim s218 As WorksheetStyle = styles.Add("s218")
            s218.Parent = "s16"
            s218.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s219
            '-----------------------------------------------
            Dim s219 As WorksheetStyle = styles.Add("s219")
            s219.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s220
            '-----------------------------------------------
            Dim s220 As WorksheetStyle = styles.Add("s220")
            s220.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s220.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s220.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s221
            '-----------------------------------------------
            Dim s221 As WorksheetStyle = styles.Add("s221")
            s221.Alignment.Horizontal = StyleHorizontalAlignment.CenterAcrossSelection
            s221.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s221.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s221.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s221.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s222
            '-----------------------------------------------
            Dim s222 As WorksheetStyle = styles.Add("s222")
            s222.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s222.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s223
            '-----------------------------------------------
            Dim s223 As WorksheetStyle = styles.Add("s223")
            s223.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s223.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s223.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s223.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s224
            '-----------------------------------------------
            Dim s224 As WorksheetStyle = styles.Add("s224")
            s224.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s224.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s224.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s224.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s224.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s225
            '-----------------------------------------------
            Dim s225 As WorksheetStyle = styles.Add("s225")
            s225.Parent = "s16"
            s225.Font.Underline = UnderlineStyle.[Single]
            s225.Font.FontName = "Arial Cyr"
            s225.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s226
            '-----------------------------------------------
            Dim s226 As WorksheetStyle = styles.Add("s226")
            s226.Alignment.Horizontal = StyleHorizontalAlignment.CenterAcrossSelection
            s226.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s226.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s226.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s226.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s226.NumberFormat = "d/m"
            '-----------------------------------------------
            ' s227
            '-----------------------------------------------
            Dim s227 As WorksheetStyle = styles.Add("s227")
            s227.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s227.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s227.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s228
            '-----------------------------------------------
            Dim s228 As WorksheetStyle = styles.Add("s228")
            s228.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s228.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s228.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s228.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s228.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s228.NumberFormat = "0%"
            '-----------------------------------------------
            ' s229
            '-----------------------------------------------
            Dim s229 As WorksheetStyle = styles.Add("s229")
            s229.Parent = "s43"
            s229.Font.FontName = "Arial Cyr"
            s229.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s229.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s229.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s229.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s229.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s229.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s230
            '-----------------------------------------------
            Dim s230 As WorksheetStyle = styles.Add("s230")
            s230.Parent = "s43"
            s230.Font.FontName = "Arial Cyr"
            s230.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s230.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s230.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s230.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s230.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s230.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s231
            '-----------------------------------------------
            Dim s231 As WorksheetStyle = styles.Add("s231")
            s231.Font.Bold = true
            s231.Font.FontName = "Arial Cyr"
            s231.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s231.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s231.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s232
            '-----------------------------------------------
            Dim s232 As WorksheetStyle = styles.Add("s232")
            s232.Font.Bold = true
            s232.Font.FontName = "Arial Cyr"
            s232.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s232.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s232.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s232.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s232.NumberFormat = "0%"
            '-----------------------------------------------
            ' s233
            '-----------------------------------------------
            Dim s233 As WorksheetStyle = styles.Add("s233")
            s233.Font.Bold = true
            s233.Font.FontName = "Arial Cyr"
            s233.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s233.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s233.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s233.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s233.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s234
            '-----------------------------------------------
            Dim s234 As WorksheetStyle = styles.Add("s234")
            s234.Font.Bold = true
            s234.Font.FontName = "Arial Cyr"
            s234.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s234.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s234.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s234.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s235
            '-----------------------------------------------
            Dim s235 As WorksheetStyle = styles.Add("s235")
            s235.Font.Bold = true
            s235.Font.FontName = "Arial Cyr"
            s235.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s235.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s235.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s235.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s236
            '-----------------------------------------------
            Dim s236 As WorksheetStyle = styles.Add("s236")
            s236.Font.Bold = true
            s236.Font.Italic = true
            s236.Font.FontName = "Arial Cyr"
            s236.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s237
            '-----------------------------------------------
            Dim s237 As WorksheetStyle = styles.Add("s237")
            s237.Font.Bold = true
            s237.Font.FontName = "Arial Cyr"
            s237.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s237.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s237.NumberFormat = "mmmm\ yy"
            '-----------------------------------------------
            ' s238
            '-----------------------------------------------
            Dim s238 As WorksheetStyle = styles.Add("s238")
            s238.Font.Bold = true
            s238.Font.FontName = "Arial Cyr"
            s238.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s238.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s238.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s240
            '-----------------------------------------------
            Dim s240 As WorksheetStyle = styles.Add("s240")
            s240.Font.Bold = true
            s240.Font.FontName = "Arial Cyr"
            s240.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s240.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s240.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s241
            '-----------------------------------------------
            Dim s241 As WorksheetStyle = styles.Add("s241")
            s241.Font.Bold = true
            s241.Font.FontName = "Arial Cyr"
            s241.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s241.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 2)
            s241.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s241.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s242
            '-----------------------------------------------
            Dim s242 As WorksheetStyle = styles.Add("s242")
            s242.Font.Bold = true
            s242.Font.FontName = "Arial Cyr"
            s242.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s242.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s243
            '-----------------------------------------------
            Dim s243 As WorksheetStyle = styles.Add("s243")
            s243.Font.Bold = true
            s243.Font.FontName = "Arial Cyr"
            s243.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s243.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s244
            '-----------------------------------------------
            Dim s244 As WorksheetStyle = styles.Add("s244")
            s244.Font.Bold = true
            s244.Font.FontName = "Arial Cyr"
            s244.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 2)
            s244.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s245
            '-----------------------------------------------
            Dim s245 As WorksheetStyle = styles.Add("s245")
            s245.Font.Bold = true
            s245.Font.FontName = "Arial Cyr"
            s245.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s245.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 2)
            s245.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            '-----------------------------------------------
            ' s246
            '-----------------------------------------------
            Dim s246 As WorksheetStyle = styles.Add("s246")
            s246.Font.Bold = true
            s246.Font.FontName = "Arial Cyr"
            s246.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s246.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s246.NumberFormat = "0.0"
            '-----------------------------------------------
            ' s247
            '-----------------------------------------------
            Dim s247 As WorksheetStyle = styles.Add("s247")
            s247.Font.Bold = true
            s247.Font.FontName = "Arial Cyr"
            s247.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s247.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 2)
            s247.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s287
            '-----------------------------------------------
            Dim s287 As WorksheetStyle = styles.Add("s287")
            s287.Font.Bold = true
            s287.Font.FontName = "Arial Cyr"
            s287.Font.Size = 9
            s287.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s287.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s287.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s288
            '-----------------------------------------------
            Dim s288 As WorksheetStyle = styles.Add("s288")
            s288.Font.Bold = true
            s288.Font.FontName = "Arial Cyr"
            s288.Font.Size = 9
            s288.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s288.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s288.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s288.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s288.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s288.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s289
            '-----------------------------------------------
            Dim s289 As WorksheetStyle = styles.Add("s289")
            s289.Font.Bold = true
            s289.Font.FontName = "Arial Cyr"
            s289.Font.Size = 9
            s289.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s289.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s289.NumberFormat = "mmmm\ yy"
            '-----------------------------------------------
            ' s290
            '-----------------------------------------------
            Dim s290 As WorksheetStyle = styles.Add("s290")
            s290.Font.FontName = "Arial Cyr"
            s290.Font.Size = 9
            s290.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s290.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s290.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s299
            '-----------------------------------------------
            Dim s299 As WorksheetStyle = styles.Add("s299")
            s299.Font.FontName = "Arial Cyr"
            s299.Font.Size = 9
            s299.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s299.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s299.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s299.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s299.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s299.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s299.NumberFormat = "dd/mm/yy\ h:mm"
            '-----------------------------------------------
            ' s304
            '-----------------------------------------------
            Dim s304 As WorksheetStyle = styles.Add("s304")
            s304.Font.FontName = "Arial Cyr"
            s304.Font.Size = 9
            s304.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s304.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s305
            '-----------------------------------------------
            Dim s305 As WorksheetStyle = styles.Add("s305")
            s305.Font.Bold = true
            s305.Font.FontName = "Arial Cyr"
            s305.Font.Size = 9
            s305.Alignment.Horizontal = StyleHorizontalAlignment.Left
            s305.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s305.NumberFormat = "@"
            '-----------------------------------------------
            ' s306
            '-----------------------------------------------
            Dim s306 As WorksheetStyle = styles.Add("s306")
            s306.Font.FontName = "Arial Cyr"
            s306.Font.Size = 9
            s306.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s306.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s306.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s306.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s309
            '-----------------------------------------------
            Dim s309 As WorksheetStyle = styles.Add("s309")
            s309.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s309.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s309.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s309.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s309.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s309.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            '-----------------------------------------------
            ' s310
            '-----------------------------------------------
            Dim s310 As WorksheetStyle = styles.Add("s310")
            s310.Font.Bold = true
            s310.Font.Italic = true
            s310.Font.FontName = "Arial Cyr"
            s310.Font.Size = 12
            s310.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s310.Alignment.Vertical = StyleVerticalAlignment.Bottom
            '-----------------------------------------------
            ' s317
            '-----------------------------------------------
            Dim s317 As WorksheetStyle = styles.Add("s317")
            s317.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s317.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s317.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s317.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s317.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s317.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s317.NumberFormat = "Fixed"
            '-----------------------------------------------
            ' s320
            '-----------------------------------------------
            Dim s320 As WorksheetStyle = styles.Add("s320")
            s320.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s320.Alignment.Vertical = StyleVerticalAlignment.Bottom
            s320.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            s320.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            s320.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            s320.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
            s320.NumberFormat = "General Date"
            '-----------------------------------------------
            ' s321
            '-----------------------------------------------
            Dim s321 As WorksheetStyle = styles.Add("s321")
            s321.Alignment.Horizontal = StyleHorizontalAlignment.Center
            s321.Alignment.Vertical = StyleVerticalAlignment.Bottom
        End Sub
        
        Private Overloads Sub GenerateWorksheetSheet(ByVal sheets As WorksheetCollection)
            Dim sheet As Worksheet = sheets.Add("Sheet")
            sheet.Table.ExpandedColumnCount = 99
            sheet.Table.ExpandedRowCount = 62
            sheet.Table.FullColumns = 1
            sheet.Table.FullRows = 1
            sheet.Table.Columns.Add(105)
            Dim column1 As WorksheetColumn = sheet.Table.Columns.Add
            column1.Width = 39
            column1.StyleID = "s96"
            Dim column2 As WorksheetColumn = sheet.Table.Columns.Add
            column2.Width = 28
            column2.StyleID = "s96"
            Dim column3 As WorksheetColumn = sheet.Table.Columns.Add
            column3.Width = 18
            column3.StyleID = "s96"
            Dim column4 As WorksheetColumn = sheet.Table.Columns.Add
            column4.Width = 33
            column4.StyleID = "s96"
            column4.Span = 1
            Dim column5 As WorksheetColumn = sheet.Table.Columns.Add
            column5.Index = 7
            column5.Width = 30
            column5.StyleID = "s96"
            Dim column6 As WorksheetColumn = sheet.Table.Columns.Add
            column6.Width = 32
            column6.StyleID = "s96"
            column6.Span = 2
            Dim column7 As WorksheetColumn = sheet.Table.Columns.Add
            column7.Index = 11
            column7.Width = 23
            column7.StyleID = "s96"
            column7.Span = 1
            Dim column8 As WorksheetColumn = sheet.Table.Columns.Add
            column8.Index = 13
            column8.Width = 33
            column8.StyleID = "s96"
            Dim column9 As WorksheetColumn = sheet.Table.Columns.Add
            column9.Width = 22
            column9.StyleID = "s96"
            column9.Span = 2
            Dim column10 As WorksheetColumn = sheet.Table.Columns.Add
            column10.Index = 17
            column10.Width = 21
            column10.StyleID = "s96"
            Dim column11 As WorksheetColumn = sheet.Table.Columns.Add
            column11.Width = 26
            column11.StyleID = "s96"
            Dim column12 As WorksheetColumn = sheet.Table.Columns.Add
            column12.Width = 39
            column12.StyleID = "s96"
            Dim column13 As WorksheetColumn = sheet.Table.Columns.Add
            column13.Width = 39
            column13.StyleID = "s96"
            Dim column14 As WorksheetColumn = sheet.Table.Columns.Add
            column14.Width = 35
            column14.StyleID = "s96"
            Dim column15 As WorksheetColumn = sheet.Table.Columns.Add
            column15.Width = 35
            column15.Span = 77
            '-----------------------------------------------
            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row0.Height = 11
            Row0.AutoFitHeight = false
            Row0.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Dim cell As WorksheetCell
            cell = Row0.Cells.Add
            cell.StyleID = "s97"
            cell.Index = 3
            cell = Row0.Cells.Add
            cell.StyleID = "s97"
            Row0.Cells.Add("????? ? ???????????????? ?? ???????? ???? ", DataType.[String], "s98")
            cell = Row0.Cells.Add
            cell.StyleID = "s289"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 13
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.Height = 11
            Row1.AutoFitHeight = false
            Row1.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row1.Cells.Add("???????: ??? ""???"" ????????????? ??????   ???????:", DataType.[String], "s99")
            cell = Row1.Cells.Add
            cell.StyleID = "s97"
            cell = Row1.Cells.Add
            cell.StyleID = "s97"
            cell = Row1.Cells.Add
            cell.StyleID = "s98"
            cell = Row1.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????]"
            cell.Index = 10
            cell.MergeAcross = 2
            cell = Row1.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???. 575-22-48"
            cell = Row1.Cells.Add
            cell.StyleID = "s101"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???? ?????:      "
            cell.Index = 16
            cell = Row1.Cells.Add
            cell.StyleID = "s102"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???? ?????]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            Row2.Height = 11
            Row2.AutoFitHeight = false
            Row2.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            Row2.Cells.Add("[??? ???? ?????]", DataType.[String], "s100")
            cell = Row2.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ???? ?????]"
            cell.Index = 5
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????? ?????:"
            cell.Index = 9
            cell = Row2.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???.?????]"
            cell.Index = 13
            cell = Row2.Cells.Add
            cell.StyleID = "s304"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??? ????:"
            cell.Index = 16
            cell.MergeAcross = 1
            cell = Row2.Cells.Add
            cell.StyleID = "s305"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??? ????]"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
            Row3.Height = 11
            Row3.AutoFitHeight = false
            Row3.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ???????????:     ??? ???  ???  ?????????????? ??????? ??? ""???"""
            cell = Row3.Cells.Add
            cell.StyleID = "s104"
            cell = Row3.Cells.Add
            cell.StyleID = "s104"
            cell = Row3.Cells.Add
            cell.StyleID = "s99"
            cell.Index = 8
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:  575-22-44"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row4 As WorksheetRow = sheet.Table.Rows.Add
            Row4.Height = 11
            Row4.AutoFitHeight = false
            Row4.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row4.Cells.Add("????????:", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[????????]", DataType.[String], "s106")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("????? ???????????:", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s107"
            Row4.Cells.Add("[????_????????????????]", DataType.[String], "s108")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("??????", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[?_??????]", DataType.[String], "s108")
            '-----------------------------------------------
            Dim Row5 As WorksheetRow = sheet.Table.Rows.Add
            Row5.Height = 11
            Row5.AutoFitHeight = false
            Row5.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ???????:"
            cell = Row5.Cells.Add
            cell.StyleID = "s104"
            cell = Row5.Cells.Add
            cell.StyleID = "s109"
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????? ?????? ? ?????:"
            cell.Index = 7
            cell = Row5.Cells.Add
            cell.StyleID = "s99"
            cell.Index = 9
            cell = Row5.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????_?????]"
            cell.Index = 11
            cell = Row5.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 15
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???,??.? ="
            cell.Index = 17
            cell = Row5.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
            Row6.Height = 11
            Row6.AutoFitHeight = false
            Row6.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????:"
            cell = Row6.Cells.Add
            cell.StyleID = "s104"
            Row6.Cells.Add("[????? ????????]", DataType.[String], "s110")
            cell = Row6.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[? ???????]"
            cell.Index = 6
            cell = Row6.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????(?????):"
            cell.Index = 8
            cell = Row6.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????]"
            cell.Index = 10
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????  ???? ????????  ?? :"
            cell.Index = 12
            cell = Row6.Cells.Add
            cell.StyleID = "s109"
            cell = Row6.Cells.Add
            cell.StyleID = "s109"
            cell = Row6.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???? ???????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row7 As WorksheetRow = sheet.Table.Rows.Add
            Row7.Height = 11
            Row7.AutoFitHeight = false
            Row7.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????.??.(?1):   "
            cell.MergeAcross = 2
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row7.Cells.Add("[??????????]", DataType.[String], "s103")
            cell = Row7.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row7.Cells.Add("[G??_min??]", DataType.[String], "s100")
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row7.Cells.Add("[G????]", DataType.[String], "s100")
            cell = Row7.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row7.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row8 As WorksheetRow = sheet.Table.Rows.Add
            Row8.Height = 11
            Row8.AutoFitHeight = false
            Row8.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????.??.(?2):   "
            cell.MergeAcross = 2
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row8.Cells.Add("[??????????]", DataType.[String], "s103")
            cell = Row8.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row8.Cells.Add("[G??_min??]", DataType.[String], "s100")
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row8.Cells.Add("[G????]", DataType.[String], "s100")
            cell = Row8.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row8.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row9 As WorksheetRow = sheet.Table.Rows.Add
            Row9.Height = 11
            Row9.AutoFitHeight = false
            Row9.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??-? ???(?3):   "
            cell.MergeAcross = 2
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row9.Cells.Add("[?????????? ???]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row9.Cells.Add("[G??(??? min)]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row9.Cells.Add("[G(???)??]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row9.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????? ???]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row10 As WorksheetRow = sheet.Table.Rows.Add
            Row10.Height = 11
            Row10.AutoFitHeight = false
            Row10.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??.????.???(?4):  "
            cell.MergeAcross = 2
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s112"
            Row10.Cells.Add("???????????.:", DataType.[String], "s109")
            cell = Row10.Cells.Add
            cell.StyleID = "s100"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row11 As WorksheetRow = sheet.Table.Rows.Add
            Row11.Height = 11
            Row11.AutoFitHeight = false
            Row11.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??.????????(V5):   "
            cell.MergeAcross = 2
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            cell = Row11.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.Index = 11
            cell.MergeAcross = 1
            cell = Row11.Cells.Add
            cell.StyleID = "s109"
            cell = Row11.Cells.Add
            cell.StyleID = "s112"
            Row11.Cells.Add("???????????.:", DataType.[String], "s109")
            cell = Row11.Cells.Add
            cell.StyleID = "s110"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row12 As WorksheetRow = sheet.Table.Rows.Add
            Row12.Height = 11
            Row12.AutoFitHeight = false
            Row12.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row12.Cells.Add("????????? ????????:", DataType.[String], "s105")
            cell = Row12.Cells.Add
            cell.StyleID = "s113"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            Row12.Cells.Add("????:", DataType.[String], "s105")
            Row12.Cells.Add("[???????]", DataType.[String], "s108")
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s107"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s114"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????:"
            cell.MergeAcross = 1
            Row12.Cells.Add("[???????2]", DataType.[String], "s108")
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            '-----------------------------------------------
            Dim Row13 As WorksheetRow = sheet.Table.Rows.Add
            Row13.Height = 11
            Row13.AutoFitHeight = false
            Row13.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ????????,????/???:"
            cell = Row13.Cells.Add
            cell.StyleID = "s104"
            cell = Row13.Cells.Add
            cell.StyleID = "s104"
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q??.="
            cell.Index = 7
            Row13.Cells.Add("[Q??]", DataType.[String], "s115")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q????.="
            Row13.Cells.Add("[Q????]", DataType.[String], "s115")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.="
            cell.Index = 12
            Row13.Cells.Add("[Q???]", DataType.[String], "s116")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.???="
            cell = Row13.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q???_???]"
            cell.Index = 16
            cell = Row13.Cells.Add
            cell.StyleID = "s117"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.="
            cell.Index = 18
            Row13.Cells.Add("[Q???]", DataType.[String], "s115")
            '-----------------------------------------------
            Dim Row14 As WorksheetRow = sheet.Table.Rows.Add
            Row14.Height = 11
            Row14.AutoFitHeight = false
            Row14.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ???????? (??.???),????/???:"
            cell = Row14.Cells.Add
            cell.StyleID = "s104"
            cell = Row14.Cells.Add
            cell.StyleID = "s104"
            cell = Row14.Cells.Add
            cell.StyleID = "s110"
            cell.Index = 7
            cell = Row14.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.??? ??="
            cell.Index = 12
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q???_??? ??]"
            cell.Index = 15
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.??="
            cell.Index = 17
            cell = Row14.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??? ??]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row15 As WorksheetRow = sheet.Table.Rows.Add
            Row15.Height = 11
            Row15.AutoFitHeight = false
            Row15.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????. ???????? (??.???),?/???:"
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G??.="
            cell.Index = 7
            Row15.Cells.Add("[G??]", DataType.[String], "s115")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G????.="
            Row15.Cells.Add("[G????]", DataType.[String], "s115")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G???.="
            cell.Index = 13
            Row15.Cells.Add("[G???]", DataType.[String], "s116")
            Row15.Cells.Add("G???.=", DataType.[String], "s111")
            Row15.Cells.Add("[G???]", DataType.[String], "s100")
            cell = Row15.Cells.Add
            cell.StyleID = "s116"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "  G???.? ="
            cell.Index = 18
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G???_?]"
            '-----------------------------------------------
            Dim Row16 As WorksheetRow = sheet.Table.Rows.Add
            Row16.Height = 11
            Row16.AutoFitHeight = false
            cell = Row16.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row16.Cells.Add("?????.????????:", DataType.[String], "s105")
            cell = Row16.Cells.Add
            cell.StyleID = "s118"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            Row16.Cells.Add("?1=", DataType.[String], "s114")
            Row16.Cells.Add("[???]", DataType.[String], "s118")
            Row16.Cells.Add("?2=", DataType.[String], "s114")
            Row16.Cells.Add("[????]", DataType.[String], "s107")
            Row16.Cells.Add("    t1=", DataType.[String], "s114")
            Row16.Cells.Add("[?1]", DataType.[String], "s119")
            Row16.Cells.Add("    t2=", DataType.[String], "s114")
            Row16.Cells.Add("[?2]", DataType.[String], "s119")
            Row16.Cells.Add("t3=", DataType.[String], "s114")
            cell = Row16.Cells.Add
            cell.StyleID = "s119"
            Row16.Cells.Add("t4=", DataType.[String], "s105")
            cell = Row16.Cells.Add
            cell.StyleID = "s119"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            '-----------------------------------------------
            Dim Row17 As WorksheetRow = sheet.Table.Rows.Add
            Row17.Height = 11
            Row17.AutoFitHeight = false
            cell = Row17.Cells.Add
            cell.StyleID = "s112"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ?????????? ???????? ??????"
            cell.Index = 2
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell = Row17.Cells.Add
            cell.StyleID = "s120"
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 7
            cell = Row17.Cells.Add
            cell.StyleID = "s121"
            cell.Index = 10
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row18 As WorksheetRow = sheet.Table.Rows.Add
            Row18.Height = 11
            Row18.AutoFitHeight = false
            cell = Row18.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 2
            cell = Row18.Cells.Add
            cell.StyleID = "s116"
            cell = Row18.Cells.Add
            cell.StyleID = "m80092384"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???? (??-1)"
            cell.Index = 5
            cell.MergeAcross = 7
            cell = Row18.Cells.Add
            cell.StyleID = "m80092000"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? (??-2)"
            cell.MergeAcross = 5
            cell = Row18.Cells.Add
            cell.StyleID = "s112"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row19 As WorksheetRow = sheet.Table.Rows.Add
            Row19.Height = 11
            Row19.AutoFitHeight = false
            cell = Row19.Cells.Add
            cell.StyleID = "m80091956"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeDown = 1
            Row19.Cells.Add("??", DataType.[String], "s122")
            Row19.Cells.Add("??", DataType.[String], "s123")
            Row19.Cells.Add("    ?1,", DataType.[String], "s124")
            Row19.Cells.Add("    ?2,", DataType.[String], "s125")
            Row19.Cells.Add("d?,", DataType.[String], "s126")
            Row19.Cells.Add("?1,", DataType.[String], "s127")
            Row19.Cells.Add("?2,", DataType.[String], "s122")
            Row19.Cells.Add("d?,", DataType.[String], "s126")
            Row19.Cells.Add(" P1,", DataType.[String], "s127")
            Row19.Cells.Add(" P2,", DataType.[String], "s126")
            Row19.Cells.Add("?3", DataType.[String], "s127")
            Row19.Cells.Add("M4,", DataType.[String], "s128")
            Row19.Cells.Add("dM,", DataType.[String], "s122")
            Row19.Cells.Add("V5,", DataType.[String], "s129")
            Row19.Cells.Add("?3,", DataType.[String], "s127")
            Row19.Cells.Add("?4,", DataType.[String], "s129")
            Row19.Cells.Add(" Q", DataType.[String], "s130")
            cell = Row19.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row20 As WorksheetRow = sheet.Table.Rows.Add
            Row20.Height = 11
            Row20.AutoFitHeight = false
            cell = Row20.Cells.Add
            cell.StyleID = "s131"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???"
            cell.Index = 3
            cell = Row20.Cells.Add
            cell.StyleID = "s132"
            Row20.Cells.Add("  ?", DataType.[String], "s133")
            Row20.Cells.Add("  ?", DataType.[String], "s131")
            Row20.Cells.Add("  ?", DataType.[String], "s134")
            Row20.Cells.Add("?", DataType.[String], "s135")
            Row20.Cells.Add("?", DataType.[String], "s131")
            Row20.Cells.Add("?", DataType.[String], "s134")
            Row20.Cells.Add("??/??2", DataType.[String], "s134")
            Row20.Cells.Add("??/??2", DataType.[String], "s134")
            Row20.Cells.Add("  ?", DataType.[String], "s136")
            Row20.Cells.Add("?", DataType.[String], "s137")
            Row20.Cells.Add("?", DataType.[String], "s131")
            Row20.Cells.Add("?.???", DataType.[String], "s138")
            Row20.Cells.Add("?", DataType.[String], "s135")
            Row20.Cells.Add("?", DataType.[String], "s132")
            Row20.Cells.Add("  ????", DataType.[String], "s139")
            cell = Row20.Cells.Add
            cell.StyleID = "s140"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row21 As WorksheetRow = sheet.Table.Rows.Add
            Row21.Height = 11
            Row21.AutoFitHeight = false
            Row21.Cells.Add("[Report:?????]", DataType.[String], "s141")
            Row21.Cells.Add("[????? ??????]", DataType.[String], "s142")
            Row21.Cells.Add("24", DataType.Number, "s143")
            cell = Row21.Cells.Add
            cell.StyleID = "s144"
            Row21.Cells.Add("[M1 ???]", DataType.[String], "s145")
            Row21.Cells.Add("[?2 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s147"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-2]-RC[-1]"
            Row21.Cells.Add("[T1 ???]", DataType.[String], "s148")
            Row21.Cells.Add("[T2 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s147"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-2]-RC[-1]"
            cell = Row21.Cells.Add
            cell.StyleID = "s149"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R17C6*1"
            cell = Row21.Cells.Add
            cell.StyleID = "s150"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R17C8*1"
            cell = Row21.Cells.Add
            cell.StyleID = "s151"
            Row21.Cells.Add("[?4 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s146"
            cell = Row21.Cells.Add
            cell.StyleID = "s152"
            cell = Row21.Cells.Add
            cell.StyleID = "s148"
            Row21.Cells.Add("[T4 ???]", DataType.[String], "s152")
            Row21.Cells.Add("[Q1 ???]", DataType.[String], "s153")
            cell = Row21.Cells.Add
            cell.StyleID = "s154"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q2 ???]"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row22 As WorksheetRow = sheet.Table.Rows.Add
            Row22.Height = 11
            Row22.AutoFitHeight = false
            cell = Row22.Cells.Add
            cell.StyleID = "s155"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row22.Cells.Add
            cell.StyleID = "s156"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s157"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s162"
            cell = Row22.Cells.Add
            cell.StyleID = "s162"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s160"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s163"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Index = 21
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            '-----------------------------------------------
            Dim Row23 As WorksheetRow = sheet.Table.Rows.Add
            Row23.Height = 11
            Row23.AutoFitHeight = false
            cell = Row23.Cells.Add
            cell.StyleID = "s164"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????"
            cell.Index = 2
            cell = Row23.Cells.Add
            cell.StyleID = "s165"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s166"
            cell = Row23.Cells.Add
            cell.StyleID = "s167"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s169"
            cell = Row23.Cells.Add
            cell.StyleID = "s170"
            cell = Row23.Cells.Add
            cell.StyleID = "s166"
            cell = Row23.Cells.Add
            cell.StyleID = "s169"
            cell = Row23.Cells.Add
            cell.StyleID = "s171"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s172"
            cell = Row23.Cells.Add
            cell.StyleID = "s173"
            cell = Row23.Cells.Add
            cell.StyleID = "s167"
            cell = Row23.Cells.Add
            cell.StyleID = "s174"
            cell = Row23.Cells.Add
            cell.StyleID = "s175"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s176"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Index = 21
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            '-----------------------------------------------
            Dim Row24 As WorksheetRow = sheet.Table.Rows.Add
            Row24.Height = 11
            Row24.AutoFitHeight = false
            cell = Row24.Cells.Add
            cell.StyleID = "s177"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ?? ???????? ?????:"
            cell.Index = 2
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s179"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s179"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            '-----------------------------------------------
            Dim Row25 As WorksheetRow = sheet.Table.Rows.Add
            Row25.Height = 11
            Row25.AutoFitHeight = false
            cell = Row25.Cells.Add
            cell.StyleID = "s180"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row25.Cells.Add
            cell.StyleID = "s181"
            cell = Row25.Cells.Add
            cell.StyleID = "s182"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s186"
            '-----------------------------------------------
            Dim Row26 As WorksheetRow = sheet.Table.Rows.Add
            Row26.Height = 11
            Row26.AutoFitHeight = false
            cell = Row26.Cells.Add
            cell.StyleID = "s164"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row26.Cells.Add
            cell.StyleID = "s165"
            cell = Row26.Cells.Add
            cell.StyleID = "s187"
            cell = Row26.Cells.Add
            cell.StyleID = "s168"
            cell = Row26.Cells.Add
            cell.StyleID = "s172"
            cell = Row26.Cells.Add
            cell.StyleID = "s173"
            cell = Row26.Cells.Add
            cell.StyleID = "s188"
            cell = Row26.Cells.Add
            cell.StyleID = "s170"
            cell = Row26.Cells.Add
            cell.StyleID = "s189"
            cell = Row26.Cells.Add
            cell.StyleID = "s190"
            cell = Row26.Cells.Add
            cell.StyleID = "s171"
            cell = Row26.Cells.Add
            cell.StyleID = "s168"
            cell = Row26.Cells.Add
            cell.StyleID = "s172"
            cell = Row26.Cells.Add
            cell.StyleID = "s191"
            cell = Row26.Cells.Add
            cell.StyleID = "s187"
            cell = Row26.Cells.Add
            cell.StyleID = "s188"
            cell = Row26.Cells.Add
            cell.StyleID = "s171"
            cell = Row26.Cells.Add
            cell.StyleID = "s192"
            '-----------------------------------------------
            Dim Row27 As WorksheetRow = sheet.Table.Rows.Add
            Row27.Height = 11
            Row27.AutoFitHeight = false
            cell = Row27.Cells.Add
            cell.StyleID = "s112"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ????????? ?? ?????? ?????? ??????:"
            cell.Index = 2
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 5
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            '-----------------------------------------------
            Dim Row28 As WorksheetRow = sheet.Table.Rows.Add
            Row28.Height = 11
            Row28.AutoFitHeight = false
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?1, ?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?2, ?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?3,?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?4,?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "V5,?.???"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "m80091672"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q, ????"
            cell.MergeAcross = 2
            cell = Row28.Cells.Add
            cell.StyleID = "m80091652"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??,?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s193"
            cell = Row28.Cells.Add
            cell.StyleID = "m80091572"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???, ????"
            cell.Index = 21
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row29 As WorksheetRow = sheet.Table.Rows.Add
            Row29.Height = 11
            Row29.AutoFitHeight = false
            cell = Row29.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:????????]"
            cell = Row29.Cells.Add
            cell.StyleID = "s299"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ??????]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M1 ???]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M2 ???]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M4 ???]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "m80091856"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??1 ???]"
            cell.MergeAcross = 2
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[TSUM1]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s194"
            cell = Row29.Cells.Add
            cell.StyleID = "m80091552"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??2 ???]"
            cell.Index = 21
            cell.MergeAcross = 2
            cell = Row29.Cells.Add
            cell.StyleID = "s195"
            '-----------------------------------------------
            Dim Row30 As WorksheetRow = sheet.Table.Rows.Add
            Row30.Height = 11
            Row30.AutoFitHeight = false
            cell = Row30.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ??????? ?? ????????:"
            cell.Index = 2
            '-----------------------------------------------
            Dim Row31 As WorksheetRow = sheet.Table.Rows.Add
            Row31.Height = 11
            Row31.AutoFitHeight = false
            cell = Row31.Cells.Add
            cell.StyleID = "s96"
            cell = Row31.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ??????? ?? ????????:"
            cell = Row31.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 15
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row32 As WorksheetRow = sheet.Table.Rows.Add
            Row32.Height = 11
            Row32.AutoFitHeight = false
            cell = Row32.Cells.Add
            cell.StyleID = "s96"
            cell = Row32.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?????????? t2:"
            '-----------------------------------------------
            Dim Row33 As WorksheetRow = sheet.Table.Rows.Add
            Row33.Height = 11
            Row33.AutoFitHeight = false
            cell = Row33.Cells.Add
            cell.StyleID = "s96"
            cell = Row33.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????? ??? ?????? ???????? ""??????????""??? ""??? ???"""
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell.Index = 11
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "/"
            cell.Index = 15
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            Row33.Cells.Add("/", DataType.[String], "s114")
            '-----------------------------------------------
            Dim Row34 As WorksheetRow = sheet.Table.Rows.Add
            Row34.Height = 11
            Row34.AutoFitHeight = false
            cell = Row34.Cells.Add
            cell.StyleID = "s96"
            cell = Row34.Cells.Add
            cell.StyleID = "s196"
            cell.Index = 11
            cell = Row34.Cells.Add
            cell.StyleID = "s196"
            '-----------------------------------------------
            Dim Row35 As WorksheetRow = sheet.Table.Rows.Add
            Row35.Height = 11
            Row35.AutoFitHeight = false
            cell = Row35.Cells.Add
            cell.StyleID = "s96"
            cell = Row35.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ?? ???? ???????? ??????? (?? ????????)"
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell.Index = 11
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "/"
            cell.Index = 15
            Row35.Cells.Add("????? ?.?.   /", DataType.[String], "s105")
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell = Row35.Cells.Add
            cell.StyleID = "s114"
            cell = Row35.Cells.Add
            cell.StyleID = "s197"
            '-----------------------------------------------
            Dim Row36 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row36.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row37 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row37.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row38 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row38.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row39 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row39.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row40 As WorksheetRow = sheet.Table.Rows.Add
            Row40.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row40.Cells.Add
            cell.StyleID = "s198"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??? ? ????????????????  ??  "
            cell.Index = 6
            cell = Row40.Cells.Add
            cell.StyleID = "s289"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 11
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row41 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row41.Cells.Add
            cell.StyleID = "s96"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row42 As WorksheetRow = sheet.Table.Rows.Add
            Row42.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row42.Cells.Add("??, ?????????????????: ????????? ?????????? ?????????? ???????? ?????????? ??? ""?"& _ 
                    "?? ???""", DataType.[String], "s199")
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            Row42.Cells.Add("[???????]", DataType.[String], "s200")
            '-----------------------------------------------
            Dim Row43 As WorksheetRow = sheet.Table.Rows.Add
            Row43.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row43.Cells.Add("? ????????????? ????????", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("[??? ???? ?????]", DataType.[String], "s200")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("?????????????? ?? ??????:", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 10
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("[????? ???? ?????]", DataType.[String], "s200")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 15
            Row43.Cells.Add(" ????????? ", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row44 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??? ? ???, ??? ?? ??????     ???????????? ???????????????? ??????????? "& _ 
                "?????????,"
            cell.Index = 2
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row45 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "? ??????? ?????? ???????? ??????? ? ??????????:"
            cell.Index = 2
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row46 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ?????????? ???????? ??????:"
            cell.Index = 2
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            Row46.Cells.Add("=", DataType.[String], "s199")
            cell = Row46.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row46.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row47 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??????????? ?? ?????????? ??????:"
            cell.Index = 2
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            Row47.Cells.Add("=", DataType.[String], "s199")
            cell = Row47.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row47.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row48 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ??????? ???????? ??????:"
            cell.Index = 2
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            Row48.Cells.Add("=", DataType.[String], "s199")
            cell = Row48.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row48.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row49 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??????????? ?? ????? ??????:"
            cell.Index = 2
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            Row49.Cells.Add("=", DataType.[String], "s199")
            cell = Row49.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row49.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row50 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ?? ??????????? ???????? ???? ?? ????????? ??????????????"
            cell.Index = 2
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            Row50.Cells.Add("??.?", DataType.[String], "s199")
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            Row50.Cells.Add("=", DataType.[String], "s199")
            cell = Row50.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row50.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row51 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ???????? ?? ??????:"
            cell.Index = 2
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            Row51.Cells.Add("=", DataType.[String], "s199")
            cell = Row51.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row51.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row52 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ?????????? ????????? ?? ??????:"
            cell.Index = 2
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            Row52.Cells.Add("=", DataType.[String], "s199")
            cell = Row52.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row52.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row53 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ????????? ?????????? ????????????:"
            cell.Index = 2
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            Row53.Cells.Add("=", DataType.[String], "s201")
            cell = Row53.Cells.Add
            cell.StyleID = "s306"
            cell.MergeAcross = 1
            Row53.Cells.Add("????", DataType.[String], "s201")
            '-----------------------------------------------
            Dim Row54 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell.Index = 2
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell.Index = 18
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row55 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row55.Cells.Add
            cell.StyleID = "s203"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            Row55.Cells.Add("=", DataType.[String], "s203")
            cell = Row55.Cells.Add
            cell.StyleID = "s287"
            cell.MergeAcross = 1
            Row55.Cells.Add("????", DataType.[String], "s203")
            '-----------------------------------------------
            Dim Row56 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 18
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row57 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row58 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 13
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row59 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ????????"
            cell.Index = 2
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s201"
            cell.Index = 13
            cell = Row59.Cells.Add
            cell.StyleID = "s201"
            cell = Row59.Cells.Add
            cell.StyleID = "s201"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            Row59.Cells.Add("????? ?.?.", DataType.[String], "s199")
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row60 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 13
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row61 As WorksheetRow = sheet.Table.Rows.Add
            Row61.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row61.Cells.Add("????????? ?????????? ?????????? ???????? ""??????????"" ???""??? ???""", DataType.[String], "s199")
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s201"
            cell.Index = 13
            cell = Row61.Cells.Add
            cell.StyleID = "s201"
            cell = Row61.Cells.Add
            cell.StyleID = "s201"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            Row61.Cells.Add("[???????]", DataType.[String], "s204")
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            ' Options
            '-----------------------------------------------
            sheet.Options.ProtectObjects = false
            sheet.Options.ProtectScenarios = false
            sheet.Options.PageSetup.Header.Margin = 0!
            sheet.Options.PageSetup.Footer.Margin = 0!
            sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008!
            sheet.Options.PageSetup.PageMargins.Left = 0.511811!
            sheet.Options.PageSetup.PageMargins.Right = 0.3149606!
            sheet.Options.PageSetup.PageMargins.Top = 0.3149606!
            sheet.Options.Print.PaperSizeIndex = 9
            sheet.Options.Print.HorizontalResolution = -4
            sheet.Options.Print.VerticalResolution = 180
            sheet.Options.Print.ValidPrinterInfo = true
        End Sub
        
        Private Overloads Sub GenerateWorksheetSheet(ByVal sheets As WorksheetCollection)
            Dim sheet As Worksheet = sheets.Add("Sheet")
            sheet.Table.ExpandedColumnCount = 99
            sheet.Table.ExpandedRowCount = 59
            sheet.Table.FullColumns = 1
            sheet.Table.FullRows = 1
            sheet.Table.Columns.Add(105)
            Dim column1 As WorksheetColumn = sheet.Table.Columns.Add
            column1.Width = 39
            column1.StyleID = "s96"
            Dim column2 As WorksheetColumn = sheet.Table.Columns.Add
            column2.Width = 28
            column2.StyleID = "s96"
            Dim column3 As WorksheetColumn = sheet.Table.Columns.Add
            column3.Width = 18
            column3.StyleID = "s96"
            Dim column4 As WorksheetColumn = sheet.Table.Columns.Add
            column4.Width = 33
            column4.StyleID = "s96"
            column4.Span = 1
            Dim column5 As WorksheetColumn = sheet.Table.Columns.Add
            column5.Index = 7
            column5.Width = 30
            column5.StyleID = "s96"
            Dim column6 As WorksheetColumn = sheet.Table.Columns.Add
            column6.Width = 32
            column6.StyleID = "s96"
            column6.Span = 2
            Dim column7 As WorksheetColumn = sheet.Table.Columns.Add
            column7.Index = 11
            column7.Width = 23
            column7.StyleID = "s96"
            column7.Span = 1
            Dim column8 As WorksheetColumn = sheet.Table.Columns.Add
            column8.Index = 13
            column8.Width = 33
            column8.StyleID = "s96"
            Dim column9 As WorksheetColumn = sheet.Table.Columns.Add
            column9.Width = 22
            column9.StyleID = "s96"
            column9.Span = 2
            Dim column10 As WorksheetColumn = sheet.Table.Columns.Add
            column10.Index = 17
            column10.Width = 21
            column10.StyleID = "s96"
            Dim column11 As WorksheetColumn = sheet.Table.Columns.Add
            column11.Width = 26
            column11.StyleID = "s96"
            Dim column12 As WorksheetColumn = sheet.Table.Columns.Add
            column12.Width = 39
            column12.StyleID = "s96"
            Dim column13 As WorksheetColumn = sheet.Table.Columns.Add
            column13.Width = 39
            column13.StyleID = "s96"
            Dim column14 As WorksheetColumn = sheet.Table.Columns.Add
            column14.Width = 35
            column14.StyleID = "s96"
            Dim column15 As WorksheetColumn = sheet.Table.Columns.Add
            column15.Width = 35
            column15.Span = 77
            '-----------------------------------------------
            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row0.Height = 11
            Row0.AutoFitHeight = false
            Row0.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Dim cell As WorksheetCell
            cell = Row0.Cells.Add
            cell.StyleID = "s97"
            cell.Index = 3
            cell = Row0.Cells.Add
            cell.StyleID = "s97"
            Row0.Cells.Add("????? ? ???????????????? ?? ???????? ???? ", DataType.[String], "s98")
            cell = Row0.Cells.Add
            cell.StyleID = "s289"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 13
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.Height = 11
            Row1.AutoFitHeight = false
            Row1.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row1.Cells.Add("???????: ??? ""???"" ????????????? ??????   ???????:", DataType.[String], "s99")
            cell = Row1.Cells.Add
            cell.StyleID = "s97"
            cell = Row1.Cells.Add
            cell.StyleID = "s97"
            cell = Row1.Cells.Add
            cell.StyleID = "s98"
            cell = Row1.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????]"
            cell.Index = 10
            cell.MergeAcross = 2
            cell = Row1.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???. 575-22-48"
            cell = Row1.Cells.Add
            cell.StyleID = "s101"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???? ?????:      "
            cell.Index = 16
            cell = Row1.Cells.Add
            cell.StyleID = "s102"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???? ?????]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            Row2.Height = 11
            Row2.AutoFitHeight = false
            Row2.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            Row2.Cells.Add("[??? ???? ?????]", DataType.[String], "s100")
            cell = Row2.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ???? ?????]"
            cell.Index = 5
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????? ?????:"
            cell.Index = 9
            cell = Row2.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???.?????]"
            cell.Index = 13
            cell = Row2.Cells.Add
            cell.StyleID = "s304"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??? ????:"
            cell.Index = 16
            cell.MergeAcross = 1
            cell = Row2.Cells.Add
            cell.StyleID = "s305"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??? ????]"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
            Row3.Height = 11
            Row3.AutoFitHeight = false
            Row3.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ???????????:     ??? ???  ???  ?????????????? ??????? ??? ""???"""
            cell = Row3.Cells.Add
            cell.StyleID = "s104"
            cell = Row3.Cells.Add
            cell.StyleID = "s104"
            cell = Row3.Cells.Add
            cell.StyleID = "s99"
            cell.Index = 8
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:  575-22-44"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row4 As WorksheetRow = sheet.Table.Rows.Add
            Row4.Height = 11
            Row4.AutoFitHeight = false
            Row4.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row4.Cells.Add("????????:", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[????????]", DataType.[String], "s106")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("????? ???????????:", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s107"
            Row4.Cells.Add("[????_????????????????]", DataType.[String], "s108")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("??????", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[?_??????]", DataType.[String], "s108")
            '-----------------------------------------------
            Dim Row5 As WorksheetRow = sheet.Table.Rows.Add
            Row5.Height = 11
            Row5.AutoFitHeight = false
            Row5.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ???????:"
            cell = Row5.Cells.Add
            cell.StyleID = "s104"
            cell = Row5.Cells.Add
            cell.StyleID = "s109"
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????? ?????? ? ?????:"
            cell.Index = 7
            cell = Row5.Cells.Add
            cell.StyleID = "s99"
            cell.Index = 9
            cell = Row5.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????_?????]"
            cell.Index = 11
            cell = Row5.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 15
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???,??.? ="
            cell.Index = 17
            cell = Row5.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
            Row6.Height = 11
            Row6.AutoFitHeight = false
            Row6.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????:"
            cell = Row6.Cells.Add
            cell.StyleID = "s104"
            Row6.Cells.Add("[????? ????????]", DataType.[String], "s110")
            cell = Row6.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[? ???????]"
            cell.Index = 6
            cell = Row6.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????(?????):"
            cell.Index = 8
            cell = Row6.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????]"
            cell.Index = 10
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????  ???? ????????  ?? :"
            cell.Index = 12
            cell = Row6.Cells.Add
            cell.StyleID = "s109"
            cell = Row6.Cells.Add
            cell.StyleID = "s109"
            cell = Row6.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???? ???????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row7 As WorksheetRow = sheet.Table.Rows.Add
            Row7.Height = 11
            Row7.AutoFitHeight = false
            Row7.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????.??.(?1):   "
            cell.MergeAcross = 2
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row7.Cells.Add("[??????????]", DataType.[String], "s103")
            cell = Row7.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row7.Cells.Add("[G??_min??]", DataType.[String], "s100")
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row7.Cells.Add("[G????]", DataType.[String], "s100")
            cell = Row7.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row7.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row8 As WorksheetRow = sheet.Table.Rows.Add
            Row8.Height = 11
            Row8.AutoFitHeight = false
            Row8.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????.??.(?2):   "
            cell.MergeAcross = 2
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row8.Cells.Add("[??????????]", DataType.[String], "s103")
            cell = Row8.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row8.Cells.Add("[G??_min??]", DataType.[String], "s100")
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row8.Cells.Add("[G????]", DataType.[String], "s100")
            cell = Row8.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row8.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row9 As WorksheetRow = sheet.Table.Rows.Add
            Row9.Height = 11
            Row9.AutoFitHeight = false
            Row9.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??-? ???(?3):   "
            cell.MergeAcross = 2
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row9.Cells.Add("[?????????? ???]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row9.Cells.Add("[G??(??? min)]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row9.Cells.Add("[G(???)??]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row9.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????? ???]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row10 As WorksheetRow = sheet.Table.Rows.Add
            Row10.Height = 11
            Row10.AutoFitHeight = false
            Row10.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??.????.???(?4):  "
            cell.MergeAcross = 2
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s112"
            Row10.Cells.Add("???????????.:", DataType.[String], "s109")
            cell = Row10.Cells.Add
            cell.StyleID = "s100"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row11 As WorksheetRow = sheet.Table.Rows.Add
            Row11.Height = 11
            Row11.AutoFitHeight = false
            Row11.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??.????????(V5):   "
            cell.MergeAcross = 2
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            cell = Row11.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.Index = 11
            cell.MergeAcross = 1
            cell = Row11.Cells.Add
            cell.StyleID = "s109"
            cell = Row11.Cells.Add
            cell.StyleID = "s112"
            Row11.Cells.Add("???????????.:", DataType.[String], "s109")
            cell = Row11.Cells.Add
            cell.StyleID = "s110"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row12 As WorksheetRow = sheet.Table.Rows.Add
            Row12.Height = 11
            Row12.AutoFitHeight = false
            Row12.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row12.Cells.Add("????????? ????????:", DataType.[String], "s105")
            cell = Row12.Cells.Add
            cell.StyleID = "s113"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            Row12.Cells.Add("????:", DataType.[String], "s105")
            Row12.Cells.Add("[???????]", DataType.[String], "s108")
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s107"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s114"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????:"
            cell.MergeAcross = 1
            Row12.Cells.Add("[???????2]", DataType.[String], "s108")
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            '-----------------------------------------------
            Dim Row13 As WorksheetRow = sheet.Table.Rows.Add
            Row13.Height = 11
            Row13.AutoFitHeight = false
            Row13.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ????????,????/???:"
            cell = Row13.Cells.Add
            cell.StyleID = "s104"
            cell = Row13.Cells.Add
            cell.StyleID = "s104"
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q??.="
            cell.Index = 7
            Row13.Cells.Add("[Q??]", DataType.[String], "s115")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q????.="
            Row13.Cells.Add("[Q????]", DataType.[String], "s115")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.="
            cell.Index = 12
            Row13.Cells.Add("[Q???]", DataType.[String], "s116")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.???="
            cell = Row13.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q???_???]"
            cell.Index = 16
            cell = Row13.Cells.Add
            cell.StyleID = "s117"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.="
            cell.Index = 18
            Row13.Cells.Add("[Q???]", DataType.[String], "s115")
            '-----------------------------------------------
            Dim Row14 As WorksheetRow = sheet.Table.Rows.Add
            Row14.Height = 11
            Row14.AutoFitHeight = false
            Row14.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ???????? (??.???),????/???:"
            cell = Row14.Cells.Add
            cell.StyleID = "s104"
            cell = Row14.Cells.Add
            cell.StyleID = "s104"
            cell = Row14.Cells.Add
            cell.StyleID = "s110"
            cell.Index = 7
            cell = Row14.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.??? ??="
            cell.Index = 12
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q???_??? ??]"
            cell.Index = 15
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.??="
            cell.Index = 17
            cell = Row14.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??? ??]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row15 As WorksheetRow = sheet.Table.Rows.Add
            Row15.Height = 11
            Row15.AutoFitHeight = false
            Row15.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????. ???????? (??.???),?/???:"
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G??.="
            cell.Index = 7
            Row15.Cells.Add("[G??]", DataType.[String], "s115")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G????.="
            Row15.Cells.Add("[G????]", DataType.[String], "s115")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G???.="
            cell.Index = 13
            Row15.Cells.Add("[G???]", DataType.[String], "s116")
            Row15.Cells.Add("G???.=", DataType.[String], "s111")
            Row15.Cells.Add("[G???]", DataType.[String], "s100")
            cell = Row15.Cells.Add
            cell.StyleID = "s116"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "  G???.? ="
            cell.Index = 18
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G???_?]"
            '-----------------------------------------------
            Dim Row16 As WorksheetRow = sheet.Table.Rows.Add
            Row16.Height = 11
            Row16.AutoFitHeight = false
            cell = Row16.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row16.Cells.Add("?????.????????:", DataType.[String], "s105")
            cell = Row16.Cells.Add
            cell.StyleID = "s118"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            Row16.Cells.Add("?1=", DataType.[String], "s114")
            Row16.Cells.Add("[???]", DataType.[String], "s118")
            Row16.Cells.Add("?2=", DataType.[String], "s114")
            Row16.Cells.Add("[????]", DataType.[String], "s107")
            Row16.Cells.Add("    t1=", DataType.[String], "s114")
            Row16.Cells.Add("[?1]", DataType.[String], "s119")
            Row16.Cells.Add("    t2=", DataType.[String], "s114")
            Row16.Cells.Add("[?2]", DataType.[String], "s119")
            Row16.Cells.Add("t3=", DataType.[String], "s114")
            cell = Row16.Cells.Add
            cell.StyleID = "s119"
            Row16.Cells.Add("t4=", DataType.[String], "s105")
            cell = Row16.Cells.Add
            cell.StyleID = "s119"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            '-----------------------------------------------
            Dim Row17 As WorksheetRow = sheet.Table.Rows.Add
            Row17.Height = 11
            Row17.AutoFitHeight = false
            cell = Row17.Cells.Add
            cell.StyleID = "s112"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ?????????? ???????? ??????"
            cell.Index = 2
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell = Row17.Cells.Add
            cell.StyleID = "s120"
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 7
            cell = Row17.Cells.Add
            cell.StyleID = "s121"
            cell.Index = 10
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row18 As WorksheetRow = sheet.Table.Rows.Add
            Row18.Height = 11
            Row18.AutoFitHeight = false
            cell = Row18.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 2
            cell = Row18.Cells.Add
            cell.StyleID = "s116"
            cell = Row18.Cells.Add
            cell.StyleID = "m85476180"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???? (??-1)"
            cell.Index = 5
            cell.MergeAcross = 7
            cell = Row18.Cells.Add
            cell.StyleID = "m85476244"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? (??-2)"
            cell.MergeAcross = 5
            cell = Row18.Cells.Add
            cell.StyleID = "s112"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row19 As WorksheetRow = sheet.Table.Rows.Add
            Row19.Height = 11
            Row19.AutoFitHeight = false
            cell = Row19.Cells.Add
            cell.StyleID = "m85476224"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeDown = 1
            Row19.Cells.Add("??", DataType.[String], "s122")
            Row19.Cells.Add("??", DataType.[String], "s123")
            Row19.Cells.Add("    ?1,", DataType.[String], "s124")
            Row19.Cells.Add("    ?2,", DataType.[String], "s125")
            Row19.Cells.Add("d?,", DataType.[String], "s126")
            Row19.Cells.Add("?1,", DataType.[String], "s127")
            Row19.Cells.Add("?2,", DataType.[String], "s122")
            Row19.Cells.Add("d?,", DataType.[String], "s126")
            Row19.Cells.Add(" P1,", DataType.[String], "s127")
            Row19.Cells.Add(" P2,", DataType.[String], "s126")
            Row19.Cells.Add("?3", DataType.[String], "s127")
            Row19.Cells.Add("M4,", DataType.[String], "s128")
            Row19.Cells.Add("dM,", DataType.[String], "s122")
            Row19.Cells.Add("V5,", DataType.[String], "s129")
            Row19.Cells.Add("?3,", DataType.[String], "s127")
            Row19.Cells.Add("?4,", DataType.[String], "s129")
            Row19.Cells.Add(" Q", DataType.[String], "s130")
            cell = Row19.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row20 As WorksheetRow = sheet.Table.Rows.Add
            Row20.Height = 11
            Row20.AutoFitHeight = false
            cell = Row20.Cells.Add
            cell.StyleID = "s131"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???"
            cell.Index = 3
            cell = Row20.Cells.Add
            cell.StyleID = "s132"
            Row20.Cells.Add("  ?", DataType.[String], "s133")
            Row20.Cells.Add("  ?", DataType.[String], "s131")
            Row20.Cells.Add("  ?", DataType.[String], "s134")
            Row20.Cells.Add("?", DataType.[String], "s135")
            Row20.Cells.Add("?", DataType.[String], "s131")
            Row20.Cells.Add("?", DataType.[String], "s134")
            Row20.Cells.Add("??/??2", DataType.[String], "s205")
            Row20.Cells.Add("??/??2", DataType.[String], "s205")
            Row20.Cells.Add("  ?", DataType.[String], "s136")
            Row20.Cells.Add("?", DataType.[String], "s137")
            Row20.Cells.Add("?", DataType.[String], "s131")
            Row20.Cells.Add("?.???", DataType.[String], "s138")
            Row20.Cells.Add("?", DataType.[String], "s135")
            Row20.Cells.Add("?", DataType.[String], "s132")
            Row20.Cells.Add("  ????", DataType.[String], "s139")
            cell = Row20.Cells.Add
            cell.StyleID = "s140"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row21 As WorksheetRow = sheet.Table.Rows.Add
            Row21.Height = 11
            Row21.AutoFitHeight = false
            Row21.Cells.Add("[Report:?????]", DataType.[String], "s141")
            Row21.Cells.Add("[????? ??????]", DataType.[String], "s142")
            Row21.Cells.Add("24", DataType.Number, "s143")
            cell = Row21.Cells.Add
            cell.StyleID = "s144"
            Row21.Cells.Add("[M1 ???]", DataType.[String], "s145")
            Row21.Cells.Add("[?2 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s147"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-2]-RC[-1]"
            Row21.Cells.Add("[T1 ???]", DataType.[String], "s148")
            Row21.Cells.Add("[T2 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s147"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-2]-RC[-1]"
            cell = Row21.Cells.Add
            cell.StyleID = "s149"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R17C6*1"
            cell = Row21.Cells.Add
            cell.StyleID = "s150"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R17C8*1"
            cell = Row21.Cells.Add
            cell.StyleID = "s151"
            Row21.Cells.Add("[?4 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s146"
            cell = Row21.Cells.Add
            cell.StyleID = "s152"
            cell = Row21.Cells.Add
            cell.StyleID = "s148"
            Row21.Cells.Add("[T4 ???]", DataType.[String], "s152")
            Row21.Cells.Add("[Q1 ???]", DataType.[String], "s153")
            cell = Row21.Cells.Add
            cell.StyleID = "s154"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q4 ???]"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row22 As WorksheetRow = sheet.Table.Rows.Add
            Row22.Height = 11
            Row22.AutoFitHeight = false
            cell = Row22.Cells.Add
            cell.StyleID = "s155"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row22.Cells.Add
            cell.StyleID = "s156"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s157"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s162"
            cell = Row22.Cells.Add
            cell.StyleID = "s162"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s160"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s163"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Index = 21
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            '-----------------------------------------------
            Dim Row23 As WorksheetRow = sheet.Table.Rows.Add
            Row23.Height = 11
            Row23.AutoFitHeight = false
            cell = Row23.Cells.Add
            cell.StyleID = "s164"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????"
            cell.Index = 2
            cell = Row23.Cells.Add
            cell.StyleID = "s165"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s166"
            cell = Row23.Cells.Add
            cell.StyleID = "s167"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s169"
            cell = Row23.Cells.Add
            cell.StyleID = "s170"
            cell = Row23.Cells.Add
            cell.StyleID = "s166"
            cell = Row23.Cells.Add
            cell.StyleID = "s169"
            cell = Row23.Cells.Add
            cell.StyleID = "s171"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s172"
            cell = Row23.Cells.Add
            cell.StyleID = "s173"
            cell = Row23.Cells.Add
            cell.StyleID = "s167"
            cell = Row23.Cells.Add
            cell.StyleID = "s174"
            cell = Row23.Cells.Add
            cell.StyleID = "s175"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s176"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Index = 21
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            '-----------------------------------------------
            Dim Row24 As WorksheetRow = sheet.Table.Rows.Add
            Row24.Height = 11
            Row24.AutoFitHeight = false
            cell = Row24.Cells.Add
            cell.StyleID = "s112"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ????????? ?? ?????? ?????? ??????:"
            cell.Index = 2
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 5
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            '-----------------------------------------------
            Dim Row25 As WorksheetRow = sheet.Table.Rows.Add
            Row25.Height = 11
            Row25.AutoFitHeight = false
            cell = Row25.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?1, ?"
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?2, ?"
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?3,?"
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?4,?"
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "V5,?.???"
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "m85476792"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q, ????"
            cell.MergeAcross = 2
            cell = Row25.Cells.Add
            cell.StyleID = "m85476772"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??,?"
            cell.MergeAcross = 1
            cell = Row25.Cells.Add
            cell.StyleID = "s193"
            cell = Row25.Cells.Add
            cell.StyleID = "m85476692"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???, ????"
            cell.Index = 21
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row26 As WorksheetRow = sheet.Table.Rows.Add
            Row26.Height = 11
            Row26.AutoFitHeight = false
            cell = Row26.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:????????]"
            cell = Row26.Cells.Add
            cell.StyleID = "s299"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ??????]"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M1 ???]"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M2 ???]"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "s160"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M4 ???]"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "s160"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "m85476608"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??1 ???]"
            cell.MergeAcross = 2
            cell = Row26.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[TSUM1]"
            cell.MergeAcross = 1
            cell = Row26.Cells.Add
            cell.StyleID = "s194"
            cell = Row26.Cells.Add
            cell.StyleID = "m85476672"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??4 ???]"
            cell.Index = 21
            cell.MergeAcross = 2
            cell = Row26.Cells.Add
            cell.StyleID = "s195"
            '-----------------------------------------------
            Dim Row27 As WorksheetRow = sheet.Table.Rows.Add
            Row27.Height = 11
            Row27.AutoFitHeight = false
            cell = Row27.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ??????? ?? ????????:"
            cell.Index = 2
            '-----------------------------------------------
            Dim Row28 As WorksheetRow = sheet.Table.Rows.Add
            Row28.Height = 11
            Row28.AutoFitHeight = false
            cell = Row28.Cells.Add
            cell.StyleID = "s96"
            cell = Row28.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ??????? ?? ????????:"
            cell = Row28.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 15
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row29 As WorksheetRow = sheet.Table.Rows.Add
            Row29.Height = 11
            Row29.AutoFitHeight = false
            cell = Row29.Cells.Add
            cell.StyleID = "s96"
            cell = Row29.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?????????? t2:"
            '-----------------------------------------------
            Dim Row30 As WorksheetRow = sheet.Table.Rows.Add
            Row30.Height = 11
            Row30.AutoFitHeight = false
            cell = Row30.Cells.Add
            cell.StyleID = "s96"
            cell = Row30.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????? ??? ?????? ???????? ""??????????""??? ""??? ???"""
            cell = Row30.Cells.Add
            cell.StyleID = "s105"
            cell.Index = 11
            cell = Row30.Cells.Add
            cell.StyleID = "s105"
            cell = Row30.Cells.Add
            cell.StyleID = "s105"
            cell = Row30.Cells.Add
            cell.StyleID = "s105"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "/"
            cell.Index = 15
            cell = Row30.Cells.Add
            cell.StyleID = "s105"
            cell = Row30.Cells.Add
            cell.StyleID = "s105"
            Row30.Cells.Add("/", DataType.[String], "s114")
            '-----------------------------------------------
            Dim Row31 As WorksheetRow = sheet.Table.Rows.Add
            Row31.Height = 11
            Row31.AutoFitHeight = false
            cell = Row31.Cells.Add
            cell.StyleID = "s96"
            cell = Row31.Cells.Add
            cell.StyleID = "s196"
            cell.Index = 11
            cell = Row31.Cells.Add
            cell.StyleID = "s196"
            '-----------------------------------------------
            Dim Row32 As WorksheetRow = sheet.Table.Rows.Add
            Row32.Height = 11
            Row32.AutoFitHeight = false
            cell = Row32.Cells.Add
            cell.StyleID = "s96"
            cell = Row32.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ?? ???? ???????? ??????? (?? ????????)"
            cell = Row32.Cells.Add
            cell.StyleID = "s105"
            cell.Index = 11
            cell = Row32.Cells.Add
            cell.StyleID = "s105"
            cell = Row32.Cells.Add
            cell.StyleID = "s105"
            cell = Row32.Cells.Add
            cell.StyleID = "s105"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "/"
            cell.Index = 15
            Row32.Cells.Add("????? ?.?.   /", DataType.[String], "s105")
            cell = Row32.Cells.Add
            cell.StyleID = "s105"
            cell = Row32.Cells.Add
            cell.StyleID = "s114"
            cell = Row32.Cells.Add
            cell.StyleID = "s197"
            '-----------------------------------------------
            Dim Row33 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row33.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row34 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row34.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row35 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row35.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row36 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row36.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row37 As WorksheetRow = sheet.Table.Rows.Add
            Row37.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row37.Cells.Add
            cell.StyleID = "s198"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??? ? ????????????????  ??  "
            cell.Index = 6
            cell = Row37.Cells.Add
            cell.StyleID = "s289"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 11
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row38 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row38.Cells.Add
            cell.StyleID = "s96"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            cell = Row38.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row39 As WorksheetRow = sheet.Table.Rows.Add
            Row39.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row39.Cells.Add("??, ?????????????????: ????????? ?????????? ?????????? ???????? ?????????? ??? ""?"& _ 
                    "?? ???""", DataType.[String], "s199")
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            cell = Row39.Cells.Add
            cell.StyleID = "s199"
            Row39.Cells.Add("[???????]", DataType.[String], "s200")
            '-----------------------------------------------
            Dim Row40 As WorksheetRow = sheet.Table.Rows.Add
            Row40.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row40.Cells.Add("? ????????????? ????????", DataType.[String], "s199")
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            Row40.Cells.Add("[??? ???? ?????]", DataType.[String], "s200")
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            Row40.Cells.Add("?????????????? ?? ??????:", DataType.[String], "s199")
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 10
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            Row40.Cells.Add("[????? ???? ?????]", DataType.[String], "s200")
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 15
            Row40.Cells.Add(" ????????? ", DataType.[String], "s199")
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            cell = Row40.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row41 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??? ? ???, ??? ?? ??????     ???????????? ???????????????? ??????????? "& _ 
                "?????????,"
            cell.Index = 2
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row42 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "? ??????? ?????? ???????? ??????? ? ??????????:"
            cell.Index = 2
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row43 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ?????????? ???????? ??????:"
            cell.Index = 2
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("=", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row43.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row44 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??????????? ?? ?????????? ??????:"
            cell.Index = 2
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            Row44.Cells.Add("=", DataType.[String], "s199")
            cell = Row44.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row44.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row45 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ??????? ???????? ??????:"
            cell.Index = 2
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            Row45.Cells.Add("=", DataType.[String], "s199")
            cell = Row45.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row45.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row46 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??????????? ?? ????? ??????:"
            cell.Index = 2
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            Row46.Cells.Add("=", DataType.[String], "s199")
            cell = Row46.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row46.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row47 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ?? ??????????? ???????? ???? ?? ????????? ??????????????"
            cell.Index = 2
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            Row47.Cells.Add("??.?", DataType.[String], "s199")
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            Row47.Cells.Add("=", DataType.[String], "s199")
            cell = Row47.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row47.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row48 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ???????? ?? ??????:"
            cell.Index = 2
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            Row48.Cells.Add("=", DataType.[String], "s199")
            cell = Row48.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row48.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row49 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ?????????? ????????? ?? ??????:"
            cell.Index = 2
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            Row49.Cells.Add("=", DataType.[String], "s199")
            cell = Row49.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row49.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row50 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ????????? ?????????? ????????????:"
            cell.Index = 2
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            cell = Row50.Cells.Add
            cell.StyleID = "s201"
            Row50.Cells.Add("=", DataType.[String], "s201")
            cell = Row50.Cells.Add
            cell.StyleID = "s306"
            cell.MergeAcross = 1
            Row50.Cells.Add("????", DataType.[String], "s201")
            '-----------------------------------------------
            Dim Row51 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell.Index = 2
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell = Row51.Cells.Add
            cell.StyleID = "s202"
            cell.Index = 18
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row52 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row52.Cells.Add
            cell.StyleID = "s203"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            Row52.Cells.Add("=", DataType.[String], "s203")
            cell = Row52.Cells.Add
            cell.StyleID = "s287"
            cell.MergeAcross = 1
            Row52.Cells.Add("????", DataType.[String], "s203")
            '-----------------------------------------------
            Dim Row53 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 18
            cell = Row53.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row54 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row55 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 13
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row56 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ????????"
            cell.Index = 2
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s201"
            cell.Index = 13
            cell = Row56.Cells.Add
            cell.StyleID = "s201"
            cell = Row56.Cells.Add
            cell.StyleID = "s201"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            Row56.Cells.Add("????? ?.?.", DataType.[String], "s199")
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row57 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 13
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row58 As WorksheetRow = sheet.Table.Rows.Add
            Row58.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row58.Cells.Add("????????? ?????????? ?????????? ???????? ""??????????"" ???""??? ???""", DataType.[String], "s199")
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s201"
            cell.Index = 13
            cell = Row58.Cells.Add
            cell.StyleID = "s201"
            cell = Row58.Cells.Add
            cell.StyleID = "s201"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            Row58.Cells.Add("[???????]", DataType.[String], "s204")
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            ' Options
            '-----------------------------------------------
            sheet.Options.ProtectObjects = false
            sheet.Options.ProtectScenarios = false
            sheet.Options.PageSetup.Header.Margin = 0!
            sheet.Options.PageSetup.Footer.Margin = 0!
            sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008!
            sheet.Options.PageSetup.PageMargins.Left = 0.511811!
            sheet.Options.PageSetup.PageMargins.Right = 0.3149606!
            sheet.Options.PageSetup.PageMargins.Top = 0.3149606!
            sheet.Options.Print.PaperSizeIndex = 9
            sheet.Options.Print.HorizontalResolution = -4
            sheet.Options.Print.VerticalResolution = 180
            sheet.Options.Print.ValidPrinterInfo = true
        End Sub
        
        Private Overloads Sub GenerateWorksheetSheet(ByVal sheets As WorksheetCollection)
            Dim sheet As Worksheet = sheets.Add("Sheet")
            sheet.Table.ExpandedColumnCount = 94
            sheet.Table.ExpandedRowCount = 21
            sheet.Table.FullColumns = 1
            sheet.Table.FullRows = 1
            sheet.Table.Columns.Add(136)
            sheet.Table.Columns.Add(39)
            sheet.Table.Columns.Add(30)
            Dim column3 As WorksheetColumn = sheet.Table.Columns.Add
            column3.Width = 35
            column3.Span = 9
            Dim column4 As WorksheetColumn = sheet.Table.Columns.Add
            column4.Index = 14
            column4.Width = 39
            sheet.Table.Columns.Add(38)
            Dim column6 As WorksheetColumn = sheet.Table.Columns.Add
            column6.Width = 35
            column6.Span = 78
            '-----------------------------------------------
            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row0.AutoFitHeight = false
            Dim cell As WorksheetCell
            cell = Row0.Cells.Add
            cell.StyleID = "s310"
            cell.Index = 3
            cell.MergeAcross = 2
            cell = Row0.Cells.Add
            cell.StyleID = "s206"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????"
            cell.Index = 8
            '-----------------------------------------------
            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.AutoFitHeight = false
            cell = Row1.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row1.Cells.Add
            cell.StyleID = "s95"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "? ??????????? ???????? ??????? ? ?????????????   ??"
            cell.Index = 4
            cell = Row1.Cells.Add
            cell.StyleID = "s95"
            cell.Index = 6
            cell = Row1.Cells.Add
            cell.StyleID = "s95"
            cell = Row1.Cells.Add
            cell.StyleID = "s207"
            cell = Row1.Cells.Add
            cell.StyleID = "s207"
            cell = Row1.Cells.Add
            cell.StyleID = "s237"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 12
            '-----------------------------------------------
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            Row2.AutoFitHeight = false
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell = Row2.Cells.Add
            cell.StyleID = "s208"
            Row2.Cells.Add("[??? ???? ?????]", DataType.[String], "s83")
            cell = Row2.Cells.Add
            cell.StyleID = "s209"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????:"
            cell.Index = 8
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            cell.Index = 11
            Row2.Cells.Add("[????????]", DataType.[String], "s71")
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
            Row3.AutoFitHeight = false
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell = Row3.Cells.Add
            cell.StyleID = "s209"
            Row3.Cells.Add("[????? ???? ?????]", DataType.[String], "s209")
            cell = Row3.Cells.Add
            cell.StyleID = "s209"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ?????????:"
            cell.Index = 8
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????]"
            cell.Index = 12
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row4 As WorksheetRow = sheet.Table.Rows.Add
            Row4.AutoFitHeight = false
            cell = Row4.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row4.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:   575-22-48"
            cell = Row4.Cells.Add
            cell.StyleID = "s210"
            cell = Row4.Cells.Add
            cell.StyleID = "s210"
            cell = Row4.Cells.Add
            cell.StyleID = "s209"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ??????:"
            cell.Index = 8
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???_??????]"
            cell.Index = 12
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row5 As WorksheetRow = sheet.Table.Rows.Add
            Row5.AutoFitHeight = false
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ???????:     Fg=[G/(n*G??)]*100%         dt=t1-t2          "
            cell.Index = 2
            cell = Row5.Cells.Add
            cell.StyleID = "s210"
            cell = Row5.Cells.Add
            cell.StyleID = "s210"
            cell = Row5.Cells.Add
            cell.StyleID = "s209"
            cell.Index = 8
            cell = Row5.Cells.Add
            cell.StyleID = "s71"
            cell.Index = 13
            cell = Row5.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
            Row6.AutoFitHeight = false
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????, ????????  ??-?: ??????????"
            cell = Row6.Cells.Add
            cell.StyleID = "s210"
            cell = Row6.Cells.Add
            cell.StyleID = "s210"
            cell = Row6.Cells.Add
            cell.StyleID = "s82"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????]"
            cell.Index = 8
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ???.:"
            cell.Index = 10
            cell = Row6.Cells.Add
            cell.StyleID = "s78"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G??_min??]"
            cell.Index = 12
            Row6.Cells.Add("[G????]", DataType.[String], "s78")
            Row6.Cells.Add("? ???./???", DataType.[String], "s71")
            '-----------------------------------------------
            Dim Row7 As WorksheetRow = sheet.Table.Rows.Add
            Row7.AutoFitHeight = false
            cell = Row7.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row7.Cells.Add("?????????? ???:", DataType.[String], "s72")
            cell = Row7.Cells.Add
            cell.StyleID = "s212"
            cell = Row7.Cells.Add
            cell.StyleID = "s95"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????????? ???]"
            cell.Index = 5
            cell = Row7.Cells.Add
            cell.StyleID = "s71"
            cell.Index = 7
            cell = Row7.Cells.Add
            cell.StyleID = "s71"
            cell = Row7.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ???.:"
            cell.Index = 10
            cell = Row7.Cells.Add
            cell.StyleID = "s78"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G??(??? min)]"
            cell.Index = 12
            Row7.Cells.Add("[G(???)??]", DataType.[String], "s78")
            Row7.Cells.Add("? ???./???", DataType.[String], "s71")
            '-----------------------------------------------
            Dim Row8 As WorksheetRow = sheet.Table.Rows.Add
            Row8.AutoFitHeight = false
            cell = Row8.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row8.Cells.Add("???????????? :", DataType.[String], "s72")
            cell = Row8.Cells.Add
            cell.StyleID = "s212"
            Row8.Cells.Add("[????? ????????]", DataType.[String], "s95")
            cell = Row8.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ???????  G?? ="
            cell.Index = 7
            cell = Row8.Cells.Add
            cell.StyleID = "s213"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????? G2]"
            cell.Index = 11
            cell = Row8.Cells.Add
            cell.StyleID = "s211"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G???="
            cell.Index = 13
            Row8.Cells.Add("[G???]", DataType.[String], "s213")
            '-----------------------------------------------
            Dim Row9 As WorksheetRow = sheet.Table.Rows.Add
            Row9.Height = 14
            Row9.AutoFitHeight = false
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row9.Cells.Add("??????? ?", DataType.[String], "s82")
            cell = Row9.Cells.Add
            cell.StyleID = "s212"
            Row9.Cells.Add("[???????]", DataType.[String], "s83")
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ????????  ??1="
            cell.Index = 7
            cell = Row9.Cells.Add
            cell.StyleID = "s78"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???]"
            cell.Index = 11
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??/??2"
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??2="
            Row9.Cells.Add("[????]", DataType.[String], "s78")
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??/??2"
            '-----------------------------------------------
            Dim Row10 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row10.Cells.Add
            cell.StyleID = "s214"
            Row10.Cells.Add(",", DataType.[String], "s90")
            cell = Row10.Cells.Add
            cell.StyleID = "s91"
            Row10.Cells.Add("  ???????? ???????????", DataType.[String], "s215")
            cell = Row10.Cells.Add
            cell.StyleID = "s215"
            cell = Row10.Cells.Add
            cell.StyleID = "s215"
            cell = Row10.Cells.Add
            cell.StyleID = "s215"
            cell = Row10.Cells.Add
            cell.StyleID = "s216"
            cell = Row10.Cells.Add
            cell.StyleID = "m85477200"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ???????????"
            cell.MergeAcross = 3
            cell = Row10.Cells.Add
            cell.StyleID = "s91"
            cell = Row10.Cells.Add
            cell.StyleID = "m85477220"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ???  [?] (??? ????????)"
            cell.MergeDown = 2
            cell = Row10.Cells.Add
            cell.StyleID = "s217"
            '-----------------------------------------------
            Dim Row11 As WorksheetRow = sheet.Table.Rows.Add
            Row11.Height = 14
            Row11.AutoFitHeight = false
            cell = Row11.Cells.Add
            cell.StyleID = "s218"
            Row11.Cells.Add("????", DataType.[String], "s92")
            Row11.Cells.Add("????", DataType.[String], "s219")
            Row11.Cells.Add("Fg1", DataType.[String], "s75")
            Row11.Cells.Add("  t1", DataType.[String], "s75")
            Row11.Cells.Add(" P1", DataType.[String], "s75")
            Row11.Cells.Add("   M1", DataType.[String], "s220")
            Row11.Cells.Add("    dt", DataType.[String], "s84")
            Row11.Cells.Add("Fg2", DataType.[String], "s75")
            Row11.Cells.Add("  t2", DataType.[String], "s75")
            Row11.Cells.Add(" P2", DataType.[String], "s75")
            Row11.Cells.Add("  M2", DataType.[String], "s75")
            Row11.Cells.Add(" M1-M2", DataType.[String], "s75")
            cell = Row11.Cells.Add
            cell.StyleID = "s93"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = " Q???"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row12 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row12.Cells.Add
            cell.StyleID = "s218"
            cell = Row12.Cells.Add
            cell.StyleID = "s221"
            cell = Row12.Cells.Add
            cell.StyleID = "s222"
            Row12.Cells.Add(" %", DataType.[String], "s76")
            Row12.Cells.Add("??.C", DataType.[String], "s76")
            Row12.Cells.Add("??/??", DataType.[String], "s76")
            Row12.Cells.Add("  ????", DataType.[String], "s223")
            Row12.Cells.Add("  ??.?", DataType.[String], "s76")
            Row12.Cells.Add(" %", DataType.[String], "s76")
            Row12.Cells.Add(" ??.C", DataType.[String], "s76")
            Row12.Cells.Add("??/??2", DataType.[String], "s76")
            Row12.Cells.Add(" ????", DataType.[String], "s76")
            Row12.Cells.Add(" ????", DataType.[String], "s76")
            cell = Row12.Cells.Add
            cell.StyleID = "s224"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "  ????"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row13 As WorksheetRow = sheet.Table.Rows.Add
            Row13.Height = 14
            Row13.AutoFitHeight = false
            Row13.Cells.Add("[Report:?????]", DataType.[String], "s225")
            Row13.Cells.Add("[????? ??????]", DataType.[String], "s226")
            Row13.Cells.Add("24", DataType.Number, "s227")
            cell = Row13.Cells.Add
            cell.StyleID = "s228"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[3]/(RC[-1]*R7C13)*100%"
            Row13.Cells.Add("[T1 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R10C11*1"
            Row13.Cells.Add("[?1 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-3]-RC[2]"
            cell = Row13.Cells.Add
            cell.StyleID = "s228"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[3]/(RC[-6]*R7C13)*100%"
            Row13.Cells.Add("[T2 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R10C14*1"
            Row13.Cells.Add("[?2 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-6]-RC[-1]"
            Row13.Cells.Add("[?3 ???]", DataType.[String], "s229")
            Row13.Cells.Add("[Q??1 ???]", DataType.[String], "s230")
            cell = Row13.Cells.Add
            cell.StyleID = "s152"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[T3 ???]"
            cell.Index = 17
            Row13.Cells.Add("[Q2 ]", DataType.[String], "s154")
            Row13.Cells.Add("[?3 ???]", DataType.[String], "s229")
            '-----------------------------------------------
            Dim Row14 As WorksheetRow = sheet.Table.Rows.Add
            Row14.Height = 14
            Row14.AutoFitHeight = false
            cell = Row14.Cells.Add
            cell.StyleID = "s231"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row14.Cells.Add
            cell.StyleID = "s80"
            cell = Row14.Cells.Add
            cell.StyleID = "s232"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s232"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s233"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            '-----------------------------------------------
            Dim Row15 As WorksheetRow = sheet.Table.Rows.Add
            Row15.Height = 14
            Row15.AutoFitHeight = false
            cell = Row15.Cells.Add
            cell.StyleID = "s234"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row15.Cells.Add
            cell.StyleID = "s235"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s74"
            cell = Row15.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s233"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            '-----------------------------------------------
            Dim Row16 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row16.Cells.Add
            cell.StyleID = "s81"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ?????????? ???????????????? ?? ????????????? ??????????? ??????:"
            cell.Index = 2
            cell = Row16.Cells.Add
            cell.StyleID = "s236"
            cell.Index = 5
            cell = Row16.Cells.Add
            cell.StyleID = "s77"
            '-----------------------------------------------
            Dim Row17 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row17.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeAcross = 2
            cell = Row17.Cells.Add
            cell.StyleID = "s321"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "M1, ?"
            cell.MergeAcross = 1
            cell = Row17.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "M2, ?"
            cell.MergeAcross = 1
            cell = Row17.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???, ????"
            cell.MergeAcross = 1
            cell = Row17.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "M???,?"
            cell.MergeAcross = 1
            cell = Row17.Cells.Add
            cell.StyleID = "m85477364"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "T???., ???"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row18 As WorksheetRow = sheet.Table.Rows.Add
            Row18.Height = 14
            Row18.AutoFitHeight = false
            cell = Row18.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:????????]"
            cell = Row18.Cells.Add
            cell.StyleID = "s320"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ??????]"
            cell.MergeAcross = 2
            cell = Row18.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M1 ???]"
            cell.MergeAcross = 1
            cell = Row18.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M2 ???]"
            cell.MergeAcross = 1
            cell = Row18.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??1 ???]"
            cell.MergeAcross = 1
            cell = Row18.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M3 ???]"
            cell.MergeAcross = 1
            cell = Row18.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[TSUM1 ???]"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row19 As WorksheetRow = sheet.Table.Rows.Add
            Row19.Index = 21
            cell = Row19.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ???? "
            cell.Index = 4
            cell = Row19.Cells.Add
            cell.StyleID = "s77"
            cell.Index = 6
            cell = Row19.Cells.Add
            cell.StyleID = "s77"
            cell = Row19.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ?.?."
            '-----------------------------------------------
            ' Options
            '-----------------------------------------------
            sheet.Options.Selected = true
            sheet.Options.ProtectObjects = false
            sheet.Options.ProtectScenarios = false
            sheet.Options.Print.PaperSizeIndex = 9
            sheet.Options.Print.HorizontalResolution = -4
            sheet.Options.Print.VerticalResolution = 180
            sheet.Options.Print.ValidPrinterInfo = true
        End Sub
        
        Private Overloads Sub GenerateWorksheetSheet2(ByVal sheets As WorksheetCollection)
            Dim sheet As Worksheet = sheets.Add("Sheet2")
            sheet.Table.ExpandedColumnCount = 94
            sheet.Table.ExpandedRowCount = 23
            sheet.Table.FullColumns = 1
            sheet.Table.FullRows = 1
            sheet.Table.Columns.Add(136)
            sheet.Table.Columns.Add(39)
            sheet.Table.Columns.Add(30)
            Dim column3 As WorksheetColumn = sheet.Table.Columns.Add
            column3.Width = 35
            column3.Span = 9
            Dim column4 As WorksheetColumn = sheet.Table.Columns.Add
            column4.Index = 14
            column4.Width = 39
            sheet.Table.Columns.Add(38)
            Dim column6 As WorksheetColumn = sheet.Table.Columns.Add
            column6.Width = 35
            column6.Span = 78
            '-----------------------------------------------
            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row0.AutoFitHeight = false
            Dim cell As WorksheetCell
            cell = Row0.Cells.Add
            cell.StyleID = "s310"
            cell.Index = 3
            cell.MergeAcross = 2
            cell = Row0.Cells.Add
            cell.StyleID = "s206"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????"
            cell.Index = 8
            '-----------------------------------------------
            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.AutoFitHeight = false
            cell = Row1.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row1.Cells.Add
            cell.StyleID = "s95"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "? ??????????? ???????? ??????? ? ?????????????   ??"
            cell.Index = 4
            cell = Row1.Cells.Add
            cell.StyleID = "s95"
            cell.Index = 6
            cell = Row1.Cells.Add
            cell.StyleID = "s95"
            cell = Row1.Cells.Add
            cell.StyleID = "s207"
            cell = Row1.Cells.Add
            cell.StyleID = "s207"
            cell = Row1.Cells.Add
            cell.StyleID = "s237"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 12
            '-----------------------------------------------
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            Row2.AutoFitHeight = false
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell = Row2.Cells.Add
            cell.StyleID = "s208"
            Row2.Cells.Add("[??? ???? ?????]", DataType.[String], "s83")
            cell = Row2.Cells.Add
            cell.StyleID = "s209"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????:"
            cell.Index = 8
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            cell.Index = 11
            Row2.Cells.Add("[????????]", DataType.[String], "s71")
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            cell = Row2.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
            Row3.AutoFitHeight = false
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell = Row3.Cells.Add
            cell.StyleID = "s209"
            Row3.Cells.Add("[????? ???? ?????]", DataType.[String], "s209")
            cell = Row3.Cells.Add
            cell.StyleID = "s209"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ?????????:"
            cell.Index = 8
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????]"
            cell.Index = 12
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            cell = Row3.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row4 As WorksheetRow = sheet.Table.Rows.Add
            Row4.AutoFitHeight = false
            cell = Row4.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row4.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:   575-22-48"
            cell = Row4.Cells.Add
            cell.StyleID = "s210"
            cell = Row4.Cells.Add
            cell.StyleID = "s210"
            cell = Row4.Cells.Add
            cell.StyleID = "s209"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ??????:"
            cell.Index = 8
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???_??????]"
            cell.Index = 12
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            cell = Row4.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row5 As WorksheetRow = sheet.Table.Rows.Add
            Row5.AutoFitHeight = false
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ???????:     Fg=[G/(n*G??)]*100%         dt=t1-t2          "
            cell.Index = 2
            cell = Row5.Cells.Add
            cell.StyleID = "s210"
            cell = Row5.Cells.Add
            cell.StyleID = "s210"
            cell = Row5.Cells.Add
            cell.StyleID = "s209"
            cell.Index = 8
            cell = Row5.Cells.Add
            cell.StyleID = "s71"
            cell.Index = 13
            cell = Row5.Cells.Add
            cell.StyleID = "s71"
            '-----------------------------------------------
            Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
            Row6.AutoFitHeight = false
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????, ????????  ??-?: ??????????"
            cell = Row6.Cells.Add
            cell.StyleID = "s210"
            cell = Row6.Cells.Add
            cell.StyleID = "s210"
            cell = Row6.Cells.Add
            cell.StyleID = "s82"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????]"
            cell.Index = 8
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ???.:"
            cell.Index = 10
            cell = Row6.Cells.Add
            cell.StyleID = "s78"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G??_min??]"
            cell.Index = 12
            Row6.Cells.Add("[G????]", DataType.[String], "s78")
            Row6.Cells.Add("? ???./???", DataType.[String], "s71")
            '-----------------------------------------------
            Dim Row7 As WorksheetRow = sheet.Table.Rows.Add
            Row7.AutoFitHeight = false
            cell = Row7.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row7.Cells.Add("?????????? ???:", DataType.[String], "s72")
            cell = Row7.Cells.Add
            cell.StyleID = "s212"
            cell = Row7.Cells.Add
            cell.StyleID = "s95"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????????? ???]"
            cell.Index = 5
            cell = Row7.Cells.Add
            cell.StyleID = "s71"
            cell.Index = 7
            cell = Row7.Cells.Add
            cell.StyleID = "s71"
            cell = Row7.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ???.:"
            cell.Index = 10
            cell = Row7.Cells.Add
            cell.StyleID = "s78"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G??(??? min)]"
            cell.Index = 12
            Row7.Cells.Add("[G(???)??]", DataType.[String], "s78")
            Row7.Cells.Add("? ???./???", DataType.[String], "s71")
            '-----------------------------------------------
            Dim Row8 As WorksheetRow = sheet.Table.Rows.Add
            Row8.AutoFitHeight = false
            cell = Row8.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row8.Cells.Add("???????????? :", DataType.[String], "s72")
            cell = Row8.Cells.Add
            cell.StyleID = "s212"
            Row8.Cells.Add("[????? ????????]", DataType.[String], "s95")
            cell = Row8.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ???????  G?? ="
            cell.Index = 7
            cell = Row8.Cells.Add
            cell.StyleID = "s213"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????? G2]"
            cell.Index = 11
            cell = Row8.Cells.Add
            cell.StyleID = "s211"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G???="
            cell.Index = 13
            Row8.Cells.Add("[G???]", DataType.[String], "s213")
            '-----------------------------------------------
            Dim Row9 As WorksheetRow = sheet.Table.Rows.Add
            Row9.Height = 14
            Row9.AutoFitHeight = false
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row9.Cells.Add("??????? ?", DataType.[String], "s82")
            cell = Row9.Cells.Add
            cell.StyleID = "s212"
            Row9.Cells.Add("[???????]", DataType.[String], "s83")
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ????????  ??1="
            cell.Index = 7
            cell = Row9.Cells.Add
            cell.StyleID = "s78"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???]"
            cell.Index = 11
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??/??2"
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??2="
            Row9.Cells.Add("[????]", DataType.[String], "s78")
            cell = Row9.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??/??2"
            '-----------------------------------------------
            Dim Row10 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row10.Cells.Add
            cell.StyleID = "s214"
            Row10.Cells.Add(",", DataType.[String], "s90")
            cell = Row10.Cells.Add
            cell.StyleID = "s91"
            Row10.Cells.Add("  ???????? ???????????", DataType.[String], "s215")
            cell = Row10.Cells.Add
            cell.StyleID = "s215"
            cell = Row10.Cells.Add
            cell.StyleID = "s215"
            cell = Row10.Cells.Add
            cell.StyleID = "s215"
            cell = Row10.Cells.Add
            cell.StyleID = "s216"
            cell = Row10.Cells.Add
            cell.StyleID = "m85478096"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? ???????????"
            cell.MergeAcross = 3
            cell = Row10.Cells.Add
            cell.StyleID = "s91"
            cell = Row10.Cells.Add
            cell.StyleID = "m85477792"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ???  [?] (??? ????????)"
            cell.MergeDown = 2
            cell = Row10.Cells.Add
            cell.StyleID = "s217"
            '-----------------------------------------------
            Dim Row11 As WorksheetRow = sheet.Table.Rows.Add
            Row11.Height = 14
            Row11.AutoFitHeight = false
            cell = Row11.Cells.Add
            cell.StyleID = "s218"
            Row11.Cells.Add("????", DataType.[String], "s92")
            Row11.Cells.Add("????", DataType.[String], "s219")
            Row11.Cells.Add("Fg1", DataType.[String], "s75")
            Row11.Cells.Add("  t1", DataType.[String], "s75")
            Row11.Cells.Add(" P1", DataType.[String], "s75")
            Row11.Cells.Add("   M1", DataType.[String], "s220")
            Row11.Cells.Add("    dt", DataType.[String], "s84")
            Row11.Cells.Add("Fg2", DataType.[String], "s75")
            Row11.Cells.Add("  t2", DataType.[String], "s75")
            Row11.Cells.Add(" P2", DataType.[String], "s75")
            Row11.Cells.Add("  M2", DataType.[String], "s75")
            Row11.Cells.Add(" M1-M2", DataType.[String], "s75")
            cell = Row11.Cells.Add
            cell.StyleID = "s93"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = " Q???"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row12 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row12.Cells.Add
            cell.StyleID = "s218"
            cell = Row12.Cells.Add
            cell.StyleID = "s221"
            cell = Row12.Cells.Add
            cell.StyleID = "s222"
            Row12.Cells.Add(" %", DataType.[String], "s76")
            Row12.Cells.Add("??.C", DataType.[String], "s76")
            Row12.Cells.Add("??/??", DataType.[String], "s76")
            Row12.Cells.Add("  ????", DataType.[String], "s223")
            Row12.Cells.Add("  ??.?", DataType.[String], "s76")
            Row12.Cells.Add(" %", DataType.[String], "s76")
            Row12.Cells.Add(" ??.C", DataType.[String], "s76")
            Row12.Cells.Add("??/??2", DataType.[String], "s76")
            Row12.Cells.Add(" ????", DataType.[String], "s76")
            Row12.Cells.Add(" ????", DataType.[String], "s76")
            cell = Row12.Cells.Add
            cell.StyleID = "s224"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "  ????"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row13 As WorksheetRow = sheet.Table.Rows.Add
            Row13.Height = 14
            Row13.AutoFitHeight = false
            Row13.Cells.Add("[Report:?????]", DataType.[String], "s225")
            Row13.Cells.Add("[????? ??????]", DataType.[String], "s226")
            Row13.Cells.Add("24", DataType.Number, "s227")
            cell = Row13.Cells.Add
            cell.StyleID = "s228"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[3]/(RC[-1]*R7C13)*100%"
            Row13.Cells.Add("[T1 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R10C11*1"
            Row13.Cells.Add("[?1 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-3]-RC[2]"
            cell = Row13.Cells.Add
            cell.StyleID = "s228"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[3]/(RC[-6]*R7C13)*100%"
            Row13.Cells.Add("[T2 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R10C14*1"
            Row13.Cells.Add("[?2 ???]", DataType.[String], "s229")
            cell = Row13.Cells.Add
            cell.StyleID = "s94"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-6]-RC[-1]"
            Row13.Cells.Add("[?3 ???]", DataType.[String], "s229")
            Row13.Cells.Add("[Q??1 ???]", DataType.[String], "s230")
            cell = Row13.Cells.Add
            cell.StyleID = "s152"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[T3 ???]"
            cell.Index = 17
            Row13.Cells.Add("[Q2 ]", DataType.[String], "s154")
            Row13.Cells.Add("[?3 ???]", DataType.[String], "s229")
            '-----------------------------------------------
            Dim Row14 As WorksheetRow = sheet.Table.Rows.Add
            Row14.Height = 14
            Row14.AutoFitHeight = false
            cell = Row14.Cells.Add
            cell.StyleID = "s231"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row14.Cells.Add
            cell.StyleID = "s80"
            cell = Row14.Cells.Add
            cell.StyleID = "s232"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s232"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s79"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row14.Cells.Add
            cell.StyleID = "s233"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            '-----------------------------------------------
            Dim Row15 As WorksheetRow = sheet.Table.Rows.Add
            Row15.Height = 14
            Row15.AutoFitHeight = false
            cell = Row15.Cells.Add
            cell.StyleID = "s238"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row15.Cells.Add
            cell.StyleID = "s240"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s85"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s73"
            cell = Row15.Cells.Add
            cell.StyleID = "s85"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s85"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s85"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row15.Cells.Add
            cell.StyleID = "s241"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            '-----------------------------------------------
            Dim Row16 As WorksheetRow = sheet.Table.Rows.Add
            Row16.Height = 14
            Row16.AutoFitHeight = false
            cell = Row16.Cells.Add
            cell.StyleID = "s245"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?? ???????? ? 19.02.07   ?? 24.02.07"
            cell.Index = 2
            cell = Row16.Cells.Add
            cell.StyleID = "s246"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s247"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s88"
            cell = Row16.Cells.Add
            cell.StyleID = "s247"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=R[-2]C*6"
            cell = Row16.Cells.Add
            cell.StyleID = "s247"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R[-2]C*6"
            cell = Row16.Cells.Add
            cell.StyleID = "s247"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=R[-2]C*6"
            cell = Row16.Cells.Add
            cell.StyleID = "s89"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=R[-2]C*6"
            '-----------------------------------------------
            Dim Row17 As WorksheetRow = sheet.Table.Rows.Add
            Row17.Height = 14
            Row17.AutoFitHeight = false
            cell = Row17.Cells.Add
            cell.StyleID = "s242"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row17.Cells.Add
            cell.StyleID = "s243"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s244"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s86"
            cell = Row17.Cells.Add
            cell.StyleID = "s244"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=R[-2]C+R[-1]C"
            cell = Row17.Cells.Add
            cell.StyleID = "s244"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R[-2]C+R[-1]C"
            cell = Row17.Cells.Add
            cell.StyleID = "s244"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=R[-2]C+R[-1]C"
            cell = Row17.Cells.Add
            cell.StyleID = "s87"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=R[-2]C+R[-1]C"
            '-----------------------------------------------
            Dim Row18 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row18.Cells.Add
            cell.StyleID = "s81"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ?????????? ???????????????? ?? ????????????? ??????????? ??????:"
            cell.Index = 2
            cell = Row18.Cells.Add
            cell.StyleID = "s236"
            cell.Index = 5
            cell = Row18.Cells.Add
            cell.StyleID = "s77"
            '-----------------------------------------------
            Dim Row19 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row19.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeAcross = 2
            cell = Row19.Cells.Add
            cell.StyleID = "s321"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "M1, ?"
            cell.MergeAcross = 1
            cell = Row19.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "M2, ?"
            cell.MergeAcross = 1
            cell = Row19.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???, ????"
            cell.MergeAcross = 1
            cell = Row19.Cells.Add
            cell.StyleID = "s309"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "M???,?"
            cell.MergeAcross = 1
            cell = Row19.Cells.Add
            cell.StyleID = "m85477832"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "T???., ???"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row20 As WorksheetRow = sheet.Table.Rows.Add
            Row20.Height = 14
            Row20.AutoFitHeight = false
            cell = Row20.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:????????]"
            cell = Row20.Cells.Add
            cell.StyleID = "s320"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ??????]"
            cell.MergeAcross = 2
            cell = Row20.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M1 ???]"
            cell.MergeAcross = 1
            cell = Row20.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M2 ???]"
            cell.MergeAcross = 1
            cell = Row20.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??1 ???]"
            cell.MergeAcross = 1
            cell = Row20.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M3 ???]"
            cell.MergeAcross = 1
            cell = Row20.Cells.Add
            cell.StyleID = "s317"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[TSUM1 ???]"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row21 As WorksheetRow = sheet.Table.Rows.Add
            Row21.Index = 23
            cell = Row21.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ???? "
            cell.Index = 4
            cell = Row21.Cells.Add
            cell.StyleID = "s77"
            cell.Index = 6
            cell = Row21.Cells.Add
            cell.StyleID = "s77"
            cell = Row21.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ?.?."
            '-----------------------------------------------
            ' Options
            '-----------------------------------------------
            sheet.Options.ProtectObjects = false
            sheet.Options.ProtectScenarios = false
            sheet.Options.Print.PaperSizeIndex = 9
            sheet.Options.Print.HorizontalResolution = -4
            sheet.Options.Print.VerticalResolution = 180
            sheet.Options.Print.ValidPrinterInfo = true
        End Sub
        
        Private Overloads Sub GenerateWorksheetSheet2(ByVal sheets As WorksheetCollection)
            Dim sheet As Worksheet = sheets.Add("Sheet2")
            sheet.Table.ExpandedColumnCount = 99
            sheet.Table.ExpandedRowCount = 62
            sheet.Table.FullColumns = 1
            sheet.Table.FullRows = 1
            sheet.Table.Columns.Add(105)
            Dim column1 As WorksheetColumn = sheet.Table.Columns.Add
            column1.Width = 39
            column1.StyleID = "s96"
            Dim column2 As WorksheetColumn = sheet.Table.Columns.Add
            column2.Width = 28
            column2.StyleID = "s96"
            Dim column3 As WorksheetColumn = sheet.Table.Columns.Add
            column3.Width = 18
            column3.StyleID = "s96"
            Dim column4 As WorksheetColumn = sheet.Table.Columns.Add
            column4.Width = 33
            column4.StyleID = "s96"
            column4.Span = 1
            Dim column5 As WorksheetColumn = sheet.Table.Columns.Add
            column5.Index = 7
            column5.Width = 30
            column5.StyleID = "s96"
            Dim column6 As WorksheetColumn = sheet.Table.Columns.Add
            column6.Width = 32
            column6.StyleID = "s96"
            column6.Span = 2
            Dim column7 As WorksheetColumn = sheet.Table.Columns.Add
            column7.Index = 11
            column7.Width = 23
            column7.StyleID = "s96"
            column7.Span = 1
            Dim column8 As WorksheetColumn = sheet.Table.Columns.Add
            column8.Index = 13
            column8.Width = 33
            column8.StyleID = "s96"
            Dim column9 As WorksheetColumn = sheet.Table.Columns.Add
            column9.Width = 22
            column9.StyleID = "s96"
            column9.Span = 2
            Dim column10 As WorksheetColumn = sheet.Table.Columns.Add
            column10.Index = 17
            column10.Width = 21
            column10.StyleID = "s96"
            Dim column11 As WorksheetColumn = sheet.Table.Columns.Add
            column11.Width = 26
            column11.StyleID = "s96"
            Dim column12 As WorksheetColumn = sheet.Table.Columns.Add
            column12.Width = 39
            column12.StyleID = "s96"
            Dim column13 As WorksheetColumn = sheet.Table.Columns.Add
            column13.Width = 39
            column13.StyleID = "s96"
            Dim column14 As WorksheetColumn = sheet.Table.Columns.Add
            column14.Width = 35
            column14.StyleID = "s96"
            Dim column15 As WorksheetColumn = sheet.Table.Columns.Add
            column15.Width = 35
            column15.Span = 77
            '-----------------------------------------------
            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row0.Height = 11
            Row0.AutoFitHeight = false
            Row0.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Dim cell As WorksheetCell
            cell = Row0.Cells.Add
            cell.StyleID = "s97"
            cell.Index = 3
            cell = Row0.Cells.Add
            cell.StyleID = "s97"
            Row0.Cells.Add("????? ? ???????????????? ?? ???????? ???? ", DataType.[String], "s98")
            cell = Row0.Cells.Add
            cell.StyleID = "s289"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 13
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.Height = 11
            Row1.AutoFitHeight = false
            Row1.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row1.Cells.Add("???????: ??? ""???"" ????????????? ??????   ???????:", DataType.[String], "s99")
            cell = Row1.Cells.Add
            cell.StyleID = "s97"
            cell = Row1.Cells.Add
            cell.StyleID = "s97"
            cell = Row1.Cells.Add
            cell.StyleID = "s98"
            cell = Row1.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????]"
            cell.Index = 10
            cell.MergeAcross = 2
            cell = Row1.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???. 575-22-48"
            cell = Row1.Cells.Add
            cell.StyleID = "s101"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???? ?????:      "
            cell.Index = 16
            cell = Row1.Cells.Add
            cell.StyleID = "s102"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???? ?????]"
            cell.Index = 18
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            Row2.Height = 11
            Row2.AutoFitHeight = false
            Row2.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            Row2.Cells.Add("[??? ???? ?????]", DataType.[String], "s100")
            cell = Row2.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ???? ?????]"
            cell.Index = 5
            cell = Row2.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????? ?????:"
            cell.Index = 9
            cell = Row2.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???.?????]"
            cell.Index = 13
            cell = Row2.Cells.Add
            cell.StyleID = "s304"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??? ????:"
            cell.Index = 16
            cell.MergeAcross = 1
            cell = Row2.Cells.Add
            cell.StyleID = "s305"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??? ????]"
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
            Row3.Height = 11
            Row3.AutoFitHeight = false
            Row3.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ???????????:     ??? ???  ???  ?????????????? ??????? ??? ""???"""
            cell = Row3.Cells.Add
            cell.StyleID = "s104"
            cell = Row3.Cells.Add
            cell.StyleID = "s104"
            cell = Row3.Cells.Add
            cell.StyleID = "s99"
            cell.Index = 8
            cell = Row3.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:  575-22-44"
            cell.Index = 15
            '-----------------------------------------------
            Dim Row4 As WorksheetRow = sheet.Table.Rows.Add
            Row4.Height = 11
            Row4.AutoFitHeight = false
            Row4.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row4.Cells.Add("????????:", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[????????]", DataType.[String], "s106")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("????? ???????????:", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s107"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[????_????????????????]", DataType.[String], "s108")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("??????", DataType.[String], "s105")
            cell = Row4.Cells.Add
            cell.StyleID = "s105"
            Row4.Cells.Add("[?_??????]", DataType.[String], "s108")
            '-----------------------------------------------
            Dim Row5 As WorksheetRow = sheet.Table.Rows.Add
            Row5.Height = 11
            Row5.AutoFitHeight = false
            Row5.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ???????:"
            cell = Row5.Cells.Add
            cell.StyleID = "s104"
            cell = Row5.Cells.Add
            cell.StyleID = "s109"
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????? ?????? ? ?????:"
            cell.Index = 7
            cell = Row5.Cells.Add
            cell.StyleID = "s99"
            cell.Index = 9
            cell = Row5.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????_?????]"
            cell.Index = 11
            cell = Row5.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 15
            cell = Row5.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???,??.? ="
            cell.Index = 17
            cell = Row5.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???]"
            cell.Index = 19
            '-----------------------------------------------
            Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
            Row6.Height = 11
            Row6.AutoFitHeight = false
            Row6.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????:"
            cell = Row6.Cells.Add
            cell.StyleID = "s104"
            Row6.Cells.Add("[????? ????????]", DataType.[String], "s110")
            cell = Row6.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[? ???????]"
            cell.Index = 6
            cell = Row6.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????(?????):"
            cell.Index = 8
            cell = Row6.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[?????]"
            cell.Index = 10
            cell = Row6.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????  ???? ????????  ?? :"
            cell.Index = 12
            cell = Row6.Cells.Add
            cell.StyleID = "s109"
            cell = Row6.Cells.Add
            cell.StyleID = "s109"
            cell = Row6.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???? ???????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row7 As WorksheetRow = sheet.Table.Rows.Add
            Row7.Height = 11
            Row7.AutoFitHeight = false
            Row7.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????.??.(?1):   "
            cell.MergeAcross = 2
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row7.Cells.Add("[??????????]", DataType.[String], "s103")
            cell = Row7.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row7.Cells.Add("[G??_min??]", DataType.[String], "s100")
            cell = Row7.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row7.Cells.Add("[G????]", DataType.[String], "s100")
            cell = Row7.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row7.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row8 As WorksheetRow = sheet.Table.Rows.Add
            Row8.Height = 11
            Row8.AutoFitHeight = false
            Row8.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????.??.(?2):   "
            cell.MergeAcross = 2
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row8.Cells.Add("[??????????]", DataType.[String], "s103")
            cell = Row8.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row8.Cells.Add("[G??_min??]", DataType.[String], "s100")
            cell = Row8.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row8.Cells.Add("[G????]", DataType.[String], "s100")
            cell = Row8.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row8.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????????]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row9 As WorksheetRow = sheet.Table.Rows.Add
            Row9.Height = 11
            Row9.AutoFitHeight = false
            Row9.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??-? ???(?3):   "
            cell.MergeAcross = 2
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            Row9.Cells.Add("[?????????? ???]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            Row9.Cells.Add("[G??(??? min)]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            Row9.Cells.Add("[G(???)??]", DataType.[String], "s100")
            cell = Row9.Cells.Add
            cell.StyleID = "s109"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????????.:"
            cell.Index = 15
            cell = Row9.Cells.Add
            cell.StyleID = "s103"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[??????????? ???]"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row10 As WorksheetRow = sheet.Table.Rows.Add
            Row10.Height = 11
            Row10.AutoFitHeight = false
            Row10.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??.????.???(?4):  "
            cell.MergeAcross = 2
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.MergeAcross = 1
            cell = Row10.Cells.Add
            cell.StyleID = "s110"
            cell = Row10.Cells.Add
            cell.StyleID = "s112"
            Row10.Cells.Add("???????????.:", DataType.[String], "s109")
            cell = Row10.Cells.Add
            cell.StyleID = "s100"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row11 As WorksheetRow = sheet.Table.Rows.Add
            Row11.Height = 11
            Row11.AutoFitHeight = false
            Row11.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??.????????(V5):   "
            cell.MergeAcross = 2
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????:"
            cell.MergeAcross = 1
            cell = Row11.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmin="
            cell.Index = 9
            cell = Row11.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Gmax="
            cell.Index = 11
            cell.MergeAcross = 1
            cell = Row11.Cells.Add
            cell.StyleID = "s109"
            cell = Row11.Cells.Add
            cell.StyleID = "s112"
            Row11.Cells.Add("???????????.:", DataType.[String], "s109")
            cell = Row11.Cells.Add
            cell.StyleID = "s110"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row12 As WorksheetRow = sheet.Table.Rows.Add
            Row12.Height = 11
            Row12.AutoFitHeight = false
            Row12.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row12.Cells.Add("????????? ????????:", DataType.[String], "s105")
            cell = Row12.Cells.Add
            cell.StyleID = "s113"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            Row12.Cells.Add("????:", DataType.[String], "s105")
            Row12.Cells.Add("[???????]", DataType.[String], "s108")
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s107"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s114"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????:"
            cell.MergeAcross = 1
            Row12.Cells.Add("[???????2]", DataType.[String], "s108")
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            cell = Row12.Cells.Add
            cell.StyleID = "s105"
            '-----------------------------------------------
            Dim Row13 As WorksheetRow = sheet.Table.Rows.Add
            Row13.Height = 11
            Row13.AutoFitHeight = false
            Row13.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ????????,????/???:"
            cell = Row13.Cells.Add
            cell.StyleID = "s104"
            cell = Row13.Cells.Add
            cell.StyleID = "s104"
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q??.="
            cell.Index = 7
            Row13.Cells.Add("[Q??]", DataType.[String], "s115")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q????.="
            Row13.Cells.Add("[Q????]", DataType.[String], "s115")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.="
            cell.Index = 12
            Row13.Cells.Add("[Q???]", DataType.[String], "s116")
            cell = Row13.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.???="
            cell = Row13.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q???_???]"
            cell.Index = 16
            cell = Row13.Cells.Add
            cell.StyleID = "s117"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.="
            cell.MergeAcross = 1
            Row13.Cells.Add("[Q???]", DataType.[String], "s115")
            '-----------------------------------------------
            Dim Row14 As WorksheetRow = sheet.Table.Rows.Add
            Row14.Height = 11
            Row14.AutoFitHeight = false
            Row14.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????????? ???????? (??.???),????/???:"
            cell = Row14.Cells.Add
            cell.StyleID = "s104"
            cell = Row14.Cells.Add
            cell.StyleID = "s104"
            cell = Row14.Cells.Add
            cell.StyleID = "s110"
            cell.Index = 7
            cell = Row14.Cells.Add
            cell.StyleID = "s99"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.??? ??="
            cell.Index = 9
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q???_??? ??]"
            cell.Index = 11
            cell = Row14.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???.??="
            cell.Index = 15
            cell = Row14.Cells.Add
            cell.StyleID = "s100"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??? ??]"
            cell.Index = 17
            '-----------------------------------------------
            Dim Row15 As WorksheetRow = sheet.Table.Rows.Add
            Row15.Height = 11
            Row15.AutoFitHeight = false
            Row15.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????. ???????? (??.???),?/???:"
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "   G??.="
            cell.Index = 7
            Row15.Cells.Add("[G??]", DataType.[String], "s115")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G????.="
            Row15.Cells.Add("[G????]", DataType.[String], "s115")
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G???.="
            cell.Index = 12
            Row15.Cells.Add("[G???]", DataType.[String], "s116")
            cell = Row15.Cells.Add
            cell.StyleID = "s111"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "G???.="
            cell.MergeAcross = 1
            Row15.Cells.Add("[G???]", DataType.[String], "s100")
            cell = Row15.Cells.Add
            cell.StyleID = "s116"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "  G???.? ="
            cell.MergeAcross = 1
            cell = Row15.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[G???_?]"
            '-----------------------------------------------
            Dim Row16 As WorksheetRow = sheet.Table.Rows.Add
            Row16.Height = 11
            Row16.AutoFitHeight = false
            cell = Row16.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:??????????]"
            Row16.Cells.Add("?????.????????:", DataType.[String], "s105")
            cell = Row16.Cells.Add
            cell.StyleID = "s118"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            Row16.Cells.Add("?1=", DataType.[String], "s114")
            Row16.Cells.Add("[???]", DataType.[String], "s118")
            Row16.Cells.Add("?2=", DataType.[String], "s114")
            Row16.Cells.Add("[????]", DataType.[String], "s107")
            Row16.Cells.Add("    t1=", DataType.[String], "s114")
            Row16.Cells.Add("[?1]", DataType.[String], "s119")
            Row16.Cells.Add("    t2=", DataType.[String], "s114")
            Row16.Cells.Add("[?2]", DataType.[String], "s119")
            Row16.Cells.Add("t3=", DataType.[String], "s114")
            cell = Row16.Cells.Add
            cell.StyleID = "s119"
            Row16.Cells.Add("t4=", DataType.[String], "s105")
            cell = Row16.Cells.Add
            cell.StyleID = "s119"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            cell = Row16.Cells.Add
            cell.StyleID = "s105"
            '-----------------------------------------------
            Dim Row17 As WorksheetRow = sheet.Table.Rows.Add
            Row17.Height = 11
            Row17.AutoFitHeight = false
            cell = Row17.Cells.Add
            cell.StyleID = "s112"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ?????????? ???????? ??????"
            cell.Index = 2
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell = Row17.Cells.Add
            cell.StyleID = "s120"
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 7
            cell = Row17.Cells.Add
            cell.StyleID = "s121"
            cell.Index = 10
            cell = Row17.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 18
            '-----------------------------------------------
            Dim Row18 As WorksheetRow = sheet.Table.Rows.Add
            Row18.Height = 11
            Row18.AutoFitHeight = false
            cell = Row18.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 2
            cell = Row18.Cells.Add
            cell.StyleID = "s116"
            cell = Row18.Cells.Add
            cell.StyleID = "m85479316"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???? (??-1)"
            cell.Index = 5
            cell.MergeAcross = 7
            cell = Row18.Cells.Add
            cell.StyleID = "m84779028"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????? (??-2)"
            cell.MergeAcross = 5
            cell = Row18.Cells.Add
            cell.StyleID = "s112"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row19 As WorksheetRow = sheet.Table.Rows.Add
            Row19.Height = 11
            Row19.AutoFitHeight = false
            cell = Row19.Cells.Add
            cell.StyleID = "m84779008"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeDown = 1
            Row19.Cells.Add("??", DataType.[String], "s122")
            Row19.Cells.Add("??", DataType.[String], "s123")
            Row19.Cells.Add("    ?1,", DataType.[String], "s124")
            Row19.Cells.Add("    ?2,", DataType.[String], "s125")
            Row19.Cells.Add("d?,", DataType.[String], "s126")
            Row19.Cells.Add("?1,", DataType.[String], "s127")
            Row19.Cells.Add("?2,", DataType.[String], "s122")
            Row19.Cells.Add("d?,", DataType.[String], "s126")
            Row19.Cells.Add(" P1,", DataType.[String], "s127")
            Row19.Cells.Add(" P2,", DataType.[String], "s126")
            Row19.Cells.Add("?3", DataType.[String], "s127")
            Row19.Cells.Add("M4,", DataType.[String], "s128")
            Row19.Cells.Add("dM,", DataType.[String], "s122")
            Row19.Cells.Add("V5,", DataType.[String], "s129")
            Row19.Cells.Add("?3,", DataType.[String], "s127")
            Row19.Cells.Add("?4,", DataType.[String], "s129")
            Row19.Cells.Add(" Q", DataType.[String], "s130")
            cell = Row19.Cells.Add
            cell.StyleID = "s110"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row20 As WorksheetRow = sheet.Table.Rows.Add
            Row20.Height = 11
            Row20.AutoFitHeight = false
            cell = Row20.Cells.Add
            cell.StyleID = "s131"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???"
            cell.Index = 3
            cell = Row20.Cells.Add
            cell.StyleID = "s132"
            Row20.Cells.Add("  ?", DataType.[String], "s133")
            Row20.Cells.Add("  ?", DataType.[String], "s131")
            Row20.Cells.Add("  ?", DataType.[String], "s134")
            Row20.Cells.Add("?", DataType.[String], "s135")
            Row20.Cells.Add("?", DataType.[String], "s131")
            Row20.Cells.Add("?", DataType.[String], "s134")
            Row20.Cells.Add("??/??2", DataType.[String], "s134")
            Row20.Cells.Add("??/??2", DataType.[String], "s134")
            Row20.Cells.Add("  ?", DataType.[String], "s136")
            Row20.Cells.Add("?", DataType.[String], "s137")
            Row20.Cells.Add("?", DataType.[String], "s131")
            Row20.Cells.Add("?.???", DataType.[String], "s138")
            Row20.Cells.Add("?", DataType.[String], "s135")
            Row20.Cells.Add("?", DataType.[String], "s132")
            Row20.Cells.Add("  ????", DataType.[String], "s139")
            cell = Row20.Cells.Add
            cell.StyleID = "s140"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row21 As WorksheetRow = sheet.Table.Rows.Add
            Row21.Height = 11
            Row21.AutoFitHeight = false
            Row21.Cells.Add("[Report:?????]", DataType.[String], "s141")
            Row21.Cells.Add("[????? ??????]", DataType.[String], "s142")
            Row21.Cells.Add("24", DataType.Number, "s143")
            cell = Row21.Cells.Add
            cell.StyleID = "s144"
            Row21.Cells.Add("[M1 ???]", DataType.[String], "s145")
            Row21.Cells.Add("[?2 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s147"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-2]-RC[-1]"
            Row21.Cells.Add("[T1 ???]", DataType.[String], "s148")
            Row21.Cells.Add("[T2 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s147"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=RC[-2]-RC[-1]"
            cell = Row21.Cells.Add
            cell.StyleID = "s149"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R17C6*1"
            cell = Row21.Cells.Add
            cell.StyleID = "s150"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=R17C8*1"
            cell = Row21.Cells.Add
            cell.StyleID = "s151"
            Row21.Cells.Add("[?4 ???]", DataType.[String], "s146")
            cell = Row21.Cells.Add
            cell.StyleID = "s146"
            cell = Row21.Cells.Add
            cell.StyleID = "s152"
            cell = Row21.Cells.Add
            cell.StyleID = "s148"
            Row21.Cells.Add("[T4 ???]", DataType.[String], "s152")
            Row21.Cells.Add("[Q1 ???]", DataType.[String], "s153")
            cell = Row21.Cells.Add
            cell.StyleID = "s154"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q2 ???]"
            cell.Index = 21
            '-----------------------------------------------
            Dim Row22 As WorksheetRow = sheet.Table.Rows.Add
            Row22.Height = 11
            Row22.AutoFitHeight = false
            cell = Row22.Cells.Add
            cell.StyleID = "s155"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row22.Cells.Add
            cell.StyleID = "s156"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s157"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s159"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s162"
            cell = Row22.Cells.Add
            cell.StyleID = "s162"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s160"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell = Row22.Cells.Add
            cell.StyleID = "s158"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s163"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            cell = Row22.Cells.Add
            cell.StyleID = "s161"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#DIV/0!"
            cell.Index = 21
            cell.Formula = "=AVERAGE(R[-2]C:R[-1]C)"
            '-----------------------------------------------
            Dim Row23 As WorksheetRow = sheet.Table.Rows.Add
            Row23.Height = 11
            Row23.AutoFitHeight = false
            cell = Row23.Cells.Add
            cell.StyleID = "s164"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????"
            cell.Index = 2
            cell = Row23.Cells.Add
            cell.StyleID = "s165"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "24"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s166"
            cell = Row23.Cells.Add
            cell.StyleID = "s167"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.[Error]
            cell.Data.Text = "#VALUE!"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s169"
            cell = Row23.Cells.Add
            cell.StyleID = "s170"
            cell = Row23.Cells.Add
            cell.StyleID = "s166"
            cell = Row23.Cells.Add
            cell.StyleID = "s169"
            cell = Row23.Cells.Add
            cell.StyleID = "s171"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell = Row23.Cells.Add
            cell.StyleID = "s168"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s172"
            cell = Row23.Cells.Add
            cell.StyleID = "s173"
            cell = Row23.Cells.Add
            cell.StyleID = "s167"
            cell = Row23.Cells.Add
            cell.StyleID = "s174"
            cell = Row23.Cells.Add
            cell.StyleID = "s175"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            cell = Row23.Cells.Add
            cell.StyleID = "s176"
            cell.Data.Type = DataType.Number
            cell.Data.Text = "0"
            cell.Index = 21
            cell.Formula = "=SUM(R[-3]C:R[-2]C)"
            '-----------------------------------------------
            Dim Row24 As WorksheetRow = sheet.Table.Rows.Add
            Row24.Height = 11
            Row24.AutoFitHeight = false
            cell = Row24.Cells.Add
            cell.StyleID = "s177"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????? ?? ???????? ?????:"
            cell.Index = 2
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s109"
            cell = Row24.Cells.Add
            cell.StyleID = "s179"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            cell = Row24.Cells.Add
            cell.StyleID = "s179"
            cell = Row24.Cells.Add
            cell.StyleID = "s178"
            '-----------------------------------------------
            Dim Row25 As WorksheetRow = sheet.Table.Rows.Add
            Row25.Height = 11
            Row25.AutoFitHeight = false
            cell = Row25.Cells.Add
            cell.StyleID = "s180"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "???????:"
            cell.Index = 2
            cell = Row25.Cells.Add
            cell.StyleID = "s181"
            cell = Row25.Cells.Add
            cell.StyleID = "s182"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s184"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s183"
            cell = Row25.Cells.Add
            cell.StyleID = "s185"
            cell = Row25.Cells.Add
            cell.StyleID = "s186"
            '-----------------------------------------------
            Dim Row26 As WorksheetRow = sheet.Table.Rows.Add
            Row26.Height = 11
            Row26.AutoFitHeight = false
            cell = Row26.Cells.Add
            cell.StyleID = "s164"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row26.Cells.Add
            cell.StyleID = "s165"
            cell = Row26.Cells.Add
            cell.StyleID = "s187"
            cell = Row26.Cells.Add
            cell.StyleID = "s168"
            cell = Row26.Cells.Add
            cell.StyleID = "s172"
            cell = Row26.Cells.Add
            cell.StyleID = "s173"
            cell = Row26.Cells.Add
            cell.StyleID = "s188"
            cell = Row26.Cells.Add
            cell.StyleID = "s170"
            cell = Row26.Cells.Add
            cell.StyleID = "s189"
            cell = Row26.Cells.Add
            cell.StyleID = "s190"
            cell = Row26.Cells.Add
            cell.StyleID = "s171"
            cell = Row26.Cells.Add
            cell.StyleID = "s168"
            cell = Row26.Cells.Add
            cell.StyleID = "s172"
            cell = Row26.Cells.Add
            cell.StyleID = "s191"
            cell = Row26.Cells.Add
            cell.StyleID = "s187"
            cell = Row26.Cells.Add
            cell.StyleID = "s188"
            cell = Row26.Cells.Add
            cell.StyleID = "s171"
            cell = Row26.Cells.Add
            cell.StyleID = "s192"
            '-----------------------------------------------
            Dim Row27 As WorksheetRow = sheet.Table.Rows.Add
            Row27.Height = 11
            Row27.AutoFitHeight = false
            cell = Row27.Cells.Add
            cell.StyleID = "s112"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ????????? ?? ?????? ?????? ??????:"
            cell.Index = 2
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell.Index = 5
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            cell = Row27.Cells.Add
            cell.StyleID = "s109"
            '-----------------------------------------------
            Dim Row28 As WorksheetRow = sheet.Table.Rows.Add
            Row28.Height = 11
            Row28.AutoFitHeight = false
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????"
            cell.Index = 2
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?1, ?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?2, ?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?3,?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?4,?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s288"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "V5,?.???"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "m84779576"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q, ????"
            cell.MergeAcross = 2
            cell = Row28.Cells.Add
            cell.StyleID = "m84779556"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??,?"
            cell.MergeAcross = 1
            cell = Row28.Cells.Add
            cell.StyleID = "s193"
            cell = Row28.Cells.Add
            cell.StyleID = "m84779476"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "Q???, ????"
            cell.Index = 21
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row29 As WorksheetRow = sheet.Table.Rows.Add
            Row29.Height = 11
            Row29.AutoFitHeight = false
            cell = Row29.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Report:????????]"
            cell = Row29.Cells.Add
            cell.StyleID = "s299"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[????? ??????]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M1 ???]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M2 ???]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[M4 ???]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "m84779392"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??1 ???]"
            cell.MergeAcross = 2
            cell = Row29.Cells.Add
            cell.StyleID = "s160"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[TSUM1]"
            cell.MergeAcross = 1
            cell = Row29.Cells.Add
            cell.StyleID = "s194"
            cell = Row29.Cells.Add
            cell.StyleID = "m84779456"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[Q??2 ???]"
            cell.Index = 21
            cell.MergeAcross = 2
            cell = Row29.Cells.Add
            cell.StyleID = "s195"
            '-----------------------------------------------
            Dim Row30 As WorksheetRow = sheet.Table.Rows.Add
            Row30.Height = 11
            Row30.AutoFitHeight = false
            cell = Row30.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ??????? ?? ????????:"
            cell.Index = 2
            '-----------------------------------------------
            Dim Row31 As WorksheetRow = sheet.Table.Rows.Add
            Row31.Height = 11
            Row31.AutoFitHeight = false
            cell = Row31.Cells.Add
            cell.StyleID = "s96"
            cell = Row31.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ??????? ?? ????????:"
            cell = Row31.Cells.Add
            cell.StyleID = "s116"
            cell.Index = 15
            cell.MergeAcross = 1
            '-----------------------------------------------
            Dim Row32 As WorksheetRow = sheet.Table.Rows.Add
            Row32.Height = 11
            Row32.AutoFitHeight = false
            cell = Row32.Cells.Add
            cell.StyleID = "s96"
            cell = Row32.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?????????? t2:"
            '-----------------------------------------------
            Dim Row33 As WorksheetRow = sheet.Table.Rows.Add
            Row33.Height = 11
            Row33.AutoFitHeight = false
            cell = Row33.Cells.Add
            cell.StyleID = "s96"
            cell = Row33.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????? ??? ?????? ???????? ""??????????""??? ""??? ???"""
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell.Index = 11
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "/"
            cell.Index = 15
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            cell = Row33.Cells.Add
            cell.StyleID = "s105"
            Row33.Cells.Add("/", DataType.[String], "s114")
            '-----------------------------------------------
            Dim Row34 As WorksheetRow = sheet.Table.Rows.Add
            Row34.Height = 11
            Row34.AutoFitHeight = false
            cell = Row34.Cells.Add
            cell.StyleID = "s96"
            cell = Row34.Cells.Add
            cell.StyleID = "s196"
            cell.Index = 11
            cell = Row34.Cells.Add
            cell.StyleID = "s196"
            '-----------------------------------------------
            Dim Row35 As WorksheetRow = sheet.Table.Rows.Add
            Row35.Height = 11
            Row35.AutoFitHeight = false
            cell = Row35.Cells.Add
            cell.StyleID = "s96"
            cell = Row35.Cells.Add
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ?? ???? ???????? ??????? (?? ????????)"
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell.Index = 11
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "/"
            cell.Index = 15
            Row35.Cells.Add("????? ?.?.   /", DataType.[String], "s105")
            cell = Row35.Cells.Add
            cell.StyleID = "s105"
            cell = Row35.Cells.Add
            cell.StyleID = "s114"
            cell = Row35.Cells.Add
            cell.StyleID = "s197"
            '-----------------------------------------------
            Dim Row36 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row36.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row37 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row37.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row38 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row38.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row39 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row39.Cells.Add
            cell.StyleID = "s96"
            '-----------------------------------------------
            Dim Row40 As WorksheetRow = sheet.Table.Rows.Add
            Row40.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            cell = Row40.Cells.Add
            cell.StyleID = "s198"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??? ? ????????????????  ??  "
            cell.Index = 6
            cell = Row40.Cells.Add
            cell.StyleID = "s289"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "[???????? ????]"
            cell.Index = 11
            cell.MergeAcross = 2
            '-----------------------------------------------
            Dim Row41 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row41.Cells.Add
            cell.StyleID = "s96"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            cell = Row41.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row42 As WorksheetRow = sheet.Table.Rows.Add
            Row42.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row42.Cells.Add("??, ?????????????????: ????????? ?????????? ?????????? ???????? ?????????? ??? ""?"& _ 
                    "?? ???""", DataType.[String], "s199")
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            cell = Row42.Cells.Add
            cell.StyleID = "s199"
            Row42.Cells.Add("[???????]", DataType.[String], "s200")
            '-----------------------------------------------
            Dim Row43 As WorksheetRow = sheet.Table.Rows.Add
            Row43.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row43.Cells.Add("? ????????????? ????????", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("[??? ???? ?????]", DataType.[String], "s200")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("?????????????? ?? ??????:", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 10
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            Row43.Cells.Add("[????? ???? ?????]", DataType.[String], "s200")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 15
            Row43.Cells.Add(" ????????? ", DataType.[String], "s199")
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            cell = Row43.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row44 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??? ? ???, ??? ?? ??????     ???????????? ???????????????? ??????????? "& _ 
                "?????????,"
            cell.Index = 2
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            cell = Row44.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row45 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "? ??????? ?????? ???????? ??????? ? ??????????:"
            cell.Index = 2
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            cell = Row45.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row46 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ?????????? ???????? ??????:"
            cell.Index = 2
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            cell = Row46.Cells.Add
            cell.StyleID = "s199"
            Row46.Cells.Add("=", DataType.[String], "s199")
            cell = Row46.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row46.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row47 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??????????? ?? ?????????? ??????:"
            cell.Index = 2
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            cell = Row47.Cells.Add
            cell.StyleID = "s199"
            Row47.Cells.Add("=", DataType.[String], "s199")
            cell = Row47.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row47.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row48 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "??????????? ??????????? ?? ??????? ???????? ??????:"
            cell.Index = 2
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            cell = Row48.Cells.Add
            cell.StyleID = "s199"
            Row48.Cells.Add("=", DataType.[String], "s199")
            cell = Row48.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row48.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row49 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????? ??????????? ?? ????? ??????:"
            cell.Index = 2
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            cell = Row49.Cells.Add
            cell.StyleID = "s199"
            Row49.Cells.Add("=", DataType.[String], "s199")
            cell = Row49.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row49.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row50 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ?? ??????????? ???????? ???? ?? ????????? ??????????????"
            cell.Index = 2
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            Row50.Cells.Add("??.?", DataType.[String], "s199")
            cell = Row50.Cells.Add
            cell.StyleID = "s199"
            Row50.Cells.Add("=", DataType.[String], "s199")
            cell = Row50.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row50.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row51 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ???????? ?? ??????:"
            cell.Index = 2
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            cell = Row51.Cells.Add
            cell.StyleID = "s199"
            Row51.Cells.Add("=", DataType.[String], "s199")
            cell = Row51.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row51.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row52 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ?????????? ????????? ?? ??????:"
            cell.Index = 2
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            cell = Row52.Cells.Add
            cell.StyleID = "s199"
            Row52.Cells.Add("=", DataType.[String], "s199")
            cell = Row52.Cells.Add
            cell.StyleID = "s290"
            cell.MergeAcross = 1
            Row52.Cells.Add("????", DataType.[String], "s199")
            '-----------------------------------------------
            Dim Row53 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????? ?? ????????? ?????????? ????????????:"
            cell.Index = 2
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            cell = Row53.Cells.Add
            cell.StyleID = "s201"
            Row53.Cells.Add("=", DataType.[String], "s201")
            cell = Row53.Cells.Add
            cell.StyleID = "s306"
            cell.MergeAcross = 1
            Row53.Cells.Add("????", DataType.[String], "s201")
            '-----------------------------------------------
            Dim Row54 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell.Index = 2
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell = Row54.Cells.Add
            cell.StyleID = "s202"
            cell.Index = 18
            cell = Row54.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row55 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row55.Cells.Add
            cell.StyleID = "s203"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "?????:"
            cell.Index = 2
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            cell = Row55.Cells.Add
            cell.StyleID = "s199"
            Row55.Cells.Add("=", DataType.[String], "s203")
            cell = Row55.Cells.Add
            cell.StyleID = "s287"
            cell.MergeAcross = 1
            Row55.Cells.Add("????", DataType.[String], "s203")
            '-----------------------------------------------
            Dim Row56 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 18
            cell = Row56.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row57 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            cell = Row57.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row58 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 13
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            cell = Row58.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row59 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell.Data.Type = DataType.[String]
            cell.Data.Text = "????????????? ????????"
            cell.Index = 2
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s201"
            cell.Index = 13
            cell = Row59.Cells.Add
            cell.StyleID = "s201"
            cell = Row59.Cells.Add
            cell.StyleID = "s201"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            Row59.Cells.Add("????? ?.?.", DataType.[String], "s199")
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            cell = Row59.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row60 As WorksheetRow = sheet.Table.Rows.Add
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 2
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell.Index = 13
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            cell = Row60.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            Dim Row61 As WorksheetRow = sheet.Table.Rows.Add
            Row61.Cells.Add("[Report:??????????]", DataType.[String], "s96")
            Row61.Cells.Add("????????? ?????????? ?????????? ???????? ""??????????"" ???""??? ???""", DataType.[String], "s199")
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s201"
            cell.Index = 13
            cell = Row61.Cells.Add
            cell.StyleID = "s201"
            cell = Row61.Cells.Add
            cell.StyleID = "s201"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            Row61.Cells.Add("[???????]", DataType.[String], "s204")
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            cell = Row61.Cells.Add
            cell.StyleID = "s199"
            '-----------------------------------------------
            ' Options
            '-----------------------------------------------
            sheet.Options.ProtectObjects = false
            sheet.Options.ProtectScenarios = false
            sheet.Options.PageSetup.Header.Margin = 0!
            sheet.Options.PageSetup.Footer.Margin = 0!
            sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008!
            sheet.Options.PageSetup.PageMargins.Left = 0.511811!
            sheet.Options.PageSetup.PageMargins.Right = 0.3149606!
            sheet.Options.PageSetup.PageMargins.Top = 0.3149606!
            sheet.Options.Print.PaperSizeIndex = 9
            sheet.Options.Print.HorizontalResolution = -4
            sheet.Options.Print.VerticalResolution = 180
            sheet.Options.Print.ValidPrinterInfo = true
        End Sub
    End Class
End Namespace
