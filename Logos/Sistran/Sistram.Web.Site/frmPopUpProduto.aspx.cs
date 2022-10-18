using System;
using System.Data;
using System.IO;

public partial class frmPopUpProduto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.IO.File.Exists("imgReport/" + this.Request["cod"] + ".jpg"))
        {
            Image1.ImageUrl = "imgReport/" + this.Request["cod"] + ".jpg";
        }
        else
        {
            DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(this.Request["cod"]));

            if (dImagem.Rows.Count > 0)
            {
                byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + Convert.ToInt32(this.Request["cod"].ToString()) + ".jpg"));
                Image1.ImageUrl = "imgReport/" + Convert.ToInt32(this.Request["cod"].ToString()) + ".jpg";
            }
        }
    }
}
