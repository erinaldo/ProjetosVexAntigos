using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace ImpressaoEtiquetaUA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void button3_Click(object sender, EventArgs e)
        {
            imrimir();
        }

        private void imrimir()
        {

            string sql = " UPDATE UNIDADEDEARMAZENAGEM SET VERSAOIMPRESSAO= ISNULL(VersaoImpressao,0) + 1 WHERE IDUNIDADEDEARMAZENAGEM=" + codigoUa;
            sql += " ; SELECT TOP 1 ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM, ";
            sql += " UA.DIGITO, ";
            sql += " UA.QUANTIDADE, ";
            sql += " UA.VALIDADE, ";
            sql += " PL.DESCRICAO AS LOCALIZACAO, ";
            sql += " UA.LOTE REFERENCIA, ";
            sql += " LT.IDLOTE, ";
            sql += " US.LOGIN, ";
            sql += " UAL.SALDO, ";
            sql += " UAL.SALDO/EMB.UNIDADEDOCLIENTE AS SALDOMAIORUNIDADE, ";
            sql += " DOC.NUMERO, ";
            sql += " LT.VALIDADE, ";
            sql += " LT.DATADEENTRADA, ";
            sql += " LT.VALORUNITARIO, ";
            sql += " LT.REFERENCIA, ";
            sql += " PRO.CODIGODEBARRAS, ";
            sql += " PROCLI.DESCRICAO, ";
            sql += " PROCLI.CODIGO, ";
            sql += " EMB.UNIDADEDOCLIENTE, ";
            sql += " EMB.UNIDADEDEMEDIDA,  isnull(UA.DataDeEmissao, getdate()) EMISSAOETIQUETA,";
            sql += " RO.IDROMANEIO, ";
            sql += " RO.EMISSAO AS EMISSAOROMANEIO, ";
            sql += " REM.RAZAOSOCIALNOME AS REMETENTE, ";
            sql += " DES.RAZAOSOCIALNOME AS DESTINATARIO, ";
            sql += " PROCLI.IDCliente, isNull(ua.VersaoImpressao, 0) VersaoImpressao ";
            sql += " FROM UNIDADEDEARMAZENAGEM UA ";
            sql += " LEFT JOIN DEPOSITOPLANTALOCALIZACAO PL ON (PL.IDDEPOSITOPLANTALOCALIZACAO=UA.IDDEPOSITOPLANTALOCALIZACAO) ";
            sql += " LEFT JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON (UAL.IDUNIDADEDEARMAZENAGEM=UA.IDUNIDADEDEARMAZENAGEM) ";
            sql += " LEFT JOIN LOTE LT ON (LT.IDLOTE=UAL.IDLOTE) ";
            sql += " LEFT JOIN PRODUTOCLIENTE PROCLI ON (PROCLI.IDPRODUTOCLIENTE=UA.IDPRODUTOCLIENTE) ";
            sql += " LEFT JOIN DOCUMENTO DOC ON (DOC.IDDOCUMENTO=LT.IDDOCUMENTO) ";
            sql += " LEFT JOIN CADASTRO REM ON (REM.IDCADASTRO=DOC.IDREMETENTE) ";
            sql += " LEFT JOIN CADASTRO DES ON (DES.IDCADASTRO=DOC.IDDESTINATARIO) ";
            sql += " LEFT JOIN PRODUTOEMBALAGEM EMB ON (EMB.IDPRODUTOCLIENTE=UA.IDPRODUTOCLIENTE) ";
            sql += " LEFT JOIN PRODUTO PRO ON (PRO.IDPRODUTO=EMB.IDPRODUTO) ";
            sql += " LEFT JOIN USUARIO US ON (US.IDUSUARIO=LT.IDUSUARIO)  ";
            sql += " LEFT JOIN (	SELECT RO.IDROMANEIO, RO.EMISSAO, ROD.IDDOCUMENTO ";
            sql += " FROM ROMANEIO RO ";
            sql += " INNER JOIN ROMANEIODOCUMENTO ROD ON (ROD.IDROMANEIO=RO.IDROMANEIO) ";
            sql += " WHERE TIPO='ENTRADA') RO ON (RO.IDDOCUMENTO=LT.IDDOCUMENTO) ";
            sql += " WHERE ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM =  " + codigoUa;
            sql += " AND UA.IDFILIAL=" + filialPadrao;

            if (cb != "")
                sql += " AND PRO.CodigoDeBarras = '" + cb + "' ";
            sql += " ORDER BY ";
            sql += " UA.IDUNIDADEDEARMAZENAGEM, LT.IDLOTE; ";

            DataTable dtImpressao = cDB.RetornarDataTable(sql, conexao);
            StringBuilder sb;
            sb = new StringBuilder();

            string uaDigito = dtImpressao.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString() + dtImpressao.Rows[0]["DIGITO"].ToString();


            sb.AppendLine("^XA");
            //sb.AppendLine("^DFB:etiqu.ZPL^FS");
            sb.AppendLine("~TA000~JSN^LT0^MNW^MTD^PON^PMN^LH0,0^JMA^PR4,4^MD15^LRN^CI0");
            sb.AppendLine("^MMT");
            sb.AppendLine("^PW799");
            sb.AppendLine("^LL0400");
            sb.AppendLine("^LS0");
            sb.AppendLine("^BY4,3,40^FT51,59^BCN,,Y,N");
            sb.AppendLine("^FD>;" + dtImpressao.Rows[0]["CODIGODEBARRAS"].ToString() + "^FS");
            sb.AppendLine(@"^FT649,50^A0N,31,31^FH\^FDCODIGO^FS");
            sb.AppendLine(@"^FT637,78^A0N,20,28^FH\^FD" + dtImpressao.Rows[0]["CODIGO"].ToString() + "^FS");
            sb.AppendLine(@"^FT56,334^A0N,28,28^FH\^FDVIA^FS");
            sb.AppendLine(@"^FT401,334^A0N,28,28^FH\^FDUA^FS");
            sb.AppendLine(@"^FT603,246^A0N,28,28^FH\^FDQUANTIDADE^FS");
            sb.AppendLine(@"^FT298,246^A0N,28,28^FH\^FDREFERENCIA^FS");
            sb.AppendLine(@"^FT299,367^A0N,25,36^FH\^FD" + dtImpressao.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString() + "^FS");
            //sb.AppendLine(@"^FT299,367^A0N,25,36^FH\^FD" + uaDigito + "^FS");
            sb.AppendLine(@"^FT53,246^A0N,28,28^FH\^FDVALIDADE^FS");
            sb.AppendLine(@"^FT658,280^A0N,25,36^FH\^FD" + int.Parse(float.Parse(dtImpressao.Rows[0]["QUANTIDADE"].ToString()).ToString()).ToString() + "^FS");
            sb.AppendLine(@"^FT299,280^A0N,25,36^FH\^FD" + dtImpressao.Rows[0]["REFERENCIA"].ToString() + "^FS");
            sb.AppendLine(@"^FT52,142^A0N,28,28^FH\^FDDESCRICAO^FS");
            sb.AppendLine(@"^FT54,280^A0N,25,36^FH\^FD" + (dtImpressao.Rows[0]["VALIDADE"].ToString() != "" ? DateTime.Parse(dtImpressao.Rows[0]["VALIDADE"].ToString()).ToString("dd/MM/yyyy") : "") + "^FS");
            sb.AppendLine(@"^FT56,367^A0N,25,36^FH\^FD" + dtImpressao.Rows[0]["VersaoImpressao"].ToString() + "^FS");
            //sb.AppendLine(@"^FT56,367^A0N,25,36^FH\^FD2^FS");
            sb.AppendLine(@"^FT629,177^A0N,25,28^FH\^FD" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + "^FS");
            sb.AppendLine(@"^FT53,177^A0N,25,36^FH\^FD" + (dtImpressao.Rows[0]["DESCRICAO"].ToString().Length > 30 ? dtImpressao.Rows[0]["DESCRICAO"].ToString().Substring(0, 29) : dtImpressao.Rows[0]["DESCRICAO"].ToString()) + "^FS");
            sb.AppendLine(@"^FT650,142^A0N,28,28^FH\^FDEMISSAO^FS");


            //sb.AppendLine("^BY3,3,52^FT531,364^BCN,,N,N");
            //sb.AppendLine("^FD>;" + uaDigito + "^FS");


            //sb.AppendLine("^BY2,3,71^FT454,381^BCN,,N,N");
            //sb.AppendLine("^FD>;" + uaDigito + "^FS");

            //sb.AppendLine("^BY2,3,64^FT454,374^BCN,,N,N");
            //sb.AppendLine("^FD>;" + uaDigito + ">^FS");



            sb.AppendLine("^^BY1,3,64^FT466,380^B3N,N,,N,N");
            //sb.AppendLine("^BY1,3,64^FT373,380^B3N,N,,N,N");
sb.AppendLine("^FD"+uaDigito+"^FS");
sb.AppendLine("^FO54,296^GB716,0,2^FS");

            
//^FD555555555555555^FS
//^FO54,296^GB716,0,2^FS
//^FO50,108^GB715,0,1^FS
//^FO50,197^GB715,0,1^FS

            sb.AppendLine("^FO50,108^GB715,0,1^FS");
            sb.AppendLine("^FO50,197^GB715,0,1^FS");
            sb.AppendLine("^XZ");
            RawPrinterHelper.SendStringToPrinter(System.Configuration.ConfigurationSettings.AppSettings["CaminhoImpressora"], sb.ToString());
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            filialPadrao = System.Configuration.ConfigurationSettings.AppSettings["FilialPadrao"];
            conexao = System.Configuration.ConfigurationSettings.AppSettings["Conexao"];
        }
        string filialPadrao = "";
        string conexao = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            //PesquisarItens();
            textBox1.Focus();
        }

        private void PesquisarItens()
        {
            filialPadrao = System.Configuration.ConfigurationSettings.AppSettings["FilialPadrao"];
            conexao = System.Configuration.ConfigurationSettings.AppSettings["Conexao"];

            dataGridView1.DataSource = cDB.RetornarUa(textBox1.Text, textBox1.Text, filialPadrao, conexao);

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {

                switch (dataGridView1.Columns[i].HeaderText)
                {
                    case "CLIENTE":
                        dataGridView1.Columns[i].Width = 250;
                        break;

                    case "DIGITO":
                        dataGridView1.Columns[i].Width = 80;
                        break;


                    case "CODIGODEBARRAS":
                        dataGridView1.Columns[i].Width = 120;
                        dataGridView1.Columns[i].HeaderText = "COD. BARRAS";
                        break;

                    case "COD. BARRAS":
                        dataGridView1.Columns[i].Width = 120;
                        dataGridView1.Columns[i].HeaderText = "COD. BARRAS";
                        break;

                    case "DESCRICAO":
                        dataGridView1.Columns[i].Width = 250;
                        break;

                    case "METODODEMOVIMENTACAO":
                        dataGridView1.Columns[i].Width = 200;
                        dataGridView1.Columns[i].HeaderText = "MOVIMENTAÇÃO";

                        break;

                    default:
                        dataGridView1.Columns[i].Width = 80;
                        break;
                }
            }

            dataGridView1.Rows[0].Selected = true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            gpPesquisaProduto.Enabled = true;
            txtPesquisaCBProduto.Focus();
            txtPesquisaCBProduto.Text = "";            
            txtCadastroCodigoDoCliente.Text = "";
            txtCadastroCliente.Text = "";
            txtCadastroCodigoDeBarras.Text = "";
            txtCadastroLoteReferencia.Text = "";
            txtCadastroValidade.Text = "";
            txtCadastroProduto.Text = "";
            textBox4.Text = "";
            txtLastro.Text = "";
            txtAltura.Text = "";
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnImprimir.Enabled = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            gpPesquisaProduto.Enabled = false;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            btnImprimir.Enabled = false;
        }

        string codigoUa = "0";
        string cb = "";
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CarregarDadosAuxiliares();
            tabControl1.SelectedIndex = 1;
        }
        
        private void CarregarDadosAuxiliares()
        {

            if (dataGridView1.CurrentCell == null)
                return;



            int row = dataGridView1.CurrentCell.RowIndex;
            codigoUa = dataGridView1[0, row].Value.ToString();
            cb = dataGridView1[3, row].Value.ToString();
            dataGridView1.Rows[row].Selected = true;
            CarregarCamposGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        DataTable ds;
        private void CarregarCamposGrid()
        {
            LimparCampos();
            try
            {
                string sql = "SELECT TOP 50 UA.IDUNIDADEDEARMAZENAGEM CODIGO, UA.IDUNIDADEDEARMAZENAGEM , UA.DIGITO,ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) CLIENTE, P.CODIGODEBARRAS, PC.DESCRICAO,PC.METODODEMOVIMENTACAO, PC.IDCLIENTE, ISNULL(C.FANTASIAAPELIDO, C.RAZAOSOCIALNOME) FANTASIAAPELIDO, SolicitarLote, SolicitarDataDeValidade, pc.idprodutocliente, UA.*, PC.LASTRO, P.ALTURA ";
                sql += " FROM UNIDADEDEARMAZENAGEM UA ";
                sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDProdutoCliente = UA.IdProdutoCliente   ";
                sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = UA.IDPRODUTOEMBALAGEM   ";
                sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO ";
                sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
                sql += " AND UA.IDUNIDADEDEARMAZENAGEM =" + codigoUa + " ";
                sql += " AND P.CODIGODEBARRAS='" + cb + "' ";
                sql += " AND UA.DIGITO IS NOT NULL ";
                sql += " ORDER BY UA.IDUNIDADEDEARMAZENAGEM DESC ";
                ds = cDB.RetornarDataTable(sql, conexao);
                
                if (ds.Rows.Count > 0)
                {
                    txtCadastroCodigoDoCliente.Text = int.Parse(float.Parse(ds.Rows[0]["IDCLIENTE"].ToString()).ToString()).ToString();
                    txtCadastroCliente.Text = ds.Rows[0]["FANTASIAAPELIDO"].ToString();
                    txtMeioDeMovimentacao.Text = ds.Rows[0]["METODODEMOVIMENTACAO"].ToString();
                    txtCadastroProduto.Text = ds.Rows[0]["DESCRICAO"].ToString();
                    txtCadastroCodigoDeBarras.Text = ds.Rows[0]["CODIGODEBARRAS"].ToString();
                    txtSolicitaLote.Text = ds.Rows[0]["SOLICITARLOTE"].ToString();
                    txtSolicitaValidade.Text = ds.Rows[0]["SOLICITARDATADEVALIDADE"].ToString();
                    lblidProdutoCliente.Text = ds.Rows[0]["IdProdutoCliente"].ToString();
                    textBox4.Text = ds.Rows[0]["QUANTIDADE"].ToString();
                    txtCadastroLoteReferencia.Text = ds.Rows[0]["LOTE"].ToString();

                    txtLastro.Text = ds.Rows[0]["LASTRO"].ToString();
                    txtAltura.Text = ds.Rows[0]["Altura"].ToString();

                    if (ds.Rows[0]["VALIDADE"].ToString() != "")
                        txtCadastroValidade.Text = DateTime.Parse(ds.Rows[0]["VALIDADE"].ToString()).ToString("dd/MM/yyyy");
                    else
                        txtCadastroValidade.Text = "";

                    textBox4.Focus();
                   btnImprimir.Enabled = true;
                    btnSalvar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnPesquisar.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtPesquisaCBProduto.Text = "";
            textBox4.Text = "";
            txtCadastroLoteReferencia.Text = "";
            txtCadastroValidade.Text = "";

            txtCadastroCodigoDoCliente.Text = "";
            txtCadastroCliente.Text = "";
            txtMeioDeMovimentacao.Text = "";
            txtCadastroProduto.Text = "";
            txtCadastroCodigoDeBarras.Text = "";
            txtSolicitaLote.Text = "";
            txtSolicitaValidade.Text = "";
            lblidProdutoCliente.Text = "";
            lblIdProrutoEmbalagem.Text = "";
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                CarregarDadosAuxiliares();

                tabControl1.SelectedIndex = 1;
                gpPesquisaProduto.Enabled = false;
                txtPesquisaCBProduto.Focus();
                txtPesquisaCBProduto.Text = "";
                btnCancelar.Enabled = true;
                btnSalvar.Enabled = true;
                btnNovo.Enabled = false;
                btnImprimir.Enabled = true;
            }
            else
            {
                LimparCampos();
                tabControl1.SelectedIndex = 0;
                gpPesquisaProduto.Enabled = false;
                btnCancelar.Enabled = false;
                btnSalvar.Enabled = false;
                btnNovo.Enabled = true;
                PesquisarItens();
                btnImprimir.Enabled = false;

            }
        }

        private void txtPesquisaCBProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtPesquisaCBProduto.Text != "")
            {
                PesquisarProduto();
            }
        }

        DataTable dtPesqProd;
        private void PesquisarProduto()
        {
            cboCliente.Items.Clear();
            string sql = "SELECT DISTINCT PC.DESCRICAO, PC.IDCLIENTE, LEFT(C.FANTASIAAPELIDO, 20) FANTASIAAPELIDO, C.FANTASIAAPELIDO,P.CODIGODEBARRAS,PC.IDPRODUTOCLIENTE, PC.CODIGO, PC.IDCLIENTE, PC.METODODEMOVIMENTACAO, PC.SOLICITARDATADEVALIDADE, PC.SOLICITARLOTE, PE.IDPRODUTOEMBALAGEM, PC.LASTRO, PC.ALTURA  FROM PRODUTOCLIENTE PC ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ";
            sql += " INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO   ";
            sql += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE   ";
            sql += " WHERE p.CodigoDeBarras ='" + txtPesquisaCBProduto.Text + "' or pc.Codigo = '" + txtPesquisaCBProduto.Text + "' ";
            dtPesqProd = cDB.RetornarDataTable(sql, conexao);

            if (dtPesqProd.Rows.Count == 0)
            {

                MessageBox.Show("Produto Não Cadastrado.");
                return;
            }

            if (dtPesqProd.Rows.Count == 1)
            {
                txtCadastroCliente.Focus();
                gpPesquisaProduto.Enabled = false;
                textBox4.Focus();
                lblUltimoCodigoDeBarras.Text = txtPesquisaCBProduto.Text;

            }
            else
            {
                gpPesquisaProduto.Enabled = true;
                for (int i = 0; i < dtPesqProd.Rows.Count; i++)
                {
                    cboCliente.Tag = dtPesqProd.Rows[i]["CODIGO"].ToString();
                    cboCliente.Items.Add(dtPesqProd.Rows[i]["CODIGO"].ToString() + " - " + dtPesqProd.Rows[i]["FANTASIAAPELIDO"].ToString());
                    btnPesquisar.Enabled = true;
                }
                cboCliente.Focus();
                cboCliente.SelectedIndex = 0;
            }

            if (dtPesqProd.Rows.Count > 0)
            {                

                txtCadastroCodigoDoCliente.Text = dtPesqProd.Rows[0]["IDCLIENTE"].ToString();
                txtCadastroCliente.Text = dtPesqProd.Rows[0]["FANTASIAAPELIDO"].ToString();
                txtMeioDeMovimentacao.Text = dtPesqProd.Rows[0]["METODODEMOVIMENTACAO"].ToString();
                txtCadastroProduto.Text = dtPesqProd.Rows[0]["DESCRICAO"].ToString();
                txtCadastroCodigoDeBarras.Text = dtPesqProd.Rows[0]["CODIGODEBARRAS"].ToString();
                txtSolicitaLote.Text = dtPesqProd.Rows[0]["SOLICITARLOTE"].ToString();
                txtSolicitaValidade.Text = dtPesqProd.Rows[0]["SOLICITARDATADEVALIDADE"].ToString();
                lblidProdutoCliente.Text = dtPesqProd.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                lblIdProrutoEmbalagem.Text = dtPesqProd.Rows[0]["IDPRODUTOEMBALAGEM"].ToString();
                txtLastro.Text = dtPesqProd.Rows[0]["LASTRO"].ToString();
                txtAltura.Text = dtPesqProd.Rows[0]["ALTURA"].ToString();
            }
        }




        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtPesquisaCBProduto.Text != "")
            {
                PesquisarProduto();
            }
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                txtCadastroCodigoDoCliente.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["IDCLIENTE"].ToString();
                txtCadastroCliente.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["FANTASIAAPELIDO"].ToString();
                txtMeioDeMovimentacao.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["METODODEMOVIMENTACAO"].ToString();
                txtCadastroProduto.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["DESCRICAO"].ToString();
                txtCadastroCodigoDeBarras.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["CODIGODEBARRAS"].ToString();
                txtSolicitaLote.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["SOLICITARLOTE"].ToString();
                txtSolicitaValidade.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["SOLICITARDATADEVALIDADE"].ToString();
                lblidProdutoCliente.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["IdProdutoCliente"].ToString();
                lblIdProrutoEmbalagem.Text = dtPesqProd.Rows[cboCliente.SelectedIndex]["IDPRODUTOEMBALAGEM"].ToString();
                txtLastro.Text = dtPesqProd.Rows[0]["LASTRO"].ToString();
                txtAltura.Text = dtPesqProd.Rows[0]["ALTURA"].ToString();

            }
        }


        private void Salvar()
        {
            try
            {
                if (txtMeioDeMovimentacao.Text.ToUpper() == "FEFO" || txtSolicitaValidade.Text == "SIM")
                {
                    if (txtCadastroValidade.Text == "")
                    {
                        txtCadastroValidade.Focus();
                        throw new Exception("Data de Validade Obrigatório");
                    }
                }



                if (txtSolicitaLote.Text == "SIM")
                {
                    if (txtCadastroLoteReferencia.Text == "")
                    {
                        txtCadastroLoteReferencia.Focus();
                        throw new Exception("Lote é Obrigatorio");

                    }
                }

                if (textBox4.Text == "")
                {
                    textBox4.Focus();
                    throw new Exception("Informe a quantidade.");

                }

                DateTime? d = (txtCadastroValidade.Text == "  /  /" ? (DateTime?)null : DateTime.Parse(txtCadastroValidade.Text));
                string ID = cDB.RetornarIDTabela(conexao, "UnidadeDeArmazenagem").ToString();
                string digito = Mod10(ID);
                codigoUa = ID;               
                string data = "";

                if (d != null)
                    data = ((DateTime)d).Year + "-" + ((DateTime)d).Month + "-" + ((DateTime)d).Day;

                string sql = "";
                sql += " INSERT INTO UnidadeDeArmazenagem (IDUnidadeDeArmazenagem, IDFilial, IDDepositoPlantaLocalizacao, Impressao, IdProdutoCliente, IdProdutoEmbalagem, Quantidade, Validade, Lote, Digito) ";
                sql += " VALUES (" + ID + ", " + filialPadrao + ", 1, "+ "NULL" +", " + lblidProdutoCliente.Text + ", "+lblIdProrutoEmbalagem.Text+", " + int.Parse(float.Parse(textBox4.Text).ToString()) + ", " + (d == null ? "NULL" : "'" + data + "'") + ", " + (txtCadastroLoteReferencia.Text == null ? "NULL" : "'" + txtCadastroLoteReferencia.Text + "'") + ", '" + digito + "') ";

                if (txtLastro.Text.Length > 0 && txtAltura.Text.Length > 0)
                {
                    sql += "; UPDATE PRODUTOCLIENTE SET LASTRO ="+ int.Parse(float.Parse(txtLastro.Text).ToString()).ToString() +", ALTURA="+ int.Parse(float.Parse(txtAltura.Text).ToString()).ToString()+" WHERE IDPRODUTOCLIENTE = " + lblidProdutoCliente.Text;
                }


                cDB.Executar(sql, conexao);
                codigoUa = ID;               
                btnSalvar.Enabled = false;
                btnCancelar.Enabled = false;
                btnNovo.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Messangem: " + ex.Message + " - Inner: " + ex.InnerException, "Ops! Houve um problema.");
            }
        }


        public string Mod10(string idUa)
        {

            int qtdNumeros = idUa.Length;
            string[] result = new string[qtdNumeros];
            int fator = 2;
            int soma = 0;

            for (int i = qtdNumeros; i > 0; i--)
            {
                result[i - 1] = (int.Parse(idUa.Substring(i - 1, 1)) * fator).ToString();

                if (fator == 2)
                    fator = 1;
                else
                    fator = 2;
            }

            //soma os numeros individualmente
            for (int i = 0; i < result.Length; i++)
            {
                if (int.Parse(result[i]) > 9)
                {
                    soma += int.Parse(result[i].Substring(0, 1)) + int.Parse(result[i].Substring(1, 1));
                }
                else
                    soma += int.Parse(result[i]);
            }

            int resto = soma % 10;

            if (resto == 0)
                return "0";
            else
                return (10 - resto).ToString();

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
            imrimir();
            tabControl1.TabIndex = 0;
            btnImprimir.Enabled = false;            
        }

        private void btnPesquisarUA_Click(object sender, EventArgs e)
        {
            PesquisarProduto();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && textBox1.Text != "")
            {
                PesquisarProduto();
            }
        }

        private void txtCadastroQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && textBox4.Text != "")
            {
                txtCadastroLoteReferencia.Focus();
                textBox4.Text = textBox4.Text.Trim();              
            }
        }

        private void txtCadastroLoteReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                txtCadastroValidade.Focus();
        }

        private void txtCadastroValidade_KeyPress(object sender, KeyPressEventArgs e)
        {            
        }

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            if (txtPesquisaCBProduto.Text != "")
            {
                PesquisarProduto();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                btnSalvar.Focus();
        }

        private void btnConsultaPesquisar_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                textBox3.Text = "";
                grdConsulta.DataSource = null;

                Consultar();
            }
        }

        private void Consultar()
        {
            grdConsulta.DataSource = null;
            string sql = "SELECT PC.CODIGO +' - '+ PC.DESCRICAO DESCRICAO, DPL.CODIGO ";
            sql += " FROM PRODUTO P ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN PRODUTOCLIENTEREGRA PCR ON PCR.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = PCR.IDDEPOSITOPLANTALOCALIZACAO ";
            sql += " WHERE P.CODIGODEBARRAS = '" + textBox2.Text + "' ";
            sql += " ORDER BY DPL.CODIGO ";
            
            DataTable  d = cDB.RetornarDataTable(sql, conexao);

            if (d.Rows.Count > 0)
            {
                grdConsulta.DataSource = d;
                textBox3.Text = d.Rows[0][0].ToString();
                grdConsulta.Columns[0].Visible = false;
            }
            else
                MessageBox.Show("Picking não encontrado");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && textBox2.Text.Length>0)
                Consultar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            gpPesquisaProduto.Enabled = true;
            txtPesquisaCBProduto.Focus();
            txtPesquisaCBProduto.Text = "";            
            txtCadastroCodigoDoCliente.Text = "";
            txtCadastroCliente.Text = "";
            txtCadastroCodigoDeBarras.Text = "";
            txtCadastroLoteReferencia.Text = "";
            txtCadastroValidade.Text = "";
            txtCadastroProduto.Text = "";
            textBox4.Text = "";
            txtLastro.Text = "";
            txtAltura.Text = "";
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            txtPesquisaCBProduto.Text = lblUltimoCodigoDeBarras.Text;
            PesquisarProduto();            
        }

        private void txtCadastroValidade_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtCadastroValidade.Text.Length > 0)
                txtLastro.Focus();
        }

        private void txtLastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtLastro.Text.Length > 0)
                txtAltura.Focus();
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtAltura.Text.Length > 0)
                btnSalvar.Focus();
        }

        private void tabControl2_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 1)
                textBox2.Focus();
            else if (tabControl2.SelectedIndex == 2)
                txtQtUA.Focus();
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 1)
                textBox2.Focus();
            else if (tabControl2.SelectedIndex == 2)
                txtQtUA.Focus();
        }

        private void btnPesquisarUA_Click_1(object sender, EventArgs e)
        {
            PesquisarItens();
        }

        private void brnImprimirUa_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < int.Parse(txtQtUA.Text); i++)
            {
                string sql = "";
                string ID = cDB.RetornarIDTabela(conexao, "UnidadeDeArmazenagem").ToString();
                string dig = Mod10(ID);

                sql += " INSERT INTO UnidadeDeArmazenagem (IDUnidadeDeArmazenagem, IDFilial, Digito, IdDepositoplantaLocalizacao) ";
                sql += " VALUES (" + ID + ", 1, '" + dig + "', 0) ";
                cDB.Executar(sql, conexao);
                System.Text.StringBuilder sb = new StringBuilder();
                //sb.AppendLine("^DFB:etiqu.ZPL^FS");


                sb.Append("^XA");
                //^DFR:UA.ZPL^FS
                sb.Append("~TA000~JSN^LT0^MNW^MTD^PON^PMN^LH0,0^JMA^PR4,4^MD15^LRN^CI0");
                sb.Append("^MMT");
                sb.Append("^PW799");
                sb.Append("^LL0400");
                sb.Append("^LS0");
                sb.Append(@"^FT94,73^A0N,28,28^FH\^FDUA"+ (checkBox1.Checked==true?"-CONFERENCIA": "-VAZIA") +"^FS");
                sb.Append(@"^FT93,106^A0N,25,36^FH\^FD" + ID + "^FS");
                //sb.Append("^BY4,3,119^FT89,278^BCN,,N,N");
                //sb.Append("^FD>;"+ID+dig+"^FS");

                sb.AppendLine("^^BY1,3,64^FT466,380^B3N,N,,N,N");
                //sb.AppendLine("^BY1,3,64^FT373,380^B3N,N,,N,N");
                sb.AppendLine("^FD" + ID + dig + "^FS");
                sb.AppendLine("^FO54,296^GB716,0,2^FS");


                sb.Append("^XZ");


                RawPrinterHelper.SendStringToPrinter(System.Configuration.ConfigurationSettings.AppSettings["CaminhoImpressora"], sb.ToString());

            }
        }
    }
}