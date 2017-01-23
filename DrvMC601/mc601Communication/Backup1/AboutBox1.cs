// Decompiled with JetBrains decompiler
// Type: MC601LogView.AboutBox1
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Kamstrup.Heat.mc601Communication;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MC601LogView
{
  internal class AboutBox1 : Form
  {
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel;
    private PictureBox logoPictureBox;
    private Label labelProductName;
    private Label labelVersion;
    private Label labelCopyright;
    private Label labelCompanyName;
    private Button okButton;
    private DataGridView gridAssemblies;

    public static string AssemblyTitle
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
        if (customAttributes.Length > 0)
        {
          AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute) customAttributes[0];
          if (assemblyTitleAttribute.Title.Length > 0)
            return assemblyTitleAttribute.Title;
        }
        return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public static string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public static string AssemblyDescription
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
        if (customAttributes.Length == 0)
          return "";
        return ((AssemblyDescriptionAttribute) customAttributes[0]).Description;
      }
    }

    public static string AssemblyProduct
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
        if (customAttributes.Length == 0)
          return "";
        return ((AssemblyProductAttribute) customAttributes[0]).Product;
      }
    }

    public static string AssemblyCopyright
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
        if (customAttributes.Length == 0)
          return "";
        return ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
      }
    }

    public static string AssemblyCompany
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
        if (customAttributes.Length == 0)
          return "";
        return ((AssemblyCompanyAttribute) customAttributes[0]).Company;
      }
    }

    public AboutBox1()
    {
      this.InitializeComponent();
      this.Text = string.Format("About {0}", (object) AboutBox1.AssemblyTitle);
      this.labelProductName.Text = AboutBox1.AssemblyProduct;
      this.labelVersion.Text = string.Format("Version {0}", (object) AboutBox1.AssemblyVersion);
      this.labelCopyright.Text = AboutBox1.AssemblyCopyright;
      this.labelCompanyName.Text = AboutBox1.AssemblyCompany;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void AboutBox1_Load(object sender, EventArgs e)
    {
      Functions functions = new Functions();
      DataTable dataTable = new DataTable();
      DataColumn column1 = new DataColumn("Name");
      DataColumn column2 = new DataColumn("Title");
      dataTable.Columns.Add(column1);
      dataTable.Columns.Add(column2);
      foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
      {
        if (asm.GetName().Name.LastIndexOf("System") < 0 && asm.GetName().Name.LastIndexOf("Microsoft") < 0 && (asm.GetName().Name.LastIndexOf("Infragistics2") < 0 && asm.GetName().Name.LastIndexOf("vshost") < 0) && (asm.GetName().Name.LastIndexOf("Accessibility") < 0 && asm.GetName().Name.LastIndexOf("mscorlib") < 0))
        {
          DataRow row = dataTable.NewRow();
          row[0] = (object) asm.GetName().Name;
          row[1] = (object) AboutBox1.ShowAssemblyProduct(asm);
          dataTable.Rows.Add(row);
        }
      }
      this.gridAssemblies.DataSource = (object) dataTable;
      this.gridAssemblies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    }

    public static string ShowAssemblyProduct(Assembly asm)
    {
      object[] customAttributes = asm.GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
      if (customAttributes.Length == 0)
        return "";
      return ((AssemblyTitleAttribute) customAttributes[0]).Title;
    }

    private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AboutBox1));
      this.tableLayoutPanel = new TableLayoutPanel();
      this.logoPictureBox = new PictureBox();
      this.labelProductName = new Label();
      this.labelVersion = new Label();
      this.labelCopyright = new Label();
      this.labelCompanyName = new Label();
      this.okButton = new Button();
      this.gridAssemblies = new DataGridView();
      this.tableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.logoPictureBox).BeginInit();
      ((ISupportInitialize) this.gridAssemblies).BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel.ColumnCount = 2;
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.12568f));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.87431f));
      this.tableLayoutPanel.Controls.Add((Control) this.logoPictureBox, 0, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.labelProductName, 1, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.labelVersion, 1, 1);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCopyright, 1, 2);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCompanyName, 1, 3);
      this.tableLayoutPanel.Controls.Add((Control) this.okButton, 1, 5);
      this.tableLayoutPanel.Controls.Add((Control) this.gridAssemblies, 1, 4);
      this.tableLayoutPanel.Dock = DockStyle.Fill;
      this.tableLayoutPanel.Location = new Point(9, 9);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 6;
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 4.607046f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 4.065041f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 4.065041f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5.149052f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 71.81572f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel.Size = new Size(549, 331);
      this.tableLayoutPanel.TabIndex = 0;
      this.tableLayoutPanel.Paint += new PaintEventHandler(this.tableLayoutPanel_Paint);
      this.logoPictureBox.Dock = DockStyle.Fill;
      this.logoPictureBox.Image = (Image) componentResourceManager.GetObject("logoPictureBox.Image");
      this.logoPictureBox.Location = new Point(3, 3);
      this.logoPictureBox.Name = "logoPictureBox";
      this.tableLayoutPanel.SetRowSpan((Control) this.logoPictureBox, 6);
      this.logoPictureBox.Size = new Size(98, 325);
      this.logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
      this.logoPictureBox.TabIndex = 12;
      this.logoPictureBox.TabStop = false;
      this.labelProductName.Dock = DockStyle.Fill;
      this.labelProductName.Location = new Point(110, 0);
      this.labelProductName.Margin = new Padding(6, 0, 3, 0);
      this.labelProductName.MaximumSize = new Size(0, 17);
      this.labelProductName.Name = "labelProductName";
      this.labelProductName.Size = new Size(436, 15);
      this.labelProductName.TabIndex = 19;
      this.labelProductName.Text = "Product Name";
      this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
      this.labelVersion.Dock = DockStyle.Fill;
      this.labelVersion.Location = new Point(110, 15);
      this.labelVersion.Margin = new Padding(6, 0, 3, 0);
      this.labelVersion.MaximumSize = new Size(0, 17);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new Size(436, 13);
      this.labelVersion.TabIndex = 0;
      this.labelVersion.Text = "Version";
      this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
      this.labelCopyright.Dock = DockStyle.Fill;
      this.labelCopyright.Location = new Point(110, 28);
      this.labelCopyright.Margin = new Padding(6, 0, 3, 0);
      this.labelCopyright.MaximumSize = new Size(0, 17);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new Size(436, 13);
      this.labelCopyright.TabIndex = 21;
      this.labelCopyright.Text = "Copyright";
      this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
      this.labelCompanyName.Dock = DockStyle.Fill;
      this.labelCompanyName.Location = new Point(110, 41);
      this.labelCompanyName.Margin = new Padding(6, 0, 3, 0);
      this.labelCompanyName.MaximumSize = new Size(0, 17);
      this.labelCompanyName.Name = "labelCompanyName";
      this.labelCompanyName.Size = new Size(436, 17);
      this.labelCompanyName.TabIndex = 22;
      this.labelCompanyName.Text = "Company Name";
      this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
      this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.okButton.DialogResult = DialogResult.Cancel;
      this.okButton.Location = new Point(471, 305);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(75, 23);
      this.okButton.TabIndex = 24;
      this.okButton.Text = "&OK";
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.gridAssemblies.AllowUserToAddRows = false;
      this.gridAssemblies.AllowUserToDeleteRows = false;
      this.gridAssemblies.BackgroundColor = SystemColors.Control;
      this.gridAssemblies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gridAssemblies.Dock = DockStyle.Fill;
      this.gridAssemblies.Location = new Point(107, 61);
      this.gridAssemblies.Name = "gridAssemblies";
      this.gridAssemblies.ReadOnly = true;
      this.gridAssemblies.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
      this.gridAssemblies.Size = new Size(439, 232);
      this.gridAssemblies.TabIndex = 25;
      this.AcceptButton = (IButtonControl) this.okButton;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(567, 349);
      this.Controls.Add((Control) this.tableLayoutPanel);
      this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutBox1";
      this.Padding = new Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "AboutBox";
      this.Load += new EventHandler(this.AboutBox1_Load);
      this.tableLayoutPanel.ResumeLayout(false);
      ((ISupportInitialize) this.logoPictureBox).EndInit();
      ((ISupportInitialize) this.gridAssemblies).EndInit();
      this.ResumeLayout(false);
    }
  }
}
