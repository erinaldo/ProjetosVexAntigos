//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventarioContagem
    {
        public InventarioContagem()
        {
            this.InventarioContagemProduto = new HashSet<InventarioContagemProduto>();
            this.InventarioUa = new HashSet<InventarioUa>();
        }
    
        public int IdinventarioContagem { get; set; }
        public int Idinventario { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
    
        public virtual Inventario Inventario { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProduto { get; set; }
        public virtual ICollection<InventarioUa> InventarioUa { get; set; }
    }
}
