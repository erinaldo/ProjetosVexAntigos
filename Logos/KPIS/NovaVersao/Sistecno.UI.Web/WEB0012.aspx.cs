using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web
{
    public partial class WEB0012 : System.Web.UI.Page
    {
        string cnx = "";
        DAL.Models.Usuario usuarioLogado;        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                cnx = Session["CNX"].ToString();
                //Session.Add("cnx", cnx); 
                usuarioLogado = (Sistecno.DAL.Models.Usuario)Session["USUARIOLOGADO"];

                if (!IsPostBack)
                {
                    FileUpload1.Attributes.Add("cnx", cnx);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {

        }
    }
}