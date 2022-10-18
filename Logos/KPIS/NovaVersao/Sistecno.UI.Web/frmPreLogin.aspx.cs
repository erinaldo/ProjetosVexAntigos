using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web 
{
    public partial class frmPreLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                Session["p1"] = Request.QueryString["p1"];               
                CarregarPaginaBanco();              
            }
        }

        private void CarregarPaginaBanco()
        {     

            DataTable dt = DAL.TelaInicial.Secoes(Request.QueryString["P1"]);

            if (dt.Rows.Count > 0)
            {
                lblsecao1.Text = dt.Rows[0]["Titulo"].ToString();
                lblSecao1Texto.Text = dt.Rows[0]["Texto"].ToString();

                lblSecao2.Text = dt.Rows[1]["Titulo"].ToString();
                lblSecao2Texto.Text = dt.Rows[1]["Texto"].ToString();

                lblSecao3.Text = dt.Rows[2]["Titulo"].ToString();
                lblSecao3Texto.Text = dt.Rows[2]["Texto"].ToString();


                byte[] imagem = (byte[])dt.Rows[0]["Logotipo"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

                if (File.Exists(Server.MapPath("TMP/") + dt.Rows[0]["Titulo"].ToString() + ".jpg"))
                {
                    File.Delete(Server.MapPath("TMP/") + dt.Rows[0]["Titulo"].ToString() + ".jpg");
                }
                returnImage.Save(Server.MapPath(@"~/TMP/" + dt.Rows[0]["Titulo"].ToString() + ".jpg"));                
                imgLogo.ImageUrl = "TMP/" +  dt.Rows[0]["Titulo"].ToString() + ".jpg";
                Session["logoCliente"] = "TMP/" + dt.Rows[0]["Titulo"].ToString() + ".jpg";


                imgLogo.AlternateText = dt.Rows[0]["Titulo"].ToString();
                returnImage.Dispose();
                ms.Dispose();
                imagem = null;
            }
        }
    }
}