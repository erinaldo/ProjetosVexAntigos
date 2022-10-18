using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;
using Artem.Web.UI.Controls;
using System.Configuration;

public partial class frmLocalizaoNota : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

            carregar();
        }
    }

    private void carregar()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("exec RETORNAR_POSICAO_NF " + Request.QueryString["iddoc"]);

        int passou = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
   

            if (dt.Rows[i]["PontodeOcorrencia"].ToString() == "SIM" && passou ==0)
            {
                GoogleMarker marcador1 = new GoogleMarker(double.Parse(dt.Rows[i]["latitude"].ToString()), double.Parse(dt.Rows[i]["longitude"].ToString()));
                marcador1.Text += "<b>NOTA ENTREGUE / FINALIZADA</b>";

                marcador1.Text += "<br>DATA HORA: " + DateTime.Parse(dt.Rows[i]["dataHora"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " <BR>Latitude:" + double.Parse(dt.Rows[i]["latitude"].ToString()) + " <BR>Longitude:" + double.Parse(dt.Rows[i]["longitude"].ToString());
                marcador1.Clickable = true;
                marcador1.Text += "<br>Nota Fiscal: " + dt.Rows[i]["NF"].ToString();
                marcador1.Text += "  -  Série: " + dt.Rows[i]["SERIE"].ToString();
                marcador1.Text += "<br>Ocorre: " + dt.Rows[i]["Nome"].ToString();
                marcador1.Text += "<br><a href='filtro.aspx?gps=s&opc=Detalhe Nota Fical&iddoc=" + dt.Rows[i]["IDDOCUMENTO"].ToString() + "' >Clique para ver DETALHE </a>";
                marcador1.IconUrl = "Imagens/Caminhao.gif";
                GoogleSize si = new GoogleSize(40, 40);
                marcador1.IconSize = si;
                mapa.Markers.Add(marcador1);

                mapa.Latitude = double.Parse(dt.Rows[i]["latitude"].ToString());
                mapa.Longitude = double.Parse(dt.Rows[i]["longitude"].ToString());
                passou++;
            }

            
        }  
        
        mapa.Width = new Unit("100%");
        mapa.Height = 780;
        //mapa.Latitude = double.Parse(dt.Rows[0]["latitude"].ToString());
        //mapa.Longitude = double.Parse(dt.Rows[0]["longitude"].ToString());
        mapa.Zoom = 15;


        //mapa.Directions.Add(new GoogleDirection("route", "Osasco, Rua Ipiranga, SP"));
        //mapa.Directions.Add(new GoogleDirection("route", "San Francisco, CA to Mountain View, CA"));
        mapa.Key = ConfigurationSettings.AppSettings["ChaveMapsGoogle"].ToString();    
    }
}