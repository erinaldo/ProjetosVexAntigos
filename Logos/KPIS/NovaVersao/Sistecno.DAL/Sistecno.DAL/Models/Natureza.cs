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
    
    public partial class Natureza
    {
        public int IDNatureza { get; set; }
        public string Descricao { get; set; }
        public string IRRF { get; set; }
        public string ISS { get; set; }
        public string INSS { get; set; }
        public string CSLL { get; set; }
        public string COFINS { get; set; }
        public string PIS { get; set; }
        public Nullable<decimal> IRRFPercentual { get; set; }
        public Nullable<decimal> ISSPercentual { get; set; }
        public Nullable<decimal> INSSPercentual { get; set; }
        public Nullable<decimal> CLSSPercentual { get; set; }
        public Nullable<decimal> COFINSPercentual { get; set; }
        public Nullable<decimal> PISPercentual { get; set; }
    }
}
