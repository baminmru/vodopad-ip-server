// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmContact
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using MC601LogView.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmContact : Form
  {
    private IContainer components;
    private Label label1;
    private Label label2;
    private PictureBox pictureBox1;
    private LinkLabel linkLabel1;

    public FrmContact()
    {
      this.InitializeComponent();
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      Graphics graphics = pevent.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.White, this.BackColor, LinearGradientMode.Vertical);
      graphics.FillRectangle((Brush) linearGradientBrush, rect);
      linearGradientBrush.Dispose();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(string.Format("mailto:{0}?subject={1}&body={2}",  "metertool@kamstrup.com",  "LogView MULTICAL 601 registration",  "For update informations regarding LogView, please register.%0ACompany:%0AContact person:%0ASerial No:%0AOrder No:%0A"));
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
      this.label2 = new Label();
      this.pictureBox1 = new PictureBox();
      this.linkLabel1 = new LinkLabel();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.Transparent;
      this.label1.Location = new Point(16, 131);
      this.label1.Name = "label1";
      this.label1.Size = new Size(152, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Mail: metertool@kamstrup.com";
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Location = new Point(16, 152);
      this.label2.Name = "label2";
      this.label2.Size = new Size(129, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Web: www.kamstrup.com";
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.Image = (Image) Resources.k2;
      this.pictureBox1.Location = new Point(19, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(203, 116);
      this.pictureBox1.TabIndex = 2;
      this.pictureBox1.TabStop = false;
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(16, 175);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(179, 13);
      this.linkLabel1.TabIndex = 3;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "LogView MULTICAL 601 registration";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(234, 196);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "FrmContact";
      this.Text = "Contact Kamstrup A/S";
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
