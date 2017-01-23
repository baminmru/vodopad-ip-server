// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmKMPLogger
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Kamstrup.Heat.mc601Communication;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace MC601LogView
{
  public class FrmKMPLogger : Form
  {
    private string m_DataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\kamstrup\\METERTOOLLogViewer601";
    private DateTime m_OldestDate = new DateTime(1990, 1, 1);
    private byte m_DestAddr = (byte) 127;
    private Functions m_mc601Functions = new Functions();
    private KMPLogDataSet m_KLDS = new KMPLogDataSet();
    private clsCalculatedRegisters m_userRegisters = new clsCalculatedRegisters();
    private eKMPLoggerType m_KMPLoggerType = eKMPLoggerType._Unknown;
    private string m_fileExtension = "";
    private string m_Title = "";
    private Thread thSplash;
    private FormLoadingData m_FormLoadingData;
    private KMPLogDataSet.RegisterInUseRow m_RegisterInUseRow;
    private KMPLogDataSet.RegisterUnitRow m_RegisterUnitRow;
    private bool bConnectionLost;
    private IContainer components;
    private GroupBox grpUsed;
    private UCRegisterCheckBox rcbCalc0;
    private GroupBox grpCalculatedRegisters;
    private Button btnRemoveAllCalculated;
    private Button btnRemoveCalculatedRegister;
    private FlowLayoutPanel flpCalculatedRegisters;
    private GroupBox grpCalculate;
    private Button btnAddToRegs;
    private Button btnCalculate;
    private ComboBox cbxRegister2;
    private ComboBox cbxCalcRule;
    private ComboBox cbxRegister1;
    private GroupBox grpGraphs;
    private Button btnSelectedRegisters;
    private GroupBox grpReadLog;
    private Label lblTo;
    private Label lblFrom;
    private Button btnClear;
    private Label lblRecords;
    private Label label3;
    private Button btnSave;
    private Button btnLoad;
    private Button btnRead;
    private GroupBox grpRegisters;
    private FlowLayoutPanel flpRegisters;
    private UCRegisterCheckBox rcbE1;
    private UCRegisterCheckBox rcbE7;
    private UCRegisterCheckBox rcbE3;
    private UCRegisterCheckBox rcbE4;
    private UCRegisterCheckBox rcbE5;
    private UCRegisterCheckBox rcbE6;
    private UCRegisterCheckBox rcbE2;
    private UCRegisterCheckBox rcbV1;
    private UCRegisterCheckBox rcbV2;
    private UCRegisterCheckBox rcbInA;
    private UCRegisterCheckBox rcbInB;
    private UCRegisterCheckBox rcbM1;
    private UCRegisterCheckBox rcbM2;
    private UCRegisterCheckBox rcbT1;
    private UCRegisterCheckBox rcbT2;
    private UCRegisterCheckBox rcbT3;
    private UCRegisterCheckBox rcbP1;
    private UCRegisterCheckBox rcbP2;
    private UCRegisterCheckBox rcbInfo;
    private Button btnSelectAll;
    private Button btnSelectNone;
    private UCRegisterCheckBox rcbE8;
    private UCRegisterCheckBox rcbE9;
    private UCRegisterCheckBox rcbT4;
    private UCRegisterCheckBox rcbT1_T2;
    private UCRegisterCheckBox rcbFLOW1;
    private UCRegisterCheckBox rcbFLOW2;
    private UCRegisterCheckBox rcbEFFECT1;
    private UCRegisterCheckBox rcbLogQOS;
    private UltraCombo cbxFrom;
    private UltraCombo cbxTo;
    private GroupBox groupBox1;
    private Button btnShowLogDetails;
    private UCRegisterCheckBox rcbHR;
    private RadioButton rdbCalcB;
    private RadioButton rdbCalcA;
    private NumericUpDown nudCalc;

    private int Progress
    {
      get
      {
        return ((FrmMC601LogView) this.MdiParent).ProgressBar;
      }
      set
      {
        ((FrmMC601LogView) this.MdiParent).ProgressBar = Math.Min(value, this.ProgressMax);
      }
    }

    private int ProgressMax
    {
      get
      {
        return ((FrmMC601LogView) this.MdiParent).ProgressBarMax;
      }
      set
      {
        ((FrmMC601LogView) this.MdiParent).ProgressBarMax = value;
      }
    }

    public FrmKMPLogger(eKMPLoggerType KMPLoggerType)
    {
      this.InitializeComponent();
      this.thSplash = new Thread(new ThreadStart(this.DoSplash));
      this.thSplash.Start();
      this.m_KMPLoggerType = KMPLoggerType;
      switch (this.m_KMPLoggerType)
      {
        case eKMPLoggerType._670B:
          this.m_fileExtension = "KMPLog670B";
          this.m_Title = "Top Module KMP Logger";
          this.m_DestAddr = (byte) 127;
          break;
        case eKMPLoggerType._670022:
          this.m_fileExtension = "KMPLog670022";
          this.m_Title = "Bottom Module KMP Logger";
          this.m_DestAddr = (byte) 191;
          break;
      }
      this.m_userRegisters.Load(this.m_fileExtension + "UR");
      this.InsertCalculatedRegister();
      this.ClearData();
    }

    private void DoSplash()
    {
      this.m_FormLoadingData = new FormLoadingData();
      int num = (int) this.m_FormLoadingData.ShowDialog();
    }

    private void InsertCalculatedRegister()
    {
      foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
      {
        clsCalculatedRegister ur = obj as clsCalculatedRegister;
        if (ur != null)
          this.addUserRegisterToForm(ur);
      }
    }

    private void MakeListOfDays()
    {
      ArrayList arrRegisters = new ArrayList();
      ArrayList arrUnits = new ArrayList();
      ArrayList arrValues = new ArrayList();
      arrRegisters.Add( 4144);
      arrRegisters.Add( 4145);
      string ErrorMessage = "";
      if (this.m_mc601Functions.GetData(this.m_DestAddr, (byte) 2, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
      {
        if (arrValues.Count == 0)
        {
          int num = (int) MessageBox.Show("There isn't any data stored in this KMP Log Module.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.thSplash.Abort();
          this.SetDefaultCursor();
        }
        else
        {
          ushort newestRecordId = Convert.ToUInt16(arrValues[0]);
          ushort num1 = Convert.ToUInt16(arrValues[1]);
          uint num2 = (uint) newestRecordId;
          if ((int) num1 > (int) newestRecordId)
            num2 += 65536U;
          this.m_FormLoadingData.ProgressBar = 0;
          this.m_FormLoadingData.ProgressBarMax = Convert.ToInt32(num2 - (uint) num1);
          this.m_KLDS.DatetimeRecord.Clear();
          try
          {
            ushort[] registers = new ushort[2];
            byte info = (byte) 0;
            ushort lastRecordId = (ushort) 0;
            registers[0] = (ushort) 1003;
            registers[1] = (ushort) 1002;
            ushort num3 = num1;
            if ((int) newestRecordId - (int) num3 > 500)
              num3 += (ushort) 5;
            uint num4 = (uint) num3;
            bool flag = true;
            for (; flag; flag = num4 <= num2)
            {
              byte noOfRecords = Convert.ToByte(Math.Min((uint) ((int) num2 - (int) num4 + 1), 9U));
              csKMPLogRegisters KMPLogRegisters = new csKMPLogRegisters();
              if (this.m_mc601Functions.GetLogFromRecordIdTowardsPresent(this.m_DestAddr, (byte) 1, registers, noOfRecords, num3, ref lastRecordId, ref newestRecordId, ref info, ref KMPLogRegisters, out ErrorMessage))
              {
                for (int index = 0; index < KMPLogRegisters.GetKMPLogRegister((ushort) 1003).Records.Length; ++index)
                {
                  string str1 = KMPLogRegisters.GetKMPLogRegister((ushort) 1003).Records[index].ToString("000000");
                  string str2 = KMPLogRegisters.GetKMPLogRegister((ushort) 1002).Records[index].ToString("000000");
                  DateTime Timestamp = new DateTime(2000 + Convert.ToInt32(str1.Substring(0, 2)), Convert.ToInt32(str1.Substring(2, 2)), Convert.ToInt32(str1.Substring(4, 2)), Convert.ToInt32(str2.Substring(0, 2)), Convert.ToInt32(str2.Substring(2, 2)), Convert.ToInt32(str2.Substring(4, 2)));
                  this.m_KLDS.DatetimeRecord.AddDatetimeRecordRow(num3, Timestamp);
                  ++num3;
                  ++num4;
                  ++this.m_FormLoadingData.ProgressBar;
                }
              }
            }
            this.cbxFrom.DataSource =  (KMPLogDataSet.DatetimeRecordDataTable) this.m_KLDS.DatetimeRecord.Copy();
            this.cbxTo.DataSource =  (KMPLogDataSet.DatetimeRecordDataTable) this.m_KLDS.DatetimeRecord.Copy();
            this.cbxFrom.DisplayLayout.Bands[0].Columns["Timestamp"].Format = "yyyy-MM-dd HH:mm";
            this.cbxFrom.DisplayLayout.Bands[0].Columns["Record_Id"].Hidden = true;
            this.cbxFrom.DisplayMember = "Timestamp";
            this.cbxFrom.ValueMember = "Record_Id";
            this.cbxTo.DisplayLayout.Bands[0].Columns["Timestamp"].Format = "yyyy-MM-dd HH:mm";
            this.cbxTo.DisplayLayout.Bands[0].Columns["Record_Id"].Hidden = true;
            this.cbxTo.DisplayMember = "Timestamp";
            this.cbxTo.ValueMember = "Record_Id";
            this.EnableRead();
          }
          catch
          {
            int num3 = (int) MessageBox.Show("Unable to read timestamps from meter.", "Unable to read data", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          this.thSplash.Abort();
          this.SetDefaultCursor();
        }
      }
      else
      {
        int num = (int) MessageBox.Show("There isn't any data stored in this KMP Log Module.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.thSplash.Abort();
        this.SetDefaultCursor();
      }
    }

    private void SetDefaultCursor()
    {
      if (this.btnRead.InvokeRequired)
        this.Invoke((Delegate) new FrmKMPLogger.dEmpty(this.SetDefaultCursor));
      else
        this.Cursor = Cursors.Default;
    }

    private void EnableRead()
    {
      if (this.btnRead.InvokeRequired)
        this.Invoke((Delegate) new FrmKMPLogger.dEmpty(this.EnableRead));
      else
        this.btnRead.Enabled = true;
    }

    private void btnSelectedRegisters_Click(object sender, EventArgs e)
    {
      this.SelectedRegisters();
    }

    private void SelectedRegisters()
    {
      DataTable dataTable = new DataSet("GraphDataSet").Tables.Add("AllValues");
      dataTable.Columns.Add("Date", typeof (DateTime));
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null && registerCheckBox.Checked && registerCheckBox.Enabled)
        {
          byte nUnit = Convert.ToByte(this.m_RegisterUnitRow[registerCheckBox.RegisterName]);
          registerCheckBox.UnitText = ClsUtils.UnitsForRegisters(nUnit);
          dataTable.Columns.Add(registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]", typeof (double));
        }
      }
      if (this.rcbCalc0.Checked && this.rcbCalc0.Enabled)
      {
        this.rcbCalc0.UnitText = ClsUtils.UnitsForRegisters(Convert.ToByte(this.m_RegisterUnitRow[this.rcbCalc0.RegisterName]));
        dataTable.Columns.Add(this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]", typeof (double));
      }
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
        if (rcb != null)
          this.AddCalculatedColumnForRCB(dataTable, rcb);
      }
      KMPLogDataSet.RegistersRow registersRow = (KMPLogDataSet.RegistersRow) null;
      foreach (KMPLogDataSet.RegistersRow row in this.m_KLDS.Registers)
      {
        DataRow dataRow = dataTable.NewRow();
        dataRow["Date"] = row["Date"];
        foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
        {
          UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
          if (registerCheckBox != null && registerCheckBox.Checked && registerCheckBox.Enabled)
          {
            double num = Convert.ToDouble(row[registerCheckBox.RegisterName]);
            dataRow[registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]"] =  num;
          }
        }
        if (this.rcbCalc0.Checked && this.rcbCalc0.Enabled)
        {
          if (registersRow != null)
          {
            Decimal num1 = Convert.ToDecimal(registersRow[this.rcbCalc0.RegisterName]);
            Decimal num2 = Convert.ToDecimal(row[this.rcbCalc0.RegisterName]);
            dataRow[this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]"] = num2 == new Decimal(0) || num1 == new Decimal(0) ?  0 :  (num1 - num2);
          }
          else
            dataRow[this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]"] =  0;
          registersRow = row;
        }
        foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
        {
          UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
          if (rcb != null)
            FrmKMPLogger.CalculateCalculatedRowForRCB(dataRow, rcb, row);
        }
        dataTable.Rows.InsertAt(dataRow, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_KLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.ShowTime = true;
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_KLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.ShowTime = true;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private static void CalculateCalculatedRowForRCB(DataRow dr, UCRegisterCheckBox rcb, KMPLogDataSet.RegistersRow row)
    {
      if (!rcb.Checked || !rcb.Enabled)
        return;
      if (rcb.UseRegister2)
      {
        Decimal num = FrmKMPLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), Convert.ToDecimal(row[rcb.RegisterName2]), rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.RegisterName2] =  num;
      }
      else
      {
        Decimal num = FrmKMPLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), rcb.CalculationValue, rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.CalculationValue.ToString()] =  num;
      }
    }

    private void AddCalculatedColumnForRCB(DataTable AllValues, UCRegisterCheckBox rcb)
    {
      if (!rcb.Checked || !rcb.Enabled)
        return;
      if (rcb.UseRegister2)
      {
        DataColumn column = new DataColumn(rcb.RegisterName + rcb.CalculationRule + rcb.RegisterName2, typeof (double));
        byte nUnit1 = Convert.ToByte(this.m_RegisterUnitRow[rcb.RegisterName]);
        byte nUnit2 = Convert.ToByte(this.m_RegisterUnitRow[rcb.RegisterName2]);
        string str = rcb.RegisterCaption1 + " [" + ClsUtils.UnitsForRegisters(nUnit1) + "] " + rcb.CalculationRule + " " + rcb.RegisterCaption2 + " [" + ClsUtils.UnitsForRegisters(nUnit2) + "]";
        column.Caption = str;
        AllValues.Columns.Add(column);
      }
      else
      {
        DataColumn column = new DataColumn(rcb.RegisterName + rcb.CalculationRule + rcb.CalculationValue.ToString(), typeof (double));
        byte nUnit = Convert.ToByte(this.m_RegisterUnitRow[rcb.RegisterName]);
        string str = rcb.RegisterCaption1 + " [" + ClsUtils.UnitsForRegisters(nUnit) + "] " + rcb.CalculationRule + " " + rcb.CalculationValue.ToString();
        column.Caption = str;
        AllValues.Columns.Add(column);
      }
    }

    private eKMPLoggerType GetCurrentTopModuleType()
    {
      eKMPLoggerType eKmpLoggerType = eKMPLoggerType._Unknown;
      try
      {
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add( 157);
        string ErrorMessage = "";
        if (this.m_mc601Functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
        {
          if ((double) arrValues[0] == 67110000.0)
            eKmpLoggerType = eKMPLoggerType._670B;
        }
      }
      catch
      {
      }
      return eKmpLoggerType;
    }

    private eKMPLoggerType GetCurrentBottomModuleType()
    {
      eKMPLoggerType eKmpLoggerType = eKMPLoggerType._Unknown;
      try
      {
        ArrayList arrayList1 = new ArrayList();
        ArrayList arrTypeRev = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        arrayList1.Add( 157);
        string ErrorMessage = "";
        if (this.m_mc601Functions.GetType(this.m_DestAddr, out arrTypeRev, out ErrorMessage))
        {
          if ((int) Convert.ToByte(arrTypeRev[0]) == 22)
          {
            if ((int) Convert.ToByte(arrTypeRev[1]) == 10)
              eKmpLoggerType = eKMPLoggerType._670022;
          }
        }
      }
      catch
      {
      }
      return eKmpLoggerType;
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (!this.IsAnyRCBChecked())
      {
        int num1 = (int) MessageBox.Show("Select one or more registers.", "Select registers", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        this.Cursor = Cursors.WaitCursor;
        ushort num2 = (ushort) this.cbxFrom.Value;
        ushort num3 = (ushort) this.cbxTo.Value;
        if ((int) num2 < (int) num3)
          this.ReadData(num2, num3);
        else
          this.ReadData(num3, num2);
        this.Progress = 0;
        this.SetupCheckableRCBs();
        if (this.m_KLDS.Registers.Rows.Count > 0)
        {
          this.Text = this.m_Title + " | Serial No " + this.m_KLDS.CustomerNo.GetCustomerNo() + " | Not saved.";
          this.btnSave.Enabled = true;
          if (MessageBox.Show("Reading of data completed.\r\nDo you wish to save the data?", "Reading completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            this.Save();
          this.btnCalculate.Enabled = true;
          this.btnAddToRegs.Enabled = true;
          this.btnSelectedRegisters.Enabled = true;
          if (this.m_RegisterInUseRow.INFO || this.m_RegisterInUseRow.LogQOS)
            this.btnShowLogDetails.Enabled = true;
        }
        else
          this.ClearData();
        this.Cursor = Cursors.Default;
      }
    }

    private void SetupCheckableRCBs()
    {
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
        {
          bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName];
          if (flag)
          {
            CbxItem cbxItem = new CbxItem();
            cbxItem.Register = registerCheckBox;
            this.cbxRegister1.Items.Add( cbxItem);
            this.cbxRegister2.Items.Add( cbxItem);
          }
          registerCheckBox.Enabled = flag;
        }
      }
      this.rcbCalc0.Enabled = (bool) this.m_RegisterInUseRow[this.rcbCalc0.RegisterName] && this.m_KLDS.Registers.Count > 0;
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
        {
          if (registerCheckBox.UseRegister2)
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName2] && this.m_KLDS.Registers.Count > 0;
            registerCheckBox.Enabled = flag;
          }
          else
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && this.m_KLDS.Registers.Count > 0;
            registerCheckBox.Enabled = flag;
          }
        }
      }
    }

    private void ReadData(ushort fromRecord, ushort toRecord)
    {
      this.bConnectionLost = false;
      this.m_KLDS.Registers.Clear();
      this.m_KLDS.RegisterUnit.Clear();
      this.m_KLDS.RegisterInUse.Clear();
      this.m_KLDS.CustomerNo.Clear();
      this.m_RegisterUnitRow = this.m_KLDS.RegisterUnit.NewRegisterUnitRow();
      this.m_KLDS.RegisterUnit.AddRegisterUnitRow(this.m_RegisterUnitRow);
      this.m_RegisterInUseRow = this.m_KLDS.RegisterInUse.NewRegisterInUseRow();
      this.m_KLDS.RegisterInUse.AddRegisterInUseRow(this.m_RegisterInUseRow);
      this.m_RegisterInUseRow.E1 = this.rcbE1.Checked;
      this.m_RegisterInUseRow.E2 = this.rcbE2.Checked;
      this.m_RegisterInUseRow.E3 = this.rcbE3.Checked;
      this.m_RegisterInUseRow.E4 = this.rcbE4.Checked;
      this.m_RegisterInUseRow.E5 = this.rcbE5.Checked;
      this.m_RegisterInUseRow.E6 = this.rcbE6.Checked;
      this.m_RegisterInUseRow.E7 = this.rcbE7.Checked;
      this.m_RegisterInUseRow.E8 = this.rcbE8.Checked;
      this.m_RegisterInUseRow.E9 = this.rcbE9.Checked;
      this.m_RegisterInUseRow.V1 = this.rcbV1.Checked;
      this.m_RegisterInUseRow.V2 = this.rcbV2.Checked;
      this.m_RegisterInUseRow.VA = this.rcbInA.Checked;
      this.m_RegisterInUseRow.VB = this.rcbInB.Checked;
      this.m_RegisterInUseRow.M1 = this.rcbM1.Checked;
      this.m_RegisterInUseRow.M2 = this.rcbM2.Checked;
      this.m_RegisterInUseRow.T1 = this.rcbT1.Checked;
      this.m_RegisterInUseRow.T2 = this.rcbT2.Checked;
      this.m_RegisterInUseRow.T3 = this.rcbT3.Checked;
      this.m_RegisterInUseRow.T4 = this.rcbT4.Checked;
      this.m_RegisterInUseRow.T1_T2 = this.rcbT1_T2.Checked;
      this.m_RegisterInUseRow.FLOW1 = this.rcbFLOW1.Checked;
      this.m_RegisterInUseRow.FLOW2 = this.rcbFLOW2.Checked;
      this.m_RegisterInUseRow.EFFECT1 = this.rcbEFFECT1.Checked;
      this.m_RegisterInUseRow.P1 = this.rcbP1.Checked;
      this.m_RegisterInUseRow.P2 = this.rcbP2.Checked;
      this.m_RegisterInUseRow.INFO = this.rcbInfo.Checked;
      this.m_RegisterInUseRow.LogQOS = this.rcbLogQOS.Checked;
      this.m_RegisterInUseRow.HR = this.rcbHR.Checked;
      this.Progress = 0;
      int num1 = this.NoOfRegistersInUse();
      this.ProgressMax = (int) toRecord < (int) fromRecord ? (65536 + (int) toRecord - (int) fromRecord) * num1 : ((int) toRecord - (int) fromRecord) * num1;
      this.ReadCustomerNo(ref this.bConnectionLost);
      foreach (KMPLogDataSet.DatetimeRecordRow datetimeRecordRow in this.m_KLDS.DatetimeRecord)
      {
        if ((int) datetimeRecordRow.Record_Id >= (int) fromRecord && (int) datetimeRecordRow.Record_Id <= (int) toRecord)
          this.m_KLDS.Registers.AddRegistersRow((int) datetimeRecordRow.Record_Id, datetimeRecordRow.Timestamp, new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0), new Decimal(0));
      }
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE3);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE4);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE5);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE6);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE7);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE8);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbE9);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbV1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbV2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbInA);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbInB);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbM1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbM2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbT1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbT2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbT3);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbT4);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbT1_T2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbFLOW1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbFLOW2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbEFFECT1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbP1);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbP2);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbInfo);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbLogQOS);
      this.ReadDataForRegister(fromRecord, toRecord, this.rcbHR);
      if (this.bConnectionLost)
      {
        int num2 = (int) MessageBox.Show("Lost the connection to the MC601 Meter!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.m_KLDS.Registers.Clear();
        this.lblRecords.Text = this.m_KLDS.Registers.Count.ToString();
      }
      else
        this.lblRecords.Text = this.m_KLDS.Registers.Count.ToString();
    }

    private int NoOfRegistersInUse()
    {
      int num = 0;
      foreach (bool flag in this.m_RegisterInUseRow.ItemArray)
      {
        if (flag)
          ++num;
      }
      return num;
    }

    private void ReadDataForRegister(ushort fromRecord, ushort toRecord, UCRegisterCheckBox rcb)
    {
      if (!rcb.Checked)
        return;
      ushort newestRecordId = (ushort) 0;
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      ArrayList arrayList3 = new ArrayList();
      string ErrorMessage = "";
      uint num1 = (uint) toRecord;
      if ((int) fromRecord > (int) toRecord)
        num1 += 65536U;
      try
      {
        ushort[] registers = new ushort[1];
        byte info = (byte) 0;
        ushort lastRecordId = (ushort) 0;
        ushort RegisterId = Convert.ToUInt16(rcb.RegisterId);
        registers[0] = RegisterId;
        ushort fromRecordId = fromRecord;
        uint num2 = (uint) fromRecordId;
        bool flag1 = true;
        bool flag2 = true;
        for (; flag1; flag1 = num2 <= num1)
        {
          byte noOfRecords = Convert.ToByte(Math.Min((uint) ((int) num1 - (int) num2 + 1), 17U));
          csKMPLogRegisters KMPLogRegisters = new csKMPLogRegisters();
          if (this.m_mc601Functions.GetLogFromRecordIdTowardsPresent(this.m_DestAddr, (byte) 1, registers, noOfRecords, fromRecordId, ref lastRecordId, ref newestRecordId, ref info, ref KMPLogRegisters, out ErrorMessage))
          {
            if (flag2)
            {
              this.m_RegisterUnitRow[rcb.RegisterName] =  KMPLogRegisters.GetKMPLogRegister(RegisterId).Unit;
              flag2 = false;
            }
            for (int index = 0; index < KMPLogRegisters.GetKMPLogRegister(RegisterId).Records.Length; ++index)
            {
              this.m_KLDS.Registers.GetRow((int) fromRecordId)[rcb.RegisterName] =  KMPLogRegisters.GetKMPLogRegister(RegisterId).Records[index];
              ++fromRecordId;
              ++num2;
              ++this.Progress;
            }
          }
        }
      }
      catch
      {
        int num2 = (int) MessageBox.Show("Unable to read for register " + rcb.RegisterName + ".", "Unable to read data", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void ReadCustomerNo(ref bool bConnectionLost)
    {
      try
      {
        Functions functions = new Functions();
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add( 1010);
        arrRegisters.Add( 112);
        arrRegisters.Add( 157);
        string ErrorMessage = "";
        if (functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
          this.m_KLDS.CustomerNo.SetCustomerNo(Convert.ToString((double) arrValues[0] + (double) arrValues[1]));
        else
          bConnectionLost = true;
      }
      catch
      {
        bConnectionLost = true;
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      if (this.btnSave.Enabled && MessageBox.Show("Do you wish to save the data,\r\nbefore you clear?", "Save data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.Save();
      this.ClearData();
    }

    private void ClearData()
    {
      this.m_KLDS.Registers.Clear();
      this.m_KLDS.RegisterInUse.Clear();
      this.m_KLDS.RegisterUnit.Clear();
      this.cbxRegister1.Items.Clear();
      this.cbxRegister2.Items.Clear();
      this.lblRecords.Text = this.m_KLDS.Registers.Count.ToString();
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = true;
      }
      this.rcbCalc0.Enabled = false;
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = false;
      }
      this.Text = this.m_Title + " | Serial No " + this.m_KLDS.CustomerNo.GetCustomerNo() + " | Empty.";
      this.btnSave.Enabled = false;
      this.btnCalculate.Enabled = false;
      this.btnAddToRegs.Enabled = false;
      this.btnSelectedRegisters.Enabled = false;
      this.btnShowLogDetails.Enabled = false;
    }

    private static Decimal DoCalculation(Decimal register1, Decimal register2, string CalcRule)
    {
      Decimal d = new Decimal(0);
      if (CalcRule == "-")
        d = register1 - register2;
      else if (CalcRule == "+")
        d = register1 + register2;
      else if (CalcRule == "*")
        d = register1 * register2;
      else if (CalcRule == "/")
        d = !(register2 == new Decimal(0)) ? register1 / register2 : new Decimal(0);
      return Math.Round(d, 2);
    }

    private void btnSelectAll_Click(object sender, EventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Checked = registerCheckBox.Enabled;
      }
    }

    private void btnSelectNone_Click(object sender, EventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Checked = false;
      }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      if (this.btnSave.Enabled && MessageBox.Show("Do you wish to save the data,\r\nbefore you load a new set of data?", "Save data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.Save();
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
        Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
      openFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
      openFileDialog.Filter = "MC601 KMP Log (*." + this.m_fileExtension + ")|*." + this.m_fileExtension + "|All files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        this.m_KLDS.DatetimeRecord.Rows.Clear();
        int num = (int) this.m_KLDS.ReadXml(openFileDialog.FileName);
        this.m_RegisterInUseRow = (KMPLogDataSet.RegisterInUseRow) this.m_KLDS.RegisterInUse.Rows[0];
        this.m_RegisterUnitRow = (KMPLogDataSet.RegisterUnitRow) this.m_KLDS.RegisterUnit.Rows[0];
        this.lblRecords.Text = this.m_KLDS.Registers.Count.ToString();
        this.btnRead.Text = "Start";
        this.SetupCheckableRCBs();
        this.btnSave.Enabled = false;
        this.btnCalculate.Enabled = true;
        this.btnAddToRegs.Enabled = true;
        this.btnSelectedRegisters.Enabled = true;
        if (this.m_RegisterInUseRow.INFO || this.m_RegisterInUseRow.LogQOS)
          this.btnShowLogDetails.Enabled = true;
        this.cbxFrom.DataSource =  null;
        this.cbxTo.DataSource =  null;
        this.Progress = 0;
        this.Text = this.m_Title + " | Serial No " + this.m_KLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(openFileDialog.FileName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Failed to load." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.Save();
    }

    private void Save()
    {
      if (this.m_KLDS.Registers.Rows.Count < 1)
      {
        int num1 = (int) MessageBox.Show("The KMP Log is empty.", "No data to save!");
      }
      else
      {
        try
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
            Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
          saveFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
          saveFileDialog.Filter = "MC601 KMP Log (*." + this.m_fileExtension + ")|*." + this.m_fileExtension + "|All files (*.*)|*.*";
          saveFileDialog.FilterIndex = 1;
          saveFileDialog.RestoreDirectory = true;
          if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
          if (saveFileDialog.FileName.IndexOf("." + this.m_fileExtension) < 0)
            saveFileDialog.FileName = saveFileDialog.FileName + "." + this.m_fileExtension;
          this.m_KLDS.WriteXml(saveFileDialog.FileName);
          this.btnSave.Enabled = false;
          this.Text = this.m_Title + " | Serial No " + this.m_KLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(saveFileDialog.FileName);
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Failed to save the the KMP Log:" + ex.Message);
        }
      }
    }

    private void Reg1XReg2(UCRegisterCheckBox Reg1, UCRegisterCheckBox Reg2, string CalcRule)
    {
      string registerName1 = Reg1.RegisterName;
      string registerName2 = Reg2.RegisterName;
      DataTable dataTable = new DataSet("GraphDataSet").Tables.Add("AllValues");
      dataTable.Columns.Add("Date", typeof (DateTime));
      byte nUnit1 = (byte) this.m_RegisterUnitRow[registerName1];
      byte nUnit2 = (byte) this.m_RegisterUnitRow[registerName2];
      string columnName = Reg1.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit1) + "] " + CalcRule + " " + Reg2.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit2) + "]";
      dataTable.Columns.Add(columnName, typeof (double));
      foreach (KMPLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.m_KLDS.Registers.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] =  registersRow.Date;
        Decimal register1 = Convert.ToDecimal(registersRow[registerName1]);
        Decimal register2 = Convert.ToDecimal(registersRow[registerName2]);
        row[columnName] =  FrmKMPLogger.DoCalculation(register1, register2, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_KLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.ShowTime = true;
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_KLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.ShowTime = true;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private void Reg1XReg2(UCRegisterCheckBox Reg1, Decimal calcValue, string CalcRule)
    {
      string registerName = Reg1.RegisterName;
      DataTable dataTable = new DataSet("GraphDataSet").Tables.Add("AllValues");
      dataTable.Columns.Add("Date", typeof (DateTime));
      byte nUnit = (byte) this.m_RegisterUnitRow[registerName];
      string columnName = Reg1.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit) + "] " + CalcRule + " " + calcValue.ToString();
      dataTable.Columns.Add(columnName, typeof (double));
      foreach (KMPLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.m_KLDS.Registers.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] =  registersRow.Date;
        Decimal register1 = Convert.ToDecimal(registersRow[registerName]);
        row[columnName] =  FrmKMPLogger.DoCalculation(register1, calcValue, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_KLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.ShowTime = true;
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_KLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.ShowTime = true;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private void btnAddToRegs_Click(object sender, EventArgs e)
    {
      if (this.rdbCalcA.Checked)
      {
        if (this.cbxRegister1.SelectedItem == null || this.cbxRegister2.SelectedItem == null || this.cbxCalcRule.SelectedItem == null)
          return;
        clsCalculatedRegister ur = new clsCalculatedRegister();
        CbxItem cbxItem1 = (CbxItem) this.cbxRegister1.SelectedItem;
        ur.RegisterName_1 = cbxItem1.Register.RegisterName;
        ur.RegisterCaption_1 = cbxItem1.Register.Caption;
        CbxItem cbxItem2 = (CbxItem) this.cbxRegister2.SelectedItem;
        ur.RegisterName_2 = cbxItem2.Register.RegisterName;
        ur.RegisterCaption_2 = cbxItem2.Register.Caption;
        ur.CalculationRule = this.cbxCalcRule.Text;
        foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
        {
          clsCalculatedRegister calculatedRegister = obj as clsCalculatedRegister;
          if (calculatedRegister != null && !calculatedRegister.UseRegister2 && (calculatedRegister.RegisterName_1 == ur.RegisterName_1 && calculatedRegister.RegisterName_2 == ur.RegisterName_2) && calculatedRegister.CalculationRule == ur.CalculationRule)
          {
            int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          }
        }
        this.m_userRegisters.ListOfCalculatedRegisters.Add( ur);
        this.m_userRegisters.Save(this.m_fileExtension + "UR");
        this.addUserRegisterToForm(ur);
      }
      else
      {
        if (this.cbxRegister1.SelectedItem == null || this.cbxCalcRule.SelectedItem == null)
          return;
        clsCalculatedRegister ur = new clsCalculatedRegister();
        CbxItem cbxItem1 = (CbxItem) this.cbxRegister1.SelectedItem;
        ur.RegisterName_1 = cbxItem1.Register.RegisterName;
        ur.RegisterCaption_1 = cbxItem1.Register.Caption;
        CbxItem cbxItem2 = (CbxItem) this.cbxRegister2.SelectedItem;
        ur.UseRegister2 = false;
        ur.CalculationValue = this.nudCalc.Value;
        ur.CalculationRule = this.cbxCalcRule.Text;
        foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
        {
          clsCalculatedRegister calculatedRegister = obj as clsCalculatedRegister;
          if (calculatedRegister != null && !calculatedRegister.UseRegister2 && (calculatedRegister.RegisterName_1 == ur.RegisterName_1 && calculatedRegister.CalculationValue == ur.CalculationValue) && calculatedRegister.CalculationRule == ur.CalculationRule)
          {
            int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          }
        }
        this.m_userRegisters.ListOfCalculatedRegisters.Add( ur);
        this.m_userRegisters.Save(this.m_fileExtension + "UR");
        this.addUserRegisterToForm(ur);
      }
    }

    private void addUserRegisterToForm(clsCalculatedRegister ur)
    {
      UCRegisterCheckBox registerCheckBox = new UCRegisterCheckBox();
      registerCheckBox.Caption = ur.ToString();
      registerCheckBox.Checked = false;
      registerCheckBox.Name = ur.ToString();
      registerCheckBox.RegisterId = -1;
      registerCheckBox.RegisterCaption1 = ur.RegisterCaption_1;
      registerCheckBox.RegisterCaption2 = ur.RegisterCaption_2;
      registerCheckBox.RegisterId = -1;
      registerCheckBox.IsRegister = false;
      registerCheckBox.RegisterName = ur.RegisterName_1;
      registerCheckBox.RegisterName2 = ur.RegisterName_2;
      registerCheckBox.UseRegister2 = ur.UseRegister2;
      registerCheckBox.CalculationValue = ur.CalculationValue;
      registerCheckBox.CalculationRule = ur.CalculationRule;
      registerCheckBox.Size = new Size(280, 21);
      registerCheckBox.UnitText = "";
      this.flpCalculatedRegisters.Controls.Add((Control) registerCheckBox);
    }

    private void btnRemoveCalculatedRegister_Click(object sender, EventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null && registerCheckBox.Checked)
        {
          foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
          {
            clsCalculatedRegister calculatedRegister = obj as clsCalculatedRegister;
            if (calculatedRegister != null)
            {
              if (registerCheckBox.UseRegister2)
              {
                if (calculatedRegister.UseRegister2 && calculatedRegister.RegisterName_1 == registerCheckBox.RegisterName && (calculatedRegister.RegisterName_2 == registerCheckBox.RegisterName2 && calculatedRegister.CalculationRule == registerCheckBox.CalculationRule))
                {
                  this.m_userRegisters.ListOfCalculatedRegisters.Remove(obj);
                  registerCheckBox.Dispose();
                  break;
                }
              }
              else if (!calculatedRegister.UseRegister2 && calculatedRegister.RegisterName_1 == registerCheckBox.RegisterName && (calculatedRegister.CalculationValue == registerCheckBox.CalculationValue && calculatedRegister.CalculationRule == registerCheckBox.CalculationRule))
              {
                this.m_userRegisters.ListOfCalculatedRegisters.Remove(obj);
                registerCheckBox.Dispose();
                break;
              }
            }
          }
        }
      }
      this.m_userRegisters.Save(this.m_fileExtension + "UR");
    }

    private void btnRemoveAllCalculated_Click(object sender, EventArgs e)
    {
      this.m_userRegisters.ListOfCalculatedRegisters.Clear();
      this.m_userRegisters.Save(this.m_fileExtension + "UR");
      this.flpCalculatedRegisters.Controls.Clear();
    }

    private bool IsAnyRCBChecked()
    {
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null && registerCheckBox.Checked)
          return true;
      }
      return false;
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      Graphics graphics = pevent.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.White, this.BackColor, LinearGradientMode.Vertical);
      graphics.FillRectangle((Brush) linearGradientBrush, rect);
      linearGradientBrush.Dispose();
    }

    private void FrmKMPLogger_Load(object sender, EventArgs e)
    {
      this.LoadTimestamps();
    }

    private void LoadTimestamps()
    {
      this.btnRead.Enabled = false;
      this.Cursor = Cursors.WaitCursor;
      if (this.m_KMPLoggerType == eKMPLoggerType._670B)
      {
        if (this.GetCurrentTopModuleType() == this.m_KMPLoggerType)
        {
          new Thread(new ThreadStart(this.MakeListOfDays)).Start();
        }
        else
        {
          int num = (int) MessageBox.Show("The chosen top module is not installed.", "Info");
          this.thSplash.Abort();
          this.Cursor = Cursors.Default;
        }
      }
      else if (this.m_KMPLoggerType == eKMPLoggerType._670022)
      {
        if (this.GetCurrentBottomModuleType() == this.m_KMPLoggerType)
        {
          new Thread(new ThreadStart(this.MakeListOfDays)).Start();
        }
        else
        {
          int num = (int) MessageBox.Show("The chosen top module is not installed.", "Info");
          this.thSplash.Abort();
          this.Cursor = Cursors.Default;
        }
      }
      else
      {
        this.thSplash.Abort();
        this.Cursor = Cursors.Default;
      }
    }

    private void btnShowLogDetails_Click(object sender, EventArgs e)
    {
      FrmKMPLoggerShowLog kmpLoggerShowLog = new FrmKMPLoggerShowLog();
      kmpLoggerShowLog.MdiParent = this.MdiParent;
      kmpLoggerShowLog.KMPDS = this.m_KLDS;
      kmpLoggerShowLog.DisplayData();
      kmpLoggerShowLog.Show();
    }

    private void rdbCalcB_CheckedChanged(object sender, EventArgs e)
    {
      this.cbxRegister2.Enabled = this.rdbCalcA.Checked;
      this.nudCalc.Enabled = this.rdbCalcB.Checked;
    }

    private void btnCalculate_Click(object sender, EventArgs e)
    {
      CbxItem cbxItem1 = (CbxItem) this.cbxRegister1.SelectedItem;
      CbxItem cbxItem2 = (CbxItem) this.cbxRegister2.SelectedItem;
      if (this.rdbCalcA.Checked)
      {
        if (cbxItem1 == null || cbxItem2 == null || this.cbxCalcRule.SelectedItem == null)
          return;
        this.Reg1XReg2(cbxItem1.Register, cbxItem2.Register, this.cbxCalcRule.SelectedItem.ToString());
      }
      else
      {
        if (cbxItem1 == null || this.cbxCalcRule.SelectedItem == null)
          return;
        this.Reg1XReg2(cbxItem1.Register, this.nudCalc.Value, this.cbxCalcRule.SelectedItem.ToString());
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
      Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmKMPLogger));
      this.grpUsed = new GroupBox();
      this.grpCalculatedRegisters = new GroupBox();
      this.btnRemoveAllCalculated = new Button();
      this.btnRemoveCalculatedRegister = new Button();
      this.flpCalculatedRegisters = new FlowLayoutPanel();
      this.grpCalculate = new GroupBox();
      this.btnAddToRegs = new Button();
      this.btnCalculate = new Button();
      this.cbxRegister2 = new ComboBox();
      this.cbxCalcRule = new ComboBox();
      this.cbxRegister1 = new ComboBox();
      this.grpGraphs = new GroupBox();
      this.btnSelectedRegisters = new Button();
      this.grpReadLog = new GroupBox();
      this.cbxTo = new UltraCombo();
      this.cbxFrom = new UltraCombo();
      this.lblTo = new Label();
      this.lblFrom = new Label();
      this.btnClear = new Button();
      this.lblRecords = new Label();
      this.label3 = new Label();
      this.btnSave = new Button();
      this.btnLoad = new Button();
      this.btnRead = new Button();
      this.grpRegisters = new GroupBox();
      this.flpRegisters = new FlowLayoutPanel();
      this.btnSelectAll = new Button();
      this.btnSelectNone = new Button();
      this.groupBox1 = new GroupBox();
      this.btnShowLogDetails = new Button();
      this.rdbCalcB = new RadioButton();
      this.rdbCalcA = new RadioButton();
      this.nudCalc = new NumericUpDown();
      this.rcbCalc0 = new UCRegisterCheckBox();
      this.rcbE1 = new UCRegisterCheckBox();
      this.rcbE7 = new UCRegisterCheckBox();
      this.rcbE3 = new UCRegisterCheckBox();
      this.rcbE4 = new UCRegisterCheckBox();
      this.rcbE5 = new UCRegisterCheckBox();
      this.rcbE6 = new UCRegisterCheckBox();
      this.rcbE2 = new UCRegisterCheckBox();
      this.rcbE8 = new UCRegisterCheckBox();
      this.rcbE9 = new UCRegisterCheckBox();
      this.rcbV1 = new UCRegisterCheckBox();
      this.rcbV2 = new UCRegisterCheckBox();
      this.rcbInA = new UCRegisterCheckBox();
      this.rcbInB = new UCRegisterCheckBox();
      this.rcbM1 = new UCRegisterCheckBox();
      this.rcbM2 = new UCRegisterCheckBox();
      this.rcbT1 = new UCRegisterCheckBox();
      this.rcbT2 = new UCRegisterCheckBox();
      this.rcbT3 = new UCRegisterCheckBox();
      this.rcbT4 = new UCRegisterCheckBox();
      this.rcbT1_T2 = new UCRegisterCheckBox();
      this.rcbFLOW1 = new UCRegisterCheckBox();
      this.rcbFLOW2 = new UCRegisterCheckBox();
      this.rcbEFFECT1 = new UCRegisterCheckBox();
      this.rcbP1 = new UCRegisterCheckBox();
      this.rcbP2 = new UCRegisterCheckBox();
      this.rcbInfo = new UCRegisterCheckBox();
      this.rcbLogQOS = new UCRegisterCheckBox();
      this.rcbHR = new UCRegisterCheckBox();
      this.grpUsed.SuspendLayout();
      this.grpCalculatedRegisters.SuspendLayout();
      this.grpCalculate.SuspendLayout();
      this.grpGraphs.SuspendLayout();
      this.grpReadLog.SuspendLayout();
      //this.cbxTo.BeginInit();
      //this.cbxFrom.BeginInit();
      this.grpRegisters.SuspendLayout();
      this.flpRegisters.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.nudCalc.BeginInit();
      this.SuspendLayout();
      this.grpUsed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.grpUsed.BackColor = Color.Transparent;
      this.grpUsed.Controls.Add((Control) this.rcbCalc0);
      this.grpUsed.Location = new Point(500, 10);
      this.grpUsed.Name = "grpUsed";
      this.grpUsed.Size = new Size(222, 42);
      this.grpUsed.TabIndex = 32;
      this.grpUsed.TabStop = false;
      this.grpUsed.Text = "Change per interval";
      this.grpCalculatedRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.grpCalculatedRegisters.BackColor = Color.Transparent;
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveAllCalculated);
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveCalculatedRegister);
      this.grpCalculatedRegisters.Controls.Add((Control) this.flpCalculatedRegisters);
      this.grpCalculatedRegisters.Location = new Point(500, 61);
      this.grpCalculatedRegisters.Name = "grpCalculatedRegisters";
      this.grpCalculatedRegisters.Size = new Size(222, 428);
      this.grpCalculatedRegisters.TabIndex = 30;
      this.grpCalculatedRegisters.TabStop = false;
      this.grpCalculatedRegisters.Text = "Calculated Registers";
      this.btnRemoveAllCalculated.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnRemoveAllCalculated.Location = new Point(126, 398);
      this.btnRemoveAllCalculated.Name = "btnRemoveAllCalculated";
      this.btnRemoveAllCalculated.Size = new Size(90, 23);
      this.btnRemoveAllCalculated.TabIndex = 30;
      this.btnRemoveAllCalculated.Text = "Remove All";
      this.btnRemoveAllCalculated.UseVisualStyleBackColor = true;
      this.btnRemoveAllCalculated.Click += new EventHandler(this.btnRemoveAllCalculated_Click);
      this.btnRemoveCalculatedRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRemoveCalculatedRegister.Location = new Point(6, 398);
      this.btnRemoveCalculatedRegister.Name = "btnRemoveCalculatedRegister";
      this.btnRemoveCalculatedRegister.Size = new Size(108, 23);
      this.btnRemoveCalculatedRegister.TabIndex = 29;
      this.btnRemoveCalculatedRegister.Text = "Remove Selected";
      this.btnRemoveCalculatedRegister.UseVisualStyleBackColor = true;
      this.btnRemoveCalculatedRegister.Click += new EventHandler(this.btnRemoveCalculatedRegister_Click);
      this.flpCalculatedRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.flpCalculatedRegisters.AutoScroll = true;
      this.flpCalculatedRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpCalculatedRegisters.Location = new Point(3, 19);
      this.flpCalculatedRegisters.Name = "flpCalculatedRegisters";
      this.flpCalculatedRegisters.Size = new Size(216, 372);
      this.flpCalculatedRegisters.TabIndex = 18;
      this.grpCalculate.BackColor = Color.Transparent;
      this.grpCalculate.Controls.Add((Control) this.rdbCalcB);
      this.grpCalculate.Controls.Add((Control) this.rdbCalcA);
      this.grpCalculate.Controls.Add((Control) this.nudCalc);
      this.grpCalculate.Controls.Add((Control) this.btnAddToRegs);
      this.grpCalculate.Controls.Add((Control) this.btnCalculate);
      this.grpCalculate.Controls.Add((Control) this.cbxRegister2);
      this.grpCalculate.Controls.Add((Control) this.cbxCalcRule);
      this.grpCalculate.Controls.Add((Control) this.cbxRegister1);
      this.grpCalculate.Location = new Point(11, 172);
      this.grpCalculate.Name = "grpCalculate";
      this.grpCalculate.Size = new Size(187, 158);
      this.grpCalculate.TabIndex = 29;
      this.grpCalculate.TabStop = false;
      this.grpCalculate.Text = "Calculate";
      this.btnAddToRegs.Enabled = false;
      this.btnAddToRegs.Location = new Point(106, (int) sbyte.MaxValue);
      this.btnAddToRegs.Name = "btnAddToRegs";
      this.btnAddToRegs.Size = new Size(75, 23);
      this.btnAddToRegs.TabIndex = 17;
      this.btnAddToRegs.Text = "Add to Regs. ";
      this.btnAddToRegs.UseVisualStyleBackColor = true;
      this.btnAddToRegs.Click += new EventHandler(this.btnAddToRegs_Click);
      this.btnCalculate.Enabled = false;
      this.btnCalculate.Location = new Point(7, (int) sbyte.MaxValue);
      this.btnCalculate.Name = "btnCalculate";
      this.btnCalculate.Size = new Size(75, 23);
      this.btnCalculate.TabIndex = 16;
      this.btnCalculate.Text = "Show Graph";
      this.btnCalculate.UseVisualStyleBackColor = true;
      this.btnCalculate.Click += new EventHandler(this.btnCalculate_Click);
      this.cbxRegister2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbxRegister2.FormattingEnabled = true;
      this.cbxRegister2.Location = new Point(45, 74);
      this.cbxRegister2.Name = "cbxRegister2";
      this.cbxRegister2.Size = new Size(136, 21);
      this.cbxRegister2.TabIndex = 2;
      this.cbxCalcRule.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbxCalcRule.FormattingEnabled = true;
      this.cbxCalcRule.Items.AddRange(new object[4]
      {
         "+",
         "-",
         "*",
         "/"
      });
      this.cbxCalcRule.Location = new Point(7, 47);
      this.cbxCalcRule.Name = "cbxCalcRule";
      this.cbxCalcRule.Size = new Size(39, 21);
      this.cbxCalcRule.TabIndex = 1;
      this.cbxRegister1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbxRegister1.FormattingEnabled = true;
      this.cbxRegister1.Location = new Point(7, 19);
      this.cbxRegister1.Name = "cbxRegister1";
      this.cbxRegister1.Size = new Size(174, 21);
      this.cbxRegister1.TabIndex = 0;
      this.grpGraphs.BackColor = Color.Transparent;
      this.grpGraphs.Controls.Add((Control) this.btnSelectedRegisters);
      this.grpGraphs.Location = new Point(12, 336);
      this.grpGraphs.Name = "grpGraphs";
      this.grpGraphs.Size = new Size(187, 50);
      this.grpGraphs.TabIndex = 28;
      this.grpGraphs.TabStop = false;
      this.grpGraphs.Text = "Graphs";
      this.btnSelectedRegisters.Enabled = false;
      this.btnSelectedRegisters.Location = new Point(6, 20);
      this.btnSelectedRegisters.Name = "btnSelectedRegisters";
      this.btnSelectedRegisters.Size = new Size(175, 23);
      this.btnSelectedRegisters.TabIndex = 11;
      this.btnSelectedRegisters.Text = "Selected Registers";
      this.btnSelectedRegisters.UseVisualStyleBackColor = true;
      this.btnSelectedRegisters.Click += new EventHandler(this.btnSelectedRegisters_Click);
      this.grpReadLog.BackColor = Color.Transparent;
      this.grpReadLog.Controls.Add((Control) this.cbxTo);
      this.grpReadLog.Controls.Add((Control) this.cbxFrom);
      this.grpReadLog.Controls.Add((Control) this.lblTo);
      this.grpReadLog.Controls.Add((Control) this.lblFrom);
      this.grpReadLog.Controls.Add((Control) this.btnClear);
      this.grpReadLog.Controls.Add((Control) this.lblRecords);
      this.grpReadLog.Controls.Add((Control) this.label3);
      this.grpReadLog.Controls.Add((Control) this.btnSave);
      this.grpReadLog.Controls.Add((Control) this.btnLoad);
      this.grpReadLog.Controls.Add((Control) this.btnRead);
      this.grpReadLog.Location = new Point(12, 10);
      this.grpReadLog.Name = "grpReadLog";
      this.grpReadLog.Size = new Size(186, 156);
      this.grpReadLog.TabIndex = 27;
      this.grpReadLog.TabStop = false;
      this.grpReadLog.Text = "Interval Log";
      this.cbxTo.CharacterCasing = CharacterCasing.Normal;
      appearance1.BackColor = SystemColors.Window;
      appearance1.BorderColor = SystemColors.InactiveCaption;
      this.cbxTo.DisplayLayout.Appearance = (AppearanceBase) appearance1;
      this.cbxTo.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
      this.cbxTo.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
      appearance2.BackColor = SystemColors.ActiveBorder;
      appearance2.BackColor2 = SystemColors.ControlDark;
      appearance2.BackGradientStyle = GradientStyle.Vertical;
      appearance2.BorderColor = SystemColors.Window;
      this.cbxTo.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance2;
      appearance3.ForeColor = SystemColors.GrayText;
      this.cbxTo.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance3;
      this.cbxTo.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
      appearance4.BackColor = SystemColors.ControlLightLight;
      appearance4.BackColor2 = SystemColors.Control;
      appearance4.BackGradientStyle = GradientStyle.Horizontal;
      appearance4.ForeColor = SystemColors.GrayText;
      this.cbxTo.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance4;
      this.cbxTo.DisplayLayout.MaxColScrollRegions = 1;
      this.cbxTo.DisplayLayout.MaxRowScrollRegions = 1;
      appearance5.BackColor = SystemColors.Window;
      appearance5.ForeColor = SystemColors.ControlText;
      this.cbxTo.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance5;
      appearance6.BackColor = SystemColors.Highlight;
      appearance6.ForeColor = SystemColors.HighlightText;
      this.cbxTo.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance6;
      this.cbxTo.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
      this.cbxTo.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
      appearance7.BackColor = SystemColors.Window;
      this.cbxTo.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance7;
      appearance8.BorderColor = Color.Silver;
      appearance8.TextTrimming = TextTrimming.EllipsisCharacter;
      this.cbxTo.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance8;
      this.cbxTo.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
      this.cbxTo.DisplayLayout.Override.CellPadding = 0;
      this.cbxTo.DisplayLayout.Override.DefaultColWidth = 110;
      appearance9.BackColor = SystemColors.Control;
      appearance9.BackColor2 = SystemColors.ControlDark;
      appearance9.BackGradientAlignment = GradientAlignment.Element;
      appearance9.BackGradientStyle = GradientStyle.Horizontal;
      appearance9.BorderColor = SystemColors.Window;
      this.cbxTo.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance9;
      appearance10.TextHAlignAsString = "Left";
      this.cbxTo.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance10;
      this.cbxTo.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
      this.cbxTo.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
      appearance11.BackColor = SystemColors.Window;
      appearance11.BorderColor = Color.Silver;
      this.cbxTo.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance11;
      this.cbxTo.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
      appearance12.BackColor = SystemColors.ControlLight;
      this.cbxTo.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance12;
      this.cbxTo.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
      this.cbxTo.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
      this.cbxTo.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
      this.cbxTo.DisplayStyle = EmbeddableElementDisplayStyle.Default;
      this.cbxTo.DropDownStyle = UltraComboStyle.DropDownList;
      this.cbxTo.Location = new Point(44, 46);
      this.cbxTo.Name = "cbxTo";
      this.cbxTo.Size = new Size(136, 22);
      this.cbxTo.TabIndex = 21;
      this.cbxFrom.CharacterCasing = CharacterCasing.Normal;
      appearance13.BackColor = SystemColors.Window;
      appearance13.BorderColor = SystemColors.InactiveCaption;
      this.cbxFrom.DisplayLayout.Appearance = (AppearanceBase) appearance13;
      this.cbxFrom.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
      this.cbxFrom.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
      appearance14.BackColor = SystemColors.ActiveBorder;
      appearance14.BackColor2 = SystemColors.ControlDark;
      appearance14.BackGradientStyle = GradientStyle.Vertical;
      appearance14.BorderColor = SystemColors.Window;
      this.cbxFrom.DisplayLayout.GroupByBox.Appearance = (AppearanceBase) appearance14;
      appearance15.ForeColor = SystemColors.GrayText;
      this.cbxFrom.DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase) appearance15;
      this.cbxFrom.DisplayLayout.GroupByBox.BorderStyle = UIElementBorderStyle.Solid;
      appearance16.BackColor = SystemColors.ControlLightLight;
      appearance16.BackColor2 = SystemColors.Control;
      appearance16.BackGradientStyle = GradientStyle.Horizontal;
      appearance16.ForeColor = SystemColors.GrayText;
      this.cbxFrom.DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase) appearance16;
      this.cbxFrom.DisplayLayout.MaxColScrollRegions = 1;
      this.cbxFrom.DisplayLayout.MaxRowScrollRegions = 1;
      appearance17.BackColor = SystemColors.Window;
      appearance17.ForeColor = SystemColors.ControlText;
      this.cbxFrom.DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase) appearance17;
      appearance18.BackColor = SystemColors.Highlight;
      appearance18.ForeColor = SystemColors.HighlightText;
      this.cbxFrom.DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase) appearance18;
      this.cbxFrom.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Dotted;
      this.cbxFrom.DisplayLayout.Override.BorderStyleRow = UIElementBorderStyle.Dotted;
      appearance19.BackColor = SystemColors.Window;
      this.cbxFrom.DisplayLayout.Override.CardAreaAppearance = (AppearanceBase) appearance19;
      appearance20.BorderColor = Color.Silver;
      appearance20.TextTrimming = TextTrimming.EllipsisCharacter;
      this.cbxFrom.DisplayLayout.Override.CellAppearance = (AppearanceBase) appearance20;
      this.cbxFrom.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
      this.cbxFrom.DisplayLayout.Override.CellPadding = 0;
      this.cbxFrom.DisplayLayout.Override.DefaultColWidth = 110;
      appearance21.BackColor = SystemColors.Control;
      appearance21.BackColor2 = SystemColors.ControlDark;
      appearance21.BackGradientAlignment = GradientAlignment.Element;
      appearance21.BackGradientStyle = GradientStyle.Horizontal;
      appearance21.BorderColor = SystemColors.Window;
      this.cbxFrom.DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase) appearance21;
      appearance22.TextHAlignAsString = "Left";
      this.cbxFrom.DisplayLayout.Override.HeaderAppearance = (AppearanceBase) appearance22;
      this.cbxFrom.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
      this.cbxFrom.DisplayLayout.Override.HeaderStyle = HeaderStyle.WindowsXPCommand;
      appearance23.BackColor = SystemColors.Window;
      appearance23.BorderColor = Color.Silver;
      this.cbxFrom.DisplayLayout.Override.RowAppearance = (AppearanceBase) appearance23;
      this.cbxFrom.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
      appearance24.BackColor = SystemColors.ControlLight;
      this.cbxFrom.DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase) appearance24;
      this.cbxFrom.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
      this.cbxFrom.DisplayLayout.ScrollStyle = ScrollStyle.Immediate;
      this.cbxFrom.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
      this.cbxFrom.DisplayStyle = EmbeddableElementDisplayStyle.Default;
      this.cbxFrom.DropDownStyle = UltraComboStyle.DropDownList;
      this.cbxFrom.Location = new Point(44, 18);
      this.cbxFrom.Name = "cbxFrom";
      this.cbxFrom.Size = new Size(136, 22);
      this.cbxFrom.TabIndex = 20;
      this.lblTo.AutoSize = true;
      this.lblTo.Location = new Point(8, 51);
      this.lblTo.Name = "lblTo";
      this.lblTo.Size = new Size(20, 13);
      this.lblTo.TabIndex = 16;
      this.lblTo.Text = "To";
      this.lblFrom.AutoSize = true;
      this.lblFrom.Location = new Point(8, 24);
      this.lblFrom.Name = "lblFrom";
      this.lblFrom.Size = new Size(30, 13);
      this.lblFrom.TabIndex = 15;
      this.lblFrom.Text = "From";
      this.btnClear.Location = new Point(105, 74);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new Size(75, 23);
      this.btnClear.TabIndex = 11;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new EventHandler(this.btnClear_Click);
      this.lblRecords.AutoSize = true;
      this.lblRecords.Location = new Point(54, 98);
      this.lblRecords.Name = "lblRecords";
      this.lblRecords.Size = new Size(13, 13);
      this.lblRecords.TabIndex = 10;
      this.lblRecords.Text = "0";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 98);
      this.label3.Name = "label3";
      this.label3.Size = new Size(50, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Records:";
      this.btnSave.Enabled = false;
      this.btnSave.Location = new Point(105, 122);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 7;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnLoad.Location = new Point(6, 122);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new Size(75, 23);
      this.btnLoad.TabIndex = 6;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
      this.btnRead.Location = new Point(6, 74);
      this.btnRead.Name = "btnRead";
      this.btnRead.Size = new Size(75, 23);
      this.btnRead.TabIndex = 3;
      this.btnRead.Text = "Read";
      this.btnRead.UseVisualStyleBackColor = true;
      this.btnRead.Click += new EventHandler(this.btnStart_Click);
      this.grpRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpRegisters.BackColor = Color.Transparent;
      this.grpRegisters.Controls.Add((Control) this.flpRegisters);
      this.grpRegisters.Location = new Point(204, 10);
      this.grpRegisters.Name = "grpRegisters";
      this.grpRegisters.Size = new Size(290, 479);
      this.grpRegisters.TabIndex = 26;
      this.grpRegisters.TabStop = false;
      this.grpRegisters.Text = "Registers";
      this.flpRegisters.AutoScroll = true;
      this.flpRegisters.Controls.Add((Control) this.rcbE1);
      this.flpRegisters.Controls.Add((Control) this.rcbE7);
      this.flpRegisters.Controls.Add((Control) this.rcbE3);
      this.flpRegisters.Controls.Add((Control) this.rcbE4);
      this.flpRegisters.Controls.Add((Control) this.rcbE5);
      this.flpRegisters.Controls.Add((Control) this.rcbE6);
      this.flpRegisters.Controls.Add((Control) this.rcbE2);
      this.flpRegisters.Controls.Add((Control) this.rcbE8);
      this.flpRegisters.Controls.Add((Control) this.rcbE9);
      this.flpRegisters.Controls.Add((Control) this.rcbV1);
      this.flpRegisters.Controls.Add((Control) this.rcbV2);
      this.flpRegisters.Controls.Add((Control) this.rcbInA);
      this.flpRegisters.Controls.Add((Control) this.rcbInB);
      this.flpRegisters.Controls.Add((Control) this.rcbM1);
      this.flpRegisters.Controls.Add((Control) this.rcbM2);
      this.flpRegisters.Controls.Add((Control) this.rcbT1);
      this.flpRegisters.Controls.Add((Control) this.rcbT2);
      this.flpRegisters.Controls.Add((Control) this.rcbT3);
      this.flpRegisters.Controls.Add((Control) this.rcbT4);
      this.flpRegisters.Controls.Add((Control) this.rcbT1_T2);
      this.flpRegisters.Controls.Add((Control) this.rcbFLOW1);
      this.flpRegisters.Controls.Add((Control) this.rcbFLOW2);
      this.flpRegisters.Controls.Add((Control) this.rcbEFFECT1);
      this.flpRegisters.Controls.Add((Control) this.rcbP1);
      this.flpRegisters.Controls.Add((Control) this.rcbP2);
      this.flpRegisters.Controls.Add((Control) this.rcbInfo);
      this.flpRegisters.Controls.Add((Control) this.rcbLogQOS);
      this.flpRegisters.Controls.Add((Control) this.rcbHR);
      this.flpRegisters.Controls.Add((Control) this.btnSelectAll);
      this.flpRegisters.Controls.Add((Control) this.btnSelectNone);
      this.flpRegisters.Dock = DockStyle.Fill;
      this.flpRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpRegisters.Location = new Point(3, 16);
      this.flpRegisters.Name = "flpRegisters";
      this.flpRegisters.Size = new Size(284, 460);
      this.flpRegisters.TabIndex = 17;
      this.btnSelectAll.Location = new Point(157, 300);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new Size(75, 23);
      this.btnSelectAll.TabIndex = 28;
      this.btnSelectAll.Text = "Select All";
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
      this.btnSelectNone.Location = new Point(157, 329);
      this.btnSelectNone.Name = "btnSelectNone";
      this.btnSelectNone.Size = new Size(75, 23);
      this.btnSelectNone.TabIndex = 27;
      this.btnSelectNone.Text = "Select None";
      this.btnSelectNone.UseVisualStyleBackColor = true;
      this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
      this.groupBox1.BackColor = Color.Transparent;
      this.groupBox1.Controls.Add((Control) this.btnShowLogDetails);
      this.groupBox1.Location = new Point(11, 392);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(187, 50);
      this.groupBox1.TabIndex = 29;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Log details";
      this.btnShowLogDetails.Enabled = false;
      this.btnShowLogDetails.Location = new Point(6, 20);
      this.btnShowLogDetails.Name = "btnShowLogDetails";
      this.btnShowLogDetails.Size = new Size(175, 23);
      this.btnShowLogDetails.TabIndex = 11;
      this.btnShowLogDetails.Text = "Show Log Details";
      this.btnShowLogDetails.UseVisualStyleBackColor = true;
      this.btnShowLogDetails.Click += new EventHandler(this.btnShowLogDetails_Click);
      this.rdbCalcB.AutoSize = true;
      this.rdbCalcB.Location = new Point(7, 103);
      this.rdbCalcB.Name = "rdbCalcB";
      this.rdbCalcB.Size = new Size(14, 13);
      this.rdbCalcB.TabIndex = 26;
      this.rdbCalcB.UseVisualStyleBackColor = true;
      this.rdbCalcB.CheckedChanged += new EventHandler(this.rdbCalcB_CheckedChanged);
      this.rdbCalcA.AutoSize = true;
      this.rdbCalcA.Checked = true;
      this.rdbCalcA.Location = new Point(7, 76);
      this.rdbCalcA.Name = "rdbCalcA";
      this.rdbCalcA.Size = new Size(14, 13);
      this.rdbCalcA.TabIndex = 25;
      this.rdbCalcA.TabStop = true;
      this.rdbCalcA.UseVisualStyleBackColor = true;
      this.rdbCalcA.CheckedChanged += new EventHandler(this.rdbCalcB_CheckedChanged);
      this.nudCalc.DecimalPlaces = 3;
      this.nudCalc.Enabled = false;
      this.nudCalc.Location = new Point(45, 101);
      NumericUpDown numericUpDown = this.nudCalc;
      int[] bits = new int[4];
      bits[0] = 100000;
      Decimal num = new Decimal(bits);
      numericUpDown.Maximum = num;
      this.nudCalc.Name = "nudCalc";
      this.nudCalc.Size = new Size(136, 20);
      this.nudCalc.TabIndex = 24;
      this.nudCalc.TextAlign = HorizontalAlignment.Right;
      this.rcbCalc0.CalculationRule = "";
      this.rcbCalc0.CalculationValue = new Decimal(new int[4]);
      this.rcbCalc0.Caption = "Used Heat energy #1 ~ E1";
      this.rcbCalc0.Checked = false;
      this.rcbCalc0.IsRegister = false;
      this.rcbCalc0.Location = new Point(6, 19);
      this.rcbCalc0.Name = "rcbCalc0";
      this.rcbCalc0.RegisterCaption1 = "E1";
      this.rcbCalc0.RegisterCaption2 = "";
      this.rcbCalc0.RegisterId = 60;
      this.rcbCalc0.RegisterName = "E1";
      this.rcbCalc0.RegisterName2 = "";
      this.rcbCalc0.RegisterType = ERegisterType.IsDouble;
      this.rcbCalc0.Size = new Size(167, 21);
      this.rcbCalc0.TabIndex = 32;
      this.rcbCalc0.UnitText = "";
      this.rcbCalc0.UseRegister2 = true;
      this.rcbE1.CalculationRule = "";
      this.rcbE1.CalculationValue = new Decimal(new int[4]);
      this.rcbE1.Caption = "Heat energy #1 ~ E1";
      this.rcbE1.Checked = false;
      this.rcbE1.IsRegister = true;
      this.rcbE1.Location = new Point(3, 3);
      this.rcbE1.Name = "rcbE1";
      this.rcbE1.RegisterCaption1 = "";
      this.rcbE1.RegisterCaption2 = "";
      this.rcbE1.RegisterId = 60;
      this.rcbE1.RegisterName = "E1";
      this.rcbE1.RegisterName2 = "";
      this.rcbE1.RegisterType = ERegisterType.IsDouble;
      this.rcbE1.Size = new Size(137, 21);
      this.rcbE1.TabIndex = 0;
      this.rcbE1.UnitText = "";
      this.rcbE1.UseRegister2 = true;
      this.rcbE7.CalculationRule = "";
      this.rcbE7.CalculationValue = new Decimal(new int[4]);
      this.rcbE7.Caption = "Heat energy #2 ~ E7";
      this.rcbE7.Checked = false;
      this.rcbE7.IsRegister = true;
      this.rcbE7.Location = new Point(3, 30);
      this.rcbE7.Name = "rcbE7";
      this.rcbE7.RegisterCaption1 = "";
      this.rcbE7.RegisterCaption2 = "";
      this.rcbE7.RegisterId = 96;
      this.rcbE7.RegisterName = "E7";
      this.rcbE7.RegisterName2 = "";
      this.rcbE7.RegisterType = ERegisterType.IsDouble;
      this.rcbE7.Size = new Size(137, 21);
      this.rcbE7.TabIndex = 6;
      this.rcbE7.UnitText = "";
      this.rcbE7.UseRegister2 = true;
      this.rcbE3.CalculationRule = "";
      this.rcbE3.CalculationValue = new Decimal(new int[4]);
      this.rcbE3.Caption = "Cooling energy ~ E3";
      this.rcbE3.Checked = false;
      this.rcbE3.IsRegister = true;
      this.rcbE3.Location = new Point(3, 57);
      this.rcbE3.Name = "rcbE3";
      this.rcbE3.RegisterCaption1 = "";
      this.rcbE3.RegisterCaption2 = "";
      this.rcbE3.RegisterId = 63;
      this.rcbE3.RegisterName = "E3";
      this.rcbE3.RegisterName2 = "";
      this.rcbE3.RegisterType = ERegisterType.IsDouble;
      this.rcbE3.Size = new Size(136, 21);
      this.rcbE3.TabIndex = 2;
      this.rcbE3.UnitText = "";
      this.rcbE3.UseRegister2 = true;
      this.rcbE4.CalculationRule = "";
      this.rcbE4.CalculationValue = new Decimal(new int[4]);
      this.rcbE4.Caption = "Flow energy ~ E4";
      this.rcbE4.Checked = false;
      this.rcbE4.IsRegister = true;
      this.rcbE4.Location = new Point(3, 84);
      this.rcbE4.Name = "rcbE4";
      this.rcbE4.RegisterCaption1 = "";
      this.rcbE4.RegisterCaption2 = "";
      this.rcbE4.RegisterId = 61;
      this.rcbE4.RegisterName = "E4";
      this.rcbE4.RegisterName2 = "";
      this.rcbE4.RegisterType = ERegisterType.IsDouble;
      this.rcbE4.Size = new Size(122, 21);
      this.rcbE4.TabIndex = 3;
      this.rcbE4.UnitText = "";
      this.rcbE4.UseRegister2 = true;
      this.rcbE5.CalculationRule = "";
      this.rcbE5.CalculationValue = new Decimal(new int[4]);
      this.rcbE5.Caption = "Return energy ~ E5";
      this.rcbE5.Checked = false;
      this.rcbE5.IsRegister = true;
      this.rcbE5.Location = new Point(3, 111);
      this.rcbE5.Name = "rcbE5";
      this.rcbE5.RegisterCaption1 = "";
      this.rcbE5.RegisterCaption2 = "";
      this.rcbE5.RegisterId = 62;
      this.rcbE5.RegisterName = "E5";
      this.rcbE5.RegisterName2 = "";
      this.rcbE5.RegisterType = ERegisterType.IsDouble;
      this.rcbE5.Size = new Size(132, 21);
      this.rcbE5.TabIndex = 4;
      this.rcbE5.UnitText = "";
      this.rcbE5.UseRegister2 = true;
      this.rcbE6.CalculationRule = "";
      this.rcbE6.CalculationValue = new Decimal(new int[4]);
      this.rcbE6.Caption = "Tap water energy ~ E6";
      this.rcbE6.Checked = false;
      this.rcbE6.IsRegister = true;
      this.rcbE6.Location = new Point(3, 138);
      this.rcbE6.Name = "rcbE6";
      this.rcbE6.RegisterCaption1 = "";
      this.rcbE6.RegisterCaption2 = "";
      this.rcbE6.RegisterId = 95;
      this.rcbE6.RegisterName = "E6";
      this.rcbE6.RegisterName2 = "";
      this.rcbE6.RegisterType = ERegisterType.IsDouble;
      this.rcbE6.Size = new Size(148, 21);
      this.rcbE6.TabIndex = 5;
      this.rcbE6.UnitText = "";
      this.rcbE6.UseRegister2 = true;
      this.rcbE2.CalculationRule = "";
      this.rcbE2.CalculationValue = new Decimal(new int[4]);
      this.rcbE2.Caption = "Control energy ~ E2";
      this.rcbE2.Checked = false;
      this.rcbE2.IsRegister = true;
      this.rcbE2.Location = new Point(3, 165);
      this.rcbE2.Name = "rcbE2";
      this.rcbE2.RegisterCaption1 = "";
      this.rcbE2.RegisterCaption2 = "";
      this.rcbE2.RegisterId = 94;
      this.rcbE2.RegisterName = "E2";
      this.rcbE2.RegisterName2 = "";
      this.rcbE2.RegisterType = ERegisterType.IsDouble;
      this.rcbE2.Size = new Size(134, 21);
      this.rcbE2.TabIndex = 1;
      this.rcbE2.UnitText = "";
      this.rcbE2.UseRegister2 = true;
      this.rcbE8.CalculationRule = "";
      this.rcbE8.CalculationValue = new Decimal(new int[4]);
      this.rcbE8.Caption = "M3 * T1 ~ E8";
      this.rcbE8.Checked = false;
      this.rcbE8.IsRegister = true;
      this.rcbE8.Location = new Point(3, 192);
      this.rcbE8.Name = "rcbE8";
      this.rcbE8.RegisterCaption1 = "";
      this.rcbE8.RegisterCaption2 = "";
      this.rcbE8.RegisterId = 97;
      this.rcbE8.RegisterName = "E8";
      this.rcbE8.RegisterName2 = "";
      this.rcbE8.RegisterType = ERegisterType.IsDouble;
      this.rcbE8.Size = new Size(100, 21);
      this.rcbE8.TabIndex = 41;
      this.rcbE8.UnitText = "";
      this.rcbE8.UseRegister2 = true;
      this.rcbE9.CalculationRule = "";
      this.rcbE9.CalculationValue = new Decimal(new int[4]);
      this.rcbE9.Caption = "M3 * T1 ~ E9";
      this.rcbE9.Checked = false;
      this.rcbE9.IsRegister = true;
      this.rcbE9.Location = new Point(3, 219);
      this.rcbE9.Name = "rcbE9";
      this.rcbE9.RegisterCaption1 = "";
      this.rcbE9.RegisterCaption2 = "";
      this.rcbE9.RegisterId = 110;
      this.rcbE9.RegisterName = "E9";
      this.rcbE9.RegisterName2 = "";
      this.rcbE9.RegisterType = ERegisterType.IsDouble;
      this.rcbE9.Size = new Size(100, 21);
      this.rcbE9.TabIndex = 40;
      this.rcbE9.UnitText = "";
      this.rcbE9.UseRegister2 = true;
      this.rcbV1.CalculationRule = "";
      this.rcbV1.CalculationValue = new Decimal(new int[4]);
      this.rcbV1.Caption = "V1";
      this.rcbV1.Checked = false;
      this.rcbV1.IsRegister = true;
      this.rcbV1.Location = new Point(3, 246);
      this.rcbV1.Name = "rcbV1";
      this.rcbV1.RegisterCaption1 = "";
      this.rcbV1.RegisterCaption2 = "";
      this.rcbV1.RegisterId = 68;
      this.rcbV1.RegisterName = "V1";
      this.rcbV1.RegisterName2 = "";
      this.rcbV1.RegisterType = ERegisterType.IsDouble;
      this.rcbV1.Size = new Size(48, 21);
      this.rcbV1.TabIndex = 11;
      this.rcbV1.UnitText = "";
      this.rcbV1.UseRegister2 = true;
      this.rcbV2.CalculationRule = "";
      this.rcbV2.CalculationValue = new Decimal(new int[4]);
      this.rcbV2.Caption = "V2";
      this.rcbV2.Checked = false;
      this.rcbV2.IsRegister = true;
      this.rcbV2.Location = new Point(3, 273);
      this.rcbV2.Name = "rcbV2";
      this.rcbV2.RegisterCaption1 = "";
      this.rcbV2.RegisterCaption2 = "";
      this.rcbV2.RegisterId = 69;
      this.rcbV2.RegisterName = "V2";
      this.rcbV2.RegisterName2 = "";
      this.rcbV2.RegisterType = ERegisterType.IsDouble;
      this.rcbV2.Size = new Size(48, 21);
      this.rcbV2.TabIndex = 12;
      this.rcbV2.UnitText = "";
      this.rcbV2.UseRegister2 = true;
      this.rcbInA.CalculationRule = "";
      this.rcbInA.CalculationValue = new Decimal(new int[4]);
      this.rcbInA.Caption = "VA";
      this.rcbInA.Checked = false;
      this.rcbInA.IsRegister = true;
      this.rcbInA.Location = new Point(3, 300);
      this.rcbInA.Name = "rcbInA";
      this.rcbInA.RegisterCaption1 = "";
      this.rcbInA.RegisterCaption2 = "";
      this.rcbInA.RegisterId = 84;
      this.rcbInA.RegisterName = "VA";
      this.rcbInA.RegisterName2 = "";
      this.rcbInA.RegisterType = ERegisterType.IsDouble;
      this.rcbInA.Size = new Size(49, 21);
      this.rcbInA.TabIndex = 13;
      this.rcbInA.UnitText = "";
      this.rcbInA.UseRegister2 = true;
      this.rcbInB.CalculationRule = "";
      this.rcbInB.CalculationValue = new Decimal(new int[4]);
      this.rcbInB.Caption = "VB";
      this.rcbInB.Checked = false;
      this.rcbInB.IsRegister = true;
      this.rcbInB.Location = new Point(3, 327);
      this.rcbInB.Name = "rcbInB";
      this.rcbInB.RegisterCaption1 = "";
      this.rcbInB.RegisterCaption2 = "";
      this.rcbInB.RegisterId = 85;
      this.rcbInB.RegisterName = "VB";
      this.rcbInB.RegisterName2 = "";
      this.rcbInB.RegisterType = ERegisterType.IsDouble;
      this.rcbInB.Size = new Size(49, 21);
      this.rcbInB.TabIndex = 14;
      this.rcbInB.UnitText = "";
      this.rcbInB.UseRegister2 = true;
      this.rcbM1.CalculationRule = "";
      this.rcbM1.CalculationValue = new Decimal(new int[4]);
      this.rcbM1.Caption = "M1";
      this.rcbM1.Checked = false;
      this.rcbM1.IsRegister = true;
      this.rcbM1.Location = new Point(3, 354);
      this.rcbM1.Name = "rcbM1";
      this.rcbM1.RegisterCaption1 = "";
      this.rcbM1.RegisterCaption2 = "";
      this.rcbM1.RegisterId = 72;
      this.rcbM1.RegisterName = "M1";
      this.rcbM1.RegisterName2 = "";
      this.rcbM1.RegisterType = ERegisterType.IsDouble;
      this.rcbM1.Size = new Size(49, 21);
      this.rcbM1.TabIndex = 15;
      this.rcbM1.UnitText = "";
      this.rcbM1.UseRegister2 = true;
      this.rcbM2.CalculationRule = "";
      this.rcbM2.CalculationValue = new Decimal(new int[4]);
      this.rcbM2.Caption = "M2";
      this.rcbM2.Checked = false;
      this.rcbM2.IsRegister = true;
      this.rcbM2.Location = new Point(3, 381);
      this.rcbM2.Name = "rcbM2";
      this.rcbM2.RegisterCaption1 = "";
      this.rcbM2.RegisterCaption2 = "";
      this.rcbM2.RegisterId = 73;
      this.rcbM2.RegisterName = "M2";
      this.rcbM2.RegisterName2 = "";
      this.rcbM2.RegisterType = ERegisterType.IsDouble;
      this.rcbM2.Size = new Size(49, 21);
      this.rcbM2.TabIndex = 16;
      this.rcbM2.UnitText = "";
      this.rcbM2.UseRegister2 = true;
      this.rcbT1.CalculationRule = "";
      this.rcbT1.CalculationValue = new Decimal(new int[4]);
      this.rcbT1.Caption = "T1";
      this.rcbT1.Checked = false;
      this.rcbT1.IsRegister = true;
      this.rcbT1.Location = new Point(3, 408);
      this.rcbT1.Name = "rcbT1";
      this.rcbT1.RegisterCaption1 = "";
      this.rcbT1.RegisterCaption2 = "";
      this.rcbT1.RegisterId = 86;
      this.rcbT1.RegisterName = "T1";
      this.rcbT1.RegisterName2 = "";
      this.rcbT1.RegisterType = ERegisterType.IsDouble;
      this.rcbT1.Size = new Size(47, 21);
      this.rcbT1.TabIndex = 17;
      this.rcbT1.UnitText = "";
      this.rcbT1.UseRegister2 = true;
      this.rcbT2.CalculationRule = "";
      this.rcbT2.CalculationValue = new Decimal(new int[4]);
      this.rcbT2.Caption = "T2";
      this.rcbT2.Checked = false;
      this.rcbT2.IsRegister = true;
      this.rcbT2.Location = new Point(3, 435);
      this.rcbT2.Name = "rcbT2";
      this.rcbT2.RegisterCaption1 = "";
      this.rcbT2.RegisterCaption2 = "";
      this.rcbT2.RegisterId = 87;
      this.rcbT2.RegisterName = "T2";
      this.rcbT2.RegisterName2 = "";
      this.rcbT2.RegisterType = ERegisterType.IsDouble;
      this.rcbT2.Size = new Size(47, 21);
      this.rcbT2.TabIndex = 9;
      this.rcbT2.UnitText = "";
      this.rcbT2.UseRegister2 = true;
      this.rcbT3.CalculationRule = "";
      this.rcbT3.CalculationValue = new Decimal(new int[4]);
      this.rcbT3.Caption = "T3";
      this.rcbT3.Checked = false;
      this.rcbT3.IsRegister = true;
      this.rcbT3.Location = new Point(157, 3);
      this.rcbT3.Name = "rcbT3";
      this.rcbT3.RegisterCaption1 = "";
      this.rcbT3.RegisterCaption2 = "";
      this.rcbT3.RegisterId = 88;
      this.rcbT3.RegisterName = "T3";
      this.rcbT3.RegisterName2 = "";
      this.rcbT3.RegisterType = ERegisterType.IsDouble;
      this.rcbT3.Size = new Size(47, 21);
      this.rcbT3.TabIndex = 10;
      this.rcbT3.UnitText = "";
      this.rcbT3.UseRegister2 = true;
      this.rcbT4.CalculationRule = "";
      this.rcbT4.CalculationValue = new Decimal(new int[4]);
      this.rcbT4.Caption = "T4";
      this.rcbT4.Checked = false;
      this.rcbT4.IsRegister = true;
      this.rcbT4.Location = new Point(157, 30);
      this.rcbT4.Name = "rcbT4";
      this.rcbT4.RegisterCaption1 = "";
      this.rcbT4.RegisterCaption2 = "";
      this.rcbT4.RegisterId = 122;
      this.rcbT4.RegisterName = "T4";
      this.rcbT4.RegisterName2 = "";
      this.rcbT4.RegisterType = ERegisterType.IsDouble;
      this.rcbT4.Size = new Size(47, 21);
      this.rcbT4.TabIndex = 42;
      this.rcbT4.UnitText = "";
      this.rcbT4.UseRegister2 = true;
      this.rcbT1_T2.CalculationRule = "";
      this.rcbT1_T2.CalculationValue = new Decimal(new int[4]);
      this.rcbT1_T2.Caption = "T1-T2";
      this.rcbT1_T2.Checked = false;
      this.rcbT1_T2.IsRegister = true;
      this.rcbT1_T2.Location = new Point(157, 57);
      this.rcbT1_T2.Name = "rcbT1_T2";
      this.rcbT1_T2.RegisterCaption1 = "";
      this.rcbT1_T2.RegisterCaption2 = "";
      this.rcbT1_T2.RegisterId = 89;
      this.rcbT1_T2.RegisterName = "T1_T2";
      this.rcbT1_T2.RegisterName2 = "";
      this.rcbT1_T2.RegisterType = ERegisterType.IsDouble;
      this.rcbT1_T2.Size = new Size(64, 21);
      this.rcbT1_T2.TabIndex = 43;
      this.rcbT1_T2.UnitText = "";
      this.rcbT1_T2.UseRegister2 = true;
      this.rcbFLOW1.CalculationRule = "";
      this.rcbFLOW1.CalculationValue = new Decimal(new int[4]);
      this.rcbFLOW1.Caption = "FLOW1";
      this.rcbFLOW1.Checked = false;
      this.rcbFLOW1.IsRegister = true;
      this.rcbFLOW1.Location = new Point(157, 84);
      this.rcbFLOW1.Name = "rcbFLOW1";
      this.rcbFLOW1.RegisterCaption1 = "";
      this.rcbFLOW1.RegisterCaption2 = "";
      this.rcbFLOW1.RegisterId = 74;
      this.rcbFLOW1.RegisterName = "FLOW1";
      this.rcbFLOW1.RegisterName2 = "";
      this.rcbFLOW1.RegisterType = ERegisterType.IsDouble;
      this.rcbFLOW1.Size = new Size(73, 21);
      this.rcbFLOW1.TabIndex = 45;
      this.rcbFLOW1.UnitText = "";
      this.rcbFLOW1.UseRegister2 = true;
      this.rcbFLOW2.CalculationRule = "";
      this.rcbFLOW2.CalculationValue = new Decimal(new int[4]);
      this.rcbFLOW2.Caption = "FLOW2";
      this.rcbFLOW2.Checked = false;
      this.rcbFLOW2.IsRegister = true;
      this.rcbFLOW2.Location = new Point(157, 111);
      this.rcbFLOW2.Name = "rcbFLOW2";
      this.rcbFLOW2.RegisterCaption1 = "";
      this.rcbFLOW2.RegisterCaption2 = "";
      this.rcbFLOW2.RegisterId = 75;
      this.rcbFLOW2.RegisterName = "FLOW2";
      this.rcbFLOW2.RegisterName2 = "";
      this.rcbFLOW2.RegisterType = ERegisterType.IsDouble;
      this.rcbFLOW2.Size = new Size(73, 21);
      this.rcbFLOW2.TabIndex = 44;
      this.rcbFLOW2.UnitText = "";
      this.rcbFLOW2.UseRegister2 = true;
      this.rcbEFFECT1.CalculationRule = "";
      this.rcbEFFECT1.CalculationValue = new Decimal(new int[4]);
      this.rcbEFFECT1.Caption = "EFFECT1";
      this.rcbEFFECT1.Checked = false;
      this.rcbEFFECT1.IsRegister = true;
      this.rcbEFFECT1.Location = new Point(157, 138);
      this.rcbEFFECT1.Name = "rcbEFFECT1";
      this.rcbEFFECT1.RegisterCaption1 = "";
      this.rcbEFFECT1.RegisterCaption2 = "";
      this.rcbEFFECT1.RegisterId = 80;
      this.rcbEFFECT1.RegisterName = "EFFECT1";
      this.rcbEFFECT1.RegisterName2 = "";
      this.rcbEFFECT1.RegisterType = ERegisterType.IsDouble;
      this.rcbEFFECT1.Size = new Size(84, 21);
      this.rcbEFFECT1.TabIndex = 46;
      this.rcbEFFECT1.UnitText = "";
      this.rcbEFFECT1.UseRegister2 = true;
      this.rcbP1.CalculationRule = "";
      this.rcbP1.CalculationValue = new Decimal(new int[4]);
      this.rcbP1.Caption = "P1";
      this.rcbP1.Checked = false;
      this.rcbP1.IsRegister = true;
      this.rcbP1.Location = new Point(157, 165);
      this.rcbP1.Name = "rcbP1";
      this.rcbP1.RegisterCaption1 = "";
      this.rcbP1.RegisterCaption2 = "";
      this.rcbP1.RegisterId = 91;
      this.rcbP1.RegisterName = "P1";
      this.rcbP1.RegisterName2 = "";
      this.rcbP1.RegisterType = ERegisterType.IsDouble;
      this.rcbP1.Size = new Size(48, 21);
      this.rcbP1.TabIndex = 39;
      this.rcbP1.UnitText = "";
      this.rcbP1.UseRegister2 = true;
      this.rcbP2.CalculationRule = "";
      this.rcbP2.CalculationValue = new Decimal(new int[4]);
      this.rcbP2.Caption = "P2";
      this.rcbP2.Checked = false;
      this.rcbP2.IsRegister = true;
      this.rcbP2.Location = new Point(157, 192);
      this.rcbP2.Name = "rcbP2";
      this.rcbP2.RegisterCaption1 = "";
      this.rcbP2.RegisterCaption2 = "";
      this.rcbP2.RegisterId = 92;
      this.rcbP2.RegisterName = "P2";
      this.rcbP2.RegisterName2 = "";
      this.rcbP2.RegisterType = ERegisterType.IsDouble;
      this.rcbP2.Size = new Size(48, 21);
      this.rcbP2.TabIndex = 38;
      this.rcbP2.UnitText = "";
      this.rcbP2.UseRegister2 = true;
      this.rcbInfo.CalculationRule = "";
      this.rcbInfo.CalculationValue = new Decimal(new int[4]);
      this.rcbInfo.Caption = "Info";
      this.rcbInfo.Checked = false;
      this.rcbInfo.IsRegister = true;
      this.rcbInfo.Location = new Point(157, 219);
      this.rcbInfo.Name = "rcbInfo";
      this.rcbInfo.RegisterCaption1 = "";
      this.rcbInfo.RegisterCaption2 = "";
      this.rcbInfo.RegisterId = 99;
      this.rcbInfo.RegisterName = "INFO";
      this.rcbInfo.RegisterName2 = "";
      this.rcbInfo.RegisterType = ERegisterType.IsDouble;
      this.rcbInfo.Size = new Size(53, 21);
      this.rcbInfo.TabIndex = 26;
      this.rcbInfo.UnitText = "";
      this.rcbInfo.UseRegister2 = true;
      this.rcbLogQOS.CalculationRule = "";
      this.rcbLogQOS.CalculationValue = new Decimal(new int[4]);
      this.rcbLogQOS.Caption = "Log QOS";
      this.rcbLogQOS.Checked = false;
      this.rcbLogQOS.IsRegister = true;
      this.rcbLogQOS.Location = new Point(157, 246);
      this.rcbLogQOS.Name = "rcbLogQOS";
      this.rcbLogQOS.RegisterCaption1 = "";
      this.rcbLogQOS.RegisterCaption2 = "";
      this.rcbLogQOS.RegisterId = 200;
      this.rcbLogQOS.RegisterName = "LogQOS";
      this.rcbLogQOS.RegisterName2 = "";
      this.rcbLogQOS.RegisterType = ERegisterType.IsDouble;
      this.rcbLogQOS.Size = new Size(81, 21);
      this.rcbLogQOS.TabIndex = 47;
      this.rcbLogQOS.UnitText = "";
      this.rcbLogQOS.UseRegister2 = true;
      this.rcbHR.CalculationRule = "";
      this.rcbHR.CalculationValue = new Decimal(new int[4]);
      this.rcbHR.Caption = "Hour Counter";
      this.rcbHR.Checked = false;
      this.rcbHR.IsRegister = true;
      this.rcbHR.Location = new Point(157, 273);
      this.rcbHR.Name = "rcbHR";
      this.rcbHR.RegisterCaption1 = "";
      this.rcbHR.RegisterCaption2 = "";
      this.rcbHR.RegisterId = 1004;
      this.rcbHR.RegisterName = "HR";
      this.rcbHR.RegisterName2 = "";
      this.rcbHR.RegisterType = ERegisterType.IsDouble;
      this.rcbHR.Size = new Size(102, 21);
      this.rcbHR.TabIndex = 48;
      this.rcbHR.UnitText = "";
      this.rcbHR.UseRegister2 = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(734, 498);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.grpUsed);
      this.Controls.Add((Control) this.grpCalculatedRegisters);
      this.Controls.Add((Control) this.grpCalculate);
      this.Controls.Add((Control) this.grpGraphs);
      this.Controls.Add((Control) this.grpReadLog);
      this.Controls.Add((Control) this.grpRegisters);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(742, 532);
      this.Name = "FrmKMPLogger";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "KMP Logger";
      this.Load += new EventHandler(this.FrmKMPLogger_Load);
      this.grpUsed.ResumeLayout(false);
      this.grpCalculatedRegisters.ResumeLayout(false);
      this.grpCalculate.ResumeLayout(false);
      this.grpCalculate.PerformLayout();
      this.grpGraphs.ResumeLayout(false);
      this.grpReadLog.ResumeLayout(false);
      this.grpReadLog.PerformLayout();
     // this.cbxTo.EndInit();
     // this.cbxFrom.EndInit();
      this.grpRegisters.ResumeLayout(false);
      this.flpRegisters.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.nudCalc.EndInit();
      this.ResumeLayout(false);
    }

    internal delegate void dEmpty();
  }
}
