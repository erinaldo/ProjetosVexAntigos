using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistecno.DAL;
using Util;
namespace Sistecno.UI.Web
{
    public partial class Teste : System.Web.UI.Page
    {
        string cnx = "";
        DataTable dtContato;

        protected void Page_Load(object sender, EventArgs e)
        {
           // UserControlPesquisarClicked += new EventHandler(Pesquisar_Click);

            lblTitulo.Text = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".aspx", "") + " - ENVOLVIDOS";
            cnx = Session["CNX"].ToString();

            if (!IsPostBack)
            {
                Session["dtContato"] = null;
                DataTable dt = new Sistecno.BLL.Cadastro().Retornar(null, cnx);
                CriarGrid(dt);


                //Prepara a parte do cadastro

                if (Request.QueryString["acao"] == "novo")
                {
                    Combo.CarregarCombo(new Sistecno.BLL.PaisUfCidade().RetornarUf(cnx), ref cboEstado, true, true, "IDESTADO", "UF");
                    cboCidade.Items.Insert(0, "SELECIONE A CIDADE");

                    cboBairro.Items.Add("SELECIONE O BAIRRO");
                    Combo.CarregarCombo(new Sistecno.BLL.Cadastro.TipoDeContato().Retornar(cnx), ref cboTipoDeEndereco, false, true, "IDCADASTROTIPODECONTATO", "NOME");
                }

                if (Request.QueryString["acao"] == "editar")
                {
                    CarregarCamposDoBanco();
                    dvManut.Visible = true;
                    dvPesq.Visible = false;
                    dvbot.Visible = false;
                }
            }
            CamposPesquisa();
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
                    Combo.CarregarCombo(new Sistecno.DAL.Cadastro.TipoDeContato().Retornar(cnx), ref cboTipoDeEndereco, false, true, "IDCADASTROTIPODECONTATO", "NOME");

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

                    grdMeiosDeContato.DataSource = dtContato;
                    grdMeiosDeContato.DataBind();
                    Session["dtContato"] = dtContato;

                    //btnSalvar.Enabled = true;
                    #endregion



                }
            }
            catch (Exception )
            {
                //Notificar(ex.Message, "Aviso");
            }
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


            DataTable dt = new Sistecno.BLL.Cadastro().Retornar(parPesq, cnx);
            CriarGrid(dt);
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
            //cria Lista de Coluanas            
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dt, "WEB0002.ASPX", Request.QueryString["opc"])));
        }

        private void CarregareDadosCadastro()
        {

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
    }
}