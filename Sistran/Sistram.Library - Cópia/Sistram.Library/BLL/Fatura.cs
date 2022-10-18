using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SistranBLL
{
    public class Fatura
    {
        public DataTable Listar(string IDTITULODUPLICATA)
        {
            return new SistranDAO.Fatura().Listar(IDTITULODUPLICATA);
        }

        public DataTable ListarbyIdTitulo(string IdTitulo)
        {
            return new SistranDAO.Fatura().ListarbyIdTitulo(IdTitulo);
        }
    }
}
