﻿// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmYearLogger
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Kamstrup.Heat.mc601Communication;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace MC601LogView
{
  public class FrmYearLogger : Form
  {
    private string m_DataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\kamstrup\\METERTOOLLogViewer601";
    private Functions m_mc601Functions = new Functions();
    private YearLogDataSet m_YLDS = new YearLogDataSet();
    private clsCalculatedRegisters m_userRegisters = new clsCalculatedRegisters();
    private YearLogDataSet.RegisterInUseRow m_RegisterInUseRow;
    private YearLogDataSet.RegisterUnitRow m_RegisterUnitRow;
    private bool bConnectionLost;
    private IContainer components;
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
    private ComboBox cbxTo;
    private ComboBox cbxFrom;
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
    private UCRegisterCheckBox rcbInfo;
    private Button btnSelectAll;
    private Button btnSelectNone;
    private UCRegisterCheckBox rcbMaxFlow1DateMdr;
    private UCRegisterCheckBox rcbMaxFlow1Year;
    private UCRegisterCheckBox rcbMinFlow1DateYear;
    private UCRegisterCheckBox rcbMinFlow1Year;
    private UCRegisterCheckBox rcbMaxEff1DateYear;
    private UCRegisterCheckBox rcbMaxEff1Year;
    private UCRegisterCheckBox rcbMinEff1DateYear;
    private UCRegisterCheckBox rcbMinEff1Year;
    private UCRegisterCheckBox rcbE8;
    private UCRegisterCheckBox rcbE9;
    private UCRegisterCheckBox rcbTA2;
    private UCRegisterCheckBox rcbTA3;
    private GroupBox grpUsed;
    private UCRegisterCheckBox rcbUsedE1;
    private UCRegisterCheckBox rcbUsedE8;
    private UCRegisterCheckBox rcbUsedE9;
    private UCRegisterCheckBox rcbUsedV1;
    private UCRegisterCheckBox rcbUsedV2;
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
        ((FrmMC601LogView) this.MdiParent).ProgressBar = value;
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

    public FrmYearLogger()
    {
      this.InitializeComponent();
      this.m_userRegisters.Load("YearLoggerUR");
      this.InsertCalculatedRegister();
      this.ClearData();
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

    private void frmYearLogger_Load(object sender, EventArgs e)
    {
      this.cbxFrom.DataSource = (object) FrmYearLogger.MakeListOfYears(15);
      this.cbxFrom.DisplayMember = "Text";
      this.cbxFrom.ValueMember = "Value";
      this.cbxTo.DataSource = (object) FrmYearLogger.MakeListOfYears(15);
      this.cbxTo.DisplayMember = "Text";
      this.cbxTo.ValueMember = "Value";
    }

    private static DataTable MakeListOfYears(int numberOfYears)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Value", typeof (ushort));
      dataTable.Columns.Add("Text", typeof (string));
      dataTable.NewRow();
      DataRow row1 = dataTable.NewRow();
      row1["Value"] = (object) 1;
      row1["Text"] = (object) "Newest year";
      dataTable.Rows.Add(row1);
      for (ushort index = (ushort) 2; (int) index <= numberOfYears; ++index)
      {
        DataRow row2 = dataTable.NewRow();
        row2["Value"] = (object) index;
        row2["Text"] = (object) ("-" + ((int) index - 1).ToString() + " years.");
        dataTable.Rows.Add(row2);
      }
      return dataTable;
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
          if (registerCheckBox.RegisterType == ERegisterType.IsDouble)
            dataTable.Columns.Add(registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]", typeof (double));
          else if (registerCheckBox.RegisterType == ERegisterType.IsDateTime)
            dataTable.Columns.Add(registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]", typeof (DateTime));
        }
      }
      foreach (Control control in (ArrangedElementCollection) this.grpUsed.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null && registerCheckBox.Checked && registerCheckBox.Enabled)
        {
          byte nUnit = Convert.ToByte(this.m_RegisterUnitRow[registerCheckBox.RegisterName]);
          registerCheckBox.UnitText = ClsUtils.UnitsForRegisters(nUnit);
          dataTable.Columns.Add(registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]", typeof (double));
        }
      }
      YearLogDataSet.RegisterRow registerRow = (YearLogDataSet.RegisterRow) null;
      foreach (YearLogDataSet.RegisterRow row in this.m_YLDS.Register)
      {
        DataRow dataRow = dataTable.NewRow();
        dataRow["Date"] = row["Date"];
        foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
        {
          UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
          if (registerCheckBox != null && registerCheckBox.Checked && registerCheckBox.Enabled)
          {
            if (registerCheckBox.RegisterType == ERegisterType.IsDouble)
            {
              Decimal num = Convert.ToDecimal(row[registerCheckBox.RegisterName]);
              dataRow[registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]"] = (object) num;
            }
            else if (registerCheckBox.RegisterType == ERegisterType.IsDateTime && Convert.ToDateTime(row[registerCheckBox.RegisterName]) != new DateTime(2000, 1, 1))
              dataRow[registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]"] = (object) Convert.ToDateTime(row[registerCheckBox.RegisterName]);
          }
        }
        foreach (Control control in (ArrangedElementCollection) this.grpUsed.Controls)
        {
          UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
          if (registerCheckBox != null && registerCheckBox.Checked && registerCheckBox.Enabled)
          {
            if (registerRow != null)
            {
              Decimal num1 = Convert.ToDecimal(registerRow[registerCheckBox.RegisterName]);
              Decimal num2 = Convert.ToDecimal(row[registerCheckBox.RegisterName]);
              dataRow[registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]"] = num2 == new Decimal(0) || num1 == new Decimal(0) ? (object) 0 : (object) (num1 - num2);
            }
            else
              dataRow[registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]"] = (object) 0;
          }
        }
        registerRow = row;
        foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
        {
          UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
          if (rcb != null)
            FrmYearLogger.CalculateCalculatedRowForRCB(dataRow, rcb, row);
        }
        dataTable.Rows.InsertAt(dataRow, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_YLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_YLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private static void CalculateCalculatedRowForRCB(DataRow dr, UCRegisterCheckBox rcb, YearLogDataSet.RegisterRow row)
    {
      if (!rcb.Checked || !rcb.Enabled)
        return;
      if (rcb.UseRegister2)
      {
        Decimal num = FrmYearLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), Convert.ToDecimal(row[rcb.RegisterName2]), rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.RegisterName2] = (object) num;
      }
      else
      {
        Decimal num = FrmYearLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), rcb.CalculationValue, rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.CalculationValue.ToString()] = (object) num;
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

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (!this.IsAnyRCBChecked())
      {
        int num1 = (int) MessageBox.Show("Select one or more registers.", "Select registers", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        this.Cursor = Cursors.WaitCursor;
        ushort num2 = (ushort) this.cbxFrom.SelectedValue;
        ushort num3 = (ushort) this.cbxTo.SelectedValue;
        if ((int) num2 < (int) num3)
          this.ReadData(num2, num3);
        else
          this.ReadData(num3, num2);
        this.Progress = 0;
        this.SetupCheckableRCBs();
        if (this.m_YLDS.Register.Rows.Count > 0)
        {
          this.Text = "Yearly Log | Serial No " + this.m_YLDS.CustomerNo.GetCustomerNo() + " |  Not saved.";
          this.btnSave.Enabled = true;
          if (MessageBox.Show("Reading of data completed.\r\nDo you wish to save the data?", "Reading completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            this.Save();
          this.btnCalculate.Enabled = true;
          this.btnAddToRegs.Enabled = true;
          this.btnSelectedRegisters.Enabled = true;
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
          if (flag && registerCheckBox.RegisterType == ERegisterType.IsDouble)
          {
            CbxItem cbxItem = new CbxItem();
            cbxItem.Register = registerCheckBox;
            this.cbxRegister1.Items.Add((object) cbxItem);
            this.cbxRegister2.Items.Add((object) cbxItem);
          }
          registerCheckBox.Enabled = flag;
        }
      }
      foreach (Control control in (ArrangedElementCollection) this.grpUsed.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && this.m_YLDS.Register.Count > 0;
      }
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
        {
          if (registerCheckBox.UseRegister2)
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName2] && this.m_YLDS.Register.Count > 0;
            registerCheckBox.Enabled = flag;
          }
          else
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && this.m_YLDS.Register.Count > 0;
            registerCheckBox.Enabled = flag;
          }
        }
      }
    }

    private void ReadData(ushort fromYear, ushort toYear)
    {
      this.bConnectionLost = false;
      this.m_YLDS.Register.Clear();
      this.m_YLDS.RegisterUnit.Clear();
      this.m_YLDS.RegisterInUse.Clear();
      this.m_YLDS.CustomerNo.Clear();
      this.m_RegisterUnitRow = this.m_YLDS.RegisterUnit.NewRegisterUnitRow();
      this.m_YLDS.RegisterUnit.AddRegisterUnitRow(this.m_RegisterUnitRow);
      this.m_RegisterInUseRow = this.m_YLDS.RegisterInUse.NewRegisterInUseRow();
      this.m_YLDS.RegisterInUse.AddRegisterInUseRow(this.m_RegisterInUseRow);
      this.m_RegisterInUseRow.E1 = this.rcbE1.Checked;
      this.m_RegisterInUseRow.E2 = this.rcbE2.Checked;
      this.m_RegisterInUseRow.E3 = this.rcbE3.Checked;
      this.m_RegisterInUseRow.E4 = this.rcbE4.Checked;
      this.m_RegisterInUseRow.E5 = this.rcbE5.Checked;
      this.m_RegisterInUseRow.E6 = this.rcbE6.Checked;
      this.m_RegisterInUseRow.E7 = this.rcbE7.Checked;
      this.m_RegisterInUseRow.E8 = this.rcbE8.Checked;
      this.m_RegisterInUseRow.E9 = this.rcbE9.Checked;
      this.m_RegisterInUseRow.TA2 = this.rcbTA2.Checked;
      this.m_RegisterInUseRow.TA3 = this.rcbTA3.Checked;
      this.m_RegisterInUseRow.V1 = this.rcbV1.Checked;
      this.m_RegisterInUseRow.V2 = this.rcbV2.Checked;
      this.m_RegisterInUseRow.INA = this.rcbInA.Checked;
      this.m_RegisterInUseRow.INB = this.rcbInB.Checked;
      this.m_RegisterInUseRow.MaxFlow1DateYear = this.rcbMaxFlow1DateMdr.Checked;
      this.m_RegisterInUseRow.MaxFlow1Year = this.rcbMaxFlow1Year.Checked;
      this.m_RegisterInUseRow.MinFlow1DateYear = this.rcbMinFlow1DateYear.Checked;
      this.m_RegisterInUseRow.MinFlow1Year = this.rcbMinFlow1Year.Checked;
      this.m_RegisterInUseRow.MaxEff1DateYear = this.rcbMaxEff1DateYear.Checked;
      this.m_RegisterInUseRow.MaxEff1Year = this.rcbMaxEff1Year.Checked;
      this.m_RegisterInUseRow.MinEff1DateYear = this.rcbMinEff1DateYear.Checked;
      this.m_RegisterInUseRow.MinEff1Year = this.rcbMinEff1Year.Checked;
      this.m_RegisterInUseRow.INFO = this.rcbInfo.Checked;
      this.Progress = 0;
      this.ProgressMax = (int) toYear - (int) fromYear + 1;
      this.Progress = 0;
      this.ReadCustomerNo(ref this.bConnectionLost);
      ushort lastFoundYear = (ushort) 0;
      bool bDone = false;
      while (!bDone && !this.bConnectionLost)
      {
        this.ReadSetOfYears(fromYear, toYear, ref lastFoundYear, ref bDone);
        if ((int) fromYear == (int) lastFoundYear || (int) toYear == (int) lastFoundYear)
        {
          bDone = true;
        }
        else
        {
          fromYear = lastFoundYear;
          ++fromYear;
        }
        try
        {
          ++this.Progress;
        }
        catch
        {
          this.Progress = this.ProgressMax;
        }
        if (this.bConnectionLost)
        {
          int num = (int) MessageBox.Show("Lost the connection to the MC601 Meter!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.m_YLDS.Register.Clear();
          this.lblRecords.Text = this.m_YLDS.Register.Count.ToString();
          return;
        }
      }
      this.lblRecords.Text = this.m_YLDS.Register.Count.ToString();
    }

    private void ReadCustomerNo(ref bool bConnectionLost)
    {
      try
      {
        Functions functions = new Functions();
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add((object) 1010);
        arrRegisters.Add((object) 112);
        arrRegisters.Add((object) 157);
        string ErrorMessage = "";
        if (functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
          this.m_YLDS.CustomerNo.SetCustomerNo(Convert.ToString((double) arrValues[0] + (double) arrValues[1]));
        else
          bConnectionLost = true;
      }
      catch
      {
        bConnectionLost = true;
      }
    }

    private void ReadSetOfYears(ushort fromYear, ushort endYear, ref ushort lastFoundYear, ref bool bDone)
    {
      byte NumberOfRecords = (byte) Math.Min(16, (int) endYear - (int) fromYear + 1);
      ArrayList arrValues = new ArrayList();
      ArrayList rowArray = new ArrayList();
      string ErrorMessage = "";
      byte UnitId = (byte) 0;
      bool histYearData = this.m_mc601Functions.GetHistYearData((byte) 63, (ushort) 1003, fromYear, NumberOfRecords, arrValues, out UnitId, out ErrorMessage);
      byte num1 = (byte) arrValues.Count;
      lastFoundYear = fromYear;
      lastFoundYear += (ushort) (arrValues.Count - 1);
      if (histYearData)
      {
        for (int index = 0; index < (int) num1; ++index)
        {
          double num2 = (double) arrValues[index];
          if (num2 != 0.0)
          {
            DateTime dateTime = new DateTime(2000 + Convert.ToInt32(num2) / 10000, Convert.ToInt32(num2) % 10000 / 100, Convert.ToInt32(num2) % 100);
            YearLogDataSet.RegisterRow row = this.m_YLDS.Register.NewRegisterRow();
            row["Id"] = (object) ((int) fromYear + index);
            row["Date"] = (object) dateTime;
            this.m_YLDS.Register.AddRegisterRow(row);
            rowArray.Add((object) row);
          }
          else
          {
            lastFoundYear = fromYear;
            lastFoundYear += (ushort) index;
            num1 = (byte) index;
            bDone = true;
          }
        }
      }
      else
        this.bConnectionLost = true;
      if (this.bConnectionLost)
        return;
      if (rowArray.Count > 0)
      {
        foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
        {
          UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
          if (rcb != null && rcb.Checked && !this.bConnectionLost)
            this.ReadYearRegister(rcb, rowArray, fromYear, (ushort) num1);
        }
      }
      else
      {
        int num3 = (int) MessageBox.Show("There is no data for the selected years.");
      }
    }

    private void ReadYearRegister(UCRegisterCheckBox rcb, ArrayList rowArray, ushort fromYear, ushort numberOfYears)
    {
      byte UnitId = (byte) 0;
      string ErrorMessage = "";
      ArrayList arrValues = new ArrayList();
      if (this.m_mc601Functions.GetHistYearData((byte) 63, (ushort) rcb.RegisterId, fromYear, (byte) numberOfYears, arrValues, out UnitId, out ErrorMessage))
      {
        if (arrValues.Count <= 0)
          return;
        this.m_RegisterUnitRow[rcb.RegisterName] = (object) UnitId;
        for (byte index = (byte) 0; (int) index < arrValues.Count; ++index)
        {
          YearLogDataSet.RegisterRow registerRow = (YearLogDataSet.RegisterRow) rowArray[(int) index];
          double num = (double) arrValues[(int) index];
          if (rcb.RegisterType == ERegisterType.IsDouble)
            registerRow[rcb.RegisterName] = (object) num;
          else if (rcb.RegisterType == ERegisterType.IsDateTime)
          {
            int year = 2000 + Convert.ToInt32(num) / 10000;
            int month = Convert.ToInt32(num) % 10000 / 100;
            int day = Convert.ToInt32(num) % 100;
            if (num != 0.0)
            {
              DateTime dateTime = new DateTime(year, month, day);
              registerRow[rcb.RegisterName] = (object) dateTime;
            }
            else
            {
              DateTime dateTime = new DateTime(2000, 1, 1);
              registerRow[rcb.RegisterName] = (object) dateTime;
            }
          }
        }
      }
      else
        this.bConnectionLost = true;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      if (this.btnSave.Enabled && MessageBox.Show("Do you wish to save the data,\r\nbefore you clear?", "Save data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.Save();
      this.ClearData();
    }

    private void ClearData()
    {
      this.m_YLDS.Register.Clear();
      this.m_YLDS.RegisterInUse.Clear();
      this.m_YLDS.RegisterUnit.Clear();
      this.cbxRegister1.Items.Clear();
      this.cbxRegister2.Items.Clear();
      this.lblRecords.Text = this.m_YLDS.Register.Count.ToString();
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = true;
      }
      foreach (Control control in (ArrangedElementCollection) this.grpUsed.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = false;
      }
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = false;
      }
      this.btnSave.Enabled = false;
      this.btnCalculate.Enabled = false;
      this.btnAddToRegs.Enabled = false;
      this.btnSelectedRegisters.Enabled = false;
      this.Text = "Yearly Log | Serial No " + this.m_YLDS.CustomerNo.GetCustomerNo() + " | Empty.";
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
      openFileDialog.Filter = "MC601 Yearly Log (*.MC601YearLog)|*.MC601YearLog|All files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        int num = (int) this.m_YLDS.ReadXml(openFileDialog.FileName);
        this.m_RegisterInUseRow = (YearLogDataSet.RegisterInUseRow) this.m_YLDS.RegisterInUse.Rows[0];
        this.m_RegisterUnitRow = (YearLogDataSet.RegisterUnitRow) this.m_YLDS.RegisterUnit.Rows[0];
        this.lblRecords.Text = this.m_YLDS.Register.Count.ToString();
        this.btnRead.Text = "Start";
        this.SetupCheckableRCBs();
        this.btnSave.Enabled = false;
        this.btnCalculate.Enabled = true;
        this.btnAddToRegs.Enabled = true;
        this.btnSelectedRegisters.Enabled = true;
        this.Progress = 0;
        this.Text = "Yearly Log | Serial No " + this.m_YLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(openFileDialog.FileName);
      }
      catch
      {
        int num = (int) MessageBox.Show("Failed to load.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.Save();
    }

    private void Save()
    {
      if (this.m_YLDS.Register.Rows.Count < 1)
      {
        int num1 = (int) MessageBox.Show("The Yearly Log is empty.", "No data to save!");
      }
      else
      {
        try
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
            Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
          saveFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
          saveFileDialog.Filter = "MC601 Yearly Log (*.MC601YearLog)|*.MC601YearLog|All files (*.*)|*.*";
          saveFileDialog.FilterIndex = 1;
          saveFileDialog.RestoreDirectory = true;
          if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
          if (saveFileDialog.FileName.IndexOf(".MC601YearLog") < 0)
            saveFileDialog.FileName = saveFileDialog.FileName + ".MC601YearLog";
          this.m_YLDS.WriteXml(saveFileDialog.FileName);
          this.btnSave.Enabled = false;
          this.Text = "Yearly Log | Serial No " + this.m_YLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(saveFileDialog.FileName);
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Failed to save the the Yearly Log:" + ex.Message);
        }
      }
    }

    private void btnCalculate_Click(object sender, EventArgs e)
    {
      CbxItem cbxItem1 = (CbxItem) this.cbxRegister1.SelectedItem;
      CbxItem cbxItem2 = (CbxItem) this.cbxRegister2.SelectedItem;
      if (this.rdbCalcA.Checked)
      {
        if (cbxItem1 != null && cbxItem2 != null && this.cbxCalcRule.SelectedItem != null)
        {
          this.Reg1XReg2(cbxItem1.Register, cbxItem2.Register, this.cbxCalcRule.SelectedItem.ToString());
        }
        else
        {
          int num1 = (int) MessageBox.Show("You need to select the right registers.");
        }
      }
      else if (cbxItem1 != null && this.cbxCalcRule.SelectedItem != null)
      {
        this.Reg1XReg2(cbxItem1.Register, this.nudCalc.Value, this.cbxCalcRule.SelectedItem.ToString());
      }
      else
      {
        int num2 = (int) MessageBox.Show("You need to select the right registers.");
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
      foreach (YearLogDataSet.RegisterRow registerRow in (InternalDataCollectionBase) this.m_YLDS.Register.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] = (object) registerRow.Date;
        Decimal register1 = Convert.ToDecimal(registerRow[registerName1]);
        Decimal register2 = Convert.ToDecimal(registerRow[registerName2]);
        row[columnName] = (object) FrmYearLogger.DoCalculation(register1, register2, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_YLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_YLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
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
      foreach (YearLogDataSet.RegisterRow registerRow in (InternalDataCollectionBase) this.m_YLDS.Register.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] = (object) registerRow.Date;
        Decimal register1 = Convert.ToDecimal(registerRow[registerName]);
        row[columnName] = (object) FrmYearLogger.DoCalculation(register1, calcValue, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_YLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_YLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
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
          if (calculatedRegister != null && calculatedRegister.UseRegister2 && (calculatedRegister.RegisterName_1 == ur.RegisterName_1 && calculatedRegister.RegisterName_2 == ur.RegisterName_2) && calculatedRegister.CalculationRule == ur.CalculationRule)
          {
            int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          }
        }
        this.m_userRegisters.ListOfCalculatedRegisters.Add((object) ur);
        this.m_userRegisters.Save("YearLoggerUR");
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
        ur.CalculationValue = this.nudCalc.Value;
        ur.UseRegister2 = false;
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
        this.m_userRegisters.ListOfCalculatedRegisters.Add((object) ur);
        this.m_userRegisters.Save("YearLoggerUR");
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
      registerCheckBox.CalculationValue = ur.CalculationValue;
      registerCheckBox.UseRegister2 = ur.UseRegister2;
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
                if (calculatedRegister.RegisterName_1 == registerCheckBox.RegisterName && calculatedRegister.RegisterName_2 == registerCheckBox.RegisterName2 && calculatedRegister.CalculationRule == registerCheckBox.CalculationRule)
                {
                  this.m_userRegisters.ListOfCalculatedRegisters.Remove(obj);
                  registerCheckBox.Dispose();
                  break;
                }
              }
              else if (calculatedRegister.RegisterName_1 == registerCheckBox.RegisterName && !calculatedRegister.UseRegister2 && (calculatedRegister.CalculationValue == registerCheckBox.CalculationValue && calculatedRegister.CalculationRule == registerCheckBox.CalculationRule))
              {
                this.m_userRegisters.ListOfCalculatedRegisters.Remove(obj);
                registerCheckBox.Dispose();
                break;
              }
            }
          }
        }
      }
      this.m_userRegisters.Save("YearLoggerUR");
    }

    private void btnRemoveAllCalculated_Click(object sender, EventArgs e)
    {
      this.m_userRegisters.ListOfCalculatedRegisters.Clear();
      this.m_userRegisters.Save("YearLoggerUR");
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

    private void grpCalculatedRegisters_Enter(object sender, EventArgs e)
    {
    }

    private void rdbCalcA_CheckedChanged(object sender, EventArgs e)
    {
      this.cbxRegister2.Enabled = this.rdbCalcA.Checked;
      this.nudCalc.Enabled = this.rdbCalcB.Checked;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmYearLogger));
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
      this.cbxTo = new ComboBox();
      this.cbxFrom = new ComboBox();
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
      this.rcbE1 = new UCRegisterCheckBox();
      this.rcbE7 = new UCRegisterCheckBox();
      this.rcbE3 = new UCRegisterCheckBox();
      this.rcbE4 = new UCRegisterCheckBox();
      this.rcbE5 = new UCRegisterCheckBox();
      this.rcbE6 = new UCRegisterCheckBox();
      this.rcbE2 = new UCRegisterCheckBox();
      this.rcbE8 = new UCRegisterCheckBox();
      this.rcbE9 = new UCRegisterCheckBox();
      this.rcbTA2 = new UCRegisterCheckBox();
      this.rcbTA3 = new UCRegisterCheckBox();
      this.rcbV1 = new UCRegisterCheckBox();
      this.rcbV2 = new UCRegisterCheckBox();
      this.rcbInA = new UCRegisterCheckBox();
      this.rcbInB = new UCRegisterCheckBox();
      this.rcbInfo = new UCRegisterCheckBox();
      this.rcbMaxFlow1DateMdr = new UCRegisterCheckBox();
      this.rcbMaxFlow1Year = new UCRegisterCheckBox();
      this.rcbMinFlow1DateYear = new UCRegisterCheckBox();
      this.rcbMinFlow1Year = new UCRegisterCheckBox();
      this.rcbMaxEff1DateYear = new UCRegisterCheckBox();
      this.rcbMaxEff1Year = new UCRegisterCheckBox();
      this.rcbMinEff1DateYear = new UCRegisterCheckBox();
      this.rcbMinEff1Year = new UCRegisterCheckBox();
      this.btnSelectAll = new Button();
      this.btnSelectNone = new Button();
      this.grpUsed = new GroupBox();
      this.rcbUsedE1 = new UCRegisterCheckBox();
      this.rcbUsedE8 = new UCRegisterCheckBox();
      this.rcbUsedE9 = new UCRegisterCheckBox();
      this.rcbUsedV1 = new UCRegisterCheckBox();
      this.rcbUsedV2 = new UCRegisterCheckBox();
      this.rdbCalcB = new RadioButton();
      this.rdbCalcA = new RadioButton();
      this.nudCalc = new NumericUpDown();
      this.grpCalculatedRegisters.SuspendLayout();
      this.grpCalculate.SuspendLayout();
      this.grpGraphs.SuspendLayout();
      this.grpReadLog.SuspendLayout();
      this.grpRegisters.SuspendLayout();
      this.flpRegisters.SuspendLayout();
      this.grpUsed.SuspendLayout();
      this.nudCalc.BeginInit();
      this.SuspendLayout();
      this.grpCalculatedRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.grpCalculatedRegisters.BackColor = Color.Transparent;
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveAllCalculated);
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveCalculatedRegister);
      this.grpCalculatedRegisters.Controls.Add((Control) this.flpCalculatedRegisters);
      this.grpCalculatedRegisters.Location = new Point(503, 173);
      this.grpCalculatedRegisters.Name = "grpCalculatedRegisters";
      this.grpCalculatedRegisters.Size = new Size(226, 264);
      this.grpCalculatedRegisters.TabIndex = 21;
      this.grpCalculatedRegisters.TabStop = false;
      this.grpCalculatedRegisters.Text = "Calculated Registers";
      this.grpCalculatedRegisters.Enter += new EventHandler(this.grpCalculatedRegisters_Enter);
      this.btnRemoveAllCalculated.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnRemoveAllCalculated.Location = new Point(126, 234);
      this.btnRemoveAllCalculated.Name = "btnRemoveAllCalculated";
      this.btnRemoveAllCalculated.Size = new Size(94, 23);
      this.btnRemoveAllCalculated.TabIndex = 30;
      this.btnRemoveAllCalculated.Text = "Remove All";
      this.btnRemoveAllCalculated.UseVisualStyleBackColor = true;
      this.btnRemoveAllCalculated.Click += new EventHandler(this.btnRemoveAllCalculated_Click);
      this.btnRemoveCalculatedRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRemoveCalculatedRegister.Location = new Point(6, 234);
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
      this.flpCalculatedRegisters.Size = new Size(220, 211);
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
      this.grpCalculate.Location = new Point(11, 173);
      this.grpCalculate.Name = "grpCalculate";
      this.grpCalculate.Size = new Size(187, 163);
      this.grpCalculate.TabIndex = 20;
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
        (object) "+",
        (object) "-",
        (object) "*",
        (object) "/"
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
      this.grpGraphs.Location = new Point(11, 342);
      this.grpGraphs.Name = "grpGraphs";
      this.grpGraphs.Size = new Size(187, 50);
      this.grpGraphs.TabIndex = 19;
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
      this.grpReadLog.Location = new Point(12, 12);
      this.grpReadLog.Name = "grpReadLog";
      this.grpReadLog.Size = new Size(186, 155);
      this.grpReadLog.TabIndex = 18;
      this.grpReadLog.TabStop = false;
      this.grpReadLog.Text = "Yearly Log";
      this.cbxTo.FormattingEnabled = true;
      this.cbxTo.Location = new Point(44, 48);
      this.cbxTo.Name = "cbxTo";
      this.cbxTo.Size = new Size(136, 21);
      this.cbxTo.TabIndex = 18;
      this.cbxFrom.FormattingEnabled = true;
      this.cbxFrom.Location = new Point(44, 21);
      this.cbxFrom.Name = "cbxFrom";
      this.cbxFrom.Size = new Size(136, 21);
      this.cbxFrom.TabIndex = 17;
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
      this.btnClear.Location = new Point(105, 75);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new Size(75, 23);
      this.btnClear.TabIndex = 11;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new EventHandler(this.btnClear_Click);
      this.lblRecords.AutoSize = true;
      this.lblRecords.Location = new Point(54, 99);
      this.lblRecords.Name = "lblRecords";
      this.lblRecords.Size = new Size(13, 13);
      this.lblRecords.TabIndex = 10;
      this.lblRecords.Text = "0";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 99);
      this.label3.Name = "label3";
      this.label3.Size = new Size(50, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Records:";
      this.btnSave.Enabled = false;
      this.btnSave.Location = new Point(105, 123);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 7;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnLoad.Location = new Point(6, 123);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new Size(75, 23);
      this.btnLoad.TabIndex = 6;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
      this.btnRead.Location = new Point(6, 75);
      this.btnRead.Name = "btnRead";
      this.btnRead.Size = new Size(75, 23);
      this.btnRead.TabIndex = 3;
      this.btnRead.Text = "Read";
      this.btnRead.UseVisualStyleBackColor = true;
      this.btnRead.Click += new EventHandler(this.btnStart_Click);
      this.grpRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpRegisters.BackColor = Color.Transparent;
      this.grpRegisters.Controls.Add((Control) this.flpRegisters);
      this.grpRegisters.Location = new Point(204, 12);
      this.grpRegisters.Name = "grpRegisters";
      this.grpRegisters.Size = new Size(290, 425);
      this.grpRegisters.TabIndex = 17;
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
      this.flpRegisters.Controls.Add((Control) this.rcbTA2);
      this.flpRegisters.Controls.Add((Control) this.rcbTA3);
      this.flpRegisters.Controls.Add((Control) this.rcbV1);
      this.flpRegisters.Controls.Add((Control) this.rcbV2);
      this.flpRegisters.Controls.Add((Control) this.rcbInA);
      this.flpRegisters.Controls.Add((Control) this.rcbInB);
      this.flpRegisters.Controls.Add((Control) this.rcbInfo);
      this.flpRegisters.Controls.Add((Control) this.rcbMaxFlow1DateMdr);
      this.flpRegisters.Controls.Add((Control) this.rcbMaxFlow1Year);
      this.flpRegisters.Controls.Add((Control) this.rcbMinFlow1DateYear);
      this.flpRegisters.Controls.Add((Control) this.rcbMinFlow1Year);
      this.flpRegisters.Controls.Add((Control) this.rcbMaxEff1DateYear);
      this.flpRegisters.Controls.Add((Control) this.rcbMaxEff1Year);
      this.flpRegisters.Controls.Add((Control) this.rcbMinEff1DateYear);
      this.flpRegisters.Controls.Add((Control) this.rcbMinEff1Year);
      this.flpRegisters.Controls.Add((Control) this.btnSelectAll);
      this.flpRegisters.Controls.Add((Control) this.btnSelectNone);
      this.flpRegisters.Dock = DockStyle.Fill;
      this.flpRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpRegisters.Location = new Point(3, 16);
      this.flpRegisters.Name = "flpRegisters";
      this.flpRegisters.Size = new Size(284, 406);
      this.flpRegisters.TabIndex = 17;
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
      this.rcbE8.Caption = "m3 x T1";
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
      this.rcbE8.Size = new Size(74, 21);
      this.rcbE8.TabIndex = 49;
      this.rcbE8.UnitText = "";
      this.rcbE8.UseRegister2 = true;
      this.rcbE9.CalculationRule = "";
      this.rcbE9.CalculationValue = new Decimal(new int[4]);
      this.rcbE9.Caption = "m3 x T2";
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
      this.rcbE9.Size = new Size(74, 21);
      this.rcbE9.TabIndex = 50;
      this.rcbE9.UnitText = "";
      this.rcbE9.UseRegister2 = true;
      this.rcbTA2.CalculationRule = "";
      this.rcbTA2.CalculationValue = new Decimal(new int[4]);
      this.rcbTA2.Caption = "TA 2";
      this.rcbTA2.Checked = false;
      this.rcbTA2.IsRegister = true;
      this.rcbTA2.Location = new Point(3, 246);
      this.rcbTA2.Name = "rcbTA2";
      this.rcbTA2.RegisterCaption1 = "";
      this.rcbTA2.RegisterCaption2 = "";
      this.rcbTA2.RegisterId = 64;
      this.rcbTA2.RegisterName = "TA2";
      this.rcbTA2.RegisterName2 = "";
      this.rcbTA2.RegisterType = ERegisterType.IsDouble;
      this.rcbTA2.Size = new Size(57, 21);
      this.rcbTA2.TabIndex = 47;
      this.rcbTA2.UnitText = "";
      this.rcbTA2.UseRegister2 = true;
      this.rcbTA3.CalculationRule = "";
      this.rcbTA3.CalculationValue = new Decimal(new int[4]);
      this.rcbTA3.Caption = "TA 3";
      this.rcbTA3.Checked = false;
      this.rcbTA3.IsRegister = true;
      this.rcbTA3.Location = new Point(3, 273);
      this.rcbTA3.Name = "rcbTA3";
      this.rcbTA3.RegisterCaption1 = "";
      this.rcbTA3.RegisterCaption2 = "";
      this.rcbTA3.RegisterId = 65;
      this.rcbTA3.RegisterName = "TA3";
      this.rcbTA3.RegisterName2 = "";
      this.rcbTA3.RegisterType = ERegisterType.IsDouble;
      this.rcbTA3.Size = new Size(57, 21);
      this.rcbTA3.TabIndex = 48;
      this.rcbTA3.UnitText = "";
      this.rcbTA3.UseRegister2 = true;
      this.rcbV1.CalculationRule = "";
      this.rcbV1.CalculationValue = new Decimal(new int[4]);
      this.rcbV1.Caption = "V1";
      this.rcbV1.Checked = false;
      this.rcbV1.IsRegister = true;
      this.rcbV1.Location = new Point(3, 300);
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
      this.rcbV2.Location = new Point(3, 327);
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
      this.rcbInA.Caption = "In A";
      this.rcbInA.Checked = false;
      this.rcbInA.IsRegister = true;
      this.rcbInA.Location = new Point(3, 354);
      this.rcbInA.Name = "rcbInA";
      this.rcbInA.RegisterCaption1 = "";
      this.rcbInA.RegisterCaption2 = "";
      this.rcbInA.RegisterId = 84;
      this.rcbInA.RegisterName = "INA";
      this.rcbInA.RegisterName2 = "";
      this.rcbInA.RegisterType = ERegisterType.IsDouble;
      this.rcbInA.Size = new Size(54, 21);
      this.rcbInA.TabIndex = 13;
      this.rcbInA.UnitText = "";
      this.rcbInA.UseRegister2 = true;
      this.rcbInB.CalculationRule = "";
      this.rcbInB.CalculationValue = new Decimal(new int[4]);
      this.rcbInB.Caption = "In B";
      this.rcbInB.Checked = false;
      this.rcbInB.IsRegister = true;
      this.rcbInB.Location = new Point(3, 381);
      this.rcbInB.Name = "rcbInB";
      this.rcbInB.RegisterCaption1 = "";
      this.rcbInB.RegisterCaption2 = "";
      this.rcbInB.RegisterId = 85;
      this.rcbInB.RegisterName = "INB";
      this.rcbInB.RegisterName2 = "";
      this.rcbInB.RegisterType = ERegisterType.IsDouble;
      this.rcbInB.Size = new Size(54, 21);
      this.rcbInB.TabIndex = 14;
      this.rcbInB.UnitText = "";
      this.rcbInB.UseRegister2 = true;
      this.rcbInfo.CalculationRule = "";
      this.rcbInfo.CalculationValue = new Decimal(new int[4]);
      this.rcbInfo.Caption = "Info";
      this.rcbInfo.Checked = false;
      this.rcbInfo.IsRegister = true;
      this.rcbInfo.Location = new Point(157, 3);
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
      this.rcbMaxFlow1DateMdr.CalculationRule = "";
      this.rcbMaxFlow1DateMdr.CalculationValue = new Decimal(new int[4]);
      this.rcbMaxFlow1DateMdr.Caption = "Flow 1 Max. Date";
      this.rcbMaxFlow1DateMdr.Checked = false;
      this.rcbMaxFlow1DateMdr.IsRegister = true;
      this.rcbMaxFlow1DateMdr.Location = new Point(157, 30);
      this.rcbMaxFlow1DateMdr.Name = "rcbMaxFlow1DateMdr";
      this.rcbMaxFlow1DateMdr.RegisterCaption1 = "";
      this.rcbMaxFlow1DateMdr.RegisterCaption2 = "";
      this.rcbMaxFlow1DateMdr.RegisterId = 123;
      this.rcbMaxFlow1DateMdr.RegisterName = "MaxFlow1DateYear";
      this.rcbMaxFlow1DateMdr.RegisterName2 = "";
      this.rcbMaxFlow1DateMdr.RegisterType = ERegisterType.IsDateTime;
      this.rcbMaxFlow1DateMdr.Size = new Size(121, 21);
      this.rcbMaxFlow1DateMdr.TabIndex = 40;
      this.rcbMaxFlow1DateMdr.UnitText = "";
      this.rcbMaxFlow1DateMdr.UseRegister2 = true;
      this.rcbMaxFlow1Year.CalculationRule = "";
      this.rcbMaxFlow1Year.CalculationValue = new Decimal(new int[4]);
      this.rcbMaxFlow1Year.Caption = "Flow 1 Max.";
      this.rcbMaxFlow1Year.Checked = false;
      this.rcbMaxFlow1Year.IsRegister = true;
      this.rcbMaxFlow1Year.Location = new Point(157, 57);
      this.rcbMaxFlow1Year.Name = "rcbMaxFlow1Year";
      this.rcbMaxFlow1Year.RegisterCaption1 = "";
      this.rcbMaxFlow1Year.RegisterCaption2 = "";
      this.rcbMaxFlow1Year.RegisterId = 124;
      this.rcbMaxFlow1Year.RegisterName = "MaxFlow1Year";
      this.rcbMaxFlow1Year.RegisterName2 = "";
      this.rcbMaxFlow1Year.RegisterType = ERegisterType.IsDouble;
      this.rcbMaxFlow1Year.Size = new Size(95, 21);
      this.rcbMaxFlow1Year.TabIndex = 41;
      this.rcbMaxFlow1Year.UnitText = "";
      this.rcbMaxFlow1Year.UseRegister2 = true;
      this.rcbMinFlow1DateYear.CalculationRule = "";
      this.rcbMinFlow1DateYear.CalculationValue = new Decimal(new int[4]);
      this.rcbMinFlow1DateYear.Caption = "Flow 1 Min. Date";
      this.rcbMinFlow1DateYear.Checked = false;
      this.rcbMinFlow1DateYear.IsRegister = true;
      this.rcbMinFlow1DateYear.Location = new Point(157, 84);
      this.rcbMinFlow1DateYear.Name = "rcbMinFlow1DateYear";
      this.rcbMinFlow1DateYear.RegisterCaption1 = "";
      this.rcbMinFlow1DateYear.RegisterCaption2 = "";
      this.rcbMinFlow1DateYear.RegisterId = 125;
      this.rcbMinFlow1DateYear.RegisterName = "MinFlow1DateYear";
      this.rcbMinFlow1DateYear.RegisterName2 = "";
      this.rcbMinFlow1DateYear.RegisterType = ERegisterType.IsDateTime;
      this.rcbMinFlow1DateYear.Size = new Size(118, 21);
      this.rcbMinFlow1DateYear.TabIndex = 42;
      this.rcbMinFlow1DateYear.UnitText = "";
      this.rcbMinFlow1DateYear.UseRegister2 = true;
      this.rcbMinFlow1Year.CalculationRule = "";
      this.rcbMinFlow1Year.CalculationValue = new Decimal(new int[4]);
      this.rcbMinFlow1Year.Caption = "Flow 1 Min.";
      this.rcbMinFlow1Year.Checked = false;
      this.rcbMinFlow1Year.IsRegister = true;
      this.rcbMinFlow1Year.Location = new Point(157, 111);
      this.rcbMinFlow1Year.Name = "rcbMinFlow1Year";
      this.rcbMinFlow1Year.RegisterCaption1 = "";
      this.rcbMinFlow1Year.RegisterCaption2 = "";
      this.rcbMinFlow1Year.RegisterId = 126;
      this.rcbMinFlow1Year.RegisterName = "MinFlow1Year";
      this.rcbMinFlow1Year.RegisterName2 = "";
      this.rcbMinFlow1Year.RegisterType = ERegisterType.IsDouble;
      this.rcbMinFlow1Year.Size = new Size(91, 21);
      this.rcbMinFlow1Year.TabIndex = 39;
      this.rcbMinFlow1Year.UnitText = "";
      this.rcbMinFlow1Year.UseRegister2 = true;
      this.rcbMaxEff1DateYear.CalculationRule = "";
      this.rcbMaxEff1DateYear.CalculationValue = new Decimal(new int[4]);
      this.rcbMaxEff1DateYear.Caption = "Power 1 Max. Date";
      this.rcbMaxEff1DateYear.Checked = false;
      this.rcbMaxEff1DateYear.IsRegister = true;
      this.rcbMaxEff1DateYear.Location = new Point(157, 138);
      this.rcbMaxEff1DateYear.Name = "rcbMaxEff1DateYear";
      this.rcbMaxEff1DateYear.RegisterCaption1 = "";
      this.rcbMaxEff1DateYear.RegisterCaption2 = "";
      this.rcbMaxEff1DateYear.RegisterId = (int) sbyte.MaxValue;
      this.rcbMaxEff1DateYear.RegisterName = "MaxEff1DateYear";
      this.rcbMaxEff1DateYear.RegisterName2 = "";
      this.rcbMaxEff1DateYear.RegisterType = ERegisterType.IsDateTime;
      this.rcbMaxEff1DateYear.Size = new Size(130, 21);
      this.rcbMaxEff1DateYear.TabIndex = 44;
      this.rcbMaxEff1DateYear.UnitText = "";
      this.rcbMaxEff1DateYear.UseRegister2 = true;
      this.rcbMaxEff1Year.CalculationRule = "";
      this.rcbMaxEff1Year.CalculationValue = new Decimal(new int[4]);
      this.rcbMaxEff1Year.Caption = "Power 1 Max.";
      this.rcbMaxEff1Year.Checked = false;
      this.rcbMaxEff1Year.IsRegister = true;
      this.rcbMaxEff1Year.Location = new Point(157, 165);
      this.rcbMaxEff1Year.Name = "rcbMaxEff1Year";
      this.rcbMaxEff1Year.RegisterCaption1 = "";
      this.rcbMaxEff1Year.RegisterCaption2 = "";
      this.rcbMaxEff1Year.RegisterId = 128;
      this.rcbMaxEff1Year.RegisterName = "MaxEff1Year";
      this.rcbMaxEff1Year.RegisterName2 = "";
      this.rcbMaxEff1Year.RegisterType = ERegisterType.IsDouble;
      this.rcbMaxEff1Year.Size = new Size(103, 21);
      this.rcbMaxEff1Year.TabIndex = 45;
      this.rcbMaxEff1Year.UnitText = "";
      this.rcbMaxEff1Year.UseRegister2 = true;
      this.rcbMinEff1DateYear.CalculationRule = "";
      this.rcbMinEff1DateYear.CalculationValue = new Decimal(new int[4]);
      this.rcbMinEff1DateYear.Caption = "Power 1 Min. Date";
      this.rcbMinEff1DateYear.Checked = false;
      this.rcbMinEff1DateYear.IsRegister = true;
      this.rcbMinEff1DateYear.Location = new Point(157, 192);
      this.rcbMinEff1DateYear.Name = "rcbMinEff1DateYear";
      this.rcbMinEff1DateYear.RegisterCaption1 = "";
      this.rcbMinEff1DateYear.RegisterCaption2 = "";
      this.rcbMinEff1DateYear.RegisterId = 129;
      this.rcbMinEff1DateYear.RegisterName = "MinEff1DateYear";
      this.rcbMinEff1DateYear.RegisterName2 = "";
      this.rcbMinEff1DateYear.RegisterType = ERegisterType.IsDateTime;
      this.rcbMinEff1DateYear.Size = new Size(126, 21);
      this.rcbMinEff1DateYear.TabIndex = 46;
      this.rcbMinEff1DateYear.UnitText = "";
      this.rcbMinEff1DateYear.UseRegister2 = true;
      this.rcbMinEff1Year.CalculationRule = "";
      this.rcbMinEff1Year.CalculationValue = new Decimal(new int[4]);
      this.rcbMinEff1Year.Caption = "Power 1 Min.";
      this.rcbMinEff1Year.Checked = false;
      this.rcbMinEff1Year.IsRegister = true;
      this.rcbMinEff1Year.Location = new Point(157, 219);
      this.rcbMinEff1Year.Name = "rcbMinEff1Year";
      this.rcbMinEff1Year.RegisterCaption1 = "";
      this.rcbMinEff1Year.RegisterCaption2 = "";
      this.rcbMinEff1Year.RegisterId = 130;
      this.rcbMinEff1Year.RegisterName = "MinEff1Year";
      this.rcbMinEff1Year.RegisterName2 = "";
      this.rcbMinEff1Year.RegisterType = ERegisterType.IsDouble;
      this.rcbMinEff1Year.Size = new Size(100, 21);
      this.rcbMinEff1Year.TabIndex = 43;
      this.rcbMinEff1Year.UnitText = "";
      this.rcbMinEff1Year.UseRegister2 = true;
      this.btnSelectAll.Location = new Point(157, 246);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new Size(75, 23);
      this.btnSelectAll.TabIndex = 28;
      this.btnSelectAll.Text = "Select All";
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
      this.btnSelectNone.Location = new Point(157, 275);
      this.btnSelectNone.Name = "btnSelectNone";
      this.btnSelectNone.Size = new Size(75, 23);
      this.btnSelectNone.TabIndex = 27;
      this.btnSelectNone.Text = "Select None";
      this.btnSelectNone.UseVisualStyleBackColor = true;
      this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
      this.grpUsed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.grpUsed.BackColor = Color.Transparent;
      this.grpUsed.Controls.Add((Control) this.rcbUsedE1);
      this.grpUsed.Controls.Add((Control) this.rcbUsedE8);
      this.grpUsed.Controls.Add((Control) this.rcbUsedE9);
      this.grpUsed.Controls.Add((Control) this.rcbUsedV1);
      this.grpUsed.Controls.Add((Control) this.rcbUsedV2);
      this.grpUsed.Location = new Point(503, 12);
      this.grpUsed.Name = "grpUsed";
      this.grpUsed.Size = new Size(226, 155);
      this.grpUsed.TabIndex = 23;
      this.grpUsed.TabStop = false;
      this.grpUsed.Text = "Used per year";
      this.rcbUsedE1.CalculationRule = "";
      this.rcbUsedE1.CalculationValue = new Decimal(new int[4]);
      this.rcbUsedE1.Caption = "Used Heat energy #1 ~ E1";
      this.rcbUsedE1.Checked = false;
      this.rcbUsedE1.IsRegister = false;
      this.rcbUsedE1.Location = new Point(6, 19);
      this.rcbUsedE1.Name = "rcbUsedE1";
      this.rcbUsedE1.RegisterCaption1 = "E1";
      this.rcbUsedE1.RegisterCaption2 = "";
      this.rcbUsedE1.RegisterId = 60;
      this.rcbUsedE1.RegisterName = "E1";
      this.rcbUsedE1.RegisterName2 = "";
      this.rcbUsedE1.RegisterType = ERegisterType.IsDouble;
      this.rcbUsedE1.Size = new Size(167, 21);
      this.rcbUsedE1.TabIndex = 34;
      this.rcbUsedE1.UnitText = "";
      this.rcbUsedE1.UseRegister2 = true;
      this.rcbUsedE8.CalculationRule = "";
      this.rcbUsedE8.CalculationValue = new Decimal(new int[4]);
      this.rcbUsedE8.Caption = "Used m3 x T1";
      this.rcbUsedE8.Checked = false;
      this.rcbUsedE8.IsRegister = false;
      this.rcbUsedE8.Location = new Point(6, 46);
      this.rcbUsedE8.Name = "rcbUsedE8";
      this.rcbUsedE8.RegisterCaption1 = "E8";
      this.rcbUsedE8.RegisterCaption2 = "";
      this.rcbUsedE8.RegisterId = 97;
      this.rcbUsedE8.RegisterName = "E8";
      this.rcbUsedE8.RegisterName2 = "";
      this.rcbUsedE8.RegisterType = ERegisterType.IsDouble;
      this.rcbUsedE8.Size = new Size(104, 21);
      this.rcbUsedE8.TabIndex = 37;
      this.rcbUsedE8.UnitText = "";
      this.rcbUsedE8.UseRegister2 = true;
      this.rcbUsedE9.CalculationRule = "";
      this.rcbUsedE9.CalculationValue = new Decimal(new int[4]);
      this.rcbUsedE9.Caption = "Used m3 x T2";
      this.rcbUsedE9.Checked = false;
      this.rcbUsedE9.IsRegister = false;
      this.rcbUsedE9.Location = new Point(6, 73);
      this.rcbUsedE9.Name = "rcbUsedE9";
      this.rcbUsedE9.RegisterCaption1 = "E9";
      this.rcbUsedE9.RegisterCaption2 = "";
      this.rcbUsedE9.RegisterId = 110;
      this.rcbUsedE9.RegisterName = "E9";
      this.rcbUsedE9.RegisterName2 = "";
      this.rcbUsedE9.RegisterType = ERegisterType.IsDouble;
      this.rcbUsedE9.Size = new Size(104, 21);
      this.rcbUsedE9.TabIndex = 38;
      this.rcbUsedE9.UnitText = "";
      this.rcbUsedE9.UseRegister2 = true;
      this.rcbUsedV1.CalculationRule = "";
      this.rcbUsedV1.CalculationValue = new Decimal(new int[4]);
      this.rcbUsedV1.Caption = "Used Volume 1";
      this.rcbUsedV1.Checked = false;
      this.rcbUsedV1.IsRegister = false;
      this.rcbUsedV1.Location = new Point(6, 100);
      this.rcbUsedV1.Name = "rcbUsedV1";
      this.rcbUsedV1.RegisterCaption1 = "V1";
      this.rcbUsedV1.RegisterCaption2 = "";
      this.rcbUsedV1.RegisterId = 68;
      this.rcbUsedV1.RegisterName = "V1";
      this.rcbUsedV1.RegisterName2 = "";
      this.rcbUsedV1.RegisterType = ERegisterType.IsDouble;
      this.rcbUsedV1.Size = new Size(111, 21);
      this.rcbUsedV1.TabIndex = 35;
      this.rcbUsedV1.UnitText = "";
      this.rcbUsedV1.UseRegister2 = true;
      this.rcbUsedV2.CalculationRule = "";
      this.rcbUsedV2.CalculationValue = new Decimal(new int[4]);
      this.rcbUsedV2.Caption = "Used Volume 2";
      this.rcbUsedV2.Checked = false;
      this.rcbUsedV2.IsRegister = false;
      this.rcbUsedV2.Location = new Point(6, (int) sbyte.MaxValue);
      this.rcbUsedV2.Name = "rcbUsedV2";
      this.rcbUsedV2.RegisterCaption1 = "V2";
      this.rcbUsedV2.RegisterCaption2 = "";
      this.rcbUsedV2.RegisterId = 69;
      this.rcbUsedV2.RegisterName = "V2";
      this.rcbUsedV2.RegisterName2 = "";
      this.rcbUsedV2.RegisterType = ERegisterType.IsDouble;
      this.rcbUsedV2.Size = new Size(111, 21);
      this.rcbUsedV2.TabIndex = 36;
      this.rcbUsedV2.UnitText = "";
      this.rcbUsedV2.UseRegister2 = true;
      this.rdbCalcB.AutoSize = true;
      this.rdbCalcB.Location = new Point(7, 103);
      this.rdbCalcB.Name = "rdbCalcB";
      this.rdbCalcB.Size = new Size(14, 13);
      this.rdbCalcB.TabIndex = 23;
      this.rdbCalcB.UseVisualStyleBackColor = true;
      this.rdbCalcB.CheckedChanged += new EventHandler(this.rdbCalcA_CheckedChanged);
      this.rdbCalcA.AutoSize = true;
      this.rdbCalcA.Checked = true;
      this.rdbCalcA.Location = new Point(7, 77);
      this.rdbCalcA.Name = "rdbCalcA";
      this.rdbCalcA.Size = new Size(14, 13);
      this.rdbCalcA.TabIndex = 22;
      this.rdbCalcA.TabStop = true;
      this.rdbCalcA.UseVisualStyleBackColor = true;
      this.rdbCalcA.CheckedChanged += new EventHandler(this.rdbCalcA_CheckedChanged);
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
      this.nudCalc.TabIndex = 21;
      this.nudCalc.TextAlign = HorizontalAlignment.Right;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(734, 449);
      this.Controls.Add((Control) this.grpUsed);
      this.Controls.Add((Control) this.grpCalculatedRegisters);
      this.Controls.Add((Control) this.grpCalculate);
      this.Controls.Add((Control) this.grpGraphs);
      this.Controls.Add((Control) this.grpReadLog);
      this.Controls.Add((Control) this.grpRegisters);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(742, 483);
      this.Name = "FrmYearLogger";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Yearly Logger";
      this.Load += new EventHandler(this.frmYearLogger_Load);
      this.grpCalculatedRegisters.ResumeLayout(false);
      this.grpCalculate.ResumeLayout(false);
      this.grpCalculate.PerformLayout();
      this.grpGraphs.ResumeLayout(false);
      this.grpReadLog.ResumeLayout(false);
      this.grpReadLog.PerformLayout();
      this.grpRegisters.ResumeLayout(false);
      this.flpRegisters.ResumeLayout(false);
      this.grpUsed.ResumeLayout(false);
      this.nudCalc.EndInit();
      this.ResumeLayout(false);
    }
  }
}
