using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioConferenciaItem
    {
        public int IdRomaneioConferenciaItem { get; set; }
        public int IdRomaneioConferencia { get; set; }
        public Nullable<int> IdEAN13 { get; set; }
        public Nullable<int> IdDUN14 { get; set; }
        public Nullable<int> IdProdutoCliente { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public Nullable<int> Fator { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Produto Produto1 { get; set; }
        public virtual RomaneioConferencia RomaneioConferencia { get; set; }
    }
}
