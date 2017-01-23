// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.Protocol.StatusEventArgs
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using System;

namespace Kamstrup.Heat.Protocol
{
  public class StatusEventArgs : EventArgs
  {
    private string m_strJobName;
    private int m_intJobStatus;

    public string JobName
    {
      get
      {
        return this.m_strJobName;
      }
      set
      {
        this.m_strJobName = value;
      }
    }

    public int JobStatus
    {
      get
      {
        return this.m_intJobStatus;
      }
      set
      {
        this.m_intJobStatus = value;
      }
    }
  }
}
