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
    
    public partial class Apolice
    {
        public int IdApolice { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<int> IdCadastroSeguradora { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public string Ativo { get; set; }
    
        public virtual Cadastro Cadastro { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Cadastro Cadastro1 { get; set; }
    }
}
