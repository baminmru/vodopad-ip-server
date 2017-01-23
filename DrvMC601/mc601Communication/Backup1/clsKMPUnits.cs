// Decompiled with JetBrains decompiler
// Type: MC601LogView.clsKMPUnits
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

namespace MC601LogView
{
  internal class clsKMPUnits
  {
    public static string UnitId2String(int nKMPUnit)
    {
      return clsKMPUnits.UnitId2String((eKMPUnit) nKMPUnit);
    }

    public static string UnitId2String(eKMPUnit KMPUnit)
    {
      switch (KMPUnit)
      {
        case eKMPUnit.Gj:
          return "Gj";
        case eKMPUnit.Cal:
          return "Cal";
        case eKMPUnit.kCal:
          return "kCal";
        case eKMPUnit.Mcal:
          return "MCal";
        case eKMPUnit.Gcal:
          return "GCal";
        case eKMPUnit.varh:
          return "";
        case eKMPUnit.kvarh:
          return "";
        case eKMPUnit.Mvarh:
          return "";
        case eKMPUnit.Gvarh:
          return "";
        case eKMPUnit.VAh:
          return "";
        case eKMPUnit.kVAh:
          return "";
        case eKMPUnit.MVAh:
          return "";
        case eKMPUnit.GVAh:
          return "";
        case eKMPUnit.W:
          return "W";
        case eKMPUnit.kW:
          return "kW";
        case eKMPUnit.MW:
          return "MW";
        case eKMPUnit.GW:
          return "GW";
        case eKMPUnit.var:
          return "var";
        case eKMPUnit.kvar:
          return "kvar";
        case eKMPUnit.Mvar:
          return "Mvar";
        case eKMPUnit.Gvar:
          return "Gvar";
        case eKMPUnit.VA:
          return "VA";
        case eKMPUnit.kVA:
          return "kVA";
        case eKMPUnit.MVA:
          return "MVA";
        case eKMPUnit.GVA:
          return "GVA";
        case eKMPUnit.V:
          return "V";
        case eKMPUnit.A:
          return "A";
        case eKMPUnit.kV:
          return "kV";
        case eKMPUnit.kA:
          return "kA";
        case eKMPUnit.C:
          return "C";
        case eKMPUnit.K:
          return "K";
        case eKMPUnit.l:
          return "l";
        case eKMPUnit.m3:
          return "m\x00B3";
        case eKMPUnit.l_h:
          return "l/h";
        case eKMPUnit.m3_h:
          return "m\x00B3/h";
        case eKMPUnit.m3xC:
          return "m\x00B3*C";
        case eKMPUnit.ton:
          return "ton";
        case eKMPUnit.ton_h:
          return "ton/h";
        case eKMPUnit.h:
          return "h";
        case eKMPUnit.clock:
          return "hh:mm:ss";
        case eKMPUnit.dato1:
          return "yy:mm:dd";
        case eKMPUnit.dato2:
          return "yyyy:mm:dd";
        case eKMPUnit.dato3:
          return "mm:dd";
        case eKMPUnit.number:
          return "number";
        case eKMPUnit.bar:
          return "bar";
        case eKMPUnit.RCT:
          return "RCT";
        case eKMPUnit.ASCII:
          return "ASCII";
        case eKMPUnit.m3X10:
          return "m\x00B3*10";
        case eKMPUnit.tonX10:
          return "ton*10";
        case eKMPUnit.GJX10:
          return "GJ*10";
        case eKMPUnit.minutes:
          return "minutes";
        default:
          return "Unknown unit";
      }
    }
  }
}
