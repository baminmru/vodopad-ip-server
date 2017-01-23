// Decompiled with JetBrains decompiler
// Type: MC601LogView.YearLogDataSet
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
  [XmlRoot("YearLogDataSet")]
  [DesignerCategory("code")]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [HelpKeyword("vs.data.DataSet")]
  [Serializable]
  public class YearLogDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private YearLogDataSet.RegisterUnitDataTable tableRegisterUnit;
    private YearLogDataSet.RegisterDataTable tableRegister;
    private YearLogDataSet.RegisterInUseDataTable tableRegisterInUse;
    private YearLogDataSet.CustomerNoDataTable tableCustomerNo;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public YearLogDataSet.RegisterUnitDataTable RegisterUnit
    {
      get
      {
        return this.tableRegisterUnit;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    public YearLogDataSet.RegisterDataTable Register
    {
      get
      {
        return this.tableRegister;
      }
    }

    [DebuggerNonUserCode]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public YearLogDataSet.RegisterInUseDataTable RegisterInUse
    {
      get
      {
        return this.tableRegisterInUse;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    public YearLogDataSet.CustomerNoDataTable CustomerNo
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

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    public YearLogDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected YearLogDataSet(SerializationInfo info, StreamingContext context)
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
          if (dataSet.Tables["RegisterUnit"] != null)
            base.Tables.Add((DataTable) new YearLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
          if (dataSet.Tables["Register"] != null)
            base.Tables.Add((DataTable) new YearLogDataSet.RegisterDataTable(dataSet.Tables["Register"]));
          if (dataSet.Tables["RegisterInUse"] != null)
            base.Tables.Add((DataTable) new YearLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
          if (dataSet.Tables["CustomerNo"] != null)
            base.Tables.Add((DataTable) new YearLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
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
      YearLogDataSet yearLogDataSet = (YearLogDataSet) base.Clone();
      yearLogDataSet.InitVars();
      yearLogDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) yearLogDataSet;
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
        if (dataSet.Tables["RegisterUnit"] != null)
          base.Tables.Add((DataTable) new YearLogDataSet.RegisterUnitDataTable(dataSet.Tables["RegisterUnit"]));
        if (dataSet.Tables["Register"] != null)
          base.Tables.Add((DataTable) new YearLogDataSet.RegisterDataTable(dataSet.Tables["Register"]));
        if (dataSet.Tables["RegisterInUse"] != null)
          base.Tables.Add((DataTable) new YearLogDataSet.RegisterInUseDataTable(dataSet.Tables["RegisterInUse"]));
        if (dataSet.Tables["CustomerNo"] != null)
          base.Tables.Add((DataTable) new YearLogDataSet.CustomerNoDataTable(dataSet.Tables["CustomerNo"]));
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
      this.tableRegisterUnit = (YearLogDataSet.RegisterUnitDataTable) base.Tables["RegisterUnit"];
      if (initTable && this.tableRegisterUnit != null)
        this.tableRegisterUnit.InitVars();
      this.tableRegister = (YearLogDataSet.RegisterDataTable) base.Tables["Register"];
      if (initTable && this.tableRegister != null)
        this.tableRegister.InitVars();
      this.tableRegisterInUse = (YearLogDataSet.RegisterInUseDataTable) base.Tables["RegisterInUse"];
      if (initTable && this.tableRegisterInUse != null)
        this.tableRegisterInUse.InitVars();
      this.tableCustomerNo = (YearLogDataSet.CustomerNoDataTable) base.Tables["CustomerNo"];
      if (!initTable || this.tableCustomerNo == null)
        return;
      this.tableCustomerNo.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "YearLogDataSet";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/DataSet1.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableRegisterUnit = new YearLogDataSet.RegisterUnitDataTable();
      base.Tables.Add((DataTable) this.tableRegisterUnit);
      this.tableRegister = new YearLogDataSet.RegisterDataTable();
      base.Tables.Add((DataTable) this.tableRegister);
      this.tableRegisterInUse = new YearLogDataSet.RegisterInUseDataTable();
      base.Tables.Add((DataTable) this.tableRegisterInUse);
      this.tableCustomerNo = new YearLogDataSet.CustomerNoDataTable();
      base.Tables.Add((DataTable) this.tableCustomerNo);
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegisterUnit()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeRegister()
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
      YearLogDataSet yearLogDataSet = new YearLogDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = yearLogDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = yearLogDataSet.GetSchemaSerializable();
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
      public YearLogDataSet.CustomerNoRow this[int index]
      {
        get
        {
          return (YearLogDataSet.CustomerNoRow) this.Rows[index];
        }
      }

      public event YearLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanging;

      public event YearLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowChanged;

      public event YearLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleting;

      public event YearLogDataSet.CustomerNoRowChangeEventHandler CustomerNoRowDeleted;

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
        return ((YearLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo;
      }

      public void SetCustomerNo(string customerNo)
      {
        if (this.Rows.Count == 0)
          this.AddCustomerNoRow(customerNo);
        else
          ((YearLogDataSet.CustomerNoRow) this.Rows[0]).CustomerNo = customerNo;
      }

      [DebuggerNonUserCode]
      public void AddCustomerNoRow(YearLogDataSet.CustomerNoRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.CustomerNoRow AddCustomerNoRow(string CustomerNo)
      {
        YearLogDataSet.CustomerNoRow customerNoRow = (YearLogDataSet.CustomerNoRow) this.NewRow();
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
        YearLogDataSet.CustomerNoDataTable customerNoDataTable = (YearLogDataSet.CustomerNoDataTable) base.Clone();
        customerNoDataTable.InitVars();
        return (DataTable) customerNoDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new YearLogDataSet.CustomerNoDataTable();
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
      public YearLogDataSet.CustomerNoRow NewCustomerNoRow()
      {
        return (YearLogDataSet.CustomerNoRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new YearLogDataSet.CustomerNoRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (YearLogDataSet.CustomerNoRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.CustomerNoRowChanged == null)
          return;
        this.CustomerNoRowChanged((object) this, new YearLogDataSet.CustomerNoRowChangeEvent((YearLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.CustomerNoRowChanging == null)
          return;
        this.CustomerNoRowChanging((object) this, new YearLogDataSet.CustomerNoRowChangeEvent((YearLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.CustomerNoRowDeleted == null)
          return;
        this.CustomerNoRowDeleted((object) this, new YearLogDataSet.CustomerNoRowChangeEvent((YearLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.CustomerNoRowDeleting == null)
          return;
        this.CustomerNoRowDeleting((object) this, new YearLogDataSet.CustomerNoRowChangeEvent((YearLogDataSet.CustomerNoRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveCustomerNoRow(YearLogDataSet.CustomerNoRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        YearLogDataSet yearLogDataSet = new YearLogDataSet();
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
          FixedValue = yearLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "CustomerNoDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = yearLogDataSet.GetSchemaSerializable();
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

    public delegate void RegisterUnitRowChangeEventHandler(object sender, YearLogDataSet.RegisterUnitRowChangeEvent e);

    public delegate void RegisterRowChangeEventHandler(object sender, YearLogDataSet.RegisterRowChangeEvent e);

    public delegate void RegisterInUseRowChangeEventHandler(object sender, YearLogDataSet.RegisterInUseRowChangeEvent e);

    public delegate void CustomerNoRowChangeEventHandler(object sender, YearLogDataSet.CustomerNoRowChangeEvent e);

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
      private DataColumn columnTA2;
      private DataColumn columnTA3;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnINFO;
      private DataColumn columnMaxFlow1DateYear;
      private DataColumn columnMaxFlow1Year;
      private DataColumn columnMinFlow1DateYear;
      private DataColumn columnMinFlow1Year;
      private DataColumn columnMaxEff1DateYear;
      private DataColumn columnMaxEff1Year;
      private DataColumn columnMinEff1DateYear;
      private DataColumn columnMinEff1Year;

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
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxFlow1DateYearColumn
      {
        get
        {
          return this.columnMaxFlow1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxFlow1YearColumn
      {
        get
        {
          return this.columnMaxFlow1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinFlow1DateYearColumn
      {
        get
        {
          return this.columnMinFlow1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinFlow1YearColumn
      {
        get
        {
          return this.columnMinFlow1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxEff1DateYearColumn
      {
        get
        {
          return this.columnMaxEff1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxEff1YearColumn
      {
        get
        {
          return this.columnMaxEff1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinEff1DateYearColumn
      {
        get
        {
          return this.columnMinEff1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinEff1YearColumn
      {
        get
        {
          return this.columnMinEff1Year;
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
      public YearLogDataSet.RegisterUnitRow this[int index]
      {
        get
        {
          return (YearLogDataSet.RegisterUnitRow) this.Rows[index];
        }
      }

      public event YearLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanging;

      public event YearLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowChanged;

      public event YearLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleting;

      public event YearLogDataSet.RegisterUnitRowChangeEventHandler RegisterUnitRowDeleted;

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
      public void AddRegisterUnitRow(YearLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterUnitRow AddRegisterUnitRow(byte E1, byte E2, byte E3, byte E4, byte E5, byte E6, byte E7, byte E8, byte E9, byte TA2, byte TA3, byte V1, byte V2, byte INA, byte INB, byte INFO, byte MaxFlow1DateYear, byte MaxFlow1Year, byte MinFlow1DateYear, byte MinFlow1Year, byte MaxEff1DateYear, byte MaxEff1Year, byte MinEff1DateYear, byte MinEff1Year)
      {
        YearLogDataSet.RegisterUnitRow registerUnitRow = (YearLogDataSet.RegisterUnitRow) this.NewRow();
        object[] objArray = new object[24]
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
          (object) TA2,
          (object) TA3,
          (object) V1,
          (object) V2,
          (object) INA,
          (object) INB,
          (object) INFO,
          (object) MaxFlow1DateYear,
          (object) MaxFlow1Year,
          (object) MinFlow1DateYear,
          (object) MinFlow1Year,
          (object) MaxEff1DateYear,
          (object) MaxEff1Year,
          (object) MinEff1DateYear,
          (object) MinEff1Year
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
        YearLogDataSet.RegisterUnitDataTable registerUnitDataTable = (YearLogDataSet.RegisterUnitDataTable) base.Clone();
        registerUnitDataTable.InitVars();
        return (DataTable) registerUnitDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new YearLogDataSet.RegisterUnitDataTable();
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
        this.columnINFO = this.Columns["INFO"];
        this.columnMaxFlow1DateYear = this.Columns["MaxFlow1DateYear"];
        this.columnMaxFlow1Year = this.Columns["MaxFlow1Year"];
        this.columnMinFlow1DateYear = this.Columns["MinFlow1DateYear"];
        this.columnMinFlow1Year = this.Columns["MinFlow1Year"];
        this.columnMaxEff1DateYear = this.Columns["MaxEff1DateYear"];
        this.columnMaxEff1Year = this.Columns["MaxEff1Year"];
        this.columnMinEff1DateYear = this.Columns["MinEff1DateYear"];
        this.columnMinEff1Year = this.Columns["MinEff1Year"];
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
        this.columnINFO = new DataColumn("INFO", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnMaxFlow1DateYear = new DataColumn("MaxFlow1DateYear", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxFlow1DateYear);
        this.columnMaxFlow1Year = new DataColumn("MaxFlow1Year", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxFlow1Year);
        this.columnMinFlow1DateYear = new DataColumn("MinFlow1DateYear", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinFlow1DateYear);
        this.columnMinFlow1Year = new DataColumn("MinFlow1Year", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinFlow1Year);
        this.columnMaxEff1DateYear = new DataColumn("MaxEff1DateYear", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxEff1DateYear);
        this.columnMaxEff1Year = new DataColumn("MaxEff1Year", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxEff1Year);
        this.columnMinEff1DateYear = new DataColumn("MinEff1DateYear", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinEff1DateYear);
        this.columnMinEff1Year = new DataColumn("MinEff1Year", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinEff1Year);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterUnitRow NewRegisterUnitRow()
      {
        return (YearLogDataSet.RegisterUnitRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new YearLogDataSet.RegisterUnitRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (YearLogDataSet.RegisterUnitRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterUnitRowChanged == null)
          return;
        this.RegisterUnitRowChanged((object) this, new YearLogDataSet.RegisterUnitRowChangeEvent((YearLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterUnitRowChanging == null)
          return;
        this.RegisterUnitRowChanging((object) this, new YearLogDataSet.RegisterUnitRowChangeEvent((YearLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterUnitRowDeleted == null)
          return;
        this.RegisterUnitRowDeleted((object) this, new YearLogDataSet.RegisterUnitRowChangeEvent((YearLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterUnitRowDeleting == null)
          return;
        this.RegisterUnitRowDeleting((object) this, new YearLogDataSet.RegisterUnitRowChangeEvent((YearLogDataSet.RegisterUnitRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterUnitRow(YearLogDataSet.RegisterUnitRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        YearLogDataSet yearLogDataSet = new YearLogDataSet();
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
          FixedValue = yearLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterUnitDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = yearLogDataSet.GetSchemaSerializable();
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
      private DataColumn columnTA2;
      private DataColumn columnTA3;
      private DataColumn columnV1;
      private DataColumn columnV2;
      private DataColumn columnINA;
      private DataColumn columnINB;
      private DataColumn columnINFO;
      private DataColumn columnMaxFlow1DateYear;
      private DataColumn columnMaxFlow1Year;
      private DataColumn columnMinFlow1DateYear;
      private DataColumn columnMinFlow1Year;
      private DataColumn columnMaxEff1DateYear;
      private DataColumn columnMaxEff1Year;
      private DataColumn columnMinEff1DateYear;
      private DataColumn columnMinEff1Year;

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
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxFlow1DateYearColumn
      {
        get
        {
          return this.columnMaxFlow1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxFlow1YearColumn
      {
        get
        {
          return this.columnMaxFlow1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinFlow1DateYearColumn
      {
        get
        {
          return this.columnMinFlow1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinFlow1YearColumn
      {
        get
        {
          return this.columnMinFlow1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxEff1DateYearColumn
      {
        get
        {
          return this.columnMaxEff1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxEff1YearColumn
      {
        get
        {
          return this.columnMaxEff1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinEff1DateYearColumn
      {
        get
        {
          return this.columnMinEff1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinEff1YearColumn
      {
        get
        {
          return this.columnMinEff1Year;
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
      public YearLogDataSet.RegisterRow this[int index]
      {
        get
        {
          return (YearLogDataSet.RegisterRow) this.Rows[index];
        }
      }

      public event YearLogDataSet.RegisterRowChangeEventHandler RegisterRowChanging;

      public event YearLogDataSet.RegisterRowChangeEventHandler RegisterRowChanged;

      public event YearLogDataSet.RegisterRowChangeEventHandler RegisterRowDeleting;

      public event YearLogDataSet.RegisterRowChangeEventHandler RegisterRowDeleted;

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
      public void AddRegisterRow(YearLogDataSet.RegisterRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterRow AddRegisterRow(int Id, DateTime Date, Decimal E1, Decimal E2, Decimal E3, Decimal E4, Decimal E5, Decimal E6, Decimal E7, Decimal E8, Decimal E9, Decimal TA2, Decimal TA3, Decimal V1, Decimal V2, Decimal INA, Decimal INB, Decimal INFO, DateTime MaxFlow1DateYear, Decimal MaxFlow1Year, DateTime MinFlow1DateYear, Decimal MinFlow1Year, DateTime MaxEff1DateYear, Decimal MaxEff1Year, DateTime MinEff1DateYear, Decimal MinEff1Year)
      {
        YearLogDataSet.RegisterRow registerRow = (YearLogDataSet.RegisterRow) this.NewRow();
        object[] objArray = new object[26]
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
          (object) TA2,
          (object) TA3,
          (object) V1,
          (object) V2,
          (object) INA,
          (object) INB,
          (object) INFO,
          (object) MaxFlow1DateYear,
          (object) MaxFlow1Year,
          (object) MinFlow1DateYear,
          (object) MinFlow1Year,
          (object) MaxEff1DateYear,
          (object) MaxEff1Year,
          (object) MinEff1DateYear,
          (object) MinEff1Year
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
        YearLogDataSet.RegisterDataTable registerDataTable = (YearLogDataSet.RegisterDataTable) base.Clone();
        registerDataTable.InitVars();
        return (DataTable) registerDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new YearLogDataSet.RegisterDataTable();
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
        this.columnINFO = this.Columns["INFO"];
        this.columnMaxFlow1DateYear = this.Columns["MaxFlow1DateYear"];
        this.columnMaxFlow1Year = this.Columns["MaxFlow1Year"];
        this.columnMinFlow1DateYear = this.Columns["MinFlow1DateYear"];
        this.columnMinFlow1Year = this.Columns["MinFlow1Year"];
        this.columnMaxEff1DateYear = this.Columns["MaxEff1DateYear"];
        this.columnMaxEff1Year = this.Columns["MaxEff1Year"];
        this.columnMinEff1DateYear = this.Columns["MinEff1DateYear"];
        this.columnMinEff1Year = this.Columns["MinEff1Year"];
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
        this.columnINFO = new DataColumn("INFO", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnMaxFlow1DateYear = new DataColumn("MaxFlow1DateYear", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxFlow1DateYear);
        this.columnMaxFlow1Year = new DataColumn("MaxFlow1Year", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxFlow1Year);
        this.columnMinFlow1DateYear = new DataColumn("MinFlow1DateYear", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinFlow1DateYear);
        this.columnMinFlow1Year = new DataColumn("MinFlow1Year", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinFlow1Year);
        this.columnMaxEff1DateYear = new DataColumn("MaxEff1DateYear", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxEff1DateYear);
        this.columnMaxEff1Year = new DataColumn("MaxEff1Year", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxEff1Year);
        this.columnMinEff1DateYear = new DataColumn("MinEff1DateYear", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinEff1DateYear);
        this.columnMinEff1Year = new DataColumn("MinEff1Year", typeof (Decimal), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinEff1Year);
        this.columnE1.DefaultValue = (object) new Decimal(0);
        this.columnE2.DefaultValue = (object) new Decimal(0);
        this.columnE3.DefaultValue = (object) new Decimal(0);
        this.columnE4.DefaultValue = (object) new Decimal(0);
        this.columnE5.DefaultValue = (object) new Decimal(0);
        this.columnE6.DefaultValue = (object) new Decimal(0);
        this.columnE7.DefaultValue = (object) new Decimal(0);
        this.columnE8.DefaultValue = (object) new Decimal(0);
        this.columnE9.DefaultValue = (object) new Decimal(0);
        this.columnTA2.DefaultValue = (object) new Decimal(0);
        this.columnTA3.DefaultValue = (object) new Decimal(0);
        this.columnV1.DefaultValue = (object) new Decimal(0);
        this.columnV2.DefaultValue = (object) new Decimal(0);
        this.columnINA.DefaultValue = (object) new Decimal(0);
        this.columnINB.DefaultValue = (object) new Decimal(0);
        this.columnINFO.DefaultValue = (object) new Decimal(0);
        this.columnMaxFlow1Year.DefaultValue = (object) new Decimal(0);
        this.columnMinFlow1Year.DefaultValue = (object) new Decimal(0);
        this.columnMaxEff1Year.DefaultValue = (object) new Decimal(0);
        this.columnMinEff1Year.DefaultValue = (object) new Decimal(0);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterRow NewRegisterRow()
      {
        return (YearLogDataSet.RegisterRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new YearLogDataSet.RegisterRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (YearLogDataSet.RegisterRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterRowChanged == null)
          return;
        this.RegisterRowChanged((object) this, new YearLogDataSet.RegisterRowChangeEvent((YearLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterRowChanging == null)
          return;
        this.RegisterRowChanging((object) this, new YearLogDataSet.RegisterRowChangeEvent((YearLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterRowDeleted == null)
          return;
        this.RegisterRowDeleted((object) this, new YearLogDataSet.RegisterRowChangeEvent((YearLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterRowDeleting == null)
          return;
        this.RegisterRowDeleting((object) this, new YearLogDataSet.RegisterRowChangeEvent((YearLogDataSet.RegisterRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterRow(YearLogDataSet.RegisterRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        YearLogDataSet yearLogDataSet = new YearLogDataSet();
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
          FixedValue = yearLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = yearLogDataSet.GetSchemaSerializable();
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
      private DataColumn columnINFO;
      private DataColumn columnMaxFlow1DateYear;
      private DataColumn columnMaxFlow1Year;
      private DataColumn columnMinFlow1DateYear;
      private DataColumn columnMinFlow1Year;
      private DataColumn columnMaxEff1DateYear;
      private DataColumn columnMaxEff1Year;
      private DataColumn columnMinEff1DateYear;
      private DataColumn columnMinEff1Year;

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
      public DataColumn INFOColumn
      {
        get
        {
          return this.columnINFO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxFlow1DateYearColumn
      {
        get
        {
          return this.columnMaxFlow1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxFlow1YearColumn
      {
        get
        {
          return this.columnMaxFlow1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinFlow1DateYearColumn
      {
        get
        {
          return this.columnMinFlow1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinFlow1YearColumn
      {
        get
        {
          return this.columnMinFlow1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxEff1DateYearColumn
      {
        get
        {
          return this.columnMaxEff1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MaxEff1YearColumn
      {
        get
        {
          return this.columnMaxEff1Year;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinEff1DateYearColumn
      {
        get
        {
          return this.columnMinEff1DateYear;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn MinEff1YearColumn
      {
        get
        {
          return this.columnMinEff1Year;
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
      public YearLogDataSet.RegisterInUseRow this[int index]
      {
        get
        {
          return (YearLogDataSet.RegisterInUseRow) this.Rows[index];
        }
      }

      public event YearLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanging;

      public event YearLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowChanged;

      public event YearLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleting;

      public event YearLogDataSet.RegisterInUseRowChangeEventHandler RegisterInUseRowDeleted;

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
      public void AddRegisterInUseRow(YearLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterInUseRow AddRegisterInUseRow(bool E1, bool E2, bool E3, bool E4, bool E5, bool E6, bool E7, bool E8, bool E9, bool TA2, bool TA3, bool V1, bool V2, bool INA, bool INB, bool INFO, bool MaxFlow1DateYear, bool MaxFlow1Year, bool MinFlow1DateYear, bool MinFlow1Year, bool MaxEff1DateYear, bool MaxEff1Year, bool MinEff1DateYear, bool MinEff1Year)
      {
        YearLogDataSet.RegisterInUseRow registerInUseRow = (YearLogDataSet.RegisterInUseRow) this.NewRow();
        object[] objArray = new object[24]
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
          (object) (bool) (TA2 ? 1 : 0),
          (object) (bool) (TA3 ? 1 : 0),
          (object) (bool) (V1 ? 1 : 0),
          (object) (bool) (V2 ? 1 : 0),
          (object) (bool) (INA ? 1 : 0),
          (object) (bool) (INB ? 1 : 0),
          (object) (bool) (INFO ? 1 : 0),
          (object) (bool) (MaxFlow1DateYear ? 1 : 0),
          (object) (bool) (MaxFlow1Year ? 1 : 0),
          (object) (bool) (MinFlow1DateYear ? 1 : 0),
          (object) (bool) (MinFlow1Year ? 1 : 0),
          (object) (bool) (MaxEff1DateYear ? 1 : 0),
          (object) (bool) (MaxEff1Year ? 1 : 0),
          (object) (bool) (MinEff1DateYear ? 1 : 0),
          (object) (bool) (MinEff1Year ? 1 : 0)
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
        YearLogDataSet.RegisterInUseDataTable registerInUseDataTable = (YearLogDataSet.RegisterInUseDataTable) base.Clone();
        registerInUseDataTable.InitVars();
        return (DataTable) registerInUseDataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new YearLogDataSet.RegisterInUseDataTable();
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
        this.columnINFO = this.Columns["INFO"];
        this.columnMaxFlow1DateYear = this.Columns["MaxFlow1DateYear"];
        this.columnMaxFlow1Year = this.Columns["MaxFlow1Year"];
        this.columnMinFlow1DateYear = this.Columns["MinFlow1DateYear"];
        this.columnMinFlow1Year = this.Columns["MinFlow1Year"];
        this.columnMaxEff1DateYear = this.Columns["MaxEff1DateYear"];
        this.columnMaxEff1Year = this.Columns["MaxEff1Year"];
        this.columnMinEff1DateYear = this.Columns["MinEff1DateYear"];
        this.columnMinEff1Year = this.Columns["MinEff1Year"];
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
        this.columnINFO = new DataColumn("INFO", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINFO);
        this.columnMaxFlow1DateYear = new DataColumn("MaxFlow1DateYear", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxFlow1DateYear);
        this.columnMaxFlow1Year = new DataColumn("MaxFlow1Year", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxFlow1Year);
        this.columnMinFlow1DateYear = new DataColumn("MinFlow1DateYear", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinFlow1DateYear);
        this.columnMinFlow1Year = new DataColumn("MinFlow1Year", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinFlow1Year);
        this.columnMaxEff1DateYear = new DataColumn("MaxEff1DateYear", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxEff1DateYear);
        this.columnMaxEff1Year = new DataColumn("MaxEff1Year", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMaxEff1Year);
        this.columnMinEff1DateYear = new DataColumn("MinEff1DateYear", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinEff1DateYear);
        this.columnMinEff1Year = new DataColumn("MinEff1Year", typeof (bool), (string) null, MappingType.Element);
        this.Columns.Add(this.columnMinEff1Year);
        this.Locale = new CultureInfo("en-GB");
      }

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterInUseRow NewRegisterInUseRow()
      {
        return (YearLogDataSet.RegisterInUseRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new YearLogDataSet.RegisterInUseRow(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (YearLogDataSet.RegisterInUseRow);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.RegisterInUseRowChanged == null)
          return;
        this.RegisterInUseRowChanged((object) this, new YearLogDataSet.RegisterInUseRowChangeEvent((YearLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.RegisterInUseRowChanging == null)
          return;
        this.RegisterInUseRowChanging((object) this, new YearLogDataSet.RegisterInUseRowChangeEvent((YearLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.RegisterInUseRowDeleted == null)
          return;
        this.RegisterInUseRowDeleted((object) this, new YearLogDataSet.RegisterInUseRowChangeEvent((YearLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.RegisterInUseRowDeleting == null)
          return;
        this.RegisterInUseRowDeleting((object) this, new YearLogDataSet.RegisterInUseRowChangeEvent((YearLogDataSet.RegisterInUseRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveRegisterInUseRow(YearLogDataSet.RegisterInUseRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        YearLogDataSet yearLogDataSet = new YearLogDataSet();
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
          FixedValue = yearLogDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "RegisterInUseDataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = yearLogDataSet.GetSchemaSerializable();
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
    public class RegisterUnitRow : DataRow
    {
      private YearLogDataSet.RegisterUnitDataTable tableRegisterUnit;

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
          this[this.tableRegisterUnit.TA2Column] = (object) value;
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
          this[this.tableRegisterUnit.TA3Column] = (object) value;
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
      public byte MaxFlow1DateYear
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MaxFlow1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxFlow1DateYear' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MaxFlow1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MaxFlow1Year
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MaxFlow1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxFlow1Year' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MaxFlow1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MinFlow1DateYear
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MinFlow1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinFlow1DateYear' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MinFlow1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MinFlow1Year
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MinFlow1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinFlow1Year' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MinFlow1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MaxEff1DateYear
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MaxEff1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxEff1DateYear' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MaxEff1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MaxEff1Year
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MaxEff1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxEff1Year' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MaxEff1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MinEff1DateYear
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MinEff1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinEff1DateYear' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MinEff1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public byte MinEff1Year
      {
        get
        {
          try
          {
            return (byte) this[this.tableRegisterUnit.MinEff1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinEff1Year' in table 'RegisterUnit' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterUnit.MinEff1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal RegisterUnitRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterUnit = (YearLogDataSet.RegisterUnitDataTable) this.Table;
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
      public bool IsMaxFlow1DateYearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MaxFlow1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxFlow1DateYearNull()
      {
        this[this.tableRegisterUnit.MaxFlow1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxFlow1YearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MaxFlow1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxFlow1YearNull()
      {
        this[this.tableRegisterUnit.MaxFlow1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinFlow1DateYearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MinFlow1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinFlow1DateYearNull()
      {
        this[this.tableRegisterUnit.MinFlow1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinFlow1YearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MinFlow1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinFlow1YearNull()
      {
        this[this.tableRegisterUnit.MinFlow1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxEff1DateYearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MaxEff1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxEff1DateYearNull()
      {
        this[this.tableRegisterUnit.MaxEff1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxEff1YearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MaxEff1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxEff1YearNull()
      {
        this[this.tableRegisterUnit.MaxEff1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinEff1DateYearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MinEff1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinEff1DateYearNull()
      {
        this[this.tableRegisterUnit.MinEff1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinEff1YearNull()
      {
        return this.IsNull(this.tableRegisterUnit.MinEff1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinEff1YearNull()
      {
        this[this.tableRegisterUnit.MinEff1YearColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterRow : DataRow
    {
      private YearLogDataSet.RegisterDataTable tableRegister;

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
      public Decimal TA2
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.TA2Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA2' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.TA2Column] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal TA3
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.TA3Column];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'TA3' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.TA3Column] = (object) value;
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
      public DateTime MaxFlow1DateYear
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableRegister.MaxFlow1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxFlow1DateYear' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MaxFlow1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal MaxFlow1Year
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.MaxFlow1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxFlow1Year' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MaxFlow1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime MinFlow1DateYear
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableRegister.MinFlow1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinFlow1DateYear' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MinFlow1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal MinFlow1Year
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.MinFlow1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinFlow1Year' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MinFlow1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime MaxEff1DateYear
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableRegister.MaxEff1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxEff1DateYear' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MaxEff1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal MaxEff1Year
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.MaxEff1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxEff1Year' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MaxEff1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime MinEff1DateYear
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableRegister.MinEff1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinEff1DateYear' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MinEff1DateYearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public Decimal MinEff1Year
      {
        get
        {
          try
          {
            return (Decimal) this[this.tableRegister.MinEff1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinEff1Year' in table 'Register' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegister.MinEff1YearColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal RegisterRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegister = (YearLogDataSet.RegisterDataTable) this.Table;
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
      public bool IsTA2Null()
      {
        return this.IsNull(this.tableRegister.TA2Column);
      }

      [DebuggerNonUserCode]
      public void SetTA2Null()
      {
        this[this.tableRegister.TA2Column] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsTA3Null()
      {
        return this.IsNull(this.tableRegister.TA3Column);
      }

      [DebuggerNonUserCode]
      public void SetTA3Null()
      {
        this[this.tableRegister.TA3Column] = Convert.DBNull;
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
      public bool IsINFONull()
      {
        return this.IsNull(this.tableRegister.INFOColumn);
      }

      [DebuggerNonUserCode]
      public void SetINFONull()
      {
        this[this.tableRegister.INFOColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxFlow1DateYearNull()
      {
        return this.IsNull(this.tableRegister.MaxFlow1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxFlow1DateYearNull()
      {
        this[this.tableRegister.MaxFlow1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxFlow1YearNull()
      {
        return this.IsNull(this.tableRegister.MaxFlow1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxFlow1YearNull()
      {
        this[this.tableRegister.MaxFlow1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinFlow1DateYearNull()
      {
        return this.IsNull(this.tableRegister.MinFlow1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinFlow1DateYearNull()
      {
        this[this.tableRegister.MinFlow1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinFlow1YearNull()
      {
        return this.IsNull(this.tableRegister.MinFlow1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinFlow1YearNull()
      {
        this[this.tableRegister.MinFlow1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxEff1DateYearNull()
      {
        return this.IsNull(this.tableRegister.MaxEff1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxEff1DateYearNull()
      {
        this[this.tableRegister.MaxEff1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxEff1YearNull()
      {
        return this.IsNull(this.tableRegister.MaxEff1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxEff1YearNull()
      {
        this[this.tableRegister.MaxEff1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinEff1DateYearNull()
      {
        return this.IsNull(this.tableRegister.MinEff1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinEff1DateYearNull()
      {
        this[this.tableRegister.MinEff1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinEff1YearNull()
      {
        return this.IsNull(this.tableRegister.MinEff1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinEff1YearNull()
      {
        this[this.tableRegister.MinEff1YearColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRow : DataRow
    {
      private YearLogDataSet.RegisterInUseDataTable tableRegisterInUse;

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
          this[this.tableRegisterInUse.TA2Column] = (object) (bool) (value ? 1 : 0);
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
          this[this.tableRegisterInUse.TA3Column] = (object) (bool) (value ? 1 : 0);
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
      public bool MaxFlow1DateYear
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MaxFlow1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxFlow1DateYear' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MaxFlow1DateYearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MaxFlow1Year
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MaxFlow1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxFlow1Year' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MaxFlow1YearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MinFlow1DateYear
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MinFlow1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinFlow1DateYear' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MinFlow1DateYearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MinFlow1Year
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MinFlow1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinFlow1Year' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MinFlow1YearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MaxEff1DateYear
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MaxEff1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxEff1DateYear' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MaxEff1DateYearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MaxEff1Year
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MaxEff1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MaxEff1Year' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MaxEff1YearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MinEff1DateYear
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MinEff1DateYearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinEff1DateYear' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MinEff1DateYearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      public bool MinEff1Year
      {
        get
        {
          try
          {
            return (bool) this[this.tableRegisterInUse.MinEff1YearColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'MinEff1Year' in table 'RegisterInUse' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableRegisterInUse.MinEff1YearColumn] = (object) (bool) (value ? 1 : 0);
        }
      }

      [DebuggerNonUserCode]
      internal RegisterInUseRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableRegisterInUse = (YearLogDataSet.RegisterInUseDataTable) this.Table;
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
      public bool IsMaxFlow1DateYearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MaxFlow1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxFlow1DateYearNull()
      {
        this[this.tableRegisterInUse.MaxFlow1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxFlow1YearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MaxFlow1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxFlow1YearNull()
      {
        this[this.tableRegisterInUse.MaxFlow1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinFlow1DateYearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MinFlow1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinFlow1DateYearNull()
      {
        this[this.tableRegisterInUse.MinFlow1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinFlow1YearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MinFlow1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinFlow1YearNull()
      {
        this[this.tableRegisterInUse.MinFlow1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxEff1DateYearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MaxEff1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxEff1DateYearNull()
      {
        this[this.tableRegisterInUse.MaxEff1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMaxEff1YearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MaxEff1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMaxEff1YearNull()
      {
        this[this.tableRegisterInUse.MaxEff1YearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinEff1DateYearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MinEff1DateYearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinEff1DateYearNull()
      {
        this[this.tableRegisterInUse.MinEff1DateYearColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsMinEff1YearNull()
      {
        return this.IsNull(this.tableRegisterInUse.MinEff1YearColumn);
      }

      [DebuggerNonUserCode]
      public void SetMinEff1YearNull()
      {
        this[this.tableRegisterInUse.MinEff1YearColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRow : DataRow
    {
      private YearLogDataSet.CustomerNoDataTable tableCustomerNo;

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
          this[this.tableCustomerNo.CustomerNoColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal CustomerNoRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableCustomerNo = (YearLogDataSet.CustomerNoDataTable) this.Table;
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
    public class RegisterUnitRowChangeEvent : EventArgs
    {
      private YearLogDataSet.RegisterUnitRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterUnitRow Row
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
      public RegisterUnitRowChangeEvent(YearLogDataSet.RegisterUnitRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterRowChangeEvent : EventArgs
    {
      private YearLogDataSet.RegisterRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterRow Row
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
      public RegisterRowChangeEvent(YearLogDataSet.RegisterRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class RegisterInUseRowChangeEvent : EventArgs
    {
      private YearLogDataSet.RegisterInUseRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public YearLogDataSet.RegisterInUseRow Row
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
      public RegisterInUseRowChangeEvent(YearLogDataSet.RegisterInUseRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class CustomerNoRowChangeEvent : EventArgs
    {
      private YearLogDataSet.CustomerNoRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public YearLogDataSet.CustomerNoRow Row
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
      public CustomerNoRowChangeEvent(YearLogDataSet.CustomerNoRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
