namespace BamiDriverTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdRead = new System.Windows.Forms.Button();
            this.gv = new System.Windows.Forms.DataGridView();
            this.cmdReadOne = new System.Windows.Forms.Button();
            this.txtUID = new System.Windows.Forms.TextBox();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.btnCurrent = new System.Windows.Forms.Button();
            this.btnTotals = new System.Windows.Forms.Button();
            this.btnHour = new System.Windows.Forms.Button();
            this.btnDay = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtHour = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(12, 12);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(118, 43);
            this.cmdRead.TabIndex = 0;
            this.cmdRead.Text = "ReadData";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.AllowUserToDeleteRows = false;
            this.gv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Location = new System.Drawing.Point(12, 181);
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            this.gv.Size = new System.Drawing.Size(841, 152);
            this.gv.TabIndex = 2;
            // 
            // cmdReadOne
            // 
            this.cmdReadOne.Location = new System.Drawing.Point(12, 61);
            this.cmdReadOne.Name = "cmdReadOne";
            this.cmdReadOne.Size = new System.Drawing.Size(118, 43);
            this.cmdReadOne.TabIndex = 3;
            this.cmdReadOne.Text = "ReadOneNode";
            this.cmdReadOne.UseVisualStyleBackColor = true;
            this.cmdReadOne.Click += new System.EventHandler(this.cmdReadOne_Click);
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(151, 73);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(408, 20);
            this.txtUID.TabIndex = 4;
            this.txtUID.Text = "ns=2;s=GIUSController.0a8b6111-05be-4e56-9dce-440ae24d72a0.1.History.A1";
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(151, 24);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.Size = new System.Drawing.Size(384, 20);
            this.txtRoot.TabIndex = 5;
            this.txtRoot.Text = "ns=2;s=GIUSController.0a8b6111-05be-4e56-9dce-440ae24d72a0";
            // 
            // btnCurrent
            // 
            this.btnCurrent.Location = new System.Drawing.Point(12, 110);
            this.btnCurrent.Name = "btnCurrent";
            this.btnCurrent.Size = new System.Drawing.Size(118, 43);
            this.btnCurrent.TabIndex = 6;
            this.btnCurrent.Text = "Current";
            this.btnCurrent.UseVisualStyleBackColor = true;
            this.btnCurrent.Click += new System.EventHandler(this.btnCurrent_Click);
            // 
            // btnTotals
            // 
            this.btnTotals.Location = new System.Drawing.Point(136, 110);
            this.btnTotals.Name = "btnTotals";
            this.btnTotals.Size = new System.Drawing.Size(118, 43);
            this.btnTotals.TabIndex = 7;
            this.btnTotals.Text = "Totals";
            this.btnTotals.UseVisualStyleBackColor = true;
            this.btnTotals.Click += new System.EventHandler(this.btnTotals_Click);
            // 
            // btnHour
            // 
            this.btnHour.Location = new System.Drawing.Point(260, 110);
            this.btnHour.Name = "btnHour";
            this.btnHour.Size = new System.Drawing.Size(118, 43);
            this.btnHour.TabIndex = 8;
            this.btnHour.Text = "Hour";
            this.btnHour.UseVisualStyleBackColor = true;
            this.btnHour.Click += new System.EventHandler(this.btnHour_Click);
            // 
            // btnDay
            // 
            this.btnDay.Location = new System.Drawing.Point(384, 110);
            this.btnDay.Name = "btnDay";
            this.btnDay.Size = new System.Drawing.Size(118, 43);
            this.btnDay.TabIndex = 9;
            this.btnDay.Text = "Day";
            this.btnDay.UseVisualStyleBackColor = true;
            this.btnDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(525, 119);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(138, 20);
            this.dtpDate.TabIndex = 10;
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(669, 119);
            this.txtHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(46, 20);
            this.txtHour.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 345);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnDay);
            this.Controls.Add(this.btnHour);
            this.Controls.Add(this.btnTotals);
            this.Controls.Add(this.btnCurrent);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.cmdReadOne);
            this.Controls.Add(this.gv);
            this.Controls.Add(this.cmdRead);
            this.Name = "Form1";
            this.Text = "Bami OPC Driver Test";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.Button cmdReadOne;
        private System.Windows.Forms.TextBox txtUID;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.Button btnCurrent;
        private System.Windows.Forms.Button btnTotals;
        private System.Windows.Forms.Button btnHour;
        private System.Windows.Forms.Button btnDay;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.NumericUpDown txtHour;
    }
}

