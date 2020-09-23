using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace ATMServiceNS
{
    [RunInstaller(true)]
    public partial class RInstaller : Installer
    {
        public RInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}