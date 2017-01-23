// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmLiveLogger
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
  public class FrmLiveLogger : Form
  {
    private string m_DataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\kamstrup\\METERTOOLLogViewer601";
    private LiveLogDataSet m_LLDS = new LiveLogDataSet();
    private clsCalculatedRegisters m_userRegisters = new clsCalculatedRegisters();
    private Functions mc601Functions = new Functions();
    private LiveLogDataSet.RegisterInUseRow m_RegisterInUseRow;
    private LiveLogDataSet.RegisterUnitRow m_RegisterUnit;
    private IContainer components;
    private GroupBox grpRegisters;
    private FlowLayoutPanel flpRegisters;
    private UCRegisterCheckBox rcbE1;
    private UCRegisterCheckBox rcbE7;
    private UCRegisterCheckBox rcbE3;
    private UCRegisterCheckBox rcbE4;
    private UCRegisterCheckBox rcbE5;
    private UCRegisterCheckBox rcbE6;
    private UCRegisterCheckBox rcbE2;
    private UCRegisterCheckBox rcbE8;
    private UCRegisterCheckBox rcbE9;
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
    private UCRegisterCheckBox rcbTA2;
    private UCRegisterCheckBox rcbTA3;
    private UCRegisterCheckBox rcbT4;
    private UCRegisterCheckBox rcbT1_T2;
    private UCRegisterCheckBox rcbFlow1;
    private UCRegisterCheckBox rcbFlow2;
    private UCRegisterCheckBox rcbEffect1;
    private GroupBox groupBox1;
    private NumericUpDown nudInterval;
    private Label label2;
    private Label label1;
    private Button btnStart;
    private Label label4;
    private NumericUpDown nudNumberOfReadings;
    private Timer timerLogger;
    private Button btnSave;
    private Button btnLoad;
    private Button btnSelectAll;
    private Button btnSelectNone;
    private Label lblRecords;
    private Label label3;
    private GroupBox grpCalculate;
    private Button btnAddToRegs;
    private Button btnCalculate;
    private ComboBox cbxRegister2;
    private ComboBox cbxCalcRule;
    private ComboBox cbxRegister1;
    private GroupBox grpGraphs;
    private Button btnSelectedRegisters;
    private Button btnClear;
    private GroupBox groupBox2;
    private FlowLayoutPanel flpCalculatedRegisters;
    private Button btnRemoveAllCalculated;
    private Button btnRemoveCalculatedRegister;
    private UCRegisterCheckBox rcbCalc0;
    private UCRegisterCheckBox rcbHour;
    private GroupBox grpUsed;
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

    public FrmLiveLogger()
    {
      this.InitializeComponent();
      this.m_userRegisters.Load("IntervalLoggerUR");
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

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (!this.IsAnyRCBChecked())
      {
        int num1 = (int) MessageBox.Show("Select one or more registers.", "Select registers", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else if (this.btnStart.Text == "Start")
      {
        if (this.btnSave.Enabled && MessageBox.Show("Do you wish to save the data,\r\nbefore you log a new set of data?", "Save data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          this.Save();
        bool flag = false;
        foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
        {
          UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
          if (registerCheckBox != null)
            flag = flag || registerCheckBox.Checked;
        }
        if (!flag)
        {
          int num2 = (int) MessageBox.Show("Select the registers to be logged.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          this.btnStart.Text = "Stop";
          this.m_LLDS.Registers.Clear();
          this.m_LLDS.RegisterInUse.Clear();
          this.m_LLDS.RegisterUnit.Clear();
          this.m_LLDS.CustomerNo.Clear();
          this.cbxRegister1.Items.Clear();
          this.cbxRegister2.Items.Clear();
          this.lblRecords.Text = this.m_LLDS.Registers.Count.ToString();
          this.m_RegisterInUseRow = this.m_LLDS.RegisterInUse.NewRegisterInUseRow();
          this.m_LLDS.RegisterInUse.AddRegisterInUseRow(this.m_RegisterInUseRow);
          this.m_RegisterUnit = this.m_LLDS.RegisterUnit.NewRegisterUnitRow();
          this.m_LLDS.RegisterUnit.AddRegisterUnitRow(this.m_RegisterUnit);
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
          this.m_RegisterInUseRow.M1 = this.rcbM1.Checked;
          this.m_RegisterInUseRow.M2 = this.rcbM2.Checked;
          this.m_RegisterInUseRow.T1 = this.rcbT1.Checked;
          this.m_RegisterInUseRow.T2 = this.rcbT2.Checked;
          this.m_RegisterInUseRow.T3 = this.rcbT3.Checked;
          this.m_RegisterInUseRow.T4 = this.rcbT4.Checked;
          this.m_RegisterInUseRow.T1_T2 = this.rcbT1_T2.Checked;
          this.m_RegisterInUseRow.P1 = this.rcbP1.Checked;
          this.m_RegisterInUseRow.P2 = this.rcbP2.Checked;
          this.m_RegisterInUseRow.FLOW1 = this.rcbFlow1.Checked;
          this.m_RegisterInUseRow.FLOW2 = this.rcbFlow2.Checked;
          this.m_RegisterInUseRow.EFFECT1 = this.rcbEffect1.Checked;
          this.m_RegisterInUseRow.Hour = this.rcbHour.Checked;
          this.Progress = 0;
          this.ProgressMax = (int) this.nudNumberOfReadings.Value * 2;
          this.timerLogger.Interval = Convert.ToInt32(new Decimal(60000) * this.nudInterval.Value);
          this.SetRCBsEnabled(false);
          this.btnSelectAll.Enabled = false;
          this.btnSelectNone.Enabled = false;
          this.btnLoad.Enabled = false;
          this.btnSave.Enabled = false;
          this.btnCalculate.Enabled = false;
          this.btnAddToRegs.Enabled = false;
          this.btnSelectedRegisters.Enabled = false;
          this.ReadCustomerNo();
          if (this.nudNumberOfReadings.Value > new Decimal(1))
            this.timerLogger.Start();
          this.MakeLogEvent();
        }
      }
      else
      {
        this.timerLogger.Stop();
        this.btnStart.Text = "Start";
        this.SetupCheckableRCBs();
        this.btnSave.Enabled = true;
        this.btnClear.Enabled = true;
        this.btnSelectAll.Enabled = true;
        this.btnLoad.Enabled = true;
        this.btnSelectNone.Enabled = true;
        this.btnCalculate.Enabled = true;
        this.btnAddToRegs.Enabled = true;
        this.btnSelectedRegisters.Enabled = true;
        this.Progress = 0;
        if (MessageBox.Show("Reading of data stopped.\r\nDo you wish to save the data?", "Reading stopped", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          this.Save();
        else
          this.Text = "Interval Log | Serial No " + this.m_LLDS.CustomerNo.GetCustomerNo() + " | Not saved.";
      }
    }

    private void ReadCustomerNo()
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
        if (!functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
          return;
        this.m_LLDS.CustomerNo.SetCustomerNo(Convert.ToString((double) arrValues[0] + (double) arrValues[1]));
      }
      catch
      {
      }
    }

    private void SetRCBsEnabled(bool bIsCheckable)
    {
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = bIsCheckable;
      }
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
          registerCheckBox.Enabled = bIsCheckable;
      }
    }

    private void timerLogger_Tick(object sender, EventArgs e)
    {
      this.MakeLogEvent();
    }

    private void MakeLogEvent()
    {
      string ErrorMessage = "";
      ArrayList arrRegisters = new ArrayList();
      ArrayList arrayList = new ArrayList();
      ArrayList arrValues = new ArrayList();
      ArrayList arrUnits = new ArrayList();
      LiveLogDataSet.RegistersRow registersRow = this.m_LLDS.Registers.NewRegistersRow();
      this.m_LLDS.Registers.AddRegistersRow(registersRow);
      ++this.Progress;
      foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null && registerCheckBox.Checked)
        {
          ushort num1 = (ushort) registerCheckBox.RegisterId;
          arrRegisters.Add((object) num1);
          arrayList.Add((object) registerCheckBox.RegisterName);
          if (arrRegisters.Count == 8)
          {
            arrValues.Clear();
            arrUnits.Clear();
            if (this.mc601Functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage) && arrValues.Count > 0)
            {
              for (int index = 0; index < arrRegisters.Count; ++index)
                this.PlaceIntoRow(registersRow, (string) arrayList[index], (double) arrValues[index], (byte) arrUnits[index]);
              arrRegisters.Clear();
              arrayList.Clear();
            }
            else
            {
              int num2 = (int) MessageBox.Show("Failed to connect to the MC601 Meter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
              this.timerLogger.Stop();
              this.btnStart.Text = "Start";
              this.SetRCBsEnabled(true);
              this.btnSave.Enabled = false;
              this.btnClear.Enabled = true;
              this.btnSelectAll.Enabled = true;
              this.btnSelectNone.Enabled = true;
              this.Progress = 0;
              return;
            }
          }
        }
      }
      if (arrRegisters.Count > 0)
      {
        arrValues.Clear();
        arrUnits.Clear();
        if (this.mc601Functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage) && arrValues.Count > 0)
        {
          for (int index = 0; index < arrRegisters.Count; ++index)
            this.PlaceIntoRow(registersRow, (string) arrayList[index], (double) arrValues[index], (byte) arrUnits[index]);
          arrRegisters.Clear();
          arrayList.Clear();
        }
        else
        {
          int num = (int) MessageBox.Show("Failed to connect to the MC601 Meter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.timerLogger.Stop();
          this.btnStart.Text = "Start";
          this.SetRCBsEnabled(true);
          this.btnSave.Enabled = false;
          this.btnClear.Enabled = true;
          this.btnLoad.Enabled = true;
          this.btnSelectAll.Enabled = true;
          this.btnSelectNone.Enabled = true;
          this.Progress = 0;
          return;
        }
      }
      registersRow.Date = DateTime.Now;
      ++this.Progress;
      this.lblRecords.Text = this.m_LLDS.Registers.Count.ToString();
      if (this.Progress != (int) this.nudNumberOfReadings.Value * 2)
        return;
      this.timerLogger.Stop();
      this.btnStart.Text = "Start";
      this.SetupCheckableRCBs();
      this.btnSave.Enabled = true;
      this.btnClear.Enabled = true;
      this.btnSelectAll.Enabled = true;
      this.btnLoad.Enabled = true;
      this.btnSelectNone.Enabled = true;
      this.btnCalculate.Enabled = true;
      this.btnAddToRegs.Enabled = true;
      this.btnSelectedRegisters.Enabled = true;
      this.Progress = 0;
      if (MessageBox.Show("Reading of data completed.\r\nDo you wish to save the data?", "Reading completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.Save();
      else
        this.Text = "Interval Log | Serial No " + this.m_LLDS.CustomerNo.GetCustomerNo() + " | Not saved.";
    }

    private void PlaceIntoRow(LiveLogDataSet.RegistersRow logData, string columnName, double Value, byte Unit)
    {
      logData[columnName] = (object) Value;
      this.m_RegisterUnit[columnName] = (object) Unit;
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
            this.cbxRegister1.Items.Add((object) cbxItem);
            this.cbxRegister2.Items.Add((object) cbxItem);
          }
          registerCheckBox.Enabled = flag;
        }
      }
      this.rcbCalc0.Enabled = (bool) this.m_RegisterInUseRow[this.rcbCalc0.RegisterName] && this.m_LLDS.Registers.Count > 0;
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
        {
          if (registerCheckBox.UseRegister2)
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName2] && this.m_LLDS.Registers.Count > 0;
            if (flag)
            {
              CbxItem cbxItem = new CbxItem();
              cbxItem.Register = registerCheckBox;
              this.cbxRegister1.Items.Add((object) cbxItem);
              this.cbxRegister2.Items.Add((object) cbxItem);
            }
            registerCheckBox.Enabled = flag;
          }
          else
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && this.m_LLDS.Registers.Count > 0;
            registerCheckBox.Enabled = flag;
          }
        }
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
      this.m_LLDS.Registers.Clear();
      this.m_LLDS.RegisterInUse.Clear();
      this.m_LLDS.RegisterUnit.Clear();
      this.cbxRegister1.Items.Clear();
      this.cbxRegister2.Items.Clear();
      this.lblRecords.Text = this.m_LLDS.Registers.Count.ToString();
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
      this.Text = "Interval Log | Serial No " + this.m_LLDS.CustomerNo.GetCustomerNo() + " | Empty.";
      this.btnSave.Enabled = false;
      this.btnCalculate.Enabled = false;
      this.btnAddToRegs.Enabled = false;
      this.btnSelectedRegisters.Enabled = false;
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

    private void Reg1XReg2(UCRegisterCheckBox Reg1, UCRegisterCheckBox Reg2, string CalcRule)
    {
      string registerName1 = Reg1.RegisterName;
      string registerName2 = Reg2.RegisterName;
      DataTable dataTable = new DataSet("GraphDataSet").Tables.Add("AllValues");
      dataTable.Columns.Add("Date", typeof (DateTime));
      byte nUnit1 = (byte) this.m_RegisterUnit[registerName1];
      byte nUnit2 = (byte) this.m_RegisterUnit[registerName2];
      string columnName = Reg1.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit1) + "] " + CalcRule + " " + Reg2.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit2) + "]";
      dataTable.Columns.Add(columnName, typeof (double));
      foreach (LiveLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.m_LLDS.Registers.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] = (object) registersRow.Date;
        Decimal register1 = Convert.ToDecimal(registersRow[registerName1]);
        Decimal register2 = Convert.ToDecimal(registersRow[registerName2]);
        row[columnName] = (object) FrmLiveLogger.DoCalculation(register1, register2, CalcRule);
        dataTable.Rows.Add(row);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_LLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_LLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private void Reg1XReg2(UCRegisterCheckBox Reg1, Decimal calcValue, string CalcRule)
    {
      string registerName = Reg1.RegisterName;
      DataTable dataTable = new DataSet("GraphDataSet").Tables.Add("AllValues");
      dataTable.Columns.Add("Date", typeof (DateTime));
      byte nUnit = (byte) this.m_RegisterUnit[registerName];
      string columnName = Reg1.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit) + "] " + CalcRule + " " + calcValue.ToString();
      dataTable.Columns.Add(columnName, typeof (double));
      foreach (LiveLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.m_LLDS.Registers.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] = (object) registersRow.Date;
        Decimal register1 = Convert.ToDecimal(registersRow[registerName]);
        row[columnName] = (object) FrmLiveLogger.DoCalculation(register1, calcValue, CalcRule);
        dataTable.Rows.Add(row);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_LLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_LLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
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
        d = !(register2 == new Decimal(0)) ? register1 / register2 : new Decimal(-1, -1, -1, false, (byte) 0);
      return Math.Round(d, 4);
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
          byte nUnit = Convert.ToByte(this.m_RegisterUnit[registerCheckBox.RegisterName]);
          registerCheckBox.UnitText = ClsUtils.UnitsForRegisters(nUnit);
          dataTable.Columns.Add(registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]", typeof (double));
        }
      }
      if (this.rcbCalc0.Checked && this.rcbCalc0.Enabled)
      {
        this.rcbCalc0.UnitText = ClsUtils.UnitsForRegisters(Convert.ToByte(this.m_RegisterUnit[this.rcbCalc0.RegisterName]));
        dataTable.Columns.Add(this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]", typeof (double));
      }
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
        if (rcb != null)
          this.AddCalculatedColumnForRCB(dataTable, rcb);
      }
      LiveLogDataSet.RegistersRow registersRow = (LiveLogDataSet.RegistersRow) null;
      foreach (LiveLogDataSet.RegistersRow row in this.m_LLDS.Registers)
      {
        DataRow dataRow = dataTable.NewRow();
        dataRow["Date"] = row["Date"];
        foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
        {
          UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
          if (registerCheckBox != null && registerCheckBox.Checked && registerCheckBox.Enabled)
          {
            double num = Convert.ToDouble(row[registerCheckBox.RegisterName]);
            dataRow[registerCheckBox.Caption + " [" + registerCheckBox.UnitText + "]"] = (object) num;
          }
        }
        if (this.rcbCalc0.Checked && this.rcbCalc0.Enabled)
        {
          if (registersRow != null)
          {
            Decimal num1 = Convert.ToDecimal(registersRow[this.rcbCalc0.RegisterName]);
            Decimal num2 = Convert.ToDecimal(row[this.rcbCalc0.RegisterName]);
            dataRow[this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]"] = num2 == new Decimal(0) || num1 == new Decimal(0) ? (object) 0 : (object) (num1 - num2);
          }
          else
            dataRow[this.rcbCalc0.Caption + " [" + this.rcbCalc0.UnitText + "]"] = (object) 0;
          registersRow = row;
        }
        foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
        {
          UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
          if (rcb != null && rcb.Checked && rcb.Enabled)
            FrmLiveLogger.CalculateCalculatedRowForRCB(dataRow, rcb, row);
        }
        dataTable.Rows.Add(dataRow);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_LLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_LLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private static void CalculateCalculatedRowForRCB(DataRow dr, UCRegisterCheckBox rcb, LiveLogDataSet.RegistersRow row)
    {
      if (!rcb.Checked || !rcb.Enabled)
        return;
      if (rcb.UseRegister2)
      {
        Decimal num = FrmLiveLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), Convert.ToDecimal(row[rcb.RegisterName2]), rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.RegisterName2] = (object) num;
      }
      else
      {
        Decimal num = FrmLiveLogger.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), rcb.CalculationValue, rcb.CalculationRule);
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
        byte nUnit1 = Convert.ToByte(this.m_RegisterUnit[rcb.RegisterName]);
        byte nUnit2 = Convert.ToByte(this.m_RegisterUnit[rcb.RegisterName2]);
        string str = rcb.RegisterCaption1 + " [" + ClsUtils.UnitsForRegisters(nUnit1) + "] " + rcb.CalculationRule + " " + rcb.RegisterCaption2 + " [" + ClsUtils.UnitsForRegisters(nUnit2) + "]";
        column.Caption = str;
        AllValues.Columns.Add(column);
      }
      else
      {
        DataColumn column = new DataColumn(rcb.RegisterName + rcb.CalculationRule + rcb.CalculationValue.ToString(), typeof (double));
        byte nUnit = Convert.ToByte(this.m_RegisterUnit[rcb.RegisterName]);
        string str = rcb.RegisterCaption1 + " [" + ClsUtils.UnitsForRegisters(nUnit) + "] " + rcb.CalculationRule + " " + rcb.CalculationValue.ToString();
        column.Caption = str;
        AllValues.Columns.Add(column);
      }
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
        this.m_userRegisters.Save("IntervalLoggerUR");
        this.addUserRegisterToForm(ur);
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
        foreach (object obj in this.m_userRegisters.ListOfCalculatedRegisters)
        {
          clsCalculatedRegister calculatedRegister = obj as clsCalculatedRegister;
          if (calculatedRegister != null && !calculatedRegister.UseRegister2 && (calculatedRegister.RegisterName_1 == ur.RegisterName_1 && !calculatedRegister.UseRegister2) && (calculatedRegister.CalculationValue == ur.CalculationValue && calculatedRegister.CalculationRule == ur.CalculationRule))
          {
            int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          }
        }
        this.m_userRegisters.ListOfCalculatedRegisters.Add((object) ur);
        this.m_userRegisters.Save("IntervalLoggerUR");
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

    private void btnLoad_Click(object sender, EventArgs e)
    {
      if (this.btnSave.Enabled && MessageBox.Show("Do you wish to save the data,\r\nbefore you load a new set of data?", "Save data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.Save();
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
        Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
      openFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
      openFileDialog.Filter = "MC601 Interval Log (*.MC601IntervalLog)|*.MC601IntervalLog|All files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      int num = (int) this.m_LLDS.ReadXml(openFileDialog.FileName);
      this.m_RegisterInUseRow = (LiveLogDataSet.RegisterInUseRow) this.m_LLDS.RegisterInUse.Rows[0];
      this.m_RegisterUnit = (LiveLogDataSet.RegisterUnitRow) this.m_LLDS.RegisterUnit.Rows[0];
      this.lblRecords.Text = this.m_LLDS.Registers.Count.ToString();
      this.timerLogger.Stop();
      this.Text = "Interval Log | Serial No " + this.m_LLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(openFileDialog.FileName);
      this.btnStart.Text = "Start";
      this.SetupCheckableRCBs();
      this.btnSave.Enabled = false;
      this.btnClear.Enabled = true;
      this.btnSelectAll.Enabled = true;
      this.btnSelectNone.Enabled = true;
      this.btnCalculate.Enabled = true;
      this.btnAddToRegs.Enabled = true;
      this.btnSelectedRegisters.Enabled = true;
      this.Progress = 0;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.Save();
    }

    private void Save()
    {
      if (this.m_LLDS.Registers.Rows.Count < 1)
      {
        int num1 = (int) MessageBox.Show("The Interval Log is empty.", "No data to save!");
      }
      else
      {
        try
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
            Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
          saveFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
          saveFileDialog.Filter = "MC601 Interval Log (*.MC601IntervalLog)|*.MC601IntervalLog|All files (*.*)|*.*";
          saveFileDialog.FilterIndex = 1;
          saveFileDialog.RestoreDirectory = true;
          if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
          if (saveFileDialog.FileName.IndexOf(".MC601IntervalLog") < 0)
            saveFileDialog.FileName = saveFileDialog.FileName + ".MC601IntervalLog";
          this.m_LLDS.WriteXml(saveFileDialog.FileName);
          this.btnSave.Enabled = false;
          this.Text = "Interval Log | Serial No " + this.m_LLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(saveFileDialog.FileName);
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Failed to save the the Interval Log:" + ex.Message);
        }
      }
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
      this.m_userRegisters.Save("IntervalLoggerUR");
    }

    private void btnRemoveAllCalculated_Click(object sender, EventArgs e)
    {
      this.m_userRegisters.ListOfCalculatedRegisters.Clear();
      this.m_userRegisters.Save("IntervalLoggerUR");
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

    private void FrmLiveLogger_Load(object sender, EventArgs e)
    {
    }

    private void rdbCalcB_CheckedChanged(object sender, EventArgs e)
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
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmLiveLogger));
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
      this.rcbM1 = new UCRegisterCheckBox();
      this.rcbM2 = new UCRegisterCheckBox();
      this.rcbT1 = new UCRegisterCheckBox();
      this.rcbT2 = new UCRegisterCheckBox();
      this.rcbT3 = new UCRegisterCheckBox();
      this.rcbT4 = new UCRegisterCheckBox();
      this.rcbT1_T2 = new UCRegisterCheckBox();
      this.rcbP1 = new UCRegisterCheckBox();
      this.rcbP2 = new UCRegisterCheckBox();
      this.rcbFlow1 = new UCRegisterCheckBox();
      this.rcbFlow2 = new UCRegisterCheckBox();
      this.rcbEffect1 = new UCRegisterCheckBox();
      this.rcbHour = new UCRegisterCheckBox();
      this.btnSelectAll = new Button();
      this.btnSelectNone = new Button();
      this.groupBox1 = new GroupBox();
      this.btnClear = new Button();
      this.lblRecords = new Label();
      this.label3 = new Label();
      this.btnSave = new Button();
      this.btnLoad = new Button();
      this.label4 = new Label();
      this.nudNumberOfReadings = new NumericUpDown();
      this.btnStart = new Button();
      this.label2 = new Label();
      this.label1 = new Label();
      this.nudInterval = new NumericUpDown();
      this.timerLogger = new Timer(this.components);
      this.grpCalculate = new GroupBox();
      this.btnAddToRegs = new Button();
      this.btnCalculate = new Button();
      this.cbxRegister2 = new ComboBox();
      this.cbxCalcRule = new ComboBox();
      this.cbxRegister1 = new ComboBox();
      this.grpGraphs = new GroupBox();
      this.btnSelectedRegisters = new Button();
      this.groupBox2 = new GroupBox();
      this.btnRemoveAllCalculated = new Button();
      this.btnRemoveCalculatedRegister = new Button();
      this.flpCalculatedRegisters = new FlowLayoutPanel();
      this.rcbCalc0 = new UCRegisterCheckBox();
      this.grpUsed = new GroupBox();
      this.rdbCalcB = new RadioButton();
      this.rdbCalcA = new RadioButton();
      this.nudCalc = new NumericUpDown();
      this.grpRegisters.SuspendLayout();
      this.flpRegisters.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.nudNumberOfReadings.BeginInit();
      this.nudInterval.BeginInit();
      this.grpCalculate.SuspendLayout();
      this.grpGraphs.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.grpUsed.SuspendLayout();
      this.nudCalc.BeginInit();
      this.SuspendLayout();
      this.grpRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.grpRegisters.BackColor = Color.Transparent;
      this.grpRegisters.Controls.Add((Control) this.flpRegisters);
      this.grpRegisters.Location = new Point(204, 12);
      this.grpRegisters.Name = "grpRegisters";
      this.grpRegisters.Size = new Size(290, 458);
      this.grpRegisters.TabIndex = 1;
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
      this.flpRegisters.Controls.Add((Control) this.rcbM1);
      this.flpRegisters.Controls.Add((Control) this.rcbM2);
      this.flpRegisters.Controls.Add((Control) this.rcbT1);
      this.flpRegisters.Controls.Add((Control) this.rcbT2);
      this.flpRegisters.Controls.Add((Control) this.rcbT3);
      this.flpRegisters.Controls.Add((Control) this.rcbT4);
      this.flpRegisters.Controls.Add((Control) this.rcbT1_T2);
      this.flpRegisters.Controls.Add((Control) this.rcbP1);
      this.flpRegisters.Controls.Add((Control) this.rcbP2);
      this.flpRegisters.Controls.Add((Control) this.rcbFlow1);
      this.flpRegisters.Controls.Add((Control) this.rcbFlow2);
      this.flpRegisters.Controls.Add((Control) this.rcbEffect1);
      this.flpRegisters.Controls.Add((Control) this.rcbHour);
      this.flpRegisters.Controls.Add((Control) this.btnSelectAll);
      this.flpRegisters.Controls.Add((Control) this.btnSelectNone);
      this.flpRegisters.Dock = DockStyle.Fill;
      this.flpRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpRegisters.Location = new Point(3, 16);
      this.flpRegisters.Name = "flpRegisters";
      this.flpRegisters.Size = new Size(284, 439);
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
      this.rcbE8.TabIndex = 7;
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
      this.rcbE9.TabIndex = 8;
      this.rcbE9.UnitText = "";
      this.rcbE9.UseRegister2 = true;
      this.rcbTA2.CalculationRule = "";
      this.rcbTA2.CalculationValue = new Decimal(new int[4]);
      this.rcbTA2.Caption = "TA2";
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
      this.rcbTA2.Size = new Size(54, 21);
      this.rcbTA2.TabIndex = 20;
      this.rcbTA2.UnitText = "";
      this.rcbTA2.UseRegister2 = true;
      this.rcbTA3.CalculationRule = "";
      this.rcbTA3.CalculationValue = new Decimal(new int[4]);
      this.rcbTA3.Caption = "TA3";
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
      this.rcbTA3.Size = new Size(54, 21);
      this.rcbTA3.TabIndex = 21;
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
      this.rcbM1.CalculationRule = "";
      this.rcbM1.CalculationValue = new Decimal(new int[4]);
      this.rcbM1.Caption = "M1";
      this.rcbM1.Checked = false;
      this.rcbM1.IsRegister = true;
      this.rcbM1.Location = new Point(3, 408);
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
      this.rcbM2.Location = new Point(157, 3);
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
      this.rcbT1.Location = new Point(157, 30);
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
      this.rcbT2.Location = new Point(157, 57);
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
      this.rcbT3.Location = new Point(157, 84);
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
      this.rcbT4.Location = new Point(157, 111);
      this.rcbT4.Name = "rcbT4";
      this.rcbT4.RegisterCaption1 = "";
      this.rcbT4.RegisterCaption2 = "";
      this.rcbT4.RegisterId = 122;
      this.rcbT4.RegisterName = "T4";
      this.rcbT4.RegisterName2 = "";
      this.rcbT4.RegisterType = ERegisterType.IsDouble;
      this.rcbT4.Size = new Size(47, 21);
      this.rcbT4.TabIndex = 22;
      this.rcbT4.UnitText = "";
      this.rcbT4.UseRegister2 = true;
      this.rcbT1_T2.CalculationRule = "";
      this.rcbT1_T2.CalculationValue = new Decimal(new int[4]);
      this.rcbT1_T2.Caption = "T1-T2";
      this.rcbT1_T2.Checked = false;
      this.rcbT1_T2.IsRegister = true;
      this.rcbT1_T2.Location = new Point(157, 138);
      this.rcbT1_T2.Name = "rcbT1_T2";
      this.rcbT1_T2.RegisterCaption1 = "";
      this.rcbT1_T2.RegisterCaption2 = "";
      this.rcbT1_T2.RegisterId = 89;
      this.rcbT1_T2.RegisterName = "T1_T2";
      this.rcbT1_T2.RegisterName2 = "";
      this.rcbT1_T2.RegisterType = ERegisterType.IsDouble;
      this.rcbT1_T2.Size = new Size(64, 21);
      this.rcbT1_T2.TabIndex = 23;
      this.rcbT1_T2.UnitText = "";
      this.rcbT1_T2.UseRegister2 = true;
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
      this.rcbP1.TabIndex = 19;
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
      this.rcbP2.TabIndex = 18;
      this.rcbP2.UnitText = "";
      this.rcbP2.UseRegister2 = true;
      this.rcbFlow1.CalculationRule = "";
      this.rcbFlow1.CalculationValue = new Decimal(new int[4]);
      this.rcbFlow1.Caption = "Flow 1";
      this.rcbFlow1.Checked = false;
      this.rcbFlow1.IsRegister = true;
      this.rcbFlow1.Location = new Point(157, 219);
      this.rcbFlow1.Name = "rcbFlow1";
      this.rcbFlow1.RegisterCaption1 = "";
      this.rcbFlow1.RegisterCaption2 = "";
      this.rcbFlow1.RegisterId = 74;
      this.rcbFlow1.RegisterName = "Flow1";
      this.rcbFlow1.RegisterName2 = "";
      this.rcbFlow1.RegisterType = ERegisterType.IsDouble;
      this.rcbFlow1.Size = new Size(67, 21);
      this.rcbFlow1.TabIndex = 24;
      this.rcbFlow1.UnitText = "";
      this.rcbFlow1.UseRegister2 = true;
      this.rcbFlow2.CalculationRule = "";
      this.rcbFlow2.CalculationValue = new Decimal(new int[4]);
      this.rcbFlow2.Caption = "Flow 2";
      this.rcbFlow2.Checked = false;
      this.rcbFlow2.IsRegister = true;
      this.rcbFlow2.Location = new Point(157, 246);
      this.rcbFlow2.Name = "rcbFlow2";
      this.rcbFlow2.RegisterCaption1 = "";
      this.rcbFlow2.RegisterCaption2 = "";
      this.rcbFlow2.RegisterId = 75;
      this.rcbFlow2.RegisterName = "Flow2";
      this.rcbFlow2.RegisterName2 = "";
      this.rcbFlow2.RegisterType = ERegisterType.IsDouble;
      this.rcbFlow2.Size = new Size(67, 21);
      this.rcbFlow2.TabIndex = 25;
      this.rcbFlow2.UnitText = "";
      this.rcbFlow2.UseRegister2 = true;
      this.rcbEffect1.CalculationRule = "";
      this.rcbEffect1.CalculationValue = new Decimal(new int[4]);
      this.rcbEffect1.Caption = "Power 1";
      this.rcbEffect1.Checked = false;
      this.rcbEffect1.IsRegister = true;
      this.rcbEffect1.Location = new Point(157, 273);
      this.rcbEffect1.Name = "rcbEffect1";
      this.rcbEffect1.RegisterCaption1 = "";
      this.rcbEffect1.RegisterCaption2 = "";
      this.rcbEffect1.RegisterId = 80;
      this.rcbEffect1.RegisterName = "Effect1";
      this.rcbEffect1.RegisterName2 = "";
      this.rcbEffect1.RegisterType = ERegisterType.IsDouble;
      this.rcbEffect1.Size = new Size(75, 21);
      this.rcbEffect1.TabIndex = 26;
      this.rcbEffect1.UnitText = "";
      this.rcbEffect1.UseRegister2 = true;
      this.rcbHour.CalculationRule = "";
      this.rcbHour.CalculationValue = new Decimal(new int[4]);
      this.rcbHour.Caption = "Hour counter";
      this.rcbHour.Checked = false;
      this.rcbHour.IsRegister = true;
      this.rcbHour.Location = new Point(157, 300);
      this.rcbHour.Name = "rcbHour";
      this.rcbHour.RegisterCaption1 = "";
      this.rcbHour.RegisterCaption2 = "";
      this.rcbHour.RegisterId = 1004;
      this.rcbHour.RegisterName = "Hour";
      this.rcbHour.RegisterName2 = "";
      this.rcbHour.RegisterType = ERegisterType.IsDouble;
      this.rcbHour.Size = new Size(99, 21);
      this.rcbHour.TabIndex = 29;
      this.rcbHour.UnitText = "";
      this.rcbHour.UseRegister2 = true;
      this.btnSelectAll.Location = new Point(157, 327);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new Size(75, 23);
      this.btnSelectAll.TabIndex = 28;
      this.btnSelectAll.Text = "Select All";
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
      this.btnSelectNone.Location = new Point(157, 356);
      this.btnSelectNone.Name = "btnSelectNone";
      this.btnSelectNone.Size = new Size(75, 23);
      this.btnSelectNone.TabIndex = 27;
      this.btnSelectNone.Text = "Select None";
      this.btnSelectNone.UseVisualStyleBackColor = true;
      this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
      this.groupBox1.BackColor = Color.Transparent;
      this.groupBox1.Controls.Add((Control) this.btnClear);
      this.groupBox1.Controls.Add((Control) this.lblRecords);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.btnSave);
      this.groupBox1.Controls.Add((Control) this.btnLoad);
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.nudNumberOfReadings);
      this.groupBox1.Controls.Add((Control) this.btnStart);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.nudInterval);
      this.groupBox1.Location = new Point(13, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(186, 155);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Interval logging";
      this.btnClear.Location = new Point(105, 72);
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
      this.label4.AutoSize = true;
      this.label4.Location = new Point(7, 46);
      this.label4.Name = "label4";
      this.label4.Size = new Size(99, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "Number of readings";
      this.nudNumberOfReadings.Location = new Point(108, 44);
      NumericUpDown numericUpDown1 = this.nudNumberOfReadings;
      int[] bits1 = new int[4];
      bits1[0] = 9999;
      Decimal num1 = new Decimal(bits1);
      numericUpDown1.Maximum = num1;
      NumericUpDown numericUpDown2 = this.nudNumberOfReadings;
      int[] bits2 = new int[4];
      bits2[0] = 1;
      Decimal num2 = new Decimal(bits2);
      numericUpDown2.Minimum = num2;
      this.nudNumberOfReadings.Name = "nudNumberOfReadings";
      this.nudNumberOfReadings.Size = new Size(52, 20);
      this.nudNumberOfReadings.TabIndex = 4;
      this.nudNumberOfReadings.TextAlign = HorizontalAlignment.Right;
      NumericUpDown numericUpDown3 = this.nudNumberOfReadings;
      int[] bits3 = new int[4];
      bits3[0] = 1;
      Decimal num3 = new Decimal(bits3);
      numericUpDown3.Value = num3;
      this.btnStart.Location = new Point(6, 72);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new Size(75, 23);
      this.btnStart.TabIndex = 3;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new EventHandler(this.btnStart_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(131, 20);
      this.label2.Name = "label2";
      this.label2.Size = new Size(46, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "minutes.";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(7, 20);
      this.label1.Name = "label1";
      this.label1.Size = new Size(69, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "1 reading per";
      this.nudInterval.Location = new Point(78, 18);
      NumericUpDown numericUpDown4 = this.nudInterval;
      int[] bits4 = new int[4];
      bits4[0] = 1440;
      Decimal num4 = new Decimal(bits4);
      numericUpDown4.Maximum = num4;
      NumericUpDown numericUpDown5 = this.nudInterval;
      int[] bits5 = new int[4];
      bits5[0] = 1;
      Decimal num5 = new Decimal(bits5);
      numericUpDown5.Minimum = num5;
      this.nudInterval.Name = "nudInterval";
      this.nudInterval.Size = new Size(51, 20);
      this.nudInterval.TabIndex = 0;
      this.nudInterval.TextAlign = HorizontalAlignment.Right;
      NumericUpDown numericUpDown6 = this.nudInterval;
      int[] bits6 = new int[4];
      bits6[0] = 1;
      Decimal num6 = new Decimal(bits6);
      numericUpDown6.Value = num6;
      this.timerLogger.Tick += new EventHandler(this.timerLogger_Tick);
      this.grpCalculate.BackColor = Color.Transparent;
      this.grpCalculate.Controls.Add((Control) this.rdbCalcB);
      this.grpCalculate.Controls.Add((Control) this.rdbCalcA);
      this.grpCalculate.Controls.Add((Control) this.nudCalc);
      this.grpCalculate.Controls.Add((Control) this.btnAddToRegs);
      this.grpCalculate.Controls.Add((Control) this.btnCalculate);
      this.grpCalculate.Controls.Add((Control) this.cbxRegister2);
      this.grpCalculate.Controls.Add((Control) this.cbxCalcRule);
      this.grpCalculate.Controls.Add((Control) this.cbxRegister1);
      this.grpCalculate.Location = new Point(12, 174);
      this.grpCalculate.Name = "grpCalculate";
      this.grpCalculate.Size = new Size(187, 156);
      this.grpCalculate.TabIndex = 10;
      this.grpCalculate.TabStop = false;
      this.grpCalculate.Text = "Calculate";
      this.btnAddToRegs.Location = new Point(106, (int) sbyte.MaxValue);
      this.btnAddToRegs.Name = "btnAddToRegs";
      this.btnAddToRegs.Size = new Size(75, 23);
      this.btnAddToRegs.TabIndex = 17;
      this.btnAddToRegs.Text = "Add to Regs. ";
      this.btnAddToRegs.UseVisualStyleBackColor = true;
      this.btnAddToRegs.Click += new EventHandler(this.btnAddToRegs_Click);
      this.btnCalculate.Location = new Point(7, (int) sbyte.MaxValue);
      this.btnCalculate.Name = "btnCalculate";
      this.btnCalculate.Size = new Size(75, 23);
      this.btnCalculate.TabIndex = 16;
      this.btnCalculate.Text = "Show Graph";
      this.btnCalculate.UseVisualStyleBackColor = true;
      this.btnCalculate.Click += new EventHandler(this.btnCalculate_Click);
      this.cbxRegister2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbxRegister2.FormattingEnabled = true;
      this.cbxRegister2.Location = new Point(44, 74);
      this.cbxRegister2.Name = "cbxRegister2";
      this.cbxRegister2.Size = new Size(137, 21);
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
      this.grpGraphs.Location = new Point(13, 336);
      this.grpGraphs.Name = "grpGraphs";
      this.grpGraphs.Size = new Size(186, 50);
      this.grpGraphs.TabIndex = 9;
      this.grpGraphs.TabStop = false;
      this.grpGraphs.Text = "Graphs";
      this.btnSelectedRegisters.Location = new Point(6, 20);
      this.btnSelectedRegisters.Name = "btnSelectedRegisters";
      this.btnSelectedRegisters.Size = new Size(175, 23);
      this.btnSelectedRegisters.TabIndex = 11;
      this.btnSelectedRegisters.Text = "Selected Registers";
      this.btnSelectedRegisters.UseVisualStyleBackColor = true;
      this.btnSelectedRegisters.Click += new EventHandler(this.btnSelectedRegisters_Click);
      this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox2.BackColor = Color.Transparent;
      this.groupBox2.Controls.Add((Control) this.btnRemoveAllCalculated);
      this.groupBox2.Controls.Add((Control) this.btnRemoveCalculatedRegister);
      this.groupBox2.Controls.Add((Control) this.flpCalculatedRegisters);
      this.groupBox2.Location = new Point(500, 57);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(240, 413);
      this.groupBox2.TabIndex = 11;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Calculated Registers";
      this.btnRemoveAllCalculated.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnRemoveAllCalculated.Location = new Point(126, 384);
      this.btnRemoveAllCalculated.Name = "btnRemoveAllCalculated";
      this.btnRemoveAllCalculated.Size = new Size(108, 23);
      this.btnRemoveAllCalculated.TabIndex = 32;
      this.btnRemoveAllCalculated.Text = "Remove All";
      this.btnRemoveAllCalculated.UseVisualStyleBackColor = true;
      this.btnRemoveAllCalculated.Click += new EventHandler(this.btnRemoveAllCalculated_Click);
      this.btnRemoveCalculatedRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRemoveCalculatedRegister.Location = new Point(6, 384);
      this.btnRemoveCalculatedRegister.Name = "btnRemoveCalculatedRegister";
      this.btnRemoveCalculatedRegister.Size = new Size(108, 23);
      this.btnRemoveCalculatedRegister.TabIndex = 31;
      this.btnRemoveCalculatedRegister.Text = "Remove Selected";
      this.btnRemoveCalculatedRegister.UseVisualStyleBackColor = true;
      this.btnRemoveCalculatedRegister.Click += new EventHandler(this.btnRemoveCalculatedRegister_Click);
      this.flpCalculatedRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.flpCalculatedRegisters.AutoScroll = true;
      this.flpCalculatedRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpCalculatedRegisters.Location = new Point(3, 13);
      this.flpCalculatedRegisters.Name = "flpCalculatedRegisters";
      this.flpCalculatedRegisters.Size = new Size(234, 365);
      this.flpCalculatedRegisters.TabIndex = 18;
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
      this.rcbCalc0.TabIndex = 33;
      this.rcbCalc0.UnitText = "";
      this.rcbCalc0.UseRegister2 = true;
      this.grpUsed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.grpUsed.BackColor = Color.Transparent;
      this.grpUsed.Controls.Add((Control) this.rcbCalc0);
      this.grpUsed.Location = new Point(500, 12);
      this.grpUsed.Name = "grpUsed";
      this.grpUsed.Size = new Size(240, 40);
      this.grpUsed.TabIndex = 24;
      this.grpUsed.TabStop = false;
      this.grpUsed.Text = "Used per year";
      this.rdbCalcB.AutoSize = true;
      this.rdbCalcB.Location = new Point(6, 103);
      this.rdbCalcB.Name = "rdbCalcB";
      this.rdbCalcB.Size = new Size(14, 13);
      this.rdbCalcB.TabIndex = 23;
      this.rdbCalcB.UseVisualStyleBackColor = true;
      this.rdbCalcB.CheckedChanged += new EventHandler(this.rdbCalcB_CheckedChanged);
      this.rdbCalcA.AutoSize = true;
      this.rdbCalcA.Checked = true;
      this.rdbCalcA.Location = new Point(6, 76);
      this.rdbCalcA.Name = "rdbCalcA";
      this.rdbCalcA.Size = new Size(14, 13);
      this.rdbCalcA.TabIndex = 22;
      this.rdbCalcA.TabStop = true;
      this.rdbCalcA.UseVisualStyleBackColor = true;
      this.rdbCalcA.CheckedChanged += new EventHandler(this.rdbCalcB_CheckedChanged);
      this.nudCalc.DecimalPlaces = 3;
      this.nudCalc.Enabled = false;
      this.nudCalc.Location = new Point(44, 101);
      NumericUpDown numericUpDown7 = this.nudCalc;
      int[] bits7 = new int[4];
      bits7[0] = 100000;
      Decimal num7 = new Decimal(bits7);
      numericUpDown7.Maximum = num7;
      this.nudCalc.Name = "nudCalc";
      this.nudCalc.Size = new Size(136, 20);
      this.nudCalc.TabIndex = 21;
      this.nudCalc.TextAlign = HorizontalAlignment.Right;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(752, 482);
      this.Controls.Add((Control) this.grpUsed);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.grpCalculate);
      this.Controls.Add((Control) this.grpGraphs);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.grpRegisters);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(760, 420);
      this.Name = "FrmLiveLogger";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Interval Logger";
      this.Load += new EventHandler(this.FrmLiveLogger_Load);
      this.grpRegisters.ResumeLayout(false);
      this.flpRegisters.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.nudNumberOfReadings.EndInit();
      this.nudInterval.EndInit();
      this.grpCalculate.ResumeLayout(false);
      this.grpCalculate.PerformLayout();
      this.grpGraphs.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.grpUsed.ResumeLayout(false);
      this.nudCalc.EndInit();
      this.ResumeLayout(false);
    }
  }
}
