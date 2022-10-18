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
                    string sql = " SELECT PC.IDPRODUTOCLIENTE, PC.CODIGO, PC.DESCRICAO,  P.CODIGODEBARRAS, PCR.IDDEPOSITOPLANTALOCALIZACAO PICKING, EM.IDPRODUTOEMBALAGEM ";
                    sql += " FROM PRODUTO P ";
                    sql += " INNER JOIN PRODUTOEMBALAGEM EM ON EM.IDPRODUTO = P.IDPRODUTO  ";
                    sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = EM.IDPRODUTOCLIENTE ";
                    sql += " LEFT JOIN PRODUTOCLIENTEREGRA PCR ON PCR.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
                    sql += " WHERE P.CODIGODEBARRAS ='" + txtCodigoDeBarras.Text + "' AND PCR.TipoDeRegra='PICKING' ";



                    dtAuxProd = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dtAuxProd.Rows.Count == 0)
                    {
                        MessageBox.Show("Produto não encontrado");
                        txtCodigoDeBarras.SelectAll();
                        txtCodigoDeBarras.Focus();
                        return;
                    }

                    if (dtAuxProd.Rows[0]["Picking"].ToString() == "")
                    {
                        MessageBox.Show("Produto sem Endereço de Picking.");
                        txtCodigoDeBarras.SelectAll();
                        txtCodigoDeBarras.Focus();
                        return;
                    }

                    lblDescricao.Text = dtAuxProd.Rows[0]["CODIGO"].ToString() + " - " + dtAuxProd.Rows[0]["DESCRICAO"].ToString();

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
                    e.Handled = true;
                    //VERIFICA SE A UA ESTA VAZIA
                    string sql = "SELECT ISNULL(UAL.SALDO,0) , UA.IdunidadeDeArmazenagem FROM UnidadeDeArmazenagem UA ";
                    sql += " LEFT JOIN UnidadeDeArmazenagemLote UAL ON UAL.IDUnidadeDeArmazenagem = UA.IDUnidadeDeArmazenagem  ";
                    sql += "WHERE CAST(UA.IDUNIDADEDEARMAZENAGEM AS VARCHAR(30)) + ISNULL(UA.DIGITO, '') = " + txtUa.Text;
                    sql += " and STATUS NOT IN('EM ESTOQUE', 'RECEBIDO') ";

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


                        txtCodigoDeBarras.Enabled = true;
                        txtCodigoDeBarras.Focus();
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                new Classes.BLL.Estoque().EntrarComUA(dtaux.Rows[0]["IdUnidadeDeArmazenagem"].ToString(),
                                                        dtAuxProd.Rows[0]["IDPRODUTOCLIENTE"].ToString(),
                                                        null,
                                                        txtQuantidade.Text,
                                                        "",
                                                        txtEndereçoDePicking.Text,
                                                        dtAuxProd.Rows[0]["IDPRODUTOEMBALAGEM"].ToString());

                MessageBox.Show("Operação Efetuada Com Sucesso");
                txtEndereçoDePicking.Text = "";
                txtQuantidade.Text = "";
                txtUa.Text = "";
                txtCodigoDeBarras.Text = "";
                txtCodigoDeBarras.Enabled = false;
                txtQuantidade.Enabled = false;
                txtEndereçoDePicking.Enabled = false;
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

                    txtEndereçoDePicking.Enabled = true;
                    txtEndereçoDePicking.Focus();
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
                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && txtCodigoDeBarras.Text != "" && txtQuantidade.Text != "" && txtEndereçoDePicking.Text != "")
                {
                    e.Handled = true;

                    DataRow[] x = dtAuxProd.Select("Picking=" + txtEndereçoDePicking.Text, "");

                    if (x.Length == 0)
                        MessageBox.Show("Endereço de Picking Inválido ou Endereço Pertence a Outro Produto");                 

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
    }
}