using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Util;

namespace Sistecno.UI.Web
{
    public partial class WEB0002 : System.Web.UI.Page
    {
        #region Variaveis de Escopo Global na Pagina
        
        string cnx = "";
        DataTable dtContato;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dtrPesquisa.UserControlPesquisarClicked += new EventHandler(Pesquisar_Click);
            lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")" ;
            cnx = Session["CNX"].ToString();

            dtrMensagensValidacao.listMensagens = null;

            if (!IsPostBack)
            {
                Session["dtContato"] = null;
                DataTable dt = new Sistecno.BLL.Cadastro().Retornar(null, cnx);
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

            if (Request.QueryString["acao"] == "editar")
            {
                verificarDataTableContato();
                CriarGridMeioDeContato(dtContato);
            }
            #endregion
        }

             

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            //localiza os controles de pesquisa
            List<Sistecno.DAL.ParametrosPesquisa> parPesq = new List<Sistecno.DAL.ParametrosPesquisa>();
            List<Sistecno.BLL.Helpers.CamposSearch> cp = dtrPesquisa.camposPesquisa;
            for (int i = 0; i < cp.Count; i++)
            {
                if (cp[i].Valor.Length > 0)
                    parPesq.Add(new Sistecno.DAL.ParametrosPesquisa(cp[i].NomeCampo.Replace("_", ""), cp[i].Valor, "string"));
            }


            DataTable dt = new Sistecno.BLL.Cadastro().Retornar(int.Parse(cp[0].Valor)  , cnx);
            CriarGrid(dt);
        }
        
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("web0002.aspx?opc="+ Request.QueryString["opc"].Replace(" ", "|"), false);
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

                //Prepara o objeto cadastro
                Sistecno.DAL.Models.Cadastro oC = new Sistecno.DAL.Models.Cadastro();

                oC.IDCadastro = int.Parse(Request.QueryString["id"]);
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
                new Sistecno.BLL.Cadastro().Gravar(oC, dtContato, null, Session["CNX"].ToString());
                Response.Redirect("web0002.aspx?opc="+ Request.QueryString["opc"].Replace(" ", "|"), false);

            }
            catch (Exception ex)
            {
                Notificar("Messangem: " + ex.Message, "ATENÇÃO");
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
        
        protected void txtCNPJCadastro_TextChanged(object sender, EventArgs e)
        {
            string c = Sistecno.BLL.Helpers.Util.Validacoes.FormatarCnpjCPF(txtCNPJCadastro.Text);
            if (Sistecno.BLL.Helpers.Util.Validacoes.CnpjValido(c) || Sistecno.BLL.Helpers.Util.Validacoes.CpfValido(c))
            {
                txtCNPJCadastro.Text = c;

                DataTable dt = new Sistecno.BLL.Cadastro().RetornarCuringa(c, "", cnx);

                if(dt.Rows.Count>0)
                    Response.Redirect("WEB0002.ASPX?acao=editar&id=" + dt.Rows[0]["CODIGO"].ToString() + "&opc=" + Request.QueryString["opc"].Replace(" ", "|"), false);
                else
                    txtCNPJCadastro.Focus();
            }
            else
            {
                Notificar("CNPJ ou CPF inválido", "Aviso");
            }
        }

        private void PrepararTelaEdicao()
        {
            dvManut.Visible = true;
            dvPesq.Visible = false;
            dvbot.Visible = false;
            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado, true, true, "IDESTADO", "UF");
            cboCidade.Items.Insert(0, "SELECIONE A CIDADE");

            cboBairro.Items.Add("SELECIONE O BAIRRO");
            Combo.CarregarCombo(new Sistecno.BLL.Cadastro.TipoDeContato().Retornar(cnx), ref cboTipoDeEndereco, false, true, "IDCADASTROTIPODECONTATO", "NOME");

            if (Request.QueryString["acao"] == "editar")
                CarregarCamposDoBanco();


            txtCNPJCadastro.Focus();
            
        }
                
        #endregion


        #region Metodos

        private void Notificar(string TextoDaMenssagem, string Titulo)
        {
            List<string> msg = new List<string>();
            msg.Add(TextoDaMenssagem);
            dtrMensagensValidacao.listMensagens = msg;
            dtrMensagensValidacao.TituloMensagem = Titulo;
            dtrMensagensValidacao.MostrarMensagem();
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

        /// <summary>
        /// Metodo auxiliar para manter o DataTable de Contato
        /// </summary>
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

        public void CamposPesquisa()
        {
            List<Sistecno.BLL.Helpers.CamposSearch> f = new List<Sistecno.BLL.Helpers.CamposSearch>();
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID", "IDCADASTRO", "70", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "120", "txt", "CNPJ", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("CPF", "CNPJCPF_", "100", "txt", "CPF", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("NOME", "RAZAOSOCIALNOME", "150", "", "", "", null));
            dtrPesquisa.camposPesquisa = f;
        }

        private void CriarGrid(DataTable dt)
        {                       
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB0002.ASPX", Request.QueryString["opc"])));
        }
              
        private void CarregarCamposDoBanco()
        {
            try
            {
                int codigo = int.Parse(Request.QueryString["id"]);
                DataSet ds = new Sistecno.BLL.Cadastro().RetornarTodosCampos(codigo, cnx);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado, true, true, "IDESTADO", "UF");
                    cboCidade.Items.Insert(0, "SELECIONE A CIDADE");
                    cboBairro.Items.Add("SELECIONE O BAIRRO");
                    Combo.CarregarCombo(new Sistecno.BLL.Cadastro.TipoDeContato().Retornar(cnx), ref cboTipoDeEndereco, false, true, "IDCADASTROTIPODECONTATO", "NOME");

                    #region Cadastro

                    txtCNPJCadastro.Text = ds.Tables[0].Rows[0]["CnpjCpf"].ToString();
                    txtRG.Text = ds.Tables[0].Rows[0]["InscricaoRG"].ToString();
                    txtRazaoSocialNome.Text = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();
                    txtFantasiaApelido.Text = ds.Tables[0].Rows[0]["FantasiaApelido"].ToString();
                    txtEndereco.Text = ds.Tables[0].Rows[0]["Endereco"].ToString();
                    txtNumero.Text = ds.Tables[0].Rows[0]["Numero"].ToString();
                    txtComplemento.Text = ds.Tables[0].Rows[0]["Complemento"].ToString();
                    txtCEP.Text = ds.Tables[0].Rows[0]["Cep"].ToString();
                    txtInscricaoMunicipal.Text = ds.Tables[0].Rows[0]["InscricaoMunicipal"].ToString();

                    if (ds.Tables[0].Rows[0]["DataDeCadastro"].ToString() != "")
                        txtDataCadastro.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DataDeCadastro"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                    txtRazaoSocialNome.Text = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();

                    if (ds.Tables[0].Rows[0]["IDCidade"].ToString().Length > 0)
                    {
                        if (ds.Tables[0].Rows[0]["IDESTADO"].ToString().Length > 0)
                        {
                            cboEstado.SelectedValue = ds.Tables[0].Rows[0]["IDESTADO"].ToString();
                            if (cboEstado.SelectedValue.Length > 0)
                            {
                                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(cnx, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, true, true, "IDCIDADE", "NOME");
                                cboCidade.SelectedValue = ds.Tables[0].Rows[0]["IDCidade"].ToString();
                                Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(cnx, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");
                                
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

                    CriarGridMeioDeContato(dtContato);
                    Session["dtContato"] = dtContato;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Notificar(ex.Message, "Aviso");
            }
        }

        #endregion
    }
}