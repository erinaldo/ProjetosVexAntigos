using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutoFoto
    {
        public int IDProdutoFoto { get; set; }
        public int IDProduto { get; set; }
        public byte[] Foto { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
