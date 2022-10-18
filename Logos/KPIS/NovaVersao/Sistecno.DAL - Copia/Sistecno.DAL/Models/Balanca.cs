using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Balanca
    {
        public string ETIQUETA { get; set; }
        public Nullable<double> COMPRIMENTO { get; set; }
        public Nullable<double> LARGURA { get; set; }
        public Nullable<double> ALTURA { get; set; }
        public Nullable<double> PESO { get; set; }
        public Nullable<double> MULTIPLO { get; set; }
        public string USUARIO { get; set; }
        public string TIPO { get; set; }
        public Nullable<System.DateTime> DATA { get; set; }
        public Nullable<int> cdStatus { get; set; }
        public Nullable<System.DateTime> dtIntegrado { get; set; }
        public Nullable<bool> stIntegradoStore { get; set; }
        public Nullable<bool> stIntegradoESL { get; set; }
        public Nullable<System.DateTime> dtIntegradoStore { get; set; }
        public Nullable<System.DateTime> dtIntegradoESL { get; set; }
        public string dsIntegradoErro { get; set; }
        public string dsLog { get; set; }
        public Nullable<System.DateTime> dtEmailErro { get; set; }
        public string DOCUMENTOSAIDA { get; set; }
    }
}
