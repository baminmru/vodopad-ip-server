// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmKMPLoggerShowLog
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe


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
    private DataGridView grdInfoLog;
    private Button btnExportToExcel;
    private SaveFileDialog saveFileDialog1;
   

    public FrmKMPLoggerShowLog()
    {
      this.InitializeComponent();
    }

    private void btnExportToExcel_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      //this.DataGridViewExcelExporter1.Export(this.grdInfoLog, this.saveFileDialog1.FileName);
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
      //this.grdInfoLog.DataBind();
      //this.grdInfoLog.DisplayLayout.Bands[0].Columns["Id"].Hidden = true;
      //this.grdInfoLog.DisplayLayout.Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
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
     
      this.grdInfoLog = new DataGridView();
      this.btnExportToExcel = new Button();
      this.saveFileDialog1 = new SaveFileDialog();
      //this.grdInfoLog.BeginInit();
      this.SuspendLayout();
      this.grdInfoLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    
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
