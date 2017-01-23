// Decompiled with JetBrains decompiler
// Type: MC601LogView.UCRegisterCheckBox
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MC601LogView
{
  public class UCRegisterCheckBox : UserControl
  {
    private string m_UnitText = "";
    private string m_RegisterCaption1 = "";
    private string m_RegisterCaption2 = "";
    private string m_RegisterName = "";
    private string m_RegisterName2 = "";
    private string m_CalculationRule = "";
    private bool m_UseRegister2 = true;
    private bool m_IsRegister = true;
    private ERegisterType m_RegisterType;
    private Decimal m_CalculationValue;
    private int m_RegisterId;
    private IContainer components;
    private CheckBox checkBox;

    public ERegisterType RegisterType
    {
      get
      {
        return this.m_RegisterType;
      }
      set
      {
        this.m_RegisterType = value;
      }
    }

    public string UnitText
    {
      get
      {
        return this.m_UnitText;
      }
      set
      {
        this.m_UnitText = value;
      }
    }

    public string RegisterCaption1
    {
      get
      {
        return this.m_RegisterCaption1;
      }
      set
      {
        this.m_RegisterCaption1 = value;
      }
    }

    public string RegisterCaption2
    {
      get
      {
        return this.m_RegisterCaption2;
      }
      set
      {
        this.m_RegisterCaption2 = value;
      }
    }

    public string RegisterName
    {
      get
      {
        return this.m_RegisterName;
      }
      set
      {
        this.m_RegisterName = value;
      }
    }

    public string RegisterName2
    {
      get
      {
        return this.m_RegisterName2;
      }
      set
      {
        this.m_RegisterName2 = value;
      }
    }

    public string CalculationRule
    {
      get
      {
        return this.m_CalculationRule;
      }
      set
      {
        this.m_CalculationRule = value;
      }
    }

    public bool UseRegister2
    {
      get
      {
        return this.m_UseRegister2;
      }
      set
      {
        this.m_UseRegister2 = value;
      }
    }

    public Decimal CalculationValue
    {
      get
      {
        return this.m_CalculationValue;
      }
      set
      {
        this.m_CalculationValue = value;
      }
    }

    public int RegisterId
    {
      get
      {
        return this.m_RegisterId;
      }
      set
      {
        this.m_RegisterId = value;
      }
    }

    public bool IsRegister
    {
      get
      {
        return this.m_IsRegister;
      }
      set
      {
        this.m_IsRegister = value;
      }
    }

    public string Caption
    {
      get
      {
        return this.checkBox.Text;
      }
      set
      {
        this.checkBox.Text = value;
      }
    }

    public bool Checked
    {
      get
      {
        return this.checkBox.Checked;
      }
      set
      {
        this.checkBox.Checked = value;
      }
    }

    public UCRegisterCheckBox()
    {
      this.InitializeComponent();
    }

    public override string ToString()
    {
      return this.Caption;
    }

    private void UCRegisterCheckBox_Paint(object sender, PaintEventArgs e)
    {
      this.Width = Convert.ToInt32(30f + e.Graphics.MeasureString(this.checkBox.Text.Trim(), this.Font).Width);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.checkBox = new CheckBox();
      this.SuspendLayout();
      this.checkBox.AutoSize = true;
      this.checkBox.Dock = DockStyle.Fill;
      this.checkBox.Location = new Point(0, 0);
      this.checkBox.Name = "checkBox";
      this.checkBox.Size = new Size(150, 38);
      this.checkBox.TabIndex = 0;
      this.checkBox.Text = "checkBox1";
      this.checkBox.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.checkBox);
      this.Name = "UCRegisterCheckBox";
      this.Size = new Size(150, 38);
      this.Paint += new PaintEventHandler(this.UCRegisterCheckBox_Paint);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
