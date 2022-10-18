using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Sistran.Library;

namespace Robo.Email.Notas.WindowsServices
{
    public partial class RoboNF : ServiceBase
    {
        public RoboNF()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
