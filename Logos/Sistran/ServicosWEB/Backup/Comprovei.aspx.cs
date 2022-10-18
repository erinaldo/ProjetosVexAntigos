using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;

namespace ServicosWEB
{
    public partial class Comprovei : System.Web.UI.Page
    {

        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            pesquisar();
        }

        private void pesquisar()
        {
            string sqlEnviados = "";
            sqlEnviados += " SELECT TOP 50 ";
            sqlEnviados += " D.IDDOCUMENTO, D.NUMERO, D.SERIE, D.NOMEDOARQUIVO1 [ARQUIVO ENVIADO], EDI.NOMEDOARQUIVO RESULTADOCOMPROVEI, ";
            sqlEnviados += " D.DOCUMENTODOCLIENTE4 CHAVE, ISNULL(CLI.FANTASIAAPELIDO, CLI.RAZAOSOCIALNOME) CLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) DESTINATARIO ";
            sqlEnviados += " FROM DOCUMENTOEDI EDI with (nolock) ";
            sqlEnviados += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = EDI.IDDOCUMENTO ";
            sqlEnviados += " INNER JOIN CADASTRO C ON C.IDCADASTRO = D.IDDESTINATARIO ";
            sqlEnviados += " INNER JOIN CADASTRO CLI ON CLI.IDCADASTRO = D.IDCLIENTE ";
            sqlEnviados += " WHERE tipo='COMPROVEI' AND  (CAST(D.IDDOCUMENTO AS VARCHAR(50)) LIKE '%" + TextBox1.Text.Trim() + "%' OR CAST(D.NUMERO AS VARCHAR(50)) LIKE '%" + TextBox1.Text.Trim() + "%' OR CAST(D.DOCUMENTODOCLIENTE4 AS VARCHAR(50)) LIKE '%" + TextBox1.Text.Trim() + "%') ";
            sqlEnviados += " ORDER BY EDI.DATA DESC ";

            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sqlEnviados.ToUpper(), cnx);
            GridView1.DataBind();
            txtProcessado.Text = " Último Processamento: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            pesquisar();
        }
    }
}