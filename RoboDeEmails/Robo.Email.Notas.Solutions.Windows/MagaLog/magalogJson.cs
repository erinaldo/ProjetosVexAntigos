using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWEB.MagaLog
{
    public class Seller
    {
        public string id { get; set; }
    }

    public class Place
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class Sale
    {
        public Place place { get; set; }
    }

    public class DeliveryService
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class PackingList
    {
        public int id { get; set; }
    }

    public class Deadline
    {
        public string date { get; set; }
        public string period { get; set; }
    }

    public class Shipping
    {
        public double cost { get; set; }
    }

    public class Label
    {
        public string barcode { get; set; }
    }

    public class Product
    {
        public string id { get; set; }
        public string description { get; set; }
        public double weight { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int length { get; set; }
    }

    public class Volume
    {
        public double weight { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int length { get; set; }
        public Label label { get; set; }
        public List<Product> products { get; set; }
    }

    public class Amount
    {
        public double total { get; set; }
        public double items { get; set; }
    }

    public class Cte
    {
        public string key { get; set; }
        public string issueDate { get; set; }
    }

    public class Carrier
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cnpj { get; set; }
    }

    public class Address
    {
        public string zipcode { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int ibgeCode { get; set; }
    }

    public class Issuer
    {
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string name { get; set; }
        public string tradingName { get; set; }
        public Address address { get; set; }
    }

    public class Invoice
    {
        public string key { get; set; }
        public int number { get; set; }
        public string serie { get; set; }
        public string issueDate { get; set; }
        public Amount amount { get; set; }
        public Cte cte { get; set; }
        public Carrier carrier { get; set; }
        public Issuer issuer { get; set; }
        public int cfop { get; set; }
        public double icms { get; set; }
        public double icmsSubstitution { get; set; }
        public double baseIcms { get; set; }
        public double baseIcmsSubstitution { get; set; }
    }

    //public class Seller2
    //{
    //    public string id { get; set; }
    //}

    public class DistributionCenter
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
    }

    public class Store
    {
        public int id { get; set; }
        public string email { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
    }

    public class Customer
    {
        public string name { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string Cnpj { get; set; }
        public string ie { get; set; }
    }

    public class PickupPlace
    {
        public DistributionCenter distributionCenter { get; set; }
        public Store store { get; set; }
        public Customer customer { get; set; }
    }

    public class Phone
    {
        public string type { get; set; }
        public long number { get; set; }
    }

    //public class Address2
    //{
    //    public string zipcode { get; set; }
    //    public string street { get; set; }
    //    public string number { get; set; }
    //    public string complement { get; set; }
    //    public string district { get; set; }
    //    public string city { get; set; }
    //    public string state { get; set; }
    //    public string country { get; set; }
    //    public int ibgeCode { get; set; }
    //}

    public class Origin
    {
        public Seller seller { get; set; }
        public PickupPlace pickupPlace { get; set; }
        public List<Phone> phones { get; set; }
        public Address address { get; set; }
    }

    //public class Seller3
    //{
    //    public string id { get; set; }
    //}

    /*public class DistributionCenter2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
    }
    */
    //public class Store2
    //{
    //    public int id { get; set; }
    //    public string email { get; set; }
    //    public string cnpj { get; set; }
    //    public string ie { get; set; }
    //}

    //public class Customer2
    //{
    //    public string name { get; set; }
    //    public string rg { get; set; }
    //    public string cpf { get; set; }
    //    public string cnpj { get; set; }
    //    public string ie { get; set; }
    //}

    public class DeliveryPlace
    {
        public DistributionCenter distributionCenter { get; set; }
        public Store store { get; set; }
        public Customer customer { get; set; }
    }

    /*public class Phone2
    {
        public string type { get; set; }
        public long number { get; set; }
    }*/

    //public class Address3
    //{
    //    public string zipcode { get; set; }
    //    public string street { get; set; }
    //    public string number { get; set; }
    //    public string complement { get; set; }
    //    public string district { get; set; }
    //    public string city { get; set; }
    //    public string state { get; set; }
    //    public string country { get; set; }
    //    public int ibgeCode { get; set; }
    //}

    public class Destination
    {
        public Seller seller { get; set; }
        public DeliveryPlace deliveryPlace { get; set; }
        public List<Phone> phones { get; set; }
        public Address address { get; set; }
    }

    public class Package
    {
        public string id { get; set; }
        public DeliveryService deliveryService { get; set; }
        public PackingList packingList { get; set; }
        public Deadline deadline { get; set; }
        public Shipping shipping { get; set; }
        public List<Volume> volumes { get; set; }
        public Invoice invoice { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public Seller seller { get; set; }
        public Sale sale { get; set; }
        public List<Package> packages { get; set; }
    }

    public class RootObject
    {
        public Order order { get; set; }
    }
}