using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subgurim.Controles;
using Artem.Web.UI.Controls;
using System.Configuration;

public partial class popMapaCidade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        mapa.Address = Request.QueryString["cidade"].ToString() + "," + Request.QueryString["uf"].ToString();
        mapa.Zoom = 15;

        GoogleMarker marcador1 = new GoogleMarker(Server.UrlDecode(Request.QueryString["cidade"].ToString()) + "," + Server.UrlDecode(Request.QueryString["uf"].ToString()));

        //GoogleMarker marcador1 = new GoogleMarker(latitude, longitude);
        marcador1.Text = "<h4><Font face='Verdana'>CIDADE: " + Server.UrlDecode(Request.QueryString["cidade"].ToString().ToUpper()) + "  UF: "+ Server.UrlDecode(Request.QueryString["uf"].ToString().ToUpper())+"</font> </h4>";
        marcador1.Clickable = true;
        mapa.Markers.Add(marcador1);
        mapa.Width = new Unit("100%");
        mapa.Height = 780;
        mapa.Key = ConfigurationSettings.AppSettings["ChaveMapsGoogle"].ToString();


    }
}
