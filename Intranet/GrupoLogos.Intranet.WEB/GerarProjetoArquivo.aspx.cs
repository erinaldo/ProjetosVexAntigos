using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class GerarProjetoArquivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["i"] == null)
            return;


        DataTable dtImages = (DataTable)Session["dtImages"];

        DataRow[] or = dtImages.Select("IDPROJETOARQUIVO=" + Request.QueryString["i"]);

        if (or.Length==0)
            return;

        byte[] readBuffer = (byte[])or[0]["Arquivo"];
        string nome = or[0]["nome"].ToString();

        System.IO.File.WriteAllBytes(Server.MapPath(@"imgReport/" + nome), readBuffer);
        FileAttributes cc = File.GetAttributes(Server.MapPath(@"imgReport/" + nome));

        FileInfo arquivo = new FileInfo(Server.MapPath(@"imgReport/" + nome));
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + arquivo.Name);
        Response.AddHeader("Content-Length", arquivo.Length.ToString());
        Response.Flush();
        Response.WriteFile(arquivo.FullName);       
        
    }
}