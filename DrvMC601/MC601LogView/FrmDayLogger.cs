// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmDayLogger
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
  public class FrmDayLogger : Form
  {
    private string m_DataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\kamstrup\\METERTOOLLogViewer601";
    private Functions m_mc601Functions = new Functions();
    private DayLogDataSet m_DLDS = new DayLogDataSet();
    private clsCalculatedRegisters m_userRegisters = new clsCalculatedRegisters();
    private DayLogDataSet.RegisterInUseRow m_RegisterInUseRow;
    private DayLogDataSet.RegisterUnitRow m_RegisterUnitRow;
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
    private UCRegisterCheckBox rcbM1;
    private UCRegisterCheckBox rcbM2;
    private UCRegisterCheckBox rcbInfo;
    private Button btnSelectAll;
    private Button btnSelectNone;
    private UCRegisterCheckBox rcbE8;
    private UCRegisterCheckBox rcbE9;
    private UCRegisterCheckBox rcbT1Avr;
    private UCRegisterCheckBox rcbT2Avr;
    private UCRegisterCheckBox rcbT3Avr;
    private UCRegisterCheckBox rcbP1Avr;
    private UCRegisterCheckBox rcbP2Avr;
    private UCRegisterCheckBox rcbCalc2;
    private UCRegisterCheckBox rcbCalc1;
    private UCRegisterCheckBox rcbCalc0;
    private GroupBox grpUsed;
    private RadioButton rdbCalcA;
    private NumericUpDown nudCalc;
    private RadioButton rdbCalcB;

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

    public FrmDayLogger()
    {
      this.InitializeComponent();
      this.m_userRegisters.Load("DayLoggerUR");
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

    private void frmDayLogger_Load(object sender, EventArgs e)
    {
      this.cbxFrom.DataSource =  FrmDayLogger.MakeListOfDays((ushort) 460);
      this.cbxFrom.DisplayMember = "Text";
      this.cbxFrom.ValueMember = "Value";
      this.cbxTo.DataSource =  FrmDayLogger.MakeListOfDays((ushort) 460);
      this.cbxTo.DisplayMember = "Text";
      this.cbxTo.ValueMember = "Value";
    }

    private static DataTable MakeListOfDays(ushort numberOfDays)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Value", typeof (ushort));
      dataTable.Columns.Add("Text", typeof (string));
      dataTable.NewRow();
      DataRow row1 = dataTable.NewRow();
      row1["Value"] =  1;
      row1["Text"] =  "Newest Date";
      dataTable.Rows.Add(row1);
      for (ushort index = (ushort) 2; (int) index < (int) numberOfDays; ++index)
      {
        DataRow row2 = dataTable.NewRow();
        row2["Value"] =  index;
        row2["Text"] =  ("-" + ((int) index - 1).ToString() + " days.");
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
          dataTable.Columns.Add(registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]", typeof (double));
        }
      }
      if (this.rcbCalc0.Checked && this.rcbCalc0.Enabled)
      {
        this.rcbCalc0.UnitText = ClsUtils.UnitsForRegisters(Convert.ToByte(this.m_RegisterUnitRow[this.rcbCalc0.RegisterName]));
        dataTable.Columns.Add(this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]", typeof (double));
      }
      this.AddCalculatedColumnForRCB(dataTable, this.rcbCalc1);
      this.AddCalculatedColumnForRCB(dataTable, this.rcbCalc2);
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
        if (rcb != null)
          this.AddCalculatedColumnForRCB(dataTable, rcb);
      }
      DayLogDataSet.RegisterRow registerRow = (DayLogDataSet.RegisterRow) null;
      foreach (DayLogDataSet.RegisterRow row in this.m_DLDS.Register)
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
          if (registerRow != null)
          {
            Decimal num1 = Convert.ToDecimal(registerRow[this.rcbCalc0.RegisterName]);
            Decimal num2 = Convert.ToDecimal(row[this.rcbCalc0.RegisterName]);
            dataRow[this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]"] = num2 == new Decimal(0) || num1 == new Decimal(0) ?  0 :  (num1 - num2);
          }
          else
            dataRow[this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]"] =  0;
          registerRow = row;
        }
        FrmDayLogger.CalculateCalculatedRowForRCB(dataRow, this.rcbCalc1, row);
        FrmDayLogger.CalculateCalculatedRowForRCB(dataRow, this.rcbCalc2, row);
        foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
        {
          UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
          if (rcb != null)
            FrmDayLogger.CalculateCalculatedRowForRCB(dataRow, rcb, row);
        }
        dataTable.Rows.InsertAt(dataRow, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_DLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_DLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private static void CalculateCalculatedRowForRCB(DataRow dr, UCRegisterCheckBox rcb, DayLogDataSet.RegisterRow row)
    {
      if (!rcb.Checked || !rcb.Enabled)
        return;
      if (rcb.UseRegister2)
      {
        Decimal num = FrmDayLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), Convert.ToDecimal(row[rcb.RegisterName2]), rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.RegisterName2] =  num;
      }
      else
      {
        Decimal num = FrmDayLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), rcb.CalculationValue, rcb.CalculationRule);
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
        string str = rcb.RegisterCaption1 + " [" + ClsUtils.UnitsForRegisters(nUnit) + "] " + rcb.CalculationRule + " " + rcb.RegisterCaption2 + " " + rcb.CalculationValue.ToString();
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
        if (this.m_DLDS.Register.Rows.Count > 0)
        {
          this.Text = "Daily Log | Serial No " + this.m_DLDS.CustomerNo.GetCustomerNo() + " | Not saved.";
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
      this.rcbCalc0.Enabled = (bool) this.m_RegisterInUseRow[this.rcbCalc0.RegisterName] && this.m_DLDS.Register.Count > 0;
      this.rcbCalc1.Enabled = (bool) this.m_RegisterInUseRow[this.rcbCalc1.RegisterName] && (bool) this.m_RegisterInUseRow[this.rcbCalc1.RegisterName2] && this.m_DLDS.Register.Count > 0;
      this.rcbCalc2.Enabled = (bool) this.m_RegisterInUseRow[this.rcbCalc2.RegisterName] && (bool) this.m_RegisterInUseRow[this.rcbCalc2.RegisterName2] && this.m_DLDS.Register.Count > 0;
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
        {
          if (registerCheckBox.UseRegister2)
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName2] && this.m_DLDS.Register.Count > 0;
            registerCheckBox.Enabled = flag;
          }
          else
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && this.m_DLDS.Register.Count > 0;
            registerCheckBox.Enabled = flag;
          }
        }
      }
    }

    private void ReadData(ushort fromDay, ushort toDay)
    {
      this.bConnectionLost = false;
      this.m_DLDS.Register.Clear();
      this.m_DLDS.RegisterUnit.Clear();
      this.m_DLDS.RegisterInUse.Clear();
      this.m_DLDS.CustomerNo.Clear();
      this.m_RegisterUnitRow = this.m_DLDS.RegisterUnit.NewRegisterUnitRow();
      this.m_DLDS.RegisterUnit.AddRegisterUnitRow(this.m_RegisterUnitRow);
      this.m_RegisterInUseRow = this.m_DLDS.RegisterInUse.NewRegisterInUseRow();
      this.m_DLDS.RegisterInUse.AddRegisterInUseRow(this.m_RegisterInUseRow);
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
      this.m_RegisterInUseRow.INA = this.rcbInA.Checked;
      this.m_RegisterInUseRow.INB = this.rcbInB.Checked;
      this.m_RegisterInUseRow.M1 = this.rcbM1.Checked;
      this.m_RegisterInUseRow.M2 = this.rcbM2.Checked;
      this.m_RegisterInUseRow.T1Avr = this.rcbT1Avr.Checked;
      this.m_RegisterInUseRow.T2Avr = this.rcbT2Avr.Checked;
      this.m_RegisterInUseRow.T3Avr = this.rcbT3Avr.Checked;
      this.m_RegisterInUseRow.P1Avr = this.rcbP1Avr.Checked;
      this.m_RegisterInUseRow.P2Avr = this.rcbP2Avr.Checked;
      this.m_RegisterInUseRow.INFO = this.rcbInfo.Checked;
      this.Progress = 0;
      this.ReadCustomerNo(ref this.bConnectionLost);
      this.ProgressMax = (int) toDay - (int) fromDay + 1;
      ushort lastFoundDay = (ushort) 0;
      bool bDone = false;
      while (!bDone && !this.bConnectionLost)
      {
        this.ReadSetOfDays(fromDay, toDay, ref lastFoundDay, ref bDone);
        if ((int) fromDay == (int) lastFoundDay || (int) toDay == (int) lastFoundDay)
        {
          bDone = true;
        }
        else
        {
          fromDay = lastFoundDay;
          ++fromDay;
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
          this.m_DLDS.Register.Clear();
          this.lblRecords.Text = this.m_DLDS.Register.Count.ToString();
          return;
        }
      }
      this.lblRecords.Text = this.m_DLDS.Register.Count.ToString();
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
          this.m_DLDS.CustomerNo.SetCustomerNo(Convert.ToString((double) arrValues[0] + (double) arrValues[1]));
        else
          bConnectionLost = true;
      }
      catch
      {
        bConnectionLost = true;
      }
    }

    private void ReadSetOfDays(ushort fromDay, ushort endDay, ref ushort lastFoundDay, ref bool bDone)
    {
      byte NumberOfRecords = (byte) Math.Min(16, (int) endDay - (int) fromDay + 1);
      ArrayList arrValues = new ArrayList();
      ArrayList rowArray = new ArrayList();
      string ErrorMessage = "";
      byte UnitId = (byte) 0;
      bool histDayData = this.m_mc601Functions.GetHistDayData((byte) 63, (ushort) 1003, fromDay, NumberOfRecords, arrValues, out UnitId, out ErrorMessage);
      byte num1 = (byte) arrValues.Count;
      lastFoundDay = fromDay;
      lastFoundDay += (ushort) (arrValues.Count - 1);
      if (histDayData)
      {
        for (int index = 0; index < (int) num1; ++index)
        {
          double num2 = (double) arrValues[index];
          if (num2 != 0.0)
          {
            DateTime dateTime = new DateTime(2000 + Convert.ToInt32(num2) / 10000, Convert.ToInt32(num2) % 10000 / 100, Convert.ToInt32(num2) % 100);
            DayLogDataSet.RegisterRow row = this.m_DLDS.Register.NewRegisterRow();
            row["Id"] =  ((int) fromDay + index);
            row["Date"] =  dateTime;
            this.m_DLDS.Register.AddRegisterRow(row);
            rowArray.Add( row);
          }
          else
          {
            lastFoundDay = fromDay;
            lastFoundDay += (ushort) index;
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
            this.ReadDayRegister(rcb, rowArray, fromDay, (ushort) num1);
        }
      }
      else
      {
        int num3 = (int) MessageBox.Show("There is no data for the selected days.");
      }
    }

    private void ReadDayRegister(UCRegisterCheckBox rcb, ArrayList rowArray, ushort fromDay, ushort numberOfDays)
    {
      byte UnitId = (byte) 0;
      string ErrorMessage = "";
      ArrayList arrValues = new ArrayList();
      if (this.m_mc601Functions.GetHistDayData((byte) 63, (ushort) rcb.RegisterId, fromDay, (byte) numberOfDays, arrValues, out UnitId, out ErrorMessage))
      {
        if (arrValues.Count <= 0)
          return;
        this.m_RegisterUnitRow[rcb.RegisterName] =  UnitId;
        for (byte index = (byte) 0; (int) index < arrValues.Count; ++index)
          ((DataRow) rowArray[(int) index])[rcb.RegisterName] = arrValues[(int) index];
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
      this.m_DLDS.Register.Clear();
      this.m_DLDS.RegisterInUse.Clear();
      this.m_DLDS.RegisterUnit.Clear();
      this.cbxRegister1.Items.Clear();
      this.cbxRegister2.Items.Clear();
      this.lblRecords.Text = this.m_DLDS.Register.Count.ToString();
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = true;
      }
      this.rcbCalc0.Enabled = false;
      this.rcbCalc1.Enabled = false;
      this.rcbCalc2.Enabled = false;
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
      this.Text = "Daily Log | Serial No " + this.m_DLDS.CustomerNo.GetCustomerNo() + "| Empty.";
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
      openFileDialog.Filter = "MC601 Daily Log (*.MC601DayLog)|*.MC601DayLog|All files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        int num = (int) this.m_DLDS.ReadXml(openFileDialog.FileName);
        this.m_RegisterInUseRow = (DayLogDataSet.RegisterInUseRow) this.m_DLDS.RegisterInUse.Rows[0];
        this.m_RegisterUnitRow = (DayLogDataSet.RegisterUnitRow) this.m_DLDS.RegisterUnit.Rows[0];
        this.lblRecords.Text = this.m_DLDS.Register.Count.ToString();
        this.btnRead.Text = "Start";
        this.SetupCheckableRCBs();
        this.btnSave.Enabled = false;
        this.btnCalculate.Enabled = true;
        this.btnAddToRegs.Enabled = true;
        this.btnSelectedRegisters.Enabled = true;
        this.Progress = 0;
        this.Text = "Daily Log | Serial No " + this.m_DLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(openFileDialog.FileName);
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
      if (this.m_DLDS.Register.Rows.Count < 1)
      {
        int num1 = (int) MessageBox.Show("The Monthly Log is empty.", "No data to save!");
      }
      else
      {
        try
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
            Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
          saveFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
          saveFileDialog.Filter = "MC601 Daily Log (*.MC601DayLog)|*.MC601DayLog|All files (*.*)|*.*";
          saveFileDialog.FilterIndex = 1;
          saveFileDialog.RestoreDirectory = true;
          if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
          if (saveFileDialog.FileName.IndexOf(".MC601DayLog") < 0)
            saveFileDialog.FileName = saveFileDialog.FileName + ".MC601DayLog";
          this.m_DLDS.WriteXml(saveFileDialog.FileName);
          this.btnSave.Enabled = false;
          this.Text = "Daily Log | Serial No " + this.m_DLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(saveFileDialog.FileName);
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Failed to save the the Daily Log:" + ex.Message);
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
      foreach (DayLogDataSet.RegisterRow registerRow in (InternalDataCollectionBase) this.m_DLDS.Register.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] =  registerRow.Date;
        Decimal register1 = Convert.ToDecimal(registerRow[registerName1]);
        Decimal register2 = Convert.ToDecimal(registerRow[registerName2]);
        row[columnName] =  FrmDayLogger.DoCalculation(register1, register2, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_DLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_DLDS.CustomerNo.GetCustomerNo());
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
      foreach (DayLogDataSet.RegisterRow registerRow in (InternalDataCollectionBase) this.m_DLDS.Register.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] =  registerRow.Date;
        Decimal register1 = Convert.ToDecimal(registerRow[registerName]);
        row[columnName] =  FrmDayLogger.DoCalculation(register1, calcValue, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_DLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_DLDS.CustomerNo.GetCustomerNo());
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
        ur.UseRegister2 = true;
        ur.CalculationRule = this.cbxCalcRule.Text;
        if (this.EksistereRCB(ur))
        {
          int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          this.m_userRegisters.ListOfCalculatedRegisters.Add( ur);
          this.m_userRegisters.Save("DayLoggerUR");
          this.addUserRegisterToForm(ur);
        }
      }
      else
      {
        if (this.cbxRegister1.SelectedItem == null || this.cbxCalcRule.SelectedItem == null)
          return;
        clsCalculatedRegister ur = new clsCalculatedRegister();
        CbxItem cbxItem = (CbxItem) this.cbxRegister1.SelectedItem;
        ur.RegisterName_1 = cbxItem.Register.RegisterName;
        ur.RegisterCaption_1 = cbxItem.Register.Caption;
        ur.UseRegister2 = false;
        ur.CalculationValue = this.nudCalc.Value;
        ur.CalculationRule = this.cbxCalcRule.Text;
        if (this.EksistereRCB(ur))
        {
          int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          this.m_userRegisters.ListOfCalculatedRegisters.Add( ur);
          this.m_userRegisters.Save("DayLoggerUR");
          this.addUserRegisterToForm(ur);
        }
      }
    }

    private bool EksistereRCB(clsCalculatedRegister ur)
    {
      if (ur.UseRegister2)
      {
        if (this.rcbCalc1.RegisterName == ur.RegisterName_1 && this.rcbCalc1.RegisterName2 == ur.RegisterName_2 && this.rcbCalc1.CalculationRule == ur.CalculationRule || this.rcbCalc2.RegisterName == ur.RegisterName_1 && this.rcbCalc2.RegisterName2 == ur.RegisterName_2 && this.rcbCalc2.CalculationRule == ur.CalculationRule)
          return true;
        foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
        {
          clsCalculatedRegister calculatedRegister = obj as clsCalculatedRegister;
          if (calculatedRegister != null && calculatedRegister.RegisterName_1 == ur.RegisterName_1 && (calculatedRegister.RegisterName_2 == ur.RegisterName_2 && calculatedRegister.CalculationRule == ur.CalculationRule))
            return true;
        }
      }
      else
      {
        foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
        {
          clsCalculatedRegister calculatedRegister = obj as clsCalculatedRegister;
          if (calculatedRegister != null && !calculatedRegister.UseRegister2 && (calculatedRegister.RegisterName_1 == ur.RegisterName_1 && calculatedRegister.CalculationValue == ur.CalculationValue) && calculatedRegister.CalculationRule == ur.CalculationRule)
            return true;
        }
      }
      return false;
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
      this.m_userRegisters.Save("DayLoggerUR");
    }

    private void btnRemoveAllCalculated_Click(object sender, EventArgs e)
    {
      this.m_userRegisters.ListOfCalculatedRegisters.Clear();
      this.m_userRegisters.Save("DayLoggerUR");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmDayLogger));
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
      this.btnSelectAll = new Button();
      this.btnSelectNone = new Button();
      this.grpUsed = new GroupBox();
      this.nudCalc = new NumericUpDown();
      this.rdbCalcA = new RadioButton();
      this.rdbCalcB = new RadioButton();
      this.rcbCalc0 = new UCRegisterCheckBox();
      this.rcbCalc2 = new UCRegisterCheckBox();
      this.rcbCalc1 = new UCRegisterCheckBox();
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
      this.rcbT1Avr = new UCRegisterCheckBox();
      this.rcbT2Avr = new UCRegisterCheckBox();
      this.rcbT3Avr = new UCRegisterCheckBox();
      this.rcbP1Avr = new UCRegisterCheckBox();
      this.rcbP2Avr = new UCRegisterCheckBox();
      this.rcbInfo = new UCRegisterCheckBox();
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
      this.grpCalculatedRegisters.Controls.Add((Control) this.rcbCalc2);
      this.grpCalculatedRegisters.Controls.Add((Control) this.rcbCalc1);
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveAllCalculated);
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveCalculatedRegister);
      this.grpCalculatedRegisters.Controls.Add((Control) this.flpCalculatedRegisters);
      this.grpCalculatedRegisters.Location = new Point(500, 60);
      this.grpCalculatedRegisters.Name = "grpCalculatedRegisters";
      this.grpCalculatedRegisters.Size = new Size(262, 380);
      this.grpCalculatedRegisters.TabIndex = 21;
      this.grpCalculatedRegisters.TabStop = false;
      this.grpCalculatedRegisters.Text = "Calculated Registers";
      this.btnRemoveAllCalculated.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnRemoveAllCalculated.Location = new Point(126, 350);
      this.btnRemoveAllCalculated.Name = "btnRemoveAllCalculated";
      this.btnRemoveAllCalculated.Size = new Size(130, 23);
      this.btnRemoveAllCalculated.TabIndex = 30;
      this.btnRemoveAllCalculated.Text = "Remove All";
      this.btnRemoveAllCalculated.UseVisualStyleBackColor = true;
      this.btnRemoveAllCalculated.Click += new EventHandler(this.btnRemoveAllCalculated_Click);
      this.btnRemoveCalculatedRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRemoveCalculatedRegister.Location = new Point(6, 350);
      this.btnRemoveCalculatedRegister.Name = "btnRemoveCalculatedRegister";
      this.btnRemoveCalculatedRegister.Size = new Size(108, 23);
      this.btnRemoveCalculatedRegister.TabIndex = 29;
      this.btnRemoveCalculatedRegister.Text = "Remove Selected";
      this.btnRemoveCalculatedRegister.UseVisualStyleBackColor = true;
      this.btnRemoveCalculatedRegister.Click += new EventHandler(this.btnRemoveCalculatedRegister_Click);
      this.flpCalculatedRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.flpCalculatedRegisters.AutoScroll = true;
      this.flpCalculatedRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpCalculatedRegisters.Location = new Point(3, 69);
      this.flpCalculatedRegisters.Name = "flpCalculatedRegisters";
      this.flpCalculatedRegisters.Size = new Size(256, 277);
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
      this.grpCalculate.Size = new Size(187, 160);
      this.grpCalculate.TabIndex = 20;
      this.grpCalculate.TabStop = false;
      this.grpCalculate.Text = "Calculate";
      this.btnAddToRegs.Enabled = false;
      this.btnAddToRegs.Location = new Point(106, 128);
      this.btnAddToRegs.Name = "btnAddToRegs";
      this.btnAddToRegs.Size = new Size(75, 23);
      this.btnAddToRegs.TabIndex = 17;
      this.btnAddToRegs.Text = "Add to Regs. ";
      this.btnAddToRegs.UseVisualStyleBackColor = true;
      this.btnAddToRegs.Click += new EventHandler(this.btnAddToRegs_Click);
      this.btnCalculate.Enabled = false;
      this.btnCalculate.Location = new Point(7, 128);
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
      this.grpGraphs.Location = new Point(11, 339);
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
      this.grpReadLog.Text = "Daily Log";
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
      this.grpRegisters.Size = new Size(290, 428);
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
      this.flpRegisters.Controls.Add((Control) this.rcbV1);
      this.flpRegisters.Controls.Add((Control) this.rcbV2);
      this.flpRegisters.Controls.Add((Control) this.rcbInA);
      this.flpRegisters.Controls.Add((Control) this.rcbInB);
      this.flpRegisters.Controls.Add((Control) this.rcbM1);
      this.flpRegisters.Controls.Add((Control) this.rcbM2);
      this.flpRegisters.Controls.Add((Control) this.rcbT1Avr);
      this.flpRegisters.Controls.Add((Control) this.rcbT2Avr);
      this.flpRegisters.Controls.Add((Control) this.rcbT3Avr);
      this.flpRegisters.Controls.Add((Control) this.rcbP1Avr);
      this.flpRegisters.Controls.Add((Control) this.rcbP2Avr);
      this.flpRegisters.Controls.Add((Control) this.rcbInfo);
      this.flpRegisters.Controls.Add((Control) this.btnSelectAll);
      this.flpRegisters.Controls.Add((Control) this.btnSelectNone);
      this.flpRegisters.Dock = DockStyle.Fill;
      this.flpRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpRegisters.Location = new Point(3, 16);
      this.flpRegisters.Name = "flpRegisters";
      this.flpRegisters.Size = new Size(284, 409);
      this.flpRegisters.TabIndex = 17;
      this.btnSelectAll.Location = new Point(157, 165);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new Size(75, 23);
      this.btnSelectAll.TabIndex = 28;
      this.btnSelectAll.Text = "Select All";
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
      this.btnSelectNone.Location = new Point(157, 194);
      this.btnSelectNone.Name = "btnSelectNone";
      this.btnSelectNone.Size = new Size(75, 23);
      this.btnSelectNone.TabIndex = 27;
      this.btnSelectNone.Text = "Select None";
      this.btnSelectNone.UseVisualStyleBackColor = true;
      this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
      this.grpUsed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.grpUsed.BackColor = Color.Transparent;
      this.grpUsed.Controls.Add((Control) this.rcbCalc0);
      this.grpUsed.Location = new Point(500, 12);
      this.grpUsed.Name = "grpUsed";
      this.grpUsed.Size = new Size(262, 42);
      this.grpUsed.TabIndex = 25;
      this.grpUsed.TabStop = false;
      this.grpUsed.Text = "Change per day";
      this.nudCalc.DecimalPlaces = 3;
      this.nudCalc.Enabled = false;
      this.nudCalc.Location = new Point(45, 102);
      NumericUpDown numericUpDown = this.nudCalc;
      int[] bits = new int[4];
      bits[0] = 100000;
      Decimal num = new Decimal(bits);
      numericUpDown.Maximum = num;
      this.nudCalc.Name = "nudCalc";
      this.nudCalc.Size = new Size(136, 20);
      this.nudCalc.TabIndex = 18;
      this.nudCalc.TextAlign = HorizontalAlignment.Right;
      this.rdbCalcA.AutoSize = true;
      this.rdbCalcA.Checked = true;
      this.rdbCalcA.Location = new Point(7, 77);
      this.rdbCalcA.Name = "rdbCalcA";
      this.rdbCalcA.Size = new Size(14, 13);
      this.rdbCalcA.TabIndex = 19;
      this.rdbCalcA.TabStop = true;
      this.rdbCalcA.UseVisualStyleBackColor = true;
      this.rdbCalcA.CheckedChanged += new EventHandler(this.rdbCalcA_CheckedChanged);
      this.rdbCalcB.AutoSize = true;
      this.rdbCalcB.Location = new Point(7, 104);
      this.rdbCalcB.Name = "rdbCalcB";
      this.rdbCalcB.Size = new Size(14, 13);
      this.rdbCalcB.TabIndex = 20;
      this.rdbCalcB.UseVisualStyleBackColor = true;
      this.rdbCalcB.CheckedChanged += new EventHandler(this.rdbCalcA_CheckedChanged);
      this.rcbCalc0.CalculationRule = "";
      this.rcbCalc0.Caption = "Used Heat energy #1 ~ E1";
      this.rcbCalc0.Checked = false;
      this.rcbCalc0.IsRegister = false;
      this.rcbCalc0.Location = new Point(10, 16);
      this.rcbCalc0.Name = "rcbCalc0";
      this.rcbCalc0.RegisterCaption1 = "E1";
      this.rcbCalc0.RegisterCaption2 = "";
      this.rcbCalc0.RegisterId = 60;
      this.rcbCalc0.RegisterName = "E1";
      this.rcbCalc0.RegisterName2 = "";
      this.rcbCalc0.RegisterType = ERegisterType.IsDouble;
      this.rcbCalc0.Size = new Size(167, 21);
      this.rcbCalc0.TabIndex = 31;
      this.rcbCalc0.UnitText = "";
      this.rcbCalc2.CalculationRule = "-";
      this.rcbCalc2.Caption = "M1-M2";
      this.rcbCalc2.Checked = false;
      this.rcbCalc2.IsRegister = false;
      this.rcbCalc2.Location = new Point(6, 19);
      this.rcbCalc2.Name = "rcbCalc2";
      this.rcbCalc2.RegisterCaption1 = "M1";
      this.rcbCalc2.RegisterCaption2 = "M2";
      this.rcbCalc2.RegisterId = 60;
      this.rcbCalc2.RegisterName = "M1";
      this.rcbCalc2.RegisterName2 = "M2";
      this.rcbCalc2.RegisterType = ERegisterType.IsDouble;
      this.rcbCalc2.Size = new Size(69, 21);
      this.rcbCalc2.TabIndex = 7;
      this.rcbCalc2.UnitText = "";
      this.rcbCalc1.CalculationRule = "-";
      this.rcbCalc1.Caption = "T1 Avr. - T2 Avr.";
      this.rcbCalc1.Checked = false;
      this.rcbCalc1.IsRegister = false;
      this.rcbCalc1.Location = new Point(6, 46);
      this.rcbCalc1.Name = "rcbCalc1";
      this.rcbCalc1.RegisterCaption1 = "T1 Avg";
      this.rcbCalc1.RegisterCaption2 = "T2 Avg";
      this.rcbCalc1.RegisterId = 60;
      this.rcbCalc1.RegisterName = "T1Avr";
      this.rcbCalc1.RegisterName2 = "T2Avr";
      this.rcbCalc1.RegisterType = ERegisterType.IsDouble;
      this.rcbCalc1.Size = new Size(116, 21);
      this.rcbCalc1.TabIndex = 8;
      this.rcbCalc1.UnitText = "";
      this.rcbE1.CalculationRule = "";
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
      this.rcbE7.CalculationRule = "";
      this.rcbE7.Caption = "Heat energi #2 ~ E7";
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
      this.rcbE7.Size = new Size(134, 21);
      this.rcbE7.TabIndex = 6;
      this.rcbE7.UnitText = "";
      this.rcbE3.CalculationRule = "";
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
      this.rcbE4.CalculationRule = "";
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
      this.rcbE5.CalculationRule = "";
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
      this.rcbE6.CalculationRule = "";
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
      this.rcbE2.CalculationRule = "";
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
      this.rcbE8.CalculationRule = "";
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
      this.rcbE8.TabIndex = 31;
      this.rcbE8.UnitText = "";
      this.rcbE9.CalculationRule = "";
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
      this.rcbE9.TabIndex = 32;
      this.rcbE9.UnitText = "";
      this.rcbV1.CalculationRule = "";
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
      this.rcbV2.CalculationRule = "";
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
      this.rcbInA.CalculationRule = "";
      this.rcbInA.Caption = "In A";
      this.rcbInA.Checked = false;
      this.rcbInA.IsRegister = true;
      this.rcbInA.Location = new Point(3, 300);
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
      this.rcbInB.CalculationRule = "";
      this.rcbInB.Caption = "In B";
      this.rcbInB.Checked = false;
      this.rcbInB.IsRegister = true;
      this.rcbInB.Location = new Point(3, 327);
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
      this.rcbM1.CalculationRule = "";
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
      this.rcbM2.CalculationRule = "";
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
      this.rcbT1Avr.CalculationRule = "";
      this.rcbT1Avr.Caption = "T1 Avr.";
      this.rcbT1Avr.Checked = false;
      this.rcbT1Avr.IsRegister = true;
      this.rcbT1Avr.Location = new Point(157, 3);
      this.rcbT1Avr.Name = "rcbT1Avr";
      this.rcbT1Avr.RegisterCaption1 = "";
      this.rcbT1Avr.RegisterCaption2 = "";
      this.rcbT1Avr.RegisterId = 86;
      this.rcbT1Avr.RegisterName = "T1Avr";
      this.rcbT1Avr.RegisterName2 = "";
      this.rcbT1Avr.RegisterType = ERegisterType.IsDouble;
      this.rcbT1Avr.Size = new Size(70, 21);
      this.rcbT1Avr.TabIndex = 35;
      this.rcbT1Avr.UnitText = "";
      this.rcbT2Avr.CalculationRule = "";
      this.rcbT2Avr.Caption = "T2 Avr.";
      this.rcbT2Avr.Checked = false;
      this.rcbT2Avr.IsRegister = true;
      this.rcbT2Avr.Location = new Point(157, 30);
      this.rcbT2Avr.Name = "rcbT2Avr";
      this.rcbT2Avr.RegisterCaption1 = "";
      this.rcbT2Avr.RegisterCaption2 = "";
      this.rcbT2Avr.RegisterId = 87;
      this.rcbT2Avr.RegisterName = "T2Avr";
      this.rcbT2Avr.RegisterName2 = "";
      this.rcbT2Avr.RegisterType = ERegisterType.IsDouble;
      this.rcbT2Avr.Size = new Size(70, 21);
      this.rcbT2Avr.TabIndex = 33;
      this.rcbT2Avr.UnitText = "";
      this.rcbT3Avr.CalculationRule = "";
      this.rcbT3Avr.Caption = "T3 Avr.";
      this.rcbT3Avr.Checked = false;
      this.rcbT3Avr.IsRegister = true;
      this.rcbT3Avr.Location = new Point(157, 57);
      this.rcbT3Avr.Name = "rcbT3Avr";
      this.rcbT3Avr.RegisterCaption1 = "";
      this.rcbT3Avr.RegisterCaption2 = "";
      this.rcbT3Avr.RegisterId = 88;
      this.rcbT3Avr.RegisterName = "T3Avr";
      this.rcbT3Avr.RegisterName2 = "";
      this.rcbT3Avr.RegisterType = ERegisterType.IsDouble;
      this.rcbT3Avr.Size = new Size(70, 21);
      this.rcbT3Avr.TabIndex = 34;
      this.rcbT3Avr.UnitText = "";
      this.rcbP1Avr.CalculationRule = "";
      this.rcbP1Avr.Caption = "P1 Avr.";
      this.rcbP1Avr.Checked = false;
      this.rcbP1Avr.IsRegister = true;
      this.rcbP1Avr.Location = new Point(157, 84);
      this.rcbP1Avr.Name = "rcbP1Avr";
      this.rcbP1Avr.RegisterCaption1 = "";
      this.rcbP1Avr.RegisterCaption2 = "";
      this.rcbP1Avr.RegisterId = 91;
      this.rcbP1Avr.RegisterName = "P1Avr";
      this.rcbP1Avr.RegisterName2 = "";
      this.rcbP1Avr.RegisterType = ERegisterType.IsDouble;
      this.rcbP1Avr.Size = new Size(71, 21);
      this.rcbP1Avr.TabIndex = 37;
      this.rcbP1Avr.UnitText = "";
      this.rcbP2Avr.CalculationRule = "";
      this.rcbP2Avr.Caption = "P2 Avr.";
      this.rcbP2Avr.Checked = false;
      this.rcbP2Avr.IsRegister = true;
      this.rcbP2Avr.Location = new Point(157, 111);
      this.rcbP2Avr.Name = "rcbP2Avr";
      this.rcbP2Avr.RegisterCaption1 = "";
      this.rcbP2Avr.RegisterCaption2 = "";
      this.rcbP2Avr.RegisterId = 92;
      this.rcbP2Avr.RegisterName = "P2Avr";
      this.rcbP2Avr.RegisterName2 = "";
      this.rcbP2Avr.RegisterType = ERegisterType.IsDouble;
      this.rcbP2Avr.Size = new Size(71, 21);
      this.rcbP2Avr.TabIndex = 36;
      this.rcbP2Avr.UnitText = "";
      this.rcbInfo.CalculationRule = "";
      this.rcbInfo.Caption = "Info";
      this.rcbInfo.Checked = false;
      this.rcbInfo.IsRegister = true;
      this.rcbInfo.Location = new Point(157, 138);
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      //this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(774, 452);
      this.Controls.Add((Control) this.grpUsed);
      this.Controls.Add((Control) this.grpCalculatedRegisters);
      this.Controls.Add((Control) this.grpCalculate);
      this.Controls.Add((Control) this.grpGraphs);
      this.Controls.Add((Control) this.grpReadLog);
      this.Controls.Add((Control) this.grpRegisters);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(742, 483);
      this.Name = "FrmDayLogger";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Daily Logger";
      this.Load += new EventHandler(this.frmDayLogger_Load);
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
