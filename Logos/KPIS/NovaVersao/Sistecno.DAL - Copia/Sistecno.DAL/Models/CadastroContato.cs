using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroContato
    {
        public CadastroContato()
        {
            this.CadastroContatoAlertas = new List<CadastroContatoAlerta>();
        }

        public int IDCadastroContato { get; set; }
        public int IDCadastro { get; set; }
        public int IDContato { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Cadastro Cadastro1 { get; set; }
        public virtual ICollection<CadastroContatoAlerta> CadastroContatoAlertas { get; set; }
    }
}
