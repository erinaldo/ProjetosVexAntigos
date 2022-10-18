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
    public partial class ConfItens : Form
    {
        public ConfItens()
        {
            InitializeComponent();
        }

        
        public string  Chave { get; set; }
        public string IdReposicaoRoge { get; set; }

        int count = 0;
        private void ConfVolume_Load(object sender, EventArgs e)
        {
            //lido = 0;

            try
            {
                CarregarGrid();
                DataTable x = Classes.BbColetor.RetornarDataTable("select max(id)+1 from ItensConferenciaCega");
                if (x.Rows.Count == 0)
                    count = 1;
                else
                    count = int.Parse((x.Rows[0][0].ToString() == "" ? "1" : x.Rows[0][0].ToString()));
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
                //if (e.KeyChar == (char)Keys.Return)
                
                if(Convert.ToInt32(e.KeyChar)==13)
                {
                    if (txtCbItem.Text.Length == 0)
                        return;

                    //Cursor.Current = Cursors.WaitCursor;
                    grdVolumes.Visible = false;
                    button2.Visible = false;
                    lblItensLidos.Visible = false;

                    string xx = txtCbItem.Text;
                    string resulFinal = "";
                    if (xx[0].ToString() == "0")
                    {
                        int PosicaoInicial = 0;

                        for (int i = 0; i < xx.Length; i++)
                        {
                            if (xx[i].ToString() != "0")
                        {
                                PosicaoInicial = i;
                                break;
                            }
                        }

                        resulFinal = xx.Substring(PosicaoInicial, xx.Length - PosicaoInicial);

                    }
                    else
                    {
                        resulFinal = txtCbItem.Text;
                    }

                    //tira os zeros da frente
                    if (txtCbItem.Text.Substring(0, 1) == "0")
                        txtCbItem.Text = txtCbItem.Text.Substring(1, txtCbItem.Text.Length - 1);


                    txtCbItem.Text = resulFinal;

                    //1- verifica se existe este codigo na conferencia
                    // 1.1 - se nao existir fazer o update marcando nao pertence a nota juntamente com o codigoDeBarras

                    string sql = "SELECT * FROM ReposicaoRogeItem RRI ";
                    sql += " INNER JOIN ReposicaoRogeCB RRCB ON  RRCB.IdReposicaoRogeItem = RRI.IdReposicaoRogeItem ";
                    sql += " WHERE RRI.IdReposicaoRoge = " + this.IdReposicaoRoge;
                    sql += " AND RRCB.CodigoDeBarras = '" + txtCbItem.Text + "'";                   

                    DataTable dtemp = Classes.BbColetor.RetornarDataTable(sql);                 
                    
                    string id = "";                    
                    count = count + 1;
                    id = (count).ToString();

                    //SE NAO EXISTE COLOCA NA TABELA DE ITENS
                    if (dtemp.Rows.Count == 0)
                    {
                        // verifica se é um produto Roge
                        string a = "Select count(*) from ReposicaoRogeEan where CodigoDeBarras = '" + txtCbItem.Text.Trim() + "'";

                        if (Classes.BbColetor.RetornarDataTable(a).Rows[0][0].ToString() == "0")
                        {
                            MessageBox.Show("Codigo de Barras não é de um produto da Roge. Favor ler outro código da Embalagem ou não é Produto Roge ", "Reposição Roge");
                            txtCbItem.Text = "";
                            txtCbItem.Focus();                            
                        }
                        else
                        {
                            sql = "INSERT INTO ItensConferenciaCega(ID, IdReposicaoRoge,CodigoRoge,IdConferenciaItem, CodigoDeBarrasLido, QuantidadeLido, PerteceANota) ";
                            sql += " values (" + id + ", " + this.IdReposicaoRoge + ",'0',0, '" + txtCbItem.Text.Trim() + "', 1 , 'NAO' )";
                            Classes.BbColetor.excSql_trans(sql);
                        }
                    }
                    else
                    {
                        //2 - se existir verificar a embalagem, se for multiplos informar ao usuario que ele deve separar a embalagem con tantas quantidades

                        if (dtemp.Rows[0]["QuantidadeEmbalagem"].ToString() != "1")
                        {
                            MessageBox.Show("Embalagem " + dtemp.Rows[0]["Embalagem"].ToString() + " com " + dtemp.Rows[0]["QuantidadeEmbalagem"].ToString() + " Unidades.", "Atenção");
                        }

                        sql = "INSERT INTO ItensConferenciaCega(ID,IdReposicaoRoge,CodigoRoge,IdConferenciaItem, CodigoDeBarrasLido, QuantidadeLido, PerteceANota) ";
                        sql += " values ("+id+", " + this.IdReposicaoRoge + ",'" + dtemp.Rows[0]["CodigoRoge"].ToString() + "'," + dtemp.Rows[0]["IdReposicaoRogeItem"].ToString() + ", '" + txtCbItem.Text.Trim() + "', " + int.Parse(dtemp.Rows[0]["QuantidadeEmbalagem"].ToString()) + ", 'SIM' )";                      
                        Classes.BbColetor.excSql_trans(sql);                          

                    }
                    //CarregarGrid();
                    txtCbItem.Text = "";
                    txtCbItem.Focus();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
            }
            finally
            {
                //Cursor.Current = Cursors.Default;
            }
        }

        private void btnConfirmarVolume_Click_1(object sender, EventArgs e)
        {
            try
            {               
                
             this.Hide();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CarregarGrid()
        {
            string s = "SELECT ID, CodigoDeBarrasLido, QuantidadeLido    FROM ItensConferenciaCega WHERE IdReposicaoRoge=" + this.IdReposicaoRoge;
            DataTable ditens = Classes.BbColetor.RetornarDataTable(s);


            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CODIGO");
            dt.Columns.Add("QUANTIDADE");


            for (int i = 0; i < ditens.Rows.Count; i++)
            {
                DataRow o = dt.NewRow();
                o[0] = ditens.Rows[i][0].ToString();
                o[1] = ditens.Rows[i][1].ToString();
                o[2] = ditens.Rows[i][2].ToString();

                dt.Rows.Add(o);
            }


            s = "SELECT * FROM ReposicaoRogeItem WHERE IdReposicaoRoge=" + this.IdReposicaoRoge ;
            DataTable dtemp = Classes.BbColetor.RetornarDataTable(s);


           // int lido = 0;
            int totalItem = 0;
            grdVolumes.DataSource = dt;
            
            totalItem = dtemp.Rows.Count;
            lblTotalDevolume.Text = "ITEM " + ditens.Rows.Count.ToString() + " de " + totalItem;

            lblItensLidos.Text = "Somatória Itens: " + ditens.Compute("sum(QuantidadeLido)", "");
        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {
            int row = grdVolumes.CurrentCell.RowNumber; 
            Classes.BbColetor.excSql_trans("delete from ItensConferenciaCega where ID=" + grdVolumes[row, 0].ToString());            
            CarregarGrid();

        }

        private void grdVolumes_Click(object sender, EventArgs e)
        {
            int row = grdVolumes.CurrentCell.RowNumber;
            grdVolumes.Select(row);

        }

        private void grdVolumes_CurrentCellChanged(object sender, EventArgs e)
        {
            int row = grdVolumes.CurrentCell.RowNumber;
            grdVolumes.Select(row);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            grdVolumes.Visible = true;
            button2.Visible = true;
            lblItensLidos.Visible = true;
            CarregarGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult x =  MessageBox.Show("Tem Certeza Que Deseja Excluir Todos os Lancamentos?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (x == DialogResult.Yes)
            {
                Classes.BbColetor.excSql_trans("delete from ItensConferenciaCega");
                CarregarGrid();
                MessageBox.Show("Operação Realizada com Sucesso.");                
            }
        }
    }
}