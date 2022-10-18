using System;
using System.Data;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class CLW00011 : Form
    {
        public CLW00011()
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

            dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarSaidaLivre(txtUa.Text,  txtEndereco.Text);
            if (dtUa.Rows.Count == 0 || dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() != txtEndereco.Text)
            {
                MessageBox.Show("Pallet não encontrado ou o Endereço não é Valido .", "Atenção");
                InciaControles();
            }
            else
            {               
                lblSaldoUa.Text = int.Parse(float.Parse(dtUa.Rows[0]["SALDO"].ToString()).ToString()).ToString();
                lblServicoDescricaoProduto.Text = dtUa.Rows[0]["DESCRICAO"].ToString();
                label7.Text = dtUa.Rows[0]["CODIGOPRODUTO"].ToString();
                lblServicoEndereco.Text = dtUa.Rows[0]["ENDERECO"].ToString();
                lblServicoQuantidade.Text = lblSaldoUa.Text;
                lblServicoUA.Text = dtUa.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString();
                lblIdProdutoCliente.Text = dtUa.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                txtUaDestino.Enabled = true;
                txtUaDestino.Focus();               

            }
        }

        private void InciaControles()
        {           
            txtEndereco.Text = "";           
            txtUa.Text = "";
            txtUa.Enabled = false;
            btnConfirmar.Enabled = false;
            lblServicoDescricaoProduto.Text = "";
            lblUaDestino.Text = "";
            lblSaldoUa.Text = "0";
            lblServicoDescricaoProduto.Text = "";
            label7.Text = "";
            lblServicoEndereco.Text = "";
            txtEndereco.Focus();
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            Salvar();
        }

        private void Salvar()
        {
            try
            {
                if (txtUa.Text != "" && txtUaDestino.Text != "")
                {
                    new Classes.BLL.Estoque().SairUAInteira(dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString(), lblIdProdutoCliente.Text, lblSaldoUa.Text, txtEndereco.Text, txtUaDestino.Text, dtUa.Rows[0]["IDPRODUTOEMBALAGEM"].ToString());
                    MessageBox.Show("Registro Gravado com Sucesso");
                    InciaControles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                txtUa.Enabled = true;
                txtUa.Focus();
            }
        }

        //private void txtCodigoDeBarras_KeyPress_1(object sender, KeyPressEventArgs e)
        //{
        //    Cursor.Current = Cursors.WaitCursor;

        //    if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "")
        //    {
        //        e.Handled = true;
               

        //        DataTable dt = new Classes.BLL.Produto().RetornarDadosLogistico(txtCodigoDeBarras.Text);

        //        if (dt.Rows.Count > 0)
        //        {
        //            lblServicoDescricaoProduto.Text = dt.Rows[0]["DESCRICAO"].ToString();
        //            lblIdProdutoCliente.Text = dt.Rows[0]["IDPRODUTOCLIENTE"].ToString();
        //            label7.Text = dt.Rows[0]["Codigo"].ToString();
        //            txtUa.Enabled = true;
        //            txtUa.Focus();
        //        }
        //        else
        //        {
        //            txtUa.Enabled = false;
        //            txtUa.Focus();
        //            //txtCodigoDeBarras.Text = "";
        //            //txtCodigoDeBarras.Focus();
        //            MessageBox.Show("Produto não encontrado");
        //        }
        //    }
        //    Cursor.Current = Cursors.Default;

        //}

        private void txtUaDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                if (e.KeyChar == (char)Keys.Return && txtUaDestino.Text != "")
                {
                    e.Handled = true;
                    //string Sql = "SELECT * FROM UNIDADEDEARMAZENAGEM UA WHERE (CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))+ UA.DIGITO)=" + txtUaDestino.Text;

                    string sql = "SELECT DPL.IDDEPOSITOPLANTALOCALIZACAO  FROM DEPOSITOPLANTA DP  INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA  WHERE DP.DESCRICAO = 'DOCAS' AND DPL.IDDepositoPlantaLocalizacao=" + txtUaDestino.Text;
                    DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Endereço de DESTINO Não Encontrado");
                        txtUaDestino.Focus();
                        txtUaDestino.SelectAll();
                        btnConfirmar.Enabled = false;
                        return;
                    }

                    btnConfirmar.Enabled = true;
                    btnConfirmar.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);

            }
            finally
            {
                   Cursor.Current = Cursors.Default;
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {                
                txtUaDestino.Enabled = true;
                txtUaDestino.Focus();
            }
        }
    }
}