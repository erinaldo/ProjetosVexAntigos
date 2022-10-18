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
    
    public partial class CentroDeCusto
    {
        public CentroDeCusto()
        {
            this.BancoConta = new HashSet<BancoConta>();
            this.CentroDeCustoFilial = new HashSet<CentroDeCustoFilial>();
            this.Cliente = new HashSet<Cliente>();
            this.ContaContabilCentroDeCusto = new HashSet<ContaContabilCentroDeCusto>();
            this.Filial = new HashSet<Filial>();
            this.Filial1 = new HashSet<Filial>();
            this.Fornecedor = new HashSet<Fornecedor>();
            this.Fornecedor1 = new HashSet<Fornecedor>();
            this.Fornecedor2 = new HashSet<Fornecedor>();
            this.Funcionario = new HashSet<Funcionario>();
            this.LancamentoContabil = new HashSet<LancamentoContabil>();
            this.ProdutoCliente = new HashSet<ProdutoCliente>();
            this.ProdutoCliente1 = new HashSet<ProdutoCliente>();
            this.UsuarioCentroDeCusto = new HashSet<UsuarioCentroDeCusto>();
        }
    
        public int IDCentroDeCusto { get; set; }
        public int IDEmpresa { get; set; }
        public string CentroDeCusto1 { get; set; }
        public string Nome { get; set; }
        public string CodigoReduzido { get; set; }
        public Nullable<int> IDParente { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<decimal> Percentual { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public string Ativo { get; set; }
        public string TipoDeConta { get; set; }
    
        public virtual ICollection<BancoConta> BancoConta { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<CentroDeCustoFilial> CentroDeCustoFilial { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<ContaContabilCentroDeCusto> ContaContabilCentroDeCusto { get; set; }
        public virtual ICollection<Filial> Filial { get; set; }
        public virtual ICollection<Filial> Filial1 { get; set; }
        public virtual ICollection<Fornecedor> Fornecedor { get; set; }
        public virtual ICollection<Fornecedor> Fornecedor1 { get; set; }
        public virtual ICollection<Fornecedor> Fornecedor2 { get; set; }
        public virtual ICollection<Funcionario> Funcionario { get; set; }
        public virtual ICollection<LancamentoContabil> LancamentoContabil { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoCliente { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoCliente1 { get; set; }
        public virtual ICollection<UsuarioCentroDeCusto> UsuarioCentroDeCusto { get; set; }
    }
}