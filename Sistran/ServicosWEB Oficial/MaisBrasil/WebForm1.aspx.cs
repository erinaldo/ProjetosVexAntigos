using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.MaisBrasil
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "  select  top 100 * from  MaisBrasilLogRemessaRecebimento ";
            sql += " where  ID = '" + Request.QueryString["i"] + "'";            

            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

            if(dt.Rows.Count >0)
            {
                Label1.Text = "NfeEntrada: " + dt.Rows[0]["NrNfe"].ToString();
                //Label2.Text = dt.Rows[0]["XML"].ToString();

                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.LoadXml(dt.Rows[0]["XML"].ToString());

                Xml1.Document = xml;
            }
        }
    }
}