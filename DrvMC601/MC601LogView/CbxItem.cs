// Decompiled with JetBrains decompiler
// Type: MC601LogView.CbxItem
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

namespace MC601LogView
{
  public class CbxItem
  {
    private UCRegisterCheckBox m_Register;

    public UCRegisterCheckBox Register
    {
      get
      {
        return this.m_Register;
      }
      set
      {
        this.m_Register = value;
      }
    }

    public override string ToString()
    {
      return this.Register.ToString();
    }
  }
}
