using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Representante
    {
        public Representante()
        {
            this.RepresentanteClientes = new List<RepresentanteCliente>();
        }

        public int IdRepresentante { get; set; }
        public int IdFilial { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<RepresentanteCliente> RepresentanteClientes { get; set; }
    }
}
