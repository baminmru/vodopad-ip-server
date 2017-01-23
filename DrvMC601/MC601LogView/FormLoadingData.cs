// Decompiled with JetBrains decompiler
// Type: MC601LogView.FormLoadingData
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FormLoadingData : Form
  {
    private IContainer components;
    private Label label1;
    private System.Windows.Forms.ProgressBar progress;

    public int ProgressBar
    {
      get
      {
        return this.progress.Value;
      }
      set
      {
        this.UpdateProgress(value);
      }
    }

    public int ProgressBarMax
    {
      get
      {
        return this.progress.Maximum;
      }
      set
      {
        this.UpdateMax(value);
      }
    }

    public FormLoadingData()
    {
      this.InitializeComponent();
    }

    private void UpdateMax(int val)
    {
      if (this.progress.InvokeRequired)
        this.Invoke((Delegate) new FormLoadingData.dInt(this.UpdateMax),  val);
      else
        this.progress.Maximum = val;
    }

    private void UpdateProgress(int val)
    {
      if (this.progress.InvokeRequired)
        this.Invoke((Delegate) new FormLoadingData.dInt(this.UpdateProgress),  val);
      else
        this.progress.Value = Math.Min(val, this.progress.Maximum);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.progress = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 18.25f);
      this.label1.Location = new Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(308, 29);
      this.label1.TabIndex = 0;
      this.label1.Text = "Loading data from module";
      this.progress.Location = new Point(12, 41);
      this.progress.Name = "progress";
      this.progress.Size = new Size(304, 23);
      this.progress.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      ////this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.InactiveCaptionText;
      this.ClientSize = new Size(328, 76);
      this.Controls.Add((Control) this.progress);
      this.Controls.Add((Control) this.label1);
      ////this.FormBorderStyle = FormBorderStyle.None;
      this.Name = "FormLoadingData";
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "FormLoadingData";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void dInt(int val);
  }
}
