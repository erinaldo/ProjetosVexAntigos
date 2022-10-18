using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sistran.Library;



namespace ServicoKPIIrwin.Wind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {      
            SistranDAO.KPI_Irwin kpi = new SistranDAO.KPI_Irwin();           
            kpi.ExecutarScripts();
            Application.Exit();
        }
    }
}
