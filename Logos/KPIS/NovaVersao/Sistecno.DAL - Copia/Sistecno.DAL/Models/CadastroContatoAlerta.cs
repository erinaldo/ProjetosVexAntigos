using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroContatoAlerta
    {
        public int IDCadastroContatoAlerta { get; set; }
        public int IDCadastroContato { get; set; }
        public int IDAlerta { get; set; }
        public virtual Alerta Alerta { get; set; }
        public virtual CadastroContato CadastroContato { get; set; }
    }
}
