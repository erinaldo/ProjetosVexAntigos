using System;
using System.IO;
using System.Net;

namespace Sistecno.BLL.Helpers
{
    public static class CnpjCpf
    {

        public static string BaixarCaptcha(CookieContainer cookieContainer, String Capcha, string path)
        {
            var url = "http://www.receita.fazenda.gov.br/" + Capcha;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.CookieContainer = cookieContainer;

            var response = (HttpWebResponse)webRequest.GetResponse();
            cookieContainer.Add(response.Cookies);
            var stream = response.GetResponseStream();
            //var image = System.Drawing.Image.FromStream(stream);

            

            string data = DateTime.Now.ToString("ffssMMddmmyyyy");
            //image.Save(path + "/cap" + data + ".jpg");
            //stream.Close();

            // return "tmp/cap" + data + ".jpg";
            return "~/tmp/cap" + data + ".jpg";
        }

        public static string BaixarParametros(CookieContainer cookieContainer, string valor)
        {
            string url;

            if (valor.Length == 11)
                url = System.Configuration.ConfigurationSettings.AppSettings["URL_POST_CPF"];
            else
                url = System.Configuration.ConfigurationSettings.AppSettings["URL_POST_CNPJ"];


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            webRequest.ProtocolVersion = HttpVersion.Version11;
            webRequest.Host = "www.receita.fazenda.gov.br";
            webRequest.KeepAlive = true;
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            webRequest.Headers.Add("Accept-Language", "pt-BR,pt;q=0.8,en-US;q=0.6,en;q=0.4");


            cookieContainer.Add(new Cookie("nova_visita_dia", "47d566a9-d910-cbaf-f2d4-071e4813962f", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("nova_visita_mes", "47d566a9-d910-cbaf-f2d4-071e4813962f", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("nova_visita_ano", "47d566a9-d910-cbaf-f2d4-071e4813962f", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("duracao_visita", "b2a466ac-ccfb-64bb-4102-4bd8b6f8311d", "/", "www.receita.fazenda.gov.br"));

            cookieContainer.Add(new Cookie("tab_0_0", "0", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("tab_1_nav-0", "1", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("tab_1_nav-1", "1", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("tab_1_nav-2", "1", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("tab_1_nav-3", "1", "/", "www.receita.fazenda.gov.br"));
            cookieContainer.Add(new Cookie("duracao_visita", "b2a466ac-ccfb-64bb-4102-4bd8b6f8311d", "/", "www.receita.fazenda.gov.br"));

            webRequest.CookieContainer = cookieContainer;


            var response = (HttpWebResponse)webRequest.GetResponse();

            string cookie = response.Headers["Set-Cookie"];
            if (cookie != null)
                cookieContainer.Add(new Cookie(cookie.Substring(0, cookie.IndexOf("=")), cookie.Substring(cookie.IndexOf("=") + 1, cookie.IndexOf(";") - cookie.IndexOf("=") - 1), "/", "www.receita.fazenda.gov.br"));

            cookieContainer.Add(response.Cookies);
            Stream os = response.GetResponseStream();
            var str = new StreamReader(os).ReadToEnd();
            os.Close();
            return str;
        }
    }  
}
