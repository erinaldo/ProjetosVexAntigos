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
                        throw new Exception("Nota Não Encontrada, OU JÁ TRANSMITIDA.");
                    }

                    lblQuantidadeDeItens.Text = ds.Tables[2].Rows.Count.ToString();
                    lblQuantidadeVolumes.Text = ds.Tables[1].Rows.Count.ToString();
                    lblIdReposicaoRoge.Text = ds.Tables[0].Rows[0][0].ToString();
                    lblClienteEspecial.Text = ds.Tables[0].Rows[0]["ClienteEspecial"].ToString();

                }
                //se ja existir
                else
                {
                    lblQuantidadeVolumes.Text = Classes.BbColetor.RetornarDataTable("SELECT COUNT(*) FROM ReposicaoRogeVolume WHERE IdReposicaoRoge IN (select IdReposicaoRoge from  ReposicaoRoge where Chave = '" + txtChaveDocumento.Text + "')").Rows[0][0].ToString();
                    lblQuantidadeDeItens.Text = Classes.BbColetor.RetornarDataTable("SELECT COUNT(*) FROM ReposicaoRogeItem WHERE IdReposicaoRoge IN (select IdReposicaoRoge from  ReposicaoRoge where Chave = '" + txtChaveDocumento.Text + "')").Rows[0][0].ToString();
                    lblIdReposicaoRoge.Text = D.Rows[0][0].ToString();

                    if (D.Rows[0]["TRANSMITIDO"].ToString() == "SIM")
                    {
                        btnVolumes.Enabled = false;
                        btnItens.Enabled = false;
                        btnEnviar.Enabled = false;
                        throw new Exception("Documento já Enviado para o Auditor.");
                    }
                }

                if (lblQuantidadeVolumes.Text != "")
                {
                    btnItens.Enabled = true;
                    btnVolumes.Enabled = true;
                    btnEnviar.Enabled = true;
                }

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
                Cursor.Current = Cursors.WaitCursor;

                List<DataTable> ds = Classes.BbColetor.RetornarConferenciaCompleta(txtChaveDocumento.Text);
                ReposicaoInterno.ReposicaoRogeInterno wss = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();

                DataTable dtNota = ds[0];
                DataTable dtVolume = ds[1];
                DataTable dtItem = ds[2];

                SistecnoColetor.ReposicaoInterno.NotaFiscal nf = new SistecnoColetor.ReposicaoInterno.NotaFiscal();
                nf.Chave = txtChaveDocumento.Text;
                nf.Usuario = VarGlobal.Usuario.Login;
                nf.IdNotaFiscal = dtNota.Rows[0]["IdReposicaoRoge"].ToString();

                List<SistecnoColetor.ReposicaoInterno.Vol> lvol = new List<SistecnoColetor.ReposicaoInterno.Vol>();

                for (int i = 0; i < dtVolume.Rows.Count; i++)
                {
                    SistecnoColetor.ReposicaoInterno.Vol v = new SistecnoColetor.ReposicaoInterno.Vol();
                    v.CodigoDeBarras = dtVolume.Rows[i]["CodigoDeBarras"].ToString();
                    v.IdVolume = dtVolume.Rows[i]["IdReposicaoRogeVolume"].ToString();
                    lvol.Add(v);
                }


                nf.Volumes = lvol.ToArray();


                List<SistecnoColetor.ReposicaoInterno.It> lit = new List<SistecnoColetor.ReposicaoInterno.It>();
                DataView view = new DataView(dtItem);
                DataTable distinctValues = view.ToTable(true, "IdConferenciaItem", "PerteceANota");

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    if (distinctValues.Rows[i]["PerteceANota"].ToString() == "SIM")
                    {

                        string SAUX = "SELECT CodigoDeBarras  FROM ReposicaoRogeCB WHERE IdReposicaoRogeItem=" + distinctValues.Rows[i]["IdConferenciaItem"].ToString() + " ORDER BY QuantidadeEmbalagem ASC ";

                        SistecnoColetor.ReposicaoInterno.It it = new SistecnoColetor.ReposicaoInterno.It();

                        DataTable dc = Classes.BbColetor.RetornarDataTable(SAUX);
                        if (dc.Rows.Count > 0)
                        {
                            it.IdItem = distinctValues.Rows[i]["IdConferenciaItem"].ToString();
                            it.CodigoDeBarras = dc.Rows[0]["CodigoDeBarras"].ToString();
                            it.PertenceAnota = distinctValues.Rows[i]["PerteceANota"].ToString();
                            it.Quantidade = dtItem.Compute("SUM(QUANTIDADELIDO)", "IdConferenciaItem=" + distinctValues.Rows[i]["IdConferenciaItem"].ToString()).ToString();
                            lit.Add(it);
                        }
                    }
                }


                DataRow[] oNaoPertence = dtItem.Select("PerteceANota='NAO'");

                for (int i = 0; i < oNaoPertence.Length; i++)
                {
                    SistecnoColetor.ReposicaoInterno.It it = new SistecnoColetor.ReposicaoInterno.It();
                    it.IdItem = oNaoPertence[i]["IdConferenciaItem"].ToString();
                    it.CodigoDeBarras = oNaoPertence[i]["CodigoDeBarrasLido"].ToString();
                    it.PertenceAnota = "NAO";
                    it.Quantidade = oNaoPertence[i]["QUANTIDADELIDO"].ToString(); ;

                    lit.Add(it);

                }
                nf.Itens = lit.ToArray();

                List<SistecnoColetor.ReposicaoInterno.ConferenciaCega> cc = new List<SistecnoColetor.ReposicaoInterno.ConferenciaCega>();

                string sqlCega = "Select * from ItensConferenciaCega where IdReposicaoRoge in (Select IdReposicaoRoge from ReposicaoRoge where chave='" + txtChaveDocumento.Text + "') ";

                DataTable dtcega = Classes.BbColetor.RetornarDataTable(sqlCega);

                for (int xx = 0; xx < dtcega.Rows.Count; xx++)
                {
                    // nao grava itens que nao pertencem a nota
                    if (dtcega.Rows[xx]["IdConferenciaItem"].ToString() != "" && dtcega.Rows[xx]["IdConferenciaItem"].ToString() != "0")
                    {
                        SistecnoColetor.ReposicaoInterno.ConferenciaCega c = new SistecnoColetor.ReposicaoInterno.ConferenciaCega();
                        c.CodigoRoge = dtcega.Rows[xx]["CodigoRoge"].ToString();
                        c.IdConferenciaItem = dtcega.Rows[xx]["IdConferenciaItem"].ToString();
                        c.CodigoDeBarrasLido = dtcega.Rows[xx]["CodigoDeBarrasLido"].ToString();
                        c.Quantidade = dtcega.Rows[xx]["QuantidadeLido"].ToString();
                        c.PertenceANota = dtcega.Rows[xx]["PerteceANota"].ToString();

                        cc.Add(c);
                    }
                }


                string ret = wss.EnviarConferenciaAprovador_Cega(nf, cc.ToArray());

                if (ret.Contains("1^"))
                {
                    Classes.BbColetor.ApagarDados(nf.IdNotaFiscal);
                    lblClienteEspecial.Text = "";
                    lblQuantidadeDeItens.Text = "";
                    lblQuantidadeVolumes.Text = "";

                    btnItens.Enabled = false;
                    btnVolumes.Enabled = false;
                    btnEnviar.Enabled = false;
                    txtChaveDocumento.Text = "";
                    txtChaveDocumento.Focus();

                    throw new Exception("Conferencia Efetuada com Sucesso.");
                }
                else
                {
                    throw new Exception(ret);
                }
            }
            catch (Exception ex)
            {

                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexão de Internet.");
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;

            }
        }
    }
}