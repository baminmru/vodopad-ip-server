// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.Protocol.csDataFrame
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using Kamstrup.Heat;
using System.Collections;

namespace Kamstrup.Heat.Protocol
{
  public class csDataFrame
  {
    private byte[] m_arrStuffBytes = new byte[5]
    {
      ControlCharacters.TXStart,
      ControlCharacters.RXStart,
      ControlCharacters.StopByte,
      ControlCharacters.ACK,
      ControlCharacters.Stuff
    };
    private byte m_bytStartByte;
    private byte m_bytDestinationAddress;
    private byte m_bytCID;
    private ArrayList m_arrCommBuffer;
    private ushort m_shoCRC;
    private byte m_bytStopByte;
    private ArrayList m_arrData;

    public byte DestinationAddress
    {
      get
      {
        return (byte) this.m_arrCommBuffer[1];
      }
    }

    public byte CID
    {
      get
      {
        return (byte) this.m_arrCommBuffer[2];
      }
    }

    public ArrayList Data
    {
      get
      {
        return this.m_arrData;
      }
    }

    public bool CRC
    {
      get
      {
        return (int) csTools.BigEndianBytesToShort(this.m_arrCommBuffer.GetRange(this.m_arrCommBuffer.Count - 3, 2)) == (int) this.m_shoCRC;
      }
    }

    public csDataFrame(ArrayList arrCommBuffer)
    {
      this.m_arrCommBuffer = new ArrayList();
      this.m_arrCommBuffer.Add(arrCommBuffer[0]);
      this.m_arrCommBuffer.AddRange((ICollection) this.Unstuff(arrCommBuffer.GetRange(1, arrCommBuffer.Count - 2)));
      this.m_arrCommBuffer.Add(arrCommBuffer[arrCommBuffer.Count - 1]);
      this.m_bytStartByte = (byte) this.m_arrCommBuffer[0];
      this.m_bytDestinationAddress = (byte) arrCommBuffer[1];
      this.m_bytCID = (byte) arrCommBuffer[2];
      this.m_shoCRC = csTools.BigEndianBytesToShort(this.m_arrCommBuffer.GetRange(this.m_arrCommBuffer.Count - 3, 2));
      this.m_bytStopByte = (byte) this.m_arrCommBuffer[this.m_arrCommBuffer.Count - 1];
      this.m_arrData = new ArrayList();
      this.m_arrData.AddRange((ICollection) this.m_arrCommBuffer.GetRange(3, this.m_arrCommBuffer.Count - 6));
    }

    public csDataFrame(byte bytStart, byte bytDestinationAddress, byte bytCID, ArrayList arrData, byte bytStop)
    {
      this.m_arrCommBuffer = new ArrayList();
      this.m_bytStartByte = bytStart;
      this.m_arrCommBuffer.Add((object) bytStart);
      this.m_bytDestinationAddress = bytDestinationAddress;
      this.m_arrCommBuffer.Add((object) bytDestinationAddress);
      this.m_bytCID = bytCID;
      this.m_arrCommBuffer.Add((object) bytCID);
      if (arrData.Count > 0)
        this.m_arrCommBuffer.AddRange((ICollection) arrData);
      this.m_shoCRC = this.GetCRC(this.m_arrCommBuffer.GetRange(1, this.m_arrCommBuffer.Count - 1));
      this.m_arrCommBuffer.AddRange((ICollection) csTools.ShortToBigEndianBytes(this.m_shoCRC));
      this.m_bytStopByte = bytStop;
      this.m_arrCommBuffer.Add((object) bytStop);
    }

    public ArrayList GetCommBuffer()
    {
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) this.m_bytStartByte);
      arrayList.AddRange((ICollection) this.Stuff(this.m_arrCommBuffer.GetRange(1, this.m_arrCommBuffer.Count - 2)));
      arrayList.Add((object) this.m_bytStopByte);
      return arrayList;
    }

    private ushort GetCRC(ArrayList arrCRCData)
    {
      return csTools.CalculateCRC((byte[]) arrCRCData.ToArray(typeof (byte)));
    }

    private ArrayList Unstuff(ArrayList arrCommBuffer)
    {
      bool flag = false;
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList((ICollection) this.m_arrStuffBytes);
      for (int index = 0; index < arrayList2.Count; ++index)
        arrayList2[index] = (object) ~(byte) arrayList2[index];
      arrayList2.Sort();
      int index1 = -1;
      foreach (byte num in arrCommBuffer)
      {
        if ((int) num == (int) ControlCharacters.Stuff)
          flag = true;
        else if (flag && (index1 = arrayList2.BinarySearch((object) num)) >= 0)
        {
          arrayList1.Add((object) ~(byte) arrayList2[index1]);
          flag = false;
        }
        else
          arrayList1.Add((object) num);
      }
      return arrayList1;
    }

    private ArrayList Stuff(ArrayList arrCommBuffer)
    {
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList((ICollection) this.m_arrStuffBytes);
      arrayList2.Sort();
      foreach (byte num in arrCommBuffer)
      {
        int index;
        if ((index = arrayList2.BinarySearch((object) num)) >= 0)
        {
          arrayList1.Add((object) 27);
          arrayList1.Add((object) ~(byte) arrayList2[index]);
        }
        else
          arrayList1.Add((object) num);
      }
      return arrayList1;
    }
  }
}
