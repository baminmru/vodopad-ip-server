// Decompiled with JetBrains decompiler
// Type: MC601LogView.FrmHourLogSelecter
// Assembly: MC601LogView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C22A753E-EAD1-4FBB-8540-FB46F840C010
// Assembly location: C:\Program Files (x86)\Kamstrup\MC601LogView\MC601LogView.exe

using Kamstrup.Heat.mc601Communication;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace MC601LogView
{
  public class FrmHourLogSelecter : Form
  {
    private string m_DataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\kamstrup\\METERTOOLLogViewer601";
    private DateTime m_OldestDate = new DateTime(1990, 1, 1);
    private Functions m_mc601Functions = new Functions();
    private HourLogDataSet m_HLDS = new HourLogDataSet();
    private clsCalculatedRegisters m_userRegisters = new clsCalculatedRegisters();
    private eHourLogType m_HourLogType = eHourLogType._Unknown;
    private string m_fileExtension = "";
    private string m_Title = "";
    private DateTime m_NewestDate;
    private HourLogDataSet.RegisterInUseRow m_RegisterInUseRow;
    private HourLogDataSet.RegisterUnitRow m_RegisterUnitRow;
    private bool bConnectionLost;
    private IContainer components;
    private GroupBox grpCalculatedRegisters;
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
    private UCRegisterCheckBox rcbdE;
    private UCRegisterCheckBox rcbcE;
    private UCRegisterCheckBox rcbInfo;
    private Button btnSelectAll;
    private Button btnSelectNone;
    private ComboBox cbxTo;
    private ComboBox cbxFrom;
    private Label lblTo;
    private Label lblFrom;
    private UCRegisterCheckBox rcbdV;
    private UCRegisterCheckBox rcbcV;
    private Button btnRemoveCalculatedRegister;
    private Button btnRemoveAllCalculated;
    private UCRegisterCheckBox rcbP1;
    private UCRegisterCheckBox rcbP2;
    private UCRegisterCheckBox rcbCalc0;
    private GroupBox grpDifferenceVE;
    private Button btnReadVE;
    private Label lblcVE;
    private Label lbldVE;
    private TextBox txtcVE;
    private TextBox txtdVE;
    private GroupBox grpUsed;
    private CheckBox chkReadRawData;
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

    public FrmHourLogSelecter(eHourLogType hourLogType)
    {
      this.InitializeComponent();
      this.m_HourLogType = hourLogType;
      switch (this.m_HourLogType)
      {
        case eHourLogType._6702:
          this.m_fileExtension = "HourLog6702";
          this.rcbdE.Visible = true;
          this.rcbcE.Visible = true;
          this.rcbdV.Visible = false;
          this.rcbcV.Visible = false;
          this.m_Title = "Hourly log with Difference Energy";
          break;
        case eHourLogType._6703:
          this.m_fileExtension = "HourLog6703";
          this.rcbdE.Visible = false;
          this.rcbcE.Visible = false;
          this.rcbdV.Visible = false;
          this.rcbcV.Visible = false;
          this.m_Title = "Hourly log with PQ-Limitter";
          break;
        case eHourLogType._6705:
          this.m_fileExtension = "HourLog6705";
          this.rcbdE.Visible = false;
          this.rcbcE.Visible = false;
          this.rcbdV.Visible = false;
          this.rcbcV.Visible = false;
          this.m_Title = "Hourly log with KMP Data-Port";
          break;
        case eHourLogType._6708:
          this.m_fileExtension = "HourLog6708";
          this.rcbdE.Visible = false;
          this.rcbcE.Visible = false;
          this.rcbdV.Visible = false;
          this.rcbcV.Visible = false;
          this.m_Title = "Hourly log";
          break;
        case eHourLogType._6709:
          this.m_fileExtension = "HourLog6709";
          this.rcbdE.Visible = false;
          this.rcbcE.Visible = false;
          this.rcbdV.Visible = true;
          this.rcbcV.Visible = true;
          this.m_Title = "Hourly log with Difference Volume";
          break;
      }
      this.m_userRegisters.Load(this.m_fileExtension + "UR");
      this.InsertCalculatedRegister();
      this.SetupDifferenceVolumeEnergy();
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

    private DataTable MakeListOfDays(int numberOfDays)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("Value", typeof (DateTime));
      dataTable.Columns.Add("Text", typeof (string));
      dataTable.NewRow();
      DataRow row1 = dataTable.NewRow();
      row1["Value"] = (object) this.m_NewestDate;
      row1["Text"] = (object) ("Newest Date - " + this.m_NewestDate.ToShortDateString());
      dataTable.Rows.Add(row1);
      DateTime dateTime = new DateTime(this.m_NewestDate.Year, this.m_NewestDate.Month, this.m_NewestDate.Day, 23, 0, 0);
      for (int index = 1; index < numberOfDays; ++index)
      {
        DataRow row2 = dataTable.NewRow();
        row2["Value"] = (object) dateTime.AddDays((double) -index);
        row2["Text"] = (object) ("-" + index.ToString() + " days.");
        dataTable.Rows.Add(row2);
      }
      return dataTable;
    }

    private bool getNewestDate()
    {
      ArrayList arrValues = new ArrayList();
      string ErrorMessage = "";
      if (this.m_mc601Functions.GetHistoricalHourDataTimeStamps((byte) 127, ushort.MaxValue, ushort.MaxValue, arrValues, out ErrorMessage))
      {
        if (arrValues.Count == 0)
        {
          int num = (int) MessageBox.Show("There isn't any data stored in this Hourly Date Top Module.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
        this.m_NewestDate = (DateTime) arrValues[0];
        if (arrValues.Count < 24)
          this.m_OldestDate = (DateTime) arrValues[arrValues.Count - 1];
        return true;
      }
      int num1 = (int) MessageBox.Show("Could not connect to a Hourly Date Top Module.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
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
      HourLogDataSet.RegistersRow registersRow = (HourLogDataSet.RegistersRow) null;
      foreach (HourLogDataSet.RegistersRow row in this.m_HLDS.Registers)
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
          if (rcb != null)
            FrmHourLogSelecter.CalculateCalculatedRowForRCB(dataRow, rcb, row);
        }
        dataTable.Rows.InsertAt(dataRow, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_HLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.ShowTime = true;
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_HLDS.CustomerNo.GetCustomerNo());
      frmGraph.MdiParent = this.MdiParent;
      frmGraph.ShowTime = true;
      frmGraph.SetData(dataTable);
      frmGraph.Show();
    }

    private static void CalculateCalculatedRowForRCB(DataRow dr, UCRegisterCheckBox rcb, HourLogDataSet.RegistersRow row)
    {
      if (!rcb.Checked || !rcb.Enabled)
        return;
      if (rcb.UseRegister2)
      {
        Decimal num = FrmHourLogSelecter.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), Convert.ToDecimal(row[rcb.RegisterName2]), rcb.CalculationRule);
        dr[rcb.RegisterName + rcb.CalculationRule + rcb.RegisterName2] = (object) num;
      }
      else
      {
        Decimal num = FrmHourLogSelecter.DoCalculation(Convert.ToDecimal(row[rcb.RegisterName]), rcb.CalculationValue, rcb.CalculationRule);
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

    private void frmHourLogSelecter_Load(object sender, EventArgs e)
    {
      this.btnRead.Enabled = false;
      if (this.GetCurrentTopMopduleType() == this.m_HourLogType)
      {
        if (!this.getNewestDate())
          return;
        this.cbxFrom.DataSource = (object) this.MakeListOfDays(64);
        this.cbxFrom.DisplayMember = "Text";
        this.cbxFrom.ValueMember = "Value";
        this.cbxTo.DataSource = (object) this.MakeListOfDays(64);
        this.cbxTo.DisplayMember = "Text";
        this.cbxTo.ValueMember = "Value";
        this.btnRead.Enabled = true;
      }
      else
      {
        int num = (int) MessageBox.Show("The chosen top module is not installed.", "Info");
      }
    }

    private eHourLogType GetCurrentTopMopduleType()
    {
      eHourLogType eHourLogType = eHourLogType._Unknown;
      try
      {
        ArrayList arrRegisters = new ArrayList();
        ArrayList arrValues = new ArrayList();
        ArrayList arrUnits = new ArrayList();
        arrRegisters.Add((object) 157);
        string ErrorMessage = "";
        if (this.m_mc601Functions.GetData((byte) 63, (byte) arrRegisters.Count, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
        {
          double num = (double) arrValues[0];
          if (num == 67020000.0)
            eHourLogType = eHourLogType._6702;
          else if (num == 67030000.0)
            eHourLogType = eHourLogType._6703;
          else if (num == 67050000.0)
            eHourLogType = eHourLogType._6705;
          else if (num == 67080000.0)
            eHourLogType = eHourLogType._6708;
          else if (num == 67090000.0)
            eHourLogType = eHourLogType._6709;
        }
      }
      catch
      {
      }
      return eHourLogType;
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (!this.IsAnyRCBChecked())
      {
        int num = (int) MessageBox.Show("Select one or more registers.", "Select registers", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        this.Cursor = Cursors.WaitCursor;
        DateTime dateTime1 = (DateTime) this.cbxFrom.SelectedValue;
        DateTime dateTime2 = (DateTime) this.cbxTo.SelectedValue;
        if (dateTime1 < dateTime2)
          this.ReadData(dateTime1, dateTime2);
        else
          this.ReadData(dateTime2, dateTime1);
        this.Progress = 0;
        this.SetupCheckableRCBs();
        if (this.m_HLDS.Registers.Rows.Count > 0)
        {
          this.Text = this.m_Title + " | Serial No " + this.m_HLDS.CustomerNo.GetCustomerNo() + " | Not saved.";
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
            this.cbxRegister1.Items.Add((object) cbxItem);
            this.cbxRegister2.Items.Add((object) cbxItem);
          }
          registerCheckBox.Enabled = flag;
        }
      }
      this.rcbCalc0.Enabled = (bool) this.m_RegisterInUseRow[this.rcbCalc0.RegisterName] && this.m_HLDS.Registers.Count > 0;
      foreach (Control control in (ArrangedElementCollection) this.flpCalculatedRegisters.Controls)
      {
        UCRegisterCheckBox registerCheckBox = control as UCRegisterCheckBox;
        if (registerCheckBox != null)
        {
          if (registerCheckBox.UseRegister2)
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName2] && this.m_HLDS.Registers.Count > 0;
            registerCheckBox.Enabled = flag;
          }
          else
          {
            bool flag = (bool) this.m_RegisterInUseRow[registerCheckBox.RegisterName] && this.m_HLDS.Registers.Count > 0;
            registerCheckBox.Enabled = flag;
          }
        }
      }
    }

    private void ReadData(DateTime dtFrom, DateTime dtTo)
    {
      this.bConnectionLost = false;
      this.m_HLDS.Registers.Clear();
      this.m_HLDS.RegisterUnit.Clear();
      this.m_HLDS.RegisterInUse.Clear();
      this.m_HLDS.CustomerNo.Clear();
      this.m_RegisterUnitRow = this.m_HLDS.RegisterUnit.NewRegisterUnitRow();
      this.m_HLDS.RegisterUnit.AddRegisterUnitRow(this.m_RegisterUnitRow);
      this.m_RegisterInUseRow = this.m_HLDS.RegisterInUse.NewRegisterInUseRow();
      this.m_HLDS.RegisterInUse.AddRegisterInUseRow(this.m_RegisterInUseRow);
      this.m_RegisterInUseRow.E1 = this.rcbE1.Checked;
      this.m_RegisterInUseRow.E2 = this.rcbE2.Checked;
      this.m_RegisterInUseRow.E3 = this.rcbE3.Checked;
      this.m_RegisterInUseRow.E4 = this.rcbE4.Checked;
      this.m_RegisterInUseRow.E5 = this.rcbE5.Checked;
      this.m_RegisterInUseRow.E6 = this.rcbE6.Checked;
      this.m_RegisterInUseRow.E7 = this.rcbE7.Checked;
      this.m_RegisterInUseRow.V1 = this.rcbV1.Checked;
      this.m_RegisterInUseRow.V2 = this.rcbV2.Checked;
      this.m_RegisterInUseRow.INA = this.rcbInA.Checked;
      this.m_RegisterInUseRow.INB = this.rcbInB.Checked;
      this.m_RegisterInUseRow.M1 = this.rcbM1.Checked;
      this.m_RegisterInUseRow.M2 = this.rcbM2.Checked;
      this.m_RegisterInUseRow.T1 = this.rcbT1.Checked;
      this.m_RegisterInUseRow.T2 = this.rcbT2.Checked;
      this.m_RegisterInUseRow.T3 = this.rcbT3.Checked;
      this.m_RegisterInUseRow.P1 = this.rcbP1.Checked;
      this.m_RegisterInUseRow.P2 = this.rcbP2.Checked;
      this.m_RegisterInUseRow.dE = false;
      this.m_RegisterInUseRow.cE = false;
      this.m_RegisterInUseRow.dV = false;
      this.m_RegisterInUseRow.cV = false;
      switch (this.m_HourLogType)
      {
        case eHourLogType._6702:
          this.m_RegisterInUseRow.dE = this.rcbdE.Checked;
          this.m_RegisterInUseRow.cE = this.rcbcE.Checked;
          break;
        case eHourLogType._6709:
          this.m_RegisterInUseRow.dV = this.rcbdV.Checked;
          this.m_RegisterInUseRow.cV = this.rcbcV.Checked;
          break;
      }
      this.m_RegisterInUseRow.INFO = this.rcbInfo.Checked;
      this.Progress = 0;
      int num1 = dtTo.DayOfYear - dtFrom.DayOfYear + 1;
      if (num1 < 0)
        num1 = dtTo.AddDays(180.0).DayOfYear - dtFrom.AddDays(180.0).DayOfYear + 1;
      this.ProgressMax = num1;
      this.ReadCustomerNo(ref this.bConnectionLost);
      if (this.chkReadRawData.Checked)
      {
        if (dtFrom.Date > dtTo.Date)
          this.ReadSetOfDays(dtFrom.Date, dtTo.Date);
        else
          this.ReadSetOfDays(dtTo.Date, dtFrom.Date);
        if (this.bConnectionLost)
        {
          int num2 = (int) MessageBox.Show("Lost the connection to the MC601 Meter!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.m_HLDS.Registers.Clear();
          this.lblRecords.Text = this.m_HLDS.Registers.Count.ToString();
          return;
        }
      }
      else
      {
        for (DateTime day = dtTo; day >= dtFrom; day = day.AddDays(-1.0))
        {
          this.ReadFor24Hours(day);
          if (day == dtTo)
            day = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day, 23, 0, 0);
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
            int num2 = (int) MessageBox.Show("Lost the connection to the MC601 Meter!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            this.m_HLDS.Registers.Clear();
            this.lblRecords.Text = this.m_HLDS.Registers.Count.ToString();
            return;
          }
        }
      }
      this.lblRecords.Text = this.m_HLDS.Registers.Count.ToString();
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
          this.m_HLDS.CustomerNo.SetCustomerNo(Convert.ToString((double) arrValues[0] + (double) arrValues[1]));
        else
          bConnectionLost = true;
      }
      catch
      {
        bConnectionLost = true;
      }
    }

    private void ReadFor24Hours(DateTime day)
    {
      bool flag = false;
      int num1 = day.Year % 100;
      int month = day.Month;
      int day1 = day.Day;
      int hour1 = day.Hour;
      ArrayList arrayList = new ArrayList();
      ArrayList rowArray = new ArrayList();
      if (day < this.m_OldestDate)
      {
        for (int hour2 = 23; hour2 > -1; --hour2)
        {
          HourLogDataSet.RegistersRow row = this.m_HLDS.Registers.NewRegistersRow();
          this.m_HLDS.Registers.AddRegistersRow(row);
          row.Date = new DateTime(2000 + num1, month, day1, hour2, 0, 0);
        }
      }
      else
      {
        while (!flag && hour1 > -1 && !this.bConnectionLost)
        {
          ArrayList arrValues = new ArrayList();
          rowArray.Clear();
          string ErrorMessage = "";
          ushort num2 = (ushort) (num1 * 256 + month);
          ushort num3 = (ushort) (day1 * 256 + hour1);
          if (this.m_mc601Functions.GetHistoricalHourDataTimeStamps((byte) 127, num2, num3, arrValues, out ErrorMessage))
          {
            if (arrValues.Count > 0)
            {
              if (arrValues.Count < 24)
                this.m_OldestDate = (DateTime) arrValues[arrValues.Count - 1];
              int num4 = 24;
              int num5 = 0;
              foreach (DateTime dateTime in arrValues)
              {
                if (dateTime.Year % 100 == num1 && dateTime.Month == month && dateTime.Day == day1)
                {
                  flag = true;
                  hour1 = dateTime.Hour;
                  if (hour1 < num4 - 1)
                  {
                    for (int hour2 = num4 - 1; hour2 > hour1; --hour2)
                    {
                      HourLogDataSet.RegistersRow row = this.m_HLDS.Registers.NewRegistersRow();
                      this.m_HLDS.Registers.AddRegistersRow(row);
                      row.Date = new DateTime(2000 + num1, month, day1, hour2, 0, 0);
                    }
                  }
                  HourLogDataSet.RegistersRow row1 = this.m_HLDS.Registers.NewRegistersRow();
                  rowArray.Add((object) row1);
                  this.m_HLDS.Registers.AddRegistersRow(row1);
                  row1.Date = dateTime;
                  row1.Id = num5;
                  ++num5;
                  num4 = hour1;
                }
              }
              if (hour1 > 0)
              {
                for (int hour2 = hour1 - 1; hour2 > -1; --hour2)
                {
                  HourLogDataSet.RegistersRow row = this.m_HLDS.Registers.NewRegistersRow();
                  this.m_HLDS.Registers.AddRegistersRow(row);
                  row.Date = new DateTime(2000 + num1, month, day1, hour2, 0, 0);
                }
              }
              foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
              {
                UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
                if (rcb != null && rcb.Checked && !this.bConnectionLost)
                  this.ReadFor24HoursRegister(rcb, rowArray, num2, num3);
              }
            }
            else
              --hour1;
          }
          else
            this.bConnectionLost = true;
        }
      }
    }

    private void ReadFor24HoursRegister(UCRegisterCheckBox rcb, ArrayList rowArray, ushort timestamp1, ushort timestamp2)
    {
      byte UnitId = (byte) 0;
      string ErrorMessage = "";
      ArrayList arrValues = new ArrayList();
      if (this.m_mc601Functions.GetHistoricalHourData((byte) 127, (ushort) rcb.RegisterId, timestamp1, timestamp2, arrValues, out UnitId, out ErrorMessage))
      {
        if (arrValues.Count <= 0)
          return;
        this.m_RegisterUnitRow[rcb.RegisterName] = (object) UnitId;
        foreach (HourLogDataSet.RegistersRow registersRow in rowArray)
        {
          int id = registersRow.Id;
          registersRow[rcb.RegisterName] = (object) (double) arrValues[id];
        }
      }
      else
        this.bConnectionLost = true;
    }

    private void ReadSetOfDays(DateTime fromDay, DateTime endDay)
    {
      int num1 = fromDay.DayOfYear - endDay.DayOfYear + 1;
      if (num1 < 0)
        num1 = fromDay.AddDays(180.0).DayOfYear - endDay.AddDays(180.0).DayOfYear + 1;
      DateTime dateTime1 = this.m_NewestDate;
      ArrayList arrValues = new ArrayList();
      ArrayList rowArray = new ArrayList();
      string ErrorMessage = "";
      bool flag = false;
      bool skipFirst = false;
      while (!flag)
      {
        int num2 = dateTime1.Year % 100;
        int month = dateTime1.Month;
        int day = dateTime1.Day;
        int hour = dateTime1.Hour;
        ushort num3 = (ushort) (num2 * 256 + month);
        ushort num4 = (ushort) (day * 256 + hour);
        Trace.WriteLine("lastFound_timestamp = " + dateTime1.ToString());
        arrValues.Clear();
        rowArray.Clear();
        if (this.m_mc601Functions.GetHistoricalHourDataTimeStamps((byte) 127, num3, num4, arrValues, out ErrorMessage))
        {
          Trace.WriteLine("hourArray count = " + arrValues.Count.ToString());
          for (int index = 0; index < arrValues.Count; ++index)
          {
            DateTime dateTime2 = (DateTime) arrValues[index];
            HourLogDataSet.RegistersRow row = this.m_HLDS.Registers.NewRegistersRow();
            row["Id"] = (object) (dateTime2.Year + dateTime2.Month + dateTime2.Day + dateTime2.Hour);
            row["Date"] = (object) dateTime2;
            if (index != 0 || !skipFirst)
              this.m_HLDS.Registers.AddRegistersRow(row);
            rowArray.Add((object) row);
            dateTime1 = dateTime2;
          }
          foreach (Control control in (ArrangedElementCollection) this.flpRegisters.Controls)
          {
            UCRegisterCheckBox rcb = control as UCRegisterCheckBox;
            if (rcb != null && rcb.Checked && !this.bConnectionLost)
              this.ReadRawFor24HoursRegister(rcb, rowArray, num3, num4, skipFirst);
          }
          rowArray.Clear();
          skipFirst = true;
          if (this.m_HLDS.Registers.Rows.Count > num1 * 24 || arrValues.Count == 0)
            flag = true;
        }
        else
          flag = true;
      }
    }

    private void ReadRawFor24HoursRegister(UCRegisterCheckBox rcb, ArrayList rowArray, ushort timestamp1, ushort timestamp2, bool skipFirst)
    {
      byte UnitId = (byte) 0;
      string ErrorMessage = "";
      ArrayList arrValues = new ArrayList();
      if (this.m_mc601Functions.GetHistoricalHourData((byte) 127, (ushort) rcb.RegisterId, timestamp1, timestamp2, arrValues, out UnitId, out ErrorMessage))
      {
        if (arrValues.Count <= 0)
          return;
        this.m_RegisterUnitRow[rcb.RegisterName] = (object) UnitId;
        for (int index = 0; index < arrValues.Count; ++index)
          ((DataRow) rowArray[index])[rcb.RegisterName] = (object) (double) arrValues[index];
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
      this.m_HLDS.Registers.Clear();
      this.m_HLDS.RegisterInUse.Clear();
      this.m_HLDS.RegisterUnit.Clear();
      this.cbxRegister1.Items.Clear();
      this.cbxRegister2.Items.Clear();
      this.lblRecords.Text = this.m_HLDS.Registers.Count.ToString();
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
      this.Text = this.m_Title + " | Serial No " + this.m_HLDS.CustomerNo.GetCustomerNo() + " | Empty.";
      this.btnSave.Enabled = false;
      this.btnCalculate.Enabled = false;
      this.btnAddToRegs.Enabled = false;
      this.btnSelectedRegisters.Enabled = false;
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
      openFileDialog.Filter = "MC601 Hourly Log (*." + this.m_fileExtension + ")|*." + this.m_fileExtension + "|All files (*.*)|*.*";
      openFileDialog.FilterIndex = 1;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        int num = (int) this.m_HLDS.ReadXml(openFileDialog.FileName);
        this.m_RegisterInUseRow = (HourLogDataSet.RegisterInUseRow) this.m_HLDS.RegisterInUse.Rows[0];
        this.m_RegisterUnitRow = (HourLogDataSet.RegisterUnitRow) this.m_HLDS.RegisterUnit.Rows[0];
        this.lblRecords.Text = this.m_HLDS.Registers.Count.ToString();
        this.btnRead.Text = "Start";
        this.SetupCheckableRCBs();
        this.btnSave.Enabled = false;
        this.btnCalculate.Enabled = true;
        this.btnAddToRegs.Enabled = true;
        this.btnSelectedRegisters.Enabled = true;
        this.Progress = 0;
        this.Text = this.m_Title + " | Serial No " + this.m_HLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(openFileDialog.FileName);
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
      if (this.m_HLDS.Registers.Rows.Count < 1)
      {
        int num1 = (int) MessageBox.Show("The Hourly Log is empty.", "No data to save!");
      }
      else
      {
        try
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          if (!Directory.Exists(this.m_DataDir + "\\Registers\\"))
            Directory.CreateDirectory(this.m_DataDir + "\\Registers\\");
          saveFileDialog.InitialDirectory = this.m_DataDir + "\\Registers\\";
          saveFileDialog.Filter = "MC601 Hourly Log (*." + this.m_fileExtension + ")|*." + this.m_fileExtension + "|All files (*.*)|*.*";
          saveFileDialog.FilterIndex = 1;
          saveFileDialog.RestoreDirectory = true;
          if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
          if (saveFileDialog.FileName.IndexOf("." + this.m_fileExtension) < 0)
            saveFileDialog.FileName = saveFileDialog.FileName + "." + this.m_fileExtension;
          this.m_HLDS.WriteXml(saveFileDialog.FileName);
          this.btnSave.Enabled = false;
          this.Text = this.m_Title + " | Serial No " + this.m_HLDS.CustomerNo.GetCustomerNo() + " | " + Path.GetFileName(saveFileDialog.FileName);
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Failed to save the the Hourly Log:" + ex.Message);
        }
      }
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
      byte nUnit1 = (byte) this.m_RegisterUnitRow[registerName1];
      byte nUnit2 = (byte) this.m_RegisterUnitRow[registerName2];
      string columnName = Reg1.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit1) + "] " + CalcRule + " " + Reg2.Caption + " [" + ClsUtils.UnitsForRegisters(nUnit2) + "]";
      dataTable.Columns.Add(columnName, typeof (double));
      foreach (HourLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.m_HLDS.Registers.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] = (object) registersRow.Date;
        Decimal register1 = Convert.ToDecimal(registersRow[registerName1]);
        Decimal register2 = Convert.ToDecimal(registersRow[registerName2]);
        row[columnName] = (object) FrmHourLogSelecter.DoCalculation(register1, register2, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_HLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.ShowTime = true;
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_HLDS.CustomerNo.GetCustomerNo());
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
      foreach (HourLogDataSet.RegistersRow registersRow in (InternalDataCollectionBase) this.m_HLDS.Registers.Rows)
      {
        DataRow row = dataTable.NewRow();
        row["Date"] = (object) registersRow.Date;
        Decimal register1 = Convert.ToDecimal(registersRow[registerName]);
        row[columnName] = (object) FrmHourLogSelecter.DoCalculation(register1, calcValue, CalcRule);
        dataTable.Rows.InsertAt(row, 0);
      }
      FrmMC601RegisterViewer mc601RegisterViewer = new FrmMC601RegisterViewer(dataTable, this.m_HLDS.CustomerNo.GetCustomerNo());
      mc601RegisterViewer.ShowTime = true;
      mc601RegisterViewer.MdiParent = this.MdiParent;
      mc601RegisterViewer.Show();
      if (dataTable.Rows.Count <= 0)
        return;
      FrmGraph frmGraph = new FrmGraph(this.m_HLDS.CustomerNo.GetCustomerNo());
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
          if (calculatedRegister != null && calculatedRegister.UseRegister2 && (calculatedRegister.RegisterName_1 == ur.RegisterName_1 && calculatedRegister.RegisterName_2 == ur.RegisterName_2) && calculatedRegister.CalculationRule == ur.CalculationRule)
          {
            int num = (int) MessageBox.Show("This combination is already created.", "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return;
          }
        }
        this.m_userRegisters.ListOfCalculatedRegisters.Add((object) ur);
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
        this.m_userRegisters.ListOfCalculatedRegisters.Add((object) ur);
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
                if (calculatedRegister.RegisterName_1 == registerCheckBox.RegisterName && calculatedRegister.RegisterName_2 == registerCheckBox.RegisterName2 && (calculatedRegister.UseRegister2 && calculatedRegister.CalculationRule == registerCheckBox.CalculationRule))
                {
                  this.m_userRegisters.ListOfCalculatedRegisters.Remove(obj);
                  registerCheckBox.Dispose();
                  break;
                }
              }
              else if (calculatedRegister.RegisterName_1 == registerCheckBox.RegisterName && calculatedRegister.CalculationValue == registerCheckBox.CalculationValue && (!calculatedRegister.UseRegister2 && calculatedRegister.CalculationRule == registerCheckBox.CalculationRule))
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

    private void SetupDifferenceVolumeEnergy()
    {
      switch (this.m_HourLogType)
      {
        case eHourLogType._6702:
          this.lblcVE.Text = "cE";
          this.lbldVE.Text = "dE";
          this.grpDifferenceVE.Text = "Difference energy";
          this.grpDifferenceVE.Enabled = true;
          break;
        case eHourLogType._6703:
          this.lblcVE.Text = "";
          this.lbldVE.Text = "";
          this.grpDifferenceVE.Enabled = false;
          break;
        case eHourLogType._6705:
          this.lblcVE.Text = "";
          this.lbldVE.Text = "";
          this.grpDifferenceVE.Enabled = false;
          break;
        case eHourLogType._6708:
          this.lblcVE.Text = "";
          this.lbldVE.Text = "";
          this.grpDifferenceVE.Enabled = false;
          break;
        case eHourLogType._6709:
          this.lblcVE.Text = "cV";
          this.lbldVE.Text = "dV";
          this.grpDifferenceVE.Text = "Difference volume";
          this.grpDifferenceVE.Enabled = true;
          break;
        default:
          this.lblcVE.Text = "";
          this.lbldVE.Text = "";
          this.grpDifferenceVE.Enabled = false;
          break;
      }
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      Graphics graphics = pevent.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.White, this.BackColor, LinearGradientMode.Vertical);
      graphics.FillRectangle((Brush) linearGradientBrush, rect);
      linearGradientBrush.Dispose();
    }

    private void btnReadVE_Click(object sender, EventArgs e)
    {
      this.txtdVE.Text = "";
      this.txtcVE.Text = "";
      ArrayList arrRegisters = new ArrayList();
      ArrayList arrValues = new ArrayList();
      ArrayList arrUnits = new ArrayList();
      string ErrorMessage = "";
      int num1 = (int) byte.MaxValue;
      if (this.m_mc601Functions.ReadEeprom((byte) 127, (ushort) 0, (ushort) 1, arrValues, out ErrorMessage) && arrValues.Count > 0)
        num1 = Convert.ToInt32(arrValues[0]);
      if (num1 == 1)
      {
        arrRegisters.Add((object) 178);
        arrRegisters.Add((object) 179);
      }
      else if (num1 == 2)
      {
        arrRegisters.Add((object) 180);
        arrRegisters.Add((object) 181);
      }
      arrValues.Clear();
      if (this.m_mc601Functions.GetData((byte) 127, (byte) 2, arrRegisters, out arrValues, out arrUnits, out ErrorMessage))
      {
        if (arrValues.Count != 2)
          return;
        this.txtdVE.Text = arrValues[0].ToString() + " " + ClsUtils.UnitsForRegisters((byte) arrUnits[0]);
        this.txtcVE.Text = arrValues[1].ToString() + " " + ClsUtils.UnitsForRegisters((byte) arrUnits[1]);
      }
      else
      {
        int num2 = (int) MessageBox.Show("Failed to read the values", "Reading failed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmHourLogSelecter));
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
      this.chkReadRawData = new CheckBox();
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
      this.grpDifferenceVE = new GroupBox();
      this.txtcVE = new TextBox();
      this.txtdVE = new TextBox();
      this.lblcVE = new Label();
      this.lbldVE = new Label();
      this.btnReadVE = new Button();
      this.grpUsed = new GroupBox();
      this.rcbCalc0 = new UCRegisterCheckBox();
      this.rcbE1 = new UCRegisterCheckBox();
      this.rcbE7 = new UCRegisterCheckBox();
      this.rcbE3 = new UCRegisterCheckBox();
      this.rcbE4 = new UCRegisterCheckBox();
      this.rcbE5 = new UCRegisterCheckBox();
      this.rcbE6 = new UCRegisterCheckBox();
      this.rcbE2 = new UCRegisterCheckBox();
      this.rcbV1 = new UCRegisterCheckBox();
      this.rcbV2 = new UCRegisterCheckBox();
      this.rcbInA = new UCRegisterCheckBox();
      this.rcbInB = new UCRegisterCheckBox();
      this.rcbM1 = new UCRegisterCheckBox();
      this.rcbM2 = new UCRegisterCheckBox();
      this.rcbT1 = new UCRegisterCheckBox();
      this.rcbT2 = new UCRegisterCheckBox();
      this.rcbT3 = new UCRegisterCheckBox();
      this.rcbP1 = new UCRegisterCheckBox();
      this.rcbP2 = new UCRegisterCheckBox();
      this.rcbdE = new UCRegisterCheckBox();
      this.rcbcE = new UCRegisterCheckBox();
      this.rcbdV = new UCRegisterCheckBox();
      this.rcbcV = new UCRegisterCheckBox();
      this.rcbInfo = new UCRegisterCheckBox();
      this.rdbCalcB = new RadioButton();
      this.rdbCalcA = new RadioButton();
      this.nudCalc = new NumericUpDown();
      this.grpCalculatedRegisters.SuspendLayout();
      this.grpCalculate.SuspendLayout();
      this.grpGraphs.SuspendLayout();
      this.grpReadLog.SuspendLayout();
      this.grpRegisters.SuspendLayout();
      this.flpRegisters.SuspendLayout();
      this.grpDifferenceVE.SuspendLayout();
      this.grpUsed.SuspendLayout();
      this.nudCalc.BeginInit();
      this.SuspendLayout();
      this.grpCalculatedRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.grpCalculatedRegisters.BackColor = Color.Transparent;
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveAllCalculated);
      this.grpCalculatedRegisters.Controls.Add((Control) this.btnRemoveCalculatedRegister);
      this.grpCalculatedRegisters.Controls.Add((Control) this.flpCalculatedRegisters);
      this.grpCalculatedRegisters.Location = new Point(500, 63);
      this.grpCalculatedRegisters.Name = "grpCalculatedRegisters";
      this.grpCalculatedRegisters.Size = new Size(222, 454);
      this.grpCalculatedRegisters.TabIndex = 16;
      this.grpCalculatedRegisters.TabStop = false;
      this.grpCalculatedRegisters.Text = "Calculated Registers";
      this.btnRemoveAllCalculated.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.btnRemoveAllCalculated.Location = new Point(126, 424);
      this.btnRemoveAllCalculated.Name = "btnRemoveAllCalculated";
      this.btnRemoveAllCalculated.Size = new Size(90, 23);
      this.btnRemoveAllCalculated.TabIndex = 30;
      this.btnRemoveAllCalculated.Text = "Remove All";
      this.btnRemoveAllCalculated.UseVisualStyleBackColor = true;
      this.btnRemoveAllCalculated.Click += new EventHandler(this.btnRemoveAllCalculated_Click);
      this.btnRemoveCalculatedRegister.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnRemoveCalculatedRegister.Location = new Point(6, 424);
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
      this.flpCalculatedRegisters.Size = new Size(216, 398);
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
      this.grpCalculate.Location = new Point(12, 193);
      this.grpCalculate.Name = "grpCalculate";
      this.grpCalculate.Size = new Size(187, 156);
      this.grpCalculate.TabIndex = 15;
      this.grpCalculate.TabStop = false;
      this.grpCalculate.Text = "Calculate";
      this.btnAddToRegs.Enabled = false;
      this.btnAddToRegs.Location = new Point(105, (int) sbyte.MaxValue);
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
      this.grpGraphs.Location = new Point(12, 355);
      this.grpGraphs.Name = "grpGraphs";
      this.grpGraphs.Size = new Size(187, 50);
      this.grpGraphs.TabIndex = 14;
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
      this.grpReadLog.Controls.Add((Control) this.chkReadRawData);
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
      this.grpReadLog.Size = new Size(186, 175);
      this.grpReadLog.TabIndex = 13;
      this.grpReadLog.TabStop = false;
      this.grpReadLog.Text = "Hourly Log";
      this.chkReadRawData.AutoSize = true;
      this.chkReadRawData.Location = new Point(10, 74);
      this.chkReadRawData.Name = "chkReadRawData";
      this.chkReadRawData.Size = new Size(103, 17);
      this.chkReadRawData.TabIndex = 19;
      this.chkReadRawData.Text = "Read Raw Data";
      this.chkReadRawData.UseVisualStyleBackColor = true;
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
      this.btnClear.Location = new Point(105, 97);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new Size(75, 23);
      this.btnClear.TabIndex = 11;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new EventHandler(this.btnClear_Click);
      this.lblRecords.AutoSize = true;
      this.lblRecords.Location = new Point(54, 121);
      this.lblRecords.Name = "lblRecords";
      this.lblRecords.Size = new Size(13, 13);
      this.lblRecords.TabIndex = 10;
      this.lblRecords.Text = "0";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 121);
      this.label3.Name = "label3";
      this.label3.Size = new Size(50, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Records:";
      this.btnSave.Enabled = false;
      this.btnSave.Location = new Point(105, 145);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 7;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnLoad.Location = new Point(6, 145);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new Size(75, 23);
      this.btnLoad.TabIndex = 6;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
      this.btnRead.Location = new Point(6, 97);
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
      this.grpRegisters.Size = new Size(290, 505);
      this.grpRegisters.TabIndex = 12;
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
      this.flpRegisters.Controls.Add((Control) this.rcbV1);
      this.flpRegisters.Controls.Add((Control) this.rcbV2);
      this.flpRegisters.Controls.Add((Control) this.rcbInA);
      this.flpRegisters.Controls.Add((Control) this.rcbInB);
      this.flpRegisters.Controls.Add((Control) this.rcbM1);
      this.flpRegisters.Controls.Add((Control) this.rcbM2);
      this.flpRegisters.Controls.Add((Control) this.rcbT1);
      this.flpRegisters.Controls.Add((Control) this.rcbT2);
      this.flpRegisters.Controls.Add((Control) this.rcbT3);
      this.flpRegisters.Controls.Add((Control) this.rcbP1);
      this.flpRegisters.Controls.Add((Control) this.rcbP2);
      this.flpRegisters.Controls.Add((Control) this.rcbdE);
      this.flpRegisters.Controls.Add((Control) this.rcbcE);
      this.flpRegisters.Controls.Add((Control) this.rcbdV);
      this.flpRegisters.Controls.Add((Control) this.rcbcV);
      this.flpRegisters.Controls.Add((Control) this.rcbInfo);
      this.flpRegisters.Controls.Add((Control) this.btnSelectAll);
      this.flpRegisters.Controls.Add((Control) this.btnSelectNone);
      this.flpRegisters.Dock = DockStyle.Fill;
      this.flpRegisters.FlowDirection = FlowDirection.TopDown;
      this.flpRegisters.Location = new Point(3, 16);
      this.flpRegisters.Name = "flpRegisters";
      this.flpRegisters.Size = new Size(284, 486);
      this.flpRegisters.TabIndex = 17;
      this.btnSelectAll.Location = new Point(157, 138);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new Size(75, 23);
      this.btnSelectAll.TabIndex = 28;
      this.btnSelectAll.Text = "Select All";
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
      this.btnSelectNone.Location = new Point(157, 167);
      this.btnSelectNone.Name = "btnSelectNone";
      this.btnSelectNone.Size = new Size(75, 23);
      this.btnSelectNone.TabIndex = 27;
      this.btnSelectNone.Text = "Select None";
      this.btnSelectNone.UseVisualStyleBackColor = true;
      this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
      this.grpDifferenceVE.BackColor = Color.Transparent;
      this.grpDifferenceVE.Controls.Add((Control) this.txtcVE);
      this.grpDifferenceVE.Controls.Add((Control) this.txtdVE);
      this.grpDifferenceVE.Controls.Add((Control) this.lblcVE);
      this.grpDifferenceVE.Controls.Add((Control) this.lbldVE);
      this.grpDifferenceVE.Controls.Add((Control) this.btnReadVE);
      this.grpDifferenceVE.Location = new Point(12, 411);
      this.grpDifferenceVE.Name = "grpDifferenceVE";
      this.grpDifferenceVE.Size = new Size(186, 104);
      this.grpDifferenceVE.TabIndex = 17;
      this.grpDifferenceVE.TabStop = false;
      this.grpDifferenceVE.Text = "Difference Volume/Energy";
      this.txtcVE.Location = new Point(34, 47);
      this.txtcVE.Name = "txtcVE";
      this.txtcVE.ReadOnly = true;
      this.txtcVE.Size = new Size(146, 20);
      this.txtcVE.TabIndex = 20;
      this.txtdVE.Location = new Point(34, 20);
      this.txtdVE.Name = "txtdVE";
      this.txtdVE.ReadOnly = true;
      this.txtdVE.Size = new Size(146, 20);
      this.txtdVE.TabIndex = 19;
      this.lblcVE.AutoSize = true;
      this.lblcVE.Location = new Point(8, 50);
      this.lblcVE.Name = "lblcVE";
      this.lblcVE.Size = new Size(20, 13);
      this.lblcVE.TabIndex = 18;
      this.lblcVE.Text = "cV";
      this.lbldVE.AutoSize = true;
      this.lbldVE.Location = new Point(8, 23);
      this.lbldVE.Name = "lbldVE";
      this.lbldVE.Size = new Size(20, 13);
      this.lbldVE.TabIndex = 17;
      this.lbldVE.Text = "dV";
      this.btnReadVE.Location = new Point(6, 75);
      this.btnReadVE.Name = "btnReadVE";
      this.btnReadVE.Size = new Size(75, 23);
      this.btnReadVE.TabIndex = 4;
      this.btnReadVE.Text = "Read";
      this.btnReadVE.UseVisualStyleBackColor = true;
      this.btnReadVE.Click += new EventHandler(this.btnReadVE_Click);
      this.grpUsed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.grpUsed.BackColor = Color.Transparent;
      this.grpUsed.Controls.Add((Control) this.rcbCalc0);
      this.grpUsed.Location = new Point(500, 12);
      this.grpUsed.Name = "grpUsed";
      this.grpUsed.Size = new Size(222, 42);
      this.grpUsed.TabIndex = 25;
      this.grpUsed.TabStop = false;
      this.grpUsed.Text = "Change per hour";
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
      this.rcbV1.CalculationRule = "";
      this.rcbV1.CalculationValue = new Decimal(new int[4]);
      this.rcbV1.Caption = "V1";
      this.rcbV1.Checked = false;
      this.rcbV1.IsRegister = true;
      this.rcbV1.Location = new Point(3, 192);
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
      this.rcbV2.Location = new Point(3, 219);
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
      this.rcbInA.Location = new Point(3, 246);
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
      this.rcbInB.Location = new Point(3, 273);
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
      this.rcbM1.Location = new Point(3, 300);
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
      this.rcbM2.Location = new Point(3, 327);
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
      this.rcbT1.Location = new Point(3, 354);
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
      this.rcbT2.Location = new Point(3, 381);
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
      this.rcbT3.Location = new Point(3, 408);
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
      this.rcbP1.CalculationRule = "";
      this.rcbP1.CalculationValue = new Decimal(new int[4]);
      this.rcbP1.Caption = "P1";
      this.rcbP1.Checked = false;
      this.rcbP1.IsRegister = true;
      this.rcbP1.Location = new Point(3, 435);
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
      this.rcbP2.Location = new Point(3, 462);
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
      this.rcbdE.CalculationRule = "";
      this.rcbdE.CalculationValue = new Decimal(new int[4]);
      this.rcbdE.Caption = "dE ";
      this.rcbdE.Checked = false;
      this.rcbdE.IsRegister = true;
      this.rcbdE.Location = new Point(157, 3);
      this.rcbdE.Name = "rcbdE";
      this.rcbdE.RegisterCaption1 = "";
      this.rcbdE.RegisterCaption2 = "";
      this.rcbdE.RegisterId = 178;
      this.rcbdE.RegisterName = "dE";
      this.rcbdE.RegisterName2 = "";
      this.rcbdE.RegisterType = ERegisterType.IsDouble;
      this.rcbdE.Size = new Size(48, 21);
      this.rcbdE.TabIndex = 19;
      this.rcbdE.UnitText = "";
      this.rcbdE.UseRegister2 = true;
      this.rcbcE.CalculationRule = "";
      this.rcbcE.CalculationValue = new Decimal(new int[4]);
      this.rcbcE.Caption = "cE";
      this.rcbcE.Checked = false;
      this.rcbcE.IsRegister = true;
      this.rcbcE.Location = new Point(157, 30);
      this.rcbcE.Name = "rcbcE";
      this.rcbcE.RegisterCaption1 = "";
      this.rcbcE.RegisterCaption2 = "";
      this.rcbcE.RegisterId = 179;
      this.rcbcE.RegisterName = "cE";
      this.rcbcE.RegisterName2 = "";
      this.rcbcE.RegisterType = ERegisterType.IsDouble;
      this.rcbcE.Size = new Size(47, 21);
      this.rcbcE.TabIndex = 18;
      this.rcbcE.UnitText = "";
      this.rcbcE.UseRegister2 = true;
      this.rcbdV.CalculationRule = "";
      this.rcbdV.CalculationValue = new Decimal(new int[4]);
      this.rcbdV.Caption = "dV";
      this.rcbdV.Checked = false;
      this.rcbdV.IsRegister = true;
      this.rcbdV.Location = new Point(157, 57);
      this.rcbdV.Name = "rcbdV";
      this.rcbdV.RegisterCaption1 = "";
      this.rcbdV.RegisterCaption2 = "";
      this.rcbdV.RegisterId = 180;
      this.rcbdV.RegisterName = "dV";
      this.rcbdV.RegisterName2 = "";
      this.rcbdV.RegisterType = ERegisterType.IsDouble;
      this.rcbdV.Size = new Size(48, 21);
      this.rcbdV.TabIndex = 30;
      this.rcbdV.UnitText = "";
      this.rcbdV.UseRegister2 = true;
      this.rcbcV.CalculationRule = "";
      this.rcbcV.CalculationValue = new Decimal(new int[4]);
      this.rcbcV.Caption = "cV";
      this.rcbcV.Checked = false;
      this.rcbcV.IsRegister = true;
      this.rcbcV.Location = new Point(157, 84);
      this.rcbcV.Name = "rcbcV";
      this.rcbcV.RegisterCaption1 = "";
      this.rcbcV.RegisterCaption2 = "";
      this.rcbcV.RegisterId = 181;
      this.rcbcV.RegisterName = "cV";
      this.rcbcV.RegisterName2 = "";
      this.rcbcV.RegisterType = ERegisterType.IsDouble;
      this.rcbcV.Size = new Size(47, 21);
      this.rcbcV.TabIndex = 29;
      this.rcbcV.UnitText = "";
      this.rcbcV.UseRegister2 = true;
      this.rcbInfo.CalculationRule = "";
      this.rcbInfo.CalculationValue = new Decimal(new int[4]);
      this.rcbInfo.Caption = "Info";
      this.rcbInfo.Checked = false;
      this.rcbInfo.IsRegister = true;
      this.rcbInfo.Location = new Point(157, 111);
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
      this.rdbCalcB.AutoSize = true;
      this.rdbCalcB.Location = new Point(6, 103);
      this.rdbCalcB.Name = "rdbCalcB";
      this.rdbCalcB.Size = new Size(14, 13);
      this.rdbCalcB.TabIndex = 29;
      this.rdbCalcB.UseVisualStyleBackColor = true;
      this.rdbCalcB.CheckedChanged += new EventHandler(this.rdbCalcA_CheckedChanged);
      this.rdbCalcA.AutoSize = true;
      this.rdbCalcA.Checked = true;
      this.rdbCalcA.Location = new Point(6, 76);
      this.rdbCalcA.Name = "rdbCalcA";
      this.rdbCalcA.Size = new Size(14, 13);
      this.rdbCalcA.TabIndex = 28;
      this.rdbCalcA.TabStop = true;
      this.rdbCalcA.UseVisualStyleBackColor = true;
      this.rdbCalcA.CheckedChanged += new EventHandler(this.rdbCalcA_CheckedChanged);
      this.nudCalc.DecimalPlaces = 3;
      this.nudCalc.Enabled = false;
      this.nudCalc.Location = new Point(44, 101);
      NumericUpDown numericUpDown = this.nudCalc;
      int[] bits = new int[4];
      bits[0] = 100000;
      Decimal num = new Decimal(bits);
      numericUpDown.Maximum = num;
      this.nudCalc.Name = "nudCalc";
      this.nudCalc.Size = new Size(136, 20);
      this.nudCalc.TabIndex = 27;
      this.nudCalc.TextAlign = HorizontalAlignment.Right;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(734, 524);
      this.Controls.Add((Control) this.grpUsed);
      this.Controls.Add((Control) this.grpDifferenceVE);
      this.Controls.Add((Control) this.grpCalculatedRegisters);
      this.Controls.Add((Control) this.grpCalculate);
      this.Controls.Add((Control) this.grpGraphs);
      this.Controls.Add((Control) this.grpReadLog);
      this.Controls.Add((Control) this.grpRegisters);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(742, 558);
      this.Name = "FrmHourLogSelecter";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Hourly Log Selecter";
      this.Load += new EventHandler(this.frmHourLogSelecter_Load);
      this.grpCalculatedRegisters.ResumeLayout(false);
      this.grpCalculate.ResumeLayout(false);
      this.grpCalculate.PerformLayout();
      this.grpGraphs.ResumeLayout(false);
      this.grpReadLog.ResumeLayout(false);
      this.grpReadLog.PerformLayout();
      this.grpRegisters.ResumeLayout(false);
      this.flpRegisters.ResumeLayout(false);
      this.grpDifferenceVE.ResumeLayout(false);
      this.grpDifferenceVE.PerformLayout();
      this.grpUsed.ResumeLayout(false);
      this.nudCalc.EndInit();
      this.ResumeLayout(false);
    }
  }
}
