using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Rastreamento
    {
        public int IdRastreamento { get; set; }
        public int IdRastreador { get; set; }
        public Nullable<int> IdDt { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<int> Satelites { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public string PontodeOcorrencia { get; set; }
        public string LATI { get; set; }
        public string LONGI { get; set; }
        public Nullable<System.DateTime> DataHoraTransmissao { get; set; }
        public virtual DT DT { get; set; }
        public virtual Rastreador Rastreador { get; set; }
    }
}
