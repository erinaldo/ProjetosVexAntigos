using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Girotrade
{
    public partial class Pedidos : System.Web.UI.Page
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
                string sql = " select top " + DropDownList2.SelectedValue + " p.Numero [Número], DataDeEntrada [Entrada],  dest.CnpjCpf [CNPJ/CPF], dest.RazaoSocialNome [Destinatário], DocumentodoCliente4 [Código Vex], (Select top 1 Status from YandehStatusPedido where NrPedido = p.Numero order by Id desc)  Status  " +
                 " from DOCUMENTOPEDIDO p " +
                 " Inner join Cadastro dest on dest.IDCadastro = p.IDDestinatario " +
                 " where Ativo = 'SIM' and p.IdDocumento>100";

                if (txtDataI.Text == "" && txtDataF.Text == "")
                {
                    sql += " and DataDeEntrada >= GETDATE() - 30 ";
                    sql += " and DataDeEntrada >= '2020-05-01' ";
                }

                sql += " and p.IDCliente=114091" +
                 " and DocumentodoCliente4 is not null ";

                if (TextBox1.Text.Length > 0)
                    sql += " and p.Numero =" + TextBox1.Text;

                if (txtDataI.Text != "" && txtDataF.Text != "")
                {
                    sql += "and cast(DataDeEntrada as date)  between cast('" + DateTime.Parse(txtDataI.Text.Trim()).ToString("yyyy-MM-dd") + "' as date) and cast('" + DateTime.Parse(txtDataF.Text.Trim()).ToString("yyyy-MM-dd") + "' as date)";
                }

                if (TextBox1.Text.Length == 0)
                {
                    if (DropDownList1.SelectedValue == "Pendentes")
                        sql += " and (Select top 1 Status from YandehStatusPedido where NrPedido = p.Numero order by Id desc) <> 'LIBERADO PARA FATURAMENTO' ";


                    if (DropDownList1.SelectedValue == "Faturados")
                        sql += " and (Select top 1 Status from YandehStatusPedido where NrPedido = p.Numero order by Id desc) = 'LIBERADO PARA FATURAMENTO' ";
                }

                sql += " Order by p.DataDeEntrada desc";


                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                Label1.Text = dt.Rows.Count.ToString() + " pedidos encontrados.";

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
                Response.Redirect("PedidoDetalhe.aspx?i=" + e.CommandName.ToString());
            }

            if (e.CommandArgument.ToString() == "AlterarStatus")
            {
                string sql = "Update YandehStatusPedido set Consumido=null where NrPedido='" + e.CommandName.ToString() + "' and Status='LIBERADO PARA FATURAMENTO'; SELECT 1 ";
                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
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