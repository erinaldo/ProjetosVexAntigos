using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public partial class ConfVeiculos : Form
    {
        public ConfVeiculos()
        {
            InitializeComponent();
        }

        SqlCeConnection cnnGeral = new SqlCeConnection(BbColetor.RetornarConexao());

        private void txtVolume_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBandeira_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBandeira_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtVolumeConferencia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVolumeConferencia_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        string[] dadosEtiquetaVeiculo;
        string dadosDaFilialBandVol = "";
        List<string> filiaisLidas;
        string idDt = "";
        private void txtBandeiraConferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtVolume.Text = txtVolume.Text.Trim();
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return /*&& txtBandeiraConferencia.Text.Trim().Length == 15*/)
                {
                    //1013164-ABK-0560-19-8-

                    dadosEtiquetaVeiculo = txtCodigoCarreta.Text.Split('-');
                    filiaisLidas = new List<string>();
                    idDt = dadosEtiquetaVeiculo[0];
                    for (int i = 3; i < dadosEtiquetaVeiculo.Length; i++)
                    {
                        if (dadosEtiquetaVeiculo[i] != "")
                            filiaisLidas.Add(dadosEtiquetaVeiculo[i]);
                    }
                    pnlConItens.Enabled = true;
                    txtVolumeConferencia.Focus();                 
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtCodigoCarreta.SelectAll();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtVolumeConferencia_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtVolumeConferencia_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.KeyChar == (char)Keys.Return /*&& txtVolumeConferencia.Text.Trim().Length == 15*/)
                {
                    dadosDaFilialBandVol = int.Parse(txtVolumeConferencia.Text.Trim().Substring(0, 4)).ToString();
                    var confere = filiaisLidas.Where(x => x == dadosDaFilialBandVol).Count();

                    if (confere == 0)
                        MessageBox.Show("A Gaiola " + txtVolumeConferencia.Text + ", não pertence a esta carreta.");
                    else
                    {
                        if (cnnGeral.State == ConnectionState.Closed)
                        {
                            cnnGeral = new SqlCeConnection(BbColetor.RetornarConexao());
                            cnnGeral.Open();
                        }

                        Classes.BbColetor.Excuta("INSERT INTO ConfCarreta(IdDt, IdGaiola, Enviado, Fechado) values(" + idDt + ", '" + txtVolumeConferencia.Text.Trim().Substring(4, 10) + "', 0, 0)", cnnGeral);

                    }

                    txtCodigoCarreta.Text = "";
                    txtVolumeConferencia.Text = "";
                    pnlConItens.Enabled = false;
                    txtCodigoCarreta.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtCodigoCarreta.SelectAll();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ConfVeiculos_Load(object sender, EventArgs e)
        {
            txtCodigoCarreta.Focus();
            cnnGeral = new SqlCeConnection(BbColetor.RetornarConexao());

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cnnGeral.State == ConnectionState.Closed)
            {
                cnnGeral = new SqlCeConnection(BbColetor.RetornarConexao());
                cnnGeral.Open();
            }


            DataTable dt2 = Classes.BbColetor.RetornarDataTable("Select IdGaiola, IdDt  from ConfCarreta");
            dataGrid1.DataSource = dt2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnnGeral.State == ConnectionState.Closed)
                {
                    cnnGeral = new SqlCeConnection(BbColetor.RetornarConexao());
                    cnnGeral.Open();
                }


                txtCodigoCarreta.Text = "";
                txtVolumeConferencia.Text = "";
                txtCodigoCarreta.Focus();
                Classes.BbColetor.Excuta("Update ConfCarreta set Fechado=1" , cnnGeral);
                MessageBox.Show("Conferência Finalizada com Sucesso. Aguarde o envio das informações.");

                //mandar as informações
                try
                {
                    DataTable dt = Classes.BbColetor.RetornarDataTable("Select Distinct IdDt from ConfCarreta where Enviado=0 and Fechado=1");
                    SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno ws = new SistecnoColetor.ReposicaoInterno.ReposicaoRogeInterno();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dt2 = Classes.BbColetor.RetornarDataTable("Select *  from ConfCarreta where IdDt=" + dt.Rows[i]["IdDt"].ToString());
                       

                        List<int> gaiolas = new List<int>();
                        for (int i2 = 0; i2 < dt2.Rows.Count; i2++)
                        {
                            gaiolas.Add(int.Parse(dt2.Rows[i2]["IdGaiola"].ToString()));
                        }

                        string ret = ws.ReceberConferenciaEmbarqueGaiola(gaiolas.ToArray(), int.Parse(dt.Rows[i]["IdDt"].ToString()), VarGlobal.Usuario.IDUsuario);

                        if (ret == "OK")
                            Classes.BbColetor.Excuta("delete ConfCarreta where IdDt=" + dt.Rows[i]["IdDt"].ToString(), cnnGeral);
                    }

                    MessageBox.Show("Conferência enviada com sucesso!");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cnnGeral.State == ConnectionState.Open)
                {                    
                    cnnGeral.Close();
                }
            }

        }
    }
}
