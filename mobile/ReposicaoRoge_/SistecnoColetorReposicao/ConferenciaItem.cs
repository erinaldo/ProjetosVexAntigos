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
    public partial class ConferenciaItem : Form
    {
        public ConferenciaItem()
        {
            InitializeComponent();
        }

        public DataTable Dt { get; set; }
        public string  Chave { get; set; }
        public int IdDocumento{get;set;}     

        
        private void ConfVolume_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                lblTitulo.Text = VarGlobal.NomePrograma;
                txtCbVolume.Focus();
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCbVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtCbVolume.Text != "")
                {
                    if (int.Parse(Dt.Compute("Count(IdDocumento)", "CodigoDeBarras='" + txtCbVolume.Text + "' and IdDocumento=" + IdDocumento).ToString()) > 0)
                    {
                        txtQuantidade.Focus();
                        txtQuantidade.Text = "1";
                        txtQuantidade.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("Produto Não Enconrado");
                        txtCbVolume.Text = "";
                        txtCbVolume.Focus();
                        txtQuantidade.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnConfirmarVolume_Click_1(object sender, EventArgs e)
        {
            try
            {
                Conciliar();
                pnlConfere.Visible = false;
                pnlDiferencas.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable dtConc;
        private void Conciliar()
        {
            dtConc = new DataTable();
            dtConc.Columns.Add("CODIGODEBARRAS");
            dtConc.Columns.Add("QUANTIDADE", System.Type.GetType("System.Decimal"));
            dtConc.Columns.Add("QUANTIDADEAFERIDA", System.Type.GetType("System.Decimal"));
            dtConc.Columns.Add("FALTA", System.Type.GetType("System.Decimal"));
            dtConc.Columns.Add("VALORUNITARIO", System.Type.GetType("System.Decimal"));
            dtConc.Columns.Add("TOTALITEM", System.Type.GetType("System.Decimal"));
            dtConc.Columns.Add("SEQUENCIA", System.Type.GetType("System.Int32"));
            dtConc.Columns.Add("CODIGO", System.Type.GetType("System.String"));
            dtConc.Columns.Add("IDDOCUMENTOITEM", System.Type.GetType("System.Int32"));
            dtConc.Columns.Add("DESCRICAO");


            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow o = dtConc.NewRow();

                o["CODIGO"] = Dt.Rows[i]["CODIGO"].ToString();
                o["IDDOCUMENTOITEM"] = Dt.Rows[i]["IDDOCUMENTOITEM"].ToString();
                o["DESCRICAO"] = Dt.Rows[i]["DESCRICAO"].ToString();
                o["CODIGODEBARRAS"] = Dt.Rows[i]["CODIGODEBARRAS"].ToString();
                o["QUANTIDADE"] = Dt.Rows[i]["QUANTIDADE"].ToString();
                string QtdAferida = dtItens.Compute("SUM(QUANTIDADE)", "CODIGODEBARRAS='" + Dt.Rows[i]["CodigoDeBarras"].ToString() + "'").ToString();

                if (QtdAferida == "")
                    QtdAferida = "0";

                o["QUANTIDADEAFERIDA"] = (decimal.Parse(QtdAferida)) * (decimal.Parse(Dt.Rows[i]["FATOR"].ToString()));
                o["FALTA"] = Math.Abs((decimal.Parse(QtdAferida) - decimal.Parse(Dt.Rows[i]["QUANTIDADE"].ToString())));
                o["VALORUNITARIO"] = Dt.Rows[i]["VALORUNITARIO"].ToString();
                o["TOTALITEM"] = Math.Abs(decimal.Parse(o["FALTA"].ToString()) * decimal.Parse(Dt.Rows[i]["VALORUNITARIO"].ToString()));

                o["SEQUENCIA"] = i + 1;


                if (decimal.Parse(o["QUANTIDADEAFERIDA"].ToString()) != decimal.Parse(o["QUANTIDADE"].ToString()))
                    dtConc.Rows.Add(o);
            }
            grdConsolidar.DataSource = dtConc;            
        }

        DataTable dtItens;
        private void CarregarGrid()
        {
            dtItens = Classes.BbColetor.RetornarDataTable("SELECT ID [ID], CODIGODEBARRAS [CODIGODEBARRAS], QUANTIDADE [QUANTIDADE] FROM DEVOLUCAOITEM WHERE IDDOCUMENTO=" + IdDocumento.ToString());

            DataTable dtaux = new DataTable();
            dtaux.Columns.Add("ID");
            dtaux.Columns.Add("CODIGODEBARRAS");
            dtaux.Columns.Add("QUANTIDADE");

            for (int i = 0; i < dtItens.Rows.Count; i++)
            {
                DataRow o = dtaux.NewRow();
                o["ID"] = dtItens.Rows[i]["ID"];
                o["CODIGODEBARRAS"] = dtItens.Rows[i]["CODIGODEBARRAS"];
                o["QUANTIDADE"] = dtItens.Rows[i]["QUANTIDADE"];
                dtaux.Rows.Add(o);
            }           
            
            dataGrid1.DataSource = dtaux;
        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {
            int row = dataGrid1.CurrentCell.RowNumber;
            DataRow[] rowd = Dt.Select("ItemCodigoBarras = '" + dataGrid1[row, 0].ToString() + "'");
            List<string> comados = new List<string>();
            comados.Add("DELETE FROM DEVOLUCAOITEM WHERE ID='" + dataGrid1[row, 0].ToString() + "'");
            Classes.BbColetor.excSql(comados);
            CarregarGrid();
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return && txtCbVolume.Text != "" && txtQuantidade.Text != "")
                {
                    string sql = "INSERT INTO DEVOLUCAOITEM (IDDOCUMENTO, CODIGODEBARRAS, QUANTIDADE) " ;
                    sql += " VALUES (" + IdDocumento + ", '" + txtCbVolume.Text.Trim() + "', " + txtQuantidade.Text.Replace(",", ".") + ")";

                    List<string> lsql = new List<string>();
                    lsql.Add(sql);
                    Classes.BbColetor.excSql(lsql);
                    txtCbVolume.Text = "";
                    txtQuantidade.Text = "";
                    txtCbVolume.Focus();
                    CarregarGrid();
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            pnlConfere.Visible = true;
            pnlDiferencas.Visible = false;
        }

        private void btnConfirmarEEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                new Classes.BLL.Ocorrencia().GravarDevolucao(IdDocumento.ToString(), dtConc);

                List<string> com = new List<string>();
                com.Add("DELETE FROM DEVOLUCAOITEM WHERE IDDOCUMENTO = " + IdDocumento);
                com.Add("DELETE FROM DEVOLUCAO WHERE IDDOCUMENTO = " + IdDocumento);
                Classes.BbColetor.excSql(com);
                MessageBox.Show("Operação Efetuada com Sucesso.");

                dtItens = null;
                IdDocumento = 0;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}