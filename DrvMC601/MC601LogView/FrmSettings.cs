// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmSettings
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Kamstrup.Heat.mc601Communication;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmSettings : Form
  {
    private Functions m_mc601Functions = new Functions();
    private IContainer components;
    private Button btnCancel;
    private Button btnOK;
    private Label lblComPort;
    private ComboBox cbxComPort;

    public FrmSettings()
    {
      this.InitializeComponent();
      this.FillcbxCOMPort();
    }

    private void FillcbxCOMPort()
    {
      this.cbxComPort.Items.Clear();
      foreach (object obj in SerialPort.GetPortNames())
        this.cbxComPort.Items.Add(obj);
      this.SetComPort(this.cbxComPort);
    }

    private bool SetComPort(ComboBox cbx)
    {
      bool flag = true;
      try
      {
        cbx.Text = this.m_mc601Functions.GetComPort();
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    private bool StoreComPort(string comPort)
    {
      bool flag;
      try
      {
        this.Cursor = Cursors.WaitCursor;
        this.m_mc601Functions.SetComPort(comPort);
        flag = this.m_mc601Functions.SaveSettings();
      }
      catch
      {
        flag = false;
      }
      this.Cursor = Cursors.Default;
      return flag;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.cbxComPort.Text == this.m_mc601Functions.GetComPort())
        this.Close();
      if (this.StoreComPort(this.cbxComPort.Text))
      {
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show("The COM-PORT setting could not be stored.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnCancel = new Button();
      this.btnOK = new Button();
      this.lblComPort = new Label();
      this.cbxComPort = new ComboBox();
      this.SuspendLayout();
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(198, 61);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.Location = new Point(117, 61);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.lblComPort.AutoSize = true;
      this.lblComPort.Location = new Point(12, 12);
      this.lblComPort.Name = "lblComPort";
      this.lblComPort.Size = new Size(50, 13);
      this.lblComPort.TabIndex = 2;
      this.lblComPort.Text = "Com Port";
      this.cbxComPort.FormattingEnabled = true;
      this.cbxComPort.Location = new Point(117, 9);
      this.cbxComPort.Name = "cbxComPort";
      this.cbxComPort.Size = new Size(156, 21);
      this.cbxComPort.Sorted = true;
      this.cbxComPort.TabIndex = 3;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(285, 91);
      this.Controls.Add((Control) this.cbxComPort);
      this.Controls.Add((Control) this.lblComPort);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.btnCancel);
      //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "FrmSettings";
      this.ShowInTaskbar = false;
      this.Text = "Settings";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
