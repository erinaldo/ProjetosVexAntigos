using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;

public partial class Intranet_frmPOPCadProprietario : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["novo"] != null)
        {
            lblTitulo.Text = "Novo Proprietário";
        }
        else
        {
            lblTitulo.Text = "Cadastro de Proprietário";
        }


        if (!IsPostBack)
        {
            CarregarCboEstado();
            cboCidade.Items.Clear();
            cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
            
            if (Request.QueryString["IDproprietario"] !=null)
            {
                CarregarCampos();
            }
            
        }
    }

    private void CarregarCampos()
    {
        int? id = int.Parse(Request.QueryString["IDPROPRIETARIO"].ToString());
        DataTable dt = new SistranBLL.Cadastro.Proprietario().Pesquisar("","",id, null);
        preencherCampos(dt);

    }

    private void preencherCampos(DataTable dt)
    {
        lblId.Text = dt.Rows[0]["IDPROPRIETARIO"].ToString();

        txtRazao.Text = dt.Rows[0]["RAZAOSOCIALNOME"].ToString();
        txtcpfcnpj.Text = dt.Rows[0]["CNPJCPF"].ToString();
        txtFantasiaApelido.Text = dt.Rows[0]["FANTASIAAPELIDO"].ToString();
        txtIE.Text = dt.Rows[0]["FANTASIAAPELIDO"].ToString();
        txtCEP.Text = dt.Rows[0]["CEP"].ToString();
        txtEndereco.Text = dt.Rows[0]["ENDERECO"].ToString();
        txtNumero.Text = dt.Rows[0]["NUMERO"].ToString();
        txtComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();
        
        
        CarregarCboEstado();

        if (dt.Rows[0]["IDCIDADE"].ToString().Trim().Length > 0)
        {
            DataTable dtCidade = new Localizacao.Cidade().Read(Convert.ToInt32(dt.Rows[0]["IDCIDADE"].ToString()));

            if (dtCidade.Rows.Count > 0)
            {
                CarregarCoboCidade(dtCidade.Rows[0]["IDESTADO"].ToString());

                cboEstado.SelectedValue = dtCidade.Rows[0]["IDESTADO"].ToString();
                cboCidade.SelectedValue = dtCidade.Rows[0]["IDCIDADE"].ToString();
                cboCidade.Enabled = true;
                CarregarCoboCidade(dtCidade.Rows[0]["IDESTADO"].ToString());
                lblCodEstado.Text = dtCidade.Rows[0]["IDESTADO"].ToString();
                lblCodCidade.Text = dtCidade.Rows[0]["IDCIDADE"].ToString();
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        salvar();
        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "ssss", "javascript:window.opener.document.getElementById('" + Request["controle"] + "').value = '" + lblId.Text + "'; window.opener.document.getElementById('" + Request["controleCpf"] + "').value = '" + txtcpfcnpj.Text + "'; window.opener.document.getElementById('" + Request["controleNome"] + "').value = '" + txtRazao.Text + "';window.close();", true);

    }

    protected void txtCPF_TextChanged(object sender, EventArgs e)
    {
        txtcpfcnpj.Text = FuncoesGerais.FormatarCnpj(txtcpfcnpj.Text);
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
            CarregarCoboCidade(cboEstado.SelectedValue);
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
        cboCidade.Enabled = true;
    }


    protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCidade.SelectedValue == "0")
        {
            lblCodCidade.Text = "";
            return;
        }

        else
        {
            lblCodCidade.Text = cboCidade.SelectedValue;

        }
    }

    protected void txtcpfcnpj_TextChanged(object sender, EventArgs e)
    {
        txtcpfcnpj.Text = FuncoesGerais.FormatarCnpj(txtcpfcnpj.Text);
        if (FormatarCnpj(txtcpfcnpj.Text) == "")
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('CPF / CNPJ inválido.')", true);
            return;
        }

        DataTable dt = new SistranBLL.Cadastro.Proprietario().Pesquisar("", FormatarCnpj(txtcpfcnpj.Text), null, null);
        txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

        if (dt.Rows.Count > 0)
        {
            preencherCampos(dt);
        }
        txtRazao.Focus();
    }
    
    #endregion

    #region Metodos and Helpers

    private void salvar()
    {
        try
        {
            if (cboCidade.SelectedIndex == 0 || cboEstado.SelectedIndex == 0)
            {
                cboEstado.Focus();
                throw new Exception("Selecione Cidade / Estado");
            }


            SistranMODEL.Cadastro oCad = new SistranMODEL.Cadastro();
            SistranMODEL.Cadastro.Proprietario oProp = new SistranMODEL.Cadastro.Proprietario();


            oCad.IDCadastro = Convert.ToInt32(lblId.Text);
            oCad.CnpjCpf = txtcpfcnpj.Text;
            oCad.InscricaoRG = txtIE.Text;
            oCad.FantasiaApelido = txtFantasiaApelido.Text;
            oCad.Endereco = txtEndereco.Text;
            oCad.Numero = txtNumero.Text;
            oCad.Complemento = txtComplemento.Text;
            oCad.Cep = txtCEP.Text;
            oCad.IDCidade = Convert.ToInt32(cboCidade.SelectedValue);
            oCad.RazaoSocialNome = txtRazao.Text;
            oCad.IDBairro = 0;

            oProp.IDProprietario = Convert.ToInt32(lblId.Text);

            if (lblId.Text == "" || lblId.Text == "0")
            {
                lblId.Text = new SistranBLL.Cadastro().TransacaoInserirCadastroProprietario("", oCad, oProp).ToString();
            }
            else
            {
                new SistranBLL.Cadastro().Alterar(oCad.CnpjCpf, oCad.InscricaoRG, txtRazao.Text.ToUpper(), txtFantasiaApelido.Text.ToUpper(), txtEndereco.Text.ToUpper(), txtNumero.Text, oCad.Complemento, oCad.IDCidade.ToString(), "", oCad.Cep, lblId.Text);
            }
            throw new Exception("Operação Realizada com Sucesso");


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
            return;
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

    #endregion    
}
