using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ServicosWEB.MaisBrasil
{
    public partial class NfeEntrada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGrid();
            }
        }

        string cnx = "";

        private void CarregarGrid()
        {
            try
            {
                string sql = "  select  top 100 Id,NrNfe,DataHora, IdDocumento from  MaisBrasilLogRemessaRecebimento ";

                if (TextBox1.Text.Length > 0)
                    sql += " and NrNfe = '" + TextBox1.Text + "'";
                
                sql += " order by Datahora desc";


                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                Label1.Text = dt.Rows.Count.ToString() + " NFe encontrados.";

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                string ee = ex.Message;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "ver")
            {
                //Response.Redirect("ver.aspx?i=" + e.CommandName.ToString());

                Response.Write("<script>window.open('WebForm1.aspx?i="+ e.CommandName.ToString()+"','_blank'); </script>");

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}