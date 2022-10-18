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
    public partial class CLW00023 : Form
    {
        public CLW00023()
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

                //txtUa.Text = "9100000099997178965247054590001";
                //txtLoteValidade.Text = "912017051308237";
                //txtCodigoDeBarras.Text = "17896524705459";
                comboBox1.SelectedIndex = 0;

              
            }
            catch (Exception ecx)
            {
                MessageBox.Show(ecx.Message);
            }
        }
        
        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtUa.Text != "")
            {
                e.Handled = true;
                
                lblTempCodigoBarras.Text = "";
                lblTempEtiqueta.Text = "";
                lblTempQuantidade.Text = "";


                if (txtUa.Text.Length < 20)
                {
                    PesquisarUA();
                }
                else
                {
                    lblTempCodigoBarras.Text = "";
                    lblTempEtiqueta.Text = "";
                    lblTempQuantidade.Text = "";
                    //lblValidade.Text = "";
                    //lblLote.Text = "";
                    txtLoteValidade.Enabled = true;
                    txtLoteValidade.Focus();
                }
            }
        }

        string sql = "";

        private void FormatarDadosEtiqueta()
        {
            try
            {
                lblTempEtiqueta.Text = txtUa.Text;
                txtUa.Text = int.Parse(lblTempEtiqueta.Text.Substring(2, 11)).ToString();
                lblDigito.Text = lblTempEtiqueta.Text.Substring(12, 1);
                lblTempCodigoBarras.Text = lblTempEtiqueta.Text.Substring(13, 14);
                lblTempQuantidade.Text = int.Parse(lblTempEtiqueta.Text.Substring(27, 4)).ToString();
                //lblValidade.Text = lblTempEtiqueta.Text.Substring(31, 8);

                DateTime? d = null;

                try
                {
                    if (txtLoteValidade.Text.Length > 0)
                    {
                        try
                        {
                            //lblValidade.Text = txtLoteValidade.Text.Substring(2, 8);
                           // d = new DateTime(int.Parse(lblValidade.Text.Substring(0, 4)), int.Parse(lblValidade.Text.Substring(4, 2)), int.Parse(lblValidade.Text.Substring(6, 2)));
                            //lblValidade.Text = ((DateTime)d).ToString("dd/MM/yyyy");
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                           // lblLote.Text = txtLoteValidade.Text.Substring(10, 5);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                //lblLote.Text = txtLoteValidade.Text.Substring(10, 4);
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Etiqueta fora do formato: " + lblTempEtiqueta.Text);
                    txtLoteValidade.Text = "";
                    txtLoteValidade.Enabled = false;
                    //lblLote.Text = "";
                    lblTempCodigoBarras.Text = "";
                    lblDigito.Text = "";
                    return;
                }

                // verifica se a Ua ja Existe
                sql = "SELECT IDUNIDADEDEARMAZENAGEM FROM UNIDADEDEARMAZENAGEM WHERE (CAST(IDUNIDADEDEARMAZENAGEM AS VARCHAR(20))) ='" + Helpers.RetirarDigitoUA(txtUa.Text) + "'";
                if (Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao).Rows.Count > 0)
                {

                    DataTable dt = Classes.BdExterno.RetornarDT("SELECT * FROM UNIDADEDEARMAZENAGEM WHERE IDUNIDADEDEARMAZENAGEM=" + Helpers.RetirarDigitoUA(txtUa.Text), VarGlobal.Conexao);

                    if (dt.Rows.Count > 0)
                    {
                        string msg = "";
                        if (dt.Rows[0]["status"].ToString() == "EM ESTOQUE")
                        {
                            msg = "A UA: " + txtUa.Text + ", já foi guardada e consta em ESTOQUE";
                        }

                        if (dt.Rows[0]["status"].ToString() == "AGUARDANDO TRANSFERENCIA")
                        {
                            msg = "A UA: " + txtUa.Text + ", ainda não foi RECEBIDA/CONFERIDA.";
                        }

                        if (dt.Rows[0]["status"].ToString() == "RECEBIDO")
                        {
                            msg = "A UA: " + txtUa.Text + ", ainda já foi RECEBIDA.";
                        }

                        if (msg == "")
                        {
                            msg = "Pallet não Encontrado";
                        }

                        MessageBox.Show(msg, "ATENÇÂO");
                    }
                    else
                    {
                        MessageBox.Show("Ua Ja Existente.");
                    }

                    return;
                }

                txtCodigoDeBarras.Enabled = true;
                txtCodigoDeBarras.Focus();

            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("sqlexc"))
                {
                    MessageBox.Show("Problema na Instrução Sql: " + sql);
                }
                else
                    MessageBox.Show(ex.Message);
            }
        }

        DataTable dtUa;
        private void PesquisarUA()
        {

            dtUa = new Classes.BLL.UnidadeDeArmazenagem().Retornar(txtUa.Text, "AGUARDANDO TRANSFERENCIA");


            if (dtUa.Rows.Count == 0)
            {
                DataTable dt = Classes.BdExterno.RetornarDT("SELECT * FROM UNIDADEDEARMAZENAGEM WHERE IDUNIDADEDEARMAZENAGEM=" + Helpers.RetirarDigitoUA(txtUa.Text), VarGlobal.Conexao);

                if (dt.Rows.Count > 0)
                {
                    string msg = "";
                    if (dt.Rows[0]["status"].ToString() == "EM ESTOQUE")
                    {
                        msg = "A UA: " + txtUa.Text + ", já foi guardada e consta em ESTOQUE";
                    }

                    if (dt.Rows[0]["status"].ToString() == "AGUARDANDO TRANSFERENCIA")
                    {
                        msg = "A UA: " + txtUa.Text + ", ainda não foi RECEBIDA/CONFERIDA.";
                    }

                    if (dt.Rows[0]["status"].ToString() == "RECEBIDO")
                    {
                        msg = "A UA: " + txtUa.Text + ", ainda já foi RECEBIDA.";
                    }

                    if (msg == "")
                    {
                        msg = "Pallet não Encontrado";
                    }

                    MessageBox.Show(msg, "ATENÇÂO");
                }
                else
                {
                    MessageBox.Show("Pallet não Encontrado", "Atenção");
                }
                txtUa.Text = "";
                txtUa.Focus();
            }
            else
            {
                txtCodigoDeBarras.Enabled = true;
                txtCodigoDeBarras.Focus();
            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            if (txtQuantidadeConferida.Text != "")
                Salvar();
            else
            {

                MessageBox.Show("Favor informar a quantidade.");
                txtQuantidadeConferida.Focus();

            }
        }

     
        private void Salvar()
        {
            try
            {
                float qtd = float.Parse(txtQuantidadeConferida.Text);

                if (int.Parse(txtQuantidadeConferida.Text) > 0 && int.Parse(txtQuantidadeConferida.Text) == Convert.ToInt32(qtd))
                {
                    new Classes.BLL.Movimentacao().GravarMovimentacao(dtUa.Rows[0]["IDunidadeDeArmazenagem"].ToString(), dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString(), lblProdutoEmbalagem.Text, txtQuantidadeConferida.Text, lblFator.Text, "2", "ENTRADA", lblIdProdutoCliente.Text, comboBox1.SelectedItem.ToString());

                    lblProduto.Text = "";
                    // lblCodigoDeBarras.Text = "";
                    txtQuantidadeConferida.Text = "";
                    txtQuantidadeConferida.Enabled = false;
                    txtCodigoDeBarras.Text = "";
                    txtCodigoDeBarras.Enabled = false;
                    txtUa.Text = "";
                    lblFator.Text = "";
                    lblProdutoEmbalagem.Text = "";
                    lblTempCodigoBarras.Text = "";
                    lblTempEtiqueta.Text = "";
                    lblTempQuantidade.Text = "";
                    
                    lblDigito.Text = "";
                    txtLoteValidade.Text = "";
                    txtLoteValidade.Enabled = false;
                    lblTempCodigoBarras.Text = "";

                    txtUa.Focus();
                    MessageBox.Show("Registro Gravado com Sucesso");
                }
                else
                {
                    MessageBox.Show("Quantidade Divverge da quantidade da UA");
                    txtQuantidadeConferida.Focus();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao atualizar a UA: " + txtUa.Text);
            }
        }




        private void txtCodigoDeBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtCodigoDeBarras.Text != "")
            {
                PesquisarProduto();
            }
        }

        DataTable dtProd;
        private void PesquisarProduto()
        {
            if (lblTempCodigoBarras.Text != "")
            {
                if (txtCodigoDeBarras.Text != lblTempCodigoBarras.Text)
                {
                    txtCodigoDeBarras.Focus();
                    txtCodigoDeBarras.Text = "";
                    MessageBox.Show("Produto lido inconsistente com a etiqueta.");
                    return;
                }
            }

        
                dtProd = new Classes.BLL.Produto().RetornarProduto(txtCodigoDeBarras.Text);
           


            if (dtProd.Rows.Count > 0)
            {
                //lblCodigoDeBarras.Text = ;
                lblProduto.Text = dtProd.Rows[0]["CODIGOPRODUTO"].ToString() + "-" + dtProd.Rows[0]["DESCRICAO"].ToString();
                lblFator.Text = (dtProd.Rows[0]["UnidadeDoCliente"].ToString() == "" ? "1" : dtProd.Rows[0]["UnidadeDoCliente"].ToString());
                lblProdutoEmbalagem.Text = dtProd.Rows[0]["IDPRODUTOEMBALAGEM"].ToString();
                lblIdProdutoCliente.Text = dtProd.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                txtQuantidadeConferida.Text = "";
                txtQuantidadeConferida.Enabled = true;
                txtQuantidadeConferida.Focus();
                btnConfirmar.Enabled = true;
            }
            else
            {
                txtCodigoDeBarras.Focus();
                txtCodigoDeBarras.Text = "";
                MessageBox.Show("Produto não encontado.");
            }
        }

        private void txtQuantidadeConferida_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantidadeConferida_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CLW00006_KeyDown(object sender, KeyEventArgs e)
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

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                FormatarDadosEtiqueta();

            }
        }

        private void txtUa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLoteValidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantidadeConferida_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Helpers.AceitarNumerosInteiros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                btnConfirmar.Enabled = true;
                btnConfirmar.Focus();
            }
            //    Salvar();
        }
    }
}