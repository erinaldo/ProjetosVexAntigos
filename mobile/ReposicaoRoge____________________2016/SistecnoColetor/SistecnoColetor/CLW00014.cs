using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public partial class CLW00014 : Form
    {
        public CLW00014()
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
                dtServ = null;
                dtServFormat = null;
                dtUa = null;
                CarregarServicos();
              
            }
            catch (Exception ecx)
            {
                MessageBox.Show(ecx.Message);
            }
        }


        DataTable dtServ;
        DataTable dtServFormat;
        private void CarregarServicos()
        {            
            List<ParametrosProcedures> par = new List<ParametrosProcedures>();

            ParametrosProcedures p = new ParametrosProcedures();
            p.nomePar = "TIPO";
            p.valorPar = "SAIDA";
            p.tipoDeDados = "string";
            par.Add(p);

            p = new ParametrosProcedures();
            p.nomePar = "OPERACAOCOLETOR";
            p.valorPar = "CARRINHO HIDRAULICO";
            p.tipoDeDados = "string";

            par.Add(p);
            dtServ = Classes.BdExterno.RetornarDataTableProcedure("PRC_SERVICOSAEXECUTAR", par, VarGlobal.Conexao);

            dtServFormat = new DataTable();
            dtServFormat.Columns.Add("EnderecoOrigem");
            dtServFormat.Columns.Add("EnderecoDestino");
            dtServFormat.Columns.Add("IDUnidadeDeArmazenagem");
            dtServFormat.Columns.Add("IDRomaneio");
            dtServFormat.Columns.Add("IDMovimentacaoItem");



            for (int i = 0; i < dtServ.Rows.Count; i++)
            {
                DataRow r = dtServFormat.NewRow();

                r[0] = dtServ.Rows[i]["EnderecoOrigem"].ToString();
                r[1] = dtServ.Rows[i]["EnderecoDestino"].ToString();
                r[2] = dtServ.Rows[i]["IDUnidadeDeArmazenagem"].ToString();
                r[3] = dtServ.Rows[i]["IDRomaneio"].ToString();
                r[4] = dtServ.Rows[i]["IDMovimentacaoItem"].ToString();

                dtServFormat.Rows.Add(r);

            }

            dataGrid1.DataSource = dtServFormat;



        }


        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "")
                {
                    e.Handled = true;
                    PesquisarUA();


                    //int row = grdServicos.CurrentCell.RowNumber;
                    string itemClicado = dataGrid1[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (txtUa.Text.Substring(0, txtUa.Text.Length - 1) != dadoSelecionado[0]["IDUnidadeDeArmazenagem"].ToString())
                    {

                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("UA INVALIDA");
                        txtUa.SelectAll();
                        txtUa.Focus();
                        return;

                    }
                    else
                    {
                        txtQuantidade.Enabled = true;
                        txtQuantidade.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao procurar a UA.");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

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

            lblUaDestino.Text = "";
            lblSaldoUa.Text = "0";
            txtUaDestino.Text = "";
            label7.Text = "";
            lblServicoQuantidade.Text = "";
            lblServRomaneio.Text = "";
            lblServicoUA.Text = "";
            lblServicoCodigoProduto.Text = "";
            tabControl1.SelectedIndex = 0;            
            dtServ = null;
            dtServFormat = null;

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

                    string itemClicado = dataGrid1[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    new Classes.BLL.Estoque().SairPorPicking(dadoSelecionado[0]["IdUnidadeDeArmazenagem"].ToString(), lblIdProdutoCliente.Text, txtQuantidade.Text, txtEndereco.Text, txtUaDestino.Text.Substring(0, txtUaDestino.Text.Length - 1), dadoSelecionado[0]["IDProdutoEmbalagem"].ToString(), dadoSelecionado[0]["IDMOVIMENTACAOITEM"].ToString(), dadoSelecionado[0]["IDRomaneio"].ToString());
                    MessageBox.Show("Registro Gravado com Sucesso");
                    InciaControles();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique se o produto: " + txtCodigoDeBarras.Text + ", na UA: " + txtUa.Text + " possui Saldo Suficiente.");
                Cursor.Current = Cursors.Default;
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
            Cursor.Current = Cursors.WaitCursor;            

            try
            {

                if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "")
                {
                    //int row = grdServicos.CurrentCell.RowNumber;
                    string itemClicado = dataGrid1[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (txtEndereco.Text == dadoSelecionado[0]["IDDepositoPlantaLocalizacaoOrigem"].ToString())
                    {
                        e.Handled = true;

                        txtCodigoDeBarras.Enabled = true;
                        txtCodigoDeBarras.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Endereço de Origem Invalido");
                        txtEndereco.SelectAll();
                        txtEndereco.Focus();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao Procurar Endereco");
            }
            finally
            {
                Cursor.Current = Cursors.Default;            
            }
        }


        private void txtCodigoDeBarras_KeyPress_1(object sender, KeyPressEventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string itemClicado = dataGrid1[row, 4].ToString();
                DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtCodigoDeBarras.Text == dadoSelecionado[0]["Codigodebarras"].ToString())
                {
                    e.Handled = true;


                    DataTable dt = new Classes.BLL.Produto().RetornarDadosLogistico(txtCodigoDeBarras.Text);

                    if (dt.Rows.Count > 0)
                    {
                        txtUa.Enabled = true;
                        txtUa.Focus();
                    }
                    else
                    {
                        txtUa.Enabled = false;
                        txtUa.Focus();
                        txtCodigoDeBarras.Text = "";
                        txtCodigoDeBarras.Focus();
                        MessageBox.Show("Produto Invalido" );
                    }


                }

                if (e.KeyChar == (char)Keys.Return && txtCodigoDeBarras.Text != dadoSelecionado[0]["Codigodebarras"].ToString())
                {
                    txtUa.Enabled = false;
                    txtUa.Focus();
                    txtCodigoDeBarras.Text = "";
                    txtCodigoDeBarras.Focus();
                    MessageBox.Show("Produto Invalido");
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



        private void txtUaDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


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
            catch (Exception)
            {
                MessageBox.Show("Problema com a UA Destino");

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (e.KeyChar == (char)Keys.Return && txtQuantidade.Text != "")
                {
                    if (int.Parse(lblSaldoUa.Text) < int.Parse(txtQuantidade.Text))
                    {
                        MessageBox.Show("Quantidade solicitada MAIOR QUE A DO ESTOQUE");
                        txtQuantidade.Focus();
                        txtQuantidade.SelectAll();
                        return;
                    }

                    if (int.Parse(float.Parse(txtQuantidade.Text).ToString()) != int.Parse(float.Parse(lblServicoQuantidade.Text).ToString()))
                    {
                        MessageBox.Show("Quantidade solicitada Invalida");
                        txtQuantidade.Focus();
                        txtQuantidade.SelectAll();
                        return;
                    }

                    txtUaDestino.Enabled = true;
                    txtUaDestino.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problema com a Quantidade");

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && lblServicoUA.Text == "")
                tabControl1.SelectedIndex = 0;
            else
            {
                CarregarServicos();
            }
        }
              

        int row = 0;
        private void grdServicos_DoubleClick_1(object sender, EventArgs e)
        {
            row = dataGrid1.CurrentCell.RowNumber;
            string itemClicado = dataGrid1[row, 4].ToString();

            DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

            if (dadoSelecionado.Length > 0)
            {
                lblServEnderecoOrigem.Text = dadoSelecionado[0]["EnderecoOrigem"].ToString();
                //lblServEnderecoDestino.Text = dadoSelecionado[0]["EnderecoDestino"].ToString();
                lblServicoUA.Text = dadoSelecionado[0]["IDUnidadeDeArmazenagem"].ToString();
                lblServRomaneio.Text = dadoSelecionado[0]["IDRomaneio"].ToString();
                lblServicoCodigoProduto.Text = dadoSelecionado[0]["Codigo"].ToString() + "-" + dadoSelecionado[0]["Descricao"].ToString();
                lblServicoQuantidade.Text = int.Parse(float.Parse(dadoSelecionado[0]["Quantidade"].ToString()).ToString()).ToString();
                lblIdProdutoCliente.Text = dadoSelecionado[0]["IDPRODUTOCLIENTE"].ToString();
                tabControl1.SelectedIndex = 1;
                txtEndereco.Focus();
            }
        }


        /*
             private void grdServicos_DoubleClick_1(object sender, EventArgs e)
        {
            row = grdServicos.CurrentCell.RowNumber;
            string itemClicado = grdServicos[row, 4].ToString();

            DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

            if (dadoSelecionado.Length > 0)
            {
                lblServEnderecoOrigem.Text = dadoSelecionado[0]["EnderecoOrigem"].ToString();
                //lblServEnderecoDestino.Text = dadoSelecionado[0]["EnderecoDestino"].ToString();
                lblServicoUA.Text = dadoSelecionado[0]["IDUnidadeDeArmazenagem"].ToString();
                lblServRomaneio.Text = dadoSelecionado[0]["IDRomaneio"].ToString();
                lblServicoCodigoProduto.Text = dadoSelecionado[0]["Codigo"].ToString() + "-" + dadoSelecionado[0]["Descricao"].ToString();
                lblServicoQuantidade.Text = int.Parse(float.Parse(dadoSelecionado[0]["Quantidade"].ToString()).ToString()).ToString();
                lblIdProdutoCliente.Text = dadoSelecionado[0]["IDPRODUTOCLIENTE"].ToString();
                tabControl1.SelectedIndex = 1;
                txtEndereco.Focus();
            }
        }
         * */

        private void tabControl1_Validated(object sender, EventArgs e)
        {

        }

        private void txtEndereco_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            row = dataGrid1.CurrentCell.RowNumber;
            string itemClicado = dataGrid1[row, 4].ToString();

            DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

            if (dadoSelecionado.Length > 0)
            {
                lblServEnderecoOrigem.Text = dadoSelecionado[0]["EnderecoOrigem"].ToString();
                //lblServEnderecoDestino.Text = dadoSelecionado[0]["EnderecoDestino"].ToString();
                lblServicoUA.Text = dadoSelecionado[0]["IDUnidadeDeArmazenagem"].ToString();
                lblServRomaneio.Text = dadoSelecionado[0]["IDRomaneio"].ToString();
                lblServicoCodigoProduto.Text = dadoSelecionado[0]["Codigo"].ToString() + "-" + dadoSelecionado[0]["Descricao"].ToString();
                lblServicoQuantidade.Text = int.Parse(float.Parse(dadoSelecionado[0]["Quantidade"].ToString()).ToString()).ToString();
                lblIdProdutoCliente.Text = dadoSelecionado[0]["IDPRODUTOCLIENTE"].ToString();
                tabControl1.SelectedIndex = 1;
                txtEndereco.Focus();
            }
        }

        private void txtCodigoDeBarras_TextChanged(object sender, EventArgs e)
        {

        }
    }
}