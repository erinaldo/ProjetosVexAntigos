using System.Data;

namespace SistranBLL
{
    public class Filial
    {
        public DataTable ListarFiliais(string Conn)
        {
            return new SistranDAO.Filial().ListarFiliais(Conn);
        }

        public DataTable ListarSelecionadosByIDMotorista(string Conn, int idMotorista)
        {
            return new SistranDAO.Filial().ListarSelecionadosByIDMotorista(Conn, idMotorista);
        }

        public DataTable ListarDisponiveisByIDMotorista(string Conn, int idMotorista, bool captacao)
        {
            return new SistranDAO.Filial().ListarDisponiveisByIDMotorista(Conn, idMotorista , captacao);
        }

        public DataTable ListarFilialPadraoInternet(string clientes)
        {
            return new SistranDAO.Filial().ListarFilialPadraoInternet( clientes);
        }

        public System.Web.UI.WebControls.DropDownList CarregarCboFilialPadraoInternet(System.Web.UI.WebControls.DropDownList cbo, string clientes)
        {
            cbo.DataTextField = "NOME";
            cbo.DataValueField = "IDFILIAL";

            DataTable dtcbo = new SistranDAO.Filial().ListarFilialPadraoInternet( clientes);
            cbo.DataSource = dtcbo;
            cbo.DataBind();

            if (dtcbo.Rows.Count > 1)
            {
                cbo.Items.Insert(0,new System.Web.UI.WebControls.ListItem("Selecione",""));
            }

            return cbo;
        }
    }
}
