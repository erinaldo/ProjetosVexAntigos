using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Globalization;
using Util;
using System.Web;
using Sistecno.DAL;
using Sistecno.DAL.BD;

namespace Sistecno.UI.Web
{
    public partial class WEB0004 : System.Web.UI.Page
    {
        #region Variaveis de Escopo Global na Pagina
        
        string cnx = "";
        Sistecno.DAL.Models.Usuario usuarioLogado;
        DataTable dtContato;
        string idEmpresa = "";

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
                DataTable dt = new Sistecno.BLL.EmpresaFilial().Retornar(null, (int)usuarioLogado.UltimaEmpresa, cnx);
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
            verificarDataTableContato();
            verificarDataTableContato();
            CriarGridMeioDeContato(dtContato);

            #endregion
        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            //localiza os controles de pesquisa
            List<ParametrosPesquisa> parPesq = new List<ParametrosPesquisa>();
            List<Sistecno.BLL.Helpers.CamposSearch> cp = dtrPesquisa.camposPesquisa;
            for (int i = 0; i < cp.Count; i++)
            {
                if (cp[i].Valor.Length > 0)
                    parPesq.Add(new ParametrosPesquisa(cp[i].NomeCampo.Replace("_", ""), cp[i].Valor, "string"));
            }


            DataTable dt = new Sistecno.BLL.EmpresaFilial().Retornar(parPesq, (int)usuarioLogado.UltimaEmpresa, cnx);
            CriarGrid(dt);
        }
        
       

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("WEB0004.aspx?opc="+ Request.QueryString["opc"], false);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCNPJCadastro.Text == "")
                    throw new Exception("CPF é obrigatório");

                if (txtRazaoSocialNome.Text == "")
                    throw new Exception("NOME é obrigatório");

                if (!Sistecno.BLL.Helpers.Util.Validacoes.CnpjValido(txtCNPJCadastro.Text) && !Sistecno.BLL.Helpers.Util.Validacoes.CpfValido(txtCNPJCadastro.Text))
                    throw new Exception("CNPJ ou CPF inválido");


                Sistecno.DAL.Models.Empresa objEmpresa = new Sistecno.DAL.Models.Empresa();

                if (lblIdEmpresa.Text == "")
                    lblIdEmpresa.Text = "0";

                if (txtAliquotaSimples.Text == "")
                    txtAliquotaSimples.Text = "0";

                if (lblIdContaDeEmail.Text == "")
                    lblIdContaDeEmail.Text = "0";

                objEmpresa.PermiteCNPJErrado = "NAO";
                objEmpresa.PermiteIEErrada = "NAO";
                objEmpresa.Nome = txtNomeEmpresaSimplificado.Text.ToUpper();
                objEmpresa.AliquotaSimples = decimal.Parse(txtAliquotaSimples.Text);
                objEmpresa.OptanteSimples = cboOptanteSimples.SelectedValue;
                objEmpresa.Ativo = cboEmpresaAtiva.SelectedValue;


                Sistecno.DAL.Models.ContaDeEmail cm = new Sistecno.DAL.Models.ContaDeEmail();


                if (txtSenhaAviso.Text.Length > 0 && txtEmailAviso.Text.Length > 0)
                {
                    cm.IdContaDeEmail = int.Parse(lblIdContaDeEmail.Text);
                    cm.Operacao = "CTE";
                    cm.SMTP = txtSmtpAviso.Text;
                    cm.Para = "";
                    cm.Porta = int.Parse(txtPortaAviso.Text);
                    cm.Senha = txtSenhaAviso.Text;
                    cm.CCopia = txtCopiaAviso.Text;
                    cm.De = txtEmailAviso.Text;
                    cm.DeApelido = txtApelidoAviso.Text;

                    if (lblIdContaDeEmail.Text != "0")
                        new Sistecno.BLL.ContaDeEmail().Alterar(cm, cnx);
                    else
                        new Sistecno.BLL.ContaDeEmail().Gravar(cm, cnx);
                }

                if (lblIdEmpresa.Text == "" || lblIdEmpresa.Text == "0")
                    lblIdEmpresa.Text = new Sistecno.BLL.EmpresaFilial().InserirEmpresa(objEmpresa, cnx).ToString();
                else
                {
                    objEmpresa.IDEmpresa = int.Parse(lblIdEmpresa.Text);
                    new Sistecno.BLL.EmpresaFilial().AlteraEmpresa(objEmpresa, cnx);
                }

                Sistecno.DAL.Plano p = (Sistecno.DAL.Plano)Session["PLANOCLIENTE"];
                string v = p.NomeCliente.Replace(".", "").Replace("-", "").Replace("-", "").Replace(" ", "").Substring(0, 10) + Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(p.IdCliente.ToString(), 4);

                if (File.Exists(MapPath("~/Logotipo/") + Session["caminhoImagem"]))
                {
                    System.Drawing.Image imagem = System.Drawing.Bitmap.FromFile(MapPath("~/LogoTipo/") + Session["caminhoImagem"].ToString().Replace(".jpg", ".bmp"));
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    new Sistecno.BLL.Cadastro().GravarImagens(objEmpresa.IDEmpresa, int.Parse((lblIdCadastroImagem.Text == "" ? "0" : lblIdCadastroImagem.Text)), ms.ToArray(), cnx);
                }

                //Prepara o objeto cadastro da filial
                Sistecno.DAL.Models.Cadastro oC = new Sistecno.DAL.Models.Cadastro();
                oC.IDCadastro = int.Parse(lblIdFilial.Text);
                oC.CnpjCpf = txtCNPJCadastro.Text;
                oC.RazaoSocialNome = txtRazaoSocialNome.Text.ToUpper();
                oC.FantasiaApelido = (txtFantasiaApelido.Text == "" ? txtRazaoSocialNome.Text : txtFantasiaApelido.Text).ToUpper();
                oC.Cep = txtCEP.Text;
                oC.Endereco = txtEndereco.Text.ToUpper();
                oC.Numero = txtNumero.Text;
                oC.Complemento = txtComplemento.Text.ToUpper();
                oC.IDCidade = (cboCidade.SelectedIndex > 0 ? int.Parse(cboCidade.SelectedValue) : (int?)null);

                oC.InscricaoRG = txtRG.Text;
                oC.DataDeCadastro = DateTime.Now;
                oC.InscricaoMunicipal = txtInscricaoMunicipal.Text;

                if (cboBairro.SelectedIndex > 0)
                    oC.IDBairro = int.Parse(cboBairro.SelectedValue);

                DataTable dtContato = (DataTable)Session["dtContato"];

                oC.IDCadastro = new Sistecno.BLL.Cadastro().GravarComImagem(oC, dtContato, null, Session["CNX"].ToString());


                Sistecno.DAL.Models.Filial ff = new Sistecno.DAL.Models.Filial();

                ff.IDCadastro = oC.IDCadastro;
                ff.Nome = oC.FantasiaApelido;
                ff.IDEmpresa = int.Parse(lblIdEmpresa.Text);
                ff.Ativo = "SIM";
                ff.NumeroDaFilial = oC.IDCadastro;
                ff.Unidade = oC.IDCadastro;

                if (lblIdFilial.Text == "0")
                    new Sistecno.BLL.Filial().Inserir(ff, cnx);
                else
                {
                    ff.IDFilial = int.Parse(lblIdFilial.Text);
                    new Sistecno.BLL.Filial().Alterar(ff, cnx);
                }
                Notificar("Cadastro de: " + txtRazaoSocialNome.Text + " foi efetuado com sucesso", "Aviso");
                Response.Redirect("WEB0004.aspx?opc=" + Request.QueryString["opc"], false);
                

            }
            catch (Exception ex)
            {
                Notificar("Messangem: " + ex.Message + "-" + ex.InnerException, "Atenção.");
            }
        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.SelectedIndex > 0)
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(cnx, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, true, true, "IDCIDADE", "NOME");
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
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(cnx, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");
            else
            {
                cboBairro.Items.Clear();
                cboBairro.Items.Add("SELECIONE A CIDADE");
            }
        }
             

        private void PrepararTelaEdicao()
        {
            dvManut.Visible = true;
            dvPesq.Visible = false;
            dvbot.Visible = false;
            dvGrid.Visible = false;

            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado, true, true, "IDESTADO", "UF");
            cboCidade.Items.Insert(0, "SELECIONE A CIDADE");
            cboBairro.Items.Add("SELECIONE O BAIRRO");


            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado_Empresa, true, true, "IDESTADO", "UF");
            cboCidade_Empresa.Items.Insert(0, "SELECIONE A CIDADE");
            txtPbBairro.Items.Add("SELECIONE O BAIRRO");

            Combo.CarregarCombo(new Sistecno.BLL.Cadastro.TipoDeContato().Retornar(cnx), ref cboTipoDeEndereco, false, true, "IDCADASTROTIPODECONTATO", "NOME");

            if (Request.QueryString["acao"] == "editar")
            {
                CarregarCamposDoBancoFilial();
            }
            else
            {
                FileUploadCentificado.Enabled = false;
                txtSenhaDoCertificado.Enabled= false;
                btnIInstalar.Enabled = false;
                txtStatusCertificado.Enabled = false;
                txtValidadeCertificado.Enabled = false;
                txtNomeCertificado.Enabled = false;

                txtSenhaDoCertificado.CssClass = "form-control input-xs";
                txtStatusCertificado.CssClass = "form-control input-xs";
                txtValidadeCertificado.CssClass = "form-control input-xs";
                txtNomeCertificado.CssClass = "form-control input-xs";
                lblIdFilial.Text = "0";
                 
            }

            CarregarEmpresa();
            txtNomeEmpresaSimplificado.Focus();
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
            
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CODIGO", "IDCADASTRO", "50", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "100", "txt", "CNPJ", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("EMPRESA", "RAZAOSOCIALNOME", "150", "", "", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("FILIAL", "E.NOME", "150", "", "", "", null));
            
            dtrPesquisa.camposPesquisa = f;
        }

        private void CriarGrid(DataTable dt)
        {                       
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB0004.ASPX", Request.QueryString["opc"])));
        }

        private void CarregarEmpresa()
        {           

            Sistecno.BLL.Cadastro cads = new Sistecno.BLL.Cadastro();

            Sistecno.DAL.Models.Cadastro cad = cads.RetornarTabela((int)usuarioLogado.UltimaEmpresa, cnx);
            lblIdEmpresa.Text = usuarioLogado.UltimaEmpresa.ToString();
            DataTable emp = new Sistecno.BLL.EmpresaFilial().RetornarEmpresa(cad.IDCadastro, cnx);
        
            if (emp.Rows.Count == 0) //cadastro mas sem a empresa cadastrada
            {
                txtNomeEmpresaSimplificado.Text = cad.FantasiaApelido;
            }
            else
            {
                txtNomeEmpresaSimplificado.Text = emp.Rows[0]["NOME"].ToString();
                cboEmpresaAtiva.SelectedValue = emp.Rows[0]["ATIVO"].ToString();
                cboOptanteSimples.SelectedValue = emp.Rows[0]["OPTANTESIMPLES"].ToString();
                txtAliquotaSimples.Text = decimal.Parse((emp.Rows[0]["AliquotaSimples"].ToString() == "" ? "0" : emp.Rows[0]["AliquotaSimples"].ToString())).ToString("#0.00");
            }

            txtPbId.Text = cad.IDCadastro.ToString();
            txtPbCnpj.Text = cad.CnpjCpf;
            txtPbIe.Text = cad.InscricaoRG;
            txtPbRazaoSocial.Text = cad.RazaoSocialNome;
            txtPbEndereco.Text = cad.Endereco;
            txtPbNumero.Text = cad.Numero;
            txtPbComplemento.Text = cad.Complemento;
            txtPbCEP.Text = cad.Cep;
            txtPbNomeFantasia.Text = cad.FantasiaApelido;


            if (cad.CadastroContatoEndereco != null && cad.CadastroContatoEndereco.Count > 0)
            {
                foreach (var item in cad.CadastroContatoEndereco)
                {
                    if (item.IDCadastroTipoDeContato == 1 && item.Endereco.Contains("@"))
                    {
                        txtPbEmail.Text = item.Endereco.ToLower();
                        break;
                    }
                }
            }

            Sistecno.DAL.Models.Cidade cid = cad.Cidade;
            Sistecno.DAL.Models.Estado est = cid.Estado;

            if (cad.IDBairro.ToString() != "")
            {
                Sistecno.DAL.Models.Bairro bar = cad.Bairro;
               // txtPbBairro.Text = bar.Nome;
            }

            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado_Empresa, true, true, "IDESTADO", "UF");

            if (cad.Cidade != null)
            {


                cboEstado_Empresa.SelectedValue = est.IDEstado.ToString();
                Sistecno.BLL.PaisUfCidade oCidEst = new Sistecno.BLL.PaisUfCidade();
                Combo.CarregarCombo(oCidEst.RetornarCidade(cnx, int.Parse(cboEstado_Empresa.SelectedValue), ""), ref cboCidade_Empresa, false, true, "IDCIDADE", "NOME");
                cboCidade_Empresa.SelectedValue = cad.IDCidade.ToString();

                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(cnx, int.Parse(cboCidade_Empresa.SelectedValue), ""), ref txtPbBairro, false, true, "IDBAIRRO", "NOME");

                if (cad.IDBairro.ToString().Length > 0)
                    txtPbBairro.SelectedValue = cad.IDBairro.ToString();
            }

            CarregarContaEmail();
            CarregarLogo(cad.IDCadastro);


        }

        private void CarregarCamposDoBancoFilial()
        {
            try
            {
                
                int codigoFil = int.Parse(Request.QueryString["id"]);
                lblIdFilial.Text = Request.QueryString["id"];
                DataSet ds = new Sistecno.BLL.Filial().RetornarTodosCampos(codigoFil, cnx);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region Cadastro

                    txtNomeFilial.Text = ds.Tables[0].Rows[0]["NOME"].ToString();
                    txtNumeroFilial.Text = ds.Tables[0].Rows[0]["NUMERODAFILIAL"].ToString();
                    txtUnidade.Text = ds.Tables[0].Rows[0]["UNIDADE"].ToString();
                    txtCNPJCadastro.Text = ds.Tables[0].Rows[0]["CnpjCpf"].ToString();
                    txtRG.Text = ds.Tables[0].Rows[0]["InscricaoRG"].ToString();
                    txtRazaoSocialNome.Text = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();
                    txtFantasiaApelido.Text = ds.Tables[0].Rows[0]["FantasiaApelido"].ToString();
                    txtEndereco.Text = ds.Tables[0].Rows[0]["Endereco"].ToString();
                    txtNumero.Text = ds.Tables[0].Rows[0]["Numero"].ToString();
                    txtComplemento.Text = ds.Tables[0].Rows[0]["Complemento"].ToString();
                    txtCEP.Text = ds.Tables[0].Rows[0]["Cep"].ToString();
                    txtInscricaoMunicipal.Text = ds.Tables[0].Rows[0]["InscricaoMunicipal"].ToString();

                    idEmpresa = ds.Tables[0].Rows[0]["IDEmpresa"].ToString();
                  
                    cboCidade.Items.Insert(0, "SELECIONE A CIDADE");
                    cboBairro.Items.Add("SELECIONE O BAIRRO");
                    

           

                   if (ds.Tables[0].Rows[0]["IDCidade"].ToString().Length > 0)
                   {
                       if (ds.Tables[0].Rows[0]["IDESTADO"].ToString().Length > 0)
                       {
                           cboEstado.SelectedValue = ds.Tables[0].Rows[0]["IDESTADO"].ToString();
                           if (cboEstado.SelectedValue.Length > 0)
                           {
                               Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, true, true, "IDCIDADE", "NOME");
                               cboCidade.SelectedValue = ds.Tables[0].Rows[0]["IDCidade"].ToString();
                               Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");

                               if (ds.Tables[0].Rows[0]["IDBAIRRO"].ToString().Length > 0)
                                   cboBairro.SelectedValue = ds.Tables[0].Rows[0]["IDBAIRRO"].ToString();
                           }
                       }
                   }

                    #endregion

                    #region Contato

                    verificarDataTableContato();
                    Session["dtContato"] = null;
                    dtContato.Rows.Clear();                   
                    cboTipoDeEndereco.SelectedIndex = 0;

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows[i]["IDCADASTROCONTATOENDERECO"].ToString().Length > 0)
                        {
                            DataRow dr = dtContato.NewRow();
                            dr["IDCADASTROCONTATOENDERECO"] = ds.Tables[1].Rows[i]["IDCADASTROCONTATOENDERECO"].ToString();
                            dr["IDCADASTRO"] = ds.Tables[1].Rows[i]["IDCADASTRO"].ToString();
                            dr["IDCADASTROTIPODECONTATO"] = ds.Tables[1].Rows[i]["IDCADASTROTIPODECONTATO"].ToString();
                            dr["TIPODECONTATO"] = ds.Tables[1].Rows[i]["TIPODECONTATO"].ToString();
                            dr["ENDCONTADO"] = ds.Tables[1].Rows[i]["ENDCONTADO"].ToString();
                            dr["STATUS"] = "SIM";

                            string seq = dtContato.Compute("MAX(SEQUENCIA)", "").ToString();

                            dr["SEQUENCIA"] = seq == "" ? "1" : (int.Parse(seq) + 1).ToString();

                            dtContato.Rows.Add(dr);
                        }
                    }

                    CriarGridMeioDeContato(dtContato);

             Session["dtContato"] = dtContato;

            
                  #endregion

                    #region Informações do Certificado


                    Sistecno.DAL.Plano p = (Sistecno.DAL.Plano)Session["PLANOCLIENTE"];

                    if (p == null)
                        return;

                    string v = p.NomeCliente.Replace(".", "").Replace("-", "").Replace("-", "").Replace(" ", "").Substring(0, 10) + Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(p.IdCliente.ToString(), 4);
                    txtStatusCertificado.Text = "PENDENTE DE INSTALAÇÃO";

                    X509Store stores = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    try
                    {
                        // Abre o Store
                        stores.Open(OpenFlags.ReadOnly);

                        // Obtém a coleção dos certificados da Store
                        X509Certificate2Collection certificados = stores.Certificates;

                        // percorre a coleção de certificados
                        foreach (X509Certificate2 certificado in certificados)
                        {
                            if (certificado.Subject.Contains(txtCNPJCadastro.Text.Replace("-", "").Replace(".", "").Replace("/", "")))
                            {
                                txtValidadeCertificado.Text = certificado.NotAfter.ToString();
                                txtNomeCertificado.Text = certificado.FriendlyName;
                                txtStatusCertificado.Text = "INSTALADO";
                                break;
                            }
                        }
                    }
                    finally
                    {
                        stores.Close();
                    }
                    #endregion
 
               }
            }
            catch (Exception ex )
            {
                Notificar(ex.Message, "Aviso");
            }
        }
            

        #endregion

        protected void btnAdicionarMeioContato_Click(object sender, EventArgs e)
        {
            if (cboTipoDeEndereco.SelectedIndex > 0 && txtEnderecoMeioDeContato.Text.Length > 0)
            {
                verificarDataTableContato();

                if (lblSequencia.Text.Length > 0)
                {
                    for (int i = 0; i < dtContato.Rows.Count; i++)
                    {
                        if (lblSequencia.Text == dtContato.Rows[i]["SEQUENCIA"].ToString())
                        {
                            dtContato.Rows[i]["ENDCONTADO"] = txtEnderecoMeioDeContato.Text.ToUpper();
                            break;
                        }
                    }
                }
                else
                {
                    DataRow dtr = dtContato.NewRow();
                    dtr["IDCADASTROCONTATOENDERECO"] = 0;
                    dtr["IDCADASTRO"] = 0;
                    dtr["IDCADASTROTIPODECONTATO"] = cboTipoDeEndereco.SelectedValue;
                    dtr["TIPODECONTATO"] = cboTipoDeEndereco.SelectedItem.Text;
                    dtr["ENDCONTADO"] = txtEnderecoMeioDeContato.Text;
                    dtr["ANIVERSARIO"] = "";
                    dtr["STATUS"] = "SIM";
                    string seq = dtContato.Compute("MAX(SEQUENCIA)", "").ToString();
                    dtr["SEQUENCIA"] = seq == "" ? "1" : (int.Parse(seq) + 1).ToString();
                    dtContato.Rows.Add(dtr);
                }

                Session["dtContato"] = dtContato;
                CriarGridMeioDeContato(dtContato);



                txtEnderecoMeioDeContato.Text = "";
                cboTipoDeEndereco.SelectedIndex = 0;
                lblSequencia.Text = "";


                txtEnderecoMeioDeContato.CssClass = "form-control input-xs";
                cboTipoDeEndereco.CssClass = "input-xs";
                txtEnderecoMeioDeContato.Focus();
            }
        }

        private void ExcluirMeioDeContatos_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).ID.Replace("btn_", "");
            dtContato = (DataTable)Session["dtContato"];

            for (int i = 0; i < dtContato.Rows.Count; i++)
            {
                if (dtContato.Rows[i]["SEQUENCIA"].ToString() == id)
                {
                    dtContato.Rows[i]["Status"] = "NAO";
                    break;
                }

            }

            Session["dtContato"] = dtContato;
            CriarGridMeioDeContato(dtContato);

        }

        public void CriarGridMeioDeContato(DataTable dados)
        {
            phMeioDeContatos.Controls.Clear();
            phMeioDeContatos.Controls.Add(new LiteralControl(" <table class='table table-bordered table-striped'>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <thead>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <tr>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <th>CÓDIGO</th>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <th>TIPO</th>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <th>ENDEREÇO</th>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <th></th>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" 	</tr>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" </thead>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" <tbody>"));
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                if (dados.Rows[i]["STATUS"].ToString() == "SIM")
                {
                    phMeioDeContatos.Controls.Add(new LiteralControl(" <tr>"));
                    phMeioDeContatos.Controls.Add(new LiteralControl(" <td>" + dados.Rows[i]["IDCADASTROCONTATOENDERECO"].ToString() + "</td>"));
                    phMeioDeContatos.Controls.Add(new LiteralControl(" <td>" + dados.Rows[i]["TIPODECONTATO"].ToString() + "</td>"));
                    phMeioDeContatos.Controls.Add(new LiteralControl(" <td>" + dados.Rows[i]["ENDCONTADO"].ToString() + "</td>"));
                    //ph.Controls.Add(new LiteralControl( " <td style='width:1%'><a class='btn btn-danger btn-xs' href='javascript:void(0)'; runat='server' >Excluir</a></td>"));

                    phMeioDeContatos.Controls.Add(new LiteralControl("<td style='width:1%'>"));
                    phMeioDeContatos.Controls.Add(this.CriarExcluir(dados.Rows[i]["SEQUENCIA"].ToString()));

                    phMeioDeContatos.Controls.Add(new LiteralControl("</td>"));
                    phMeioDeContatos.Controls.Add(new LiteralControl(" </tr>"));
                }
            }

            phMeioDeContatos.Controls.Add(new LiteralControl(" </tbody>"));
            phMeioDeContatos.Controls.Add(new LiteralControl(" </table>"));

        }

        public Button CriarExcluir(string sequencia)
        {
            Button b = new Button();
            b.Text = "Excluir";
            b.CssClass = "btn btn-danger btn-xs";
            b.ID = "btn_" + sequencia;

            b.Click += new System.EventHandler(this.ExcluirMeioDeContatos_Click);
            return b;
        }

        private void verificarDataTableContato()
        {
            if (Session["dtContato"] == null)
            {
                dtContato = new DataTable();

                dtContato.Columns.Add("IDCADASTROCONTATOENDERECO");
                dtContato.Columns.Add("IDCADASTRO");
                dtContato.Columns.Add("IDCADASTROTIPODECONTATO");
                dtContato.Columns.Add("TIPODECONTATO");
                dtContato.Columns.Add("ENDCONTADO");
                dtContato.Columns.Add("ANIVERSARIO");
                dtContato.Columns.Add("STATUS");
                dtContato.Columns.Add("SEQUENCIA");
            }
            else
                dtContato = (DataTable)Session["dtContato"];

        }

        private void CarregarLogo(int idCdadastroEmpresa)
        {
            imgLogo.ImageUrl = "LogoTipo/expressTms.png";
            Sistecno.DAL.Plano p = (Sistecno.DAL.Plano)Session["PLANOCLIENTE"];
            string v = p.NomeCliente.Replace(".", "").Replace("-", "").Replace("-", "").Replace(" ", "").Substring(0, 10) + Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(p.IdCliente.ToString(), 4);

            DataTable dtImg = DAL.CadastroImagem.RetornarImagemSite(idCdadastroEmpresa, false, cnx);

            //SE NAO ENCONTROU A IMAGEM DO SITE PEGA A PRIMEIRA
            if (dtImg.Rows.Count == 0)
            {
                dtImg = DAL.CadastroImagem.RetornarImagemSite(idCdadastroEmpresa, true, cnx);
            }

            if (dtImg.Rows.Count == 0)
                return;

            if (dtImg.Rows[0][1] == DBNull.Value)
                return;

            byte[] imagem = (byte[])dtImg.Rows[0][1];

            lblIdCadastroImagem.Text = dtImg.Rows[0][0].ToString();

            if (imagem != null)
            {
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(MapPath("~/LogoTipo/") + idCdadastroEmpresa + ".bmp");

                using (Bitmap img = new Bitmap(MapPath("~/LogoTipo/") + idCdadastroEmpresa.ToString() + ".bmp"))
                {
                   ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(enc => enc.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                    System.Drawing.Imaging.EncoderParameters imgParams = new System.Drawing.Imaging.EncoderParameters(1);
                    imgParams.Param = new[] { new System.Drawing.Imaging.EncoderParameter(Encoder.Quality, 75L) };
                    img.Save(MapPath("~/LogoTipo/") + idCdadastroEmpresa.ToString() + "_compress.bmp", codec, imgParams);
                    img.Save(MapPath("~/LogoTipo/") + idCdadastroEmpresa.ToString() + ".jpg", codec, imgParams);
                }

                imgLogo.ImageUrl = "LogoTipo/" + idCdadastroEmpresa.ToString() + ".jpg";
                Session["caminhoImagem"] = idCdadastroEmpresa.ToString() + ".jpg";

            }
        }

        private void CarregarContaEmail()
        {
            //Sistecno.BLL.ContaDeEmail ob = new Sistecno.BLL.ContaDeEmail().Retornar("CTE", cnx);

            //if (ob != null)
            //{
                //txtApelidoAviso.Text = ob.apelido;
                //txtCopiaAviso.Text = ob.destinatarios_email;
                //txtEmailAviso.Text = ob.destinatarios_email;
                //txtPortaAviso.Text = ob.porta.ToString();
                //txtSenhaAviso.Text = ob.senha;
                //txtSmtpAviso.Text = ob.smtp;
                //lblIdContaDeEmail.Text = ob.IdContaDeEmail.ToString();
            //}
        }

        protected void cboEstado_Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado_Empresa.SelectedIndex > 0)
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(new DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboEstado_Empresa.SelectedValue), ""), ref cboCidade_Empresa, true, true, "IDCIDADE", "NOME");
            else
            {
                cboCidade_Empresa.Items.Clear();
                cboCidade_Empresa.Items.Clear();
                cboCidade_Empresa.Items.Add("SELECIONE O ESTADO");
                cboCidade_Empresa.Items.Add("SELECIONE A CIDADE");
            }
        }

        protected void cboCidade_Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidade_Empresa.SelectedIndex > 0)
                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(cnx, int.Parse(cboCidade_Empresa.SelectedValue), ""), ref txtPbBairro , false, true, "IDBAIRRO", "NOME");
            else
            {
                txtPbBairro.Items.Clear();
                txtPbBairro.Items.Add("SELECIONE A CIDADE");
            }
        }

        protected void btnConfImagem_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.PostedFile.ContentLength < 8388608)
            {
                try
                {
                    if (FileUploadControl.HasFile)
                    {
                        try
                        {
                            //Aqui ele vai filtrar pelo tipo de arquivo
                            if (FileUploadControl.PostedFile.ContentType == "image/jpeg" ||
                                FileUploadControl.PostedFile.ContentType == "image/png" ||
                                FileUploadControl.PostedFile.ContentType == "image/gif" ||
                                FileUploadControl.PostedFile.ContentType == "image/bmp")
                            {
                                try
                                {
                                    //Obtem o  HttpFileCollection
                                    HttpFileCollection hfc = Request.Files;
                                    for (int i = 0; i < hfc.Count; i++)
                                    {
                                        HttpPostedFile hpf = hfc[i];
                                        if (hpf.ContentLength > 0)
                                        {
                                            //Pega o nome do arquivo
                                            string nome = System.IO.Path.GetFileName(hpf.FileName);
                                            //Pega a extensão do arquivo
                                            string extensao = Path.GetExtension(hpf.FileName);
                                            //Gera nome novo do Arquivo numericamente
                                            string filename = string.Format("{0:00000000000000}", GerarID());
                                            //Caminho a onde será salvo
                                            hpf.SaveAs(Server.MapPath("~/LogoTipo/") + filename + ".bmp");
                                            Session["caminhoImagem"] = filename + ".bmp";

                                            imgLogo.ImageUrl = "logotipo/" + filename + ".bmp";
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                     Notificar("O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message, "Atenção");

                                }
                                // Mensagem se tudo ocorreu bem
                                //StatusLabel.Text = "Todas imagens carregadas com sucesso!";
                               // Notificar("O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message, "Atenção");


                            }
                            else
                            {
                                // Mensagem notifica que é permitido carregar apenas 
                                // as imagens definida la em cima.
                               // StatusLabel.Text = "É permitido carregar apenas imagens!";
                                Notificar("É permitido carregar apenas imagens! " , "Atenção");
                                txtNomeEmpresaSimplificado.Focus();

                            }
                        }
                        catch (Exception ex)
                        {
                            // Mensagem notifica quando ocorre erros
                            
                            Notificar("O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message, "Atenção");
                            txtNomeEmpresaSimplificado.Focus();

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Mensagem notifica quando ocorre erros
                    Notificar("O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message, "Atenção");
                    
                }
            }
            else
            {
                // Mensagem notifica quando imagem é superior a 8 MB
                Notificar("Não é permitido carregar mais do que 8 MB ", "Atenção");
                
            }
        }

        private Int64 GerarID()
        {
            try
            {
                DateTime data = new DateTime();
                data = DateTime.Now;
                string s = data.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
                return Convert.ToInt64(s);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        protected void txtCNPJCadastro_TextChanged(object sender, EventArgs e)
        {
            string c = Sistecno.BLL.Helpers.Util.Validacoes.FormatarCnpjCPF(txtCNPJCadastro.Text);
            if (Sistecno.BLL.Helpers.Util.Validacoes.CnpjValido(c) || Sistecno.BLL.Helpers.Util.Validacoes.CpfValido(c))
            {
                txtCNPJCadastro.Text = c;
                CarregarCamposFilial(0, c);
                txtRG.Focus();
            }
            else
            {
                Notificar("CNPJ ou CPF inválido", "Aviso");
            }
        }

        private void CarregarCamposFilial(int idfilial, string cnpjcfp)
        {
            if (idfilial == 0)
                return;

            try
            {
                DataSet ds = new Sistecno.BLL.Filial().RetornarTodosCampos(idfilial, Session["CNX"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    #region Cadastro

                    txtNomeFilial.Text = ds.Tables[0].Rows[0]["NOME"].ToString();
                    txtNumeroFilial.Text = ds.Tables[0].Rows[0]["NUMERODAFILIAL"].ToString();
                    txtUnidade.Text = ds.Tables[0].Rows[0]["UNIDADE"].ToString();
                    txtCNPJCadastro.Text = ds.Tables[0].Rows[0]["CnpjCpf"].ToString();
                    txtRG.Text = ds.Tables[0].Rows[0]["InscricaoRG"].ToString();
                    txtRazaoSocialNome.Text = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();
                    txtFantasiaApelido.Text = ds.Tables[0].Rows[0]["FantasiaApelido"].ToString();
                    txtEndereco.Text = ds.Tables[0].Rows[0]["Endereco"].ToString();
                    txtNumero.Text = ds.Tables[0].Rows[0]["Numero"].ToString();
                    txtComplemento.Text = ds.Tables[0].Rows[0]["Complemento"].ToString();
                    txtCEP.Text = ds.Tables[0].Rows[0]["Cep"].ToString();
                    txtInscricaoMunicipal.Text = ds.Tables[0].Rows[0]["InscricaoMunicipal"].ToString();
                                      
                    txtRazaoSocialNome.Text = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();

                    if (ds.Tables[0].Rows[0]["IDCidade"].ToString().Length > 0)
                    {
                        if (ds.Tables[0].Rows[0]["IDESTADO"].ToString().Length > 0)
                        {
                            cboEstado.SelectedValue = ds.Tables[0].Rows[0]["IDESTADO"].ToString();
                            if (cboEstado.SelectedValue.Length > 0)
                            {
                                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, true, true, "IDCIDADE", "NOME");
                                cboCidade.SelectedValue = ds.Tables[0].Rows[0]["IDCidade"].ToString();
                                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(new Sistecno.DAL.BD.ConexaoPrincipal(Session["CNX"].ToString()).CxPrincipal, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");
                            }
                        }
                    }

                    #endregion

                    #region Contato

                    verificarDataTableContato();
                    Session["dtContato"] = null;
                    dtContato.Rows.Clear();

                    lblSequencia.Text = "";
                    // txtAniversario.Text = "";
                    txtEnderecoMeioDeContato.Text = "";
                    cboTipoDeEndereco.SelectedIndex = 0;


                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows[i]["IDCADASTROCONTATOENDERECO"].ToString().Length > 0)
                        {
                            DataRow dr = dtContato.NewRow();
                            dr["IDCADASTROCONTATOENDERECO"] = ds.Tables[1].Rows[i]["IDCADASTROCONTATOENDERECO"].ToString();
                            dr["IDCADASTRO"] = ds.Tables[1].Rows[i]["IDCADASTRO"].ToString();
                            dr["IDCADASTROTIPODECONTATO"] = ds.Tables[1].Rows[i]["IDCADASTROTIPODECONTATO"].ToString();
                            dr["TIPODECONTATO"] = ds.Tables[1].Rows[i]["TIPODECONTATO"].ToString();
                            dr["ENDCONTADO"] = ds.Tables[1].Rows[i]["ENDCONTADO"].ToString();
                            dr["STATUS"] = "SIM";

                            string seq = dtContato.Compute("MAX(SEQUENCIA)", "").ToString();

                            dr["SEQUENCIA"] = seq == "" ? "1" : (int.Parse(seq) + 1).ToString();

                            dtContato.Rows.Add(dr);
                        }
                    }

                    //grdMeiosDeContato.DataSource = dtContato;
                    //grdMeiosDeContato.DataBind();

                    CriarGridMeioDeContato(dtContato);
                    Session["dtContato"] = dtContato;

                    //btnSalvar.Enabled = true;
                    #endregion

                    #region Informações do Certificado


                    Sistecno.DAL.Plano p = (Sistecno.DAL.Plano)Session["PLANOCLIENTE"];
                    string v = p.NomeCliente.Replace(".", "").Replace("-", "").Replace("-", "").Replace(" ", "").Substring(0, 10) + Sistecno.BLL.Helpers.Util.Validacoes.ZerosEsquerda(p.IdCliente.ToString(), 4);
                    txtStatusCertificado.Text = "PENDENTE DE INSTALAÇÃO";

                    X509Store stores = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    try
                    {
                        // Abre o Store
                        stores.Open(OpenFlags.ReadOnly);

                        // Obtém a coleção dos certificados da Store
                        X509Certificate2Collection certificados = stores.Certificates;

                        // percorre a coleção de certificados
                        foreach (X509Certificate2 certificado in certificados)
                        {
                            if (certificado.Subject.Contains(txtCNPJCadastro.Text.Replace("-", "").Replace(".", "").Replace("/", "")))
                            {
                                txtValidadeCertificado.Text = certificado.NotAfter.ToString();
                                txtNomeCertificado.Text = certificado.FriendlyName;
                                txtStatusCertificado.Text = "INSTALADO";
                                break;
                            }
                        }
                    }
                    finally
                    {
                        stores.Close();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Notificar(ex.Message, "Aviso");
            }
        }
    }

}