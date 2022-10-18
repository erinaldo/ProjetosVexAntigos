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
    public partial class frmBaixarXMLDacte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strsql = "";
            strsql += " SELECT TOP 1 IDNOTA,ULTIMOARQUIVOXML FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + Request.QueryString["idnota"] + "'";
            //strsql += " SELECT TOP 1 IDNOTA,ULTIMOARQUIVOXML FROM DOCUMENTOELETRONICO WHERE IDNOTA='35130767506105000600570040000109881000109885'";
            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            if (ds.Rows.Count > 0)
            {
                string cam = Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".html";

                if (File.Exists(Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".html"))
                {
                    File.Delete(Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".html");
                }

                string conteudo = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'></head>";
                conteudo += "<body>    ";
                conteudo += "<form action='http://www.webdanfe.com.br/danfe/GeraDanfe.php' name='one' enctype='multipart/form-data' method='post'>    ";
                //conteudo += "<input type='submit' value='enviar'>";
                conteudo += "<textarea name='arquivoXml' cols='150' rows='50' style='visibility:hidden'>";
                conteudo += " @@@nota@@@   </textarea>    </form>    <script> document.one.submit(); </script></body></html>";

                StreamWriter wr = new StreamWriter(cam, true);

               

                wr.WriteLine(conteudo.Replace("@@@nota@@@", ds.Rows[0]["ULTIMOARQUIVOXML"].ToString()));
                wr.Close();
                this.ifx.Attributes.Add("src", "XmlGerados/" + ds.Rows[0]["IdNota"].ToString() + ".html");
            }
        }
    }
}