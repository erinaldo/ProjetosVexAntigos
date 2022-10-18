using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TesteWss
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }


        public void processar()
        {
            wssRoge.ReposcicaoRoge ws = new wssRoge.ReposcicaoRoge();

            for (int i = 0; i < 5; i++)
            {
                label1.Text = (i + 1).ToString();
                Application.DoEvents();

                List<wssRoge.Volumes> vol = new List<wssRoge.Volumes>();
                for (int ivol = 0; ivol < 5; ivol++)
                {
                    wssRoge.Volumes v = new wssRoge.Volumes();
                    v.CodigoBarras = "VOLUME" + ivol.ToString();

                    vol.Add(v);
                }

                List<wssRoge.Itens> item = new List<wssRoge.Itens>();

                for (int ivol = 0; ivol < 5; ivol++)
                {
                    wssRoge.Itens it = new wssRoge.Itens();
                    it.CodigoRoge = ivol;
                    it.Descricao = "Desc" + ivol;
                    it.Quantidade = i + 2;
                    it.Valor = "425";

                    wssRoge.CodigoBarras[] x = new wssRoge.CodigoBarras[10];

                    for (int ix = 0; ix < 10; ix++)
                    {
                        wssRoge.CodigoBarras XX = new wssRoge.CodigoBarras();
                        XX.CodigoDeBarras = "ITEM " + ix.ToString();
                        XX.Embalagem = "CX ";
                        XX.EmbalagemQuantidade = (ix + 1).ToString();
                       // XX.Valor = "450";
                        x[ix] = XX;
                    }

                    it.ItensCodigoBarras = x;
                    item.Add(it);
                }
                string ret = ws.GravarDocumento("NOTA_T_CFESTE" + i.ToString(), (i + 1).ToString(), "reg" + (i + 1).ToString(), "Moises", vol.ToArray(), item.ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblInicio.Text = DateTime.Now.ToString();

            Application.DoEvents();
           // processar();

            Cancelar();
            Application.DoEvents();
            lblFim.Text = DateTime.Now.ToString();
            MessageBox.Show("fim");
        }

        private void Cancelar()
        {

            localhost.HR o = new localhost.HR();
            localhost.PedidoCancelado ped = new localhost.PedidoCancelado();
            ped.Cliente_CNPJ = "19.364.681/0001-03";
            ped.CompraVenda = "VENDA";
            ped.NumeroDocumento = "9999980";
            ped.Serie = "PED";
            ped.Dest_CNPJCPF = "23.274.712/0001-02";
            o.CancelarPedidos("homerefill", "hr2015", ped, "");
            
        }
    }
}