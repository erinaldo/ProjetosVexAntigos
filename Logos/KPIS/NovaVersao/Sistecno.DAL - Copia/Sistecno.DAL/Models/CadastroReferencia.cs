using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroReferencia
    {
        public int IDCadastroReferencia { get; set; }
        public int IDCadastro { get; set; }
        public string TipoDeReferencia { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Referencia { get; set; }
        public string Contato { get; set; }
        public string Observacao { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}
