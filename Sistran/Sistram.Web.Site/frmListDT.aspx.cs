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

public partial class frmListDT : System.Web.UI.Page
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
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                btnGerarReport.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "window.open('frmRptListDt.aspx?tipo=TELA&tit=" +  Server.UrlEncode(Request.QueryString["opc"].ToString()) +"', 'NovaJanela2', 'yes')");
                //btnPDF.Attributes.Add("onClick", "window.open('frmRptEntregas.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");
  
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
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU NF.", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

    }

    #endregion

    #region Methods

    private void CarregarGrid()
    {

        if (txtDt.Text.Length == 0)
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

        DateTime? ini = null, fim = null;

        if (txtI.Text.Length > 0)
            ini = Convert.ToDateTime(txtI.Text);

        if (txtF.Text.Length > 0)
            fim = Convert.ToDateTime(txtF.Text);

        int idDt = 0;
        try
        {
            idDt = int.Parse(txtDt.Text);

        }
        catch (Exception)
        {
        }

        RadGrid16.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue);
        DataTable dt = new SistranBLL.DocumentoDeTransporte().Pesquisar(idDt, ini, fim, (cboTipoDes1.SelectedValue.ToUpper()=="PENDENTES"?true:false), txtDt0.Text);

        Session["dt_DT"] = dt;

        RadGrid16.DataSource = dt;
        RadGrid16.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnGerarReport.Visible = true;
           // btnPDF.Visible = true;
        }
        else
        {
            btnGerarReport.Visible = false;
            //btnPDF.Visible = false;
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

    protected void txtDT_TextChanged(object sender, EventArgs e)
    {
        if (txtDt.Text.Length > 0)
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