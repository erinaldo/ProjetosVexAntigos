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
            Response.Redirect("fatura.aspx?idtitulo=C603794B-4A64-416F-B481-5B30C055023B", false);
            //Response.Redirect("fatura.aspx?idtitulo=" + "11276&b=SBTransportes", false);
            //Response.Redirect("fatura.aspx?idtitulo=" + "9007&b=RedLog_Stnet2", false);
            // Response.Redirect("fatura.aspx?idtitulo=" + "3693269", false);
            // Response.Redirect("frmGerarDocCob.aspx?idtitulo=" + "3013933", false);
        }
    }
}

//http://www.grupologos.com.br/duplicatas/fatura.aspx?idtitulo=3383257
//http://www2.logoslogistica.com.br/DUPLICATAS/FATURA.ASPX?IDTITULO=3621706 //