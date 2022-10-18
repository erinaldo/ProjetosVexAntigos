using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ServicosWEB
{
    public partial class RelatorioFoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string c = Request.QueryString["s"];

                string[] valores = c.Split('|');

                string sql = "";


                sql += " SELECT PROCLI.IDPRODUTOCLIENTE, PROCLI.CODIGO, CASE WHEN PROCLI.CODIGODOCLIENTE IS NULL THEN	PROCLI.CODIGO   ELSE PROCLI.CODIGODOCLIENTE  ";
                sql += " END AS CODIGODOCLIENTE, PROCLI.DESCRICAO,  PROCLI.ATIVO,(Select top 1 replace(convert(varchar, cast(ValorUnitario AS NUMERIC(15,2))), '.',',') from ProdutoEmbalagem  pei where pei.IDProdutoCliente = PROCLI.IDPRODUTOCLIENTE and ValorUnitario>0 order by pei.IDProdutoEmbalagem desc ) VlUnitario,";
                sql += " GrupoDeProduto.Descricao AS Grupo, 	CTM.Nome as SubGrupo, CLIDIV.NOME, 	(CONVERT(VARCHAR(10), PROCLI.DATADECADASTRO, 103) + ' ' +	CONVERT(VARCHAR(08), PROCLI.DATADECADASTRO, 108) 	) AS DATADECADASTRO,";
                sql += " (select top 1 CONVERT(VARCHAR(10), EDM.DataHora, 103)  from EstoqueDivisaoMov EDM 	 where EDM.IDEstoqueDivisao = ESTDIV.IDEstoqueDivisao and EDM.IDEstoqueOperacao in(1,5,6) and DataHora is not null order by EDM.DataHora desc ) ULTIMAENTRADA,";
                sql += " CAST(COALESCE(ESTDIV.SALDO + ESTDIV.SALDOBASEEXTERNA, 0) - (SELECT COALESCE(SUM(DOCIT.QUANTIDADEUNIDADEESTOQUE),0) AS QUANTIDADEUNIDADEESTOQUE FROM DOCUMENTOITEM DOCIT 	INNER JOIN DOCUMENTO DOC 	ON (DOC.IDDOCUMENTO = DOCIT.IDDOCUMENTO)  INNER JOIN PRODUTOEMBALAGEM PRODE ON (PRODE.IDPRODUTOEMBALAGEM = DOCIT.IDPRODUTOEMBALAGEM) 	WHERE DOC.ENTRADASAIDA = 'SAIDA'    AND DOC.TIPODEDOCUMENTO='PEDIDO'    AND DOC.ATIVO = 'SIM' ";
                sql += " AND PRODE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE AND COALESCE(DOCIT.ESTOQUEPROCESSADO,'NAO') = 'NAO'  AND DOCIT.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO) AS NUMERIC(10,0)) AS SALDO,";

                sql += " (SELECT TOP 1 P.CODIGODEBARRAS FROM PRODUTOEMBALAGEM PE INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO WHERE IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE) CODIGODEBARRAS,'' Numero_Pedido,	(SELECT CNPJCPF FROM CADASTRO CCC WHERE CCC.IDCADASTRO=PROCLI.IDCLIENTE) CNPJCLIENTE,	ROW_NUMBER() OVER (ORDER BY PROCLI.DESCRICAO ASC) AS NUMERADOR";
                sql += " FROM ESTOQUEDIVISAO ESTDIV 	LEFT JOIN ESTOQUE EST    ON(EST.IDESTOQUE = ESTDIV.IDESTOQUE)	LEFT JOIN PRODUTOCLIENTE PROCLI ON(PROCLI.IDPRODUTOCLIENTE = EST.IDPRODUTOCLIENTE)";
                sql += " LEFT JOIN CLIENTEDIVISAO CLIDIV    ON (CLIDIV.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO)	LEFT JOIN CLIENTEDIVISAO CLIDIVPAR 	ON (CLIDIVPAR.IDCLIENTEDIVISAO = CLIDIV.IDParente)    LEFT JOIN GrupoDeProduto ON PROCLI.IDGrupoDeProduto = GrupoDeProduto.IDGrupoDeProduto		LEFT JOIN ClienteTipoDeMaterial CTM on CTM.IDClienteTipoDeMaterial = Procli.IDClienteTipoDeMaterial	";
                sql += " WHERE ProCli.IdCliente = " + valores[0];
                sql += " And  COALESCE(ESTDIV.SALDO + ESTDIV.SALDOBASEEXTERNA, 0)  - (SELECT  	COALESCE(SUM(DOCIT.QUANTIDADEUNIDADEESTOQUE),0) AS QUANTIDADEUNIDADEESTOQUE FROM DOCUMENTOITEM DOCIT INNER JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO = DOCIT.IDDOCUMENTO) INNER JOIN PRODUTOEMBALAGEM PRODE ";
                sql += " ON (PRODE.IDPRODUTOEMBALAGEM = DOCIT.IDPRODUTOEMBALAGEM) 	WHERE DOC.ENTRADASAIDA = 'SAIDA'  AND DOC.TIPODEDOCUMENTO='PEDIDO'";
                sql += " AND DOC.ATIVO = 'SIM' AND PRODE.IDPRODUTOCLIENTE = PROCLI.IDPRODUTOCLIENTE AND COALESCE(DOCIT.ESTOQUEPROCESSADO,'NAO') = 'NAO'   AND DOCIT.IDCLIENTEDIVISAO = ESTDIV.IDCLIENTEDIVISAO ) > 0";


                if (valores[1] != "@b@")
                {
                    sql += " AND ESTDIV.IDCLIENTEDIVISAO IN (" + valores[1] + ")";
                }

                if (valores[2] != "@c@")
                {
                    sql += " AND PROCLI.DESCRICAO like '" + valores[2] + "%'";
                }

                if (valores[3] != "@d@")
                {
                    sql += " AND PROCLI.CODIGO like '" + valores[3] + "%'";

                }

                if (valores[4] != "@e@")
                {
                    sql += " AND PROCLI.CODIGODOCLIENTE like '" + valores[4] + "%'";

                }

                sql += "AND Est.IDFilial =15";
                sql += " group by	PROCLI.IDPRODUTOCLIENTE, PROCLI.CODIGO,	PROCLI.CODIGODOCLIENTE, PROCLI.IDCLIENTE, PROCLI.DESCRICAO,  PROCLI.ATIVO, GrupoDeProduto.Descricao, CTM.Nome,CLIDIV.NOME, PROCLI.DATADECADASTRO,ESTDIV.IDClienteDivisao,	ESTDIV.IDEstoqueDivisao,ESTDIV.Saldo,ESTDIV.SaldoBaseExterna";

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
                //dt.Columns.Add("imagem");

                string prodCli = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    prodCli += dt.Rows[i]["IDPRODUTOCLIENTE"].ToString() + ",";
                   // dt.Rows[i]["imagem"] = "http://localhost:35040/tmp/" + dt.Rows[i]["IDPRODUTOCLIENTE"].ToString() + ".jpg";
                }

                sql = "select  foto,  pc.IDProdutoCliente from ProdutoFoto pf left join ProdutoEmbalagem pei on pei.IDProduto = pf.IDProduto left join ProdutoCliente pc on pc.IDProdutoCliente = pei.IDProdutoCliente where pei.IDProdutoCliente in(" + prodCli.Substring(0, prodCli.Length - 1) + ") and Foto is not null";
                DataTable dtfoto = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                for (int i = 0; i < dtfoto.Rows.Count; i++)
                {
                    if (dtfoto.Rows[i][0].ToString() != "")
                    {
                        byte[] imagem = (byte[])dtfoto.Rows[i]["foto"];
                        MemoryStream ms = new MemoryStream(imagem);
                        Size s = new Size(100, 100);

                        System.Drawing.Image returnImage = redimensionarImagem(System.Drawing.Image.FromStream(ms), s);                     

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
            Response.AddHeader("content-disposition", "attachment;filename="+Guid.NewGuid().ToString()+".xls");
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
                            cell.Height = new Unit("110");
                            
                            
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