using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroHistorico
    {
        public int IDCadastroHistorico { get; set; }
        public int IDCadastro { get; set; }
        public string Data { get; set; }
        public int IDUsuario { get; set; }
        public string Assunto { get; set; }
        public string PessoaDeContato { get; set; }
        public string Texto { get; set; }
        public byte[] Arquivo { get; set; }
        public string NomeArquivo { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}
