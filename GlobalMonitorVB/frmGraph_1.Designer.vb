<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGraph
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim PaintElement1 As Infragistics.UltraChart.Resources.Appearance.PaintElement = New Infragistics.UltraChart.Resources.Appearance.PaintElement
        Dim GradientEffect1 As Infragistics.UltraChart.Resources.Appearance.GradientEffect = New Infragistics.UltraChart.Resources.Appearance.GradientEffect
        Dim PaintElement2 As Infragistics.UltraChart.Resources.Appearance.PaintElement = New Infragistics.UltraChart.Resources.Appearance.PaintElement
        Dim GradientEffect2 As Infragistics.UltraChart.Resources.Appearance.GradientEffect = New Infragistics.UltraChart.Resources.Appearance.GradientEffect
        Dim PaintElement3 As Infragistics.UltraChart.Resources.Appearance.PaintElement = New Infragistics.UltraChart.Resources.Appearance.PaintElement
        Dim GradientEffect3 As Infragistics.UltraChart.Resources.Appearance.GradientEffect = New Infragistics.UltraChart.Resources.Appearance.GradientEffect
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem4 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem5 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem6 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem7 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Dim ValueListItem8 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.CHART_A = New Infragistics.Win.UltraWinChart.UltraChart
        Me.CHART_B = New Infragistics.Win.UltraWinChart.UltraChart
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.CHART_C = New Infragistics.Win.UltraWinChart.UltraChart
        Me.chkDay = New Infragistics.Win.UltraWinEditors.UltraCheckEditor
        Me.chkHour = New Infragistics.Win.UltraWinEditors.UltraCheckEditor
        Me.chkMoment = New Infragistics.Win.UltraWinEditors.UltraCheckEditor
        Me.cmdRight = New Infragistics.Win.Misc.UltraButton
        Me.cmdLeft = New Infragistics.Win.Misc.UltraButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtNewTo = New System.Windows.Forms.DateTimePicker
        Me.dtNewFrom = New System.Windows.Forms.DateTimePicker
        Me.optG = New Infragistics.Win.UltraWinEditors.UltraOptionSet
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.CHART_A, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHART_B, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.CHART_C, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(736, 381)
        Me.SplitContainer1.SplitterDistance = 363
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.CHART_A)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.CHART_B)
        Me.SplitContainer3.Size = New System.Drawing.Size(363, 381)
        Me.SplitContainer3.SplitterDistance = 163
        Me.SplitContainer3.TabIndex = 0
        '
        '			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
        '			'ChartType' must be persisted ahead of any Axes change made in design time.
        '		
        Me.CHART_A.ChartType = Infragistics.UltraChart.[Shared].Styles.ChartType.LineChart
        '
        'CHART_A
        '
        Me.CHART_A.Axis.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        PaintElement1.ElementType = Infragistics.UltraChart.[Shared].Styles.PaintElementType.None
        PaintElement1.Fill = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.CHART_A.Axis.PE = PaintElement1
        Me.CHART_A.Axis.X.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_A.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.CHART_A.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.X.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_A.Axis.X.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_A.Axis.X.Labels.SeriesLabels.FormatString = ""
        Me.CHART_A.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_A.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.X.LineThickness = 1
        Me.CHART_A.Axis.X.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_A.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.X.MajorGridLines.Visible = True
        Me.CHART_A.Axis.X.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_A.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.X.MinorGridLines.Visible = False
        Me.CHART_A.Axis.X.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_A.Axis.X.Visible = True
        Me.CHART_A.Axis.X2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_A.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_A.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.CHART_A.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.X2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.FormatString = ""
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_A.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.X2.Labels.Visible = False
        Me.CHART_A.Axis.X2.LineThickness = 1
        Me.CHART_A.Axis.X2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_A.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.X2.MajorGridLines.Visible = True
        Me.CHART_A.Axis.X2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_A.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.X2.MinorGridLines.Visible = False
        Me.CHART_A.Axis.X2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_A.Axis.X2.Visible = False
        Me.CHART_A.Axis.Y.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_A.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_A.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.CHART_A.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Y.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.FormatString = ""
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Y.LineThickness = 1
        Me.CHART_A.Axis.Y.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_A.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Y.MajorGridLines.Visible = True
        Me.CHART_A.Axis.Y.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_A.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Y.MinorGridLines.Visible = False
        Me.CHART_A.Axis.Y.TickmarkInterval = 500
        Me.CHART_A.Axis.Y.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_A.Axis.Y.Visible = True
        Me.CHART_A.Axis.Y2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_A.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.CHART_A.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.FormatString = ""
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Y2.Labels.Visible = False
        Me.CHART_A.Axis.Y2.LineThickness = 1
        Me.CHART_A.Axis.Y2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_A.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Y2.MajorGridLines.Visible = True
        Me.CHART_A.Axis.Y2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_A.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Y2.MinorGridLines.Visible = False
        Me.CHART_A.Axis.Y2.TickmarkInterval = 500
        Me.CHART_A.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_A.Axis.Y2.Visible = False
        Me.CHART_A.Axis.Z.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_A.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.Z.Labels.ItemFormatString = ""
        Me.CHART_A.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Z.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Z.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_A.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Z.Labels.Visible = False
        Me.CHART_A.Axis.Z.LineThickness = 1
        Me.CHART_A.Axis.Z.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_A.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Z.MajorGridLines.Visible = True
        Me.CHART_A.Axis.Z.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_A.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Z.MinorGridLines.Visible = False
        Me.CHART_A.Axis.Z.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_A.Axis.Z.Visible = False
        Me.CHART_A.Axis.Z2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_A.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.Z2.Labels.ItemFormatString = ""
        Me.CHART_A.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Z2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_A.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_A.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_A.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_A.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_A.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_A.Axis.Z2.Labels.Visible = False
        Me.CHART_A.Axis.Z2.LineThickness = 1
        Me.CHART_A.Axis.Z2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_A.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Z2.MajorGridLines.Visible = True
        Me.CHART_A.Axis.Z2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_A.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_A.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_A.Axis.Z2.MinorGridLines.Visible = False
        Me.CHART_A.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_A.Axis.Z2.Visible = False
        Me.CHART_A.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CHART_A.ColorModel.AlphaLevel = CType(150, Byte)
        Me.CHART_A.ColorModel.ModelStyle = Infragistics.UltraChart.[Shared].Styles.ColorModels.CustomSkin
        Me.CHART_A.Data.SwapRowsAndColumns = True
        Me.CHART_A.Data.ZeroAligned = True
        Me.CHART_A.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CHART_A.Effects.Effects.Add(GradientEffect1)
        Me.CHART_A.Legend.Location = Infragistics.UltraChart.[Shared].Styles.LegendLocation.Bottom
        Me.CHART_A.Legend.Visible = True
        Me.CHART_A.Location = New System.Drawing.Point(0, 0)
        Me.CHART_A.Name = "CHART_A"
        Me.CHART_A.Size = New System.Drawing.Size(363, 163)
        Me.CHART_A.TabIndex = 0
        Me.CHART_A.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray
        Me.CHART_A.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray
        Me.CHART_A.Tooltips.TooltipControl = Nothing
        '
        '			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
        '			'ChartType' must be persisted ahead of any Axes change made in design time.
        '		
        Me.CHART_B.ChartType = Infragistics.UltraChart.[Shared].Styles.ChartType.LineChart
        '
        'CHART_B
        '
        Me.CHART_B.Axis.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        PaintElement2.ElementType = Infragistics.UltraChart.[Shared].Styles.PaintElementType.None
        PaintElement2.Fill = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.CHART_B.Axis.PE = PaintElement2
        Me.CHART_B.Axis.X.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_B.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.CHART_B.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.X.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_B.Axis.X.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_B.Axis.X.Labels.SeriesLabels.FormatString = ""
        Me.CHART_B.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_B.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.X.LineThickness = 1
        Me.CHART_B.Axis.X.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_B.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.X.MajorGridLines.Visible = True
        Me.CHART_B.Axis.X.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_B.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.X.MinorGridLines.Visible = False
        Me.CHART_B.Axis.X.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_B.Axis.X.Visible = True
        Me.CHART_B.Axis.X2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_B.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_B.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.CHART_B.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.X2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.FormatString = ""
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_B.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.X2.Labels.Visible = False
        Me.CHART_B.Axis.X2.LineThickness = 1
        Me.CHART_B.Axis.X2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_B.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.X2.MajorGridLines.Visible = True
        Me.CHART_B.Axis.X2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_B.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.X2.MinorGridLines.Visible = False
        Me.CHART_B.Axis.X2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_B.Axis.X2.Visible = False
        Me.CHART_B.Axis.Y.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_B.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_B.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.CHART_B.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Y.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.FormatString = ""
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Y.LineThickness = 1
        Me.CHART_B.Axis.Y.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_B.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Y.MajorGridLines.Visible = True
        Me.CHART_B.Axis.Y.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_B.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Y.MinorGridLines.Visible = False
        Me.CHART_B.Axis.Y.TickmarkInterval = 100
        Me.CHART_B.Axis.Y.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_B.Axis.Y.Visible = True
        Me.CHART_B.Axis.Y2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_B.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.CHART_B.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.FormatString = ""
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Y2.Labels.Visible = False
        Me.CHART_B.Axis.Y2.LineThickness = 1
        Me.CHART_B.Axis.Y2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_B.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Y2.MajorGridLines.Visible = True
        Me.CHART_B.Axis.Y2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_B.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Y2.MinorGridLines.Visible = False
        Me.CHART_B.Axis.Y2.TickmarkInterval = 100
        Me.CHART_B.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_B.Axis.Y2.Visible = False
        Me.CHART_B.Axis.Z.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_B.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.Z.Labels.ItemFormatString = ""
        Me.CHART_B.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Z.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Z.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_B.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Z.Labels.Visible = False
        Me.CHART_B.Axis.Z.LineThickness = 1
        Me.CHART_B.Axis.Z.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_B.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Z.MajorGridLines.Visible = True
        Me.CHART_B.Axis.Z.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_B.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Z.MinorGridLines.Visible = False
        Me.CHART_B.Axis.Z.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_B.Axis.Z.Visible = False
        Me.CHART_B.Axis.Z2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_B.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.Z2.Labels.ItemFormatString = ""
        Me.CHART_B.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Z2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_B.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_B.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_B.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_B.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_B.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_B.Axis.Z2.Labels.Visible = False
        Me.CHART_B.Axis.Z2.LineThickness = 1
        Me.CHART_B.Axis.Z2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_B.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Z2.MajorGridLines.Visible = True
        Me.CHART_B.Axis.Z2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_B.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_B.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_B.Axis.Z2.MinorGridLines.Visible = False
        Me.CHART_B.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_B.Axis.Z2.Visible = False
        Me.CHART_B.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CHART_B.ColorModel.AlphaLevel = CType(150, Byte)
        Me.CHART_B.ColorModel.ModelStyle = Infragistics.UltraChart.[Shared].Styles.ColorModels.CustomSkin
        Me.CHART_B.Data.SwapRowsAndColumns = True
        Me.CHART_B.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CHART_B.Effects.Effects.Add(GradientEffect2)
        Me.CHART_B.Legend.Location = Infragistics.UltraChart.[Shared].Styles.LegendLocation.Bottom
        Me.CHART_B.Legend.Visible = True
        Me.CHART_B.Location = New System.Drawing.Point(0, 0)
        Me.CHART_B.Name = "CHART_B"
        Me.CHART_B.Size = New System.Drawing.Size(363, 214)
        Me.CHART_B.TabIndex = 0
        Me.CHART_B.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray
        Me.CHART_B.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray
        Me.CHART_B.Tooltips.TooltipControl = Nothing
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.CHART_C)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.chkDay)
        Me.SplitContainer2.Panel2.Controls.Add(Me.chkHour)
        Me.SplitContainer2.Panel2.Controls.Add(Me.chkMoment)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdRight)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdLeft)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel2.Controls.Add(Me.dtNewTo)
        Me.SplitContainer2.Panel2.Controls.Add(Me.dtNewFrom)
        Me.SplitContainer2.Panel2.Controls.Add(Me.optG)
        Me.SplitContainer2.Size = New System.Drawing.Size(369, 381)
        Me.SplitContainer2.SplitterDistance = 162
        Me.SplitContainer2.TabIndex = 0
        '
        '			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
        '			'ChartType' must be persisted ahead of any Axes change made in design time.
        '		
        Me.CHART_C.ChartType = Infragistics.UltraChart.[Shared].Styles.ChartType.LineChart
        '
        'CHART_C
        '
        Me.CHART_C.Axis.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        PaintElement3.ElementType = Infragistics.UltraChart.[Shared].Styles.PaintElementType.None
        PaintElement3.Fill = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.CHART_C.Axis.PE = PaintElement3
        Me.CHART_C.Axis.X.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_C.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.CHART_C.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.X.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_C.Axis.X.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_C.Axis.X.Labels.SeriesLabels.FormatString = ""
        Me.CHART_C.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_C.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.X.LineThickness = 1
        Me.CHART_C.Axis.X.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_C.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.X.MajorGridLines.Visible = True
        Me.CHART_C.Axis.X.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_C.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.X.MinorGridLines.Visible = False
        Me.CHART_C.Axis.X.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_C.Axis.X.Visible = True
        Me.CHART_C.Axis.X2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_C.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_C.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>"
        Me.CHART_C.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.X2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.FormatString = ""
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.VerticalLeftFacing
        Me.CHART_C.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.X2.Labels.Visible = False
        Me.CHART_C.Axis.X2.LineThickness = 1
        Me.CHART_C.Axis.X2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_C.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.X2.MajorGridLines.Visible = True
        Me.CHART_C.Axis.X2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_C.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.X2.MinorGridLines.Visible = False
        Me.CHART_C.Axis.X2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_C.Axis.X2.Visible = False
        Me.CHART_C.Axis.Y.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_C.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_C.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.CHART_C.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Y.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.FormatString = ""
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Y.LineThickness = 1
        Me.CHART_C.Axis.Y.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_C.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Y.MajorGridLines.Visible = True
        Me.CHART_C.Axis.Y.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_C.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Y.MinorGridLines.Visible = False
        Me.CHART_C.Axis.Y.TickmarkInterval = 400
        Me.CHART_C.Axis.Y.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_C.Axis.Y.Visible = True
        Me.CHART_C.Axis.Y2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_C.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>"
        Me.CHART_C.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.FormatString = ""
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Y2.Labels.Visible = False
        Me.CHART_C.Axis.Y2.LineThickness = 1
        Me.CHART_C.Axis.Y2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_C.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Y2.MajorGridLines.Visible = True
        Me.CHART_C.Axis.Y2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_C.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Y2.MinorGridLines.Visible = False
        Me.CHART_C.Axis.Y2.TickmarkInterval = 400
        Me.CHART_C.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_C.Axis.Y2.Visible = False
        Me.CHART_C.Axis.Z.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_C.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.Z.Labels.ItemFormatString = ""
        Me.CHART_C.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Z.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Z.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray
        Me.CHART_C.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Z.Labels.Visible = False
        Me.CHART_C.Axis.Z.LineThickness = 1
        Me.CHART_C.Axis.Z.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_C.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Z.MajorGridLines.Visible = True
        Me.CHART_C.Axis.Z.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_C.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Z.MinorGridLines.Visible = False
        Me.CHART_C.Axis.Z.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_C.Axis.Z.Visible = False
        Me.CHART_C.Axis.Z2.Labels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray
        Me.CHART_C.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.Z2.Labels.ItemFormatString = ""
        Me.CHART_C.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Z2.Labels.SeriesLabels.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.CHART_C.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray
        Me.CHART_C.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near
        Me.CHART_C.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.[Shared].Styles.AxisLabelLayoutBehaviors.[Auto]
        Me.CHART_C.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.[Shared].Styles.TextOrientation.Horizontal
        Me.CHART_C.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center
        Me.CHART_C.Axis.Z2.Labels.Visible = False
        Me.CHART_C.Axis.Z2.LineThickness = 1
        Me.CHART_C.Axis.Z2.MajorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro
        Me.CHART_C.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Z2.MajorGridLines.Visible = True
        Me.CHART_C.Axis.Z2.MinorGridLines.AlphaLevel = CType(255, Byte)
        Me.CHART_C.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray
        Me.CHART_C.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.[Shared].Styles.LineDrawStyle.Dot
        Me.CHART_C.Axis.Z2.MinorGridLines.Visible = False
        Me.CHART_C.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.[Shared].Styles.AxisTickStyle.Smart
        Me.CHART_C.Axis.Z2.Visible = False
        Me.CHART_C.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.CHART_C.ColorModel.AlphaLevel = CType(150, Byte)
        Me.CHART_C.ColorModel.ModelStyle = Infragistics.UltraChart.[Shared].Styles.ColorModels.CustomSkin
        Me.CHART_C.Data.SwapRowsAndColumns = True
        Me.CHART_C.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CHART_C.Effects.Effects.Add(GradientEffect3)
        Me.CHART_C.Legend.Location = Infragistics.UltraChart.[Shared].Styles.LegendLocation.Bottom
        Me.CHART_C.Legend.Visible = True
        Me.CHART_C.Location = New System.Drawing.Point(0, 0)
        Me.CHART_C.Name = "CHART_C"
        Me.CHART_C.Size = New System.Drawing.Size(369, 162)
        Me.CHART_C.TabIndex = 0
        Me.CHART_C.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray
        Me.CHART_C.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray
        Me.CHART_C.Tooltips.TooltipControl = Nothing
        '
        'chkDay
        '
        Me.chkDay.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkDay.Location = New System.Drawing.Point(248, 100)
        Me.chkDay.Name = "chkDay"
        Me.chkDay.Size = New System.Drawing.Size(88, 29)
        Me.chkDay.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkDay.TabIndex = 23
        Me.chkDay.Text = "Суточные"
        '
        'chkHour
        '
        Me.chkHour.Location = New System.Drawing.Point(248, 65)
        Me.chkHour.Name = "chkHour"
        Me.chkHour.Size = New System.Drawing.Size(88, 29)
        Me.chkHour.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkHour.TabIndex = 22
        Me.chkHour.Text = "Часовые"
        '
        'chkMoment
        '
        Me.chkMoment.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button3DOldStyle
        Me.chkMoment.Checked = True
        Me.chkMoment.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMoment.Location = New System.Drawing.Point(248, 30)
        Me.chkMoment.Name = "chkMoment"
        Me.chkMoment.Size = New System.Drawing.Size(88, 29)
        Me.chkMoment.Style = Infragistics.Win.EditCheckStyle.Button
        Me.chkMoment.TabIndex = 21
        Me.chkMoment.Text = "Мгновенные"
        '
        'cmdRight
        '
        Me.cmdRight.Location = New System.Drawing.Point(97, 98)
        Me.cmdRight.Name = "cmdRight"
        Me.cmdRight.Size = New System.Drawing.Size(46, 31)
        Me.cmdRight.TabIndex = 20
        Me.cmdRight.Text = ">>"
        Me.cmdRight.Visible = False
        '
        'cmdLeft
        '
        Me.cmdLeft.Location = New System.Drawing.Point(41, 98)
        Me.cmdLeft.Name = "cmdLeft"
        Me.cmdLeft.Size = New System.Drawing.Size(48, 31)
        Me.cmdLeft.TabIndex = 19
        Me.cmdLeft.Text = "<<"
        Me.cmdLeft.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "С"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "По"
        '
        'dtNewTo
        '
        Me.dtNewTo.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtNewTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtNewTo.Location = New System.Drawing.Point(41, 58)
        Me.dtNewTo.Name = "dtNewTo"
        Me.dtNewTo.Size = New System.Drawing.Size(163, 20)
        Me.dtNewTo.TabIndex = 16
        '
        'dtNewFrom
        '
        Me.dtNewFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss"
        Me.dtNewFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtNewFrom.Location = New System.Drawing.Point(41, 32)
        Me.dtNewFrom.Name = "dtNewFrom"
        Me.dtNewFrom.Size = New System.Drawing.Size(163, 20)
        Me.dtNewFrom.TabIndex = 15
        '
        'optG
        '
        Me.optG.CheckedIndex = 0
        ValueListItem1.DataValue = CType(8, Short)
        ValueListItem1.DisplayText = "8 ч."
        ValueListItem2.DataValue = CType(12, Short)
        ValueListItem2.DisplayText = "12 ч."
        ValueListItem4.CheckState = System.Windows.Forms.CheckState.Checked
        ValueListItem4.DataValue = CType(16, Short)
        ValueListItem4.DisplayText = "16 ч."
        ValueListItem5.DataValue = CType(24, Short)
        ValueListItem5.DisplayText = "24 ч."
        ValueListItem6.DataValue = CType(48, Short)
        ValueListItem6.DisplayText = "2 с."
        ValueListItem7.DataValue = CType(96, Short)
        ValueListItem7.DisplayText = "4 C."
        ValueListItem8.DataValue = CType(0, Short)
        ValueListItem8.DisplayText = "С-По"
        Me.optG.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem7, ValueListItem8})
        Me.optG.Location = New System.Drawing.Point(3, 3)
        Me.optG.Name = "optG"
        Me.optG.Size = New System.Drawing.Size(333, 23)
        Me.optG.TabIndex = 14
        Me.optG.Text = "8 ч."
        '
        'frmGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(736, 381)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmGraph"
        Me.Text = "Гафики"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.CHART_A, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHART_B, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.CHART_C, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Private WithEvents CHART_A As Infragistics.Win.UltraWinChart.UltraChart
    Private WithEvents CHART_B As Infragistics.Win.UltraWinChart.UltraChart
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Private WithEvents CHART_C As Infragistics.Win.UltraWinChart.UltraChart
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtNewTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtNewFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents optG As Infragistics.Win.UltraWinEditors.UltraOptionSet
    Friend WithEvents cmdRight As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmdLeft As Infragistics.Win.Misc.UltraButton
    Friend WithEvents chkDay As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkHour As Infragistics.Win.UltraWinEditors.UltraCheckEditor
    Friend WithEvents chkMoment As Infragistics.Win.UltraWinEditors.UltraCheckEditor
End Class
