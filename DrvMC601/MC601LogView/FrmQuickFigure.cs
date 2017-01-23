// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmQuickFigure
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Kamstrup.Heat.mc601Communication;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class FrmQuickFigure : Form
  {
    private Functions mc601Functions = new Functions();
    private IContainer components;
    private Label label1;
    private Label label2;
    private TextBox txtRegisteredEnergy;
    private TextBox txtQuickFigure;
    private Button btnClear;
    private Button btnRead;
    private Button btnClose;
    private Label lblUnit1;

    public FrmQuickFigure()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmQuickFigure));
      this.label1 = new Label();
      this.label2 = new Label();
      this.txtRegisteredEnergy = new TextBox();
      this.txtQuickFigure = new TextBox();
      this.btnClear = new Button();
      this.btnRead = new Button();
      this.btnClose = new Button();
      this.lblUnit1 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(94, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Registered Energy";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(13, 39);
      this.label2.Name = "label2";
      this.label2.Size = new Size(64, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Quick figure";
      this.txtRegisteredEnergy.BackColor = Color.White;
      this.txtRegisteredEnergy.Location = new Point(128, 10);
      this.txtRegisteredEnergy.Name = "txtRegisteredEnergy";
      this.txtRegisteredEnergy.ReadOnly = true;
      this.txtRegisteredEnergy.Size = new Size(83, 20);
      this.txtRegisteredEnergy.TabIndex = 2;
      this.txtRegisteredEnergy.TextAlign = HorizontalAlignment.Right;
      this.txtQuickFigure.BackColor = Color.White;
      this.txtQuickFigure.Location = new Point(128, 36);
      this.txtQuickFigure.Name = "txtQuickFigure";
      this.txtQuickFigure.ReadOnly = true;
      this.txtQuickFigure.Size = new Size(83, 20);
      this.txtQuickFigure.TabIndex = 3;
      this.txtQuickFigure.TextAlign = HorizontalAlignment.Right;
      this.btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnClear.Location = new Point(93, 62);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new Size(75, 23);
      this.btnClear.TabIndex = 13;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new EventHandler(this.btnClear_Click);
      this.btnRead.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRead.Location = new Point(12, 62);
      this.btnRead.Name = "btnRead";
      this.btnRead.Size = new Size(75, 23);
      this.btnRead.TabIndex = 12;
      this.btnRead.Text = "Read";
      this.btnRead.UseVisualStyleBackColor = true;
      this.btnRead.Click += new EventHandler(this.btnRead_Click);
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.Location = new Point(173, 62);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 14;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.lblUnit1.AutoSize = true;
      this.lblUnit1.Location = new Point(218, 13);
      this.lblUnit1.Name = "lblUnit1";
      this.lblUnit1.Size = new Size(13, 13);
      this.lblUnit1.TabIndex = 15;
      this.lblUnit1.Text = "[]";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(258, 97);
      this.Controls.Add((Control) this.lblUnit1);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.btnClear);
      this.Controls.Add((Control) this.btnRead);
      this.Controls.Add((Control) this.txtQuickFigure);
      this.Controls.Add((Control) this.txtRegisteredEnergy);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      //this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(266, 131);
      this.Name = "FrmQuickFigure";
      this.ShowIcon = false;
      this.Text = "Quick figure";
      this.Load += new EventHandler(this.FrmQuickFigure_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void btnRead_Click(object sender, EventArgs e)
    {
      try
      {
        this.Clear();
        ushort precounter = this.GetPrecounter();
        ushort impl = this.GetImpl();
        string str = "";
        Decimal num1 = new Decimal(1);
        string format = "f2";
        ushort num2 = (ushort) Math.Round(Convert.ToDecimal((int) precounter / (int) impl));
        if ((uint) num2 <= 10U)
        {
          if ((int) num2 != 1)
          {
            if ((int) num2 == 10)
            {
              str = "Wh";
              num1 = new Decimal(1);
              format = "f1";
            }
          }
          else
          {
            str = "Wh";
            num1 = new Decimal(1, 0, 0, false, (byte) 1);
            format = "f2";
          }
        }
        else if ((int) num2 != 100)
        {
          if ((int) num2 == 1000)
          {
            str = "kWh";
            num1 = new Decimal(1, 0, 0, false, (byte) 2);
            format = "f2";
          }
        }
        else
        {
          str = "Wh";
          num1 = new Decimal(10);
          format = "f0";
        }
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add( 155);
        string ErrorMessage = "";
        if (this.mc601Functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
        {
          Decimal num3 = Convert.ToDecimal(arrValues[0]) * num1;
          this.txtRegisteredEnergy.Text = num3.ToString(format);
          this.txtQuickFigure.Text = (num3 * new Decimal(86, 0, 0, false, (byte) 1)).ToString(format);
          this.lblUnit1.Text = "[" + str + "]";
        }
        else
        {
          int num4 = (int) MessageBox.Show("Unable to read data.", "Error");
        }
      }
      catch
      {
        int num = (int) MessageBox.Show("Unable to read data.", "Error");
      }
    }

    private void Clear()
    {
      this.txtRegisteredEnergy.Text = "";
      this.txtQuickFigure.Text = "";
      this.lblUnit1.Text = "[]";
    }

    private ushort GetPrecounter()
    {
      ArrayList arrEeprom = new ArrayList();
      string ErrorMessage = "";
      ushort num1 = (ushort) byte.MaxValue;
      if (this.mc601Functions.ReadEeprom((byte) 63, (ushort) 3536, (ushort) 2, arrEeprom, out ErrorMessage))
      {
        if (arrEeprom.Count > 0)
        {
          num1 = Convert.ToUInt16((int) Convert.ToByte(arrEeprom[0]) * 256 + (int) Convert.ToByte(arrEeprom[1]));
        }
        else
        {
          int num2 = (int) MessageBox.Show("Failed to read the precounter value.\r\n" + ErrorMessage, "Reading failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      else
      {
        int num3 = (int) MessageBox.Show("Failed to read the precounter value.\r\n" + ErrorMessage, "Reading failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return num1;
    }

    private ushort GetImpl()
    {
      ArrayList arrEeprom = new ArrayList();
      string ErrorMessage = "";
      ushort num1 = (ushort) byte.MaxValue;
      if (this.mc601Functions.ReadEeprom((byte) 63, (ushort) 3560, (ushort) 2, arrEeprom, out ErrorMessage))
      {
        if (arrEeprom.Count > 0)
        {
          num1 = Convert.ToUInt16(((int) Convert.ToByte(arrEeprom[0]) * 256 + (int) Convert.ToByte(arrEeprom[1])) / 100);
        }
        else
        {
          int num2 = (int) MessageBox.Show("Failed to read the precounter value.\r\n" + ErrorMessage, "Reading failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      else
      {
        int num3 = (int) MessageBox.Show("Failed to read the precounter value.\r\n" + ErrorMessage, "Reading failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return num1;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      this.Clear();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void FrmQuickFigure_Load(object sender, EventArgs e)
    {
      this.btnRead.Focus();
    }
  }
}
