using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;
using System.Collections.Generic;

public partial class Intranet_frmCadMotorista : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        
        if (!IsPostBack)
        {
            if (Session["dtFoto"] != null)
            {
                Session["dtFoto"] = null;
                Session["lblId"] = null;
            }


            if (Request.QueryString["novo"] != null)
            {
                lblTitulo.Text = "Novo Motorista";
                Session["lblId"] = "novo";
            }
            else
            {
                lblTitulo.Text = "Cadastro de Motorista";
                Session["lblId"] = Request.QueryString["IDMOTORISTA"];
            }

         
            
            //RadTabStrip1.SelectedIndex = 1;
            RadTabStrip1.SelectedIndex = 0;
            rmp.PageViews[1].Selected = true;
            rmp.PageViews[0].Selected = true;

            cboCidade.Items.Clear();
            cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
            cboCidadeNascimento.Items.Clear();
            cboCidadeNascimento.Items.Insert(0, (new ListItem("SELECIONE O ESTADO DE NASCIMENTO", "0")));
            CarregarCboEstado();
            CarregarCboEstadoNascimento();
            CarregarListaFilial();

            if (Request.QueryString["IDMOTORISTA"] != null)
            {
                preencherCampos();
                ctrHistoricos.IDS = Convert.ToInt32(Request.QueryString["IDMOTORISTA"]);
                 
            }            
        }
       //txtcpfcnpj.Attributes.Add("onkeypress", "CopiarDados("+ txtcpfcnpj.ClientID +", "+ txtcpfcnpjFavorecido.ClientID +");");
        }
        catch (Exception exe)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + exe.Message.Replace("'", "´") + "')", true);         
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            salvar();
        }
        catch (Exception exe)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + exe.Message.Replace("'", "´") + "')", true);

        }
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
        cboCidade.Focus();

        txtcpfcnpjFavorecido.Text = FormatarCnpj(txtcpfcnpjFavorecido.Text);
        txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

        CopiarDados();
    }

    protected void cboEstadoNascimento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboEstadoNascimento.SelectedValue == lblCodEstadoNascimento.Text && cboCidadeNascimento.SelectedValue == lblCodCidadeNascimeto.Text)
        {
            return;
        }

        if (cboEstadoNascimento.SelectedValue == "0")
        {
            cboCidadeNascimento.Items.Clear();
            cboCidadeNascimento.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
            cboCidadeNascimento.Enabled = false;
            lblCodEstadoNascimento.Text = "";
            return;
        }
        else
        {
            lblCodEstadoNascimento.Text = cboEstadoNascimento.SelectedValue;
            lblCodCidadeNascimeto.Text = "";
            CarregarCoboCidadeNascimento(cboEstadoNascimento.SelectedValue);
        }

        cboCidadeNascimento.Focus();
        txtcpfcnpjFavorecido.Text = FormatarCnpj(txtcpfcnpjFavorecido.Text);
        txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

        CopiarDados();
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

        //Procura se ja esta cadastrado
        DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar("", FormatarCnpj(txtcpfcnpj.Text), "", "", null, "", "", false);

        txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

        if (dt.Rows.Count > 0)
        {
            preencherCampos();
        }

        if (txtcpfcnpjFavorecido.Text == "" || txtcpfcnpjFavorecido.Text.Length < 10)
        {
            CopiarDados();
        }
        txtRazao.Focus();
    }

 
    protected void txtDataNascimento_TextChanged(object sender, EventArgs e)
    {
        lblAniversario.Text = txtDataNascimento.Text.Length == 10 ? Convert.ToDateTime(txtDataNascimento.Text).ToString("dd/MM") : "";
    }

    protected void txtRazao_TextChanged(object sender, EventArgs e)
    {
        if (txtNomeFavorecido.Text == "")
        {
            txtNomeFavorecido.Text = txtRazao.Text;
        }
    }

    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstFiliaisSelecionadas.SelectedIndex == -1)
            {
                throw new Exception("Selecione um item na lista para remover.");
            }
            lstTodasFiliais.Items.Add(new ListItem(lstFiliaisSelecionadas.SelectedItem.Text, lstFiliaisSelecionadas.SelectedValue));
            lstFiliaisSelecionadas.Items.RemoveAt(lstFiliaisSelecionadas.SelectedIndex);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
       

    }

    protected void btnPosterior_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstTodasFiliais.SelectedIndex == -1)
            {
                throw new Exception("Selecione um item na lista para incluir.");
            }

            lstFiliaisSelecionadas.Items.Add(new ListItem(lstTodasFiliais.SelectedItem.Text, lstTodasFiliais.SelectedValue));
            lstTodasFiliais.Items.RemoveAt(lstTodasFiliais.SelectedIndex);

        }
        catch (Exception ex )
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Replace("'", "´" ) +"')", true);                        
        }
        
    }


    #endregion

    #region Métodos
    private void preencherCampos()
    {
        
        int? id = int.Parse(Request.QueryString["IDMOTORISTA"].ToString());
       // Session["lblId"] = lblId.Text;
        DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar("", "", "", "", id, "", "", false);
        lblId.Text = dt.Rows[0]["IDCADASTRO"].ToString();
        lblDataBloqueio.Text = (dt.Rows[0]["DataDeBloqueio"] == DBNull.Value ? "" : "Data de Bloqueio: " + Convert.ToDateTime(dt.Rows[0]["DataDeBloqueio"]).ToString("dd/MM/yyyy"));
        chkAtivo.Checked = dt.Rows[0]["ATIVO"].ToString() == "SIM" ? true : false;
        chkLiberado.Checked = dt.Rows[0]["LIBERADO"].ToString() == "SIM" ? true : false;
        txtRazao.Text = dt.Rows[0]["RAZAOSOCIALNOME"].ToString();
        txtcpfcnpj.Text = dt.Rows[0]["CNPJCPF"].ToString();
        txtFantasiaApelido.Text = dt.Rows[0]["FANTASIAAPELIDO"].ToString();
        txtIE.Text = dt.Rows[0]["InscricaoRG"].ToString();
        txtCEP.Text = dt.Rows[0]["CEP"].ToString();
        txtEndereco.Text = dt.Rows[0]["ENDERECO"].ToString();
        txtNumero.Text = dt.Rows[0]["NUMERO"].ToString();
        txtComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();
        txtDataNascimento.Text = (dt.Rows[0]["DataDeNascimento"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["DataDeNascimento"]).ToShortDateString());
        txtNomePai.Text = dt.Rows[0]["NomeDoPai"].ToString();
        txtNomeMae.Text = dt.Rows[0]["NomeDamae"].ToString();
        txtEstacocivil.Text = dt.Rows[0]["EstadoCivil"].ToString();
        txtNomeConjuge.Text = dt.Rows[0]["Conjuge"].ToString();
        txtPrimaHabilitacao.Text = (dt.Rows[0]["DataDaPrimeiraHabilitacao"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["DataDaPrimeiraHabilitacao"]).ToShortDateString());
        txtValidade.Text = (dt.Rows[0]["ValidadeDaHabilitacao"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["ValidadeDaHabilitacao"]).ToShortDateString());
        txtCarteiraHabilitacao.Text = dt.Rows[0]["CarteiraDeHabilitacao"].ToString();
        txtCategoriaHabilitacao.Text = dt.Rows[0]["Categoria"].ToString();
        txtRoubo.Text = dt.Rows[0]["VitimaDeRouboQuantidade"].ToString();
        txtAcidente.Text = dt.Rows[0]["SofreuAcidadeQuantidade"].ToString();
        txtAliquota.Text = dt.Rows[0]["AliquotaSestSenat"].ToString();
        txtPancary.Text = (dt.Rows[0]["VencimentoPancary"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["VencimentoPancary"]).ToShortDateString());
        txtBuonny.Text = (dt.Rows[0]["VencimentoBuonny"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["VencimentoBuonny"]).ToShortDateString());
        txtBrasilRisk.Text = (dt.Rows[0]["VencimentoBrasilrisk"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["VencimentoBrasilrisk"]).ToShortDateString());
        txtPancard.Text = dt.Rows[0]["NumeroPancard"].ToString().ToUpper();
        cboMoop.SelectedValue = dt.Rows[0]["MOPP"].ToString().ToUpper();

        if (dt.Rows[0]["CarregamentoAutorizadoAte"] != DBNull.Value && dt.Rows[0]["CarregamentoAutorizadoAte"].ToString() != "")
            txtCarregamentoAutor.Text = Convert.ToDecimal(dt.Rows[0]["CarregamentoAutorizadoAte"]).ToString("#0.00");

        DataTable dtFone = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().RetornarTelefone(Convert.ToInt32(Request.QueryString["IDMOTORISTA"]));

        foreach (DataRow item in dtFone.Rows)
        {
            if (item["Nome"].ToString() == "TELEFONE DE RECADO")
            {
                txtFoneRecado.Text = item["ENDERECO"].ToString();
                idtelefoneRec.Value = item["IDCadastroContatoEndereco"].ToString();
            }

            if (item["Nome"].ToString() == "TELEFONE RESIDENCIAL")
            {
                txtFoneResidencial.Text = item["ENDERECO"].ToString();
                idtelefoneRes.Value = item["IDCadastroContatoEndereco"].ToString();
            }

            if (item["Nome"].ToString() == "CELULAR")
            {
                txtFoneCelular.Text = item["ENDERECO"].ToString();
                idtelefoneCel.Value = item["IDCadastroContatoEndereco"].ToString();
            }

            if (item["Nome"].ToString() == "NEXTEL")
            {
                txtFoneNextel.Text = item["ENDERECO"].ToString();
                idtelefoneNextel.Value = item["IDCadastroContatoEndereco"].ToString();
            }
        }

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

        CarregarCboEstadoNascimento();

        if (dt.Rows[0]["IDCidadeNascimento"].ToString().Trim().Length > 0)
        {
            DataTable dtCidadeNascimento = new Localizacao.Cidade().Read(Convert.ToInt32(dt.Rows[0]["IDCidadeNascimento"].ToString()));

            if (dtCidadeNascimento.Rows.Count > 0)
            {
                CarregarCoboCidadeNascimento(dtCidadeNascimento.Rows[0]["IDESTADO"].ToString());
                cboEstadoNascimento.SelectedValue = dtCidadeNascimento.Rows[0]["IDESTADO"].ToString();
                cboCidadeNascimento.SelectedValue = dtCidadeNascimento.Rows[0]["IDCIDADE"].ToString();
                cboCidadeNascimento.Enabled = true;
                CarregarCoboCidadeNascimento(dtCidadeNascimento.Rows[0]["IDESTADO"].ToString());
                lblCodEstadoNascimento.Text = dtCidadeNascimento.Rows[0]["IDESTADO"].ToString();
                lblCodCidadeNascimeto.Text = dtCidadeNascimento.Rows[0]["IDCIDADE"].ToString();
            }
        }

        DataTable dtCadCompl = new SistranBLL.Cadastro.CadastroComplemento().readByIdCadastro(Convert.ToInt32(dt.Rows[0]["IDCADASTRO"]));

        foreach (DataRow item in dtCadCompl.Rows)
        {
            lblAniversario.Text = item["Aniversario"].ToString();
            txtDependentes.Text = item["Dependentes"].ToString();
            txtNumeroBanco.Text = item["BANCO"].ToString();
            txtAgencia.Text = item["Agencia"].ToString();
            txtAgenciaDig.Text = item["AgenciaDigito"].ToString();
            txtConta.Text = item["Conta"].ToString();
            txtContaDig.Text = item["ContaDigito"].ToString();
            cboTipoConta.SelectedValue = item["TipoConta"].ToString();
            txtcpfcnpjFavorecido.Text = item["CnpjCpfFavorecido"].ToString();
            txtNomeFavorecido.Text = item["NomeFavorecido"].ToString();
            txtInscricaoInss.Text = item["InscricaoNoInss"].ToString();
            txtPIS.Text = item["InscricaoPIS"].ToString();
            txtPensaoAlimenticia.Text = Convert.ToDecimal(item["ValorPensaoAlimenticia"].ToString() == "" ? "0" : item["ValorPensaoAlimenticia"].ToString()).ToString("#0.00");
            txtVinculoComFavorecido.Text = item["VinculoFavorecido"].ToString();
            cboAposentado.SelectedValue = item["Aposentado"].ToString();
            txtDataExpedicao.Text = (item["DataExpedicaoRG"] == DBNull.Value ? "" : Convert.ToDateTime(item["DataExpedicaoRG"]).ToString("dd/MM/yyyy"));
            cboContratado.SelectedValue = item["Contratado"].ToString();
            txtOrgaoExpedidor.Text = item["OrgaoExpedicaoRG"].ToString();
            txtUltimaDataDeComprovacao.Text = (item["UltimaComprovacaoEndereco"]==DBNull.Value?"":Convert.ToDateTime(item["UltimaComprovacaoEndereco"]).ToString("dd/MM/yyyy"));
        }
    }

    private void CarregarListaFilial()
    {
        if (Request.QueryString["IDMOTORISTA"] != null)
        {
            lstFiliaisSelecionadas.DataSource = new SistranBLL.Filial().ListarSelecionadosByIDMotorista("", int.Parse(Request.QueryString["IDMOTORISTA"].ToString()));
            lstFiliaisSelecionadas.DataTextField = "NOME";
            lstFiliaisSelecionadas.DataValueField = "IDFILIAL";
            lstFiliaisSelecionadas.DataBind();

            lstTodasFiliais.DataSource = new SistranBLL.Filial().ListarDisponiveisByIDMotorista("", int.Parse(Request.QueryString["IDMOTORISTA"].ToString()));            
        }
        else
        {
            lstTodasFiliais.DataSource = new SistranBLL.Filial().ListarDisponiveisByIDMotorista("", 0);
        }
        lstTodasFiliais.DataTextField = "NOME";
        lstTodasFiliais.DataValueField = "IDFILIAL";
        lstTodasFiliais.DataBind();
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

    private void CarregarCoboCidadeNascimento(string idEstado)
    {
        cboCidadeNascimento.Items.Clear();
        cboCidadeNascimento.DataSource = new Localizacao.Cidade().ReadbyIdEstado(Convert.ToInt32(idEstado));
        cboCidadeNascimento.DataTextField = "NOME";
        cboCidadeNascimento.DataValueField = "IDCIDADE";
        cboCidadeNascimento.DataBind();
        cboCidadeNascimento.Items.Insert(0, (new ListItem("SELECIONE", "0")));
        cboCidadeNascimento.Enabled = true;
    }

    private void CopiarDados()
    {
        txtcpfcnpjFavorecido.Text = txtcpfcnpj.Text;
        txtNomeFavorecido.Text = txtRazao.Text;
        if (Request.QueryString["novo"] != null)
        {
            lblAniversario.Text = txtDataNascimento.Text.Length == 10 ? Convert.ToDateTime(txtDataNascimento.Text).ToString("dd/MM") : "";
            txtVinculoComFavorecido.Text = "O MESMO";
        }
    }

    private void salvar()
    {
        try
        {
            if (txtObservacao.Text.Trim().Length == 0)
            {
                RadTabStrip1.SelectedIndex = 1;
                subPgDadosBancarios.Selected = true;
                txtObservacao.BorderColor = System.Drawing.Color.Red;
                txtObservacao.Focus();
                throw new Exception("Informe o motivo da alteração no campo Observações.");

            }

            if (cboCidade.SelectedIndex == 0 || cboEstado.SelectedIndex == 0)
            {
                cboEstado.Focus();
                throw new Exception("Selecione Cidade / Estado");
            }


            if (cboCidadeNascimento.SelectedIndex == 0 || cboEstadoNascimento.SelectedIndex == 0)
            {
                cboEstadoNascimento.Focus();
                throw new Exception("Selecione Cidade / Estado de Nascimento");
            }

            if (txtcpfcnpj.Text != txtcpfcnpjFavorecido.Text || txtRazao.Text != txtNomeFavorecido.Text)
            {
                if (txtVinculoComFavorecido.Text.Trim()=="")
                {
                    cboEstadoNascimento.Focus();
                    throw new Exception("Preencha o vinculo com o favorecido.");
                }
            }

            txtcpfcnpjFavorecido.Text = FormatarCnpj(txtcpfcnpjFavorecido.Text);
            txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

            lblAniversario.Text = txtDataNascimento.Text.Length == 10 ? Convert.ToDateTime(txtDataNascimento.Text).ToString("dd/MM") : "";

            SistranMODEL.Cadastro oCad = new SistranMODEL.Cadastro();
            SistranMODEL.Cadastro.Motorista oMot = new SistranMODEL.Cadastro.Motorista();
            SistranMODEL.Cadastro.CadastroComplemento oCadComp = new SistranMODEL.Cadastro.CadastroComplemento();
            SistranMODEL.Cadastro.Motorista.MotoristaHistorico oMotHst = new SistranMODEL.Cadastro.Motorista.MotoristaHistorico();
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)System.Web.HttpContext.Current.Session["USUARIO"];


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

            oCadComp.Agencia = txtAgencia.Text;
            oCadComp.AgenciaDigito = txtAgenciaDig.Text;
            oCadComp.Aniversario = lblAniversario.Text;
            oCadComp.Banco = txtNumeroBanco.Text;
            oCadComp.CnpjCpfFavorecido = FormatarCnpj(txtcpfcnpjFavorecido.Text);
            oCadComp.Conta = txtConta.Text;
            oCadComp.ContaDigito = txtContaDig.Text;
            oCadComp.Dependentes = txtDependentes.Text == "" ? 0 : Convert.ToInt32(txtDependentes.Text);
            oCadComp.IDCadastro = lblId.Text == "" ? 0 : Convert.ToInt32(lblId.Text);
            oCadComp.InscricaoNoInss = txtInscricaoInss.Text;
            oCadComp.InscricaoPIS = txtPIS.Text;
            oCadComp.NomeFavorecido = txtNomeFavorecido.Text.ToUpper();
            oCadComp.TipoConta = cboTipoConta.SelectedItem.Text;
            oCadComp.ValorPensaoAlimenticia = txtPensaoAlimenticia.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtPensaoAlimenticia.Text);
            oCadComp.VinculoFavorecido = txtVinculoComFavorecido.Text.ToUpper().Trim();
            oCadComp.Aposentado = cboAposentado.SelectedItem.Text;
            oCadComp.DataExpedicaoRG = Convert.ToDateTime(txtDataExpedicao.Text);
            oCadComp.Contratado = cboContratado.SelectedItem.Text;
            oCadComp.OrgaoExpedicaoRG = txtOrgaoExpedidor.Text.ToUpper();
            oCadComp.UltimaComprovacaoEndereco = Convert.ToDateTime(txtUltimaDataDeComprovacao.Text);
            
            oMot.IDMotorista = Convert.ToInt32(lblId.Text);
            oMot.Liberado = "NAO";
            oMot.Ativo = chkAtivo.Checked == true ? "SIM" : "NAO";
            oMot.CarteiraDeHabilitacao = txtCarteiraHabilitacao.Text;
            oMot.ValidadeDaHabilitacao = Convert.ToDateTime(txtValidade.Text);
            oMot.DataDaPrimeiraHabilitacao = Convert.ToDateTime(txtPrimaHabilitacao.Text);
            oMot.Categoria = txtCategoriaHabilitacao.Text;
            oMot.DataDeNascimento = Convert.ToDateTime(txtDataNascimento.Text);
            oMot.IDCidadeNascimento = cboCidadeNascimento.SelectedValue;
            oMot.NomeDaMae = txtNomeMae.Text;
            oMot.NomeDoPai = txtNomePai.Text;
            oMot.Conjuge = txtNomeConjuge.Text;
            oMot.VitimaDeRouboQuantidade = Convert.ToInt32(txtRoubo.Text);
            oMot.SofreuAcidadeQuantidade = Convert.ToInt32(txtAcidente.Text);
            oMot.EstadoCivil = txtEstacocivil.Text;

            if (txtPancary.Text != "")
                oMot.VencimentoPancary = Convert.ToDateTime(txtPancary.Text);

            if (txtBuonny.Text != "")
                oMot.VencimentoBuonny = Convert.ToDateTime(txtBuonny.Text);

            if (txtBrasilRisk.Text != "")
                oMot.VencimentoBrasilrisk = Convert.ToDateTime(txtBrasilRisk.Text);


            oMot.AliquotaSestSenat = Convert.ToDecimal(txtAliquota.Text);
            oMot.CarregamentoAutorizadoAte = txtCarregamentoAutor.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCarregamentoAutor.Text);
            oMot.MOOP = cboMoop.SelectedValue;
            oMot.NumeroPancard = txtPancard.Text;

            oMot.TelefoneCel = txtFoneCelular.Text;
            oMot.TelefoneRecado = txtFoneRecado.Text;
            oMot.TelefoneRes = txtFoneResidencial.Text;
            oMot.TelefoneNextel = txtFoneNextel.Text;

            oMot.IDTelefoneCel = idtelefoneCel.Value;
            oMot.IDTelefoneRecado = idtelefoneRec.Value;
            oMot.IDTelefoneRes = idtelefoneRes.Value;
            oMot.IDTelefoneNextel = idtelefoneNextel.Value;


            if (lblId.Text == "" || lblId.Text == "0")
            {
                oMotHst.Historico = txtObservacao.Text.ToUpper().Trim();                
                oMotHst.IDUsuario = ILusuario[0].UsuarioId;
                lblId.Text = new SistranBLL.Cadastro().TransacaoInserirCadastroMotorista("", oCad, oMot, oMotHst).ToString();
                oCadComp.IDCadastro = int.Parse(lblId.Text);
                int m = new SistranBLL.Cadastro.CadastroComplemento().inserir(oCadComp);
                new SistranBLL.Cadastro.Motorista.MotoristaFilial().Inserir(m, lstFiliaisSelecionadas);
                
            }
            else
            {
                new SistranBLL.Cadastro().Alterar(oCad.CnpjCpf, oCad.InscricaoRG, txtRazao.Text.ToUpper(), txtFantasiaApelido.Text.ToUpper(), txtEndereco.Text.ToUpper(), txtNumero.Text, oCad.Complemento, oCad.IDCidade.ToString(), "", oCad.Cep, lblId.Text);
                new SistranBLL.Cadastro.Motorista().alterar(oMot, Convert.ToInt32(lblId.Text));
                oCadComp.IDCadastro = int.Parse(lblId.Text);
                int m = new SistranBLL.Cadastro.CadastroComplemento().inserir(oCadComp);
                oMotHst.Historico = txtObservacao.Text.ToUpper().Trim();
                oMotHst.IdMotorista = Convert.ToInt32(lblId.Text);
                oMotHst.IDUsuario = ILusuario[0].UsuarioId;
                new SistranBLL.Cadastro.Motorista.MotoristaHistorico().inserir(oMotHst);
                new SistranBLL.Cadastro.Motorista.MotoristaFilial().Inserir(int.Parse(lblId.Text), lstFiliaisSelecionadas);
            }

            if (chkProprietario.Checked)
            {
                string sql = "IF NOT EXISTS(SELECT * FROM PROPRIETARIO WHERE IDPROPRIETARIO=" + lblId.Text + ") INSERT INTO PROPRIETARIO (IDPROPRIETARIO) VALUES(" + lblId.Text + ")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, "");
            }


            if (Request.QueryString["LIB"] == null)
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Aler", "alert('Operação Efetuada com Sucesso.'); window.location.href='frmListMotorista.aspx' ", true);
            else
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Aler", "alert('Operação Efetuada com Sucesso.'); window.location.href='frmListLiberarMotorista.aspx' ", true);
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
        cboEstado.DataTextField = "UF";
        cboEstado.DataValueField = "IDESTADO";
        cboEstado.DataBind();
        cboEstado.Items.Insert(0, new ListItem("SELECIONE", "0"));
        cboCidade.Enabled = true;
    }

    private void CarregarCboEstadoNascimento()
    {
        cboEstadoNascimento.DataSource = new SistranBLL.Localizacao.Estado().Listar();
        cboEstadoNascimento.DataTextField = "UF";
        cboEstadoNascimento.DataValueField = "IDESTADO";
        cboEstadoNascimento.DataBind();
        cboEstadoNascimento.Items.Insert(0, new ListItem("SELECIONE", "0"));
        cboCidadeNascimento.Enabled = true;
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
