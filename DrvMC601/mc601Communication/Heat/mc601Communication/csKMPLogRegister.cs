// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.mc601Communication.csKMPLogRegister
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

namespace Kamstrup.Heat.mc601Communication
{
  public class csKMPLogRegister
  {
    public byte Unit = (byte) 0;
    public byte Size = (byte) 0;
    public byte SignEx = (byte) 0;
    public ushort RegisterId = (ushort) 0;
    public double[] Records = new double[0];

    public csKMPLogRegister(byte noOfRecords)
    {
      this.Records = new double[(int) noOfRecords];
    }

    public void CopyRecordsTo(double[] newRecords)
    {
      double[] numArray = new double[newRecords.Length + this.Records.Length];
      int index = 0;
      foreach (double num in this.Records)
      {
        numArray[index] = num;
        ++index;
      }
      foreach (double num in newRecords)
      {
        numArray[index] = num;
        ++index;
      }
      this.Records = numArray;
    }
  }
}
