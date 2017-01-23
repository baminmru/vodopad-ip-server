// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmKMPLoggerShowLog
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmKMPLoggerShowLog : Form
  {
    public KMPLogDataSet KMPDS;
    private IContainer components;
    private UltraGrid grdInfoLog;
    private Button btnExportToExcel;
    private SaveFileDialog saveFileDialog1;
    private UltraGridExcelExporter ultraGridExcelExporter1;

    public FrmKMPLoggerShowLog()
    {
      this.InitializeComponent();
    }

    private void btnExportToExcel_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.ultraGridExcelExporter1.Export(this.grdInfoLog, this.saveFileDialog1.FileName);
    }

    internal void DisplayData()
    {
      DataTable dataTable = new DataSet("LogDataSet").Tables.Add("AllValues");
      KMPLogDataSet.RegisterInUseRow registerInUseRow = this.KMPDS.RegisterInUse[0];
      dataTable.Columns.Add(new DataColumn()
      {
        DataType = typeof (int),
        ColumnName = "Id"
      });
      dataTable.Columns.Add("TimeStamp", typeof (DateTime));
      if (registerInUseRow.INFO)
      {
        dataTable.Columns.Add("Info Code", typeof (double));
        dataTable.Columns.Add("Info", typeof (string));
      }
      if (registerInUseRow.LogQOS)
      {
        dataTable.Columns.Add("LogQOS Code", typeof (double));
        dataTable.Columns.Add("LogQOS", typeof (string));
      }
      foreach (KMPLogDataSet.RegistersRow registersRow in this.KMPDS.Registers)
      {
        DataRow row = dataTable.NewRow();
        row["Id"] =  registersRow.Id;
        row["TimeStamp"] =  registersRow.Date;
        if (registerInUseRow.INFO)
        {
          row["Info Code"] =  registersRow.INFO;
          row["Info"] =  ClsUtils.InfoCodeToString(Convert.ToDouble(registersRow.INFO));
        }
        if (registerInUseRow.INFO)
        {
          row["LogQOS Code"] =  registersRow.LogQOS;
          row["LogQOS"] =  ClsUtils.LogQOSCodeToString(registersRow.LogQOS);
        }
        dataTable.Rows.Add(row);
      }
      this.grdInfoLog.DataSource =  dataTable;
      this.grdInfoLog.DataBind();
      this.grdInfoLog.DisplayLayout.Bands[0].Columns["Id"].Hidden = true;
      this.grdInfoLog.DisplayLayout.Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
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
      this.grdInfoLog = new UltraGrid();
      this.btnExportToExcel = new Button();
      this.saveFileDialog1 = new SaveFileDialog();
      this.ultraGridExcelExporter1 = new UltraGridExcelExporter(this.components);
      //this.grdInfoLog.BeginInit();
      this.SuspendLayout();
      this.grdInfoLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      appearance1.BackColor = SystemColors.Window;
      appearance1.BorderColor = SystemColors.InactiveCaption;
      this.grdInfoLog.DisplayLayout.Appearance = (AppearanceBase) appearance1;
      this.grdInfoLog.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
      this.grdInfoLog.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
      this.grdInfoLog.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
      appearance2.BackColor = SystemColors.ActiveBorder;
      appearance2.BackColor2 = SystemColors.ControlDark;
      appearance2.BackGradientStyle = GradientStyle.Vertical;
      appearance2.BorderColor = SystemColors.Window;
      this.grdInfoLog.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
      appearance3.ForeColor = SystemColors.GrayText;
      this.grdInfoLog.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
      this.grdInfoLog.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
      appearance4.BackColor = SystemColors.ControlLightLight;
      appearance4.BackColor2 = SystemColors.Control;
      appearance4.BackGradientStyle = GradientStyle.Horizontal;
      appearance4.ForeColor = SystemColors.GrayText;
      this.grdInfoLog.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
      this.grdInfoLog.DisplayLayout.MaxColScrollRegions = 1;
      this.grdInfoLog.DisplayLayout.MaxRowScrollRegions = 1;
      appearance5.BackColor = SystemColors.Window;
      appearance5.ForeColor = SystemColors.ControlText;
      this.grdInfoLog.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
      appearance6.BackColor = SystemColors.Highlight;
      appearance6.ForeColor = SystemColors.HighlightText;
      this.grdInfoLog.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
      this.grdInfoLog.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
      this.grdInfoLog.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
      appearance7.BackColor = SystemColors.Window;
      this.grdInfoLog.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
      appearance8.BorderColor = Color.Silver;
      appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
      this.grdInfoLog.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
      this.grdInfoLog.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
      this.grdInfoLog.DisplayLayout.Override.CellPadding = 0;
      appearance9.BackColor = SystemColors.Control;
      appearance9.BackColor2 = SystemColors.ControlDark;
      appearance9.BackGradientAlignment = GradientAlignment.Element;
      appearance9.BackGradientStyle = GradientStyle.Horizontal;
      appearance9.BorderColor = SystemColors.Window;
      this.grdInfoLog.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
      appearance10.TextHAlignAsString = "Left";
      this.grdInfoLog.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
      this.grdInfoLog.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
      this.grdInfoLog.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
      appearance11.BackColor = SystemColors.Window;
      appearance11.BorderColor = Color.Silver;
      this.grdInfoLog.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
      this.grdInfoLog.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
      appearance12.BackColor = SystemColors.ControlLight;
      this.grdInfoLog.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
      this.grdInfoLog.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
      this.grdInfoLog.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
      this.grdInfoLog.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
      this.grdInfoLog.Location = new Point(0, 40);
      this.grdInfoLog.Name = "grdInfoLog";
      this.grdInfoLog.Size = new Size(804, 469);
      this.grdInfoLog.TabIndex = 6;
      this.grdInfoLog.Text = "grdInfoLog";
      this.btnExportToExcel.Location = new Point(12, 11);
      this.btnExportToExcel.Name = "btnExportToExcel";
      this.btnExportToExcel.Size = new Size(97, 23);
      this.btnExportToExcel.TabIndex = 5;
      this.btnExportToExcel.Text = "Export to Excel";
      this.btnExportToExcel.UseVisualStyleBackColor = true;
      this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(803, 508);
      this.Controls.Add((Control) this.grdInfoLog);
      this.Controls.Add((Control) this.btnExportToExcel);
      this.Name = "FrmKMPLoggerShowLog";
      this.Text = "KMP Logger Show Log";
      //this.grdInfoLog.EndInit();
      this.ResumeLayout(false);
    }
  }
}
