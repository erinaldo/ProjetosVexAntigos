using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroTipoDeContato
    {
        public CadastroTipoDeContato()
        {
            this.CadastroContatoEnderecoes = new List<CadastroContatoEndereco>();
        }

        public int IDCadastroTipoDeContato { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<CadastroContatoEndereco> CadastroContatoEnderecoes { get; set; }
    }
}
