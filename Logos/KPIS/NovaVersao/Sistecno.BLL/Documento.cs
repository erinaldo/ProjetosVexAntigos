using System.Collections.Generic;
using System.Data;
using System.Xml;
namespace Sistecno.BLL
{
    public class Documento
    {

        public void ImportarEDidponibilizarCTE(string caminho, string cnx)
        {
        }

        public DataTable RetornarConferencia(bool chave, string numero, int? IdCliente, int? idfilial, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarConferencia(chave,numero, IdCliente, idfilial, cnx);
        }

        public DataTable RetornarConferenciaChave(string chave, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarConferenciaChave(chave, cnx);

        }

        public Sistecno.DAL.Models.Documento RetornarVolumes(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarVolumes(idDocumento, cnx);
        }

         public DataTable RetornarConferencia(int iddocumento, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarConferencia(iddocumento, cnx);
        }

        public DataTable RetornarNfsConhecimento(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarNfsConhecimento(idDocumento, cnx);
        }

        public DataTable RetornarDetalheConhecimento(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarDetalheConhecimento(idDocumento, cnx);
        }

        public XmlDocument retornarXMLPorChave(string chave, string cnx)
        {
            return new Sistecno.DAL.Documento().retornarXMLPorChave(chave, cnx);
        }

        public DataTable RetornarCteImpressao(int idfilial, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarCteImpressao(idfilial, cnx);
        }

        public DataTable RetornarCteImpressaoDetalhe(int idCliente, int idFilial, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarCteImpressaoDetalhe(idCliente, idFilial, cnx);
        }

        public void MarcarImpressoByChave(string chave, string cnx)
        {
            new Sistecno.DAL.Documento().MarcarImpressoByChave(chave, cnx);
        }

        public Sistecno.DAL.Models.Documento Retornar(Sistecno.DAL.Models.Documento doc, string cnx)
        {
            return new Sistecno.DAL.Documento().Retornar(doc, cnx);
        }

        public int Gravar(Sistecno.DAL.Models.Documento documento, List<int> IdsDocumentos, string cnx)
        {
            return new Sistecno.DAL.Documento().Gravar(documento,  IdsDocumentos, cnx);
        }

        public Sistecno.DAL.Models.Documento RetornarByIdDocumento(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarByIdDocumento(idDocumento, cnx);
        }
        public DataTable RetornarCtesEmLote(int idfilial, bool resultadoDeEnvio, int idLoteEletronico, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarCtesEmLote(idfilial, resultadoDeEnvio, idLoteEletronico, cnx);
        }

        public int GravarDocumentoImportacaoXml(Sistecno.DAL.Models.Documento doc, string cnx)
        {
            return new Sistecno.DAL.Documento().GravarDocumentoImportacaoXml(doc, cnx);
        }

        public DataTable RetornarCtesEmLoteDetalhe(int idLoteEletronico, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarCtesEmLoteDetalhe(idLoteEletronico, cnx);
        }

        public DataTable RetornarNotasDisponivesCte(int idfilial, string agrupamento , string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarNotasDisponivesCte(idfilial, agrupamento, cnx);
        }

        public DataTable RetornarNotasDisponivesCteDetalhe(int idfilial, string agrupamento, int id, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarNotasDisponivesCteDetalhe(idfilial, agrupamento,id, cnx);
        }
        public int Gravar(Sistecno.DAL.Models.Documento doc, string cnx)
        {
            return new Sistecno.DAL.Documento().Gravar(doc, cnx);
        }


        public DataTable RetornarDocumentosIn(List<string> idDocumentos, bool abreviarNomes, string cnx)
        {
            string docs = "";
            for (int i = 0; i < idDocumentos.Count; i++)
            {
                docs += idDocumentos[i] + ",";             
            }

            if (docs == "")
                docs = "0,";


            return new Sistecno.DAL.Documento().RetornarDocumentosIn(docs.Remove(docs.Length-1), abreviarNomes, cnx);
        }

        public DataTable RetornarPesquisa(string Numero,  string situacao, string tipoDeDocumento, int idfilial, string cStatus, bool abreviarNomes, string cnx)
         {
            return new Sistecno.DAL.Documento().RetornarPesquisa(Numero,  situacao, tipoDeDocumento, idfilial,  cStatus,abreviarNomes, cnx);
        }

        public DataTable RetornarPesquisaTelaDocumentos(string Numero, int idCliente, string situacao, int idfilial, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarPesquisaTelaDocumentos(Numero, idCliente, situacao, idfilial, cnx);

        }

        public DataTable RetornarCteDisponivel(int idfilial, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarCteDisponivel(idfilial, cnx);
        }

        public DataTable RetornarCteDisponivelDetalhe(int idfilial, int idcliente, string cnx)
        {
            return new Sistecno.DAL.Documento().RetornarCteDisponivelDetalhe(idfilial, idcliente, cnx);
        }

        //public void CriarLote(int idFilial, string Remetente, List<int> idDocumentos, string cnx)
        //{
        //    new Sistecno.DAL.Documento().CriarLote(idFilial, Remetente, idDocumentos, cnx);
        //}

        public DataTable RetornarCtesParaEnvio(int idfilial, int idcliente, List<int> idDocumentos,string cnx)
        {
          return  new Sistecno.DAL.Documento().RetornarCtesParaEnvio(idfilial, idcliente, idDocumentos,  cnx);
        }

        public class EletronicoParametro
        {
            public DataTable Retornar(int idFilial, string TipoEletronico, string cnx)
            {
                return new Sistecno.DAL.Documento.EletronicoParametro().Retornar(idFilial, TipoEletronico, cnx);
            }
        }


        public class Frete
        {
            public Sistecno.DAL.Models.DocumentoFrete Retornar(int iddocumento, string cnx)
            {
                return new Sistecno.DAL.Documento.Frete().Retornar(iddocumento, cnx);
            }
        }

        public static class Numerador
        {
            public static string RetornarNumerador(int idempresa, int idfilial, string ChaveNome, string serie, string cnx)
            {
                return Sistecno.DAL.Documento.Numerador.RetornarNumerador(idempresa, idfilial, ChaveNome, serie, cnx);
            }
        }
    }
}
