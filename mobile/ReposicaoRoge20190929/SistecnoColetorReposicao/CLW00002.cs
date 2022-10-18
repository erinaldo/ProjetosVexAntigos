using System;
using System.Data;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class CLW00002 : Form
    {
        DataTable dt;

        public CLW00002()
        {
            InitializeComponent();
        }

        private void CLW00002_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
        }

        private void txtEnderecoProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtEnderecoProduto.Text != "")
                    Pesquisar();
            }
            catch (Exception)
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        private void Pesquisar()
        {
            lblMetodoMovimento.Text = "";
            lblIdProdutoCliente.Text = "";
            lblEnderecoPicking.Text = "";
            lblEnderecoPicking.Text = "";
            lblIdEnderecoPicking.Text = "";
            lblIdEnderecoOrigem.Text = "";

            lblEndecoOrigem.Text = "";
            lblUaOrigem.Text = "";
            txtLeituraDaUa.Text = "";
            txtLeituraEndereco.Text = "";

            grd.DataSource = null;
            btnConfirmar.Enabled = false;
            grd.Enabled = true;

            dt = new Classes.BLL.Produto().RetornarProdutosDoEnderecoPicking(txtEnderecoProduto.Text);
            grd.DataSource = dt;
                      

            switch (dt.Rows.Count)
            {
                case 0:
                    MessageBox.Show("Nenhum Item Encontrado", "SISTECNO COLETOR");                    
                    break;

                case 1:
                    lblMetodoMovimento.Text = dt.Rows[0]["METODODEMOVIMENTACAO"].ToString();
                    lblIdProdutoCliente.Text = dt.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    lblEnderecoPicking.Text = dt.Rows[0]["Endereco"].ToString();
                    lblIdEnderecoPicking.Text = dt.Rows[0]["IDDepositoPlantaLocalizacao"].ToString();

                    if (dt.Rows[0]["ENDERECO"].ToString() == "")
                    {
                        MessageBox.Show("Produto sem Endereço de Picking Cadastrado.", "SISTECNO COLETOR");
                    }

                    string[] ret = new Classes.BLL.DepositoPlantaLocalizacao().RetornarEndereco(lblIdProdutoCliente.Text, lblMetodoMovimento.Text, dt.Rows[0]["ENDERECO"].ToString());

                    lblIdEnderecoOrigem.Text = ret[0];
                    lblEndecoOrigem.Text = ret[1];
                    lblUaOrigem.Text = ret[2];
                    txtLeituraDaUa.Enabled = true;
                    txtLeituraDaUa.Focus();
                    break;
            }
        }

        private void grd_Click(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            lblMetodoMovimento.Text = grd[row, 3].ToString();
            lblIdProdutoCliente.Text = grd[row, 0].ToString();

            if (grd[row, 3].ToString() == "")
            {
                MessageBox.Show("Produto sem Endereço de Picking Cadastrado.", "SISTECNO COLETOR");
                return;
            }

            string[] ret = new Classes.BLL.DepositoPlantaLocalizacao().RetornarEndereco(lblIdProdutoCliente.Text, lblMetodoMovimento.Text, dt.Rows[0]["ENDERECO"].ToString());

            lblUaOrigem.Text = ret[2];
            lblEndecoOrigem.Text = ret[1];
            lblIdEnderecoOrigem.Text = ret[0];
            txtLeituraDaUa.Enabled = true;
            txtLeituraDaUa.Focus();
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPic = new Classes.BLL.DepositoPlantaLocalizacao().RetornarEnderecosPicking(lblIdProdutoCliente.Text);

                if (dtPic.Rows.Count == 0)
                {
                    lblMetodoMovimento.Text = "";
                    lblIdProdutoCliente.Text = "";
                    lblEnderecoPicking.Text = "";

                    grd.DataSource = null;
                    btnConfirmar.Enabled = false;
                    txtEnderecoProduto.Text = "";
                    txtEnderecoProduto.Focus();
                    lblMetodoMovimento.Text = "";
                    lblIdProdutoCliente.Text = "";
                    lblEnderecoPicking.Text = "";
                    lblEnderecoPicking.Text = "";
                    lblIdEnderecoPicking.Text = "";
                    lblIdEnderecoOrigem.Text = "";
                    lblEndecoOrigem.Text = "";
                    lblUaOrigem.Text = "";
                    txtLeituraDaUa.Text = "";
                    txtLeituraEndereco.Text = "";
                    txtEnderecoProduto.Focus();
                    throw new Exception("Produto sem cadastro de Picking.");
                }

                // SE ENCONTRAR ENDERÇO DE PICKING
                if (int.Parse(dtPic.Compute("COUNT(IDDEPOSITOPLANTALOCALIZACAO)", "IDDEPOSITOPLANTALOCALIZACAO=" + txtLeituraEndereco.Text).ToString()) > 0)
                {
                    new Classes.BLL.UnidadeDeArmazenagem().AlterarEnderecoUa(lblUaOrigem.Text, lblIdEnderecoPicking.Text);
                    lblMetodoMovimento.Text = "";
                    lblIdProdutoCliente.Text = "";
                    lblEnderecoPicking.Text = "";

                    grd.DataSource = null;
                    btnConfirmar.Enabled = false;
                    txtEnderecoProduto.Text = "";
                    txtEnderecoProduto.Focus();
                    lblMetodoMovimento.Text = "";
                    lblIdProdutoCliente.Text = "";
                    lblEnderecoPicking.Text = "";
                    lblEnderecoPicking.Text = "";
                    lblIdEnderecoPicking.Text = "";
                    lblIdEnderecoOrigem.Text = "";
                    lblEndecoOrigem.Text = "";
                    lblUaOrigem.Text = "";
                    txtLeituraDaUa.Text = "";
                    txtLeituraEndereco.Text = "";
                    MessageBox.Show("Operação Efetuada com Sucesso.", "Sistecno Coletor");
                }
                else
                {
                    MessageBox.Show("Selecione um produto.", "Sistecno Coletor");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtLeituraDaUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtEnderecoProduto.Text != "")
                {
                    if (txtLeituraDaUa.Text.Substring(0, txtLeituraDaUa.Text.Length-1) == lblUaOrigem.Text)
                    {
                        txtLeituraEndereco.Enabled = true;
                        txtLeituraEndereco.Focus();
                        txtLeituraEndereco.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("UA Incorreta");
                        txtLeituraDaUa.Text = "";
                        txtLeituraDaUa.Focus();
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtLeituraEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtEnderecoProduto.Text != "")
                {
                    if (txtLeituraEndereco.Text == lblIdEnderecoPicking.Text)
                    {
                        btnConfirmar.Enabled = true;
                        btnConfirmar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Endereço Incorreto");
                        txtLeituraEndereco.Text = "";
                        txtLeituraEndereco.Focus();
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}