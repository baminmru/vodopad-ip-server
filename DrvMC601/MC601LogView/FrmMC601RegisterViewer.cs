// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmMC601RegisterViewer
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Infragistics.Win;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmMC601RegisterViewer : Form
  {
    private IContainer components;
    private UltraGrid grdView;
    private Button btnExportToExcel;
    private UltraGridExcelExporter ultraGridExcelExporter1;
    private SaveFileDialog saveFileDialog1;
    private Button btnPrint;
    private UltraPrintPreviewDialog ultraPrintPreviewDialog1;
    private DataTable m_Mc601Register;
    private bool m_ShowTime;

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

    public FrmMC601RegisterViewer(DataTable mc601Register, string customerNo)
    {
      this.InitializeComponent();
      this.m_Mc601Register = mc601Register;
      this.Text = "MC601 Register Viewer for Serial No " + customerNo + ".";
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
      Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMC601RegisterViewer));
      this.grdView = new UltraGrid();
      this.btnExportToExcel = new Button();
      this.ultraGridExcelExporter1 = new UltraGridExcelExporter(this.components);
      this.saveFileDialog1 = new SaveFileDialog();
      this.btnPrint = new Button();
      this.ultraPrintPreviewDialog1 = new UltraPrintPreviewDialog(this.components);
      //this.grdView.BeginInit();
      this.SuspendLayout();
      this.grdView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      appearance1.BackColor = SystemColors.Window;
      appearance1.BorderColor = SystemColors.InactiveCaption;
      this.grdView.DisplayLayout.Appearance = (AppearanceBase) appearance1;
      this.grdView.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
      this.grdView.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
      appearance2.BackColor = SystemColors.ActiveBorder;
      appearance2.BackColor2 = SystemColors.ControlDark;
      appearance2.BackGradientStyle = GradientStyle.Vertical;
      appearance2.BorderColor = SystemColors.Window;
      this.grdView.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
      appearance3.ForeColor = SystemColors.GrayText;
      this.grdView.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
      this.grdView.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
      appearance4.BackColor = SystemColors.ControlLightLight;
      appearance4.BackColor2 = SystemColors.Control;
      appearance4.BackGradientStyle = GradientStyle.Horizontal;
      appearance4.ForeColor = SystemColors.GrayText;
      this.grdView.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
      this.grdView.DisplayLayout.MaxColScrollRegions = 1;
      this.grdView.DisplayLayout.MaxRowScrollRegions = 1;
      appearance5.BackColor = SystemColors.Window;
      appearance5.ForeColor = SystemColors.ControlText;
      this.grdView.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
      appearance6.BackColor = SystemColors.Highlight;
      appearance6.ForeColor = SystemColors.HighlightText;
      this.grdView.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
      this.grdView.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
      this.grdView.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
      this.grdView.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
      this.grdView.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
      appearance7.BackColor = SystemColors.Window;
      this.grdView.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
      appearance8.BorderColor = Color.Silver;
      appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
      this.grdView.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
      this.grdView.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
      this.grdView.DisplayLayout.Override.CellPadding = 0;
      appearance9.BackColor = SystemColors.Control;
      appearance9.BackColor2 = SystemColors.ControlDark;
      appearance9.BackGradientAlignment = GradientAlignment.Element;
      appearance9.BackGradientStyle = GradientStyle.Horizontal;
      appearance9.BorderColor = SystemColors.Window;
      this.grdView.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
      appearance10.TextHAlignAsString = "Left";
      this.grdView.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
      this.grdView.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
      this.grdView.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
      appearance11.BackColor = SystemColors.Window;
      appearance11.BorderColor = Color.Silver;
      this.grdView.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
      this.grdView.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
      appearance12.BackColor = SystemColors.ControlLight;
      this.grdView.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
      this.grdView.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
      this.grdView.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
      this.grdView.Location = new Point(0, 36);
      this.grdView.Name = "grdView";
      this.grdView.Size = new Size(819, 504);
      this.grdView.TabIndex = 0;
      this.grdView.Text = " ";
      this.btnExportToExcel.Location = new Point(12, 7);
      this.btnExportToExcel.Name = "btnExportToExcel";
      this.btnExportToExcel.Size = new Size(97, 23);
      this.btnExportToExcel.TabIndex = 1;
      this.btnExportToExcel.Text = "Export to Excel";
      this.btnExportToExcel.UseVisualStyleBackColor = true;
      this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
      this.saveFileDialog1.DefaultExt = "xls";
      this.saveFileDialog1.Filter = "Excel files|*.xls|All files|*.*";
      this.saveFileDialog1.Title = "Enter Excel file to save to.";
      this.btnPrint.Location = new Point(115, 7);
      this.btnPrint.Name = "btnPrint";
      this.btnPrint.Size = new Size(97, 23);
      this.btnPrint.TabIndex = 2;
      this.btnPrint.Text = "Print";
      this.btnPrint.UseVisualStyleBackColor = true;
      this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
      this.ultraPrintPreviewDialog1.Name = "ultraPrintPreviewDialog1";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(819, 540);
      this.Controls.Add((Control) this.btnPrint);
      this.Controls.Add((Control) this.btnExportToExcel);
      this.Controls.Add((Control) this.grdView);
     // this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FrmMC601RegisterViewer";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "MC601 Register Viewer";
      this.Load += new EventHandler(this.frmMC601RegisterViewer_Load);
      //this.grdView.EndInit();
      this.ResumeLayout(false);
    }

    private void frmMC601RegisterViewer_Load(object sender, EventArgs e)
    {
      this.grdView.DataSource =  this.m_Mc601Register;
      if (this.ShowTime)
      {
        this.grdView.DisplayLayout.Bands[0].Columns[0].Format = "yyyy-MM-dd HH:mm";
        this.grdView.DisplayLayout.Bands[0].Columns[0].Width = 100;
      }
      else
        this.grdView.DisplayLayout.Bands[0].Columns[0].Format = "yyyy-MM-dd";
    }

    private void btnExportToExcel_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.ultraGridExcelExporter1.Export(this.grdView, this.saveFileDialog1.FileName);
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      this.grdView.PrintPreview();
    }
  }
}
