using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.Web.Fatura
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Session["idtitulo"] = 3013933;
            //Response.Redirect("fatura.aspx?idtitulo=" + "3193&b=redlog_stnet2", false);
            //Response.Redirect("fatura.aspx?idtitulo=" + "11276&b=SBTransportes", false);
            Response.Redirect("fatura.aspx?idtitulo=" + "3409496", false);
           // Response.Redirect("fatura.aspx?idtitulo=" + "3381542", false);
           // Response.Redirect("frmGerarDocCob.aspx?idtitulo=" + "3013933", false);
        }
    }
}

//http://www.grupologos.com.br/duplicatas/fatura.aspx?idtitulo=3383257