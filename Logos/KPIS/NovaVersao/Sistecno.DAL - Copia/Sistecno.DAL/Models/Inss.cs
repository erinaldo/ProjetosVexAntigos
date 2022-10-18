using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Inss
    {
        public int IdInss { get; set; }
        public int IdEmpresa { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public Nullable<decimal> AliquotaEmpresa { get; set; }
        public Nullable<decimal> BaseDeCalculo { get; set; }
        public Nullable<decimal> ValorTeto { get; set; }
        public Nullable<decimal> SestSenat { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
