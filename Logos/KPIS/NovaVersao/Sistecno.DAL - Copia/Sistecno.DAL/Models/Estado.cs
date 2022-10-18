using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Estado
    {
        public Estado()
        {
            this.Cidades = new List<Cidade>();
            this.EstadoFaixaDeCeps = new List<EstadoFaixaDeCep>();
            this.Feriadoes = new List<Feriado>();
            this.Icms = new List<Icm>();
            this.Icms1 = new List<Icm>();
            this.RegiaoItems = new List<RegiaoItem>();
            this.TabelaDeFreteRotas = new List<TabelaDeFreteRota>();
            this.TabelaDeFreteRotas1 = new List<TabelaDeFreteRota>();
        }

        public int IDEstado { get; set; }
        public int IDPais { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public string CodigoDoIbge { get; set; }
        public string CepInicial { get; set; }
        public string CepFinal { get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }
        public virtual Pai Pai { get; set; }
        public virtual ICollection<EstadoFaixaDeCep> EstadoFaixaDeCeps { get; set; }
        public virtual ICollection<Feriado> Feriadoes { get; set; }
        public virtual ICollection<Icm> Icms { get; set; }
        public virtual ICollection<Icm> Icms1 { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas1 { get; set; }
    }
}
