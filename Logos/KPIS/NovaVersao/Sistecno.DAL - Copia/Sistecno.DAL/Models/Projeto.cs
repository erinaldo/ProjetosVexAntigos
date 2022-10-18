using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Projeto
    {
        public Projeto()
        {
            this.ProjetoArquivoes = new List<ProjetoArquivo>();
            this.ProjetoFilials = new List<ProjetoFilial>();
            this.ProjetoItems = new List<ProjetoItem>();
            this.ProjetoProducaos = new List<ProjetoProducao>();
        }

        public int IdProjeto { get; set; }
        public int IdFilial { get; set; }
        public string Nome { get; set; }
        public string ContatoCliente { get; set; }
        public string ContatoContratado { get; set; }
        public string UtilizaAreaClimatizada { get; set; }
        public Nullable<System.DateTime> InicioDaProducao { get; set; }
        public Nullable<System.DateTime> FinalDaProducao { get; set; }
        public Nullable<System.DateTime> InicioDaEntrega { get; set; }
        public Nullable<System.DateTime> FinalDaEntrega { get; set; }
        public Nullable<int> TotalDeKits { get; set; }
        public Nullable<int> FatorPorCaixa { get; set; }
        public Nullable<int> FatorPorPallet { get; set; }
        public Nullable<decimal> PesoPorKit { get; set; }
        public Nullable<decimal> FretePorKit { get; set; }
        public Nullable<decimal> TempoDeProducao { get; set; }
        public Nullable<int> Turnos { get; set; }
        public Nullable<int> PessoasPorTurno { get; set; }
        public Nullable<int> MaoDeObra { get; set; }
        public string Status { get; set; }
        public string Edi { get; set; }
        public string DigitalizarComprovante { get; set; }
        public string EmitirCTRC { get; set; }
        public string EmitirNotaFiscalServico { get; set; }
        public string FormaFaturamento { get; set; }
        public string Vencimento { get; set; }
        public string OrigemDaColeta { get; set; }
        public string ValorPorColeta { get; set; }
        public string PlanejamentoDeTransferencia { get; set; }
        public string Observacao { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<ProjetoArquivo> ProjetoArquivoes { get; set; }
        public virtual ICollection<ProjetoFilial> ProjetoFilials { get; set; }
        public virtual ICollection<ProjetoItem> ProjetoItems { get; set; }
        public virtual ICollection<ProjetoProducao> ProjetoProducaos { get; set; }
    }
}
