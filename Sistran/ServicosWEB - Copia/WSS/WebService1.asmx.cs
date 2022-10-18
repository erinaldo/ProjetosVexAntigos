using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Sistran.Library.DTO;
using System.Data;

namespace ServicosWEB.WSS
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string GravarDocumento(string Chave, string CodigoRegiao, string NomeRegiao, string ClienteEspecial, List<Volumes> Volume, List<Itens> Item)
        {
            try
            {
                new SistranBLL.ReposicaoRoge().Gravar(Chave, "0", CodigoRegiao, NomeRegiao, ClienteEspecial, Volume, Item);
                return "0^Gravado Com Sucesso";
            }
            catch (Exception ex)
            {
                return "1^Problema ao Gravar a Nota:" + Chave + " Detalhes: " + ex.Message;
            }
        }


        [WebMethod]
        public string CancelarConferencia(string Chave, string UsuarioSolicitante)
        {
            try
            {
                new SistranBLL.ReposicaoRoge().CancelarConferencia(Chave, UsuarioSolicitante);
                return "0^Cancelamento Efetuado Com Sucesso";
            }
            catch (Exception ex)
            {
                return "1^Problema ao Cancelar a Conferencia da Nota:" + Chave + " Detalhes: " + ex.Message;
            }
        }


        [WebMethod]
        public DataSet ResgatarDocumentoPeloColetor(string Chave, string IdUsuario)
        {
            return new SistranBLL.ReposicaoRoge().ResgatarDocumentoColetor(Chave, IdUsuario);
        }


        [WebMethod]
        public void GravarConferencia(List<SistranDAO.Estrutura> Dados)
        {
            try
            {
                new SistranBLL.ReposicaoRoge().gravarConferencia(Dados);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
