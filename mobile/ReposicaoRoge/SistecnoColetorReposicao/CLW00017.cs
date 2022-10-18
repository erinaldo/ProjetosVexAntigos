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
    public partial class CLW00017 : Form
    {
        public CLW00017()
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
                DataTable d = new DataTable();

                string sql = "SELECT IDDEPOSITOPLANTALOCALIZACAO FROM PRODUTOCLIENTEREGRA WHERE TIPODEREGRA='PICKING'  AND IDDEPOSITOPLANTALOCALIZACAO=" + txtEndereco.Text;
                //sql += " UNION ";
                //sql += " SELECT IDDEPOSITOPLANTALOCALIZACAO FROM DEPOSITOPLANTALOCALIZACAO WHERE IDDEPOSITOPLANTALOCALIZACAO =" + txtEndereco.Text + " AND PICKING ='SIM' ";

                d = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                if (d.Rows.Count == 0)
                {
                    MessageBox.Show("O endereço não é um endereço de picking");
                    txtEndereco.SelectAll();
                    return;
                }

                
                d = Classes.BdExterno.RetornarDT("SELECT COUNT(*) FROM DEPOSITOPLANTALOCALIZACAO WHERE IDDEPOSITOPLANTALOCALIZACAO=" + txtEndereco.Text, VarGlobal.Conexao);

                if (d.Rows.Count== 0)
                {
                    MessageBox.Show("Endereço Insistente");
                    txtEndereco.SelectAll();
                    return;
                }


                txtCodBarras.Enabled = true;
                txtCodBarras.Focus();

            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao pesquisar o endereço");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void CLW00016_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            txtEndereco.Focus();
        }

        int qtd = 0;
        //private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;


        //        if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtUa.Text != "")
        //        {
        //            Cursor.Current = Cursors.WaitCursor;

        //            e.Handled = true;

        //            // se a ua for zero o usuario sinaliza que o endereço esta vazio
        //            if (txtUa.Text == "0")
        //            {
        //                //validar se esta vazio
        //                if (dt.Rows.Count == 0)
        //                    qtd = 0;
        //                else
        //                    qtd = int.Parse(float.Parse(dt.Compute("SUM(SALDO)", "").ToString()).ToString());

        //                if (qtd != 0)
        //                {
        //                    string sql = "select ua.IDUnidadeDeArmazenagem, ua.Digito  from UnidadeDeArmazenagem ua inner join DepositoPlantaLocalizacao dpl on dpl.idDepositoPlantaLocalizacao = ua.idDepositoPlantaLocalizacao where ua.idDepositoPlantaLocalizacao = " + txtEndereco.Text;
        //                    DataTable aa = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        //                    if (aa.Rows.Count > 0)
        //                        MessageBox.Show("Consta Saldo neste endereço para a Ua: " + aa.Rows[0][0].ToString() + aa.Rows[0][1].ToString());
        //                    else
        //                        MessageBox.Show("Consta Saldo neste endereço.");

        //                    return;
        //                }
        //                else
        //                {
        //                    btnConfirmar.Enabled = true;
        //                    btnConfirmar.Focus();
        //                }
        //            }
        //            else
        //            {
        //                //endereco valido mas sem Ua
        //                if (dt.Rows.Count == 0)
        //                {
        //                    MessageBox.Show("Não Foi encontrado nenhum pallet neste endereço. Vc deve colocar zero na UA.");
        //                    txtUa.SelectAll();
        //                    return;
        //                }


        //                qtd = int.Parse(float.Parse(dt.Compute("SUM(SALDO)", "").ToString()).ToString());
        //                DataRow[] oua = dt.Select("IDUnidadeDeArmazenagem=" + Helpers.RetirarDigitoUA(txtUa.Text), "");

        //                if (oua == null || oua.Length == 0)
        //                {
        //                    //se nao encontrar a ua verifica se ela esta em algum outro endereco
        //                    string sql = "select Dpl.Codigo from UnidadeDeArmazenagem ua ";
        //                    sql += " inner join DepositoPlantaLocalizacao dpl on dpl.idDepositoPlantaLocalizacao = ua.idDepositoPlantaLocalizacao ";
        //                    sql += " where ua.idunidadedearmazenagem = " + Helpers.RetirarDigitoUA(txtUa.Text);

        //                    DataTable dtx = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        //                    if (dtx.Rows.Count > 0)
        //                        MessageBox.Show("A UA: " + txtUa.Text + " não pertence a este endereço e sim ao Endereço: " + dtx.Rows[0][0].ToString());
        //                    else
        //                        MessageBox.Show("A UA: " + txtUa.Text + " não pertence a este endereço e não esta associado a nenhum endereço");

        //                    txtUa.Text = "";
        //                    txtEndereco.SelectAll();
        //                    btnConfirmar.Enabled = false;
        //                    dt = null;
        //                    qtd = 0;
        //                    return;
        //                }
        //                else
        //                {
        //                    string sql = "select Dpl.IdDepositoplantaLocalizacao from UnidadeDeArmazenagem ua ";
        //                    sql += " inner join DepositoPlantaLocalizacao dpl on dpl.idDepositoPlantaLocalizacao = ua.idDepositoPlantaLocalizacao ";
        //                    sql += " where ua.idunidadedearmazenagem = " + Helpers.RetirarDigitoUA(txtUa.Text);

        //                    DataTable dtx = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

        //                    if (dtx.Rows.Count == 0)
        //                    {
        //                        MessageBox.Show("A UA: +" + txtUa.Text + " não pertence.");

        //                        txtUa.Text = "";
        //                        txtEndereco.SelectAll();
        //                        btnConfirmar.Enabled = false;
        //                        dt = null;
        //                        qtd = 0;
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        if (txtEndereco.Text == dtx.Rows[0][0].ToString())
        //                        {
        //                            btnConfirmar.Enabled = true;
        //                            btnConfirmar.Focus();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("O Endereco não esta correto");

        //                            txtUa.Text = "";
        //                            txtEndereco.SelectAll();
        //                            btnConfirmar.Enabled = false;
        //                            dt = null;
        //                            qtd = 0;
        //                            return;
        //                        }
        //                    }

        //                }
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Problema ao pesquisa a UA");
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;

        //    }
        //}

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            txtCodBarras.Text = "";
            txtCodBarras.Enabled = false;
            txtEndereco.Focus();
            txtEndereco.Text = "";
            dataGrid1.Visible = false;


            //try
            //{



            //    DataRow[] oua = dt.Select("IDUnidadeDeArmazenagem=" + Helpers.RetirarDigitoUA(txtUa.Text), "");

            //    string InventarioContagemProduto = BdExterno.RetornarIDTabela("InventarioContagemProduto").ToString();

            //    string sql = "insert into InventarioContagemProduto (IdInventarioContagemProduto, IdInventarioContagem, IdProdutoCliente, IdProdutoEmbalagem, IdDepositoPlantaLocalizacao, IdUnidadeDeArmazenagem,IdUsuario,Quantidade) values ";

            //    if (oua == null || oua.Length == 0)
            //        sql += " (" + InventarioContagemProduto + ", 1, 0, 0, " + txtEndereco.Text + ", 0,2,0)";
            //    else
            //        sql += " (" + InventarioContagemProduto + ", 1, " + oua[0]["IDPRODUTOCLIENTE"].ToString() + ", " + oua[0]["IDPRODUTOEMBALAGEM"].ToString() + ", " + txtEndereco.Text + ", " + Helpers.RetirarDigitoUA(txtUa.Text) + ",2," + int.Parse(float.Parse(oua[0]["SALDO"].ToString()).ToString()) + ")";

            //    BdExterno.Executar(sql, VarGlobal.Conexao);
            //    txtUa.Text = "";
            //    txtEndereco.Text = "";
            //    txtEndereco.SelectAll();
            //    btnConfirmar.Enabled = false;
            //    dt = null;
            //    qtd = 0;

            //    MessageBox.Show("Gravado com sucesso!");

            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Problema ao gravar Produto Inventario.");
            //}
        }

        private void txtCodBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtCodBarras.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;

                    DataTable dtProd = new Classes.BLL.Produto().RetornarDadosLogistico(txtCodBarras.Text);

                    if (dtProd.Rows.Count == 0)
                    {
                        MessageBox.Show("Codigo de Barras Inseistente.");
                        txtCodBarras.SelectAll();
                        return;
                    }

                    string sql = "SELECT IDDEPOSITOPLANTALOCALIZACAO FROM PRODUTOCLIENTEREGRA WHERE IDPRODUTOCLIENTE="+ dtProd.Rows[0]["IDPRODUTOCLIENTE"].ToString() +"  AND TIPODEREGRA='PICKING'  AND IDDEPOSITOPLANTALOCALIZACAO=" + txtEndereco.Text;
       
                    DataTable d = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (d.Rows.Count == 0)
                    {
                        MessageBox.Show("Endereco de Picking Invalido para o Produto. A Grid apresenta os endereços de picking corretos.");

                        sql = " SELECT DPL.CODIGO FROM PRODUTOCLIENTEREGRA PCR INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = PCR.IDDEPOSITOPLANTALOCALIZACAO WHERE IDPRODUTOCLIENTE = " + dtProd.Rows[0]["IDPRODUTOCLIENTE"].ToString() + " order by 1";

                        dataGrid1.DataSource = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);
                        dataGrid1.Visible = true;

                        
                        sql = "INSERT INTO CONFERENCIAPICKING (IdConferenciaPicking, IdDepositoPlantaLocalizacao, CodigoDeBarras, DataHora, Divergencia, IdUsuario )";
                        sql += "VALUES(" + BdExterno.RetornarIDTabela("CONFERENCIAPICKING").ToString() + ", " + txtEndereco.Text +", '"+txtCodBarras.Text+"', GetDate(), 'SIM', "+VarGlobal.Usuario.IDUsuario+" )";
                        Classes.BdExterno.Executar(sql, VarGlobal.Conexao);
                        return;
                    }
                    else
                    {

                        sql = "INSERT INTO CONFERENCIAPICKING (IdConferenciaPicking, IdDepositoPlantaLocalizacao, CodigoDeBarras, DataHora, Divergencia , IdUsuario )";
                        sql += "VALUES(" + BdExterno.RetornarIDTabela("CONFERENCIAPICKING").ToString() + ", " + txtEndereco.Text + ", '" + txtCodBarras.Text + "', GetDate(), 'NAO', "+ VarGlobal.Usuario.IDUsuario +" )";
                        Classes.BdExterno.Executar(sql, VarGlobal.Conexao);

                        MessageBox.Show("Endereço de Picking e Produtos Corretos.");
                        txtCodBarras.Text = "";
                        txtCodBarras.Enabled = false;
                        txtEndereco.Focus();
                        txtEndereco.Text = "";
                        dataGrid1.Visible = false;
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