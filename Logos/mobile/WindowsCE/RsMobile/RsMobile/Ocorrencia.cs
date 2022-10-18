using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsMobile.Forms;

using System.IO;
using System.Reflection;
using Microsoft.WindowsMobile.Status;


namespace RsMobile
{
    public partial class Ocorrencia : Form
    {
        string _idDocumento;
        string _cd_cliente;
        string _idOcorrenciaSelecionada;
        string _chave;
        public Ocorrencia()
        {
            InitializeComponent();
        }

        public Ocorrencia(string IdDocumento, string cd_cliente, string chave)
        {
            _idDocumento = IdDocumento;
            _cd_cliente = cd_cliente;
            _chave = chave;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Ocorrencia_Load(object sender, EventArgs e)
        {
            DataTable DT = new Bb().RetornarOcorrencias();

            DataTable dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("CODIGO", typeof(string)));
            dtb.Columns.Add(new DataColumn("NOME", typeof(string)));
            dtb.Columns.Add(new DataColumn("IDOCORRENCIA", typeof(string)));

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow o = dtb.NewRow();
                o[0] = DT.Rows[i]["CODIGO"].ToString();
                o[1] = DT.Rows[i]["NOME"].ToString();
                o[2] = DT.Rows[i]["IDOCORRENCIA"].ToString();
                dtb.Rows.Add(o);
            }

            grd.DataSource = dtb;
        }

        private void btnVoltarListaStatus_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (lblOcorrencia.Text != "")
            {
                new Bb().SetarOcorrencia("0", "0", _idOcorrenciaSelecionada, _idDocumento);
                ListarStatus l = new ListarStatus(_cd_cliente, _chave);
                DialogResult ret;

                if (!SystemState.CameraPresent)
                    ret = DialogResult.No;
                else
                    ret = MessageBox.Show("Deseja Tirar Foto?", "RSMobile", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                switch (ret)
                {
                    case DialogResult.No:
                        l.Show();
                        this.Hide();
                        break;

                    case DialogResult.Yes:
                        if (SystemState.CameraPresent)
                            chamarCamera();
                        l.Show();
                        this.Hide();
                        break;
                }
            }
        }

        private void grd_Click(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            lblOcorrencia.Text = grd[row, 1].ToString();
            _idOcorrenciaSelecionada = grd[row, 2].ToString();
        }

        private void grd_DoubleClick(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            lblOcorrencia.Text = grd[row, 1].ToString();
            _idOcorrenciaSelecionada = grd[row, 2].ToString();
            new Bb().SetarOcorrencia("0", "0", _idOcorrenciaSelecionada, _idDocumento);
            ListarStatus l = new ListarStatus(_cd_cliente, _chave);
            DialogResult ret;

            if (!SystemState.CameraPresent)
                ret = DialogResult.No;
            else
                ret = MessageBox.Show("Deseja Tirar Foto?", "RSMobile", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            switch (ret)
            {
                case DialogResult.No:
                    l.Show();
                    this.Hide();
                    break;

                case DialogResult.Yes:
                    if (SystemState.CameraPresent)
                        chamarCamera();
                    l.Show();
                    this.Hide();
                    break;
            }
        }

        private void chamarCamera()
        {
            CameraCaptureDialog cameraCapture = new CameraCaptureDialog();
            cameraCapture.Owner = this;
            cameraCapture.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            cameraCapture.Title = "Tire uma foto";
            cameraCapture.VideoTypes = CameraCaptureVideoTypes.Messaging;
            cameraCapture.Resolution = new Size(176, 144);
            cameraCapture.StillQuality = CameraCaptureStillQuality.Low;
            cameraCapture.VideoTimeLimit = new TimeSpan(0, 0, 60);
            cameraCapture.Mode = CameraCaptureMode.Still;
            cameraCapture.DefaultFileName = _idDocumento + ".jpg";

            if (cameraCapture.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\" + _idDocumento + ".jpg";
                    if (File.Exists(caminho))
                    {
                        Bb bd = new Bb();
                        FileStream f = new FileStream(caminho, FileMode.Open);
                        long iLength = f.Length;
                        byte[] ms = new byte[(int)f.Length];
                        f.Read(ms, 0, (int)f.Length);
                        f.Close();
                        bd.GravarImagemBD(_idDocumento, _idOcorrenciaSelecionada, ms);
                        File.Delete(caminho);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void grd_Click_1(object sender, EventArgs e)
        {
            int row = grd.CurrentCell.RowNumber;
            lblOcorrencia.Text = grd[row, 1].ToString();
            _idOcorrenciaSelecionada = grd[row, 2].ToString();
        }
    }
}
