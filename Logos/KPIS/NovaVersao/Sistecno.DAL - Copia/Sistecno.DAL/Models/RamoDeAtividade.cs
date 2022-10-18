using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RamoDeAtividade
    {
        public RamoDeAtividade()
        {
            this.Clientes = new List<Cliente>();
        }

        public int IDRamoDeAtividade { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
