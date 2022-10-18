using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RsMobile
{
    public partial class DetalheNota : Form
    {
        public string _IdDocumento;
        public string _cd_cliente;
        public string _chave;
        public DetalheNota()
        {
            InitializeComponent();
        }

        public DetalheNota(string IdDocumento, string cd_cliente, string chave)
        {
            _IdDocumento = IdDocumento;
            _cd_cliente = cd_cliente;
            _chave = chave;
            InitializeComponent();
        }

        private void DetalheNota_Load(object sender, EventArgs e)
        {
            DataTable dt = new Bb().RetornarDocumentos(_IdDocumento);
            lblNumero.Text = dt.Rows[0]["NumeroDocumento"].ToString();
            lblDestinatario.Text = dt.Rows[0]["Destinatario"].ToString();
            lblEndereco.Text = dt.Rows[0]["ENDERECO"].ToString() + "," + dt.Rows[0]["NUMERO"].ToString() + "-" + dt.Rows[0]["CIDADE"].ToString() + " - " + dt.Rows[0]["CIDADE"].ToString();
            lblPeso.Text = dt.Rows[0]["PESOBRUTO"].ToString();
            lblVolumes.Text = dt.Rows[0]["VOLUMES"].ToString();
        }

        private void btnVoltarListaStatus_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ocorrencia F = new Ocorrencia(_IdDocumento, _cd_cliente,_chave);
            this.Close();
            F.Show();
        }

        private void btnVoltarListaStatus_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}