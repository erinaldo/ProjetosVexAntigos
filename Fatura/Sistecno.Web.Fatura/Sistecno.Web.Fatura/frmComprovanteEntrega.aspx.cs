using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;

namespace Sistecno.Web.Fatura
{
    public partial class frmComprovanteEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strsql = "";
            strsql += " SELECT TOP 1 * FROM DOCUMENTOOCORRENCIAARQUIVO DOA WHERE DOA.IDDOCUMENTOOCORRENCIAarquivo =" + Request.QueryString["idDoc"] ;
            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            if (ds.Rows.Count > 0)
            {
                string cam = Server.MapPath("XmlGerados") + "\\" + Request.QueryString["idDoc"] + ".jpg";

                if (File.Exists(Server.MapPath("XmlGerados") + "\\" + Request.QueryString["idDoc"] + ".jpg"))
                {
                    File.Delete(Server.MapPath("XmlGerados") + "\\" + Request.QueryString["idDoc"] + ".jpg");
                }


                //StreamWriter wr = new StreamWriter(cam, true);
                //wr.WriteLine(ds.Rows[0]["Arquivo"].ToString());
                //wr.Close();


                byte[] imagem = (byte[])ds.Rows[0]["Arquivo"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

                returnImage.Save(cam);
                



                string caminho = Server.MapPath("XmlGerados") + "\\" + Request.QueryString["idDoc"] + ".jpg";

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "text/jepg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Request.QueryString["idDoc"] + ".jpg");
                Response.TransmitFile(caminho);
                Response.End();
                Response.Flush();
                Response.Clear();
            }
        }
    }
}