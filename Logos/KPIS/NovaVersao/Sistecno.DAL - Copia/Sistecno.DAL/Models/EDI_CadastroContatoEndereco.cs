using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_CadastroContatoEndereco
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDCadastroContatoEndereco { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public Nullable<int> IDCadastroTipoDeContato { get; set; }
        public string Endereco { get; set; }
    }
}
