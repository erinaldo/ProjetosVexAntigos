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
            if(!IsPostBack)
                pesquisar();
        }

        private void pesquisar()
        {
            string sqlEnviados = "";
            sqlEnviados += " SELECT TOP 50 ";
            //sqlEnviados += " D.IDDOCUMENTO, D.NUMERO, D.SERIE, D.EnviadoComprovei [ARQUIVO ENVIADO], EDI.NOMEDOARQUIVO RESULTADOCOMPROVEI, ";
            sqlEnviados += " D.IDDOCUMENTO, D.NUMERO, D.SERIE, /*D.EnviadoComprovei*/	'ENVIADO COMPROVEI - '+  convert(varchar(30), edi.Data, 120)   [ARQUIVO ENVIADO], EDI.NOMEDOARQUIVO RESULTADOCOMPROVEI,  ";
            sqlEnviados += " isnull(D.DOCUMENTODOCLIENTE4, de.IDNOTA) CHAVE, ISNULL(CLI.FANTASIAAPELIDO, CLI.RAZAOSOCIALNOME) CLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) DESTINATARIO ";
            sqlEnviados += " FROM DOCUMENTOEDI EDI with (nolock) ";
            sqlEnviados += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = EDI.IDDOCUMENTO ";
            //sqlEnviados += " FROM DOCUMENTO D  ";
            sqlEnviados += " INNER JOIN CADASTRO C ON C.IDCADASTRO = D.IDDESTINATARIO ";
            sqlEnviados += " INNER JOIN CADASTRO CLI ON CLI.IDCADASTRO = D.IDCLIENTE ";
            sqlEnviados += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = D.IDDOCUMENTO ";
            sqlEnviados += " WHERE 0=0 ";
                if(TextBox1.Text.Length>2)
                         sqlEnviados+=  " and tipo='COMPROVEI' AND  (CAST(D.IDDOCUMENTO AS VARCHAR(50)) = '" + TextBox1.Text.Trim() + "' OR CAST(D.NUMERO AS VARCHAR(50)) ='" + TextBox1.Text.Trim() + "' OR CAST(D.DOCUMENTODOCLIENTE4 AS VARCHAR(50)) = '" + TextBox1.Text.Trim() + "') ";

                sqlEnviados += " ORDER BY  EDI.Data DESC ";

            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sqlEnviados.ToUpper(), cnx);
            GridView1.DataBind();
            txtProcessado.Text = " Último Processamento: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); 
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            pesquisar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "r")
            {
                string sql = "Update documento set DataDeConclusao=null, IdDocumentoOcorrencia=null, enviadoComprovei=null where idDocumento = " + e.CommandName.ToString() + "; Select 1;";
                Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                Label1.Text = "Documento Reenviado ao COMPROVEI. Favor aguardar o Robo de envio atuar";
                pesquisar();

            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = "";
            pesquisar();

        }
    }
}