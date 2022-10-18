using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.Globalization;
using System.Threading;


public partial class frmPerfisDetalhe : System.Web.UI.Page
{
    DataTable dtGravados;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");


            if (!IsPostBack)
            {
                Label1.Text = Request.QueryString["id"];
                carregarCampos();
                CarregarRepeaterMenuopcoes();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void CarregarRepeaterMenuopcoes()
    {        
        //List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];

        if (Request.QueryString["id"] != "novo")
        {
            dtGravados = SistranBLL.Menu.MenuOpcoesGravados(Request.QueryString["id"]);
        }
        rptMenu.DataSource = SistranBLL.Menu.MenuOpcoes();
        rptMenu.DataBind();
    }

    private void carregarCampos()
    {
        if (Request.QueryString["id"] == "novo")
            return;

        DataTable dt = new SistranBLL.Usuario().ConsultarPerfil(Request.QueryString["id"]);

        if (dt.Rows.Count > 0)
        {
            txtNf0.Text = dt.Rows[0]["Nome"].ToString().ToUpper();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

        
        if (txtNf0.Text.Trim() == "")
        {
            throw new Exception("Informe o nome do perfil.");
        }

        InserirPerfil();
        InserirHabPerfil();
        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);
        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Fechar", "window.close();", true);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);

        }


    }

    private void InserirHabPerfil()
    {
        SistranBLL.Menu.DesabilitarAcessoByIdUsuario(Label1.Text, Session["Conn"].ToString());

        for (int i = 0; i < rptMenu.Items.Count; i++)
        {
            Image imgTipo = (Image)rptMenu.Items[i].FindControl("imgTipo");
            ImageButton imgHab = (ImageButton)rptMenu.Items[i].FindControl("imgHab");

            if (imgTipo.Visible == true)
            {
                SistranBLL.Menu.InserirHabilitacoes(Label1.Text, imgHab.CommandName, "SIM", Session["Conn"].ToString());
            }
        }
        
    }

    private void InserirPerfil()
    {
        Usuario o = new Usuario();
      Label1.Text =  o.GravarPerfil(Request.QueryString["id"], txtNf0.Text.ToUpper()).ToString();

    }

    protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image imgTipo = (Image)e.Item.FindControl("imgTipo");
        ImageButton imgHab = (ImageButton)e.Item.FindControl("imgHab");
               

        if (imgTipo != null )
        {
            if (dtGravados != null)
            {
                DataRow[] r = dtGravados.Select("IDMODULOOPCAO=" + imgHab.CommandName.ToString(), "");

                if (r.Length == 0)
                {
                    imgTipo.Visible = false;
                    imgHab.ImageUrl = "~/images/Botao_ConfirmarVistaDisabled.ico";
                    imgHab.ToolTip = "Clique aqui para habilitar acesso.";
                }
                else
                {
                    imgTipo.Visible = true;
                    imgHab.ToolTip = "Clique aqui para desabilitar acesso.";
                    imgHab.ImageUrl = "~/images/Botao_ConfirmarVista.ico";
                }
            }
            else
            {
                imgTipo.Visible = false;
                imgHab.ImageUrl = "~/images/Botao_ConfirmarVistaDisabled.ico";
                imgHab.ToolTip = "Clique aqui para habilitar acesso.";
            }

        }       

    }
    protected void rptMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "edit")
        {
            Image imgTipo = (Image)e.Item.FindControl("imgTipo");
            ImageButton imgHab = (ImageButton)e.Item.FindControl("imgHab");

            if (imgTipo.Visible == false)
            {
                imgTipo.Visible = true;
                imgHab.ImageUrl = "~/images/Botao_ConfirmarVista.ico";
                imgHab.ToolTip = "Clique aqui para desabilitar acesso.";

            }
            else
            {
                imgTipo.Visible = false;
                imgHab.ImageUrl = "~/images/Botao_ConfirmarVistaDisabled.ico";
                imgHab.ToolTip = "Clique aqui para habilitar acesso.";

            }
        }
    }
}