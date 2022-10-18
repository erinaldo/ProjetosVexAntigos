using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class CLW00001 : Form
    {
        public CLW00001()
        {
            InitializeComponent();
        }

        private void CLW00001_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
        }

        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtUa.Text !="")
            {
                e.Handled = true;
                PesquisarUA();
            }
        }

        DataTable dt;
        private void PesquisarUA()
        {
            dt = new Classes.BLL.UnidadeDeArmazenagem().LerUnidadeDeArmazenagem(txtUa.Text);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Pallet não Encontrado", "Atenção");
                txtUa.Text = "";
                txtUa.Focus();
                lblCodigoDeBarras.Text = "";
                lblEnderecoUa.Text = "";
                lblProduto.Text = "";
            }
            else
            {

                if (int.Parse(float.Parse(dt.Rows[0]["SALDO"].ToString()).ToString()) == 0)
                {
                    MessageBox.Show("Não é possivel mudar a localização de uma UA Vazia.", "Atenção");
                    txtUa.Text = "";
                    txtUa.Focus();
                    lblCodigoDeBarras.Text = "";
                    lblEnderecoUa.Text = "";
                    lblProduto.Text = "";
                }

                txtDestino.Enabled = true;
                txtDestino.Focus();
                lblCodigoDeBarras.Text = dt.Rows[0]["CODIGODEBARRAS"].ToString();
                lblEnderecoUa.Text = dt.Rows[0]["ENDERECO"].ToString(); ;
                lblProduto.Text = dt.Rows[0]["DESCRICAO"].ToString();
                idProdutoCliente = dt.Rows[0]["IDPRODUTOCLIENTE"].ToString();
            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {

            try
            {
                new Classes.BLL.UnidadeDeArmazenagem().AlterarEnderecoUa(int.Parse(idProdutoCliente), txtDestino.Text, dt.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString());          
                MessageBox.Show("Operação Efetuada com Sucesso!", "Coletor Sistecno");
                txtDestino.Enabled = false;
                txtDestino.Text = "";
                txtUa.Focus();
                txtUa.Text = "";
                lblCodigoDeBarras.Text = "";
                lblEnderecoUa.Text = "";
                lblProduto.Text = "";
                btnConfirmar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique o endereço de Destino. O Endereço esta configurado para não Aceitar Multiplas Uas.");
            }
        }

        private void txtDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtDestino.Text != "")
            {
                e.Handled = true;
                VerificarDestino();
            }
        }

        string idProdutoCliente = "0";

        private void VerificarDestino()
        {
            DataTable dt = new Classes.BLL.DepositoPlantaLocalizacao().RetornarDepositoPlantaLocalizacao(txtDestino.Text);

            if (dt.Rows.Count > 0)
            {
                //idProdutoCliente = dt.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                if (dt.Rows[0]["MultiplosProdutos"].ToString() == "NAO")
                {
                    if (dt.Rows[0]["IDPRODUTOCLIENTE"].ToString() != "")//nao esta vazia
                    {
                        MessageBox.Show("Endereço não aceita multiplos produtos ou existe outros produtos.", "Atenção");
                        txtDestino.Enabled = false;
                        txtDestino.Text = "";
                        btnConfirmar.Enabled = false;
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Endereço não encontrado", "Atenção");
                return;
            }

            if (Helpers.EnderecoVazio(txtDestino.Text))
            {

                btnConfirmar.Enabled = true;
                btnConfirmar.Focus();
            }
            else
            {
                MessageBox.Show("Endereço de Destino já possui Ua.", "Atenção");
                
                txtDestino.Text = "";
                btnConfirmar.Enabled = false;

                return;
            }

        }
    }
}
