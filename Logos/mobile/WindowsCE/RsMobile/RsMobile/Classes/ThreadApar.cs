using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using RsMobile.Classes.DTO;



namespace RsMobile.Classes
{
    public class ThreadApar
    {
        public string cd_cliente;
        public string GetDeviceID;

        public void ChamarThread()
        {

            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ExecutarThread);
            System.Threading.Thread t = new System.Threading.Thread(ts);
            t.IsBackground = true;
            t.Start();
        }

        public void ChamarThread(string _cd_cliente, string _GetDeviceID)
        {
            cd_cliente = _cd_cliente;
            GetDeviceID = _GetDeviceID;
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ExecutarThread);
            System.Threading.Thread t = new System.Threading.Thread(ts);
            t.IsBackground = true;
            t.Start();
        }

    
        private void ExecutarThread()
        {
            try
            {
                webService.Service w = new RsMobile.webService.Service();
                string strDeviceID = GetDeviceID;
                string deviceName = System.Net.Dns.GetHostName();
                Bb b = new Bb();

                //retona o aparelho do webservice
                RsMobile.webService.aparelho[] apar = w.Verificar_Aparelho(strDeviceID, "s817i625s433t341e05c6n7o8", cd_cliente);
                if (apar.Length > 0)
                {
                    b.LimparTabellaAparelho();
                    Aparelho ap = new Aparelho();
                    ap.EnviaFoto = apar[0].EnviaFoto;
                    ap.Chave = strDeviceID;
                    ap.Nome = apar[0].Nome;
                    ap.NumeroFone = apar[0].NumeroFone;
                    ap.Tempo = apar[0].Tempo;
                    ap.EnviaPosicaozerada = apar[0].EnviaPosicaoZerada;
                    b.GravarinformacoesAparelho(ap);
                }
                else
                {
                    Aparelho ap = new Aparelho();
                    ap.EnviaFoto = "N";
                    ap.Chave = strDeviceID;
                    ap.Nome = deviceName;
                    ap.NumeroFone = deviceName;
                    ap.Tempo = "5";
                    ap.EnviaPosicaozerada = "S";
                    b.GravarinformacoesAparelho(ap);
                    w.GravarAparelho(ap.Chave, ap.Nome, ap.Tempo, ap.EnviaPosicaozerada, ap.NumeroFone, ap.EnviaFoto,cd_cliente, "s817i625s433t341e05c6n7o8");

                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
 