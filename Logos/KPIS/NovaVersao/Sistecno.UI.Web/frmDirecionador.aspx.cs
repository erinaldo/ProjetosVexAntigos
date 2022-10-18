using System;

namespace Sistecno.UI.Web
{
    public partial class frmDirecionador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboParceiro.Items.Add("sistecno");
               cboParceiro.Items.Add("express-tms");
               cboParceiro.Items.Add("ersystem");
           }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmPreLogin.aspx?p1=" + cboParceiro.SelectedValue, false);
        }
    }
}