using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Regiao
    {
        public Regiao()
        {
            this.AgrupamentoRegiaos = new List<AgrupamentoRegiao>();
            this.RegiaoItems = new List<RegiaoItem>();
            this.Romaneios = new List<Romaneio>();
            this.RomaneioPrevisaoRegiaos = new List<RomaneioPrevisaoRegiao>();
            this.TabelaDeFreteRotas = new List<TabelaDeFreteRota>();
            this.TabelaDeFreteRotas1 = new List<TabelaDeFreteRota>();
            this.VeiculoTabelaRegiaos = new List<VeiculoTabelaRegiao>();
        }

        public int IDRegiao { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDTransportadora { get; set; }
        public string Nome { get; set; }
        public string Ordem { get; set; }
        public Nullable<decimal> PrazoDeEntrega { get; set; }
        public string Ativo { get; set; }
        public string DiasDeSaida { get; set; }
        public Nullable<int> IdTabelaDeFrete { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> Ordenar { get; set; }
        public virtual ICollection<AgrupamentoRegiao> AgrupamentoRegiaos { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual TabelaDeFrete TabelaDeFrete { get; set; }
        public virtual Transportadora Transportadora { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
        public virtual ICollection<Romaneio> Romaneios { get; set; }
        public virtual ICollection<RomaneioPrevisaoRegiao> RomaneioPrevisaoRegiaos { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas1 { get; set; }
        public virtual ICollection<VeiculoTabelaRegiao> VeiculoTabelaRegiaos { get; set; }
    }
}
