using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;

namespace ServicosWEB
{
    public partial class GaiolaRelatorios : System.Web.UI.Page
    {
        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                txtDataI.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00");
                txtDataF.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
        }

        DataTable dx = null;
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdRelatorios.DataSource = null;
            grdRelatorios.DataBind();
            gerar();
        }

        private void gerar()
        {
            try
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

                if (DropDownList1.SelectedIndex != 5)
                {
                    if (txtDataI.Text == "" || txtDataF.Text == "")
                    {
                        lblMensagem.Text = "Informe o período.";
                        return;
                    }
                }
                else
                {
                    if (txtPrenota.Text == "")
                    {
                        lblMensagem.Text = "Informe o numero da Pré-Nota.";
                        return;
                    }
                }

                DateTime di = new DateTime();
                DateTime df = new DateTime();


                try
                {
                    di = DateTime.Parse(txtDataI.Text);
                    df = DateTime.Parse(txtDataF.Text);

                }
                catch (Exception)
                {
                    lblMensagem.Text = "erro";
                }




                string sql = "";
                if (DropDownList1.SelectedIndex == 0) //movimentos
                {

                    GridView1.Visible = true;
                    grdRelatorios.Visible = false;
                    sql = "SELECT CONVERT(VARCHAR(10), G.DATAFECHAMENTO, 103) DATA,W.NOMEREGIAO FILIAL,COUNT(DISTINCT G.IDGAIOLA) GAIOLAS,COUNT(GC.CODIGODEBARRAS) VOLUMES FROM GAIOLA G with(nolock) ";
                    sql += " inner JOIN GAIOLACONFERENCIA GC  with(nolock) ON G.IDGAIOLA = GC.IDGAIOLA  AND GC.situacao <> 'PENDENCIA: VOLUME LIDO EM DUPLICIDADE' /*AND GC.SITUACAO='OK'*/ ";
                    sql += " inner JOIN VWREGIOESROGE W  with(nolock) ON CAST(W.CODIGOREGIAO AS INT) = CAST(G.FILIAL AS INT) ";
                    sql += " WHERE G.DATAFECHAMENTO  BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";
                    sql += " AND G.SITUACAO='FECHADO' ";
                    sql += " GROUP BY CONVERT(VARCHAR(10), G.DATAFECHAMENTO, 103), W.NOMEREGIAO ";
                    sql += " ORDER BY CONVERT(VARCHAR(10), G.DATAFECHAMENTO, 103) , W.NOMEREGIAO";

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = DBNull.Value;
                        r[1] = "TOTAL";
                        r[2] = int.Parse(dt.Compute("sum(Gaiolas)", "").ToString());
                        r[3] = int.Parse(dt.Compute("sum(Volumes)", "").ToString());
                        dt.Rows.Add(r);


                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        Session["dx"] = dt;

                        int nlinhas = GridView1.Rows.Count - 1;
                        GridView1.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        GridView1.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        GridView1.Rows[nlinhas].Font.Bold = true;
                    }
                }
                else if (DropDownList1.SelectedIndex == 1) // Divergencias
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;

                    sql = "SELECT CODIGODEBARRAS[VOLUME], U.LOGIN [USUÁRIO VOLUME],  G.IDGAIOLA [GAIOLA TENTOU ENTRAR], USA.LOGIN [USUÁRIO GAIOLA] , WV.NOMEREGIAO [FILIAL TENTOU ENTRAR],VWETIQ.NOMEREGIAO [FILIAL DA ETIQUETA], GC.SITUACAO [STATUS] ";
                    sql += " FROM GAIOLACONFERENCIA GC ";
                    sql += " INNER JOIN USUARIO U ON GC.IDUSUARIO = U.IDUSUARIO ";
                    sql += " INNER JOIN GAIOLA G ON G.IDGAIOLA = GC.IDGAIOLA ";
                    sql += " INNER JOIN USUARIO USA ON USA.IDUSUARIO = G.IDUSUARIO ";
                    sql += " INNER JOIN VWREGIOESROGE WV ON CONVERT(INT,WV.CODIGOREGIAO) =  G.FILIAL ";
                    sql += " INNER JOIN VWREGIOESROGE VWETIQ ON CONVERT(INT,VWETIQ.CODIGOREGIAO) =  CONVERT(INT, SUBSTRING(CODIGODEBARRAS, 27,2)) ";
                    sql += " WHERE ";
                    sql += " G.DATAFECHAMENTO BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";
                    sql += " AND GC.SITUACAO LIKE '%VOLUME JA LIDO NA GAIOLA:%' ";
                    sql += " AND G.SITUACAO='PENDENCIA'";
                    sql += " and GC.ATIVO = 'SIM'";

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    Session["dx"] = dt;
                    grdRelatorios.DataSource = dt;
                    grdRelatorios.DataBind();

                }
                else if (DropDownList1.SelectedIndex == 2) // Produtividade
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;

                    sql = " SELECT CONVERT(VARCHAR(20), G.IDGAIOLA) [GAIOLA], DATA [ABERTURA GAIOLA], DATAFECHAMENTO [FINALIZAÇÃO] , DATEDIFF(MI, DATA, DATAFECHAMENTO) [TEMPO EM MIN.], ";
                    sql += " (SELECT TOP 1 LOGIN FROM USUARIO U	INNER JOIN GAIOLACONFERENCIA GC ON GC.IDUSUARIO = U.IDUSUARIO AND GC.IDGAIOLA = G.IDGAIOLA) [USUÁRIO], ";
                    sql += " (SELECT COUNT(*) FROM GAIOLACONFERENCIA GC WHERE GC.IDGAIOLA = G.IDGAIOLA AND GC.SITUACAO='OK') [VOLUMES LIDOS], ";
                    sql += " (SELECT COUNT(*) FROM GAIOLACONFERENCIA GC WHERE GC.IDGAIOLA = G.IDGAIOLA AND GC.SITUACAO LIKE '%VOLUME JA LIDO NA GAIOLA:%') [VOLUMES LIDOS  COM DIVERGENCIAS] ";
                    sql += " FROM GAIOLA G ";
                    sql += " WHERE  G.SITUACAO='FECHADO' ";
                    sql += " AND G.DATAFECHAMENTO BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";
                    sql += " ORDER BY 1";

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);


                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = "TOTAL";
                        r[1] = DBNull.Value;
                        r[2] = DBNull.Value;

                        r[3] = int.Parse(dt.Compute("sum([TEMPO EM MIN.])", "").ToString());
                        r[4] = int.Parse(dt.Compute("count([USUÁRIO])", "").ToString());
                        r[5] = int.Parse(dt.Compute("sum([VOLUMES LIDOS])", "").ToString());
                        r[6] = int.Parse(dt.Compute("sum([VOLUMES LIDOS  COM DIVERGENCIAS])", "").ToString());

                        dt.Rows.Add(r);


                        grdRelatorios.DataSource = dt;
                        grdRelatorios.DataBind();
                        Session["dx"] = dt;

                        int nlinhas = grdRelatorios.Rows.Count - 1;
                        grdRelatorios.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        grdRelatorios.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        grdRelatorios.Rows[nlinhas].Font.Bold = true;
                    }


                }
                else if (DropDownList1.SelectedIndex == 3) // Produtividade Por Usuario
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;

                    sql = " SELECT U.LOGIN [USUÁRIO], COUNT(DISTINCT G.IDGAIOLA) [QTD.GAIOLAS], COUNT(GC.CODIGODEBARRAS) VOLUMES ";
                    sql += " FROM USUARIO U  ";
                    sql += " INNER JOIN GAIOLACONFERENCIA GC ON GC.IDUSUARIO = U.IDUSUARIO ";
                    sql += " INNER JOIN GAIOLA G ON G.IDGAIOLA = GC.IDGAIOLA ";
                    sql += " AND G.DATAFECHAMENTO BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";
                    sql += " AND GC.SITUACAO <> 'PENDENCIA: VOLUME LIDO EM DUPLICIDADE' AND  G.SITUACAO='FECHADO'";
                    sql += " GROUP BY U.LOGIN";

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = "TOTAL";
                        r[1] = int.Parse(dt.Compute("sum([QTD.GAIOLAS])", "").ToString());
                        r[2] = int.Parse(dt.Compute("sum(VOLUMES)", "").ToString());

                        dt.Rows.Add(r);


                        grdRelatorios.DataSource = dt;
                        grdRelatorios.DataBind();
                        Session["dx"] = dt;

                        int nlinhas = grdRelatorios.Rows.Count - 1;
                        grdRelatorios.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        grdRelatorios.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        grdRelatorios.Rows[nlinhas].Font.Bold = true;
                    }

                }
                else if (DropDownList1.SelectedIndex == 4) // DE HOHA EM HORA
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;
                    sql = " SELECT CONVERT(VARCHAR(10),DATA ,103) DATA, HORA,GAIOLAS, VOLUMES FROM  FN_GAILASPORHORA('" + di.ToString("yyyy-MM-dd HH:mm:00") + "', '" + df.ToString("yyyy-MM-dd HH:mm:59") + "')  ";
                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = "TOTAL";
                        r[1] = DBNull.Value;
                        r[2] = int.Parse(dt.Compute("sum(GAIOLAS)", "").ToString());
                        r[3] = int.Parse(dt.Compute("sum(VOLUMES)", "").ToString());
                        dt.Rows.Add(r);
                        grdRelatorios.DataSource = dt;
                        grdRelatorios.DataBind();


                        int nlinhas = grdRelatorios.Rows.Count - 1;
                        grdRelatorios.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        grdRelatorios.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        grdRelatorios.Rows[nlinhas].Font.Bold = true;


                        Session["dx"] = dt;
                    }
                }
                else if (DropDownList1.SelectedIndex == 5) // prenota
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;
                    sql = "SELECT  SUBSTRING(CODIGODEBARRAS, 14,7) [PRE NOTA] ,G.IDGAIOLA GAIOLA, VW.NOMEREGIAO [FILIAL], G.IDUSUARIO [USUARIO GAIOLA],UG.LOGIN [NOME USUARIO GAIOLA], gc.Codigodebarras[VOLUME] , GC.IDUSUARIO [USUARIO VOLUME],UGC.LOGIN [NOME USUARIO CONFERENCIA],GC.DATA [DATA LEITURA],G.DATA [DATA GAIOLA],G.SITUACAO [STATUS GAIOLA]";
                    sql += " FROM GAIOLACONFERENCIA  GC";
                    sql += " INNER JOIN GAIOLA G ON G.IDGAIOLA = GC.IDGAIOLA";
                    sql += " INNER JOIN USUARIO UG ON UG.IDUSUARIO = G.IDUSUARIO";
                    sql += " INNER JOIN USUARIO UGC ON UGC.IDUSUARIO = GC.IDUSUARIO";
                    sql += " INNER JOIN VWREGIOESROGE VW ON CONVERT(INT, VW.CODIGOREGIAO) =  CONVERT(INT, G.FILIAL) ";
                    sql += " WHERE convert(int,SUBSTRING(CODIGODEBARRAS, 14,7)) = '" + int.Parse(txtPrenota.Text) + "'";
                    sql += " AND GC.ATIVO ='SIM'";
                    sql += " AND GC.SITUACAO='OK' and GC.PERTENCEAFILIAL='SIM'";
                    sql += " And G.DATAFECHAMENTO BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd 23:59:59") + "'";

                    if (cboRegioes.SelectedIndex > 0)
                    {
                        sql += "AND  convert(int,G.FILIAL) =" + cboRegioes.SelectedValue;
                    }

                    sql += " order by 3, 2";
                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[10] = dt.Rows.Count + " Itens";
                        dt.Rows.Add(r);


                        grdRelatorios.DataSource = dt;
                        grdRelatorios.DataBind();

                        int nlinhas = grdRelatorios.Rows.Count - 1;
                        grdRelatorios.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        grdRelatorios.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        grdRelatorios.Rows[nlinhas].Font.Bold = true;
                        Session["dx"] = dt;
                    }
                }
                else if (DropDownList1.SelectedIndex == 6) //movimentos Geral
                {

                    GridView1.Visible = true;
                    grdRelatorios.Visible = false;
                    sql = "SELECT CONVERT(VARCHAR(10), G.DATAFECHAMENTO, 103) DATA,W.NOMEREGIAO FILIAL,COUNT(DISTINCT G.IDGAIOLA) GAIOLAS,COUNT(DISTINCT GC.CODIGODEBARRAS) VOLUMES FROM GAIOLA G with(nolock) ";
                    sql += " INNER JOIN GAIOLACONFERENCIA GC  WITH(NOLOCK) ON G.IDGAIOLA = GC.IDGAIOLA  /*AND GC.SITUACAO <> 'PENDENCIA: VOLUME LIDO EM DUPLICIDADE' AND GC.SITUACAO='OK'*/ ";
                    sql += " INNER JOIN VWREGIOESROGE W  WITH(NOLOCK) ON CAST(W.CODIGOREGIAO AS INT) = CAST(G.FILIAL AS INT) ";
                    sql += " WHERE G.DATAFECHAMENTO  BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";
                    // sql += " AND G.SITUACAO='FECHADO' ";
                    sql += " GROUP BY CONVERT(VARCHAR(10), G.DATAFECHAMENTO, 103), W.NOMEREGIAO ";
                    sql += " ORDER BY CONVERT(VARCHAR(10), G.DATAFECHAMENTO, 103) , W.NOMEREGIAO";

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = DBNull.Value;
                        r[1] = "TOTAL";
                        r[2] = int.Parse(dt.Compute("sum(Gaiolas)", "").ToString());
                        r[3] = int.Parse(dt.Compute("sum(Volumes)", "").ToString());
                        dt.Rows.Add(r);


                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        Session["dx"] = dt;

                        int nlinhas = GridView1.Rows.Count - 1;
                        GridView1.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        GridView1.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        GridView1.Rows[nlinhas].Font.Bold = true;
                    }
                }
                else if (DropDownList1.SelectedIndex == 7) //Consulta de Gaiola
                {

                    if(txtPrenota.Text=="")
                    return;

                    GridView1.Visible = false;
                    grdRelatorios.Visible = true ;

                    sql = "SELECT  ROW_NUMBER() OVER(ORDER BY GC.IDGAIOLA  DESC) AS '#', G.IDGAIOLA GAIOLA, G.DATA, G.SITUACAO, GC.CODIGODEBARRAS, GC.SITUACAO [STATUS DO VOLUMES] ,  W.NOMEREGIAO FILIAL";
                    sql += " FROM GAIOLA G ";
                    sql += " INNER JOIN GAIOLACONFERENCIA GC ON GC.IDGAIOLA = G.IDGAIOLA ";
                    sql += " INNER JOIN VWREGIOESROGE W  WITH(NOLOCK) ON CAST(W.CODIGOREGIAO AS INT) = CAST(G.FILIAL AS INT) ";
                    sql += " WHERE G.IDGAIOLA IN(" + txtPrenota.Text + ") and GC.PertenceAFilial='SIM' ";
                    sql += " ORDER BY 4 ";


                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.NewRow();
                        r[0] = DBNull.Value;
                        r[1] = DBNull.Value;
                        r[2] = DBNull.Value;
                        r[3] = "TOTAL: ";
                        r[4] = dt.Rows.Count.ToString() + " VOLUMES";
                        dt.Rows.Add(r);


                        grdRelatorios.DataSource = dt;
                        grdRelatorios.DataBind();
                        Session["dx"] = dt;

                        int nlinhas = grdRelatorios.Rows.Count - 1;
                        grdRelatorios.Rows[nlinhas].BackColor = System.Drawing.Color.Black;
                        grdRelatorios.Rows[nlinhas].ForeColor = System.Drawing.Color.White;
                        grdRelatorios.Rows[nlinhas].Font.Bold = true;
                    }
                }
                else if (DropDownList1.SelectedIndex == 8) //Consolidado por Giaola
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;
                    cboRegioes.Visible = true;

                    sql = "SELECT  ";
                    sql += " G.DATAFECHAMENTO DATA,G.IDGAIOLA,R.NOMEREGIAO,U.LOGIN DUPLA,";
                    sql += " (SELECT COUNT(DISTINCT CODIGODEBARRAS) FROM GAIOLACONFERENCIA WHERE IDGAIOLA=G.IDGAIOLA AND PERTENCEAFILIAL='SIM' AND  ATIVO='SIM' AND SITUACAO='OK' ) [VOLUMES LIDOS JARINU],";
                    sql += " ISNULL(G.QTDCONFIRMADASITE, 0) [VOLUMES CONFIRMADOS FILIAL], ";
                    sql += " (SELECT COUNT(DISTINCT CODIGODEBARRAS) FROM GAIOLACONFERENCIA WHERE IDGAIOLA=G.IDGAIOLA AND PERTENCEAFILIAL='SIM' AND  ATIVO='SIM' AND SITUACAO='OK') -  ISNULL(G.QTDCONFIRMADASITE,0) DIFERENCA,";
                    sql += " ISNULL(G.QTDCONFIRMADACOLETOR, 0) [QTD. COLETOR CONFIRMOU]";
                    sql += " FROM GAIOLA G WITH (NOLOCK)";
                    sql += " INNER JOIN USUARIO U   WITH (NOLOCK) ON U.IDUSUARIO = G.IDUSUARIO";
                    sql += " INNER JOIN VWREGIOESROGE R  WITH (NOLOCK) ON CAST(R.CODIGOREGIAO AS  INT) = CAST(G.FILIAL AS INT)";
                    sql += " WHERE DATAFECHAMENTO BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";
                    
                    if(cboRegioes.SelectedIndex>0)
                        sql += " and  cast(r.CodigoRegiao as  int) = "+ int.Parse(cboRegioes.SelectedItem.Text.Substring(0,2));
                    
                    sql += " GROUP BY G.IDGAIOLA, DATAFECHAMENTO , FILIAL, R.NOMEREGIAO,  G.IDUSUARIO, U.LOGIN, G.QTDCONFIRMADACOLETOR, G.QTDCONFIRMADASITE";
                    sql += " ORDER BY 1";

                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    Session["dx"] = dt;
                    grdRelatorios.DataSource = dt;
                    grdRelatorios.DataBind();

                }
                else if (DropDownList1.SelectedIndex == 9) //Consolidado por dUPLA
                {
                    GridView1.Visible = false;
                    grdRelatorios.Visible = true;
                    cboRegioes.Visible = true;

                    sql = "SELECT   ";
                    sql += " CAST(G.DATAFECHAMENTO AS DATE) DATA, ";
                    sql += " U.LOGIN DUPLA, ";
                    sql += " COUNT(DISTINCT G.IDGAIOLA) [QUANTIDA DE GAIOLAS], ";
                    sql += " (SELECT COUNT(DISTINCT CODIGODEBARRAS) FROM GAIOLACONFERENCIA WHERE IDGAIOLA=G.IDGAIOLA AND PERTENCEAFILIAL='SIM' AND  ATIVO='SIM' AND SITUACAO='OK' ) [VOLUMES LIDOS JARINU], ";
                    sql += " ISNULL(G.QTDCONFIRMADASITE,0) [QUANTIDADE CONFIRMADA FILIAL],  ";
                    sql += " (SELECT COUNT(DISTINCT CODIGODEBARRAS) FROM GAIOLACONFERENCIA WHERE IDGAIOLA=G.IDGAIOLA AND PERTENCEAFILIAL='SIM' AND  ATIVO='SIM' AND SITUACAO='OK') -  ISNULL(G.QTDCONFIRMADASITE,0) DIFERENCA ";
                    sql += " FROM GAIOLA G WITH (NOLOCK) ";
                    sql += " INNER JOIN USUARIO U   WITH (NOLOCK) ON U.IDUSUARIO = G.IDUSUARIO ";
                    sql += " WHERE G.DATAFECHAMENTO BETWEEN '" + di.ToString("yyyy-MM-dd HH:mm:00") + "' AND  '" + df.ToString("yyyy-MM-dd HH:mm:59") + "' ";


                    if (cboRegioes.SelectedIndex > 0)
                        sql += " and  cast(g.filial as  int) = " + int.Parse(cboRegioes.SelectedItem.Text.Substring(0, 2));

                    sql += " GROUP BY G.IDGAIOLA, CAST(G.DATAFECHAMENTO AS DATE) , FILIAL,  G.IDUSUARIO, U.LOGIN, G.QTDCONFIRMADASITE ";
                    sql += " ORDER BY 1 ";



                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
                    Session["dx"] = dt;
                    grdRelatorios.DataSource = dt;
                    grdRelatorios.DataBind();
                }

                Session["ini"] = txtDataI.Text;
                Session["fim"] = txtDataF.Text;
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink HyperLink1 = (HyperLink)e.Row.FindControl("HyperLink1");

            if (HyperLink1 != null)
            {

                System.Data.DataRowView o = (System.Data.DataRowView)e.Row.DataItem;

                if (DropDownList1.SelectedIndex == 6)
                {
                    HyperLink1.NavigateUrl = "#";
                }
                else
                {
                    HyperLink1.NavigateUrl = "GaiolaRelatoriosDetalhes.aspx?tipo=" + (DropDownList1.SelectedIndex == 0 ? "mov" : "div") + "&filial=" + o["Filial"].ToString() + "&di=" + o["Data"].ToString().Replace(" 00:00:00", "") + "&df=" + o["Data"].ToString().Replace(" 00:00:00", "");
                    HyperLink1.Text = o["Data"].ToString().Replace(" 00:00:00", "");
                    HyperLink1.Target = "_blank";
                }

                if (HyperLink1.Text == "")
                {
                    //ImageButton1.Visible = false;
                    e.Row.Font.Bold = true;
                }

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar0_Click(object sender, EventArgs e)
        {
            GridView1.PagerSettings.Visible = false;


            GridView1.DataSource = ((DataTable)Session["dx"]);
            GridView1.DataBind();


            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            GridView1.PagerSettings.Visible = true;

            GridView1.DataSource = ((DataTable)Session["dx"]);
            GridView1.DataBind();
        }

        protected void grdRelatorios_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            System.Data.DataRowView x = (System.Data.DataRowView)e.Row.DataItem;

            DataView dv = null;
            if (x != null)
            {
                dv = x.DataView;

                for (int i = 0; i < dv.Table.Columns.Count; i++)
                {

                    switch (dv.Table.Columns[i].DataType.Name.ToUpper().Trim())
                    {
                        case "INT32":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case "DATETIME":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                            break;
                    }
                }
            }



            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    switch (e.Row.Cells[i].Text.ToUpper().Trim())
                    {
                        case "VOLUMES LIDOS COM DIVERGENCIAS":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;
                        case "VOLUMES LIDOS":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;
                        case "TEMPO EM MIN.":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case "GAIOLA":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case "HORA":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case "GAIOLAS":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case "QTD.GAIOLAS":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;

                        case "VOLUMES":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            break;




                        case "FINALIZA&#199;&#195;O":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                            break;
                        case "ABERTURA GAIOLA":
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                            break;
                    }
                }
            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + DropDownList1.SelectedValue + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            gerar();

            if (GridView1.Visible == true)
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    GridView1.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);
                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    grdRelatorios.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grdRelatorios.HeaderRow.Cells)
                    {
                        cell.BackColor = grdRelatorios.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grdRelatorios.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grdRelatorios.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grdRelatorios.RenderControl(hw);
                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }


            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportToExcel(sender, e);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the
            /* specified ASP.NET server control at run time. */
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtDataI.Text = DateTime.Now.ToString("dd/MM/yyyy 00:00");
            //txtDataF.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            GridView1.DataSource = null;
            GridView1.DataBind();

            if (DropDownList1.SelectedIndex == 5)
            {
                //txtDataF.Enabled=false;
                //txtDataI.Enabled=false;
                txtPrenota.Enabled = true;

                txtPrenota.Text = "";
                lblTitulo.Text = "Pré-Nota:";
                txtPrenota.Focus();
                CarregarCboRegioes();
            }
            else if (DropDownList1.SelectedIndex == 7)
            {
                txtPrenota.Enabled = true;
                txtPrenota.Text = "";
                lblTitulo.Text = "Código da Gaiola:";
                txtPrenota.Focus();
                CarregarCboRegioes();
            }
            else if (DropDownList1.SelectedIndex == 8 || DropDownList1.SelectedIndex == 9)
            {
                CarregarCboRegioes();
                cboRegioes.Visible=true;
            }
            else
            {
                txtDataF.Enabled = true;
                txtDataI.Enabled = true;
                txtPrenota.Enabled = false;
                txtPrenota.Text = "";
                txtDataI.Focus();
                cboRegioes.Visible = false;
                cboRegioes.Items.Clear();
            }
        }

        private void CarregarCboRegioes()
        {
            string sql = "SELECT DISTINCT CODIGOREGIAO, NOMEREGIAO FROM REPOSICAOROGE ORDER BY 2 ";
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
            cboRegioes.DataSource = dt;
            cboRegioes.DataTextField = "NomeRegiao";
            cboRegioes.DataValueField = "CodigoRegiao";
            cboRegioes.DataBind();
            cboRegioes.Visible = true;

            cboRegioes.Items.Insert(0, new ListItem("::: SELECIONE :::", ""));


        }
    }
}