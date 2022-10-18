using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.Globalization;
using System.Threading;

public partial class frmUsuariosDetalheInterno : System.Web.UI.Page
{
    DataTable dtxx;
    string n = "";
    DataRow pp;
    DataTable dtGravados;

    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

            txtCPF.Attributes.Add("onkeypress", "return SomenteNumero(event)");

            if (Request.QueryString["id"] != "novo")
            {
                carregarCampos();
            }
            else
            {
                txtCPF.Text = "";
                txtLogin.Text = "";                
                txtNome.Text = "";
                lblIdUsuario.Text = "0";
                txtSenha.Text = "";
                txtCPF.Focus();
            }            
            CarregarRepeaterMenuopcoes();

        }
    }

    
    protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image imgTipo = (Image)e.Item.FindControl("imgTipo");
        ImageButton imgHab = (ImageButton)e.Item.FindControl("imgHab");


        if (imgTipo != null)
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
                imgTipo.ImageUrl = "~/Images/user_32x32.bmp";

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

    private void CarregarRepeaterMenuopcoes()
    {
        if (Request.QueryString["id"] != "novo")
        {
            dtGravados = SistranBLL.Menu.MenuOpcoesGravados(Request.QueryString["id"]);            
        }
        rptMenu.DataSource = SistranBLL.Menu.MenuOpcoes();
        rptMenu.DataBind();

    }

    private void InserirHabPerfil()
    {
        SistranBLL.Menu.DesabilitarAcessoByIdUsuario(lblIdUsuario.Text, Session["Conn"].ToString());

        for (int i = 0; i < rptMenu.Items.Count; i++)
        {
            Image imgTipo = (Image)rptMenu.Items[i].FindControl("imgTipo");
            ImageButton imgHab = (ImageButton)rptMenu.Items[i].FindControl("imgHab");

            if (imgTipo.Visible == true)
            {
                SistranBLL.Menu.InserirHabilitacoes(lblIdUsuario.Text, imgHab.CommandName, "SIM", Session["Conn"].ToString());
            }
        }

    }

    //private void CarregarListGravados()
    //{
    //    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
    //    lstSelecionados.DataSource = new SistranBLL.Cliente.Divisao().ListarDivisoesCadastradasUser(lblIdUsuario.Text, Session["IDEmpresa"].ToString());
    //    lstSelecionados.DataTextField = "Nome";
    //    lstSelecionados.DataValueField = "IDCLIENTEDIVISAO";
    //    lstSelecionados.DataBind();
    //}

    private void CarregarDivisao()
    {
        DataTable DivisoesCompleta = SistranDAO.Cliente.DivisoesCompleta(Session["IDEmpresa"].ToString());
        Session["DivisoesCompleta"] = DivisoesCompleta;

        dtxx = new DataTable();
        dtxx.Columns.Add("IDCLIENTEDIVISAO");
        dtxx.Columns.Add("NOME");

        DataRow[] xx = DivisoesCompleta.Select("IDPARENTE IS NULL ", "");

        if (xx.Length == 0)
        {
            return;
        }

        foreach (var item in xx)
        {
            //pp = dtxx.NewRow();
            //pp[0] = xx[0]["IDCLIENTEDIVISAO"].ToString();
            //pp[1] = xx[0]["NOME"].ToString();
            //dtxx.Rows.Add(pp);

            pp = dtxx.NewRow();
            pp[0] = item["IDCLIENTEDIVISAO"].ToString();
            pp[1] = item["NOME"].ToString();
            dtxx.Rows.Add(pp);


            procurarFilhosDestino(DivisoesCompleta, item["IDCLIENTEDIVISAO"].ToString());

        }


        //lstSelecionados.Rows = (dtxx.Rows.Count + 5);
        //Repeater1.DataSource = dtxx;
        //Repeater1.DataBind();

    }

    private void procurarFilhosDestino(DataTable DivisoesCompleta, string IdClienteDivisao)
    {
        DataRow[] cc = DivisoesCompleta.Select("IDPARENTE='" + IdClienteDivisao + "'", "");

        foreach (DataRow item in cc)
        {
            //nivel 2
            n = "&nbsp;&nbsp;&nbsp;&nbsp;";
            pp = dtxx.NewRow();
            pp[0] = item["IDCLIENTEDIVISAO"].ToString();
            pp[1] = n + item["NOME"].ToString();
            dtxx.Rows.Add(pp);

            DataRow[] ccx = DivisoesCompleta.Select("IDPARENTE='" + item["IDCLIENTEDIVISAO"].ToString() + "'", "");

            if (ccx.Length > 0)
            {
                // nivel 3
                n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                foreach (DataRow itemff in ccx)
                {
                    pp = dtxx.NewRow();
                    pp[0] = itemff["IDCLIENTEDIVISAO"].ToString();
                    pp[1] = n + itemff["NOME"].ToString();
                    dtxx.Rows.Add(pp);


                    DataRow[] ccz = DivisoesCompleta.Select("IDPARENTE='" + itemff["IDCLIENTEDIVISAO"].ToString() + "'", "");
                    if (ccz.Length > 0)
                    {
                        //nivel 4
                        foreach (DataRow itemfff in ccz)
                        {
                            n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            pp = dtxx.NewRow();
                            pp[0] = itemfff["IDCLIENTEDIVISAO"].ToString();
                            pp[1] = n + itemfff["NOME"].ToString();
                            dtxx.Rows.Add(pp);

                            DataRow[] ccNivel4 = DivisoesCompleta.Select("IDPARENTE='" + itemfff["IDCLIENTEDIVISAO"].ToString() + "'", "");

                            if (ccNivel4.Length > 0)
                            {
                                //nivel 5
                                foreach (DataRow ItemNivel4 in ccNivel4)
                                {
                                    n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    pp = dtxx.NewRow();
                                    pp[0] = ItemNivel4["IDCLIENTEDIVISAO"].ToString();
                                    pp[1] = n + ItemNivel4["NOME"].ToString();
                                    dtxx.Rows.Add(pp);



                                    DataRow[] ccNivel5 = DivisoesCompleta.Select("IDPARENTE='" + ItemNivel4["IDCLIENTEDIVISAO"].ToString() + "'", "");
                                    if (ccNivel5.Length > 0)
                                    {
                                        //nivel 6
                                        foreach (DataRow ItemNivel5 in ccNivel5)
                                        {
                                            n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                            pp = dtxx.NewRow();
                                            pp[0] = ItemNivel5["IDCLIENTEDIVISAO"].ToString();
                                            pp[1] = n + ItemNivel5["NOME"].ToString();
                                            dtxx.Rows.Add(pp);
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }

    private void carregarCampos()
    {
        DataTable dtUsers = new SistranBLL.Usuario().ConsultarIntranet(Request.QueryString["id"]);

        if (dtUsers.Rows.Count > 0)
        {
            if (dtUsers.Rows[0]["CnpjCpf"].ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length != 14 && dtUsers.Rows[0]["CnpjCpf"].ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length != 11)
                txtCPF.Text = "";
            else
                txtCPF.Text = dtUsers.Rows[0]["CnpjCpf"].ToString();


            txtLogin.Text = dtUsers.Rows[0]["Login"].ToString();
            //           txtEmail.Text = dtUsers.Rows[0]["email"].ToString();
            txtNome.Text = dtUsers.Rows[0]["Nome"].ToString();
            lblIdUsuario.Text = dtUsers.Rows[0]["IDUSUARIO"].ToString();
            txtSenha.Text = dtUsers.Rows[0]["SENHA"].ToString();
            //cboPerfil.SelectedValue = dtUsers.Rows[0]["IDPERFIL"].ToString();

        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {

            InserirHabPerfil();
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "f", "window.close();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            LinkButton lblNome0 = (LinkButton)e.Item.FindControl("lblNome0");
            if (lblNome0 != null)
            {
                hdCodigoDivisaoCliente.Value = e.CommandArgument.ToString();
                hdCodigoDivisaoCliente0.Value = lblNome0.Text.Trim().Replace("&nbsp;", "");
            }
        }

    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblIDClienteDivisao = (Label)e.Item.FindControl("lblIDClienteDivisao");
        LinkButton lblNome0 = (LinkButton)e.Item.FindControl("lblNome0");

        if (lblNome0 == null)
            return;


        lblNome0.CommandArgument = lblIDClienteDivisao.Text;
        lblNome0.CommandName = "edit";
    }

    protected void txtCPF_TextChanged(object sender, EventArgs e)
    {
        if (txtCPF.Text.Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length != 14 && txtCPF.Text.Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length != 11)
        {
            txtCPF.Text = "";
        }

        if (Request.QueryString["id"] == "novo")
        {
            DataTable dtUsers;

            if (txtCPF.Text.Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length == 14 || txtCPF.Text.Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length == 11)
            {
                dtUsers = new SistranBLL.Usuario().Listar("", "", FuncoesGerais.FormatarCnpj(txtCPF.Text));
                txtCPF.Text = FuncoesGerais.FormatarCnpj(txtCPF.Text);

                if (dtUsers.Rows.Count > 0)
                {
                    txtCPF.Text = FuncoesGerais.FormatarCnpj(dtUsers.Rows[0]["CnpjCpf"].ToString());
                    txtLogin.Text = dtUsers.Rows[0]["Login"].ToString();
                    txtNome.Text = dtUsers.Rows[0]["Nome"].ToString();
                    lblIdUsuario.Text = dtUsers.Rows[0]["IDUSUARIO"].ToString();
                }
            }

        }
        txtLogin.Focus();
    }
}