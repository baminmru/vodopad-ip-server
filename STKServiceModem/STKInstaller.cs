using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace STKServiceModem
{
    [RunInstaller(true)]
    public partial class STKInstaller : Installer
    {
        public STKInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}