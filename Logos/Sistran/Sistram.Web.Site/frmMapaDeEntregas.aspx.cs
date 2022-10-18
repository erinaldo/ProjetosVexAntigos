using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artem.Web.UI.Controls;

public partial class frmMapaDeEntregas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {



        mapa.Address = "Sao Paulo, sp";
        mapa.Zoom = 7;

       // GoogleMarker marcador1 = new GoogleMarker("Sao paulo, sp");
        //GoogleMarker marcador2 = new GoogleMarker("Osasco, sp");

        GoogleMarker marcador1 = new GoogleMarker(double.Parse("-23,4985845"), double.Parse("-46,8242799"));
        GoogleMarker marcador2 = new GoogleMarker(double.Parse("-23,4965333"), double.Parse("-46,8942444"));
        GoogleMarker marcador3 = new GoogleMarker(double.Parse("-23,0000000"), double.Parse("-45,0000000"));
        
        marcador1.Text = "<h4><Font face='Verdana'>CIDADE: Sao Paulo  UF: SP </font> </h4>";
        marcador1.Clickable = true;
        mapa.Markers.Add(marcador1);
        
        marcador2.Text = "<h4><Font face='Verdana'>CIDADE: Osasco  UF: SP </font> </h4>";
        marcador2.Clickable = true;
        mapa.Markers.Add(marcador2);
        
        marcador3.Text = "<h4><Font face='Verdana'>CIDADE: Osasco  UF: SP </font> </h4>";
        marcador3.Clickable = true;
        mapa.Markers.Add(marcador3);
        
        
        
        mapa.Width = new Unit("100%");
        mapa.Height = 780;
        mapa.Latitude = double.Parse("-23,4985845");
        mapa.Longitude = double.Parse("-46,8242799");



        mapa.Directions.Add(new GoogleDirection("route", "Osasco, Rua Ipiranga, SP"));
        //mapa.Directions.Add(new GoogleDirection("route", "San Francisco, CA to Mountain View, CA"));
        

        
        mapa.Key = "ABQIAAAA5LdWdrpUdaMd2vHvrVCi1xTMWSmi_MSuKkTW-Hv7c6tRJoBLARQOMvyXtIOJbTJFXAu8YeoAhavb5A";//ConfigurationSettings.AppSettings["ChaveMapsGoogle"].ToString();
    }
}