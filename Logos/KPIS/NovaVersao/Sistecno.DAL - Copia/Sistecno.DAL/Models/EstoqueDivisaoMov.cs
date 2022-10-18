using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EstoqueDivisaoMov
    {
        public int IDEstoqueDivisaoMov { get; set; }
        public int IDEstoqueDivisao { get; set; }
        public int IDEstoqueOperacao { get; set; }
        public Nullable<int> IDEstoqueDivisaoOrigem { get; set; }
        public Nullable<int> IDEstoqueDivisaoDestino { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IdMovimentacaoItem { get; set; }
        public string Historico { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Saldo { get; set; }
        public Nullable<decimal> SaldoBaseExterna { get; set; }
        public Nullable<int> IdUsuarioSolicitante { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public virtual EstoqueDivisao EstoqueDivisao { get; set; }
        public virtual EstoqueDivisao EstoqueDivisao1 { get; set; }
        public virtual EstoqueDivisao EstoqueDivisao2 { get; set; }
        public virtual EstoqueOperacao EstoqueOperacao { get; set; }
        public virtual MovimentacaoItem MovimentacaoItem { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
