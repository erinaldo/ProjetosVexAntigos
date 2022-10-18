using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TE
    {
        public TE()
        {
            this.TESCFOPs = new List<TESCFOP>();
            this.TESControles = new List<TESControle>();
        }

        public int IDTES { get; set; }
        public string Descricao { get; set; }
        public string Aplicacao { get; set; }
        public string TipoDeDocumento { get; set; }
        public string EntradaSaida { get; set; }
        public string TipoDeServico { get; set; }
        public string GeraDuplicata { get; set; }
        public string GeraRomaneio { get; set; }
        public string GeraDocumento { get; set; }
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
        public string LivroICMS { get; set; }
        public string LivroIPI { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> IDOcorrencia { get; set; }
        public string ConsideraOM { get; set; }
        public Nullable<int> IDLocal { get; set; }
        public string Ativo { get; set; }
        public string Sistema { get; set; }
        public string OrigemMovimentacao { get; set; }
        public string CalculaFrete { get; set; }
        public string MovimentaEstoque { get; set; }
        public string LoteEntradaSaida { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual ICollection<TESCFOP> TESCFOPs { get; set; }
        public virtual ICollection<TESControle> TESControles { get; set; }
    }
}
