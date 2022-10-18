using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.ViaVarejo
{
    public partial class Ocorrencias : System.Web.UI.Page
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
                string sql = " select D.IdDocumento, d.Numero, d.Serie, do.protocolo, do.DetalheEnvioParaCliente, do.EnviadoParaCliente, DetalheEnvio from documento d Inner join DocumentoOcorrencia  do on do.IDDocumento = d.IDDocumento where do.Protocolo is not null and enviadoParacliente >= Getdate()-45 Order by D.Numero, do.IDDocumentoOcorrencia";


                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                Label1.Text = dt.Rows.Count.ToString() + " pedidos encontrados.";

                GridView1.DataSource = dt;
                GridView1.DataBind();

                ExportToExcel();
            }
            catch (Exception ex)
            {
                string ee = ex.Message;
            }
        }


        public void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";



            if (GridView1.Visible == true)
            {
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
            else
            {
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
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}