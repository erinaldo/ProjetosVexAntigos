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
    public partial class ComproveiRec : System.Web.UI.Page
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
            sqlEnviados += " SELECT TOP 50  RETORNOCOMPROVEI.IDDOCUMENTO,  DOCUMENTO.NUMERO , SERIE , CHAVE CHAVE, DATADAOCORRENCIA [DT. OCORRENCIA], OCORRENCIA, IDOCORRENCIACOMPROVEI, ";
            sqlEnviados += " (CASE WHEN FOTO IS NULL THEN 'NAO' ELSE 'SIM' END ) FOTO, ";
            sqlEnviados += " HORARIORECEBIMENTO [RECEBIDO COMPROVEI], C.FANTASIAAPELIDO CLIENTE, D.FANTASIAAPELIDO DESTINATARIO,  ";
            sqlEnviados += " PROCESSADO [PROCESSADO NO SISTRAN] ";
            sqlEnviados += " FROM RETORNOCOMPROVEI ";
            sqlEnviados += " INNER JOIN DOCUMENTO ON DOCUMENTO.IDDOCUMENTO = RETORNOCOMPROVEI.IDDOCUMENTO ";
            sqlEnviados += " INNER JOIN CADASTRO C ON C.IDCADASTRO = IDCLIENTE ";
            sqlEnviados += " INNER JOIN CADASTRO D ON D.IDCADASTRO = IDDESTINATARIO ";
            if (TextBox1.Text.Trim().Length > 0)
                sqlEnviados += " WHERE DOCUMENTO.NUMERO LIKE '%" + TextBox1.Text.Trim() + "%' ";
            sqlEnviados += " ORDER BY HORARIORECEBIMENTO DESC ";
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