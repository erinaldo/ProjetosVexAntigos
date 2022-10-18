using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using SistranMODEL;
using System.IO;
using System.Data;

public partial class IntranetMasterBlank : System.Web.UI.MasterPage
{
    #region Events

    private void CarregarCboCLiente()
    {
        LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
        Cliente cli = new Cliente();
        if (cli.ClienteImagen != null)
        {
            MemoryStream ms = new MemoryStream(cli.ClienteImagen);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USUARIO"] == null)
        {
            Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");

        }

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
          
        if (!IsPostBack)
        {
            if (Session["USUARIO"] == null)
            {
                Response.Redirect("Erro.aspx");
            }
                CultureInfo culture = new CultureInfo("pt-BR");
            

            if (Session["ProfileIndex"] != null && !string.IsNullOrEmpty(Session["ProfileIndex"].ToString()) && Convert.ToInt32(Session["ProfileIndex"].ToString()) >= 0)
            {
               
                LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);
                SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
                Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
                lblUserName.Text = "Usuário: " + Usuario.UsuarioNome;
                this.Page.Title = Usuario.UsuarioNome;

     
                CarregarCboCLiente();

                if (LUser.Count == 1)
                {
                    tb_troca.Visible = false;
                }

                if (Session["IDEmpresa"].ToString() != "")
                {
                    foreach (var item in LUser)
                    {
                        if (item.EmpresaId.ToString() == Session["IDEmpresa"].ToString())
                        {                           
                            lblCodEmpresa.Text = item.EmpresaId.ToString();
                            Session["IDEmpresa"] = item.EmpresaId.ToString();
                            return;
                        }
                    }
                }
                else
                {                    
                    lblCodEmpresa.Text = Usuario.EmpresaId.ToString();
                    Session["IDEmpresa"] = Usuario.EmpresaId.ToString();
                }
                 this.Page.Title = Usuario.NomeEmpresa;
            }
            else
            {
                Response.Write("<script>window.setTimeout(\"window.close()\", 1); self.close();</script>");
                Response.End();

            }
        }
    }


    
    protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
    {       
       
        LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        foreach (var item in LUser)
        {
            if (item.EmpresaId.ToString() == lblCodEmpresa.Text)
            {               
                Session["IDEmpresa"] = lblCodEmpresa.Text;
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    #endregion

    #region Methods
    private static List<SistranMODEL.Usuario> LUser;

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {        
        List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranMODEL.Usuario Usuario = new SistranMODEL.Usuario();
        Usuario = LUser[Convert.ToInt32(Session["ProfileIndex"])];
        lblUserName.Text = Usuario.UsuarioNome;
    }

  
    #endregion
      
}
