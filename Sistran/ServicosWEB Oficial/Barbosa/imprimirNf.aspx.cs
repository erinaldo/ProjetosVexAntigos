using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Barbosa
{
    public partial class imprimirNf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid();
        }

        private void carregarGrid()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "Select Id, cast(SUBSTRING(chave, 26,9) as int) NF, Chave, convert(varchar(30), cast(Processado as datetime), 120) 'Recebido', case isnull(impresso, '2000-01-01') when  '2000-01-01' then 'NAO' else 'SIM' end Impresso, Origem, Destino from BarbosaNF where Iddocumento >= 0 and Processado>= getdate()-" + txtDias.Text + " Order By Processado, Impresso desc";

            if(RadioButtonList1.SelectedValue == "NaoImpresso")
                 sql = "Select Id, cast(SUBSTRING(chave, 26,9) as int) NF, Chave, convert(varchar(30), cast(Processado as datetime), 120) 'Recebido', case isnull(impresso, '2000-01-01') when  '2000-01-01' then 'NAO' else 'SIM' end Impresso , Origem, Destino from BarbosaNF where Iddocumento >= 0 and Processado>= getdate()-" + txtDias.Text + " and case isnull(impresso, '2000-01-01') when  '2000-01-01' then 'NAO' else 'SIM' end = 'NAO' Order By DataHoraRecbimento desc";

            if (RadioButtonList1.SelectedValue == "Impresso")        
                sql = "Select Id, cast(SUBSTRING(chave, 26,9) as int) NF, Chave, convert(varchar(30), cast(Processado as datetime), 120) 'Recebido', case isnull(impresso, '2000-01-01') when  '2000-01-01' then 'NAO' else 'SIM' end Impresso , Origem, Destino from BarbosaNF where Iddocumento >= 0 and Processado>= getdate()-" + txtDias.Text + " and case isnull(impresso, '2000-01-01') when  '2000-01-01' then 'NAO' else 'SIM' end = 'SIM' Order By DataHoraRecbimento desc";

            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "imprimir")
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string sql = "Update BarbosaNf set Impresso=getdate() where Chave='"+ e.CommandName.ToString()+"'; select 1";
                GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);


                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", "attachment; filename=" + e.CommandName + ".pdf;");
                response.TransmitFile(@"c:\tmp\"+ e.CommandName + ".pdf");
                response.Flush();
                carregarGrid();

                response.End();

            }
        }
    }
}