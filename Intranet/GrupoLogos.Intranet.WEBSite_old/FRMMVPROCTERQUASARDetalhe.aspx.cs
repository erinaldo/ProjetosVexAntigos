using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using ChartDirector;
using System.Web.UI.WebControls;

public partial class FRMMVPROCTERQUASARDetalhe : System.Web.UI.Page
{

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = 31;
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

                //btnGerarReport.Visible = false;
                //btnPDF.Visible = false;
                //btnGerarReport.Attributes.Add("onClick", "FullWindow('frmRptDesempenhoFilial.aspx?n=" + Guid.NewGuid() + "&tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                //btnPDF.Attributes.Add("onClick", "FullWindow('frmRptDesempenhoFilial.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");

                string[] DataConf = FuncoesGerais.DataConf();
                //txtI.Text = DataConf[0];
                //txtF.Text = DataConf[1];
                MontarTable();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            //Session["DataConf"] = txtI.Text + "|" + txtF.Text;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
            return;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
    }
    
    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

        string[] DataConf = FuncoesGerais.DataConf();


        DataTable dt = new SistranBLL.ProcterQuasar().RerotnarLista(DateTime.Parse(DataConf[0]), DateTime.Parse(DataConf[1]), Request.QueryString["cnpj"]);
        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();

        Session["dadosExport"] = dt;
        btnOrigemDados.Attributes.Add("onClick", "FullWindow('kpi/gerarexcel.aspx', 'NovaJanela2', 'yes'); return false;");

    }
    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        //MontarTable();
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        MontarTable();
    }
}
