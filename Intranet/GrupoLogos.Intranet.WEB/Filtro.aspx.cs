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

public partial class Filtro : System.Web.UI.Page
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
            intervalo = Convert.ToInt32( ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                btnGerarReport.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptEntregas.aspx?tipo=TELA&tit=" +  Server.UrlEncode(Request.QueryString["opc"].ToString()) +"', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "FullWindow('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");               

                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Replace("'", "´" ) +"')", true);
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
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU NF.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

    }

    #endregion

    #region Methods

    private void CarregarGrid()
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
        

        System.Text.StringBuilder ssql = new System.Text.StringBuilder();

        ssql.Append(" SELECT DISTINCT ");
        ssql.Append(" DOC.NUMERO, ");
        ssql.Append(" DOC.IDDOCUMENTO, ");
        ssql.Append(" DOC.SERIE, ");
        ssql.Append(" DOC.DATADEEMISSAO, ");
        ssql.Append(" DOC.DATADEENTRADA, ");
        ssql.Append(" DOC.PREVISAODESAIDA, ");
        ssql.Append(" DOC.DATADESAIDA, ");
        ssql.Append(" DOC.DATAPLANEJADA, ");
        ssql.Append(" DOC.DATADECONCLUSAO, ");
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
        
        ssql.Append(" INNER JOIN CADASTRO CADDES WITH(NOLOCK) ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
        ssql.Append(" INNER JOIN CIDADE CIDDES WITH(NOLOCK) ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
        ssql.Append(" INNER JOIN ESTADO ESTDES WITH(NOLOCK) ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
        ssql.Append(" INNER JOIN CADASTRO CADREM WITH(NOLOCK) ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
        ssql.Append(" INNER JOIN CIDADE CIDREM WITH(NOLOCK) ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
        ssql.Append(" INNER JOIN ESTADO ESTREM WITH(NOLOCK) ON(ESTREM.IDESTADO = CIDREM.IDESTADO) Left Join DocumentoFilial DF on (DF.IDDocumento = DOC.IDDocumento)  ");
        
        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDDESTINATARIO IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + "))");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' AND DOC.SERIE<>'UNI'");
        
        if (txtNf.Text.Trim().Length > 0)
            ssql.Append(" AND DOC.NUMERO=" + txtNf.Text);

        if (txtNf.Text.Length == 0)
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

        ssql.Append(" AND " + cboTipoRem.SelectedValue + " like '%" + txtRem.Text + "%'");
        ssql.Append(" AND " + cboTipoDes.SelectedValue + " like '%" + txtDest.Text + "%'");

        ssql.Append(" ORDER BY 1 DESC ");

        RadGrid16.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue);
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

}