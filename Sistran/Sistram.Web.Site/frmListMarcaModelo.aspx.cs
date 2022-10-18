using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using AjaxControlToolkit;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


public partial class frmListMarcaModelo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CarregarMarcas();
            
            if (lblMracaID.Text != "0" && lblMracaID.Text != "")
                carregarModelos(lblMracaID.Text);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
            {
                lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"] != null ? Request.QueryString["opc"].ToString().ToUpper() : "Marca / Modelo");
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                pnlModelo.Visible = false;
                lblTitulo1.Visible = false;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }
    
    private void CarregarMarcas()
    {
        PHMarca.Controls.Clear();
        PHMarca.Controls.Add(new LiteralControl(@"<table cellspacing=1 celpanding=1>"));

        DataTable dtMarca = new SistranBLL.Veiculo.Marca().Listar();


        if (dtMarca.Rows.Count == 0)
        {
            PHMarca.Controls.Add(new LiteralControl(@"<tr>"));
            PHMarca.Controls.Add(new LiteralControl(@"<td align='rigth' style='font-weight: bold; font-family: Verdana' >Nenhum Registro Encontrado..."));
            PHMarca.Controls.Add(new LiteralControl(@"</td>"));
            PHMarca.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            PHMarca.Controls.Add(new LiteralControl(@"<tr>"));
            PHMarca.Controls.Add(new LiteralControl(@"<td align='rigth' style='font-weight: bold; font-family: Verdana' > Selecione uma Marca para alterar ou incluir um Modelo"));
            PHMarca.Controls.Add(new LiteralControl(@"</td>"));
            PHMarca.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dtMarca.Rows)
            {
                PHMarca.Controls.Add(new LiteralControl(@"<tr>"));
                PHMarca.Controls.Add(new LiteralControl(@"<td>"));
                
                PHMarca.Controls.Add(criarBotao(item["IDVeiculoMarca"].ToString(),item["Nome"].ToString()));
                PHMarca.Controls.Add(new LiteralControl(@"</td>"));

            }

            PHMarca.Controls.Add(new LiteralControl(@"</Table>"));

        }
    }

    private void carregarModelos(string IdVeiculoMarca)
    {
        pnlModelo.Visible = false;

        PHModelo.Controls.Clear();
        PHModelo.Controls.Add(new LiteralControl(@"<table cellspacing=1 celpanding=1>"));

        DataTable dtModelo = new SistranBLL.Veiculo.Modelo().Listar(IdVeiculoMarca);
        pnlModelo.Visible = true;

        if (dtModelo.Rows.Count == 0)
        {      
            lblModeloSelecionado.Text = "Nenhum Registro Encontrado. Clique em Novo para Incluir";
        }
        else
        {
            lblModeloSelecionado.Text = "";
            PHModelo.Controls.Add(new LiteralControl(@"<tr>"));
            PHModelo.Controls.Add(new LiteralControl(@"<td align='rigth' style='font-weight: bold; font-family: Verdana' > Selecione um Modelo ou Clique em Novo para Incluir "));
            PHModelo.Controls.Add(new LiteralControl(@"</td>"));
            PHModelo.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dtModelo.Rows)
            {
                PHModelo.Controls.Add(new LiteralControl(@"<tr>"));
                PHModelo.Controls.Add(new LiteralControl(@"<td>"));
                PHModelo.Controls.Add(criarBotaoModelo(item["IDVeiculoModelo"].ToString(), item["Nome"].ToString()));
                PHModelo.Controls.Add(new LiteralControl(@"</td>"));
            }

            PHModelo.Controls.Add(new LiteralControl(@"</Table>"));
        }
    }

    private Button criarBotao(string chave, string texto)
    {
        Button bot = new Button();
        bot.Text = texto;
        bot.BorderStyle = BorderStyle.None;
        bot.Font.Name = "Verdana";
        bot.Style.Add("font-size", "7pt");
        bot.CommandArgument = chave;
        bot.Enabled = false;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Enabled = true;
        bot.Click += new EventHandler(Button_Click);
        bot.BackColor = System.Drawing.Color.Transparent;
        bot.ForeColor = System.Drawing.Color.Black;
        bot.CausesValidation = false;
        return bot;
    }

    private Button criarBotaoModelo(string chave, string texto)
    {
        Button botModelo = new Button();
        botModelo.Text = texto;
        botModelo.BorderStyle = BorderStyle.None;
        botModelo.Font.Name = "Verdana";
        botModelo.Style.Add("font-size", "7pt");
        botModelo.CommandArgument = chave;
        botModelo.Enabled = false;
        botModelo.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        botModelo.Enabled = true;
        botModelo.Click += new EventHandler(ButtonModelo_Click);
        botModelo.BackColor = System.Drawing.Color.Transparent;
        botModelo.ForeColor = System.Drawing.Color.Black;
        return botModelo;
    }

    private void Button_Click(object sender, System.EventArgs e)
    {
        Button b = (Button)sender;
        lblMracaSelecionada.Text = "Marca Selecionada: ";
        txtMarcaSelecionada.Text =  b.Text;
        txtMarcaSelecionada.Visible = true;
        btnAlterarMarca.Text = "Confirmar";
        btnCancelar.Visible = true;
        txtMarcaSelecionada.Focus();
        lblMracaID.Text = b.CommandArgument.ToString();
        lblTitulo1.Visible = true;
        carregarModelos(b.CommandArgument);


        //lblModeloSelecionado.Text = "";
        txtModeloSelecionada.Visible = false;
        lblModeloID.Text = "0";

    }

    private void ButtonModelo_Click(object sender, System.EventArgs e)
    {
        Button b1 = (Button)sender;
        lblModeloSelecionado.Text = "Modelo Selecionado: ";
        txtModeloSelecionada.Text = b1.Text;
        txtModeloSelecionada.Visible = true;
        btnAlterarMarca0.Text = "Confirmar";
        btnCancelar0.Visible = true;
        txtModeloSelecionada.Focus();
        lblModeloID.Text = b1.CommandArgument.ToString();
    }
    
    protected void btnAlterarMarca_Click(object sender, EventArgs e)
    {
        if (btnAlterarMarca.Text == "Novo")
        {
            txtMarcaSelecionada.Visible = true;
            txtMarcaSelecionada.Text = "";
            txtMarcaSelecionada.Focus();
            lblMracaSelecionada.Text = "Digite o Nome da Nova Marca:";
            btnCancelar.Visible = true;
            btnAlterarMarca.Text = "Confirmar";
            lblMracaID.Text ="0";
            return;
        }

        if (btnAlterarMarca.Text == "Confirmar")
        {
            new SistranBLL.Veiculo.Marca().AlterarIncluir(txtMarcaSelecionada.Text.Trim(), lblMracaID.Text == "" ? 0 : Convert.ToInt32(lblMracaID.Text));
            IniciarComportamentoMarca();
            CarregarMarcas();
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        IniciarComportamentoMarca();
    }

    private void IniciarComportamentoMarca()
    {
        txtMarcaSelecionada.Text = "";
        txtMarcaSelecionada.Visible = false;
        btnAlterarMarca.Text = "Novo";
        lblMracaSelecionada.Text = "";
        btnCancelar.Visible = false;
        lblMracaID.Text = "0";
        PHModelo.Controls.Clear();
        pnlModelo.Visible = false;
        lblTitulo1.Visible = false;
    }

    private void IniciarComportamentoModelo()
    {
        txtModeloSelecionada.Text = "";
        txtModeloSelecionada.Visible = false;
        btnAlterarMarca0.Text = "Novo";
        lblModeloSelecionado.Text = "";
        btnCancelar0.Visible = false;
        lblModeloID.Text = "0";

        carregarModelos(lblMracaID.Text);
    }

    protected void btnAlterarMarca0_Click(object sender, EventArgs e)
    {
        if (btnAlterarMarca0.Text == "Novo")
        {
            txtModeloSelecionada.Visible = true;
            txtModeloSelecionada.Text = "";
            txtModeloSelecionada.Focus();
            lblModeloSelecionado.Text = "Digite o Nome da Nova Marca:";
            btnCancelar0.Visible = true;
            btnAlterarMarca0.Text = "Confirmar";
            lblModeloID.Text = "0";
            return;
        }

        if (btnAlterarMarca0.Text == "Confirmar")
        {
            new SistranBLL.Veiculo.Modelo().AlterarIncluir(txtModeloSelecionada.Text, Convert.ToInt32(lblMracaID.Text), lblModeloID.Text == "" ? 0 : Convert.ToInt32(lblModeloID.Text));
            IniciarComportamentoModelo();
            carregarModelos(lblMracaID.Text);
        }
    }

    protected void btnCancelar0_Click(object sender, EventArgs e)
    {
        IniciarComportamentoModelo();
    }
}
