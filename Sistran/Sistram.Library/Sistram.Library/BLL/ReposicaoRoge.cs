using System.Text;
using System.Data;
using System.Web;
using System;
using System.Collections.Generic;
using Sistran.Library.DTO;
using SistranDAO;

namespace SistranBLL
{


    public sealed class ReposicaoRoge
    {
        public void gravarConferencia(List<Estrutura> dt)
        {
            new SistranDAO.ReposicaoRoge().gravarConferencia(dt);
        }

        public void Gravar(string Chave, string IdNota, string CodigoRegiao, string NomeRegiao, string ClienteEspecial, List<Volumes> Volume, List<Itens> Item)
        {
            new SistranDAO.ReposicaoRoge().Gravar( Chave,  IdNota,  CodigoRegiao,  NomeRegiao,  ClienteEspecial,  Volume,  Item);
        }

        public void CancelarConferencia(string chave, string usuario)
        {
            new SistranDAO.ReposicaoRoge().CancelarConferencia(chave, usuario);
        }

        public DataSet ResgatarDocumentoColetor(string chave, string idUsuario)
        {
            return new SistranDAO.ReposicaoRoge().ResgatarDocumentoColetor(chave, idUsuario);
        }
    
    }
}
