using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo.Email.Notas.Solutions.Windows.Testes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class AdditionalInformation
    {
        [JsonProperty("receiver_name")]
        public string ReceiverName { get; set; }

        [JsonProperty("receiver_document")]
        public string ReceiverDocument { get; set; }

        [JsonProperty("kinship")]
        public string Kinship { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_in_base64")]
        public string ContentInBase64 { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("additional_information")]
        public AdditionalInformation AdditionalInformation { get; set; }
    }

    public class Location
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("additional")]
        public string Additional { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }

        [JsonProperty("quarter")]
        public string Quarter { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }

    public class Extra
    {
        [JsonProperty("any_key")]
        public string AnyKey { get; set; }
    }

    public class Event
    {
        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("original_code")]
        public string OriginalCode { get; set; }

        [JsonProperty("original_message")]
        public string OriginalMessage { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }
    }

    public class ViaVarejoModel
    {
        public ViaVarejoModel()
        {
            this.Events = new List<Event>();
        }
        [JsonProperty("logistics_provider")]
        public string LogisticsProvider { get; set; }

        [JsonProperty("logistics_provider_id")]
        public int LogisticsProviderId { get; set; }

        [JsonProperty("logistics_provider_federal_tax_id")]
        public string LogisticsProviderFederalTaxId { get; set; }

        [JsonProperty("shipper")]
        public string Shipper { get; set; }

        [JsonProperty("shipper_federal_tax_id")]
        public string ShipperFederalTaxId { get; set; }

        [JsonProperty("invoice_key")]
        public string InvoiceKey { get; set; }

        [JsonProperty("invoice_series")]
        public string InvoiceSeries { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("tracking_code")]
        public string TrackingCode { get; set; }

        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("volume_number")]
        public string VolumeNumber { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
    }


}
