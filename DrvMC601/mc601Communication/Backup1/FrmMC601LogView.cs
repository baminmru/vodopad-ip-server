// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmMC601LogView
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Kamstrup.Heat.mc601Communication;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace MC601LogView
{
  public class FrmMC601LogView : Form
  {
    private IContainer components;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem logsToolStripMenuItem;
    private ToolStripMenuItem windowToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem logSelecterPerMonthToolStripMenuItem;
    private ToolStripMenuItem logSelecterPerYearToolStripMenuItem;
    private ToolStripMenuItem infoDataToolStripMenuItem;
    private ToolStripMenuItem dayDSDataToolStripMenuItem;
    private ToolStripMenuItem liveLoggerToolStripMenuItem;
    private ToolStripMenuItem topModuleToolStripMenuItem;
    private ToolStripMenuItem hourLog6708MenuItem;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel statusSerialNo;
    private ToolStripStatusLabel statusTopModule;
    private ToolStripProgressBar statusProgressBar;
    private ToolStripStatusLabel statusRefresh;
    private ToolStripMenuItem hourWithDifferenceEnergyData6702MenuItem;
    private ToolStripMenuItem hourWithDifferenceVolumeData6709MenuItem;
    private ToolStripMenuItem contactToolStripMenuItem;
    private ToolStripMenuItem hourDataWithPQLimitterToolStripMenuItem;
    private ToolStripMenuItem hourDataWithKMPDataPortToolStripMenuItem;
    private ToolStripMenuItem bToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem kMPLoggerToolStripMenuItem;
    private ToolStripMenuItem quickFigureToolStripMenuItem;
    private ToolStripMenuItem quickFigureToolStripMenuItem1;

    public int ProgressBar
    {
      get
      {
        return this.statusProgressBar.Value;
      }
      set
      {
        this.statusProgressBar.Value = value;
      }
    }

    public int ProgressBarMax
    {
      get
      {
        return this.statusProgressBar.Maximum;
      }
      set
      {
        this.statusProgressBar.Maximum = value;
      }
    }

    public FrmMC601LogView()
    {
      this.InitializeComponent();
    }

    private void dayDSDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmDayLogger frmDayLogger = new FrmDayLogger();
      frmDayLogger.MdiParent = (Form) this;
      frmDayLogger.Show();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AboutBox1 aboutBox1 = new AboutBox1();
      aboutBox1.MdiParent = (Form) this;
      aboutBox1.Show();
    }

    private void infoDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmMC601LogInfoViewer mc601LogInfoViewer = new FrmMC601LogInfoViewer();
      mc601LogInfoViewer.MdiParent = (Form) this;
      mc601LogInfoViewer.Show();
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmSettings frmSettings = new FrmSettings();
      int num = (int) frmSettings.ShowDialog();
      frmSettings.Dispose();
    }

    private void logSelecterPerMonthToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmMonthLogger frmMonthLogger = new FrmMonthLogger();
      frmMonthLogger.MdiParent = (Form) this;
      frmMonthLogger.Show();
    }

    private void liveLoggerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmLiveLogger frmLiveLogger = new FrmLiveLogger();
      frmLiveLogger.MdiParent = (Form) this;
      frmLiveLogger.Show();
    }

    private void logSelecterPerYearToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmYearLogger frmYearLogger = new FrmYearLogger();
      frmYearLogger.MdiParent = (Form) this;
      frmYearLogger.Show();
    }

    private void topModuleMenuItem_Click(object sender, EventArgs e)
    {
      FrmHourLogSelecter frmHourLogSelecter = new FrmHourLogSelecter(eHourLogType._6708);
      frmHourLogSelecter.MdiParent = (Form) this;
      frmHourLogSelecter.Show();
    }

    private void frmMC601LogView_Load(object sender, EventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control.GetType() == typeof (MdiClient))
          control.BackColor = Color.FromArgb(238, 238, 238);
      }
    }

    private void SetupTopModuleData()
    {
      ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
      this.topModuleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) toolStripMenuItem
      });
      toolStripMenuItem.Name = "hourLogToolStripMenuItem";
      toolStripMenuItem.Size = new Size(152, 22);
      toolStripMenuItem.Text = "&Hourly Data";
      toolStripMenuItem.Click += new EventHandler(this.topModuleMenuItem_Click);
    }

    private void toolStripStatusLabel1_Click(object sender, EventArgs e)
    {
      try
      {
        this.statusSerialNo.Text = "Serial No: ";
        this.statusTopModule.Text = "Top Module: ";
        Functions functions = new Functions();
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add((object) 1010);
        arrRegisters.Add((object) 112);
        arrRegisters.Add((object) 157);
        string ErrorMessage = "";
        if (!functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
          return;
        this.statusSerialNo.Text = "Customer No: " + Convert.ToString((double) arrValues[0] + (double) arrValues[1]);
        this.statusTopModule.Text = "Top Module: " + Convert.ToString((double) arrValues[2]);
      }
      catch
      {
      }
    }

    private void hourWithDifferenceEnergyData6702MenuItem_Click(object sender, EventArgs e)
    {
      FrmHourLogSelecter frmHourLogSelecter = new FrmHourLogSelecter(eHourLogType._6702);
      frmHourLogSelecter.MdiParent = (Form) this;
      frmHourLogSelecter.Show();
    }

    private void hourLogToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmHourLogSelecter frmHourLogSelecter = new FrmHourLogSelecter(eHourLogType._6708);
      frmHourLogSelecter.MdiParent = (Form) this;
      frmHourLogSelecter.Show();
    }

    private void hourWithDifferenceVolumeData6709MenuItem_Click(object sender, EventArgs e)
    {
      FrmHourLogSelecter frmHourLogSelecter = new FrmHourLogSelecter(eHourLogType._6709);
      frmHourLogSelecter.MdiParent = (Form) this;
      frmHourLogSelecter.Show();
    }

    private void contactToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmContact frmContact = new FrmContact();
      frmContact.MdiParent = (Form) this;
      frmContact.Show();
    }

    private void hourDataWithPQLimitterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmHourLogSelecter frmHourLogSelecter = new FrmHourLogSelecter(eHourLogType._6703);
      frmHourLogSelecter.MdiParent = (Form) this;
      frmHourLogSelecter.Show();
    }

    private void hourDataWithKMPDataPortToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmHourLogSelecter frmHourLogSelecter = new FrmHourLogSelecter(eHourLogType._6705);
      frmHourLogSelecter.MdiParent = (Form) this;
      frmHourLogSelecter.Show();
    }

    private void bToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmKMPLogger frmKmpLogger = new FrmKMPLogger(eKMPLoggerType._670B);
      frmKmpLogger.MdiParent = (Form) this;
      frmKmpLogger.Show();
    }

    private void kMPLoggerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmKMPLogger frmKmpLogger = new FrmKMPLogger(eKMPLoggerType._670022);
      frmKmpLogger.MdiParent = (Form) this;
      frmKmpLogger.Show();
    }

    private void quickFigureToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      FrmQuickFigure frmQuickFigure = new FrmQuickFigure();
      frmQuickFigure.MdiParent = (Form) this;
      frmQuickFigure.Show();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMC601LogView));
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.settingsToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.logsToolStripMenuItem = new ToolStripMenuItem();
      this.liveLoggerToolStripMenuItem = new ToolStripMenuItem();
      this.dayDSDataToolStripMenuItem = new ToolStripMenuItem();
      this.logSelecterPerMonthToolStripMenuItem = new ToolStripMenuItem();
      this.logSelecterPerYearToolStripMenuItem = new ToolStripMenuItem();
      this.infoDataToolStripMenuItem = new ToolStripMenuItem();
      this.topModuleToolStripMenuItem = new ToolStripMenuItem();
      this.hourWithDifferenceEnergyData6702MenuItem = new ToolStripMenuItem();
      this.hourDataWithPQLimitterToolStripMenuItem = new ToolStripMenuItem();
      this.hourDataWithKMPDataPortToolStripMenuItem = new ToolStripMenuItem();
      this.hourLog6708MenuItem = new ToolStripMenuItem();
      this.hourWithDifferenceVolumeData6709MenuItem = new ToolStripMenuItem();
      this.bToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem1 = new ToolStripMenuItem();
      this.kMPLoggerToolStripMenuItem = new ToolStripMenuItem();
      this.windowToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.contactToolStripMenuItem = new ToolStripMenuItem();
      this.aboutToolStripMenuItem = new ToolStripMenuItem();
      this.quickFigureToolStripMenuItem = new ToolStripMenuItem();
      this.quickFigureToolStripMenuItem1 = new ToolStripMenuItem();
      this.statusStrip1 = new StatusStrip();
      this.statusProgressBar = new ToolStripProgressBar();
      this.statusSerialNo = new ToolStripStatusLabel();
      this.statusTopModule = new ToolStripStatusLabel();
      this.statusRefresh = new ToolStripStatusLabel();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      this.menuStrip1.Items.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.logsToolStripMenuItem,
        (ToolStripItem) this.topModuleToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.quickFigureToolStripMenuItem,
        (ToolStripItem) this.windowToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.MdiWindowListItem = this.windowToolStripMenuItem;
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(1016, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.settingsToolStripMenuItem,
        (ToolStripItem) this.exitToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(35, 20);
      this.fileToolStripMenuItem.Text = "&File";
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new Size(152, 22);
      this.settingsToolStripMenuItem.Text = "&Settings";
      this.settingsToolStripMenuItem.Click += new EventHandler(this.settingsToolStripMenuItem_Click);
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new Size(152, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.logsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.liveLoggerToolStripMenuItem,
        (ToolStripItem) this.dayDSDataToolStripMenuItem,
        (ToolStripItem) this.logSelecterPerMonthToolStripMenuItem,
        (ToolStripItem) this.logSelecterPerYearToolStripMenuItem,
        (ToolStripItem) this.infoDataToolStripMenuItem
      });
      this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
      this.logsToolStripMenuItem.Size = new Size(36, 20);
      this.logsToolStripMenuItem.Text = "&Log";
      this.liveLoggerToolStripMenuItem.Name = "liveLoggerToolStripMenuItem";
      this.liveLoggerToolStripMenuItem.Size = new Size(149, 22);
      this.liveLoggerToolStripMenuItem.Text = "&Interval Data";
      this.liveLoggerToolStripMenuItem.Click += new EventHandler(this.liveLoggerToolStripMenuItem_Click);
      this.dayDSDataToolStripMenuItem.Name = "dayDSDataToolStripMenuItem";
      this.dayDSDataToolStripMenuItem.Size = new Size(149, 22);
      this.dayDSDataToolStripMenuItem.Text = "&Daily Data";
      this.dayDSDataToolStripMenuItem.Click += new EventHandler(this.dayDSDataToolStripMenuItem_Click);
      this.logSelecterPerMonthToolStripMenuItem.Name = "logSelecterPerMonthToolStripMenuItem";
      this.logSelecterPerMonthToolStripMenuItem.Size = new Size(149, 22);
      this.logSelecterPerMonthToolStripMenuItem.Text = "&Monthly Data";
      this.logSelecterPerMonthToolStripMenuItem.Click += new EventHandler(this.logSelecterPerMonthToolStripMenuItem_Click);
      this.logSelecterPerYearToolStripMenuItem.Name = "logSelecterPerYearToolStripMenuItem";
      this.logSelecterPerYearToolStripMenuItem.Size = new Size(149, 22);
      this.logSelecterPerYearToolStripMenuItem.Text = "&Yearly Data";
      this.logSelecterPerYearToolStripMenuItem.Click += new EventHandler(this.logSelecterPerYearToolStripMenuItem_Click);
      this.infoDataToolStripMenuItem.Name = "infoDataToolStripMenuItem";
      this.infoDataToolStripMenuItem.Size = new Size(149, 22);
      this.infoDataToolStripMenuItem.Text = "&Info Data";
      this.infoDataToolStripMenuItem.Click += new EventHandler(this.infoDataToolStripMenuItem_Click);
      this.topModuleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.hourWithDifferenceEnergyData6702MenuItem,
        (ToolStripItem) this.hourDataWithPQLimitterToolStripMenuItem,
        (ToolStripItem) this.hourDataWithKMPDataPortToolStripMenuItem,
        (ToolStripItem) this.hourLog6708MenuItem,
        (ToolStripItem) this.hourWithDifferenceVolumeData6709MenuItem,
        (ToolStripItem) this.bToolStripMenuItem
      });
      this.topModuleToolStripMenuItem.Name = "topModuleToolStripMenuItem";
      this.topModuleToolStripMenuItem.Size = new Size(94, 20);
      this.topModuleToolStripMenuItem.Text = "&Top Module Log";
      this.hourWithDifferenceEnergyData6702MenuItem.Name = "hourWithDifferenceEnergyData6702MenuItem";
      this.hourWithDifferenceEnergyData6702MenuItem.Size = new Size(276, 22);
      this.hourWithDifferenceEnergyData6702MenuItem.Text = "6702 Hour with Difference &Energy Data";
      this.hourWithDifferenceEnergyData6702MenuItem.Click += new EventHandler(this.hourWithDifferenceEnergyData6702MenuItem_Click);
      this.hourDataWithPQLimitterToolStripMenuItem.Name = "hourDataWithPQLimitterToolStripMenuItem";
      this.hourDataWithPQLimitterToolStripMenuItem.Size = new Size(276, 22);
      this.hourDataWithPQLimitterToolStripMenuItem.Text = "6703 Hour data with PQ-Limitter";
      this.hourDataWithPQLimitterToolStripMenuItem.Click += new EventHandler(this.hourDataWithPQLimitterToolStripMenuItem_Click);
      this.hourDataWithKMPDataPortToolStripMenuItem.Name = "hourDataWithKMPDataPortToolStripMenuItem";
      this.hourDataWithKMPDataPortToolStripMenuItem.Size = new Size(276, 22);
      this.hourDataWithKMPDataPortToolStripMenuItem.Text = "6705 Hour data with KMP Data Port";
      this.hourDataWithKMPDataPortToolStripMenuItem.Click += new EventHandler(this.hourDataWithKMPDataPortToolStripMenuItem_Click);
      this.hourLog6708MenuItem.Name = "hourLog6708MenuItem";
      this.hourLog6708MenuItem.Size = new Size(276, 22);
      this.hourLog6708MenuItem.Text = "6708 &Hour Data";
      this.hourLog6708MenuItem.Click += new EventHandler(this.hourLogToolStripMenuItem_Click);
      this.hourWithDifferenceVolumeData6709MenuItem.Name = "hourWithDifferenceVolumeData6709MenuItem";
      this.hourWithDifferenceVolumeData6709MenuItem.Size = new Size(276, 22);
      this.hourWithDifferenceVolumeData6709MenuItem.Text = "6709 Hour With Difference &Volume Data";
      this.hourWithDifferenceVolumeData6709MenuItem.Click += new EventHandler(this.hourWithDifferenceVolumeData6709MenuItem_Click);
      this.bToolStripMenuItem.Name = "bToolStripMenuItem";
      this.bToolStripMenuItem.Size = new Size(276, 22);
      this.bToolStripMenuItem.Text = "670B KMP Logger";
      this.bToolStripMenuItem.Click += new EventHandler(this.bToolStripMenuItem_Click);
      this.toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.kMPLoggerToolStripMenuItem
      });
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(110, 20);
      this.toolStripMenuItem1.Text = "&Bottom Module Log";
      this.kMPLoggerToolStripMenuItem.Name = "kMPLoggerToolStripMenuItem";
      this.kMPLoggerToolStripMenuItem.Size = new Size(180, 22);
      this.kMPLoggerToolStripMenuItem.Text = "670022 KMP Logger";
      this.kMPLoggerToolStripMenuItem.Click += new EventHandler(this.kMPLoggerToolStripMenuItem_Click);
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      this.windowToolStripMenuItem.Size = new Size(57, 20);
      this.windowToolStripMenuItem.Text = "&Window";
      this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.contactToolStripMenuItem,
        (ToolStripItem) this.aboutToolStripMenuItem
      });
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(40, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
      this.contactToolStripMenuItem.Size = new Size(123, 22);
      this.contactToolStripMenuItem.Text = "Contact";
      this.contactToolStripMenuItem.Click += new EventHandler(this.contactToolStripMenuItem_Click);
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new Size(123, 22);
      this.aboutToolStripMenuItem.Text = "&About";
      this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
      this.quickFigureToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.quickFigureToolStripMenuItem1
      });
      this.quickFigureToolStripMenuItem.Name = "quickFigureToolStripMenuItem";
      this.quickFigureToolStripMenuItem.Size = new Size(78, 20);
      this.quickFigureToolStripMenuItem.Text = "Quick Figure";
      this.quickFigureToolStripMenuItem1.Name = "quickFigureToolStripMenuItem1";
      this.quickFigureToolStripMenuItem1.Size = new Size(152, 22);
      this.quickFigureToolStripMenuItem1.Text = "Quick figure";
      this.quickFigureToolStripMenuItem1.Click += new EventHandler(this.quickFigureToolStripMenuItem1_Click);
      this.statusStrip1.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.statusProgressBar,
        (ToolStripItem) this.statusSerialNo,
        (ToolStripItem) this.statusTopModule,
        (ToolStripItem) this.statusRefresh
      });
      this.statusStrip1.Location = new Point(0, 712);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(1016, 22);
      this.statusStrip1.TabIndex = 3;
      this.statusStrip1.Text = "statusStrip1";
      this.statusProgressBar.Name = "statusProgressBar";
      this.statusProgressBar.Size = new Size(100, 16);
      this.statusSerialNo.Name = "statusSerialNo";
      this.statusSerialNo.Size = new Size(49, 17);
      this.statusSerialNo.Text = "Serial No";
      this.statusTopModule.Name = "statusTopModule";
      this.statusTopModule.Size = new Size(62, 17);
      this.statusTopModule.Text = "Top Module";
      this.statusRefresh.BackColor = SystemColors.Control;
      this.statusRefresh.BorderSides = ToolStripStatusLabelBorderSides.All;
      this.statusRefresh.BorderStyle = Border3DStyle.Raised;
      this.statusRefresh.Name = "statusRefresh";
      this.statusRefresh.Size = new Size(49, 17);
      this.statusRefresh.Text = "Refresh";
      this.statusRefresh.Click += new EventHandler(this.toolStripStatusLabel1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
      this.BackgroundImageLayout = ImageLayout.Center;
      this.ClientSize = new Size(1016, 734);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.IsMdiContainer = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "FrmMC601LogView";
      this.Text = "LogView MULTICAL® 601 ";
      this.Load += new EventHandler(this.frmMC601LogView_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
