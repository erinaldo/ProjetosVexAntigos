using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;



namespace RsMobile.Classes
{
    public class GerenciaThread
    {
        public string cd_cliente;
        public string enviarFoto;
        public string Chave;
 
        public void ChamarThread()
        {
         
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ExecutarThread);
            System.Threading.Thread t = new System.Threading.Thread(ts);
            t.IsBackground = true;
            t.Start();
        }

        public void ChamarThread(string _cd_cliente, string _enviarFoto, string _chave)
        {
            cd_cliente = _cd_cliente;
            enviarFoto = _enviarFoto;
            Chave = _chave;
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ExecutarThread);
            System.Threading.Thread t = new System.Threading.Thread(ts);
            t.IsBackground = true;
            t.Start();
        }

            [DllImport("wininet.dll")]
            private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

            public static bool IsConnectedToInternet()
            {
                int Desc;
                bool ret = InternetGetConnectedState(out Desc, 0);
                return ret;
            }

            public static bool IsReachable(string _url)
            {
                System.Uri Url = new System.Uri(_url);

                System.Net.WebRequest webReq;
                System.Net.WebResponse resp;
                webReq = System.Net.WebRequest.Create(Url);

                try
                {
                    resp = webReq.GetResponse();
                    resp.Close();
                    webReq = null;
                    return true;
                }
                catch
                {
                    webReq = null;
                    return false;
                }
            }
     

        private void ExecutarThread()
        {
            try
            {                              
                webService.Service ws = new RsMobile.webService.Service();
                if (!ws.estaAtivo())
                    return;

                ws.GrarvarUltimoEnvioDeDados(Chave, cd_cliente, "s817i625s433t341e05c6n7o8");

                Bb bd = new Bb();
                DataTable dt = bd.RetornarPendentesSincronizacao();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ws.gravarDocumentoOcorrenciaSemImagem_wce(dt.Rows[i]["IDDOCUMENTO"].ToString(), dt.Rows[i]["IDOCORRENCIA"].ToString(), "Efetuado WCE", "6", "0", "0", dt.Rows[i]["IDDT"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm"), DateTime.Parse(dt.Rows[i]["DATADAOCORRENCIA"].ToString()).ToString("yyyy-MM-dd HH:mm"), cd_cliente, "s817i625s433t341e05c6n7o8");
                    bd.AlterarStatusDocumento(dt.Rows[i]["IDDOCUMENTO"].ToString());
                  
                }

                if (enviarFoto == "S")
                {
                    new WSS().EnviarFotos(cd_cliente);
                }

                DataTable dtSinc = bd.RetornarSincronizacao();

                if (dtSinc.Rows.Count > 0)
                {
                    ws.GrarvarTempoSincronizacao(dtSinc.Rows[0]["Chave"].ToString(), dtSinc.Rows[0]["DT"].ToString(), dtSinc.Rows[0]["DataInicial"].ToString(), dtSinc.Rows[0]["DataFinal"].ToString(), cd_cliente, "s817i625s433t341e05c6n7o8");
                    bd.AtualizarSincronizacao();
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }
        }
    }
}
 