using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.Globalization;
using System.Threading;

public partial class frmUsuariosDetalhe : System.Web.UI.Page
{
 
    DataTable dtGravados;
    DataTable dtItensPerfil;

    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {

            CarregarComboPerfil();
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

            txtCPF.Attributes.Add("onkeypress", "return SomenteNumero(event)");

            if (Request.QueryString["id"] != "novo")
            {
                lblIdUsuario.Text = Request.QueryString["id"];
                carregarCampos();
            }
            else
            {
                txtCPF.Text = "";
                txtLogin.Text = "";
                txtEmail.Text = "";
                txtNome.Text = "";
                lblIdUsuario.Text = "0";
                txtSenha.Text = "";
                txtCPF.Focus();
            }
            CarregarRepeaterMenuopcoes();

        }
    }

    private void CarregarComboPerfil()
    {
        cboPerfil.DataSource = new Usuario().ListarPerfil("");
        cboPerfil.DataTextField = "Nome";
        cboPerfil.DataValueField = "IDUsuario";
        cboPerfil.DataBind();
        cboPerfil.Items.Insert(0, new ListItem("Selecione", "0"));
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

                    if (dtItensPerfil != null)
                    {

                        DataRow[] rr = dtItensPerfil.Select("IDMODULOOPCAO=" + imgHab.CommandName.ToString(), "");
                        if (rr.Length == 0)
                        {
                            imgTipo.ImageUrl = "~/Images/user_32x32.bmp";
                        }
                    }
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
            dtItensPerfil = SistranBLL.Menu.MenuOpcoesGravados(cboPerfil.SelectedValue);
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

    private void carregarCampos()
    {
       
        DataTable dtUsers = new SistranBLL.Usuario().Consultar(lblIdUsuario.Text);

        if (dtUsers.Rows.Count > 0)
        {
            txtCPF.Text = dtUsers.Rows[0]["CnpjCpf"].ToString();
            txtLogin.Text = dtUsers.Rows[0]["Login"].ToString();
            txtEmail.Text = dtUsers.Rows[0]["email"].ToString();
            txtNome.Text = dtUsers.Rows[0]["Nome"].ToString();
            lblIdUsuario.Text = dtUsers.Rows[0]["IDUSUARIO"].ToString();
            txtSenha.Text = dtUsers.Rows[0]["SENHA"].ToString();

            if(dtUsers.Rows[0]["IDPERFIL"].ToString() !="")
                cboPerfil.SelectedValue = dtUsers.Rows[0]["IDPERFIL"].ToString();

            
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if(i==0)
                    chkEscolhidosFinal.Items.Clear();

                string nome = dtUsers.Rows[i]["NOMECLIENTE"].ToString();

                if (nome.Length > 40)
                    nome = nome.Substring(0, 40) + "...";

                chkEscolhidosFinal.Items.Add(new ListItem(nome, dtUsers.Rows[i]["CODIGODOCLIENTE"].ToString()));
                chkEscolhidosFinal.Items[i].Selected = true;
            }

        }
    }


    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        ListBox lstSelecionados = new ListBox();
        try
        {
            if (cboPerfil.SelectedValue == "0")
                throw new Exception("Selecione um perfil.");

            if (chkEscolhidosFinal.Items[0].Text == "Nenhum Cliente")
                throw new Exception("Selecione ao menos um Cliente");

            int countSelecionados = 0;
            for (int i = 0; i < chkEscolhidosFinal.Items.Count; i++)
            {
                if (chkEscolhidosFinal.Items[i].Selected == true)
                {
                    countSelecionados++;
                }
            }

            if (countSelecionados == 0)
                throw new Exception("Selecione ao menos um Cliente");


            HttpContext.Current.Session["IDEmpresa"] = "";

            string clientesSelec = "";
            for (int i = 0; i < chkEscolhidosFinal.Items.Count; i++)
            {
                if (chkEscolhidosFinal.Items[i].Selected == true)
                {
                    clientesSelec += chkEscolhidosFinal.Items[i].Value + ",";
                }
            }
            HttpContext.Current.Session["IDEmpresa"] = clientesSelec.Substring(0, clientesSelec.Length - 1);

            if (Request.QueryString["id"].ToLower() == "novo")
            {
                if (txtCPF.Text.Trim().Length == 11 || txtCPF.Text.Trim().Length == 14)
                {
                    txtCPF.Text = FuncoesGerais.FormatarCnpj(txtCPF.Text);
                }


                //PASSA OS DADOS PARA A CLASSE USUARIO E FAZ A OPERAÇÃO DE ALTERAÇÃO / INCLUSO
                string[] dados = new SistranBLL.Usuario().GravarUsuarios((lblIdCadastro.Text == "0" ? "" : lblIdCadastro.Text), "", txtCPF.Text, txtLogin.Text, txtLogin.Text, txtEmail.Text, txtSenha.Text, lstSelecionados, cboPerfil.SelectedValue);
                lblIdUsuario.Text = dados[1];
                InserirHabPerfil();
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "f", "window.close();", true);
                
                string Carta = "Caro Usuario " + txtNome.Text + "<br>";
                Carta += " Conforme solicitado segue os dados para acesso ao site http://www.grupologos.com.br";
                Carta += "<BR> Usuário: " + txtLogin.Text;
                Carta += "<BR> Senha: " + txtSenha.Text;
                Carta += "<BR>";
                Carta += "<BR>";
                Carta += "<BR> PARA ALTERAR SUA SENHA CLIQUE <A HREF=http://www.grupologos.com.br/intranet/trocarsenha.aspx?id=" + lblIdUsuario.Text + ">AQUI</A>";

                Sistran.Library.EnviarEmails.EnviarEmail(txtEmail.Text, "sistema@grupologos.com.br", "INCLUSÃO DE USUÁRIO", Carta, "mail.grupologos.com.br", "logos0902");

            }
            else
            {
                string[] dados = new SistranBLL.Usuario().GravarUsuarios(Request.QueryString["ic"], Request.QueryString["id"], txtCPF.Text, txtNome.Text, txtLogin.Text, txtEmail.Text, txtSenha.Text, lstSelecionados, cboPerfil.SelectedValue);
                InserirHabPerfil();
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "f", "window.close();", true);

                string Carta = "Caro Usuario " + txtNome.Text + "<br>";
                Carta += " Conforme solicitado segue os novos dados para acesso ao site http://www.grupologos.com.br";
                Carta += "<BR> Usuário: " + txtLogin.Text;
                Carta += "<BR> Senha: " + txtSenha.Text;
                Carta += "<BR>";
                Carta += "<BR>";
                Carta += "<BR> PARA ALTERAR SUA SENHA CLIQUE <A HREF=http://www.grupologos.com.br/intranet/trocarsenha.aspx?id=" + lblIdUsuario.Text + ">AQUI</A>";

                Sistran.Library.EnviarEmails.EnviarEmail(txtEmail.Text, "sistema@grupologos.com.br", "ALTERAÇÃO DE USUARIO E SENHA", Carta, "mail.grupologos.com.br", "logos0902");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
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
        else
        {
            txtCPF.Text = FuncoesGerais.FormatarCnpj(txtCPF.Text);
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
                    txtEmail.Text = dtUsers.Rows[0]["email"].ToString();
                    txtNome.Text = dtUsers.Rows[0]["Nome"].ToString();
                    lblIdUsuario.Text = dtUsers.Rows[0]["IDUSUARIO"].ToString();
                    cboPerfil.SelectedValue = dtUsers.Rows[0]["IDPERFIL"].ToString();

                }
            }
        }       
        txtLogin.Focus();
    }
        
    protected void cboPerfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int iie = 0; iie < rptMenu.Items.Count; iie++)
        {
            Image imgTipo = (Image)rptMenu.Items[iie].FindControl("imgTipo");
            ImageButton imgHab = (ImageButton)rptMenu.Items[iie].FindControl("imgHab");
            imgTipo.Visible = false;
            imgHab.ImageUrl = "~/images/Botao_ConfirmarVistaDisabled.ico";
            imgHab.ToolTip = "Clique aqui para habilitar acesso.";
        }

        if (cboPerfil.SelectedIndex > 0)
        {


            DataTable f = SistranBLL.Menu.MenuOpcoesGravados(cboPerfil.SelectedValue);

            if (f.Rows.Count > 0)
            {
                for (int i = 0; i < f.Rows.Count; i++)
                {
                    for (int ii = 0; ii < rptMenu.Items.Count; ii++)
                    {
                        Image imgTipo = (Image)rptMenu.Items[ii].FindControl("imgTipo");
                        ImageButton imgHab = (ImageButton)rptMenu.Items[ii].FindControl("imgHab");


                        if (imgHab.CommandName.ToString() == f.Rows[i]["IDModuloOpcao"].ToString())
                        {

                            imgTipo.Visible = true;
                            imgHab.ToolTip = "Clique aqui para desabilitar acesso.";
                            imgHab.ImageUrl = "~/images/Botao_ConfirmarVista.ico";
                        }
                    }
                }
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        dvEscolherCliente.Visible = true;
        btnAbriPesquisarCliente.Visible = false;
        txtFiltroNome.Focus();
        txtFiltroNome.Text = "";
        rdListClientes.Items.Clear();
    }

    protected void btnPesquisarFiltro_Click(object sender, EventArgs e)
    {
        rdListClientes.Items.Clear();
        if (txtFiltroNome.Text.Trim() == "")
        {
            return;
        }

        DataTable dtRet = new SistranBLL.Cliente().RetornarClientesIntranet(txtFiltroNome.Text, "");
        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();

            if (nome.Length > 40)
                nome = nome.Substring(0, 40) + "...";
            rdListClientes.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["IDCLIENTE"].ToString()));
        }

        rdListClientes.Visible = true;
        btnConfirmar.Visible = true;
        tblEscolherClientes.Visible = true;

    }
    
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        dvEscolherCliente.Visible = false;
        btnAbriPesquisarCliente.Visible = true;

        if (chkEscolhidosFinal.Items.Count == 1 && chkEscolhidosFinal.Items[0].Text == "Nenhum Cliente")
            chkEscolhidosFinal.Items.Clear();


        for (int i = 0; i < rdListClientesSelecionados.Items.Count; i++)
        {
            if (rdListClientesSelecionados.Items[i].Selected == true)
            {
                bool existe = false;

                for (int iii = 0; iii < chkEscolhidosFinal.Items.Count; iii++)
                {                    
                    if (chkEscolhidosFinal.Items[iii].Value == rdListClientesSelecionados.Items[i].Value)
                    {
                        existe = true;
                        break;
                    }
                }

                if (existe == false)
                {
                    chkEscolhidosFinal.Items.Add(new ListItem(rdListClientesSelecionados.Items[i].Text, rdListClientesSelecionados.Items[i].Value, true));
                }

                for (int iii = 0; iii < chkEscolhidosFinal.Items.Count; iii++)
                {
                    chkEscolhidosFinal.Items[iii].Selected = true;
                }
            }
        }
        rdListClientes.Items.Clear();
        rdListClientesSelecionados.Items.Clear();
        txtFiltroNome.Text = "";
    }
    
    protected void chkEscolhidosFinal_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkEscolhidosFinal.Items.Count; i++)
        {
            if (chkEscolhidosFinal.Items[i].Selected == false)
            {
                chkEscolhidosFinal.Items.RemoveAt(i);
                return;
            }
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (dvEscolherUsuario.Visible == false)
        {

            dvEscolherUsuario.Visible = true;
            pnlteste.Enabled = false;
            txtNome.Text = "";
            txtNome.Focus();
            
        }
    }

    protected void btnPesquisrFiltro_Click(object sender, EventArgs e)
    {
        RadioButtonList1.Items.Clear();

        if (txtFiltroNomeCliente.Text.Trim() != "")
        {
            DataTable dt = new Cliente().RetornarClientesUsuariosPelasIniciais(txtFiltroNomeCliente.Text.Trim().ToUpper());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RadioButtonList1.Items.Add(new ListItem(dt.Rows[i]["CnpjCpf"].ToString() + "-" + dt.Rows[i]["RazaoSocialNome"].ToString(), dt.Rows[i]["IDCadastro"].ToString() + "|" + dt.Rows[i]["CnpjCpf"].ToString() + "|" + dt.Rows[i]["RazaoSocialNome"].ToString() + "|" + dt.Rows[i]["login"].ToString() + "|" + dt.Rows[i]["senha"].ToString() + "|" + dt.Rows[i]["idusuario"].ToString()));             
            }
        }
    }


    protected void btnConfirmarUsuario_Click(object sender, EventArgs e)
    {
        
        for (int i = 0; i < RadioButtonList1.Items.Count; i++)
        {
            if (RadioButtonList1.Items[i].Selected == true)
            {
                dvEscolherUsuario.Visible = false;

                txtCPF.ReadOnly = false;
                txtNome.ReadOnly = false;

                string[] dadosSelecionados  =  RadioButtonList1.Items[i].Value.ToString().Split('|');
                lblIdCadastro.Text = dadosSelecionados[0];
                txtCPF.Text = dadosSelecionados[1];
                txtNome.Text = dadosSelecionados[2];
                txtLogin.Text = dadosSelecionados[3];
                txtSenha.Text = dadosSelecionados[4];
                lblIdUsuario.Text = dadosSelecionados[5];
                txtCPF.ReadOnly = true;
                txtNome.ReadOnly = true;

              

                RadioButtonList1.Items.Clear();
                txtFiltroNomeCliente.Text = "";
                pnlteste.Enabled = true;

                if (txtLogin.Text == "")
                {
                    txtLogin.Focus();
                }

                chkEscolhidosFinal.Items.Clear();

                if (lblIdUsuario.Text != "" && lblIdUsuario.Text != "0")
                {
                    carregarCampos();
                }
                return;
            }

        }
    }
    
    protected void rdListClientes_SelectedIndexChanged1(object sender, EventArgs e)
    {
        for (int i = 0; i < rdListClientes.Items.Count; i++)
        {
            if (rdListClientes.Items[i].Selected == true)
            {
                carregarListClientesRelacionados(rdListClientes.Items[i].Value);
            }
        }
    }

    private void carregarListClientesRelacionados(string p)
    {        
        DataTable dtRet = new SistranBLL.Cliente().RetornarClientasRelacionados(p);

        for (int i = 0; i < dtRet.Rows.Count; i++)
        {
            string nome = dtRet.Rows[i]["CNPJCPF"].ToString() + " - " + dtRet.Rows[i]["RAZAOSOCIALNOME"].ToString().ToUpper();
            if (nome.Length > 35)
                nome = nome.Substring(0, 35) + "...";


            bool existe = false;

            for (int ii = 0; ii < rdListClientesSelecionados.Items.Count; ii++)
            {
                if (rdListClientesSelecionados.Items[ii].Value == dtRet.Rows[i]["IDCLIENTE"].ToString())
                {
                    existe = true;
                    return;
                }
            }

            if (existe == false)
                rdListClientesSelecionados.Items.Insert(i, new ListItem(nome, dtRet.Rows[i]["IDCLIENTE"].ToString()));


            rdListClientesSelecionados.Items[i].Selected = true;
            btnConfirmar.Visible = true;
        }

        if (rdListClientesSelecionados.Items.Count > 0)
        {
            rdListClientesSelecionados.Visible = true;
        }
    }

    
}