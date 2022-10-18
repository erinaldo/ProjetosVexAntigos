using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class frmFotoMotorista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            Button2.Attributes.Add("Onclick", "window.close();");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (fileUploadArquivo.HasFile == true && (Path.GetExtension(fileUploadArquivo.PostedFile.FileName) == ".jpg" || Path.GetExtension(fileUploadArquivo.PostedFile.FileName) == ".gif"))
        {
            int intTamanho = System.Convert.ToInt32(fileUploadArquivo.PostedFile.InputStream.Length);
            byte[] imageBytes = new byte[intTamanho];
            fileUploadArquivo.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);
            string nome = Guid.NewGuid().ToString();
            fileUploadArquivo.SaveAs(Server.MapPath("imgReport") + "\\" + nome + ".jpg");
            Image1.ImageUrl = "imgReport/" + nome + ".jpg";
            Session["imageBytes"] = imageBytes;
            Button2.Attributes.Add("Onclick", "javascript:window.opener.document.getElementById('" + Request["controle"] + "').value = '" + nome + ".jpg" + "'; window.close();");
        }
        else
            Button2.Attributes.Add("Onclick", "window.close();");
    }
}
