using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace RepService
{
    [RunInstaller(true)]
    public partial class RepServicetInstaller : System.Configuration.Install.Installer
    {
        public RepServicetInstaller()
        {
            InitializeComponent();
        }
    }
}
