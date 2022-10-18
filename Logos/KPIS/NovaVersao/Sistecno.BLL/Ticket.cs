using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class Ticket
    {
        /*
         * 1) ABERTURA DO CHAMADO = ABERTO
         * 2) ATRIBUIU O CHAMADO = CHAMADO ATRIBUIDO
         * 3) HOUVE INTERAÇÃO = CHAMADO EM ANDAMENTO
         * 4) FECHOU = FINALIZADO
         * */

        public DataTable Retornar(string status, int IdUsuario, string cnx)
        {
            return new Sistecno.DAL.Ticket().Retornar(IdUsuario, cnx);
        }

        public DataTable RetornarChamadoCompleto(int idEmpresa, int idTicket, int IdUsuario, bool atribuido, string cnx)
        {
            return new Sistecno.DAL.Ticket().RetornarChamadoCompleto(idEmpresa, idTicket, IdUsuario, atribuido, cnx);
        }

        public DataTable RetornarChamadosNaoAtribuidos(int IdClienteAtribuido, string cnx)
        {
            return new Sistecno.DAL.Ticket().RetornarChamadosNaoAtribuidos(IdClienteAtribuido, cnx);
        }

        public void AtribuirChamado(int idTicket, int atribuirA, string NomeAtribuidoA, int Idusuario, string cnx)
        {
            new Sistecno.DAL.Ticket().AtribuirChamado(idTicket, atribuirA, NomeAtribuidoA, Idusuario, cnx);
        }

        public int RetornarQtdChmados(int IdUsuario, string cnx)
        {
            return new Sistecno.DAL.Ticket().RetornarQtdChmados(IdUsuario, cnx);

        }

        public string AbrirChamado(
                                     int idClienteAtribuido,
                                     int? idUsuarioAtribuido,
                                     int idusuarioDono,
                                     int idUsuarioSolicitante,
            Sistecno.DAL.Ticket.UserTicket objUsuarioSolicitante,
                                     string assunto,
                                     string status, string texto, int IdTicketDivisao,
                                     List<byte[]> Arquivos,
                                     List<string> NomeArquivos,
                                     List<string> idUsuarioAcompanhamento,
                                     string cnx)
        {
            return new Sistecno.DAL.Ticket().AbrirChamado(idClienteAtribuido, idUsuarioAtribuido, idusuarioDono, idUsuarioSolicitante, objUsuarioSolicitante, assunto, status, texto, IdTicketDivisao, Arquivos, NomeArquivos, idUsuarioAcompanhamento,cnx);
        }

        public void AlterarChamado(int idTicket, int IdClienteAtribuido, int Idusuario, string Assunto, string status, string texto, int IdTicketDivisao, List<byte[]> Arquivos, List<string> NomeArquivos, bool AtribuirChamado, int? idUsuarioAAtribuir, string cnx)
        {
            new Sistecno.DAL.Ticket().AlterarChamado(idTicket, IdClienteAtribuido, Idusuario, Assunto, status, texto, IdTicketDivisao, Arquivos, NomeArquivos, AtribuirChamado, idUsuarioAAtribuir, cnx);
        }

        public void IniciarTarefa(int idTicket, int Idusuario, List<byte[]> Arquivos, List<string> NomeArquivos, string texto, string cnx)
        {
            new Sistecno.DAL.Ticket().IniciarTarefa(idTicket, Idusuario, Arquivos, NomeArquivos, texto, cnx);
        }

        public void FinalizarTarefa(int idTicket, int Idusuario, List<byte[]> Arquivos, List<string> NomeArquivos, string texto, string cnx)
        {
            new Sistecno.DAL.Ticket().FinalizarTarefa(idTicket, Idusuario, Arquivos, NomeArquivos, texto, cnx);
        }

        public void Finalizar(int idTicket, int Idusuario, List<byte[]> Arquivos, List<string> NomeArquivos, string texto, string cnx)
        {
            new Sistecno.DAL.Ticket().Finalizar(idTicket, Idusuario, Arquivos, NomeArquivos, texto, cnx);
        }

        public DataTable RetornarChamadoCompletoNaoAtribuido(int idEmpresa, string cnx)
        {
            return new Sistecno.DAL.Ticket().RetornarChamadoCompletoNaoAtribuido(idEmpresa, cnx);
        }


        public DataTable RetornarPesquisa(int idEmpresa, string idTicket, string usuario, DateTime? ini, DateTime? fim, string status, string usuarioLogodo, string cnx)
        {
            return new Sistecno.DAL.Ticket().RetornarPesquisa(idEmpresa, idTicket, usuario, ini, fim, status, usuarioLogodo, cnx);
        }

        #region TicketDivisao

        public class Divisao
        {
            public DataTable Retornar(string cnx)
            {
                return new Sistecno.DAL.Ticket.Divisao().Retornar(cnx);

            }

        }


        #endregion
    }
}