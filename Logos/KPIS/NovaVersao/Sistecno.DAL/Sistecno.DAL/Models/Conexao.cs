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
    
    public partial class Conexao
    {
        public int IdConexao { get; set; }
        public int IdCliente { get; set; }
        public string IP { get; set; }
        public string BaseDeDados { get; set; }
        public string UsuarioBD { get; set; }
        public string SenhaBD { get; set; }
        public string Porta { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}