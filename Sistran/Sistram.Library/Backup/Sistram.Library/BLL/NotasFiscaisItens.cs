using System;
using System.Collections.Generic;
using System.Data;

namespace SistranBLL
{
    public sealed class NotasFiscaisItens
    {
        public static List<SistranMODEL.NotasFiscaisItens> RetornaNotasFiscaisSaidaItens(int NotaFiscalId, string Conn)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(NotaFiscalId)))
                {
                    throw new Exception("Nota fiscal inválida. Por favor entre em contato com o adminstrador do sistema");
                    //erro -  retornar mensagem para preencher usuario e senha
                }
                else
                {
                    //faz a busca do nome do banco no xml - 
                    List<SistranMODEL.NotasFiscaisItens> LnotasFiscaisItens = SistranDAO.NotasFiscaisItens.RetornaNotasFiscaisSaidaItens(NotaFiscalId, Conn);


                    return LnotasFiscaisItens;

                }

            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }

        public static DataTable RetornarNotasFiscaisSaidaItens(int NotaFiscalId, string Conn)
        {
            return SistranDAO.NotasFiscaisItens.RetornarNotasFiscaisSaidaItens(NotaFiscalId, Conn);            
        }
    }
}