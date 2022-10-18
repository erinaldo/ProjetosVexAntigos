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
using System.Threading;

namespace Sistecno.Web.Fatura
{
    public partial class frmBaixarXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strsql = "";
            strsql += " SELECT TOP 1 IDNOTA,ULTIMOARQUIVOXML FROM DOCUMENTOELETRONICO WHERE IDNOTA='" + Request.QueryString["idnota"] + "'";
            DataTable ds = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, Session["cnx"].ToString()).Tables[0];

            if (ds.Rows.Count > 0)
            {
                string cam = Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".xml";

                if (File.Exists(Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".xml"))
                {
                    File.Delete(Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".xml");
                }

                //if (ds.Rows[0]["ULTIMOARQUIVOXML"].ToString().Contains("<vBC>0.00</vBC>"))
                //{
                //    Label1.Text = "Não foi possível gerar o arquivo.";
                //    string texto = "Data: " + DateTime.Now.ToString() + " <br>Fatura: " + Session["idtitulo"];
                //    texto += "<br>Chave: " + ds.Rows[0]["IdNota"].ToString();

                //    texto += "<br><br><br>" + ds.Rows[0]["ULTIMOARQUIVOXML"].ToString();
                //    texto += "<br>IP: " + Request.UserHostAddress;
                //    Sistran.Library.EnviarEmails.EnviarEmailx("edvaldo@sistecno.com.br", "moises@sistecno.com.br", "Fatura com Frete Zerado", "Direto no link <br>" +texto, "Fatura com FRETE ZERADO");
                //    return;
                //}

                StreamWriter wr = new StreamWriter(cam, true);
                wr.WriteLine(ds.Rows[0]["ULTIMOARQUIVOXML"].ToString());
                wr.Close();

               // Thread.Sleep(10000);


                string caminho = Server.MapPath("XmlGerados") + "\\" + ds.Rows[0]["IdNota"].ToString() + ".xml";

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "text/xml";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Request.QueryString["idnota"] + ".xml");
                Response.TransmitFile(caminho);
                Response.End();
                Response.Flush();
                Response.Clear();
            }
        }
    }
}