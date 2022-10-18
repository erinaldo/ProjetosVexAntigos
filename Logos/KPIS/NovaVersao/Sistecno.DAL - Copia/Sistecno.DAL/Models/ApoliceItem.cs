using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ApoliceItem
    {
        public int IdApoliceItem { get; set; }
        public int IdApolice { get; set; }
        public Nullable<decimal> De { get; set; }
        public Nullable<decimal> Ate { get; set; }
        public string Produto { get; set; }
        public string Exigencias { get; set; }
        public string ExigeLiberacao { get; set; }
    }
}
