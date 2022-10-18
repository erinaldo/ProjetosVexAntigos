using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class frmVerFoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (Session["imagem"] != null)
            {
                byte[] imagem = (byte[])Session["imagem"];
                string x = DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "");
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + x + ".jpg"));
                Image2.ImageUrl = "imgReport/" + x + ".jpg";
                Session["caminho"] = Server.MapPath("~\\imgReport\\") + x + ".jpg";
            }
             
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
