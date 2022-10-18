using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Globalization;

namespace Robo.Email.Notas.Solutions.Windows.Testes
{
    public partial class TesteIdTabelacs : Form
    {

        public void FindCoordinates(double lat, double lng)
        {
            string requestUri = string.Format(baseUri, lat.ToString().Replace(",", "."), lng.ToString().Replace(",", "."));

            string url = requestUri;
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader); 
                    DataTable dtCoordinates = new DataTable();
                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)), new DataColumn("Address", typeof(string)), new DataColumn("Latitude", typeof(string)), new DataColumn("Longitude", typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0]; 
                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                    } 
                 
                }
            }
        }









        public TesteIdTabelacs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void TesteIdTabelacs_Load(object sender, EventArgs e)

            	
        {
            FindCoordinates(double.Parse("-23,5960266"), double.Parse("-46,6879576"));

            //WebRequest request = WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?sensor=false&address=" + Uri.EscapeDataString("Rua Claudio Manoel Da Costa, 425, Osasco , SP"));

            //using (WebResponse response = request.GetResponse())
            //{
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        XDocument document = XDocument.Load(new StreamReader(stream));

            //        XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
            //        XElement latitudeElement = document.Descendants("lat").FirstOrDefault();
            //        double Longitude = Double.Parse(longitudeElement.Value, CultureInfo.InvariantCulture);
            //        double Latitude = Double.Parse(latitudeElement.Value, CultureInfo.InvariantCulture);

            //        //RetrieveFormatedAddress(latitudeElement.Value, longitudeElement.Value);

            //        FindCoordinates(Latitude, Longitude);

            //    }
            //}

        }

        static string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";


        public static void RetrieveFormatedAddress(string lat, string lng)
        {
            string requestUri = string.Format(baseUri, lat, lng);

            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            //    wc.DownloadStringAsync(new Uri(requestUri));

            //}

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            //var xdoc = XDocument.Load(response.GetResponseStream());
        }

        //static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    var xmlElm = XElement.Parse(e.Result);

        //    var status = (from elm in xmlElm.Descendants()
        //                  where elm.Name == "status"
        //                  select elm).FirstOrDefault();
        //    if (status.Value.ToLower() == "ok")
        //    {
        //        var res = (from elm in xmlElm.Descendants()
        //                   where elm.Name == "formatted_address"
        //                   select elm).FirstOrDefault();
        //        Console.WriteLine(res.Value);
        //    }
        //    else
        //    {
        //        Console.WriteLine("No Address Found");
        //    }
        //}
    }

    public class GeocoderLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
