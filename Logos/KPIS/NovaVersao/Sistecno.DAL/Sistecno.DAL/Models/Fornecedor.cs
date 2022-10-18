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
    
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            this.FornecedorFilial = new HashSet<FornecedorFilial>();
            this.Titulo = new HashSet<Titulo>();
        }
    
        public int IDFornecedor { get; set; }
        public int CodigoDoFornecedor { get; set; }
        public int CodigoDoFornecedorFilial { get; set; }
        public Nullable<int> IDRamoDeAtividade { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public Nullable<int> IDCentroDeCustoCredito { get; set; }
        public Nullable<int> IDCentroDeCustoDebito { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> IDCentroDeCusto { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
    
        public virtual Cadastro Cadastro { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual CentroDeCusto CentroDeCusto1 { get; set; }
        public virtual CentroDeCusto CentroDeCusto2 { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ContaContabil ContaContabil1 { get; set; }
        public virtual ContaContabil ContaContabil2 { get; set; }
        public virtual ICollection<FornecedorFilial> FornecedorFilial { get; set; }
        public virtual ICollection<Titulo> Titulo { get; set; }
    }
}