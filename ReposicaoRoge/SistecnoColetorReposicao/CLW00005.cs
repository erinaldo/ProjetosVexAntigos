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
    public partial class CLW00005 : Form
    {    

        public CLW00005()
        {            
            InitializeComponent();
        }

        private void CLW00005_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            textBox1.Focus();
            dtendCad = null;
        }

        private void grd_Click_1(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            lblIdProdutoCliente.Text = grd[row, 3].ToString();
            //btnConfirmar.Enabled = true;
            txtEndereco.Enabled = true;
            txtEndereco.Focus();
            CarragarGrdiCadastrados();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                new Classes.BLL.Produto().AlterarEnderecoDePicking(lblIdProdutoCliente.Text, txtEndereco.Text);
                lblIdProdutoCliente.Text = "";
                panel1.Visible = false;
                grd.DataSource = null;
                txtEndereco.Text = "";
                txtEndereco.Enabled = false;
                textBox1.Text = "";
                textBox1.Focus();

                MessageBox.Show("Operação Efetuada com Sucesso!", "Coletor Sistecno");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {    
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                Pesquisar();
            }
        }


        public void Pesquisar()
        {
            if (textBox1.Text == "")
                return;


            DataTable dt = new Classes.BLL.Produto().RetornarProdutoByCBandCodigo(textBox1.Text);

            if (dt.Rows.Count > 1)
            {
                panel1.Visible = true;                
                lblIdProdutoCliente.Text = "";
                panel1.Visible = true;
                grd.DataSource = dt;

            }
            else if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Produto não encontrado");
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                lblIdProdutoCliente.Text = dt.Rows[0]["IDProdutoCliente"].ToString();
                lblCodigoProduto.Text = dt.Rows[0]["CODIGO"].ToString();                                
                txtEndereco.Enabled = true;
                txtEndereco.Focus();
                CarragarGrdiCadastrados();

            }
        }
        DataTable dtendCad;
        private void CarragarGrdiCadastrados()
        {
            dtendCad = new Classes.BLL.DepositoPlantaLocalizacao().RetornarEnderecosPicking(lblIdProdutoCliente.Text);

            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("CODIGO");
            dtGrid.Columns.Add("ENDERECO");

            for (int i = 0; i < dtendCad.Rows.Count; i++)
            {
                DataRow o = dtGrid.NewRow();
                o[0] = dtendCad.Rows[i]["IDDEPOSITOPLANTALOCALIZACAO"];
                o[1] = dtendCad.Rows[i]["CODIGO"].ToString();
                dtGrid.Rows.Add(o);
            }

            grdEnderecosCadastrados.DataSource = dtGrid;
        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                ChecarEndereco();         
            }
        }

        private void ChecarEndereco()
        {
            try
            {


                DataTable dt = new Classes.BLL.DepositoPlantaLocalizacao().RetornarDepositoPlantaLocalizacao(txtEndereco.Text);

                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["IDPRODUTOCLIENTE"].ToString() == "")// esta vazia
                    {

                        if (dtendCad.Rows.Count > 0 && int.Parse(dtendCad.Compute("COUNT(IDDEPOSITOPLANTALOCALIZACAO)", "IDDEPOSITOPLANTALOCALIZACAO=" + txtEndereco.Text).ToString()) > 0)
                        {
                            MessageBox.Show("Endereço já cadastrado para outro produto.");
                            txtEndereco.Text = "";
                            txtEndereco.Focus();
                            return;
                        }
                        else
                        {
                            string sql = "INSERT INTO PRODUTOCLIENTEREGRA(IdProdutoClienteRegra, IdProdutoCliente, IdDepositoPlantaLocalizacao, TipoDeRegra)";
                            sql += "VALUES(@IDPRODUTOCLIENTEREGRA@, " + lblIdProdutoCliente.Text + ", " + txtEndereco.Text + ", 'PICKING')";
                            new Classes.BLL.DepositoPlantaLocalizacao().GravarPicking(sql, true);                            
                            txtEndereco.Text = "";                            
                            panel1.Visible = false;
                            CarragarGrdiCadastrados();
                            txtEndereco.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Endereço já alocado para outro produto.", "Atenção");
                        return;

                    }

                }
                else
                {
                    MessageBox.Show("Endereço não encontrado", "Atenção");
                    return;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    

        private void CLW00005_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

            int row = grdEnderecosCadastrados.CurrentCell.RowNumber;
            string sql = "DELETE FROM PRODUTOCLIENTEREGRA WHERE IDDEPOSITOPLANTALOCALIZACAO="+ grd[row, 0].ToString() + " AND IDPRODUTOCLIENTE=" + lblIdProdutoCliente.Text;
            new Classes.BLL.DepositoPlantaLocalizacao().GravarPicking(sql, false);
        }
    }
}