using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ServicosWEB
{
    public partial class mp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("loginGaiola.aspx");

            if (((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() == "5298")
            {
                Button3.Enabled = false;
                Button5.Enabled = false;
                Button4.Enabled = false;
                Button7.Enabled = false;
                Button6.Enabled = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gaiolas.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("GaiolaManut.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("GaiolaDiiver.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("GaiolaRelatorios.aspx");

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("GaiolaConsultaVolumes.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("GaiolaLog.aspx");

        }

        protected void btnEtiquetaCarreta_Click(object sender, EventArgs e)
        {
            Response.Redirect("ETIQUETACarreta.aspx");
        }

        
        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("InformarVolumes.aspx");

        }
    }
}