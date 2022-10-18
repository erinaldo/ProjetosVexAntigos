using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;
using System.Data;

namespace Sistram.Web.Captacao
{
    public partial class frmCadastrarMotorista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                HttpContext.Current.Session["Conn"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboCidadeNascimento.Items.Clear();
                cboCidadeNascimento.Items.Insert(0, (new ListItem("SELECIONE O ESTADO DE NASCIMENTO", "0")));
                CarregarCboEstado();
                CarregarCboEstadoNascimento();
                CarregarListaFilial();

                txtUltimaDataDeComprovacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Request.QueryString["i"].ToString() != "novo")
                    carregarCampos();
                else
                {
                    txtcpfcnpj.Text = Request.QueryString["cnpj"].ToString();
                    txtRazao.Focus();
                }


                //txtCarregamentoAutor.Attributes.Add("onkeypress", "return SomenteNumero(event)");
                txtRoubo.Attributes.Add("onkeypress", "return SomenteNumero(event)");
                txtAcidente.Attributes.Add("onkeypress", "return SomenteNumero(event)");
                //txtAliquota.Attributes.Add("onkeypress", "return SomenteNumero(event)");
                txtDependentes.Attributes.Add("onkeypress", "return SomenteNumero(event)");
                txtAcidente.Attributes.Add("onkeypress", "return SomenteNumero(event)");
                txtCarteiraHabilitacao.Attributes.Add("onkeypress", "return SomenteNumero(event)");

            }
        }

        private void carregarCampos()
        {

            if (Request["i"] == "novo")
            {
                txtcpfcnpj.Focus();
                return;
            }
            DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar("", "", "", "", int.Parse(Request.QueryString["i"].ToString()), "", "", false);

            txtcpfcnpj.Text = dt.Rows[0]["CNPJCPF"].ToString();
            txtFantasiaApelido.Text = dt.Rows[0]["FANTASIAAPELIDO"].ToString();
            lblId.Text = dt.Rows[0]["IDCADASTRO"].ToString();
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
            txtSenha.Text = dt.Rows[0]["senha"].ToString();
            //txtAliquota.Text = "0";
            txtPamcary.Text = (dt.Rows[0]["VencimentoPancary"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["VencimentoPancary"]).ToShortDateString());
            txtBuonny.Text = (dt.Rows[0]["VencimentoBuonny"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["VencimentoBuonny"]).ToShortDateString());
            txtBrasilRisk.Text = (dt.Rows[0]["VencimentoBrasilrisk"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["VencimentoBrasilrisk"]).ToShortDateString());
           // txtPancard.Text = "0";
           // cboMoop.SelectedValue = "0";

            txtLocalDeExpedicao.Text = dt.Rows[0]["LocalEmissaoRG"].ToString();
            cboRecolheInss.SelectedValue = dt.Rows[0]["RecolheINSS"].ToString();
            cboRecolheIRPJ.SelectedValue = dt.Rows[0]["RecolheIRRF"].ToString();
            cboRecolheSetsSenat.SelectedValue = dt.Rows[0]["RecolheINSS"].ToString();

            //if (dt.Rows[0]["CarregamentoAutorizadoAte"] != DBNull.Value && dt.Rows[0]["CarregamentoAutorizadoAte"].ToString() != "")
                //txtCarregamentoAutor.Text = Convert.ToDecimal(dt.Rows[0]["CarregamentoAutorizadoAte"]).ToString("#0.00");

            DataTable dtFone = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().RetornarTelefone(Convert.ToInt32(Request.QueryString["i"]));

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

                }
            }

            //filial
            if (Request.QueryString["i"] != null)
            {
                DataTable dtfil = new SistranBLL.Filial().ListarSelecionadosByIDMotorista("", int.Parse(Request.QueryString["i"].ToString()));

                for (int i = 0; i < lstTodasFiliais.Items.Count; i++)
                {

                    for (int ii = 0; ii < dtfil.Rows.Count; ii++)
                    {
                        if (dtfil.Rows[ii]["IDFILIAL"].ToString() == lstTodasFiliais.Items[i].Value)
                        {
                            lstTodasFiliais.Items[i].Selected = true;
                        }
                    }

                }

            }


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

                }
            }

            DataTable dtCadCompl = new SistranBLL.Cadastro.CadastroComplemento().readByIdCadastro(Convert.ToInt32(dt.Rows[0]["IDCADASTRO"]));

            foreach (DataRow item in dtCadCompl.Rows)
            {
                //lblAniversario.Text = item["Aniversario"].ToString();
                txtDependentes.Text = item["Dependentes"].ToString();
                
                txtNumeroBanco.Text = "237";//item["BANCO"].ToString();
                
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
                txtUltimaDataDeComprovacao.Text = (item["UltimaComprovacaoEndereco"] == DBNull.Value ? "" : Convert.ToDateTime(item["UltimaComprovacaoEndereco"]).ToString("dd/MM/yyyy"));
            }
        }

        private void CarregarListaFilial()
        {
            lstTodasFiliais.DataSource = new SistranBLL.Filial().ListarDisponiveisByIDMotorista("", 0, true);
            lstTodasFiliais.DataTextField = "NOME";
            lstTodasFiliais.DataValueField = "IDFILIAL";
            lstTodasFiliais.DataBind();
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

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }



        #region HELPERS

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

        protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboEstado.SelectedValue == "0")
            {
                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboCidade.Enabled = false;
                return;
            }
            else
            {
                CarregarCoboCidade(cboEstado.SelectedValue);
            }
            cboCidade.Focus();
        }

        protected void cboEstadoNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstadoNascimento.SelectedValue == "0")
            {
                cboCidadeNascimento.Items.Clear();
                cboCidadeNascimento.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboCidadeNascimento.Enabled = false;
                return;
            }
            else
            {
                CarregarCoboCidadeNascimento(cboEstadoNascimento.SelectedValue);
            }

            cboCidadeNascimento.Focus();
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

        protected void cboCidadeNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void brnSalvar_Click(object sender, EventArgs e)
        {
            txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

            //139.854.018-88 --14
            //12.367.719/0001-44 --18

            bool valido = false;

            if (txtcpfcnpj.Text.Trim().Length == 14)
            {
                valido = Validacao.ValidaCPF.IsCpf(txtcpfcnpj.Text.Trim());
            }


            if (txtcpfcnpj.Text.Trim().Length == 18)
            {
                valido = Validacao.ValidaCNPJ.IsCnpj(txtcpfcnpj.Text.Trim());
            }


            if (valido == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "<script>alert('CNPJ ou CPF Inválido')</script>", false);
                return;
            }

            salvar();

            try
            {
                enviarEmail(true);
            }catch(Exception )
            {
            }
            

        }

        private void enviarEmail( bool debugs)
        {
            //create the mail message
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //set the addresses
            mail.From = new System.Net.Mail.MailAddress("sistema@grupologos.com.br");
            mail.Bcc.Add("moises@sistecno.com.br");

            if(!debugs)
                mail.To.Add("jjunior@grupologos.com.br");
            else
                mail.To.Add("moises@sistecno.com.com.br");


            //set the content
            mail.Subject = "Aviso de novo Cadastro Captação";
            mail.Body = "Prezado, <br> Acesse o intranet e entre na tela de cadastro de motorista/proprietario e veja os dados desta nova ocorrência.   Lembrando que este cadastro esta bloqueado para a validação. <br> CPF: "+txtcpfcnpj.Text+" Nome: "+txtRazao.Text+"  <br> Atenciosamente, <br> GrupoLogos Intranet";            mail.Priority = System.Net.Mail.MailPriority.High;
            mail.IsBodyHtml = true;


            //send the message
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.grupologos.com.br");
            smtp.EnableSsl = false;

            System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential("sistema@grupologos.com.br", "logos0902");
            smtp.Credentials = credenciais;
            smtp.Send(mail);
        }


        private void salvar()
        {
            try
            {               

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


                if (txtcpfcnpj.Text != "" && txtcpfcnpjFavorecido.Text.Trim() == "")
                {
                    txtcpfcnpjFavorecido.Text = txtcpfcnpj.Text;
                    txtNomeFavorecido.Text = txtRazao.Text;
                    txtVinculoComFavorecido.Text = "O MESMO";
                }

                if (txtcpfcnpj.Text != txtcpfcnpjFavorecido.Text || txtRazao.Text != txtNomeFavorecido.Text)
                {
                    if (txtVinculoComFavorecido.Text.Trim() == "")
                    {
                        txtVinculoComFavorecido.Focus();
                        throw new Exception("Preencha o vinculo com o favorecido.");
                    }
                }

                txtcpfcnpjFavorecido.Text = FormatarCnpj(txtcpfcnpjFavorecido.Text);
                txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

                //lblAniversario.Text = txtDataNascimento.Text.Length == 10 ? Convert.ToDateTime(txtDataNascimento.Text).ToString("dd/MM") : "";

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
                oCadComp.Aniversario = "00/00";
                oCadComp.Banco = "237";
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
                oMot.Ativo = "NAO";
                oMot.Senha = txtSenha.Text;
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

                oMot.LocalEmissaoRG = txtLocalDeExpedicao.Text.ToUpper().Replace("'", "");
                oMot.RecolheINSS = cboRecolheInss.SelectedValue;
                oMot.RecolheIRRF = cboRecolheIRPJ.SelectedValue;
                oMot.RecolheSESTSENAT = cboRecolheSetsSenat.SelectedValue;
                


                if (txtPamcary.Text != "")
                    oMot.VencimentoPamcary = Convert.ToDateTime(txtPamcary.Text);

                if (txtBuonny.Text != "")
                    oMot.VencimentoBuonny = Convert.ToDateTime(txtBuonny.Text);

                if (txtBrasilRisk.Text != "")
                    oMot.VencimentoBrasilrisk = Convert.ToDateTime(txtBrasilRisk.Text);


                oMot.AliquotaSestSenat = Convert.ToDecimal(0);
                oMot.CarregamentoAutorizadoAte = 0;
                oMot.MOOP = "0";
                oMot.NumeroPancard = "0";

                oMot.TelefoneCel = txtFoneCelular.Text;
                oMot.TelefoneRecado = txtFoneRecado.Text;
                oMot.TelefoneRes = txtFoneResidencial.Text;
                oMot.TelefoneNextel = txtFoneNextel.Text;

                oMot.IDTelefoneCel = idtelefoneCel.Value;
                oMot.IDTelefoneRecado = idtelefoneRec.Value;
                oMot.IDTelefoneRes = idtelefoneRes.Value;
                oMot.IDTelefoneNextel = idtelefoneNextel.Value;

                ListBox lstFiliaisSelecionadas = new ListBox();


                for (int i = 0; i < lstTodasFiliais.Items.Count; i++)
                {
                    if (lstTodasFiliais.Items[i].Selected == true)
                    {
                        lstFiliaisSelecionadas.Items.Add(new ListItem(lstTodasFiliais.Items[i].Value, lstTodasFiliais.Items[i].Value));
                    }
                }

                if (lblId.Text == "" || lblId.Text == "0")
                {
                    oMotHst.Historico = "CADASTRADO PELA CAPTACAO";
                    //oMotHst.IDUsuario = ILusuario[0].UsuarioId;
                    lblId.Text = new SistranBLL.Cadastro().TransacaoInserirCadastroMotorista(HttpContext.Current.Session["Conn"].ToString(), oCad, oMot, oMotHst).ToString();
                    oCadComp.IDCadastro = int.Parse(lblId.Text);
                    int m = new SistranBLL.Cadastro.CadastroComplemento().inserir(oCadComp);
                    new SistranBLL.Cadastro.Motorista.MotoristaFilial().Inserir(oCadComp.IDCadastro, lstFiliaisSelecionadas);

                    try
                    {
                        enviarEmail(false);
                    }
                    catch (Exception)
                    {
                    }

                }
                else
                {
                    new SistranBLL.Cadastro().Alterar(oCad.CnpjCpf, oCad.InscricaoRG, txtRazao.Text.ToUpper(), txtFantasiaApelido.Text.ToUpper(), txtEndereco.Text.ToUpper(), txtNumero.Text, oCad.Complemento, oCad.IDCidade.ToString(), "", oCad.Cep, lblId.Text);
                    new SistranBLL.Cadastro.Motorista().alterar(oMot, Convert.ToInt32(lblId.Text));
                    oCadComp.IDCadastro = int.Parse(lblId.Text);
                    int m = new SistranBLL.Cadastro.CadastroComplemento().inserir(oCadComp);
                    oMotHst.Historico = "CADASTRADO PELA CAPTACAO";
                    oMotHst.IdMotorista = Convert.ToInt32(lblId.Text);
                    oMotHst.IDUsuario = 1;
                    new SistranBLL.Cadastro.Motorista.MotoristaHistorico().inserir(oMotHst);
                    new SistranBLL.Cadastro.Motorista.MotoristaFilial().Inserir(int.Parse(lblId.Text), lstFiliaisSelecionadas);
                }
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Aler", "window.location.href='frmCadastrarProprietario.aspx?i=" + lblId.Text + "' ", true);               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
                return;
            }
        }
    }
}