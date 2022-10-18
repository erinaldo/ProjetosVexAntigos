using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

public partial class frmProjeto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
            CarregarGrid();
        }
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Projeto().Filtrar(null);
        RadGridUsuarios.DataSource = dt;
        RadGridUsuarios.DataBind();
    }
    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }
    protected void RadGrid16_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }
}