using Sistecno.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Util;


namespace Sistecno.UI.Web
{
    public partial class WEB1000 : System.Web.UI.Page
    {
        #region Variaveis de Escopo Global na Pagina
        
       // string cnx = "";     

        #endregion

        #region Eventos
        string cnxstn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            dtrPesquisa.UserControlPesquisarClicked += new EventHandler(Pesquisar_Click);
            lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")";

            cnxstn = new DAL.BD.ConexaoPrincipal("").CxPrincipal;

            dtrMensagensValidacao.listMensagens = null;

            if (!IsPostBack)
            {
                Session["dtContato"] = null;
                
                DataTable dt = new BLL.ConexaoSistecno().RetornarGrid(null, cnxstn);
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

            //if (Request.QueryString["acao"] == "editar")
            ////{
            ////   // verificarDataTableContato();
            ////    CriarGridMeioDeContato(dtContato);
            ////}
            #endregion
        }



        private void Pesquisar_Click(object sender, EventArgs e)
        {
            //localiza os controles de pesquisa
            List<ParametrosPesquisa> parPesq = new List<ParametrosPesquisa>();
            List<Sistecno.BLL.Helpers.CamposSearch> cp = dtrPesquisa.camposPesquisa;
            //for (int i = 0; i < cp.Count; i++)
            //{
            //    if (cp[i].Valor.Length > 0)
            //        parPesq.Add(new ParametrosPesquisa(cp[i].NomeCampo.Replace("_", ""), cp[i].Valor, "string"));
            //}

            DAL.Models.Conexao obj = new DAL.Models.Conexao();

            //objCa.RazaoSocialNome = "RED LOG TRANSPORTES E LOGISTICA LTDA";
            //obj.Cliente.Cadastro = objCa;

            if (cp[0].Valor != "")
            {
                obj.IdConexao = int.Parse(cp[0].Valor);
            }
            if (cp[1].Valor != "")
            {
                obj.IdConexao = int.Parse(cp[1].Valor);
            }
            DataTable dt = new Sistecno.BLL.ConexaoSistecno().Pesquisar(obj, cnxstn);
            CriarGrid(dt);
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("web1000.aspx?opc="+ Request.QueryString["opc"].Replace(" ", "|"), false);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Sistecno.DAL.Models.Conexao oC = new Sistecno.DAL.Models.Conexao();

                oC.IdConexao =int.Parse( txtIdConexao.Text);
                oC.IdCliente = int.Parse(txtIdCliente.Text);
                oC.IP = txtIP.Text;
                oC.BaseDeDados = txtBancoDeDados.Text;
                oC.UsuarioBD = txtUsuario.Text;
                oC.SenhaBD = txtSenha.Text;
                oC.Porta = txtPorta.Text;

              
                new Sistecno.BLL.ConexaoSistecno().Gravar(oC,cnxstn);

                Response.Redirect("web1000.aspx?opc="+ Request.QueryString["opc"].Replace(" ", "|"), false);

            }
            catch (Exception ex)
            {
                Notificar("Messangem: " + ex.Message, "ATENÇÃO");
            }
        }


        private void PrepararTelaEdicao()
        {
            dvManut.Visible = true;
            dvPesq.Visible = false;
            dvbot.Visible = false;

            DataTable dt = new BLL.Cliente().RetornarTodosClientes(cnxstn);
            Combo.CarregarCombo(dt, ref DropDownListCliente, false, true, "IdCliente", "RazaoSocialNome");
            if (Request.QueryString["acao"] == "editar")
                CarregarCamposDoBanco();


           txtIdConexao.Focus();
            
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

     

        public void CamposPesquisa()
        {
            List<Sistecno.BLL.Helpers.CamposSearch> f = new List<Sistecno.BLL.Helpers.CamposSearch>();
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID", "IDCONEXAO", "70", "txt", "SomenteNumero", "", null));
            f.Add(new Sistecno.BLL.Helpers.CamposSearch("ID CLIENTE", "IDCLIENTE", "70", "txt", "SomenteNumero", "", null));
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("CNPJ", "CNPJCPF", "120", "txt", "CNPJ", "", null));
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("CPF", "CNPJCPF_", "100", "txt", "CPF", "", null));
            //f.Add(new Sistecno.BLL.Helpers.CamposSearch("NOME", "RAZAOSOCIALNOME", "150", "", "", "", null));
            dtrPesquisa.camposPesquisa = f;
        }

        private void CriarGrid(DataTable dt)
        {                       
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB1000.ASPX", Request.QueryString["opc"])));
        }
              
        private void CarregarCamposDoBanco()
        {
            try
            {
                int codigo = int.Parse(Request.QueryString["id"]);
                DataTable ds = new Sistecno.BLL.ConexaoSistecno().RetornarGrid(codigo, cnxstn);


                if (ds.Rows.Count > 0)
                {
                    //#region Cadastro
                    //DropDownListCliente

                    txtIdConexao.Text = ds.Rows[0]["IdConexao"].ToString();
                    txtIdCliente.Text = ds.Rows[0]["IdCliente"].ToString();
                    txtIP.Text = ds.Rows[0]["IP"].ToString();
                    txtBancoDeDados.Text = ds.Rows[0]["BaseDeDados"].ToString();
                    txtUsuario.Text = ds.Rows[0]["UsuarioBD"].ToString();
                    txtSenha.Text = ds.Rows[0]["SenhaBD"].ToString();
                    txtPorta.Text = ds.Rows[0]["Porta"].ToString();
                    DropDownListCliente.SelectedValue = ds.Rows[0]["IdCliente"].ToString();

                    //txtInscricaoMunicipal.Text = ds.Tables[0].Rows[0]["InscricaoMunicipal"].ToString();

                    //if (ds.Tables[0].Rows[0]["DataDeCadastro"].ToString() != "")
                    //    txtDataCadastro.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DataDeCadastro"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                    //txtRazaoSocialNome.Text = ds.Tables[0].Rows[0]["RazaoSocialNome"].ToString();

                    //if (ds.Tables[0].Rows[0]["IDCidade"].ToString().Length > 0)
                    //{
                    //    if (ds.Tables[0].Rows[0]["IDESTADO"].ToString().Length > 0)
                    //    {
                    //        cboEstado.SelectedValue = ds.Tables[0].Rows[0]["IDESTADO"].ToString();
                    //        if (cboEstado.SelectedValue.Length > 0)
                    //        {
                    //            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarCidade(cnx, int.Parse(cboEstado.SelectedValue), ""), ref cboCidade, true, true, "IDCIDADE", "NOME");
                    //            cboCidade.SelectedValue = ds.Tables[0].Rows[0]["IDCidade"].ToString();
                    //            Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarBairro(cnx, int.Parse(cboCidade.SelectedValue), ""), ref cboBairro, false, true, "IDBAIRRO", "NOME");

                    //            if (ds.Tables[0].Rows[0]["IDBAIRRO"].ToString().Length > 0)
                    //                cboBairro.SelectedValue = ds.Tables[0].Rows[0]["IDBAIRRO"].ToString();
                    //        }
                    //    }
                    //}

                    //#endregion

                    //#region Contato

                    //verificarDataTableContato();
                    //Session["dtContato"] = null;
                    //dtContato.Rows.Clear();
                    //txtEnderecoMeioDeContato.Text = "";
                    //cboTipoDeEndereco.SelectedIndex = 0;

                    //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    //{
                    //    if (ds.Tables[1].Rows[i]["IDCADASTROCONTATOENDERECO"].ToString().Length > 0)
                    //    {
                    //        DataRow dr = dtContato.NewRow();
                    //        dr["IDCADASTROCONTATOENDERECO"] = ds.Tables[1].Rows[i]["IDCADASTROCONTATOENDERECO"].ToString();
                    //        dr["IDCADASTRO"] = ds.Tables[1].Rows[i]["IDCADASTRO"].ToString();
                    //        dr["IDCADASTROTIPODECONTATO"] = ds.Tables[1].Rows[i]["IDCADASTROTIPODECONTATO"].ToString();
                    //        dr["TIPODECONTATO"] = ds.Tables[1].Rows[i]["TIPODECONTATO"].ToString();
                    //        dr["ENDCONTADO"] = ds.Tables[1].Rows[i]["ENDCONTADO"].ToString();
                    //        dr["STATUS"] = "SIM";

                    //        string seq = dtContato.Compute("MAX(SEQUENCIA)", "").ToString();

                    //        dr["SEQUENCIA"] = seq == "" ? "1" : (int.Parse(seq) + 1).ToString();

                    //        dtContato.Rows.Add(dr);
                    //    }
                    //}

                    //CriarGridMeioDeContato(dtContato);
                    //Session["dtContato"] = dtContato;
                    //#endregion
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