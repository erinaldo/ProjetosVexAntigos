using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoConta
    {
        public BancoConta()
        {
            this.BancoContaBloquetoes = new List<BancoContaBloqueto>();
            this.Cheques = new List<Cheque>();
            this.MovimentacaoBancarias = new List<MovimentacaoBancaria>();
            this.MovimentacaoBancarias1 = new List<MovimentacaoBancaria>();
            this.MovimentacaoBancarias2 = new List<MovimentacaoBancaria>();
        }

        public int IDBancoConta { get; set; }
        public Nullable<int> IDBanco { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public int IdEmpresa { get; set; }
        public string BancoCarteira { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDigito { get; set; }
        public string Conta { get; set; }
        public string ContaDigito { get; set; }
        public string CodigoDaEmpresaNoBanco { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> LimiteDeCredito { get; set; }
        public Nullable<System.DateTime> LimiteDataDeVencimento { get; set; }
        public string Gerente { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string ParticipaDoFluxoDeCaixa { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public Nullable<int> IDCentroDeCusto { get; set; }
        public Nullable<decimal> SaldoDinheiro { get; set; }
        public Nullable<decimal> SaldoCheque { get; set; }
        public Nullable<decimal> SaldoCartao { get; set; }
        public string EmiteCheque { get; set; }
        public string AgenciaNome { get; set; }
        public string ContaTipo { get; set; }
        public string Titular { get; set; }
        public string DDD { get; set; }
        public string EMail { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<int> ProximoNumeroDeRemessa { get; set; }
        public Nullable<decimal> LimiteTed { get; set; }
        public Nullable<decimal> LimiteDoc { get; set; }
        public Nullable<int> ConvenioPagFor { get; set; }
        public Nullable<int> ProximoNumeroPagFor { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<BancoContaBloqueto> BancoContaBloquetoes { get; set; }
        public virtual ICollection<Cheque> Cheques { get; set; }
        public virtual ICollection<MovimentacaoBancaria> MovimentacaoBancarias { get; set; }
        public virtual ICollection<MovimentacaoBancaria> MovimentacaoBancarias1 { get; set; }
        public virtual ICollection<MovimentacaoBancaria> MovimentacaoBancarias2 { get; set; }
    }
}
