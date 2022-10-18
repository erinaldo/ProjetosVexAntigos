using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClientesComprovei
    {
        public int IdCadastro { get; set; }
        public string CnpjCPF { get; set; }
        public string RazaoSocialNome { get; set; }
        public string FantasiaApelido { get; set; }
        public string Ativo { get; set; }
    }
}
