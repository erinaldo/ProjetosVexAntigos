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
    public partial class CLW00004 : Form
    {
        public CLW00004()
        {
            InitializeComponent();
        }

        private void CLW00004_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            lblIdReposicaoRoge.Text = "";
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
            lblChave.Text = txtChaveDocumento.Text;

            try
            {               
                
                D = Classes.BbColetor.RetornarConferenciaPorChave(txtChaveDocumento.Text);

                if (D.Rows.Count == 0)
                {
                    //D = new Classes.WS().ResgatarDocumento(txtChaveDocumento.Text).Tables[0];
                    DataSet ds = new Classes.WS().ResgatarDocumento(txtChaveDocumento.Text);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        txtChaveDocumento.Text = "";

                        lblQuantidadeDeItens.Text = "";
                        lblQuantidadeVolumes.Text = "";
                        txtChaveDocumento.Focus();
                        throw new Exception("Nota Não Encontrada");
                    }

                    lblQuantidadeDeItens.Text = ds.Tables[2].Rows.Count.ToString();
                    lblQuantidadeVolumes.Text = ds.Tables[1].Rows.Count.ToString();
                    lblIdReposicaoRoge.Text = ds.Tables[0].Rows[0][0].ToString();

                }
                //se ja existir
                else
                {
                    lblQuantidadeVolumes.Text = Classes.BbColetor.RetornarDataTable("SELECT COUNT(*) FROM ReposicaoRogeVolume WHERE IdReposicaoRoge IN (select IdReposicaoRoge from  ReposicaoRoge where Chave = '" + txtChaveDocumento.Text + "')").Rows[0][0].ToString();
                    lblQuantidadeDeItens.Text = Classes.BbColetor.RetornarDataTable("SELECT COUNT(*) FROM ReposicaoRogeItem WHERE IdReposicaoRoge IN (select IdReposicaoRoge from  ReposicaoRoge where Chave = '" + txtChaveDocumento.Text + "')").Rows[0][0].ToString();
                    lblIdReposicaoRoge.Text = D.Rows[0][0].ToString();
                }

                btnItens.Enabled = true;
                btnVolumes.Enabled = true;

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
            ConfVolume v = new ConfVolume();
           // v.Dt = D;
            v.Chave = txtChaveDocumento.Text;
            v.IdReposicaoRoge = lblIdReposicaoRoge.Text;
            v.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ConfItens v = new ConfItens();
           // v.Dt = D;
            v.Chave = txtChaveDocumento.Text;
            v.IdReposicaoRoge = lblIdReposicaoRoge.Text;

            v.Show();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                D = Classes.BbColetor.RetornarConferenciaPorChave(txtChaveDocumento.Text);
                br.com.grupologos.www.ReposcicaoRoge wss = new SistecnoColetor.br.com.grupologos.www.ReposcicaoRoge();

                
                
                for (int i = 0; i < D.Rows.Count; i++)
                {
                    
                }
                //wss.GravarConferencia(D);

                Classes.BbColetor.AlterarTransmissao(txtChaveDocumento.Text);
            }
            catch (Exception ex)
            {

                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexão de Internet.");
                else
                    MessageBox.Show(ex.Message);
            }
        }
    }
}