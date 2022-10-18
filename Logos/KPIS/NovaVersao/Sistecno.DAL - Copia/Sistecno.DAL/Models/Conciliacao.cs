using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Conciliacao
    {
        public Conciliacao()
        {
            this.ConciliacaoLotes = new List<ConciliacaoLote>();
        }

        public int IdConciliacao { get; set; }
        public Nullable<decimal> HControleBanco { get; set; }
        public Nullable<decimal> HControleLote { get; set; }
        public Nullable<decimal> HControleRegistro { get; set; }
        public Nullable<decimal> HEmpresaInscricaoTipo { get; set; }
        public Nullable<decimal> HEmpresaInscricaoNumero { get; set; }
        public string HEmpresaConvenio { get; set; }
        public Nullable<decimal> HEmpresaContaAgenciaCodigo { get; set; }
        public string HEmpresaContaAgenciaDigito { get; set; }
        public Nullable<decimal> HEmpresaContaContaNumero { get; set; }
        public string HEmpresaContaContaDigito { get; set; }
        public string HEmpresaContaDV { get; set; }
        public string HEmpresaNome { get; set; }
        public string HNomeDoBanco { get; set; }
        public Nullable<decimal> HArquivoCodigo { get; set; }
        public Nullable<decimal> HArquivoDataGeracao { get; set; }
        public Nullable<decimal> HArquivoHoraGeracao { get; set; }
        public Nullable<decimal> HArquivoSequencia { get; set; }
        public Nullable<decimal> HArquivoLayOut { get; set; }
        public string HReservadoEmpresa { get; set; }
        public Nullable<decimal> TControleBanco { get; set; }
        public Nullable<decimal> TControleLote { get; set; }
        public Nullable<decimal> TControleRegistro { get; set; }
        public Nullable<decimal> TTotaisQuantidadeDeLotes { get; set; }
        public Nullable<decimal> TTotaisQuantidadeDeRegistros { get; set; }
        public Nullable<decimal> TTotaisQuantidadeDeContasConciliadas { get; set; }
        public virtual ICollection<ConciliacaoLote> ConciliacaoLotes { get; set; }
    }
}
