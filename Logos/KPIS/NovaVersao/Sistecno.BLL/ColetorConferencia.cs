using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class ColetorConferencia
    {
        public DataTable RetornarRptEnvio(string idFilial, string Data, string Status, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarRptEnvio(idFilial, Data, Status, cnx);
        }
        public DataTable RetornarByIdDocumentoTop10(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarByIdDocumentoTop10(idDocumento, cnx);
        }

        public DataTable RetornarByIdDocumentoTop(int idDocumento, int exibirQtosRegistros, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarByIdDocumentoTop(idDocumento, exibirQtosRegistros, cnx);
        }

        public void UpdateColetorConferencia(int IdColetorConferencia, string status, string codigoRetorno, string DescricaoRetorno, string idFilial, int idUsuario, string idDocumento, string cnx)
        {
            new Sistecno.DAL.ColetorConferencia().UpdateColetorConferencia(IdColetorConferencia, status, codigoRetorno, DescricaoRetorno, idFilial, idUsuario, idDocumento, cnx);
        }
        public string RetornarByIdDocumento_Quantidade(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarByIdDocumento_Quantidade(idDocumento, cnx);
        }

        public DataTable RetornarVolumesByIdDocumento(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarVolumesByIdDocumento(idDocumento, cnx);
        }

        public DataTable RetornarByIdDocumento(int idDocumento, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarByIdDocumento(idDocumento, cnx);
        }

        public DataTable Deletar(int IdColetorConferenciaItem, int IdDocumento, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().Deletar(IdColetorConferenciaItem, IdDocumento, cnx);
        }

        public DataTable DeletarVolumes(int IdColetorConferenciaVolume, int IdDocumento, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().DeletarVolumes(IdColetorConferenciaVolume, IdDocumento, cnx);

        }

        public void CriarColetorConferencia(int idDocumento, int idFilial, int idUsuario, string CodigoDeBarras, int qtd, string CodPai, string cnx)
        {
            new Sistecno.DAL.ColetorConferencia().CriarColetorConferencia(idDocumento, idFilial, idUsuario, CodigoDeBarras, qtd, CodPai, cnx);
        }

        public void UpdateColetorConferencia(int IdColetorConferencia, string status, string codigoRetorno, string DescricaoRetorno, string cnx)
        {
            new Sistecno.DAL.ColetorConferencia().UpdateColetorConferencia(IdColetorConferencia, status, codigoRetorno, DescricaoRetorno, cnx);
        }

        public void CriarItem(string CodPai, string CodigoDeBarras, int qtd, string cnx)
        {
            new Sistecno.DAL.ColetorConferencia().CriarItem(CodPai, CodigoDeBarras, qtd, cnx);
        }


        public DataTable RetornarCodigoDeBarras(string CodigoDeBarras, string cnx)
        {
            return new Sistecno.DAL.ColetorConferencia().RetornarCodigoDeBarras(CodigoDeBarras, cnx);
        }

        public void excluirAllItens(string p, string cnx)
        {
            //new Sistecno.DAL.ColetorConferencia().exclurAllItens(p, cnx);

        }
    }
}