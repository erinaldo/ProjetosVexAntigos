using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistecnoColetor
{
    public partial class CLW00010 : Form
    {
        public CLW00010()
        {
            InitializeComponent();
        }

        private void CLW00010_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            txtCodigoDebarras.Focus();
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {


                if (txtEndereco.Text != "" || txtCodigoDebarras.Text != "")
                    Pesquisar();
                else
                    MessageBox.Show("Informe ao menos um campo para pesquisa.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        DataTable dt;
        private void Pesquisar()
        {
            try
            {


                string SQL = "SELECT distinct ";
                SQL += " ISNULL( (SELECT top 1 ISNULL( UALI.SALDO, 0) FROM UNIDADEDEARMAZENAGEMLOTE UALI WHERE UALI.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM),0) SALDOUA, ";
                SQL += " P.CODIGODEBARRAS [CODIGO DE BARRAS], pc.idcliente [IDCLIENTE],";
                SQL += " PC.CODIGO [CODIGO], ";
                SQL += " PC.DESCRICAO [PRODUTO],  ";
                SQL += " PC.METODODEMOVIMENTACAO [METODO], ";
                SQL += " PC.IDPRODUTOCLIENTE [IDPRODUTOCLIENTE],  ";
                SQL += " C.RAZAOSOCIALNOME  [CLIENTE], DPL.CODIGO [POSICAO], ENDERECO [ENDERECO],  UA.IDUNIDADEDEARMAZENAGEM [UA], UA.LOTE [REFERENCIA], CONVERT(VARCHAR(10), UA.VALIDADE, 103) [VALIDADE], ";
                SQL += " ( ";
                SQL += " SELECT TOP 1 DPLI.CODIGO FROM PRODUTOCLIENTEREGRA PCR   ";
                SQL += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPLI ON DPLI.IDDEPOSITOPLANTALOCALIZACAO = PCR.IDDEPOSITOPLANTALOCALIZACAO ";
                SQL += " WHERE PCR.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ORDER BY 1) [PRIMEIROENDERECOPICKING] ";
                SQL += " FROM PRODUTO P ";
                SQL += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTO = P.IDPRODUTO ";
                SQL += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE  =PE.IDPRODUTOCLIENTE ";
                SQL += " INNER JOIN CADASTRO C ON C.IDCADASTRO = PC.IDCLIENTE ";
                SQL += " LEFT  JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
                SQL += " LEFT JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = UA.IDDEPOSITOPLANTALOCALIZACAO ";
                
                //SQL += " LEFT JOIN UnidadeDeArmazenagemLote UAL ON UAL.IDUnidadeDeArmazenagem = UAL.IDUnidadeDeArmazenagem";

                if (txtCodigoDebarras.Text.Length > 0)
                    SQL += "  WHERE (P.CODIGODEBARRAS = '" + txtCodigoDebarras.Text.Trim() + "' OR PC.CODIGO='" + txtCodigoDebarras.Text + "') ";

                else if (txtEndereco.Text.Length > 0)
                    SQL += " WHERE DPL.IDDEPOSITOPLANTALOCALIZACAO =" + txtEndereco.Text;

                SQL += " AND IDFILIAL = " + VarGlobal.Usuario.UltimaFilial;
                SQL += " AND UA.STATUS ='EM ESTOQUE'";
                SQL += " AND ISNULL( (SELECT top 1 ISNULL( UALI.SALDO, 0) FROM UNIDADEDEARMAZENAGEMLOTE UALI WHERE UALI.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM),0)>0";
                //SQL += " AND UAL.SALDO>0";

                dt = Classes.BdExterno.RetornarDT(SQL, VarGlobal.Conexao);

                

                if (dt.Rows.Count == 0)
                    MessageBox.Show("Nenhum Item Encontrado");
                else
                {
                    lblResultEnderecoPicking.Text = dt.Rows[0]["PRIMEIROENDERECOPICKING"].ToString();

                    DataTable dtGrid = new DataTable();
                    dtGrid.Columns.Add("CLIENTE");
                    dtGrid.Columns.Add("IDCLIENTE");
                    dtGrid.Columns.Add("PRODUTO");
                    dtGrid.Columns.Add("IDPRODUTOCLIENTE");
                    dtGrid.Columns.Add("CODIGO DE BARRAS");
                    dtGrid.Columns.Add("CODIGO");

                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "CLIENTE", "IDCLIENTE", "PRODUTO", "IDPRODUTOCLIENTE", "CODIGO", "CODIGO DE BARRAS");
                    dataGrid1.DataSource = distinctValues;
                    dataGrid1.Select(0);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void txtCodigoProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                Pesquisar();
            }
        }

        private void txtCodigoDebarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                Pesquisar();
            }
        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                Pesquisar();
            }
        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            int linha = dataGrid1.CurrentCell.RowNumber;
            tabControl1.SelectedIndex = 1;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 1)
            {
                int linha = dataGrid1.CurrentCell.RowNumber;

                if (dt.Rows.Count == 0)
                    tabControl1.SelectedIndex = 0;

                carregarDetalhes();

            }
        }

        private void carregarDetalhes()
        {
            try
            {

                int linha = dataGrid1.CurrentCell.RowNumber;

                DataRow[] l = dt.Select("IDCLIENTE=" + dataGrid1[linha, 5].ToString() + " AND [CODIGO DE BARRAS]='" + dataGrid1[linha, 3].ToString() + "' ", "POSICAO");
                lblResultCodigoBarras.Text = l[0]["CODIGO DE BARRAS"].ToString();
                lblResultDescricao.Text = l[0]["PRODUTO"].ToString();
                lblResultEnderecoPicking.Text = l[0]["PRIMEIROENDERECOPICKING"].ToString();


                DataTable dtGdr2 = new DataTable();
                dtGdr2.Columns.Add("ENDERECO");
                dtGdr2.Columns.Add("UA");
                dtGdr2.Columns.Add("REFERENCIA");
                dtGdr2.Columns.Add("VALIDADE");
                dtGdr2.Columns.Add("SALDOUA");


                for (int i = 0; i < l.Length; i++)
                {
                    DataRow r = dtGdr2.NewRow();
                    r[0] = l[i]["POSICAO"].ToString();
                    r[1] = l[i]["UA"].ToString();
                    r[2] = l[i]["REFERENCIA"].ToString();
                    r[3] = l[i]["VALIDADE"].ToString();
                    r[4] = l[i]["SALDOUA"].ToString();
                    dtGdr2.Rows.Add(r);
                }

                if (l[0]["METODO"].ToString().ToUpper() == "FIFO")
                {
                    DataView dv = dtGdr2.DefaultView;
                    dv.Sort = "REFERENCIA asc";
                    DataTable sortedDT = dv.ToTable();
                    dataGrid2.DataSource = sortedDT;
                }
                else
                {
                    DataView dv = dtGdr2.DefaultView;
                    dv.Sort = "VALIDADE asc";
                    DataTable sortedDT = dv.ToTable();
                    dataGrid2.DataSource = sortedDT;
                }




                string SQL = " SELECT DPL.Codigo PICKING  ";
                SQL += " FROM ProdutoClienteRegra P ";
                SQL += " INNER JOIN DepositoPlantaLocalizacao DPL ON DPL.IDDepositoPlantaLocalizacao =  P.IdDepositoPlantaLocalizacao ";
                SQL += " WHERE IDPRODUTOCLIENTE  = " + dataGrid1[linha, 0].ToString();
                SQL += " AND  TIPODEREGRA='PICKING' ";
                SQL += " ORDER BY DPL.Codigo ";

                dataGrid3.DataSource = Classes.BdExterno.RetornarDT(SQL, VarGlobal.Conexao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label9_ParentChanged(object sender, EventArgs e)
        {

        }

        private void label8_ParentChanged(object sender, EventArgs e)
        {

        }

        private void dataGrid2_CurrentCellChanged(object sender, EventArgs e)
        {

        }
    }
}