// Decompiled with JetBrains decompiler
// Type: MC601LogView.DayLogDataSet
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MC601LogView
{
  [XmlRoot("DayLogDataSet")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [DesignerCategory("code")]
  [HelpKeyword("vs.data.DataSet")]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [Serializable]
  public class DayLogDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private DayLogDataSet.RegisterDataTable tableRegister;
    private DayLogDataSet.RegisterUnitDataTable tableRegisterUnit;
    private DayLogDataSet.RegisterInUseDataTable tableRegisterInUse;
    private DayLogDataSet.CustomerNoDataTable tableCustomerNo;

    [DebuggerNonUserCode]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DayLogDataSet.RegisterDataTable Register
    {
      get
      {
        return this.tableRegister;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public DayLogDataSet.RegisterUnitDataTable RegisterUnit
    {
      get
      {
        return this.tableRegisterUnit;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public DayLogDataSet.RegisterInUseDataTable RegisterInUse
    {
      get
      {
        return this.tableRegisterInUse;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    public DayLogDataSet.CustomerNoDataTable CustomerNo
    {
      get
      {
        return this.tableCustomerNo;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DebuggerNonUserCode]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get
      {
        return this._schemaSerializationMode;
      }
      set
      {
        this._schemaSerializationMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
    public new DataTableCollection Tables
    {
      get
      {
        return base.Tables;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    [DebuggerNonUserCode]
    public DayLogDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected DayLogDataSet(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables["Register"] != null)
            base.Tables.Add((DataTable) new DayLogDataSet.RegisterDataTable(dataSet.Tables["Register"]));
          if (dataSet.Tables["RegisterUnit"] != null)
            base.Tables.Add((DataTable) new DayLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
          if (dataSet.Tables["RegisterInUse"] != null)
            base.Tables.Add((DataTable) new DayLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
          if (dataSet.Tables["CustomerNo"] != null)
            base.Tables.Add((DataTable) new DayLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [DebuggerNonUserCode]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    public override DataSet Clone()
    {
      DayLogDataSet dayLogDataSet = (DayLogDataSet) base.Clone();
      dayLogDataSet.InitVars();
      dayLogDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) dayLogDataSet;
    }

    [DebuggerNonUserCode]
    protected override bool ShouldSerializeTables()
    {
      return false;
    }

    [DebuggerNonUserCode]
    protected override bool ShouldSerializeRelations()
    {
      return false;
    }

    [DebuggerNonUserCode]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["Register"] != null)
          base.Tables.Add((DataTable) new DayLogDataSet.RegisterDataTable(dataSet.Tables["Register"]));
        if (dataSet.Tables["RegisterUnit"] != null)
          base.Tables.Add((DataTable) new DayLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
        if (dataSet.Tables["RegisterInUse"] != null)
          base.Tables.Add((DataTable) new DayLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
        if (dataSet.Tables["CustomerNo"] != null)
          base.Tables.Add((DataTable) new DayLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [DebuggerNonUserCode]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [DebuggerNonUserCode]
    internal void InitVars()
    {
      this.InitVars(true);
    }

    [DebuggerNonUserCode]
    internal void InitVars(bool initTable)
    {
      this.tableRegister = (DayLogDataSet.RegisterDataTable) base.Tables["Register"];
      if (initTable && this.tableRegister != null)
        this.tableRegister.InitVars();
      this.tableRegisterUnit = (DayLogDataSet.RegisterUnitDataTable) base.Tables["RegisterUnit"];
      if (initTable && this.tableRegisterUnit != null)
        this.tableRegisterUnit.InitVars();
      this.tableRegisterInUse = (DayLogDataSet.RegisterInUseDataTable) base.Tables["RegisterInUse"];
      if (initTable && this.tableRegisterInUse != null)
        this.tableRegisterInUse.InitVars();
      this.tableCustomerNo = (DayLogDataSet.CustomerNoDataTable) base.Tables["CustomerNo"];
      if (!initTable || this.tableCustomerNo == null)
        return;
      this.tableCustomerNo.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "DayLogDataSet";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/DayLogDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableRegister = new DayLogDataSet.RegisterDataTable();
      base.Tables.Add((DataTable) this.tableRegister);
      this.tableRegisterUnit = new DayLogDataSet.RegisterUnitDataTable();
      base.Tables.Add((DataTable) this.tableRegisterUnit);
      this.tableRegisterInUse = new DayLogDataSet.RegisterInUseDataTable();
      base.Tables.Add((DataTable) this.tableRegisterInUse);
      this.tableCustomerNo = new DayLogDataSet.CustomerNoDataTable();
      base.Tables.Add((DataTable) this.tableCustomerNo);
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegister()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegisterUnit()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegisterInUse()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeCustomerNo()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      DayLogDataSet dayLogDataSet = new DayLogDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = dayLogDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = dayLogDataSet.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
          {
            memoryStream2.SetLength(0L);
            xmlSchema.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return schemaComplexType;
            }
          }
        }
        finally
        {
          if (memoryStream1 != null)
            memoryStream1.Close();
          if (memoryStream2 != null)
            memoryStream2.Close();
        }
      }
      xs.Add(schemaSerializable);
      return schemaComplexType;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class CustomerNoDataTable : DataTable, IEnumerable
    {
      private DataColumn columnCustomerNo;

      [DebuggerNonUserCode]
      public DataColumn CustomerNoColumn
      {
        get
        {
          return this.columnCustomerNo;
        }
      }

      [Browsable(false)]
      [DebuggerNonUserCode]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.CustomerNoRow this[int index]
      {
        get
        {
          return (DayLogDataSet.CustomerNoRow) this.Rows[index];
        }
      }

      public event DayLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanging;

      public event DayLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanged;

      public event DayLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleting;

      public event DayLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleted;

      [DebuggerNonUserCode]
      public CustomerNoDataTable()
      {
        this.TableName = "CustomerNo";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal CustomerNoDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected CustomerNoDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      public string GetCustomerNo()
      {
        if (this.Rows.Count == 0)
          return "";
        return ((DayLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo;
      }

      public void SetCustomerNo(string customerNo)
      {
        if (this.Rows.Count == 0)
          this.AddCustomerNoRow(customerNo);
        else
          ((DayLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo = customerNo;
      }

      [DebuggerNonUserCode]
      public void AddCustomerNoRow(DayLogDataSet.CustomerNoRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.CustomerNoRow AddCustomerNoRow(string CustomerNo)
      {
        DayLogDataSet.CustomerNoRow customerNoRow = (DayLogDataSet.CustomerNoRow) this.NewRow();
        object[] objArray = new object[1]
        {
          (object) CustomerNo
        };
        customerNoRow.ItemArray = objArray;
        this.Rows.Add((DataRow) customerNoRow);
        return customerNoRow;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        DayLogDataSet.CustomerNoDataTable customerNoDataTable = (DayLogDataSet.CustomerNoDataTable) base.Clone();
        customerNoDataTable.InitVars();
        return (DataTable) customerNoDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new DayLogDataSet.CustomerNoDataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnCustomerNo = this.Columns["CustomerNo"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnCustomerNo = new DataColumn("CustomerNo", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCustomerNo);
        this.columnCustomerNo.DefaultValue = (object) " ";
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.CustomerNoRow NewCustomerNoRow()
      {
        return (DayLogDataSet.CustomerNoRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new DayLogDataSet.CustomerNoRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (DayLogDataSet.CustomerNoRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.CustomerNoRowChanged == null)
          return;
        this.CustomerNoRowChanged((object) this, new DayLogDataSet.CustomerNoRowChangeEvent((DayLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.CustomerNoRowChanging == null)
          return;
        this.CustomerNoRowChanging((object) this, new DayLogDataSet.CustomerNoRowChangeEvent((DayLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.CustomerNoRowDeleted == null)
          return;
        this.CustomerNoRowDeleted((object) this, new DayLogDataSet.CustomerNoRowChangeEvent((DayLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.CustomerNoRowDeleting == null)
          return;
        this.CustomerNoRowDeleting((object) this, new DayLogDataSet.CustomerNoRowChangeEvent((DayLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveCustomerNoRow(DayLogDataSet.CustomerNoRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        DayLogDataSet dayLogDataSet = new DayLogDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dayLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "CustomerNoDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dayLogDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    public delegate void RegisterRowChangeEventHandler(object sender, DayLogDataSet.RegisterRowChangeEvent e);

    public delegate void RegisterUnitRowChangeEventHandler(object sender, DayLogDataSet.RegisterUnitRowChangeEvent e);

    public delegate void RegisterInUseRowChangeEventHandler(object sender, DayLogDataSet.RegisterInUseRowChangeEvent e);

    public delegate void CustomerNoRowChangeEventHandler(object sender, DayLogDataSet.CustomerNoRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable]
    public class RegisterDataTable : DataTable, IEnumerable
    {
      private DataColumn columnId;
      private DataColumn columnDate;
      private DataColumn columnE1;
      private DataColumn columnE2;
      private DataColumn columnE3;
      private DataColumn columnE4;
      private DataColumn columnE5;
      private DataColumn columnE6;
      private DataColumn columnE7;
      private DataColumn columnE8;
      private DataColumn columnE9;
      private DataColumn columnT1Avr;
      private DataColumn columnT2Avr;
      private DataColumn columnT3Avr;
      private DataColumn columnP1Avr;
      private DataColumn columnP2Avr;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnINFO;

      [DebuggerNonUserCode]
      public DataColumn IdColumn
      {
        get
        {
          return this.columnId;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn DateColumn
      {
        get
        {
          return this.columnDate;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E1Column
      {
        get
        {
          return this.columnE1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E2Column
      {
        get
        {
          return this.columnE2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E3Column
      {
        get
        {
          return this.columnE3;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E4Column
      {
        get
        {
          return this.columnE4;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E5Column
      {
        get
        {
          return this.columnE5;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E6Column
      {
        get
        {
          return this.columnE6;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E7Column
      {
        get
        {
          return this.columnE7;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E8Column
      {
        get
        {
          return this.columnE8;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E9Column
      {
        get
        {
          return this.columnE9;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T1AvrColumn
      {
        get
        {
          return this.columnT1Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T2AvrColumn
      {
        get
        {
          return this.columnT2Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T3AvrColumn
      {
        get
        {
          return this.columnT3Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P1AvrColumn
      {
        get
        {
          return this.columnP1Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P2AvrColumn
      {
        get
        {
          return this.columnP2Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn V1Column
      {
        get
        {
          return this.columnV1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn V2Column
      {
        get
        {
          return this.columnV2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INAColumn
      {
        get
        {
          return this.columnINA;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INBColumn
      {
        get
        {
          return this.columnINB;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn M1Column
      {
        get
        {
          return this.columnM1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn M2Column
      {
        get
        {
          return this.columnM2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      [Browsable(false)]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterRow this[int index]
      {
        get
        {
          return (DayLogDataSet.RegisterRow) this.Rows[index];
        }
      }

      public event DayLogDataSet.RegisterRowChangeEventHandler RegisterRowChanging;

      public event DayLogDataSet.RegisterRowChangeEventHandler RegisterRowChanged;

      public event DayLogDataSet.RegisterRowChangeEventHandler RegisterRowDeleting;

      public event DayLogDataSet.RegisterRowChangeEventHandler RegisterRowDeleted;

      [DebuggerNonUserCode]
      public RegisterDataTable()
      {
        this.TableName = "Register";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal RegisterDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected RegisterDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddRegisterRow(DayLogDataSet.RegisterRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterRow AddRegisterRow(int Id, DateTime Date, Decimal E1, Decimal E2, Decimal E3, Decimal E4, Decimal E5, Decimal E6, Decimal E7, Decimal E8, Decimal E9, Decimal T1Avr, Decimal T2Avr, Decimal T3Avr, Decimal P1Avr, Decimal P2Avr, Decimal V1, Decimal V2, Decimal INA, Decimal INB, Decimal M1, Decimal M2, Decimal INFO)
      {
        DayLogDataSet.RegisterRow registerRow = (DayLogDataSet.RegisterRow) this.NewRow();
        object[] objArray = new object[23]
        {
          (object) Id,
          (object) Date,
          (object) E1,
          (object) E2,
          (object) E3,
          (object) E4,
          (object) E5,
          (object) E6,
          (object) E7,
          (object) E8,
          (object) E9,
          (object) T1Avr,
          (object) T2Avr,
          (object) T3Avr,
          (object) P1Avr,
          (object) P2Avr,
          (object) V1,
          (object) V2,
          (object) INA,
          (object) INB,
          (object) M1,
          (object) M2,
          (object) INFO
        };
        registerRow.ItemArray = objArray;
        this.Rows.Add((DataRow) registerRow);
        return registerRow;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        DayLogDataSet.RegisterDataTable registerDataTable = (DayLogDataSet.RegisterDataTable) base.Clone();
        registerDataTable.InitVars();
        return (DataTable) registerDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new DayLogDataSet.RegisterDataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnId = this.Columns["Id"];
        this.columnDate = this.Columns["Date"];
        this.columnE1 = this.Columns["E1"];
        this.columnE2 = this.Columns["E2"];
        this.columnE3 = this.Columns["E3"];
        this.columnE4 = this.Columns["E4"];
        this.columnE5 = this.Columns["E5"];
        this.columnE6 = this.Columns["E6"];
        this.columnE7 = this.Columns["E7"];
        this.columnE8 = this.Columns["E8"];
        this.columnE9 = this.Columns["E9"];
        this.columnT1Avr = this.Columns["T1Avr"];
        this.columnT2Avr = this.Columns["T2Avr"];
        this.columnT3Avr = this.Columns["T3Avr"];
        this.columnP1Avr = this.Columns["P1Avr"];
        this.columnP2Avr = this.Columns["P2Avr"];
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnINA = this.Columns["INA"];
        this.columnINB = this.Columns["INB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnINFO = this.Columns["INFO"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnId = new DataColumn("Id", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columnId);
        this.columnDate = new DataColumn("Date", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDate);
        this.columnE1 = new DataColumn("E1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE1);
        this.columnE2 = new DataColumn("E2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE2);
        this.columnE3 = new DataColumn("E3", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE3);
        this.columnE4 = new DataColumn("E4", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE4);
        this.columnE5 = new DataColumn("E5", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE5);
        this.columnE6 = new DataColumn("E6", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE6);
        this.columnE7 = new DataColumn("E7", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE7);
        this.columnE8 = new DataColumn("E8", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE8);
        this.columnE9 = new DataColumn("E9", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE9);
        this.columnT1Avr = new DataColumn("T1Avr", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1Avr);
        this.columnT2Avr = new DataColumn("T2Avr", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT2Avr);
        this.columnT3Avr = new DataColumn("T3Avr", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT3Avr);
        this.columnP1Avr = new DataColumn("P1Avr", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1Avr);
        this.columnP2Avr = new DataColumn("P2Avr", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2Avr);
        this.columnV1 = new DataColumn("V1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV1);
        this.columnV2 = new DataColumn("V2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV2);
        this.columnINA = new DataColumn("INA", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINA);
        this.columnINB = new DataColumn("INB", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINB);
        this.columnM1 = new DataColumn("M1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnM1);
        this.columnM2 = new DataColumn("M2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnM2);
        this.columnINFO = new DataColumn("INFO", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnE1.DefaultValue = (object) new Decimal(0);
        this.columnE2.DefaultValue = (object) new Decimal(0);
        this.columnE3.DefaultValue = (object) new Decimal(0);
        this.columnE4.DefaultValue = (object) new Decimal(0);
        this.columnE5.DefaultValue = (object) new Decimal(0);
        this.columnE6.DefaultValue = (object) new Decimal(0);
        this.columnE7.DefaultValue = (object) new Decimal(0);
        this.columnE8.DefaultValue = (object) new Decimal(0);
        this.columnE9.DefaultValue = (object) new Decimal(0);
        this.columnT1Avr.DefaultValue = (object) new Decimal(0);
        this.columnT2Avr.DefaultValue = (object) new Decimal(0);
        this.columnT3Avr.DefaultValue = (object) new Decimal(0);
        this.columnP1Avr.DefaultValue = (object) new Decimal(0);
        this.columnP2Avr.DefaultValue = (object) new Decimal(0);
        this.columnV1.DefaultValue = (object) new Decimal(0);
        this.columnV2.DefaultValue = (object) new Decimal(0);
        this.columnINA.DefaultValue = (object) new Decimal(0);
        this.columnINB.DefaultValue = (object) new Decimal(0);
        this.columnM1.DefaultValue = (object) new Decimal(0);
        this.columnM2.DefaultValue = (object) new Decimal(0);
        this.columnINFO.DefaultValue = (object) new Decimal(0);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterRow NewRegisterRow()
      {
        return (DayLogDataSet.RegisterRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new DayLogDataSet.RegisterRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (DayLogDataSet.RegisterRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterRowChanged == null)
          return;
        this.RegisterRowChanged((object) this, new DayLogDataSet.RegisterRowChangeEvent((DayLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterRowChanging == null)
          return;
        this.RegisterRowChanging((object) this, new DayLogDataSet.RegisterRowChangeEvent((DayLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterRowDeleted == null)
          return;
        this.RegisterRowDeleted((object) this, new DayLogDataSet.RegisterRowChangeEvent((DayLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterRowDeleting == null)
          return;
        this.RegisterRowDeleting((object) this, new DayLogDataSet.RegisterRowChangeEvent((DayLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterRow(DayLogDataSet.RegisterRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        DayLogDataSet dayLogDataSet = new DayLogDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dayLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dayLogDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [XmlSchemaProvider("GetTypedTableSchema")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable]
    public class RegisterUnitDataTable : DataTable, IEnumerable
    {
      private DataColumn columnE1;
      private DataColumn columnE2;
      private DataColumn columnE3;
      private DataColumn columnE4;
      private DataColumn columnE5;
      private DataColumn columnE6;
      private DataColumn columnE7;
      private DataColumn columnE8;
      private DataColumn columnE9;
      private DataColumn columnT1Avr;
      private DataColumn columnT2Avr;
      private DataColumn columnT3Avr;
      private DataColumn columnP1Avr;
      private DataColumn columnP2Avr;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnINFO;

      [DebuggerNonUserCode]
      public DataColumn E1Column
      {
        get
        {
          return this.columnE1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E2Column
      {
        get
        {
          return this.columnE2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E3Column
      {
        get
        {
          return this.columnE3;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E4Column
      {
        get
        {
          return this.columnE4;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E5Column
      {
        get
        {
          return this.columnE5;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E6Column
      {
        get
        {
          return this.columnE6;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E7Column
      {
        get
        {
          return this.columnE7;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E8Column
      {
        get
        {
          return this.columnE8;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E9Column
      {
        get
        {
          return this.columnE9;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T1AvrColumn
      {
        get
        {
          return this.columnT1Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T2AvrColumn
      {
        get
        {
          return this.columnT2Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T3AvrColumn
      {
        get
        {
          return this.columnT3Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P1AvrColumn
      {
        get
        {
          return this.columnP1Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P2AvrColumn
      {
        get
        {
          return this.columnP2Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn V1Column
      {
        get
        {
          return this.columnV1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn V2Column
      {
        get
        {
          return this.columnV2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INAColumn
      {
        get
        {
          return this.columnINA;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INBColumn
      {
        get
        {
          return this.columnINB;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn M1Column
      {
        get
        {
          return this.columnM1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn M2Column
      {
        get
        {
          return this.columnM2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      [Browsable(false)]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterUnitRow this[int index]
      {
        get
        {
          return (DayLogDataSet.RegisterUnitRow) this.Rows[index];
        }
      }

      public event DayLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanging;

      public event DayLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanged;

      public event DayLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleting;

      public event DayLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleted;

      [DebuggerNonUserCode]
      public RegisterUnitDataTable()
      {
        this.TableName = "RegisterUnit";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal RegisterUnitDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected RegisterUnitDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddRegisterUnitRow(DayLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterUnitRow AddRegisterUnitRow(byte E1, byte E2, byte E3, byte E4, byte E5, byte E6, byte E7, byte E8, byte E9, byte T1Avr, byte T2Avr, byte T3Avr, byte P1Avr, byte P2Avr, byte V1, byte V2, byte INA, byte INB, byte M1, byte M2, byte INFO)
      {
        DayLogDataSet.RegisterUnitRow registerUnitRow = (DayLogDataSet.RegisterUnitRow) this.NewRow();
        object[] objArray = new object[21]
        {
          (object) E1,
          (object) E2,
          (object) E3,
          (object) E4,
          (object) E5,
          (object) E6,
          (object) E7,
          (object) E8,
          (object) E9,
          (object) T1Avr,
          (object) T2Avr,
          (object) T3Avr,
          (object) P1Avr,
          (object) P2Avr,
          (object) V1,
          (object) V2,
          (object) INA,
          (object) INB,
          (object) M1,
          (object) M2,
          (object) INFO
        };
        registerUnitRow.ItemArray = objArray;
        this.Rows.Add((DataRow) registerUnitRow);
        return registerUnitRow;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        DayLogDataSet.RegisterUnitDataTable registerUnitDataTable = (DayLogDataSet.RegisterUnitDataTable) base.Clone();
        registerUnitDataTable.InitVars();
        return (DataTable) registerUnitDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new DayLogDataSet.RegisterUnitDataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnE1 = this.Columns["E1"];
        this.columnE2 = this.Columns["E2"];
        this.columnE3 = this.Columns["E3"];
        this.columnE4 = this.Columns["E4"];
        this.columnE5 = this.Columns["E5"];
        this.columnE6 = this.Columns["E6"];
        this.columnE7 = this.Columns["E7"];
        this.columnE8 = this.Columns["E8"];
        this.columnE9 = this.Columns["E9"];
        this.columnT1Avr = this.Columns["T1Avr"];
        this.columnT2Avr = this.Columns["T2Avr"];
        this.columnT3Avr = this.Columns["T3Avr"];
        this.columnP1Avr = this.Columns["P1Avr"];
        this.columnP2Avr = this.Columns["P2Avr"];
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnINA = this.Columns["INA"];
        this.columnINB = this.Columns["INB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnINFO = this.Columns["INFO"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnE1 = new DataColumn("E1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE1);
        this.columnE2 = new DataColumn("E2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE2);
        this.columnE3 = new DataColumn("E3", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE3);
        this.columnE4 = new DataColumn("E4", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE4);
        this.columnE5 = new DataColumn("E5", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE5);
        this.columnE6 = new DataColumn("E6", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE6);
        this.columnE7 = new DataColumn("E7", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE7);
        this.columnE8 = new DataColumn("E8", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE8);
        this.columnE9 = new DataColumn("E9", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE9);
        this.columnT1Avr = new DataColumn("T1Avr", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1Avr);
        this.columnT2Avr = new DataColumn("T2Avr", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT2Avr);
        this.columnT3Avr = new DataColumn("T3Avr", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT3Avr);
        this.columnP1Avr = new DataColumn("P1Avr", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1Avr);
        this.columnP2Avr = new DataColumn("P2Avr", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2Avr);
        this.columnV1 = new DataColumn("V1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV1);
        this.columnV2 = new DataColumn("V2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV2);
        this.columnINA = new DataColumn("INA", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINA);
        this.columnINB = new DataColumn("INB", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINB);
        this.columnM1 = new DataColumn("M1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnM1);
        this.columnM2 = new DataColumn("M2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnM2);
        this.columnINFO = new DataColumn("INFO", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterUnitRow NewRegisterUnitRow()
      {
        return (DayLogDataSet.RegisterUnitRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new DayLogDataSet.RegisterUnitRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (DayLogDataSet.RegisterUnitRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterUnitRowChanged == null)
          return;
        this.RegisterUnitRowChanged((object) this, new DayLogDataSet.RegisterUnitRowChangeEvent((DayLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterUnitRowChanging == null)
          return;
        this.RegisterUnitRowChanging((object) this, new DayLogDataSet.RegisterUnitRowChangeEvent((DayLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterUnitRowDeleted == null)
          return;
        this.RegisterUnitRowDeleted((object) this, new DayLogDataSet.RegisterUnitRowChangeEvent((DayLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterUnitRowDeleting == null)
          return;
        this.RegisterUnitRowDeleting((object) this, new DayLogDataSet.RegisterUnitRowChangeEvent((DayLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterUnitRow(DayLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        DayLogDataSet dayLogDataSet = new DayLogDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dayLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterUnitDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dayLogDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class RegisterInUseDataTable : DataTable, IEnumerable
    {
      private DataColumn columnE1;
      private DataColumn columnE2;
      private DataColumn columnE3;
      private DataColumn columnE4;
      private DataColumn columnE5;
      private DataColumn columnE6;
      private DataColumn columnE7;
      private DataColumn columnE8;
      private DataColumn columnE9;
      private DataColumn columnT1Avr;
      private DataColumn columnT2Avr;
      private DataColumn columnT3Avr;
      private DataColumn columnP1Avr;
      private DataColumn columnP2Avr;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnINFO;

      [DebuggerNonUserCode]
      public DataColumn E1Column
      {
        get
        {
          return this.columnE1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E2Column
      {
        get
        {
          return this.columnE2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E3Column
      {
        get
        {
          return this.columnE3;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E4Column
      {
        get
        {
          return this.columnE4;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E5Column
      {
        get
        {
          return this.columnE5;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E6Column
      {
        get
        {
          return this.columnE6;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E7Column
      {
        get
        {
          return this.columnE7;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E8Column
      {
        get
        {
          return this.columnE8;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn E9Column
      {
        get
        {
          return this.columnE9;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T1AvrColumn
      {
        get
        {
          return this.columnT1Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T2AvrColumn
      {
        get
        {
          return this.columnT2Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T3AvrColumn
      {
        get
        {
          return this.columnT3Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P1AvrColumn
      {
        get
        {
          return this.columnP1Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P2AvrColumn
      {
        get
        {
          return this.columnP2Avr;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn V1Column
      {
        get
        {
          return this.columnV1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn V2Column
      {
        get
        {
          return this.columnV2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INAColumn
      {
        get
        {
          return this.columnINA;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INBColumn
      {
        get
        {
          return this.columnINB;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn M1Column
      {
        get
        {
          return this.columnM1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn M2Column
      {
        get
        {
          return this.columnM2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [Browsable(false)]
      [DebuggerNonUserCode]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterInUseRow this[int index]
      {
        get
        {
          return (DayLogDataSet.RegisterInUseRow) this.Rows[index];
        }
      }

      public event DayLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanging;

      public event DayLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanged;

      public event DayLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleting;

      public event DayLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleted;

      [DebuggerNonUserCode]
      public RegisterInUseDataTable()
      {
        this.TableName = "RegisterInUse";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal RegisterInUseDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected RegisterInUseDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddRegisterInUseRow(DayLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterInUseRow AddRegisterInUseRow(bool E1, bool E2, bool E3, bool E4, bool E5, bool E6, bool E7, bool E8, bool E9, bool T1Avr, bool T2Avr, bool T3Avr, bool P1Avr, bool P2Avr, bool V1, bool V2, bool INA, bool INB, bool M1, bool M2, bool INFO)
      {
        DayLogDataSet.RegisterInUseRow registerInUseRow = (DayLogDataSet.RegisterInUseRow) this.NewRow();
        object[] objArray = new object[21]
        {
          (object) (bool) (E1 ? 1 : 0),
          (object) (bool) (E2 ? 1 : 0),
          (object) (bool) (E3 ? 1 : 0),
          (object) (bool) (E4 ? 1 : 0),
          (object) (bool) (E5 ? 1 : 0),
          (object) (bool) (E6 ? 1 : 0),
          (object) (bool) (E7 ? 1 : 0),
          (object) (bool) (E8 ? 1 : 0),
          (object) (bool) (E9 ? 1 : 0),
          (object) (bool) (T1Avr ? 1 : 0),
          (object) (bool) (T2Avr ? 1 : 0),
          (object) (bool) (T3Avr ? 1 : 0),
          (object) (bool) (P1Avr ? 1 : 0),
          (object) (bool) (P2Avr ? 1 : 0),
          (object) (bool) (V1 ? 1 : 0),
          (object) (bool) (V2 ? 1 : 0),
          (object) (bool) (INA ? 1 : 0),
          (object) (bool) (INB ? 1 : 0),
          (object) (bool) (M1 ? 1 : 0),
          (object) (bool) (M2 ? 1 : 0),
          (object) (bool) (INFO ? 1 : 0)
        };
        registerInUseRow.ItemArray = objArray;
        this.Rows.Add((DataRow) registerInUseRow);
        return registerInUseRow;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        DayLogDataSet.RegisterInUseDataTable registerInUseDataTable = (DayLogDataSet.RegisterInUseDataTable) base.Clone();
        registerInUseDataTable.InitVars();
        return (DataTable) registerInUseDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new DayLogDataSet.RegisterInUseDataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnE1 = this.Columns["E1"];
        this.columnE2 = this.Columns["E2"];
        this.columnE3 = this.Columns["E3"];
        this.columnE4 = this.Columns["E4"];
        this.columnE5 = this.Columns["E5"];
        this.columnE6 = this.Columns["E6"];
        this.columnE7 = this.Columns["E7"];
        this.columnE8 = this.Columns["E8"];
        this.columnE9 = this.Columns["E9"];
        this.columnT1Avr = this.Columns["T1Avr"];
        this.columnT2Avr = this.Columns["T2Avr"];
        this.columnT3Avr = this.Columns["T3Avr"];
        this.columnP1Avr = this.Columns["P1Avr"];
        this.columnP2Avr = this.Columns["P2Avr"];
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnINA = this.Columns["INA"];
        this.columnINB = this.Columns["INB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnINFO = this.Columns["INFO"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnE1 = new DataColumn("E1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE1);
        this.columnE2 = new DataColumn("E2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE2);
        this.columnE3 = new DataColumn("E3", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE3);
        this.columnE4 = new DataColumn("E4", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE4);
        this.columnE5 = new DataColumn("E5", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE5);
        this.columnE6 = new DataColumn("E6", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE6);
        this.columnE7 = new DataColumn("E7", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE7);
        this.columnE8 = new DataColumn("E8", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE8);
        this.columnE9 = new DataColumn("E9", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnE9);
        this.columnT1Avr = new DataColumn("T1Avr", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1Avr);
        this.columnT2Avr = new DataColumn("T2Avr", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT2Avr);
        this.columnT3Avr = new DataColumn("T3Avr", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT3Avr);
        this.columnP1Avr = new DataColumn("P1Avr", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1Avr);
        this.columnP2Avr = new DataColumn("P2Avr", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2Avr);
        this.columnV1 = new DataColumn("V1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV1);
        this.columnV2 = new DataColumn("V2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV2);
        this.columnINA = new DataColumn("INA", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINA);
        this.columnINB = new DataColumn("INB", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINB);
        this.columnM1 = new DataColumn("M1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnM1);
        this.columnM2 = new DataColumn("M2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnM2);
        this.columnINFO = new DataColumn("INFO", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterInUseRow NewRegisterInUseRow()
      {
        return (DayLogDataSet.RegisterInUseRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new DayLogDataSet.RegisterInUseRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (DayLogDataSet.RegisterInUseRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterInUseRowChanged == null)
          return;
        this.RegisterInUseRowChanged((object) this, new DayLogDataSet.RegisterInUseRowChangeEvent((DayLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterInUseRowChanging == null)
          return;
        this.RegisterInUseRowChanging((object) this, new DayLogDataSet.RegisterInUseRowChangeEvent((DayLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterInUseRowDeleted == null)
          return;
        this.RegisterInUseRowDeleted((object) this, new DayLogDataSet.RegisterInUseRowChangeEvent((DayLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterInUseRowDeleting == null)
          return;
        this.RegisterInUseRowDeleting((object) this, new DayLogDataSet.RegisterInUseRowChangeEvent((DayLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterInUseRow(DayLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        DayLogDataSet dayLogDataSet = new DayLogDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dayLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterInUseDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dayLogDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterRow : DataRow
    {
      private DayLogDataSet.RegisterDataTable tableRegister;

      [DebuggerNonUserCode]
      public int Id
      {
        get
        {
          try
          {
            return (int) this[this.tableRegister.IdColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Id' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.IdColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime Date
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableRegister.DateColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Date' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.DateColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E1' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E1Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E2' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E3
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E3' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E3Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E4
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E4' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E4Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E5
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E5Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E5' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E5Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E6
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E6Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E6' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E6Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E7
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E7Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E7' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E7Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E8
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E8Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E8' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E8Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E9
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.E9Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E9' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.E9Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T1Avr
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.T1AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1Avr' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.T1AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T2Avr
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.T2AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T2Avr' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.T2AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T3Avr
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.T3AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T3Avr' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.T3AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal P1Avr
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.P1AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P1Avr' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.P1AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal P2Avr
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.P2AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P2Avr' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.P2AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal V1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.V1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V1' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.V1Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal V2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.V2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V2' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.V2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal INA
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.INAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INA' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.INAColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal INB
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.INBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INB' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.INBColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal M1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.M1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M1' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.M1Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal M2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.M2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M2' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.M2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal INFO
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.INFOColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INFO' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.INFOColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal RegisterRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegister = (DayLogDataSet.RegisterDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsIdNull()
      {
        return this.IsNull(this.tableRegister.IdColumn);
      }

      [DebuggerNonUserCode]
      public void SetIdNull()
      {
        this[this.tableRegister.IdColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsDateNull()
      {
        return this.IsNull(this.tableRegister.DateColumn);
      }

      [DebuggerNonUserCode]
      public void SetDateNull()
      {
        this[this.tableRegister.DateColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE1Null()
      {
        return this.IsNull(this.tableRegister.E1Column);
      }

      [DebuggerNonUserCode]
      public void SetE1Null()
      {
        this[this.tableRegister.E1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE2Null()
      {
        return this.IsNull(this.tableRegister.E2Column);
      }

      [DebuggerNonUserCode]
      public void SetE2Null()
      {
        this[this.tableRegister.E2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE3Null()
      {
        return this.IsNull(this.tableRegister.E3Column);
      }

      [DebuggerNonUserCode]
      public void SetE3Null()
      {
        this[this.tableRegister.E3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE4Null()
      {
        return this.IsNull(this.tableRegister.E4Column);
      }

      [DebuggerNonUserCode]
      public void SetE4Null()
      {
        this[this.tableRegister.E4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE5Null()
      {
        return this.IsNull(this.tableRegister.E5Column);
      }

      [DebuggerNonUserCode]
      public void SetE5Null()
      {
        this[this.tableRegister.E5Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE6Null()
      {
        return this.IsNull(this.tableRegister.E6Column);
      }

      [DebuggerNonUserCode]
      public void SetE6Null()
      {
        this[this.tableRegister.E6Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE7Null()
      {
        return this.IsNull(this.tableRegister.E7Column);
      }

      [DebuggerNonUserCode]
      public void SetE7Null()
      {
        this[this.tableRegister.E7Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE8Null()
      {
        return this.IsNull(this.tableRegister.E8Column);
      }

      [DebuggerNonUserCode]
      public void SetE8Null()
      {
        this[this.tableRegister.E8Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE9Null()
      {
        return this.IsNull(this.tableRegister.E9Column);
      }

      [DebuggerNonUserCode]
      public void SetE9Null()
      {
        this[this.tableRegister.E9Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1AvrNull()
      {
        return this.IsNull(this.tableRegister.T1AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT1AvrNull()
      {
        this[this.tableRegister.T1AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT2AvrNull()
      {
        return this.IsNull(this.tableRegister.T2AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT2AvrNull()
      {
        this[this.tableRegister.T2AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT3AvrNull()
      {
        return this.IsNull(this.tableRegister.T3AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT3AvrNull()
      {
        this[this.tableRegister.T3AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP1AvrNull()
      {
        return this.IsNull(this.tableRegister.P1AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetP1AvrNull()
      {
        this[this.tableRegister.P1AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP2AvrNull()
      {
        return this.IsNull(this.tableRegister.P2AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetP2AvrNull()
      {
        this[this.tableRegister.P2AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV1Null()
      {
        return this.IsNull(this.tableRegister.V1Column);
      }

      [DebuggerNonUserCode]
      public void SetV1Null()
      {
        this[this.tableRegister.V1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV2Null()
      {
        return this.IsNull(this.tableRegister.V2Column);
      }

      [DebuggerNonUserCode]
      public void SetV2Null()
      {
        this[this.tableRegister.V2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINANull()
      {
        return this.IsNull(this.tableRegister.INAColumn);
      }

      [DebuggerNonUserCode]
      public void SetINANull()
      {
        this[this.tableRegister.INAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINBNull()
      {
        return this.IsNull(this.tableRegister.INBColumn);
      }

      [DebuggerNonUserCode]
      public void SetINBNull()
      {
        this[this.tableRegister.INBColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM1Null()
      {
        return this.IsNull(this.tableRegister.M1Column);
      }

      [DebuggerNonUserCode]
      public void SetM1Null()
      {
        this[this.tableRegister.M1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM2Null()
      {
        return this.IsNull(this.tableRegister.M2Column);
      }

      [DebuggerNonUserCode]
      public void SetM2Null()
      {
        this[this.tableRegister.M2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegister.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegister.INFOColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterUnitRow : DataRow
    {
      private DayLogDataSet.RegisterUnitDataTable tableRegisterUnit;

      [DebuggerNonUserCode]
      public byte E1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E1Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E3
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E3' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E3Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E4
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E4' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E4Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E5
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E5Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E5' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E5Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E6
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E6Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E6' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E6Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E7
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E7Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E7' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E7Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E8
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E8Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E8' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E8Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte E9
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.E9Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E9' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.E9Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte T1Avr
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T1AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1Avr' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T1AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte T2Avr
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T2AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T2Avr' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T2AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte T3Avr
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T3AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T3Avr' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T3AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte P1Avr
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.P1AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P1Avr' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.P1AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte P2Avr
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.P2AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P2Avr' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.P2AvrColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte V1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.V1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.V1Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte V2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.V2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.V2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte INA
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.INAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INA' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.INAColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte INB
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.INBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INB' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.INBColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte M1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.M1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.M1Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte M2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.M2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.M2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte INFO
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.INFOColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INFO' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.INFOColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal RegisterUnitRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterUnit = (DayLogDataSet.RegisterUnitDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsE1Null()
      {
        return this.IsNull(this.tableRegisterUnit.E1Column);
      }

      [DebuggerNonUserCode]
      public void SetE1Null()
      {
        this[this.tableRegisterUnit.E1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE2Null()
      {
        return this.IsNull(this.tableRegisterUnit.E2Column);
      }

      [DebuggerNonUserCode]
      public void SetE2Null()
      {
        this[this.tableRegisterUnit.E2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE3Null()
      {
        return this.IsNull(this.tableRegisterUnit.E3Column);
      }

      [DebuggerNonUserCode]
      public void SetE3Null()
      {
        this[this.tableRegisterUnit.E3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE4Null()
      {
        return this.IsNull(this.tableRegisterUnit.E4Column);
      }

      [DebuggerNonUserCode]
      public void SetE4Null()
      {
        this[this.tableRegisterUnit.E4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE5Null()
      {
        return this.IsNull(this.tableRegisterUnit.E5Column);
      }

      [DebuggerNonUserCode]
      public void SetE5Null()
      {
        this[this.tableRegisterUnit.E5Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE6Null()
      {
        return this.IsNull(this.tableRegisterUnit.E6Column);
      }

      [DebuggerNonUserCode]
      public void SetE6Null()
      {
        this[this.tableRegisterUnit.E6Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE7Null()
      {
        return this.IsNull(this.tableRegisterUnit.E7Column);
      }

      [DebuggerNonUserCode]
      public void SetE7Null()
      {
        this[this.tableRegisterUnit.E7Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE8Null()
      {
        return this.IsNull(this.tableRegisterUnit.E8Column);
      }

      [DebuggerNonUserCode]
      public void SetE8Null()
      {
        this[this.tableRegisterUnit.E8Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE9Null()
      {
        return this.IsNull(this.tableRegisterUnit.E9Column);
      }

      [DebuggerNonUserCode]
      public void SetE9Null()
      {
        this[this.tableRegisterUnit.E9Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1AvrNull()
      {
        return this.IsNull(this.tableRegisterUnit.T1AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT1AvrNull()
      {
        this[this.tableRegisterUnit.T1AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT2AvrNull()
      {
        return this.IsNull(this.tableRegisterUnit.T2AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT2AvrNull()
      {
        this[this.tableRegisterUnit.T2AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT3AvrNull()
      {
        return this.IsNull(this.tableRegisterUnit.T3AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT3AvrNull()
      {
        this[this.tableRegisterUnit.T3AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP1AvrNull()
      {
        return this.IsNull(this.tableRegisterUnit.P1AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetP1AvrNull()
      {
        this[this.tableRegisterUnit.P1AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP2AvrNull()
      {
        return this.IsNull(this.tableRegisterUnit.P2AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetP2AvrNull()
      {
        this[this.tableRegisterUnit.P2AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV1Null()
      {
        return this.IsNull(this.tableRegisterUnit.V1Column);
      }

      [DebuggerNonUserCode]
      public void SetV1Null()
      {
        this[this.tableRegisterUnit.V1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV2Null()
      {
        return this.IsNull(this.tableRegisterUnit.V2Column);
      }

      [DebuggerNonUserCode]
      public void SetV2Null()
      {
        this[this.tableRegisterUnit.V2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINANull()
      {
        return this.IsNull(this.tableRegisterUnit.INAColumn);
      }

      [DebuggerNonUserCode]
      public void SetINANull()
      {
        this[this.tableRegisterUnit.INAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINBNull()
      {
        return this.IsNull(this.tableRegisterUnit.INBColumn);
      }

      [DebuggerNonUserCode]
      public void SetINBNull()
      {
        this[this.tableRegisterUnit.INBColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM1Null()
      {
        return this.IsNull(this.tableRegisterUnit.M1Column);
      }

      [DebuggerNonUserCode]
      public void SetM1Null()
      {
        this[this.tableRegisterUnit.M1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM2Null()
      {
        return this.IsNull(this.tableRegisterUnit.M2Column);
      }

      [DebuggerNonUserCode]
      public void SetM2Null()
      {
        this[this.tableRegisterUnit.M2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegisterUnit.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegisterUnit.INFOColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRow : DataRow
    {
      private DayLogDataSet.RegisterInUseDataTable tableRegisterInUse;

      [DebuggerNonUserCode]
      public bool E1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E1Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E2Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E3
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E3' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E3Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E4
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E4' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E4Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E5
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E5Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E5' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E5Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E6
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E6Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E6' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E6Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E7
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E7Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E7' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E7Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E8
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E8Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E8' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E8Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool E9
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.E9Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E9' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.E9Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool T1Avr
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T1AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1Avr' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T1AvrColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool T2Avr
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T2AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T2Avr' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T2AvrColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool T3Avr
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T3AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T3Avr' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T3AvrColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool P1Avr
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.P1AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P1Avr' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.P1AvrColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool P2Avr
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.P2AvrColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P2Avr' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.P2AvrColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool V1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.V1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.V1Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool V2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.V2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.V2Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool INA
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.INAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INA' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.INAColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool INB
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.INBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INB' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.INBColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool M1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.M1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.M1Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool M2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.M2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.M2Column] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool INFO
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.INFOColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INFO' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.INFOColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      internal RegisterInUseRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterInUse = (DayLogDataSet.RegisterInUseDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsE1Null()
      {
        return this.IsNull(this.tableRegisterInUse.E1Column);
      }

      [DebuggerNonUserCode]
      public void SetE1Null()
      {
        this[this.tableRegisterInUse.E1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE2Null()
      {
        return this.IsNull(this.tableRegisterInUse.E2Column);
      }

      [DebuggerNonUserCode]
      public void SetE2Null()
      {
        this[this.tableRegisterInUse.E2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE3Null()
      {
        return this.IsNull(this.tableRegisterInUse.E3Column);
      }

      [DebuggerNonUserCode]
      public void SetE3Null()
      {
        this[this.tableRegisterInUse.E3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE4Null()
      {
        return this.IsNull(this.tableRegisterInUse.E4Column);
      }

      [DebuggerNonUserCode]
      public void SetE4Null()
      {
        this[this.tableRegisterInUse.E4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE5Null()
      {
        return this.IsNull(this.tableRegisterInUse.E5Column);
      }

      [DebuggerNonUserCode]
      public void SetE5Null()
      {
        this[this.tableRegisterInUse.E5Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE6Null()
      {
        return this.IsNull(this.tableRegisterInUse.E6Column);
      }

      [DebuggerNonUserCode]
      public void SetE6Null()
      {
        this[this.tableRegisterInUse.E6Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE7Null()
      {
        return this.IsNull(this.tableRegisterInUse.E7Column);
      }

      [DebuggerNonUserCode]
      public void SetE7Null()
      {
        this[this.tableRegisterInUse.E7Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE8Null()
      {
        return this.IsNull(this.tableRegisterInUse.E8Column);
      }

      [DebuggerNonUserCode]
      public void SetE8Null()
      {
        this[this.tableRegisterInUse.E8Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE9Null()
      {
        return this.IsNull(this.tableRegisterInUse.E9Column);
      }

      [DebuggerNonUserCode]
      public void SetE9Null()
      {
        this[this.tableRegisterInUse.E9Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1AvrNull()
      {
        return this.IsNull(this.tableRegisterInUse.T1AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT1AvrNull()
      {
        this[this.tableRegisterInUse.T1AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT2AvrNull()
      {
        return this.IsNull(this.tableRegisterInUse.T2AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT2AvrNull()
      {
        this[this.tableRegisterInUse.T2AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT3AvrNull()
      {
        return this.IsNull(this.tableRegisterInUse.T3AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetT3AvrNull()
      {
        this[this.tableRegisterInUse.T3AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP1AvrNull()
      {
        return this.IsNull(this.tableRegisterInUse.P1AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetP1AvrNull()
      {
        this[this.tableRegisterInUse.P1AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP2AvrNull()
      {
        return this.IsNull(this.tableRegisterInUse.P2AvrColumn);
      }

      [DebuggerNonUserCode]
      public void SetP2AvrNull()
      {
        this[this.tableRegisterInUse.P2AvrColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV1Null()
      {
        return this.IsNull(this.tableRegisterInUse.V1Column);
      }

      [DebuggerNonUserCode]
      public void SetV1Null()
      {
        this[this.tableRegisterInUse.V1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV2Null()
      {
        return this.IsNull(this.tableRegisterInUse.V2Column);
      }

      [DebuggerNonUserCode]
      public void SetV2Null()
      {
        this[this.tableRegisterInUse.V2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINANull()
      {
        return this.IsNull(this.tableRegisterInUse.INAColumn);
      }

      [DebuggerNonUserCode]
      public void SetINANull()
      {
        this[this.tableRegisterInUse.INAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINBNull()
      {
        return this.IsNull(this.tableRegisterInUse.INBColumn);
      }

      [DebuggerNonUserCode]
      public void SetINBNull()
      {
        this[this.tableRegisterInUse.INBColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM1Null()
      {
        return this.IsNull(this.tableRegisterInUse.M1Column);
      }

      [DebuggerNonUserCode]
      public void SetM1Null()
      {
        this[this.tableRegisterInUse.M1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM2Null()
      {
        return this.IsNull(this.tableRegisterInUse.M2Column);
      }

      [DebuggerNonUserCode]
      public void SetM2Null()
      {
        this[this.tableRegisterInUse.M2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegisterInUse.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegisterInUse.INFOColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRow : DataRow
    {
      private DayLogDataSet.CustomerNoDataTable tableCustomerNo;

      [DebuggerNonUserCode]
      public string CustomerNo
      {
        get
        {
          if (this.IsCustomerNoNull())
            return string.Empty;
          return (string) this[this.tableCustomerNo.CustomerNoColumn];
        }
        set
        {
          this[this.tableCustomerNo.CustomerNoColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal CustomerNoRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableCustomerNo = (DayLogDataSet.CustomerNoDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsCustomerNoNull()
      {
        return this.IsNull(this.tableCustomerNo.CustomerNoColumn);
      }

      [DebuggerNonUserCode]
      public void SetCustomerNoNull()
      {
        this[this.tableCustomerNo.CustomerNoColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterRowChangeEvent : EventArgs
    {
      private DayLogDataSet.RegisterRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public RegisterRowChangeEvent(DayLogDataSet.RegisterRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterUnitRowChangeEvent : EventArgs
    {
      private DayLogDataSet.RegisterUnitRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterUnitRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public RegisterUnitRowChangeEvent(DayLogDataSet.RegisterUnitRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRowChangeEvent : EventArgs
    {
      private DayLogDataSet.RegisterInUseRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public DayLogDataSet.RegisterInUseRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public RegisterInUseRowChangeEvent(DayLogDataSet.RegisterInUseRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRowChangeEvent : EventArgs
    {
      private DayLogDataSet.CustomerNoRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public DayLogDataSet.CustomerNoRow Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public CustomerNoRowChangeEvent(DayLogDataSet.CustomerNoRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
