using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using OsmExplorer.Data;
using OsmExplorer.Units;

namespace XmlImporter
{
    public partial class OsmImporter : Form
    {
        public OsmImporter()
        {
            InitializeComponent();
            tb_dbDirectory.Text = @"D:\Routing\OsmExplorer_V3.3\bin\";
            tb_xmlpath.Text = @"D:\Routing\OsmExplorer_V3.3\bin\demo.osm";

            lbl_roadlinks.Visible = false;
            lbl_ways.Visible = false;
            lbl_nodes.Visible = false;
            cb_progress.Checked = true;
            cbox_SpeedUnits.SelectedIndex = 0;
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            lbl_ways.Text = "Imported ways: 0";
            lbl_nodes.Text = "Imported nodes: 0";
            lbl_roadlinks.Text = "Imported RoadLinks: 0";

            lbl_nodes.Visible = true;
            lbl_roadlinks.Visible = true;
            lbl_ways.Visible = true;

            SpeedUnits defaultUnits;
            switch (cbox_SpeedUnits.Text) 
            {
                case "KPH":
                    defaultUnits = SpeedUnits.KPH;
                    break;
                case "MPH":
                    defaultUnits = SpeedUnits.MPH;
                    break;
                default:
                    defaultUnits = SpeedUnits.KPH;
                    break;
            }
            if (!ImportThread.IsBusy)
                ImportThread.RunWorkerAsync(new object[] { tb_xmlpath.Text, tb_dbDirectory.Text, cb_progress.Checked, defaultUnits });
        }

        private void ImportProgress(int progress, OsmXmlImporter.ImportStatus importStatus) 
        {
            ImportThread.ReportProgress(progress, importStatus);
        }

        private void ImportThread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] args = e.Argument as object[];
                FileInfo xmlPath = new FileInfo(args[0] as string);
                DirectoryInfo dInfo = new DirectoryInfo(args[1] as string);
                bool reportProgress = (bool)args[2];
                SpeedUnits defaultUnits = (SpeedUnits)args[3];

                OsmXmlImporter import = new OsmXmlImporter(xmlPath, dInfo);
                import.DefaultSpeedUnits = defaultUnits;
                import.ImportProgress = ImportProgress;
                import.CreatePrimitiveDatabase(reportProgress);
                import.CreateSpatialDatabase();
            }
            catch (IOException ex1)
            {
                MessageBox.Show(ex1.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (XmlException ex2) //Bad XML file or (more likely) buggy import code;
            {
                MessageBox.Show(ex2.Message, "Xml Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Volante.DatabaseException ex3) //Database exceptions not figured out yet.
            {
                MessageBox.Show(ex3.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ImportThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OsmXmlImporter.ImportStatus status = (OsmXmlImporter.ImportStatus)e.UserState;
            switch (status.State)
            {
                case OsmXmlImporter.ImportState.Complete:
                    lbl_ways.Text = "Import Complete.";
                    lbl_nodes.Visible = false;
                    lbl_roadlinks.Visible = false;
                    break;
                case OsmXmlImporter.ImportState.CreatingRoutingDatabase:
                    lbl_ways.Text = "Creating Routing Database: " + status.Count;
                    lbl_nodes.Visible = false;
                    lbl_roadlinks.Visible = false;
                    break;
                case OsmXmlImporter.ImportState.ImportingNodes:
                    lbl_nodes.Text = "Imported nodes: " + status.Count;
                    break;
                case OsmXmlImporter.ImportState.ImportingRoadLinks:
                    lbl_roadlinks.Text = "Imported RoadLinks: " + status.Count;
                    break;
                case OsmXmlImporter.ImportState.ImportingWays:
                    lbl_ways.Text = "Imported ways: " + status.Count.ToString();
                    break;
                case OsmXmlImporter.ImportState.UpdatingSpatialIndex:
                    lbl_ways.Text = "Updating Spatial Index: " + status.Count;
                    lbl_nodes.Visible = false;
                    lbl_roadlinks.Visible = false;
                    break;
            }
            this.progressBar1.Value = e.ProgressPercentage;
        }
        private void ImportThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Value = 0;
        }
    }
}
