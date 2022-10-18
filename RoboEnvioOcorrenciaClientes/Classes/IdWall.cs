using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sistecno.Facility.Dbo.Domain.Comum
{
    public class IdWall
    {
        public static string Consulta(string chave)
        {
            string ret = "";
            try
            {
                string url = "https://api-v2.idwall.co/relatorios/" + chave + "/consultas";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "6a01a8aa-a036-4de6-8810-5db29fc6df5f");

                    HttpResponseMessage res = client.GetAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        ret = res.Content.ReadAsStringAsync().Result;
                    }
                }
                return ret;
            }
            catch (Exception)
            {
                return ret;
            }
        }

        public static string Parametros(string chave)
        {
            string ret = "";
            try
            {
                string url = "https://api-v2.idwall.co/relatorios/" + chave + "/parametros";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "6a01a8aa-a036-4de6-8810-5db29fc6df5f");

                    HttpResponseMessage res = client.GetAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        ret = res.Content.ReadAsStringAsync().Result;
                    }
                }
                return ret;
            }
            catch (Exception)
            {
                return ret;
            }
        }


        public static string GetImage(string url)
        {
            try
            {

                url = "https://" + url;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
                    client.DefaultRequestHeaders.Add("Authorization", "6a01a8aa-a036-4de6-8810-5db29fc6df5f");

                    HttpResponseMessage res = client.GetAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var ret = res.Content.ReadAsByteArrayAsync().Result;
                        if (ret == null || ret.ToString() == "")
                            return "";

                        return Convert.ToBase64String(ret);

                    }
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }

        public static byte[] GetImageBytes(string url)
        {
            try
            {

                url = "https://" + url;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
                    client.DefaultRequestHeaders.Add("Authorization", "6a01a8aa-a036-4de6-8810-5db29fc6df5f");

                    HttpResponseMessage res = client.GetAsync(url).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var ret = res.Content.ReadAsByteArrayAsync().Result;
                        return ret;
                       

                    }
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }



    }

    public class IdWallConsultas
    {
        public class Tentativa
        {
            public double? duracao_tentativa { get; set; }
            public DateTime? hora_fim_tentativa { get; set; }
            public DateTime? hora_inicio_tentativa { get; set; }
            public string msg_erro_tentativa { get; set; }
            public string nome_fonte { get; set; }
            public string status_fonte { get; set; }
            public string status_tentativa { get; set; }
            public string tipo_erro_tentativa { get; set; }
        }

        public class Consulta
        {
            public string nome { get; set; }
            public string idConsulta { get; set; }
            public string status_fonte { get; set; }
            public List<Tentativa> tentativas { get; set; }
        }

        public class Result
        {
            public string nome_matriz { get; set; }
            public string status_protocolo { get; set; }
            public List<Consulta> consultas { get; set; }
        }

        public class Root
        {
            public Result result { get; set; }
            public int status_code { get; set; }
        }
    }


    public class IdWallParametros
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class ImagensFacematch
        {
            public string url { get; set; }
            public bool principal { get; set; }
        }

        public class ParametrosAuxiliare
        {
            public string tipo { get; set; }
            public string fonte { get; set; }
            public string valor { get; set; }
        }

        public class RecursosMidia
        {
            public string tag { get; set; }
            public string url { get; set; }
            public string type { get; set; }
        }

        public class Parametros
        {
            public string cnh { get; set; }
            public string cnh_categoria { get; set; }
            public string cnh_data_da_primeira_habilitacao { get; set; }
            public string cnh_data_de_validade { get; set; }
            public string cnh_estado_expedicao { get; set; }
            public string cnh_numero_espelho { get; set; }
            public string cnh_numero_renach { get; set; }
            public string cpf { get; set; }
            public string data_de_nascimento { get; set; }
            public string estado_expedicao_cnh { get; set; }
            public string estado_expedicao_rg { get; set; }
            public List<ImagensFacematch> imagens_facematch { get; set; }
            public string nome { get; set; }
            public string nome_da_mae { get; set; }
            public string nome_do_pai { get; set; }
            public string orgao_emissor_rg { get; set; }
            public List<ParametrosAuxiliare> parametros_auxiliares { get; set; }
            public string rg { get; set; }
            public string seguranca_cnh { get; set; }
            public List<RecursosMidia> recursos_midia { get; set; }
        }

        public class DocumentoOcr
        {
            public string filename_front { get; set; }
            public string id_usuario { get; set; }
            public string status { get; set; }
            public string nome { get; set; }
            public string rg { get; set; }
            public string orgao_emissor_rg { get; set; }
            public string estado_emissao_rg { get; set; }
            public string cpf { get; set; }
            public string data_de_nascimento { get; set; }
            public string nome_do_pai { get; set; }
            public string nome_da_mae { get; set; }
            public string categoria { get; set; }
            public string numero_registro { get; set; }
            public string validade { get; set; }
            public string data_primeira_habilitacao { get; set; }
            public string observacoes { get; set; }
            public string data_de_emissao { get; set; }
            public string numero_renach { get; set; }
            public string numero_espelho { get; set; }
            public string permissao { get; set; }
            public object orgao_emissor { get; set; }
            public string local_emissao { get; set; }
            public string estado_emissao { get; set; }
            public string acc { get; set; }
            public string numero { get; set; }
            public string numero_seguranca { get; set; }
            public string filename_back { get; set; }
            public string matriz { get; set; }
            public string id_protocolo { get; set; }
            public object foto_perfil { get; set; }
            public object rg_digito { get; set; }
            public object filename_full { get; set; }
            public bool mask { get; set; }
            public string tipo_detectado { get; set; }
        }

        public class Result
        {
            public DateTime atualizado_em { get; set; }
            public string mensagem { get; set; }
            public string nome { get; set; }
            public string numero { get; set; }
            public Parametros parametros { get; set; }
            public string resultado { get; set; }
            public string status { get; set; }
            public DocumentoOcr documento_ocr { get; set; }
        }

        public class Root
        {
            public Result result { get; set; }
            public int status_code { get; set; }
        }


    }
}
