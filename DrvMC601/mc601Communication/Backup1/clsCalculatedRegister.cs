// Decompiled with JetBrains decompiler
// Type: MC601LogView.clsCalculatedRegister
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using System;

namespace MC601LogView
{
  [Serializable]
  internal class clsCalculatedRegister
  {
    public string RegisterName_1 = "";
    public string RegisterName_2 = "";
    public string RegisterCaption_1 = "";
    public string RegisterCaption_2 = "";
    public bool UseRegister2 = true;
    public string CalculationRule = "";
    public Decimal CalculationValue;

    public override string ToString()
    {
      if (this.UseRegister2)
        return "'" + this.RegisterCaption_1 + "' " + this.CalculationRule + " '" + this.RegisterCaption_2 + "'";
      return "'" + this.RegisterCaption_1 + "' " + this.CalculationRule + " " + this.CalculationValue.ToString();
    }
  }
}
