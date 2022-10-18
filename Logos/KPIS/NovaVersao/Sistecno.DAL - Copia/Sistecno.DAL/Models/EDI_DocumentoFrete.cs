using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_DocumentoFrete
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDDocumentoFrete { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public Nullable<int> IDPagadorDoFrete { get; set; }
        public Nullable<int> IDServico { get; set; }
        public Nullable<int> IDCidadeOrigemCalculo { get; set; }
        public Nullable<int> IDCidadeDestinoCalculo { get; set; }
        public Nullable<decimal> FretePeso { get; set; }
        public Nullable<decimal> FreteExcedente { get; set; }
        public Nullable<decimal> FretePorNotaFiscal { get; set; }
        public Nullable<decimal> FretePercentual { get; set; }
        public Nullable<decimal> FreteValor { get; set; }
        public Nullable<decimal> Gris { get; set; }
        public Nullable<decimal> Cat { get; set; }
        public Nullable<decimal> Tas { get; set; }
        public Nullable<decimal> Ted { get; set; }
        public Nullable<decimal> Pedagio { get; set; }
        public Nullable<decimal> Seguro { get; set; }
        public Nullable<decimal> Suframa { get; set; }
        public Nullable<decimal> TaxaDeColeta { get; set; }
        public Nullable<decimal> TaxaDeEntrega { get; set; }
        public Nullable<decimal> Descarga { get; set; }
        public Nullable<decimal> Paletizacao { get; set; }
        public Nullable<decimal> Ajudante { get; set; }
        public Nullable<decimal> Diaria { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public Nullable<decimal> IcmsIss { get; set; }
        public Nullable<decimal> Frete { get; set; }
    }
}
