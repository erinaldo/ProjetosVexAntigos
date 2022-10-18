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

        
        private void ConfVolume_Load(object sender, EventArgs e)
        {
            //lido = 0;

            try
            {
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
                if (e.KeyChar == (char)Keys.Return && txtCbItem.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;

                    //1- verifica se existe este codigo na conferencia
                    // 1.1 - se nao existir fazer o update marcando nao pertence a nota juntamente com o codigoDeBarras

                    string sql = "SELECT * FROM ReposicaoRogeItem RRI ";
                    sql += " INNER JOIN ReposicaoRogeCB RRCB ON  RRCB.IdReposicaoRogeItem = RRI.IdReposicaoRogeItem ";
                    sql += " WHERE RRI.IdReposicaoRoge = " + this.IdReposicaoRoge;
                    sql += " AND RRCB.CodigoDeBarras = '" + txtCbItem.Text + "'";
                    DataTable dtemp = Classes.BbColetor.RetornarDataTable(sql);

                    //SE NAO EXISTE COLOCA NA TABELA DE ITENS
                    if (dtemp.Rows.Count == 0)
                    {
                        sql = "INSERT INTO ItensConferenciaCega(IdReposicaoRoge,CodigoRoge,IdConferenciaItem, CodigoDeBarrasLido, QuantidadeLido, PerteceANota) ";
                        sql += " values ("+this.IdReposicaoRoge+",'0',0, '"+txtCbItem.Text.Trim()+"', 1 , 'NAO' )";
                        Classes.BbColetor.excSql_trans(sql);
                    }
                    else
                    {
                        //2 - se existir verificar a embalagem, se for multiplos informar ao usuario que ele deve separar a embalagem con tantas quantidades

                        if (dtemp.Rows[0]["QuantidadeEmbalagem"].ToString() != "1")
                        {
                            MessageBox.Show("Embalagem " + dtemp.Rows[0]["Embalagem"].ToString() + " com " + dtemp.Rows[0]["QuantidadeEmbalagem"].ToString() + " Unidades.", "Atenção");
                        }

                        sql = "INSERT INTO ItensConferenciaCega(IdReposicaoRoge,CodigoRoge,IdConferenciaItem, CodigoDeBarrasLido, QuantidadeLido, PerteceANota) ";
                        sql += " values (" + this.IdReposicaoRoge + ",'" + dtemp.Rows[0]["CodigoRoge"].ToString() + "'," + dtemp.Rows[0]["IdReposicaoRogeItem"].ToString() + ", '" + txtCbItem.Text.Trim() + "', " + int.Parse(dtemp.Rows[0]["QuantidadeEmbalagem"].ToString()) + ", 'SIM' )";
                        Classes.BbColetor.excSql_trans(sql);                          

                    }
                    CarregarGrid();
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
                Cursor.Current = Cursors.Default;
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
    }
}