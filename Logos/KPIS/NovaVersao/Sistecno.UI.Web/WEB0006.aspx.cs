using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistecno.DAL;
using Util;

namespace Sistecno.UI.Web
{
    public partial class WEB0006 : System.Web.UI.Page
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
                Session["caminhoImagem"] = null;
                Session["dtContato"] = null;
                DataTable dt = new Sistecno.BLL.Veiculo().Retornar(null, "", "", "", cnx);
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
            List<ParametrosPesquisa> parPesq = new List<ParametrosPesquisa>();
            List<Sistecno.BLL.Helpers.CamposSearch> cp = dtrPesquisa.camposPesquisa;
          
            for (int i = 0; i < cp.Count; i++)
            {
                parPesq.Add(new ParametrosPesquisa(cp[i].NomeCampo.Replace("_", ""), cp[i].Valor, "string"));
            }

           DataTable dt = new Sistecno.BLL.Veiculo().Retornar(
                (parPesq[0].Valor == "" ? (int?)null : int.Parse(parPesq[0].Valor)),
                parPesq[1].Valor,
                parPesq[2].Valor,
                parPesq[3].Valor,
                cnx);

            CriarGrid(dt);
        }
  
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("WEB0006.aspx?opc="+ Request.QueryString["opc"], false);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
             if (txtPlaca.Text == "")
                    throw new Exception("Informe a Placa");

                if (txtProprietarioCpf.Text == "")
                    throw new Exception("Informe o CPF do Proprietario");

                if (txtMotoristaCpf.Text == "")
                    throw new Exception("Informe o CPF do Motorista");

                Regex regex = new Regex(@"^[a-zA-Z]{3}\-\d{4}$");

                if (!regex.IsMatch(txtPlaca.Text))
                    throw new Exception("Placa Inválida");
                               


                #region Veículo
                Sistecno.DAL.Models.Veiculo v = new Sistecno.DAL.Models.Veiculo();

                v.IDVeiculo = int.Parse(hdIdVeiculo.Value);
                v.IDVeiculoModelo = (cboModelo.SelectedIndex == 0 ? (int?)null : int.Parse(cboModelo.SelectedValue));
                v.Ano = (txtAnoFabricacao.Text == "" ? (int?)null : int.Parse(txtAnoFabricacao.Text));
                v.AnoModelo = (txtAnoModelo.Text == "" ? (int?)null : int.Parse(txtAnoModelo.Text));
                v.Placa = txtPlaca.Text;
                v.IDVeiculoTipo = (cboTipo.SelectedIndex == 0 ? (int?)null : int.Parse(cboTipo.SelectedValue));
                v.QuatidadeDeEixos = (txtEixos.Text == "" ? (int?)null : int.Parse(txtEixos.Text));
                v.CapacidadeDeCargaKG = decimal.Parse((txtCapacidadeCargakg.Text == "" ? "0" : txtCapacidadeCargakg.Text.Replace(".", ",")));
                v.CapacidadeDeCargaM3 = decimal.Parse((txtCapacidadeCargam3.Text == "" ? "0" : txtCapacidadeCargam3.Text.Replace(".", ",")));
                v.CategoriasDeCNHPermitidas = cboCategoriaPermitida.SelectedValue;
                v.IDVeiculoRastreador = (cboRastreador.SelectedIndex == 0 ? (int?)null : int.Parse(cboRastreador.SelectedValue));
                v.NumeroSerieEquipamento = txtSerieRastreador.Text;
                v.Chassi = txtChassi.Text;
                v.Renavam = txtRenavam.Text;
                v.Antt = txtAntt.Text;
                v.AnttVencimento = (txtVencAntt.Text == "" ? (DateTime?)null : DateTime.Parse(txtVencAntt.Text));
                v.DataDeLicenciamento = (txtDataLicenciamento.Text == "" ? (DateTime?)null : DateTime.Parse(txtDataLicenciamento.Text));
                v.Cor = txtCor.Text;
                #endregion

                #region Motorista
                Sistecno.DAL.Models.Motorista m = new Sistecno.DAL.Models.Motorista();

                m.IDMotorista = int.Parse(hdIdMotorista.Value);
                m.Liberado = "SIM";
                m.Ativo = "SIM";
                m.CarteiraDeHabilitacao = txtMotoristaCNH.Text;
                m.Categoria = txtMotoristaCategoria.Text;
                m.DataDeNascimento = (txtMotoristaDataDeNascimento.Text == "" ? (DateTime?)null : DateTime.Parse(txtMotoristaDataDeNascimento.Text));


                Sistecno.DAL.Models.Cadastro CadMot = new Sistecno.DAL.Models.Cadastro();
                CadMot.IDCadastro = int.Parse(hdIdMotorista.Value);
                CadMot.CnpjCpf = txtMotoristaCpf.Text;
                CadMot.RazaoSocialNome = txtMotoristaNome.Text;
                CadMot.InscricaoRG = txtMotoristaRg.Text;
                CadMot.Endereco = txtMotoristaEndreco.Text;
                CadMot.Numero = txtMotoristaNumero.Text;
                CadMot.Complemento = txtMotoristaComplemento.Text;
                CadMot.IDCidade = (cboMotoristaCidade.SelectedIndex > 0 ? int.Parse(cboMotoristaCidade.SelectedValue) : (int?)null);
                CadMot.Cep = txtMotoristaCEP.Text;


                Sistecno.BLL.Cadastro.Motorista o = new Sistecno.BLL.Cadastro.Motorista();
                Sistecno.DAL.Models.CadastroComplemento cc = new DAL.Models.CadastroComplemento();

                DataTable dtContatos = new DataTable();
                hdIdMotorista.Value = o.GravarMotorista(CadMot, null, null, m, cnx).ToString();
                #endregion
                
                #region Proprietario
                Sistecno.DAL.Models.Proprietario Prop = new Sistecno.DAL.Models.Proprietario();
                Prop.IDProprietario = int.Parse(hdIdProprietario.Value);

                Sistecno.DAL.Models.Cadastro CadProp = new Sistecno.DAL.Models.Cadastro();
                CadProp.IDCadastro = int.Parse(hdIdProprietario.Value);
                CadProp.CnpjCpf = txtProprietarioCpf.Text;
                CadProp.RazaoSocialNome = txtProprietarioNome.Text;
                CadProp.Endereco = txtProprietarioEndereco.Text;
                CadProp.Numero = txtProprietarioNumero.Text;
                CadProp.Complemento = txtProprietarioComplemento.Text;
                CadProp.IDCidade = (cboProprietarioCidade.SelectedIndex > 0 ? int.Parse(cboProprietarioCidade.SelectedValue) : (int?)null);
                CadProp.Cep = txtProprietarioCEP.Text;

                Sistecno.BLL.Cadastro opr = new Sistecno.BLL.Cadastro();

                hdIdProprietario.Value = opr.Gravar(CadProp, cnx).ToString();


                new Sistecno.BLL.Cadastro.Proprietario().Inserir(CadProp.IDCadastro, cnx);

                #endregion

                v.IDProprietario = int.Parse(hdIdProprietario.Value);
                v.IDMotorista = int.Parse(hdIdMotorista.Value);

                new Sistecno.BLL.Veiculo().GravarVeiculo(v, cnx);

                Notificar("Cadastro efetuado com sucesso", "Aviso");
                Response.Redirect("web0006.aspx?opc=" + Request.QueryString["opc"], false);
            }
            catch (Exception ex)
            {
                Notificar("Messangem: " + ex.Message + " - " + ex.InnerException, "ATENÇÃO");
            }
        }


        protected void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPlaca.Text = Sistecno.BLL.Helpers.Util.Validacoes.FormatarPlaca(txtPlaca.Text);

                Regex regex = new Regex(@"^[a-zA-Z]{3}\-\d{4}$");

                if (regex.IsMatch(txtPlaca.Text))
                {
                    Sistecno.DAL.Models.Veiculo v = new Sistecno.BLL.Veiculo().RetornarbyPlaca(txtPlaca.Text, cnx);

                    if (v != null && v.IDVeiculo != null && v.IDVeiculo > 0)
                        CarregarCampos(v.IDVeiculo);

                }
                else
                {
                    txtPlaca.Focus();
                    txtPlaca.Text = "";
                    throw new Exception("Placa Inválida");
                }
            }
            catch (Exception ex)
            {
                Notificar("Messangem: " + ex.Message + " - " + ex.InnerException, "ATENÇÃO");
            }
        }

        private void CarregarCampos(int codigoVeiculo)
        {
            try
            {


                Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Modelo().Retornar(cnx), ref cboModelo, false, true, "IDVeiculoModelo", "Nome");
                Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Tipo().Retornar(cnx), ref cboTipo, false, true, "IDveiculoTipo", "Nome");
                Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Rastreador().Retornar(cnx), ref cboRastreador, false, true, "IDVeiculoRastreador", "Nome");
                Combo.CarregarCombo((DataTable)Session["dtUF"], ref cboMotoristaEstado, true, true, "IDESTADO", "NOME");
                Combo.CarregarCombo((DataTable)Session["dtUF"], ref cboProprietarioEstado, true, true, "IDESTADO", "NOME");


                Sistecno.DAL.Models.Veiculo v = new Sistecno.BLL.Veiculo().RetornarAllFields(codigoVeiculo, cnx);

                if (v != null)
                {
                    hdIdVeiculo.Value = v.IDVeiculo.ToString();
                    cboModelo.SelectedValue = v.IDVeiculoModelo.ToString();
                    txtAnoFabricacao.Text = v.Ano.ToString();
                    txtAnoModelo.Text = v.AnoModelo.ToString();
                    txtPlaca.Text = v.Placa;
                    txtCor.Text = v.Cor;
                    cboTipo.SelectedValue = v.IDVeiculoTipo.ToString();
                    txtEixos.Text = v.QuatidadeDeEixos.ToString();

                    txtCapacidadeCargakg.Text = (v.CapacidadeDeCargaKG.ToString() == "" ? "0" : Convert.ToInt32(decimal.Parse(v.CapacidadeDeCargaKG.ToString())).ToString());
                    txtCapacidadeCargam3.Text = (v.CapacidadeDeCargaM3.ToString() == "" ? "0" : Convert.ToInt32(decimal.Parse(v.CapacidadeDeCargaM3.ToString())).ToString());
                    cboCategoriaPermitida.SelectedValue = v.CategoriasDeCNHPermitidas;
                    cboRastreador.SelectedValue = v.IDVeiculoRastreador.ToString();
                    txtSerieRastreador.Text = v.NumeroSerieEquipamento;
                    txtChassi.Text = v.Chassi;
                    txtRenavam.Text = v.Renavam;
                    txtAntt.Text = v.Antt;
                    txtVencAntt.Text = v.AnttVencimento.ToString();
                    txtDataLicenciamento.Text = v.DataDeLicenciamento.ToString();


                    Sistecno.DAL.Models.Cadastro cm = null;
                    Sistecno.DAL.Models.Motorista mot = null;

                    if (mot == null)
                        mot = new Sistecno.BLL.Cadastro.Motorista().RetornarTable((int)v.IDMotorista, cnx);

                    if (cm == null)
                        cm = new Sistecno.BLL.Cadastro().RetornarTabela((int)v.IDMotorista, cnx);


                    Sistecno.DAL.Models.Cidade cid = null;
                    Sistecno.DAL.Models.Estado est = null;

                    if (cm.Cidade != null)
                    {
                        cid = cm.Cidade;
                        est = cid.Estado;
                    }


                    txtMotoristaCpf.Text = cm.CnpjCpf;
                    txtMotoristaNome.Text = cm.RazaoSocialNome;
                    txtMotoristaEndreco.Text = cm.Endereco;
                    txtMotoristaNumero.Text = cm.Numero;

                    if (cid != null && est.IDEstado > 0)
                    {
                        cboMotoristaEstado.SelectedValue = est.IDEstado.ToString();
                        CarregarCidadesCbo(cboMotoristaEstado, cboMotoristaCidade);
                        cboMotoristaCidade.SelectedValue = cm.IDCidade.ToString();
                    }

                    hdIdMotorista.Value = cm.IDCadastro.ToString();
                    txtMotoristaCEP.Text = cm.Cep;
                    txtMotoristaRg.Text = cm.InscricaoRG;
                    txtMotoristaCNH.Text = mot.NumeroRegistroCNH;
                    txtMotoristaDataDeNascimento.Text = mot.DataDeNascimento.ToString();
                    txtMotoristaCategoria.Text = mot.CarteiraDeHabilitacao;


                    Sistecno.DAL.Models.Cadastro cp = new Sistecno.BLL.Cadastro().RetornarTabela((int)v.IDProprietario, cnx);

                    hdIdProprietario.Value = cp.IDCadastro.ToString();
                    txtProprietarioCEP.Text = cp.Cep;
                    txtProprietarioComplemento.Text = cp.Complemento;
                    txtProprietarioCpf.Text = cp.CnpjCpf;
                    txtProprietarioEndereco.Text = cp.Endereco;
                    txtProprietarioNome.Text = cp.RazaoSocialNome;
                    txtProprietarioNumero.Text = cp.Numero;

                    cid = null;
                    est = null;

                    if (cp.Cidade != null)
                    {
                        cid = cp.Cidade;
                        est = cid.Estado;
                    }

                    if (cid != null && est.IDEstado > 0)
                    {
                        cboProprietarioEstado.SelectedValue = est.IDEstado.ToString();
                        CarregarCidadesCbo(cboProprietarioEstado, cboProprietarioCidade);
                        cboProprietarioCidade.SelectedValue = cp.IDCidade.ToString();
                    }
                }
            }
            catch (Exception ec)
            {
                Notificar(ec.Message, "Erro");
            }
        }

        public void CarregarCidadesCbo(DropDownList estado, DropDownList cidade)
        {
            if (estado.SelectedIndex > 0)
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(Session["CNX"].ToString(), int.Parse(estado.SelectedValue), ""), ref cidade, true, true, "IDCIDADE", "NOME");
            else
            {
                cidade.Items.Clear();
                cidade.Items.Add("SELECIONE O ESTADO");
            }
        }
        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMotoristaEstado.SelectedIndex > 0)
            {
                CarregarCidadesCbo(cboMotoristaEstado, cboMotoristaCidade);
            }
            else
            {
                cboMotoristaCidade.Items.Clear();
                cboMotoristaCidade.Items.Add(new ListItem("Selecione o Estado"));
            }
        }

        protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CODIGO", "CODIGO", "50", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("PLACA", "PLACA", "70", "txt", "", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("MOTORISTA", "MOTORISTA", "120", "txt", "", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("PROPRIETARIO", "PROPRIETARIO", "120", "txt", "", "", null));           
            dtrPesquisa.camposPesquisa = f;
        }

        private void CriarGrid(DataTable dt)
        {                       
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB0006.ASPX", Request.QueryString["opc"])));
        }

        private void PrepararTelaEdicao()
        {
            dvManut.Visible = true;
            dvPesq.Visible = false;
            dvbot.Visible = false;
            dvGrid.Visible = false;


            Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Modelo().Retornar(cnx), ref cboModelo, false, true, "IDVeiculoModelo", "Nome");
            Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Tipo().Retornar(cnx), ref cboTipo, false, true, "IDveiculoTipo", "Nome");
            Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Rastreador().Retornar(cnx), ref cboRastreador, false, true, "IDVeiculoRastreador", "Nome");
            Combo.CarregarCombo((DataTable)Session["dtUF"], ref cboMotoristaEstado, true, true, "IDESTADO", "NOME");
            Combo.CarregarCombo((DataTable)Session["dtUF"], ref cboProprietarioEstado, true, true, "IDESTADO", "NOME");

            if (Request.QueryString["acao"] == "editar")
            {
                CarregarCampos(int.Parse(Request.QueryString["id"]));
            }
        }
        
        protected void txtMotoristaCpf_TextChanged(object sender, EventArgs e)
        {
            if (txtMotoristaCpf.MaxLength == 0)
            {
                txtMotoristaCpf.Focus();
                return;
            }
            if (Sistecno.BLL.Helpers.Util.Validacoes.CpfValido(txtMotoristaCpf.Text))
            {

                Sistecno.DAL.Models.Cadastro cm = new Sistecno.BLL.Cadastro().RetornarByCnpj(txtMotoristaCpf.Text, cnx);
                Sistecno.DAL.Models.Motorista mot = new Sistecno.BLL.Cadastro.Motorista().RetornarTable(cm.IDCadastro, cnx);

                Sistecno.DAL.Models.Cidade cid = null;
                Sistecno.DAL.Models.Estado est = null;

                if (cm.IDCidade != null)
                {
                    cid = cm.Cidade;
                    est = cid.Estado;
                }

                txtMotoristaCpf.Text = cm.CnpjCpf;
                txtMotoristaNome.Text = cm.RazaoSocialNome;
                txtMotoristaEndreco.Text = cm.Endereco;
                txtMotoristaNumero.Text = cm.Numero;

                if (est != null && est.IDEstado > 0)
                {
                    cboMotoristaEstado.SelectedValue = est.IDEstado.ToString();
                    CarregarCidadesCbo(cboMotoristaEstado, cboMotoristaCidade);
                    cboMotoristaCidade.SelectedValue = cm.IDCidade.ToString();
                }

                hdIdMotorista.Value = cm.IDCadastro.ToString();
                txtMotoristaCEP.Text = cm.Cep;
                txtMotoristaRg.Text = cm.InscricaoRG;

                if (mot != null)
                {
                    txtMotoristaCNH.Text = mot.CarteiraDeHabilitacao;
                    txtMotoristaDataDeNascimento.Text = mot.DataDeNascimento.ToString();
                    txtMotoristaCategoria.Text = mot.Categoria;
                }
            }
            else
            {
                txtMotoristaCpf.Focus();
                return;
            }
        }

        protected void txtProprietarioCpf_TextChanged(object sender, EventArgs e)
        {
            if (txtProprietarioCpf.MaxLength == 0)
            {
                txtProprietarioCpf.Focus();
                return;
            }
            if (Sistecno.BLL.Helpers.Util.Validacoes.CpfValido(txtProprietarioCpf.Text))
            {

                Sistecno.DAL.Models.Cadastro cm = new Sistecno.BLL.Cadastro().RetornarByCnpj(txtProprietarioCpf.Text, cnx);

                Sistecno.DAL.Models.Cidade cid = null;
                Sistecno.DAL.Models.Estado est = null;

                if (cm.IDCidade != null)
                {
                    cid = cm.Cidade;
                    est = cid.Estado;
                }

                txtProprietarioCpf.Text = cm.CnpjCpf;
                txtProprietarioNome.Text = cm.RazaoSocialNome;
                txtProprietarioEndereco.Text = cm.Endereco;
                txtProprietarioNumero.Text = cm.Numero;

                if (est != null && est.IDEstado > 0)
                {
                    cboProprietarioEstado.SelectedValue = est.IDEstado.ToString();
                    CarregarCidadesCbo(cboProprietarioEstado, cboProprietarioCidade);
                    cboProprietarioCidade.SelectedValue = cm.IDCidade.ToString();
                }

                hdIdProprietario.Value = cm.IDCadastro.ToString();
                txtProprietarioCEP.Text = cm.Cep;

            }
            else
            {
                txtProprietarioCpf.Focus();
                return;
            }
        }

        protected void cboProprietarioEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProprietarioEstado.SelectedIndex > 0)
            {
                CarregarCidadesCbo(cboProprietarioEstado, cboProprietarioCidade);
            }
            else
            {
                cboProprietarioCidade.Items.Clear();
                cboProprietarioCidade.Items.Add(new ListItem("Selecione o Estado"));
            }
        }

    
    }



        #endregion
}