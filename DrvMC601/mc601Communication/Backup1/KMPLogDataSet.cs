// Decompiled with JetBrains decompiler
// Type: MC601LogView.KMPLogDataSet
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
  [HelpKeyword("vs.data.DataSet")]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("KMPLogDataSet")]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [Serializable]
  public class KMPLogDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private KMPLogDataSet.RegistersDataTable tableRegisters;
    private KMPLogDataSet.RegisterInUseDataTable tableRegisterInUse;
    private KMPLogDataSet.RegisterUnitDataTable tableRegisterUnit;
    private KMPLogDataSet.CustomerNoDataTable tableCustomerNo;
    private KMPLogDataSet.DatetimeRecordDataTable tableDatetimeRecord;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public KMPLogDataSet.RegistersDataTable Registers
    {
      get
      {
        return this.tableRegisters;
      }
    }

    [DebuggerNonUserCode]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KMPLogDataSet.RegisterInUseDataTable RegisterInUse
    {
      get
      {
        return this.tableRegisterInUse;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public KMPLogDataSet.RegisterUnitDataTable RegisterUnit
    {
      get
      {
        return this.tableRegisterUnit;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public KMPLogDataSet.CustomerNoDataTable CustomerNo
    {
      get
      {
        return this.tableCustomerNo;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public KMPLogDataSet.DatetimeRecordDataTable DatetimeRecord
    {
      get
      {
        return this.tableDatetimeRecord;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DebuggerNonUserCode]
    [Browsable(true)]
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
    public KMPLogDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected KMPLogDataSet(SerializationInfo info, StreamingContext context)
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
            base.Tables.Add((DataTable) new KMPLogDataSet.RegistersDataTable(dataSet.Tables["Registers"]));
          if (dataSet.Tables["RegisterInUse"] != null)
            base.Tables.Add((DataTable) new KMPLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
          if (dataSet.Tables["RegisterUnit"] != null)
            base.Tables.Add((DataTable) new KMPLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
          if (dataSet.Tables["CustomerNo"] != null)
            base.Tables.Add((DataTable) new KMPLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
          if (dataSet.Tables["DatetimeRecord"] != null)
            base.Tables.Add((DataTable) new KMPLogDataSet.DatetimeRecordDataTable(dataSet.Tables["DatetimeRecord"]));
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
      KMPLogDataSet kmpLogDataSet = (KMPLogDataSet) base.Clone();
      kmpLogDataSet.InitVars();
      kmpLogDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) kmpLogDataSet;
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
          base.Tables.Add((DataTable) new KMPLogDataSet.RegistersDataTable(dataSet.Tables["Registers"]));
        if (dataSet.Tables["RegisterInUse"] != null)
          base.Tables.Add((DataTable) new KMPLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
        if (dataSet.Tables["RegisterUnit"] != null)
          base.Tables.Add((DataTable) new KMPLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
        if (dataSet.Tables["CustomerNo"] != null)
          base.Tables.Add((DataTable) new KMPLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
        if (dataSet.Tables["DatetimeRecord"] != null)
          base.Tables.Add((DataTable) new KMPLogDataSet.DatetimeRecordDataTable(dataSet.Tables["DatetimeRecord"]));
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
      this.tableRegisters = (KMPLogDataSet.RegistersDataTable) base.Tables["Registers"];
      if (initTable && this.tableRegisters != null)
        this.tableRegisters.InitVars();
      this.tableRegisterInUse = (KMPLogDataSet.RegisterInUseDataTable) base.Tables["RegisterInUse"];
      if (initTable && this.tableRegisterInUse != null)
        this.tableRegisterInUse.InitVars();
      this.tableRegisterUnit = (KMPLogDataSet.RegisterUnitDataTable) base.Tables["RegisterUnit"];
      if (initTable && this.tableRegisterUnit != null)
        this.tableRegisterUnit.InitVars();
      this.tableCustomerNo = (KMPLogDataSet.CustomerNoDataTable) base.Tables["CustomerNo"];
      if (initTable && this.tableCustomerNo != null)
        this.tableCustomerNo.InitVars();
      this.tableDatetimeRecord = (KMPLogDataSet.DatetimeRecordDataTable) base.Tables["DatetimeRecord"];
      if (!initTable || this.tableDatetimeRecord == null)
        return;
      this.tableDatetimeRecord.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "KMPLogDataSet";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/KMPLogDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableRegisters = new KMPLogDataSet.RegistersDataTable();
      base.Tables.Add((DataTable) this.tableRegisters);
      this.tableRegisterInUse = new KMPLogDataSet.RegisterInUseDataTable();
      base.Tables.Add((DataTable) this.tableRegisterInUse);
      this.tableRegisterUnit = new KMPLogDataSet.RegisterUnitDataTable();
      base.Tables.Add((DataTable) this.tableRegisterUnit);
      this.tableCustomerNo = new KMPLogDataSet.CustomerNoDataTable();
      base.Tables.Add((DataTable) this.tableCustomerNo);
      this.tableDatetimeRecord = new KMPLogDataSet.DatetimeRecordDataTable();
      base.Tables.Add((DataTable) this.tableDatetimeRecord);
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
    private bool ShouldSerializeDatetimeRecord()
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
      KMPLogDataSet kmpLogDataSet = new KMPLogDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = kmpLogDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = kmpLogDataSet.GetSchemaSerializable();
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
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnVA;
      private DataColumn columnVB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnT1;
      private DataColumn columnT2;
      private DataColumn columnT3;
      private DataColumn columnT4;
      private DataColumn columnT1_T2;
      private DataColumn columnFLOW1;
      private DataColumn columnFLOW2;
      private DataColumn columnEFFECT1;
      private DataColumn columnP1;
      private DataColumn columnP2;
      private DataColumn columnINFO;
      private DataColumn columnLogQOS;
      private DataColumn columnHR;

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
      public DataColumn VAColumn
      {
        get
        {
          return this.columnVA;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn VBColumn
      {
        get
        {
          return this.columnVB;
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
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn LogQOSColumn
      {
        get
        {
          return this.columnLogQOS;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn HRColumn
      {
        get
        {
          return this.columnHR;
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
      public KMPLogDataSet.RegistersRow this[int index]
      {
        get
        {
          return (KMPLogDataSet.RegistersRow) this.Rows[index];
        }
      }

      public event KMPLogDataSet.RegistersRowChangeEventHandler RegistersRowChanging;

      public event KMPLogDataSet.RegistersRowChangeEventHandler RegistersRowChanged;

      public event KMPLogDataSet.RegistersRowChangeEventHandler RegistersRowDeleting;

      public event KMPLogDataSet.RegistersRowChangeEventHandler RegistersRowDeleted;

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

      public KMPLogDataSet.RegistersRow GetRow(int recordId)
      {
        foreach (KMPLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.Rows)
        {
          if (registersRow.Id == recordId)
            return registersRow;
        }
        return (KMPLogDataSet.RegistersRow) null;
      }

      [DebuggerNonUserCode]
      public void AddRegistersRow(KMPLogDataSet.RegistersRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegistersRow AddRegistersRow(int Id, DateTime Date, Decimal E1, Decimal E2, Decimal E3, Decimal E4, Decimal E5, Decimal E6, Decimal E7, Decimal E8, Decimal E9, Decimal V1, Decimal V2, Decimal VA, Decimal VB, Decimal M1, Decimal M2, Decimal T1, Decimal T2, Decimal T3, Decimal T4, Decimal T1_T2, Decimal FLOW1, Decimal FLOW2, Decimal EFFECT1, Decimal P1, Decimal P2, Decimal INFO, Decimal LogQOS, Decimal HR)
      {
        KMPLogDataSet.RegistersRow registersRow = (KMPLogDataSet.RegistersRow) this.NewRow();
        object[] objArray = new object[30]
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
          (object) V1,
          (object) V2,
          (object) VA,
          (object) VB,
          (object) M1,
          (object) M2,
          (object) T1,
          (object) T2,
          (object) T3,
          (object) T4,
          (object) T1_T2,
          (object) FLOW1,
          (object) FLOW2,
          (object) EFFECT1,
          (object) P1,
          (object) P2,
          (object) INFO,
          (object) LogQOS,
          (object) HR
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
        KMPLogDataSet.RegistersDataTable registersDataTable = (KMPLogDataSet.RegistersDataTable) base.Clone();
        registersDataTable.InitVars();
        return (DataTable) registersDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new KMPLogDataSet.RegistersDataTable();
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
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnVA = this.Columns["VA"];
        this.columnVB = this.Columns["VB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnT1 = this.Columns["T1"];
        this.columnT2 = this.Columns["T2"];
        this.columnT3 = this.Columns["T3"];
        this.columnT4 = this.Columns["T4"];
        this.columnT1_T2 = this.Columns["T1_T2"];
        this.columnFLOW1 = this.Columns["FLOW1"];
        this.columnFLOW2 = this.Columns["FLOW2"];
        this.columnEFFECT1 = this.Columns["EFFECT1"];
        this.columnP1 = this.Columns["P1"];
        this.columnP2 = this.Columns["P2"];
        this.columnINFO = this.Columns["INFO"];
        this.columnLogQOS = this.Columns["LogQOS"];
        this.columnHR = this.Columns["HR"];
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
        this.columnV1 = new DataColumn("V1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV1);
        this.columnV2 = new DataColumn("V2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV2);
        this.columnVA = new DataColumn("VA", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVA);
        this.columnVB = new DataColumn("VB", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVB);
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
        this.columnFLOW1 = new DataColumn("FLOW1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW1);
        this.columnFLOW2 = new DataColumn("FLOW2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW2);
        this.columnEFFECT1 = new DataColumn("EFFECT1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnEFFECT1);
        this.columnP1 = new DataColumn("P1", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1);
        this.columnP2 = new DataColumn("P2", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2);
        this.columnINFO = new DataColumn("INFO", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnLogQOS = new DataColumn("LogQOS", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnLogQOS);
        this.columnHR = new DataColumn("HR", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnHR);
        this.columnE1.DefaultValue = (object) new Decimal(0);
        this.columnE2.DefaultValue = (object) new Decimal(0);
        this.columnE3.DefaultValue = (object) new Decimal(0);
        this.columnE4.DefaultValue = (object) new Decimal(0);
        this.columnE5.DefaultValue = (object) new Decimal(0);
        this.columnE6.DefaultValue = (object) new Decimal(0);
        this.columnE7.DefaultValue = (object) new Decimal(0);
        this.columnE8.DefaultValue = (object) new Decimal(0);
        this.columnE9.DefaultValue = (object) new Decimal(0);
        this.columnV1.DefaultValue = (object) new Decimal(0);
        this.columnV2.DefaultValue = (object) new Decimal(0);
        this.columnVA.DefaultValue = (object) new Decimal(0);
        this.columnVB.DefaultValue = (object) new Decimal(0);
        this.columnM1.DefaultValue = (object) new Decimal(0);
        this.columnM2.DefaultValue = (object) new Decimal(0);
        this.columnT1.DefaultValue = (object) new Decimal(0);
        this.columnT2.DefaultValue = (object) new Decimal(0);
        this.columnT3.DefaultValue = (object) new Decimal(0);
        this.columnT4.DefaultValue = (object) new Decimal(0);
        this.columnT1_T2.DefaultValue = (object) new Decimal(0);
        this.columnFLOW1.DefaultValue = (object) new Decimal(0);
        this.columnFLOW2.DefaultValue = (object) new Decimal(0);
        this.columnEFFECT1.DefaultValue = (object) new Decimal(0);
        this.columnP1.DefaultValue = (object) new Decimal(0);
        this.columnP2.DefaultValue = (object) new Decimal(0);
        this.columnINFO.DefaultValue = (object) new Decimal(0);
        this.columnLogQOS.DefaultValue = (object) new Decimal(0);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegistersRow NewRegistersRow()
      {
        return (KMPLogDataSet.RegistersRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new KMPLogDataSet.RegistersRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (KMPLogDataSet.RegistersRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegistersRowChanged == null)
          return;
        this.RegistersRowChanged((object) this, new KMPLogDataSet.RegistersRowChangeEvent((KMPLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegistersRowChanging == null)
          return;
        this.RegistersRowChanging((object) this, new KMPLogDataSet.RegistersRowChangeEvent((KMPLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegistersRowDeleted == null)
          return;
        this.RegistersRowDeleted((object) this, new KMPLogDataSet.RegistersRowChangeEvent((KMPLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegistersRowDeleting == null)
          return;
        this.RegistersRowDeleting((object) this, new KMPLogDataSet.RegistersRowChangeEvent((KMPLogDataSet.RegistersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegistersRow(KMPLogDataSet.RegistersRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        KMPLogDataSet kmpLogDataSet = new KMPLogDataSet();
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
          FixedValue = kmpLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegistersDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = kmpLogDataSet.GetSchemaSerializable();
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
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnVA;
      private DataColumn columnVB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnT1;
      private DataColumn columnT2;
      private DataColumn columnT3;
      private DataColumn columnT4;
      private DataColumn columnT1_T2;
      private DataColumn columnFLOW1;
      private DataColumn columnFLOW2;
      private DataColumn columnEFFECT1;
      private DataColumn columnP1;
      private DataColumn columnP2;
      private DataColumn columnINFO;
      private DataColumn columnLogQOS;
      private DataColumn columnHR;

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
      public DataColumn VAColumn
      {
        get
        {
          return this.columnVA;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn VBColumn
      {
        get
        {
          return this.columnVB;
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
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn LogQOSColumn
      {
        get
        {
          return this.columnLogQOS;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn HRColumn
      {
        get
        {
          return this.columnHR;
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
      public KMPLogDataSet.RegisterUnitRow this[int index]
      {
        get
        {
          return (KMPLogDataSet.RegisterUnitRow) this.Rows[index];
        }
      }

      public event KMPLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanging;

      public event KMPLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanged;

      public event KMPLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleting;

      public event KMPLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleted;

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
      public void AddRegisterUnitRow(KMPLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegisterUnitRow AddRegisterUnitRow(byte E1, byte E2, byte E3, byte E4, byte E5, byte E6, byte E7, byte E8, byte E9, byte V1, byte V2, byte VA, byte VB, byte M1, byte M2, byte T1, byte T2, byte T3, byte T4, byte T1_T2, byte FLOW1, byte FLOW2, byte EFFECT1, byte P1, byte P2, byte INFO, byte LogQOS, byte HR)
      {
        KMPLogDataSet.RegisterUnitRow registerUnitRow = (KMPLogDataSet.RegisterUnitRow) this.NewRow();
        object[] objArray = new object[28]
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
          (object) V1,
          (object) V2,
          (object) VA,
          (object) VB,
          (object) M1,
          (object) M2,
          (object) T1,
          (object) T2,
          (object) T3,
          (object) T4,
          (object) T1_T2,
          (object) FLOW1,
          (object) FLOW2,
          (object) EFFECT1,
          (object) P1,
          (object) P2,
          (object) INFO,
          (object) LogQOS,
          (object) HR
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
        KMPLogDataSet.RegisterUnitDataTable registerUnitDataTable = (KMPLogDataSet.RegisterUnitDataTable) base.Clone();
        registerUnitDataTable.InitVars();
        return (DataTable) registerUnitDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new KMPLogDataSet.RegisterUnitDataTable();
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
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnVA = this.Columns["VA"];
        this.columnVB = this.Columns["VB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnT1 = this.Columns["T1"];
        this.columnT2 = this.Columns["T2"];
        this.columnT3 = this.Columns["T3"];
        this.columnT4 = this.Columns["T4"];
        this.columnT1_T2 = this.Columns["T1_T2"];
        this.columnFLOW1 = this.Columns["FLOW1"];
        this.columnFLOW2 = this.Columns["FLOW2"];
        this.columnEFFECT1 = this.Columns["EFFECT1"];
        this.columnP1 = this.Columns["P1"];
        this.columnP2 = this.Columns["P2"];
        this.columnINFO = this.Columns["INFO"];
        this.columnLogQOS = this.Columns["LogQOS"];
        this.columnHR = this.Columns["HR"];
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
        this.columnV1 = new DataColumn("V1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV1);
        this.columnV2 = new DataColumn("V2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV2);
        this.columnVA = new DataColumn("VA", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVA);
        this.columnVB = new DataColumn("VB", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVB);
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
        this.columnFLOW1 = new DataColumn("FLOW1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW1);
        this.columnFLOW2 = new DataColumn("FLOW2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW2);
        this.columnEFFECT1 = new DataColumn("EFFECT1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnEFFECT1);
        this.columnP1 = new DataColumn("P1", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1);
        this.columnP2 = new DataColumn("P2", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2);
        this.columnINFO = new DataColumn("INFO", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnLogQOS = new DataColumn("LogQOS", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnLogQOS);
        this.columnHR = new DataColumn("HR", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnHR);
        this.columnINFO.DefaultValue = (object) byte.MaxValue;
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegisterUnitRow NewRegisterUnitRow()
      {
        return (KMPLogDataSet.RegisterUnitRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new KMPLogDataSet.RegisterUnitRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (KMPLogDataSet.RegisterUnitRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterUnitRowChanged == null)
          return;
        this.RegisterUnitRowChanged((object) this, new KMPLogDataSet.RegisterUnitRowChangeEvent((KMPLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterUnitRowChanging == null)
          return;
        this.RegisterUnitRowChanging((object) this, new KMPLogDataSet.RegisterUnitRowChangeEvent((KMPLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterUnitRowDeleted == null)
          return;
        this.RegisterUnitRowDeleted((object) this, new KMPLogDataSet.RegisterUnitRowChangeEvent((KMPLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterUnitRowDeleting == null)
          return;
        this.RegisterUnitRowDeleting((object) this, new KMPLogDataSet.RegisterUnitRowChangeEvent((KMPLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterUnitRow(KMPLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        KMPLogDataSet kmpLogDataSet = new KMPLogDataSet();
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
          FixedValue = kmpLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterUnitDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = kmpLogDataSet.GetSchemaSerializable();
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
      public KMPLogDataSet.CustomerNoRow this[int index]
      {
        get
        {
          return (KMPLogDataSet.CustomerNoRow) this.Rows[index];
        }
      }

      public event KMPLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanging;

      public event KMPLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanged;

      public event KMPLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleting;

      public event KMPLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleted;

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
        return ((KMPLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo;
      }

      public void SetCustomerNo(string customerNo)
      {
        if (this.Rows.Count == 0)
          this.AddCustomerNoRow(customerNo);
        else
          ((KMPLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo = customerNo;
      }

      [DebuggerNonUserCode]
      public void AddCustomerNoRow(KMPLogDataSet.CustomerNoRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.CustomerNoRow AddCustomerNoRow(string CustomerNo)
      {
        KMPLogDataSet.CustomerNoRow customerNoRow = (KMPLogDataSet.CustomerNoRow) this.NewRow();
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
        KMPLogDataSet.CustomerNoDataTable customerNoDataTable = (KMPLogDataSet.CustomerNoDataTable) base.Clone();
        customerNoDataTable.InitVars();
        return (DataTable) customerNoDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new KMPLogDataSet.CustomerNoDataTable();
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
      public KMPLogDataSet.CustomerNoRow NewCustomerNoRow()
      {
        return (KMPLogDataSet.CustomerNoRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new KMPLogDataSet.CustomerNoRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (KMPLogDataSet.CustomerNoRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.CustomerNoRowChanged == null)
          return;
        this.CustomerNoRowChanged((object) this, new KMPLogDataSet.CustomerNoRowChangeEvent((KMPLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.CustomerNoRowChanging == null)
          return;
        this.CustomerNoRowChanging((object) this, new KMPLogDataSet.CustomerNoRowChangeEvent((KMPLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.CustomerNoRowDeleted == null)
          return;
        this.CustomerNoRowDeleted((object) this, new KMPLogDataSet.CustomerNoRowChangeEvent((KMPLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.CustomerNoRowDeleting == null)
          return;
        this.CustomerNoRowDeleting((object) this, new KMPLogDataSet.CustomerNoRowChangeEvent((KMPLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveCustomerNoRow(KMPLogDataSet.CustomerNoRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        KMPLogDataSet kmpLogDataSet = new KMPLogDataSet();
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
          FixedValue = kmpLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "CustomerNoDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = kmpLogDataSet.GetSchemaSerializable();
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

    public delegate void RegistersRowChangeEventHandler(object sender, KMPLogDataSet.RegistersRowChangeEvent e);

    public delegate void RegisterInUseRowChangeEventHandler(object sender, KMPLogDataSet.RegisterInUseRowChangeEvent e);

    public delegate void RegisterUnitRowChangeEventHandler(object sender, KMPLogDataSet.RegisterUnitRowChangeEvent e);

    public delegate void CustomerNoRowChangeEventHandler(object sender, KMPLogDataSet.CustomerNoRowChangeEvent e);

    public delegate void DatetimeRecordRowChangeEventHandler(object sender, KMPLogDataSet.DatetimeRecordRowChangeEvent e);

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
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnVA;
      private DataColumn columnVB;
      private DataColumn columnM1;
      private DataColumn columnM2;
      private DataColumn columnT1;
      private DataColumn columnT2;
      private DataColumn columnT3;
      private DataColumn columnT4;
      private DataColumn columnT1_T2;
      private DataColumn columnFLOW1;
      private DataColumn columnFLOW2;
      private DataColumn columnEFFECT1;
      private DataColumn columnP1;
      private DataColumn columnP2;
      private DataColumn columnINFO;
      private DataColumn columnLogQOS;
      private DataColumn columnHR;

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
      public DataColumn VAColumn
      {
        get
        {
          return this.columnVA;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn VBColumn
      {
        get
        {
          return this.columnVB;
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
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn LogQOSColumn
      {
        get
        {
          return this.columnLogQOS;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn HRColumn
      {
        get
        {
          return this.columnHR;
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
      public KMPLogDataSet.RegisterInUseRow this[int index]
      {
        get
        {
          return (KMPLogDataSet.RegisterInUseRow) this.Rows[index];
        }
      }

      public event KMPLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanging;

      public event KMPLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanged;

      public event KMPLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleting;

      public event KMPLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleted;

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
      public void AddRegisterInUseRow(KMPLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegisterInUseRow AddRegisterInUseRow(bool E1, bool E2, bool E3, bool E4, bool E5, bool E6, bool E7, bool E8, bool E9, bool V1, bool V2, bool VA, bool VB, bool M1, bool M2, bool T1, bool T2, bool T3, bool T4, bool T1_T2, bool FLOW1, bool FLOW2, bool EFFECT1, bool P1, bool P2, bool INFO, bool LogQOS, bool HR)
      {
        KMPLogDataSet.RegisterInUseRow registerInUseRow = (KMPLogDataSet.RegisterInUseRow) this.NewRow();
        object[] objArray = new object[28]
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
          (object) (bool) (V1 ? 1 : 0),
          (object) (bool) (V2 ? 1 : 0),
          (object) (bool) (VA ? 1 : 0),
          (object) (bool) (VB ? 1 : 0),
          (object) (bool) (M1 ? 1 : 0),
          (object) (bool) (M2 ? 1 : 0),
          (object) (bool) (T1 ? 1 : 0),
          (object) (bool) (T2 ? 1 : 0),
          (object) (bool) (T3 ? 1 : 0),
          (object) (bool) (T4 ? 1 : 0),
          (object) (bool) (T1_T2 ? 1 : 0),
          (object) (bool) (FLOW1 ? 1 : 0),
          (object) (bool) (FLOW2 ? 1 : 0),
          (object) (bool) (EFFECT1 ? 1 : 0),
          (object) (bool) (P1 ? 1 : 0),
          (object) (bool) (P2 ? 1 : 0),
          (object) (bool) (INFO ? 1 : 0),
          (object) (bool) (LogQOS ? 1 : 0),
          (object) (bool) (HR ? 1 : 0)
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
        KMPLogDataSet.RegisterInUseDataTable registerInUseDataTable = (KMPLogDataSet.RegisterInUseDataTable) base.Clone();
        registerInUseDataTable.InitVars();
        return (DataTable) registerInUseDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new KMPLogDataSet.RegisterInUseDataTable();
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
        this.columnV1 = this.Columns["V1"];
        this.columnV2 = this.Columns["V2"];
        this.columnVA = this.Columns["VA"];
        this.columnVB = this.Columns["VB"];
        this.columnM1 = this.Columns["M1"];
        this.columnM2 = this.Columns["M2"];
        this.columnT1 = this.Columns["T1"];
        this.columnT2 = this.Columns["T2"];
        this.columnT3 = this.Columns["T3"];
        this.columnT4 = this.Columns["T4"];
        this.columnT1_T2 = this.Columns["T1_T2"];
        this.columnFLOW1 = this.Columns["FLOW1"];
        this.columnFLOW2 = this.Columns["FLOW2"];
        this.columnEFFECT1 = this.Columns["EFFECT1"];
        this.columnP1 = this.Columns["P1"];
        this.columnP2 = this.Columns["P2"];
        this.columnINFO = this.Columns["INFO"];
        this.columnLogQOS = this.Columns["LogQOS"];
        this.columnHR = this.Columns["HR"];
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
        this.columnV1 = new DataColumn("V1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV1);
        this.columnV2 = new DataColumn("V2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnV2);
        this.columnVA = new DataColumn("VA", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVA);
        this.columnVB = new DataColumn("VB", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVB);
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
        this.columnFLOW1 = new DataColumn("FLOW1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW1);
        this.columnFLOW2 = new DataColumn("FLOW2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnFLOW2);
        this.columnEFFECT1 = new DataColumn("EFFECT1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnEFFECT1);
        this.columnP1 = new DataColumn("P1", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP1);
        this.columnP2 = new DataColumn("P2", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnP2);
        this.columnINFO = new DataColumn("INFO", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnLogQOS = new DataColumn("LogQOS", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnLogQOS);
        this.columnHR = new DataColumn("HR", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnHR);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegisterInUseRow NewRegisterInUseRow()
      {
        return (KMPLogDataSet.RegisterInUseRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new KMPLogDataSet.RegisterInUseRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (KMPLogDataSet.RegisterInUseRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterInUseRowChanged == null)
          return;
        this.RegisterInUseRowChanged((object) this, new KMPLogDataSet.RegisterInUseRowChangeEvent((KMPLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterInUseRowChanging == null)
          return;
        this.RegisterInUseRowChanging((object) this, new KMPLogDataSet.RegisterInUseRowChangeEvent((KMPLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterInUseRowDeleted == null)
          return;
        this.RegisterInUseRowDeleted((object) this, new KMPLogDataSet.RegisterInUseRowChangeEvent((KMPLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterInUseRowDeleting == null)
          return;
        this.RegisterInUseRowDeleting((object) this, new KMPLogDataSet.RegisterInUseRowChangeEvent((KMPLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterInUseRow(KMPLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        KMPLogDataSet kmpLogDataSet = new KMPLogDataSet();
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
          FixedValue = kmpLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterInUseDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = kmpLogDataSet.GetSchemaSerializable();
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
    public class DatetimeRecordDataTable : DataTable, IEnumerable
    {
      private DataColumn columnRecord_Id;
      private DataColumn columnTimestamp;

      [DebuggerNonUserCode]
      public DataColumn Record_IdColumn
      {
        get
        {
          return this.columnRecord_Id;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn TimestampColumn
      {
        get
        {
          return this.columnTimestamp;
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
      public KMPLogDataSet.DatetimeRecordRow this[int index]
      {
        get
        {
          return (KMPLogDataSet.DatetimeRecordRow) this.Rows[index];
        }
      }

      public event KMPLogDataSet.DatetimeRecordRowChangeEventHandler DatetimeRecordRowChanging;

      public event KMPLogDataSet.DatetimeRecordRowChangeEventHandler DatetimeRecordRowChanged;

      public event KMPLogDataSet.DatetimeRecordRowChangeEventHandler DatetimeRecordRowDeleting;

      public event KMPLogDataSet.DatetimeRecordRowChangeEventHandler DatetimeRecordRowDeleted;

      [DebuggerNonUserCode]
      public DatetimeRecordDataTable()
      {
        this.TableName = "DatetimeRecord";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal DatetimeRecordDataTable(DataTable table)
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
      protected DatetimeRecordDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddDatetimeRecordRow(KMPLogDataSet.DatetimeRecordRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.DatetimeRecordRow AddDatetimeRecordRow(ushort Record_Id, DateTime Timestamp)
      {
        KMPLogDataSet.DatetimeRecordRow datetimeRecordRow = (KMPLogDataSet.DatetimeRecordRow) this.NewRow();
        object[] objArray = new object[2]
        {
          (object) Record_Id,
          (object) Timestamp
        };
        datetimeRecordRow.ItemArray = objArray;
        this.Rows.Add((DataRow) datetimeRecordRow);
        return datetimeRecordRow;
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.DatetimeRecordRow FindByRecord_Id(ushort Record_Id)
      {
        return (KMPLogDataSet.DatetimeRecordRow) this.Rows.Find(new object[1]
        {
          (object) Record_Id
        });
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        KMPLogDataSet.DatetimeRecordDataTable datetimeRecordDataTable = (KMPLogDataSet.DatetimeRecordDataTable) base.Clone();
        datetimeRecordDataTable.InitVars();
        return (DataTable) datetimeRecordDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new KMPLogDataSet.DatetimeRecordDataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnRecord_Id = this.Columns["Record_Id"];
        this.columnTimestamp = this.Columns["Timestamp"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnRecord_Id = new DataColumn("Record_Id", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnRecord_Id);
        this.columnTimestamp = new DataColumn("Timestamp", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnTimestamp);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
        {
          this.columnRecord_Id
        }, 1 != 0));
        this.columnRecord_Id.AllowDBNull = false;
        this.columnRecord_Id.Unique = true;
      }

      [DebuggerNonUserCode]
      public KMPLogDataSet.DatetimeRecordRow NewDatetimeRecordRow()
      {
        return (KMPLogDataSet.DatetimeRecordRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new KMPLogDataSet.DatetimeRecordRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (KMPLogDataSet.DatetimeRecordRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.DatetimeRecordRowChanged == null)
          return;
        this.DatetimeRecordRowChanged((object) this, new KMPLogDataSet.DatetimeRecordRowChangeEvent((KMPLogDataSet.DatetimeRecordRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.DatetimeRecordRowChanging == null)
          return;
        this.DatetimeRecordRowChanging((object) this, new KMPLogDataSet.DatetimeRecordRowChangeEvent((KMPLogDataSet.DatetimeRecordRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.DatetimeRecordRowDeleted == null)
          return;
        this.DatetimeRecordRowDeleted((object) this, new KMPLogDataSet.DatetimeRecordRowChangeEvent((KMPLogDataSet.DatetimeRecordRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.DatetimeRecordRowDeleting == null)
          return;
        this.DatetimeRecordRowDeleting((object) this, new KMPLogDataSet.DatetimeRecordRowChangeEvent((KMPLogDataSet.DatetimeRecordRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveDatetimeRecordRow(KMPLogDataSet.DatetimeRecordRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        KMPLogDataSet kmpLogDataSet = new KMPLogDataSet();
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
          FixedValue = kmpLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "DatetimeRecordDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = kmpLogDataSet.GetSchemaSerializable();
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
    public class RegistersRow : DataRow
    {
      private KMPLogDataSet.RegistersDataTable tableRegisters;

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
          this[this.tableRegisters.IdColumn] = (object) value;
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
          this[this.tableRegisters.DateColumn] = (object) value;
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
          this[this.tableRegisters.E1Column] = (object) value;
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
          this[this.tableRegisters.E2Column] = (object) value;
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
          this[this.tableRegisters.E3Column] = (object) value;
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
          this[this.tableRegisters.E4Column] = (object) value;
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
          this[this.tableRegisters.E5Column] = (object) value;
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
          this[this.tableRegisters.E6Column] = (object) value;
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
          this[this.tableRegisters.E7Column] = (object) value;
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
          this[this.tableRegisters.E8Column] = (object) value;
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
          this[this.tableRegisters.E9Column] = (object) value;
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
          this[this.tableRegisters.V1Column] = (object) value;
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
          this[this.tableRegisters.V2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal VA
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.VAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VA' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.VAColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal VB
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.VBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VB' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.VBColumn] = (object) value;
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
          this[this.tableRegisters.M1Column] = (object) value;
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
          this[this.tableRegisters.M2Column] = (object) value;
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
          this[this.tableRegisters.T1Column] = (object) value;
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
          this[this.tableRegisters.T2Column] = (object) value;
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
          this[this.tableRegisters.T3Column] = (object) value;
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
          this[this.tableRegisters.T4Column] = (object) value;
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
          this[this.tableRegisters.T1_T2Column] = (object) value;
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
          this[this.tableRegisters.FLOW1Column] = (object) value;
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
          this[this.tableRegisters.FLOW2Column] = (object) value;
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
          this[this.tableRegisters.EFFECT1Column] = (object) value;
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
          this[this.tableRegisters.P1Column] = (object) value;
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
          this[this.tableRegisters.P2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal INFO
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.INFOColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INFO' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.INFOColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal LogQOS
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.LogQOSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'LogQOS' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.LogQOSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal HR
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegisters.HRColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'HR' in table 'Registers' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisters.HRColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal RegistersRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisters = (KMPLogDataSet.RegistersDataTable) this.Table;
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
      public bool IsVANull()
      {
        return this.IsNull(this.tableRegisters.VAColumn);
      }

      [DebuggerNonUserCode]
      public void SetVANull()
      {
        this[this.tableRegisters.VAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsVBNull()
      {
        return this.IsNull(this.tableRegisters.VBColumn);
      }

      [DebuggerNonUserCode]
      public void SetVBNull()
      {
        this[this.tableRegisters.VBColumn] = Convert.DBNull;
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
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegisters.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegisters.INFOColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsLogQOSNull()
      {
        return this.IsNull(this.tableRegisters.LogQOSColumn);
      }

      [DebuggerNonUserCode]
      public void SetLogQOSNull()
      {
        this[this.tableRegisters.LogQOSColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsHRNull()
      {
        return this.IsNull(this.tableRegisters.HRColumn);
      }

      [DebuggerNonUserCode]
      public void SetHRNull()
      {
        this[this.tableRegisters.HRColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRow : DataRow
    {
      private KMPLogDataSet.RegisterInUseDataTable tableRegisterInUse;

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
      public bool VA
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.VAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VA' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.VAColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool VB
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.VBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VB' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.VBColumn] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.T1Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.T2Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.T3Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.T4Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.T1_T2Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.FLOW1Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.FLOW2Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.EFFECT1Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.P1Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.P2Column] = (object) (bool) (value ? 1 : 0);
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
      public bool LogQOS
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.LogQOSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'LogQOS' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.LogQOSColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool HR
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.HRColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'HR' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.HRColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      internal RegisterInUseRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterInUse = (KMPLogDataSet.RegisterInUseDataTable) this.Table;
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
      public bool IsVANull()
      {
        return this.IsNull(this.tableRegisterInUse.VAColumn);
      }

      [DebuggerNonUserCode]
      public void SetVANull()
      {
        this[this.tableRegisterInUse.VAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsVBNull()
      {
        return this.IsNull(this.tableRegisterInUse.VBColumn);
      }

      [DebuggerNonUserCode]
      public void SetVBNull()
      {
        this[this.tableRegisterInUse.VBColumn] = Convert.DBNull;
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
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegisterInUse.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegisterInUse.INFOColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsLogQOSNull()
      {
        return this.IsNull(this.tableRegisterInUse.LogQOSColumn);
      }

      [DebuggerNonUserCode]
      public void SetLogQOSNull()
      {
        this[this.tableRegisterInUse.LogQOSColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsHRNull()
      {
        return this.IsNull(this.tableRegisterInUse.HRColumn);
      }

      [DebuggerNonUserCode]
      public void SetHRNull()
      {
        this[this.tableRegisterInUse.HRColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterUnitRow : DataRow
    {
      private KMPLogDataSet.RegisterUnitDataTable tableRegisterUnit;

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
      public byte VA
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.VAColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VA' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.VAColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte VB
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.VBColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VB' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.VBColumn] = (object) value;
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
          this[this.tableRegisterUnit.T1Column] = (object) value;
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
          this[this.tableRegisterUnit.T2Column] = (object) value;
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
          this[this.tableRegisterUnit.T3Column] = (object) value;
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
          this[this.tableRegisterUnit.T4Column] = (object) value;
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
          this[this.tableRegisterUnit.T1_T2Column] = (object) value;
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
          this[this.tableRegisterUnit.FLOW1Column] = (object) value;
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
          this[this.tableRegisterUnit.FLOW2Column] = (object) value;
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
          this[this.tableRegisterUnit.EFFECT1Column] = (object) value;
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
          this[this.tableRegisterUnit.P1Column] = (object) value;
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
          this[this.tableRegisterUnit.P2Column] = (object) value;
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
      public byte LogQOS
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.LogQOSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'LogQOS' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.LogQOSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte HR
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.HRColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'HR' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.HRColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal RegisterUnitRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterUnit = (KMPLogDataSet.RegisterUnitDataTable) this.Table;
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
      public bool IsVANull()
      {
        return this.IsNull(this.tableRegisterUnit.VAColumn);
      }

      [DebuggerNonUserCode]
      public void SetVANull()
      {
        this[this.tableRegisterUnit.VAColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsVBNull()
      {
        return this.IsNull(this.tableRegisterUnit.VBColumn);
      }

      [DebuggerNonUserCode]
      public void SetVBNull()
      {
        this[this.tableRegisterUnit.VBColumn] = Convert.DBNull;
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
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegisterUnit.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegisterUnit.INFOColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsLogQOSNull()
      {
        return this.IsNull(this.tableRegisterUnit.LogQOSColumn);
      }

      [DebuggerNonUserCode]
      public void SetLogQOSNull()
      {
        this[this.tableRegisterUnit.LogQOSColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsHRNull()
      {
        return this.IsNull(this.tableRegisterUnit.HRColumn);
      }

      [DebuggerNonUserCode]
      public void SetHRNull()
      {
        this[this.tableRegisterUnit.HRColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRow : DataRow
    {
      private KMPLogDataSet.CustomerNoDataTable tableCustomerNo;

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
        this.tableCustomerNo = (KMPLogDataSet.CustomerNoDataTable) this.Table;
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
    public class DatetimeRecordRow : DataRow
    {
      private KMPLogDataSet.DatetimeRecordDataTable tableDatetimeRecord;

      [DebuggerNonUserCode]
      public ushort Record_Id
      {
        get
        {
          return (ushort) this[this.tableDatetimeRecord.Record_IdColumn];
        }
        set
        {
          this[this.tableDatetimeRecord.Record_IdColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime Timestamp
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableDatetimeRecord.TimestampColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Timestamp' in table 'DatetimeRecord' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableDatetimeRecord.TimestampColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal DatetimeRecordRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableDatetimeRecord = (KMPLogDataSet.DatetimeRecordDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsTimestampNull()
      {
        return this.IsNull(this.tableDatetimeRecord.TimestampColumn);
      }

      [DebuggerNonUserCode]
      public void SetTimestampNull()
      {
        this[this.tableDatetimeRecord.TimestampColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegistersRowChangeEvent : EventArgs
    {
      private KMPLogDataSet.RegistersRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegistersRow Row
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
      public RegistersRowChangeEvent(KMPLogDataSet.RegistersRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRowChangeEvent : EventArgs
    {
      private KMPLogDataSet.RegisterInUseRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegisterInUseRow Row
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
      public RegisterInUseRowChangeEvent(KMPLogDataSet.RegisterInUseRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterUnitRowChangeEvent : EventArgs
    {
      private KMPLogDataSet.RegisterUnitRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public KMPLogDataSet.RegisterUnitRow Row
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
      public RegisterUnitRowChangeEvent(KMPLogDataSet.RegisterUnitRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRowChangeEvent : EventArgs
    {
      private KMPLogDataSet.CustomerNoRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public KMPLogDataSet.CustomerNoRow Row
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
      public CustomerNoRowChangeEvent(KMPLogDataSet.CustomerNoRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class DatetimeRecordRowChangeEvent : EventArgs
    {
      private KMPLogDataSet.DatetimeRecordRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public KMPLogDataSet.DatetimeRecordRow Row
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
      public DatetimeRecordRowChangeEvent(KMPLogDataSet.DatetimeRecordRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
