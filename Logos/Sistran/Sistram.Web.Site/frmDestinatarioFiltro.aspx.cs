using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
//using SistranBLL;
using System.Globalization;
using System.Threading;


public partial class frmDestinatarioFiltro : System.Web.UI.Page
{
    //const string Situacao = "ConstDcoFilSitAguardEmbarque";
    #region Events  

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                btnNovo.Attributes.Add("OnClick", "javascript:window.open('frmDestinatarioDetalhe.aspx?idCadastro=novo');");
            }
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
        try
        {

            string condicao = "";

            if (cboTipoDes.SelectedValue == "0")
            {
                condicao = "CNPJCPF like '%" + txtDest.Text + "%' OR RAZAOSOCIALNOME like '%" + txtDest.Text + "%' OR FANTASIAAPELIDO like '%" + txtDest.Text + "%'";
            }
            else
            {
                condicao = cboTipoDes.SelectedValue + " like '" + txtDest.Text + "%'";
            }

            DataTable dt = new SistranBLL.Cadastro().Read(condicao);

            RadGrid16.PageSize = Convert.ToInt32(cboTipoDes0.SelectedValue);
            RadGrid16.DataSource = dt;

            RadGrid16.DataBind();
            Session["dt"] = dt;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
   
     #endregion


    protected void btnNovo_Click(object sender, EventArgs e)
    {

    }
    protected void txtDest_TextChanged(object sender, EventArgs e)
    {
        CarregarGrid();
    }
}