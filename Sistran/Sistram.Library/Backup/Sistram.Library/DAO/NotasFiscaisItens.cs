using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class NotasFiscaisItens
    {
        public static List<SistranMODEL.NotasFiscaisItens> RetornaNotasFiscaisSaidaItens(int NotaFiscalId, string Conn)
        {
            
            StringBuilder strsql = new StringBuilder();

            strsql.Append(" SELECT  DocIte.*,   DocIte.IDDocumentoItem,");
            strsql.Append(" Cast(DocIte.Quantidade AS NUMERIC (10,0)) as Quantidade,");
            strsql.Append(" 'R$ ' + Cast(Cast(DocIte.ValorTotalDoItem AS NUMERIC (10,2)) AS VARCHAR (12)) AS ValorTotalDoItem,");
            strsql.Append(" ProCli.Codigo,");
            strsql.Append(" ProEmb.Conteudo,");
            strsql.Append(" Pro.CodigoDeBarras,");
            strsql.Append(" ProCli.Descricao,");
            strsql.Append(" UniDeMed.Decimais");
            strsql.Append(" FROM ");
            strsql.Append(" DocumentoItem DocIte WITH (NOLOCK)");
            strsql.Append(" LEFT JOIN");
            strsql.Append(" ProdutoEmbalagem ProEmb WITH (NOLOCK)");
            strsql.Append(" ON(ProEmb.IDProdutoEmbalagem = DocIte.IDProdutoEmbalagem)");
            strsql.Append(" LEFT JOIN Produto Pro WITH (NOLOCK)");
            strsql.Append(" ON (ProEmb.IDProduto = Pro.IDProduto)");
            strsql.Append(" LEFT JOIN");
            strsql.Append(" ProdutoCliente ProCli WITH (NOLOCK)");
            strsql.Append(" ON(ProCli.IDProdutoCliente = ProEmb.IDProdutoCliente)");
            strsql.Append(" LEFT JOIN UnidadeDeMedida UniDeMed WITH (NOLOCK)");
            strsql.Append(" ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida)");
            strsql.Append(" WHERE ");
            strsql.Append(" DocIte.IDDocumento = @NotaFiscalId");
            strsql.Replace("@NotaFiscalId", NotaFiscalId.ToString());

            List<SistranMODEL.NotasFiscaisItens> ILNotasFiscaisItens = new List<SistranMODEL.NotasFiscaisItens>();

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            IDbConnection cn = factory.CreateConnection();
            IDbCommand cd = factory.CreateCommand();


            //if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            //{
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            //}
            //else
            //{
            //    Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
            //    cn.ConnectionString = SistranDb.ConnectionString;
            //}

            cd.Connection = cn;
            cd.CommandText = strsql.ToString();


            cn.Open();
            IDataReader drNotasSaida = cd.ExecuteReader();   

                while (drNotasSaida.Read())
                {
                    ILNotasFiscaisItens.Add(new SistranMODEL.NotasFiscaisItens(Convert.ToInt32(drNotasSaida["IDDocumentoItem"]),
                    drNotasSaida["CodigoDeBarras"].ToString(),
                    drNotasSaida["Descricao"].ToString(),
                    Convert.ToDecimal(drNotasSaida["Quantidade"]),
                    Convert.ToDecimal(drNotasSaida["ValorTotalDoItem"]).ToString("N2")));
                }
                drNotasSaida.Close();
                cn.Close();
            return ILNotasFiscaisItens;
        }

        public static DataTable RetornarNotasFiscaisSaidaItens(int NotaFiscalId, string Conn)
        {

            StringBuilder strsql = new StringBuilder();

            strsql.Append(" SELECT  DocIte.*,   DocIte.IDDocumentoItem,");
            strsql.Append(" Cast(DocIte.Quantidade AS NUMERIC (10,0)) as Quantidade,");
            strsql.Append(" 'R$ ' + Cast(Cast(DocIte.ValorTotalDoItem AS NUMERIC (10,2)) AS VARCHAR (12)) AS ValorTotalDoItem,");
            strsql.Append(" ProCli.Codigo,");
            strsql.Append(" ProEmb.Conteudo,");
            strsql.Append(" Pro.CodigoDeBarras,");
            strsql.Append(" ProCli.Descricao,");
            strsql.Append(" UniDeMed.Decimais");
            strsql.Append(" FROM ");
            strsql.Append(" DocumentoItem DocIte WITH (NOLOCK)");
            strsql.Append(" LEFT JOIN");
            strsql.Append(" ProdutoEmbalagem ProEmb WITH (NOLOCK)");
            strsql.Append(" ON(ProEmb.IDProdutoEmbalagem = DocIte.IDProdutoEmbalagem)");
            strsql.Append(" LEFT JOIN Produto Pro WITH (NOLOCK)");
            strsql.Append(" ON (ProEmb.IDProduto = Pro.IDProduto)");
            strsql.Append(" LEFT JOIN");
            strsql.Append(" ProdutoCliente ProCli WITH (NOLOCK)");
            strsql.Append(" ON(ProCli.IDProdutoCliente = ProEmb.IDProdutoCliente)");
            strsql.Append(" LEFT JOIN UnidadeDeMedida UniDeMed WITH (NOLOCK)");
            strsql.Append(" ON (UniDeMed.IDUnidadeDeMedida = ProCli.IDUnidadeDeMedida)");
            strsql.Append(" WHERE ");
            strsql.Append(" DocIte.IDDocumento = @NotaFiscalId");
            strsql.Replace("@NotaFiscalId", NotaFiscalId.ToString());
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);          
        }
    }
}
