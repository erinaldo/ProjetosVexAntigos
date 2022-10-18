using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistecno.DAL;
using Util;

namespace Sistecno.UI.Web
{
    public partial class WEB0011: System.Web.UI.Page
    {
        #region Variaveis de Escopo Global na Pagina
        
        string cnx = "";
        DAL.Models.Usuario usuarioLogado;
           

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioLogado = (Sistecno.DAL.Models.Usuario)Session["USUARIOLOGADO"];         

            dtrPesquisa.UserControlPesquisarClicked += new EventHandler(Pesquisar_Click);
            lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")";
            cnx = Session["CNX"].ToString();

            dtrMensagensValidacao.listMensagens = null;

            if (!IsPostBack)
            {
                DataTable dt = new BLL.Documento().RetornarPesquisaTelaDocumentos("", (int)usuarioLogado.UltimaEmpresa, "", (int)usuarioLogado.UltimaFilial, cnx);
                CriarGrid(dt);


                //Prepara a parte do cadastro
                switch (Request.QueryString["acao"])
                {
                    case "novo":
                    case "editar":
                        PrepararTelaEdicao();
                        break;
                }
            }


            #region OBJETOS QUE NECESSITAM SER CRIADOS EM TODOS OS POSTS
            CamposPesquisa();
        

            #endregion
        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            //localiza os controles de pesquisa
           //localiza os controles de pesquisa
            List<ParametrosPesquisa> parPesq = new List<ParametrosPesquisa>();
            List<Sistecno.BLL.Helpers.CamposSearch> cp = dtrPesquisa.camposPesquisa;
            for (int i = 0; i < cp.Count; i++)
            {
                if (cp[i].Valor.Length > 0)
                    parPesq.Add(new ParametrosPesquisa(cp[i].NomeCampo.Replace("_", ""), cp[i].Valor, "string"));
            }


             DataTable dt = new BLL.Documento().RetornarPesquisaTelaDocumentos(cp[0].Valor, (int)usuarioLogado.UltimaEmpresa, "", (int)usuarioLogado.UltimaFilial, cnx);
            CriarGrid(dt);
        }
  
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("WEB0005.aspx?opc="+ Request.QueryString["opc"], false);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            cnx = new  Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal;
            //frwSistecno.Helpers h = new frwSistecno.Helpers(Session["CNX"].ToString());

            try
            {
                //if (txtCNPJCadastro.Text == "")
                //    throw new Exception("CPF é obrigatório");

                //if (txtRazaoSocialNome.Text == "")
                //    throw new Exception("NOME é obrigatório");

                //if (!Sistecno.BLL.Helpers.Util.Validacoes.CnpjValido(txtCNPJCadastro.Text) && !Sistecno.BLL.Helpers.Util.Validacoes.CpfValido(txtCNPJCadastro.Text))
                //    throw new Exception("CNPJ ou CPF inválido");

                //#region Cadastro
                //Sistecno.DAL.Models.Cadastro oC = new Sistecno.DAL.Models.Cadastro();
                //Sistecno.DAL.Models.CadastroComplemento oCC = new Sistecno.DAL.Models.CadastroComplemento();
                //Sistecno.DAL.Models.Motorista oM = new Sistecno.DAL.Models.Motorista();

                //TextBox txtCodigo = new TextBox();
                //txtCodigo.Text = Request.QueryString["id"];
                //oC.IDCadastro = int.Parse(txtCodigo.Text);

                //oC.CnpjCpf = txtCNPJCadastro.Text;
                //oC.RazaoSocialNome = txtRazaoSocialNome.Text.ToUpper();
                //oC.FantasiaApelido = (txtFantasiaApelido.Text == "" ? txtRazaoSocialNome.Text : txtFantasiaApelido.Text).ToUpper();
                //oC.Cep = txtCEP.Text;
                //oC.Endereco = txtEndereco.Text.ToUpper();
                //oC.Numero = txtNumero.Text;
                //oC.Complemento = txtComplemento.Text.ToUpper();
                //oC.IDCidade = (cboCidade.SelectedIndex > 0 ? int.Parse(cboCidade.SelectedValue) : (int?)null);
                //oC.InscricaoRG = txtRG.Text;
                //oC.DataDeCadastro = DateTime.Now;

                //if (cboBairro.SelectedIndex > 0)
                //    oC.IDBairro = int.Parse(cboBairro.SelectedValue);

                //#endregion

                //#region Motorista
                //oM.IDMotorista = int.Parse(txtCodigo.Text);
                //oM.CarteiraDeHabilitacao = txtCarteiraHab.Text;
                //oM.NumeroRegistroCNH = txtNumeroRegistro.Text;
                //oM.DataDaPrimeiraHabilitacao = (txtDataPrimeiraHab.Text != "" ? DateTime.Parse(txtDataPrimeiraHab.Text) : (DateTime?)null);
                //oM.ValidadeDaHabilitacao = (txtValidade.Text != "" ? DateTime.Parse(txtValidade.Text) : (DateTime?)null);
                //oM.IDCidadeNascimento = (cboCidadeNascimento.SelectedIndex > 0 ? cboCidadeNascimento.SelectedValue : "");
                //oM.LocalEmissaoRG = txtEmissaoRG.Text;
                //oM.NumeroInss = txtNumeroINSS.Text;
                //oM.NomeDaMae = txtNomeMae.Text;
                //oM.NomeDoPai = txtNomePai.Text;
                //oM.VencimentoPancary = (txtVencimentoPancary.Text != "" ? DateTime.Parse(txtVencimentoPancary.Text) : (DateTime?)null);
                //oM.VencimentoBrasilrisk = (txtVencimentoBrasilRisk.Text != "" ? DateTime.Parse(txtVencimentoBrasilRisk.Text) : (DateTime?)null);
                //oM.NumeroPancard = txtNumeroPancard.Text;
                //oM.VencimentoBuonny = (txtVencimentoBUony.Text != "" ? DateTime.Parse(txtVencimentoBUony.Text) : (DateTime?)null);
                //oM.VitimaDeRouboQuantidade = (txtNumeroRoubos.Text != "" ? int.Parse(txtNumeroRoubos.Text) : (int?)null);
                //oM.SofreuAcidadeQuantidade = (txtNumeroAcidentes.Text != "" ? int.Parse(txtNumeroAcidentes.Text) : (int?)null);
                //oM.AliquotaSestSenat = (txtAliquitaSestSenat.Text != "" ? decimal.Parse(txtAliquitaSestSenat.Text.Replace(",", ".")) : (decimal?)null);
                //oM.Conjuge = txtConjuge.Text;
                //oM.EstadoCivil = cboEstadoCivil.SelectedValue;
                //#endregion

                //#region Cadastro Complemento
                //oCC.Banco = txtNumeroBanco.Text;
                //oCC.Agencia = txtAgencia.Text;
                //oCC.AgenciaDigito = txtAgenciaDigito.Text;
                //oCC.TipoConta = cboTipodeConta.SelectedValue;
                //oCC.Conta = txtNumeroConta.Text;
                //oCC.ContaDigito = txtDigitoConta.Text;
                //oCC.NomeFavorecido = txtFavorecido.Text;
                //oCC.CnpjCpfFavorecido = txtCpfFavorecido.Text;
                //oCC.ValorPensaoAlimenticia = (txtValorPensao.Text != "" ? decimal.Parse(txtValorPensao.Text.Replace(",", ".")) : (decimal?)null);
                //oCC.Aposentado = cboAposentado.SelectedValue;
                //#endregion


                //DataTable dtContato = (DataTable)Session["dtContato"];
                //new Sistecno.BLL.Cadastro.Motorista().GravarMotorista(oC, oCC, dtContato, oM, cnx);

                //Notificar("Cadastro de: " + txtRazaoSocialNome.Text + " foi efetuado com sucesso", "Aviso");
                //Response.Redirect("WEB0005.ASPX?opc=" + Request.QueryString["opc"], false);
            }
            catch (Exception ex)
            {
                Notificar("Messangem: " + ex.Message + " - Inner: " + ex.InnerException, "Ops! Houve um problema.");
            }
        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboEstado.SelectedIndex > 0)
            {
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, true, true, "IDCIDADE", "NOME");
                cboCidade.Focus();
            }
            else
            {
                cboCidade.Items.Clear();
                cboBairro.Items.Clear();
                cboCidade.Items.Add("SELECIONE O ESTADO");
                cboBairro.Items.Add("SELECIONE A CIDADE");
            }
        }

        protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidade.SelectedIndex > 0)
            {
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");
                cboBairro.Focus();
            }
            else
            {
                cboBairro.Items.Clear();
                cboBairro.Items.Add("SELECIONE A CIDADE");
            }
        }

        protected void cboEstadoNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstadoNascimento.SelectedIndex > 0)
            {
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboEstadoNascimento.SelectedValue), ""), ref cboCidadeNascimento, true, true, "IDCIDADE", "NOME");
                cboCidadeNascimento.Focus();
            }
            else
            {
                cboCidadeNascimento.Items.Clear();
                cboCidadeNascimento.Items.Add("SELECIONE O ESTADO");
            }
        }
       
    
                   
                
        #endregion
        
        #region Metodos

        private void Notificar(string TextoDaMenssagem, string Titulo)
        {
            List<string> msg = new List<string>();

            if (TextoDaMenssagem.ToUpper().Contains("TRIGGER"))
            {
                TextoDaMenssagem = "NÃO É PERMITIDO TROCAR OS DADOS DA FILIAL.(TRIGGER)";
            }


            msg.Add(TextoDaMenssagem);
            dtrMensagensValidacao.listMensagens = msg;
            dtrMensagensValidacao.TituloMensagem = Titulo;
            dtrMensagensValidacao.MostrarMensagem();
        }
                 
        public void CamposPesquisa()
        {
            List<Sistecno.BLL.Helpers.CamposSearch> f = new List<Sistecno.BLL.Helpers.CamposSearch>();
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID", "IDCADASTRO", "70", "txt", "SomenteNumero", "", null));
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "120", "txt", "CNPJ", "", null));
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("CPF", "CNPJCPF_", "100", "txt", "CPF", "", null));
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("NOME", "RAZAOSOCIALNOME", "150", "", "", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("NÚMERO", "NUMERO", "150", "txt", "SomenteNumero", "", null));
            dtrPesquisa.camposPesquisa = f;
        }

        private void CriarGrid(DataTable dt)
        {                       
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB0011.ASPX", Request.QueryString["opc"])));
        }

        private void PrepararTelaEdicao()
        {
            dvManut.Visible = true;
            dvPesq.Visible = false;
            dvbot.Visible = false;
            dvGrid.Visible = false;          
          

            if (Request.QueryString["acao"] == "editar")
            {
                CarregarCampos(int.Parse(Request.QueryString["id"]), "");
            }
            else
            {
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado, true, true, "CODIGO", "NOME");
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstadoNascimento, true, true, "CODIGO", "NOME");

                cboEstado.Items.Insert(0, new ListItem("Selecione o Estado"));
                cboEstadoNascimento.Items.Insert(0, new ListItem("Selecione o Estado"));
            }

            Combo.CarregarCombo(new Sistecno.BLL.Cadastro.TipoDeContato().Retornar(cnx), ref cboTipoDeEndereco, false, true, "IDCADASTROTIPODECONTATO", "NOME");
            txtCNPJCadastro.Focus();
        }

        //public Button CriarExcluir(string sequencia)
        //{
        //    Button b = new Button();
        //    b.Text = "Excluir";
        //    b.CssClass = "btn btn-danger btn-xs";
        //    b.ID = "btn_" + sequencia;

        //    b.Click += new System.EventHandler(this.ExcluirMeioDeContatos_Click);
        //    return b;
        //}

          
      
        protected void txtCNPJCadastro_TextChanged1(object sender, EventArgs e)
        {
           
        }

        private void CarregarCampos(int Codigo, string cnpjcfp)
        {
            //cnx = new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal;
            try
            {
            //    DataTable dt = new Sistecno.BLL.Cadastro.Motorista().RetornarTodosCampos(Codigo, cnx);
            //    Sistecno.BLL.PaisUfCidade oCidEst = new Sistecno.BLL.PaisUfCidade();

            //    #region Cadastro
            //    if (dt.Rows.Count > 0)
            //    {
            //        txtCNPJCadastro.Text = dt.Rows[0]["CnpjCpf"].ToString();
            //        txtRG.Text = dt.Rows[0]["InscricaoRG"].ToString();
            //        txtDataCadastro.Text = (dt.Rows[0]["DataDeCadastro"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["DataDeCadastro"].ToString()).ToShortDateString() : "");
            //        txtRazaoSocialNome.Text = dt.Rows[0]["RazaoSocialNome"].ToString();
            //        txtFantasiaApelido.Text = dt.Rows[0]["FantasiaApelido"].ToString();
            //        txtCEP.Text = dt.Rows[0]["Cep"].ToString();
            //        txtEndereco.Text = dt.Rows[0]["Endereco"].ToString();
            //        txtNumero.Text = dt.Rows[0]["Numero"].ToString();
            //        txtComplemento.Text = dt.Rows[0]["Complemento"].ToString();

            //        if (dt.Rows[0]["IDCIDADE"].ToString() != "")
            //        {
            //            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado, true, true, "CODIGO", "NOME");
            //            cboEstado.SelectedValue = dt.Rows[0]["IDESTADO"].ToString();

            //            Combo.CarregarCombo(oCidEst.RetornarCidade(cnx, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, false, true, "IDCIDADE", "NOME");
            //            cboCidade.SelectedValue = dt.Rows[0]["IDCIDADE"].ToString();

            //            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(new DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");

            //            if (dt.Rows[0]["IDBAIRRO"].ToString().Length > 0)
            //                cboBairro.SelectedValue = dt.Rows[0]["IDBAIRRO"].ToString();
            //        }

            //    }
            //    #endregion

            //    #region Motorista

            //    cboAtivo.SelectedValue = dt.Rows[0]["Ativo"].ToString();
            //    cboLiberado.SelectedValue = dt.Rows[0]["Liberado"].ToString();
            //    txtDataBloqueio.Text = (dt.Rows[0]["DataDeBloqueio"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["DataDeBloqueio"].ToString()).ToShortDateString() : "");
            //    txtCarteiraHab.Text = dt.Rows[0]["CarteiraDeHabilitacao"].ToString();
            //    txtNumeroRegistro.Text = dt.Rows[0]["NumeroRegistroCNH"].ToString();
            //    txtCategoria.Text = dt.Rows[0]["Categoria"].ToString();
            //    txtValidade.Text = (dt.Rows[0]["ValidadeDaHabilitacao"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["ValidadeDaHabilitacao"].ToString()).ToShortDateString() : "");

            //    if (dt.Rows[0]["IDCIDADENASCIMENTO"].ToString() != "")
            //    {
            //        Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstadoNascimento, true, true, "CODIGO", "NOME");
            //        cboEstadoNascimento.SelectedValue = dt.Rows[0]["IDESTADONASCIMENTO"].ToString();

            //        Combo.CarregarCombo(oCidEst.RetornarCidade(cnx, int.Parse(cboEstadoNascimento.SelectedValue), ""), ref cboCidadeNascimento, false, true, "IDCIDADE", "NOME");
            //        cboCidadeNascimento.SelectedValue = dt.Rows[0]["IDCIDADENASCIMENTO"].ToString();
            //    }


            //    txtNumeroINSS.Text = dt.Rows[0]["NumeroInss"].ToString();
            //    txtNomePai.Text = dt.Rows[0]["NomeDoPai"].ToString();
            //    txtNomeMae.Text = dt.Rows[0]["NomeDaMae"].ToString();
            //    cboEstadoCivil.SelectedValue = dt.Rows[0]["EstadoCivil"].ToString();
            //    txtConjuge.Text = dt.Rows[0]["Conjuge"].ToString();
            //    txtNumeroPancard.Text = dt.Rows[0]["NumeroPancard"].ToString();
            //    txtVencimentoBrasilRisk.Text = (dt.Rows[0]["VencimentoBrasilrisk"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["VencimentoBrasilrisk"].ToString()).ToShortDateString() : "");
            //    txtVencimentoPancary.Text = (dt.Rows[0]["VencimentoPancary"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["VencimentoPancary"].ToString()).ToShortDateString() : "");
            //    txtVencimentoBUony.Text = (dt.Rows[0]["VencimentoBuonny"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["VencimentoBuonny"].ToString()).ToShortDateString() : "");
            //    txtNumeroRoubos.Text = (dt.Rows[0]["VitimaDeRouboQuantidade"].ToString() == "" ? "0" : dt.Rows[0]["VitimaDeRouboQuantidade"].ToString());
            //    txtNumeroAcidentes.Text = (dt.Rows[0]["SofreuAcidadeQuantidade"].ToString() == "" ? "0" : dt.Rows[0]["SofreuAcidadeQuantidade"].ToString());
            //    txtAliquitaSestSenat.Text = (dt.Rows[0]["AliquotaSestSenat"].ToString() == "" ? "0" : dt.Rows[0]["AliquotaSestSenat"].ToString());

            //    #endregion

            //    #region Cadastro Complemento

            //    txtUltimaComprovacaoDeEndereco.Text = (dt.Rows[0]["UltimaComprovacaoEndereco"].ToString() != "" ? DateTime.Parse(dt.Rows[0]["UltimaComprovacaoEndereco"].ToString()).ToShortDateString() : "");
            //    txtEmissaoRG.Text = dt.Rows[0]["OrgaoExpedicaoRG"].ToString();
            //    txtNumeroBanco.Text = dt.Rows[0]["Banco"].ToString();
            //    txtNumeroConta.Text = dt.Rows[0]["Conta"].ToString();
            //    txtDigitoConta.Text = dt.Rows[0]["ContaDigito"].ToString();
            //    txtAgencia.Text = dt.Rows[0]["Agencia"].ToString();
            //    txtAgenciaDigito.Text = dt.Rows[0]["AgenciaDigito"].ToString();
            //    cboTipodeConta.SelectedValue = dt.Rows[0]["TipoConta"].ToString();
            //    txtCpfFavorecido.Text = dt.Rows[0]["CnpjCpfFavorecido"].ToString();
            //    txtFavorecido.Text = dt.Rows[0]["NomeFavorecido"].ToString();
            //    txtValorPensao.Text = (dt.Rows[0]["ValorPensaoAlimenticia"].ToString() == "" ? "0" : Convert.ToDecimal(dt.Rows[0]["ValorPensaoAlimenticia"].ToString()).ToString("#0.00"));
            //    cboAposentado.SelectedValue = dt.Rows[0]["Aposentado"].ToString();

            //    #endregion

            //    #region Proprietario
            //    chkProprietario.Checked = (dt.Rows[0]["IDPROPRIETARIO"].ToString() == "0" ? false : true);

            //    #endregion

            //    #region Contatos
            //    DataTable dtCont = new Sistecno.BLL.Cadastro.ContatoEndereco().RetornarByIdCasdastro(Codigo, cnx);

            //    verificarDataTableContato();
            //    Session["dtContato"] = null;
            //    dtContato.Rows.Clear();

            //    lblSequencia.Text = "";
            //    txtEnderecoMeioDeContato.Text = "";
            //    cboTipoDeEndereco.SelectedIndex = 0;

            //    for (int i = 0; i < dtCont.Rows.Count; i++)
            //    {
            //        if (dtCont.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString().Length > 0)
            //        {
            //            DataRow dr = dtContato.NewRow();
            //            dr["IDCADASTROCONTATOENDERECO"] = dtCont.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString();
            //            dr["IDCADASTRO"] = dtCont.Rows[i]["IDCADASTRO"].ToString();
            //            dr["IDCADASTROTIPODECONTATO"] = dtCont.Rows[i]["IDCADASTROTIPODECONTATO"].ToString();
            //            dr["TIPODECONTATO"] = dtCont.Rows[i]["TIPODECONTATO"].ToString();
            //            dr["ENDCONTADO"] = dtCont.Rows[i]["ENDCONTADO"].ToString();
            //            //dr["ANIVERSARIO"] = dtCont.Rows[i]["ANIVERSARIO"].ToString();
            //            dr["STATUS"] = "SIM";

            //            string seq = dtContato.Compute("MAX(SEQUENCIA)", "").ToString();

            //            dr["SEQUENCIA"] = seq == "" ? "1" : (int.Parse(seq) + 1).ToString();

            //            dtContato.Rows.Add(dr);
            //        }
            //    }

            //    CriarGridMeioDeContato(dtContato);
            //    Session["dtContato"] = dtContato;
              
            //    #endregion

            }
            catch (Exception ex)
            {
                Notificar(ex.Message, "Aviso");
            }
        }
    }

        #endregion
}