using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaPalletProdutoXXXXX
    {
        public int IdConferenciaPalletProduto { get; set; }
        public int IdConferenciaPallet { get; set; }
        public int IdProdutoEmbalagem { get; set; }
        public decimal Quantidade { get; set; }
        public string Tipo { get; set; }
        public virtual ConferenciaPallet ConferenciaPallet { get; set; }
        public virtual ProdutoEmbalagem ProdutoEmbalagem { get; set; }
    }
}
