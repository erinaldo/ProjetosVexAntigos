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
using System.Web.UI.HtmlControls;


public partial class NotasFiscaisAguardEmbarqueFiltro : System.Web.UI.Page
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
            HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
            tr0.Style.Add("display", "none");

            if (!IsPostBack)
            {
                
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            }

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

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    CarregarGrid();
    //}

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


        Label lblCodEmpresa = (Label)Master.FindControl("lblCodEmpresa");
        lblCodEmpresa.Text = Session["IDEmpresa"].ToString();

        System.Text.StringBuilder ssql = new System.Text.StringBuilder();

        ssql.Append(" SELECT DISTINCT ");
        ssql.Append(" DOC.NUMERO, ");
        ssql.Append(" isnull(DOC.DOCUMENTOESPECIAL, 'NAO') ESPECIAL, ");
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
        ssql.Append(" LEFT JOIN FILIAL FIL ON FIL.IDFILIAL = DOC.IDFILIAL");
        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") )");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO IN('TRANSPORTE', 'COLETA')  AND DOC.SERIE<>'UNI' ");

        ssql.Append(" AND NOT DOC.DATADEENTRADA IS NULL AND DOC.ATIVO='SIM'");

        bool OutrasSeries = bool.Parse(Request.QueryString["os"]);
        bool Dev = bool.Parse(Request.QueryString["d"]);
        bool Ret = bool.Parse(Request.QueryString["r"]);


        if (OutrasSeries && Dev && Ret)
        {
            ssql.Append("");
        }
        else
        {
            if (OutrasSeries && Dev == false && Ret == false)
            {
                ssql.Append(" AND DOC.SERIE NOT IN('RET', 'DEV') ");
            }
            else
            {
                if (OutrasSeries && (Dev == false || Ret == false))
                {
                    if (Dev)
                        ssql.Append(" AND DOC.SERIE <> 'RET' ");

                    if (Ret)
                        ssql.Append(" AND DOC.SERIE <> 'DEV' ");

                }
                else
                {
                    string sel = "";
                    if (Dev && Ret)
                        sel += "'DEV', 'RET'";
                    else
                    {
                        if (Dev)
                            sel += "'DEV'";

                        if (Ret)
                            sel += "'RET'";
                    }

                    ssql.Append(" AND DOC.SERIE  IN(" + sel + ") ");
                }
            }
        }
        

        if (Request.QueryString["tipo"] == "agemb") // aguardando embarque
        {
            ssql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
            ssql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL ");
            ssql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL");
            ssql.Append(" AND DF.SITUACAO='AGUARDANDO EMBARQUE' ");
            lblTitulo.Text = "NOTAS FISCAIS AGUARDANDO EMBRAQUE";
        }
        else if (Request.QueryString["tipo"] == "devol") // aguradando devolucao
        {
            ssql.Append(" AND DF.SITUACAO='AGUARDANDO DEVOLUCAO' ");
            lblTitulo.Text = "NOTAS FISCAIS AGUARDANDO DEVOLUÇÃO";
            ssql.Append(" AND DOC.DATADECONCLUSAO IS not NULL ");

        }
        else if (Request.QueryString["tipo"] == "EM DEVOLUCAO" || Request.QueryString["tipo"] == "EM ENTREGA" || Request.QueryString["tipo"] == "AGUARDANDO EMBARQUE" || Request.QueryString["tipo"] == "AGUARDANDO DEVOLUCAO" || Request.QueryString["tipo"] == "MERCADORIA EMBARCADA")
        {
            ssql.Append(" AND DF.SITUACAO='" + Request.QueryString["tipo"].ToString() + "' ");
            lblTitulo.Text = "NOTAS FISCAIS " + Request.QueryString["tipo"];

            if (!Request.QueryString["tipo"].ToString().Contains("DEVOLUCAO"))
            {
                ssql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NULL ");
                ssql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
                ssql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL ");
            }
        }
        else
        {
            ssql.Append(" AND OCO.IDOCORRENCIA=" + Request.QueryString["tipo"].ToString() );
            ssql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT  NULL ");
            ssql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL ");
            ssql.Append(" AND DATADECONCLUSAO IS NULL");
            lblTitulo.Text = "NOTAS FISCAIS POR OCORRENCIA";
        }
                

        //vem da tela inicial
        if (Request.QueryString["data"] != "")
        {
            ssql.Append(" AND DOC.DataDeEntrada BETWEEN CONVERT(DATETIME, '" + Request.QueryString["data"] + "', 103) AND CONVERT(DATETIME, '" + Request.QueryString["data"] + " 23:59:59',103) ");
        }

        if (Request.QueryString["idfilial"] != "")
        {
            ssql.Append(" AND IDFILIALATUAL=" + Request.QueryString["idfilial"].ToString());
        }


        if (Request.QueryString["esp"] != null && Request.QueryString["esp"] == "SIM")
            ssql.Append(" AND DOC.DOCUMENTOESPECIAL='SIM'");

        ssql.Append(" ORDER BY 1 DESC ");

        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        Session["dt"] = dt;
        RadGrid16.PageSize = 20;
        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();

        btnExport.Attributes.Add("onClick", "window.open('kpi/gerarexcelInic.aspx', 'NovaJanela2', 'yes'); return false;");

        
    }
    protected void RadGrid16_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem || e.Item.ItemType== Telerik.Web.UI.GridItemType.Item)
        {
            DataRowView dataItem = (DataRowView)e.Item.DataItem;

            if (dataItem["ESPECIAL"].ToString() == "SIM")
            {
                e.Item.ForeColor = System.Drawing.Color.Red;
                e.Item.Font.Bold = true;
            }
        }
    }
    
}
    #endregion
