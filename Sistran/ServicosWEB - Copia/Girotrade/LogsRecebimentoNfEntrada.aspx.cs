using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Girotrade
{
    public partial class LogsRecebimentoNfEntrada : System.Web.UI.Page
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
            if (TextBox1.Text == "")
                return;
            try
            {
                string sql = "select * from YandehLogRecebimento where chave like '%"+ TextBox1.Text + "%' and acao not like '%Tempo Limite de Execução%' and Acao <> 'Inico da Inclusao'";
                    

                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);               

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
            if (e.CommandArgument.ToString().Contains("reenviar"))
            {


                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                //GridViewRow row = GridView1.Rows[index];
                //TextBox txtGridData = (TextBox)row.FindControl("txtIdDocumento");



                //string sql = "Update Romaneio set  Andamento ='INTEFACE LIBERADA' WHERE IDROMANEIO=" + e.CommandName;
                //sql += "; Update documento set EnviadoComprovei =null where Iddocumento =" + e.CommandArgument.ToString().Replace(",reenviar", "").Replace("reenviar","") ;
                //cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                //DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";



            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);
                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();


            }


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}