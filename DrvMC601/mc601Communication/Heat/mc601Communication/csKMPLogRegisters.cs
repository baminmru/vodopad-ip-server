// Decompiled with JetBrains decompiler
// Type: Kamstrup.Heat.mc601Communication.csKMPLogRegisters
// Assembly: mc601Communication, Version=1.0.2981.15880, Culture=neutral, PublicKeyToken=null
// MVID: BD99FFD8-7BC8-4B6D-B3FD-EC88EF21C46D
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\mc601Communication.dll

using System.Collections;

namespace Kamstrup.Heat.mc601Communication
{
  public class csKMPLogRegisters
  {
    internal ArrayList m_List = new ArrayList();

    public bool IsFixedSize
    {
      get
      {
        return this.m_List.IsFixedSize;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.m_List.IsReadOnly;
      }
    }

    public csKMPLogRegister this[int index]
    {
      get
      {
        return (csKMPLogRegister) this.m_List[index];
      }
      set
      {
        this.m_List[index] =  value;
      }
    }

    public int Count
    {
      get
      {
        return this.m_List.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return this.m_List.IsSynchronized;
      }
    }

    public object SyncRoot
    {
      get
      {
        return this.m_List.SyncRoot;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return this.m_List.GetEnumerator();
    }

    public int Add(csKMPLogRegister value)
    {
      return this.m_List.Add( value);
    }

    public void Clear()
    {
      this.m_List.Clear();
    }

    public bool Contains(csKMPLogRegister value)
    {
      return this.m_List.Contains( value);
    }

    public int IndexOf(csKMPLogRegister value)
    {
      return this.m_List.IndexOf( value);
    }

    public void Insert(int index, csKMPLogRegister value)
    {
      this.m_List.Insert(index,  value);
    }

    public void Remove(csKMPLogRegister value)
    {
      this.m_List.Remove( value);
    }

    public void RemoveAt(int index)
    {
      this.m_List.RemoveAt(index);
    }

    public void CopyTo(csKMPLogRegisters array, int index)
    {
      foreach (csKMPLogRegister csKmpLogRegister in array)
        this.m_List.Insert(index,  csKmpLogRegister);
    }

    public void CopyRecordsTo(csKMPLogRegisters array, int index)
    {
      foreach (csKMPLogRegister csKmpLogRegister in array.m_List)
        this.GetKMPLogRegister(csKmpLogRegister.RegisterId).CopyRecordsTo(csKmpLogRegister.Records);
    }

    public csKMPLogRegister GetKMPLogRegister(ushort RegisterId)
    {
      foreach (csKMPLogRegister csKmpLogRegister in this)
      {
        if ((int) csKmpLogRegister.RegisterId == (int) RegisterId)
          return csKmpLogRegister;
      }
      return (csKMPLogRegister) null;
    }

    public void RemoveAllWithRegister(ushort RegisterId)
    {
      foreach (csKMPLogRegister csKmpLogRegister in this)
      {
        if ((int) csKmpLogRegister.RegisterId == (int) RegisterId)
          this.Remove(csKmpLogRegister);
      }
    }
  }
}
