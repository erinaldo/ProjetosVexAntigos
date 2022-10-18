using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PreFaturaRoge
    {
        public int IdPreFaturaRoge { get; set; }
        public System.DateTime ChavePlanilha { get; set; }
        public int IdFilial { get; set; }
        public string Chave { get; set; }
        public decimal Frete { get; set; }
        public Nullable<int> IdDocumentoNF { get; set; }
        public Nullable<int> IdDocumentoCte { get; set; }
        public Nullable<int> IdLinhaPlanilhaRoge { get; set; }
        public Nullable<int> IdDocumentoAguardandoCtrc { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdTitulo { get; set; }
        public string Operacao { get; set; }
        public string Situacao { get; set; }
        public Nullable<int> IdCteBkp { get; set; }
        public Nullable<System.DateTime> DataHora { get; set; }
    }
}
