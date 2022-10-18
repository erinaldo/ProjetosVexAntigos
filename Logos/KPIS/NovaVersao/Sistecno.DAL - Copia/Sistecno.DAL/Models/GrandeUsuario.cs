using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class GrandeUsuario
    {
        public int IDGrandeUsuario { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Abreviatura { get; set; }
        public virtual GrandeUsuarioEndereco GrandeUsuarioEndereco { get; set; }
    }
}
