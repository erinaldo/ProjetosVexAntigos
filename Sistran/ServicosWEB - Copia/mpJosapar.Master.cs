using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ServicosWEB
{
    public partial class mpJosapar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("loginJosapar.aspx");

          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("JosaparEstoqueValidade.aspx");
        }

       
    }
}