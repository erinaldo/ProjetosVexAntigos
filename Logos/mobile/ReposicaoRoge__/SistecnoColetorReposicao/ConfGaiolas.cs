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
    public partial class ConfGaiolas : Form
    {
        public ConfGaiolas()
        {
            InitializeComponent();
        }

        private void btnNovaGaiola_Click(object sender, EventArgs e)
        {
            //pnlVolume.Visible = true;
            //btnNovaGaiola.Enabled = false;
            //btnFecharGaiola.Enabled = true;
            txtVolume.Focus();
            //lblFilial.Text = "";
            //lblIdGailola.Text = "";

        }
        ReposicaoInterno.ReposicaoRogeInterno ws;

        private void txtVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtVolume.Text = txtVolume.Text.Trim();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtVolume.Text != "")
                {

                    //if (lblIdGailola.Text == "")
                    //{
                        ReposicaoInterno.ReposicaoRogeInterno ws;
                        string iggal = "";

                        try
                        {
                            ws = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();
                            iggal = ws.CriarGaiola(txtVolume.Text, VarGlobal.Usuario.IDUsuario.ToString());

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Verifique a Conexão com a Internet.");
                        }

                        if (iggal.Contains("Err^"))
                        {
                            throw new Exception(iggal.Replace("Err^", ""));
                        }

                        MessageBox.Show("Bandeira criada com sucesso.", "Criação de Bandeira de Gaiolas.");
                        txtVolume.Text = "";
                        txtVolume.Focus();


                        //lblFilial.Text = txtVolume.Text.Substring(26, 2);
                        //lblIdGailola.Text = iggal;

                        //pnlConfirmarBandeira.Visible = true;
                        //pnlVolume.Visible = false;
                        //txtBandeira.Focus();
                    //}
                    //else
                    //{
                    //    string filial = txtVolume.Text.Substring(26, 2);
                    //    string Pertence = "NAO";
                    //    if (filial == lblFilial.Text)
                    //        Pertence = "SIM";
                    //    else
                    //    {
                    //        Pertence = "NAO";
                    //        MessageBox.Show("O Volume " + txtVolume.Text + " pertence a Filial: " + txtVolume.Text.Substring(26, 2), "Atenção");
                    //    }

                    //    Classes.BbColetor.excSql_trans("INSERT INTO ConfGaiola(IdGailoa, CodigoDeBarras, PertenceFilial) values(" + int.Parse(txtBandeira.Text.Substring(4, 10)) + ", '" + txtVolume.Text + "', '" + Pertence + "')");
                    //    CarregarGrid();

                    //    txtVolume.Text = "";
                    //    txtVolume.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtVolume.Text = "";
                txtVolume.Focus();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ConfGaiolas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "CONF. GAIOLAS - " + this.Name;
                statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
                if (tabControl1.SelectedIndex == 0)
                    txtVolume.Focus();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBandeira_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtBandeira_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtVolume.Text = txtVolume.Text.Trim();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtBandeiraConferencia.Text.Trim() != "")
                {
                    
                    ws = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();
                    string ret = ws.ChecarBandeira(txtBandeiraConferencia.Text);

                    if (ret.Contains("Err^"))
                    {
                        throw new Exception(ret.Replace("Err^", ""));
                    }

                    MessageBox.Show("Bandeira Válida");

                    //Classes.BbColetor.excSql_trans("INSERT INTO ConfGaiola(IdGailoa, CodigoDeBarras, PertenceFilial) values(" + int.Parse(txtBandeiraConferencia.Text.Substring(4, 10)) + ", '" + txtVolume.Text + "', 'SIM')");
                  //  CarregarGrid();    

                    pnlBandeira.Enabled = false;
                    pnlConItens.Enabled = true;
                    txtVolumeConferencia.Text = "";
                    txtVolumeConferencia.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtBandeiraConferencia.SelectAll();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        //DataTable dtGrid = null;
        //private void CarregarGrid()
        //{
        //    dtGrid = new DataTable();
        //    dtGrid.Columns.Add("ID");
        //    dtGrid.Columns.Add("CODIGODEBARRAS");

        //    DataTable t = Classes.BbColetor.RetornarDataTable("Select ID, CODIGODEBARRAS  from ConfGaiola WHERE IDGAILOA=" + lblIdGailola.Text);
        //    for (int i = 0; i < t.Rows.Count; i++)
        //    {
        //        DataRow o = dtGrid.NewRow();
        //        o[0] = t.Rows[i][0];
        //        o[1] = t.Rows[i][1];

        //        dtGrid.Rows.Add(o);

        //    }

        //    dataGrid1.DataSource = dtGrid;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
           // pnlVolume.Visible = false;
            //pnlConfirmarBandeira.Visible = false;
            //btnEnviarGaiola.Enabled = false;
            //btnFecharGaiola.Enabled = false;
            //btnNovaGaiola.Enabled = true;
            //btnNovaGaiola.Focus();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    LimparCampos();
                    txtVolume.Focus();                    
                    break;

                case 1:
                    txtBandeiraConferencia.Focus();
                    break;

                case 2:
                    caregarGrid();
                    break;
            }
        }

        private void LimparCampos()
        {
            txtVolume.Text = "";
            pnlQuantidadeVolumes.Visible = false;
            pnlConItens.Enabled = false;
            pnlBandeira.Enabled = true;

            txtBandeiraConferencia.Text = "";
            txtVolumeConferencia.Text = "";
            dataGrid1.DataSource = null;

            txtVolume.Focus();
        }

        private void caregarGrid()
        {

            if(txtBandeiraConferencia.Text=="")
                return;


           DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("ID");
            dtGrid.Columns.Add("CODIGODEBARRAS");

            DataTable t = Classes.BbColetor.RetornarDataTable("Select ID, CODIGODEBARRAS  from ConfGaiola WHERE IDGAILOA=" + int.Parse(txtBandeiraConferencia.Text.Substring(4, 10)));
            for (int i = 0; i < t.Rows.Count; i++)
            {
                DataRow o = dtGrid.NewRow();
                o[0] = t.Rows[i][0];
                o[1] = t.Rows[i][1];

                dtGrid.Rows.Add(o);

            }

            dataGrid1.DataSource = dtGrid;
        }

        private void txtVolumeConferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtVolumeConferencia.Text.Trim() != "")
                {
                    string filial = txtVolumeConferencia.Text.Substring(26, 2);
                    string Pertence = "NAO";
                    
                    if (int.Parse(filial) == int.Parse( txtBandeiraConferencia.Text.Substring(0,4)))
                        Pertence = "SIM";
                    else
                    {
                        Pertence = "NAO";
                        MessageBox.Show("O Volume " + txtVolumeConferencia.Text + " pertence a Filial: " + txtVolumeConferencia.Text.Substring(26, 2), "Atenção");
                    }

                    string sql = "SELECT COUNT(*) FROM ConfGaiola WHERE CodigoDeBarras='" + txtVolumeConferencia.Text + "' AND IdGailoa=" + int.Parse(txtBandeiraConferencia.Text.Substring(4, 10));
                    
                    if (Classes.BbColetor.RetornarDataTable(sql).Rows[0][0].ToString() != "0")
                    {
                        txtVolumeConferencia.Text = "";
                        throw new Exception("Codigo de Barras ja foi lido.");
                    }                


                    Classes.BbColetor.excSql_trans("INSERT INTO ConfGaiola(IdGailoa, CodigoDeBarras, PertenceFilial) values(" + int.Parse(txtBandeiraConferencia.Text.Substring(4, 10)) + ", '" + txtVolumeConferencia.Text + "', '" + Pertence + "')");

                    txtVolumeConferencia.Text = "";
                    txtVolumeConferencia.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtVolumeConferencia.SelectAll();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pnlQuantidadeVolumes.Visible = true;
            txtQuantidadeDeVolumes.Focus();
            txtQuantidadeDeVolumes.SelectAll();
        }

        private void txtQuantidadeDeVolumes_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.KeyChar == (char)Keys.Return && txtQuantidadeDeVolumes.Text.Trim() != "")
                {
                    DataTable t = Classes.BbColetor.RetornarDataTable("Select *  from ConfGaiola WHERE IDGAILOA=" + int.Parse(txtBandeiraConferencia.Text.Substring(4, 10)));

                    int qtdvolumesLidos = (int)t.Compute("COUNT(IDGAILOA)", "PERTENCEFILIAL='SIM'");

                    if (qtdvolumesLidos == int.Parse(txtQuantidadeDeVolumes.Text))
                    {
                        //enviar
                        ws = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();
                        List<ReposicaoInterno.vol> itens = new List<ReposicaoInterno.vol>();

                        for (int i = 0; i < t.Rows.Count; i++)
                        {
                            ReposicaoInterno.vol item = new SistecnoColetor.ReposicaoInterno.vol();
                            item.CB = t.Rows[i]["CodigoDeBarras"].ToString();
                            item.Pertence = t.Rows[i]["PertenceFilial"].ToString();
                            itens.Add(item);
                        }
                        string ret = ws.ReceberVolumes(itens.ToArray(), txtBandeiraConferencia.Text.Substring(4, 10), VarGlobal.Usuario.IDUsuario.ToString());

                        if (ret == "OK")
                        {
                            Apagar();
                            LimparCampos();
                            tabControl1.SelectedIndex = 0;
                        }
                        else
                            throw new Exception(ret);
                    }
                    else
                    {
                        MessageBox.Show("Quantidade de volumes contados, não é igual a quantidade de volumes lidos.", "ATENÇÃO");
                        pnlQuantidadeVolumes.Visible = false;
                        txtQuantidadeDeVolumes.Text = "0";
                        txtVolumeConferencia.Focus();
                    }
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

        private void Apagar()
        {
            try
            {                
                string sql = "DELETE FROM ConfGaiola WHERE IdGailoa = " + txtBandeiraConferencia.Text.Substring(4, 10);
                List<string> S =  new List<string>();
                S.Add(sql);
                Classes.BbColetor.excSql(S);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}