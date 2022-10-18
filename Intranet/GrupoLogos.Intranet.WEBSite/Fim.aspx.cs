using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SistranMODEL;
using System.Collections.Generic;

public partial class Fim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Usuario> ILusuario = (List<Usuario>)Session["USUARIO"];
        Request.Cookies.Clear();
        FormsAuthentication.SignOut();
        //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "FECHOU A JANELA DO BROWSER.", "SAIU DO SITE");
        Session.Clear();        
        //Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
        Response.Redirect("frmLoginGrupoLogos.aspx");
    }
}
