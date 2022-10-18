using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Titulo
    {
        public Titulo()
        {
            this.DocumentoEdis = new List<DocumentoEdi>();
            this.TituloDocumentoes = new List<TituloDocumento>();
            this.TituloDuplicatas = new List<TituloDuplicata>();
            this.TituloHistoricoes = new List<TituloHistorico>();
            this.TituloImagems = new List<TituloImagem>();
        }

        public int IDTitulo { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDFornecedor { get; set; }
        public int IDFilial { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IDCondicaoDePagamento { get; set; }
        public string Tipo { get; set; }
        public decimal Numero { get; set; }
        public string NumeroOriginal { get; set; }
        public string Serie { get; set; }
        public string PagarReceber { get; set; }
        public decimal Valor { get; set; }
        public string Impresso { get; set; }
        public string Ativo { get; set; }
        public Nullable<decimal> AliquotaMulta { get; set; }
        public Nullable<decimal> AliquotaJurosDiario { get; set; }
        public Nullable<decimal> Desconto { get; set; }
        public Nullable<System.DateTime> DataDeEmissao { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public string Origem { get; set; }
        public Nullable<System.DateTime> DataProtocolo { get; set; }
        public string Previsao { get; set; }
        public Nullable<int> Parcelas { get; set; }
        public Nullable<int> IdTipoDeTitulo { get; set; }
        public Nullable<System.DateTime> DataHoraInclusao { get; set; }
        public string TipoDeEntrada { get; set; }
        public Nullable<int> IdContaContabil { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual CondicaoDePagamento CondicaoDePagamento { get; set; }
        public virtual ICollection<DocumentoEdi> DocumentoEdis { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<TituloDocumento> TituloDocumentoes { get; set; }
        public virtual ICollection<TituloDuplicata> TituloDuplicatas { get; set; }
        public virtual ICollection<TituloHistorico> TituloHistoricoes { get; set; }
        public virtual ICollection<TituloImagem> TituloImagems { get; set; }
    }
}
