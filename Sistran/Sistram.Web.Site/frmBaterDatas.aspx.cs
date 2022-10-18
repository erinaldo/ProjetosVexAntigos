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

public partial class frmBaterDatas : System.Web.UI.Page
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
               
            }            
            
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
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU NF.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

    }

    #endregion

    #region Methods

    private void CarregarGrid()
    {

        System.Text.StringBuilder ssql = new System.Text.StringBuilder();

        ssql.Append("  SELECT ");
        ssql.Append("   NF.IDDOCUMENTO, ");
        ssql.Append("  FL.NOME FILIAL, ");
        ssql.Append("  ISNULL(NF.PRAZOUTILIZADO,0) PRAZOUTILIZADO_EDVALDO, NF.DATADEENTRADA,");
        ssql.Append("  NF.DATADEEMISSAO,");
        ssql.Append("  NF.DATADECONCLUSAO,");
        ssql.Append("  NF.NUMERO,");

        ssql.Append("  COALESCE(CID.IDCIDADE,CIDDEST.IDCIDADE) IDCIDADE,");
        ssql.Append("  COALESCE(EST.IDESTADO, ESTDEST.IDESTADO) IDESTADO, ");
        ssql.Append("  CID.NOME, EST.UF");
        ssql.Append("  FROM DOCUMENTO NF     ");
        ssql.Append("  INNER JOIN CADASTRO REME ON (REME.IDCADASTRO = NF.IDREMETENTE)     ");
        ssql.Append("  INNER JOIN CADASTRO DEST ON (DEST.IDCADASTRO = NF.IDDESTINATARIO)     ");
        ssql.Append("  INNER JOIN CIDADE DESTCID ON (DESTCID.IDCIDADE = DEST.IDCIDADE)     ");
        ssql.Append("  INNER JOIN ESTADO DESTEST ON (DESTEST.IDESTADO = DESTCID.IDESTADO)     ");
        ssql.Append("  INNER JOIN FILIAL FL ON (FL.IDFILIAL = NF.IDFILIAL)     ");
        ssql.Append("   LEFT JOIN CIDADE CID ON (CID.IDCIDADE = NF.IDENDERECOCIDADE) ");
        ssql.Append("  LEFT JOIN ESTADO EST ON (EST.IDESTADO = CID.IDESTADO)   ");
        ssql.Append("  LEFT JOIN CIDADE CIDDEST ON (CIDDEST.IDCIDADE = DEST.IDCIDADE)  ");
        ssql.Append("  LEFT JOIN ESTADO ESTDEST ON (ESTDEST.IDESTADO = CIDDEST.IDESTADO)  ");


        ssql.Append("  WHERE   NF.TIPODEDOCUMENTO = 'NOTA FISCAL'    ");
        ssql.Append("  AND NF.ATIVO='SIM'    ");
        ssql.Append("  AND NF.DATADEEMISSAO BETWEEN  CONVERT(DATETIME, '01/05/2011 00:00:00', 103) ");
        ssql.Append("  AND CONVERT(DATETIME, '30/05/2011 00:00:00', 103)   ");
        ssql.Append("  AND (NF.IDREMETENTE IN (444,446,445,62694)    OR NF.IDCLIENTE IN (444,446,445,62694) ) ");
        ssql.Append("  AND DATADECONCLUSAO IS NOT NULL");
        //ssql.Append("  and NF.PRAZOUTILIZADONOVO is null ");
        ssql.Append("  AND FL.IDFilial=" + cboFilial.SelectedValue);
        ssql.Append("  ORDER BY FL.NOME, NF.PRAZOUTILIZADO");

        RadGrid16.PageSize = Convert.ToInt32(100000000);
        DataTable dt = new SistranBLL.NF().ExcSQL(Session["Conn"].ToString(), ssql.ToString()).Tables[0];


        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();

    }

    public int DiferencaEntreDias(DateTime d1, DateTime d2)
    {
        int dif=0;
        TimeSpan ts = d1 - d2;
        dif = ts.Days;
        return Math.Abs(dif);
    }

    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Item.FindControl("HyperLink1");
        Label lblprazoMoises = (Label)e.Item.FindControl("lblprazoMoises");
        Label LBLIDCIDADE = (Label)e.Item.FindControl("LBLIDCIDADE");
        Label LBLIDESTADO = (Label)e.Item.FindControl("LBLIDESTADO");
        Label LBLCIDADE = (Label)e.Item.FindControl("LBLCIDADE");
        Label LBLESTADO = (Label)e.Item.FindControl("LBLESTADO");

        

        if (lblprazoMoises != null)
        {
            lblprazoMoises.Text = "a calcular";

            System.Data.DataRowView  linha  = (DataRowView)e.Item.DataItem;

            DateTime DataDeEntrada = (DateTime)linha["DataDeEntrada"];
            DateTime DataDeConclusao = (DateTime)linha["DataDeConclusao"];
            int descontar=0;
            
            DateTime dttemp = DataDeEntrada;
            int difEntreDias = DiferencaEntreDias(DataDeEntrada, DataDeConclusao );

            for (int i = 0; i < difEntreDias; i++)
            {
                if (dttemp.DayOfWeek == DayOfWeek.Saturday || dttemp.DayOfWeek == DayOfWeek.Sunday)
                descontar++;

                dttemp = dttemp.AddDays(1);
            }

            //DataTable dtFeriado = Sistran.Library.GetDataTables.RetornarDataTable("Select Coalesce(IdEstado,0) IdEstado, Coalesce(IdCidade,0) IdCidade, Data From Feriado where Data Between CONVERT(DATETIME,'" + linha["DataDeEntrada"] + "',103) and CONVERT(DATETIME,'" + linha["DataDeConclusao"] + "',103)");
            




            difEntreDias = difEntreDias - descontar;
            if (difEntreDias <= 0)
            {
                lblprazoMoises.Text = "1";
                difEntreDias = 1;
            }
            else
                lblprazoMoises.Text = difEntreDias.ToString();

            string strsql = "UPDATE DOCUMENTO SET PrazoUtilizadoNovo=" + difEntreDias + " WHERE IDDOCUMENTO=" + linha["IDDOCUMENTO"];
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, Session["Conn"].ToString());
        }
    }

    protected void cboTipoDes0_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarGrid();
    }

    protected void txtNf_TextChanged(object sender, EventArgs e)
    {
       
    }

    #endregion   

}