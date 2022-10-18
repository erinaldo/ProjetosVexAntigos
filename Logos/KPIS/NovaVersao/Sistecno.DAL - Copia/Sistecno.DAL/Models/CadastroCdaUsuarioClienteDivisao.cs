using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroCdaUsuarioClienteDivisao
    {
        public int IdCadastroCdaUsuarioClienteDivisao { get; set; }
        public int IdCadastroCda { get; set; }
        public int IDUsuarioClienteDivisao { get; set; }
        public string Inventario { get; set; }
        public virtual UsuarioClienteDivisao UsuarioClienteDivisao { get; set; }
    }
}
