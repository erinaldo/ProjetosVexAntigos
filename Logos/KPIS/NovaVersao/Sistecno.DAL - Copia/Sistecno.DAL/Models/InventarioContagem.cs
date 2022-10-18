using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class InventarioContagem
    {
        public InventarioContagem()
        {
            this.InventarioContagemProdutoes = new List<InventarioContagemProduto>();
            this.InventarioUas = new List<InventarioUa>();
        }

        public int IdinventarioContagem { get; set; }
        public int Idinventario { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public virtual Inventario Inventario { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public virtual ICollection<InventarioUa> InventarioUas { get; set; }
    }
}
