using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboEnvioOcorrenciaClientes.Classes
{
    public class PedidoRiachelo
    {
        public List<Datum> data { get; set; }
    }

    public class Carrier
    {
        public int id { get; set; }
        public string idNumber { get; set; }
        public string name { get; set; }
        public string carrierEnum { get; set; }
        public string sapCode { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string number { get; set; }
        public string zipCode { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string city { get; set; }
    }

    public class Recipient
    {
        public string idNumber { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
    }

    public class Invoice
    {
        public string number { get; set; }
        public string series { get; set; }
        public string key { get; set; }
        public string cfop { get; set; }
        public double amount { get; set; }
        public double productsAmount { get; set; }
        public string date { get; set; }
    }

    public class Package
    {
        public string packageNumber { get; set; }
        public string identifier { get; set; }
        public string nature { get; set; }
        public int productsQuantity { get; set; }
        public string type { get; set; }
        public double weight { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int length { get; set; }
        public Invoice invoice { get; set; }
    }

    public class Datum
    {
        public int trackingId { get; set; }
        public string orderNumber { get; set; }
        public string storeCode { get; set; }
        public string status { get; set; }
        public string deliveryMethod { get; set; }
        public Carrier carrier { get; set; }
        public Recipient recipient { get; set; }
        public List<Package> packages { get; set; }
        public int amount { get; set; }
        public int freightCost { get; set; }
        public int quantity { get; set; }
        public bool scheduled { get; set; }
    }

   
}
