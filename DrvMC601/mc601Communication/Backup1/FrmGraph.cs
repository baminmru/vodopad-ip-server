// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmGraph
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Infragistics.UltraChart.Shared.Events;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.Win.Misc;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinChart;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmGraph : Form
  {
    private bool m_ShowTime;
    private IContainer components;
    private UltraChart graph;
    private UltraButton btnPrint;
    private UltraPrintPreviewDialog ultraPrintPreviewDialog1;

    public bool ShowTime
    {
      get
      {
        return this.m_ShowTime;
      }
      set
      {
        this.m_ShowTime = value;
      }
    }

    public FrmGraph(string serialNo)
    {
      this.InitializeComponent();
      this.Text = "Graph for Serial No " + serialNo + ".";
    }

    public void SetData(DataTable graphData)
    {
      this.graph.DataSource = (object) graphData;
      this.graph.DataBind();
    }

    private void graph_DataItemOver(object sender, ChartDataEventArgs e)
    {
      if (this.ShowTime)
        this.graph.Tooltips.FormatString = e.RowLabel + (object) "\r\n" + e.ColumnLabel + "\r\n" + (string) (object) e.DataValue;
      else
        this.graph.Tooltips.FormatString = e.RowLabel + (object) "\r\n" + e.ColumnLabel.Substring(0, 10) + "\r\n" + (string) (object) e.DataValue;
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      this.ultraPrintPreviewDialog1.Document = this.graph.PrintDocument;
      int num = (int) this.ultraPrintPreviewDialog1.ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmGraph));
      this.graph = new UltraChart();
      this.btnPrint = new UltraButton();
      this.ultraPrintPreviewDialog1 = new UltraPrintPreviewDialog(this.components);
      this.graph.BeginInit();
      this.SuspendLayout();
      this.graph.Axis.BackColor = Color.FromArgb((int) byte.MaxValue, 248, 220);
      this.graph.Axis.X.Extent = 85;
      this.graph.Axis.X.Labels.Flip = true;
      this.graph.Axis.X.Labels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
      this.graph.Axis.X.Labels.Orientation = TextOrientation.Custom;
      this.graph.Axis.X.Labels.OrientationAngle = 230;
      this.graph.Axis.X.Labels.SeriesLabels.FormatString = "";
      this.graph.Axis.X.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.X.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.X.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.X.Labels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.X.MajorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.X.MajorGridLines.Color = Color.Gainsboro;
      this.graph.Axis.X.MajorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.X.MajorGridLines.Visible = true;
      this.graph.Axis.X.MinorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.X.MinorGridLines.Color = Color.LightGray;
      this.graph.Axis.X.MinorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.X.MinorGridLines.Visible = false;
      this.graph.Axis.X.ScrollScale.Height = 5;
      this.graph.Axis.X.ScrollScale.Visible = true;
      this.graph.Axis.X.ScrollScale.Width = 10;
      this.graph.Axis.X.TickmarkInterval = 1.0;
      this.graph.Axis.X.TickmarkIntervalType = AxisIntervalType.Days;
      this.graph.Axis.X.TickmarkStyle = AxisTickStyle.Smart;
      this.graph.Axis.X.Visible = true;
      this.graph.Axis.X2.Labels.HorizontalAlign = StringAlignment.Far;
      this.graph.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
      this.graph.Axis.X2.Labels.Orientation = TextOrientation.VerticalLeftFacing;
      this.graph.Axis.X2.Labels.SeriesLabels.FormatString = "";
      this.graph.Axis.X2.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Far;
      this.graph.Axis.X2.Labels.SeriesLabels.Orientation = TextOrientation.VerticalLeftFacing;
      this.graph.Axis.X2.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.X2.Labels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.X2.MajorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.X2.MajorGridLines.Color = Color.Gainsboro;
      this.graph.Axis.X2.MajorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.X2.MajorGridLines.Visible = true;
      this.graph.Axis.X2.MinorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.X2.MinorGridLines.Color = Color.LightGray;
      this.graph.Axis.X2.MinorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.X2.MinorGridLines.Visible = false;
      this.graph.Axis.X2.Visible = false;
      this.graph.Axis.Y.Extent = 60;
      this.graph.Axis.Y.Labels.HorizontalAlign = StringAlignment.Far;
      this.graph.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
      this.graph.Axis.Y.Labels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Y.Labels.SeriesLabels.FormatString = "";
      this.graph.Axis.Y.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Far;
      this.graph.Axis.Y.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Y.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Y.Labels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Y.MajorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Y.MajorGridLines.Color = Color.Gainsboro;
      this.graph.Axis.Y.MajorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Y.MajorGridLines.Visible = true;
      this.graph.Axis.Y.MinorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Y.MinorGridLines.Color = Color.LightGray;
      this.graph.Axis.Y.MinorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Y.MinorGridLines.Visible = false;
      this.graph.Axis.Y.RangeMax = 200.0;
      this.graph.Axis.Y.ScrollScale.Height = 5;
      this.graph.Axis.Y.ScrollScale.Visible = true;
      this.graph.Axis.Y.ScrollScale.Width = 10;
      this.graph.Axis.Y.TickmarkInterval = 40.0;
      this.graph.Axis.Y.TickmarkStyle = AxisTickStyle.Smart;
      this.graph.Axis.Y.Visible = true;
      this.graph.Axis.Y2.Labels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
      this.graph.Axis.Y2.Labels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Y2.Labels.SeriesLabels.FormatString = "";
      this.graph.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.Y2.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Y2.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Y2.Labels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Y2.MajorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Y2.MajorGridLines.Color = Color.Gainsboro;
      this.graph.Axis.Y2.MajorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Y2.MajorGridLines.Visible = true;
      this.graph.Axis.Y2.MinorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Y2.MinorGridLines.Color = Color.LightGray;
      this.graph.Axis.Y2.MinorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Y2.MinorGridLines.Visible = false;
      this.graph.Axis.Y2.Visible = false;
      this.graph.Axis.Z.Labels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.Z.Labels.ItemFormatString = "";
      this.graph.Axis.Z.Labels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Z.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.Z.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Z.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Z.Labels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Z.MajorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Z.MajorGridLines.Color = Color.Gainsboro;
      this.graph.Axis.Z.MajorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Z.MajorGridLines.Visible = true;
      this.graph.Axis.Z.MinorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Z.MinorGridLines.Color = Color.LightGray;
      this.graph.Axis.Z.MinorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Z.MinorGridLines.Visible = false;
      this.graph.Axis.Z.Visible = false;
      this.graph.Axis.Z2.Labels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.Z2.Labels.ItemFormatString = "";
      this.graph.Axis.Z2.Labels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = StringAlignment.Near;
      this.graph.Axis.Z2.Labels.SeriesLabels.Orientation = TextOrientation.Horizontal;
      this.graph.Axis.Z2.Labels.SeriesLabels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Z2.Labels.VerticalAlign = StringAlignment.Center;
      this.graph.Axis.Z2.MajorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Z2.MajorGridLines.Color = Color.Gainsboro;
      this.graph.Axis.Z2.MajorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Z2.MajorGridLines.Visible = true;
      this.graph.Axis.Z2.MinorGridLines.AlphaLevel = byte.MaxValue;
      this.graph.Axis.Z2.MinorGridLines.Color = Color.LightGray;
      this.graph.Axis.Z2.MinorGridLines.DrawStyle = LineDrawStyle.Dot;
      this.graph.Axis.Z2.MinorGridLines.Visible = false;
      this.graph.Axis.Z2.Visible = false;
      this.graph.Border.CornerRadius = 5;
      this.graph.ChartType = ChartType.LineChart;
      this.graph.ColorModel.AlphaLevel = (byte) 150;
      this.graph.Data.EmptyStyle.LineStyle.DrawStyle = LineDrawStyle.Dash;
      this.graph.Data.SwapRowsAndColumns = true;
      this.graph.Data.UseRowLabelsColumn = true;
      this.graph.Dock = DockStyle.Fill;
      this.graph.EmptyChartText = "Data Not Available.";
      this.graph.ForeColor = SystemColors.ControlText;
      this.graph.Legend.Location = LegendLocation.Bottom;
      this.graph.Legend.Margins.Bottom = 0;
      this.graph.Legend.Margins.Left = 0;
      this.graph.Legend.Margins.Right = 0;
      this.graph.Legend.Margins.Top = 0;
      this.graph.Legend.SpanPercentage = 22;
      this.graph.Legend.Visible = true;
      this.graph.Location = new Point(0, 0);
      this.graph.Name = "graph";
      this.graph.Size = new Size(485, 384);
      this.graph.TabIndex = 0;
      this.graph.TitleBottom.Visible = false;
      this.graph.TitleTop.Visible = false;
      this.graph.Tooltips.Font = new Font("Microsoft Sans Serif", 7.8f);
      this.graph.Tooltips.FormatString = "";
      this.graph.DataItemOver += new DataItemOverEventHandler(this.graph_DataItemOver);
      this.btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnPrint.Location = new Point(435, 271);
      this.btnPrint.Name = "btnPrint";
      this.btnPrint.Size = new Size(38, 23);
      this.btnPrint.TabIndex = 3;
      this.btnPrint.Text = "Print";
      this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
      this.ultraPrintPreviewDialog1.Name = "ultraPrintPreviewDialog1";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(485, 384);
      this.Controls.Add((Control) this.btnPrint);
      this.Controls.Add((Control) this.graph);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FrmGraph";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Graph";
      this.graph.EndInit();
      this.ResumeLayout(false);
    }
  }
}
