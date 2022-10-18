using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class SiteMasterDetalhe : System.Web.UI.MasterPage
{
    private static List<SistranMODEL.Usuario> LUser;
    private void CarregaImagemPrincipal()
    {
        if (Session["ConexaoCliete"] != null)
        {
            string m = FuncoesGerais.LoadDataSetLogo(Session["ConexaoCliete"].ToString());

            if (m != "")
            {
                imgLogoPrincipal.Src = null;
                imgLogoPrincipal.Src = "LogoCliente/" + m;
            }
        }
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

            CarregaImagemPrincipal();
            if (Session["ProfileIndex"] != null && !string.IsNullOrEmpty(Session["ProfileIndex"].ToString()) && Convert.ToInt32(Session["ProfileIndex"].ToString()) >= 0)
            {
                LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
                SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
                Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
                lblUserName.Text = "Usuário: " + Usuario.UsuarioNome;
                this.Page.Title = Usuario.UsuarioNome;
   
            }
            else
            {
                lblUserName.Text = "Visualização de Nota / Pedido";
                this.Page.Title = "Grupo Logos";

                //Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
               // Response.End();
            }
        }
    }
}
