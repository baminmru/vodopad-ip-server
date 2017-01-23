// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.Protocol.csCommands
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using Kamstrup.Heat;
using Kamstrup.Heat.mc601Communication;
using Kamstrup.PTA;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;
using System.Threading;
using System.Timers;

namespace Kamstrup.Heat.Protocol
{
  public class csCommands
  {
    private const int m_intRetries = 4;
    private SerialPort m_objSerialPort;
    private bool m_blnStart;
    private bool m_blnStop;
    private bool m_blnAck;
    private ArrayList m_arrCommBuffer;
    private System.Timers.Timer m_TimerReadInterval;
    private double m_dReadIntervalTimeout;
    private System.Timers.Timer m_TimerStartByte;
    private double m_dStartByteTimeout;
    private Settings m_objSettings;

    public bool Online
    {
      get
      {
        return this.m_objSerialPort.IsOpen;
      }
    }

    public event StatusEventHandler Status;

    public csCommands()
    {
      HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Qualified assembly name: " + typeof (Settings).AssemblyQualifiedName.ToString());
      this.m_objSettings = new Settings();
      if (!File.Exists("MC601Communication.config"))
        this.SaveSettings();
      this.LoadSettings();
      this.m_objSerialPort = new SerialPort(this.m_objSettings.COMM_Port, 1200, Parity.None, 8, StopBits.Two);
      this.m_objSerialPort.DtrEnable = true;
      this.m_objSerialPort.DataReceived += new SerialDataReceivedEventHandler(this.m_objSerialPort_DataReceived);
      this.m_blnStart = false;
      this.m_blnStop = false;
      this.m_blnAck = false;
      this.m_arrCommBuffer = new ArrayList();
      this.m_dReadIntervalTimeout = 300.0;
      this.m_TimerReadInterval = this.TimeOut(this.m_dReadIntervalTimeout);
      this.m_TimerReadInterval.Enabled = false;
      this.m_dStartByteTimeout = 2265.0;
      this.m_TimerStartByte = this.TimeOut(this.m_dStartByteTimeout);
      this.m_TimerStartByte.Enabled = false;
      this.LoadControlCharacters();
    }

    private void m_objSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      SerialPort serialPort = (SerialPort) sender;
      while (serialPort.BytesToRead != 0)
        this.OnRxChar((byte) serialPort.ReadByte());
    }

    public bool SetComPort(string comPort)
    {
      try
      {
        this.m_objSettings.COMM_Port = comPort;
        this.SaveSettings();
        this.m_objSerialPort = new SerialPort(this.m_objSettings.COMM_Port, 1200, Parity.None, 8, StopBits.Two);
        this.m_objSerialPort.DtrEnable = true;
        this.m_objSerialPort.DataReceived += new SerialDataReceivedEventHandler(this.m_objSerialPort_DataReceived);
      }
      catch
      {
        return false;
      }
      return true;
    }

    public string GetComPort()
    {
      return this.m_objSettings.COMM_Port;
    }

    public bool LoadSettings()
    {
      bool flag = true;
      try
      {
        if (File.Exists("MC601Communication.config"))
        {
          FileInfo fileInfo = new FileInfo("MC601Communication.config");
          if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes) 0)
            --fileInfo.Attributes;
          Stream serializationStream = (Stream) File.Open("MC601Communication.config", FileMode.Open);
          this.m_objSettings = (Settings) new SoapFormatter().Deserialize(serializationStream);
          serializationStream.Close();
        }
        else
        {
          HeatCommon.Trace(TraceLevel.Error, "5097057", "Failed to open the MC601Communication.config file.", "", "");
          flag = false;
        }
      }
      catch (Exception ex)
      {
        HeatCommon.Trace(TraceLevel.Error, "5097057", this.ToString() + "." + MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace);
        flag = false;
      }
      return flag;
    }

    public bool SaveSettings()
    {
      bool flag = true;
      try
      {
        Stream serializationStream = (Stream) File.Open("MC601Communication.config", FileMode.Create);
        new SoapFormatter().Serialize(serializationStream,  this.m_objSettings);
        serializationStream.Close();
      }
      catch (Exception ex)
      {
        HeatCommon.Trace(TraceLevel.Error, "5097057", "Failed to save the MC601Communication.config file." + ex.Message);
        flag = false;
      }
      return flag;
    }

    private void LoadControlCharacters()
    {
      ControlCharacters.ACK = this.m_objSettings.ACK;
      ControlCharacters.StopByte = this.m_objSettings.StopByte;
      ControlCharacters.Stuff = this.m_objSettings.Stuff;
      ControlCharacters.RXStart = this.m_objSettings.RXStart;
      ControlCharacters.TXStart = this.m_objSettings.TXStart;
    }

    public bool Open(out string ErrorMessage)
    {
      try
      {
        ErrorMessage = "";
        this.m_objSerialPort.Open();
        return true;
      }
      catch (Exception ex)
      {
        ErrorMessage = ex.Message;
      }
      return false;
    }

    public void Close()
    {
      this.m_objSerialPort.Close();
    }

    protected bool HandlePortOpen(out bool PortOpened, out string ErrorMessage)
    {
      ErrorMessage = "";
      PortOpened = false;
      if (!this.Online)
      {
        PortOpened = this.Open(out ErrorMessage);
        if (!PortOpened)
        {
          HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Open Failed!");
          ErrorMessage = this.ToString() + "." + MethodBase.GetCurrentMethod().Name + " Open Failed!";
          return false;
        }
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, " Open Succeeded!");
      }
      return true;
    }

    protected void HandlePortClose(bool PortOpened)
    {
      if (!PortOpened || !this.Online)
        return;
      this.Close();
      HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Close");
    }

    private bool Set1200Baud()
    {
      this.Close();
      this.m_objSerialPort.BaudRate = 1200;
      HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "");
      string ErrorMessage = "";
      return this.Open(out ErrorMessage);
    }

    public bool Set2400Baud(byte DestinationAddress, out string ErrorMessage)
    {
      bool PortOpened = false;
      if (!this.HandlePortOpen(out PortOpened, out ErrorMessage))
        return false;
      ArrayList arrData = new ArrayList();
      bool flag = this.send_data(new csDataFrame(ControlCharacters.TXStart, DestinationAddress, (byte) 112, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (flag)
      {
        this.Close();
        this.m_objSerialPort.BaudRate = 2400;
        string ErrorMessage1 = "";
        if (!PortOpened)
          flag = this.Open(out ErrorMessage1);
      }
      HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "");
      return flag;
    }

    protected virtual void OnStatus(StatusEventArgs e)
    {
      if (this.Status == null)
        return;
      this.Status( this, e);
    }

    protected ArrayList get_data(ArrayList arrCommBuffer, out string ErrorMessage)
    {
      ErrorMessage = "";
      try
      {
        for (int index = 1; index <= 4; ++index)
        {
          if (index > 2)
            this.Set1200Baud();
          this.m_blnStart = false;
          this.m_blnStop = false;
          this.m_arrCommBuffer.Clear();
          HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Starts!");
          this.m_objSerialPort.Write((byte[]) arrCommBuffer.ToArray(typeof (byte)), 0, arrCommBuffer.Count);
          this.m_TimerStartByte.Enabled = true;
          while (this.m_TimerStartByte.Enabled && !this.m_blnStart)
            Thread.Sleep(10);
          this.m_TimerStartByte.Enabled = false;
          if (!this.m_blnStart)
          {
            HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Failed(STARTBYTE ERROR on " + index.ToString() + ". Retry)!");
            ErrorMessage = this.ToString() + "." + MethodBase.GetCurrentMethod().Name + " Start Byte Timeout(" + index.ToString() + ". Retry)";
          }
          else
          {
            this.m_TimerReadInterval.Enabled = true;
            while (this.m_TimerReadInterval.Enabled && !this.m_blnStop)
              Thread.Sleep(10);
            this.m_TimerReadInterval.Enabled = false;
            if (!this.m_blnStop)
            {
              HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Failed(STOPBYTE ERROR on " + index.ToString() + ". Retry)!");
              ErrorMessage = this.ToString() + "." + MethodBase.GetCurrentMethod().Name + " Read Interval Timeout(" + index.ToString() + ". Retry)";
            }
            else
              break;
          }
        }
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Stops!");
        if (this.m_blnStart && this.m_blnStop)
        {
          ErrorMessage = "";
          return this.m_arrCommBuffer;
        }
      }
      catch (Exception ex)
      {
        ErrorMessage = this.ToString() + "." + MethodBase.GetCurrentMethod().Name + " get_data() Throw an Exception\n";
        ErrorMessage += ex.Message;
      }
      return (ArrayList) null;
    }

    protected bool send_data(ArrayList arrCommBuffer, out string ErrorMessage)
    {
      ErrorMessage = "";
      try
      {
        for (int index = 1; index <= 4; ++index)
        {
          if (index > 2)
            this.Set1200Baud();
          this.m_blnAck = false;
          HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Starts!");
          this.m_objSerialPort.Write((byte[]) arrCommBuffer.ToArray(typeof (byte)), 0, arrCommBuffer.Count);
          this.m_TimerStartByte.Enabled = true;
          while (this.m_TimerStartByte.Enabled && !this.m_blnAck)
            Thread.Sleep(10);
          this.m_TimerStartByte.Enabled = false;
          if (!this.m_blnAck)
          {
            HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Failed!");
            ErrorMessage = this.ToString() + "." + MethodBase.GetCurrentMethod().Name + " Acknowledge Timeout(" + index.ToString() + ". Retry)";
          }
          else
          {
            HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Stops!");
            ErrorMessage = "";
            return true;
          }
        }
      }
      catch (Exception ex)
      {
        ErrorMessage = this.ToString() + "." + MethodBase.GetCurrentMethod().Name + " Throwed an Exception\n";
        ErrorMessage += ex.Message;
      }
      return false;
    }

    protected bool send_data(ArrayList arrCommBuffer, double DelayAfterAck, out string ErrorMessage)
    {
      bool flag = this.send_data(arrCommBuffer, out ErrorMessage);
      if (flag && DelayAfterAck > 0.0)
      {
        System.Timers.Timer timer = new System.Timers.Timer(DelayAfterAck);
        timer.AutoReset = false;
        timer.Enabled = true;
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "DelayAfterAck(" + DelayAfterAck.ToString() + ")");
        while (timer.Enabled)
          Thread.Sleep(10);
      }
      return flag;
    }

    protected bool write_eeprom(byte bytDestinationAddress, ushort shoPassword, ushort shoStartAddress, byte bytNumberOfBytes, ArrayList arrEepromValues, out string ErrorMessage)
    {
      HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Addresses = " + shoStartAddress.ToString() + " - " + ((int) shoStartAddress + (int) bytNumberOfBytes).ToString());
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(shoPassword));
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(shoStartAddress));
      arrData.Add( bytNumberOfBytes);
      arrData.AddRange((ICollection) arrEepromValues);
      return this.send_data(new csDataFrame(ControlCharacters.TXStart, bytDestinationAddress, (byte) 3, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
    }

    protected bool read_eeprom(byte bytDestinationAddress, ushort shoStartAddress, byte bytNumberOfBytes, ref ArrayList arrEeprom, out string ErrorMessage)
    {
      HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Addresses = " + shoStartAddress.ToString() + " - " + ((int) shoStartAddress + (int) bytNumberOfBytes).ToString());
      ArrayList arrData = new ArrayList();
      arrData.AddRange((ICollection) csTools.ShortToBigEndianBytes(shoStartAddress));
      arrData.Add( bytNumberOfBytes);
      ArrayList data = this.get_data(new csDataFrame(ControlCharacters.TXStart, bytDestinationAddress, (byte) 4, arrData, ControlCharacters.StopByte).GetCommBuffer(), out ErrorMessage);
      if (data == null)
        return false;
      csDataFrame csDataFrame = new csDataFrame(data);
      if (!csDataFrame.CRC)
        return false;
      arrEeprom = csDataFrame.Data;
      return true;
    }

    public System.Timers.Timer TimeOut(double dTimeout)
    {
      System.Timers.Timer timer = new System.Timers.Timer(dTimeout);
      timer.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
      timer.AutoReset = false;
      timer.Enabled = true;
      return timer;
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
      System.Timers.Timer timer = (System.Timers.Timer) source;
      if (timer == this.m_TimerReadInterval)
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "ReadInterval Timer event");
      else if (timer == this.m_TimerStartByte)
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "StartByte Timer event");
      else
        HeatCommon.Trace(TraceLevel.Error, this.ToString() + "." + MethodBase.GetCurrentMethod().Name, "Unkonown Timer event");
    }

    protected void OnRxChar(byte ch)
    {
      if ((int) ch == (int) ControlCharacters.RXStart)
      {
        if (!this.m_blnStart)
          this.m_blnStart = true;
        this.m_arrCommBuffer.Clear();
        this.m_blnStop = false;
      }
      if ((int) ch == (int) ControlCharacters.StopByte)
        this.m_blnStop = true;
      if ((int) ch == (int) ControlCharacters.ACK)
      {
        this.m_blnAck = true;
      }
      else
      {
        this.m_arrCommBuffer.Add( ch);
        this.m_TimerReadInterval.Interval = this.m_dReadIntervalTimeout;
      }
    }
  }
}
