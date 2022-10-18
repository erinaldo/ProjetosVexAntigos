using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Sistecno.Web.Fatura
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
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
                
               
                   // CarregaImagemPrincipal();
            }

        }

        private void CarregaImagemPrincipal()
        {
            string m = "";

             if(Request.QueryString["b"]!=null)                 
                m= FuncoesGerais.LoadDataSetLogo(Request.QueryString["b"].ToString());

            
            if (m != "")
            {
                imgLogoPrincipal.ImageUrl = "LogoCliente/" + m;
            }
            else
            {
              //  imgLogoPrincipal.ImageUrl = "Imagens/LOGOS-LOGTRANSP-03.jpg";
            }
        }  
    }
}