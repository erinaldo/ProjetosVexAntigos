using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Inventario
    {
        public Inventario()
        {
            this.InventarioContagems = new List<InventarioContagem>();
        }

        public int IdInventario { get; set; }
        public int IdFilial { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Situacao { get; set; }
        public string Descricao { get; set; }
        public string TipoDeInventario { get; set; }
        public string SolicitarUA { get; set; }
        public Nullable<int> PosicoesContadas { get; set; }
        public Nullable<int> PosicoesCorretas { get; set; }
        public Nullable<int> PosicoesContadasABC { get; set; }
        public Nullable<int> PosicoesCorretasABC { get; set; }
        public Nullable<int> SKUCorretos { get; set; }
        public Nullable<int> SKUTotal { get; set; }
        public Nullable<int> IdCda { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<InventarioContagem> InventarioContagems { get; set; }
    }
}
