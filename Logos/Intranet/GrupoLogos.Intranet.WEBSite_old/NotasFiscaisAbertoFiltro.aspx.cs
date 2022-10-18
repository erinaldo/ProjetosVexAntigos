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

public partial class NotasFiscaisEntregueFiltro : System.Web.UI.Page
{
    #region Events
    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
                string titulo = "";

        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);
            
            if (!IsPostBack)
            {
                if (Request.QueryString["opc"] == null)
                {
                    titulo = "NOTAS FISCAIS: "  + Server.UrlDecode(Request.QueryString["situacao"].ToString());
                }



                btnGerarReport.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptEntregas.aspx?tipo=TELA&tit=" + titulo+ "', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "FullWindow('frmRptEntregas.aspx?tipo=PDF&tit=" + titulo + "', 'NovaJanela22', 'yes')");
                //DateTime primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //txtF.Text = DateTime.Now.ToShortDateString();
                //txtI.Text = primeiroDiaMes.ToShortDateString();
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];

                if (Request.QueryString["situacao"] != null)
                {
                    CarregarGrid();
                    novatb.Visible = false;
                }
            }

            lblTitulo.Text =titulo.ToUpper();
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");
        }
    }

    protected void cboTipoDes0_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarGrid();
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

    #endregion

    #region Methods

    private void CarregarGrid()
    {
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
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
            return;
        }

        Label lblCodEmpresa = (Label)Master.FindControl("lblCodEmpresa");

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
        ssql.Append(" CADDES.CNPJCPF AS DESTCNPJ, DF.SITUACAO ");
        ssql.Append(" FROM DOCUMENTO DOC WITH(NOLOCK) ");
        ssql.Append(" LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO WITH(NOLOCK) ON(DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA) ");
        ssql.Append(" LEFT JOIN OCORRENCIA OCO WITH(NOLOCK) ON(DOCOCO.IDOCORRENCIA = OCO.IDOCORRENCIA) ");
        ssql.Append(" LEFT JOIN CADASTRO CADDES WITH(NOLOCK) ON(CADDES.IDCADASTRO = DOC.IDDESTINATARIO)  ");
        ssql.Append(" LEFT JOIN CIDADE CIDDES WITH(NOLOCK) ON(CIDDES.IDCIDADE = CADDES.IDCIDADE)  ");
        ssql.Append(" LEFT JOIN ESTADO ESTDES WITH(NOLOCK) ON(ESTDES.IDESTADO = CIDDES.IDESTADO)  ");
        ssql.Append(" LEFT JOIN CADASTRO CADREM WITH(NOLOCK) ON(CADREM.IDCADASTRO = DOC.IDREMETENTE) ");
        ssql.Append(" LEFT JOIN CIDADE CIDREM WITH(NOLOCK) ON(CIDREM.IDCIDADE = CADREM.IDCIDADE)  ");
        ssql.Append(" LEFT JOIN ESTADO ESTREM WITH(NOLOCK) ON(ESTREM.IDESTADO = CIDREM.IDESTADO) Left Join DocumentoFilial DF on (DF.IDDocumento = DOC.IDDocumento)  ");
        ssql.Append(" WHERE (DOC.IDREMETENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") OR DOC.IDCLIENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + "))");
        ssql.Append(" AND DOC.TIPODEDOCUMENTO = 'NOTA FISCAL' ");
        ssql.Append(" AND DOC.DATADECONCLUSAO IS NULL ");
        ssql.Append(" AND NOT DOC.DATADEENTRADA IS NULL ");
        ssql.Append(" AND NOT DOC.CODIGODORECEXP IS NULL  AND DOC.SERIE<>'UNI'");
        ssql.Append(" AND DOC.ATIVO='SIM'");

        if (Request.QueryString["situacao"] != null)
        {
            ssql.Append(" AND DF.SITUACAO='" + Server.UrlDecode(Request.QueryString["situacao"]) + "'");

        }
        //if (cboTipoData.SelectedIndex > 0)
        //{
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

        ssql.Append(" AND " + cboTipoRem.SelectedValue + " like '%" + txtRem.Text + "%'");

        ssql.Append(" AND " + cboTipoDes.SelectedValue + " like '%" + txtDest.Text + "%'");


        ssql.Append(" ORDER BY 1 DESC ");

        RadGrid16.PageSize = Convert.ToInt16(cboTipoDes0.SelectedValue);
        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];
        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();

        Session["dt"] = dt;

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

    #endregion
}