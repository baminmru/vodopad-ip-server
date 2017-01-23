// Decompiled with JetBrains decompiler
// Type: Kamstrup.PTA.HeatCommon
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using System;
using System.Diagnostics;

namespace Kamstrup.PTA
{
  internal class HeatCommon
  {
    public static void Trace(TraceLevel traceLevel, string Appl, string Message)
    {
      string str = DateTime.Now.ToString("hh:mm:ss.fff");
      TraceSwitch traceSwitch = new TraceSwitch("TraceSwitch", "Trace Level.");
      if (traceLevel == TraceLevel.Error)
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceError ? 1 : 0) != 0, str + "  " + Appl + " --- " + Message);
      else if (traceLevel == TraceLevel.Warning)
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceWarning ? 1 : 0) != 0, str + "  " + Appl + " --- " + Message);
      else if (traceLevel == TraceLevel.Info)
      {
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceInfo ? 1 : 0) != 0, str + "  " + Appl + " --- " + Message);
      }
      else
      {
          if (traceLevel != TraceLevel.Verbose)
              return;
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceVerbose ? 1 : 0) != 0, str + "  " + Appl + " --- " + Message);
      }
    }

    public static void Trace(TraceLevel traceLevel, string Appl, string sFunction, string ExceptionMessage, string StackTrace)
    {
      string str = DateTime.Now.ToString("hh:mm:ss.fff");
      TraceSwitch traceSwitch = new TraceSwitch("TraceSwitch", "");
      traceSwitch.Level.ToString();
      if (traceLevel == TraceLevel.Error)
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceError ? 1 : 0) != 0, str + "  " + Appl + "-Function:" + sFunction + "\r\n            Ex:" + ExceptionMessage + "\r\n            ST:" + StackTrace);
      else if (traceLevel == TraceLevel.Warning)
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceWarning ? 1 : 0) != 0, str + "  " + Appl + "-Function:" + sFunction + "\r\n            Ex:" + ExceptionMessage + "\r\n            ST:" + StackTrace);
      else if (traceLevel == TraceLevel.Info)
      {
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceInfo ? 1 : 0) != 0, str + "  " + Appl + "-Function:" + sFunction + "\r\n            Ex:" + ExceptionMessage + "\r\n            ST:" + StackTrace);
      }
      else
      {
          if (traceLevel != TraceLevel.Verbose)
              return;
          System.Diagnostics.Trace.WriteLineIf((traceSwitch.TraceVerbose ? 1 : 0) != 0, str + "  " + Appl + "-Function:" + sFunction + "\r\n            Ex:" + ExceptionMessage + "\r\n            ST:" + StackTrace);
      }
    }
  }
}
