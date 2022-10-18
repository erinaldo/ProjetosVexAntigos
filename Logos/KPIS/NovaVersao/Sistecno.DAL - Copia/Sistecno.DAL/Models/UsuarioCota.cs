using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioCota
    {
        public int IdUsuarioCota { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string SiglaProduto { get; set; }
        public Nullable<decimal> ValorCota { get; set; }
    }
}
