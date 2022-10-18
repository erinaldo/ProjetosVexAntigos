using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

namespace ServicosWEB
{
    public partial class RelatorioFotoGestaoArmazenagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string s = Request.QueryString["s"];

                string[] m = s.Split('|');

                //string s = Request.QueryString["s"];
                // string s = Request.QueryString["s"];
                //if (s == null)
                //    return;
                //Label1.Text = s.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                //Label1.Text = Label1.Text.Replace("'R$ '", "'R$ ' + ");
                //Label1.Text = Label1.Text.Replace("order by", " order by");
                //Label1.Text = Label1.Text.Replace("inner join", " inner join");
                //Label1.Text = Label1.Text.Replace("left join", " left join");
                //Label1.Text = Label1.Text.Replace("* 12.5)", " * 12.5)+");
                //Label1.Text = Label1.Text.Replace("CaseWhen", " Case When ");
                //Label1.Text = Label1.Text.Replace("When", " When ");
                //Label1.Text = Label1.Text.Replace("ANDDatediff", " AND Datediff");
                //Label1.Text = Label1.Text.Replace("ANDconvert", " AND convert");
                //Label1.Text = Label1.Text.Replace("NUMERADORFROM", " NUMERADOR FROM ");
                //Label1.Text = Label1.Text.Replace("dbo.EstoqueLEFT", " dbo.Estoque LEFT ");


                //Label1.Text = Label1.Text.Replace("IDEstoqueLEFT", " IDEstoque LEFT ");
                //Label1.Text = Label1.Text.Replace("IDEstoqueDivisaoLEFT", " IDEstoqueDivisao LEFT ");
                //Label1.Text = Label1.Text.Replace("IDProdutoClienteLEFT", " IDProdutoCliente LEFT ");
                //Label1.Text = Label1.Text.Replace("IDProdutoLEFT", " IDProduto LEFT ");
                //Label1.Text = Label1.Text.Replace("IDClienteDivisaoLEFT", " IDClienteDivisao LEFT ");
                //Label1.Text = Label1.Text.Replace("IDProdutoClienteLEFT", " IDProdutoCliente LEFT ");
                //Label1.Text = Label1.Text.Replace("IDGrupoDeProdutoLEFT", " IDGrupoDeProduto LEFT ");

                Label1.Text = "SELECT dbo.Estoque.IDProdutoCliente, dbo.ProdutoCliente.Codigo, dbo.ProdutoCliente.CodigoDoCliente, ";
                Label1.Text += " dbo.ProdutoCliente.Descricao, CLITIPMAT.Nome TipoMaterial, dbo.ProdutoCliente.Marca as Marca, dbo.GrupoDeProduto.Descricao AS Grupo, dbo.ClienteDivisao.Nome AS Divisoes, convert(integer,dbo.EstoqueDivisao.Saldo) Saldo, convert(integer,dbo.EstoqueDivisaoMov.Quantidade) QtdeUltSaida, 'R$ ' +  replace(Cast(COALESCE(dbo.ProdutoEmbalagem.ValorUnitario, 0) as numeric (10,2)),'.',',') AS ValorUnitario, 'R$ ' +  replace(Cast(COALESCE(dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo, 0) as numeric (10,2)),'.',',') AS ValorTotal, replace(cast(dbo.Produto.PesoBruto as varchar(50)),'.',',') AS PesoRealUnit, replace(cast(dbo.Produto.PesoBruto * dbo.EstoqueDivisao.Saldo as varchar(50)),'.',',') AS PesoRealTotal, replace(cast(dbo.Produto.Altura as VARCHAR(50)),'.',',') as Altura, replace(cast(dbo.Produto.Largura as varchar(50)),'.',',') as largura, replace(cast(dbo.Produto.Comprimento as varchar(50)),'.',',') Comprimento, replace(cast(dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento as varchar(50)),'.',',') AS MetCubUnit, replace(cast((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) as varchar(50)),'.',',') AS MetCubTotal, replace(cast((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864 as varchar(50)),'.',',') AS QtdPallet, replace(cast((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44) as varchar(50)),'.',',') AS MetCubFaturado, 'R$ ' +  replace(cast((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44) * 12.5 as varchar(50)),'.',',') AS ValorArmazenagem, 'R$ ' +  replace(cast((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005 as varchar(50)),'.',',') AS ValorSeguro, 'R$ ' +  replace(cast(((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005) as varchar(50)),'.',',') As VlrTotalArmaz, 'R$ ' +  replace(cast((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005)) / dbo.EstoqueDivisao.Saldo as varchar(50)),'.',',') as VlrArmzMes, 'R$ ' +  replace(cast(((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005)) / dbo.EstoqueDivisao.Saldo)/30 as varchar(50)),'.',',') as VlrArmzDia, convert(integer,(dbo.ProdutoEmbalagem.ValorUnitario / (((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento *dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005)) / dbo.EstoqueDivisao.Saldo)/30))) as QtdeDias, convert(integer,(dbo.ProdutoEmbalagem.ValorUnitario / (((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005)) / dbo.EstoqueDivisao.Saldo)/30)) / 30) as QtdeMeses, CONVERT(char(10), dbo.ProdutoCliente.DataLimiteDeUso, 103) AS DataLimiteDeUso, Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) as QtdeDiasVctoLimite,  Case  When   Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) >= 31 Then 'PRODUTO DENTRO DO PRAZO' ELSE Case  When Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) >= 0  AND Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) <= 30 Then'PRODUTO PROX AO PRAZO' ELSE Case  When Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) IS null Then'PRODUTO SEM DATA VALIDADE' ELSE 'PRODUTO VENCIDO' END END END AS SITVCTO, Case  When  Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) >= 31 Then 'VERDE' ELSE Case  When Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) >= 0  AND Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) <= 30 Then'AMARELO' ELSE Case  When Datediff(Day,GetDate(),dbo.ProdutoCliente.DataLimiteDeUso) IS null Then'BRANCO' ELSE 'VERMELHO' END END END AS CORSITVCTO, CONVERT(char(10), dbo.EstoqueDivisaoMov.DataHora, 103) AS DataUltMovto_old, Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) as QtdeDiasVctoSMov_old, Datediff(Day, ( select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc) ,GetDate()) as QtdeDiasVctoSMov, ( select top 1 CONVERT(char(10), em.DataHora, 103) from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc) DataUltMovto, Case  When   Datediff(Day,( select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc),GetDate()) >= 90 Then 'MAIOR OU IGUAL A 90 DIAS PARADO' ELSE Case  When Datediff(Day,(select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA') order by em.dataHora desc),GetDate()) > 0  AND Datediff(Day,(select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc),GetDate()) < 31 Then 'ATE 30 DIAS EM ESTOQUE' ELSE Case  When  Datediff(Day,(select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA') order by em.dataHora desc),GetDate()) > 30  AND Datediff(Day,(select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA') order by em.dataHora desc),GetDate()) < 90 Then 'ENTRE 31 A 89 DIAS PARADO' ELSE 'PRODUTO COM MOVIMENTACAO' END END END AS SITMOVTO, Case  When   Datediff(Day,( select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc),GetDate()) >= 90 Then 'VERMELHO' ELSE Case  When Datediff(Day,( select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc),GetDate()) > 0 AND Datediff(Day,( select top 1 em.DataHora from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao  inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc),GetDate()) BETWEEN 31 AND 89 Then 'AMARELO' ELSE 'VERDE' END END AS CORSITMOVTO , 'R$ ' +  replace(Cast(Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) * (((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005)) / dbo.EstoqueDivisao.Saldo)/30) as varchar(50)),'.',',') As VlrDiaParado, convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) * (((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) * 0.0005)) / dbo.EstoqueDivisao.Saldo)/30) / dbo.ProdutoEmbalagem.ValorUnitario) * 100)) As PercAvaliacao, Case  When  convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) * (((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento * dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) *0.0005)) / dbo.EstoqueDivisao.Saldo)/30) /dbo.ProdutoEmbalagem.ValorUnitario) * 100)) >= 100 Then 'PREJUIZO' Else  Case  When  convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) *(((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento *dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) *0.0005)) / dbo.EstoqueDivisao.Saldo)/30) /dbo.ProdutoEmbalagem.ValorUnitario) * 100)) > 70  AND convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) *(((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento *dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) *0.0005)) / dbo.EstoqueDivisao.Saldo)/30) /dbo.ProdutoEmbalagem.ValorUnitario) * 100)) <= 99 Then 'ESTADO DE ATENO'ELSE 'LUCRO' END END AS SITFINANCEIRA, Case  When  convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) *(((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento *dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) *0.0005)) / dbo.EstoqueDivisao.Saldo)/30) /dbo.ProdutoEmbalagem.ValorUnitario) * 100)) >= 100 Then 'VERMELHO' Else  Case  When  convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) *(((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento *dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) *0.0005)) / dbo.EstoqueDivisao.Saldo)/30) /dbo.ProdutoEmbalagem.ValorUnitario) * 100)) > 70  AND convert(integer,((Datediff(Day,dbo.EstoqueDivisaoMov.DataHora,GetDate()) *(((((((dbo.Produto.Altura * dbo.Produto.Largura * dbo.Produto.Comprimento *dbo.EstoqueDivisao.Saldo) / 0.864) * 1.44)  * 12.5)+ ((dbo.ProdutoEmbalagem.ValorUnitario * dbo.EstoqueDivisao.Saldo) *0.0005)) / dbo.EstoqueDivisao.Saldo)/30) /dbo.ProdutoEmbalagem.ValorUnitario) * 100)) <= 99 Then 'AMARELO' ELSE'VERDE' END END AS CORSITFINANCEIRA,(SELECT CNPJCPF FROM CADASTRO CCC WHERE CCC.IDCADASTRO=DBO.PRODUTOCLIENTE.IDCLIENTE) CNPJCLIENTE, ROW_NUMBER() OVER (ORDER BY ProdutoCliente.Descricao asc) AS  NUMERADOR FROM   dbo.Estoque LEFT  JOIN dbo.EstoqueDivisao ON dbo.Estoque.IDEstoque = dbo.EstoqueDivisao. IDEstoque LEFT  JOIN dbo.EstoqueDivisaoMov ON dbo.EstoqueDivisao.IDEstoqueDivisao = dbo.EstoqueDivisaoMov. IDEstoqueDivisao LEFT  JOIN dbo.ProdutoCliente ON dbo.Estoque.IDProdutoCliente = dbo.ProdutoCliente. IDProdutoCliente LEFT  JOIN dbo.ProdutoEmbalagem ON dbo.ProdutoCliente.IDProdutoCliente = dbo.ProdutoEmbalagem. IDProdutoCliente LEFT  JOIN dbo.Produto ON dbo.ProdutoEmbalagem.IDProduto = dbo.Produto. IDProduto LEFT  JOIN dbo.ClienteDivisao ON dbo.EstoqueDivisao.IDClienteDivisao = dbo.ClienteDivisao. IDClienteDivisao LEFT  JOIN dbo.GrupoDeProduto ON dbo.ProdutoCliente.IDGrupoDeProduto = dbo.GrupoDeProduto. IDGrupoDeProduto LEFT  JOIN ClienteTipoDeMaterial CLITIPMAT on CLITIPMAT.IDClienteTipoDeMaterial = ProdutoCliente.IDClienteTipoDeMaterial WHERE (dbo.estoquedivisaoMov.IDEstoqueDivisaoMov in ( select top 1 edm.IDESTOQUEDIVISAOMOV from estoquedivisaomov edm  inner join estoqueDivisao ed on ed.idestoquedivisao= edm.idestoquedivisao inner join estoquemov em on em.idestoque = ed.idestoque where ed.idestoqueDivisao=dbo.EstoqueDivisao.IDEstoqueDivisao and (em.historico='SAIDA' or em.historico='ENTRADA')  order by em.dataHora desc ) ) ";
                Label1.Text += " AND (dbo.Estoque.IDFilial = " + m[1] + ")AND (dbo.ProdutoCliente.IDCliente =" + m[2] + ") AND (dbo.EstoqueDivisao.Saldo >0) ";
                Label1.Text += " AND (dbo.ClienteDivisao.Ativo='SIM') and dbo.ProdutoEmbalagem.ValorUnitario>0 AND (dbo.ClienteDivisao.IDClienteDivisao in(" + m[0] + "))";

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(Label1.Text, cnx);

                string prodCli = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    prodCli += dt.Rows[i]["IDPRODUTOCLIENTE"].ToString() + ",";
                    // dt.Rows[i]["imagem"] = "http://localhost:35040/tmp/" + dt.Rows[i]["IDPRODUTOCLIENTE"].ToString() + ".jpg";
                }

                string sql = "select  foto,  pc.IDProdutoCliente from ProdutoFoto pf left join ProdutoEmbalagem pei on pei.IDProduto = pf.IDProduto left join ProdutoCliente pc on pc.IDProdutoCliente = pei.IDProdutoCliente where pei.IDProdutoCliente in(" + prodCli.Substring(0, prodCli.Length - 1) + ") and Foto is not null";
                DataTable dtfoto = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                for (int i = 0; i < dtfoto.Rows.Count; i++)
                {
                    if (dtfoto.Rows[i][0].ToString() != "")
                    {
                        byte[] imagem = (byte[])dtfoto.Rows[i]["foto"];
                        MemoryStream ms = new MemoryStream(imagem);
                        Size ss = new Size(100, 100);

                        System.Drawing.Image returnImage = redimensionarImagem(System.Drawing.Image.FromStream(ms), ss);

                        try
                        {
                            returnImage.Save(@"\\192.168.10.8\tmp\" + dtfoto.Rows[i]["IDProdutoCliente"].ToString() + ".jpg");
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }


                GridView1.DataSource = dt;
                GridView1.DataBind();
                ExportToExcel();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Web.UI.WebControls.Image Image1 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");


            if (Image1 != null)
            {
                Image1.ImageUrl = "http://www1.logoslogistica.com.br/sistranweb/tmp/" + Image1.ImageUrl + ".jpg";
                Image1.Height = new Unit("100");
                Image1.Width = new Unit("100");
            }
        }


        public static System.Drawing.Image redimensionarImagem(System.Drawing.Image imagem, Size tamanho)
        {
            int larguraOrigem = imagem.Width;
            int alturaOrigem = imagem.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //nPercentW = ((float)tamanho.Width / (float)larguraOrigem);
            //nPercentH = ((float)tamanho.Height / (float)alturaOrigem);

            //if (nPercentH < nPercentW)
            //    nPercent = nPercentH;
            //else
            //    nPercent = nPercentW;

            int larguraDestino = (int)(90);
            int alturaDestino = (int)(90);

            Bitmap b = new Bitmap(larguraDestino, alturaDestino);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imagem, 0, 0, larguraDestino, alturaDestino);
            g.Dispose();

            return (System.Drawing.Image)b;
        }

        protected void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Guid.NewGuid().ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;


                GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    int cel = 0;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (cel == 0)
                        {
                            cell.Width = new Unit("110");
                            cell.Height = new Unit("100");


                        }
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                        cel++;
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode {text-align:center;  vertical-align: middle} </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}