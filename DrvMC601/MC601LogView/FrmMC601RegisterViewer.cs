// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmMC601RegisterViewer
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
  public class FrmMC601RegisterViewer : Form
  {
    private IContainer components;
    private DataGridView grdView;
    private Button btnExportToExcel;
    private SaveFileDialog saveFileDialog1;
    private Button btnPrint;
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
     
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMC601RegisterViewer));
      this.grdView = new DataGridView();
      this.btnExportToExcel = new Button();
     
      this.saveFileDialog1 = new SaveFileDialog();
      this.btnPrint = new Button();
     
      //this.grdView.BeginInit();
      this.SuspendLayout();
      this.grdView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
     
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
      //if (this.ShowTime)
      //{
      //  this.grdView.DisplayLayout.Bands[0].Columns[0].Format = "yyyy-MM-dd HH:mm";
      //  this.grdView.DisplayLayout.Bands[0].Columns[0].Width = 100;
      //}
      //else
      //  this.grdView.DisplayLayout.Bands[0].Columns[0].Format = "yyyy-MM-dd";
    }

    private void btnExportToExcel_Click(object sender, EventArgs e)
    {
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      //this.DataGridViewExcelExporter1.Export(this.grdView, this.saveFileDialog1.FileName);
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      //this.grdView.PrintPreview();
    }
  }
}
