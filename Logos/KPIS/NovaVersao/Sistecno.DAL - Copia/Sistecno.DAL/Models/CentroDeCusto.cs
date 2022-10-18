using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CentroDeCusto
    {
        public CentroDeCusto()
        {
            this.BancoContas = new List<BancoConta>();
            this.CentroDeCustoFilials = new List<CentroDeCustoFilial>();
            this.Clientes = new List<Cliente>();
            this.ContaContabilCentroDeCustoes = new List<ContaContabilCentroDeCusto>();
            this.Filials = new List<Filial>();
            this.Filials1 = new List<Filial>();
            this.Fornecedors = new List<Fornecedor>();
            this.Fornecedors1 = new List<Fornecedor>();
            this.Fornecedors2 = new List<Fornecedor>();
            this.Funcionarios = new List<Funcionario>();
            this.LancamentoContabils = new List<LancamentoContabil>();
            this.ProdutoClientes = new List<ProdutoCliente>();
            this.ProdutoClientes1 = new List<ProdutoCliente>();
            this.UsuarioCentroDeCustoes = new List<UsuarioCentroDeCusto>();
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
        public virtual ICollection<BancoConta> BancoContas { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<CentroDeCustoFilial> CentroDeCustoFilials { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<ContaContabilCentroDeCusto> ContaContabilCentroDeCustoes { get; set; }
        public virtual ICollection<Filial> Filials { get; set; }
        public virtual ICollection<Filial> Filials1 { get; set; }
        public virtual ICollection<Fornecedor> Fornecedors { get; set; }
        public virtual ICollection<Fornecedor> Fornecedors1 { get; set; }
        public virtual ICollection<Fornecedor> Fornecedors2 { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<LancamentoContabil> LancamentoContabils { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes1 { get; set; }
        public virtual ICollection<UsuarioCentroDeCusto> UsuarioCentroDeCustoes { get; set; }
    }
}
