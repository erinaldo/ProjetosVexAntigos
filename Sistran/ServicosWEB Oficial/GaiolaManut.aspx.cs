using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ServicosWEB
{
    public partial class GaiolaManut : System.Web.UI.Page
    {
        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                CarregarGridPendencia();
            }
        }

        private void CarregarGridPendencia()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "SELECT * , (SELECT TOP 1 NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CodigoRegiao AS INT) =CAST(G.FILIAL AS INT) ) NOMEFILIAL FROM GAIOLA G  WHERE G.SITUACAO LIKE 'PENDENCIA%' AND DATAFECHAMENTO IS NOT NULL ORDER BY 1 ";

         //   GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
          //  GridView1.DataBind();
        }



        //private void carregarReport(string idGaiola)
        //{


        //    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin("EXEC PRC_BANDEIRA_IMPRESSO_DETALHE " + idGaiola, cnx); ;


        //    this.rptViewer.Width = new Unit("100%");
        //    this.rptViewer.Height = 600;
        //    this.rptViewer.ZoomMode = ZoomMode.PageWidth;
        //    this.rptViewer.LocalReport.EnableHyperlinks = true;
        //    this.rptViewer.Reset();
        //    this.rptViewer.LocalReport.ReportPath = Server.MapPath("Bandeira.rdlc");

        //    string x = "";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        x += dt.Rows[i]["CodigoDeBarras"].ToString() + "  ";               
        //    }


        //    ReportParameter par = new ReportParameter("Itens", x);

        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //    ReportParameter pari;

        //    string sfilial = dt.Rows[0]["FILIAL"].ToString();
        //    string idg = dt.Rows[0]["IdGaiola"].ToString();

        //    int l = sfilial.Length;

        //    string aux = "";
        //    for (int i = 0; i < (4 - l); i++)
        //    {
        //        aux += "0";
        //    }

        //    sfilial = aux + sfilial;

        //    aux = "";
        //    l = idg.Length;
        //    for (int i = 0; i < (10 - l); i++)
        //    {
        //        aux += "0";
        //    }

        //    idg = aux + idg;

        //    int Imp = int.Parse(dt.Rows[0]["IMPRESSO"].ToString());

        //    if (int.Parse(dt.Rows[0]["Impresso"].ToString()) < 9)
        //    {
        //        Imp++;
        //        string sql = "UPDATE GAIOLA SET IMPRESSO=" + Imp + " WHERE IDGAIOLA=" + dt.Rows[0]["IDGAIOLA"].ToString();
        //        Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnx);
        //    }

        //    string barCode = "*" + sfilial + idg + (Imp).ToString() + "*";
        //    pari = new ReportParameter("imagem", barCode);
        //    ReportParameter par2 = new ReportParameter("TotalItens", "TOTAL DE VOLUMES: " + dt.Rows.Count.ToString());
        //    this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { par, par2, pari });
        //    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        //    this.rptViewer.LocalReport.DataSources.Clear();
        //    this.rptViewer.LocalReport.DataSources.Add(rds);
        //    this.rptViewer.DataBind();
        //    this.rptViewer.LocalReport.Refresh();
        //    this.rptViewer.ShowPrintButton = true;
        //    this.rptViewer.ShowZoomControl = true;

        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    Warning[] warnings;
        //    string[] streamids;
        //    byte[] exportBytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = mimeType;
        //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ResultadoPesquisa " + DateTime.Now.ToString() + "." + fileNameExtension);
        //    HttpContext.Current.Response.BinaryWrite(exportBytes);
        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.End();


        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //    carregarReport(e.CommandArgument.ToString());
        }

        protected void rbOpcoesImpressao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (rbOpcoesImpressao.SelectedIndex)
            //{
            //    case 0:
            //        CarregarGrid(false);
            //        pnlPesq.Visible = false;
            //        break;

            //    case 1:
            //        GridView1.DataSource = null;
            //        GridView1.DataBind();
            //        pnlPesq.Visible = true;
            //        carregarComboRegioes();

            //        txtCodigoBarrasBandeira.Focus();
            //        break;
            //}
        }

        private void carregarComboRegioes()
        {
            //string sql = "SELECT DISTINCT CodigoRegiao, NomeRegiao FROM REPOSICAOROGE ";
            //DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
            //cboFilial.DataSource = dt;
            //cboFilial.DataTextField = "NomeRegiao";
            //cboFilial.DataValueField = "CodigoRegiao";
            //cboFilial.DataBind();            

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select * , (SELECT TOP 1 NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CodigoRegiao AS INT) =CAST(G.FILIAL AS INT) ) NOMEFILIAL from Gaiola G where DataFechamento is not null and IdGaiola=" + txtCodigoBarrasBandeira.Text.Substring(4, 10);
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count > 0)
                {
                    Panel1.Visible = true;
                    lblBandeira.Text = dt.Rows[0]["IdGaiola"].ToString();
                    lblFilial.Text = dt.Rows[0]["nomefilial"].ToString();

                }
                else
                {
                    Panel1.Visible = false;
                    lblMensagem.Text = "Bandeira não encontrada";

                }
            }
            catch (Exception EX)
            {
                lblMensagem.Text = EX.Message;
            }
        }

        protected void btnReabrir_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "UPDATE GAIOLA SET DATAFECHAMENTO=NULL, SITUACAO='ABERTO' WHERE IDGAIOLA=" + lblBandeira.Text;
                sql += "; INSERT INTO GaiolaHistorico VALUES (" + lblBandeira.Text + ", NULL, GETDATE(), 'ABRIU A BANDEIRA PARA NOVAS INCLUSOES', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                Panel1.Visible = false;
                lblMensagem.Text = "";
                lblBandeira.Text = "";
                txtCodigoBarrasBandeira.Text = "";
                txtCodigoBarrasBandeira.Focus();
                lblFilial.Text = "";
                lblMensagem.Text = "Conferencia Aberta com Sucesso.";

            }
            catch (Exception EX)
            {
                lblMensagem.Text = EX.Message;
            }
        }

        protected void btnExcluirVolume_Click(object sender, EventArgs e)
        {
            try
            {

                //string sql = "Select * from GaiolaConferencia WHERE ISNULL(ATIVO, 'SIM')='SIM' AND IDGAIOLA=" + lblBandeira.Text;
                //DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                Panel2.Visible = true;
                Panel1.Enabled = false;

                //GridView2.DataSource = dt;
                //GridView2.DataBind();
            }
            catch (Exception EX)
            {
                lblMensagem.Text = EX.Message;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel1.Enabled = true;
            Panel2.Visible = false;

            Panel1.Visible = false; lblMensagem.Text = "";
            lblBandeira.Text = "";
            txtCodigoBarrasBandeira.Text = "";
            txtCodigoBarrasBandeira.Focus();
            lblFilial.Text = "";
            lblMensagem.Text = "";
            lblBandeira.Text = "";
            txtCodigoBarrasBandeira.Text = "";
            txtCodigoBarrasBandeira.Focus();
            lblFilial.Text = "";

            CarregarGridPendencia();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "Select * from GaiolaConferencia WHERE CODIGODEBARRAS='" + txtCodigoBarrasBandeira0.Text + "' AND ISNULL(ATIVO, 'SIM')='SIM' AND IDGAIOLA=" + lblBandeira.Text;
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count > 0)
                {

                    //for (int i = 0; i < GridView2.Rows.Count; i++)
                    //{
                    //    CheckBox c = (CheckBox) GridView2.Rows[i].FindControl("chkGrid");

                    //    if (c != null)
                    //    {
                    //        if (c.Checked)
                    //        {
                    //sql = "DELETE from GAIOLACONFERENCIA  WHERE IDGAIOLACONFERENCIA=" + dt.Rows[0]["IDGAIOLACONFERENCIA"].ToString();
                    sql = "UPDATE GAIOLACONFERENCIA SET ATIVO='NAO' WHERE IDGAIOLACONFERENCIA ="+ dt.Rows[0]["IDGAIOLACONFERENCIA"].ToString();
                    sql += "; INSERT INTO GaiolaHistorico VALUES (" + lblBandeira.Text + ", '" + dt.Rows[0]["IDGAIOLACONFERENCIA"].ToString() + "', GETDATE(), 'EXCLUIU VOLUME', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";
                    sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA =" + lblBandeira.Text + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + lblBandeira.Text + " AND ISNULL(ATIVO, 'SIM')='SIM' AND SITUACAO LIKE '%PENDENCIA%')=0";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

                    lblMensagem.Text = "Volume: " + txtCodigoBarrasBandeira0.Text + " excluído com sucesso! ";
                    //        }
                    //    }
                    //}



                }
                else
                {
                    lblMensagem.Text = "VOLUME NÃO ENCONTRADO";
                    txtCodigoBarrasBandeira0.Text = "";
                    txtCodigoBarrasBandeira0.Focus();
                }

               // lblMensagem.Text = "VOLUME NÃO ENCONTRADO";
                txtCodigoBarrasBandeira0.Text = "";
                txtCodigoBarrasBandeira0.Focus();
                /*
                Panel1.Enabled = true;
                Panel2.Visible = false;

                Panel1.Visible = false; lblMensagem.Text = "";
                lblBandeira.Text = "";
                txtCodigoBarrasBandeira.Text = "";
                txtCodigoBarrasBandeira.Focus();
                lblFilial.Text = "";
                lblMensagem.Text = "";
                lblBandeira.Text = "";
                txtCodigoBarrasBandeira.Text = "";
                txtCodigoBarrasBandeira.Focus();
                lblFilial.Text = "";
                 * */

            }
            catch (Exception EX )
            {

                lblMensagem.Text = EX.Message;
            }
        }
    }
}