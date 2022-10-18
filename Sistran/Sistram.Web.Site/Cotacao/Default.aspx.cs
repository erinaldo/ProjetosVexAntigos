using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cotacao_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        //Response.Redirect("frmaprovarRequisicao.aspx?b=stnnovo&I=104&iu=2406");
        Response.Redirect("COTACAOFORNECEDOR.ASPX?I=1284");
        //Response.Redirect("CotacaoFornecedor.aspx?I=1241");
        
    }
}