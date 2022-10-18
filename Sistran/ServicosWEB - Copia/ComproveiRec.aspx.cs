using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;
using System.IO;
using System.Drawing;

namespace ServicosWEB
{
    public partial class ComproveiRec : System.Web.UI.Page
    {

        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("Select IdFilial, NomeComprovei from Filial where Ativo='sim' and NomeComprovei is not null order by 2", cnx);

               
                cboFilial.DataSource = dt;
                cboFilial.DataTextField = "NomeComprovei";
                cboFilial.DataValueField = "IdFilial";
                cboFilial.DataBind();

                cboFilial.Items.Insert(0, new ListItem("Selecione", ""));
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            pesquisar();
        }

        private void pesquisar()
        {
            string sqlEnviados = "";
            sqlEnviados += " SELECT TOP 1000  RETORNOCOMPROVEI.IDDOCUMENTO,   PROTOCOLO, DOCUMENTO.NUMERO , SERIE , FIL.NOMECOMPROVEI FILIAL,  CHAVE CHAVE, DATADAOCORRENCIA [DT. OCORRENCIA], OCORRENCIA, IDOCORRENCIACOMPROVEI, ";
            sqlEnviados += " (CASE WHEN FOTO IS NULL THEN 'NAO' ELSE 'SIM' END ) FOTO, ";
            sqlEnviados += " HORARIORECEBIMENTO [RECEBIDO COMPROVEI], C.FANTASIAAPELIDO CLIENTE, D.FANTASIAAPELIDO DESTINATARIO,  ";
            sqlEnviados += " PROCESSADO [PROCESSADO NO SISTRAN] ";
            sqlEnviados += " FROM RETORNOCOMPROVEI   with (nolock)";
            sqlEnviados += " INNER JOIN DOCUMENTO   with (nolock) ON DOCUMENTO.IDDOCUMENTO = RETORNOCOMPROVEI.IDDOCUMENTO ";
            sqlEnviados += " INNER JOIN FILIAL FIL ON FIL.IDFILIAL = DOCUMENTO.IDFILIALATUAL ";
            sqlEnviados += " INNER JOIN CADASTRO C   with (nolock) ON C.IDCADASTRO = IDCLIENTE ";
            sqlEnviados += " INNER JOIN CADASTRO D    with (nolock)ON D.IDCADASTRO = IDDESTINATARIO ";
            
            if (TextBox1.Text.Trim().Length > 0)
                sqlEnviados += " WHERE (DOCUMENTO.NUMERO = '" + TextBox1.Text.Trim() + "' OR CAST(DOCUMENTO.IDDOCUMENTO AS VARCHAR(20)) = '" + TextBox1.Text.Trim() + "') ";

            if (txtCliente.Text.Trim() != "")
                sqlEnviados += " and (c.razaosocialnome like '" + txtCliente.Text.Trim() + "%'  or c.FantasiaApelido like  '" + txtCliente.Text.Trim() + "%')";

            if (txtDataI.Text.Trim() != "")
            {
                DateTime? dd= null;
                try
                {
                    dd = DateTime.Parse(txtDataI.Text);
                }
                catch (Exception)
                {                  
                }

                if(dd!=null)
                    sqlEnviados += " and cast(DATADAOCORRENCIA as date)  = '"+((DateTime)dd).ToString("yyyy-MM-dd")+"'";
                    
            }

            if(cboFilial.SelectedIndex>0)
                    sqlEnviados += " and IdFilialAtual  = " + cboFilial.SelectedValue;




            sqlEnviados += " ORDER BY RETORNOCOMPROVEI.IDOCORRENCIACOMPROVEI DESC ";
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sqlEnviados.ToUpper(), cnx);
            GridView1.DataBind();
            txtProcessado.Text = " Último Processamento: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }


        public  void ExportToExcel()
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

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            pesquisar();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            pesquisar();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            //pesquisar();

        }

        protected void GridView1_Sorted(object sender, EventArgs e)
        {
            //pesquisar();

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}