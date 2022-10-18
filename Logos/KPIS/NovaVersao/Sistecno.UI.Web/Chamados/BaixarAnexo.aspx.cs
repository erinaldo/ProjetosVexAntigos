using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistecno.DAL.BD;

namespace SistecnoWeb.HelpDesk
{
    public partial class BaixarAnexo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string     cnx = new   Sistecno.DAL.BD.ConexaoPrincipal("").CxPrincipal;

            DataTable dt = cDb.RetornarDataTable("SELECT ARQUIVO, NOMEARQUIVO FROM TICKETMOVIMENTOANEXO WHERE IDTICKETMOVIMENTOANEXO=" + Request.QueryString["id"], cnx);


            byte[] arq = (byte[])dt.Rows[0]["ARQUIVO"];
            FileStream Stream = new FileStream(MapPath("../Log") + "\\" + dt.Rows[0]["NOMEARQUIVO"].ToString(), FileMode.Create);

            //Escrevo arquivo no fluxo
            Stream.Write(arq, 0, arq.Length);

            //Fecho fluxo pra finalmente salvar em disco
            Stream.Close();


            FileInfo fInfo = new FileInfo(MapPath("../Log") + "\\" + dt.Rows[0]["NOMEARQUIVO"].ToString());
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + dt.Rows[0]["NOMEARQUIVO"].ToString() + "\"");
            HttpContext.Current.Response.AddHeader("Content-Length", arq.Length.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.WriteFile(MapPath("../Log") + "\\" + dt.Rows[0]["NOMEARQUIVO"].ToString());
            fInfo = null;
        }
    }
}