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
    public partial class CLW00003 : Form 
    {
        public CLW00003()
        {
            InitializeComponent();
        }

        private void CLW00003_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
        }

       
        private void txtEnderecoProduto_KeyPress_1(object sender, KeyPressEventArgs e)
        {
              Cursor.Current = Cursors.WaitCursor;
              if (e.KeyChar == (char)Keys.Return && txtCodigoDeBarras.Text != "")
              {
                  Pesquisar();
              }
              Cursor.Current = Cursors.Default;

        }

        DataTable DT;
        private void Pesquisar()
        {
            txtLastro.Text = "";
            txtLastroAltura.Text = "";
            txtCubagemAltura.Text = "";
            txtCubagemLargura.Text = "";
            txtCubagemPesoLiq.Text = "";
            txtCubagemPesoBruto.Text = "";
            txtCubagemComprimento.Text = "";


            try
            {
                DT = new Classes.BLL.Produto().RetornarDadosLogistico(txtCodigoDeBarras.Text.Trim());

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("Produto Não Encontrado", "Atenção");
                    return;
                }

                grd.DataSource = DT;

                if (DT.Rows.Count == 1)
                {
                    txtLastro.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["LASTRO"].ToString()));
                    txtLastroAltura.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["LASTROALTURA"].ToString()));
                    txtCubagemAltura.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["ALTURA"].ToString()));
                    txtCubagemLargura.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["LARGURA"].ToString()));
                    txtCubagemPesoLiq.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["PESOLIQUIDO"].ToString()));
                    txtCubagemPesoBruto.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["PESOBRUTO"].ToString()));
                    txtCubagemComprimento.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[0]["COMPRIMENTO"].ToString()));
                    LblIdProdutoCliente.Text = DT.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    lblIdProduto.Text = DT.Rows[0]["IDPRODUTO"].ToString();

                    txtLastroAltura.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grd_Click(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            txtLastro.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["LASTRO"].ToString()));
            txtLastroAltura.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["LASTROALTURA"].ToString()));
            txtCubagemAltura.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["ALTURA"].ToString()));
            txtCubagemLargura.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["LARGURA"].ToString()));
            txtCubagemPesoLiq.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["PESOLIQUIDO"].ToString()));
            txtCubagemPesoBruto.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["PESOBRUTO"].ToString()));
            txtCubagemComprimento.Text = Helpers.Formatar3CasasDecimais(decimal.Parse(DT.Rows[row]["COMPRIMENTO"].ToString()));
            LblIdProdutoCliente.Text = DT.Rows[row]["COMPRIMENTO"].ToString();
            lblIdProduto.Text = DT.Rows[row]["IDPRODUTO"].ToString();
            txtLastroAltura.Focus();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void txtLastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');
            
        }

        private void txtLastro_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((txtLastro.Text.Replace(",", ".") == "" ? "0" : txtLastro.Text.Replace(",", "."))));
        }

        private void txtLastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLastroAltura.Focus(); 
            }
        }

        private void txtLastroAltura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCubagemAltura.Focus();
        }

        private void txtCubagemAltura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCubagemLargura.Focus();
        }

        private void txtCubagemLargura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCubagemComprimento.Focus();
        }

        private void txtCubagemPesoLiq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnConfirmar.Focus();
        }

        private void txtCubagemPesoBruto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCubagemPesoLiq.Focus();
        }

        private void txtCubagemComprimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCubagemPesoBruto.Focus();
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(lblIdProduto.Text=="" || LblIdProdutoCliente.Text=="")
                    return;

                SistecnoColetor.Classes.DTO.Produto pro = new SistecnoColetor.Classes.DTO.Produto();

                pro.CodigoDeBarras = txtCodigoDeBarras.Text;
                pro.Lastro = txtLastro.Text;
                pro.LastroAltura = txtLastroAltura.Text;
                pro.Altura = txtCubagemAltura.Text;
                pro.Largura = txtCubagemLargura.Text;
                pro.PesoLiquido = txtCubagemPesoLiq.Text;
                pro.PesoBruto = txtCubagemPesoBruto.Text;
                pro.Comprimento = txtCubagemComprimento.Text;
                pro.IdProdutoCliente = LblIdProdutoCliente.Text;
                pro.IdProduto = lblIdProduto.Text;

                new Classes.BLL.Produto().Alterar(pro);

                MessageBox.Show("Operação efetuada com sucesso!", "Opração OK");

                txtLastro.Text = "";
                txtLastroAltura.Text = "";
                txtCubagemAltura.Text = "";
                txtCubagemLargura.Text = "";
                txtCubagemPesoLiq.Text = "";
                txtCubagemPesoBruto.Text = "";
                txtCubagemComprimento.Text = "";
                LblIdProdutoCliente.Text="";
                lblIdProduto.Text= "";
                txtCodigoDeBarras.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    

        private void txtLastroAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');

        }

        private void txtCubagemAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');

        }

        private void txtCubagemLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');

        }

        private void txtCubagemComprimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');

        }

        private void txtCubagemPesoBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');

        }

        private void txtCubagemPesoLiq_KeyPress(object sender, KeyPressEventArgs e)
        {
            Helpers.AceitarDecimais(sender, e, ',');

        }

        private void txtLastroAltura_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((((TextBox)sender).Text.Replace(",", ".") == "" ? "0" : ((TextBox)sender).Text.Replace(",", "."))));

        }

        private void txtCubagemAltura_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((((TextBox)sender).Text.Replace(",", ".") == "" ? "0" : ((TextBox)sender).Text.Replace(",", "."))));

        }

        private void txtCubagemLargura_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((((TextBox)sender).Text.Replace(",", ".") == "" ? "0" : ((TextBox)sender).Text.Replace(",", "."))));

        }

        private void txtCubagemComprimento_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((((TextBox)sender).Text.Replace(",", ".") == "" ? "0" : ((TextBox)sender).Text.Replace(",", "."))));

        }

        private void txtCubagemPesoBruto_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((((TextBox)sender).Text.Replace(",", ".") == "" ? "0" : ((TextBox)sender).Text.Replace(",", "."))));

        }

        private void txtCubagemPesoLiq_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Helpers.Formatar3CasasDecimais(decimal.Parse((((TextBox)sender).Text.Replace(",", ".") == "" ? "0" : ((TextBox)sender).Text.Replace(",", "."))));

        }

        
    }
}