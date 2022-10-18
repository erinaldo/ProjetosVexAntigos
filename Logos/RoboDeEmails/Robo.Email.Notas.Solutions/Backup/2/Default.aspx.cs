using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("nfrobo.aspx?fl=01&tipo=xxx");
        //Response.Redirect("INICIAL.ASPX?cliente=3241*&data=01/08/2011&data2=16/08/2011&nu=nomeDoUsuario");



        //http://xxxx/frmINICIAL.ASPX?cliente=3241*&data=01/08/2011&data2=12/08/2011&nu=nomeDoUsuario
    }
}
