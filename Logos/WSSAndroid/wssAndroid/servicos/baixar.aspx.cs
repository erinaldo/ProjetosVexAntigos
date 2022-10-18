using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class baixar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        FileInfo arquivo = new FileInfo(Server.MapPath(@"RSMobileAndroid/EntregasSistecno.apk"));
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + arquivo.Name);
        Response.AddHeader("Content-Length", arquivo.Length.ToString());
        Response.Flush();
        Response.WriteFile(arquivo.FullName);
    }
}