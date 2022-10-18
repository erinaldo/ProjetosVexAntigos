using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TESCFOP
    {
        public int IdTesCFOP { get; set; }
        public int IdTes { get; set; }
        public int IdCfop { get; set; }
        public Nullable<int> IdCfopContraPartida { get; set; }
        public string GeraDuplicata { get; set; }
        public string GeraRomaneio { get; set; }
        public string GeraDocumento { get; set; }
        public string MovimentaEstoque { get; set; }
        public string ImprimeDocumento { get; set; }
        public string RequerDocumentoDeOrigem { get; set; }
        public string CalculaICMS { get; set; }
        public string CalculaIPI { get; set; }
        public string DestacaIPI { get; set; }
        public string CalculaISS { get; set; }
        public string CalculaPIS { get; set; }
        public string CalculaCOFINS { get; set; }
        public string CalculaCSLL { get; set; }
        public string AtualizaEstoque { get; set; }
        public string CreditaICMS { get; set; }
        public string CreditaIPI { get; set; }
        public string IPINaBaseICMS { get; set; }
        public Nullable<decimal> ReducaoICMS { get; set; }
        public Nullable<decimal> ReducaoIPI { get; set; }
        public Nullable<decimal> ReducaoISS { get; set; }
        public string IPISobreFrete { get; set; }
        public string PoderDeTerceiros { get; set; }
        public string DiferencaICMS { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Observacao { get; set; }
        public string Ativo { get; set; }
        public string CSTCTe { get; set; }
        public string Servicos { get; set; }
        public string CalcularFrete { get; set; }
        public string TipoDeItem { get; set; }
        public string SituacaoTributariaIPI { get; set; }
        public string SituacaoTributariaCofins { get; set; }
        public string SituacaoTributariaIcms { get; set; }
        public string SituacaoTributariaPIS { get; set; }
        public string Aplicacao { get; set; }
        public string GerarCtrc { get; set; }
        public string EnquadramentoIPI { get; set; }
        public virtual Cfop Cfop { get; set; }
        public virtual TE TE { get; set; }
    }
}
