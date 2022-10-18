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
    public partial class CLW00012 : Form
    {
        public CLW00012()
        {
            InitializeComponent();
        }

        private void CLW00012_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            dtAuxProd = null;
            dtaux = null;
        }

        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        DataTable dtAuxProd;
        private void txtCodigoDeBarras_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && txtCodigoDeBarras.Text != "")
                {
                    e.Handled = true;
                    //verifica o produto e verifica se o mesmo possui endereco de picking cadastrado
                    string sql = " SELECT top 1 PC.IDPRODUTOCLIENTE, PC.CODIGO, PC.DESCRICAO,  P.CODIGODEBARRAS, '0' PICKING, EM.IDPRODUTOEMBALAGEM ";
                    sql += " FROM PRODUTO P ";
                    sql += " INNER JOIN PRODUTOEMBALAGEM EM ON EM.IDPRODUTO = P.IDPRODUTO  ";
                    sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = EM.IDPRODUTOCLIENTE ";
                    //sql += " LEFT JOIN PRODUTOCLIENTEREGRA PCR ON PCR.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
                    sql += " WHERE P.CODIGODEBARRAS ='" + txtCodigoDeBarras.Text + "'";// AND PCR.TipoDeRegra='PICKING' ";



                    dtAuxProd = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dtAuxProd.Rows.Count == 0)
                    {
                        MessageBox.Show("Produto não encontrado");
                        txtCodigoDeBarras.SelectAll();
                        txtCodigoDeBarras.Focus();
                        return;
                    }

                    //if (dtAuxProd.Rows[0]["Picking"].ToString() == "")
                    //{
                    //    MessageBox.Show("Produto sem Endereço de Picking.");
                    //    txtCodigoDeBarras.SelectAll();
                    //    txtCodigoDeBarras.Focus();
                    //    return;
                    //}

                   // lblDescricao.Text = dtAuxProd.Rows[0]["CODIGO"].ToString() + " - " + dtAuxProd.Rows[0]["DESCRICAO"].ToString();

                    txtQuantidade.Enabled = true;
                    txtQuantidade.Focus();

                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + EX.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {

        }

        DataTable dtaux;
        private void txtUa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "")
                {


                        txtEndereçoDePicking.Enabled = true;
                        txtEndereçoDePicking.Focus();
                    
                }
                   /* e.Handled = true;
                    //VERIFICA SE A UA ESTA VAZIA
                    string sql = "SELECT ISNULL(UAL.SALDO,0) , UA.IdunidadeDeArmazenagem FROM UnidadeDeArmazenagem UA ";
                    sql += " LEFT JOIN UnidadeDeArmazenagemLote UAL ON UAL.IDUnidadeDeArmazenagem = UA.IDUnidadeDeArmazenagem  ";
                    sql += "WHERE CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(30)) + ISNULL(UA.DIGITO, '') = " + txtUa.Text;
                    //sql += " and STATUS NOT IN('EM ESTOQUE') ";

                    dtaux = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dtaux.Rows.Count == 0)
                    {
                        MessageBox.Show("Esta Ua Não está Vazia.");
                        txtUa.SelectAll();
                        txtUa.Focus();
                        return;
                    }
                    else
                    {
                        if (float.Parse(dtaux.Rows[0][0].ToString()) > float.Parse("0"))
                        {
                            MessageBox.Show("Ua já possui saldo!");
                            txtUa.SelectAll();
                            txtUa.Focus();
                            return;
                        }

                    
                    
                }*/

            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + EX.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                //zera as uas que estao no endereço, preparando para receber a nova ua
                string sql = "update ual set Saldo=0 ";
                sql += " from DepositoPlantaLocalizacao dpl ";
                sql += " inner join UnidadeDeArmazenagem ua on ua.IDDepositoPlantaLocalizacao = dpl.IDDepositoPlantaLocalizacao ";
                sql += " inner join UnidadeDeArmazenagemLote ual on ual.IDUnidadeDeArmazenagem = ua.IDUnidadeDeArmazenagem ";
                sql += " where dpl.IDDepositoPlantaLocalizacao =" + txtEndereçoDePicking.Text;
                sql += " and ual.Saldo>0; ";

               


                if (txtUa.Text == "0")
                    Classes.BdExterno.Executar(sql, VarGlobal.Conexao);
                else
                {
                    sql += " UPDATE MI SET MI.DATADEEXECUCAO=GETDATE() ";
                    sql += "FROM MOVIMENTACAOITEM MI  ";
                    sql += "INNER JOIN MOVIMENTACAO M ON M.IDMOVIMENTACAO = MI.IDMOVIMENTACAO ";
                    sql += "WHERE IDUNIDADEDEARMAZENAGEM = " + Helpers.RetirarDigitoUA(txtUa.Text);
                    sql += "AND MI.DATADEEXECUCAO IS NULL AND M.ESTOQUEPROCESSADO='NAO' ";
                    sql += "AND M.TIPO = 'SAIDA' ;";

                    new Classes.BLL.Estoque().EntrarComUA(Helpers.RetirarDigitoUA(txtUa.Text),
                                                              dtAuxProd.Rows[0]["IDPRODUTOCLIENTE"].ToString(),
                                                              null,
                                                              txtQuantidade.Text,
                                                              "",
                                                              txtEndereçoDePicking.Text,
                                                              dtAuxProd.Rows[0]["IDPRODUTOEMBALAGEM"].ToString(), sql);
                }

                MessageBox.Show("Operação Efetuada Com Sucesso");
                txtEndereçoDePicking.Text = "";
                txtQuantidade.Text = "";
                txtUa.Text = "";
                txtCodigoDeBarras.Text = "";
                txtCodigoDeBarras.Enabled = false;
                txtQuantidade.Enabled = false;
                txtEndereçoDePicking.Enabled = false;
                lblDescricao.Text = "";
                txtUa.Focus();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + EX.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtQuantidade_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && txtCodigoDeBarras.Text != "" && txtQuantidade.Text != "")
                {
                    e.Handled = true;
                    float x = float.Parse(txtQuantidade.Text);

                    btnConfirmar.Enabled = true;
                    btnConfirmar.Focus();
                }

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message + EX.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtEndereçoDePicking_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && txtEndereçoDePicking.Text != "")
                {
                    e.Handled = true;

                    if (txtUa.Text == "0")
                    {
                       //DialogResult r= MessageBox.Show("Tem certeza que o endereco não possui nenhum pallet? Clique em YES para ZERAR o Endereço ou NO para CANCELAR", "Sistecno Coletor", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                       //if (r == DialogResult.Yes)
                       //{
                           btnConfirmar.Enabled = true;
                           btnConfirmar.Focus();
                       //}
                       //else
                       //{

                       //    txtEndereçoDePicking.Text = "";
                       //    txtEndereçoDePicking.Enabled = false;
                       //}
                        return;
                    }


                    string sql = " SELECT UA.IDUNIDADEDEARMAZENAGEM, UA.IDDEPOSITOPLANTALOCALIZACAO, DPL.CODIGO, UAL.SALDO, PC.DESCRICAO, PC.CODIGO CodProduto ";
                    sql += " FROM UNIDADEDEARMAZENAGEM UA ";
                    sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO  = UA.IDDEPOSITOPLANTALOCALIZACAO ";
                    sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM ";
                    sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = UA.IDPRODUTOCLIENTE ";
                    sql += " INNER JOIN LOTE L ON L.IDLOTE = UAL.IDLOTE  ";
                    sql += " WHERE SALDO>0 ";
                    sql += " AND UA.IDUNIDADEDEARMAZENAGEM = " + Helpers.RetirarDigitoUA(txtUa.Text) + " AND DPL.IDDEPOSITOPLANTALOCALIZACAO = " + txtEndereçoDePicking.Text;
                    sql += " AND L.IDPRODUTOCLIENTE  = PC.IDPRODUTOCLIENTE ";

                    DataTable dtaux = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dtaux.Rows.Count == 0)
                    {
                        //DialogResult d = MessageBox.Show("OS DADOS NÃO CONFEREM. APERTE SIM PARA FAZER O LANCAMENTO?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        //if (d == DialogResult.Yes)
                        //{
                            txtCodigoDeBarras.Enabled = true;
                            txtCodigoDeBarras.Focus();
                        //}
                        //else
                        //{
                        //    txtEndereçoDePicking.Text = "";
                        //    txtEndereçoDePicking.Enabled = false;
                        //    txtUa.Text = "";
                        //    txtUa.Focus();
                        //    lblDescricao.Text = "";
                        //}
                    }
                    else
                    {
                        lblDescricao.Text = dtaux.Rows[0]["CODPRODUTO"].ToString() + "-" + dtaux.Rows[0]["DESCRICAO"].ToString();
                        lblDescricao.Text += "\n";
                        lblDescricao.Text += "QTD: " + dtaux.Rows[0]["SALDO"].ToString();
                        txtQuantidade.Text = dtaux.Rows[0]["SALDO"].ToString();

                        DialogResult d = MessageBox.Show("O Produto: " + dtaux.Rows[0]["DESCRICAO"].ToString() + ", QTD:"+ dtaux.Rows[0]["SALDO"].ToString() + " ESTÃO CORRETOS. DESEJA ALTERAR A QUANTIDADE?"  ,"SISTECNO COLETOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

                       // MessageBox.Show("Posição e Produto Correto");

                        if (d == DialogResult.Yes)
                        {
                            txtCodigoDeBarras.Enabled = true;
                            txtCodigoDeBarras.Focus();
                        }
                        else
                        {
                            txtEndereçoDePicking.Text = "";
                            txtEndereçoDePicking.Enabled = false;
                            txtUa.Text = "";
                            txtUa.Focus();
                            lblDescricao.Text = "";
                        }

                    }

                    
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message + EX.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}