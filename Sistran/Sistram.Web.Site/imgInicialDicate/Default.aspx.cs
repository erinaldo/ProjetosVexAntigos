using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class imgInicialDicate_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (fl.HasFile == true && (Path.GetExtension(fl.PostedFile.FileName) == ".jpg" || Path.GetExtension(fl.PostedFile.FileName) == ".jpg"))
        {            
            int intTamanho = System.Convert.ToInt32(fl.PostedFile.InputStream.Length);
            byte[] imageBytes = new byte[intTamanho];
            fl.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);
            
            if(File.Exists(Server.MapPath("~/imgInicialDicate") + "\\inicial.jpg"))
            {
                File.Delete(Server.MapPath("~/imgInicialDicate") + "\\inicial.jpg");
            }

            fl.SaveAs(Server.MapPath("~/imgInicialDicate") + "\\inicial.jpg");

            Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int intTamanho = System.Convert.ToInt32(fl.PostedFile.InputStream.Length);
        byte[] imageBytes = new byte[intTamanho];
        fl.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);

        if (File.Exists(Server.MapPath("~/imgInicialDicate") + "\\inicial.jpg"))
        {
            File.Delete(Server.MapPath("~/imgInicialDicate") + "\\inicial.jpg");
        }        
    }
}
