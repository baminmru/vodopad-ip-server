// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.mc601Communication.Settings
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using System;
using System.Xml.Serialization;

namespace Kamstrup.Heat.mc601Communication
{
  [Serializable]
  public class Settings
  {
    [XmlElement]
    public string COMM_Port = "COM1";
    [XmlElement]
    public byte ACK = (byte) 6;
    [XmlElement]
    public byte StopByte = (byte) 13;
    [XmlElement]
    public byte Stuff = (byte) 27;
    [XmlElement]
    public byte RXStart = (byte) 64;
    [XmlElement]
    public byte TXStart = (byte) 0x80; //sbyte.MinValue;
  }
}
