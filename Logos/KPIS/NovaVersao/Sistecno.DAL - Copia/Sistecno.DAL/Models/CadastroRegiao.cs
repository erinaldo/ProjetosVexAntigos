using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroRegiao
    {
        public int IDCadastroRegiao { get; set; }
        public int IDCadastro { get; set; }
        public int IDFilial { get; set; }
        public int IDRegiaoItem { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual RegiaoItem RegiaoItem { get; set; }
    }
}
