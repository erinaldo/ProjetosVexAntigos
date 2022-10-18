using System.Web;

namespace Sistran.Library.Fatura
{
    public static class FaturaHistorico
    {
        public static void gravarLog(string acao, string idtitulo)
        {
            HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("TITULOHISTORICO");
            string strsql = " INSERT INTO TITULOHISTORICO (IDTITULOHISTORICO, IDTITULO, HISTORICO, DATADECADASTRO, IDUSUARIO) VALUES (" + ID + ", " + idtitulo + ", 'WEB: " + acao.ToUpper() + "', getdate(), null)";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

        }
    }
}
