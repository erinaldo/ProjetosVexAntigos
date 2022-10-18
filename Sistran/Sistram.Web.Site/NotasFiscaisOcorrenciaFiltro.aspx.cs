using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.Globalization;
using System.Threading;


public partial class NotasFiscaisOcorrenciaFiltro : System.Web.UI.Page
{
    //const string Situacao = "ConstDcoFilSitAguardEmbarque";
    #region Events
    public int intervalo;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            


            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                btnGerarReport.Visible = false;      
                carregarComboOcorrencia();


                if (Request.QueryString["idOcorrencia"] == null)
                {
                    btnGerarReport.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                    btnPDF.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");
                    lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
                    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                    SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

   
                }
                else
                {
                    btnGerarReport.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=TELA&tit=" + Server.UrlEncode("Notas Fiscais com ocorrências") + "', 'NovaJanela2', 'yes')");
                    btnPDF.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode("Notas Fiscais com ocorrências") + "', 'NovaJanela22', 'yes')");
                    lblTitulo.Text = Server.UrlDecode(Server.UrlEncode("Notas Fiscais com ocorrências"));
                    lblTitulo.Text = lblTitulo.Text.ToUpper();

                    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                    SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

   
                    cboOcorrencia.SelectedValue = Server.UrlEncode(Request.QueryString["idOcorrencia"]);
                    CarregarGrid();
                }
            }           
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
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

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

        string sclientes = Sistran.Library.FuncoesUteis.retornarClientes();        

        System.Text.StringBuilder ssql = new System.Text.StringBuilder();
        ssql.Append(" SELECT DISTINCT ");
        ssql.Append(" DOC.NUMERO, ");
        ssql.Append(" DOC.IDDOCUMENTO, ");
        ssql.Append(" DOC.SERIE,  ISNULL(FIL.NOME, '') FILIAL , ");
        ssql.Append(" DOC.DATADEEMISSAO, ");
        ssql.Append(" DOC.DATADEENTRADA, ");
        ssql.Append(" DOC.PREVISAODESAIDA, ");
        ssql.Append(" DOC.DATADESAIDA, ");
        ssql.Append(" DOC.DATAPLANEJADA, ");
        ssql.Append(" DOC.DATADECONCLUSAO, ");
        ssql.Append(" DATEDIFF(DAY,  DOC.DATADEENTRADA, DOC.DATADECONCLUSAO) TRANSITTIME, ");
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
        ssql.Append(" LEFT JOIN FILIALCIDADESETOR FCS ON FCS.IDCIDADE = CADDES.IDCIDADE ");
        ssql.Append(" LEFT JOIN FILIAL FIL ON FIL.IDFILIAL = FCS.IDFILIAL");
        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") )");
        //ssql.Append(" AND DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' ");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO IN('NOTA FISCAL', 'GUIA DE REMESSA')  AND TIPODESERVICO  IN('TRANSPORTE', 'COLETA')  AND DOC.SERIE<>'UNI' AND SERIE<>'DEV' ");

        ssql.Append(" AND NOT DOC.DATADEENTRADA IS NULL ");
        ssql.Append(" AND DOC.CODIGODORECEXP IS NOT NULL ");
        //ssql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
        ssql.Append(" AND DOC.IDDOCUMENTOOCORRENCIA IS NOT NULL ");
        ssql.Append(" AND OCO.NOME IS NOT NULL AND OCO.CODIGO <>'01' ");


        if(cboOcorrencia.SelectedIndex>0)
              ssql.Append(" AND OCO.IDOCORRENCIA = '" + cboOcorrencia.SelectedValue.Trim() + "'");

        string[] DataConf = FuncoesGerais.DataConf();
        //txtI.Text = DataConf[0];
        //txtF.Text = DataConf[1];

        if (Request.QueryString["tipod"] == "emissao")
        {
            ssql.Append("  AND DATADECONCLUSAO IS NULL AND DOC.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataConf[0] + "', 103) AND CONVERT(DATETIME, '" + DataConf[1] + "', 103) ");
        }
        else
        {
            ssql.Append(" AND DOCOCO.DATAOCORRENCIA BETWEEN CONVERT(DATETIME, '" + DataConf[0] + "', 103) AND CONVERT(DATETIME, '" + DataConf[1] + "', 103) ");
        }


        ssql.Append(" AND " + cboTipoRem.SelectedValue + " like '%" + txtRem.Text + "%'");
        ssql.Append(" AND " + cboTipoDes.SelectedValue + " like '%" + txtDest.Text + "%'");

        //vem da tela inicial
        if (Request.QueryString["data"] != null)
        {
            ssql.Append("AND OCO.IDOCORRENCIA = '" + Server.UrlDecode(Request.QueryString["idOcorrencia"].ToString()) + "'");
        }


        ssql.Append(" ORDER BY 1 DESC ");

        RadGrid16.PageSize = Convert.ToInt16(cboTipoDes0.SelectedValue);
        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];
        Session["dt"] = dt;


        for (int i = 0; i < dt.Rows.Count ; i++)
        {
            if (dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Length >= 20)
            {
                dt.Rows[i]["DESCRICAOOCORRENCIA"] = dt.Rows[i]["DESCRICAOOCORRENCIA"].ToString().Substring(0, 20) + "...";
            }

            if (dt.Rows[i]["REMENOME"].ToString().Length >= 20)
            {
                dt.Rows[i]["REMENOME"] = dt.Rows[i]["REMENOME"].ToString().Substring(0, 20) + "...";
            }

            if (dt.Rows[i]["DESTNOME"].ToString().Length >= 20)
            {
                dt.Rows[i]["DESTNOME"] = dt.Rows[i]["DESTNOME"].ToString().Substring(0, 20) + "...";
            }

            if (dt.Rows[i]["Situacao"].ToString().Length >= 21)
            {
                dt.Rows[i]["Situacao"] = dt.Rows[i]["Situacao"].ToString().Substring(0, 20) + "...";
            }
            
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

    string idClientes = "";
    protected void carregarComboOcorrencia()
    {
        Label lblCodEmpresa = (Label)Master.FindControl("lblCodEmpresa");
        idClientes = Sistran.Library.FuncoesUteis.retornarClientes();
        cboOcorrencia.DataSource = NotasFiscais.Ocorrencia.OcorrenciaListar(Session["Conn"].ToString(), idClientes);
        cboOcorrencia.DataValueField = "Codigo";
        cboOcorrencia.DataTextField = "Nome";
        cboOcorrencia.DataBind();

        if (cboOcorrencia.Items.Count == 0)
        {
            cboOcorrencia.Items.Clear();
            cboOcorrencia.Items.Insert(0, new ListItem("Nenhuma Ocorrência Encontrada.", "0"));
            Button1.Visible = false;
        }
        else
            cboOcorrencia.Items.Insert(0, new ListItem(":: Mostrar Todas ::", "0"));

    }
    
    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {       
    }   

    #endregion

   
}