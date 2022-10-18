using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Girotrade
{
    public partial class PedidoDetalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPedidoEnviado();
            }
        }

        string cnx = "";

        private void CarregarPedidoEnviado()
        {
            try
            {
                string sql = "select p.Numero, P.DataDeEntrada, pc.Codigo, pc.Descricao, cast(di.Quantidade as int) Quantidade , * " +
                 " from DOCUMENTOPEDIDO p" +
                 " Inner join DOCUMENTOPEDIDOITEM di on di.IdDocumento = p.IDDocumento" +
                 " Inner join Cadastro dest on dest.IDCadastro = p.IDDestinatario" +
                " Inner join ProdutoCliente pc on pc.IDProdutoCliente = di.IdProdutoCliente" +
                 " where p.Ativo = 'SIM' and DataDeEntrada >= GETDATE() - 90" +
                 " and DocumentodoCliente4 is not null" +
                 " and p.Numero = "+ Request.QueryString["i"] +
                 " Order by pc.Codigo";


                cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                GridView1.DataSource = dt;
                GridView1.DataBind();


                Label1.Text = "N. Pedido: " + dt.Rows[0]["Numero"].ToString() + "<br>Destinatário: " + dt.Rows[0]["RazaoSocialnome"].ToString();

            }
            catch (Exception)
            {

            }
        }
    }
}