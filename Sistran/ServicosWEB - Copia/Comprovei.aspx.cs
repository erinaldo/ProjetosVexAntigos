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
            try
            {

           
            Label1.Text = "";

            string sqlEnviados = "";
            sqlEnviados += " SELECT TOP 10 ";
            sqlEnviados += " D.IDDOCUMENTO, D.NUMERO, D.SERIE, /*D.EnviadoComprovei*/	'ENVIADO COMPROVEI - '+  convert(varchar(30), d.DataDeEmissao, 120)   [ARQUIVO ENVIADO], D.EnviadoComprovei RESULTADOCOMPROVEI,  ";
            sqlEnviados += " isnull(D.DOCUMENTODOCLIENTE4,    '') CHAVE, ISNULL(CLI.FANTASIAAPELIDO, CLI.RAZAOSOCIALNOME) CLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) DESTINATARIO ";
            sqlEnviados += " FROM DOCUMENTO D ";
            //--sqlEnviados += " left join DOCUMENTOEDI EDI  with (nolock)   on edi.IdDocumento = d.IDDocumento ";
            sqlEnviados += " INNER JOIN CADASTRO C ON C.IDCADASTRO = D.IDDESTINATARIO ";
            sqlEnviados += " INNER JOIN CADASTRO CLI ON CLI.IDCADASTRO = D.IDCLIENTE ";
            //--sqlEnviados += " LEFT JOIN DOCUMENTOELETRONICO DE ON DE.IDDOCUMENTO = D.IDDOCUMENTO ";
            sqlEnviados += " WHERE 0=0 ";
            sqlEnviados += " and d.dataDeEmissao >= getdate()-20 And d.TipoDeDocumento in('Nota FIscal', 'ORDEM DE SERVICO', 'GUIA DE REMESSA') ";
           
            if (TextBox1.Text.Length > 2)
                sqlEnviados += " AND  (CAST(D.IDDOCUMENTO AS VARCHAR(50)) = '" + TextBox1.Text.Trim() + "' OR CAST(D.NUMERO AS VARCHAR(50)) ='" + TextBox1.Text.Trim() + "' OR CAST(D.DOCUMENTODOCLIENTE4 AS VARCHAR(50)) = '" + TextBox1.Text.Trim() + "') ";

            sqlEnviados += " ORDER BY  d.IdDocumento DESC ";

            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sqlEnviados.ToUpper(), cnx);
            GridView1.DataBind();
            txtProcessado.Text = " Último Processamento: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            }
            catch (Exception)
           {
            }
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            pesquisar();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "r")
            {
                string sql = "Update documento set enviadoComprovei='subir' where idDocumento = " + e.CommandName.ToString() + "; Select 1;";
                //string sql = "Update documento set DataDeConclusao=null, IdDocumentoOcorrencia=null, enviadoComprovei=null where idDocumento = " + e.CommandName.ToString() + "; Select 1;";
                Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                pesquisar();
                Label1.Text = "Documento Reenviado ao COMPROVEI. Favor aguardar o Robo de envio atuar";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = "";
            pesquisar();

        }
    }
}