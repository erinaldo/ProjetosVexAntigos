//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
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
