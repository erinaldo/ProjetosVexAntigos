using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;

namespace RsMobile.Classes
{
    public class WSS
    {
        public void GrarvarTempoSincronizacao(string chave, string dt, string dataInicial, string dataFinal, string empresa)
        {
            webService.Service ws = new RsMobile.webService.Service();
            ws.GrarvarTempoSincronizacao(chave, dt, dataInicial, dataFinal, empresa, "s817i625s433t341e05c6n7o8");

        }

        public string VerificarQuantidadeDocumentosNaDT(string empresa, string nDt, string placa)
        {
            webService.Service ws = new RsMobile.webService.Service();
            return ws.RetornarQtdNotasNoDt(placa, nDt, empresa, "s817i625s433t341e05c6n7o8");

        }

        public void BaixarOcorrencias(string empresa)
        {
            webService.Service ws = new RsMobile.webService.Service();
            RsMobile.webService.Ocorrencias[] ret =ws.Listar_All_Ocorrencias(empresa, "s817i625s433t341e05c6n7o8");


            Bb odb = new Bb();
            odb.LimparOcorrencias();
            odb.GravarOcorrencias(ret);
        }
        public void BaixarDocumentos(string empresa, string placa, string nDt, string Aparelho)
        {
            Bb odb = new Bb();
            webService.Service ws = new RsMobile.webService.Service();

            if (odb.RetornaDocumentosPendentesTransmissao().Rows.Count > 0)
            {
                try
                {
                    DataTable dt = odb.RetornarPendentesSincronizacao();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (System.IO.File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\" + dt.Rows[i]["IDDOCUMENTO"].ToString() + ".jpg"))
                        {
                            string cam = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\" + dt.Rows[i]["IDDOCUMENTO"].ToString() + ".jpg";
                            FileStream f = new FileStream(cam, FileMode.Open);
                            long iLength = f.Length;
                            byte[] ms = new byte[(int)f.Length];
                            f.Read(ms, 0, (int)f.Length);
                            f.Close();
                            ws.gravarDocumentoOcorrencia_wce(dt.Rows[i]["IDDOCUMENTO"].ToString(), dt.Rows[i]["IDOCORRENCIA"].ToString(), "Efetuado WCE", "6", ms, "0", "0", dt.Rows[i]["IDDT"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm"), DateTime.Parse(dt.Rows[i]["DATADAOCORRENCIA"].ToString()).ToString("yyyy-MM-dd HH:mm"), empresa, "s817i625s433t341e05c6n7o8");
                        }
                        else
                        {
                            ws.gravarDocumentoOcorrenciaSemImagem_wce(dt.Rows[i]["IDDOCUMENTO"].ToString(), dt.Rows[i]["IDOCORRENCIA"].ToString(), "Efetuado WCE", "6", "0", "0", dt.Rows[i]["IDDT"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm"), DateTime.Parse(dt.Rows[i]["DATADAOCORRENCIA"].ToString()).ToString("yyyy-MM-dd HH:mm"), empresa, "s817i625s433t341e05c6n7o8");
                           
                        }
                        odb.AlterarStatusDocumento(dt.Rows[i]["IDDOCUMENTO"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }            
            RsMobile.webService.listar_documentos[] ret = ws.Listar_Documentos(placa, nDt, Aparelho,empresa, "s817i625s433t341e05c6n7o8");
            odb.LimparDocumentos();
            odb.GravarDocumentos(ret);
        }
        
        internal void EnviarFotos(string cd_cliente)
        {
            try
            {
                Bb odb = new Bb();
                DataTable dt = odb.RetornaFotosPendentesTransmissao();
                webService.Service ws = new RsMobile.webService.Service();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ws.gravarImagens(dt.Rows[i]["IDDOCUMENTO"].ToString(), dt.Rows[i]["IDOCORRENCIA"].ToString(), (Byte[])dt.Rows[i]["FOTO"], cd_cliente, "s817i625s433t341e05c6n7o8");
                    odb.DeletarFotoDispositivo(dt.Rows[i]["IDDOCUMENTO"].ToString());
                }
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }

        }
    }
}
