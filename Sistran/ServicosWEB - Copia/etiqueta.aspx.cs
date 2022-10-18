using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ServicosWEB
{
    public partial class etiqueta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }



        public void Imprimir()
        {
            byte[] zpl = Encoding.UTF8.GetBytes(@"^XA
^CF0,60
^FO30,50^FDVex Logistica e Transporte^FS
^CF0,30
^FO30,120^GB750,3,3^FS
^FX Second section with recipient address and permit information.
^CFA,30
^FX Third section with bar code.
^FO80,170^BY3^BCN,120,,,,A^FD050837990001400012345678001004^FS
^XZ^XA
^CF0,60
^FO30,50^FDVex Logistica e Transporte^FS
^CF0,30
^FO30,120^GB750,3,3^FS
^FX Second section with recipient address and permit information.
^CFA,30
^FX Third section with bar code.
^FO80,170^BY3^BCN,120,,,,A^FD050837990001400012345678001004^FS
^XZ");

            // adjust print density (8dpmm), label width (4 inches), label height (6 inches), and label index (0) as necessary
            //var request = (HttpWebRequest)WebRequest.Create("http://api.labelary.com/v1/printers/8dpmm/labels/4x6/");

            string width = "3.93701";
            string height = "1.9685";
            var dpmm = "8dpmm";

            string texto = $"http://api.labelary.com/v1/printers/{dpmm}/labels/{width}x{height}";

            var request = (HttpWebRequest)WebRequest.Create(texto);


            request.Method = "POST";
            request.Accept = "application/pdf"; // omit this line to get PNG images back
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zpl.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(zpl, 0, zpl.Length);
            requestStream.Close();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                var fileStream = File.Create(MapPath("etiquetas") + "\\etiqueta.pdf"); // change file name for PNG images


                responseStream.CopyTo(fileStream);
                responseStream.Close();
                fileStream.Close();

                ((HtmlControl)(form1.FindControl("ifrm"))).Attributes["src"] = "etiquetas/etiqueta.pdf";
                //ifrm.Attributes.Add("src", "etiquetas/etiqueta.pdf");

            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: {0}", ex.Status);
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}