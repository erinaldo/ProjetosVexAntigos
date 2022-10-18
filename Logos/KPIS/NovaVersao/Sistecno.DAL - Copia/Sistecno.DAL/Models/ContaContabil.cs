using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ContaContabil
    {
        public ContaContabil()
        {
            this.BancoContas = new List<BancoConta>();
            this.CentroDeCustoes = new List<CentroDeCusto>();
            this.Clientes = new List<Cliente>();
            this.ContaContabil1 = new List<ContaContabil>();
            this.ContaContabilCentroDeCustoes = new List<ContaContabilCentroDeCusto>();
            this.ContaContabilFilials = new List<ContaContabilFilial>();
            this.ContaContabilLancamentoes = new List<ContaContabilLancamento>();
            this.ContaContabilLancamentoes1 = new List<ContaContabilLancamento>();
            this.Filials = new List<Filial>();
            this.Filials1 = new List<Filial>();
            this.Fornecedors = new List<Fornecedor>();
            this.Fornecedors1 = new List<Fornecedor>();
            this.Fornecedors2 = new List<Fornecedor>();
            this.Funcionarios = new List<Funcionario>();
            this.LancamentoContabils = new List<LancamentoContabil>();
            this.ProdutoClientes = new List<ProdutoCliente>();
            this.ProdutoClientes1 = new List<ProdutoCliente>();
            this.Transportadoras = new List<Transportadora>();
        }

        public int IDContaContabil { get; set; }
        public int IDEmpresa { get; set; }
        public string Conta { get; set; }
        public string Nome { get; set; }
        public string CodigoReduzido { get; set; }
        public Nullable<int> IDParente { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string TipoDeConta { get; set; }
        public string Redutora { get; set; }
        public Nullable<int> Grau { get; set; }
        public Nullable<double> SaldoInicial { get; set; }
        public string SpedFiscal { get; set; }
        public string Receita { get; set; }
        public string Despesa { get; set; }
        public string ContaSefaz { get; set; }
        public string Ativo { get; set; }
        public string TipoDeNumerario { get; set; }
        public string DRE { get; set; }
        public string PlanoReferencial { get; set; }
        public string FCont { get; set; }
        public string ContaFCont { get; set; }
        public string Sistema { get; set; }
        public virtual ICollection<BancoConta> BancoContas { get; set; }
        public virtual ICollection<CentroDeCusto> CentroDeCustoes { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<ContaContabil> ContaContabil1 { get; set; }
        public virtual ContaContabil ContaContabil2 { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<ContaContabilCentroDeCusto> ContaContabilCentroDeCustoes { get; set; }
        public virtual ICollection<ContaContabilFilial> ContaContabilFilials { get; set; }
        public virtual ICollection<ContaContabilLancamento> ContaContabilLancamentoes { get; set; }
        public virtual ICollection<ContaContabilLancamento> ContaContabilLancamentoes1 { get; set; }
        public virtual ICollection<Filial> Filials { get; set; }
        public virtual ICollection<Filial> Filials1 { get; set; }
        public virtual ICollection<Fornecedor> Fornecedors { get; set; }
        public virtual ICollection<Fornecedor> Fornecedors1 { get; set; }
        public virtual ICollection<Fornecedor> Fornecedors2 { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<LancamentoContabil> LancamentoContabils { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes1 { get; set; }
        public virtual ICollection<Transportadora> Transportadoras { get; set; }
    }
}
