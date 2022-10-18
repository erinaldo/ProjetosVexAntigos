using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.Globalization;
using System.Threading;
using System.ComponentModel;


public partial class frmDestinatarioDetalhe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");


            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));

                cboBairro.Items.Clear();
                cboBairro.Items.Insert(0, (new ListItem("SELECIONE A CIDADE", "0")));

                if (Request.QueryString["idCadastro"].ToString().ToUpper() != "NOVO")
                {
                    CarregarCampos();
                }


                if (Request.QueryString["idCadastro"].ToString().ToUpper() == "NOVO")
                {
                    cboBairro.Enabled = false;
                    CarregarCboEstado();
                }
            }

            if (Session["dt"] == null && Request.QueryString["idCadastro"].ToString().ToUpper() != "NOVO")
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Fechar", "window.close();", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void CarregarCboEstado()
    {
        cboEstado.DataSource = new SistranBLL.Localizacao.Estado().Listar();
        cboEstado.DataTextField = "NOME";
        cboEstado.DataValueField = "IDESTADO";
        cboEstado.DataBind();
        cboEstado.Items.Insert(0, new ListItem("SELECIONE", "0"));
        cboCidade.Enabled = true;
    }

    private void CarregarCampos()
    {
        DataTable dt = (DataTable)Session["dt"];

        DataRow[] r = dt.Select("IDCADASTRO='" + Request.QueryString["idCadastro"].ToString() + "'", "");

        lblCodDestinatario.Text = r[0]["IDCadastro"].ToString().ToUpper();
        txtcpfcnpj.Text = r[0]["CnpjCpf"].ToString().ToUpper();

        txtRazao.Text = r[0]["RazaoSocialNome"].ToString().ToUpper();
        txtFantasia.Text = r[0]["FantasiaApelido"].ToString().ToUpper();
        txtCEP.Text = r[0]["Cep"].ToString().ToUpper();

        txtEndereco.Text = r[0]["Endereco"].ToString().ToUpper();
        txtNumero.Text = r[0]["Numero"].ToString().ToUpper();
        txtComplemento.Text = r[0]["Complemento"].ToString().ToUpper();
        txtIE.Text = r[0]["InscricaoRG"].ToString().ToUpper();

        CarregarCboEstado();
        DataTable dtCidade = new Localizacao.Cidade().Read(Convert.ToInt32(r[0]["IDCIDADE"].ToString()));

        if (dtCidade.Rows.Count > 0)
        {
            cboEstado.SelectedValue = dtCidade.Rows[0]["IDESTADO"].ToString();
            cboCidade.SelectedValue = dtCidade.Rows[0]["IDCIDADE"].ToString();
            cboCidade.Enabled = true;

            CarregarCoboCidade(dtCidade.Rows[0]["IDESTADO"].ToString());

            CarregarCboBairro();
            cboBairro.SelectedValue = r[0]["IDBAIRRO"].ToString();

            lblCodEstado.Text = dtCidade.Rows[0]["IDESTADO"].ToString();
            lblCodCidade.Text = dtCidade.Rows[0]["IDCIDADE"].ToString();
        }
    }

    private void CarregarCoboCidade(string idEstado)
    {
        cboCidade.Items.Clear();
        cboCidade.DataSource = new Localizacao.Cidade().ReadbyIdEstado(Convert.ToInt32(idEstado));
        cboCidade.DataTextField = "NOME";
        cboCidade.DataValueField = "IDCIDADE";
        cboCidade.DataBind();
        cboCidade.Items.Insert(0, (new ListItem("SELECIONE", "0")));
    }

    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }

    protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboEstado.SelectedValue == lblCodEstado.Text && cboCidade.SelectedValue == lblCodCidade.Text)
        {
            return;
        }

        if (cboEstado.SelectedValue == "0")
        {
            cboCidade.Items.Clear();
            cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
            cboCidade.Enabled = false;
            lblCodEstado.Text = "";
            return;
        }
        else
        {
            lblCodEstado.Text = cboEstado.SelectedValue;
            lblCodCidade.Text = "";
            cboBairro.Items.Clear();
            cboBairro.Items.Add(new ListItem("SELECIONE A CIDADE", "0"));

            cboBairro.Enabled = false;
            CarregarCoboCidade(cboEstado.SelectedValue);

        }
    }

    protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCidade.SelectedValue == "0")
        {
            cboBairro.Items.Clear();
            cboBairro.Items.Insert(0, new ListItem("SELECIONE A CIDADE", "0"));
            cboBairro.Enabled = false;
            lblCodCidade.Text = "";
            return;
        }

        else
        {
            cboBairro.Enabled = true;
            lblCodCidade.Text = cboCidade.SelectedValue;
            CarregarCboBairro();
        }
    }

    private void CarregarCboBairro()
    {
        cboBairro.DataSource = new Localizacao.Bairro().Read(Convert.ToInt32(cboCidade.SelectedValue));
        cboBairro.DataTextField = "NOME";
        cboBairro.DataValueField = "IDBAIRRO";
        cboBairro.DataBind();
        cboBairro.Items.Insert(0, new ListItem("SELECIONE O BAIRRO", "0"));
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (cboCidade.SelectedIndex == 0 || cboEstado.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert2", "alert('Informe a Cidade / Estado.')", true);
            return;
        }

        if (FormatarCnpj(txtcpfcnpj.Text) == "")
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('CPF / CNPJ inválido.')", true);
            return;
        }



        string bairro = "0";
        if (cboBairro.SelectedIndex == 0)
        {
            bairro = "";
        }
        else
        {
            bairro = cboBairro.SelectedValue;
        }

        if (Request.QueryString["idCadastro"].ToString().ToLower() == "novo")
        {

            lblCodDestinatario.Text = new SistranBLL.Cadastro().Inserir(FormatarCnpj(txtcpfcnpj.Text), txtIE.Text, txtRazao.Text, txtFantasia.Text, txtEndereco.Text, txtNumero.Text, txtComplemento.Text, cboCidade.SelectedValue, bairro, txtCEP.Text).ToString();
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Registro gravado com sucesso.')", true);
        }
        else
        {
            SistranBLL.Cadastro o = new SistranBLL.Cadastro();
            o.Alterar(FormatarCnpj(txtcpfcnpj.Text), txtIE.Text, txtRazao.Text, txtFantasia.Text, txtEndereco.Text, txtNumero.Text, txtComplemento.Text, cboCidade.SelectedValue, bairro, txtCEP.Text, lblCodDestinatario.Text);
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Registro gravado com sucesso.')", true);

            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "fechar", "window.close();", true);

        }
    }

    private string FormatarCnpj(string s)
    {
        s = s.Replace(".", "");
        s = s.Replace("-", "");
        s = s.Replace("/", "");
        s = s.Replace(@"\", "");

        if (s.Length == 0)
        {
            return "";
        }

        if (s.Length <= 11)
        {
            MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
            mtpCpf.Set(ZerosEsquerda(s, 11));
            return mtpCpf.ToString();
        }
        else
        {
            MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
            mtpCnpj.Set(ZerosEsquerda(s, 11));
            return mtpCnpj.ToString();
        }
    }

    public static string ZerosEsquerda(string strString, int intTamanho)
    {

        string strResult = "";

        for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
        {

            strResult += "0";

        }

        return strResult + strString;

    }

    protected void txtcpfcnpj_TextChanged(object sender, EventArgs e)
    {
        if (FormatarCnpj(txtcpfcnpj.Text) == "")
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('CPF / CNPJ inválido.')", true);
            return;
        }
        
        DataTable dt = new SistranBLL.Cadastro().Read("CNPJCPF='" + FormatarCnpj(txtcpfcnpj.Text) + "'");
        txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

        if (dt.Rows.Count > 0)
        {
            Verificar(dt);
        }
        txtRazao.Focus();


    }

    private void Verificar(DataTable dt)
    {
        DataRow[] r = dt.Select("0=0", "");

        lblCodDestinatario.Text = r[0]["IDCadastro"].ToString().ToUpper();
        txtcpfcnpj.Text = r[0]["CnpjCpf"].ToString().ToUpper();

        txtRazao.Text = r[0]["RazaoSocialNome"].ToString().ToUpper();
        txtFantasia.Text = r[0]["FantasiaApelido"].ToString().ToUpper();
        txtCEP.Text = r[0]["Cep"].ToString().ToUpper();

        txtEndereco.Text = r[0]["Endereco"].ToString().ToUpper();
        txtNumero.Text = r[0]["Numero"].ToString().ToUpper();
        txtComplemento.Text = r[0]["Complemento"].ToString().ToUpper();
        txtIE.Text = r[0]["InscricaoRG"].ToString().ToUpper();

        CarregarCboEstado();
        DataTable dtCidade = new Localizacao.Cidade().Read(Convert.ToInt32(r[0]["IDCIDADE"].ToString()));

        if (dtCidade.Rows.Count > 0)
        {
            cboEstado.SelectedValue = dtCidade.Rows[0]["IDESTADO"].ToString();

            lblCodEstado.Text = dtCidade.Rows[0]["IDESTADO"].ToString();
            lblCodCidade.Text = dtCidade.Rows[0]["IDCIDADE"].ToString();

            CarregarCoboCidade(dtCidade.Rows[0]["IDESTADO"].ToString());
            cboCidade.SelectedValue = dtCidade.Rows[0]["IDCIDADE"].ToString();

            cboCidade.Enabled = true;

            CarregarCboBairro();
            if (r[0]["IDBAIRRO"].ToString() != "")
                cboBairro.SelectedValue = r[0]["IDBAIRRO"].ToString();
            else
                cboBairro.SelectedValue = "0";
        }

    }

}
