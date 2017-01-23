// Decompiled with JetBrains decompiler
// Type: MC601LogView.LiveLogDataSet
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
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [XmlRoot("LiveLogDataSet")]
  [HelpKeyword("vs.data.DataSet")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [Serializable]
  public class LiveLogDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private LiveLogDataSet.RegistersDataTable tableRegisters;
    private LiveLogDataSet.RegisterInUseDataTable tableRegisterInUse;
    private LiveLogDataSet.RegisterUnitDataTable tableRegisterUnit;
    private LiveLogDataSet.CustomerNoDataTable tableCustomerNo;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public LiveLogDataSet.RegistersDataTable Registers
    {
      get
      {
        return this.tableRegisters;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public LiveLogDataSet.RegisterInUseDataTable RegisterInUse
    {
      get
      {
        return this.tableRegisterInUse;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public LiveLogDataSet.RegisterUnitDataTable RegisterUnit
    {
      get
      {
        return this.tableRegisterUnit;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public LiveLogDataSet.CustomerNoDataTable CustomerNo
    {
      get
      {
        return this.tableCustomerNo;
      }
    }

    [DebuggerNonUserCode]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    [DebuggerNonUserCode]
    public LiveLogDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected LiveLogDataSet(SerializationInfo info, StreamingContext context)
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
          if (dataSet.Tables["Registers"] != null)
            base.Tables.Add((DataTable) new LiveLogDataSet.RegistersDataTable(dataSet.Tables["Registers"]));
          if (dataSet.Tables["RegisterInUse"] != null)
            base.Tables.Add((DataTable) new LiveLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
          if (dataSet.Tables["RegisterUnit"] != null)
            base.Tables.Add((DataTable) new LiveLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
          if (dataSet.Tables["CustomerNo"] != null)
            base.Tables.Add((DataTable) new LiveLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
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
      LiveLogDataSet liveLogDataSet = (LiveLogDataSet) base.Clone();
      liveLogDataSet.InitVars();
      liveLogDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) liveLogDataSet;
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
        if (dataSet.Tables["Registers"] != null)
          base.Tables.Add((DataTable) new LiveLogDataSet.RegistersDataTable(dataSet.Tables["Registers"]));
        if (dataSet.Tables["RegisterInUse"] != null)
          base.Tables.Add((DataTable) new LiveLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
        if (dataSet.Tables["RegisterUnit"] != null)
          base.Tables.Add((DataTable) new LiveLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
        if (dataSet.Tables["CustomerNo"] != null)
          base.Tables.Add((DataTable) new LiveLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
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
      this.tableRegisters = (LiveLogDataSet.RegistersDataTable) base.Tables["Registers"];
      if (initTable && this.tableRegisters != null)
        this.tableRegisters.InitVars();
      this.tableRegisterInUse = (LiveLogDataSet.RegisterInUseDataTable) base.Tables["RegisterInUse"];
      if (initTable && this.tableRegisterInUse != null)
        this.tableRegisterInUse.InitVars();
      this.tableRegisterUnit = (LiveLogDataSet.RegisterUnitDataTable) base.Tables["RegisterUnit"];
      if (initTable && this.tableRegisterUnit != null)
        this.tableRegisterUnit.InitVars();
      this.tableCustomerNo = (LiveLogDataSet.CustomerNoDataTable) base.Tables["CustomerNo"];
      if (!initTable || this.tableCustomerNo == null)
        return;
      this.tableCustomerNo.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "LiveLogDataSet";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/LiveLogDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableRegisters = new LiveLogDataSet.RegistersDataTable();
      base.Tables.Add((DataTable) this.tableRegisters);
      this.tableRegisterInUse = new LiveLogDataSet.RegisterInUseDataTable();
      base.Tables.Add((DataTable) this.tableRegisterInUse);
      this.tableRegisterUnit = new LiveLogDataSet.RegisterUnitDataTable();
      base.Tables.Add((DataTable) this.tableRegisterUnit);
      this.tableCustomerNo = new LiveLogDataSet.CustomerNoDataTable();
      base.Tables.Add((DataTable) this.tableCustomerNo);
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegisters()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegisterInUse()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegisterUnit()
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
      LiveLogDataSet liveLogDataSet = new LiveLogDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = liveLogDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = liveLogDataSet.GetSchemaSerializable();
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

    public delegate void RegistersRowChangeEventHandler(object sender, LiveLogDataSet.RegistersRowChangeEvent e);

    public delegate void RegisterInUseRowChangeEventHandler(object sender, LiveLogDataSet.RegisterInUseRowChangeEvent e);

    public delegate void RegisterUnitRowChangeEventHandler(object sender, LiveLogDataSet.RegisterUnitRowChangeEvent e);

    public delegate void CustomerNoRowChangeEventHandler(object sender, LiveLogDataSet.CustomerNoRowChangeEvent e);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class RegistersDataTable : DataTable, IEnumerable
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
      private DataColumn columnTA2;
      private DataColumn columnTA3;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnT1;
      private DataColumn columnT2;
      private DataColumn columnT3;
      private DataColumn columnT4;
      private DataColumn columnT1_T2;
      private DataColumn columnP1;
      private DataColumn columnP2;
      private DataColumn columnFLOW1;
      private DataColumn columnFLOW2;
      private DataColumn columnEFFECT1;
      private DataColumn columnHour;

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
      public DataColumn TA2Column
      {
        get
        {
          return this.columnTA2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn TA3Column
      {
        get
        {
          return this.columnTA3;
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
      public DataColumn T1Column
      {
        get
        {
          return this.columnT1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T2Column
      {
        get
        {
          return this.columnT2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T3Column
      {
        get
        {
          return this.columnT3;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T4Column
      {
        get
        {
          return this.columnT4;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T1_T2Column
      {
        get
        {
          return this.columnT1_T2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P1Column
      {
        get
        {
          return this.columnP1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P2Column
      {
        get
        {
          return this.columnP2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn FLOW1Column
      {
        get
        {
          return this.columnFLOW1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn FLOW2Column
      {
        get
        {
          return this.columnFLOW2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn EFFECT1Column
      {
        get
        {
          return this.columnEFFECT1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn HourColumn
      {
        get
        {
          return this.columnHour;
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
      public LiveLogDataSet.RegistersRow this[int index]
      {
        get
        {
          return (LiveLogDataSet.RegistersRow) this.Rows[index];
        }
      }

      public event LiveLogDataSet.RegistersRowChangeEventHandler RegistersRowChanging;

      public event LiveLogDataSet.RegistersRowChangeEventHandler RegistersRowChanged;

      public event LiveLogDataSet.RegistersRowChangeEventHandler RegistersRowDeleting;

      public event LiveLogDataSet.RegistersRowChangeEventHandler RegistersRowDeleted;

      [DebuggerNonUserCode]
      public RegistersDataTable()
      {
        this.TableName = "Registers";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal RegistersDataTable(DataTable table)
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
      protected RegistersDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddRegistersRow(LiveLogDataSet.RegistersRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegistersRow AddRegistersRow(DateTime Date, Decimal E1, Decimal E2, Decimal E3, Decimal E4, Decimal E5, Decimal E6, Decimal E7, Decimal E8, Decimal E9, Decimal TA2, Decimal TA3, Decimal V1, Decimal V2, Decimal INA, Decimal INB, Decimal M1, Decimal M2, Decimal T1, Decimal T2, Decimal T3, Decimal T4, Decimal T1_T2, Decimal P1, Decimal P2, Decimal FLOW1, Decimal FLOW2, Decimal EFFECT1, Decimal Hour)
      {
        LiveLogDataSet.RegistersRow registersRow = (LiveLogDataSet.RegistersRow) this.NewRow();
        object[] objArray = new object[30]
        {
          null,
           Date,
           E1,
           E2,
           E3,
           E4,
           E5,
           E6,
           E7,
           E8,
           E9,
           TA2,
           TA3,
           V1,
           V2,
           INA,
           INB,
           M1,
           M2,
           T1,
           T2,
           T3,
           T4,
           T1_T2,
           P1,
           P2,
           FLOW1,
           FLOW2,
           EFFECT1,
           Hour
        };
        registersRow.ItemArray = objArray;
        this.Rows.Add((DataRow) registersRow);
        return registersRow;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        LiveLogDataSet.RegistersDataTable registersDataTable = (LiveLogDataSet.RegistersDataTable) base.Clone();
        registersDataTable.InitVars();
        return (DataTable) registersDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new LiveLogDataSet.RegistersDataTable();
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
        this.columnTA2 = this.Columns["TA2"];
        this.columnTA3 = this.Columns["TA3"];
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnINA = this.Columns["INA"];
        this.columnINB = this.Columns["INB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnT1 = this.Columns["T1"];
        this.columnT2 = this.Columns["T2"];
        this.columnT3 = this.Columns["T3"];
        this.columnT4 = this.Columns["T4"];
        this.columnT1_T2 = this.Columns["T1_T2"];
        this.columnP1 = this.Columns["P1"];
        this.columnP2 = this.Columns["P2"];
        this.columnFLOW1 = this.Columns["FLOW1"];
        this.columnFLOW2 = this.Columns["FLOW2"];
        this.columnEFFECT1 = this.Columns["EFFECT1"];
        this.columnHour = this.Columns["Hour"];
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
        this.columnTA2 = new DataColumn("TA2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTA2);
        this.columnTA3 = new DataColumn("TA3", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTA3);
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
        this.columnT1 = new DataColumn("T1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1);
        this.columnT2 = new DataColumn("T2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT2);
        this.columnT3 = new DataColumn("T3", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT3);
        this.columnT4 = new DataColumn("T4", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT4);
        this.columnT1_T2 = new DataColumn("T1_T2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1_T2);
        this.columnP1 = new DataColumn("P1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1);
        this.columnP2 = new DataColumn("P2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2);
        this.columnFLOW1 = new DataColumn("FLOW1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW1);
        this.columnFLOW2 = new DataColumn("FLOW2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW2);
        this.columnEFFECT1 = new DataColumn("EFFECT1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnEFFECT1);
        this.columnHour = new DataColumn("Hour", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnHour);
        this.columnId.AutoIncrement = true;
        this.columnId.AutoIncrementSeed = 1L;
        this.columnE1.DefaultValue =  new Decimal(0);
        this.columnE2.DefaultValue =  new Decimal(0);
        this.columnE3.DefaultValue =  new Decimal(0);
        this.columnE4.DefaultValue =  new Decimal(0);
        this.columnE5.DefaultValue =  new Decimal(0);
        this.columnE6.DefaultValue =  new Decimal(0);
        this.columnE7.DefaultValue =  new Decimal(0);
        this.columnE8.DefaultValue =  new Decimal(0);
        this.columnE9.DefaultValue =  new Decimal(0);
        this.columnTA2.DefaultValue =  new Decimal(0);
        this.columnTA3.DefaultValue =  new Decimal(0);
        this.columnV1.DefaultValue =  new Decimal(0);
        this.columnV2.DefaultValue =  new Decimal(0);
        this.columnINA.DefaultValue =  new Decimal(0);
        this.columnINB.DefaultValue =  new Decimal(0);
        this.columnM1.DefaultValue =  new Decimal(0);
        this.columnM2.DefaultValue =  new Decimal(0);
        this.columnT1.DefaultValue =  new Decimal(0);
        this.columnT2.DefaultValue =  new Decimal(0);
        this.columnT3.DefaultValue =  new Decimal(0);
        this.columnT4.DefaultValue =  new Decimal(0);
        this.columnT1_T2.Caption = "T1-T2";
        this.columnT1_T2.DefaultValue =  new Decimal(0);
        this.columnP1.DefaultValue =  new Decimal(0);
        this.columnP2.DefaultValue =  new Decimal(0);
        this.columnFLOW1.Caption = "Flow 1";
        this.columnFLOW1.DefaultValue =  new Decimal(0);
        this.columnFLOW2.Caption = "Flow 2";
        this.columnFLOW2.DefaultValue =  new Decimal(0);
        this.columnEFFECT1.Caption = "Effect 1";
        this.columnEFFECT1.DefaultValue =  new Decimal(0);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegistersRow NewRegistersRow()
      {
        return (LiveLogDataSet.RegistersRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new LiveLogDataSet.RegistersRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (LiveLogDataSet.RegistersRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegistersRowChanged == null)
          return;
        this.RegistersRowChanged( this, new LiveLogDataSet.RegistersRowChangeEvent((LiveLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegistersRowChanging == null)
          return;
        this.RegistersRowChanging( this, new LiveLogDataSet.RegistersRowChangeEvent((LiveLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegistersRowDeleted == null)
          return;
        this.RegistersRowDeleted( this, new LiveLogDataSet.RegistersRowChangeEvent((LiveLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegistersRowDeleting == null)
          return;
        this.RegistersRowDeleting( this, new LiveLogDataSet.RegistersRowChangeEvent((LiveLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegistersRow(LiveLogDataSet.RegistersRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        LiveLogDataSet liveLogDataSet = new LiveLogDataSet();
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
          FixedValue = liveLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegistersDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = liveLogDataSet.GetSchemaSerializable();
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
      private DataColumn columnTA2;
      private DataColumn columnTA3;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnT1;
      private DataColumn columnT2;
      private DataColumn columnT3;
      private DataColumn columnT4;
      private DataColumn columnT1_T2;
      private DataColumn columnP1;
      private DataColumn columnP2;
      private DataColumn columnFLOW1;
      private DataColumn columnFLOW2;
      private DataColumn columnEFFECT1;
      private DataColumn columnHour;

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
      public DataColumn TA2Column
      {
        get
        {
          return this.columnTA2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn TA3Column
      {
        get
        {
          return this.columnTA3;
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
      public DataColumn T1Column
      {
        get
        {
          return this.columnT1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T2Column
      {
        get
        {
          return this.columnT2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T3Column
      {
        get
        {
          return this.columnT3;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T4Column
      {
        get
        {
          return this.columnT4;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T1_T2Column
      {
        get
        {
          return this.columnT1_T2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P1Column
      {
        get
        {
          return this.columnP1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P2Column
      {
        get
        {
          return this.columnP2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn FLOW1Column
      {
        get
        {
          return this.columnFLOW1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn FLOW2Column
      {
        get
        {
          return this.columnFLOW2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn EFFECT1Column
      {
        get
        {
          return this.columnEFFECT1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn HourColumn
      {
        get
        {
          return this.columnHour;
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
      public LiveLogDataSet.RegisterInUseRow this[int index]
      {
        get
        {
          return (LiveLogDataSet.RegisterInUseRow) this.Rows[index];
        }
      }

      public event LiveLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanging;

      public event LiveLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanged;

      public event LiveLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleting;

      public event LiveLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleted;

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
      public void AddRegisterInUseRow(LiveLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegisterInUseRow AddRegisterInUseRow(bool E1, bool E2, bool E3, bool E4, bool E5, bool E6, bool E7, bool E8, bool E9, bool TA2, bool TA3, bool V1, bool V2, bool INA, bool INB, bool M1, bool M2, bool T1, bool T2, bool T3, bool T4, bool T1_T2, bool P1, bool P2, bool FLOW1, bool FLOW2, bool EFFECT1, bool Hour)
      {
        LiveLogDataSet.RegisterInUseRow registerInUseRow = (LiveLogDataSet.RegisterInUseRow) this.NewRow();
        object[] objArray = new object[28]
        {
           (bool) (E1 ? true : false),
           (bool) (E2 ? true : false),
           (bool) (E3 ? true : false),
           (bool) (E4 ? true : false),
           (bool) (E5 ? true : false),
           (bool) (E6 ? true : false),
           (bool) (E7 ? true : false),
           (bool) (E8 ? true : false),
           (bool) (E9 ? true : false),
           (bool) (TA2 ? true : false),
           (bool) (TA3 ? true : false),
           (bool) (V1 ? true : false),
           (bool) (V2 ? true : false),
           (bool) (INA ? true : false),
           (bool) (INB ? true : false),
           (bool) (M1 ? true : false),
           (bool) (M2 ? true : false),
           (bool) (T1 ? true : false),
           (bool) (T2 ? true : false),
           (bool) (T3 ? true : false),
           (bool) (T4 ? true : false),
           (bool) (T1_T2 ? true : false),
           (bool) (P1 ? true : false),
           (bool) (P2 ? true : false),
           (bool) (FLOW1 ? true : false),
           (bool) (FLOW2 ? true : false),
           (bool) (EFFECT1 ? true : false),
           (bool) (Hour ? true : false)
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
        LiveLogDataSet.RegisterInUseDataTable registerInUseDataTable = (LiveLogDataSet.RegisterInUseDataTable) base.Clone();
        registerInUseDataTable.InitVars();
        return (DataTable) registerInUseDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new LiveLogDataSet.RegisterInUseDataTable();
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
        this.columnTA2 = this.Columns["TA2"];
        this.columnTA3 = this.Columns["TA3"];
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnINA = this.Columns["INA"];
        this.columnINB = this.Columns["INB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnT1 = this.Columns["T1"];
        this.columnT2 = this.Columns["T2"];
        this.columnT3 = this.Columns["T3"];
        this.columnT4 = this.Columns["T4"];
        this.columnT1_T2 = this.Columns["T1_T2"];
        this.columnP1 = this.Columns["P1"];
        this.columnP2 = this.Columns["P2"];
        this.columnFLOW1 = this.Columns["FLOW1"];
        this.columnFLOW2 = this.Columns["FLOW2"];
        this.columnEFFECT1 = this.Columns["EFFECT1"];
        this.columnHour = this.Columns["Hour"];
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
        this.columnTA2 = new DataColumn("TA2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTA2);
        this.columnTA3 = new DataColumn("TA3", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTA3);
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
        this.columnT1 = new DataColumn("T1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1);
        this.columnT2 = new DataColumn("T2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT2);
        this.columnT3 = new DataColumn("T3", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT3);
        this.columnT4 = new DataColumn("T4", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT4);
        this.columnT1_T2 = new DataColumn("T1_T2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1_T2);
        this.columnP1 = new DataColumn("P1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1);
        this.columnP2 = new DataColumn("P2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2);
        this.columnFLOW1 = new DataColumn("FLOW1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW1);
        this.columnFLOW2 = new DataColumn("FLOW2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW2);
        this.columnEFFECT1 = new DataColumn("EFFECT1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnEFFECT1);
        this.columnHour = new DataColumn("Hour", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnHour);
        this.columnTA2.Caption = "V1";
        this.columnTA3.Caption = "V1";
        this.columnT1.Caption = "T1Avr";
        this.columnT2.Caption = "T2Avr";
        this.columnT3.Caption = "T3Avr";
        this.columnT4.Caption = "T3Avr";
        this.columnT1_T2.Caption = "T3Avr";
        this.columnP1.Caption = "P1Avr";
        this.columnP2.Caption = "P2Avr";
        this.columnFLOW1.Caption = "Flow 1";
        this.columnFLOW2.Caption = "Flow 2";
        this.columnEFFECT1.Caption = "P2Avr";
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegisterInUseRow NewRegisterInUseRow()
      {
        return (LiveLogDataSet.RegisterInUseRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new LiveLogDataSet.RegisterInUseRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (LiveLogDataSet.RegisterInUseRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterInUseRowChanged == null)
          return;
        this.RegisterInUseRowChanged( this, new LiveLogDataSet.RegisterInUseRowChangeEvent((LiveLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterInUseRowChanging == null)
          return;
        this.RegisterInUseRowChanging( this, new LiveLogDataSet.RegisterInUseRowChangeEvent((LiveLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterInUseRowDeleted == null)
          return;
        this.RegisterInUseRowDeleted( this, new LiveLogDataSet.RegisterInUseRowChangeEvent((LiveLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterInUseRowDeleting == null)
          return;
        this.RegisterInUseRowDeleting( this, new LiveLogDataSet.RegisterInUseRowChangeEvent((LiveLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterInUseRow(LiveLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        LiveLogDataSet liveLogDataSet = new LiveLogDataSet();
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
          FixedValue = liveLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterInUseDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = liveLogDataSet.GetSchemaSerializable();
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
      private DataColumn columnTA2;
      private DataColumn columnTA3;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnT1;
      private DataColumn columnT2;
      private DataColumn columnT3;
      private DataColumn columnT4;
      private DataColumn columnT1_T2;
      private DataColumn columnP1;
      private DataColumn columnP2;
      private DataColumn columnFLOW1;
      private DataColumn columnFLOW2;
      private DataColumn columnEFFECT1;
      private DataColumn columnHour;

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
      public DataColumn TA2Column
      {
        get
        {
          return this.columnTA2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn TA3Column
      {
        get
        {
          return this.columnTA3;
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
      public DataColumn T1Column
      {
        get
        {
          return this.columnT1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T2Column
      {
        get
        {
          return this.columnT2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T3Column
      {
        get
        {
          return this.columnT3;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T4Column
      {
        get
        {
          return this.columnT4;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn T1_T2Column
      {
        get
        {
          return this.columnT1_T2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P1Column
      {
        get
        {
          return this.columnP1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn P2Column
      {
        get
        {
          return this.columnP2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn FLOW1Column
      {
        get
        {
          return this.columnFLOW1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn FLOW2Column
      {
        get
        {
          return this.columnFLOW2;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn EFFECT1Column
      {
        get
        {
          return this.columnEFFECT1;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn HourColumn
      {
        get
        {
          return this.columnHour;
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
      public LiveLogDataSet.RegisterUnitRow this[int index]
      {
        get
        {
          return (LiveLogDataSet.RegisterUnitRow) this.Rows[index];
        }
      }

      public event LiveLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanging;

      public event LiveLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanged;

      public event LiveLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleting;

      public event LiveLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleted;

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
      public void AddRegisterUnitRow(LiveLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegisterUnitRow AddRegisterUnitRow(byte E1, byte E2, byte E3, byte E4, byte E5, byte E6, byte E7, byte E8, byte E9, byte TA2, byte TA3, byte V1, byte V2, byte INA, byte INB, byte M1, byte M2, byte T1, byte T2, byte T3, byte T4, byte T1_T2, byte P1, byte P2, byte FLOW1, byte FLOW2, byte EFFECT1, byte Hour)
      {
        LiveLogDataSet.RegisterUnitRow registerUnitRow = (LiveLogDataSet.RegisterUnitRow) this.NewRow();
        object[] objArray = new object[28]
        {
           E1,
           E2,
           E3,
           E4,
           E5,
           E6,
           E7,
           E8,
           E9,
           TA2,
           TA3,
           V1,
           V2,
           INA,
           INB,
           M1,
           M2,
           T1,
           T2,
           T3,
           T4,
           T1_T2,
           P1,
           P2,
           FLOW1,
           FLOW2,
           EFFECT1,
           Hour
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
        LiveLogDataSet.RegisterUnitDataTable registerUnitDataTable = (LiveLogDataSet.RegisterUnitDataTable) base.Clone();
        registerUnitDataTable.InitVars();
        return (DataTable) registerUnitDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new LiveLogDataSet.RegisterUnitDataTable();
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
        this.columnTA2 = this.Columns["TA2"];
        this.columnTA3 = this.Columns["TA3"];
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnINA = this.Columns["INA"];
        this.columnINB = this.Columns["INB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnT1 = this.Columns["T1"];
        this.columnT2 = this.Columns["T2"];
        this.columnT3 = this.Columns["T3"];
        this.columnT4 = this.Columns["T4"];
        this.columnT1_T2 = this.Columns["T1_T2"];
        this.columnP1 = this.Columns["P1"];
        this.columnP2 = this.Columns["P2"];
        this.columnFLOW1 = this.Columns["FLOW1"];
        this.columnFLOW2 = this.Columns["FLOW2"];
        this.columnEFFECT1 = this.Columns["EFFECT1"];
        this.columnHour = this.Columns["Hour"];
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
        this.columnTA2 = new DataColumn("TA2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTA2);
        this.columnTA3 = new DataColumn("TA3", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTA3);
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
        this.columnT1 = new DataColumn("T1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1);
        this.columnT2 = new DataColumn("T2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT2);
        this.columnT3 = new DataColumn("T3", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT3);
        this.columnT4 = new DataColumn("T4", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT4);
        this.columnT1_T2 = new DataColumn("T1_T2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnT1_T2);
        this.columnP1 = new DataColumn("P1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1);
        this.columnP2 = new DataColumn("P2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2);
        this.columnFLOW1 = new DataColumn("FLOW1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW1);
        this.columnFLOW2 = new DataColumn("FLOW2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW2);
        this.columnEFFECT1 = new DataColumn("EFFECT1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnEFFECT1);
        this.columnHour = new DataColumn("Hour", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnHour);
        this.columnTA2.Caption = "V1";
        this.columnTA3.Caption = "V1";
        this.columnT1.Caption = "T1Avr";
        this.columnT2.Caption = "T2Avr";
        this.columnT3.Caption = "T3Avr";
        this.columnT4.Caption = "T3Avr";
        this.columnT1_T2.Caption = "T3Avr";
        this.columnP1.Caption = "P1Avr";
        this.columnP2.Caption = "P2Avr";
        this.columnFLOW1.Caption = "P2Avr";
        this.columnFLOW2.Caption = "P2Avr";
        this.columnEFFECT1.Caption = "P2Avr";
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegisterUnitRow NewRegisterUnitRow()
      {
        return (LiveLogDataSet.RegisterUnitRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new LiveLogDataSet.RegisterUnitRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (LiveLogDataSet.RegisterUnitRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterUnitRowChanged == null)
          return;
        this.RegisterUnitRowChanged( this, new LiveLogDataSet.RegisterUnitRowChangeEvent((LiveLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterUnitRowChanging == null)
          return;
        this.RegisterUnitRowChanging( this, new LiveLogDataSet.RegisterUnitRowChangeEvent((LiveLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterUnitRowDeleted == null)
          return;
        this.RegisterUnitRowDeleted( this, new LiveLogDataSet.RegisterUnitRowChangeEvent((LiveLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterUnitRowDeleting == null)
          return;
        this.RegisterUnitRowDeleting( this, new LiveLogDataSet.RegisterUnitRowChangeEvent((LiveLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterUnitRow(LiveLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        LiveLogDataSet liveLogDataSet = new LiveLogDataSet();
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
          FixedValue = liveLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterUnitDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = liveLogDataSet.GetSchemaSerializable();
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
      public LiveLogDataSet.CustomerNoRow this[int index]
      {
        get
        {
          return (LiveLogDataSet.CustomerNoRow) this.Rows[index];
        }
      }

      public event LiveLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanging;

      public event LiveLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanged;

      public event LiveLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleting;

      public event LiveLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleted;

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

      [DebuggerNonUserCode]
      public void AddCustomerNoRow(LiveLogDataSet.CustomerNoRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.CustomerNoRow AddCustomerNoRow(string CustomerNo)
      {
        LiveLogDataSet.CustomerNoRow customerNoRow = (LiveLogDataSet.CustomerNoRow) this.NewRow();
        object[] objArray = new object[1]
        {
           CustomerNo
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
        LiveLogDataSet.CustomerNoDataTable customerNoDataTable = (LiveLogDataSet.CustomerNoDataTable) base.Clone();
        customerNoDataTable.InitVars();
        return (DataTable) customerNoDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new LiveLogDataSet.CustomerNoDataTable();
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
        this.columnCustomerNo.DefaultValue =  " ";
      }

      [DebuggerNonUserCode]
      public LiveLogDataSet.CustomerNoRow NewCustomerNoRow()
      {
        return (LiveLogDataSet.CustomerNoRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new LiveLogDataSet.CustomerNoRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (LiveLogDataSet.CustomerNoRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.CustomerNoRowChanged == null)
          return;
        this.CustomerNoRowChanged( this, new LiveLogDataSet.CustomerNoRowChangeEvent((LiveLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.CustomerNoRowChanging == null)
          return;
        this.CustomerNoRowChanging( this, new LiveLogDataSet.CustomerNoRowChangeEvent((LiveLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.CustomerNoRowDeleted == null)
          return;
        this.CustomerNoRowDeleted( this, new LiveLogDataSet.CustomerNoRowChangeEvent((LiveLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.CustomerNoRowDeleting == null)
          return;
        this.CustomerNoRowDeleting( this, new LiveLogDataSet.CustomerNoRowChangeEvent((LiveLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveCustomerNoRow(LiveLogDataSet.CustomerNoRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        LiveLogDataSet liveLogDataSet = new LiveLogDataSet();
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
          FixedValue = liveLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "CustomerNoDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = liveLogDataSet.GetSchemaSerializable();
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

      public string GetCustomerNo()
      {
        if (this.Rows.Count == 0)
          return "";
        return ((LiveLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo;
      }

      public void SetCustomerNo(string customerNo)
      {
        if (this.Rows.Count == 0)
          this.AddCustomerNoRow(customerNo);
        else
          ((LiveLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo = customerNo;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegistersRow : DataRow
    {
      private LiveLogDataSet.RegistersDataTable tableRegisters;

      [DebuggerNonUserCode]
      public int Id
      {
        get
        {
          try
          {
            return (int) this[this.tableRegisters.IdColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Id' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.IdColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime Date
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableRegisters.DateColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Date' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.DateColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E3
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E3' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E3Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E4
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E4' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E4Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E5
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E5Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E5' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E5Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E6
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E6Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E6' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E6Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E7
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E7Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E7' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E7Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E8
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E8Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E8' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E8Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal E9
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.E9Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'E9' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.E9Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal TA2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.TA2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.TA2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal TA3
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.TA3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA3' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.TA3Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal V1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.V1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.V1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal V2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.V2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'V2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.V2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal INA
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.INAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INA' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.INAColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal INB
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.INBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INB' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.INBColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal M1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.M1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.M1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal M2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.M2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'M2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.M2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.T1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.T1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.T2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.T2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T3
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.T3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T3' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.T3Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T4
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.T4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T4' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.T4Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal T1_T2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.T1_T2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1_T2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.T1_T2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal P1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.P1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.P1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal P2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.P2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.P2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal FLOW1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.FLOW1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'FLOW1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.FLOW1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal FLOW2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.FLOW2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'FLOW2' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.FLOW2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal EFFECT1
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.EFFECT1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'EFFECT1' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.EFFECT1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal Hour
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.HourColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Hour' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.HourColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      internal RegistersRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisters = (LiveLogDataSet.RegistersDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsIdNull()
      {
        return this.IsNull(this.tableRegisters.IdColumn);
      }

      [DebuggerNonUserCode]
      public void SetIdNull()
      {
        this[this.tableRegisters.IdColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsDateNull()
      {
        return this.IsNull(this.tableRegisters.DateColumn);
      }

      [DebuggerNonUserCode]
      public void SetDateNull()
      {
        this[this.tableRegisters.DateColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE1Null()
      {
        return this.IsNull(this.tableRegisters.E1Column);
      }

      [DebuggerNonUserCode]
      public void SetE1Null()
      {
        this[this.tableRegisters.E1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE2Null()
      {
        return this.IsNull(this.tableRegisters.E2Column);
      }

      [DebuggerNonUserCode]
      public void SetE2Null()
      {
        this[this.tableRegisters.E2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE3Null()
      {
        return this.IsNull(this.tableRegisters.E3Column);
      }

      [DebuggerNonUserCode]
      public void SetE3Null()
      {
        this[this.tableRegisters.E3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE4Null()
      {
        return this.IsNull(this.tableRegisters.E4Column);
      }

      [DebuggerNonUserCode]
      public void SetE4Null()
      {
        this[this.tableRegisters.E4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE5Null()
      {
        return this.IsNull(this.tableRegisters.E5Column);
      }

      [DebuggerNonUserCode]
      public void SetE5Null()
      {
        this[this.tableRegisters.E5Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE6Null()
      {
        return this.IsNull(this.tableRegisters.E6Column);
      }

      [DebuggerNonUserCode]
      public void SetE6Null()
      {
        this[this.tableRegisters.E6Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE7Null()
      {
        return this.IsNull(this.tableRegisters.E7Column);
      }

      [DebuggerNonUserCode]
      public void SetE7Null()
      {
        this[this.tableRegisters.E7Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE8Null()
      {
        return this.IsNull(this.tableRegisters.E8Column);
      }

      [DebuggerNonUserCode]
      public void SetE8Null()
      {
        this[this.tableRegisters.E8Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsE9Null()
      {
        return this.IsNull(this.tableRegisters.E9Column);
      }

      [DebuggerNonUserCode]
      public void SetE9Null()
      {
        this[this.tableRegisters.E9Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsTA2Null()
      {
        return this.IsNull(this.tableRegisters.TA2Column);
      }

      [DebuggerNonUserCode]
      public void SetTA2Null()
      {
        this[this.tableRegisters.TA2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsTA3Null()
      {
        return this.IsNull(this.tableRegisters.TA3Column);
      }

      [DebuggerNonUserCode]
      public void SetTA3Null()
      {
        this[this.tableRegisters.TA3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV1Null()
      {
        return this.IsNull(this.tableRegisters.V1Column);
      }

      [DebuggerNonUserCode]
      public void SetV1Null()
      {
        this[this.tableRegisters.V1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsV2Null()
      {
        return this.IsNull(this.tableRegisters.V2Column);
      }

      [DebuggerNonUserCode]
      public void SetV2Null()
      {
        this[this.tableRegisters.V2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINANull()
      {
        return this.IsNull(this.tableRegisters.INAColumn);
      }

      [DebuggerNonUserCode]
      public void SetINANull()
      {
        this[this.tableRegisters.INAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINBNull()
      {
        return this.IsNull(this.tableRegisters.INBColumn);
      }

      [DebuggerNonUserCode]
      public void SetINBNull()
      {
        this[this.tableRegisters.INBColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM1Null()
      {
        return this.IsNull(this.tableRegisters.M1Column);
      }

      [DebuggerNonUserCode]
      public void SetM1Null()
      {
        this[this.tableRegisters.M1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsM2Null()
      {
        return this.IsNull(this.tableRegisters.M2Column);
      }

      [DebuggerNonUserCode]
      public void SetM2Null()
      {
        this[this.tableRegisters.M2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1Null()
      {
        return this.IsNull(this.tableRegisters.T1Column);
      }

      [DebuggerNonUserCode]
      public void SetT1Null()
      {
        this[this.tableRegisters.T1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT2Null()
      {
        return this.IsNull(this.tableRegisters.T2Column);
      }

      [DebuggerNonUserCode]
      public void SetT2Null()
      {
        this[this.tableRegisters.T2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT3Null()
      {
        return this.IsNull(this.tableRegisters.T3Column);
      }

      [DebuggerNonUserCode]
      public void SetT3Null()
      {
        this[this.tableRegisters.T3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT4Null()
      {
        return this.IsNull(this.tableRegisters.T4Column);
      }

      [DebuggerNonUserCode]
      public void SetT4Null()
      {
        this[this.tableRegisters.T4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1_T2Null()
      {
        return this.IsNull(this.tableRegisters.T1_T2Column);
      }

      [DebuggerNonUserCode]
      public void SetT1_T2Null()
      {
        this[this.tableRegisters.T1_T2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP1Null()
      {
        return this.IsNull(this.tableRegisters.P1Column);
      }

      [DebuggerNonUserCode]
      public void SetP1Null()
      {
        this[this.tableRegisters.P1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP2Null()
      {
        return this.IsNull(this.tableRegisters.P2Column);
      }

      [DebuggerNonUserCode]
      public void SetP2Null()
      {
        this[this.tableRegisters.P2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsFLOW1Null()
      {
        return this.IsNull(this.tableRegisters.FLOW1Column);
      }

      [DebuggerNonUserCode]
      public void SetFLOW1Null()
      {
        this[this.tableRegisters.FLOW1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsFLOW2Null()
      {
        return this.IsNull(this.tableRegisters.FLOW2Column);
      }

      [DebuggerNonUserCode]
      public void SetFLOW2Null()
      {
        this[this.tableRegisters.FLOW2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsEFFECT1Null()
      {
        return this.IsNull(this.tableRegisters.EFFECT1Column);
      }

      [DebuggerNonUserCode]
      public void SetEFFECT1Null()
      {
        this[this.tableRegisters.EFFECT1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsHourNull()
      {
        return this.IsNull(this.tableRegisters.HourColumn);
      }

      [DebuggerNonUserCode]
      public void SetHourNull()
      {
        this[this.tableRegisters.HourColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRow : DataRow
    {
      private LiveLogDataSet.RegisterInUseDataTable tableRegisterInUse;

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
          this[this.tableRegisterInUse.E1Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E2Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E3Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E4Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E5Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E6Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E7Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E8Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.E9Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool TA2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.TA2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.TA2Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool TA3
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.TA3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA3' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.TA3Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.V1Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.V2Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.INAColumn] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.INBColumn] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.M1Column] =  (bool) (value ? true : false);
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
          this[this.tableRegisterInUse.M2Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool T1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T1Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool T2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T2Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool T3
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T3' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T3Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool T4
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T4' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T4Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool T1_T2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.T1_T2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1_T2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.T1_T2Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool P1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.P1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.P1Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool P2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.P2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.P2Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool FLOW1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.FLOW1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'FLOW1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.FLOW1Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool FLOW2
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.FLOW2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'FLOW2' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.FLOW2Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool EFFECT1
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.EFFECT1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'EFFECT1' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.EFFECT1Column] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      public bool Hour
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.HourColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Hour' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.HourColumn] =  (bool) (value ? true : false);
        }
      }

      [DebuggerNonUserCode]
      internal RegisterInUseRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterInUse = (LiveLogDataSet.RegisterInUseDataTable) this.Table;
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
      public bool IsTA2Null()
      {
        return this.IsNull(this.tableRegisterInUse.TA2Column);
      }

      [DebuggerNonUserCode]
      public void SetTA2Null()
      {
        this[this.tableRegisterInUse.TA2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsTA3Null()
      {
        return this.IsNull(this.tableRegisterInUse.TA3Column);
      }

      [DebuggerNonUserCode]
      public void SetTA3Null()
      {
        this[this.tableRegisterInUse.TA3Column] = Convert.DBNull;
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
      public bool IsT1Null()
      {
        return this.IsNull(this.tableRegisterInUse.T1Column);
      }

      [DebuggerNonUserCode]
      public void SetT1Null()
      {
        this[this.tableRegisterInUse.T1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT2Null()
      {
        return this.IsNull(this.tableRegisterInUse.T2Column);
      }

      [DebuggerNonUserCode]
      public void SetT2Null()
      {
        this[this.tableRegisterInUse.T2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT3Null()
      {
        return this.IsNull(this.tableRegisterInUse.T3Column);
      }

      [DebuggerNonUserCode]
      public void SetT3Null()
      {
        this[this.tableRegisterInUse.T3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT4Null()
      {
        return this.IsNull(this.tableRegisterInUse.T4Column);
      }

      [DebuggerNonUserCode]
      public void SetT4Null()
      {
        this[this.tableRegisterInUse.T4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1_T2Null()
      {
        return this.IsNull(this.tableRegisterInUse.T1_T2Column);
      }

      [DebuggerNonUserCode]
      public void SetT1_T2Null()
      {
        this[this.tableRegisterInUse.T1_T2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP1Null()
      {
        return this.IsNull(this.tableRegisterInUse.P1Column);
      }

      [DebuggerNonUserCode]
      public void SetP1Null()
      {
        this[this.tableRegisterInUse.P1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP2Null()
      {
        return this.IsNull(this.tableRegisterInUse.P2Column);
      }

      [DebuggerNonUserCode]
      public void SetP2Null()
      {
        this[this.tableRegisterInUse.P2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsFLOW1Null()
      {
        return this.IsNull(this.tableRegisterInUse.FLOW1Column);
      }

      [DebuggerNonUserCode]
      public void SetFLOW1Null()
      {
        this[this.tableRegisterInUse.FLOW1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsFLOW2Null()
      {
        return this.IsNull(this.tableRegisterInUse.FLOW2Column);
      }

      [DebuggerNonUserCode]
      public void SetFLOW2Null()
      {
        this[this.tableRegisterInUse.FLOW2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsEFFECT1Null()
      {
        return this.IsNull(this.tableRegisterInUse.EFFECT1Column);
      }

      [DebuggerNonUserCode]
      public void SetEFFECT1Null()
      {
        this[this.tableRegisterInUse.EFFECT1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsHourNull()
      {
        return this.IsNull(this.tableRegisterInUse.HourColumn);
      }

      [DebuggerNonUserCode]
      public void SetHourNull()
      {
        this[this.tableRegisterInUse.HourColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterUnitRow : DataRow
    {
      private LiveLogDataSet.RegisterUnitDataTable tableRegisterUnit;

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
          this[this.tableRegisterUnit.E1Column] =  value;
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
          this[this.tableRegisterUnit.E2Column] =  value;
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
          this[this.tableRegisterUnit.E3Column] =  value;
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
          this[this.tableRegisterUnit.E4Column] =  value;
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
          this[this.tableRegisterUnit.E5Column] =  value;
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
          this[this.tableRegisterUnit.E6Column] =  value;
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
          this[this.tableRegisterUnit.E7Column] =  value;
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
          this[this.tableRegisterUnit.E8Column] =  value;
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
          this[this.tableRegisterUnit.E9Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte TA2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.TA2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.TA2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte TA3
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.TA3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA3' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.TA3Column] =  value;
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
          this[this.tableRegisterUnit.V1Column] =  value;
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
          this[this.tableRegisterUnit.V2Column] =  value;
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
          this[this.tableRegisterUnit.INAColumn] =  value;
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
          this[this.tableRegisterUnit.INBColumn] =  value;
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
          this[this.tableRegisterUnit.M1Column] =  value;
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
          this[this.tableRegisterUnit.M2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte T1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte T2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte T3
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T3' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T3Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte T4
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T4Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T4' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T4Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte T1_T2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.T1_T2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'T1_T2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.T1_T2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte P1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.P1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.P1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte P2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.P2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'P2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.P2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte FLOW1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.FLOW1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'FLOW1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.FLOW1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte FLOW2
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.FLOW2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'FLOW2' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.FLOW2Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte EFFECT1
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.EFFECT1Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'EFFECT1' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.EFFECT1Column] =  value;
        }
      }

      [DebuggerNonUserCode]
      public byte Hour
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.HourColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Hour' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.HourColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      internal RegisterUnitRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterUnit = (LiveLogDataSet.RegisterUnitDataTable) this.Table;
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
      public bool IsTA2Null()
      {
        return this.IsNull(this.tableRegisterUnit.TA2Column);
      }

      [DebuggerNonUserCode]
      public void SetTA2Null()
      {
        this[this.tableRegisterUnit.TA2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsTA3Null()
      {
        return this.IsNull(this.tableRegisterUnit.TA3Column);
      }

      [DebuggerNonUserCode]
      public void SetTA3Null()
      {
        this[this.tableRegisterUnit.TA3Column] = Convert.DBNull;
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
      public bool IsT1Null()
      {
        return this.IsNull(this.tableRegisterUnit.T1Column);
      }

      [DebuggerNonUserCode]
      public void SetT1Null()
      {
        this[this.tableRegisterUnit.T1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT2Null()
      {
        return this.IsNull(this.tableRegisterUnit.T2Column);
      }

      [DebuggerNonUserCode]
      public void SetT2Null()
      {
        this[this.tableRegisterUnit.T2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT3Null()
      {
        return this.IsNull(this.tableRegisterUnit.T3Column);
      }

      [DebuggerNonUserCode]
      public void SetT3Null()
      {
        this[this.tableRegisterUnit.T3Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT4Null()
      {
        return this.IsNull(this.tableRegisterUnit.T4Column);
      }

      [DebuggerNonUserCode]
      public void SetT4Null()
      {
        this[this.tableRegisterUnit.T4Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsT1_T2Null()
      {
        return this.IsNull(this.tableRegisterUnit.T1_T2Column);
      }

      [DebuggerNonUserCode]
      public void SetT1_T2Null()
      {
        this[this.tableRegisterUnit.T1_T2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP1Null()
      {
        return this.IsNull(this.tableRegisterUnit.P1Column);
      }

      [DebuggerNonUserCode]
      public void SetP1Null()
      {
        this[this.tableRegisterUnit.P1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsP2Null()
      {
        return this.IsNull(this.tableRegisterUnit.P2Column);
      }

      [DebuggerNonUserCode]
      public void SetP2Null()
      {
        this[this.tableRegisterUnit.P2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsFLOW1Null()
      {
        return this.IsNull(this.tableRegisterUnit.FLOW1Column);
      }

      [DebuggerNonUserCode]
      public void SetFLOW1Null()
      {
        this[this.tableRegisterUnit.FLOW1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsFLOW2Null()
      {
        return this.IsNull(this.tableRegisterUnit.FLOW2Column);
      }

      [DebuggerNonUserCode]
      public void SetFLOW2Null()
      {
        this[this.tableRegisterUnit.FLOW2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsEFFECT1Null()
      {
        return this.IsNull(this.tableRegisterUnit.EFFECT1Column);
      }

      [DebuggerNonUserCode]
      public void SetEFFECT1Null()
      {
        this[this.tableRegisterUnit.EFFECT1Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsHourNull()
      {
        return this.IsNull(this.tableRegisterUnit.HourColumn);
      }

      [DebuggerNonUserCode]
      public void SetHourNull()
      {
        this[this.tableRegisterUnit.HourColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRow : DataRow
    {
      private LiveLogDataSet.CustomerNoDataTable tableCustomerNo;

      [DebuggerNonUserCode]
      public string CustomerNo
      {
        get
        {
          if (this.IsCustomerNoNull())
            return "(Empty)";
          return (string) this[this.tableCustomerNo.CustomerNoColumn];
        }
        set
        {
          this[this.tableCustomerNo.CustomerNoColumn] =  value;
        }
      }

      [DebuggerNonUserCode]
      internal CustomerNoRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableCustomerNo = (LiveLogDataSet.CustomerNoDataTable) this.Table;
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
    public class RegistersRowChangeEvent : EventArgs
    {
      private LiveLogDataSet.RegistersRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegistersRow Row
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
      public RegistersRowChangeEvent(LiveLogDataSet.RegistersRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRowChangeEvent : EventArgs
    {
      private LiveLogDataSet.RegisterInUseRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegisterInUseRow Row
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
      public RegisterInUseRowChangeEvent(LiveLogDataSet.RegisterInUseRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterUnitRowChangeEvent : EventArgs
    {
      private LiveLogDataSet.RegisterUnitRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public LiveLogDataSet.RegisterUnitRow Row
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
      public RegisterUnitRowChangeEvent(LiveLogDataSet.RegisterUnitRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRowChangeEvent : EventArgs
    {
      private LiveLogDataSet.CustomerNoRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public LiveLogDataSet.CustomerNoRow Row
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
      public CustomerNoRowChangeEvent(LiveLogDataSet.CustomerNoRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
