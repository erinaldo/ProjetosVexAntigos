using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioClienteDivisao
    {
        public UsuarioClienteDivisao()
        {
            this.CadastroCdaUsuarioClienteDivisaos = new List<CadastroCdaUsuarioClienteDivisao>();
        }

        public int IDUsuarioClienteDivisao { get; set; }
        public int IDUsuarioCliente { get; set; }
        public int IDClienteDivisao { get; set; }
        public virtual ICollection<CadastroCdaUsuarioClienteDivisao> CadastroCdaUsuarioClienteDivisaos { get; set; }
        public virtual ClienteDivisao ClienteDivisao { get; set; }
    }
}
