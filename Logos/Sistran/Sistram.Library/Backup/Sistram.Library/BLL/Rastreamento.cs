using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranBLL
{
    public sealed class Rastreamento
    {

        public static DataTable Rastrear(string NumeroNotas, string CNPJ, string Conn)
        {
            return SistranDAO.Rastreamento.Rastrear(NumeroNotas, CNPJ, Conn);
        }

        public static int Contar(string NumeroNotas, string CNPJ, string Conn)
        {
            return SistranDAO.Rastreamento.Contar(NumeroNotas, CNPJ, Conn);
        }

        public static DataTable RetornarTracking(string IdDocumento, string Conn)
        {
            return SistranDAO.Rastreamento.RetornarTracking(IdDocumento,  Conn);
        }
    }
}