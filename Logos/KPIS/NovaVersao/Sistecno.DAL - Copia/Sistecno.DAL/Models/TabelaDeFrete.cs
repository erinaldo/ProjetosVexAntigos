using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TabelaDeFrete
    {
        public TabelaDeFrete()
        {
            this.Regiaos = new List<Regiao>();
            this.UsuarioDeTabelaDeFretes = new List<UsuarioDeTabelaDeFrete>();
            this.TabelaDeFreteProdutoes = new List<TabelaDeFreteProduto>();
            this.TabelaDeFreteRotas = new List<TabelaDeFreteRota>();
            this.TabelaDeFreteVigencias = new List<TabelaDeFreteVigencia>();
        }

        public int IDTabelaDeFrete { get; set; }
        public string Descricao { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public string TipoDeTabela { get; set; }
        public string Ativo { get; set; }
        public string TipoDeCalculo { get; set; }
        public virtual ICollection<Regiao> Regiaos { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
        public virtual ICollection<TabelaDeFreteProduto> TabelaDeFreteProdutoes { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas { get; set; }
        public virtual ICollection<TabelaDeFreteVigencia> TabelaDeFreteVigencias { get; set; }
    }
}
