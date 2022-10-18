using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TesteWss.localhost;
using System.IO;


namespace TesteWss
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                localhost.HR HR = new localhost.HR();
                List<localhost.Pedido> lped = new List<localhost.Pedido>();

                for (int ii = 0; ii < 1; ii++)
                {
                    localhost.Pedido ped = new localhost.Pedido();

                    ped.Cliente_CNPJ = "19364681000103";

                    ped.Dest_CNPJCPF = "44476893830";
                    ped.Dest_RAZAOSOCIAL = "MURILO HENRIQUE PEREIRA";
                    ped.Dest_FANTASIA = "MURILO HENRIQUE PEREIRA";
                    ped.Dest_IERG = "ISENTO";

                    

                    ped.PrimeiraEntrega = false;
                    ped.OcorrenciasPedidosAnteriores = new string[] { "Atraso no refill", "Atraso no refill" };

                    
                    ped.DataDeEmissao = DateTime.Now;
                    ped.DataParaEntrega = "2017-12-02";
                    ped.NumeroDocumento = "103991";
                    ped.Serie = "1";



                    ped.TipoDeDocumento = "PEDIDO";
                    //ped.Dest_BAIRRO = "Jd. Santo Antonio";
                    //ped.Dest_CEP = "04619002";
                    //ped.Dest_CIDADE_NOME = "Osasco";
                   // ped.Dest_COD_IBGE_CIDADE = "3550308";
                    ped.Dest_ENDERECO = "RUA 2";                   
                    ped.Dest_NUMERO = "34";
                    ped.Dest_COMPLEMENTO = "Casa ";


                    ped.Dest_ENTREGA_ENDERECO = "RUA 2";
                    ped.Dest_ENTREGA_NUMERO = "34";
                    ped.Dest_ENTREGA_COMPLEMENTO = "CASA 2";
                    //ped.Dest_ENTREGA_BAIRRO = "Jd. Santo Antonio";
                    //ped.Dest_ENTREGA_CEP = "04619002";
                    //ped.Dest_ENTREGA_COD_IBGE_CIDADE = "3550308";

                    ped.PeriodoDeEntregaInicio = "2017-12-02-08:00";
                    ped.PeriodoDeEntregaFim = "2017-12-02-20:00";

                    ped.CompraVenda = "VENDA";
                    //ped.DataParaEntrega = "2017-09-12";
                    ped.Latitude = "-22.8630864";
                    ped.Longitude = "-47.1419204";
                    ped.Dest_Telefone = "19 993532201";
                    ped.Dest_Email = "murilo-henreque@bol.com.br";                  	
                    



                    List<localhost.Itens> lit = new List<localhost.Itens>();

                    Itens it = new Itens();
                    it.SKU = "1530138367";
                    it.EAN = "7891150054585";
                    it.Descricao = "AMACIANTE CONCENTRADO COMFORT TRADICIONAL REFIL 900ML";
                    it.PesoLiquido = decimal.Parse("0");
                    it.Quantidade = 1;
                    it.ValorUnitario = decimal.Parse("11.91");
                    //it.CodigoNCM = "";
                    lit.Add(it);


                    ped.itens = lit.ToArray();
                    lped.Add(ped);

                }

                MessageBox.Show(HR.ReceberPedidosStatus("321500", "321500", lped.ToArray(), "Homologacao", "PREVISAO"));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private void Cancelar()
        {
            localhost.HR o = new localhost.HR();
            localhost.PedidoCancelado ped = new localhost.PedidoCancelado();
            ped.Cliente_CNPJ = "19364681000103";
            ped.CompraVenda = "Compra";
            ped.NumeroDocumento = "3094053";
            ped.Serie = "1";
            ped.Dest_CNPJCPF = "61.068.276/0102-40";
            MessageBox.Show(o.CancelarPedidos("homerefill", "hr2015", ped, "PRODUCAO"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                localhost.HR o = new localhost.HR();
                DataTable dt = o.PosicaoEstoque("homerefill", "");
                dataGridView1.DataSource = dt;

                //var x = o.ProdutosAtivos("homerefill", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                localhost.HR o = new localhost.HR();
                DataTable dt = o.Entradas("homerefill", "", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                localhost.HR o = new localhost.HR();
                var x = o.ProdutosAtivos("homerefill", "");
                dataGridView1.DataSource = x;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                localhost.HR o = new localhost.HR();
                var x = o.SaldoAtual("homerefill", "");
                dataGridView1.DataSource = x;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnviarXml_Click(object sender, EventArgs e)
        {
            byte[] arrBytes = File.ReadAllBytes(@"D:\PenDrive\35160845543915003288550010003222691389930947.xml");        

            localhost.HR o = new localhost.HR();
            MessageBox.Show( o.ReceberXml("35160845543915003288550010003222691389930947", arrBytes, "Entrada"));

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //br.com.grupologos.www.wsLoginApp wss = new br.com.grupologos.www.wsLoginApp();
            //DataTable d = wss.Logar("mrandrade", "15");
        }
    }
}