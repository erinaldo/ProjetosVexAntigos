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
    public partial class CLW00009 : Form
    {
        public CLW00009()
        {
            InitializeComponent();
        }

        private void CLW00004_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtChaveDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtChaveDocumento.Text != "")
                    PesquisarNF();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema em Pesquisar a Nota FIscal. " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void PesquisarNF()
        {
            lblEmissao.Text = "";
            lblNumeroDocumentp.Text = "";
            lblQuantidadeDeItens.Text = "";

            string sql = "SELECT DISTINCT D.NUMERO, D.DATADEEMISSAO,  D.IDDOCUMENTO, CADEST.CNPJCPF + '-' + CADEST.RAZAOSOCIALNOME DESTINATARIO";
            sql += " FROM DOCUMENTO D ";
            sql += " INNER JOIN CADASTRO CADEST ON CADEST.IDCADASTRO = D.IDDESTINATARIO ";
            sql += " WHERE TIPODEDOCUMENTO  ='NOTA FISCAL' AND IDFILIAl = " + VarGlobal.Usuario.UltimaFilial + " AND (DOCUMENTODOCLIENTE4='" + txtChaveDocumento.Text + "' or D.NUMERO='" + txtChaveDocumento.Text + "' )";
            DataTable dtaux=Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

            DataTable dx = new DataTable();
            dx.Columns.Add("IDDOCUMENTO");
            dx.Columns.Add("NUMERO");
            dx.Columns.Add("EMISSAO");
            dx.Columns.Add("DESTINATARIO");

            for (int i = 0; i < dtaux.Rows.Count; i++)
            {
                DataRow o = dx.NewRow();
                o[0] = dtaux.Rows[i]["IDDOCUMENTO"].ToString();
                o[1] = dtaux.Rows[i]["NUMERO"].ToString();
                o[2] = DateTime.Parse( dtaux.Rows[i]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy");
                o[3] = dtaux.Rows[i]["DESTINATARIO"].ToString();

                dx.Rows.Add(o);
            }

            dataGrid1.Visible = true;
            dataGrid1.DataSource = dx;


            //if (dx.Rows.Count == 1)
            //    dataGrid1.Select(0);

            if (dx.Rows.Count == 0)
            {
                txtChaveDocumento.SelectAll();
                throw new Exception("Nenhum Item Encontrado.");
            }

        }

        DataTable D;
        private void Pesquisar(string IdDocumento)
        {
            try
            {
                D = new Classes.BLL.Documento().retornarConferenciaByChave(txtChaveDocumento.Text, IdDocumento);
                try
                {
                    if (D.Rows.Count > 0)
                    {
                        lblEmissao.Text = DateTime.Parse( D.Rows[0]["DataDeEmissao"].ToString()).ToString("dd/MM/yyyy");
                        lblNumeroDocumentp.Text = D.Rows[0]["NUMERO"].ToString();
                        lblQuantidadeDeItens.Text = D.Rows[0]["DESTINATARIO"].ToString();
                        btnConfirmar.Enabled = true;

                        string existe = Classes.BbColetor.RetornarDataTable("SELECT COUNT(IDDOCUMENTO) FROM DEVOLUCAO WHERE IDDOCUMENTO=" + D.Rows[0]["IdDocumento"].ToString()).Rows[0][0].ToString();
                        
                        
                        if (existe == "0")
                        {
                            List<string> lcom = new List<string>();
                            for (int i = 0; i < D.Rows.Count; i++)
                            {
                                lcom.Add("INSERT INTO DEVOLUCAO VALUES (" + D.Rows[i]["IDDOCUMENTO"].ToString() + ", '" + D.Rows[i]["CHAVE"].ToString() + "', " + D.Rows[i]["IDPRODUTO"].ToString() + ", " + D.Rows[i]["IDPRODUTOEMBALAGEM"].ToString() + ", '" + D.Rows[i]["CODIGODEBARRAS"].ToString() + "', " + D.Rows[i]["QUANTIDADE"].ToString().Replace(",", ".") + ")");
                            }
                            Classes.BbColetor.excSql(lcom);
                        }

                    }
                    //else if (D.Rows.Count > 1)
                    //{

                    //    //DataView view = new DataView(table);
                    //    //DataTable distinctValues = view.ToTable(true, "Column1", "Column2" ...);

                    //    dataGrid1.DataSource = D;
                    //    lblEmissao.Text = "";
                    //    lblNumeroDocumentp.Text = "";
                    //    lblQuantidadeDeItens.Text = "";
                    //    btnConfirmar.Enabled = false;
                    //    MessageBox.Show("EXISTEM VÁRIAS NOTAS COM ESTE NÚMERO. FAVOR SELECIONAR UMA NO GRID.");
                    //    dataGrid1.Visible = true;

                    //}
                    else
                    {
                        lblEmissao.Text = "";
                        lblNumeroDocumentp.Text = "";
                        lblQuantidadeDeItens.Text = "";
                        txtChaveDocumento.Focus();
                        txtChaveDocumento.Text = "";
                        btnConfirmar.Enabled = false;
                    }
                }
                catch (Exception)
                {
                }


                if (D.Rows.Count == 0)
                    throw new Exception("Documento Não Encontrado!");
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexão de Internet.");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ConferenciaItem v = new ConferenciaItem();
            v.Dt = D;
            v.Chave = txtChaveDocumento.Text;
            v.IdDocumento = int.Parse(D.Rows[0]["IdDocumento"].ToString());
            v.Show();
        }       

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {       
            }
            catch (Exception ex)
            {

                if (ex.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED"))
                    MessageBox.Show("Verifique a Conexão de Internet.");
                else
                    MessageBox.Show(ex.Message);
            }
        }

       
        private void btnConfirmar_Click_2(object sender, EventArgs e)
        {
            ConferenciaItem v = new ConferenciaItem();
            v.Dt = D;
            v.Chave = txtChaveDocumento.Text;
            v.IdDocumento = int.Parse(D.Rows[0]["IdDocumento"].ToString());
            v.Show();
        }

        private void CLW00009_Activated(object sender, EventArgs e)
        {
            //txtChaveDocumento.Text = "";
            txtChaveDocumento.Focus();
            lblEmissao.Text = "";
            lblNumeroDocumentp.Text = "";
            lblQuantidadeDeItens.Text = "";
            btnConfirmar.Enabled = false;
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            SetarLinha();
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            SetarLinha();

        }

        private void SetarLinha()
        {
            int linha = dataGrid1.CurrentCell.RowNumber;
            dataGrid1.Select(linha);
            Pesquisar(dataGrid1[linha, 0].ToString());
            
        }
    }
}