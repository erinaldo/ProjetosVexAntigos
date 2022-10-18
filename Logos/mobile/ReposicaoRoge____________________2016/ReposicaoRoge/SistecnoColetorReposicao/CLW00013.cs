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
    public partial class CLW00013 : Form
    {
        public CLW00013()
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
            p.valorPar = "EMPILHADEIRA";
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

            grdServicos.DataSource = dtServFormat;



        }

        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Cursor.Current = Cursors.WaitCursor;
                //int row = grdServicos.CurrentCell.RowNumber;
                string itemClicado = grdServicos[row, 4].ToString();
                DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && Helpers.RetirarDigitoUA( txtUa.Text )== dadoSelecionado[0]["idUnidadedeArmazenagem"].ToString())
                {
                    e.Handled = true;
                    PesquisarUA();
                }
                else
                {
                    MessageBox.Show("UA Invalida");
                    txtUa.SelectAll();
                    txtUa.Focus();
                }
                Cursor.Current = Cursors.Default;
            }
        }

        DataTable dtUa;
        private void PesquisarUA()
        {

            dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarSaida(txtUa.Text,  txtEndereco.Text);
            if (dtUa.Rows.Count == 0 || dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() != txtEndereco.Text)
            {
                MessageBox.Show("Pallet não encontrado ou o Endereço não é Valido .", "Atenção");
                InciaControles();
            }
            else
            {               
                lblSaldoUa.Text = int.Parse(float.Parse(dtUa.Rows[0]["SALDO"].ToString()).ToString()).ToString();
                //lblServicoDescricaoProduto.Text = dtUa.Rows[0]["DESCRICAO"].ToString();
                //lblServicoCodigoProduto.Text = dtUa.Rows[0]["CODIGOPRODUTO"].ToString();
                //lblServEnderecoOrigem.Text = dtUa.Rows[0]["ENDERECO"].ToString();
                //lblServicoQuantidade.Text = lblSaldoUa.Text;
                //lblServicoUA.Text = dtUa.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString();
                lblIdProdutoCliente.Text = dtUa.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                txtUaDestino.Enabled = true;
                txtUaDestino.Focus();
                //btnConfirmar.Enabled = true;

            }
        }

        private void InciaControles()
        {           
            txtEndereco.Text = "";           
            txtUa.Text = "";
            txtUa.Enabled = false;
            btnConfirmar.Enabled = false;
            //lblServicoDescricaoProduto.Text = "";
            lblUaDestino.Text = "";
            lblSaldoUa.Text = "0";
            //lblServicoDescricaoProduto.Text = "";
            lblServicoCodigoProduto.Text = "";
            lblServEnderecoOrigem.Text = "";
            lblServEnderecoOrigem.Text = "";
            lblServEnderecoDestino.Text = "";
            lblIdProdutoCliente.Text = "";
            lblServicoQuantidade.Text = "";
            lblServRomaneio.Text = "";
            lblServicoUA.Text = "";
            lblServicoCodigoProduto.Text = "";
            tabControl1.SelectedIndex = 0;
            //txtEndereco.Focus();
            dtServ = null;
            dtServFormat = null;
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
                    //verifica se ele colocou o endereco de destino corretamente
                    //int row = grdServicos.CurrentCell.RowNumber;
                    string itemClicado = grdServicos[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (txtUaDestino.Text == dadoSelecionado[0]["IDDepositoPlantaLocalizacaoDestino"].ToString())
                    {

                        new Classes.BLL.Estoque().SairUAInteira(dadoSelecionado[0]["IdUnidadeDeArmazenagem"].ToString(), lblIdProdutoCliente.Text, lblServicoQuantidade.Text, dadoSelecionado[0]["IDDepositoPlantaLocalizacaoOrigem"].ToString(), dadoSelecionado[0]["IDDepositoPlantaLocalizacaoDestino"].ToString(), dtUa.Rows[0]["IDPRODUTOEMBALAGEM"].ToString(), dadoSelecionado[0]["IdmovimentacaoItem"].ToString(), dadoSelecionado[0]["IDRomaneio"].ToString());
                        MessageBox.Show("Registro Gravado com Sucesso");
                        InciaControles();
                        CarregarServicos();
                        tabControl1.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Endereço Incorreto");
                        txtUaDestino.SelectAll();
                        txtUaDestino.Focus();
                    }
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

            if (e.KeyChar == (char)Keys.Return)
            {
                //int row = grdServicos.CurrentCell.RowNumber;
                string itemClicado = grdServicos[row, 4].ToString();
                DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtEndereco.Text == dadoSelecionado[0][5].ToString())
                   // if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtEndereco.Text == dadoSelecionado[0]["EnderecoOrigem"].ToString())
                {
                    e.Handled = true;
                    txtUa.Enabled = true;
                    txtUa.Focus();
                }
                else
                {
                    MessageBox.Show("Endereço de Origem Invalido");
                    txtEndereco.SelectAll();
                    txtEndereco.Focus();
                }
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

                    string itemClicado = grdServicos[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (dadoSelecionado[0]["IDDepositoPlantaLocalizacaoDestino"].ToString() == txtUaDestino.Text)
                    {
                        btnConfirmar.Enabled = true;
                        btnConfirmar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Endereço de DESTINO Não Encontrado");
                        txtUaDestino.Focus();
                        txtUaDestino.SelectAll();
                        btnConfirmar.Enabled = false;
                        return;
                    }


                    // string sql = "SELECT DPL.IDDEPOSITOPLANTALOCALIZACAO  FROM DEPOSITOPLANTA DP  INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA  WHERE DP.DESCRICAO = 'DOCAS' AND DPL.IDDepositoPlantaLocalizacao=" + txtUaDestino.Text;
                    //DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    /*if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Endereço de DESTINO Não Encontrado");
                        txtUaDestino.Focus();
                        txtUaDestino.SelectAll();
                        btnConfirmar.Enabled = false;
                        return;
                    }*/



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
        int row = 0;
        private void grdServicos_DoubleClick(object sender, EventArgs e)
        {
            row = grdServicos.CurrentCell.RowNumber;
            string itemClicado = grdServicos[row, 4].ToString();

            DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

            if (dadoSelecionado.Length > 0)
            {
                lblServEnderecoOrigem.Text = dadoSelecionado[0]["EnderecoOrigem"].ToString();
                lblServEnderecoDestino.Text = dadoSelecionado[0]["EnderecoDestino"].ToString();
                lblServicoUA.Text = dadoSelecionado[0]["IDUnidadeDeArmazenagem"].ToString();
                lblServRomaneio.Text = dadoSelecionado[0]["IDRomaneio"].ToString();
                lblServicoCodigoProduto.Text = dadoSelecionado[0]["Codigo"].ToString() + "-" + dadoSelecionado[0]["Descricao"].ToString();
                lblServicoQuantidade.Text =int.Parse( float.Parse(dadoSelecionado[0]["Quantidade"].ToString()).ToString()).ToString();

                tabControl1.SelectedIndex = 1;
                txtEndereco.Focus();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && lblServEnderecoDestino.Text == "")
                tabControl1.SelectedIndex = 0;
            else
            {
                CarregarServicos();              
            }

        }

        private void tabControl1_Validated(object sender, EventArgs e)
        {

        }

        private void txtUaDestino_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}