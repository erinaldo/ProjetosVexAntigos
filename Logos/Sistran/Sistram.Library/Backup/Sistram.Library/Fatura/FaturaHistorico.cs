using System.Web;

namespace Sistran.Library.Fatura
{
    public static class FaturaHistorico
    {
        public static void gravarLog(string acao, string idtitulo, string cnx)
        {
            HttpContext.Current.Session["ConnLogin"] = cnx;
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("TITULOHISTORICO", cnx);
            string strsql = " INSERT INTO TITULOHISTORICO (IDTITULOHISTORICO, IDTITULO, HISTORICO, DATADECADASTRO, IDUSUARIO) VALUES (" + ID + ", " + idtitulo + ", 'WEB: " + acao.ToUpper() + "', getdate(), null)";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, cnx);
        }
    }
}