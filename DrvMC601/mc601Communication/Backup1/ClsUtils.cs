// Decompiled with JetBrains decompiler
// Type: MC601LogView.ClsUtils
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using System;

namespace MC601LogView
{
  internal class ClsUtils
  {
    private ClsUtils()
    {
    }

    public static string UnitsForRegisters(byte nUnit)
    {
      switch (nUnit)
      {
        case (byte) 1:
          return "Wh";
        case (byte) 2:
          return "kWh";
        case (byte) 3:
          return "MWh";
        case (byte) 4:
          return "GWh";
        case (byte) 5:
          return "j";
        case (byte) 6:
          return "kj";
        case (byte) 7:
          return "Mj";
        case (byte) 8:
          return "Gj";
        case (byte) 9:
          return "Cal";
        case (byte) 10:
          return "kCal";
        case (byte) 11:
          return "Mcal";
        case (byte) 12:
          return "Gcal";
        case (byte) 13:
          return "varh";
        case (byte) 14:
          return "kvarh";
        case (byte) 15:
          return "Mvarh";
        case (byte) 16:
          return "Gvarh";
        case (byte) 17:
          return "VAh";
        case (byte) 18:
          return "kVAh";
        case (byte) 19:
          return "MVAh";
        case (byte) 20:
          return "GVAh";
        case (byte) 21:
          return "W";
        case (byte) 22:
          return "kW";
        case (byte) 23:
          return "MW";
        case (byte) 24:
          return "GW";
        case (byte) 25:
          return "var";
        case (byte) 26:
          return "kvar";
        case (byte) 27:
          return "Mvar";
        case (byte) 28:
          return "Gvar";
        case (byte) 29:
          return "VA";
        case (byte) 30:
          return "kVA";
        case (byte) 31:
          return "MVA";
        case (byte) 32:
          return "GVA";
        case (byte) 33:
          return "V";
        case (byte) 34:
          return "A";
        case (byte) 35:
          return "kV";
        case (byte) 36:
          return "kA";
        case (byte) 37:
          return "C";
        case (byte) 38:
          return "K";
        case (byte) 39:
          return "l";
        case (byte) 40:
          return "m3";
        case (byte) 41:
          return "l/h";
        case (byte) 42:
          return "m3/h";
        case (byte) 43:
          return "m3xC";
        case (byte) 44:
          return "ton";
        case (byte) 45:
          return "ton/h";
        case (byte) 46:
          return "h";
        case (byte) 47:
          return "hh:mm:ss";
        case (byte) 48:
          return "yy:mm:dd";
        case (byte) 49:
          return "yyyy:mm:dd";
        case (byte) 50:
          return "mm:dd";
        case (byte) 51:
          return "nummer";
        case (byte) 52:
          return "bar";
        default:
          return "";
      }
    }

    public static string InfoCodeToString(double infoCode)
    {
      string str = "";
      int num = (int) infoCode;
      if (num == 0)
        return str;
      if ((num & 1) > 0)
        str += "Supply voltage has been cut off, ";
      if ((num & 8) > 0)
        str += "Temperature sensor T1 outside measuring range, ";
      if ((num & 4) > 0)
        str += "Temperature sensor T2 outside measuring range, ";
      if ((num & 32) > 0)
        str += "Temperature sensor T3 outside measuring range, ";
      if ((num & 64) > 0)
        str += "Leak in the Cold-water system, ";
      if ((num & 256) > 0)
        str += "Leak in the heating system, ";
      if ((num & 512) > 0)
        str += "Burst in the heating system, ";
      if ((num & 16) > 0)
        str += "Flow sensor V1 - Datacomm error - signal too low or wrong direction, ";
      if ((num & 1024) > 0)
        str += "Flow sensor V2 - Datacomm error - signal too low or wrong direction, ";
      if ((num & 2048) > 0)
        str += "Flow sensor V1 Wrong meter factor, ";
      if ((num & 128) > 0)
        str += "Flow sensor V2 Wrong meter factor, ";
      if ((num & 4096) > 0)
        str += "Flow sensor V1 Signal too low (Air), ";
      if ((num & 8192) > 0)
        str += "Flow sensor V2 Signal too low (Air), ";
      if ((num & 16384) > 0)
        str += "Flow sensor V1 Wrong flow direction, ";
      if ((num & 32768) > 0)
        str += "Flow sensor V2 Wrong flow direction, ";
      if (str.Length < 3)
        return str;
      return str.Remove(str.Length - 2);
    }

    public static string LogQOSCodeToString(Decimal infoCode)
    {
      string str = "";
      int num = (int) infoCode;
      if (num == 0)
        return str;
      if ((num & 1) > 0)
        str += "Start up sequence, ";
      if ((num & 2) > 0)
        str += "Power failure, ";
      if ((num & 4) > 0)
        str += "Unable to read meter, ";
      if ((num & 16) > 0)
        str += "Logger configuration changed, ";
      if ((num & 32) > 0)
        str += "Logger interval changed, ";
      if ((num & 64) > 0)
        str += "Log erased, ";
      if ((num & 256) > 0)
        str += "RTC time no valid, ";
      if ((num & 512) > 0)
        str += "Time adjusted 7-15 sec., ";
      if ((num & 1024) > 0)
        str += "Time adjusted more than 15 sec., ";
      if ((num & 16777216) > 0)
        str += "RTC communication error, ";
      if ((num & 33554432) > 0)
        str += "EEPROM communication error, ";
      if (str.Length < 3)
        return str;
      return str.Remove(str.Length - 2);
    }
  }
}
