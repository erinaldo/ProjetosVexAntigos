using System.Text;
using System.Data;
using System.Web;
using System;
using Sistran.Library;

namespace SistranDAO
{
    public sealed class Arquivo
    {

        public DataTable Filtrar(int? idArquivoItem)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT AI.IDARQUIVOITEM, NOMEDOARQUIVO, DATA, A.NOME TIPO, U.NOME USUARIO, ConteudoArquivo ");
            strsql.Append(" FROM ARQUIVOITEM AI  ");
            strsql.Append(" INNER JOIN ARQUIVO A ON A.IDARQUIVO = AI.IDARQUIVO ");
            strsql.Append(" INNER JOIN USUARIO U ON U.IDUSUARIO = A.IDUSUARIO WHERE 0=0 ");
            strsql.Append((idArquivoItem == null ? "" : " AND AI.IDARQUIVOITEM=" + idArquivoItem.ToString()));
            strsql.Append(" ORDER BY A.NOME");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public void insertTeste()
        {
            HttpContext.Current.Session["ConnLogin"] = BaseAntigaConfig.Default.BDNovoLogos;

            for (int i = 0; i < 5000; i++)
            {
                int m = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("AB"));

                Sistran.Library.GetDataTables.ExecutarComandoSql("INSERT INTO AB VALUES ("+ m.ToString() +", 1, '1234')", BaseAntigaConfig.Default.BDNovoLogos);
            }



        }
    }
}