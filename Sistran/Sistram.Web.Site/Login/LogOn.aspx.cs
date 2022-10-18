using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Caching;
using System.Data;

public partial class Login_LogOn : System.Web.UI.Page
{

    private string dataRequest;
    private string Conn;

    protected void Page_Load(object sender, EventArgs e)
    {

        //verifica se existe a query string cf para buscar qual o banco de dados que a aplicação deve se conectar
        if (Request.QueryString["cf"] != null)
        {
            dataRequest = Request.QueryString["cf"].ToString();
        }
        else
        {
            this.plhLogin.Visible = false;
            Response.Write("Por favor contate a sistecno. Falha com o processo de Login");
        }
    }

    protected void btnLogar_Click(object sender, EventArgs e)
    {
        try
        {
            Conn = BuscaConnection(dataRequest);

            if (!string.IsNullOrEmpty(BuscaConnection(dataRequest)))
            {
                Session["Conn"] = Conn;

                CarregaUsuario();
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "UserLoginFalha", "<script>alert('Problemas com a conexão, por favor contate a sistecno')</script>");
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>alert('" + ex.Message.Replace("'", "´") + "')</script>");
        }

    }

    public static string BuscaConnection(string conn)
    {
        string connection = string.Empty;
        DataSet dt = LoadDataSet();
        foreach (DataRow item in dt.Tables[0].Rows)
        {
            if (item["key"].ToString() == conn)
            {
                connection = item["dataBaseName"].ToString();
            }
        }

        return connection;
    }

    private static DataSet LoadDataSet()
    {
        string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, ConfigurationManager.AppSettings["UrlXmlConnections"]);
        DataSet ds = new DataSet();
        ds.ReadXml(filePath);

        string fileText = System.IO.File.ReadAllText(filePath);

        return ds;
    }

    private static string GetAttributeValue(XElement itemNode, string attributeName)
    {
        return itemNode.Attributes(attributeName).Single().Value;
    }

    private static XDocument LoadXml()
    {
        XDocument xmlConnections = new XDocument();
        Cache cache = HttpContext.Current.Cache;

        if (cache.Get("xmlConnections") == null)
        {
            string filePath = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, ConfigurationManager.AppSettings["UrlXmlConnections"]);
            string fileText = System.IO.File.ReadAllText(filePath);
            xmlConnections = XDocument.Parse(fileText);

            cache.Insert("xmlConnections", xmlConnections);
        }
        else
        {
            xmlConnections = (XDocument)cache.Get("xmlConnections");
        }

        return xmlConnections;
    }

    protected void btnAcessarSistema_Click(object sender, EventArgs e)
    {
        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        Session["USUARIO"] = LUser;
        Session["ProfileIndex"] = 0;
        Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeoutSession"].ToString());

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnderecoSiteDefault"]))
        {
            //ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open ('" + System.Configuration.ConfigurationManager.AppSettings["EnderecoSiteDefault"].ToString() + "');</script>");
            ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('../default.aspx?acao=login');</script>");

        }
    }

    private void CarregaUsuario()
    {

        //busca o id e o nome do usuario que esta se logando
        List<SistranMODEL.Usuario> ILusuario = SistranBLL.Usuario.Login_Alone(txtUser.Text.ToString().ToUpper(), txtSenha.Text.ToString().ToUpper(), Conn, false);

        Session["USUARIO"] = ILusuario;
        Session["ProfileIndex"] = 0;
        Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeoutSession"].ToString());

        //abre uma nova tela do browser 
        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnderecoSiteDefault"]))
        {
            //ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open ('" + System.Configuration.ConfigurationManager.AppSettings["EnderecoSiteDefault"].ToString() + "');</script>");
            ClientScript.RegisterStartupScript(typeof(Page), "User", "<script>window.open('../default.aspx?acao=login');</script>");


        }
    }
}