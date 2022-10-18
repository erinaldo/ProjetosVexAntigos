using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            int dia = DateTime.Now.Day;
            int ano = DateTime.Now.Year;
            string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
            string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
            string data = diasemana + ", " + dia + " de " + mes + " de " + ano;
            lblDataHora.Text = data;


            if (Session["ProfileIndex"] != null && !string.IsNullOrEmpty(Session["ProfileIndex"].ToString()) && Convert.ToInt32(Session["ProfileIndex"].ToString()) >= 0)
            {
                LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
                SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
                Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];

                lblUserName.Text = "Bem Vindo(a), " + Usuario.UsuarioNome;

                //lblNomeUsuario.Text = ;
                //lblLogin.Text = Usuario.Login;
                //lblNomeEmpresa.Text = Usuario.NomeEmpresa;
                //ddlClientes.DataSource = LUser;
                //ddlClientes.DataTextField = "RazaoSocialNome";
                //ddlClientes.DataValueField = "EmpresaId";
                //ddlClientes.DataBind();
               //CarregaMenu();
            }
            else
            {
                Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
                Response.End();

            }

        }

    }

    #region Menu
    private static List<SistranMODEL.Usuario> LUser;

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["ProfileIndex"] = ddlClientes.SelectedIndex.ToString();

        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
        Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
        lblUserName.Text = Usuario.UsuarioNome;


        //lblNomeUsuario.Text = Usuario.UsuarioNome;
        //lblLogin.Text = Usuario.Login;
        //lblNomeEmpresa.Text = Usuario.NomeEmpresa;

        //ddlClientes.SelectedIndex = Convert.ToInt32(Session["ProfileIndex"]);

       
        //ModalPopupExtender1.Dispose();
        //ModalPopupExtender1.Hide();


    }

    //private void CarregaMenu()
    //{
    //    SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
    //    Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];

    //    List<SistranMODEL.Menu> menuPai = new List<SistranMODEL.Menu>();
    //    menuPai = SistranBLL.Menu.GetMenuParent(Usuario.UsuarioId, Session["Conn"].ToString());     
    //    //nodes pais
    //    foreach (var item in menuPai)
    //    {
    //        TreeNode newNode = new TreeNode(item.Titulo.ToString(), item.Id.ToString());
    //        TreeViewMenu.Nodes.Add(newNode);

    //        //adiciona os nos filhos
    //        AddChildNodes(newNode, item.Id);
    //    }


    //    TreeNode newNodeExtender;
    //    newNodeExtender = new TreeNode("Externo");
    //    TreeViewMenu.Nodes.Add(newNodeExtender);

    //    newNodeExtender = new TreeNode("Sair");
    //    TreeViewMenu.Nodes.Add(newNodeExtender);

    //}

    //private void AddChildNodes(TreeNode parentNode, int parentId)
    //{
    //    //var childNodeIds GetChildNodeIds(parentNode.ValueId);

    //    SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
    //    Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];

    //    List<SistranMODEL.menuChildren> menuChildren = new List<SistranMODEL.menuChildren>();
    //    menuChildren = SistranBLL.menuChildren.GetMenuChildren(Usuario.UsuarioId, parentId, Session["Conn"].ToString());


    //    foreach (var childNodeId in menuChildren)
    //    {
    //        TreeNode childNode = new TreeNode(childNodeId.Titulo.ToString(), childNodeId.Id.ToString());
    //        childNode.NavigateUrl = childNodeId.Link;
    //        //add to parent         
    //        parentNode.ChildNodes.Add(childNode);
    //       //call same function recursively
    //        if (menuChildren.Count > 0)
    //        {
    //            AddChildNodes(childNode, childNodeId.Id);
    //        }

    //    }


    //}
    #endregion
}
