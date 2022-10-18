using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;

public partial class Intranet_frmCadVeiculo : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["novo"] != null)
            {
                lblTitulo.Text = "Novo Veículo";
            }
            else
            {
                lblTitulo.Text = "Cadastro de Veículo";
            }


            if (!IsPostBack)
            {
                imgProcurarMotorista.Attributes.Add("onClick", "javascript:NewWindow('frmPOPListMotorista.aspx?controle=" + lblIdMotorista.ClientID + "&controleCpf=" + txtCPFMotorista.ClientID + "&controleNome=" + txtMotoristaNome.ClientID + "','new','width=600px,height=350px,scrollbars=yes')");
                imgProcurarMotorista.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");


                imgProcurarProprietario.Attributes.Add("onClick", "javascript:NewWindow('frmPOPListProprietario.aspx?controle=" + lblProprietario.ClientID + "&controleCpf=" + txtCPFProprietario.ClientID + "&controleNome=" + txtMotoristaProprietario.ClientID + "','new','width=600px,height=350px,scrollbars=yes')");
                imgProcurarProprietario.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");

                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboModelo.Items.Insert(0, (new ListItem("SELECIONE UMA MARCA", "0")));
                CarregarCboEstado();
                CarregarComboMarca();
                CarregarComboTipo();
                CarregarComboRastreador();

                if (Request.QueryString["idveiculo"] != null)
                {
                    CarregarCampos();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
            return;
        }
    }

    private void CarregarComboRastreador()
    {
        cboRastreador.Items.Clear();
        cboRastreador.DataSource = new SistranBLL.Veiculo.Rastreador().Listar();
        cboRastreador.DataTextField = "Nome";
        cboRastreador.DataValueField = "IDVEICULORASTREADOR";
        cboRastreador.DataBind();
        cboRastreador.Items.Insert(0, (new ListItem("SELECIONE", "0")));
    }

    private void CarregarComboTipo()
    {
        cboTipo.Items.Clear();
        cboTipo.DataSource = new SistranBLL.Veiculo.Tipo().Listar();
        cboTipo.DataTextField = "Nome";
        cboTipo.DataValueField = "IDVEICULOTIPO";
        cboTipo.DataBind();
        cboTipo.Items.Insert(0, (new ListItem("SELECIONE", "0")));
        Session["CategoriaPermitida"] = (DataTable)cboTipo.DataSource;
    }

    private void CarregarComboModelo()
    {
        cboModelo.Items.Clear();
        cboModelo.DataSource = new SistranBLL.Veiculo.Modelo().Listar(cboMarca.SelectedValue);
        cboModelo.DataTextField = "Nome";
        cboModelo.DataValueField = "IDVeiculoModelo";
        cboModelo.DataBind();

        if (cboModelo.Items.Count == 0)
        {
            cboModelo.Items.Insert(0, (new ListItem("NENHUM MODELO CADASTRADO", "0")));
        }
        else
        {
            cboModelo.Items.Insert(0, (new ListItem("SELECIONE", "0")));
        }
    }

    private void CarregarComboMarca()
    {
        cboMarca.Items.Clear();
        cboMarca.DataSource = new SistranBLL.Veiculo.Marca().Listar();
        cboMarca.DataTextField = "Nome";
        cboMarca.DataValueField = "IDVeiculoMarca";
        cboMarca.DataBind();
        cboMarca.Items.Insert(0, (new ListItem("SELECIONE", "0")));
    }

    private void CarregarCampos()
    {
        int? id = int.Parse(Request.QueryString["IDVEICULO"].ToString());
        DataTable dt = new SistranBLL.Veiculo().Pesquisar(id, "", false, false);
        preencherCampos(dt);

    }

    private void preencherCampos(DataTable dt)
    {
        lblId.Text = dt.Rows[0]["IDVeiculo"].ToString();
        txtPlaca.Text = dt.Rows[0]["Placa"].ToString();
        txtAntt.Text = dt.Rows[0]["Antt"].ToString();
        txtChassi.Text = dt.Rows[0]["Chassi"].ToString();
        txtRenavan.Text = dt.Rows[0]["Renavam"].ToString();

        txtCPFMotorista.Text = dt.Rows[0]["CPFMOTORISTA"].ToString();
        txtMotoristaNome.Text = dt.Rows[0]["NOMEMOTORISTA"].ToString();
        lblIdMotorista.Text = dt.Rows[0]["IDMOTORISTA"].ToString();

        txtCPFProprietario.Text = dt.Rows[0]["CPFPROPRIETARIO"].ToString();
        txtMotoristaProprietario.Text = dt.Rows[0]["NOMEPROPRIETARIO"].ToString();
        lblProprietario.Text = dt.Rows[0]["IDPROPRIETARIO"].ToString();

        cboMarca.Items.Clear();
        CarregarComboMarca();
        cboMarca.SelectedValue = dt.Rows[0]["IDVEICULOMARCA"].ToString();

        cboModelo.Items.Clear();
        CarregarComboModelo();
        cboModelo.SelectedValue = dt.Rows[0]["IDVEICULOMODELO"].ToString();

        txtano.Text = dt.Rows[0]["ANO"].ToString();
        txtanoModelo.Text = dt.Rows[0]["AnoModelo"].ToString();
        txtCor.Text = dt.Rows[0]["COR"].ToString();

        cboTipo.SelectedValue = dt.Rows[0]["IDVEICULOTIPO"].ToString();
        txteixos.Text = dt.Rows[0]["QUATIDADEDEEIXOS"].ToString();

        txtCapacidade.Text = dt.Rows[0]["CAPACIDADEDECARGAM3"].ToString();
        txtCapacidadeKG.Text = dt.Rows[0]["CAPACIDADEDECARGAKG"].ToString();

        //cboCategorias.SelectedValue = dt.Rows[0]["CATEGORIASDECNHPERMITIDAS"].ToString();
        cboRastreador.SelectedValue = dt.Rows[0]["IDVEICULORASTREADOR"].ToString();
        txtNumeroSerieEquipamento.Text = dt.Rows[0]["NUMEROSERIEEQUIPAMENTO"].ToString();
        txtVencimentoAntt.Text = dt.Rows[0]["ANTTVENCIMENTO"]==DBNull.Value?"":Convert.ToDateTime(dt.Rows[0]["ANTTVENCIMENTO"]).ToShortDateString();
        txtDataLicenciamento.Text = dt.Rows[0]["DATADELICENCIAMENTO"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["DATADELICENCIAMENTO"]).ToShortDateString();


        lblDataLimite.Text = dt.Rows[0]["DATALIMITE"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["DATALIMITE"]).ToShortDateString();

        
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
        try
        {
            if (lblIdMotorista.Text == "" || lblIdMotorista.Text == "0")
            {
                throw new Exception("Informe o Motorista");
            }


            if (lblProprietario.Text == "" || lblProprietario.Text == "0")
            {
                throw new Exception("Informe o Proprietário");
            }

            if (cboModelo.SelectedIndex == 0)
            {
                throw new Exception("Informe o Modelo");
            }


            if (cboTipo.SelectedIndex == 0)
            {
                throw new Exception("Informe o Tipo de Veículo");
            }

            if (cboRastreador.SelectedIndex == 0)
            {
                throw new Exception("Informe o Rastreador");
            }


            if (cboCidade.SelectedIndex == 0 || cboEstado.SelectedIndex == 0)
            {
                throw new Exception("Informe o Estado/Cidade");
            }

            salvar();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
            return;
        }

    }

    private void salvar()
    {
        try
        {
            string catPerm = "";
            string catMot = "";
            DataTable dts = (DataTable)Session["CategoriaPermitida"];
            
            DataRow[] dr = dts.Select("IDVEICULOTIPO=" + cboTipo.SelectedValue);
            catPerm = dr[0]["CATEGORIAPERMITIDA"].ToString().Trim();
            
            DataTable dtMot = new SistranBLL.Cadastro.Motorista().Pesquisar(txtCPFMotorista.Text);

            if (dtMot.Rows.Count == 0)
                throw new Exception("Erro ao selecionar o motorista");

            catMot = dtMot.Rows[0]["Categoria"].ToString().Trim();


            if (lblId.Text == "" || lblId.Text == "0")
            {
                lblId.Text = new SistranBLL.Veiculo().Inserir(Convert.ToInt32(cboModelo.SelectedValue), Convert.ToInt32(cboTipo.SelectedValue), Convert.ToInt32(cboRastreador.SelectedValue), Convert.ToInt32(cboCidade.SelectedValue),
                                                              Convert.ToInt32(lblProprietario.Text), Convert.ToInt32(lblIdMotorista.Text), txtPlaca.Text.ToUpper(), txtRenavan.Text.ToUpper(),
                                                              txtChassi.Text.ToUpper(), Convert.ToInt32(txtano.Text), txtCor.Text.ToUpper(), txtCapacidadeKG.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidadeKG.Text), txtCapacidade.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidade.Text),
                                                              txteixos.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txteixos.Text), catPerm, txtAntt.Text.ToUpper(), txtNumeroSerieEquipamento.Text.ToUpper(), Convert.ToDateTime(txtVencimentoAntt.Text), Convert.ToDateTime(txtDataLicenciamento.Text), cboMarca.SelectedItem.Text, txtanoModelo.Text).ToString();
            }
            else
            {
                new SistranBLL.Veiculo().Update(Convert.ToInt32(lblId.Text), Convert.ToInt32(cboModelo.SelectedValue), Convert.ToInt32(cboTipo.SelectedValue), Convert.ToInt32(cboRastreador.SelectedValue), Convert.ToInt32(cboCidade.SelectedValue),
                                                              Convert.ToInt32(lblProprietario.Text), Convert.ToInt32(lblIdMotorista.Text), txtPlaca.Text.ToUpper(), txtRenavan.Text.ToUpper(),
                                                              txtChassi.Text.ToUpper(), Convert.ToInt32(txtano.Text), txtCor.Text.ToUpper(), txtCapacidadeKG.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidadeKG.Text), txtCapacidade.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidade.Text),
                                                              txteixos.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txteixos.Text), catPerm, txtAntt.Text.ToUpper(), txtNumeroSerieEquipamento.Text.ToUpper(), Convert.ToDateTime(txtVencimentoAntt.Text), Convert.ToDateTime(txtDataLicenciamento.Text), cboMarca.SelectedItem.Text, txtanoModelo.Text);
            }

            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Aler", "alert('Operação Efetuada com Sucesso.'); window.location.href='frmListVeiculo.aspx' ", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
            return;
        }
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

    protected void txtMotorista_TextChanged(object sender, EventArgs e)
    {
        txtCPFMotorista.Text = FuncoesGerais.FormatarCnpj(txtCPFMotorista.Text);

        DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar(txtCPFMotorista.Text);
        if (dt.Rows.Count > 0)
        {
            txtMotoristaNome.Text = dt.Rows[0]["RAZAOSOCIALNOME"].ToString();
            lblIdMotorista.Text = dt.Rows[0]["IDMOTORISTA"].ToString();
        }
        else
        {
            txtMotoristaNome.Text = "";
            lblIdMotorista.Text = "";
            txtCPFMotorista.Text = "";
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "POPUP", "javascript:NewWindow('frmPOPListMotorista.aspx?controle=" + lblIdMotorista.ClientID + "&controleCpf=" + txtCPFMotorista.ClientID + "&controleNome=" + txtMotoristaNome.ClientID + "','new','width=400px,height=450px,scrollbars=yes')", true);
        }
    }

    protected void txtMotoristaNome_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void txtCPFProprietario_TextChanged(object sender, EventArgs e)
    {
        txtCPFProprietario.Text = FuncoesGerais.FormatarCnpj(txtCPFProprietario.Text);

        DataTable dt = new SistranBLL.Cadastro.Proprietario().Pesquisar(txtCPFProprietario.Text);
        if (dt.Rows.Count > 0)
        {
            txtMotoristaProprietario.Text = dt.Rows[0]["RAZAOSOCIALNOME"].ToString();
            lblProprietario.Text = dt.Rows[0]["IDPROPRIETARIO"].ToString();
        }
        else
        {
            txtMotoristaNome.Text = "";
            lblProprietario.Text = "";
            txtCPFProprietario.Text = "";
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "POPUP", "javascript:NewWindow('frmPOPListpROPRIETARIO.aspx?controle=" + lblProprietario.ClientID + "&controleCpf=" + txtCPFProprietario.ClientID + "&controleNome=" + txtMotoristaProprietario.ClientID + "','new','width=600px,height=450px,scrollbars=yes');retun false", true);
        }
    }

    protected void txtPlaca_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPlaca.Text.Length > 0)
            {
                if (txtPlaca.Text.Length < 7)
                {
                    throw new Exception("Placa Inválida");
                }

                txtPlaca.Text = txtPlaca.Text.Replace(".", "");
                txtPlaca.Text = txtPlaca.Text.Replace("-", "");
                txtPlaca.Text = txtPlaca.Text.Replace("/", "");
                txtPlaca.Text = txtPlaca.Text.Replace(@"\", "");

                string s = txtPlaca.Text;
                string ss = "";

                ss = (s.Substring(0, 3) + "-" + s.Substring(3, 4)).ToUpper();
                txtPlaca.Text = ss;

                DataTable dtVei = new SistranBLL.Veiculo().Pesquisar(null, txtPlaca.Text, false, false);
                if (dtVei.Rows.Count > 0)
                {
                    preencherCampos(dtVei);
                    txtChassi.Focus();
                }
                txtDataLicenciamento.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
            return;
        }
    }

    protected void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarComboModelo();
    }
}