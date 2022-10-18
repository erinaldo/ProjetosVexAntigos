using System;
using System.Windows.Forms;
using RsMobile.Classes;
using System.Linq;
using PocketSignature;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Text;
using System.Data;
using RsMobile.Classes.DTO;


namespace RsMobile
{
    public partial class Inicial : Form
    {
        public Inicial()
        {
            InitializeComponent();
        }

        SignatureControl signature = new SignatureControl();

        #region INFORMAÇÕES
        private static Int32 METHOD_BUFFERED = 0;
        private static Int32 FILE_ANY_ACCESS = 0;
        private static Int32 FILE_DEVICE_HAL = 0x00000101;

        private const Int32 ERROR_NOT_SUPPORTED = 0x32;
        private const Int32 ERROR_INSUFFICIENT_BUFFER = 0x7A;

        private static Int32 IOCTL_HAL_GET_DEVICEID =
            ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14)
            | ((21) << 2) | (METHOD_BUFFERED);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool KernelIoControl(Int32 dwIoControlCode,
            IntPtr lpInBuf, Int32 nInBufSize, byte[] lpOutBuf,
            Int32 nOutBufSize, ref Int32 lpBytesReturned);



        [DllImport("sms.dll")]
        private static extern IntPtr SmsGetPhoneNumber(IntPtr psmsaAddress);

        private static string GetDeviceID()
        {
            // Initialize the output buffer to the size of a 
            // Win32 DEVICE_ID structure.
            byte[] outbuff = new byte[20];
            Int32 dwOutBytes;
            bool done = false;

            Int32 nBuffSize = outbuff.Length;

            // Set DEVICEID.dwSize to size of buffer.  Some platforms look at
            // this field rather than the nOutBufSize param of KernelIoControl
            // when determining if the buffer is large enough.
            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
            dwOutBytes = 0;

            // Loop until the device ID is retrieved or an error occurs.
            while (!done)
            {
                if (KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero,
                    0, outbuff, nBuffSize, ref dwOutBytes))
                {
                    done = true;
                }
                else
                {
                    int error = Marshal.GetLastWin32Error();
                    switch (error)
                    {
                        case ERROR_NOT_SUPPORTED:
                            throw new NotSupportedException(
                                "IOCTL_HAL_GET_DEVICEID is not supported on this device",
                                new Win32Exception(error));

                        case ERROR_INSUFFICIENT_BUFFER:

                            // The buffer is not big enough for the data.  The
                            // required size is in the first 4 bytes of the output
                            // buffer (DEVICE_ID.dwSize).
                            nBuffSize = BitConverter.ToInt32(outbuff, 0);
                            outbuff = new byte[nBuffSize];

                            // Set DEVICEID.dwSize to size of buffer.  Some
                            // platforms look at this field rather than the
                            // nOutBufSize param of KernelIoControl when
                            // determining if the buffer is large enough.
                            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
                            break;

                        default:
                            throw new Win32Exception(error, "Unexpected error");
                    }
                }
            }

            // Copy the elements of the DEVICE_ID structure.
            Int32 dwPresetIDOffset = BitConverter.ToInt32(outbuff, 0x4);
            Int32 dwPresetIDSize = BitConverter.ToInt32(outbuff, 0x8);
            Int32 dwPlatformIDOffset = BitConverter.ToInt32(outbuff, 0xc);
            Int32 dwPlatformIDSize = BitConverter.ToInt32(outbuff, 0x10);
            StringBuilder sb = new StringBuilder();

            for (int i = dwPresetIDOffset;
                i < dwPresetIDOffset + dwPresetIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }

            sb.Append("-");

            for (int i = dwPlatformIDOffset;
                i < dwPlatformIDOffset + dwPlatformIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }
            return sb.ToString();
        }

        private string RetornaNumero()
        {

            IntPtr number = Marshal.AllocHGlobal(512);
            IntPtr result = IntPtr.Zero;
            string phoneNumber = "";

            try
            {
                result = SmsGetPhoneNumber(number);
            }
            catch (Exception ex)
            {
                Marshal.FreeHGlobal(number);
                MessageBox.Show(ex.Message);
                //  Return phoneNumber
            }


            if (int.Parse(result.ToString()) != 0)
            {
                MessageBox.Show("Out of luck");
                Marshal.FreeHGlobal(number);
                return phoneNumber;

            }

            // phoneNumber = Marshal.PtrToStringUni(IntPtr.op_Explicit(System.Runtime.InteropServices.Marshal.SizeOf(typeof(Int32)) + number.ToInt32));

            return phoneNumber;


        }


        #endregion


        //signature.Location = areaSignature.Location;
        //signature.Size = areaSignature.Size;
        //signature.Background = "\\Program Files\\SignCaptureV2\\sign here.png";
        //this.Controls.Add(signature);
        private void btnConfimar_Click(object sender, EventArgs e)
        {
            ListarStatus f = new ListarStatus(txtEmpresa.Text, GetDeviceID());

            if (txtDt.Text == "" || txtEmpresa.Text == "" || txtPlaca.Text == "")
            {
                MessageBox.Show("Informe a Empresa, Número do DT e a Placa");                
                return;
            }

            new Bb().AterarDadosTelaInicial(txtEmpresa.Text.Trim(), txtPlaca.Text.Trim(), txtDt.Text.Trim());
            InformacoesAparelho();
            f.Show();
        }



        private void menuItem2_Click(object sender, EventArgs e)
        {
            DateTime dataIni = DateTime.Now;            
            lblMenssagem.Text = "Tentando Conectar a Internet";           

            this.Refresh();


            if (txtDt.Text == "" || txtEmpresa.Text == "" || txtPlaca.Text == "")
            {
                MessageBox.Show("Digite os dados solicitados", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                txtPlaca.Focus();
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string qdtDocumentos = new WSS().VerificarQuantidadeDocumentosNaDT(txtEmpresa.Text, txtDt.Text, txtPlaca.Text);

                if (int.Parse(qdtDocumentos) > 0)
                {
                    lblMenssagem.Text = "Quantidade de Notas: " + qdtDocumentos;
                    lblMenssagem.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMenssagem.Text = "DT/RE Não Disponível";
                    lblMenssagem.ForeColor = System.Drawing.Color.Red;
                }

                this.Refresh();

                new WSS().BaixarDocumentos(txtEmpresa.Text, txtPlaca.Text, txtDt.Text, GetDeviceID());
                InformacoesAparelho();

                if (int.Parse(qdtDocumentos) > 0)
                {
                    ListarStatus f = new ListarStatus(txtEmpresa.Text, GetDeviceID());
                    new Bb().AterarDadosTelaInicial(txtEmpresa.Text.Trim(), txtPlaca.Text.Trim(), txtDt.Text.Trim());
                    InformacoesAparelho();
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique a conexão com a internet - e: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                lblMenssagem.Text = "Internet não está disponível";
                lblMenssagem.ForeColor = System.Drawing.Color.Red;

            }
            finally
            {
                Sincronizacao sinc = new Sincronizacao();
                sinc.DT = txtDt.Text;
                sinc.Chave = GetDeviceID();
                sinc.DataInicial = dataIni.ToString();
                sinc.DataFinal = DateTime.Now.ToString();
                sinc.Enviado="N";

                new Bb().GravarSincronizacao(sinc);
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                lblMenssagem.Text = "Sincronizando Ocorrências";
                lblMenssagem.ForeColor = System.Drawing.Color.Red;
                Cursor.Current = Cursors.WaitCursor;
                new WSS().BaixarOcorrencias(txtEmpresa.Text);

                lblMenssagem.Text = "Ocorrências Sincronizadas";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique a conexão com a internet - e: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                lblMenssagem.Text = "Internet não está disponível";                
                lblMenssagem.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Verifique se você está conectado em uma rede wi-fi. Caso contrário ocorrerá custos excedentes de transmissão de dados", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (ret == DialogResult.No)
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                new WSS().EnviarFotos(txtEmpresa.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifique a conexão com a internet - e: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            new WSS().BaixarOcorrencias(txtEmpresa.Text);
            new WSS().BaixarDocumentos(txtEmpresa.Text, txtPlaca.Text, txtDt.Text, GetDeviceID());
            Cursor.Current = Cursors.Default;


        }


        private void butNew_Click_1(object sender, EventArgs e)
        {
            signature.Clear();
            this.Refresh();
        }

        private void Save_Click_1(object sender, EventArgs e)
        {
            signature.StoreSigData("SignFile.txt");
        }

        private void Load_Click_1(object sender, EventArgs e)
        {
            int baseX = 10;
            int baseY = 100;
            string signatureFile = "SignFile.txt";
            load_signature(baseX, baseY, signatureFile);
        }

        void load_signature(int baseX, int baseY, string signatureFile)
        {
            System.IO.StreamReader streamReader = new System.IO.StreamReader("SignFile.txt");
            string pointString = null;

            while ((pointString = streamReader.ReadLine()) != null)
            {
                if (pointString.Trim().Length > 0)
                {
                    String[] points = new String[4];
                    points = pointString.Split(new Char[] { ' ' });
                    Pen pen = new Pen(Color.Black);
                    this.CreateGraphics().DrawLine(pen, (baseX + int.Parse(points[0].ToString())), (baseY + int.Parse(points[1].ToString())), (baseX + int.Parse(points[2].ToString())), (baseY + int.Parse(points[3].ToString())));
                }
            }
            streamReader.Close();
        }

        private void Inicial_Activated(object sender, EventArgs e)
        { 
            
            lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();     
        }

        private void Inicial_Load(object sender, EventArgs e)
        {
            //InformacoesAparelho();
            lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();     


            DataTable dt = new Bb().RetornarDadosTelaInicial();
            if (dt.Rows.Count == 0)
            {
                txtDt.Text = "";
                txtEmpresa.Text = "";
                txtPlaca.Text = "";
            }
            else
            {
                txtDt.Text = dt.Rows[0]["DT"].ToString();
                txtEmpresa.Text = dt.Rows[0]["Empresa"].ToString();
                txtPlaca.Text = dt.Rows[0]["Placa"].ToString();
            }
        }

        private void InformacoesAparelho()
        {
            if (txtDt.Text == "" || txtEmpresa.Text == "" || txtPlaca.Text == "")
            {
                return;
            }

            ThreadApar t = new ThreadApar();
            t.ChamarThread(txtEmpresa.Text, GetDeviceID());      

            //try
            //{
            //    webService.Service w = new RsMobile.webService.Service();
            //    string strDeviceID = GetDeviceID();
            //    string deviceName = System.Net.Dns.GetHostName();
            //    Bb b = new Bb();

            //    //retona o aparelho do webservice
            //    RsMobile.webService.aparelho[] apar = w.Verificar_Aparelho(strDeviceID, "s817i625s433t341e05c6n7o8", txtEmpresa.Text);
            //    if (apar.Length > 0)
            //    {
            //        b.LimparTabellaAparelho();
            //        Aparelho ap = new Aparelho();
            //        ap.EnviaFoto = apar[0].EnviaFoto;
            //        ap.Chave = strDeviceID;
            //        ap.Nome = apar[0].Nome;
            //        ap.NumeroFone = apar[0].NumeroFone;
            //        ap.Tempo = apar[0].Tempo;
            //        ap.EnviaPosicaozerada = apar[0].EnviaPosicaoZerada;
            //        b.GravarinformacoesAparelho(ap);
            //    }
            //    else
            //    {
            //        Aparelho ap = new Aparelho();
            //        ap.EnviaFoto = "N";
            //        ap.Chave = strDeviceID;
            //        ap.Nome = deviceName;
            //        ap.NumeroFone = deviceName;
            //        ap.Tempo = "5";
            //        ap.EnviaPosicaozerada = "S";
            //        b.GravarinformacoesAparelho(ap);
            //        w.GravarAparelho(ap.Chave, ap.Nome, ap.Tempo, ap.EnviaPosicaozerada, ap.NumeroFone, ap.EnviaFoto, txtEmpresa.Text, "s817i625s433t341e05c6n7o8");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message.ToString());
            //}
        }
    }
}