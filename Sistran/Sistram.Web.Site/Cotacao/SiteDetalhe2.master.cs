using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class SiteMasterDetalhe2 : System.Web.UI.MasterPage
{
//    private static List<SistranMODEL.Usuario> LUser;
    private void CarregaImagemPrincipal()
    {
        //string m = FuncoesGerais.LoadDataSetLogo(Session["ConexaoCliete"].ToString());

        //if (m != "")
        //{
       //     imgLogoPrincipal.Src = null;
            //imgLogoPrincipal.Src = "LogoCliente/" + m;
       // }
    }      

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            int dia = DateTime.Now.Day;
            int ano = DateTime.Now.Year;
            string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
            string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
            string data = diasemana + ", " + dia + " de " + mes + " de " + ano;
            lblDataHora.Text = data;

            this.Page.Title ="Bem Vindo";
           
        }
    }
}
