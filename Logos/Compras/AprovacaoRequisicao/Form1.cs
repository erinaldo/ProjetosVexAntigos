using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AprovacaoRequisicao.Library;

namespace AprovacaoRequisicao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAprovacaoRequisicao_Click(object sender, EventArgs e)
        {
            EnviarEmailRequisicao();
            MessageBox.Show("Concluido.");
        }

        private void EnviarEmailRequisicao()
        {
            try
            {
                Requisicao.RequisicoesAguaguardandoAprovacao();
                //MessageBox.Show("Concluido");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void EnviarEmailCotacao()
        {
            try
            {
                Cotacao.CotacaoAguaguardandoAprovacao();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void EnviarEmailAprovacaPedidoDeCompra()
        {
            try
            {
                PedidoDeCompra.PedidoDeCompraAguardndoAprovacao();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = int.Parse(txttempo.Text) * 60000;
            label3.Text = Conexao.stringConexao();
            chamarTodos();

        }

        private void txttempo_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = int.Parse(txttempo.Text) * 60000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chamarTodos();
        }

        private void chamarTodos()
        {

            timer1.Enabled = false;
            
            EnviarEmailRequisicao();
            EnviarEmailCotacao();
            EnviarEmailAprovacaPedidoDeCompra();       
     
            timer1.Enabled = true;
            label2.Text = DateTime.Now.ToString();

            this.Refresh();
        }

        private void btnAprovacaoCotacao_Click(object sender, EventArgs e)
        {
            EnviarEmailCotacao();
            MessageBox.Show("Concluido.");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.Text = this.Text;
            notifyIcon1.BalloonTipTitle = this.Text;
            notifyIcon1.BalloonTipText = "Clique duas vezes no ícone para retornar à aplicação!";
            this.ShowInTaskbar = false;
            notifyIcon1.ShowBalloonTip(0);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnviarEmailAprovacaPedidoDeCompra();
            MessageBox.Show("Concluido.");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        

    }
}
