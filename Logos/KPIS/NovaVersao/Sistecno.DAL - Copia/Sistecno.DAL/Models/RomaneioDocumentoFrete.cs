using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioDocumentoFrete
    {
        public int IDRomaneioDocumentoFrete { get; set; }
        public int IDDocumento { get; set; }
        public int IDFilial { get; set; }
        public int IDCadastro { get; set; }
        public int IDServico { get; set; }
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
        public virtual Cadastro Cadastro { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Servico Servico { get; set; }
    }
}
