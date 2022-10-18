using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistecno.UI.Web.HR
{
    public partial class ExportarExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "";
            if( Request.QueryString["tipo"]=="PendenciaAnteriores")
            sql = "SELECT * FROM VW_LIBERACAO_PEDIDOS_DETALHE WHERE IDCLIENTE=150000 ORDER BY 4";
            else
                sql = "SELECT * FROM VW_LIBERACAO_PEDIDOS_DETALHE_ATUAL WHERE IDCLIENTE=150000 ORDER BY 4";

            DataTable dt = new CentralWssLogos.wsExecutarComado().ExecSql("SISTECNO", "@ONCETSIS12122014", sql);

            string attachment = "attachment; filename="+Guid.NewGuid()+".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();          
          
          }
    }
}