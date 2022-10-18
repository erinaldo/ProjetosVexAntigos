using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artem.Web.UI.Controls;

public partial class objMapa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GoogleMap1.Directions.Add(new GoogleDirection("route","rua ipiranga, 282, osasco, sp"));
    }
}