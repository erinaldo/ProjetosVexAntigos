using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroEndereco
    {
        public int IDCadastroEndereco { get; set; }
        public int IDCadastro { get; set; }
        public string TipoDeEndereco { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDBairro { get; set; }
        public string CnpjCpf { get; set; }
        public string RazaoSocialNome { get; set; }
        public virtual Bairro Bairro { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
