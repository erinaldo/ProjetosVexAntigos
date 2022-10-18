using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoCondicaoDePagamento
    {
        public int IdDocumentoCondicaoDePagamento { get; set; }
        public int IdDocumento { get; set; }
        public System.DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public virtual Documento Documento { get; set; }
    }
}
