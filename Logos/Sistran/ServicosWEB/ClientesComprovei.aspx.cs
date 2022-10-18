using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;

namespace ServicosWEB
{
    public partial class ClientesComprovei : System.Web.UI.Page
    {

        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("LoginComprovei.aspx");

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if(!IsPostBack)
                    pesquisar("");
        }

        private void pesquisar(string Argumento)
        {
            Label2.Text = "";            
            string sqlEnviados = "";
            sqlEnviados += " SELECT C.IDCADASTRO [CODIGO],  C.CNPJCPF 'CNPJ',  C.RAZAOSOCIALNOME [RAZAO SOCIAL], C.FANTASIAAPELIDO [NOME FANTASIA], DATA [DATA DE CADASTRO] FROM CLIENTESCOMPROVEI CC  INNER JOIN CADASTRO C ON C.IDCADASTRO = CC.IDCADASTRO WHERE  cc.ATIVO <> 'NAO' " + (Argumento.Length > 0 ? " AND REPLACE(REPLACE(REPLACE(CC.CNPJCPF,'.',''), '/',''), '-','') Like '" + TextBox1.Text.Replace(".", "").Replace("/", "").Replace("-", "") + "%'" : "") + " ORDER BY 3 ";
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(sqlEnviados.ToUpper(), cnx);
            GridView1.DataBind();            
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {


                Label2.Text = "";

                if (TextBox1.Text.Length < 5)
                    return;

                string sql = "SELECT * FROM CLIENTE C INNER JOIN CADASTRO CC ON CC.IDCADASTRO = C.IDCLIENTE WHERE REPLACE(REPLACE(REPLACE(CNPJCPF,'.',''), '/',''), '-','')='" + TextBox1.Text.Replace(".", "").Replace("/", "").Replace("-", "") + "'  AND  CC.IDCADASTRO NOT IN (SELECT IDCADASTRO FROM CLIENTESCOMPROVEI WHERE ATIVO <> 'NAO')";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                if (dt.Rows.Count == 0)
                {

                    sql = "SELECT * FROM CLIENTESCOMPROVEI WHERE ATIVO='SIM' AND REPLACE(REPLACE(REPLACE(CNPJCPF,'.',''), '/',''), '-','')='" + TextBox1.Text.Replace(".", "").Replace("/", "").Replace("-", "")+"'";

                    if (Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx).Rows.Count > 0)
                    {
                        Label2.Text = "CNPJ já cadastrado";
                        TextBox1.Focus();
                        return;
                    }
                    else
                    {
                        Label2.Text = "CNPJ não encontrado.";
                        TextBox1.Focus();
                        return;
                    }
                }


                sql = "INSERT INTO CLIENTESCOMPROVEI VALUES(" + dt.Rows[0]["IDCADASTRO"].ToString() + ", '" + dt.Rows[0]["CNPJCPF"].ToString() + "', '" + dt.Rows[0]["RazaoSocialNome"].ToString() + "', '" + dt.Rows[0]["FantasiaApelido"].ToString() + "', 'SIM', getdate()); select 1";
                dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);
                Label2.Text = "Cliente foi incluído com sucesso!";
                pesquisar("");
                
                TextBox1.Focus();
                TextBox1.Text = "";
                

            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Label2.Text = "";

            if (e.CommandArgument.ToString() == "excluir")
            {
                string sql = "UPDATE CLIENTESCOMPROVEI SET ATIVO='NAO' WHERE IDCADASTRO=" + e.CommandName.ToString() + "; select 1";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(sql.ToUpper(), cnx);

                pesquisar("");
                Label2.Text = "Cliente removido com sucesso!";

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            pesquisar(TextBox1.Text);
        }
    }
}