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
    public partial class ConfVolume : Form
    {
        public ConfVolume()
        {
            InitializeComponent();
        }

        
        public string Chave { get; set; }
        public string IdReposicaoRoge { get; set; }

        private void ConfVolume_Load(object sender, EventArgs e)
        {
            lido = 0;

            try
            {
                this.Text = "Reposição Volumes - " + this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                CarregarGrid();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        int lido = 0;
        int totalItem = 0;
        private void txtCbVolume_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtCbVolume.Text != "")
                {
                    DataTable dx = Classes.BbColetor.RetornarDataTable("select IdReposicaoRogeVolume from ReposicaoRogeVolume where CodIgoDeBarras='" + txtCbVolume.Text + "' and IdReposicaoRoge=" + IdReposicaoRoge + " AND CONFERIDO='NAO'");

                    if (dx.Rows.Count > 0)
                    {
                        List<string> ls = new List<string>();
                        ls.Add("UPDATE ReposicaoRogeVolume SET CONFERIDO='SIM' WHERE IDReposicaoRogeVolume=" + dx.Rows[0][0].ToString());
                        Classes.BbColetor.excSql_trans(ls);

                        txtCbVolume.Text = "";
                        txtCbVolume.Focus();
                        CarregarGrid();
                    }
                    else
                    {
                        txtCbVolume.Text = "";
                        txtCbVolume.Focus();
                        throw new Exception("VOLUME JÁ LIDO OU  NAO PERTENCE A ESTA NOTA.");
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
                this.Hide();         
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        private void CarregarGrid()
        {

          DataTable  dt = Classes.BbColetor.RetornarVolumesConferidosPorChave(this.Chave);         
            grdVolumes.DataSource = dt;
            totalItem = int.Parse(Classes.BbColetor.RetornarDataTable("SELECT COUNT(*) FROM ReposicaoRogeVolume WHERE IdReposicaoRoge IN (select IdReposicaoRoge from  ReposicaoRoge where Chave = '" + this.Chave + "')").Rows[0][0].ToString());
            lido = dt.Rows.Count;
            lblTotalDevolume.Text = "Volume " + lido.ToString() + " de " + totalItem;


            if (lido == totalItem)
            {
                txtCbVolume.Enabled = false;
                MessageBox.Show("Leitura de Volumes completa.");
            }
            
        }

        private void menuItem1_Click_1(object sender, EventArgs e)
        {            
            int row = grdVolumes.CurrentCell.RowNumber;
            string s = "update ReposicaoRogeVolume set Conferido='NAO' WHERE IDReposicaoRogeVolume=" + grdVolumes[row, 0].ToString();
            Classes.BbColetor.excSql_trans(s);
            CarregarGrid();            
        }

        private void grdVolumes_CurrentCellChanged(object sender, EventArgs e)
        {
            int row = grdVolumes.CurrentCell.RowNumber;
            grdVolumes.Select(row);
        }

        private void grdVolumes_Click(object sender, EventArgs e)
        {
            int row = grdVolumes.CurrentCell.RowNumber;
            grdVolumes.Select(row);
        }
    }
}