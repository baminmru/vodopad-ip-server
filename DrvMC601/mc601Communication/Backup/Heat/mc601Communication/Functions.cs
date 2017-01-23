// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.mc601Communication.Functions
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using Kamstrup.Heat;
using Kamstrup.Heat.Protocol;
using Kamstrup.PTA;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace Kamstrup.Heat.mc601Communication
{
  public class Functions : csCommands
  {
    public bool GetType(byte bytDestinationAddress, out ArrayList arrTypeRev, out string ErrorMessage)
    {
      ErrorMessage = "";
      arrTypeRev = (ArrayList) null;
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      this.ToString();
      ArrayList arrData = new ArrayList();
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, bytDestinationAddress, (byte) 1, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
        return false;
      csDataFrame csDataFrame = new csDataFrame(data);
      if (!csDataFrame.CRC)
        return false;
      arrTypeRev = csDataFrame.Data;
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool GetType(byte bytDestinationAddress, out ushort Type, out string Revision, out string ErrorMessage)
    {
      Type = (ushort) 0;
      Revision = "";
      ArrayList arrTypeRev;
      bool type = this.GetType(bytDestinationAddress, out arrTypeRev, out ErrorMessage);
      if (type)
      {
        Type = csTools.BigEndianBytesToShort(arrTypeRev.GetRange(0, 2));
        byte num1 = (byte) arrTypeRev[2];
        byte num2 = (byte) arrTypeRev[3];
        Revision = Convert.ToChar((int) num1 + 64).ToString();
        Revision += Convert.ToString(num2, 10);
      }
      return type;
    }

    public bool WriteEeprom(byte bytDestinationAddress, ushort shoPassword, ushort shoStartAddress, ArrayList arrEepromValues, bool StayOpen, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      byte bytNumberOfBytes1 = (byte) 64;
      int num = arrEepromValues.Count / (int) bytNumberOfBytes1;
      byte bytNumberOfBytes2 = (byte) ((uint) arrEepromValues.Count % (uint) bytNumberOfBytes1);
      StatusEventArgs e = new StatusEventArgs();
      e.JobName = "WriteEeprom...";
      for (int index = 0; index < num; ++index)
      {
        bool flag = this.write_eeprom(bytDestinationAddress, shoPassword, (ushort) ((uint) shoStartAddress + (uint) index * (uint) bytNumberOfBytes1), bytNumberOfBytes1, arrEepromValues.GetRange(index * (int) bytNumberOfBytes1, (int) bytNumberOfBytes1), out ErrorMessage);
        if (!flag)
        {
          HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "WriteEeprom(loop) Failed");
          return flag;
        }
        e.JobStatus = 100 / (num + 1) * (index + 1);
        this.OnStatus(e);
      }
      bool flag1 = this.write_eeprom(bytDestinationAddress, shoPassword, (ushort) ((uint) shoStartAddress + (uint) num * (uint) bytNumberOfBytes1), bytNumberOfBytes2, arrEepromValues.GetRange(num * (int) bytNumberOfBytes1, (int) bytNumberOfBytes2), out ErrorMessage);
      if (!flag1)
      {
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "WriteEeprom(first/last) Failed");
        return flag1;
      }
      e.JobStatus = 100;
      this.OnStatus(e);
      this.HandlePortClose(PortOpened);
      return flag1;
    }

    public bool ReadEeprom(byte bytDestinationAddress, ushort shoStartAddress, ushort shoNumberOfBytes, ArrayList arrEeprom, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      bool flag = true;
      byte bytNumberOfBytes1 = (byte) 64;
      int num = (int) shoNumberOfBytes / (int) bytNumberOfBytes1;
      byte bytNumberOfBytes2 = (byte) ((uint) shoNumberOfBytes % (uint) bytNumberOfBytes1);
      StatusEventArgs e = new StatusEventArgs();
      e.JobName = "ReadEeprom...";
      ArrayList arrEeprom1 = new ArrayList();
      for (int index = 0; index < num; ++index)
      {
        flag = this.read_eeprom(bytDestinationAddress, (ushort) ((uint) shoStartAddress + (uint) index * (uint) bytNumberOfBytes1), bytNumberOfBytes1, ref arrEeprom1, out ErrorMessage);
        if (flag)
        {
          arrEeprom.AddRange((ICollection) arrEeprom1);
          arrEeprom1.Clear();
          e.JobStatus = 100 / (num + 1) * (index + 1);
          this.OnStatus(e);
        }
        else
          break;
      }
      if (flag)
        flag = this.read_eeprom(bytDestinationAddress, (ushort) ((uint) shoStartAddress + (uint) num * (uint) bytNumberOfBytes1), bytNumberOfBytes2, ref arrEeprom1, out ErrorMessage);
      if (flag)
      {
        arrEeprom.AddRange((ICollection) arrEeprom1);
        e.JobStatus = 100;
        this.OnStatus(e);
      }
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool ReadRAM(byte bytDestinationAddress, ushort shoStartAddress, byte bytNumberOfBytes, ArrayList arrRAM, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrRAM = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(shoStartAddress));
      arrData.Add((object) bytNumberOfBytes);
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, bytDestinationAddress, (byte) 5, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
        return false;
      csDataFrame csDataFrame = new csDataFrame(data);
      if (!csDataFrame.CRC)
        return false;
      arrRAM.AddRange((ICollection) csDataFrame.Data);
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool Reset(byte DestinationAddress, ushort Password, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(Password));
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 7, arrData, ControlCharacters.StopByte).GetCommBuffer(), 5000.0, out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool ProductionInit(byte DestinationAddress, ushort Password, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(Password));
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 8, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool WriteTestData(byte DestinationAddress, ushort Password, ushort TestDataId, ArrayList arrTestData, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(Password));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(TestDataId));
      arrData.Add((object) (byte) arrTestData.Count);
      arrData.AddRange((ICollection) arrTestData);
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 32, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool WriteRAM(byte DestinationAddress, ushort Password, ushort StartAddress, ArrayList arrRAMData, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(Password));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(StartAddress));
      arrData.Add((object) (byte) arrRAMData.Count);
      arrData.AddRange((ICollection) arrRAMData);
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 6, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool RTCPresent(byte DestinationAddress, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 115, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool GetSerialNo(byte DestinationAddress, out uint SerialNo, out string ErrorMessage)
    {
      ErrorMessage = "";
      SerialNo = 0U;
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      ArrayList data1 = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 2, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data1 == null)
        return false;
      csDataFrame csDataFrame = new csDataFrame(data1);
      if (!csDataFrame.CRC)
        return false;
      ArrayList data2 = csDataFrame.Data;
      SerialNo = csTools.BigEndianBytesToInt(data2.GetRange(0, 4));
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool SetClock(byte DestinationAddress, uint Date, uint Clock, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.IntToBigEndianBytes(Date));
      arrData.AddRange((ICollection) csTools.IntToBigEndianBytes(Clock));
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 9, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool SetTransportMode(byte DestinationAddress, ushort TMOAddress, out string ErrorMessage)
    {
      ErrorMessage = "";
      uint SerialNo = 0U;
      ArrayList arrEepromValues = new ArrayList();
      arrEepromValues.Add((object) byte.MaxValue);
      bool flag = this.GetSerialNo(DestinationAddress, out SerialNo, out ErrorMessage);
      if (flag)
      {
        ushort shoPassword = csTools.CalculateCRC(csTools.IntToBigEndianBytes(SerialNo));
        flag = this.WriteEeprom(DestinationAddress, shoPassword, TMOAddress, arrEepromValues, false, out ErrorMessage);
      }
      return flag;
    }

    public bool GetData(byte bytDestinationAddress, byte bytNumberOfRegisters, ArrayList arrRegisters, out ArrayList arrValues, out ArrayList arrUnits, out ArrayList arrDecimals, out string ErrorMessage)
    {
      ErrorMessage = "";
      arrValues = new ArrayList();
      arrUnits = new ArrayList();
      arrDecimals = new ArrayList();
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.Add((object) bytNumberOfRegisters);
      foreach (ushort shoValue in arrRegisters)
        arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(shoValue));
      ArrayList data1 = this.get_data(new csDataFrame(ControlCharacters.TXStart, bytDestinationAddress, (byte) 16, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data1 == null)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      csDataFrame csDataFrame = new csDataFrame(data1);
      if (!csDataFrame.CRC)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      ushort num1 = (ushort) 0;
      byte Decimals = (byte) 0;
      byte num2;
      int index1;
      for (int index2 = 0; index2 < csDataFrame.Data.Count - 5; index2 = index1 + (int) num2)
      {
        num1 = csTools.BigEndianBytesToShort(csDataFrame.Data.GetRange(index2, 2));
        int num3 = index2 + 2;
        ArrayList data2 = csDataFrame.Data;
        int index3 = num3;
        int num4 = 1;
        int num5 = index3 + num4;
        byte num6 = (byte) data2[index3];
        ArrayList data3 = csDataFrame.Data;
        int index4 = num5;
        int num7 = 1;
        int num8 = index4 + num7;
        num2 = (byte) data3[index4];
        ArrayList data4 = csDataFrame.Data;
        int index5 = num8;
        int num9 = 1;
        index1 = index5 + num9;
        byte SignExp = (byte) data4[index5];
        arrValues.Add((object) csTools.ToFloat(SignExp, csDataFrame.Data.GetRange(index1, (int) num2), out Decimals));
        arrUnits.Add((object) num6);
        arrDecimals.Add((object) Decimals);
      }
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool GetData(byte bytDestinationAddress, byte bytNumberOfRegisters, ArrayList arrRegisters, out ArrayList arrValues, out ArrayList arrUnits, out string ErrorMessage)
    {
      ErrorMessage = "";
      ArrayList arrDecimals;
      return this.GetData(bytDestinationAddress, bytNumberOfRegisters, arrRegisters, out arrValues, out arrUnits, out arrDecimals, out ErrorMessage);
    }

    public bool GetLogFromRecordIdTowardsPresent(byte DestinationAddress, byte LogId, ushort[] registers, byte noOfRecords, ushort fromRecordId, ref ushort lastRecordId, ref ushort newestRecordId, ref byte info, ref csKMPLogRegisters KMPLogRegisters, out string ErrorMessage)
    {
      bool PortOpened = false;
      byte bytCID = (byte) 162;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      byte num1 = Convert.ToByte(registers.Length);
      ArrayList arrData = new ArrayList();
      arrData.Add((object) LogId);
      arrData.Add((object) num1);
      foreach (ushort shoValue in registers)
        arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(shoValue));
      arrData.Add((object) noOfRecords);
      arrData.Add((object) Convert.ToByte(128));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(fromRecordId));
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, bytCID, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      csDataFrame objGetLogDataAns = new csDataFrame(data);
      if (!objGetLogDataAns.CRC || objGetLogDataAns.Data.Count < 4)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      byte num2 = (byte) objGetLogDataAns.Data[1];
      KMPLogRegisters.Clear();
      int num3 = objGetLogDataAns.Data.Count - 3;
      lastRecordId = Functions.GetUshortFromLogData(objGetLogDataAns, objGetLogDataAns.Data.Count - 5);
      newestRecordId = Functions.GetUshortFromLogData(objGetLogDataAns, objGetLogDataAns.Data.Count - 3);
      info = (byte) objGetLogDataAns.Data[objGetLogDataAns.Data.Count - 1];
      ushort num4 = (ushort) 0;
      ushort num5 = (ushort) 0;
      int pointer1 = 2;
      while (num3 - pointer1 > 5)
      {
        num5 = Functions.GetUshortFromLogData(objGetLogDataAns, pointer1);
        pointer1 += 2;
        ++num4;
        if ((int) num4 == 1)
        {
          for (int index1 = 1; index1 <= (int) num2; ++index1)
          {
            csKMPLogRegister csKmpLogRegister = new csKMPLogRegister(noOfRecords);
            csKmpLogRegister.RegisterId = Functions.GetUshortFromLogData(objGetLogDataAns, pointer1);
            int index2 = pointer1 + 2;
            csKmpLogRegister.Unit = (byte) objGetLogDataAns.Data[index2];
            int index3 = index2 + 1;
            csKmpLogRegister.Size = (byte) objGetLogDataAns.Data[index3];
            int index4 = index3 + 1;
            csKmpLogRegister.SignEx = (byte) objGetLogDataAns.Data[index4];
            int pointer2 = index4 + 1;
            csKmpLogRegister.Records[(int) num4 - 1] = Functions.GetDoubleFromLogData(objGetLogDataAns, csKmpLogRegister.SignEx, pointer2);
            pointer1 = pointer2 + 4;
            KMPLogRegisters.Add(csKmpLogRegister);
          }
        }
        else
        {
          for (int index = 1; index <= (int) num2; ++index)
          {
            csKMPLogRegister csKmpLogRegister = KMPLogRegisters[index - 1];
            csKmpLogRegister.Records[(int) num4 - 1] = Functions.GetDoubleFromLogData(objGetLogDataAns, csKmpLogRegister.SignEx, pointer1);
            pointer1 += 4;
          }
        }
      }
      this.HandlePortClose(PortOpened);
      return true;
    }

    private static ushort GetUshortFromLogData(csDataFrame objGetLogDataAns, int pointer)
    {
      return csTools.BigEndianBytesToShort(new ArrayList()
      {
        objGetLogDataAns.Data[pointer],
        objGetLogDataAns.Data[pointer + 1]
      });
    }

    private static byte GetFormatFromLogData(csDataFrame objGetLogDataAns, int pointer)
    {
      return Convert.ToByte(objGetLogDataAns.Data[pointer]);
    }

    private static double GetDoubleFromLogData(csDataFrame objGetLogDataAns, byte SignEx, int pointer)
    {
      return csTools.ToFloat(SignEx, new ArrayList()
      {
        objGetLogDataAns.Data[pointer],
        objGetLogDataAns.Data[pointer + 1],
        objGetLogDataAns.Data[pointer + 2],
        objGetLogDataAns.Data[pointer + 3]
      });
    }

    private bool GetHistoricalData(byte DestinationAddress, byte CommandId, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte numberOfDecimals, out byte UnitId, out string ErrorMessage)
    {
      UnitId = (byte) 0;
      numberOfDecimals = (byte) 0;
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(RegisterId));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(StartRecord));
      arrData.Add((object) NumberOfRecords);
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, CommandId, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      csDataFrame csDataFrame = new csDataFrame(data);
      if (!csDataFrame.CRC || csDataFrame.Data.Count < 4)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      if ((int) csTools.BigEndianBytesToShort(new ArrayList()
      {
        csDataFrame.Data[0],
        csDataFrame.Data[1]
      }) != (int) RegisterId)
        return false;
      UnitId = (byte) csDataFrame.Data[2];
      byte num = (byte) csDataFrame.Data[3];
      byte SignExp = (byte) csDataFrame.Data[4];
      int index = 5;
      while (index < csDataFrame.Data.Count)
      {
        arrValues.Add((object) csTools.ToFloat(SignExp, out numberOfDecimals, csDataFrame.Data.GetRange(index, (int) num)));
        index += (int) num;
      }
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool GetHistYearData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte UnitId, out string ErrorMessage)
    {
      byte numberOfDecimals;
      return this.GetHistoricalData(DestinationAddress, (byte) 100, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool GetHistYearData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte numberOfDecimals, out byte UnitId, out string ErrorMessage)
    {
      return this.GetHistoricalData(DestinationAddress, (byte) 100, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool GetHistMonthData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte UnitId, out string ErrorMessage)
    {
      byte numberOfDecimals;
      return this.GetHistoricalData(DestinationAddress, (byte) 101, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool GetHistMonthData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte numberOfDecimals, out byte UnitId, out string ErrorMessage)
    {
      return this.GetHistoricalData(DestinationAddress, (byte) 101, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool GetHistDayData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte UnitId, out string ErrorMessage)
    {
      byte numberOfDecimals;
      return this.GetHistoricalData(DestinationAddress, (byte) 102, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool GetHistDayData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte numberOfDecimals, out byte UnitId, out string ErrorMessage)
    {
      return this.GetHistoricalData(DestinationAddress, (byte) 102, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool GetHistInfoData(byte DestinationAddress, ushort RegisterId, ushort StartRecord, byte NumberOfRecords, ArrayList arrValues, out byte UnitId, out string ErrorMessage)
    {
      byte numberOfDecimals;
      return this.GetHistoricalData(DestinationAddress, (byte) 103, RegisterId, StartRecord, NumberOfRecords, arrValues, out numberOfDecimals, out UnitId, out ErrorMessage);
    }

    public bool SetInA(byte DestinationAddress, uint InA, out string ErrorMessage)
    {
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.IntToBigEndianBytes(InA));
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 113, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool SetInB(byte DestinationAddress, uint InB, out string ErrorMessage)
    {
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.IntToBigEndianBytes(InB));
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 114, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool StartAutoTemp(byte DestinationAddress, byte NumberOfTemps, out string ErrorMessage)
    {
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 116, new ArrayList()
      {
        (object) NumberOfTemps
      }, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool StartAutoInteg(byte DestinationAddress, byte NumberOfIntegrations, out string ErrorMessage)
    {
      ErrorMessage = "";
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 119, new ArrayList()
      {
        (object) NumberOfIntegrations
      }, ControlCharacters.StopByte).GetCommBuffer(), (double) ((int) NumberOfIntegrations * 460 + 800), out ErrorMessage);
      this.HandlePortClose(PortOpened);
      return flag;
    }

    public bool ReadEvent(byte DestinationAddress, out byte Event, out string ErrorMessage)
    {
      Event = (byte) 0;
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 120, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
        return false;
      csDataFrame csDataFrame = new csDataFrame(data);
      if (!csDataFrame.CRC)
        return false;
      Event = (byte) csDataFrame.Data[0];
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool GetHistoricalHourData(byte DestinationAddress, ushort RegisterId, ushort TimeStamp1, ushort TimeStamp2, ArrayList arrValues, out byte UnitId, out string ErrorMessage)
    {
      UnitId = (byte) 0;
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(RegisterId));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(TimeStamp1));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(TimeStamp2));
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 99, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      csDataFrame csDataFrame = new csDataFrame(data);
      if (!csDataFrame.CRC || csDataFrame.Data.Count < 4)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      if ((int) csTools.BigEndianBytesToShort(new ArrayList()
      {
        csDataFrame.Data[0],
        csDataFrame.Data[1]
      }) != (int) RegisterId)
        return false;
      UnitId = (byte) csDataFrame.Data[2];
      byte num = (byte) csDataFrame.Data[3];
      byte SignExp = (byte) csDataFrame.Data[4];
      if ((int) (byte) csDataFrame.Data[5] == (int) byte.MaxValue)
      {
        int index = 6;
        while (index < csDataFrame.Data.Count)
        {
          arrValues.Add((object) csTools.ToFloat(SignExp, csDataFrame.Data.GetRange(index, (int) num)));
          index += (int) num;
        }
      }
      this.HandlePortClose(PortOpened);
      return true;
    }

    public bool GetHistoricalHourDataTimeStamps(byte DestinationAddress, ushort TimeStamp1, ushort TimeStamp2, ArrayList arrValues, out string ErrorMessage)
    {
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData1 = new ArrayList();
      arrData1.AddRange((ICollection) csTools.ShortToBigEndianBytes((ushort) 186));
      arrData1.AddRange((ICollection) csTools.ShortToBigEndianBytes(TimeStamp1));
      arrData1.AddRange((ICollection) csTools.ShortToBigEndianBytes(TimeStamp2));
      ArrayList data1 = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 99, arrData1, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data1 == null)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      csDataFrame csDataFrame1 = new csDataFrame(data1);
      if (!csDataFrame1.CRC || csDataFrame1.Data.Count < 4)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      if ((int) csTools.BigEndianBytesToShort(new ArrayList()
      {
        csDataFrame1.Data[0],
        csDataFrame1.Data[1]
      }) != 186)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      if ((int) (byte) csDataFrame1.Data[5] == 0)
      {
        this.HandlePortClose(PortOpened);
        return true;
      }
      ArrayList arrData2 = new ArrayList();
      arrData2.AddRange((ICollection) csTools.ShortToBigEndianBytes((ushort) 187));
      arrData2.AddRange((ICollection) csTools.ShortToBigEndianBytes(TimeStamp1));
      arrData2.AddRange((ICollection) csTools.ShortToBigEndianBytes(TimeStamp2));
      ArrayList data2 = this.get_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 99, arrData2, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data2 == null)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      csDataFrame csDataFrame2 = new csDataFrame(data2);
      if (!csDataFrame2.CRC || csDataFrame2.Data.Count < 4)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      if ((int) csTools.BigEndianBytesToShort(new ArrayList()
      {
        csDataFrame2.Data[0],
        csDataFrame2.Data[1]
      }) != 187)
      {
        this.HandlePortClose(PortOpened);
        return false;
      }
      if ((int) (byte) csDataFrame2.Data[5] == (int) byte.MaxValue)
      {
        int index = 6;
        while (index < csDataFrame1.Data.Count && index < csDataFrame2.Data.Count)
        {
          int year = 2000 + (int) (byte) csDataFrame1.Data[index];
          int month = (int) (byte) csDataFrame1.Data[index + 1];
          int day = (int) (byte) csDataFrame2.Data[index];
          int hour = (int) (byte) csDataFrame2.Data[index + 1];
          if (day != 0)
          {
            DateTime dateTime = new DateTime(year, month, day, hour, 0, 0);
            arrValues.Add((object) dateTime);
          }
          index += 2;
        }
      }
      this.HandlePortClose(PortOpened);
      return true;
    }
  }
}
