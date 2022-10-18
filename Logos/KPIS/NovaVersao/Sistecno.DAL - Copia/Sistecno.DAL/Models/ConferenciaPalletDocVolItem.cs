using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaPalletDocVolItem
    {
        public int IdConferenciaPalletDocVolItem { get; set; }
        public int IdConferenciaPalletDocVol { get; set; }
        public int IdProdutoCliente { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> Fator { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public string Lote { get; set; }
        public virtual ConferenciaPalletDocVol ConferenciaPalletDocVol { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
    }
}
