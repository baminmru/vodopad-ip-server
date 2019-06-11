namespace XmlImporter
{
    partial class OsmImporter
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
            this.ImportThread = new System.ComponentModel.BackgroundWorker();
            this.tb_xmlpath = new System.Windows.Forms.TextBox();
            this.tb_dbDirectory = new System.Windows.Forms.TextBox();
            this.lbl_xmlPath = new System.Windows.Forms.Label();
            this.lbl_dbDirectory = new System.Windows.Forms.Label();
            this.btn_Import = new System.Windows.Forms.Button();
            this.lbl_ways = new System.Windows.Forms.Label();
            this.lbl_nodes = new System.Windows.Forms.Label();
            this.lbl_roadlinks = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cb_progress = new System.Windows.Forms.CheckBox();
            this.cbox_SpeedUnits = new System.Windows.Forms.ComboBox();
            this.lbl_Speed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ImportThread
            // 
            this.ImportThread.WorkerReportsProgress = true;
            this.ImportThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ImportThread_DoWork);
            this.ImportThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ImportThread_ProgressChanged);
            this.ImportThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ImportThread_RunWorkerCompleted);
            // 
            // tb_xmlpath
            // 
            this.tb_xmlpath.Location = new System.Drawing.Point(19, 37);
            this.tb_xmlpath.Name = "tb_xmlpath";
            this.tb_xmlpath.Size = new System.Drawing.Size(264, 20);
            this.tb_xmlpath.TabIndex = 0;
            // 
            // tb_dbDirectory
            // 
            this.tb_dbDirectory.Location = new System.Drawing.Point(19, 89);
            this.tb_dbDirectory.Name = "tb_dbDirectory";
            this.tb_dbDirectory.Size = new System.Drawing.Size(264, 20);
            this.tb_dbDirectory.TabIndex = 1;
            // 
            // lbl_xmlPath
            // 
            this.lbl_xmlPath.AutoSize = true;
            this.lbl_xmlPath.Location = new System.Drawing.Point(20, 21);
            this.lbl_xmlPath.Name = "lbl_xmlPath";
            this.lbl_xmlPath.Size = new System.Drawing.Size(69, 13);
            this.lbl_xmlPath.TabIndex = 2;
            this.lbl_xmlPath.Text = ".osm file path";
            // 
            // lbl_dbDirectory
            // 
            this.lbl_dbDirectory.AutoSize = true;
            this.lbl_dbDirectory.Location = new System.Drawing.Point(20, 73);
            this.lbl_dbDirectory.Name = "lbl_dbDirectory";
            this.lbl_dbDirectory.Size = new System.Drawing.Size(165, 13);
            this.lbl_dbDirectory.TabIndex = 3;
            this.lbl_dbDirectory.Text = "Output directory for database files";
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(19, 116);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 23);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "Import file";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // lbl_ways
            // 
            this.lbl_ways.AutoSize = true;
            this.lbl_ways.Location = new System.Drawing.Point(20, 151);
            this.lbl_ways.Name = "lbl_ways";
            this.lbl_ways.Size = new System.Drawing.Size(75, 13);
            this.lbl_ways.TabIndex = 5;
            this.lbl_ways.Text = "Imported ways";
            // 
            // lbl_nodes
            // 
            this.lbl_nodes.AutoSize = true;
            this.lbl_nodes.Location = new System.Drawing.Point(20, 177);
            this.lbl_nodes.Name = "lbl_nodes";
            this.lbl_nodes.Size = new System.Drawing.Size(80, 13);
            this.lbl_nodes.TabIndex = 6;
            this.lbl_nodes.Text = "Imported nodes";
            // 
            // lbl_roadlinks
            // 
            this.lbl_roadlinks.AutoSize = true;
            this.lbl_roadlinks.Location = new System.Drawing.Point(20, 202);
            this.lbl_roadlinks.Name = "lbl_roadlinks";
            this.lbl_roadlinks.Size = new System.Drawing.Size(93, 13);
            this.lbl_roadlinks.TabIndex = 7;
            this.lbl_roadlinks.Text = "Imported roadlinks";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 230);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(516, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // cb_progress
            // 
            this.cb_progress.AutoSize = true;
            this.cb_progress.Location = new System.Drawing.Point(308, 39);
            this.cb_progress.Name = "cb_progress";
            this.cb_progress.Size = new System.Drawing.Size(98, 17);
            this.cb_progress.TabIndex = 9;
            this.cb_progress.Text = "Track Progress";
            this.cb_progress.UseVisualStyleBackColor = true;
            // 
            // cbox_SpeedUnits
            // 
            this.cbox_SpeedUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_SpeedUnits.FormattingEnabled = true;
            this.cbox_SpeedUnits.Items.AddRange(new object[] {
            "KPH",
            "MPH"});
            this.cbox_SpeedUnits.Location = new System.Drawing.Point(308, 89);
            this.cbox_SpeedUnits.Name = "cbox_SpeedUnits";
            this.cbox_SpeedUnits.Size = new System.Drawing.Size(121, 21);
            this.cbox_SpeedUnits.TabIndex = 10;
            // 
            // lbl_Speed
            // 
            this.lbl_Speed.AutoSize = true;
            this.lbl_Speed.Location = new System.Drawing.Point(309, 73);
            this.lbl_Speed.Name = "lbl_Speed";
            this.lbl_Speed.Size = new System.Drawing.Size(98, 13);
            this.lbl_Speed.TabIndex = 11;
            this.lbl_Speed.Text = "Default speed units";
            // 
            // OsmImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 266);
            this.Controls.Add(this.lbl_Speed);
            this.Controls.Add(this.cbox_SpeedUnits);
            this.Controls.Add(this.cb_progress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbl_roadlinks);
            this.Controls.Add(this.lbl_nodes);
            this.Controls.Add(this.lbl_ways);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.lbl_dbDirectory);
            this.Controls.Add(this.lbl_xmlPath);
            this.Controls.Add(this.tb_dbDirectory);
            this.Controls.Add(this.tb_xmlpath);
            this.MaximumSize = new System.Drawing.Size(555, 300);
            this.MinimumSize = new System.Drawing.Size(555, 300);
            this.Name = "OsmImporter";
            this.Text = "OSM Importer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker ImportThread;
        private System.Windows.Forms.TextBox tb_xmlpath;
        private System.Windows.Forms.TextBox tb_dbDirectory;
        private System.Windows.Forms.Label lbl_xmlPath;
        private System.Windows.Forms.Label lbl_dbDirectory;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Label lbl_ways;
        private System.Windows.Forms.Label lbl_nodes;
        private System.Windows.Forms.Label lbl_roadlinks;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox cb_progress;
        private System.Windows.Forms.ComboBox cbox_SpeedUnits;
        private System.Windows.Forms.Label lbl_Speed;
    }
}

