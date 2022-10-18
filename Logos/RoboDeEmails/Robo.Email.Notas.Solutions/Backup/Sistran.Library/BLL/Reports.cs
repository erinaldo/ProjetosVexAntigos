using System.Data;

namespace SistranBLL
{
    public class Reports
    {
        public DataTable ListaSimplesUsuario()
        {
            return new SistranDAO.Reports().ListaSimplesUsuario();
        }

    }
}
