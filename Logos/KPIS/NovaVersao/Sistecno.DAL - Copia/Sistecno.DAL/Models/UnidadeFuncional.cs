using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UnidadeFuncional
    {
        public UnidadeFuncional()
        {
            this.UsuarioCompras = new List<UsuarioCompra>();
        }

        public int IdUnidadeFuncional { get; set; }
        public string UnidadeFuncional1 { get; set; }
        public Nullable<int> IdParente { get; set; }
        public virtual ICollection<UsuarioCompra> UsuarioCompras { get; set; }
    }
}
