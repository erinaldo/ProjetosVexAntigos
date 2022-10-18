using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public partial class CLW00021 : Form
    {
        public CLW00021()
        {
            InitializeComponent();
        }

        private void CLW00001_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                lblTitulo.Text = VarGlobal.NomePrograma;
                dtServ = null;
                dtServFormat = null;
                CarregarServicos();
            }
            catch (Exception ecx)
            {
                MessageBox.Show(ecx.Message);
            }
        }

        DataTable dtServ;
        DataTable dtServFormat;
        private void CarregarServicos()
        {

            List<ParametrosProcedures> par = new List<ParametrosProcedures>();

            ParametrosProcedures p = new ParametrosProcedures();
            p.nomePar = "TIPO";
            p.valorPar = "SAIDA";
            p.tipoDeDados = "string";
            par.Add(p);

            p = new ParametrosProcedures();
            p.nomePar = "OPERACAOCOLETOR";
            p.valorPar = "EMPILHADEIRA";
            p.tipoDeDados = "string";

            par.Add(p);
            dtServ = Classes.BdExterno.RetornarDataTableProcedure("PRC_SERVICOSAEXECUTAR", par, VarGlobal.Conexao);

            dtServFormat = new DataTable();
            dtServFormat.Columns.Add("EnderecoOrigem");
            dtServFormat.Columns.Add("EnderecoDestino");
            dtServFormat.Columns.Add("IDUnidadeDeArmazenagem");
            dtServFormat.Columns.Add("IDRomaneio");
            dtServFormat.Columns.Add("IDMovimentacaoItem");



            for (int i = 0; i < dtServ.Rows.Count; i++)
            {
                DataRow r = dtServFormat.NewRow();

                r[0] = dtServ.Rows[i]["EnderecoOrigem"].ToString();
                r[1] = dtServ.Rows[i]["EnderecoDestino"].ToString();
                r[2] = dtServ.Rows[i]["IDUnidadeDeArmazenagem"].ToString();
                r[3] = dtServ.Rows[i]["IDRomaneio"].ToString();
                r[4] = dtServ.Rows[i]["IDMovimentacaoItem"].ToString();

                dtServFormat.Rows.Add(r);

            }

            grdServicos.DataSource = dtServFormat;



        }

        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                if (e.KeyChar == (char)Keys.Return)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //int row = grdServicos.CurrentCell.RowNumber;
                    string itemClicado = grdServicos[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (e.KeyChar == (char)Keys.Return && txtUa.Text != "" && Helpers.RetirarDigitoUA(txtUa.Text) == dadoSelecionado[0]["idUnidadedeArmazenagem"].ToString())
                    {
                        e.Handled = true;
                        PesquisarUA();
                    }
                    else
                    {
                        MessageBox.Show("UA Invalida");
                        txtUa.SelectAll();
                        txtUa.Focus();
                    }
                    Cursor.Current = Cursors.Default;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        DataTable dtUa;
        private void PesquisarUA()
        {
            if (tabControl1.SelectedIndex == 1)
                dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarSaida(txtUa.Text, txtEndereco.Text);
            else
                dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarSaida(txtUaLivre.Text, txtEnderecoLivre.Text);


            if (tabControl1.SelectedIndex == 1)
            {
                if (dtUa.Rows.Count == 0 || dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() != txtEndereco.Text)
                {
                    MessageBox.Show("Pallet não encontrado ou o Endereço não é Valido .", "Atenção");
                    InciaControles();
                }
                else
                {
                    lblSaldoUa.Text = int.Parse(float.Parse(dtUa.Rows[0]["SALDO"].ToString()).ToString()).ToString();
                    //lblServicoDescricaoProduto.Text = dtUa.Rows[0]["DESCRICAO"].ToString();
                    //lblServicoCodigoProduto.Text = dtUa.Rows[0]["CODIGOPRODUTO"].ToString();
                    //lblServEnderecoOrigem.Text = dtUa.Rows[0]["ENDERECO"].ToString();
                    //lblServicoQuantidade.Text = lblSaldoUa.Text;
                    //lblServicoUA.Text = dtUa.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString();
                    lblIdProdutoCliente.Text = dtUa.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    txtUaDestino.Enabled = true;
                    txtUaDestino.Focus();
                    //btnConfirmar.Enabled = true;

                }
            }
            else
            {
                if (dtUa.Rows.Count == 0 || dtUa.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() != txtEnderecoLivre.Text)
                {
                    MessageBox.Show("Pallet não encontrado ou o Endereço não é Valido .", "Atenção");
                    InciaControles();
                }
                else
                {
                    lblSaldoUa.Text = int.Parse(float.Parse(dtUa.Rows[0]["SALDO"].ToString()).ToString()).ToString();
                    //lblServicoDescricaoProduto.Text = dtUa.Rows[0]["DESCRICAO"].ToString();
                    //lblServicoCodigoProduto.Text = dtUa.Rows[0]["CODIGOPRODUTO"].ToString();
                    //lblServEnderecoOrigem.Text = dtUa.Rows[0]["ENDERECO"].ToString();
                    //lblServicoQuantidade.Text = lblSaldoUa.Text;
                    //lblServicoUA.Text = dtUa.Rows[0]["IDUNIDADEDEARMAZENAGEM"].ToString();
                    lblIdProdutoCliente.Text = dtUa.Rows[0]["IDPRODUTOCLIENTE"].ToString();
                    txtRomanioLivre.Enabled = true;
                    txtRomanioLivre.Focus();
                    //btnConfirmar.Enabled = true;

                }
            }







        }

        private void InciaControles()
        {
            txtEndereco.Text = "";
            txtUa.Text = "";
            txtUa.Enabled = false;
            btnConfirmar.Enabled = false;
            //lblServicoDescricaoProduto.Text = "";
            lblUaDestino.Text = "";
            lblSaldoUa.Text = "0";
            //lblServicoDescricaoProduto.Text = "";
            lblServicoCodigoProduto.Text = "";
            lblServEnderecoOrigem.Text = "";
            lblServEnderecoOrigem.Text = "";
            lblServEnderecoDestino.Text = "";
            lblIdProdutoCliente.Text = "";
            lblServicoQuantidade.Text = "";
            lblServRomaneio.Text = "";
            lblServicoUA.Text = "";
            lblServicoCodigoProduto.Text = "";
            tabControl1.SelectedIndex = 0;
            //txtEndereco.Focus();
            dtServ = null;
            dtServFormat = null;
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            Salvar();
        }

        private void Salvar()
        {
            try
            {
                if (txtUa.Text != "" && txtUaDestino.Text != "")
                {
                    //string[] idRom = txtUaDestino.Text.Split('X');
                    string rom = txtUaDestino.Text.Substring(0, 10);
                    string Conf = txtUaDestino.Text.Substring(10, 10);

                    rom = int.Parse(rom).ToString();
                    Conf = int.Parse(Conf).ToString();
                    DataTable dtrom = Classes.BdExterno.RetornarDT("select * from romaneio where idromaneio=" + rom, VarGlobal.Conexao);

                    DataTable DtConfPallet = Classes.BdExterno.RetornarDT("Select * from ConferenciaPallet where idConferencia=" + Conf + " and IdPallet=" + dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString(), VarGlobal.Conexao);

                    if (DtConfPallet.Rows.Count > 0)
                    {
                        MessageBox.Show("Pallet já incluido no Romaneio: " + rom);
                        InciaControles();
                        return;
                    }

                    if (dtrom.Rows.Count == 0)
                    {
                        MessageBox.Show("Romaneio: " + rom + " não encontrado.");
                        InciaControles();
                        return;
                    }

                    string itemClicado = grdServicos[row, 4].ToString();
                    //DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");


                    string sql = "update UnidadeDeArmazenagem set IddepositoPlantaLocalizacao= " + dtrom.Rows[0]["IdDepositoplantaLocalizacao"].ToString() + " where idUnidadeDeArmazenagem=" + dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString();
                    sql += "; update movimentacaoItem set ExecutouSaida='sim' where idmovimentacaoItem=" + itemClicado;
                    string idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                    sql += " ; INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO, UACompleta, executouServico)";
                    sql += " VALUES (" + idConferenciaPallet + ", " + Conf + ", " + dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString() + ", " + dtrom.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() + ", 'CONFERENCIA', 'SIM', 'SIM')";


                    Classes.BdExterno.Executar(sql, VarGlobal.Conexao);

                    MessageBox.Show("Registro Gravado com Sucesso");
                    InciaControles();
                    CarregarServicos();
                    tabControl1.SelectedIndex = 0;


                    /*
                    //verifica se ele colocou o endereco de destino corretamente
                    //int row = grdServicos.CurrentCell.RowNumber;
                    string itemClicado = grdServicos[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (txtUaDestino.Text == dadoSelecionado[0]["IDDepositoPlantaLocalizacaoDestino"].ToString())
                    {

                        new Classes.BLL.Estoque().SairUAInteira(dadoSelecionado[0]["IdUnidadeDeArmazenagem"].ToString(), lblIdProdutoCliente.Text, lblServicoQuantidade.Text, dadoSelecionado[0]["IDDepositoPlantaLocalizacaoOrigem"].ToString(), dadoSelecionado[0]["IDDepositoPlantaLocalizacaoDestino"].ToString(), dtUa.Rows[0]["IDPRODUTOEMBALAGEM"].ToString(), dadoSelecionado[0]["IdmovimentacaoItem"].ToString(), dadoSelecionado[0]["IDRomaneio"].ToString());
                        MessageBox.Show("Registro Gravado com Sucesso");
                        InciaControles();
                        CarregarServicos();
                        tabControl1.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Endereço Incorreto");
                        txtUaDestino.SelectAll();
                        txtUaDestino.Focus();
                    }*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CLW00008_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                if (e.KeyChar == (char)Keys.Return)
                {
                    //int row = grdServicos.CurrentCell.RowNumber;
                    string itemClicado = grdServicos[row, 4].ToString();
                    DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

                    if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtEndereco.Text == dadoSelecionado[0]["iddepositoplantalocalizacaoorigem"].ToString())
                    // if (e.KeyChar == (char)Keys.Return && txtEndereco.Text != "" && txtEndereco.Text == dadoSelecionado[0]["EnderecoOrigem"].ToString())
                    {
                        e.Handled = true;
                        txtUa.Enabled = true;
                        txtUa.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Endereço de Origem Invalido");
                        txtEndereco.SelectAll();
                        txtEndereco.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUaDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                if (e.KeyChar == (char)Keys.Return && txtUaDestino.Text != "")
                {
                    e.Handled = true;

                    btnConfirmar.Enabled = true;
                    btnConfirmar.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                txtUaDestino.Enabled = true;
                txtUaDestino.Focus();
            }
        }
        int row = 0;
        private void grdServicos_DoubleClick(object sender, EventArgs e)
        {
            excecutarServico();
        }

        private void excecutarServico()
        {
            row = grdServicos.CurrentCell.RowNumber;
            string itemClicado = grdServicos[row, 4].ToString();

            DataRow[] dadoSelecionado = dtServ.Select("IDMovimentacaoItem=" + itemClicado, "");

            if (dadoSelecionado.Length > 0)
            {
                lblServEnderecoOrigem.Text = dadoSelecionado[0]["EnderecoOrigem"].ToString();
                lblServEnderecoDestino.Text = dadoSelecionado[0]["EnderecoDestino"].ToString();
                lblServicoUA.Text = dadoSelecionado[0]["IDUnidadeDeArmazenagem"].ToString();
                lblServRomaneio.Text = dadoSelecionado[0]["IDRomaneio"].ToString();
                lblServicoCodigoProduto.Text = dadoSelecionado[0]["Codigo"].ToString() + "-" + dadoSelecionado[0]["Descricao"].ToString();
                lblServicoQuantidade.Text = int.Parse(float.Parse(dadoSelecionado[0]["Quantidade"].ToString()).ToString()).ToString();

                tabControl1.SelectedIndex = 1;
                txtEndereco.Focus();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && lblServicoUA.Text == "")
            {
                tabControl1.SelectedIndex = 0;
                CarregarServicos();
            }
            else if (tabControl1.SelectedIndex == 1 && lblServicoUA.Text != "")
            {
                txtUaDestino.Text = "";
                txtEndereco.Focus();
            }

            if (tabControl1.SelectedIndex == 2)
            {
                txtRomanioLivre.Text = "";
                txtProdutoLivre.Text = "";
                txtUaPesquisa.Text = "";
                txtProdutoLivre.Focus();
                //CarregarList("");
            }




        }

        private void tabControl1_Validated(object sender, EventArgs e)
        {

        }

        private void txtUaDestino_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtEnderecoLivre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtEnderecoLivre.Text != "")
            {
                e.Handled = true;
                CarregarList("");
            }
        }

        private void txtUaLivre_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return && txtUaLivre.Text != "")
                {

                    if (Helpers.RetirarDigitoUA(txtUaLivre.Text) == UaClicada)
                    {
                        PesquisarUALivre();
                        e.Handled = true;

                    }
                    else
                    {
                        MessageBox.Show("Ua Invalida");
                        txtUaLivre.SelectAll();
                        txtUaLivre.Text = "";
                        txtRomanioLivre.Enabled = false;

                        txtUaLivre.Focus();
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problema ao pesuisar UA");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void PesquisarUALivre()
        {
            dtUa = new Classes.BLL.UnidadeDeArmazenagem().RetornarSaidaLivre(txtUaLivre.Text, txtEnderecoLivre.Text);
            if (dtUa.Rows.Count == 0)
            {
                MessageBox.Show("Pallet não encontrado ou o Endereço não é Valido .", "Atenção");
                txtUaLivre.Text = "";
                txtRomanioLivre.Enabled = false;
                txtUaLivre.Focus();
            }
            else
            {
                txtRomanioLivre.Enabled = true;
                txtRomanioLivre.Focus();
            }
        }

        private void txtRomanioLivre_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;


                if (e.KeyChar == (char)Keys.Return && txtRomanioLivre.Text != "")
                {
                    e.Handled = true;

                    btnConfirmarLivre.Enabled = true;
                    btnConfirmarLivre.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnConfirmarLivre_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUaLivre.Text != "" && txtRomanioLivre.Text != "")
                {
                    //string[] idRom = txtUaDestino.Text.Split('X');
                    string rom = txtRomanioLivre.Text.Substring(0, 10);
                    string Conf = txtRomanioLivre.Text.Substring(10, 10);

                    rom = int.Parse(rom).ToString();
                    Conf = int.Parse(Conf).ToString();
                    DataTable dtrom = Classes.BdExterno.RetornarDT("SELECT * FROM ROMANEIO WHERE IDROMANEIO=" + rom, VarGlobal.Conexao);

                    DataTable DtConfPallet = Classes.BdExterno.RetornarDT("SELECT * FROM CONFERENCIAPALLET WHERE IDCONFERENCIA=" + Conf + " AND IDPALLET=" + dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString(), VarGlobal.Conexao);

                    //DataTable dchecaMovi = Classes.BdExterno.RetornarDT("SELECT count(*) FROM MOVIMENTACAOITEM MI INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE  =  MI.IDPRODUTOCLIENTE WHERE MI.IDROMANEIO = " + rom + " AND PE.IDPRODUTOEMBALAGEM = " + embalagem + " AND MI.QUANTIDADE =" + int.Parse(float.Parse(saldo).ToString()), VarGlobal.Conexao);

                    //if (dchecaMovi.Rows[0][0].ToString() != "0")
                    //{
                    //    MessageBox.Show("Dados do Romaneio nao comrrerponde ao item e quantidade solicitada.");
                    //    txtRomanioLivre.SelectAll();
                    //    return;
                    //}

                    if (DtConfPallet.Rows.Count > 0)
                    {
                        MessageBox.Show("Pallet já incluido no Romaneio: " + rom);
                        InciaControlesLivre();
                        return;
                    }

                    if (dtrom.Rows.Count == 0)
                    {
                        MessageBox.Show("Romaneio: " + rom + " não encontrado.");
                        InciaControlesLivre();
                        return;
                    }


                    if (saldo == "")
                        saldo = "0";

                    string sql = " UPDATE UNIDADEDEARMAZENAGEM SET IDDEPOSITOPLANTALOCALIZACAO= " + dtrom.Rows[0]["IdDepositoplantaLocalizacao"].ToString() + " WHERE IDUNIDADEDEARMAZENAGEM=" + dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString();

                    string idConferenciaPallet = BdExterno.RetornarIDTabela("CONFERENCIAPALLET").ToString();
                    sql += " INSERT INTO CONFERENCIAPALLET (IdConferenciaPallet, IdConferencia, IdPallet, IdDepositoPlantaLocalizacao, TIPO, UACompleta, IdDepositoPlantaLocalizacaoOrigem, IdUnidadeDeArmazenagemOrigem)";
                    sql += " VALUES (" + idConferenciaPallet + ", " + Conf + ", " + dtUa.Rows[0]["IdUnidadeDeArmazenagem"].ToString() + ", " + dtrom.Rows[0]["IDDEPOSITOPLANTALOCALIZACAO"].ToString() + ", 'SEPARACAO', 'SIM', " + txtEnderecoLivre.Text + ", " + Helpers.RetirarDigitoUA(txtUaLivre.Text) + ")";

                    string idConferenciaPalletProduto = BdExterno.RetornarIDTabela("CONFERENCIAPALLETPRODUTO").ToString();
                    sql += " INSERT INTO CONFERENCIAPALLETPRODUTO ( IDCONFERENCIAPALLETPRODUTO,IDCONFERENCIAPALLET,IDPRODUTOEMBALAGEM,QUANTIDADE,TIPO,LANCAMENTO, IdDepositoPlantaLocalizacaoOrigem, IdUnidadeDeAemazenagemOrigem) ";
                    sql += " VALUES (" + idConferenciaPalletProduto + "," + idConferenciaPallet + "," + embalagem + "," + int.Parse(float.Parse(saldo).ToString()) + ",NULL,GETDATE(), NULL, NULL ) ";

                    Classes.BdExterno.Executar(sql, VarGlobal.Conexao);

                    MessageBox.Show("Registro Gravado com Sucesso");
                    InciaControlesLivre();
                    txtEnderecoLivre.Text = "";
                    txtUaLivre.Text = "";
                    txtRomanioLivre.Text = "";
                    btnConfirmarLivre.Enabled = false;
                    txtProdutoLivre.Text = "";
                    txtUaPesquisa.Text = "";
                    dtUa = null;
                    dataGrid1.DataSource = null;

                    txtProdutoLivre.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InciaControlesLivre()
        {
            dtUa = null;
            btnConfirmarLivre.Enabled = false;
            txtRomanioLivre.Enabled = false;
            txtUa.Enabled = false;
            txtEnderecoLivre.Text = "";
            txtProdutoLivre.Text = "";
            txtUaPesquisa.Text = "";
            txtProdutoLivre.Focus();
        }

        private void grdServicos_Click(object sender, EventArgs e)
        {
            excecutarServico();
        }

        private void txtProdutoLivre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtProdutoLivre.Text != "")
            {
                e.Handled = true;
                CarregarList("CB");
            }
        }

        string UaClicada = "";
        string embalagem = "";
        string saldo = "0";
        private void CarregarList(string TipoPesq)
        {


            string sql = "";
            sql += " SELECT DISTINCT";
            sql += " PC.IDPRODUTOCLIENTE, PC.IDCLIENTE, PC.DESCRICAO, UAL.IDLOTE, UAL.IDUNIDADEDEARMAZENAGEM, UAL.SALDO, ua.iddepositoplantalocalizacao, dpl.codigo, PE.IdProdutoEmbalagem ";
            sql += " FROM PRODUTO P ";
            sql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO ";
            sql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN CLIENTEFILIAL CF ON CF.IDCLIENTE = PC.IDCLIENTE ";
            sql += " INNER JOIN LOTE L ON L.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON UAL.IDLOTE = L.IDLOTE ";
            sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM  = UAL.IDUNIDADEDEARMAZENAGEM ";
            sql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO ";

            if (TipoPesq == "CB")
                sql += " WHERE P.CODIGODEBARRAS = '" + txtProdutoLivre.Text + "' ";
            else if (TipoPesq == "UA")

                sql += " WHERE ua.IDUNIDADEDEARMAZENAGEM =  " + Helpers.RetirarDigitoUA(txtUaPesquisa.Text) ;

            else
                sql += " WHERE DPL.IDDEPOSITOPLANTALOCALIZACAO =  " + txtEnderecoLivre.Text;

            sql += " AND L.IDPRODUTOEMBALAGEM=PE.IDPRODUTOEMBALAGEM ";
            sql += " AND CF.CLIENTELOGISTICA = 'SIM' ";
            sql += " AND UAL.SALDO>0 ";
            sql += " AND UA.IDUNIDADEDEARMAZENAGEM NOT IN (SELECT IDPALLET FROM CONFERENCIAPALLET WHERE TIPO='SEPARACAO')";


            DataTable DTaux = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);


            DataTable x = new DataTable();
            x.Columns.Add("Endereco");
            x.Columns.Add("UA");
            x.Columns.Add("Produto");
            x.Columns.Add("Quantidade");
            x.Columns.Add("Info");
            x.Columns.Add("IdProdutoEmbalagem");
            x.Columns.Add("IdDepositoPlantaLocalizacao");


            for (int i = 0; i < DTaux.Rows.Count; i++)
            {
                DataRow item = x.NewRow();
                item[0] = DTaux.Rows[i]["CODIGO"].ToString();
                item[1] = DTaux.Rows[i]["IDUNIDADEDEARMAZENAGEM"].ToString();
                item[2] = DTaux.Rows[i]["DESCRICAO"].ToString();
                item[3] = DTaux.Rows[i]["SALDO"].ToString();

                string descr = DTaux.Rows[i]["DESCRICAO"].ToString();

                if (descr.Length > 30)
                    descr = descr.Substring(0, 30) + "\n" + descr.Substring(30, descr.Length - 31);

                item[4] = DTaux.Rows[i]["CODIGO"].ToString() + " | UA: " + DTaux.Rows[i]["IDUNIDADEDEARMAZENAGEM"].ToString() + " | Saldo: " + DTaux.Rows[i]["SALDO"].ToString() + "\n" + descr;
                item[5] = DTaux.Rows[i]["IdProdutoEmbalagem"].ToString();
                item[6] = DTaux.Rows[i]["iddepositoplantalocalizacao"].ToString();


                x.Rows.Add(item);
            }


            dataGrid1.DataSource = x;

            if (x.Rows.Count > 0)
            {
                //dataGrid1.Select(0);
                UaClicada = "0"; //dataGrid1[0, 2].ToString();
            }
            else
            {
                InciaControlesLivre();
                MessageBox.Show("Endereço ou Produto não encontrado");
            }


        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            txtUaLivre.Text = "";
            txtEnderecoLivre.Text = "";
            int linha = dataGrid1.CurrentCell.RowNumber;
            dataGrid1.Select(linha);
            UaClicada = dataGrid1[linha, 2].ToString();
            saldo = dataGrid1[linha, 4].ToString();
            embalagem = dataGrid1[linha, 5].ToString();
            txtEnderecoLivre.Text = dataGrid1[linha, 6].ToString();

        }

        string liverIdDepositoPL = "";
        private void dataGrid1_Click(object sender, EventArgs e)
        {
            txtUaLivre.Text = "";
            txtEnderecoLivre.Text = "";

            int linha = dataGrid1.CurrentCell.RowNumber;
            dataGrid1.Select(linha);
            UaClicada = dataGrid1[linha, 2].ToString();
            saldo = dataGrid1[linha, 4].ToString();
            embalagem = dataGrid1[linha, 5].ToString();
            txtEnderecoLivre.Text = dataGrid1[linha, 6].ToString();


            txtUaLivre.Enabled = true;
            txtUaLivre.Focus();
        }

        private void txtUaLivre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUaPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return && txtUaPesquisa.Text != "")
            {
                e.Handled = true;
                CarregarList("UA");
            }
        }
    }

}