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
    public partial class frmCadastrarProprietario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CarregarCboEstado();
                cboCidade.Items.Clear();
                cboCidade.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));

                cboCidadeVeiculo.Items.Clear();
                cboCidadeVeiculo.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboModelo.Items.Insert(0, (new ListItem("SELECIONE UMA MARCA", "0")));
                CarregarCboEstadoVeiculo();
                CarregarComboMarca();
                CarregarComboTipo();
                CarregarComboRastreador();
                Carregar();
            }
        }

        private void Carregar()
        {
            brnCopiarDados.Visible = false;

            string strsql = "";
            strsql += " SELECT ";
            strsql += " P.IDPROPRIETARIO, V.IDVEICULO, ";
            strsql += " CADPROP.RAZAOSOCIALNOME, ";
            strsql += " CADPROP.FANTASIAAPELIDO, ";
            strsql += " CADPROP.CNPJCPF, ";
            strsql += " CADPROP.CEP, ";
            strsql += " CADPROP.ENDERECO, COMPLEMENTO, ";
            strsql += " CADPROP.NUMERO, ";
            strsql += " c.idcidade, ";
            strsql += " e.idestado, CADPROP.INSCRICAORG ";

            strsql += " FROM MOTORISTA M ";
            strsql += " LEFT JOIN VEICULO V  ON V.IDMOTORISTA = M.IDMOTORISTA ";
            strsql += " LEFT JOIN PROPRIETARIO P ON P.IDPROPRIETARIO = V.IDPROPRIETARIO ";
            strsql += " LEFT JOIN CADASTRO CADPROP ON CADPROP.IDCADASTRO = V.IDPROPRIETARIO ";
            strsql += " left join cidade c on c.idcidade = CADPROP.idcidade ";
            strsql += " left join estado e on e.idestado = c.idestado ";
            strsql += " WHERE M.IDMOTORISTA =" + Request.QueryString["i"];

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["IDPROPRIETARIO"].ToString()))
                {
                    brnCopiarDados.Visible = true;
                }
                else
                {
                    completarDadosProprietario(dt, int.Parse(dt.Rows[0]["IDPROPRIETARIO"].ToString()));
                    completarDadosVeiculo(dt);
                    txtRazao.Focus();

                }
            }
            //}
        }

        private void completarDadosVeiculo(DataTable dtv)
        {
            lblidVeiculo.Text = dtv.Rows[0]["idveiculo"].ToString();
            int? id = int.Parse(lblidVeiculo.Text);
            DataTable dt = new SistranBLL.Veiculo().Pesquisar(id, "", false, false);
            preencherCamposVeiculos(dt);
        }

        protected void brnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void txtcpfcnpj_TextChanged(object sender, EventArgs e)
        {
            txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);
            if (FormatarCnpj(txtcpfcnpj.Text) == "")
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('CPF / CNPJ inválido.')", true);
                return;
            }

            DataTable dt = new SistranBLL.Cadastro.Proprietario().Pesquisar("", FormatarCnpj(txtcpfcnpj.Text), null);
            txtcpfcnpj.Text = FormatarCnpj(txtcpfcnpj.Text);

            if (dt.Rows.Count > 0)
            {
                preencherCamposExistente(dt);
            }
            txtRazao.Focus();
        }

        private void preencherCamposExistente(DataTable dt)
        {
            lblIdProprietario.Text = dt.Rows[0]["IDPROPRIETARIO"].ToString();
            txtRazao.Text = dt.Rows[0]["RAZAOSOCIALNOME"].ToString();
            txtcpfcnpj.Text = dt.Rows[0]["CNPJCPF"].ToString();
            txtFantasiaApelido.Text = dt.Rows[0]["FANTASIAAPELIDO"].ToString();
            txtIE.Text = dt.Rows[0]["INSCRICAORG"].ToString();
            txtCEP.Text = dt.Rows[0]["CEP"].ToString();
            txtEndereco.Text = dt.Rows[0]["ENDERECO"].ToString();
            txtNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            txtComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();

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

            DataTable dtRef = new Cadastro.CadastroReferencia().Listar(int.Parse(lblIdProprietario.Text));

            DataRow[] orcl = dtRef.Select("TIPODEREFERENCIA='COMERCIAIS'");

            for (int i = 0; i < orcl.Length; i++)
            {

                switch (i)
                {
                    case 0:
                        lblIdContRefCom1.Text = orcl[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaCom1.Text = orcl[i]["Contato"].ToString();
                        txtReferenciaComFone1.Text = orcl[i]["Referencia"].ToString();
                        break;
                    case 1:
                        lblIdContRefCom2.Text = orcl[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaCom2.Text = orcl[i]["Contato"].ToString();
                        txtReferenciaComFone2.Text = orcl[i]["Referencia"].ToString();
                    break;
                }
            }

            DataRow[] orPess = dtRef.Select("TIPODEREFERENCIA='PESSOAIS'");

            for (int i = 0; i < orPess.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        lblIdContRefPes1.Text = orPess[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaPes1.Text = orPess[i]["Contato"].ToString();
                        txtReferenciaPesFone1.Text = orPess[i]["Referencia"].ToString();
                        break;

                    case 1:
                        lblIdContRefPes2.Text = orPess[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaPes2.Text = orPess[i]["Contato"].ToString();
                        txtReferenciaPesFone2.Text = orPess[i]["Referencia"].ToString();
                        break;

                    case 2:
                        lblIdContRefPes3.Text = orPess[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaPes3.Text = orPess[i]["Contato"].ToString();
                        txtReferenciaPesFone3.Text = orPess[i]["Referencia"].ToString();
                        break;
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

        private void CarregarCboEstado()
        {
            cboEstado.DataSource = new SistranBLL.Localizacao.Estado().Listar();
            cboEstado.DataTextField = "NOME";
            cboEstado.DataValueField = "IDESTADO";
            cboEstado.DataBind();
            cboEstado.Items.Insert(0, new ListItem("SELECIONE", "0"));
            cboCidade.Enabled = true;
        }

        private void CarregarCboEstadoVeiculo()
        {
            cboEstadoVeiculo.DataSource = new SistranBLL.Localizacao.Estado().Listar();
            cboEstadoVeiculo.DataTextField = "NOME";
            cboEstadoVeiculo.DataValueField = "IDESTADO";
            cboEstadoVeiculo.DataBind();
            cboEstadoVeiculo.Items.Insert(0, new ListItem("SELECIONE", "0"));
            cboEstadoVeiculo.Enabled = true;
        }
        
        private void CarregarCoboCidadeVeiculo(string idEstado)
        {
            cboCidadeVeiculo.Items.Clear();
            cboCidadeVeiculo.DataSource = new Localizacao.Cidade().ReadbyIdEstado(Convert.ToInt32(idEstado));
            cboCidadeVeiculo.DataTextField = "NOME";
            cboCidadeVeiculo.DataValueField = "IDCIDADE";
            cboCidadeVeiculo.DataBind();
            cboCidadeVeiculo.Items.Insert(0, (new ListItem("SELECIONE", "0")));
            cboCidadeVeiculo.Enabled = true;
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
                cboModelo.Focus();
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

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        protected void brnCopiarDados_Click(object sender, EventArgs e)
        {
            string strsql = "";
            strsql += "  SELECT ";
            strsql += " P.IDPROPRIETARIO, ";
            strsql += " CADPROP.RAZAOSOCIALNOME, ";
            strsql += " CADPROP.FANTASIAAPELIDO, ";
            strsql += " CADPROP.CNPJCPF, ";
            strsql += " CADPROP.CEP, ";
            strsql += " CADPROP.ENDERECO, ";
            strsql += " CADPROP.NUMERO, CADPROP.COMPLEMENTO,";
            strsql += " cadprop.InscricaoRG, ";
            strsql += " c.idcidade, ";
            strsql += " e.idestado ";

            strsql += " FROM MOTORISTA M ";
            strsql += " LEFT JOIN VEICULO V  ON V.IDMOTORISTA = M.IDMOTORISTA ";
            strsql += " LEFT JOIN PROPRIETARIO P ON P.IDPROPRIETARIO = V.IDPROPRIETARIO ";
            strsql += " LEFT JOIN CADASTRO CADPROP ON CADPROP.IDCADASTRO = M.idmotorista ";
            strsql += " left join cidade c on c.idcidade = CADPROP.idcidade ";
            strsql += " left join estado e on e.idestado = c.idestado ";
            strsql += " WHERE M.IDMOTORISTA= " + Request.QueryString["i"];

            DataTable dadosMotorista = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            completarDadosProprietario(dadosMotorista, int.Parse(Request.QueryString["i"]));
            lblIdMotorista.Text = Request.QueryString["i"];
        }

        private void completarDadosProprietario(DataTable dadosMotorista, int IdCadastro)
        {

            lblIdProprietario.Text = dadosMotorista.Rows[0]["IDPROPRIETARIO"].ToString();
            txtcpfcnpj.Text = FormatarCnpj(dadosMotorista.Rows[0]["CNPJCPF"].ToString());
            txtRazao.Text = dadosMotorista.Rows[0]["RAZAOSOCIALNOME"].ToString();
            txtIE.Text = dadosMotorista.Rows[0]["INSCRICAORG"].ToString();
            txtFantasiaApelido.Text = dadosMotorista.Rows[0]["FANTASIAAPELIDO"].ToString();

            txtCEP.Text = dadosMotorista.Rows[0]["CEP"].ToString();
            txtEndereco.Text = dadosMotorista.Rows[0]["ENDERECO"].ToString();
            txtNumero.Text = dadosMotorista.Rows[0]["NUMERO"].ToString();
            txtComplemento.Text = dadosMotorista.Rows[0]["complemento"].ToString();

            cboEstado.SelectedValue = dadosMotorista.Rows[0]["idestado"].ToString();

            lblCodEstado.Text = cboEstado.SelectedValue;
            lblCodCidade.Text = "";
            CarregarCoboCidade(cboEstado.SelectedValue);
            cboCidade.SelectedValue = dadosMotorista.Rows[0]["idcidade"].ToString();


            DataTable dtFone = new SistranBLL.Cadastro.CadastroContato.CadastroContatoEndereco().RetornarTelefone(IdCadastro);

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

            DataTable dtRef = new Cadastro.CadastroReferencia().Listar(IdCadastro);

            DataRow[] orcl = dtRef.Select("TIPODEREFERENCIA='COMERCIAIS'");

            for (int i = 0; i < orcl.Length; i++)
            {

                switch (i)
                {
                    case 0:
                        lblIdContRefCom1.Text = orcl[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaCom1.Text = orcl[i]["Contato"].ToString();
                        txtReferenciaComFone1.Text = orcl[i]["Referencia"].ToString();
                        break;
                    case 1:
                        lblIdContRefCom2.Text = orcl[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaCom2.Text = orcl[i]["Contato"].ToString();
                        txtReferenciaComFone2.Text = orcl[i]["Referencia"].ToString();
                        break;
                }
            }

            DataRow[] orPess = dtRef.Select("TIPODEREFERENCIA='PESSOAIS'");

            for (int i = 0; i < orPess.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        lblIdContRefPes1.Text = orPess[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaPes1.Text = orPess[i]["Contato"].ToString();
                        txtReferenciaPesFone1.Text = orPess[i]["Referencia"].ToString();
                        break;

                    case 1:
                        lblIdContRefPes2.Text = orPess[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaPes2.Text = orPess[i]["Contato"].ToString();
                        txtReferenciaPesFone2.Text = orPess[i]["Referencia"].ToString();
                        break;

                    case 2:
                        lblIdContRefPes3.Text = orPess[i]["IDCadastroReferencia"].ToString();
                        txtReferenciaPes3.Text = orPess[i]["Contato"].ToString();
                        txtReferenciaPesFone3.Text = orPess[i]["Referencia"].ToString();
                        break;
                }
            }
        }

        protected void brnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCadastrarMotorista.aspx?i=" + Request.QueryString["i"]);
        }

        protected void brnAvancar_Click(object sender, EventArgs e)
        {
            salvar();
            Response.Redirect("frmCadAnexos.aspx?idmotorista="+lblIdMotorista.Text+"&idProprietario="+lblIdProprietario.Text);
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


                SistranMODEL.Cadastro oCad = new SistranMODEL.Cadastro();
                SistranMODEL.Cadastro.Proprietario oProp = new SistranMODEL.Cadastro.Proprietario();


                oCad.IDCadastro = Convert.ToInt32((lblIdProprietario.Text == "" ? "0" : lblIdProprietario.Text));
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

                oProp.TelefoneCel = txtFoneCelular.Text;
                oProp.TelefoneRecado = txtFoneRecado.Text;
                oProp.TelefoneRes = txtFoneResidencial.Text;
                oProp.TelefoneNextel = txtFoneNextel.Text;

                oProp.IDTelefoneCel = idtelefoneCel.Value;
                oProp.IDTelefoneRecado = idtelefoneRec.Value;
                oProp.IDTelefoneRes = idtelefoneRes.Value;
                oProp.IDTelefoneNextel = idtelefoneNextel.Value;

                oProp.IDProprietario = Convert.ToInt32((lblIdProprietario.Text == "" ? "0" : lblIdProprietario.Text));

                if (lblIdProprietario.Text == "" || lblIdProprietario.Text == "0")
                {
                    lblIdProprietario.Text = new SistranBLL.Cadastro().TransacaoInserirCadastroProprietario("", oCad, oProp).ToString();
                }
                else
                {
                    new SistranBLL.Cadastro().Alterar(oCad.CnpjCpf, oCad.InscricaoRG, txtRazao.Text.ToUpper(), txtFantasiaApelido.Text.ToUpper(), txtEndereco.Text.ToUpper(), txtNumero.Text, oCad.Complemento, oCad.IDCidade.ToString(), "", oCad.Cep, lblIdProprietario.Text);
                    new SistranBLL.Cadastro.Proprietario().GravarTelefonesProprietario(oProp);
                }



                #region Referencias
                List<SistranMODEL.Cadastro.CadastroReferencia> locadRef = new List<SistranMODEL.Cadastro.CadastroReferencia>();

                SistranMODEL.Cadastro.CadastroReferencia ocadRef = new SistranMODEL.Cadastro.CadastroReferencia();
                ocadRef.IDCadastroReferencia = int.Parse( lblIdContRefCom1.Text);
                ocadRef.Contato = txtReferenciaCom1.Text.ToUpper().Replace("'", "");
                ocadRef.Referencia = txtReferenciaComFone1.Text.ToUpper().Replace("'", "");
                ocadRef.TipoDeReferencia = "COMERCIAIS";                
                locadRef.Add(ocadRef);

                ocadRef = new SistranMODEL.Cadastro.CadastroReferencia();
                ocadRef.IDCadastroReferencia = int.Parse(lblIdContRefCom2.Text);
                ocadRef.Contato = txtReferenciaCom2.Text.ToUpper().Replace("'", "");
                ocadRef.Referencia = txtReferenciaComFone2.Text.ToUpper().Replace("'", "");
                ocadRef.TipoDeReferencia = "COMERCIAIS";
                locadRef.Add(ocadRef);



                ocadRef = new SistranMODEL.Cadastro.CadastroReferencia();
                ocadRef.IDCadastroReferencia = int.Parse(lblIdContRefPes1.Text);
                ocadRef.Contato = txtReferenciaPes1.Text.ToUpper().Replace("'", "");
                ocadRef.Referencia = txtReferenciaPesFone1.Text.ToUpper().Replace("'", "");
                ocadRef.TipoDeReferencia = "PESSOAIS";
                locadRef.Add(ocadRef);


                ocadRef = new SistranMODEL.Cadastro.CadastroReferencia();
                ocadRef.IDCadastroReferencia = int.Parse(lblIdContRefPes2.Text);
                ocadRef.Contato = txtReferenciaPes2.Text.ToUpper().Replace("'", "");
                ocadRef.Referencia = txtReferenciaPesFone2.Text.ToUpper().Replace("'", "");
                ocadRef.TipoDeReferencia = "PESSOAIS";
                locadRef.Add(ocadRef);

                ocadRef = new SistranMODEL.Cadastro.CadastroReferencia();
                ocadRef.IDCadastroReferencia = int.Parse(lblIdContRefPes3.Text);
                ocadRef.Contato = txtReferenciaPes3.Text.ToUpper().Replace("'", "");
                ocadRef.Referencia = txtReferenciaPesFone3.Text.ToUpper().Replace("'", "");
                ocadRef.TipoDeReferencia = "PESSOAIS";
                locadRef.Add(ocadRef);




                #endregion


                //veiculo                                 


                if (lblidVeiculo.Text == "" || lblidVeiculo.Text == "0")
                {
                    lblidVeiculo.Text = new SistranBLL.Veiculo().Inserir(
                        Convert.ToInt32(cboModelo.SelectedValue), 
                        Convert.ToInt32(cboTipo.SelectedValue), 
                        Convert.ToInt32(cboRastreador.SelectedValue), 
                        Convert.ToInt32(cboCidade.SelectedValue),
                        Convert.ToInt32(lblIdProprietario.Text), 
                        Convert.ToInt32(lblIdMotorista.Text), 
                        txtPlaca.Text.ToUpper(), 
                        txtRenavan.Text.ToUpper(),
                        txtChassi.Text.ToUpper(), Convert.ToInt32(txtano.Text), 
                        txtCor.Text.ToUpper(), 
                        txtCapacidadeKG.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidadeKG.Text), 
                        txtCapacidade.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidade.Text),
                        txteixos.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txteixos.Text), 
                        "", 
                        txtAntt.Text.ToUpper(), 
                        txtNumeroSerieEquipamento.Text.ToUpper(), 
                        Convert.ToDateTime(txtVencimentoAntt.Text), 
                        Convert.ToDateTime(txtDataLicenciamento.Text), 
                        cboMarca.SelectedItem.Text,
                        txtanoModelo.Text, locadRef).ToString();
                }
                else
                {
                    new SistranBLL.Veiculo().Update(
                        Convert.ToInt32(lblidVeiculo.Text), 
                        Convert.ToInt32(cboModelo.SelectedValue), 
                        Convert.ToInt32(cboTipo.SelectedValue), 
                        Convert.ToInt32(cboRastreador.SelectedValue), 
                        Convert.ToInt32(cboCidade.SelectedValue),
                        Convert.ToInt32(lblProprietario.Text), 
                        Convert.ToInt32(lblIdMotorista.Text), 
                        txtPlaca.Text.ToUpper(), 
                        txtRenavan.Text.ToUpper(),
                        txtChassi.Text.ToUpper(), 
                        Convert.ToInt32(txtano.Text), 
                        txtCor.Text.ToUpper(), 
                        txtCapacidadeKG.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidadeKG.Text), 
                        txtCapacidade.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtCapacidade.Text),
                        txteixos.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txteixos.Text), "", 
                        txtAntt.Text.ToUpper(), txtNumeroSerieEquipamento.Text.ToUpper(), 
                        Convert.ToDateTime(txtVencimentoAntt.Text), 
                        Convert.ToDateTime(txtDataLicenciamento.Text), 
                        cboMarca.SelectedItem.Text,
                        txtanoModelo.Text, locadRef                                                               
                        );
                }

                ////////
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Aler", "alert('Operação Efetuada com Sucesso.'); window.location.href='CHECAR.aspx' ", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
                return;
            }
        }

        protected void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPlaca.Text.Length > 0 && txtPlaca.Text != "___-____")
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
                        preencherCamposVeiculos(dtVei);
                        txtChassi.Focus();
                    }
                    txtDataLicenciamento.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
                return;
            }
        }

        private void preencherCamposVeiculos(DataTable dt)
        {
            lblidVeiculo.Text = dt.Rows[0]["IDVeiculo"].ToString();
            txtPlaca.Text = dt.Rows[0]["Placa"].ToString();
            txtAntt.Text = dt.Rows[0]["Antt"].ToString();
            txtChassi.Text = dt.Rows[0]["Chassi"].ToString();
            txtRenavan.Text = dt.Rows[0]["Renavam"].ToString();
          
            lblIdMotorista.Text = dt.Rows[0]["IDMOTORISTA"].ToString();            
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
            
            cboRastreador.SelectedValue = dt.Rows[0]["IDVEICULORASTREADOR"].ToString();
            txtNumeroSerieEquipamento.Text = dt.Rows[0]["NUMEROSERIEEQUIPAMENTO"].ToString();
            txtVencimentoAntt.Text = dt.Rows[0]["ANTTVENCIMENTO"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["ANTTVENCIMENTO"]).ToShortDateString();
            txtDataLicenciamento.Text = dt.Rows[0]["DATADELICENCIAMENTO"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["DATADELICENCIAMENTO"]).ToShortDateString();

             CarregarCboEstado();

            if (dt.Rows[0]["IDCIDADE"].ToString().Trim().Length > 0)
            {
                DataTable dtCidade = new Localizacao.Cidade().Read(Convert.ToInt32(dt.Rows[0]["IDCIDADE"].ToString()));

                if (dtCidade.Rows.Count > 0)
                {
                    CarregarCoboCidadeVeiculo(dtCidade.Rows[0]["IDESTADO"].ToString());
                    cboEstadoVeiculo.SelectedValue = dtCidade.Rows[0]["IDESTADO"].ToString();
                    cboCidadeVeiculo.SelectedValue = dtCidade.Rows[0]["IDCIDADE"].ToString();
                    cboCidadeVeiculo.Enabled = true;
                    lblCodEstado.Text = dtCidade.Rows[0]["IDESTADO"].ToString();
                    lblCodCidade.Text = dtCidade.Rows[0]["IDCIDADE"].ToString();
                }
            }
        }

        protected void cboEstadoVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstadoVeiculo.SelectedValue == lblCodEstado.Text && cboCidadeVeiculo.SelectedValue == lblCodCidade.Text)
            {
                return;
            }

            if (cboEstadoVeiculo.SelectedValue == "0")
            {
                cboCidadeVeiculo.Items.Clear();
                cboCidadeVeiculo.Items.Insert(0, (new ListItem("SELECIONE O ESTADO", "0")));
                cboCidadeVeiculo.Enabled = false;
                lblCodEstado.Text = "";
                return;
            }
            else
            {
                lblCodEstado.Text = cboEstadoVeiculo.SelectedValue;
                lblCodCidade.Text = "";
                CarregarCoboCidadeVeiculo(cboEstadoVeiculo.SelectedValue);
                cboCidadeVeiculo.Focus();
            }
        }

        protected void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarComboModelo();
        }
    }
}