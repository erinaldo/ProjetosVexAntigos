using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            this.Apolice = new List<Apolouse>();
            this.BancoContas = new List<BancoConta>();
            this.CentroDeCustoes = new List<CentroDeCusto>();
            this.ContaContabils = new List<ContaContabil>();
            this.Filials = new List<Filial>();
            this.Insses = new List<Inss>();
            this.Modals = new List<Modal>();
            this.Numeradors = new List<Numerador>();
            this.Ocorrencias = new List<Ocorrencia>();
            this.ParametroFluxoDeCaixas = new List<ParametroFluxoDeCaixa>();
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
        public virtual ICollection<Apolouse> Apolice { get; set; }
        public virtual ICollection<BancoConta> BancoContas { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<CentroDeCusto> CentroDeCustoes { get; set; }
        public virtual ICollection<ContaContabil> ContaContabils { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<Filial> Filials { get; set; }
        public virtual ICollection<Inss> Insses { get; set; }
        public virtual ICollection<Modal> Modals { get; set; }
        public virtual ICollection<Numerador> Numeradors { get; set; }
        public virtual ICollection<Ocorrencia> Ocorrencias { get; set; }
        public virtual ICollection<ParametroFluxoDeCaixa> ParametroFluxoDeCaixas { get; set; }
    }
}
