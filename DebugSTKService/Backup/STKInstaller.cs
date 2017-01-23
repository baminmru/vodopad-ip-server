using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace STKService
{
    [RunInstaller(true)]
    public partial class STKInstaller : Installer
    {
        public STKInstaller()
        {
            InitializeComponent();
        }
    }
}