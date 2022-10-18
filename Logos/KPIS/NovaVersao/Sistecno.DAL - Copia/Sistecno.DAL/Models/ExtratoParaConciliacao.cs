using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ExtratoParaConciliacao
    {
        public int IdExtratoParaConciliacao { get; set; }
        public Nullable<int> IdContaContabilFilial { get; set; }
        public Nullable<System.DateTime> Processamento { get; set; }
        public string Id { get; set; }
        public string Documento { get; set; }
        public Nullable<System.DateTime> DataMovimento { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> Entrada { get; set; }
        public Nullable<decimal> Saida { get; set; }
        public string TipoMovimento { get; set; }
        public Nullable<int> BankID { get; set; }
        public string AccountID { get; set; }
        public string AccountType { get; set; }
        public Nullable<decimal> InitialBalance { get; set; }
        public Nullable<decimal> FinalBalance { get; set; }
        public string Conciliado { get; set; }
        public Nullable<int> CodigoDeConciliacao { get; set; }
    }
}
