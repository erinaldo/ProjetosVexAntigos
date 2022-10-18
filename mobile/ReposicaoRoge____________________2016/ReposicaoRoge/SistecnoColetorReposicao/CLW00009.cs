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
    public partial class CLW00009 : Form
    {
        public CLW00009()
        {
            InitializeComponent();
        }

        private void CLW00004_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtChaveDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtChaveDocumento.Text != "")
                    Pesquisar();
            }
            catch (Exception)
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        DataTable D;
        private void Pesquisar()
        {
            try
            {
                D = new Classes.BLL.Documento().retornarConferenciaByChave(txtChaveDocumento.Text);
                try
                {
                    if (D.Rows.Count > 0)
                    {
                        lblEmissao.Text = DateTime.Parse( D.Rows[0]["DataDeEmissao"].ToString()).ToShortDateString();
                        lblNumeroDocumentp.Text = D.Rows[0]["NUMERO"].ToString();
                        lblQuantidadeDeItens.Text = D.Rows[0]["DESTINATARIO"].ToString();
                        btnConfirmar.Enabled = true;

                        string existe = Classes.BbColetor.RetornarDataTable("SELECT COUNT(IDDOCUMENTO) FROM DEVOLUCAO WHERE IDDOCUMENTO=" + D.Rows[0]["IdDocumento"].ToString()).Rows[0][0].ToString();
                        
                        
                        if (existe == "0")
                        {
                            List<string> lcom = new List<string>();
                            for (int i = 0; i < D.Rows.Count; i++)
                            {
                                lcom.Add("INSERT INTO DEVOLUCAO VALUES (" + D.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + D.Rows[i]["CHAVE"].ToString() + "', " + D.Rows[i]["IDPRODUTO"].ToString() + ", " + D.Rows[i]["IDPRODUTOEMBALAGEM"].ToString() + ", '" + D.Rows[i]["CODIGODEBARRAS"].ToString() + "', " + D.Rows[i]["QUANTIDADE"].ToString().Replace(",", ".") + ")");
                            }
                            Classes.BbColetor.excSql(lcom);
                        }

                    }
                    else
                    {
                        lblEmissao.Text = "";
                        lblNumeroDocumentp.Text = "";
                        lblQuantidadeDeItens.Text = "";
                        txtChaveDocumento.Focus();
                        txtChaveDocumento.Text = "";
                        btnConfirmar.Enabled = false;
                    }
                }
                catch (Exception)
                {
                }


                if (D.Rows.Count == 0)
                    throw new Exception("Documento Não Encontrado!");
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexão de Internet.");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ConferenciaItem v = new ConferenciaItem();
            v.Dt = D;
            v.Chave = txtChaveDocumento.Text;
            v.IdDocumento = int.Parse(D.Rows[0]["IdDocumento"].ToString());
            v.Show();
        }       

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {       
            }
            catch (Exception ex)
            {

                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexão de Internet.");
                else
                    MessageBox.Show(ex.Message);
            }
        }

       
        private void btnConfirmar_Click_2(object sender, EventArgs e)
        {
            ConferenciaItem v = new ConferenciaItem();
            v.Dt = D;
            v.Chave = txtChaveDocumento.Text;
            v.IdDocumento = int.Parse(D.Rows[0]["IdDocumento"].ToString());
            v.Show();
        }

        private void CLW00009_Activated(object sender, EventArgs e)
        {
            //txtChaveDocumento.Text = "";
            txtChaveDocumento.Focus();
            lblEmissao.Text = "";
            lblNumeroDocumentp.Text = "";
            lblQuantidadeDeItens.Text = "";
            btnConfirmar.Enabled = false;
        }
    }
}