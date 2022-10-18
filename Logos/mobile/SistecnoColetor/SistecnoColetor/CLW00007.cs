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
    public partial class CLW00007 : Form
    {
        public CLW00007()
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
            try
            {           

                if (e.KeyChar == (char)Keys.Return && txtUa.Text != "")
                {
                    e.Handled = true;
                    PesquisarUA();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao Procurar a UA.");
            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }
        }

        DataTable dtUa;
        private void PesquisarUA()
        {
            string sql = "select * from UnidadeDeArmazenagem where cast(IDUnidadeDeArmazenagem as varchar(10))+digito = "+ txtUa.Text;
            DataTable dtaux = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

            if (dtaux.Rows.Count == 0)
            {
                MessageBox.Show("UA Inesistente.");
                txtUa.Text = "";
                txtUa.Focus();
                return;
            }
            else
            {
                if (dtaux.Rows[0]["status"].ToString() == "AGUARDANDO TRANSFERENCIA")
                {
                    MessageBox.Show("UA ainda não foi recebida. Efetue a conferencia no Menu Recebimento de Pallets");
                    txtUa.Text = "";
                    txtUa.Focus();
                    return;
                }
            }


            dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarGuardarPallet(txtUa.Text, "RECEBIDO");
            if (dtUa.Rows.Count == 0)
            {
                DataTable dt = Classes.BdExterno.RetornarDT("SELECT * FROM UNIDADEDEARMAZENAGEM WHERE IDUNIDADEDEARMAZENAGEM=" + Helpers.RetirarDigitoUA(txtUa.Text), VarGlobal.Conexao);

                if (dt.Rows.Count > 0)
                {
                    string msg = "";
                    if (dt.Rows[0]["status"].ToString() == "EM ESTOQUE")
                    {
                        msg = "A UA: " + txtUa.Text + ", já foi guardada em ESTOQUE";   
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
                return;
            }
            else
            {
                lblEndrecoDestino.Text = dtUa.Rows[0]["ENDRECO_DESTINO"].ToString();
                lblIdDepositoPlantaLocalizacao.Text = dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString();
                lblEndPicking.Text = dtUa.Rows[0]["PICKING"].ToString();

                if (lblEndPicking.Text == "")
                {
                    MessageBox.Show("Produto sem Picking.");
                }
                btnConfirmar.Enabled = true;
                txtCodigoDeBarras.Enabled = true;
                txtCodigoDeBarras.Focus();
            }
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
                if (txtCodigoDeBarras.Text != "")
                {
                    new Classes.BLL.Estoque().EntrarComUA(dtUa.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString(), txtCodigoDeBarras.Text);


                    txtCodigoDeBarras.Text = "";
                    txtCodigoDeBarras.Enabled = false;
                    txtUa.Text = "";
                    txtUa.Focus();
                    lblEndrecoDestino.Text = "";
                    lblIdDepositoPlantaLocalizacao.Text = "";
                    lblEndrecoDestino.Text = "";
                    lblIdDepositoPlantaLocalizacao.Text = "";
                    lblEndPicking.Text = "";
                    MessageBox.Show("Registro Gravado com Sucesso");
                }
                else
                    MessageBox.Show("Endereco Incorreto");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    

        private void txtCodigoDeBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return) 
            {
                //se bate o endereco e caso nao tenha o endereco de picking pode gravar no endereco desejado
                //if (txtCodigoDeBarras.Text != "" && txtCodigoDeBarras.Text == lblIdDepositoPlantaLocalizacao.Text || lblEndPicking.Text == "")
                //{


                if (txtCodigoDeBarras.Text != "" && txtUa.Text != "")
                {
                    if (Helpers.EnderecoVazio(txtCodigoDeBarras.Text))
                    {

                        btnConfirmar.Enabled = true;
                        btnConfirmar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Endereço de Destino não esta Vazio", "Atenção");
                        txtCodigoDeBarras.Focus();
                        return;
                    }

                }

                //}
               // Salvar();
            }
        }

      
    }
}
