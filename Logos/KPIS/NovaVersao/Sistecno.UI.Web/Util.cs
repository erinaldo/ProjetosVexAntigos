using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Util
{
    public static class TrataImagem
    {
        public static string resizeImageAndSave(string imagePath, int largura, int altura, string prefixo)
        {
            System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(imagePath);
            var thumbnailImg = new Bitmap(largura, altura);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, largura, altura);
            thumbGraph.DrawImage(fullSizeImg, imageRectangle);
            string targetPath = imagePath.Replace(Path.GetFileNameWithoutExtension(imagePath), Path.GetFileNameWithoutExtension(imagePath) + prefixo);
            thumbnailImg.Save(targetPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbnailImg.Dispose();
            return targetPath;
        }
    }
    public class PropriedadesGrid
    {
        public string TextoColuna { get; set; }
        public string Tipo { get; set; }
    }

    public static class GradeDeDados
    {

        public static string AlinhamentoColuna(string tipo)
        {

            string alinhamento = "left";

            switch (tipo)
            {
                case "INT":
                    alinhamento = "right";
                    break;

                case "INT32":
                    alinhamento = "right";
                    break;


                case "INT64":
                    alinhamento = "right";
                    break;


                case "DECIMAL":
                    alinhamento = "right";
                    break;

                case "FLOAT":
                    alinhamento = "right";
                    break;

                case "DATETIME":
                    alinhamento = "center";
                    break;

            }

            return alinhamento;
        }


        /// <summary>
        /// Criar Grid
        /// </summary>
        /// <param name="dados">Data Table com os dados para popular o grid. Obrigatoriamente a primeira coluna ser o Id  da tabela</param>
        /// <param name="LinkEdicao"></param>
        /// <returns></returns>
        public static PlaceHolder CriarGrid(DataTable dados)
        {
            List<PropriedadesGrid> Colunas = new List<PropriedadesGrid>();

            for (int i = 0; i < dados.Columns.Count; i++)
            {
                PropriedadesGrid p = new PropriedadesGrid();
                p.TextoColuna = dados.Columns[i].ColumnName.ToUpper();
                p.Tipo = dados.Columns[i].DataType.Name.ToString().ToUpper();
                Colunas.Add(p);
            }


            PlaceHolder htm = new PlaceHolder();            


            htm.Controls.Add(new LiteralControl("<div>"));
            htm.Controls.Add(new LiteralControl("<div class='jarviswidget-editbox'>"));
            htm.Controls.Add(new LiteralControl("</div>"));
            htm.Controls.Add(new LiteralControl("<div class='widget-body no-padding'>"));

            htm.Controls.Add(new LiteralControl("<table id='datatable_tabletools' class='table table-striped table-bordered table-hover' width='100%'>"));
            htm.Controls.Add(new LiteralControl("<thead>"));
            htm.Controls.Add(new LiteralControl("<tr>"));

            for (int ii = 0; ii < Colunas.Count; ii++)
            {
                string alinhamento = AlinhamentoColuna(Colunas[ii].Tipo.ToUpper());
                htm.Controls.Add(new LiteralControl("<th style='text-align:" + alinhamento + "'>" + Colunas[ii].TextoColuna + "</th>"));
            }

            //if(PK_tabela!="")
                htm.Controls.Add(new LiteralControl("<th></th>"));

            htm.Controls.Add(new LiteralControl("</tr>"));
            htm.Controls.Add(new LiteralControl("</thead>"));            
            htm.Controls.Add(new LiteralControl("<tbody>"));

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                htm.Controls.Add(new LiteralControl("<tr>"));

                string alinhamento = "left";

                for (int c = 0; c < dados.Columns.Count; c++)
                {
                    alinhamento = AlinhamentoColuna(dados.Columns[c].DataType.Name.ToString().ToUpper());
                    htm.Controls.Add(new LiteralControl("<td style='text-align: " + alinhamento + "'>" + dados.Rows[i][c].ToString() + "</td>"));

                }

               // if (PK_tabela != "")

                ImageButton botao = new ImageButton();
                botao.ImageUrl = "";
                botao.ID = dados.Rows[i][0].ToString();
                botao.Attributes.Add("onClick", "MostrarEsconder('" + dados.Rows[i][0].ToString() + "');");
                htm.Controls.Add(new LiteralControl("<td style='text-align: right'>"));

               // htm.Controls.Add(botao);
                htm.Controls.Add(new LiteralControl("</td>"));

                
                //htm.Controls.Add(new LiteralControl("<td style='text-align: right'><a ID='l'"+  dados.Rows[i][0].ToString()  + " runat='server' AutoPostBack='True'  href='#' onclick=MostrarEsconder('" + dados.Rows[i][0].ToString() + "');><i class='fa fa-check text-navy'></i></a></td>";
                //htm.Controls.Add(new LiteralControl("<td style='text-align: right'><a href='" + LinkEdicao + "?acao=editar&id=" + dados.Rows[i][0].ToString() + "'><i class='fa fa-check text-navy'></i></a></td>";
    
                htm.Controls.Add(new LiteralControl("</tr>"));
            }

            htm.Controls.Add(new LiteralControl("</tbody>"));
            htm.Controls.Add(new LiteralControl("</table>"));
            return htm;
        }

       public static string CriarGrid(DataTable dados, string LinkEdicao, string TituloTela)
        {
            List<PropriedadesGrid> Colunas = new List<PropriedadesGrid>();

            for (int i = 0; i < dados.Columns.Count; i++)
            {
                PropriedadesGrid p = new PropriedadesGrid();
                
                if(dados.Columns[i].ColumnName.ToUpper()== "CODIGO")
                                    p.TextoColuna = "ID";
                else
                    p.TextoColuna = dados.Columns[i].ColumnName.ToUpper();


                p.Tipo = dados.Columns[i].DataType.Name.ToString().ToUpper();
                Colunas.Add(p);
            }


            string htm = "";


            htm += "<div>";
            htm += "<div class='jarviswidget-editbox'>";
            htm += "</div>";
            htm += "<div class='widget-body no-padding'>";

            htm += "<table id='datatable_tabletools' class='table table-striped table-bordered table-hover' width='100%'>";
            htm += "<thead>";
            htm += "<tr>";

            for (int ii = 0; ii < Colunas.Count; ii++)
            {
                string alinhamento = AlinhamentoColuna(Colunas[ii].Tipo.ToUpper());
                htm += "<th style='text-align:" + alinhamento + "'>" + Colunas[ii].TextoColuna + "</th>";
            }

            //if(LinkEdicao!="")
            //    htm += "<th></th>";

            htm += "</tr>";
            htm += "</thead>";
            htm += "<tbody>";

            for (int i = 0; i < dados.Rows.Count; i++)
            {
                htm += "<tr>";

                string alinhamento = "left";

                for (int c = 0; c < dados.Columns.Count; c++)
                {

                    if (LinkEdicao != "" && c==0)
                    {
                        alinhamento = AlinhamentoColuna(dados.Columns[c].DataType.Name.ToString().ToUpper());
                        //htm += "<td style='text-align: " + alinhamento + "'>" + dados.Rows[i][c].ToString() + "</td>";
                        htm += "<td style='text-align: right; text-decoration:underline; color:#333'><a href='" + LinkEdicao + "?acao=editar&id=" + dados.Rows[i][0].ToString() + "&opc=" + TituloTela + "'><b>" + dados.Rows[i][c].ToString() + "</b></a></td>";
                    }
                    else
                    {
                        alinhamento = AlinhamentoColuna(dados.Columns[c].DataType.Name.ToString().ToUpper());
                        htm += "<td style='text-align: " + alinhamento + "'>" + dados.Rows[i][c].ToString() + "</td>";
                    }

                }
                //if (LinkEdicao != "")
                //    htm += "<td style='text-align: right'><a href='" + LinkEdicao + "?acao=editar&id=" + dados.Rows[i][0].ToString() + "&opc="+TituloTela+"'><i class='fa fa-edit'></i></a></td>";
    
                htm += "</tr>";
            }

            htm += "</tbody>";
            htm += "</table>";
            return htm;
        }

   
    }
    


    public static class Combo
    {
        public static DropDownList CarregarCombo(DataTable fonteDeDados, ref DropDownList dp, Boolean PostaPagina, Boolean InserirSelecione, string CampoValue, string CampoText)
        {
            dp.Items.Clear();
            dp.DataSource = null;
            dp.SelectedIndex = -1;
            dp.DataSource = fonteDeDados;
            dp.DataTextField = CampoText;
            dp.DataValueField = CampoValue;
            dp.DataBind();
            //dp.CssClass = "cbo";


            if (PostaPagina)
                dp.AutoPostBack = true;

            if (InserirSelecione)
                dp.Items.Insert(0, new ListItem("SELECIONE", ""));




            return dp;
        }


        public static string[] devolve(string texto)
        {
            return texto.Split(';');
        }

    }
}