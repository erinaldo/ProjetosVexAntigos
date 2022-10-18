using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MigracaoOcorrencias.ClassLibrary;

namespace MigracaoOcorrencias.WindForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            this.Refresh();
            
            string BaseOrigem = System.Configuration.ConfigurationSettings.AppSettings["BaseOrigem"];
            string BaseDestino = System.Configuration.ConfigurationSettings.AppSettings["BaseDestino"];

            //new MigracaoOcorrencias.ClassLibrary.Iniciar().IniciarProcessoNotasFicaisNaoEntregue(BaseOrigem, BaseDestino);
            new MigracaoOcorrencias.ClassLibrary.Iniciar().iniciarProcessoImportacao(BaseOrigem,BaseDestino);

            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            
            string BaseOrigem = System.Configuration.ConfigurationSettings.AppSettings["BaseOrigem"];
            string BaseDestino = System.Configuration.ConfigurationSettings.AppSettings["BaseDestino"];
            new MigracaoOcorrencias.ClassLibrary.Iniciar().iniciarProcessoImportacao(BaseOrigem, BaseDestino);

            //List<UPDATESS> list = new MigracaoOcorrencias.ClassLibrary.Iniciar().IniciarProcessoNotasFicaisNaoEntregue(BaseOrigem, BaseDestino);
                      


            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
