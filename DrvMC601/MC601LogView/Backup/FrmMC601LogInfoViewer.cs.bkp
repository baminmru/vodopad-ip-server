// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmMC601LogInfoViewer
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using Kamstrup.Heat.mc601Communication;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmMC601LogInfoViewer : Form
  {
    private string m_CustomerNo = "";
    private IContainer components;
    private Button btnReadLog;
    private Button btnExportToExcel;
    private UltraGrid grdInfoLog;
    private SaveFileDialog saveFileDialog1;
    private UltraGridExcelExporter ultraGridExcelExporter1;

    public FrmMC601LogInfoViewer()
    {
      this.InitializeComponent();
    }

    private void ReadCustomerNo()
    {
      try
      {
        Functions functions = new Functions();
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add((object) 1010);
        arrRegisters.Add((object) 112);
        arrRegisters.Add((object) 157);
        string ErrorMessage = "";
        if (functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
          this.m_CustomerNo = Convert.ToString((double) arrValues[0] + (double) arrValues[1]);
        else
          this.m_CustomerNo = "";
      }
      catch
      {
        this.m_CustomerNo = "";
      }
    }

    private void btnExportToExcel_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.ultraGridExcelExporter1.Export(this.grdInfoLog, this.saveFileDialog1.FileName);
    }

    private void btnReadLog_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      this.ReadInfoLog();
      this.Cursor = Cursors.Default;
    }

    private void ReadInfoLog()
    {
      string ErrorMessage = "";
      byte UnitId = (byte) 0;
      ushort num1 = (ushort) 1;
      Functions functions = new Functions();
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      DataTable dataTable = new DataSet("InfoLogDataSet").Tables.Add("AllValues");
      dataTable.Columns.Add(new DataColumn()
      {
        DataType = typeof (int),
        ColumnName = "Id"
      });
      dataTable.Columns.Add("Date", typeof (DateTime));
      dataTable.Columns.Add("Info Code", typeof (double));
      dataTable.Columns.Add("Info", typeof (string));
      ArrayList arrValues1 = new ArrayList();
      ArrayList arrValues2 = new ArrayList();
      bool flag = true;
      this.ReadCustomerNo();
      if (flag && functions.GetHistInfoData((byte) 63, (ushort) 1003, (ushort) 1, (byte) 16, arrValues1, out UnitId, out ErrorMessage) && functions.GetHistInfoData((byte) 63, (ushort) 99, (ushort) 1, (byte) 16, arrValues2, out UnitId, out ErrorMessage) && functions.GetHistInfoData((byte) 63, (ushort) 1003, (ushort) 17, (byte) 16, arrValues1, out UnitId, out ErrorMessage) && functions.GetHistInfoData((byte) 63, (ushort) 99, (ushort) 17, (byte) 16, arrValues2, out UnitId, out ErrorMessage) && functions.GetHistInfoData((byte) 63, (ushort) 1003, (ushort) 33, (byte) 4, arrValues1, out UnitId, out ErrorMessage) && functions.GetHistInfoData((byte) 63, (ushort) 99, (ushort) 33, (byte) 4, arrValues2, out UnitId, out ErrorMessage))
      {
        for (byte index = (byte) 0; (int) index < arrValues2.Count; ++index)
        {
          DataRow row = dataTable.NewRow();
          row["Id"] = (object) ((int) num1 + (int) index);
          double num2 = (double) arrValues1[(int) index];
          int year = 2000 + Convert.ToInt32(num2) / 10000;
          int month = Convert.ToInt32(num2) % 10000 / 100;
          int day = Convert.ToInt32(num2) % 100;
          DateTime dateTime;
          try
          {
            dateTime = new DateTime(year, month, day);
          }
          catch
          {
            dateTime = new DateTime(2000, 1, 1);
          }
          row["Date"] = (object) dateTime;
          double infoCode = (double) arrValues2[(int) index];
          row["Info Code"] = (object) infoCode;
          row["Info"] = (object) ClsUtils.InfoCodeToString(infoCode);
          dataTable.Rows.Add(row);
        }
        this.grdInfoLog.DataSource = (object) dataTable;
        this.grdInfoLog.DataBind();
        this.grdInfoLog.DisplayLayout.Bands[0].Columns["Id"].Hidden = true;
        this.grdInfoLog.DisplayLayout.Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
        if (dataTable.Rows.Count <= 0)
          return;
        DataTable graphData = dataTable.Clone();
        foreach (DataRow dataRow in (InternalDataCollectionBase) dataTable.Rows)
        {
          DataRow row = graphData.NewRow();
          row["Date"] = dataRow["Date"];
          row["Info Code"] = dataRow["Info Code"];
          graphData.Rows.Add(row);
        }
        graphData.Columns.Remove("Id");
        graphData.Columns.Remove("Info");
        FrmGraph frmGraph = new FrmGraph(this.m_CustomerNo);
        frmGraph.MdiParent = this.MdiParent;
        frmGraph.ShowTime = true;
        frmGraph.SetData(graphData);
        frmGraph.Show();
      }
      else
      {
        int num3 = (int) MessageBox.Show("Lost the connection to the MC601 Meter!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void grdInfoLog_InitializeLayout(object sender, InitializeLayoutEventArgs e)
    {
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMC601LogInfoViewer));
      this.btnReadLog = new Button();
      this.btnExportToExcel = new Button();
      this.grdInfoLog = new UltraGrid();
      this.saveFileDialog1 = new SaveFileDialog();
      this.ultraGridExcelExporter1 = new UltraGridExcelExporter(this.components);
      this.grdInfoLog.BeginInit();
      this.SuspendLayout();
      this.btnReadLog.Location = new Point(13, 12);
      this.btnReadLog.Name = "btnReadLog";
      this.btnReadLog.Size = new Size(75, 23);
      this.btnReadLog.TabIndex = 0;
      this.btnReadLog.Text = "Read Log";
      this.btnReadLog.UseVisualStyleBackColor = true;
      this.btnReadLog.Click += new EventHandler(this.btnReadLog_Click);
      this.btnExportToExcel.Location = new Point(94, 12);
      this.btnExportToExcel.Name = "btnExportToExcel";
      this.btnExportToExcel.Size = new Size(97, 23);
      this.btnExportToExcel.TabIndex = 2;
      this.btnExportToExcel.Text = "Export to Excel";
      this.btnExportToExcel.UseVisualStyleBackColor = true;
      this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
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
      this.grdInfoLog.Location = new Point(0, 41);
      this.grdInfoLog.Name = "grdInfoLog";
      this.grdInfoLog.Size = new Size(665, 439);
      this.grdInfoLog.TabIndex = 3;
      this.grdInfoLog.Text = "grdInfoLog";
      this.grdInfoLog.InitializeLayout += new InitializeLayoutEventHandler(this.grdInfoLog_InitializeLayout);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(662, 479);
      this.Controls.Add((Control) this.grdInfoLog);
      this.Controls.Add((Control) this.btnExportToExcel);
      this.Controls.Add((Control) this.btnReadLog);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "FrmMC601LogInfoViewer";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "MC601 Log Info Viewer";
      this.grdInfoLog.EndInit();
      this.ResumeLayout(false);
    }
  }
}
