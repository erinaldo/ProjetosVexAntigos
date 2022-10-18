using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Artem.Web.UI.Controls;
using System.Configuration;

public partial class UserControls_MapaDT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if(!IsPostBack)
            montarMapaTodasDTS();
    }


    private void montarMapaTodasDTS()
    {
        try
        {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("exec RETORNAR_POSICAO_VEICULO " + ILusuario[0].EmpresaId);
                
            if (Session["dtPesquisas"] == null)
                    Session["dtPesquisas"] = DateTime.Now;
            
            DateTime d = DateTime.Parse(Session["dtPesquisas"].ToString());
            DataRow[] dr = dt.Select("DATANOTA='"+ d.ToString("yyyy-MM-dd") +"'", "");

            if (dr.Length == 0)
                mapa.Visible = false;
            //else
            //    mapa.Visible = true;

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

                mapa.Width = 350;
                mapa.Height = 350;

                mapa.Zoom = 15;
                mapa.Key = ConfigurationSettings.AppSettings["ChaveMapsGoogle"].ToString();

                Panel1.Controls.Add(mapa);
            }
        
        catch (Exception xe)
        {

            throw xe;
        }
    }
}