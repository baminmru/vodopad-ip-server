using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Configuration;

namespace BamiDriverTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Client;
            application.ConfigSectionName = "BamiDriverTest";
            

            try
            {
                // process and command line arguments.
                //if (application.ProcessCommandLine())
                //{
                //    return;
                //}

                // load the application configuration.
                application.LoadApplicationConfiguration(false);
               
                // check the application certificate.
                //application.CheckApplicationInstanceCertificate(false, 0);

                //application.ApplicationConfiguration.SecurityConfiguration.AutoAcceptUntrustedCertificates = true;

                // run the application interactively.
                Application.Run(new Form1(application.ApplicationConfiguration));
            }
            catch (Exception e)
            {
                ExceptionDlg.Show(application.ApplicationName, e);
                return;
            }

            
        }
    }
}
