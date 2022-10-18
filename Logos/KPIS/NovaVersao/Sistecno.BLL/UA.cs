using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.BLL
{
    public class UA
    {

        public DataTable RetornarUa(string IdUa, string CodigodeBarras, string IdFilial, string cnx)
        {
            return new Sistecno.DAL.UA().RetornarUa(IdUa, CodigodeBarras, IdFilial, cnx);
        }

        public DataTable RetornarUaBYid(string IdUa, string CodigoDeBarras, string cnx)
        {
            return new Sistecno.DAL.UA().RetornarUaBYid(IdUa, CodigoDeBarras, cnx);
        }

        public DataTable Retornar(string cb, string cnx)
        {
            return new Sistecno.DAL.UA().Retornar(cb, cnx);
        }

        public int Gravar(int idFilial, int IdProdutoCliente, int quantidade, DateTime? Validade, string Lote, string cnx)
        {
            return new Sistecno.DAL.UA().Gravar(idFilial, IdProdutoCliente, quantidade, Validade, Lote, cnx);
        }

        public DataTable RetornarEtiquetaUA(string IdFilial, string idUa, string CodBarras, string cnx)
        {
            return new Sistecno.DAL.UA().RetornarEtiquetaUA(IdFilial, idUa, CodBarras, cnx);
        }
    }
}
