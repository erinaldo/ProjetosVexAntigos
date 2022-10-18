using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTFaturamentoCliente
    {
        public DTFaturamentoCliente()
        {
            this.DTFaturamentoClienteDocumentoes = new List<DTFaturamentoClienteDocumento>();
        }

        public int IdDtFaturamentoCliente { get; set; }
        public int IdDtFaturamento { get; set; }
        public int IdCadastro { get; set; }
        public Nullable<int> IdCte { get; set; }
        public Nullable<decimal> FretePeso { get; set; }
        public Nullable<decimal> FreteExcedente { get; set; }
        public Nullable<decimal> FretePorNotaFiscal { get; set; }
        public Nullable<decimal> FretePercentual { get; set; }
        public Nullable<decimal> FreteValor { get; set; }
        public Nullable<decimal> Gris { get; set; }
        public Nullable<decimal> Cat { get; set; }
        public Nullable<decimal> Despacho { get; set; }
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
        public Nullable<decimal> DescargaNaoPaletizada { get; set; }
        public Nullable<decimal> DescargaPaletizada { get; set; }
        public Nullable<decimal> DevCan { get; set; }
        public Nullable<decimal> ImpostoARecolher { get; set; }
        public Nullable<decimal> Outros { get; set; }
        public string Proprietario { get; set; }
        public Nullable<decimal> DiferencaDeFrete { get; set; }
        public Nullable<decimal> BaseDeCalculo { get; set; }
        public Nullable<decimal> FreteSimulado { get; set; }
        public Nullable<int> IdTabelaDeFreteRotaModal { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public Nullable<decimal> PesoCalculado { get; set; }
        public Nullable<decimal> MetragemCubica { get; set; }
        public Nullable<decimal> Volumes { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public Nullable<int> IdRegiaoItemFilial { get; set; }
        public string ClasseCFOP { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual DTFaturamento DTFaturamento { get; set; }
        public virtual ICollection<DTFaturamentoClienteDocumento> DTFaturamentoClienteDocumentoes { get; set; }
    }
}
