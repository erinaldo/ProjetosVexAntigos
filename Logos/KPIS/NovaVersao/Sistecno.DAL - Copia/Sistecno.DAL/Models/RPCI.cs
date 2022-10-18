using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RPCI
    {
        public RPCI()
        {
            this.DTs = new List<DT>();
            this.RPCIDocumentoes = new List<RPCIDocumento>();
            this.RPCIImagems = new List<RPCIImagem>();
            this.TituloDocumentoes = new List<TituloDocumento>();
        }

        public int IDRPCI { get; set; }
        public int IDFilial { get; set; }
        public int IDUsuario { get; set; }
        public int IDCadastroTitular { get; set; }
        public string RPCIContrato { get; set; }
        public int Numero { get; set; }
        public string NumeroOriginal { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public Nullable<System.DateTime> DataDeEmissao { get; set; }
        public Nullable<decimal> BaseCalculoINSS { get; set; }
        public Nullable<decimal> AliquotaINSS { get; set; }
        public Nullable<decimal> BaseCalculoIRRF { get; set; }
        public Nullable<decimal> AliquotaIRRF { get; set; }
        public Nullable<decimal> BaseCalculoSestSenat { get; set; }
        public Nullable<decimal> AliquotaSestSenat { get; set; }
        public Nullable<decimal> CreditoValor { get; set; }
        public Nullable<decimal> CreditoAgenciamento { get; set; }
        public Nullable<decimal> CreditoPedagio { get; set; }
        public Nullable<decimal> CreditoCarga { get; set; }
        public Nullable<decimal> CreditoDescarga { get; set; }
        public Nullable<decimal> CreditoDiaria { get; set; }
        public Nullable<decimal> CreditoColeta { get; set; }
        public Nullable<decimal> CreditoEntrega { get; set; }
        public Nullable<decimal> CreditoAjudante { get; set; }
        public Nullable<decimal> CreditoAdicional { get; set; }
        public Nullable<decimal> CreditoOutros { get; set; }
        public Nullable<decimal> DebitoINSS { get; set; }
        public Nullable<decimal> DebitoSestSenat { get; set; }
        public Nullable<decimal> DebitoIRRF { get; set; }
        public Nullable<decimal> DebitoSeguro { get; set; }
        public Nullable<decimal> DebitoOutros { get; set; }
        public Nullable<decimal> DebitoAdiantamento { get; set; }
        public Nullable<decimal> SaldoAReceber { get; set; }
        public Nullable<int> Dependentes { get; set; }
        public Nullable<decimal> DependentesDeducao { get; set; }
        public Nullable<decimal> ValorAcumuladoAnterior { get; set; }
        public Nullable<decimal> IRRFAcumuladoAnterior { get; set; }
        public Nullable<decimal> INSSAcumuladoAnterior { get; set; }
        public string Ativo { get; set; }
        public string Situacao { get; set; }
        public Nullable<decimal> ValorPensaoAlimenticia { get; set; }
        public Nullable<decimal> AliquotaISS { get; set; }
        public Nullable<decimal> ValorISS { get; set; }
        public Nullable<decimal> BaseCalculoIRRFAcumulado { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<RPCIDocumento> RPCIDocumentoes { get; set; }
        public virtual ICollection<RPCIImagem> RPCIImagems { get; set; }
        public virtual ICollection<TituloDocumento> TituloDocumentoes { get; set; }
    }
}
