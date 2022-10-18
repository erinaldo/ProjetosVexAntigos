using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo.Email.Notas.Solutions.Windows.Testes.Dtos
{
   public  class LoginKabum
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class JsonSerializer
        {
            public object DateFormat { get; set; }
            public object RootElement { get; set; }
            public object Namespace { get; set; }
            public string ContentType { get; set; }
        }

        public class XmlSerializer
        {
            public object RootElement { get; set; }
            public object Namespace { get; set; }
            public object DateFormat { get; set; }
            public string ContentType { get; set; }
        }

        public class Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public int Type { get; set; }
            public object ContentType { get; set; }
        }

        public class Method
        {
            public string Name { get; set; }
            public string AssemblyName { get; set; }
            public string ClassName { get; set; }
            public string Signature { get; set; }
            public string Signature2 { get; set; }
            public int MemberType { get; set; }
            public object GenericArguments { get; set; }
        }

        public class OnBeforeDeserialization
        {
            public Method Method { get; set; }
            public object Target { get; set; }
        }

        public class Request
        {
            public bool AlwaysMultipartFormData { get; set; }
            public JsonSerializer JsonSerializer { get; set; }
            public XmlSerializer XmlSerializer { get; set; }
            public object ResponseWriter { get; set; }
            public bool UseDefaultCredentials { get; set; }
            public List<Parameter> Parameters { get; set; }
            public List<object> Files { get; set; }
            public int Method { get; set; }
            public object Resource { get; set; }
            public int RequestFormat { get; set; }
            public object RootElement { get; set; }
            public OnBeforeDeserialization OnBeforeDeserialization { get; set; }
            public object DateFormat { get; set; }
            public object XmlNamespace { get; set; }
            public object Credentials { get; set; }
            public object UserState { get; set; }
            public int Timeout { get; set; }
            public int ReadWriteTimeout { get; set; }
            public int Attempts { get; set; }
        }

        public class Cooky
        {
            public string Comment { get; set; }
            public object CommentUri { get; set; }
            public bool Discard { get; set; }
            public string Domain { get; set; }
            public bool Expired { get; set; }
            public DateTime Expires { get; set; }
            public bool HttpOnly { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public string Port { get; set; }
            public bool Secure { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Value { get; set; }
            public int Version { get; set; }
        }

        public class Header
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int Type { get; set; }
            public object ContentType { get; set; }
        }

        public class Root
        {
            public Request Request { get; set; }
            public string ContentType { get; set; }
            public int ContentLength { get; set; }
            public string ContentEncoding { get; set; }
            public string Content { get; set; }
            public int StatusCode { get; set; }
            public string StatusDescription { get; set; }
            public string RawBytes { get; set; }
            public string ResponseUri { get; set; }
            public string Server { get; set; }
            public List<Cooky> Cookies { get; set; }
            public List<Header> Headers { get; set; }
            public int ResponseStatus { get; set; }
            public object ErrorMessage { get; set; }
            public object ErrorException { get; set; }
        }


    }
}
