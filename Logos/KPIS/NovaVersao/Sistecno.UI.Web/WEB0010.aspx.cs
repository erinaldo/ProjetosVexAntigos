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
    public partial class WEB0010 : System.Web.UI.Page
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
            //dtrPesquisaGenericaR.UserControlSelecionarClicked += new EventHandler(SelecionarItemClicado_Click);
            //dtrPesquisaGenericaDetinatario.UserControlSelecionarClicked += new EventHandler(SelecionarItemClicadoDestinatario_Click);
            //dtrPesquisaGenericaCliente.UserControlSelecionarClicked += new EventHandler(SelecionarItemClicadoCliente_Click);

            lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")";

            cnx = Session["CNX"].ToString();

            dtrMensagensValidacao.listMensagens = null;

            if (!IsPostBack)
            {
                Session["caminhoImagem"] = null;
                Session["dtContato"] = null;
                DataTable dt = new Sistecno.BLL.Documento().RetornarPesquisa("", "", "ORDEM DE SERVICO", (int)usuarioLogado.UltimaFilial, "", true, cnx);
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
            CamposPesquisaGenRemetente();
            CamposPesquisaGenDestinatario();
            CamposPesquisaGenCliente();
            #endregion
        }

        private void SelecionarItemClicadoCliente_Click(object sender, EventArgs e)
        {
            //retorna os dados do registro escolhido na pesquisa(grid) no filtro
            DataRow[] retor = (DataRow[])Session["linhaSel"];

            Sistecno.BLL.Cadastro c = new Sistecno.BLL.Cadastro();

            Sistecno.DAL.Models.Cadastro cad = c.RetornarTabela(int.Parse(dtrPesquisaGenericaCliente.ret), cnx);

            txtPbCnpj_2.Text = cad.CnpjCpf;
            txtPbRazaoSocial_2.Text = cad.RazaoSocialNome;
            txtPbId_2.Text = dtrPesquisaGenericaR.ret;
            txtPbIe_2.Text = cad.InscricaoRG;
            txtPbEndereco_2.Text = cad.Endereco;
            txtPbNumero_2.Text = cad.Numero;
            txtPbComplemento_2.Text = cad.Complemento;
            txtPbCEP_2.Text = cad.Cep;
            Sistecno.DAL.Models.Cidade cid = null;
            Sistecno.DAL.Models.Estado est = null;

            if (cad.Cidade != null)
            {
                cid = cad.Cidade;
                est = cid.Estado;
            }

            if (cad.IDBairro.ToString() != "")
            {
                Sistecno.DAL.Models.Bairro bar = cad.Bairro;
                txtPbBairro_2.Text = bar.Nome;
            }

            if (cad.Cidade != null)
            {
                txtPbCidade_2.Text = cid.Nome;

                if (cid.Estado != null)
                    txtPbUF_2.Text = est.Nome;
            }

            if (cad.CadastroContatoEndereco != null && cad.CadastroContatoEndereco.Count > 0)
            {
                foreach (var item in cad.CadastroContatoEndereco)
                {
                    if (item.IDCadastroTipoDeContato == 1 && item.Endereco.Contains("@"))
                    {
                        txtPbEmail_2.Text = item.Endereco.ToLower();
                        break;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "fecharModal('myModalPesquisaGenericaCliente');", true);       
        }

        private void CamposPesquisaGenCliente()
        {
            //define os campos de pesquisa
            List<Sistecno.BLL.Helpers.CamposSearch> f = new List<Sistecno.BLL.Helpers.CamposSearch>();
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID", "IDCADASTRO", "70", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "120", "txt", "CNPJ", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CPF", "CNPJCPF_", "100", "txt", "CPF", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("NOME", "RAZAOSOCIALNOME", "150", "", "", "", null));
            dtrPesquisaGenericaCliente.camposPesquisa = f;

            //define a select de origem sem o where
            string sql = "SELECT TOP 10";
            sql += " C.IDCADASTRO ID,  ";
            sql += " C.CNPJCPF [CNPJ/CPF], ";
            sql += " LEFT(C.RAZAOSOCIALNOME, 40) [RAZAO SOCIAL], ";
            sql += " EST.UF, ";
            sql += " CID.NOME CIDADE ";
            sql += " FROM  CADASTRO C  WITH(NOLOCK)   ";
            sql += " LEFT JOIN CIDADE CID ON C.IDCIDADE = CID.IDCIDADE ";
            sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";

            //define lista de restrições, por exemplo filial / empresa.
            // porem o campotem que estar definido no select acima
            dtrPesquisaGenericaCliente.restricoesPesquisa = null;

            //passa o sql para o controle
            dtrPesquisaGenericaCliente.sqlBasico = sql;

            //comando para montar os campos
            dtrPesquisaGenericaCliente.MontarCampos();
        }

        private void CamposPesquisaGenDestinatario()
        {
            //define os campos de pesquisa
            List<Sistecno.BLL.Helpers.CamposSearch> f = new List<Sistecno.BLL.Helpers.CamposSearch>();
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID", "IDCADASTRO", "70", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "120", "txt", "CNPJ", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CPF", "CNPJCPF_", "100", "txt", "CPF", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("NOME", "RAZAOSOCIALNOME", "150", "", "", "", null));
            dtrPesquisaGenericaDetinatario.camposPesquisa = f;

            //define a select de origem sem o where
            string sql = "SELECT TOP 10";
            sql += " C.IDCADASTRO ID,  ";
            sql += " C.CNPJCPF [CNPJ/CPF], ";
            sql += " LEFT(C.RAZAOSOCIALNOME, 40) [RAZAO SOCIAL], ";
            sql += " EST.UF, ";
            sql += " CID.NOME CIDADE ";
            sql += " FROM  CADASTRO C  WITH(NOLOCK)   ";
            sql += " LEFT JOIN CIDADE CID ON C.IDCIDADE = CID.IDCIDADE ";
            sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";

            //define lista de restrições, por exemplo filial / empresa.
            // porem o campotem que estar definido no select acima
            dtrPesquisaGenericaDetinatario.restricoesPesquisa = null;

            //passa o sql para o controle
            dtrPesquisaGenericaDetinatario.sqlBasico = sql;

            //comando para montar os campos
            dtrPesquisaGenericaDetinatario.MontarCampos();
        }

        private void SelecionarItemClicadoDestinatario_Click(object sender, EventArgs e)
        {
            //retorna os dados do registro escolhido na pesquisa(grid) no filtro
            DataRow[] retor = (DataRow[])Session["linhaSel"];

            Sistecno.BLL.Cadastro c = new Sistecno.BLL.Cadastro();

            Sistecno.DAL.Models.Cadastro cad = c.RetornarTabela(int.Parse(dtrPesquisaGenericaDetinatario.ret), cnx);

            txtPbCnpj_1.Text = cad.CnpjCpf;
            txtPbRazaoSocial_1.Text = cad.RazaoSocialNome;
            txtPbId_1.Text = dtrPesquisaGenericaR.ret;
            txtPbIe_1.Text = cad.InscricaoRG;
            txtPbEndereco_1.Text = cad.Endereco;
            txtPbNumero_1.Text = cad.Numero;
            txtPbComplemento_1.Text = cad.Complemento;
            txtPbCEP_1.Text = cad.Cep;
            Sistecno.DAL.Models.Cidade cid = null;
            Sistecno.DAL.Models.Estado est = null;

            if (cad.Cidade != null)
            {
                cid = cad.Cidade;
                est = cid.Estado;
            }

            if (cad.IDBairro.ToString() != "")
            {
                Sistecno.DAL.Models.Bairro bar = cad.Bairro;
                txtPbBairro_1.Text = bar.Nome;
            }

            if (cad.Cidade != null)
            {
                txtPbCidade_1.Text = cid.Nome;

                if (cid.Estado != null)
                    txtPbUF_1.Text = est.Nome;
            }

            if (cad.CadastroContatoEndereco != null && cad.CadastroContatoEndereco.Count > 0)
            {
                foreach (var item in cad.CadastroContatoEndereco)
                {
                    if (item.IDCadastroTipoDeContato == 1 && item.Endereco.Contains("@"))
                    {
                        txtPbEmail_1.Text = item.Endereco.ToLower();
                        break;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "fecharModal('myModalPesquisaGenericaDetinatario');", true);         

        }

        private void SelecionarItemClicado_Click(object sender, EventArgs e)
        {
            hdIdRemetente.Value = dtrPesquisaGenericaR.ret;

            //retorna os dados do registro escolhido na pesquisa(grid) no filtro
            DataRow[] retor = (DataRow[])Session["linhaSel"];

            Sistecno.BLL.Cadastro c = new Sistecno.BLL.Cadastro();
            
             Sistecno.DAL.Models.Cadastro cad = c.RetornarTabela(int.Parse(hdIdRemetente.Value), cnx);

             txtPbCnpj_0.Text = cad.CnpjCpf;
            txtPbRazaoSocial_0.Text = cad.RazaoSocialNome;
            txtPbId_0.Text = dtrPesquisaGenericaR.ret;
            txtPbIe_1.Text = cad.InscricaoRG;
            txtPbEndereco_0.Text = cad.Endereco;
            txtPbNumero_0.Text = cad.Numero;
            txtPbComplemento_0.Text = cad.Complemento;
            txtPbCEP_0.Text = cad.Cep;
            Sistecno.DAL.Models.Cidade cid = null;
            Sistecno.DAL.Models.Estado est = null;

            if (cad.Cidade != null)
            {
                cid = cad.Cidade;
                est = cid.Estado;
            }

            if (cad.IDBairro.ToString() != "")
            {
                Sistecno.DAL.Models.Bairro bar = cad.Bairro;
                txtPbBairro_0.Text = bar.Nome;
            }

            if (cad.Cidade != null)
            {
                txtPbCidade_0.Text = cid.Nome;

                if (cid.Estado != null)
                    txtPbUF_0.Text = est.Nome;
            }

            if (cad.CadastroContatoEndereco != null && cad.CadastroContatoEndereco.Count > 0)
            {
                foreach (var item in cad.CadastroContatoEndereco)
                {
                    if (item.IDCadastroTipoDeContato == 1 && item.Endereco.Contains("@"))
                    {
                        txtPbEmail_0.Text = item.Endereco.ToLower();
                        break;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop1", "fecharModal('myModalPesquisaGenerica');", true);         

        }

        private void CamposPesquisaGenRemetente()
        {

            //define os campos de pesquisa
            List<Sistecno.BLL.Helpers.CamposSearch> f = new List<Sistecno.BLL.Helpers.CamposSearch>();
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID", "IDCADASTRO", "70", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "120", "txt", "CNPJ", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CPF", "CNPJCPF_", "100", "txt", "CPF", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("NOME", "RAZAOSOCIALNOME", "150", "", "", "", null));
            dtrPesquisaGenericaR.camposPesquisa = f;

            //define a select de origem sem o where
            string sql = "SELECT TOP 10";
            sql += " C.IDCADASTRO ID,  ";
            sql += " C.CNPJCPF [CNPJ/CPF], ";           
            sql += " LEFT(C.RAZAOSOCIALNOME, 40) [RAZAO SOCIAL], ";
            sql += " EST.UF, ";
            sql += " CID.NOME CIDADE ";            
            sql += " FROM  CADASTRO C  WITH(NOLOCK)   ";
            sql += " LEFT JOIN CIDADE CID ON C.IDCIDADE = CID.IDCIDADE ";
            sql += " LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ";

            //define lista de restrições, por exemplo filial / empresa.
            // porem o campotem que estar definido no select acima
            dtrPesquisaGenericaR.restricoesPesquisa = null;

            //passa o sql para o controle
            dtrPesquisaGenericaR.sqlBasico = sql;

            //comando para montar os campos
            dtrPesquisaGenericaR.MontarCampos();
        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            List<ParametrosPesquisa> parPesq = new List<ParametrosPesquisa>();
            List<Sistecno.BLL.Helpers.CamposSearch> cp = dtrPesquisa.camposPesquisa;
          
            for (int i = 0; i < cp.Count; i++)
            {
                parPesq.Add(new ParametrosPesquisa(cp[i].NomeCampo.Replace("_", ""), cp[i].Valor, "string"));
            }

            DataTable dt = new Sistecno.BLL.Documento().RetornarPesquisa(parPesq[0].Valor, "", "ORDEM DE SERVICO", (int)usuarioLogado.UltimaFilial, "", true, cnx);
            CriarGrid(dt);
        }
  
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("WEB0009.aspx?opc="+ Request.QueryString["opc"], false);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Sistecno.DAL.Models.Documento d = new Sistecno.DAL.Models.Documento();


                TextBox txtCodigo = new TextBox();
                txtCodigo.Text = Request.QueryString["id"];

                if (txtPbId_0.Text == "0" || txtPbId_0.Text == "")
                    throw new Exception("Informe o Destinatário");

                if (txtPbId_2.Text == "0" || txtPbId_2.Text == "")
                    throw new Exception("Informe o Destinatário");

                if (txtPbId_1.Text == "0" || txtPbId_1.Text == "")
                    throw new Exception("Informe o Destinatário");


                d.IDDocumento = int.Parse(txtCodigo.Text);
                d.DataDeEmissao = (txtDataDeCadastro.Text == "" ? (DateTime?)null : DateTime.Parse(txtDataDeCadastro.Text));
                d.DataDeEntrada = (txtDataDeCadastro.Text == "" ? (DateTime?)null : DateTime.Parse(txtDataDeCadastro.Text));
                d.DataPlanejada = (txtDataDeCadastro.Text == "" ? (DateTime?)null : DateTime.Parse(txtDataDeCadastro.Text));
                d.DocumentodoCliente4 = txtNotaFiscalDocumento.Text;
                d.Natureza = txtNatureza.Text;
                d.Numero = int.Parse(txtNumero.Text);

                d.PesoBruto = (txtPesoReal.Text == "" ? (decimal?)null : decimal.Parse(txtPesoReal.Text));
                d.PesoCubado = (txtPesoCubado.Text == "" ? (decimal?)null : decimal.Parse(txtPesoCubado.Text));
                d.Volumes = (txtVolumes.Text == "" ? (decimal?)null : decimal.Parse(txtVolumes.Text));
                d.ValorDaNota = (txtValorDocumento.Text == "" ? (decimal?)null : decimal.Parse(txtValorDocumento.Text));
                d.Especie = txtVeiculoSugerido.Text;

                d.DocumentoDoCliente2 = txtContatoTelefone.Text;
                d.IDModal = 1;

                Sistecno.DAL.Models.DocumentoObservacao obs = new Sistecno.DAL.Models.DocumentoObservacao();
                obs.Observacao = txtObservacao.Text;
                obs.IDDocumentoObservacao = int.Parse(lblIdObserv.Text);

                d.DocumentoObservacao.Add(obs);
                d.IDCliente = int.Parse(txtPbId_2.Text);
                d.IDRemetente = int.Parse(txtPbId_0.Text);
                d.IDDestinatario = int.Parse(txtPbId_1.Text);
                d.TipoDeServico = "TRANSPORTE";
                d.TipoDeDocumento = "ORDEM DE SERVICO";

                new Sistecno.BLL.Documento().Gravar(d, cnx);
                Response.Redirect("WEB0009.aspx?opc=" + Request.QueryString["opc"], false);
                Notificar("Cadastro efetuado com sucesso", "Aviso");
            }
            catch (Exception ex)
            {
                Notificar(ex.Message, "Atenção");
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void imgBuscarRemetente_Click(object sender, ImageClickEventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('myModalPesquisaGenerica');", true);
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            //  List<ParametrosPesquisa> parPesq = new List<ParametrosPesquisa>();
            ////  parPesq.Add(new ParametrosPesquisa("CNPJCPF", txtPesquisaRem.Text, "string"));
            //  parPesq.Add(new ParametrosPesquisa("RAZAOSOCIALNOME", txtPesquisaRem.Text, "string"));             


            //  DataTable dt = new Sistecno.BLL.Cadastro().Retornar(parPesq, cnx);
            //  grdRemPesq.DataSource = dt;
            //  grdRemPesq.DataBind();

        }

        #endregion
        
        #region Metodos

        private void CarregarCampos()
        {
            try
            {


                //Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Modelo().Retornar(cnx), ref cboModelo, false, true, "IDVeiculoModelo", "Nome");
                //Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Tipo().Retornar(cnx), ref cboTipo, false, true, "IDveiculoTipo", "Nome");
                //Combo.CarregarCombo(new Sistecno.BLL.Veiculo.Rastreador().Retornar(cnx), ref cboRastreador, false, true, "IDVeiculoRastreador", "Nome");
                //Combo.CarregarCombo((DataTable)Session["dtUF"], ref cboMotoristaEstado, true, true, "IDESTADO", "NOME");
                //Combo.CarregarCombo((DataTable)Session["dtUF"], ref cboProprietarioEstado, true, true, "IDESTADO", "NOME");


                //Sistecno.DAL.Models.Veiculo v = new Sistecno.BLL.Veiculo().RetornarAllFields(codigoVeiculo, cnx);

                //if (v != null)
                //{
                //    hdIdVeiculo.Value = v.IDVeiculo.ToString();
                //    cboModelo.SelectedValue = v.IDVeiculoModelo.ToString();
                //    txtAnoFabricacao.Text = v.Ano.ToString();
                //    txtAnoModelo.Text = v.AnoModelo.ToString();
                //    txtPlaca.Text = v.Placa;
                //    txtCor.Text = v.Cor;
                //    cboTipo.SelectedValue = v.IDVeiculoTipo.ToString();
                //    txtEixos.Text = v.QuatidadeDeEixos.ToString();

                //    txtCapacidadeCargakg.Text = (v.CapacidadeDeCargaKG.ToString() == "" ? "0" : Convert.ToInt32(decimal.Parse(v.CapacidadeDeCargaKG.ToString())).ToString());
                //    txtCapacidadeCargam3.Text = (v.CapacidadeDeCargaM3.ToString() == "" ? "0" : Convert.ToInt32(decimal.Parse(v.CapacidadeDeCargaM3.ToString())).ToString());
                //    cboCategoriaPermitida.SelectedValue = v.CategoriasDeCNHPermitidas;
                //    cboRastreador.SelectedValue = v.IDVeiculoRastreador.ToString();
                //    txtSerieRastreador.Text = v.NumeroSerieEquipamento;
                //    txtChassi.Text = v.Chassi;
                //    txtRenavam.Text = v.Renavam;
                //    txtAntt.Text = v.Antt;
                //    txtVencAntt.Text = v.AnttVencimento.ToString();
                //    txtDataLicenciamento.Text = v.DataDeLicenciamento.ToString();


                //    Sistecno.DAL.Models.Cadastro cm = null;
                //    Sistecno.DAL.Models.Motorista mot = null;

                //    if (mot == null)
                //        mot = new Sistecno.BLL.Cadastro.Motorista().RetornarTable((int)v.IDMotorista, cnx);

                //    if (cm == null)
                //        cm = new Sistecno.BLL.Cadastro().RetornarTabela((int)v.IDMotorista, cnx);


                //    Sistecno.DAL.Models.Cidade cid = null;
                //    Sistecno.DAL.Models.Estado est = null;

                //    if (cm.Cidade != null)
                //    {
                //        cid = cm.Cidade;
                //        est = cid.Estado;
                //    }


                //    txtMotoristaCpf.Text = cm.CnpjCpf;
                //    txtMotoristaNome.Text = cm.RazaoSocialNome;
                //    txtMotoristaEndreco.Text = cm.Endereco;
                //    txtMotoristaNumero.Text = cm.Numero;

                //    if (cid != null && est.IDEstado > 0)
                //    {
                //        cboMotoristaEstado.SelectedValue = est.IDEstado.ToString();
                //        CarregarCidadesCbo(cboMotoristaEstado, cboMotoristaCidade);
                //        cboMotoristaCidade.SelectedValue = cm.IDCidade.ToString();
                //    }

                //    hdIdMotorista.Value = cm.IDCadastro.ToString();
                //    txtMotoristaCEP.Text = cm.Cep;
                //    txtMotoristaRg.Text = cm.InscricaoRG;
                //    txtMotoristaCNH.Text = mot.NumeroRegistroCNH;
                //    txtMotoristaDataDeNascimento.Text = mot.DataDeNascimento.ToString();
                //    txtMotoristaCategoria.Text = mot.CarteiraDeHabilitacao;


                //    Sistecno.DAL.Models.Cadastro cp = new Sistecno.BLL.Cadastro().RetornarTabela((int)v.IDProprietario, cnx);

                //    hdIdProprietario.Value = cp.IDCadastro.ToString();
                //    txtProprietarioCEP.Text = cp.Cep;
                //    txtProprietarioComplemento.Text = cp.Complemento;
                //    txtProprietarioCpf.Text = cp.CnpjCpf;
                //    txtProprietarioEndereco.Text = cp.Endereco;
                //    txtProprietarioNome.Text = cp.RazaoSocialNome;
                //    txtProprietarioNumero.Text = cp.Numero;

                //    cid = null;
                //    est = null;

                //    if (cp.Cidade != null)
                //    {
                //        cid = cp.Cidade;
                //        est = cid.Estado;
                //    }

                //    if (cid != null && est.IDEstado > 0)
                //    {
                //        cboProprietarioEstado.SelectedValue = est.IDEstado.ToString();
                //        CarregarCidadesCbo(cboProprietarioEstado, cboProprietarioCidade);
                //        cboProprietarioCidade.SelectedValue = cp.IDCidade.ToString();
                //    }
                //}
            }
            catch (Exception ec)
            {
                Notificar(ec.Message, "Erro");
            }
        }

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
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("NUMERO", "NUMERO", "70", "txt", "SomenteNumero", "", null));
            dtrPesquisa.camposPesquisa = f;
        }

        private void CriarGrid(DataTable dt)
        {                       
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB0009.ASPX", Request.QueryString["opc"])));
        }

        private void PrepararTelaEdicao()
        {
            dvManut.Visible = true;
            dvPesq.Visible = false;
            dvbot.Visible = false;
            dvGrid.Visible = false;


            if (Request.QueryString["acao"] == "editar")
            {
                CarregarCampos();
            }
            else if (Request.QueryString["acao"] == "novo")
            {
                Sistecno.DAL.Models.Cadastro cadSol = new Sistecno.BLL.Cadastro().RetornarTabela((int)usuarioLogado.IDCadastro, cnx);
                txtSolicitanteNome.Text = cadSol.RazaoSocialNome;

                foreach (var item in cadSol.CadastroContatoEndereco)
                {
                    if ((item.IDCadastroTipoDeContato == 2 || item.IDCadastroTipoDeContato == 3 || item.IDCadastroTipoDeContato == 4) && item.Endereco != "")
                        txtSolicitanteTelefone.Text = item.Endereco;
                    else if (item.IDCadastroTipoDeContato == 1 && item.Endereco != "")
                        txtSolicitanteEmial.Text = item.Endereco;
                }

                txtDataDeCadastro.Text = DateTime.Now.ToString();
                txtNumero.Text = Sistecno.BLL.Documento.Numerador.RetornarNumerador(int.Parse(usuarioLogado.UltimaEmpresa.ToString()), (int)usuarioLogado.UltimaFilial, "ORDEM SERVICO COLETA", "", cnx);

                //txtDataDeColeta.Focus();
            }
        }
        #endregion

        protected void imgBuscarDestinatario_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('myModalPesquisaGenericaDetinatario');", true);
        }

        protected void imgBuscarCliente_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('myModalPesquisaGenericaCliente');", true);

        }

    }

}