using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teste.Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("conexoes");

            dt.Columns.Add("cd_cliente");
            dt.Columns.Add("conexao");


            DataRow dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "cn1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "cn2";
            dt.Rows.Add(dr);

            dt.WriteXml("m:\\conexoes.xml");
        }
    }
}
