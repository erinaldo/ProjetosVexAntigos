using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;
using RsMobile.Classes;

namespace RsMobile
{
    public partial class ListarStatus : Form
    {
        DataTable dt;
        public ListarStatus()
        {
            InitializeComponent();
        }
        string _cdCliente;
        string _chave;
        public ListarStatus(string cd_cliente, string chave)
        {
            _cdCliente = cd_cliente;
            _chave = chave;
            InitializeComponent();

        }

        private void ListarStatus_Load(object sender, EventArgs e)
        {
            DataTable dtAparelho = new Bb().RetornarInformacoesAparelho();

            if (dtAparelho.Rows.Count > 0)
                tmEnvio.Interval = int.Parse((dtAparelho.Rows[0]["TEMPO"].ToString() == "" ? "5" : dtAparelho.Rows[0]["TEMPO"].ToString())) * 60000;


            carregar();
            timer1.Enabled = true;
            tmEnvio.Enabled = true;         
        }

        private void btnPendentes_Click(object sender, EventArgs e)
        {
            if (int.Parse(dt.Compute("Count(IDDOCUMENTO)", "Pendente='S'").ToString()) == 0)
                return;

            pnlBotoes.Visible = false;
            pnlGrid.Visible = true;

            DataRow[] Pend = dt.Select("Pendente='S'", "numero asc");
            DataTable dtPend = new DataTable();

            dtPend.Columns.Add(new DataColumn("IdDocumento", typeof(string)));
            dtPend.Columns.Add(new DataColumn("NUMERODOCUMENTO", typeof(string)));
            dtPend.Columns.Add(new DataColumn("Remetente", typeof(string)));
            dtPend.Columns.Add(new DataColumn("Destinatario", typeof(string)));


            for (int i = 0; i < Pend.Length; i++)
            {
                DataRow or = dtPend.NewRow();
                or[0] = Pend[i]["IDDOCUMENTO"].ToString();
                or[1] = Pend[i]["NUMERODOCUMENTO"].ToString();
                or[2] = Pend[i]["REMETENTE"].ToString();
                or[3] = Pend[i]["DESTINATARIO"].ToString();
                dtPend.Rows.Add(or);
            }
            grd.DataSource = dtPend;
            this.Text = "Pendentes";
        }

        private void btnATransmitir_Click(object sender, EventArgs e)
        {
            if (int.Parse(dt.Compute("Count(IDDOCUMENTO)", "Pendente='N' and Transmitido='N'").ToString()) == 0)
                return;

            pnlBotoes.Visible = false;
            pnlGrid.Visible = true;

            DataRow[] Pend = dt.Select("Pendente='N' and Transmitido='N'", "numero asc");
            DataTable dtPend = new DataTable();

            dtPend.Columns.Add(new DataColumn("IdDocumento", typeof(string)));
            dtPend.Columns.Add(new DataColumn("NUMERODOCUMENTO", typeof(string)));
            dtPend.Columns.Add(new DataColumn("Remetente", typeof(string)));
            dtPend.Columns.Add(new DataColumn("Destinatario", typeof(string)));


            for (int i = 0; i < Pend.Length; i++)
            {
                DataRow or = dtPend.NewRow();
                or[0] = Pend[i]["IDDOCUMENTO"].ToString();
                or[1] = Pend[i]["NUMERODOCUMENTO"].ToString();
                or[2] = Pend[i]["REMETENTE"].ToString();
                or[3] = Pend[i]["DESTINATARIO"].ToString();
                dtPend.Rows.Add(or);
            }
            grd.DataSource = dtPend;
            this.Text = "A Transmitir";
        }

        private void btnEfetuados_Click(object sender, EventArgs e)
        {
            if (int.Parse(dt.Compute("Count(IDDOCUMENTO)", "Pendente='N'").ToString()) == 0)
                return;

            pnlBotoes.Visible = false;
            pnlGrid.Visible = true;

            DataRow[] Pend = dt.Select("Pendente='N'", "numero asc");
            DataTable dtPend = new DataTable();

            dtPend.Columns.Add(new DataColumn("IdDocumento", typeof(string)));
            dtPend.Columns.Add(new DataColumn("NUMERODOCUMENTO", typeof(string)));
            dtPend.Columns.Add(new DataColumn("Remetente", typeof(string)));
            dtPend.Columns.Add(new DataColumn("Destinatario", typeof(string)));

            for (int i = 0; i < Pend.Length; i++)
            {
                DataRow or = dtPend.NewRow();
                or[0] = Pend[i]["IDDOCUMENTO"].ToString();
                or[1] = Pend[i]["NUMERODOCUMENTO"].ToString();
                or[2] = Pend[i]["REMETENTE"].ToString();
                or[3] = Pend[i]["DESTINATARIO"].ToString();
                dtPend.Rows.Add(or);
            }
            grd.DataSource = dtPend;
            this.Text = "Efetuados";
        }

        private void btnVoltarListaStatus_Click(object sender, EventArgs e)
        {
            pnlGrid.Visible = false;
            pnlBotoes.Visible = true;
            this.Text = "RS Mobile";
        }

        private void grd_DoubleClick(object sender, EventArgs e)
        {            
            int row = grd.CurrentCell.RowNumber;
            DetalheNota f = new DetalheNota(grd[row, 3].ToString(), _cdCliente, _chave);
            f.Show();
            this.Hide();

        }

        private void grd_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void ListarStatus_Activated(object sender, EventArgs e)
        {
            //DataTable dtAparelho = new Bb().RetornarInformacoesAparelho();

            //if (dtAparelho.Rows.Count > 0)
            //    tmEnvio.Interval = int.Parse((dtAparelho.Rows[0]["TEMPO"].ToString() == "" ? "5" : dtAparelho.Rows[0]["TEMPO"].ToString())) * 60000;


            //carregar();
            //timer1.Enabled = true;
            //tmEnvio.Enabled = true;
            
        }

        private void carregar()
        {
            dt = new Bb().RetornarAllDocumentos();
            btnPendentes.Text = "Pendentes: " + dt.Compute("Count(IDDOCUMENTO)", "Pendente='S'").ToString();
            btnATransmitir.Text = "A Transferir: " + dt.Compute("Count(IDDOCUMENTO)", "Pendente='N' and Transmitido='N' ").ToString();
            btnEfetuados.Text = "Efetuados: " + dt.Compute("Count(IDDOCUMENTO)", "Pendente='N'").ToString();

            //lblStatusEnvio.Text = "";
            //this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GerenciaThread t = new GerenciaThread();
            t.ChamarThread(_cdCliente, "N", _chave);            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {              
                GerenciaThread t = new GerenciaThread();
                t.ChamarThread(_cdCliente,"N", _chave);                
                
            }
            catch (Exception)
            {             
            }
        }
                
        private void timer1_Tick_1(object sender, EventArgs e)
        {           
            carregar();            
        }

        private void ListarStatus_Closing(object sender, CancelEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void ListarStatus_Closed(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void ListarStatus_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void tmEnvio_Tick(object sender, EventArgs e)
        {
            lblStatusEnvio.Text = "Enviando Dados";
            this.Refresh();
            tmEnvio.Enabled = false;
            GerenciaThread t = new GerenciaThread();
            t.ChamarThread(_cdCliente, "N", _chave);
            tmEnvio.Enabled = true;

            lblStatusEnvio.Text = "Última Tentativa de Envio: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            this.Refresh();

        }

        private void btnAlterarDt_Click(object sender, EventArgs e)
        {
            Inicial frm = new Inicial();
            this.Hide();
            frm.Show();
        }

        private void grd_Click(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            DetalheNota f = new DetalheNota(grd[row, 3].ToString(), _cdCliente, _chave);
            f.Show();
            this.Hide();
        }
    }
}