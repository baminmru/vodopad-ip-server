// Decompiled with JetBrains decompiler
// Type: MC601LogView.clsCalculatedRegisters
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace MC601LogView
{
  internal class clsCalculatedRegisters
  {
    public ArrayList ListOfCalculatedRegisters = new ArrayList();

    public void Load(string fileName)
    {
      if (!File.Exists(fileName + ".xml"))
        return;
      Stream serializationStream = (Stream) File.OpenRead(fileName + ".xml");
      if (serializationStream == null)
        return;
      this.ListOfCalculatedRegisters = (ArrayList) new SoapFormatter().Deserialize(serializationStream);
      serializationStream.Close();
    }

    public bool Save(string fileName)
    {
      Stream serializationStream = (Stream) File.OpenWrite(fileName + ".xml");
      if (serializationStream == null)
        return false;
      new SoapFormatter().Serialize(serializationStream,  this.ListOfCalculatedRegisters);
      serializationStream.Close();
      return true;
    }
  }
}
