using System.Text;
using System.Data;
using System.Web;
using System;

namespace SistranBLL
{
    public sealed class Arquivo
    {

        public DataTable Filtrar(int? idArquivoItem)
        {
            return new SistranDAO.Arquivo().Filtrar(idArquivoItem);
        }
    }
}
