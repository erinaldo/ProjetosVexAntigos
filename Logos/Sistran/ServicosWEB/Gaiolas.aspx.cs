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
using System.Globalization;
using System.Threading;

namespace ServicosWEB
{
    public partial class Gaiolas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                // CarregarGrid(false);
                //CarregarGridEmAberto();
                CarregarPorSelecionado();

            }

            lblQuantidade.Text = GridView1.Rows.Count.ToString() + " Gaiolas";

        }

        private void CarregarGrid(bool Impresso)
        {

            if (txtCodigoBarrasBandeira.Text.Length == 0 && rbOpcoesImpressao.SelectedIndex > 2)
                return;

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "SELECT IDGAIOLA [CÓDIGO], (SELECT TOP 1  NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CODIGOREGIAO AS INT) = CAST( G.FILIAL AS INT))  FILIAL, DATA, U.LOGIN [USUÁRIO],  CASE IMPRESSO WHEN '0' THEN 'NAO' ELSE 'SIM - VIA ' + IMPRESSO END [STATUS IMPRESSAO], G.NUMEROCOLETOR [COLETOR], IMPRESSO FROM GAIOLA G ";
            sql += " INNER JOIN USUARIO U ON U.IDUSUARIO = G.IDUSUARIO ";
            if (Impresso == false)
                sql += "wHERE IMPRESSO = '0' ";
            else
            {
                sql += "wHERE IMPRESSO > '0' ";

                if (txtCodigoBarrasBandeira.Text.Length == 15)
                    sql += " and cast(filial as int) =" + int.Parse(txtCodigoBarrasBandeira.Text.Substring(0, 4)) + " and idgaiola = " + int.Parse(txtCodigoBarrasBandeira.Text.Substring(4, 10));

                else
                    sql += " and cast(filial as int) =" + int.Parse(cboFilial.SelectedValue) + " and idgaiola = " + int.Parse(txtCodigoBarrasBandeira.Text);
            }



            sql += " ORDER BY 1 DESC ";
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

        string cnx = "";


        private void carregarReport(string idGaiola)
        {
            try
            {

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin("EXEC PRC_BANDEIRA_IMPRESSO_DETALHE " + idGaiola, cnx); ;

                this.rptViewer.Width = new Unit("100%");
                this.rptViewer.Height = 600;
                this.rptViewer.ZoomMode = ZoomMode.PageWidth;
                this.rptViewer.LocalReport.EnableHyperlinks = true;
                this.rptViewer.Reset();
                this.rptViewer.LocalReport.ReportPath = Server.MapPath("Bandeira.rdlc");

                string x = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x += dt.Rows[i]["CodigoDeBarras"].ToString() + "  ";
                }


                ReportParameter par = new ReportParameter("Itens", x);

                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                ReportParameter pari;

                string sfilial = dt.Rows[0]["FILIAL"].ToString();
                string idg = dt.Rows[0]["IdGaiola"].ToString();

                int l = sfilial.Length;

                string aux = "";
                for (int i = 0; i < (4 - l); i++)
                {
                    aux += "0";
                }

                sfilial = aux + sfilial;

                aux = "";
                l = idg.Length;
                for (int i = 0; i < (10 - l); i++)
                {
                    aux += "0";
                }

                idg = aux + idg;

                int Imp = int.Parse(dt.Rows[0]["IMPRESSO"].ToString());

                if (int.Parse(dt.Rows[0]["Impresso"].ToString()) < 9)
                {
                    Imp++;
                    string sql = "UPDATE GAIOLA SET IMPRESSO=" + Imp + " WHERE IDGAIOLA=" + dt.Rows[0]["IDGAIOLA"].ToString();
                    sql += " ; INSERT INTO GaiolaHistorico VALUES (" + dt.Rows[0]["IdGaiola"].ToString() + ", NULL, GETDATE(), 'IMPRIMIU A BANDEIRA VIA " + Imp + "', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";

                    Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnx);
                }


                /*============================================*/

                //if (rbOpcoesImpressao.SelectedIndex == 1)
                //    CarregarGridEmConferencia();
                //else if (rbOpcoesImpressao.SelectedIndex == 2)
                //    CarregarGrid(false);
                //else if (rbOpcoesImpressao.SelectedIndex == 3)
                //    CarregarGrid(true);
                //else
                //    CarregarGridEmAberto();
                CarregarPorSelecionado();
                /*============================================*/

                string barCode = "*" + sfilial + idg + (Imp).ToString() + "*";
                pari = new ReportParameter("imagem", barCode);
                ReportParameter par2 = new ReportParameter("TotalItens", "TOTAL DE VOLUMES: " + dt.Rows.Count.ToString());
                this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { par, par2, pari });
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                this.rptViewer.LocalReport.DataSources.Clear();
                this.rptViewer.LocalReport.DataSources.Add(rds);
                this.rptViewer.DataBind();
                this.rptViewer.LocalReport.Refresh();
                this.rptViewer.ShowPrintButton = true;
                this.rptViewer.ShowZoomControl = true;

                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streamids;
                byte[] exportBytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = mimeType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + idGaiola + DateTime.Now.ToString() + "." + fileNameExtension);
                HttpContext.Current.Response.BinaryWrite(exportBytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandArgument == "imprimir")
            {
                try
                {
                    carregarReport(e.CommandName.ToString());
                }
                catch (Exception)
                { }

                //Response.Write("<script>window.location.href='gaiolas.aspx?rb="+rbOpcoesImpressao.SelectedIndex+"'</script>");
                //ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('gaiolas.aspx?rb=" + rbOpcoesImpressao.SelectedIndex + "', '_top');</script>");


            }
            else
            {
                if (((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() =="5298")                 
                    return;


                string sql = "DELETE FROM GAIOLACONFERENCIA WHERE IDGAIOLA=" + e.CommandName.ToString();
                sql += "DELETE FROM GAIOLA WHERE IDGAIOLA=" + e.CommandName.ToString();
                sql += "; INSERT INTO GaiolaHistorico VALUES (" + e.CommandName.ToString() + ", NULL, GETDATE(), 'EXCLUIU A BANDEIRA', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

                CarregarPorSelecionado();

            }            
        }

        private void CarregarPorSelecionado()
        {
            if (rbOpcoesImpressao.SelectedIndex == 0)
                CarregarGrid(false);
            else if (rbOpcoesImpressao.SelectedIndex == 1)
                CarregarGridEmAberto();
            else if (rbOpcoesImpressao.SelectedIndex == 2)
                CarregarGridEmConferencia();
            else
                CarregarGrid(true);

            lblQuantidade.Text = GridView1.Rows.Count.ToString() + " Gaiolas";
        }

        protected void rbOpcoesImpressao_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            CarregarPorSelecionado();
            //switch (rbOpcoesImpressao.SelectedIndex)
            //{

            //    case 0:
            //        CarregarGridEmAberto();
            //        pnlPesq.Visible = false;
            //        break;

            //    case 1:
            //        CarregarGridEmConferencia();
            //        pnlPesq.Visible = false;
            //        break;


            //    case 2:
            //        CarregarGrid(false);
            //        pnlPesq.Visible = false;
            //        break;

            //    case 3:
            //        pnlPesq.Visible = true;
            //        carregarComboRegioes();
            //        txtCodigoBarrasBandeira.Focus();
            //        break;
            //}
        }

        private void CarregarGridEmConferencia()
        {
            if (txtCodigoBarrasBandeira.Text.Length == 0 && rbOpcoesImpressao.SelectedIndex > 2)
                return;

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "SELECT IDGAIOLA [CÓDIGO], (SELECT TOP 1  NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CODIGOREGIAO AS INT) = CAST( G.FILIAL AS INT))  FILIAL, DATA, U.LOGIN [USUÁRIO],  CASE IMPRESSO WHEN '0' THEN 'NAO IMPRESSO' ELSE 'IMPRESSO - VIA ' + IMPRESSO END + ' - ' + cast((SELECT COUNT(*) FROM GAIOLACONFERENCIA GC WHERE GC.IDGAIOLA = G.IDGAIOLA) as varchar(50)) +' VOLUMES' [STATUS], G.NUMEROCOLETOR [COLETOR], IMPRESSO FROM GAIOLA G ";
            sql += " INNER JOIN USUARIO U ON U.IDUSUARIO = G.IDUSUARIO ";
            sql += "WHERE G.SITUACAO = 'EM CONFERENCIA'";
            sql += " ORDER BY 1 DESC ";
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

        private void CarregarGridEmAberto()
        {
            if (txtCodigoBarrasBandeira.Text.Length == 0 && rbOpcoesImpressao.SelectedIndex > 2)
                return;

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            string sql = "SELECT IDGAIOLA [CÓDIGO], (SELECT TOP 1  NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CODIGOREGIAO AS INT) = CAST( G.FILIAL AS INT))  FILIAL, DATA, U.LOGIN [USUÁRIO],  CASE IMPRESSO WHEN '0' THEN 'NAO IMPRESSO' ELSE 'IMPRESSO - VIA ' + IMPRESSO END + ' - ' + cast((SELECT COUNT(*) FROM GAIOLACONFERENCIA GC WHERE GC.IDGAIOLA = G.IDGAIOLA) as varchar(50)) +' VOLUMES' [STATUS], G.NUMEROCOLETOR [COLETOR], IMPRESSO FROM GAIOLA G ";
            sql += " INNER JOIN USUARIO U ON U.IDUSUARIO = G.IDUSUARIO ";
            sql += "WHERE G.SITUACAO = 'ABERTO' AND IMPRESSO<>'0'";

            sql += " ORDER BY 1 DESC ";
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

        private void carregarComboRegioes()
        {
            string sql = "SELECT DISTINCT CODIGOREGIAO, NOMEREGIAO FROM REPOSICAOROGE ";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
            cboFilial.DataSource = dt;
            cboFilial.DataTextField = "NomeRegiao";
            cboFilial.DataValueField = "CodigoRegiao";
            cboFilial.DataBind();

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrid(true);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Data.DataRowView ed = (System.Data.DataRowView)e.Row.DataItem;



            ImageButton ii = (ImageButton)e.Row.FindControl("btnexcluir");
            ImageButton ImageButton1 = (ImageButton)e.Row.FindControl("ImageButton1");


            if ((rbOpcoesImpressao.SelectedIndex != 0 && ii != null) && rbOpcoesImpressao.SelectedIndex != 1 && ii != null && rbOpcoesImpressao.SelectedIndex != 2 && ii != null)
                ii.Visible = false;
            else if (ii != null)
                ii.Attributes.Add("onClick", "javascript:return pergunta();");

            if(ImageButton1 != null)
                ImageButton1.Attributes.Add("onClick", "javascript:return pergunta2( " + ed["Impresso"].ToString().Trim() + " );");
            //ImageButton1.Attributes.Add("onClientclick", "window.location.href='gaiolas.aspx?opc=00'");

            if (((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() == "5298" && ii!=null)     
                ii.Enabled = false;

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            CarregarPorSelecionado();
        }

        protected void btnPesquisar0_Click(object sender, EventArgs e)
        {
            CarregarPorSelecionado();
        }

        protected void GridView1_Unload(object sender, EventArgs e)
        {
            //GridView x = (GridView)sender;
            //lblQuantidade.Text = x.Rows.Count.ToString() +  " Gaiolas";

            //btnPesquisar0.Text = "Atualizar (" + x.Rows.Count.ToString() + " Gaiolas)";
        }

        protected void GridView1_Load(object sender, EventArgs e)
        {
            //GridView x = (GridView)sender;
            //lblQuantidade.Text = x.Rows.Count.ToString() + " Gaiolas";
        }
    }
}