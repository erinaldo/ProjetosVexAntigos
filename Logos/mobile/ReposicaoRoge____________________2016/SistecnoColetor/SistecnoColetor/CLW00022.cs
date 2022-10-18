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
    public partial class CLW00022 : Form
    {
        public CLW00022()
        {
            InitializeComponent();
        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void txtEndereco_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                e.Handled = true;
                Pesquisar();
                Cursor.Current = Cursors.Default;

            }
        }

        DataTable dt;
        private void Pesquisar()
        {
            try
            {



                DataTable d = Classes.BdExterno.RetornarDT("SELECT COUNT(*) FROM DEPOSITOPLANTALOCALIZACAO WHERE IDDEPOSITOPLANTALOCALIZACAO=" + txtEndereco.Text, VarGlobal.Conexao);

                if (d.Rows[0][0].ToString() == "0")
                {
                    MessageBox.Show("Endereço Insistente");
                    txtEndereco.SelectAll();
                    return;
                }


                dt = new DataTable();
                string SQL = "EXEC PCR_PROCURA_UA_ENDERECO " + txtEndereco.Text;
               dt = Classes.BdExterno.RetornarDT(SQL, VarGlobal.Conexao);

                //if (dt.Rows.Count == 0)
                //{
                //    MessageBox.Show("Endereco não encontrado");
                //    txtEndereco.SelectAll();
                //    dt = null;
                //    qtd = 0;
                //    return;
                //}
                txtUa.Enabled = true;
                txtUa.Focus();
                txtUa.Text = "0";
                txtUa.SelectAll();

            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao pesquisar o endereço");
            }
        }

        private void CLW00016_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            PrCliente = new DataTable();
            txtEndereco.Focus();
        }

        int qtd = 0;
        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtUa.Text != "")
                {
                    e.Handled = true;

                    string sql = "Select count(*) from UnidadeDeArmazenagem where IddepositoplantaLocalizacao =" + txtEndereco.Text + " and idUnidadeDeArmazenagem=" + Helpers.RetirarDigitoUA(txtUa.Text);

                    /*if (int.Parse(Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao).Rows[0][0].ToString()) == 0)
                    {
                        DialogResult result = MessageBox.Show("Existe uma inconsitencia da ua neste endereço. Deseja Continuar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (result == DialogResult.No)
                        {
                            txtCodigoDeBarras.Text = "";
                            txtUa.Text = "";
                            txtEndereco.Text = "";
                            txtQuantidade.Text = "";

                            txtCodigoDeBarras.Enabled = false;
                            txtUa.Enabled = false;
                            txtEndereco.Focus();
                            btnConfirmar.Enabled = false;
                            txtQuantidade.Enabled = false;

                            return;
                        }
                    }*/

                    txtCodigoDeBarras.Enabled = true;
                    txtCodigoDeBarras.Focus();


                }

                /*if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtUa.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    e.Handled = true;

                    
                    // se a ua for zero o usuario sinaliza que o endereço esta vazio
                    if (txtUa.Text == "0")
                    {
                        //validar se esta vazio
                        if (dt.Rows.Count == 0)
                            qtd = 0;
                        else
                            qtd = int.Parse(float.Parse(dt.Compute("SUM(SALDO)", "").ToString()).ToString());

                        if (qtd != 0)
                        {
                            string sql = "SELECT UA.IDUNIDADEDEARMAZENAGEM, UA.DIGITO ";
                            sql += " FROM UNIDADEDEARMAZENAGEM UA  ";
                            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO  ";
                            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM ";
                            sql += " WHERE UA.IDDEPOSITOPLANTALOCALIZACAO = " + txtEndereco.Text;
                            sql += " AND UAL.SALDO>0";
                            DataTable aa = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                            if (aa.Rows.Count > 0)
                                MessageBox.Show("Consta Saldo neste endereço para a Ua: " + aa.Rows[0][0].ToString() + aa.Rows[0][1].ToString());
                            else
                                MessageBox.Show("Consta Saldo neste endereço.");

                            return;
                        }
                        else
                        {
                            //btnConfirmar.Enabled = true;
                            //btnConfirmar.Focus();
                        }
                    }
                    else
                    {
                        //endereco valido mas sem Ua
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Não Foi encontrado nenhum pallet neste endereço. Você deve colocar (0)zero na UA.");
                            txtUa.SelectAll();
                            return;
                        }


                        qtd = int.Parse(float.Parse(dt.Compute("SUM(SALDO)", "").ToString()).ToString());
                        DataRow[] oua = dt.Select("IDUnidadeDeArmazenagem=" + Helpers.RetirarDigitoUA(txtUa.Text), "");

                        if (oua == null || oua.Length == 0)
                        {
                            //se nao encontrar a ua verifica se ela esta em algum outro endereco
                            string sql = "select Dpl.Codigo from UnidadeDeArmazenagem ua ";
                            sql += " inner join DepositoPlantaLocalizacao dpl on dpl.idDepositoPlantaLocalizacao = ua.idDepositoPlantaLocalizacao ";
                            sql += " where ua.idunidadedearmazenagem = " + Helpers.RetirarDigitoUA(txtUa.Text);

                            DataTable dtx = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                            if (dtx.Rows.Count > 0)
                                MessageBox.Show("A UA: " + txtUa.Text + " não pertence a este endereço e sim ao Endereço: " + dtx.Rows[0][0].ToString());
                            else
                                MessageBox.Show("A UA: " + txtUa.Text + " não pertence a este endereço e não esta associado a nenhum endereço");

                            txtUa.Text = "";
                            txtEndereco.SelectAll();
                            btnConfirmar.Enabled = false;
                            dt = null;
                            qtd = 0;
                            return;
                        }
                        else
                        {
                            string sql = "select Dpl.IdDepositoplantaLocalizacao from UnidadeDeArmazenagem ua ";
                            sql += " inner join DepositoPlantaLocalizacao dpl on dpl.idDepositoPlantaLocalizacao = ua.idDepositoPlantaLocalizacao ";
                            sql += " where ua.idunidadedearmazenagem = " + Helpers.RetirarDigitoUA(txtUa.Text);

                            DataTable dtx = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                            if (dtx.Rows.Count == 0)
                            {
                                MessageBox.Show("A UA: +" + txtUa.Text + " não pertence.");

                                txtUa.Text = "";
                                txtEndereco.SelectAll();
                                btnConfirmar.Enabled = false;
                                dt = null;
                                qtd = 0;
                                return;
                            }
                            else
                            {
                                if (txtEndereco.Text == dtx.Rows[0][0].ToString())
                                {
                                    btnConfirmar.Enabled = true;
                                    btnConfirmar.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("O Endereco não esta correto");

                                    txtUa.Text = "";
                                    txtEndereco.SelectAll();
                                    btnConfirmar.Enabled = false;
                                    dt = null;
                                    qtd = 0;
                                    return;
                                }
                            }

                        }
                    }}*/
                
            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao pesquisa a UA");
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Pega a última contagem aberta
                string com = "SELECT * FROM INVENTARIOCONTAGEM WHERE CONVERT(DATE, DATA, 103) =  CONVERT(DATE, GETDATE(), 103) AND STATUS='ABERTO'";
                DataTable dtx = Classes.BdExterno.RetornarDT(com, VarGlobal.Conexao);
                string idInventario="", idIvnContagem="";

                //caso nao tenha inventaria aberto no dia abre
                if (dtx.Rows.Count == 0)
                {
                    idInventario = BdExterno.RetornarIDTabela("Inventario").ToString();
                    idIvnContagem = BdExterno.RetornarIDTabela("InventarioContagem").ToString();

                    com = "INSERT INTO INVENTARIO (IDINVENTARIO, IDFILIAL, IDCLIENTE, DATA) ";
                    com += "VALUES(" + idInventario + ", " + VarGlobal.Usuario.UltimaFilial + ", 3, GETDATE()) ;";

                    com += "INSERT INTO INVENTARIOCONTAGEM (IDINVENTARIOCONTAGEM, IDINVENTARIO, DESCRICAO)";
                    com += "VALUES ("+idIvnContagem+", "+idInventario+", 'CONTAGEM GERAL')";

                    BdExterno.Executar(com, VarGlobal.Conexao);

                }
                else
                {
                    idInventario = dtx.Rows[0]["IdInventario"].ToString();
                    idIvnContagem = dtx.Rows[0]["IdInventarioContagem"].ToString();
                }



                DataRow[] oua = dt.Select("IDUnidadeDeArmazenagem=" + Helpers.RetirarDigitoUA(txtUa.Text), "");

                string InventarioContagemProduto = BdExterno.RetornarIDTabela("InventarioContagemProduto").ToString();

                string sql = "insert into InventarioContagemProduto (IdInventarioContagemProduto, IdInventarioContagem, IdProdutoCliente, IdProdutoEmbalagem, IdDepositoPlantaLocalizacao, IdUnidadeDeArmazenagem,IdUsuario,Quantidade) values ";

                //if (oua == null || oua.Length == 0)
                //    sql += " (" + InventarioContagemProduto + ", "+idIvnContagem+", 0, 0, " + txtEndereco.Text + ", 0,2,0)";
                //else
                sql += " (" + InventarioContagemProduto + ", " + idIvnContagem + ", " + PrCliente.Rows[0]["IDPRODUTOCLIENTE"].ToString() + ", " + PrCliente.Rows[0]["IDPRODUTOEMBALAGEM"].ToString() + ", " + txtEndereco.Text + ", " + Helpers.RetirarDigitoUA(txtUa.Text) + ",2," + int.Parse(txtQuantidade.Text).ToString() + ")";

                BdExterno.Executar(sql, VarGlobal.Conexao);
                txtUa.Text = "";
                txtQuantidade.Text = "";
                txtCodigoDeBarras.Text = "";

                txtUa.Enabled = false;
                txtQuantidade.Enabled = false;
                txtCodigoDeBarras.Enabled = false;

                txtEndereco.Text = "";
                txtEndereco.SelectAll();
                btnConfirmar.Enabled = false;
                dt = null;
                PrCliente = null;
                qtd = 0;

                MessageBox.Show("Gravado com sucesso!");

            }
            catch (Exception)
            {

                MessageBox.Show("Problema ao gravar Produto Inventario.");
            }
        }

        DataTable PrCliente;
        private void txtCodigoDeBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                 Cursor.Current = Cursors.WaitCursor;
                 if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtUa.Text != "" && txtCodigoDeBarras.Text!="")
                 {
                     string sql = "select * from Produto p  inner join ProdutoEmbalagem pe on pe.idproduto = p.idproduto inner join ProdutoCliente pc on pc.idprodutocliente= pe.idprodutocliente where p.codigodebarras ='"+txtCodigoDeBarras.Text+"'";
                     PrCliente = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                     if (PrCliente.Rows.Count == 0)
                     {
                         txtCodigoDeBarras.SelectAll();
                         throw new Exception("Produto Invalido ou Insistente");                         
                     }
                     else
                     {
                         txtQuantidade.Enabled = true;
                         txtQuantidade.Text = "0";
                         txtQuantidade.SelectAll();
                         txtQuantidade.Focus();
                     }
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

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtUa.Text != "" && txtCodigoDeBarras.Text != "" && txtQuantidade.Text !="")
                {
                    int x = int.Parse(txtQuantidade.Text);
                    btnConfirmar.Enabled = true;
                    btnConfirmar.Focus();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Quantidade invalida. Verifique o valor digitado.");
                txtQuantidade.SelectAll();
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }
    }
}