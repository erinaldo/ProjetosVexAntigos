using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Icm
    {
        public int IDIcms { get; set; }
        public int IDEstadoOrigem { get; set; }
        public int IDEstadoDestino { get; set; }
        public Nullable<decimal> AliquotaContribuinte { get; set; }
        public Nullable<decimal> AliquotaNaoContribuinte { get; set; }
        public Nullable<decimal> AliquotaSeguroAcidente { get; set; }
        public Nullable<decimal> AliquotaSeguroRoubo { get; set; }
        public Nullable<decimal> FatorDeInclusaoDeIcmsContribuinte { get; set; }
        public Nullable<decimal> AliquotaNaoContribuinteNF { get; set; }
        public Nullable<decimal> AliquotaContribuinteNF { get; set; }
        public Nullable<decimal> FCP { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Estado Estado1 { get; set; }
    }
}
