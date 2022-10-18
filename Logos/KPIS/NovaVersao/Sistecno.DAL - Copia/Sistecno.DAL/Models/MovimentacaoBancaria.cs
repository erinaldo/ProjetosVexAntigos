using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MovimentacaoBancaria
    {
        public int IDMovimentacaoBancaria { get; set; }
        public Nullable<int> IDBancoConta { get; set; }
        public Nullable<int> IDTituloDuplicata { get; set; }
        public Nullable<int> IDCheque { get; set; }
        public string Descricao { get; set; }
        public string DebitoCredito { get; set; }
        public decimal Valor { get; set; }
        public System.DateTime Data { get; set; }
        public Nullable<System.DateTime> DataDisponibilidade { get; set; }
        public Nullable<int> IDContaOrigem { get; set; }
        public Nullable<int> IDContaDestino { get; set; }
        public string origem { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public string Tipo { get; set; }
        public string Documento { get; set; }
        public virtual BancoConta BancoConta { get; set; }
        public virtual BancoConta BancoConta1 { get; set; }
        public virtual BancoConta BancoConta2 { get; set; }
        public virtual Cheque Cheque { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual TituloDuplicata TituloDuplicata { get; set; }
    }
}
