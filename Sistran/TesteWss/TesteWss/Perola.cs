using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace TesteWss
{
    public partial class Perola : Form
    {
        public Perola()
        {
            InitializeComponent();
        }

        private void Perola_Load(object sender, EventArgs e)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "exec PRC_RetornarNfeOcorrenciasComImagens ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql.ToUpper(), cnx);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                File.WriteAllBytes("c:\\Perola\\" + dt.Rows[i]["Numero"].ToString().Trim() + "_" + dt.Rows[i]["Codigo"].ToString().Trim() + "_" + dt.Rows[i]["Serie"].ToString().Trim() + ".jpg", (byte[])dt.Rows[i]["Arquivo"]);
                    
            }

        }
    }
}
