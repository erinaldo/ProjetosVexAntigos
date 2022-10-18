using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using System.Web.Services;
using Robo.Email.Notas.Solutions.Windows.Testes.WSStwx;

namespace WebServiceExample.Classes
{
    public class WebServiceClass : WebServiceAutologPOD
    {
        public string _Pass { get; set; }
        public string _User { get; set; }

        public WebServiceClass(string url)
        {
            if(!String.IsNullOrEmpty(url)) this.Url = url;
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.WebRequest request = base.GetWebRequest(uri);
            string auth = "Basic " +  Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(this._User + ":" + this._Pass));
            request.Headers.Add("Authorization", auth);
            return request;
        }
    }
}
