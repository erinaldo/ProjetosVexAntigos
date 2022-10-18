using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
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
