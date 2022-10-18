using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;


public partial class NotasFiscaisAguardEmbarqueFiltro : System.Web.UI.Page
{
    const string Situacao = "ConstDcoFilSitAguardEmbarque";
    #region Events
    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            novatb.Visible = false;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {

                btnGerarReport0.Visible = false;
                //                DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtF.Text = DateTime.Now.ToShortDateString();
                //txtI.Text = primeiroDiaMes.ToShortDateString();
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];

                if (Request.QueryString["data"] != null && Request.QueryString["idfilial"] != null)
                {
                    btnGerarReport0.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                    btnPDF0.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");
                    lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

                }
                else if (Request.QueryString["idfilial"] != null)
                {
                    btnGerarReport0.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=TELA&tit=" + Server.UrlEncode("Notas Fiscais - " + FuncoesGerais.LoadDataSetConstantes(Situacao)) + "', 'NovaJanela2', 'yes')");
                    btnPDF0.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode("Notas Fiscais - " + FuncoesGerais.LoadDataSetConstantes(Situacao)) + "', 'NovaJanela22', 'yes')");
                    lblTitulo.Text = Server.UrlDecode(Server.UrlEncode("Notas Fiscais - " + FuncoesGerais.LoadDataSetConstantes(Situacao)));
                    lblTitulo.Text = lblTitulo.Text.ToUpper();
                }
                else
                {
                    btnGerarReport0.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=TELA&tit=" + Server.UrlEncode("Notas Fiscais - " + FuncoesGerais.LoadDataSetConstantes(Situacao)) + "', 'NovaJanela2', 'yes')");
                    btnPDF0.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode("Notas Fiscais - " + FuncoesGerais.LoadDataSetConstantes(Situacao)) + "', 'NovaJanela22', 'yes')");
                    lblTitulo.Text = Server.UrlDecode(Server.UrlEncode("Notas Fiscais - " + FuncoesGerais.LoadDataSetConstantes(Situacao)));
                    lblTitulo.Text = lblTitulo.Text.ToUpper();

                    if (Request.QueryString["data"] != null)
                    {
                        txtF.Text = Server.UrlDecode(Request.QueryString["data"]).ToString();
                        txtI.Text = Server.UrlDecode(Request.QueryString["data"]).ToString();
                    }

                }

                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());


            }
            //Session["DataConf"] = txtI.Text + "|" + txtF.Text;
            CarregarGrid();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");           
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
    }

    protected void cboTipoDes0_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarGrid();
    }
    #endregion

    #region Methods

    private void CarregarGrid()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());


        TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);

        if (txtF.Text == "" || txtI.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Datas são obrigatórias.')", true);
            return;
        }


        if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
            return;
        }

        if (ts.Days > intervalo)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo + " dias.')", true);
            return;
        }

        Label lblCodEmpresa = (Label)Master.FindControl("lblCodEmpresa");
        lblCodEmpresa.Text = Session["IDEmpresa"].ToString();

        System.Text.StringBuilder ssql = new System.Text.StringBuilder();

        ssql.Append(" SELECT DISTINCT ");
        ssql.Append(" DOC.NUMERO, ");
        ssql.Append(" DOC.IDDOCUMENTO, ");
        ssql.Append(" DOC.SERIE, ISNULL(FIL.NOME, '') FILIAL ,");
        ssql.Append(" DOC.DATADEEMISSAO, ");
        ssql.Append(" DOC.DATADEENTRADA, ");
        ssql.Append(" DOC.PREVISAODESAIDA, ");
        ssql.Append(" DOC.DATADESAIDA, ");
        ssql.Append(" DOC.DATAPLANEJADA, ");
        ssql.Append(" DOC.DATADECONCLUSAO, ");
        ssql.Append(" DOC.DATADEAGENDAMENTO, ");
        ssql.Append(" DATEDIFF(DAY,  DOC.DATADEENTRADA, DOC.DATADECONCLUSAO) TRANSITTIME, ");
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
        ssql.Append(" LEFT JOIN CADASTRO CADDES WITH(NOLOCK) ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
        ssql.Append(" LEFT JOIN CIDADE CIDDES WITH(NOLOCK) ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
        ssql.Append(" LEFT JOIN ESTADO ESTDES WITH(NOLOCK) ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
        ssql.Append(" LEFT JOIN CADASTRO CADREM WITH(NOLOCK) ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
        ssql.Append(" LEFT JOIN CIDADE CIDREM WITH(NOLOCK) ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
        ssql.Append(" LEFT JOIN ESTADO ESTREM WITH(NOLOCK) ON(ESTREM.IDESTADO = CIDREM.IDESTADO) Left Join DocumentoFilial DF on (DF.IDDocumento = DOC.IDDocumento)  ");
        ssql.Append(" LEFT JOIN FILIALCIDADESETOR FCS ON FCS.IDCIDADE = CADDES.IDCIDADE " );
        //ssql.Append(" LEFT JOIN FILIAL FIL ON FIL.IDFILIAL = FCS.IDFILIAL");
        ssql.Append(" LEFT JOIN FILIAL FIL ON FIL.IDFILIAL = DOC.IDFILIALATUAL");

        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") )");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  AND DOC.SERIE<>'UNI' AND SERIE<>'DEV' ");
        ssql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
        ssql.Append(" AND NOT DOC.DATADEENTRADA IS NULL ");
        ssql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL ");
        ssql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL");
        ssql.Append(" AND DF.SITUACAO='" + FuncoesGerais.LoadDataSetConstantes(Situacao) + "' AND DOC.ATIVO='SIM'");



        if (cboTipoData.SelectedIndex > 0)
        {
            DateTime? I = null;
            DateTime? F = null;


            if (txtI.Text == "")
            {
                I = DateTime.Now.AddDays(-intervalo);
            }
            else
            {
                I = Convert.ToDateTime(txtI.Text);
            }


            if (txtF.Text == "")
            {
                F = DateTime.Now;
            }
            else
            {
                F = Convert.ToDateTime(txtF.Text);
            }

            ssql.Append(" AND " + cboTipoData.SelectedValue + " BETWEEN CONVERT(DATETIME, '" + I + "', 103) AND CONVERT(DATETIME, '" + F + "',103)");
        }

        if (cboTipoRem.SelectedIndex > 0 && txtRem.Text.Trim().Length > 0)
        {
            ssql.Append(" AND " + cboTipoRem.SelectedValue + " like '%" + txtRem.Text + "%'");
        }

        if (cboTipoDes.SelectedIndex > 0 && txtDest.Text.Trim().Length > 0)
        {
            ssql.Append(" AND " + cboTipoDes.SelectedValue + " like '%" + txtDest.Text + "%'");
        }

        //vem da tela inicial
        if (Request.QueryString["data"] != null)
                    ssql.Append(" AND DOC.DataDeEntrada BETWEEN CONVERT(DATETIME, '" + Server.UrlDecode(Request.QueryString["data"]).ToString() + "', 103) AND CONVERT(DATETIME, '" +  Server.UrlDecode(Request.QueryString["data"]).ToString() + " 23:59:59',103)");
        else if (Request.QueryString["idfilial"] != null)
        {
            DateTime inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            ssql.Append(" AND DF.IDFILIAL=" + Request.QueryString["idfilial"].ToString());
            ssql.Append(" AND DOC.DataDeEmissao BETWEEN CONVERT(DATETIME, '" + inicio.ToShortDateString() + "', 103) AND CONVERT(DATETIME, '" + fim.ToShortDateString() + "',103)");

        }

        ssql.Append(" ORDER BY 1 DESC ");

        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        Session["dt"] = dt;


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Length >= 17)
            {
                dt.Rows[i]["DESCRICAOOCORRENCIA"] = dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Substring(0, 15) + "...";
            }

            if (dt.Rows[i]["REMENOME"].ToString().Length >= 17)
            {
                dt.Rows[i]["REMENOME"] = dt.Rows[i]["REMENOME"].ToString().Substring(0, 15) + "...";
            }

            if (dt.Rows[i]["DESTNOME"].ToString().Length >= 17)
            {
                dt.Rows[i]["DESTNOME"] = dt.Rows[i]["DESTNOME"].ToString().Substring(0, 15) + "...";
            }

            if (dt.Rows[i]["Situacao"].ToString().Length >= 17)
            {
                dt.Rows[i]["Situacao"] = dt.Rows[i]["Situacao"].ToString().Substring(0, 15) + "...";
            }

        }
        RadGrid16.PageSize = Convert.ToInt16(cboTipoDes0.SelectedValue);
        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();


        if (dt.Rows.Count > 0)
        {
            btnGerarReport0.Visible = true;
            btnPDF0.Visible = true;
           // tdBot.Visible = true;
        }
        else
        {
            btnGerarReport0.Visible = false;
            btnPDF0.Visible = false;
            //tdBot.Visible = false;
        }
        //if (dt.Rows.Count > 0)
        //{
        //    btnGerarReport.Visible = true;
        //    btnPDF.Visible = true;
        //}
        //else
        //{
        //    btnGerarReport.Visible = false;
        //    btnPDF.Visible = false;
        //}
    }

    private bool ValidarData(string str)
    {
        Regex testa = new Regex(@"^(([0-2]d|[3][0-1])/([0]d|[1][0-2])/[1-2][0-9]d{2})$");
        return testa.IsMatch(str);
    }

    
    
}

    #endregion
