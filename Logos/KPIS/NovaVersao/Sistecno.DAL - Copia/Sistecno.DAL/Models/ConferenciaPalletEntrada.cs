using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaPalletEntrada
    {
        public int IdConferenciaPalletEntrada { get; set; }
        public int IdConferenciaPallet { get; set; }
        public int IdProdutoCliente { get; set; }
        public Nullable<int> IdProduto { get; set; }
        public Nullable<int> IdDocumentoItem { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> Fator { get; set; }
        public string Lote { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public virtual ConferenciaPallet ConferenciaPallet { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
    }
}
