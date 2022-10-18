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
    
    public partial class Empresa
    {
        public Empresa()
        {
            this.Apolice = new HashSet<Apolice>();
            this.BancoConta = new HashSet<BancoConta>();
            this.CentroDeCusto = new HashSet<CentroDeCusto>();
            this.ContaContabil = new HashSet<ContaContabil>();
            this.Filial = new HashSet<Filial>();
            this.Inss = new HashSet<Inss>();
            this.Modal = new HashSet<Modal>();
            this.Numerador = new HashSet<Numerador>();
            this.Ocorrencia = new HashSet<Ocorrencia>();
            this.ParametroFluxoDeCaixa = new HashSet<ParametroFluxoDeCaixa>();
        }
    
        public int IDEmpresa { get; set; }
        public Nullable<int> IDGrupo { get; set; }
        public string Nome { get; set; }
        public string PermiteCNPJErrado { get; set; }
        public string PermiteIEErrada { get; set; }
        public string FormatacaoDaContaContabil { get; set; }
        public string FormatacaoDoCentroDeCusto { get; set; }
        public string CodigoDaLicenca { get; set; }
        public string AcessosSimultaneos { get; set; }
        public string Permissao { get; set; }
        public string MinSegProtecaoTela { get; set; }
        public Nullable<int> ExpirarSenha { get; set; }
        public string Ativo { get; set; }
        public string OptanteSimples { get; set; }
        public string CSOSN { get; set; }
        public string RNTRC { get; set; }
        public string RegimeApuracaoImposto { get; set; }
        public Nullable<decimal> Pis { get; set; }
        public Nullable<decimal> Cofins { get; set; }
        public Nullable<decimal> LimitadorIcms { get; set; }
        public Nullable<decimal> AliquotaSimples { get; set; }
        public string LocalGerarArquivoAverbacao { get; set; }
    
        public virtual ICollection<Apolice> Apolice { get; set; }
        public virtual ICollection<BancoConta> BancoConta { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<CentroDeCusto> CentroDeCusto { get; set; }
        public virtual ICollection<ContaContabil> ContaContabil { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<Filial> Filial { get; set; }
        public virtual ICollection<Inss> Inss { get; set; }
        public virtual ICollection<Modal> Modal { get; set; }
        public virtual ICollection<Numerador> Numerador { get; set; }
        public virtual ICollection<Ocorrencia> Ocorrencia { get; set; }
        public virtual ICollection<ParametroFluxoDeCaixa> ParametroFluxoDeCaixa { get; set; }
    }
}
