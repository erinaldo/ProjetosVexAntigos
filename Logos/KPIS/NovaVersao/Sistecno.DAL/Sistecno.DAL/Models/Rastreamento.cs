//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
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
