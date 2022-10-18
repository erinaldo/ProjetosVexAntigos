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
    public partial class CLW00008 : Form
    {
        public CLW00008()
        {
            InitializeComponent();
        }

        private void CLW00001_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                lblTitulo.Text = VarGlobal.NomePrograma;
            }
            catch (Exception ecx)
            {
                MessageBox.Show(ecx.Message);
            }
        }



        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.KeyChar == (char)Keys.Return && txtUa.Text != "")
            {
                e.Handled = true;
                PesquisarUA();
            }
            Cursor.Current = Cursors.Default;

        }

        DataTable dtUa;
        private void PesquisarUA()
        {

            dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarSaida(txtUa.Text, txtCodigoDeBarras.Text, txtEndereco.Text);
            if (dtUa.Rows.Count == 0 || dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() != txtEndereco.Text)
            {
                MessageBox.Show("Pallet não encontrado ou o Endereço não é um Picking .", "Atenção");
                InciaControles();

            }
            else
            {
                txtQuantidade.Text = "";                
                txtQuantidade.Enabled = true;
                txtQuantidade.Focus();

                lblSaldoUa.Text = int.Parse(float.Parse(dtUa.Rows[0]["SALDO"].ToString()).ToString()).ToString();
            }
        }

        private void InciaControles()
        {
            txtQuantidade.Text = "";
            txtQuantidade.Enabled = false;
            txtEndereco.Text = "";
            txtCodigoDeBarras.Text = "";
            txtCodigoDeBarras.Enabled = false;
            txtUa.Text = "";
            txtUa.Enabled = false;
            btnConfirmar.Enabled = false;
            txtUaDestino.Enabled = false;
            lblServicoDescricaoProduto.Text = "";
            lblUaDestino.Text = "";
            lblSaldoUa.Text = "0";
            txtUaDestino.Text = "";
            label7.Text = "";
            txtEndereco.Focus();
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Salvar();
            Cursor.Current = Cursors.Default;

        }

        private void Salvar()
        {
            try
            {

                if (txtCodigoDeBarras.Text != "" && txtCodigoDeBarras.Text != "" && txtUa.Text != "" && lblIdProdutoCliente.Text != "")
                {
                    new Classes.BLL.Estoque().SairPorPicking(dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString(), lblIdProdutoCliente.Text, txtQuantidade.Text, txtEndereco.Text, txtUaDestino.Text, dtUa.Rows[0]["IDPRODUTOEMBALAGEM"].ToString());
                    MessageBox.Show("Registro Gravado com Sucesso");
                    InciaControles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }


        private void CLW00008_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "")
            {
                e.Handled = true;
                txtCodigoDeBarras.Enabled = true;
                txtCodigoDeBarras.Focus();
            }
        }

        private void txtCodigoDeBarras_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "")
            {
                e.Handled = true;
               

                DataTable dt = new Classes.BLL.Produto().RetornarDadosLogistico(txtCodigoDeBarras.Text);

                if (dt.Rows.Count > 0)
                {
                    lblServicoDescricaoProduto.Text = dt.Rows[0]["DESCRICAO"].ToString();
                    lblIdProdutoCliente.Text = dt.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    label7.Text = dt.Rows[0]["Codigo"].ToString();
                    txtUa.Enabled = true;
                    txtUa.Focus();
                }
                else
                {
                    txtUa.Enabled = false;
                    txtUa.Focus();
                    txtCodigoDeBarras.Text = "";
                    txtCodigoDeBarras.Focus();
                    MessageBox.Show("Produto não encontrado");
                }
            }
            Cursor.Current = Cursors.Default;

        }

        private void txtUaDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {



                if (e.KeyChar == (char)Keys.Return && txtUaDestino.Text != "")
                {
                    e.Handled = true;
                    string Sql = "SELECT * FROM UNIDADEDEARMAZENAGEM UA WHERE (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ isnull(UA.DIGITO,''))='" + txtUaDestino.Text + "' and STATUS NOT IN('EM ESTOQUE', 'RECEBIDO')";
                    DataTable dt = Classes.BdExterno.RetornarDT(Sql, VarGlobal.Conexao);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("UA de DESTINO Não encontrada");
                        txtUaDestino.Focus();
                        txtUaDestino.SelectAll();
                        btnConfirmar.Enabled = false;
                        return;
                    }

                    btnConfirmar.Enabled = true;
                    btnConfirmar.Focus();
                }

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + EX.InnerException);
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtQuantidade.Text != "")
            {
                if (int.Parse(lblSaldoUa.Text) < int.Parse(txtQuantidade.Text))
                {
                    MessageBox.Show("Quantidade solicitada Invalida");
                    txtQuantidade.Focus();
                    txtQuantidade.SelectAll();
                    return;
                }

                txtUaDestino.Enabled = true;
                txtUaDestino.Focus();
               // btnConfirmar.Enabled = true;
                //btnConfirmar.Focus();
            }
        }
    }
}