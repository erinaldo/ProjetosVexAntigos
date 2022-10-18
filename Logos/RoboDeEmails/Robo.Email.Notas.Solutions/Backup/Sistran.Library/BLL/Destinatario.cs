using System.Data;

namespace SistranBLL
{
    public class Destinatario
    {
        public DataTable ListarDestinatario()
        {
            return new SistranDAO.Destinatario().ListarDestinatario();
        }

        public DataTable ListarDestinatario(string filtro)
        {
            return new SistranDAO.Destinatario().ListarDestinatario(filtro);
        }

        public DataTable ConsultarDadosDestinatario(string idCadastro)
        {
            return new SistranDAO.Destinatario().ConsultarDadosDestinatario(idCadastro);
        }
    }
}