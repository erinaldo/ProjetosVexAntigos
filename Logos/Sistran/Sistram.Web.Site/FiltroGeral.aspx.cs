using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;

public partial class FiltroGeral : System.Web.UI.Page
{
    #region Events

    public int intervalo;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");            
            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                btnGerarReport.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");

                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];

                if (Request.QueryString["gps"] != null)
                    CarregarGridPosicaoClienteGps();

                if (Request.QueryString["acao"] != null)
                    CarregarGrid();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarGrid();
    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarGrid();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarGrid();
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU NF.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

    }

    #endregion

    #region Methods

    private void CarregarGrid()
    {
        // se vem de alguma pagina de ação, ignora as validações
        if (Request.QueryString["acao"] == null)
        {
            if (txtNf.Text.Length == 0)
            {
                if (txtF.Text == "" || txtI.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Datas são obrigatórias.')", true);
                    return;
                }

                TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);


                if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
                    return;
                }

                if (ts.Days > intervalo)
                {
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
                    return;
                }
            }
        }

        System.Text.StringBuilder ssql = new System.Text.StringBuilder();

        ssql.Append(" SELECT DISTINCT CASE ISNULL(DOA.IDDOCUMENTOOCORRENCIAARQUIVO, 0) WHEN 0 THEN '' ELSE 'SIM' END 'FOTO', ");
        ssql.Append(" DOC.NUMERO, ");
        ssql.Append(" DOC.IDDOCUMENTO, ");
        ssql.Append(" DOC.SERIE, ");
        ssql.Append(" DOC.DATADEEMISSAO, ");
        ssql.Append(" DOC.DATADEENTRADA, ");
        ssql.Append(" DOC.PREVISAODESAIDA, ");
        ssql.Append(" DOC.DATADESAIDA, ");
        ssql.Append(" DOC.DATAPLANEJADA, ");
        
        ssql.Append(" DOCOCO.DataOcorrencia DATADECONCLUSAO, ");
        ssql.Append(" DOC.PRAZOUTILIZADO TRANSITTIME, ");
        ssql.Append(" OCO.NOME AS DESCRICAOOCORRENCIA, ");
        ssql.Append(" OCO.NOME AS DESCRICAOOCORRENCIA, ");
        ssql.Append(" CADREM.CNPJCPF AS REMECNPJ, ");
        ssql.Append(" COALESCE(CADREM.FANTASIAAPELIDO, CADREM.RAZAOSOCIALNOME) AS REMENOME, ");
        ssql.Append(" CADDES.CNPJCPF AS DESTCNPJ, ");
        ssql.Append(" COALESCE(CADDES.FANTASIAAPELIDO, CADDES.RAZAOSOCIALNOME) AS DESTNOME, ");
        ssql.Append(" CADDES.CNPJCPF AS DESTCNPJ, DF.Situacao ");
        ssql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK) ");
        ssql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA) ");
        ssql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA) ");

        ssql.Append(" INNER JOIN CADASTRO CADDES WITH(NOLOCK) ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
        ssql.Append(" INNER JOIN CIDADE CIDDES WITH(NOLOCK) ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
        ssql.Append(" INNER JOIN ESTADO ESTDES WITH(NOLOCK) ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
        ssql.Append(" INNER JOIN CADASTRO CADREM WITH(NOLOCK) ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
        ssql.Append(" INNER JOIN CIDADE CIDREM WITH(NOLOCK) ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
        ssql.Append(" INNER JOIN ESTADO ESTREM WITH(NOLOCK) ON(ESTREM.IDESTADO = CIDREM.IDESTADO) Left Join DocumentoFilial DF on (DF.IDDocumento = DOC.IDDocumento)  ");
        ssql.Append(" LEFT JOIN DOCUMENTOOCORRENCIAARQUIVO DOA WITH(NOLOCK)  ON DOA.IDDOCUMENTOOCORRENCIA = DOCOCO.IDDOCUMENTOOCORRENCIA ");

        if (Request.QueryString["acao"] != null && Request.QueryString["acao"] == "dt")
        {
            ssql.Append(" INNER JOIN ROMANEIODOCUMENTO ROMDOC ON (ROMDOC.IDDOCUMENTO=DOC.IDDOCUMENTO)   ");
            ssql.Append(" INNER  JOIN DTROMANEIO DTROM ON (ROMDOC.IDROMANEIO=DTROM.IDROMANEIO)     AND DTROM.IDDT= " + Request.QueryString["dt"]);
        }

        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") )");
        ssql.Append(" AND DOC.SERIE<>'UNI' AND SERIE<>'DEV' ");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL',  'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA', 'LOGISTICA') ");


        if (txtNf.Text.Trim().Length > 0)
            ssql.Append(" AND DOC.NUMERO=" + txtNf.Text);

        if (txtNf.Text.Length == 0)
        {
            DateTime? I = null;
            DateTime? F = null;

            if (txtI.Text == "")
                            I = DateTime.Now.AddDays(-intervalo);            
            else
                            I = Convert.ToDateTime(txtI.Text);
            


            if (txtF.Text == "")
                       F = DateTime.Now;
                        else
                            F = Convert.ToDateTime(txtF.Text);

            if (Request.QueryString["acao"] == null || Request.QueryString["acao"] != "dt")
            {
                ssql.Append(" AND " + cboTipoData.SelectedValue + " BETWEEN CONVERT(DATETIME, '" + I + "', 103) AND CONVERT(DATETIME, '" + F + "',103)");
            }
        }

        if(txtRem.Text !="")
            ssql.Append(" AND " + cboTipoRem.SelectedValue + " like '%" + txtRem.Text + "%'");

        if (txtDest.Text != "")
            ssql.Append(" AND " + cboTipoDes.SelectedValue + " like '%" + txtDest.Text + "%'");

        ssql.Append(" ORDER BY 1 DESC ");

        RadGrid16.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue);
        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        Session["dt"] = dt;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Length >= 40)
                            dt.Rows[i]["DESCRICAOOCORRENCIA"] = dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Substring(0, 40) + "...";
            

            if (dt.Rows[i]["REMENOME"].ToString().Length >= 25)
                dt.Rows[i]["REMENOME"] = dt.Rows[i]["REMENOME"].ToString().Substring(0, 25) + "...";

            if (dt.Rows[i]["DESTNOME"].ToString().Length >= 25)
                dt.Rows[i]["DESTNOME"] = dt.Rows[i]["DESTNOME"].ToString().Substring(0, 25) + "...";
            
        }

        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnGerarReport.Visible = true;
            btnPDF.Visible = true;
        }
        else
        {
            btnGerarReport.Visible = false;
            btnPDF.Visible = false;
        }
    }

    private void CarregarGridPosicaoClienteGps()
    {


        System.Text.StringBuilder ssql = new System.Text.StringBuilder();

        ssql.Append(" SELECT DISTINCT CASE ISNULL(DOA.IDDOCUMENTOOCORRENCIAARQUIVO, 0) WHEN 0 THEN '' ELSE 'SIM' END 'FOTO', ");
        ssql.Append(" DOC.NUMERO, ");
        ssql.Append(" DOC.IDDOCUMENTO, ");
        ssql.Append(" DOC.SERIE, ");
        ssql.Append(" DOC.DATADEEMISSAO, ");
        ssql.Append(" DOC.DATADEENTRADA, ");
        ssql.Append(" DOC.PREVISAODESAIDA, ");
        ssql.Append(" DOC.DATADESAIDA, ");
        ssql.Append(" DOC.DATAPLANEJADA, ");
        ssql.Append(" DOC.DATADECONCLUSAO, ");
        ssql.Append(" DOCOCO.Latitude, ");
        ssql.Append(" DOCOCO.Longitude, ");

        //ssql.Append(" DATEDIFF(DAY,  DOC.DATADEENTRADA, DOC.DATADECONCLUSAO) TRANSITTIME, ");
        ssql.Append(" DOC.PRAZOUTILIZADO TRANSITTIME, ");
        ssql.Append(" OCO.NOME AS DESCRICAOOCORRENCIA, ");
        ssql.Append(" OCO.NOME AS DESCRICAOOCORRENCIA, ");
        ssql.Append(" CADREM.CNPJCPF AS REMECNPJ, ");
        ssql.Append(" COALESCE(CADREM.FANTASIAAPELIDO, CADREM.RAZAOSOCIALNOME) AS REMENOME, ");
        ssql.Append(" CADDES.CNPJCPF AS DESTCNPJ, ");
        ssql.Append(" COALESCE(CADDES.FANTASIAAPELIDO, CADDES.RAZAOSOCIALNOME) AS DESTNOME, ");
        ssql.Append(" CADDES.CNPJCPF AS DESTCNPJ, DF.Situacao ");
        ssql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK) ");
        ssql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA) ");
        ssql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA) ");

        ssql.Append(" INNER JOIN CADASTRO CADDES WITH(NOLOCK) ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
        ssql.Append(" INNER JOIN CIDADE CIDDES WITH(NOLOCK) ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
        ssql.Append(" INNER JOIN ESTADO ESTDES WITH(NOLOCK) ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
        ssql.Append(" INNER JOIN CADASTRO CADREM WITH(NOLOCK) ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
        ssql.Append(" INNER JOIN CIDADE CIDREM WITH(NOLOCK) ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
        ssql.Append(" INNER JOIN ESTADO ESTREM WITH(NOLOCK) ON(ESTREM.IDESTADO = CIDREM.IDESTADO) Left Join DocumentoFilial DF on (DF.IDDocumento = DOC.IDDocumento)  ");
        ssql.Append(" LEFT JOIN DOCUMENTOOCORRENCIAARQUIVO DOA ON DOA.IDDOCUMENTOOCORRENCIA = DOCOCO.IDDOCUMENTOOCORRENCIA ");

        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") )");
        ssql.Append(" AND DOC.SERIE<>'UNI' AND SERIE<>'DEV' ");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA', 'LOGISTICA')  ");


        if (txtNf.Text.Trim().Length > 0)
            ssql.Append(" AND DOC.NUMERO=" + txtNf.Text);

        if (Request.QueryString["iddoc"] == null && Request.QueryString["ic"] != null)
        {
            ssql.Append(" AND DOC.IDDOCUMENTO IN(SELECT DISTINCT D.IDDOCUMENTO FROM DOCUMENTO D  ");
            ssql.Append(" INNER JOIN ROMANEIODOCUMENTO ROMD ON ROMD.IDDOCUMENTO = D.IDDOCUMENTO ");
            ssql.Append(" INNER JOIN DTROMANEIO DTR ON DTR.IDROMANEIO = ROMD.IDROMANEIO ");
            ssql.Append(" WHERE IDDT =  " + Request.QueryString["iddt"]);
            ssql.Append(" AND IDCLIENTE =" + Request.QueryString["ic"] + ")");
        }

        if (Request.QueryString["iddoc"] != null)
        {
            ssql.Append(" AND DOC.IDDOCUMENTO IN(" + Request.QueryString["iddoc"] + ")");
        }
        //if (txtNf.Text.Length == 0)
        //{
        //    DateTime? I = null;
        //    DateTime? F = null;

        //    if (txtI.Text == "")
        //    {
        //        I = DateTime.Now.AddDays(-intervalo);
        //    }
        //    else
        //    {
        //        I = Convert.ToDateTime(txtI.Text);
        //    }


        //    if (txtF.Text == "")
        //    {
        //        F = DateTime.Now;
        //    }
        //    else
        //    {
        //        F = Convert.ToDateTime(txtF.Text);
        //    }

        //    ssql.Append(" AND " + cboTipoData.SelectedValue + " BETWEEN CONVERT(DATETIME, '" + I + "', 103) AND CONVERT(DATETIME, '" + F + "',103)");
        //}

        //ssql.Append(" AND " + cboTipoRem.SelectedValue + " like '%" + txtRem.Text + "%'");
        //ssql.Append(" AND " + cboTipoDes.SelectedValue + " like '%" + txtDest.Text + "%'");

        ssql.Append(" ORDER BY 1 DESC ");

        RadGrid16.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue);
        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        Session["dt"] = dt;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Length >= 30)
            {
                dt.Rows[i]["DESCRICAOOCORRENCIA"] = dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Substring(0, 30) + "...";
            }

            if (dt.Rows[i]["REMENOME"].ToString().Length >= 17)
            {
                dt.Rows[i]["REMENOME"] = dt.Rows[i]["REMENOME"].ToString().Substring(0, 15) + "...";
            }

            if (dt.Rows[i]["DESTNOME"].ToString().Length >= 17)
            {
                dt.Rows[i]["DESTNOME"] = dt.Rows[i]["DESTNOME"].ToString().Substring(0, 15) + "...";
            }

            //if (dt.Rows[i]["Situacao"].ToString().Length >= 17)
            //{
            //    dt.Rows[i]["Situacao"] = dt.Rows[i]["Situacao"].ToString().Substring(0, 15) + "...";
            //}
        }

        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnGerarReport.Visible = true;
            btnPDF.Visible = true;
        }
        else
        {
            btnGerarReport.Visible = false;
            btnPDF.Visible = false;
        }
    }

    private bool ValidarData(string str)
    {
        Regex testa = new Regex(@"^(([0-2]d|[3][0-1])/([0]d|[1][0-2])/[1-2][0-9]d{2})$");
        return testa.IsMatch(str);
    }

    protected void carregarComboOcorrencia()
    {
        //cboOcorrencia.DataSource = SistranBLL.NotasFiscais.Ocorrencia.OcorrenciaListar(Session["Conn"].ToString());
        //cboOcorrencia.DataValueField = "IDOCORRENCIA";
        //cboOcorrencia.DataTextField = "Nome";
        //cboOcorrencia.DataBind();
        //cboOcorrencia.Items.Insert(0, new ListItem(":: Todos ::", "0"));

    }

    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (Request.QueryString["gps"] != null)
        {
            ImageButton imgGoogle = (ImageButton)e.Item.FindControl("imgGoogle");

            if (imgGoogle != null)
            {
                DataRowView x = (DataRowView)e.Item.DataItem;

                if (x["Longitude"].ToString() != "")
                {
                    imgGoogle.Visible = true;
                    imgGoogle.Attributes.Add("onClick", "javascript:window.open('frmLocalizaoNota.aspx?opc=Localização Notas Fiscal&iddoc=" + x["iddocumento"] + "'); return false;");
                }
            }
        }
    }

    protected void cboTipoDes0_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarGrid();
    }

    protected void txtNf_TextChanged(object sender, EventArgs e)
    {
        if (txtNf.Text.Length > 0)
        {
            txtI.Text = "";
            txtF.Text = "";
            CarregarGrid();
        }
        else
        {
            txtI.Text = DateTime.Now.AddDays(-intervalo).ToShortDateString();
            txtF.Text = DateTime.Now.ToShortDateString();
        }
    }

    #endregion

    protected void Button2_Click(object sender, EventArgs e)
    {
        RadGrid16.ExportSettings.ExportOnlyData = true;
        RadGrid16.ExportSettings.IgnorePaging = true;
        RadGrid16.ExportSettings.OpenInNewWindow = true;
        RadGrid16.MasterTableView.ExportToExcel();
    }

    int numero = 0;
    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        numero = 0;
        if (e.CommandArgument.ToString() == "VerFoto")
        {
            dvFoto.Visible = true;
            DataTable dtfotos = buscarFoto(e.CommandName.ToString());
            Session["dtfoto"] = dtfotos;
            lstFoto.DataSource = dtfotos;
            lstFoto.DataBind();

            for (int i = 0; i < dtfotos.Rows.Count; i++)
            {
                byte[] imagem = (byte[])dtfotos.Rows[i]["ARQUIVO"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + dtfotos.Rows[i][0].ToString() + ".jpg"));
                imgFotoGrande.ImageUrl = "imgReport/" + dtfotos.Rows[i]["IDDOCUMENTOOCORRENCIAARQUIVO"] + ".jpg";
            }
            pnlteste.Enabled = false;


        }
    }

    private DataTable buscarFoto(string iddocumento)
    {
        string strsq = "";
        strsq += " SELECT DOA.IDDOCUMENTOOCORRENCIAARQUIVO, ";
        strsq += " DOA.ARQUIVO,  ";
        strsq += " CONVERT(VARCHAR(10),DO.DATAOCORRENCIA, 103) DATAOCORRENCIA ";
        strsq += " FROM DOCUMENTOOCORRENCIAARQUIVO DOA ";
        strsq += " INNER JOIN DOCUMENTOOCORRENCIA DO ON DO.IDDOCUMENTOOCORRENCIA =  DOA.IDDOCUMENTOOCORRENCIA ";
        strsq += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTOOCORRENCIA  =  DOA.IDDOCUMENTOOCORRENCIA";
        strsq += " WHERE DOC.IDDOCUMENTO = " + iddocumento;
        strsq += " ORDER BY DO.DATAOCORRENCIA DESC ";
        return new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), strsq).Tables[0];
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {


    }
    protected void btnFecharImagem_Click(object sender, EventArgs e)
    {
        dvFoto.Visible = false;
        pnlteste.Enabled = true;
    }

    protected void lstFoto_ItemCommand(object source, DataListCommandEventArgs e)
    {

    }

    protected void lstFoto_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        ImageButton imgFotos = (ImageButton)e.Item.FindControl("btnAmpliarImagem");
        Label lblData = (Label)e.Item.FindControl("lblData");

        if (imgFotos != null)
        {
            imgFotos.ImageUrl = "imgReport/" + imgFotos.CommandArgument.ToString() + ".jpg";
            numero = numero + 1;
            lblData.Text = "Foto: " + numero.ToString();
        }
    }

    protected void lstFoto_ItemCommand1(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Ampliar")
        {
            imgFotoGrande.ImageUrl = "imgReport/" + e.CommandArgument.ToString() + ".jpg";

        }
    }
}