using System;

public partial class LoginLogos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //rmp.TabIndex = 0;
            //RadTabStrip1.SelectedIndex = 0;
            //rpvPromo.Selected = true;
            
            
        }
    }

    protected void RadTabStrip1_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
    {
        //Session.Clear();
        //Session.Abandon();
        //Request.Cookies.Clear();
    }

}
