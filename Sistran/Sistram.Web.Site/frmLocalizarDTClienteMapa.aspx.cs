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

public partial class frmLocalizarDTClienteMapa : System.Web.UI.Page
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
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("exec RETORNAR_POSICAO_VEICULO " + ILusuario[0].EmpresaId);

        if (Session["dtPesquisas"] == null)
            Session["dtPesquisas"] = DateTime.Now;

        DateTime d = DateTime.Parse(Session["dtPesquisas"].ToString());
        DataRow[] dr = dt.Select("DATANOTA='" + d.ToString("yyyy-MM-dd") + "'", "");

        for (int i = 0; i < dr.Length; i++)
        {
            GoogleMarker marcador1 = new GoogleMarker(double.Parse(dr[i]["lat"].ToString()), double.Parse(dr[i]["long"].ToString()));
            marcador1.Text = "DATA HORA: " + DateTime.Parse(dr[i]["dataHra"].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + " <BR>Latitude:" + double.Parse(dr[i]["lat"].ToString()) + " <BR>Longitude:" + double.Parse(dr[i]["long"].ToString());
            marcador1.Text += "<br>MOTORSTA: " + dr[i]["RazaoSocialNome"].ToString();
            marcador1.Text += "<br>PLACA: " + dr[i]["Placa"].ToString();
            marcador1.Text += "<br>N. DT: " + dr[i]["Numero"].ToString();
            marcador1.Text += "<br><a href='filtro.aspx?opc=Localizar Carga&gps=s&ic=" + ILusuario[0].EmpresaId + "&iddt=" + dr[i]["IDDT"].ToString() + "'>QUANTIDADE DE ENTREGAS: " + dr[i]["Notas"].ToString() + "</a>";

            if (i == 0)
            {

                mapa.Latitude = double.Parse(dr[0]["lat"].ToString());
                mapa.Longitude = double.Parse(dr[0]["long"].ToString());
            }

            marcador1.Clickable = true;
            mapa.Markers.Add(marcador1);
        }
        
        mapa.Width = new Unit("100%");
        mapa.Height = 780;
        //mapa.Latitude = double.Parse(dt.Rows[0]["lat"].ToString());
        //mapa.Longitude = double.Parse(dt.Rows[0]["long"].ToString());
        mapa.Zoom = 15;


        //mapa.Directions.Add(new GoogleDirection("route", "Osasco, Rua Ipiranga, SP"));
        //mapa.Directions.Add(new GoogleDirection("route", "San Francisco, CA to Mountain View, CA"));
        mapa.Key = ConfigurationSettings.AppSettings["ChaveMapsGoogle"].ToString();    
    }
}